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
            gvPeriodosGuardados.SettingsPager.PageSizeItemSettings.Visible = true;
            gvPeriodosGuardados.SettingsPager.PageSizeItemSettings.Position = PagerPageSizePosition.Right;
            gvPeriodosGuardados.SettingsText.SearchPanelEditorNullText = "Ingresa la información a buscar:";
            gvPeriodosGuardados.Settings.ShowGroupPanel = true;
            gvPeriodosGuardados.Settings.ShowTitlePanel = true;
            gvPeriodosGuardados.SettingsText.Title = "Periodos Pendientes";

            gvVuelos.Settings.ShowGroupPanel = false;

            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(date1.Text))
                    sFechaDesde = DateTime.Now.ToShortDateString();
                if (string.IsNullOrEmpty(date2.Text))
                    sFechaHasta = DateTime.Now.ToShortDateString();

                sParametro = txtParametro.Text;

                if (eSearchObj != null)
                    eSearchObj(sender, e);

                if (eSearchConceptos != null)
                    eSearchConceptos(sender, e);

                if (eGetParams != null)
                    eGetParams(sender, e);
            }
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                sFechaDesde = date1.Text;
                sFechaHasta = date2.Text;
                sParametro = txtParametro.Text;

                if (eSearchCalculos != null)
                    eSearchCalculos(sender, e);

                if (eSearchObj != null)
                    eSearchObj(sender, e);

                sFechaInicio = sFechaDesde;
                sFechaFinal = sFechaHasta;

                if (eSearchReporteGral != null)
                    eSearchReporteGral(sender, e);
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

                        //-----------------------------------------------------


                        pnlDatosPiloto.Visible = true;
                        pnlBusqueda.Visible = false;
                        pnlVuelos.Visible = false;
                        pnlCalcularViaticos.Visible = true;
                        upaVuelos.Update();

                        if (eSearchCalculos != null)
                            eSearchCalculos(sender, e);

                        if (dtCalculos != null && dtCalculos.Rows.Count > 0)
                        {
                            CargaViaticos();
                        }
                    }

                }
                else if (e.CommandArgs.CommandName.S() == "Ajustes")
                {
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
                    string[] fieldValues = { "Piloto", "CrewCode", "FechaInicio", "FechaFin", "Estatus", "HomeBase" };
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

                        //-----------------------------------------------------


                        pnlDatosPiloto.Visible = true;
                        pnlBusqueda.Visible = false;
                        pnlVuelos.Visible = false;
                        pnlCalcularViaticos.Visible = true;
                        upaVuelos.Update();

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
                                dMonto = dtConceptos.Rows[x]["MontoMXN"].S().D();
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
                                dMonto = dtConceptos.Rows[x]["MontoUSD"].S().D();
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
                    oCD.DDesayuno = dtViaticosDiaInsert.Rows[i]["Desayuno"].S().Replace("$","").D();
                    oCD.DComida = dtViaticosDiaInsert.Rows[i]["Comida"].S().Replace("$", "").D();
                    oCD.DCena = dtViaticosDiaInsert.Rows[i]["Cena"].S().Replace("$", "").D();
                    oCD.DTotal = dtViaticosDiaInsert.Rows[i]["Total"].S().Replace("$", "").D();

                    if (dtViaticosDiaInsert.Rows[i]["Moneda"].S() == "USD")
                        oCD.DTipoCambio = new DBCalculoPagos().ObtenerTipoCambio(dtViaticosDiaInsert.Rows[i]["Fecha"].S());
                    else
                        oCD.DTipoCambio = 0;

                    oLsViPorDia.Add(oCD);
                }
                oLstPorDia = oLsViPorDia;

                if (eSaveObj != null)
                    eSaveObj(sender, e);

                if (sOk == "ok")
                {
                    btnCancelar_Click(sender, e);
                    MostrarMensaje("¡Se ha guardado los datos calculados del piloto!", "Guardado");
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
                for (int i = 0; i < dtCalculos.Rows.Count; i++)
                {
                    List<PeriodoPiloto> oLsPer = new List<PeriodoPiloto>();
                    List<ConceptosPiloto> oLsConPiloto = new List<ConceptosPiloto>();
                    List<VuelosPiernasPiloto> oLsVuelos = new List<VuelosPiernasPiloto>();

                    PeriodoPiloto oPer = new PeriodoPiloto();
                    oPer.SCvePiloto = dtCalculos.Rows[i]["CrewCode"].S();
                    oPer.SFechaInicio = dtCalculos.Rows[i]["FechaInicio"].S();
                    oPer.SFechaFinal = dtCalculos.Rows[i]["FechaFin"].S();
                    oPer.SUsuario = ((UserIdentity)Session["UserIdentity"]).sUsuario;

                    int iDesNal = dtCalculos.Rows[i]["DesayunosNal"].S().I();
                    int iComNal = dtCalculos.Rows[i]["ComidasNal"].S().I();
                    int iCenNal = dtCalculos.Rows[i]["CenasNal"].S().I();

                    int iDesInt = dtCalculos.Rows[i]["DesayunosInt"].S().I();
                    int iComInt = dtCalculos.Rows[i]["ComidasInt"].S().I();
                    int iCenInt = dtCalculos.Rows[i]["CenasInt"].S().I();

                    //Conceptos

                    #region NACIONALES
                    for (int x = 0; x < dtConceptos.Rows.Count; x++)
                    {
                        ConceptosPiloto oCP = new ConceptosPiloto();
                        oCP.IIdPeriodo = 0;
                        int iIdConcepto = 0;
                        decimal dMonto = 0;

                        if (dtConceptos.Rows[x]["DesConcepto"].S().ToUpper() == "DESAYUNO")
                        {
                            iIdConcepto = dtConceptos.Rows[x]["IdConcepto"].S().I();
                            dMonto = dtConceptos.Rows[x]["MontoMXN"].S().D();

                            oCP.IIdConcepto = iIdConcepto;
                            oCP.SDesConcepto = dtConceptos.Rows[x]["DesConcepto"].S();
                            oCP.ICantidad = iDesNal;
                            oCP.DMontoConcepto = dMonto;
                            oCP.SMoneda = "MXN";
                            oCP.DTotal = iDesNal * dMonto;
                            oLsConPiloto.Add(oCP);
                        }
                        else if (dtConceptos.Rows[x]["DesConcepto"].S().ToUpper() == "COMIDA")
                        {
                            iIdConcepto = dtConceptos.Rows[x]["IdConcepto"].S().I();
                            dMonto = dtConceptos.Rows[x]["MontoMXN"].S().D();

                            oCP.IIdConcepto = iIdConcepto;
                            oCP.SDesConcepto = dtConceptos.Rows[x]["DesConcepto"].S();
                            oCP.ICantidad = iComNal;
                            oCP.DMontoConcepto = dMonto;
                            oCP.SMoneda = "MXN";
                            oCP.DTotal = iComNal * dMonto;
                            oLsConPiloto.Add(oCP);
                        }
                        else if (dtConceptos.Rows[x]["DesConcepto"].S().ToUpper() == "CENA")
                        {
                            iIdConcepto = dtConceptos.Rows[x]["IdConcepto"].S().I();
                            dMonto = dtConceptos.Rows[x]["MontoMXN"].S().D();

                            oCP.IIdConcepto = iIdConcepto;
                            oCP.SDesConcepto = dtConceptos.Rows[x]["DesConcepto"].S();
                            oCP.ICantidad = iCenNal;
                            oCP.DMontoConcepto = dMonto;
                            oCP.SMoneda = "MXN";
                            oCP.DTotal = iCenNal * dMonto;
                            oLsConPiloto.Add(oCP);
                        }
                        //oLst = oLsConPiloto;
                    }
                    #endregion

                    #region INTERNACIONALES
                    for (int x = 0; x < dtConceptos.Rows.Count; x++)
                    {
                        ConceptosPiloto oCP = new ConceptosPiloto();
                        oCP.IIdPeriodo = 0;
                        int iIdConcepto = 0;
                        decimal dMonto = 0;

                        if (dtConceptos.Rows[x]["DesConcepto"].S().ToUpper() == "DESAYUNO")
                        {
                            iIdConcepto = dtConceptos.Rows[x]["IdConcepto"].S().I();
                            dMonto = dtConceptos.Rows[x]["MontoUSD"].S().D();

                            oCP.IIdConcepto = iIdConcepto;
                            oCP.SDesConcepto = dtConceptos.Rows[x]["DesConcepto"].S();
                            oCP.ICantidad = iDesInt;
                            oCP.DMontoConcepto = dMonto;
                            oCP.SMoneda = "USD";
                            oCP.DTotal = iDesInt * dMonto;
                            oLsConPiloto.Add(oCP);
                        }
                        else if (dtConceptos.Rows[x]["DesConcepto"].S().ToUpper() == "COMIDA")
                        {
                            iIdConcepto = dtConceptos.Rows[x]["IdConcepto"].S().I();
                            dMonto = dtConceptos.Rows[x]["MontoUSD"].S().D();

                            oCP.IIdConcepto = iIdConcepto;
                            oCP.SDesConcepto = dtConceptos.Rows[x]["DesConcepto"].S();
                            oCP.ICantidad = iComInt;
                            oCP.DMontoConcepto = dMonto;
                            oCP.SMoneda = "USD";
                            oCP.DTotal = iComInt * dMonto;
                            oLsConPiloto.Add(oCP);
                        }
                        else if (dtConceptos.Rows[x]["DesConcepto"].S().ToUpper() == "CENA")
                        {
                            iIdConcepto = dtConceptos.Rows[x]["IdConcepto"].S().I();
                            dMonto = dtConceptos.Rows[x]["MontoUSD"].S().D();

                            oCP.IIdConcepto = iIdConcepto;
                            oCP.SDesConcepto = dtConceptos.Rows[x]["DesConcepto"].S();
                            oCP.ICantidad = iCenInt;
                            oCP.DMontoConcepto = dMonto;
                            oCP.SMoneda = "USD";
                            oCP.DTotal = iCenInt * dMonto;
                            oLsConPiloto.Add(oCP);
                        }

                    }
                    #endregion

                    #region VUELOS

                    DataRow[] dr = dtVuelosPiloto.Select("crewcode='" + dtCalculos.Rows[i]["CrewCode"].S() + "'");

                    for (int x = 0; x < dr.Length; x++)
                    {
                        VuelosPiernasPiloto oV = new VuelosPiernasPiloto();
                        oV.IIdPeriodo = 0;
                        oV.LTrip = dr[x]["Trip"].S().L();
                        oV.LLegId = dr[x]["LegId"].S().L();
                        oLsVuelos.Add(oV);
                    }
                    oLstVP = oLsVuelos;

                    #endregion

                    oLsPer.Add(oPer);



                    oLstPeriodo = oLsPer;
                    oLst = oLsConPiloto;

                    if (eSavePeriodos != null)
                        eSavePeriodos(sender, e);
                }
                btnExportar.Enabled = true;
                


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
                if (e.DataColumn.FieldName == "IdFolio")
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
                gvCalculo.DataSource = dtCalculos2;
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
        public void LlenaCalculoPilotos(DataTable dt)
        {
            try
            {
                dtCalculos = dt;
                dtCalculos2 = dt;

                DataTable dt1 = new DataTable();
                DataTable dt2 = new DataTable();

                dt1 = dtCalculos.Copy();
                dt2 = dtCalculos2.Copy();

                dt1.TableName = "Calculos";
                dt2.TableName = "Calculos2";

                DataSet ds = new DataSet();
                ds.Tables.Add(dt1);
                ds.Tables.Add(dt2);

                //GeneraCalculo();
                if (dtCalculos != null && dtCalculos.Rows.Count > 0)
                {
                    btnAprobar.Visible = true;
                    pnlVuelos.Visible = true;

                    string sPeriodoBusqueda = string.Empty;
                    sPeriodoBusqueda = "Del " + date1.Text.Dt().Day.S() + " de " + GetMes(date1.Text.Dt().Month) + " al " + date2.Text.Dt().Day.S() + " de " + GetMes(date2.Text.Dt().Month) + " de " + date2.Text.Dt().Year.S();
                    lblPeriodoBusqueda.Text = sPeriodoBusqueda;

                    //Periodos sin guardar
                    //gvCalculo.DataSource = dtCalculos;
                    //gvCalculo.DataBind();

                    DataTable dtPendientes = new DataTable();
                    dtPendientes = EliminarPeriodosGuardado(ds.Tables["Calculos2"], 1);

                    //Periodos pendientes
                    gvPeriodosGuardados.DataSource = dtPendientes;
                    gvPeriodosGuardados.DataBind();

                    dtCalculos = null;
                    dtCalculos = dtPendientes;


                    DataTable dtGuardados = new DataTable();
                    dtGuardados = EliminarPeriodosGuardado(ds.Tables["Calculos"], 2);
                    //Periodos guadados
                    gvCalculo.DataSource = dtGuardados; //dt;
                    gvCalculo.DataBind();

                    dtCalculos2 = null;
                    dtCalculos2 = dtGuardados;


                    //Periodos guardados
                    //gvPeriodosGuardados.DataSource = dtCalculos;
                    //gvPeriodosGuardados.DataBind();

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
        public DataTable EliminarPeriodosGuardado(DataTable dtSource, int ban)
        {
            try
            {
                //DataTable dtRes = new DataTable();
                //dtRes = dtSource;

                //for (int i = dtSource.Rows.Count - 1; i >= 0; i

                if (ban == 1)
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
                    }
                    dtSource.AcceptChanges();
                }
                else if (ban == 2)
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

                        DataRow dr2 = dtSource.Rows[i];

                        if ((iExistePeriodo == 0) && (iEstatus == 0 || iEstatus == 1))
                            dtSource.Rows.Remove(dr2);
                    }
                    dtSource.AcceptChanges();
                }
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
                #endregion

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
                dr2["INTERNACIONAL"] = dtCalculos.Rows[0]["TotalUSD"].S().D().ToString("c");
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
                dr3["NACIONAL"] = dtCalculos.Rows[0]["TotalPesos"].S().D().ToString("c");
                dr3["INTERNACIONAL"] = dtCalculos.Rows[0]["TotalUSD"].S().D().ToString("c");
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
                                drow["Desayuno"] = dTotalDesNal.ToString("c");
                                drow["Comida"] = dTotalComNal.ToString("c");
                                drow["Cena"] = dTotalCenNal.ToString("c");
                                drow["Total"] = dTotalNal.ToString("c");
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
                                drow["Desayuno"] = dTotalDesInt.ToString("c");
                                drow["Comida"] = dTotalComInt.ToString("c");
                                drow["Cena"] = dTotalCenInt.ToString("c");
                                drow["Total"] = dTotalInt.ToString("c");
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
                        drDias["Fecha"] = dtDiasDistinct.Rows[i]["Dia"].S().Dt().Day.S() + " " + GetMes(dtDiasDistinct.Rows[i]["Dia"].S().Dt().Month).Substring(0,3) + " " + dtDiasDistinct.Rows[i]["Dia"].S().Dt().Year.S();

                        dTipoCambio = new DBCalculoPagos().ObtenerTipoCambio(drDias["Fecha"].S());

                        //DataView dvDV = new DataView(dtDiasVia);
                        //DataTable dtDiasDV = dvDV.ToTable(true, "FechaDia", "DesNal", "ComNal", "CenNal", "DesInt", "ComInt", "CenInt");

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

                        drDias["TipoCambio"] = dTipoCambio.ToString("c");
                        drDias["Desayuno"] = iCountDesayuno;
                        drDias["Comida"] = iCountComida;
                        drDias["Cena"] = iCountCena;

                        drDias["DesayunoNac"] = dDesNac.ToString("c");
                        drDias["DesayunoInt"] = dDesInt.ToString("c");
                        drDias["ComidaNac"] = dComNac.ToString("c");
                        drDias["ComidaInt"] = dComInt.ToString("c");
                        drDias["CenaNac"] = dCenNac.ToString("c");
                        drDias["CenaInt"] = dCenInt.ToString("c");
                        drDias["Total"] = dTotal.ToString("c");
                        dt.Rows.Add(drDias);
                    }

                    gvDiasViaticos.DataSource = dt;
                    gvDiasViaticos.DataBind();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
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
        public void LlenaVuelosPiloto(DataTable dt)
        {
            dtVuelosPiloto = null;
            dtVuelosPiloto = dt;
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
                    string sHtml = ImprimirReporte(ds);
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
                    string filename = "Reporte.pdf";
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
                        btnExportar.Enabled = true;
                        string sContent = CrearReporteGral(ds);
                        if (!string.IsNullOrEmpty(sContent))
                        {
                            divReporte.InnerHtml = sContent;
                        }
                    }
                    else
                        btnExportar.Enabled = false;
                }
                else
                    btnExportar.Enabled = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string CrearReporteGral(DataSet dsGral)
        {
            try
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
                sHtml += "  <div>";

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



                    sHtml += "<table width='100%' border='0' class='table'>";

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
                        sHtml += "          <table class='table table-bordered' style='margin: 25px auto; width: 100%; color:#ffffff;'>";
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
                            sHtml += "      <table class='table table-bordered' style='margin: 25px auto; width:100%; color:#ffffff;'>";
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
        public DataTable dtViaticosDiaInsert
        {
            get { return (DataTable)ViewState["VSViaticosDiaInsert"]; }
            set { ViewState["VSViaticosDiaInsert"] = value; }
        }
        public List<ConceptosViaticosPorDia> oLstPorDia
        {
            get { return (List<ConceptosViaticosPorDia>)ViewState["VSConceptosViaticosPorDia"]; }
            set { ViewState["VSConceptosViaticosPorDia"] = value; }
        }
        public DataTable dtTotales
        {
            get { return (DataTable)ViewState["VSdtTotales"]; }
            set { ViewState["VSdtTotales"] = value; }
        }


        #endregion

        
    }
}