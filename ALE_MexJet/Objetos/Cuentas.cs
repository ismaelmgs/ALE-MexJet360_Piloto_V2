using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ALE_MexJet.Objetos
{
    [Serializable]
    public class Cuentas : BaseObjeto
    {
        private string _sTitular = string.Empty;
        private string _sCuenta = string.Empty;
        private string _sNoTarjeta = string.Empty;
        private string _sEdoCorte = string.Empty;
        private string _sCuartaLinea = string.Empty;
        private string _sCvePiloto = string.Empty;

        public string sTitular { get { return _sTitular; } set { _sTitular = value; } }
        public string sCuenta { get { return _sCuenta; } set { _sCuenta = value; } }
        public string sNoTarjeta { get { return _sNoTarjeta; } set { _sNoTarjeta = value; } }
        public string sEdoCorte { get { return _sEdoCorte; } set { _sEdoCorte = value; } }
        public string sCuartaLinea { get { return _sCuartaLinea; } set { _sCuartaLinea = value; } }
        public string sCvePiloto { get { return _sCvePiloto; } set { _sCvePiloto = value; } }
    }
}