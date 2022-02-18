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
    public class DashboarsAtnCliente_Presenter : BasePresenter<IViewDashboardAtnCliente>
    {
        private readonly DBDashboardAtnCliente  oIGestCat;

        public DashboarsAtnCliente_Presenter(IViewDashboardAtnCliente oView, DBDashboardAtnCliente oGC)
            : base(oView)
        {
            oIGestCat = oGC;
            oIView.eSearchObj += LoadObjects_Presenter;
            oIView.eSolicitud += LoadSol_Presenter;
            oIView.eCasos += LoadSCaso_Presenter;
        }
        public void LoadObjects_Presenter(object sender, EventArgs e)
        {
            oIView.LoadObjects(oIGestCat.DBSearchObj(oIView.oArrFiltros));
        }
        public void LoadSol_Presenter(object sender, EventArgs e)
        {
            oIView.LoadSol(oIGestCat.DBSolicitudes(oIView.oArrFilSol));
        }
        public void LoadSCaso_Presenter(object sender, EventArgs e)
        {
            oIView.LoadCaso(oIGestCat.DBCasos(oIView.oArrCasos));
        }

    }
}