using ALE_MexJet.Clases;
using ALE_MexJet.DomainModel;
using ALE_MexJet.Interfaces;
using ALE_MexJet.Objetos;
using ALE_MexJet.Presenter;
using DevExpress.Export;
using DevExpress.Web;
using DevExpress.Web.Data;
using DevExpress.XtraPrinting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NucleoBase.Core;

namespace ALE_MexJet.Views.Catalogos
{
    public partial class frmMembresias : System.Web.UI.Page, IViewCat
    {
        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                DrPermisos = Utils.ObtenerPermisos(Enumeraciones.Pantallas.Membresias);
                LoadActions(DrPermisos);
                oPresenter = new Membresia_Presenter(this, new DBMembresias());
                gvMembresias.Columns["Status"].Visible = true;
                gvMembresias.SettingsPager.Position = PagerPosition.TopAndBottom;
                gvMembresias.SettingsPager.ShowDisabledButtons = true;
                gvMembresias.SettingsPager.ShowNumericButtons = true;
                gvMembresias.SettingsPager.ShowSeparators = true;
                gvMembresias.SettingsPager.Summary.Visible = true;
                gvMembresias.SettingsPager.PageSizeItemSettings.Visible = true;
                gvMembresias.SettingsPager.PageSizeItemSettings.Position = PagerPageSizePosition.Right;
                gvMembresias.SettingsText.SearchPanelEditorNullText = "Ingrese la información a buscar";
                ObtieneValores();
                if (IsPostBack)
                {
                    if (!string.IsNullOrEmpty(ViewState["oDatos"].S()))
                    {
                        LoadObjects((DataTable)ViewState["oDatos"]);
                        ObtieneValores();
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "Page_Load", "Aviso");
            }
        }

        protected void gvMembresias_CancelRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            try
            {
                gvMembresias.Columns["Status"].Visible = true;
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvMembresias_CancelRowEditing", "Aviso");
            }
        }

        protected void gvMembresias_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            try
            {
                eCrud = Enumeraciones.TipoOperacion.Eliminar;
                oCrud = e;
                CancelEditing(e);

                if (eDeleteObj != null)
                    eDeleteObj(sender, e);

                ObtieneValores();
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvMembresias_RowDeleting", "Aviso");
            }
        }

        protected void gvMembresias_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            try
            {
                eCrud = Enumeraciones.TipoOperacion.Actualizar;
                oCrud = e;

                if (eSaveObj != null)
                    eSaveObj(sender, e);

                CancelEditing(e);

                ObtieneValores();
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvMembresias_RowUpdating", "Aviso");
            }
        }

        protected void gvMembresias_StartRowEditing(object sender, ASPxStartRowEditingEventArgs e)
        {
            try
            {
                gvMembresias.SettingsText.PopupEditFormCaption = "Formulario de Edición";
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvMembresias_StartRowEditing", "Aviso");
            }
        }


        protected void gvMembresias_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            try
            {
                eCrud = Enumeraciones.TipoOperacion.Insertar;
                oCrud = e;

                if (eNewObj != null)
                    eNewObj(sender, e);

                CancelEditing(e);

                ObtieneValores();
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvMembresias_RowInserting", "Aviso");
            }
        }

        protected void gvMembresias_RowValidating1(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            try
            {
                oCrud = e;

                if (eObjSelected != null)
                    eObjSelected(sender, e);

                if (bDuplicado)
                {
                    AddError(e.Errors, gvMembresias.Columns["CodigoMembresia"], "Este codigo de membresia ya existe, favor de validarla.");
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvMembresias_RowValidating1", "Aviso");
            }
        }
        protected void gvMembresias_CellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
        {
            try
            {
                e.Editor.ReadOnly = false;
                ASPxGridView gridView = (ASPxGridView)sender;

                if (gridView.IsNewRowEditing)
                {
                    eCrud = Enumeraciones.TipoOperacion.Insertar;
                    gvMembresias.Columns["Status"].Visible = false;

                }
                else
                {
                    eCrud = Enumeraciones.TipoOperacion.Actualizar;
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvMembresias_CellEditorInitialize", "Aviso");
            }
        }

        protected void btnExportar_Click(object sender, EventArgs e)
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

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                ObtieneValores();
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnBuscar_Click", "Aviso");
            }
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                gvMembresias.SettingsText.PopupEditFormCaption = "Formulario de Creación";
                gvMembresias.Columns["Status"].Visible = false;
                gvMembresias.AddNewRow();
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnNuevo_Click", "Aviso");
            }

        }

        protected void UpdatePanel1_Unload(object sender, EventArgs e)
        {
            try
            {
                MethodInfo methodInfo = typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Where(i => i.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel")).First();
                methodInfo.Invoke(ScriptManager.GetCurrent(Page),
                    new object[] { sender as UpdatePanel });
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "UpdatePanel1_Unload", "Aviso");
            }

        }
        protected void gvMembresias_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {
            try
            {
                int iPos = 0;
                for (iPos = 0; iPos < DrPermisos[0].ItemArray.Length; iPos++)
                {
                    switch (iPos)
                    {
                        case 5:
                            if (DrPermisos[0].ItemArray[iPos].S().I() == Convert.ToInt32(Enumeraciones.Bloqueo.Permitido))
                            {
                                if (e.ButtonType == ColumnCommandButtonType.New)
                                    e.Enabled = true;
                            }
                            else
                            {
                                if (e.ButtonType == ColumnCommandButtonType.New)
                                    e.Enabled = false;
                            }
                            break;
                        case 6:
                            if (DrPermisos[0].ItemArray[iPos].S().I() == Convert.ToInt32(Enumeraciones.Bloqueo.Permitido))
                            {
                                if (e.ButtonType == ColumnCommandButtonType.Edit)
                                    e.Enabled = true;
                            }
                            else
                            {
                                if (e.ButtonType == ColumnCommandButtonType.Edit)
                                    e.Enabled = false;
                            }
                            break;
                        case 7:
                            if (DrPermisos[0].ItemArray[iPos].S().I() == Convert.ToInt32(Enumeraciones.Bloqueo.Permitido))
                            {
                                if (e.ButtonType == ColumnCommandButtonType.Delete)
                                    e.Enabled = true;
                            }
                            else
                            {
                                if (e.ButtonType == ColumnCommandButtonType.Delete)
                                    e.Enabled = false;
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvMembresias_CommandButtonInitialize", "Aviso");
            }
        }
        #endregion

        #region "METODOS"

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
                gvMembresias.DataSource = null;
                if (dtObject.Rows.Count > 0)
                {
                    gvMembresias.DataSource = dtObject;
                    gvMembresias.DataBind();
                }
                ViewState["oDatos"] = dtObject;
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
                gvMembresias.JSProperties["cpText"] = sMensaje;
                gvMembresias.JSProperties["cpShowPopup"] = true;
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
                gvMembresias.CancelEdit();
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
        public void LoadActions(DataRow[] DrActions)
        {
            try
            {
                int iPos = 0;
                if (DrActions.Length == 0)
                {
                    btnBusqueda.Enabled = false;
                    txtTextoBusqueda.Enabled = false;
                    ddlTipoBusqueda.Enabled = false;
                    btnNuevo.Enabled = false;
                    btnExcel.Enabled = false;
                    btnNuevo2.Enabled = false;
                    ASPxButton2.Enabled = false;
                }
                else
                {
                    for (iPos = 0; iPos < DrActions[0].ItemArray.Length; iPos++)
                    {
                        switch (iPos)
                        {
                            case 4:
                                if (DrActions[0].ItemArray[iPos].S().I() == Convert.ToInt32(Enumeraciones.Bloqueo.Permitido))
                                {
                                    btnBusqueda.Enabled = true;
                                    txtTextoBusqueda.Enabled = true;
                                    ddlTipoBusqueda.Enabled = true;
                                    btnExcel.Enabled = true;
                                    ASPxButton2.Enabled = true;
                                }
                                else
                                {
                                    btnBusqueda.Enabled = false;
                                    txtTextoBusqueda.Enabled = false;
                                    ddlTipoBusqueda.Enabled = false;
                                    btnExcel.Enabled = false;
                                    ASPxButton2.Enabled = false;
                                }
                                break;
                            case 5:
                                if (DrActions[0].ItemArray[iPos].S().I() == Convert.ToInt32(Enumeraciones.Bloqueo.Permitido))
                                {
                                    btnNuevo.Enabled = true;
                                    btnNuevo2.Enabled = true;
                                }
                                else
                                {
                                    btnNuevo.Enabled = false;
                                    btnNuevo2.Enabled = false;
                                }
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region "Vars y Propiedades"

        private const string sClase = "frmMembresias.aspx.cs";
        private const string sPagina = "frmMembresias.aspx";

        Membresia_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        UserIdentity oUsuario = new UserIdentity();
        public DataRow[] DrPermisos
        {
            get { return (DataRow[])Session["DrPermisos"]; }
            set { Session["DrPermisos"] = value; }
        }
        public bool bDuplicado
        {
            get
            {
                return (bool)ViewState["RegistroDuplicado"];
            }
            set
            {
                ViewState["RegistroDuplicado"] = value;
            }
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
                int iEstatus = -1;
                string sDescripcion = string.Empty;

                switch (ddlTipoBusqueda.SelectedValue.S())
                {
                    case "1":
                        iEstatus = -1;
                        sDescripcion = txtTextoBusqueda.Text.S();
                        break;
                    case "2":
                        iEstatus = 1;
                        sDescripcion = string.Empty;
                        break;
                    case "3":
                        iEstatus = 0;
                        sDescripcion = string.Empty;
                        break;
                }

                return new object[]{
                                        "@Descripcion", "%" + sDescripcion + "%",
                                        "@estatus", iEstatus
                                    };
            }

        }

        #endregion
    }
}