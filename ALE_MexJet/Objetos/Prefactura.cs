using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ALE_MexJet.Objetos
{
    public class Prefactura
    {
        private int _iId = -1;
        private int _iIdCliente = -1;
        private int _iIdContrato = -1;
        private int _iIdMonedaVuelo = -1;
        private string _sClaveFacturanteVuelo = string.Empty;
        private int _iIdMonedaSCC = -1;
        private string _sClaveFacturanteSCC = string.Empty;
        private List<int> _lstIdRemisionesSeleccionadas = new List<int>();
        private string _sTipoContrato = string.Empty;
        private string _sCliente = string.Empty;
        private string _sContrato = string.Empty;


        // FECHAS FACTURA
        private string _sFechaSalida = string.Empty;
        private string _sFechaLlegada = string.Empty;

        // SERVICIOS DE VUELO
        private decimal _dSubDllV = 0m;
        private decimal _dDescDllV = 0;
        private decimal _dIVAV = 0m;
        private decimal _dIVADllV = 0m;
        private decimal _dTotalDllV = 0m;

        private decimal _dSubMXNV = 0m;
        private decimal _dDescMXNV = 0;
        private decimal _dIVAMXNV = 0m;
        private decimal _dTotalMXNV = 0m;

        private decimal _dSubHorasV = 0m;

        // SERVIOS CON CARGO
        private decimal _dSubDllC = 0m;
        private decimal _dIVAC = 0m;
        private decimal _dIVADllC = 0m;
        private decimal _dTotalDllC = 0m;

        private decimal _dSubMXNC = 0m;
        private decimal _dIVAMXNC = 0m;
        private decimal _dTotalMXNC = 0m;
        
        private decimal _dTipoCambio = 0m;
        private string _sSubHorasC = string.Empty;

        
        private bool _bCobroV = false;
        private bool _bCobroSCC = false;
        private bool _bCobroAmbos = false;
        private int _iIdFactura = -1;
        private int _iIdPorcentaje = -1;
        private string _sFacturaVuelo = string.Empty;
        private string _sFacturaSCC = string.Empty;


        // Sumarizado
        private decimal _dSubTotalSumarizadoDllc = 0;
        private decimal _dSubTotalSumarizadoMXNC = 0;
        
        private int _iStatus = 1;
        private string _sUsuarioCreacion = string.Empty;
        private DateTime _dtFechaCreacion = new DateTime();
        private string _sUsuarioMod = string.Empty;
        private DateTime _dtFechaMod = new DateTime();
        private string _sIP = string.Empty;
        private string _sFechaUltMov = string.Empty;
        private int _UnaFactura = 0;


        public int iId { get { return _iId; } set { _iId = value; } }
        public int iIdCliente { get { return _iIdCliente; } set { _iIdCliente = value; } }
        public int iIdContrato { get { return _iIdContrato; } set { _iIdContrato = value; } }
        public int iIdMonedaVuelo { get { return _iIdMonedaVuelo; } set { _iIdMonedaVuelo = value; } }
        public string sClaveFacturanteVuelo { get { return _sClaveFacturanteVuelo; } set { _sClaveFacturanteVuelo = value; } }
        public int iIdMonedaSCC { get { return _iIdMonedaSCC; } set { _iIdMonedaSCC = value; } }
        public string sClaveFacturanteSCC { get { return _sClaveFacturanteSCC; } set { _sClaveFacturanteSCC = value; } }

        public List<int> lstIdRemisionesSeleccionadas { get { return _lstIdRemisionesSeleccionadas; } set { _lstIdRemisionesSeleccionadas = value; } }
        public string sTipoContrato { get { return _sTipoContrato; } set { _sTipoContrato = value; } }
        public string sCliente { get { return _sCliente; } set { _sCliente = value; } }
        public string sContrato { get { return _sContrato; } set { _sContrato = value; } }


        public decimal dSubDllV { get { return _dSubDllV; } set { _dSubDllV= value; } }
        public decimal dDescMXNV { get { return _dDescMXNV; } set { _dDescMXNV = value; } }
        public decimal dSubMXNV { get { return _dSubMXNV; } set { _dSubMXNV = value; } }
        public decimal dDescDllV { get { return _dDescDllV; } set { _dDescDllV = value; } }
        public decimal dSubHorasV { get { return _dSubHorasV; } set { _dSubHorasV = value; } }

        public decimal dIVAV { get { return _dIVAV; } set { _dIVAV = value; } }

        public decimal dIVADllV { get { return _dIVADllV; } set { _dIVADllV = value; } }
        public decimal dIVAMXNV { get { return _dIVAMXNV; } set { _dIVAMXNV = value; } }

        public decimal dTotalDllV { get { return _dTotalDllV; } set { _dTotalDllV = value; } }
        public decimal dTotalMXNV { get { return _dTotalMXNV; } set { _dTotalMXNV = value; } }

        public decimal dSubDllC { get { return _dSubDllC; } set { _dSubDllC = value; } }
        public decimal dSubMXNC { get { return _dSubMXNC; } set { _dSubMXNC = value; } }
        public string sSubHorasC { get { return _sSubHorasC; } set { _sSubHorasC = value; } }

        public decimal dIVAC { get { return _dIVAC; } set { _dIVAC = value; } }
        public decimal dTipoCambio { get { return _dTipoCambio; } set { _dTipoCambio = value; } }
        public decimal dIVADllC { get { return _dIVADllC; } set { _dIVADllC = value; } }
        public decimal dIVAMXNC { get { return _dIVAMXNC; } set { _dIVAMXNC = value; } }

        public decimal dTotalDllC { get { return _dTotalDllC; } set { _dTotalDllC = value; } }
        public decimal dTotalMXNC { get { return _dTotalMXNC; } set { _dTotalMXNC = value; } }

        public bool bCobroV { get { return _bCobroV; } set { _bCobroV = value; } }
        public bool bCobroSCC { get { return _bCobroSCC; } set { _bCobroSCC = value; } }
        public bool bCobroAmbos { get { return _bCobroAmbos; } set { _bCobroAmbos = value; } }
        public int iIdFactura { get { return _iIdFactura; } set { _iIdFactura = value; } }
        public int iIdPorcentaje { get { return _iIdPorcentaje; } set { _iIdPorcentaje = value; } }


        public string sFacturaVuelo { get { return _sFacturaVuelo; } set { _sFacturaVuelo = value; } }
        public string sFacturaSCC { get { return _sFacturaSCC; } set { _sFacturaSCC = value; } }


        // Sumarizado
        public decimal dSubTotalSumarizadoDllc { get { return _dSubTotalSumarizadoDllc; } set { _dSubTotalSumarizadoDllc = value; } }
        public decimal dSubTotalSumarizadoMXNC { get { return _dSubTotalSumarizadoMXNC; } set { _dSubTotalSumarizadoMXNC = value; } }


        public int iStatus { get { return _iStatus; } set { _iStatus = value; } }

        public string sUsuarioCreacion { get { return _sUsuarioCreacion; } set { _sUsuarioCreacion = value; } }

        public DateTime dtFechaCreacion { get { return _dtFechaCreacion; } set { _dtFechaCreacion = value; } }

        public string sUsuarioMod { get { return _sUsuarioMod; } set { _sUsuarioMod = value; } }

        public DateTime dtFechaMod { get { return _dtFechaMod; } set { _dtFechaMod = value; } }

        public string sIP { get { return _sIP; } set { _sIP = value; } }

        
        public string sFechaUltMov { get { return _sFechaUltMov; } set { _sFechaUltMov = value; } }

        public int UnaFactura { get { return _UnaFactura; } set { _UnaFactura = value; } }

    }
}