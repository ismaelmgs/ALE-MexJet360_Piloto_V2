using ALE_MexJet.Clases;
using ALE_MexJet.Interfaces;
using ALE_MexJet.Objetos;
using DevExpress.Export;
using DevExpress.Web;
using DevExpress.Web.Data;
using DevExpress.XtraPrinting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NucleoBase.Core;
using System.Data;
using System.Reflection;
using ALE_MexJet.Presenter;
using ALE_MexJet.DomainModel;

namespace ALE_MexJet.Views.FBO
{
    public partial class frmConsultaTrafico : System.Web.UI.Page, IViewConsultaTrafico
    {
        #region Events
        
        protected void Page_Load(object sender, EventArgs e)
        {
            oPresenter = new ConsultaTrafico_Presenter(this, new DBConsultaTrafico());
            cuClienteContrato.miEventoCliente += mpeComboClienteContrato_miEventoCliente;
            cuClienteContrato.miEventoContrato += mpeComboClienteContrato_miEventoContrato;
            gvConsultaTrafico.SettingsPager.Position = PagerPosition.TopAndBottom;
            gvConsultaTrafico.SettingsPager.ShowDisabledButtons = true;
            gvConsultaTrafico.SettingsPager.ShowNumericButtons = true;
            gvConsultaTrafico.SettingsPager.ShowSeparators = true;
            gvConsultaTrafico.SettingsPager.Summary.Visible = true;
            gvConsultaTrafico.SettingsPager.PageSizeItemSettings.Visible = true;
            gvConsultaTrafico.SettingsPager.PageSizeItemSettings.Position = PagerPageSizePosition.Right;
            gvConsultaTrafico.SettingsText.SearchPanelEditorNullText = "Ingrese la información a buscar";

            ObtieneClientes();

            /*
            if (!IsPostBack)
            {
                sClienteCombo = null;
                sContratoCombo = null;
                idNoSolicitud = -1;
            }
            */
            CargaConsultaTrafico();

            if(ppEditar.ShowOnPageLoad == true)
                ObtieneTrips();            
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                Exporter.WriteXlsToResponse(new XlsExportOptionsEx() { ExportType = ExportType.DataAware });
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnExcel_Click", "Aviso");
            }
        }
        protected void gvTrip_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            try
            {
                e.Editor.ReadOnly = false;

                ASPxGridView gridView = (ASPxGridView)sender;

                if (gridView.IsNewRowEditing)
                {
                    eCrud = Enumeraciones.TipoOperacion.Insertar;
                    //gvTrip.Columns["Status"].Visible = false;

                }
                else
                {
                    eCrud = Enumeraciones.TipoOperacion.Actualizar;
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvTrip_CellEditorInitialize", "Aviso");
            }
        }
        protected void gvTrip_RowDeleting(object sender, ASPxDataDeletingEventArgs e)
        {
            try
            {
                eCrud = Enumeraciones.TipoOperacion.Eliminar;
                oCrud = e;

                if (eEliminaTripSolicitud != null)
                    eEliminaTripSolicitud(sender, e);

                CancelEditing(e);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvTrip_RowDeleting", "Aviso");
            }
        }
        protected void gvTrip_RowInserting(object sender, ASPxDataInsertingEventArgs e)
        {
            try
            {
                eCrud = Enumeraciones.TipoOperacion.Insertar;

                oCrud = e;

                if (eNewObj != null)
                    eNewObj(sender, e);                                

                e.NewValues["IdSolicitud"] = iIdSolicitud;
                e.NewValues["UserIdentity"] = ((UserIdentity)Session["UserIdentity"]).sUsuario.S();

                if (eNewTrip != null)
                    eNewTrip(sender, e);             

                CancelEditing(e);
                
                CargaConsultaTrafico();
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvTrip_RowInserting", "Aviso");
            }
        }
        protected void gvTrip_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
        {
            try
            {
                eCrud = Enumeraciones.TipoOperacion.Actualizar;
                oCrud = e;

                e.NewValues["UserIdentity"] = ((UserIdentity)Session["UserIdentity"]).sUsuario.S();

                if (eSaveTrip != null)
                    eSaveTrip(sender, e);

                CargaConsultaTrafico();

                CancelEditing(e);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvTrip_RowUpdating", "Aviso");
            }
        }
        protected void gvTrip_StartRowEditing(object sender, ASPxStartRowEditingEventArgs e)
        {
            try
            {
                gvTrip.SettingsText.PopupEditFormCaption = "Formulario de Edición";
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvTrip_StartRowEditing", "Aviso");
            }
        }
        protected void gvTrip_RowValidating(object sender, ASPxDataValidationEventArgs e)
        {
            try
            {
                string x = e.NewValues["Trip"].S();
                if (eValidaTrip != null)
                    eValidaTrip(x, e);

                if (Session["Validacion"] != null && Session["Validacion"].S().I() > 0)
                {
                    AddError(e.Errors, gvTrip.Columns["TRIP"], "Este Trip ya existe, favor de validarlo.");
                    Session["Validacion"] = null;
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvTrip_RowValidating", "Aviso");
            }
        }
        protected void CancelEditing(CancelEventArgs e)
        {
            try
            {
                e.Cancel = true;
                gvTrip.CancelEdit();
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "CancelEditing", "Aviso");
            }
        }
        protected void gvPiernas_BeforePerformDataSelect(object sender, EventArgs e)
        {
            try
            {
                ASPxGridView gridDetalle = sender as ASPxGridView;

                iIdSolicitud = gridDetalle.GetMasterRowKeyValue().S().I();

                if (eSearchDetalle != null)
                    eSearchDetalle(sender, EventArgs.Empty);

                gridDetalle.DataSource = (DataTable)ViewState["CargaDetalle"];
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvPiernas_BeforePerformDataSelect", "Aviso");
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
        private void mpeComboClienteContrato_miEventoContrato(object sender, EventArgs e)
        {
            try
            {
                ASPxComboBox cmb = (ASPxComboBox)sender;
                if (cmb.SelectedIndex != -1)
                    sContratoCombo = cmb.SelectedItem.Text;
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "mpeComboClienteContrato_miEventoContrato", "Aviso");
            }
        }
        private void mpeComboClienteContrato_miEventoCliente(object sender, EventArgs e)
        {
            try
            {
                ASPxComboBox cmb = (ASPxComboBox)sender;
                if (cmb.SelectedIndex != -1)
                {
                    sClienteCombo = cmb.SelectedItem.Text;
                    ObtieneContrato();
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "mpeComboClienteContrato_miEventoCliente", "Aviso");
            }
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                CargaConsultaTrafico();
                /*cuClienteContrato.LimpiarComboCliente();
                cuClienteContrato.LimpiarComboContrato();
                deFechaFinal.Value = null;
                deFechaFinal.Text = "";
                deFechaInicial.Value = null;
                deFechaFinal.Text = "";
                txtNoSolicitud.Text = "";*/
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnBuscar_Click", "Aviso");
            }
        }

        protected void gvConsultaTrafico_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
        {
            try
            {
                if (e.CommandArgs.CommandName.S() == "Edicion")
                {
                    iIdSolicitud = e.CommandArgs.CommandArgument.S().I();
                    if (eBuscaSubGrid != null)
                        eBuscaSubGrid(null, EventArgs.Empty);

                    if (eConsultaSol != null)
                        eConsultaSol(sender, EventArgs.Empty);

                    ObtieneTrips();

                    ppEditar.ShowOnPageLoad = true;
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvConsultaTrafico_RowCommand", "Aviso");
            }
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                gvTrip.SettingsText.PopupEditFormCaption = "Formulario de Creación";
                gvTrip.AddNewRow();
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnNuevo_Click", "Aviso");
            }
        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            CargaConsultaTrafico();
        }

        #endregion Events

        #region Methods

        void AddError(Dictionary<GridViewColumn, string> errors, GridViewColumn column, string errorText)
        {
            try
            {
                if (errors.ContainsKey(column)) return;
                errors[column] = errorText;
            }
            catch (Exception x)
            {
                throw x;
            }
        }
        public void ObtieneClientes()
        {
            try
            {
                if (eGetClientes != null)
                    eGetClientes(null, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ObtieneContrato()
        {
            try
            {
                if (eGetContrato != null)
                    eGetContrato(null, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LoadClientes(DataTable dtObjCat)
        {
            try
            {
                cuClienteContrato.llenarComboCliente(dtObjCat);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LoadContrato(DataTable dtObjCat)
        {
            try
            {
                cuClienteContrato.llenarComboContrato(dtObjCat);
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
                mpeMensaje.ShowMessage(sMensaje, sCaption);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LlenaGrid(DataTable dtObjCat)
        {
            gvConsultaTrafico.DataSource = null;
            gvConsultaTrafico.DataSource = dtObjCat;
            gvConsultaTrafico.DataBind();
            dtObjCat = null;
        }
        public void CargaConsultaTrafico()
        {
            try
            {
                if (eSearchObj != null)
                    eSearchObj(null, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LoadSol(DataTable DT)
        {
            try
            {
                txtContacto.Text = "";
                txtMotivo.Text = "";
                txtTipoEquipo.Text = "";
                txtMatricula.Text = "";
                mNotasSolicitud.Text = "";
                lblSolicitud.Text = "";

                txtContacto.Text = DT.Rows[0]["Contacto"].S();
                txtMotivo.Text = DT.Rows[0]["DescripcionMotivo"].S();
                txtTipoEquipo.Text = DT.Rows[0]["Equipo"].S();
                txtMatricula.Text = DT.Rows[0]["Matricula"].S();
                mNotasSolicitud.Text = DT.Rows[0]["Notas"].S();
                lblSolicitud.Text = iIdSolicitud.S();
            }
            catch (Exception ex) { throw ex; }
        }

        public void LoadPiernas(DataTable dtObjCat)
        {
            try
            {
                gvPiernaDic.DataSource = dtObjCat;
                gvPiernaDic.DataBind();
                ViewState["LlenaSubGrid"] = dtObjCat;
            }
            catch (Exception x) { throw x; }
        }
        protected void RecargaSubGrid()
        {
            try
            {
                if (ViewState["LlenaSubGrid"] != null)
                {
                    gvPiernaDic.DataSource = (DataTable)ViewState["LlenaSubGrid"];
                    gvPiernaDic.DataBind();

                }
            }
            catch (Exception x) { throw x; }
        }

        #endregion Methods

        #region Vars and Properties

        ConsultaTrafico_Presenter oPresenter;
        private const string sClase = "frmConsultaTrafico.aspx.cs";
        private const string sPagina = "frmConsultaTrafico.aspx";
        protected static string sClienteCombo;
        protected static string sContratoCombo;
        protected static int idNoSolicitud = -1;

        public event EventHandler eNewTrip;
        public event EventHandler eSaveTrip;
        public event EventHandler eLoadObjTrips;
        public event EventHandler eEliminaTripSolicitud;
        public event EventHandler eValidaTrip;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eSearchDetalle;
        public event EventHandler eGetClientes;
        public event EventHandler eGetContrato;
        public event EventHandler eConsultaSol;
        public event EventHandler eBuscaSubGrid;
        

        public object[] oArrFiltroContrato
        {
            get { return new object[] { "@NombreCliente", null, "@CodigoCliente", sClienteCombo }; }
        }
        public object[] oArrFiltroClientes
        {
            get { return new object[] { "@idCliente", 0 }; }
        }
        public object[] oArrFiltros
        {
            get
            {

                if (txtNoSolicitud.Text.Length > 0)                
                    idNoSolicitud = txtNoSolicitud.Text.I();                
                else
                    idNoSolicitud = -1;

                if (sClienteCombo == "Todos")
                {
                    sClienteCombo = null;
                    sContratoCombo = null;
                }
                
                
                return new object[] {
                    "@Cliente",       sClienteCombo,
                    "@Contrato",	  sContratoCombo,
	                "@IdSolicitud",   idNoSolicitud,
	                "@FechaInicial",  deFechaInicial.Value,
	                "@FechaFinal",    deFechaFinal.Value	
                };
            }
        }

        public Objetos.Enumeraciones.TipoOperacion eCrud
        {
            get { return (ALE_MexJet.Objetos.Enumeraciones.TipoOperacion)ViewState["eCrud"]; }
            set { ViewState["eCrud"] = value; }
        }

        public object oCrud
        {
            get { return ViewState["CrudTrip"]; }
            set { ViewState["CrudTrip"] = value; }
        }

        public void ObtieneTrips()
        {
            try
            {
                if (eLoadObjTrips != null)
                    eLoadObjTrips(null, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LoadObjectsTrips(DataTable dtObjCat)
        {
            try
            {
                gvTrip.DataSource = dtObjCat;
                gvTrip.DataBind();
                ViewState["gvTrip"] = dtObjCat;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int iIdSolicitud
        {
            get { return ViewState["idSolicitud"].S().I(); }
            set { ViewState["idSolicitud"] = value; }
        }

        public object[] oArrFilSolicitud
        {
            get
            {
                return new object[] { 
                                        "@IdSolicitud", iIdSolicitud
                                    };
            }
        }

        #endregion Vars and Properties

        
              
        
    }
}