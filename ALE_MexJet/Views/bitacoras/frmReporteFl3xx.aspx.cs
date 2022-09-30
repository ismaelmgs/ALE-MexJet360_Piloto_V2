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

namespace ALE_MexJet.Views.bitacoras
{
    public partial class frmReporteFl3xx : System.Web.UI.Page, IViewReporteFl3xx
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            oPresenter = new ReporteFl3xx_Presenter(this, new DBReporteFl3xx());
            gvFl3xx.SettingsPager.Position = PagerPosition.TopAndBottom;
            gvFl3xx.SettingsPager.ShowDisabledButtons = true;
            gvFl3xx.SettingsPager.ShowNumericButtons = true;
            gvFl3xx.SettingsPager.ShowSeparators = true;
            gvFl3xx.SettingsPager.Summary.Visible = true;
            gvFl3xx.SettingsPager.PageSizeItemSettings.Visible = true;
            gvFl3xx.SettingsPager.PageSizeItemSettings.Position = PagerPageSizePosition.Right;
            gvFl3xx.SettingsText.SearchPanelEditorNullText = "Ingresa la información a buscar:";
            gvFl3xx.Settings.ShowGroupPanel = false;

            ////Si envia error al registrar el boton de exportar excel, agrear estas dos lineas
            //ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            //scriptManager.RegisterPostBackControl(this.btnExportar);

            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(date1.Text))
                    sFechaDesde = DateTime.Now.ToShortDateString();
                if (string.IsNullOrEmpty(date2.Text))
                    sFechaHasta = DateTime.Now.ToShortDateString();

                if (eSearchObj != null)
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
                    string fileName = "ReporteFl3xx.xlsx";
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
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(date1.Text))
                    sFechaDesde = date1.Value.ToString();
                if (!string.IsNullOrEmpty(date2.Text))
                    sFechaHasta = date2.Value.ToString();

                if (eSearchObj != null)
                    eSearchObj(sender, e);
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
        public void LoadFl3xx(DataTable dt)
        {
            try
            {
                dtFl3xx = null;
                dtFl3xx = dt;

                if (dtFl3xx != null && dtFl3xx.Rows.Count > 0)
                {
                    gvFl3xx.DataSource = dtFl3xx;
                    gvFl3xx.DataBind();
                    pnlReporteFl3xx.Visible = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region PROPIEDADES Y VARIABLES
        ReporteFl3xx_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;

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
        public DataTable dtFl3xx
        {
            get { return (DataTable)ViewState["VSdtFl3xx"]; }
            set { ViewState["VSdtFl3xx"] = value; }
        }
        #endregion


    }
}