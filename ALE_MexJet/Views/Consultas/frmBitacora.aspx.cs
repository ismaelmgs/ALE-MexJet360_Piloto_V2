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
    public partial class frmBitacora : System.Web.UI.Page, IViewBitacora
    {
        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            try 
            {
                DrPermisos = Utils.ObtenerPermisos(Enumeraciones.Pantallas.BitacorasTransferidas);
                LoadActions(DrPermisos);
                gvNotaCredito.SettingsText.SearchPanelEditorNullText = "Ingrese la información a buscar";
                gvNotaCredito.SettingsText.GroupPanel= "Arrastre un encabezado de columna aquí para agrupar por esa columna";

                gvBitDuplicada.SettingsText.SearchPanelEditorNullText = "Ingrese la información a buscar";
                gvBitDuplicada.SettingsText.GroupPanel = "Arrastre un encabezado de columna aquí para agrupar por esa columna";

                gvBitCobrada.SettingsText.SearchPanelEditorNullText = "Ingrese la información a buscar";
                gvBitCobrada.SettingsText.GroupPanel = "Arrastre un encabezado de columna aquí para agrupar por esa columna";

                gvBitPorCobrar.SettingsText.SearchPanelEditorNullText = "Ingrese la información a buscar";
                gvBitPorCobrar.SettingsText.GroupPanel = "Arrastre un encabezado de columna aquí para agrupar por esa columna";

                oPresenter = new BitacoraPresenter(this, new DBBitacora());
                LLenaGrid();
                if (!IsPostBack)
                {                    
                    if (eObjCliente != null)
                        eObjCliente(null, null);                                        
                }
                
                if (gvBitDuplicada.Visible == true)
                    CargaBitacorasDuplicadas();

                if (gvBitCobrada.Visible == true)
                    CargaBitacorasCobradas();

                if (gvBitPorCobrar.Visible == true)
                    CargaBitacorasPorCobrar();

                CargaNumeroBitacorasCobradas();
                CargaNumeroBitacorasPorCobrar();
                CargaNumeroTotalRegistros();
                CargaNumeroTotalDuplicadas();


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
                gvBitDuplicada.Visible = false;
                gvBitCobrada.Visible = false;
                gvBitPorCobrar.Visible = false;
                gvNotaCredito.Visible = true;
                btnExcel.Visible = true;
                hfFechaInicial["hfFechaInicial"] = dFechaIni.Text == null ? string.Empty : dFechaIni.Text.S();
                hfFechaFinal["hfFechaFinal"] = dFechaFin.Text == null ? string.Empty : dFechaFin.Text.S();
                hfCliente["hfCliente"] = ddlClientes.SelectedItem == null ? "Todos" : ddlClientes.SelectedItem.Text;
                hfContrato["hfContrato"] = ddlContrato.SelectedItem == null ? "Todos" : ddlContrato.SelectedItem.Text;
                hfFolio["hfFolio"] = txtTextoBusqueda.Text == string.Empty ? "Todos" : txtTextoBusqueda.Text;

                if (eObjBitacora != null)
                        eObjBitacora(null, null); 
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnBusqueda_Click", "Aviso");
            }           
        }


        protected void btnPosiblesDuplicados_Click(object sender, EventArgs e)
        {
            gvNotaCredito.Visible = false;
            gvBitCobrada.Visible = false;
            gvBitPorCobrar.Visible = false;
            gvBitDuplicada.Visible = true;
            btnExcel.Visible = false;
            CargaBitacorasDuplicadas();
        }

        protected void btnBitacorasCobradas_Click(object sender, EventArgs e)
        {
            gvNotaCredito.Visible = false;
            gvBitDuplicada.Visible = false;
            gvBitPorCobrar.Visible = false;
            gvBitCobrada.Visible = true;
            btnExcel.Visible = false;
            CargaBitacorasCobradas();

        }

        protected void btnBitacoraPorCobrar_Click(object sender, EventArgs e)
        {
            gvNotaCredito.Visible = false;
            gvBitDuplicada.Visible = false;
            gvBitCobrada.Visible = false;
            gvBitPorCobrar.Visible = true;           
            btnExcel.Visible = false;
            CargaBitacorasPorCobrar();
        }

        protected void gvBitDuplicada_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {
            if (e.ButtonType != ColumnCommandButtonType.Delete) return;
            string sRemisionado = gvBitDuplicada.GetRowValues(e.VisibleIndex, "Remisionado").S();
            e.Visible = sRemisionado == "0";
        }

        protected void gvBitDuplicada_RowDeleting(object sender, ASPxDataDeletingEventArgs e)
        {
            try
            {
                eCrud = Enumeraciones.TipoOperacion.Eliminar;
                oCrud = e;
                CancelEditing(e);

                if (eDeleteObj != null)
                    eDeleteObj(sender, e);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvAeronave_RowDeleting", "Aviso");
            }
        }

        protected void gvBitCobrada_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {
            if (e.ButtonType != ColumnCommandButtonType.Delete) return;
            string sRemisionado = gvBitCobrada.GetRowValues(e.VisibleIndex, "Remisionado").S();
            e.Visible = sRemisionado == "0";
        }

        protected void gvBitCobrada_RowDeleting(object sender, ASPxDataDeletingEventArgs e)
        {
            try
            {
                eCrud = Enumeraciones.TipoOperacion.Eliminar;
                oCrud = e;
                CancelEditing(e);

                if (eDeleteObj != null)
                    eDeleteObj(sender, e);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvAeronave_RowDeleting", "Aviso");
            }
        }

        protected void gvBitPorCobrar_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {
            if (e.ButtonType != ColumnCommandButtonType.Delete) return;
            string sRemisionado = gvBitPorCobrar.GetRowValues(e.VisibleIndex, "Remisionado").S();
            e.Visible = sRemisionado == "0";
        }

        protected void gvBitPorCobrar_RowDeleting(object sender, ASPxDataDeletingEventArgs e)
        {
            try
            {
                eCrud = Enumeraciones.TipoOperacion.Eliminar;
                oCrud = e;
                CancelEditing(e);

                if (eDeleteObj != null)
                    eDeleteObj(sender, e);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvAeronave_RowDeleting", "Aviso");
            }
        }

        protected void ddlClientes_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            try
            {
                if (eObjConrato != null)
                    eObjConrato(null, null);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "ddlClientes_SelectedIndexChanged", "Aviso");
            }
        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                //Exporter.WriteXlsToResponse(new XlsExportOptionsEx() { ExportType = ExportType.DataAware });
                ExportExcel();
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

        protected void UpdatePanel2_Unload(object sender, EventArgs e)
        {
            try
            {
                MethodInfo methodInfo = typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Where(i => i.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel")).First();
                methodInfo.Invoke(ScriptManager.GetCurrent(Page),
                new object[] { sender as UpdatePanel });
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "UpdatePanel2_Unload", "Aviso");
            }
        }
        #endregion

        #region "METODOS"

        protected void CancelEditing(CancelEventArgs e)
        {
            try
            {
                e.Cancel = true;
                gvBitDuplicada.CancelEdit();
                gvBitCobrada.CancelEdit();
                gvBitPorCobrar.CancelEdit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
            ViewState["oContrato"] = dtObjCat;
            ddlContrato.DataSource = dtObjCat;
            ddlContrato.TextField = "ClaveContrato";
            ddlContrato.ValueField = "IdContrato";
            ddlContrato.DataBind();
            ddlContrato.SelectedItem = null;

        }

        public void LoadBitacora(DataTable dtObjCat)
        {
             ViewState["oBitacora"] = dtObjCat;
             gvNotaCredito.DataSource = dtObjCat;
             gvNotaCredito.DataBind();
        }

        public void LoadBitacoraDuplicada(DataTable dtObjCat)
        {
            gvBitDuplicada.DataSource = dtObjCat;
            gvBitDuplicada.DataBind();
            
        }

        public void CargaBitacorasDuplicadas()
        {
            if (eObjBitacoraDuplicada != null)
                eObjBitacoraDuplicada (null, null);
        }

        public void CargaNumeroBitacorasCobradas()
        {
            if (eObjNumeroBitacorasCobradas != null)
                eObjNumeroBitacorasCobradas(null, null);         
        }

        public void LoadNumeroBitacorasCobradas(DataTable dtObjCat)
        {
            foreach (DataRow item in dtObjCat.Rows)
            {
                NumeroBitacorasCobradas = item["BitacorasCobradas"].I();
            }          
            LblTotalCobradas.Text = NumeroBitacorasCobradas.S();
        }

        public void CargaNumeroBitacorasPorCobrar()
        {
            if (eObjNumeroBitacorasPorCobrar != null)
                eObjNumeroBitacorasPorCobrar(null, null);
        }

        public void LoadNumeroBitacorasPorCobrar(DataTable dtObjCat)
        {
            foreach (DataRow item in dtObjCat.Rows)
            {
                NumeroBitacorasPorCobrar = item["BitacorasPorCobrar"].I();
            }
            LblTotalPorCobar.Text = NumeroBitacorasPorCobrar.S();
        }

        public void CargaNumeroTotalRegistros()
        {
            if (eObjNumeroTotalRegistros != null)
                eObjNumeroTotalRegistros(null, null);
        }

        public void LoadNumeroTotalRegistros(DataTable dtObjCat)
        {
            foreach (DataRow item in dtObjCat.Rows)
            {
                NumeroTotalRegistros = item["TotalRegistros"].I();
            }
            LblTotalRegistro.Text = NumeroTotalRegistros.S();
        }

        public void CargaNumeroTotalDuplicadas()
        {
            if (eObjNumeroTotalDuplicadas != null)
                eObjNumeroTotalDuplicadas(null, null);
        }

        public void LoadNumeroTotalDuplicadas(DataTable dtObjCat)
        {
            foreach (DataRow item in dtObjCat.Rows)
            {
                NumeroTotalDuplicadas = item["TotalDuplicadas"].I();
            }
            LblTotalDuplicadas.Text = NumeroTotalDuplicadas.S();
        }

        public void LoadBitacoraCobrada(DataTable dtObjCat)
        {
            gvBitCobrada.DataSource = dtObjCat;
            gvBitCobrada.DataBind();
        }

        public void CargaBitacorasCobradas()
        {
            if (eObjBitacoraCobrada != null)
                eObjBitacoraCobrada(null, null);
        }

        public void LoadBitacoraPorCobrar(DataTable dtObjCat)
        {
            gvBitPorCobrar.DataSource = dtObjCat;
            gvBitPorCobrar.DataBind();
        }

        public void CargaBitacorasPorCobrar()
        {
            if (eObjBitacoraPorCobrar != null)
                eObjBitacoraPorCobrar(null, null);
        }

        protected void LLenaGrid()
        {
            if (ViewState["oBitacora"] != null)
            {
                gvNotaCredito.DataSource = (DataTable)ViewState["oBitacora"];
                gvNotaCredito.DataBind();
            }
        }

        private void ExportExcel()
        {
            try
            {

                gvNotaCredito.DataBind();
                DataTable table = (DataTable)gvNotaCredito.DataSource;
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
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=Transferencia de Bitacoras.xls");
                HttpContext.Current.Response.Charset = "utf-8";
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");

                HttpContext.Current.Response.Write(@"<table><tr><td style=' text-align: center; vertical-align: middle'><img src='http://" + _ippuertoserver.S() + "/img/mexjet_p.png' height='100px' width='90px'/></td>" +
                 "<td style=' text-align: center; font-size: x-large; vertical-align: middle' colspan='" + x.S() + "'>&nbsp;&nbsp;&nbsp; Transferencia de Bítacora</td><td>Clientes: " + hfCliente["hfCliente"].S() + "</td></tr>" +
                 "<tr><td></td><td style='text-align: center; font-size: large; vertical-align: middle' colspan='" + x.S() + "'>&nbsp;&nbsp;" + hfFechaInicial["hfFechaInicial"].S() + " - " + hfFechaFinal["hfFechaFinal"].S()  + "</td><td>Contrato: " + hfContrato["hfContrato"].S() + " </td></tr></table>");

                HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; background:white;'>");
                HttpContext.Current.Response.Write("<hr style='background-color: #000000' />");

                HttpContext.Current.Response.Write("<TR>");
                for (int j = 0; j < columnscount; j++)
                {
                    HttpContext.Current.Response.Write("<Td style='border-width: 1px 0px 1px 0px'>");
                    HttpContext.Current.Response.Write("<B>");
                    HttpContext.Current.Response.Write(gvNotaCredito.Columns[j].Caption.S());
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
        public void LoadActions(DataRow[] DrActions)
        {
            int iPos = 0;
            if (DrActions.Length == 0)
            {
                btnBusqueda.Enabled = false;
                txtTextoBusqueda.Enabled = false;
                ddlClientes.Enabled = false;
                ddlContrato.Enabled = false;
                btnExcel.Enabled = false;
                dFechaFin.Enabled = false;
                dFechaIni.Enabled = false;
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
                                txtTextoBusqueda.Enabled = true;
                                ddlClientes.Enabled = true;
                                ddlContrato.Enabled = true;
                                btnExcel.Enabled = true;
                                dFechaFin.Enabled = true;
                                dFechaIni.Enabled = true;
                            }
                            else
                            {
                                btnBusqueda.Enabled = false;
                                txtTextoBusqueda.Enabled = false;
                                ddlClientes.Enabled = false;
                                ddlContrato.Enabled = false;
                                btnExcel.Enabled = false;
                                dFechaFin.Enabled = false;
                                dFechaIni.Enabled = false;
                            } break;
                    }
                }
            }

        }

        #endregion

        #region "Vars y Propiedades"
        private const string sClase = "frmBitacora.aspx.cs";
        private const string sPagina = "frmBitacora.aspx";


        BitacoraPresenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eObjCliente;
        public event EventHandler eObjConrato;
        public event EventHandler eObjBitacora;
        public event EventHandler eObjBitacoraDuplicada;
        public event EventHandler eObjBitacoraCobrada;
        public event EventHandler eObjBitacoraPorCobrar;
        public event EventHandler eObjNumeroBitacorasCobradas;
        public event EventHandler eObjNumeroBitacorasPorCobrar;
        public event EventHandler eObjNumeroTotalRegistros;
        public event EventHandler eObjNumeroTotalDuplicadas;
        UserIdentity oUsuario = new UserIdentity();

        public int NumeroBitacorasCobradas
        {
            get { return (int)ViewState["VSNumeroBitacorasCobradas"]; }
            set { ViewState["VSNumeroBitacorasCobradas"] = value; }
        }

        public int NumeroBitacorasPorCobrar
        {
            get { return (int)ViewState["VSNumeroBitacorasPorCobrar"]; }
            set { ViewState["VSNumeroBitacorasPorCobrar"] = value; }
        }

        public int NumeroTotalRegistros
        {
            get { return (int)ViewState["VSNumeroTotalRegistros"]; }
            set { ViewState["VSNumeroTotalRegistros"] = value; }
        }

        public int NumeroTotalDuplicadas
        {
            get { return (int)ViewState["VSNumeroTotalDuplicadas"]; }
            set { ViewState["VSNumeroTotalDuplicadas"] = value; }
        }

        public DataRow[] DrPermisos
        {
            get { return (DataRow[])Session["DrPermisos"]; }
            set { Session["DrPermisos"] = value; }
        }

        public Bitacora oBitacoraContrato 
        {
            get
            {
                Bitacora B = new Bitacora();
                B.IdCliente = ddlClientes.SelectedItem.Value.S(); 
                return B;
            }
        }

        public Bitacora oBitacoraBitacora
        {
            get 
            {
                Bitacora B = new Bitacora();
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

                B.IdFolio = txtTextoBusqueda.Text;
                return B;
            }
        }

        public Bitacora oBitacoraBitacoraCobrada
        {
            get
            {
                Bitacora B = new Bitacora();
                DateTime dHoy = DateTime.Today;               

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

        public Bitacora oBitacoraBitacoraNumeroCobrada
        {
            get
            {
                Bitacora B = new Bitacora();
                DateTime dHoy = DateTime.Today;

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

        public Bitacora oBitacoraBitacoraPorCobrar
        {
            get
            {
                Bitacora B = new Bitacora();
                DateTime dHoy = DateTime.Today;

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

        public Bitacora oBitacoraBitacoraNumeroPorCobrar
        {
            get
            {
                Bitacora B = new Bitacora();
                DateTime dHoy = DateTime.Today;

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

        public Bitacora oNumeroTotalRegistros
        {
            get
            {
                Bitacora B = new Bitacora();
                DateTime dHoy = DateTime.Today;

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

        public Enumeraciones.TipoOperacion eCrud
        {
            get
            {
                return (ALE_MexJet.Objetos.Enumeraciones.TipoOperacion)ViewState["eCrud"];
            }
            set
            {
                ViewState["eCrud"] = value;
            }
        }
        public object oCrud
        {
            get { return ViewState["CrudBitacoraDuplicada"]; }
            set { ViewState["CrudBitacoraDuplicada"] = value; }
        }
        #endregion
    }
}