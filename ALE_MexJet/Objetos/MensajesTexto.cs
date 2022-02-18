using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ALE_MexJet.Objetos
{
    public class MensajesTexto
    {
        public string message { set; get; }        public string tpoa { set; get; }        public List<recipient> recipient { set; get; }
    }

    public class recipient    {        public string msisdn { set; get; }    }

}