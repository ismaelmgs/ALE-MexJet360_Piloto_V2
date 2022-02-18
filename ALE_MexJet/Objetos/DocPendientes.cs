using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ALE_MexJet.Objetos
{
    [Serializable, Bindable(BindableSupport.Yes)]
    public class DocPendientes : BaseObjeto
    {
        private string _IdCliente = "0";
        private string _IdContrato = string.Empty;
        private int _status = 1;
        private string _NombreCliente = string.Empty;
        private string _FechaIni = string.Empty;
        private string _FechaFin = string.Empty;


        [Display(AutoGenerateField = false), ScaffoldColumn(false)]

        public string NombreCliente { get { return _NombreCliente; } set { _NombreCliente = value; } }

        public int status { get { return _status; } set { _status = value; } }

        public string IdCliente { get { return _IdCliente; } set { _IdCliente = value; } }

        public string IdContrato { get { return _IdContrato; } set { _IdContrato = value; } }

        public string FechaIni { get { return _FechaIni; } set { _FechaIni = value; } }

        public string FechaFin { get { return _FechaFin; } set { _FechaFin = value; } }
        
    }
}