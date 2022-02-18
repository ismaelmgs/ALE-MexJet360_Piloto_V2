using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ALE_MexJet.Objetos
{
    [Serializable, Bindable(BindableSupport.Yes)]
    public class EstadoCuenta : BaseObjeto
    {
        private string _IdCliente = "0";
        private string _IdContrato = string.Empty;
        private int _status = 1;
        private string _NombreCliente = string.Empty;
        private string _FechaIni = string.Empty;
        private string _FechaFin = string.Empty;

        [Display(AutoGenerateField = false), ScaffoldColumn(false)]

        public string NombreCliente { get { return _NombreCliente; } set { _NombreCliente = value; } }

        public int status { get { return _status; } set { _status = value; } }

        public string IdCliente { get { return _IdCliente; } set { _IdCliente = value; } }

        public string IdContrato { get { return _IdContrato; } set { _IdContrato = value; } }

        public string FechaIni { get { return _FechaIni; } set { _FechaIni = value; } }

        public string FechaFin { get { return _FechaFin; } set { _FechaFin = value; } }
        
    }


    public class ReporteEdoCuent : BaseObjeto
    {
        /// <summary>
        /// SALDOS INICIALES
        /// </summary>
        private string _sClaveContrato = string.Empty;

        // Saldos Pesos
        private decimal _dAnticipoInicialSP = 0;
        private decimal _dQuintoAnioSP = 0;
        private decimal _dFijoAnualSP = 0;
        private decimal _dIvaSP = 0;
        private decimal _dGastosSP = 0;
        private decimal _dServiciosConCargoSP = 0;
        private decimal _dVuelosSP = 0;
        
        //Saldos Dolares
        private decimal _dAnticipoInicialSD = 0;
        private decimal _dQuintoAnioSD = 0;
        private decimal _dFijoAnualSD = 0;
        private decimal _dIvaSD = 0;
        private decimal _dGastosSD = 0;
        private decimal _dServiciosConCargoSD = 0;
        private decimal _dVuelosSD = 0;

        // Pagos Pesos
        private decimal _dAnticipoInicialPP = 0;
        private decimal _dQuintoAnioPP = 0;
        private decimal _dFijoAnualPP = 0;
        private decimal _dIvaPP = 0;
        private decimal _dGastosPP = 0;
        private decimal _dServiciosConCargoPP = 0;
        private decimal _dVuelosPP = 0;

        //Pagos Dolares
        private decimal _dAnticipoInicialPD = 0;
        private decimal _dQuintoAnioPD = 0;
        private decimal _dFijoAnualPD = 0;
        private decimal _dIvaPD = 0;
        private decimal _dGastosPD = 0;
        private decimal _dServiciosConCargoPD = 0;
        private decimal _dVuelosPD = 0;

        

        /// <summary>
        /// MOVIMIENTOS PREVIOS AL REPORTE (SALDOS PREVIOS PESOS)
        /// </summary>
        private decimal _dAnticipoInicialSPP = 0;
        private decimal _dQuintoAnioSPP = 0;
        private decimal _dFijoAnualSPP = 0;
        private decimal _dIvaSPP = 0;
        private decimal _dGastosSPP = 0;
        private decimal _dServiciosConCargoSPP = 0;
        private decimal _dVuelosSPP = 0;

        //Saldos Dolares
        private decimal _dAnticipoInicialSPD = 0;
        private decimal _dQuintoAnioSPD = 0;
        private decimal _dFijoAnualSPD = 0;
        private decimal _dIvaSPD = 0;
        private decimal _dGastosSPD = 0;
        private decimal _dServiciosConCargoSPD = 0;
        private decimal _dVuelosSPD = 0;

        // Pagos Pesos
        private decimal _dAnticipoInicialPPP = 0;
        private decimal _dQuintoAnioPPP = 0;
        private decimal _dFijoAnualPPP = 0;
        private decimal _dIvaPPP = 0;
        private decimal _dGastosPPP = 0;
        private decimal _dServiciosConCargoPPP = 0;
        private decimal _dVuelosPPP = 0;

        //Pagos Dolares
        private decimal _dAnticipoInicialPPD = 0;
        private decimal _dQuintoAnioPPD = 0;
        private decimal _dFijoAnualPPD = 0;
        private decimal _dIvaPPD = 0;
        private decimal _dGastosPPD = 0;
        private decimal _dServiciosConCargoPPD = 0;
        private decimal _dVuelosPPD = 0;



        /// GASTOS INTERNOS DEL PERIODO
        /// Pesos
        private decimal _dAnticipoInicialGIPP = 0;
        private decimal _dQuintoAnioGIPP = 0;
        private decimal _dFijoAnualGIPP = 0;
        private decimal _dIvaGIPP = 0;
        private decimal _dGastosGIPP = 0;
        private decimal _dServiciosConCargoGIPP = 0;
        private decimal _dVuelosGIPP = 0;

        // Dolares
        private decimal _dAnticipoInicialGIPD = 0;
        private decimal _dQuintoAnioGIPD = 0;
        private decimal _dFijoAnualGIPD = 0;
        private decimal _dIvaGIPD = 0;
        private decimal _dGastosGIPD = 0;
        private decimal _dServiciosConCargoGIPD = 0;
        private decimal _dVuelosGIPD = 0;


        /// GASTOS INTERNOS ANTERIORES
        /// Pesos
        private decimal _dAnticipoInicialGIAP = 0;
        private decimal _dQuintoAnioGIAP = 0;
        private decimal _dFijoAnualGIAP = 0;
        private decimal _dIvaGIAP = 0;
        private decimal _dGastosGIAP = 0;
        private decimal _dServiciosConCargoGIAP = 0;
        private decimal _dVuelosGIAP = 0;

        // Dolares
        private decimal _dAnticipoInicialGIAD = 0;
        private decimal _dQuintoAnioGIAD = 0;
        private decimal _dFijoAnualGIAD = 0;
        private decimal _dIvaGIAD = 0;
        private decimal _dGastosGIAD = 0;
        private decimal _dServiciosConCargoGIAD = 0;
        private decimal _dVuelosGIAD = 0;



        // DATOS GENERALES
        public string sClaveContrato { get { return _sClaveContrato; } set { _sClaveContrato = value; } }

        /// <summary>
        /// SALDOS INICIALES
        /// </summary>
        
        // SALDOS PESOS
        public decimal dAnticipoInicialSP { get { return _dAnticipoInicialSP; } set { _dAnticipoInicialSP = value; } }
        public decimal dQuintoAnioSP { get { return _dQuintoAnioSP; } set { _dQuintoAnioSP = value; } }
        public decimal dFijoAnualSP { get { return _dFijoAnualSP; } set { _dFijoAnualSP = value; } }
        public decimal dIvaSP { get { return _dIvaSP; } set { _dIvaSP = value; } }
        public decimal dGastosSP { get { return _dGastosSP; } set { _dGastosSP = value; } }
        public decimal dServiciosConCargoSP { get { return _dServiciosConCargoSP; } set { _dServiciosConCargoSP = value; } }
        public decimal dVuelosSP { get { return _dVuelosSP; } set { _dVuelosSP = value; } }

        // SALDOS DOLARES
        public decimal dAnticipoInicialSD { get { return _dAnticipoInicialSD; } set { _dAnticipoInicialSD = value; } }
        public decimal dQuintoAnioSD { get { return _dQuintoAnioSD; } set { _dQuintoAnioSD = value; } }
        public decimal dFijoAnualSD { get { return _dFijoAnualSD; } set { _dFijoAnualSD = value; } }
        public decimal dIvaSD { get { return _dIvaSD; } set { _dIvaSD = value; } }
        public decimal dGastosSD { get { return _dGastosSD; } set { _dGastosSD = value; } }
        public decimal dServiciosConCargoSD { get { return _dServiciosConCargoSD; } set { _dServiciosConCargoSD = value; } }
        public decimal dVuelosSD { get { return _dVuelosSD; } set { _dVuelosSD = value; } }

        // PAGOS PESOS
        public decimal dAnticipoInicialPP { get { return _dAnticipoInicialPP; } set { _dAnticipoInicialPP = value; } }
        public decimal dQuintoAnioPP { get { return _dQuintoAnioPP; } set { _dQuintoAnioPP = value; } }
        public decimal dFijoAnualPP { get { return _dFijoAnualPP; } set { _dFijoAnualPP = value; } }
        public decimal dIvaPP { get { return _dIvaPP; } set { _dIvaPP = value; } }
        public decimal dGastosPP { get { return _dGastosPP; } set { _dGastosPP = value; } }
        public decimal dServiciosConCargoPP { get { return _dServiciosConCargoPP; } set { _dServiciosConCargoPP = value; } }
        public decimal dVuelosPP { get { return _dVuelosPP; } set { _dVuelosPP = value; } }

        // PAGOS DOLARES
        public decimal dAnticipoInicialPD { get { return _dAnticipoInicialPD; } set { _dAnticipoInicialPD = value; } }
        public decimal dQuintoAnioPD { get { return _dQuintoAnioPD; } set { _dQuintoAnioPD = value; } }
        public decimal dFijoAnualPD { get { return _dFijoAnualPD; } set { _dFijoAnualPD = value; } }
        public decimal dIvaPD { get { return _dIvaPD; } set { _dIvaPD = value; } }
        public decimal dGastosPD { get { return _dGastosPD; } set { _dGastosPD = value; } } 
        public decimal dServiciosConCargoPD { get { return _dServiciosConCargoPD; } set { _dServiciosConCargoPD = value; } }
        public decimal dVuelosPD { get { return _dVuelosPD; } set { _dVuelosPD = value; } }



        /// 
        /// MOVIMIENTOS PREVIOS AL REPORTE Y MAYORES AL 1RO DE ENERO
        ///
        // SALDOS PREVIOS PESOS
        public decimal dAnticipoInicialSPP { get { return _dAnticipoInicialSPP; } set { _dAnticipoInicialSPP = value; } }
        public decimal dQuintoAnioSPP { get { return _dQuintoAnioSPP; } set { _dQuintoAnioSPP = value; } }
        public decimal dFijoAnualSPP { get { return _dFijoAnualSPP; } set { _dFijoAnualSPP = value; } }
        public decimal dIvaSPP { get { return _dIvaSPP; } set { _dIvaSPP = value; } }
        public decimal dGastosSPP { get { return _dGastosSPP; } set { _dGastosSPP = value; } }
        public decimal dServiciosConCargoSPP { get { return _dServiciosConCargoSPP; } set { _dServiciosConCargoSPP = value; } }
        public decimal dVuelosSPP { get { return _dVuelosSPP; } set { _dVuelosSPP = value; } }

        // SALDOS PREVIOS DOLARES
        public decimal dAnticipoInicialSPD { get { return _dAnticipoInicialSPD; } set { _dAnticipoInicialSPD = value; } }
        public decimal dQuintoAnioSPD { get { return _dQuintoAnioSPD; } set { _dQuintoAnioSPD = value; } }
        public decimal dFijoAnualSPD { get { return _dFijoAnualSPD; } set { _dFijoAnualSPD = value; } }
        public decimal dIvaSPD { get { return _dIvaSPD; } set { _dIvaSPD = value; } }
        public decimal dGastosSPD { get { return _dGastosSPD; } set { _dGastosSPD = value; } }
        public decimal dServiciosConCargoSPD { get { return _dServiciosConCargoSPD; } set { _dServiciosConCargoSPD = value; } }
        public decimal dVuelosSPD { get { return _dVuelosSPD; } set { _dVuelosSPD = value; } }

        // PAGOS PREVIOS DOLARES
        public decimal dAnticipoInicialPPP { get { return _dAnticipoInicialPPP; } set { _dAnticipoInicialPPP = value; } }
        public decimal dQuintoAnioPPP { get { return _dQuintoAnioPPP; } set { _dQuintoAnioPPP = value; } }
        public decimal dFijoAnualPPP { get { return _dFijoAnualPPP; } set { _dFijoAnualPPP = value; } }
        public decimal dIvaPPP { get { return _dIvaPPP; } set { _dIvaPPP = value; } }
        public decimal dGastosPPP { get { return _dGastosPPP; } set { _dGastosPPP = value; } }
        public decimal dServiciosConCargoPPP { get { return _dServiciosConCargoPPP; } set { _dServiciosConCargoPPP = value; } }
        public decimal dVuelosPPP { get { return _dVuelosPPP; } set { _dVuelosPPP = value; } }

        // PAGOS PREVIOS DOLARES
        public decimal dAnticipoInicialPPD { get { return _dAnticipoInicialPPD; } set { _dAnticipoInicialPPD = value; } }
        public decimal dQuintoAnioPPD { get { return _dQuintoAnioPPD; } set { _dQuintoAnioPPD = value; } }
        public decimal dFijoAnualPPD { get { return _dFijoAnualPPD; } set { _dFijoAnualPPD = value; } }
        public decimal dIvaPPD { get { return _dIvaPPD; } set { _dIvaPPD = value; } }
        public decimal dGastosPPD { get { return _dGastosPPD; } set { _dGastosPPD = value; } }
        public decimal dServiciosConCargoPPD { get { return _dServiciosConCargoPPD; } set { _dServiciosConCargoPPD = value; } }
        public decimal dVuelosPPD { get { return _dVuelosPPD; } set { _dVuelosPPD = value; } }


        /// 
        /// GASTOS INTERNOS DEL PERIODO DE CONSULTA
        ///

        // Pesos
        public decimal dAnticipoInicialGIPP { get { return _dAnticipoInicialGIPP; } set { _dAnticipoInicialGIPP = value; } }
        public decimal dQuintoAnioGIPP { get { return _dQuintoAnioGIPP; } set { _dQuintoAnioGIPP = value; } }
        public decimal dFijoAnualGIPP { get { return _dFijoAnualGIPP; } set { _dFijoAnualGIPP = value; } }
        public decimal dIvaGIPP { get { return _dIvaGIPP; } set { _dIvaGIPP = value; } }
        public decimal dGastosGIPP { get { return _dGastosGIPP; } set { _dGastosGIPP = value; } }
        public decimal dServiciosConCargoGIPP { get { return _dServiciosConCargoGIPP; } set { _dServiciosConCargoGIPP = value; } }
        public decimal dVuelosGIPP { get { return _dVuelosGIPP; } set { _dVuelosGIPP = value; } }

        // Dolares
        public decimal dAnticipoInicialGIPD { get { return _dAnticipoInicialGIPD; } set { _dAnticipoInicialGIPD = value; } }
        public decimal dQuintoAnioGIPD { get { return _dQuintoAnioGIPD; } set { _dQuintoAnioGIPD = value; } }
        public decimal dFijoAnualGIPD { get { return _dFijoAnualGIPD; } set { _dFijoAnualGIPD = value; } }
        public decimal dIvaGIPD { get { return _dIvaGIPD; } set { _dIvaGIPD = value; } }
        public decimal dGastosGIPD { get { return _dGastosGIPD; } set { _dGastosGIPD = value; } }
        public decimal dServiciosConCargoGIPD { get { return _dServiciosConCargoGIPD; } set { _dServiciosConCargoGIPD = value; } }
        public decimal dVuelosGIPD { get { return _dVuelosGIPD; } set { _dVuelosGIPD = value; } }


        /// 
        /// GASTOS INTERNOS PREVIOS AL REPORTE Y MAYORES AL 1RO DE ENERO
        ///

        // Pesos
        public decimal dAnticipoInicialGIAP { get { return _dAnticipoInicialGIAP; } set { _dAnticipoInicialGIAP = value; } }
        public decimal dQuintoAnioGIAP { get { return _dQuintoAnioGIAP; } set { _dQuintoAnioGIAP = value; } }
        public decimal dFijoAnualGIAP { get { return _dFijoAnualGIAP; } set { _dFijoAnualGIAP = value; } }
        public decimal dIvaGIAP { get { return _dIvaGIAP; } set { _dIvaGIAP = value; } }
        public decimal dGastosGIAP { get { return _dGastosGIAP; } set { _dGastosGIAP = value; } }
        public decimal dServiciosConCargoGIAP { get { return _dServiciosConCargoGIAP; } set { _dServiciosConCargoGIAP = value; } }
        public decimal dVuelosGIAP { get { return _dVuelosGIAP; } set { _dVuelosGIAP = value; } }

        // Dolares
        public decimal dAnticipoInicialGIAD { get { return _dAnticipoInicialGIAD; } set { _dAnticipoInicialGIAD = value; } }
        public decimal dQuintoAnioGIAD { get { return _dQuintoAnioGIAD; } set { _dQuintoAnioGIAD = value; } }
        public decimal dFijoAnualGIAD { get { return _dFijoAnualGIAD; } set { _dFijoAnualGIAD = value; } }
        public decimal dIvaGIAD { get { return _dIvaGIAD; } set { _dIvaGIAD = value; } }
        public decimal dGastosGIAD { get { return _dGastosGIAD; } set { _dGastosGIAD = value; } }
        public decimal dServiciosConCargoGIAD { get { return _dServiciosConCargoGIAD; } set { _dServiciosConCargoGIAD = value; } }
        public decimal dVuelosGIAD { get { return _dVuelosGIAD; } set { _dVuelosGIAD = value; } }



    }
}