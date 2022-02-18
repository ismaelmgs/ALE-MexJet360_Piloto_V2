using ALE_MexJet.DomainModel;
using ALE_MexJet.Interfaces;
using ALE_MexJet.Objetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NucleoBase.Core;
using DevExpress.Web.Data;
using ALE_MexJet.Clases;

namespace ALE_MexJet.Presenter
{
    public class ConsultaVuelos_Presenter : BasePresenter<IViewConsultaVuelos>
    {
        private readonly DBConsultaVuelos oIGestCat;

        public ConsultaVuelos_Presenter(IViewConsultaVuelos oView, DBConsultaVuelos oGC)
            : base(oView)
        {
            oIGestCat = oGC;
            oIView.eSearchVuelos += SearchVuelos_Presenter;
            oIView.eSearchParametros += SearchParametros_Presenter;
        }

        public void SearchVuelos_Presenter(object sender, EventArgs e)
        {
            oIView.LoadVuelos(oIGestCat.DBConsultaVuelosUSA_FPK(oIView.sFecha));
        }
        public void SearchParametros_Presenter(object sender, EventArgs e)
        {
            oIView.LoadParametros(oIGestCat.DBConsultaParametros());
        }
    }
}