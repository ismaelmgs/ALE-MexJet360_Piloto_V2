using ALE_MexJet.DomainModel;
using ALE_MexJet.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using NucleoBase.Core;

namespace ALE_MexJet.Presenter
{
    public class ConsultaViaticosVuelos_Presenter : BasePresenter<IViewConsultaViaticosVuelos>
    {
        private readonly DBConsultaViaticosVuelos oIGestCat;
        public ConsultaViaticosVuelos_Presenter(IViewConsultaViaticosVuelos oView, DBConsultaViaticosVuelos oGC)
            : base(oView)
        {
            oIGestCat = oGC;
        }
        protected override void SearchObj_Presenter(object sender, EventArgs e)
        {
            oIView.LoadViaticosVuelos(oIGestCat.GetVuelosViaticos(oIView.sFechaDesde, oIView.sFechaHasta, oIView.sParametro));
        }

    }
}