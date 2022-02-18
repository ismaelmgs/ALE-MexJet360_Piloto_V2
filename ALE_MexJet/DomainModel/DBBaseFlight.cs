using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NucleoBase.Core;
using NucleoBase.BaseDeDatos;

namespace ALE_MexJet.DomainModel
{
    public class DBBaseFlight
    {
        public BD_SP oDB_SP = new BD_SP();
        private bool bDisposed = false;

        public DBBaseFlight()
        {
            oDB_SP.sConexionSQL = Globales.GetConfigConnection("SqlDefault");
        }
    }
}