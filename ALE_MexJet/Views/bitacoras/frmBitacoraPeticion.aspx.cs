using ALE_MexJet.Interfaces;
using ALE_MexJet.Presenter;
using ALE_MexJet.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Data;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;
using ClosedXML.Excel;
using ALE_MexJet.Objetos;
using ALE_MexJet.Clases;

namespace ALE_MexJet.Views.bitacoras
{
    public partial class frmBitacoraPeticion : System.Web.UI.Page, IViewBitacoraPeticion
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            oPresenter = new BitacoraPeticion_Presenter(this, new DBBitacoraPeticion());
            gvBitacora.SettingsPager.Position = PagerPosition.TopAndBottom;
            gvBitacora.SettingsPager.ShowDisabledButtons = true;
            gvBitacora.SettingsPager.ShowNumericButtons = true;
            gvBitacora.SettingsPager.ShowSeparators = true;
            gvBitacora.SettingsPager.Summary.Visible = true;
            gvBitacora.SettingsPager.PageSizeItemSettings.Visible = true;
            gvBitacora.SettingsPager.PageSizeItemSettings.Position = PagerPageSizePosition.Right;
            gvBitacora.SettingsText.SearchPanelEditorNullText = "Ingresa la información a buscar:";
            gvBitacora.Settings.ShowGroupPanel = false;

            ////Si envia error al registrar el boton de exportar excel, agrear estas dos lineas
            //ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            //scriptManager.RegisterPostBackControl(this.btnExportar);

            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(fechaInicio.Text))
                    sFechaDesde = DateTime.Now.ToShortDateString();
                if (string.IsNullOrEmpty(fechaFin.Text))
                    sFechaHasta = DateTime.Now.ToShortDateString();
                if (string.IsNullOrEmpty(tripNumber.Text))
                    stripNumber = tripNumber.Text;

                if (eSearchObj != null && !string.IsNullOrEmpty(stripNumber))
                    eSearchObj(sender, e);
            }
        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtFl3xx.Rows.Count > 0)
                {
                    dtFl3xx.TableName = "Fl3xx";
                    dtFl3xx.Columns.Remove("flightId");
                    dtFl3xx.AcceptChanges();

                    DataTable dt = dtFl3xx;
                    //Name of File  
                    string fileName = "mientras.xls";
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        //Add DataTable in worksheet  
                        var hoja = wb.Worksheets.Add(dt);
                        hoja.ColumnsUsed().AdjustToContents(); //Ajuste de columnas
                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("content-disposition", "attachment;filename=" + fileName);

                        using (MemoryStream stream = new MemoryStream())
                        {
                            wb.SaveAs(stream);
                            Response.BinaryWrite(stream.ToArray());
                            Response.Flush();
                            //Response.End();
                            //Si marca error Response.End(); agregar las siguientes lineas
                            Response.SuppressContent = true;
                            HttpContext.Current.ApplicationInstance.CompleteRequest();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnProcesar_Click(object sender, EventArgs e)
        {
            if (eObjProcesar != null)
                eObjProcesar(sender, e);
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(fechaInicio.Text))
                    sFechaDesde = fechaInicio.Value.ToString();
                if (!string.IsNullOrEmpty(fechaFin.Text))
                    sFechaHasta = fechaFin.Value.ToString();
                if (!string.IsNullOrEmpty(tripNumber.Text))
                    stripNumber = tripNumber.Text;

                if (eSearchObj != null)
                    eSearchObj(sender, e);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gvBitacora_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int pageIndex = gvBitacora.PageIndex;
                gvBitacora.PageIndex = pageIndex;
                gvBitacora.DataSource = dtFl3xx;
                gvBitacora.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region MÉTODOS
        public void MostrarMensaje(string sMensaje, string sCaption)
        {
            //mpeMensaje.ShowMessage(sMensaje, sCaption);
            lbl.Text = sMensaje;
            ppAlert.ShowOnPageLoad = true;
        }

        public void LoadFl3xx(List<FlightsFlexx> lstflights)
        {
            try
            {
                flights = null;
                flights = lstflights;

                dtFl3xx = null;
                dtFl3xx = flights.ConvertListToDataTable();

                if (dtFl3xx != null && dtFl3xx.Rows.Count > 0)
                {
                    gvBitacora.DataSource = dtFl3xx;
                    gvBitacora.DataBind();
                    pnlReporteFl3xx.Visible = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void getPostflight(List<PostFlightFlexx> postFlights)
        {
            try
            {
                lstPostFlights = null;
                lstPostFlights = postFlights;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void vuelosProcesados(bool procesado)
        {
            dtFl3xx = null;
            flights = null;
            lstPostFlights = null;

            fechaInicio.Text = "";
            fechaFin.Text = "";
            tripNumber.Text = "";

            gvBitacora.DataSource = dtFl3xx;
            gvBitacora.DataBind();
            pnlReporteFl3xx.Visible = false;

            MostrarMensaje("Se porcesaron con exito los vuelos!!", "");
        }

        #endregion


        #region PROPIEDADES Y VARIABLES
        BitacoraPeticion_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eObjProcesar;

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
        public string stripNumber
        {
            get { return (string)ViewState["VStripNumber"]; }
            set { ViewState["VStripNumber"] = value; }
        }
        public DataTable dtFl3xx
        {
            get { return (DataTable)ViewState["VSdtFl3xx"]; }
            set { ViewState["VSdtFl3xx"] = value; }
        }
        public List<FlightsFlexx> flights
        {
            get { return (List<FlightsFlexx>)ViewState["VSflights"]; }
            set { ViewState["VSflights"] = value; }
        }
        public List<PostFlightFlexx> lstPostFlights
        {
            get { return (List<PostFlightFlexx>)ViewState["VSlstPostFlights"]; }
            set { ViewState["VSlstPostFlights"] = value; }
        }

        #endregion
    }
}