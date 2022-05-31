using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ALE_MexJet.Objetos
{
    [Serializable, Bindable(BindableSupport.Yes)]
    public class Contrato_Tarifas
    {
        private int _iIdContrato = -1;
        private decimal _dCostoDirNal = 0m;
        private decimal _dCostoDirInt = 0m;
        private bool _bCombustible = true;
        private int _iTipoCalculo = -1;
        private decimal _dConsumoGalones = 0m;
        private decimal _dFactorTramosNal = 0m;
        private decimal _dFactorTramosInt = 0m;

        private int _bAplicaFactorCombustible = 0;

        private bool _bPrecioInternacionalEspecial = false;

        private bool _bCobraTiempoEspera = true;
        private decimal _dTiempoEsperaFijaNal = 0m;
        private decimal _dTiempoEsperaFijaInt = 0m;
        private decimal _dTiempoEsperaVarNal = 0m;
        private decimal _dTiempoEsperaVarInt = 0m;

        private bool _bCobraPernoctas = true;
        private decimal _dPernoctasFijaNal = 0m;
        private decimal _dPernoctasFijaInt = 0m;
        private decimal _dPernoctasVarNal = 0m;
        private decimal _dPernoctasVarInt = 0m;

        private int _iCDNBaseInflacion = -1;
        private decimal _dCDNPorcentaje = 0m;
        private decimal _dCDNPuntos = 0m;
        private decimal _dCDNTopeMAximo = 0m;

        private int _iCDIBaseInflacion = -1;
        private decimal _dCDIPorcentaje = 0m;
        private decimal _dCDIPuntos = 0m;
        private decimal _dCDITopeMAximo = 0m;

        private int _iFABaseInflacion = -1;
        private decimal _dFAPorcentaje = 0m;
        private decimal _dFAPuntos = 0m;
        private decimal _dFATopeMAximo = 0m;

        private string _sNotas = string.Empty;
        private int _iAplicaIncremento;
        

        /////////////////
        public int iIdContrato { get { return _iIdContrato; } set { _iIdContrato = value; } }
        public decimal dCostoDirNal { get { return _dCostoDirNal; } set { _dCostoDirNal = value; } }
        public decimal dCostoDirInt { get { return _dCostoDirInt; } set { _dCostoDirInt = value; } }
        public bool bCombustible { get { return _bCombustible; } set { _bCombustible = value; } }
        public int iTipoCalculo { get { return _iTipoCalculo; } set { _iTipoCalculo = value; } }
        public decimal dConsumoGalones { get { return _dConsumoGalones; } set { _dConsumoGalones = value; } }
        public decimal dFactorTramosNal { get { return _dFactorTramosNal; } set { _dFactorTramosNal = value; } }
        public decimal dFactorTramosInt { get { return _dFactorTramosInt; } set { _dFactorTramosInt = value; } }
        public int bAplicaFactorCombustible { get { return _bAplicaFactorCombustible; } set { _bAplicaFactorCombustible = value; } }
        public bool bPrecioInternacionalEspecial { get { return _bPrecioInternacionalEspecial; } set { _bPrecioInternacionalEspecial = value; } }

        public bool bCobraTiempoEspera { get { return _bCobraTiempoEspera; } set { _bCobraTiempoEspera = value; } }
        public decimal dTiempoEsperaFijaNal { get { return _dTiempoEsperaFijaNal; } set { _dTiempoEsperaFijaNal = value; } }
        public decimal dTiempoEsperaFijaInt { get { return _dTiempoEsperaFijaInt; } set { _dTiempoEsperaFijaInt = value; } }
        public decimal dTiempoEsperaVarNal { get { return _dTiempoEsperaVarNal; } set { _dTiempoEsperaVarNal = value; } }
        public decimal dTiempoEsperaVarInt { get { return _dTiempoEsperaVarInt; } set { _dTiempoEsperaVarInt = value; } }

        public bool bCobraPernoctas { get { return _bCobraPernoctas; } set { _bCobraPernoctas = value; } }
        public decimal dPernoctasFijaNal { get { return _dPernoctasFijaNal; } set { _dPernoctasFijaNal = value; } }
        public decimal dPernoctasFijaInt { get { return _dPernoctasFijaInt; } set { _dPernoctasFijaInt = value; } }
        public decimal dPernoctasVarNal { get { return _dPernoctasVarNal; } set { _dPernoctasVarNal = value; } }
        public decimal dPernoctasVarInt { get { return _dPernoctasVarInt; } set { _dPernoctasVarInt = value; } }

        public int iCDNBaseInflacion { get { return _iCDNBaseInflacion; } set { _iCDNBaseInflacion = value; } }
        public decimal dCDNPorcentaje { get { return _dCDNPorcentaje; } set { _dCDNPorcentaje = value; } }
        public decimal dCDNPuntos { get { return _dCDNPuntos; } set { _dCDNPuntos = value; } }
        public decimal dCDNTopeMAximo { get { return _dCDNTopeMAximo; } set { _dCDNTopeMAximo = value; } }

        public int iCDIBaseInflacion { get { return _iCDIBaseInflacion; } set { _iCDIBaseInflacion = value; } }
        public decimal dCDIPorcentaje { get { return _dCDIPorcentaje; } set { _dCDIPorcentaje = value; } }
        public decimal dCDIPuntos { get { return _dCDIPuntos; } set { _dCDIPuntos = value; } }
        public decimal dCDITopeMAximo { get { return _dCDITopeMAximo; } set { _dCDITopeMAximo = value; } }

        public int iFABaseInflacion { get { return _iFABaseInflacion; } set { _iFABaseInflacion = value; } }
        public decimal dFAPorcentaje { get { return _dFAPorcentaje; } set { _dFAPorcentaje = value; } }
        public decimal dFAPuntos { get { return _dFAPuntos; } set { _dFAPuntos = value; } }
        public decimal dFATopeMAximo { get { return _dFATopeMAximo; } set { _dFATopeMAximo = value; } }
        public string sNotas { get { return _sNotas; } set { _sNotas = value; } }
        public int iAplicaIncremento { get { return _iAplicaIncremento; } set { _iAplicaIncremento = value; } }

    }
}