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
using System.Text;


using System.Collections.Specialized;

using System.IO;

namespace ALE_MexJet.Views.AtencionClientes
{
    public partial class frmDashboardAtnCliente : System.Web.UI.Page, IViewDashboardAtnCliente
    {

        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                oPresenter = new DashboarsAtnCliente_Presenter(this, new DBDashboardAtnCliente());
                gvVuelos.SettingsText.SearchPanelEditorNullText = "Ingrese la información a buscar";

                if (!IsPostBack)
                {
                    if (eSearchObj != null)
                        eSearchObj(null, null);
                }
                CargaVuelos();
                CargaSol();
                CargaCaso();
            }
            catch (Exception ex) { Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "Page_Load", "Aviso"); }
        }
        protected void Unnamed_Unload(object sender, EventArgs e)
        {
            MethodInfo methodInfo = typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Where(i => i.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel")).First();
            methodInfo.Invoke(ScriptManager.GetCurrent(Page),
                new object[] { sender as UpdatePanel });
        }
        protected void lbNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                GetStatusSol(1);
                if (eSolicitud != null)
                    eSolicitud(null, null);

                gvSolVuelo.Visible = true;
                gvCasos.Visible = false;

                ppDetalle.HeaderText = "Solicitudes Nuevas";
                ppDetalle.ShowOnPageLoad = true;
            }
            catch (Exception ex) { Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "lbNuevo_Click", "Aviso"); }
        }
        protected void hlTrabajando_Click(object sender, EventArgs e)
        {
            try
            {
                GetStatusSol(3);
                if (eSolicitud != null)
                    eSolicitud(null, null);

                gvSolVuelo.Visible = true;
                gvCasos.Visible = false;

                ppDetalle.HeaderText = "Solicitudes Trabajando";
                ppDetalle.ShowOnPageLoad = true;
            }
            catch (Exception ex) { Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "hlTrabajando_Click", "Aviso"); }

        }
        protected void hlSolEsp_Click(object sender, EventArgs e)
        {
            try
            {
                GetIdCaso(4);
                if (eCasos != null)
                    eCasos(null, null);

                gvSolVuelo.Visible = false;
                gvCasos.Visible = true;
                gvCasos.Columns["Minutos"].Visible = false;
                gvCasos.Columns["AreaDescripcion"].Visible = false;
                gvCasos.Columns["DescEspecial"].Visible = true;
                gvCasos.Columns["Otorgado"].Visible = true;

                ppDetalle.HeaderText = "Solicitudes especiales de la Semana";
                ppDetalle.ShowOnPageLoad = true;
            }
            catch (Exception ex) { Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "hlSolEsp_Click", "Aviso"); }

        }
        protected void hlQuejas_Click(object sender, EventArgs e)
        {
            try
            {
                GetIdCaso(3);
                if (eCasos != null)
                    eCasos(null, null);

                gvSolVuelo.Visible = false;
                gvCasos.Visible = true;

                gvCasos.Columns["Minutos"].Visible = false;
                gvCasos.Columns["AreaDescripcion"].Visible = true;
                gvCasos.Columns["DescEspecial"].Visible = false;
                gvCasos.Columns["Otorgado"].Visible = false;
                ppDetalle.HeaderText = "Quejas de la Semana";
                ppDetalle.ShowOnPageLoad = true;
            }
            catch (Exception ex) { Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "hlQuejas_Click", "Aviso"); }

        }
        protected void hlIncidencias_Click(object sender, EventArgs e)
        {
            try
            {
                GetIdCaso(2);
                if (eCasos != null)
                    eCasos(null, null);

                gvSolVuelo.Visible = false;
                gvCasos.Visible = true;
                gvCasos.Columns["Minutos"].Visible = false;
                gvCasos.Columns["AreaDescripcion"].Visible = true;
                gvCasos.Columns["DescEspecial"].Visible = false;
                gvCasos.Columns["Otorgado"].Visible = false;
                ppDetalle.HeaderText = "Incidencias de la Semana";
                ppDetalle.ShowOnPageLoad = true;
            }
            catch (Exception ex) { Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "hlIncidencias_Click", "Aviso"); }

        }
        protected void lbDemoras_Click(object sender, EventArgs e)
        {
            try
            {
                GetIdCaso(1);
                if (eCasos != null)
                    eCasos(null, null);

                gvSolVuelo.Visible = false;
                gvCasos.Visible = true;

                gvCasos.Columns["Minutos"].Visible = true;
                gvCasos.Columns["AreaDescripcion"].Visible = false;
                gvCasos.Columns["DescEspecial"].Visible = false;
                gvCasos.Columns["Otorgado"].Visible = false;
                ppDetalle.HeaderText = "Demora de la Semana";
                ppDetalle.ShowOnPageLoad = true;
            }
            catch (Exception ex) { Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "lbDemoras_Click", "Aviso"); }

        }
        protected void btnCerrar_Click(object sender, EventArgs e)
        {
            try
            {
                ppDetalle.ShowOnPageLoad = false;
            }
            catch (Exception ex) { Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnCerrar_Click", "Aviso"); }

        }
        protected void btnBusqueda_Click(object sender, EventArgs e)
        {
            try
            {
                if (eSearchObj != null)
                    eSearchObj(null, null);
            }
            catch (Exception ex) { Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnBusqueda_Click", "Aviso"); }

        }
        #endregion

        #region METODOS
        public void LoadObjects(DataSet dtObjCat)
        {
            try
            {
                if (dtObjCat != null)
                {
                    gvVuelos.DataSource = dtObjCat.Tables[0];
                    gvVuelos.DataBind();
                    ViewState["gvVuelos"] = dtObjCat.Tables[0];

                    DataTable DT = dtObjCat.Tables[1];
                    lbNuevo.Text = DT.Rows[0]["NUEVASOLICITUD"].S();
                    hlTrabajando.Text = DT.Rows[0]["TABAJANDO"].S();
                    hlSolEsp.Text = DT.Rows[0]["SOLICITUDESPECIAL"].S();
                    hlQuejas.Text = DT.Rows[0]["QUEJA"].S();
                    hlIncidencias.Text = DT.Rows[0]["INCIDENCIA"].S();
                    lbDemoras.Text = DT.Rows[0]["DEMORA"].S();
                }
            }
            catch (Exception z) { throw z; }
        }
        public void LoadSol(DataTable DT)
        {
            try
            {
                if (DT != null)
                {
                    gvSolVuelo.DataSource = DT;
                    gvSolVuelo.DataBind();
                    ViewState["gvSolVuelo"] = null;
                    ViewState["gvSolVuelo"] = DT;
                }
            }
            catch (Exception x) { throw x; }
        }
        protected void CargaVuelos()
        {
            try
            {
                DataTable DT = (DataTable)ViewState["gvVuelos"];
                if (DT != null && DT.Rows.Count > 0)
                {
                    gvVuelos.DataSource = DT;
                    gvVuelos.DataBind();
                }
            }
            catch (Exception x) { throw x; }
        }
        protected void CargaSol()
        {
            try
            {
                DataTable DT = (DataTable)ViewState["gvSolVuelo"];
                if (DT != null && DT.Rows.Count > 0)
                {
                    gvSolVuelo.DataSource = DT;
                    gvSolVuelo.DataBind();
                }
            }
            catch (Exception z) { throw z; }
        }
        protected void CargaCaso()
        {
            try
            {
                DataTable DT = (DataTable)ViewState["gvCasos"];
                if (DT != null && DT.Rows.Count > 0)
                {
                    gvCasos.DataSource = DT;
                    gvCasos.DataBind();
                }
            }
            catch (Exception x) { throw x; }
        }
        public void GetStatusSol(int iStatusSol)
        {
            try
            {
                Session["StatusSol"] = iStatusSol;
                iStatusSol = Session["StatusSol"].S().I();
            }
            catch (Exception x) { throw x; }
        }
        public void GetIdCaso(int iCaso)
        {
            try
            {
                Session["iCaso"] = iCaso;
                iCaso = Session["iCaso"].S().I();
            }
            catch (Exception x) { throw x; }
        }
        public void LoadCaso(DataTable DT)
        {
            try
            {
                gvCasos.DataSource = DT;
                gvCasos.DataBind();
                ViewState["gvCasos"] = null;
                ViewState["gvCasos"] = DT;
            }
            catch (Exception x) { throw x; }
        }
        #endregion

        #region Variables
        DashboarsAtnCliente_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eSolicitud;
        public event EventHandler eCasos;
        private const string sClase = "frmDashboard.aspx.cs";
        private const string sPagina = "frmDashboard.aspx";
        public object[] oArrFiltros
        {
            get
            {
                return new object[] { 
                                        "@FECHA", deFecha.Text == "" ? System.DateTime.Now.ToString("MM-dd-yyyy") : deFecha.Date.ToString("MM-dd-yyyy"), 
                                    };
            }
        }
        public object[] oArrFilSol
        {
            get
            {
                return new object[] { 
                                        "@FECHA", deFecha.Text == "" ? System.DateTime.Now.ToString("MM-dd-yyyy") : deFecha.Date.ToString("MM-dd-yyyy"),
                                        "@Status" , iStatusSol
                                    };
            }
        }
        public object[] oArrCasos
        {
            get
            {
                return new object[] { 
                                        "@FECHA", deFecha.Text == "" ? System.DateTime.Now.ToString("MM-dd-yyyy") : deFecha.Date.ToString("MM-dd-yyyy"),
                                        "@IdCaso" , iCaso
                                    };
            }
        }
        public int iStatusSol
        {
            get { return Session["StatusSol"].S().I(); }
            set { Session["StatusSol"] = iStatusSol; }
        }
        public int iCaso
        {
            get { return Session["iCaso"].S().I(); }
            set { Session["iCaso"] = iCaso; }
        }
        #endregion

    }
}