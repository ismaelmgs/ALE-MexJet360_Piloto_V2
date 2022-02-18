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


namespace ALE_MexJet.Views.Consultas
{
    public partial class frmConsultaCasos : System.Web.UI.Page, IViewConsultaCasos
    {
        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            try 
            {
            //    DrPermisos = Utils.ObtenerPermisos(Enumeraciones.Pantallas.ConsultaCasos);
            //    LoadActions(DrPermisos);
                gvConsultaCasos.SettingsText.SearchPanelEditorNullText = "Ingrese la información a buscar";
                gvConsultaCasos.SettingsText.GroupPanel = "Arrastre un encabezado de columna aquí para agrupar por esa columna";
                oPresenter = new ConsultaCasosPresenter(this, new DBConsultaCasos());
               
                if (!IsPostBack)
                {
                    if (eObjConsultaTop != null)
                        eObjConsultaTop(sender, e);

                    if (eGetObjects != null)
                        eGetObjects(sender, e);
                   
                }

                LLenaGrid((DataTable)ViewState["oConsultaCasos"]);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "Page_Load", "Aviso");
            }
        }

        protected void btnBusqueda_Click(object sender, System.EventArgs e)
        {
           try
            {
                hfFechaInicial["hfFechaInicial"] = dFechaIni.Text == null ? string.Empty : dFechaIni.Text.S();
                hfFechaFinal["hfFechaFinal"] = dFechaFin.Text == null ? string.Empty : dFechaFin.Text.S();
                hfCliente["hfCliente"] = ddlClientes.SelectedItem == null ? "Todos" : ddlClientes.SelectedItem.Text;
                hfTipoCaso["hfTipoCaso"] = ddlTipoCaso.SelectedItem == null ? "Todos" : ddlTipoCaso.SelectedItem.Text;
                hfSolicitud["hfSolicitud"] = txtSolicitud.Text;
                hfArea["hfArea"] = ddlArea.SelectedItem == null ? "Todos" : ddlArea.SelectedItem.Text;
                hfMotivo["hfMotivo"] = ddlMotivo.SelectedItem == null ? "Todos" : ddlMotivo.SelectedItem.Text;

                if (eObjConsultaCasos != null)
                    eObjConsultaCasos(null, null); 
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnBusqueda_Click", "Aviso");
            }
        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                Exporter.WriteXlsToResponse(new XlsExportOptionsEx() { ExportType = ExportType.DataAware });
                //ExportExcel();
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnExportar_Click", "Aviso");
            }
        }

        protected void UpdatePanel1_Unload(object sender, EventArgs e)
        {
            try
            {
                MethodInfo methodInfo = typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Where(i => i.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel")).First();
                methodInfo.Invoke(ScriptManager.GetCurrent(Page),
                new object[] { sender as UpdatePanel });
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "UpdatePanel1_Unload", "Aviso");
            }
        }

        protected void ddlArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //if (eGetArea != null)
                //    eGetArea(sender, e); 
            }
            catch (Exception Ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, Ex.Message, sPagina, sClase, "ddlArea_SelectedIndexChanged", "Aviso");
            }
        }

        protected void ddlTipoCaso_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (eGetMotivos != null)
                    eGetMotivos(sender, e);
            }
            catch (Exception Ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, Ex.Message, sPagina, sClase, "ddlTipoCaso_SelectedIndexChanged", "Aviso");
            }
        }

        #endregion

        #region "METODOS"

        public void ObtieneValores()
        {
            if (eSearchObj != null)
                eSearchObj(null, EventArgs.Empty);
        }

        public void LoadObjects(DataTable dtObject)
        {
            gvConsultaCasos.DataSource = null;
            gvConsultaCasos.DataSource = dtObject;
            gvConsultaCasos.DataBind();
            ViewState["oConsultaCasos"] = dtObject;
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

        public void LoadTipoCaso(DataTable dtObjCat)
        {
            try
            { 
            ViewState["oTipoCaso"] = dtObjCat;
            ddlTipoCaso.DataSource = dtObjCat;
            ddlTipoCaso.TextField = "Descripcion";
            ddlTipoCaso.ValueField = "IdCaso";
            ddlTipoCaso.DataBind();
            ddlTipoCaso.SelectedItem = null;
            }
            catch (Exception x) { throw x; }
        }

        public void LoadArea(DataTable dtObjCat)
        {
            try
            {
                ViewState["oArea"] = dtObjCat;
                ddlArea.DataSource = dtObjCat;
                ddlArea.TextField = "Descripcion";
                ddlArea.ValueField = "IdArea";
                ddlArea.DataBind();
                ddlArea.SelectedItem = null;
            }
            catch (Exception x) { throw x; }
        }

        public void LoadMotivo(DataTable dtObjCat)
        {
            try
            {
                ViewState["oMotivo"] = dtObjCat;
                ddlMotivo.DataSource = dtObjCat;
                ddlMotivo.TextField = "Descripcion";
                ddlMotivo.ValueField = "IdMotivo";
                ddlMotivo.DataBind();
                ddlMotivo.SelectedItem = null;
            }
            catch (Exception x) { throw x; }
        }

        public void LoadConsultaCasos(DataTable dtObjCat)
        {
             ViewState["oConsultaCasos"] = dtObjCat;
             gvConsultaCasos.DataSource = dtObjCat;
             gvConsultaCasos.DataBind();
        }

        public void LoadConsultaTop (DataTable dtObjCat)
        {
            ViewState["oConsultaCasos"] = dtObjCat;
            gvConsultaCasos.DataSource = dtObjCat;
            gvConsultaCasos.DataBind();
        }
        protected void LLenaGrid(DataTable dtObjGrid)
        {

            gvConsultaCasos.DataSource = dtObjGrid;
            gvConsultaCasos.DataBind();
            
        }

        private void ExportExcel()
        {
            try
            {

                gvConsultaCasos.DataBind();
                DataTable table = (DataTable)gvConsultaCasos.DataSource;
                if (table == null || table.Rows.Count == 0)
                    return;

                int columnscount = gvConsultaCasos.Columns.Count;
                int x = columnscount - 2;
                string _ippuertoserver = Request.Url.Authority;

                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.ClearHeaders();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "application/ms-excel";
                HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=Transferencia de Consulta de Casos.xls");
                HttpContext.Current.Response.Charset = "utf-8";
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");

                HttpContext.Current.Response.Write(@"<table><tr><td style=' text-align: center; vertical-align: middle'><img src='http://" + _ippuertoserver.S() + "/img/mexjet_p.png' height='100px' width='90px'/></td>" +
                 "<td style=' text-align: center; font-size: x-large; vertical-align: middle' colspan='" + x.S() + "'>&nbsp;&nbsp;&nbsp; Transferencia de Bítacora</td><td>Clientes: " + hfCliente["hfCliente"].S() + "</td></tr>" +
                 "<tr><td></td><td style='text-align: center; font-size: large; vertical-align: middle' colspan='" + x.S() + "'>&nbsp;&nbsp;" + hfFechaInicial["hfFechaInicial"].S() + " - " + hfFechaFinal["hfFechaFinal"].S() + "</td><td>TipoCaso: " + hfTipoCaso["hfTipoCaso"].S() + " </td></tr></table>");

                HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; background:white;'>");
                HttpContext.Current.Response.Write("<hr style='background-color: #000000' />");

                HttpContext.Current.Response.Write("<TR>");
                for (int j = 0; j < columnscount; j++)
                {
                    HttpContext.Current.Response.Write("<Td style='border-width: 1px 0px 1px 0px'>");
                    HttpContext.Current.Response.Write("<B>");
                    HttpContext.Current.Response.Write(gvConsultaCasos.Columns[j].Caption.S());
                    HttpContext.Current.Response.Write("</B>");
                    HttpContext.Current.Response.Write("</Td>");
                }
                HttpContext.Current.Response.Write("</TR>");

                foreach (DataRow row in table.Rows)
                {
                    HttpContext.Current.Response.Write("<TR>");
                    for (int i = 0; i < gvConsultaCasos.Columns.Count; i++)
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
        public void LoadActions(DataRow[] DrActions)
        {
            int iPos = 0;
            if (DrActions.Length == 0)
            {
                btnBusqueda.Enabled = false;
                txtSolicitud.Enabled = false;
                ddlArea.Enabled = false;
                ddlClientes.Enabled = false;
                ddlTipoCaso.Enabled = false;
                ddlMotivo.Enabled = false;
                dFechaIni.Enabled = false;
                dFechaFin.Enabled = false;
                btnExcel.Enabled = false;
            }
            else
            {
                for (iPos = 0; iPos < DrActions[0].ItemArray.Length; iPos++)
                {
                    switch (iPos)
                    {
                        case 4: if (DrActions[0].ItemArray[iPos].S().I() == Convert.ToInt32(Enumeraciones.Bloqueo.Permitido))
                            {
                                btnBusqueda.Enabled = true;
                                txtSolicitud.Enabled = true;
                                ddlArea.Enabled = true;
                                ddlClientes.Enabled = true;
                                ddlTipoCaso.Enabled = true;
                                ddlMotivo.Enabled = true;
                                dFechaIni.Enabled = true;
                                dFechaFin.Enabled = true;
                                btnExcel.Enabled = true;

                            }
                            else
                            {
                                btnBusqueda.Enabled = false;
                                txtSolicitud.Enabled = false;
                                ddlArea.Enabled = false;
                                ddlClientes.Enabled = false;
                                ddlTipoCaso.Enabled = false;
                                ddlMotivo.Enabled = false;
                                dFechaIni.Enabled = false;
                                dFechaFin.Enabled = false;
                                btnExcel.Enabled = false;

                            } break;
                    }
                }
            }

        }
        #endregion

        #region "Vars y Propiedades"
        private const string sClase = "frmConsultaCasos.aspx.cs";
        private const string sPagina = "frmConsultaCasos.aspx";

        ConsultaCasosPresenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eGetObjects;
        public event EventHandler eObjConsultaCasos;
        public event EventHandler eGetMotivos;
        public event EventHandler eGetArea;
        public event EventHandler eObjConsultaTop;
        UserIdentity oUsuario = new UserIdentity();
        public DataRow[] DrPermisos
        {
            get { return (DataRow[])Session["DrPermisos"]; }
            set { Session["DrPermisos"] = value; }
        }

        public int iIdTipoCaso
        {
            get
            {
                return ddlTipoCaso.SelectedItem.Value.S().I();
            }
        }
        public ConsultaCasos oConsultaCasosTipoCaso
        {
            get
            {
                ConsultaCasos B = new ConsultaCasos();
                B.Idsolicitud = txtSolicitud.Text.S().I();
                B.IdTipoCaso = ddlTipoCaso.SelectedItem.Value.S().I();
                B.IdMotivo = ddlMotivo.SelectedItem.Value.S().I();

                return B;
            }
        }

        public ConsultaCasos oConsultaCasosConsultaCasos
        {
            get 
            {
                ConsultaCasos B = new ConsultaCasos();
                DateTime dHoy = DateTime.Today;
                B.IdCliente = ddlClientes.SelectedItem != null ? ddlClientes.SelectedItem.Value.S().I() : 0;
                B.Idsolicitud = txtSolicitud.Text.S().I();
                B.IdArea = ddlArea.SelectedItem != null ? ddlArea.SelectedItem.Value.S().I() : 0;
                B.IdTipoCaso = ddlTipoCaso.SelectedItem != null ? ddlTipoCaso.SelectedItem.Value.S().I() : 0;
                B.IdMotivo = ddlMotivo.SelectedItem != null ? ddlMotivo.SelectedItem.Value.S().I() : 0;

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

                return B;
           }
        }

        public ConsultaCasos oConsultaCasosConsultaTop
        {
            get
            {
                ConsultaCasos B = new ConsultaCasos();
                DateTime dHoy = DateTime.Today;
                B.IdCliente = ddlClientes.SelectedItem != null ? ddlClientes.SelectedItem.Value.S().I() : 0;
                B.Idsolicitud = txtSolicitud.Text.S().I();
                B.IdArea = ddlArea.SelectedItem != null ? ddlArea.SelectedItem.Value.S().I() : 0;
                B.IdTipoCaso = ddlTipoCaso.SelectedItem != null ? ddlTipoCaso.SelectedItem.Value.S().I() : 0;
                B.IdMotivo = ddlMotivo.SelectedItem != null ? ddlMotivo.SelectedItem.Value.S().I() : 0;

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
                    B.FechaIni = dFechaIni.Text == null ? string.Empty : dFechaIni.Date.ToString("MM/dd/yyyy"); ;
                    B.FechaFin = dFechaFin.Text == null ? string.Empty : dFechaFin.Date.ToString("MM/dd/yyyy"); ;
                }

                return B;
            }
        }

        #endregion

        protected void ddlClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlTipoCaso.SelectedIndex = -1;
            ddlMotivo.SelectedIndex = -1;
        }

        

        

    }
}