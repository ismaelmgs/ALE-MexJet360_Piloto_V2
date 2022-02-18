using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ALE_MexJet.Objetos
{
    public class FormulaRemision
    {
        public int idFormula { get; set; }
        public string codigoF { get; set; }
        public string descripcion { get; set; }
        public string formula { get; set; }
        public int sts { get; set; }
        public DateTime fechaRegistro { get; set; }
        public string usuarioRegistro { get; set; }
    }
}