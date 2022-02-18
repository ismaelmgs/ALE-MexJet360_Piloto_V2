using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ALE_MexJet.Objetos
{
    [Serializable, Bindable(BindableSupport.Yes)]
    public class CombustibleSemanalDetalle : BaseObjeto
    {
        private int _iIdCombustibleSemDet = 0;
        private int _iIdCombustibleSem = 0;
        private int _iSemana = 0;
        private decimal _dCostoXLitro = 0;
        private decimal _dGalonMXN = 0;
        private decimal _dGalonUSD = 0;
        private decimal _dTipoCambio = 0;
        private int _iStatus = 1;
        private string _sUsuarioCreacion = string.Empty;
        private DateTime _dtFechaCreacion = new DateTime();
        private string _sUsuarioMod = string.Empty;
        private DateTime _dtFechaMod = new DateTime();
        private string _sIP = string.Empty;

        [Display(AutoGenerateField = false), ScaffoldColumn(false)]
        public int iIdCombustibleSemDet { get { return _iIdCombustibleSemDet; } set { _iIdCombustibleSemDet = value; } }
        public int iIdCombustibleSem { get { return _iIdCombustibleSem; } set { _iIdCombustibleSem = value; } }
        public int iSemana { get { return _iSemana; } set { _iSemana = value; } }
        public decimal dCostoXLitro { get { return _dCostoXLitro; } set { _dCostoXLitro = value; } }
        public decimal dGalonMXN { get { return _dGalonMXN; } set { _dGalonMXN = value; } }
        public decimal dGalonUSD { get { return _dGalonUSD; } set { _dGalonUSD = value; } }
        public decimal dTipoCambio { get { return _dTipoCambio; } set { _dTipoCambio = value; } }


        [Display(Name = "¿Activo? "), Required]
        public int iStatus { get { return _iStatus; } set { _iStatus = value; } }

        public string sUsuarioCreacion { get { return _sUsuarioCreacion; } set { _sUsuarioCreacion = value; } }

        public DateTime dtFechaCreacion { get { return _dtFechaCreacion; } set { _dtFechaCreacion = value; } }

        public string sUsuarioMod { get { return _sUsuarioMod; } set { _sUsuarioMod = value; } }

        public DateTime dtFechaMod { get { return _dtFechaMod; } set { _dtFechaMod = value; } }

        public string sIP { get { return _sIP; } set { _sIP = value; } }
    }
}