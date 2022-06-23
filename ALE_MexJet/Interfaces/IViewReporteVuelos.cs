using ALE_MexJet.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE_MexJet.Interfaces
{
    public interface IViewReporteVuelos : IBaseView
    {
        string sFecha { set; get; }
        string sFecha2 { set; get; }
        string sTripNum { set; get; }
        int iRes { set; get; }
        void LoadVuelos(DataTable dt);
        List<Vuelo> ListaVuelos { set; get; }
    }
}
