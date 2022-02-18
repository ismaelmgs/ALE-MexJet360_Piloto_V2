using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ALE_MexJet.Objetos
{
    public class DatosToken
    {
        private string _privateKey = string.Empty;

        public string privateKey { get { return _privateKey; } set { _privateKey = value; } }
    }

    public class Token
    {
        private string _token = string.Empty;
        public string token { get { return _token; } set { _token = value; } }
    }
}