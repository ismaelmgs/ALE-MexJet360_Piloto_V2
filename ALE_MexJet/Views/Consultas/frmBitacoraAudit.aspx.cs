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

namespace ALE_MexJet.Views.Consultas
{
    public partial class frmBitacoraAudit : System.Web.UI.Page, IViewBitacoraAudit
    {
        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            try 
            {
                DrPermisos = Utils.ObtenerPermisos(Enumeraciones.Pantallas.BitacoraAudit);
                LoadActions(DrPermisos);
                gvBitacoraAudit.SettingsText.SearchPanelEditorNullText = "Ingrese la información a buscar";
                gvBitacoraAudit.SettingsText.GroupPanel = "Arrastre un encabezado de columna aquí para agrupar por esa columna";
                oPresenter = new BitacoraAudit_Presenter(this, new DBBitacoraAudit());
               
                if (IsPostBack)
                {
                    LoadBitacoraAudit((DataTable)Session["oBitacora"]);
                }
                else
                {
                    ObtieneValoresAccion();
                    ObtieneValoresModulo();
                    ObtieneValoresUsuario();
                    
                }
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
                if (eSearchObj != null)
                    eSearchObj(null, null);
             
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnBusqueda_Click", "Aviso");
            }
        }

        protected void ddlAccion_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            //try
            //{
            //    if (eObjConrato != null)
            //        eObjConrato(null, null);
            //}
            //catch (Exception ex)
            //{
            //    Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "ddlClientes_SelectedIndexChanged", "Aviso");
            //}
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

        protected void UpdatePanel2_Unload(object sender, EventArgs e)
        {
            try
            {
                MethodInfo methodInfo = typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Where(i => i.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel")).First();
                methodInfo.Invoke(ScriptManager.GetCurrent(Page),
                new object[] { sender as UpdatePanel });
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "UpdatePanel2_Unload", "Aviso");
            }
        }
        protected void UpdatePanel3_Unload(object sender, EventArgs e)
        {
            try
            {
                MethodInfo methodInfo = typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Where(i => i.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel")).First();
                methodInfo.Invoke(ScriptManager.GetCurrent(Page),
                new object[] { sender as UpdatePanel });
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "UpdatePanel3_Unload", "Aviso");
            }
        }
        #endregion

        #region "METODOS"

        public void ObtieneValores()
        {
            if (eSearchObj != null)
                eSearchObj(null, EventArgs.Empty);
        }
        public void ObtieneValoresAccion()
        {
            if (eObjAccion != null)
                eObjAccion(null, EventArgs.Empty);
        }
        public void ObtieneValoresModulo()
        {
            if (eObjModulo != null)
                eObjModulo(null, EventArgs.Empty);
        }
        public void ObtieneValoresUsuario()
        {
            if (eObjUsuario != null)
                eObjUsuario(null, EventArgs.Empty);
        }
        public void MostrarMensaje(string sMensaje, string sCaption)
        {
            try
            {
                mpeMensaje.ShowMessage(sMensaje, sCaption);
            }
            catch (Exception x) { throw x; }
        }

        public void LoadAccion(DataTable dtObjAcci)
        {
            ViewState["oAccion"] = dtObjAcci;
            ddlAccion.DataSource = dtObjAcci;
            ddlAccion.TextField = "Descripcion";
            ddlAccion.ValueField = "IdAccion";
            ddlAccion.DataBind();
            ddlAccion.SelectedItem = null;
        }
        public void LoadModulo(DataTable dtObjModu)
        {
            ViewState["oModulo"] = dtObjModu;
            ddlModulo.DataSource = dtObjModu;
            ddlModulo.TextField = "Descripcion";
            ddlModulo.ValueField = "ModuloId";
            ddlModulo.DataBind();
            ddlModulo.SelectedItem = null;
        }
        public void LoadUsuario(DataTable dtObjUsu)
        {
            ViewState["oUsuario"] = dtObjUsu;
            ddlUsuario.DataSource = dtObjUsu;
            ddlUsuario.TextField = "Descripcion";
            ddlUsuario.ValueField = "IdUsuario";
            ddlUsuario.DataBind();
            ddlUsuario.SelectedItem = null;
        }

        public void LoadBitacoraAudit(DataTable dtObjCat)
        {
             Session["oBitacora"] = dtObjCat;
             gvBitacoraAudit.DataSource = dtObjCat;
             gvBitacoraAudit.DataBind();
        }
        public void LoadActions(DataRow[] DrActions)
        {
            int iPos = 0;
            if (DrActions.Length == 0)
            {
                btnBusqueda.Enabled = false;
                ddlModulo.Enabled = false;
                ddlAccion.Enabled = false;
                ddlUsuario.Enabled = false;
                dFechaIni.Enabled = false;
                dFechaFin.Enabled = false;
                btnExcel.Enabled = false;
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
                                ddlModulo.Enabled = true;
                                ddlAccion.Enabled = true;
                                ddlUsuario.Enabled = true;
                                dFechaIni.Enabled = true;
                                dFechaFin.Enabled = true;
                                btnExcel.Enabled = true;

                            }
                            else
                            {
                                btnBusqueda.Enabled = false;
                                ddlModulo.Enabled = false;
                                ddlAccion.Enabled = false;
                                ddlUsuario.Enabled = false;
                                dFechaIni.Enabled = false;
                                dFechaFin.Enabled = false;
                                btnExcel.Enabled = false;

                            } break;
                    }
                }
            }

        }
        #endregion

        #region "Vars y Propiedades"
        private const string sClase = "frmBitacoraAudit.aspx.cs";
        private const string sPagina = "frmBitacoraAudit.aspx";

        BitacoraAudit_Presenter oPresenter;
        public event EventHandler eObjAccion;
        public event EventHandler eObjModulo;
        public event EventHandler eObjUsuario;
        public event EventHandler eSearchObj;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        UserIdentity oUsuario = new UserIdentity();
        public DataRow[] DrPermisos
        {
            get { return (DataRow[])Session["DrPermisos"]; }
            set { Session["DrPermisos"] = value; }
        }
        public object[] oArrFiltros
        {
            get
            {
                int iIdModulo;
                int iIdAccion;
                string sUsuario = string.Empty;
                string sFechaInicial = string.Empty;
                string sFechaFinal = string.Empty;
                string sIp = string.Empty;

                iIdModulo = ddlModulo.Value.S().I();
                iIdAccion = ddlAccion.Value.S().I();
                if (ddlUsuario.Text.S() != "[Sin Filtro]")
                    sUsuario = ddlUsuario.Text.S();
                else
                sUsuario = "";
                sFechaInicial = dFechaIni.Text.S();
                sFechaFinal = dFechaFin.Text.S();
                sIp = sIP.Text.S();

                return new object[]{
                                        "@ModuloId", iIdModulo ,
                                        "@IdAccion", iIdAccion,
                                        "@Usuario",  sUsuario ,
                                        "@FechaIni", sFechaInicial,
                                        "@FechaFin", sFechaFinal ,
                                        "@IP", sIp
                                    };
            }

        }

        #endregion

        protected void ddlModulo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}