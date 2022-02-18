using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ALE_MexJet.Objetos
{
    [Serializable,Bindable(BindableSupport.Yes)]
    public class SolicitudVuelo : BaseObjeto
    {
        private int _iIdSeguimiento = -1;
        private int _iIdSolicitud = -1;
        private int _iIdContrato = -1;
        private int _iIdContacto = -1;
        private int _iIdMotivo = -1;
        private int _iIdOrigen = -1;
        private int _iIdEquipo = -1;
        private string _sNotasVuelo = string.Empty;
        private string _sItinerariov = string.Empty;
        private int _iStatus = 1;
        private string _sUsuarioCreacion = string.Empty;
        private DateTime _dtFechaCreacion = new DateTime();
        private string _sUsuarioMod = string.Empty;
        private DateTime _dtFechaMod = new DateTime();
        private string _sIP = string.Empty;

        private string _sNotas = string.Empty;
        private int _iIdAutor = -1;

        private byte[] _bArchivo = null;
        private string _sNombreArchivo = string.Empty;
        private string _sMatricula = string.Empty;
        private string _sNotaSolVuelo = string.Empty;
        private int _iDictamen = -1;

        public int idictamen { get { return _iDictamen; } set { _iDictamen = value; } }
        public int iIdSolicitud { get { return _iIdSolicitud; } set { _iIdSolicitud = value; } }
        public int iIdContrato { get { return _iIdContrato; } set { _iIdContrato = value; } }
        public int iIdContacto { get { return _iIdContacto; } set { _iIdContacto = value; } }
        public int iIdMotivo { get { return _iIdMotivo; } set { _iIdMotivo = value; } }
        public int iIdOrigen { get { return _iIdOrigen; } set { _iIdOrigen = value; } }
        public int iIdEquipo { get { return _iIdEquipo; } set { _iIdEquipo = value; } }
        public string sNotasVuelo { get { return _sNotasVuelo; } set { _sNotasVuelo = value; } }
        public string sItinerariov { get { return _sItinerariov; } set { _sItinerariov = value; } }
        public int iStatus { get { return _iStatus; } set { _iStatus = value; } }
        public string sUsuarioCreacion { get { return _sUsuarioCreacion; } set { _sUsuarioCreacion = value; } }
        public DateTime dtFechaCreacion { get { return _dtFechaCreacion; } set { _dtFechaCreacion = value; } }
        public string sUsuarioMod { get { return _sUsuarioMod; } set { _sUsuarioMod = value; } }
        public DateTime dtFechaMod { get { return _dtFechaMod; } set { _dtFechaMod = value; } }
        public string sIP { get { return _sIP; } set { _sIP = value; } }
        public string sNotas { get { return _sNotas; } set { _sNotas = value; } }
        public int iIdAutor { get { return _iIdAutor; } set { _iIdAutor = value; } }
        public byte[] bARchivo { get { return _bArchivo; } set { _bArchivo = value; } }
        public int iIdSeguimiento { get { return _iIdSeguimiento; } set { _iIdSeguimiento = value; } }
        public string sNombreArchivo { get { return _sNombreArchivo; } set { _sNombreArchivo = value; } }
        public string sMatricula { get { return _sMatricula; } set { _sMatricula = value; } }
        public string sNotaSolVuelo { get { return _sNotaSolVuelo; } set { _sNotaSolVuelo = value; } }
        
    }
}