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

using System.IO;
using System.Drawing;
using System.Configuration;

namespace ALE_MexJet.Views.Consultas
{
    public partial class frmDocPendientes : System.Web.UI.Page, IViewDocPendientes
    {
        #region E V E N T O S
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                gvDocPend.SettingsText.SearchPanelEditorNullText = "Ingresa la información a buscar.";
                oPresenter = new DocPendientes_Presenter(this, new DBDocPendientes());
                LlenaGrid();
                LLegaGridRem();
                LlenaPreFac();
                if (!IsPostBack)
                {
                    if (eObjCliente != null)
                        eObjCliente(null, null);

                    gvDocPend.Visible = false;
                    gvRemisiones.Visible = false;
                    gvPreFactura.Visible = false;
                }
            }
            catch (Exception ex){Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "Page_Load", "Aviso");  }
        }
        protected void btnBusqueda_Click(object sender, EventArgs e)
        {
            try
            {
                hfFiltro["hfFiltro"] = rblFiltro.SelectedItem != null ? rblFiltro.SelectedItem.Text : string.Empty;
                hfCliente["hfCliente"] = ddlClientes.SelectedItem != null ? ddlClientes.SelectedItem.Text : "Todos";
                hfContrato["hfContrato"] = ddlContrato.SelectedItem != null ? ddlContrato.SelectedItem.Text : "Todos";

                //gvDocPend.Columns["Prueba"].Visible = false;
                if (rblFiltro.SelectedItem != null && rblFiltro.SelectedItem.Value.S() == "1")
                {      
                    if (eObjBitPend != null)
                        eObjBitPend(null, null);

                    gvDocPend.Visible = true;
                    gvRemisiones.Visible = false;
                    gvPreFactura.Visible = false;
                }
                else if (rblFiltro.SelectedItem != null && rblFiltro.SelectedItem.Value.S() == "2")
                {
                    if (eObjRemPend != null)
                        eObjRemPend(null, null);

                    gvDocPend.Visible = false;
                    gvRemisiones.Visible = true;
                    gvPreFactura.Visible = false;
                }
                else if (rblFiltro.SelectedItem != null && rblFiltro.SelectedItem.Value.S() == "3")
                {
                    if (eObjFacPend != null)
                        eObjFacPend(null, null);

                    gvDocPend.Visible = false;
                    gvRemisiones.Visible = false;
                    gvPreFactura.Visible = true;
                }
                else if (rblFiltro.SelectedItem == null )
                {
                    gvDocPend.Visible=false;
                    gvRemisiones.Visible = false;
                    gvPreFactura.Visible = false;
                }
            }
            catch (Exception ex){Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnBusqueda_Click", "Aviso");}
        }
        protected void UpdatePanel1_Unload(object sender, EventArgs e)
        {
            try
            {
                MethodInfo methodInfo = typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Where(i => i.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel")).First();
                methodInfo.Invoke(ScriptManager.GetCurrent(Page),
                new object[] { sender as UpdatePanel });
            }
            catch (Exception ex){ Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "UpdatePanel1_Unload", "Aviso");}
        }
        protected void ddlClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (eObjConrato != null)
                    eObjConrato(null, null);
            }
            catch (Exception ex) { Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "ddlClientes_SelectedIndexChanged", "Aviso"); }
        }
        protected void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (rblFiltro.SelectedItem != null && rblFiltro.SelectedItem.Value.S() == "1")
                    ExportExcel();
                else if (rblFiltro.SelectedItem != null && rblFiltro.SelectedItem.Value.S() == "2")
                    ExportaRemPen();
                else if (rblFiltro.SelectedItem != null && rblFiltro.SelectedItem.Value.S() == "3")
                    ExportaPreFacPen();
            }
            catch (Exception ex) { Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnExcel_Click", "Aviso"); }
        }
        protected void rblFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                switch (rblFiltro.SelectedItem.Value.S())
                {
                    case "1":
                        gvDocPend.Visible = true;
                        gvRemisiones.Visible = false;
                        break;

                    case "2":
                        gvDocPend.Visible = false;
                        gvRemisiones.Visible = true;
                        break;

                    case "3":
                        gvDocPend.Visible = false;
                        gvRemisiones.Visible = false;
                        break;
                }
            }
            catch (Exception ex) { Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "rblFiltro_SelectedIndexChanged", "Aviso"); }
        }
        #endregion

        #region M E T O D O S
        protected void AgregaCoumnas()
        {
            try
            {
                if (rblFiltro != null && rblFiltro.SelectedItem.Value.S() == "1")
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Hola");

                    gvDocPend.Columns["Adios"].Visible = false;
                    gvDocPend.Columns["Hola"].Visible = true;
                    gvDocPend.DataSource = dt;


                    gvDocPend.DataBind();
                }
                else
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Adios");

                    gvDocPend.Columns["Adios"].Visible = true;
                    gvDocPend.Columns["Hola"].Visible = false;
                    gvDocPend.DataSource = dt;

                    gvDocPend.DataBind();
                }
            }
            catch (Exception x) { throw x; }
        }
        public void LoadCliente(DataTable dtObjCat)
        {
            try
            {
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
                mpeMensaje.ShowMessage(sMensaje, sCaption);
            }
            catch (Exception x) { throw x; }
        }
        public void LoadContrato(DataTable dtObjCat)
        {
            try
            {
                ViewState["oContrato"] = dtObjCat;
                ddlContrato.DataSource = dtObjCat;
                ddlContrato.TextField = "ClaveContrato";
                ddlContrato.ValueField = "IdContrato";
                ddlContrato.DataBind();
                ddlContrato.SelectedItem = null;
            }
            catch (Exception x) { throw x; }
        }
        public void LoadBitPend(DataTable dtObjB)
        {
            try
            {
                ViewState["oBitPendiente"] = dtObjB;
                gvDocPend.DataSource = dtObjB;
                gvDocPend.DataBind();
            }
            catch (Exception x) { throw x; }
        }
        protected void LlenaGrid()
        {
            try
            {
                if (ViewState["oBitPendiente"] != null)
                {
                    gvDocPend.DataSource = (DataTable)ViewState["oBitPendiente"];
                    gvDocPend.DataBind();
                }
            }
            catch (Exception x) { throw x; }
        }
        protected void LLegaGridRem()
        {
            try
            {
                if (ViewState["gvRemisiones"] != null)
                {
                    gvRemisiones.DataSource = (DataTable)ViewState["gvRemisiones"];
                    gvRemisiones.DataBind();
                }
            }
            catch (Exception x) { throw x; }
        }
        private void ExportExcel()
        {
            try
            {
                gvDocPend.DataBind();
                DataTable table = (DataTable)gvDocPend.DataSource;
                if (table == null || table.Rows.Count == 0)
                    return;

                int columnscount = table.Columns.Count;
                int x = columnscount - 2;
                string _ippuertoserver = Request.Url.Authority;

                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.ClearHeaders();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "application/ms-excel";
                HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + hfFiltro["hfFiltro"].S() + ".xls");
                HttpContext.Current.Response.Charset = "utf-8";
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");

                HttpContext.Current.Response.Write(@"<table><tr><td style=' text-align: center; vertical-align: middle'><img src='http://" + _ippuertoserver.S() + "/img/mexjet_p.png' height='100px' width='90px'/></td>" +
                 "<td style=' text-align: center; font-size: x-large; vertical-align: middle' colspan='" + x.S() + "'>&nbsp;&nbsp;&nbsp; Aerolineas Ejecutivas S.A. de C.V.</td><td>Clientes: " + hfCliente["hfCliente"].S() + "</td></tr>" +
                 "<tr><td></td><td style='text-align: center; font-size: large; vertical-align: middle' colspan='" + x.S() + "'>&nbsp;&nbsp;" + hfFiltro["hfFiltro"].S() + "</td><td>Contrato: " + hfContrato["hfContrato"].S() + "</td></tr>" +
                 "<td colspan='" + columnscount.S() + "' style=' text-align: center;'>" + ViewState["FechaIni"].S() + " - " + ViewState["FechaFin"].S() + "</td></table>");

                HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; background:white;'>");
                HttpContext.Current.Response.Write("<hr style='background-color: #000000' />");

                HttpContext.Current.Response.Write("<TR>");
                for (int j = 0; j < columnscount; j++)
                {
                    HttpContext.Current.Response.Write("<Td style='border-width: 1px 0px 1px 0px'>");
                    HttpContext.Current.Response.Write("<B>");
                    HttpContext.Current.Response.Write(gvDocPend.Columns[j].Caption.S());
                    HttpContext.Current.Response.Write("</B>");
                    HttpContext.Current.Response.Write("</Td>");
                }
                HttpContext.Current.Response.Write("</TR>");

                foreach (DataRow row in table.Rows)
                {
                    HttpContext.Current.Response.Write("<TR>");
                    for (int i = 0; i < table.Columns.Count; i++)
                    {
                        HttpContext.Current.Response.Write("<Td style='border-width: 0px'>");
                        HttpContext.Current.Response.Write(row[i].ToString());
                        HttpContext.Current.Response.Write("</Td>");
                    }
                    HttpContext.Current.Response.Write("</TR>");
                }
                HttpContext.Current.Response.Write("</Table> </font>");
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
            catch (Exception x) { throw x; }
        }
        private void ExportaRemPen()
        {
            try
            {
                DataTable table = (DataTable)gvRemisiones.DataSource;
                if (table == null || table.Rows.Count == 0)
                    return;

                int columnscount = table.Columns.Count;
                int x = columnscount - 2;
                string _ippuertoserver = Request.Url.Authority;

                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.ClearHeaders();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "application/ms-excel";
                HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + hfFiltro["hfFiltro"].S() + ".xls");
                HttpContext.Current.Response.Charset = "utf-8";
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");

                HttpContext.Current.Response.Write(@"<table><tr><td style=' text-align: center; vertical-align: middle'><img src='http://" + _ippuertoserver.S() + "/img/mexjet_p.png' height='100px' width='90px'/></td>" +
                 "<td style=' text-align: center; font-size: x-large; vertical-align: middle' colspan='" + x.S() + "'>&nbsp;&nbsp;&nbsp; Aerolineas Ejecutivas S.A. de C.V.</td><td>Clientes: " + hfCliente["hfCliente"].S() + "</td></tr>" +
                 "<tr><td></td><td style='text-align: center; font-size: large; vertical-align: middle' colspan='" + x.S() + "'>&nbsp;&nbsp;" + hfFiltro["hfFiltro"].S() + "</td><td>Contrato: " + hfContrato["hfContrato"].S() + "</td></tr>" +
                 "<td colspan='" + columnscount.S() + "' style=' text-align: center;'>" + ViewState["FechaIni"].S() + " - " + ViewState["FechaFin"].S() + "</td></table>");

                HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; background:white;'>");
                HttpContext.Current.Response.Write("<hr style='background-color: #000000' />");

                HttpContext.Current.Response.Write("<TR>");
                for (int j = 0; j < columnscount; j++)
                {
                    HttpContext.Current.Response.Write("<Td style='border-width: 1px 0px 1px 0px'>");
                    HttpContext.Current.Response.Write("<B>");
                    HttpContext.Current.Response.Write(gvRemisiones.Columns[j].Caption.S());
                    HttpContext.Current.Response.Write("</B>");
                    HttpContext.Current.Response.Write("</Td>");
                }
                HttpContext.Current.Response.Write("</TR>");

                foreach (DataRow row in table.Rows)
                {
                    HttpContext.Current.Response.Write("<TR>");
                    for (int i = 0; i < table.Columns.Count; i++)
                    {
                        HttpContext.Current.Response.Write("<Td style='border-width: 0px'>");
                        HttpContext.Current.Response.Write(row[i].ToString());
                        HttpContext.Current.Response.Write("</Td>");
                    }
                    HttpContext.Current.Response.Write("</TR>");
                }
                HttpContext.Current.Response.Write("</Table> </font>");
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
            catch (Exception x) { throw x; }
        }
        private void ExportaPreFacPen()
        {
            try
            {
                DataTable table = (DataTable)gvPreFactura.DataSource;
                if (table == null || table.Rows.Count == 0)
                    return;

                int columnscount = table.Columns.Count;
                int x = columnscount - 2;
                string _ippuertoserver = Request.Url.Authority;

                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.ClearHeaders();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "application/ms-excel";
                HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + hfFiltro["hfFiltro"].S() + ".xls");
                HttpContext.Current.Response.Charset = "utf-8";
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");

                HttpContext.Current.Response.Write(@"<table><tr><td style=' text-align: center; vertical-align: middle'><img src='http://" + _ippuertoserver.S() + "/img/mexjet_p.png' height='100px' width='90px'/></td>" +
                 "<td style=' text-align: center; font-size: x-large; vertical-align: middle' colspan='" + x.S() + "'>&nbsp;&nbsp;&nbsp; Aerolineas Ejecutivas S.A. de C.V.</td><td>Clientes: " + hfCliente["hfCliente"].S() + "</td></tr>" +
                 "<tr><td></td><td style='text-align: center; font-size: large; vertical-align: middle' colspan='" + x.S() + "'>&nbsp;&nbsp;" + hfFiltro["hfFiltro"].S() + "</td><td>Contrato: " + hfContrato["hfContrato"].S() + "</td></tr>" +
                 "<td colspan='" + columnscount.S() + "' style=' text-align: center;'>" + ViewState["FechaIni"].S() + " - " + ViewState["FechaFin"].S() + "</td></table>");

                HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; background:white;'>");
                HttpContext.Current.Response.Write("<hr style='background-color: #000000' />");

                HttpContext.Current.Response.Write("<TR>");
                for (int j = 0; j < gvRemisiones.Columns.Count; j++)
                {
                    HttpContext.Current.Response.Write("<Td style='border-width: 1px 0px 1px 0px'>");
                    HttpContext.Current.Response.Write("<B>");
                    HttpContext.Current.Response.Write(gvPreFactura.Columns[j].Caption.S());
                    HttpContext.Current.Response.Write("</B>");
                    HttpContext.Current.Response.Write("</Td>");
                }
                HttpContext.Current.Response.Write("</TR>");

                for (int z = 0; z < gvPreFactura.VisibleRowCount; z++)
                {
                    DataRow r = gvPreFactura.GetDataRow(z);
                    HttpContext.Current.Response.Write("<TR>");
                    for (int i = 0; i < x; i++)
                    {

                        HttpContext.Current.Response.Write("<Td style='border-width: 0px'>");
                        HttpContext.Current.Response.Write(r[i].ToString());
                        HttpContext.Current.Response.Write("</Td>");
                    }
                    HttpContext.Current.Response.Write("</TR>");
                }
                HttpContext.Current.Response.Write("</Table> </font>");
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
            catch (Exception x) { throw x; }
        }
        public void LoadRemPend(DataTable DT)
        {
            try
            {
                gvRemisiones.DataSource = DT;
                gvRemisiones.DataBind();
                ViewState["gvRemisiones"] = DT;
            }
            catch (Exception x) { throw x; }
        }
        public void LoadFacPend(DataTable dtObjCat)
        {
            gvPreFactura.DataSource = dtObjCat;
            gvPreFactura.DataBind();
            ViewState["gvPreFactura"] = dtObjCat;
        } 
        protected void LlenaPreFac()
        {
            DataTable DT = (DataTable)ViewState["gvPreFactura"];
            if (DT != null)
            {
                gvPreFactura.DataSource = DT;
                gvPreFactura.DataBind();
            }
        }
        #endregion 

        #region V A R I A B L E S
        private const string sClase = "frmDocPendientes.aspx.cs";
        private const string sPagina = "frmDocPendientes.aspx";
       
        DocPendientes_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eObjCliente;
        public event EventHandler eObjConrato;
        public event EventHandler eObjBitPend;
        public event EventHandler eObjRemPend;
        public event EventHandler eObjFacPend;
        public DocPendientes oDocContrato
        {
            get
            {
                DocPendientes B = new DocPendientes();
                B.IdCliente = ddlClientes.SelectedItem.Value.S();
                return B;
            }
        }

        public DocPendientes oBitPen 
        {
            get
            {
                DocPendientes B = new DocPendientes();
                
                DateTime dHoy = DateTime.Today;
                B.IdCliente = ddlClientes.SelectedItem == null || ddlClientes.SelectedItem.Text == "Clientes" ? "0" : ddlClientes.SelectedItem.Text;
                B.IdContrato = ddlContrato.SelectedItem == null || ddlContrato.SelectedItem.Text == "Contratos" ? "0" : ddlContrato.SelectedItem.Text;

                if (dFechaIni.Text != string.Empty && dFechaFin.Text == string.Empty)
                {
                    B.FechaIni = dFechaIni.Date.ToString("MM/dd/yyyy");
                    B.FechaFin = dHoy.ToString("MM/dd/yyyy");
                }
                else if (dFechaIni.Text == string.Empty && dFechaFin.Text != string.Empty)
                {
                    B.FechaIni = dHoy.ToString("MM/dd/yyyy");
                    B.FechaFin = dFechaFin.Date.ToString("MM/dd/yyyy");
                }
                else
                {
                    B.FechaIni = dFechaIni.Text == "" ? string.Empty : dFechaIni.Date.ToString("MM/dd/yyyy");
                    B.FechaFin = dFechaFin.Text == "" ? string.Empty : dFechaFin.Date.ToString("MM/dd/yyyy");
                }

                ViewState["FechaIni"] = dFechaIni.Date.ToString("dd/MM/yyyy");
                ViewState["FechaFin"] = dFechaFin.Date.ToString("dd/MM/yyyy");
                return B;
            }
        }

        #endregion
    }
}