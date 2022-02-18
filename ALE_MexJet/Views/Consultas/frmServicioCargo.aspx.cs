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
using System.Collections.Specialized;

using System;  
using System.IO;  
using System.Data;  
using System.Threading;  
using System.Data.SqlClient;  
using System.Configuration;  
using System.Text;  
using System.Web.UI.WebControls;  
using System.Web.UI.HtmlControls;
using System.Web.UI;

using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.xml;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace ALE_MexJet.Views.Consultas
{
    public partial class frmServicioCargo : System.Web.UI.Page, IViewConsultaServicioCargo
    {

        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                oPresenter = new ServicioCargo_Presenter(this, new DBServicioCargo());
                CargaGrv();

                if (!IsPostBack)
                {
                    if (eSearchObj != null)
                        eSearchObj(null, null);

                    upGv.Visible = false;
                }
            } 
            catch (Exception ex)  {Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnBusqueda_Click", "Aviso");}
        }
        protected void ddlClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            { 
                if (eObjContrato != null)
                    eObjContrato(null, null);
            }
            catch (Exception ex) { Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "ddlClientes_SelectedIndexChanged", "Aviso"); }
        }
        protected void btnBusqueda_Click(object sender, EventArgs e)
        {
            try 
            {
                hfCliente["hfCliente"] = ddlClientes.SelectedItem == null ? "" : ddlClientes.SelectedItem.Text;
                hfContrato["hfContrato"] = ddlContrato.SelectedItem == null ? "" : ddlContrato.SelectedItem.Text;
                hfFechaInicial["hfFechaInicial"] = dFechaIni.Text;
                hfFechaFinal["hfFechaFinal"] = dFechaFin.Text;
                
                if (eObjServico != null)
                    eObjServico(null, null);


                lblPer.Text = "Periodo " + dFechaIni.Text + " a " + dFechaFin.Text;
                lblFechaRep.Text = "Fecha: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
                lblClienteR.Text = "Cliente: " + sRazonSocial;
                lblContratoR.Text = "Contrato: " + ddlContrato.SelectedItem.Text;

                DataSet ds = (DataSet)ViewState["gvServicio"];
                gvServiciosC.DataSource = ds.Tables[0];
                gvServiciosC.DataBind();

                upGv.Visible = true;
                //string _ippuertoserver = Request.Url.Authority;

                //imgMexJet.ImageUrl = @"http://" + _ippuertoserver.S() + "/img/mexjet_p.png' width='150' height='80'/>";
                //imgAle.ImageUrl = @"http://" + _ippuertoserver.S() + "/img/colors/blue/logo-ale.png' width='150' height='80'/>";
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
                //ExportExcel();

                //Response.Clear();
                //Response.Buffer = true;
                //Response.ContentType = "application/vnd.ms-excel";
                //Response.AddHeader("content-disposition", "attachment;filename=ServiciosCargo_" + ddlContrato.SelectedItem.Text + ".xls");
                //Response.Charset = "UTF-8";
                //Response.ContentEncoding = Encoding.Default;
                //this.EnableViewState = false;

                //StringWriter stringWrite = new StringWriter();
                //HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
                //pnlServiciosC.RenderControl(htmlWrite);
                //Response.Write(stringWrite.ToString());
                //Response.End();


                string _ippuertoserver = Request.Url.Authority;
                int x = gvServiciosC.Columns.Count;

                int iColspan = 6;
                int iCol = x - iColspan;

                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.ClearHeaders();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "application/ms-excel";
                HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//ES"">");
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=Servicio Cargo.xls");
                HttpContext.Current.Response.Charset = "utf-8";
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1252");

                string imageMexJetpath = @"<img src='http://" + _ippuertoserver.S() + "/img/mexjet_p.png' width='150' height='80'/>";
                //string imageAlePath = @"<img src='http://" + _ippuertoserver.S() + "/img/logo_100px.jpg' width='110' height='100'/>";
                string imageAlePath = @"<img src='http://" + _ippuertoserver.S() + "/img/colors/blue/logo-ale.png' width='150' height='80'/>";

                HttpContext.Current.Response.Write(@"<table>
                                                    <tr>
                                                        <td style=' text-align: center; vertical-align: middle'>" + imageMexJetpath + " </td>" +
                                                            "<td style=' text-align: center; font-size: x-large; vertical-align: middle' colspan='" + (x - 2).S() + "'>" +
                                                                "Aerolineas Ejecutivas S.A. de C.V." +
                                                            "</td>" +
                                                            " <td style=' text-align: center; vertical-align: middle'>" + imageAlePath + " </td>" +
                                                        "</tr>" +
                                                        "<tr>" +
                                                            "<td></td>" +
                                                            "<td style='text-align: center; font-size: large; vertical-align: middle' colspan='" + (x - 2).S() + "'>" +
                                                                "Reporte de Servicios Cargo </td>" +
                                                            "<td></td>" +
                                                        "</tr>" +
                                                        "<tr>" +
                                                            "<td></td>" +
                                                            "<td style='text-align: center; vertical-align: middle' colspan='" + (x - 2).S() + "'>" +
                                                                "Periodo " + hfFechaInicial["hfFechaInicial"].S() + " a " + hfFechaFinal["hfFechaFinal"].S() + " </td>" +
                                                            "<td></td>" +
                                                        "</tr>" +
                    //                                                    "<tr>" +
                    //                                                        "<td></td>" +
                    //                                                        "<td style='text-align: center; height:'15px' vertical-align: middle' colspan='" + x.S() + "'></td>" +
                    //                                                        "<td></td>" +
                    //                                                    "</tr>" +
                                                        "<tr>" +
                                                            "<td colspan='3' style='text-align: left;' >" +
                                                                "Cliente: " + sRazonSocial +
                                                            "</td>" +
                    //"<td style='text-align: center; vertical-align: middle' colspan='" + iCol.S() + "'>" + "</td>" +
                    //"<td></td>" +
                                                        "</tr>" +
                                                        "<tr>" +
                                                            "<td colspan='3' style='text-align: left;'>" +
                                                                "Contrato: " + ddlContrato.SelectedItem.Text +
                                                            "</td>" +
                    // "<td style='text-align: center; vertical-align: middle' colspan='" + iCol.S() + "'>" + "</td>" +
                    // "<td style='text-align: right>" +
                    ////"Fecha: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() +
                                                            "</td>" +
                                                        "</tr>" +

                                                        "<tr>" +
                                                            "<td></td>" +
                                                            "<td style='text-align: center; height:'15px' vertical-align: middle' colspan='" + x.S() + "'></td>" +
                                                            "<td></td>" +
                                                        "</tr>" +
                                                    "</table>");


                StringBuilder sb = new StringBuilder();
                StringWriter tw = new StringWriter(sb);
                HtmlTextWriter hw = new HtmlTextWriter(tw);
                gvServiciosC.RenderControl(hw);
                var html = sb.ToString();

                HttpContext.Current.Response.Write(sb.ToString());
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
                HttpContext.Current.Response.Clear();
            }
            catch (Exception x) { throw x; }

        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }
        protected void UpdatePanel1_Unload(object sender, EventArgs e)
        {
                MethodInfo methodInfo = typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Where(i => i.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel")).First();
                methodInfo.Invoke(ScriptManager.GetCurrent(Page),
                new object[] { sender as UpdatePanel });


        }
        protected void gvServiciosC_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            try
            {
                DataSet ds = (DataSet)ViewState["gvServicio"];

                if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.Footer)
                {

                    e.Row.Style.Add("border-top", "solid");
                    //e.Row.Style.Add("border-bottom", "solid");
                    e.Row.Style.Add("border-width", "1px");
                    
                    DataRow row = ds.Tables[1].Rows[0];

                    e.Row.Cells[0].Text = row.S("TFECHA");
                    e.Row.Cells[1].Text = row.S("TREMISION");
                    e.Row.Cells[2].Text = row.S("TMATRICULA");
                    e.Row.Cells[3].Text = row.S("TRUTA");
                    e.Row.Cells[4].Text = row.S("TDSM");
                    e.Row.Cells[5].Text = row.S("TMIGRATORIO");
                    e.Row.Cells[6].Text = row.S("TTUAINT");
                    e.Row.Cells[7].Text = row.S("TTUANAC");
                    e.Row.Cells[8].Text = row.S("TAPHIS");
                    e.Row.Cells[9].Text = row.S("TSENEAM");
                    e.Row.Cells[10].Text = row.S("TCOMISARIATO");
                    e.Row.Cells[11].Text = row.S("TATERRIZAJE");
                    e.Row.Cells[12].Text = row.S("TOTROS");
                    e.Row.Cells[13].Text = row.S("TINTEGRACION");
                    e.Row.Cells[14].Text = row.S("TSUBTOTAL");
                    e.Row.Cells[15].Text = row.S("TIVA");
                    e.Row.Cells[16].Text = row.S("TTOTAL");
                }

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region METODOS
        public void CargaGrid(DataSet ds)
        {
            try
            {
                ViewState["gvServicio"] = ds;
            }
            catch (Exception x) 
            { 
                throw x;
            }
        }
        public void CargaGrv()
        {
            try
            {
                DataSet ds = (DataSet)ViewState["gvServicio"];
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    //gvServicio.DataSource = ds.Tables[0];
                    //gvServicio.DataBind();
                }
            }
            catch (Exception x) { throw x; }
        }
        public void CargaCliente(DataTable DT)
        {
            try
            {
                DT.Rows[0].Delete();
                ddlClientes.DataSource = DT;
                ddlClientes.TextField = "CodigoCliente";
                ddlClientes.ValueField = "IdCliente";
                ddlClientes.DataBind();
            }
            catch (Exception x) { throw x; }
        }
        public void CargaContrato(DataTable DT)
        {
            try
            {
                if (DT != null && DT.Rows.Count > 0)
                {
                    DT.Rows[0].Delete();
                    ddlContrato.DataSource = DT;
                    ddlContrato.ValueField = "IdContrato";
                    ddlContrato.TextField = "ClaveContrato";
                    ddlContrato.DataBind();
                }
            }
            catch (Exception x) { throw x; }
        }
        public void MostrarMensaje(string sMensaje, string sCaption)
        {
            mpeMensaje.ShowMessage(sMensaje, sCaption);
        }
        
        #endregion 

        #region VARIABLES Y PROPIEDADES
        ServicioCargo_Presenter oPresenter;
        private const string sClase = "frmServicioCargo.aspx.cs";
        private const string sPagina = "frmServicioCargo.aspx";

        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eObjContrato;
        public event EventHandler eObjServico;
        public object[] oArrContrato { 
            get {
                return new object []
                    {
                        "@IdCliente", ddlClientes.SelectedItem.Value
                    };
                } 
            }
        public object[] oArrServicio
        {
            get
            {
                return new object[]
                    {
                        "@Cliente", ddlClientes.SelectedItem.Value,
                        "@Contrato", ddlContrato.SelectedItem != null ? ddlContrato.SelectedItem .Value : 0,
                        "@FechaIni", dFechaIni.Text != string.Empty ? dFechaIni.Date : System.DateTime.Today,
                        "@FechaFin", dFechaFin.Text != string.Empty ? dFechaFin.Date : System.DateTime.Today
                    };
            }
        }

        public string sRazonSocial 
        {
            get
            {
                try
                {
                    DataTable dt = new DBCliente().DBSearchObj("@CodigoCliente", ddlClientes.SelectedItem.Text,
                                                                "@Nombre", string.Empty,
                                                                "@TipoCliente", string.Empty,
                                                                "@estatus", 1);
                    if (dt.Rows.Count > 0)
                        return dt.Rows[0]["Nombre"].S();
                    else
                        return string.Empty;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        #endregion

        protected void btnPDF_Click(object sender, System.EventArgs e)
        {
            CristalReport();
            
            #region CODIGO COMENTADO
            //            try
//            {

//                string _ippuertoserver = Request.Url.Authority;
//                int x = gvServiciosC.Columns.Count;

//                int iColspan = 6;
//                int iCol = x - iColspan;

//                string imageMexJetpath = @"<img src='http://" + _ippuertoserver.S() + "/img/mexjet_p.png' width='90' height='30'/>";
//                string imageAlePath = @"<img src='http://" + _ippuertoserver.S() + "/img/colors/blue/logo-ale.png' width='0' height='30'/>";

//                string CABECERO = @"<table>
//                                                    <tr>
//                                                        <td style=' text-align: center; vertical-align: middle'>" + imageMexJetpath + " </td>" +
//                                                            "<td style=' text-align: center; font-size: 10px; vertical-align: middle' colspan='" + (x - 2).S() + "'>" +
//                                                                "Aerolineas Ejecutivas S.A. de C.V." +
//                                                            "</td>" +
//                                                            " <td style=' text-align: left; vertical-align: middle'>" + imageAlePath + " </td>" +
//                                                        "</tr>" +
//                                                        "<tr>" +
//                                                            "<td></td>" +
//                                                            "<td style='text-align: center; font-size: 8px; vertical-align: middle' colspan='" + (x - 2).S() + "'>" +
//                                                                "Reporte de Servicios Cargo </td>" +
//                                                            "<td></td>" +
//                                                        "</tr>" +
//                                                        "<tr>" +
//                                                            "<td></td>" +
//                                                            "<td style='text-align: center; font-size: 8px; vertical-align: middle' colspan='" + (x - 2).S() + "'>" +
//                                                                "Periodo " + hfFechaInicial["hfFechaInicial"].S() + " a " + hfFechaFinal["hfFechaFinal"].S() + " </td>" +
//                                                            "<td></td>" +
//                                                        "</tr>" +
//                                                        "<tr>" +
//                                                            "<td colspan='" + x.S() + "' style='text-align: left; font-size: 8px;' >" +
//                                                                "Cliente: " + sRazonSocial +
//                                                            "</td>" +
//                                                        "</tr>" +
//                                                        "<tr>" +
//                                                            "<td colspan='" + x.S() + "' style='text-align: left; font-size: 8px;'>" +
//                                                                "Contrato: " + ddlContrato.SelectedItem.Text +
//                                                            "</td>" +
//                                                            "</td>" +
//                                                        "</tr>" +

//                                                        "<tr>" +
//                                                            "<td></td>" +
//                                                            "<td style='text-align: center; height:'15px' vertical-align: middle' colspan='" + x.S() + "'></td>" +
//                                                            "<td></td>" +
//                                                        "</tr>" +
//                                                    "</table>";

//                string attachment = "attachment; filename=ReporteServiciosCargo" + sRazonSocial + ".pdf";
//                Response.ClearContent();
//                Response.AddHeader("content-disposition", attachment);
//                Response.ContentType = "application/pdf";
//                StringWriter stw = new StringWriter();
//                HtmlTextWriter htextw = new HtmlTextWriter(stw);

//                gvServiciosC.HeaderStyle.Font.Size = 5;
//                gvServiciosC.FooterStyle.Font.Size = 5;
//                gvServiciosC.RowStyle.Font.Size = 5;
//                gvServiciosC.RenderControl(htextw);
//                iTextSharp.text.Document document = new iTextSharp.text.Document();

//                document = new iTextSharp.text.Document(PageSize.A4.Rotate());
//                PdfWriter.GetInstance(document, Response.OutputStream);
//                document.Open();

//                CABECERO = CABECERO + stw.ToString();

//                StringReader str = new StringReader(CABECERO);
//                HTMLWorker htmlworker = new HTMLWorker(document);
//                htmlworker.Parse(str);

//                document.Close();
//                Response.Write(document);

//            }
            //            catch (Exception x) { throw x; }
            #endregion
        }

        protected void CristalReport()
        {
            ReportDocument rd = new ReportDocument();

            string strPath = string.Empty;
            strPath = Server.MapPath("Views\\Consultas\\CristalReport\\ServicioCargo.rpt");
            strPath = strPath.Replace("\\Views\\Consultas\\Views\\Consultas", "\\Views\\Consultas");
            rd.Load(strPath);

            DataTable DTServicioCargo = ((DataSet)ViewState["gvServicio"]).Tables[0];
            DataTable DTServicioCargoTotal = ((DataSet)ViewState["gvServicio"]).Tables[1];
            DataTable DTServicioCargoCABECERO = ((DataSet)ViewState["gvServicio"]).Tables[2];

            if (DTServicioCargo.Rows.Count > 0)
            {
                //rd.SetDataSource(DTServicioCargo,0);
                // rd.SetDataSource(DTServicioCargoTotal);
                //  rd.SetDataSource((DataSet)ViewState["gvServicio"]);
                //rd.SetDataSource = (DataSet)ViewState["gvServicio"];
                rd.Database.Tables["Detalle"].SetDataSource(DTServicioCargo);
                rd.Database.Tables["TOTAL"].SetDataSource(DTServicioCargoTotal);
                rd.Database.Tables["CABEERO"].SetDataSource(DTServicioCargoCABECERO);
                rd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "ServiciosCargo" + DTServicioCargoCABECERO.Rows[0][1].S());
            }
        }

    }
}