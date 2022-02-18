using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ALE_MexJet.Objetos
{
    [Serializable, Bindable(BindableSupport.Yes)]
    public class TarifaGrupoModeloEz : BaseObjeto
    {
        private int _iId = -1;
        private int _iGrupoModelo = 0;
        public string _sDescripcion = string.Empty;
        private decimal _dTarifaNal = 0;
        private decimal _dTarifaInt = 0;
        private int _iStatus = 1;
        private string _sUsuarioCreacion = string.Empty;
        private DateTime _dtFechaCreacion = new DateTime();
        private string _sUsuarioMod = string.Empty;
        private DateTime _dtFechaMod = new DateTime();
        private string _sIP = string.Empty;

        public int iId { get { return _iId; } set { _iId = value; } }
        public int iGrupoModelo { get { return _iGrupoModelo; } set { _iGrupoModelo = value; } }
        public string sDescripcion { get { return _sDescripcion; } set { _sDescripcion = value; } }
        public decimal dTarifaNal { get { return _dTarifaNal; } set { _dTarifaNal = value; } }
        public decimal dTarifaInt { get { return _dTarifaInt; } set { _dTarifaInt = value; } }
        public int iStatus { get { return _iStatus; } set { _iStatus = value; } }
        public string sUsuarioCreacion { get { return _sUsuarioCreacion; } set { _sUsuarioCreacion = value; } }
        public DateTime dtFechaCreacion { get { return _dtFechaCreacion; } set { _dtFechaCreacion = value; } }
        public string sUsuarioMod { get { return _sUsuarioMod; } set { _sUsuarioMod = value; } }
        public DateTime dtFechaMod { get { return _dtFechaMod; } set { _dtFechaMod = value; } }
        public string sIP { get { return _sIP; } set { _sIP = value; } }
    }
}