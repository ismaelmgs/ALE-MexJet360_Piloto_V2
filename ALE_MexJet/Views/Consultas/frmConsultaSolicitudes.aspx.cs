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
using ALE_MexJet.Clases;
using DevExpress.Utils;
using System.Reflection;
using System.Collections.Specialized;
using System.Text;
using System.IO;


namespace ALE_MexJet.Views.Consultas
{
    public partial class frmConsultaSolicitudes : System.Web.UI.Page, IViewConsultaSolicitudes
    {
        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            try 
            {
                oPresenter = new ConsultaSolicitudesPresenter(this, new DBConsultaSolicitudes());

                gvConsultaSolicitudes.SettingsPager.Position = PagerPosition.TopAndBottom;
                gvConsultaSolicitudes.SettingsPager.ShowDisabledButtons = true;
                gvConsultaSolicitudes.SettingsPager.ShowNumericButtons = true;
                gvConsultaSolicitudes.SettingsPager.ShowSeparators = true;
                gvConsultaSolicitudes.SettingsPager.Summary.Visible = true;
                gvConsultaSolicitudes.SettingsPager.PageSizeItemSettings.Visible = true;
                gvConsultaSolicitudes.SettingsPager.PageSizeItemSettings.Position = PagerPageSizePosition.Right;
                gvConsultaSolicitudes.SettingsText.SearchPanelEditorNullText = "Ingrese la información a buscar";

                if (eGetObjects != null)
                    eGetObjects(sender, e);

                if (eSearchObj != null)
                    eSearchObj(sender, e);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "Page_Load", "Aviso");
            }
        }

        protected void btnBusqueda_Click(object sender, System.EventArgs e)
        {
           try
           {
                hftxtClaveCliente["hfddlClaveCliente"] = ddlClaveCliente.Text;
                hftxtContrato["hftxtContrato"] = ddlContrato.Text;
                hftxtTrip["hftxtTrip"] = txtTrip.Value;
                hftxtUsuario["hftxtUsuario"] = txtUsuario.Text;
                hftxtEstatus["hftxtEstatus"] = ddlEstatusSolicitud.Value.S().I();
                hfcFechaIni["hfcFechaIni"] = cFechaIni.Text == null ? string.Empty : cFechaIni.Text.S();
                hfcFechaFin["hfcFechaFin"] = cFechaFin.Text == null ? string.Empty : cFechaFin.Text.S();
                hfvFechaIni["hfvFechaIni"] = vFechaIni.Text == null ? string.Empty : vFechaIni.Text.S();
                hfvFechaFin["hfvFechaIni"] = vFechaFin.Text == null ? string.Empty : vFechaFin.Text.S();

                if (eObjConsultaSolicitudes != null)
                    eObjConsultaSolicitudes(null, null);
                ddlEstatusSolicitud.Value = 0;

            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnBusqueda_Click", "Aviso");
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                Exporter.WriteXlsToResponse(new XlsExportOptionsEx() { ExportType = ExportType.DataAware });
                //ExportExcel();
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnExportar_Click", "Aviso");
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

        #endregion

        #region "METODOS"

        public void LoadEstatusSolicitud(DataTable dtObjCat)
        {
            try
            {
                ViewState["oEstatusSolicitud"] = dtObjCat;
                ddlEstatusSolicitud.DataSource = dtObjCat;
                ddlEstatusSolicitud.TextField = "Descripcion";
                ddlEstatusSolicitud.ValueField = "IdEstatus";
                ddlEstatusSolicitud.DataBind();
                ddlEstatusSolicitud.Value = 0;
            }
            catch (Exception x) { throw x; }
        }

        public void LoadCliente(DataTable dtObjCat)
        {
            try
            {
                ViewState["oCliente"] = dtObjCat;
                ddlClaveCliente.DataSource = dtObjCat;
                ddlClaveCliente.TextField = "CodigoCliente";
                ddlClaveCliente.ValueField = "IdCliente";
                ddlClaveCliente.DataBind();
                ddlClaveCliente.Value = null;

            }
            catch (Exception x) { throw x; }
        }

        public void LoadContrato(DataTable dtObjCat)
        {
            try
            {
                ViewState["oContrato"] = dtObjCat;
                ddlContrato.DataSource = dtObjCat;
                ddlContrato.TextField = "ClaveContrato";
                ddlContrato.ValueField = "IdContrato";
                ddlContrato.DataBind();
                ddlContrato.Value = null;

            }
            catch (Exception x) { throw x; }
        }

        public void MostrarMensaje(string sMensaje, string sCaption)
        {
            try
            {
                mpeMensaje.ShowMessage(sMensaje, sCaption);
            }
            catch (Exception x) { throw x; }
        }
        public void LoadConsultaSolicitudes(DataTable dtObjCat)
        {
             ViewState["oConsultaSolicitudes"] = dtObjCat;
             gvConsultaSolicitudes.DataSource = dtObjCat;
             gvConsultaSolicitudes.DataBind();
        }

        protected void LLenaGrid(DataTable dtObjGrid)
        {
            gvConsultaSolicitudes.DataSource = dtObjGrid;
            ViewState["oConsultaSolicitudes"] = dtObjGrid;
            gvConsultaSolicitudes.DataBind();
        }
        public void LoadActions(DataRow[] DrActions)
        {
            int iPos = 0;
            if (DrActions.Length == 0)
            {
                btnBusqueda.Enabled = false;
                ddlClaveCliente.Enabled = false;
                ddlContrato.Enabled = false;
                txtTrip.Enabled = false;
                cFechaIni.Enabled = false;
                cFechaFin.Enabled = false;
                btnExcel.Enabled = false;
                txtUsuario.Enabled = false;
                vFechaIni.Enabled = false;
                vFechaFin.Enabled = false;
                ddlEstatusSolicitud.Enabled = false;
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
                                ddlClaveCliente.Enabled = true;
                                ddlContrato.Enabled = true;
                                txtTrip.Enabled = true;
                                cFechaIni.Enabled = true;
                                cFechaFin.Enabled = true;
                                btnExcel.Enabled = true;
                                txtUsuario.Enabled = true;
                                vFechaIni.Enabled = true;
                                vFechaFin.Enabled = true;
                                ddlEstatusSolicitud.Enabled = true;
                            }
                            else
                            {
                                btnBusqueda.Enabled = false;
                                ddlClaveCliente.Enabled = false;
                                ddlContrato.Enabled = false;
                                txtTrip.Enabled = false;
                                cFechaIni.Enabled = false;
                                cFechaFin.Enabled = false;
                                btnExcel.Enabled = false;
                                txtUsuario.Enabled = false;
                                vFechaIni.Enabled = false;
                                vFechaFin.Enabled = false;
                                ddlEstatusSolicitud.Enabled = false;
                            } break;
                    }
                }
            }

        }
        #endregion

        #region "Vars y Propiedades"
        private const string sClase = "frmConsultaSolicitudes.aspx.cs";
        private const string sPagina = "frmConsultaSolicitudes.aspx";

        ConsultaSolicitudesPresenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eGetObjects;
        public event EventHandler eObjConsultaSolicitudes;
        public event EventHandler eGetCliente;
        public event EventHandler eGetContratos;
        UserIdentity oUsuario = new UserIdentity();
        public DataRow[] DrPermisos
        {
            get { return (DataRow[])Session["DrPermisos"]; }
            set { Session["DrPermisos"] = value; }
        }
        
        public ConsultaSolicitudes oConsultaSolicitudesConsultaSolicitudes
        {
            get 
            {
                ConsultaSolicitudes B = new ConsultaSolicitudes();
                DateTime dHoy = DateTime.Today;
                B.ClaveCliente = ddlClaveCliente.Text;
                B.ClaveContrato = ddlContrato.Text;
                B.IdTrip = txtTrip.Text.S().I();
                B.UsuarioCreacion = txtUsuario.Text;
                B.IdEstatus = ddlEstatusSolicitud.Value.S().I();
                B.FechaCreacionIni = cFechaIni.Date.ToString("dd/MM//yyyy");
                B.FechaCreacionFin = cFechaFin.Date.ToString("dd/MM/yyyy");
                B.FechaVueloIni = vFechaIni.Date.ToString("dd/MM/yyyy");
                B.FechaVueloFin = vFechaFin.Date.ToString("dd/MM/yyyy");

                if (cFechaIni.Text != string.Empty && cFechaFin.Text == string.Empty)
                {
                    B.FechaCreacionIni = cFechaIni.Date.ToString("dd/MM/yyyy");
                    B.FechaCreacionFin = dHoy.ToString("dd/MM/yyyy");
                }
                else if (cFechaIni.Text == string.Empty && cFechaFin.Text != string.Empty)
                {
                    B.FechaCreacionIni = dHoy.ToString("dd/MM/yyyy");
                    B.FechaCreacionFin = cFechaFin.Date.ToString("dd/MM/yyyy");
                }
                else
                {
                    B.FechaCreacionIni = cFechaIni.Text == null ? string.Empty : cFechaIni.Date.ToString("dd/MM/yyyy"); ;
                    B.FechaCreacionFin = cFechaFin.Text == null ? string.Empty : cFechaFin.Date.ToString("dd/MM/yyyy"); ;
                }
                
                return B;
           }
        }

        protected void ddlClaveCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (eGetContratos != null)
                    eGetContratos(sender, e);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "ddlClaveCliente_SelectedIndexChanged", "Aviso");
            }
        }

        public Bitacora oBitacoraContrato
        {
            get
            {
                Bitacora B = new Bitacora();
                B.IdCliente = ddlClaveCliente.SelectedItem.Value.S();
                return B;
            }
        }

        #endregion

        

        

    }
}