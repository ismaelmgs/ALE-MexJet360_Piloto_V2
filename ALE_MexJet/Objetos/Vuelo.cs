using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ALE_MexJet.Objetos
{
    [Serializable]
    public class Vuelo : BaseObjeto
    {
        private int _iTripNum = 0;
        private int _iLegID = 0;
        private string _sCveContrato = string.Empty;
        private string _sMatricula = string.Empty;
        private string _sOrigen = string.Empty;
        private string _sDestino = string.Empty;
        private string _sPaisOrigen = string.Empty;
        private string _sPaisDestino = string.Empty;
        private DateTime _dtOrigenVuelo = DateTime.Now;
        private DateTime _dtDestinoVuelo = DateTime.Now;
        private string _sUsuario = string.Empty;

        public int ITripNum { get => _iTripNum; set => _iTripNum = value; }
        public int ILegID { get => _iLegID; set => _iLegID = value; }
        public string SCveContrato { get => _sCveContrato; set => _sCveContrato = value; }
        public string SMatricula { get => _sMatricula; set => _sMatricula = value; }
        public string SOrigen { get => _sOrigen; set => _sOrigen = value; }
        public string SDestino { get => _sDestino; set => _sDestino = value; }
        public DateTime DtOrigenVuelo { get => _dtOrigenVuelo; set => _dtOrigenVuelo = value; }
        public DateTime DtDestinoVuelo { get => _dtDestinoVuelo; set => _dtDestinoVuelo = value; }
        public string SUsuario { get => _sUsuario; set => _sUsuario = value; }
        public string SPaisOrigen { get => _sPaisOrigen; set => _sPaisOrigen = value; }
        public string SPaisDestino { get => _sPaisDestino; set => _sPaisDestino = value; }
    }
}