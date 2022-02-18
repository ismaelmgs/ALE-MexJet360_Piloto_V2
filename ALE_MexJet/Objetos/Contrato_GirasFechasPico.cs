using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ALE_MexJet.Objetos
{
    [Serializable, Bindable(BindableSupport.Yes)]
    public class Contrato_GirasFechasPico
    {
        private int _iIdContrato = -1;
        private bool _bAplicaGiraEspera = false;
        private int _iNumeroVeces = 0;
        private bool _bAplicaGiraHora = false;
        private string _sHoraInicio = string.Empty;
        private string _sHoraFin = string.Empty;
        private decimal _dPorcentajeDescuento = 0m;

        private bool _bAplicaFactorFechaPico = false;
        private decimal _dFactorFechaPico = 0m;

        private string _sNotas = string.Empty;
        public string sNotas { get { return _sNotas; } set { _sNotas = value; } }

        public int iIdContrato { get { return _iIdContrato; } set { _iIdContrato = value; } }

        public bool bAplicaGiraEspera { get { return _bAplicaGiraEspera; } set { _bAplicaGiraEspera = value; } }
        public int iNumeroVeces { get { return _iNumeroVeces; } set { _iNumeroVeces = value; } }
        public bool bAplicaGiraHora { get { return _bAplicaGiraHora; } set { _bAplicaGiraHora = value; } }
        public string sHoraInicio { get { return _sHoraInicio; } set { _sHoraInicio = value; } }
        public string sHoraFin { get { return _sHoraFin; } set { _sHoraFin = value; } }
        public decimal dPorcentajeDescuento { get { return _dPorcentajeDescuento; } set { _dPorcentajeDescuento = value; } }

        public bool bAplicaFactorFechaPico { get { return _bAplicaFactorFechaPico; } set { _bAplicaFactorFechaPico = value; } }
        public decimal dFactorFechaPico { get { return _dFactorFechaPico; } set { _dFactorFechaPico = value; } }
     

    }
}