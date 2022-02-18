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

namespace ALE_MexJet.Views.Reportes
{
    public partial class frmVuelosReasignados : System.Web.UI.Page, IViewVuelosReasignados
    {
        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            oPresenter = new VuelosReasignados_Presenter(this, new DBVuelosReasignados());
            RecargaGrid();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (eSearchObj != null)
                eSearchObj(null, null);
        }

        protected void upaReport_Unload(object sender, EventArgs e)
        {
            MethodInfo methodInfo = typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Where(i => i.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel")).First();
            methodInfo.Invoke(ScriptManager.GetCurrent(Page),
                new object[] { sender as UpdatePanel });
        }
        
		protected void btnExcel_Click(object sender, EventArgs e)
        {
			try
			{
				string Cadena = VuelosReasignados.InnerHtml;
				ExportaExcel(Cadena);
			}
			catch (Exception ex) { Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnExcel_Click5", "Aviso"); }
        }
        #endregion

        #region METODOS
        public void MostrarMensaje(string sMensaje, string sCaption)
        { }
        
		public void LlenaGrid(DataTable dtObjCat)
        {
            //gvVuelosreasignados.DataSource = dtObjCat;
            //gvVuelosreasignados.DataBind();
            ViewState["gvVuelosreasignados"] = dtObjCat;
            try
            {

                VuelosReasignados.InnerHtml = string.Empty;

                DataTable Acumulado = (DataTable)ViewState["gvVuelosreasignados"];


                string cadena = @"<table border='1' bgColor='#ffffff' borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; background:white; width:100%;'>" 
                    
                    + "<tr>"
                        +   "<td colspan='13' style='border-bottom : none; border-left:none; border-top:none; border-right:none; text-align: center; vertical-align: middle'>"
                        +   "<B>&nbsp;</B></td>"
                    + "</tr>" 
                    
                    + "<tr>"
                        + "<td colspan='8' style='border-bottom : none; border-left:none; border-right:none; text-align: center; vertical-align: middle'><B>&nbsp;</B></td>"
                        + "<td colspan='2' style='border-bottom : none; border-left:none; border-right:none; text-align: center; vertical-align: middle'>"
                        + "<B>Pernocta</B></td>"
                        + "<td colspan='2' style='border-bottom : none; border-left:none; border-right:none; text-align: center; vertical-align: middle'>" 
                        + "<B>Tiempo de Espera</B></td>"
                        + "<td style='border-bottom : none; border-left:none; border-right:none; text-align: center; vertical-align: middle'><B>&nbsp;</B></td>"
                    + "</tr>" 
                    
                    + "<tr>"
                        + "<td style='border-left:none; border-right:none;'>"
                        + "<B>&nbsp;&nbsp;&nbsp;CLIENTE</B></td>"
                        + "<td style='border-left:none; border-right:none;'>"
                        + "<B>&nbsp;&nbsp;&nbsp;CONTRATO</B></td>"
                        + "<td style='border-left:none; border-right:none;'>"
                        + "<B>&nbsp;&nbsp;&nbsp;FECHA</B></td>" 
                        + "<td style='border-left:none; border-right:none;'>"
                        + "<B>&nbsp;&nbsp;&nbsp;REMISION</B></td>"
                        + "<td style='border-left:none; border-right:none;'>"
                        + "<B>&nbsp;&nbsp;&nbsp;MATRICULA</B></td>"
                        + "<td style='border-left:none; border-right:none;'>"
                        + "<B>&nbsp;&nbsp;&nbsp;RUTA</B></td>"
                        + "<td style='border-left:none; border-right:none;'>"
                        + "<B>&nbsp;&nbsp;&nbsp;PAX</B></td>"
                        + "<td style='border-left:none; border-right:none;'>"
                        + "<B>&nbsp;&nbsp;&nbsp;TIEMPO DE VUELO</B></td>"
                        + "<td style='border-left:none; border-right:none;'>"
                        + "<B>&nbsp;&nbsp;&nbsp;NACIONAL</B></td>"
                        + "<td style='border-left:none; border-right:none;'>"
                        + "<B>&nbsp;&nbsp;&nbsp;INTERNACIONAL</B></td>"
                        + "<td style='border-left:none; border-right:none;'>"
                        + "<B>&nbsp;&nbsp;&nbsp;NACIONAL</B></td>"
                        + "<td style='border-left:none; border-right:none;'>"
                        + "<B>&nbsp;&nbsp;&nbsp;INTERNACIONAL</B></td>"
                        + "<td style='border-left:none; border-right:none;'>"
                        + "<B>&nbsp;&nbsp;&nbsp;IMPORTE TOTAL</B></td>"
                    + "</tr>";

                if (Acumulado != null && Acumulado.Rows.Count != 0)
                {
                    int RowMax = Acumulado.Rows.Count - 1;

                    //DETALLE
                    for (int i = 0; i < Acumulado.Rows.Count - 1; i++)
                    {
                        cadena +="<tr>";
                        for (int c = 0; c < Acumulado.Columns.Count; c++)
                        {
							int firstCol = !String.IsNullOrEmpty(Acumulado.Rows[i][1].ToString()) ? 0 : 1;
							if (c != firstCol && c != 4)
							{
								string styles = firstCol == 0 ? "border-width:0px;" : "border-left:none; border-right:none;";
								cadena += "<td style='" + styles + "'>&nbsp;&nbsp;&nbsp;";
								cadena += c == 3 && !String.IsNullOrEmpty(Acumulado.Rows[i][c].ToString()) ? 
									Acumulado.Rows[i][c].ToString().Remove(10) : Acumulado.Rows[i][c].S();
								cadena += "&nbsp;&nbsp;&nbsp;</td>";
							}
                        }
                        cadena += "</tr>";
                    }


                    //TOTAL
                    cadena = cadena + "<tr>";
                    for (int c = 0; c < Acumulado.Columns.Count; c++)
                    {
						if (c != 1 && c != 4)
						{
							cadena += "<td style='border-left:none; border-right:none;'>&nbsp;&nbsp;&nbsp;";
							cadena += Acumulado.Rows[RowMax][c].S();
							cadena += "&nbsp;&nbsp;&nbsp;</td>";
						}
                    }
                    cadena = cadena + "</tr>";
                }
                cadena = cadena + "</table>";

                VuelosReasignados.InnerHtml = cadena;
            }
            catch (Exception x) { throw x; }
        }
        
		protected void RecargaGrid()
        {
            if (ViewState["gvVuelosreasignados"] != null)
            {
               // gvVuelosreasignados.DataSource = (DataTable)ViewState["gvVuelosreasignados"];
               // gvVuelosreasignados.DataBind();
            }
        }
        
		protected void ExportaExcel(string cadena)
        {
			try
			{

				string _ippuertoserver = Utils.ObtieneParametroPorClave("53");
				string FileName = "VuelosReasignados - " + dtFechaDesde.Value.ToString().Remove(10) + " - " + dtFechaHasta.Value.ToString().Remove(10) + ".xls";
				//DataTable DT = (DataTable)ViewState["oCliente"];

				//DT = DT.Select("IdCliente = " + ddlClientes.SelectedItem.Value.S() + "").CopyToDataTable();
				//string Razonsocial = DT.Rows[0]["Nombre"].S();
				//string _ippuertoserver = Request.Url.Authority;

				HttpContext.Current.Response.Clear();
				HttpContext.Current.Response.ClearContent();
				HttpContext.Current.Response.ClearHeaders();
				HttpContext.Current.Response.Buffer = true;
				HttpContext.Current.Response.ContentType = "application/ms-excel";
				HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
				HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + FileName + "\"");
				//HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment; filename = \"{0}\"", System.IO.Path.GetFileName(FileName)));
				HttpContext.Current.Response.Charset = "iso-8859-1";
				HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1252");

				HttpContext.Current.Response.Write(@"<table><tr><td style=' text-align: center; vertical-align: middle' colspan='2'></td>" +
				 "<td style=' text-align: center; font-size: x-large; vertical-align: middle' colspan='8'>Aerolíneas Ejecutivas S.A. de C.V.</td><td></td><td></td> " +
				 "<td ></td><td style=' text-align: right; vertical-align: middle' colspan='2'></td> </tr>" +
				 "<tr><td style=' text-align: center; vertical-align: middle' colspan='2'><img src='http://" + _ippuertoserver.S() + "/img/mexjet_p.png' height='100px' width='90px'/></td>" +
				 "<td style=' text-align: center; font-size: x-large; vertical-align: middle' colspan='8'>Reporte de Vuelos Reasignados</td><td colspan='3'></td>" +
				 "<td style=' text-align: right; vertical-align: middle' colspan='2'><img src='http://" + _ippuertoserver.S() + "/img/colors/blue/logo-ale.png' height='100px' width='90px'/></td></tr>" +
				 "<tr><td colspan='2'></td><td colspan='9'style='text-align: center;  font-size: x-large; vertical-align: middle' ></td><td colspan='4'></td></tr>" +
				 "<tr><td colspan='2'></td><td style='text-align: center;  vertical-align: middle' colspan='8'>&nbsp;&nbsp;" + dtFechaDesde.Value.ToString().Remove(10) + " - " + dtFechaHasta.Value.ToString().Remove(10) + "</td><td></td><td colspan='3'></td></tr></table> <br /><br />");


				HttpContext.Current.Response.Write(cadena.S());
				HttpContext.Current.Response.Flush();
				HttpContext.Current.Response.End();
			}
			catch (Exception x) { throw x; }
        }
        #endregion

        #region VARIABLES
        private const string sClase = "frmFacturaProveedor.aspx.cs";
        private const string sPagina = "frmFacturaProveedor.aspx";

        VuelosReasignados_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;

        public object[] oArrFiltros
        {
            get
            {
                return new object[]
                { 
                    
                    "@FechaInicio", dtFechaDesde.Date.ToString("MM-dd-yyyy"),
                    "@FechaFinal",  dtFechaHasta.Date.ToString("MM-dd-yyyy")
                };
            }
        }

        #endregion

    }
}