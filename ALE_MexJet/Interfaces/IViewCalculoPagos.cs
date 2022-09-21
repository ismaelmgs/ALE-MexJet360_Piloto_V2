using ALE_MexJet.Objetos;
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

        PeriodoPiloto oPer { get; }

        List<ConceptosPiloto> oLst { set; get; }
        List<ConceptosAdicionalesPiloto> oLstAd { set; get; }
        List<VuelosPiernasPiloto> oLstVP { set; get; }

        List<PeriodoPiloto> oLstPeriodo { set; get; }

        void LlenaCalculoPilotos(DataTable dt);
        void LlenaVuelosPiloto(DataTable dt);
        DataSet dsParams { set; get; }
        int iIdPeriodo { set; get; }
        string sOk { set; get; }
        int iEstatus { set; get; }
        void LoadsGrids(DataSet ds);

        event EventHandler eSearchConceptos;
        event EventHandler eSearchVuelos;
        event EventHandler eSearchCalculos;
        event EventHandler eGetParams;
        event EventHandler eGetAdicionales;
        event EventHandler eSavePeriodos;
        event EventHandler eSearchEstatus;
    }
}
