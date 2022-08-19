using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ALE_MexJet.Objetos
{
    public class Concepto
    {
        private int _iIdConcepto = 0;
        private string _sHorarioInicial = string.Empty;
        private string _sHorarioFinal = string.Empty;
        private decimal _dMontoMXN = 0;
        private decimal _dMontoUSD = 0;

        public int IIdConcepto { get => _iIdConcepto; set => _iIdConcepto = value; }
        public string SHorarioInicial { get => _sHorarioInicial; set => _sHorarioInicial = value; }
        public string SHorarioFinal { get => _sHorarioFinal; set => _sHorarioFinal = value; }
        public decimal DMontoMXN { get => _dMontoMXN; set => _dMontoMXN = value; }
        public decimal DMontoUSD { get => _dMontoUSD; set => _dMontoUSD = value; }
    }
}