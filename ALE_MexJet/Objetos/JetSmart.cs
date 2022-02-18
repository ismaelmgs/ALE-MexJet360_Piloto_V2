using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ALE_MexJet.Objetos
{
    [Serializable]
    public class OfertaFerry : BaseObjeto
    {
        private int _iIdFerry = 0;
        private int _iTrip = 0;
        private string _sOrigen = string.Empty;
        private string _sDestino = string.Empty;
        private DateTime _dtFechaInicio = new DateTime();
        private DateTime _dtFechaFin = new DateTime();
        private string _sTiempoVuelo = string.Empty;
        private int _iIdGrupoModelo = 0;
        private string _sGrupoModelo = string.Empty;
        private string _sMatricula = string.Empty;
        private string _sReferencia = string.Empty;
        private int _iIdPadre = 0;
        private decimal _dCostoVuelo = 0;
        private decimal _dIvaVuelo = 0;
        private int _iIdPendiente = 0;
        private int _iDiferencia = 0;
        private int _iNoPax = 0;
        

        public int iIdFerry { get { return _iIdFerry; } set { _iIdFerry = value; } }
        public int iTrip { get { return _iTrip; } set { _iTrip = value; } }
        public string sOrigen { get { return _sOrigen; } set { _sOrigen = value; } }
        public string sDestino { get { return _sDestino; } set { _sDestino = value; } }
        public DateTime dtFechaInicio { get { return _dtFechaInicio; } set { _dtFechaInicio = value; } }
        public DateTime dtFechaFin { get { return _dtFechaFin; } set { _dtFechaFin = value; } }
        public string sTiempoVuelo { get { return _sTiempoVuelo; } set { _sTiempoVuelo = value; } }
        public int iIdGrupoModelo { get { return _iIdGrupoModelo; } set { _iIdGrupoModelo = value; } }
        public string sGrupoModelo { get { return _sGrupoModelo; } set { _sGrupoModelo = value; } }
        public string sMatricula { get { return _sMatricula; } set { _sMatricula = value; } }
        public string sReferencia { get { return _sReferencia; } set { _sReferencia = value; } }
        public int iIdPadre { get { return _iIdPadre; } set { _iIdPadre = value; } }
        public decimal dCostoVuelo { get { return _dCostoVuelo; } set { _dCostoVuelo = value; } }
        public decimal dIvaVuelo { get { return _dIvaVuelo; } set { _dIvaVuelo = value; } }
        public int iIdPendiente { get { return _iIdPendiente; } set { _iIdPendiente = value; } }
        public int iDiferencia { get { return _iDiferencia; } set { _iDiferencia = value; } }
        public int iNoPax { get { return _iNoPax; } set { _iNoPax = value; } }
        
    }

    [Serializable]
    public class TableroFerry : BaseObjeto
    {
        private int _iIdFerry = 0;
        private int _iStatusJetSmart = 0;
        private int _iStatusApp = 0;

        public int iIdFerry { get { return _iIdFerry; } set { _iIdFerry = value; } }
        public int iStatusJetSmart { get { return _iStatusJetSmart; } set { _iStatusJetSmart = value; } }
        public int iStatusApp { get { return _iStatusApp; } set { _iStatusApp = value; } }
    }

    [Serializable]
    public class VentaFerry : BaseObjeto
    {
        private int _iIdFerry = 0;
        private decimal _dCostoVuelo = 0;
        private decimal _dIvaVuelo = 0;
        private DateTime _dtFechaReservar = new DateTime();
        private bool _bApp = false;
        private int _iStatusApp = 0;
        private bool _bEzMexJet = false;
        private int _iStatusEz = 0;
        private int _iPrioridad = 1;



        public int iIdFerry { get { return _iIdFerry; } set { _iIdFerry = value; } }
        public decimal dCostoVuelo { get { return _dCostoVuelo; } set { _dCostoVuelo = value; } }
        public decimal dIvaVuelo { get { return _dIvaVuelo; } set { _dIvaVuelo = value; } }
        public DateTime dtFechaReservar { get { return _dtFechaReservar; } set { _dtFechaReservar = value; } }
        public bool bApp { get { return _bApp; } set { _bApp = value; } }
        public int iStatusApp { get { return _iStatusApp; } set { _iStatusApp = value; } }
        public bool bEzMexJet { get { return _bEzMexJet; } set { _bEzMexJet = value; } }
        public int iStatusEz { get { return _iStatusEz; } set { _iStatusEz = value; } }
        public int iPrioridad { get { return _iPrioridad; } set { _iPrioridad = value; } }
    }

    [Serializable]
    public class ListaEnvios : BaseObjeto
    {
        private int _iIdLista = 0;
        private string _sNombreLista = string.Empty;

        public int iIdLista { get { return _iIdLista; } set { _iIdLista = value; } }
        public string sNombreLista { get { return _sNombreLista; } set { _sNombreLista = value; } }
    }

    [Serializable]
    public class FerryNuevos : BaseObjeto
    {
        private int _iIdFerry = 0;
        private int _iTrip = 0;
        private int _iNoPierna = 0;
        private int _iIdOrigen = 0;
        private string _sOrigen = string.Empty;
        private DateTime _dtFechaSalida = new DateTime();
        private int _iIdDestino = 0;
        private string _sDestino = string.Empty;
        private DateTime _dtFechaLlegada = new DateTime();
        private string _sMatricula = string.Empty;
        private bool _bJetSmart = false;
        private int _iStatusJetSmart = 0;
        private bool _bApp = false;
        private int _iStatusApp = 0;
        private bool _bEZMexJet = false;
        private int _iStatusEZ = 0;
        private decimal _dCostoVuelo = 0;
        private decimal _dIvaVuelo = 0;
        private DateTime _dtFechaReservar = new DateTime();
        private string _sUsuario = string.Empty;
        private string _sIP = string.Empty;


        public int iIdFerry { get { return _iIdFerry; } set { _iIdFerry = value; } }

        public int iTrip { get { return _iTrip; } set { _iTrip = value; } }

        public int iNoPierna { get { return _iNoPierna; } set { _iNoPierna = value; } }

        public int iIdOrigen { get { return _iIdOrigen; } set { _iIdOrigen = value; } }

        public string sOrigen { get { return _sOrigen; } set { _sOrigen = value; } }

        public DateTime dtFechaSalida { get { return _dtFechaSalida; } set { _dtFechaSalida = value; } }

        public int iIdDestino { get { return _iIdDestino; } set { _iIdDestino = value; } }

        public string sDestino { get { return _sDestino; } set { _sDestino = value; } }

        public DateTime dtFechaLlegada { get { return _dtFechaLlegada; } set { _dtFechaLlegada = value; } }

        public string sMatricula { get { return _sMatricula; } set { _sMatricula = value; } }

        public bool bJetSmart { get { return _bJetSmart; } set { _bJetSmart = value; } }

        public int iStatusJetSmart { get { return _iStatusJetSmart; } set { _iStatusJetSmart = value; } }

        public bool bApp { get { return _bApp; } set { _bApp = value; } }

        public int iStatusApp { get { return _iStatusApp; } set { _iStatusApp = value; } }

        public bool bEZMexJet { get { return _bEZMexJet; } set { _bEZMexJet = value; } }

        public int iStatusEZ { get { return _iStatusEZ; } set { _iStatusEZ = value; } }

        public decimal dCostoVuelo { get { return _dCostoVuelo; } set { _dCostoVuelo = value; } }

        public decimal dIvaVuelo { get { return _dIvaVuelo; } set { _dIvaVuelo = value; } }

        public DateTime dtFechaReservar { get { return _dtFechaReservar; } set { _dtFechaReservar = value; } }

        public string sUsuario { get { return _sUsuario; } set { _sUsuario = value; } }

        public string sIP { get { return _sIP; } set { _sIP = value; } }

    }
}