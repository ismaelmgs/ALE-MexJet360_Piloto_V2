using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ALE_MexJet.Objetos
{
    [Serializable, Bindable(BindableSupport.Yes)]
    public class PersonaDifusion : BaseObjeto
    {
        private int _iId = -1;
        public string _sNombre = string.Empty;
        private string _sApellidoPaterno = string.Empty;
        private string _sApellidoMaterno = string.Empty;
        private string _sTelefonoMovil = string.Empty;
        private string _sCorreoElectronico = string.Empty;
        private int _iIdTitulo = -1;
        private int _iIdTipoPersona = -1;
        private int _iIdTipoContacto = -1;
        private int _iCorreos = -1;
        private int _iSMS = -1;
        private int _iPublicidad = -1;
        private int _iLLamadas = -1;
        private int _iStatus = 1;
        private string _sUsuarioCreacion = string.Empty;
        private DateTime _dtFechaCreacion = new DateTime();
        private string _sUsuarioMod = string.Empty;
        private DateTime _dtFechaMod = new DateTime();
        private string _sIP = string.Empty;

        [Display(AutoGenerateField = false), ScaffoldColumn(false)]
        public int iId { get { return _iId; } set { _iId = value; } }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Nombre es obligatorio")]
        [Display(Name = "El Nombre es requerido", AutoGenerateField = true)]
        public virtual string sNombre { get { return _sNombre; } set { _sNombre = value; } }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Apellido Paterno es obligatorio")]
        [Display(Name = "El Apellido es requerido", AutoGenerateField = true)]
        public virtual string sApellidoPaterno { get { return _sApellidoPaterno; } set { _sApellidoPaterno = value; } }

        public string sApellidoMaterno { get { return _sApellidoMaterno; } set { _sApellidoMaterno = value; } }

        public string sTelefonoMovil { get { return _sTelefonoMovil; } set { _sTelefonoMovil = value; } }

        public string sCorreoElectronico { get { return _sCorreoElectronico; } set { _sCorreoElectronico = value; } }

        public int iIdTitulo { get { return _iIdTitulo; } set { _iIdTitulo = value; } }

        public int iIdTipoPersona { get { return _iIdTipoPersona; } set { _iIdTipoPersona = value; } }

        public int iIdTipoContacto { get { return _iIdTipoContacto; } set { _iIdTipoContacto = value; } }

        public int iCorreos { get { return _iCorreos; } set { _iCorreos = value; } }

        public int iSMS { get { return _iSMS; } set { _iSMS = value; } }

        public int iPublicidad { get { return _iPublicidad; } set { _iPublicidad = value; } }

        public int iLLamadas { get { return _iLLamadas; } set { _iLLamadas = value; } }

        [Display(Name = "¿Activo? "), Required]
        public int iStatus { get { return _iStatus; } set { _iStatus = value; } }

        public string sUsuarioCreacion { get { return _sUsuarioCreacion; } set { _sUsuarioCreacion = value; } }

        public DateTime dtFechaCreacion { get { return _dtFechaCreacion; } set { _dtFechaCreacion = value; } }

        public string sUsuarioMod { get { return _sUsuarioMod; } set { _sUsuarioMod = value; } }

        public DateTime dtFechaMod { get { return _dtFechaMod; } set { _dtFechaMod = value; } }

        public string sIP { get { return _sIP; } set { _sIP = value; } }
    }
}