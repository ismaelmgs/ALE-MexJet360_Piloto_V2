using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ALE_MexJet.Objetos
{
    [Serializable, Bindable(BindableSupport.Yes)]
    public class TUA
    {

        private int _iIdTUA = -1;
        private int _iIdAeropuerto = 0;
        private int _iIdMes = 0;
        private int _iAnio = 0;
        private decimal _dNacional = 0.0M;
        private decimal _dInternacional = 0.0M;
        private int _iStatus = 1;
        private string _sUsuarioCreacion = string.Empty;
        private DateTime _dtFechaCreacion = new DateTime();
        private string _sUsuarioMod = string.Empty;
        private DateTime _dtFechaMod = new DateTime();
        private string _sIP = string.Empty;


        public int iIdTUA { get { return _iIdTUA; } set { _iIdTUA = value; } }
        public int iIdAeropuerto { get { return _iIdAeropuerto; } set { _iIdAeropuerto = value; } }
        public int iIdMes { get { return _iIdMes; } set { _iIdMes = value; } }
        public int iAnio { get { return _iAnio; } set { _iAnio = value; } }
        public decimal dNacional { get { return _dNacional; } set { _dNacional = value; } }
        public decimal dInternacional { get { return _dInternacional; } set { _dInternacional = value; } }
        
        [Display(Name = "¿Activo? "), Required]
        public int iStatus { get { return _iStatus; } set { _iStatus = value; } }

        public string sUsuarioCreacion { get { return _sUsuarioCreacion; } set { _sUsuarioCreacion = value; } }

        public DateTime dtFechaCreacion { get { return _dtFechaCreacion; } set { _dtFechaCreacion = value; } }

        public string sUsuarioMod { get { return _sUsuarioMod; } set { _sUsuarioMod = value; } }

        public DateTime dtFechaMod { get { return _dtFechaMod; } set { _dtFechaMod = value; } }

        public string sIP { get { return _sIP; } set { _sIP = value; } }

    }
}