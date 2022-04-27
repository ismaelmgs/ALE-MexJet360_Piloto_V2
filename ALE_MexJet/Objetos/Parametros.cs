using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ALE_MexJet.Objetos
{
    public class Parametros
    {
        private int _iIdParametro = -1;
        private string _sClave = string.Empty;
        private string _sDescripcion = string.Empty;
        private string _sValor = string.Empty;
        private int _iStatus = 1;
        private string _sUsuarioCreacion = string.Empty;
        private DateTime _dtFechaCreacion = new DateTime();
        private string _sUsuarioMod = string.Empty;
        private DateTime _dtFechaMod = new DateTime();
        private string _sIP = string.Empty;

        public int iIdParametro { get { return _iIdParametro; } set { _iIdParametro = value; } }
        public string sClave { get { return _sClave; } set { _sClave = value; } }
        public string sDescripcion { get { return _sDescripcion; } set { _sDescripcion = value; } }
        public string sValor { get { return _sValor; } set { _sValor = value; } }

        [Display(Name = "¿Activo? "), Required]
        public int iStatus { get { return _iStatus; } set { _iStatus = value; } }

        public string sUsuarioCreacion { get { return _sUsuarioCreacion; } set { _sUsuarioCreacion = value; } }

        public DateTime dtFechaCreacion { get { return _dtFechaCreacion; } set { _dtFechaCreacion = value; } }

        public string sUsuarioMod { get { return _sUsuarioMod; } set { _sUsuarioMod = value; } }

        public DateTime dtFechaMod { get { return _dtFechaMod; } set { _dtFechaMod = value; } }

        public string sIP { get { return _sIP; } set { _sIP = value; } }

        public string Nombre { get; set; }
        public string Valor { get; set; }
    }
}