using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ALE_MexJet.Objetos
{
    [Serializable, Bindable(BindableSupport.Yes)]
    public class Membresia : BaseObjeto
    {
        private int _iId = -1;
        public string _sCodigo = string.Empty;
        public string _sDescripcion = string.Empty;
        private int _iStatus = 1;
        private string _sUsuarioCreacion = string.Empty;
        private DateTime _dtFechaCreacion = new DateTime();
        private string _sUsuarioMod = string.Empty;
        private DateTime _dtFechaMod = new DateTime();
        private string _sIP = string.Empty;

        public int iId { get { return _iId; } set { _iId = value; } }
        public string sCodigo { get { return _sCodigo; } set { _sCodigo = value; } }
        public string sDescripcion { get { return _sDescripcion; } set { _sDescripcion = value; } }
        public int iStatus { get { return _iStatus; } set { _iStatus = value; } }
        public string sUsuarioCreacion { get { return _sUsuarioCreacion; } set { _sUsuarioCreacion = value; } }
        public DateTime dtFechaCreacion { get { return _dtFechaCreacion; } set { _dtFechaCreacion = value; } }
        public string sUsuarioMod { get { return _sUsuarioMod; } set { _sUsuarioMod = value; } }
        public DateTime dtFechaMod { get { return _dtFechaMod; } set { _dtFechaMod = value; } }
        public string sIP { get { return _sIP; } set { _sIP = value; } }
    }

    [Serializable]
    public class AutorizacionMembresia
    {
        private string _message = string.Empty;

        public string message { get { return _message; } set { _message = value; } }
    }
}