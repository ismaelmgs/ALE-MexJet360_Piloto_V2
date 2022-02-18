using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ALE_MexJet.Objetos
{
    public class FacturaPaso1
    {
        private int _iIdPrefactura = 0;
        private String _sFacturante = string.Empty;
        private String _sInvNum = string.Empty;
        private DateTime _dtInvDate = DateTime.Now;
        private DateTime _dtDueDate = DateTime.Now;
        private String _sAcct = string.Empty;
        private Decimal _dAmount = 0m;
        private Decimal _dSales_tax = 0m;
        private String _sRef = string.Empty;
        private String _sDescription = string.Empty;
        private Decimal _dExch_rate = 0m;
        private String _sTax_code1 = string.Empty;
        private String _sAcct_unit3 = string.Empty;
        private String _sAcct_unit4 = string.Empty;
        private String _sUsuario = string.Empty;

        private String _Ufserie = string.Empty;
        private String _Ufruta = string.Empty;
        private String _UfModelo = string.Empty;
        private String _UfMarca = string.Empty;
        private String _TipoFactura = string.Empty;

        private DateTime? _DtFechaSalida = null;
        private DateTime? _DtFechaRegreso = null;
        private int _Ufremision = 0;

        private string _sTipo = string.Empty;
        private string _sMoneda = string.Empty;
        private int _iEmpresa = 0;
        private string _sSucursal = string.Empty;
        private string _sSerie = string.Empty;

        private string _sMetodoPago = string.Empty;
        private string _sFormaPago = string.Empty;
        private string _sUsoCFDI = string.Empty;
        

        
        public int iIdPrefactura { get {return _iIdPrefactura; } set { _iIdPrefactura = value; } }
        public String sFacturante { get { return _sFacturante; } set { _sFacturante = value; } }
        public String sInvNum { get { return _sInvNum; } set { _sInvNum = value; } }
        public DateTime dtInvDate { get { return _dtInvDate; } set { _dtInvDate = value; } }
        public DateTime dtDueDate { get { return _dtDueDate; } set { _dtDueDate = value; } }
        public String sAcct { get { return _sAcct; } set { _sAcct = value; } }
        public Decimal dAmount { get { return _dAmount; } set { _dAmount = value; } }
        public Decimal dSales_tax { get { return _dSales_tax; } set { _dSales_tax = value; } }
        public String sRef { get { return _sRef; } set { _sRef = value; } }
        public String sDescription { get { return _sDescription; } set { _sDescription = value; } }
        public Decimal dExch_rate { get { return _dExch_rate; } set { _dExch_rate = value; } }
        public String sTax_code1 { get { return _sTax_code1; } set { _sTax_code1 = value; } }
        public String sAcct_unit3 { get { return _sAcct_unit3; } set { _sAcct_unit3 = value; } }
        public String sAcct_unit4 { get { return _sAcct_unit4; } set { _sAcct_unit4 = value; } }
        public String sUsuario { get { return _sUsuario; } set { _sUsuario = value; } }
        public String Ufserie { get { return _Ufserie; } set { _Ufserie = value; } }
        public String Ufruta { get { return _Ufruta; } set { _Ufruta = value; } }
        public String UfModelo { get { return _UfModelo; } set { _UfModelo = value; } }
        public String UfMarca { get { return _UfMarca; } set { _UfMarca = value; } }
        public String TipoFactura { get { return _TipoFactura; } set { _TipoFactura = value; } }
        public DateTime? DtFechaSalida { get { return _DtFechaSalida; } set { _DtFechaSalida = value; } }
        public DateTime? DtFechaRegreso { get { return _DtFechaRegreso; } set { _DtFechaRegreso = value; } }
        public int Ufremision { get { return _Ufremision; } set { _Ufremision = value; } }
        
        public String sTipo { get { return _sTipo; } set { _sTipo = value; } }
        public string sMoneda { get { return _sMoneda; } set { _sMoneda = value; } }
        public int iEmpresa { get { return _iEmpresa; } set { _iEmpresa = value; } }
        public string sSucursal { get { return _sSucursal; } set { _sSucursal = value; } }
        public string sSerie { get { return _sSerie; } set { _sSerie = value; } }

        public string sMetodoPago { get { return _sMetodoPago; } set { _sMetodoPago = value; } }
        public string sFormaPago { get { return _sFormaPago; } set { _sFormaPago = value; } }
        public string sUsoCFDI { get { return _sUsoCFDI; } set { _sUsoCFDI = value; } }
        
    }
}