using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ALE_MexJet.Objetos
{
    [Serializable, Bindable(BindableSupport.Yes)]
    public class Contrato_CaracteristicasEspeciales
    {
        private int _iIdContrato = -1;
        private string _sTiempoMinimoSolicitud = string.Empty;
        private decimal _dTiempoMinimoSolicitudFBN = 0;         // NUEVO
        private string _sTiempoMinimoSolicitudCA = string.Empty;
        private string _sTiempoMinimoSolicitudFeriado = string.Empty;
        private decimal _dRangoAcomodoVuelosFeriado = 0;        // NUEVO
        private decimal _dReprogramarSalidaAntesProgramada = 0; // NUEVO
        private bool _bPenalizacion = false;
        private decimal _dCancelacionAnticipadaSB = 0;          // NUEVO
        private decimal _dCancelacionAnticipadaFB = 0;          // NUEVO
        private string _sPenalizacionAleIncuplimiento = string.Empty;
        private string _sPenalizacionClienteRetraso = string.Empty;
        private string _sAcuerdosEspeciales = string.Empty;
        private string _sAntiguedadAeronave = string.Empty;
        private string _sTiempoMinimoCancelarVuelo = string.Empty;
        private bool _bVuelosSimultaneos = false;
        private int _iVuelosSimultaneos = -1;
        private decimal _dFactorVloSim = 0;
        private string _sComentarios = string.Empty;

        private string _sNotas = string.Empty;
        public string sNotas { get { return _sNotas; } set { _sNotas = value; } }

        public int iIdContrato { get { return _iIdContrato; } set { _iIdContrato = value; } }
        public string sPenalizacionAleIncuplimiento { get { return _sPenalizacionAleIncuplimiento; } set { _sPenalizacionAleIncuplimiento = value; } }
        public string sPenalizacionClienteRetraso { get { return _sPenalizacionClienteRetraso; } set { _sPenalizacionClienteRetraso = value; } }
        public string sAcuerdosEspeciales { get { return _sAcuerdosEspeciales; } set { _sAcuerdosEspeciales = value; } }
        public string sAntiguedadAeronave { get { return _sAntiguedadAeronave; } set { _sAntiguedadAeronave = value; } }
        public string sTiempoMinimoCancelarVuelo { get { return _sTiempoMinimoCancelarVuelo; } set { _sTiempoMinimoCancelarVuelo = value; } }
        public string sTiempoMinimoSolicitud { get { return _sTiempoMinimoSolicitud; } set { _sTiempoMinimoSolicitud = value; } }
        public string sTiempoMinimoSolicitudFeriado { get { return _sTiempoMinimoSolicitudFeriado; } set { _sTiempoMinimoSolicitudFeriado = value; } }
        public string sTiempoMinimoSolicitudCA { get { return _sTiempoMinimoSolicitudCA; } set { _sTiempoMinimoSolicitudCA = value; } }
        public bool bPenalizacion { get { return _bPenalizacion; } set { _bPenalizacion = value; } }
        public bool bVuelosSimultaneos { get { return _bVuelosSimultaneos; } set { _bVuelosSimultaneos = value; } }
        public int iVuelosSimultaneos { get { return _iVuelosSimultaneos; } set { _iVuelosSimultaneos = value; } }
        public decimal dFactorVloSim { get { return _dFactorVloSim; } set { _dFactorVloSim = value; } }
        public string sComentarios { get { return _sComentarios; } set { _sComentarios = value; } }
        public decimal dTiempoMinimoSolicitudFBN { get { return _dTiempoMinimoSolicitudFBN; } set { _dTiempoMinimoSolicitudFBN = value; } }
        public decimal dRangoAcomodoVuelosFeriado { get { return _dRangoAcomodoVuelosFeriado; } set { _dRangoAcomodoVuelosFeriado = value; } }
        public decimal dReprogramarSalidaAntesProgramada { get { return _dReprogramarSalidaAntesProgramada; } set { _dReprogramarSalidaAntesProgramada = value; } }
        public decimal dCancelacionAnticipadaSB { get { return _dCancelacionAnticipadaSB; } set { _dCancelacionAnticipadaSB = value; } }
        public decimal dCancelacionAnticipadaFB { get { return _dCancelacionAnticipadaFB; } set { _dCancelacionAnticipadaFB = value; } }
    }
}