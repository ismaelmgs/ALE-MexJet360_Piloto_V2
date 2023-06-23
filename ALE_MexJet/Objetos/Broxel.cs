using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ALE_MexJet.Objetos
{
    public class Broxel
    {
        public class TokenAccess
        {
            private string _token = string.Empty;
            public string token { get { return _token; } set { _token = value; } }
        }
        public class Transfer
        {
            public int currency { get; set; }
            public string idClientTransaction { get; set; }
            public string sourceReference { get; set; }
            public int sourceReferenceType { get; set; }
            public decimal targetAmount { get; set; }
            public string targetReference { get; set; }
            public int targetReferenceType { get; set; }

        }

        public class Data
        {
            public string processIdentifier { get; set; }
        }

        public class DataTransfer
        {
            public Data data { get; set; }
            public bool success { get; set; }
            public int code { get; set; }
            public string userMessage { get; set; }
            public string internalMessage { get; set; }
            public object errorList { get; set; }
            public DateTime responseDate { get; set; }
        }

    }
}