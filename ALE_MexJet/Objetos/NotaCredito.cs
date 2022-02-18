using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ALE_MexJet.Objetos
{
    [Serializable, Bindable(BindableSupport.Yes)]
    public class NotaCredito : BaseObjeto
    {
        private int _iIdFolioRemision = -1;
        private string _TipoNotaCredito = string.Empty;
        private decimal _Cantidad = 0;
        private string _Tiempo = string.Empty;
        private int _Status = 1;
        private string _UsuarioCreacion = string.Empty;
        private string _IP = string.Empty;

        private int _FolioNotaCredito = -1;
        private string _CodigoCliente = null;
        private string _ClaveContrato = null;
        private string _FechaInicio = null;
        private string _FechaFin = null;

        private string _Opcion = "1";
        private int _IdNotaCredito = -1;
        [Display(AutoGenerateField = false), ScaffoldColumn(false)]

        public int iIdFolioRemision { get { return _iIdFolioRemision; } set { _iIdFolioRemision = value; } }

        public string iTipoNotaCredito { get { return _TipoNotaCredito; } set { _TipoNotaCredito = value; } }

        public decimal iCantidad { get { return _Cantidad; } set { _Cantidad = value; } }

        public string iTiempo { get { return _Tiempo; } set { _Tiempo = value; } }

        public int iStatus { get { return _Status; } set { _Status = value; } }

        public string iUsuarioCreacion {get  {return _UsuarioCreacion;} set { _UsuarioCreacion = value;} }

        public string iIP { get { return _IP; } set { _IP = value; } }



        public int FolioNotaCredito { get { return _FolioNotaCredito; } set { _FolioNotaCredito = value; } }

        public string CodigoCliente { get { return _CodigoCliente; } set { _CodigoCliente = value; } }
        
        public string Clavecontrato {get {return _ClaveContrato; } set {_ClaveContrato = value;}}

        public string FechaInicio  { get { return _FechaInicio; } set { _FechaInicio = value; } }

        public string FechaFin { get { return _FechaFin; } set { _FechaFin = value; } }

        public string Opcion { get { return _Opcion; } set { _Opcion = value; } }
        public int IdNotaCredito { get { return _IdNotaCredito; } set { _IdNotaCredito = value; } }
        
    }
}