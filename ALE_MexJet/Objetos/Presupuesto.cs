using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;

namespace ALE_MexJet.Objetos
{
    [Serializable, Bindable(BindableSupport.Yes)]
    public class Presupuesto : BaseObjeto
    {
        private int _iIdPresupuesto = 0;
        private DateTime _dtFechaPresupuesto = new DateTime();
        private int _iDiasVigencia = 0;
        private int _iIdCliente = 0;
        private int _iIdContrato = 0;
        private string _sCompaniaImpresion = string.Empty;
        private int _iIdSolicitante = 0;
        private string _sNombreSolicitante = string.Empty;
        private string _sTelefono = string.Empty;
        private string _sEmail = string.Empty;
        private int _iIdGrupoModeloSol = 0;
        private int _iIdMonedaPresupuesto = 0;
        private decimal _dVueloNal = 0;
        private decimal _dVueloInt = 0;
        private decimal _dEsperaNal = 0;
        private decimal _dEsperaInt = 0;
        private decimal _dPernoctaNal = 0;
        private decimal _dPernoctaInt = 0;
        private int _iIdSiglasAeropuerto = 0;
        private bool _bFactorIntercambio = false;
        private bool _bFactorGiraEspera = false;
        private bool _bFactorGiraHorario = false;
        private bool _bFactorFechaPico = false;
        private bool _bFactorTramoNal = false;
        private bool _bFactorTramoInt = false;
        private string _sObservaciones = string.Empty;
        private int _iStatus = 0;
        private string _sUsuario = string.Empty;
        private string _sIP = string.Empty;
        private decimal _dTipoCambio = 0;
        private decimal _dFactorIntercambio = 0;
        private decimal _dFactorFechaPico = 0;
        private decimal _dGiraEspera = 0;
        private decimal _dGiraHorario = 0;
        private decimal _dFactorTramoNal = 0;
        private decimal _dFactorTramoInt = 0;

        
        private int _iIdSolicitudVuelo = 0;
        private decimal _dSubTotalSV = 0;
        private decimal _dSubTotalSC = 0;

        private DataTable _dtTramos = new DataTable();
        private DataTable _dtServicios = new DataTable();
        private DataTable _dtConceptos = new DataTable();
        


        public int iIdPresupuesto { get { return _iIdPresupuesto; } set { _iIdPresupuesto = value; } }
        public DateTime dtFechaPresupuesto { get { return _dtFechaPresupuesto; } set { _dtFechaPresupuesto = value; } }
        public int iDiasVigencia { get { return _iDiasVigencia; } set { _iDiasVigencia = value; } }
        public int iIdCliente { get { return _iIdCliente; } set { _iIdCliente = value; } }
        public int iIdContrato { get { return _iIdContrato; } set { _iIdContrato = value; } }
        public string sCompaniaImpresion { get { return _sCompaniaImpresion; } set { _sCompaniaImpresion = value; } }
        public int iIdSolicitante { get { return _iIdSolicitante; } set { _iIdSolicitante = value; } }
        public string sNombreSolicitante { get { return _sNombreSolicitante; } set { _sNombreSolicitante = value; } }
        public string sTelefono { get { return _sTelefono; } set { _sTelefono = value; } }
        public string sEmail { get { return _sEmail; } set { _sEmail = value; } }
        public int iIdGrupoModeloSol { get { return _iIdGrupoModeloSol; } set { _iIdGrupoModeloSol = value; } }
        public int iIdMonedaPresupuesto { get { return _iIdMonedaPresupuesto; } set { _iIdMonedaPresupuesto = value; } }
        public decimal dVueloNal { get { return _dVueloNal; } set { _dVueloNal = value; } }
        public decimal dVueloInt { get { return _dVueloInt; } set { _dVueloInt = value; } }
        public decimal dEsperaNal { get { return _dEsperaNal; } set { _dEsperaNal = value; } }
        public decimal dEsperaInt { get { return _dEsperaInt; } set { _dEsperaInt = value; } }
        public decimal dPernoctaNal { get { return _dPernoctaNal; } set { _dPernoctaNal = value; } }
        public decimal dPernoctaInt { get { return _dPernoctaInt; } set { _dPernoctaInt = value; } }
        public int iIdSiglasAeropuerto { get { return _iIdSiglasAeropuerto; } set { _iIdSiglasAeropuerto = value; } }
        public bool bFactorIntercambio { get { return _bFactorIntercambio; } set { _bFactorIntercambio = value; } }
        public bool bFactorGiraEspera { get { return _bFactorGiraEspera; } set { _bFactorGiraEspera = value; } }
        public bool bFactorGiraHorario { get { return _bFactorGiraHorario; } set { _bFactorGiraHorario = value; } }
        public bool bFactorFechaPico { get { return _bFactorFechaPico; } set { _bFactorFechaPico = value; } }
        public bool bFactorTramoNal { get { return _bFactorTramoNal; } set { _bFactorTramoNal = value; } }
        public bool bFactorTramoInt { get { return _bFactorTramoInt; } set { _bFactorTramoInt = value; } }
        public string sObservaciones { get { return _sObservaciones; } set { _sObservaciones = value; } }
        public int iStatus { get { return _iStatus; } set { _iStatus = value; } }        
        public string sUsuario { get { return _sUsuario; } set { _sUsuario = value; } }
        public string sIP { get { return _sIP; } set { _sIP = value; } }
        public decimal dTipoCambio { get { return _dTipoCambio; } set { _dTipoCambio = value; } }
        public decimal dFactorIntercambio { get { return _dFactorIntercambio; } set { _dFactorIntercambio = value; } }
        public decimal dFactorFechaPico { get { return _dFactorFechaPico; } set { _dFactorFechaPico = value; } }
        public decimal dGiraEspera { get { return _dGiraEspera; } set { _dGiraEspera = value; } }
        public decimal dGiraHorario { get { return _dGiraHorario; } set { _dGiraHorario = value; } }
        public decimal dFactorTramoNal { get { return _dFactorTramoNal; } set { _dFactorTramoNal = value; } }
        public decimal dFactorTramoInt { get { return _dFactorTramoInt; } set { _dFactorTramoInt = value; } }
        public int iIdSolicitudVuelo { get { return _iIdSolicitudVuelo; } set { _iIdSolicitudVuelo = value; } }
        public decimal dSubTotalSV { get { return _dSubTotalSV; } set { _dSubTotalSV = value; } }
        public decimal dSubTotalSC { get { return _dSubTotalSC; } set { _dSubTotalSC = value; } }


        public DataTable dtTramos { get { return _dtTramos; } set { _dtTramos = value; } }
        public DataTable dtServicios { get { return _dtServicios; } set { _dtServicios = value; } }
        public DataTable dtConceptos { get { return _dtConceptos; } set { _dtConceptos = value; } }

    }

}