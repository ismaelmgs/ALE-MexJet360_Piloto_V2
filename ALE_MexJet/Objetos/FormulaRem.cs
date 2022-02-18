using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ALE_MexJet.Objetos
{
    public class FormulaRem
    {
        private int _iId = -1;
        private string _sFormula = string.Empty;
        private string _sDescripcion = string.Empty;
        private string _CodigoF = string.Empty;
        private int _iStatus = 1;

        public int iId { get => _iId; set => _iId = value; }
        public string sFormula { get => _sFormula; set => _sFormula = value; }
        public string sDescripcion { get => _sDescripcion; set => _sDescripcion = value; }
        public string CodigoF { get => _CodigoF; set => _CodigoF = value; }
        public int iStatus { get => _iStatus; set => _iStatus = value; }

    }

    [Serializable]
    public class useVariables
    {
        private string _sDescripcion = string.Empty;
        private string _sValor = string.Empty;

        public string sDescripcion { get => _sDescripcion; set => _sDescripcion = value; }
        public string sValor { get => _sValor; set => _sValor = value; }
    }
}