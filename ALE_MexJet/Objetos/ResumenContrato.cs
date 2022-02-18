using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ALE_MexJet.Objetos
{
    public class ResumenContrato: BaseObjeto
    {
        private int _iIdResumen = 0;
        private decimal _dAnualidades = 0;
        private string _sFacturas = string.Empty;

        public int iIdResumen { get { return _iIdResumen; } set { _iIdResumen = value; } }
        public decimal dAnualidades { get { return _dAnualidades; } set { _dAnualidades = value; } }
        public string sFacturas { get { return _sFacturas; } set { _sFacturas = value; } }

    }
}