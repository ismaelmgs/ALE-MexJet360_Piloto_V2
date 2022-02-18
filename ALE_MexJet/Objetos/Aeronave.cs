using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ALE_MexJet.Objetos
{
    [Serializable, Bindable(BindableSupport.Yes)]
    public class Aeronave : BaseObjeto
    {
        private int _iId = -1;
        private string _sSerie = string.Empty;
        private int _iIdMarca = -1;
        private int _iIdModelo = -1;
        private int _iIdFlota = -1;
        private int _iIdTipo = -1;
        private string _sMatricula = string.Empty;
        private int? _iIdAeropuertoBase = -1;
        private int _iAñoFabricacion = -1;
        private int _iCapacidadPasajero = -1;
        private string _sIdMAtriculaInfo = string.Empty;
        private string _sMAtriculaInfo = string.Empty;
        private string _sIdBaseInfo = string.Empty;
        private string _sBaseInfo = string.Empty;
        private string _sIdUnidadNegocio = string.Empty;
        private string _sUnidadNegocio = string.Empty;
        private int _bReporteSENEAM = 1;
        private DateTime _dtFechaInicio = new DateTime();
        private DateTime _dtFechaFin = new DateTime();
        private int _iStatus = 1;
        private string _sUsuarioCreacion = string.Empty;
        private DateTime _dtFechaCreacion = new DateTime();
        private string _sUsuarioMod = string.Empty;
        private DateTime _dtFechaMod = new DateTime();
        private string _sIP = string.Empty;
        private string _sFechaUltMov = string.Empty;

       
        

        [Display(AutoGenerateField = false), ScaffoldColumn(false)]
        public int iId { get { return _iId; } set { _iId = value; } }

        public string sSerie { get { return _sSerie; } set { _sSerie = value; } }
        public int iIdMarca { get { return _iIdMarca; } set { _iIdMarca = value; } }
        public int iIdModelo { get { return _iIdModelo; } set { _iIdModelo = value; } }
        public int iIFlota { get { return _iIdFlota; } set { _iIdFlota = value; } }
        public int iIdTipo { get { return _iIdTipo; } set { _iIdTipo = value; } }

        public string sMatricula { get { return _sMatricula; } set { _sMatricula = value; } }
        public int? iIdAeropuertoBase { get { return _iIdAeropuertoBase; } set { _iIdAeropuertoBase = value; } }
        public int iAñoFabricacion { get { return _iAñoFabricacion; } set { _iAñoFabricacion = value; } }
        public int iCapacidadPasajero { get { return _iCapacidadPasajero; } set { _iCapacidadPasajero = value; } }
        public string sIdMAtriculaInfo { get { return _sIdMAtriculaInfo; } set { _sIdMAtriculaInfo = value; } }
        public string sMAtriculaInfo { get { return _sMAtriculaInfo; } set { _sMAtriculaInfo = value; } }
        public string sIdBaseInfo { get { return _sIdBaseInfo; } set { _sIdBaseInfo = value; } }
        public string sBaseInfo { get { return _sBaseInfo; } set { _sBaseInfo = value; } }
        public string sIdUnidadNegocio { get { return _sIdUnidadNegocio; } set { _sIdUnidadNegocio = value; } }
        public string sUnidadNegocio { get { return _sUnidadNegocio; } set { _sUnidadNegocio = value; } }
        public DateTime dtFechaInicio { get { return _dtFechaInicio; } set { _dtFechaInicio = value; } }
        public DateTime dtFechaFin { get { return _dtFechaFin; } set { _dtFechaFin = value; } }

        public int bReporteSENEAM { get { return _bReporteSENEAM; } set { _bReporteSENEAM = value; } }

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