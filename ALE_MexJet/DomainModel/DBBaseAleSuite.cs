using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NucleoBase.Core;
using NucleoBase.BaseDeDatos;
using ALE_MexJet.Clases;

namespace ALE_MexJet.DomainModel
{
    public class DBBaseAleSuite
    {
        public BD_SP oDB_SP = new BD_SP();
        private bool bDisposed = false;

        public DBBaseAleSuite()
        {
            var CadenaEncriptada = Globales.GetConfigConnection("SqlAleSuite");
            var CadenaDesencriptada = CadenaEncriptada.ConvertBase64ToString();

            oDB_SP.sConexionSQL = CadenaDesencriptada;
        }
    }
}