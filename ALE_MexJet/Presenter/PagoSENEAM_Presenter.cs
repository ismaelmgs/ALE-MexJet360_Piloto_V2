using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ALE_MexJet.Interfaces;
using ALE_MexJet.DomainModel;

namespace ALE_MexJet.Presenter
{
    public class PagoSENEAM_Presenter : BasePresenter<IViewPagoSENEAM>
    {
        private readonly DBPagoSENEAM oIPagoSENEAM;

        public PagoSENEAM_Presenter(IViewPagoSENEAM oView, DBPagoSENEAM oDBPago):base(oView)
        {
            oIPagoSENEAM = oDBPago;
            oIView.eSearchObj += oIView_eSearchObj;
        }

        void oIView_eSearchObj(object sender, EventArgs e)
        {
            oIView.LoadObjects(oIPagoSENEAM.DBSearchPagoSENEAM(oIView.oArrFiltros));
        }
    }
}