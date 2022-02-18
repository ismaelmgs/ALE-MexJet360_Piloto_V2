using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;
using NucleoBase.Core;
using ALE_MexJet.Clases;
using ALE_MexJet.Interfaces;
using ALE_MexJet.Presenter;
using ALE_MexJet.DomainModel;

namespace ALE_MexJet.Views.Consultas
{
    public partial class frmCtrlAeroTercerosFacturas : System.Web.UI.Page, IViewCtrlAeroTercerosFacturas
    {
        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                oPresenter = new CtrlAeroTercerosFacturas_Presenter(this, new DBCtrlAeroTercerosFacturas());
                gvReporteFactura.SettingsText.SearchPanelEditorNullText = "Ingrese la información a buscar";
                
                if (IsPostBack)
                    RecargaGrid();
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "Page_Load", "Aviso");
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
        protected void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                ExportExcel();
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnExcel_Click", "Aviso");
            }
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                ObtieneReporteFacturas();
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnBuscar_Click", "Aviso");
            }
        }
        #endregion

        #region METODOS
        public void LoadReporteFacturas(DataTable dtReporteFacturas)
        {
            try
            {
                gvReporteFactura.DataSource = null;
                gvReporteFactura.DataSource = dtReporteFacturas;
                gvReporteFactura.DataBind();

                ViewState["gvReporteFactura"] = dtReporteFacturas;
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
                if (ViewState["gvReporteFactura"] != null)
                {
                    gvReporteFactura.DataSource = (DataTable)ViewState["gvReporteFactura"];
                    gvReporteFactura.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ObtieneReporteFacturas()
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

        private void ExportExcel()
        {
            try
            {
                //gvReporteFactura.DataBind();
                DataTable table = (DataTable)gvReporteFactura.DataSource;
                if (table == null || table.Rows.Count == 0)
                    return;

                int columnscount = table.Columns.Count;
                int x = columnscount - 2;
                string _ippuertoserver = Utils.GetParametrosClave("53");

                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.ClearHeaders();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "application/ms-excel";
                HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=Reporte de rentas de terceros.xls");
                HttpContext.Current.Response.Charset = "utf-8";
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");

                HttpContext.Current.Response.Write(@"<table>    <tr>  
                                                                    <td> </td> 
                                                                    <td> </td>
                                                                    <td> </td>
                                                                    <td> </td> 
                                                                </tr>   
                                                                <tr>  
                                                                    <td> </td>  
                                                                    <td style=' text-align: center; vertical-align: middle'><img src='" + _ippuertoserver.S() + "/img/logoALE.png' height='100px' width='130px'/> </td>" +
                                                                    "<td style=' text-align: center; font-family:Candara; font-size: 22px; font-style:oblique; font-weight:bold; vertical-align: middle' colspan='" + x.S() + "'>&nbsp; AEROLÍNEAS EJECUTIVAS S.A. de C.V. </td>" +
                                                                    "<td> </td> " +
                                                               "</tr> " +
                                                               "<tr> " +
                                                                    "<td> </td>  " +
                                                                    "<td style=' text-align: center; vertical-align: middle'></td>" +
                                                                    "<td style=' text-align: center; font-family:Candara; font-size: 22px; font-style:oblique; font-weight:bold; vertical-align: middle' colspan='" + x.S() + "'>&nbsp; Aviones Rentados para ALE </td>" +
                                                                    "<td> </td>" +
                                                               "</tr>" +
                                                               "<tr>" +
                                                                    "<td> </td>" +
                                                                    "<td> </td>" +
                                                                    "<td style='text-align: center; font-size: large; vertical-align: middle' colspan='" + x.S() + "' style=' text-align: center; font-family:Candara; font-size: 14px; font-weight:bold; vertical-align: middle' colspan='" + x.S() + "'>&nbsp; Periodo " + dFechaIni.Value.S().Dt().ToShortDateString() + " a " + dFechaFin.Value.S().Dt().ToShortDateString() + " </td>" +
                                                                    "<td>  </td>" +

                                                               "</tr> " +
                                                               "<tr>" +
                                                                    "<td> </td>" +
                                                                    "<td> </td>" +
                                                                    "<td> </td>" +
                                                                    "<td> </td>" +
                                                                "</tr>" +
                                                                "<tr> " +
                                                                    "<td> </td>  " +
                                                                    "<td style=' text-align: center; vertical-align: middle'></td>" +
                                                                    "<td></td>" +
                                                                    "<td> </td>" +
                                                               "</tr>" +

                                                    "</table>");

                HttpContext.Current.Response.Write("<Table border='0' bgColor='#ffffff' borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; background:white;'>");
                HttpContext.Current.Response.Write("<hr style='background-color: #000000' />");


                HttpContext.Current.Response.Write("<TR>");
                HttpContext.Current.Response.Write("<td></td>");

                HttpContext.Current.Response.Write("<Td style='border-width: 1px 0px 1px 0px; color: #ffffff; background-color:#191970; font-weight:bold; font-size: 14px; vertical-align: middle; text-align: center;'>");
                HttpContext.Current.Response.Write("<B>");
                HttpContext.Current.Response.Write("Fecha de");
                HttpContext.Current.Response.Write("</B>");
                HttpContext.Current.Response.Write("</Td>");

                HttpContext.Current.Response.Write("<Td style='border-width: 1px 0px 1px 0px; color: #ffffff; background-color:#191970; font-weight:bold; font-size: 14px; vertical-align: middle; text-align: center;'>");
                HttpContext.Current.Response.Write("<B>");
                HttpContext.Current.Response.Write("Fecha de");
                HttpContext.Current.Response.Write("</B>");
                HttpContext.Current.Response.Write("</Td>");

                HttpContext.Current.Response.Write("<Td style='border-width: 1px 0px 1px 0px; color: #ffffff; background-color:#191970; font-weight:bold; font-size: 14px; vertical-align: middle; text-align: center;'>");
                HttpContext.Current.Response.Write("<B>");
                HttpContext.Current.Response.Write("");
                HttpContext.Current.Response.Write("</B>");
                HttpContext.Current.Response.Write("</Td>");

                HttpContext.Current.Response.Write("<Td style='border-width: 1px 0px 1px 0px; color: #ffffff; background-color:#191970; font-weight:bold; font-size: 14px; vertical-align: middle; text-align: center;'>");
                HttpContext.Current.Response.Write("<B>");
                HttpContext.Current.Response.Write("");
                HttpContext.Current.Response.Write("</B>");
                HttpContext.Current.Response.Write("</Td>");

                HttpContext.Current.Response.Write("<Td style='border-width: 1px 0px 1px 0px; color: #ffffff; background-color:#191970; font-weight:bold; font-size: 14px; vertical-align: middle; text-align: center;'>");
                HttpContext.Current.Response.Write("<B>");
                HttpContext.Current.Response.Write("");
                HttpContext.Current.Response.Write("</B>");
                HttpContext.Current.Response.Write("</Td>");

                HttpContext.Current.Response.Write("<Td style='border-width: 1px 0px 1px 0px; color: #ffffff; background-color:#191970; font-weight:bold; font-size: 14px; vertical-align: middle; text-align: center;'>");
                HttpContext.Current.Response.Write("<B>");
                HttpContext.Current.Response.Write("");
                HttpContext.Current.Response.Write("</B>");
                HttpContext.Current.Response.Write("</Td>");

                HttpContext.Current.Response.Write("<Td style='border-width: 1px 0px 1px 0px; color: #ffffff; background-color:#191970; font-weight:bold; font-size: 14px; vertical-align: middle; text-align: center;'>");
                HttpContext.Current.Response.Write("<B>");
                HttpContext.Current.Response.Write("");
                HttpContext.Current.Response.Write("</B>");
                HttpContext.Current.Response.Write("</Td>");

                HttpContext.Current.Response.Write("<Td style='border-width: 1px 0px 1px 0px; color: #ffffff; background-color:#191970; font-weight:bold; font-size: 14px; vertical-align: middle; text-align: center;'>");
                HttpContext.Current.Response.Write("<B>");
                HttpContext.Current.Response.Write("Tiempo de");
                HttpContext.Current.Response.Write("</B>");
                HttpContext.Current.Response.Write("</Td>");

                HttpContext.Current.Response.Write("<Td style='border-width: 1px 0px 1px 0px; color: #ffffff; background-color:#191970; font-weight:bold; font-size: 14px; vertical-align: middle; text-align: center;'>");
                HttpContext.Current.Response.Write("<B>");
                HttpContext.Current.Response.Write("Tiempo de");
                HttpContext.Current.Response.Write("</B>");
                HttpContext.Current.Response.Write("</Td>");

                HttpContext.Current.Response.Write("<Td style='border-width: 1px 0px 1px 0px; color: #ffffff; background-color:#191970; font-weight:bold; font-size: 14px; vertical-align: middle; text-align: center;'>");
                HttpContext.Current.Response.Write("<B>");
                HttpContext.Current.Response.Write("");
                HttpContext.Current.Response.Write("</B>");
                HttpContext.Current.Response.Write("</Td>");

                HttpContext.Current.Response.Write("<Td style='border-width: 1px 0px 1px 0px; color: #ffffff; background-color:#191970; font-weight:bold; font-size: 14px; vertical-align: middle; text-align: center;'>");
                HttpContext.Current.Response.Write("<B>");
                HttpContext.Current.Response.Write("");
                HttpContext.Current.Response.Write("</B>");
                HttpContext.Current.Response.Write("</Td>");

                HttpContext.Current.Response.Write("<Td colspan='2' style='border-width: 1px 0px 1px 0px; color: #ffffff; background-color:#191970; font-weight:bold; font-size: 14px; vertical-align: middle; text-align: center;'>");
                HttpContext.Current.Response.Write("<B>");
                HttpContext.Current.Response.Write("Facturado");
                HttpContext.Current.Response.Write("</B>");
                HttpContext.Current.Response.Write("</Td>");

                HttpContext.Current.Response.Write("<Td style='border-width: 1px 0px 1px 0px; color: #ffffff; background-color:#191970; font-weight:bold; font-size: 14px; vertical-align: middle; text-align: center;'>");
                HttpContext.Current.Response.Write("<B>");
                HttpContext.Current.Response.Write("Tipo de");
                HttpContext.Current.Response.Write("</B>");
                HttpContext.Current.Response.Write("</Td>");

                HttpContext.Current.Response.Write("<Td colspan='2' style='border-width: 1px 0px 1px 0px; color: #ffffff; background-color:#191970; font-weight:bold; font-size: 14px; vertical-align: middle; text-align: center;'>");
                HttpContext.Current.Response.Write("<B>");
                HttpContext.Current.Response.Write("Facturado ALE");
                HttpContext.Current.Response.Write("</B>");
                HttpContext.Current.Response.Write("</Td>");

                HttpContext.Current.Response.Write("<Td style='border-width: 1px 0px 1px 0px; color: #ffffff; background-color:#191970; font-weight:bold; font-size: 14px; vertical-align: middle; text-align: center;'>");
                HttpContext.Current.Response.Write("<B>");
                HttpContext.Current.Response.Write("Tipo de");
                HttpContext.Current.Response.Write("</B>");
                HttpContext.Current.Response.Write("</Td>");

                HttpContext.Current.Response.Write("<Td style='border-width: 1px 0px 1px 0px; color: #ffffff; background-color:#191970; font-weight:bold; font-size: 14px; vertical-align: middle; text-align: center;'>");
                HttpContext.Current.Response.Write("<B>");
                HttpContext.Current.Response.Write("Diferencia");
                HttpContext.Current.Response.Write("</B>");
                HttpContext.Current.Response.Write("</Td>");

                HttpContext.Current.Response.Write("<Td style='border-width: 1px 0px 1px 0px; color: #ffffff; background-color:#191970; font-weight:bold; font-size: 14px; vertical-align: middle; text-align: center;'>");
                HttpContext.Current.Response.Write("<B>");
                HttpContext.Current.Response.Write("Cliente");
                HttpContext.Current.Response.Write("</B>");
                HttpContext.Current.Response.Write("</Td>");

                HttpContext.Current.Response.Write("</TR>");

                HttpContext.Current.Response.Write("<TR>");
                HttpContext.Current.Response.Write("<td></td>");

                HttpContext.Current.Response.Write("<Td style='border-width: 1px 0px 1px 0px; color: #ffffff; background-color:#191970; font-weight:bold; font-size: 14px; vertical-align: middle; text-align: center;'>");
                HttpContext.Current.Response.Write("<B>");
                HttpContext.Current.Response.Write("vuelo");
                HttpContext.Current.Response.Write("</B>");
                HttpContext.Current.Response.Write("</Td>");

                HttpContext.Current.Response.Write("<Td style='border-width: 1px 0px 1px 0px; color: #ffffff; background-color:#191970; font-weight:bold; font-size: 14px; vertical-align: middle; text-align: center;'>");
                HttpContext.Current.Response.Write("<B>");
                HttpContext.Current.Response.Write("factura");
                HttpContext.Current.Response.Write("</B>");
                HttpContext.Current.Response.Write("</Td>");

                HttpContext.Current.Response.Write("<Td style='border-width: 1px 0px 1px 0px; color: #ffffff; background-color:#191970; font-weight:bold; font-size: 14px; vertical-align: middle; text-align: center;'>");
                HttpContext.Current.Response.Write("<B>");
                HttpContext.Current.Response.Write("Solicitud");
                HttpContext.Current.Response.Write("</B>");
                HttpContext.Current.Response.Write("</Td>");

                HttpContext.Current.Response.Write("<Td style='border-width: 1px 0px 1px 0px; color: #ffffff; background-color:#191970; font-weight:bold; font-size: 14px; vertical-align: middle; text-align: center;'>");
                HttpContext.Current.Response.Write("<B>");
                HttpContext.Current.Response.Write("Factura");
                HttpContext.Current.Response.Write("</B>");
                HttpContext.Current.Response.Write("</Td>");

                HttpContext.Current.Response.Write("<Td style='border-width: 1px 0px 1px 0px; color: #ffffff; background-color:#191970; font-weight:bold; font-size: 14px; vertical-align: middle; text-align: center;'>");
                HttpContext.Current.Response.Write("<B>");
                HttpContext.Current.Response.Write("Matricula");
                HttpContext.Current.Response.Write("</B>");
                HttpContext.Current.Response.Write("</Td>");

                HttpContext.Current.Response.Write("<Td style='border-width: 1px 0px 1px 0px; color: #ffffff; background-color:#191970; font-weight:bold; font-size: 14px; vertical-align: middle; text-align: center;'>");
                HttpContext.Current.Response.Write("<B>");
                HttpContext.Current.Response.Write("Equipo");
                HttpContext.Current.Response.Write("</B>");
                HttpContext.Current.Response.Write("</Td>");

                HttpContext.Current.Response.Write("<Td style='border-width: 1px 0px 1px 0px; color: #ffffff; background-color:#191970; font-weight:bold; font-size: 14px; vertical-align: middle; text-align: center;'>");
                HttpContext.Current.Response.Write("<B>");
                HttpContext.Current.Response.Write("Ruta");
                HttpContext.Current.Response.Write("</B>");
                HttpContext.Current.Response.Write("</Td>");

                HttpContext.Current.Response.Write("<Td style='border-width: 1px 0px 1px 0px; color: #ffffff; background-color:#191970; font-weight:bold; font-size: 14px; vertical-align: middle; text-align: center;'>");
                HttpContext.Current.Response.Write("<B>");
                HttpContext.Current.Response.Write("Vuelo");
                HttpContext.Current.Response.Write("</B>");
                HttpContext.Current.Response.Write("</Td>");

                HttpContext.Current.Response.Write("<Td style='border-width: 1px 0px 1px 0px; color: #ffffff; background-color:#191970; font-weight:bold; font-size: 14px; vertical-align: middle; text-align: center;'>");
                HttpContext.Current.Response.Write("<B>");
                HttpContext.Current.Response.Write("Vuelo Remisión");
                HttpContext.Current.Response.Write("</B>");
                HttpContext.Current.Response.Write("</Td>");

                HttpContext.Current.Response.Write("<Td style='border-width: 1px 0px 1px 0px; color: #ffffff; background-color:#191970; font-weight:bold; font-size: 14px; vertical-align: middle; text-align: center;'>");
                HttpContext.Current.Response.Write("<B>");
                HttpContext.Current.Response.Write("Pernocta");
                HttpContext.Current.Response.Write("</B>");
                HttpContext.Current.Response.Write("</Td>");

                HttpContext.Current.Response.Write("<Td style='border-width: 1px 0px 1px 0px; color: #ffffff; background-color:#191970; font-weight:bold; font-size: 14px; vertical-align: middle; text-align: center;'>");
                HttpContext.Current.Response.Write("<B>");
                HttpContext.Current.Response.Write("Proveedor");
                HttpContext.Current.Response.Write("</B>");
                HttpContext.Current.Response.Write("</Td>");

                HttpContext.Current.Response.Write("<Td style='border-width: 1px 0px 1px 0px; color: #ffffff; background-color:#191970; font-weight:bold; font-size: 14px; vertical-align: middle; text-align: center;'>");
                HttpContext.Current.Response.Write("<B>");
                HttpContext.Current.Response.Write("USD");
                HttpContext.Current.Response.Write("</B>");
                HttpContext.Current.Response.Write("</Td>");

                HttpContext.Current.Response.Write("<Td style='border-width: 1px 0px 1px 0px; color: #ffffff; background-color:#191970; font-weight:bold; font-size: 14px; vertical-align: middle; text-align: center;'>");
                HttpContext.Current.Response.Write("<B>");
                HttpContext.Current.Response.Write("MXN");
                HttpContext.Current.Response.Write("</B>");
                HttpContext.Current.Response.Write("</Td>");

                HttpContext.Current.Response.Write("<Td style='border-width: 1px 0px 1px 0px; color: #ffffff; background-color:#191970; font-weight:bold; font-size: 14px; vertical-align: middle; text-align: center;'>");
                HttpContext.Current.Response.Write("<B>");
                HttpContext.Current.Response.Write("Cambio");
                HttpContext.Current.Response.Write("</B>");
                HttpContext.Current.Response.Write("</Td>");

                HttpContext.Current.Response.Write("<Td style='border-width: 1px 0px 1px 0px; color: #ffffff; background-color:#191970; font-weight:bold; font-size: 14px; vertical-align: middle; text-align: center;'>");
                HttpContext.Current.Response.Write("<B>");
                HttpContext.Current.Response.Write("USD");
                HttpContext.Current.Response.Write("</B>");
                HttpContext.Current.Response.Write("</Td>");

                HttpContext.Current.Response.Write("<Td style='border-width: 1px 0px 1px 0px; color: #ffffff; background-color:#191970; font-weight:bold; font-size: 14px; vertical-align: middle; text-align: center;'>");
                HttpContext.Current.Response.Write("<B>");
                HttpContext.Current.Response.Write("MXN");
                HttpContext.Current.Response.Write("</B>");
                HttpContext.Current.Response.Write("</Td>");

                HttpContext.Current.Response.Write("<Td style='border-width: 1px 0px 1px 0px; color: #ffffff; background-color:#191970; font-weight:bold; font-size: 14px; vertical-align: middle; text-align: center;'>");
                HttpContext.Current.Response.Write("<B>");
                HttpContext.Current.Response.Write("Cambio");
                HttpContext.Current.Response.Write("</B>");
                HttpContext.Current.Response.Write("</Td>");

                HttpContext.Current.Response.Write("<Td style='border-width: 1px 0px 1px 0px; color: #ffffff; background-color:#191970; font-weight:bold; font-size: 14px; vertical-align: middle; text-align: center;'>");
                HttpContext.Current.Response.Write("<B>");
                HttpContext.Current.Response.Write("A cargo / A favor");
                HttpContext.Current.Response.Write("</B>");
                HttpContext.Current.Response.Write("</Td>");

                HttpContext.Current.Response.Write("<Td style='border-width: 1px 0px 1px 0px; color: #ffffff; background-color:#191970; font-weight:bold; font-size: 14px; vertical-align: middle; text-align: center;'>");
                HttpContext.Current.Response.Write("<B>");
                HttpContext.Current.Response.Write("Contrato");
                HttpContext.Current.Response.Write("</B>");
                HttpContext.Current.Response.Write("</Td>");

                HttpContext.Current.Response.Write("</TR>");

                foreach (DataRow row in table.Rows)
                {
                    HttpContext.Current.Response.Write("<TR>");
                    HttpContext.Current.Response.Write("<td></td>");
                    for (int i = 0; i < table.Columns.Count; i++)
                    {
                        if (i == 11 || i == 12 || i == 13 || i == 14 || i == 15 || i == 16 || i == 17)
                            HttpContext.Current.Response.Write("<Td style='border-width: 0px; text-align: right;'>");
                        //else if(i == 18)
                        //    HttpContext.Current.Response.Write("<Td style='border-width: 0px; text-align: center;'>");
                        else
                            HttpContext.Current.Response.Write("<Td style='border-width: 0px'>");
                        //HttpContext.Current.Response.Write(row[i].ToString());

                        if (i == 0)
                        {
                            if (string.IsNullOrEmpty(row[i].ToString()))
                                HttpContext.Current.Response.Write("");
                            else
                                HttpContext.Current.Response.Write(Convert.ToString(Convert.ToDateTime(row[i].ToString()).ToShortDateString()));
                        }
                        else if (i == 1)
                        {
                            if (string.IsNullOrEmpty(row[i].ToString()))
                                HttpContext.Current.Response.Write("");
                            else
                                HttpContext.Current.Response.Write(Convert.ToString(Convert.ToDateTime(row[i].ToString()).ToShortDateString()));
                        }
                        else if (i == 11)
                        {
                            if (string.IsNullOrEmpty(row[i].ToString()))
                                HttpContext.Current.Response.Write("");
                            else
                                HttpContext.Current.Response.Write(Convert.ToString(Convert.ToDecimal(row[i].ToString()).ToString("C4")));
                        }
                        else if (i == 12)
                        {
                            if (string.IsNullOrEmpty(row[i].ToString()))
                                HttpContext.Current.Response.Write("");
                            else
                                HttpContext.Current.Response.Write(Convert.ToString(Convert.ToDecimal(row[i].ToString()).ToString("C4")));
                        }
                        else if (i == 13)
                        {
                            if (string.IsNullOrEmpty(row[i].ToString()))
                                HttpContext.Current.Response.Write("");
                            else
                                HttpContext.Current.Response.Write(Convert.ToString(Convert.ToDecimal(row[i].ToString()).ToString("C4")));
                        }
                        else if (i == 14)
                        {
                            if (string.IsNullOrEmpty(row[i].ToString()))
                                HttpContext.Current.Response.Write("");
                            else
                                HttpContext.Current.Response.Write(Convert.ToString(Convert.ToDecimal(row[i].ToString()).ToString("C4")));
                        }
                        else if (i == 15)
                        {
                            if (string.IsNullOrEmpty(row[i].ToString()))
                                HttpContext.Current.Response.Write("");
                            else
                                HttpContext.Current.Response.Write(Convert.ToString(Convert.ToDecimal(row[i].ToString()).ToString("C4")));
                        }
                        else if (i == 16)
                        {
                            if (string.IsNullOrEmpty(row[i].ToString()))
                                HttpContext.Current.Response.Write("");
                            else
                                HttpContext.Current.Response.Write(Convert.ToString(Convert.ToDecimal(row[i].ToString()).ToString("C4")));
                        }
                        else if (i == 17)
                        {
                            if (string.IsNullOrEmpty(row[i].ToString()))
                                HttpContext.Current.Response.Write("");
                            else
                                HttpContext.Current.Response.Write(Convert.ToString(Convert.ToDecimal(row[i].ToString()).ToString("C4")));
                        }
                        else
                            HttpContext.Current.Response.Write(row[i].ToString());

                        HttpContext.Current.Response.Write("</Td>");

                    }
                    HttpContext.Current.Response.Write("</TR>");
                }

                HttpContext.Current.Response.Write("</Table> </font>");
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
            catch (Exception ex) { throw ex; }
        }

        #endregion

        #region VARIABLES Y PROPIEDADES
        CtrlAeroTercerosFacturas_Presenter oPresenter;

        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;

        private const string sClase = "frmCtrlAeroTercerosFacturas.aspx.cs";
        private const string sPagina = "frmCtrlAeroTercerosFacturas.aspx";

        private string Mes(int iMes)
        {
            string Mes;
            switch (iMes)
            {
                case 1: Mes = "Enero"; break;
                case 2: Mes = "Febrero"; break;
                case 3: Mes = "Marzo"; break;
                case 4: Mes = "Abril"; break;
                case 5: Mes = "Mayo"; break;
                case 6: Mes = "Junio"; break;
                case 7: Mes = "Julio"; break;
                case 8: Mes = "Agosto"; break;
                case 9: Mes = "Septiembre"; break;
                case 10: Mes = "Octubre"; break;
                case 11: Mes = "Noviembre"; break;
                case 12: Mes = "Diciembre"; break;
                default: Mes = ""; break;
            }
            return Mes;
        }

        public object[] oArrFiltroReporteFacturas
        {
            get
            {
                return new object[] {   "@FechaInicio",                     dFechaIni.Value, 
                                        "@FechaFinal",                     dFechaFin.Value};
            }
        }

        #endregion

    }
}