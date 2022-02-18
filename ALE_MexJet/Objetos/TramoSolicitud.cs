using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ALE_MexJet.Objetos
{
    [Serializable, Bindable(BindableSupport.Yes)]
    public class TramoSolicitud
    {
        public int _iIdTramo = -1;
        public int _iIdSolicitud = -1;
        public int _iIdAeropuertoO = -1;
        public int _iIdAeropuertoD = -1;
        public string _dFechaVuelo = string.Empty;
        public string _sHoraVuelo = string.Empty;
        public int _iNoPax = -1;
        public string _sTransportacion = string.Empty;
        public int _iStatus=1;
        private string _sUsuarioCreacion = string.Empty;
        private DateTime _dtFechaCreacion = new DateTime();
        private string _sUsuarioMod = string.Empty;
        private DateTime _dtFechaMod = new DateTime();
        private string _sIP = string.Empty;
        private string _sNombrePax = string.Empty;
        private string _ComisariatoDesc = string.Empty;
        private decimal _PrecioCotizado = 0;
        private int _iIdComisariato = -1;
        private int _iIdProveedor = -1;

        public int iIdTramo { get { return _iIdTramo; } set { _iIdTramo = value; } }
        public int iIdSolicitud { get { return _iIdSolicitud; } set { _iIdSolicitud = value; } }
        public int iIdAeropuertoO { get { return _iIdAeropuertoO; } set { _iIdAeropuertoO = value; } }
        public int iIdAeropuertoD { get { return _iIdAeropuertoD; } set { _iIdAeropuertoD = value; } }
        public string dFechaVuelo { get { return _dFechaVuelo; } set { _dFechaVuelo = value; } }
        public string sHoraVuelo { get { return _sHoraVuelo; } set { _sHoraVuelo = value; } }
        public int iNoPax { get { return _iNoPax; } set { _iNoPax = value; } }
        public string sTransportacion { get { return _sTransportacion; } set { _sTransportacion = value; } }
        public int iStatus { get { return _iStatus; } set { _iStatus = value; } }
        public string sUsuarioCreacion { get { return _sUsuarioCreacion; } set { _sUsuarioCreacion = value; } }
        public DateTime dtFechaCreacion { get { return _dtFechaCreacion; } set { _dtFechaCreacion = value; } }
        public string sUsuarioMod { get { return _sUsuarioMod; } set { _sUsuarioMod = value; } }
        public DateTime dtFechaMod { get { return _dtFechaMod; } set { _dtFechaMod = value; } }
        public string sIP { get { return _sIP; } set { _sIP = value; } }
        public string sNombrePax { get { return _sNombrePax; } set { _sNombrePax = value; } }
        public string sComisariatoDesc { get { return _ComisariatoDesc; } set { _ComisariatoDesc = value; } }
        public decimal dPrecioCotizado { get { return _PrecioCotizado; } set { _PrecioCotizado = value; } }
        public int iIdComisariato { get { return _iIdComisariato; } set { _iIdComisariato = value; } }
        public int iIdProveedor { get { return _iIdProveedor; } set { _iIdProveedor = value; } }
 
    }
}