using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ALE_MexJet.Objetos
{
    [Serializable, Bindable(BindableSupport.Yes)]
    public class Cliente
    {

        private int _iId = -1;
        private string _sCodigoCliente = string.Empty;
        private int _iTipoCliente = -1;
        private string _sNombre = string.Empty;
        private string _sObservaciones = string.Empty;
        private string _sNotas = string.Empty;
        private string _sOtros = string.Empty;
        private int _iStatus = 1;
        private string _sUsuarioCreacion = string.Empty;
        private DateTime _dtFechaCreacion = new DateTime();
        private string _sUsuarioMod = string.Empty;
        private DateTime _dtFechaMod = new DateTime();
        private string _sIP = string.Empty;
        private string _sFechaUltMov = string.Empty;


        public int iId { get { return _iId; } set { _iId = value; } }
        public string sCodigoCliente { get { return _sCodigoCliente; } set {_sCodigoCliente = value; } }
        public int iTipoCliente { get { return _iTipoCliente; } set { _iTipoCliente = value; } }
        public string sNombre { get { return  _sNombre; } set {_sNombre = value; } }
        public string sObservaciones { get { return  _sObservaciones; } set {_sObservaciones = value; } }
        public string sNotas  { get { return  _sNotas; } set {_sNotas = value; } }
        public string sOtros { get { return _sOtros; } set { _sOtros = value; } }
        
        [Display(Name = "¿Activo? "), Required]
        public int iStatus { get { return _iStatus; } set { _iStatus = value; } }

        public string sUsuarioCreacion { get { return _sUsuarioCreacion; } set { _sUsuarioCreacion = value; } }

        public DateTime dtFechaCreacion { get { return _dtFechaCreacion; } set { _dtFechaCreacion = value; } }

        public string sUsuarioMod { get { return _sUsuarioMod; } set { _sUsuarioMod = value; } }

        public DateTime dtFechaMod { get { return _dtFechaMod; } set { _dtFechaMod = value; } }

        public string sIP { get { return _sIP; } set { _sIP = value; } }

        [Display(Name = "Fecha DT"), Required]
        public string sFechaUltMov { get { return _sFechaUltMov; } set { _sFechaUltMov = value; } }

    }
}