using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ALE_MexJet.Objetos
{
    public class JsonObjeto
    {
        private int? _iId = null;
        private int? _iIdLeg = null;
        private string _sCadenaResponse;
        private string _sCadenaRequest;
        private string _sCadenaResponseCancela;
        private string _sCadenaRequestCancela;

        public int? iId { get { return _iId; } set { _iId = value; } }
        public int? iIdLeg { get { return _iIdLeg; } set { _iIdLeg = value; } }
        public string sCadenaResponse { get { return _sCadenaResponse; } set { _sCadenaResponse = value; } }
        public string sCadenaRequest { get { return _sCadenaRequest; } set { _sCadenaRequest = value; } }
        public string sCadenaResponseCancela { get { return _sCadenaResponseCancela; } set { _sCadenaResponseCancela = value; } }
        public string sCadenaRequestCancela { get { return _sCadenaRequestCancela; } set { _sCadenaRequestCancela = value; } }
    }
}