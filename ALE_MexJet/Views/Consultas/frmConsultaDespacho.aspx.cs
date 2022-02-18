using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ALE_MexJet.Interfaces;
using ALE_MexJet.DomainModel;
using ALE_MexJet.Presenter;
using DevExpress.Web;
using System.Reflection;
using System.Data;
using DevExpress.XtraPrinting;
using DevExpress.Export;
using ALE_MexJet.Clases;
using NucleoBase.Core;
using System.Drawing;

namespace ALE_MexJet.Views.Consultas
{
    public partial class frmConsultaDespacho : System.Web.UI.Page, IViewConsultaDespacho
    {
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                oPresenter = new ConsultaDespacho_Presenter(this, new DBConsultaDespacho());
                cuClienteContrato.miEventoCliente += mpeComboClienteContrato_miEventoCliente;
                cuClienteContrato.miEventoContrato += mpeComboClienteContrato_miEventoContrato;
                gvConsultaDespacho.SettingsPager.Position = PagerPosition.TopAndBottom;
                gvConsultaDespacho.SettingsPager.ShowDisabledButtons = true;
                gvConsultaDespacho.SettingsPager.ShowNumericButtons = true;
                gvConsultaDespacho.SettingsPager.ShowSeparators = true;
                gvConsultaDespacho.SettingsPager.Summary.Visible = true;
                gvConsultaDespacho.SettingsPager.PageSizeItemSettings.Visible = true;
                gvConsultaDespacho.SettingsPager.PageSizeItemSettings.Position = PagerPageSizePosition.Right;
                gvConsultaDespacho.SettingsText.SearchPanelEditorNullText = "Ingrese la información a buscar";

                if (!IsPostBack)
                {
                    iStatus = -1;
                    if (cmdEstatus.SelectedIndex != -1)
                        iStatus = Convert.ToInt32(cmdEstatus.Value);
                    sClienteCombo = null;
                    sContratoCombo = null;
                }
                ObtieneClientes();                        
                CargaConsultaDespacho();
                RecargaSubGrid();
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "Page_Load", "Aviso");
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
                CargaConsultaDespacho();
                //cuClienteContrato.LimpiarComboCliente();
                //cuClienteContrato.LimpiarComboContrato();
                //cmdEstatus.SelectedIndex = -1;
                //cmdEstatus.SelectedItem = null;
                //deFechaFinal.Value = null;
                //deFechaFinal.Text = "";
                //deFechaInicial.Value = null;
                //deFechaFinal.Text = "";
                ////sContratoCombo = null;
                ////sClienteCombo = null;
                
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnBuscar_Click", "Aviso");
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
        protected void gvPiernas_BeforePerformDataSelect(object sender, EventArgs e)
        {
            try
            {
                ASPxGridView grid = sender as ASPxGridView;

                iIdSolicitud = grid.GetMasterRowKeyValue().S().I();


            }
            catch (Exception ex) { Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvPiernas_BeforePerformDataSelect", "Aviso"); }
        }
        protected void gvConsultaDespacho_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
        {
            try
            {
                if (e.CommandArgs.CommandName.S() == "Editar")
                {
                    iIdSolicitud = e.CommandArgs.CommandArgument.S().I();
                    if (eBuscaSubGrid != null)
                        eBuscaSubGrid(null, EventArgs.Empty);

                    if (eConsultaSol != null)
                        eConsultaSol(null, null);

                    ppDictamen.ShowOnPageLoad = true;
                }
            }
            catch (Exception ex) { Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvConsultaDespacho_RowCommand", "Aviso"); }
        }
        protected void btnGuardarD_Click(object sender, EventArgs e)
        {
            try
            {
                if (eInsertaDictamen != null)
                    eInsertaDictamen(null, null);

                new DBMonitorTrafico().DBInsertaMonitorTrafico(iIdSolicitud);
                new DBMonitorAtencionClientes().DBEditaAtnCliente(iIdSolicitud);
                CargaConsultaDespacho();

            }
            catch (Exception x) { Utils.SaveErrorEnBitacora(mpeMensaje, x.Message, sPagina, sClase, "btnGuardarD_Click", "Aviso"); }
        }
       
        #endregion Eventos

        #region Metodos
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
        public void LoadConsultaDespacho(DataTable dtConsultaDespacho)
        {
            try
            {
                gvConsultaDespacho.DataSource = null;
                gvConsultaDespacho.DataSource = dtConsultaDespacho;
                gvConsultaDespacho.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void CargaConsultaDespacho()
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


                txtContacto.ReadOnly = true;
                txtMotivo.ReadOnly = true;
                txtTipoEquipo.ReadOnly = true;
                txtMatricula.ReadOnly = true;
                mNotasSolicitud.ReadOnly = true;
            }
            catch (Exception x) { throw x; }
        }
        #endregion Metodos

        #region Vars y Props
        ConsultaDespacho_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eGetClientes;
        public event EventHandler eGetContrato;
        public event EventHandler eBuscaSubGrid;
        public event EventHandler eInsertaDictamen;
        public event EventHandler eConsultaSol;

        protected static string sClienteCombo;
        protected static string sContratoCombo;
        protected static int iStatus = -1;

        private const string sClase = "frmConsultaDespacho.aspx.cs";
        private const string sPagina = "frmConsultaDespacho.aspx";

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
                //int iStatus = -1;
               
                if (cmdEstatus.SelectedIndex != -1)
                    iStatus = Convert.ToInt32(cmdEstatus.Value);

                return new object[] {
                    "@pcCliente",       sClienteCombo,
                    "@pcContrato",	    sContratoCombo,
	                "@piStatus",	    iStatus,
	                "@pdFechaInicial",  deFechaInicial.Value,
	                "@pdFechaFinal",    deFechaFinal.Value	
                };
            }
        }
        public object[] oArrInsertDictamen
        {
            get
            {
                return new object[] 
                {
                    "@IdSolicitud", iIdSolicitud,
                    "@Dictamen", rblDictamen.Value,
	                "@OrigenSolicitud", "Mex Jet 360",
	                "@Observaciones", mObservaiones.Text,
	                "@UsuarioCreacion", Utils.GetUser,
	                "@IP", ""
                };
            }
        }
        
        #endregion Vars y Props                        

    }
}