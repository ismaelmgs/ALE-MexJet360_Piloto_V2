using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ALE_MexJet.Objetos
{
    [Serializable]
    public class Login
    {
        public string email { set; get; }
        public string password { set; get; }
    }
    [Serializable]
    public class nombreUsuario
    {
        public string nombre { set; get; }
    }

    public class FiltroEmail
    {
        public string email { get; set; }
    }
}