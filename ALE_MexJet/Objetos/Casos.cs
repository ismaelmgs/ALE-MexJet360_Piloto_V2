using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ALE_MexJet.Objetos
{
    [Serializable, Bindable(BindableSupport.Yes)]
    public class Casos
    {
        private int _iId = -1;
        private int _iIdCaso = -1;
        private int _iIdTramo = -1;
        private int _iIdTipoCaso = -1;
        private int _iIdMotivo = -1;
        private int _iMinutos = 0;
        private int _iIdArea = -1;
        private int _iIdSolicitud = -1;
        private string _sDetalle = string.Empty;
        private string _sAccionCorrectiva = string.Empty;
        private bool _bOtorgado = false;
        private int _iStatus = 1;
        private string _sUsuarioCreacion = string.Empty;
        private DateTime _dtFechaCreacion = new DateTime();
        private string _sUsuarioMod = string.Empty;
        private DateTime _dtFechaMod = new DateTime();
        private string _sIP = string.Empty;
        private string _sFechaUltMov = string.Empty;

        public int iId { get { return _iId; } set { _iId = value; } }
        public int iIdCaso { get { return _iIdCaso; } set { _iIdCaso = value; } }
        public int iIdTramo { get { return _iIdTramo; } set { _iIdTramo = value; } }
        public int iIdTipoCaso { get { return _iIdTipoCaso; } set { _iIdTipoCaso = value; } }
        public int iIdMotivo { get { return _iIdMotivo; } set { _iIdMotivo = value; } }
        public int iMinutos { get { return _iMinutos; } set { _iMinutos = value; } }
        public int iIdArea { get { return _iIdArea; } set { _iIdArea = value; } }
        public int iIdSolicitud { get { return _iIdSolicitud; } set { _iIdSolicitud = value; } }
        public string sDetalle { get { return _sDetalle; } set { _sDetalle = value; } }
        public string sAccionCorrectiva { get { return _sAccionCorrectiva; } set { _sAccionCorrectiva = value; } }
        public bool bOtorgado { get { return _bOtorgado; } set { _bOtorgado = value; } }
        public int iStatus { get { return _iStatus; } set { _iStatus = value; } }

        public string sUsuarioCreacion { get { return _sUsuarioCreacion; } set { _sUsuarioCreacion = value; } }

        public DateTime dtFechaCreacion { get { return _dtFechaCreacion; } set { _dtFechaCreacion = value; } }

        public string sUsuarioMod { get { return _sUsuarioMod; } set { _sUsuarioMod = value; } }

        public DateTime dtFechaMod { get { return _dtFechaMod; } set { _dtFechaMod = value; } }

        public string sIP { get { return _sIP; } set { _sIP = value; } }

    }
}