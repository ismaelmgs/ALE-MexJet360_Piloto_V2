using ALE_MexJet.DomainModel;
using ALE_MexJet.Interfaces;
using ALE_MexJet.Objetos;
using DevExpress.Web.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using NucleoBase.Core;
using System.Web.UI.WebControls;
using ALE_MexJet.Clases;
using DevExpress.Web;

namespace ALE_MexJet.Presenter
{
    public class ReporteVuelos_Presenter : BasePresenter<IViewReporteVuelos>
    {
        private readonly DBReporteVuelos oIGestCat;
        public ReporteVuelos_Presenter(IViewReporteVuelos oView, DBReporteVuelos oGC)
            : base(oView)
        {
            oIGestCat = oGC;
        }
        protected override void SearchObj_Presenter(object sender, EventArgs e)
        {
            oIView.LoadVuelos(oIGestCat.DBGetVuelos(oIView.sFecha, oIView.sFecha2, oIView.sTripNum));
        }
        protected override void NewObj_Presenter(object sender, EventArgs e)
        {
            oIView.iRes = oIGestCat.DBSetVuelos(oIView.ListaVuelos);
        }
    }
}