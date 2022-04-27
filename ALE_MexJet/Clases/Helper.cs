using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ALE_MexJet.Clases
{
    public class Helper
    {
        public const string DominioWS = "WSMorvelRest";         //PRODUCCION
        //public const string DominioWS = "WSMorvelRestDev";    //DESARROLLO

        public const string UrlToken = "http://201.163.208.231/" + DominioWS + "/ws/authentica";
        public const string UrlLogin = "http://201.163.208.231/" + DominioWS + "/ws/pc/valAccesoUsuarios";
        public const string US_UrlObtieneParametros = "http://201.163.208.231/" + DominioWS + "/ws/pc/obtieneParams";
        public const string US_UrlObtieneValidacionUusario = "http://201.163.208.231/" + DominioWS + "/ws/pc/existeUsr";
    }
}