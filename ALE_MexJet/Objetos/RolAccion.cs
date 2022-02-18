using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace ALE_MexJet.Objetos
{
    [Serializable, Bindable(BindableSupport.Yes)]
    public class RolAccion : BaseObjeto
    {
        private int _iIdRolAccion = -1;
        private int _iIdRol = 0;
        private int _iIdModulo = 0;
        private int _iIdAccion = 0;
        private byte _bPermitido = 1;
        private int _iStatus = 1;
        private string _sUsuarioCreacion = string.Empty;
        private DateTime _dtFechaCreacion = new DateTime();
        private string _sUsuarioMod = string.Empty;
        private DateTime _dtFechaMod = new DateTime();
        private string _sIP = string.Empty;
        private DataTable _dtaRolAccion;

        [Display(AutoGenerateField = false), ScaffoldColumn(false)]
        public int iIdRolAccion { get { return _iIdRolAccion; } set { _iIdRolAccion = value; } }
        public int iIdRol { get { return _iIdRol; } set { _iIdRol = value; } }
        public int iIdModulo { get { return _iIdModulo; } set { _iIdModulo = value; } }
        public int iIdAccion { get { return _iIdAccion; } set { _iIdAccion = value; } }
        public byte bPermitido { get { return _bPermitido; } set { _bPermitido = value; } }
        public int iStatus { get { return _iStatus; } set { _iStatus = value; } }
        public string sUsuarioCreacion { get { return _sUsuarioCreacion; } set { _sUsuarioCreacion = value; } }
        public DateTime dtFechaCreacion { get { return _dtFechaCreacion; } set { _dtFechaCreacion = value; } }
        public string sUsuarioMod { get { return _sUsuarioMod; } set { _sUsuarioMod = value; } }
        public DateTime dtFechaMod { get { return _dtFechaMod; } set { _dtFechaMod = value; } }
        public string sIP { get { return _sIP; } set { _sIP = value; } }
        public DataTable dtaRolAccion { get { return _dtaRolAccion; } set { _dtaRolAccion = value; } }

    }
}