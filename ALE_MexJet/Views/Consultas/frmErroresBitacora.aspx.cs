using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ALE_MexJet.Interfaces;
using ALE_MexJet.Presenter;
using ALE_MexJet.DomainModel;
using DevExpress.Web;
using NucleoBase.Core;
using System.Reflection;
using DevExpress.XtraPrinting;
using DevExpress.Export;
using ALE_MexJet.Clases;
using System.Data;
using ALE_MexJet.Objetos;

namespace ALE_MexJet.Views.Consultas
{
    public partial class frmErroresBitacora : System.Web.UI.Page, IViewErroresBitacora
    {
        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {DrPermisos = Utils.ObtenerPermisos(Enumeraciones.Pantallas.ErroresBitacoras);
                
                LoadActions(DrPermisos);
                oPresenter = new ErroresBitacora_Presenter(this, new DBErroresBitacora());
                gvErroresBitacora.SettingsPager.Position = PagerPosition.TopAndBottom;
                gvErroresBitacora.SettingsPager.ShowDisabledButtons = true;
                gvErroresBitacora.SettingsPager.ShowNumericButtons = true;
                gvErroresBitacora.SettingsPager.ShowSeparators = true;
                gvErroresBitacora.SettingsPager.Summary.Visible = true;
                gvErroresBitacora.SettingsPager.PageSizeItemSettings.Visible = true;
                gvErroresBitacora.SettingsPager.PageSizeItemSettings.Position = PagerPageSizePosition.Right;
                gvErroresBitacora.SettingsText.SearchPanelEditorNullText = "Ingrese la información a buscar";

                ObtieneValores();
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "Page_Load", "Se presentó un error");
            }
        }

        protected void btnBusqueda_Click(object sender, EventArgs e)
        {
            try
            {
                ObtieneValores();
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnBusqueda_Click", "Se presentó un error");
            }
        }

        protected void btnExportarExcel_Click(object sender, EventArgs e)
        {
            try
            {
                Exporter.WriteXlsToResponse(new XlsExportOptionsEx() { ExportType = ExportType.DataAware });
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnExportarExcel_Click", "Se presentó un error");
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
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "Unnamed_Unload", "Se presentó un error");
            }
        }  
        #endregion EVENTOS


        #region METODOS
        public void MostrarMensaje(string sMensaje, string sCaption)
        {
            popup.HeaderText = sCaption;
            gvErroresBitacora.JSProperties["cpText"] = sMensaje;
            gvErroresBitacora.JSProperties["cpShowPopup"] = true;
        }
        public void MostrarMensajeError(string sMensaje, string sCaption)
        {
            mpeMensaje.ShowMessage(sMensaje, sCaption);
        }

        public void LoadObjects(System.Data.DataTable dtObjCat)
        {
            gvErroresBitacora.DataSource = null;
            ViewState["oDatos"] = null;
            gvErroresBitacora.DataSource = dtObjCat;
            ViewState["oDatos"] = dtObjCat;
            gvErroresBitacora.DataBind();
        }

        public void ObtieneValores()
        {
            if (eSearchObj != null)
                eSearchObj(null, EventArgs.Empty);
        }
        public void LoadActions(DataRow[] DrActions)
        {
            int iPos = 0;
            if (DrActions.Length == 0)
            {
                btnBusqueda.Enabled = false;
                txtTextoBusqueda.Enabled = false;
                ddlTipoBusqueda.Enabled = false;
                btnExportarExcel2.Enabled = false;
                btnExportarExcel.Enabled = false;
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
                                txtTextoBusqueda.Enabled = true;
                                ddlTipoBusqueda.Enabled = true;;
                                btnExportarExcel2.Enabled = true;
                                btnExportarExcel.Enabled = true;

                            }
                            else
                            {
                                btnBusqueda.Enabled = false;
                                txtTextoBusqueda.Enabled = false;
                                ddlTipoBusqueda.Enabled = false;
                                btnExportarExcel2.Enabled = false;
                                btnExportarExcel.Enabled = false;

                            } break;
                    }
                }
            }

        }
        #endregion METODOS


        #region VARS Y PROPIEDADES
        
        private const string sPagina = "frmErroresBitacora.aspx";
        private const string sClase = "frmErroresBitacora.aspx.cs";
        public object[] oArrFiltros
        {
            get
            {

                string sSerie = string.Empty;
                string sMatricula = string.Empty;
                string sFolioReal = string.Empty;

                switch (ddlTipoBusqueda.SelectedValue.S())
                {
                    case "1": sSerie = txtTextoBusqueda.Text.S(); sMatricula = string.Empty; sFolioReal = string.Empty; break;
                    case "2": sMatricula = txtTextoBusqueda.Text.S(); sSerie = string.Empty; sFolioReal = string.Empty; break;
                    case "3": sFolioReal = txtTextoBusqueda.Text.S(); sSerie = string.Empty; sMatricula = string.Empty; break;
                }

                return new object[]{
                                        "@Serie", "%" + sSerie + "%",
                                        "@Matricula", "%" + sMatricula + "%",                                       
                                        "@FolioReal", "%" + sFolioReal + "%"
                                    };
            }
        }

        ErroresBitacora_Presenter oPresenter;

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
        #endregion VARS Y PROPIEDADES
               
    }
}