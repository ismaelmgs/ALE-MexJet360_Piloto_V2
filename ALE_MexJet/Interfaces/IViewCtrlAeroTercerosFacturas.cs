using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE_MexJet.Interfaces
{
    public interface IViewCtrlAeroTercerosFacturas : IBaseView
    {
        void LoadReporteFacturas(DataTable dtReporteFacturas);
        void ObtieneReporteFacturas();
        object[] oArrFiltroReporteFacturas { get; }
    }
}
