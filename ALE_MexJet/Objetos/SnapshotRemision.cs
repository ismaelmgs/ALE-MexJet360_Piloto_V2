using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ALE_MexJet.Objetos
{
    [Serializable, Bindable(BindableSupport.Yes)]
    public class SnapshotRemision
    {
        private DatosRemision _oDatosRem = new DatosRemision();
        private List<FactoresTramoSnapshot> _oFactoresTramos = new List<FactoresTramoSnapshot>();

        public DatosRemision oDatosRem { get => _oDatosRem; set => _oDatosRem = value; }
        public List<FactoresTramoSnapshot> oFactoresTramos { get => _oFactoresTramos; set => _oFactoresTramos = value; }
        
    }
 
    [Serializable]
    public class FactoresTramoSnapshot
    {
        private int _iNoTramo = 0;
        public long _iIdRemision = 0;
        public string sMatricula { set; get; }
        public string sOrigen { set; get; }
        public string sDestino { set; get; }
        public string sTiempoOriginal { set; get; }
        public string sTiempoFinal { set; get; }

        private double _dFactorEspeciaRem = 0;
        private double _dAplicaFactorTramoNacional = 0;
        private double _dAplicaFactorTramoInternacional = 0;
        private double _dAplicoIntercambio = 0;
        private double _dAplicaGiraEspera = 0;
        private double _dAplicaGiraHorario = 0;
        private double _dAplicaFactorFechaPico = 0;
        private double _dAplicaFactorVueloSimultaneo = 0;

        private string _sFactorEspeciaRem;
        private string _sAplicaFactorTramoNacional;
        private string _sAplicaFactorTramoInternacional;
        private string _sAplicoIntercambio;
        private string _sAplicaGiraEspera;
        private string _sAplicaGiraHorario;
        private string _sAplicaFactorFechaPico;
        private string _sAplicaFactorVueloSimultaneo;

        public int iNoTramo { get => _iNoTramo; set => _iNoTramo = value; }
        public long iIdRemision { get { return _iIdRemision; } set { _iIdRemision = value; } }
        public double dFactorEspeciaRem { get => _dFactorEspeciaRem; set => _dFactorEspeciaRem = value; }
        public double dAplicaFactorTramoNacional { get => _dAplicaFactorTramoNacional; set => _dAplicaFactorTramoNacional = value; }
        public double dAplicaFactorTramoInternacional { get => _dAplicaFactorTramoInternacional; set => _dAplicaFactorTramoInternacional = value; }
        public double dAplicoIntercambio { get => _dAplicoIntercambio; set => _dAplicoIntercambio = value; }
        public double dAplicaGiraEspera { get => _dAplicaGiraEspera; set => _dAplicaGiraEspera = value; }
        public double dAplicaGiraHorario { get => _dAplicaGiraHorario; set => _dAplicaGiraHorario = value; }
        public double dAplicaFactorFechaPico { get => _dAplicaFactorFechaPico; set => _dAplicaFactorFechaPico = value; }
        public double dAplicaFactorVueloSimultaneo { get => _dAplicaFactorVueloSimultaneo; set => _dAplicaFactorVueloSimultaneo = value; }

        public string sFactorEspeciaRem { get => _sFactorEspeciaRem; set => _sFactorEspeciaRem = value; }
        public string sAplicaFactorTramoNacional { get => _sAplicaFactorTramoNacional; set => _sAplicaFactorTramoNacional = value; }
        public string sAplicaFactorTramoInternacional { get => _sAplicaFactorTramoInternacional; set => _sAplicaFactorTramoInternacional = value; }
        public string sAplicoIntercambio { get => _sAplicoIntercambio; set => _sAplicoIntercambio = value; }
        public string sAplicaGiraEspera { get => _sAplicaGiraEspera; set => _sAplicaGiraEspera = value; }
        public string sAplicaGiraHorario { get => _sAplicaGiraHorario; set => _sAplicaGiraHorario = value; }
        public string sAplicaFactorFechaPico { get => _sAplicaFactorFechaPico; set => _sAplicaFactorFechaPico = value; }
        public string sAplicaFactorVueloSimultaneo { get => _sAplicaFactorVueloSimultaneo; set => _sAplicaFactorVueloSimultaneo = value; }
    }
}