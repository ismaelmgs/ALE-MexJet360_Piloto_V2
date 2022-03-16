using ALE_MexJet.DomainModel;
using ALE_MexJet.Interfaces;
using ALE_MexJet.Presenter;
using ALE_MexJet.Objetos;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NucleoBase.Core;
using System.Reflection;
using ALE_MexJet.Clases;
using DevExpress.XtraPrinting;
using DevExpress.Export;

namespace ALE_MexJet.Views.CreditoCobranza
{
    public partial class frmRemisiones : System.Web.UI.Page, IViewRemision
    {

        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            DrPermisos = Utils.ObtenerPermisos(Enumeraciones.Pantallas.Remisiones);
            LoadActions(DrPermisos);
            gvRemisiones.Columns["Estatus"].Visible = true;
            oPresenter = new Remision_Presenter(this, new DBRemision());
            gvRemisiones.SettingsPager.Position = PagerPosition.TopAndBottom;
            gvRemisiones.SettingsPager.ShowDisabledButtons = true;
            gvRemisiones.SettingsPager.ShowNumericButtons = true;
            gvRemisiones.SettingsPager.ShowSeparators = true;
            gvRemisiones.SettingsPager.Summary.Visible = true;
            gvRemisiones.SettingsPager.PageSizeItemSettings.Visible = true;
            gvRemisiones.SettingsPager.PageSizeItemSettings.Position = PagerPageSizePosition.Right;
            gvRemisiones.SettingsText.SearchPanelEditorNullText = "Ingresa la información a buscar:";
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("frmGRemision.aspx");
            }
            catch (Exception ex)
            {

            }
        }

        protected void UpdatePanel1_Unload(object sender, EventArgs e)
        {
            MethodInfo methodInfo = typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Where(i => i.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel")).First();
            methodInfo.Invoke(ScriptManager.GetCurrent(Page),
                new object[] { sender as UpdatePanel });
        }


        protected void gvRemisiones_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
        {
            if (e.CommandArgs.CommandName.S() == "Detalle")
            {
                Session["Matricula"] = string.Empty;
                int index = e.VisibleIndex.I();
                string sIdRemision = e.CommandArgs.CommandArgument.S();
                string sMatricula = gvRemisiones.GetRowValues(index, "Matricula").ToString();

                if (!string.IsNullOrEmpty(sMatricula))
                    Session["Matricula"] = sMatricula;

                Response.Redirect("~/Views/CreditoCobranza/frmGRemision.aspx?Folio=" + sIdRemision, false);
            }

            else if (e.CommandArgs.CommandName.S() == "Eliminar")
            {
                if (hfValor.Value == "true")
                {
                    iIdRemision = e.CommandArgs.CommandArgument.I();

                    if (eDeleteObj != null)
                        eDeleteObj(null, EventArgs.Empty);
                }
            }
        }        

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            Exporter.WriteXlsToResponse(new XlsExportOptionsEx() { ExportType = ExportType.DataAware });
        }
       

        #endregion

        #region METODOS
        
        public void MostrarMensaje(string sMensaje, string sCaption)
        {
            try
            {
                popup.HeaderText = sCaption;
                gvRemisiones.JSProperties["cpText"] = sMensaje;
                gvRemisiones.JSProperties["cpShowPopup"] = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public void LoadObjects(DataTable dt)
        {
            gvRemisiones.DataSource = dt;
            gvRemisiones.DataBind();
        }
        public void LoadActions(DataRow[] DrActions)
        {
            int iPos = 0;
            if (DrActions.Length == 0)
            {
                btnBusqueda.Enabled = false;
                txtTextoBusqueda.Enabled = false;
                ddlTipoBusqueda.Enabled = false;
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
                                ddlTipoBusqueda.Enabled = true;
                                btnExcel.Enabled = true;

                            }
                            else
                            {
                                btnBusqueda.Enabled = false;
                                ddlTipoBusqueda.Enabled = false;
                                btnExcel.Enabled = false;

                            } break;
                        case 5: if (DrActions[0].ItemArray[iPos].S().I() == Convert.ToInt32(Enumeraciones.Bloqueo.Permitido))
                            {
                                btnNuevo.Enabled = true;
                            }
                            else
                            {
                                btnNuevo.Enabled = false;
                            } break;
                    }
                }
            }

        }
        #endregion

        #region VARIABLES Y PROPIEDADES

        Remision_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;       
        UserIdentity oUsuario = new UserIdentity();

        protected static int iIdRemision;
        public Remision oRemision
        {
            get
            {
                Remision oRem = new Remision();
                oRem.iIdRemision = iIdRemision;
                return oRem;
            }
        }
        public DataRow[] DrPermisos
        {
            get { return (DataRow[])Session["DrPermisos"]; }
            set { Session["DrPermisos"] = value; }
        }
        public object[] oArrFiltros
        {
            get
            {
                long iFolio = 0;
                string sCliente = string.Empty;
                string sContrato = string.Empty;
                string sPrimeros = "0";


                switch (ddlTipoBusqueda.Value.S())
                {
                    case "0":
                        sPrimeros = "1";
                        break;
                    case "1":
                        iFolio = txtTextoBusqueda.Text.S().L();
                        break;
                    case "2":
                        sCliente = txtTextoBusqueda.Text.S();
                        break;
                    case "3":
                        sContrato = txtTextoBusqueda.Text.S();
                        break;
                }

                return new object[]{
                                        "@FolioRemision", iFolio,
                                        "@Cliente", "%" + sCliente + "%",
                                        "@Contrato", "%" + sContrato + "%",
                                        "@Primeros", sPrimeros
                                    };
            }
        }

        #endregion               

        protected void gvRemisiones_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {
            object value = (sender as DevExpress.Web.ASPxGridView).GetRowValues(e.VisibleIndex, "Status");
            e.Enabled = value.S().I() > 0;

        }

        protected void gvRemisiones_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
        {
            object value = (sender as DevExpress.Web.ASPxGridView).GetRowValues(e.VisibleIndex, "Status");
            e.Enabled = value.S().I() > 0;
        }

        protected void btn_Init(object sender, EventArgs e)
        {

            ASPxButton btn = (ASPxButton)sender;
            GridViewDataItemTemplateContainer bl = (GridViewDataItemTemplateContainer)btn.NamingContainer;
            ASPxButton btnEdicion = (ASPxButton)bl.Controls[3];
            int idRemision = bl.KeyValue.S().I();
            object status = gvRemisiones.GetRowValuesByKeyValue(bl.KeyValue, "Estatus");
                if (status.S() == "Cancelada")
            {
                btn.Enabled = false;
                btnEdicion.Enabled = false;
            }
            
        }
                    
    }
}