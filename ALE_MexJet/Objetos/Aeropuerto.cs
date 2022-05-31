using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ALE_MexJet.Objetos
{
    [Serializable, Bindable(BindableSupport.Yes)]
    public class Aeropuerto : BaseObjeto
    {
        private int _iId = -1;
        private string _sIATA = string.Empty;
        private string _sICAO = string.Empty;
        private string _sDescripcion = string.Empty;
        private int _iTipoAeropuerto = 0;
        private int _iIdPais = 0;
        private int _iIdEstado = 0;
        private string _sCiudad = string.Empty;
        private bool _bBase = false;
        private string _TipoDestino = string.Empty;
        private bool _bAeropuertoHelipuerto = false;
        private decimal _dAeropuertoHelipuertoTarifa = 0m;

        private bool _bCobraAterrizaje = false;
        private decimal _dAterrizajeNal = 0m;
        private decimal _dAterrizajeInt = 0m;
        private int _iStatus = 1;
        private string _sUsuarioCreacion = string.Empty;
        private DateTime _dtFechaCreacion = new DateTime();
        private string _sUsuarioMod = string.Empty;
        private DateTime _dtFechaMod = new DateTime();
        private string _sIP = string.Empty;


        private string _sFechaUltMov = string.Empty;

        [Display(AutoGenerateField = false), ScaffoldColumn(false)]
        public int iId { get { return _iId; } set { _iId = value; } }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Marca es obligatorio")]
        [Display(Name = "El comisariato es requerido", AutoGenerateField = true)]
        public virtual string sDescripcion { get { return _sDescripcion; } set { _sDescripcion = value; } }


        public string sIATA { get { return _sIATA; } set { _sIATA = value; } }
        public string sICAO { get { return _sICAO; } set { _sICAO = value; } }
        public int iTipoAeropuerto { get { return _iTipoAeropuerto; } set { _iTipoAeropuerto = value; } }
        public int iIdPais { get { return _iIdPais; } set { _iIdPais = value; } }
        public int iIdEstado { get { return _iIdEstado; } set { _iIdEstado = value; } }
        public string sCiudad { get { return _sCiudad; } set { _sCiudad = value; } }
        public bool bBase { get { return _bBase; } set { _bBase = value; } }
        public string TipoDestino { get { return _TipoDestino; } set { _TipoDestino = value; } }
        public bool bAeropuertoHelipuerto { get { return _bAeropuertoHelipuerto; } set { _bAeropuertoHelipuerto = value; } }
        public decimal dAeropuertoHelipuertoTarifa { get { return _dAeropuertoHelipuertoTarifa; } set { _dAeropuertoHelipuertoTarifa = value; } }
        public bool bCobraAterrizaje { get { return _bCobraAterrizaje; } set { _bCobraAterrizaje = value; } }
        public decimal dAterrizajeNal { get => _dAterrizajeNal; set => _dAterrizajeNal = value; }
        public decimal dAterrizajeInt { get => _dAterrizajeInt; set => _dAterrizajeInt = value; }


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