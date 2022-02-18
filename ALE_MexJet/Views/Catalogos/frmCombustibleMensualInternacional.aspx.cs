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
using DevExpress.Export;
using DevExpress.XtraPrinting;
using System.ComponentModel;
using ALE_MexJet.ControlesUsuario;
using DevExpress.Web.Data;
using DevExpress.Web;
using System.Reflection;
using ALE_MexJet.Clases;

namespace ALE_MexJet.Views.Catalogos
{
    public partial class frmCombustibleMensualInternacional : System.Web.UI.Page, IViewCombustibleMensualInternacional
    {
        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                DrPermisos = Utils.ObtenerPermisos(Enumeraciones.Pantallas.CombustibleMensualInternacional);
                LoadActions(DrPermisos);
                oPresenter = new CombustibleMensualInternacional_Presenter(this, new DBCombustibleMensualInternacional());
                gvCombustibleMensualInt.SettingsPager.Position = PagerPosition.TopAndBottom;
                gvCombustibleMensualInt.SettingsPager.ShowDisabledButtons = true;
                gvCombustibleMensualInt.SettingsPager.ShowNumericButtons = true;
                gvCombustibleMensualInt.SettingsPager.ShowSeparators = true;
                gvCombustibleMensualInt.SettingsPager.Summary.Visible = true;
                gvCombustibleMensualInt.SettingsPager.PageSizeItemSettings.Visible = true;
                gvCombustibleMensualInt.SettingsPager.PageSizeItemSettings.Position = PagerPageSizePosition.Right;
                gvCombustibleMensualInt.SettingsText.SearchPanelEditorNullText = "Ingrese la información a buscar";


                ObtieneValoresCombustible();
                if (!IsPostBack)
                {
                    gvCombustibleMensualInt.Columns["Status"].Visible = true;
                    if (gvCombustibleMensualInt.VisibleRowCount < 1)
                    {
                        gvCombustibleMensualInt.SettingsText.PopupEditFormCaption = "Formulario de Creación";
                        gvCombustibleMensualInt.Columns["Status"].Visible = false;
                        gvCombustibleMensualInt.AddNewRow();
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "Page_Load", "Aviso");
            }

        }
        protected void gvCombustibleMensualInt_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            try
            {
                e.Editor.ReadOnly = false;
                if (e.Column.FieldName == "IdMes")
                {
                    if (eSearchObjMes != null)
                        eSearchObjMes(this, e);
                    ASPxComboBox cmb = e.Editor as ASPxComboBox;
                    cmb.DataSource = (DataTable)ViewState["oDatosMes"];
                    cmb.ValueField = "IdMes";
                    cmb.ValueType = typeof(Int32);
                    cmb.TextField = "Nombre";
                    cmb.DataBindItems();
                }
                ASPxGridView gridView = (ASPxGridView)sender;

                if (gridView.IsNewRowEditing)
                {
                    eCrud = Enumeraciones.TipoOperacion.Insertar;
                }
                else
                {
                    eCrud = Enumeraciones.TipoOperacion.Actualizar;
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvCombustibleMensualInt_CellEditorInitialize", "Aviso");
            }
        }
        protected void gvCombustibleMensualInt_RowDeleting(object sender, ASPxDataDeletingEventArgs e)
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
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvCombustibleMensualInt_RowDeleting", "Aviso");
            }
        }
        protected void gvCombustibleMensualInt_RowInserting(object sender, ASPxDataInsertingEventArgs e)
        {
            try
            {
                eCrud = Enumeraciones.TipoOperacion.Insertar;
                oCrud = e;

                if (eNewObj != null)
                    eNewObj(sender, e);

                CancelEditing(e);
                gvCombustibleMensualInt.Columns["Status"].Visible = true;
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvCombustibleMensualInt_RowInserting", "Aviso");
            }
        }
        protected void gvCombustibleMensualInt_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
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
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvCombustibleMensualInt_RowUpdating", "Aviso");
            }
        }
        protected void gvCombustibleMensualInt_StartRowEditing(object sender, ASPxStartRowEditingEventArgs e)
        {
            try
            {
                gvCombustibleMensualInt.SettingsText.PopupEditFormCaption = "Formulario de Edición";
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvCombustibleMensualInt_StartRowEditing", "Aviso");
            }
        }

        protected void gvCombustibleMensualInt_CancelRowEditing(object sender, ASPxStartRowEditingEventArgs e)
        {
            try
            {
                gvCombustibleMensualInt.Columns["Status"].Visible = true;
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvCombustibleMensualInt_CancelRowEditing", "Aviso");
            }
        }

        protected void gvCombustibleMensualInt_RowValidating(object sender, ASPxDataValidationEventArgs e)
        {
            try
            {
                oCrud = e;

                if (eObjSelected != null)
                    eObjSelected(sender, e);

                if (bDuplicado)
                {
                    AddError(e.Errors, gvCombustibleMensualInt.Columns["Importe"], "Este registro ya existe, favor de validarlo.");
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvCombustibleMensualInt_RowValidating", "Aviso");
            }
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                ObtieneValoresCombustible();
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
                gvCombustibleMensualInt.Columns["Status"].Visible = false;
                gvCombustibleMensualInt.SettingsText.PopupEditFormCaption = "Formulario de Creación";
                gvCombustibleMensualInt.AddNewRow();
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnNuevo_Click", "Aviso");
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
        protected void gvCombustibleMensualInt_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {
            try
            {
                int iPos = 0;
                for (iPos = 0; iPos < DrPermisos[0].ItemArray.Length; iPos++)
                {
                    switch (iPos)
                    {
                        case 5: if (DrPermisos[0].ItemArray[iPos].S().I() == Convert.ToInt32(Enumeraciones.Bloqueo.Permitido))
                            {
                                if (e.ButtonType == ColumnCommandButtonType.New)
                                    e.Enabled = true;
                            }
                            else
                            {
                                if (e.ButtonType == ColumnCommandButtonType.New)
                                    e.Enabled = false;
                            } break;
                        case 6: if (DrPermisos[0].ItemArray[iPos].S().I() == Convert.ToInt32(Enumeraciones.Bloqueo.Permitido))
                            {
                                if (e.ButtonType == ColumnCommandButtonType.Edit)
                                    e.Enabled = true;
                            }
                            else
                            {
                                if (e.ButtonType == ColumnCommandButtonType.Edit)
                                    e.Enabled = false;
                            } break;
                        case 7: if (DrPermisos[0].ItemArray[iPos].S().I() == Convert.ToInt32(Enumeraciones.Bloqueo.Permitido))
                            {
                                if (e.ButtonType == ColumnCommandButtonType.Delete)
                                    e.Enabled = true;
                            }
                            else
                            {
                                if (e.ButtonType == ColumnCommandButtonType.Delete)
                                    e.Enabled = false;
                            } break;
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvCombustibleMensualInt_CommandButtonInitialize", "Aviso");
            }
        }
        #endregion

        #region "METODOS"

        public void ObtieneValoresCombustible()
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
                gvCombustibleMensualInt.DataSource = null;
                ViewState["oDatos"] = null;

                gvCombustibleMensualInt.DataSource = dtObject;
                ViewState["oDatos"] = dtObject;

                gvCombustibleMensualInt.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void LoadObjectsMes(DataTable dtObject)
        {
            try
            {
                ViewState["oDatosMes"] = dtObject;
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
                gvCombustibleMensualInt.JSProperties["cpText"] = sMensaje;
                gvCombustibleMensualInt.JSProperties["cpShowPopup"] = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void CancelEditing(CancelEventArgs e)
        {
            e.Cancel = true;
            gvCombustibleMensualInt.CancelEdit();
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
                            case 4: if (DrActions[0].ItemArray[iPos].S().I() == Convert.ToInt32(Enumeraciones.Bloqueo.Permitido))
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
                                } break;
                            case 5: if (DrActions[0].ItemArray[iPos].S().I() == Convert.ToInt32(Enumeraciones.Bloqueo.Permitido))
                                {
                                    btnNuevo.Enabled = true;
                                    btnNuevo2.Enabled = true;
                                }
                                else
                                {
                                    btnNuevo.Enabled = false;
                                    btnNuevo2.Enabled = false;
                                } break;
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
        private const string sClase = "frmCombustibleMensualInternacional.aspx.cs";
        private const string sPagina = "frmCombustibleMensualInternacional.aspx";

        CombustibleMensualInternacional_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eSearchObjMes;
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

        public int iIdCombustibleMenInt
        {
            get { return ViewState["IdCombusMen"].S().I(); }
            set {  ViewState["IdCombusMen"] = value; }
        }
        public object[] oArrFiltros
        {
            get
            {
                int iAnio = 0;
                string  sMes = string.Empty;
                decimal dimporte = 0.0M;
                int iEstatus = -1;

                switch (ddlTipoBusqueda.SelectedValue.S())
                {
                    case "1":
                        iAnio = txtTextoBusqueda.Text.S().I();
                        sMes = string.Empty;
                        dimporte = 0.0M;
                        iEstatus = -1;
                        break;
                    case "2":
                        iAnio = 0;
                        sMes = txtTextoBusqueda.Text.S();
                        dimporte = 0.0M;
                        iEstatus = -1;
                        break;
                    case "3":
                        iAnio = 0;
                        sMes = string.Empty;
                        dimporte = txtTextoBusqueda.Text.S().D();
                        iEstatus = -1;
                        break;
                    case "4":
                        iAnio = 0;
                        sMes = string.Empty;
                        dimporte = txtTextoBusqueda.Text.S().D();
                        iEstatus = 1;
                        break;
                    case "5":
                        iAnio = 0;
                        sMes = string.Empty;
                        dimporte = txtTextoBusqueda.Text.S().D();
                        iEstatus = 0;
                        break;
                }

                return new object[]{
                                        "@Anio", iAnio ,
                                        "@Mes", "%" +  sMes + "%",
                                        "@Importe", dimporte,
                                        "@estatus", iEstatus
                                    };
            }
        }

        public object[] oArrFiltrosMes
        {
            get
            {
                int iIdMes = 0;
                string sNombre = string.Empty;
                int iEstatus = 0;

                return new object[]{ 
                                        "@IdMes", iIdMes,
                                        "@Nombre", "%" + sNombre + "%",
                                        "@estatus", iEstatus
                                    };
            }
        }
        #endregion

        

        
    }
}