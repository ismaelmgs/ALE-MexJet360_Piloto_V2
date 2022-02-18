using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ALE_MexJet.Objetos
{
    public class Modelo : BaseObjeto
    {
        private int _iId = -1;
        private int _iMarca = -1;
        private int _iGrupoModelo = -1;
        private int _iTipo = -1;
        private decimal _dVelocidad = 0m;
        private int _iGrupoTamaño = -1;
        private int _iHorasAño =-1;
        private decimal _dPesoMaximo = 0m;
        private int _iDesignador = -1;
        public string _sDescripcion = string.Empty;
        private int _iStatus = 1;
        private string _sUsuarioCreacion = string.Empty;
        private DateTime _dtFechaCreacion = new DateTime();
        private string _sUsuarioMod = string.Empty;
        private DateTime _dtFechaMod = new DateTime();
        private string _sIP = string.Empty;
        private string _sFechaUltMov = string.Empty;

        [Display(AutoGenerateField = false), ScaffoldColumn(false)]
        public int iId { get { return _iId; } set { _iId = value; } }
        public int iMarca { get { return _iMarca; } set { _iMarca = value; } }
        public int iGrupoModelo { get { return _iGrupoModelo; } set { _iGrupoModelo = value; } }
        public int iTipo { get { return _iTipo; } set { _iTipo = value; } }
        public decimal dVelocidad { get { return _dVelocidad; } set { _dVelocidad = value; } }
        public int iGrupoTamaño { get { return _iGrupoTamaño; } set { _iGrupoTamaño = value; } }
        public int iHorasAño { get { return _iHorasAño; } set { _iHorasAño = value; } }
        public decimal dPesoMaximo { get { return _dPesoMaximo; } set { _dPesoMaximo = value; } }
        public int iDesignador { get { return _iDesignador; } set { _iDesignador = value; } }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Marca es obligatorio")]
        [Display(Name = "El Rol es requerida", AutoGenerateField = true)]
        public virtual string sDescripcion { get { return _sDescripcion; } set { _sDescripcion = value; } }

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