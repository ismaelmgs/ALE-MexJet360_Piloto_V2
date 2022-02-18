using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Reflection;
using DevExpress.XtraPrinting;
using DevExpress.Export;
using DevExpress.Web;
using ALE_MexJet.Presenter;
using ALE_MexJet.Interfaces;
using ALE_MexJet.DomainModel;
using ALE_MexJet.Objetos;
using NucleoBase.Core;
using ALE_MexJet.Clases;


namespace ALE_MexJet.Views.CreditoCobranza
{
    public partial class frmPagoAPHIS : System.Web.UI.Page, IViewGeneracionPagosAPHIS
    {
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            DrPermisos = Utils.ObtenerPermisos(Enumeraciones.Pantallas.PagoAPHIS);
            LoadActions(DrPermisos);
            oPresenter = new GeneracionPagosAPHIS_Presenter(this, new DBGeneracionPagosAPHIS());
            gvPagosAPHIS.SettingsPager.Position = PagerPosition.TopAndBottom;
            gvPagosAPHIS.SettingsPager.ShowDisabledButtons = true;
            gvPagosAPHIS.SettingsPager.ShowNumericButtons = true;
            gvPagosAPHIS.SettingsPager.ShowSeparators = true;
            gvPagosAPHIS.SettingsPager.Summary.Visible = true;
            gvPagosAPHIS.SettingsPager.PageSizeItemSettings.Visible = true;
            gvPagosAPHIS.SettingsPager.PageSizeItemSettings.Position = PagerPageSizePosition.Right;
            gvPagosAPHIS.SettingsText.SearchPanelEditorNullText = "Ingrese la información a buscar";

            obtieneValores();
            obtieneValoresFecha();                        
        }
        
        protected void Unnamed_Unload(object sender, EventArgs e)
        {
            MethodInfo methodInfo = typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Where(i => i.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel")).First();
            methodInfo.Invoke(ScriptManager.GetCurrent(Page),
                new object[] { sender as UpdatePanel });
        }
               
        protected void btnBusqueda_Click(object sender, EventArgs e)
        {
            obtieneValores();
        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            Exporter.WriteXlsToResponse(new XlsExportOptionsEx() { ExportType = ExportType.DataAware });
        }

        #endregion Eventos

        #region Metodos

        public void obtieneValores()
        {
            if (eGetPagosAPHIS != null)
                eGetPagosAPHIS(this, EventArgs.Empty);
        }
        public void obtieneValoresFecha()
        {
            int añoActual = System.DateTime.Now.Year;
            int añoAnterior = System.DateTime.Now.Year - 1;

            if (ddlanio.Items.Count != 4)
            {
                ddlanio.Items.Clear();
                ddlanio.Items.Add("[Sin Filtro]");
                ddlanio.Items.Add(Convert.ToString(añoAnterior));
                ddlanio.Items.Add(Convert.ToString(añoActual));
                ddlanio.Items.Add("2007");
                ddlanio.Items[0].Value = "0";
                ddlanio.Items[1].Value = "1";
                ddlanio.Items[2].Value = "2";
            }
        }

        public void LoadPagosAPHIS(DataTable dtObjAPHIS)
        {
            gvPagosAPHIS.DataSource = null;
            gvPagosAPHIS.DataSource = dtObjAPHIS;
            gvPagosAPHIS.DataBind();
            ViewState["PagosAphis"] = dtObjAPHIS;
        }
        public void LoadActions(DataRow[] DrActions)
        {
            int iPos = 0;
            if (DrActions.Length == 0)
            {
                btnBusqueda.Enabled = false;
                ddlanio.Enabled = false;
                ddlTipoBusqueda.Enabled = false;
                btnExportar.Enabled = false;
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
                                ddlanio.Enabled = true;
                                ddlTipoBusqueda.Enabled = true;
                                btnExportar.Enabled = true;
                                btnExcel.Enabled = true;

                            }
                            else
                            {
                                btnBusqueda.Enabled = false;
                                ddlanio.Enabled = false;
                                ddlTipoBusqueda.Enabled = false;
                                btnExportar.Enabled = false;
                                btnExcel.Enabled = false;

                            } break;
                    }
                }
            }

        }
        #endregion Metodos

        #region Propiedades y Variables
        
        public object[] oArrFiltroPagosAPHIS
        {
            get
            {
                string sArmarFecha = "01/01/1900";
                if (ddlanio.Items.Count == 4 )
                {
                    if (ddlanio.SelectedItem.Text != "[Sin Filtro]" && ddlTipoBusqueda.SelectedItem.Text != "[Sin Filtro]")
                    {
                        string sMes = "";
                        if (ddlTipoBusqueda.SelectedItem.Text == "Trimestre 1")
                            sMes = "01";
                        else if (ddlTipoBusqueda.SelectedItem.Text == "Trimestre 2")
                            sMes = "04";
                        else if (ddlTipoBusqueda.SelectedItem.Text == "Trimestre 3")
                            sMes = "07";
                        else if (ddlTipoBusqueda.SelectedItem.Text == "Trimestre 4")
                            sMes = "10";
                        sArmarFecha = "01/" + sMes.ToString() + "/" + ddlanio.SelectedItem.Text;
                    }                    
                }
                DateTime sFecha = Convert.ToDateTime(sArmarFecha);                
                return new object[] { "@pdFecha", sFecha};
            }
        }

        GeneracionPagosAPHIS_Presenter oPresenter;
        public event EventHandler eGetPagosAPHIS;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        UserIdentity oUsuario = new UserIdentity();
        public DataRow[] DrPermisos
        {
            get { return (DataRow[])Session["DrPermisos"]; }
            set { Session["DrPermisos"] = value; }
        }
        #endregion Propiedades y Variables                                                                 
    }
}