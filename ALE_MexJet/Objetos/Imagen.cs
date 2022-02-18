using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ALE_MexJet.Objetos
{
    [Serializable, Bindable(BindableSupport.Yes)]
    public class Imagen : BaseObjeto
    {
        private string _ClaveContrato = null;
        private string _Matricula = null;
        private string _Cliente = null;
        private string _Fecha = null;
        private string _IdTramo = null;

        private int _IdPax = -1;
        private bool _Arrivo =false;
        private string _HoraLlegada = null;
        private string _IdSolicitud = null;

        private int _IdPadre = 0;

        private int _IdControl = 0;
        private int _IdImagen =0;
        private bool _AbordadoPre = false;
        private bool _AbordadoPos = false;
        private string _Abordada =string.Empty;
        private string _ObservacionesPre =string.Empty;
        private string _ObservacionesPos = string.Empty;
        private string _UsuarioModificacion =string.Empty;
        private string _IP = string.Empty;
        private string _PostFlight = string.Empty;
        private string _Opcion = string.Empty;

        [Display(AutoGenerateField = false), ScaffoldColumn(false)]

        public string  ClaveContrato { get { return _ClaveContrato; } set { _ClaveContrato = value; } }
        public string Matricula { get { return _Matricula; } set { _Matricula = value; } }
        public string Cliente { get { return _Cliente; } set { _Cliente = value; } }
        public string Fecha { get { return _Fecha; } set { _Fecha = value; } }
        public string IdTramo { get { return _IdTramo; } set { _IdTramo = value; } }
        public int IdPax { get { return _IdPax; } set { _IdPax = value; } }
        public bool Arrivo { get { return _Arrivo; } set { _Arrivo = value; } }
        public string HoraLlegada { get { return _HoraLlegada; } set { _HoraLlegada = value; } }
        public string IdSolicitud { get { return _IdSolicitud; } set { _IdSolicitud = value; } }
        public int IdPadre { get { return _IdPadre; } set { _IdPadre = value; } }
        public int IdControl { get { return _IdControl; } set { _IdControl = value; } }
        public int IdImagen { get { return _IdImagen; } set { _IdImagen = value; } }
        public bool AbordadoPre { get { return _AbordadoPre; } set { _AbordadoPre = value; } }
        public bool AbordadoPos { get { return _AbordadoPos; } set { _AbordadoPos = value; } }
        public string Abordada { get { return _Abordada; } set { _Abordada = value; } }
        public string ObservacionesPre { get { return _ObservacionesPre; } set { _ObservacionesPre = value; } }
        public string ObservacionesPos { get { return _ObservacionesPos; } set { _ObservacionesPos = value; } }
        public string UsuarioModificacion { get { return _UsuarioModificacion; } set { _UsuarioModificacion = value; } }
        public string IP { get { return _IP; } set { _IP = value; } }
        public string PostFlight { get { return _PostFlight; } set { _PostFlight = value; } }
        public string Opcion { get { return _Opcion; } set { _Opcion = value; } }
        
    }
}