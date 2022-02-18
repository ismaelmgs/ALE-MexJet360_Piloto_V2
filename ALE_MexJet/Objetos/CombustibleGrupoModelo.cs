using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ALE_MexJet.Objetos
{
    [Serializable, Bindable(BindableSupport.Yes)]
    public class CombustibleGrupoModelo : BaseObjeto
    {
        private int _iIdCombustible = 0;
        private int _iIdGrupoModelo = 0;
        private int _iIdTipoGrupo = 0;
        private int _iIdTipoContrato = 0;
        private int _iStatus = 1;
        private string _sUsuarioCreacion = string.Empty;
        private DateTime _dtFechaCreacion = new DateTime();
        private string _sUsuarioMod = string.Empty;
        private DateTime _dtFechaMod = new DateTime();
        private string _sIP = string.Empty;

        [Display(AutoGenerateField = false), ScaffoldColumn(false)]
        public int iIdCombustible { get { return _iIdCombustible; } set { _iIdCombustible = value; } }
        public int iIdGrupoModelo { get { return _iIdGrupoModelo; } set { _iIdGrupoModelo = value; } }
        public int iIdTipoGrupo { get { return _iIdTipoGrupo; } set { _iIdTipoGrupo = value; } }
        public int iIdTipoContrato { get { return _iIdTipoContrato; } set { _iIdTipoContrato = value; } }


        [Display(Name = "¿Activo? "), Required]
        public int iStatus { get { return _iStatus; } set { _iStatus = value; } }

        public string sUsuarioCreacion { get { return _sUsuarioCreacion; } set { _sUsuarioCreacion = value; } }

        public DateTime dtFechaCreacion { get { return _dtFechaCreacion; } set { _dtFechaCreacion = value; } }

        public string sUsuarioMod { get { return _sUsuarioMod; } set { _sUsuarioMod = value; } }

        public DateTime dtFechaMod { get { return _dtFechaMod; } set { _dtFechaMod = value; } }

        public string sIP { get { return _sIP; } set { _sIP = value; } }

        public string s { get; set; }
    }
}