using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ALE_MexJet.Objetos
{
    [Serializable, Bindable(BindableSupport.Yes)]
    public class ContactosyPreferencias
    {

        private int _iIdContacto = -1;
        private int _iIdCliente = -1;
        private string _sNombre = string.Empty;
        private int _iIdTitulo = -1;
        private string _sCorreoElectronico = string.Empty;
        private string _sTelOficina = string.Empty;
        private string _sTelMovil = string.Empty;
        private string _sOtroTel = string.Empty;
        private int _iStatus = 1;
        private string _sUsuarioCreacion = string.Empty;
        private DateTime _dtFechaCreacion = new DateTime();
        private string _sUsuarioMod = string.Empty;
        private DateTime _dtFechaMod = new DateTime();
        private string _sIP = string.Empty;


        public int iIdContacto { get { return _iIdContacto; } set { _iIdContacto = value; } }

        public int iIdCliente { get { return _iIdCliente; } set { _iIdCliente = value; } }

        public string sNombre { get { return _sNombre; } set { _sNombre = value; } }
        public int iIdTitulo { get { return _iIdTitulo; } set { _iIdTitulo = value; } }
        public string sCorreoElectronico { get { return _sCorreoElectronico; } set { _sCorreoElectronico = value; } }

        public string sTelOficina { get { return _sTelOficina; } set { _sTelOficina = value; } }
        public string sTelMovil { get { return _sTelMovil; } set { _sTelMovil = value; } }
        public string sOtroTel { get { return _sOtroTel; } set { _sOtroTel = value; } }
    
        [Display(Name = "¿Activo? "), Required]
        public int iStatus { get { return _iStatus; } set { _iStatus = value; } }

        public string sUsuarioCreacion { get { return _sUsuarioCreacion; } set { _sUsuarioCreacion = value; } }

        public DateTime dtFechaCreacion { get { return _dtFechaCreacion; } set { _dtFechaCreacion = value; } }

        public string sUsuarioMod { get { return _sUsuarioMod; } set { _sUsuarioMod = value; } }

        public DateTime dtFechaMod { get { return _dtFechaMod; } set { _dtFechaMod = value; } }

        public string sIP { get { return _sIP; } set { _sIP = value; } }

        //[Display(Name = "Fecha DT"), Required]
    }
    
    

}