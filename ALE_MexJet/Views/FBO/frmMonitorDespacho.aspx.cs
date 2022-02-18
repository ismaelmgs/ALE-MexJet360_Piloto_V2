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
using ALE_MexJet.Objetos;

namespace ALE_MexJet.Views.FBO
{
    public partial class frmMonitorDespacho : System.Web.UI.Page, IViewMonitorDespacho
    {
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                gvMonitorDespacho.SettingsText.SearchPanelEditorNullText = "Ingrese la información a buscar";
                oPresenter = new MonitorDespacho_Presenter(this, new DBMonitorDespacho());
                if (!IsPostBack)
                {
                    if (eBuscaDDL != null)
                        eBuscaDDL(null, null);
                }
                RecargaGrid();
                RecargaPiernaDictamen();
            }
            catch (Exception ex) { Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "Page_Load", "Aviso"); }
        }
        protected void gvMonitorDespacho_RowCommand(object sender, DevExpress.Web.ASPxGridViewRowCommandEventArgs e)
        {
            try
            {
                if (e.CommandArgs.CommandName.S() == "Dictamen")
                {
                    rblDictamen.SelectedItem = null;
                    mObservaiones.Text = "";
                    mNotas.Text = "";
                    DataTable dt = (DataTable)ViewState["gvMonitorDespacho"];
                    iIdSolicitudApp = e.KeyValue.S().I();//dt.Rows[e.VisibleIndex]["IdSolicitud"].S().I();

                    iIdMonitor = e.CommandArgs.CommandArgument.S().I();

                    ppDictamen.ShowOnPageLoad = true;

                    iIdSolicitud = e.KeyValue.ToString().I();

                    if (eBuscaDictamen != null)
                        eBuscaDictamen(null, null);

                    if (eBuscaPiernadictamen != null)
                        eBuscaPiernadictamen(null,null);
                }
                else
                    if (e.CommandArgs.CommandName.S() == "Notas")
                    {
                        mObservaiones.Text = "";
                        mNotas.Text = "";

                        iIdSolicitud = e.CommandArgs.CommandArgument.S().I();

                        

                        ppNotas.ShowOnPageLoad = true;
                    }
                    else
                        if (e.CommandArgs.CommandName.S() == "Enterado")
                        {
                            iIdMonitor = e.CommandArgs.CommandArgument.S().I();
                            ViewState["Enterado"] = "1";

                            if (eSaveObj != null)
                                eSaveObj(null, null);

                            if (eSearchObj != null)
                                eSearchObj(null, null);

                            ViewState["Enterado"] = "0";
                        }
            }
            catch (Exception ex) { Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvMonitorDespacho_RowCommand", "Aviso"); }
        }
        protected void gvPiernas_BeforePerformDataSelect(object sender, EventArgs e)
        {
            try
            {
                ASPxGridView grid = sender as ASPxGridView;

                iIdSolicitud = grid.GetMasterRowKeyValue().S().I();

                if (eBuscaSubGrid != null)
                    eBuscaSubGrid(null, EventArgs.Empty);

                grid.DataSource = (DataTable)ViewState["LlenaSubGrid"];
            }
            catch (Exception ex) { Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvPiernas_BeforePerformDataSelect", "Aviso"); }
        }
        protected void UpdatePanel1_Unload(object sender, EventArgs e)
        {
            MethodInfo methodInfo = typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Where(i => i.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel")).First();
            methodInfo.Invoke(ScriptManager.GetCurrent(Page),
                new object[] { sender as UpdatePanel });
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if (eSearchObj != null)
                    eSearchObj(null, null);
            }
            catch (Exception ex) { Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnBuscar_Click", "Aviso"); }
        }
        protected void btnGuardarD_Click(object sender, EventArgs e)
        {
            try
            {
                if (eSaveObj != null)
                {
                        eSaveObj(sender, e);
                        eSaveSeguimiento(sender, e);

                        new DBMonitorTrafico().DBInsertaMonitorTrafico(iIdSolicitud);
                }

                if (eSearchObj != null)
                    eSearchObj(null, null);

                rblDictamen.SelectedItem = null;
                mObservaiones.Text = "";
            }
            catch (Exception ex) { Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnGuardarD_Click", "Aviso"); }
        }
        protected void UpdateTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (ddlBase.SelectedItem != null)
                {
                    if (eSearchObj != null)
                        eSearchObj(null, null);
                }
            }
            catch (Exception ex) { Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "UpdateTimer_Tick", "Aviso"); }
        }
        protected void gvMonitorDespacho_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            try
            {
                if (gvMonitorDespacho.VisibleRowCount > 0 && ViewState["gvMonitorDespacho"] != null && ((DataTable)ViewState["gvMonitorDespacho"]).Rows.Count > 0)
                {
                    if (e.GetValue("Dictamen").Equals(3))
                    {
                        e.Row.BackColor = System.Drawing.Color.Yellow;
                    }
                }
            }
            catch (Exception ex) { Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvMonitorDespacho_HtmlRowPrepared", "Aviso"); }
        }
        #endregion

        #region Metodos
        public void LlenaDDL(DataTable dtObjCat)
        {
            try
            {
                ddlBase.DataSource = dtObjCat;
                ddlBase.TextField = "AeropuertoICAO";
                ddlBase.ValueField = "idAeropuert";
                ddlBase.DataBind();
            }
            catch (Exception x) { throw x; }
        }
        public void LlenaGrid(DataTable dtObjCat)
        {
            try
            {
                gvMonitorDespacho.DataSource = dtObjCat;
                gvMonitorDespacho.DataBind();
                ViewState["gvMonitorDespacho"] = dtObjCat;
            }
            catch (Exception x) { throw x; }
        }
        protected void RecargaGrid()
        {
            try
            {
                if (ViewState["gvMonitorDespacho"] != null)
                {
                    gvMonitorDespacho.DataSource = (DataTable)ViewState["gvMonitorDespacho"];
                    gvMonitorDespacho.DataBind();
                }
            }
            catch (Exception x) { throw x; }
        }
        public void LlenaSubGrid(DataTable dt)
        {
            try
            {
                ViewState["LlenaSubGrid"] = dt;
            }
            catch (Exception x) { throw x; }
        }
        public void MostrarMensaje(string sMensaje, string sCaption)
        {
            try
            {
                popup.HeaderText = sCaption;
                lbl.Text = sMensaje;
                popup.ShowOnPageLoad = true;
            }
            catch (Exception x) { throw x; }
        }
        public void LlenaDictamen(DataTable dtObjCat)
        {
            try
            {
                if (dtObjCat.Rows.Count > 0)
                {
                    rblDictamen.Value = dtObjCat.Rows[0]["Dictamen"].S();
                    mObservaiones.Text = dtObjCat.Rows[0]["Observaciones"].S();
                    mNotas.Text = dtObjCat.Rows[0]["Notas"].S();

                    txtContacto.Text = dtObjCat.Rows[0]["Contacto"].S();
                    txtMotivo.Text = dtObjCat.Rows[0]["DescripcionMotivo"].S();
                    txtTipoEquipo.Text = dtObjCat.Rows[0]["Equipo"].S();
                    txtMatricula.Text = dtObjCat.Rows[0]["Matricula"].S();

                    txtContacto.ReadOnly = true;
                    txtMotivo.ReadOnly = true;
                    txtTipoEquipo.ReadOnly = true;
                    txtMatricula.ReadOnly = true;
                    txtContacto.ReadOnly = true;
                    mNotas.ReadOnly = true;
                }
                else
                {
                    rblDictamen.Value = null;
                    mObservaiones.Text = "";
                    mNotas.Text = "";

                    txtContacto.Text = "";
                    txtMotivo.Text = "";
                    txtTipoEquipo.Text = "";
                    txtMatricula.Text = "";
                }
            }
            catch (Exception x) { throw x; }
        }
        public void LlenaPiernaDictamen(DataTable dtObjCat)
        {
            try
            {
                gvPiernaDic.DataSource = dtObjCat;
                gvPiernaDic.DataBind();
                ViewState["LlenaPiernaDictamen"] = dtObjCat;
            }
            catch (Exception x) { throw x; }
        }
        public void RecargaPiernaDictamen()
        {
            try
            {
                if (ViewState["LlenaPiernaDictamen"] != null)
                {
                    gvPiernaDic.DataSource = (DataTable)ViewState["LlenaPiernaDictamen"];
                    gvPiernaDic.DataBind();
                }
            }
            catch (Exception x) { throw x; }
        }
        #endregion

        #region Variables
        private const string sClase = "MonitorComisariato.aspx.cs";
        private const string sPagina = "MonitorComisariato.aspx";
        UserIdentity oUsuario = new UserIdentity();
        MonitorDespacho_Presenter oPresenter;
        public int iIdSolicitud
        {
            get { return ViewState["idSolicitud"].S().I(); }
            set { ViewState["idSolicitud"] = value; }
        }
        public int iIdSolicitudApp
        {
            get { return ViewState["idSolicitudApp"].S().I(); }
            set { ViewState["idSolicitudApp"] = value; }
        }
        public int iIdMonitor
        {
            get { return ViewState["iIdMonitor"].S().I(); }
            set { ViewState["iIdMonitor"] = value; }
        }
        public object[] oArrFiltros
        {
            get
            {
                return new object[] { 
                                        "@IdBase", ddlBase.SelectedItem.Value                                        
                                    };
            }
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
        public object[] oArrFilUpd
        {
            get
            {
                return new object[] { 
                                        "@IdMonitor", iIdMonitor,
                                        "@Dictamen",  ViewState["Enterado"] == "1" ? 0 : rblDictamen.Value,
                                        "@Observaciones", mObservaiones.Text,
                                        "@OrigenSolicitud", "MexJet360",
                                        "@Usuario", ((UserIdentity)Session["UserIdentity"]).sUsuario.S(),
                                        "@IP", ""
                                    };
            }
        }

        public object[] oArrFilSeguimiento
        {
            get
            {
                return new object[] { 
                                        "@IdSolicitud", iIdSolicitud,
                                        "@idAutor",  2,
                                        "@Nota", String.Concat("El dictámen fué: ", (rblDictamen.Value=="1"?"SI":rblDictamen.Value=="2"?"NO":rblDictamen.Value=="3"?"RESTRINGIDO":"")),
                                        "@Status", 1,
                                        "@IP", Request.UserHostAddress,
                                        "@Usuario", ((UserIdentity)Session["UserIdentity"]).sUsuario.S()
                                    };
            }
        }

        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eSaveSeguimiento;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eBuscaDDL;
        public event EventHandler eBuscaSubGrid;
        public event EventHandler eBuscaDictamen;
        public event EventHandler eBuscaPiernadictamen;
        #endregion
    }
}