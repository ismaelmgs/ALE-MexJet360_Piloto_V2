using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ALE_MexJet.Objetos
{
    [Serializable, Bindable(BindableSupport.Yes)]
    public class Contrato_Generales
    {
        private int _iId  = -1;
        private int _iIdContrato = -1;
        private int _iIdCliente = -1;
        private int _iIdEjecutivo = -1;
        private int _iiDVendedor = -1;
        private int _iIdPAquete = -1;
        private int _iIdGrupoModelo = -1;
        private int _iAñoContratados = -1;
        private int _iMesesGracia = -1;
        private int _iHorasContratadasTotal = -1;
        private int _iHorasContratadasAño = -1;
        private string _sContrato = string.Empty;
        private decimal _dHorasNoUsadasAcumulables = 0m;
        private DateTime _dtFechaContrato;
        private DateTime _dtFechaInicioVuelo;
        private string _sMatricula = string.Empty;


        private int _iIdTipoCambio = -1;
        private decimal _dAnticipioInicial = 0m;
        private decimal _dFijoAnual = 0m;
        private decimal _dRenovacion = 0m;
        private decimal _dPrenda = 0m;
        private decimal _dIncrementoCostoDirectoRenovacion = 0m;

        private List<Contratos_Bases> _lstBases = new List<Contratos_Bases>();
        private bool _bReasigna = false;
        
        private string _sNotas = string.Empty;
        private string _sNotasIntercambios = string.Empty;
        private string _sNotasRangoCombustible = string.Empty;

        private string _sMetodoPagoFact = string.Empty;
        private string _sFormaPago = string.Empty;
        private string _sUsoCFDI = string.Empty;

        private string _sFormatoEdoCta = string.Empty;
        
        private string _sNombreArchivo = string.Empty;
        private byte[] _bContratoD = null;

        private int _iStatus = 1;
        private string _sUsuarioCreacion = string.Empty;
        private DateTime _dtFechaCreacion = new DateTime();
        private string _sUsuarioMod = string.Empty;
        private DateTime _dtFechaMod = new DateTime();
        private string _sIP = string.Empty;

        public int iId { get { return _iId; } set { _iId = value; } }
        public int iIdContrato { get { return _iIdContrato; } set { _iIdContrato = value; } }
        public int iIdCliente { get { return _iIdCliente; } set { _iIdCliente = value; } }
        public int iIdEjecutivo { get { return _iIdEjecutivo; } set { _iIdEjecutivo = value; } }
        public int iiDVendedor { get { return _iiDVendedor; } set { _iiDVendedor = value; } }
        public int iIdPAquete { get { return _iIdPAquete; } set { _iIdPAquete = value; } }
        public int iIdGrupoModelo { get { return _iIdGrupoModelo; } set { _iIdGrupoModelo = value; } }
        public int iAñoContratados { get { return _iAñoContratados; } set { _iAñoContratados = value; } }
        public int iMesesGracia { get { return _iMesesGracia; } set { _iMesesGracia = value; } }
        public int iHorasContratadasTotal { get { return _iHorasContratadasTotal; } set { _iHorasContratadasTotal = value; } }
        public int iHorasContratadasAño { get { return _iHorasContratadasAño; } set { _iHorasContratadasAño = value; } }
        public string sContrato { get { return _sContrato; } set { _sContrato = value; } }
        public decimal dHorasNoUsadasAcumulables { get { return _dHorasNoUsadasAcumulables; } set { _dHorasNoUsadasAcumulables = value; } }
        public DateTime dtFechaContrato { get { return _dtFechaContrato; } set { _dtFechaContrato = value; } }
        public DateTime dtFechaInicioVuelo { get { return _dtFechaInicioVuelo; } set { _dtFechaInicioVuelo = value; } }
        public string sMatricula { get { return _sMatricula; } set { _sMatricula = value; } }


        public int iIdTipoCambio { get { return _iIdTipoCambio; } set { _iIdTipoCambio = value; } }
        public decimal dAnticipioInicial { get { return _dAnticipioInicial; } set { _dAnticipioInicial = value; } }
        public decimal dFijoAnual { get { return _dFijoAnual; } set { _dFijoAnual = value; } }
        public decimal dRenovacion { get { return _dRenovacion; } set { _dRenovacion = value; } }
        public decimal dPrenda { get { return _dPrenda; } set { _dPrenda = value; } }
        public decimal dIncrementoCostoDirectoRenovacion { get { return _dIncrementoCostoDirectoRenovacion; } set { _dIncrementoCostoDirectoRenovacion = value; } }

        public List<Contratos_Bases> lstBases { get { return _lstBases; } set { _lstBases = value; } }
        public bool bReasigna { get { return _bReasigna; } set { _bReasigna = value; } }
        public string sNotas { get { return _sNotas; } set { _sNotas = value; } }

        public string sNotasIntercambios { get { return _sNotasIntercambios; } set { _sNotasIntercambios = value; } }
        public string sNotasRangoCombustible { get { return _sNotasRangoCombustible; } set { _sNotasRangoCombustible = value; } }


        public string sMetodoPagoFact { get { return _sMetodoPagoFact; } set { _sMetodoPagoFact = value; } }
        public string sFormaPago { get { return _sFormaPago; } set { _sFormaPago = value; } }
        public string sUsoCFDI { get { return _sUsoCFDI; } set { _sUsoCFDI = value; } }
        public string sFormatoEdoCta { get => _sFormatoEdoCta; set => _sFormatoEdoCta = value; }

        public string sNombreArchivo { get { return _sNombreArchivo; } set { _sNombreArchivo = value; } }
        public byte[] bContratoD { get { return _bContratoD; } set { _bContratoD = value; } }

        public int iStatus { get { return _iStatus; } set { _iStatus = value; } }

        public string sUsuarioCreacion { get { return _sUsuarioCreacion; } set { _sUsuarioCreacion = value; } }

        public DateTime dtFechaCreacion { get { return _dtFechaCreacion; } set { _dtFechaCreacion = value; } }

        public string sUsuarioMod { get { return _sUsuarioMod; } set { _sUsuarioMod = value; } }

        public DateTime dtFechaMod { get { return _dtFechaMod; } set { _dtFechaMod = value; } }

        public string sIP { get { return _sIP; } set { _sIP = value; } }

        

        //[Display(Name = "Fecha DT"), Required]
        //public string sFechaUltMov { get { return _sFechaUltMov; } set { _sFechaUltMov = value; } }
    }
}