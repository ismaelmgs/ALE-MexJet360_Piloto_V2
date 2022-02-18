using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ALE_MexJet.Objetos
{
    [Serializable, Bindable(BindableSupport.Yes)]
    public class CorreoM
    {
        
        private string _sMotivo = string.Empty;
        private string _sAsunto = string.Empty;
        private string _sDestinatarios = string.Empty;
        private string _sCopiados = string.Empty;
        private string _sContenido = string.Empty;
        private int _iStatus = -1;
        private string _sUsuarioCre = string.Empty;
        private DateTime _dtFechaCre = new DateTime();
        private string _sIP = string.Empty;
        private int _iIdCorreo = -1;

        public string sMotivo { get { return _sMotivo; } set { _sMotivo = value; } }
        public string sAsunto { get { return _sAsunto; } set { _sAsunto = value; } }
        public string sDestinatarios { get { return _sDestinatarios; } set { _sDestinatarios = value; } }
        public string sCopiados { get { return _sCopiados; } set { _sCopiados = value; } }
        public string sContenido { get { return _sContenido; } set { _sContenido = value; } }
        public int iStatus { get { return _iStatus; } set { _iStatus = value; } }
        public string sUsuarioCre { get { return _sUsuarioCre; } set { _sUsuarioCre = value; } }
        public DateTime dtFechaCre { get { return _dtFechaCre; } set { _dtFechaCre = value; } }
        public string sIP { get { return _sIP; } set { _sIP = value; } }
        public int iIdCorreo { get { return _iIdCorreo; } set { _iIdCorreo = value; } }
        
    }
}