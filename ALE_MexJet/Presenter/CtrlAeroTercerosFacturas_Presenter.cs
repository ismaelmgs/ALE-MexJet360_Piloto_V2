using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ALE_MexJet.Interfaces;
using ALE_MexJet.DomainModel;

namespace ALE_MexJet.Presenter
{
    public class CtrlAeroTercerosFacturas_Presenter : BasePresenter<IViewCtrlAeroTercerosFacturas>
    {
         private readonly DBCtrlAeroTercerosFacturas oIGestCat;

         public CtrlAeroTercerosFacturas_Presenter(IViewCtrlAeroTercerosFacturas oView, DBCtrlAeroTercerosFacturas oGC)
            : base(oView)
        {
            oIGestCat = oGC;
            oIView.eSearchObj += oIView_eSearchObj;
        }

        void oIView_eSearchObj(object sender, EventArgs e)
        {
            oIView.LoadReporteFacturas(oIGestCat.DBConsultaReporteFacturas(oIView.oArrFiltroReporteFacturas));
        }
    }
}
