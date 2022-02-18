using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ALE_MexJet.Objetos
{
    [Serializable]
    public class VentaFerrys
    {
        private int _iIdFerry = 0;
        private int _iIdPadre = 0;
        private string _sOrigen = string.Empty;
        private string _sDestino = string.Empty;
        private DateTime _dtFechaInicioReservar = new DateTime();
        //private DateTime _dtFechaLlegada = new DateTime()
        private string _sDuracion = string.Empty;
        private DateTime _dtFechaLimiteReservar = new DateTime();
        //private string _sMatricula = string.Empty;
        private string _sGrupoModelo = string.Empty;
        private decimal _dPrecioLista = 0;
        private int _iPrioridad = 0;
        private int _noPax = 0;
        private List<PreciosTipoCliente> _oLstPrecios = new List<PreciosTipoCliente>();
        //private List<SMSDifusion> _oLsSMS = new List<SMSDifusion>();
        //private List<MailDifusion> _oLsMail = new List<MailDifusion>();

        public int iIdFerry { get { return _iIdFerry; } set { _iIdFerry = value; } }

        public int iIdPadre { get { return _iIdPadre; } set { _iIdPadre = value; } }

        public string sOrigen { get { return _sOrigen; } set { _sOrigen = value; } }
        
        public string sDestino { get { return _sDestino; } set { _sDestino = value; } }
        
        public DateTime dtFechaInicioReservar { get { return _dtFechaInicioReservar; } set { _dtFechaInicioReservar = value; } }
        
        //  public DateTime dtFechaLlegada { get { return _dtFechaLlegada; } set { _dtFechaLlegada = value; } }
        
        public string sDuracion { get { return _sDuracion; } set { _sDuracion = value; } }

        public DateTime dtFechaLimiteReservar { get { return _dtFechaLimiteReservar; } set { _dtFechaLimiteReservar = value; } }

        //public string sMatricula { get { return _sMatricula; } set { _sMatricula = value; } }

        public string sGrupoModelo { get { return _sGrupoModelo; } set { _sGrupoModelo = value; } }
        
        public decimal dPrecioLista { get { return _dPrecioLista; } set { _dPrecioLista = value; } }

        public int iPrioridad { get { return _iPrioridad; } set { _iPrioridad = value; } }

        public int noPax { get { return _noPax; } set { _noPax = value; } }

        public List<PreciosTipoCliente> oLstPrecios { get { return _oLstPrecios; } set { _oLstPrecios = value; } }
        
        //public List<SMSDifusion> oLsSMS { get { return _oLsSMS; } set { _oLsSMS = value; } }
        
        //public List<MailDifusion> oLsMail { get { return _oLsMail; } set { _oLsMail = value; } }
    }

    [Serializable]
    public class SMSDifusion
    {
        private int _iIdSMS = 0;
        private string _sNombre = string.Empty;
        private string _sNumero = string.Empty;

        public int iIdSMS { get { return _iIdSMS; } set { _iIdSMS = value; } }
        
        public string sNombre { get { return _sNombre; } set { _sNombre = value; } }
        
        public string sNumero { get { return _sNumero; } set { _sNumero = value; } }
    }

    [Serializable]
    public class MailDifusion
    {
        private int _iIdMail = 0;
        private string _sNombre = string.Empty;
        private string _sMail = string.Empty;

        public int iIdMail { get { return _iIdMail; } set { _iIdMail = value; } }
        
        public string sNombre { get { return _sNombre; } set { _sNombre = value; } }
        
        public string sMail { get { return _sMail; } set { _sMail = value; } }
    }

    [Serializable]
    public class PreciosTipoCliente
    {
        private string _sTipoCliente = string.Empty;
        private decimal _dDescuento = 0;
        private decimal _dImporteFinal = 0;

        public string sTipoCliente { get { return _sTipoCliente; } set { _sTipoCliente = value; } }
        
        public decimal dDescuento { get { return _dDescuento; } set { _dDescuento = value; } }
        
        public decimal dImporteFinal { get { return _dImporteFinal; } set { _dImporteFinal = value; } }
    }

    [Serializable]
    public class ResultPubFerry
    {
        private string _message = string.Empty;
        private int _idVagap = 0;
        private int _np = 0;

        public string message { get { return _message; } set { _message = value; } }
        public int idVagap { get { return _idVagap; } set { _idVagap = value; } }
        public int np { get { return _np; } set { _np = value; } }
    }

    [Serializable]
    public class EstatusFerry
    {
        private int _iIdFerry = 0;
        private int _status = 0;

        public int iIdFerry { get { return _iIdFerry; } set { _iIdFerry = value; } }
        public int status { get { return _status; } set { _status = value; } }
    }

    [Serializable]
    public class InformacionFerry
    {
        private int _iIdFerry = 0;
        private DateTime _dtInicio = new DateTime();
        private DateTime _dtFin = new DateTime();
        private int _iNoPax = 0;


        public int iIdFerry { get { return _iIdFerry; } set { _iIdFerry = value; } }
        public DateTime dtInicio { get { return _dtInicio; } set { _dtInicio = value; } }
        public DateTime dtFin { get { return _dtFin; } set { _dtFin = value; } }
        public int iNoPax { get { return _iNoPax; } set { _iNoPax = value; } }
    }
}