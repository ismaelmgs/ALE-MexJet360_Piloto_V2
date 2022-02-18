using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace ALE_MexJet.Objetos
{
    [Serializable, Bindable(BindableSupport.Yes)]
    public class MonitorCliente :BaseObjeto
    {
        private int _iIdContrato = -1;
        private int _iIdCliente = -1;
        private string _sClaveContrato = string.Empty;
        private string _sCodigoCliente = string.Empty;
        private string _sTipoContrato = string.Empty;
        private string _sGrupoModelo = string.Empty;
        private string _sVendedor = string.Empty;
        public int iIdContrato { get { return _iIdContrato; } set { _iIdContrato = value; } }
        public int iIdCliente { get { return _iIdCliente; } set { _iIdCliente = value; } }
        public string sClaveContrato { get { return _sClaveContrato; } set { _sClaveContrato = value; } }
        public string sCodigoCliente { get { return _sCodigoCliente; } set { _sCodigoCliente = value; } }
        public string sTipoContrato { get { return _sTipoContrato; } set { _sTipoContrato = value; } }
        public string sGrupoModelo { get { return _sGrupoModelo; } set { _sGrupoModelo = value; } }
        public string sVendedor { get { return _sVendedor; } set { _sVendedor = value; } }

    }
}