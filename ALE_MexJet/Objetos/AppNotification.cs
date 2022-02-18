using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ALE_MexJet.Objetos
{
    public class AppNotification
    {
        private int _id = 0;
        private string _status = string.Empty;

        public int id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string status
        {
            get { return _status; }
            set { _status = value; }
        }
    }
}