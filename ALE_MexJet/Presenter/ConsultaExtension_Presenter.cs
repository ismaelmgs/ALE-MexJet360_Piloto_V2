using ALE_MexJet.DomainModel;
using ALE_MexJet.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ALE_MexJet.Presenter
{
    public class ConsultaExtension_Presenter : BasePresenter<IViewConsultaExtension>
    {
        private readonly DBConsultaExtension oIGestCat;
        public ConsultaExtension_Presenter(IViewConsultaExtension oView, DBConsultaExtension oGC)
            : base(oView)
        {
            oIGestCat = oGC;


        }

        protected override void SearchObj_Presenter(object sender, EventArgs e)
        {
            oIView.LoadExtensiones(oIGestCat.DBGetObtieneExtensionesServicio(oIView.oArrFil));
        }
    }
}