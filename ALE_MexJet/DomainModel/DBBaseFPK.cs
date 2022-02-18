using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ALE_MexJet.Clases;
using NucleoBase.BaseDeDatos;
using NucleoBase.Core;

namespace ALE_MexJet.DomainModel
{
    public class DBBaseFPK
    {
        public BD_SP oDB_SP = new BD_SP();
        private bool bDisposed = false;

        public DBBaseFPK()
        {
            var CadenaEncriptada = Globales.GetConfigConnection("SqlDefaultFPK");
            var CadenaDesencriptada = CadenaEncriptada.ConvertBase64ToString();

            oDB_SP.sConexionSQL = CadenaDesencriptada;
        }
    }
}