using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ALE_MexJet.Objetos
{
    public class Revenew : BaseObjeto
    {

        private int _iIdDescuento = 0;
        private int _iIdTipoCliente = 0;
        private string _sDescripcion = string.Empty;
        private decimal _dDescuento = 0;
        private int _iAcumulado = 0;
        private string _sUsuario = string.Empty;

        public int iIdDescuento { get { return _iIdDescuento; } set { _iIdDescuento = value; } }
        public int iIdTipoCliente { get { return _iIdTipoCliente; } set { _iIdTipoCliente = value; } }
        
        public string sDescripcion { get { return _sDescripcion; } set { _sDescripcion = value; } }
        
        public decimal dDescuento { get { return _dDescuento; } set { _dDescuento = value; } }
        
        public int iAcumulado { get { return _iAcumulado; } set { _iAcumulado = value; } }
        
        public string sUsuario { get { return _sUsuario; } set { _sUsuario = value; } }

    }
}