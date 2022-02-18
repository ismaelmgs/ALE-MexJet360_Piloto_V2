using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ALE_MexJet.Objetos
{
    [Serializable, Bindable(BindableSupport.Yes)]
    public class Vendedor : BaseObjeto
    {
        private int _iIdVendedor = 0;
        private string _sNombre = string.Empty;
        private string _sZona = string.Empty;
        private string _sIdUnidad4 = string.Empty;
        private string _sUnidadNegocio = string.Empty;
        private string _sLogin = string.Empty;
        private string _sIdUnidad1 = string.Empty;
        private string _sDescripcionUnidad = string.Empty;
        private string _sCorreoElectronico = string.Empty;
        private int _iIdBase = 0;
        private int _iStatus = 1;
        private string _sUsuarioCreacion = string.Empty;
        private DateTime _dtFechaCreacion = new DateTime();
        private string _sUsuarioMod = string.Empty;
        private DateTime _dtFechaMod = new DateTime();
        private string _sIP = string.Empty;

        [Display(AutoGenerateField = false), ScaffoldColumn(false)]
        public int iIdVendedor { get { return _iIdVendedor; } set { _iIdVendedor = value; } }
        public string sNombre { get { return _sNombre; } set { _sNombre = value; } }
        public string sZona { get { return _sZona; } set { _sZona = value; } }
        public string sIdUnidad4 { get { return _sIdUnidad4; } set { _sIdUnidad4 = value; } }
        public string sUnidadNegocio { get { return _sUnidadNegocio; } set { _sUnidadNegocio = value; } }
        public string sLogin { get { return _sLogin; } set { _sLogin = value; } }
        public string sIdUnidad1 { get { return _sIdUnidad1; } set { _sIdUnidad1 = value; } }
        public string sDescripcionUnidad { get { return _sDescripcionUnidad; } set { _sDescripcionUnidad = value; } }
        public string sCorreoElectronico { get { return _sCorreoElectronico; } set { _sCorreoElectronico = value; } }
        public int iIdBase { get { return _iIdBase; } set { _iIdBase = value; } }


        [Display(Name = "¿Activo? "), Required]
        public int iStatus { get { return _iStatus; } set { _iStatus = value; } }

        public string sUsuarioCreacion { get { return _sUsuarioCreacion; } set { _sUsuarioCreacion = value; } }

        public DateTime dtFechaCreacion { get { return _dtFechaCreacion; } set { _dtFechaCreacion = value; } }

        public string sUsuarioMod { get { return _sUsuarioMod; } set { _sUsuarioMod = value; } }

        public DateTime dtFechaMod { get { return _dtFechaMod; } set { _dtFechaMod = value; } }

        public string sIP { get { return _sIP; } set { _sIP = value; } }
    }
}