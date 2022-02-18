using ALE_MexJet.DomainModel;
using ALE_MexJet.Interfaces;
using ALE_MexJet.Presenter;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NucleoBase.Core;
using System.Reflection;
using ALE_MexJet.Clases;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using DevExpress.Web;

namespace ALE_MexJet.Views.Consultas
{
    public partial class frmResumenContrato : System.Web.UI.Page, IViewResumenContrato
    {
        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            oPresenter = new ResumenContrato_Presenter(this, new DBResumenContrato());

            if (!IsPostBack)
            {
                mpeMensaje.OkButtonPressed += mpeMensaje_OkButtonPressed;

                if (eSearchObj != null)
                    eSearchObj(sender, e);
            }
        }

        protected void mpeMensaje_OkButtonPressed(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "OcultaError", "OcultaError();", true);
        }
        protected void upaReport_Unload(object sender, EventArgs e)
        {
            MethodInfo methodInfo = typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Where(i => i.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel")).First();
            methodInfo.Invoke(ScriptManager.GetCurrent(Page),
                new object[] { sender as UpdatePanel });
        }
        protected void ddlCiente_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (eSearchContracts != null)
                    eSearchContracts(sender, e);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "ddlCiente_SelectedIndexChanged", "Aviso");
            }
        }
        protected void btnExcel_Click(object sender, EventArgs e)
        {
            if (eSaveObj != null)
                eSaveObj(sender, e);

            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("content-disposition", "attachment;filename=ResumenContrato_" + DateTime.Now.ToShortDateString() + ".xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            this.EnableViewState = false;

            StringWriter stringWrite = new StringWriter();
            HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            pnlReporteTab.RenderControl(htmlWrite);

            StringBuilder sCad = ParseaHTMLExcel(stringWrite);

            Response.Write(sCad.ToString());
            Response.End();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }

        protected void btnGenerar_Click(object sender, EventArgs e)
        {
            try
            {
                if (eObjSelected != null)
                    eObjSelected(sender, e);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnGenerar_Click", "Aviso");
            }
        }

        protected void lkbTraspasos_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton txt = (LinkButton)sender;
                GridViewRow Row = (GridViewRow)txt.NamingContainer;

                iIdResumen = dtResumen.Rows[Row.RowIndex]["IdResumen"].S().I();

                if (eSearchTraspasos != null)
                    eSearchTraspasos(sender, e);

                ppTransferencias.ShowOnPageLoad = true;
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "lkbTraspasos_Click", "Aviso");
            }
        }

        protected void btnPDF2_Click(object sender, EventArgs e)
        {
            if (eSaveObj != null)
                eSaveObj(sender, e);

            if (eSearchReport != null)
                eSearchReport(sender, e);

            if (dsResumenPres != null && dsResumenPres.Tables.Count > 0)
            {
                ReportDocument rd = new ReportDocument();

                string strPath = string.Empty;
                strPath = Server.MapPath("CristalReport\\ResumenContratoRPT.rpt");
                rd.Load(strPath);

                if (dsResumenPres.Tables[0].Rows.Count > 0)
                {
                    rd.Database.Tables["DtEncabezado"].SetDataSource(dsResumenPres.Tables[0]);
                    rd.Database.Tables["DtDescuentos"].SetDataSource(dsResumenPres.Tables[1]);
                    rd.Database.Tables["DtTarifas"].SetDataSource(dsResumenPres.Tables[2]);
                    rd.Database.Tables["DtResumenContrato"].SetDataSource(dsResumenPres.Tables[3]);
                    rd.Database.Tables["DtResumen"].SetDataSource(dsResumenPres.Tables[4]);
                    rd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ResumenContrato_" + ddlContrato.Text);

                }
            }
        }

        protected void gvResumen_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    ASPxTextBox txtAnualidades = (ASPxTextBox)e.Row.FindControl("txtAnualidades");
                    ASPxTextBox txtNFactura = (ASPxTextBox)e.Row.FindControl("txtNoFactura");
                    if (txtAnualidades != null && txtNFactura != null)
                    {
                        txtAnualidades.Text = dtResumen.Rows[e.Row.RowIndex]["Anualidades"].S();
                        txtNFactura.Text = dtResumen.Rows[e.Row.RowIndex]["NoFactura"].S();
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region METODOS

        public void LoadClientes(DataTable dtClientes)
        {
            try
            {
                ddlCiente.DataSource = dtClientes;
                ddlCiente.ValueField = "IdCliente";
                ddlCiente.TextField = "CodigoCliente";
                ddlCiente.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LoadContracts(DataTable dtContratos)
        {
            try
            {
                ddlContrato.DataSource = dtContratos;
                ddlContrato.ValueField = "IdContrato";
                ddlContrato.TextField = "ClaveContrato";
                ddlContrato.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LoadReport(DataSet ds)
        {
            try
            {
                lblClienteResp.Text = ds.Tables[0].Rows[0].S("ClaveCliente");
                lblRazonSocial.Text = ds.Tables[0].Rows[0].S("RazonSocial");
                lblContratoResp.Text = ds.Tables[0].Rows[0].S("Paquete");
                lblBaseResp.Text = ds.Tables[0].Rows[0].S("Base");
                lblEquipoResp.Text = ds.Tables[0].Rows[0].S("Equipo");

                gvDatosContrato.DataSource = ds.Tables[1];
                gvDatosContrato.DataBind();

                gvTarifas.DataSource = ds.Tables[2];
                gvTarifas.DataBind();

                dtResumen = ds.Tables[3].Copy();
                gvResumen.DataSource = dtResumen;
                gvResumen.DataBind();

                gvImporteContrato.DataSource = ds.Tables[4];
                gvImporteContrato.DataBind();

                pnlReporteTab.Visible = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LoadTransferenciasPorPeriodo(DataTable dtTras)
        {
            try
            {
                gvTransferencias.DataSource = dtTras;
                gvTransferencias.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ObtieneResumenContrato()
        {
            try
            {
                foreach (GridViewRow row in gvResumen.Rows)
                {
                    ASPxTextBox txtA = (ASPxTextBox)row.FindControl("txtAnualidades");
                    ASPxTextBox txtF = (ASPxTextBox)row.FindControl("txtNoFactura");

                    if (txtA != null && txtF.Text != null)
                    {
                        dtResumen.Rows[row.RowIndex]["Anualidades"] = txtA.Text.S().D();
                        dtResumen.Rows[row.RowIndex]["NoFactura"] = txtF.Text.S().D();
                    }
                }

                return dtResumen;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private StringBuilder ParseaHTMLExcel(StringWriter ExcelHTML)
        {
            StringBuilder Resultado = new StringBuilder();

            try
            {
                string CadenaReemplazar = string.Empty;
                string textoRemplazo = string.Empty;

                string ExcelHTMLFinal = ExcelHTML.ToString();

                CadenaReemplazar = "../../img/mexjet_p.png";
                textoRemplazo = Server.MapPath("../../img/mexjet_p.png");
                ExcelHTMLFinal = ExcelHTMLFinal.Replace(CadenaReemplazar, textoRemplazo);

                CadenaReemplazar = "../../img/colors/blue/logo-ale.png";
                textoRemplazo = Server.MapPath("../../img/colors/blue/logo-ale.png");
                ExcelHTMLFinal = ExcelHTMLFinal.Replace(CadenaReemplazar, textoRemplazo);

                string CadenaLink = string.Empty;
                string CadenaTextBox = string.Empty;

                do
                {
                    CadenaLink = ObtenerCadenaLink(ExcelHTMLFinal);

                    if (!string.IsNullOrEmpty(CadenaLink))
                    {

                        CadenaReemplazar = CadenaLink;
                        textoRemplazo = ObtenerValorCadenaLink(CadenaLink);
                        ExcelHTMLFinal = ExcelHTMLFinal.Replace(CadenaReemplazar, textoRemplazo);

                        CadenaTextBox = ObtenerCadenaTextBox(ExcelHTMLFinal);

                        CadenaReemplazar = CadenaTextBox;
                        textoRemplazo = ObtenerValorCadenaTextBox(CadenaTextBox);
                        ExcelHTMLFinal = ExcelHTMLFinal.Replace(CadenaReemplazar, textoRemplazo);

                        CadenaTextBox = ObtenerCadenaTextBox(ExcelHTMLFinal);

                        CadenaReemplazar = CadenaTextBox;
                        textoRemplazo = ObtenerValorCadenaTextBox(CadenaTextBox);
                        ExcelHTMLFinal = ExcelHTMLFinal.Replace(CadenaReemplazar, textoRemplazo);


                    }

                } while (!string.IsNullOrEmpty(CadenaLink));

                return Resultado.Append(ExcelHTMLFinal);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private string ObtenerCadenaLink(string ExcelHTML)
        {
            string Resultado = string.Empty;

            try
            {
                string searchString = "<a id=";
                int startIndex = ExcelHTML.IndexOf(searchString);

                if (startIndex != -1)
                {
                    searchString = "</a>";
                    int endIndex = ExcelHTML.IndexOf(searchString);

                    Resultado = ExcelHTML.Substring(startIndex, endIndex + searchString.Length - startIndex);
                }

                return Resultado;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private string ObtenerValorCadenaLink(string CadenaLink)
        {
            string Resultado = string.Empty;

            try
            {
                string searchString = "<a id=";
                int startIndex = CadenaLink.IndexOf(searchString);
                searchString = ";\">";
                int endIndex = CadenaLink.LastIndexOf(searchString);

                string Valor = CadenaLink.Substring(endIndex + searchString.Length);

                Valor = Valor.Substring(0, Valor.Length - 4);

                return Resultado = Valor;

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        private string ObtenerCadenaTextBox(string ExcelHTML)
        {
            string Resultado = string.Empty;

            try
            {
                string searchString = "<table class=\"dxeTextBoxSys";
                int startIndex = ExcelHTML.IndexOf(searchString);

                if (startIndex != -1)
                {
                    searchString = "//-->\n</script>";
                    int endIndex = ExcelHTML.IndexOf(searchString);

                    Resultado = ExcelHTML.Substring(startIndex, endIndex + searchString.Length - startIndex);
                }

                return Resultado;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private string ObtenerValorCadenaTextBox(string CadenaTextBox)
        {
            string Resultado = string.Empty;

            try
            {
                string searchString = "<table class=\"dxeTextBoxSys";
                int startIndex = CadenaTextBox.IndexOf(searchString);
                searchString = "value=\"";
                int endIndex = CadenaTextBox.LastIndexOf(searchString);

                string Valor = CadenaTextBox.Substring(endIndex + searchString.Length);

                searchString = "\" type=\"text\"";
                startIndex = Valor.IndexOf(searchString);

                Valor = Valor.Substring(0, startIndex);

                return Resultado = Valor;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        #endregion

        #region VARIABLES Y PROPIEDADES

        private const string sClase = "frmResumenContrato.aspx.cs";
        private const string sPagina = "frmResumenContrato.aspx";

        ResumenContrato_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eSearchContracts;
        public event EventHandler eSearchTraspasos;
        public event EventHandler eSearchReport;

        public object[] oArrFiltros
        {
            get
            {

                return new object[]{
                                        "@IdContrato",  ddlContrato.SelectedItem.Value.S().I()
                                    };
            }

        }
        public int iIdContrato
        {
            get
            {
                return ddlContrato.SelectedItem.Value.S().I();
            }

        }
        public int iIdCliente
        {
            get
            {
                return ddlCiente.SelectedItem.Value.S().I();
            }

        }
        public DataTable dtResumen
        {
            get { return (DataTable)ViewState["VSResumen"]; }
            set { ViewState["VSResumen"] = value; }
        }
        public DataSet dsResumenPres
        {
            get { return (DataSet)ViewState["VSdsResumen"]; }
            set { ViewState["VSdsResumen"] = value; }
        }
        public int iIdResumen
        {
            get { return (int)ViewState["VSiIdResumen"]; }
            set { ViewState["VSiIdResumen"] = value; }
        }
        #endregion



    }
}