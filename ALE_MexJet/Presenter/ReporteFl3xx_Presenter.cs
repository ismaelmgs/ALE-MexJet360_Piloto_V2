using ALE_MexJet.DomainModel;
using ALE_MexJet.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using NucleoBase.Core;
using ALE_MexJet.Objetos;
using ALE_MexJet.Clases;

namespace ALE_MexJet.Presenter
{
    public class ReporteFl3xx_Presenter : BasePresenter<IViewReporteFl3xx>
    {
        private readonly DBReporteFl3xx oIGestCat;
        public ReporteFl3xx_Presenter(IViewReporteFl3xx oView, DBReporteFl3xx oGC)
           : base(oView)
        {
            oIGestCat = oGC;
        }
        protected override void SearchObj_Presenter(object sender, EventArgs e)
        {
            oIView.LoadFl3xx(oIGestCat.GetReporteFl3xx(oIView.sFechaDesde, oIView.sFechaHasta));
        }
    }
}