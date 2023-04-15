using ALE_MexJet.DomainModel;
using ALE_MexJet.Interfaces;
using ALE_MexJet.Objetos;
using ALE_MexJet.Presenter;
using DevExpress.Web;
using DevExpress.Web.Bootstrap;
using DevExpress.XtraPrinting.Export.Web;
using NucleoBase.Core;
using Org.BouncyCastle.Utilities.Encoders;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace ALE_MexJet.Views.viaticos
{
    public partial class frmCalculoPagos : System.Web.UI.Page, IViewCalculoPagos
    {
        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            oPresenter = new CalculoPagos_Presenter(this, new DBCalculoPagos());
            gvCalculo.SettingsPager.Position = PagerPosition.TopAndBottom;
            gvCalculo.SettingsPager.ShowDisabledButtons = true;
            gvCalculo.SettingsPager.ShowNumericButtons = true;
            gvCalculo.SettingsPager.ShowSeparators = true;
            gvCalculo.SettingsPager.Summary.Visible = true;
            gvCalculo.SettingsPager.PageSize = 10;
            gvCalculo.SettingsPager.PageSizeItemSettings.Visible = true;
            gvCalculo.SettingsPager.PageSizeItemSettings.Position = PagerPageSizePosition.Right;
            gvCalculo.SettingsText.SearchPanelEditorNullText = "Ingresa la información a buscar:";
            gvCalculo.Settings.ShowGroupPanel = true;
            gvCalculo.Settings.ShowTitlePanel = true;
            gvCalculo.SettingsText.Title = "Periodos Guardados";

            gvPeriodosGuardados.SettingsPager.Position = PagerPosition.TopAndBottom;
            gvPeriodosGuardados.SettingsPager.ShowDisabledButtons = true;
            gvPeriodosGuardados.SettingsPager.ShowNumericButtons = true;
            gvPeriodosGuardados.SettingsPager.ShowSeparators = true;
            gvPeriodosGuardados.SettingsPager.Summary.Visible = true;
            gvPeriodosGuardados.SettingsPager.PageSize = 10;
            gvPeriodosGuardados.SettingsPager.PageSizeItemSettings.Visible = true;
            gvPeriodosGuardados.SettingsPager.PageSizeItemSettings.Position = PagerPageSizePosition.Right;
            gvPeriodosGuardados.SettingsText.SearchPanelEditorNullText = "Ingresa la información a buscar:";
            gvPeriodosGuardados.Settings.ShowGroupPanel = true;
            gvPeriodosGuardados.Settings.ShowTitlePanel = true;
            gvPeriodosGuardados.SettingsText.Title = "Periodos Pendientes";

            gvVuelos.Settings.ShowGroupPanel = false;
            gvVuelos.SettingsPager.PageSize = 100;

            gvDiasViaticos.Settings.ShowGroupPanel = false;
            gvDiasViaticos.SettingsPager.PageSize = 100;

            if (!IsPostBack)
            {
                dtCalculos1X1 = null;
                dtVuelosPiloto1x1 = null;

                if (string.IsNullOrEmpty(date1.Text))
                    sFechaDesde = DateTime.Now.ToShortDateString();
                if (string.IsNullOrEmpty(date2.Text))
                    sFechaHasta = DateTime.Now.ToShortDateString();

                sParametro = txtParametro.Text;

                //if (eSearchObj != null)
                //    eSearchObj(sender, e);

                if (eSearchConceptos != null)
                    eSearchConceptos(sender, e);

                if (eGetParams != null)
                    eGetParams(sender, e);

                pnlVuelos.Visible = false;
            }
        }

        protected void gvPeriodosGuardados_BeforePerformDataSelect(object sender, EventArgs e)
        {
            try
            {
                if(dtCalculos != null && dtCalculos.Rows.Count > 0)
                    (sender as ASPxGridView).DataSource = dtCalculos;
                //else
                //    (sender as ASPxGridView).DataSource = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void gvPeriodosGuardados_Load(object sender, EventArgs e)
        {
            if (dtCalculos == null)
                return;
            (sender as ASPxGridView).DataBind();
        }

        protected void gvCalculo_BeforePerformDataSelect(object sender, EventArgs e)
        {
            try
            {
                if (dtGuardados != null && dtGuardados.Rows.Count > 0)
                    (sender as ASPxGridView).DataSource = dtGuardados;
                //else
                //    (sender as ASPxGridView).DataSource = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void gvCalculo_Load(object sender, EventArgs e)
        {
            if (dtGuardados == null)
                return;
            (sender as ASPxGridView).DataBind();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                dtDiasViaticos = null;
                //dtDiasViaticosHoteles = null;
                oLstCon = null;
                oLstVPil = null;
                oLstPorDiaPil = null;
                sFechaDesde = string.Empty;
                sFechaHasta = string.Empty;
                sFechaInicio = string.Empty;
                sFechaFinal = string.Empty;
                sCvePiloto = string.Empty;
                sParametro = string.Empty;

                sFechaDesde = date1.Text;
                sFechaHasta = date2.Text;
                sParametro = txtParametro.Text;

                //gvPeriodosGuardados.DataSource = null;
                //gvPeriodosGuardados.DataBind();
                //gvCalculo.DataSource = null;
                //gvCalculo.DataBind();

                if(dtGuardados != null)
                    dtGuardados.Dispose();

                if(dtCalculos != null)
                    dtCalculos.Dispose();

                string sFechaIniSave = string.Empty;
                string sFechaFinSave = string.Empty;
                string sCvePilotoSave = string.Empty;
                sFechaIniSave = date1.Text;
                sFechaFinSave = date2.Text;
                sCvePilotoSave = txtParametro.Text;

                sFechaInicio = sFechaIniSave;
                sFechaFinal = sFechaFinSave;
                sCvePiloto = sCvePilotoSave;

                if (eSearchGuardados != null)
                    eSearchGuardados(sender, e);

                if (eSearchCalculos != null)
                    eSearchCalculos(sender, e);

                if (eSearchObj != null)
                    eSearchObj(sender, e);

                if (eSearchReporteGral != null)
                    eSearchReporteGral(sender, e);

                

                UpdatePanel1.Update();
                UpdatePanel2.Update();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void gvCalculo_RowCommand(object sender, DevExpress.Web.ASPxGridViewRowCommandEventArgs e)
        {
            try
            {
                if (e.CommandArgs.CommandName.S() == "Ver")
                {
                    int IdPeriodo = e.CommandArgs.CommandArgument.I();
                    dtDiasViaticos = null;
                    dtAjustes = null;
                    int index = e.VisibleIndex.I();
                    string sCrewCode = gvCalculo.GetRowValues(index, "CrewCode").S();
                    string[] fieldValues = { "Piloto", "CrewCode", "FechaInicio", "FechaFin", "Estatus", "HomeBase" };
                    object obj = gvCalculo.GetRowValues(index, fieldValues);
                    object[] oB = (object[])obj;

                    if (oB.Length > 0)
                    {
                        //hdnIdBitacora.Value = iIdBitacora.S();
                        readPiloto.Text = oB[0].S();
                        readCvePiloto.Text = oB[1].S();
                        DateTime dtFecha1 = oB[2].S().Dt();
                        DateTime dtFecha2 = oB[3].S().Dt();
                        string sMesDel = GetMes(dtFecha1.Month);
                        string sMesHasta = GetMes(dtFecha2.Month);
                        string sHomeBase = oB[5].S();

                        readPeríodo.Text = "Del " + dtFecha1.Day.S() + " de " + sMesDel + " de " + dtFecha1.Year.S() + " al " + dtFecha2.Day.S() + " de " + sMesHasta + " de " + dtFecha2.Year.S();
                        hdnFechaInicio.Value = dtFecha1.ToShortDateString();
                        hdnFechaFinal.Value = dtFecha2.ToShortDateString();
                        readBase.Text = sHomeBase;

                        sParametro = oB[1].S();

                        if (eGetAdicionales != null)
                            eGetAdicionales(sender, e);

                        sCvePiloto = oB[1].S();
                        sFechaInicio = oB[2].S();
                        sFechaFinal = oB[3].S();

                        if (eSearchEstatus != null)
                            eSearchEstatus(sender, e);

                        if (eSearchExistePeriodoPic != null)
                            eSearchExistePeriodoPic(sender, e);

                        if (iEstatus <= 1 && iExistePeriodo == 0)
                            btnGuardarPeriodo.Visible = true;
                        else if (iEstatus == 1 && iExistePeriodo == 1)
                            btnGuardarPeriodo.Visible = true;
                        else if (iEstatus == 2 && iExistePeriodo == 1)
                            btnGuardarPeriodo.Visible = false;
                        else if (iEstatus == 3 && iExistePeriodo == 1)
                            btnGuardarPeriodo.Visible = false;

                        //Consulta periodo si existe muestra ajustes por piloto
                        if (eSearchPeriodo != null)
                            eSearchPeriodo(sender, e);


                        hdnIdPeriodo.Value = iIdPeriodo.S();
                        if (eSearchAjustesPiloto != null)
                            eSearchAjustesPiloto(sender, e);

                        iIdPeriodo = IdPeriodo;

                        if (eSearchViaticosGuardados != null)
                            eSearchViaticosGuardados(sender, e);

                    }
                    pnlBusqueda.Visible = false;
                    pnlVuelos.Visible = false;
                    pnlCalcularViaticos.Visible = true;
                    pnlDatosPiloto.Visible = true;
                    //upaVuelos.Update();

                }
                else if (e.CommandArgs.CommandName.S() == "Ajustes")
                {
                    iIdPeriodo = e.CommandArgs.CommandArgument.I();
                    dtAjustes = null;
                    int index = e.VisibleIndex.I();
                    string sCrewCode = gvCalculo.GetRowValues(index, "CrewCode").S();
                    string[] fieldValues = { "Piloto", "CrewCode", "FechaInicio", "FechaFin", "Estatus" };
                    object obj = gvCalculo.GetRowValues(index, fieldValues);
                    object[] oB = (object[])obj;
                    if (oB.Length > 0)
                    {
                        readPiloto.Text = oB[0].S();
                        readCvePiloto.Text = oB[1].S();
                        DateTime dtFecha1 = oB[2].S().Dt();
                        DateTime dtFecha2 = oB[3].S().Dt();
                        string sMesDel = GetMes(dtFecha1.Month);
                        string sMesHasta = GetMes(dtFecha2.Month);

                        readPeríodo.Text = "Del " + dtFecha1.Day.S() + " de " + sMesDel + " de " + dtFecha1.Year.S() + " al " + dtFecha2.Day.S() + " de " + sMesHasta + " de " + dtFecha2.Year.S();
                        hdnFechaInicio.Value = dtFecha1.ToShortDateString();
                        hdnFechaFinal.Value = dtFecha2.ToShortDateString();

                        if (eGetAdicionales != null)
                            eGetAdicionales(sender, e);

                        sCvePiloto = oB[1].S();
                        sFechaInicio = oB[2].S();
                        sFechaFinal = oB[3].S();

                        if (eSearchPeriodo != null)
                            eSearchPeriodo(sender, e);

                        if (iIdPeriodo != 0)
                        {
                            hdnIdPeriodo.Value = iIdPeriodo.S();
                            //Consulta conceptos adicionales por periodo
                            if (eSearchConAdPeriodo != null)
                                eSearchConAdPeriodo(sender, e);
                            //}

                            pnlDatosPiloto.Visible = true;
                            pnlBusqueda.Visible = false;
                            pnlVuelos.Visible = false;
                            pnlCalcularViaticos.Visible = false;
                            pnlAjuste.Visible = true;
                        }
                    }
                }
                else if (e.CommandArgs.CommandName.S() == "Reporte")
                {
                    iIdPeriodo = e.CommandArgs.CommandArgument.I();
                    int index = e.VisibleIndex.I();
                    string sCrewCode = gvCalculo.GetRowValues(index, "CrewCode").S();
                    string[] fieldValues = { "Piloto", "CrewCode", "FechaInicio", "FechaFin", "Estatus" };
                    object obj = gvCalculo.GetRowValues(index, fieldValues);
                    object[] oB = (object[])obj;
                    if (oB.Length > 0)
                    {
                        sCvePiloto = oB[1].S();
                        sFechaInicio = oB[2].S();
                        sFechaFinal = oB[3].S();

                        if (eSearchPeriodo != null)
                            eSearchPeriodo(sender, e);

                        if (iIdPeriodo != 0)
                        {
                            if (eSearchReporte != null)
                                eSearchReporte(sender, e);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gvPeriodosGuardados_RowCommand(object sender, DevExpress.Web.ASPxGridViewRowCommandEventArgs e)
        {
            try
            {
                if (e.CommandArgs.CommandName.S() == "Ver")
                {
                    dtDiasViaticos = null;
                    dtAjustes = null;
                    int index = e.VisibleIndex.I();
                    string sCrewCode = gvPeriodosGuardados.GetRowValues(index, "CrewCode").S();
                    string[] fieldValues = { "Piloto", "CrewCode", "FechaInicio", "FechaFin", "HomeBase" };
                    object obj = gvPeriodosGuardados.GetRowValues(index, fieldValues);
                    object[] oB = (object[])obj;

                    if (oB.Length > 0)
                    {
                        //hdnIdBitacora.Value = iIdBitacora.S();
                        readPiloto.Text = oB[0].S();
                        readCvePiloto.Text = oB[1].S();
                        DateTime dtFecha1 = oB[2].S().Dt();
                        DateTime dtFecha2 = oB[3].S().Dt();
                        string sMesDel = GetMes(dtFecha1.Month);
                        string sMesHasta = GetMes(dtFecha2.Month);
                        string sHomeBase = oB[4].S();

                        readPeríodo.Text = "Del " + dtFecha1.Day.S() + " de " + sMesDel + " de " + dtFecha1.Year.S() + " al " + dtFecha2.Day.S() + " de " + sMesHasta + " de " + dtFecha2.Year.S();
                        hdnFechaInicio.Value = dtFecha1.ToShortDateString();
                        hdnFechaFinal.Value = dtFecha2.ToShortDateString();
                        readBase.Text = sHomeBase;

                        sParametro = oB[1].S();

                        if (eGetAdicionales != null)
                            eGetAdicionales(sender, e);

                        sCvePiloto = oB[1].S();
                        sFechaInicio = oB[2].S();
                        sFechaFinal = oB[3].S();

                        if (eSearchEstatus != null)
                            eSearchEstatus(sender, e);

                        if (eSearchExistePeriodoPic != null)
                            eSearchExistePeriodoPic(sender, e);

                        if (iEstatus <= 1 && iExistePeriodo == 0)
                            btnGuardarPeriodo.Visible = true;
                        else if (iEstatus == 1 && iExistePeriodo == 1)
                            btnGuardarPeriodo.Visible = true;
                        else if (iEstatus == 2 && iExistePeriodo == 1)
                            btnGuardarPeriodo.Visible = false;
                        else if (iEstatus == 3 && iExistePeriodo == 1)
                            btnGuardarPeriodo.Visible = false;

                        //Consulta periodo si existe muestra ajustes por piloto
                        if (eSearchPeriodo != null)
                            eSearchPeriodo(sender, e);


                        hdnIdPeriodo.Value = iIdPeriodo.S();
                        if (eSearchAjustesPiloto != null)
                            eSearchAjustesPiloto(sender, e);

                        //-----------------------------------------------------


                        pnlDatosPiloto.Visible = true;
                        pnlBusqueda.Visible = false;
                        pnlVuelos.Visible = false;
                        pnlCalcularViaticos.Visible = true;
                        //upaVuelos.Update();

                        if (eSearchCalculos != null)
                            eSearchCalculos(sender, e);

                        if (dtCalculos != null && dtCalculos.Rows.Count > 0)
                        {
                            CargaViaticos();
                        }
                    }

                }
                //else if (e.CommandArgs.CommandName.S() == "Ajustes")
                //{
                //    dtAjustes = null;
                //    int index = e.VisibleIndex.I();
                //    string sCrewCode = gvPeriodosGuardados.GetRowValues(index, "CrewCode").S();
                //    string[] fieldValues = { "Piloto", "CrewCode", "FechaInicio", "FechaFin", "Estatus" };
                //    object obj = gvPeriodosGuardados.GetRowValues(index, fieldValues);
                //    object[] oB = (object[])obj;
                //    if (oB.Length > 0)
                //    {
                //        readPiloto.Text = oB[0].S();
                //        readCvePiloto.Text = oB[1].S();
                //        DateTime dtFecha1 = oB[2].S().Dt();
                //        DateTime dtFecha2 = oB[3].S().Dt();
                //        string sMesDel = GetMes(dtFecha1.Month);
                //        string sMesHasta = GetMes(dtFecha2.Month);

                //        readPeríodo.Text = "Del " + dtFecha1.Day.S() + " de " + sMesDel + " de " + dtFecha1.Year.S() + " al " + dtFecha2.Day.S() + " de " + sMesHasta + " de " + dtFecha2.Year.S();
                //        hdnFechaInicio.Value = dtFecha1.ToShortDateString();
                //        hdnFechaFinal.Value = dtFecha2.ToShortDateString();

                //        if (eGetAdicionales != null)
                //            eGetAdicionales(sender, e);

                //        sCvePiloto = oB[1].S();
                //        sFechaInicio = oB[2].S();
                //        sFechaFinal = oB[3].S();

                //        if (eSearchPeriodo != null)
                //            eSearchPeriodo(sender, e);

                //        if (iIdPeriodo != 0)
                //        {
                //            hdnIdPeriodo.Value = iIdPeriodo.S();
                //            //Consulta conceptos adicionales por periodo
                //            if (eSearchConAdPeriodo != null)
                //                eSearchConAdPeriodo(sender, e);
                //            //}

                //            pnlDatosPiloto.Visible = true;
                //            pnlBusqueda.Visible = false;
                //            pnlVuelos.Visible = false;
                //            pnlCalcularViaticos.Visible = false;
                //            pnlAjuste.Visible = true;
                //        }
                //    }
                //}
                //else if (e.CommandArgs.CommandName.S() == "Reporte")
                //{
                //    int index = e.VisibleIndex.I();
                //    string sCrewCode = gvPeriodosGuardados.GetRowValues(index, "CrewCode").S();
                //    string[] fieldValues = { "Piloto", "CrewCode", "FechaInicio", "FechaFin", "Estatus" };
                //    object obj = gvPeriodosGuardados.GetRowValues(index, fieldValues);
                //    object[] oB = (object[])obj;
                //    if (oB.Length > 0)
                //    {
                //        sCvePiloto = oB[1].S();
                //        sFechaInicio = oB[2].S();
                //        sFechaFinal = oB[3].S();

                //        if (eSearchPeriodo != null)
                //            eSearchPeriodo(sender, e);

                //        if (iIdPeriodo != 0)
                //        {
                //            if (eSearchReporte != null)
                //                eSearchReporte(sender, e);
                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnGuardarPeriodo_Click(object sender, EventArgs e)
        {
            try
            {
                List<ConceptosPiloto> oLs = new List<ConceptosPiloto>();
                List<VuelosPiernasPiloto> oLsV = new List<VuelosPiernasPiloto>();

                List<HotelesPiloto> oLsH = new List<HotelesPiloto>();
                DataTable dtHoteles = new DataTable();

                if (dsParamsH != null && dsParamsH.Tables.Count > 0)
                {
                    dtHoteles = dsParamsH.Tables[0];
                }



                for (int i = 0; i < dtNal.Rows.Count; i++)
                {
                    ConceptosPiloto oP = new ConceptosPiloto();
                    if (dtNal.Rows[i]["CONCEPTO"].S().ToUpper() != "TOTAL")
                    {
                        oP.IIdPeriodo = 0;
                        int iIdConcepto = 0;
                        decimal dMonto = 0;

                        for (int x = 0; x < dtConceptos.Rows.Count; x++)
                        {
                            if (dtConceptos.Rows[x]["DesConcepto"].S().ToUpper() == dtNal.Rows[i]["CONCEPTO"].S().ToUpper())
                            {
                                iIdConcepto = dtConceptos.Rows[x]["IdConcepto"].S().I();
                                dMonto = dtConceptos.Rows[x]["MontoMXN"].S().Replace(" MXN", "").D();
                                break;
                            }
                        }
                        oP.IIdConcepto = iIdConcepto;
                        oP.SDesConcepto = dtNal.Rows[i]["CONCEPTO"].S();
                        oP.ICantidad = dtNal.Rows[i]["NACIONAL"].S().I();
                        oP.DMontoConcepto = dMonto;
                        oP.SMoneda = "MXN";
                        oP.DTotal = dtNal.Rows[i]["NACIONAL"].S().I() * dMonto;
                        oLs.Add(oP);
                    }
                }

                for (int i = 0; i < dtInt.Rows.Count; i++)
                {
                    ConceptosPiloto oP = new ConceptosPiloto();
                    if (dtInt.Rows[i]["CONCEPTO"].S().ToUpper() != "TOTAL")
                    {
                        oP.IIdPeriodo = 0;
                        int iIdConcepto = 0;
                        decimal dMonto = 0;

                        for (int x = 0; x < dtConceptos.Rows.Count; x++)
                        {
                            if (dtConceptos.Rows[x]["DesConcepto"].S().ToUpper() == dtInt.Rows[i]["CONCEPTO"].S().ToUpper())
                            {
                                iIdConcepto = dtConceptos.Rows[x]["IdConcepto"].S().I();
                                dMonto = dtConceptos.Rows[x]["MontoUSD"].S().Replace(" USD", "").D();
                                break;
                            }
                        }
                        oP.IIdConcepto = iIdConcepto;
                        oP.SDesConcepto = dtInt.Rows[i]["CONCEPTO"].S();
                        oP.ICantidad = dtInt.Rows[i]["INTERNACIONAL"].S().I();
                        oP.DMontoConcepto = dMonto;
                        oP.SMoneda = "USD";
                        oP.DTotal = dtInt.Rows[i]["INTERNACIONAL"].S().I() * dMonto;
                        oLs.Add(oP);
                    }
                }
                //Enlistamos los conceptos nacionales e internacionales del calculo del piloto
                oLst = oLs;

                for (int i = 0; i < dtVuelosPiloto.Rows.Count; i++)
                {
                    VuelosPiernasPiloto oV = new VuelosPiernasPiloto();
                    oV.IIdPeriodo = 0;
                    oV.LTrip = dtVuelosPiloto.Rows[i]["Trip"].S().L();
                    oV.LLegId = dtVuelosPiloto.Rows[i]["LegId"].S().L();
                    oLsV.Add(oV);
                }
                oLstVP = oLsV;



                //Enlistamos viaticos por dia
                List<ConceptosViaticosPorDia> oLsViPorDia = new List<ConceptosViaticosPorDia>();
                for (int i = 0; i < dtViaticosDiaInsert.Rows.Count; i++)
                {
                    ConceptosViaticosPorDia oCD = new ConceptosViaticosPorDia();
                    oCD.SMoneda = dtViaticosDiaInsert.Rows[i]["Moneda"].S();
                    oCD.DtFecha = dtViaticosDiaInsert.Rows[i]["Fecha"].S().Dt();
                    oCD.DDesayuno = dtViaticosDiaInsert.Rows[i]["Desayuno"].S().Replace("$", "").Replace(" MXN", "").Replace(" USD", "").D();
                    oCD.DComida = dtViaticosDiaInsert.Rows[i]["Comida"].S().Replace("$", "").Replace(" MXN", "").Replace(" USD", "").D();
                    oCD.DCena = dtViaticosDiaInsert.Rows[i]["Cena"].S().Replace("$", "").Replace(" MXN", "").Replace(" USD", "").D();
                    oCD.DTotal = dtViaticosDiaInsert.Rows[i]["Total"].S().Replace("$", "").Replace(" MXN", "").Replace(" USD", "").D();

                    if (dtViaticosDiaInsert.Rows[i]["Moneda"].S() == "USD")
                        oCD.DTipoCambio = new DBCalculoPagos().ObtenerTipoCambio(dtViaticosDiaInsert.Rows[i]["Fecha"].S());
                    else
                        oCD.DTipoCambio = 0;

                    if (dtViaticosDiaInsert.Rows[i]["Moneda"].S() == "MXN")
                    {
                        oCD.DDesNac = dtViaticosDiaInsert.Rows[i]["Desayuno"].S().Replace("$", "").Replace(" MXN", "").Replace(" USD", "").D();
                        oCD.DComNac = dtViaticosDiaInsert.Rows[i]["Comida"].S().Replace("$", "").Replace(" MXN", "").Replace(" USD", "").D();
                        oCD.DCenNac = dtViaticosDiaInsert.Rows[i]["Cena"].S().Replace("$", "").Replace(" MXN", "").Replace(" USD", "").D();
                        //oCD.DDesInt = 0;
                        //oCD.DComInt = 0;
                        //oCD.DCenInt = 0;
                    }

                    if (dtViaticosDiaInsert.Rows[i]["Moneda"].S() == "USD")
                    {
                        oCD.DDesInt = dtViaticosDiaInsert.Rows[i]["Desayuno"].S().Replace("$", "").Replace(" MXN", "").Replace(" USD", "").D();
                        oCD.DComInt = dtViaticosDiaInsert.Rows[i]["Comida"].S().Replace("$", "").Replace(" MXN", "").Replace(" USD", "").D();
                        oCD.DCenInt = dtViaticosDiaInsert.Rows[i]["Cena"].S().Replace("$", "").Replace(" MXN", "").Replace(" USD", "").D();
                        //oCD.DDesNac = 0;
                        //oCD.DComNac = 0;
                        //oCD.DCenNac = 0;
                    }

                    oLsViPorDia.Add(oCD);
                }
                oLstPorDia = oLsViPorDia;


                #region VIATICOS DE HOTEL y VIATICOS DE HOTEL POR DÍA
                for (int i = 0; i < dtHotNal.Rows.Count; i++)
                {
                    HotelesPiloto oP = new HotelesPiloto();
                    if (dtHotNal.Rows[i]["CONCEPTO"].S().ToUpper() != "TOTAL")
                    {
                        oP.IIdPeriodo = 0;
                        int iIdHotel = 0;
                        decimal dMonto = 0;

                        if (dtHoteles != null && dtHoteles.Rows.Count > 0)
                        {
                            for (int x = 0; x < dtHoteles.Rows.Count; x++)
                            {
                                if (dtHoteles.Rows[x]["DesHotel"].S().ToUpper() == dtHotNal.Rows[i]["CONCEPTO"].S().ToUpper())
                                {
                                    iIdHotel = dtHoteles.Rows[x]["IdHotel"].S().I();
                                    dMonto = dtHoteles.Rows[x]["MontoMXN"].S().Replace(" MXN", "").D();
                                    break;
                                }
                            }
                        }
                        oP.IIdHotel = iIdHotel;
                        oP.SDesHotel = dtHotNal.Rows[i]["CONCEPTO"].S();
                        oP.ICantidad = dtHotNal.Rows[i]["NACIONAL"].S().I();
                        oP.DMontoHotel = dMonto;
                        oP.SMoneda = "MXN";
                        oP.DTotal = dtHotNal.Rows[i]["NACIONAL"].S().I() * dMonto;
                        oLsH.Add(oP);
                    }
                }

                for (int i = 0; i < dtHotInt.Rows.Count; i++)
                {
                    HotelesPiloto oP = new HotelesPiloto();
                    if (dtHotInt.Rows[i]["CONCEPTO"].S().ToUpper() != "TOTAL")
                    {
                        oP.IIdPeriodo = 0;
                        int iIdHotel = 0;
                        decimal dMonto = 0;

                        if (dtHoteles != null && dtHoteles.Rows.Count > 0)
                        {
                            for (int x = 0; x < dtHoteles.Rows.Count; x++)
                            {
                                if (dtHoteles.Rows[x]["DesHotel"].S().ToUpper() == dtHotInt.Rows[i]["CONCEPTO"].S().ToUpper())
                                {
                                    iIdHotel = dtHoteles.Rows[x]["IdHotel"].S().I();
                                    dMonto = dtHoteles.Rows[x]["MontoUSD"].S().Replace(" USD", "").D();
                                    break;
                                }
                            }
                        }
                        oP.IIdHotel = iIdHotel;
                        oP.SDesHotel = dtHotInt.Rows[i]["CONCEPTO"].S();
                        oP.ICantidad = dtHotInt.Rows[i]["INTERNACIONAL"].S().I();
                        oP.DMontoHotel = dMonto;
                        oP.SMoneda = "USD";
                        oP.DTotal = dtHotInt.Rows[i]["INTERNACIONAL"].S().I() * dMonto;
                        oLsH.Add(oP);
                    }
                }
                //Enlistamos los hoteles nacionales e internacionales del calculo del piloto
                oLstHP = oLsH;

                //Viaticos de hotel por día
                List<ViaticosHotelPorDia> oLsViHPorDia = new List<ViaticosHotelPorDia>();
                for (int i = 0; i < dtViaticosHotelDiaInsert.Rows.Count; i++)
                {
                    ViaticosHotelPorDia oHD = new ViaticosHotelPorDia();
                    oHD.SMoneda = dtViaticosHotelDiaInsert.Rows[i]["Moneda"].S();
                    oHD.DtFecha = dtViaticosHotelDiaInsert.Rows[i]["Fecha"].S().Dt();
                    oHD.DHotNac = dtViaticosHotelDiaInsert.Rows[i]["HotNac"].S().Replace("$", "").Replace(" MXN", "").Replace(" USD", "").D();
                    oHD.DHotInt = dtViaticosHotelDiaInsert.Rows[i]["HotInt"].S().Replace("$", "").Replace(" MXN", "").Replace(" USD", "").D();
                    oHD.DTotal = dtViaticosHotelDiaInsert.Rows[i]["Total"].S().Replace("$", "").Replace(" MXN", "").Replace(" USD", "").D();

                    if (dtViaticosHotelDiaInsert.Rows[i]["Moneda"].S() == "USD")
                        oHD.DTipoCambio = new DBCalculoPagos().ObtenerTipoCambio(dtViaticosHotelDiaInsert.Rows[i]["Fecha"].S());
                    else
                        oHD.DTipoCambio = 0;

                    //if (dtViaticosHotelDiaInsert.Rows[i]["Moneda"].S() == "MXN")
                    //{
                    //    oHD.DDesNac = dtViaticosHotelDiaInsert.Rows[i]["Desayuno"].S().Replace("$", "").Replace(" MXN", "").Replace(" USD", "").D();
                    //    oHD.DComNac = dtViaticosHotelDiaInsert.Rows[i]["Comida"].S().Replace("$", "").Replace(" MXN", "").Replace(" USD", "").D();
                    //    oHD.DCenNac = dtViaticosHotelDiaInsert.Rows[i]["Cena"].S().Replace("$", "").Replace(" MXN", "").Replace(" USD", "").D();
                    //}

                    //if (dtViaticosHotelDiaInsert.Rows[i]["Moneda"].S() == "USD")
                    //{
                    //    oHD.DDesInt = dtViaticosHotelDiaInsert.Rows[i]["Desayuno"].S().Replace("$", "").Replace(" MXN", "").Replace(" USD", "").D();
                    //    oHD.DComInt = dtViaticosHotelDiaInsert.Rows[i]["Comida"].S().Replace("$", "").Replace(" MXN", "").Replace(" USD", "").D();
                    //    oHD.DCenInt = dtViaticosHotelDiaInsert.Rows[i]["Cena"].S().Replace("$", "").Replace(" MXN", "").Replace(" USD", "").D();
                    //}

                    oLsViHPorDia.Add(oHD);
                }
                oLstHotelPorDia = oLsViHPorDia;


                #endregion


                if (eSaveObj != null)
                    eSaveObj(sender, e);

                if (sOk == "ok")
                {
                    btnCancelar_Click(sender, e);
                    MostrarMensaje("¡Se ha guardado los datos calculados del piloto!", "Guardado");
                    btnBuscar_Click(sender, e);
                }
                else if (sOk == "error")
                {
                    MostrarMensaje("Ocurrió un error, favor de verificar", "Error");
                }


            }
            catch (Exception ex)
            {
                MostrarMensaje("Error: " + ex.Message, "Error");
            }
        }
        protected void btnAprobar_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < dtCalculos1X1.Rows.Count; i++)
                {
                    oLstCon = null;
                    oLstVPil = null;
                    oLstPorDiaPil = null;
                    dtDiasViaticos = null;

                    #region Llena Periodo
                    //List<PeriodoPiloto> oLsPer = new List<PeriodoPiloto>();
                    PeriodoPiloto oP = new PeriodoPiloto();
                    oP.SCvePiloto = dtCalculos1X1.Rows[i]["CrewCode"].S();
                    oP.SFechaInicio = date1.Text; //dtCalculos1X1.Rows[i]["FechaInicio"].S();
                    oP.SFechaFinal = date2.Text; //dtCalculos1X1.Rows[i]["FechaFin"].S();
                    oP.SUsuario = ((UserIdentity)Session["UserIdentity"]).sUsuario;
                    oPe = oP;
                    #endregion

                    #region Llena Conceptos
                    List<ConceptosPilotoSave> oLsConPiloto = new List<ConceptosPilotoSave>();
                    int iDesNal = 0;
                    int iComNal = 0;
                    int iCenNal = 0;
                    int iDesInt = 0;
                    int iComInt = 0;
                    int iCenInt = 0;

                    iDesNal = dtCalculos1X1.Rows[i]["DesayunosNal"].S().I();
                    iComNal = dtCalculos1X1.Rows[i]["ComidasNal"].S().I();
                    iCenNal = dtCalculos1X1.Rows[i]["CenasNal"].S().I();
                    iDesInt = dtCalculos1X1.Rows[i]["DesayunosInt"].S().I();
                    iComInt = dtCalculos1X1.Rows[i]["ComidasInt"].S().I();
                    iCenInt = dtCalculos1X1.Rows[i]["CenasInt"].S().I();

                    //Conceptos
                    #region NACIONALES
                    for (int x = 0; x < dtConceptos.Rows.Count; x++)
                    {
                        ConceptosPilotoSave oCP = new ConceptosPilotoSave();
                        oCP.IdPeriodo = 0;
                        int iIdConcepto = 0;
                        decimal dMonto = 0;

                        if (dtConceptos.Rows[x]["DesConcepto"].S().ToUpper() == "DESAYUNO")
                        {
                            iIdConcepto = dtConceptos.Rows[x]["IdConcepto"].S().I();
                            dMonto = dtConceptos.Rows[x]["MontoMXN"].S().Replace(" MXN", "").D();

                            oCP.IdConcepto = iIdConcepto;
                            oCP.DesConcepto = dtConceptos.Rows[x]["DesConcepto"].S();
                            oCP.Cantidad = iDesNal;
                            oCP.MontoConcepto = dMonto;
                            oCP.Moneda = "MXN";
                            oCP.Total = iDesNal * dMonto;
                            oCP.Estatus = 1;
                            oLsConPiloto.Add(oCP);
                        }
                        else if (dtConceptos.Rows[x]["DesConcepto"].S().ToUpper() == "COMIDA")
                        {
                            iIdConcepto = dtConceptos.Rows[x]["IdConcepto"].S().I();
                            dMonto = dtConceptos.Rows[x]["MontoMXN"].S().Replace(" MXN", "").D();

                            oCP.IdConcepto = iIdConcepto;
                            oCP.DesConcepto = dtConceptos.Rows[x]["DesConcepto"].S();
                            oCP.Cantidad = iComNal;
                            oCP.MontoConcepto = dMonto;
                            oCP.Moneda = "MXN";
                            oCP.Total = iComNal * dMonto;
                            oCP.Estatus = 1;
                            oLsConPiloto.Add(oCP);
                        }
                        else if (dtConceptos.Rows[x]["DesConcepto"].S().ToUpper() == "CENA")
                        {
                            iIdConcepto = dtConceptos.Rows[x]["IdConcepto"].S().I();
                            dMonto = dtConceptos.Rows[x]["MontoMXN"].S().Replace(" MXN", "").D();

                            oCP.IdConcepto = iIdConcepto;
                            oCP.DesConcepto = dtConceptos.Rows[x]["DesConcepto"].S();
                            oCP.Cantidad = iCenNal;
                            oCP.MontoConcepto = dMonto;
                            oCP.Moneda = "MXN";
                            oCP.Total = iCenNal * dMonto;
                            oCP.Estatus = 1;
                            oLsConPiloto.Add(oCP);
                        }
                    }
                    #endregion

                    #region INTERNACIONALES
                    for (int x = 0; x < dtConceptos.Rows.Count; x++)
                    {
                        ConceptosPilotoSave oCP = new ConceptosPilotoSave();
                        oCP.IdPeriodo = 0;
                        int iIdConcepto = 0;
                        decimal dMonto = 0;

                        if (dtConceptos.Rows[x]["DesConcepto"].S().ToUpper() == "DESAYUNO")
                        {
                            iIdConcepto = dtConceptos.Rows[x]["IdConcepto"].S().I();
                            dMonto = dtConceptos.Rows[x]["MontoUSD"].S().Replace(" USD", "").D();

                            oCP.IdConcepto = iIdConcepto;
                            oCP.DesConcepto = dtConceptos.Rows[x]["DesConcepto"].S();
                            oCP.Cantidad = iDesInt;
                            oCP.MontoConcepto = dMonto;
                            oCP.Moneda = "USD";
                            oCP.Total = iDesInt * dMonto;
                            oCP.Estatus = 1;
                            oLsConPiloto.Add(oCP);
                        }
                        else if (dtConceptos.Rows[x]["DesConcepto"].S().ToUpper() == "COMIDA")
                        {
                            iIdConcepto = dtConceptos.Rows[x]["IdConcepto"].S().I();
                            dMonto = dtConceptos.Rows[x]["MontoUSD"].S().Replace(" USD", "").D();

                            oCP.IdConcepto = iIdConcepto;
                            oCP.DesConcepto = dtConceptos.Rows[x]["DesConcepto"].S();
                            oCP.Cantidad = iComInt;
                            oCP.MontoConcepto = dMonto;
                            oCP.Moneda = "USD";
                            oCP.Total = iComInt * dMonto;
                            oCP.Estatus = 1;
                            oLsConPiloto.Add(oCP);
                        }
                        else if (dtConceptos.Rows[x]["DesConcepto"].S().ToUpper() == "CENA")
                        {
                            iIdConcepto = dtConceptos.Rows[x]["IdConcepto"].S().I();
                            dMonto = dtConceptos.Rows[x]["MontoUSD"].S().Replace(" USD", "").D();

                            oCP.IdConcepto = iIdConcepto;
                            oCP.DesConcepto = dtConceptos.Rows[x]["DesConcepto"].S();
                            oCP.Cantidad = iCenInt;
                            oCP.MontoConcepto = dMonto;
                            oCP.Moneda = "USD";
                            oCP.Total = iCenInt * dMonto;
                            oCP.Estatus = 1;
                            oLsConPiloto.Add(oCP);
                        }
                    }
                    #endregion

                    //Llena conceptos piloto
                    oLstCon = oLsConPiloto;

                    #endregion

                    #region Llena Vuelos
                    List<VuelosPiernasPilotoSave> oLsVuelos = new List<VuelosPiernasPilotoSave>();

                    DataRow[] dr = dtVuelosPiloto1x1.Select("crewcode='" + dtCalculos1X1.Rows[i]["CrewCode"].S() + "'");

                    for (int x = 0; x < dr.Length; x++)
                    {
                        VuelosPiernasPilotoSave oV = new VuelosPiernasPilotoSave();
                        oV.IdPeriodo = 0;
                        oV.Trip = dr[x]["Trip"].S().L();
                        oV.LegId = dr[x]["LegId"].S().L();
                        oLsVuelos.Add(oV);
                    }
                    oLstVPil = oLsVuelos;

                    #endregion

                    #region Llena Viaticos por día
                    sParametro = string.Empty;
                    sFechaInicio = string.Empty;
                    sFechaFinal = string.Empty;

                    sParametro = dtCalculos1X1.Rows[i]["CrewCode"].S();
                    sFechaInicio = dtCalculos1X1.Rows[i]["FechaInicio"].S();
                    sFechaFinal = dtCalculos1X1.Rows[i]["FechaFin"].S();

                    if (eSearchCalculos != null)
                        eSearchCalculos(sender, e);

                    if (dtDiasViaticos != null && dtDiasViaticos.Rows.Count > 0)
                    {
                        //Se genera el listado de viaticos
                        CargaViaticos2();
                        //Se llena la lista de viaticos por día
                    }

                    if (dtViaticosHot != null && dtViaticosHot.Rows.Count > 0)
                    {
                        //Se genera el listado de viaticos de hotel
                        CargaViaticosHotel();
                        //Se llena la lista de viaticos de hotel por día
                    }

                    List<ConceptosViaticosPorDiaSave> oLsViPorDia = new List<ConceptosViaticosPorDiaSave>();
                    if (dtViaticosDiaInsert != null && dtViaticosDiaInsert.Rows.Count > 0)
                    {
                        for (int b = 0; b < dtViaticosDiaInsert.Rows.Count; b++)
                        {
                            ConceptosViaticosPorDiaSave oCD = new ConceptosViaticosPorDiaSave();
                            oCD.Moneda = dtViaticosDiaInsert.Rows[b]["Moneda"].S();
                            oCD.Fecha = dtViaticosDiaInsert.Rows[b]["Fecha"].S().Dt();
                            oCD.Desayuno = dtViaticosDiaInsert.Rows[b]["Desayuno"].S().Replace("$", "").Replace(" MXN", "").Replace(" USD", "").D();
                            oCD.Comida = dtViaticosDiaInsert.Rows[b]["Comida"].S().Replace("$", "").Replace(" MXN", "").Replace(" USD", "").D();
                            oCD.Cena = dtViaticosDiaInsert.Rows[b]["Cena"].S().Replace("$", "").Replace(" MXN", "").Replace(" USD", "").D();
                            oCD.Total = dtViaticosDiaInsert.Rows[b]["Total"].S().Replace("$", "").Replace(" MXN", "").Replace(" USD", "").D();

                            if (dtViaticosDiaInsert.Rows[b]["Moneda"].S() == "USD")
                                oCD.TipoCambio = new DBCalculoPagos().ObtenerTipoCambio(dtViaticosDiaInsert.Rows[b]["Fecha"].S());
                            else
                                oCD.TipoCambio = 0;

                            if (dtViaticosDiaInsert.Rows[b]["Moneda"].S() == "MXN")
                            {
                                oCD.DesNac = dtViaticosDiaInsert.Rows[b]["Desayuno"].S().Replace("$", "").Replace(" MXN", "").Replace(" USD", "").D();
                                oCD.ComNac = dtViaticosDiaInsert.Rows[b]["Comida"].S().Replace("$", "").Replace(" MXN", "").Replace(" USD", "").D();
                                oCD.CenNac = dtViaticosDiaInsert.Rows[b]["Cena"].S().Replace("$", "").Replace(" MXN", "").Replace(" USD", "").D();
                            }

                            if (dtViaticosDiaInsert.Rows[b]["Moneda"].S() == "USD")
                            {
                                oCD.DesInt = dtViaticosDiaInsert.Rows[b]["Desayuno"].S().Replace("$", "").Replace(" MXN", "").Replace(" USD", "").D();
                                oCD.ComInt = dtViaticosDiaInsert.Rows[b]["Comida"].S().Replace("$", "").Replace(" MXN", "").Replace(" USD", "").D();
                                oCD.CenInt = dtViaticosDiaInsert.Rows[b]["Cena"].S().Replace("$", "").Replace(" MXN", "").Replace(" USD", "").D();
                            }
                            oCD.Estatus = 1;
                            oLsViPorDia.Add(oCD);
                        }
                    }
                    oLstPorDiaPil = oLsViPorDia;

                    #endregion


                    #region Llena Hoteles
                    List<HotelesPilotoSave> oLsHotPiloto = new List<HotelesPilotoSave>();
                    int iHotelNal = 0;
                    int iHotelInt = 0;
                    oLstHPS = null;

                    iHotelNal = dtCalculos1X1.Rows[i]["HotelesNac"].S().I();
                    iHotelInt = dtCalculos1X1.Rows[i]["HotelesInt"].S().I();

                    #region HOTELES NACIONALES
                    for (int x = 0; x < dsParamsH.Tables[0].Rows.Count; x++)
                    {
                        HotelesPilotoSave oHP = new HotelesPilotoSave();
                        oHP.IdPeriodo = 0;
                        int iIdHotel = 0;
                        decimal dMonto = 0;

                        if (dsParamsH.Tables[0].Rows[x]["DesHotel"].S().ToUpper() == "HOTEL")
                        {
                            iIdHotel = dsParamsH.Tables[0].Rows[x]["IdHotel"].S().I();
                            dMonto = dsParamsH.Tables[0].Rows[x]["MontoMXN"].S().Replace(" MXN", "").D();

                            oHP.IdHotel = iIdHotel;
                            oHP.DesHotel = dsParamsH.Tables[0].Rows[x]["DesHotel"].S();
                            oHP.Cantidad = iHotelNal;
                            oHP.MontoHotel = dMonto;
                            oHP.Moneda = "MXN";
                            oHP.Total = iHotelNal * dMonto;
                            oHP.Estatus = 1;
                            oLsHotPiloto.Add(oHP);
                        }
                        else if (dsParamsH.Tables[0].Rows[x]["DesHotel"].S().ToUpper() == "HOTEL ESPECIAL")
                        {
                            iIdHotel = dsParamsH.Tables[0].Rows[x]["IdHotel"].S().I();
                            dMonto = dsParamsH.Tables[0].Rows[x]["MontoMXN"].S().Replace(" MXN", "").D();

                            oHP.IdHotel = iIdHotel;
                            oHP.DesHotel = dsParamsH.Tables[0].Rows[x]["DesHotel"].S();
                            oHP.Cantidad = iHotelNal;
                            oHP.MontoHotel = dMonto;
                            oHP.Moneda = "MXN";
                            oHP.Total = iHotelNal * dMonto;
                            oHP.Estatus = 1;
                            oLsHotPiloto.Add(oHP);
                        }
                    }
                    #endregion

                    #region HOTELES INTERNACIONALES
                    for (int x = 0; x < dsParamsH.Tables[0].Rows.Count; x++)
                    {
                        HotelesPilotoSave oHP = new HotelesPilotoSave();
                        oHP.IdPeriodo = 0;
                        int iIdHotel = 0;
                        decimal dMonto = 0;

                        if (dsParamsH.Tables[0].Rows[x]["DesHotel"].S().ToUpper() == "HOTEL")
                        {
                            iIdHotel = dsParamsH.Tables[0].Rows[x]["IdHotel"].S().I();
                            dMonto = dsParamsH.Tables[0].Rows[x]["MontoUSD"].S().Replace(" USD", "").D();

                            oHP.IdHotel = iIdHotel;
                            oHP.DesHotel = dsParamsH.Tables[0].Rows[x]["DesHotel"].S();
                            oHP.Cantidad = iHotelInt;
                            oHP.MontoHotel = dMonto;
                            oHP.Moneda = "USD";
                            oHP.Total = iHotelInt * dMonto;
                            oHP.Estatus = 1;
                            oLsHotPiloto.Add(oHP);
                        }
                        else if (dsParamsH.Tables[0].Rows[x]["DesHotel"].S().ToUpper() == "HOTEL ESPECIAL")
                        {
                            iIdHotel = dsParamsH.Tables[0].Rows[x]["IdHotel"].S().I();
                            dMonto = dsParamsH.Tables[0].Rows[x]["MontoUSD"].S().Replace(" USD", "").D();

                            oHP.IdHotel = iIdHotel;
                            oHP.DesHotel = dsParamsH.Tables[0].Rows[x]["DesHotel"].S();
                            oHP.Cantidad = iHotelInt;
                            oHP.MontoHotel = dMonto;
                            oHP.Moneda = "USD";
                            oHP.Total = iHotelInt * dMonto;
                            oHP.Estatus = 1;
                            oLsHotPiloto.Add(oHP);
                        }
                        
                    }
                    oLstHPS = oLsHotPiloto;
                    #endregion


                    #endregion

                    #region Llena Hoteles de piloto por día
                    List<ViaticosHotelPorDiaSave> oLsViHotPorDia = new List<ViaticosHotelPorDiaSave>();
                    if (dtViaticosHotelDiaInsert != null && dtViaticosHotelDiaInsert.Rows.Count > 0)
                    {
                        for (int b = 0; b < dtViaticosHotelDiaInsert.Rows.Count; b++)
                        {
                            ViaticosHotelPorDiaSave oHD = new ViaticosHotelPorDiaSave();
                            oHD.SMoneda = dtViaticosHotelDiaInsert.Rows[b]["Moneda"].S();
                            oHD.DtFecha = dtViaticosHotelDiaInsert.Rows[b]["Fecha"].S().Dt();
                            oHD.DHotNac = dtViaticosHotelDiaInsert.Rows[b]["HotNac"].S().Replace("$", "").Replace(" MXN", "").Replace(" USD", "").D();
                            oHD.DHotInt = dtViaticosHotelDiaInsert.Rows[b]["HotInt"].S().Replace("$", "").Replace(" MXN", "").Replace(" USD", "").D();

                            oHD.DTotal = dtViaticosHotelDiaInsert.Rows[b]["Total"].S().Replace("$", "").Replace(" MXN", "").Replace(" USD", "").D();

                            if (dtViaticosHotelDiaInsert.Rows[b]["Moneda"].S() == "USD")
                                oHD.DTipoCambio = new DBCalculoPagos().ObtenerTipoCambio(dtViaticosHotelDiaInsert.Rows[b]["Fecha"].S());
                            else
                                oHD.DTipoCambio = 0;

                            //if (dtViaticosHotelDiaInsert.Rows[b]["Moneda"].S() == "MXN")
                            //{
                            //    oHD.DesNac = dtViaticosHotelDiaInsert.Rows[b]["Desayuno"].S().Replace("$", "").Replace(" MXN", "").Replace(" USD", "").D();
                            //    oHD.ComNac = dtViaticosHotelDiaInsert.Rows[b]["Comida"].S().Replace("$", "").Replace(" MXN", "").Replace(" USD", "").D();
                            //    oHD.CenNac = dtViaticosHotelDiaInsert.Rows[b]["Cena"].S().Replace("$", "").Replace(" MXN", "").Replace(" USD", "").D();
                            //}

                            //if (dtViaticosHotelDiaInsert.Rows[b]["Moneda"].S() == "USD")
                            //{
                            //    oHD.DesInt = dtViaticosHotelDiaInsert.Rows[b]["Desayuno"].S().Replace("$", "").Replace(" MXN", "").Replace(" USD", "").D();
                            //    oHD.ComInt = dtViaticosHotelDiaInsert.Rows[b]["Comida"].S().Replace("$", "").Replace(" MXN", "").Replace(" USD", "").D();
                            //    oHD.CenInt = dtViaticosHotelDiaInsert.Rows[b]["Cena"].S().Replace("$", "").Replace(" MXN", "").Replace(" USD", "").D();
                            //}

                            oLsViHotPorDia.Add(oHD);
                        }
                    }
                    oLstHotelPorDiaSave = oLsViHotPorDia;
                    #endregion

                    if (eSavePeriodos != null)
                        eSavePeriodos(sender, e);
                    
                    //Vaciar listas
                    oLstCon = null;
                    oLstVPil = null;
                    oLstPorDiaPil = null;

                    oLstHPS = null;
                    oLstHotelPorDiaSave = null;
                    //GC.Collect();
                }

                if (sOk == "ok")
                {
                    btnCancelar_Click(sender, e);
                    MostrarMensaje("¡Se han guardado los datos calculados de los periodos de la búsqueda!", "Guardado");
                    //btnBuscar_Click(sender, e);
                    //btnExportar.Enabled = true;
                }
                else if (sOk == "error")
                {
                    MostrarMensaje("Ocurrió un error, favor de verificar", "Error");
                }
 
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error: " + ex.Message, "Aviso");
            }
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                btnBuscar_Click(sender, e);
                pnlBusqueda.Visible = true;
                pnlVuelos.Visible = true;
                pnlCalcularViaticos.Visible = false;
                pnlDatosPiloto.Visible = false;
                hdnIdPeriodo.Value = "0";
                btnBuscar_Click(sender, e);
                //gvPeriodosGuardados.DataSource = dtCalculos1X1;
                //gvPeriodosGuardados.DataBind();
                //GC.Collect();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void gvCalculo_HtmlDataCellPrepared(object sender, DevExpress.Web.Bootstrap.BootstrapGridViewTableDataCellEventArgs e)
        {
            try
            {
                if (e.DataColumn.FieldName == "Estatus")
                {

                    BootstrapGridView gvB = (BootstrapGridView)(sender as BootstrapGridView);
                    Label rdEstatus = gvB.FindRowCellTemplateControl(e.VisibleIndex, e.DataColumn, "readEstatus") as Label;

                    string[] fieldValues = { "Piloto", "CrewCode", "FechaInicio", "FechaFin" };
                    object obj = gvB.GetRowValues(e.VisibleIndex, fieldValues);
                    object[] oB = (object[])obj;

                    sCvePiloto = oB[1].S();
                    sFechaInicio = oB[2].S();
                    sFechaFinal = oB[3].S();

                    if (eSearchEstatus != null)
                        eSearchEstatus(sender, e);

                    //Consultar existencia del periodo por piloto

                    if (iEstatus == 0 || iEstatus == 1)
                        rdEstatus.Text = "Pendiente";
                    else if (iEstatus == 2)
                        rdEstatus.Text = "Guardado";
                    else if (iEstatus == 3)
                        rdEstatus.Text = "Pagado";


                }
                if (e.DataColumn.FieldName == "IdPeriodo")
                {
                    BootstrapGridView gvB = (BootstrapGridView)(sender as BootstrapGridView);
                    Label rdEstatus = gvB.FindRowCellTemplateControl(e.VisibleIndex, e.DataColumn, "readEstatus") as Label;
                    ASPxButton btnVerAjustes = gvB.FindRowCellTemplateControl(e.VisibleIndex, e.DataColumn, "btnVerAjustes") as ASPxButton;
                    ASPxButton btnReporte = gvB.FindRowCellTemplateControl(e.VisibleIndex, e.DataColumn, "btnReporte") as ASPxButton;

                    //int iIdBitacora = gvB.GetRowValues(e.VisibleIndex, "IdFolio").S().I();
                    string[] fieldValues = { "Piloto", "CrewCode", "FechaInicio", "FechaFin" };
                    object obj = gvB.GetRowValues(e.VisibleIndex, fieldValues);
                    object[] oB = (object[])obj;

                    sCvePiloto = oB[1].S();
                    sFechaInicio = oB[2].S();
                    sFechaFinal = oB[3].S();

                    if (eSearchEstatus != null)
                        eSearchEstatus(sender, e);

                    if (eSearchExistePeriodoPic != null)
                        eSearchExistePeriodoPic(sender, e);

                    if (btnVerAjustes != null)
                    {
                        if (iEstatus <= 1 && iExistePeriodo == 0)
                        {
                            btnVerAjustes.Enabled = false;
                            btnReporte.Enabled = false;
                        }
                        else if (iEstatus == 1 && iExistePeriodo == 1)
                        {
                            btnVerAjustes.Enabled = false;
                            btnReporte.Enabled = false;
                        }
                        else if (iEstatus == 2 && iExistePeriodo == 1)
                        {
                            btnVerAjustes.Enabled = true;
                            btnReporte.Enabled = true;
                        }
                        else if (iEstatus == 3 && iExistePeriodo == 1)
                        {
                            btnVerAjustes.Enabled = true;
                            btnReporte.Enabled = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gvPeriodosGuardados_HtmlDataCellPrepared(object sender, DevExpress.Web.Bootstrap.BootstrapGridViewTableDataCellEventArgs e)
        {
            try
            {
                if (e.DataColumn.FieldName == "Estatus")
                {
                    sCvePiloto = string.Empty;
                    sFechaInicio = string.Empty;
                    sFechaFinal = string.Empty;

                    BootstrapGridView gvB = (BootstrapGridView)(sender as BootstrapGridView);
                    Label rdEstatus = gvB.FindRowCellTemplateControl(e.VisibleIndex, e.DataColumn, "readEstatusPendientes") as Label;

                    string[] fieldValues = { "Piloto", "CrewCode", "FechaInicio", "FechaFin" };
                    object obj = gvB.GetRowValues(e.VisibleIndex, fieldValues);
                    object[] oB = (object[])obj;

                    sCvePiloto = oB[1].S();
                    sFechaInicio = oB[2].S();
                    sFechaFinal = oB[3].S();

                    if (eSearchEstatus != null)
                        eSearchEstatus(sender, e);

                    //Consultar existencia del periodo por piloto

                    if (iEstatus == 0 || iEstatus == 1)
                        rdEstatus.Text = "Pendiente";
                    //else if (iEstatus == 2)
                    //    rdEstatus.Text = "Guardado";
                    //else if (iEstatus == 3)
                    //    rdEstatus.Text = "Pagado";


                }
                if (e.DataColumn.FieldName == "IdFolio")
                {
                    sCvePiloto = string.Empty;
                    sFechaInicio = string.Empty;
                    sFechaFinal = string.Empty;

                    BootstrapGridView gvB = (BootstrapGridView)(sender as BootstrapGridView);
                    Label rdEstatus = gvB.FindRowCellTemplateControl(e.VisibleIndex, e.DataColumn, "readEstatusPendientes") as Label;
                    //ASPxButton btnVerAjustes = gvB.FindRowCellTemplateControl(e.VisibleIndex, e.DataColumn, "btnVerAjustes") as ASPxButton;
                    //ASPxButton btnReporte = gvB.FindRowCellTemplateControl(e.VisibleIndex, e.DataColumn, "btnReporte") as ASPxButton;

                    //int iIdBitacora = gvB.GetRowValues(e.VisibleIndex, "IdFolio").S().I();
                    string[] fieldValues = { "Piloto", "CrewCode", "FechaInicio", "FechaFin" };
                    object obj = gvB.GetRowValues(e.VisibleIndex, fieldValues);
                    object[] oB = (object[])obj;

                    sCvePiloto = oB[1].S();
                    sFechaInicio = oB[2].S();
                    sFechaFinal = oB[3].S();

                    if (eSearchEstatus != null)
                        eSearchEstatus(sender, e);

                    if (eSearchExistePeriodoPic != null)
                        eSearchExistePeriodoPic(sender, e);

                    //if (btnVerAjustes != null)
                    //{
                    //    if (iEstatus <= 1 && iExistePeriodo == 0)
                    //    {
                    //        btnVerAjustes.Enabled = false;
                    //        btnReporte.Enabled = false;
                    //    }
                    //    else if (iEstatus == 1 && iExistePeriodo == 1)
                    //    {
                    //        btnVerAjustes.Enabled = false;
                    //        btnReporte.Enabled = false;
                    //    }
                    //    else if (iEstatus == 2 && iExistePeriodo == 1)
                    //    {
                    //        btnVerAjustes.Enabled = true;
                    //        btnReporte.Enabled = true;
                    //    }
                    //    else if (iEstatus == 3 && iExistePeriodo == 1)
                    //    {
                    //        btnVerAjustes.Enabled = true;
                    //        btnReporte.Enabled = true;
                    //    }
                    //}
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            try
            {
                btnBuscar_Click(sender, e);
                pnlBusqueda.Visible = true;
                pnlVuelos.Visible = true;
                pnlCalcularViaticos.Visible = false;
                pnlDatosPiloto.Visible = false;
                pnlAjuste.Visible = false;
                hdnIdPeriodo.Value = "0";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void btnAgregarAjuste_Click(object sender, EventArgs e)
        {
            try
            {
                ddlConceptoAdicional.Value = "";
                ddlMoneda.Value = "";
                txtImporte.Text = string.Empty;
                txtComentarios.Text = string.Empty;
                ppAjustes.ShowOnPageLoad = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void btnGuardarAdicional_Click(object sender, EventArgs e)
        {
            try
            {
                Page.Validate("gpAjuste");
                if (Page.IsValid)
                {
                    if (eSaveAjustes != null)
                        eSaveAjustes(sender, e);

                    if (sOk == "correcto")
                    {
                        if (eSearchConAdPeriodo != null)
                            eSearchConAdPeriodo(sender, e);

                        ppAjustes.ShowOnPageLoad = false;
                        MostrarMensaje("Se ha registrado el ajuste", "Listo");
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error: " + ex.Message, "Error");
            }
        }
        protected void gvAjustes_RowCommand(object sender, DevExpress.Web.ASPxGridViewRowCommandEventArgs e)
        {
            try
            {
                if (e.CommandArgs.CommandName.S() == "Eliminar")
                {
                    int index = e.VisibleIndex.I();
                    int IdAdicional = gvAjustes.GetRowValues(index, "IdAdicional").S().I();
                    iIdAjuste = IdAdicional;

                    ppAlertConfirm.ShowOnPageLoad = true;
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error: " + ex.Message, "Error");
            }
        }
        protected void btnAccept_Click(object sender, EventArgs e)
        {
            try
            {
                if (iIdAjuste != 0)
                {
                    if (eRemoveAjuste != null)
                        eRemoveAjuste(sender, e);

                    if (sOk == "correcto")
                    {
                        if (eSearchConAdPeriodo != null)
                            eSearchConAdPeriodo(sender, e);

                        ppAlertConfirm.ShowOnPageLoad = false;

                        MostrarMensaje("Se ha eliminado el ajuste", "Listo");
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error: " + ex.Message, "Error");
            }
        }
        protected void gvConteoDias_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int pageIndex = gvConteoDias.PageIndex;
                gvConteoDias.PageIndex = pageIndex;
                gvConteoDias.DataSource = dtViaticosDiaInsert;
                gvConteoDias.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void gvCalculo_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int pageIndex = gvCalculo.PageIndex;
                gvCalculo.PageIndex = pageIndex;
                gvCalculo.DataSource = dtGuardados;
                gvCalculo.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        protected void gvPeriodosGuardados_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int pageIndex = gvPeriodosGuardados.PageIndex;
                gvPeriodosGuardados.PageIndex = pageIndex;
                gvPeriodosGuardados.DataSource = dtCalculos;
                gvPeriodosGuardados.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region MÉTODOS
        public void MostrarMensaje(string sMensaje, string sCaption)
        {
            //mpeMensaje.ShowMessage(sMensaje, sCaption);
            lbl.Text = sMensaje;
            ppAlert.ShowOnPageLoad = true;
        }
        public string GetMes(int iMes)
        {
            try
            {
                string sMes = string.Empty;
                switch (iMes)
                {
                    case 1:
                        sMes = "Enero";
                        break;
                    case 2:
                        sMes = "Febrero";
                        break;
                    case 3:
                        sMes = "Marzo";
                        break;
                    case 4:
                        sMes = "Abril";
                        break;
                    case 5:
                        sMes = "Mayo";
                        break;
                    case 6:
                        sMes = "Junio";
                        break;
                    case 7:
                        sMes = "Julio";
                        break;
                    case 8:
                        sMes = "Agosto";
                        break;
                    case 9:
                        sMes = "Septiembre";
                        break;
                    case 10:
                        sMes = "Octubre";
                        break;
                    case 11:
                        sMes = "Noviembre";
                        break;
                    case 12:
                        sMes = "Diciembre";
                        break;
                    default:
                        break;
                }
                return sMes;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        public int GetMesReverse(string sMes)
        {
            try
            {
                int iMes = 0;
                switch (sMes)
                {
                    case "Ene":
                        iMes = 1;
                        break;
                    case "Feb":
                        iMes = 2;
                        break;
                    case "Mar":
                        iMes = 3;
                        break;
                    case "Abr":
                        iMes = 4;
                        break;
                    case "May":
                        iMes = 5;
                        break;
                    case "Jun":
                        iMes = 6;
                        break;
                    case "Jul":
                        iMes = 7;
                        break;
                    case "Ago":
                        iMes = 8;
                        break;
                    case "Sep":
                        iMes = 9;
                        break;
                    case "Oct":
                        iMes = 10;
                        break;
                    case "Nov":
                        iMes = 11;
                        break;
                    case "Dic":
                        iMes = 12;
                        break;
                    default:
                        break;
                }
                return iMes;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public void LoadsGrids(DataSet ds)
        {
            try
            {
                DataTable dt1 = new DataTable();
                DataTable dt2 = new DataTable();

                if (ds != null && ds.Tables.Count > 0)
                {
                    dt1 = ds.Tables[0];
                    gvHorarios.DataSource = dt1;
                    gvHorarios.DataBind();

                    dt2 = ds.Tables[1];
                    //gvAdicionales.DataSource = dt2;
                    //gvAdicionales.DataBind();

                    ddlConceptoAdicional.DataSource = dt2;
                    ddlConceptoAdicional.ValueField = "IdParametro";
                    ddlConceptoAdicional.TextField = "DesConcepto";
                    ddlConceptoAdicional.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LoadsHoteles(DataSet ds)
        {
            try
            {
                if (ds != null && ds.Tables.Count > 0)
                {
                    gvHorariosHotel.DataSource = ds.Tables[0];
                    gvHorariosHotel.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string CalcularViaticos()
        {
            try
            {
                string sHtml = string.Empty;
                string sTablaNacional = string.Empty;
                string sTablaExtranjera = string.Empty;

                for (int x = 0; x < dtVuelosXPiloto.Rows.Count; x++)
                {
                    string sHrCheckIn = string.Empty;
                    string sHrCheckOut = string.Empty;
                    string[] sArrCheckIn = null;
                    string[] sArrCheckOut = null;
                    TimeSpan tsCheckInMXN = new TimeSpan();
                    TimeSpan tsCheckOutMXN = new TimeSpan();
                    TimeSpan tsCheckInUSD;
                    TimeSpan tsCheckOutUSD;
                    string sCvePais = string.Empty;

                    sArrCheckIn = dtVuelosXPiloto.Rows[x]["CheckIn"].S().Split(' ');
                    sArrCheckOut = dtVuelosXPiloto.Rows[x]["CheckOut"].S().Split(' ');
                    sCvePais = dtVuelosXPiloto.Rows[x]["CvePaisDestino"].S();

                    if (dtConceptos != null && dtConceptos.Rows.Count > 0)
                    {
                        if (dtConceptos.Columns.Contains("CantidadMXN") == false)
                            dtConceptos.Columns.Add("CantidadMXN");

                        if (dtConceptos.Columns.Contains("CantidadUSD") == false)
                            dtConceptos.Columns.Add("CantidadUSD");

                        string sTimeIni = string.Empty;
                        string sTimeFin = string.Empty;
                        string sConcepto = string.Empty;

                        decimal dMontoMXN = 0;
                        decimal dMontoUSD = 0;

                        TimeSpan tsIni;
                        TimeSpan tsFin;

                        for (int i = 0; i < dtConceptos.Rows.Count; i++)
                        {
                            sConcepto = dtConceptos.Rows[i]["DesConcepto"].S();
                            sTimeIni = dtConceptos.Rows[i]["HoraInicial"].S().Substring(0, 5);
                            sTimeFin = dtConceptos.Rows[i]["HoraFinal"].S().Substring(0, 5);
                            dMontoMXN = dtConceptos.Rows[i]["MontoMXN"].S().D();
                            dMontoUSD = dtConceptos.Rows[i]["MontoUSD"].S().D();
                            TimeSpan timeIni = TimeSpan.Parse(sTimeIni);
                            TimeSpan timeFin = TimeSpan.Parse(sTimeFin);

                            int iCountMXN = 0;
                            int iCountUSD = 0;
                            decimal dTotalMXN = 0;
                            decimal dTotalUSD = 0;

                            #region Viáticos Nacionales
                            if (sCvePais == "MX")
                            {
                                if (sArrCheckIn.Length > 1)
                                {
                                    sHrCheckIn = sArrCheckIn[1].S().Substring(0, 5);
                                    tsCheckInMXN = TimeSpan.Parse(sHrCheckIn);
                                }
                                if (sArrCheckOut.Length > 1)
                                {
                                    sHrCheckOut = sArrCheckOut[1].S().Substring(0, 5);
                                    tsCheckOutMXN = TimeSpan.Parse(sHrCheckOut);
                                }

                                if (tsCheckInMXN >= timeIni && tsCheckInMXN <= timeFin)
                                {

                                    if (!string.IsNullOrEmpty(dtConceptos.Rows[i]["CantidadMXN"].S()))
                                    {
                                        int iCantidad = dtConceptos.Rows[i]["CantidadMXN"].I();
                                        iCountMXN += 1;
                                        dtConceptos.Rows[i]["CantidadMXN"] = iCountMXN + iCantidad;
                                    }
                                    else
                                    {
                                        iCountMXN += 1;
                                        dtConceptos.Rows[i]["CantidadMXN"] = iCountMXN;
                                    }
                                }
                                else
                                {
                                    dtConceptos.Rows[i]["CantidadMXN"] = 0;
                                }
                            }
                            #endregion

                            #region Viáticos Internacionales
                            else if (sCvePais != "MX")
                            {
                                if (sArrCheckIn.Length > 1)
                                {
                                    sHrCheckIn = sArrCheckIn[1].S().Substring(0, 5);
                                    tsCheckInMXN = TimeSpan.Parse(sHrCheckIn);
                                }
                                if (sArrCheckOut.Length > 1)
                                {
                                    sHrCheckOut = sArrCheckOut[1].S().Substring(0, 5);
                                    tsCheckOutMXN = TimeSpan.Parse(sHrCheckOut);
                                }

                                if (tsCheckInMXN >= timeIni && tsCheckInMXN <= timeFin)
                                {

                                    if (!string.IsNullOrEmpty(dtConceptos.Rows[i]["CantidadUSD"].S()))
                                    {
                                        int iCantidad = dtConceptos.Rows[i]["CantidadUSD"].I();
                                        iCountUSD += 1;
                                        dtConceptos.Rows[i]["CantidadUSD"] = iCountUSD + iCantidad;
                                    }
                                    else
                                    {
                                        iCountUSD += 1;
                                        dtConceptos.Rows[i]["CantidadUSD"] = iCountUSD;
                                    }
                                }
                                else
                                {
                                    dtConceptos.Rows[i]["CantidadUSD"] = 0;
                                }
                            }
                            #endregion


                        }
                    }
                }


                #region Creación de viáticos Nacionales e Internacionales

                decimal _dMontoMXN = 0;
                decimal _dTotalMXN = 0;
                int iCantidadMXN = 0;

                sTablaNacional = "<table border='1' width='40%' style='border-radius:4px 4px 4px 4px; border: 1px solid #ccc;'>";
                sTablaNacional += "  <tr>";
                sTablaNacional += "      <td colspan='2' style='background-color:#ccc; text-align:center;'><label>NACIONALES</label></td>";
                sTablaNacional += "  </tr>";

                for (int i = 0; i < dtConceptos.Rows.Count; i++)
                {
                    if (!string.IsNullOrEmpty(dtConceptos.Rows[i]["CantidadMXN"].S()))
                        iCantidadMXN = dtConceptos.Rows[i]["CantidadMXN"].S().I();
                    else
                        iCantidadMXN = 0;

                    _dMontoMXN = dtConceptos.Rows[i]["MontoMXN"].S().D();
                    _dTotalMXN += _dMontoMXN * iCantidadMXN;

                    sTablaNacional += "  <tr>";
                    sTablaNacional += "  <td><label>" + dtConceptos.Rows[i]["DesConcepto"].S().ToUpper() + "</label></td>";
                    sTablaNacional += "  <td align='center'><label>" + iCantidadMXN.S() + "</label></td>";
                    sTablaNacional += "  </tr>";
                }
                sTablaNacional += "  <tr>";
                sTablaNacional += "     <td><span><b>TOTAL:</b></span></td>";
                sTablaNacional += "     <td align='right'><span>" + _dTotalMXN.ToString("C") + "</span></td>";
                sTablaNacional += "  </tr>";
                sTablaNacional += "  </table>";



                decimal _dMontoUSD = 0;
                decimal _dTotalUSD = 0;
                int iCantidadUSD = 0;

                sTablaExtranjera = "<table border='1' width='40%' style='border-radius:4px 4px 4px 4px; border: 1px solid #ccc;'>";
                sTablaExtranjera += "  <tr>";
                sTablaExtranjera += "      <td colspan='2' style='background-color:#ccc; text-align:center;'><label>INTERNACIONALES</label></td>";
                sTablaExtranjera += "  </tr>";

                for (int i = 0; i < dtConceptos.Rows.Count; i++)
                {
                    if (!string.IsNullOrEmpty(dtConceptos.Rows[i]["CantidadUSD"].S()))
                        iCantidadUSD = dtConceptos.Rows[i]["CantidadUSD"].S().I();
                    else
                        iCantidadUSD = 0;

                    _dMontoUSD = dtConceptos.Rows[i]["MontoUSD"].S().D();
                    _dTotalUSD += _dMontoUSD * iCantidadUSD;

                    sTablaExtranjera += "  <tr>";
                    sTablaExtranjera += "  <td><label>" + dtConceptos.Rows[i]["DesConcepto"].S().ToUpper() + "</label></td>";
                    sTablaExtranjera += "  <td align='center'><label>" + iCantidadUSD.S() + "</label></td>";
                    sTablaExtranjera += "  </tr>";
                }
                sTablaExtranjera += "  <tr>";
                sTablaExtranjera += "     <td><span><b>TOTAL:</b></span></td>";
                sTablaExtranjera += "     <td align='right'><span>" + _dTotalUSD.ToString("C") + "</span></td>";
                sTablaExtranjera += "  </tr>";
                sTablaExtranjera += "  </table>";


                #endregion

                sHtml += "<div class='row'>";
                sHtml += "  <div class='col-md-6' align='center'>";
                sHtml += sTablaNacional;
                sHtml += "  </div>";
                sHtml += "  <div class='col-md-6' align='center'>";
                sHtml += sTablaExtranjera;
                sHtml += "  </div>";
                sHtml += "</div>";

                return sHtml;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LoadConceptos(DataTable dt)
        {
            try
            {
                dtConceptos = null;
                dtConceptos = dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LoadPilotos(DataTable dt)
        {
            try
            {
                dtVuelos = null;
                dtVuelos = dt;

                if (dtVuelos != null && dtVuelos.Rows.Count > 0)
                {
                    //gvCalculo.DataSource = dtVuelos;
                    //gvCalculo.DataBind();
                    pnlVuelos.Visible = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LoadVuelos(DataTable dt)
        {
            try
            {
                dtVuelosXPiloto = null;
                dtVuelosXPiloto = dt;

                if (dtVuelosXPiloto != null && dtVuelosXPiloto.Rows.Count > 0)
                {
                    gvVuelos.DataSource = dtVuelosXPiloto;
                    gvVuelos.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Llena primer grid de calculos
        /// </summary>
        public void LlenaCalculoPilotos(DataTable dt)
        {
            try
            {
                DataTable dtPeriodos = new DataTable();
                dtPeriodos = dt;

                //GeneraCalculo();
                if (dtPeriodos != null && dtPeriodos.Rows.Count > 0)
                {
                    string sPeriodoBusqueda = string.Empty;
                    sPeriodoBusqueda = "Del " + date1.Text.Dt().Day.S() + " de " + GetMes(date1.Text.Dt().Month) + " al " + date2.Text.Dt().Day.S() + " de " + GetMes(date2.Text.Dt().Month) + " de " + date2.Text.Dt().Year.S();
                    lblPeriodoBusqueda.Text = sPeriodoBusqueda;

                    DataTable dtPendientes = new DataTable();
                    dtPendientes = EliminarPeriodosGuardados(dtPeriodos);

                    if (dtPendientes == null)
                    {
                        btnAprobar.Visible = false;
                        pnlVuelos.Visible = false;
                        gvPeriodosGuardados.DataBind();
                    }
                    else
                    {
                        //Periodos pendientes
                        gvPeriodosGuardados.DataSource = dtPendientes;
                        gvPeriodosGuardados.DataBind();

                        btnAprobar.Visible = true;
                        pnlVuelos.Visible = true;

                        dtCalculos = null;
                        dtCalculos = dtPendientes;

                        if (dtCalculos1X1 == null || dtCalculos1X1.Rows.Count == 0)
                            dtCalculos1X1 = dtCalculos;
                    }
                }
                else
                {
                    lblPeriodoBusqueda.Text = string.Empty;
                    btnAprobar.Visible = false;
                    pnlVuelos.Visible = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable EliminarPeriodosGuardados(DataTable dtSource)
        {
            try
            {
                for (int i = dtSource.Rows.Count - 1; i >= 0; i--)
                {
                    sCvePiloto = dtSource.Rows[i]["CrewCode"].S();
                    sFechaInicio = dtSource.Rows[i]["FechaInicio"].S();
                    sFechaFinal = dtSource.Rows[i]["FechaFin"].S();

                    if (eSearchEstatus != null)
                        eSearchEstatus(null, null);

                    if (eSearchExistePeriodoPic != null)
                        eSearchExistePeriodoPic(null, null);

                    DataRow dr1 = dtSource.Rows[i];

                    if (iExistePeriodo == 1 && iEstatus == 2)
                        dtSource.Rows.Remove(dr1);

                    dtSource.AcceptChanges();
                }
                //else if (ban == 2)
                //{
                //    for (int i = dtSource.Rows.Count - 1; i >= 0; i--)
                //    {
                //        sCvePiloto = dtSource.Rows[i]["CrewCode"].S();
                //        sFechaInicio = dtSource.Rows[i]["FechaInicio"].S();
                //        sFechaFinal = dtSource.Rows[i]["FechaFin"].S();

                //        if (eSearchEstatus != null)
                //            eSearchEstatus(null, null);

                //        if (eSearchExistePeriodoPic != null)
                //            eSearchExistePeriodoPic(null, null);

                //        DataRow dr2 = dtSource.Rows[i];

                //        if ((iExistePeriodo == 0) && (iEstatus == 0 || iEstatus == 1))
                //            dtSource.Rows.Remove(dr2);
                //    }
                //    dtSource.AcceptChanges();
                //}
                return dtSource;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void CargaViaticos()
        {
            try
            {
                #region CÁLCULO
                decimal dDesNal = 0;
                decimal dDesInt = 0;
                decimal dComNal = 0;
                decimal dComInt = 0;
                decimal dCenNal = 0;
                decimal dCenInt = 0;

                foreach (DataRow row in dsParams.Tables[0].Rows)
                {

                    if (row["Concepto"].S() == "Desayuno")
                    {
                        dDesNal = row["MontoMXN"].S().D();
                        dDesInt = row["MontoUSD"].S().D();
                    }
                    if (row["Concepto"].S() == "Comida")
                    {
                        dComNal = row["MontoMXN"].S().D();
                        dComInt = row["MontoUSD"].S().D();
                    }
                    if (row["Concepto"].S() == "Cena")
                    {
                        dCenNal = row["MontoMXN"].S().D();
                        dCenInt = row["MontoUSD"].S().D();
                    }
                }

                if (!dtCalculos.Columns.Contains("TotalPesos"))
                {
                    dtCalculos.Columns.Add("TotalPesos", typeof(decimal));
                    dtCalculos.Columns["TotalPesos"].ReadOnly = false;
                }

                if (!dtCalculos.Columns.Contains("TotalUSD"))
                {
                    dtCalculos.Columns.Add("TotalUSD", typeof(decimal));
                    dtCalculos.Columns["TotalUSD"].ReadOnly = false;
                }


                foreach (DataRow row in dtCalculos.Rows)
                {
                    decimal dTotalNal = 0;
                    decimal dTotalInt = 0;

                    dTotalNal += row["DesayunosNal"].S().D() * dDesNal;
                    dTotalNal += row["ComidasNal"].S().D() * dComNal;
                    dTotalNal += row["CenasNal"].S().D() * dCenNal;

                    dTotalInt += row["DesayunosInt"].S().D() * dDesInt;
                    dTotalInt += row["ComidasInt"].S().D() * dComInt;
                    dTotalInt += row["CenasInt"].S().D() * dCenInt;


                    row["TotalPesos"] = dTotalNal;
                    row["TotalUSD"] = dTotalInt;
                }
                #endregion

                #region NACIONALES
                dtNal = new DataTable();
                dtNal.Columns.Add("CONCEPTO");
                dtNal.Columns.Add("NACIONAL");

                DataRow dr = dtNal.NewRow();
                dr["CONCEPTO"] = "DESAYUNO";
                dr["NACIONAL"] = dtCalculos.Rows[0]["DesayunosNal"].S();
                dtNal.Rows.Add(dr);

                dr = dtNal.NewRow();
                dr["CONCEPTO"] = "COMIDA";
                dr["NACIONAL"] = dtCalculos.Rows[0]["ComidasNal"].S();
                dtNal.Rows.Add(dr);

                dr = dtNal.NewRow();
                dr["CONCEPTO"] = "CENA";
                dr["NACIONAL"] = dtCalculos.Rows[0]["CenasNal"].S();
                dtNal.Rows.Add(dr);

                dr = dtNal.NewRow();
                dr["CONCEPTO"] = "TOTAL";
                dr["NACIONAL"] = dtCalculos.Rows[0]["TotalPesos"].S().D().ToString("c");
                dtNal.Rows.Add(dr);

                gvNacionales.DataSource = dtNal;
                gvNacionales.DataBind();
                #endregion

                #region INTERNACIONALES
                dtInt = new DataTable();
                dtInt.Columns.Add("CONCEPTO");
                dtInt.Columns.Add("INTERNACIONAL");

                DataRow dr2 = dtInt.NewRow();
                dr2["CONCEPTO"] = "DESAYUNO";
                dr2["INTERNACIONAL"] = dtCalculos.Rows[0]["DesayunosInt"].S();
                dtInt.Rows.Add(dr2);

                dr2 = dtInt.NewRow();
                dr2["CONCEPTO"] = "COMIDA";
                dr2["INTERNACIONAL"] = dtCalculos.Rows[0]["ComidasInt"].S();
                dtInt.Rows.Add(dr2);

                dr2 = dtInt.NewRow();
                dr2["CONCEPTO"] = "CENA";
                dr2["INTERNACIONAL"] = dtCalculos.Rows[0]["CenasInt"].S();
                dtInt.Rows.Add(dr2);

                dr2 = dtInt.NewRow();
                dr2["CONCEPTO"] = "TOTAL";
                dr2["INTERNACIONAL"] = dtCalculos.Rows[0]["TotalUSD"].S().D().ToString("c") + " " + "USD";
                dtInt.Rows.Add(dr2);

                gvInternacionales.DataSource = dtInt;
                gvInternacionales.DataBind();
                #endregion

                #region Llenado general PESOS Y DOLARES
                dtMXNUSD = new DataTable();
                dtMXNUSD.Columns.Add("CONCEPTO");
                dtMXNUSD.Columns.Add("NACIONAL");
                dtMXNUSD.Columns.Add("INTERNACIONAL");

                DataRow dr3 = dtMXNUSD.NewRow();
                dr3["CONCEPTO"] = "DESAYUNO";
                dr3["NACIONAL"] = dtCalculos.Rows[0]["DesayunosNal"].S();
                dr3["INTERNACIONAL"] = dtCalculos.Rows[0]["DesayunosInt"].S();
                dtMXNUSD.Rows.Add(dr3);

                dr3 = dtMXNUSD.NewRow();
                dr3["CONCEPTO"] = "COMIDA";
                dr3["NACIONAL"] = dtCalculos.Rows[0]["ComidasNal"].S();
                dr3["INTERNACIONAL"] = dtCalculos.Rows[0]["ComidasInt"].S();
                dtMXNUSD.Rows.Add(dr3);

                dr3 = dtMXNUSD.NewRow();
                dr3["CONCEPTO"] = "CENA";
                dr3["NACIONAL"] = dtCalculos.Rows[0]["CenasNal"].S();
                dr3["INTERNACIONAL"] = dtCalculos.Rows[0]["CenasInt"].S();
                dtMXNUSD.Rows.Add(dr3);

                dr3 = dtMXNUSD.NewRow();
                dr3["CONCEPTO"] = "TOTAL";
                dr3["NACIONAL"] = dtCalculos.Rows[0]["TotalPesos"].S().D().ToString("c") + " " + "MXN";
                dr3["INTERNACIONAL"] = dtCalculos.Rows[0]["TotalUSD"].S().D().ToString("c") + " " + "USD";
                dtMXNUSD.Rows.Add(dr3);

                gvMXNUSD.DataSource = dtMXNUSD;
                gvMXNUSD.DataBind();
                #endregion

                pnlVuelos.Visible = false;

                //------------------------------------------------------------------------------

                if (dtDiasViaticos != null && dtDiasViaticos.Rows.Count > 0)
                {
                    DataRow drow;
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Fecha");
                    dt.Columns.Add("Moneda");
                    dt.Columns.Add("Desayuno");
                    dt.Columns.Add("Comida");
                    dt.Columns.Add("Cena");
                    dt.Columns.Add("Total");

                    for (int i = 0; i < dtDiasViaticos.Rows.Count; i++)
                    {
                        for (int x = 0; x < 2; x++)
                        {
                            if (x == 0)
                            {
                                decimal dTotalDesNal = 0;
                                decimal dTotalComNal = 0;
                                decimal dTotalCenNal = 0;
                                decimal dTotalNal = 0;

                                dTotalDesNal = dtDiasViaticos.Rows[i]["DesNal"].S().D() * ObtenValorConcepto(1, "MXN");
                                dTotalComNal = dtDiasViaticos.Rows[i]["ComNal"].S().D() * ObtenValorConcepto(2, "MXN");
                                dTotalCenNal = dtDiasViaticos.Rows[i]["CenNal"].S().D() * ObtenValorConcepto(3, "MXN");
                                dTotalNal = ((dTotalDesNal + dTotalComNal) + dTotalCenNal);

                                drow = dt.NewRow();
                                drow["Fecha"] = dtDiasViaticos.Rows[i]["FechaDia"].S().Dt();
                                drow["Moneda"] = "MXN";
                                drow["Desayuno"] = dTotalDesNal.ToString("c") + " " + "MXN";
                                drow["Comida"] = dTotalComNal.ToString("c") + " " + "MXN";
                                drow["Cena"] = dTotalCenNal.ToString("c") + " " + "MXN";
                                drow["Total"] = dTotalNal.ToString("c") + " " + "MXN";
                            }
                            else
                            {
                                decimal dTotalDesInt = 0;
                                decimal dTotalComInt = 0;
                                decimal dTotalCenInt = 0;
                                decimal dTotalInt = 0;

                                dTotalDesInt = dtDiasViaticos.Rows[i]["DesInt"].S().D() * ObtenValorConcepto(1, "USD");
                                dTotalComInt = dtDiasViaticos.Rows[i]["ComInt"].S().D() * ObtenValorConcepto(2, "USD");
                                dTotalCenInt = dtDiasViaticos.Rows[i]["CenInt"].S().D() * ObtenValorConcepto(3, "USD");
                                dTotalInt = ((dTotalDesInt + dTotalComInt) + dTotalCenInt);

                                drow = dt.NewRow();
                                drow["Fecha"] = dtDiasViaticos.Rows[i]["FechaDia"].S().Dt();
                                drow["Moneda"] = "USD";
                                drow["Desayuno"] = dTotalDesInt.ToString("c") + " " + "USD";
                                drow["Comida"] = dTotalComInt.ToString("c") + " " + "USD";
                                drow["Cena"] = dTotalCenInt.ToString("c") + " " + "USD";
                                drow["Total"] = dTotalInt.ToString("c") + " " + "USD";
                            }
                            dt.Rows.Add(drow);
                        }


                    }

                    dtViaticosDiaInsert = null;
                    dtViaticosDiaInsert = dt;
                    gvConteoDias.DataSource = dt;
                    gvConteoDias.DataBind();

                    //Mostrar conteo dias-viaticos
                    AgruparDiasViaticos(dtDiasViaticos);

                    //Mostrar conteo días-hotel

                    if (dtViaticosHot != null && dtViaticosHot.Rows.Count > 0)
                    {

                        DataRow drowH;
                        DataTable dtH = new DataTable();
                        dtH.Columns.Add("Fecha");
                        dtH.Columns.Add("Moneda");
                        dtH.Columns.Add("HotNac");
                        dtH.Columns.Add("HotInt");
                        dtH.Columns.Add("Total");

                        for (int i = 0; i < dtViaticosHot.Rows.Count; i++)
                        {
                            for (int x = 0; x < 2; x++)
                            {
                                if (x == 0)
                                {
                                    decimal dTotalHotNal = 0;
                                    decimal dTotalNal = 0;

                                    for (int a = 0; a < dsParamsH.Tables[1].Rows.Count; a++)
                                    {
                                        if (dtViaticosHot.Rows[i]["POA"].S() == dsParamsH.Tables[1].Rows[a]["POA"].S())
                                        {
                                            dTotalHotNal = dtViaticosHot.Rows[i]["HotNal"].S().D() * ObtenValorHotel(2, "MXN");
                                            break;
                                        }
                                    }

                                    if(dTotalHotNal == 0)
                                        dTotalHotNal = dtViaticosHot.Rows[i]["HotNal"].S().D() * ObtenValorHotel(1, "MXN");

                                    dTotalNal = dTotalHotNal;

                                    drowH = dtH.NewRow();
                                    drowH["Fecha"] = dtViaticosHot.Rows[i]["FechaDia"].S().Dt();
                                    drowH["Moneda"] = "MXN";
                                    drowH["HotNac"] = dTotalHotNal.ToString("c") + " " + "MXN";
                                    drowH["HotInt"] = (0).ToString("c") + " " + "USD";
                                    drowH["Total"] = dTotalNal.ToString("c") + " " + "MXN";
                                }
                                else
                                {
                                    decimal dTotalHotInt = 0;
                                    decimal dTotalInt = 0;

                                    for (int a = 0; a < dsParamsH.Tables[1].Rows.Count; a++)
                                    {
                                        if (dtViaticosHot.Rows[i]["POA"].S() == dsParamsH.Tables[1].Rows[a]["POA"].S())
                                        {
                                            dTotalHotInt = dtViaticosHot.Rows[i]["HotInt"].S().D() * ObtenValorHotel(2, "USD");
                                            break;
                                        }
                                    }

                                    if(dTotalHotInt == 0)
                                        dTotalHotInt = dtViaticosHot.Rows[i]["HotInt"].S().D() * ObtenValorHotel(1, "USD");

                                    dTotalInt = dTotalHotInt;

                                    drowH = dtH.NewRow();
                                    drowH["Fecha"] = dtViaticosHot.Rows[i]["FechaDia"].S().Dt();
                                    drowH["Moneda"] = "USD";
                                    drowH["HotNac"] = (0).ToString("c") + " " + "MXN";
                                    drowH["HotInt"] = dTotalHotInt.ToString("c") + " " + "USD";
                                    drowH["Total"] = dTotalInt.ToString("c") + " " + "USD";
                                }
                                dtH.Rows.Add(drowH);
                            }
                        }

                        dtViaticosHotelDiaInsert = null;
                        dtViaticosHotelDiaInsert = dtH;

                        AgruparDiasGastosHotel(dtViaticosHot);
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void CargaViaticosGuardados()
        {
            try
            {
                #region CÁLCULO
                decimal dDesNal = 0;
                decimal dDesInt = 0;
                decimal dComNal = 0;
                decimal dComInt = 0;
                decimal dCenNal = 0;
                decimal dCenInt = 0;

                foreach (DataRow row in dsParams.Tables[0].Rows)
                {

                    if (row["Concepto"].S() == "Desayuno")
                    {
                        dDesNal = row["MontoMXN"].S().D();
                        dDesInt = row["MontoUSD"].S().D();
                    }
                    if (row["Concepto"].S() == "Comida")
                    {
                        dComNal = row["MontoMXN"].S().D();
                        dComInt = row["MontoUSD"].S().D();
                    }
                    if (row["Concepto"].S() == "Cena")
                    {
                        dCenNal = row["MontoMXN"].S().D();
                        dCenInt = row["MontoUSD"].S().D();
                    }
                }

                dtCalculos2.Columns.Add("TotalPesos", typeof(decimal));
                dtCalculos2.Columns.Add("TotalUSD", typeof(decimal));

                dtCalculos2.Columns["TotalPesos"].ReadOnly = false;
                dtCalculos2.Columns["TotalUSD"].ReadOnly = false;


                foreach (DataRow row in dtCalculos2.Rows)
                {
                    decimal dTotalNal = 0;
                    decimal dTotalInt = 0;

                    dTotalNal += row["DesayunosNal"].S().D() * dDesNal;
                    dTotalNal += row["ComidasNal"].S().D() * dComNal;
                    dTotalNal += row["CenasNal"].S().D() * dCenNal;

                    dTotalInt += row["DesayunosInt"].S().D() * dDesInt;
                    dTotalInt += row["ComidasInt"].S().D() * dComInt;
                    dTotalInt += row["CenasInt"].S().D() * dCenInt;


                    row["TotalPesos"] = dTotalNal;
                    row["TotalUSD"] = dTotalInt;
                }
                #endregion

                dtNal = new DataTable();
                dtNal.Columns.Add("CONCEPTO");
                dtNal.Columns.Add("NACIONAL");

                DataRow dr = dtNal.NewRow();
                dr["CONCEPTO"] = "DESAYUNO";
                dr["NACIONAL"] = dtCalculos2.Rows[0]["DesayunosNal"].S();
                dtNal.Rows.Add(dr);

                dr = dtNal.NewRow();
                dr["CONCEPTO"] = "COMIDA";
                dr["NACIONAL"] = dtCalculos2.Rows[0]["ComidasNal"].S();
                dtNal.Rows.Add(dr);

                dr = dtNal.NewRow();
                dr["CONCEPTO"] = "CENA";
                dr["NACIONAL"] = dtCalculos2.Rows[0]["CenasNal"].S();
                dtNal.Rows.Add(dr);

                dr = dtNal.NewRow();
                dr["CONCEPTO"] = "TOTAL";
                dr["NACIONAL"] = dtCalculos2.Rows[0]["TotalPesos"].S().D().ToString("c");
                dtNal.Rows.Add(dr);

                gvNacionales.DataSource = dtNal;
                gvNacionales.DataBind();


                dtInt = new DataTable();
                dtInt.Columns.Add("CONCEPTO");
                dtInt.Columns.Add("INTERNACIONAL");

                DataRow dr2 = dtInt.NewRow();
                dr2["CONCEPTO"] = "DESAYUNO";
                dr2["INTERNACIONAL"] = dtCalculos2.Rows[0]["DesayunosInt"].S();
                dtInt.Rows.Add(dr2);

                dr2 = dtInt.NewRow();
                dr2["CONCEPTO"] = "COMIDA";
                dr2["INTERNACIONAL"] = dtCalculos2.Rows[0]["ComidasInt"].S();
                dtInt.Rows.Add(dr2);

                dr2 = dtInt.NewRow();
                dr2["CONCEPTO"] = "CENA";
                dr2["INTERNACIONAL"] = dtCalculos2.Rows[0]["CenasInt"].S();
                dtInt.Rows.Add(dr2);

                dr2 = dtInt.NewRow();
                dr2["CONCEPTO"] = "TOTAL";
                dr2["INTERNACIONAL"] = dtCalculos2.Rows[0]["TotalUSD"].S().D().ToString("c") + " " + "USD";
                dtInt.Rows.Add(dr2);

                gvInternacionales.DataSource = dtInt;
                gvInternacionales.DataBind();


                //Llenado general
                dtMXNUSD = new DataTable();
                dtMXNUSD.Columns.Add("CONCEPTO");
                dtMXNUSD.Columns.Add("NACIONAL");
                dtMXNUSD.Columns.Add("INTERNACIONAL");

                DataRow dr3 = dtMXNUSD.NewRow();
                dr3["CONCEPTO"] = "DESAYUNO";
                dr3["NACIONAL"] = dtCalculos2.Rows[0]["DesayunosNal"].S();
                dr3["INTERNACIONAL"] = dtCalculos2.Rows[0]["DesayunosInt"].S();
                dtMXNUSD.Rows.Add(dr3);

                dr3 = dtMXNUSD.NewRow();
                dr3["CONCEPTO"] = "COMIDA";
                dr3["NACIONAL"] = dtCalculos2.Rows[0]["ComidasNal"].S();
                dr3["INTERNACIONAL"] = dtCalculos2.Rows[0]["ComidasInt"].S();
                dtMXNUSD.Rows.Add(dr3);

                dr3 = dtMXNUSD.NewRow();
                dr3["CONCEPTO"] = "CENA";
                dr3["NACIONAL"] = dtCalculos2.Rows[0]["CenasNal"].S();
                dr3["INTERNACIONAL"] = dtCalculos2.Rows[0]["CenasInt"].S();
                dtMXNUSD.Rows.Add(dr3);

                dr3 = dtMXNUSD.NewRow();
                dr3["CONCEPTO"] = "TOTAL";
                dr3["NACIONAL"] = dtCalculos2.Rows[0]["TotalPesos"].S().D().ToString("c") + " " + "MXN";
                dr3["INTERNACIONAL"] = dtCalculos2.Rows[0]["TotalUSD"].S().D().ToString("c") + " " + "USD";
                dtMXNUSD.Rows.Add(dr3);

                gvMXNUSD.DataSource = dtMXNUSD;
                gvMXNUSD.DataBind();

                pnlVuelos.Visible = false;

                //------------------------------------------------------------------------------

                if (dtDiasViaticos != null && dtDiasViaticos.Rows.Count > 0)
                {
                    DataRow drow;
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Fecha");
                    dt.Columns.Add("Moneda");
                    dt.Columns.Add("Desayuno");
                    dt.Columns.Add("Comida");
                    dt.Columns.Add("Cena");
                    dt.Columns.Add("Total");

                    for (int i = 0; i < dtDiasViaticos.Rows.Count; i++)
                    {
                        //drow = dt.NewRow();
                        //if (dtDiasViaticos.Rows[i]["DesNal"].S().I() != 0)
                        //{
                        //    drow["Fecha"] = dtDiasViaticos.Rows[i]["FechaDia"].S().Dt();
                        //    drow["Moneda"] = "MXN";
                        //    drow["Desayuno"] = dtDiasViaticos.Rows[i]["DesNal"].S().D() * ObtenValorConcepto(1, "MXN");
                        //}

                        //decimal dTotalDesNal = 0;
                        //decimal dTotalComNal = 0;
                        //decimal dTotalCenNal = 0;
                        //decimal dTotalNal = 0;

                        //decimal dTotalDesInt = 0;
                        //decimal dTotalComInt = 0;
                        //decimal dTotalCenInt = 0;
                        //decimal dTotalInt = 0;


                        for (int x = 0; x < 2; x++)
                        {
                            if (x == 0)
                            {
                                decimal dTotalDesNal = 0;
                                decimal dTotalComNal = 0;
                                decimal dTotalCenNal = 0;
                                decimal dTotalNal = 0;

                                dTotalDesNal = dtDiasViaticos.Rows[i]["DesNal"].S().D() * ObtenValorConcepto(1, "MXN");
                                dTotalComNal = dtDiasViaticos.Rows[i]["ComNal"].S().D() * ObtenValorConcepto(2, "MXN");
                                dTotalCenNal = dtDiasViaticos.Rows[i]["CenNal"].S().D() * ObtenValorConcepto(3, "MXN");
                                dTotalNal = ((dTotalDesNal + dTotalComNal) + dTotalCenNal);

                                drow = dt.NewRow();
                                drow["Fecha"] = dtDiasViaticos.Rows[i]["FechaDia"].S().Dt();
                                drow["Moneda"] = "MXN";
                                drow["Desayuno"] = dTotalDesNal.ToString("c") + " " + "MXN";
                                drow["Comida"] = dTotalComNal.ToString("c") + " " + "MXN";
                                drow["Cena"] = dTotalCenNal.ToString("c") + " " + "MXN";
                                drow["Total"] = dTotalNal.ToString("c") + " " + "MXN";
                            }
                            else
                            {
                                decimal dTotalDesInt = 0;
                                decimal dTotalComInt = 0;
                                decimal dTotalCenInt = 0;
                                decimal dTotalInt = 0;

                                dTotalDesInt = dtDiasViaticos.Rows[i]["DesInt"].S().D() * ObtenValorConcepto(1, "USD");
                                dTotalComInt = dtDiasViaticos.Rows[i]["ComInt"].S().D() * ObtenValorConcepto(2, "USD");
                                dTotalCenInt = dtDiasViaticos.Rows[i]["CenInt"].S().D() * ObtenValorConcepto(3, "USD");
                                dTotalInt = ((dTotalDesInt + dTotalComInt) + dTotalCenInt);

                                drow = dt.NewRow();
                                drow["Fecha"] = dtDiasViaticos.Rows[i]["FechaDia"].S().Dt();
                                drow["Moneda"] = "USD";
                                drow["Desayuno"] = dTotalDesInt.ToString("c") + " " + "USD";
                                drow["Comida"] = dTotalComInt.ToString("c") + " " + "USD";
                                drow["Cena"] = dTotalCenInt.ToString("c") + " " + "USD";
                                drow["Total"] = dTotalInt.ToString("c") + " " + "USD";
                            }
                            dt.Rows.Add(drow);
                        }


                    }

                    dtViaticosDiaInsert = null;
                    dtViaticosDiaInsert = dt;
                    gvConteoDias.DataSource = dt;
                    gvConteoDias.DataBind();

                    //Mostrar conteo dias-viaticos
                    AgruparDiasViaticos(dtDiasViaticos);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void AgruparDiasViaticos(DataTable dtDiasVia)
        {
            try
            {
                decimal dDesayunoMXN = 0;
                decimal dDesayunoUSD = 0;
                decimal dComidaMXN = 0;
                decimal dComidaUSD = 0;
                decimal dCenaMXN = 0;
                decimal dCenaUSD = 0;

                if (dsParams.Tables[0] != null && dsParams.Tables[0].Rows.Count > 0)
                {
                    dDesayunoMXN = dsParams.Tables[0].Rows[0]["MontoMXN"].S().D();
                    dDesayunoUSD = dsParams.Tables[0].Rows[0]["MontoUSD"].S().D();
                    dComidaMXN = dsParams.Tables[0].Rows[1]["MontoMXN"].S().D();
                    dComidaUSD = dsParams.Tables[0].Rows[1]["MontoUSD"].S().D();
                    dCenaMXN = dsParams.Tables[0].Rows[2]["MontoMXN"].S().D();
                    dCenaUSD = dsParams.Tables[0].Rows[2]["MontoUSD"].S().D();
                }


                DataRow dRow;
                DataTable dtDiasDistinct = new DataTable();
                DataTable dtDiasFecha = new DataTable();
                dtDiasFecha.Columns.Add("Dia");

                for (int i = 0; i < dtDiasVia.Rows.Count; i++)
                {
                    dRow = dtDiasFecha.NewRow();
                    dRow["Dia"] = dtDiasVia.Rows[i]["FechaDia"].S().Dt().ToString("dd/MM/yyyy");
                    dtDiasFecha.Rows.Add(dRow);
                }
                dtDiasFecha.AcceptChanges();
                DataView dv = new DataView(dtDiasFecha);
                dtDiasDistinct = dv.ToTable(true, "Dia"); //Datatable de dias o fechas

                if (dtDiasDistinct != null && dtDiasDistinct.Rows.Count > 0)
                {
                    DataRow drDias;
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Fecha");
                    dt.Columns.Add("TipoCambio");
                    dt.Columns.Add("Desayuno");
                    dt.Columns.Add("Comida");
                    dt.Columns.Add("Cena");

                    dt.Columns.Add("DesayunoNac");
                    dt.Columns.Add("DesayunoInt");
                    dt.Columns.Add("ComidaNac");
                    dt.Columns.Add("ComidaInt");
                    dt.Columns.Add("CenaNac");
                    dt.Columns.Add("CenaInt");
                    dt.Columns.Add("Total");

                    //Tabla para resumen
                    DataRow drDiasResumen;
                    DataTable dtResumen = new DataTable();
                    dtResumen.Columns.Add("Id");
                    dtResumen.Columns.Add("IdPeriodo");
                    dtResumen.Columns.Add("Fecha");
                    dtResumen.Columns.Add("Desayuno");
                    dtResumen.Columns.Add("Comida");
                    dtResumen.Columns.Add("Cena");

                    dtResumen.Columns.Add("DesNac");
                    dtResumen.Columns.Add("DesInt");
                    dtResumen.Columns.Add("ComNac");
                    dtResumen.Columns.Add("ComInt");
                    dtResumen.Columns.Add("CenNac");
                    dtResumen.Columns.Add("CenInt");
                    dtResumen.Columns.Add("Total");
                    dtResumen.Columns.Add("Estatus");
                    dtResumen.Columns.Add("TipoCambio");


                    for (int i = 0; i < dtDiasVia.Rows.Count; i++)
                    {
                        dtDiasVia.Rows[i]["FechaDia"] = dtDiasVia.Rows[i]["FechaDia"].S().Dt().ToString("dd/MM/yyyy");
                    }
                    dtDiasVia.AcceptChanges();

                    for (int i = 0; i < dtDiasDistinct.Rows.Count; i++)
                    {
                        int iCountDesayuno = 0;
                        int iCountComida = 0;
                        int iCountCena = 0;

                        decimal dTipoCambio = 0;
                        decimal dDesNac = 0;
                        decimal dDesInt = 0;
                        decimal dComNac = 0;
                        decimal dComInt = 0;
                        decimal dCenNac = 0;
                        decimal dCenInt = 0;
                        decimal dTotal = 0;

                        drDias = dt.NewRow();
                        drDias["Fecha"] = dtDiasDistinct.Rows[i]["Dia"].S().Dt().Day.S() + " " + GetMes(dtDiasDistinct.Rows[i]["Dia"].S().Dt().Month).Substring(0, 3) + " " + dtDiasDistinct.Rows[i]["Dia"].S().Dt().Year.S();

                        string sFechaVuelo = string.Empty;
                        sFechaVuelo = dtDiasDistinct.Rows[i]["Dia"].S();

                        dTipoCambio = new DBCalculoPagos().ObtenerTipoCambio(sFechaVuelo);

                        //DataView dvDV = new DataView(dtDiasVia);
                        //DataTable dtDiasDV = dvDV.ToTable(true, "FechaDia", "DesNal", "ComNal", "CenNal", "DesInt", "ComInt", "CenInt");

                        DataRow[] dr = dtDiasVia.Select("FechaDia='" + dtDiasDistinct.Rows[i]["Dia"].S() + "'");

                        for (int x = 0; x < dr.Length; x++)
                        {
                            //dDesNac = 0;
                            //dDesInt = 0;
                            //dComNac = 0;
                            //dComInt = 0;
                            //dCenNac = 0;
                            //dCenInt = 0;
                            //dTotal = 0;

                            iCountDesayuno += dr[x]["DesNal"].S().I() + dr[x]["DesInt"].S().I();
                            iCountComida += dr[x]["ComNal"].S().I() + dr[x]["ComInt"].S().I();
                            iCountCena += dr[x]["CenNal"].S().I() + dr[x]["CenInt"].S().I();

                            dDesNac = dr[x]["DesNal"].S().I() * dDesayunoMXN;
                            dDesInt = (dr[x]["DesInt"].S().I() * dDesayunoUSD) * dTipoCambio;

                            dComNac = dr[x]["ComNal"].S().I() * dComidaMXN;
                            dComInt = (dr[x]["ComInt"].S().I() * dComidaUSD) * dTipoCambio;

                            dCenNac = dr[x]["CenNal"].S().I() * dCenaMXN;
                            dCenInt = (dr[x]["CenInt"].S().I() * dCenaUSD) * dTipoCambio;

                            dTotal = ((((dDesNac + dDesInt) + dComNac) + dComInt) + dCenNac) + dCenInt;
                        }
                        

                        drDias["TipoCambio"] = dTipoCambio.ToString("c") + " " + "MXN";
                        drDias["Desayuno"] = iCountDesayuno;
                        drDias["Comida"] = iCountComida;
                        drDias["Cena"] = iCountCena;

                        drDias["DesayunoNac"] = dDesNac.ToString("c") + " " + "MXN";
                        drDias["DesayunoInt"] = dDesInt.ToString("c") + " " + "MXN";
                        drDias["ComidaNac"] = dComNac.ToString("c") + " " + "MXN";
                        drDias["ComidaInt"] = dComInt.ToString("c") + " " + "MXN";
                        drDias["CenaNac"] = dCenNac.ToString("c") + " " + "MXN";
                        drDias["CenaInt"] = dCenInt.ToString("c") + " " + "MXN";
                        drDias["Total"] = dTotal.ToString("c") + " " + "MXN";
                        dt.Rows.Add(drDias);

                        //Tabla de Resumen
                        drDiasResumen = dtResumen.NewRow();
                        drDiasResumen["Id"] = 0;
                        drDiasResumen["IdPeriodo"] = 0;
                        drDiasResumen["Fecha"] = sFechaVuelo.Dt().ToString("yyyy-MM-dd HH:ss:fff");
                        drDiasResumen["Desayuno"] = iCountDesayuno;
                        drDiasResumen["Comida"] = iCountComida;
                        drDiasResumen["Cena"] = iCountCena;
                        drDiasResumen["DesNac"] = dDesNac;
                        drDiasResumen["DesInt"] = dDesInt;
                        drDiasResumen["ComNac"] = dComNac;
                        drDiasResumen["ComInt"] = dComInt;
                        drDiasResumen["CenNac"] = dCenNac;
                        drDiasResumen["CenInt"] = dCenInt;
                        drDiasResumen["Total"] = dTotal;
                        drDiasResumen["Estatus"] = 1;
                        drDiasResumen["TipoCambio"] = dTipoCambio;
                        dtResumen.Rows.Add(drDiasResumen);
                    }
                    dtResumenViaticos = dtResumen;
                    gvDiasViaticos.DataSource = dt;
                    gvDiasViaticos.DataBind();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AgruparDiasGastosHotel(DataTable dtDiasVia)
        {
            try
            {
                decimal dHotelMXN = 0;
                decimal dHotelUSD = 0;
                decimal dHotelSPMXN = 0;
                decimal dHotelSPUSD = 0;
                DataTable dtAeroEspecial = new DataTable();

                if (dsParamsH.Tables[1] != null && dsParamsH.Tables[1].Rows.Count > 0)
                {
                    dtAeroEspecial = dsParamsH.Tables[1];
                }

                if (dsParamsH.Tables[0] != null && dsParamsH.Tables[0].Rows.Count > 0)
                {
                    dHotelMXN = dsParamsH.Tables[0].Rows[0]["MontoMXN"].S().D();
                    dHotelUSD = dsParamsH.Tables[0].Rows[0]["MontoUSD"].S().D();

                    dHotelSPMXN = dsParamsH.Tables[0].Rows[1]["MontoMXN"].S().D();
                    dHotelSPUSD = dsParamsH.Tables[0].Rows[1]["MontoUSD"].S().D();
                }

                DataRow dRow;
                DataTable dtDiasDistinct = new DataTable();
                DataTable dtDiasFecha = new DataTable();
                dtDiasFecha.Columns.Add("Dia");

                for (int i = 0; i < dtDiasVia.Rows.Count; i++)
                {
                    dRow = dtDiasFecha.NewRow();
                    dRow["Dia"] = dtDiasVia.Rows[i]["FechaDia"].S().Dt().ToString("dd/MM/yyyy");
                    dtDiasFecha.Rows.Add(dRow);
                }
                dtDiasFecha.AcceptChanges();
                DataView dv = new DataView(dtDiasFecha);
                dtDiasDistinct = dv.ToTable(true, "Dia");

                if (dtDiasDistinct != null && dtDiasDistinct.Rows.Count > 0)
                {
                    DataRow drDias;
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Fecha");
                    dt.Columns.Add("TipoCambio");

                    dt.Columns.Add("HotelNac");
                    dt.Columns.Add("HotelInt");
                    dt.Columns.Add("Total");

                    //Tabla para resumen
                    DataRow drDiasResumen;
                    DataTable dtResumen = new DataTable();
                    dtResumen.Columns.Add("Id");
                    dtResumen.Columns.Add("IdPeriodo");
                    dtResumen.Columns.Add("Fecha");

                    dtResumen.Columns.Add("HotelNac");
                    dtResumen.Columns.Add("HotelInt");
                    dtResumen.Columns.Add("Total");
                    dtResumen.Columns.Add("Estatus");
                    dtResumen.Columns.Add("TipoCambio");


                    for (int i = 0; i < dtDiasVia.Rows.Count; i++)
                    {
                        dtDiasVia.Rows[i]["FechaDia"] = dtDiasVia.Rows[i]["FechaDia"].S().Dt().ToString("dd/MM/yyyy");
                    }
                    dtDiasVia.AcceptChanges();


                    //decimal dHotNac = 0;
                    //decimal dHotInt = 0;
                    //decimal dTotal = 0;

                    for (int i = 0; i < dtDiasDistinct.Rows.Count; i++)
                    {
                        //int iCountDesayuno = 0;
                        //int iCountComida = 0;
                        //int iCountCena = 0;

                        decimal dTipoCambio = 0;
                        decimal dHotNac = 0;
                        decimal dHotInt = 0;
                        decimal dTotal = 0;

                        drDias = dt.NewRow();
                        drDias["Fecha"] = dtDiasDistinct.Rows[i]["Dia"].S().Dt().Day.S() + " " + GetMes(dtDiasDistinct.Rows[i]["Dia"].S().Dt().Month).Substring(0, 3) + " " + dtDiasDistinct.Rows[i]["Dia"].S().Dt().Year.S();

                        string sFechaVuelo = string.Empty;
                        sFechaVuelo = dtDiasDistinct.Rows[i]["Dia"].S();

                        dTipoCambio = new DBCalculoPagos().ObtenerTipoCambio(sFechaVuelo);

                        DataRow[] dr = dtDiasVia.Select("FechaDia='" + dtDiasDistinct.Rows[i]["Dia"].S() + "'");

                        //dHotNac = 0;
                        //dHotInt = 0;
                        //dTotal = 0;

                        for (int x = 0; x < dr.Length; x++)
                        {
                            //identifica los hoteles especiales para asignar costos de hotel
                            for (int a = 0; a < dtAeroEspecial.Rows.Count; a++)
                            {
                                //Cuando los aeropuertos son especiales se utilizan distintos costos de hotel nacional
                                if (dr[x]["POA"].S() == dtAeroEspecial.Rows[a]["POA"].S())
                                {
                                    dHotNac += dr[x]["HotNal"].S().I() * dHotelSPMXN;
                                    break;
                                }
                                else if (a == dtAeroEspecial.Rows.Count - 1)
                                {
                                    //Si no hay aeropuertos especiales se asigna el costo normal de hotel
                                    dHotNac += dr[x]["HotNal"].S().I() * dHotelMXN;
                                }
                            }

                            for (int b = 0; b < dtAeroEspecial.Rows.Count; b++)
                            {
                                //Cuando los aeropuertos son especiales se utilizan distintos costos de hotel internacional
                                if (dr[x]["POA"].S() == dtAeroEspecial.Rows[b]["POA"].S())
                                {
                                    dHotInt += (dr[x]["HotInt"].S().I() * dHotelSPUSD) * dTipoCambio;
                                    break;
                                }
                                else if (b == dtAeroEspecial.Rows.Count - 1)
                                {
                                    dHotInt += (dr[x]["HotInt"].S().I() * dHotelUSD) * dTipoCambio;
                                }
                            }

                            dTotal = dHotNac + dHotInt;
                        }

                        drDias["TipoCambio"] = dTipoCambio.ToString("c") + " " + "MXN";

                        drDias["HotelNac"] = dHotNac.ToString("c") + " " + "MXN";
                        drDias["HotelInt"] = dHotInt.ToString("c") + " " + "MXN";
                        drDias["Total"] = dTotal.ToString("c") + " " + "MXN";
                        dt.Rows.Add(drDias);

                        //Tabla de Resumen
                        drDiasResumen = dtResumen.NewRow();
                        drDiasResumen["Id"] = 0;
                        drDiasResumen["IdPeriodo"] = 0;
                        drDiasResumen["Fecha"] = sFechaVuelo.Dt().ToString("yyyy-MM-dd HH:ss:fff");
                        drDiasResumen["HotelNac"] = dHotNac;
                        drDiasResumen["HotelInt"] = dHotInt;
                        drDiasResumen["Total"] = dTotal;
                        drDiasResumen["Estatus"] = 1;
                        drDiasResumen["TipoCambio"] = dTipoCambio;
                        dtResumen.Rows.Add(drDiasResumen);
                    }
                    dtResumenViaticosHotel = dtResumen;
                    gvViaticosHotelPorDia.DataSource = dt;
                    gvViaticosHotelPorDia.DataBind();


                    #region Llenado viaticos de hotel Nacional e Internacional
                    int iNac = 0;
                    int iNacSP = 0;
                    int iInt = 0;
                    int iIntSP = 0;

                    for (int i = 0; i < dtViaticosHot.Rows.Count; i++)
                    {
                        for (int a = 0; a < dtAeroEspecial.Rows.Count; a++)
                        {
                            //Cuando los aeropuertos son especiales se utilizan distintos costos de hotel nacional
                            if (dtViaticosHot.Rows[i]["POA"].S() == dtAeroEspecial.Rows[a]["POA"].S())
                            {
                                iNacSP += dtViaticosHot.Rows[i]["HotNal"].S().I();
                                break;
                            }
                        }
                        for (int b = 0; b < dtAeroEspecial.Rows.Count; b++)
                        {
                            //Cuando los aeropuertos son especiales se utilizan distintos costos de hotel internacional
                            if (dtViaticosHot.Rows[i]["POA"].S() == dtAeroEspecial.Rows[b]["POA"].S())
                            {
                                iIntSP += dtViaticosHot.Rows[i]["HotInt"].S().I();
                                break;
                            }
                        }
                        //Si no hay aeropuertos especiales se asigna hotel normal
                        if (iNacSP == 0)
                            iNac += dtViaticosHot.Rows[i]["HotNal"].S().I();

                        if (iIntSP == 0)
                            iInt += dtViaticosHot.Rows[i]["HotInt"].S().I();


                        //iNac += dtViaticosHot.Rows[i]["HotNal"].S().I();
                        //iInt += dtViaticosHot.Rows[i]["HotInt"].S().I();
                    }

                    decimal dTotalHotNac = 0;
                    decimal dTotalHotInt = 0;
                    decimal dTotalHotNacSP = 0;
                    decimal dTotalHotIntSP = 0;

                    dTotalHotNac = iNac * dHotelMXN;
                    dTotalHotInt = iInt * dHotelUSD;
                    dTotalHotNacSP = iNacSP * dHotelSPMXN;
                    dTotalHotIntSP = iIntSP * dHotelSPUSD;

                    decimal dTotalNacional = dTotalHotNac + dTotalHotNacSP;
                    decimal dTotalInternacional = dTotalHotInt + dTotalHotIntSP;


                    #region HOTEL NACIONALES E INTERNACIONALES PARA GUARDAR EN BD
                    //NACIONAL
                    dtHotNal = new DataTable();
                    dtHotNal.Columns.Add("CONCEPTO");
                    dtHotNal.Columns.Add("NACIONAL");

                    DataRow drN = dtHotNal.NewRow();
                    drN["CONCEPTO"] = "HOTEL";
                    drN["NACIONAL"] = iNac.S();
                    dtHotNal.Rows.Add(drN);

                    drN = dtHotNal.NewRow();
                    drN["CONCEPTO"] = "HOTEL ESPECIAL";
                    drN["NACIONAL"] = iNacSP.S();
                    dtHotNal.Rows.Add(drN);

                    drN = dtHotNal.NewRow();
                    drN["CONCEPTO"] = "TOTAL";
                    drN["NACIONAL"] = dTotalNacional.ToString("c") + " " + "MXN";
                    dtHotNal.Rows.Add(drN);

                    //INTERNACIONAL
                    dtHotInt = new DataTable();
                    dtHotInt.Columns.Add("CONCEPTO");
                    dtHotInt.Columns.Add("INTERNACIONAL");

                    DataRow drI = dtHotInt.NewRow();
                    drI["CONCEPTO"] = "HOTEL";
                    drI["INTERNACIONAL"] = iInt.S();
                    dtHotInt.Rows.Add(drI);

                    drI = dtHotInt.NewRow();
                    drI["CONCEPTO"] = "HOTEL ESPECIAL";
                    drI["INTERNACIONAL"] = iIntSP.S();
                    dtHotInt.Rows.Add(drI);

                    drI = dtHotInt.NewRow();
                    drI["CONCEPTO"] = "TOTAL";
                    drI["INTERNACIONAL"] = dTotalInternacional.ToString("c") + " " + "USD";
                    dtHotInt.Rows.Add(drI);

                    #endregion


                    DataTable dtVHotel = new DataTable();
                    dtVHotel.Columns.Add("CONCEPTO");
                    dtVHotel.Columns.Add("NACIONAL");
                    dtVHotel.Columns.Add("INTERNACIONAL");

                    DataRow drH = dtVHotel.NewRow();
                    drH["CONCEPTO"] = "HOTEL";
                    drH["NACIONAL"] = iNac.S();
                    drH["INTERNACIONAL"] = iInt.S();
                    dtVHotel.Rows.Add(drH);

                    drH = dtVHotel.NewRow();
                    drH["CONCEPTO"] = "HOTEL ESPECIAL";
                    drH["NACIONAL"] = iNacSP.S();
                    drH["INTERNACIONAL"] = iIntSP.S();
                    dtVHotel.Rows.Add(drH);

                    drH = dtVHotel.NewRow();
                    drH["CONCEPTO"] = "TOTAL";
                    drH["NACIONAL"] = dTotalNacional.ToString("c") + " " + "MXN";
                    drH["INTERNACIONAL"] = dTotalInternacional.ToString("c") + " " + "USD";
                    dtVHotel.Rows.Add(drH);

                    gvViaticosHotel.DataSource = dtVHotel;
                    gvViaticosHotel.DataBind();
                    #endregion

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Similar a AgruparDiasViaticos -----------------------------------------------------
        public void CargaViaticos2()
        {
            try
            {
                #region CÁLCULO
                decimal dDesNal = 0;
                decimal dDesInt = 0;
                decimal dComNal = 0;
                decimal dComInt = 0;
                decimal dCenNal = 0;
                decimal dCenInt = 0;

                foreach (DataRow row in dsParams.Tables[0].Rows)
                {

                    if (row["Concepto"].S() == "Desayuno")
                    {
                        dDesNal = row["MontoMXN"].S().D();
                        dDesInt = row["MontoUSD"].S().D();
                    }
                    if (row["Concepto"].S() == "Comida")
                    {
                        dComNal = row["MontoMXN"].S().D();
                        dComInt = row["MontoUSD"].S().D();
                    }
                    if (row["Concepto"].S() == "Cena")
                    {
                        dCenNal = row["MontoMXN"].S().D();
                        dCenInt = row["MontoUSD"].S().D();
                    }
                }

                //if (!dtCalculos.Columns.Contains("TotalPesos"))
                //{
                //    dtCalculos.Columns.Add("TotalPesos", typeof(decimal));
                //    dtCalculos.Columns["TotalPesos"].ReadOnly = false;
                //}

                //if (!dtCalculos.Columns.Contains("TotalUSD"))
                //{
                //    dtCalculos.Columns.Add("TotalUSD", typeof(decimal));
                //    dtCalculos.Columns["TotalUSD"].ReadOnly = false;
                //}


                //foreach (DataRow row in dtCalculos.Rows)
                //{
                //    decimal dTotalNal = 0;
                //    decimal dTotalInt = 0;

                //    dTotalNal += row["DesayunosNal"].S().D() * dDesNal;
                //    dTotalNal += row["ComidasNal"].S().D() * dComNal;
                //    dTotalNal += row["CenasNal"].S().D() * dCenNal;

                //    dTotalInt += row["DesayunosInt"].S().D() * dDesInt;
                //    dTotalInt += row["ComidasInt"].S().D() * dComInt;
                //    dTotalInt += row["CenasInt"].S().D() * dCenInt;


                //    row["TotalPesos"] = dTotalNal;
                //    row["TotalUSD"] = dTotalInt;
                //}
                #endregion

                #region NACIONALES
                //dtNal = new DataTable();
                //dtNal.Columns.Add("CONCEPTO");
                //dtNal.Columns.Add("NACIONAL");

                //DataRow dr = dtNal.NewRow();
                //dr["CONCEPTO"] = "DESAYUNO";
                //dr["NACIONAL"] = dtCalculos.Rows[0]["DesayunosNal"].S();
                //dtNal.Rows.Add(dr);

                //dr = dtNal.NewRow();
                //dr["CONCEPTO"] = "COMIDA";
                //dr["NACIONAL"] = dtCalculos.Rows[0]["ComidasNal"].S();
                //dtNal.Rows.Add(dr);

                //dr = dtNal.NewRow();
                //dr["CONCEPTO"] = "CENA";
                //dr["NACIONAL"] = dtCalculos.Rows[0]["CenasNal"].S();
                //dtNal.Rows.Add(dr);

                //dr = dtNal.NewRow();
                //dr["CONCEPTO"] = "TOTAL";
                //dr["NACIONAL"] = dtCalculos.Rows[0]["TotalPesos"].S().D().ToString("c");
                //dtNal.Rows.Add(dr);

                //gvNacionales.DataSource = dtNal;
                //gvNacionales.DataBind();
                #endregion

                #region INTERNACIONALES
                //dtInt = new DataTable();
                //dtInt.Columns.Add("CONCEPTO");
                //dtInt.Columns.Add("INTERNACIONAL");

                //DataRow dr2 = dtInt.NewRow();
                //dr2["CONCEPTO"] = "DESAYUNO";
                //dr2["INTERNACIONAL"] = dtCalculos.Rows[0]["DesayunosInt"].S();
                //dtInt.Rows.Add(dr2);

                //dr2 = dtInt.NewRow();
                //dr2["CONCEPTO"] = "COMIDA";
                //dr2["INTERNACIONAL"] = dtCalculos.Rows[0]["ComidasInt"].S();
                //dtInt.Rows.Add(dr2);

                //dr2 = dtInt.NewRow();
                //dr2["CONCEPTO"] = "CENA";
                //dr2["INTERNACIONAL"] = dtCalculos.Rows[0]["CenasInt"].S();
                //dtInt.Rows.Add(dr2);

                //dr2 = dtInt.NewRow();
                //dr2["CONCEPTO"] = "TOTAL";
                //dr2["INTERNACIONAL"] = dtCalculos.Rows[0]["TotalUSD"].S().D().ToString("c") + " " + "USD";
                //dtInt.Rows.Add(dr2);

                //gvInternacionales.DataSource = dtInt;
                //gvInternacionales.DataBind();
                #endregion

                #region Llenado general PESOS Y DOLARES
                //dtMXNUSD = new DataTable();
                //dtMXNUSD.Columns.Add("CONCEPTO");
                //dtMXNUSD.Columns.Add("NACIONAL");
                //dtMXNUSD.Columns.Add("INTERNACIONAL");

                //DataRow dr3 = dtMXNUSD.NewRow();
                //dr3["CONCEPTO"] = "DESAYUNO";
                //dr3["NACIONAL"] = dtCalculos.Rows[0]["DesayunosNal"].S();
                //dr3["INTERNACIONAL"] = dtCalculos.Rows[0]["DesayunosInt"].S();
                //dtMXNUSD.Rows.Add(dr3);

                //dr3 = dtMXNUSD.NewRow();
                //dr3["CONCEPTO"] = "COMIDA";
                //dr3["NACIONAL"] = dtCalculos.Rows[0]["ComidasNal"].S();
                //dr3["INTERNACIONAL"] = dtCalculos.Rows[0]["ComidasInt"].S();
                //dtMXNUSD.Rows.Add(dr3);

                //dr3 = dtMXNUSD.NewRow();
                //dr3["CONCEPTO"] = "CENA";
                //dr3["NACIONAL"] = dtCalculos.Rows[0]["CenasNal"].S();
                //dr3["INTERNACIONAL"] = dtCalculos.Rows[0]["CenasInt"].S();
                //dtMXNUSD.Rows.Add(dr3);

                //dr3 = dtMXNUSD.NewRow();
                //dr3["CONCEPTO"] = "TOTAL";
                //dr3["NACIONAL"] = dtCalculos.Rows[0]["TotalPesos"].S().D().ToString("c") + " " + "MXN";
                //dr3["INTERNACIONAL"] = dtCalculos.Rows[0]["TotalUSD"].S().D().ToString("c") + " " + "USD";
                //dtMXNUSD.Rows.Add(dr3);

                //gvMXNUSD.DataSource = dtMXNUSD;
                //gvMXNUSD.DataBind();
                #endregion

                //pnlVuelos.Visible = false;

                //------------------------------------------------------------------------------

                if (dtDiasViaticos != null && dtDiasViaticos.Rows.Count > 0)
                {
                    DataRow drow;
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Fecha");
                    dt.Columns.Add("Moneda");
                    dt.Columns.Add("Desayuno");
                    dt.Columns.Add("Comida");
                    dt.Columns.Add("Cena");
                    dt.Columns.Add("Total");

                    for (int i = 0; i < dtDiasViaticos.Rows.Count; i++)
                    {
                        for (int x = 0; x < 2; x++)
                        {
                            if (x == 0)
                            {
                                decimal dTotalDesNal = 0;
                                decimal dTotalComNal = 0;
                                decimal dTotalCenNal = 0;
                                decimal dTotalNal = 0;

                                dTotalDesNal = dtDiasViaticos.Rows[i]["DesNal"].S().D() * ObtenValorConcepto(1, "MXN");
                                dTotalComNal = dtDiasViaticos.Rows[i]["ComNal"].S().D() * ObtenValorConcepto(2, "MXN");
                                dTotalCenNal = dtDiasViaticos.Rows[i]["CenNal"].S().D() * ObtenValorConcepto(3, "MXN");
                                dTotalNal = ((dTotalDesNal + dTotalComNal) + dTotalCenNal);

                                drow = dt.NewRow();
                                drow["Fecha"] = dtDiasViaticos.Rows[i]["FechaDia"].S().Dt();
                                drow["Moneda"] = "MXN";
                                drow["Desayuno"] = dTotalDesNal.ToString("c") + " " + "MXN";
                                drow["Comida"] = dTotalComNal.ToString("c") + " " + "MXN";
                                drow["Cena"] = dTotalCenNal.ToString("c") + " " + "MXN";
                                drow["Total"] = dTotalNal.ToString("c") + " " + "MXN";
                            }
                            else
                            {
                                decimal dTotalDesInt = 0;
                                decimal dTotalComInt = 0;
                                decimal dTotalCenInt = 0;
                                decimal dTotalInt = 0;

                                dTotalDesInt = dtDiasViaticos.Rows[i]["DesInt"].S().D() * ObtenValorConcepto(1, "USD");
                                dTotalComInt = dtDiasViaticos.Rows[i]["ComInt"].S().D() * ObtenValorConcepto(2, "USD");
                                dTotalCenInt = dtDiasViaticos.Rows[i]["CenInt"].S().D() * ObtenValorConcepto(3, "USD");
                                dTotalInt = ((dTotalDesInt + dTotalComInt) + dTotalCenInt);

                                drow = dt.NewRow();
                                drow["Fecha"] = dtDiasViaticos.Rows[i]["FechaDia"].S().Dt();
                                drow["Moneda"] = "USD";
                                drow["Desayuno"] = dTotalDesInt.ToString("c") + " " + "USD";
                                drow["Comida"] = dTotalComInt.ToString("c") + " " + "USD";
                                drow["Cena"] = dTotalCenInt.ToString("c") + " " + "USD";
                                drow["Total"] = dTotalInt.ToString("c") + " " + "USD";
                            }
                            dt.Rows.Add(drow);
                        }


                    }

                    dtViaticosDiaInsert = null;
                    dtViaticosDiaInsert = dt;
                    //gvConteoDias.DataSource = dt;
                    //gvConteoDias.DataBind();

                    //Mostrar conteo dias-viaticos
                    AgruparDiasViaticos2(dtDiasViaticos);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void AgruparDiasViaticos2(DataTable dtDiasVia)
        {
            try
            {
                decimal dDesayunoMXN = 0;
                decimal dDesayunoUSD = 0;
                decimal dComidaMXN = 0;
                decimal dComidaUSD = 0;
                decimal dCenaMXN = 0;
                decimal dCenaUSD = 0;
                dtResumenViaticos = null;

                if (dsParams.Tables[0] != null && dsParams.Tables[0].Rows.Count > 0)
                {
                    dDesayunoMXN = dsParams.Tables[0].Rows[0]["MontoMXN"].S().D();
                    dDesayunoUSD = dsParams.Tables[0].Rows[0]["MontoUSD"].S().D();
                    dComidaMXN = dsParams.Tables[0].Rows[1]["MontoMXN"].S().D();
                    dComidaUSD = dsParams.Tables[0].Rows[1]["MontoUSD"].S().D();
                    dCenaMXN = dsParams.Tables[0].Rows[2]["MontoMXN"].S().D();
                    dCenaUSD = dsParams.Tables[0].Rows[2]["MontoUSD"].S().D();
                }


                DataRow dRow;
                DataTable dtDiasDistinct = new DataTable();
                DataTable dtDiasFecha = new DataTable();
                dtDiasFecha.Columns.Add("Dia");

                for (int i = 0; i < dtDiasVia.Rows.Count; i++)
                {
                    dRow = dtDiasFecha.NewRow();
                    dRow["Dia"] = dtDiasVia.Rows[i]["FechaDia"].S().Dt().ToString("dd/MM/yyyy");
                    dtDiasFecha.Rows.Add(dRow);
                }
                dtDiasFecha.AcceptChanges();
                DataView dv = new DataView(dtDiasFecha);
                dtDiasDistinct = dv.ToTable(true, "Dia"); //Datatable de dias o fechas

                if (dtDiasDistinct != null && dtDiasDistinct.Rows.Count > 0)
                {
                    DataRow drDias;
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Fecha");
                    dt.Columns.Add("TipoCambio");
                    dt.Columns.Add("Desayuno");
                    dt.Columns.Add("Comida");
                    dt.Columns.Add("Cena");

                    dt.Columns.Add("DesayunoNac");
                    dt.Columns.Add("DesayunoInt");
                    dt.Columns.Add("ComidaNac");
                    dt.Columns.Add("ComidaInt");
                    dt.Columns.Add("CenaNac");
                    dt.Columns.Add("CenaInt");
                    dt.Columns.Add("Total");

                    //Tabla para resumen
                    DataRow drDiasResumen;
                    DataTable dtResumen = new DataTable();
                    dtResumen.Columns.Add("Id");
                    dtResumen.Columns.Add("IdPeriodo");
                    dtResumen.Columns.Add("Fecha");
                    dtResumen.Columns.Add("Desayuno");
                    dtResumen.Columns.Add("Comida");
                    dtResumen.Columns.Add("Cena");
                    
                    dtResumen.Columns.Add("DesNac");
                    dtResumen.Columns.Add("DesInt");
                    dtResumen.Columns.Add("ComNac");
                    dtResumen.Columns.Add("ComInt");
                    dtResumen.Columns.Add("CenNac");
                    dtResumen.Columns.Add("CenInt");
                    dtResumen.Columns.Add("Total");
                    dtResumen.Columns.Add("Estatus");
                    dtResumen.Columns.Add("TipoCambio");

                    for (int i = 0; i < dtDiasVia.Rows.Count; i++)
                    {
                        dtDiasVia.Rows[i]["FechaDia"] = dtDiasVia.Rows[i]["FechaDia"].S().Dt().ToString("dd/MM/yyyy");
                    }
                    dtDiasVia.AcceptChanges();

                    for (int i = 0; i < dtDiasDistinct.Rows.Count; i++)
                    {
                        int iCountDesayuno = 0;
                        int iCountComida = 0;
                        int iCountCena = 0;

                        decimal dTipoCambio = 0;
                        decimal dDesNac = 0;
                        decimal dDesInt = 0;
                        decimal dComNac = 0;
                        decimal dComInt = 0;
                        decimal dCenNac = 0;
                        decimal dCenInt = 0;
                        decimal dTotal = 0;
                        string sFechaVuelo = string.Empty;
                        sFechaVuelo = dtDiasDistinct.Rows[i]["Dia"].S();
                        dTipoCambio = new DBCalculoPagos().ObtenerTipoCambio(sFechaVuelo);

                        DataRow[] dr = dtDiasVia.Select("FechaDia='" + dtDiasDistinct.Rows[i]["Dia"].S() + "'");

                        for (int x = 0; x < dr.Length; x++)
                        {
                            dDesNac = 0;
                            dDesInt = 0;
                            dComNac = 0;
                            dComInt = 0;
                            dCenNac = 0;
                            dCenInt = 0;
                            dTotal = 0;

                            iCountDesayuno += dr[x]["DesNal"].S().I() + dr[x]["DesInt"].S().I();
                            iCountComida += dr[x]["ComNal"].S().I() + dr[x]["ComInt"].S().I();
                            iCountCena += dr[x]["CenNal"].S().I() + dr[x]["CenInt"].S().I();

                            dDesNac = dr[x]["DesNal"].S().I() * dDesayunoMXN;
                            dDesInt = (dr[x]["DesInt"].S().I() * dDesayunoUSD) * dTipoCambio;

                            dComNac = dr[x]["ComNal"].S().I() * dComidaMXN;
                            dComInt = (dr[x]["ComInt"].S().I() * dComidaUSD) * dTipoCambio;

                            dCenNac = dr[x]["CenNal"].S().I() * dCenaMXN;
                            dCenInt = (dr[x]["CenInt"].S().I() * dCenaUSD) * dTipoCambio;

                            dTotal = ((((dDesNac + dDesInt) + dComNac) + dComInt) + dCenNac) + dCenInt;
                        }

                        drDias = dt.NewRow();
                        drDias["Fecha"] = dtDiasDistinct.Rows[i]["Dia"].S().Dt().Day.S() + " " + GetMes(dtDiasDistinct.Rows[i]["Dia"].S().Dt().Month).Substring(0, 3) + " " + dtDiasDistinct.Rows[i]["Dia"].S().Dt().Year.S();
                        drDias["TipoCambio"] = dTipoCambio.ToString("c") + " " + "MXN";
                        drDias["Desayuno"] = iCountDesayuno;
                        drDias["Comida"] = iCountComida;
                        drDias["Cena"] = iCountCena;
                        drDias["DesayunoNac"] = dDesNac.ToString("c") + " " + "MXN";
                        drDias["DesayunoInt"] = dDesInt.ToString("c") + " " + "MXN";
                        drDias["ComidaNac"] = dComNac.ToString("c") + " " + "MXN";
                        drDias["ComidaInt"] = dComInt.ToString("c") + " " + "MXN";
                        drDias["CenaNac"] = dCenNac.ToString("c") + " " + "MXN";
                        drDias["CenaInt"] = dCenInt.ToString("c") + " " + "MXN";
                        drDias["Total"] = dTotal.ToString("c") + " " + "MXN";
                        dt.Rows.Add(drDias);

                        //Tabla de Resumen
                        drDiasResumen = dtResumen.NewRow();
                        drDiasResumen["Id"] = 0;
                        drDiasResumen["IdPeriodo"] = 0;
                        drDiasResumen["Fecha"] = sFechaVuelo.Dt().ToString("yyyy-MM-dd HH:ss:fff");
                        drDiasResumen["Desayuno"] = iCountDesayuno;
                        drDiasResumen["Comida"] = iCountComida;
                        drDiasResumen["Cena"] = iCountCena;
                        drDiasResumen["DesNac"] = dDesNac;
                        drDiasResumen["DesInt"] = dDesInt;
                        drDiasResumen["ComNac"] = dComNac;
                        drDiasResumen["ComInt"] = dComInt;
                        drDiasResumen["CenNac"] = dCenNac;
                        drDiasResumen["CenInt"] = dCenInt;
                        drDiasResumen["Total"] = dTotal;
                        drDiasResumen["Estatus"] = 1;
                        drDiasResumen["TipoCambio"] = dTipoCambio;
                        dtResumen.Rows.Add(drDiasResumen);
                    }
                    dtResumenViaticos = dtResumen;
                    //gvDiasViaticos.DataSource = dt;
                    //gvDiasViaticos.DataBind();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //Carga viaticos de hotel para guardar todos los pilotos
        public void CargaViaticosHotel()
        {
            try
            {
                if (dtViaticosHot != null && dtViaticosHot.Rows.Count > 0)
                {
                    DataRow drowH;
                    DataTable dtH = new DataTable();
                    dtH.Columns.Add("Fecha");
                    dtH.Columns.Add("Moneda");
                    dtH.Columns.Add("HotNac");
                    dtH.Columns.Add("HotInt");
                    dtH.Columns.Add("Total");

                    for (int i = 0; i < dtViaticosHot.Rows.Count; i++)
                    {
                        for (int x = 0; x < 2; x++)
                        {
                            if (x == 0)
                            {
                                decimal dTotalHotNal = 0;
                                decimal dTotalNal = 0;

                                for (int a = 0; a < dsParamsH.Tables[1].Rows.Count; a++)
                                {
                                    if (dtViaticosHot.Rows[i]["POA"].S() == dsParamsH.Tables[1].Rows[a]["POA"].S())
                                    {
                                        dTotalHotNal = dtViaticosHot.Rows[i]["HotNal"].S().D() * ObtenValorHotel(2, "MXN");
                                        break;
                                    }
                                }

                                if (dTotalHotNal == 0)
                                    dTotalHotNal = dtViaticosHot.Rows[i]["HotNal"].S().D() * ObtenValorHotel(1, "MXN");

                                dTotalNal = dTotalHotNal;

                                drowH = dtH.NewRow();
                                drowH["Fecha"] = dtViaticosHot.Rows[i]["FechaDia"].S().Dt();
                                drowH["Moneda"] = "MXN";
                                drowH["HotNac"] = dTotalHotNal.ToString("c") + " " + "MXN";
                                drowH["HotInt"] = (0).ToString("c") + " " + "USD";
                                drowH["Total"] = dTotalNal.ToString("c") + " " + "MXN";
                            }
                            else
                            {
                                decimal dTotalHotInt = 0;
                                decimal dTotalInt = 0;

                                for (int a = 0; a < dsParamsH.Tables[1].Rows.Count; a++)
                                {
                                    if (dtViaticosHot.Rows[i]["POA"].S() == dsParamsH.Tables[1].Rows[a]["POA"].S())
                                    {
                                        dTotalHotInt = dtViaticosHot.Rows[i]["HotInt"].S().D() * ObtenValorHotel(2, "USD");
                                        break;
                                    }
                                }

                                if (dTotalHotInt == 0)
                                    dTotalHotInt = dtViaticosHot.Rows[i]["HotInt"].S().D() * ObtenValorHotel(1, "USD");

                                dTotalInt = dTotalHotInt;

                                drowH = dtH.NewRow();
                                drowH["Fecha"] = dtViaticosHot.Rows[i]["FechaDia"].S().Dt();
                                drowH["Moneda"] = "USD";
                                drowH["HotNac"] = (0).ToString("c") + " " + "MXN";
                                drowH["HotInt"] = dTotalHotInt.ToString("c") + " " + "USD";
                                drowH["Total"] = dTotalInt.ToString("c") + " " + "USD";
                            }
                            dtH.Rows.Add(drowH);
                        }
                    }

                    dtViaticosHotelDiaInsert = null;
                    dtViaticosHotelDiaInsert = dtH;
                    AgruparDiasGastosHotelSave(dtViaticosHot);
                }




                    
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void AgruparDiasGastosHotelSave(DataTable dtDiasVia)
        {
            try
            {
                decimal dHotelMXN = 0;
                decimal dHotelUSD = 0;
                decimal dHotelSPMXN = 0;
                decimal dHotelSPUSD = 0;
                DataTable dtAeroEspecial = new DataTable();

                if (dsParamsH.Tables[1] != null && dsParamsH.Tables[1].Rows.Count > 0)
                {
                    dtAeroEspecial = dsParamsH.Tables[1];
                }

                if (dsParamsH.Tables[0] != null && dsParamsH.Tables[0].Rows.Count > 0)
                {
                    dHotelMXN = dsParamsH.Tables[0].Rows[0]["MontoMXN"].S().D();
                    dHotelUSD = dsParamsH.Tables[0].Rows[0]["MontoUSD"].S().D();

                    dHotelSPMXN = dsParamsH.Tables[0].Rows[1]["MontoMXN"].S().D();
                    dHotelSPUSD = dsParamsH.Tables[0].Rows[1]["MontoUSD"].S().D();
                }

                DataRow dRow;
                DataTable dtDiasDistinct = new DataTable();
                DataTable dtDiasFecha = new DataTable();
                dtDiasFecha.Columns.Add("Dia");

                for (int i = 0; i < dtDiasVia.Rows.Count; i++)
                {
                    dRow = dtDiasFecha.NewRow();
                    dRow["Dia"] = dtDiasVia.Rows[i]["FechaDia"].S().Dt().ToString("dd/MM/yyyy");
                    dtDiasFecha.Rows.Add(dRow);
                }
                dtDiasFecha.AcceptChanges();
                DataView dv = new DataView(dtDiasFecha);
                dtDiasDistinct = dv.ToTable(true, "Dia");

                if (dtDiasDistinct != null && dtDiasDistinct.Rows.Count > 0)
                {
                    DataRow drDias;
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Fecha");
                    dt.Columns.Add("TipoCambio");

                    dt.Columns.Add("HotelNac");
                    dt.Columns.Add("HotelInt");
                    dt.Columns.Add("Total");

                    //Tabla para resumen
                    DataRow drDiasResumen;
                    DataTable dtResumen = new DataTable();
                    dtResumen.Columns.Add("Id");
                    dtResumen.Columns.Add("IdPeriodo");
                    dtResumen.Columns.Add("Fecha");

                    dtResumen.Columns.Add("HotelNac");
                    dtResumen.Columns.Add("HotelInt");
                    dtResumen.Columns.Add("Total");
                    dtResumen.Columns.Add("Estatus");
                    dtResumen.Columns.Add("TipoCambio");


                    for (int i = 0; i < dtDiasVia.Rows.Count; i++)
                    {
                        dtDiasVia.Rows[i]["FechaDia"] = dtDiasVia.Rows[i]["FechaDia"].S().Dt().ToString("dd/MM/yyyy");
                    }
                    dtDiasVia.AcceptChanges();


                    //decimal dHotNac = 0;
                    //decimal dHotInt = 0;
                    //decimal dTotal = 0;

                    for (int i = 0; i < dtDiasDistinct.Rows.Count; i++)
                    {
                        //int iCountDesayuno = 0;
                        //int iCountComida = 0;
                        //int iCountCena = 0;

                        decimal dTipoCambio = 0;
                        decimal dHotNac = 0;
                        decimal dHotInt = 0;
                        decimal dTotal = 0;

                        drDias = dt.NewRow();
                        drDias["Fecha"] = dtDiasDistinct.Rows[i]["Dia"].S().Dt().Day.S() + " " + GetMes(dtDiasDistinct.Rows[i]["Dia"].S().Dt().Month).Substring(0, 3) + " " + dtDiasDistinct.Rows[i]["Dia"].S().Dt().Year.S();

                        string sFechaVuelo = string.Empty;
                        sFechaVuelo = dtDiasDistinct.Rows[i]["Dia"].S();

                        dTipoCambio = new DBCalculoPagos().ObtenerTipoCambio(sFechaVuelo);

                        DataRow[] dr = dtDiasVia.Select("FechaDia='" + dtDiasDistinct.Rows[i]["Dia"].S() + "'");

                        //dHotNac = 0;
                        //dHotInt = 0;
                        //dTotal = 0;

                        for (int x = 0; x < dr.Length; x++)
                        {
                            //identifica los hoteles especiales para asignar costos de hotel
                            for (int a = 0; a < dtAeroEspecial.Rows.Count; a++)
                            {
                                //Cuando los aeropuertos son especiales se utilizan distintos costos de hotel nacional
                                if (dr[x]["POA"].S() == dtAeroEspecial.Rows[a]["POA"].S())
                                {
                                    dHotNac += dr[x]["HotNal"].S().I() * dHotelSPMXN;
                                    break;
                                }
                                else if (a == dtAeroEspecial.Rows.Count - 1)
                                {
                                    //Si no hay aeropuertos especiales se asigna el costo normal de hotel
                                    dHotNac += dr[x]["HotNal"].S().I() * dHotelMXN;
                                }
                            }

                            for (int b = 0; b < dtAeroEspecial.Rows.Count; b++)
                            {
                                //Cuando los aeropuertos son especiales se utilizan distintos costos de hotel internacional
                                if (dr[x]["POA"].S() == dtAeroEspecial.Rows[b]["POA"].S())
                                {
                                    dHotInt += (dr[x]["HotInt"].S().I() * dHotelSPUSD) * dTipoCambio;
                                    break;
                                }
                                else if (b == dtAeroEspecial.Rows.Count - 1)
                                {
                                    dHotInt += (dr[x]["HotInt"].S().I() * dHotelUSD) * dTipoCambio;
                                }
                            }
                            dTotal = dHotNac + dHotInt;
                        }

                        drDias["TipoCambio"] = dTipoCambio.ToString("c") + " " + "MXN";

                        drDias["HotelNac"] = dHotNac.ToString("c") + " " + "MXN";
                        drDias["HotelInt"] = dHotInt.ToString("c") + " " + "MXN";
                        drDias["Total"] = dTotal.ToString("c") + " " + "MXN";
                        dt.Rows.Add(drDias);

                        //Tabla de Resumen
                        drDiasResumen = dtResumen.NewRow();
                        drDiasResumen["Id"] = 0;
                        drDiasResumen["IdPeriodo"] = 0;
                        drDiasResumen["Fecha"] = sFechaVuelo.Dt().ToString("yyyy-MM-dd HH:ss:fff");
                        drDiasResumen["HotelNac"] = dHotNac;
                        drDiasResumen["HotelInt"] = dHotInt;
                        drDiasResumen["Total"] = dTotal;
                        drDiasResumen["Estatus"] = 1;
                        drDiasResumen["TipoCambio"] = dTipoCambio;
                        dtResumen.Rows.Add(drDiasResumen);
                    }
                    dtResumenViaticosHotel = dtResumen;
                    //gvViaticosHotelPorDia.DataSource = dt;
                    //gvViaticosHotelPorDia.DataBind();


                    #region Llenado viaticos de hotel Nacional e Internacional
                    int iNac = 0;
                    int iNacSP = 0;
                    int iInt = 0;
                    int iIntSP = 0;

                    for (int i = 0; i < dtViaticosHot.Rows.Count; i++)
                    {
                        for (int a = 0; a < dtAeroEspecial.Rows.Count; a++)
                        {
                            //Cuando los aeropuertos son especiales se utilizan distintos costos de hotel nacional
                            if (dtViaticosHot.Rows[i]["POA"].S() == dtAeroEspecial.Rows[a]["POA"].S())
                            {
                                iNacSP += dtViaticosHot.Rows[i]["HotNal"].S().I();
                                break;
                            }
                        }
                        for (int b = 0; b < dtAeroEspecial.Rows.Count; b++)
                        {
                            //Cuando los aeropuertos son especiales se utilizan distintos costos de hotel internacional
                            if (dtViaticosHot.Rows[i]["POA"].S() == dtAeroEspecial.Rows[b]["POA"].S())
                            {
                                iIntSP += dtViaticosHot.Rows[i]["HotInt"].S().I();
                                break;
                            }
                        }
                        //Si no hay aeropuertos especiales se asigna hotel normal
                        if (iNacSP == 0)
                            iNac += dtViaticosHot.Rows[i]["HotNal"].S().I();

                        if (iIntSP == 0)
                            iInt += dtViaticosHot.Rows[i]["HotInt"].S().I();


                        //iNac += dtViaticosHot.Rows[i]["HotNal"].S().I();
                        //iInt += dtViaticosHot.Rows[i]["HotInt"].S().I();
                    }

                    decimal dTotalHotNac = 0;
                    decimal dTotalHotInt = 0;
                    decimal dTotalHotNacSP = 0;
                    decimal dTotalHotIntSP = 0;

                    dTotalHotNac = iNac * dHotelMXN;
                    dTotalHotInt = iInt * dHotelUSD;
                    dTotalHotNacSP = iNacSP * dHotelSPMXN;
                    dTotalHotIntSP = iIntSP * dHotelSPUSD;

                    decimal dTotalNacional = dTotalHotNac + dTotalHotNacSP;
                    decimal dTotalInternacional = dTotalHotInt + dTotalHotIntSP;


                    #region HOTEL NACIONALES E INTERNACIONALES PARA GUARDAR EN BD
                    //NACIONAL
                    dtHotNal = new DataTable();
                    dtHotNal.Columns.Add("CONCEPTO");
                    dtHotNal.Columns.Add("NACIONAL");

                    DataRow drN = dtHotNal.NewRow();
                    drN["CONCEPTO"] = "HOTEL";
                    drN["NACIONAL"] = iNac.S();
                    dtHotNal.Rows.Add(drN);

                    drN = dtHotNal.NewRow();
                    drN["CONCEPTO"] = "HOTEL ESPECIAL";
                    drN["NACIONAL"] = iNacSP.S();
                    dtHotNal.Rows.Add(drN);

                    drN = dtHotNal.NewRow();
                    drN["CONCEPTO"] = "TOTAL";
                    drN["NACIONAL"] = dTotalNacional.ToString("c") + " " + "MXN";
                    dtHotNal.Rows.Add(drN);

                    //INTERNACIONAL
                    dtHotInt = new DataTable();
                    dtHotInt.Columns.Add("CONCEPTO");
                    dtHotInt.Columns.Add("INTERNACIONAL");

                    DataRow drI = dtHotInt.NewRow();
                    drI["CONCEPTO"] = "HOTEL";
                    drI["INTERNACIONAL"] = iInt.S();
                    dtHotInt.Rows.Add(drI);

                    drI = dtHotInt.NewRow();
                    drI["CONCEPTO"] = "HOTEL ESPECIAL";
                    drI["INTERNACIONAL"] = iIntSP.S();
                    dtHotInt.Rows.Add(drI);

                    drI = dtHotInt.NewRow();
                    drI["CONCEPTO"] = "TOTAL";
                    drI["INTERNACIONAL"] = dTotalInternacional.ToString("c") + " " + "USD";
                    dtHotInt.Rows.Add(drI);

                    #endregion


                    DataTable dtVHotel = new DataTable();
                    dtVHotel.Columns.Add("CONCEPTO");
                    dtVHotel.Columns.Add("NACIONAL");
                    dtVHotel.Columns.Add("INTERNACIONAL");

                    DataRow drH = dtVHotel.NewRow();
                    drH["CONCEPTO"] = "HOTEL";
                    drH["NACIONAL"] = iNac.S();
                    drH["INTERNACIONAL"] = iInt.S();
                    dtVHotel.Rows.Add(drH);

                    drH = dtVHotel.NewRow();
                    drH["CONCEPTO"] = "HOTEL ESPECIAL";
                    drH["NACIONAL"] = iNacSP.S();
                    drH["INTERNACIONAL"] = iIntSP.S();
                    dtVHotel.Rows.Add(drH);

                    drH = dtVHotel.NewRow();
                    drH["CONCEPTO"] = "TOTAL";
                    drH["NACIONAL"] = dTotalNacional.ToString("c") + " " + "MXN";
                    drH["INTERNACIONAL"] = dTotalInternacional.ToString("c") + " " + "USD";
                    dtVHotel.Rows.Add(drH);

                    //gvViaticosHotel.DataSource = dtVHotel;
                    //gvViaticosHotel.DataBind();
                    #endregion

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //-----------------------------------------------------------------------------------

        public decimal ObtenValorConcepto(int IdConcepto, string sMoneda)
        {
            try
            {
                decimal dValor = 0;
                DataRow[] dr = dtConceptos.Select("IdConcepto=" + IdConcepto);

                if (sMoneda == "MXN")
                {
                    dValor = dr[0]["MontoMXN"].S().D();
                }
                else if (sMoneda == "USD")
                {
                    dValor = dr[0]["MontoUSD"].S().D();
                }

                return dValor;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public decimal ObtenValorHotel(int IdHotel, string sMoneda)
        {
            try
            {
                decimal dValor = 0;
                dtHoteles = dsParamsH.Tables[0];
                DataRow[] dr = dtHoteles.Select("IdHotel=" + IdHotel);

                if (sMoneda == "MXN")
                {
                    dValor = dr[0]["MontoMXN"].S().D();
                }
                else if (sMoneda == "USD")
                {
                    dValor = dr[0]["MontoUSD"].S().D();
                }

                return dValor;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public void LlenaVuelosPiloto(DataTable dt)
        {
            dtVuelosPiloto = null;
            dtVuelosPiloto = dt;

            if (dtVuelosPiloto1x1 == null || dtVuelosPiloto1x1.Rows.Count == 0)
                dtVuelosPiloto1x1 = dt;

            gvVuelos.DataSource = dtVuelosPiloto;
            gvVuelos.DataBind();
        }
        private void GeneraCalculo()
        {
            try
            {
                decimal dDesNal = 0;
                decimal dDesInt = 0;
                decimal dComNal = 0;
                decimal dComInt = 0;
                decimal dCenNal = 0;
                decimal dCenInt = 0;

                foreach (DataRow row in dsParams.Tables[0].Rows)
                {
                    if (row["Concepto"].S() == "Desayuno")
                    {
                        dDesNal = row["MontoMXN"].S().D();
                        dDesInt = row["MontoUSD"].S().D();
                    }
                    if (row["Concepto"].S() == "Comida")
                    {
                        dComNal = row["MontoMXN"].S().D();
                        dComInt = row["MontoUSD"].S().D();
                    }
                    if (row["Concepto"].S() == "Cena")
                    {
                        dCenNal = row["MontoMXN"].S().D();
                        dCenInt = row["MontoUSD"].S().D();
                    }
                }

                dtCalculos.Columns.Add("TotalPesos", typeof(decimal));
                dtCalculos.Columns.Add("TotalUSD", typeof(decimal));

                dtCalculos.Columns["TotalPesos"].ReadOnly = false;
                dtCalculos.Columns["TotalUSD"].ReadOnly = false;


                foreach (DataRow row in dtCalculos.Rows)
                {
                    decimal dTotalNal = 0;
                    decimal dTotalInt = 0;

                    dTotalNal += row["DesayunosNal"].S().D() * dDesNal;
                    dTotalNal += row["ComidasNal"].S().D() * dComNal;
                    dTotalNal += row["CenasNal"].S().D() * dCenNal;

                    dTotalInt += row["DesayunosInt"].S().D() * dDesInt;
                    dTotalInt += row["ComidasInt"].S().D() * dComInt;
                    dTotalInt += row["CenasInt"].S().D() * dCenInt;


                    row["TotalPesos"] = dTotalNal;
                    row["TotalUSD"] = dTotalInt;
                }

                //Se crea tabla para mostrar viaticos
                string sHtml = string.Empty;
                string sTablaNacional = string.Empty;
                string sTablaExtranjera = string.Empty;

                if (dtCalculos != null && dtCalculos.Rows.Count > 0)
                {
                    //MXN columna 4,6,8
                    sTablaNacional = "<table border='1' width='40%' style='border-radius:4px; border: 1px solid #ccc;'>";
                    sTablaNacional += "  <tr>";
                    sTablaNacional += "      <td colspan='2' style='background-color:#ccc; text-align:center;'><label>NACIONALES</label></td>";
                    sTablaNacional += "  </tr>";

                    for (int x = 0; x < dtConceptos.Rows.Count; x++)
                    {
                        if (dtConceptos.Rows[x]["DesConcepto"].S().Contains("Desayuno"))
                        {
                            sTablaNacional += "  <tr>";
                            sTablaNacional += "  <td><label>" + dtConceptos.Rows[x]["DesConcepto"].S().ToUpper() + "</label></td>";
                            sTablaNacional += "  <td align='center'><label>" + dtCalculos.Rows[0][4].S() + "</label></td>";
                            sTablaNacional += "  </tr>";
                        }
                        else if (dtConceptos.Rows[x]["DesConcepto"].S().Contains("Comida"))
                        {
                            sTablaNacional += "  <tr>";
                            sTablaNacional += "  <td><label>" + dtConceptos.Rows[x]["DesConcepto"].S().ToUpper() + "</label></td>";
                            sTablaNacional += "  <td align='center'><label>" + dtCalculos.Rows[0][6].S() + "</label></td>";
                            sTablaNacional += "  </tr>";
                        }
                        else if (dtConceptos.Rows[x]["DesConcepto"].S().Contains("Cena"))
                        {
                            sTablaNacional += "  <tr>";
                            sTablaNacional += "  <td><label>" + dtConceptos.Rows[x]["DesConcepto"].S().ToUpper() + "</label></td>";
                            sTablaNacional += "  <td align='center'><label>" + dtCalculos.Rows[0][8].S() + "</label></td>";
                            sTablaNacional += "  </tr>";
                        }
                    }
                    sTablaNacional += "  <tr>";
                    sTablaNacional += "     <td><span><b>TOTAL:</b></span></td>";
                    sTablaNacional += "     <td align='right'><span>" + dtCalculos.Rows[0][10].S() + "</span></td>";
                    sTablaNacional += "  </tr>";
                    sTablaNacional += "  </table>";

                    //USD columna 5,7,9
                    sTablaExtranjera = "<table border='1' width='40%' style='border-radius:4px; border: 1px solid #ccc;'>";
                    sTablaExtranjera += "  <tr>";
                    sTablaExtranjera += "      <td colspan='2' style='background-color:#ccc; text-align:center;'><label>EXTRANJEROS</label></td>";
                    sTablaExtranjera += "  </tr>";

                    for (int x = 0; x < dtConceptos.Rows.Count; x++)
                    {
                        if (dtConceptos.Rows[x]["DesConcepto"].S().Contains("Desayuno"))
                        {
                            sTablaExtranjera += "  <tr>";
                            sTablaExtranjera += "  <td><label>" + dtConceptos.Rows[x]["DesConcepto"].S().ToUpper() + "</label></td>";
                            sTablaExtranjera += "  <td align='center'><label>" + dtCalculos.Rows[0][5].S() + "</label></td>";
                            sTablaExtranjera += "  </tr>";
                        }
                        else if (dtConceptos.Rows[x]["DesConcepto"].S().Contains("Comida"))
                        {
                            sTablaExtranjera += "  <tr>";
                            sTablaExtranjera += "  <td><label>" + dtConceptos.Rows[x]["DesConcepto"].S().ToUpper() + "</label></td>";
                            sTablaExtranjera += "  <td align='center'><label>" + dtCalculos.Rows[0][7].S() + "</label></td>";
                            sTablaExtranjera += "  </tr>";
                        }
                        else if (dtConceptos.Rows[x]["DesConcepto"].S().Contains("Cena"))
                        {
                            sTablaExtranjera += "  <tr>";
                            sTablaExtranjera += "  <td><label>" + dtConceptos.Rows[x]["DesConcepto"].S().ToUpper() + "</label></td>";
                            sTablaExtranjera += "  <td align='center'><label>" + dtCalculos.Rows[0][9].S() + "</label></td>";
                            sTablaExtranjera += "  </tr>";
                        }
                    }
                    sTablaExtranjera += "  <tr>";
                    sTablaExtranjera += "     <td><span><b>TOTAL:</b></span></td>";
                    sTablaExtranjera += "     <td align='right'><span>" + dtCalculos.Rows[0][11].S() + "</span></td>";
                    sTablaExtranjera += "  </tr>";
                    sTablaExtranjera += "  </table>";

                    sHtml += "<div class='row'>";
                    sHtml += "  <div class='col-md-6' align='center'>";
                    sHtml += sTablaNacional;
                    sHtml += "  </div>";
                    sHtml += "  <div class='col-md-6' align='center'>";
                    sHtml += sTablaExtranjera;
                    sHtml += "  </div>";
                    sHtml += "</div>";
                    divViaticos.InnerHtml = sHtml;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        public void LlenaAdicionalesPeriodo(DataTable dt)
        {
            try
            {
                dtAjustes = null;
                dtAjustes = dt;
                gvAjustes.DataSource = dtAjustes;
                gvAjustes.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LlenaAjustesPorPiloto(DataTable dt)
        {
            try
            {
                dtAjustesPiloto = null;
                dtAjustesPiloto = dt;
                gvAjustesPiloto.DataSource = dtAjustesPiloto;
                gvAjustesPiloto.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LlenaReporte(DataSet ds)
        {
            try
            {
                if (ds != null && ds.Tables.Count > 0)
                {
                    string sCvePiloto = string.Empty;
                    string sHtml = ImprimirReporte(ds);
                    sCvePiloto = ds.Tables[0].Rows[0]["CvePiloto"].S();
                    var htmlToPdf = new NReco.PdfGenerator.HtmlToPdfConverter();
                    htmlToPdf.Orientation = NReco.PdfGenerator.PageOrientation.Landscape;
                    htmlToPdf.Margins.Left = 1;
                    htmlToPdf.Margins.Right = 1;
                    htmlToPdf.Size = NReco.PdfGenerator.PageSize.A4;

                    var pdfBytes = htmlToPdf.GeneratePdf(sHtml);
                    //var base64EncodedPdf = System.Convert.ToBase64String(pdfBytes);

                    Response.Clear();
                    Response.ClearHeaders();
                    Response.Buffer = true;
                    Response.Charset = "UTF-8";
                    string filename = "Reporte_" + sCvePiloto + ".pdf";
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("Content-Disposition", "attachment;filename=\"" + filename + "\"");
                    Response.BinaryWrite(pdfBytes);
                    Response.Flush();
                    //Response.End();
                    Response.SuppressContent = true;
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LlenaReporteGral(DataSet ds)
        {
            try
            {
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        //btnExportar.Enabled = true;
                        string sContent = CrearReporteGral(ds);
                        if (!string.IsNullOrEmpty(sContent))
                        {
                            divReporte.InnerHtml = sContent;
                        }
                    }
                    //else
                    //    btnExportar.Enabled = false;
                }
                //else
                //    btnExportar.Enabled = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string CrearReporteGral(DataSet dsGral)
        {
            decimal dTotalMXN = 0;
            decimal dTotalUSD = 0;
            string sFechaActual = string.Empty;
            DateTime dtActual = DateTime.Now;
            DataTable dtAli = new DataTable();
            DataTable dtAjustes = new DataTable();
            dtAli = dsGral.Tables[0];
            dtAjustes = dsGral.Tables[1];

            string sPeriodo = string.Empty;
            string sDel = string.Empty;
            string sAl = string.Empty;
            string style = @"<style> " + System.IO.File.ReadAllText(Server.MapPath("~/Styles/bootstrap4.min.css")) + "</style>";

            try
            {
                //string css = System.IO.File.ReadAllText(Server.MapPath("~/Styles/bootstrap4.min.css"));
                byte[] imageArray = System.IO.File.ReadAllBytes(Server.MapPath(@"~/img/logo-ale.jpg"));
                string base64ImageRepresentation = Convert.ToBase64String(imageArray);

                sFechaActual = dtActual.Day.S() + " de " + GetMes(dtActual.Month) + " de " + dtActual.Year.S() + " a las " + dtActual.Hour.S() + ":" + dtActual.Minute.S() + " hrs.";
                sDel = "Del " + dsGral.Tables[2].Rows[0]["FechaInicio"].S().Dt().Day.S() + " " + GetMes(dsGral.Tables[2].Rows[0]["FechaInicio"].S().Dt().Month) + " " + dsGral.Tables[2].Rows[0]["FechaInicio"].S().Dt().Year.S();
                sAl = "al " + dsGral.Tables[2].Rows[0]["FechaFinal"].S().Dt().Day.S() + " " + GetMes(dsGral.Tables[2].Rows[0]["FechaFinal"].S().Dt().Month) + " " + dsGral.Tables[2].Rows[0]["FechaFinal"].S().Dt().Year.S();
                sPeriodo = sDel + " " + sAl;

                string sHtml = string.Empty;
                sHtml += "<!doctype html>";
                sHtml += "<html>";
                sHtml += "<head>";
                sHtml += "  <title></title>";
                sHtml += "  <meta charset='utf-8' />";
                sHtml += "  <meta name='viewport' content='width=device-width, initial-scale=1, shrink-to-fit=no'>";
                sHtml += "  <style>";
                //sHtml += css;
                sHtml += "      .row {";
                sHtml += "          margin-right: 0px;";
                sHtml += "          margin-left: 0px; }";

                sHtml += ".table td, .table th {";
                sHtml += "    padding: .75rem;";
                sHtml += "    vertical-align: top;";
                sHtml += "    border-top: 0px solid #ffffff !important;";
                sHtml += "}";

                sHtml += "      .header {";
                sHtml += "          color: #2065D5;";
                sHtml += "          font-size: 12px; }";

                sHtml += "  </style>";
                //sHtml += style;

                sHtml += "</head>";
                sHtml += "<body>";

                sHtml += "  <div>";

                #region ENCABEZADO;

                sHtml += "      <div class='row'>";
                sHtml += "          <div class='col-md-4' style='width:300px; top:50px;'>";
                //sHtml += "              <img src='" + Server.MapPath(@"~/img/logo-ale.jpg") + "' alt='Logo Ale' width='300px' height='100px' />";
                sHtml += "              <img src='http://172.16.23.30/MexJet360_FL3XX/img/logo-ale.jpg' alt='Logo Ale' width='300px' height='100px' />";

                sHtml += "          </div>";
                sHtml += "          <div class='col-md-8'>";
                sHtml += "              <div style='text-align:center; width:800px; margin-top:-400px;'>";
                sHtml += "                  <h1>Total de Viáticos por Periodo</h1><br />";
                sHtml += "              </div>";
                sHtml += "          </div>";
                sHtml += "      </div>";
                sHtml += "      <div class='row'>";
                sHtml += "          <div class='col-md-12' style='text-align:center; '>";
                sHtml += "              <label Style='font-weight:bold;'>Periodo</label><br />";
                sHtml += "              <label>" + sPeriodo + "</label>";
                sHtml += "          </div>";
                sHtml += "      </div>";

                sHtml += "      <div class='row' style='margin-top:-50px;'>";
                sHtml += "          <div class='col-md-8' style='width:1000px;'>";
                //sHtml += "              &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                sHtml += "          </div>";
                sHtml += "          <div class='col-md-4' style='text-align:right;width:400px;'>";
                sHtml += "              <label>Fecha: " + sFechaActual + "</label><br />";
                sHtml += "              <label Style='font-weight:bold; '>Usuario: </label>";
                sHtml += "              <label>" + ((UserIdentity)Session["UserIdentity"]).sName + "</label>";
                sHtml += "          </div>";
                sHtml += "      </div>";


                #endregion

                #region VUELOS

                sHtml += "      <div class='row'>";
                sHtml += "          <div class='col-md-12'>";
                sHtml += "              <table width='100%' border='0' class='table'>";
                sHtml += "                  <tr>";
                sHtml += "                    <td colspan='17' style='font-size:12pt;font-weight:600;'>Vuelos</td>";
                sHtml += "                  </tr>";
                sHtml += "                  <tr>";
                sHtml += "                    <td class='header'><label>Clave Piloto</label></td>";
                sHtml += "                    <td class='header'><label>Piloto</label></td>";
                sHtml += "                    <td class='header'><label>Desayuno Nac.</label></td>";
                sHtml += "                    <td class='header'><label>Importe</label></td>";
                sHtml += "                    <td class='header'><label>Desayuno Int.</label></td>";
                sHtml += "                    <td class='header'><label>Importe</label></td>";
                sHtml += "                    <td class='header'><label>Comida Nac.</label></td>";
                sHtml += "                    <td class='header'><label>Importe</label></td>";
                sHtml += "                    <td class='header'><label>Comida Int.</label></td>";
                sHtml += "                    <td class='header'><label>Importe</label></td>";
                sHtml += "                    <td class='header'><label>Cena Nac.</label></td>";
                sHtml += "                    <td class='header'><label>Importe</label></td>";
                sHtml += "                    <td class='header'><label>Cena Int.</label></td>";
                sHtml += "                    <td class='header'><label>Importe</label></td>";
                sHtml += "                    <td class='header'><label>Estatus</label></td>";
                sHtml += "                    <td class='header'><label>Total en MXN</label></td>";
                sHtml += "                    <td class='header'><label>Total en USD</label></td>";

                sHtml += "                  </tr>";

                for (int i = 0; i < dtAli.Rows.Count; i++)
                {
                    sHtml += "<tr>";
                    sHtml += "  <td>" + dtAli.Rows[i]["CvePiloto"].S() + "</td>";
                    sHtml += "  <td>" + dtAli.Rows[i]["Piloto"].S() + "</td>";

                    sHtml += "  <td>" + dtAli.Rows[i]["DesNac"].S() + "</td>";
                    sHtml += "  <td>" + dtAli.Rows[i]["ImpDesNac"].S().D().ToString("C") + "</td>";
                    sHtml += "  <td>" + dtAli.Rows[i]["DesInt"].S() + "</td>";
                    sHtml += "  <td>" + dtAli.Rows[i]["ImpDesInt"].S().D().ToString("C2") + "</td>";
                    sHtml += "  <td>" + dtAli.Rows[i]["ComNac"].S() + "</td>";
                    sHtml += "  <td>" + dtAli.Rows[i]["ImpComNac"].S().D().ToString("C") + "</td>";
                    sHtml += "  <td>" + dtAli.Rows[i]["ComInt"].S() + "</td>";
                    sHtml += "  <td>" + dtAli.Rows[i]["ImpComInt"].S().D().ToString("C2") + "</td>";
                    sHtml += "  <td>" + dtAli.Rows[i]["CenNac"].S() + "</td>";
                    sHtml += "  <td>" + dtAli.Rows[i]["ImpCenNac"].S().D().ToString("C") + "</td>";
                    sHtml += "  <td>" + dtAli.Rows[i]["CenInt"].S() + "</td>";
                    sHtml += "  <td>" + dtAli.Rows[i]["ImpCenInt"].S().D().ToString("C2") + "</td>";

                    sHtml += "  <td>" + dtAli.Rows[i]["Estatus"].S() + "</td>";
                    sHtml += "  <td>" + dtAli.Rows[i]["TotalMXN"].S().D().ToString("C") + "</td>";
                    sHtml += "  <td>" + dtAli.Rows[i]["TotalUSD"].S().D().ToString("C2") + "</td>";

                    sHtml += "</tr>";

                    dTotalMXN += dtAli.Rows[i]["TotalMXN"].S().D();
                    dTotalUSD += dtAli.Rows[i]["TotalUSD"].S().D();
                }

                sHtml += "                  <tr>";
                sHtml += "                    <td class='header' colspan='15' align='right'><label>Total</label></td>";
                sHtml += "                    <td style='font-weight:600;'><label>" + dTotalMXN.ToString("C") + "</label></td>";
                sHtml += "                    <td style='font-weight:600;'><label>" + dTotalUSD.ToString("C2") + "</label></td>";

                sHtml += "                  </tr>";

                sHtml += "              </table>";

                sHtml += "          </div>";
                sHtml += "      </div>";
                #endregion

                #region AJUSTES
                sHtml += "      <div class='row'>";
                sHtml += "          <div class='col-md-12'>";
                sHtml += "              <table width='100%' border='0' class='table'>";
                sHtml += "                  <tr>";
                sHtml += "                    <td colspan='17' style='font-size:12pt;font-weight:600;'>Especiales</td>";
                sHtml += "                  </tr>";
                sHtml += "                  <tr>";
                sHtml += "                    <td class='header'><label>Clave Piloto</label></td>";
                sHtml += "                    <td class='header'><label>Piloto</label></td>";
                sHtml += "                    <td class='header' colspan='2'><label>Concepto Adicional</label></td>";
                sHtml += "                    <td class='header' colspan='2'><label>Descripción</label></td>";
                sHtml += "                    <td class='header'><label>MXN</label></td>";
                sHtml += "                    <td class='header'><label>USD</label></td>";

                sHtml += "                  </tr>";

                decimal dTotalAjustesMXN = 0;
                decimal dTotalAjustesUSD = 0;

                for (int x = 0; x < dtAjustes.Rows.Count; x++)
                {
                    sHtml += "              <tr>";
                    sHtml += "                <td>" + dtAjustes.Rows[x]["CvePiloto"].S() + "</td>";
                    sHtml += "                <td>" + dtAjustes.Rows[x]["Piloto"].S() + "</td>";
                    sHtml += "                <td colspan='2'>" + dtAjustes.Rows[x]["ConceptoAdicional"].S() + "</td>";
                    sHtml += "                <td colspan='2'>" + dtAjustes.Rows[x]["Descripcion"].S() + "</td>";
                    sHtml += "                <td>" + dtAjustes.Rows[x]["MXN"].S().D().ToString("C") + "</td>";
                    sHtml += "                <td>" + dtAjustes.Rows[x]["USD"].S().D().ToString("C2") + "</td>";
                    sHtml += "              </tr>";
                    dTotalAjustesMXN += dtAjustes.Rows[x]["MXN"].S().D();
                    dTotalAjustesUSD += dtAjustes.Rows[x]["USD"].S().D();
                }
                sHtml += "                  <tr>";
                sHtml += "                    <td class='header' colspan='6' align='right'><label>Total</label></td>";
                sHtml += "                    <td style='font-weight:600;'><label>" + dTotalAjustesMXN.ToString("C") + "</label></td>";
                sHtml += "                    <td style='font-weight:600;'><label>" + dTotalAjustesUSD.ToString("C2") + "</label></td>";

                sHtml += "                  </tr>";

                sHtml += "              </table>";
                sHtml += "          </div>";
                sHtml += "      </div>";
                #endregion

                #region Total General
                decimal dTotalVuelosMXN = 0;
                decimal dTotalVuelosUSD = 0;

                dTotalVuelosMXN = dTotalMXN;
                dTotalVuelosUSD = dTotalUSD;

                decimal dTotalEspecialesMXN = 0;
                decimal dTotalEspecialesUSD = 0;

                dTotalEspecialesMXN = dTotalAjustesMXN;
                dTotalEspecialesUSD = dTotalAjustesUSD;

                decimal dTotalGralMXN = 0;
                decimal dTotalGralUSD = 0;

                dTotalGralMXN = dTotalVuelosMXN + dTotalEspecialesMXN;
                dTotalGralUSD = dTotalVuelosUSD + dTotalEspecialesUSD;

                sHtml += "      <div class='row'>";
                sHtml += "          <div class='col-md-12'>";
                sHtml += "              <table width='100%' border='0' class='table'>";
                //sHtml += "                  <tr>";
                //sHtml += "                    <td colspan='17' style='font-size:12pt;font-weight:600;'>Total General</td>";
                //sHtml += "                  </tr>";

                sHtml += "                  <tr>";
                sHtml += "                    <td colspan='2' style='font-size:12pt;font-weight:600;'>Total General</td>";
                sHtml += "                    <td class='header'><label>MXN</label></td>";
                sHtml += "                    <td class='header'><label>USD</label></td>";
                sHtml += "                  </tr>";

                sHtml += "                  <tr>";
                sHtml += "                    <td colspan='2'>Total Vuelos</td>";
                sHtml += "                    <td><label>" + dTotalVuelosMXN.ToString("C") + "</label></td>";
                sHtml += "                    <td><label>" + dTotalVuelosUSD.ToString("C2") + "</label></td>";
                sHtml += "                  </tr>";

                sHtml += "                  <tr>";
                sHtml += "                    <td colspan='2'>Total Especiales</td>";
                sHtml += "                    <td><label>" + dTotalEspecialesMXN.ToString("C") + "</label></td>";
                sHtml += "                    <td><label>" + dTotalEspecialesUSD.ToString("C2") + "</label></td>";
                sHtml += "                  </tr>";

                sHtml += "                  <tr>";
                sHtml += "                    <td colspan='2'> </td>";
                sHtml += "                    <td style='font-weight:600;'><label>" + dTotalGralMXN.ToString("C") + "</label></td>";
                sHtml += "                    <td style='font-weight:600;'><label>" + dTotalGralUSD.ToString("C2") + "</label></td>";
                sHtml += "                  </tr>";

                sHtml += "              </table>";
                sHtml += "          </div>";
                sHtml += "      </div>";

                #endregion


                sHtml += "  </div>";

                sHtml += "</body>";
                sHtml += "</html>";
                return sHtml;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally {
                dTotalMXN = 0;
                dTotalUSD = 0;
                sFechaActual = string.Empty;
                dtAli.Dispose();
                dtAjustes.Dispose();

                sPeriodo = string.Empty;
                sDel = string.Empty;
                sAl = string.Empty;
                style = string.Empty;
            }
        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("content-disposition", "attachment;filename=ReporteGeneral.xls");
            Response.Charset = Encoding.UTF8.WebName; //"UTF-8";
            Response.ContentEncoding = Encoding.UTF8;
            this.EnableViewState = false;

            StringWriter stringWrite = new StringWriter();
            HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            pnlReporte.RenderControl(htmlWrite);

            //string style = @"<style> " + System.IO.File.ReadAllText(Server.MapPath("~/Styles/bootstrap4.min.css")) + "</style>";
            //Response.Write(style);

            Response.Write(stringWrite.ToString());
            Response.Flush();
            Response.Close();

            stringWrite.Flush();
            htmlWrite.Flush();
            stringWrite.Close();
            htmlWrite.Close();
            stringWrite.Dispose();
            htmlWrite.Dispose();

            //Response.End();
            Response.SuppressContent = true;
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        public string ImprimirReporte(DataSet ds)
        {
            try
            {
                string sFechaActual = string.Empty;

                DateTime dtActual = DateTime.Now;
                sFechaActual = dtActual.Day.S() + " de " + GetMes(dtActual.Month) + " de " + dtActual.Year.S() + " a las " + dtActual.Hour.S() + ":" + dtActual.Minute.S() + " hrs.";

                DataTable dtHeader = new DataTable();
                string sPeriodo = string.Empty;
                string sDel = string.Empty;
                string sAl = string.Empty;
                string css = System.IO.File.ReadAllText(Server.MapPath("~/Styles/bootstrap4.min.css"));

                byte[] imageArray = System.IO.File.ReadAllBytes(Server.MapPath(@"~/img/logo-ale.jpg"));
                string base64ImageRepresentation = Convert.ToBase64String(imageArray);

                dtHeader = ds.Tables[0];

                sDel = "Del " + ds.Tables[0].Rows[0]["FechaInicio"].S().Dt().Day.S() + " " + GetMes(ds.Tables[0].Rows[0]["FechaInicio"].S().Dt().Month) + " " + ds.Tables[0].Rows[0]["FechaInicio"].S().Dt().Year.S();
                sAl = "al " + ds.Tables[0].Rows[0]["FechaFinal"].S().Dt().Day.S() + " " + GetMes(ds.Tables[0].Rows[0]["FechaFinal"].S().Dt().Month) + " " + ds.Tables[0].Rows[0]["FechaFinal"].S().Dt().Year.S();
                sPeriodo = sDel + " " + sAl;

                string sHtml = string.Empty;
                //sHtml += "<html xmlns='http://www.w3.org/1999/xhtml'>";
                sHtml += "<!doctype html>";
                sHtml += "<html>";
                sHtml += "<head>";
                sHtml += "  <title></title>";
                sHtml += "  <meta charset='utf-8' />";
                sHtml += "  <meta name='viewport' content='width=device-width, initial-scale=1, shrink-to-fit=no'>";
                sHtml += "  <style>";
                sHtml += css;
                sHtml += "      .row {";
                sHtml += "          margin-right: 0px;";
                sHtml += "          margin-left: 0px; }";

                sHtml += ".table td, .table th {";
                sHtml += "    padding: .75rem;";
                sHtml += "    vertical-align: top;";
                sHtml += "    border-top: 0px solid #ffffff !important;";
                sHtml += "}";


                sHtml += "  </style>";
                sHtml += "</head>";
                sHtml += "<body>";
                sHtml += "  <div style='margin-top:0px;'>";

                #region ENCABEZADO

                sHtml += "      <div class='row'>";
                sHtml += "          <div class='col-md-1'>";
                sHtml += "              &nbsp;";
                sHtml += "          </div>";
                sHtml += "          <div class='col-md-10'>";
                sHtml += "              <div style='text-align:center; '>";
                sHtml += "                  <br />";
                sHtml += "                  <h4>Aerolíneas Ejecutivas</h4>";
                sHtml += "                  <img src='data:image/png;base64," + base64ImageRepresentation + "' alt='Fancy Image' />";
                sHtml += "                  <h1>Reporte de Viáticos</h1><br />";
                sHtml += "                  <h5><label>" + dtHeader.Rows[0]["Piloto"].S() + "</label></h5>";
                sHtml += "              </div><br />";
                sHtml += "          <div class='row'>";
                sHtml += "              <div class='col-md-6' style='text-align:center; '>";
                sHtml += "                  <label Style='font-weight:bold;'>Periodo del Pago</label><br />";
                sHtml += "                  <label>" + sPeriodo + "</label>";
                sHtml += "              </div>";
                sHtml += "              <div class='col-md-6' style='text-align:center;'>";
                sHtml += "                  <label Style='font-weight:bold; '>Ejecutivo: </label>";
                sHtml += "                  <label>" + ((UserIdentity)Session["UserIdentity"]).sName + "</label><br />";
                sHtml += "                  <label>" + sFechaActual + "</label>";
                sHtml += "              </div>";
                sHtml += "          </div>";
                #endregion

                DataRow dr;
                DataTable dtMov = new DataTable();
                DataTable dtMovDis = new DataTable();
                dtMov = ds.Tables[1];

                DataTable dtFechaMov = new DataTable();
                dtFechaMov.Columns.Add("FechaMov");

                for (int i = 0; i < dtMov.Rows.Count; i++)
                {
                    dr = dtFechaMov.NewRow();
                    dr["FechaMov"] = dtMov.Rows[i]["locdep"].S().Dt().ToString("dd/MM/yyyy");
                    dtFechaMov.Rows.Add(dr);
                }
                dtFechaMov.AcceptChanges();
                DataView dv = new DataView(dtFechaMov);
                dtMovDis = dv.ToTable(true, "FechaMov"); //Datatable de dias o fechas

                //MOVIMIENTOS
                #region MOVIMIENTOS

                DataTable dtCloned = new DataTable();
                dtCloned = dtMov.Clone();
                dtCloned.Columns["locdep"].DataType = typeof(String);
                foreach (DataRow row in dtMov.Rows)
                {
                    dtCloned.ImportRow(row);
                }

                //FORMATEAR FECHAS DE MOVIMIENTOS
                for (int x = 0; x < dtCloned.Rows.Count; x++)
                {
                    dtCloned.Rows[x]["locdep"] = dtCloned.Rows[x]["locdep"].S().Dt().ToString("dd/MM/yyyy");
                }
                dtCloned.AcceptChanges();

                #region DATOS ALIMENTOS

                if (ds.Tables[3] != null && ds.Tables[3].Rows.Count > 0)
                {
                    DataTable dtAlimentos = new DataTable();
                    dtAlimentos = ds.Tables[3].Clone();
                    dtAlimentos.Columns["Fecha"].DataType = typeof(String);
                    foreach (DataRow row in ds.Tables[3].Rows)
                    {
                        dtAlimentos.ImportRow(row);
                    }
                    //FORMATEAR FECHAS DE ALIMENTOS
                    for (int x = 0; x < dtAlimentos.Rows.Count; x++)
                    {
                        dtAlimentos.Rows[x]["Fecha"] = dtAlimentos.Rows[x]["Fecha"].S().Dt().ToString("dd/MM/yyyy");
                    }
                    dtAlimentos.AcceptChanges();

                    #endregion



                    sHtml += "<table width='100%' border='0' class='table' style='margin-top:-20px;'>";

                    sHtml += "  <tr>";
                    sHtml += "      <td colspan='2'>";
                    sHtml += "          <hr />";
                    sHtml += "          <h4>MOVIMIENTOS Y VIÁTICOS POR DÍA</h4><br />";
                    sHtml += "      </td>";
                    sHtml += "  </tr>";

                    for (int i = 0; i < dtMovDis.Rows.Count; i++)
                    {
                        sHtml += "  <tr>";
                        sHtml += "      <td>";
                        //-------
                        sHtml += "          <table class='table table-bordered' style='margin: -5px auto; width: 100%; color:#ffffff;'>";
                        sHtml += "            <tr>";
                        sHtml += "                <td colspan='4' style='background-color:#315497;text-align:center;'>";
                        sHtml += "                    <label>MOVIMIENTOS</label>";
                        sHtml += "                </td>";
                        sHtml += "            </tr>";
                        sHtml += "            <tr>";
                        sHtml += "                <td style='background-color:#64beed;text-align:center;'>";
                        sHtml += "                    <label>FECHA</label>";
                        sHtml += "                </td>";
                        sHtml += "                <td style='background-color:#64beed;text-align:center;'>";
                        sHtml += "                    <label>TIPO</label>";
                        sHtml += "                </td>";
                        sHtml += "                <td style='background-color:#64beed;text-align:center;'>";
                        sHtml += "                    <label>Ori - Des</label>";
                        sHtml += "                </td>";
                        sHtml += "                <td style='background-color:#64beed;text-align:center;'>";
                        sHtml += "                    <label>TIPO</label>";
                        sHtml += "                </td>";
                        sHtml += "            </tr>";

                        //Content
                        DataRow[] dRow = dtCloned.Select("locdep='" + dtMovDis.Rows[i]["FechaMov"].S() + "'");

                        for (int x = 0; x < dRow.Length; x++)
                        {
                            sHtml += "<tr style='color:#000000;'>";
                            sHtml += "    <td style='background-color:#ffffff;text-align:center;'>";
                            sHtml += dRow[x]["locdep"].S();
                            sHtml += "    </td>";
                            sHtml += "    <td style='background-color:#ffffff;text-align:center;'>";
                            sHtml += "        F";
                            sHtml += "    </td>";
                            sHtml += "    <td style='background-color:#ffffff;text-align:center;'>";
                            //sHtml += "        MMTO MTYY<br />07:00 09:00";
                            sHtml += dRow[x]["depicao_id"].S() + " " + dRow[x]["arricao_id"].S() + "<br />" + dRow[x]["time_locdep"].S() + " " + dRow[x]["time_locarr"].S();
                            sHtml += "    </td>";
                            sHtml += "    <td style='background-color:#ffffff;text-align:center;'>";
                            sHtml += dRow[x]["Tipo"].S();
                            sHtml += "    </td>";
                            sHtml += "</tr>";
                        }
                        sHtml += "      </table>";

                        //-------
                        sHtml += "   </td>";
                        sHtml += "   <td>";

                        //ALIMENTOS
                        if (ds.Tables[3] != null && ds.Tables[3].Rows.Count > 0)
                        {
                            sHtml += "      <table class='table table-bordered' style='margin: -5px auto; width:100%; color:#ffffff;'>";
                            sHtml += "          <tr>";
                            sHtml += "              <td style='background-color:#64beed;text-align:center;background-color:#315497;width:20%;'>";
                            sHtml += "                  <br /><label>MONEDA</label>";
                            sHtml += "              </td>";
                            sHtml += "              <td style='background-color:#64beed;text-align:center;background-color:#a9d08f;width:20%;'>";
                            sHtml += "                  <label>DESAYUNO</label><br /><label>07:00 a 08:00</label>";
                            sHtml += "              </td>";
                            sHtml += "              <td style='background-color:#64beed;text-align:center;background-color:#c65811;width:20%;'>";
                            sHtml += "                  <label>COMIDA</label><br /><label>14:00 a 15:00</label>";
                            sHtml += "              </td>";
                            sHtml += "              <td style='background-color:#64beed;text-align:center;background-color:#6600cd;width:20%;'>";
                            sHtml += "                  <label>CENA</label><br /></label>20:00 a 21:00</label>";
                            sHtml += "              </td>";
                            sHtml += "              <td style='background-color:#64beed;text-align:center;background-color:#335398;width:20%;'>";
                            sHtml += "                  <br /><label>TOTAL</label>";
                            sHtml += "              </td>";
                            sHtml += "          </tr>";

                            //Content
                            DataRow[] dRow2 = dtAlimentos.Select("Fecha='" + dtMovDis.Rows[i]["FechaMov"].S() + "'");

                            decimal dTotalDesayunoMXN = 0;
                            decimal dTotalComidaMXN = 0;
                            decimal dTotalCenaMXN = 0;
                            decimal dTotalMXN = 0;

                            decimal dTotalDesayunoUSD = 0;
                            decimal dTotalComidaUSD = 0;
                            decimal dTotalCenaUSD = 0;
                            decimal dTotalUSD = 0;

                            //PESOS MXN
                            for (int y = 0; y < dRow2.Length; y++)
                            {
                                if (dRow2[y]["Moneda"].S() == "MXN")
                                {
                                    dTotalDesayunoMXN += dRow2[y]["Desayuno"].S().D();
                                    dTotalComidaMXN += dRow2[y]["Comida"].S().D();
                                    dTotalCenaMXN += dRow2[y]["Cena"].S().D();
                                }
                            }
                            dTotalMXN = ((dTotalDesayunoMXN + dTotalComidaMXN) + dTotalCenaMXN);

                            for (int z = 0; z < dRow2.Length; z++)
                            {
                                if (dRow2[z]["Moneda"].S() == "USD")
                                {
                                    dTotalDesayunoUSD += dRow2[z]["Desayuno"].S().D();
                                    dTotalComidaUSD += dRow2[z]["Comida"].S().D();
                                    dTotalCenaUSD += dRow2[z]["Cena"].S().D();
                                }
                            }
                            dTotalUSD = ((dTotalDesayunoUSD + dTotalComidaUSD) + dTotalCenaUSD);

                            //MXN
                            sHtml += "<tr style='color:#000000;'>";
                            sHtml += "  <td style='background-color:#ffffff;text-align:center;'>";
                            sHtml += "      MXN";
                            sHtml += "  </td>";
                            sHtml += "  <td style='background-color:#ffffff;text-align:right;'>";
                            sHtml += dTotalDesayunoMXN.ToString("c");
                            sHtml += "  </td>";
                            sHtml += "  <td style='background-color:#ffffff;text-align:right;'>";
                            sHtml += dTotalComidaMXN.ToString("c");
                            sHtml += "  </td>";
                            sHtml += "  <td style='background-color:#ffffff;text-align:right;'>";
                            sHtml += dTotalCenaMXN.ToString("c");
                            sHtml += "  </td>";
                            sHtml += "  <td style='background-color:#ffffff;text-align:right;'>";
                            sHtml += dTotalMXN.ToString("c");
                            sHtml += "  </td>";
                            sHtml += "</tr>";

                            //USD
                            sHtml += "<tr style='color:#000000;'>";
                            sHtml += "  <td style='background-color:#ffffff;text-align:center;'>";
                            sHtml += "      USD";
                            sHtml += "  </td>";
                            sHtml += "  <td style='background-color:#ffffff;text-align:right;'>";
                            sHtml += dTotalDesayunoUSD.ToString("c");
                            sHtml += "  </td>";
                            sHtml += "  <td style='background-color:#ffffff;text-align:right;'>";
                            sHtml += dTotalComidaUSD.ToString("c");
                            sHtml += "  </td>";
                            sHtml += "  <td style='background-color:#ffffff;text-align:right;'>";
                            sHtml += dTotalCenaUSD.ToString("c");
                            sHtml += "  </td>";
                            sHtml += "  <td style='background-color:#ffffff;text-align:right;'>";
                            sHtml += dTotalUSD.ToString("c");
                            sHtml += "  </td>";
                            sHtml += "</tr>";

                            //ENVIO TOTALES PARA CARGAR ULTIMA TABLA
                            CargarTotales(dtMovDis.Rows[i]["FechaMov"].S(), dTotalUSD, dTotalMXN);

                            //sHtml += ""; CONTENIDO DE TABLA DE ALIMENTOS
                            sHtml += " </table>";
                        }

                        sHtml += "      </td>";
                        sHtml += "  </tr>";
                    }

                    sHtml += "  <tr>";
                    sHtml += "      <td colspan='2'>";

                    sHtml += "          <hr />";

                    DataTable dtAjustes = new DataTable();
                    dtAjustes = ds.Tables[2];

                    if (dtAjustes != null && dtAjustes.Rows.Count > 0)
                    {
                        sHtml += "<h4>AJUSTES DEL PERIODO</h4><br />";
                        sHtml += "<div class='row'>";
                        sHtml += "  <div class='col-md-5 table'>&nbsp;</div>";
                        sHtml += "  <div class='col-md-7 table' align='right'>";
                        //TABLA DE AJUSTES
                        sHtml += "      <table class='table table-bordered table-hover' style='width:90%; color:#ffffff;'>";
                        sHtml += "        <tr>";
                        sHtml += "            <td style='text-align:center; background-color:#315497;width:60%;'>";
                        sHtml += "                <label>CONCEPTOS ADICIONALES</label>";
                        sHtml += "            </td>";
                        sHtml += "            <td style='text-align:center; background-color:#315497;width:20%;'>";
                        sHtml += "                <label>MONEDA</label>";
                        sHtml += "            </td>";
                        sHtml += "            <td style='text-align:center; background-color:#315497;width:20%;'>";
                        sHtml += "                <label>TOTAL</label>";
                        sHtml += "            </td>";
                        sHtml += "        </tr>";

                        decimal dAjusteMXN = 0;
                        decimal dAjusteUSD = 0;

                        for (int i = 0; i < dtAjustes.Rows.Count; i++)
                        {
                            sHtml += "  <tr style='color:#000000;'>";
                            sHtml += "      <td style='background-color:#ffffff;text-align:center;'>";
                            sHtml += dtAjustes.Rows[i]["DesConcepto"].S();
                            sHtml += "      </td>";
                            sHtml += "      <td style='background-color:#ffffff;text-align:center;'>";
                            sHtml += dtAjustes.Rows[i]["Moneda"].S();
                            sHtml += "      </td>";
                            sHtml += "      <td style='background-color:#ffffff;text-align:right;'>";
                            sHtml += dtAjustes.Rows[i]["Valor"].S().D().ToString("c");
                            sHtml += "      </td>";
                            sHtml += "  </tr>";

                            if (dtAjustes.Rows[i]["Moneda"].S() == "MXN")
                                dAjusteMXN += dtAjustes.Rows[i]["Valor"].S().D();

                            if (dtAjustes.Rows[i]["Moneda"].S() == "USD")
                                dAjusteUSD += dtAjustes.Rows[i]["Valor"].S().D();
                        }

                        //if (dAjusteMXN != 0)
                        CargarTotales("- Ajuste -", dAjusteUSD, dAjusteMXN);

                        //if (dAjusteUSD != 0)
                        //    CargarTotales("- Ajuste -", dAjusteUSD, 0);

                        sHtml += "      </table>";
                        sHtml += "  </div>";
                        sHtml += "</div>";


                    }

                    sHtml += "      </td>";
                    sHtml += "  </tr>";
                    sHtml += "  <tr>";
                    sHtml += "      <td colspan='2'>";

                    //SUMATORIA -----------------------------------------------
                    sHtml += "          <hr />";
                    sHtml += "<h4>RESUMEN</h4><br />";

                    sHtml += "<div class='row'>";
                    sHtml += "  <div class='col-md-5 table'>&nbsp;</div>";
                    sHtml += "  <div class='col-md-7 table' align='right'>";

                    //TABLA DE TOTALES POR MONEDA Y VUELOS---------------------
                    sHtml += "      <table class='table table-bordered table-hover' style='width:90%; color:#ffffff;'>";
                    sHtml += "        <tr>";
                    sHtml += "            <td style='text-align:center; background-color:#315497;width:40%;'>";
                    sHtml += "                <label>FECHA VUELO</label>";
                    sHtml += "            </td>";
                    sHtml += "            <td style='text-align:center; background-color:#315497;width:30%;'>";
                    sHtml += "                <label>TOTAL USD</label>";
                    sHtml += "            </td>";
                    sHtml += "            <td style='text-align:center; background-color:#315497;width:30%;'>";
                    sHtml += "                <label>TOTAL MXN</label>";
                    sHtml += "            </td>";
                    sHtml += "        </tr>";

                    //DETALLE---
                    if (dtTotales != null && dtTotales.Rows.Count > 0)
                    {
                        decimal dTotalPesos = 0;
                        decimal dTotalDolares = 0;

                        for (int i = 0; i < dtTotales.Rows.Count; i++)
                        {
                            sHtml += "  <tr style='color:#000000;'>";
                            sHtml += "      <td style='background-color:#ffffff;text-align:center;'>";
                            sHtml += dtTotales.Rows[i]["FECHAVUELO"].S();
                            sHtml += "      </td>";
                            sHtml += "      <td style='background-color:#ffffff;text-align:right;'>";
                            sHtml += dtTotales.Rows[i]["TOTALUSD"].S();
                            sHtml += "      </td>";
                            sHtml += "      <td style='background-color:#ffffff;text-align:right;'>";
                            sHtml += dtTotales.Rows[i]["TOTALMXN"].S();
                            sHtml += "      </td>";
                            sHtml += "  </tr>";

                            dTotalPesos += dtTotales.Rows[i]["TOTALMXN"].S().Replace("$", "").D();
                            dTotalDolares += dtTotales.Rows[i]["TOTALUSD"].S().Replace("$", "").D();
                        }

                        sHtml += "  <tr style='color:#000000;'>";
                        sHtml += "      <td style='text-align:center; background-color:#315497;width:40%; color:#ffffff !important; font-weight: bold;'>";
                        sHtml += "TOTAL";
                        sHtml += "      </td>";
                        sHtml += "      <td style='background-color:#ffffff;text-align:right;font-weight: bold;'>";
                        sHtml += dTotalDolares.ToString("c");
                        sHtml += "      </td>";
                        sHtml += "      <td style='background-color:#ffffff;text-align:right;font-weight: bold;'>";
                        sHtml += dTotalPesos.ToString("c");
                        sHtml += "      </td>";
                        sHtml += "  </tr>";

                    }
                }
                sHtml += "      </table>";
                ////---------------------------------------------------------

                sHtml += "  </div>";
                sHtml += "</div>";


                sHtml += "      </td>";
                sHtml += "  </tr>";
                sHtml += "</table>";


                //for (int i = 0; i < dtMovDis.Rows.Count; i++)
                //{
                ////MOVIMIENTOS
                //sHtml += "  <table class='table table-bordered' style='margin: 25px auto; width: 100%; color:#ffffff;'>";
                //sHtml += "    <tr>";
                //sHtml += "        <td colspan='4' style='background-color:#315497;text-align:center;'>";
                //sHtml += "            <label>MOVIMIENTOS</label>";
                //sHtml += "        </td>";
                //sHtml += "    </tr>";
                //sHtml += "    <tr>";
                //sHtml += "        <td style='background-color:#64beed;text-align:center;'>";
                //sHtml += "            <label>FECHA</label>";
                //sHtml += "        </td>";
                //sHtml += "        <td style='background-color:#64beed;text-align:center;'>";
                //sHtml += "            <label>TIPO</label>";
                //sHtml += "        </td>";
                //sHtml += "        <td style='background-color:#64beed;text-align:center;'>";
                //sHtml += "            <label>Ori - Des</label>";
                //sHtml += "        </td>";
                //sHtml += "        <td style='background-color:#64beed;text-align:center;'>";
                //sHtml += "            <label>TIPO</label>";
                //sHtml += "        </td>";
                //sHtml += "    </tr>";

                ////Content
                //DataRow[] dRow = dtCloned.Select("locdep='" + dtMovDis.Rows[i]["FechaMov"].S() + "'");

                //for (int x = 0; x < dRow.Length; x++)
                //{
                //    sHtml += "<tr style='color:#000000;'>";
                //    sHtml += "    <td style='background-color:#ffffff;text-align:center;'>";
                //    sHtml += dRow[x]["locdep"].S();
                //    sHtml += "    </td>";
                //    sHtml += "    <td style='background-color:#ffffff;text-align:center;'>";
                //    sHtml += "        F";
                //    sHtml += "    </td>";
                //    sHtml += "    <td style='background-color:#ffffff;text-align:center;'>";
                //    //sHtml += "        MMTO MTYY<br />07:00 09:00";
                //    sHtml += dRow[x]["depicao_id"].S() + " " + dRow[x]["arricao_id"].S() + "<br />" + dRow[x]["time_locdep"].S() + " " + dRow[x]["time_locarr"].S();
                //    sHtml += "    </td>";
                //    sHtml += "    <td style='background-color:#ffffff;text-align:center;'>";
                //    sHtml += dRow[x]["Tipo"].S();
                //    sHtml += "    </td>";
                //    sHtml += "</tr>";
                //}


                //sHtml += "  </table>";
                //sHtml += "</div>";

                //}
                //sHtml += "  </table>";
                //sHtml += "</div>";

                #endregion

                //sHtml += "      </td>";
                //sHtml += "      <td width='70%'>";

                //ALIMENTOS
                //#region ALIMENTOS
                //sHtml += "  <div class='col-md-7 table'>";

                //if (ds.Tables[3] != null && ds.Tables[3].Rows.Count > 0)
                //{
                //    DataTable dtAlimentos = new DataTable();
                //    dtAlimentos = ds.Tables[3].Clone();
                //    dtAlimentos.Columns["Fecha"].DataType = typeof(String);
                //    foreach (DataRow row in ds.Tables[3].Rows)
                //    {
                //        dtAlimentos.ImportRow(row);
                //    }
                //    //FORMATEAR FECHAS DE ALIMENTOS
                //    for (int x = 0; x < dtAlimentos.Rows.Count; x++)
                //    {
                //        dtAlimentos.Rows[x]["Fecha"] = dtAlimentos.Rows[x]["Fecha"].S().Dt().ToString("dd/MM/yyyy");
                //    }
                //    dtAlimentos.AcceptChanges();

                //    for (int i = 0; i < dtMovDis.Rows.Count; i++)
                //    {
                //        //ALIMENTOS
                //        sHtml += "  <table class='table table-bordered' style='margin: 25px auto; width:100%; color:#ffffff;'>";
                //        sHtml += "      <tr>";
                //        sHtml += "          <td style='background-color:#64beed;text-align:center;background-color:#315497;width:20%;'>";
                //        sHtml += "              <br /><label>MONEDA</label>";
                //        sHtml += "          </td>";
                //        sHtml += "          <td style='background-color:#64beed;text-align:center;background-color:#a9d08f;width:20%;'>";
                //        sHtml += "              <label>DESAYUNO</label><br /><label>07:00 a 08:00</label>";
                //        sHtml += "          </td>";
                //        sHtml += "          <td style='background-color:#64beed;text-align:center;background-color:#c65811;width:20%;'>";
                //        sHtml += "              <label>COMIDA</label><br /><label>14:00 a 15:00</label>";
                //        sHtml += "          </td>";
                //        sHtml += "          <td style='background-color:#64beed;text-align:center;background-color:#6600cd;width:20%;'>";
                //        sHtml += "              <label>CENA</label><br /></label>20:00 a 21:00</label>";
                //        sHtml += "          </td>";
                //        sHtml += "          <td style='background-color:#64beed;text-align:center;background-color:#335398;width:20%;'>";
                //        sHtml += "              <br /><label>TOTAL</label>";
                //        sHtml += "          </td>";
                //        sHtml += "      </tr>";

                //        //Content
                //        DataRow[] dRow = dtAlimentos.Select("Fecha='" + dtMovDis.Rows[i]["FechaMov"].S() + "'");

                //        decimal dTotalDesayunoMXN = 0;
                //        decimal dTotalComidaMXN = 0;
                //        decimal dTotalCenaMXN = 0;
                //        decimal dTotalMXN = 0;

                //        decimal dTotalDesayunoUSD = 0;
                //        decimal dTotalComidaUSD = 0;
                //        decimal dTotalCenaUSD = 0;
                //        decimal dTotalUSD = 0;

                //        //PESOS MXN
                //        for (int y = 0; y < dRow.Length; y++)
                //        {
                //            if (dRow[y]["Moneda"].S() == "MXN")
                //            {
                //                dTotalDesayunoMXN += dRow[y]["Desayuno"].S().D();
                //                dTotalComidaMXN += dRow[y]["Comida"].S().D();
                //                dTotalCenaMXN += dRow[y]["Cena"].S().D();
                //            }
                //        }
                //        dTotalMXN = ((dTotalDesayunoMXN + dTotalComidaMXN) + dTotalCenaMXN);

                //        for (int z = 0; z < dRow.Length; z++)
                //        {
                //            if (dRow[z]["Moneda"].S() == "USD")
                //            {
                //                dTotalDesayunoUSD += dRow[z]["Desayuno"].S().D();
                //                dTotalComidaUSD += dRow[z]["Comida"].S().D();
                //                dTotalCenaUSD += dRow[z]["Cena"].S().D();
                //            }
                //        }
                //        dTotalUSD = ((dTotalDesayunoUSD + dTotalComidaUSD) + dTotalCenaUSD);

                //        //MXN
                //        sHtml += "<tr style='color:#000000;'>";
                //        sHtml += "  <td style='background-color:#ffffff;text-align:center;'>";
                //        sHtml += "      MXN";
                //        sHtml += "  </td>";
                //        sHtml += "  <td style='background-color:#ffffff;text-align:right;'>";
                //        sHtml += dTotalDesayunoMXN.ToString("c");
                //        sHtml += "  </td>";
                //        sHtml += "  <td style='background-color:#ffffff;text-align:right;'>";
                //        sHtml += dTotalComidaMXN.ToString("c");
                //        sHtml += "  </td>";
                //        sHtml += "  <td style='background-color:#ffffff;text-align:right;'>";
                //        sHtml += dTotalCenaMXN.ToString("c");
                //        sHtml += "  </td>";
                //        sHtml += "  <td style='background-color:#ffffff;text-align:right;'>";
                //        sHtml += dTotalMXN.ToString("c");
                //        sHtml += "  </td>";
                //        sHtml += "</tr>";

                //        //USD
                //        sHtml += "<tr style='color:#000000;'>";
                //        sHtml += "  <td style='background-color:#ffffff;text-align:center;'>";
                //        sHtml += "      USD";
                //        sHtml += "  </td>";
                //        sHtml += "  <td style='background-color:#ffffff;text-align:right;'>";
                //        sHtml += dTotalDesayunoUSD.ToString("c");
                //        sHtml += "  </td>";
                //        sHtml += "  <td style='background-color:#ffffff;text-align:right;'>";
                //        sHtml += dTotalComidaUSD.ToString("c");
                //        sHtml += "  </td>";
                //        sHtml += "  <td style='background-color:#ffffff;text-align:right;'>";
                //        sHtml += dTotalCenaUSD.ToString("c");
                //        sHtml += "  </td>";
                //        sHtml += "  <td style='background-color:#ffffff;text-align:right;'>";
                //        sHtml += dTotalUSD.ToString("c");
                //        sHtml += "  </td>";
                //        sHtml += "</tr>";

                //        //ENVIO TOTALES PARA CARGAR ULTIMA TABLA
                //        CargarTotales(dtMovDis.Rows[i]["FechaMov"].S(), dTotalUSD, dTotalMXN);

                //        //sHtml += ""; CONTENIDO DE TABLA DE ALIMENTOS
                //        sHtml += " </table><br />";

                //    }
                //}

                //sHtml += "</div>";
                //#endregion



                //sHtml += "</div>";




                //sHtml += "</div>";
                //sHtml += "</div>";
                //---------------------------------------------------------


                sHtml += "      </div>";
                sHtml += "  </div>";
                sHtml += "</body>";
                sHtml += "</html>";
                return sHtml;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
        public void CargarTotales(string sFechaVuelo, decimal dTotalUSD, decimal dTotalMXN)
        {
            try
            {
                DataRow dr;

                if (dtTotales == null)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("FECHAVUELO");
                    dt.Columns.Add("TOTALUSD");
                    dt.Columns.Add("TOTALMXN");

                    dr = dt.NewRow();
                    dr["FECHAVUELO"] = sFechaVuelo;
                    dr["TOTALUSD"] = dTotalUSD.ToString("c");
                    dr["TOTALMXN"] = dTotalMXN.ToString("c");
                    dt.Rows.Add(dr);
                    dtTotales = dt;
                }
                else
                {
                    dr = dtTotales.NewRow();
                    dr["FECHAVUELO"] = sFechaVuelo;
                    dr["TOTALUSD"] = dTotalUSD.ToString("c");
                    dr["TOTALMXN"] = dTotalMXN.ToString("c");
                    dtTotales.Rows.Add(dr);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Recibe Periodos guardados en base de datos y llena segundo gridview
        /// </summary>
        public void LlenaPeriodosGuardados(DataTable dt)
        {
            try
            {
                dtGuardados = null;
                dtGuardados = dt;
                gvCalculo.DataSource = dt;
                gvCalculo.DataBind();

                if (dt != null && dt.Rows.Count > 0)
                    btnExportar.Enabled = true;
                else
                    btnExportar.Enabled = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LlenaViaticosGuardados(DataSet ds)
        {
            try
            {
                if (ds != null & ds.Tables[0].Rows.Count > 0)
                {
                    //gvGuardados_MXNUSD.DataSource = ds.Tables[1];
                    //gvGuardados_MXNUSD.DataBind();

                    //decimal dDesayunoMXN = 0;
                    //decimal dDesayunoUSD = 0;
                    //decimal dComidaMXN = 0;
                    //decimal dComidaUSD = 0;
                    //decimal dCenaMXN = 0;
                    //decimal dCenaUSD = 0;

                    //if (ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
                    //{
                    //    dDesayunoMXN = ds.Tables[2].Rows[0]["MontoMXN"].S().D();
                    //    dDesayunoUSD = ds.Tables[2].Rows[0]["MontoUSD"].S().D();
                    //    dComidaMXN = ds.Tables[2].Rows[1]["MontoMXN"].S().D();
                    //    dComidaUSD = ds.Tables[2].Rows[1]["MontoUSD"].S().D();
                    //    dCenaMXN = ds.Tables[2].Rows[2]["MontoMXN"].S().D();
                    //    dCenaUSD = ds.Tables[2].Rows[2]["MontoUSD"].S().D();
                    //}

                    DataTable dtConceptos = new DataTable();
                    dtConceptos = FormatoConceptos(ds.Tables[1]);

                    gvMXNUSD.DataSource = dtConceptos;
                    gvMXNUSD.DataBind();

                    gvHorarios.DataSource = ds.Tables[2];
                    gvHorarios.DataBind();

                    //Viaticos por día
                    DataTable dtViaticosXDia = new DataTable();
                    dtViaticosXDia = FormatoViaticosPorDia(ds.Tables[3]);

                    gvDiasViaticos.DataSource = dtViaticosXDia;
                    gvDiasViaticos.DataBind();


                    //AJUESTES
                    gvAjustesPiloto.DataSource = ds.Tables[4];
                    gvAjustesPiloto.DataBind();

                    //Vuelos
                    gvVuelos.DataSource = ds.Tables[5];
                    gvVuelos.DataBind();


                    //Viaticos Hotel
                    DataTable dtConceptosHotel = new DataTable();
                    dtConceptosHotel = FormatoConceptosHotel(ds.Tables[8]);

                    gvViaticosHotel.DataSource = dtConceptosHotel;
                    gvViaticosHotel.DataBind();

                    //Horarios Hotel
                    gvHorariosHotel.DataSource = ds.Tables[9];
                    gvHorariosHotel.DataBind();

                    //Viaticos de Hotel por día
                    DataTable dtViaticosHotelXDia = new DataTable();
                    dtViaticosHotelXDia = FormatoViaticosHotelPorDia(ds.Tables[10]);

                    gvViaticosHotelPorDia.DataSource = dtViaticosHotelXDia;
                    gvViaticosHotelPorDia.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        

        public DataTable FormatoConceptos(DataTable dt)
        {
            try
            {
                DataTable dtConcepto = new DataTable();
                dtConcepto.Columns.Add("IdConcepto");
                dtConcepto.Columns.Add("CONCEPTO");
                dtConcepto.Columns.Add("NACIONAL");
                dtConcepto.Columns.Add("INTERNACIONAL");
                dtConcepto.Columns.Add("TOTALMXN");
                dtConcepto.Columns.Add("TOTALUSD");

                decimal dTotalMXN = 0;
                decimal dTotalUSD = 0;

                DataRow _dr;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    _dr = dtConcepto.NewRow();
                    _dr["IdConcepto"] = dt.Rows[i]["IdConcepto"].S();
                    _dr["CONCEPTO"] = dt.Rows[i]["CONCEPTO"].S();
                    _dr["NACIONAL"] = dt.Rows[i]["NACIONAL"].S();
                    _dr["INTERNACIONAL"] = dt.Rows[i]["INTERNACIONAL"].S();
                    _dr["TOTALMXN"] = dt.Rows[i]["TOTALMXN"].S();
                    _dr["TOTALUSD"] = dt.Rows[i]["TOTALUSD"].S();
                    dtConcepto.Rows.Add(_dr);

                    dTotalMXN += dt.Rows[i]["TOTALMXN"].S().D();
                    dTotalUSD += dt.Rows[i]["TOTALUSD"].S().D();
                }
                _dr = dtConcepto.NewRow();
                _dr["IdConcepto"] = dt.Rows.Count + 1;
                _dr["CONCEPTO"] = "TOTAL";
                _dr["NACIONAL"] = dTotalMXN.ToString("c") + " MXN";
                _dr["INTERNACIONAL"] = dTotalUSD.ToString("c") + " USD";
                _dr["TOTALMXN"] = 0;
                _dr["TOTALUSD"] = 0;
                dtConcepto.Rows.Add(_dr);

                return dtConcepto;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable FormatoConceptosHotel(DataTable dt)
        {
            try
            {
                DataTable dtConcepto = new DataTable();
                dtConcepto.Columns.Add("IdHotel");
                dtConcepto.Columns.Add("CONCEPTO");
                dtConcepto.Columns.Add("NACIONAL");
                dtConcepto.Columns.Add("INTERNACIONAL");
                dtConcepto.Columns.Add("TOTALMXN");
                dtConcepto.Columns.Add("TOTALUSD");

                decimal dTotalMXN = 0;
                decimal dTotalUSD = 0;

                DataRow _dr;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    _dr = dtConcepto.NewRow();
                    _dr["IdHotel"] = dt.Rows[i]["IdHotel"].S();
                    _dr["CONCEPTO"] = dt.Rows[i]["CONCEPTO"].S();
                    _dr["NACIONAL"] = dt.Rows[i]["NACIONAL"].S();
                    _dr["INTERNACIONAL"] = dt.Rows[i]["INTERNACIONAL"].S();
                    _dr["TOTALMXN"] = dt.Rows[i]["TOTALMXN"].S();
                    _dr["TOTALUSD"] = dt.Rows[i]["TOTALUSD"].S();
                    dtConcepto.Rows.Add(_dr);

                    dTotalMXN += dt.Rows[i]["TOTALMXN"].S().D();
                    dTotalUSD += dt.Rows[i]["TOTALUSD"].S().D();
                }
                _dr = dtConcepto.NewRow();
                _dr["IdHotel"] = dt.Rows.Count + 1;
                _dr["CONCEPTO"] = "TOTAL";
                _dr["NACIONAL"] = dTotalMXN.ToString("c") + " MXN";
                _dr["INTERNACIONAL"] = dTotalUSD.ToString("c") + " USD";
                _dr["TOTALMXN"] = 0;
                _dr["TOTALUSD"] = 0;
                dtConcepto.Rows.Add(_dr);

                return dtConcepto;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataTable FormatoViaticosPorDia(DataTable dt)
        {
            try
            {
                DataView view = new DataView(dt);
                DataTable distinctValues = view.ToTable(true, "Fecha");

                DataTable dtViaticos = new DataTable();
                dtViaticos.Columns.Add("Fecha");
                dtViaticos.Columns.Add("TipoCambio");
                dtViaticos.Columns.Add("Desayuno");
                dtViaticos.Columns.Add("Comida");
                dtViaticos.Columns.Add("Cena");
                dtViaticos.Columns.Add("DesayunoNac");
                dtViaticos.Columns.Add("DesayunoInt");
                dtViaticos.Columns.Add("ComidaNac");
                dtViaticos.Columns.Add("ComidaInt");
                dtViaticos.Columns.Add("CenaNac");
                dtViaticos.Columns.Add("CenaInt");
                dtViaticos.Columns.Add("Total");

                DataRow dr;

                for (int i = 0; i < distinctValues.Rows.Count; i++)
                {
                    decimal dTipoCambio = 0;
                    decimal dDesNac = 0;
                    decimal dDesInt = 0;
                    decimal dComNac = 0;
                    decimal dComInt = 0;
                    decimal dCenNac = 0;
                    decimal dCenInt = 0;
                    decimal dTotal = 0;

                    for (int x = 0; x < dt.Rows.Count; x++)
                    {

                        if (distinctValues.Rows[i]["Fecha"].S() == dt.Rows[x]["Fecha"].S() && dt.Rows[x]["Moneda"].S() == "MXN")
                        {
                            dTipoCambio = dt.Rows[x]["TipoCambio"].S().D();
                            dDesNac = dt.Rows[x]["DesNac"].S().D();
                            dComNac = dt.Rows[x]["ComNac"].S().D();
                            dCenNac = dt.Rows[x]["CenNac"].S().D();
                            dTotal = dt.Rows[x]["Total"].S().D();
                        }
                        else if (distinctValues.Rows[i]["Fecha"].S() == dt.Rows[x]["Fecha"].S() && dt.Rows[x]["Moneda"].S() == "USD")
                        {
                            dTipoCambio = dt.Rows[x]["TipoCambio"].S().D();
                            dDesInt = dt.Rows[x]["DesInt"].S().D() * dTipoCambio;
                            dComInt = dt.Rows[x]["ComInt"].S().D() * dTipoCambio;
                            dCenInt = dt.Rows[x]["CenInt"].S().D() * dTipoCambio;
                            dTotal = dt.Rows[x]["Total"].S().D();
                        }


                    }
                    dTotal = (((((dDesNac + dDesInt) + dComNac) + dComInt) + dCenNac) + dCenInt);
                    dr = dtViaticos.NewRow();


                    dr["Fecha"] = distinctValues.Rows[i]["Fecha"].S().Dt().Day.S() + " " + GetMes(distinctValues.Rows[i]["Fecha"].S().Dt().Month).Substring(0, 3) + " " + distinctValues.Rows[i]["Fecha"].S().Dt().Year.S();

                    dTipoCambio = new DBCalculoPagos().ObtenerTipoCambio(distinctValues.Rows[i]["Fecha"].S());
                    dr["TipoCambio"] = dTipoCambio.ToString("c") + " MXN";
                    dr["Desayuno"] = 0;
                    dr["Comida"] = 0;
                    dr["Cena"] = 0;
                    dr["DesayunoNac"] = dDesNac.ToString("c") + " MXN";
                    dr["DesayunoInt"] = dDesInt.ToString("c") + " MXN";
                    dr["ComidaNac"] = dComNac.ToString("c") + " MXN";
                    dr["ComidaInt"] = dComInt.ToString("c") + " MXN";
                    dr["CenaNac"] = dCenNac.ToString("c") + " MXN";
                    dr["CenaInt"] = dCenInt.ToString("c") + " MXN";
                    dr["Total"] = dTotal.ToString("c") + " MXN";
                    dtViaticos.Rows.Add(dr);
                }

                return dtViaticos;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataTable FormatoViaticosHotelPorDia(DataTable dt)
        {
            try
            {
                DataView view = new DataView(dt);
                DataTable distinctValues = view.ToTable(true, "Fecha");

                DataTable dtViaticos = new DataTable();
                dtViaticos.Columns.Add("Fecha");
                dtViaticos.Columns.Add("TipoCambio");
                dtViaticos.Columns.Add("HotelNac");
                dtViaticos.Columns.Add("HotelInt");
                dtViaticos.Columns.Add("Total");

                DataRow dr;

                for (int i = 0; i < distinctValues.Rows.Count; i++)
                {
                    decimal dTipoCambio = 0;
                    decimal dHotNac = 0;
                    decimal dHotInt = 0;
                    decimal dTotal = 0;

                    for (int x = 0; x < dt.Rows.Count; x++)
                    {

                        if (distinctValues.Rows[i]["Fecha"].S() == dt.Rows[x]["Fecha"].S() && dt.Rows[x]["Moneda"].S() == "MXN")
                        {
                            dTipoCambio = dt.Rows[x]["TipoCambio"].S().D();
                            dHotNac += dt.Rows[x]["HotNac"].S().D();
                            dTotal = dt.Rows[x]["Total"].S().D();
                        }
                        else if (distinctValues.Rows[i]["Fecha"].S() == dt.Rows[x]["Fecha"].S() && dt.Rows[x]["Moneda"].S() == "USD")
                        {
                            dTipoCambio = dt.Rows[x]["TipoCambio"].S().D();
                            dHotInt += dt.Rows[x]["HotInt"].S().D() * dTipoCambio;
                            dTotal = dt.Rows[x]["Total"].S().D();
                        }


                    }
                    dTotal = dHotNac + dHotInt;
                    dr = dtViaticos.NewRow();


                    dr["Fecha"] = distinctValues.Rows[i]["Fecha"].S().Dt().Day.S() + " " + GetMes(distinctValues.Rows[i]["Fecha"].S().Dt().Month).Substring(0, 3) + " " + distinctValues.Rows[i]["Fecha"].S().Dt().Year.S();

                    dTipoCambio = new DBCalculoPagos().ObtenerTipoCambio(distinctValues.Rows[i]["Fecha"].S());
                    dr["TipoCambio"] = dTipoCambio.ToString("c") + " MXN";
                    dr["HotelNac"] = dHotNac.ToString("c") + " MXN";
                    dr["HotelInt"] = dHotInt.ToString("c") + " MXN";
                    dr["Total"] = dTotal.ToString("c") + " MXN";
                    dtViaticos.Rows.Add(dr);
                }

                return dtViaticos;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion

        #region PROPIEDADES Y VARIABLES
        CalculoPagos_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eSearchConceptos;
        public event EventHandler eSearchVuelos;
        public event EventHandler eSearchCalculos;
        public event EventHandler eGetParams;
        public event EventHandler eGetAdicionales;
        public event EventHandler eSavePeriodos;
        public event EventHandler eSearchEstatus;
        public event EventHandler eSearchPeriodo;
        public event EventHandler eSearchConAdPeriodo;
        public event EventHandler eSaveAjustes;
        public event EventHandler eSearchAjustesPiloto;
        public event EventHandler eRemoveAjuste;
        public event EventHandler eSearchExistePeriodoPic;
        public event EventHandler eSaveViaticosDia;
        public event EventHandler eSearchReporte;
        public event EventHandler eSearchReporteGral;
        public event EventHandler eSearchGuardados;
        public event EventHandler eSearchViaticosGuardados;

        public string sCvePiloto
        {
            get { return (string)ViewState["VSCvePiloto"]; }
            set { ViewState["VSCvePiloto"] = value; }
        }
        public string sFechaInicio
        {
            get { return (string)ViewState["VSFechaInicio"]; }
            set { ViewState["VSFechaInicio"] = value; }
        }
        public string sFechaFinal
        {
            get { return (string)ViewState["VSFechaFinal"]; }
            set { ViewState["VSFechaFinal"] = value; }
        }
        public string sParametro
        {
            get { return (string)ViewState["VSParametro"]; }
            set { ViewState["VSParametro"] = value; }
        }
        public string sFechaDesde
        {
            get { return (string)ViewState["VSFechaDesde"]; }
            set { ViewState["VSFechaDesde"] = value; }
        }
        public string sFechaHasta
        {
            get { return (string)ViewState["VSFechaHasta"]; }
            set { ViewState["VSFechaHasta"] = value; }
        }
        public List<CantidadComidas> oLstCant
        {
            get { return (List<CantidadComidas>)ViewState["VSCantidadComidas"]; }
            set { ViewState["VSCantidadComidas"] = value; }
        }
        public List<CantidadHoteles> oLstCantH
        {
            get { return (List<CantidadHoteles>)ViewState["VSCantidadHoteles"]; }
            set { ViewState["VSCantidadHoteles"] = value; }
        }
        public int iIdPeriodo
        {
            get { return (int)ViewState["VSIdPeriodo"]; }
            set { ViewState["VSIdPeriodo"] = value; }
        }
        public int iEstatus
        {
            get { return (int)ViewState["VSiEstatus"]; }
            set { ViewState["VSiEstatus"] = value; }
        }
        public int iExistePeriodo
        {
            get { return (int)ViewState["VSExistePeriodo"]; }
            set { ViewState["VSExistePeriodo"] = value; }
        }
        public int iIdAjuste
        {
            get { return (int)ViewState["VSIdAjuste"]; }
            set { ViewState["VSIdAjuste"] = value; }
        }
        public string sOk
        {
            get { return (string)ViewState["VSOk"]; }
            set { ViewState["VSOk"] = value; }
        }
        public DataSet dsParams
        {
            get { return (DataSet)ViewState["VSParametros"]; }
            set { ViewState["VSParametros"] = value; }
        }
        public DataSet dsParamsH
        {
            get { return (DataSet)ViewState["VSParametrosHotel"]; }
            set { ViewState["VSParametrosHotel"] = value; }
        }
        public DataTable dtAjustes
        {
            get { return (DataTable)ViewState["VSAjustes"]; }
            set { ViewState["VSAjustes"] = value; }
        }
        public DataTable dtAjustesPiloto
        {
            get { return (DataTable)ViewState["VSAjustesPiloto"]; }
            set { ViewState["VSAjustesPiloto"] = value; }
        }
        public DataTable dtConceptos
        {
            get { return (DataTable)ViewState["VSConceptos"]; }
            set { ViewState["VSConceptos"] = value; }
        }
        public DataTable dtHoteles
        {
            get { return (DataTable)ViewState["VSHoteles"]; }
            set { ViewState["VSHoteles"] = value; }
        }
        public DataTable dtVuelosXPiloto
        {
            get { return (DataTable)ViewState["VSVuelosXPiloto"]; }
            set { ViewState["VSVuelosXPiloto"] = value; }
        }
        public DataTable dtVuelos
        {
            get { return (DataTable)ViewState["VSdtVuelos"]; }
            set { ViewState["VSdtVuelos"] = value; }
        }
        public DataTable dtNal
        {
            get { return (DataTable)ViewState["VSdtNal"]; }
            set { ViewState["VSdtNal"] = value; }
        }
        public DataTable dtInt
        {
            get { return (DataTable)ViewState["VSdtInt"]; }
            set { ViewState["VSdtInt"] = value; }
        }

        public DataTable dtHotNal
        {
            get { return (DataTable)ViewState["VSdtHotNal"]; }
            set { ViewState["VSdtHotNal"] = value; }
        }
        public DataTable dtHotInt
        {
            get { return (DataTable)ViewState["VSdtHotInt"]; }
            set { ViewState["VSdtHotInt"] = value; }
        }

        public DataTable dtMXNUSD
        {
            get { return (DataTable)ViewState["VSMXNUSD"]; }
            set { ViewState["VSMXNUSD"] = value; }
        }
        public DataTable dtVuelosPiloto
        {
            get { return (DataTable)ViewState["VSdtVuelosPiloto"]; }
            set { ViewState["VSdtVuelosPiloto"] = value; }
        }
        public DataTable dtVuelosPiloto1x1
        {
            get { return (DataTable)ViewState["VSdtVuelosPiloto1x1"]; }
            set { ViewState["VSdtVuelosPiloto1x1"] = value; }
        }
        public DataTable dtCalculos
        {
            get { return (DataTable)ViewState["VSCalculos"]; }
            set { ViewState["VSCalculos"] = value; }
        }
        public DataTable dtCalculos2
        {
            get { return (DataTable)ViewState["VSCalculos2"]; }
            set { ViewState["VSCalculos2"] = value; }
        }

        public DataTable dtCalculos1X1
        {
            get { return (DataTable)ViewState["VSCalculos1X1"]; }
            set { ViewState["VSCalculos1X1"] = value; }
        }

        public List<PeriodoPiloto> oLstPeriodo
        {
            get { return (List<PeriodoPiloto>)ViewState["VSPeriodos"]; }
            set { ViewState["VSPeriodos"] = value; }
        }
        public List<ConceptosPiloto> oLst
        {
            get { return (List<ConceptosPiloto>)ViewState["VSConceptosPilotos"]; }
            set { ViewState["VSConceptosPilotos"] = value; }
        }
        public List<ConceptosPilotoSave> oLstCon
        {
            get { return (List<ConceptosPilotoSave>)ViewState["VSConceptosPilotoSave"]; }
            set { ViewState["VSConceptosPilotoSave"] = value; }
        }
        public List<HotelesPiloto> oLstHP
        {
            get { return (List<HotelesPiloto>)ViewState["VSHotelesPiloto"]; }
            set { ViewState["VSHotelesPiloto"] = value; }
        }
        public List<HotelesPilotoSave> oLstHPS
        {
            get { return (List<HotelesPilotoSave>)ViewState["VSHotelesPilotoSave"]; }
            set { ViewState["VSHotelesPilotoSave"] = value; }
        }
        public List<ConceptosAdicionalesPiloto> oLstAd
        {
            get { return (List<ConceptosAdicionalesPiloto>)ViewState["VSConceptosAdicionalesPiloto"]; }
            set { ViewState["VSConceptosAdicionalesPiloto"] = value; }
        }
        public List<VuelosPiernasPiloto> oLstVP
        {
            get { return (List<VuelosPiernasPiloto>)ViewState["VSVuelosPiernasPiloto"]; }
            set { ViewState["VSVuelosPiernasPiloto"] = value; }
        }
        public List<VuelosPiernasPilotoSave> oLstVPil
        {
            get { return (List<VuelosPiernasPilotoSave>)ViewState["VSVuelosPiernasPilotoSave"]; }
            set { ViewState["VSVuelosPiernasPilotoSave"] = value; }
        }
        public PeriodoPiloto oPer
        {
            get 
            {
                PeriodoPiloto oPer = new PeriodoPiloto();
                oPer.SCvePiloto = readCvePiloto.Text;
                oPer.SFechaInicio = hdnFechaInicio.Value;
                oPer.SFechaFinal = hdnFechaFinal.Value;
                oPer.SUsuario = ((UserIdentity)Session["UserIdentity"]).sUsuario;
                return oPer;
            }
        }
        public PeriodoPiloto oPe
        {
            get { return (PeriodoPiloto)ViewState["VSPeriodoPiloto"]; }
            set { ViewState["VSPeriodoPiloto"] = value; }
        }
        public ConceptosAdicionalesPiloto oAjuste
        {
            get
            {
                ConceptosAdicionalesPiloto oAjuste = new ConceptosAdicionalesPiloto();
                oAjuste.IIdPeriodo = hdnIdPeriodo.Value.I();
                oAjuste.IId_Concepto = ddlConceptoAdicional.Value.I();
                oAjuste.SDesConcepto = ddlConceptoAdicional.Text.S();
                oAjuste.DValor = txtImporte.Value.S().D();
                oAjuste.SMoneda = ddlMoneda.Value.S();
                oAjuste.SComentarios = txtComentarios.Value.S();
                return oAjuste;
            }
        }
        public DataTable dtDiasViaticos 
        {
            get { return (DataTable)ViewState["VSdtDiasViaticos"]; }
            set { ViewState["VSdtDiasViaticos"] = value; }
        }
        public DataTable dtDiasViaticosHoteles
        {
            get { return (DataTable)ViewState["VSDiasViaticosHoteles"]; }
            set { ViewState["VSeDiasViaticosHoteles"] = value; }
        }
        public DataTable dtViaticosDiaInsert
        {
            get { return (DataTable)ViewState["VSViaticosDiaInsert"]; }
            set { ViewState["VSViaticosDiaInsert"] = value; }
        }
        public DataTable dtViaticosHotelDiaInsert
        {
            get { return (DataTable)ViewState["VSViaticosHotelDiaInsert"]; }
            set { ViewState["VSViaticosHotelDiaInsert"] = value; }
        }
        public List<ConceptosViaticosPorDia> oLstPorDia
        {
            get { return (List<ConceptosViaticosPorDia>)ViewState["VSConceptosViaticosPorDia"]; }
            set { ViewState["VSConceptosViaticosPorDia"] = value; }
        }
        public List<ConceptosViaticosPorDiaSave> oLstPorDiaPil
        {
            get { return (List<ConceptosViaticosPorDiaSave>)ViewState["VSConceptosViaticosPorDiaSave"]; }
            set { ViewState["VSConceptosViaticosPorDiaSave"] = value; }
        }

        public List<ViaticosHotelPorDia> oLstHotelPorDia
        {
            get { return (List<ViaticosHotelPorDia>)ViewState["VSHotelPorDia"]; }
            set { ViewState["VSHotelPorDia"] = value; }
        }

        public List<ViaticosHotelPorDiaSave> oLstHotelPorDiaSave
        {
            get { return (List<ViaticosHotelPorDiaSave>)ViewState["VSHotelPorDiaSave"]; }
            set { ViewState["VSHotelPorDiaSave"] = value; }
        }

        public DataTable dtTotales
        {
            get { return (DataTable)ViewState["VSdtTotales"]; }
            set { ViewState["VSdtTotales"] = value; }
        }
        public DataTable dtGuardados
        {
            get { return (DataTable)ViewState["VSdtGuardados"]; }
            set { ViewState["VSdtGuardados"] = value; }
        }
        public DataTable dtResumenViaticos
        {
            get { return (DataTable)ViewState["VSResumenViaticos"]; }
            set { ViewState["VSResumenViaticos"] = value; }
        }
        public DataTable dtResumenViaticosHotel
        {
            get { return (DataTable)ViewState["VSResumenViaticosHotel"]; }
            set { ViewState["VSResumenViaticosHotel"] = value; }
        }

        public DataTable dtViaticosHot
        {
            get { return (DataTable)ViewState["VSViaticosHot"]; }
            set { ViewState["VSViaticosHot"] = value; }
        }
        #endregion


    }
}