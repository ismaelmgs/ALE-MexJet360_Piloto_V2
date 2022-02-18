using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ALE_MexJet.Objetos
{
    [Serializable, Bindable(BindableSupport.Yes)]
    public class Contrato_Intercambios
    {
        private int _iId = -1;
        private int _iIdContrato = -1;
        private int _iIdGrupoModelo = -1;
        private bool _bAplicaFerry = false;
        private decimal _dFactor = 0m;
        private decimal _dTarifaNalDlls = 0m;
        private decimal _dTarifaIntDlls = 0m;
        private decimal _dGalones = 0m;
        private decimal _dCDN = 0m;
        private decimal _dCDI = 0m;

        private string _sNotas = string.Empty;
        public string sNotas { get { return _sNotas; } set { _sNotas = value; } }

        public int iId { get { return _iId; } set { _iId = value; } }
        public int iIdContrato { get { return _iIdContrato; } set { _iIdContrato = value; } }
        public int iIdGrupoModelo { get { return _iIdGrupoModelo; } set { _iIdGrupoModelo = value; } }
        public bool bAplicaFerry { get { return _bAplicaFerry; } set { _bAplicaFerry = value; } }
        public decimal dFactor { get { return _dFactor; } set { _dFactor = value; } }
        public decimal dTarifaNalDlls { get { return _dTarifaNalDlls; } set { _dTarifaNalDlls = value; } }
        public decimal dTarifaIntDlls { get { return _dTarifaIntDlls; } set { _dTarifaIntDlls = value; } }
        public decimal dGalones { get { return _dGalones; } set { _dGalones = value; } }
        public decimal dCDN { get { return _dCDN; } set { _dCDN = value; } }
        public decimal dCDI { get { return _dCDI; } set { _dCDI = value; } }
    }
}