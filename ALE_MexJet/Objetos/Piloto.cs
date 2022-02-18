using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace ALE_MexJet.Objetos
{
    [Serializable, Bindable(BindableSupport.Yes)]
    public class Piloto :BaseObjeto
    {
        private int _iIdPiloto = -1;
        private string _sCrewCode = string.Empty;
        private string _sPilotoNombre = string.Empty;
        private string _sPilotoApPaterno = string.Empty;
        private string _sPilotoApMaterno = string.Empty;

        private int _iStatus = 1;
        private string _sUsuarioCreacion = string.Empty;
        private DateTime _dtFechaCreacion = new DateTime();
        private string _sUsuarioMod = string.Empty;
        private DateTime _dtFechaMod = new DateTime();
        private string _sIP = string.Empty;

        private string _sFechaUltMov = string.Empty;

        [Display(AutoGenerateField = false), ScaffoldColumn(false)]
        public int iIdPiloto { get { return _iIdPiloto; } set { _iIdPiloto = value; } }
        
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo CrewCode es obligatorio")]
        [Range( 0, int.MaxValue)]
        public string sCrewCode { get { return _sCrewCode; } set { _sCrewCode = value; } }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Nombre es obligatorio" )]
        [Display(Name = "nombre", AutoGenerateField = true)]
        //[MaxLength(50,ErrorMessage = "El campo no debe tener más de 50 caracteres")]
        public string sPilotoNombre { get { return _sPilotoNombre; } set { _sPilotoNombre = value; } }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Apellido Paterno es obligatorio")]
        [Display(Name = "Apellido Paterno", AutoGenerateField = true)]
        //[MaxLength(50, ErrorMessage = "El campo no debe tener más de 50 caracteres")]
        public string sPilotoApPaterno { get { return _sPilotoApPaterno; } set { _sPilotoApPaterno = value; } }

        
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Apellido Paterno es obligatorio")]
        [Display(Name = "Apellido Materno", AutoGenerateField = true)]
        //[MaxLength(50, ErrorMessage = "El campo no debe tener más de 50 caracteres")]
        public string sPilotoApMaterno { get { return _sPilotoApMaterno; } set { _sPilotoApMaterno = value; } }

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