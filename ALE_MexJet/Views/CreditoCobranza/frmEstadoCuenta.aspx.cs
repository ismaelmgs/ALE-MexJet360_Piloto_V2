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

namespace ALE_MexJet.Views.CreditoCobranza
{
    public partial class frmEstadoCuenta : System.Web.UI.Page, IViewEstadoCuenta
    {
        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                oPresenter = new EstadoCuenta_Presenter(this, new DBEstadoCuenta());
                if (!IsPostBack)
                {
                    if (eObjCliente != null)
                        eObjCliente(null, null);
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
        protected void ddlClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (eObjContrato != null)
                    eObjContrato(null, null);
            }
            catch (Exception ex) { Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "ddlClientes_SelectedIndexChanged", "Aviso"); }
        }
        protected void gvVuelo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string inx = e.Row.Cells[0].Text.S();
                    DataTable DT = (DataTable)ViewState["LoadVueloDetail"];
                    DT = DT.Select("IdRemision = " + inx + "").CopyToDataTable();

                    //DataRow rw = new DataRow();
                    //TableCell cell = new TableCell();
                    //cell.Text = "hola";
                    //rw[0] = cell;
                }
            }
            catch (Exception ex) { Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvVuelo_RowDataBound", "Aviso"); }
        }
        protected void btnBusqueda_Click(object sender, EventArgs e)
        {
            try
            {
                hfFechaInicial["hfFechaInicial"] = dFechaIni.Text == null ? string.Empty : dFechaIni.Text.S();
                hfFechaFinal["hfFechaFinal"] = dFechaFin.Text == null ? string.Empty : dFechaFin.Text.S();
                hfCliente["hfCliente"] = ddlClientes.SelectedItem == null ? string.Empty : ddlClientes.SelectedItem.Text;
                hfContrato["hfContrato"] = ddlContrato.SelectedItem == null ? string.Empty : ddlContrato.SelectedItem.Text;

                if (eObjHeadEdoCnta != null)
                    eObjHeadEdoCnta(null, null);
                CreaHead();

                if (eObjVueloHDetail != null)
                    eObjVueloHDetail(null, null);

                if (eObjVueloHhead != null)
                    eObjVueloHhead(null, null);

                CreaHtml();

                if (eObjVueloEqDif != null)
                    eObjVueloEqDif(null, null);

                CargaEstadoCuentaMXN();
                CreaHtmlFacturacion();
                CargaResumen();


                CreaHtmlVuelEfecEquipDif();

            }
            catch (Exception ex) { Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnBusqueda_Click", "Aviso"); }
        }
        protected void btnExcel_Click(object sender, EventArgs e)
        {

            string Cadena = Prueba.InnerHtml;
            Cadena = Cadena + "<br /><br />";
            Cadena = Cadena + VuelEfecEquipDif.InnerHtml;
            Cadena = Cadena + "<br /><br />";
            Cadena = Cadena + Facturacion.InnerHtml;
            Cadena = Cadena + "<br /><br />";
            Cadena = Cadena + Resumen.InnerHtml;
            Cadena = Cadena + "<br /><br />";
            Cadena = Cadena + ResumenAcumulado.InnerHtml;
            ExportExcel(Cadena);
        }
        protected void btnPDF_Click(object sender, EventArgs e)
        {
            try
            {
                CristalReport();
            }
            catch (Exception ex) { Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnPDF_Click", "Aviso"); }
        }
        #endregion

        #region METODOS
        public void CargaEstadoCuentaMXN()
        {
            try
            {
                string Contrato = ddlContrato.SelectedItem == null ? "0" : ddlContrato.SelectedItem.Text.S();
                DateTime FIni = dFechaIni.Date.ToString("dd-MM-yyyy").ToString().Dt();
                DateTime FFin = dFechaFin.Date.ToString("dd-MM-yyyy").ToString().Dt();

                //string Contrato = "CEUTA";
                //DateTime FIni = "01-01-2015".Dt();
                //DateTime FFin = "30-12-2015".Dt();

                DataSet DS = new DataSet();
                using (wsSyteline.Iws_SyteLineClient ws = new wsSyteline.Iws_SyteLineClient())
                {
                    ws.Open();
                    DS = ws.GetEstadoCuentaFacturacionDLLS(Contrato, FIni, FFin);
                    ws.Close();
                }

                if (DS.Tables[0] != null)
                {
                    ViewState["CargaEstadoCuentaMXN"] = DS.Tables[0].Copy();
                }
            }
            catch (Exception x) { throw x; }
        }
        public void CargaResumen()
        {
            try
            {
                string Contrato = ddlContrato.SelectedItem == null ? "0" : ddlContrato.SelectedItem.Text.S();
                DateTime FIni = dFechaIni.Date.ToString("dd-MM-yyyy").ToString().Dt();
                DateTime FFin = dFechaFin.Date.ToString("dd-MM-yyyy").ToString().Dt();

                //string Contrato = "NEXTE";
                //DateTime FIni = "01-07-2015".Dt();
                //DateTime FFin = "31-07-2015".Dt();

                DataSet DS = new DataSet();
                using (wsSyteline.Iws_SyteLineClient ws = new wsSyteline.Iws_SyteLineClient())
                {
                    ws.Open();
                    DS = ws.GetEstadoCuentaResumenAcumuladoDLLS(Contrato, FIni, FFin);
                    ws.Close();
                }
                ///
                /// Se Obtienen Gastos Internos del Periodo
                ///
                DataTable dtGIP = new DBEstadoCuenta().DBGetObtieneGIdelPeriodo(ddlContrato.SelectedItem.Value.S().I(), FIni, FFin);
                DS = ActualizaGIPeriodo(DS, dtGIP);
                ObtieneSumaResumenPeriodoAct(DS);

                ///
                /// Se obtienen saldos iniciales
                ///
                ReporteEdoCuent oRSI = new DBEstadoCuenta().DBGetObtieneSaldosIniciales(ddlContrato.SelectedItem.Value.S(), FIni, FFin);

                DataSet dsMovAnt = new DataSet();
                using (wsSyteline.Iws_SyteLineClient ws = new wsSyteline.Iws_SyteLineClient())
                {
                    ws.Open();
                    dsMovAnt = ws.GetMovimientosAnterioresEdoCuenta(Contrato, FIni);
                    ws.Close();
                }

                if (dsMovAnt.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = dsMovAnt.Tables[0].Rows[0];

                    oRSI.dAnticipoInicialSPD = dr["AnticipoInicialSD"].S().D();
                    oRSI.dQuintoAnioSPD = dr["QuintoAnioSD"].S().D();
                    oRSI.dFijoAnualSPD = dr["FijoAnualSD"].S().D();
                    oRSI.dIvaSPD = dr["IvaSD"].S().D();
                    oRSI.dGastosSPD = dr["GastosSD"].S().D();
                    oRSI.dServiciosConCargoSPD = dr["ServiciosCargoSD"].S().D();
                    oRSI.dVuelosSPD = dr["VuelosSD"].S().D();

                    oRSI.dAnticipoInicialSPP = dr["AnticipoInicialSP"].S().D();
                    oRSI.dQuintoAnioSPP = dr["QuintoAnioSP"].S().D();
                    oRSI.dFijoAnualSPP = dr["FijoAnualSP"].S().D();
                    oRSI.dIvaSPP = dr["IvaSP"].S().D();
                    oRSI.dGastosSPP = dr["GastosSP"].S().D();
                    oRSI.dServiciosConCargoSPP = dr["ServiciosCargoSP"].S().D();
                    oRSI.dVuelosSPP = dr["VuelosSP"].S().D();

                    oRSI.dAnticipoInicialPPD = dr["AnticipoInicialPD"].S().D();
                    oRSI.dQuintoAnioPPD = dr["QuintoAnioPD"].S().D();
                    oRSI.dFijoAnualPPD = dr["FijoAnualPD"].S().D();
                    oRSI.dIvaPPD = dr["IvaPD"].S().D();
                    oRSI.dGastosPPD = dr["GastosPD"].S().D();
                    oRSI.dServiciosConCargoPPD = dr["ServiciosCargoPD"].S().D();
                    oRSI.dVuelosPPD = dr["VuelosPD"].S().D();

                    oRSI.dAnticipoInicialPPP = dr["AnticipoInicialPP"].S().D();
                    oRSI.dQuintoAnioPPP = dr["QuintoAnioPP"].S().D();
                    oRSI.dFijoAnualPPP = dr["FijoAnualPP"].S().D();
                    oRSI.dIvaPPP = dr["IvaPP"].S().D();
                    oRSI.dGastosPPP = dr["GastosPP"].S().D();
                    oRSI.dServiciosConCargoPPP = dr["ServiciosCargoPP"].S().D();
                    oRSI.dVuelosPPP = dr["VuelosPP"].S().D();

                }
                //  Fin de Saldos Iniciales

                if (DS.Tables[0].Rows.Count > 1)
                {

                    DS.Tables[0].Rows[5]["CargosP"] = sServCargo == "" ? "0.0000" : sServCargo.D().S();
                    DS.Tables[0].Rows[3]["CargosU"] = sVuelo == "" ? "0.0000" : sVuelo.D().S();

                    DS.Tables[0].Rows[6]["CargosU"] = sIvaD == "" ? "0.0000" : sIvaD.D().S();
                    DS.Tables[0].Rows[6]["CargosP"] = sIvaN == "" ? "0.0000" : sIvaN.D().S();

                    sServCargo = "0.0000";
                    sVuelo = "0.0000";
                    sIvaN = "0.0000";
                    sIvaD = "0.0000";

                    DS.Tables[0].Rows[7]["CargosP"] = "0.0000";
                    DS.Tables[0].Rows[7]["CargosU"] = "0.0000";

                    decimal servicio = SumaImportesTabla(DS.Tables[0], "CargosP");
                    decimal vuelos = SumaImportesTabla(DS.Tables[0], "CargosU");

                    DS.Tables[0].Rows[7]["CargosP"] = servicio.S();
                    DS.Tables[0].Rows[7]["CargosU"] = vuelos.S();


                    string _sServCargo = string.Empty;
                    string _sVuelo = string.Empty;
                    string _sIvaN = string.Empty;
                    string _sIvaD = string.Empty;
                    string _sPagosN = string.Empty;
                    string _sPagosD = string.Empty;


                    _sServCargo = sServCargo;
                    _sVuelo = sVuelo;
                    _sIvaN = sIvaN;
                    _sIvaD = sIvaD;
                    _sPagosN = sPagosN;
                    _sPagosD = sPagosD;

                    if (chbAcumulado.Checked)
                    {
                        DataTable dtRes = new DataTable();
                        //if(FIni > new DateTime(2016,1,1))
                            dtRes = GetSaldoAnterior(ddlContrato.SelectedItem.Value.S().I(), Contrato, FIni, FFin);

                        sServCargo = _sServCargo;
                        sVuelo = _sVuelo;
                        sIvaN = _sIvaN;
                        sIvaD = _sIvaD;
                        sPagosN = _sPagosN;
                        sPagosD = _sPagosD;

                        ViewState["GetEstadoCuentaResumenAcumuladoDLLS"] = ArmaTablaResumenAcumuladoPos(dtRes, DS.Tables[0], oRSI);
                        CreaHtmlResumenAcumulado();
                    }
                    else
                    {
                        DataTable dtRes = new DataTable();
                        //if(FIni > new DateTime(2016,1,1))
                            dtRes = GetSaldoAnterior(ddlContrato.SelectedItem.Value.S().I(), Contrato, FIni, FFin);

                        sServCargo = _sServCargo;
                        sVuelo = _sVuelo;
                        sIvaN = _sIvaN;
                        sIvaD = _sIvaD;
                        sPagosN = _sPagosN;
                        sPagosD = _sPagosD;

                        ViewState["GetEstadoCuentaResumenAcumuladoDLLS"] = ArmaTablaResumenNoAcumuladoPos(dtRes, DS.Tables[0], oRSI);
                        CreaHtmlResumenNoAcumulado();
                    }
                }
                else
                    ViewState["GetEstadoCuentaResumenAcumuladoDLLS"] = null;
            }
            catch (Exception x) { throw x; }
        }

        private DataTable GetSaldoAnterior(int iIdContrato, string sContrato, DateTime dtIni, DateTime dtFin)
        {
            try
            {
                DataTable dtFinal = new DataTable();

                string Contrato = sContrato;
                DateTime FIni = new DateTime(2016, 1, 1);
                DateTime FFin = dtIni.AddDays(-1);

                /*
                 * OBTIENE INFORMACIÓN DE VUELOS DE ESTADO DE CUENTA ANTERIOR
                 */
                sServCargo = "0.0000";
                sVuelo = "0.0000";
                sIvaN = "0.0000";
                sIvaD = "0.0000";
                sPagosN = "0.0000";
                sPagosD = "0.0000";

                DataTable dtVuelos = new DBEstadoCuenta().dtObjVuelosDetail2(iIdContrato, FIni.Date.ToString("MM-dd-yyyy"), FFin.Date.ToString("MM-dd-yyyy"));
                DataTable Ddt = dtVuelos.Select("IdRemision =  0").CopyToDataTable();

                if (Ddt.Rows.Count > 0)
                {
                    sServCargo = Ddt.Rows[0]["ServicioCargo"].S();
                    sVuelo = Ddt.Rows[0]["CostoVuelo"].S();
                }

                DataSet dsFact = new DataSet();
                using (wsSyteline.Iws_SyteLineClient ws = new wsSyteline.Iws_SyteLineClient())
                {
                    ws.Open();
                    dsFact = ws.GetEstadoCuentaFacturacionDLLS(Contrato, FIni, FFin);
                    ws.Close();
                }

                if (dsFact.Tables[0] != null)
                {
                    DataTable dtFact = dsFact.Tables[0].Copy();

                    sIvaN = dtFact.Rows[dtFact.Rows.Count - 1]["IVAMXN"].S();
                    sIvaD = dtFact.Rows[dtFact.Rows.Count - 1]["IVADLLS"].S();
                    
                    sPagosN = dtFact.Rows[dtFact.Rows.Count - 1]["PagosMXN"].S();
                    sPagosD = dtFact.Rows[dtFact.Rows.Count - 1]["PagosDLLS"].S();
                }

                /*
                 * FIN DE ESTADO DE CUENTA ANTERIOR
                */

                DataSet DS = new DataSet();
                using (wsSyteline.Iws_SyteLineClient ws = new wsSyteline.Iws_SyteLineClient())
                {
                    ws.Open();
                    DS = ws.GetEstadoCuentaResumenAcumuladoDLLS(Contrato, FIni, FFin);
                    ws.Close();
                }

                ///
                /// Se Obtienen Gastos Internos del Periodo
                ///
                
                DataTable dtGIP = new DBEstadoCuenta().DBGetObtieneGIdelPeriodo(ddlContrato.SelectedItem.Value.S().I(), FIni, FFin);
                DS = ActualizaGIPeriodo(DS, dtGIP);
                ObtieneSumaResumenPeriodoAct(DS);

                ///
                /// Se obtienen saldos iniciales
                ///
                
                ReporteEdoCuent oRSI = new DBEstadoCuenta().DBGetObtieneSaldosIniciales(ddlContrato.SelectedItem.Value.S(), FIni, FFin);

                DataSet dsMovAnt = new DataSet();
                using (wsSyteline.Iws_SyteLineClient ws = new wsSyteline.Iws_SyteLineClient())
                {
                    ws.Open();
                    dsMovAnt = ws.GetMovimientosAnterioresEdoCuenta(Contrato, FIni);
                    ws.Close();
                }

                if (dsMovAnt.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = dsMovAnt.Tables[0].Rows[0];

                    oRSI.dAnticipoInicialSPD = dr["AnticipoInicialSD"].S().D();
                    oRSI.dQuintoAnioSPD = dr["QuintoAnioSD"].S().D();
                    oRSI.dFijoAnualSPD = dr["FijoAnualSD"].S().D();
                    oRSI.dIvaSPD = dr["IvaSD"].S().D();
                    oRSI.dGastosSPD = dr["GastosSD"].S().D();
                    oRSI.dServiciosConCargoSPD = dr["ServiciosCargoSD"].S().D();
                    oRSI.dVuelosSPD = dr["VuelosSD"].S().D();

                    oRSI.dAnticipoInicialSPP = dr["AnticipoInicialSP"].S().D();
                    oRSI.dQuintoAnioSPP = dr["QuintoAnioSP"].S().D();
                    oRSI.dFijoAnualSPP = dr["FijoAnualSP"].S().D();
                    oRSI.dIvaSPP = dr["IvaSP"].S().D();
                    oRSI.dGastosSPP = dr["GastosSP"].S().D();
                    oRSI.dServiciosConCargoSPP = dr["ServiciosCargoSP"].S().D();
                    oRSI.dVuelosSPP = dr["VuelosSP"].S().D();

                    oRSI.dAnticipoInicialPPD = dr["AnticipoInicialPD"].S().D();
                    oRSI.dQuintoAnioPPD = dr["QuintoAnioPD"].S().D();
                    oRSI.dFijoAnualPPD = dr["FijoAnualPD"].S().D();
                    oRSI.dIvaPPD = dr["IvaPD"].S().D();
                    oRSI.dGastosPPD = dr["GastosPD"].S().D();
                    oRSI.dServiciosConCargoPPD = dr["ServiciosCargoPD"].S().D();
                    oRSI.dVuelosPPD = dr["VuelosPD"].S().D();

                    oRSI.dAnticipoInicialPPP = dr["AnticipoInicialPP"].S().D();
                    oRSI.dQuintoAnioPPP = dr["QuintoAnioPP"].S().D();
                    oRSI.dFijoAnualPPP = dr["FijoAnualPP"].S().D();
                    oRSI.dIvaPPP = dr["IvaPP"].S().D();
                    oRSI.dGastosPPP = dr["GastosPP"].S().D();
                    oRSI.dServiciosConCargoPPP = dr["ServiciosCargoPP"].S().D();
                    oRSI.dVuelosPPP = dr["VuelosPP"].S().D();

                }
                
                //  Fin de Saldos Iniciales

                if (DS.Tables[0].Rows.Count > 1)
                {    
                    DS.Tables[0].Rows[5]["CargosP"] = sServCargo == "" ? "0.0000" : sServCargo.D().S();
                    DS.Tables[0].Rows[3]["CargosU"] = sVuelo == "" ? "0.0000" : sVuelo.D().S();

                    DS.Tables[0].Rows[6]["CargosU"] = sIvaD == "" ? "0.0000" : sIvaD.D().S();
                    DS.Tables[0].Rows[6]["CargosP"] = sIvaN == "" ? "0.0000" : sIvaN.D().S();

                    DS.Tables[0].Rows[7]["CargosP"] = "0.0000";
                    DS.Tables[0].Rows[7]["CargosU"] = "0.0000";

                    decimal servicio = SumaImportesTabla(DS.Tables[0], "CargosP");
                    decimal vuelos = SumaImportesTabla(DS.Tables[0], "CargosU");

                    DS.Tables[0].Rows[7]["CargosP"] = servicio.S();
                    DS.Tables[0].Rows[7]["CargosU"] = vuelos.S();

                    if (chbAcumulado.Checked)
                    {
                        dtFinal = ArmaTablaResumenAcumulado(new DataTable(), DS.Tables[0], oRSI);
                    }
                    else
                    {
                        dtFinal = ArmaTablaResumenNoAcumulado(new DataTable(), DS.Tables[0], oRSI);
                    }
                }

                return dtFinal;
            }
            catch (Exception x) 
            { 
                throw x;
            }
        }

        private DataSet ActualizaGIPeriodo(DataSet ds, DataTable dt)
        {
            try
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    if (row.S("ConceptoU") == "ANTICIPO INICIAL")
                    {
                        row["CargosU"] = row.S("CargosU").D() + dt.Rows[0]["AnticipoInicialD"].S().D();
                        row["CargosP"] = row.S("CargosP").D() + dt.Rows[0]["AnticipoInicialP"].S().D();
                    }
                    if (row.S("ConceptoU") == "QUINTO AÑO")
                    {
                        row["CargosU"] = row.S("CargosU").D() + dt.Rows[0]["QuintoAnioD"].S().D();
                        row["CargosP"] = row.S("CargosP").D() + dt.Rows[0]["QuintoAnioP"].S().D();
                    }
                    if (row.S("ConceptoU") == "FIJO ANUAL")
                    {
                        row["CargosU"] = row.S("CargosU").D() + dt.Rows[0]["FijoAnualD"].S().D();
                        row["CargosP"] = row.S("CargosP").D() + dt.Rows[0]["FijoAnualP"].S().D();
                    }
                    if (row.S("ConceptoU") == "VUELOS")
                    {
                        row["CargosU"] = row.S("CargosU").D() + dt.Rows[0]["VuelosD"].S().D();
                        row["CargosP"] = row.S("CargosP").D() + dt.Rows[0]["VuelosP"].S().D();
                    }
                    if (row.S("ConceptoU") == "GASTOS")
                    {
                        row["CargosU"] = row.S("CargosU").D() + dt.Rows[0]["GastosD"].S().D();
                        row["CargosP"] = row.S("CargosP").D() + dt.Rows[0]["GastosP"].S().D();
                    }
                    if (row.S("ConceptoU") == "SERVICIOS CON CARGO")
                    {
                        row["CargosU"] = row.S("CargosU").D() + dt.Rows[0]["ServiciosCargoD"].S().D();
                        row["CargosP"] = row.S("CargosP").D() + dt.Rows[0]["ServiciosCargoP"].S().D();
                    }
                    if (row.S("ConceptoU") == "I.V.A.")
                    {
                        row["CargosU"] = row.S("CargosU").D() + dt.Rows[0]["IvaD"].S().D();
                        row["CargosP"] = row.S("CargosP").D() + dt.Rows[0]["IvaP"].S().D();
                    }
                }

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ObtieneSumaResumenPeriodoAct(DataSet ds)
        {
            try
            {
                decimal dSumaD = 0;
                decimal dSumaP = 0;
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    if (row.S("ConceptoU") == "ANTICIPO INICIAL" || row.S("ConceptoU") == "QUINTO AÑO" || row.S("ConceptoU") == "FIJO ANUAL" ||
                        row.S("ConceptoU") == "VUELOS" || row.S("ConceptoU") == "GASTOS" || row.S("ConceptoU") == "SERVICIOS CON CARGO" || row.S("ConceptoU") == "I.V.A.")
                    {
                        dSumaD += row.S("CargosU").D();
                        dSumaP += row.S("CargosP").D();
                    }
                }

                ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1]["CargosU"] = dSumaD;
                ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1]["CargosP"] = dSumaP;
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
                dtObjCat.Rows[0].Delete();
                //DataTable DT = dtObjCat.Copy();
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
        public void LoadVueloHead(DataTable dtObjCat)
        {
            try
            {
                ViewState["gvVuelo"] = dtObjCat;
            }
            catch (Exception x) { throw x; }
        }
        public void LoadVueloDetail(DataTable dtObjCat)
        {
            try
            {
                ViewState["LoadVueloDetail"] = dtObjCat;
            }
            catch (Exception x) { throw x; }
        }
        public void LoadVueloEqDif(DataTable dtObjCat)
        {
            try
            {
                ViewState["LoadVueloEqDif"] = dtObjCat;
            }
            catch (Exception x) { throw x; }
        }
        protected void CreaHtml()
        {
            try
            {
                DataTable Header = (DataTable)ViewState["gvVuelo"];
                DataTable Detalle = (DataTable)ViewState["LoadVueloDetail"];

                ArmaDataTableVuelos();

                float fTiemp = 0;
                string TrNalTiempo = Utils.ObtieneTotalTiempo(Header, "TrNalTiempo", ref fTiemp);
                string TrIntTiempo = Utils.ObtieneTotalTiempo(Header, "TrIntTiempo", ref fTiemp);
                string TeNalTiempo = Utils.ObtieneTotalTiempo(Header, "TeNalTiempo", ref fTiemp);
                string TeIntTiempo = Utils.ObtieneTotalTiempo(Header, "TeIntTiempo", ref fTiemp);

                decimal TotalPNal = SumaImportesTabla(Header, "PNalTiempo");
                decimal TotalPInt = SumaImportesTabla(Header, "PIntTiempo");

                string _ippuertoserver = Request.Url.Authority;

                string cadena = @"<table border='0' bgColor='#ffffff' borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; background:white; width: 1400px'>" +
                "<tr bgcolor='#0D2240'><td colspan='16' style='border-bottom : none; border-left:none; border-top:none; border-right:none; text-align: center; vertical-align: middle'><font color='#ffffff'><B>V U E L O S</B></font></td></tr>" +
                "<tr bgcolor='#EEEFF0'><td style='border-bottom : none; border-left:none;  border-right:none;'><B>&nbsp;FECHA DE</B></td><td style='border-bottom : none; border-left:none; border-right:none;'><B>&nbsp;FECHA DE</B></td><td style='border-bottom : none; border-left:none; border-right:none;'><B>REMISIÓN &nbsp;</B></td><td style='border-bottom : none; border-left:none; border-right:none;'><B>PREFACTURA &nbsp;</B></td><td style='border-bottom : none; border-left:none; border-right:none;'><B>MATRICULA &nbsp;</B></td><td style='border-bottom : none; border-left:none; border-right:none;'><B>RUTA</B></td><td colspan='2' style='border-bottom : none; border-left:none; border-right:none; text-align: center;  vertical-align: middle' ><B>COMBUSTIBLE</B></td>" +
                "<td colspan='2' style='border-bottom : none; border-left:none; border-right:none; text-align: center; vertical-align: middle'><B>TRAMO</B></td><td colspan='2' style='border-bottom : none; border-left:none; border-right:none;text-align: center;  vertical-align: middle'><B>TIEMPO DE ESPERA</B></td><td colspan='2' style='border-bottom : none; border-left:none; border-right:none; text-align: center;  vertical-align: middle'><B>PERNOCTAS</B></td><td style='border-bottom : none; border-left:none; border-right:none;'><B>COSTO DE</B></td><td style='border-bottom : none; border-left:none; border-right:none;'><B>SERVICIOS CON</B></td></tr>" +

                "<tr bgcolor='#EEEFF0'><td style='border-left:none; border-top:none; border-right:none;'><B>&nbsp;SALIDA</B></td><td style='border-left:none; border-top:none; border-right:none;'><B>&nbsp;LLEGADA</B></td><td colspan='4' style='border-left:none; border-top:none; border-right:none;'></td><td style='border-left:none; border-top:none; border-right:none; text-align: center;'><B>NACIONAL</B></td>" +
                "<td style='border-left:none; border-top:none; border-right:none; text-align: center;'><B>INTERNACIONAL</B></td><td style='border-left:none; border-top:none; border-right:none; text-align: center;'><B>NACIONAL</B></td><td style='border-left:none; border-top:none; border-right:none; text-align: center;'><B>INTERNACIONAL</B></td><td style='border-left:none; border-top:none; border-right:none; text-align: center;'><B>NACIONAL</B></td><td style='border-left:none; border-top:none; border-right:none; text-align: center;'><B>INTERNACIONAL</B></td><td style='border-left:none; border-top:none; border-right:none; text-align: center;'><B>NACIONAL</B></td><td style='border-left:none; border-top:none; border-right:none; text-align: center;'><B>INTERNACIONAL</B></td><td style='border-left:none; border-top:none; border-right:none; text-align: center;'><B>VUELO (USD)</B></td><td style='border-left:none; border-top:none; border-right:none; text-align: center;'><B>CARGO (MXN)</B></td></tr>";


                if (Header != null || Header.Rows.Count != 0)
                {
                    for (int i = 0; i < Header.Rows.Count; i++)
                    {
                        cadena = cadena + "<tr>";
                        for (int c = 0; c <= 13; c++)
                        {
                            if (c >= 7)
                                cadena = cadena + "<td style='border-width: 0px; text-align: right;'>" + Header.Rows[i][c].S() + "</td>";
                            else if (c == 2 || c == 3)
                                cadena = cadena + "<td style='border-width: 0px; text-align: right;'>" + Header.Rows[i][c].S() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>";
                            else
                                cadena = cadena + "<td style='border-width: 0px'>" + Header.Rows[i][c].S() + "</td>";
                        }
                        cadena = cadena + "<td colspan='2' style='border-width: 0px'></td></tr>";

                        string x = Header.Rows[i]["IdRemision"].S();
                        DataTable Dt = Detalle.Select("IdRemision =  " + x.S() + "").CopyToDataTable();

                        for (int z = 0; z < Dt.Rows.Count; z++)
                        {
                            cadena = cadena + "<tr><td colspan='8' style='border-width: 0px'></td>";
                            for (int y = 7; y < 13; y++)
                            {
                                cadena = cadena + "<td style='border-width: 0px; text-align: right;'> " + Dt.Rows[0][y].S() + "</td>";
                            }
                            cadena = cadena + "<td colspan='2' style='border-width: 0px'></td></tr>";

                            cadena = cadena + "<tr><td colspan='8' style='border-bottom : none; border-left:none; border-top:none; border-right:none;' ></td>";
                            for (int w = 13; w < 21; w++)
                            {
                                cadena = cadena + "<td style='border-left:none;  border-right:none; text-align: right;'> " + Convert.ToDecimal(Dt.Rows[0][w]).ToString("N2") + "</td>";
                            }
                            cadena = cadena + "</tr>";

                            for (int t = 21; t < Dt.Columns.Count; t++)
                            {
                                if (Dt.Rows[0][t].B() && Dt.Columns[t].ColumnName.Equals("AplicaFactorEspecial"))
                                {
                                    cadena = cadena + "<tr><td colspan='8' style='border-width: 0px'></td>";
                                    cadena = cadena + "<td style='border-width: 0px'> Factor Especial " + Dt.Rows[0][1].S() + "</td>";
                                    cadena = cadena + "<td colspan='7' style='border-width: 0px'></td></tr>";
                                }
                                else if (Dt.Rows[0][t].B() && Dt.Columns[t].ColumnName.Equals("GiraHorario"))
                                {
                                    cadena = cadena + "<tr><td colspan='8' style='border-width: 0px'></td>";
                                    cadena = cadena + "<td style='border-width: 0px'> Factor Gira Horario " + Dt.Rows[0][2].S() + "</td>";
                                    cadena = cadena + "<td colspan='7' style='border-width: 0px'></td></tr>";
                                }
                                else if (Dt.Rows[0][t].B() && Dt.Columns[t].ColumnName.Equals("Gira"))
                                {
                                    cadena = cadena + "<tr><td colspan='8' style='border-width: 0px'></td>";
                                    cadena = cadena + "<td style='border-width: 0px'> Factor Gira " + Dt.Rows[0][3].S() + "</td>";
                                    cadena = cadena + "<td colspan='7' style='border-width: 0px'></td></tr>";
                                }
                                else if (Dt.Rows[0][t].B() && Dt.Columns[t].ColumnName.Equals("Intercamio"))
                                {
                                    cadena = cadena + "<tr><td colspan='8' style='border-width: 0px'></td>";
                                    cadena = cadena + "<td style='border-width: 0px'> Factor Intercambio " + Dt.Rows[0][4].S() + "</td>";
                                    cadena = cadena + "<td colspan='7' style='border-width: 0px'></td></tr>";
                                }
                                else if (Dt.Rows[0][t].B() && Dt.Columns[t].ColumnName.Equals("AplicaFechaPico"))
                                {
                                    cadena = cadena + "<tr><td colspan='8' style='border-width: 0px'></td>";
                                    cadena = cadena + "<td style='border-width: 0px'> Factor Fecha Pico " + Dt.Rows[0][5].S() + "</td>";
                                    cadena = cadena + "<td colspan='7' style='border-width: 0px'></td></tr>";
                                }
                                else if (Dt.Rows[0][t].B() && Dt.Columns[t].ColumnName.Equals("AplicaVueloSimultaneo"))
                                {
                                    cadena = cadena + "<tr><td colspan='8' style='border-width: 0px'></td>";
                                    cadena = cadena + "<td style='border-width: 0px'> Factor Vuelo Simultaneo " + Dt.Rows[0][6].S() + "</td>";
                                    cadena = cadena + "<td colspan='7' style='border-width: 0px'></td></tr>";
                                }

                                else if (Dt.Rows[0][t].B() && Dt.Columns[t].ColumnName.Equals("AplicaFactorTramoNal"))
                                {
                                    cadena = cadena + "<tr><td colspan='8' style='border-width: 0px'></td>";
                                    cadena = cadena + "<td style='border-width: 0px'> Factor Tramo Nacional " + Dt.Rows[0]["FactorTramoNal"].S() + "</td>";
                                    cadena = cadena + "<td colspan='7' style='border-width: 0px'></td></tr>";
                                }

                                else if (Dt.Rows[0][t].B() && Dt.Columns[t].ColumnName.Equals("AplicaFactorTramoInt"))
                                {
                                    cadena = cadena + "<tr><td colspan='8' style='border-width: 0px'></td>";
                                    cadena = cadena + "<td style='border-width: 0px'> Factor Tramo Internacional " + Dt.Rows[0]["FactorTramoInt"].S() + "</td>";
                                    cadena = cadena + "<td colspan='7' style='border-width: 0px'></td></tr>";
                                }
                            }
                            cadena = cadena + "<tr><td colspan='16' style='border-bottom : none; border-left:none; border-top:none; border-right:none;'>&nbsp;</td></tr>";
                        }
                    }

                    DataTable Ddt = Detalle.Select("IdRemision =  0").CopyToDataTable();

                    cadena = cadena + "<tr><td colspan='8' style='border-width: 0px'></td>";
                    cadena = cadena + "<td style='border-width: 0px;text-align: right;'> " + TrNalTiempo + "</td>";
                    cadena = cadena + "<td style='border-width: 0px;text-align: right;'> " + TrIntTiempo + "</td>";
                    cadena = cadena + "<td style='border-width: 0px;text-align: right;'> " + TeNalTiempo + "</td>";
                    cadena = cadena + "<td style='border-width: 0px;text-align: right;'> " + TeIntTiempo + "</td>";
                    cadena = cadena + "<td style='border-width: 0px;text-align: right;'> " + TotalPNal + " </td>";
                    cadena = cadena + "<td style='border-width: 0px;text-align: right;'> " + TotalPInt + " </td>";
                    cadena = cadena + "<td style='border-width: 0px'> </td>";
                    cadena = cadena + "<td style='border-width: 0px'> </td>";
                    cadena = cadena + "</tr>";

                    cadena = cadena + "<tr><td colspan='8' style='border-width: 0px'></td>";
                    for (int y = 13; y < 21; y++)
                    {
                        cadena = cadena + "<td style='border-width: 0px; text-align: right;'><B>" + Convert.ToDecimal(Ddt.Rows[0][y].D()).ToString("N2") + "</B></td>";
                    }
                    sServCargo = Ddt.Rows[0]["ServicioCargo"].S();
                    sVuelo = Ddt.Rows[0]["CostoVuelo"].S();
                    // cadena = cadena + "<td colspan='2' style='border-width: 0px'></td></tr>";
                }


                cadena = cadena + "</table>";
                Prueba.InnerHtml = cadena;
            }
            catch (Exception x) { throw x; }
        }
        protected void CreaHtmlVuelEfecEquipDif()
        {
            try
            {
                DataTable LoadVueloEqDif = (DataTable)ViewState["LoadVueloEqDif"];

                foreach (DataColumn col in LoadVueloEqDif.Columns)
                    col.ReadOnly = false;

                foreach (DataRow row in LoadVueloEqDif.Rows)
                {
                    long lIdRemision = row.S("IdRemision").L();

                    DataTable dtTr = new DBRemision().DBGetConsultaTramosRemisionExistentes(lIdRemision);

                    DatosRemision odRem = new DBRemision().DBGetObtieneDatosRemision(lIdRemision, ddlContrato.SelectedItem.Value.S().I());
                    odRem.dtTramos = dtTr.Copy();
                    DataTable dtTiempo = Utils.CalculaCostosRemisionEdoCuenta(odRem.iCobroTiempo, odRem.dtTramos, odRem);

                    row["TrNalTiempo"] = dtTiempo.Rows[0].S("Cantidad");
                    row["TrIntTiempo"] = dtTiempo.Rows[1].S("Cantidad");
                    row["TeNalTiempo"] = dtTiempo.Rows[2].S("Cantidad");
                    row["TeIntTiempo"] = dtTiempo.Rows[3].S("Cantidad");
                    row["PNalTotal"] = dtTiempo.Rows[4].S("Cantidad");
                    row["PIntTotal"] = dtTiempo.Rows[5].S("Cantidad");
                }

                float fTiemp = 0;
                string TrNalTiempo = Utils.ObtieneTotalTiempo(LoadVueloEqDif, "TrNalTiempo", ref fTiemp);
                string TrIntTiempo = Utils.ObtieneTotalTiempo(LoadVueloEqDif, "TrIntTiempo", ref fTiemp);
                string TeNalTiempo = Utils.ObtieneTotalTiempo(LoadVueloEqDif, "TeNalTiempo", ref fTiemp);
                string TeIntTiempo = Utils.ObtieneTotalTiempo(LoadVueloEqDif, "TeIntTiempo", ref fTiemp);

                decimal TotalPNal = SumaImportesTabla(LoadVueloEqDif, "PNalTotal");
                decimal TotalPInt = SumaImportesTabla(LoadVueloEqDif, "PIntTotal");

                string cadena = @"<table border='0' bgColor='#ffffff' borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; background:white; width:100%;'>" +
                "<tr ><td colspan='2' style='border-bottom : none; border-left:none; border-top:none; border-right:none;'></td><td bgcolor='#0D2240' colspan='12' style='border-bottom : none; border-left:none; border-top:none; border-right:none; text-align: center; vertical-align: middle'><font color='#ffffff'><B>V U E L O S &nbsp; E F E C T U A D O S &nbsp; E N &nbsp; E Q U I P O S &nbsp; D I F E R E N T E S &nbsp; A L &nbsp; C O N T R A T O </B></font></td></tr>" +
                "<tr><td colspan='2' style='border-bottom : none; border-left:none; border-top:none; border-right:none;'></td><td bgcolor='#EEEFF0' style='border-bottom : none; border-left:none;  border-right:none; text-align: center;'><B>FECHA DE</B></td><td bgcolor='#EEEFF0' style='border-bottom : none; border-left:none;  border-right:none; text-align: center;'><B>&nbsp;FECHA DE&nbsp;</B></td><td bgcolor='#EEEFF0' style='border-bottom : none; border-left:none; border-right:none; text-align: center;'><B>&nbsp;REMISIÓN&nbsp</B></td><td bgcolor='#EEEFF0' style='border-bottom : none; border-left:none; border-right:none; text-align: center;'><B>&nbsp;PREFACTURA&nbsp;</B></td><td bgcolor='#EEEFF0' style='border-bottom : none; border-left:none;  border-right:none; text-align: center;'><B>&nbsp;MATRICULA&nbsp;</B></td><td bgcolor='#EEEFF0' style='border-bottom : none; border-left:none;  border-right:none; text-align: center;'><B>&nbsp;&nbsp;&nbsp;RUTA&nbsp;&nbsp;&nbsp;</B></td>" +
                "<td bgcolor='#EEEFF0' colspan='2' style='border-bottom : none; border-left:none;  border-right:none; text-align: center;'><B>TRAMO</B></td><td bgcolor='#EEEFF0' colspan='2' style='border-bottom : none; border-left:none;  border-right:none; text-align: center;'><B>TIEMPO DE ESPERA</B></td><td bgcolor='#EEEFF0' colspan='2' style='border-bottom : none; border-left:none;  border-right:none; text-align: center;'><B>PERNOCTAS</B></td></tr>" +

                "<tr><td colspan='2' style='border-bottom : none; border-left:none; border-top:none; border-right:none;'></td><td bgcolor='#EEEFF0' style='border-left:none; border-top:none; border-right:none; text-align: center;'><B>SALIDA</B></td><td bgcolor='#EEEFF0' style='border-left:none; border-top:none; border-right:none; text-align: center;'><B>LLEGADA</B></td><td bgcolor='#EEEFF0' colspan='4' style='border-left:none; border-top:none; border-right:none;'></td>" +
                "<td bgcolor='#EEEFF0' style='border-left:none; border-top:none; border-right:none; text-align: center;'><B>NACIONAL</B></td>" +
                "<td bgcolor='#EEEFF0' style='border-left:none; border-top:none; border-right:none; text-align: center;'><B>INTERNACIONAL</B></td><td bgcolor='#EEEFF0' style='border-left:none; border-top:none; border-right:none; text-align: center;'><B>NACIONAL</B></td><td bgcolor='#EEEFF0' style='border-left:none; border-top:none; border-right:none; text-align: center;'><B>INTERNACIONAL</B></td><td bgcolor='#EEEFF0' style='border-left:none; border-top:none; border-right:none; text-align: center;'><B>NACIONAL</B></td><td bgcolor='#EEEFF0' style='border-left:none; border-top:none; border-right:none; text-align: center;'><B>INTERNACIONAL</B></td></tr>";


                if (LoadVueloEqDif != null || LoadVueloEqDif.Rows.Count != 0)
                {

                    for (int i = 0; i < LoadVueloEqDif.Rows.Count; i++)
                    {
                        cadena = cadena + "<tr><td colspan='2' style='border-bottom : none; border-left:none; border-top:none; border-right:none;'></td>";
                        for (int c = 1; c < LoadVueloEqDif.Columns.Count; c++)
                        {
                            if (c <= 6)
                                cadena = cadena + "<td style='border-width: 0px; text-align: center;'>" + LoadVueloEqDif.Rows[i][c].S() + "</td>";
                            else
                                cadena = cadena + "<td style='border-width: 0px; text-align: right;'>" + LoadVueloEqDif.Rows[i][c].S() + "&nbsp;&nbsp;</td>";
                        }
                        cadena = cadena + "</tr>";
                    }

                    cadena = cadena + "<tr><td colspan='2' style='border-bottom : none; border-left:none; border-top:none; border-right:none;'></td><td colspan='6' style='border-width: 0px'></td>";
                    cadena = cadena + "<td style='border-left:none;  border-right:none; text-align: right;'><B>" + TrNalTiempo + "&nbsp;&nbsp;</B></td>";
                    cadena = cadena + "<td style='border-left:none;  border-right:none; text-align: right;'><B>" + TrIntTiempo + "&nbsp;&nbsp;</B></td>";
                    cadena = cadena + "<td style='border-left:none;  border-right:none; text-align: right;'><B>" + TeNalTiempo + "&nbsp;&nbsp;</B></td>";
                    cadena = cadena + "<td style='border-left:none;  border-right:none; text-align: right;'><B>" + TeIntTiempo + "&nbsp;&nbsp;</B></td>";
                    cadena = cadena + "<td style='border-left:none;  border-right:none; text-align: right;'><B>" + TotalPNal + "&nbsp;&nbsp;</B></td>";
                    cadena = cadena + "<td style='border-left:none;  border-right:none; text-align: right;'><B>" + TotalPInt + "&nbsp;&nbsp;</B></td>";

                    cadena = cadena + "</tr>";

                }

                cadena = cadena + "</table>";
                VuelEfecEquipDif.InnerHtml = cadena;
                ArmaDataTableVuelosdif();
            }
            catch (Exception x) { throw x; }
        }
        protected void CreaHtmlFacturacion()
        {
            try
            {
                DataTable CargaEstadoCuentaMXN = (DataTable)ViewState["CargaEstadoCuentaMXN"];

                string cadena = @"<table border='0' bgColor='#ffffff' borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; background:white; width:100%;'>" +
                "<tr ><td colspan='2'></td><td bgcolor='#0D2240' colspan='11' style='border-bottom : none; border-left:none; border-top:none; border-right:none; text-align: center; vertical-align: middle'><font color='#ffffff'><B>F A C T U R A C I Ó N</B></font></td></tr>" +
                "<tr><td colspan='2'></td><td bgcolor='#EEEFF0' style='border-bottom : none; border-left:none; border-right:none;'><B>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;FECHA&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</B></td><td bgcolor='#EEEFF0' style='border-bottom : none; border-left:none; border-right:none;'><B>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;DOCUMENTO&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</B></td><td bgcolor='#EEEFF0' style='border-bottom : none; border-left:none; border-right:none;'><B>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;RUTA Y/O CONCEPTO&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</B></td>" +
                "<td bgcolor='#EEEFF0' colspan='4' style='border-bottom : none; border-left:none;  border-right:none; text-align: center;'><B>DOLARES</B></td><td bgcolor='#EEEFF0' colspan='4' style='border-bottom : none; border-left:none;  border-right:none; text-align: center;'><B>MONEDA NACIONAL</B></td></tr>" +
                "<tr><td colspan='2'></td><td bgcolor='#EEEFF0' colspan='3' style='border-left:none; border-top:none; border-right:none;'></td><td bgcolor='#EEEFF0' style='border-left:none; border-top:none; border-right:none; text-align: center;'><B>PAGOS</B></td><td bgcolor='#EEEFF0' style='border-left:none; border-top:none; border-right:none; text-align: center;'><B>IMPORTE</B></td>" +
                "<td bgcolor='#EEEFF0' style='border-left:none; border-top:none; border-right:none; text-align: center;'><B>&nbsp;&nbsp;&nbsp;IVA&nbsp;&nbsp;&nbsp;</B></td><td bgcolor='#EEEFF0' style='border-left:none; border-top:none; border-right:none; text-align: center;'><B>&nbsp;&nbsp;&nbsp;TOTAL&nbsp;&nbsp;&nbsp;</B></td>" +
                "<td bgcolor='#EEEFF0' style='border-left:none; border-top:none; border-right:none; text-align: center;'><B>&nbsp;&nbsp;&nbsp;PAGOS&nbsp;&nbsp;&nbsp;</B></td><td bgcolor='#EEEFF0' style='border-left:none; border-top:none; border-right:none; text-align: center;'><B>&nbsp;&nbsp;&nbsp;IMPORTE&nbsp;&nbsp;&nbsp;</B></td><td bgcolor='#EEEFF0' style='border-left:none; border-top:none; border-right:none; text-align: center;'><B>&nbsp;&nbsp;&nbsp;IVA&nbsp;&nbsp;&nbsp;</B></td><td bgcolor='#EEEFF0' style='border-left:none; border-top:none; border-right:none; text-align: center;'><B>&nbsp;&nbsp;&nbsp;TOTAL&nbsp;&nbsp;&nbsp;</B></td></tr>";

                if (CargaEstadoCuentaMXN != null)
                {
                    int RowMax = CargaEstadoCuentaMXN.Rows.Count - 1;

                    //DETALLE
                    for (int i = 0; i < CargaEstadoCuentaMXN.Rows.Count - 1; i++)
                    {
                        cadena = cadena + "<tr><td colspan='2'></td>";
                        for (int c = 0; c < CargaEstadoCuentaMXN.Columns.Count; c++)
                        {
                            if (c <= 2)
                                cadena = cadena + "<td style='border-width: 0px; '>&nbsp;&nbsp;" + CargaEstadoCuentaMXN.Rows[i][c].S() + "&nbsp;&nbsp;</td>";
                            else
                                cadena = cadena + "<td style='border-width: 0px; text-align: right;'>" + CargaEstadoCuentaMXN.Rows[i][c].S() + "</td>";
                        }
                        cadena = cadena + "</tr>";
                    }

                    //TOTAL
                    cadena = cadena + "<tr><td colspan='2'></td><td style='border-width: 0px' colspan='3'></td>";
                    for (int c = 3; c < CargaEstadoCuentaMXN.Columns.Count; c++)
                    {
                        if (CargaEstadoCuentaMXN.Rows[RowMax][c].S().Equals(""))
                            cadena = cadena + "<td style='border-left:none; border-right:none;'><B>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + CargaEstadoCuentaMXN.Rows[RowMax][c].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</B></td>";
                        else
                            cadena = cadena + "<td style='border-left:none; border-right:none; text-align: right;'><B>" + Convert.ToDecimal(CargaEstadoCuentaMXN.Rows[RowMax][c]).ToString("N2") + "</B></td>";
                    }
                    cadena = cadena + "</tr>";

                    cadena = cadena + "</tr>";

                    sIvaN = CargaEstadoCuentaMXN.Rows[RowMax]["IVAMXN"].ToString();
                    sIvaD = CargaEstadoCuentaMXN.Rows[RowMax]["IVADLLS"].ToString();
                    sPagosN = CargaEstadoCuentaMXN.Rows[RowMax]["PagosMXN"].ToString();
                    sPagosD = CargaEstadoCuentaMXN.Rows[RowMax]["PagosDLLS"].ToString();
                }
                cadena = cadena + "</table>";
                Facturacion.InnerHtml = cadena;
            }
            catch (Exception x) { throw x; }
        }
        protected void CreaHtmlResumenNoAcumulado()
        {
            try
            {

                ResumenAcumulado.InnerHtml = string.Empty;

                DataTable Acumulado = (DataTable)ViewState["GetEstadoCuentaResumenAcumuladoDLLS"];


                string cadena = @"<table border='0' bgColor='#ffffff' borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; background:white; width:100%;'>" +
                "<tr ><td colspan='3'></td><td bgcolor='#0D2240'colspan='10' style='border-bottom : none; border-left:none; border-top:none; border-right:none; text-align: center; vertical-align: middle'><font color='#ffffff'><B>R E S U M E N</B></font></td></tr>" +
                "<tr ><td colspan='3'></td><td bgcolor='#EEEFF0' colspan='5' style='border-bottom : none; border-left:none; border-right:none; text-align: center; vertical-align: middle'><B>D O L A R E S</B></td><td bgcolor='#EEEFF0' colspan='5' style='border-bottom : none; border-left:none; border-right:none; text-align: center; vertical-align: middle'><B>M O N E D A&nbsp;&nbsp;&nbsp;N A C I O N A L</B></td></tr>" +
                "<tr ><td colspan='3'></td><td bgcolor='#EEEFF0' style='border-bottom : none; border-left:none; border-right:none;'><B>&nbsp;&nbsp;&nbsp;CONCEPTO</B></td><td bgcolor='#EEEFF0' style='border-bottom : none; border-left:none; border-right:none; text-align: right;'><B>SALDO ANTERIOR&nbsp;&nbsp;&nbsp;</B></td><td bgcolor='#EEEFF0' style='border-bottom : none; border-left:none; border-right:none; text-align: center;' colspan='2'><B>EN ESTE PERIODO</B></td>" +
                "<td bgcolor='#EEEFF0' style='border-bottom : none; border-left:none;  border-right:none; text-align: right;'><B>SALDO NUEVO&nbsp;&nbsp;&nbsp;</B></td><td bgcolor='#EEEFF0' style='border-bottom : none; border-left:none;  border-right:none;'><B>&nbsp;&nbsp;&nbsp;CONCEPTO</B></td><td bgcolor='#EEEFF0' style='border-bottom : none; border-left:none;  border-right:none; text-align: right;'><B>SALDO ANTERIOR&nbsp;&nbsp;&nbsp;</B></td><td bgcolor='#EEEFF0' style='border-bottom : none; border-left:none;  border-right:none; text-align:center;' colspan='2'><B>EN ESTE PERIODO</B></td><td bgcolor='#EEEFF0' style='border-bottom : none; border-left:none;  border-right:none; text-align: right;'><B>SALDO NUEVO&nbsp;&nbsp;&nbsp;</B></td></tr>" +
                "<tr><td colspan='3'></td><td bgcolor='#EEEFF0' style='border-left:none;  border-top:none; border-right:none;' colspan='2'></td><td bgcolor='#EEEFF0' style='border-top:none; border-left:none; border-right:none; text-align: right;'><B>CARGOS</B></td><td bgcolor='#EEEFF0' style='border-left:none; border-top:none; border-right:none; text-align: right;'><B>ABONOS</B></td>" +
                "<td bgcolor='#EEEFF0' style='border-top:none; border-left:none; border-right:none;' colspan='3'></td><td bgcolor='#EEEFF0' style='border-left:none; border-right:none; border-top:none; text-align: right;'><B>CARGOS</B></td><td bgcolor='#EEEFF0' style='border-left:none; border-right:none; border-top:none; text-align: center; text-align: right;'><B>ABONOS</B></td><td bgcolor='#EEEFF0' style='border-left:none; border-right:none; border-top:none; text-align: center;'></td></tr>";

                if (Acumulado != null || Acumulado.Rows.Count != 0)
                {
                    int RowMax = Acumulado.Rows.Count - 1;

                    //DETALLE
                    for (int i = 0; i < Acumulado.Rows.Count - 1; i++)
                    {
                        cadena = cadena + "<tr><td colspan='3'></td>";
                        for (int c = 0; c < Acumulado.Columns.Count; c++)
                        {
                            if (c == 0 || c == 5)
                                cadena = cadena + "<td style='border-width: 0px;'>&nbsp;&nbsp;&nbsp; " + Acumulado.Rows[i][c].S() + "</td>";
                            else
                                cadena = cadena + "<td style='border-width: 0px; text-align: right;'>" + Acumulado.Rows[i][c].D().ToString("N2") + "</td>";
                        }
                        cadena = cadena + "</tr>";
                    }


                    //TOTAL
                    cadena = cadena + "<tr><td colspan='3'></td>";
                    for (int c = 0; c < Acumulado.Columns.Count; c++)
                    {
                        if (c == 0 || c == 5)
                            cadena = cadena + "<td style='border-left:none; border-right:none;'><B>&nbsp;&nbsp;" + Acumulado.Rows[RowMax][c].S() + "</B></td>";
                        else
                            cadena = cadena + "<td style='border-left:none; border-right:none; text-align: right;'><B>" + Acumulado.Rows[RowMax][c].D().ToString("N2") + "</B></td>";
                    }
                    cadena = cadena + "</tr>";
                }
                cadena = cadena + "</table>";

                Resumen.InnerHtml = cadena;
            }
            catch (Exception x) { throw x; }
        }
        private void ExportExcel(string cadena)
        {
            DataTable Head = (DataTable)ViewState["LoadHeadEdoCnta"];
            float fTiemp = 0;
            string HrsVol = Utils.ObtieneTotalTiempo(Head, "HrsVoladas", ref fTiemp);


            string _ippuertoserver = Utils.ObtieneParametroPorClave("53");

            DataTable DT = (DataTable)ViewState["oCliente"];

            DT = DT.Select("IdCliente = " + ddlClientes.SelectedItem.Value.S() + "").CopyToDataTable();
            string Razonsocial = DT.Rows[0]["Nombre"].S();
            //string _ippuertoserver = Request.Url.Authority;
            NombreSocial = Razonsocial;

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=EstadoCuenta - " + hfCliente["hfCliente"].S() + hfFechaInicial["hfFechaInicial"].S() + " - " + hfFechaFinal["hfFechaFinal"].S() + ".xls");
            HttpContext.Current.Response.Charset = "iso-8859-1";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1252");

            HttpContext.Current.Response.Write(@"<table bgcolor='#0D2240' ><tr><td style=' text-align: center; vertical-align: middle' colspan='2'><img src='http://" + _ippuertoserver.S() + "/img/AleAzul2.PNG' height='100px' width='90px'/></td>" +
             "<td style=' text-align: center; font-size: x-large; vertical-align: middle' colspan='9'></td><td colspan='2'><font color='#ffffff'>Cliente: <br /> Contrato:</font></td><td colspan='3'><font color='#ffffff'>" + hfCliente["hfCliente"].S() + "<br /> " + hfContrato["hfContrato"].S() + "</font></td></tr>" +
             "<tr><td style=' text-align: center; vertical-align: middle' colspan='2'></td>" +
             "<td style=' text-align: center; font-size: x-large; vertical-align: middle' colspan='9'></td><td colspan='2'><font color='#ffffff'>Equipo Contratado: <br /> Paquete:</font></td><td colspan='3'><font color='#ffffff'>" + Head.Rows[0]["Equipo"].S() + "<br />" + Head.Rows[0]["Paquete"].S() + "</font></td>" +
             "</tr>" +
             "<tr><td colspan='2'></td><td colspan='9'style='text-align: center;  font-size: x-large; vertical-align: middle' ><font color='#ffffff'>Aerolíneas Ejecutivas S.A. de C.V.</font></td><td colspan='2'><font color='#ffffff'>Costo Directo Nacional:<br /> Costo Directo Internacional:</font></td><td style='text-align: right;'><font color='#ffffff'>" + Convert.ToDecimal(Head.Rows[0]["CstDirNac"].S()).ToString("N2") + "<br /> " + Convert.ToDecimal(Head.Rows[0]["CstDirInt"].S()).ToString("N2") + "</font></td><td colspan='2'></td></tr>" +
             "<tr><td colspan='2'></td><td style='text-align: center;  vertical-align: middle' colspan='8'>&nbsp;&nbsp;</td><td></td><td colspan='2'><font color='#ffffff'>Horas Voladas:</font></td><td coslpan'3'><font color='#ffffff'>" + HrsVol + "</font></td></tr></table>" +

            "<br /><table>" +
            "<tr><td style=' text-align: center; vertical-align: middle' colspan='2'></td><td style=' text-align: center; font-size: x-large; vertical-align: middle' colspan='9'></td><td colspan='5'><img src='http://" + _ippuertoserver.S() + "/img/MexJet2.PNG' /></td></tr>" +
            "<tr><td style=' text-align: center; vertical-align: middle' colspan='2'></td><td style=' text-align: center; font-size: x-large; vertical-align: middle' colspan='9'></td><td colspan='5'></td></tr>" +
            "<tr><td style=' text-align: center; vertical-align: middle' colspan='2'></td><td style=' text-align: center; font-size: x-large; vertical-align: middle' colspan='9'><font color='#8B939B'>ESTADO DE CUENTA</font></td><td colspan='4'></td><td style=' text-align: right; vertical-align: middle' colspan='2'></td></tr>" +
            "<tr><td colspan='2'></td><td colspan='9'style='text-align: center;  font-size: x-large; vertical-align: middle' ><font color='#8B939B'>" + Razonsocial.S() + "</font></td><td colspan='4'></td></tr>" +

            "</table>");


            HttpContext.Current.Response.Write(cadena.S());
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();

        }
        protected void CreaHtmlResumenAcumulado()
        {
            try
            {
                Resumen.InnerHtml = string.Empty;

                DataTable Acumulado = (DataTable)ViewState["GetEstadoCuentaResumenAcumuladoDLLS"];

                string cadena = @"<table border='0' bgColor='#ffffff' borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; background:white; width:100%;'>" +
                "<tr><td colspan='3'></td><td bgcolor='#0D2240' colspan='8' style='border-bottom : none; border-left:none; border-top:none; border-right:none; text-align: center; vertical-align: middle'><font color='#ffffff'><B>R E S U M E N&nbsp;&nbsp;&nbsp;&nbsp;A C U M U L A D O</B></font></td></tr>" +

                "<tr ><td colspan='3'></td><td bgcolor='#EEEFF0' colspan='4' style='border-bottom : none; border-left:none; border-right:none; text-align: center; vertical-align: middle'><B>D O L A R E S</B></td><td bgcolor='#EEEFF0' colspan='4' style='border-bottom : none; border-left:none; border-right:none; text-align: center; vertical-align: middle'><B>M O N E D A&nbsp;&nbsp;&nbsp;N A C I O N A L</B></td></tr>" +
                "<tr ><td colspan='3'></td><td bgcolor='#EEEFF0' style='border-left:none; border-right:none; text-align: center;'><B>CONCEPTO</B></td><td bgcolor='#EEEFF0' style='border-left:none; border-right:none; text-align: center;'><B>SALDO ANTERIOR</B></td><td bgcolor='#EEEFF0' style='border-left:none; border-right:none; text-align: center;'><B>EN ESTE PERIODO</B></td>" +
                "<td bgcolor='#EEEFF0' style='border-left:none;  border-right:none; text-align: center;'><B>SALDO NUEVO</B></td><td bgcolor='#EEEFF0' style='border-left:none;  border-right:none; text-align: center;'><B>CONCEPTO</B></td><td bgcolor='#EEEFF0' style='border-left:none; border-right:none; text-align: center;'><B>SALDO ANTERIOR</B></td><td bgcolor='#EEEFF0' style='border-left:none; border-right:none; text-align:center;'><B>EN ESTE PERIODO</B></td><td bgcolor='#EEEFF0' style='border-left:none;  border-right:none; text-align: center;'><B>SALDO NUEVO</B></td></tr>";
                //"<tr><td style='border-bottom : none; border-left:none;  border-top:none; border-right:none;' colspan='2'></td><td style='border-bottom : none; border-top:none; border-left:none; border-right:none;'><B>&nbsp;&nbsp;&nbsp;CARGOS</B></td><td style='border-bottom : none; border-left:none; border-top:none; border-right:none;'><B>&nbsp;&nbsp;&nbsp;ABONOS</B></td>" +
                //"<td style='border-bottom:none; border-top:none; border-left:none; border-right:none;' colspan='3'></td><td style='border-bottom:none; border-left:none; border-right:none; border-top:none;'><B>CARGOS</B></td><td style='border-bottom:none; border-left:none; border-right:none; border-top:none; text-align: center;'><B>ABONOS</B></td><td style='border-bottom:none; border-left:none; border-right:none; border-top:none; text-align: center;'></td></tr>";

                if (Acumulado != null || Acumulado.Rows.Count != 0)
                {
                    int RowMax = Acumulado.Rows.Count;

                    //DETALLE
                    for (int i = 0; i < Acumulado.Rows.Count - 3; i++)
                    {
                        cadena = cadena + "<tr><td colspan='3'></td>";

                        for (int c = 0; c < Acumulado.Columns.Count; c++)
                        {
                            if (c == 0 || c == 4)
                                cadena = cadena + "<td style='border-width: 0px'>" + Acumulado.Rows[i][c].S() + "</td>";
                            else
                                cadena = cadena + "<td style='border-width: 0px; text-align: right;'>" + Acumulado.Rows[i][c].D().ToString("N2") + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>";
                        }
                        cadena = cadena + "</tr>";
                    }

                    ////SUMA
                    cadena = cadena + "<tr><td colspan='3'></td>";
                    for (int c = 0; c < Acumulado.Columns.Count; c++)
                    {
                        if (c == 0 || c == 4)
                            cadena = cadena + "<td style='border-bottom:none; border-left:none; border-right:none;'>" + Acumulado.Rows[7][c].S() + "</td>";
                        else
                            cadena = cadena + "<td style='border-bottom:none; border-left:none; border-right:none; text-align: right;'>" + Acumulado.Rows[7][c].D().ToString("N2") + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>";
                    }
                    cadena = cadena + "</tr>";

                    ////PAGOS
                    cadena = cadena + "<tr><td colspan='3'></td>";
                    for (int c = 0; c < Acumulado.Columns.Count; c++)
                    {
                        if (c == 0 || c == 4)
                            cadena = cadena + "<td style='border-left:none; border-top:none; border-right:none;'>" + Acumulado.Rows[8][c].S() + "</td>";
                        else
                            cadena = cadena + "<td style='border-left:none; border-top:none; border-right:none; text-align: right;'>" + Acumulado.Rows[8][c].D().ToString("N2") + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>";
                    }
                    cadena = cadena + "</tr>";

                    ////SALDO POR PAGAR
                    cadena = cadena + "<tr><td colspan='3'></td>";
                    for (int c = 0; c < Acumulado.Columns.Count; c++)
                    {
                        if (c == 0 || c == 4)
                            cadena = cadena + "<td style='border-left:none; border-top:none; border-right:none;'><B>" + Acumulado.Rows[9][c].S() + "</B></td>";
                        else
                            cadena = cadena + "<td style='border-left:none; border-top:none; border-right:none; text-align: right;'><B>" + Acumulado.Rows[9][c].D().ToString("N2") + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</B></td>";
                    }
                    cadena = cadena + "</tr>";
                }

                cadena = cadena + "</table>";

                ResumenAcumulado.InnerHtml = cadena;
            }
            catch (Exception x) { throw x; }
        }
        private DataTable ArmaTablaResumenNoAcumulado(DataTable dtAnt, DataTable dt, ReporteEdoCuent oR)
        {
            try
            {
                foreach (DataColumn col in dt.Columns)
                    col.ReadOnly = false;

                for (int i = 0; i < 7; i++)
                {
                    DataRow row = dt.Rows[i];

                    decimal dSaldoNuevoP = 0;
                    decimal dSaldoNuevoD = 0;

                    switch (i)
                    {
                        case 0:
                            decimal dAnticipoInicialP = 0;
                            decimal dAnticipoInicialD = 0;

                            // Anticipo Inicial Dolares
                            dAnticipoInicialD = oR.dAnticipoInicialSD;
                            // Saldo Inicial  = (Saldo Inicial + Saldos Anteriores) - Pagos Anteriores
                            dAnticipoInicialD = (dAnticipoInicialD + oR.dAnticipoInicialSPD) - oR.dAnticipoInicialPD;
                            // Saldo Inicial  = (Saldo Inicial  +- Gastos Internos Anteriores)
                            dAnticipoInicialD = (dAnticipoInicialD + oR.dAnticipoInicialGIAD);


                            // Anticipo Inicial Pesos
                            dAnticipoInicialP = oR.dAnticipoInicialSP;
                            // Saldo Inicial  = (Saldo Inicial + Saldos Anteriores) - Pagos Anteriores
                            dAnticipoInicialP = (dAnticipoInicialP + oR.dAnticipoInicialSPP) - oR.dAnticipoInicialPP;
                            // Saldo Inicial  = (Saldo Inicial  +- Gastos Internos Anteriores)
                            dAnticipoInicialP = (dAnticipoInicialP + oR.dAnticipoInicialGIAP);

                            row["SaldoAnteriorU"] = dAnticipoInicialD;
                            row["SaldoAnteriorP"] = dAnticipoInicialP;

                            // Saldo Nuevo = Saldo Anterior +  Cargos - Abonos
                            dSaldoNuevoD = (dAnticipoInicialD + row["CargosU"].S().D()) - row["AbonosU"].S().D();
                            dSaldoNuevoP = (dAnticipoInicialP + row["CargosP"].S().D()) - row["AbonosP"].S().D();

                            row["SaldoNuevoU"] = dSaldoNuevoD;
                            row["SaldoNuevoP"] = dSaldoNuevoP;

                            break;
                        case 1:
                            decimal dQuintoAnioP = 0;
                            decimal dQuintoAnioD = 0;

                            // Anticipo Inicial Dolares
                            dQuintoAnioD = oR.dQuintoAnioSD;
                            // Saldo Inicial  = (Saldo Inicial + Saldos Anteriores) - Pagos Anteriores
                            dQuintoAnioD = (dQuintoAnioD + oR.dQuintoAnioSPD) - oR.dQuintoAnioPD;
                            // Saldo Inicial  = (Saldo Inicial  +- Gastos Internos Anteriores)
                            dQuintoAnioD = (dQuintoAnioD + oR.dQuintoAnioGIAD);


                            // Anticipo Inicial Pesos
                            dQuintoAnioP = oR.dQuintoAnioSP;
                            // Saldo Inicial  = (Saldo Inicial + Saldos Anteriores) - Pagos Anteriores
                            dQuintoAnioP = (dQuintoAnioP + oR.dQuintoAnioSPP) - oR.dQuintoAnioPP;
                            // Saldo Inicial  = (Saldo Inicial  +- Gastos Internos Anteriores)
                            dQuintoAnioP = (dQuintoAnioP + oR.dQuintoAnioGIAP);

                            row["SaldoAnteriorU"] = dQuintoAnioD;
                            row["SaldoAnteriorP"] = dQuintoAnioP;

                            // Saldo Nuevo = Saldo Anterior +  Cargos - Abonos
                            dSaldoNuevoD = (dQuintoAnioD + row["CargosU"].S().D()) - row["AbonosU"].S().D();
                            dSaldoNuevoP = (dQuintoAnioP + row["CargosP"].S().D()) - row["AbonosP"].S().D();

                            row["SaldoNuevoU"] = dSaldoNuevoD;
                            row["SaldoNuevoP"] = dSaldoNuevoP;

                            break;
                        case 2:
                            decimal dFijoAnualP = 0;
                            decimal dFijoAnualD = 0;

                            // Anticipo Inicial Dolares
                            dFijoAnualD = oR.dFijoAnualSD;
                            // Saldo Inicial  = (Saldo Inicial + Saldos Anteriores) - Pagos Anteriores
                            dFijoAnualD = (dFijoAnualD + oR.dFijoAnualSPD) - oR.dFijoAnualPD;
                            // Saldo Inicial  = (Saldo Inicial  +- Gastos Internos Anteriores)
                            dFijoAnualD = (dFijoAnualD + oR.dFijoAnualGIAD);


                            // Anticipo Inicial Pesos
                            dFijoAnualP = oR.dFijoAnualSP;
                            // Saldo Inicial  = (Saldo Inicial + Saldos Anteriores) - Pagos Anteriores
                            dFijoAnualP = (dFijoAnualP + oR.dFijoAnualSPP) - oR.dFijoAnualPP;
                            // Saldo Inicial  = (Saldo Inicial  +- Gastos Internos Anteriores)
                            dFijoAnualP = (dFijoAnualP + oR.dFijoAnualGIAP);

                            row["SaldoAnteriorU"] = dFijoAnualD;
                            row["SaldoAnteriorP"] = dFijoAnualP;

                            // Saldo Nuevo = Saldo Anterior +  Cargos - Abonos
                            dSaldoNuevoD = (dFijoAnualD + row["CargosU"].S().D()) - row["AbonosU"].S().D();
                            dSaldoNuevoP = (dFijoAnualP + row["CargosP"].S().D()) - row["AbonosP"].S().D();

                            row["SaldoNuevoU"] = dSaldoNuevoD;
                            row["SaldoNuevoP"] = dSaldoNuevoP;

                            break;
                        case 3:
                            decimal dVuelosP = 0;
                            decimal dVuelosD = 0;

                            // Anticipo Inicial Dolares
                            dVuelosD = oR.dVuelosSD;
                            // Saldo Inicial  = (Saldo Inicial + Saldos Anteriores) - Pagos Anteriores
                            dVuelosD = (dVuelosD + oR.dVuelosSPD) - oR.dVuelosPD;
                            // Saldo Inicial  = (Saldo Inicial  +- Gastos Internos Anteriores)
                            dVuelosD = (dVuelosD + oR.dVuelosGIAD);


                            // Anticipo Inicial Pesos
                            dVuelosP = oR.dVuelosSP;
                            // Saldo Inicial  = (Saldo Inicial + Saldos Anteriores) - Pagos Anteriores
                            dVuelosP = (dVuelosP + oR.dVuelosSPP) - oR.dVuelosPP;
                            // Saldo Inicial  = (Saldo Inicial  +- Gastos Internos Anteriores)
                            dVuelosP = (dVuelosP + oR.dVuelosGIAP);

                            row["SaldoAnteriorU"] = dVuelosD;
                            row["SaldoAnteriorP"] = dVuelosP;

                            // Saldo Nuevo = Saldo Anterior +  Cargos - Abonos
                            dSaldoNuevoD = (dVuelosD + row["CargosU"].S().D()) - row["AbonosU"].S().D();
                            dSaldoNuevoP = (dVuelosP + row["CargosP"].S().D()) - row["AbonosP"].S().D();

                            row["SaldoNuevoU"] = dSaldoNuevoD;
                            row["SaldoNuevoP"] = dSaldoNuevoP;

                            break;
                        case 4:
                            decimal dGastosP = 0;
                            decimal dGastosD = 0;

                            // Anticipo Inicial Dolares
                            dGastosD = oR.dGastosSD;
                            // Saldo Inicial  = (Saldo Inicial + Saldos Anteriores) - Pagos Anteriores
                            dGastosD = (dGastosD + oR.dGastosSPD) - oR.dGastosPD;
                            // Saldo Inicial  = (Saldo Inicial  +- Gastos Internos Anteriores)
                            dGastosD = (dGastosD + oR.dGastosGIAD);


                            // Anticipo Inicial Pesos
                            dGastosP = oR.dGastosSP;
                            // Saldo Inicial  = (Saldo Inicial + Saldos Anteriores) - Pagos Anteriores
                            dGastosP = (dGastosP + oR.dGastosSPP) - oR.dGastosPP;
                            // Saldo Inicial  = (Saldo Inicial  +- Gastos Internos Anteriores)
                            dGastosP = (dGastosP + oR.dGastosGIAP);

                            row["SaldoAnteriorU"] = dGastosD;
                            row["SaldoAnteriorP"] = dGastosP;

                            // Saldo Nuevo = Saldo Anterior +  Cargos - Abonos
                            dSaldoNuevoD = (dGastosD + row["CargosU"].S().D()) - row["AbonosU"].S().D();
                            dSaldoNuevoP = (dGastosP + row["CargosP"].S().D()) - row["AbonosP"].S().D();

                            row["SaldoNuevoU"] = dSaldoNuevoD;
                            row["SaldoNuevoP"] = dSaldoNuevoP;

                            break;
                        case 5:
                            decimal dServiciosConCargoP = 0;
                            decimal dServiciosConCargoD = 0;

                            // Anticipo Inicial Dolares
                            dServiciosConCargoD = oR.dServiciosConCargoSD;
                            // Saldo Inicial  = (Saldo Inicial + Saldos Anteriores) - Pagos Anteriores
                            dServiciosConCargoD = (dServiciosConCargoD + oR.dServiciosConCargoSPD) - oR.dServiciosConCargoPD;
                            // Saldo Inicial  = (Saldo Inicial  +- Gastos Internos Anteriores)
                            dServiciosConCargoD = (dServiciosConCargoD + oR.dServiciosConCargoGIAD);


                            // Anticipo Inicial Pesos
                            dServiciosConCargoP = oR.dServiciosConCargoSP;
                            // Saldo Inicial  = (Saldo Inicial + Saldos Anteriores) - Pagos Anteriores
                            dServiciosConCargoP = (dServiciosConCargoP + oR.dServiciosConCargoSPP) - oR.dServiciosConCargoPP;
                            // Saldo Inicial  = (Saldo Inicial  +- Gastos Internos Anteriores)
                            dServiciosConCargoP = (dServiciosConCargoP + oR.dServiciosConCargoGIAP);

                            row["SaldoAnteriorU"] = dServiciosConCargoD;
                            row["SaldoAnteriorP"] = dServiciosConCargoP;

                            // Saldo Nuevo = Saldo Anterior +  Cargos - Abonos
                            dSaldoNuevoD = (dServiciosConCargoD + row["CargosU"].S().D()) - row["AbonosU"].S().D();
                            dSaldoNuevoP = (dServiciosConCargoP + row["CargosP"].S().D()) - row["AbonosP"].S().D();

                            row["SaldoNuevoU"] = dSaldoNuevoD;
                            row["SaldoNuevoP"] = dSaldoNuevoP;

                            break;
                        case 6:
                            decimal dIvaP = 0;
                            decimal dIvaD = 0;

                            // Anticipo Inicial Dolares
                            dIvaD = oR.dIvaSD;
                            // Saldo Inicial  = (Saldo Inicial + Saldos Anteriores) - Pagos Anteriores
                            dIvaD = (dIvaD + oR.dIvaSPD) - oR.dIvaPD;
                            // Saldo Inicial  = (Saldo Inicial  +- Gastos Internos Anteriores)
                            dIvaD = (dIvaD + oR.dIvaGIAD);


                            // Anticipo Inicial Pesos
                            dIvaP = oR.dIvaSP;
                            // Saldo Inicial  = (Saldo Inicial + Saldos Anteriores) - Pagos Anteriores
                            dIvaP = (dIvaP + oR.dIvaSPP) - oR.dIvaPP;
                            // Saldo Inicial  = (Saldo Inicial  +- Gastos Internos Anteriores)
                            dIvaP = (dIvaP + oR.dIvaGIAP);

                            row["SaldoAnteriorU"] = dIvaD;
                            row["SaldoAnteriorP"] = dIvaP;

                            // Saldo Nuevo = Saldo Anterior +  Cargos - Abonos
                            dSaldoNuevoD = (dIvaD + row["CargosU"].S().D()) - row["AbonosU"].S().D();
                            dSaldoNuevoP = (dIvaP + row["CargosP"].S().D()) - row["AbonosP"].S().D();

                            row["SaldoNuevoU"] = dSaldoNuevoD;
                            row["SaldoNuevoP"] = dSaldoNuevoP;

                            break;
                    }

                }


                if (dtAnt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        foreach (DataRow row2 in dtAnt.Rows)
                        {
                            if (row.S("ConceptoU") == row2.S("ConceptoU"))
                            {
                                row["SaldoAnteriorU"] = row2["SaldoNuevoU"];
                                row["SaldoAnteriorP"] = row2["SaldoNuevoP"];
                            }
                        }
                    }
                }

                DataRow dr = dt.Rows[7];
                dr["ConceptoU"] = "SUMA";
                dr["SaldoAnteriorU"] = ObtieneSumaTabla(dt, "SaldoAnteriorU");
                dr["CargosU"] = dt.Rows[7]["CargosU"].S();
                dr["AbonosU"] = dt.Rows[7]["AbonosU"].S();
                dr["SaldoNuevoU"] = ObtieneSumaTabla(dt, "SaldoNuevoU");

                dr["ConceptoP"] = "SUMA";
                dr["SaldoAnteriorP"] = ObtieneSumaTabla(dt, "SaldoAnteriorP");
                dr["CargosP"] = dt.Rows[7]["CargosP"].S();
                dr["AbonosP"] = dt.Rows[7]["AbonosP"].S();
                dr["SaldoNuevoP"] = ObtieneSumaTabla(dt, "SaldoNuevoP");

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private DataTable ArmaTablaResumenNoAcumuladoPos(DataTable dtAnt, DataTable dt, ReporteEdoCuent oR)
        {
            try
            {
                foreach (DataColumn col in dt.Columns)
                    col.ReadOnly = false;


                if (dtAnt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        foreach (DataRow row2 in dtAnt.Rows)
                        {
                            if (row.S("ConceptoU") == row2.S("ConceptoU"))
                            {
                                row["SaldoAnteriorU"] = row2["SaldoNuevoU"];
                                row["SaldoAnteriorP"] = row2["SaldoNuevoP"];
                            }
                        }
                    }
                }


                for (int i = 0; i < 7; i++)
                {
                    DataRow row = dt.Rows[i];

                    decimal dSaldoNuevoP = 0;
                    decimal dSaldoNuevoD = 0;

                    switch (i)
                    {
                        case 0:
                            decimal dAnticipoInicialP = 0;
                            decimal dAnticipoInicialD = 0;

                            //// Anticipo Inicial Dolares
                            //dAnticipoInicialD = oR.dAnticipoInicialSD;
                            //// Saldo Inicial  = (Saldo Inicial + Saldos Anteriores) - Pagos Anteriores
                            //dAnticipoInicialD = (dAnticipoInicialD + oR.dAnticipoInicialSPD) - oR.dAnticipoInicialPD;
                            //// Saldo Inicial  = (Saldo Inicial  +- Gastos Internos Anteriores)
                            //dAnticipoInicialD = (dAnticipoInicialD + oR.dAnticipoInicialGIAD);


                            //// Anticipo Inicial Pesos
                            //dAnticipoInicialP = oR.dAnticipoInicialSP;
                            //// Saldo Inicial  = (Saldo Inicial + Saldos Anteriores) - Pagos Anteriores
                            //dAnticipoInicialP = (dAnticipoInicialP + oR.dAnticipoInicialSPP) - oR.dAnticipoInicialPP;
                            //// Saldo Inicial  = (Saldo Inicial  +- Gastos Internos Anteriores)
                            //dAnticipoInicialP = (dAnticipoInicialP + oR.dAnticipoInicialGIAP);

                            //row["SaldoAnteriorU"] = dAnticipoInicialD;
                            //row["SaldoAnteriorP"] = dAnticipoInicialP;

                            // Saldo Nuevo = Saldo Anterior +  Cargos - Abonos
                            dSaldoNuevoD = (row["SaldoAnteriorU"].S().D() + row["CargosU"].S().D()) - row["AbonosU"].S().D();
                            dSaldoNuevoP = (row["SaldoAnteriorP"].S().D() + row["CargosP"].S().D()) - row["AbonosP"].S().D();

                            row["SaldoNuevoU"] = dSaldoNuevoD;
                            row["SaldoNuevoP"] = dSaldoNuevoP;

                            break;
                        case 1:
                            decimal dQuintoAnioP = 0;
                            decimal dQuintoAnioD = 0;

                            //// Anticipo Inicial Dolares
                            //dQuintoAnioD = oR.dQuintoAnioSD;
                            //// Saldo Inicial  = (Saldo Inicial + Saldos Anteriores) - Pagos Anteriores
                            //dQuintoAnioD = (dQuintoAnioD + oR.dQuintoAnioSPD) - oR.dQuintoAnioPD;
                            //// Saldo Inicial  = (Saldo Inicial  +- Gastos Internos Anteriores)
                            //dQuintoAnioD = (dQuintoAnioD + oR.dQuintoAnioGIAD);


                            //// Anticipo Inicial Pesos
                            //dQuintoAnioP = oR.dQuintoAnioSP;
                            //// Saldo Inicial  = (Saldo Inicial + Saldos Anteriores) - Pagos Anteriores
                            //dQuintoAnioP = (dQuintoAnioP + oR.dQuintoAnioSPP) - oR.dQuintoAnioPP;
                            //// Saldo Inicial  = (Saldo Inicial  +- Gastos Internos Anteriores)
                            //dQuintoAnioP = (dQuintoAnioP + oR.dQuintoAnioGIAP);

                            row["SaldoAnteriorU"] = dQuintoAnioD;
                            row["SaldoAnteriorP"] = dQuintoAnioP;

                            // Saldo Nuevo = Saldo Anterior +  Cargos - Abonos
                            dSaldoNuevoD = (row["SaldoAnteriorU"].S().D() + row["CargosU"].S().D()) - row["AbonosU"].S().D();
                            dSaldoNuevoP = (row["SaldoAnteriorP"].S().D() + row["CargosP"].S().D()) - row["AbonosP"].S().D();

                            row["SaldoNuevoU"] = dSaldoNuevoD;
                            row["SaldoNuevoP"] = dSaldoNuevoP;

                            break;
                        case 2:
                            decimal dFijoAnualP = 0;
                            decimal dFijoAnualD = 0;

                            //// Anticipo Inicial Dolares
                            //dFijoAnualD = oR.dFijoAnualSD;
                            //// Saldo Inicial  = (Saldo Inicial + Saldos Anteriores) - Pagos Anteriores
                            //dFijoAnualD = (dFijoAnualD + oR.dFijoAnualSPD) - oR.dFijoAnualPD;
                            //// Saldo Inicial  = (Saldo Inicial  +- Gastos Internos Anteriores)
                            //dFijoAnualD = (dFijoAnualD + oR.dFijoAnualGIAD);


                            //// Anticipo Inicial Pesos
                            //dFijoAnualP = oR.dFijoAnualSP;
                            //// Saldo Inicial  = (Saldo Inicial + Saldos Anteriores) - Pagos Anteriores
                            //dFijoAnualP = (dFijoAnualP + oR.dFijoAnualSPP) - oR.dFijoAnualPP;
                            //// Saldo Inicial  = (Saldo Inicial  +- Gastos Internos Anteriores)
                            //dFijoAnualP = (dFijoAnualP + oR.dFijoAnualGIAP);

                            //row["SaldoAnteriorU"] = dFijoAnualD;
                            //row["SaldoAnteriorP"] = dFijoAnualP;

                            // Saldo Nuevo = Saldo Anterior +  Cargos - Abonos
                            dSaldoNuevoD = (row["SaldoAnteriorU"].S().D() + row["CargosU"].S().D()) - row["AbonosU"].S().D();
                            dSaldoNuevoP = (row["SaldoAnteriorP"].S().D() + row["CargosP"].S().D()) - row["AbonosP"].S().D();

                            row["SaldoNuevoU"] = dSaldoNuevoD;
                            row["SaldoNuevoP"] = dSaldoNuevoP;

                            break;
                        case 3:
                            decimal dVuelosP = 0;
                            decimal dVuelosD = 0;

                            //// Anticipo Inicial Dolares
                            //dVuelosD = oR.dVuelosSD;
                            //// Saldo Inicial  = (Saldo Inicial + Saldos Anteriores) - Pagos Anteriores
                            //dVuelosD = (dVuelosD + oR.dVuelosSPD) - oR.dVuelosPD;
                            //// Saldo Inicial  = (Saldo Inicial  +- Gastos Internos Anteriores)
                            //dVuelosD = (dVuelosD + oR.dVuelosGIAD);


                            //// Anticipo Inicial Pesos
                            //dVuelosP = oR.dVuelosSP;
                            //// Saldo Inicial  = (Saldo Inicial + Saldos Anteriores) - Pagos Anteriores
                            //dVuelosP = (dVuelosP + oR.dVuelosSPP) - oR.dVuelosPP;
                            //// Saldo Inicial  = (Saldo Inicial  +- Gastos Internos Anteriores)
                            //dVuelosP = (dVuelosP + oR.dVuelosGIAP);

                            //row["SaldoAnteriorU"] = dVuelosD;
                            //row["SaldoAnteriorP"] = dVuelosP;

                            // Saldo Nuevo = Saldo Anterior +  Cargos - Abonos
                            dSaldoNuevoD = (row["SaldoAnteriorU"].S().D() + row["CargosU"].S().D()) - row["AbonosU"].S().D();
                            dSaldoNuevoP = (row["SaldoAnteriorP"].S().D() + row["CargosP"].S().D()) - row["AbonosP"].S().D();

                            row["SaldoNuevoU"] = dSaldoNuevoD;
                            row["SaldoNuevoP"] = dSaldoNuevoP;

                            break;
                        case 4:
                            decimal dGastosP = 0;
                            decimal dGastosD = 0;

                            //// Anticipo Inicial Dolares
                            //dGastosD = oR.dGastosSD;
                            //// Saldo Inicial  = (Saldo Inicial + Saldos Anteriores) - Pagos Anteriores
                            //dGastosD = (dGastosD + oR.dGastosSPD) - oR.dGastosPD;
                            //// Saldo Inicial  = (Saldo Inicial  +- Gastos Internos Anteriores)
                            //dGastosD = (dGastosD + oR.dGastosGIAD);


                            //// Anticipo Inicial Pesos
                            //dGastosP = oR.dGastosSP;
                            //// Saldo Inicial  = (Saldo Inicial + Saldos Anteriores) - Pagos Anteriores
                            //dGastosP = (dGastosP + oR.dGastosSPP) - oR.dGastosPP;
                            //// Saldo Inicial  = (Saldo Inicial  +- Gastos Internos Anteriores)
                            //dGastosP = (dGastosP + oR.dGastosGIAP);

                            //row["SaldoAnteriorU"] = dGastosD;
                            //row["SaldoAnteriorP"] = dGastosP;

                            // Saldo Nuevo = Saldo Anterior +  Cargos - Abonos
                            dSaldoNuevoD = (row["SaldoAnteriorU"].S().D() + row["CargosU"].S().D()) - row["AbonosU"].S().D();
                            dSaldoNuevoP = (row["SaldoAnteriorP"].S().D() + row["CargosP"].S().D()) - row["AbonosP"].S().D();

                            row["SaldoNuevoU"] = dSaldoNuevoD;
                            row["SaldoNuevoP"] = dSaldoNuevoP;

                            break;
                        case 5:
                            decimal dServiciosConCargoP = 0;
                            decimal dServiciosConCargoD = 0;

                            //// Anticipo Inicial Dolares
                            //dServiciosConCargoD = oR.dServiciosConCargoSD;
                            //// Saldo Inicial  = (Saldo Inicial + Saldos Anteriores) - Pagos Anteriores
                            //dServiciosConCargoD = (dServiciosConCargoD + oR.dServiciosConCargoSPD) - oR.dServiciosConCargoPD;
                            //// Saldo Inicial  = (Saldo Inicial  +- Gastos Internos Anteriores)
                            //dServiciosConCargoD = (dServiciosConCargoD + oR.dServiciosConCargoGIAD);


                            //// Anticipo Inicial Pesos
                            //dServiciosConCargoP = oR.dServiciosConCargoSP;
                            //// Saldo Inicial  = (Saldo Inicial + Saldos Anteriores) - Pagos Anteriores
                            //dServiciosConCargoP = (dServiciosConCargoP + oR.dServiciosConCargoSPP) - oR.dServiciosConCargoPP;
                            //// Saldo Inicial  = (Saldo Inicial  +- Gastos Internos Anteriores)
                            //dServiciosConCargoP = (dServiciosConCargoP + oR.dServiciosConCargoGIAP);

                            //row["SaldoAnteriorU"] = dServiciosConCargoD;
                            //row["SaldoAnteriorP"] = dServiciosConCargoP;

                            // Saldo Nuevo = Saldo Anterior +  Cargos - Abonos
                            dSaldoNuevoD = (row["SaldoAnteriorU"].S().D() + row["CargosU"].S().D()) - row["AbonosU"].S().D();
                            dSaldoNuevoP = (row["SaldoAnteriorP"].S().D() + row["CargosP"].S().D()) - row["AbonosP"].S().D();

                            row["SaldoNuevoU"] = dSaldoNuevoD;
                            row["SaldoNuevoP"] = dSaldoNuevoP;

                            break;
                        case 6:
                            decimal dIvaP = 0;
                            decimal dIvaD = 0;

                            //// Anticipo Inicial Dolares
                            //dIvaD = oR.dIvaSD;
                            //// Saldo Inicial  = (Saldo Inicial + Saldos Anteriores) - Pagos Anteriores
                            //dIvaD = (dIvaD + oR.dIvaSPD) - oR.dIvaPD;
                            //// Saldo Inicial  = (Saldo Inicial  +- Gastos Internos Anteriores)
                            //dIvaD = (dIvaD + oR.dIvaGIAD);


                            //// Anticipo Inicial Pesos
                            //dIvaP = oR.dIvaSP;
                            //// Saldo Inicial  = (Saldo Inicial + Saldos Anteriores) - Pagos Anteriores
                            //dIvaP = (dIvaP + oR.dIvaSPP) - oR.dIvaPP;
                            //// Saldo Inicial  = (Saldo Inicial  +- Gastos Internos Anteriores)
                            //dIvaP = (dIvaP + oR.dIvaGIAP);

                            //row["SaldoAnteriorU"] = dIvaD;
                            //row["SaldoAnteriorP"] = dIvaP;

                            // Saldo Nuevo = Saldo Anterior +  Cargos - Abonos
                            dSaldoNuevoD = (row["SaldoAnteriorU"].S().D() + row["CargosU"].S().D()) - row["AbonosU"].S().D();
                            dSaldoNuevoP = (row["SaldoAnteriorP"].S().D() + row["CargosP"].S().D()) - row["AbonosP"].S().D();

                            row["SaldoNuevoU"] = dSaldoNuevoD;
                            row["SaldoNuevoP"] = dSaldoNuevoP;

                            break;
                    }

                }


                DataRow dr = dt.Rows[7];
                dr["ConceptoU"] = "SUMA";
                dr["SaldoAnteriorU"] = ObtieneSumaTabla(dt, "SaldoAnteriorU");
                dr["CargosU"] = dt.Rows[7]["CargosU"].S();
                dr["AbonosU"] = dt.Rows[7]["AbonosU"].S();
                dr["SaldoNuevoU"] = ObtieneSumaTabla(dt, "SaldoNuevoU");

                dr["ConceptoP"] = "SUMA";
                dr["SaldoAnteriorP"] = ObtieneSumaTabla(dt, "SaldoAnteriorP");
                dr["CargosP"] = dt.Rows[7]["CargosP"].S();
                dr["AbonosP"] = dt.Rows[7]["AbonosP"].S();
                dr["SaldoNuevoP"] = ObtieneSumaTabla(dt, "SaldoNuevoP");

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private decimal ObtieneSumaTabla(DataTable dt, string sColumna)
        {
            try
            {
                decimal dSuma = 0;

                foreach (DataRow row in dt.Rows)
                {
                    dSuma += row.S(sColumna).D();
                }

                return dSuma;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private DataTable ArmaTablaResumenAcumulado(DataTable dtAnt, DataTable dt, ReporteEdoCuent oR)
        {
            try
            {
                DataTable dtFinal = new DataTable();
                dtFinal.Columns.Add("ConceptoU");
                dtFinal.Columns.Add("SaldoAnteriorU", typeof(decimal));
                dtFinal.Columns.Add("EnEstePeriodoU", typeof(decimal));
                dtFinal.Columns.Add("SaldoNuevoU", typeof(decimal));

                dtFinal.Columns.Add("ConceptoP");
                dtFinal.Columns.Add("SaldoAnteriorP", typeof(decimal));
                dtFinal.Columns.Add("EnEstePeriodoP", typeof(decimal));
                dtFinal.Columns.Add("SaldoNuevoP", typeof(decimal));

                
                for (int i = 0; i < 7; i++)
                {
                    DataRow dr = dtFinal.NewRow();
                    DataRow row = dt.Rows[i];

                    decimal dSaldoNuevoP = 0;
                    decimal dSaldoNuevoD = 0;

                    dr["ConceptoU"] = row.S("ConceptoU");
                    dr["ConceptoP"] = row.S("ConceptoP");
                    dr["EnEstePeriodoU"] = row["CargosU"];
                    dr["EnEstePeriodoP"] = row["CargosP"];

                    switch (row.S("ConceptoU"))
                    {
                        case "ANTICIPO INICIAL":
                            decimal dAnticipoInicialP = 0;
                            decimal dAnticipoInicialD = 0;

                            // Anticipo Inicial Dolares
                            dAnticipoInicialD = oR.dAnticipoInicialSD;
                            // Saldo Inicial  = (Saldo Inicial + Saldos Anteriores)
                            dAnticipoInicialD = (dAnticipoInicialD + oR.dAnticipoInicialSPD);
                            // Saldo Inicial  = (Saldo Inicial  +- Gastos Internos Anteriores)
                            dAnticipoInicialD = (dAnticipoInicialD + oR.dAnticipoInicialGIAD);

                            // Anticipo Inicial Pesos
                            dAnticipoInicialP = oR.dAnticipoInicialSP;
                            // Saldo Inicial  = (Saldo Inicial + Saldos Anteriores)
                            dAnticipoInicialP = (dAnticipoInicialP + oR.dAnticipoInicialSPP);
                            // Saldo Inicial  = (Saldo Inicial  +- Gastos Internos Anteriores)
                            dAnticipoInicialP = (dAnticipoInicialP + oR.dAnticipoInicialGIAP);

                            dr["SaldoAnteriorU"] = dAnticipoInicialD;
                            dr["SaldoAnteriorP"] = dAnticipoInicialP;

                            // Saldo Nuevo = Saldo Anterior +  Cargos
                            dSaldoNuevoD = (dAnticipoInicialD + row["CargosU"].S().D());
                            dSaldoNuevoP = (dAnticipoInicialP + row["CargosP"].S().D());

                            dr["SaldoNuevoU"] = dSaldoNuevoD;
                            dr["SaldoNuevoP"] = dSaldoNuevoP;

                            break;
                        case "QUINTO AÑO":
                            decimal dQuintoAnioP = 0;
                            decimal dQuintoAnioD = 0;

                            // Anticipo Inicial Dolares
                            dQuintoAnioD = oR.dQuintoAnioSD;
                            // Saldo Inicial  = (Saldo Inicial + Saldos Anteriores)
                            dQuintoAnioD = (dQuintoAnioD + oR.dQuintoAnioSPD);
                            // Saldo Inicial  = (Saldo Inicial  +- Gastos Internos Anteriores)
                            dQuintoAnioD = (dQuintoAnioD + oR.dQuintoAnioGIAD);


                            // Anticipo Inicial Pesos
                            dQuintoAnioP = oR.dQuintoAnioSP;
                            // Saldo Inicial  = (Saldo Inicial + Saldos Anteriores)
                            dQuintoAnioP = (dQuintoAnioP + oR.dQuintoAnioSPP);
                            // Saldo Inicial  = (Saldo Inicial  +- Gastos Internos Anteriores)
                            dQuintoAnioP = (dQuintoAnioP + oR.dQuintoAnioGIAP);

                            dr["SaldoAnteriorU"] = dQuintoAnioD;
                            dr["SaldoAnteriorP"] = dQuintoAnioP;

                            // Saldo Nuevo = Saldo Anterior +  Cargos - Abonos
                            dSaldoNuevoD = (dQuintoAnioD + row["CargosU"].S().D());
                            dSaldoNuevoP = (dQuintoAnioP + row["CargosP"].S().D());

                            dr["SaldoNuevoU"] = dSaldoNuevoD;
                            dr["SaldoNuevoP"] = dSaldoNuevoP;

                            break;
                        case "FIJO ANUAL":
                            decimal dFijoAnualP = 0;
                            decimal dFijoAnualD = 0;

                            // Anticipo Inicial Dolares
                            dFijoAnualD = oR.dFijoAnualSD;
                            // Saldo Inicial  = (Saldo Inicial + Saldos Anteriores)
                            dFijoAnualD = (dFijoAnualD + oR.dFijoAnualSPD);
                            // Saldo Inicial  = (Saldo Inicial  +- Gastos Internos Anteriores)
                            dFijoAnualD = (dFijoAnualD + oR.dFijoAnualGIAD);

                            // Anticipo Inicial Pesos
                            dFijoAnualP = oR.dFijoAnualSP;
                            // Saldo Inicial  = (Saldo Inicial + Saldos Anteriores)
                            dFijoAnualP = (dFijoAnualP + oR.dFijoAnualSPP);
                            // Saldo Inicial  = (Saldo Inicial  +- Gastos Internos Anteriores)
                            dFijoAnualP = (dFijoAnualP + oR.dFijoAnualGIAP);

                            dr["SaldoAnteriorU"] = dFijoAnualD;
                            dr["SaldoAnteriorP"] = dFijoAnualP;

                            // Saldo Nuevo = Saldo Anterior +  Cargos - Abonos
                            dSaldoNuevoD = (dFijoAnualD + row["CargosU"].S().D());
                            dSaldoNuevoP = (dFijoAnualP + row["CargosP"].S().D());

                            dr["SaldoNuevoU"] = dSaldoNuevoD;
                            dr["SaldoNuevoP"] = dSaldoNuevoP;

                            break;
                        case "VUELOS":
                            decimal dVuelosP = 0;
                            decimal dVuelosD = 0;

                            // Anticipo Inicial Dolares
                            dVuelosD = oR.dVuelosSD;
                            // Saldo Inicial  = (Saldo Inicial + Saldos Anteriores)
                            dVuelosD = (dVuelosD + oR.dVuelosSPD);
                            // Saldo Inicial  = (Saldo Inicial  +- Gastos Internos Anteriores)
                            dVuelosD = (dVuelosD + oR.dVuelosGIAD);

                            // Anticipo Inicial Pesos
                            dVuelosP = oR.dVuelosSP;
                            // Saldo Inicial  = (Saldo Inicial + Saldos Anteriores)
                            dVuelosP = (dVuelosP + oR.dVuelosSPP);
                            // Saldo Inicial  = (Saldo Inicial  +- Gastos Internos Anteriores)
                            dVuelosP = (dVuelosP + oR.dVuelosGIAP);

                            dr["SaldoAnteriorU"] = dVuelosD;
                            dr["SaldoAnteriorP"] = dVuelosP;

                            // Saldo Nuevo = Saldo Anterior +  Cargos - Abonos
                            dSaldoNuevoD = (dVuelosD + row["CargosU"].S().D());
                            dSaldoNuevoP = (dVuelosP + row["CargosP"].S().D());

                            dr["SaldoNuevoU"] = dSaldoNuevoD;
                            dr["SaldoNuevoP"] = dSaldoNuevoP;

                            break;
                        case "GASTOS":
                            decimal dGastosP = 0;
                            decimal dGastosD = 0;

                            // Anticipo Inicial Dolares
                            dGastosD = oR.dGastosSD;
                            // Saldo Inicial  = (Saldo Inicial + Saldos Anteriores)
                            dGastosD = (dGastosD + oR.dGastosSPD);
                            // Saldo Inicial  = (Saldo Inicial  +- Gastos Internos Anteriores)
                            dGastosD = (dGastosD + oR.dGastosGIAD);


                            // Anticipo Inicial Pesos
                            dGastosP = oR.dGastosSP;
                            // Saldo Inicial  = (Saldo Inicial + Saldos Anteriores)
                            dGastosP = (dGastosP + oR.dGastosSPP);
                            // Saldo Inicial  = (Saldo Inicial  +- Gastos Internos Anteriores)
                            dGastosP = (dGastosP + oR.dGastosGIAP);

                            dr["SaldoAnteriorU"] = dGastosD;
                            dr["SaldoAnteriorP"] = dGastosP;

                            // Saldo Nuevo = Saldo Anterior +  Cargos - Abonos
                            dSaldoNuevoD = (dGastosD + row["CargosU"].S().D());
                            dSaldoNuevoP = (dGastosP + row["CargosP"].S().D());

                            dr["SaldoNuevoU"] = dSaldoNuevoD;
                            dr["SaldoNuevoP"] = dSaldoNuevoP;

                            break;
                        case "SERVICIOS CON CARGO":
                            decimal dServiciosConCargoP = 0;
                            decimal dServiciosConCargoD = 0;

                            // Anticipo Inicial Dolares
                            dServiciosConCargoD = oR.dServiciosConCargoSD;
                            // Saldo Inicial  = (Saldo Inicial + Saldos Anteriores)
                            dServiciosConCargoD = (dServiciosConCargoD + oR.dServiciosConCargoSPD);
                            // Saldo Inicial  = (Saldo Inicial  +- Gastos Internos Anteriores)
                            dServiciosConCargoD = (dServiciosConCargoD + oR.dServiciosConCargoGIAD);


                            // Anticipo Inicial Pesos
                            dServiciosConCargoP = oR.dServiciosConCargoSP;
                            // Saldo Inicial  = (Saldo Inicial + Saldos Anteriores)
                            dServiciosConCargoP = (dServiciosConCargoP + oR.dServiciosConCargoSPP);
                            // Saldo Inicial  = (Saldo Inicial  +- Gastos Internos Anteriores)
                            dServiciosConCargoP = (dServiciosConCargoP + oR.dServiciosConCargoGIAP);

                            dr["SaldoAnteriorU"] = dServiciosConCargoD;
                            dr["SaldoAnteriorP"] = dServiciosConCargoP;

                            // Saldo Nuevo = Saldo Anterior +  Cargos - Abonos
                            dSaldoNuevoD = (dServiciosConCargoD + row["CargosU"].S().D());
                            dSaldoNuevoP = (dServiciosConCargoP + row["CargosP"].S().D());

                            dr["SaldoNuevoU"] = dSaldoNuevoD;
                            dr["SaldoNuevoP"] = dSaldoNuevoP;

                            break;
                        case "I.V.A.":
                            decimal dIvaP = 0;
                            decimal dIvaD = 0;

                            // Anticipo Inicial Dolares
                            dIvaD = oR.dIvaSD;
                            // Saldo Inicial  = (Saldo Inicial + Saldos Anteriores)
                            dIvaD = (dIvaD + oR.dIvaSPD);
                            // Saldo Inicial  = (Saldo Inicial  +- Gastos Internos Anteriores)
                            dIvaD = (dIvaD + oR.dIvaGIAD);


                            // Anticipo Inicial Pesos
                            dIvaP = oR.dIvaSP;
                            // Saldo Inicial  = (Saldo Inicial + Saldos Anteriores)
                            dIvaP = (dIvaP + oR.dIvaSPP);
                            // Saldo Inicial  = (Saldo Inicial  +- Gastos Internos Anteriores)
                            dIvaP = (dIvaP + oR.dIvaGIAP);

                            dr["SaldoAnteriorU"] = dIvaD;
                            dr["SaldoAnteriorP"] = dIvaP;

                            // Saldo Nuevo = Saldo Anterior +  Cargos - Abonos
                            dSaldoNuevoD = (dIvaD + row["CargosU"].S().D());
                            dSaldoNuevoP = (dIvaP + row["CargosP"].S().D());

                            dr["SaldoNuevoU"] = dSaldoNuevoD;
                            dr["SaldoNuevoP"] = dSaldoNuevoP;

                            break;
                    }

                    dtFinal.Rows.Add(dr);
                }
                
                if(dtAnt.Rows.Count > 0)
                {
                    foreach (DataRow row in dtFinal.Rows)
                    {
                        foreach(DataRow row2 in dtAnt.Rows)
                        {
                            if (row.S("ConceptoU") == row2.S("ConceptoU"))
                            {
                                row["SaldoAnteriorU"] = row2["SaldoNuevoU"];
                                row["SaldoAnteriorP"] = row2["SaldoNuevoP"];
                            }
                        }
                    }
                }

                // ---==========   SUMAS  ==========--- //
                DataRow drSumas = dtFinal.NewRow();
                drSumas["ConceptoU"] = "SUMA";
                drSumas["SaldoAnteriorU"] = ObtieneSumaTabla(dtFinal, "SaldoAnteriorU");
                drSumas["EnEstePeriodoU"] = ObtieneSumaTabla(dtFinal, "EnEstePeriodoU");
                drSumas["SaldoNuevoU"] = ObtieneSumaTabla(dtFinal, "SaldoNuevoU");

                drSumas["ConceptoP"] = "SUMA";
                drSumas["SaldoAnteriorP"] = ObtieneSumaTabla(dtFinal, "SaldoAnteriorP");
                drSumas["EnEstePeriodoP"] = ObtieneSumaTabla(dtFinal, "EnEstePeriodoP");
                drSumas["SaldoNuevoP"] = ObtieneSumaTabla(dtFinal, "SaldoNuevoP");

                // ---==========   PAGOS  ==========--- //
                DataRow drPagos = dtFinal.NewRow();
                drPagos["ConceptoU"] = "PAGOS";

                decimal dPagosSaldoAnterior = 0;
                dPagosSaldoAnterior = dPagosSaldoAnterior + oR.dAnticipoInicialPD;
                dPagosSaldoAnterior = dPagosSaldoAnterior + oR.dQuintoAnioPD;
                dPagosSaldoAnterior = dPagosSaldoAnterior + oR.dFijoAnualPD;
                dPagosSaldoAnterior = dPagosSaldoAnterior + oR.dVuelosPD;
                dPagosSaldoAnterior = dPagosSaldoAnterior + oR.dGastosPD;
                dPagosSaldoAnterior = dPagosSaldoAnterior + oR.dServiciosConCargoPD;
                dPagosSaldoAnterior = dPagosSaldoAnterior + oR.dIvaPD;


                if (dtAnt.Rows.Count > 0)
                {
                    drPagos["SaldoAnteriorU"] = dtAnt.Rows[8]["SaldoNuevoU"];
                }
                else
                    drPagos["SaldoAnteriorU"] = dPagosSaldoAnterior;

                decimal dPagosPeriodoU = 0;
                //dPagosPeriodoU = dt.Rows[7]["AbonosU"].S().D();
                dPagosPeriodoU = sPagosD.S() == "" ? "0000.0".D() : Math.Abs(sPagosD.S().D());
                sPagosD = "0000.0";

                drPagos["EnEstePeriodoU"] = dPagosPeriodoU;
                drPagos["SaldoNuevoU"] = dPagosSaldoAnterior + dPagosPeriodoU;


                drPagos["ConceptoP"] = "PAGOS";

                decimal dPagosSaldoAnteriorP = 0;
                dPagosSaldoAnteriorP = dPagosSaldoAnteriorP + oR.dAnticipoInicialPP;
                dPagosSaldoAnteriorP = dPagosSaldoAnteriorP + oR.dQuintoAnioPP;
                dPagosSaldoAnteriorP = dPagosSaldoAnteriorP + oR.dFijoAnualPP;
                dPagosSaldoAnteriorP = dPagosSaldoAnteriorP + oR.dVuelosPP;
                dPagosSaldoAnteriorP = dPagosSaldoAnteriorP + oR.dGastosPP;
                dPagosSaldoAnteriorP = dPagosSaldoAnteriorP + oR.dServiciosConCargoPP;
                dPagosSaldoAnteriorP = dPagosSaldoAnteriorP + oR.dIvaPP;


                if (dtAnt.Rows.Count > 0)
                {
                    drPagos["SaldoAnteriorP"] = dtAnt.Rows[8]["SaldoNuevoP"];
                }
                else
                    drPagos["SaldoAnteriorP"] = dPagosSaldoAnteriorP;


                decimal dPagosPeriodoP = 0;
                //dPagosPeriodoP = dt.Rows[7]["AbonosP"].S().D();
                dPagosPeriodoP = sPagosN.S() == "" ? "0000.0".D() : Math.Abs(sPagosN.S().D());
                sPagosN = "0000.0";

                drPagos["EnEstePeriodoP"] = dPagosPeriodoP;
                drPagos["SaldoNuevoP"] = dPagosSaldoAnteriorP + dPagosPeriodoP;

                dtFinal.Rows.Add(drSumas);
                dtFinal.Rows.Add(drPagos);

                // ---==========   SALDO  ==========--- //
                DataRow drSaldo = dtFinal.NewRow();
                drSaldo["ConceptoU"] = "SALDO POR PAGAR";
                drSaldo["SaldoAnteriorU"] = dtFinal.Rows[7].S("SaldoAnteriorU").D() - dtFinal.Rows[8].S("SaldoAnteriorU").D();
                drSaldo["EnEstePeriodoU"] = dtFinal.Rows[7].S("EnEstePeriodoU").D() - dtFinal.Rows[8].S("EnEstePeriodoU").D();
                drSaldo["SaldoNuevoU"] = dtFinal.Rows[7].S("SaldoNuevoU").D() - dtFinal.Rows[8].S("SaldoNuevoU").D();

                drSaldo["ConceptoP"] = "SALDO POR PAGAR";
                drSaldo["SaldoAnteriorP"] = dtFinal.Rows[7].S("SaldoAnteriorP").D() - dtFinal.Rows[8].S("SaldoAnteriorP").D();
                drSaldo["EnEstePeriodoP"] = dtFinal.Rows[7].S("EnEstePeriodoP").D() - dtFinal.Rows[8].S("EnEstePeriodoP").D();
                drSaldo["SaldoNuevoP"] = dtFinal.Rows[7].S("SaldoNuevoP").D() - dtFinal.Rows[8].S("SaldoNuevoP").D();


                dtFinal.Rows.Add(drSaldo);

                return dtFinal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private DataTable ArmaTablaResumenAcumuladoPos(DataTable dtAnt, DataTable dt, ReporteEdoCuent oR)
        {
            try
            {
                DataTable dtFinal = new DataTable();
                dtFinal.Columns.Add("ConceptoU");
                dtFinal.Columns.Add("SaldoAnteriorU", typeof(decimal));
                dtFinal.Columns.Add("EnEstePeriodoU", typeof(decimal));
                dtFinal.Columns.Add("SaldoNuevoU", typeof(decimal));

                dtFinal.Columns.Add("ConceptoP");
                dtFinal.Columns.Add("SaldoAnteriorP", typeof(decimal));
                dtFinal.Columns.Add("EnEstePeriodoP", typeof(decimal));
                dtFinal.Columns.Add("SaldoNuevoP", typeof(decimal));


                for (int i = 0; i < 7; i++)
                {
                    DataRow dr = dtFinal.NewRow();
                    DataRow row = dt.Rows[i];

                    decimal dSaldoNuevoP = 0;
                    decimal dSaldoNuevoD = 0;

                    dr["ConceptoU"] = row.S("ConceptoU");
                    dr["ConceptoP"] = row.S("ConceptoP");
                    dr["EnEstePeriodoU"] = row["CargosU"];
                    dr["EnEstePeriodoP"] = row["CargosP"];

                    switch (row.S("ConceptoU"))
                    {
                        case "ANTICIPO INICIAL":
                            decimal dAnticipoInicialP = 0;
                            decimal dAnticipoInicialD = 0;

                            //// Anticipo Inicial Dolares
                            //dAnticipoInicialD = oR.dAnticipoInicialSD;
                            //// Saldo Inicial  = (Saldo Inicial + Saldos Anteriores)
                            //dAnticipoInicialD = (dAnticipoInicialD + oR.dAnticipoInicialSPD);
                            //// Saldo Inicial  = (Saldo Inicial  +- Gastos Internos Anteriores)
                            //dAnticipoInicialD = (dAnticipoInicialD + oR.dAnticipoInicialGIAD);

                            //// Anticipo Inicial Pesos
                            //dAnticipoInicialP = oR.dAnticipoInicialSP;
                            //// Saldo Inicial  = (Saldo Inicial + Saldos Anteriores)
                            //dAnticipoInicialP = (dAnticipoInicialP + oR.dAnticipoInicialSPP);
                            //// Saldo Inicial  = (Saldo Inicial  +- Gastos Internos Anteriores)
                            //dAnticipoInicialP = (dAnticipoInicialP + oR.dAnticipoInicialGIAP);

                            //dr["SaldoAnteriorU"] = dAnticipoInicialD;
                            //dr["SaldoAnteriorP"] = dAnticipoInicialP;

                            dr["SaldoAnteriorU"] = dtAnt.Rows[0]["SaldoNuevoU"];
                            dr["SaldoAnteriorP"] = dtAnt.Rows[0]["SaldoNuevoP"];

                            //// Saldo Nuevo = Saldo Anterior +  Cargos
                            //dSaldoNuevoD = (dAnticipoInicialD + row["CargosU"].S().D());
                            //dSaldoNuevoP = (dAnticipoInicialP + row["CargosP"].S().D());

                            // Saldo Nuevo = Saldo Anterior +  Cargos
                            dSaldoNuevoD = (dtAnt.Rows[0]["SaldoAnteriorU"].S().D() + row["CargosU"].S().D());
                            dSaldoNuevoP = (dtAnt.Rows[0]["SaldoAnteriorP"].S().D() + row["CargosP"].S().D());

                            dr["SaldoNuevoU"] = dSaldoNuevoD;
                            dr["SaldoNuevoP"] = dSaldoNuevoP;

                            break;
                        case "QUINTO AÑO":
                            decimal dQuintoAnioP = 0;
                            decimal dQuintoAnioD = 0;

                            //// Anticipo Inicial Dolares
                            //dQuintoAnioD = oR.dQuintoAnioSD;
                            //// Saldo Inicial  = (Saldo Inicial + Saldos Anteriores)
                            //dQuintoAnioD = (dQuintoAnioD + oR.dQuintoAnioSPD);
                            //// Saldo Inicial  = (Saldo Inicial  +- Gastos Internos Anteriores)
                            //dQuintoAnioD = (dQuintoAnioD + oR.dQuintoAnioGIAD);


                            //// Anticipo Inicial Pesos
                            //dQuintoAnioP = oR.dQuintoAnioSP;
                            //// Saldo Inicial  = (Saldo Inicial + Saldos Anteriores)
                            //dQuintoAnioP = (dQuintoAnioP + oR.dQuintoAnioSPP);
                            //// Saldo Inicial  = (Saldo Inicial  +- Gastos Internos Anteriores)
                            //dQuintoAnioP = (dQuintoAnioP + oR.dQuintoAnioGIAP);

                            //dr["SaldoAnteriorU"] = dQuintoAnioD;
                            //dr["SaldoAnteriorP"] = dQuintoAnioP;

                            //// Saldo Nuevo = Saldo Anterior +  Cargos - Abonos
                            //dSaldoNuevoD = (dQuintoAnioD + row["CargosU"].S().D());
                            //dSaldoNuevoP = (dQuintoAnioP + row["CargosP"].S().D());

                            dr["SaldoAnteriorU"] = dtAnt.Rows[1]["SaldoNuevoU"];
                            dr["SaldoAnteriorP"] = dtAnt.Rows[1]["SaldoNuevoP"];

                            // Saldo Nuevo = Saldo Anterior +  Cargos
                            dSaldoNuevoD = (dtAnt.Rows[1]["SaldoAnteriorU"].S().D() + row["CargosU"].S().D());
                            dSaldoNuevoP = (dtAnt.Rows[1]["SaldoAnteriorP"].S().D() + row["CargosP"].S().D());

                            dr["SaldoNuevoU"] = dSaldoNuevoD;
                            dr["SaldoNuevoP"] = dSaldoNuevoP;

                            break;
                        case "FIJO ANUAL":
                            decimal dFijoAnualP = 0;
                            decimal dFijoAnualD = 0;

                            //// Anticipo Inicial Dolares
                            //dFijoAnualD = oR.dFijoAnualSD;
                            //// Saldo Inicial  = (Saldo Inicial + Saldos Anteriores)
                            //dFijoAnualD = (dFijoAnualD + oR.dFijoAnualSPD);
                            //// Saldo Inicial  = (Saldo Inicial  +- Gastos Internos Anteriores)
                            //dFijoAnualD = (dFijoAnualD + oR.dFijoAnualGIAD);

                            //// Anticipo Inicial Pesos
                            //dFijoAnualP = oR.dFijoAnualSP;
                            //// Saldo Inicial  = (Saldo Inicial + Saldos Anteriores)
                            //dFijoAnualP = (dFijoAnualP + oR.dFijoAnualSPP);
                            //// Saldo Inicial  = (Saldo Inicial  +- Gastos Internos Anteriores)
                            //dFijoAnualP = (dFijoAnualP + oR.dFijoAnualGIAP);

                            //dr["SaldoAnteriorU"] = dFijoAnualD;
                            //dr["SaldoAnteriorP"] = dFijoAnualP;

                            //// Saldo Nuevo = Saldo Anterior +  Cargos - Abonos
                            //dSaldoNuevoD = (dFijoAnualD + row["CargosU"].S().D());
                            //dSaldoNuevoP = (dFijoAnualP + row["CargosP"].S().D());


                            dr["SaldoAnteriorU"] = dtAnt.Rows[2]["SaldoNuevoU"];
                            dr["SaldoAnteriorP"] = dtAnt.Rows[2]["SaldoNuevoP"];

                            // Saldo Nuevo = Saldo Anterior +  Cargos
                            dSaldoNuevoD = (dtAnt.Rows[2]["SaldoAnteriorU"].S().D() + row["CargosU"].S().D());
                            dSaldoNuevoP = (dtAnt.Rows[2]["SaldoAnteriorP"].S().D() + row["CargosP"].S().D());


                            dr["SaldoNuevoU"] = dSaldoNuevoD;
                            dr["SaldoNuevoP"] = dSaldoNuevoP;

                            break;
                        case "VUELOS":
                            decimal dVuelosP = 0;
                            decimal dVuelosD = 0;

                            //// Anticipo Inicial Dolares
                            //dVuelosD = oR.dVuelosSD;
                            //// Saldo Inicial  = (Saldo Inicial + Saldos Anteriores)
                            //dVuelosD = (dVuelosD + oR.dVuelosSPD);
                            //// Saldo Inicial  = (Saldo Inicial  +- Gastos Internos Anteriores)
                            //dVuelosD = (dVuelosD + oR.dVuelosGIAD);

                            //// Anticipo Inicial Pesos
                            //dVuelosP = oR.dVuelosSP;
                            //// Saldo Inicial  = (Saldo Inicial + Saldos Anteriores)
                            //dVuelosP = (dVuelosP + oR.dVuelosSPP);
                            //// Saldo Inicial  = (Saldo Inicial  +- Gastos Internos Anteriores)
                            //dVuelosP = (dVuelosP + oR.dVuelosGIAP);

                            //dr["SaldoAnteriorU"] = dVuelosD;
                            //dr["SaldoAnteriorP"] = dVuelosP;

                            //// Saldo Nuevo = Saldo Anterior +  Cargos - Abonos
                            //dSaldoNuevoD = (dVuelosD + row["CargosU"].S().D());
                            //dSaldoNuevoP = (dVuelosP + row["CargosP"].S().D());

                            dr["SaldoAnteriorU"] = dtAnt.Rows[3]["SaldoNuevoU"];
                            dr["SaldoAnteriorP"] = dtAnt.Rows[3]["SaldoNuevoP"];

                            // Saldo Nuevo = Saldo Anterior +  Cargos
                            dSaldoNuevoD = (dtAnt.Rows[3]["SaldoAnteriorU"].S().D() + row["CargosU"].S().D());
                            dSaldoNuevoP = (dtAnt.Rows[3]["SaldoAnteriorP"].S().D() + row["CargosP"].S().D());

                            dr["SaldoNuevoU"] = dSaldoNuevoD;
                            dr["SaldoNuevoP"] = dSaldoNuevoP;

                            break;
                        case "GASTOS":
                            decimal dGastosP = 0;
                            decimal dGastosD = 0;

                            //// Anticipo Inicial Dolares
                            //dGastosD = oR.dGastosSD;
                            //// Saldo Inicial  = (Saldo Inicial + Saldos Anteriores)
                            //dGastosD = (dGastosD + oR.dGastosSPD);
                            //// Saldo Inicial  = (Saldo Inicial  +- Gastos Internos Anteriores)
                            //dGastosD = (dGastosD + oR.dGastosGIAD);


                            //// Anticipo Inicial Pesos
                            //dGastosP = oR.dGastosSP;
                            //// Saldo Inicial  = (Saldo Inicial + Saldos Anteriores)
                            //dGastosP = (dGastosP + oR.dGastosSPP);
                            //// Saldo Inicial  = (Saldo Inicial  +- Gastos Internos Anteriores)
                            //dGastosP = (dGastosP + oR.dGastosGIAP);

                            //dr["SaldoAnteriorU"] = dGastosD;
                            //dr["SaldoAnteriorP"] = dGastosP;

                            //// Saldo Nuevo = Saldo Anterior +  Cargos - Abonos
                            //dSaldoNuevoD = (dGastosD + row["CargosU"].S().D());
                            //dSaldoNuevoP = (dGastosP + row["CargosP"].S().D());

                            dr["SaldoAnteriorU"] = dtAnt.Rows[4]["SaldoNuevoU"];
                            dr["SaldoAnteriorP"] = dtAnt.Rows[4]["SaldoNuevoP"];

                            // Saldo Nuevo = Saldo Anterior +  Cargos
                            dSaldoNuevoD = (dtAnt.Rows[4]["SaldoAnteriorU"].S().D() + row["CargosU"].S().D());
                            dSaldoNuevoP = (dtAnt.Rows[4]["SaldoAnteriorP"].S().D() + row["CargosP"].S().D());

                            dr["SaldoNuevoU"] = dSaldoNuevoD;
                            dr["SaldoNuevoP"] = dSaldoNuevoP;

                            break;
                        case "SERVICIOS CON CARGO":
                            decimal dServiciosConCargoP = 0;
                            decimal dServiciosConCargoD = 0;

                            // Anticipo Inicial Dolares
                            //dServiciosConCargoD = oR.dServiciosConCargoSD;
                            //// Saldo Inicial  = (Saldo Inicial + Saldos Anteriores)
                            //dServiciosConCargoD = (dServiciosConCargoD + oR.dServiciosConCargoSPD);
                            //// Saldo Inicial  = (Saldo Inicial  +- Gastos Internos Anteriores)
                            //dServiciosConCargoD = (dServiciosConCargoD + oR.dServiciosConCargoGIAD);


                            //// Anticipo Inicial Pesos
                            //dServiciosConCargoP = oR.dServiciosConCargoSP;
                            //// Saldo Inicial  = (Saldo Inicial + Saldos Anteriores)
                            //dServiciosConCargoP = (dServiciosConCargoP + oR.dServiciosConCargoSPP);
                            //// Saldo Inicial  = (Saldo Inicial  +- Gastos Internos Anteriores)
                            //dServiciosConCargoP = (dServiciosConCargoP + oR.dServiciosConCargoGIAP);

                            //dr["SaldoAnteriorU"] = dServiciosConCargoD;
                            //dr["SaldoAnteriorP"] = dServiciosConCargoP;

                            //// Saldo Nuevo = Saldo Anterior +  Cargos - Abonos
                            //dSaldoNuevoD = (dServiciosConCargoD + row["CargosU"].S().D());
                            //dSaldoNuevoP = (dServiciosConCargoP + row["CargosP"].S().D());

                            dr["SaldoAnteriorU"] = dtAnt.Rows[5]["SaldoNuevoU"];
                            dr["SaldoAnteriorP"] = dtAnt.Rows[5]["SaldoNuevoP"];

                            // Saldo Nuevo = Saldo Anterior +  Cargos
                            dSaldoNuevoD = (dtAnt.Rows[5]["SaldoAnteriorU"].S().D() + row["CargosU"].S().D());
                            dSaldoNuevoP = (dtAnt.Rows[5]["SaldoAnteriorP"].S().D() + row["CargosP"].S().D());


                            dr["SaldoNuevoU"] = dSaldoNuevoD;
                            dr["SaldoNuevoP"] = dSaldoNuevoP;

                            break;
                        case "I.V.A.":
                            decimal dIvaP = 0;
                            decimal dIvaD = 0;

                            //// Anticipo Inicial Dolares
                            //dIvaD = oR.dIvaSD;
                            //// Saldo Inicial  = (Saldo Inicial + Saldos Anteriores)
                            //dIvaD = (dIvaD + oR.dIvaSPD);
                            //// Saldo Inicial  = (Saldo Inicial  +- Gastos Internos Anteriores)
                            //dIvaD = (dIvaD + oR.dIvaGIAD);


                            //// Anticipo Inicial Pesos
                            //dIvaP = oR.dIvaSP;
                            //// Saldo Inicial  = (Saldo Inicial + Saldos Anteriores)
                            //dIvaP = (dIvaP + oR.dIvaSPP);
                            //// Saldo Inicial  = (Saldo Inicial  +- Gastos Internos Anteriores)
                            //dIvaP = (dIvaP + oR.dIvaGIAP);

                            //dr["SaldoAnteriorU"] = dIvaD;
                            //dr["SaldoAnteriorP"] = dIvaP;

                            //// Saldo Nuevo = Saldo Anterior +  Cargos - Abonos
                            //dSaldoNuevoD = (dIvaD + row["CargosU"].S().D());
                            //dSaldoNuevoP = (dIvaP + row["CargosP"].S().D());

                            dr["SaldoAnteriorU"] = dtAnt.Rows[6]["SaldoNuevoU"];
                            dr["SaldoAnteriorP"] = dtAnt.Rows[6]["SaldoNuevoP"];

                            // Saldo Nuevo = Saldo Anterior +  Cargos
                            dSaldoNuevoD = (dtAnt.Rows[6]["SaldoAnteriorU"].S().D() + row["CargosU"].S().D());
                            dSaldoNuevoP = (dtAnt.Rows[6]["SaldoAnteriorP"].S().D() + row["CargosP"].S().D());

                            dr["SaldoNuevoU"] = dSaldoNuevoD;
                            dr["SaldoNuevoP"] = dSaldoNuevoP;

                            break;
                    }

                    dtFinal.Rows.Add(dr);
                }

                //if (dtAnt.Rows.Count > 0)
                //{
                //    foreach (DataRow row in dtFinal.Rows)
                //    {
                //        foreach (DataRow row2 in dtAnt.Rows)
                //        {
                //            if (row.S("ConceptoU") == row2.S("ConceptoU"))
                //            {
                //                row["SaldoAnteriorU"] = row2["SaldoNuevoU"];
                //                row["SaldoAnteriorP"] = row2["SaldoNuevoP"];
                //            }
                //        }
                //    }
                //}

                // ---==========   SUMAS  ==========--- //
                DataRow drSumas = dtFinal.NewRow();
                drSumas["ConceptoU"] = "SUMA";
                drSumas["SaldoAnteriorU"] = ObtieneSumaTabla(dtFinal, "SaldoAnteriorU");
                drSumas["EnEstePeriodoU"] = ObtieneSumaTabla(dtFinal, "EnEstePeriodoU");
                drSumas["SaldoNuevoU"] = ObtieneSumaTabla(dtFinal, "SaldoNuevoU");

                drSumas["ConceptoP"] = "SUMA";
                drSumas["SaldoAnteriorP"] = ObtieneSumaTabla(dtFinal, "SaldoAnteriorP");
                drSumas["EnEstePeriodoP"] = ObtieneSumaTabla(dtFinal, "EnEstePeriodoP");
                drSumas["SaldoNuevoP"] = ObtieneSumaTabla(dtFinal, "SaldoNuevoP");

                // ---==========   PAGOS  ==========--- //
                DataRow drPagos = dtFinal.NewRow();
                drPagos["ConceptoU"] = "PAGOS";

                decimal dPagosSaldoAnterior = 0;
                dPagosSaldoAnterior = dPagosSaldoAnterior + oR.dAnticipoInicialPD;
                dPagosSaldoAnterior = dPagosSaldoAnterior + oR.dQuintoAnioPD;
                dPagosSaldoAnterior = dPagosSaldoAnterior + oR.dFijoAnualPD;
                dPagosSaldoAnterior = dPagosSaldoAnterior + oR.dVuelosPD;
                dPagosSaldoAnterior = dPagosSaldoAnterior + oR.dGastosPD;
                dPagosSaldoAnterior = dPagosSaldoAnterior + oR.dServiciosConCargoPD;
                dPagosSaldoAnterior = dPagosSaldoAnterior + oR.dIvaPD;


                if (dtAnt.Rows.Count > 0)
                {
                    drPagos["SaldoAnteriorU"] = dtAnt.Rows[8]["SaldoNuevoU"];
                }
                else
                    drPagos["SaldoAnteriorU"] = dPagosSaldoAnterior;

                decimal dPagosPeriodoU = 0;
                //dPagosPeriodoU = dt.Rows[7]["AbonosU"].S().D();
                dPagosPeriodoU = sPagosD.S() == "" ? "0000.0".D() : Math.Abs(sPagosD.S().D());
                sPagosD = "0000.0";

                drPagos["EnEstePeriodoU"] = dPagosPeriodoU;
                drPagos["SaldoNuevoU"] = dPagosSaldoAnterior + dPagosPeriodoU;


                drPagos["ConceptoP"] = "PAGOS";

                decimal dPagosSaldoAnteriorP = 0;
                dPagosSaldoAnteriorP = dPagosSaldoAnteriorP + oR.dAnticipoInicialPP;
                dPagosSaldoAnteriorP = dPagosSaldoAnteriorP + oR.dQuintoAnioPP;
                dPagosSaldoAnteriorP = dPagosSaldoAnteriorP + oR.dFijoAnualPP;
                dPagosSaldoAnteriorP = dPagosSaldoAnteriorP + oR.dVuelosPP;
                dPagosSaldoAnteriorP = dPagosSaldoAnteriorP + oR.dGastosPP;
                dPagosSaldoAnteriorP = dPagosSaldoAnteriorP + oR.dServiciosConCargoPP;
                dPagosSaldoAnteriorP = dPagosSaldoAnteriorP + oR.dIvaPP;


                if (dtAnt.Rows.Count > 0)
                {
                    drPagos["SaldoAnteriorP"] = dtAnt.Rows[8]["SaldoNuevoP"];
                }
                else
                    drPagos["SaldoAnteriorP"] = dPagosSaldoAnteriorP;


                decimal dPagosPeriodoP = 0;
                //dPagosPeriodoP = dt.Rows[7]["AbonosP"].S().D();
                dPagosPeriodoP = sPagosN.S() == "" ? "0000.0".D() : Math.Abs(sPagosN.S().D());
                sPagosN = "0000.0";

                drPagos["EnEstePeriodoP"] = dPagosPeriodoP;
                drPagos["SaldoNuevoP"] = dPagosSaldoAnteriorP + dPagosPeriodoP;

                dtFinal.Rows.Add(drSumas);
                dtFinal.Rows.Add(drPagos);

                // ---==========   SALDO  ==========--- //
                DataRow drSaldo = dtFinal.NewRow();
                drSaldo["ConceptoU"] = "SALDO POR PAGAR";
                drSaldo["SaldoAnteriorU"] = dtFinal.Rows[7].S("SaldoAnteriorU").D() - dtFinal.Rows[8].S("SaldoAnteriorU").D();
                drSaldo["EnEstePeriodoU"] = dtFinal.Rows[7].S("EnEstePeriodoU").D() - dtFinal.Rows[8].S("EnEstePeriodoU").D();
                drSaldo["SaldoNuevoU"] = dtFinal.Rows[7].S("SaldoNuevoU").D() - dtFinal.Rows[8].S("SaldoNuevoU").D();

                drSaldo["ConceptoP"] = "SALDO POR PAGAR";
                drSaldo["SaldoAnteriorP"] = dtFinal.Rows[7].S("SaldoAnteriorP").D() - dtFinal.Rows[8].S("SaldoAnteriorP").D();
                drSaldo["EnEstePeriodoP"] = dtFinal.Rows[7].S("EnEstePeriodoP").D() - dtFinal.Rows[8].S("EnEstePeriodoP").D();
                drSaldo["SaldoNuevoP"] = dtFinal.Rows[7].S("SaldoNuevoP").D() - dtFinal.Rows[8].S("SaldoNuevoP").D();


                dtFinal.Rows.Add(drSaldo);

                return dtFinal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LoadHeadEdoCnta(DataTable dtObjCat)
        {
            try
            {
                ViewState["LoadHeadEdoCnta"] = dtObjCat;
            }
            catch (Exception x) { throw x; }
        }
        public void CreaHead()
        {
            try
            {
                DataTable Head = (DataTable)ViewState["LoadHeadEdoCnta"];
                if (Head != null && Head.Rows.Count > 0)
                {
                    float fTiemp = 0;
                    string HrsVol = Utils.ObtieneTotalTiempo(Head, "HrsVoladas", ref fTiemp);


                    string cadena = @"<table border='1' bgColor='#ffffff' borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; background:white; width: 100%'>" +
                    "<tr><td>Equipo Contratado: " + Head.Rows[0]["Equipo"].S() + "</td><td >Paquete: " + Head.Rows[0]["Paquete"].S() + "</td>" +
                    "<td >Costo Directo Nacional: " + Convert.ToDecimal(Head.Rows[0]["CstDirNac"].S()).ToString("N2") + "</td><td >Costo Directo Internacional: " + Convert.ToDecimal(Head.Rows[0]["CstDirInt"].S()).ToString("N2") + "</td>" +
                    "<td >Horas Voladas: " + HrsVol + "</td></tr>" +
                    "</table>";

                    head.InnerHtml = cadena;
                }
            }
            catch (Exception x) { throw x; }
        }
        protected void ArmaDataTableVuelos()
        {
            try
            {
                DataTable DTVuelos = new DataTable();
                DTVuelos.Columns.Add("FechaSalida");
                DTVuelos.Columns.Add("FechaLlegada");
                DTVuelos.Columns.Add("Remision");
                DTVuelos.Columns.Add("Prefactura");
                DTVuelos.Columns.Add("Matricula");
                DTVuelos.Columns.Add("Ruta");
                DTVuelos.Columns.Add("CNal");
                DTVuelos.Columns.Add("CInt");
                DTVuelos.Columns.Add("TrNalTiempo");
                DTVuelos.Columns.Add("TrIntTiempo");
                DTVuelos.Columns.Add("TeNalTiempo");
                DTVuelos.Columns.Add("TeIntTiempo");
                DTVuelos.Columns.Add("PNalTiempo");
                DTVuelos.Columns.Add("PIntTiempo");
                DTVuelos.Columns.Add("CostoVuelo");
                DTVuelos.Columns.Add("ServicioCargo");
                DTVuelos.Columns.Add("IsTotalRow");

                if (ViewState["gvVuelo"] != null && ((DataTable)ViewState["gvVuelo"]).Rows.Count > 0)
                {
                    DataTable gvVuelo = (DataTable)ViewState["gvVuelo"];
                    DataTable Detalle = (DataTable)ViewState["LoadVueloDetail"];

                    float fTiemp = 0;
                    string TrNalTiempo = Utils.ObtieneTotalTiempo(gvVuelo, "TrNalTiempo", ref fTiemp);
                    string TrIntTiempo = Utils.ObtieneTotalTiempo(gvVuelo, "TrIntTiempo", ref fTiemp);
                    string TeNalTiempo = Utils.ObtieneTotalTiempo(gvVuelo, "TeNalTiempo", ref fTiemp);
                    string TeIntTiempo = Utils.ObtieneTotalTiempo(gvVuelo, "TeIntTiempo", ref fTiemp);
                    decimal TotalPNal = SumaImportesTabla(gvVuelo, "PNalTiempo");
                    decimal TotalPInt = SumaImportesTabla(gvVuelo, "PIntTiempo");

                    for (int i = 0; i < gvVuelo.Rows.Count; i++)
                    {
                        DataRow drCant = DTVuelos.NewRow();
                        drCant["FechaSalida"] = gvVuelo.Rows[i]["FechaSalida"] == null || gvVuelo.Rows[i]["FechaSalida"].S().EstaVacio() ? "   " : gvVuelo.Rows[i]["FechaSalida"].S();
                        drCant["FechaLlegada"] = gvVuelo.Rows[i]["FechaLlegada"].S();
                        drCant["Remision"] = gvVuelo.Rows[i]["Remision"].S();
                        drCant["Prefactura"] = gvVuelo.Rows[i]["Prefactura"].S();
                        drCant["Matricula"] = gvVuelo.Rows[i]["Matricula"].S();
                        drCant["Ruta"] = gvVuelo.Rows[i]["Ruta"].S();
                        drCant["CNal"] = gvVuelo.Rows[i]["CNal"].S();
                        drCant["CInt"] = gvVuelo.Rows[i]["CInt"].S();
                        drCant["TrNalTiempo"] = gvVuelo.Rows[i]["TrNalTiempo"].S();
                        drCant["TrIntTiempo"] = gvVuelo.Rows[i]["TrIntTiempo"].S();
                        drCant["TeNalTiempo"] = gvVuelo.Rows[i]["TeNalTiempo"].S();
                        drCant["TeIntTiempo"] = gvVuelo.Rows[i]["TeIntTiempo"].S();
                        drCant["PNalTiempo"] = gvVuelo.Rows[i]["PNalTiempo"].S();
                        drCant["PIntTiempo"] = gvVuelo.Rows[i]["PIntTiempo"].S();
                        drCant["IsTotalRow"] = "0";
                        DTVuelos.Rows.Add(drCant);

                        DataRow drCobro = DTVuelos.NewRow();
                        drCobro["TrNalTiempo"] = Convert.ToDecimal(gvVuelo.Rows[i]["TrNalCobro"]).ToString("N2");
                        drCobro["TrIntTiempo"] = Convert.ToDecimal(gvVuelo.Rows[i]["TrIntCobro"]).ToString("N2");
                        drCobro["TeNalTiempo"] = Convert.ToDecimal(gvVuelo.Rows[i]["TeNalCobro"]).ToString("N2");
                        drCobro["TeIntTiempo"] = Convert.ToDecimal(gvVuelo.Rows[i]["TeIntCobro"]).ToString("N2");
                        drCobro["PNalTiempo"] = Convert.ToDecimal(gvVuelo.Rows[i]["PNalCobro"]).ToString("N2");
                        drCobro["PIntTiempo"] = Convert.ToDecimal(gvVuelo.Rows[i]["PIntCobro"]).ToString("N2");
                        drCobro["IsTotalRow"] = "0";
                        DTVuelos.Rows.Add(drCobro);
                        string x = gvVuelo.Rows[i]["IdRemision"].S();
                        DataTable Dt = Detalle.Select("IdRemision =  " + x.S() + "").CopyToDataTable();

                        for (int t = 21; t < Dt.Columns.Count; t++)
                        {
                            DataRow drFactor = DTVuelos.NewRow();
                            drFactor["IsTotalRow"] = "0";
                            if (Dt.Rows[0][t].B() && Dt.Columns[t].ColumnName.Equals("AplicaFactorEspecial"))
                            {
                                drFactor["TrNalTiempo"] = "Factor Especial " + Dt.Rows[0][1].S();
                                DTVuelos.Rows.Add(drFactor);
                            }
                            else if (Dt.Rows[0][t].B() && Dt.Columns[t].ColumnName.Equals("GiraHorario"))
                            {
                                drFactor["TrNalTiempo"] = "Factor Gira Horario " + Dt.Rows[0][2].S();
                                DTVuelos.Rows.Add(drFactor);
                            }
                            else if (Dt.Rows[0][t].B() && Dt.Columns[t].ColumnName.Equals("Gira"))
                            {
                                drFactor["TrNalTiempo"] = "Factor Gira " + Dt.Rows[0][3].S();
                                DTVuelos.Rows.Add(drFactor);
                            }
                            else if (Dt.Rows[0][t].B() && Dt.Columns[t].ColumnName.Equals("Intercamio"))
                            {
                                drFactor["TrNalTiempo"] = "Factor Intercambio " + Dt.Rows[0][4].S();
                                DTVuelos.Rows.Add(drFactor);
                            }
                            else if (Dt.Rows[0][t].B() && Dt.Columns[t].ColumnName.Equals("AplicaFechaPico"))
                            {
                                drFactor["TrNalTiempo"] = "Factor Fecha Pico " + Dt.Rows[0][5].S();
                                DTVuelos.Rows.Add(drFactor);
                            }
                            else if (Dt.Rows[0][t].B() && Dt.Columns[t].ColumnName.Equals("AplicaVueloSimultaneo"))
                            {
                                drFactor["TrNalTiempo"] = "Factor Vuelo Simultaneo " + Dt.Rows[0][6].S();
                                DTVuelos.Rows.Add(drFactor);
                            }
                            else if (Dt.Rows[0][t].B() && Dt.Columns[t].ColumnName.Equals("AplicaFactorTramoNal"))
                            {
                                drFactor["TrNalTiempo"] = "Factor Tramo Nacional " + Dt.Rows[0]["FactorTramoNal"].S();
                                DTVuelos.Rows.Add(drFactor);
                            }
                            else if (Dt.Rows[0][t].B() && Dt.Columns[t].ColumnName.Equals("AplicaFactorTramoInt"))
                            {
                                drFactor["TrNalTiempo"] = "Factor Tramo Internacional " + Dt.Rows[0]["FactorTramoInt"].S();
                                DTVuelos.Rows.Add(drFactor);
                            }
                        }

                        DataRow drTotal = DTVuelos.NewRow();
                        drTotal["TrNalTiempo"] = Convert.ToDecimal(gvVuelo.Rows[i]["TrNalTotal"]).ToString("N2");
                        drTotal["TrIntTiempo"] = Convert.ToDecimal(gvVuelo.Rows[i]["TrIntTotal"]).ToString("N2");
                        drTotal["TeNalTiempo"] = Convert.ToDecimal(gvVuelo.Rows[i]["TeNalTotal"]).ToString("N2");
                        drTotal["TeIntTiempo"] = Convert.ToDecimal(gvVuelo.Rows[i]["TeIntTotal"]).ToString("N2");
                        drTotal["PNalTiempo"] = Convert.ToDecimal(gvVuelo.Rows[i]["PNalTotal"]).ToString("N2");
                        drTotal["PIntTiempo"] = Convert.ToDecimal(gvVuelo.Rows[i]["PIntTotal"]).ToString("N2");
                        drTotal["CostoVuelo"] = Convert.ToDecimal(gvVuelo.Rows[i]["CostoVuelo"]).ToString("N2");
                        drTotal["ServicioCargo"] = Convert.ToDecimal(Detalle.Rows[i]["ServicioCargo"]).ToString("N2");
                        drTotal["IsTotalRow"] = "1";
                        DTVuelos.Rows.Add(drTotal);
                    }

                    DataRow drTotTiemp = DTVuelos.NewRow();
                    drTotTiemp["TrNalTiempo"] = TrNalTiempo;
                    drTotTiemp["TrIntTiempo"] = TrIntTiempo;
                    drTotTiemp["TeNalTiempo"] = TeNalTiempo;
                    drTotTiemp["TeIntTiempo"] = TeIntTiempo;

                    drTotTiemp["PNalTiempo"] = TotalPNal;
                    drTotTiemp["PIntTiempo"] = TotalPInt;

                    drTotTiemp["IsTotalRow"] = "2";
                    drTotTiemp["FechaSalida"] = "Total Horas";
                    DTVuelos.Rows.Add(drTotTiemp);

                    DataTable Ddt = Detalle.Select("IdRemision =  0").CopyToDataTable();

                    DataRow drTotalT = DTVuelos.NewRow();
                    drTotalT["TrNalTiempo"] = Convert.ToDecimal(Ddt.Rows[0]["TrNalTotal"]).ToString("N2");
                    drTotalT["TrIntTiempo"] = Convert.ToDecimal(Ddt.Rows[0]["TrIntTotal"]).ToString("N2");
                    drTotalT["TeNalTiempo"] = Convert.ToDecimal(Ddt.Rows[0]["TeNalTotal"]).ToString("N2");
                    drTotalT["TeIntTiempo"] = Convert.ToDecimal(Ddt.Rows[0]["TeIntTotal"]).ToString("N2");
                    drTotalT["PNalTiempo"] = Convert.ToDecimal(Ddt.Rows[0]["PNalTotal"]).ToString("N2");
                    drTotalT["PIntTiempo"] = Convert.ToDecimal(Ddt.Rows[0]["PIntTotal"]).ToString("N2");
                    drTotalT["CostoVuelo"] = Convert.ToDecimal(Ddt.Rows[0]["CostoVuelo"]).ToString("N2");
                    drTotalT["ServicioCargo"] = Convert.ToDecimal(Ddt.Rows[0]["ServicioCargo"]).ToString("N2");
                    drTotalT["IsTotalRow"] = "2";
                    drTotalT["FechaSalida"] = "Total Importe";
                    DTVuelos.Rows.Add(drTotalT);

                    ViewState["Vuelos"] = DTVuelos;
                }

                ViewState["Vuelos"] = DTVuelos;
            }
            catch (Exception x) { throw x; }
        }
        protected void ArmaDataTableVuelosdif()
        {
            try
            {
                if (ViewState["LoadVueloEqDif"] != null)
                {
                    DataTable LoadVueloEqDif = (DataTable)ViewState["LoadVueloEqDif"];
                    float fTiemp = 0;
                    string TrNalTiempo = Utils.ObtieneTotalTiempo(LoadVueloEqDif, "TrNalTiempo", ref fTiemp);
                    string TrIntTiempo = Utils.ObtieneTotalTiempo(LoadVueloEqDif, "TrIntTiempo", ref fTiemp);
                    string TeNalTiempo = Utils.ObtieneTotalTiempo(LoadVueloEqDif, "TeNalTiempo", ref fTiemp);
                    string TeIntTiempo = Utils.ObtieneTotalTiempo(LoadVueloEqDif, "TeIntTiempo", ref fTiemp);
                    decimal TotalPNal = SumaImportesTabla(LoadVueloEqDif, "PNalTotal");
                    decimal TotalPInt = SumaImportesTabla(LoadVueloEqDif, "PIntTotal");
                    LoadVueloEqDif.Columns.Add("IsTotalRow");
                    foreach (DataRow row in LoadVueloEqDif.Rows)
                    {
                        row["IsTotalRow"] = "0";
                    }

                    DataRow drtotal = LoadVueloEqDif.NewRow();
                    drtotal["TrNalTiempo"] = TrNalTiempo.ToString();
                    drtotal["TrIntTiempo"] = TrIntTiempo.ToString();
                    drtotal["TeNalTiempo"] = TeNalTiempo.ToString();
                    drtotal["TeIntTiempo"] = TeIntTiempo.ToString();
                    drtotal["PNalTotal"] = TotalPNal.ToString();
                    drtotal["PIntTotal"] = TotalPInt.ToString();
                    drtotal["IsTotalRow"] = "1";
                    drtotal["FechaSalida"] = "Total";
                    LoadVueloEqDif.Rows.Add(drtotal);

                    ViewState["CRVueloEqDif"] = LoadVueloEqDif;
                }
                else
                    ViewState["CRVueloEqDif"] = ViewState["LoadVueloEqDif"];
            }
            catch (Exception x) { throw x; }
        }
        private DataTable ArmaHeadCristal()
        {
            try
            {
                DataTable LoadHeadEdoCnta = (DataTable)ViewState["LoadHeadEdoCnta"];
                DataTable DTC = (DataTable)ViewState["oCliente"];
                DTC = DTC.Select("IdCliente = " + ddlClientes.SelectedItem.Value.S() + "").CopyToDataTable();
                string Razonsocial = DTC.Rows[0]["Nombre"].S();
                float fTiemp = 0;
                string HrsVol = Utils.ObtieneTotalTiempo(LoadHeadEdoCnta, "HrsVoladas", ref fTiemp);

                DataTable DT = new DataTable();
                DT.Columns.Add("Cliente");
                DT.Columns.Add("Contrato");
                DT.Columns.Add("Equipo");
                DT.Columns.Add("Paquete");
                DT.Columns.Add("CstNacional");
                DT.Columns.Add("CstInternacional");
                DT.Columns.Add("Nombre");
                DT.Columns.Add("HrsVoladas");
                DT.Columns.Add("HrsDisp");

                DataRow dr = DT.NewRow();
                dr["Cliente"] = hfCliente["hfCliente"].S();
                dr["Contrato"] = hfContrato["hfContrato"].S();
                dr["Equipo"] = LoadHeadEdoCnta.Rows[0]["Equipo"].S();
                dr["Paquete"] = LoadHeadEdoCnta.Rows[0]["Paquete"].S();
                dr["CstNacional"] = Convert.ToDecimal(LoadHeadEdoCnta.Rows[0]["CstDirNac"].S()).ToString("N2");
                dr["CstInternacional"] = Convert.ToDecimal(LoadHeadEdoCnta.Rows[0]["CstDirInt"].S()).ToString("N2");
                dr["Nombre"] = Razonsocial.S();
                dr["HrsVoladas"] = HrsVol.S().Equals("") ? "00:00" : HrsVol.S();
                dr["HrsDisp"] = "--";
                DT.Rows.Add(dr);

                return DT;
            }
            catch (Exception x) { throw x; }
        }
        protected void CristalReport()
        {
            try
            {
                DataTable D = ArmaHeadCristal();
                ReportDocument rd = new ReportDocument();

                string sFormatoEdoCta = "EstCnta";

                DataTable Head = (DataTable)ViewState["LoadHeadEdoCnta"];
                if (Head != null && Head.Rows.Count > 0)
                    sFormatoEdoCta = Head.Rows[0]["FormatoEdoCta"].S();


                string strPath = string.Empty;
                strPath = Server.MapPath("Views\\Consultas\\CristalReport\\" + sFormatoEdoCta +".rpt");
                strPath = strPath.Replace("\\Views\\CreditoCobranza", "");

                rd.Load(strPath);

                DataTable DTServicioCargo = (DataTable)ViewState["Vuelos"];
                DataTable DTVuelosdif = (DataTable)ViewState["CRVueloEqDif"];
                DataTable DTFacturacion = (DataTable)ViewState["CargaEstadoCuentaMXN"];
                foreach (DataColumn col in DTFacturacion.Columns)
                {
                    col.ReadOnly = false;
                }
                DTFacturacion.Rows[DTFacturacion.Rows.Count - 1]["Fecha"] = "Total";
                DTFacturacion.Rows[DTFacturacion.Rows.Count - 1]["Concepto"] = string.Empty;
                DataTable DTFechas = new DataTable();
                DTFechas.Columns.Add("FechaDesde");
                DTFechas.Columns.Add("FechaHasta");
                DataRow RFechas = DTFechas.NewRow();
                // TO DO
                RFechas["FechaDesde"] = dFechaIni.Date.ToString("dd/MM/yyyy");
                RFechas["FechaHasta"] = dFechaFin.Date.ToString("dd/MM/yyyy");
                DTFechas.Rows.Add(RFechas);

                if (DTServicioCargo != null)
                {

                    rd.Database.Tables["Vuelos"].SetDataSource(DTServicioCargo);
                    rd.Database.Tables["VuelosDiferentes"].SetDataSource(DTVuelosdif);
                    rd.Database.Tables["Facturacion"].SetDataSource(DTFacturacion);
                    rd.Database.Tables["Fechas"].SetDataSource(DTFechas);
                    if (chbAcumulado.Checked)
                    {
                        rd.Database.Tables["Acumulado"].SetDataSource((DataTable)ViewState["GetEstadoCuentaResumenAcumuladoDLLS"]);

                        DataTable DTNoAcumulado = new DataTable();
                        DTNoAcumulado.Columns.Add("ConceptoU");
                        DTNoAcumulado.Columns.Add("SaldoAnteriorU");
                        DTNoAcumulado.Columns.Add("CargosU");
                        DTNoAcumulado.Columns.Add("AbonosU");
                        DTNoAcumulado.Columns.Add("SaldoNuevoU");
                        DTNoAcumulado.Columns.Add("CoceptoP");
                        DTNoAcumulado.Columns.Add("SaldoAnteriorP");
                        DTNoAcumulado.Columns.Add("CargosP");
                        DTNoAcumulado.Columns.Add("AbonosP");
                        DTNoAcumulado.Columns.Add("SaldoNuevoP");

                        rd.Database.Tables["NoAcumulado"].SetDataSource(DTNoAcumulado);

                    }
                    else
                    {
                        rd.Database.Tables["NoAcumulado"].SetDataSource((DataTable)ViewState["GetEstadoCuentaResumenAcumuladoDLLS"]);

                        DataTable DTAcumulado = new DataTable();
                        DTAcumulado.Columns.Add("ConceptoU");
                        DTAcumulado.Columns.Add("SaldoAnteriorU");
                        DTAcumulado.Columns.Add("EnEstePeriodoU");
                        DTAcumulado.Columns.Add("SaldoNuevoU");
                        DTAcumulado.Columns.Add("ConceptoP");
                        DTAcumulado.Columns.Add("SaldoAnteriorP");
                        DTAcumulado.Columns.Add("SaldoNuevoP");
                        DTAcumulado.Columns.Add("EnEstePeriodoP");

                        rd.Database.Tables["Acumulado"].SetDataSource(DTAcumulado);

                    }

                    rd.Database.Tables["Header"].SetDataSource(ArmaHeadCristal());
                    rd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "Estado Cuenta");
                }
            }
            catch (Exception x) { throw x; }
        }
        public decimal SumaImportesTabla(DataTable dt, string sColumna)
        {
            decimal dSuma = 0;
            foreach (DataRow row in dt.Rows)
            {
                dSuma += row.S(sColumna).D();
            }

            return dSuma;
        }
        #endregion

        #region V A R I A B L E S
        private const string sClase = "frmEstadoCuenta.aspx.cs";
        private const string sPagina = "frmEstadoCuenta.aspx";

        EstadoCuenta_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eObjCliente;
        public event EventHandler eObjContrato;
        public event EventHandler eObjVueloHhead;
        public event EventHandler eObjVueloHDetail;
        public event EventHandler eObjVueloEqDif;
        public event EventHandler eObjHeadEdoCnta;

        string sServCargo
        {
            get { return ViewState["sServCargo"].S(); }
            set { ViewState["sServCargo"] = value; }
        }
        string sVuelo
        {
            get { return ViewState["sVuelo"].S(); }
            set { ViewState["sVuelo"] = value; }
        }
        string sIvaN
        {
            get { return ViewState["sIvaN"].S(); }
            set { ViewState["sIvaN"] = value; }
        }
        string sIvaD
        {
            get { return ViewState["sIvaD"].S(); }
            set { ViewState["sIvaD"] = value; }
        }
        string sPagosN
        {
            get { return ViewState["sPagosN"].S(); }
            set { ViewState["sPagosN"] = value; }
        }
        string sPagosD
        {
            get { return ViewState["sPagosD"].S(); }
            set { ViewState["sPagosD"] = value; }
        }
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
        public object[] oArrVuelosHead
        {
            get
            {
                return new object[]
                { 
                    "@IdContrato", ddlContrato.SelectedItem.Value.S(),
                    "@FechaInicio" ,dFechaIni.Date.ToString("MM-dd-yyyy"),
                    "@FechaFin" ,dFechaFin.Date.ToString("MM-dd-yyyy")
                };
            }
        }
        public string NombreSocial
        {
            get { return ViewState["NombreSocial"].S(); }
            set { ViewState["NombreSocial"] = value; }
        }
        #endregion

    }
}