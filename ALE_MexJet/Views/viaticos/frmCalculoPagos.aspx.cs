using ALE_MexJet.DomainModel;
using ALE_MexJet.Interfaces;
using ALE_MexJet.Objetos;
using ALE_MexJet.Presenter;
using DevExpress.Web;
using DevExpress.Web.Bootstrap;
using NucleoBase.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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

                //if (eSearchObj != null)
                //    eSearchObj(sender, e);
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
                    string[] fieldValues = { "Piloto", "CrewCode", "FechaInicio", "FechaFin", "Estatus" };
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

                        readPeríodo.Text = "Del " + dtFecha1.Day.S() + " de " + sMesDel + " de " + dtFecha1.Year.S() + " al " + dtFecha2.Day.S() + " de " + sMesHasta + " de " + dtFecha2.Year.S();
                        hdnFechaInicio.Value = dtFecha1.ToShortDateString();
                        hdnFechaFinal.Value = dtFecha2.ToShortDateString();
                        
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
                        }

                        pnlDatosPiloto.Visible = true;
                        pnlBusqueda.Visible = false;
                        pnlVuelos.Visible = false;
                        pnlCalcularViaticos.Visible = false;
                        pnlAjuste.Visible = true;
                    }
                }
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
                    //ASPxButton btnVerAjustes = gvB.FindRowCellTemplateControl(e.VisibleIndex, e.DataColumn, "btnVerAjustes") as ASPxButton;

                    //int iIdBitacora = gvB.GetRowValues(e.VisibleIndex, "CrewCode").S().I();
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
                            btnVerAjustes.Enabled = false;
                        else if (iEstatus == 1 && iExistePeriodo == 1)
                            btnVerAjustes.Enabled = false;
                        else if (iEstatus == 2 && iExistePeriodo == 1)
                            btnVerAjustes.Enabled = true;
                        else if (iEstatus == 3 && iExistePeriodo == 1)
                            btnVerAjustes.Enabled = true;
                    }
                }
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
                //GeneraCalculo();

                gvCalculo.DataSource = dtCalculos;
                gvCalculo.DataBind();
                pnlVuelos.Visible = true;
            }
            catch (Exception ex)
            {
                throw ex;
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
                                drow = dt.NewRow();
                                drow["Fecha"] = dtDiasViaticos.Rows[i]["FechaDia"].S().Dt();
                                drow["Moneda"] = "MXN";
                                drow["Desayuno"] = dtDiasViaticos.Rows[i]["DesNal"].S().D() * ObtenValorConcepto(1, "MXN");
                                drow["Comida"] = "";
                                drow["Cena"] = "";
                                drow["Total"] = "";
                            }
                            else
                            {
                                drow = dt.NewRow();
                                drow["Fecha"] = dtDiasViaticos.Rows[i]["FechaDia"].S().Dt();
                                drow["Moneda"] = "USD";
                                drow["Desayuno"] = "";
                                drow["Comida"] = "";
                                drow["Cena"] = "";
                                drow["Total"] = "";
                            }
                        }

                        
                    }
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

                DataRow[] dr = dtConceptos.Select("IdConcepto=" + IdConcepto + " AND Moneda='" + sMoneda + "'");

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

        #endregion

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
    }
}