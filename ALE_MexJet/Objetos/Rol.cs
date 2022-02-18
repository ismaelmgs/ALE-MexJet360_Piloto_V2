using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ALE_MexJet.Objetos
{
    [Serializable, Bindable(BindableSupport.Yes)]
    public class Rol : BaseObjeto
    {
        private int _iId = -1;
        public string _sDescripcion = string.Empty;
        private int _iIdModuloDefault = 0;
        private int _iIdRolO = 0;
        private int _iIdRolD = 0;
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
        [Display(Name = "El Rol es requerida", AutoGenerateField = true)]
        public virtual string sDescripcion { get { return _sDescripcion; } set { _sDescripcion = value; } }

        public int iIdModuloDefault { get { return _iIdModuloDefault; } set { _iIdModuloDefault = value; } }

        public int iIdRolO { get { return _iIdRolO; } set { _iIdRolO = value; } }
        public int iIdRolD { get { return _iIdRolD; } set { _iIdRolD = value; } }

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