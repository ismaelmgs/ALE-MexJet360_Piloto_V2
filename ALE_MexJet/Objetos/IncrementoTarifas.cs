using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ALE_MexJet.Objetos
{
    public class IncrementoTarifas : BaseObjeto
    {
        private int _iIdContrato = 0;
        private int _iIdConcepto = 0;
        private decimal _dImporteOri = 0;
        private string _sInflacionDesc = string.Empty;
        private decimal _dPorcentaje = 0;
        private decimal _dMasPuntos = 0;
        private decimal _dTope = 0;
        private decimal _dInflacion = 0;
        private decimal _dImporteNuevo = 0;
        private int _iAnio = 0;
        public int iIdContrato { get { return _iIdContrato; } set { _iIdContrato = value; } }
        public int iIdConcepto { get { return _iIdConcepto; } set { _iIdConcepto = value; } }
        public decimal dImporteOri { get { return _dImporteOri; } set { _dImporteOri = value; } }
        public string sInflacionDesc { get { return _sInflacionDesc; } set { _sInflacionDesc = value; } }
        public decimal dPorcentaje { get { return _dPorcentaje; } set { _dPorcentaje = value; } }
        public decimal dMasPuntos { get { return _dMasPuntos; } set { _dMasPuntos = value; } }
        public decimal dTope { get { return _dTope; } set { _dTope = value; } }
        public decimal dInflacion { get { return _dInflacion; } set { _dInflacion = value; } }
        public decimal dImporteNuevo { get { return _dImporteNuevo; } set { _dImporteNuevo = value; }}
        public int iAnio { get { return _iAnio; } set { _iAnio = value; } }
    }

}