using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ALE_MexJet.Objetos
{
    [Serializable, Bindable(BindableSupport.Yes)]
    public class MonitorComisariato : BaseObjeto
    {
        public int _iIdComisariato = -1;
        public int _iStatus = -1;
        public string _Usuario = string.Empty;
       

        public int iIdComisariato { get { return _iIdComisariato; } set { _iIdComisariato = value; } }
        public int iStatus { get { return _iStatus; } set { _iStatus = value; } }
        public string sUsuario { get { return _Usuario; } set { _Usuario = value; } }
    }
}