using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ALE_MexJet.Objetos
{
    [Serializable]
    public class PeriodoPiloto
    {
        private string _sCvePiloto = string.Empty;
        private string _sFechaInicio = string.Empty;
        private string _sFechaFinal = string.Empty;
        private int _iEstatus = 0;
        private string _sUsuario = string.Empty;
        private List<ConceptosPiloto> _oLst = new List<ConceptosPiloto>();
        private List<ConceptosAdicionalesPiloto> _oLstAd = new List<ConceptosAdicionalesPiloto>();
        private List<VuelosPiernasPiloto> _oLstVP = new List<VuelosPiernasPiloto>();

        public string SCvePiloto { get => _sCvePiloto; set => _sCvePiloto = value; }
        public string SFechaInicio { get => _sFechaInicio; set => _sFechaInicio = value; }
        public string SFechaFinal { get => _sFechaFinal; set => _sFechaFinal = value; }
        public int IEstatus { get => _iEstatus; set => _iEstatus = value; }
        public string SUsuario { get => _sUsuario; set => _sUsuario = value; }
        public List<ConceptosPiloto> oLst { get => _oLst; set => _oLst = value; }
        public List<ConceptosAdicionalesPiloto> oLstAd { get => _oLstAd; set => _oLstAd = value; }
        public List<VuelosPiernasPiloto> oLstVP { get => _oLstVP; set => _oLstVP = value; }
    }

    [Serializable]
    public class ConceptosPiloto
    {
        private int _iIdPeriodo = 0;
        private int _iIdConcepto = 0;
        private string _sDesConcepto = string.Empty;
        private int _iCantidad = 0;
        private decimal _dMontoConcepto = 0;
        private decimal _dTotal = 0;
        private string _sMoneda = string.Empty;
        private int _iEstatus = 0;

        public int IIdPeriodo { get => _iIdPeriodo; set => _iIdPeriodo = value; }
        public int IIdConcepto { get => _iIdConcepto; set => _iIdConcepto = value; }
        public string SDesConcepto { get => _sDesConcepto; set => _sDesConcepto = value; }
        public int ICantidad { get => _iCantidad; set => _iCantidad = value; }
        public decimal DMontoConcepto { get => _dMontoConcepto; set => _dMontoConcepto = value; }
        public decimal DTotal { get => _dTotal; set => _dTotal = value; }
        public string SMoneda { get => _sMoneda; set => _sMoneda = value; }
        public int IEstatus { get => _iEstatus; set => _iEstatus = value; }
    }

    [Serializable]
    public class ConceptosViaticosPorDia
    {
        private int _iIdPeriodo = 0;
        private string _sMoneda = string.Empty;
        private DateTime _dtFecha;
        private decimal _dDesayuno = 0;
        private decimal _dComida = 0;
        private decimal _dCena = 0;
        private decimal _dTotal = 0;

        private decimal _dTipoCambio = 0;

        public int IIdPeriodo { get => _iIdPeriodo; set => _iIdPeriodo = value; }
        public string SMoneda { get => _sMoneda; set => _sMoneda = value; }
        public DateTime DtFecha { get => _dtFecha; set => _dtFecha = value; }
        public decimal DDesayuno { get => _dDesayuno; set => _dDesayuno = value; }
        public decimal DComida { get => _dComida; set => _dComida = value; }
        public decimal DCena { get => _dCena; set => _dCena = value; }
        public decimal DTotal { get => _dTotal; set => _dTotal = value; }
        public decimal DTipoCambio { get => _dTipoCambio; set => _dTipoCambio = value; }
    }

    [Serializable]
    public class ConceptosAdicionalesPiloto
    {
        private int _iIdPeriodo = 0;
        private int _iId_Concepto = 0;
        private string _sDesConcepto = string.Empty;
        private decimal _dValor = 0;
        private string _sMoneda = string.Empty;
        private int _iEstatus = 0;
        private string _sComentarios = string.Empty;

        public int IIdPeriodo { get => _iIdPeriodo; set => _iIdPeriodo = value; }
        public int IId_Concepto { get => _iId_Concepto; set => _iId_Concepto = value; }
        public string SDesConcepto { get => _sDesConcepto; set => _sDesConcepto = value; }
        public decimal DValor { get => _dValor; set => _dValor = value; }
        public string SMoneda { get => _sMoneda; set => _sMoneda = value; }
        public int IEstatus { get => _iEstatus; set => _iEstatus = value; }
        public string SComentarios { get => _sComentarios; set => _sComentarios = value; }
    }

    [Serializable]
    public class VuelosPiernasPiloto
    {
        private int _iIdPeriodo = 0;
        private long _lTrip = 0;
        private long _lLegId = 0;

        public int IIdPeriodo { get => _iIdPeriodo; set => _iIdPeriodo = value; }
        public long LTrip { get => _lTrip; set => _lTrip = value; }
        public long LLegId { get => _lLegId; set => _lLegId = value; }
    }
}