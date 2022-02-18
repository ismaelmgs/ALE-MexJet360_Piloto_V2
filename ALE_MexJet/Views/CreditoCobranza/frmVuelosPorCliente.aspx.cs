using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ALE_MexJet.Interfaces;
using ALE_MexJet.Presenter;
using ALE_MexJet.DomainModel;
using DevExpress.Web;
using DevExpress.XtraPrinting;
using DevExpress.Export;
using NucleoBase.Core;
using ALE_MexJet.Clases;

namespace ALE_MexJet.Views.CreditoCobranza
{
    public partial class frmVuelosPorCliente : System.Web.UI.Page, IViewVuelosPorCliente
    {

        #region Eventos
        
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                oPresenter = new VuelosPorCliente_Presenter(this, new DBVuelosPorCliente());
                mpeComboClienteContrato.miEventoCliente += mpeComboClienteContrato_miEventoCliente;
                mpeComboClienteContrato.miEventoContrato += mpeComboClienteContrato_miEventoContrato;
                gvVueloCliente.SettingsPager.Position = PagerPosition.TopAndBottom;
                gvVueloCliente.SettingsPager.ShowDisabledButtons = true;
                gvVueloCliente.SettingsPager.ShowNumericButtons = true;
                gvVueloCliente.SettingsPager.ShowSeparators = true;
                gvVueloCliente.SettingsPager.Summary.Visible = true;
                gvVueloCliente.SettingsPager.PageSizeItemSettings.Visible = true;
                gvVueloCliente.SettingsPager.PageSizeItemSettings.Position = PagerPageSizePosition.Right;
                gvVueloCliente.SettingsText.SearchPanelEditorNullText = "Ingrese la información a buscar";

                ObtieneDatosCliente();
                RecargaGrid();
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "Page_Load", "Aviso");
            }
        }

        void mpeComboClienteContrato_miEventoContrato(object sender, EventArgs e)
        {
            try
            {
                ASPxComboBox cmb = (ASPxComboBox)sender;
                if (cmb.SelectedIndex != -1)
                    sContratoCombo = cmb.SelectedItem.Text;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       
        private void mpeComboClienteContrato_miEventoCliente(object sender, EventArgs e)
        {
            try
            {
                ASPxComboBox cmb = (ASPxComboBox)sender;
                if (cmb.SelectedIndex != -1)
                {
                    sClienteCombo = cmb.SelectedItem.Text == "Todos" ? "" : cmb.SelectedItem.Text;
                    ObtieneDatosContrato();
                }
            }
            catch (Exception ex)
            {
                throw ex;
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

        protected void btnBusqueda_Click(object sender, EventArgs e)
        {
            try
            {
                OtieneReporteVuelosPorCliente();
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnBusqueda_Click", "Aviso");
            }
        }


        #endregion Eventos


        #region Metodos

        public void LoadClientes(DataTable dtObjCat)
        {
            mpeComboClienteContrato.llenarComboCliente(dtObjCat);
        }
        public void LoadContrato(DataTable dtObjCat)
        {
            mpeComboClienteContrato.llenarComboContrato(dtObjCat);
        }

        public void LoadReporteVuelosPorCliente(DataTable dtObjCat)
        {
            gvVueloCliente.DataSource = null;
            gvVueloCliente.DataSource = dtObjCat;
            gvVueloCliente.DataBind();            
        }
        protected void RecargaGrid()
        {
            if (ViewState["gvVueloCliente"] != null)
            {
                gvVueloCliente.DataSource = (DataTable)ViewState["gvVueloCliente"];
                gvVueloCliente.DataBind();
            }
        }
        public void ObtieneDatosCliente()
        {
            if (eGetClientes != null)
                eGetClientes(null, EventArgs.Empty);
        }
        public void ObtieneDatosContrato()
        {
            if (eGetContrato != null)
                eGetContrato(null, EventArgs.Empty);
        }
        public void OtieneReporteVuelosPorCliente()
        {
            if (eGetReporteVuelosPorCliente != null)
                eGetReporteVuelosPorCliente(null, EventArgs.Empty);
        }

        private void ExportExcel()
        {
            try
            {

                gvVueloCliente.DataBind();
                DataTable table = (DataTable)gvVueloCliente.DataSource;
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
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=Reporte de vuelos por cliente.xls");
                HttpContext.Current.Response.Charset = "utf-8";
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");

                HttpContext.Current.Response.Write(@"<table><tr><td style=' text-align: center; vertical-align: middle'><img src='http://" + _ippuertoserver.S() + "/img/mexjet_p.png' height='100px' width='90px'/></td>" +
                 "<td style=' text-align: center; font-size: x-large; vertical-align: middle' colspan='" + x.S() + "'>&nbsp;&nbsp;&nbsp; Aerolineas Ejecutivas S.A. de C.V. </td><td>Clientes: " + sClienteCombo + "</td></tr>" +
                 "<tr><td></td><td style='text-align: center; font-size: large; vertical-align: middle' colspan='" + x.S() + "'> Reporte de vuelos realizados del &nbsp;&nbsp;" + dFechaIni.Text + " &nbsp;&nbsp; al &nbsp;&nbsp; " + dFechaFin.Text + "</td><td>Contrato: " + sContratoCombo + " </td></tr></table>");

                HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; background:white;'>");
                HttpContext.Current.Response.Write("<hr style='background-color: #000000' />");

                HttpContext.Current.Response.Write("<TR>");
                for (int j = 0; j < columnscount; j++)
                {
                    HttpContext.Current.Response.Write("<Td style='border-width: 1px 0px 1px 0px'>");
                    HttpContext.Current.Response.Write("<B>");
                    HttpContext.Current.Response.Write(gvVueloCliente.Columns[j].Caption.S());
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
        
        #endregion Metodos


        #region Propiedades y Vars

        public object[] oArrFiltroClientes
        {
            get { return new object[] { "@idCliente", 0 }; }
        }
        public object[] oArrFiltroContrato
        {
            get { return new object[] { "@NombreCliente", null, "@CodigoCliente", sClienteCombo }; }
        }
        public object[] oArrFiltroReporteVuelosPorCliente
        {
            get
            {
                DateTime dtfechaInicio;
                DateTime dtFechaFin;

                if (string.IsNullOrEmpty(dFechaIni.Text))                
                    dtfechaInicio = Convert.ToDateTime("01/01/1900");                
                else
                    dtfechaInicio = Convert.ToDateTime(dFechaIni.Text);

                if (string.IsNullOrEmpty(dFechaFin.Text))                
                    dtFechaFin = Convert.ToDateTime("01/01/1900");                
                else
                    dtFechaFin = Convert.ToDateTime(dFechaFin.Text);

                return new object[] {   "@Cliente", sClienteCombo, 
                                        "@Contrato", sContratoCombo,
                                        "@FechaInicial", dtfechaInicio,
                                        "@FechaFinal", dtFechaFin
                                      };
            }
        }

        VuelosPorCliente_Presenter oPresenter;

        private const string sClase = "frmVueloPorCliente.aspx.cs";
        private const string sPagina = "frmVueloPorCliente.aspx";

        protected static string sClienteCombo;
        protected static string sContratoCombo;
   
        public event EventHandler eGetReporteVuelosPorCliente;
        public event EventHandler eGetClientes;
        public event EventHandler eGetContrato;
        public event EventHandler eObjSelected;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eSaveObj;
        public event EventHandler eNewObj;                
                
        #endregion Propiedades y Vars                                                
    }
}