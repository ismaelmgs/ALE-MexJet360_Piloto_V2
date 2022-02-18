using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ALE_MexJet.Objetos
{
    [Serializable, Bindable(BindableSupport.Yes)]
    public class RangoCombustible : BaseObjeto
    {
        private int _iIdRangoIden = 0;
        private int _iIdCombustible = 0;
        private int _iIdRango = 0;
        public decimal _dDesde = 0.0M;
        public decimal _dHasta = 0.0M;
        public decimal _dAumento = 0.0M;
        private int _iStatus = 1;
        private string _sUsuarioCreacion = string.Empty;
        private DateTime _dtFechaCreacion = new DateTime();
        private string _sUsuarioMod = string.Empty;
        private DateTime _dtFechaMod = new DateTime();
        private string _sIP = string.Empty;

        [Display(AutoGenerateField = false), ScaffoldColumn(false)]
        public int iIdRangoIden { get { return _iIdRangoIden; } set { _iIdRangoIden = value; } }
        public int iIdCombustible { get { return _iIdCombustible; } set { _iIdCombustible = value; } }
        public int iIdRango { get { return _iIdRango; } set { _iIdRango = value; } }
        public decimal dDesde { get { return _dDesde; } set { _dDesde = value; } }
        public decimal dHasta { get { return _dHasta; } set { _dHasta = value; } }
        public decimal dAumento { get { return _dAumento; } set { _dAumento = value; } }


        [Display(Name = "¿Activo? "), Required]
        public int iStatus { get { return _iStatus; } set { _iStatus = value; } }

        public string sUsuarioCreacion { get { return _sUsuarioCreacion; } set { _sUsuarioCreacion = value; } }

        public DateTime dtFechaCreacion { get { return _dtFechaCreacion; } set { _dtFechaCreacion = value; } }

        public string sUsuarioMod { get { return _sUsuarioMod; } set { _sUsuarioMod = value; } }

        public DateTime dtFechaMod { get { return _dtFechaMod; } set { _dtFechaMod = value; } }

        public string sIP { get { return _sIP; } set { _sIP = value; } }

    }
}