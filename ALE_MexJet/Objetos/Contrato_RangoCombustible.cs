using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ALE_MexJet.Objetos
{
    public class Contrato_RangoCombustible
    {
        private int _iId = -1;
        private int _iIdContrato = -1;
        private int _iIdGrupoModelo = -1;
        private int _iIdTipo = -1;
        private decimal _dDesde = 0m;
        private decimal _dHasta = 0m;
        private decimal _dIncremento = 0m;


        public int iId { get { return _iId; } set { _iId = value; } }
        public int iIdContrato { get { return _iIdContrato; } set { _iIdContrato = value; } }
        public int iIdGrupoModelo { get { return _iIdGrupoModelo; } set { _iIdGrupoModelo = value; } }
        public int iIdTipo { get { return _iIdTipo; } set { _iIdTipo = value; } }

        public decimal dDesde { get { return _dDesde; } set { _dDesde = value; } }
        public decimal dHasta { get { return _dHasta; } set { _dHasta = value; } }
        public decimal dIncremento { get { return _dIncremento; } set { _dIncremento = value; } }

    }
}