using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ALE_MexJet.Interfaces;
using ALE_MexJet.DomainModel;
using ALE_MexJet.Views;

namespace ALE_MexJet.Presenter
{
    public class ErroresBitacora_Presenter : BasePresenter<IViewErroresBitacora>
    {
        private readonly DBErroresBitacora oIEB;

        public ErroresBitacora_Presenter(IViewErroresBitacora oView, DBErroresBitacora oDB)
            : base(oView)
        {
            oIEB = oDB;
            oIView.eSearchObj += oIView_eSearchObj;
        }

        void oIView_eSearchObj(object sender, EventArgs e)
        {
            oIView.LoadObjects(oIEB.DBConsultaErrores(oIView.oArrFiltros));
        }
    }
}