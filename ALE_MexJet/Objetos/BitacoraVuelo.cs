using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ALE_MexJet.Objetos
{
    [Serializable, Bindable(BindableSupport.Yes)]
    public class BitacoraVuelo : BaseObjeto
    {
        private int _iIdBitacora = 0;
        private int _iId = 0;
        private long _lLegId = 0;
        private string _sFolio = string.Empty;
        private string _sFlightOff = string.Empty;
        private string _sFlightOn = string.Empty;
        private string _sFlightDiff = string.Empty;
        private string _sCalzoIn = string.Empty;
        private string _sCalzoOut = string.Empty;
        private string _sCalzoDiff = string.Empty;
        private string _sFuelIn = string.Empty;
        private string _sFuelOut = string.Empty;
        private string _sFuelDiff = string.Empty;
        private string _sUser = string.Empty;

        public int IIdBitacora { get => _iIdBitacora; set => _iIdBitacora = value; }
        public int IId { get => _iId; set => _iId = value; }
        public long LLegId { get => _lLegId; set => _lLegId = value; }
        public string SFolio { get => _sFolio; set => _sFolio = value; }
        public string SFlightOff { get => _sFlightOff; set => _sFlightOff = value; }
        public string SFlightOn { get => _sFlightOn; set => _sFlightOn = value; }
        public string SFlightDiff { get => _sFlightDiff; set => _sFlightDiff = value; }
        public string SCalzoIn { get => _sCalzoIn; set => _sCalzoIn = value; }
        public string SCalzoOut { get => _sCalzoOut; set => _sCalzoOut = value; }
        public string SCalzoDiff { get => _sCalzoDiff; set => _sCalzoDiff = value; }
        public string SFuelIn { get => _sFuelIn; set => _sFuelIn = value; }
        public string SFuelOut { get => _sFuelOut; set => _sFuelOut = value; }
        public string SFuelDiff { get => _sFuelDiff; set => _sFuelDiff = value; }
        public string SUser { get => _sUser; set => _sUser = value; }
    }
}