using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Data;
using ALE_MexJet.Presenter;
using ALE_MexJet.Interfaces;
using ALE_MexJet.DomainModel;
using ALE_MexJet.Objetos;
using DevExpress.XtraPrinting;
using DevExpress.Export;
using NucleoBase.Core;
using ALE_MexJet.Clases;

namespace ALE_MexJet.Views.CreditoCobranza
{
    public partial class frmPagoSENEAM : System.Web.UI.Page, IViewPagoSENEAM
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //DrPermisos = Utils.ObtenerPermisos(Enumeraciones.Pantallas.PagoSENEAM);
            //LoadActions(DrPermisos);
            oPresenter = new PagoSENEAM_Presenter(this, new DBPagoSENEAM());
            gvPagoSENEAM.SettingsPager.Position = PagerPosition.TopAndBottom;
            gvPagoSENEAM.SettingsPager.ShowDisabledButtons = true;
            gvPagoSENEAM.SettingsPager.ShowNumericButtons = true;
            gvPagoSENEAM.SettingsPager.ShowSeparators = true;
            gvPagoSENEAM.SettingsPager.Summary.Visible = true;
            gvPagoSENEAM.SettingsPager.PageSizeItemSettings.Visible = true;
            gvPagoSENEAM.SettingsPager.PageSizeItemSettings.Position = PagerPageSizePosition.Right;
            gvPagoSENEAM.SettingsText.SearchPanelEditorNullText = "Ingrese la información a buscar";

            RecargaGrid();
            deFecha.AllowUserInput = false;
            //ObtieneValores();
        }
        protected void Unnamed_Unload(object sender, EventArgs e)
        {
            MethodInfo methodInfo = typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Where(i => i.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel")).First();
            methodInfo.Invoke(ScriptManager.GetCurrent(Page),
                new object[] { sender as UpdatePanel });
        }

        protected void btnBusqueda_Click(object sender, EventArgs e)
        {
            ObtieneValores();
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            Exporter.WriteXlsToResponse(new XlsExportOptionsEx() { ExportType = ExportType.DataAware });
        }

        public void ObtieneValores()
        {
            if (eSearchObj != null)
                eSearchObj(null, EventArgs.Empty);
        }
        public void LoadObjects(DataTable dtObjPagoSENEAM)
        {
            gvPagoSENEAM.DataSource = null;
            gvPagoSENEAM.DataSource = dtObjPagoSENEAM;
            gvPagoSENEAM.DataBind();
            ViewState["PagoSENEAM"] = dtObjPagoSENEAM;
        }
        protected void RecargaGrid()
        {
            if (ViewState["PagoSENEAM"] != null)
            {
                gvPagoSENEAM.DataSource = (DataTable)ViewState["PagoSENEAM"];
                gvPagoSENEAM.DataBind();
            }
        }
        public void LoadActions(DataRow[] DrActions)
        {
            int iPos = 0;
            if (DrActions.Length == 0)
            {
                btnBusqueda.Enabled = false;
                //cmdMeses.Enabled = false;
                deFecha.Enabled = false;
                btnExcel2.Enabled = false;
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
                                //cmdMeses.Enabled = true;
                                deFecha.Enabled = false;
                                btnExcel2.Enabled = true;
                                btnExcel.Enabled = true;

                            }
                            else
                            {
                                btnBusqueda.Enabled = false;
                                //cmdMeses.Enabled = false;
                                deFecha.Enabled = false;
                                btnExcel2.Enabled = false;
                                btnExcel.Enabled = false;

                            } break;
                    }
                }
            }

        }
        public object[] oArrFiltros
        {
            get {

                string sMes = "";
                string iAnio = "";
                int iNumeroMes = 0;

                char[] delimiterChars = { '-' };
                string Fecha = deFecha.Text;

                string[] words = Fecha.Split(delimiterChars);

                foreach (string s in words)
                {
                    if (sMes == "")
                        sMes = s;

                    iAnio = s;
                }

                if (sMes != "")
                {
                    switch (sMes)
                    {
                        case "enero": iNumeroMes = 1; break;
                        case "febrero": iNumeroMes = 2; break;
                        case "marzo": iNumeroMes = 3; break;
                        case "abril": iNumeroMes = 4; break;
                        case "mayo": iNumeroMes = 5; break;
                        case "junio": iNumeroMes = 6; break;
                        case "julio": iNumeroMes = 7; break;
                        case "agosto": iNumeroMes = 8; break;
                        case "septiembre": iNumeroMes = 9; break;
                        case "octubre": iNumeroMes = 10; break;
                        case "noviembre": iNumeroMes = 11; break;
                        case "diciembre": iNumeroMes = 12; break;
                    }
                }
                return new object[] { 
                    "@viMes", iNumeroMes,
                    "@viAnio",Convert.ToInt32(iAnio)
                };
            }
        }

        public void MostrarMensaje(string sMensaje, string sCaption)
        {
            popup.HeaderText = sCaption;
            gvPagoSENEAM.JSProperties["cpText"] = sMensaje;
            gvPagoSENEAM.JSProperties["cpShowPopup"] = true;
        }
        #region  Propiedad y Variables
        PagoSENEAM_Presenter oPresenter;

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