using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ALE_MexJet.Objetos
{
    public class FactorFijo
    {
        private int _iId = -1;
        private string _sClave = string.Empty;
        private string _sDescripcion = string.Empty;
        private decimal _valor = 0;
        private int _iStatus = 1;

        public int iId { get => _iId; set => _iId = value; }
        public string sClave { get => _sClave; set => _sClave = value; }
        public string sDescripcion { get => _sDescripcion; set => _sDescripcion = value; }
        public decimal dValor { get => _valor; set => _valor = value; }
        public int iStatus { get => _iStatus; set => _iStatus = value; }
        
    }
}