﻿using ALE_MexJet.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE_MexJet.Interfaces
{
    public interface IViewCalculoPagos : IBaseView
    {
        string sCvePiloto { get; set; }
        string sFechaInicio { get; set; }
        string sFechaFinal { get; set; }

        string sParametro { get; set; }
        string sFechaDesde { get; set; }
        string sFechaHasta { get; set; }
        void LoadConceptos(DataTable dt);
        void LoadPilotos(DataTable dt);
        void LoadVuelos(DataTable dt);

        List<CantidadComidas> oLstCant { set; get; }
        List<CantidadHoteles> oLstCantH { set; get; }

        PeriodoPiloto oPer { get; }
        ConceptosAdicionalesPiloto oAjuste { get; }

        List<ConceptosPiloto> oLst { set; get; }
        List<ConceptosPilotoSave> oLstCon { set; get; }

        List<HotelesPiloto> oLstHP { set; get; }
        List<HotelesPilotoSave> oLstHPS { set; get; }

        List<ConceptosAdicionalesPiloto> oLstAd { set; get; }
        List<VuelosPiernasPiloto> oLstVP { set; get; }
        List<VuelosPiernasPilotoSave> oLstVPil { set; get; }
        List<PeriodoPiloto> oLstPeriodo { set; get; }

        List<ConceptosViaticosPorDia> oLstPorDia { set; get; }
        List<ConceptosViaticosPorDiaSave> oLstPorDiaPil { set; get; }

        List<ViaticosHotelPorDia> oLstHotelPorDia { set; get; }
        List<ViaticosHotelPorDiaSave> oLstHotelPorDiaSave { set; get; }

        void LlenaCalculoPilotos(DataTable dt);
        void LlenaVuelosPiloto(DataTable dt);
        DataSet dsParams { set; get; }
        DataSet dsParamsH { set; get; }
        int iIdPeriodo { set; get; }
        string sOk { set; get; }
        int iEstatus { set; get; }
        int iExistePeriodo { set; get; }
        int iIdAjuste { set; get; }
        void LoadsGrids(DataSet ds);
        void LoadsHoteles(DataSet ds);
        void LlenaAdicionalesPeriodo(DataTable dt);
        void LlenaAjustesPorPiloto(DataTable dt);

        DataTable dtDiasViaticos { set; get; }
        DataTable dtDiasViaticosHoteles { set; get; }

        DataTable dtViaticosHot { get; set; }

        void LlenaReporte(DataSet ds);
        void LlenaReporteGral(DataSet ds);

        void LlenaPeriodosGuardados(DataTable dt);
        void LlenaViaticosGuardados(DataSet ds);
        PeriodoPiloto oPe { set; get; }
        DataTable dtResumenViaticos { set; get; }
        DataTable dtResumenViaticosHotel { set; get; }

        event EventHandler eSearchConceptos;
        event EventHandler eSearchVuelos;
        event EventHandler eSearchCalculos;
        event EventHandler eGetParams;
        event EventHandler eGetAdicionales;
        event EventHandler eSavePeriodos;
        event EventHandler eSearchEstatus;
        event EventHandler eSearchPeriodo;
        event EventHandler eSearchConAdPeriodo;
        event EventHandler eSaveAjustes;
        event EventHandler eSearchAjustesPiloto;
        event EventHandler eRemoveAjuste;
        event EventHandler eSearchExistePeriodoPic;
        event EventHandler eSearchReporte;
        event EventHandler eSearchReporteGral;
        event EventHandler eSearchGuardados;
        event EventHandler eSearchViaticosGuardados;
    }
}
