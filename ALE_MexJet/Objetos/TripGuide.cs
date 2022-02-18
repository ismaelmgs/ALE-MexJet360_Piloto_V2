using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ALE_MexJet.Objetos
{
    [Serializable, Bindable(BindableSupport.Yes)]
    public class TripGuide : BaseObjeto
    {
        private Int64 _iIdTripGuide = -1;

        private int _iIdSolicitud = -1;

        private int _iIdTrip = -1;

        private int _iIdPierna = -1;

        private string _sNombreArchivoPDF = string.Empty;

        private byte[] _bPDF = null;

        private string _sUsuarioCreacion = string.Empty;

        private string _sUsuarioModificacion = string.Empty;

        private string _sIP = string.Empty;

        private string _sObservaciones;

        private string _sNombreContacto;

        private string _sICAOOrigen;

        private string _sNombreAeropuertoOrigen;

        private string _sICAODestino;

        private string _sNombreAeropuertoDestino;

        private DateTime _dtFechaSalida;

        private int _iNumeroPasajero;

        private string _sAeronave;

        private string _sPilotoTelefono;

        private string _sCoPiloto;

        private string _sCoPilotoTelefono;

        public string sICAOOrigen
        {
            get { return _sICAOOrigen; }
            set { _sICAOOrigen = value; }
        }
        
        public string sNombreAeropuertoOrigen
        {
            get { return _sNombreAeropuertoOrigen; }
            set { _sNombreAeropuertoOrigen = value; }
        }
        public string sICAODestino
        {
            get { return _sICAODestino; }
            set { _sICAODestino = value; }
        }
        
        public string sNombreAeropuertoDestino
        {
            get { return _sNombreAeropuertoDestino; }
            set { _sNombreAeropuertoDestino = value; }
        }
        
        public DateTime dtFechaSalida
        {
            get { return _dtFechaSalida; }
            set { _dtFechaSalida = value; }
        }
        
        public int iNumeroPasajero
        {
            get { return _iNumeroPasajero; }
            set { _iNumeroPasajero = value; }
        }
        
        public string sAeronave
        {
            get { return _sAeronave; }
            set { _sAeronave = value; }
        }
        private string _sPiloto;

        public string sPiloto
        {
            get { return _sPiloto; }
            set { _sPiloto = value; }
        }
        
        public string sPilotoTelefono
        {
            get { return _sPilotoTelefono; }
            set { _sPilotoTelefono = value; }
        }
        
        public string sCoPiloto
        {
            get { return _sCoPiloto; }
            set { _sCoPiloto = value; }
        }
       
        public string sCoPilotoTelefono
        {
            get { return _sCoPilotoTelefono; }
            set { _sCoPilotoTelefono = value; }
        }

        public int iIdSolicitud
        {
            get { return _iIdSolicitud; }
            set { _iIdSolicitud = value; }
        }

        public int iIdTrip
        {
            get { return _iIdTrip; }
            set { _iIdTrip = value; }
        }

        public int iIdPierna
        {
            get { return _iIdPierna; }
            set { _iIdPierna = value; }
        }

        public string sNombreArchivoPDF
        {
            get { return _sNombreArchivoPDF; }
            set { _sNombreArchivoPDF = value; }
        }

        public byte[] bPDF
        {
            get { return _bPDF; }
            set { _bPDF = value; }
        }

        public string sUsuarioCreacion
        {
            get { return _sUsuarioCreacion; }
            set { _sUsuarioCreacion = value; }
        }

        public string sUsuarioModificacion
        {
            get { return _sUsuarioModificacion; }
            set { _sUsuarioModificacion = value; }
        }

        public string sIP
        {
            get { return _sIP; }
            set { _sIP = value; }
        }

        public Int64 iIdTripGuide
        {
            get { return _iIdTripGuide; }
            set { _iIdTripGuide = value; }
        }

        public string sObservaciones
        {
            get { return _sObservaciones; }
            set { _sObservaciones = value; }
        }

        public string sNombreContacto
        {
            get { return _sNombreContacto; }
            set { _sNombreContacto = value; }
        }


    }
}