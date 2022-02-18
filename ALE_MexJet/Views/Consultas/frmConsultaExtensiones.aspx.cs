using ALE_MexJet.DomainModel;
using ALE_MexJet.Interfaces;
using ALE_MexJet.Presenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NucleoBase.Core;
using System.Data;
using System.Reflection;
using DevExpress.Web;
using System.Text;

namespace ALE_MexJet.Views.Consultas
{
    public partial class frmConsultaExtensiones : System.Web.UI.Page, IViewConsultaExtension
    {
        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            oPresenter = new ConsultaExtension_Presenter(this, new DBConsultaExtension());
            if (!IsPostBack)
            {

            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (eSearchObj != null)
                eSearchObj(sender, e);
        }

        protected void upaPrincipal_Unload(object sender, EventArgs e)
        {
            MethodInfo methodInfo = typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Where(i => i.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel")).First();
            methodInfo.Invoke(ScriptManager.GetCurrent(Page),
            new object[] { sender as UpdatePanel });
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            
        }

        protected void btnPrueba_Click(object sender, EventArgs e)
        {

        }

        protected void gvExtensiones_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            try
            {
                e.Cancel = true;
                gvExtensiones.CancelEdit();

                object value = e.EditingKeyValue;
                string ruta = "~/Views/CreditoCobranza/frmExtensionesHorario.aspx?Folio=" + value.S();

                if (IsCallback)
                    ASPxWebControl.RedirectOnCallback(ruta);
                else
                    Response.Redirect(ruta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region METODOS
        public void LoadExtensiones(DataTable dtExt)
        {
            try
            {
                gvExtensiones.DataSource = dtExt;
                gvExtensiones.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region VARIABLES Y PROPIEDADES
        ConsultaExtension_Presenter oPresenter;
        private const string sPagina = "frmReporteTabulado.aspx";
        private const string sClase = "frmReporteTabulado.aspx.cs";

        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;


        public object [] oArrFil
        {
            get
            {
                return new object[]
                {
                    "@TipoOperacion", ddlTipoOperacion.SelectedItem.Value.S().I(),
                    "@TipoSolicitud", ddlTipoSolicitud.SelectedItem.Value.S().I(),
                    "@IdEstatus", ddlEstatus.SelectedItem.Value.S().I(), 
                    "@FechaInicio", dtFechaDesde.Text.S() == string.Empty ? string.Empty : dtFechaDesde.Value.S().Dt().Year.S().PadLeft(4,'0') + "-" + dtFechaDesde.Value.S().Dt().Month.S().PadLeft(2,'0') + "-" + dtFechaDesde.Value.S().Dt().Day.S().PadLeft(2,'0'),
                    "@FechaFin", dtFechaHasta.Text.S() == string.Empty ? string.Empty : dtFechaHasta.Value.S().Dt().Year.S().PadLeft(4,'0') + "-" +   dtFechaHasta.Value.S().Dt().Month.S().PadLeft(2,'0') +"-"+ dtFechaHasta.Value.S().Dt().Day.S().PadLeft(2,'0')
                };
            }
        }
        #endregion

        

        
    }
}