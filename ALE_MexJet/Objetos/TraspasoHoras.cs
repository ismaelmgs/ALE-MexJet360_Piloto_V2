using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ALE_MexJet.Objetos
{
    [Serializable, Bindable(BindableSupport.Yes)]
    public class TraspasoHoras
    {
		private int _iIdIntercambioHoras;
		private int _iIdClienteOrigen;
        private int _iIdContratoOrigen;
        private int _iIdClienteDestino;
        private int _iIdContratoDestino;
        private string _sHorasIntercambiadas;
        private string _sObservaciones;	
        private int _iStatus = 1;
        private string _sUsuarioCreacion = string.Empty;
        private DateTime _dtFechaCreacion = new DateTime();
        private string _sUsuarioModificacion = string.Empty;
        private DateTime _dtFechaMododificacion = new DateTime();
        private string _sIP = string.Empty;

		public int iIdIntercambioHoras { get { return _iIdIntercambioHoras; } set { _iIdIntercambioHoras = value; } }
		public int iIdClienteOrigen { get { return _iIdClienteOrigen; } set { _iIdClienteOrigen = value; } }
        public int iIdContratoOrigen { get { return _iIdContratoOrigen; } set { _iIdContratoOrigen = value; } }
        public int iIdClienteDestino { get { return _iIdClienteDestino; } set { _iIdClienteDestino = value; } }
        public int iIdContratoDestino { get { return _iIdContratoDestino; } set { _iIdContratoDestino = value; } }
        public string sHorasIntercambiadas { get { return _sHorasIntercambiadas; } set { _sHorasIntercambiadas = value; } }
        public string sObservaciones { get { return _sObservaciones; } set { _sObservaciones = value; } }
        public int iStatus { get { return _iStatus;} set {_iStatus=value; } }
        public string sUsuarioCreacion { get { return _sUsuarioCreacion; } set { _sUsuarioCreacion = value; } }
        public DateTime dtFechaCreacion { get { return _dtFechaCreacion; } set { _dtFechaCreacion = value; } }
        public string sUsuarioModificacion { get { return _sUsuarioModificacion; } set { _sUsuarioModificacion = value; } }
        public DateTime tFechaMododificacion { get { return _dtFechaMododificacion; } set { _dtFechaMododificacion = value; } }
        public string sIP { get { return _sIP; } set { _sIP = value; } }
    }
}