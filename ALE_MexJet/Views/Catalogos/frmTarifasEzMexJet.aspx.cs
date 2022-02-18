using ALE_MexJet.DomainModel;
using ALE_MexJet.Interfaces;
using ALE_MexJet.Objetos;
using ALE_MexJet.Presenter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NucleoBase.Core;
using DevExpress.Web;
using ALE_MexJet.Clases;
using System.Reflection;
using System.ComponentModel;
using DevExpress.XtraPrinting;
using DevExpress.Export;

namespace ALE_MexJet.Views.Catalogos
{
    public partial class frmTarifasEzMexJet : System.Web.UI.Page, IViewCat
    {
        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            oPresenter = new TarifaGrupoModeloEz_Presenter(this, new DBTarifaGrupoModeloEz());
            gvTarifasEz.Columns["Status"].Visible = true;
            gvTarifasEz.SettingsPager.Position = PagerPosition.TopAndBottom;
            gvTarifasEz.SettingsPager.ShowDisabledButtons = true;
            gvTarifasEz.SettingsPager.ShowNumericButtons = true;
            gvTarifasEz.SettingsPager.ShowSeparators = true;
            gvTarifasEz.SettingsPager.Summary.Visible = true;
            gvTarifasEz.SettingsPager.PageSizeItemSettings.Visible = true;
            gvTarifasEz.SettingsPager.PageSizeItemSettings.Position = PagerPageSizePosition.Right;
            gvTarifasEz.SettingsText.SearchPanelEditorNullText = "Ingrese la información a buscar";

            ObtieneValores();

            if (!IsPostBack)
            {
                if (gvTarifasEz.VisibleRowCount < 1)
                {

                    gvTarifasEz.SettingsText.PopupEditFormCaption = "Formulario de Creación";
                    gvTarifasEz.Columns["Status"].Visible = false;
                    gvTarifasEz.AddNewRow();
                }
            }
        }

        protected void btnBusqueda_Click(object sender, EventArgs e)
        {
            try
            {
                ObtieneValores();
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnBusqueda_Click", "Aviso");
            }
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                gvTarifasEz.SettingsText.PopupEditFormCaption = "Formulario de Creación";
                gvTarifasEz.Columns["Status"].Visible = false;
                gvTarifasEz.AddNewRow();
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnNuevo_Click", "Aviso");
            }
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                Exporter.WriteXlsToResponse(new XlsExportOptionsEx() { ExportType = ExportType.DataAware });
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnExportar_Click", "Aviso");
            }
        }

        protected void gvTarifasEz_CellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
        {
            try
            {
                e.Editor.ReadOnly = false;
                ASPxGridView gridView = (ASPxGridView)sender;

                if (gridView.IsNewRowEditing)
                {
                    eCrud = Enumeraciones.TipoOperacion.Insertar;
                    gvTarifasEz.Columns["Status"].Visible = false;

                }
                else
                {
                    eCrud = Enumeraciones.TipoOperacion.Actualizar;
                }

                if (e.Column.FieldName == "IdGrupoModelo")
                {

                    DataTable dtGruposModelo = new DBSolicitudesVuelo().DBSearchGrupoModelo();

                    if (dtGruposModelo != null)
                    {
                        ASPxComboBox cmb = e.Editor as ASPxComboBox;
                        cmb.DataSource = dtGruposModelo;
                        cmb.ValueField = "GrupoModeloId";
                        cmb.ValueType = typeof(int);
                        cmb.TextField = "Descripcion";
                        cmb.DataBindItems();
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvTarifasEz_CellEditorInitialize", "Aviso");
            }
        }

        protected void gvTarifasEz_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            try
            {
                eCrud = Enumeraciones.TipoOperacion.Eliminar;
                oCrud = e;
                CancelEditing(e);

                if (eDeleteObj != null)
                    eDeleteObj(sender, e);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvTarifasEz_RowDeleting", "Aviso");
            }
        }

        protected void gvTarifasEz_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            try
            {
                eCrud = Enumeraciones.TipoOperacion.Insertar;
                oCrud = e;

                if (eNewObj != null)
                    eNewObj(sender, e);

                CancelEditing(e);
                gvTarifasEz.Columns["Status"].Visible = true;
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvTarifasEz_RowInserting", "Aviso");
            }
        }

        protected void gvTarifasEz_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            try
            {
                eCrud = Enumeraciones.TipoOperacion.Actualizar;
                oCrud = e;

                if (eSaveObj != null)
                    eSaveObj(sender, e);

                CancelEditing(e);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvTarifasEz_RowUpdating", "Aviso");
            }
        }

        protected void gvTarifasEz_CommandButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCommandButtonEventArgs e)
        {

        }

        protected void gvTarifasEz_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            try
            {
                oCrud = e;
                if (eObjSelected != null)
                    eObjSelected(sender, e);
                if (bDuplicado)
                {
                    AddError(e.Errors, gvTarifasEz.Columns["IdGrupoModelo"], "Este Grupo Modelo ya tiene una tarifa, favor de validarlo.");
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvTarifasEz_RowValidating", "Aviso");
            }
        }

        protected void gvTarifasEz_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            try
            {
                gvTarifasEz.SettingsText.PopupEditFormCaption = "Formulario de Edición";
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvTarifasEz_StartRowEditing", "Aviso");
            }
        }

        protected void gvTarifasEz_CancelRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            try
            {
                gvTarifasEz.Columns["Status"].Visible = true;
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvTarifasEz_CancelRowEditing", "Aviso");
            }
        }

        protected void Unnamed_Unload(object sender, EventArgs e)
        {
            try
            {
                MethodInfo methodInfo = typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Where(i => i.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel")).First();
                methodInfo.Invoke(ScriptManager.GetCurrent(Page),
                    new object[] { sender as UpdatePanel });
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "Unnamed_Unload", "Aviso");
            }
        }
        #endregion


        #region METODOS
        public void ObtieneValores()
        {
            try
            {
                if (eSearchObj != null)
                    eSearchObj(null, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LoadObjects(DataTable dtObject)
        {
            try
            {
                gvTarifasEz.DataSource = null;
                ViewState["oDatos"] = null;

                gvTarifasEz.DataSource = dtObject;
                ViewState["oDatos"] = dtObject;

                gvTarifasEz.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void MostrarMensaje(string sMensaje, string sCaption)
        {
            try
            {
                popup.HeaderText = sCaption;
                gvTarifasEz.JSProperties["cpText"] = sMensaje;
                gvTarifasEz.JSProperties["cpShowPopup"] = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void CancelEditing(CancelEventArgs e)
        {
            try
            {
                e.Cancel = true;
                gvTarifasEz.CancelEdit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        void AddError(Dictionary<GridViewColumn, string> errors, GridViewColumn column, string errorText)
        {
            try
            {
                if (errors.ContainsKey(column)) return;
                errors[column] = errorText;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion



        #region "Vars y Propiedades"

        private const string sClase = "frmTarifasEzMexJet.aspx.cs";
        private const string sPagina = "frmTarifasEzMexJet.aspx";

        TarifaGrupoModeloEz_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        UserIdentity oUsuario = new UserIdentity();

        public bool bDuplicado
        {
            get { return (bool)ViewState["RegistroDuplicado"]; }
            set { ViewState["RegistroDuplicado"] = value; }
        }

        public object oCrud
        {
            get { return ViewState["CrudPiloto"]; }
            set { ViewState["CrudPiloto"] = value; }
        }

        public ALE_MexJet.Objetos.Enumeraciones.TipoOperacion eCrud
        {
            get { return (ALE_MexJet.Objetos.Enumeraciones.TipoOperacion)ViewState["eCrud"]; }
            set { ViewState["eCrud"] = value; }
        }

        public object[] oArrFiltros
        {
            get
            {
                string sDescripcion = string.Empty;

                switch (ddlTipoBusqueda.SelectedValue.S())
                {
                    case "1":
                        sDescripcion = textTextoBusqueda.Text.S();
                        break;
                }

                return new object[]{
                                        "@Descripcion", sDescripcion
                                    };
            }
        }

        #endregion
    }
}