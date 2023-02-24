using ALE_MexJet.DomainModel;
using ALE_MexJet.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using NucleoBase.Core;
using ALE_MexJet.Objetos;
using ALE_MexJet.Clases;

namespace ALE_MexJet.Presenter
{
    public class CalculoPagos_Presenter : BasePresenter<IViewCalculoPagos>
    {
        private readonly DBCalculoPagos oIGestCat;
        public CalculoPagos_Presenter(IViewCalculoPagos oView, DBCalculoPagos oGC)
            : base(oView)
        {
            oIGestCat = oGC;
            oIView.eSearchVuelos += SearchVuelos_Presenter;
            oIView.eSearchConceptos += SearchConceptos_Presenter;
            oIView.eSearchCalculos += SearchCalculos_Presenter;
            oIView.eGetParams += eGetParams_Presenter;
            oIView.eGetAdicionales += GetAdicionales_Presenter;
            oIView.eSavePeriodos += SavePeriodos_Presenter;
            oIView.eSearchEstatus += SearchEstatus_Presenter;
            oIView.eSearchPeriodo += SearchPeriodo_Presenter;
            oIView.eSearchConAdPeriodo += SearchConAdPeriodo_Presenter;
            oIView.eSaveAjustes += SaveAjustes_Presenter;
            oIView.eSearchAjustesPiloto += SearchAjustesPiloto_Presenter;
            oIView.eRemoveAjuste += RemoveAjuste_Presenter;
            oIView.eSearchExistePeriodoPic += SearchExistePeriodoPic_Presenter;
            oIView.eSearchReporte += SearchReporte_Presenter;
            oIView.eSearchReporteGral += SearchReporteGral_Presenter;
            oIView.eSearchGuardados += SearchGuardados_Presenter;
            oIView.eSearchViaticosGuardados += SearchViaticosGuardados_Presenter;
    }

        protected void GetAdicionales_Presenter(object sender, EventArgs e)
        {
            oIView.LoadsGrids(oIGestCat.ObtieneConceptosAdicionales());
        }

        protected override void SearchObj_Presenter(object sender, EventArgs e)
        {
            oIView.LoadPilotos(oIGestCat.GetPilotosVuelos(oIView.sFechaDesde, oIView.sFechaHasta, oIView.sParametro));
        }

        protected void SearchVuelos_Presenter(object sender, EventArgs e)
        {
            oIView.LoadVuelos(oIGestCat.GetVuelos(oIView.sFechaDesde, oIView.sFechaHasta, oIView.sParametro));
        }

        protected void SearchConceptos_Presenter(object sender, EventArgs e)
        {
            oIView.LoadConceptos(oIGestCat.GetConceptos());
        }

        DataTable dtComidas = new DataTable();
        protected void SearchCalculos_Presenter(object sender, EventArgs e)
        {
            string sCve = string.Empty;
            try
            {
                #region CONSULTA VUELOS
                DataSet ds = oIGestCat.ObtieneVuelosDelPeriodo(oIView.sFechaDesde.Dt(), oIView.sFechaHasta.Dt(), oIView.sParametro);
                DataTable dtVuelos = ds.Tables[0];
                //DataTable dtPilotos = ds.Tables[1]; //new DBCalculoPagos().ObtienePilotosFPK;

                DataTable dtPilotSrc = ds.Tables[1];
                dtComidas = null;
                //List<ComidasPorDia> oLsComDia;

                //List<CantidadComidas> oLstCant;

                for (int x = 0; x < dtPilotSrc.Rows.Count; x++)
                {
                    sCve = dtPilotSrc.Rows[x]["ClavePiloto"].S();
                    DataRow drPiloto;
                    DataTable dtPilotos = new DataTable();
                    dtPilotos.Columns.Add("Id");
                    dtPilotos.Columns.Add("ClavePiloto");
                    dtPilotos.Columns.Add("NoVuelos");
                    dtPilotos.Columns.Add("Piloto");
                    dtPilotos.Columns.Add("Estatus");
                    dtPilotos.Columns.Add("HomeBase");

                    drPiloto = dtPilotos.NewRow();
                    drPiloto["Id"] = dtPilotSrc.Rows[x]["Id"].S();
                    drPiloto["ClavePiloto"] = dtPilotSrc.Rows[x]["ClavePiloto"].S();
                    drPiloto["NoVuelos"] = dtPilotSrc.Rows[x]["NoVuelos"].S();
                    drPiloto["Piloto"] = dtPilotSrc.Rows[x]["Piloto"].S();
                    drPiloto["Estatus"] = dtPilotSrc.Rows[x]["Estatus"].S();
                    drPiloto["HomeBase"] = dtPilotSrc.Rows[x]["HomeBase"].S();
                    dtPilotos.Rows.Add(drPiloto);

                    //dtPilotos.Columns.Add("NoVuelos", typeof(int));
                    DataTable dtVlos = CreaEstructuraVuelos();
                    string sBase = string.Empty;
                    string FechaInicio = "CheckIn"; //"Locdep";
                    string FechaFin = "CheckOut"; //"LocArr";

                    //= new List<CantidadComidas>();
                    #endregion

                    List<CantidadComidas> oLstCant = new List<CantidadComidas>();

                    #region PRIMERA PARTE DE CODIGO
                    foreach (DataRow row in dtPilotos.Rows)
                    {
                        
                        //List<CantidadComidas> oLstCant = new List<CantidadComidas>();
                        DataRow[] rows = dtVuelos.Select("crewcode = '" + row["ClavePiloto"].S() + "'");
                        DataTable dtVuelosPil = dtVuelos.Clone();

                        PilotosCalculo oPil = new PilotosCalculo();
                        oPil.sCrewCode = row["ClavePiloto"].S();
                        oPil.dImporte = 0;

                        for (int i = 0; i < rows.Length; i++)
                        {
                            if (sBase == string.Empty)
                                sBase = rows[i]["HomeBase"].S();

                            if (i == 0 && rows[i]["duty_type"].S() == "DH")
                                rows[i]["POD"] = sBase;
                            else if (rows.Length > 1 && rows[i]["duty_type"].S() == "DH")
                            {
                                rows[i]["POD"] = rows[i - 1]["POA"].S();
                            }
                            else
                                rows[i]["POD"] = rows[i]["POD"].S();

                            dtVuelosPil.ImportRow(rows[i]);
                        }


                        int iVuelo = 1;
                        int iPierna = 1;
                        string sPOA = string.Empty;
                        string sPOD = string.Empty;


                        for (int i = 0; i < dtVuelosPil.Rows.Count; i++)
                        {
                            DataRow dr1 = dtVuelosPil.Rows[i];
                            DataRow dr = dtVlos.NewRow();

                            dr["NoVuelo"] = iVuelo;
                            dr["NoPierna"] = iPierna;
                            dr["LegId"] = dr1["LegId"].S().I();
                            dr["FechaDia"] = dr1["FechaHoraReal"].S().Dt();
                            dr["POD"] = dr1["POD"].S();
                            dr["POA"] = dr1["POA"].S();
                            dr["FechaSalida"] = dr1["FechaSalida"].S().Dt();
                            dr["FechaLlegada"] = dr1["FechaLlegada"].S();
                            dr["LocDep"] = dr1["LocDep"].S().Dt();
                            dr["LocArr"] = dr1["LocArr"].S().Dt();
                            dr["CrewCode"] = dr1["crewcode"].S();
                            dr["DutyType"] = dr1["duty_type"].S();
                            dr["HomeBase"] = dr1["HomeBase"].S();
                            dr["EsInternacional"] = dr1["EsInternacional"].S();
                            dr["SeCobra"] = dr1["SeCobra"].S();
                            dr["Dia"] = dr1[FechaInicio].S().Dt().Day; //dr1["Dia"].S();
                            dr["PaisPOD"] = dr1["PaisPOD"].S();
                            dr["PaisPOA"] = dr1["PaisPOA"].S();
                            dr["CheckIn"] = dr1["CheckIn"].S().Dt();
                            dr["CheckOut"] = dr1["CheckOut"].S().Dt();


                            if (dr["POA"].S() == sBase)
                            {
                                iVuelo++;
                                iPierna = 1;
                            }
                            else
                                iPierna++;

                            dtVlos.Rows.Add(dr);

                            if (dr["POA"].S() != sBase && i == dtVuelosPil.Rows.Count - 1)
                                iVuelo++;
                        }

                        row["NoVuelos"] = iVuelo - 1;
                    }
                    #endregion

                    #region SEGUNDA PARTE DE CODIGO
                    float fInicioDesayuno = 7;
                    float fFinDesayuno = 9;
                    float fInicioComida = 15;
                    float fFinComida = 17;
                    float fInicioCena = 19;
                    float fFinCena = 21;
                    int iHorasPernocta = 6;
                    int iHorasDayUse = 8;

                    DataTable dt = new DBCalculoPagos().ObtieneParametrosPernoctas(); //oIGestCat.GetConceptos();
                    if (dt.Rows.Count > 0)
                    {
                        fInicioDesayuno = float.Parse(Utils.ConvierteTiempoaDecimal(dt.Rows[0]["DesayunoInicial"].S()).S());
                        fFinDesayuno = float.Parse(Utils.ConvierteTiempoaDecimal(dt.Rows[0]["DesayunoFinal"].S()).S());
                        fInicioComida = float.Parse(Utils.ConvierteTiempoaDecimal(dt.Rows[0]["ComidaInicial"].S()).S());
                        fFinComida = float.Parse(Utils.ConvierteTiempoaDecimal(dt.Rows[0]["ComidaFinal"].S()).S());
                        fInicioCena = float.Parse(Utils.ConvierteTiempoaDecimal(dt.Rows[0]["CenaInicial"].S()).S());
                        fFinCena = float.Parse(Utils.ConvierteTiempoaDecimal(dt.Rows[0]["CenaFinal"].S()).S());
                        iHorasPernocta = dt.Rows[0]["HorasPernocta"].S().I();
                        iHorasDayUse = dt.Rows[0]["HorasDayUse"].S().I();
                    }

                    #endregion

                    //List<CantidadComidas> oLstCant = new List<CantidadComidas>();

                    #region TERCERA PARTE DEL CODIGO
                    HorarioAlimentos oHor = new HorarioAlimentos();
                    oHor.fInicioDesayuno = fInicioDesayuno;
                    oHor.fFinDesayuno = fFinDesayuno;
                    oHor.fInicioComida = fInicioComida;
                    oHor.fFinComida = fFinComida;
                    oHor.fInicioCena = fInicioCena;
                    oHor.fFinCena = fFinCena;

                    foreach (DataRow drP in dtPilotos.Rows)
                    {
                        if(drP["ClavePiloto"].S() == "CLR")
                        {
                            string sCo = "0";
                        }
                        //List<ComidasPorDia> oLsComDia = new List<ComidasPorDia>();

                        int iVlos = drP["NoVuelos"].S().I();
                        if (iVlos > 0)
                        {
                            for (int i = 1; i <= iVlos; i++)
                            {
                                DataTable dtRows = dtVlos.Clone(); //dtVlos.Select("CrewCode = '" + drP["ClavePiloto"].S() + "' AND NoVuelo = " + i.S());
                                foreach (DataRow rowAux in dtVlos.Rows)
                                {
                                    if (rowAux["CrewCode"].S() == drP["ClavePiloto"].S() && rowAux["NoVuelo"].S() == i.S())
                                    {
                                        dtRows.ImportRow(rowAux);
                                    }
                                }

                                DataRow[] rowsNC = dtVlos.Select("CrewCode = '" + drP["ClavePiloto"].S() + "' AND SeCobra = 'False'");

                                if (dtRows.Rows.Count > 0)
                                {
                                    DataTable dtLegs = dtVlos.Clone();
                                    DataTable dtVlosNC = dtVlos.Clone();
                                    for (int j = 1; j <= dtRows.Rows.Count; j++)
                                    {
                                        for (int k = 0; k < dtRows.Rows.Count; k++)
                                        {
                                            if (dtRows.Rows[k]["NoPierna"].S().I() == j)
                                            {
                                                if (dtRows.Rows[k]["SeCobra"].S().B())
                                                {
                                                    dtLegs.ImportRow(dtRows.Rows[k]);
                                                    break;
                                                }
                                            }
                                        }
                                    }


                                    CantidadComidas oCant = new CantidadComidas();
                                    oCant.sCrewCode = drP["ClavePiloto"].S();
                                    oCant.dtFechaInicio = oIView.sFechaDesde.Dt(); //oIView.dtFechaInicio;
                                    oCant.dtFechaFin = oIView.sFechaHasta.Dt(); //oIView.dtFechaFin;
                                    oCant.dtVuelos = dtLegs;
                                    oCant.iVuelo = i;

                                    List<ComidasPorDia> oLsComDia = new List<ComidasPorDia>();

                                    if (dtLegs.Rows.Count > 0)
                                    {
                                        ObtienePernoctasYDayUse(oCant, dtLegs, FechaInicio, FechaFin, iHorasPernocta, iHorasDayUse);

                                        DataTable dtDias = CreaEstructuraDias();

                                        foreach (DataRow dr in dtLegs.Rows)
                                        {
                                            if (!VerificaExisteValor(dtDias, dr[FechaInicio].S().Dt().Day.S(), "Dia"))
                                            {
                                                DataRow drN = dtDias.NewRow();
                                                drN["NoVuelo"] = i.S();
                                                drN["Dia"] = dr[FechaInicio].S().Dt().Day.S();

                                                dtDias.Rows.Add(drN);
                                            }
                                        }

                                        float fHoraInicio = 0;
                                        float fHoraFinal = 0;

                                        DateTime dtIni = dtLegs.Rows[0][FechaInicio].S().Dt();
                                        DateTime dtFin = dtLegs.Rows[dtLegs.Rows.Count - 1][FechaFin].S().Dt();

                                        //DataTable dtCheckDate = ds.Tables[2];

                                        //DataRow[] rowCheck = dtCheckDate.Select("Dia='" + dtDias.Rows[0]["Dia"].S() + "' ");


                                        //DateTime dtIni = rowCheck[0]["CheckIn"].S().Dt();
                                        //DateTime dtFin = rowCheck[0]["CheckOut"].S().Dt();

                                        bool bEsInterInicio = false;
                                        bool bEsInterFinal = false;

                                        bEsInterInicio = dtLegs.Rows[0]["EsInternacional"].S() == "1" ? true : false;
                                        bEsInterFinal = dtLegs.Rows[dtLegs.Rows.Count - 1]["EsInternacional"].S() == "1" ? true : false;

                                        fHoraInicio = dtIni.Hour + (dtIni.Minute / float.Parse("60"));
                                        fHoraFinal = dtFin.Hour + (dtFin.Minute / float.Parse("60"));

                                        string sPod = dtLegs.Rows[0]["POD"].S();

                                        #region CÓDIGO DE CALCULO

                                        #region CUANDO ES SÓLO UN DÍA
                                        if (dtDias.Rows.Count == 1)
                                        {
                                            DataRow[] rowsD = dtLegs.Select("Dia = '" + dtDias.Rows[0]["Dia"].S() + "' ");

                                            if (rowsD.Length > 1)
                                            {
                                                for (int k = 0; k < rowsD.Length; k++)
                                                {
                                                    fHoraInicio = 0;
                                                    fHoraFinal = 0;

                                                    dtIni = dtLegs.Rows[0][FechaInicio].S().Dt();
                                                    dtFin = dtLegs.Rows[dtLegs.Rows.Count - 1][FechaFin].S().Dt();

                                                    bEsInterInicio = false;
                                                    bEsInterFinal = false;

                                                    bEsInterInicio = dtLegs.Rows[0]["EsInternacional"].S() == "1" ? true : false;
                                                    bEsInterFinal = dtLegs.Rows[dtLegs.Rows.Count - 1]["EsInternacional"].S() == "1" ? true : false;

                                                    fHoraInicio = dtIni.Hour + (dtIni.Minute / float.Parse("60"));
                                                    fHoraFinal = dtFin.Hour + (dtFin.Minute / float.Parse("60"));

                                                    ComidasPorDia oComDia1 = new ComidasPorDia();
                                                    oComDia1.sClavePiloto = drP["ClavePiloto"].S();
                                                    oComDia1.dtFechaDia = rowsD[k][FechaInicio].S().Dt();
                                                    oComDia1.dtFechaFin = rowsD[k][FechaFin].S().Dt();
                                                    oComDia1.sduty_type = rowsD[k]["DutyType"].S();
                                                    oComDia1.sOrigen = rowsD[k]["POD"].S();
                                                    oComDia1.sDestino = rowsD[k]["POA"].S();   

                                                    if (k + 1 == rowsD.Length)
                                                    {
                                                        DateTime dtIniDia = rowsD[0][FechaInicio].S().Dt();
                                                        DateTime dtFinDia = rowsD[rowsD.Length - 1][FechaFin].S().Dt();

                                                        fHoraInicio = dtIniDia.Hour + (dtIniDia.Minute / float.Parse("60"));
                                                        fHoraFinal = dtFinDia.Hour + (dtFinDia.Minute / float.Parse("60"));

                                                        #region DESAYUNO
                                                        //if (fHoraInicio < fInicioDesayuno || (fHoraInicio > fInicioDesayuno && fHoraInicio < fFinDesayuno)) //Linea origen

                                                        //Primera condicion
                                                        if (((fHoraInicio < fInicioDesayuno && fHoraFinal > fInicioDesayuno) && fHoraFinal < fFinDesayuno) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                                                        {
                                                            if (bEsInterFinal) { oCant.iCantDesayunosInt++; oComDia1.iDesayunosInt++; } else { oCant.iCantDesayunos++; oComDia1.iDesayunosNal++; }
                                                        }
                                                        //Segunda condición
                                                        else if ((fHoraInicio > fInicioDesayuno && fHoraInicio < fFinDesayuno) && (fHoraFinal > fFinDesayuno && fHoraFinal > fInicioDesayuno) && (oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioComida)) //Evaluar antes de comida
                                                        {
                                                            if (bEsInterFinal) { oCant.iCantDesayunosInt++; oComDia1.iDesayunosInt++; } else { oCant.iCantDesayunos++; oComDia1.iDesayunosNal++; }
                                                        }
                                                        //Tercera condición
                                                        else if ((fHoraInicio >= fInicioDesayuno && fHoraFinal > fFinDesayuno) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioComida))
                                                        {
                                                            if (bEsInterFinal) { oCant.iCantDesayunosInt++; oComDia1.iDesayunosInt++; } else { oCant.iCantDesayunos++; oComDia1.iDesayunosNal++; }
                                                        }
                                                        //Cuarta condición
                                                        else if ((fHoraInicio < fInicioDesayuno && fHoraFinal <= fInicioDesayuno) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                                                        {
                                                            if (bEsInterFinal) { oCant.iCantDesayunosInt++; oComDia1.iDesayunosInt++; } else { oCant.iCantDesayunos++; oComDia1.iDesayunosNal++; }
                                                        }
                                                        //Quinta condición
                                                        else if ((fHoraInicio < fInicioDesayuno && fHoraFinal < fInicioDesayuno) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                                                        {
                                                            if (bEsInterFinal) { oCant.iCantDesayunosInt++; oComDia1.iDesayunosInt++; } else { oCant.iCantDesayunos++; oComDia1.iDesayunosNal++; }
                                                        }
                                                        //Sexta condición
                                                        else if ((fHoraInicio < fInicioDesayuno && (fHoraFinal > fInicioDesayuno && fHoraFinal <= fFinDesayuno)) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                                                        {
                                                            if (bEsInterFinal) { oCant.iCantDesayunosInt++; oComDia1.iDesayunosInt++; } else { oCant.iCantDesayunos++; oComDia1.iDesayunosNal++; }
                                                        }
                                                        //Septima condición
                                                        else if ((fHoraInicio >= fInicioDesayuno && fHoraInicio <= fFinDesayuno) && (fHoraFinal > fInicioDesayuno && fHoraFinal <= fFinDesayuno) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                                                        {
                                                            if (bEsInterFinal) { oCant.iCantDesayunosInt++; oComDia1.iDesayunosInt++; } else { oCant.iCantDesayunos++; oComDia1.iDesayunosNal++; }
                                                        }

                                                        //Octava condición
                                                        else if ((fHoraInicio < fInicioDesayuno && (fHoraFinal > fInicioDesayuno && fHoraFinal >= fFinDesayuno)) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioComida))
                                                        {
                                                            if (bEsInterFinal) { oCant.iCantDesayunosInt++; oComDia1.iDesayunosInt++; } else { oCant.iCantDesayunos++; oComDia1.iDesayunosNal++; }
                                                        }
                                                        #endregion

                                                        #region COMIDA
                                                        //Primera condicion Comidas
                                                        if (((fHoraInicio < fInicioComida && fHoraFinal > fInicioComida) && fHoraFinal < fFinComida) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                                                        {
                                                            if (bEsInterFinal) { oCant.iCantComidasInt++; oComDia1.iComidaInt++; } else { oCant.iCantComidas++; oComDia1.iComidaNal++; }
                                                        }
                                                        //Segunda condición Comidas
                                                        else if ((fHoraInicio > fInicioComida && fHoraInicio < fFinComida) && (fHoraFinal > fFinComida && fHoraFinal > fInicioComida) && (oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioCena)) //Evaluar antes de cena
                                                        {
                                                            if (bEsInterFinal) { oCant.iCantComidasInt++; oComDia1.iComidaInt++; } else { oCant.iCantComidas++; oComDia1.iComidaNal++; }
                                                        }
                                                        //Tercera condición Comidas
                                                        else if ((fHoraInicio >= fInicioComida && fHoraFinal > fFinComida) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioCena))
                                                        {
                                                            if (bEsInterFinal) { oCant.iCantComidasInt++; oComDia1.iComidaInt++; } else { oCant.iCantComidas++; oComDia1.iComidaNal++; }
                                                        }
                                                        //Cuarta condición Comidas
                                                        else if ((fHoraInicio < fInicioComida && fHoraFinal <= fInicioComida) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                                                        {
                                                            if (bEsInterFinal) { oCant.iCantComidasInt++; oComDia1.iComidaInt++; } else { oCant.iCantComidas++; oComDia1.iComidaNal++; }
                                                        }
                                                        //Quinta condición Comidas
                                                        else if ((fHoraInicio < fInicioComida && fHoraFinal < fInicioComida) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                                                        {
                                                            if (bEsInterFinal) { oCant.iCantComidasInt++; oComDia1.iComidaInt++; } else { oCant.iCantComidas++; oComDia1.iComidaNal++; }
                                                        }
                                                        //Sexta condición Comidas
                                                        else if ((fHoraInicio < fInicioComida && (fHoraFinal > fInicioComida && fHoraFinal <= fFinComida)) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                                                        {
                                                            if (bEsInterFinal) { oCant.iCantComidasInt++; oComDia1.iComidaInt++; } else { oCant.iCantComidas++; oComDia1.iComidaNal++; }
                                                        }
                                                        //Septima condición Comidas
                                                        else if ((fHoraInicio >= fInicioComida && fHoraInicio <= fFinComida) && (fHoraFinal > fInicioComida && fHoraFinal <= fFinComida) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                                                        {
                                                            if (bEsInterFinal) { oCant.iCantComidasInt++; oComDia1.iComidaInt++; } else { oCant.iCantComidas++; oComDia1.iComidaNal++; }
                                                        }
                                                        //Octava condición
                                                        else if ((fHoraInicio < fInicioComida && (fHoraFinal > fInicioComida && fHoraFinal >= fFinComida)) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioCena))
                                                        {
                                                            if (bEsInterFinal) { oCant.iCantComidasInt++; oComDia1.iComidaInt++; } else { oCant.iCantComidas++; oComDia1.iComidaNal++; }
                                                        }
                                                        #endregion

                                                        #region CENA
                                                        //Primera condicion Cenas
                                                        if (((fHoraInicio < fInicioCena && fHoraFinal > fInicioCena) && fHoraFinal < fFinCena) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                                                        {
                                                            if (bEsInterFinal) { oCant.iCantCenasInt++; oComDia1.iCenaInt++; } else { oCant.iCantCenas++; oComDia1.iCenaNal++; }
                                                        }
                                                        //Segunda condición Cenas
                                                        else if ((fHoraInicio > fInicioCena && fHoraInicio < fFinCena) && (fHoraFinal > fFinCena && fHoraFinal > fInicioCena) && (oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                                                        {
                                                            if (bEsInterFinal) { oCant.iCantCenasInt++; oComDia1.iCenaInt++; } else { oCant.iCantCenas++; oComDia1.iCenaNal++; }
                                                        }
                                                        //Tercera condición Cenas
                                                        else if ((fHoraInicio >= fInicioCena && fHoraFinal > fFinCena) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                                                        {
                                                            if (bEsInterFinal) { oCant.iCantCenasInt++; oComDia1.iCenaInt++; } else { oCant.iCantCenas++; oComDia1.iCenaNal++; }
                                                        }
                                                        //Cuarta condición Cenas
                                                        else if ((fHoraInicio < fInicioCena && fHoraFinal <= fInicioCena) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                                                        {
                                                            if (bEsInterFinal) { oCant.iCantCenasInt++; oComDia1.iCenaInt++; } else { oCant.iCantCenas++; oComDia1.iCenaNal++; }
                                                        }
                                                        //Quinta condición Cenas
                                                        else if ((fHoraInicio < fInicioCena && fHoraFinal < fInicioCena) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                                                        {
                                                            if (bEsInterFinal) { oCant.iCantCenasInt++; oComDia1.iCenaInt++; } else { oCant.iCantCenas++; oComDia1.iCenaNal++; }
                                                        }
                                                        //Sexta condición Cenas
                                                        else if ((fHoraInicio < fInicioCena && (fHoraFinal > fInicioCena && fHoraFinal <= fFinCena)) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                                                        {
                                                            if (bEsInterFinal) { oCant.iCantCenasInt++; oComDia1.iCenaInt++; } else { oCant.iCantCenas++; oComDia1.iCenaNal++; }
                                                        }
                                                        //Septima condición Cenas
                                                        else if ((fHoraInicio >= fInicioCena && fHoraInicio <= fFinCena) && (fHoraFinal > fInicioCena && fHoraFinal <= fFinCena) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                                                        {
                                                            if (bEsInterFinal) { oCant.iCantCenasInt++; oComDia1.iCenaInt++; } else { oCant.iCantCenas++; oComDia1.iCenaNal++; }
                                                        }
                                                        //Octava condición Cenas
                                                        else if ((fHoraInicio < fInicioCena && (fHoraFinal > fInicioCena && fHoraFinal >= fFinCena)) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                                                        {
                                                            if (bEsInterFinal) { oCant.iCantCenasInt++; oComDia1.iCenaInt++; } else { oCant.iCantCenas++; oComDia1.iCenaNal++; }
                                                        }
                                                        #endregion

                                                    }

                                                    oLsComDia.Add(oComDia1);
                                                }
                                            }
                                            else
                                            {
                                                ComidasPorDia oComDia = new ComidasPorDia();
                                                oComDia.dtFechaDia = dtIni;
                                                oComDia.dtFechaFin = dtFin;
                                                oComDia.sClavePiloto = drP["ClavePiloto"].S();
                                                oComDia.sduty_type = dtLegs.Rows[0]["DutyType"].S();
                                                oComDia.sOrigen = dtLegs.Rows[0]["POD"].S();
                                                oComDia.sDestino = dtLegs.Rows[dtLegs.Rows.Count - 1]["POA"].S();

                                                #region DESAYUNO
                                                //if (fHoraInicio < fInicioDesayuno || (fHoraInicio > fInicioDesayuno && fHoraInicio < fFinDesayuno)) //Linea origen

                                                //Primera condicion
                                                if (((fHoraInicio < fInicioDesayuno && fHoraFinal > fInicioDesayuno) && fHoraFinal < fFinDesayuno) && (oComDia.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                                                {
                                                    if (bEsInterFinal) { oCant.iCantDesayunosInt++; oComDia.iDesayunosInt++; }
                                                    else { oCant.iCantDesayunos++; oComDia.iDesayunosNal++; }
                                                }
                                                //Segunda condición
                                                else if ((fHoraInicio > fInicioDesayuno && fHoraInicio < fFinDesayuno) && (fHoraFinal > fFinDesayuno && fHoraFinal > fInicioDesayuno) && (oComDia.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioComida)) //Evaluar antes de comida
                                                {
                                                    if (bEsInterFinal) { oCant.iCantDesayunosInt++; oComDia.iDesayunosInt++; }
                                                    else { oCant.iCantDesayunos++; oComDia.iDesayunosNal++; }
                                                }
                                                //Tercera condición
                                                else if ((fHoraInicio >= fInicioDesayuno && fHoraFinal > fFinDesayuno) && (oComDia.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioComida))
                                                {
                                                    if (bEsInterFinal) { oCant.iCantDesayunosInt++; oComDia.iDesayunosInt++; }
                                                    else { oCant.iCantDesayunos++; oComDia.iDesayunosNal++; }
                                                }
                                                //Cuarta condición
                                                else if ((fHoraInicio < fInicioDesayuno && fHoraFinal <= fInicioDesayuno) && (oComDia.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                                                {
                                                    if (bEsInterFinal) { oCant.iCantDesayunosInt++; oComDia.iDesayunosInt++; }
                                                    else { oCant.iCantDesayunos++; oComDia.iDesayunosNal++; }
                                                }
                                                //Quinta condición
                                                else if ((fHoraInicio < fInicioDesayuno && fHoraFinal < fInicioDesayuno) && (oComDia.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                                                {
                                                    if (bEsInterFinal) { oCant.iCantDesayunosInt++; oComDia.iDesayunosInt++; }
                                                    else { oCant.iCantDesayunos++; oComDia.iDesayunosNal++; }
                                                }
                                                //Sexta condición
                                                else if ((fHoraInicio < fInicioDesayuno && (fHoraFinal > fInicioDesayuno && fHoraFinal <= fFinDesayuno)) && (oComDia.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                                                {
                                                    if (bEsInterFinal) { oCant.iCantDesayunosInt++; oComDia.iDesayunosInt++; }
                                                    else { oCant.iCantDesayunos++; oComDia.iDesayunosNal++; }
                                                }
                                                //Septima condición
                                                else if ((fHoraInicio >= fInicioDesayuno && fHoraInicio <= fFinDesayuno) && (fHoraFinal > fInicioDesayuno && fHoraFinal <= fFinDesayuno) && (oComDia.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                                                {
                                                    if (bEsInterFinal) { oCant.iCantDesayunosInt++; oComDia.iDesayunosInt++; }
                                                    else { oCant.iCantDesayunos++; oComDia.iDesayunosNal++; }
                                                }

                                                //Octava condición
                                                else if ((fHoraInicio < fInicioDesayuno && (fHoraFinal > fInicioDesayuno && fHoraFinal >= fFinDesayuno)) && (oComDia.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioComida))
                                                {
                                                    if (bEsInterFinal) { oCant.iCantDesayunosInt++; oComDia.iDesayunosInt++; }
                                                    else { oCant.iCantDesayunos++; oComDia.iDesayunosNal++; }
                                                }
                                                #endregion

                                                #region COMIDA
                                                //Primera condicion Comidas
                                                if (((fHoraInicio < fInicioComida && fHoraFinal > fInicioComida) && fHoraFinal < fFinComida) && (oComDia.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                                                {
                                                    if (bEsInterFinal) { oCant.iCantComidasInt++; oComDia.iComidaInt++; }
                                                    else { oCant.iCantComidas++; oComDia.iComidaNal++; }
                                                }
                                                //Segunda condición Comidas
                                                else if ((fHoraInicio > fInicioComida && fHoraInicio < fFinComida) && (fHoraFinal > fFinComida && fHoraFinal > fInicioComida) && (oComDia.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioCena)) //Evaluar antes de cena
                                                {
                                                    if (bEsInterFinal) { oCant.iCantComidasInt++; oComDia.iComidaInt++; }
                                                    else { oCant.iCantComidas++; oComDia.iComidaNal++; }
                                                }
                                                //Tercera condición Comidas
                                                else if ((fHoraInicio >= fInicioComida && fHoraFinal > fFinComida) && (oComDia.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioCena))
                                                {
                                                    if (bEsInterFinal) { oCant.iCantComidasInt++; oComDia.iComidaInt++; }
                                                    else { oCant.iCantComidas++; oComDia.iComidaNal++; }
                                                }
                                                //Cuarta condición Comidas
                                                else if ((fHoraInicio < fInicioComida && fHoraFinal <= fInicioComida) && (oComDia.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                                                {
                                                    if (bEsInterFinal) { oCant.iCantComidasInt++; oComDia.iComidaInt++; }
                                                    else { oCant.iCantComidas++; oComDia.iComidaNal++; }
                                                }
                                                //Quinta condición Comidas
                                                else if ((fHoraInicio < fInicioComida && fHoraFinal < fInicioComida) && (oComDia.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                                                {
                                                    if (bEsInterFinal) { oCant.iCantComidasInt++; oComDia.iComidaInt++; }
                                                    else { oCant.iCantComidas++; oComDia.iComidaNal++; }
                                                }
                                                //Sexta condición Comidas
                                                else if ((fHoraInicio < fInicioComida && (fHoraFinal > fInicioComida && fHoraFinal <= fFinComida)) && (oComDia.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                                                {
                                                    if (bEsInterFinal) { oCant.iCantComidasInt++; oComDia.iComidaInt++; }
                                                    else { oCant.iCantComidas++; oComDia.iComidaNal++; }
                                                }
                                                //Septima condición Comidas
                                                else if ((fHoraInicio >= fInicioComida && fHoraInicio <= fFinComida) && (fHoraFinal > fInicioComida && fHoraFinal <= fFinComida) && (oComDia.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                                                {
                                                    if (bEsInterFinal) { oCant.iCantComidasInt++; oComDia.iComidaInt++; }
                                                    else { oCant.iCantComidas++; oComDia.iComidaNal++; }
                                                }
                                                //Octava condición
                                                else if ((fHoraInicio < fInicioComida && (fHoraFinal > fInicioComida && fHoraFinal >= fFinComida)) && (oComDia.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioCena))
                                                {
                                                    if (bEsInterFinal) { oCant.iCantComidasInt++; oComDia.iComidaInt++; }
                                                    else { oCant.iCantComidas++; oComDia.iComidaNal++; }
                                                }
                                                #endregion

                                                #region CENA
                                                //Primera condicion Cenas
                                                if (((fHoraInicio < fInicioCena && fHoraFinal > fInicioCena) && fHoraFinal < fFinCena) && (oComDia.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                                                {
                                                    if (bEsInterFinal) { oCant.iCantCenasInt++; oComDia.iCenaInt++; }
                                                    else { oCant.iCantCenas++; oComDia.iCenaNal++; }
                                                }
                                                //Segunda condición Cenas
                                                else if ((fHoraInicio > fInicioCena && fHoraInicio < fFinCena) && (fHoraFinal > fFinCena && fHoraFinal > fInicioCena) && (oComDia.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                                                {
                                                    if (bEsInterFinal) { oCant.iCantCenasInt++; oComDia.iCenaInt++; }
                                                    else { oCant.iCantCenas++; oComDia.iCenaNal++; }
                                                }
                                                //Tercera condición Cenas
                                                else if ((fHoraInicio >= fInicioCena && fHoraFinal > fFinCena) && (oComDia.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                                                {
                                                    if (bEsInterFinal) { oCant.iCantCenasInt++; oComDia.iCenaInt++; }
                                                    else { oCant.iCantCenas++; oComDia.iCenaNal++; }
                                                }
                                                //Cuarta condición Cenas
                                                else if ((fHoraInicio < fInicioCena && fHoraFinal <= fInicioCena) && (oComDia.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                                                {
                                                    if (bEsInterFinal) { oCant.iCantCenasInt++; oComDia.iCenaInt++; }
                                                    else { oCant.iCantCenas++; oComDia.iCenaNal++; }
                                                }
                                                //Quinta condición Cenas
                                                else if ((fHoraInicio < fInicioCena && fHoraFinal < fInicioCena) && (oComDia.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                                                {
                                                    if (bEsInterFinal) { oCant.iCantCenasInt++; oComDia.iCenaInt++; }
                                                    else { oCant.iCantCenas++; oComDia.iCenaNal++; }
                                                }
                                                //Sexta condición Cenas
                                                else if ((fHoraInicio < fInicioCena && (fHoraFinal > fInicioCena && fHoraFinal <= fFinCena)) && (oComDia.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                                                {
                                                    if (bEsInterFinal) { oCant.iCantCenasInt++; oComDia.iCenaInt++; }
                                                    else { oCant.iCantCenas++; oComDia.iCenaNal++; }
                                                }
                                                //Septima condición Cenas
                                                else if ((fHoraInicio >= fInicioCena && fHoraInicio <= fFinCena) && (fHoraFinal > fInicioCena && fHoraFinal <= fFinCena) && (oComDia.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                                                {
                                                    if (bEsInterFinal) { oCant.iCantCenasInt++; oComDia.iCenaInt++; }
                                                    else { oCant.iCantCenas++; oComDia.iCenaNal++; }
                                                }
                                                //Octava condición Cenas
                                                else if ((fHoraInicio < fInicioCena && (fHoraFinal > fInicioCena && fHoraFinal >= fFinCena)) && (oComDia.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                                                {
                                                    if (bEsInterFinal) { oCant.iCantCenasInt++; oComDia.iCenaInt++; }
                                                    else { oCant.iCantCenas++; oComDia.iCenaNal++; }
                                                }
                                                #endregion

                                                oLsComDia.Add(oComDia);

                                            }
                                        }
                                        #endregion

                                        #region CUANDO SON 2 DÍAS
                                        else if (dtDias.Rows.Count == 2)
                                        {
                                            for (int l = 0; l < dtDias.Rows.Count; l++)
                                            {
                                                DataRow[] rowsD = dtLegs.Select("Dia = '" + dtDias.Rows[l]["Dia"].S() + "' ");

                                                if (rowsD.Length > 1)
                                                {
                                                    #region PIERNAS EN MEXICO Y EN EL EXTRANJERO COMENTADO TEMPORALMENTE
                                                    //if (rowsD[0]["PaisPOA"].S() != rowsD[rowsD.Length - 1]["PaisPOA"].S())
                                                    //{
                                                    //    // PIERNAS EN MEXICO Y EL EXTRANJERO

                                                    //    for (int k = 0; k < rowsD.Length; k++)
                                                    //    {
                                                    //        ComidasPorDia oComDia1 = new ComidasPorDia();
                                                    //        oComDia1.sClavePiloto = drP["ClavePiloto"].S();
                                                    //        oComDia1.dtFechaDia = rowsD[k][FechaInicio].S().Dt();
                                                    //        oComDia1.dtFechaFin = rowsD[k][FechaFin].S().Dt();
                                                    //        oComDia1.sduty_type = rowsD[k]["DutyType"].S();
                                                    //        oComDia1.sOrigen = rowsD[k]["POD"].S();
                                                    //        oComDia1.sDestino = rowsD[k]["POA"].S();

                                                    //        bEsInterInicio = rowsD[k]["PaisPOD"].S() == "MX" ? false : true;
                                                    //        bEsInterFinal = rowsD[k]["PaisPOA"].S() == "MX" ? false : true;

                                                    //        DateTime dtIniDia = rowsD[k][FechaInicio].S().Dt();
                                                    //        DateTime dtFinDia = rowsD[k][FechaFin].S().Dt();

                                                    //        fHoraInicio = dtIniDia.Hour + (dtIniDia.Minute / float.Parse("60"));
                                                    //        fHoraFinal = dtFinDia.Hour + (dtFinDia.Minute / float.Parse("60"));

                                                    //        if (l > 0 && k == 0)
                                                    //            fHoraInicio = 0;

                                                    //        #region DESAYUNO
                                                    //        ////Primera condicion
                                                    //        //if (((fHoraInicio < fInicioDesayuno && fHoraFinal > fInicioDesayuno) && fHoraFinal < fFinDesayuno) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                                                    //        //{
                                                    //        //    if (bEsInterFinal) { oCant.iCantDesayunosInt++; oComDia1.iDesayunosInt++; }
                                                    //        //    else { oCant.iCantDesayunos++; oComDia1.iDesayunosNal++; }
                                                    //        //}
                                                    //        ////Segunda condición
                                                    //        //else if ((fHoraInicio > fInicioDesayuno && fHoraInicio < fFinDesayuno) && (fHoraFinal > fFinDesayuno && fHoraFinal > fInicioDesayuno) && (oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioComida)) //Evaluar antes de comida
                                                    //        //{
                                                    //        //    if (bEsInterFinal) { oCant.iCantDesayunosInt++; oComDia1.iDesayunosInt++; }
                                                    //        //    else { oCant.iCantDesayunos++; oComDia1.iDesayunosNal++; }
                                                    //        //}
                                                    //        ////Tercera condición
                                                    //        //else if ((fHoraInicio >= fInicioDesayuno && fHoraFinal > fFinDesayuno) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                                                    //        //{
                                                    //        //    if (bEsInterFinal) { oCant.iCantDesayunosInt++; oComDia1.iDesayunosInt++; }
                                                    //        //    else { oCant.iCantDesayunos++; oComDia1.iDesayunosNal++; }
                                                    //        //}
                                                    //        ////Cuarta condición
                                                    //        //else if ((fHoraInicio < fInicioDesayuno && fHoraFinal <= fInicioDesayuno) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                                                    //        //{
                                                    //        //    if (bEsInterFinal) { oCant.iCantDesayunosInt++; oComDia1.iDesayunosInt++; }
                                                    //        //    else { oCant.iCantDesayunos++; oComDia1.iDesayunosNal++; }
                                                    //        //}
                                                    //        ////Quinta condición
                                                    //        //else if ((fHoraInicio < fInicioDesayuno && fHoraFinal < fInicioDesayuno) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                                                    //        //{
                                                    //        //    if (bEsInterFinal) { oCant.iCantDesayunosInt++; oComDia1.iDesayunosInt++; }
                                                    //        //    else { oCant.iCantDesayunos++; oComDia1.iDesayunosNal++; }
                                                    //        //}
                                                    //        ////Sexta condición
                                                    //        //else if ((fHoraInicio < fInicioDesayuno && (fHoraFinal > fInicioDesayuno && fHoraFinal <= fFinDesayuno)) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                                                    //        //{
                                                    //        //    if (bEsInterFinal) { oCant.iCantDesayunosInt++; oComDia1.iDesayunosInt++; }
                                                    //        //    else { oCant.iCantDesayunos++; oComDia1.iDesayunosNal++; }
                                                    //        //}
                                                    //        ////Septima condición
                                                    //        //else if ((fHoraInicio >= fInicioDesayuno && fHoraInicio <= fFinDesayuno) && (fHoraFinal > fInicioDesayuno && fHoraFinal <= fFinDesayuno) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                                                    //        //{
                                                    //        //    if (bEsInterFinal) { oCant.iCantDesayunosInt++; oComDia1.iDesayunosInt++; }
                                                    //        //    else { oCant.iCantDesayunos++; oComDia1.iDesayunosNal++; }
                                                    //        //}
                                                    //        ////Octava condición
                                                    //        //else if ((fHoraInicio < fInicioDesayuno && (fHoraFinal > fInicioDesayuno && fHoraFinal >= fFinDesayuno)) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                                                    //        //{
                                                    //        //    if (bEsInterFinal) { oCant.iCantDesayunosInt++; oComDia1.iDesayunosInt++; }
                                                    //        //    else { oCant.iCantDesayunos++; oComDia1.iDesayunosNal++; }
                                                    //        //}
                                                    //        //else
                                                    //        //{
                                                    //        //    if (l == 0 && k == 0)
                                                    //        //    {
                                                    //        //        if (bEsInterFinal)
                                                    //        //        {
                                                    //        //            oCant.iCantCenasInt++;
                                                    //        //            oComDia1.iCenaInt++;
                                                    //        //        }
                                                    //        //        else
                                                    //        //        {
                                                    //        //            oCant.iCantCenas++;
                                                    //        //            oComDia1.iCenaNal++;
                                                    //        //        }
                                                    //        //    }
                                                    //        //}
                                                    //        #endregion

                                                    //        #region CODIGO ANTERIOR
                                                    //        //// SI - CENA
                                                    //        //if ((fHoraInicio < fInicioCena && fHoraFinal >= fInicioCena) || (fHoraInicio <= fFinCena && fHoraFinal > fInicioCena))
                                                    //        //{
                                                    //        //    if (bEsInterFinal)
                                                    //        //    {
                                                    //        //        if (l > 0 && k == 0)
                                                    //        //        {
                                                    //        //            if (!bEsInterInicio)
                                                    //        //            {
                                                    //        //                oCant.iCantDesayunos++;
                                                    //        //                oCant.iCantComidas++;

                                                    //        //                oComDia1.iDesayunosNal++;
                                                    //        //                oComDia1.iComidaNal++;
                                                    //        //            }
                                                    //        //            else
                                                    //        //            {
                                                    //        //                oCant.iCantDesayunosInt++;
                                                    //        //                oCant.iCantComidasInt++;

                                                    //        //                oComDia1.iDesayunosInt++;
                                                    //        //                oComDia1.iComidaInt++;
                                                    //        //            }
                                                    //        //        }


                                                    //        //        oCant.iCantCenasInt++;
                                                    //        //        oComDia1.iCenaInt++;
                                                    //        //    }
                                                    //        //    else
                                                    //        //    {
                                                    //        //        if (l > 0 && k == 0)
                                                    //        //        {
                                                    //        //            if (bEsInterInicio)
                                                    //        //            {
                                                    //        //                oCant.iCantDesayunosInt++;
                                                    //        //                oCant.iCantComidasInt++;

                                                    //        //                oComDia1.iDesayunosInt++;
                                                    //        //                oComDia1.iComidaInt++;
                                                    //        //            }
                                                    //        //            else
                                                    //        //            {
                                                    //        //                oCant.iCantDesayunos++;
                                                    //        //                oCant.iCantComidas++;

                                                    //        //                oComDia1.iDesayunosNal++;
                                                    //        //                oComDia1.iComidaNal++;
                                                    //        //            }
                                                    //        //        }

                                                    //        //        oCant.iCantCenas++;
                                                    //        //        oComDia1.iCenaNal++;
                                                    //        //    }
                                                    //        //}
                                                    //        //// SI - DESAYUNO / COMIDA
                                                    //        //else if ((fHoraInicio <= fInicioComida && (fHoraFinal > fInicioComida)) || (fHoraInicio > fInicioComida && fHoraInicio <= fFinComida) || (fHoraInicio < fInicioComida && fHoraFinal > fFinComida))
                                                    //        //{
                                                    //        //    if (bEsInterFinal)
                                                    //        //    {
                                                    //        //        if (l > 0 && k == 0)
                                                    //        //        {
                                                    //        //            if (!bEsInterInicio)
                                                    //        //            {
                                                    //        //                oCant.iCantDesayunos++;
                                                    //        //                oComDia1.iDesayunosNal++;
                                                    //        //            }
                                                    //        //            else
                                                    //        //            {
                                                    //        //                oCant.iCantDesayunosInt++;
                                                    //        //                oComDia1.iDesayunosInt++;
                                                    //        //            }
                                                    //        //        }

                                                    //        //        oCant.iCantComidasInt++;
                                                    //        //        oComDia1.iComidaInt++;
                                                    //        //    }
                                                    //        //    else
                                                    //        //    {
                                                    //        //        if (l > 0 && k == 0)
                                                    //        //        {
                                                    //        //            if (bEsInterInicio)
                                                    //        //            {
                                                    //        //                oCant.iCantDesayunosInt++;
                                                    //        //                oComDia1.iDesayunosInt++;
                                                    //        //            }
                                                    //        //            else
                                                    //        //            {
                                                    //        //                oCant.iCantDesayunos++;
                                                    //        //                oComDia1.iDesayunosNal++;
                                                    //        //            }
                                                    //        //        }

                                                    //        //        oCant.iCantComidas++;
                                                    //        //        oComDia1.iComidaNal++;
                                                    //        //    }

                                                    //        //    if (l == 0 && k == 0)
                                                    //        //    {
                                                    //        //        if (bEsInterFinal)
                                                    //        //        {
                                                    //        //            oCant.iCantCenasInt++;
                                                    //        //            oComDia1.iCenaInt++;
                                                    //        //        }
                                                    //        //        else
                                                    //        //        {
                                                    //        //            oCant.iCantCenas++;
                                                    //        //            oComDia1.iCenaNal++;
                                                    //        //        }
                                                    //        //    }
                                                    //        //}
                                                    //        //// SI - DESAYUNO / COMIDA / CENA
                                                    //        //else if (fHoraInicio < fInicioDesayuno || (fHoraInicio > fInicioDesayuno && fHoraInicio < fFinDesayuno))
                                                    //        //{
                                                    //        //    if (bEsInterFinal)
                                                    //        //    {
                                                    //        //        oCant.iCantDesayunosInt++;
                                                    //        //        oComDia1.iDesayunosInt++;
                                                    //        //    }
                                                    //        //    else
                                                    //        //    {
                                                    //        //        oCant.iCantDesayunos++;
                                                    //        //        oComDia1.iDesayunosNal++;
                                                    //        //    }
                                                    //        //}
                                                    //        //else
                                                    //        //{
                                                    //        //    if (l == 0 && k == 0)
                                                    //        //    {
                                                    //        //        if (bEsInterFinal)
                                                    //        //        {
                                                    //        //            oCant.iCantCenasInt++;
                                                    //        //            oComDia1.iCenaInt++;
                                                    //        //        }
                                                    //        //        else
                                                    //        //        {
                                                    //        //            oCant.iCantCenas++;
                                                    //        //            oComDia1.iCenaNal++;
                                                    //        //        }
                                                    //        //    }
                                                    //        //}
                                                    //        #endregion

                                                    //        oLsComDia.Add(oComDia1);
                                                    //    }
                                                    //}
                                                    //else
                                                    //{
                                                    //    // PRIMERA PIERNA Y ULTIMA DEL MISMO PAIS
                                                    //    for (int k = 0; k < rowsD.Length; k++)
                                                    //    {
                                                    //        ComidasPorDia oComDia1 = new ComidasPorDia();
                                                    //        oComDia1.sClavePiloto = drP["ClavePiloto"].S();
                                                    //        oComDia1.dtFechaDia = rowsD[k][FechaInicio].S().Dt();
                                                    //        oComDia1.dtFechaFin = rowsD[k][FechaFin].S().Dt();
                                                    //        oComDia1.sduty_type = rowsD[k]["DutyType"].S();
                                                    //        oComDia1.sOrigen = rowsD[k]["POD"].S();
                                                    //        oComDia1.sDestino = rowsD[k]["POA"].S();

                                                    //        if (k + 1 == rowsD.Length)
                                                    //        {
                                                    //            bEsInterInicio = rowsD[rowsD.Length - 1]["PaisPOD"].S() == "MX" ? false : true;
                                                    //            bEsInterFinal = rowsD[rowsD.Length - 1]["PaisPOA"].S() == "MX" ? false : true;

                                                    //            DateTime dtIniDia = rowsD[0][FechaInicio].S().Dt();
                                                    //            DateTime dtFinDia = rowsD[rowsD.Length - 1][FechaFin].S().Dt();

                                                    //            fHoraInicio = dtIniDia.Hour + (dtIniDia.Minute / float.Parse("60"));
                                                    //            fHoraFinal = dtFinDia.Hour + (dtFinDia.Minute / float.Parse("60"));

                                                    //            if (l > 0)
                                                    //                fHoraInicio = 0;

                                                    //            #region DESAYUNO
                                                    //            //Primera condicion
                                                    //            if (((fHoraInicio < fInicioDesayuno && fHoraFinal > fInicioDesayuno) && fHoraFinal < fFinDesayuno) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                                                    //            {
                                                    //                if (bEsInterFinal) { oCant.iCantDesayunosInt++; oComDia1.iDesayunosInt++; }
                                                    //                else { oCant.iCantDesayunos++; oComDia1.iDesayunosNal++; }
                                                    //            }
                                                    //            //Segunda condición
                                                    //            else if ((fHoraInicio > fInicioDesayuno && fHoraInicio < fFinDesayuno) && (fHoraFinal > fFinDesayuno && fHoraFinal > fInicioDesayuno) && (oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioComida)) //Evaluar antes de comida
                                                    //            {
                                                    //                if (bEsInterFinal) { oCant.iCantDesayunosInt++; oComDia1.iDesayunosInt++; }
                                                    //                else { oCant.iCantDesayunos++; oComDia1.iDesayunosNal++; }
                                                    //            }
                                                    //            //Tercera condición
                                                    //            else if ((fHoraInicio >= fInicioDesayuno && fHoraFinal > fFinDesayuno) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                                                    //            {
                                                    //                if (bEsInterFinal) { oCant.iCantDesayunosInt++; oComDia1.iDesayunosInt++; }
                                                    //                else { oCant.iCantDesayunos++; oComDia1.iDesayunosNal++; }
                                                    //            }
                                                    //            //Cuarta condición
                                                    //            else if ((fHoraInicio < fInicioDesayuno && fHoraFinal <= fInicioDesayuno) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                                                    //            {
                                                    //                if (bEsInterFinal) { oCant.iCantDesayunosInt++; oComDia1.iDesayunosInt++; }
                                                    //                else { oCant.iCantDesayunos++; oComDia1.iDesayunosNal++; }
                                                    //            }
                                                    //            //Quinta condición
                                                    //            else if ((fHoraInicio < fInicioDesayuno && fHoraFinal < fInicioDesayuno) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                                                    //            {
                                                    //                if (bEsInterFinal) { oCant.iCantDesayunosInt++; oComDia1.iDesayunosInt++; }
                                                    //                else { oCant.iCantDesayunos++; oComDia1.iDesayunosNal++; }
                                                    //            }
                                                    //            //Sexta condición
                                                    //            else if ((fHoraInicio < fInicioDesayuno && (fHoraFinal > fInicioDesayuno && fHoraFinal <= fFinDesayuno)) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                                                    //            {
                                                    //                if (bEsInterFinal) { oCant.iCantDesayunosInt++; oComDia1.iDesayunosInt++; }
                                                    //                else { oCant.iCantDesayunos++; oComDia1.iDesayunosNal++; }
                                                    //            }
                                                    //            //Septima condición
                                                    //            else if ((fHoraInicio >= fInicioDesayuno && fHoraInicio <= fFinDesayuno) && (fHoraFinal > fInicioDesayuno && fHoraFinal <= fFinDesayuno) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                                                    //            {
                                                    //                if (bEsInterFinal) { oCant.iCantDesayunosInt++; oComDia1.iDesayunosInt++; }
                                                    //                else { oCant.iCantDesayunos++; oComDia1.iDesayunosNal++; }
                                                    //            }
                                                    //            //Octava condición
                                                    //            else if ((fHoraInicio < fInicioDesayuno && (fHoraFinal > fInicioDesayuno && fHoraFinal >= fFinDesayuno)) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                                                    //            {
                                                    //                if (bEsInterFinal) { oCant.iCantDesayunosInt++; oComDia1.iDesayunosInt++; }
                                                    //                else { oCant.iCantDesayunos++; oComDia1.iDesayunosNal++; }
                                                    //            }
                                                    //            //else
                                                    //            //{
                                                    //            //    if (l == 0)
                                                    //            //    {
                                                    //            //        if (bEsInterFinal)
                                                    //            //        {
                                                    //            //            oCant.iCantCenasInt++;
                                                    //            //            oComDia1.iCenaInt++;
                                                    //            //        }
                                                    //            //        else
                                                    //            //        {
                                                    //            //            oCant.iCantCenas++;
                                                    //            //            oComDia1.iCenaNal++;
                                                    //            //        }
                                                    //            //    }
                                                    //            //}
                                                    //            #endregion

                                                    //            #region COMIDAS
                                                    //            ////Primera condicion Comidas
                                                    //            //if (((fHoraInicio < fInicioComida && fHoraFinal > fInicioComida) && fHoraFinal < fFinComida) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                                                    //            //{
                                                    //            //    if (bEsInterFinal)
                                                    //            //    {
                                                    //            //        if (l > 0)
                                                    //            //        {
                                                    //            //            if (!bEsInterInicio)
                                                    //            //            {
                                                    //            //                oCant.iCantDesayunos++;
                                                    //            //                oComDia1.iDesayunosNal++;
                                                    //            //            }
                                                    //            //            else
                                                    //            //            {
                                                    //            //                oCant.iCantDesayunosInt++;
                                                    //            //                oComDia1.iDesayunosInt++;
                                                    //            //            }
                                                    //            //        }

                                                    //            //        oCant.iCantComidasInt++;
                                                    //            //        oComDia1.iComidaInt++;
                                                    //            //    }
                                                    //            //    else
                                                    //            //    {
                                                    //            //        if (l > 0)
                                                    //            //        {
                                                    //            //            if (bEsInterInicio)
                                                    //            //            {
                                                    //            //                oCant.iCantDesayunosInt++;
                                                    //            //                oComDia1.iDesayunosInt++;
                                                    //            //            }
                                                    //            //            else
                                                    //            //            {
                                                    //            //                oCant.iCantDesayunos++;
                                                    //            //                oComDia1.iDesayunosNal++;
                                                    //            //            }
                                                    //            //        }

                                                    //            //        oCant.iCantComidas++;
                                                    //            //        oComDia1.iComidaNal++;
                                                    //            //    }

                                                    //            //    if (l == 0)
                                                    //            //    {
                                                    //            //        if (bEsInterFinal)
                                                    //            //        {
                                                    //            //            oCant.iCantCenasInt++;
                                                    //            //            oComDia1.iCenaInt++;
                                                    //            //        }
                                                    //            //        else
                                                    //            //        {
                                                    //            //            oCant.iCantCenas++;
                                                    //            //            oComDia1.iCenaNal++;
                                                    //            //        }
                                                    //            //    }
                                                    //            //}
                                                    //            ////Segunda condición Comidas
                                                    //            //else if ((fHoraInicio > fInicioComida && fHoraInicio < fFinComida) && (fHoraFinal > fFinComida && fHoraFinal > fInicioComida) && (oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioCena)) //Evaluar antes de cena
                                                    //            //{
                                                    //            //    if (bEsInterFinal)
                                                    //            //    {
                                                    //            //        if (l > 0)
                                                    //            //        {
                                                    //            //            if (!bEsInterInicio)
                                                    //            //            {
                                                    //            //                oCant.iCantDesayunos++;
                                                    //            //                oComDia1.iDesayunosNal++;
                                                    //            //            }
                                                    //            //            else
                                                    //            //            {
                                                    //            //                oCant.iCantDesayunosInt++;
                                                    //            //                oComDia1.iDesayunosInt++;
                                                    //            //            }
                                                    //            //        }

                                                    //            //        oCant.iCantComidasInt++;
                                                    //            //        oComDia1.iComidaInt++;
                                                    //            //    }
                                                    //            //    else
                                                    //            //    {
                                                    //            //        if (l > 0)
                                                    //            //        {
                                                    //            //            if (bEsInterInicio)
                                                    //            //            {
                                                    //            //                oCant.iCantDesayunosInt++;
                                                    //            //                oComDia1.iDesayunosInt++;
                                                    //            //            }
                                                    //            //            else
                                                    //            //            {
                                                    //            //                oCant.iCantDesayunos++;
                                                    //            //                oComDia1.iDesayunosNal++;
                                                    //            //            }
                                                    //            //        }

                                                    //            //        oCant.iCantComidas++;
                                                    //            //        oComDia1.iComidaNal++;
                                                    //            //    }

                                                    //            //    if (l == 0)
                                                    //            //    {
                                                    //            //        if (bEsInterFinal)
                                                    //            //        {
                                                    //            //            oCant.iCantCenasInt++;
                                                    //            //            oComDia1.iCenaInt++;
                                                    //            //        }
                                                    //            //        else
                                                    //            //        {
                                                    //            //            oCant.iCantCenas++;
                                                    //            //            oComDia1.iCenaNal++;
                                                    //            //        }
                                                    //            //    }
                                                    //            //}
                                                    //            ////Tercera condición Comidas
                                                    //            //else if ((fHoraInicio >= fInicioComida && fHoraFinal > fFinComida) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                                                    //            //{
                                                    //            //    if (bEsInterFinal)
                                                    //            //    {
                                                    //            //        if (l > 0)
                                                    //            //        {
                                                    //            //            if (!bEsInterInicio)
                                                    //            //            {
                                                    //            //                oCant.iCantDesayunos++;
                                                    //            //                oComDia1.iDesayunosNal++;
                                                    //            //            }
                                                    //            //            else
                                                    //            //            {
                                                    //            //                oCant.iCantDesayunosInt++;
                                                    //            //                oComDia1.iDesayunosInt++;
                                                    //            //            }
                                                    //            //        }

                                                    //            //        oCant.iCantComidasInt++;
                                                    //            //        oComDia1.iComidaInt++;
                                                    //            //    }
                                                    //            //    else
                                                    //            //    {
                                                    //            //        if (l > 0)
                                                    //            //        {
                                                    //            //            if (bEsInterInicio)
                                                    //            //            {
                                                    //            //                oCant.iCantDesayunosInt++;
                                                    //            //                oComDia1.iDesayunosInt++;
                                                    //            //            }
                                                    //            //            else
                                                    //            //            {
                                                    //            //                oCant.iCantDesayunos++;
                                                    //            //                oComDia1.iDesayunosNal++;
                                                    //            //            }
                                                    //            //        }

                                                    //            //        oCant.iCantComidas++;
                                                    //            //        oComDia1.iComidaNal++;
                                                    //            //    }

                                                    //            //    if (l == 0)
                                                    //            //    {
                                                    //            //        if (bEsInterFinal)
                                                    //            //        {
                                                    //            //            oCant.iCantCenasInt++;
                                                    //            //            oComDia1.iCenaInt++;
                                                    //            //        }
                                                    //            //        else
                                                    //            //        {
                                                    //            //            oCant.iCantCenas++;
                                                    //            //            oComDia1.iCenaNal++;
                                                    //            //        }
                                                    //            //    }
                                                    //            //}
                                                    //            ////Cuarta condición Comidas
                                                    //            //else if ((fHoraInicio < fInicioComida && fHoraFinal <= fInicioComida) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                                                    //            //{
                                                    //            //    if (bEsInterFinal)
                                                    //            //    {
                                                    //            //        if (l > 0)
                                                    //            //        {
                                                    //            //            if (!bEsInterInicio)
                                                    //            //            {
                                                    //            //                oCant.iCantDesayunos++;
                                                    //            //                oComDia1.iDesayunosNal++;
                                                    //            //            }
                                                    //            //            else
                                                    //            //            {
                                                    //            //                oCant.iCantDesayunosInt++;
                                                    //            //                oComDia1.iDesayunosInt++;
                                                    //            //            }
                                                    //            //        }

                                                    //            //        oCant.iCantComidasInt++;
                                                    //            //        oComDia1.iComidaInt++;
                                                    //            //    }
                                                    //            //    else
                                                    //            //    {
                                                    //            //        if (l > 0)
                                                    //            //        {
                                                    //            //            if (bEsInterInicio)
                                                    //            //            {
                                                    //            //                oCant.iCantDesayunosInt++;
                                                    //            //                oComDia1.iDesayunosInt++;
                                                    //            //            }
                                                    //            //            else
                                                    //            //            {
                                                    //            //                oCant.iCantDesayunos++;
                                                    //            //                oComDia1.iDesayunosNal++;
                                                    //            //            }
                                                    //            //        }

                                                    //            //        oCant.iCantComidas++;
                                                    //            //        oComDia1.iComidaNal++;
                                                    //            //    }

                                                    //            //    if (l == 0)
                                                    //            //    {
                                                    //            //        if (bEsInterFinal)
                                                    //            //        {
                                                    //            //            oCant.iCantCenasInt++;
                                                    //            //            oComDia1.iCenaInt++;
                                                    //            //        }
                                                    //            //        else
                                                    //            //        {
                                                    //            //            oCant.iCantCenas++;
                                                    //            //            oComDia1.iCenaNal++;
                                                    //            //        }
                                                    //            //    }
                                                    //            //}
                                                    //            ////Quinta condición Comidas
                                                    //            //else if ((fHoraInicio < fInicioComida && fHoraFinal < fInicioComida) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                                                    //            //{
                                                    //            //    if (bEsInterFinal)
                                                    //            //    {
                                                    //            //        if (l > 0)
                                                    //            //        {
                                                    //            //            if (!bEsInterInicio)
                                                    //            //            {
                                                    //            //                oCant.iCantDesayunos++;
                                                    //            //                oComDia1.iDesayunosNal++;
                                                    //            //            }
                                                    //            //            else
                                                    //            //            {
                                                    //            //                oCant.iCantDesayunosInt++;
                                                    //            //                oComDia1.iDesayunosInt++;
                                                    //            //            }
                                                    //            //        }

                                                    //            //        oCant.iCantComidasInt++;
                                                    //            //        oComDia1.iComidaInt++;
                                                    //            //    }
                                                    //            //    else
                                                    //            //    {
                                                    //            //        if (l > 0)
                                                    //            //        {
                                                    //            //            if (bEsInterInicio)
                                                    //            //            {
                                                    //            //                oCant.iCantDesayunosInt++;
                                                    //            //                oComDia1.iDesayunosInt++;
                                                    //            //            }
                                                    //            //            else
                                                    //            //            {
                                                    //            //                oCant.iCantDesayunos++;
                                                    //            //                oComDia1.iDesayunosNal++;
                                                    //            //            }
                                                    //            //        }

                                                    //            //        oCant.iCantComidas++;
                                                    //            //        oComDia1.iComidaNal++;
                                                    //            //    }

                                                    //            //    if (l == 0)
                                                    //            //    {
                                                    //            //        if (bEsInterFinal)
                                                    //            //        {
                                                    //            //            oCant.iCantCenasInt++;
                                                    //            //            oComDia1.iCenaInt++;
                                                    //            //        }
                                                    //            //        else
                                                    //            //        {
                                                    //            //            oCant.iCantCenas++;
                                                    //            //            oComDia1.iCenaNal++;
                                                    //            //        }
                                                    //            //    }
                                                    //            //}
                                                    //            ////Sexta condición Comidas
                                                    //            //else if ((fHoraInicio < fInicioComida && (fHoraFinal > fInicioComida && fHoraFinal <= fFinComida)) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                                                    //            //{
                                                    //            //    if (bEsInterFinal)
                                                    //            //    {
                                                    //            //        if (l > 0)
                                                    //            //        {
                                                    //            //            if (!bEsInterInicio)
                                                    //            //            {
                                                    //            //                oCant.iCantDesayunos++;
                                                    //            //                oComDia1.iDesayunosNal++;
                                                    //            //            }
                                                    //            //            else
                                                    //            //            {
                                                    //            //                oCant.iCantDesayunosInt++;
                                                    //            //                oComDia1.iDesayunosInt++;
                                                    //            //            }
                                                    //            //        }

                                                    //            //        oCant.iCantComidasInt++;
                                                    //            //        oComDia1.iComidaInt++;
                                                    //            //    }
                                                    //            //    else
                                                    //            //    {
                                                    //            //        if (l > 0)
                                                    //            //        {
                                                    //            //            if (bEsInterInicio)
                                                    //            //            {
                                                    //            //                oCant.iCantDesayunosInt++;
                                                    //            //                oComDia1.iDesayunosInt++;
                                                    //            //            }
                                                    //            //            else
                                                    //            //            {
                                                    //            //                oCant.iCantDesayunos++;
                                                    //            //                oComDia1.iDesayunosNal++;
                                                    //            //            }
                                                    //            //        }

                                                    //            //        oCant.iCantComidas++;
                                                    //            //        oComDia1.iComidaNal++;
                                                    //            //    }

                                                    //            //    if (l == 0)
                                                    //            //    {
                                                    //            //        if (bEsInterFinal)
                                                    //            //        {
                                                    //            //            oCant.iCantCenasInt++;
                                                    //            //            oComDia1.iCenaInt++;
                                                    //            //        }
                                                    //            //        else
                                                    //            //        {
                                                    //            //            oCant.iCantCenas++;
                                                    //            //            oComDia1.iCenaNal++;
                                                    //            //        }
                                                    //            //    }
                                                    //            //}
                                                    //            ////Septima condición Comidas
                                                    //            //else if ((fHoraInicio >= fInicioComida && fHoraInicio <= fFinComida) && (fHoraFinal > fInicioComida && fHoraFinal <= fFinComida) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                                                    //            //{
                                                    //            //    if (bEsInterFinal)
                                                    //            //    {
                                                    //            //        if (l > 0)
                                                    //            //        {
                                                    //            //            if (!bEsInterInicio)
                                                    //            //            {
                                                    //            //                oCant.iCantDesayunos++;
                                                    //            //                oComDia1.iDesayunosNal++;
                                                    //            //            }
                                                    //            //            else
                                                    //            //            {
                                                    //            //                oCant.iCantDesayunosInt++;
                                                    //            //                oComDia1.iDesayunosInt++;
                                                    //            //            }
                                                    //            //        }

                                                    //            //        oCant.iCantComidasInt++;
                                                    //            //        oComDia1.iComidaInt++;
                                                    //            //    }
                                                    //            //    else
                                                    //            //    {
                                                    //            //        if (l > 0)
                                                    //            //        {
                                                    //            //            if (bEsInterInicio)
                                                    //            //            {
                                                    //            //                oCant.iCantDesayunosInt++;
                                                    //            //                oComDia1.iDesayunosInt++;
                                                    //            //            }
                                                    //            //            else
                                                    //            //            {
                                                    //            //                oCant.iCantDesayunos++;
                                                    //            //                oComDia1.iDesayunosNal++;
                                                    //            //            }
                                                    //            //        }

                                                    //            //        oCant.iCantComidas++;
                                                    //            //        oComDia1.iComidaNal++;
                                                    //            //    }

                                                    //            //    if (l == 0)
                                                    //            //    {
                                                    //            //        if (bEsInterFinal)
                                                    //            //        {
                                                    //            //            oCant.iCantCenasInt++;
                                                    //            //            oComDia1.iCenaInt++;
                                                    //            //        }
                                                    //            //        else
                                                    //            //        {
                                                    //            //            oCant.iCantCenas++;
                                                    //            //            oComDia1.iCenaNal++;
                                                    //            //        }
                                                    //            //    }
                                                    //            //}
                                                    //            ////Octava condición
                                                    //            //else if ((fHoraInicio < fInicioComida && (fHoraFinal > fInicioComida && fHoraFinal >= fFinComida)) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                                                    //            //{
                                                    //            //    if (bEsInterFinal)
                                                    //            //    {
                                                    //            //        if (l > 0)
                                                    //            //        {
                                                    //            //            if (!bEsInterInicio)
                                                    //            //            {
                                                    //            //                oCant.iCantDesayunos++;
                                                    //            //                oComDia1.iDesayunosNal++;
                                                    //            //            }
                                                    //            //            else
                                                    //            //            {
                                                    //            //                oCant.iCantDesayunosInt++;
                                                    //            //                oComDia1.iDesayunosInt++;
                                                    //            //            }
                                                    //            //        }

                                                    //            //        oCant.iCantComidasInt++;
                                                    //            //        oComDia1.iComidaInt++;
                                                    //            //    }
                                                    //            //    else
                                                    //            //    {
                                                    //            //        if (l > 0)
                                                    //            //        {
                                                    //            //            if (bEsInterInicio)
                                                    //            //            {
                                                    //            //                oCant.iCantDesayunosInt++;
                                                    //            //                oComDia1.iDesayunosInt++;
                                                    //            //            }
                                                    //            //            else
                                                    //            //            {
                                                    //            //                oCant.iCantDesayunos++;
                                                    //            //                oComDia1.iDesayunosNal++;
                                                    //            //            }
                                                    //            //        }

                                                    //            //        oCant.iCantComidas++;
                                                    //            //        oComDia1.iComidaNal++;
                                                    //            //    }

                                                    //            //    if (l == 0)
                                                    //            //    {
                                                    //            //        if (bEsInterFinal)
                                                    //            //        {
                                                    //            //            oCant.iCantCenasInt++;
                                                    //            //            oComDia1.iCenaInt++;
                                                    //            //        }
                                                    //            //        else
                                                    //            //        {
                                                    //            //            oCant.iCantCenas++;
                                                    //            //            oComDia1.iCenaNal++;
                                                    //            //        }
                                                    //            //    }
                                                    //            //}
                                                    //            #endregion

                                                    //            #region CENA
                                                    //            ////Primera condicion Cenas
                                                    //            //if (((fHoraInicio < fInicioCena && fHoraFinal > fInicioCena) && fHoraFinal < fFinCena) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                                                    //            //{
                                                    //            //    if (bEsInterFinal)
                                                    //            //    {
                                                    //            //        if (l > 0)
                                                    //            //        {
                                                    //            //            if (!bEsInterInicio)
                                                    //            //            {
                                                    //            //                oCant.iCantDesayunos++;
                                                    //            //                oCant.iCantComidas++;

                                                    //            //                oComDia1.iDesayunosNal++;
                                                    //            //                oComDia1.iComidaNal++;
                                                    //            //            }
                                                    //            //            else
                                                    //            //            {
                                                    //            //                oCant.iCantDesayunosInt++;
                                                    //            //                oCant.iCantComidasInt++;

                                                    //            //                oComDia1.iDesayunosInt++;
                                                    //            //                oComDia1.iComidaInt++;
                                                    //            //            }
                                                    //            //        }

                                                    //            //        oCant.iCantCenasInt++;
                                                    //            //        oComDia1.iCenaInt++;
                                                    //            //    }
                                                    //            //    else
                                                    //            //    {
                                                    //            //        if (l > 0)
                                                    //            //        {
                                                    //            //            if (bEsInterInicio)
                                                    //            //            {
                                                    //            //                oCant.iCantDesayunosInt++;
                                                    //            //                oCant.iCantComidasInt++;

                                                    //            //                oComDia1.iDesayunosInt++;
                                                    //            //                oComDia1.iComidaInt++;
                                                    //            //            }
                                                    //            //            else
                                                    //            //            {
                                                    //            //                oCant.iCantDesayunos++;
                                                    //            //                oCant.iCantComidas++;

                                                    //            //                oComDia1.iDesayunosNal++;
                                                    //            //                oComDia1.iComidaNal++;
                                                    //            //            }
                                                    //            //        }

                                                    //            //        oCant.iCantCenas++;
                                                    //            //        oComDia1.iCenaNal++;
                                                    //            //    }
                                                    //            //}
                                                    //            ////Segunda condición Cenas
                                                    //            //else if ((fHoraInicio > fInicioCena && fHoraInicio < fFinCena) && (fHoraFinal > fFinCena && fHoraFinal > fInicioCena) && (oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                                                    //            //{
                                                    //            //    if (bEsInterFinal)
                                                    //            //    {
                                                    //            //        if (l > 0)
                                                    //            //        {
                                                    //            //            if (!bEsInterInicio)
                                                    //            //            {
                                                    //            //                oCant.iCantDesayunos++;
                                                    //            //                oCant.iCantComidas++;

                                                    //            //                oComDia1.iDesayunosNal++;
                                                    //            //                oComDia1.iComidaNal++;
                                                    //            //            }
                                                    //            //            else
                                                    //            //            {
                                                    //            //                oCant.iCantDesayunosInt++;
                                                    //            //                oCant.iCantComidasInt++;

                                                    //            //                oComDia1.iDesayunosInt++;
                                                    //            //                oComDia1.iComidaInt++;
                                                    //            //            }
                                                    //            //        }

                                                    //            //        oCant.iCantCenasInt++;
                                                    //            //        oComDia1.iCenaInt++;
                                                    //            //    }
                                                    //            //    else
                                                    //            //    {
                                                    //            //        if (l > 0)
                                                    //            //        {
                                                    //            //            if (bEsInterInicio)
                                                    //            //            {
                                                    //            //                oCant.iCantDesayunosInt++;
                                                    //            //                oCant.iCantComidasInt++;

                                                    //            //                oComDia1.iDesayunosInt++;
                                                    //            //                oComDia1.iComidaInt++;
                                                    //            //            }
                                                    //            //            else
                                                    //            //            {
                                                    //            //                oCant.iCantDesayunos++;
                                                    //            //                oCant.iCantComidas++;

                                                    //            //                oComDia1.iDesayunosNal++;
                                                    //            //                oComDia1.iComidaNal++;
                                                    //            //            }
                                                    //            //        }

                                                    //            //        oCant.iCantCenas++;
                                                    //            //        oComDia1.iCenaNal++;
                                                    //            //    }
                                                    //            //}
                                                    //            ////Tercera condición Cenas
                                                    //            //else if ((fHoraInicio >= fInicioCena && fHoraFinal > fFinCena) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                                                    //            //{
                                                    //            //    if (bEsInterFinal)
                                                    //            //    {
                                                    //            //        if (l > 0)
                                                    //            //        {
                                                    //            //            if (!bEsInterInicio)
                                                    //            //            {
                                                    //            //                oCant.iCantDesayunos++;
                                                    //            //                oCant.iCantComidas++;

                                                    //            //                oComDia1.iDesayunosNal++;
                                                    //            //                oComDia1.iComidaNal++;
                                                    //            //            }
                                                    //            //            else
                                                    //            //            {
                                                    //            //                oCant.iCantDesayunosInt++;
                                                    //            //                oCant.iCantComidasInt++;

                                                    //            //                oComDia1.iDesayunosInt++;
                                                    //            //                oComDia1.iComidaInt++;
                                                    //            //            }
                                                    //            //        }

                                                    //            //        oCant.iCantCenasInt++;
                                                    //            //        oComDia1.iCenaInt++;
                                                    //            //    }
                                                    //            //    else
                                                    //            //    {
                                                    //            //        if (l > 0)
                                                    //            //        {
                                                    //            //            if (bEsInterInicio)
                                                    //            //            {
                                                    //            //                oCant.iCantDesayunosInt++;
                                                    //            //                oCant.iCantComidasInt++;

                                                    //            //                oComDia1.iDesayunosInt++;
                                                    //            //                oComDia1.iComidaInt++;
                                                    //            //            }
                                                    //            //            else
                                                    //            //            {
                                                    //            //                oCant.iCantDesayunos++;
                                                    //            //                oCant.iCantComidas++;

                                                    //            //                oComDia1.iDesayunosNal++;
                                                    //            //                oComDia1.iComidaNal++;
                                                    //            //            }
                                                    //            //        }

                                                    //            //        oCant.iCantCenas++;
                                                    //            //        oComDia1.iCenaNal++;
                                                    //            //    }
                                                    //            //}
                                                    //            ////Cuarta condición Cenas
                                                    //            //else if ((fHoraInicio < fInicioCena && fHoraFinal <= fInicioCena) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                                                    //            //{
                                                    //            //    if (bEsInterFinal)
                                                    //            //    {
                                                    //            //        if (l > 0)
                                                    //            //        {
                                                    //            //            if (!bEsInterInicio)
                                                    //            //            {
                                                    //            //                oCant.iCantDesayunos++;
                                                    //            //                oCant.iCantComidas++;

                                                    //            //                oComDia1.iDesayunosNal++;
                                                    //            //                oComDia1.iComidaNal++;
                                                    //            //            }
                                                    //            //            else
                                                    //            //            {
                                                    //            //                oCant.iCantDesayunosInt++;
                                                    //            //                oCant.iCantComidasInt++;

                                                    //            //                oComDia1.iDesayunosInt++;
                                                    //            //                oComDia1.iComidaInt++;
                                                    //            //            }
                                                    //            //        }

                                                    //            //        oCant.iCantCenasInt++;
                                                    //            //        oComDia1.iCenaInt++;
                                                    //            //    }
                                                    //            //    else
                                                    //            //    {
                                                    //            //        if (l > 0)
                                                    //            //        {
                                                    //            //            if (bEsInterInicio)
                                                    //            //            {
                                                    //            //                oCant.iCantDesayunosInt++;
                                                    //            //                oCant.iCantComidasInt++;

                                                    //            //                oComDia1.iDesayunosInt++;
                                                    //            //                oComDia1.iComidaInt++;
                                                    //            //            }
                                                    //            //            else
                                                    //            //            {
                                                    //            //                oCant.iCantDesayunos++;
                                                    //            //                oCant.iCantComidas++;

                                                    //            //                oComDia1.iDesayunosNal++;
                                                    //            //                oComDia1.iComidaNal++;
                                                    //            //            }
                                                    //            //        }

                                                    //            //        oCant.iCantCenas++;
                                                    //            //        oComDia1.iCenaNal++;
                                                    //            //    }
                                                    //            //}
                                                    //            ////Quinta condición Cenas
                                                    //            //else if ((fHoraInicio < fInicioCena && fHoraFinal < fInicioCena) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                                                    //            //{
                                                    //            //    if (bEsInterFinal)
                                                    //            //    {
                                                    //            //        if (l > 0)
                                                    //            //        {
                                                    //            //            if (!bEsInterInicio)
                                                    //            //            {
                                                    //            //                oCant.iCantDesayunos++;
                                                    //            //                oCant.iCantComidas++;

                                                    //            //                oComDia1.iDesayunosNal++;
                                                    //            //                oComDia1.iComidaNal++;
                                                    //            //            }
                                                    //            //            else
                                                    //            //            {
                                                    //            //                oCant.iCantDesayunosInt++;
                                                    //            //                oCant.iCantComidasInt++;

                                                    //            //                oComDia1.iDesayunosInt++;
                                                    //            //                oComDia1.iComidaInt++;
                                                    //            //            }
                                                    //            //        }

                                                    //            //        oCant.iCantCenasInt++;
                                                    //            //        oComDia1.iCenaInt++;
                                                    //            //    }
                                                    //            //    else
                                                    //            //    {
                                                    //            //        if (l > 0)
                                                    //            //        {
                                                    //            //            if (bEsInterInicio)
                                                    //            //            {
                                                    //            //                oCant.iCantDesayunosInt++;
                                                    //            //                oCant.iCantComidasInt++;

                                                    //            //                oComDia1.iDesayunosInt++;
                                                    //            //                oComDia1.iComidaInt++;
                                                    //            //            }
                                                    //            //            else
                                                    //            //            {
                                                    //            //                oCant.iCantDesayunos++;
                                                    //            //                oCant.iCantComidas++;

                                                    //            //                oComDia1.iDesayunosNal++;
                                                    //            //                oComDia1.iComidaNal++;
                                                    //            //            }
                                                    //            //        }

                                                    //            //        oCant.iCantCenas++;
                                                    //            //        oComDia1.iCenaNal++;
                                                    //            //    }
                                                    //            //}
                                                    //            ////Sexta condición Cenas
                                                    //            //else if ((fHoraInicio < fInicioCena && (fHoraFinal > fInicioCena && fHoraFinal <= fFinCena)) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                                                    //            //{
                                                    //            //    if (bEsInterFinal)
                                                    //            //    {
                                                    //            //        if (l > 0)
                                                    //            //        {
                                                    //            //            if (!bEsInterInicio)
                                                    //            //            {
                                                    //            //                oCant.iCantDesayunos++;
                                                    //            //                oCant.iCantComidas++;

                                                    //            //                oComDia1.iDesayunosNal++;
                                                    //            //                oComDia1.iComidaNal++;
                                                    //            //            }
                                                    //            //            else
                                                    //            //            {
                                                    //            //                oCant.iCantDesayunosInt++;
                                                    //            //                oCant.iCantComidasInt++;

                                                    //            //                oComDia1.iDesayunosInt++;
                                                    //            //                oComDia1.iComidaInt++;
                                                    //            //            }
                                                    //            //        }

                                                    //            //        oCant.iCantCenasInt++;
                                                    //            //        oComDia1.iCenaInt++;
                                                    //            //    }
                                                    //            //    else
                                                    //            //    {
                                                    //            //        if (l > 0)
                                                    //            //        {
                                                    //            //            if (bEsInterInicio)
                                                    //            //            {
                                                    //            //                oCant.iCantDesayunosInt++;
                                                    //            //                oCant.iCantComidasInt++;

                                                    //            //                oComDia1.iDesayunosInt++;
                                                    //            //                oComDia1.iComidaInt++;
                                                    //            //            }
                                                    //            //            else
                                                    //            //            {
                                                    //            //                oCant.iCantDesayunos++;
                                                    //            //                oCant.iCantComidas++;

                                                    //            //                oComDia1.iDesayunosNal++;
                                                    //            //                oComDia1.iComidaNal++;
                                                    //            //            }
                                                    //            //        }

                                                    //            //        oCant.iCantCenas++;
                                                    //            //        oComDia1.iCenaNal++;
                                                    //            //    }
                                                    //            //}
                                                    //            ////Septima condición Cenas
                                                    //            //else if ((fHoraInicio >= fInicioCena && fHoraInicio <= fFinCena) && (fHoraFinal > fInicioCena && fHoraFinal <= fFinCena) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                                                    //            //{
                                                    //            //    if (bEsInterFinal)
                                                    //            //    {
                                                    //            //        if (l > 0)
                                                    //            //        {
                                                    //            //            if (!bEsInterInicio)
                                                    //            //            {
                                                    //            //                oCant.iCantDesayunos++;
                                                    //            //                oCant.iCantComidas++;

                                                    //            //                oComDia1.iDesayunosNal++;
                                                    //            //                oComDia1.iComidaNal++;
                                                    //            //            }
                                                    //            //            else
                                                    //            //            {
                                                    //            //                oCant.iCantDesayunosInt++;
                                                    //            //                oCant.iCantComidasInt++;

                                                    //            //                oComDia1.iDesayunosInt++;
                                                    //            //                oComDia1.iComidaInt++;
                                                    //            //            }
                                                    //            //        }

                                                    //            //        oCant.iCantCenasInt++;
                                                    //            //        oComDia1.iCenaInt++;
                                                    //            //    }
                                                    //            //    else
                                                    //            //    {
                                                    //            //        if (l > 0)
                                                    //            //        {
                                                    //            //            if (bEsInterInicio)
                                                    //            //            {
                                                    //            //                oCant.iCantDesayunosInt++;
                                                    //            //                oCant.iCantComidasInt++;

                                                    //            //                oComDia1.iDesayunosInt++;
                                                    //            //                oComDia1.iComidaInt++;
                                                    //            //            }
                                                    //            //            else
                                                    //            //            {
                                                    //            //                oCant.iCantDesayunos++;
                                                    //            //                oCant.iCantComidas++;

                                                    //            //                oComDia1.iDesayunosNal++;
                                                    //            //                oComDia1.iComidaNal++;
                                                    //            //            }
                                                    //            //        }

                                                    //            //        oCant.iCantCenas++;
                                                    //            //        oComDia1.iCenaNal++;
                                                    //            //    }
                                                    //            //}
                                                    //            ////Octava condición Cenas
                                                    //            //else if ((fHoraInicio < fInicioCena && (fHoraFinal > fInicioCena && fHoraFinal >= fFinCena)) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                                                    //            //{
                                                    //            //    if (bEsInterFinal)
                                                    //            //    {
                                                    //            //        if (l > 0)
                                                    //            //        {
                                                    //            //            if (!bEsInterInicio)
                                                    //            //            {
                                                    //            //                oCant.iCantDesayunos++;
                                                    //            //                oCant.iCantComidas++;

                                                    //            //                oComDia1.iDesayunosNal++;
                                                    //            //                oComDia1.iComidaNal++;
                                                    //            //            }
                                                    //            //            else
                                                    //            //            {
                                                    //            //                oCant.iCantDesayunosInt++;
                                                    //            //                oCant.iCantComidasInt++;

                                                    //            //                oComDia1.iDesayunosInt++;
                                                    //            //                oComDia1.iComidaInt++;
                                                    //            //            }
                                                    //            //        }

                                                    //            //        oCant.iCantCenasInt++;
                                                    //            //        oComDia1.iCenaInt++;
                                                    //            //    }
                                                    //            //    else
                                                    //            //    {
                                                    //            //        if (l > 0)
                                                    //            //        {
                                                    //            //            if (bEsInterInicio)
                                                    //            //            {
                                                    //            //                oCant.iCantDesayunosInt++;
                                                    //            //                oCant.iCantComidasInt++;

                                                    //            //                oComDia1.iDesayunosInt++;
                                                    //            //                oComDia1.iComidaInt++;
                                                    //            //            }
                                                    //            //            else
                                                    //            //            {
                                                    //            //                oCant.iCantDesayunos++;
                                                    //            //                oCant.iCantComidas++;

                                                    //            //                oComDia1.iDesayunosNal++;
                                                    //            //                oComDia1.iComidaNal++;
                                                    //            //            }
                                                    //            //        }

                                                    //            //        oCant.iCantCenas++;
                                                    //            //        oComDia1.iCenaNal++;
                                                    //            //    }
                                                    //            //}
                                                    //            #endregion

                                                    //            #region CODIGO ANTERIOR
                                                    //            //// SI - CENA
                                                    //            //if ((fHoraInicio < fInicioCena && fHoraFinal >= fInicioCena) || (fHoraInicio <= fFinCena && fHoraFinal > fInicioCena))
                                                    //            //{
                                                    //            //    if (bEsInterFinal)
                                                    //            //    {
                                                    //            //        if (l > 0)
                                                    //            //        {
                                                    //            //            if (!bEsInterInicio)
                                                    //            //            {
                                                    //            //                oCant.iCantDesayunos++;
                                                    //            //                oCant.iCantComidas++;

                                                    //            //                oComDia1.iDesayunosNal++;
                                                    //            //                oComDia1.iComidaNal++;
                                                    //            //            }
                                                    //            //            else
                                                    //            //            {
                                                    //            //                oCant.iCantDesayunosInt++;
                                                    //            //                oCant.iCantComidasInt++;

                                                    //            //                oComDia1.iDesayunosInt++;
                                                    //            //                oComDia1.iComidaInt++;
                                                    //            //            }
                                                    //            //        }

                                                    //            //        oCant.iCantCenasInt++;
                                                    //            //        oComDia1.iCenaInt++;
                                                    //            //    }
                                                    //            //    else
                                                    //            //    {
                                                    //            //        if (l > 0)
                                                    //            //        {
                                                    //            //            if (bEsInterInicio)
                                                    //            //            {
                                                    //            //                oCant.iCantDesayunosInt++;
                                                    //            //                oCant.iCantComidasInt++;

                                                    //            //                oComDia1.iDesayunosInt++;
                                                    //            //                oComDia1.iComidaInt++;
                                                    //            //            }
                                                    //            //            else
                                                    //            //            {
                                                    //            //                oCant.iCantDesayunos++;
                                                    //            //                oCant.iCantComidas++;

                                                    //            //                oComDia1.iDesayunosNal++;
                                                    //            //                oComDia1.iComidaNal++;
                                                    //            //            }
                                                    //            //        }

                                                    //            //        oCant.iCantCenas++;
                                                    //            //        oComDia1.iCenaNal++;
                                                    //            //    }
                                                    //            //}
                                                    //            //// SI - DESAYUNO / COMIDA
                                                    //            //else if ((fHoraInicio <= fInicioComida && (fHoraFinal > fInicioComida)) || (fHoraInicio > fInicioComida && fHoraInicio <= fFinComida) || (fHoraInicio < fInicioComida && fHoraFinal > fFinComida))
                                                    //            //{
                                                    //            //    if (bEsInterFinal)
                                                    //            //    {
                                                    //            //        if (l > 0)
                                                    //            //        {
                                                    //            //            if (!bEsInterInicio)
                                                    //            //            {
                                                    //            //                oCant.iCantDesayunos++;
                                                    //            //                oComDia1.iDesayunosNal++;
                                                    //            //            }
                                                    //            //            else
                                                    //            //            {
                                                    //            //                oCant.iCantDesayunosInt++;
                                                    //            //                oComDia1.iDesayunosInt++;
                                                    //            //            }
                                                    //            //        }

                                                    //            //        oCant.iCantComidasInt++;
                                                    //            //        oComDia1.iComidaInt++;
                                                    //            //    }
                                                    //            //    else
                                                    //            //    {
                                                    //            //        if (l > 0)
                                                    //            //        {
                                                    //            //            if (bEsInterInicio)
                                                    //            //            {
                                                    //            //                oCant.iCantDesayunosInt++;
                                                    //            //                oComDia1.iDesayunosInt++;
                                                    //            //            }
                                                    //            //            else
                                                    //            //            {
                                                    //            //                oCant.iCantDesayunos++;
                                                    //            //                oComDia1.iDesayunosNal++;
                                                    //            //            }
                                                    //            //        }

                                                    //            //        oCant.iCantComidas++;
                                                    //            //        oComDia1.iComidaNal++;
                                                    //            //    }

                                                    //            //    if (l == 0)
                                                    //            //    {
                                                    //            //        if (bEsInterFinal)
                                                    //            //        {
                                                    //            //            oCant.iCantCenasInt++;
                                                    //            //            oComDia1.iCenaInt++;
                                                    //            //        }
                                                    //            //        else
                                                    //            //        {
                                                    //            //            oCant.iCantCenas++;
                                                    //            //            oComDia1.iCenaNal++;
                                                    //            //        }
                                                    //            //    }
                                                    //            //}
                                                    //            //// SI - DESAYUNO / COMIDA / CENA
                                                    //            //else if (fHoraInicio < fInicioDesayuno || (fHoraInicio > fInicioDesayuno && fHoraInicio < fFinDesayuno))
                                                    //            //{
                                                    //            //    if (bEsInterFinal)
                                                    //            //    {
                                                    //            //        oCant.iCantDesayunosInt++;
                                                    //            //        oComDia1.iDesayunosInt++;
                                                    //            //    }
                                                    //            //    else
                                                    //            //    {
                                                    //            //        oCant.iCantDesayunos++;
                                                    //            //        oComDia1.iDesayunosNal++;
                                                    //            //    }
                                                    //            //}
                                                    //            //else
                                                    //            //{
                                                    //            //    if (l == 0)
                                                    //            //    {
                                                    //            //        if (bEsInterFinal)
                                                    //            //        {
                                                    //            //            oCant.iCantCenasInt++;
                                                    //            //            oComDia1.iCenaInt++;
                                                    //            //        }
                                                    //            //        else
                                                    //            //        {
                                                    //            //            oCant.iCantCenas++;
                                                    //            //            oComDia1.iCenaNal++;
                                                    //            //        }
                                                    //            //    }
                                                    //            //}
                                                    //            #endregion


                                                    //        }

                                                    //        oLsComDia.Add(oComDia1);
                                                    //    }
                                                    //}
                                                    #endregion

                                                    #region CODIGO CUANDO ES UN DIA AGREGADO TEMPORALMENTE PARA REVISAR FUNCIONAMIENTO PARA MAS DE DOS DÍAS
                                                    for (int k = 0; k < rowsD.Length; k++)
                                                    {
                                                        ComidasPorDia oComDia1 = new ComidasPorDia();
                                                        oComDia1.sClavePiloto = drP["ClavePiloto"].S();
                                                        oComDia1.dtFechaDia = rowsD[k][FechaInicio].S().Dt();
                                                        oComDia1.dtFechaFin = rowsD[k][FechaFin].S().Dt();
                                                        oComDia1.sduty_type = rowsD[k]["DutyType"].S();
                                                        oComDia1.sOrigen = rowsD[k]["POD"].S();
                                                        oComDia1.sDestino = rowsD[k]["POA"].S();

                                                        fHoraInicio = 0;
                                                        fHoraFinal = 0;

                                                        bEsInterInicio = false;
                                                        bEsInterFinal = false;

                                                        bEsInterInicio = rowsD[k]["PaisPOD"].S() == "MX" ? false : true;
                                                        bEsInterFinal = rowsD[k]["PaisPOA"].S() == "MX" ? false : true;

                                                        DateTime dtIniDia = rowsD[0][FechaInicio].S().Dt();
                                                        DateTime dtFinDia = rowsD[k][FechaFin].S().Dt();

                                                        fHoraInicio = dtIniDia.Hour + (dtIniDia.Minute / float.Parse("60"));
                                                        fHoraFinal = dtFinDia.Hour + (dtFinDia.Minute / float.Parse("60"));

                                                        if (l > 0 && k == 0)
                                                            fHoraInicio = 0;

                                                        if (k + 1 == rowsD.Length)
                                                        {

                                                            #region DESAYUNO
                                                            //if (fHoraInicio < fInicioDesayuno || (fHoraInicio > fInicioDesayuno && fHoraInicio < fFinDesayuno)) //Linea origen

                                                            //Primera condicion
                                                            if (((fHoraInicio < fInicioDesayuno && fHoraFinal > fInicioDesayuno) && fHoraFinal < fFinDesayuno) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                                                            {
                                                                if (bEsInterFinal) { oCant.iCantDesayunosInt++; oComDia1.iDesayunosInt++; } else { oCant.iCantDesayunos++; oComDia1.iDesayunosNal++; }
                                                            }
                                                            //Segunda condición
                                                            else if ((fHoraInicio > fInicioDesayuno && fHoraInicio < fFinDesayuno) && (fHoraFinal > fFinDesayuno && fHoraFinal > fInicioDesayuno) && (oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioComida)) //Evaluar antes de comida
                                                            {
                                                                if (bEsInterFinal) { oCant.iCantDesayunosInt++; oComDia1.iDesayunosInt++; } else { oCant.iCantDesayunos++; oComDia1.iDesayunosNal++; }
                                                            }
                                                            //Tercera condición
                                                            else if ((fHoraInicio >= fInicioDesayuno && fHoraFinal > fFinDesayuno) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioComida))
                                                            {
                                                                if (bEsInterFinal) { oCant.iCantDesayunosInt++; oComDia1.iDesayunosInt++; } else { oCant.iCantDesayunos++; oComDia1.iDesayunosNal++; }
                                                            }
                                                            //Cuarta condición
                                                            else if ((fHoraInicio < fInicioDesayuno && fHoraFinal <= fInicioDesayuno) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                                                            {
                                                                if (bEsInterFinal) { oCant.iCantDesayunosInt++; oComDia1.iDesayunosInt++; } else { oCant.iCantDesayunos++; oComDia1.iDesayunosNal++; }
                                                            }
                                                            //Quinta condición
                                                            else if ((fHoraInicio < fInicioDesayuno && fHoraFinal < fInicioDesayuno) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                                                            {
                                                                if (bEsInterFinal) { oCant.iCantDesayunosInt++; oComDia1.iDesayunosInt++; } else { oCant.iCantDesayunos++; oComDia1.iDesayunosNal++; }
                                                            }
                                                            //Sexta condición
                                                            else if ((fHoraInicio < fInicioDesayuno && (fHoraFinal > fInicioDesayuno && fHoraFinal <= fFinDesayuno)) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                                                            {
                                                                if (bEsInterFinal) { oCant.iCantDesayunosInt++; oComDia1.iDesayunosInt++; } else { oCant.iCantDesayunos++; oComDia1.iDesayunosNal++; }
                                                            }
                                                            //Septima condición
                                                            else if ((fHoraInicio >= fInicioDesayuno && fHoraInicio <= fFinDesayuno) && (fHoraFinal > fInicioDesayuno && fHoraFinal <= fFinDesayuno) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                                                            {
                                                                if (bEsInterFinal) { oCant.iCantDesayunosInt++; oComDia1.iDesayunosInt++; } else { oCant.iCantDesayunos++; oComDia1.iDesayunosNal++; }
                                                            }

                                                            //Octava condición
                                                            else if ((fHoraInicio < fInicioDesayuno && (fHoraFinal > fInicioDesayuno && fHoraFinal >= fFinDesayuno)) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioComida))
                                                            {
                                                                if (bEsInterFinal) { oCant.iCantDesayunosInt++; oComDia1.iDesayunosInt++; } else { oCant.iCantDesayunos++; oComDia1.iDesayunosNal++; }
                                                            }
                                                            #endregion

                                                            #region COMIDA
                                                            //Primera condicion Comidas
                                                            if (((fHoraInicio < fInicioComida && fHoraFinal > fInicioComida) && fHoraFinal < fFinComida) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                                                            {
                                                                if (bEsInterFinal) { oCant.iCantComidasInt++; oComDia1.iComidaInt++; } else { oCant.iCantComidas++; oComDia1.iComidaNal++; }
                                                            }
                                                            //Segunda condición Comidas
                                                            else if ((fHoraInicio > fInicioComida && fHoraInicio < fFinComida) && (fHoraFinal > fFinComida && fHoraFinal > fInicioComida) && (oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioCena)) //Evaluar antes de cena
                                                            {
                                                                if (bEsInterFinal) { oCant.iCantComidasInt++; oComDia1.iComidaInt++; } else { oCant.iCantComidas++; oComDia1.iComidaNal++; }
                                                            }
                                                            //Tercera condición Comidas
                                                            else if ((fHoraInicio >= fInicioComida && fHoraFinal > fFinComida) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioCena))
                                                            {
                                                                if (bEsInterFinal) { oCant.iCantComidasInt++; oComDia1.iComidaInt++; } else { oCant.iCantComidas++; oComDia1.iComidaNal++; }
                                                            }
                                                            //Cuarta condición Comidas
                                                            else if ((fHoraInicio < fInicioComida && fHoraFinal <= fInicioComida) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                                                            {
                                                                if (bEsInterFinal) { oCant.iCantComidasInt++; oComDia1.iComidaInt++; } else { oCant.iCantComidas++; oComDia1.iComidaNal++; }
                                                            }
                                                            //Quinta condición Comidas
                                                            else if ((fHoraInicio < fInicioComida && fHoraFinal < fInicioComida) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                                                            {
                                                                if (bEsInterFinal) { oCant.iCantComidasInt++; oComDia1.iComidaInt++; } else { oCant.iCantComidas++; oComDia1.iComidaNal++; }
                                                            }
                                                            //Sexta condición Comidas
                                                            else if ((fHoraInicio < fInicioComida && (fHoraFinal > fInicioComida && fHoraFinal <= fFinComida)) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                                                            {
                                                                if (bEsInterFinal) { oCant.iCantComidasInt++; oComDia1.iComidaInt++; } else { oCant.iCantComidas++; oComDia1.iComidaNal++; }
                                                            }
                                                            //Septima condición Comidas
                                                            else if ((fHoraInicio >= fInicioComida && fHoraInicio <= fFinComida) && (fHoraFinal > fInicioComida && fHoraFinal <= fFinComida) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                                                            {
                                                                if (bEsInterFinal) { oCant.iCantComidasInt++; oComDia1.iComidaInt++; } else { oCant.iCantComidas++; oComDia1.iComidaNal++; }
                                                            }
                                                            //Octava condición
                                                            else if ((fHoraInicio < fInicioComida && (fHoraFinal > fInicioComida && fHoraFinal >= fFinComida)) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioCena))
                                                            {
                                                                if (bEsInterFinal) { oCant.iCantComidasInt++; oComDia1.iComidaInt++; } else { oCant.iCantComidas++; oComDia1.iComidaNal++; }
                                                            }
                                                            #endregion

                                                            #region CENA
                                                            //Primera condicion Cenas
                                                            if (((fHoraInicio < fInicioCena && fHoraFinal > fInicioCena) && fHoraFinal < fFinCena) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                                                            {
                                                                if (bEsInterFinal) { oCant.iCantCenasInt++; oComDia1.iCenaInt++; } else { oCant.iCantCenas++; oComDia1.iCenaNal++; }
                                                            }
                                                            //Segunda condición Cenas
                                                            else if ((fHoraInicio > fInicioCena && fHoraInicio < fFinCena) && (fHoraFinal > fFinCena && fHoraFinal > fInicioCena) && (oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                                                            {
                                                                if (bEsInterFinal) { oCant.iCantCenasInt++; oComDia1.iCenaInt++; } else { oCant.iCantCenas++; oComDia1.iCenaNal++; }
                                                            }
                                                            //Tercera condición Cenas
                                                            else if ((fHoraInicio >= fInicioCena && fHoraFinal > fFinCena) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                                                            {
                                                                if (bEsInterFinal) { oCant.iCantCenasInt++; oComDia1.iCenaInt++; } else { oCant.iCantCenas++; oComDia1.iCenaNal++; }
                                                            }
                                                            //Cuarta condición Cenas
                                                            else if ((fHoraInicio < fInicioCena && fHoraFinal <= fInicioCena) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                                                            {
                                                                if (bEsInterFinal) { oCant.iCantCenasInt++; oComDia1.iCenaInt++; } else { oCant.iCantCenas++; oComDia1.iCenaNal++; }
                                                            }
                                                            //Quinta condición Cenas
                                                            else if ((fHoraInicio < fInicioCena && fHoraFinal < fInicioCena) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                                                            {
                                                                if (bEsInterFinal) { oCant.iCantCenasInt++; oComDia1.iCenaInt++; } else { oCant.iCantCenas++; oComDia1.iCenaNal++; }
                                                            }
                                                            //Sexta condición Cenas
                                                            else if ((fHoraInicio < fInicioCena && (fHoraFinal > fInicioCena && fHoraFinal <= fFinCena)) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                                                            {
                                                                if (bEsInterFinal) { oCant.iCantCenasInt++; oComDia1.iCenaInt++; } else { oCant.iCantCenas++; oComDia1.iCenaNal++; }
                                                            }
                                                            //Septima condición Cenas
                                                            else if ((fHoraInicio >= fInicioCena && fHoraInicio <= fFinCena) && (fHoraFinal > fInicioCena && fHoraFinal <= fFinCena) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                                                            {
                                                                if (bEsInterFinal) { oCant.iCantCenasInt++; oComDia1.iCenaInt++; } else { oCant.iCantCenas++; oComDia1.iCenaNal++; }
                                                            }
                                                            //Octava condición Cenas
                                                            else if ((fHoraInicio < fInicioCena && (fHoraFinal > fInicioCena && fHoraFinal >= fFinCena)) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                                                            {
                                                                if (bEsInterFinal) { oCant.iCantCenasInt++; oComDia1.iCenaInt++; } else { oCant.iCantCenas++; oComDia1.iCenaNal++; }
                                                            }
                                                            #endregion

                                                        }

                                                        oLsComDia.Add(oComDia1);
                                                    }
                                                    #endregion
                                                }
                                                else
                                                {
                                                    ComidasPorDia oComDia1 = new ComidasPorDia();
                                                    oComDia1.sClavePiloto = drP["ClavePiloto"].S();
                                                    oComDia1.dtFechaDia = rowsD[0][FechaInicio].S().Dt();
                                                    oComDia1.dtFechaFin = rowsD[0][FechaFin].S().Dt();
                                                    oComDia1.sduty_type = rowsD[0]["DutyType"].S();
                                                    oComDia1.sOrigen = rowsD[0]["POD"].S();
                                                    oComDia1.sDestino = rowsD[0]["POA"].S();


                                                    DateTime dtInicial = rowsD[0][FechaInicio].S().Dt();
                                                    DateTime dtFinal = rowsD[0][FechaFin].S().Dt();

                                                    bEsInterFinal = rowsD[0]["EsInternacional"].S() == "1" ? true : false;
                                                    bEsInterInicio = rowsD[0]["PaisPOD"].S() == "MX" ? false : true;


                                                    fHoraInicio = dtInicial.Hour + (dtInicial.Minute / float.Parse("60"));
                                                    fHoraFinal = dtFinal.Hour + (dtFinal.Minute / float.Parse("60"));

                                                    //if (l > 0)
                                                    //{
                                                    //    fHoraInicio = 0;
                                                    //}

                                                    #region CODIGO ANTERIOR
                                                    //// SI - CENA
                                                    //if ((fHoraInicio < fInicioCena && fHoraFinal >= fInicioCena) || (fHoraInicio <= fFinCena && fHoraFinal > fInicioCena))
                                                    //{
                                                    //    if (bEsInterFinal)
                                                    //    {
                                                    //        if (l > 0)
                                                    //        {
                                                    //            if (!bEsInterInicio)
                                                    //            {
                                                    //                oCant.iCantDesayunos++;
                                                    //                oCant.iCantComidas++;

                                                    //                oComDia1.iDesayunosNal++;
                                                    //                oComDia1.iComidaNal++;
                                                    //            }
                                                    //        }


                                                    //        oCant.iCantCenasInt++;
                                                    //        oComDia1.iCenaInt++;
                                                    //    }
                                                    //    else
                                                    //    {
                                                    //        if (l > 0)
                                                    //        {
                                                    //            if (bEsInterInicio)
                                                    //            {
                                                    //                oCant.iCantDesayunosInt++;
                                                    //                oCant.iCantComidasInt++;

                                                    //                oComDia1.iDesayunosInt++;
                                                    //                oComDia1.iComidaInt++;
                                                    //            }
                                                    //        }

                                                    //        oCant.iCantCenas++;
                                                    //        oComDia1.iCenaNal++;
                                                    //    }
                                                    //}
                                                    //// SI - DESAYUNO / COMIDA
                                                    //else if ((fHoraInicio <= fInicioComida && (fHoraFinal > fInicioComida)) || (fHoraInicio > fInicioComida && fHoraInicio <= fFinComida) || (fHoraInicio < fInicioComida && fHoraFinal > fFinComida))
                                                    //{
                                                    //    if (bEsInterFinal)
                                                    //    {
                                                    //        if (l > 0)
                                                    //        {
                                                    //            if (!bEsInterInicio)
                                                    //            {
                                                    //                oCant.iCantDesayunos++;
                                                    //                oComDia1.iDesayunosNal++;
                                                    //            }
                                                    //        }

                                                    //        oCant.iCantComidasInt++;
                                                    //        oComDia1.iComidaInt++;
                                                    //    }
                                                    //    else
                                                    //    {
                                                    //        if (l > 0)
                                                    //        {
                                                    //            if (bEsInterInicio)
                                                    //            {
                                                    //                oCant.iCantDesayunosInt++;
                                                    //                oComDia1.iDesayunosInt++;
                                                    //            }
                                                    //        }

                                                    //        oCant.iCantComidas++;
                                                    //        oComDia1.iComidaNal++;
                                                    //    }

                                                    //    if (l == 0)
                                                    //    {
                                                    //        if (bEsInterFinal)
                                                    //        {
                                                    //            oCant.iCantCenasInt++;
                                                    //            oComDia1.iCenaInt++;
                                                    //        }
                                                    //        else
                                                    //        {
                                                    //            oCant.iCantCenas++;
                                                    //            oComDia1.iCenaNal++;
                                                    //        }
                                                    //    }
                                                    //}
                                                    //// SI - DESAYUNO / COMIDA / CENA
                                                    //else if (fHoraInicio < fInicioDesayuno || (fHoraInicio > fInicioDesayuno && fHoraInicio < fFinDesayuno))
                                                    //{
                                                    //    if (bEsInterFinal)
                                                    //    {
                                                    //        oCant.iCantDesayunosInt++;
                                                    //        //oCant.iCantComidasInt++;
                                                    //        //oCant.iCantCenasInt++;

                                                    //        oComDia1.iDesayunosInt++;
                                                    //        //oComDia1.iComidaInt++;
                                                    //        //oComDia1.iCenaInt++;
                                                    //    }
                                                    //    else
                                                    //    {
                                                    //        oCant.iCantDesayunos++;
                                                    //        //oCant.iCantComidas++;
                                                    //        //oCant.iCantCenas++;

                                                    //        oComDia1.iDesayunosNal++;
                                                    //        //oComDia1.iComidaNal++;
                                                    //        //oComDia1.iCenaNal++;
                                                    //    }
                                                    //}
                                                    //else
                                                    //{
                                                    //    if (l == 0)
                                                    //    {
                                                    //        if (bEsInterFinal)
                                                    //        {
                                                    //            oCant.iCantCenasInt++;
                                                    //            oComDia1.iCenaInt++;
                                                    //        }
                                                    //        else
                                                    //        {
                                                    //            oCant.iCantCenas++;
                                                    //            oComDia1.iCenaNal++;
                                                    //        }
                                                    //    }
                                                    //}
                                                    #endregion

                                                    #region DESAYUNO
                                                    //Primera condicion
                                                    if (((fHoraInicio < fInicioDesayuno && fHoraFinal > fInicioDesayuno) && fHoraFinal < fFinDesayuno) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                                                    {
                                                        if (bEsInterFinal) { oCant.iCantDesayunosInt++; oComDia1.iDesayunosInt++; }
                                                        else { oCant.iCantDesayunos++; oComDia1.iDesayunosNal++; }
                                                    }
                                                    //Segunda condición
                                                    else if ((fHoraInicio > fInicioDesayuno && fHoraInicio < fFinDesayuno) && (fHoraFinal > fFinDesayuno && fHoraFinal > fInicioDesayuno) && (oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioComida)) //Evaluar antes de comida
                                                    {
                                                        if (bEsInterFinal) { oCant.iCantDesayunosInt++; oComDia1.iDesayunosInt++; }
                                                        else { oCant.iCantDesayunos++; oComDia1.iDesayunosNal++; }
                                                    }
                                                    //Tercera condición
                                                    else if ((fHoraInicio >= fInicioDesayuno && fHoraFinal > fFinDesayuno) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioComida))
                                                    {
                                                        if (bEsInterFinal) { oCant.iCantDesayunosInt++; oComDia1.iDesayunosInt++; }
                                                        else { oCant.iCantDesayunos++; oComDia1.iDesayunosNal++; }
                                                    }
                                                    //Cuarta condición
                                                    else if ((fHoraInicio < fInicioDesayuno && fHoraFinal <= fInicioDesayuno) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioComida))
                                                    {
                                                        if (bEsInterFinal) { oCant.iCantDesayunosInt++; oComDia1.iDesayunosInt++; }
                                                        else { oCant.iCantDesayunos++; oComDia1.iDesayunosNal++; }
                                                    }
                                                    //Quinta condición
                                                    else if ((fHoraInicio < fInicioDesayuno && fHoraFinal < fInicioDesayuno) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioComida))
                                                    {
                                                        if (bEsInterFinal) { oCant.iCantDesayunosInt++; oComDia1.iDesayunosInt++; }
                                                        else { oCant.iCantDesayunos++; oComDia1.iDesayunosNal++; }
                                                    }
                                                    //Sexta condición
                                                    else if ((fHoraInicio < fInicioDesayuno && (fHoraFinal > fInicioDesayuno && fHoraFinal <= fFinDesayuno)) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioComida))
                                                    {
                                                        if (bEsInterFinal) { oCant.iCantDesayunosInt++; oComDia1.iDesayunosInt++; }
                                                        else { oCant.iCantDesayunos++; oComDia1.iDesayunosNal++; }
                                                    }
                                                    //Septima condición
                                                    else if ((fHoraInicio >= fInicioDesayuno && fHoraInicio <= fFinDesayuno) && (fHoraFinal > fInicioDesayuno && fHoraFinal <= fFinDesayuno) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioComida))
                                                    {
                                                        if (bEsInterFinal) { oCant.iCantDesayunosInt++; oComDia1.iDesayunosInt++; }
                                                        else { oCant.iCantDesayunos++; oComDia1.iDesayunosNal++; }
                                                    }
                                                    //Octava condición
                                                    else if ((fHoraInicio < fInicioDesayuno && (fHoraFinal > fInicioDesayuno && fHoraFinal >= fFinDesayuno)) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioComida))
                                                    {
                                                        if (bEsInterFinal) { oCant.iCantDesayunosInt++; oComDia1.iDesayunosInt++; }
                                                        else { oCant.iCantDesayunos++; oComDia1.iDesayunosNal++; }
                                                    }
                                                    else
                                                    {
                                                        if (l == 0)
                                                        {
                                                            if (bEsInterFinal)
                                                            {
                                                                oCant.iCantDesayunosInt++;
                                                                oComDia1.iDesayunosInt++;
                                                            }
                                                            else
                                                            {
                                                                oCant.iCantDesayunos++;
                                                                oComDia1.iDesayunosNal++;
                                                            }
                                                        }
                                                    }
                                                    #endregion

                                                    #region COMIDA
                                                    //Primera condicion Comidas
                                                    if (((fHoraInicio < fInicioComida && fHoraFinal > fInicioComida) && fHoraFinal < fFinComida) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioCena))
                                                    {
                                                        if (bEsInterFinal) { oCant.iCantComidasInt++; oComDia1.iComidaInt++; } else { oCant.iCantComidas++; oComDia1.iComidaNal++; }
                                                        //if (bEsInterFinal)
                                                        //{
                                                        //    if (l > 0)
                                                        //    {
                                                        //        if (!bEsInterInicio)
                                                        //        {
                                                        //            oCant.iCantDesayunos++;
                                                        //            oComDia1.iDesayunosNal++;
                                                        //        }
                                                        //    }

                                                        //    oCant.iCantComidasInt++;
                                                        //    oComDia1.iComidaInt++;
                                                        //}
                                                        //else
                                                        //{
                                                        //    if (l > 0)
                                                        //    {
                                                        //        if (bEsInterInicio)
                                                        //        {
                                                        //            oCant.iCantDesayunosInt++;
                                                        //            oComDia1.iDesayunosInt++;
                                                        //        }
                                                        //    }

                                                        //    oCant.iCantComidas++;
                                                        //    oComDia1.iComidaNal++;
                                                        //}

                                                        //if (l == 0)
                                                        //{
                                                        //    if (bEsInterFinal)
                                                        //    {
                                                        //        oCant.iCantCenasInt++;
                                                        //        oComDia1.iCenaInt++;
                                                        //    }
                                                        //    else
                                                        //    {
                                                        //        oCant.iCantCenas++;
                                                        //        oComDia1.iCenaNal++;
                                                        //    }
                                                        //}
                                                    }
                                                    //Segunda condición Comidas
                                                    else if ((fHoraInicio > fInicioComida && fHoraInicio < fFinComida) && (fHoraFinal > fFinComida && fHoraFinal > fInicioComida) && (oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioCena)) //Evaluar antes de cena
                                                    {
                                                        if (bEsInterFinal) { oCant.iCantComidasInt++; oComDia1.iComidaInt++; } else { oCant.iCantComidas++; oComDia1.iComidaNal++; }
                                                        //if (bEsInterFinal)
                                                        //{
                                                        //    if (l > 0)
                                                        //    {
                                                        //        if (!bEsInterInicio)
                                                        //        {
                                                        //            oCant.iCantDesayunos++;
                                                        //            oComDia1.iDesayunosNal++;
                                                        //        }
                                                        //    }

                                                        //    oCant.iCantComidasInt++;
                                                        //    oComDia1.iComidaInt++;
                                                        //}
                                                        //else
                                                        //{
                                                        //    if (l > 0)
                                                        //    {
                                                        //        if (bEsInterInicio)
                                                        //        {
                                                        //            oCant.iCantDesayunosInt++;
                                                        //            oComDia1.iDesayunosInt++;
                                                        //        }
                                                        //    }

                                                        //    oCant.iCantComidas++;
                                                        //    oComDia1.iComidaNal++;
                                                        //}

                                                        //if (l == 0)
                                                        //{
                                                        //    if (bEsInterFinal)
                                                        //    {
                                                        //        oCant.iCantCenasInt++;
                                                        //        oComDia1.iCenaInt++;
                                                        //    }
                                                        //    else
                                                        //    {
                                                        //        oCant.iCantCenas++;
                                                        //        oComDia1.iCenaNal++;
                                                        //    }
                                                        //}
                                                    }
                                                    //Tercera condición Comidas
                                                    else if ((fHoraInicio >= fInicioComida && fHoraFinal > fFinComida) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioCena))
                                                    {
                                                        if (bEsInterFinal) { oCant.iCantComidasInt++; oComDia1.iComidaInt++; } else { oCant.iCantComidas++; oComDia1.iComidaNal++; }
                                                        //if (bEsInterFinal)
                                                        //{
                                                        //    if (l > 0)
                                                        //    {
                                                        //        if (!bEsInterInicio)
                                                        //        {
                                                        //            oCant.iCantDesayunos++;
                                                        //            oComDia1.iDesayunosNal++;
                                                        //        }
                                                        //    }

                                                        //    oCant.iCantComidasInt++;
                                                        //    oComDia1.iComidaInt++;
                                                        //}
                                                        //else
                                                        //{
                                                        //    if (l > 0)
                                                        //    {
                                                        //        if (bEsInterInicio)
                                                        //        {
                                                        //            oCant.iCantDesayunosInt++;
                                                        //            oComDia1.iDesayunosInt++;
                                                        //        }
                                                        //    }

                                                        //    oCant.iCantComidas++;
                                                        //    oComDia1.iComidaNal++;
                                                        //}

                                                        //if (l == 0)
                                                        //{
                                                        //    if (bEsInterFinal)
                                                        //    {
                                                        //        oCant.iCantCenasInt++;
                                                        //        oComDia1.iCenaInt++;
                                                        //    }
                                                        //    else
                                                        //    {
                                                        //        oCant.iCantCenas++;
                                                        //        oComDia1.iCenaNal++;
                                                        //    }
                                                        //}
                                                    }
                                                    //Cuarta condición Comidas
                                                    else if ((fHoraInicio < fInicioComida && fHoraFinal <= fInicioComida) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioCena))
                                                    {
                                                        if (bEsInterFinal) { oCant.iCantComidasInt++; oComDia1.iComidaInt++; } else { oCant.iCantComidas++; oComDia1.iComidaNal++; }
                                                        //if (bEsInterFinal)
                                                        //{
                                                        //    if (l > 0)
                                                        //    {
                                                        //        if (!bEsInterInicio)
                                                        //        {
                                                        //            oCant.iCantDesayunos++;
                                                        //            oComDia1.iDesayunosNal++;
                                                        //        }
                                                        //    }

                                                        //    oCant.iCantComidasInt++;
                                                        //    oComDia1.iComidaInt++;
                                                        //}
                                                        //else
                                                        //{
                                                        //    if (l > 0)
                                                        //    {
                                                        //        if (bEsInterInicio)
                                                        //        {
                                                        //            oCant.iCantDesayunosInt++;
                                                        //            oComDia1.iDesayunosInt++;
                                                        //        }
                                                        //    }

                                                        //    oCant.iCantComidas++;
                                                        //    oComDia1.iComidaNal++;
                                                        //}

                                                        //if (l == 0)
                                                        //{
                                                        //    if (bEsInterFinal)
                                                        //    {
                                                        //        oCant.iCantCenasInt++;
                                                        //        oComDia1.iCenaInt++;
                                                        //    }
                                                        //    else
                                                        //    {
                                                        //        oCant.iCantCenas++;
                                                        //        oComDia1.iCenaNal++;
                                                        //    }
                                                        //}
                                                    }
                                                    //Quinta condición Comidas
                                                    else if ((fHoraInicio < fInicioComida && fHoraFinal < fInicioComida) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioCena))
                                                    {
                                                        if (bEsInterFinal) { oCant.iCantComidasInt++; oComDia1.iComidaInt++; } else { oCant.iCantComidas++; oComDia1.iComidaNal++; }
                                                        //if (bEsInterFinal)
                                                        //{
                                                        //    if (l > 0)
                                                        //    {
                                                        //        if (!bEsInterInicio)
                                                        //        {
                                                        //            oCant.iCantDesayunos++;
                                                        //            oComDia1.iDesayunosNal++;
                                                        //        }
                                                        //    }

                                                        //    oCant.iCantComidasInt++;
                                                        //    oComDia1.iComidaInt++;
                                                        //}
                                                        //else
                                                        //{
                                                        //    if (l > 0)
                                                        //    {
                                                        //        if (bEsInterInicio)
                                                        //        {
                                                        //            oCant.iCantDesayunosInt++;
                                                        //            oComDia1.iDesayunosInt++;
                                                        //        }
                                                        //    }

                                                        //    oCant.iCantComidas++;
                                                        //    oComDia1.iComidaNal++;
                                                        //}

                                                        //if (l == 0)
                                                        //{
                                                        //    if (bEsInterFinal)
                                                        //    {
                                                        //        oCant.iCantCenasInt++;
                                                        //        oComDia1.iCenaInt++;
                                                        //    }
                                                        //    else
                                                        //    {
                                                        //        oCant.iCantCenas++;
                                                        //        oComDia1.iCenaNal++;
                                                        //    }
                                                        //}
                                                    }
                                                    //Sexta condición Comidas
                                                    else if ((fHoraInicio < fInicioComida && (fHoraFinal > fInicioComida && fHoraFinal <= fFinComida)) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioCena))
                                                    {
                                                        if (bEsInterFinal) { oCant.iCantComidasInt++; oComDia1.iComidaInt++; } else { oCant.iCantComidas++; oComDia1.iComidaNal++; }
                                                        //if (bEsInterFinal)
                                                        //{
                                                        //    if (l > 0)
                                                        //    {
                                                        //        if (!bEsInterInicio)
                                                        //        {
                                                        //            oCant.iCantDesayunos++;
                                                        //            oComDia1.iDesayunosNal++;
                                                        //        }
                                                        //    }

                                                        //    oCant.iCantComidasInt++;
                                                        //    oComDia1.iComidaInt++;
                                                        //}
                                                        //else
                                                        //{
                                                        //    if (l > 0)
                                                        //    {
                                                        //        if (bEsInterInicio)
                                                        //        {
                                                        //            oCant.iCantDesayunosInt++;
                                                        //            oComDia1.iDesayunosInt++;
                                                        //        }
                                                        //    }

                                                        //    oCant.iCantComidas++;
                                                        //    oComDia1.iComidaNal++;
                                                        //}

                                                        //if (l == 0)
                                                        //{
                                                        //    if (bEsInterFinal)
                                                        //    {
                                                        //        oCant.iCantCenasInt++;
                                                        //        oComDia1.iCenaInt++;
                                                        //    }
                                                        //    else
                                                        //    {
                                                        //        oCant.iCantCenas++;
                                                        //        oComDia1.iCenaNal++;
                                                        //    }
                                                        //}
                                                    }
                                                    //Septima condición Comidas
                                                    else if ((fHoraInicio >= fInicioComida && fHoraInicio <= fFinComida) && (fHoraFinal > fInicioComida && fHoraFinal <= fFinComida) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioCena))
                                                    {
                                                        if (bEsInterFinal) { oCant.iCantComidasInt++; oComDia1.iComidaInt++; } else { oCant.iCantComidas++; oComDia1.iComidaNal++; }
                                                        //if (bEsInterFinal)
                                                        //{
                                                        //    if (l > 0)
                                                        //    {
                                                        //        if (!bEsInterInicio)
                                                        //        {
                                                        //            oCant.iCantDesayunos++;
                                                        //            oComDia1.iDesayunosNal++;
                                                        //        }
                                                        //    }

                                                        //    oCant.iCantComidasInt++;
                                                        //    oComDia1.iComidaInt++;
                                                        //}
                                                        //else
                                                        //{
                                                        //    if (l > 0)
                                                        //    {
                                                        //        if (bEsInterInicio)
                                                        //        {
                                                        //            oCant.iCantDesayunosInt++;
                                                        //            oComDia1.iDesayunosInt++;
                                                        //        }
                                                        //    }

                                                        //    oCant.iCantComidas++;
                                                        //    oComDia1.iComidaNal++;
                                                        //}

                                                        //if (l == 0)
                                                        //{
                                                        //    if (bEsInterFinal)
                                                        //    {
                                                        //        oCant.iCantCenasInt++;
                                                        //        oComDia1.iCenaInt++;
                                                        //    }
                                                        //    else
                                                        //    {
                                                        //        oCant.iCantCenas++;
                                                        //        oComDia1.iCenaNal++;
                                                        //    }
                                                        //}
                                                    }
                                                    //Octava condición
                                                    else if ((fHoraInicio < fInicioComida && (fHoraFinal > fInicioComida && fHoraFinal >= fFinComida)) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioCena))
                                                    {
                                                        if (bEsInterFinal) { oCant.iCantComidasInt++; oComDia1.iComidaInt++; } else { oCant.iCantComidas++; oComDia1.iComidaNal++; }
                                                        //if (bEsInterFinal)
                                                        //{
                                                        //    if (l > 0)
                                                        //    {
                                                        //        if (!bEsInterInicio)
                                                        //        {
                                                        //            oCant.iCantDesayunos++;
                                                        //            oComDia1.iDesayunosNal++;
                                                        //        }
                                                        //    }

                                                        //    oCant.iCantComidasInt++;
                                                        //    oComDia1.iComidaInt++;
                                                        //}
                                                        //else
                                                        //{
                                                        //    if (l > 0)
                                                        //    {
                                                        //        if (bEsInterInicio)
                                                        //        {
                                                        //            oCant.iCantDesayunosInt++;
                                                        //            oComDia1.iDesayunosInt++;
                                                        //        }
                                                        //    }

                                                        //    oCant.iCantComidas++;
                                                        //    oComDia1.iComidaNal++;
                                                        //}

                                                        //if (l == 0)
                                                        //{
                                                        //    if (bEsInterFinal)
                                                        //    {
                                                        //        oCant.iCantCenasInt++;
                                                        //        oComDia1.iCenaInt++;
                                                        //    }
                                                        //    else
                                                        //    {
                                                        //        oCant.iCantCenas++;
                                                        //        oComDia1.iCenaNal++;
                                                        //    }
                                                        //}
                                                    }
                                                    #endregion

                                                    #region CENA
                                                    //Primera condicion Cenas
                                                    if (((fHoraInicio < fInicioCena && fHoraFinal > fInicioCena) && fHoraFinal < fFinCena) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                                                    {
                                                        if (bEsInterFinal) { oCant.iCantCenasInt++; oComDia1.iCenaInt++; } else { oCant.iCantCenas++; oComDia1.iCenaNal++; }
                                                        //if (bEsInterFinal)
                                                        //{
                                                        //    if (l > 0)
                                                        //    {
                                                        //        if (!bEsInterInicio)
                                                        //        {
                                                        //            oCant.iCantDesayunos++;
                                                        //            oCant.iCantComidas++;

                                                        //            oComDia1.iDesayunosNal++;
                                                        //            oComDia1.iComidaNal++;
                                                        //        }
                                                        //    }


                                                        //    oCant.iCantCenasInt++;
                                                        //    oComDia1.iCenaInt++;
                                                        //}
                                                        //else
                                                        //{
                                                        //    if (l > 0)
                                                        //    {
                                                        //        if (bEsInterInicio)
                                                        //        {
                                                        //            oCant.iCantDesayunosInt++;
                                                        //            oCant.iCantComidasInt++;

                                                        //            oComDia1.iDesayunosInt++;
                                                        //            oComDia1.iComidaInt++;
                                                        //        }
                                                        //    }

                                                        //    oCant.iCantCenas++;
                                                        //    oComDia1.iCenaNal++;
                                                    //}
                                                    }
                                                    //Segunda condición Cenas
                                                    else if ((fHoraInicio > fInicioCena && fHoraInicio < fFinCena) && (fHoraFinal > fFinCena && fHoraFinal > fInicioCena) && (oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                                                    {
                                                        if (bEsInterFinal) { oCant.iCantCenasInt++; oComDia1.iCenaInt++; } else { oCant.iCantCenas++; oComDia1.iCenaNal++; }
                                                        //if (bEsInterFinal)
                                                        //{
                                                        //    if (l > 0)
                                                        //    {
                                                        //        if (!bEsInterInicio)
                                                        //        {
                                                        //            oCant.iCantDesayunos++;
                                                        //            oCant.iCantComidas++;

                                                        //            oComDia1.iDesayunosNal++;
                                                        //            oComDia1.iComidaNal++;
                                                        //        }
                                                        //    }


                                                        //    oCant.iCantCenasInt++;
                                                        //    oComDia1.iCenaInt++;
                                                        //}
                                                        //else
                                                        //{
                                                        //    if (l > 0)
                                                        //    {
                                                        //        if (bEsInterInicio)
                                                        //        {
                                                        //            oCant.iCantDesayunosInt++;
                                                        //            oCant.iCantComidasInt++;

                                                        //            oComDia1.iDesayunosInt++;
                                                        //            oComDia1.iComidaInt++;
                                                        //        }
                                                        //    }

                                                        //    oCant.iCantCenas++;
                                                        //    oComDia1.iCenaNal++;
                                                        //}
                                                    }
                                                    //Tercera condición Cenas
                                                    else if ((fHoraInicio >= fInicioCena && fHoraFinal > fFinCena) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                                                    {
                                                        if (bEsInterFinal) { oCant.iCantCenasInt++; oComDia1.iCenaInt++; } else { oCant.iCantCenas++; oComDia1.iCenaNal++; }

                                                        //if (bEsInterFinal)
                                                        //{
                                                        //    if (l > 0)
                                                        //    {
                                                        //        if (!bEsInterInicio)
                                                        //        {
                                                        //            oCant.iCantDesayunos++;
                                                        //            oCant.iCantComidas++;

                                                        //            oComDia1.iDesayunosNal++;
                                                        //            oComDia1.iComidaNal++;
                                                        //        }
                                                        //    }


                                                        //    oCant.iCantCenasInt++;
                                                        //    oComDia1.iCenaInt++;
                                                        //}
                                                        //else
                                                        //{
                                                        //    if (l > 0)
                                                        //    {
                                                        //        if (bEsInterInicio)
                                                        //        {
                                                        //            oCant.iCantDesayunosInt++;
                                                        //            oCant.iCantComidasInt++;

                                                        //            oComDia1.iDesayunosInt++;
                                                        //            oComDia1.iComidaInt++;
                                                        //        }
                                                        //    }

                                                        //    oCant.iCantCenas++;
                                                        //    oComDia1.iCenaNal++;
                                                        //}
                                                    }
                                                    //Cuarta condición Cenas
                                                    else if ((fHoraInicio < fInicioCena && fHoraFinal <= fInicioCena) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                                                    {
                                                        if (bEsInterFinal) { oCant.iCantCenasInt++; oComDia1.iCenaInt++; } else { oCant.iCantCenas++; oComDia1.iCenaNal++; }
                                                        //if (bEsInterFinal)
                                                        //{
                                                        //    if (l > 0)
                                                        //    {
                                                        //        if (!bEsInterInicio)
                                                        //        {
                                                        //            oCant.iCantDesayunos++;
                                                        //            oCant.iCantComidas++;

                                                        //            oComDia1.iDesayunosNal++;
                                                        //            oComDia1.iComidaNal++;
                                                        //        }
                                                        //    }


                                                        //    oCant.iCantCenasInt++;
                                                        //    oComDia1.iCenaInt++;
                                                        //}
                                                        //else
                                                        //{
                                                        //    if (l > 0)
                                                        //    {
                                                        //        if (bEsInterInicio)
                                                        //        {
                                                        //            oCant.iCantDesayunosInt++;
                                                        //            oCant.iCantComidasInt++;

                                                        //            oComDia1.iDesayunosInt++;
                                                        //            oComDia1.iComidaInt++;
                                                        //        }
                                                        //    }

                                                        //    oCant.iCantCenas++;
                                                        //    oComDia1.iCenaNal++;
                                                        //}
                                                    }
                                                    //Quinta condición Cenas
                                                    else if ((fHoraInicio < fInicioCena && fHoraFinal < fInicioCena) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                                                    {
                                                        if (bEsInterFinal) { oCant.iCantCenasInt++; oComDia1.iCenaInt++; } else { oCant.iCantCenas++; oComDia1.iCenaNal++; }
                                                        //if (bEsInterFinal)
                                                        //{
                                                        //    if (l > 0)
                                                        //    {
                                                        //        if (!bEsInterInicio)
                                                        //        {
                                                        //            oCant.iCantDesayunos++;
                                                        //            oCant.iCantComidas++;

                                                        //            oComDia1.iDesayunosNal++;
                                                        //            oComDia1.iComidaNal++;
                                                        //        }
                                                        //    }


                                                        //    oCant.iCantCenasInt++;
                                                        //    oComDia1.iCenaInt++;
                                                        //}
                                                        //else
                                                        //{
                                                        //    if (l > 0)
                                                        //    {
                                                        //        if (bEsInterInicio)
                                                        //        {
                                                        //            oCant.iCantDesayunosInt++;
                                                        //            oCant.iCantComidasInt++;

                                                        //            oComDia1.iDesayunosInt++;
                                                        //            oComDia1.iComidaInt++;
                                                        //        }
                                                        //    }

                                                        //    oCant.iCantCenas++;
                                                        //    oComDia1.iCenaNal++;
                                                        //}
                                                    }
                                                    //Sexta condición Cenas
                                                    else if ((fHoraInicio < fInicioCena && (fHoraFinal > fInicioCena && fHoraFinal <= fFinCena)) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                                                    {
                                                        if (bEsInterFinal) { oCant.iCantCenasInt++; oComDia1.iCenaInt++; } else { oCant.iCantCenas++; oComDia1.iCenaNal++; }
                                                        //if (bEsInterFinal)
                                                        //{
                                                        //    if (l > 0)
                                                        //    {
                                                        //        if (!bEsInterInicio)
                                                        //        {
                                                        //            oCant.iCantDesayunos++;
                                                        //            oCant.iCantComidas++;

                                                        //            oComDia1.iDesayunosNal++;
                                                        //            oComDia1.iComidaNal++;
                                                        //        }
                                                        //    }


                                                        //    oCant.iCantCenasInt++;
                                                        //    oComDia1.iCenaInt++;
                                                        //}
                                                        //else
                                                        //{
                                                        //    if (l > 0)
                                                        //    {
                                                        //        if (bEsInterInicio)
                                                        //        {
                                                        //            oCant.iCantDesayunosInt++;
                                                        //            oCant.iCantComidasInt++;

                                                        //            oComDia1.iDesayunosInt++;
                                                        //            oComDia1.iComidaInt++;
                                                        //        }
                                                        //    }

                                                        //    oCant.iCantCenas++;
                                                        //    oComDia1.iCenaNal++;
                                                        //}
                                                    }
                                                    //Septima condición Cenas
                                                    else if ((fHoraInicio >= fInicioCena && fHoraInicio <= fFinCena) && (fHoraFinal > fInicioCena && fHoraFinal <= fFinCena) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                                                    {
                                                        if (bEsInterFinal) { oCant.iCantCenasInt++; oComDia1.iCenaInt++; } else { oCant.iCantCenas++; oComDia1.iCenaNal++; }
                                                        //if (bEsInterFinal)
                                                        //{
                                                        //    if (l > 0)
                                                        //    {
                                                        //        if (!bEsInterInicio)
                                                        //        {
                                                        //            oCant.iCantDesayunos++;
                                                        //            oCant.iCantComidas++;

                                                        //            oComDia1.iDesayunosNal++;
                                                        //            oComDia1.iComidaNal++;
                                                        //        }
                                                        //    }


                                                        //    oCant.iCantCenasInt++;
                                                        //    oComDia1.iCenaInt++;
                                                        //}
                                                        //else
                                                        //{
                                                        //    if (l > 0)
                                                        //    {
                                                        //        if (bEsInterInicio)
                                                        //        {
                                                        //            oCant.iCantDesayunosInt++;
                                                        //            oCant.iCantComidasInt++;

                                                        //            oComDia1.iDesayunosInt++;
                                                        //            oComDia1.iComidaInt++;
                                                        //        }
                                                        //    }

                                                        //    oCant.iCantCenas++;
                                                        //    oComDia1.iCenaNal++;
                                                        //}
                                                    }
                                                    //Octava condición Cenas
                                                    else if ((fHoraInicio < fInicioCena && (fHoraFinal > fInicioCena && fHoraFinal >= fFinCena)) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                                                    {
                                                        if (bEsInterFinal) { oCant.iCantCenasInt++; oComDia1.iCenaInt++; } else { oCant.iCantCenas++; oComDia1.iCenaNal++; }
                                                        //if (bEsInterFinal)
                                                        //{
                                                        //    if (l > 0)
                                                        //    {
                                                        //        if (!bEsInterInicio)
                                                        //        {
                                                        //            oCant.iCantDesayunos++;
                                                        //            oCant.iCantComidas++;

                                                        //            oComDia1.iDesayunosNal++;
                                                        //            oComDia1.iComidaNal++;
                                                        //        }
                                                        //    }


                                                        //    oCant.iCantCenasInt++;
                                                        //    oComDia1.iCenaInt++;
                                                        //}
                                                        //else
                                                        //{
                                                        //    if (l > 0)
                                                        //    {
                                                        //        if (bEsInterInicio)
                                                        //        {
                                                        //            oCant.iCantDesayunosInt++;
                                                        //            oCant.iCantComidasInt++;

                                                        //            oComDia1.iDesayunosInt++;
                                                        //            oComDia1.iComidaInt++;
                                                        //        }
                                                        //    }

                                                        //    oCant.iCantCenas++;
                                                        //    oComDia1.iCenaNal++;
                                                        //}
                                                    }
                                                    #endregion

                                                    oLsComDia.Add(oComDia1);
                                                }
                                            }
                                        }
                                        #endregion

                                        #region CUANDO SON MÁS DE DOS DÍAS
                                        else if (dtDias.Rows.Count > 2)
                                        {
                                            for (int l = 0; l < dtDias.Rows.Count; l++)
                                            {
                                                DataRow[] rowsD = dtLegs.Select("Dia = '" + dtDias.Rows[l]["Dia"].S() + "' ");

                                                bEsInterInicio = rowsD[0]["EsInternacional"].S() == "1" ? true : false;
                                                bEsInterFinal = rowsD[rowsD.Length - 1]["EsInternacional"].S() == "1" ? true : false;

                                                // una pierna que tiene mas de un dia.
                                                if (rowsD.Length == 1 && ((rowsD[0][FechaFin].S().Dt().Day) - (rowsD[0][FechaInicio].S().Dt().Day)) > 0)
                                                {
                                                    bool bTieneDiasAntes = false;
                                                    bool bTieneDiasDespues = false;

                                                    if (l > 0)
                                                        bTieneDiasAntes = true;

                                                    try
                                                    {
                                                        if (dtDias.Rows[l + 1] != null)
                                                            bTieneDiasDespues = true;
                                                    }
                                                    catch (Exception)
                                                    {
                                                        bTieneDiasDespues = false;
                                                    }

                                                    //if (l > 0)
                                                    //    bTieneDiasAntes = true;
                                                    //if (dtDias.Rows[l + 1] != null)
                                                    //    bTieneDiasDespues = true;

                                                    //for (int k = 0; k < rowsD.Length; k++)
                                                    //{
                                                    //    CalculaAlimentos(rowsD[0][FechaInicio].S().Dt(), rowsD[0][FechaFin].S().Dt(), oCant, oHor, sPod, sBase, bTieneDiasAntes, bTieneDiasDespues, bEsInterInicio, bEsInterFinal, drP["ClavePiloto"].S(), oLsComDia, rowsD[0]["DutyType"].S(), rowsD[0]["POD"].S(), rowsD[0]["POA"].S(), dtLegs);
                                                    //}


                                                        CalculaAlimentos(rowsD[0][FechaInicio].S().Dt(), rowsD[0][FechaFin].S().Dt(), oCant, oHor, sPod, sBase, bTieneDiasAntes, bTieneDiasDespues, bEsInterInicio, bEsInterFinal, drP["ClavePiloto"].S(), oLsComDia, rowsD[0]["DutyType"].S(), rowsD[0]["POD"].S(), rowsD[0]["POA"].S(), dtLegs);
                                                }
                                                else if (rowsD.Length > 1)
                                                {
                                                    for (int k = 0; k < rowsD.Length; k++)
                                                    {
                                                        ComidasPorDia oComDia1 = new ComidasPorDia();
                                                        oComDia1.sClavePiloto = drP["ClavePiloto"].S();
                                                        oComDia1.dtFechaDia = rowsD[k][FechaInicio].S().Dt();
                                                        oComDia1.dtFechaFin = rowsD[k][FechaFin].S().Dt();
                                                        oComDia1.sduty_type = rowsD[k]["DutyType"].S();
                                                        oComDia1.sOrigen = rowsD[k]["POD"].S();
                                                        oComDia1.sDestino = rowsD[k]["POA"].S();

                                                        if (k + 1 == rowsD.Length)
                                                        {
                                                            DateTime dtIniDia = rowsD[0][FechaInicio].S().Dt();
                                                            DateTime dtFinDia = rowsD[rowsD.Length - 1][FechaFin].S().Dt();

                                                            fHoraInicio = dtIniDia.Hour + (dtIniDia.Minute / float.Parse("60"));
                                                            fHoraFinal = dtFinDia.Hour + (dtFinDia.Minute / float.Parse("60"));

                                                            bEsInterInicio = rowsD[k]["PaisPOD"].S() == "MX" ? false : true;

                                                            if (bEsInterInicio != bEsInterFinal)
                                                            {
                                                                #region COMENTADO CONDICONES ANTERIORES
                                                                //if ((fHoraInicio < fInicioCena && fHoraFinal >= fInicioCena) || (fHoraInicio <= fFinCena && fHoraFinal > fInicioCena))
                                                                //{
                                                                //    if (bEsInterFinal)
                                                                //    {
                                                                //        if (l > 0)
                                                                //        {
                                                                //            if (!bEsInterInicio)
                                                                //            {
                                                                //                oCant.iCantDesayunos++;
                                                                //                oCant.iCantComidas++;

                                                                //                oComDia1.iDesayunosNal++;
                                                                //                oComDia1.iComidaNal++;
                                                                //            }
                                                                //        }

                                                                //        oCant.iCantCenasInt++;
                                                                //        oComDia1.iCenaInt++;
                                                                //    }
                                                                //    else
                                                                //    {
                                                                //        if (l > 0)
                                                                //        {
                                                                //            if (bEsInterInicio)
                                                                //            {
                                                                //                oCant.iCantDesayunosInt++;
                                                                //                oCant.iCantComidasInt++;

                                                                //                oComDia1.iDesayunosInt++;
                                                                //                oComDia1.iComidaInt++;
                                                                //            }
                                                                //        }

                                                                //        oCant.iCantCenas++;
                                                                //        oComDia1.iCenaNal++;
                                                                //    }
                                                                //}
                                                                //else if ((fHoraInicio <= fInicioComida && (fHoraFinal > fInicioComida)) || (fHoraInicio > fInicioComida && fHoraInicio <= fFinComida) || (fHoraInicio < fInicioComida && fHoraFinal > fFinComida))
                                                                //{
                                                                //    if (bEsInterFinal)
                                                                //    {
                                                                //        if (l > 0)
                                                                //        {
                                                                //            if (!bEsInterInicio)
                                                                //            {
                                                                //                oCant.iCantDesayunos++;
                                                                //                oComDia1.iDesayunosNal++;
                                                                //            }
                                                                //        }


                                                                //        oCant.iCantComidasInt++;
                                                                //        oCant.iCantCenasInt++;

                                                                //        oComDia1.iComidaInt++;
                                                                //        oComDia1.iCenaInt++;
                                                                //    }
                                                                //    else
                                                                //    {
                                                                //        if (l > 0)
                                                                //        {
                                                                //            if (bEsInterInicio)
                                                                //            {
                                                                //                oCant.iCantDesayunosInt++;
                                                                //                oComDia1.iDesayunosInt++;
                                                                //            }
                                                                //        }

                                                                //        oCant.iCantComidas++;
                                                                //        oCant.iCantCenas++;

                                                                //        oComDia1.iComidaNal++;
                                                                //        oComDia1.iCenaNal++;
                                                                //    }
                                                                //}
                                                                //else if (fHoraInicio < fInicioDesayuno || (fHoraInicio > fInicioDesayuno && fHoraInicio < fFinDesayuno))
                                                                //{
                                                                //    if (bEsInterFinal)
                                                                //    {
                                                                //        oCant.iCantDesayunosInt++;
                                                                //        oCant.iCantComidasInt++;
                                                                //        oCant.iCantCenasInt++;

                                                                //        oComDia1.iDesayunosInt++;
                                                                //        oComDia1.iComidaInt++;
                                                                //        oComDia1.iCenaInt++;
                                                                //    }
                                                                //    else
                                                                //    {
                                                                //        oComDia1.iDesayunosNal++;
                                                                //        oComDia1.iComidaNal++;
                                                                //        oComDia1.iCenaNal++;

                                                                //        oCant.iCantDesayunos++;
                                                                //        oCant.iCantComidas++;
                                                                //        oCant.iCantCenas++;
                                                                //    }
                                                                //}
                                                                #endregion

                                                                #region DESAYUNO
                                                                //Primera condicion
                                                                if (((fHoraInicio < fInicioDesayuno && fHoraFinal > fInicioDesayuno) && fHoraFinal < fFinDesayuno) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                                                                {
                                                                    if (bEsInterFinal)
                                                                    {
                                                                        oCant.iCantDesayunosInt++;
                                                                        //oCant.iCantComidasInt++;
                                                                        //oCant.iCantCenasInt++;

                                                                        oComDia1.iDesayunosInt++;
                                                                        //oComDia1.iComidaInt++;
                                                                        //oComDia1.iCenaInt++;
                                                                    }
                                                                    else
                                                                    {
                                                                        oComDia1.iDesayunosNal++;
                                                                        //oComDia1.iComidaNal++;
                                                                        //oComDia1.iCenaNal++;

                                                                        oCant.iCantDesayunos++;
                                                                        //oCant.iCantComidas++;
                                                                        //oCant.iCantCenas++;
                                                                    }
                                                                }
                                                                //Segunda condición
                                                                else if ((fHoraInicio > fInicioDesayuno && fHoraInicio < fFinDesayuno) && (fHoraFinal > fFinDesayuno && fHoraFinal > fInicioDesayuno) && (oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioComida)) //Evaluar antes de comida
                                                                {
                                                                    if (bEsInterFinal)
                                                                    {
                                                                        oCant.iCantDesayunosInt++;
                                                                        //oCant.iCantComidasInt++;
                                                                        //oCant.iCantCenasInt++;

                                                                        oComDia1.iDesayunosInt++;
                                                                        //oComDia1.iComidaInt++;
                                                                        //oComDia1.iCenaInt++;
                                                                    }
                                                                    else
                                                                    {
                                                                        oComDia1.iDesayunosNal++;
                                                                        //oComDia1.iComidaNal++;
                                                                        //oComDia1.iCenaNal++;

                                                                        oCant.iCantDesayunos++;
                                                                        //oCant.iCantComidas++;
                                                                        //oCant.iCantCenas++;
                                                                    }
                                                                }
                                                                //Tercera condición
                                                                else if ((fHoraInicio >= fInicioDesayuno && fHoraFinal > fFinDesayuno) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioComida))
                                                                {
                                                                    if (bEsInterFinal)
                                                                    {
                                                                        oCant.iCantDesayunosInt++;
                                                                        //oCant.iCantComidasInt++;
                                                                        //oCant.iCantCenasInt++;

                                                                        oComDia1.iDesayunosInt++;
                                                                        //oComDia1.iComidaInt++;
                                                                        //oComDia1.iCenaInt++;
                                                                    }
                                                                    else
                                                                    {
                                                                        oComDia1.iDesayunosNal++;
                                                                        //oComDia1.iComidaNal++;
                                                                        //oComDia1.iCenaNal++;

                                                                        oCant.iCantDesayunos++;
                                                                        //oCant.iCantComidas++;
                                                                        //oCant.iCantCenas++;
                                                                    }
                                                                }
                                                                //Cuarta condición
                                                                else if ((fHoraInicio < fInicioDesayuno && fHoraFinal <= fInicioDesayuno) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                                                                {
                                                                    if (bEsInterFinal)
                                                                    {
                                                                        oCant.iCantDesayunosInt++;
                                                                        //oCant.iCantComidasInt++;
                                                                        //oCant.iCantCenasInt++;

                                                                        oComDia1.iDesayunosInt++;
                                                                        //oComDia1.iComidaInt++;
                                                                        //oComDia1.iCenaInt++;
                                                                    }
                                                                    else
                                                                    {
                                                                        oComDia1.iDesayunosNal++;
                                                                        //oComDia1.iComidaNal++;
                                                                        //oComDia1.iCenaNal++;

                                                                        oCant.iCantDesayunos++;
                                                                        //oCant.iCantComidas++;
                                                                        //oCant.iCantCenas++;
                                                                    }
                                                                }
                                                                //Quinta condición
                                                                else if ((fHoraInicio < fInicioDesayuno && fHoraFinal < fInicioDesayuno) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                                                                {
                                                                    if (bEsInterFinal)
                                                                    {
                                                                        oCant.iCantDesayunosInt++;
                                                                        //oCant.iCantComidasInt++;
                                                                        //oCant.iCantCenasInt++;

                                                                        oComDia1.iDesayunosInt++;
                                                                        //oComDia1.iComidaInt++;
                                                                        //oComDia1.iCenaInt++;
                                                                    }
                                                                    else
                                                                    {
                                                                        oComDia1.iDesayunosNal++;
                                                                        //oComDia1.iComidaNal++;
                                                                        //oComDia1.iCenaNal++;

                                                                        oCant.iCantDesayunos++;
                                                                        //oCant.iCantComidas++;
                                                                        //oCant.iCantCenas++;
                                                                    }
                                                                }
                                                                //Sexta condición
                                                                else if ((fHoraInicio < fInicioDesayuno && (fHoraFinal > fInicioDesayuno && fHoraFinal <= fFinDesayuno)) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                                                                {
                                                                    if (bEsInterFinal)
                                                                    {
                                                                        oCant.iCantDesayunosInt++;
                                                                        //oCant.iCantComidasInt++;
                                                                        //oCant.iCantCenasInt++;

                                                                        oComDia1.iDesayunosInt++;
                                                                        //oComDia1.iComidaInt++;
                                                                        //oComDia1.iCenaInt++;
                                                                    }
                                                                    else
                                                                    {
                                                                        oComDia1.iDesayunosNal++;
                                                                        //oComDia1.iComidaNal++;
                                                                        //oComDia1.iCenaNal++;

                                                                        oCant.iCantDesayunos++;
                                                                        //oCant.iCantComidas++;
                                                                        //oCant.iCantCenas++;
                                                                    }
                                                                }
                                                                //Septima condición
                                                                else if ((fHoraInicio >= fInicioDesayuno && fHoraInicio <= fFinDesayuno) && (fHoraFinal > fInicioDesayuno && fHoraFinal <= fFinDesayuno) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                                                                {
                                                                    if (bEsInterFinal)
                                                                    {
                                                                        oCant.iCantDesayunosInt++;
                                                                        //oCant.iCantComidasInt++;
                                                                        //oCant.iCantCenasInt++;

                                                                        oComDia1.iDesayunosInt++;
                                                                        //oComDia1.iComidaInt++;
                                                                        //oComDia1.iCenaInt++;
                                                                    }
                                                                    else
                                                                    {
                                                                        oComDia1.iDesayunosNal++;
                                                                        //oComDia1.iComidaNal++;
                                                                        //oComDia1.iCenaNal++;

                                                                        oCant.iCantDesayunos++;
                                                                        //oCant.iCantComidas++;
                                                                        //oCant.iCantCenas++;
                                                                    }
                                                                }
                                                                //Octava condición
                                                                else if ((fHoraInicio < fInicioDesayuno && (fHoraFinal > fInicioDesayuno && fHoraFinal >= fFinDesayuno)) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioComida))
                                                                {
                                                                    if (bEsInterFinal)
                                                                    {
                                                                        oCant.iCantDesayunosInt++;
                                                                        //oCant.iCantComidasInt++;
                                                                        //oCant.iCantCenasInt++;

                                                                        oComDia1.iDesayunosInt++;
                                                                        //oComDia1.iComidaInt++;
                                                                        //oComDia1.iCenaInt++;
                                                                    }
                                                                    else
                                                                    {
                                                                        oComDia1.iDesayunosNal++;
                                                                        //oComDia1.iComidaNal++;
                                                                        //oComDia1.iCenaNal++;

                                                                        oCant.iCantDesayunos++;
                                                                        //oCant.iCantComidas++;
                                                                        //oCant.iCantCenas++;
                                                                    }
                                                                }
                                                                #endregion

                                                                #region COMIDA
                                                                //Primera condicion Comidas
                                                                if (((fHoraInicio < fInicioComida && fHoraFinal > fInicioComida) && fHoraFinal < fFinComida) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioCena))
                                                                {
                                                                    if (bEsInterFinal)
                                                                    {
                                                                        //if (l > 0)
                                                                        //{
                                                                        //    if (!bEsInterInicio)
                                                                        //    {
                                                                        //        oCant.iCantDesayunos++;
                                                                        //        oComDia1.iDesayunosNal++;
                                                                        //    }
                                                                        //}


                                                                        oCant.iCantComidasInt++;
                                                                        //oCant.iCantCenasInt++;

                                                                        oComDia1.iComidaInt++;
                                                                        //oComDia1.iCenaInt++;
                                                                    }
                                                                    else
                                                                    {
                                                                        //if (l > 0)
                                                                        //{
                                                                        //    if (bEsInterInicio)
                                                                        //    {
                                                                        //        oCant.iCantDesayunosInt++;
                                                                        //        oComDia1.iDesayunosInt++;
                                                                        //    }
                                                                        //}

                                                                        oCant.iCantComidas++;
                                                                        //oCant.iCantCenas++;

                                                                        oComDia1.iComidaNal++;
                                                                        //oComDia1.iCenaNal++;
                                                                    }
                                                                }
                                                                //Segunda condición Comidas
                                                                else if ((fHoraInicio > fInicioComida && fHoraInicio < fFinComida) && (fHoraFinal > fFinComida && fHoraFinal > fInicioComida) && (oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioCena)) //Evaluar antes de cena
                                                                {
                                                                    if (bEsInterFinal)
                                                                    {
                                                                        //if (l > 0)
                                                                        //{
                                                                        //    if (!bEsInterInicio)
                                                                        //    {
                                                                        //        oCant.iCantDesayunos++;
                                                                        //        oComDia1.iDesayunosNal++;
                                                                        //    }
                                                                        //}


                                                                        oCant.iCantComidasInt++;
                                                                        //oCant.iCantCenasInt++;

                                                                        oComDia1.iComidaInt++;
                                                                        //oComDia1.iCenaInt++;
                                                                    }
                                                                    else
                                                                    {
                                                                        //if (l > 0)
                                                                        //{
                                                                        //    if (bEsInterInicio)
                                                                        //    {
                                                                        //        oCant.iCantDesayunosInt++;
                                                                        //        oComDia1.iDesayunosInt++;
                                                                        //    }
                                                                        //}

                                                                        oCant.iCantComidas++;
                                                                        //oCant.iCantCenas++;

                                                                        oComDia1.iComidaNal++;
                                                                        //oComDia1.iCenaNal++;
                                                                    }
                                                                }
                                                                //Tercera condición Comidas
                                                                else if ((fHoraInicio >= fInicioComida && fHoraFinal > fFinComida) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioCena))
                                                                {
                                                                    if (bEsInterFinal)
                                                                    {
                                                                        //if (l > 0)
                                                                        //{
                                                                        //    if (!bEsInterInicio)
                                                                        //    {
                                                                        //        oCant.iCantDesayunos++;
                                                                        //        oComDia1.iDesayunosNal++;
                                                                        //    }
                                                                        //}


                                                                        oCant.iCantComidasInt++;
                                                                        //oCant.iCantCenasInt++;

                                                                        oComDia1.iComidaInt++;
                                                                        //oComDia1.iCenaInt++;
                                                                    }
                                                                    else
                                                                    {
                                                                        //if (l > 0)
                                                                        //{
                                                                        //    if (bEsInterInicio)
                                                                        //    {
                                                                        //        oCant.iCantDesayunosInt++;
                                                                        //        oComDia1.iDesayunosInt++;
                                                                        //    }
                                                                        //}

                                                                        oCant.iCantComidas++;
                                                                        //oCant.iCantCenas++;

                                                                        oComDia1.iComidaNal++;
                                                                        //oComDia1.iCenaNal++;
                                                                    }
                                                                }
                                                                //Cuarta condición Comidas
                                                                else if ((fHoraInicio < fInicioComida && fHoraFinal <= fInicioComida) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioCena))
                                                                {
                                                                    if (bEsInterFinal)
                                                                    {
                                                                        //if (l > 0)
                                                                        //{
                                                                        //    if (!bEsInterInicio)
                                                                        //    {
                                                                        //        oCant.iCantDesayunos++;
                                                                        //        oComDia1.iDesayunosNal++;
                                                                        //    }
                                                                        //}


                                                                        oCant.iCantComidasInt++;
                                                                        //oCant.iCantCenasInt++;

                                                                        oComDia1.iComidaInt++;
                                                                        //oComDia1.iCenaInt++;
                                                                    }
                                                                    else
                                                                    {
                                                                        //if (l > 0)
                                                                        //{
                                                                        //    if (bEsInterInicio)
                                                                        //    {
                                                                        //        oCant.iCantDesayunosInt++;
                                                                        //        oComDia1.iDesayunosInt++;
                                                                        //    }
                                                                        //}

                                                                        oCant.iCantComidas++;
                                                                        //oCant.iCantCenas++;

                                                                        oComDia1.iComidaNal++;
                                                                        //oComDia1.iCenaNal++;
                                                                    }
                                                                }
                                                                //Quinta condición Comidas
                                                                else if ((fHoraInicio < fInicioComida && fHoraFinal < fInicioComida) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioCena))
                                                                {
                                                                    if (bEsInterFinal)
                                                                    {
                                                                        //if (l > 0)
                                                                        //{
                                                                        //    if (!bEsInterInicio)
                                                                        //    {
                                                                        //        oCant.iCantDesayunos++;
                                                                        //        oComDia1.iDesayunosNal++;
                                                                        //    }
                                                                        //}


                                                                        oCant.iCantComidasInt++;
                                                                        //oCant.iCantCenasInt++;

                                                                        oComDia1.iComidaInt++;
                                                                        //oComDia1.iCenaInt++;
                                                                    }
                                                                    else
                                                                    {
                                                                        //if (l > 0)
                                                                        //{
                                                                        //    if (bEsInterInicio)
                                                                        //    {
                                                                        //        oCant.iCantDesayunosInt++;
                                                                        //        oComDia1.iDesayunosInt++;
                                                                        //    }
                                                                        //}

                                                                        oCant.iCantComidas++;
                                                                        //oCant.iCantCenas++;

                                                                        oComDia1.iComidaNal++;
                                                                        //oComDia1.iCenaNal++;
                                                                    }
                                                                }
                                                                //Sexta condición Comidas
                                                                else if ((fHoraInicio < fInicioComida && (fHoraFinal > fInicioComida && fHoraFinal <= fFinComida)) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioCena))
                                                                {
                                                                    if (bEsInterFinal)
                                                                    {
                                                                        //if (l > 0)
                                                                        //{
                                                                        //    if (!bEsInterInicio)
                                                                        //    {
                                                                        //        oCant.iCantDesayunos++;
                                                                        //        oComDia1.iDesayunosNal++;
                                                                        //    }
                                                                        //}


                                                                        oCant.iCantComidasInt++;
                                                                        //oCant.iCantCenasInt++;

                                                                        oComDia1.iComidaInt++;
                                                                        //oComDia1.iCenaInt++;
                                                                    }
                                                                    else
                                                                    {
                                                                        //if (l > 0)
                                                                        //{
                                                                        //    if (bEsInterInicio)
                                                                        //    {
                                                                        //        oCant.iCantDesayunosInt++;
                                                                        //        oComDia1.iDesayunosInt++;
                                                                        //    }
                                                                        //}

                                                                        oCant.iCantComidas++;
                                                                        //oCant.iCantCenas++;

                                                                        oComDia1.iComidaNal++;
                                                                        //oComDia1.iCenaNal++;
                                                                    }
                                                                }
                                                                //Septima condición Comidas
                                                                else if ((fHoraInicio >= fInicioComida && fHoraInicio <= fFinComida) && (fHoraFinal > fInicioComida && fHoraFinal <= fFinComida) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal > fInicioCena))
                                                                {
                                                                    if (bEsInterFinal)
                                                                    {
                                                                        //if (l > 0)
                                                                        //{
                                                                        //    if (!bEsInterInicio)
                                                                        //    {
                                                                        //        oCant.iCantDesayunos++;
                                                                        //        oComDia1.iDesayunosNal++;
                                                                        //    }
                                                                        //}


                                                                        oCant.iCantComidasInt++;
                                                                        //oCant.iCantCenasInt++;

                                                                        oComDia1.iComidaInt++;
                                                                        //oComDia1.iCenaInt++;
                                                                    }
                                                                    else
                                                                    {
                                                                        //if (l > 0)
                                                                        //{
                                                                        //    if (bEsInterInicio)
                                                                        //    {
                                                                        //        oCant.iCantDesayunosInt++;
                                                                        //        oComDia1.iDesayunosInt++;
                                                                        //    }
                                                                        //}

                                                                        oCant.iCantComidas++;
                                                                        //oCant.iCantCenas++;

                                                                        oComDia1.iComidaNal++;
                                                                        //oComDia1.iCenaNal++;
                                                                    }
                                                                }
                                                                //Octava condición
                                                                else if ((fHoraInicio < fInicioComida && (fHoraFinal > fInicioComida && fHoraFinal >= fFinComida)) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioCena))
                                                                {
                                                                    if (bEsInterFinal)
                                                                    {
                                                                        //if (l > 0)
                                                                        //{
                                                                        //    if (!bEsInterInicio)
                                                                        //    {
                                                                        //        oCant.iCantDesayunos++;
                                                                        //        oComDia1.iDesayunosNal++;
                                                                        //    }
                                                                        //}


                                                                        oCant.iCantComidasInt++;
                                                                        //oCant.iCantCenasInt++;

                                                                        oComDia1.iComidaInt++;
                                                                        //oComDia1.iCenaInt++;
                                                                    }
                                                                    else
                                                                    {
                                                                        //if (l > 0)
                                                                        //{
                                                                        //    if (bEsInterInicio)
                                                                        //    {
                                                                        //        oCant.iCantDesayunosInt++;
                                                                        //        oComDia1.iDesayunosInt++;
                                                                        //    }
                                                                        //}

                                                                        oCant.iCantComidas++;
                                                                        //oCant.iCantCenas++;

                                                                        oComDia1.iComidaNal++;
                                                                        //oComDia1.iCenaNal++;
                                                                    }
                                                                }
                                                                #endregion

                                                                #region CENA
                                                                //Primera condicion Cenas
                                                                if (((fHoraInicio < fInicioCena && fHoraFinal > fInicioCena) && fHoraFinal < fFinCena) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                                                                {
                                                                    if (bEsInterFinal)
                                                                    {
                                                                        //if (l > 0)
                                                                        //{
                                                                        //    if (!bEsInterInicio)
                                                                        //    {
                                                                        //        oCant.iCantDesayunos++;
                                                                        //        oCant.iCantComidas++;

                                                                        //        oComDia1.iDesayunosNal++;
                                                                        //        oComDia1.iComidaNal++;
                                                                        //    }
                                                                        //}

                                                                        oCant.iCantCenasInt++;
                                                                        oComDia1.iCenaInt++;
                                                                    }
                                                                    else
                                                                    {
                                                                        //if (l > 0)
                                                                        //{
                                                                        //    if (bEsInterInicio)
                                                                        //    {
                                                                        //        oCant.iCantDesayunosInt++;
                                                                        //        oCant.iCantComidasInt++;

                                                                        //        oComDia1.iDesayunosInt++;
                                                                        //        oComDia1.iComidaInt++;
                                                                        //    }
                                                                        //}

                                                                        oCant.iCantCenas++;
                                                                        oComDia1.iCenaNal++;
                                                                    }
                                                                }
                                                                //Segunda condición Cenas
                                                                else if ((fHoraInicio > fInicioCena && fHoraInicio < fFinCena) && (fHoraFinal > fFinCena && fHoraFinal > fInicioCena) && (oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                                                                {
                                                                    if (bEsInterFinal)
                                                                    {
                                                                        //if (l > 0)
                                                                        //{
                                                                        //    if (!bEsInterInicio)
                                                                        //    {
                                                                        //        oCant.iCantDesayunos++;
                                                                        //        oCant.iCantComidas++;

                                                                        //        oComDia1.iDesayunosNal++;
                                                                        //        oComDia1.iComidaNal++;
                                                                        //    }
                                                                        //}

                                                                        oCant.iCantCenasInt++;
                                                                        oComDia1.iCenaInt++;
                                                                    }
                                                                    else
                                                                    {
                                                                        //if (l > 0)
                                                                        //{
                                                                        //    if (bEsInterInicio)
                                                                        //    {
                                                                        //        oCant.iCantDesayunosInt++;
                                                                        //        oCant.iCantComidasInt++;

                                                                        //        oComDia1.iDesayunosInt++;
                                                                        //        oComDia1.iComidaInt++;
                                                                        //    }
                                                                        //}

                                                                        oCant.iCantCenas++;
                                                                        oComDia1.iCenaNal++;
                                                                    }
                                                                }
                                                                //Tercera condición Cenas
                                                                else if ((fHoraInicio >= fInicioCena && fHoraFinal > fFinCena) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                                                                {
                                                                    if (bEsInterFinal)
                                                                    {
                                                                        //if (l > 0)
                                                                        //{
                                                                        //    if (!bEsInterInicio)
                                                                        //    {
                                                                        //        oCant.iCantDesayunos++;
                                                                        //        oCant.iCantComidas++;

                                                                        //        oComDia1.iDesayunosNal++;
                                                                        //        oComDia1.iComidaNal++;
                                                                        //    }
                                                                        //}

                                                                        oCant.iCantCenasInt++;
                                                                        oComDia1.iCenaInt++;
                                                                    }
                                                                    else
                                                                    {
                                                                        //if (l > 0)
                                                                        //{
                                                                        //    if (bEsInterInicio)
                                                                        //    {
                                                                        //        oCant.iCantDesayunosInt++;
                                                                        //        oCant.iCantComidasInt++;

                                                                        //        oComDia1.iDesayunosInt++;
                                                                        //        oComDia1.iComidaInt++;
                                                                        //    }
                                                                        //}

                                                                        oCant.iCantCenas++;
                                                                        oComDia1.iCenaNal++;
                                                                    }
                                                                }
                                                                //Cuarta condición Cenas
                                                                else if ((fHoraInicio < fInicioCena && fHoraFinal <= fInicioCena) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                                                                {
                                                                    if (bEsInterFinal)
                                                                    {
                                                                        //if (l > 0)
                                                                        //{
                                                                        //    if (!bEsInterInicio)
                                                                        //    {
                                                                        //        oCant.iCantDesayunos++;
                                                                        //        oCant.iCantComidas++;

                                                                        //        oComDia1.iDesayunosNal++;
                                                                        //        oComDia1.iComidaNal++;
                                                                        //    }
                                                                        //}

                                                                        oCant.iCantCenasInt++;
                                                                        oComDia1.iCenaInt++;
                                                                    }
                                                                    else
                                                                    {
                                                                        //if (l > 0)
                                                                        //{
                                                                        //    if (bEsInterInicio)
                                                                        //    {
                                                                        //        oCant.iCantDesayunosInt++;
                                                                        //        oCant.iCantComidasInt++;

                                                                        //        oComDia1.iDesayunosInt++;
                                                                        //        oComDia1.iComidaInt++;
                                                                        //    }
                                                                        //}

                                                                        oCant.iCantCenas++;
                                                                        oComDia1.iCenaNal++;
                                                                    }
                                                                }
                                                                //Quinta condición Cenas
                                                                else if ((fHoraInicio < fInicioCena && fHoraFinal < fInicioCena) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                                                                {
                                                                    if (bEsInterFinal)
                                                                    {
                                                                        //if (l > 0)
                                                                        //{
                                                                        //    if (!bEsInterInicio)
                                                                        //    {
                                                                        //        oCant.iCantDesayunos++;
                                                                        //        oCant.iCantComidas++;

                                                                        //        oComDia1.iDesayunosNal++;
                                                                        //        oComDia1.iComidaNal++;
                                                                        //    }
                                                                        //}

                                                                        oCant.iCantCenasInt++;
                                                                        oComDia1.iCenaInt++;
                                                                    }
                                                                    else
                                                                    {
                                                                        //if (l > 0)
                                                                        //{
                                                                        //    if (bEsInterInicio)
                                                                        //    {
                                                                        //        oCant.iCantDesayunosInt++;
                                                                        //        oCant.iCantComidasInt++;

                                                                        //        oComDia1.iDesayunosInt++;
                                                                        //        oComDia1.iComidaInt++;
                                                                        //    }
                                                                        //}

                                                                        oCant.iCantCenas++;
                                                                        oComDia1.iCenaNal++;
                                                                    }
                                                                }
                                                                //Sexta condición Cenas
                                                                else if ((fHoraInicio < fInicioCena && (fHoraFinal > fInicioCena && fHoraFinal <= fFinCena)) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                                                                {
                                                                    if (bEsInterFinal)
                                                                    {
                                                                        //if (l > 0)
                                                                        //{
                                                                        //    if (!bEsInterInicio)
                                                                        //    {
                                                                        //        oCant.iCantDesayunos++;
                                                                        //        oCant.iCantComidas++;

                                                                        //        oComDia1.iDesayunosNal++;
                                                                        //        oComDia1.iComidaNal++;
                                                                        //    }
                                                                        //}

                                                                        oCant.iCantCenasInt++;
                                                                        oComDia1.iCenaInt++;
                                                                    }
                                                                    else
                                                                    {
                                                                        //if (l > 0)
                                                                        //{
                                                                        //    if (bEsInterInicio)
                                                                        //    {
                                                                        //        oCant.iCantDesayunosInt++;
                                                                        //        oCant.iCantComidasInt++;

                                                                        //        oComDia1.iDesayunosInt++;
                                                                        //        oComDia1.iComidaInt++;
                                                                        //    }
                                                                        //}

                                                                        oCant.iCantCenas++;
                                                                        oComDia1.iCenaNal++;
                                                                    }
                                                                }
                                                                //Septima condición Cenas
                                                                else if ((fHoraInicio >= fInicioCena && fHoraInicio <= fFinCena) && (fHoraFinal > fInicioCena && fHoraFinal <= fFinCena) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                                                                {
                                                                    if (bEsInterFinal)
                                                                    {
                                                                        //if (l > 0)
                                                                        //{
                                                                        //    if (!bEsInterInicio)
                                                                        //    {
                                                                        //        oCant.iCantDesayunos++;
                                                                        //        oCant.iCantComidas++;

                                                                        //        oComDia1.iDesayunosNal++;
                                                                        //        oComDia1.iComidaNal++;
                                                                        //    }
                                                                        //}

                                                                        oCant.iCantCenasInt++;
                                                                        oComDia1.iCenaInt++;
                                                                    }
                                                                    else
                                                                    {
                                                                        //if (l > 0)
                                                                        //{
                                                                        //    if (bEsInterInicio)
                                                                        //    {
                                                                        //        oCant.iCantDesayunosInt++;
                                                                        //        oCant.iCantComidasInt++;

                                                                        //        oComDia1.iDesayunosInt++;
                                                                        //        oComDia1.iComidaInt++;
                                                                        //    }
                                                                        //}

                                                                        oCant.iCantCenas++;
                                                                        oComDia1.iCenaNal++;
                                                                    }
                                                                }
                                                                //Octava condición Cenas
                                                                else if ((fHoraInicio < fInicioCena && (fHoraFinal > fInicioCena && fHoraFinal >= fFinCena)) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal > fInicioCena))
                                                                {
                                                                    if (bEsInterFinal)
                                                                    {
                                                                        //if (l > 0)
                                                                        //{
                                                                        //    if (!bEsInterInicio)
                                                                        //    {
                                                                        //        oCant.iCantDesayunos++;
                                                                        //        oCant.iCantComidas++;

                                                                        //        oComDia1.iDesayunosNal++;
                                                                        //        oComDia1.iComidaNal++;
                                                                        //    }
                                                                        //}

                                                                        oCant.iCantCenasInt++;
                                                                        oComDia1.iCenaInt++;
                                                                    }
                                                                    else
                                                                    {
                                                                        //if (l > 0)
                                                                        //{
                                                                        //    if (bEsInterInicio)
                                                                        //    {
                                                                        //        oCant.iCantDesayunosInt++;
                                                                        //        oCant.iCantComidasInt++;

                                                                        //        oComDia1.iDesayunosInt++;
                                                                        //        oComDia1.iComidaInt++;
                                                                        //    }
                                                                        //}

                                                                        oCant.iCantCenas++;
                                                                        oComDia1.iCenaNal++;
                                                                    }
                                                                }
                                                                #endregion

                                                            }
                                                            else
                                                            {
                                                                int iAux = 0;

                                                                #region COMENTADO CONDIONES ANTERIORES
                                                                //if (fHoraInicio < fInicioDesayuno || (fHoraInicio > fInicioDesayuno && fHoraInicio < fFinDesayuno))
                                                                //{
                                                                //    if (bEsInterFinal)
                                                                //    {
                                                                //        oCant.iCantDesayunosInt++;
                                                                //        oCant.iCantComidasInt++;
                                                                //        oCant.iCantCenasInt++;

                                                                //        oComDia1.iDesayunosInt++;
                                                                //        oComDia1.iComidaInt++;
                                                                //        oComDia1.iCenaInt++;
                                                                //    }
                                                                //    else
                                                                //    {
                                                                //        oComDia1.iDesayunosNal++;
                                                                //        oComDia1.iComidaNal++;
                                                                //        oComDia1.iCenaNal++;
                                                                //    }

                                                                //    iAux = 1;
                                                                //}
                                                                //else if ((fHoraInicio <= fInicioComida && (fHoraFinal > fInicioComida)) || (fHoraInicio > fInicioComida && fHoraInicio <= fFinComida) || (fHoraInicio < fInicioComida && fHoraFinal > fFinComida))
                                                                //{
                                                                //    // Valida si tiene un día anterior

                                                                //    if (bEsInterFinal)
                                                                //    {
                                                                //        if (l != 0 || l != dtDias.Rows.Count - 1)
                                                                //        {
                                                                //            oCant.iCantDesayunosInt++;
                                                                //            oComDia1.iDesayunosInt++;
                                                                //        }

                                                                //        oCant.iCantComidasInt++;
                                                                //        oCant.iCantCenasInt++;

                                                                //        oComDia1.iComidaInt++;
                                                                //        oComDia1.iCenaInt++;
                                                                //    }
                                                                //    else
                                                                //    {
                                                                //        if (l != 0 || l != dtDias.Rows.Count - 1)
                                                                //        {
                                                                //            oCant.iCantDesayunos++;
                                                                //            oComDia1.iDesayunosNal++;
                                                                //        }

                                                                //        oCant.iCantComidas++;
                                                                //        oCant.iCantCenas++;

                                                                //        oComDia1.iComidaNal++;
                                                                //        oComDia1.iCenaNal++;
                                                                //    }

                                                                //    iAux = 2;
                                                                //}
                                                                //else if ((fHoraInicio < fInicioCena && fHoraFinal >= fInicioCena) || (fHoraInicio <= fFinCena && fHoraFinal > fInicioCena))
                                                                //{
                                                                //    if (bEsInterFinal)
                                                                //    {
                                                                //        if (l != 0 || l != dtDias.Rows.Count - 1)
                                                                //        {
                                                                //            oCant.iCantDesayunosInt++;
                                                                //            oCant.iCantComidasInt++;

                                                                //            oComDia1.iDesayunosInt++;
                                                                //            oComDia1.iComidaInt++;
                                                                //        }

                                                                //        oCant.iCantCenasInt++;
                                                                //        oComDia1.iCenaInt++;
                                                                //    }
                                                                //    else
                                                                //    {
                                                                //        if (l != 0 || l != dtDias.Rows.Count - 1)
                                                                //        {
                                                                //            oCant.iCantDesayunos++;
                                                                //            oCant.iCantComidas++;

                                                                //            oComDia1.iDesayunosNal++;
                                                                //            oComDia1.iComidaNal++;
                                                                //        }

                                                                //        oCant.iCantCenas++;
                                                                //        oComDia1.iCenaNal++;
                                                                //    }

                                                                //    iAux = 3;
                                                                //}
                                                                #endregion

                                                                #region DESAYUNO
                                                                //Primera condicion
                                                                if (((fHoraInicio < fInicioDesayuno && fHoraFinal > fInicioDesayuno) && fHoraFinal < fFinDesayuno) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                                                                {
                                                                    if (bEsInterFinal)
                                                                    {
                                                                        oCant.iCantDesayunosInt++;
                                                                        //oCant.iCantComidasInt++;
                                                                        //oCant.iCantCenasInt++;

                                                                        oComDia1.iDesayunosInt++;
                                                                        //oComDia1.iComidaInt++;
                                                                        //oComDia1.iCenaInt++;
                                                                    }
                                                                    else
                                                                    {
                                                                        oComDia1.iDesayunosNal++;
                                                                        //oComDia1.iComidaNal++;
                                                                        //oComDia1.iCenaNal++;
                                                                    }

                                                                    iAux = 1;
                                                                }
                                                                //Segunda condición
                                                                else if ((fHoraInicio > fInicioDesayuno && fHoraInicio < fFinDesayuno) && (fHoraFinal > fFinDesayuno && fHoraFinal > fInicioDesayuno) && (oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioComida)) //Evaluar antes de comida
                                                                {
                                                                    if (bEsInterFinal)
                                                                    {
                                                                        oCant.iCantDesayunosInt++;
                                                                        //oCant.iCantComidasInt++;
                                                                        //oCant.iCantCenasInt++;

                                                                        oComDia1.iDesayunosInt++;
                                                                        //oComDia1.iComidaInt++;
                                                                        //oComDia1.iCenaInt++;
                                                                    }
                                                                    else
                                                                    {
                                                                        oComDia1.iDesayunosNal++;
                                                                        //oComDia1.iComidaNal++;
                                                                        //oComDia1.iCenaNal++;
                                                                    }

                                                                    iAux = 1;
                                                                }
                                                                //Tercera condición
                                                                else if ((fHoraInicio >= fInicioDesayuno && fHoraFinal > fFinDesayuno) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioComida))
                                                                {
                                                                    if (bEsInterFinal)
                                                                    {
                                                                        oCant.iCantDesayunosInt++;
                                                                        //oCant.iCantComidasInt++;
                                                                        //oCant.iCantCenasInt++;

                                                                        oComDia1.iDesayunosInt++;
                                                                        //oComDia1.iComidaInt++;
                                                                        //oComDia1.iCenaInt++;
                                                                    }
                                                                    else
                                                                    {
                                                                        oComDia1.iDesayunosNal++;
                                                                        //oComDia1.iComidaNal++;
                                                                        //oComDia1.iCenaNal++;
                                                                    }

                                                                    iAux = 1;
                                                                }
                                                                //Cuarta condición
                                                                else if ((fHoraInicio < fInicioDesayuno && fHoraFinal <= fInicioDesayuno) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                                                                {
                                                                    if (bEsInterFinal)
                                                                    {
                                                                        oCant.iCantDesayunosInt++;
                                                                        //oCant.iCantComidasInt++;
                                                                        //oCant.iCantCenasInt++;

                                                                        oComDia1.iDesayunosInt++;
                                                                        //oComDia1.iComidaInt++;
                                                                        //oComDia1.iCenaInt++;
                                                                    }
                                                                    else
                                                                    {
                                                                        oComDia1.iDesayunosNal++;
                                                                        //oComDia1.iComidaNal++;
                                                                        //oComDia1.iCenaNal++;
                                                                    }

                                                                    iAux = 1;
                                                                }
                                                                //Quinta condición
                                                                else if ((fHoraInicio < fInicioDesayuno && fHoraFinal < fInicioDesayuno) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                                                                {
                                                                    if (bEsInterFinal)
                                                                    {
                                                                        oCant.iCantDesayunosInt++;
                                                                        //oCant.iCantComidasInt++;
                                                                        //oCant.iCantCenasInt++;

                                                                        oComDia1.iDesayunosInt++;
                                                                        //oComDia1.iComidaInt++;
                                                                        //oComDia1.iCenaInt++;
                                                                    }
                                                                    else
                                                                    {
                                                                        oComDia1.iDesayunosNal++;
                                                                        //oComDia1.iComidaNal++;
                                                                        //oComDia1.iCenaNal++;
                                                                    }

                                                                    iAux = 1;
                                                                }
                                                                //Sexta condición
                                                                else if ((fHoraInicio < fInicioDesayuno && (fHoraFinal > fInicioDesayuno && fHoraFinal <= fFinDesayuno)) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                                                                {
                                                                    if (bEsInterFinal)
                                                                    {
                                                                        oCant.iCantDesayunosInt++;
                                                                        //oCant.iCantComidasInt++;
                                                                        //oCant.iCantCenasInt++;

                                                                        oComDia1.iDesayunosInt++;
                                                                        //oComDia1.iComidaInt++;
                                                                        //oComDia1.iCenaInt++;
                                                                    }
                                                                    else
                                                                    {
                                                                        oComDia1.iDesayunosNal++;
                                                                        //oComDia1.iComidaNal++;
                                                                        //oComDia1.iCenaNal++;
                                                                    }

                                                                    iAux = 1;
                                                                }
                                                                //Septima condición
                                                                else if ((fHoraInicio >= fInicioDesayuno && fHoraInicio <= fFinDesayuno) && (fHoraFinal > fInicioDesayuno && fHoraFinal <= fFinDesayuno) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                                                                {
                                                                    if (bEsInterFinal)
                                                                    {
                                                                        oCant.iCantDesayunosInt++;
                                                                        //oCant.iCantComidasInt++;
                                                                        //oCant.iCantCenasInt++;

                                                                        oComDia1.iDesayunosInt++;
                                                                        //oComDia1.iComidaInt++;
                                                                        //oComDia1.iCenaInt++;
                                                                    }
                                                                    else
                                                                    {
                                                                        oComDia1.iDesayunosNal++;
                                                                        //oComDia1.iComidaNal++;
                                                                        //oComDia1.iCenaNal++;
                                                                    }

                                                                    iAux = 1;
                                                                }
                                                                //Octava condición
                                                                else if ((fHoraInicio < fInicioDesayuno && (fHoraFinal > fInicioDesayuno && fHoraFinal >= fFinDesayuno)) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioComida))
                                                                {
                                                                    if (bEsInterFinal)
                                                                    {
                                                                        oCant.iCantDesayunosInt++;
                                                                        //oCant.iCantComidasInt++;
                                                                        //oCant.iCantCenasInt++;

                                                                        oComDia1.iDesayunosInt++;
                                                                        //oComDia1.iComidaInt++;
                                                                        //oComDia1.iCenaInt++;
                                                                    }
                                                                    else
                                                                    {
                                                                        oCant.iCantDesayunos++;
                                                                        oComDia1.iDesayunosNal++;
                                                                        //oComDia1.iComidaNal++;
                                                                        //oComDia1.iCenaNal++;
                                                                    }

                                                                    iAux = 1;
                                                                }
                                                                #endregion

                                                                #region COMIDA
                                                                //Primera condicion Comidas
                                                                if (((fHoraInicio < fInicioComida && fHoraFinal > fInicioComida) && fHoraFinal < fFinComida) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioCena))
                                                                {
                                                                    // Valida si tiene un día anterior
                                                                    if (bEsInterFinal)
                                                                    {
                                                                        if (l != 0 || l != dtDias.Rows.Count - 1)
                                                                        {
                                                                            oCant.iCantDesayunosInt++;
                                                                            oComDia1.iDesayunosInt++;
                                                                        }

                                                                        oCant.iCantComidasInt++;
                                                                        //oCant.iCantCenasInt++;

                                                                        oComDia1.iComidaInt++;
                                                                        //oComDia1.iCenaInt++;
                                                                    }
                                                                    else
                                                                    {
                                                                        if (l != 0 || l != dtDias.Rows.Count - 1)
                                                                        {
                                                                            oCant.iCantDesayunos++;
                                                                            oComDia1.iDesayunosNal++;
                                                                        }

                                                                        oCant.iCantComidas++;
                                                                        //oCant.iCantCenas++;

                                                                        oComDia1.iComidaNal++;
                                                                        //oComDia1.iCenaNal++;
                                                                    }

                                                                    iAux = 2;
                                                                }
                                                                //Segunda condición Comidas
                                                                else if ((fHoraInicio > fInicioComida && fHoraInicio < fFinComida) && (fHoraFinal > fFinComida && fHoraFinal > fInicioComida) && (oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioCena)) //Evaluar antes de cena
                                                                {
                                                                    // Valida si tiene un día anterior
                                                                    if (bEsInterFinal)
                                                                    {
                                                                        if (l != 0 || l != dtDias.Rows.Count - 1)
                                                                        {
                                                                            oCant.iCantDesayunosInt++;
                                                                            oComDia1.iDesayunosInt++;
                                                                        }

                                                                        oCant.iCantComidasInt++;
                                                                        //oCant.iCantCenasInt++;

                                                                        oComDia1.iComidaInt++;
                                                                        //oComDia1.iCenaInt++;
                                                                    }
                                                                    else
                                                                    {
                                                                        if (l != 0 || l != dtDias.Rows.Count - 1)
                                                                        {
                                                                            oCant.iCantDesayunos++;
                                                                            oComDia1.iDesayunosNal++;
                                                                        }

                                                                        oCant.iCantComidas++;
                                                                        //oCant.iCantCenas++;

                                                                        oComDia1.iComidaNal++;
                                                                        //oComDia1.iCenaNal++;
                                                                    }

                                                                    iAux = 2;
                                                                }
                                                                //Tercera condición Comidas
                                                                else if ((fHoraInicio >= fInicioComida && fHoraFinal > fFinComida) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioCena))
                                                                {
                                                                    // Valida si tiene un día anterior
                                                                    if (bEsInterFinal)
                                                                    {
                                                                        if (l != 0 || l != dtDias.Rows.Count - 1)
                                                                        {
                                                                            oCant.iCantDesayunosInt++;
                                                                            oComDia1.iDesayunosInt++;
                                                                        }

                                                                        oCant.iCantComidasInt++;
                                                                        //oCant.iCantCenasInt++;

                                                                        oComDia1.iComidaInt++;
                                                                        //oComDia1.iCenaInt++;
                                                                    }
                                                                    else
                                                                    {
                                                                        if (l != 0 || l != dtDias.Rows.Count - 1)
                                                                        {
                                                                            oCant.iCantDesayunos++;
                                                                            oComDia1.iDesayunosNal++;
                                                                        }

                                                                        oCant.iCantComidas++;
                                                                        //oCant.iCantCenas++;

                                                                        oComDia1.iComidaNal++;
                                                                        //oComDia1.iCenaNal++;
                                                                    }

                                                                    iAux = 2;
                                                                }
                                                                //Cuarta condición Comidas
                                                                else if ((fHoraInicio < fInicioComida && fHoraFinal <= fInicioComida) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioCena))
                                                                {
                                                                    // Valida si tiene un día anterior
                                                                    if (bEsInterFinal)
                                                                    {
                                                                        if (l != 0 || l != dtDias.Rows.Count - 1)
                                                                        {
                                                                            oCant.iCantDesayunosInt++;
                                                                            oComDia1.iDesayunosInt++;
                                                                        }

                                                                        oCant.iCantComidasInt++;
                                                                        //oCant.iCantCenasInt++;

                                                                        oComDia1.iComidaInt++;
                                                                        //oComDia1.iCenaInt++;
                                                                    }
                                                                    else
                                                                    {
                                                                        if (l != 0 || l != dtDias.Rows.Count - 1)
                                                                        {
                                                                            oCant.iCantDesayunos++;
                                                                            oComDia1.iDesayunosNal++;
                                                                        }

                                                                        oCant.iCantComidas++;
                                                                        //oCant.iCantCenas++;

                                                                        oComDia1.iComidaNal++;
                                                                        //oComDia1.iCenaNal++;
                                                                    }

                                                                    iAux = 2;
                                                                }
                                                                //Quinta condición Comidas
                                                                else if ((fHoraInicio < fInicioComida && fHoraFinal < fInicioComida) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioCena))
                                                                {
                                                                    // Valida si tiene un día anterior
                                                                    if (bEsInterFinal)
                                                                    {
                                                                        if (l != 0 || l != dtDias.Rows.Count - 1)
                                                                        {
                                                                            oCant.iCantDesayunosInt++;
                                                                            oComDia1.iDesayunosInt++;
                                                                        }

                                                                        oCant.iCantComidasInt++;
                                                                        //oCant.iCantCenasInt++;

                                                                        oComDia1.iComidaInt++;
                                                                        //oComDia1.iCenaInt++;
                                                                    }
                                                                    else
                                                                    {
                                                                        if (l != 0 || l != dtDias.Rows.Count - 1)
                                                                        {
                                                                            oCant.iCantDesayunos++;
                                                                            oComDia1.iDesayunosNal++;
                                                                        }

                                                                        oCant.iCantComidas++;
                                                                        //oCant.iCantCenas++;

                                                                        oComDia1.iComidaNal++;
                                                                        //oComDia1.iCenaNal++;
                                                                    }

                                                                    iAux = 2;
                                                                }
                                                                //Sexta condición Comidas
                                                                else if ((fHoraInicio < fInicioComida && (fHoraFinal > fInicioComida && fHoraFinal <= fFinComida)) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioCena))
                                                                {
                                                                    // Valida si tiene un día anterior
                                                                    if (bEsInterFinal)
                                                                    {
                                                                        if (l != 0 || l != dtDias.Rows.Count - 1)
                                                                        {
                                                                            oCant.iCantDesayunosInt++;
                                                                            oComDia1.iDesayunosInt++;
                                                                        }

                                                                        oCant.iCantComidasInt++;
                                                                        //oCant.iCantCenasInt++;

                                                                        oComDia1.iComidaInt++;
                                                                        //oComDia1.iCenaInt++;
                                                                    }
                                                                    else
                                                                    {
                                                                        if (l != 0 || l != dtDias.Rows.Count - 1)
                                                                        {
                                                                            oCant.iCantDesayunos++;
                                                                            oComDia1.iDesayunosNal++;
                                                                        }

                                                                        oCant.iCantComidas++;
                                                                        //oCant.iCantCenas++;

                                                                        oComDia1.iComidaNal++;
                                                                        //oComDia1.iCenaNal++;
                                                                    }

                                                                    iAux = 2;
                                                                }
                                                                //Septima condición Comidas
                                                                else if ((fHoraInicio >= fInicioComida && fHoraInicio <= fFinComida) && (fHoraFinal > fInicioComida && fHoraFinal <= fFinComida) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioCena))
                                                                {
                                                                    // Valida si tiene un día anterior
                                                                    if (bEsInterFinal)
                                                                    {
                                                                        if (l != 0 || l != dtDias.Rows.Count - 1)
                                                                        {
                                                                            oCant.iCantDesayunosInt++;
                                                                            oComDia1.iDesayunosInt++;
                                                                        }

                                                                        oCant.iCantComidasInt++;
                                                                        //oCant.iCantCenasInt++;

                                                                        oComDia1.iComidaInt++;
                                                                        //oComDia1.iCenaInt++;
                                                                    }
                                                                    else
                                                                    {
                                                                        if (l != 0 || l != dtDias.Rows.Count - 1)
                                                                        {
                                                                            oCant.iCantDesayunos++;
                                                                            oComDia1.iDesayunosNal++;
                                                                        }

                                                                        oCant.iCantComidas++;
                                                                        //oCant.iCantCenas++;

                                                                        oComDia1.iComidaNal++;
                                                                        //oComDia1.iCenaNal++;
                                                                    }

                                                                    iAux = 2;
                                                                }
                                                                //Octava condición
                                                                else if ((fHoraInicio < fInicioComida && (fHoraFinal > fInicioComida && fHoraFinal >= fFinComida)) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal > fInicioCena))
                                                                {
                                                                    // Valida si tiene un día anterior
                                                                    if (bEsInterFinal)
                                                                    {
                                                                        if (l != 0 || l != dtDias.Rows.Count - 1)
                                                                        {
                                                                            //oCant.iCantDesayunosInt++;
                                                                            //oComDia1.iDesayunosInt++;
                                                                        }

                                                                        oCant.iCantComidasInt++;
                                                                        //oCant.iCantCenasInt++;

                                                                        oComDia1.iComidaInt++;
                                                                        //oComDia1.iCenaInt++;
                                                                    }
                                                                    else
                                                                    {
                                                                        if (l != 0 || l != dtDias.Rows.Count - 1)
                                                                        {
                                                                            //oCant.iCantDesayunos++;
                                                                            //oComDia1.iDesayunosNal++;
                                                                        }

                                                                        oCant.iCantComidas++;
                                                                        //oCant.iCantCenas++;

                                                                        oComDia1.iComidaNal++;
                                                                        //oComDia1.iCenaNal++;
                                                                    }

                                                                    iAux = 2;
                                                                }
                                                                #endregion

                                                                #region CENA
                                                                //Primera condicion Cenas
                                                                if (((fHoraInicio < fInicioCena && fHoraFinal > fInicioCena) && fHoraFinal < fFinCena) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                                                                {
                                                                    if (bEsInterFinal)
                                                                    {
                                                                        if (l != 0 || l != dtDias.Rows.Count - 1)
                                                                        {
                                                                            oCant.iCantDesayunosInt++;
                                                                            oCant.iCantComidasInt++;

                                                                            oComDia1.iDesayunosInt++;
                                                                            oComDia1.iComidaInt++;
                                                                        }

                                                                        oCant.iCantCenasInt++;
                                                                        oComDia1.iCenaInt++;
                                                                    }
                                                                    else
                                                                    {
                                                                        if (l != 0 || l != dtDias.Rows.Count - 1)
                                                                        {
                                                                            oCant.iCantDesayunos++;
                                                                            oCant.iCantComidas++;

                                                                            oComDia1.iDesayunosNal++;
                                                                            oComDia1.iComidaNal++;
                                                                        }

                                                                        oCant.iCantCenas++;
                                                                        oComDia1.iCenaNal++;
                                                                    }

                                                                    iAux = 3;
                                                                }
                                                                //Segunda condición Cenas
                                                                else if ((fHoraInicio > fInicioCena && fHoraInicio < fFinCena) && (fHoraFinal > fFinCena && fHoraFinal > fInicioCena) && (oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                                                                {
                                                                    if (bEsInterFinal)
                                                                    {
                                                                        if (l != 0 || l != dtDias.Rows.Count - 1)
                                                                        {
                                                                            oCant.iCantDesayunosInt++;
                                                                            oCant.iCantComidasInt++;

                                                                            oComDia1.iDesayunosInt++;
                                                                            oComDia1.iComidaInt++;
                                                                        }

                                                                        oCant.iCantCenasInt++;
                                                                        oComDia1.iCenaInt++;
                                                                    }
                                                                    else
                                                                    {
                                                                        if (l != 0 || l != dtDias.Rows.Count - 1)
                                                                        {
                                                                            oCant.iCantDesayunos++;
                                                                            oCant.iCantComidas++;

                                                                            oComDia1.iDesayunosNal++;
                                                                            oComDia1.iComidaNal++;
                                                                        }

                                                                        oCant.iCantCenas++;
                                                                        oComDia1.iCenaNal++;
                                                                    }

                                                                    iAux = 3;
                                                                }
                                                                //Tercera condición Cenas
                                                                else if ((fHoraInicio >= fInicioCena && fHoraFinal > fFinCena) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                                                                {
                                                                    if (bEsInterFinal)
                                                                    {
                                                                        if (l != 0 || l != dtDias.Rows.Count - 1)
                                                                        {
                                                                            oCant.iCantDesayunosInt++;
                                                                            oCant.iCantComidasInt++;

                                                                            oComDia1.iDesayunosInt++;
                                                                            oComDia1.iComidaInt++;
                                                                        }

                                                                        oCant.iCantCenasInt++;
                                                                        oComDia1.iCenaInt++;
                                                                    }
                                                                    else
                                                                    {
                                                                        if (l != 0 || l != dtDias.Rows.Count - 1)
                                                                        {
                                                                            oCant.iCantDesayunos++;
                                                                            oCant.iCantComidas++;

                                                                            oComDia1.iDesayunosNal++;
                                                                            oComDia1.iComidaNal++;
                                                                        }

                                                                        oCant.iCantCenas++;
                                                                        oComDia1.iCenaNal++;
                                                                    }

                                                                    iAux = 3;
                                                                }
                                                                //Cuarta condición Cenas
                                                                else if ((fHoraInicio < fInicioCena && fHoraFinal <= fInicioCena) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                                                                {
                                                                    if (bEsInterFinal)
                                                                    {
                                                                        if (l != 0 || l != dtDias.Rows.Count - 1)
                                                                        {
                                                                            oCant.iCantDesayunosInt++;
                                                                            oCant.iCantComidasInt++;

                                                                            oComDia1.iDesayunosInt++;
                                                                            oComDia1.iComidaInt++;
                                                                        }

                                                                        oCant.iCantCenasInt++;
                                                                        oComDia1.iCenaInt++;
                                                                    }
                                                                    else
                                                                    {
                                                                        if (l != 0 || l != dtDias.Rows.Count - 1)
                                                                        {
                                                                            oCant.iCantDesayunos++;
                                                                            oCant.iCantComidas++;

                                                                            oComDia1.iDesayunosNal++;
                                                                            oComDia1.iComidaNal++;
                                                                        }

                                                                        oCant.iCantCenas++;
                                                                        oComDia1.iCenaNal++;
                                                                    }

                                                                    iAux = 3;
                                                                }
                                                                //Quinta condición Cenas
                                                                else if ((fHoraInicio < fInicioCena && fHoraFinal < fInicioCena) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                                                                {
                                                                    if (bEsInterFinal)
                                                                    {
                                                                        if (l != 0 || l != dtDias.Rows.Count - 1)
                                                                        {
                                                                            oCant.iCantDesayunosInt++;
                                                                            oCant.iCantComidasInt++;

                                                                            oComDia1.iDesayunosInt++;
                                                                            oComDia1.iComidaInt++;
                                                                        }

                                                                        oCant.iCantCenasInt++;
                                                                        oComDia1.iCenaInt++;
                                                                    }
                                                                    else
                                                                    {
                                                                        if (l != 0 || l != dtDias.Rows.Count - 1)
                                                                        {
                                                                            oCant.iCantDesayunos++;
                                                                            oCant.iCantComidas++;

                                                                            oComDia1.iDesayunosNal++;
                                                                            oComDia1.iComidaNal++;
                                                                        }

                                                                        oCant.iCantCenas++;
                                                                        oComDia1.iCenaNal++;
                                                                    }

                                                                    iAux = 3;
                                                                }
                                                                //Sexta condición Cenas
                                                                else if ((fHoraInicio < fInicioCena && (fHoraFinal > fInicioCena && fHoraFinal <= fFinCena)) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                                                                {
                                                                    if (bEsInterFinal)
                                                                    {
                                                                        if (l != 0 || l != dtDias.Rows.Count - 1)
                                                                        {
                                                                            //oCant.iCantDesayunosInt++;
                                                                            //oCant.iCantComidasInt++;

                                                                            //oComDia1.iDesayunosInt++;
                                                                            //oComDia1.iComidaInt++;
                                                                        }

                                                                        oCant.iCantCenasInt++;
                                                                        oComDia1.iCenaInt++;
                                                                    }
                                                                    else
                                                                    {
                                                                        if (l != 0 || l != dtDias.Rows.Count - 1)
                                                                        {
                                                                            //oCant.iCantDesayunos++;
                                                                            //oCant.iCantComidas++;

                                                                            //oComDia1.iDesayunosNal++;
                                                                            //oComDia1.iComidaNal++;
                                                                        }

                                                                        oCant.iCantCenas++;
                                                                        oComDia1.iCenaNal++;
                                                                    }

                                                                    iAux = 3;
                                                                }
                                                                //Septima condición Cenas
                                                                else if ((fHoraInicio >= fInicioCena && fHoraInicio <= fFinCena) && (fHoraFinal > fInicioCena && fHoraFinal <= fFinCena) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                                                                {
                                                                    if (bEsInterFinal)
                                                                    {
                                                                        if (l != 0 || l != dtDias.Rows.Count - 1)
                                                                        {
                                                                            oCant.iCantDesayunosInt++;
                                                                            oCant.iCantComidasInt++;

                                                                            oComDia1.iDesayunosInt++;
                                                                            oComDia1.iComidaInt++;
                                                                        }

                                                                        oCant.iCantCenasInt++;
                                                                        oComDia1.iCenaInt++;
                                                                    }
                                                                    else
                                                                    {
                                                                        if (l != 0 || l != dtDias.Rows.Count - 1)
                                                                        {
                                                                            oCant.iCantDesayunos++;
                                                                            oCant.iCantComidas++;

                                                                            oComDia1.iDesayunosNal++;
                                                                            oComDia1.iComidaNal++;
                                                                        }

                                                                        oCant.iCantCenas++;
                                                                        oComDia1.iCenaNal++;
                                                                    }

                                                                    iAux = 3;
                                                                }
                                                                //Octava condición Cenas
                                                                else if ((fHoraInicio < fInicioCena && (fHoraFinal > fInicioCena && fHoraFinal >= fFinCena)) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal > fInicioCena))
                                                                {
                                                                    if (bEsInterFinal)
                                                                    {
                                                                        //if (l != 0 || l != dtDias.Rows.Count - 1)
                                                                        //{
                                                                        //    oCant.iCantDesayunosInt++;
                                                                        //    oCant.iCantComidasInt++;

                                                                        //    oComDia1.iDesayunosInt++;
                                                                        //    oComDia1.iComidaInt++;
                                                                        //}

                                                                        oCant.iCantCenasInt++;
                                                                        oComDia1.iCenaInt++;
                                                                    }
                                                                    else
                                                                    {
                                                                        if (l != 0 || l != dtDias.Rows.Count - 1)
                                                                        {
                                                                            //oCant.iCantDesayunos++;
                                                                            //oCant.iCantComidas++;

                                                                            //oComDia1.iDesayunosNal++;
                                                                            //oComDia1.iComidaNal++;
                                                                        }

                                                                        oCant.iCantCenas++;
                                                                        oComDia1.iCenaNal++;
                                                                    }

                                                                    iAux = 3;
                                                                }
                                                                #endregion

                                                                if (iAux == 0)
                                                                {
                                                                    if (bEsInterInicio)
                                                                    {
                                                                        if (l == 0 || l == dtDias.Rows.Count - 1)
                                                                        {
                                                                            if (fHoraFinal > fFinComida)
                                                                            {
                                                                                oCant.iCantDesayunosInt++;
                                                                                oCant.iCantComidasInt++;

                                                                                oComDia1.iDesayunosInt++;
                                                                                oComDia1.iComidaInt++;
                                                                            }
                                                                            else
                                                                            {
                                                                                oCant.iCantDesayunosInt++;
                                                                                oComDia1.iDesayunosInt++;
                                                                            }
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        if (l == 0 || l == dtDias.Rows.Count - 1)
                                                                        {
                                                                            if (fHoraFinal > fFinComida)
                                                                            {
                                                                                oCant.iCantDesayunos++;
                                                                                oCant.iCantComidas++;

                                                                                oComDia1.iDesayunosNal++;
                                                                                oComDia1.iComidaNal++;
                                                                            }
                                                                            else
                                                                            {
                                                                                oCant.iCantDesayunos++;
                                                                                oComDia1.iDesayunosNal++;
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }


                                                        }

                                                        oLsComDia.Add(oComDia1);
                                                    }
                                                }
                                                else
                                                {
                                                    if (l == 0 || l == dtDias.Rows.Count - 1)
                                                    {
                                                        ComidasPorDia oCom = new ComidasPorDia();
                                                        oCom.sClavePiloto = drP["ClavePiloto"].S();

                                                        dtIni = rowsD[0][FechaInicio].S().Dt();
                                                        dtFin = rowsD[rowsD.Length - 1][FechaFin].S().Dt();

                                                        if (l == 0)
                                                        {
                                                            oCom.dtFechaDia = dtIni;
                                                            oCom.dtFechaFin = dtFin;

                                                            oCom.sduty_type = rowsD[l]["DutyType"].S();
                                                            oCom.sOrigen = rowsD[l]["POD"].S();
                                                            oCom.sDestino = rowsD[l]["POA"].S();
                                                        }
                                                        else
                                                        {
                                                            oCom.dtFechaDia = dtIni;
                                                            oCom.dtFechaFin = dtFin;

                                                            oCom.sduty_type = rowsD[rowsD.Length - 1]["DutyType"].S();
                                                            oCom.sOrigen = rowsD[rowsD.Length - 1]["POD"].S();
                                                            oCom.sDestino = rowsD[rowsD.Length - 1]["POA"].S();
                                                        }


                                                        fHoraInicio = dtIni.Hour + (dtIni.Minute / float.Parse("60"));
                                                        fHoraFinal = dtFin.Hour + (dtFin.Minute / float.Parse("60"));

                                                        bEsInterInicio = rowsD[0]["PaisPOD"].S() == "MX" ? false : true;
                                                        bEsInterFinal = rowsD[rowsD.Length - 1]["EsInternacional"].S() == "1" ? true : false;

                                                        int iAux = 0;

                                                        #region COMENTADO CONDICIONES ANTERIORES
                                                        //if (fHoraInicio < fInicioDesayuno || (fHoraInicio > fInicioDesayuno && fHoraInicio < fFinDesayuno))
                                                        //{
                                                        //    if (bEsInterFinal)
                                                        //    {
                                                        //        oCant.iCantDesayunosInt++;
                                                        //        oCant.iCantComidasInt++;
                                                        //        oCant.iCantCenasInt++;

                                                        //        oCom.iDesayunosInt++;
                                                        //        oCom.iComidaInt++;
                                                        //        oCom.iCenaInt++;
                                                        //    }
                                                        //    else
                                                        //    {
                                                        //        oCom.iDesayunosNal++;
                                                        //        oCom.iComidaNal++;
                                                        //        oCom.iCenaNal++;
                                                        //    }

                                                        //    iAux = 1;
                                                        //}
                                                        //else if ((fHoraInicio <= fInicioComida && (fHoraFinal > fInicioComida)) || (fHoraInicio > fInicioComida && fHoraInicio <= fFinComida) || (fHoraInicio < fInicioComida && fHoraFinal > fFinComida))
                                                        //{
                                                        //    // Valida si tiene un día anterior

                                                        //    if (bEsInterFinal)
                                                        //    {
                                                        //        if (l != 0 || l != dtDias.Rows.Count - 1)
                                                        //        {
                                                        //            oCant.iCantDesayunosInt++;
                                                        //            oCom.iDesayunosInt++;
                                                        //        }

                                                        //        oCant.iCantComidasInt++;
                                                        //        oCant.iCantCenasInt++;

                                                        //        oCom.iComidaInt++;
                                                        //        oCom.iCenaInt++;
                                                        //    }
                                                        //    else
                                                        //    {
                                                        //        if (l != 0 || l != dtDias.Rows.Count - 1)
                                                        //        {
                                                        //            oCant.iCantDesayunos++;
                                                        //            oCom.iDesayunosNal++;
                                                        //        }

                                                        //        oCant.iCantComidas++;
                                                        //        oCant.iCantCenas++;

                                                        //        oCom.iComidaNal++;
                                                        //        oCom.iCenaNal++;
                                                        //    }

                                                        //    iAux = 2;
                                                        //}
                                                        //else if ((fHoraInicio < fInicioCena && fHoraFinal >= fInicioCena) || (fHoraInicio <= fFinCena && fHoraFinal > fInicioCena))
                                                        //{
                                                        //    if (bEsInterFinal)
                                                        //    {
                                                        //        if (l != 0 || l != dtDias.Rows.Count - 1)
                                                        //        {
                                                        //            oCant.iCantDesayunosInt++;
                                                        //            oCant.iCantComidasInt++;

                                                        //            oCom.iDesayunosInt++;
                                                        //            oCom.iComidaInt++;
                                                        //        }

                                                        //        oCant.iCantCenasInt++;
                                                        //        oCom.iCenaInt++;
                                                        //    }
                                                        //    else
                                                        //    {
                                                        //        if (l != 0 || l != dtDias.Rows.Count - 1)
                                                        //        {
                                                        //            oCant.iCantDesayunos++;
                                                        //            oCant.iCantComidas++;

                                                        //            oCom.iDesayunosNal++;
                                                        //            oCom.iComidaNal++;
                                                        //        }

                                                        //        oCant.iCantCenas++;
                                                        //        oCom.iCenaNal++;
                                                        //    }

                                                        //    iAux = 3;
                                                        //}
                                                        #endregion

                                                        #region DESAYUNO
                                                        //Primera condicion
                                                        if (((fHoraInicio < fInicioDesayuno && fHoraFinal > fInicioDesayuno) && fHoraFinal < fFinDesayuno) && (oCom.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                                                        {
                                                            if (bEsInterFinal)
                                                            {
                                                                oCant.iCantDesayunosInt++;
                                                                oCant.iCantComidasInt++;
                                                                oCant.iCantCenasInt++;

                                                                oCom.iDesayunosInt++;
                                                                oCom.iComidaInt++;
                                                                oCom.iCenaInt++;
                                                            }
                                                            else
                                                            {
                                                                oCom.iDesayunosNal++;
                                                                oCom.iComidaNal++;
                                                                oCom.iCenaNal++;
                                                            }

                                                            iAux = 1;
                                                        }
                                                        //Segunda condición
                                                        else if ((fHoraInicio > fInicioDesayuno && fHoraInicio < fFinDesayuno) && (fHoraFinal > fFinDesayuno && fHoraFinal > fInicioDesayuno) && (oCom.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioComida)) //Evaluar antes de comida
                                                        {
                                                            if (bEsInterFinal)
                                                            {
                                                                oCant.iCantDesayunosInt++;
                                                                oCant.iCantComidasInt++;
                                                                oCant.iCantCenasInt++;

                                                                oCom.iDesayunosInt++;
                                                                oCom.iComidaInt++;
                                                                oCom.iCenaInt++;
                                                            }
                                                            else
                                                            {
                                                                oCom.iDesayunosNal++;
                                                                oCom.iComidaNal++;
                                                                oCom.iCenaNal++;
                                                            }

                                                            iAux = 1;
                                                        }
                                                        //Tercera condición
                                                        else if ((fHoraInicio >= fInicioDesayuno && fHoraFinal > fFinDesayuno) && (oCom.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oCom.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioComida))
                                                        {
                                                            if (bEsInterFinal)
                                                            {
                                                                oCant.iCantDesayunosInt++;
                                                                oCant.iCantComidasInt++;
                                                                oCant.iCantCenasInt++;

                                                                oCom.iDesayunosInt++;
                                                                oCom.iComidaInt++;
                                                                oCom.iCenaInt++;
                                                            }
                                                            else
                                                            {
                                                                oCom.iDesayunosNal++;
                                                                oCom.iComidaNal++;
                                                                oCom.iCenaNal++;
                                                            }

                                                            iAux = 1;
                                                        }
                                                        //Cuarta condición
                                                        else if ((fHoraInicio < fInicioDesayuno && fHoraFinal <= fInicioDesayuno) && (oCom.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                                                        {
                                                            if (bEsInterFinal)
                                                            {
                                                                oCant.iCantDesayunosInt++;
                                                                oCant.iCantComidasInt++;
                                                                oCant.iCantCenasInt++;

                                                                oCom.iDesayunosInt++;
                                                                oCom.iComidaInt++;
                                                                oCom.iCenaInt++;
                                                            }
                                                            else
                                                            {
                                                                oCom.iDesayunosNal++;
                                                                oCom.iComidaNal++;
                                                                oCom.iCenaNal++;
                                                            }

                                                            iAux = 1;
                                                        }
                                                        //Quinta condición
                                                        else if ((fHoraInicio < fInicioDesayuno && fHoraFinal < fInicioDesayuno) && (oCom.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                                                        {
                                                            if (bEsInterFinal)
                                                            {
                                                                oCant.iCantDesayunosInt++;
                                                                oCant.iCantComidasInt++;
                                                                oCant.iCantCenasInt++;

                                                                oCom.iDesayunosInt++;
                                                                oCom.iComidaInt++;
                                                                oCom.iCenaInt++;
                                                            }
                                                            else
                                                            {
                                                                oCom.iDesayunosNal++;
                                                                oCom.iComidaNal++;
                                                                oCom.iCenaNal++;
                                                            }

                                                            iAux = 1;
                                                        }
                                                        //Sexta condición
                                                        else if ((fHoraInicio < fInicioDesayuno && (fHoraFinal > fInicioDesayuno && fHoraFinal <= fFinDesayuno)) && (oCom.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oCom.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                                                        {
                                                            if (bEsInterFinal)
                                                            {
                                                                oCant.iCantDesayunosInt++;
                                                                oCant.iCantComidasInt++;
                                                                oCant.iCantCenasInt++;

                                                                oCom.iDesayunosInt++;
                                                                oCom.iComidaInt++;
                                                                oCom.iCenaInt++;
                                                            }
                                                            else
                                                            {
                                                                oCom.iDesayunosNal++;
                                                                oCom.iComidaNal++;
                                                                oCom.iCenaNal++;
                                                            }

                                                            iAux = 1;
                                                        }
                                                        //Septima condición
                                                        else if ((fHoraInicio >= fInicioDesayuno && fHoraInicio <= fFinDesayuno) && (fHoraFinal > fInicioDesayuno && fHoraFinal <= fFinDesayuno) && (oCom.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oCom.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                                                        {
                                                            if (bEsInterFinal)
                                                            {
                                                                oCant.iCantDesayunosInt++;
                                                                oCant.iCantComidasInt++;
                                                                oCant.iCantCenasInt++;

                                                                oCom.iDesayunosInt++;
                                                                oCom.iComidaInt++;
                                                                oCom.iCenaInt++;
                                                            }
                                                            else
                                                            {
                                                                oCom.iDesayunosNal++;
                                                                oCom.iComidaNal++;
                                                                oCom.iCenaNal++;
                                                            }

                                                            iAux = 1;
                                                        }
                                                        //Octava condición
                                                        else if ((fHoraInicio < fInicioDesayuno && (fHoraFinal > fInicioDesayuno && fHoraFinal >= fFinDesayuno)) && (oCom.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oCom.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioComida))
                                                        {
                                                            if (bEsInterFinal)
                                                            {
                                                                oCant.iCantDesayunosInt++;
                                                                oCant.iCantComidasInt++;
                                                                oCant.iCantCenasInt++;

                                                                oCom.iDesayunosInt++;
                                                                oCom.iComidaInt++;
                                                                oCom.iCenaInt++;
                                                            }
                                                            else
                                                            {
                                                                oCom.iDesayunosNal++;
                                                                oCom.iComidaNal++;
                                                                oCom.iCenaNal++;
                                                            }

                                                            iAux = 1;
                                                        }
                                                        #endregion

                                                        #region COMIDA
                                                        //Primera condicion Comidas
                                                        if (((fHoraInicio < fInicioComida && fHoraFinal > fInicioComida) && fHoraFinal < fFinComida) && (oCom.sDestino != dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioCena))
                                                        {
                                                            // Valida si tiene un día anterior
                                                            if (bEsInterFinal)
                                                            {
                                                                if (l != 0 || l != dtDias.Rows.Count - 1)
                                                                {
                                                                    oCant.iCantDesayunosInt++;
                                                                    oCom.iDesayunosInt++;
                                                                }

                                                                oCant.iCantComidasInt++;
                                                                oCant.iCantCenasInt++;

                                                                oCom.iComidaInt++;
                                                                oCom.iCenaInt++;
                                                            }
                                                            else
                                                            {
                                                                if (l != 0 || l != dtDias.Rows.Count - 1)
                                                                {
                                                                    oCant.iCantDesayunos++;
                                                                    oCom.iDesayunosNal++;
                                                                }

                                                                oCant.iCantComidas++;
                                                                oCant.iCantCenas++;

                                                                oCom.iComidaNal++;
                                                                oCom.iCenaNal++;
                                                            }

                                                            iAux = 2;
                                                        }
                                                        //Segunda condición Comidas
                                                        else if ((fHoraInicio > fInicioComida && fHoraInicio < fFinComida) && (fHoraFinal > fFinComida && fHoraFinal > fInicioComida) && (oCom.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioCena)) //Evaluar antes de cena
                                                        {
                                                            // Valida si tiene un día anterior
                                                            if (bEsInterFinal)
                                                            {
                                                                if (l != 0 || l != dtDias.Rows.Count - 1)
                                                                {
                                                                    oCant.iCantDesayunosInt++;
                                                                    oCom.iDesayunosInt++;
                                                                }

                                                                oCant.iCantComidasInt++;
                                                                oCant.iCantCenasInt++;

                                                                oCom.iComidaInt++;
                                                                oCom.iCenaInt++;
                                                            }
                                                            else
                                                            {
                                                                if (l != 0 || l != dtDias.Rows.Count - 1)
                                                                {
                                                                    oCant.iCantDesayunos++;
                                                                    oCom.iDesayunosNal++;
                                                                }

                                                                oCant.iCantComidas++;
                                                                oCant.iCantCenas++;

                                                                oCom.iComidaNal++;
                                                                oCom.iCenaNal++;
                                                            }

                                                            iAux = 2;
                                                        }
                                                        //Tercera condición Comidas
                                                        else if ((fHoraInicio >= fInicioComida && fHoraFinal > fFinComida) && (oCom.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oCom.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioCena))
                                                        {
                                                            // Valida si tiene un día anterior
                                                            if (bEsInterFinal)
                                                            {
                                                                if (l != 0 || l != dtDias.Rows.Count - 1)
                                                                {
                                                                    oCant.iCantDesayunosInt++;
                                                                    oCom.iDesayunosInt++;
                                                                }

                                                                oCant.iCantComidasInt++;
                                                                oCant.iCantCenasInt++;

                                                                oCom.iComidaInt++;
                                                                oCom.iCenaInt++;
                                                            }
                                                            else
                                                            {
                                                                if (l != 0 || l != dtDias.Rows.Count - 1)
                                                                {
                                                                    oCant.iCantDesayunos++;
                                                                    oCom.iDesayunosNal++;
                                                                }

                                                                oCant.iCantComidas++;
                                                                oCant.iCantCenas++;

                                                                oCom.iComidaNal++;
                                                                oCom.iCenaNal++;
                                                            }

                                                            iAux = 2;
                                                        }
                                                        //Cuarta condición Comidas
                                                        else if ((fHoraInicio < fInicioComida && fHoraFinal <= fInicioComida) && (oCom.sDestino != dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioCena))
                                                        {
                                                            // Valida si tiene un día anterior
                                                            if (bEsInterFinal)
                                                            {
                                                                if (l != 0 || l != dtDias.Rows.Count - 1)
                                                                {
                                                                    oCant.iCantDesayunosInt++;
                                                                    oCom.iDesayunosInt++;
                                                                }

                                                                oCant.iCantComidasInt++;
                                                                oCant.iCantCenasInt++;

                                                                oCom.iComidaInt++;
                                                                oCom.iCenaInt++;
                                                            }
                                                            else
                                                            {
                                                                if (l != 0 || l != dtDias.Rows.Count - 1)
                                                                {
                                                                    oCant.iCantDesayunos++;
                                                                    oCom.iDesayunosNal++;
                                                                }

                                                                oCant.iCantComidas++;
                                                                oCant.iCantCenas++;

                                                                oCom.iComidaNal++;
                                                                oCom.iCenaNal++;
                                                            }

                                                            iAux = 2;
                                                        }
                                                        //Quinta condición Comidas
                                                        else if ((fHoraInicio < fInicioComida && fHoraFinal < fInicioComida) && (oCom.sDestino != dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioCena))
                                                        {
                                                            // Valida si tiene un día anterior
                                                            if (bEsInterFinal)
                                                            {
                                                                if (l != 0 || l != dtDias.Rows.Count - 1)
                                                                {
                                                                    oCant.iCantDesayunosInt++;
                                                                    oCom.iDesayunosInt++;
                                                                }

                                                                oCant.iCantComidasInt++;
                                                                oCant.iCantCenasInt++;

                                                                oCom.iComidaInt++;
                                                                oCom.iCenaInt++;
                                                            }
                                                            else
                                                            {
                                                                if (l != 0 || l != dtDias.Rows.Count - 1)
                                                                {
                                                                    oCant.iCantDesayunos++;
                                                                    oCom.iDesayunosNal++;
                                                                }

                                                                oCant.iCantComidas++;
                                                                oCant.iCantCenas++;

                                                                oCom.iComidaNal++;
                                                                oCom.iCenaNal++;
                                                            }

                                                            iAux = 2;
                                                        }
                                                        //Sexta condición Comidas
                                                        else if ((fHoraInicio < fInicioComida && (fHoraFinal > fInicioComida && fHoraFinal <= fFinComida)) && (oCom.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oCom.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioCena))
                                                        {
                                                            // Valida si tiene un día anterior
                                                            if (bEsInterFinal)
                                                            {
                                                                if (l != 0 || l != dtDias.Rows.Count - 1)
                                                                {
                                                                    oCant.iCantDesayunosInt++;
                                                                    oCom.iDesayunosInt++;
                                                                }

                                                                oCant.iCantComidasInt++;
                                                                oCant.iCantCenasInt++;

                                                                oCom.iComidaInt++;
                                                                oCom.iCenaInt++;
                                                            }
                                                            else
                                                            {
                                                                if (l != 0 || l != dtDias.Rows.Count - 1)
                                                                {
                                                                    oCant.iCantDesayunos++;
                                                                    oCom.iDesayunosNal++;
                                                                }

                                                                oCant.iCantComidas++;
                                                                oCant.iCantCenas++;

                                                                oCom.iComidaNal++;
                                                                oCom.iCenaNal++;
                                                            }

                                                            iAux = 2;
                                                        }
                                                        //Septima condición Comidas
                                                        else if ((fHoraInicio >= fInicioComida && fHoraInicio <= fFinComida) && (fHoraFinal > fInicioComida && fHoraFinal <= fFinComida) && (oCom.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oCom.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioCena))
                                                        {
                                                            // Valida si tiene un día anterior
                                                            if (bEsInterFinal)
                                                            {
                                                                if (l != 0 || l != dtDias.Rows.Count - 1)
                                                                {
                                                                    oCant.iCantDesayunosInt++;
                                                                    oCom.iDesayunosInt++;
                                                                }

                                                                oCant.iCantComidasInt++;
                                                                oCant.iCantCenasInt++;

                                                                oCom.iComidaInt++;
                                                                oCom.iCenaInt++;
                                                            }
                                                            else
                                                            {
                                                                if (l != 0 || l != dtDias.Rows.Count - 1)
                                                                {
                                                                    oCant.iCantDesayunos++;
                                                                    oCom.iDesayunosNal++;
                                                                }

                                                                oCant.iCantComidas++;
                                                                oCant.iCantCenas++;

                                                                oCom.iComidaNal++;
                                                                oCom.iCenaNal++;
                                                            }

                                                            iAux = 2;
                                                        }
                                                        //Octava condición
                                                        else if ((fHoraInicio < fInicioComida && (fHoraFinal > fInicioComida && fHoraFinal >= fFinComida)) && (oCom.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oCom.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioCena))
                                                        {
                                                            // Valida si tiene un día anterior
                                                            if (bEsInterFinal)
                                                            {
                                                                if (l != 0 || l != dtDias.Rows.Count - 1)
                                                                {
                                                                    oCant.iCantDesayunosInt++;
                                                                    oCom.iDesayunosInt++;
                                                                }

                                                                oCant.iCantComidasInt++;
                                                                oCant.iCantCenasInt++;

                                                                oCom.iComidaInt++;
                                                                oCom.iCenaInt++;
                                                            }
                                                            else
                                                            {
                                                                if (l != 0 || l != dtDias.Rows.Count - 1)
                                                                {
                                                                    oCant.iCantDesayunos++;
                                                                    oCom.iDesayunosNal++;
                                                                }

                                                                oCant.iCantComidas++;
                                                                oCant.iCantCenas++;

                                                                oCom.iComidaNal++;
                                                                oCom.iCenaNal++;
                                                            }

                                                            iAux = 2;
                                                        }
                                                        #endregion

                                                        #region CENA
                                                        //Primera condicion Cenas
                                                        if (((fHoraInicio < fInicioCena && fHoraFinal > fInicioCena) && fHoraFinal < fFinCena) && (oCom.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                                                        {
                                                            if (bEsInterFinal)
                                                            {
                                                                if (l != 0 || l != dtDias.Rows.Count - 1)
                                                                {
                                                                    oCant.iCantDesayunosInt++;
                                                                    oCant.iCantComidasInt++;

                                                                    oCom.iDesayunosInt++;
                                                                    oCom.iComidaInt++;
                                                                }

                                                                oCant.iCantCenasInt++;
                                                                oCom.iCenaInt++;
                                                            }
                                                            else
                                                            {
                                                                if (l != 0 || l != dtDias.Rows.Count - 1)
                                                                {
                                                                    oCant.iCantDesayunos++;
                                                                    oCant.iCantComidas++;

                                                                    oCom.iDesayunosNal++;
                                                                    oCom.iComidaNal++;
                                                                }

                                                                oCant.iCantCenas++;
                                                                oCom.iCenaNal++;
                                                            }

                                                            iAux = 3;
                                                        }
                                                        //Segunda condición Cenas
                                                        else if ((fHoraInicio > fInicioCena && fHoraInicio < fFinCena) && (fHoraFinal > fFinCena && fHoraFinal > fInicioCena) && (oCom.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                                                        {
                                                            if (bEsInterFinal)
                                                            {
                                                                if (l != 0 || l != dtDias.Rows.Count - 1)
                                                                {
                                                                    oCant.iCantDesayunosInt++;
                                                                    oCant.iCantComidasInt++;

                                                                    oCom.iDesayunosInt++;
                                                                    oCom.iComidaInt++;
                                                                }

                                                                oCant.iCantCenasInt++;
                                                                oCom.iCenaInt++;
                                                            }
                                                            else
                                                            {
                                                                if (l != 0 || l != dtDias.Rows.Count - 1)
                                                                {
                                                                    oCant.iCantDesayunos++;
                                                                    oCant.iCantComidas++;

                                                                    oCom.iDesayunosNal++;
                                                                    oCom.iComidaNal++;
                                                                }

                                                                oCant.iCantCenas++;
                                                                oCom.iCenaNal++;
                                                            }

                                                            iAux = 3;
                                                        }
                                                        //Tercera condición Cenas
                                                        else if ((fHoraInicio >= fInicioCena && fHoraFinal > fFinCena) && (oCom.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oCom.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                                                        {
                                                            if (bEsInterFinal)
                                                            {
                                                                if (l != 0 || l != dtDias.Rows.Count - 1)
                                                                {
                                                                    oCant.iCantDesayunosInt++;
                                                                    oCant.iCantComidasInt++;

                                                                    oCom.iDesayunosInt++;
                                                                    oCom.iComidaInt++;
                                                                }

                                                                oCant.iCantCenasInt++;
                                                                oCom.iCenaInt++;
                                                            }
                                                            else
                                                            {
                                                                if (l != 0 || l != dtDias.Rows.Count - 1)
                                                                {
                                                                    oCant.iCantDesayunos++;
                                                                    oCant.iCantComidas++;

                                                                    oCom.iDesayunosNal++;
                                                                    oCom.iComidaNal++;
                                                                }

                                                                oCant.iCantCenas++;
                                                                oCom.iCenaNal++;
                                                            }

                                                            iAux = 3;
                                                        }
                                                        //Cuarta condición Cenas
                                                        else if ((fHoraInicio < fInicioCena && fHoraFinal <= fInicioCena) && (oCom.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                                                        {
                                                            if (bEsInterFinal)
                                                            {
                                                                if (l != 0 || l != dtDias.Rows.Count - 1)
                                                                {
                                                                    oCant.iCantDesayunosInt++;
                                                                    oCant.iCantComidasInt++;

                                                                    oCom.iDesayunosInt++;
                                                                    oCom.iComidaInt++;
                                                                }

                                                                oCant.iCantCenasInt++;
                                                                oCom.iCenaInt++;
                                                            }
                                                            else
                                                            {
                                                                if (l != 0 || l != dtDias.Rows.Count - 1)
                                                                {
                                                                    oCant.iCantDesayunos++;
                                                                    oCant.iCantComidas++;

                                                                    oCom.iDesayunosNal++;
                                                                    oCom.iComidaNal++;
                                                                }

                                                                oCant.iCantCenas++;
                                                                oCom.iCenaNal++;
                                                            }

                                                            iAux = 3;
                                                        }
                                                        //Quinta condición Cenas
                                                        else if ((fHoraInicio < fInicioCena && fHoraFinal < fInicioCena) && (oCom.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                                                        {
                                                            if (bEsInterFinal)
                                                            {
                                                                if (l != 0 || l != dtDias.Rows.Count - 1)
                                                                {
                                                                    oCant.iCantDesayunosInt++;
                                                                    oCant.iCantComidasInt++;

                                                                    oCom.iDesayunosInt++;
                                                                    oCom.iComidaInt++;
                                                                }

                                                                oCant.iCantCenasInt++;
                                                                oCom.iCenaInt++;
                                                            }
                                                            else
                                                            {
                                                                if (l != 0 || l != dtDias.Rows.Count - 1)
                                                                {
                                                                    oCant.iCantDesayunos++;
                                                                    oCant.iCantComidas++;

                                                                    oCom.iDesayunosNal++;
                                                                    oCom.iComidaNal++;
                                                                }

                                                                oCant.iCantCenas++;
                                                                oCom.iCenaNal++;
                                                            }

                                                            iAux = 3;
                                                        }
                                                        //Sexta condición Cenas
                                                        else if ((fHoraInicio < fInicioCena && (fHoraFinal > fInicioCena && fHoraFinal <= fFinCena)) && (oCom.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oCom.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                                                        {
                                                            if (bEsInterFinal)
                                                            {
                                                                if (l != 0 || l != dtDias.Rows.Count - 1)
                                                                {
                                                                    oCant.iCantDesayunosInt++;
                                                                    oCant.iCantComidasInt++;

                                                                    oCom.iDesayunosInt++;
                                                                    oCom.iComidaInt++;
                                                                }

                                                                oCant.iCantCenasInt++;
                                                                oCom.iCenaInt++;
                                                            }
                                                            else
                                                            {
                                                                if (l != 0 || l != dtDias.Rows.Count - 1)
                                                                {
                                                                    oCant.iCantDesayunos++;
                                                                    oCant.iCantComidas++;

                                                                    oCom.iDesayunosNal++;
                                                                    oCom.iComidaNal++;
                                                                }

                                                                oCant.iCantCenas++;
                                                                oCom.iCenaNal++;
                                                            }

                                                            iAux = 3;
                                                        }
                                                        //Septima condición Cenas
                                                        else if ((fHoraInicio >= fInicioCena && fHoraInicio <= fFinCena) && (fHoraFinal > fInicioCena && fHoraFinal <= fFinCena) && (oCom.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oCom.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                                                        {
                                                            if (bEsInterFinal)
                                                            {
                                                                if (l != 0 || l != dtDias.Rows.Count - 1)
                                                                {
                                                                    oCant.iCantDesayunosInt++;
                                                                    oCant.iCantComidasInt++;

                                                                    oCom.iDesayunosInt++;
                                                                    oCom.iComidaInt++;
                                                                }

                                                                oCant.iCantCenasInt++;
                                                                oCom.iCenaInt++;
                                                            }
                                                            else
                                                            {
                                                                if (l != 0 || l != dtDias.Rows.Count - 1)
                                                                {
                                                                    oCant.iCantDesayunos++;
                                                                    oCant.iCantComidas++;

                                                                    oCom.iDesayunosNal++;
                                                                    oCom.iComidaNal++;
                                                                }

                                                                oCant.iCantCenas++;
                                                                oCom.iCenaNal++;
                                                            }

                                                            iAux = 3;
                                                        }
                                                        //Octava condición Cenas
                                                        else if ((fHoraInicio < fInicioCena && (fHoraFinal > fInicioCena && fHoraFinal >= fFinCena)) && (oCom.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oCom.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                                                        {
                                                            if (bEsInterFinal)
                                                            {
                                                                if (l != 0 || l != dtDias.Rows.Count - 1)
                                                                {
                                                                    oCant.iCantDesayunosInt++;
                                                                    oCant.iCantComidasInt++;

                                                                    oCom.iDesayunosInt++;
                                                                    oCom.iComidaInt++;
                                                                }

                                                                oCant.iCantCenasInt++;
                                                                oCom.iCenaInt++;
                                                            }
                                                            else
                                                            {
                                                                if (l != 0 || l != dtDias.Rows.Count - 1)
                                                                {
                                                                    oCant.iCantDesayunos++;
                                                                    oCant.iCantComidas++;

                                                                    oCom.iDesayunosNal++;
                                                                    oCom.iComidaNal++;
                                                                }

                                                                oCant.iCantCenas++;
                                                                oCom.iCenaNal++;
                                                            }

                                                            iAux = 3;
                                                        }
                                                        #endregion

                                                        if (iAux == 0)
                                                        {
                                                            if (bEsInterInicio)
                                                            {
                                                                if (l == 0 || l == dtDias.Rows.Count - 1)
                                                                {
                                                                    if (fHoraFinal > fFinComida)
                                                                    {
                                                                        oCant.iCantDesayunosInt++;
                                                                        oCant.iCantComidasInt++;

                                                                        oCom.iDesayunosInt++;
                                                                        oCom.iComidaInt++;
                                                                    }
                                                                    else
                                                                    {
                                                                        oCant.iCantDesayunosInt++;
                                                                        oCom.iDesayunosInt++;
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (l == 0 || l == dtDias.Rows.Count - 1)
                                                                {
                                                                    if (fHoraFinal > fFinComida)
                                                                    {
                                                                        oCant.iCantDesayunos++;
                                                                        oCant.iCantComidas++;

                                                                        oCom.iDesayunosNal++;
                                                                        oCom.iComidaNal++;
                                                                    }
                                                                    else
                                                                    {
                                                                        oCant.iCantDesayunos++;
                                                                        oCom.iDesayunosNal++;
                                                                    }
                                                                }
                                                            }
                                                        }

                                                        #region COMENTADO
                                                        //if (fHoraInicio < fInicioDesayuno || (fHoraInicio > fInicioDesayuno && fHoraInicio < fFinDesayuno) || (fHoraInicio >= fInicioDesayuno && rowsD[0]["POD"].S() != sBase))
                                                        //{
                                                        //    if (bEsInterFinal)
                                                        //    {
                                                        //        oCant.iCantDesayunosInt++;

                                                        //        oCom.iDesayunosInt++;
                                                        //    }
                                                        //    else
                                                        //    {
                                                        //        oCant.iCantDesayunos++;

                                                        //        oCom.iDesayunosNal++;
                                                        //    }

                                                        //    iAux = 1;
                                                        //}
                                                        //if ((fHoraInicio <= fInicioComida && (fHoraFinal > fInicioComida)) || (fHoraInicio > fInicioComida && fHoraInicio <= fFinComida) || (fHoraInicio < fInicioComida && fHoraFinal > fFinComida))
                                                        //{
                                                        //    if (bEsInterFinal)
                                                        //    {
                                                        //        oCant.iCantComidasInt++;

                                                        //        oCom.iComidaInt++;
                                                        //    }
                                                        //    else
                                                        //    {
                                                        //        oCant.iCantComidas++;

                                                        //        oCom.iComidaNal++;
                                                        //    }

                                                        //    iAux = 2;
                                                        //}
                                                        //if ((fHoraInicio < fInicioCena && fHoraFinal > fInicioCena) || (fHoraInicio <= fFinCena && fHoraFinal > fInicioCena))
                                                        //{
                                                        //    if (bEsInterFinal)
                                                        //    {
                                                        //        oCant.iCantCenasInt++;

                                                        //        oCom.iCenaInt++;
                                                        //    }
                                                        //    else
                                                        //    {
                                                        //        oCant.iCantCenas++;

                                                        //        oCom.iCenaNal++;
                                                        //    }

                                                        //    iAux = 3;
                                                        //}

                                                        //if (iAux == 1)
                                                        //{
                                                        //    if (l != dtDias.Rows.Count - 1)
                                                        //    {
                                                        //        if (bEsInterFinal)
                                                        //        {
                                                        //            oCant.iCantComidasInt++;
                                                        //            oCant.iCantCenasInt++;

                                                        //            oCom.iCenaInt++;
                                                        //            oCom.iComidaInt++;
                                                        //        }
                                                        //        else
                                                        //        {
                                                        //            oCant.iCantCenas++;
                                                        //            oCant.iCantComidas++;

                                                        //            oCom.iCenaNal++;
                                                        //            oCom.iComidaNal++;
                                                        //        }
                                                        //    }
                                                        //}
                                                        //if (iAux == 2)
                                                        //{
                                                        //    if (l != dtDias.Rows.Count - 1)
                                                        //    {
                                                        //        if (bEsInterFinal)
                                                        //        {
                                                        //            oCant.iCantCenasInt++;
                                                        //            oCom.iCenaInt++;
                                                        //        }
                                                        //        else
                                                        //        {
                                                        //            oCant.iCantCenas++;
                                                        //            oCom.iCenaNal++;
                                                        //        }
                                                        //    }
                                                        //}
                                                        #endregion

                                                        oLsComDia.Add(oCom);
                                                    }
                                                    else
                                                    {
                                                        DataTable dtDias2 = dtDias;
                                                        DateTime dtIniAux = rowsD[0][FechaInicio].S().Dt();
                                                        DateTime dtFinAux = rowsD[0][FechaFin].S().Dt();

                                                        bEsInterInicio = rowsD[0]["PaisPOD"].S() == "MX" ? false : true;

                                                        ComidasPorDia oCom = new ComidasPorDia();
                                                        oCom.sClavePiloto = drP["ClavePiloto"].S();
                                                        oCom.dtFechaDia = dtIniAux;// dtIni.AddDays(l);
                                                        oCom.dtFechaFin = dtFinAux;// dtFin.AddDays(l);
                                                        oCom.sduty_type = rowsD[0]["DutyType"].S();
                                                        oCom.sOrigen = rowsD[0]["POD"].S();
                                                        oCom.sDestino = rowsD[0]["POA"].S();


                                                        if (bEsInterInicio != bEsInterFinal)
                                                        {
                                                            #region COMENTADO CONDICIONES ANTERIORES
                                                            //if ((fHoraInicio < fInicioCena && fHoraFinal >= fInicioCena) || (fHoraInicio <= fFinCena && fHoraFinal > fInicioCena))
                                                            //{
                                                            //    if (bEsInterFinal)
                                                            //    {
                                                            //        if (l > 0)
                                                            //        {
                                                            //            if (!bEsInterInicio)
                                                            //            {
                                                            //                oCant.iCantDesayunos++;
                                                            //                oCant.iCantComidas++;

                                                            //                oCom.iDesayunosNal++;
                                                            //                oCom.iComidaNal++;
                                                            //            }
                                                            //        }

                                                            //        oCant.iCantCenasInt++;
                                                            //        oCom.iCenaInt++;
                                                            //    }
                                                            //    else
                                                            //    {
                                                            //        if (l > 0)
                                                            //        {
                                                            //            if (bEsInterInicio)
                                                            //            {
                                                            //                oCant.iCantDesayunosInt++;
                                                            //                oCant.iCantComidasInt++;

                                                            //                oCom.iDesayunosInt++;
                                                            //                oCom.iComidaInt++;
                                                            //            }
                                                            //        }

                                                            //        oCant.iCantCenas++;
                                                            //        oCom.iCenaNal++;
                                                            //    }
                                                            //}
                                                            //else if ((fHoraInicio <= fInicioComida && (fHoraFinal > fInicioComida)) || (fHoraInicio > fInicioComida && fHoraInicio <= fFinComida) || (fHoraInicio < fInicioComida && fHoraFinal > fFinComida))
                                                            //{
                                                            //    if (bEsInterFinal)
                                                            //    {
                                                            //        if (l > 0)
                                                            //        {
                                                            //            if (!bEsInterInicio)
                                                            //            {
                                                            //                oCant.iCantDesayunos++;
                                                            //                oCom.iDesayunosNal++;
                                                            //            }
                                                            //        }


                                                            //        oCant.iCantComidasInt++;
                                                            //        oCant.iCantCenasInt++;

                                                            //        oCom.iComidaInt++;
                                                            //        oCom.iCenaInt++;
                                                            //    }
                                                            //    else
                                                            //    {
                                                            //        if (l > 0)
                                                            //        {
                                                            //            if (bEsInterInicio)
                                                            //            {
                                                            //                oCant.iCantDesayunosInt++;
                                                            //                oCom.iDesayunosInt++;
                                                            //            }
                                                            //        }

                                                            //        oCant.iCantComidas++;
                                                            //        oCant.iCantCenas++;

                                                            //        oCom.iComidaNal++;
                                                            //        oCom.iCenaNal++;
                                                            //    }
                                                            //}
                                                            //else if (fHoraInicio < fInicioDesayuno || (fHoraInicio > fInicioDesayuno && fHoraInicio < fFinDesayuno))
                                                            //{
                                                            //    if (bEsInterFinal)
                                                            //    {
                                                            //        oCant.iCantDesayunosInt++;
                                                            //        oCant.iCantComidasInt++;
                                                            //        oCant.iCantCenasInt++;

                                                            //        oCom.iDesayunosInt++;
                                                            //        oCom.iComidaInt++;
                                                            //        oCom.iCenaInt++;
                                                            //    }
                                                            //    else
                                                            //    {
                                                            //        oCom.iDesayunosNal++;
                                                            //        oCom.iComidaNal++;
                                                            //        oCom.iCenaNal++;

                                                            //        oCant.iCantDesayunos++;
                                                            //        oCant.iCantComidas++;
                                                            //        oCant.iCantCenas++;
                                                            //    }
                                                            //}
                                                            #endregion

                                                            #region DESAYUNO
                                                            //Primera condicion
                                                            if (((fHoraInicio < fInicioDesayuno && fHoraFinal > fInicioDesayuno) && fHoraFinal < fFinDesayuno) && (oCom.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                                                            {
                                                                if (bEsInterFinal)
                                                                {
                                                                    oCant.iCantDesayunosInt++;
                                                                    //oCant.iCantComidasInt++;
                                                                    //oCant.iCantCenasInt++;

                                                                    oCom.iDesayunosInt++;
                                                                    //oCom.iComidaInt++;
                                                                    //oCom.iCenaInt++;
                                                                }
                                                                else
                                                                {
                                                                    oCom.iDesayunosNal++;
                                                                    //oCom.iComidaNal++;
                                                                    //oCom.iCenaNal++;

                                                                    oCant.iCantDesayunos++;
                                                                    //oCant.iCantComidas++;
                                                                    //oCant.iCantCenas++;
                                                                }
                                                            }
                                                            //Segunda condición
                                                            else if ((fHoraInicio > fInicioDesayuno && fHoraInicio < fFinDesayuno) && (fHoraFinal > fFinDesayuno && fHoraFinal > fInicioDesayuno) && (oCom.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioComida)) //Evaluar antes de comida
                                                            {
                                                                if (bEsInterFinal)
                                                                {
                                                                    oCant.iCantDesayunosInt++;
                                                                    //oCant.iCantComidasInt++;
                                                                    //oCant.iCantCenasInt++;

                                                                    oCom.iDesayunosInt++;
                                                                    //oCom.iComidaInt++;
                                                                    //oCom.iCenaInt++;
                                                                }
                                                                else
                                                                {
                                                                    oCom.iDesayunosNal++;
                                                                    //oCom.iComidaNal++;
                                                                    //oCom.iCenaNal++;

                                                                    oCant.iCantDesayunos++;
                                                                    //oCant.iCantComidas++;
                                                                    //oCant.iCantCenas++;
                                                                }
                                                            }
                                                            //Tercera condición
                                                            else if ((fHoraInicio >= fInicioDesayuno && fHoraFinal > fFinDesayuno) && (oCom.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oCom.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioComida))
                                                            {
                                                                if (bEsInterFinal)
                                                                {
                                                                    oCant.iCantDesayunosInt++;
                                                                    //oCant.iCantComidasInt++;
                                                                    //oCant.iCantCenasInt++;

                                                                    oCom.iDesayunosInt++;
                                                                    //oCom.iComidaInt++;
                                                                    //oCom.iCenaInt++;
                                                                }
                                                                else
                                                                {
                                                                    oCom.iDesayunosNal++;
                                                                    //oCom.iComidaNal++;
                                                                    //oCom.iCenaNal++;

                                                                    oCant.iCantDesayunos++;
                                                                    //oCant.iCantComidas++;
                                                                    //oCant.iCantCenas++;
                                                                }
                                                            }
                                                            //Cuarta condición
                                                            else if ((fHoraInicio < fInicioDesayuno && fHoraFinal <= fInicioDesayuno) && (oCom.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                                                            {
                                                                if (bEsInterFinal)
                                                                {
                                                                    oCant.iCantDesayunosInt++;
                                                                    //oCant.iCantComidasInt++;
                                                                    //oCant.iCantCenasInt++;

                                                                    oCom.iDesayunosInt++;
                                                                    //oCom.iComidaInt++;
                                                                    //oCom.iCenaInt++;
                                                                }
                                                                else
                                                                {
                                                                    oCom.iDesayunosNal++;
                                                                    //oCom.iComidaNal++;
                                                                    //oCom.iCenaNal++;

                                                                    oCant.iCantDesayunos++;
                                                                    //oCant.iCantComidas++;
                                                                    //oCant.iCantCenas++;
                                                                }
                                                            }
                                                            //Quinta condición
                                                            else if ((fHoraInicio < fInicioDesayuno && fHoraFinal < fInicioDesayuno) && (oCom.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                                                            {
                                                                if (bEsInterFinal)
                                                                {
                                                                    oCant.iCantDesayunosInt++;
                                                                    //oCant.iCantComidasInt++;
                                                                    //oCant.iCantCenasInt++;

                                                                    oCom.iDesayunosInt++;
                                                                    //oCom.iComidaInt++;
                                                                    //oCom.iCenaInt++;
                                                                }
                                                                else
                                                                {
                                                                    oCom.iDesayunosNal++;
                                                                    //oCom.iComidaNal++;
                                                                    //oCom.iCenaNal++;

                                                                    oCant.iCantDesayunos++;
                                                                    //oCant.iCantComidas++;
                                                                    //oCant.iCantCenas++;
                                                                }
                                                            }
                                                            //Sexta condición
                                                            else if ((fHoraInicio < fInicioDesayuno && (fHoraFinal > fInicioDesayuno && fHoraFinal <= fFinDesayuno)) && (oCom.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oCom.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                                                            {
                                                                if (bEsInterFinal)
                                                                {
                                                                    oCant.iCantDesayunosInt++;
                                                                    //oCant.iCantComidasInt++;
                                                                    //oCant.iCantCenasInt++;

                                                                    oCom.iDesayunosInt++;
                                                                    //oCom.iComidaInt++;
                                                                    //oCom.iCenaInt++;
                                                                }
                                                                else
                                                                {
                                                                    oCom.iDesayunosNal++;
                                                                    //oCom.iComidaNal++;
                                                                    //oCom.iCenaNal++;

                                                                    oCant.iCantDesayunos++;
                                                                    //oCant.iCantComidas++;
                                                                    //oCant.iCantCenas++;
                                                                }
                                                            }
                                                            //Septima condición
                                                            else if ((fHoraInicio >= fInicioDesayuno && fHoraInicio <= fFinDesayuno) && (fHoraFinal > fInicioDesayuno && fHoraFinal <= fFinDesayuno) && (oCom.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oCom.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                                                            {
                                                                if (bEsInterFinal)
                                                                {
                                                                    oCant.iCantDesayunosInt++;
                                                                    //oCant.iCantComidasInt++;
                                                                    //oCant.iCantCenasInt++;

                                                                    oCom.iDesayunosInt++;
                                                                    //oCom.iComidaInt++;
                                                                    //oCom.iCenaInt++;
                                                                }
                                                                else
                                                                {
                                                                    oCom.iDesayunosNal++;
                                                                    //oCom.iComidaNal++;
                                                                    //oCom.iCenaNal++;

                                                                    oCant.iCantDesayunos++;
                                                                    //oCant.iCantComidas++;
                                                                    //oCant.iCantCenas++;
                                                                }
                                                            }
                                                            //Octava condición
                                                            else if ((fHoraInicio < fInicioDesayuno && (fHoraFinal > fInicioDesayuno && fHoraFinal >= fFinDesayuno)) && (oCom.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oCom.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioComida) && (fHoraFinal < fInicioComida))
                                                            {
                                                                if (bEsInterFinal)
                                                                {
                                                                    oCant.iCantDesayunosInt++;
                                                                    //oCant.iCantComidasInt++;
                                                                    //oCant.iCantCenasInt++;

                                                                    oCom.iDesayunosInt++;
                                                                    //oCom.iComidaInt++;
                                                                    //oCom.iCenaInt++;
                                                                }
                                                                else
                                                                {
                                                                    oCom.iDesayunosNal++;
                                                                    //oCom.iComidaNal++;
                                                                    //oCom.iCenaNal++;

                                                                    oCant.iCantDesayunos++;
                                                                    //oCant.iCantComidas++;
                                                                    //oCant.iCantCenas++;
                                                                }
                                                            }
                                                            #endregion

                                                            #region COMIDA
                                                            //Primera condicion Comidas
                                                            if (((fHoraInicio < fInicioComida && fHoraFinal > fInicioComida) && fHoraFinal < fFinComida) && (oCom.sDestino != dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioCena))
                                                            {
                                                                if (bEsInterFinal)
                                                                {
                                                                    if (l > 0)
                                                                    {
                                                                        if (!bEsInterInicio)
                                                                        {
                                                                            oCant.iCantDesayunos++;
                                                                            oCom.iDesayunosNal++;
                                                                        }
                                                                    }


                                                                    oCant.iCantComidasInt++;
                                                                    //oCant.iCantCenasInt++;

                                                                    oCom.iComidaInt++;
                                                                    //oCom.iCenaInt++;
                                                                }
                                                                else
                                                                {
                                                                    if (l > 0)
                                                                    {
                                                                        if (bEsInterInicio)
                                                                        {
                                                                            oCant.iCantDesayunosInt++;
                                                                            oCom.iDesayunosInt++;
                                                                        }
                                                                    }

                                                                    oCant.iCantComidas++;
                                                                    //oCant.iCantCenas++;

                                                                    oCom.iComidaNal++;
                                                                    //oCom.iCenaNal++;
                                                                }
                                                            }
                                                            //Segunda condición Comidas
                                                            else if ((fHoraInicio > fInicioComida && fHoraInicio < fFinComida) && (fHoraFinal > fFinComida && fHoraFinal > fInicioComida) && (oCom.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioCena)) //Evaluar antes de cena
                                                            {
                                                                if (bEsInterFinal)
                                                                {
                                                                    if (l > 0)
                                                                    {
                                                                        if (!bEsInterInicio)
                                                                        {
                                                                            oCant.iCantDesayunos++;
                                                                            oCom.iDesayunosNal++;
                                                                        }
                                                                    }


                                                                    oCant.iCantComidasInt++;
                                                                    //oCant.iCantCenasInt++;

                                                                    oCom.iComidaInt++;
                                                                    //oCom.iCenaInt++;
                                                                }
                                                                else
                                                                {
                                                                    if (l > 0)
                                                                    {
                                                                        if (bEsInterInicio)
                                                                        {
                                                                            oCant.iCantDesayunosInt++;
                                                                            oCom.iDesayunosInt++;
                                                                        }
                                                                    }

                                                                    oCant.iCantComidas++;
                                                                    //oCant.iCantCenas++;

                                                                    oCom.iComidaNal++;
                                                                    //oCom.iCenaNal++;
                                                                }
                                                            }
                                                            //Tercera condición Comidas
                                                            else if ((fHoraInicio >= fInicioComida && fHoraFinal > fFinComida) && (oCom.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oCom.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioCena))
                                                            {
                                                                if (bEsInterFinal)
                                                                {
                                                                    if (l > 0)
                                                                    {
                                                                        if (!bEsInterInicio)
                                                                        {
                                                                            oCant.iCantDesayunos++;
                                                                            oCom.iDesayunosNal++;
                                                                        }
                                                                    }


                                                                    oCant.iCantComidasInt++;
                                                                    //oCant.iCantCenasInt++;

                                                                    oCom.iComidaInt++;
                                                                    //oCom.iCenaInt++;
                                                                }
                                                                else
                                                                {
                                                                    if (l > 0)
                                                                    {
                                                                        if (bEsInterInicio)
                                                                        {
                                                                            oCant.iCantDesayunosInt++;
                                                                            oCom.iDesayunosInt++;
                                                                        }
                                                                    }

                                                                    oCant.iCantComidas++;
                                                                    //oCant.iCantCenas++;

                                                                    oCom.iComidaNal++;
                                                                    //oCom.iCenaNal++;
                                                                }
                                                            }
                                                            //Cuarta condición Comidas
                                                            else if ((fHoraInicio < fInicioComida && fHoraFinal <= fInicioComida) && (oCom.sDestino != dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioCena))
                                                            {
                                                                if (bEsInterFinal)
                                                                {
                                                                    if (l > 0)
                                                                    {
                                                                        if (!bEsInterInicio)
                                                                        {
                                                                            oCant.iCantDesayunos++;
                                                                            oCom.iDesayunosNal++;
                                                                        }
                                                                    }


                                                                    oCant.iCantComidasInt++;
                                                                    //oCant.iCantCenasInt++;

                                                                    oCom.iComidaInt++;
                                                                    //oCom.iCenaInt++;
                                                                }
                                                                else
                                                                {
                                                                    if (l > 0)
                                                                    {
                                                                        if (bEsInterInicio)
                                                                        {
                                                                            oCant.iCantDesayunosInt++;
                                                                            oCom.iDesayunosInt++;
                                                                        }
                                                                    }

                                                                    oCant.iCantComidas++;
                                                                    //oCant.iCantCenas++;

                                                                    oCom.iComidaNal++;
                                                                    //oCom.iCenaNal++;
                                                                }
                                                            }
                                                            //Quinta condición Comidas
                                                            else if ((fHoraInicio < fInicioComida && fHoraFinal < fInicioComida) && (oCom.sDestino != dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioCena))
                                                            {
                                                                if (bEsInterFinal)
                                                                {
                                                                    if (l > 0)
                                                                    {
                                                                        if (!bEsInterInicio)
                                                                        {
                                                                            oCant.iCantDesayunos++;
                                                                            oCom.iDesayunosNal++;
                                                                        }
                                                                    }


                                                                    oCant.iCantComidasInt++;
                                                                    //oCant.iCantCenasInt++;

                                                                    oCom.iComidaInt++;
                                                                    //oCom.iCenaInt++;
                                                                }
                                                                else
                                                                {
                                                                    if (l > 0)
                                                                    {
                                                                        if (bEsInterInicio)
                                                                        {
                                                                            oCant.iCantDesayunosInt++;
                                                                            oCom.iDesayunosInt++;
                                                                        }
                                                                    }

                                                                    oCant.iCantComidas++;
                                                                    //oCant.iCantCenas++;

                                                                    oCom.iComidaNal++;
                                                                    //oCom.iCenaNal++;
                                                                }
                                                            }
                                                            //Sexta condición Comidas
                                                            else if ((fHoraInicio < fInicioComida && (fHoraFinal > fInicioComida && fHoraFinal <= fFinComida)) && (oCom.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oCom.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioCena))
                                                            {
                                                                if (bEsInterFinal)
                                                                {
                                                                    if (l > 0)
                                                                    {
                                                                        if (!bEsInterInicio)
                                                                        {
                                                                            oCant.iCantDesayunos++;
                                                                            oCom.iDesayunosNal++;
                                                                        }
                                                                    }


                                                                    oCant.iCantComidasInt++;
                                                                    //oCant.iCantCenasInt++;

                                                                    oCom.iComidaInt++;
                                                                    //oCom.iCenaInt++;
                                                                }
                                                                else
                                                                {
                                                                    if (l > 0)
                                                                    {
                                                                        if (bEsInterInicio)
                                                                        {
                                                                            oCant.iCantDesayunosInt++;
                                                                            oCom.iDesayunosInt++;
                                                                        }
                                                                    }

                                                                    oCant.iCantComidas++;
                                                                    //oCant.iCantCenas++;

                                                                    oCom.iComidaNal++;
                                                                    //oCom.iCenaNal++;
                                                                }
                                                            }
                                                            //Septima condición Comidas
                                                            else if ((fHoraInicio >= fInicioComida && fHoraInicio <= fFinComida) && (fHoraFinal > fInicioComida && fHoraFinal <= fFinComida) && (oCom.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oCom.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioCena))
                                                            {
                                                                if (bEsInterFinal)
                                                                {
                                                                    if (l > 0)
                                                                    {
                                                                        if (!bEsInterInicio)
                                                                        {
                                                                            oCant.iCantDesayunos++;
                                                                            oCom.iDesayunosNal++;
                                                                        }
                                                                    }


                                                                    oCant.iCantComidasInt++;
                                                                    //oCant.iCantCenasInt++;

                                                                    oCom.iComidaInt++;
                                                                    //oCom.iCenaInt++;
                                                                }
                                                                else
                                                                {
                                                                    if (l > 0)
                                                                    {
                                                                        if (bEsInterInicio)
                                                                        {
                                                                            oCant.iCantDesayunosInt++;
                                                                            oCom.iDesayunosInt++;
                                                                        }
                                                                    }

                                                                    oCant.iCantComidas++;
                                                                    //oCant.iCantCenas++;

                                                                    oCom.iComidaNal++;
                                                                    //oCom.iCenaNal++;
                                                                }
                                                            }
                                                            //Octava condición
                                                            else if ((fHoraInicio < fInicioComida && (fHoraFinal > fInicioComida && fHoraFinal >= fFinComida)) && (oCom.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oCom.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioCena))
                                                            {
                                                                if (bEsInterFinal)
                                                                {
                                                                    if (l > 0)
                                                                    {
                                                                        if (!bEsInterInicio)
                                                                        {
                                                                            oCant.iCantDesayunos++;
                                                                            oCom.iDesayunosNal++;
                                                                        }
                                                                    }


                                                                    oCant.iCantComidasInt++;
                                                                    //oCant.iCantCenasInt++;

                                                                    oCom.iComidaInt++;
                                                                    //oCom.iCenaInt++;
                                                                }
                                                                else
                                                                {
                                                                    if (l > 0)
                                                                    {
                                                                        if (bEsInterInicio)
                                                                        {
                                                                            oCant.iCantDesayunosInt++;
                                                                            oCom.iDesayunosInt++;
                                                                        }
                                                                    }

                                                                    oCant.iCantComidas++;
                                                                    //oCant.iCantCenas++;

                                                                    oCom.iComidaNal++;
                                                                    //oCom.iCenaNal++;
                                                                }
                                                            }
                                                            #endregion

                                                            #region CENA
                                                            //Primera condicion Cenas
                                                            if (((fHoraInicio < fInicioCena && fHoraFinal > fInicioCena) && fHoraFinal < fFinCena) && (oCom.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                                                            {
                                                                if (bEsInterFinal)
                                                                {
                                                                    if (l > 0)
                                                                    {
                                                                        if (!bEsInterInicio)
                                                                        {
                                                                            oCant.iCantDesayunos++;
                                                                            oCant.iCantComidas++;

                                                                            oCom.iDesayunosNal++;
                                                                            oCom.iComidaNal++;
                                                                        }
                                                                    }

                                                                    oCant.iCantCenasInt++;
                                                                    oCom.iCenaInt++;
                                                                }
                                                                else
                                                                {
                                                                    if (l > 0)
                                                                    {
                                                                        if (bEsInterInicio)
                                                                        {
                                                                            oCant.iCantDesayunosInt++;
                                                                            oCant.iCantComidasInt++;

                                                                            oCom.iDesayunosInt++;
                                                                            oCom.iComidaInt++;
                                                                        }
                                                                    }

                                                                    oCant.iCantCenas++;
                                                                    oCom.iCenaNal++;
                                                                }
                                                            }
                                                            //Segunda condición Cenas
                                                            else if ((fHoraInicio > fInicioCena && fHoraInicio < fFinCena) && (fHoraFinal > fFinCena && fHoraFinal > fInicioCena) && (oCom.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                                                            {
                                                                if (bEsInterFinal)
                                                                {
                                                                    if (l > 0)
                                                                    {
                                                                        if (!bEsInterInicio)
                                                                        {
                                                                            oCant.iCantDesayunos++;
                                                                            oCant.iCantComidas++;

                                                                            oCom.iDesayunosNal++;
                                                                            oCom.iComidaNal++;
                                                                        }
                                                                    }

                                                                    oCant.iCantCenasInt++;
                                                                    oCom.iCenaInt++;
                                                                }
                                                                else
                                                                {
                                                                    if (l > 0)
                                                                    {
                                                                        if (bEsInterInicio)
                                                                        {
                                                                            oCant.iCantDesayunosInt++;
                                                                            oCant.iCantComidasInt++;

                                                                            oCom.iDesayunosInt++;
                                                                            oCom.iComidaInt++;
                                                                        }
                                                                    }

                                                                    oCant.iCantCenas++;
                                                                    oCom.iCenaNal++;
                                                                }
                                                            }
                                                            //Tercera condición Cenas
                                                            else if ((fHoraInicio >= fInicioCena && fHoraFinal > fFinCena) && (oCom.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oCom.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                                                            {
                                                                if (bEsInterFinal)
                                                                {
                                                                    if (l > 0)
                                                                    {
                                                                        if (!bEsInterInicio)
                                                                        {
                                                                            oCant.iCantDesayunos++;
                                                                            oCant.iCantComidas++;

                                                                            oCom.iDesayunosNal++;
                                                                            oCom.iComidaNal++;
                                                                        }
                                                                    }

                                                                    oCant.iCantCenasInt++;
                                                                    oCom.iCenaInt++;
                                                                }
                                                                else
                                                                {
                                                                    if (l > 0)
                                                                    {
                                                                        if (bEsInterInicio)
                                                                        {
                                                                            oCant.iCantDesayunosInt++;
                                                                            oCant.iCantComidasInt++;

                                                                            oCom.iDesayunosInt++;
                                                                            oCom.iComidaInt++;
                                                                        }
                                                                    }

                                                                    oCant.iCantCenas++;
                                                                    oCom.iCenaNal++;
                                                                }
                                                            }
                                                            //Cuarta condición Cenas
                                                            else if ((fHoraInicio < fInicioCena && fHoraFinal <= fInicioCena) && (oCom.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                                                            {
                                                                if (bEsInterFinal)
                                                                {
                                                                    if (l > 0)
                                                                    {
                                                                        if (!bEsInterInicio)
                                                                        {
                                                                            oCant.iCantDesayunos++;
                                                                            oCant.iCantComidas++;

                                                                            oCom.iDesayunosNal++;
                                                                            oCom.iComidaNal++;
                                                                        }
                                                                    }

                                                                    oCant.iCantCenasInt++;
                                                                    oCom.iCenaInt++;
                                                                }
                                                                else
                                                                {
                                                                    if (l > 0)
                                                                    {
                                                                        if (bEsInterInicio)
                                                                        {
                                                                            oCant.iCantDesayunosInt++;
                                                                            oCant.iCantComidasInt++;

                                                                            oCom.iDesayunosInt++;
                                                                            oCom.iComidaInt++;
                                                                        }
                                                                    }

                                                                    oCant.iCantCenas++;
                                                                    oCom.iCenaNal++;
                                                                }
                                                            }
                                                            //Quinta condición Cenas
                                                            else if ((fHoraInicio < fInicioCena && fHoraFinal < fInicioCena) && (oCom.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                                                            {
                                                                if (bEsInterFinal)
                                                                {
                                                                    if (l > 0)
                                                                    {
                                                                        if (!bEsInterInicio)
                                                                        {
                                                                            oCant.iCantDesayunos++;
                                                                            oCant.iCantComidas++;

                                                                            oCom.iDesayunosNal++;
                                                                            oCom.iComidaNal++;
                                                                        }
                                                                    }

                                                                    oCant.iCantCenasInt++;
                                                                    oCom.iCenaInt++;
                                                                }
                                                                else
                                                                {
                                                                    if (l > 0)
                                                                    {
                                                                        if (bEsInterInicio)
                                                                        {
                                                                            oCant.iCantDesayunosInt++;
                                                                            oCant.iCantComidasInt++;

                                                                            oCom.iDesayunosInt++;
                                                                            oCom.iComidaInt++;
                                                                        }
                                                                    }

                                                                    oCant.iCantCenas++;
                                                                    oCom.iCenaNal++;
                                                                }
                                                            }
                                                            //Sexta condición Cenas
                                                            else if ((fHoraInicio < fInicioCena && (fHoraFinal > fInicioCena && fHoraFinal <= fFinCena)) && (oCom.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oCom.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                                                            {
                                                                if (bEsInterFinal)
                                                                {
                                                                    if (l > 0)
                                                                    {
                                                                        if (!bEsInterInicio)
                                                                        {
                                                                            oCant.iCantDesayunos++;
                                                                            oCant.iCantComidas++;

                                                                            oCom.iDesayunosNal++;
                                                                            oCom.iComidaNal++;
                                                                        }
                                                                    }

                                                                    oCant.iCantCenasInt++;
                                                                    oCom.iCenaInt++;
                                                                }
                                                                else
                                                                {
                                                                    if (l > 0)
                                                                    {
                                                                        if (bEsInterInicio)
                                                                        {
                                                                            oCant.iCantDesayunosInt++;
                                                                            oCant.iCantComidasInt++;

                                                                            oCom.iDesayunosInt++;
                                                                            oCom.iComidaInt++;
                                                                        }
                                                                    }

                                                                    oCant.iCantCenas++;
                                                                    oCom.iCenaNal++;
                                                                }
                                                            }
                                                            //Septima condición Cenas
                                                            else if ((fHoraInicio >= fInicioCena && fHoraInicio <= fFinCena) && (fHoraFinal > fInicioCena && fHoraFinal <= fFinCena) && (oCom.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oCom.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                                                            {
                                                                if (bEsInterFinal)
                                                                {
                                                                    if (l > 0)
                                                                    {
                                                                        if (!bEsInterInicio)
                                                                        {
                                                                            oCant.iCantDesayunos++;
                                                                            oCant.iCantComidas++;

                                                                            oCom.iDesayunosNal++;
                                                                            oCom.iComidaNal++;
                                                                        }
                                                                    }

                                                                    oCant.iCantCenasInt++;
                                                                    oCom.iCenaInt++;
                                                                }
                                                                else
                                                                {
                                                                    if (l > 0)
                                                                    {
                                                                        if (bEsInterInicio)
                                                                        {
                                                                            oCant.iCantDesayunosInt++;
                                                                            oCant.iCantComidasInt++;

                                                                            oCom.iDesayunosInt++;
                                                                            oCom.iComidaInt++;
                                                                        }
                                                                    }

                                                                    oCant.iCantCenas++;
                                                                    oCom.iCenaNal++;
                                                                }
                                                            }
                                                            //Octava condición Cenas
                                                            else if ((fHoraInicio < fInicioCena && (fHoraFinal > fInicioCena && fHoraFinal >= fFinCena)) && (oCom.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oCom.sDestino == dtLegs.Rows[0]["HomeBase"].S()) || (fHoraFinal > fInicioCena))
                                                            {
                                                                if (bEsInterFinal)
                                                                {
                                                                    if (l > 0)
                                                                    {
                                                                        if (!bEsInterInicio)
                                                                        {
                                                                            //oCant.iCantDesayunos++;
                                                                            //oCant.iCantComidas++;

                                                                            //oCom.iDesayunosNal++;
                                                                            //oCom.iComidaNal++;
                                                                        }
                                                                    }

                                                                    oCant.iCantCenasInt++;
                                                                    oCom.iCenaInt++;
                                                                }
                                                                else
                                                                {
                                                                    if (l > 0)
                                                                    {
                                                                        if (bEsInterInicio)
                                                                        {
                                                                            //oCant.iCantDesayunosInt++;
                                                                            //oCant.iCantComidasInt++;

                                                                            //oCom.iDesayunosInt++;
                                                                            //oCom.iComidaInt++;
                                                                        }
                                                                    }

                                                                    oCant.iCantCenas++;
                                                                    oCom.iCenaNal++;
                                                                }
                                                            }
                                                            #endregion

                                                        }
                                                        else
                                                        {
                                                            //if (bEsInterFinal)
                                                            //{
                                                            //    oCant.iCantDesayunosInt++;
                                                            //    oCant.iCantComidasInt++;
                                                            //    oCant.iCantCenasInt++;

                                                            //    oCom.iDesayunosInt++;
                                                            //    oCom.iComidaInt++;
                                                            //    oCom.iCenaInt++;
                                                            //}
                                                            //else
                                                            //{
                                                            //    oCant.iCantDesayunos++;
                                                            //    oCant.iCantComidas++;
                                                            //    oCant.iCantCenas++;

                                                            //    oCom.iDesayunosNal++;
                                                            //    oCom.iComidaNal++;
                                                            //    oCom.iCenaNal++;
                                                            //}
                                                        }

                                                        oLsComDia.Add(oCom);
                                                    }
                                                }
                                            }
                                        }
                                        #endregion

                                        #endregion


                                        #region CÓDIGO DE CALCULO ORIGINAL
                                        //if (dtDias.Rows.Count == 1)
                                        //{
                                        //    DataRow[] rowsD = dtLegs.Select("Dia = '" + dtDias.Rows[0]["Dia"].S() + "' ");

                                        //    if (rowsD.Length > 1)
                                        //    {
                                        //        for (int k = 0; k < rowsD.Length; k++)
                                        //        {
                                        //            fHoraInicio = 0;
                                        //            fHoraFinal = 0;

                                        //            dtIni = dtLegs.Rows[0][FechaInicio].S().Dt();
                                        //            dtFin = dtLegs.Rows[dtLegs.Rows.Count - 1][FechaFin].S().Dt();

                                        //            bEsInterInicio = false;
                                        //            bEsInterFinal = false;

                                        //            bEsInterInicio = dtLegs.Rows[0]["EsInternacional"].S() == "1" ? true : false;
                                        //            bEsInterFinal = dtLegs.Rows[dtLegs.Rows.Count - 1]["EsInternacional"].S() == "1" ? true : false;

                                        //            fHoraInicio = dtIni.Hour + (dtIni.Minute / float.Parse("60"));
                                        //            fHoraFinal = dtFin.Hour + (dtFin.Minute / float.Parse("60"));

                                        //            ComidasPorDia oComDia1 = new ComidasPorDia();
                                        //            oComDia1.sClavePiloto = drP["ClavePiloto"].S();
                                        //            oComDia1.dtFechaDia = rowsD[k][FechaInicio].S().Dt();
                                        //            oComDia1.dtFechaFin = rowsD[k][FechaFin].S().Dt();
                                        //            oComDia1.sduty_type = rowsD[k]["DutyType"].S();
                                        //            oComDia1.sOrigen = rowsD[k]["POD"].S();
                                        //            oComDia1.sDestino = rowsD[k]["POA"].S();

                                        //            if (k + 1 == rowsD.Length)
                                        //            {
                                        //                DateTime dtIniDia = rowsD[0][FechaInicio].S().Dt();
                                        //                DateTime dtFinDia = rowsD[rowsD.Length - 1][FechaFin].S().Dt();

                                        //                fHoraInicio = dtIniDia.Hour + (dtIniDia.Minute / float.Parse("60"));
                                        //                fHoraFinal = dtFinDia.Hour + (dtFinDia.Minute / float.Parse("60"));

                                        //                if (fHoraInicio < fInicioDesayuno || (fHoraInicio > fInicioDesayuno && fHoraInicio < fFinDesayuno))
                                        //                {
                                        //                    if (bEsInterFinal)
                                        //                    {
                                        //                        oCant.iCantDesayunosInt++;
                                        //                        oComDia1.iDesayunosInt++;
                                        //                    }
                                        //                    else
                                        //                    {
                                        //                        oCant.iCantDesayunos++;
                                        //                        oComDia1.iDesayunosNal++;
                                        //                    }
                                        //                }
                                        //                if ((fHoraInicio <= fInicioComida && (fHoraFinal > fInicioComida)) || (fHoraInicio > fInicioComida && fHoraInicio <= fFinComida) || (fHoraInicio < fInicioComida && fHoraFinal > fFinComida))
                                        //                {
                                        //                    if (bEsInterFinal)
                                        //                    {
                                        //                        oCant.iCantComidasInt++;
                                        //                        oComDia1.iComidaInt++;
                                        //                    }
                                        //                    else
                                        //                    {
                                        //                        oCant.iCantComidas++;
                                        //                        oComDia1.iComidaNal++;
                                        //                    }
                                        //                }
                                        //                if ((fHoraInicio < fInicioCena && fHoraFinal >= fInicioCena) || (fHoraInicio <= fFinCena && fHoraFinal > fInicioCena))
                                        //                {
                                        //                    if (bEsInterFinal)
                                        //                    {
                                        //                        oCant.iCantCenasInt++;
                                        //                        oComDia1.iCenaInt++;
                                        //                    }
                                        //                    else
                                        //                    {
                                        //                        oCant.iCantCenas++;
                                        //                        oComDia1.iCenaNal++;
                                        //                    }
                                        //                }
                                        //            }

                                        //            oLsComDia.Add(oComDia1);
                                        //        }
                                        //    }
                                        //    else
                                        //    {
                                        //        ComidasPorDia oComDia = new ComidasPorDia();
                                        //        oComDia.dtFechaDia = dtIni;
                                        //        oComDia.dtFechaFin = dtFin;
                                        //        oComDia.sClavePiloto = drP["ClavePiloto"].S();
                                        //        oComDia.sduty_type = dtLegs.Rows[0]["DutyType"].S();
                                        //        oComDia.sOrigen = dtLegs.Rows[0]["POD"].S();
                                        //        oComDia.sDestino = dtLegs.Rows[dtLegs.Rows.Count - 1]["POA"].S();

                                        //        if ((fHoraInicio < fInicioDesayuno || (fHoraInicio > fInicioDesayuno && fHoraInicio < fFinDesayuno)) && oComDia.sDestino != dtLegs.Rows[0]["HomeBase"].S())
                                        //        {
                                        //            if (bEsInterFinal)
                                        //            {
                                        //                oCant.iCantDesayunosInt++;

                                        //                oComDia.iDesayunosInt++;
                                        //            }
                                        //            else
                                        //            {
                                        //                oCant.iCantDesayunos++;

                                        //                oComDia.iDesayunosNal++;
                                        //            }
                                        //        }
                                        //        if (((fHoraInicio <= fInicioComida && (fHoraFinal > fInicioComida)) || (fHoraInicio > fInicioComida && fHoraInicio <= fFinComida) || (fHoraInicio < fInicioComida && fHoraFinal > fFinComida)) && oComDia.sDestino != dtLegs.Rows[0]["HomeBase"].S())
                                        //        {
                                        //            if (bEsInterFinal)
                                        //            {
                                        //                oCant.iCantComidasInt++;

                                        //                oComDia.iComidaInt++;
                                        //            }
                                        //            else
                                        //            {
                                        //                oCant.iCantComidas++;

                                        //                oComDia.iComidaNal++;
                                        //            }
                                        //        }
                                        //        if (((fHoraInicio < fInicioCena && fHoraFinal >= fInicioCena) || (fHoraInicio <= fFinCena && fHoraFinal > fInicioCena)) && oComDia.sDestino != dtLegs.Rows[0]["HomeBase"].S())
                                        //        {
                                        //            if (bEsInterFinal)
                                        //            {
                                        //                oCant.iCantCenasInt++;

                                        //                oComDia.iCenaInt++;
                                        //            }
                                        //            else
                                        //            {
                                        //                oCant.iCantCenas++;

                                        //                oComDia.iCenaNal++;
                                        //            }
                                        //        }

                                        //        oLsComDia.Add(oComDia);

                                        //    }
                                        //}
                                        //else if (dtDias.Rows.Count == 2)
                                        //{
                                        //    for (int l = 0; l < dtDias.Rows.Count; l++)
                                        //    {
                                        //        DataRow[] rowsD = dtLegs.Select("Dia = '" + dtDias.Rows[l]["Dia"].S() + "' ");

                                        //        if (rowsD.Length > 1)
                                        //        {
                                        //            if (rowsD[0]["PaisPOA"].S() != rowsD[rowsD.Length - 1]["PaisPOA"].S())
                                        //            {
                                        //                // PIERNAS EN MEXICO Y EL EXTRANJERO

                                        //                for (int k = 0; k < rowsD.Length; k++)
                                        //                {
                                        //                    ComidasPorDia oComDia1 = new ComidasPorDia();
                                        //                    oComDia1.sClavePiloto = drP["ClavePiloto"].S();
                                        //                    oComDia1.dtFechaDia = rowsD[k][FechaInicio].S().Dt();
                                        //                    oComDia1.dtFechaFin = rowsD[k][FechaFin].S().Dt();
                                        //                    oComDia1.sduty_type = rowsD[k]["DutyType"].S();
                                        //                    oComDia1.sOrigen = rowsD[k]["POD"].S();
                                        //                    oComDia1.sDestino = rowsD[k]["POA"].S();

                                        //                    bEsInterInicio = rowsD[k]["PaisPOD"].S() == "MX" ? false : true;
                                        //                    bEsInterFinal = rowsD[k]["PaisPOA"].S() == "MX" ? false : true;

                                        //                    DateTime dtIniDia = rowsD[k][FechaInicio].S().Dt();
                                        //                    DateTime dtFinDia = rowsD[k][FechaFin].S().Dt();

                                        //                    fHoraInicio = dtIniDia.Hour + (dtIniDia.Minute / float.Parse("60"));
                                        //                    fHoraFinal = dtFinDia.Hour + (dtFinDia.Minute / float.Parse("60"));

                                        //                    if (l > 0 && k == 0)
                                        //                        fHoraInicio = 0;

                                        //                    // SI - CENA
                                        //                    if ((fHoraInicio < fInicioCena && fHoraFinal >= fInicioCena) || (fHoraInicio <= fFinCena && fHoraFinal > fInicioCena))
                                        //                    {
                                        //                        if (bEsInterFinal)
                                        //                        {
                                        //                            if (l > 0 && k == 0)
                                        //                            {
                                        //                                if (!bEsInterInicio)
                                        //                                {
                                        //                                    oCant.iCantDesayunos++;
                                        //                                    oCant.iCantComidas++;

                                        //                                    oComDia1.iDesayunosNal++;
                                        //                                    oComDia1.iComidaNal++;
                                        //                                }
                                        //                                else
                                        //                                {
                                        //                                    oCant.iCantDesayunosInt++;
                                        //                                    oCant.iCantComidasInt++;

                                        //                                    oComDia1.iDesayunosInt++;
                                        //                                    oComDia1.iComidaInt++;
                                        //                                }
                                        //                            }


                                        //                            oCant.iCantCenasInt++;
                                        //                            oComDia1.iCenaInt++;
                                        //                        }
                                        //                        else
                                        //                        {
                                        //                            if (l > 0 && k == 0)
                                        //                            {
                                        //                                if (bEsInterInicio)
                                        //                                {
                                        //                                    oCant.iCantDesayunosInt++;
                                        //                                    oCant.iCantComidasInt++;

                                        //                                    oComDia1.iDesayunosInt++;
                                        //                                    oComDia1.iComidaInt++;
                                        //                                }
                                        //                                else
                                        //                                {
                                        //                                    oCant.iCantDesayunos++;
                                        //                                    oCant.iCantComidas++;

                                        //                                    oComDia1.iDesayunosNal++;
                                        //                                    oComDia1.iComidaNal++;
                                        //                                }
                                        //                            }

                                        //                            oCant.iCantCenas++;
                                        //                            oComDia1.iCenaNal++;
                                        //                        }
                                        //                    }
                                        //                    // SI - DESAYUNO / COMIDA
                                        //                    else if ((fHoraInicio <= fInicioComida && (fHoraFinal > fInicioComida)) || (fHoraInicio > fInicioComida && fHoraInicio <= fFinComida) || (fHoraInicio < fInicioComida && fHoraFinal > fFinComida))
                                        //                    {
                                        //                        if (bEsInterFinal)
                                        //                        {
                                        //                            if (l > 0 && k == 0)
                                        //                            {
                                        //                                if (!bEsInterInicio)
                                        //                                {
                                        //                                    oCant.iCantDesayunos++;
                                        //                                    oComDia1.iDesayunosNal++;
                                        //                                }
                                        //                                else
                                        //                                {
                                        //                                    oCant.iCantDesayunosInt++;
                                        //                                    oComDia1.iDesayunosInt++;
                                        //                                }
                                        //                            }

                                        //                            oCant.iCantComidasInt++;
                                        //                            oComDia1.iComidaInt++;
                                        //                        }
                                        //                        else
                                        //                        {
                                        //                            if (l > 0 && k == 0)
                                        //                            {
                                        //                                if (bEsInterInicio)
                                        //                                {
                                        //                                    oCant.iCantDesayunosInt++;
                                        //                                    oComDia1.iDesayunosInt++;
                                        //                                }
                                        //                                else
                                        //                                {
                                        //                                    oCant.iCantDesayunos++;
                                        //                                    oComDia1.iDesayunosNal++;
                                        //                                }
                                        //                            }

                                        //                            oCant.iCantComidas++;
                                        //                            oComDia1.iComidaNal++;
                                        //                        }

                                        //                        if (l == 0 && k == 0)
                                        //                        {
                                        //                            if (bEsInterFinal)
                                        //                            {
                                        //                                oCant.iCantCenasInt++;
                                        //                                oComDia1.iCenaInt++;
                                        //                            }
                                        //                            else
                                        //                            {
                                        //                                oCant.iCantCenas++;
                                        //                                oComDia1.iCenaNal++;
                                        //                            }
                                        //                        }
                                        //                    }
                                        //                    // SI - DESAYUNO / COMIDA / CENA
                                        //                    else if (fHoraInicio < fInicioDesayuno || (fHoraInicio > fInicioDesayuno && fHoraInicio < fFinDesayuno))
                                        //                    {
                                        //                        if (bEsInterFinal)
                                        //                        {
                                        //                            oCant.iCantDesayunosInt++;
                                        //                            oComDia1.iDesayunosInt++;
                                        //                        }
                                        //                        else
                                        //                        {
                                        //                            oCant.iCantDesayunos++;
                                        //                            oComDia1.iDesayunosNal++;
                                        //                        }
                                        //                    }
                                        //                    else
                                        //                    {
                                        //                        if (l == 0 && k == 0)
                                        //                        {
                                        //                            if (bEsInterFinal)
                                        //                            {
                                        //                                oCant.iCantCenasInt++;
                                        //                                oComDia1.iCenaInt++;
                                        //                            }
                                        //                            else
                                        //                            {
                                        //                                oCant.iCantCenas++;
                                        //                                oComDia1.iCenaNal++;
                                        //                            }
                                        //                        }
                                        //                    }

                                        //                    oLsComDia.Add(oComDia1);
                                        //                }
                                        //            }
                                        //            else
                                        //            {
                                        //                // PRIMERA PIERNA Y ULTIMA DEL MISMO PAIS
                                        //                for (int k = 0; k < rowsD.Length; k++)
                                        //                {
                                        //                    ComidasPorDia oComDia1 = new ComidasPorDia();
                                        //                    oComDia1.sClavePiloto = drP["ClavePiloto"].S();
                                        //                    oComDia1.dtFechaDia = rowsD[k][FechaInicio].S().Dt();
                                        //                    oComDia1.dtFechaFin = rowsD[k][FechaFin].S().Dt();
                                        //                    oComDia1.sduty_type = rowsD[k]["DutyType"].S();
                                        //                    oComDia1.sOrigen = rowsD[k]["POD"].S();
                                        //                    oComDia1.sDestino = rowsD[k]["POA"].S();

                                        //                    if (k + 1 == rowsD.Length)
                                        //                    {
                                        //                        bEsInterInicio = rowsD[rowsD.Length - 1]["PaisPOD"].S() == "MX" ? false : true;
                                        //                        bEsInterFinal = rowsD[rowsD.Length - 1]["PaisPOA"].S() == "MX" ? false : true;

                                        //                        DateTime dtIniDia = rowsD[0][FechaInicio].S().Dt();
                                        //                        DateTime dtFinDia = rowsD[rowsD.Length - 1][FechaFin].S().Dt();

                                        //                        fHoraInicio = dtIniDia.Hour + (dtIniDia.Minute / float.Parse("60"));
                                        //                        fHoraFinal = dtFinDia.Hour + (dtFinDia.Minute / float.Parse("60"));

                                        //                        if (l > 0)
                                        //                            fHoraInicio = 0;

                                        //                        // SI - CENA
                                        //                        if ((fHoraInicio < fInicioCena && fHoraFinal >= fInicioCena) || (fHoraInicio <= fFinCena && fHoraFinal > fInicioCena))
                                        //                        {
                                        //                            if (bEsInterFinal)
                                        //                            {
                                        //                                if (l > 0)
                                        //                                {
                                        //                                    if (!bEsInterInicio)
                                        //                                    {
                                        //                                        oCant.iCantDesayunos++;
                                        //                                        oCant.iCantComidas++;

                                        //                                        oComDia1.iDesayunosNal++;
                                        //                                        oComDia1.iComidaNal++;
                                        //                                    }
                                        //                                    else
                                        //                                    {
                                        //                                        oCant.iCantDesayunosInt++;
                                        //                                        oCant.iCantComidasInt++;

                                        //                                        oComDia1.iDesayunosInt++;
                                        //                                        oComDia1.iComidaInt++;
                                        //                                    }
                                        //                                }

                                        //                                oCant.iCantCenasInt++;
                                        //                                oComDia1.iCenaInt++;
                                        //                            }
                                        //                            else
                                        //                            {
                                        //                                if (l > 0)
                                        //                                {
                                        //                                    if (bEsInterInicio)
                                        //                                    {
                                        //                                        oCant.iCantDesayunosInt++;
                                        //                                        oCant.iCantComidasInt++;

                                        //                                        oComDia1.iDesayunosInt++;
                                        //                                        oComDia1.iComidaInt++;
                                        //                                    }
                                        //                                    else
                                        //                                    {
                                        //                                        oCant.iCantDesayunos++;
                                        //                                        oCant.iCantComidas++;

                                        //                                        oComDia1.iDesayunosNal++;
                                        //                                        oComDia1.iComidaNal++;
                                        //                                    }
                                        //                                }

                                        //                                oCant.iCantCenas++;
                                        //                                oComDia1.iCenaNal++;
                                        //                            }
                                        //                        }
                                        //                        // SI - DESAYUNO / COMIDA
                                        //                        else if ((fHoraInicio <= fInicioComida && (fHoraFinal > fInicioComida)) || (fHoraInicio > fInicioComida && fHoraInicio <= fFinComida) || (fHoraInicio < fInicioComida && fHoraFinal > fFinComida))
                                        //                        {
                                        //                            if (bEsInterFinal)
                                        //                            {
                                        //                                if (l > 0)
                                        //                                {
                                        //                                    if (!bEsInterInicio)
                                        //                                    {
                                        //                                        oCant.iCantDesayunos++;
                                        //                                        oComDia1.iDesayunosNal++;
                                        //                                    }
                                        //                                    else
                                        //                                    {
                                        //                                        oCant.iCantDesayunosInt++;
                                        //                                        oComDia1.iDesayunosInt++;
                                        //                                    }
                                        //                                }

                                        //                                oCant.iCantComidasInt++;
                                        //                                oComDia1.iComidaInt++;
                                        //                            }
                                        //                            else
                                        //                            {
                                        //                                if (l > 0)
                                        //                                {
                                        //                                    if (bEsInterInicio)
                                        //                                    {
                                        //                                        oCant.iCantDesayunosInt++;
                                        //                                        oComDia1.iDesayunosInt++;
                                        //                                    }
                                        //                                    else
                                        //                                    {
                                        //                                        oCant.iCantDesayunos++;
                                        //                                        oComDia1.iDesayunosNal++;
                                        //                                    }
                                        //                                }

                                        //                                oCant.iCantComidas++;
                                        //                                oComDia1.iComidaNal++;
                                        //                            }

                                        //                            if (l == 0)
                                        //                            {
                                        //                                if (bEsInterFinal)
                                        //                                {
                                        //                                    oCant.iCantCenasInt++;
                                        //                                    oComDia1.iCenaInt++;
                                        //                                }
                                        //                                else
                                        //                                {
                                        //                                    oCant.iCantCenas++;
                                        //                                    oComDia1.iCenaNal++;
                                        //                                }
                                        //                            }
                                        //                        }
                                        //                        // SI - DESAYUNO / COMIDA / CENA
                                        //                        else if (fHoraInicio < fInicioDesayuno || (fHoraInicio > fInicioDesayuno && fHoraInicio < fFinDesayuno))
                                        //                        {
                                        //                            if (bEsInterFinal)
                                        //                            {
                                        //                                oCant.iCantDesayunosInt++;
                                        //                                oComDia1.iDesayunosInt++;
                                        //                            }
                                        //                            else
                                        //                            {
                                        //                                oCant.iCantDesayunos++;
                                        //                                oComDia1.iDesayunosNal++;
                                        //                            }
                                        //                        }
                                        //                        else
                                        //                        {
                                        //                            if (l == 0)
                                        //                            {
                                        //                                if (bEsInterFinal)
                                        //                                {
                                        //                                    oCant.iCantCenasInt++;
                                        //                                    oComDia1.iCenaInt++;
                                        //                                }
                                        //                                else
                                        //                                {
                                        //                                    oCant.iCantCenas++;
                                        //                                    oComDia1.iCenaNal++;
                                        //                                }
                                        //                            }
                                        //                        }

                                        //                        #region COMENTADO
                                        //                        //if (fHoraInicio < fInicioDesayuno || (fHoraInicio > fInicioDesayuno && fHoraInicio < fFinDesayuno))
                                        //                        //{
                                        //                        //    if (bEsInterFinal)
                                        //                        //    {
                                        //                        //        oCant.iCantDesayunosInt++;
                                        //                        //        oCant.iCantComidasInt++;
                                        //                        //        oCant.iCantCenasInt++;

                                        //                        //        oComDia1.iDesayunosInt++;
                                        //                        //        oComDia1.iComidaInt++;
                                        //                        //        oComDia1.iCenaInt++;
                                        //                        //    }
                                        //                        //    else
                                        //                        //    {
                                        //                        //        oComDia1.iDesayunosNal++;
                                        //                        //        oComDia1.iComidaNal++;
                                        //                        //        oComDia1.iCenaNal++;
                                        //                        //    }
                                        //                        //}
                                        //                        //else if ((fHoraInicio <= fInicioComida && (fHoraFinal > fInicioComida)) || (fHoraInicio > fInicioComida && fHoraInicio <= fFinComida) || (fHoraInicio < fInicioComida && fHoraFinal > fFinComida))
                                        //                        //{
                                        //                        //    if (bEsInterFinal)
                                        //                        //    {
                                        //                        //        oCant.iCantComidasInt++;
                                        //                        //        oCant.iCantCenasInt++;

                                        //                        //        oComDia1.iComidaInt++;
                                        //                        //        oComDia1.iCenaInt++;
                                        //                        //    }
                                        //                        //    else
                                        //                        //    {
                                        //                        //        oCant.iCantComidas++;
                                        //                        //        oCant.iCantCenas++;

                                        //                        //        oComDia1.iComidaNal++;
                                        //                        //        oComDia1.iCenaNal++;
                                        //                        //    }
                                        //                        //}
                                        //                        //else if ((fHoraInicio < fInicioCena && fHoraFinal >= fInicioCena) || (fHoraInicio <= fFinCena && fHoraFinal > fInicioCena))
                                        //                        //{
                                        //                        //    if (bEsInterFinal)
                                        //                        //    {
                                        //                        //        oCant.iCantCenasInt++;

                                        //                        //        oComDia1.iCenaInt++;
                                        //                        //    }
                                        //                        //    else
                                        //                        //    {
                                        //                        //        oCant.iCantCenas++;
                                        //                        //        oComDia1.iCenaNal++;
                                        //                        //    }
                                        //                        //}
                                        //                        #endregion
                                        //                    }

                                        //                    oLsComDia.Add(oComDia1);
                                        //                }
                                        //            }
                                        //        }
                                        //        else
                                        //        {
                                        //            ComidasPorDia oComDia1 = new ComidasPorDia();
                                        //            oComDia1.sClavePiloto = drP["ClavePiloto"].S();
                                        //            oComDia1.dtFechaDia = rowsD[0][FechaInicio].S().Dt();
                                        //            oComDia1.dtFechaFin = rowsD[0][FechaFin].S().Dt();
                                        //            oComDia1.sduty_type = rowsD[0]["DutyType"].S();
                                        //            oComDia1.sOrigen = rowsD[0]["POD"].S();
                                        //            oComDia1.sDestino = rowsD[0]["POA"].S();


                                        //            DateTime dtInicial = rowsD[0][FechaInicio].S().Dt();
                                        //            DateTime dtFinal = rowsD[0][FechaFin].S().Dt();

                                        //            bEsInterFinal = rowsD[0]["EsInternacional"].S() == "1" ? true : false;
                                        //            bEsInterInicio = rowsD[0]["PaisPOD"].S() == "MX" ? false : true;


                                        //            fHoraInicio = dtInicial.Hour + (dtInicial.Minute / float.Parse("60"));
                                        //            fHoraFinal = dtFinal.Hour + (dtFinal.Minute / float.Parse("60"));

                                        //            if (l > 0)
                                        //            {
                                        //                fHoraInicio = 0;
                                        //            }

                                        //            // SI - CENA
                                        //            if ((fHoraInicio < fInicioCena && fHoraFinal >= fInicioCena) || (fHoraInicio <= fFinCena && fHoraFinal > fInicioCena))
                                        //            {
                                        //                if (bEsInterFinal)
                                        //                {
                                        //                    if (l > 0)
                                        //                    {
                                        //                        if (!bEsInterInicio)
                                        //                        {
                                        //                            oCant.iCantDesayunos++;
                                        //                            oCant.iCantComidas++;

                                        //                            oComDia1.iDesayunosNal++;
                                        //                            oComDia1.iComidaNal++;
                                        //                        }
                                        //                    }


                                        //                    oCant.iCantCenasInt++;
                                        //                    oComDia1.iCenaInt++;
                                        //                }
                                        //                else
                                        //                {
                                        //                    if (l > 0)
                                        //                    {
                                        //                        if (bEsInterInicio)
                                        //                        {
                                        //                            oCant.iCantDesayunosInt++;
                                        //                            oCant.iCantComidasInt++;

                                        //                            oComDia1.iDesayunosInt++;
                                        //                            oComDia1.iComidaInt++;
                                        //                        }
                                        //                    }

                                        //                    oCant.iCantCenas++;
                                        //                    oComDia1.iCenaNal++;
                                        //                }
                                        //            }
                                        //            // SI - DESAYUNO / COMIDA
                                        //            else if ((fHoraInicio <= fInicioComida && (fHoraFinal > fInicioComida)) || (fHoraInicio > fInicioComida && fHoraInicio <= fFinComida) || (fHoraInicio < fInicioComida && fHoraFinal > fFinComida))
                                        //            {
                                        //                if (bEsInterFinal)
                                        //                {
                                        //                    if (l > 0)
                                        //                    {
                                        //                        if (!bEsInterInicio)
                                        //                        {
                                        //                            oCant.iCantDesayunos++;
                                        //                            oComDia1.iDesayunosNal++;
                                        //                        }
                                        //                    }

                                        //                    oCant.iCantComidasInt++;
                                        //                    oComDia1.iComidaInt++;
                                        //                }
                                        //                else
                                        //                {
                                        //                    if (l > 0)
                                        //                    {
                                        //                        if (bEsInterInicio)
                                        //                        {
                                        //                            oCant.iCantDesayunosInt++;
                                        //                            oComDia1.iDesayunosInt++;
                                        //                        }
                                        //                    }

                                        //                    oCant.iCantComidas++;
                                        //                    oComDia1.iComidaNal++;
                                        //                }

                                        //                if (l == 0)
                                        //                {
                                        //                    if (bEsInterFinal)
                                        //                    {
                                        //                        oCant.iCantCenasInt++;
                                        //                        oComDia1.iCenaInt++;
                                        //                    }
                                        //                    else
                                        //                    {
                                        //                        oCant.iCantCenas++;
                                        //                        oComDia1.iCenaNal++;
                                        //                    }
                                        //                }
                                        //            }
                                        //            // SI - DESAYUNO / COMIDA / CENA
                                        //            else if (fHoraInicio < fInicioDesayuno || (fHoraInicio > fInicioDesayuno && fHoraInicio < fFinDesayuno))
                                        //            {
                                        //                if (bEsInterFinal)
                                        //                {
                                        //                    oCant.iCantDesayunosInt++;
                                        //                    //oCant.iCantComidasInt++;
                                        //                    //oCant.iCantCenasInt++;

                                        //                    oComDia1.iDesayunosInt++;
                                        //                    //oComDia1.iComidaInt++;
                                        //                    //oComDia1.iCenaInt++;
                                        //                }
                                        //                else
                                        //                {
                                        //                    oCant.iCantDesayunos++;
                                        //                    //oCant.iCantComidas++;
                                        //                    //oCant.iCantCenas++;

                                        //                    oComDia1.iDesayunosNal++;
                                        //                    //oComDia1.iComidaNal++;
                                        //                    //oComDia1.iCenaNal++;
                                        //                }
                                        //            }
                                        //            else
                                        //            {
                                        //                if (l == 0)
                                        //                {
                                        //                    if (bEsInterFinal)
                                        //                    {
                                        //                        oCant.iCantCenasInt++;
                                        //                        oComDia1.iCenaInt++;
                                        //                    }
                                        //                    else
                                        //                    {
                                        //                        oCant.iCantCenas++;
                                        //                        oComDia1.iCenaNal++;
                                        //                    }
                                        //                }
                                        //            }

                                        //            oLsComDia.Add(oComDia1);
                                        //        }
                                        //    }
                                        //}
                                        //else if (dtDias.Rows.Count > 2)
                                        //{
                                        //    for (int l = 0; l < dtDias.Rows.Count; l++)
                                        //    {
                                        //        DataRow[] rowsD = dtLegs.Select("Dia = '" + dtDias.Rows[l]["Dia"].S() + "' ");

                                        //        bEsInterInicio = rowsD[0]["EsInternacional"].S() == "1" ? true : false;
                                        //        bEsInterFinal = rowsD[rowsD.Length - 1]["EsInternacional"].S() == "1" ? true : false;

                                        //        // una pierna que tiene mas de un dia.
                                        //        if (rowsD.Length == 1 && ((rowsD[0][FechaFin].S().Dt().Day) - (rowsD[0][FechaInicio].S().Dt().Day)) > 0)
                                        //        {
                                        //            bool bTieneDiasAntes = false;
                                        //            bool bTieneDiasDespues = false;

                                        //            if (l > 0)
                                        //                bTieneDiasAntes = true;

                                        //            try
                                        //            {
                                        //                if (dtDias.Rows[l + 1] != null)
                                        //                    bTieneDiasDespues = true;
                                        //            }
                                        //            catch (Exception)
                                        //            {
                                        //                bTieneDiasDespues = false;
                                        //            }

                                        //            //if (l > 0)
                                        //            //    bTieneDiasAntes = true;
                                        //            //if (dtDias.Rows[l + 1] != null)
                                        //            //    bTieneDiasDespues = true;

                                        //            CalculaAlimentos(rowsD[0][FechaInicio].S().Dt(), rowsD[0][FechaFin].S().Dt(), oCant, oHor, sPod, sBase, bTieneDiasAntes, bTieneDiasDespues, bEsInterInicio, bEsInterFinal, drP["ClavePiloto"].S(), oLsComDia, rowsD[0]["DutyType"].S(), rowsD[0]["POD"].S(), rowsD[0]["POA"].S(), dtLegs);
                                        //        }
                                        //        else if (rowsD.Length > 1)
                                        //        {
                                        //            for (int k = 0; k < rowsD.Length; k++)
                                        //            {
                                        //                ComidasPorDia oComDia1 = new ComidasPorDia();
                                        //                oComDia1.sClavePiloto = drP["ClavePiloto"].S();
                                        //                oComDia1.dtFechaDia = rowsD[k][FechaInicio].S().Dt();
                                        //                oComDia1.dtFechaFin = rowsD[k][FechaFin].S().Dt();
                                        //                oComDia1.sduty_type = rowsD[k]["DutyType"].S();
                                        //                oComDia1.sOrigen = rowsD[k]["POD"].S();
                                        //                oComDia1.sDestino = rowsD[k]["POA"].S();

                                        //                if (k + 1 == rowsD.Length)
                                        //                {
                                        //                    DateTime dtIniDia = rowsD[0][FechaInicio].S().Dt();
                                        //                    DateTime dtFinDia = rowsD[rowsD.Length - 1][FechaFin].S().Dt();

                                        //                    fHoraInicio = dtIniDia.Hour + (dtIniDia.Minute / float.Parse("60"));
                                        //                    fHoraFinal = dtFinDia.Hour + (dtFinDia.Minute / float.Parse("60"));

                                        //                    bEsInterInicio = rowsD[k]["PaisPOD"].S() == "MX" ? false : true;

                                        //                    if (bEsInterInicio != bEsInterFinal)
                                        //                    {
                                        //                        if ((fHoraInicio < fInicioCena && fHoraFinal >= fInicioCena) || (fHoraInicio <= fFinCena && fHoraFinal > fInicioCena))
                                        //                        {
                                        //                            if (bEsInterFinal)
                                        //                            {
                                        //                                if (l > 0)
                                        //                                {
                                        //                                    if (!bEsInterInicio)
                                        //                                    {
                                        //                                        oCant.iCantDesayunos++;
                                        //                                        oCant.iCantComidas++;

                                        //                                        oComDia1.iDesayunosNal++;
                                        //                                        oComDia1.iComidaNal++;
                                        //                                    }
                                        //                                }

                                        //                                oCant.iCantCenasInt++;
                                        //                                oComDia1.iCenaInt++;
                                        //                            }
                                        //                            else
                                        //                            {
                                        //                                if (l > 0)
                                        //                                {
                                        //                                    if (bEsInterInicio)
                                        //                                    {
                                        //                                        oCant.iCantDesayunosInt++;
                                        //                                        oCant.iCantComidasInt++;

                                        //                                        oComDia1.iDesayunosInt++;
                                        //                                        oComDia1.iComidaInt++;
                                        //                                    }
                                        //                                }

                                        //                                oCant.iCantCenas++;
                                        //                                oComDia1.iCenaNal++;
                                        //                            }
                                        //                        }
                                        //                        else if ((fHoraInicio <= fInicioComida && (fHoraFinal > fInicioComida)) || (fHoraInicio > fInicioComida && fHoraInicio <= fFinComida) || (fHoraInicio < fInicioComida && fHoraFinal > fFinComida))
                                        //                        {
                                        //                            if (bEsInterFinal)
                                        //                            {
                                        //                                if (l > 0)
                                        //                                {
                                        //                                    if (!bEsInterInicio)
                                        //                                    {
                                        //                                        oCant.iCantDesayunos++;
                                        //                                        oComDia1.iDesayunosNal++;
                                        //                                    }
                                        //                                }


                                        //                                oCant.iCantComidasInt++;
                                        //                                oCant.iCantCenasInt++;

                                        //                                oComDia1.iComidaInt++;
                                        //                                oComDia1.iCenaInt++;
                                        //                            }
                                        //                            else
                                        //                            {
                                        //                                if (l > 0)
                                        //                                {
                                        //                                    if (bEsInterInicio)
                                        //                                    {
                                        //                                        oCant.iCantDesayunosInt++;
                                        //                                        oComDia1.iDesayunosInt++;
                                        //                                    }
                                        //                                }

                                        //                                oCant.iCantComidas++;
                                        //                                oCant.iCantCenas++;

                                        //                                oComDia1.iComidaNal++;
                                        //                                oComDia1.iCenaNal++;
                                        //                            }
                                        //                        }
                                        //                        else if (fHoraInicio < fInicioDesayuno || (fHoraInicio > fInicioDesayuno && fHoraInicio < fFinDesayuno))
                                        //                        {
                                        //                            if (bEsInterFinal)
                                        //                            {
                                        //                                oCant.iCantDesayunosInt++;
                                        //                                oCant.iCantComidasInt++;
                                        //                                oCant.iCantCenasInt++;

                                        //                                oComDia1.iDesayunosInt++;
                                        //                                oComDia1.iComidaInt++;
                                        //                                oComDia1.iCenaInt++;
                                        //                            }
                                        //                            else
                                        //                            {
                                        //                                oComDia1.iDesayunosNal++;
                                        //                                oComDia1.iComidaNal++;
                                        //                                oComDia1.iCenaNal++;

                                        //                                oCant.iCantDesayunos++;
                                        //                                oCant.iCantComidas++;
                                        //                                oCant.iCantCenas++;
                                        //                            }
                                        //                        }
                                        //                    }
                                        //                    else
                                        //                    {
                                        //                        int iAux = 0;

                                        //                        if (fHoraInicio < fInicioDesayuno || (fHoraInicio > fInicioDesayuno && fHoraInicio < fFinDesayuno))
                                        //                        {
                                        //                            if (bEsInterFinal)
                                        //                            {
                                        //                                oCant.iCantDesayunosInt++;
                                        //                                oCant.iCantComidasInt++;
                                        //                                oCant.iCantCenasInt++;

                                        //                                oComDia1.iDesayunosInt++;
                                        //                                oComDia1.iComidaInt++;
                                        //                                oComDia1.iCenaInt++;
                                        //                            }
                                        //                            else
                                        //                            {
                                        //                                oComDia1.iDesayunosNal++;
                                        //                                oComDia1.iComidaNal++;
                                        //                                oComDia1.iCenaNal++;
                                        //                            }

                                        //                            iAux = 1;
                                        //                        }
                                        //                        else if ((fHoraInicio <= fInicioComida && (fHoraFinal > fInicioComida)) || (fHoraInicio > fInicioComida && fHoraInicio <= fFinComida) || (fHoraInicio < fInicioComida && fHoraFinal > fFinComida))
                                        //                        {
                                        //                            // Valida si tiene un día anterior

                                        //                            if (bEsInterFinal)
                                        //                            {
                                        //                                if (l != 0 || l != dtDias.Rows.Count - 1)
                                        //                                {
                                        //                                    oCant.iCantDesayunosInt++;
                                        //                                    oComDia1.iDesayunosInt++;
                                        //                                }

                                        //                                oCant.iCantComidasInt++;
                                        //                                oCant.iCantCenasInt++;

                                        //                                oComDia1.iComidaInt++;
                                        //                                oComDia1.iCenaInt++;
                                        //                            }
                                        //                            else
                                        //                            {
                                        //                                if (l != 0 || l != dtDias.Rows.Count - 1)
                                        //                                {
                                        //                                    oCant.iCantDesayunos++;
                                        //                                    oComDia1.iDesayunosNal++;
                                        //                                }

                                        //                                oCant.iCantComidas++;
                                        //                                oCant.iCantCenas++;

                                        //                                oComDia1.iComidaNal++;
                                        //                                oComDia1.iCenaNal++;
                                        //                            }

                                        //                            iAux = 2;
                                        //                        }
                                        //                        else if ((fHoraInicio < fInicioCena && fHoraFinal >= fInicioCena) || (fHoraInicio <= fFinCena && fHoraFinal > fInicioCena))
                                        //                        {
                                        //                            if (bEsInterFinal)
                                        //                            {
                                        //                                if (l != 0 || l != dtDias.Rows.Count - 1)
                                        //                                {
                                        //                                    oCant.iCantDesayunosInt++;
                                        //                                    oCant.iCantComidasInt++;

                                        //                                    oComDia1.iDesayunosInt++;
                                        //                                    oComDia1.iComidaInt++;
                                        //                                }

                                        //                                oCant.iCantCenasInt++;
                                        //                                oComDia1.iCenaInt++;
                                        //                            }
                                        //                            else
                                        //                            {
                                        //                                if (l != 0 || l != dtDias.Rows.Count - 1)
                                        //                                {
                                        //                                    oCant.iCantDesayunos++;
                                        //                                    oCant.iCantComidas++;

                                        //                                    oComDia1.iDesayunosNal++;
                                        //                                    oComDia1.iComidaNal++;
                                        //                                }

                                        //                                oCant.iCantCenas++;
                                        //                                oComDia1.iCenaNal++;
                                        //                            }

                                        //                            iAux = 3;
                                        //                        }

                                        //                        if (iAux == 0)
                                        //                        {
                                        //                            if (bEsInterInicio)
                                        //                            {
                                        //                                if (l == 0 || l == dtDias.Rows.Count - 1)
                                        //                                {
                                        //                                    if (fHoraFinal > fFinComida)
                                        //                                    {
                                        //                                        oCant.iCantDesayunosInt++;
                                        //                                        oCant.iCantComidasInt++;

                                        //                                        oComDia1.iDesayunosInt++;
                                        //                                        oComDia1.iComidaInt++;
                                        //                                    }
                                        //                                    else
                                        //                                    {
                                        //                                        oCant.iCantDesayunosInt++;
                                        //                                        oComDia1.iDesayunosInt++;
                                        //                                    }
                                        //                                }
                                        //                            }
                                        //                            else
                                        //                            {
                                        //                                if (l == 0 || l == dtDias.Rows.Count - 1)
                                        //                                {
                                        //                                    if (fHoraFinal > fFinComida)
                                        //                                    {
                                        //                                        oCant.iCantDesayunos++;
                                        //                                        oCant.iCantComidas++;

                                        //                                        oComDia1.iDesayunosNal++;
                                        //                                        oComDia1.iComidaNal++;
                                        //                                    }
                                        //                                    else
                                        //                                    {
                                        //                                        oCant.iCantDesayunos++;
                                        //                                        oComDia1.iDesayunosNal++;
                                        //                                    }
                                        //                                }
                                        //                            }
                                        //                        }
                                        //                    }


                                        //                }

                                        //                oLsComDia.Add(oComDia1);
                                        //            }
                                        //        }
                                        //        else
                                        //        {
                                        //            if (l == 0 || l == dtDias.Rows.Count - 1)
                                        //            {
                                        //                ComidasPorDia oCom = new ComidasPorDia();
                                        //                oCom.sClavePiloto = drP["ClavePiloto"].S();

                                        //                dtIni = rowsD[0][FechaInicio].S().Dt();
                                        //                dtFin = rowsD[rowsD.Length - 1][FechaFin].S().Dt();

                                        //                if (l == 0)
                                        //                {
                                        //                    oCom.dtFechaDia = dtIni;
                                        //                    oCom.dtFechaFin = dtFin;

                                        //                    oCom.sduty_type = rowsD[l]["DutyType"].S();
                                        //                    oCom.sOrigen = rowsD[l]["POD"].S();
                                        //                    oCom.sDestino = rowsD[l]["POA"].S();
                                        //                }
                                        //                else
                                        //                {
                                        //                    oCom.dtFechaDia = dtIni;
                                        //                    oCom.dtFechaFin = dtFin;

                                        //                    oCom.sduty_type = rowsD[rowsD.Length - 1]["DutyType"].S();
                                        //                    oCom.sOrigen = rowsD[rowsD.Length - 1]["POD"].S();
                                        //                    oCom.sDestino = rowsD[rowsD.Length - 1]["POA"].S();
                                        //                }


                                        //                fHoraInicio = dtIni.Hour + (dtIni.Minute / float.Parse("60"));
                                        //                fHoraFinal = dtFin.Hour + (dtFin.Minute / float.Parse("60"));

                                        //                bEsInterInicio = rowsD[0]["PaisPOD"].S() == "MX" ? false : true;
                                        //                bEsInterFinal = rowsD[rowsD.Length - 1]["EsInternacional"].S() == "1" ? true : false;

                                        //                int iAux = 0;

                                        //                if (fHoraInicio < fInicioDesayuno || (fHoraInicio > fInicioDesayuno && fHoraInicio < fFinDesayuno))
                                        //                {
                                        //                    if (bEsInterFinal)
                                        //                    {
                                        //                        oCant.iCantDesayunosInt++;
                                        //                        oCant.iCantComidasInt++;
                                        //                        oCant.iCantCenasInt++;

                                        //                        oCom.iDesayunosInt++;
                                        //                        oCom.iComidaInt++;
                                        //                        oCom.iCenaInt++;
                                        //                    }
                                        //                    else
                                        //                    {
                                        //                        oCom.iDesayunosNal++;
                                        //                        oCom.iComidaNal++;
                                        //                        oCom.iCenaNal++;
                                        //                    }

                                        //                    iAux = 1;
                                        //                }
                                        //                else if ((fHoraInicio <= fInicioComida && (fHoraFinal > fInicioComida)) || (fHoraInicio > fInicioComida && fHoraInicio <= fFinComida) || (fHoraInicio < fInicioComida && fHoraFinal > fFinComida))
                                        //                {
                                        //                    // Valida si tiene un día anterior

                                        //                    if (bEsInterFinal)
                                        //                    {
                                        //                        if (l != 0 || l != dtDias.Rows.Count - 1)
                                        //                        {
                                        //                            oCant.iCantDesayunosInt++;
                                        //                            oCom.iDesayunosInt++;
                                        //                        }

                                        //                        oCant.iCantComidasInt++;
                                        //                        oCant.iCantCenasInt++;

                                        //                        oCom.iComidaInt++;
                                        //                        oCom.iCenaInt++;
                                        //                    }
                                        //                    else
                                        //                    {
                                        //                        if (l != 0 || l != dtDias.Rows.Count - 1)
                                        //                        {
                                        //                            oCant.iCantDesayunos++;
                                        //                            oCom.iDesayunosNal++;
                                        //                        }

                                        //                        oCant.iCantComidas++;
                                        //                        oCant.iCantCenas++;

                                        //                        oCom.iComidaNal++;
                                        //                        oCom.iCenaNal++;
                                        //                    }

                                        //                    iAux = 2;
                                        //                }
                                        //                else if ((fHoraInicio < fInicioCena && fHoraFinal >= fInicioCena) || (fHoraInicio <= fFinCena && fHoraFinal > fInicioCena))
                                        //                {
                                        //                    if (bEsInterFinal)
                                        //                    {
                                        //                        if (l != 0 || l != dtDias.Rows.Count - 1)
                                        //                        {
                                        //                            oCant.iCantDesayunosInt++;
                                        //                            oCant.iCantComidasInt++;

                                        //                            oCom.iDesayunosInt++;
                                        //                            oCom.iComidaInt++;
                                        //                        }

                                        //                        oCant.iCantCenasInt++;
                                        //                        oCom.iCenaInt++;
                                        //                    }
                                        //                    else
                                        //                    {
                                        //                        if (l != 0 || l != dtDias.Rows.Count - 1)
                                        //                        {
                                        //                            oCant.iCantDesayunos++;
                                        //                            oCant.iCantComidas++;

                                        //                            oCom.iDesayunosNal++;
                                        //                            oCom.iComidaNal++;
                                        //                        }

                                        //                        oCant.iCantCenas++;
                                        //                        oCom.iCenaNal++;
                                        //                    }

                                        //                    iAux = 3;
                                        //                }

                                        //                if (iAux == 0)
                                        //                {
                                        //                    if (bEsInterInicio)
                                        //                    {
                                        //                        if (l == 0 || l == dtDias.Rows.Count - 1)
                                        //                        {
                                        //                            if (fHoraFinal > fFinComida)
                                        //                            {
                                        //                                oCant.iCantDesayunosInt++;
                                        //                                oCant.iCantComidasInt++;

                                        //                                oCom.iDesayunosInt++;
                                        //                                oCom.iComidaInt++;
                                        //                            }
                                        //                            else
                                        //                            {
                                        //                                oCant.iCantDesayunosInt++;
                                        //                                oCom.iDesayunosInt++;
                                        //                            }
                                        //                        }
                                        //                    }
                                        //                    else
                                        //                    {
                                        //                        if (l == 0 || l == dtDias.Rows.Count - 1)
                                        //                        {
                                        //                            if (fHoraFinal > fFinComida)
                                        //                            {
                                        //                                oCant.iCantDesayunos++;
                                        //                                oCant.iCantComidas++;

                                        //                                oCom.iDesayunosNal++;
                                        //                                oCom.iComidaNal++;
                                        //                            }
                                        //                            else
                                        //                            {
                                        //                                oCant.iCantDesayunos++;
                                        //                                oCom.iDesayunosNal++;
                                        //                            }
                                        //                        }
                                        //                    }
                                        //                }

                                        //                #region COMENTADO
                                        //                //if (fHoraInicio < fInicioDesayuno || (fHoraInicio > fInicioDesayuno && fHoraInicio < fFinDesayuno) || (fHoraInicio >= fInicioDesayuno && rowsD[0]["POD"].S() != sBase))
                                        //                //{
                                        //                //    if (bEsInterFinal)
                                        //                //    {
                                        //                //        oCant.iCantDesayunosInt++;

                                        //                //        oCom.iDesayunosInt++;
                                        //                //    }
                                        //                //    else
                                        //                //    {
                                        //                //        oCant.iCantDesayunos++;

                                        //                //        oCom.iDesayunosNal++;
                                        //                //    }

                                        //                //    iAux = 1;
                                        //                //}
                                        //                //if ((fHoraInicio <= fInicioComida && (fHoraFinal > fInicioComida)) || (fHoraInicio > fInicioComida && fHoraInicio <= fFinComida) || (fHoraInicio < fInicioComida && fHoraFinal > fFinComida))
                                        //                //{
                                        //                //    if (bEsInterFinal)
                                        //                //    {
                                        //                //        oCant.iCantComidasInt++;

                                        //                //        oCom.iComidaInt++;
                                        //                //    }
                                        //                //    else
                                        //                //    {
                                        //                //        oCant.iCantComidas++;

                                        //                //        oCom.iComidaNal++;
                                        //                //    }

                                        //                //    iAux = 2;
                                        //                //}
                                        //                //if ((fHoraInicio < fInicioCena && fHoraFinal > fInicioCena) || (fHoraInicio <= fFinCena && fHoraFinal > fInicioCena))
                                        //                //{
                                        //                //    if (bEsInterFinal)
                                        //                //    {
                                        //                //        oCant.iCantCenasInt++;

                                        //                //        oCom.iCenaInt++;
                                        //                //    }
                                        //                //    else
                                        //                //    {
                                        //                //        oCant.iCantCenas++;

                                        //                //        oCom.iCenaNal++;
                                        //                //    }

                                        //                //    iAux = 3;
                                        //                //}

                                        //                //if (iAux == 1)
                                        //                //{
                                        //                //    if (l != dtDias.Rows.Count - 1)
                                        //                //    {
                                        //                //        if (bEsInterFinal)
                                        //                //        {
                                        //                //            oCant.iCantComidasInt++;
                                        //                //            oCant.iCantCenasInt++;

                                        //                //            oCom.iCenaInt++;
                                        //                //            oCom.iComidaInt++;
                                        //                //        }
                                        //                //        else
                                        //                //        {
                                        //                //            oCant.iCantCenas++;
                                        //                //            oCant.iCantComidas++;

                                        //                //            oCom.iCenaNal++;
                                        //                //            oCom.iComidaNal++;
                                        //                //        }
                                        //                //    }
                                        //                //}
                                        //                //if (iAux == 2)
                                        //                //{
                                        //                //    if (l != dtDias.Rows.Count - 1)
                                        //                //    {
                                        //                //        if (bEsInterFinal)
                                        //                //        {
                                        //                //            oCant.iCantCenasInt++;
                                        //                //            oCom.iCenaInt++;
                                        //                //        }
                                        //                //        else
                                        //                //        {
                                        //                //            oCant.iCantCenas++;
                                        //                //            oCom.iCenaNal++;
                                        //                //        }
                                        //                //    }
                                        //                //}
                                        //                #endregion

                                        //                oLsComDia.Add(oCom);
                                        //            }
                                        //            else
                                        //            {
                                        //                DataTable dtDias2 = dtDias;
                                        //                DateTime dtIniAux = rowsD[0][FechaInicio].S().Dt();
                                        //                DateTime dtFinAux = rowsD[0][FechaFin].S().Dt();

                                        //                bEsInterInicio = rowsD[0]["PaisPOD"].S() == "MX" ? false : true;

                                        //                ComidasPorDia oCom = new ComidasPorDia();
                                        //                oCom.sClavePiloto = drP["ClavePiloto"].S();
                                        //                oCom.dtFechaDia = dtIniAux;// dtIni.AddDays(l);
                                        //                oCom.dtFechaFin = dtFinAux;// dtFin.AddDays(l);
                                        //                oCom.sduty_type = rowsD[0]["DutyType"].S();
                                        //                oCom.sOrigen = rowsD[0]["POD"].S();
                                        //                oCom.sDestino = rowsD[0]["POA"].S();


                                        //                if (bEsInterInicio != bEsInterFinal)
                                        //                {
                                        //                    if ((fHoraInicio < fInicioCena && fHoraFinal >= fInicioCena) || (fHoraInicio <= fFinCena && fHoraFinal > fInicioCena))
                                        //                    {
                                        //                        if (bEsInterFinal)
                                        //                        {
                                        //                            if (l > 0)
                                        //                            {
                                        //                                if (!bEsInterInicio)
                                        //                                {
                                        //                                    oCant.iCantDesayunos++;
                                        //                                    oCant.iCantComidas++;

                                        //                                    oCom.iDesayunosNal++;
                                        //                                    oCom.iComidaNal++;
                                        //                                }
                                        //                            }

                                        //                            oCant.iCantCenasInt++;
                                        //                            oCom.iCenaInt++;
                                        //                        }
                                        //                        else
                                        //                        {
                                        //                            if (l > 0)
                                        //                            {
                                        //                                if (bEsInterInicio)
                                        //                                {
                                        //                                    oCant.iCantDesayunosInt++;
                                        //                                    oCant.iCantComidasInt++;

                                        //                                    oCom.iDesayunosInt++;
                                        //                                    oCom.iComidaInt++;
                                        //                                }
                                        //                            }

                                        //                            oCant.iCantCenas++;
                                        //                            oCom.iCenaNal++;
                                        //                        }
                                        //                    }
                                        //                    else if ((fHoraInicio <= fInicioComida && (fHoraFinal > fInicioComida)) || (fHoraInicio > fInicioComida && fHoraInicio <= fFinComida) || (fHoraInicio < fInicioComida && fHoraFinal > fFinComida))
                                        //                    {
                                        //                        if (bEsInterFinal)
                                        //                        {
                                        //                            if (l > 0)
                                        //                            {
                                        //                                if (!bEsInterInicio)
                                        //                                {
                                        //                                    oCant.iCantDesayunos++;
                                        //                                    oCom.iDesayunosNal++;
                                        //                                }
                                        //                            }


                                        //                            oCant.iCantComidasInt++;
                                        //                            oCant.iCantCenasInt++;

                                        //                            oCom.iComidaInt++;
                                        //                            oCom.iCenaInt++;
                                        //                        }
                                        //                        else
                                        //                        {
                                        //                            if (l > 0)
                                        //                            {
                                        //                                if (bEsInterInicio)
                                        //                                {
                                        //                                    oCant.iCantDesayunosInt++;
                                        //                                    oCom.iDesayunosInt++;
                                        //                                }
                                        //                            }

                                        //                            oCant.iCantComidas++;
                                        //                            oCant.iCantCenas++;

                                        //                            oCom.iComidaNal++;
                                        //                            oCom.iCenaNal++;
                                        //                        }
                                        //                    }
                                        //                    else if (fHoraInicio < fInicioDesayuno || (fHoraInicio > fInicioDesayuno && fHoraInicio < fFinDesayuno))
                                        //                    {
                                        //                        if (bEsInterFinal)
                                        //                        {
                                        //                            oCant.iCantDesayunosInt++;
                                        //                            oCant.iCantComidasInt++;
                                        //                            oCant.iCantCenasInt++;

                                        //                            oCom.iDesayunosInt++;
                                        //                            oCom.iComidaInt++;
                                        //                            oCom.iCenaInt++;
                                        //                        }
                                        //                        else
                                        //                        {
                                        //                            oCom.iDesayunosNal++;
                                        //                            oCom.iComidaNal++;
                                        //                            oCom.iCenaNal++;

                                        //                            oCant.iCantDesayunos++;
                                        //                            oCant.iCantComidas++;
                                        //                            oCant.iCantCenas++;
                                        //                        }
                                        //                    }
                                        //                }
                                        //                else
                                        //                {
                                        //                    if (bEsInterFinal)
                                        //                    {
                                        //                        oCant.iCantDesayunosInt++;
                                        //                        oCant.iCantComidasInt++;
                                        //                        oCant.iCantCenasInt++;

                                        //                        oCom.iDesayunosInt++;
                                        //                        oCom.iComidaInt++;
                                        //                        oCom.iCenaInt++;
                                        //                    }
                                        //                    else
                                        //                    {
                                        //                        oCant.iCantDesayunos++;
                                        //                        oCant.iCantComidas++;
                                        //                        oCant.iCantCenas++;

                                        //                        oCom.iDesayunosNal++;
                                        //                        oCom.iComidaNal++;
                                        //                        oCom.iCenaNal++;
                                        //                    }
                                        //                }

                                        //                oLsComDia.Add(oCom);
                                        //            }
                                        //        }
                                        //    }
                                        //}
                                        #endregion

                                        oCant.oLstPorDia = oLsComDia;
                                        ObtieneDiasViaticos(oLsComDia);
                                    }

                                    oLstCant.Add(oCant);
                                }

                                if (rowsNC.Length > 0)
                                {
                                    oIGestCat.ActualizaVuelosNoCobrables(rowsNC);
                                }

                            }
                        }
                    }
                    #endregion

                    #region CUARTA PARTE DEL CODIGO
                    GC.Collect();
                    oIView.oLstCant = oLstCant;
                    dtComidas = DBGetObtieneTablaCalculos(oLstCant, dtPilotos);

                }
                //Fin de for piloto por piloto
                oIView.LlenaCalculoPilotos(dtComidas);
                oIView.LlenaVuelosPiloto(ds.Tables[0]);
                #endregion
            }
            catch (Exception ex)
            {
                string sPiloto = sCve;
            }
            
        }

        public void ObtieneDiasViaticos(List<ComidasPorDia> oLstAlimentos)
        {
            try
            {
                for (int i = 0; i < oLstAlimentos.Count; i++)
                {
                    ComidasPorDia oCom = new ComidasPorDia();
                    oCom = oLstAlimentos[i];

                    if (oIView.dtDiasViaticos == null)
                    {
                        DataRow dr;
                        oIView.dtDiasViaticos = new DataTable();
                        oIView.dtDiasViaticos.Columns.Add("FechaDia");
                        oIView.dtDiasViaticos.Columns.Add("DesNal");
                        oIView.dtDiasViaticos.Columns.Add("ComNal");
                        oIView.dtDiasViaticos.Columns.Add("CenNal");
                        oIView.dtDiasViaticos.Columns.Add("DesInt");
                        oIView.dtDiasViaticos.Columns.Add("ComInt");
                        oIView.dtDiasViaticos.Columns.Add("CenInt");

                        dr = oIView.dtDiasViaticos.NewRow();
                        dr["FechaDia"] = oCom.dtFechaDia;
                        dr["DesNal"] = oCom.iDesayunosNal;
                        dr["ComNal"] = oCom.iComidaNal;
                        dr["CenNal"] = oCom.iCenaNal;
                        dr["DesInt"] = oCom.iDesayunosInt;
                        dr["ComInt"] = oCom.iComidaInt;
                        dr["CenInt"] = oCom.iCenaInt;
                        oIView.dtDiasViaticos.Rows.Add(dr);
                    }
                    else
                    {
                        DataRow dr;
                        dr = oIView.dtDiasViaticos.NewRow();
                        dr["FechaDia"] = oCom.dtFechaDia;
                        dr["DesNal"] = oCom.iDesayunosNal;
                        dr["ComNal"] = oCom.iComidaNal;
                        dr["CenNal"] = oCom.iCenaNal;
                        dr["DesInt"] = oCom.iDesayunosInt;
                        dr["ComInt"] = oCom.iComidaInt;
                        dr["CenInt"] = oCom.iCenaInt;
                        oIView.dtDiasViaticos.Rows.Add(dr);
                    }
                }

                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private DataTable CreaEstructuraVuelos()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("NoVuelo");
            dt.Columns.Add("NoPierna");
            dt.Columns.Add("LegId");
            dt.Columns.Add("FechaDia");
            dt.Columns.Add("POD");
            dt.Columns.Add("POA");
            dt.Columns.Add("FechaSalida");
            dt.Columns.Add("FechaLlegada");
            dt.Columns.Add("LocDep");
            dt.Columns.Add("LocArr");
            dt.Columns.Add("CrewCode");
            dt.Columns.Add("DutyType");
            dt.Columns.Add("HomeBase");
            dt.Columns.Add("EsInternacional");
            dt.Columns.Add("SeCobra");
            dt.Columns.Add("Dia");
            dt.Columns.Add("PaisPOD");
            dt.Columns.Add("PaisPOA");
            dt.Columns.Add("CheckIn");
            dt.Columns.Add("CheckOut");

            return dt;
        }
        private void ObtienePernoctasYDayUse(CantidadComidas oCant, DataTable dtLegs, string sFechaInicio, string sFechaFin, int iHorasPernocta, int iHorasDay)
        {
            try
            {
                for (int i = 0; i < dtLegs.Rows.Count; i++)
                {
                    int iInternacional = dtLegs.Rows[i]["EsInternacional"].S().I();
                    if (iInternacional == 1)
                    {
                        bool bTieneDiasAntes = true;
                        bool bTieneDiasDespues = true;


                        DateTime dtInicio = dtLegs.Rows[i][sFechaInicio].S().Dt();
                        DateTime dtFin = dtLegs.Rows[i][sFechaFin].S().Dt();


                        DateTime dtSig = new DateTime();

                        if (dtLegs.Rows.Count > (i + 1))
                            dtSig = dtLegs.Rows[i + 1][sFechaInicio].S().Dt();

                        if (i == 0)
                            bTieneDiasAntes = false;
                        if (i + 1 == dtLegs.Rows.Count)
                            bTieneDiasDespues = false;

                        if (i + 1 < dtLegs.Rows.Count)
                        {
                            // Si el vuelo inicia y termina el mismo día, hacer calculos con el día siguiente
                            if (dtInicio.Day == dtFin.Day)
                            {
                                TimeSpan timeDif = dtSig - dtFin;

                                // El sig vuelo es al dia siguiente
                                if (dtFin.AddDays(1).Day == dtSig.Day)
                                {
                                    if (timeDif.Hours >= iHorasPernocta)
                                    {
                                        PernoctasDayUse oPer = new PernoctasDayUse();
                                        oPer.dtFecha = dtInicio;
                                        oPer.iNoPernoctas++;

                                        oCant.oLstPer.Add(oPer);
                                    }
                                }

                                if (dtFin.Day == dtSig.Day)
                                {
                                    if (timeDif.Hours >= iHorasDay)
                                    {
                                        PernoctasDayUse oPer = new PernoctasDayUse();
                                        oPer.dtFecha = dtInicio;
                                        oPer.iDayUse++;

                                        oCant.oLstPer.Add(oPer);
                                    }
                                }
                            }   // Si el vuelo inicia un día y termina al otro (vuelos a la media noche)
                            else if (dtInicio.AddDays(1).Day == dtFin.Day)
                            {
                                TimeSpan timeDif = dtSig - dtFin;

                                if (dtFin.AddDays(1).Day == dtSig.Day)
                                {
                                    if (timeDif.Hours >= iHorasPernocta)
                                    {
                                        PernoctasDayUse oPer = new PernoctasDayUse();
                                        oPer.dtFecha = dtInicio;
                                        oPer.iNoPernoctas++;

                                        oCant.oLstPer.Add(oPer);
                                    }
                                }
                                else if (dtFin.Day == dtSig.Day)
                                {
                                    if (timeDif.Hours >= iHorasDay)
                                    {
                                        PernoctasDayUse oPer = new PernoctasDayUse();
                                        oPer.dtFecha = dtInicio;
                                        oPer.iDayUse++;

                                        oCant.oLstPer.Add(oPer);
                                    }
                                }
                            }
                            else if (dtInicio.AddDays(2).Day <= dtFin.Day)
                            {
                                // la pierna no termina el mismo día que inicio
                                int iDias = dtFin.Day - dtInicio.Day;
                                for (int j = 0; j < iDias; j++)
                                {
                                    PernoctasDayUse oPer = new PernoctasDayUse();
                                    oPer.dtFecha = dtInicio.AddDays(j);
                                    oPer.iNoPernoctas++;

                                    oCant.oLstPer.Add(oPer);
                                }

                                if (bTieneDiasDespues)
                                {
                                    PernoctasDayUse oPer = new PernoctasDayUse();
                                    oPer.dtFecha = dtFin;
                                    oPer.iNoPernoctas++;

                                    oCant.oLstPer.Add(oPer);
                                }
                            }
                        }
                        else // es la ultima posición
                        {
                            // ya no se asignan pernoctas porque regresa a Base
                        }


                        //oCant.oLstPer.Add(oPer);
                    }
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private DataTable CreaEstructuraDias()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("NoVuelo");
            dt.Columns.Add("Dia");

            return dt;
        }
        private bool VerificaExisteValor(DataTable dt, string sValor, string sColumna)
        {
            try
            {
                bool ban = false;

                foreach (DataRow dr in dt.Rows)
                {
                    if (dr[sColumna].S() == sValor)
                    {
                        ban = true;
                        break;
                    }
                }

                return ban;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private CantidadComidas CalculaAlimentos(DateTime dtInicial, DateTime dtFinal, CantidadComidas oCant, HorarioAlimentos oHor,
            string sPOD, string sBase, bool bTieneDiasAntes, bool bTieneDiasDespues, bool bEsInterInicio, bool bEsInterFinal, string sClavePiloto,
            List<ComidasPorDia> oLst, string sDutyType, string sPODLeg, string sPOA, DataTable dtLegs)
        {
            try
            {
                float fInicioDesayuno = oHor.fInicioDesayuno;
                float fFinDesayuno = oHor.fFinDesayuno;
                float fInicioComida = oHor.fInicioComida;
                float fFinComida = oHor.fFinComida;
                float fInicioCena = oHor.fInicioCena;
                float fFinCena = oHor.fFinCena;

                //int iHoraInicio = dtInicial.Hour;
                //int iHoraFinal = dtFinal.Hour;

                float fHoraInicio = dtInicial.Hour + (dtInicial.Minute / float.Parse("60"));
                float fHoraFinal = dtFinal.Hour + (dtFinal.Minute / float.Parse("60"));


                int iDif = dtFinal.Day - dtInicial.Day;

                if (iDif == 0)
                {
                    ComidasPorDia oCom = new ComidasPorDia();
                    oCom.sClavePiloto = sClavePiloto;
                    oCom.dtFechaDia = dtInicial;

                    oCom.sduty_type = sDutyType;
                    oCom.sOrigen = sPOD;
                    oCom.sDestino = sPOA;

                    #region DESAYUNO
                    //if (fHoraInicio < fInicioDesayuno || (fHoraInicio > fInicioDesayuno && fHoraInicio < fFinDesayuno) || (fHoraInicio >= fInicioDesayuno && sPOD != sBase))
                    //{
                    //    if (bEsInterFinal)
                    //        oCant.iCantDesayunosInt++;
                    //    else
                    //        oCant.iCantDesayunos++;
                    //}

                    //Primera condicion
                    if (((fHoraInicio < fInicioDesayuno && fHoraFinal > fInicioDesayuno) && fHoraFinal < fFinDesayuno) && (sPOD != sBase))
                    {
                        //if (bEsInterFinal) { oCant.iCantDesayunosInt++; oComDia1.iDesayunosInt++; } else { oCant.iCantDesayunos++; oComDia1.iDesayunosNal++; }
                        if (bEsInterFinal)
                            oCant.iCantDesayunosInt++;
                        else
                            oCant.iCantDesayunos++;
                    }
                    //Segunda condición
                    else if ((fHoraInicio > fInicioDesayuno && fHoraInicio < fFinDesayuno) && (fHoraFinal > fFinDesayuno && fHoraFinal > fInicioDesayuno) && (sPOD == sBase) && (fHoraFinal < fInicioComida)) //Evaluar antes de comida
                    {
                        //if (bEsInterFinal) { oCant.iCantDesayunosInt++; oComDia1.iDesayunosInt++; } else { oCant.iCantDesayunos++; oComDia1.iDesayunosNal++; }
                        if (bEsInterFinal)
                            oCant.iCantDesayunosInt++;
                        else
                            oCant.iCantDesayunos++;
                    }
                    //Tercera condición
                    else if ((fHoraInicio >= fInicioDesayuno && fHoraFinal > fFinDesayuno) && (sPOD != sBase || sPOD == sBase) && (fHoraFinal < fInicioComida))
                    {
                        if (bEsInterFinal)
                            oCant.iCantDesayunosInt++;
                        else
                            oCant.iCantDesayunos++;
                    }
                    //Cuarta condición
                    else if ((fHoraInicio < fInicioDesayuno && fHoraFinal <= fInicioDesayuno) && (sPOD != sBase))
                    {
                        if (bEsInterFinal)
                            oCant.iCantDesayunosInt++;
                        else
                            oCant.iCantDesayunos++;
                    }
                    //Quinta condición
                    else if ((fHoraInicio < fInicioDesayuno && fHoraFinal < fInicioDesayuno) && (sPOD != sBase))
                    {
                        if (bEsInterFinal)
                            oCant.iCantDesayunosInt++;
                        else
                            oCant.iCantDesayunos++;
                    }
                    //Sexta condición
                    else if ((fHoraInicio < fInicioDesayuno && (fHoraFinal > fInicioDesayuno && fHoraFinal <= fFinDesayuno)) && (sPOD != sBase || sPOD == sBase))
                    {
                        if (bEsInterFinal)
                            oCant.iCantDesayunosInt++;
                        else
                            oCant.iCantDesayunos++;
                    }
                    //Septima condición
                    else if ((fHoraInicio >= fInicioDesayuno && fHoraInicio <= fFinDesayuno) && (fHoraFinal > fInicioDesayuno && fHoraFinal <= fFinDesayuno) && (sPOD != sBase || sPOD == sBase))
                    {
                        if (bEsInterFinal)
                            oCant.iCantDesayunosInt++;
                        else
                            oCant.iCantDesayunos++;
                    }

                    //Octava condición
                    else if ((fHoraInicio < fInicioDesayuno && (fHoraFinal > fInicioDesayuno && fHoraFinal >= fFinDesayuno)) && (sPOD != sBase || sPOD == sBase) && (fHoraFinal < fInicioComida))
                    {
                        if (bEsInterFinal)
                            oCant.iCantDesayunosInt++;
                        else
                            oCant.iCantDesayunos++;
                    }
                    #endregion

                    #region COMIDA
                    //if ((fHoraInicio < fInicioComida && (fHoraFinal > fInicioComida)) || (fHoraInicio > fInicioComida && fHoraInicio < fFinComida) || (fHoraInicio < fInicioComida && fHoraFinal > fFinComida))
                    //{
                    //    if (bEsInterFinal)
                    //        oCant.iCantComidasInt++;
                    //    else
                    //        oCant.iCantComidas++;
                    //}

                    //Primera condicion Comidas
                    if (((fHoraInicio < fInicioComida && fHoraFinal > fInicioComida) && fHoraFinal < fFinComida) && (sPOD != sBase))
                    {
                        if (bEsInterFinal)
                            oCant.iCantComidasInt++;
                        else
                            oCant.iCantComidas++;
                    }
                    //Segunda condición Comidas
                    else if ((fHoraInicio > fInicioComida && fHoraInicio < fFinComida) && (fHoraFinal > fFinComida && fHoraFinal > fInicioComida) && (sPOD == sBase) && (fHoraFinal < fInicioCena)) //Evaluar antes de cena
                    {
                        if (bEsInterFinal)
                            oCant.iCantComidasInt++;
                        else
                            oCant.iCantComidas++;
                    }
                    //Tercera condición Comidas
                    else if ((fHoraInicio >= fInicioComida && fHoraFinal > fFinComida) && (sPOD != sBase || sPOD == sBase) && (fHoraFinal < fInicioCena))
                    {
                        if (bEsInterFinal)
                            oCant.iCantComidasInt++;
                        else
                            oCant.iCantComidas++;
                    }
                    //Cuarta condición Comidas
                    else if ((fHoraInicio < fInicioComida && fHoraFinal <= fInicioComida) && (sPOD != sBase))
                    {
                        if (bEsInterFinal)
                            oCant.iCantComidasInt++;
                        else
                            oCant.iCantComidas++;
                    }
                    //Quinta condición Comidas
                    else if ((fHoraInicio < fInicioComida && fHoraFinal < fInicioComida) && (sPOD != sBase))
                    {
                        if (bEsInterFinal)
                            oCant.iCantComidasInt++;
                        else
                            oCant.iCantComidas++;
                    }
                    //Sexta condición Comidas
                    else if ((fHoraInicio < fInicioComida && (fHoraFinal > fInicioComida && fHoraFinal <= fFinComida)) && (sPOD != sBase || sPOD == sBase))
                    {
                        if (bEsInterFinal)
                            oCant.iCantComidasInt++;
                        else
                            oCant.iCantComidas++;
                    }
                    //Septima condición Comidas
                    else if ((fHoraInicio >= fInicioComida && fHoraInicio <= fFinComida) && (fHoraFinal > fInicioComida && fHoraFinal <= fFinComida) && (sPOD != sBase || sPOD == sBase))
                    {
                        if (bEsInterFinal)
                            oCant.iCantComidasInt++;
                        else
                            oCant.iCantComidas++;
                    }
                    //Octava condición
                    else if ((fHoraInicio < fInicioComida && (fHoraFinal > fInicioComida && fHoraFinal >= fFinComida)) && (sPOD != sBase || sPOD == sBase) && (fHoraFinal < fInicioCena))
                    {
                        if (bEsInterFinal)
                            oCant.iCantComidasInt++;
                        else
                            oCant.iCantComidas++;
                    }
                    #endregion

                    #region CENA
                    //if ((fHoraInicio < fInicioCena && fHoraFinal > fInicioCena) || (fHoraInicio <= fFinCena && fHoraFinal > fInicioCena))
                    //{
                    //    if (bEsInterFinal)
                    //        oCant.iCantCenasInt++;
                    //    else
                    //        oCant.iCantCenas++;
                    //}

                    //Primera condicion Cenas
                    if (((fHoraInicio < fInicioCena && fHoraFinal > fInicioCena) && fHoraFinal < fFinCena) && (sPOD != sBase))
                    {
                        if (bEsInterFinal)
                            oCant.iCantCenasInt++;
                        else
                            oCant.iCantCenas++;
                    }
                    //Segunda condición Cenas
                    else if ((fHoraInicio > fInicioCena && fHoraInicio < fFinCena) && (fHoraFinal > fFinCena && fHoraFinal > fInicioCena) && (sPOD == sBase))
                    {
                        if (bEsInterFinal)
                            oCant.iCantCenasInt++;
                        else
                            oCant.iCantCenas++;
                    }
                    //Tercera condición Cenas
                    else if ((fHoraInicio >= fInicioCena && fHoraFinal > fFinCena) && (sPOD != sBase || sPOD == sBase))
                    {
                        if (bEsInterFinal)
                            oCant.iCantCenasInt++;
                        else
                            oCant.iCantCenas++;
                    }
                    //Cuarta condición Cenas
                    else if ((fHoraInicio < fInicioCena && fHoraFinal <= fInicioCena) && (sPOD != sBase))
                    {
                        if (bEsInterFinal)
                            oCant.iCantCenasInt++;
                        else
                            oCant.iCantCenas++;
                    }
                    //Quinta condición Cenas
                    else if ((fHoraInicio < fInicioCena && fHoraFinal < fInicioCena) && (sPOD != sBase))
                    {
                        if (bEsInterFinal)
                            oCant.iCantCenasInt++;
                        else
                            oCant.iCantCenas++;
                    }
                    //Sexta condición Cenas
                    else if ((fHoraInicio < fInicioCena && (fHoraFinal > fInicioCena && fHoraFinal <= fFinCena)) && (sPOD != sBase || sPOD == sBase))
                    {
                        if (bEsInterFinal)
                            oCant.iCantCenasInt++;
                        else
                            oCant.iCantCenas++;
                    }
                    //Septima condición Cenas
                    else if ((fHoraInicio >= fInicioCena && fHoraInicio <= fFinCena) && (fHoraFinal > fInicioCena && fHoraFinal <= fFinCena) && (sPOD != sBase || sPOD == sBase))
                    {
                        if (bEsInterFinal)
                            oCant.iCantCenasInt++;
                        else
                            oCant.iCantCenas++;
                    }
                    //Octava condición Cenas
                    else if ((fHoraInicio < fInicioCena && (fHoraFinal > fInicioCena && fHoraFinal >= fFinCena)) && (sPOD != sBase || sPOD == sBase))
                    {
                        if (bEsInterFinal)
                            oCant.iCantCenasInt++;
                        else
                            oCant.iCantCenas++;
                    }
                    #endregion

                    oCom.iDesayunosNal = oCant.iCantDesayunos;
                    oCom.iDesayunosInt = oCant.iCantDesayunosInt;
                    oCom.iComidaNal = oCant.iCantComidas;
                    oCom.iComidaInt = oCant.iCantComidasInt;
                    oCom.iCenaNal = oCant.iCantCenas;
                    oCom.iCenaInt = oCant.iCantCenasInt;

                    oLst.Add(oCom);
                }
                else if (iDif == 1)
                {
                    ComidasPorDia oComDia1 = new ComidasPorDia();
                    oComDia1.sClavePiloto = sClavePiloto;
                    oComDia1.sduty_type = sDutyType;
                    oComDia1.sOrigen = sPOD;
                    oComDia1.sDestino = sPOA;

                    #region COMENTADO CONDICIONES ANTERIORES
                    // DIA INICIAL
                    if (fHoraInicio < fInicioDesayuno || (fHoraInicio > fInicioDesayuno && fHoraInicio < fFinDesayuno))
                    {
                        if (bEsInterFinal)
                        {
                            oCant.iCantDesayunosInt++;
                            oCant.iCantComidasInt++;
                            oCant.iCantCenasInt++;
                        }
                        else
                        {
                            oCant.iCantDesayunos++;
                            oCant.iCantComidas++;
                            oCant.iCantCenas++;
                        }
                    }
                    else if (fHoraInicio < fInicioComida && fHoraInicio > fFinDesayuno && fHoraInicio < fFinComida)
                    {
                        if (bEsInterFinal)
                        {
                            oCant.iCantComidasInt++;
                            oCant.iCantCenasInt++;
                        }
                        else
                        {
                            oCant.iCantComidas++;
                            oCant.iCantCenas++;
                        }
                    }
                    else if (fHoraInicio < fInicioCena && fHoraInicio > fFinComida && fHoraInicio < fFinCena)
                    {
                        if (bEsInterFinal)
                            oCant.iCantCenasInt++;
                        else
                            oCant.iCantCenas++;
                    }
                    #endregion

                    #region DESAYUNO
                    //Primera condicion
                    if (((fHoraInicio < fInicioDesayuno && fHoraFinal > fInicioDesayuno) && fHoraFinal < fFinDesayuno) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                    {
                        if (bEsInterFinal)
                        {
                            oCant.iCantDesayunosInt++;
                            //oCant.iCantComidasInt++;
                            //oCant.iCantCenasInt++;
                        }
                        else
                        {
                            oCant.iCantDesayunos++;
                            //oCant.iCantComidas++;
                            //oCant.iCantCenas++;
                        }
                    }
                    //Segunda condición
                    else if ((fHoraInicio > fInicioDesayuno && fHoraInicio < fFinDesayuno) && (fHoraFinal > fFinDesayuno && fHoraFinal > fInicioDesayuno) && (oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioComida)) //Evaluar antes de comida
                    {
                        if (bEsInterFinal)
                        {
                            oCant.iCantDesayunosInt++;
                            //oCant.iCantComidasInt++;
                            //oCant.iCantCenasInt++;
                        }
                        else
                        {
                            oCant.iCantDesayunos++;
                            //oCant.iCantComidas++;
                            //oCant.iCantCenas++;
                        }
                    }
                    //Tercera condición
                    else if ((fHoraInicio >= fInicioDesayuno && fHoraFinal > fFinDesayuno) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioComida))
                    {
                        if (bEsInterFinal)
                        {
                            oCant.iCantDesayunosInt++;
                            //oCant.iCantComidasInt++;
                            //oCant.iCantCenasInt++;
                        }
                        else
                        {
                            oCant.iCantDesayunos++;
                            //oCant.iCantComidas++;
                            //oCant.iCantCenas++;
                        }
                    }
                    //Cuarta condición
                    else if ((fHoraInicio < fInicioDesayuno && fHoraFinal <= fInicioDesayuno) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                    {
                        if (bEsInterFinal)
                        {
                            oCant.iCantDesayunosInt++;
                            //oCant.iCantComidasInt++;
                            //oCant.iCantCenasInt++;
                        }
                        else
                        {
                            oCant.iCantDesayunos++;
                            //oCant.iCantComidas++;
                            //oCant.iCantCenas++;
                        }
                    }
                    //Quinta condición
                    else if ((fHoraInicio < fInicioDesayuno && fHoraFinal < fInicioDesayuno) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                    {
                        if (bEsInterFinal)
                        {
                            oCant.iCantDesayunosInt++;
                            //oCant.iCantComidasInt++;
                            //oCant.iCantCenasInt++;
                        }
                        else
                        {
                            oCant.iCantDesayunos++;
                            //oCant.iCantComidas++;
                            //oCant.iCantCenas++;
                        }
                    }
                    //Sexta condición
                    else if ((fHoraInicio < fInicioDesayuno && (fHoraFinal > fInicioDesayuno && fHoraFinal <= fFinDesayuno)) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                    {
                        if (bEsInterFinal)
                        {
                            oCant.iCantDesayunosInt++;
                            //oCant.iCantComidasInt++;
                            //oCant.iCantCenasInt++;
                        }
                        else
                        {
                            oCant.iCantDesayunos++;
                            //oCant.iCantComidas++;
                            //oCant.iCantCenas++;
                        }
                    }
                    //Septima condición
                    else if ((fHoraInicio >= fInicioDesayuno && fHoraInicio <= fFinDesayuno) && (fHoraFinal > fInicioDesayuno && fHoraFinal <= fFinDesayuno) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                    {
                        if (bEsInterFinal)
                        {
                            oCant.iCantDesayunosInt++;
                            //oCant.iCantComidasInt++;
                            //oCant.iCantCenasInt++;
                        }
                        else
                        {
                            oCant.iCantDesayunos++;
                            //oCant.iCantComidas++;
                            //oCant.iCantCenas++;
                        }
                    }
                    //Octava condición
                    else if ((fHoraInicio < fInicioDesayuno && (fHoraFinal > fInicioDesayuno && fHoraFinal >= fFinDesayuno)) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioComida))
                    {
                        if (bEsInterFinal)
                        {
                            oCant.iCantDesayunosInt++;
                            //oCant.iCantComidasInt++;
                            //oCant.iCantCenasInt++;
                        }
                        else
                        {
                            oCant.iCantDesayunos++;
                            //oCant.iCantComidas++;
                            //oCant.iCantCenas++;
                        }
                    }
                    #endregion

                    #region COMIDA
                    //Primera condicion Comidas
                    if (((fHoraInicio < fInicioComida && fHoraFinal > fInicioComida) && fHoraFinal < fFinComida) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioCena))
                    {
                        if (bEsInterFinal)
                        {
                            oCant.iCantComidasInt++;
                            //oCant.iCantCenasInt++;
                        }
                        else
                        {
                            oCant.iCantComidas++;
                            //oCant.iCantCenas++;
                        }
                    }
                    //Segunda condición Comidas
                    else if ((fHoraInicio > fInicioComida && fHoraInicio < fFinComida) && (fHoraFinal > fFinComida && fHoraFinal > fInicioComida) && (oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioCena)) //Evaluar antes de cena
                    {
                        if (bEsInterFinal)
                        {
                            oCant.iCantComidasInt++;
                            //oCant.iCantCenasInt++;
                        }
                        else
                        {
                            oCant.iCantComidas++;
                            //oCant.iCantCenas++;
                        }
                    }
                    //Tercera condición Comidas
                    else if ((fHoraInicio >= fInicioComida && fHoraFinal > fFinComida) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioCena))
                    {
                        if (bEsInterFinal)
                        {
                            oCant.iCantComidasInt++;
                            //oCant.iCantCenasInt++;
                        }
                        else
                        {
                            oCant.iCantComidas++;
                            //oCant.iCantCenas++;
                        }
                    }
                    //Cuarta condición Comidas
                    else if ((fHoraInicio < fInicioComida && fHoraFinal <= fInicioComida) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioCena))
                    {
                        if (bEsInterFinal)
                        {
                            oCant.iCantComidasInt++;
                            //oCant.iCantCenasInt++;
                        }
                        else
                        {
                            oCant.iCantComidas++;
                            //oCant.iCantCenas++;
                        }
                    }
                    //Quinta condición Comidas
                    else if ((fHoraInicio < fInicioComida && fHoraFinal < fInicioComida) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioCena))
                    {
                        if (bEsInterFinal)
                        {
                            oCant.iCantComidasInt++;
                            //oCant.iCantCenasInt++;
                        }
                        else
                        {
                            oCant.iCantComidas++;
                            //oCant.iCantCenas++;
                        }
                    }
                    //Sexta condición Comidas
                    else if ((fHoraInicio < fInicioComida && (fHoraFinal > fInicioComida && fHoraFinal <= fFinComida)) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioCena))
                    {
                        if (bEsInterFinal)
                        {
                            oCant.iCantComidasInt++;
                            //oCant.iCantCenasInt++;
                        }
                        else
                        {
                            oCant.iCantComidas++;
                            //oCant.iCantCenas++;
                        }
                    }
                    //Septima condición Comidas
                    else if ((fHoraInicio >= fInicioComida && fHoraInicio <= fFinComida) && (fHoraFinal > fInicioComida && fHoraFinal <= fFinComida) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioCena))
                    {
                        if (bEsInterFinal)
                        {
                            oCant.iCantComidasInt++;
                            //oCant.iCantCenasInt++;
                        }
                        else
                        {
                            oCant.iCantComidas++;
                            //oCant.iCantCenas++;
                        }
                    }
                    //Octava condición
                    else if ((fHoraInicio < fInicioComida && (fHoraFinal > fInicioComida && fHoraFinal >= fFinComida)) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioCena))
                    {
                        if (bEsInterFinal)
                        {
                            oCant.iCantComidasInt++;
                            //oCant.iCantCenasInt++;
                        }
                        else
                        {
                            oCant.iCantComidas++;
                            //oCant.iCantCenas++;
                        }
                    }
                    #endregion

                    #region CENA
                    //Primera condicion Cenas
                    if (((fHoraInicio < fInicioCena && fHoraFinal > fInicioCena) && fHoraFinal < fFinCena) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                    {
                        if (bEsInterFinal)
                            oCant.iCantCenasInt++;
                        else
                            oCant.iCantCenas++;
                    }
                    //Segunda condición Cenas
                    else if ((fHoraInicio > fInicioCena && fHoraInicio < fFinCena) && (fHoraFinal > fFinCena && fHoraFinal > fInicioCena) && (oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                    {
                        if (bEsInterFinal)
                            oCant.iCantCenasInt++;
                        else
                            oCant.iCantCenas++;
                    }
                    //Tercera condición Cenas
                    else if ((fHoraInicio >= fInicioCena && fHoraFinal > fFinCena) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                    {
                        if (bEsInterFinal)
                            oCant.iCantCenasInt++;
                        else
                            oCant.iCantCenas++;
                    }
                    //Cuarta condición Cenas
                    else if ((fHoraInicio < fInicioCena && fHoraFinal <= fInicioCena) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                    {
                        if (bEsInterFinal)
                            oCant.iCantCenasInt++;
                        else
                            oCant.iCantCenas++;
                    }
                    //Quinta condición Cenas
                    else if ((fHoraInicio < fInicioCena && fHoraFinal < fInicioCena) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                    {
                        if (bEsInterFinal)
                            oCant.iCantCenasInt++;
                        else
                            oCant.iCantCenas++;
                    }
                    //Sexta condición Cenas
                    else if ((fHoraInicio < fInicioCena && (fHoraFinal > fInicioCena && fHoraFinal <= fFinCena)) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                    {
                        if (bEsInterFinal)
                            oCant.iCantCenasInt++;
                        else
                            oCant.iCantCenas++;
                    }
                    //Septima condición Cenas
                    else if ((fHoraInicio >= fInicioCena && fHoraInicio <= fFinCena) && (fHoraFinal > fInicioCena && fHoraFinal <= fFinCena) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                    {
                        if (bEsInterFinal)
                            oCant.iCantCenasInt++;
                        else
                            oCant.iCantCenas++;
                    }
                    //Octava condición Cenas
                    else if ((fHoraInicio < fInicioCena && (fHoraFinal > fInicioCena && fHoraFinal >= fFinCena)) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                    {
                        if (bEsInterFinal)
                            oCant.iCantCenasInt++;
                        else
                            oCant.iCantCenas++;
                    }
                    #endregion

                    oComDia1.dtFechaDia = dtInicial;
                    oComDia1.iDesayunosNal = oCant.iCantDesayunos;
                    oComDia1.iDesayunosInt = oCant.iCantDesayunosInt;
                    oComDia1.iComidaNal = oCant.iCantComidas;
                    oComDia1.iComidaInt = oCant.iCantComidasInt;
                    oComDia1.iCenaNal = oCant.iCantCenas;
                    oComDia1.iCenaInt = oCant.iCantCenasInt;

                    oLst.Add(oComDia1);


                    // DIA FINAL
                    ComidasPorDia oComDia2 = new ComidasPorDia();
                    oComDia2.sClavePiloto = sClavePiloto;
                    oComDia2.dtFechaDia = dtFinal;
                    oComDia2.sduty_type = sDutyType;
                    oComDia2.sOrigen = sPOD;
                    oComDia2.sDestino = sPOA;

                    #region COMENTADO CONDICIONES ANTERIORES
                    //if (fHoraFinal > fFinCena || (fHoraFinal > fInicioCena && fHoraFinal < fFinCena))
                    //{
                    //    if (bEsInterFinal)
                    //    {
                    //        oCant.iCantDesayunosInt++;
                    //        oCant.iCantComidasInt++;
                    //        oCant.iCantCenasInt++;

                    //        oComDia2.iDesayunosInt++;
                    //        oComDia2.iComidaInt++;
                    //        oComDia2.iCenaInt++;
                    //    }
                    //    else
                    //    {
                    //        oCant.iCantDesayunos++;
                    //        oCant.iCantComidas++;
                    //        oCant.iCantCenas++;

                    //        oComDia2.iDesayunosNal++;
                    //        oComDia2.iComidaNal++;
                    //        oComDia2.iCenaNal++;
                    //    }
                    //}
                    //else if (fHoraFinal < fInicioCena && fHoraFinal > fInicioComida)
                    //{
                    //    if (bEsInterFinal)
                    //    {
                    //        oCant.iCantComidasInt++;
                    //        oCant.iCantCenasInt++;

                    //        oComDia2.iComidaInt++;
                    //        oComDia2.iCenaInt++;
                    //    }
                    //    else
                    //    {
                    //        oCant.iCantComidas++;
                    //        oCant.iCantCenas++;

                    //        oComDia2.iComidaNal++;
                    //        oComDia2.iCenaNal++;
                    //    }
                    //}
                    //else if (fHoraFinal < fInicioComida && fHoraFinal > fInicioDesayuno)
                    //{
                    //    if (bEsInterFinal)
                    //    {
                    //        oCant.iCantDesayunosInt++;

                    //        oComDia2.iDesayunosInt++;
                    //    }
                    //    else
                    //    {
                    //        oCant.iCantDesayunos++;

                    //        oComDia2.iDesayunosNal++;
                    //    }
                    //}
                    #endregion

                    #region DESAYUNO
                    //Primera condicion
                    if (((fHoraInicio < fInicioDesayuno && fHoraFinal > fInicioDesayuno) && fHoraFinal < fFinDesayuno) && (oComDia2.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                    {
                        if (bEsInterFinal)
                        {
                            oCant.iCantDesayunosInt++;
                            oComDia2.iDesayunosInt++;
                        }
                        else
                        {
                            oCant.iCantDesayunos++;
                            oComDia2.iDesayunosNal++;
                        }
                    }
                    //Segunda condición
                    else if ((fHoraInicio > fInicioDesayuno && fHoraInicio < fFinDesayuno) && (fHoraFinal > fFinDesayuno && fHoraFinal > fInicioDesayuno) && (oComDia2.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioComida)) //Evaluar antes de comida
                    {
                        if (bEsInterFinal)
                        {
                            oCant.iCantDesayunosInt++;
                            oComDia2.iDesayunosInt++;
                        }
                        else
                        {
                            oCant.iCantDesayunos++;
                            oComDia2.iDesayunosNal++;
                        }
                    }
                    //Tercera condición
                    else if ((fHoraInicio >= fInicioDesayuno && fHoraFinal > fFinDesayuno) && (oComDia2.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia2.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioComida))
                    {
                        if (bEsInterFinal)
                        {
                            oCant.iCantDesayunosInt++;
                            oComDia2.iDesayunosInt++;
                        }
                        else
                        {
                            oCant.iCantDesayunos++;
                            oComDia2.iDesayunosNal++;
                        }
                    }
                    //Cuarta condición
                    else if ((fHoraInicio < fInicioDesayuno && fHoraFinal <= fInicioDesayuno) && (oComDia2.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                    {
                        if (bEsInterFinal)
                        {
                            oCant.iCantDesayunosInt++;
                            oComDia2.iDesayunosInt++;
                        }
                        else
                        {
                            oCant.iCantDesayunos++;
                            oComDia2.iDesayunosNal++;
                        }
                    }
                    //Quinta condición
                    else if ((fHoraInicio < fInicioDesayuno && fHoraFinal < fInicioDesayuno) && (oComDia2.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                    {
                        if (bEsInterFinal)
                        {
                            oCant.iCantDesayunosInt++;
                            oComDia2.iDesayunosInt++;
                        }
                        else
                        {
                            oCant.iCantDesayunos++;
                            oComDia2.iDesayunosNal++;
                        }
                    }
                    //Sexta condición
                    else if ((fHoraInicio < fInicioDesayuno && (fHoraFinal > fInicioDesayuno && fHoraFinal <= fFinDesayuno)) && (oComDia2.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia2.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                    {
                        if (bEsInterFinal)
                        {
                            oCant.iCantDesayunosInt++;
                            oComDia2.iDesayunosInt++;
                        }
                        else
                        {
                            oCant.iCantDesayunos++;
                            oComDia2.iDesayunosNal++;
                        }
                    }
                    //Septima condición
                    else if ((fHoraInicio >= fInicioDesayuno && fHoraInicio <= fFinDesayuno) && (fHoraFinal > fInicioDesayuno && fHoraFinal <= fFinDesayuno) && (oComDia2.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia2.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                    {
                        if (bEsInterFinal)
                        {
                            oCant.iCantDesayunosInt++;
                            oComDia2.iDesayunosInt++;
                        }
                        else
                        {
                            oCant.iCantDesayunos++;
                            oComDia2.iDesayunosNal++;
                        }
                    }
                    //Octava condición
                    else if ((fHoraInicio < fInicioDesayuno && (fHoraFinal > fInicioDesayuno && fHoraFinal >= fFinDesayuno)) && (oComDia2.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia2.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioComida))
                    {
                        if (bEsInterFinal)
                        {
                            oCant.iCantDesayunosInt++;
                            oComDia2.iDesayunosInt++;
                        }
                        else
                        {
                            oCant.iCantDesayunos++;
                            oComDia2.iDesayunosNal++;
                        }
                    }
                    #endregion

                    #region COMIDA
                    //Primera condicion Comidas
                    if (((fHoraInicio < fInicioComida && fHoraFinal > fInicioComida) && fHoraFinal < fFinComida) && (oComDia2.sDestino != dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioCena))
                    {
                        if (bEsInterFinal)
                        {
                            oCant.iCantComidasInt++;
                            //oCant.iCantCenasInt++;

                            oComDia2.iComidaInt++;
                            //oComDia2.iCenaInt++;
                        }
                        else
                        {
                            oCant.iCantComidas++;
                            //oCant.iCantCenas++;

                            oComDia2.iComidaNal++;
                            //oComDia2.iCenaNal++;
                        }
                    }
                    //Segunda condición Comidas
                    else if ((fHoraInicio > fInicioComida && fHoraInicio < fFinComida) && (fHoraFinal > fFinComida && fHoraFinal > fInicioComida) && (oComDia2.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioCena)) //Evaluar antes de cena
                    {
                        if (bEsInterFinal)
                        {
                            oCant.iCantComidasInt++;
                            //oCant.iCantCenasInt++;

                            oComDia2.iComidaInt++;
                            //oComDia2.iCenaInt++;
                        }
                        else
                        {
                            oCant.iCantComidas++;
                            //oCant.iCantCenas++;

                            oComDia2.iComidaNal++;
                            //oComDia2.iCenaNal++;
                        }
                    }
                    //Tercera condición Comidas
                    else if ((fHoraInicio >= fInicioComida && fHoraFinal > fFinComida) && (oComDia2.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia2.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioCena))
                    {
                        if (bEsInterFinal)
                        {
                            oCant.iCantComidasInt++;
                            //oCant.iCantCenasInt++;

                            oComDia2.iComidaInt++;
                            //oComDia2.iCenaInt++;
                        }
                        else
                        {
                            oCant.iCantComidas++;
                            //oCant.iCantCenas++;

                            oComDia2.iComidaNal++;
                            //oComDia2.iCenaNal++;
                        }
                    }
                    //Cuarta condición Comidas
                    else if ((fHoraInicio < fInicioComida && fHoraFinal <= fInicioComida) && (oComDia2.sDestino != dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioCena))
                    {
                        if (bEsInterFinal)
                        {
                            oCant.iCantComidasInt++;
                            //oCant.iCantCenasInt++;

                            oComDia2.iComidaInt++;
                            //oComDia2.iCenaInt++;
                        }
                        else
                        {
                            oCant.iCantComidas++;
                            //oCant.iCantCenas++;

                            oComDia2.iComidaNal++;
                            //oComDia2.iCenaNal++;
                        }
                    }
                    //Quinta condición Comidas
                    else if ((fHoraInicio < fInicioComida && fHoraFinal < fInicioComida) && (oComDia2.sDestino != dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioCena))
                    {
                        if (bEsInterFinal)
                        {
                            oCant.iCantComidasInt++;
                            //oCant.iCantCenasInt++;

                            oComDia2.iComidaInt++;
                            //oComDia2.iCenaInt++;
                        }
                        else
                        {
                            oCant.iCantComidas++;
                            //oCant.iCantCenas++;

                            oComDia2.iComidaNal++;
                            //oComDia2.iCenaNal++;
                        }
                    }
                    //Sexta condición Comidas
                    else if ((fHoraInicio < fInicioComida && (fHoraFinal > fInicioComida && fHoraFinal <= fFinComida)) && (oComDia2.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia2.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioCena))
                    {
                        if (bEsInterFinal)
                        {
                            oCant.iCantComidasInt++;
                            //oCant.iCantCenasInt++;

                            oComDia2.iComidaInt++;
                            //oComDia2.iCenaInt++;
                        }
                        else
                        {
                            oCant.iCantComidas++;
                            //oCant.iCantCenas++;

                            oComDia2.iComidaNal++;
                            //oComDia2.iCenaNal++;
                        }
                    }
                    //Septima condición Comidas
                    else if ((fHoraInicio >= fInicioComida && fHoraInicio <= fFinComida) && (fHoraFinal > fInicioComida && fHoraFinal <= fFinComida) && (oComDia2.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia2.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioCena))
                    {
                        if (bEsInterFinal)
                        {
                            oCant.iCantComidasInt++;
                            //oCant.iCantCenasInt++;

                            oComDia2.iComidaInt++;
                            //oComDia2.iCenaInt++;
                        }
                        else
                        {
                            oCant.iCantComidas++;
                            //oCant.iCantCenas++;

                            oComDia2.iComidaNal++;
                            //oComDia2.iCenaNal++;
                        }
                    }
                    //Octava condición
                    else if ((fHoraInicio < fInicioComida && (fHoraFinal > fInicioComida && fHoraFinal >= fFinComida)) && (oComDia2.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia2.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioCena))
                    {
                        if (bEsInterFinal)
                        {
                            oCant.iCantComidasInt++;
                            //oCant.iCantCenasInt++;

                            oComDia2.iComidaInt++;
                            //oComDia2.iCenaInt++;
                        }
                        else
                        {
                            oCant.iCantComidas++;
                            //oCant.iCantCenas++;

                            oComDia2.iComidaNal++;
                            //oComDia2.iCenaNal++;
                        }
                    }
                    #endregion

                    #region CENA
                    //Primera condicion Cenas
                    if (((fHoraInicio < fInicioCena && fHoraFinal > fInicioCena) && fHoraFinal < fFinCena) && (oComDia2.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                    {
                        if (bEsInterFinal)
                        {
                            //oCant.iCantDesayunosInt++;
                            //oCant.iCantComidasInt++;
                            oCant.iCantCenasInt++;

                            //oComDia2.iDesayunosInt++;
                            //oComDia2.iComidaInt++;
                            oComDia2.iCenaInt++;
                        }
                        else
                        {
                            //oCant.iCantDesayunos++;
                            //oCant.iCantComidas++;
                            oCant.iCantCenas++;

                            //oComDia2.iDesayunosNal++;
                            //oComDia2.iComidaNal++;
                            oComDia2.iCenaNal++;
                        }
                    }
                    //Segunda condición Cenas
                    else if ((fHoraInicio > fInicioCena && fHoraInicio < fFinCena) && (fHoraFinal > fFinCena && fHoraFinal > fInicioCena) && (oComDia2.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                    {
                        if (bEsInterFinal)
                        {
                            //oCant.iCantDesayunosInt++;
                            //oCant.iCantComidasInt++;
                            oCant.iCantCenasInt++;

                            //oComDia2.iDesayunosInt++;
                            //oComDia2.iComidaInt++;
                            oComDia2.iCenaInt++;
                        }
                        else
                        {
                            //oCant.iCantDesayunos++;
                            //oCant.iCantComidas++;
                            oCant.iCantCenas++;

                            //oComDia2.iDesayunosNal++;
                            //oComDia2.iComidaNal++;
                            oComDia2.iCenaNal++;
                        }
                    }
                    //Tercera condición Cenas
                    else if ((fHoraInicio >= fInicioCena && fHoraFinal > fFinCena) && (oComDia2.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia2.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                    {
                        if (bEsInterFinal)
                        {
                            //oCant.iCantDesayunosInt++;
                            //oCant.iCantComidasInt++;
                            oCant.iCantCenasInt++;

                            //oComDia2.iDesayunosInt++;
                            //oComDia2.iComidaInt++;
                            oComDia2.iCenaInt++;
                        }
                        else
                        {
                            //oCant.iCantDesayunos++;
                            //oCant.iCantComidas++;
                            oCant.iCantCenas++;

                            //oComDia2.iDesayunosNal++;
                            //oComDia2.iComidaNal++;
                            oComDia2.iCenaNal++;
                        }
                    }
                    //Cuarta condición Cenas
                    else if ((fHoraInicio < fInicioCena && fHoraFinal <= fInicioCena) && (oComDia2.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                    {
                        if (bEsInterFinal)
                        {
                            //oCant.iCantDesayunosInt++;
                            //oCant.iCantComidasInt++;
                            oCant.iCantCenasInt++;

                            //oComDia2.iDesayunosInt++;
                            //oComDia2.iComidaInt++;
                            oComDia2.iCenaInt++;
                        }
                        else
                        {
                            //oCant.iCantDesayunos++;
                            //oCant.iCantComidas++;
                            oCant.iCantCenas++;

                            //oComDia2.iDesayunosNal++;
                            //oComDia2.iComidaNal++;
                            oComDia2.iCenaNal++;
                        }
                    }
                    //Quinta condición Cenas
                    else if ((fHoraInicio < fInicioCena && fHoraFinal < fInicioCena) && (oComDia2.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                    {
                        if (bEsInterFinal)
                        {
                            //oCant.iCantDesayunosInt++;
                            //oCant.iCantComidasInt++;
                            oCant.iCantCenasInt++;

                            //oComDia2.iDesayunosInt++;
                            //oComDia2.iComidaInt++;
                            oComDia2.iCenaInt++;
                        }
                        else
                        {
                            //oCant.iCantDesayunos++;
                            //oCant.iCantComidas++;
                            oCant.iCantCenas++;

                            //oComDia2.iDesayunosNal++;
                            //oComDia2.iComidaNal++;
                            oComDia2.iCenaNal++;
                        }
                    }
                    //Sexta condición Cenas
                    else if ((fHoraInicio < fInicioCena && (fHoraFinal > fInicioCena && fHoraFinal <= fFinCena)) && (oComDia2.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia2.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                    {
                        if (bEsInterFinal)
                        {
                            //oCant.iCantDesayunosInt++;
                            //oCant.iCantComidasInt++;
                            oCant.iCantCenasInt++;

                            //oComDia2.iDesayunosInt++;
                            //oComDia2.iComidaInt++;
                            oComDia2.iCenaInt++;
                        }
                        else
                        {
                            //oCant.iCantDesayunos++;
                            //oCant.iCantComidas++;
                            oCant.iCantCenas++;

                            //oComDia2.iDesayunosNal++;
                            //oComDia2.iComidaNal++;
                            oComDia2.iCenaNal++;
                        }
                    }
                    //Septima condición Cenas
                    else if ((fHoraInicio >= fInicioCena && fHoraInicio <= fFinCena) && (fHoraFinal > fInicioCena && fHoraFinal <= fFinCena) && (oComDia2.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia2.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                    {
                        if (bEsInterFinal)
                        {
                            //oCant.iCantDesayunosInt++;
                            //oCant.iCantComidasInt++;
                            oCant.iCantCenasInt++;

                            //oComDia2.iDesayunosInt++;
                            //oComDia2.iComidaInt++;
                            oComDia2.iCenaInt++;
                        }
                        else
                        {
                            //oCant.iCantDesayunos++;
                            //oCant.iCantComidas++;
                            oCant.iCantCenas++;

                            //oComDia2.iDesayunosNal++;
                            //oComDia2.iComidaNal++;
                            oComDia2.iCenaNal++;
                        }
                    }
                    //Octava condición Cenas
                    else if ((fHoraInicio < fInicioCena && (fHoraFinal > fInicioCena && fHoraFinal >= fFinCena)) && (oComDia2.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia2.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                    {
                        if (bEsInterFinal)
                        {
                            //oCant.iCantDesayunosInt++;
                            //oCant.iCantComidasInt++;
                            oCant.iCantCenasInt++;

                            //oComDia2.iDesayunosInt++;
                            //oComDia2.iComidaInt++;
                            oComDia2.iCenaInt++;
                        }
                        else
                        {
                            //oCant.iCantDesayunos++;
                            //oCant.iCantComidas++;
                            oCant.iCantCenas++;

                            //oComDia2.iDesayunosNal++;
                            //oComDia2.iComidaNal++;
                            oComDia2.iCenaNal++;
                        }
                    }
                    #endregion

                    oLst.Add(oComDia2);
                }
                else if (iDif > 1)
                {
                    int iRestantes = iDif - 1;

                    for (int i = 0; i < iRestantes; i++)
                    {
                        ComidasPorDia oCom = new ComidasPorDia();
                        oCom.sClavePiloto = sClavePiloto;

                        DateTime dtIniaux = dtInicial.AddDays(i + 1);

                        oCom.dtFechaDia = new DateTime(dtIniaux.Year, dtIniaux.Month, dtIniaux.Day, 0, 1, 0);
                        oCom.dtFechaFin = new DateTime(dtIniaux.Year, dtIniaux.Month, dtIniaux.Day, 23, 59, 0);
                        oCom.sduty_type = sDutyType;
                        oCom.sOrigen = sPODLeg;
                        oCom.sDestino = sPOA;


                        if (bEsInterFinal)
                        {
                            oCant.iCantDesayunosInt++;
                            oCant.iCantComidasInt++;
                            oCant.iCantCenasInt++;

                            oCom.iDesayunosInt++;
                            oCom.iComidaInt++;
                            oCom.iCenaInt++;
                        }
                        else
                        {
                            oCant.iCantDesayunos++;
                            oCant.iCantComidas++;
                            oCant.iCantCenas++;

                            oCom.iDesayunosNal++;
                            oCom.iComidaNal++;
                            oCom.iCenaNal++;
                        }

                        oLst.Add(oCom);
                    }

                    // ya anda fuera de base antes de la pierna inicial?
                    if (bTieneDiasAntes && bTieneDiasDespues)
                    {
                        ComidasPorDia oComDia1 = new ComidasPorDia();
                        oComDia1.sClavePiloto = sClavePiloto;
                        oComDia1.dtFechaDia = dtInicial;
                        oComDia1.dtFechaFin = new DateTime(dtInicial.Year, dtInicial.Month, dtInicial.Day, 23, 59, 0);
                        oComDia1.sduty_type = sDutyType;
                        oComDia1.sOrigen = sPODLeg;
                        oComDia1.sDestino = sPOA;

                        if (bEsInterFinal)
                        {
                            oCant.iCantDesayunosInt++;
                            oCant.iCantComidasInt++;
                            oCant.iCantCenasInt++;

                            oComDia1.iDesayunosInt++;
                            oComDia1.iComidaInt++;
                            oComDia1.iCenaInt++;
                        }
                        else
                        {
                            oCant.iCantDesayunos++;
                            oCant.iCantComidas++;
                            oCant.iCantCenas++;

                            oComDia1.iDesayunosNal++;
                            oComDia1.iComidaNal++;
                            oComDia1.iCenaNal++;
                        }

                        oLst.Add(oComDia1);
                    }
                    else
                    {
                        // DIA INICIAL
                        ComidasPorDia oComDia1 = new ComidasPorDia();
                        oComDia1.sClavePiloto = sClavePiloto;
                        oComDia1.sduty_type = sDutyType;
                        oComDia1.sOrigen = sPODLeg;
                        oComDia1.sDestino = sPOA;

                        #region COMENTADO CONDICIONES ANTERIORES
                        //if (fHoraInicio < fInicioDesayuno || (fHoraInicio > fInicioDesayuno && fHoraInicio < fFinDesayuno))
                        //{
                        //    if (bEsInterFinal)
                        //    {
                        //        oCant.iCantDesayunosInt++;
                        //        oCant.iCantComidasInt++;
                        //        oCant.iCantCenasInt++;

                        //        oComDia1.iDesayunosInt++;
                        //        oComDia1.iComidaInt++;
                        //        oComDia1.iCenaInt++;
                        //    }
                        //    else
                        //    {
                        //        oCant.iCantDesayunos++;
                        //        oCant.iCantComidas++;
                        //        oCant.iCantCenas++;

                        //        oComDia1.iDesayunosNal++;
                        //        oComDia1.iComidaNal++;
                        //        oComDia1.iCenaNal++;
                        //    }
                        //}
                        //else if (fHoraInicio < fInicioComida && fHoraInicio > fFinDesayuno && fHoraInicio < fFinComida)
                        //{
                        //    if (bEsInterFinal)
                        //    {
                        //        oCant.iCantComidasInt++;
                        //        oCant.iCantCenasInt++;

                        //        oComDia1.iComidaInt++;
                        //        oComDia1.iCenaInt++;
                        //    }
                        //    else
                        //    {
                        //        oCant.iCantComidas++;
                        //        oCant.iCantCenas++;

                        //        oComDia1.iComidaNal++;
                        //        oComDia1.iCenaNal++;
                        //    }
                        //}
                        //else if (fHoraInicio < fInicioCena && fHoraInicio > fFinComida && fHoraInicio < fFinCena)
                        //{
                        //    if (bEsInterFinal)
                        //    {
                        //        oCant.iCantCenasInt++;

                        //        oComDia1.iCenaInt++;
                        //    }
                        //    else
                        //    {
                        //        oCant.iCantCenas++;

                        //        oComDia1.iCenaNal++;
                        //    }
                        //}
                        #endregion

                        #region DESAYUNO
                        //Primera condicion
                        if (((fHoraInicio < fInicioDesayuno && fHoraFinal > fInicioDesayuno) && fHoraFinal < fFinDesayuno) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                        {
                            if (bEsInterFinal)
                            {
                                oCant.iCantDesayunosInt++;
                                //oCant.iCantComidasInt++;
                                //oCant.iCantCenasInt++;

                                oComDia1.iDesayunosInt++;
                                //oComDia1.iComidaInt++;
                                //oComDia1.iCenaInt++;
                            }
                            else
                            {
                                oCant.iCantDesayunos++;
                                //oCant.iCantComidas++;
                                //oCant.iCantCenas++;

                                oComDia1.iDesayunosNal++;
                                //oComDia1.iComidaNal++;
                                //oComDia1.iCenaNal++;
                            }
                        }
                        //Segunda condición
                        else if ((fHoraInicio > fInicioDesayuno && fHoraInicio < fFinDesayuno) && (fHoraFinal > fFinDesayuno && fHoraFinal > fInicioDesayuno) && (oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioComida)) //Evaluar antes de comida
                        {
                            if (bEsInterFinal)
                            {
                                oCant.iCantDesayunosInt++;
                                //oCant.iCantComidasInt++;
                                //oCant.iCantCenasInt++;

                                oComDia1.iDesayunosInt++;
                                //oComDia1.iComidaInt++;
                                //oComDia1.iCenaInt++;
                            }
                            else
                            {
                                oCant.iCantDesayunos++;
                                //oCant.iCantComidas++;
                                //oCant.iCantCenas++;

                                oComDia1.iDesayunosNal++;
                                //oComDia1.iComidaNal++;
                                //oComDia1.iCenaNal++;
                            }
                        }
                        //Tercera condición
                        else if ((fHoraInicio >= fInicioDesayuno && fHoraFinal > fFinDesayuno) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioComida))
                        {
                            if (bEsInterFinal)
                            {
                                oCant.iCantDesayunosInt++;
                                //oCant.iCantComidasInt++;
                                //oCant.iCantCenasInt++;

                                oComDia1.iDesayunosInt++;
                                //oComDia1.iComidaInt++;
                                //oComDia1.iCenaInt++;
                            }
                            else
                            {
                                oCant.iCantDesayunos++;
                                //oCant.iCantComidas++;
                                //oCant.iCantCenas++;

                                oComDia1.iDesayunosNal++;
                                //oComDia1.iComidaNal++;
                                //oComDia1.iCenaNal++;
                            }
                        }
                        //Cuarta condición
                        else if ((fHoraInicio < fInicioDesayuno && fHoraFinal <= fInicioDesayuno) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                        {
                            if (bEsInterFinal)
                            {
                                oCant.iCantDesayunosInt++;
                                //oCant.iCantComidasInt++;
                                //oCant.iCantCenasInt++;

                                oComDia1.iDesayunosInt++;
                                //oComDia1.iComidaInt++;
                                //oComDia1.iCenaInt++;
                            }
                            else
                            {
                                oCant.iCantDesayunos++;
                                //oCant.iCantComidas++;
                                //oCant.iCantCenas++;

                                oComDia1.iDesayunosNal++;
                                //oComDia1.iComidaNal++;
                                //oComDia1.iCenaNal++;
                            }
                        }
                        //Quinta condición
                        else if ((fHoraInicio < fInicioDesayuno && fHoraFinal < fInicioDesayuno) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                        {
                            if (bEsInterFinal)
                            {
                                oCant.iCantDesayunosInt++;
                                //oCant.iCantComidasInt++;
                                //oCant.iCantCenasInt++;

                                oComDia1.iDesayunosInt++;
                                //oComDia1.iComidaInt++;
                                //oComDia1.iCenaInt++;
                            }
                            else
                            {
                                oCant.iCantDesayunos++;
                                //oCant.iCantComidas++;
                                //oCant.iCantCenas++;

                                oComDia1.iDesayunosNal++;
                                //oComDia1.iComidaNal++;
                                //oComDia1.iCenaNal++;
                            }
                        }
                        //Sexta condición
                        else if ((fHoraInicio < fInicioDesayuno && (fHoraFinal > fInicioDesayuno && fHoraFinal <= fFinDesayuno)) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                        {
                            if (bEsInterFinal)
                            {
                                oCant.iCantDesayunosInt++;
                                //oCant.iCantComidasInt++;
                                //oCant.iCantCenasInt++;

                                oComDia1.iDesayunosInt++;
                                //oComDia1.iComidaInt++;
                                //oComDia1.iCenaInt++;
                            }
                            else
                            {
                                oCant.iCantDesayunos++;
                                //oCant.iCantComidas++;
                                //oCant.iCantCenas++;

                                oComDia1.iDesayunosNal++;
                                //oComDia1.iComidaNal++;
                                //oComDia1.iCenaNal++;
                            }
                        }
                        //Septima condición
                        else if ((fHoraInicio >= fInicioDesayuno && fHoraInicio <= fFinDesayuno) && (fHoraFinal > fInicioDesayuno && fHoraFinal <= fFinDesayuno) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                        {
                            if (bEsInterFinal)
                            {
                                oCant.iCantDesayunosInt++;
                                //oCant.iCantComidasInt++;
                                //oCant.iCantCenasInt++;

                                oComDia1.iDesayunosInt++;
                                //oComDia1.iComidaInt++;
                                //oComDia1.iCenaInt++;
                            }
                            else
                            {
                                oCant.iCantDesayunos++;
                                //oCant.iCantComidas++;
                                //oCant.iCantCenas++;

                                oComDia1.iDesayunosNal++;
                                //oComDia1.iComidaNal++;
                                //oComDia1.iCenaNal++;
                            }
                        }
                        //Octava condición
                        else if ((fHoraInicio < fInicioDesayuno && (fHoraFinal > fInicioDesayuno && fHoraFinal >= fFinDesayuno)) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioComida))
                        {
                            if (bEsInterFinal)
                            {
                                oCant.iCantDesayunosInt++;
                                //oCant.iCantComidasInt++;
                                //oCant.iCantCenasInt++;

                                oComDia1.iDesayunosInt++;
                                //oComDia1.iComidaInt++;
                                //oComDia1.iCenaInt++;
                            }
                            else
                            {
                                oCant.iCantDesayunos++;
                                //oCant.iCantComidas++;
                                //oCant.iCantCenas++;

                                oComDia1.iDesayunosNal++;
                                //oComDia1.iComidaNal++;
                                //oComDia1.iCenaNal++;
                            }
                        }
                        #endregion

                        #region COMIDA
                        //Primera condicion Comidas
                        if (((fHoraInicio < fInicioComida && fHoraFinal > fInicioComida) && fHoraFinal < fFinComida) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioCena))
                        {
                            if (bEsInterFinal)
                            {
                                oCant.iCantComidasInt++;
                                //oCant.iCantCenasInt++;

                                oComDia1.iComidaInt++;
                                //oComDia1.iCenaInt++;
                            }
                            else
                            {
                                oCant.iCantComidas++;
                                //oCant.iCantCenas++;

                                oComDia1.iComidaNal++;
                                //oComDia1.iCenaNal++;
                            }
                        }
                        //Segunda condición Comidas
                        else if ((fHoraInicio > fInicioComida && fHoraInicio < fFinComida) && (fHoraFinal > fFinComida && fHoraFinal > fInicioComida) && (oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioCena)) //Evaluar antes de cena
                        {
                            if (bEsInterFinal)
                            {
                                oCant.iCantComidasInt++;
                                //oCant.iCantCenasInt++;

                                oComDia1.iComidaInt++;
                                //oComDia1.iCenaInt++;
                            }
                            else
                            {
                                oCant.iCantComidas++;
                                //oCant.iCantCenas++;

                                oComDia1.iComidaNal++;
                                //oComDia1.iCenaNal++;
                            }
                        }
                        //Tercera condición Comidas
                        else if ((fHoraInicio >= fInicioComida && fHoraFinal > fFinComida) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioCena))
                        {
                            if (bEsInterFinal)
                            {
                                oCant.iCantComidasInt++;
                                //oCant.iCantCenasInt++;

                                oComDia1.iComidaInt++;
                                //oComDia1.iCenaInt++;
                            }
                            else
                            {
                                oCant.iCantComidas++;
                                //oCant.iCantCenas++;

                                oComDia1.iComidaNal++;
                                //oComDia1.iCenaNal++;
                            }
                        }
                        //Cuarta condición Comidas
                        else if ((fHoraInicio < fInicioComida && fHoraFinal <= fInicioComida) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioCena))
                        {
                            if (bEsInterFinal)
                            {
                                oCant.iCantComidasInt++;
                                //oCant.iCantCenasInt++;

                                oComDia1.iComidaInt++;
                                //oComDia1.iCenaInt++;
                            }
                            else
                            {
                                oCant.iCantComidas++;
                                //oCant.iCantCenas++;

                                oComDia1.iComidaNal++;
                                //oComDia1.iCenaNal++;
                            }
                        }
                        //Quinta condición Comidas
                        else if ((fHoraInicio < fInicioComida && fHoraFinal < fInicioComida) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioCena))
                        {
                            if (bEsInterFinal)
                            {
                                oCant.iCantComidasInt++;
                                //oCant.iCantCenasInt++;

                                oComDia1.iComidaInt++;
                                //oComDia1.iCenaInt++;
                            }
                            else
                            {
                                oCant.iCantComidas++;
                                //oCant.iCantCenas++;

                                oComDia1.iComidaNal++;
                                //oComDia1.iCenaNal++;
                            }
                        }
                        //Sexta condición Comidas
                        else if ((fHoraInicio < fInicioComida && (fHoraFinal > fInicioComida && fHoraFinal <= fFinComida)) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioCena))
                        {
                            if (bEsInterFinal)
                            {
                                oCant.iCantComidasInt++;
                                //oCant.iCantCenasInt++;

                                oComDia1.iComidaInt++;
                                //oComDia1.iCenaInt++;
                            }
                            else
                            {
                                oCant.iCantComidas++;
                                //oCant.iCantCenas++;

                                oComDia1.iComidaNal++;
                                //oComDia1.iCenaNal++;
                            }
                        }
                        //Septima condición Comidas
                        else if ((fHoraInicio >= fInicioComida && fHoraInicio <= fFinComida) && (fHoraFinal > fInicioComida && fHoraFinal <= fFinComida) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioCena))
                        {
                            if (bEsInterFinal)
                            {
                                oCant.iCantComidasInt++;
                                //oCant.iCantCenasInt++;

                                oComDia1.iComidaInt++;
                                //oComDia1.iCenaInt++;
                            }
                            else
                            {
                                oCant.iCantComidas++;
                                //oCant.iCantCenas++;

                                oComDia1.iComidaNal++;
                                //oComDia1.iCenaNal++;
                            }
                        }
                        //Octava condición
                        else if ((fHoraInicio < fInicioComida && (fHoraFinal > fInicioComida && fHoraFinal >= fFinComida)) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioCena))
                        {
                            if (bEsInterFinal)
                            {
                                oCant.iCantComidasInt++;
                                //oCant.iCantCenasInt++;

                                oComDia1.iComidaInt++;
                                //oComDia1.iCenaInt++;
                            }
                            else
                            {
                                oCant.iCantComidas++;
                                //oCant.iCantCenas++;

                                oComDia1.iComidaNal++;
                                //oComDia1.iCenaNal++;
                            }
                        }
                        #endregion

                        #region CENA
                        //Primera condicion Cenas
                        if (((fHoraInicio < fInicioCena && fHoraFinal > fInicioCena) && fHoraFinal < fFinCena) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                        {
                            if (bEsInterFinal)
                            {
                                oCant.iCantCenasInt++;
                                oComDia1.iCenaInt++;
                            }
                            else
                            {
                                oCant.iCantCenas++;
                                oComDia1.iCenaNal++;
                            }
                        }
                        //Segunda condición Cenas
                        else if ((fHoraInicio > fInicioCena && fHoraInicio < fFinCena) && (fHoraFinal > fFinCena && fHoraFinal > fInicioCena) && (oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                        {
                            if (bEsInterFinal)
                            {
                                oCant.iCantCenasInt++;
                                oComDia1.iCenaInt++;
                            }
                            else
                            {
                                oCant.iCantCenas++;
                                oComDia1.iCenaNal++;
                            }
                        }
                        //Tercera condición Cenas
                        else if ((fHoraInicio >= fInicioCena && fHoraFinal > fFinCena) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                        {
                            if (bEsInterFinal)
                            {
                                oCant.iCantCenasInt++;
                                oComDia1.iCenaInt++;
                            }
                            else
                            {
                                oCant.iCantCenas++;
                                oComDia1.iCenaNal++;
                            }
                        }
                        //Cuarta condición Cenas
                        else if ((fHoraInicio < fInicioCena && fHoraFinal <= fInicioCena) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                        {
                            if (bEsInterFinal)
                            {
                                oCant.iCantCenasInt++;
                                oComDia1.iCenaInt++;
                            }
                            else
                            {
                                oCant.iCantCenas++;
                                oComDia1.iCenaNal++;
                            }
                        }
                        //Quinta condición Cenas
                        else if ((fHoraInicio < fInicioCena && fHoraFinal < fInicioCena) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                        {
                            if (bEsInterFinal)
                            {
                                oCant.iCantCenasInt++;
                                oComDia1.iCenaInt++;
                            }
                            else
                            {
                                oCant.iCantCenas++;
                                oComDia1.iCenaNal++;
                            }
                        }
                        //Sexta condición Cenas
                        else if ((fHoraInicio < fInicioCena && (fHoraFinal > fInicioCena && fHoraFinal <= fFinCena)) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                        {
                            if (bEsInterFinal)
                            {
                                oCant.iCantCenasInt++;
                                oComDia1.iCenaInt++;
                            }
                            else
                            {
                                oCant.iCantCenas++;
                                oComDia1.iCenaNal++;
                            }
                        }
                        //Septima condición Cenas
                        else if ((fHoraInicio >= fInicioCena && fHoraInicio <= fFinCena) && (fHoraFinal > fInicioCena && fHoraFinal <= fFinCena) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                        {
                            if (bEsInterFinal)
                            {
                                oCant.iCantCenasInt++;
                                oComDia1.iCenaInt++;
                            }
                            else
                            {
                                oCant.iCantCenas++;
                                oComDia1.iCenaNal++;
                            }
                        }
                        //Octava condición Cenas
                        else if ((fHoraInicio < fInicioCena && (fHoraFinal > fInicioCena && fHoraFinal >= fFinCena)) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                        {
                            if (bEsInterFinal)
                            {
                                oCant.iCantCenasInt++;
                                oComDia1.iCenaInt++;
                            }
                            else
                            {
                                oCant.iCantCenas++;
                                oComDia1.iCenaNal++;
                            }
                        }
                        #endregion

                        oLst.Add(oComDia1);
                    }

                    // estará fuera de base al terminar esta pierna?
                    if (bTieneDiasAntes && bTieneDiasDespues)
                    {
                        ComidasPorDia oComDia2 = new ComidasPorDia();
                        oComDia2.sClavePiloto = sClavePiloto;
                        oComDia2.dtFechaDia = dtFinal;
                        oComDia2.dtFechaFin = new DateTime(dtFinal.Year, dtFinal.Month, dtFinal.Day, 23, 59, 0);
                        oComDia2.sduty_type = sDutyType;
                        oComDia2.sOrigen = sPODLeg;
                        oComDia2.sDestino = sPOA;

                        if (bEsInterFinal)
                        {
                            oCant.iCantDesayunosInt++;
                            oCant.iCantComidasInt++;
                            oCant.iCantCenasInt++;

                            oComDia2.iDesayunosInt++;
                            oComDia2.iComidaInt++;
                            oComDia2.iCenaInt++;
                        }
                        else
                        {
                            oCant.iCantDesayunos++;
                            oCant.iCantComidas++;
                            oCant.iCantCenas++;

                            oComDia2.iDesayunosNal++;
                            oComDia2.iComidaNal++;
                            oComDia2.iCenaNal++;
                        }

                        oLst.Add(oComDia2);
                    }
                    else
                    {
                        // DIA FINAL
                        ComidasPorDia oComDia2 = new ComidasPorDia();
                        oComDia2.sClavePiloto = sClavePiloto;
                        oComDia2.dtFechaDia = dtFinal;
                        oComDia2.sduty_type = sDutyType;
                        oComDia2.sOrigen = sPODLeg;
                        oComDia2.sDestino = sPOA;

                        #region COMENTADO CONDICIONES ANTERIORES
                        //if (fHoraFinal > fFinCena || (fHoraFinal > fInicioCena && fHoraFinal < fFinCena))
                        //{
                        //    if (bEsInterFinal)
                        //    {
                        //        oCant.iCantDesayunosInt++;
                        //        oCant.iCantComidasInt++;
                        //        oCant.iCantCenasInt++;

                        //        oComDia2.iDesayunosInt++;
                        //        oComDia2.iComidaInt++;
                        //        oComDia2.iCenaInt++;
                        //    }
                        //    else
                        //    {
                        //        oCant.iCantDesayunos++;
                        //        oCant.iCantComidas++;
                        //        oCant.iCantCenas++;

                        //        oComDia2.iDesayunosNal++;
                        //        oComDia2.iComidaNal++;
                        //        oComDia2.iCenaNal++;
                        //    }
                        //}
                        //else if (fHoraFinal < fInicioCena && fHoraFinal > fInicioComida)
                        //{
                        //    if (bEsInterFinal)
                        //    {
                        //        oCant.iCantComidasInt++;
                        //        oCant.iCantCenasInt++;

                        //        oComDia2.iComidaInt++;
                        //        oComDia2.iCenaInt++;
                        //    }
                        //    else
                        //    {
                        //        oCant.iCantComidas++;
                        //        oCant.iCantCenas++;

                        //        oComDia2.iComidaNal++;
                        //        oComDia2.iCenaNal++;
                        //    }
                        //}
                        //else if (fHoraFinal < fInicioComida && fHoraFinal > fInicioDesayuno)
                        //{
                        //    if (bEsInterFinal)
                        //    {
                        //        oCant.iCantDesayunosInt++;

                        //        oComDia2.iDesayunosInt++;
                        //    }
                        //    else
                        //    {
                        //        oCant.iCantDesayunos++;

                        //        oComDia2.iDesayunosNal++;
                        //    }
                        //}
                        #endregion

                        #region DESAYUNO
                        //Primera condicion
                        if (((fHoraInicio < fInicioDesayuno && fHoraFinal > fInicioDesayuno) && fHoraFinal < fFinDesayuno) && (oComDia2.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                        {
                            if (bEsInterFinal)
                            {
                                oCant.iCantDesayunosInt++;
                                oComDia2.iDesayunosInt++;
                            }
                            else
                            {
                                oCant.iCantDesayunos++;
                                oComDia2.iDesayunosNal++;
                            }
                        }
                        //Segunda condición
                        else if ((fHoraInicio > fInicioDesayuno && fHoraInicio < fFinDesayuno) && (fHoraFinal > fFinDesayuno && fHoraFinal > fInicioDesayuno) && (oComDia2.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioComida)) //Evaluar antes de comida
                        {
                            if (bEsInterFinal)
                            {
                                oCant.iCantDesayunosInt++;
                                oComDia2.iDesayunosInt++;
                            }
                            else
                            {
                                oCant.iCantDesayunos++;
                                oComDia2.iDesayunosNal++;
                            }
                        }
                        //Tercera condición
                        else if ((fHoraInicio >= fInicioDesayuno && fHoraFinal > fFinDesayuno) && (oComDia2.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia2.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioComida))
                        {
                            if (bEsInterFinal)
                            {
                                oCant.iCantDesayunosInt++;
                                oComDia2.iDesayunosInt++;
                            }
                            else
                            {
                                oCant.iCantDesayunos++;
                                oComDia2.iDesayunosNal++;
                            }
                        }
                        //Cuarta condición
                        else if ((fHoraInicio < fInicioDesayuno && fHoraFinal <= fInicioDesayuno) && (oComDia2.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                        {
                            if (bEsInterFinal)
                            {
                                oCant.iCantDesayunosInt++;
                                oComDia2.iDesayunosInt++;
                            }
                            else
                            {
                                oCant.iCantDesayunos++;
                                oComDia2.iDesayunosNal++;
                            }
                        }
                        //Quinta condición
                        else if ((fHoraInicio < fInicioDesayuno && fHoraFinal < fInicioDesayuno) && (oComDia2.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                        {
                            if (bEsInterFinal)
                            {
                                oCant.iCantDesayunosInt++;
                                oComDia2.iDesayunosInt++;
                            }
                            else
                            {
                                oCant.iCantDesayunos++;
                                oComDia2.iDesayunosNal++;
                            }
                        }
                        //Sexta condición
                        else if ((fHoraInicio < fInicioDesayuno && (fHoraFinal > fInicioDesayuno && fHoraFinal <= fFinDesayuno)) && (oComDia2.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia2.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                        {
                            if (bEsInterFinal)
                            {
                                oCant.iCantDesayunosInt++;
                                oComDia2.iDesayunosInt++;
                            }
                            else
                            {
                                oCant.iCantDesayunos++;
                                oComDia2.iDesayunosNal++;
                            }
                        }
                        //Septima condición
                        else if ((fHoraInicio >= fInicioDesayuno && fHoraInicio <= fFinDesayuno) && (fHoraFinal > fInicioDesayuno && fHoraFinal <= fFinDesayuno) && (oComDia2.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia2.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                        {
                            if (bEsInterFinal)
                            {
                                oCant.iCantDesayunosInt++;
                                oComDia2.iDesayunosInt++;
                            }
                            else
                            {
                                oCant.iCantDesayunos++;
                                oComDia2.iDesayunosNal++;
                            }
                        }
                        //Octava condición
                        else if ((fHoraInicio < fInicioDesayuno && (fHoraFinal > fInicioDesayuno && fHoraFinal >= fFinDesayuno)) && (oComDia2.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia2.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioComida))
                        {
                            if (bEsInterFinal)
                            {
                                oCant.iCantDesayunosInt++;
                                oComDia2.iDesayunosInt++;
                            }
                            else
                            {
                                oCant.iCantDesayunos++;
                                oComDia2.iDesayunosNal++;
                            }
                        }
                        #endregion

                        #region COMIDA
                        //Primera condicion Comidas
                        if (((fHoraInicio < fInicioComida && fHoraFinal > fInicioComida) && fHoraFinal < fFinComida) && (oComDia2.sDestino != dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioCena))
                        {
                            if (bEsInterFinal)
                            {
                                oCant.iCantComidasInt++;
                                oComDia2.iComidaInt++;
                            }
                            else
                            {
                                oCant.iCantComidas++;
                                oComDia2.iComidaNal++;
                            }
                        }
                        //Segunda condición Comidas
                        else if ((fHoraInicio > fInicioComida && fHoraInicio < fFinComida) && (fHoraFinal > fFinComida && fHoraFinal > fInicioComida) && (oComDia2.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioCena)) //Evaluar antes de cena
                        {
                            if (bEsInterFinal)
                            {
                                oCant.iCantComidasInt++;
                                oComDia2.iComidaInt++;
                            }
                            else
                            {
                                oCant.iCantComidas++;
                                oComDia2.iComidaNal++;
                            }
                        }
                        //Tercera condición Comidas
                        else if ((fHoraInicio >= fInicioComida && fHoraFinal > fFinComida) && (oComDia2.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia2.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioCena))
                        {
                            if (bEsInterFinal)
                            {
                                oCant.iCantComidasInt++;
                                oComDia2.iComidaInt++;
                            }
                            else
                            {
                                oCant.iCantComidas++;
                                oComDia2.iComidaNal++;
                            }
                        }
                        //Cuarta condición Comidas
                        else if ((fHoraInicio < fInicioComida && fHoraFinal <= fInicioComida) && (oComDia2.sDestino != dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioCena))
                        {
                            if (bEsInterFinal)
                            {
                                oCant.iCantComidasInt++;
                                oComDia2.iComidaInt++;
                            }
                            else
                            {
                                oCant.iCantComidas++;
                                oComDia2.iComidaNal++;
                            }
                        }
                        //Quinta condición Comidas
                        else if ((fHoraInicio < fInicioComida && fHoraFinal < fInicioComida) && (oComDia2.sDestino != dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioCena))
                        {
                            if (bEsInterFinal)
                            {
                                oCant.iCantComidasInt++;
                                oComDia2.iComidaInt++;
                            }
                            else
                            {
                                oCant.iCantComidas++;
                                oComDia2.iComidaNal++;
                            }
                        }
                        //Sexta condición Comidas
                        else if ((fHoraInicio < fInicioComida && (fHoraFinal > fInicioComida && fHoraFinal <= fFinComida)) && (oComDia2.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia2.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioCena))
                        {
                            if (bEsInterFinal)
                            {
                                oCant.iCantComidasInt++;
                                oComDia2.iComidaInt++;
                            }
                            else
                            {
                                oCant.iCantComidas++;
                                oComDia2.iComidaNal++;
                            }
                        }
                        //Septima condición Comidas
                        else if ((fHoraInicio >= fInicioComida && fHoraInicio <= fFinComida) && (fHoraFinal > fInicioComida && fHoraFinal <= fFinComida) && (oComDia2.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia2.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioCena))
                        {
                            if (bEsInterFinal)
                            {
                                oCant.iCantComidasInt++;
                                oComDia2.iComidaInt++;
                            }
                            else
                            {
                                oCant.iCantComidas++;
                                oComDia2.iComidaNal++;
                            }
                        }
                        //Octava condición
                        else if ((fHoraInicio < fInicioComida && (fHoraFinal > fInicioComida && fHoraFinal >= fFinComida)) && (oComDia2.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia2.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioCena))
                        {
                            if (bEsInterFinal)
                            {
                                oCant.iCantComidasInt++;
                                oComDia2.iComidaInt++;
                            }
                            else
                            {
                                oCant.iCantComidas++;
                                oComDia2.iComidaNal++;
                            }
                        }
                        #endregion

                        #region CENA
                        //Primera condicion Cenas
                        if (((fHoraInicio < fInicioCena && fHoraFinal > fInicioCena) && fHoraFinal < fFinCena) && (oComDia2.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                        {
                            if (bEsInterFinal)
                            {
                                //oCant.iCantDesayunosInt++;
                                //oCant.iCantComidasInt++;
                                oCant.iCantCenasInt++;

                                //oComDia2.iDesayunosInt++;
                                //oComDia2.iComidaInt++;
                                oComDia2.iCenaInt++;
                            }
                            else
                            {
                                //oCant.iCantDesayunos++;
                                //oCant.iCantComidas++;
                                oCant.iCantCenas++;

                                //oComDia2.iDesayunosNal++;
                                //oComDia2.iComidaNal++;
                                oComDia2.iCenaNal++;
                            }
                        }
                        //Segunda condición Cenas
                        else if ((fHoraInicio > fInicioCena && fHoraInicio < fFinCena) && (fHoraFinal > fFinCena && fHoraFinal > fInicioCena) && (oComDia2.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                        {
                            if (bEsInterFinal)
                            {
                                //oCant.iCantDesayunosInt++;
                                //oCant.iCantComidasInt++;
                                oCant.iCantCenasInt++;

                                //oComDia2.iDesayunosInt++;
                                //oComDia2.iComidaInt++;
                                oComDia2.iCenaInt++;
                            }
                            else
                            {
                                //oCant.iCantDesayunos++;
                                //oCant.iCantComidas++;
                                oCant.iCantCenas++;

                                //oComDia2.iDesayunosNal++;
                                //oComDia2.iComidaNal++;
                                oComDia2.iCenaNal++;
                            }
                        }
                        //Tercera condición Cenas
                        else if ((fHoraInicio >= fInicioCena && fHoraFinal > fFinCena) && (oComDia2.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia2.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                        {
                            if (bEsInterFinal)
                            {
                                //oCant.iCantDesayunosInt++;
                                //oCant.iCantComidasInt++;
                                oCant.iCantCenasInt++;

                                //oComDia2.iDesayunosInt++;
                                //oComDia2.iComidaInt++;
                                oComDia2.iCenaInt++;
                            }
                            else
                            {
                                //oCant.iCantDesayunos++;
                                //oCant.iCantComidas++;
                                oCant.iCantCenas++;

                                //oComDia2.iDesayunosNal++;
                                //oComDia2.iComidaNal++;
                                oComDia2.iCenaNal++;
                            }
                        }
                        //Cuarta condición Cenas
                        else if ((fHoraInicio < fInicioCena && fHoraFinal <= fInicioCena) && (oComDia2.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                        {
                            if (bEsInterFinal)
                            {
                                //oCant.iCantDesayunosInt++;
                                //oCant.iCantComidasInt++;
                                oCant.iCantCenasInt++;

                                //oComDia2.iDesayunosInt++;
                                //oComDia2.iComidaInt++;
                                oComDia2.iCenaInt++;
                            }
                            else
                            {
                                //oCant.iCantDesayunos++;
                                //oCant.iCantComidas++;
                                oCant.iCantCenas++;

                                //oComDia2.iDesayunosNal++;
                                //oComDia2.iComidaNal++;
                                oComDia2.iCenaNal++;
                            }
                        }
                        //Quinta condición Cenas
                        else if ((fHoraInicio < fInicioCena && fHoraFinal < fInicioCena) && (oComDia2.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                        {
                            if (bEsInterFinal)
                            {
                                //oCant.iCantDesayunosInt++;
                                //oCant.iCantComidasInt++;
                                oCant.iCantCenasInt++;

                                //oComDia2.iDesayunosInt++;
                                //oComDia2.iComidaInt++;
                                oComDia2.iCenaInt++;
                            }
                            else
                            {
                                //oCant.iCantDesayunos++;
                                //oCant.iCantComidas++;
                                oCant.iCantCenas++;

                                //oComDia2.iDesayunosNal++;
                                //oComDia2.iComidaNal++;
                                oComDia2.iCenaNal++;
                            }
                        }
                        //Sexta condición Cenas
                        else if ((fHoraInicio < fInicioCena && (fHoraFinal > fInicioCena && fHoraFinal <= fFinCena)) && (oComDia2.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia2.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                        {
                            if (bEsInterFinal)
                            {
                                //oCant.iCantDesayunosInt++;
                                //oCant.iCantComidasInt++;
                                oCant.iCantCenasInt++;

                                //oComDia2.iDesayunosInt++;
                                //oComDia2.iComidaInt++;
                                oComDia2.iCenaInt++;
                            }
                            else
                            {
                                //oCant.iCantDesayunos++;
                                //oCant.iCantComidas++;
                                oCant.iCantCenas++;

                                //oComDia2.iDesayunosNal++;
                                //oComDia2.iComidaNal++;
                                oComDia2.iCenaNal++;
                            }
                        }
                        //Septima condición Cenas
                        else if ((fHoraInicio >= fInicioCena && fHoraInicio <= fFinCena) && (fHoraFinal > fInicioCena && fHoraFinal <= fFinCena) && (oComDia2.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia2.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                        {
                            if (bEsInterFinal)
                            {
                                //oCant.iCantDesayunosInt++;
                                //oCant.iCantComidasInt++;
                                oCant.iCantCenasInt++;

                                //oComDia2.iDesayunosInt++;
                                //oComDia2.iComidaInt++;
                                oComDia2.iCenaInt++;
                            }
                            else
                            {
                                //oCant.iCantDesayunos++;
                                //oCant.iCantComidas++;
                                oCant.iCantCenas++;

                                //oComDia2.iDesayunosNal++;
                                //oComDia2.iComidaNal++;
                                oComDia2.iCenaNal++;
                            }
                        }
                        //Octava condición Cenas
                        else if ((fHoraInicio < fInicioCena && (fHoraFinal > fInicioCena && fHoraFinal >= fFinCena)) && (oComDia2.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia2.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                        {
                            if (bEsInterFinal)
                            {
                                //oCant.iCantDesayunosInt++;
                                //oCant.iCantComidasInt++;
                                oCant.iCantCenasInt++;

                                //oComDia2.iDesayunosInt++;
                                //oComDia2.iComidaInt++;
                                oComDia2.iCenaInt++;
                            }
                            else
                            {
                                //oCant.iCantDesayunos++;
                                //oCant.iCantComidas++;
                                oCant.iCantCenas++;

                                //oComDia2.iDesayunosNal++;
                                //oComDia2.iComidaNal++;
                                oComDia2.iCenaNal++;
                            }
                        }
                        #endregion

                        oLst.Add(oComDia2);
                    }
                }


                #region CODIGO CUANDO ES UN DIA AGREGADO TEMPORALMENTE PARA REVISAR FUNCIONAMIENTO PARA MAS DE DOS DÍAS
                //for (int k = 0; k < rowsD.Length; k++)
                //{
                //    ComidasPorDia oComDia1 = new ComidasPorDia();
                //    oComDia1.sClavePiloto = drP["ClavePiloto"].S();
                //    oComDia1.dtFechaDia = rowsD[k][FechaInicio].S().Dt();
                //    oComDia1.dtFechaFin = rowsD[k][FechaFin].S().Dt();
                //    oComDia1.sduty_type = rowsD[k]["DutyType"].S();
                //    oComDia1.sOrigen = rowsD[k]["POD"].S();
                //    oComDia1.sDestino = rowsD[k]["POA"].S();

                //    fHoraInicio = 0;
                //    fHoraFinal = 0;

                //    bEsInterInicio = false;
                //    bEsInterFinal = false;

                //    bEsInterInicio = rowsD[k]["PaisPOD"].S() == "MX" ? false : true;
                //    bEsInterFinal = rowsD[k]["PaisPOA"].S() == "MX" ? false : true;

                //    DateTime dtIniDia = rowsD[0][FechaInicio].S().Dt();
                //    DateTime dtFinDia = rowsD[k][FechaFin].S().Dt();

                //    fHoraInicio = dtIniDia.Hour + (dtIniDia.Minute / float.Parse("60"));
                //    fHoraFinal = dtFinDia.Hour + (dtFinDia.Minute / float.Parse("60"));

                //    if (l > 0 && k == 0)
                //        fHoraInicio = 0;

                //    if (k + 1 == rowsD.Length)
                //    {

                //        #region DESAYUNO
                //        //if (fHoraInicio < fInicioDesayuno || (fHoraInicio > fInicioDesayuno && fHoraInicio < fFinDesayuno)) //Linea origen

                //        //Primera condicion
                //        if (((fHoraInicio < fInicioDesayuno && fHoraFinal > fInicioDesayuno) && fHoraFinal < fFinDesayuno) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                //        {
                //            if (bEsInterFinal) { oCant.iCantDesayunosInt++; oComDia1.iDesayunosInt++; } else { oCant.iCantDesayunos++; oComDia1.iDesayunosNal++; }
                //        }
                //        //Segunda condición
                //        else if ((fHoraInicio > fInicioDesayuno && fHoraInicio < fFinDesayuno) && (fHoraFinal > fFinDesayuno && fHoraFinal > fInicioDesayuno) && (oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioComida)) //Evaluar antes de comida
                //        {
                //            if (bEsInterFinal) { oCant.iCantDesayunosInt++; oComDia1.iDesayunosInt++; } else { oCant.iCantDesayunos++; oComDia1.iDesayunosNal++; }
                //        }
                //        //Tercera condición
                //        else if ((fHoraInicio >= fInicioDesayuno && fHoraFinal > fFinDesayuno) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                //        {
                //            if (bEsInterFinal) { oCant.iCantDesayunosInt++; oComDia1.iDesayunosInt++; } else { oCant.iCantDesayunos++; oComDia1.iDesayunosNal++; }
                //        }
                //        //Cuarta condición
                //        else if ((fHoraInicio < fInicioDesayuno && fHoraFinal <= fInicioDesayuno) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                //        {
                //            if (bEsInterFinal) { oCant.iCantDesayunosInt++; oComDia1.iDesayunosInt++; } else { oCant.iCantDesayunos++; oComDia1.iDesayunosNal++; }
                //        }
                //        //Quinta condición
                //        else if ((fHoraInicio < fInicioDesayuno && fHoraFinal < fInicioDesayuno) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                //        {
                //            if (bEsInterFinal) { oCant.iCantDesayunosInt++; oComDia1.iDesayunosInt++; } else { oCant.iCantDesayunos++; oComDia1.iDesayunosNal++; }
                //        }
                //        //Sexta condición
                //        else if ((fHoraInicio < fInicioDesayuno && (fHoraFinal > fInicioDesayuno && fHoraFinal <= fFinDesayuno)) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                //        {
                //            if (bEsInterFinal) { oCant.iCantDesayunosInt++; oComDia1.iDesayunosInt++; } else { oCant.iCantDesayunos++; oComDia1.iDesayunosNal++; }
                //        }
                //        //Septima condición
                //        else if ((fHoraInicio >= fInicioDesayuno && fHoraInicio <= fFinDesayuno) && (fHoraFinal > fInicioDesayuno && fHoraFinal <= fFinDesayuno) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                //        {
                //            if (bEsInterFinal) { oCant.iCantDesayunosInt++; oComDia1.iDesayunosInt++; } else { oCant.iCantDesayunos++; oComDia1.iDesayunosNal++; }
                //        }

                //        //Octava condición
                //        else if ((fHoraInicio < fInicioDesayuno && (fHoraFinal > fInicioDesayuno && fHoraFinal >= fFinDesayuno)) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                //        {
                //            if (bEsInterFinal) { oCant.iCantDesayunosInt++; oComDia1.iDesayunosInt++; } else { oCant.iCantDesayunos++; oComDia1.iDesayunosNal++; }
                //        }
                //        #endregion

                //        #region COMIDA
                //        ////Primera condicion Comidas
                //        //if (((fHoraInicio < fInicioComida && fHoraFinal > fInicioComida) && fHoraFinal < fFinComida) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                //        //{
                //        //    if (bEsInterFinal) { oCant.iCantComidasInt++; oComDia1.iComidaInt++; } else { oCant.iCantComidas++; oComDia1.iComidaNal++; }
                //        //}
                //        ////Segunda condición Comidas
                //        //else if ((fHoraInicio > fInicioComida && fHoraInicio < fFinComida) && (fHoraFinal > fFinComida && fHoraFinal > fInicioComida) && (oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()) && (fHoraFinal < fInicioCena)) //Evaluar antes de cena
                //        //{
                //        //    if (bEsInterFinal) { oCant.iCantComidasInt++; oComDia1.iComidaInt++; } else { oCant.iCantComidas++; oComDia1.iComidaNal++; }
                //        //}
                //        ////Tercera condición Comidas
                //        //else if ((fHoraInicio >= fInicioComida && fHoraFinal > fFinComida) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                //        //{
                //        //    if (bEsInterFinal) { oCant.iCantComidasInt++; oComDia1.iComidaInt++; } else { oCant.iCantComidas++; oComDia1.iComidaNal++; }
                //        //}
                //        ////Cuarta condición Comidas
                //        //else if ((fHoraInicio < fInicioComida && fHoraFinal <= fInicioComida) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                //        //{
                //        //    if (bEsInterFinal) { oCant.iCantComidasInt++; oComDia1.iComidaInt++; } else { oCant.iCantComidas++; oComDia1.iComidaNal++; }
                //        //}
                //        ////Quinta condición Comidas
                //        //else if ((fHoraInicio < fInicioComida && fHoraFinal < fInicioComida) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                //        //{
                //        //    if (bEsInterFinal) { oCant.iCantComidasInt++; oComDia1.iComidaInt++; } else { oCant.iCantComidas++; oComDia1.iComidaNal++; }
                //        //}
                //        ////Sexta condición Comidas
                //        //else if ((fHoraInicio < fInicioComida && (fHoraFinal > fInicioComida && fHoraFinal <= fFinComida)) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                //        //{
                //        //    if (bEsInterFinal) { oCant.iCantComidasInt++; oComDia1.iComidaInt++; } else { oCant.iCantComidas++; oComDia1.iComidaNal++; }
                //        //}
                //        ////Septima condición Comidas
                //        //else if ((fHoraInicio >= fInicioComida && fHoraInicio <= fFinComida) && (fHoraFinal > fInicioComida && fHoraFinal <= fFinComida) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                //        //{
                //        //    if (bEsInterFinal) { oCant.iCantComidasInt++; oComDia1.iComidaInt++; } else { oCant.iCantComidas++; oComDia1.iComidaNal++; }
                //        //}
                //        ////Octava condición
                //        //else if ((fHoraInicio < fInicioComida && (fHoraFinal > fInicioComida && fHoraFinal >= fFinComida)) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                //        //{
                //        //    if (bEsInterFinal) { oCant.iCantComidasInt++; oComDia1.iComidaInt++; } else { oCant.iCantComidas++; oComDia1.iComidaNal++; }
                //        //}
                //        #endregion

                //        #region CENA
                //        ////Primera condicion Cenas
                //        //if (((fHoraInicio < fInicioCena && fHoraFinal > fInicioCena) && fHoraFinal < fFinCena) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                //        //{
                //        //    if (bEsInterFinal) { oCant.iCantCenasInt++; oComDia1.iCenaInt++; } else { oCant.iCantCenas++; oComDia1.iCenaNal++; }
                //        //}
                //        ////Segunda condición Cenas
                //        //else if ((fHoraInicio > fInicioCena && fHoraInicio < fFinCena) && (fHoraFinal > fFinCena && fHoraFinal > fInicioCena) && (oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                //        //{
                //        //    if (bEsInterFinal) { oCant.iCantCenasInt++; oComDia1.iCenaInt++; } else { oCant.iCantCenas++; oComDia1.iCenaNal++; }
                //        //}
                //        ////Tercera condición Cenas
                //        //else if ((fHoraInicio >= fInicioCena && fHoraFinal > fFinCena) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                //        //{
                //        //    if (bEsInterFinal) { oCant.iCantCenasInt++; oComDia1.iCenaInt++; } else { oCant.iCantCenas++; oComDia1.iCenaNal++; }
                //        //}
                //        ////Cuarta condición Cenas
                //        //else if ((fHoraInicio < fInicioCena && fHoraFinal <= fInicioCena) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                //        //{
                //        //    if (bEsInterFinal) { oCant.iCantCenasInt++; oComDia1.iCenaInt++; } else { oCant.iCantCenas++; oComDia1.iCenaNal++; }
                //        //}
                //        ////Quinta condición Cenas
                //        //else if ((fHoraInicio < fInicioCena && fHoraFinal < fInicioCena) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S()))
                //        //{
                //        //    if (bEsInterFinal) { oCant.iCantCenasInt++; oComDia1.iCenaInt++; } else { oCant.iCantCenas++; oComDia1.iCenaNal++; }
                //        //}
                //        ////Sexta condición Cenas
                //        //else if ((fHoraInicio < fInicioCena && (fHoraFinal > fInicioCena && fHoraFinal <= fFinCena)) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                //        //{
                //        //    if (bEsInterFinal) { oCant.iCantCenasInt++; oComDia1.iCenaInt++; } else { oCant.iCantCenas++; oComDia1.iCenaNal++; }
                //        //}
                //        ////Septima condición Cenas
                //        //else if ((fHoraInicio >= fInicioCena && fHoraInicio <= fFinCena) && (fHoraFinal > fInicioCena && fHoraFinal <= fFinCena) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                //        //{
                //        //    if (bEsInterFinal) { oCant.iCantCenasInt++; oComDia1.iCenaInt++; } else { oCant.iCantCenas++; oComDia1.iCenaNal++; }
                //        //}
                //        ////Octava condición Cenas
                //        //else if ((fHoraInicio < fInicioCena && (fHoraFinal > fInicioCena && fHoraFinal >= fFinCena)) && (oComDia1.sDestino != dtLegs.Rows[0]["HomeBase"].S() || oComDia1.sDestino == dtLegs.Rows[0]["HomeBase"].S()))
                //        //{
                //        //    if (bEsInterFinal) { oCant.iCantCenasInt++; oComDia1.iCenaInt++; } else { oCant.iCantCenas++; oComDia1.iCenaNal++; }
                //        //}
                //        #endregion

                //    }

                //    oLsComDia.Add(oComDia1);
                //}
                #endregion



                return oCant;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private DataTable DBGetObtieneTablaCalculos(List<CantidadComidas> oLstCant, DataTable dtPilotos)
        {
            try
            {
                if (oLstCant.Count > 0)
                {
                    if (dtComidas == null)
                    {
                        dtComidas = new DataTable();
                        dtComidas.Columns.Add("IdFolio", typeof(int));
                        dtComidas.Columns.Add("CrewCode");
                        dtComidas.Columns.Add("FechaInicio");
                        dtComidas.Columns.Add("FechaFin");
                        dtComidas.Columns.Add("DesayunosNal", typeof(int));
                        dtComidas.Columns.Add("DesayunosInt", typeof(int));
                        dtComidas.Columns.Add("ComidasNal", typeof(int));
                        dtComidas.Columns.Add("ComidasInt", typeof(int));
                        dtComidas.Columns.Add("CenasNal", typeof(int));
                        dtComidas.Columns.Add("CenasInt", typeof(int));
                        dtComidas.Columns.Add("Piloto", typeof(string));
                        dtComidas.Columns.Add("HomeBase", typeof(string));

                        foreach (DataRow row in dtPilotos.Rows)
                        {
                            var Results = oLstCant.Where(r => r.sCrewCode == row["ClavePiloto"].S()).ToList();

                            int iDesNal = 0;
                            int iDesInt = 0;
                            int iComNal = 0;
                            int iComInt = 0;
                            int iCenNal = 0;
                            int iCenInt = 0;

                            foreach (CantidadComidas oCant in Results)
                            {
                                iDesNal += oCant.iCantDesayunos;
                                iDesInt += oCant.iCantDesayunosInt;
                                iComNal += oCant.iCantComidas;
                                iComInt += oCant.iCantComidasInt;
                                iCenNal += oCant.iCantCenas;
                                iCenInt += oCant.iCantCenasInt;
                            }

                            DataRow dr = dtComidas.NewRow();
                            dr["IdFolio"] = 0;
                            dr["CrewCode"] = row["ClavePiloto"].S();
                            dr["FechaInicio"] = oLstCant[0].dtFechaInicio.ToString("dd/MM/yyyy"); //oLstCant[0].dtFechaInicio.ToString("dd/MM/yyyy 00:00:00");
                            dr["FechaFin"] = oLstCant[0].dtFechaFin.AddDays(1).AddSeconds(-1).ToString("dd/MM/yyyy"); //oLstCant[0].dtFechaFin.AddDays(1).AddSeconds(-1).ToString("dd/MM/yyyy 00:00:00");
                            dr["DesayunosNal"] = iDesNal;
                            dr["DesayunosInt"] = iDesInt;
                            dr["ComidasNal"] = iComNal;
                            dr["ComidasInt"] = iComInt;
                            dr["CenasNal"] = iCenNal;
                            dr["CenasINT"] = iCenInt;
                            dr["Piloto"] = row["Piloto"];
                            dr["HomeBase"] = row["HomeBase"];

                            dtComidas.Rows.Add(dr);
                        }
                    }
                    else
                    {
                        foreach (DataRow row in dtPilotos.Rows)
                        {
                            var Results = oLstCant.Where(r => r.sCrewCode == row["ClavePiloto"].S()).ToList();

                            int iDesNal = 0;
                            int iDesInt = 0;
                            int iComNal = 0;
                            int iComInt = 0;
                            int iCenNal = 0;
                            int iCenInt = 0;

                            foreach (CantidadComidas oCant in Results)
                            {
                                iDesNal += oCant.iCantDesayunos;
                                iDesInt += oCant.iCantDesayunosInt;
                                iComNal += oCant.iCantComidas;
                                iComInt += oCant.iCantComidasInt;
                                iCenNal += oCant.iCantCenas;
                                iCenInt += oCant.iCantCenasInt;
                            }

                            DataRow dr = dtComidas.NewRow();
                            dr["IdFolio"] = 0;
                            dr["CrewCode"] = row["ClavePiloto"].S();
                            dr["FechaInicio"] = oLstCant[0].dtFechaInicio.ToString("dd/MM/yyyy"); //oLstCant[0].dtFechaInicio.ToString("dd/MM/yyyy 00:00:00");
                            dr["FechaFin"] = oLstCant[0].dtFechaFin.AddDays(1).AddSeconds(-1).ToString("dd/MM/yyyy"); //oLstCant[0].dtFechaFin.AddDays(1).AddSeconds(-1).ToString("dd/MM/yyyy 00:00:00");
                            dr["DesayunosNal"] = iDesNal;
                            dr["DesayunosInt"] = iDesInt;
                            dr["ComidasNal"] = iComNal;
                            dr["ComidasInt"] = iComInt;
                            dr["CenasNal"] = iCenNal;
                            dr["CenasINT"] = iCenInt;
                            dr["Piloto"] = row["Piloto"];
                            dr["HomeBase"] = row["HomeBase"];

                            dtComidas.Rows.Add(dr);
                        }
                    }
                }
                return dtComidas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void eGetParams_Presenter(object sender, EventArgs e)
        {
            oIView.dsParams = oIGestCat.ObtieneParametrosViaticos();
        }

        protected override void SaveObj_Presenter(object sender, EventArgs e)
        {
            oIView.iIdPeriodo = oIGestCat.SetInsertaPeriodo(oIView.oPer);

            if (oIView.iIdPeriodo != 0)
            {
                //Guarda conceptos, adicionales y vuelos
                if (oIView.oLst.Count > 0)
                {
                    if (oIGestCat.SetInsertaConceptosPilotoBitacora(oIView.oLst, oIView.iIdPeriodo))
                    {
                        if (oIGestCat.SetInsertaVuelosPiernasPiloto(oIView.oLstVP, oIView.iIdPeriodo))
                        {
                            if(oIGestCat.SetInsertaViaticosPorDia(oIView.oLstPorDia, oIView.iIdPeriodo))
                                oIView.sOk = "ok";
                            else
                                oIView.sOk = "error";
                        }                            
                    }
                }
            }
        }
        protected void SavePeriodos_Presenter(object sender, EventArgs e)
        {
            if (oIView.oLstPeriodo.Count > 0)
            {
                for (int i = 0; i < oIView.oLstPeriodo.Count; i++)
                {
                    oIView.iIdPeriodo = oIGestCat.SetInsertaPeriodo(oIView.oLstPeriodo[i]);

                    if (oIView.iIdPeriodo > 0)
                    {
                        if (oIGestCat.SetInsertaConceptosPilotoBitacora(oIView.oLst, oIView.iIdPeriodo))
                        {
                            //Inserta piernas
                            if (oIGestCat.SetInsertaVuelosPiernasPiloto(oIView.oLstVP, oIView.iIdPeriodo))
                                oIView.sOk = "ok";
                            else
                                oIView.sOk = "error";
                        }
                    }
                }
            }
        }
        protected void SearchEstatus_Presenter(object sender, EventArgs e)
        {
            oIView.iEstatus = oIGestCat.GetEstatusPeriodo(oIView.sCvePiloto, oIView.sFechaInicio.Dt(), oIView.sFechaFinal.Dt());
        }
        protected void SearchPeriodo_Presenter(object sender, EventArgs e)
        {
            oIView.iIdPeriodo = oIGestCat.GetPeriodoPiloto(oIView.sCvePiloto, oIView.sFechaInicio.Dt(), oIView.sFechaFinal.Dt());
        }
        protected void SearchConAdPeriodo_Presenter(object sender, EventArgs e)
        {
            oIView.LlenaAdicionalesPeriodo(oIGestCat.GetAjustesPeriodoPiloto(oIView.iIdPeriodo));
        }
        protected void SaveAjustes_Presenter(object sender, EventArgs e)
        {
            bool bRes = oIGestCat.SetInsertaAjuste(oIView.oAjuste);
            if(bRes)
                oIView.sOk = "correcto";
            else
                oIView.sOk = "error";
        }
        protected void SearchAjustesPiloto_Presenter(object sender, EventArgs e)
        {
            oIView.LlenaAjustesPorPiloto(oIGestCat.GetAjustesPeriodoPiloto(oIView.iIdPeriodo));
        }
        protected void RemoveAjuste_Presenter(object sender, EventArgs e)
        {
            bool bRes = oIGestCat.SetEliminarAjuste(oIView.iIdAjuste);
            if (bRes)
                oIView.sOk = "correcto";
            else
                oIView.sOk = "error";
        }
        protected void SearchExistePeriodoPic_Presenter(object sender, EventArgs e)
        {
            oIView.iExistePeriodo = oIGestCat.GetExistePeriodoPiloto(oIView.sCvePiloto, oIView.sFechaInicio.Dt(), oIView.sFechaFinal.Dt());
        }
        protected void SearchReporte_Presenter(object sender, EventArgs e)
        {
            oIView.LlenaReporte(oIGestCat.ObtieneDatosReporte(oIView.iIdPeriodo));
        }
        protected void SearchReporteGral_Presenter(object sender, EventArgs e)
        {
            oIView.LlenaReporteGral(oIGestCat.ObtieneDatosReporteGral(oIView.sFechaInicio, oIView.sFechaFinal));
        }
        protected void SearchGuardados_Presenter(object sender, EventArgs e)
        {
            try
            {
                oIView.LlenaPeriodosGuardados(oIGestCat.GetPeriodosGuardados(oIView.sFechaInicio, oIView.sFechaFinal, oIView.sCvePiloto));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void SearchViaticosGuardados_Presenter(object sender, EventArgs e)
        {
            try
            {
                oIView.LlenaViaticosGuardados(oIGestCat.GetDatosGuardados(oIView.iIdPeriodo));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}