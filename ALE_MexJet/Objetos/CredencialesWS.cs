using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ALE_MexJet.Objetos
{
    [Serializable]
    public class CredencialesWS
    {
        public string username { set; get; }
        public string password { set; get; }
    }

    [Serializable]
    public class TokenWS
    {
        public string token { set; get; }
    }
}