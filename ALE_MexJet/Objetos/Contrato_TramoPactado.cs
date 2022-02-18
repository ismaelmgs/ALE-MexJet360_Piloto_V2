using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ALE_MexJet.Objetos
{
    [Serializable, Bindable(BindableSupport.Yes)]
    public class Contrato_TramoPactado
    {
        private int _iId = -1;
        private int _iIdContrato = -1;
        private int _iIdOrigen = -1;
        private int _iIdDestino = -1;
        private string _sTiempoVuelo = string.Empty;

        public int iId { get { return _iId; } set { _iId = value; } }
        public int iIdContrato { get { return _iIdContrato; } set { _iIdContrato = value; } }
        public int iIdOrigen { get { return _iIdOrigen; } set { _iIdOrigen = value; } }
        public int iIdDestino { get { return _iIdDestino; } set { _iIdDestino = value; } }
        public virtual string sTiempoVuelo { get { return _sTiempoVuelo; } set { _sTiempoVuelo = value; } }
    }
}
