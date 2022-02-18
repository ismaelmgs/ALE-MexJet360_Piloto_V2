using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ALE_MexJet.Clases;
using ALE_MexJet.ControlesUsuario;
using ALE_MexJet.DomainModel;
using ALE_MexJet.Interfaces;
using ALE_MexJet.Presenter;
using ALE_MexJet.Objetos;
using NucleoBase.Core;
using NucleoBase.Seguridad;
using DevExpress.XtraPrinting;
using DevExpress.Export;
using DevExpress.Web;
using DevExpress.Web.Export;
using System.Data;
using System.Reflection;

namespace ALE_MexJet.Views.Reportes
{
	public partial class frmHorasVoladasCliente : System.Web.UI.Page, IViewHorasVoladasCliente
	{
		#region Eventos
		protected void Page_Load(object sender, EventArgs e)
		{
			try
			{
				oPresenter = new HorasVoladasCliente_Presenter(this, new DBHorasVoladasCliente());

				if (!IsPostBack)
				{
					if (eObjCliente != null)
						eObjCliente(null, null);

					ddlContrato.Enabled = false;
				}
			}
			catch (Exception ex)
			{
				Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "Page_Load", "Aviso");
			}
		}

		protected void btnNuevo_Click(object sender, EventArgs e)
		{
			try
			{

			}
			catch (Exception ex)
			{
				Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnNuevo_Click", "Aviso");
			}
		}

		protected void btnExcel_Click(object sender, EventArgs e)
		{
			try
			{
				string cadena = HorasVoladas.InnerHtml;
				string _ippuertoserver = Utils.ObtieneParametroPorClave("53");
				string sFechaDesde = dtFechaDesde.Date == DateTime.MinValue ? 
					String.Format("{0:dd-MM-yyyy}", DateTime.MinValue) : 
					String.Format("{0:dd-MM-yyyy}", dtFechaDesde.Date);
				string sFechaHasta = dtFechaHasta.Date == DateTime.MinValue ? 
					String.Format("{0:dd-MM-yyyy}", DateTime.Now) : 
					String.Format("{0:dd-MM-yyyy}", dtFechaHasta.Date);
				string sConCosto = chkCosto.Checked ? "con Costo " : "";
				string FileName = "Horas Voladas " + sConCosto + "- " + ddlContrato.SelectedItem.Text.S() 
					+ " - " + sFechaDesde + " - " + sFechaHasta + ".xls";

				HttpContext.Current.Response.Clear();
				HttpContext.Current.Response.ClearContent();
				HttpContext.Current.Response.ClearHeaders();
				HttpContext.Current.Response.Buffer = true;
				HttpContext.Current.Response.ContentType = "application/ms-excel";
				HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
				HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + FileName + "\"");
				HttpContext.Current.Response.Charset = "iso-8859-1";
				HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1252");
				HttpContext.Current.Response.Write(cadena);
				HttpContext.Current.Response.Flush();
				HttpContext.Current.Response.End();
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
				if (!chkCosto.Checked)
				{
					if (eSearchObj != null)
						eSearchObj(null, EventArgs.Empty);

					CargaReporte();
				}
				else
				{
					if (eSearchObj2 != null)
						eSearchObj2(null, EventArgs.Empty);

					CargaReporteCosto();
				}
			}
			catch (Exception ex)
			{
				Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnBuscar_Click", "Aviso");
			}
		}

		protected void gvHorasVoladasCliente_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
		{
			try
			{

			}
			catch (Exception ex)
			{
				Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvHorasVoladasCliente_CustomButtonCallback", "Aviso");
			}
		}

		protected void UpdatePanel_Unload(object sender, EventArgs e)
		{
			MethodInfo methodInfo = typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Where(i => i.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel")).First();
			methodInfo.Invoke(ScriptManager.GetCurrent(Page),
			new object[] { sender as UpdatePanel });
		}

		protected void ddlClientes_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				if (eObjContrato != null)
					eObjContrato(null, null);

				ddlContrato.Enabled = true;
			}
			catch (Exception ex) { Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "ddlClientes_SelectedIndexChanged", "Aviso"); }
		}
		#endregion Eventos

		#region Metodos
		public void CargaGrid(DataSet dsObjHorasVoladasCliente)
		{
			try
			{
				ViewState["gvHorasVoladasCliente"] = dsObjHorasVoladasCliente;
			}
			catch (Exception ex)
			{
				Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "CargaGrid", "Aviso");
			}
		}

		public void CargaReporte()
		{
			try
			{
				DataSet ds = (DataSet)ViewState["gvHorasVoladasCliente"];

				HorasVoladas.InnerHtml = string.Empty;
				string cadena = generarHeader();
				cadena += "<tbody><tr>"
						+ "<th colspan='15' class='no-border'>&nbsp;</th>"
					+ "</tr>"
					+ "<tr>"
						+ "<th colspan='5' class='head-1' style='border-top:1px solid;'>&nbsp;</th>"
						+ "<th colspan='2' class='head-1' style='border-top:1px solid;'>- Horas de Vuelo -</th>"
						+ "<th colspan='2' class='head-1' style='border-top:1px solid;'>- Horas de Espera -</th>"
						+ "<th colspan='2' class='head-1' style='border-top:1px solid;'>- Pernoctas -</th>"
						+ "<th colspan='2' class='head-1' style='border-top:1px solid;'>- Equivalente en horas -</th>"
						+ "<th colspan='2' class='head-1' style='border-top:1px solid;'>- Horas Voladas -</th>"
					+ "</tr>"
					+ "<tr>"
						+ "<th class='head-2' style='border-bottom:1px solid;'>Fecha</th>"
						+ "<th class='head-2' style='border-bottom:1px solid;'>Matrícula</th>"
						+ "<th class='head-2' style='border-bottom:1px solid;'>Remisión</th>"
						+ "<th class='head-2' style='border-bottom:1px solid;'>Ruta de Vuelo</th>"
						+ "<th class='head-2' style='border-bottom:1px solid;'>&nbsp;</th>"
						+ "<th class='head-2' style='border-bottom:1px solid;'>Nacional</th>"
						+ "<th class='head-2' style='border-bottom:1px solid;'>Internacional</th>"
						+ "<th class='head-2' style='border-bottom:1px solid;'>Nacional</th>"
						+ "<th class='head-2' style='border-bottom:1px solid;'>Internacional</th>"
						+ "<th class='head-2' style='border-bottom:1px solid;'>Nacional</th>"
						+ "<th class='head-2' style='border-bottom:1px solid;'>Internacional</th>"
						+ "<th class='head-2' style='border-bottom:1px solid;'>Espera</th>"
						+ "<th class='head-2' style='border-bottom:1px solid;'>Pernocta</th>"
					    + "<th class='head-2' style='border-bottom:1px solid;'>&nbsp;</th>"
						+ "<th class='head-2' style='border-bottom:1px solid;'>Acumulado</th>"
					+ "</tr>";

				if (ds != null && ds.Tables[0].Rows.Count != 0)
				{
					cadena += "<tr>" 
						+ "<td colspan='4' class='no-border'>&nbsp;</td>"
						+ "<td class='no-border align-right'>INICIALES: </td>"
						+ "<td class='no-border align-right'>" + ds.Tables[0].Rows[0][0].S() + "</td>"
						+ "<td class='no-border align-right'>" + ds.Tables[0].Rows[0][1].S() + "</td>"
						+ "<td class='no-border align-right'>" + ds.Tables[0].Rows[0][2].S() + "</td>"
						+ "<td class='no-border align-right'>" + ds.Tables[0].Rows[0][3].S() + "</td>"
						+ "<td class='no-border align-right'>" + ds.Tables[0].Rows[0][4].S() + "</td>"
						+ "<td class='no-border align-right'>" + ds.Tables[0].Rows[0][5].S() + "</td>"
						+ "<td class='no-border align-right'>" + ds.Tables[0].Rows[0][6].S() + "</td>"
						+ "<td class='no-border align-right'>" + ds.Tables[0].Rows[0][7].S() + "</td>"
						+ "<td class='no-border align-right'>" + ds.Tables[0].Rows[0][8].S() + "</td>"
						+ "<td class='no-border align-right'>&nbsp;</td>"
						+ "</tr>";
				}

				int iMes = 0;
				if (ds != null && ds.Tables[1].Rows.Count != 0)
				{
					DataTable tblDatos = ds.Tables[1];
					DataTable tblTotalesMes = ds.Tables[2];
					DataTable tblTotalesMesAcumulados = ds.Tables[3];
					DateTime dMes = (DateTime)tblDatos.Rows[0]["Mes"];
					
					for (int i = 0; i < tblDatos.Rows.Count; i++)
					{
						if (dMes != (DateTime)tblDatos.Rows[i]["Mes"])
						{
							cadena += agregarTotalesMes(iMes);
							dMes = (DateTime)tblDatos.Rows[i]["Mes"];
							iMes++;
						}

						cadena += "<tr>";
						cadena += "<td class='no-border'>" + tblDatos.Rows[i]["Fecha"].S().Remove(10) + "</td>";
						cadena += "<td class='no-border' style='white-space: nowrap;'>" + tblDatos.Rows[i]["Matricula"].S() + "</td>";
						cadena += "<td class='no-border'>" + tblDatos.Rows[i]["IdRemision"].S() + "</td>";
						cadena += "<td class='no-border' style='white-space: nowrap;'>" + tblDatos.Rows[i]["Ruta"].S() + "</td>";
						cadena += "<td class='no-border align-right'>&nbsp;</td>";
						cadena += "<td class='no-border align-right'>" + eliminarCeros(tblDatos.Rows[i]["VueloNacional"].S()) + "</td>";
						cadena += "<td class='no-border align-right'>" + eliminarCeros(tblDatos.Rows[i]["VueloInternacional"].S()) + "</td>";
						cadena += "<td class='no-border align-right'>" + eliminarCeros(tblDatos.Rows[i]["EsperaNacional"].S()) + "</td>";
						cadena += "<td class='no-border align-right'>" + eliminarCeros(tblDatos.Rows[i]["EsperaInternacional"].S()) + "</td>";
						cadena += "<td class='no-border align-right'>" + eliminarCeros(tblDatos.Rows[i]["PernoctaNacional"].S()) + "</td>";
						cadena += "<td class='no-border align-right'>" + eliminarCeros(tblDatos.Rows[i]["PernoctaInternacional"].S()) + "</td>";
						cadena += "<td class='no-border align-right'>" + eliminarCeros(tblDatos.Rows[i]["EquivalenteEspera"].S()) + "</td>";
						cadena += "<td class='no-border align-right'>" + eliminarCeros(tblDatos.Rows[i]["EquivalentePernocta"].S()) + "</td>";
						cadena += "<td class='no-border align-right'>" + eliminarCeros(tblDatos.Rows[i]["HorasVoladas"].S()) + "</td>";
						cadena += "<td class='no-border align-right'>" + eliminarCeros(tblDatos.Rows[i]["Acumulado"].S()) + "</td>";
						cadena += "</tr>";
					}
				}
				cadena += agregarTotalesMes(iMes);
				cadena += agregarTotalesMes(iMes, true);
				cadena += "</tbody></table>";
				HorasVoladas.InnerHtml = cadena;
			}
			catch (Exception ex)
			{
				Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "CargaReporte", "Aviso");
			}
		}

		public void CargaReporteCosto()
		{
			try
			{
				DataSet ds = (DataSet)ViewState["gvHorasVoladasCliente"];

				HorasVoladas.InnerHtml = string.Empty;
				string cadena = generarHeader();
				cadena += "<tbody><tr>"
						+ "<th colspan='15' class='no-border'>&nbsp;</th>"
					+ "</tr>"
					+ "<tr>"
						+ "<th colspan='5' class='head-1' style='border-top:1px solid;'>&nbsp;</th>"
						+ "<th colspan='2' class='head-1' style='border-top:1px solid;'>- Vuelo Normal -</th>"
						+ "<th colspan='2' class='head-1' style='border-top:1px solid;'>- Vuelo Ferrys -</th>"
						+ "<th colspan='2' class='head-1' style='border-top:1px solid;'>- Esperas -</th>"
						+ "<th colspan='2' class='head-1' style='border-top:1px solid;'>- Pernoctas -</th>"
						+ "<th colspan='2' class='head-1' style='border-top:1px solid;'>- Total -</th>"
					+ "</tr>"
					+ "<tr style='text-align:center;'>"
						+ "<th class='head-2' style='border-bottom:1px solid;' style='border-bottom:1px solid;'>Fecha</th>"
						+ "<th class='head-2' style='border-bottom:1px solid;'>Matrícula</th>"
						+ "<th class='head-2' style='border-bottom:1px solid;'>Remisión</th>"
						+ "<th class='head-2' style='border-bottom:1px solid;'>Ruta de Vuelo</th>"
						+ "<th class='head-2' style='border-bottom:1px solid;'>&nbsp;</th>"
						+ "<th class='head-2' style='border-bottom:1px solid;'>Horas</th>"
						+ "<th class='head-2' style='border-bottom:1px solid;'>Costo</th>"
						+ "<th class='head-2' style='border-bottom:1px solid;'>Horas</th>"
						+ "<th class='head-2' style='border-bottom:1px solid;'>Costo</th>"
						+ "<th class='head-2' style='border-bottom:1px solid;'>Horas</th>"
						+ "<th class='head-2' style='border-bottom:1px solid;'>Costo</th>"
						+ "<th class='head-2' style='border-bottom:1px solid;'>Horas</th>"
						+ "<th class='head-2' style='border-bottom:1px solid;'>Costo</th>"
						+ "<th class='head-2' style='border-bottom:1px solid;'>Horas</th>"
						+ "<th class='head-2' style='border-bottom:1px solid;'>Costo</th>"
					+ "</tr>";

				int iMes = 0;
				if (ds != null && ds.Tables[0].Rows.Count != 0)
				{
					DataTable tblDatos = ds.Tables[0];
					DataTable tblTotalesMes = ds.Tables[1];
					
					DateTime dMes = (DateTime)tblDatos.Rows[0]["Mes"];

					for (int i = 0; i < tblDatos.Rows.Count; i++)
					{
						if (dMes != (DateTime)tblDatos.Rows[i]["Mes"])
						{
							cadena += agregarTotalesMesCosto(iMes);
							dMes = (DateTime)tblDatos.Rows[i]["Mes"];
							iMes++;
						}

						cadena += "<tr>";
						cadena += "<td class='no-border'>" + tblDatos.Rows[i]["Fecha"].S().Remove(10) + "</td>";
						cadena += "<td class='no-border' style='white-space: nowrap;'>" + tblDatos.Rows[i]["Matricula"].S() + "</td>";
						cadena += "<td class='no-border'>" + tblDatos.Rows[i]["IdRemision"].S() + "</td>";
						cadena += "<td class='no-border' style='white-space: nowrap;'>" + tblDatos.Rows[i]["Ruta"].S() + "</td>";
						cadena += "<td class='no-border align-right'>&nbsp;</td>";
						cadena += "<td class='no-border align-right'>" + eliminarCeros(tblDatos.Rows[i]["HrsVueloNormal"].S()) + "</td>";
						cadena += "<td class='no-border align-right'>" + eliminarCeros(tblDatos.Rows[i]["CostoVueloNormal"].S()) + "</td>";
						cadena += "<td class='no-border align-right'>" + eliminarCeros(tblDatos.Rows[i]["HrsVueloFerrys"].S()) + "</td>";
						cadena += "<td class='no-border align-right'>" + eliminarCeros(tblDatos.Rows[i]["CostoVueloFerrys"].S()) + "</td>";
						cadena += "<td class='no-border align-right'>" + eliminarCeros(tblDatos.Rows[i]["HrsEspera"].S()) + "</td>";
						cadena += "<td class='no-border align-right'>" + eliminarCeros(tblDatos.Rows[i]["CostoEspera"].S()) + "</td>";
						cadena += "<td class='no-border align-right'>" + eliminarCeros(tblDatos.Rows[i]["HrsPernocta"].S()) + "</td>";
						cadena += "<td class='no-border align-right'>" + eliminarCeros(tblDatos.Rows[i]["CostoPernocta"].S()) + "</td>";
						cadena += "<td class='no-border align-right'>" + eliminarCeros(tblDatos.Rows[i]["TotalHrs"].S()) + "</td>";
						cadena += "<td class='no-border align-right'>" + eliminarCeros(tblDatos.Rows[i]["TotalCosto"].S()) + "</td>";
						cadena += "</tr>";
					}
				}
				cadena += agregarTotalesMesCosto(iMes);
				cadena += agregarTotalesMesCosto(iMes, true);
				cadena += "</tbody></table>";
				HorasVoladas.InnerHtml = cadena;
			}
			catch (Exception ex)
			{
				Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "CargaReporteCosto", "Aviso");
			}
		}

		private string generarHeader()
		{
			string _ippuertoserver = Utils.ObtieneParametroPorClave("53");
			string sFechaDesde = dtFechaDesde.Date == DateTime.MinValue ?
				String.Format("{0:dd/MM/yyyy}", DateTime.MinValue) :
				String.Format("{0:dd/MM/yyyy}", dtFechaDesde.Date);
			string sFechaHasta = dtFechaHasta.Date == DateTime.MinValue ?
				String.Format("{0:dd/MM/yyyy}", DateTime.Now) :
				String.Format("{0:dd/MM/yyyy}", dtFechaHasta.Date);
			string cadena = @"<table bgColor='#ffffff' borderColor='#000000' cellSpacing='0' cellPadding='0' class='tblRpt'><tbody>"
					+ "<tr>"
						+ "<td style=' text-align: center; vertical-align: middle' colspan='2'></td>"
						+ "<td style=' text-align: center; font-size: x-large; vertical-align: middle' colspan='11'>"
							+ "Aerolíneas Ejecutivas S.A. de C.V."
						+ "</td>"
						+ "<td style=' text-align: right; vertical-align: middle' colspan='2'>Fecha: " + DateTime.Now.S() + "</td>"
					+ "</tr>"
					+ "<tr>"
						+ "<td style=' text-align: center; vertical-align: middle' colspan='2'>"
							+ "<img src='http://" + _ippuertoserver.S() + "/img/mexjet_p.png' width='150px' height='67px'/>"
						+ "</td>"
						+ "<td style=' text-align: center; font-size: large; vertical-align: middle' colspan='11'>"
							+ "Resumen de Horas Voladas"
						+ "</td>"
						+ "<td style=' text-align: right; vertical-align: middle' colspan='2'>"
							+ "<img src='http://" + _ippuertoserver.S() + "/img/colors/blue/logo-ale.png' width='156px' height='92px'/>"
						+ "</td>"
					+ "</tr>"
					+ "<tr>"
						+ "<td colspan='2'></td>"
						+ "<td colspan='11'style='text-align: center; font-size: x-large; vertical-align: middle' ></td>"
						+ "<td colspan='2'></td>"
					+ "</tr>"
					+ "<tr>"
						+ "<td style=' text-align: center; vertical-align: middle' colspan='2'></td>"
						+ "<td style=' text-align: center; vertical-align: middle' colspan='11'>"
							+ ddlClientes.SelectedItem.Text + " - " + ddlContrato.SelectedItem.Text
						+ "</td>"
						+ "<td style=' text-align: right; vertical-align: middle' colspan='2'></td>"
					+ "</tr>"
					+ "<tr>"
						+ "<td colspan='2'></td>"
						+ "<td style='text-align: center; vertical-align: middle' colspan='11'>&nbsp;&nbsp;"
							+ sFechaDesde + " - " + sFechaHasta
						+ "</td>"
						+ "<td colspan='2></td>"
					+ "</tr><tr><td colspan='15'>&nbsp;</td></tr></tbody>";
			return cadena;
		}

		private string agregarTotalesMes(int iMes, bool isTotal = false)
		{
			DataSet ds = (DataSet)ViewState["gvHorasVoladasCliente"];
			DataTable tblTotalesMes = ds.Tables[2];
			DataTable tblTotalesMesAcumulados = ds.Tables[3];
			string cadena = string.Empty;
			if (isTotal)
			{
				cadena = "<tr><td colspan='5' class='no-border'></td><td colspan='10' class='no-border' style='border-bottom: 1px solid; border-top:none;'>&nbsp;</td></tr>"
				+ "<tr class='no-border'>"
				+ "<td colspan='4' class='no-border'>&nbsp;</td>"
				+ "<td class='no-border align-right'>TOTAL: </td>"
				+ "<td class='no-border align-right'>" + tblTotalesMesAcumulados.Rows[iMes]["VueloNacional"].S() + "</td>"
				+ "<td class='no-border align-right'>" + tblTotalesMesAcumulados.Rows[iMes]["VueloInternacional"].S() + "</td>"
				+ "<td class='no-border align-right'>" + tblTotalesMesAcumulados.Rows[iMes]["EsperaNacional"].S() + "</td>"
				+ "<td class='no-border align-right'>" + tblTotalesMesAcumulados.Rows[iMes]["EsperaInternacional"].S() + "</td>"
				+ "<td class='no-border align-right'>" + tblTotalesMesAcumulados.Rows[iMes]["PernoctaNacional"].S() + "</td>"
				+ "<td class='no-border align-right'>" + tblTotalesMesAcumulados.Rows[iMes]["PernoctaInternacional"].S() + "</td>"
				+ "<td class='no-border align-right'>" + tblTotalesMesAcumulados.Rows[iMes]["EquivalenteEspera"].S() + "</td>"
				+ "<td class='no-border align-right'>" + tblTotalesMesAcumulados.Rows[iMes]["EquivalentePernocta"].S() + "</td>"
				+ "<td class='no-border align-right'>" + tblTotalesMesAcumulados.Rows[iMes]["HorasVoladas"].S() + "</td>"
				+ "<td class='no-border align-right'>&nbsp;</td>"
				+ "</tr>";
			}
			else
			{
				cadena = "<tr>"
					+ "<td colspan='4' class='no-border'>&nbsp;</td>"
					+ "<td class='no-border align-right'>DEL MES: </td>"
					+ "<td class='border-top align-right' style='border-top:1px solid;'>" + eliminarCeros(tblTotalesMes.Rows[iMes]["VueloNacional"].S()) + "</td>"
					+ "<td class='border-top align-right' style='border-top:1px solid;'>" + eliminarCeros(tblTotalesMes.Rows[iMes]["VueloInternacional"].S()) + "</td>"
					+ "<td class='border-top align-right' style='border-top:1px solid;'>" + eliminarCeros(tblTotalesMes.Rows[iMes]["EsperaNacional"].S()) + "</td>"
					+ "<td class='border-top align-right' style='border-top:1px solid;'>" + eliminarCeros(tblTotalesMes.Rows[iMes]["EsperaInternacional"].S()) + "</td>"
					+ "<td class='border-top align-right' style='border-top:1px solid;'>" + eliminarCeros(tblTotalesMes.Rows[iMes]["PernoctaNacional"].S()) + "</td>"
					+ "<td class='border-top align-right' style='border-top:1px solid;'>" + eliminarCeros(tblTotalesMes.Rows[iMes]["PernoctaInternacional"].S()) + "</td>"
					+ "<td class='border-top align-right' style='border-top:1px solid;'>" + eliminarCeros(tblTotalesMes.Rows[iMes]["EquivalenteEspera"].S()) + "</td>"
					+ "<td class='border-top align-right' style='border-top:1px solid;'>" + eliminarCeros(tblTotalesMes.Rows[iMes]["EquivalentePernocta"].S()) + "</td>"
					+ "<td class='border-top align-right' style='border-top:1px solid;'>" + eliminarCeros(tblTotalesMes.Rows[iMes]["HorasVoladas"].S()) + "</td>"
					+ "<td class='border-top align-right' style='border-top:1px solid;'>&nbsp;</td>"
					+ "</tr>"
					+ "<tr class='no-border'>"
					+ "<td colspan='4' class='no-border'>&nbsp;</td>"
					+ "<td class='no-border align-right'>ACUMULADO: </td>"
					+ "<td class='no-border align-right'>" + tblTotalesMesAcumulados.Rows[iMes]["VueloNacional"].S() + "</td>"
					+ "<td class='no-border align-right'>" + tblTotalesMesAcumulados.Rows[iMes]["VueloInternacional"].S() + "</td>"
					+ "<td class='no-border align-right'>" + tblTotalesMesAcumulados.Rows[iMes]["EsperaNacional"].S() + "</td>"
					+ "<td class='no-border align-right'>" + tblTotalesMesAcumulados.Rows[iMes]["EsperaInternacional"].S() + "</td>"
					+ "<td class='no-border align-right'>" + tblTotalesMesAcumulados.Rows[iMes]["PernoctaNacional"].S() + "</td>"
					+ "<td class='no-border align-right'>" + tblTotalesMesAcumulados.Rows[iMes]["PernoctaInternacional"].S() + "</td>"
					+ "<td class='no-border align-right'>" + tblTotalesMesAcumulados.Rows[iMes]["EquivalenteEspera"].S() + "</td>"
					+ "<td class='no-border align-right'>" + tblTotalesMesAcumulados.Rows[iMes]["EquivalentePernocta"].S() + "</td>"
					+ "<td class='no-border align-right'>" + tblTotalesMesAcumulados.Rows[iMes]["HorasVoladas"].S() + "</td>"
					+ "<td class='no-border align-right'>&nbsp;</td>"
					+ "</tr>";
			}
			return cadena;
		}

		private string agregarTotalesMesCosto(int iMes, bool isTotal = false)
		{
			DataSet ds = (DataSet)ViewState["gvHorasVoladasCliente"];
			DataTable tblTotalesMes = ds.Tables[1];
			DataTable tblTotal = ds.Tables[2];
			string cadena = string.Empty;

			if (isTotal)
			{
				cadena = "<tr><td colspan='5' class='no-border'></td><td colspan='10' class='no-border' style='border-bottom: 1px solid; border-top:none;'>&nbsp;</td></tr>"
				+ "<tr class='no-border'>"
				+ "<td colspan='4' class='no-border'>&nbsp;</td>"
				+ "<td class='no-border align-right'>TOTAL: </td>"
				+ "<td class='no-border align-right'>" + eliminarCeros(tblTotal.Rows[0]["HrsVueloNormal"].S()) + "</td>"
				+ "<td class='no-border align-right'>" + eliminarCeros(tblTotal.Rows[0]["CostoVueloNormal"].S()) + "</td>"
				+ "<td class='no-border align-right'>" + eliminarCeros(tblTotal.Rows[0]["HrsVueloFerrys"].S()) + "</td>"
				+ "<td class='no-border align-right'>" + eliminarCeros(tblTotal.Rows[0]["CostoVueloFerrys"].S()) + "</td>"
				+ "<td class='no-border align-right'>" + eliminarCeros(tblTotal.Rows[0]["HrsEspera"].S()) + "</td>"
				+ "<td class='no-border align-right'>" + eliminarCeros(tblTotal.Rows[0]["CostoEspera"].S()) + "</td>"
				+ "<td class='no-border align-right'>" + eliminarCeros(tblTotal.Rows[0]["HrsPernocta"].S()) + "</td>"
				+ "<td class='no-border align-right'>" + eliminarCeros(tblTotal.Rows[0]["CostoPernocta"].S()) + "</td>"
				+ "<td class='no-border align-right'>" + eliminarCeros(tblTotal.Rows[0]["TotalHrs"].S()) + "</td>"
				+ "<td class='no-border align-right'>" + eliminarCeros(tblTotal.Rows[0]["TotalCosto"].S()) + "</td>"
				+ "</tr>";
			}
			else
			{
				cadena = "<tr><td colspan='5' class='no-border'></td><td colspan='10' class='no-border' style='border-bottom: 1px solid; border-top:none;'>&nbsp;</td></tr>"
				+ "<tr class='no-border'>"
				+ "<td colspan='4' class='no-border'>&nbsp;</td>"
				+ "<td class='no-border align-right'>DEL MES: </td>"
				+ "<td class='no-border align-right'>" + eliminarCeros(tblTotalesMes.Rows[iMes]["HrsVueloNormal"].S()) + "</td>"
				+ "<td class='no-border align-right'>" + eliminarCeros(tblTotalesMes.Rows[iMes]["CostoVueloNormal"].S()) + "</td>"
				+ "<td class='no-border align-right'>" + eliminarCeros(tblTotalesMes.Rows[iMes]["HrsVueloFerrys"].S()) + "</td>"
				+ "<td class='no-border align-right'>" + eliminarCeros(tblTotalesMes.Rows[iMes]["CostoVueloFerrys"].S()) + "</td>"
				+ "<td class='no-border align-right'>" + eliminarCeros(tblTotalesMes.Rows[iMes]["HrsEspera"].S()) + "</td>"
				+ "<td class='no-border align-right'>" + eliminarCeros(tblTotalesMes.Rows[iMes]["CostoEspera"].S()) + "</td>"
				+ "<td class='no-border align-right'>" + eliminarCeros(tblTotalesMes.Rows[iMes]["HrsPernocta"].S()) + "</td>"
				+ "<td class='no-border align-right'>" + eliminarCeros(tblTotalesMes.Rows[iMes]["CostoPernocta"].S()) + "</td>"
				+ "<td class='no-border align-right'>" + eliminarCeros(tblTotalesMes.Rows[iMes]["TotalHrs"].S()) + "</td>"
				+ "<td class='no-border align-right'>" + eliminarCeros(tblTotalesMes.Rows[iMes]["TotalCosto"].S()) + "</td>"
				+ "</tr>";
			}
			return cadena;
		}

		private string eliminarCeros(string valor)
		{
			if (valor == "00:00" || valor == "0" || valor == "0.0" || valor == "0.00")
				return "&nbsp;";
			else
				return valor;
		}

		public void MostrarMensaje(string sMensaje, string sCaption)
		{
			mpeMensaje.ShowMessage(sMensaje, sCaption);
		}

		public void LoadCliente(DataTable dtObjCat)
		{
			try
			{
				dtObjCat.Rows[0].Delete();
				ViewState["oCliente"] = dtObjCat;
				ddlClientes.DataSource = dtObjCat;
				ddlClientes.TextField = "CodigoCliente";
				ddlClientes.ValueField = "IdCliente";
				ddlClientes.DataBind();
				ddlClientes.SelectedItem = null;
			}
			catch (Exception x) { throw x; }
		}

		public void LoadContrato(DataTable dtObjCat)
		{
			try
			{
				dtObjCat.Rows[0].Delete();
				ViewState["oContrato"] = dtObjCat;
				ddlContrato.DataSource = dtObjCat;
				ddlContrato.TextField = "ClaveContrato";
				ddlContrato.ValueField = "IdContrato";
				ddlContrato.DataBind();
				ddlContrato.SelectedItem = null;
			}
			catch (Exception x) { throw x; }
		}

		public void ObtieneDatosCliente()
		{
			if (eObjCliente != null)
				eObjCliente(null, EventArgs.Empty);
		}

		public void ObtieneDatosContrato()
		{
			if (eObjContrato != null)
				eObjContrato(null, EventArgs.Empty);
		}
		#endregion Metodos

		#region Variables
		HorasVoladasCliente_Presenter oPresenter;
		private const string sClase = "frmHorasVoladasCliente.aspx.cs";
		private const string sPagina = "frmHorasVoladasCliente.aspx";

		public event EventHandler eNewObj;
		public event EventHandler eObjSelected;
		public event EventHandler eSaveObj;
		public event EventHandler eDeleteObj;
		public event EventHandler eSearchObj;
		public event EventHandler eSearchObj2;
		public event EventHandler eObjCliente;
		public event EventHandler eObjContrato;

		public object[] oArrFiltros
		{
			get
			{
				DateTime dFechaDesde = dtFechaDesde.Date == DateTime.MinValue ? DateTime.Parse("1/1/1753 12:00:00") : dtFechaDesde.Date;
				DateTime dFechaHasta = dtFechaHasta.Date == DateTime.MinValue ? DateTime.Parse("1/1/1753 12:00:00") : dtFechaHasta.Date;
				int idCliente = ddlClientes.SelectedItem == null ? 0 : ddlClientes.SelectedItem.Value.I();
				int idContrato = ddlContrato.SelectedItem == null ? 0 : ddlContrato.SelectedItem.Value.I();
				return new object[]
				{
					"@IdCliente", idCliente,
					"@IdContrato", idContrato,
					"@FechaDesde", dFechaDesde,
					"@FechaHasta", dFechaHasta
				};
			}
		}

		public object[] oArrFiltroClientes
		{
			get { return new object[] { "@idCliente", 0 }; }
		}

		public object[] oArrFiltroContrato
		{
			get { return new object[] { "@IdCliente", ddlClientes.SelectedItem.Value }; }
		}
		#endregion Variables

	}
}