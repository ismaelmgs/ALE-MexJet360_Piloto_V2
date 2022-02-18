using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ALE_MexJet.Objetos
{
    [Serializable, Bindable(BindableSupport.Yes)]
    public class ServicioConCargo  : BaseObjeto
    {
        private int _iId = -1;
        private string _sDescripcion = string.Empty;
        private string _sCveArticulo = string.Empty;
        private string _sArticuloDescripcion = string.Empty;
        //private string _sCveCuenta = string.Empty;
        //private string _sDescripcionCuenta = string.Empty;
        private string _sCveCodUnitUno = string.Empty;
        private string _sDescripcionCodUnitUno = string.Empty;
        private decimal _dImporte = 0m;
        private bool _bPasajero = false;
        private bool _bPierna = false;
        private int _iStatus = 1;
        private string _sUsuarioCreacion = string.Empty;
        private DateTime _dtFechaCreacion = new DateTime();
        private string _sUsuarioMod = string.Empty;
        private DateTime _dtFechaMod = new DateTime();
        private string _sIP = string.Empty;
        private string _sFechaUltMov = string.Empty;
       

        [Display(AutoGenerateField = false), ScaffoldColumn(false)]
        public int iId { get { return _iId; } set { _iId = value; } }

        public decimal dImporte { get { return _dImporte; } set { _dImporte = value; } }

        public string sDescripcion { get { return _sDescripcion; } set { _sDescripcion = value; } }

        //public string sCveCuenta { get { return _sCveCuenta; } set { _sCveCuenta = value; } }
        //public string sDescripcionCuenta { get { return _sDescripcionCuenta; } set { _sDescripcionCuenta = value; } }
        public string sCveCodUnitUno { get { return _sCveCodUnitUno; } set { _sCveCodUnitUno = value; } }
        public string sDescripcionCodUnitUno { get { return _sDescripcionCodUnitUno; } set { _sDescripcionCodUnitUno = value; } }

        public string sCveArticulo { get { return _sCveArticulo; } set { _sCveArticulo = value; } }
        public string sArticuloDescripcion { get { return _sArticuloDescripcion; } set { _sArticuloDescripcion = value; } }

        public bool bPierna { get { return _bPierna; } set { _bPierna = value; } }
        public bool bPasajero { get { return _bPasajero; } set { _bPasajero = value; } }

        [Display(Name = "¿Activo? "), Required]
        public int iStatus { get { return _iStatus; } set { _iStatus = value; } }

        public string sUsuarioCreacion { get { return _sUsuarioCreacion; } set { _sUsuarioCreacion = value; } }

        public DateTime dtFechaCreacion { get { return _dtFechaCreacion; } set { _dtFechaCreacion = value; } }

        public string sUsuarioMod { get { return _sUsuarioMod; } set { _sUsuarioMod = value; } }

        public DateTime dtFechaMod { get { return _dtFechaMod; } set { _dtFechaMod = value; } }

        public string sIP { get { return _sIP; } set { _sIP = value; } }

        [Display(Name = "Fecha DT"), Required]
        public string sFechaUltMov { get { return _sFechaUltMov; } set { _sFechaUltMov = value; } }
    }
}

        