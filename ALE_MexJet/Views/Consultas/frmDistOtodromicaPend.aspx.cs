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
    public partial class frmDistOtodromicaPend : System.Web.UI.Page, IVIewDistOrtPend 
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            oPresenter = new DistOrtPend_Presenter(this, new DBDistOrtPend());
            gvDisOrto.SettingsText.SearchPanelEditorNullText = "Ingrese la información a buscar";

            if (!IsPostBack)
            {
                if (eSearchObj != null)
                    eSearchObj(null, null);
            }
            RecargaGrid();
        }
        protected void btnExcel_Click(object sender, EventArgs e)
        {
            Exporter.WriteXlsToResponse(new XlsExportOptionsEx() { ExportType = ExportType.DataAware });
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
                throw ex;
            }
        }
        public void LoadGv(DataTable dtObjCat)
        {
            gvDisOrto.DataSource = dtObjCat;
            gvDisOrto.DataBind();
            ViewState["gvDisOrto"] = dtObjCat;
        }
        protected void RecargaGrid()
        {
            if (ViewState["gvDisOrto"] != null)
            {
                gvDisOrto.DataSource = (DataTable)ViewState["gvDisOrto"];
                gvDisOrto.DataBind();
            }
        }

        #region "Vars y Propiedades"
        private const string sClase = "frmDistOtodromicaPend.aspx.cs";
        private const string sPagina = "frmDistOtodromicaPend.aspx";

        DistOrtPend_Presenter oPresenter;
        public event EventHandler eSearchObj;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;

        #endregion
    }
}