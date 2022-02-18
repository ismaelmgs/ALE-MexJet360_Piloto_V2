using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ALE_MexJet.Objetos
{
    [Serializable, Bindable(BindableSupport.Yes)]
    public class ConsultaCasos : BaseObjeto
    {
        private string _NombreCliente = string.Empty;
        private int _status = 1;
        private int _IdCliente = 0;
        private int _IdArea = 0;
        private int _Idsolicitud = 0;
        private int _IdTipoCaso = 0;
        private int? _IdMotivo = 0;
        private string _FechaIni = string.Empty;
        private string _FechaFin = string.Empty;

        [Display(AutoGenerateField = false), ScaffoldColumn(false)]

        public string NombreCliente { get { return _NombreCliente; } set { _NombreCliente = value; } }

        public int status { get { return _status; } set { _status = value; } }

        public int IdCliente { get { return _IdCliente; } set { _IdCliente = value; } }

        public int IdArea { get { return _IdArea; } set { _IdArea = value; } }

        public int Idsolicitud { get { return _Idsolicitud; } set { _Idsolicitud = value; } }

        public int IdTipoCaso { get { return _IdTipoCaso; } set { _IdTipoCaso = value; } }

        public int? IdMotivo { get { return _IdMotivo; } set { _IdMotivo = value; } }

        public string FechaIni { get { return _FechaIni; } set { _FechaIni = value; } }

        public string FechaFin { get { return _FechaFin; } set { _FechaFin = value; } }

    }

    public class ConsultaTop : BaseObjeto
    {
        private string _NombreCliente = string.Empty;
        private int _status = 1;
        private int _IdCliente = 0;
        private string _IdArea = string.Empty;
        private string _CodigoCliente = string.Empty;
        private string _DesTipoCaso = string.Empty;
        private string _DesMotivoCaso = string.Empty;
        private int _Minutos = 0;
        private string _Detalle = string.Empty;
        private string _DescOtorgado = string.Empty;
        private string _UsuarioCreacion = string.Empty;
        private string _FechaCreacion = string.Empty;
       

        [Display(AutoGenerateField = false), ScaffoldColumn(false)]

        public string NombreCliente { get { return _NombreCliente; } set { _NombreCliente = value; } }

        public int status { get { return _status; } set { _status = value; } }

        public int IdCliente { get { return _IdCliente; } set { _IdCliente = value; } }

        public string IdArea { get { return _IdArea; } set { _IdArea = value; } }

        public string CodigoCliente { get { return _CodigoCliente; } set { _CodigoCliente = value; } }

        public string DesTipoCaso { get { return _DesTipoCaso; } set { _DesTipoCaso = value; } }

        public string DesMotivoCaso { get { return _DesMotivoCaso; } set { _DesMotivoCaso = value; } }

        public int Minutos { get { return _Minutos; } set { _Minutos = value; } }

        public string Detalle { get { return _Detalle; } set { _Detalle = value; } }

        public string DescOtorgado { get { return _DescOtorgado; } set { _DescOtorgado = value; } }

        public string UsuarioCreacion { get { return _UsuarioCreacion; } set { _UsuarioCreacion = value; } }

        public string FechaCreacion { get { return _FechaCreacion; } set { _FechaCreacion = value; } }


    }
}