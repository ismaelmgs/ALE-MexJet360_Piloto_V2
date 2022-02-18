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
using System.Web.UI.HtmlControls;
using DevExpress.Export;
using System.ComponentModel;
using ALE_MexJet.ControlesUsuario;
using DevExpress.Web.Data;
using DevExpress.Web;
using ALE_MexJet.Clases;
using DevExpress.Utils;
using System.Reflection;
using System.Collections.Specialized;
using System.IO;
using System.Drawing;
using System.Configuration;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace ALE_MexJet.Views.Reportes
{
    public partial class frmDetalleVueloCliente : System.Web.UI.Page, IViewDetalleVuelos
    {
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                oPresenter = new DetalleVuelos_Presenter(this, new DBDetalleVuelos());
                if (!IsPostBack)
                {
                    if (eObjCliente != null)
                        eObjCliente(null, null);

                    txtMatricula.Enabled = false;
                    ddlClientes.Enabled = false;
                    ddlContrato.Enabled = false;
                }
            }
            catch (Exception ex) { Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "Page_Load", "Aviso"); }
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
        protected void UpdatePanel1_Unload(object sender, EventArgs e)
        {
            MethodInfo methodInfo = typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Where(i => i.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel")).First();
            methodInfo.Invoke(ScriptManager.GetCurrent(Page),
            new object[] { sender as UpdatePanel });
        }
        protected void rbSeleccion_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbSeleccion.SelectedItem.Value.Equals("0"))
                {
                    txtMatricula.Enabled = false;
                    txtMatricula.Text = string.Empty;
                    ddlClientes.SelectedItem = null;
                    ddlClientes.Enabled = true;
                    ddlContrato.SelectedItem = null;
                    ddlContrato.Enabled = true;
                }
                else if (rbSeleccion.SelectedItem.Value.Equals("1"))
                {
                    txtMatricula.Enabled = true;
                    txtMatricula.Text = string.Empty;
                    ddlClientes.SelectedItem = null;
                    ddlClientes.Enabled = false;
                    ddlContrato.SelectedItem = null;
                    ddlContrato.Enabled = false;
                }
            }
            catch (Exception ex) { Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "rbSeleccion_SelectedIndexChanged", "Aviso"); }
        }
        protected void btnBusqueda_Click(object sender, EventArgs e)
        {
            try 
            {
                hfFechaInicial["hfFechaInicial"] = dFechaIni.Text == null ? string.Empty : dFechaIni.Text.S();
                hfFechaFinal["hfFechaFinal"] = dFechaFin.Text == null ? string.Empty : dFechaFin.Text.S();
                hfCliente["hfCliente"] = ddlClientes.SelectedItem == null ? string.Empty : ddlClientes.SelectedItem.Text;
                hfContrato["hfContrato"] = ddlContrato.SelectedItem == null ? string.Empty : ddlContrato.SelectedItem.Text;

                if (rbSeleccion.SelectedItem.Value.Equals("0"))
                {
                    if (eSearchObj != null)
                        eSearchObj(null, null);
                }
                else if (rbSeleccion.SelectedItem.Value.Equals("1"))
                {
                    if (eObjMatricula != null)
                        eObjMatricula(null, null);
                }
            }
            catch (Exception ex) { Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnBusqueda_Click", "Aviso"); }
        }
        protected void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                ExportExcel();
            }
            catch (Exception ex) { Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnExcel_Click", "Aviso"); }
        }
        #endregion 

        #region Metodos
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
        public void MostrarMensaje(string sMensaje, string sCaption)
        {
            try
            {
                lbl.Text = sMensaje.S();
                ppAlert.ShowOnPageLoad = true;
            }
            catch (Exception x) { throw x; }
        }
        public void LoadContrato(DataTable dtObjCat)
        {
            try
            {
                dtObjCat.Rows[0].Delete();
                //DataTable DT = dtObjCat.Copy();
                ViewState["LoadContrato"] = dtObjCat;
                ddlContrato.DataSource = dtObjCat;
                ddlContrato.TextField = "ClaveContrato";
                ddlContrato.ValueField = "IdContrato";
                ddlContrato.DataBind();
                ddlContrato.SelectedItem = null;
            }
            catch (Exception x) { throw x; }
        }
        public void LoadVueloCliente(DataTable dtObjCat)
        {
            try
            {
                ViewState["LoadVueloCliente"] = dtObjCat;
                CargaVuelosCliente();
            }
            catch (Exception x) { }
        }
        protected void CargaVuelosCliente()
        {
            try
            {
                DataTable dCliente = (DataTable)ViewState["LoadVueloCliente"];
                if (dCliente != null)
                {
                    string cadena = @"<table border='1' bgColor='#ffffff' borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; background:white; width: 1400px'>" +
                     "<tr ><td colspan='18' style='border-bottom : none; border-left:none; border-top:none; border-right:none; text-align: center; vertical-align: middle'><B>Detalle de Vuelos por Cliente</B></font></td></tr>" +
                    "<tr ><td colspan='6' style='border-left:none; border-right:none; border-bottom : none; text-align: center; vertical-align: middle'></td><td colspan='4' style=' border-left:none; border-right:none; border-bottom : none; text-align: center; vertical-align: middle'><B>VUELO NACIONAL</B></td><td colspan='4' style='border-left:none; border-right:none; border-bottom : none;text-align: center; vertical-align: middle'><B>VUELO INTERNACINAL</B></td>" +
                    "<td colspan='2' style='border-left:none; border-right:none;  text-align: center; border-bottom : none; vertical-align: middle'><B>PERNOCTAS</B></td><td colspan='2' style='border-left:none; border-bottom : none; border-right:none; text-align: center; vertical-align: middle'><B>ESPERA c/CARGO</B></td></tr>" +
                     "<tr><td style='border-bottom : none; border-top: none; border-left:none; border-right:none; text-align: center;'><B>FECHA DE</B></td><td style='border-bottom : none; border-top: none; border-left:none; border-right:none; text-align: center;'><B>FECHA DE</B></td><td style='border-left:none; border-right:none; text-align: center;border-bottom : none; border-top: none;'><B>FECHA DE</B></td><td style='border-bottom : none; border-top: none; border-left:none; border-right:none; text-align: center;'><B>REMISION </B></td><td style='border-bottom : none; border-top: none; border-left:none; border-right:none; text-align: center;'><B>MATRICULA</B></td><td style='border-bottom : none; border-top: none; border-left:none; border-right:none; text-align: center;'><B>TIEMPO</B></td><td style='border-left:none; border-right:none; border-bottom : none; border-top: none; text-align: center; vertical-align: middle;'><B>TIEMPO</B></td><td style='border-left:none; border-right:none; border-bottom : none; border-top: none; text-align: center; vertical-align: middle' ><B>TIEMPO</B></td>" +
                     "<td style='border-left:none; border-right:none; border-bottom : none; border-top: none; text-align: center; vertical-align: middle'><B>DIFERENCIA</B></td><td style='border-left:none; border-right:none; border-bottom : none; border-top: none; text-align: center; vertical-align: middle'><B>ACUMULADO</B></td><td style=' border-bottom : none; border-top: none; border-left:none; border-right:none;text-align: center; vertical-align: middle'><B>TIEMPO</B></td><td style='border-bottom : none; border-top: none; border-left:none; border-right:none; text-align: center;  vertical-align: middle'><B>TIEMPO</B></td><td style='border-bottom : none; border-top: none; border-left:none; border-right:none; text-align: center;'><B>DIFERENCIA</B></td><td style='border-left:none; border-right:none; border-bottom : none; border-top: none; text-align: center; vertical-align: middle'><B>ACUMULADO</B></td><td style='border-bottom : none; border-top: none; border-left:none; border-right:none; text-align: center;'><B>NACIONAL</B></td><td style='border-bottom : none; border-top: none; border-left:none; border-right:none; text-align: center;'><B>INTERNACIONAL</B></td><td style='border-bottom : none; border-top: none; border-left:none; border-right:none; text-align: center;'><B>NACIONAL</B></td><td style='border-bottom : none; border-top: none; border-left:none; border-right:none; text-align: center;'><B>INTERNACIONAL</B></td></tr>" +
                     "<tr><td style='border-left:none; border-top:none; border-right:none; text-align: center;'><B>REMISION</B></td><td style='border-left:none; border-top:none; border-right:none; text-align: center;'><B>SALIDA</B></td><td style='border-left:none; border-top:none; border-right:none; text-align: center;'><B>LLEGADA</B></td><td style='border-left:none; border-top:none; border-right:none; text-align: center;'></td>" +
                     "<td style='border-left:none; border-top:none; border-right:none; text-align: center;'></td><td style='border-left:none; border-top:none; border-right:none; text-align: center;'><B>TOTAL VUELO</B></td><td style='border-left:none; border-top:none; border-right:none; text-align: center;'><B>REAL</B></td><td style='border-left:none; border-top:none; border-right:none; text-align: center;'><B>PACTADO</B></td><td style='border-left:none; border-top:none; border-right:none; text-align: center;' colspan='2'></td><td style='border-left:none; border-top:none; border-right:none; text-align: center;'><B>REAL</B></td><td style='border-left:none; border-top:none; border-right:none; text-align: center;'><B>PACTADO</B></td><td style='border-left:none; border-top:none; border-right:none; text-align: center;' colspan='6'></td></tr>";

                    for (int i = 0; i < dCliente.Rows.Count; i++)
                    {
                        cadena = cadena + "<tr>";
                        for (int c = 1; c < dCliente.Columns.Count; c++)
                        {
                            if (c <= 5)
                                cadena = cadena + "<td style='border-width: 0px; text-align: left;'>" + dCliente.Rows[i][c].S() + "</td>";
                            else
                                cadena = cadena + "<td style='border-width: 0px; text-align: right; '>" + dCliente.Rows[i][c].S() + "&nbsp;&nbsp;</td>";
                        }
                        cadena = cadena + "</tr>";
                    }
                    cadena = cadena + "</table>";

                    dVueloCliente.InnerHtml = cadena;
                    dVueloMatricula.InnerHtml = "";
                }
            }
            catch (Exception x) { throw x; }
        }
        private void ExportExcel()
        {
            try
            {
                string Titulo = string.Empty;
                string Tipo = string.Empty;
                DataTable table = new DataTable();
                if (rbSeleccion.SelectedItem.Value.Equals("0"))
                {
                    table  = (DataTable)ViewState["LoadVueloCliente"];
                    Titulo = "Detalle de Vuelos por Cliente";
                    Tipo = "Cliente: " + hfCliente["hfCliente"].S();
                }
                else if (rbSeleccion.SelectedItem.Value.Equals("1"))
                {
                    table  = (DataTable)ViewState["LoadVueloMatricula"];
                    Titulo = "Detalle de Vuelos por Matricula";
                    Tipo = "Matricula: " + txtMatricula.Text;
                }
                    
                int columnscount = table.Columns.Count;
                int x = columnscount - 3;
                string _ippuertoserver = Utils.GetParametrosClave("53").S();
                    
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.ClearHeaders();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "application/ms-excel";
                HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + Titulo + ".xls");
                HttpContext.Current.Response.Charset = "utf-8";
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");

                HttpContext.Current.Response.Write(@"<table><tr><td style=' text-align: center; vertical-align: middle'><img src='http://" + _ippuertoserver.S() + "/img/mexjet_p.png' height='100px' width='90px'/></td>" +
                 "<td style=' text-align: center; font-size: x-large; vertical-align: middle' colspan='" + x.S() + "'>&nbsp;&nbsp;&nbsp; " + Titulo + "</td><td></td></tr>" +
                 "<tr><td></td><td style='text-align: center;  vertical-align: middle' colspan='" + x.S() + "'>&nbsp;&nbsp;Efectuados del periodo de " + hfFechaInicial["hfFechaInicial"].S() + " al " + hfFechaFinal["hfFechaFinal"].S() + "</td><td></td></tr>" +
                 "<td style='height: 20px;'></td><td style='text-align: center;  vertical-align: middle' colspan='" + x.S() + "'></td><td></td></tr><tr><td>" + Tipo + "</td><td style='text-align: center;  vertical-align: middle' colspan='" + x.S() + "'></td><td></td></tr></table>");

                if (rbSeleccion.SelectedItem.Value.Equals("0"))
                {
                    HttpContext.Current.Response.Write(dVueloCliente.InnerHtml.S());
                }
                else if (rbSeleccion.SelectedItem.Value.Equals("1"))
                {
                    HttpContext.Current.Response.Write(dVueloMatricula.InnerHtml.S());
                }
                
                
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
            catch (Exception x) { throw x; }
        }
        public void LoadVueloMatricula(DataTable dtObjCat)
        {
            try
            {
                ViewState["LoadVueloMatricula"] = dtObjCat;
                CargaVueloMatricula();
            }
            catch (Exception x) {throw x;}
        }
        private void CargaVueloMatricula()
        {
            try
            {
                DataTable dCliente = (DataTable)ViewState["LoadVueloMatricula"];
                if (dCliente != null)
                {
                    string cadena = @"<table border='1' bgColor='#ffffff' borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; background:white; width: 1400px'>" +
                     "<tr ><td colspan='18' style='border-bottom : none; border-left:none; border-top:none; border-right:none; text-align: center; vertical-align: middle'><B>Detalle de Vuelos por Matricula</B></font></td></tr>" +
                    "<tr ><td colspan='6' style='border-left:none; border-right:none; border-bottom : none; text-align: center; vertical-align: middle'></td><td colspan='4' style=' border-left:none; border-right:none; border-bottom : none; text-align: center; vertical-align: middle'><B>VUELO NACIONAL</B></td><td colspan='4' style='border-left:none; border-right:none; border-bottom : none;text-align: center; vertical-align: middle'><B>VUELO INTERNACINAL</B></td>" +
                    "<td colspan='2' style='border-left:none; border-right:none;  text-align: center; border-bottom : none; vertical-align: middle'><B>PERNOCTAS</B></td><td colspan='2' style='border-left:none; border-bottom : none; border-right:none; text-align: center; vertical-align: middle'><B>ESPERA c/CARGO</B></td></tr>" +
                     "<tr><td style='border-bottom : none; border-top: none; border-left:none; border-right:none; text-align: center;'><B>FECHA DE</B></td><td style='border-bottom : none; border-top: none; border-left:none; border-right:none; text-align: center;'><B>FECHA DE</B></td><td style='border-left:none; border-right:none; text-align: center;border-bottom : none; border-top: none;'><B>FECHA DE</B></td><td style='border-bottom : none; border-top: none; border-left:none; border-right:none; text-align: center;'><B>REMISION </B></td><td style='border-bottom : none; border-top: none; border-left:none; border-right:none; text-align: center;'><B>CLIENTE</B></td><td style='border-bottom : none; border-top: none; border-left:none; border-right:none; text-align: center;'><B>TIEMPO</B></td><td style='border-left:none; border-right:none; border-bottom : none; border-top: none; text-align: center; vertical-align: middle;'><B>TIEMPO</B></td><td style='border-left:none; border-right:none; border-bottom : none; border-top: none; text-align: center; vertical-align: middle' ><B>TIEMPO</B></td>" +
                     "<td style='border-left:none; border-right:none; border-bottom : none; border-top: none; text-align: center; vertical-align: middle'><B>DIFERENCIA</B></td><td style='border-left:none; border-right:none; border-bottom : none; border-top: none; text-align: center; vertical-align: middle'><B>ACUMULADO</B></td><td style=' border-bottom : none; border-top: none; border-left:none; border-right:none;text-align: center; vertical-align: middle'><B>TIEMPO</B></td><td style='border-bottom : none; border-top: none; border-left:none; border-right:none; text-align: center;  vertical-align: middle'><B>TIEMPO</B></td><td style='border-bottom : none; border-top: none; border-left:none; border-right:none; text-align: center;'><B>DIFERENCIA</B></td><td style='border-left:none; border-right:none; border-bottom : none; border-top: none; text-align: center; vertical-align: middle'><B>ACUMULADO</B></td><td style='border-bottom : none; border-top: none; border-left:none; border-right:none; text-align: center;'><B>NACIONAL</B></td><td style='border-bottom : none; border-top: none; border-left:none; border-right:none; text-align: center;'><B>INTERNACIONAL</B></td><td style='border-bottom : none; border-top: none; border-left:none; border-right:none; text-align: center;'><B>NACIONAL</B></td><td style='border-bottom : none; border-top: none; border-left:none; border-right:none; text-align: center;'><B>INTERNACIONAL</B></td></tr>" +
                     "<tr><td style='border-left:none; border-top:none; border-right:none; text-align: center;'><B>REMISION</B></td><td style='border-left:none; border-top:none; border-right:none; text-align: center;'><B>SALIDA</B></td><td style='border-left:none; border-top:none; border-right:none; text-align: center;'><B>LLEGADA</B></td><td style='border-left:none; border-top:none; border-right:none; text-align: center;'></td>" +
                     "<td style='border-left:none; border-top:none; border-right:none; text-align: center;'></td><td style='border-left:none; border-top:none; border-right:none; text-align: center;'><B>TOTAL VUELO</B></td><td style='border-left:none; border-top:none; border-right:none; text-align: center;'><B>REAL</B></td><td style='border-left:none; border-top:none; border-right:none; text-align: center;'><B>PACTADO</B></td><td style='border-left:none; border-top:none; border-right:none; text-align: center;' colspan='2'></td><td style='border-left:none; border-top:none; border-right:none; text-align: center;'><B>REAL</B></td><td style='border-left:none; border-top:none; border-right:none; text-align: center;'><B>PACTADO</B></td><td style='border-left:none; border-top:none; border-right:none; text-align: center;' colspan='6'></td></tr>";

                    for (int i = 0; i < dCliente.Rows.Count; i++)
                    {
                        cadena = cadena + "<tr>";
                        for (int c = 1; c < dCliente.Columns.Count; c++)
                        {
                            if (c <= 5)
                                cadena = cadena + "<td style='border-width: 0px; text-align: left;'>" + dCliente.Rows[i][c].S() + "</td>";
                            else
                                cadena = cadena + "<td style='border-width: 0px; text-align: right; '>" + dCliente.Rows[i][c].S() + "&nbsp;&nbsp;</td>";
                        }
                        cadena = cadena + "</tr>";
                    }
                    cadena = cadena + "</table>";

                    dVueloCliente.InnerHtml = "";
                    dVueloMatricula.InnerHtml = cadena;
                }
            }
            catch (Exception x) { throw x; }
        }
        #endregion

        #region Variables

        private const string sClase = "frmDetalleVueloCliente.aspx.cs";
        private const string sPagina = "frmDetalleVueloCliente.aspx";

        DetalleVuelos_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eObjCliente;
        public event EventHandler eObjContrato;
        public event EventHandler eObjMatricula;

        public object[] oArrFiltros
        {
            get
            {
                return new object[]
                { 
                    "@IdCliente", ddlClientes.SelectedItem.Value.S()
                };
            }
        }
        public object[] oArrCliente
        {
            get
            {
                return new object[]
                {   
                    "@IdContrato", ddlContrato.SelectedItem.Value.S(),
                    "@FechaInicio",dFechaIni.Date.ToString("MM-dd-yyyy"),
                    "@FechaFin", dFechaFin.Date.ToString("MM-dd-yyyy"),
                };
            }
        }
        public object[] oArrMatricula
        {
            get
            {
                return new object[]
                { 
                    "@Matricula", txtMatricula.Text.S(),
                    "@FechaInicio",dFechaIni.Date.ToString("MM-dd-yyyy"),
                    "@FechaFin", dFechaFin.Date.ToString("MM-dd-yyyy"),
                };
            }
        }
       
        #endregion

    }
}