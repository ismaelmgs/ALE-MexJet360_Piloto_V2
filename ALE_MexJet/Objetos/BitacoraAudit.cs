using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ALE_MexJet.Objetos
{
    [Serializable, Bindable(BindableSupport.Yes)]
    public class BitacoraAudit : BaseObjeto
    {
        private int _iIdModulo = 0;
        private int _iIdAccion = 0;
        private string _sUsuario = string.Empty;
        private string _sFechaIni = string.Empty;
        private string _sFechaFin = string.Empty;
        private string _sIP = string.Empty;

        [Display(AutoGenerateField = false), ScaffoldColumn(false)]

        public int iIdModulo { get { return _iIdModulo; } set { _iIdModulo = value; } }
        public int iIdAccion { get { return _iIdAccion; } set { _iIdAccion = value; } }
        public string sUsuario { get { return _sUsuario; } set { _sUsuario = value; } }
        public string sFechaIni { get { return _sFechaIni; } set { _sFechaIni = value; } }
        public string sFechaFin { get { return _sFechaFin; } set { _sFechaFin = value; } }
        public string sIP { get { return _sIP; } set { _sIP = value; } }

    }
}