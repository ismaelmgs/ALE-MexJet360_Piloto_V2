using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ALE_MexJet.Objetos
{
    [Serializable, Bindable(BindableSupport.Yes)]
    public class Bitacora : BaseObjeto
    {
        private string _NombreCliente = string.Empty;
        private int _status = 1;
        private string _IdCliente = "0";
        private string _IdFolio = string.Empty;
        private string _IdContrato = string.Empty;
        private string _FechaIni = string.Empty;
        private string _FechaFin = string.Empty;
        private int _iIdBitacora;
        
        [Display(AutoGenerateField = false), ScaffoldColumn(false)]

        public string NombreCliente { get { return _NombreCliente; } set { _NombreCliente  = value; } }

        public int status { get { return _status; } set { _status = value; } }
        public int iIdBitacora { get { return _iIdBitacora; } set { _iIdBitacora = value; } }

        public string IdCliente { get { return _IdCliente; } set { _IdCliente  = value; } }

        public string IdFolio { get { return _IdFolio; } set { _IdFolio = value; } }

        public string IdContrato { get { return _IdContrato; } set { _IdContrato = value; } }

        public string FechaIni { get { return _FechaIni; } set { _FechaIni = value; } }

        public string FechaFin { get { return _FechaFin; } set { _FechaFin = value; } }
         
    }
}