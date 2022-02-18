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
    public partial class frmHorasVueloMatricula : System.Web.UI.Page, IViewHoraVueloMatricula
    {
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                oPresenter = new HoraVueloMatricula_Presenter(this, new DBHoraVueloMatricula());
                if (!IsPostBack)
                {
                    if (eObjCliente != null)
                        eObjCliente(null, null);

                    if (eObjFlota != null)
                        eObjFlota(null, null);

                    txtMatricula.Enabled = false;
                    ddlClientes.Enabled = false;
                    ddlContrato.Enabled = false;
                    ddlFlota.Enabled = false;
                }
            }
            catch (Exception ex) { Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "Page_Load", "Aviso"); }
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
                    txtMatricula.Text = "";
                    ddlClientes.Enabled = true;
                    ddlContrato.Enabled = true;
                    ddlFlota.Enabled = false;
                    ddlFlota.SelectedItem = null;
                }
                else if (rbSeleccion.SelectedItem.Value.Equals("1"))
                {
                    txtMatricula.Enabled = true;
                    ddlClientes.Enabled = false;
                    ddlClientes.SelectedItem = null;
                    ddlContrato.Enabled = false;
                    ddlContrato.SelectedItem = null;
                    ddlFlota.Enabled = false;
                    ddlFlota.SelectedItem = null;
                }
                else if (rbSeleccion.SelectedItem.Value.Equals("2"))
                {
                    txtMatricula.Enabled = false;
                    txtMatricula.Text = "";
                    ddlClientes.Enabled = false;
                    ddlClientes.SelectedItem = null;
                    ddlContrato.Enabled = false;
                    ddlContrato.SelectedItem = null;
                    ddlFlota.Enabled = true;

                }
            }
            catch (Exception ex) { Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "rbSeleccion_SelectedIndexChanged", "Aviso"); }
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
                hfFechaInicial["hfFechaInicial"] = dFechaIni.Text == null ? string.Empty : dFechaIni.Text.S();
                hfFechaFinal["hfFechaFinal"] = dFechaFin.Text == null ? string.Empty : dFechaFin.Text.S();
                hfCliente["hfCliente"] = ddlClientes.SelectedItem == null ? string.Empty : ddlClientes.SelectedItem.Text;
                hfContrato["hfContrato"] = ddlContrato.SelectedItem == null ? string.Empty : ddlContrato.SelectedItem.Text;
                hfFlota["hfFlota"] = ddlFlota.SelectedItem == null ? string.Empty : ddlFlota.SelectedItem.Text;

                if (rbSeleccion.SelectedItem.Value.Equals("0"))
                {
                    if (eObjConsultaCliente != null)
                        eObjConsultaCliente(null, null);
                }
                else if (rbSeleccion.SelectedItem.Value.Equals("1"))
                {
                    if (eObjConsultaMatricula != null)
                        eObjConsultaMatricula(null, null);
                }
                else if (rbSeleccion.SelectedItem.Value.Equals("2"))
                {
                    if (eObjConsultaFlota != null)
                        eObjConsultaFlota(null, null);
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
        private void ExportExcel()
        {
            try
            {
                DateTime localDate = DateTime.Now;
                string Titulo = string.Empty;
                string Tipo = string.Empty;
                DataTable table = new DataTable();
                if (rbSeleccion.SelectedItem.Value.Equals("0"))
                {
                    table = (DataTable)ViewState["LoadConsultaCliente"];
                    Titulo = "HORAS VOLADAS POR CLIENTE";
                    Tipo = "Cliente: " + hfCliente["hfCliente"].S() + " <br /> Contrato: " + hfContrato["hfContrato"].S();
                }
                else if (rbSeleccion.SelectedItem.Value.Equals("1"))
                {
                    table = (DataTable)ViewState["LoadConsultaMatricula"];
                    Titulo = "HORAS VOLADAS POR MATRICULA";
                    Tipo = "Matricula: " + txtMatricula.Text;
                }
                else if (rbSeleccion.SelectedItem.Value.Equals("2"))
                {
                    table = (DataTable)ViewState["LoadConsultaFlota"];
                    Titulo = "HORAS VOLADAS POR FLOTA";
                    Tipo = "Flota: " + hfFlota["hfFlota"].S();
                }

                int columnscount = table.Columns.Count + 1;
                int x = columnscount - 4;
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

                HttpContext.Current.Response.Write(@"<table><tr><td colspan ='2' style=' text-align: center; vertical-align: middle'><img src='http://" + _ippuertoserver.S() + "/img/mexjet_p.png' height='100px' width='90px'/></td>" +
                 "<td style=' text-align: center; font-size: x-large; vertical-align: middle' colspan='" + x.S() + "'>Aerolineas Ejecutivas S.A. de C.V.</td><td>" + localDate + "</td></tr>" +
                 "<tr><td></td><td style='text-align: center;  vertical-align: middle' colspan='" + x.S() + "'>&nbsp;&nbsp;HORAS VOLADAS POR MATRICULA</td><td></td></tr>" +
                 "<td style='height: 20px;'></td><td style='text-align: center;  vertical-align: middle' colspan='" + x.S() + "'></td><td></td></tr><tr><td></td><td style='text-align: center;  vertical-align: middle' colspan='" + (x + 1) + "'></td><td>" + Tipo + "</td></tr></table>");

                if (rbSeleccion.SelectedItem.Value.Equals("0"))
                {
                    HttpContext.Current.Response.Write(dVueloCliente.InnerHtml.S());
                }
                else if (rbSeleccion.SelectedItem.Value.Equals("1"))
                {
                    HttpContext.Current.Response.Write(dVueloMatricula.InnerHtml.S());
                }
                else if (rbSeleccion.SelectedItem.Value.Equals("2"))
                {
                    HttpContext.Current.Response.Write(dVueloFlota.InnerHtml.S());
                }


                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
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
                ViewState["LoadContrato"] = dtObjCat;
                ddlContrato.DataSource = dtObjCat;
                ddlContrato.TextField = "ClaveContrato";
                ddlContrato.ValueField = "IdContrato";
                ddlContrato.DataBind();
                ddlContrato.SelectedItem = null;
            }
            catch (Exception x) { throw x; }
        }
        public void LoadConsultaCliente(DataTable dtObjCat)
        {
            try
            {
                ViewState["LoadConsultaCliente"] = dtObjCat;
                CargaConsultaCliente();
            }
            catch (Exception x) { }
        }
        protected void CargaConsultaCliente()
        {
            try
            {
                DataTable dCliente = (DataTable)ViewState["LoadConsultaCliente"];
                if (dCliente != null)
                {
                    string cadena = @"<table border='1' bgColor='#ffffff' borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; background:white; width: 100%'>" +
                     "<tr><td style='border-left:none; border-right:none; text-align: left;'><B>&nbsp;&nbsp;FECHA</B></td><td style='border-left:none; border-right:none; text-align: left;'><B>&nbsp;&nbsp;REMISION</B></td><td style='border-left:none; border-right:none; text-align: left;'><B>&nbsp;&nbsp;CLIENTE</B></td>" +
                    "<td style='border-left:none; border-right:none; text-align: left;'><B>&nbsp;&nbsp;RUTA DE VUELO</B></td><td style='border-left:none; border-right:none; text-align: center;'><B>&nbsp;&nbsp;TIEMPO CALZO</B></td><td style='border-left:none; border-right:none; text-align: center;'><B>&nbsp;&nbsp;TIEMPO VUELO</B></td>" +
                    "<td style='border-left:none; border-right:none;text-align: center; vertical-align: middle;'><B>&nbsp;&nbsp;IMPORTES&nbsp;&nbsp;</B></td></tr>";

                    for (int i = 0; i < dCliente.Rows.Count; i++)
                    {
                        cadena = cadena + "<tr>";
                        for (int c = 0; c < dCliente.Columns.Count; c++)
                        {
                            if (c <= 3)
                                cadena = cadena + "<td style='border-width: 0px; text-align: left;'> &nbsp;&nbsp;" + dCliente.Rows[i][c].S() + "&nbsp;&nbsp;</td>";
                            else
                                if (dCliente.Rows[i]["Ruta"].S().Equals("TIEMPO TOTAL DE VUELO POR MATRICULA") || dCliente.Rows[i]["Ruta"].S().Equals("TIEMPO TOTAL FINAL DE VUELO"))
                                {
                                    if (dCliente.Columns[c].ColumnName.S().Equals("TiempoCalzo") || dCliente.Columns[c].ColumnName.S().Equals("TiempoVuelo") || dCliente.Columns[c].ColumnName.S().Equals("Importe"))
                                    {
                                        cadena = cadena + "<td style='border-left:none; border-right:none;border-bottom: none; text-align: right; '> &nbsp;&nbsp;" + dCliente.Rows[i][c].S() + "&nbsp;&nbsp;&nbsp;&nbsp;</td>";
                                    }
                                }
                                else
                                    cadena = cadena + "<td style='border-width: 0px; text-align: right;'> &nbsp;&nbsp;" + dCliente.Rows[i][c].S() + "&nbsp;&nbsp;&nbsp;&nbsp;</td>";
                        }
                        cadena = cadena + "</tr>";
                    }
                    cadena = cadena + "</table>";

                    dVueloCliente.InnerHtml = cadena;
                    dVueloMatricula.InnerHtml = "";
                    dVueloFlota.InnerHtml = "";
                }
            }
            catch (Exception x) { throw x; }
        }
        public void LoadConsultaMatricula(DataTable dtObjCat)
        {
            try
            {
                ViewState["LoadConsultaMatricula"] = dtObjCat;
                CargaConsultaMatricula();
            }
            catch (Exception x) { throw x; }
        }
        protected void CargaConsultaMatricula()
        {
            try
            {
                DataTable dCliente = (DataTable)ViewState["LoadConsultaMatricula"];
                if (dCliente != null)
                {
                    string cadena = @"<table border='1' bgColor='#ffffff' borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; background:white; width: 100%'>" +
                     "<tr><td style='border-left:none; border-right:none; text-align: left;'><B>&nbsp;&nbsp;FECHA</B></td><td style='border-left:none; border-right:none; text-align: left;'><B>&nbsp;&nbsp;REMISION</B></td><td style='border-left:none; border-right:none; text-align: left;'><B>&nbsp;&nbsp;CLIENTE</B></td>" +
                    "<td style='border-left:none; border-right:none; text-align: left;'><B>&nbsp;&nbsp;RUTA DE VUELO</B></td><td style='border-left:none; border-right:none; text-align: center;'><B>&nbsp;&nbsp;TIEMPO CALZO</B></td><td style='border-left:none; border-right:none; text-align: center;'><B>&nbsp;&nbsp;TIEMPO VUELO</B></td>" +
                    "<td style='border-left:none; border-right:none;text-align: center; vertical-align: middle;'><B>&nbsp;&nbsp;IMPORTES&nbsp;&nbsp;</B></td></tr>";

                    for (int i = 0; i < dCliente.Rows.Count; i++)
                    {
                        cadena = cadena + "<tr>";
                        for (int c = 0; c < dCliente.Columns.Count; c++)
                        {
                            if (c <= 3)
                                cadena = cadena + "<td style='border-width: 0px; text-align: left;'> &nbsp;&nbsp;" + dCliente.Rows[i][c].S() + "&nbsp;&nbsp;</td>";
                            else
                                if (dCliente.Rows[i]["Ruta"].S().Equals("TIEMPO TOTAL DE VUELO POR MATRICULA") || dCliente.Rows[i]["Ruta"].S().Equals("TIEMPO TOTAL FINAL DE VUELO"))
                                {
                                    if (dCliente.Columns[c].ColumnName.S().Equals("TiempoCalzo") || dCliente.Columns[c].ColumnName.S().Equals("TiempoVuelo") || dCliente.Columns[c].ColumnName.S().Equals("Importe"))
                                    {
                                        cadena = cadena + "<td style='border-left:none; border-right:none;border-bottom: none; text-align: right; '> &nbsp;&nbsp;" + dCliente.Rows[i][c].S() + "&nbsp;&nbsp;&nbsp;&nbsp;</td>";   
                                    }
                                }
                                else
                                {
                                    cadena = cadena + "<td style='border-width: 0px; text-align: right; '> &nbsp;&nbsp;" + dCliente.Rows[i][c].S() + "&nbsp;&nbsp;&nbsp;&nbsp;</td>";  
                                }
                        }
                        cadena = cadena + "</tr>";
                    }
                    cadena = cadena + "</table>";

                    dVueloMatricula.InnerHtml = cadena;
                    dVueloCliente.InnerHtml = "";
                    dVueloFlota.InnerHtml = "";
                }
            }
            catch (Exception x) { throw x; }
        }
        public void LoadFlota(DataTable dtObjCat)
        {
            try
            {
                ViewState["LoadFlota"] = dtObjCat;
                ddlFlota.DataSource = dtObjCat;
                ddlFlota.TextField = "DescripcionFlota";
                ddlFlota.ValueField = "IdFlota";
                ddlFlota.DataBind();
            }
            catch (Exception x) { throw x; }
        }
        public void LoadConsultaFlota(DataTable dtObjCat)
        {
            try
            {
                ViewState["LoadConsultaFlota"] = dtObjCat;
                CargaConsultaFlota();
            }
            catch (Exception x) { throw x; }
        }
        protected void CargaConsultaFlota()
        {
            try
            {
                DataTable dCliente = (DataTable)ViewState["LoadConsultaFlota"];
                if (dCliente != null)
                {
                    string cadena = @"<table border='1' bgColor='#ffffff' borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; background:white; width: 100%'>" +
                    "<tr><td style='border-left:none; border-right:none; text-align: left;'><B>&nbsp;&nbsp;FECHA</B></td><td style='border-left:none; border-right:none; text-align: left;'><B>&nbsp;&nbsp;REMISION</B></td><td style='border-left:none; border-right:none; text-align: left;'><B>&nbsp;&nbsp;CLIENTE</B></td>" +
                    "<td style='border-left:none; border-right:none; text-align: left;'><B>&nbsp;&nbsp;RUTA DE VUELO</B></td><td style='border-left:none; border-right:none; text-align: center;'><B>&nbsp;&nbsp;TIEMPO CALZO</B></td><td style='border-left:none; border-right:none; text-align: center;'><B>&nbsp;&nbsp;TIEMPO VUELO</B></td>" +
                    "<td style='border-left:none; border-right:none; text-align: center; vertical-align: middle;'><B>&nbsp;&nbsp;IMPORTES&nbsp;&nbsp;</B></td></tr>";

                    for (int i = 0; i < dCliente.Rows.Count; i++)
                    {
                        cadena = cadena + "<tr>";
                        for (int c = 0; c < dCliente.Columns.Count; c++)
                        {
                            if (c <= 3)
                                cadena = cadena + "<td style='border-width: 0px; text-align: left;'> &nbsp;&nbsp;" + dCliente.Rows[i][c].S() + "&nbsp;&nbsp;</td>";
                            else
                                if (dCliente.Rows[i]["Ruta"].S().Equals("TIEMPO TOTAL DE VUELO POR MATRICULA") || dCliente.Rows[i]["Ruta"].S().Equals("TIEMPO TOTAL FINAL DE VUELO"))
                                {
                                    if (dCliente.Columns[c].ColumnName.S().Equals("TiempoCalzo") || dCliente.Columns[c].ColumnName.S().Equals("TiempoVuelo") || dCliente.Columns[c].ColumnName.S().Equals("Importe"))
                                    {
                                        cadena = cadena + "<td style='border-left:none; border-right:none;border-bottom: none; text-align: right; '> &nbsp;&nbsp;" + dCliente.Rows[i][c].S() + "&nbsp;&nbsp;&nbsp;&nbsp;</td>";
                                    }
                                }
                                else
                                    cadena = cadena + "<td style='border-width: 0px; text-align: right;'> &nbsp;&nbsp;" + dCliente.Rows[i][c].S() + "&nbsp;&nbsp;&nbsp;&nbsp;</td>";
                        }
                        cadena = cadena + "</tr>";
                    }
                    cadena = cadena + "</table>";

                    dVueloCliente.InnerHtml = "";
                    dVueloMatricula.InnerHtml = "";
                    dVueloFlota.InnerHtml = cadena;
                }
            }
            catch (Exception x) { throw x; }
        }
        #endregion

        #region Variables

        private const string sClase = "frmHorasVueloMatricula.aspx.cs";
        private const string sPagina = "frmHorasVueloMatricula.aspx";

        HoraVueloMatricula_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eObjCliente;
        public event EventHandler eObjContrato;
        public event EventHandler eObjMatricula;
        public event EventHandler eObjConsultaCliente;
        public event EventHandler eObjConsultaMatricula;
        public event EventHandler eObjFlota;
        public event EventHandler eObjConsultaFlota;

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
        public object[] oArrClient
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
                    "@Matricu", txtMatricula.Text.S(),
                    "@FechaInicio",dFechaIni.Date.ToString("MM-dd-yyyy"),
                    "@FechaFin", dFechaFin.Date.ToString("MM-dd-yyyy"),
                };
            }
        }
        public object[] oArrFlota
        {
            get
            {
                return new object[]
                { 
                    "@IdFlota",  ddlFlota.SelectedItem.Value,
                    "@FechaInicio",dFechaIni.Date.ToString("MM-dd-yyyy"),
                    "@FechaFin", dFechaFin.Date.ToString("MM-dd-yyyy"),
                };
            }
        }

        #endregion

    }
}