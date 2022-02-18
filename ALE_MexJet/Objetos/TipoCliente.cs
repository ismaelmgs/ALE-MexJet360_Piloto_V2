using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace ALE_MexJet.Objetos
{
    public class TipoCliente
    {
        private int _iId = -1;
        private string _sTipoClienteDescripcion = string.Empty;
        private string _sCodigoDeUnidad4 = string.Empty;
        private string _sCodigoDeUnidad4Descripcion = string.Empty;
        private int _iHrsPernocta = -1;
        private int _iStatus = 1;
        private string _sUsuarioCreacion = string.Empty;
        private DateTime _dtFechaCreacion = new DateTime();
        private string _sUsuarioMod = string.Empty;
        private DateTime _dtFechaMod = new DateTime();
        private string _sIP = string.Empty;
        private string _sFechaUltMov = string.Empty;

        [Display(AutoGenerateField = false), ScaffoldColumn(false)]
        public int iId { get { return _iId; } set { _iId = value; } }

        public string sTipoClienteDescripcion { get { return _sTipoClienteDescripcion; } set { _sTipoClienteDescripcion = value; } }
        public string sCodigoDeUnidad4 { get { return _sCodigoDeUnidad4; } set { _sCodigoDeUnidad4 = value; } }

        public string sCodigoDeUnidad4Descripcion { get { return _sCodigoDeUnidad4Descripcion; } set { _sCodigoDeUnidad4Descripcion = value; } }
        public int iHrsPernocta { get { return _iHrsPernocta; } set { _iHrsPernocta = value; } }

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