using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE_MexJet.Interfaces
{
    public interface IViewConsultaViaticosVuelos : IBaseView
    {
        string sParametro { get; set; }
        string sFechaDesde { get; set; }
        string sFechaHasta { get; set; }
        void LoadViaticosVuelos(DataTable dt);
    }
}
