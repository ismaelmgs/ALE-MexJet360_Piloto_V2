using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ALE_MexJet.Clases;
using NucleoBase.BaseDeDatos;
using NucleoBase.Core;

namespace ALE_MexJet.DomainModel
{
    public class DBBaseSAP
    {
        public BD_SP oDB_SP = new BD_SP();
        private bool bDisposed = false;

        public DBBaseSAP()
        {
            var CadenaEncriptada = Globales.GetConfigConnection("SqlDefaulSAP");
            var CadenaDesencriptada = CadenaEncriptada.ConvertBase64ToString();

            oDB_SP.sConexionSQL = CadenaDesencriptada;
        }
    }
}