using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ALE_MexJet.Objetos
{
    public class FacturaPaso2
    {
        private int _iIdFactura = 0;
        private string _sFacturante = string.Empty;
        private string _sInvNum = string.Empty;
        private int _iDistSeq = 0;
        private string _sAcct = string.Empty;
        private decimal _dAmount = 0m;
        private string _sTax_code1 = string.Empty;
        private int _iTaxSystem = 0;
        private string _sAcct_unit1 = string.Empty;
        private string _sAcct_unit2 = string.Empty;
        private string _sAcct_unit3 = string.Empty;
        private string _sAcct_unit4 = string.Empty;
        private string _sUsuario = string.Empty;

        private int _iEmpresa = 0;
        private string _sItem = string.Empty;
        private string _sConceptoUsuario = string.Empty;
        private string _sCodigoBarras = string.Empty;
        private int _iCantidad = 0;
        private decimal _dDescuento = 0;
        private string _sAlmacen = string.Empty;
        private string _sProyecto = string.Empty;
        private string _sAcct_unit5 = string.Empty;

        public int iIdFactura { get { return _iIdFactura; } set { _iIdFactura = value; } }
        public string sFacturante { get { return _sFacturante; } set { _sFacturante = value; } }
        public string sInvNum { get { return _sInvNum; } set { _sInvNum = value; } }
        public int iDistSeq { get { return _iDistSeq; } set { _iDistSeq = value; } }
        public string sAcct { get { return _sAcct; } set { _sAcct = value; } }
        public decimal dAmount { get { return _dAmount; } set { _dAmount = value; } }
        public string sTax_code1 { get { return _sTax_code1; } set { _sTax_code1 = value; } }
        public int iTaxSystem { get { return _iTaxSystem; } set { _iTaxSystem = value; } }
        public string sAcct_unit1 { get { return _sAcct_unit1; } set { _sAcct_unit1 = value; } }
        public string sAcct_unit2 { get { return _sAcct_unit2; } set { _sAcct_unit2 = value; } }
        public string sAcct_unit3 { get { return _sAcct_unit3; } set { _sAcct_unit3 = value; } }
        public string sAcct_unit4 { get { return _sAcct_unit4; } set { _sAcct_unit4 = value; } }
        public string sUsuario { get { return _sUsuario; } set { _sUsuario = value; } }

        public int iEmpresa { get { return _iEmpresa; } set { _iEmpresa = value; } }
        public string sItem { get { return _sItem; } set { _sItem = value; } }
        public string sConceptoUsuario { get { return _sConceptoUsuario; } set { _sConceptoUsuario = value; } }
        public string sCodigoBarras { get { return _sCodigoBarras; } set { _sCodigoBarras = value; } }
        public int iCantidad { get { return _iCantidad; } set { _iCantidad = value; } }
        public decimal dDescuento { get { return _dDescuento; } set { _dDescuento = value; } }
        public string sAlmacen { get { return _sAlmacen; } set { _sAlmacen = value; } }
        public string sProyecto { get { return _sProyecto; } set { _sProyecto = value; } }
        public string sAcct_unit5 { get { return _sAcct_unit5; } set { _sAcct_unit5 = value; } }
    }


    public class FacturaSC : BaseObjeto
    {
        private int _iIdPrefactura = 0;
        private int _iEmpresa = 0;
        private string _sItem = string.Empty;
        private string _sConceptoUsuario = string.Empty;
        private string _sCodigoBarras = string.Empty;
        private int _iCantidad = 1;
        private int _iNumeroLinea = 0;
        private int _iIdMoneda = 0;
        private decimal _dSubtotal = 0;
        private decimal _dTipoCambioPrefactura = 0;
        private int _iFactorIVA = 0;
        private string _sDimension1 = string.Empty;
        private string _sDimension2 = string.Empty;
        private string _sDimension3 = string.Empty;
        private string _sDimension4 = string.Empty;
        private string _sDimension5 = string.Empty;
        private string _sProyecto = string.Empty;

        public int iIdPrefactura { get { return _iIdPrefactura; } set { _iIdPrefactura = value; } }
        public int iEmpresa { get { return _iEmpresa; } set { _iEmpresa = value; } }
        public string sItem { get { return _sItem; } set { _sItem = value; } }
        public string sConceptoUsuario { get { return _sConceptoUsuario; } set { _sConceptoUsuario = value; } }
        public string sCodigoBarras { get { return _sCodigoBarras; } set { _sCodigoBarras = value; } }
        public int iCantidad { get { return _iCantidad; } set { _iCantidad = value; } }
        public int iNumeroLinea { get { return _iNumeroLinea; } set { _iNumeroLinea = value; } }
        public int iIdMoneda { get { return _iIdMoneda; } set { _iIdMoneda = value; } }
        public decimal dSubtotal { get { return _dSubtotal; } set { _dSubtotal = value; } }
        public decimal dTipoCambioPrefactura { get { return _dTipoCambioPrefactura; } set { _dTipoCambioPrefactura = value; } }
        public int iFactorIVA { get { return _iFactorIVA; } set { _iFactorIVA = value; } }
        public string sDimension1 { get { return _sDimension1; } set { _sDimension1 = value; } }
        public string sDimension2 { get { return _sDimension2; } set { _sDimension2 = value; } }
        public string sDimension3 { get { return _sDimension3; } set { _sDimension3 = value; } }
        public string sDimension4 { get { return _sDimension4; } set { _sDimension4 = value; } }
        public string sDimension5 { get { return _sDimension5; } set { _sDimension5 = value; } }
        public string sProyecto { get { return _sProyecto; } set { _sProyecto = value; } }
    }
}