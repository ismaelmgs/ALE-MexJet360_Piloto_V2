using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ALE_MexJet.Objetos
{
    [Serializable]
    public class Bitacoras
    {
        private string _sMatricula = string.Empty;
        private long _lTripNum = 0;
        private int _iLegNum = 0;
        private string _sFolioReal = string.Empty;
        private string _sVueloContratoId = string.Empty;
        private string _sPilotoId = string.Empty;
        private string _sCopilotoId = string.Empty;
        private DateTime _dtFecha;
        private string _sOrigen = string.Empty;
        private string _sDestino = string.Empty;
        private string _sOrigenVuelo = string.Empty;
        private DateTime _dtDestinoVuelo;
        private DateTime _dtOrigenCalzo;
        private string _sDestinoCalzo = string.Empty;
        private string _sConsumoOrigen = string.Empty;
        private string _sConsumoDestino = string.Empty;
        private string _sCantPax = string.Empty;
        private string _sTipo = string.Empty;
        private string _sUsuario = string.Empty;
        private string _sLongNum = string.Empty;
        private long _lLegId = 0;

        public string SMatricula { get => _sMatricula; set => _sMatricula = value; }
        public long LTripNum { get => _lTripNum; set => _lTripNum = value; }
        public int ILegNum { get => _iLegNum; set => _iLegNum = value; }
        public string SFolioReal { get => _sFolioReal; set => _sFolioReal = value; }
        public string SVueloContratoId { get => _sVueloContratoId; set => _sVueloContratoId = value; }
        public string SPilotoId { get => _sPilotoId; set => _sPilotoId = value; }
        public string SCopilotoId { get => _sCopilotoId; set => _sCopilotoId = value; }
        public DateTime DtFecha { get => _dtFecha; set => _dtFecha = value; }
        public string SOrigen { get => _sOrigen; set => _sOrigen = value; }
        public string SDestino { get => _sDestino; set => _sDestino = value; }
        public string SOrigenVuelo { get => _sOrigenVuelo; set => _sOrigenVuelo = value; }
        public DateTime DtDestinoVuelo { get => _dtDestinoVuelo; set => _dtDestinoVuelo = value; }
        public DateTime DtOrigenCalzo { get => _dtOrigenCalzo; set => _dtOrigenCalzo = value; }
        public string SDestinoCalzo { get => _sDestinoCalzo; set => _sDestinoCalzo = value; }
        public string SConsumoOrigen { get => _sConsumoOrigen; set => _sConsumoOrigen = value; }
        public string SConsumoDestino { get => _sConsumoDestino; set => _sConsumoDestino = value; }
        public string SCantPax { get => _sCantPax; set => _sCantPax = value; }
        public string STipo { get => _sTipo; set => _sTipo = value; }
        public string SUsuario { get => _sUsuario; set => _sUsuario = value; }
        public string SLongNum { get => _sLongNum; set => _sLongNum = value; }
        public long LLegId { get => _lLegId; set => _lLegId = value; }
    }
}