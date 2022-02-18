using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ALE_MexJet.Objetos
{
    [Serializable, Bindable(BindableSupport.Yes)]
    public class Contrato_CombustibleInternacional
    {

        private int _iId = -1;
        private int _iIdContrato = -1;
        private DateTime _dtFecha = DateTime.Now;
        private decimal _dImport = 0m;

        public int iId { get { return _iId; } set { _iId = value; } }
        public int iIdContrato { get { return _iIdContrato; } set { _iIdContrato = value; } }
        public DateTime dtFecha { get { return _dtFecha; } set { _dtFecha = value; } }
        public decimal dImport { get { return _dImport; } set { _dImport = value; } }
    }
}