using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ALE_MexJet.Objetos
{
    [Serializable, Bindable(BindableSupport.Yes)]
    public class ConsultaSolicitudes : BaseObjeto
    {
        private int _IdCliente = 0;
        private string _ClaveCliente = string.Empty;
        private int _IdContrato = 0;
        private string _ClaveContrato = string.Empty;
        private long _IdTrip = 0;
        private int _Idusuario = 0;
        private string _UsuarioCreacion = string.Empty;
        private int _IdEstatus = 0;
        private string _FechaCreacionIni = string.Empty;
        private string _FechaCreacionFin = string.Empty;
        private string _FechaVueloIni = string.Empty;
        private string _FechaVueloFin = string.Empty;

        [Display(AutoGenerateField = false), ScaffoldColumn(false)]

        public int IdCliente { get { return _IdCliente; } set { _IdCliente = value; } }
        public string ClaveCliente { get { return _ClaveCliente; } set { _ClaveCliente = value; } }
        public int IdContrato { get { return _IdContrato; } set { _IdContrato = value; } }
        public string ClaveContrato { get { return _ClaveContrato; } set { _ClaveContrato = value; } }
        public long IdTrip { get { return _IdTrip; } set { _IdTrip = value; } }
        public int Idusuario { get { return _Idusuario; } set { _Idusuario = value; } }
        public string UsuarioCreacion { get { return _UsuarioCreacion; } set { _UsuarioCreacion = value; } }
        public int IdEstatus { get { return _IdEstatus; } set { _IdEstatus = value; } }
        public string FechaCreacionIni { get { return _FechaCreacionIni; } set { _FechaCreacionIni = value; } }
        public string FechaCreacionFin { get { return _FechaCreacionFin; } set { _FechaCreacionFin = value; } }
        public string FechaVueloIni { get { return _FechaVueloIni; } set { _FechaVueloIni = value; } }
        public string FechaVueloFin { get { return _FechaVueloFin; } set { _FechaVueloFin = value; } }

    }
}