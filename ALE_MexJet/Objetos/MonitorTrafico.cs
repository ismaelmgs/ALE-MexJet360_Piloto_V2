using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ALE_MexJet.Objetos
{
    public class MonitorTrafico
    {
        private int _iIdSolicitud;
        private int _iStatus;

        public int iIdSolicitud { get { return _iIdSolicitud; } set { _iIdSolicitud = value; } }

        public int iStatus { get { return _iStatus; } set { _iStatus = value; } }
    }
}