using ALE_MexJet.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ALE_MexJet.Interfaces;
using ALE_MexJet.DomainModel;
using ALE_MexJet.Presenter;
using System.Data;
using DevExpress.Web;
using NucleoBase.Core;
using DevExpress.XtraPrinting;
using DevExpress.Export;
using ALE_MexJet.Objetos;

namespace ALE_MexJet.Views.FBO
{
    public partial class frmMonitorAtencionClientes : System.Web.UI.Page, IViewMonitorAtencionClientes
    {

        #region eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                oPresenter = new MonitorAtencionClientes_Presenter(this, new DBMonitorAtencionClientes());

                gvMonitorAtencionClientes.SettingsPager.Position = PagerPosition.TopAndBottom;
                gvMonitorAtencionClientes.SettingsPager.ShowDisabledButtons = true;
                gvMonitorAtencionClientes.SettingsPager.ShowNumericButtons = true;
                gvMonitorAtencionClientes.SettingsPager.ShowSeparators = true;
                gvMonitorAtencionClientes.SettingsPager.Summary.Visible = true;
                gvMonitorAtencionClientes.SettingsPager.PageSizeItemSettings.Visible = true;
                gvMonitorAtencionClientes.SettingsPager.PageSizeItemSettings.Position = PagerPageSizePosition.Right;
                gvMonitorAtencionClientes.SettingsText.SearchPanelEditorNullText = "Ingrese la información a buscar";

                if (!IsPostBack)
                {
                    if (eSearchAropuertoBase != null)
                        eSearchAropuertoBase(sender, EventArgs.Empty);
                }

                loadConsultaMonitorAtencionClientes();
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "Page_Load", "Aviso");
            }
        }
        protected void UpdateTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (eSearchObj != null)
                    eSearchObj(sender, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "UpdateTimer_Tick", "Aviso");
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
        protected void gvMonitorAtencionClientes_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
        {
            try
            {
                if (e.CommandArgs.CommandName.S() == "Viabilidad")
                {
                    iIdSolicitud = e.CommandArgs.CommandArgument.S().I();
                    if (eBuscaNotas != null)
                        eBuscaNotas(sender, EventArgs.Empty);
                    ppViabilidad.ShowOnPageLoad = true;
                }
                else if (e.CommandArgs.CommandName.S() == "NotasTrafico")
                {
                    iIdSolicitud = e.CommandArgs.CommandArgument.S().I();
                    ppNotasTrafico.ShowOnPageLoad = true;
                }
                else if (e.CommandArgs.CommandName.S() == "VistoBueno")
                {
                    iIdSolicitud = e.CommandArgs.CommandArgument.S().I();

                    DataTable dt = (DataTable)gvMonitorAtencionClientes.DataSource;
                    DataRow[] rows = dt.Select("IdSolicitud = " + iIdSolicitud);
                    if (rows.Length > 0)
                    {
                        if (rows[0]["EstatusSolicitud"].S() != "CANCELADO")
                        {
                            int iAux = 0;
                            foreach (DataRow row in dt.Rows)
                            {
                                if (iIdSolicitud == Convert.ToInt32(row[2].S()) && (row["TraficoEstatus"].S().Equals("En proceso") || row["DespachoEstatus"].S().Equals("Sin Viabilidad")))
                                {
                                    iAux = 2;
                                    break;
                                }
                            }

                            if (iAux == 2)
                            {
                                lbl.Text = "Favor de validar el estatus de los monitores.";
                                popup.ShowOnPageLoad = true;
                                return; // no va a pasar hasta que tenga viabilidad por parte de despacho
                            }
                        }
                    }
                     

                    ppVistoBueno.ShowOnPageLoad = true;
                }
                else if (e.CommandArgs.CommandName.S() == "Rechazo")
                {
                    int indx = e.VisibleIndex;
                    DataRow RW = gvMonitorAtencionClientes.GetDataRow(indx);
                    iIdSolicitud = RW[2].S().I();
                    
                    mRechazo.Text = "";
                    iIdMonitor = e.CommandArgs.CommandArgument.S().I();
                    ppConfirmacion.ShowOnPageLoad = true;
                    // 
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvMonitorAtencionClientes_RowCommand", "Aviso");
            }
        }
        protected void gvPiernas_BeforePerformDataSelect(object sender, EventArgs e)
        {
            try
            {
                ASPxGridView gridDetalle = sender as ASPxGridView;
                iIdSolicitud = gridDetalle.GetMasterRowKeyValue().S().I();
                loadConsultaMonitorAtencionClientesDetalle();
                gridDetalle.DataSource = (DataTable)ViewState["CargaDetalle"];
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvPiernas_BeforePerformDataSelect", "Aviso");
            }
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if (eSearchObj != null)
                    eSearchObj(sender, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnBuscar_Click", "Aviso");
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
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnExcel_Click", "Aviso");
            }
        }
        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                if (eActualizaStatus != null)
                    eActualizaStatus(sender, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnConfirmar_Click", "Aviso");
            }
        }
        protected void btnConfirmarpp_Click(object sender, EventArgs e)
        {
            try
            {
                if (eEditaAtnCliente != null)
                    eEditaAtnCliente(null, null);
                
                new DBMonitorTrafico().DBInsertaMonitorTrafico(iIdSolicitud);
                loadConsultaMonitorAtencionClientes();

                ppConfirmacion.ShowOnPageLoad = false;
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnConfirmarpp_Click", "Aviso");
            }
        }
        #endregion eventos

        #region metodos

        public void CargaConsultaMonitorAtencionClientes(DataTable dtObjCat)
        {
            try
            {
                gvMonitorAtencionClientes.DataSource = null;
                gvMonitorAtencionClientes.DataSource = dtObjCat;
                gvMonitorAtencionClientes.DataBind();
                dtObjCat = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void loadConsultaMonitorAtencionClientes()
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

        public void CargaConsultaMonitorAtencionClientesDetalle(DataTable dtObjCat)
        {
            try
            {
                ViewState["CargaDetalle"] = null;
                ViewState["CargaDetalle"] = dtObjCat;
                dtObjCat = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void loadConsultaMonitorAtencionClientesDetalle()
        {
            try
            {
                if (eSearchDetalle != null)
                    eSearchDetalle(null, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CargaAeropuertoBase(DataTable dtAeropuertoBase)
        {
            try
            {
                ddlBase.DataSource = dtAeropuertoBase;
                ddlBase.TextField = "AeropuertoICAO";
                ddlBase.ValueField = "idAeropuert";
                ddlBase.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void CargaNotas(DataSet dsNotas)
        {
            try
            {
                DataTable dtPreferencia = new DataTable();
                dtPreferencia = dsNotas.Tables[2];
                mPreferencias.Text = dtPreferencia.Rows[0]["Notas"].ToString();

                mPreferencias.ForeColor = System.Drawing.Color.Black;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion metodos

        #region vars y props
        private const string sClase = "MonitorAtencionClientes.aspx.cs";
        private const string sPagina = "MonitorAtencionClientes.aspx";
        MonitorAtencionClientes_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eSearchDetalle;
        public event EventHandler eBuscaNotas;
        public event EventHandler eSearchAropuertoBase;
        public event EventHandler eActualizaStatus;
        public event EventHandler eEditaAtnCliente;

        public object[] oArrConsultaMonitorAtencionClientes
        {
            get
            {

                int iIdBase;

                if (ddlBase.SelectedIndex == -1)
                {
                    iIdBase = -1;
                }
                else
                {
                    iIdBase = Convert.ToInt32(ddlBase.SelectedItem.Value);
                }

                return new object[] { 
                    "@IdBase",iIdBase
                };
            }
        }
        public object[] oArrConsultaMonitorAtencionClientesDetalle
        {
            get
            {
                return new object[] { 
                    "@IdSolicitud",iIdSolicitud
                };
            }
        }
        public object[] oArrConsultaNotas
        {
            get
            {
                return new object[] { 
                    "@IdSolicitud",iIdSolicitud
                };
            }
        }
        public object[] oArrActualizaStatus
        {
            get
            {
                return new object[] { 
                    "@IdSolicitud",iIdSolicitud,
                    "@UsuarioModificacion",((UserIdentity)Session["UserIdentity"]).sUsuario.S()
                };
            }
        }
        public object[] oArrEditaAtncclientes 
        {
            get
            {
                return new object[] { 
                    "@IdMonitor",iIdMonitor,
                    "@Notas",mRechazo.Text,
                    "@Status" , 2,
                    "@Usuario", Utils.GetUser.S(),
                    "@IP", ""
                };
            }
        }        
        public Objetos.Enumeraciones.TipoOperacion eCrud
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public object oCrud
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public int iIdSolicitud
        {
            get { return ViewState["idSolicitud"].S().I(); }
            set { ViewState["idSolicitud"] = value; }
        }
        public int iIdMonitor
        {
            get { return ViewState["iIdMonitor"].S().I(); }
            set { ViewState["iIdMonitor"] = value; }
        }
        #endregion vars y props
                                             
    }
}