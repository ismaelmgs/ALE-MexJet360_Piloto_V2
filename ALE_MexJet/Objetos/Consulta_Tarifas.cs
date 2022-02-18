using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ALE_MexJet.Objetos
{
    public class Consulta_Tarifas
    {
        private int _iIdContrato = -1;
        private int _iIdCliente = -1;
        private int _iIdPaquete = -1;
        private string _sDesPaquete = string.Empty;
        private int _iIdGrupoModelo = -1;
        private string _sDesGrupoModelo = string.Empty;
        private decimal _dCostoDirNal = 0m;
        private decimal _dCostoDirInt = 0m;
        private int _iSeCobraCombustibleV = -1;
        private bool _bCalculoPrecioCombuV = true;
        private decimal _dConsumoGalonesHrV = 0m;
        private decimal _dTarifaVueloNal = 0m;
        private decimal _dTarifaVueloInt = 0m;
        private bool _bCombustible = true;
        private decimal _dTiempoEsperaFijaNal = 0m;
        private decimal _dTiempoEsperaFijaInt = 0m;     
        private decimal _dPernoctasFijaNal = 0m;
        private decimal _dPernoctasFijaInt = 0m;
      

        /////////////////
        public int iIdContrato { get { return _iIdContrato; } set { _iIdContrato = value; } }
        public decimal dCostoDirNal { get { return _dCostoDirNal; } set { _dCostoDirNal = value; } }
        public decimal dCostoDirInt { get { return _dCostoDirInt; } set { _dCostoDirInt = value; } }
        public bool bCombustible { get { return _bCombustible; } set { _bCombustible = value; } }
        public int iIdCliente { get { return _iIdCliente; } set { _iIdCliente = value; } }
        public int iIdPaquete { get { return _iIdPaquete; } set { _iIdPaquete = value; } }
        public string sDesPaquete { get { return _sDesPaquete; } set { _sDesPaquete = value; } }

        public int iIdGrupoModelo { get { return _iIdGrupoModelo; } set { _iIdGrupoModelo = value; } }
        public decimal dTiempoEsperaFijaNal { get { return _dTiempoEsperaFijaNal; } set { _dTiempoEsperaFijaNal = value; } }
        public decimal dTiempoEsperaFijaInt { get { return _dTiempoEsperaFijaInt; } set { _dTiempoEsperaFijaInt = value; } }
        public string sDesGrupoModelo { get { return _sDesGrupoModelo; } set { _sDesGrupoModelo = value; } }

        public decimal dPernoctasFijaNal { get { return _dPernoctasFijaNal; } set { _dPernoctasFijaNal = value; } }
        public decimal dPernoctasFijaInt { get { return _dPernoctasFijaInt; } set { _dPernoctasFijaInt = value; } }
        public decimal dTarifaVueloNal { get { return _dTarifaVueloNal; } set { _dTarifaVueloNal = value; } }
        public decimal dTarifaVueloInt { get { return _dTarifaVueloInt; } set { _dTarifaVueloInt = value; } }

        public bool bCalculoPrecioCombuV { get { return _bCalculoPrecioCombuV; } set { _bCalculoPrecioCombuV = value; } }
        public int iSeCobraCombustibleV { get { return _iSeCobraCombustibleV; } set { _iSeCobraCombustibleV = value; } }
        public decimal dConsumoGalonesHrV { get { return _dConsumoGalonesHrV; } set { _dConsumoGalonesHrV = value; } }
       

    }
}
  