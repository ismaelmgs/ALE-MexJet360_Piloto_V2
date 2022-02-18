using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ALE_MexJet.Objetos
{
    [Serializable, Bindable(BindableSupport.Yes)]
    public class Contrato_CobrosDescuentos
    {
        private int _iIdContrato = -1;
        private int _iFerrysConCargo = 0;
        private bool _bAplicaEsperaLibre = false;
        private decimal _dHorasVuelo = -1;
        private decimal _dFactorHorasVuelo = 0m;
        private bool _bPernoctaNal = false;
        private bool _bPernoctaInt = false;
        private decimal _dPernoctaFactorConversionNal = 0m;
        private decimal _dPernoctaFactorConversionInt = 0m;
        private decimal _dNumeroPernoctasLibreAnual = 0m;
        private bool _bPernoctasDescuento = false;
        private bool _bPernoctasCobro = false;
        private bool _bTiempoEsperaNal = false;
        private bool _bTiempoEsperaInt = false;
        private decimal _dTiempoEsperaFactorConversionNal = 0m;
        private decimal _dTiempoEsperaFactorConversionInt = 0m;
        private int _iTiempoFatura = 0;
        private decimal _dMinutos = 0m;
        private List<int> _lstIdServiciosConCargo = new List<int>();
        private bool _bAplicaTramos = true;


        private string _sNotas = string.Empty;
        public string sNotas { get { return _sNotas; } set { _sNotas = value; } }

        public int iIdContrato { get { return _iIdContrato; } set { _iIdContrato = value; } }


        public int iFerrysConCargo { get { return _iFerrysConCargo; } set { _iFerrysConCargo = value; } }
        public bool bAplicaEsperaLibre { get { return _bAplicaEsperaLibre; } set { _bAplicaEsperaLibre = value; } }
        public decimal dHorasVuelo { get { return _dHorasVuelo; } set { _dHorasVuelo = value; } }
        public decimal dFactorHorasVuelo { get { return _dFactorHorasVuelo; } set { _dFactorHorasVuelo = value; } }
        public bool bPernoctaNal { get { return _bPernoctaNal; } set { _bPernoctaNal = value; } }
        public bool bPernoctaInt { get { return _bPernoctaInt; } set { _bPernoctaInt = value; } }
        public decimal dPernoctaFactorConversionNal { get { return _dPernoctaFactorConversionNal; } set { _dPernoctaFactorConversionNal = value; } }
        public decimal dPernoctaFactorConversionInt { get { return _dPernoctaFactorConversionInt; } set { _dPernoctaFactorConversionInt = value; } }
        public decimal dNumeroPernoctasLibreAnual { get { return _dNumeroPernoctasLibreAnual; } set { _dNumeroPernoctasLibreAnual = value; } }
        public bool bPernoctasDescuento { get { return _bPernoctasDescuento; } set { _bPernoctasDescuento = value; } }
        public bool bPernoctasCobro { get { return _bPernoctasCobro; } set { _bPernoctasCobro = value; } }

        public bool bTiempoEsperaNal { get { return _bTiempoEsperaNal; } set { _bTiempoEsperaNal = value; } }
        public bool bTiempoEsperaInt { get { return _bTiempoEsperaInt; } set { _bTiempoEsperaInt = value; } }
        public decimal dTiempoEsperaFactorConversionNal { get { return _dTiempoEsperaFactorConversionNal; } set { _dTiempoEsperaFactorConversionNal = value; } }
        public decimal dTiempoEsperaFactorConversionInt { get { return _dTiempoEsperaFactorConversionInt; } set { _dTiempoEsperaFactorConversionInt = value; } }

        public int iTiempoFatura { get { return _iTiempoFatura; } set { _iTiempoFatura = value; } }
        public decimal dMinutos { get { return _dMinutos; } set { _dMinutos = value; } }

        public List<int> lstIdServiciosConCargo { get { return _lstIdServiciosConCargo; } set { _lstIdServiciosConCargo = value; } }

        public bool bAplicaTramos { get { return _bAplicaTramos; } set { _bAplicaTramos = value; } }
    }
}