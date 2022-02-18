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
    public partial class frmPaquete : System.Web.UI.Page, IViewPaquete
    {
        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                DrPermisos = Utils.ObtenerPermisos(Enumeraciones.Pantallas.Paquete);
                LoadActions(DrPermisos);
                oPresenter = new Paquete_Presenter(this, new DBPaquete(), new DBPaqueteCuenta());
                gvPaquete.SettingsPager.Position = PagerPosition.TopAndBottom;
                gvPaquete.SettingsPager.ShowDisabledButtons = true;
                gvPaquete.SettingsPager.ShowNumericButtons = true;
                gvPaquete.SettingsPager.ShowSeparators = true;
                gvPaquete.SettingsPager.Summary.Visible = true;
                gvPaquete.SettingsPager.PageSizeItemSettings.Visible = true;
                gvPaquete.SettingsPager.PageSizeItemSettings.Position = PagerPageSizePosition.Right;
                gvPaquete.SettingsText.SearchPanelEditorNullText = "Ingrese la información a buscar";
                ObtieneValoresPaquete();
                if (!IsPostBack)
                {
                    if (gvPaquete.VisibleRowCount < 1)
                    {
                        gvPaquete.SettingsText.PopupEditFormCaption = "Fomulario de Creación";
                        gvPaquete.Columns["Status"].Visible = false;
                        gvPaquete.AddNewRow();
                    }
                }
                else
                {
                    if (Session["IndexGridDetail"] != null)
                    {
                        ObtieneValoresCuentas();
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "Page_Load", "Aviso");
            }
        }

        protected void gvPaquete_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            try
            {
                e.Editor.ReadOnly = false;
                ASPxGridView gridView = (ASPxGridView)sender;

                if (gridView.IsNewRowEditing)
                {
                    eCrud = Enumeraciones.TipoOperacion.Insertar;
                }
                else
                {
                    eCrud = Enumeraciones.TipoOperacion.Actualizar;
                }

                if (e.Column.FieldName == "ProyectoSAP")
                {
                    if (eGetProyectoSAP != null)
                        eGetProyectoSAP(sender, e);

                    ASPxComboBox cmb = e.Editor as ASPxComboBox;
                    cmb.DataSource = dtProyectoSAP;
                    cmb.ValueField = "PrcCode";
                    cmb.ValueType = typeof(string);
                    cmb.TextField = "PrcName";
                    cmb.DataBindItems();
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvPaquete_CellEditorInitialize", "Aviso");
            }
        }

        protected void gvCuentas_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            try
            {
                e.Editor.ReadOnly = false;
                ASPxGridView gridView = (ASPxGridView)sender;

                if (gridView.IsNewRowEditing)
                {
                    eCrud = Enumeraciones.TipoOperacion.Insertar;
                }
                else
                {
                    eCrud = Enumeraciones.TipoOperacion.Actualizar;
                }

                if (e.Column.FieldName == "ClaveCuenta")
                {
                    if (eGetCuentas != null)
                        eGetCuentas(sender, e);

                    ASPxComboBox cmb = e.Editor as ASPxComboBox;
                    cmb.DataSource = dtCuentas;
                    cmb.ValueField = "acct";
                    cmb.ValueType = typeof(string);
                    cmb.TextField = "CveDesc";
                    cmb.DataBindItems();
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvCuentas_CellEditorInitialize", "Aviso");
            }
        }
        protected void gvPaquete_RowDeleting(object sender, ASPxDataDeletingEventArgs e)
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
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvPaquete_RowDeleting", "Aviso");
            }
        }
        protected void gvCuentas_RowDeleting(object sender, ASPxDataDeletingEventArgs e)
        {
            try
            {
                eCrud = Enumeraciones.TipoOperacion.Eliminar;
                oCrud = e;
                CancelEditingCue(e);

                if (eDeleteObjCue != null)
                    eDeleteObjCue(sender, e);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvCuentas_RowDeleting", "Aviso");
            }
        }
        protected void gvPaquete_RowInserting(object sender, ASPxDataInsertingEventArgs e)
        {
            try
            {
                eCrud = Enumeraciones.TipoOperacion.Insertar;
                oCrud = e;

                if (eNewObj != null)
                    eNewObj(sender, e);

                CancelEditing(e);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvPaquete_RowInserting", "Aviso");
            }

        }
        protected void gvCuentas_RowInserting(object sender, ASPxDataInsertingEventArgs e)
        {
            try
            {
                int i = Session["IndexGridDetail"].S().I();
                DevExpress.Web.ASPxGridView gvCuentas = gvPaquete.FindDetailRowTemplateControl(i, "gvCuentas") as DevExpress.Web.ASPxGridView;
                eCrud = Enumeraciones.TipoOperacion.Insertar;
                oCrud = e;
                iIdTipoPaquete = Session["IdTipoPaquete"].S().I();


                if (eNewObjCue != null)
                    eNewObjCue(sender, e);

                CancelEditingCue(e);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvCuentas_RowInserting", "Aviso");
            }
        }

        protected void gvPaquete_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
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
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvPaquete_RowUpdating", "Aviso");
            }

        }
        protected void gvCuentas_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
        {
            try
            {
                DevExpress.Web.ASPxGridView gvDetalle = gvPaquete.FindDetailRowTemplateControl(Session["IndexGridDetail"].S().I(), "gvCuentas") as DevExpress.Web.ASPxGridView;
                eCrud = Enumeraciones.TipoOperacion.Actualizar;
                oCrud = e;

                if (eSaveObjCue != null)
                    eSaveObjCue(sender, e);

                CancelEditingCue(e);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvCuentas_RowUpdating", "Aviso");
            }

        }

        protected void gvPaquete_StartRowEditing(object sender, ASPxStartRowEditingEventArgs e)
        {
            try
            {
                gvPaquete.SettingsText.PopupEditFormCaption = "Formulario de Edición";
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvPaquete_StartRowEditing", "Aviso");
            }
        }
        protected void gvCuentas_StartRowEditing(object sender, ASPxStartRowEditingEventArgs e)
        {
            try
            {
                DevExpress.Web.ASPxGridView gvCuentas = gvPaquete.FindDetailRowTemplateControl(Session["IndexGridDetail"].S().I(), "gvCuentas") as DevExpress.Web.ASPxGridView;
                gvCuentas.SettingsText.PopupEditFormCaption = "Formulario de Edición";
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvCuentas_StartRowEditing", "Aviso");
            }
        }

        protected void gvPaquete_DetailRowExpandedChanged(object sender, ASPxGridViewDetailRowEventArgs e)
        {
            try
            {
                int iIndice = 0;
                int iIdTipoPaquete = 0;
                if (e.Expanded)
                {
                    iIndice = e.VisibleIndex;
                    Session["IndexGridDetail"] = iIndice;
                    object oFila = new object();
                    oFila = gvPaquete.GetRowValues(iIndice, gvPaquete.KeyFieldName);
                    iIdTipoPaquete = oFila.S().I();
                    Session["IdTipoPaquete"] = iIdTipoPaquete;
                    if (eSearchObjCue != null)
                        eSearchObjCue(null, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvPaquete_DetailRowExpandedChanged", "Aviso");
            }
        }

        protected void gvPaquete_RowValidating(object sender, ASPxDataValidationEventArgs e)
        {
            try
            {
                oCrud = e;

                if (eObjSelected != null)
                    eObjSelected(sender, e);

                if (bDuplicado)
                {
                    AddError(e.Errors, gvPaquete.Columns["Descripcion"], "El paquete ya está registrado favor de validarlo.");
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvPaquete_RowValidating", "Aviso");
            }
        }
        protected void gvCuentas_RowValidating(object sender, ASPxDataValidationEventArgs e)
        {
            try
            {
                DevExpress.Web.ASPxGridView gvCuentas = gvPaquete.FindDetailRowTemplateControl(Session["IndexGridDetail"].S().I(), "gvCuentas") as DevExpress.Web.ASPxGridView;
                oCrud = e;
                iIdTipoPaquete = Session["IdTipoPaquete"].S().I();

                if (eObjSelectedCue != null)
                    eObjSelectedCue(sender, e);

                if (bDuplicado)
                {
                    AddError(e.Errors, gvCuentas.Columns["Descripcion"], "Esta cuenta ya existe, favor de validarla.");
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvCuentas_RowValidating", "Aviso");
            }
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                ObtieneValoresPaquete();
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
                gvPaquete.SettingsText.PopupEditFormCaption = "Formulario de Creación";
                gvPaquete.Columns["Status"].Visible = false;
                gvPaquete.AddNewRow();
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
        protected void gvPaquete_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
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
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvPaquete_CommandButtonInitialize", "Aviso");
            }
        }
        #endregion

        #region "METODOS"

        public void ObtieneValoresPaquete()
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
        public void ObtieneValoresCuentas()
        {
            try
            {
                if (eSearchObjCue != null)
                    eSearchObjCue(null, EventArgs.Empty);
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
                gvPaquete.DataSource = null;
                ViewState["oDatos"] = null;

                gvPaquete.DataSource = dtObject;
                ViewState["oDatos"] = dtObject;

                gvPaquete.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void LoadObjectsCuentas(DataTable dtObject)
        {
            try
            {
                DevExpress.Web.ASPxGridView gvCuentas = gvPaquete.FindDetailRowTemplateControl(Session["IndexGridDetail"].S().I(), "gvCuentas") as DevExpress.Web.ASPxGridView;
                if (gvCuentas != null)
                {
                    gvCuentas.DataSource = dtObject;
                    ViewState["oDatosDetalle"] = dtObject;
                    gvCuentas.DataBind();
                }
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
                gvPaquete.JSProperties["cpText"] = sMensaje;
                gvPaquete.JSProperties["cpShowPopup"] = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void MostrarMensajeCuenta(string sMensaje, string sCaption)
        {
            try
            {
                DevExpress.Web.ASPxGridView gvCuentas = gvPaquete.FindDetailRowTemplateControl(Session["IndexGridDetail"].S().I(), "gvCuentas") as DevExpress.Web.ASPxGridView;
                popup.HeaderText = sCaption;
                gvCuentas.JSProperties["cpText"] = sMensaje;
                gvCuentas.JSProperties["cpShowPopup"] = true;
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
                gvPaquete.CancelEdit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void gvPaquete_CancelRowEditing(object sender, ASPxStartRowEditingEventArgs e)
        {
            try
            {
                gvPaquete.Columns["Status"].Visible = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        protected void CancelEditingCue(CancelEventArgs e)
        {
            try
            {
                DevExpress.Web.ASPxGridView gvCuentas = gvPaquete.FindDetailRowTemplateControl(Session["IndexGridDetail"].S().I(), "gvCuentas") as DevExpress.Web.ASPxGridView;
                e.Cancel = true;
                gvCuentas.CancelEdit();
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

        private const string sClase = "frmPaquete.aspx.cs";
        private const string sPagina = "frmPaquete.aspx";

        Paquete_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eNewObjCue;
        public event EventHandler eObjSelectedCue;
        public event EventHandler eSaveObjCue;
        public event EventHandler eDeleteObjCue;
        public event EventHandler eSearchObjCue;
        public event EventHandler eGetCuentas;
        public event EventHandler eGetProyectoSAP;

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

        public int iIdTipoPaquete
        {
            get { return ViewState["IdPaquete"].S().I(); }
            set { ViewState["IdPaquete"] = value; }
        }
        public object[] oArrFiltros
        {
            get
            {
                string  sDescripcion = string.Empty;
                int iEstatus = -1;

                switch (ddlTipoBusqueda.SelectedValue.S())
                {
                    case "1":
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
                                        "@Descripcion", "%" +  sDescripcion + "%",
                                        "@estatus", iEstatus
                                    };
            }
        }

        public DataTable dtCuentas
        {
            get { return (DataTable)ViewState["dtCuentas"]; }
            set { ViewState["dtCuentas"] = value; }
        }

        public DataTable dtProyectoSAP
        {
            get { return (DataTable)ViewState["VSdtProyectoSAP"]; }
            set { ViewState["VSdtProyectoSAP"] = value; }
        }
        public object[] oArrFiltrosCue
        {
            get
            {
                int iIdTipoPaquete= Session["IdTipoPaquete"].S().I();
                string sDescripcion = string.Empty;
                int iEstatus = -1;

                return new object[]{ 
                                        "@IdTipoPaquete", iIdTipoPaquete,
                                        "@Descripcion", sDescripcion,
                                        "@estatus", iEstatus
                                    };
            }
        }

        #endregion

       
    }
}