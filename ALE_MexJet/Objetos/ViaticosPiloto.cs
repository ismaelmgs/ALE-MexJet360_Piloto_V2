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
        private int _IdViaticos = 0;
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
        public int IdViaticos { get => _IdViaticos; set => _IdViaticos = value; }
    }
    [Serializable]
    public class ConceptosPilotoSave
    {
        public int IdViaticos { get; set; }
        public int IdPeriodo { get; set; }
        public int IdConcepto { get; set; }
        public string DesConcepto { get; set; }
        public int Cantidad { get; set; }
        public decimal MontoConcepto { get; set; }
        public decimal Total { get; set; }
        public string Moneda { get; set; }
        public int Estatus { get; set; }
    }

    [Serializable]
    public class ConceptosViaticosPorDia
    {
        private int _Id = 0;
        private int _iIdPeriodo = 0;
        private string _sMoneda = string.Empty;
        private DateTime _dtFecha;
        private decimal _dDesayuno = 0;
        private decimal _dComida = 0;
        private decimal _dCena = 0;
        private decimal _dTotal = 0;

        private decimal _dTipoCambio = 0;
        
        private decimal _dDesNac = 0;
        private decimal _dDesInt = 0;
        private decimal _dComNac = 0;
        private decimal _dComInt = 0;
        private decimal _dCenNac = 0;
        private decimal _dCenInt = 0;


        public int IIdPeriodo { get => _iIdPeriodo; set => _iIdPeriodo = value; }
        public string SMoneda { get => _sMoneda; set => _sMoneda = value; }
        public DateTime DtFecha { get => _dtFecha; set => _dtFecha = value; }
        public decimal DDesayuno { get => _dDesayuno; set => _dDesayuno = value; }
        public decimal DComida { get => _dComida; set => _dComida = value; }
        public decimal DCena { get => _dCena; set => _dCena = value; }
        public decimal DTotal { get => _dTotal; set => _dTotal = value; }
        public decimal DTipoCambio { get => _dTipoCambio; set => _dTipoCambio = value; }
        public decimal DDesNac { get => _dDesNac; set => _dDesNac = value; }
        public decimal DDesInt { get => _dDesInt; set => _dDesInt = value; }
        public decimal DComNac { get => _dComNac; set => _dComNac = value; }
        public decimal DComInt { get => _dComInt; set => _dComInt = value; }
        public decimal DCenNac { get => _dCenNac; set => _dCenNac = value; }
        public decimal DCenInt { get => _dCenInt; set => _dCenInt = value; }
        public int Id { get => _Id; set => _Id = value; }
    }
    [Serializable]
    public class ConceptosViaticosPorDiaSave
    {
        public int Id { get; set; }
        public int IdPeriodo { get; set; }
        public string Moneda { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Desayuno { get; set; }
        public decimal Comida { get; set; }
        public decimal Cena { get; set; }
        public decimal DesNac { get; set; }
        public decimal DesInt { get; set; }
        public decimal ComNac { get; set; }
        public decimal ComInt { get; set; }
        public decimal CenNac { get; set; }
        public decimal CenInt { get; set; }
        public decimal Total { get; set; }
        public int Estatus { get; set; }
        public decimal TipoCambio { get; set; }
        
    }

    [Serializable]
    public class ViaticosHotelPorDia
    {
        private int _Id = 0;
        private int _iIdPeriodo = 0;
        private string _sMoneda = string.Empty;
        private DateTime _dtFecha;
        private decimal _dTipoCambio = 0;
        private decimal _dHotNac = 0;
        private decimal _dHotInt = 0;
        private decimal _dTotal = 0;

        public int Id { get => _Id; set => _Id = value; }
        public int IIdPeriodo { get => _iIdPeriodo; set => _iIdPeriodo = value; }
        public string SMoneda { get => _sMoneda; set => _sMoneda = value; }
        public DateTime DtFecha { get => _dtFecha; set => _dtFecha = value; }
        public decimal DTipoCambio { get => _dTipoCambio; set => _dTipoCambio = value; }
        public decimal DHotNac { get => _dHotNac; set => _dHotNac = value; }
        public decimal DHotInt { get => _dHotInt; set => _dHotInt = value; }
        public decimal DTotal { get => _dTotal; set => _dTotal = value; }
    }
    [Serializable]
    public class ViaticosHotelPorDiaSave
    {
        private int _Id = 0;
        private int _iIdPeriodo = 0;
        private string _sMoneda = string.Empty;
        private DateTime _dtFecha;
        private decimal _dTipoCambio = 0;
        private decimal _dHotNac = 0;
        private decimal _dHotInt = 0;
        private decimal _dTotal = 0;

        public int Id { get => _Id; set => _Id = value; }
        public int IIdPeriodo { get => _iIdPeriodo; set => _iIdPeriodo = value; }
        public string SMoneda { get => _sMoneda; set => _sMoneda = value; }
        public DateTime DtFecha { get => _dtFecha; set => _dtFecha = value; }
        public decimal DTipoCambio { get => _dTipoCambio; set => _dTipoCambio = value; }
        public decimal DHotNac { get => _dHotNac; set => _dHotNac = value; }
        public decimal DHotInt { get => _dHotInt; set => _dHotInt = value; }
        public decimal DTotal { get => _dTotal; set => _dTotal = value; }
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
        private int _IdVuelo = 0;
        private int _iIdPeriodo = 0;
        private long _lTrip = 0;
        private long _lLegId = 0;

        public int IIdPeriodo { get => _iIdPeriodo; set => _iIdPeriodo = value; }
        public long LTrip { get => _lTrip; set => _lTrip = value; }
        public long LLegId { get => _lLegId; set => _lLegId = value; }
        public int IdVuelo { get => _IdVuelo; set => _IdVuelo = value; }
    }
    [Serializable]
    public class VuelosPiernasPilotoSave
    {
        public int IdVuelo { get; set; }
        public int IdPeriodo { get; set; }
        public long Trip { get; set; }
        public long LegId { get; set; }
    }


    [Serializable]
    public class HotelesPiloto
    {
        private int _IdViaticosHotel = 0;
        private int _iIdPeriodo = 0;
        private int _iIdHotel = 0;
        private string _sDesHotel = string.Empty;
        private int _iCantidad = 0;
        private decimal _dMontoHotel = 0;
        private decimal _dTotal = 0;
        private string _sMoneda = string.Empty;
        private int _iEstatus = 0;

        public int IdViaticosHotel { get => _IdViaticosHotel; set => _IdViaticosHotel = value; }
        public int IIdPeriodo { get => _iIdPeriodo; set => _iIdPeriodo = value; }
        public int IIdHotel { get => _iIdHotel; set => _iIdHotel = value; }
        public string SDesHotel { get => _sDesHotel; set => _sDesHotel = value; }
        public int ICantidad { get => _iCantidad; set => _iCantidad = value; }
        public decimal DMontoHotel { get => _dMontoHotel; set => _dMontoHotel = value; }
        public decimal DTotal { get => _dTotal; set => _dTotal = value; }
        public string SMoneda { get => _sMoneda; set => _sMoneda = value; }
        public int IEstatus { get => _iEstatus; set => _iEstatus = value; }
    }
    [Serializable]
    public class HotelesPilotoSave
    {
        public int IdViaticosHotel { get; set; }
        public int IdPeriodo { get; set; }
        public int IdHotel { get; set; }
        public string DesHotel { get; set; }
        public int Cantidad { get; set; }
        public decimal MontoHotel { get; set; }
        public decimal Total { get; set; }
        public string Moneda { get; set; }
        public int Estatus { get; set; }
    }
}