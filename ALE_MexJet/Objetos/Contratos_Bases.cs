using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ALE_MexJet.Objetos
{
    [Serializable, Bindable(BindableSupport.Yes)]
    public class Contratos_Bases
    {
        private int _iId = -1;
        private int _iIdContrato = -1;
        private string _sAeropuerto = string.Empty;
        private int  _iPredeterminada = -1;
        public int iId { get { return _iId; } set { _iId = value; } }
        public int iIdContrato { get { return _iIdContrato; } set { _iIdContrato = value; } }
        public string sAeropuerto { get { return _sAeropuerto; } set { _sAeropuerto = value; } }
        public int iPredeterminada { get { return _iPredeterminada; } set { _iPredeterminada = value; } }
    }
}