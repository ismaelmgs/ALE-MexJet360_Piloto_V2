using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ALE_MexJet.Objetos
{
    public class ParametrosGrales
    {
        private int _iIdParametro = 0;
        private string _sClave = string.Empty;
        private string _sDescripcion = string.Empty;
        private string _sValor = string.Empty;
        private string _sUsuario = string.Empty;

        public int IIdParametro { get => _iIdParametro; set => _iIdParametro = value; }
        public string SClave { get => _sClave; set => _sClave = value; }
        public string SDescripcion { get => _sDescripcion; set => _sDescripcion = value; }
        public string SValor { get => _sValor; set => _sValor = value; }
        public string SUsuario { get => _sUsuario; set => _sUsuario = value; }
    }
}