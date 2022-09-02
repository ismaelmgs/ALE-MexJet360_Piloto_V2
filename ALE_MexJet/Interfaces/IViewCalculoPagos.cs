using ALE_MexJet.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE_MexJet.Interfaces
{
    public interface IViewCalculoPagos : IBaseView
    {
        string sParametro { get; set; }
        string sFechaDesde { get; set; }
        string sFechaHasta { get; set; }
        void LoadConceptos(DataTable dt);
        void LoadPilotos(DataTable dt);
        void LoadVuelos(DataTable dt);

        List<CantidadComidas> oLstCant { set; get; }
        void LlenaCalculoPilotos(DataTable dt);

        event EventHandler eSearchConceptos;
        event EventHandler eSearchVuelos;
        event EventHandler eSearchCalculos;
    }
}
