using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.XtraPrinting;
using DevExpress.Export;
using DevExpress.Web;
using ALE_MexJet.Interfaces;
using ALE_MexJet.DomainModel;
using ALE_MexJet.Presenter;
using ALE_MexJet.Clases;

namespace ALE_MexJet.Views.Consultas
{
    public partial class frmSolicitudesPorCaso : System.Web.UI.Page, IViewSolicitudesPorCaso
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                oPresenter = new SolicitudesPorCaso_Presenter(this, new DBSolicitudesPorCaso());               
                gvSolicitudPorCaso.SettingsPager.Position = PagerPosition.TopAndBottom;
                gvSolicitudPorCaso.SettingsPager.ShowDisabledButtons = true;
                gvSolicitudPorCaso.SettingsPager.ShowNumericButtons = true;
                gvSolicitudPorCaso.SettingsPager.ShowSeparators = true;
                gvSolicitudPorCaso.SettingsPager.Summary.Visible = true;
                gvSolicitudPorCaso.SettingsPager.PageSizeItemSettings.Visible = true;
                gvSolicitudPorCaso.SettingsPager.PageSizeItemSettings.Position = PagerPageSizePosition.Right;
                gvSolicitudPorCaso.SettingsText.SearchPanelEditorNullText = "Ingrese la información a buscar";     

                ObtieneSolicitudesPorCaso();
                if (!IsPostBack)
                {
                    ObtieneTiposCaso();
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "Page_Load", "Aviso");
            }
        }
        
        protected void btnBusqueda_Click(object sender, EventArgs e)
        {
            try
            {
                ObtieneSolicitudesPorCaso();
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnBusqueda_Click", "Aviso");
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

        private const string sClase = "frmSolicitudesPorCaso.aspx.cs";
        private const string sPagina = "frmSolicitudesPorCaso.aspx";


        public void ObtieneSolicitudesPorCaso()
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
        public void ObtieneTiposCaso()
        {
            try
            {
                if (eSearchTiposCaso != null)
                    eSearchTiposCaso(null, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LoadSolicitudesPorCaso(DataTable dtObjSolicitudes)
        {
            try
            {
                gvSolicitudPorCaso.DataSource = null;
                gvSolicitudPorCaso.DataSource = dtObjSolicitudes;
                gvSolicitudPorCaso.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LoadTiposCaso(DataTable dtObjTiposCaso)
        {
            try
            {
                cmbTipoCaso.DataSource = dtObjTiposCaso;
                cmbTipoCaso.ValueField = "idCaso";
                cmbTipoCaso.ValueType = typeof(string);
                cmbTipoCaso.TextField = "Descripcion";
                cmbTipoCaso.DataBind();

                // Agregamos el valor por default y lo activamos
                cmbTipoCaso.Items.Insert(0, new ListEditItem("Todos los casos"));
                cmbTipoCaso.Items[0].Selected = true;
            }
            catch (Exception ex)
            { 
                throw ex; 
            }
        }

        public object[] oArrFiltroSolicitudesPorCaso
        {        
            get
            {
                try
                {
                    string TipoCaso;
                    DateTime fechaInicio;
                    DateTime fechaFinal;
                    DateTime dHoy = DateTime.Today;

                    if (cmbTipoCaso.SelectedIndex == -1)
                    {
                        TipoCaso = "Todos los casos";
                    }
                    else
                    {
                        TipoCaso = cmbTipoCaso.SelectedItem.Text;
                    }

                    string fechaIni;
                    string fechaFin;
                    fechaIni = "01/01/1900";
                    fechaFin = "01/01/1900";

                    if (string.IsNullOrEmpty(dFechaIni.Text))
                        fechaInicio = Convert.ToDateTime(fechaIni);
                    else
                        fechaInicio = Convert.ToDateTime(dFechaIni.Text);

                    if (string.IsNullOrEmpty(dFechaFin.Text))
                        fechaFinal = Convert.ToDateTime(fechaFin);
                    else
                        fechaFinal = Convert.ToDateTime(dFechaFin.Text);

                    return new object[] {   
                                        "@FechaInicio", fechaInicio, 
                                        "@FechaFinal", fechaFinal,
                                        "@NumeroTrip", txtNumeroTrip.Text,
                                        "@Caso",TipoCaso 
                                      };
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }        
        }

        public object[] oArrFiltroTiposCaso
        {
            get 
            {
                try
                {
                    return new object[] { "@idTipoCaso", null };
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }  

        SolicitudesPorCaso_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eSearchTiposCaso;
                    
    }
}