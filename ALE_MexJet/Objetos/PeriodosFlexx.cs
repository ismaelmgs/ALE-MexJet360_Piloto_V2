using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ALE_MexJet.Objetos
{
    public class PeriodosFlexx
    {
        private int _id;
        private DateTime _Inicio;
        private DateTime _Fin;

        public int id { get => _id; set => _id = value; }
        public DateTime Inicio { get => _Inicio; set => _Inicio = value; }
        public DateTime Fin { get => _Fin; set => _Fin = value; }
    }
}