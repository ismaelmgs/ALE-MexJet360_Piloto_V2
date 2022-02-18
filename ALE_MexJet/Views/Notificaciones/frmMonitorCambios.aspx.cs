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
using DevExpress.XtraPrinting;

using System.Collections.Specialized;

using System.IO;

namespace ALE_MexJet.Views.Notificaciones
{
    public partial class frmMonitorCambios : System.Web.UI.Page, IViewMonitorCambios
    {
        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                oPresenter = new MonitorCambios_Presenter(this, new DBMonitorCambios());

                gvMonitorNotificaciones.SettingsPager.Position = PagerPosition.TopAndBottom;
                gvMonitorNotificaciones.SettingsPager.ShowDisabledButtons = true;
                gvMonitorNotificaciones.SettingsPager.ShowNumericButtons = true;
                gvMonitorNotificaciones.SettingsPager.ShowSeparators = true;
                gvMonitorNotificaciones.SettingsPager.Summary.Visible = true;
                gvMonitorNotificaciones.SettingsPager.PageSizeItemSettings.Visible = true;
                gvMonitorNotificaciones.SettingsPager.PageSizeItemSettings.Position = PagerPageSizePosition.Right;
                gvMonitorNotificaciones.SettingsText.SearchPanelEditorNullText = "Ingrese la información a buscar";

                loadConsultaMonitorNotificaciones();
            }
            catch (Exception ex)
            {
                //Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "Page_Load", "Aviso");
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
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "Unnamed_Unload", "Aviso");
            }
        }


        public void CargaConsultaMonitorNotificaciones(DataTable dtObjCat)
        {
            try
            {
                gvMonitorNotificaciones.DataSource = null;
                gvMonitorNotificaciones.DataSource = dtObjCat;
                gvMonitorNotificaciones.DataBind();
                dtObjCat = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gvMonitorNotificaciones_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
        {
            try
            {
                iIdNotificacion = e.CommandArgs.CommandArgument.S().I();
                eActualizaStatus(sender, EventArgs.Empty);
                loadConsultaMonitorNotificaciones();
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvMonitorCambios_RowCommand", "Aviso");
            }
        }
        
        protected void gvCambios_BeforePerformDataSelect(object sender, EventArgs e)
        {
            try
            {
                ASPxGridView gridDetalle = sender as ASPxGridView;
                iIdCambio = gridDetalle.GetMasterRowKeyValue().S().I();
                loadConsultaMonitorNotificacionesDetalle();
                DataTable dt = (DataTable)ViewState["CargaDetalle"];
                if (dt.Rows[0]["Tipo"].ToString() == "1")
                {
                    gridDetalle.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvCambios_BeforePerformDataSelect", "Aviso");
            }
        }

        protected void gvCambios2_BeforePerformDataSelect(object sender, EventArgs e)
        {
            try
            {
                ASPxGridView gridDetalle = sender as ASPxGridView;
                iIdCambio = gridDetalle.GetMasterRowKeyValue().S().I();
                loadConsultaMonitorNotificacionesDetalle();
                DataTable dt = (DataTable)ViewState["CargaDetalle"];
                if (dt.Rows[0]["Tipo"].ToString() == "2")
                {
                    gridDetalle.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvCambios_BeforePerformDataSelect", "Aviso");
            }
        }

        protected void gvCambios3_BeforePerformDataSelect(object sender, EventArgs e)
        {
            try
            {
                ASPxGridView gridDetalle = sender as ASPxGridView;
                iIdCambio = gridDetalle.GetMasterRowKeyValue().S().I();
                loadConsultaMonitorNotificacionesDetalle();
                DataTable dt = (DataTable)ViewState["CargaDetalle"];
                if (dt.Rows[0]["Tipo"].ToString() == "3")
                {
                    gridDetalle.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvCambios_BeforePerformDataSelect", "Aviso");
            }
        }

        public void loadConsultaMonitorNotificacionesDetalle()
        {
            try
            {
                if (eSearchDetalle != null)
                    eSearchDetalle(null, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CargaConsultaCambiosDetalle(DataTable dtObjCat)
        {
            try
            {
                ViewState["CargaDetalle"] = null;
                ViewState["CargaDetalle"] = dtObjCat;
                dtObjCat = null;
            }
            catch (Exception ex)
            {
                throw ex;
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
        #endregion 

        #region METODOS

        public void LlenaGrid(DataTable dtObjCat)
        {
            try
            {
                gvMonitorNotificaciones.DataSource = dtObjCat;
                gvMonitorNotificaciones.DataBind();
                ViewState["gvMonitorNotificaciones"] = dtObjCat;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void RecargaGrid()
        {
            try 
            {
                if (ViewState["gvMonitorNotificaciones"] != null)
                {
                    gvMonitorNotificaciones.DataSource = (DataTable)ViewState["gvMonitorNotificaciones"];
                    gvMonitorNotificaciones.DataBind();
                }
            }
            catch (Exception x) { throw x; }
        }
        #endregion

        #region  VARIABLES
        private const string sClase = "MonitorCambios.aspx.cs";
        private const string sPagina = "MonitorCambios.aspx";

        MonitorCambios_Presenter oPresenter;

        public event EventHandler eActualizaStatus;

        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eSearchDetalle;

        public void loadConsultaMonitorNotificaciones()
        {
            try
            {
                if (eSearchObj != null)
                    eSearchObj(null, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object[] oArrActualizaStatus
        {
            get
            {
                return new object[] { 
                    "@Id",iIdNotificacion,
                    "@Usuario",((UserIdentity)Session["UserIdentity"]).sUsuario.S()
                };
            }
        }

        public int iIdCambio
        {
            get { return ViewState["idCambio"].S().I(); }
            set { ViewState["idCambio"] = value; }
        }

        public int iIdNotificacion
        {
            get { return ViewState["idNotificacion"].S().I(); }
            set { ViewState["idNotificacion"] = value; }
        }
        
        public object[] oArrConsultaCambiosDetalle
        {
            get
            {
                return new object[] { 
                    "@IdCambio",iIdCambio
                };
            }
        }
    }

        

        #endregion               
}