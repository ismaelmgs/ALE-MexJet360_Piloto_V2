using ALE_MexJet.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE_MexJet.Interfaces
{
    public interface IViewReporteFl3xx : IBaseView
    {
        string sFechaDesde { get; set; }
        string sFechaHasta { get; set; }
        void LoadFl3xx(DataTable dt);
    }
}
