using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;

namespace ALE_MexJet.Objetos
{

    [Serializable, Bindable(BindableSupport.Yes)]
    public class BusqRemision : BaseObjeto
    {
        private long _iIdRemision = 0;

        public long iIdRemision
        {
            get { return _iIdRemision; }
            set { _iIdRemision = value; }
        }
        private int _iIdContrato = 0;

        public int iIdContrato
        {
            get { return _iIdContrato; }
            set { _iIdContrato = value; }
        }
    }



    [Serializable, Bindable(BindableSupport.Yes)]
    public class Remision : BaseObjeto
    {
        private long _iIdRemision = -1;
        private int _iIdCliente = -1;
        private int _iIdContrato = -1;
        private DateTime _dtFechaRemision = DateTime.Now;
        private bool _bAplicaIntercambio = false;
        private decimal _dFactorEspecial = 0;
        private string _sGrupoModeloDesc = string.Empty;
        private string _sCliente = string.Empty;
        private string _sContrato = string.Empty;
        private int _iStatus = 1;
        private string _sUsuarioCreacion = string.Empty;
        private DateTime _dtFechaCreacion = new DateTime();
        private string _sUsuarioMod = string.Empty;
        private DateTime _dtFechaMod = new DateTime();
        private string _sIP = string.Empty;
        private string _sFechaUltMov = string.Empty;
        private List<BitacoraRemision> _oLstBit = new List<BitacoraRemision>();

        
       
        public long iIdRemision { get { return _iIdRemision; } set { _iIdRemision = value; } }
        public DateTime dtFechaRemision { get { return _dtFechaRemision; } set { _dtFechaRemision = value; }}
        public int iIdCliente { get { return _iIdCliente; } set { _iIdCliente = value; }}
        public int iIdContrato { get { return _iIdContrato; } set { _iIdContrato = value; } }
        public bool bAplicaIntercambio { get { return _bAplicaIntercambio; } set { _bAplicaIntercambio = value; } }
        public decimal dFactorEspecial { get { return _dFactorEspecial; } set { _dFactorEspecial = value; } }
        public string sGrupoModeloDesc { get { return _sGrupoModeloDesc; } set { _sGrupoModeloDesc = value; } }
        public string sCliente { get { return _sCliente; } set { _sCliente = value; } }
        public string sContrato { get { return _sContrato; } set { _sContrato = value; } }
        public int iStatus { get { return _iStatus; } set { _iStatus = value; } }
        public string sUsuarioCreacion { get { return _sUsuarioCreacion; } set { _sUsuarioCreacion = value; } }
        public DateTime dtFechaCreacion { get { return _dtFechaCreacion; } set { _dtFechaCreacion = value; } }
        public string sUsuarioMod { get { return _sUsuarioMod; } set { _sUsuarioMod = value; } }
        public DateTime dtFechaMod { get { return _dtFechaMod; } set { _dtFechaMod = value; } }
        public string sIP { get { return _sIP; } set { _sIP = value; } }
        public string sFechaUltMov { get { return _sFechaUltMov; } set { _sFechaUltMov = value; } }
        public List<BitacoraRemision> oLstBit { get { return _oLstBit; } set { _oLstBit = value; } }
    }



    [Serializable, Bindable(BindableSupport.Yes)]
    public class BitacoraRemision
    {
        private long _iIdRemision = -1;
        private long _lBitacora = 0;
        private int _iIdSolicitudVuelo = 0;
        private int _iIdTipoPierna = 0;
        private int _iSeCobra = 0;
        private int _iPax = 0;
        private string _sUsuario = string.Empty;
        private string _sIP = string.Empty;
        

        public long iIdRemision { get { return _iIdRemision; } set { _iIdRemision = value; } }
        public long lBitacora { get { return _lBitacora; } set { _lBitacora = value; } }
        public int iIdSolicitudVuelo { get { return _iIdSolicitudVuelo; } set { _iIdSolicitudVuelo = value; } }
        public int iIdTipoPierna { get { return _iIdTipoPierna; } set { _iIdTipoPierna = value; } }
        public int iSeCobra { get { return _iSeCobra; } set { _iSeCobra = value; } }
        public int iPax { get { return _iPax; } set { _iPax = value; } }
        public string sUsuario { get { return _sUsuario; } set { _sUsuario = value; } }
        public string sIP { get { return _sIP; } set { _sIP = value; } }
    }



    [Serializable, Bindable(BindableSupport.Yes)]
    public class DatosRemision : BaseObjeto
    {
        private long _iIdRemision = -1;
        private string _sClaveContrato = string.Empty;
        private string _sClaveCliente = string.Empty;
        private int _iIdContrato = -1;
        private ALE_MexJet.Objetos.Enumeraciones.SeCobraFerrys _eSeCobraFerry;
        private bool _bAplicaEsperaLibre = false;
        private decimal _dHorasPorVuelo = 0;
        private decimal _dFactorHrVuelo = 0;
        private int _iCobroTiempo = 0;  // 1.- Tiempo Vuelo 2.- TiempoCalzo
        private int _iMasMinutos = 0;
        private string _sRuta = string.Empty;
        private bool _bTiemposPactados = false;
        private int _iHorasPernocta = 0;
        private int _iFactosTiempoEsp = 0;
        private int _iIdGrupoModelo = 0;
        private int _iIdGrupoModeloPres = 0;
        private string _sIdGrupoModeloPres = string.Empty;
        private string _sGrupoModeloDesc = string.Empty;
        private decimal _dTipoCambio = 0;
        private string _sTipoPaquete = string.Empty;
        private string _sFechaInicio = string.Empty;
        private int _iHorasContratadasTotal = 0;
        private int _iHorasContratadasAnio = 0;
        
        

        //---====================== COBROS ==========================---
            // Vuelo
        private decimal _dVueloCostoDirNal = 0;
        private decimal _dVueloCostoDirInt = 0;
        private decimal _dTarifaVueloNal = 0;
        private decimal _dTarifaVueloInt = 0;
        private bool _bSeCobraCombustible = false;
        private Enumeraciones.CobroCombustible _eCobroCombustible;
        private decimal _dFactorTramosNal = 0;
        private decimal _dFactorTramosInt = 0;

        
            //Tiempo de Espera
        private bool _bSeCobreEspera = false;
        private decimal _dTarifaNalEspera = 0;
        private decimal _dTarifaIntEspera = 0;
        private decimal _dPorTarifaNalEspera = 0;
        private decimal _dPorTarifaIntEspera = 0;
        
            //Pernoctas
        private bool _bSeCobraPernoctas = false;
        private decimal _dTarifaDolaresNal = 0;
        private decimal _dPorTarifaVueloNal = 0;
        private decimal _dTarifaDolaresInt = 0;
        private decimal _dPorTarifaVueloInt = 0;

            // Intercambios
        private List<IntercambioRemision> _oLstIntercambios = new List<IntercambioRemision>();

        // Helicopteros
        private int _iCobraFerryHelicoptero = 0;
        private int _iMasMinutosHelicoptero = 0;

        //---====================== DESCUENTOS =========================---
        //Tiempo de Espera
        private bool _bSeDescuentaEsperaNal = false;
        private bool _bSeDescuentaEsperaInt = false;
        private decimal _dFactorEHrVueloNal = 0;
        private decimal _dFactorEHrVueloInt = 0;
            //Pernoctas
        private bool _bSeDescuentanPerNal = false;
        private bool _bSeDescuentanPerInt = false;
        private decimal _dFactorConvHrVueloNal = 0;
        private decimal _dFactorConvHrVueloInt = 0;
        private int _iPernoctasLibreAnio = 0;

        
        private DataTable _dtTramos = new DataTable();
        private DataTable _dtBases = new DataTable();
        private DataTable _dtTramosPactadosEsp = new DataTable();
        private DataTable _dtTramosPactadosGen = new DataTable();
        private DataTable _dtIntercambios = new DataTable();

        // --================= VUELOS SIMULTANEOS =======================--
        private bool _bPermiteVloSimultaneo = false;
        private int _iCuantosVloSimultaneo = 0;
        private decimal _dFactorVloSimultaneo = 0;
        

        // --================= FACTORES APLICADOS =======================---
        private bool _bAplicoFactorEspecial = false;
        private bool _bAplicoIntercambio = false;
        private bool _bAplicaGiraEspera = false;
        private bool _bAplicaGiraHorario = false;
        private bool _bAplicaFactorFechaPico = false;
        private bool _bAplicaFactorVueloSimultaneo = false;
        private bool _bAplicaFactorTramoNacional = false;
        private bool _bAplicaFactorTramoInternacional = false;
        private decimal _dFactorEspecialF = 0;
        
        
        private decimal _dFactorIntercambioF = 0;
        private decimal _dFactorGiraEsperaF = 0;
        private decimal _dFactorGiraHorarioF = 0;
        private decimal _dFactorFechaPicoF = 0;
        private decimal _dFactorVueloSimulF = 0;
        
        

        // --================= FACTORES APLICADOS =======================---
        private bool _bAplicaFerryIntercambio = false;

        // --================= TOTALES TIEMPOS =======================---
        private string _sTotalTiempoVuelo = string.Empty;
        private string _sTotalTiempoCalzo = string.Empty;

        // --================= BANDERA DOBLE PRESUPUESTO ====================--
        private bool _bPermiteDoblePresupuesto = true;



        // -- ============= TARIFAS COBRADAS EN REMISION =============== --
        private decimal _dTarifaVloNalComb = 0;
        private decimal _dTarifaVloIntComb = 0;
        private decimal _dTiempoEsperaGeneral = 0;
        private decimal _dTiempoVueloGeneral = 0;
        private decimal _dTotalTiempoEsperaCobrar = 0;
        private string _sTiempoEsperaGeneral = string.Empty;
        private string _sTiempoVueloGeneral = string.Empty;
        private string _sTotalTiempoEsperaCobrar = string.Empty;





        public long lIdRemision { get { return _iIdRemision; } set { _iIdRemision = value; } }
        public string sClaveContrato { get => _sClaveContrato; set => _sClaveContrato = value; }
        public string sClaveCliente { get => _sClaveCliente; set => _sClaveCliente = value; }
        public int iIdContrato { get { return _iIdContrato; } set { _iIdContrato = value; } }
        public ALE_MexJet.Objetos.Enumeraciones.SeCobraFerrys eSeCobraFerry { get { return _eSeCobraFerry; } set { _eSeCobraFerry = value; } }
        public decimal dFactorTramosNal { get { return _dFactorTramosNal; } set { _dFactorTramosNal = value; } }
        public decimal dFactorTramosInt { get { return _dFactorTramosInt; } set { _dFactorTramosInt = value; } }
        public bool bAplicaEsperaLibre { get { return _bAplicaEsperaLibre; } set { _bAplicaEsperaLibre = value; } }
        public decimal dHorasPorVuelo { get { return _dHorasPorVuelo; } set { _dHorasPorVuelo = value; } }
        public decimal dFactorHrVuelo { get { return _dFactorHrVuelo; } set { _dFactorHrVuelo = value; } }
        public int iCobroTiempo { get { return _iCobroTiempo; } set { _iCobroTiempo = value; } }
        public int iMasMinutos { get { return _iMasMinutos; } set { _iMasMinutos = value; } }
        public string sRuta { get { return _sRuta; } set { _sRuta = value; } }
        public bool bTiemposPactados { get { return _bTiemposPactados; } set { _bTiemposPactados = value; } }
        public int iHorasPernocta { get { return _iHorasPernocta; } set { _iHorasPernocta = value; } }
        public int iFactosTiempoEsp { get { return _iFactosTiempoEsp; } set { _iFactosTiempoEsp = value; } }
        public int iIdGrupoModelo { get { return _iIdGrupoModelo; } set { _iIdGrupoModelo = value; } }
        public int iIdGrupoModeloPres { get { return _iIdGrupoModeloPres; } set { _iIdGrupoModeloPres = value; } }
        public string sIdGrupoModeloPres { get { return _sIdGrupoModeloPres; } set { _sIdGrupoModeloPres = value; } }
        public string sGrupoModeloDesc { get { return _sGrupoModeloDesc; } set { _sGrupoModeloDesc = value; } }
        public decimal dTipoCambio { get { return _dTipoCambio; } set { _dTipoCambio = value; } }
        public string sTipoPaquete { get { return _sTipoPaquete; } set { _sTipoPaquete = value; } }
        public string sFechaInicio { get { return _sFechaInicio; } set { _sFechaInicio = value; } }
        public int iHorasContratadasTotal { get { return _iHorasContratadasTotal; } set { _iHorasContratadasTotal = value; } }
        public int iHorasContratadasAnio { get { return _iHorasContratadasAnio; } set { _iHorasContratadasAnio = value; } }
        public decimal dVueloCostoDirNal { get { return _dVueloCostoDirNal; } set { _dVueloCostoDirNal = value; } }
        public decimal dVueloCostoDirInt { get { return _dVueloCostoDirInt; } set { _dVueloCostoDirInt = value; } }
        public decimal dTarifaVueloNal { get { return _dTarifaVueloNal; } set { _dTarifaVueloNal = value; } }
        public decimal dTarifaVueloInt { get { return _dTarifaVueloInt; } set { _dTarifaVueloInt = value; } }
        public bool bSeCobraCombustible { get { return _bSeCobraCombustible; } set { _bSeCobraCombustible = value; } }
        public Enumeraciones.CobroCombustible eCobroCombustible { get { return _eCobroCombustible; } set { _eCobroCombustible = value; } }
        public bool bSeCobreEspera { get { return _bSeCobreEspera; } set { _bSeCobreEspera = value; } }
        public decimal dTarifaNalEspera { get { return _dTarifaNalEspera; } set { _dTarifaNalEspera = value; } }
        public decimal dTarifaIntEspera { get { return _dTarifaIntEspera; } set { _dTarifaIntEspera = value; } }
        public decimal dPorTarifaNalEspera { get { return _dPorTarifaNalEspera; } set { _dPorTarifaNalEspera = value; } }
        public decimal dPorTarifaIntEspera { get { return _dPorTarifaIntEspera; } set { _dPorTarifaIntEspera = value; } }
        public decimal dTarifaDolaresNal { get { return _dTarifaDolaresNal; } set { _dTarifaDolaresNal = value; } }
        public decimal dPorTarifaVueloNal { get { return _dPorTarifaVueloNal; } set { _dPorTarifaVueloNal = value; } }
        public decimal dTarifaDolaresInt { get { return _dTarifaDolaresInt; } set { _dTarifaDolaresInt = value; } }
        public decimal dPorTarifaVueloInt { get { return _dPorTarifaVueloInt; } set { _dPorTarifaVueloInt = value; } }

        public List<IntercambioRemision> oLstIntercambios { get { return _oLstIntercambios; } set { _oLstIntercambios = value; } }
        public int iCobraFerryHelicoptero { get { return _iCobraFerryHelicoptero; } set { _iCobraFerryHelicoptero = value; } }
        public int iMasMinutosHelicoptero { get { return _iMasMinutosHelicoptero; } set { _iMasMinutosHelicoptero = value; } }

        public bool bSeDescuentaEsperaNal { get { return _bSeDescuentaEsperaNal; } set { _bSeDescuentaEsperaNal = value; } }
        public bool bSeDescuentaEsperaInt { get { return _bSeDescuentaEsperaInt; } set { _bSeDescuentaEsperaInt = value; } }
        public decimal dFactorEHrVueloNal { get { return _dFactorEHrVueloNal; } set { _dFactorEHrVueloNal = value; } }
        public decimal dFactorEHrVueloInt { get { return _dFactorEHrVueloInt; } set { _dFactorEHrVueloInt = value; } }
        public bool bSeCobraPernoctas { get { return _bSeCobraPernoctas; } set { _bSeCobraPernoctas = value; } }
        public bool bSeDescuentanPerNal { get { return _bSeDescuentanPerNal; } set { _bSeDescuentanPerNal = value; } }
        public bool bSeDescuentanPerInt { get { return _bSeDescuentanPerInt; } set { _bSeDescuentanPerInt = value; } }
        public decimal dFactorConvHrVueloNal { get { return _dFactorConvHrVueloNal; } set { _dFactorConvHrVueloNal = value; } }
        public decimal dFactorConvHrVueloInt { get { return _dFactorConvHrVueloInt; } set { _dFactorConvHrVueloInt = value; } }
        public int iPernoctasLibreAnio { get { return _iPernoctasLibreAnio; } set { _iPernoctasLibreAnio = value; } }

        public DataTable dtTramos { get { return _dtTramos; } set { _dtTramos = value; } }
        public DataTable dtBases { get { return _dtBases; } set { _dtBases = value; } }
        public DataTable dtTramosPactadosEsp { get { return _dtTramosPactadosEsp; } set { _dtTramosPactadosEsp = value; } }
        public DataTable dtTramosPactadosGen { get { return _dtTramosPactadosGen; } set { _dtTramosPactadosGen = value; } }
        public DataTable dtIntercambios { get { return _dtIntercambios; } set { _dtIntercambios = value; } }

        // VUELOS SIMULTANEOS
        public bool bPermiteVloSimultaneo { get { return _bPermiteVloSimultaneo; } set { _bPermiteVloSimultaneo = value; } }
        public int iCuantosVloSimultaneo { get { return _iCuantosVloSimultaneo; } set { _iCuantosVloSimultaneo = value; } }
        public decimal dFactorVloSimultaneo { get { return _dFactorVloSimultaneo; } set { _dFactorVloSimultaneo = value; } }


        public bool bAplicoFactorEspecial { get { return _bAplicoFactorEspecial; } set { _bAplicoFactorEspecial = value; } }
        public bool bAplicoIntercambio { get { return _bAplicoIntercambio; } set { _bAplicoIntercambio = value; } }
        public bool bAplicaGiraEspera { get { return _bAplicaGiraEspera; } set { _bAplicaGiraEspera = value; } }
        public bool bAplicaGiraHorario { get { return _bAplicaGiraHorario; } set { _bAplicaGiraHorario = value; } }
        public bool bAplicaFactorFechaPico { get { return _bAplicaFactorFechaPico; } set { _bAplicaFactorFechaPico = value; } }
        public bool bAplicaFactorVueloSimultaneo { get { return _bAplicaFactorVueloSimultaneo; } set { _bAplicaFactorVueloSimultaneo = value; } }
        public bool bAplicaFactorTramoNacional { get { return _bAplicaFactorTramoNacional; } set { _bAplicaFactorTramoNacional = value; } }
        public bool bAplicaFactorTramoInternacional { get { return _bAplicaFactorTramoInternacional; } set { _bAplicaFactorTramoInternacional = value; } }

        public decimal dFactorEspecialF { get { return _dFactorEspecialF; } set { _dFactorEspecialF = value; } }
        public decimal dFactorIntercambioF { get { return _dFactorIntercambioF; } set { _dFactorIntercambioF = value; } }
        public decimal dFactorGiraEsperaF { get { return _dFactorGiraEsperaF; } set { _dFactorGiraEsperaF = value; } }
        public decimal dFactorGiraHorarioF { get { return _dFactorGiraHorarioF; } set { _dFactorGiraHorarioF = value; } }
        public decimal dFactorFechaPicoF { get { return _dFactorFechaPicoF; } set { _dFactorFechaPicoF = value; } }
        public decimal dFactorVueloSimulF { get { return _dFactorVueloSimulF; } set { _dFactorVueloSimulF = value; } }


        public bool bAplicaFerryIntercambio { get { return _bAplicaFerryIntercambio; } set { _bAplicaFerryIntercambio = value; } }


        public string sTotalTiempoVuelo { get { return _sTotalTiempoVuelo; } set { _sTotalTiempoVuelo = value; } }
        public string sTotalTiempoCalzo { get { return _sTotalTiempoCalzo; } set { _sTotalTiempoCalzo = value; } }


        public bool bPermiteDoblePresupuesto { get { return _bPermiteDoblePresupuesto; } set { _bPermiteDoblePresupuesto = value; } }

        public decimal dTarifaVloNalComb { get => _dTarifaVloNalComb; set => _dTarifaVloNalComb = value; }
        public decimal dTarifaVloIntComb { get => _dTarifaVloIntComb; set => _dTarifaVloIntComb = value; }
        public decimal dTiempoEsperaGeneral { get => _dTiempoEsperaGeneral; set => _dTiempoEsperaGeneral = value; }
        public decimal dTiempoVueloGeneral { get => _dTiempoVueloGeneral; set => _dTiempoVueloGeneral = value; }
        public decimal dTotalTiempoEsperaCobrar { get => _dTotalTiempoEsperaCobrar; set => _dTotalTiempoEsperaCobrar = value; }
        public string sTiempoEsperaGeneral { get => _sTiempoEsperaGeneral; set => _sTiempoEsperaGeneral = value; }
        public string sTiempoVueloGeneral { get => _sTiempoVueloGeneral; set => _sTiempoVueloGeneral = value; }
        public string sTotalTiempoEsperaCobrar { get => _sTotalTiempoEsperaCobrar; set => _sTotalTiempoEsperaCobrar = value; }
        
    }



    [Serializable, Bindable(BindableSupport.Yes)]
    public class ImportesRemision
    {
        private long _iIdRemision = 0;
        private int _iIdConcepto = 0;
        private string _sCantidad = string.Empty;
        private decimal _dTarifaDlls = 0;
        private decimal _dImporte = 0;
        private string _sHrDescontar = string.Empty;
        private string _sUsuario = string.Empty;
        private string _sIP = string.Empty;

        public long iIdRemision { get { return _iIdRemision; } set { _iIdRemision = value; } }
        public int iIdConcepto { get { return _iIdConcepto; } set { _iIdConcepto = value; } }
        public string sCantidad { get { return _sCantidad; } set { _sCantidad = value; } }
        public decimal dTarifaDlls { get { return _dTarifaDlls; } set { _dTarifaDlls = value; } }
        public decimal dImporte { get { return _dImporte; } set { _dImporte = value; } }
        public string sHrDescontar { get { return _sHrDescontar; } set { _sHrDescontar = value; } }
        public string sUsuario { get { return _sUsuario; } set { _sUsuario = value; } }
        public string sIP { get { return _sIP; } set { _sIP = value; } }
    }



    [Serializable, Bindable(BindableSupport.Yes)]
    public class ServiciosVueloH : BaseObjeto
    {
        private long _iIdRemision = 0;
        private decimal _dSubtotal = 0;
        private decimal _dIva = 0;
        private decimal _dTotal = 0;
        private string _sHrDescontar = string.Empty;
        private decimal _dTipoCambio = 0;
        private int _iFactorIva = 0;
        private decimal _dCombustibleNal = 0;
        private decimal _dCombustibleInt = 0;
        private string _sUsuario = string.Empty;
        private string _sIP = string.Empty;

        public long iIdRemision
        {
          get { return _iIdRemision; }
          set { _iIdRemision = value; }
        }
        
        public decimal dSubtotal
        {
          get { return _dSubtotal; }
          set { _dSubtotal = value; }
        }
        
        public decimal dIva
        {
          get { return _dIva; }
          set { _dIva = value; }
        }

        public decimal dTotal
        {
          get { return _dTotal; }
          set { _dTotal = value; }
        }

        public string sHrDescontar
        {
            get { return _sHrDescontar; }
            set { _sHrDescontar = value; }
        }

        public decimal dTipoCambio
        {
            get { return _dTipoCambio; }
            set { _dTipoCambio = value; }
        }

        public int iFactorIva
        {
            get { return _iFactorIva; }
            set { _iFactorIva = value; }
        }

        public decimal dCombustibleNal
        {
            get { return _dCombustibleNal; }
            set { _dCombustibleNal = value; }
        }
        public decimal dCombustibleInt
        {
            get { return _dCombustibleInt; }
            set { _dCombustibleInt = value; }
        }

        public string sUsuario
        {
            get { return _sUsuario; }
            set { _sUsuario = value; }
        }

        public string sIP
        {
            get { return _sIP; }
            set { _sIP = value; }
        }
    }



    [Serializable, Bindable(BindableSupport.Yes)]
    public class ServiciosVueloD : BaseObjeto
    {
        private long _iIdRemision = 0;
        private int _iIdConcepto = 0;
        private decimal _dCostoDirecto = 0;
        private decimal _dCostoComb = 0;
        private decimal _dTarifaDlls = 0;
        private string _sCantidad = string.Empty;
        private decimal _dImporteDlls = 0;
        private string _sHrDescontar = string.Empty;
        private string _sUsuario = string.Empty;
        private string _sIP = string.Empty;

        public long iIdRemision
        {
            get { return _iIdRemision; }
            set { _iIdRemision = value; }
        }
        
        public int iIdConcepto
        {
            get { return _iIdConcepto; }
            set { _iIdConcepto = value; }
        }
        
        public decimal dCostoDirecto
        {
            get { return _dCostoDirecto; }
            set { _dCostoDirecto = value; }
        }
        
        public decimal dCostoComb
        {
            get { return _dCostoComb; }
            set { _dCostoComb = value; }
        }
        
        public decimal dTarifaDlls
        {
            get { return _dTarifaDlls; }
            set { _dTarifaDlls = value; }
        }
        
        public string sCantidad
        {
            get { return _sCantidad; }
            set { _sCantidad = value; }
        }
        
        public decimal dImporteDlls
        {
            get { return _dImporteDlls; }
            set { _dImporteDlls = value; }
        }
        
        public string sHrDescontar
        {
            get { return _sHrDescontar; }
            set { _sHrDescontar = value; }
        }
        
        public string sUsuario
        {
            get { return _sUsuario; }
            set { _sUsuario = value; }
        }
        

        public string sIP
        {
            get { return _sIP; }
            set { _sIP = value; }
        }

    }



    [Serializable, Bindable(BindableSupport.Yes)]
    public class ServiciosCargoH : BaseObjeto
    {
        private long _iIdRemision = 0;
        private decimal _dSubtotal = 0;
        private decimal _iFactorIVA = 0;
        private decimal _dIva = 0;
        private decimal _dTotal = 0;
        private string _sUsuario = string.Empty;
        private string _sIP = string.Empty;


        public long iIdRemision
        {
            get { return _iIdRemision; }
            set { _iIdRemision = value; }
        }

        public decimal dSubtotal
        {
            get { return _dSubtotal; }
            set { _dSubtotal = value; }
        }

        public decimal iFactorIVA
        {
            get { return _iFactorIVA; }
            set { _iFactorIVA = value; }
        }

        public decimal dIva
        {
            get { return _dIva; }
            set { _dIva = value; }
        }

        public decimal dTotal
        {
            get { return _dTotal; }
            set { _dTotal = value; }
        }

        public string sUsuario
        {
            get { return _sUsuario; }
            set { _sUsuario = value; }
        }

        public string sIP
        {
            get { return _sIP; }
            set { _sIP = value; }
        }
    }



    [Serializable, Bindable(BindableSupport.Yes)]
    public class ServiciosCargoD : BaseObjeto
    {
        private long _iIdRemision = 0;
        private int _iIdServicioCargo = 0;
        private decimal _dImporte = 0;
        private string _sUsuario = string.Empty;
        private string _sIP = string.Empty;


        public long iIdRemision
        {
            get { return _iIdRemision; }
            set { _iIdRemision = value; }
        }

        public int iIdServicioCargo
        {
            get { return _iIdServicioCargo; }
            set { _iIdServicioCargo = value; }
        }

        public decimal dImporte
        {
            get { return _dImporte; }
            set { _dImporte = value; }
        }

        public string sUsuario
        {
            get { return _sUsuario; }
            set { _sUsuario = value; }
        }

        public string sIP
        {
            get { return _sIP; }
            set { _sIP = value; }
        }
    }



    [Serializable, Bindable(BindableSupport.Yes)]
    public class RemisionDatosGrals : BaseObjeto
    {
        private long _iIdRemision = 0;
        private string _sTotalTiempoCobrar = string.Empty;
        private bool _bAplicaFactorEspecual = false;
        private bool _bIntercambio = false;
        private bool _bGira = false;
        private bool _bGiraHorario = false;
        private bool _bAplicaHoraPico = false;
        private bool _bAplicaVueloSimultaneo = false;
        private bool _bAplicaFactorTramoNal = false;
        private bool _bAplicaFactorTramoInt = false;
        

        private decimal _dFactorIntercambio = 0;
        private decimal _dFactorGiraEspera = 0;
        private decimal _dFactorGiraHorario = 0;
        private decimal _dFactorFechaPico = 0;
        private decimal _dFactorVueloSimultaneo = 0;
        private decimal _dFactorTramoNal = 0;
        private decimal _dFactorTramoInt = 0;
        
        
        private decimal _dTotalRemisionPesos = 0;
        private decimal _dTotalRemisionDlls = 0;
        private int _iStatus = 0;
        private string _sUsuario = string.Empty;
        private string _sIP = string.Empty;

        private int iIdContrato = 0;
        private string sMatricula = string.Empty;
        private string sCargo = string.Empty; //Total de horas
        private string sAbono = string.Empty;
        private int iIdMotivo = 0;
        private string sNotas = string.Empty;

        private List<KardexRemision> _oLstKardex = new List<KardexRemision>();


        public long iIdRemision { get { return _iIdRemision; } set { _iIdRemision = value; } }        
        public string sTotalTiempoCobrar { get { return _sTotalTiempoCobrar; } set { _sTotalTiempoCobrar = value; } }
        public bool bIntercambio { get { return _bIntercambio; } set { _bIntercambio = value; } }
        public bool bGira { get { return _bGira; } set { _bGira = value; } }
        public bool bGiraHorario { get { return _bGiraHorario; } set { _bGiraHorario = value; } }
        public bool bAplicaHoraPico { get { return _bAplicaHoraPico; } set { _bAplicaHoraPico = value; } }
        public bool bAplicaVueloSimultaneo { get { return _bAplicaVueloSimultaneo; } set { _bAplicaVueloSimultaneo = value; } }
        public bool bAplicaFactorEspecual { get { return _bAplicaFactorEspecual; } set { _bAplicaFactorEspecual = value; } }
        public bool bAplicaFactorTramoNal { get { return _bAplicaFactorTramoNal; } set { _bAplicaFactorTramoNal = value; } }
        public bool bAplicaFactorTramoInt { get { return _bAplicaFactorTramoInt; } set { _bAplicaFactorTramoInt = value; }}
        
        public decimal dFactorIntercambio { get { return _dFactorIntercambio; } set { _dFactorIntercambio = value; } } 
        public decimal dFactorGiraEspera { get { return _dFactorGiraEspera; } set { _dFactorGiraEspera = value; } }
        public decimal dFactorGiraHorario { get { return _dFactorGiraHorario; } set { _dFactorGiraHorario = value; } }
        public decimal dFactorFechaPico { get { return _dFactorFechaPico; } set { _dFactorFechaPico = value; } }
        public decimal dFactorVueloSimultaneo { get { return _dFactorVueloSimultaneo; } set { _dFactorVueloSimultaneo = value; } }
        public decimal dFactorTramoNal { get { return _dFactorTramoNal; } set { _dFactorTramoNal = value; }}
        public decimal dFactorTramoInt { get { return _dFactorTramoInt; } set { _dFactorTramoInt = value; } }
        
        public decimal dTotalRemisionPesos { get { return _dTotalRemisionPesos; } set { _dTotalRemisionPesos = value; }}
        public decimal dTotalRemisionDlls { get { return _dTotalRemisionDlls; } set { _dTotalRemisionDlls = value; } }
        public int iStatus { get { return _iStatus; } set { _iStatus = value; }}
        public string sUsuario { get { return _sUsuario; } set { _sUsuario = value; } }
        public string sIP { get { return _sIP; } set { _sIP = value; } }

        public int IIdContrato { get => iIdContrato; set => iIdContrato = value; }
        public string SMatricula { get => sMatricula; set => sMatricula = value; }
        public string SCargo { get => sCargo; set => sCargo = value; }
        public string SAbono { get => sAbono; set => sAbono = value; }
        public int IIdMotivo { get => iIdMotivo; set => iIdMotivo = value; }
        public string SNotas { get => sNotas; set => sNotas = value; }
        public List<KardexRemision> OLstKardex { get => _oLstKardex; set => _oLstKardex = value; }
    }

    [Serializable, Bindable(BindableSupport.Yes)]
    public class KardexRemision : BaseObjeto
    {
        private long _iIdRemision = 0;
        private int iIdContrato = 0;
        private string sMatricula = string.Empty;
        private string sCargo = string.Empty; //Total de horas
        private string sAbono = string.Empty;
        private int iIdMotivo = 0; //Conceptos de remisiones
        private string sNotas = string.Empty;
        private string _sUsuario = string.Empty;

        public long IIdRemision { get => _iIdRemision; set => _iIdRemision = value; }
        public int IIdContrato { get => iIdContrato; set => iIdContrato = value; }
        public string SMatricula { get => sMatricula; set => sMatricula = value; }
        public string SCargo { get => sCargo; set => sCargo = value; }
        public string SAbono { get => sAbono; set => sAbono = value; }
        public int IIdMotivo { get => iIdMotivo; set => iIdMotivo = value; }
        public string SNotas { get => sNotas; set => sNotas = value; }
        public string SUsuario { get => _sUsuario; set => _sUsuario = value; }
    }



    [Serializable, Bindable(BindableSupport.Yes)]
    public class IntercambioRemision : BaseObjeto
    {
        //Intercambios
        private string _sMatriculaInter = string.Empty;
        private decimal _dTarifaNalInter = 0;
        private decimal _dTarifaIntInter = 0;
        private decimal _dGaolnesInter = 0;
        private decimal _dCostoDirNalInter = 0;
        private decimal _dCostoDirIntInter = 0;

        public string sMatriculaInter { get { return _sMatriculaInter; } set { _sMatriculaInter = value; } }
        public decimal dTarifaNalInter { get { return _dTarifaNalInter; } set { _dTarifaNalInter = value; } }
        public decimal dTarifaIntInter { get { return _dTarifaIntInter; } set { _dTarifaIntInter = value; } }
        public decimal dGaolnesInter { get { return _dGaolnesInter; } set { _dGaolnesInter = value; } }
        public decimal dCostoDirNalInter { get { return _dCostoDirNalInter; } set { _dCostoDirNalInter = value; } }
        public decimal dCostoDirIntInter { get { return _dCostoDirIntInter; } set { _dCostoDirIntInter = value; } }
    }


    [Serializable, Bindable(BindableSupport.Yes)]
    public class FactoresTramos : BaseObjeto
    {
        // --================= FACTORES APLICADOS =======================---
        private bool _bAplicoFactorEspecial = false;
        private bool _bAplicoIntercambio = false;
        private bool _bAplicaGiraEspera = false;
        private bool _bAplicaGiraHorario = false;
        private bool _bAplicaFactorFechaPico = false;
        private bool _bAplicaFactorVueloSimultaneo = false;

        private decimal _dFactorFactorEspecial = 0;
        private decimal _dFactorIntercambio = 0;
        private decimal _dFactorGiraEspera = 0;
        private decimal _dFactorGiraHorario = 0;
        private decimal _dFactorFechaPico = 0;
        private decimal _dFactorVloSimultaneo = 0;

        
        public bool bAplicoFactorEspecial { get { return _bAplicoFactorEspecial; } set { _bAplicoFactorEspecial = value; } }
        public bool bAplicoIntercambio { get { return _bAplicoIntercambio; } set { _bAplicoIntercambio = value; } }
        public bool bAplicaGiraEspera { get { return _bAplicaGiraEspera; } set { _bAplicaGiraEspera = value; } }
        public bool bAplicaGiraHorario { get { return _bAplicaGiraHorario; } set { _bAplicaGiraHorario = value; } }
        public bool bAplicaFactorFechaPico { get { return _bAplicaFactorFechaPico; } set { _bAplicaFactorFechaPico = value; } }
        public bool bAplicaFactorVueloSimultaneo { get { return _bAplicaFactorVueloSimultaneo; } set { _bAplicaFactorVueloSimultaneo = value; } }

        public decimal dFactorFactorEspecial { get { return _dFactorFactorEspecial; } set { _dFactorFactorEspecial = value; } }
        public decimal dFactorIntercambio { get { return _dFactorIntercambio; } set { _dFactorIntercambio = value; } }
        public decimal dFactorGiraEspera { get { return _dFactorGiraEspera; } set { _dFactorGiraEspera = value; } }
        public decimal dFactorGiraHorario { get { return _dFactorGiraHorario; } set { _dFactorGiraHorario = value; } }
        public decimal dFactorFechaPico { get { return _dFactorFechaPico; } set { _dFactorFechaPico = value; } }
        public decimal dFactorVloSimultaneo { get { return _dFactorVloSimultaneo; } set { _dFactorVloSimultaneo = value; } }
    }

}