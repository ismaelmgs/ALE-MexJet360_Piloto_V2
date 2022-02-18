using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ALE_MexJet.Objetos
{
    [Serializable, Bindable(BindableSupport.Yes)]
    public class Paquete
    {

        private int _iIdTipoPaquete = -1;
        private string _sDescripcion = string.Empty;
        private int _iMexJet = 0;
        private int _iEzMexJet = 0;
        private string _sProyectoSAP = string.Empty;
        private string _sDescProyectoSAP = string.Empty;
        private int _iStatus = 1;
        private string _sUsuarioCreacion = string.Empty;
        private DateTime _dtFechaCreacion = new DateTime();
        private string _sUsuarioMod = string.Empty;
        private DateTime _dtFechaMod = new DateTime();
        private string _sIP = string.Empty;

        public int iIdTipoPaquete { get { return _iIdTipoPaquete; } set { _iIdTipoPaquete = value; } }
        public string sDescripcion { get { return _sDescripcion; } set { _sDescripcion = value; } }
        public int iMexJet { get { return _iMexJet; } set { _iMexJet = value; } }
        public int iEzMexJet { get { return _iEzMexJet; } set { _iEzMexJet = value; } }
        public string sProyectoSAP { get { return _sProyectoSAP; } set { _sProyectoSAP = value; } }
        public string sDescProyectoSAP { get { return _sDescProyectoSAP; } set { _sDescProyectoSAP = value; } }
        [Display(Name = "¿Activo? "), Required]
        public int iStatus { get { return _iStatus; } set { _iStatus = value; } }

        public string sUsuarioCreacion { get { return _sUsuarioCreacion; } set { _sUsuarioCreacion = value; } }

        public DateTime dtFechaCreacion { get { return _dtFechaCreacion; } set { _dtFechaCreacion = value; } }

        public string sUsuarioMod { get { return _sUsuarioMod; } set { _sUsuarioMod = value; } }

        public DateTime dtFechaMod { get { return _dtFechaMod; } set { _dtFechaMod = value; } }

        public string sIP { get { return _sIP; } set { _sIP = value; } }

    }
}