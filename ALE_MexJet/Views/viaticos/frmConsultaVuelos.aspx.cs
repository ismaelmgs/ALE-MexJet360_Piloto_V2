using ALE_MexJet.DomainModel;
using ALE_MexJet.Interfaces;
using ALE_MexJet.Presenter;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ALE_MexJet.Views.viaticos
{
    public partial class frmConsultaVuelos : System.Web.UI.Page, IViewConsultaViaticosVuelos
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            oPresenter = new ConsultaViaticosVuelos_Presenter(this, new DBConsultaViaticosVuelos());
            gvVuelos.SettingsPager.Position = PagerPosition.TopAndBottom;
            gvVuelos.SettingsPager.ShowDisabledButtons = true;
            gvVuelos.SettingsPager.ShowNumericButtons = true;
            gvVuelos.SettingsPager.ShowSeparators = true;
            gvVuelos.SettingsPager.Summary.Visible = true;
            gvVuelos.SettingsPager.PageSizeItemSettings.Visible = true;
            gvVuelos.SettingsPager.PageSizeItemSettings.Position = PagerPageSizePosition.Right;
            gvVuelos.SettingsText.SearchPanelEditorNullText = "Ingresa la información a buscar:";
            gvVuelos.SettingsText.CustomizationDialogGroupingPageEmptyDragArea = "Arrastra el encabezado de la columna aqui para agrupar.";

            if (!IsPostBack) 
            {
                if(string.IsNullOrEmpty(date1.Text))
                    sFechaDesde = DateTime.Now.ToShortDateString();
                if(string.IsNullOrEmpty(date2.Text))
                    sFechaHasta = DateTime.Now.ToShortDateString();
                
                sParametro = txtParametro.Text;

                if (eSearchObj != null)
                    eSearchObj(sender, e);
            }
           
        }

        protected void btnConsultaVuelos_Click(object sender, EventArgs e)
        {
            try
            {
                sFechaDesde = date1.Text;
                sFechaHasta = date2.Text;
                sParametro = txtParametro.Text;

                if (eSearchObj != null)
                    eSearchObj(sender, e);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region MÉTODOS
        public void LoadViaticosVuelos(DataTable dt) 
        {
            try
            {
                dtVuelos = null;
                dtVuelos = dt;

                if (dtVuelos != null && dtVuelos.Rows.Count > 0)
                {
                    gvVuelos.DataSource = dtVuelos;
                    gvVuelos.DataBind();
                    pnlVuelos.Visible = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //public void CrearGridTest()
        //{
        //    try
        //    {
        //        DataTable tablaVuelos = new DataTable();
        //        tablaVuelos.Columns.Add("legid");
        //        tablaVuelos.Columns.Add("vuelo");
        //        tablaVuelos.Columns.Add("claveContrato");
        //        tablaVuelos.Columns.Add("Matricula");
        //        tablaVuelos.Columns.Add("Origen");
        //        tablaVuelos.Columns.Add("Destino");
        //        tablaVuelos.Columns.Add("PaisOrigen");
        //        tablaVuelos.Columns.Add("PaisDestino");
        //        tablaVuelos.Columns.Add("FechaHoraOrigen");
        //        tablaVuelos.Columns.Add("FechaHoraDestino");

        //        DataRow dr = tablaVuelos.NewRow();
        //        dr["legid"] = "1000";
        //        dr["vuelo"] = "2031";
        //        dr["claveContrato"] = "AVN001";
        //        dr["Matricula"] = "XA-SOR";
        //        dr["Origen"] = "MMTO";
        //        dr["Destino"] = "MMTY";
        //        dr["PaisOrigen"] = "MEXICO";
        //        dr["PaisDestino"] = "MEXICO";
        //        dr["FechaHoraOrigen"] = "27/07/2022";
        //        dr["FechaHoraDestino"] = "27/07/2022";
        //        tablaVuelos.Rows.Add(dr);

        //        dr = tablaVuelos.NewRow();
        //        dr["legid"] = "1001";
        //        dr["vuelo"] = "2032";
        //        dr["claveContrato"] = "AVN002";
        //        dr["Matricula"] = "XA-FTY";
        //        dr["Origen"] = "MMTY";
        //        dr["Destino"] = "MMTO";
        //        dr["PaisOrigen"] = "MEXICO";
        //        dr["PaisDestino"] = "MEXICO";
        //        dr["FechaHoraOrigen"] = "27/07/2022";
        //        dr["FechaHoraDestino"] = "27/07/2022";
        //        tablaVuelos.Rows.Add(dr);

        //        gvVuelos.DataSource = tablaVuelos;
        //        gvVuelos.DataBind();
        //        pnlVuelos.Visible = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        #endregion

        #region PROPIEDADES Y VARIABLES
        ConsultaViaticosVuelos_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;

        public DataTable dtVuelos
        {
            get { return (DataTable)ViewState["VSdtVuelos"]; }
            set { ViewState["VSdtVuelos"] = value; }
        }
        public string sParametro
        {
            get { return (string)ViewState["VSParametro"]; }
            set { ViewState["VSParametro"] = value; }
        }
        public string sFechaDesde
        {
            get { return (string)ViewState["VSFechaDesde"]; }
            set { ViewState["VSFechaDesde"] = value; }
        }
        public string sFechaHasta
        {
            get { return (string)ViewState["VSFechaHasta"]; }
            set { ViewState["VSFechaHasta"] = value; }
        }
        #endregion
    }
}