using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ALE_MexJet.Interfaces;
using ALE_MexJet.DomainModel;

namespace ALE_MexJet.Presenter
{
    public class Dashboard_Presenter : BasePresenter<IViewDashboard>
    {
        private readonly DBDashboard oIGestCat;        
        
        public Dashboard_Presenter(IViewDashboard oView, DBDashboard oBC): base(oView)
        {
            oIGestCat = oBC;
            oView.eGetDashboardAvisos += oView_eGetDashboardAvisos;
            oView.eGetDashboardClientes += oView_eGetDashboardClientes;
            oView.eGetDashboardVuelos += oView_eGetDashboardVuelos;
        }             

        void oView_eGetDashboardAvisos(object sender, EventArgs e)
        {
            oIView.LoadDashboardAvisos(oIGestCat.DBConsultaAvisos());       
        }

        void oView_eGetDashboardClientes(object sender, EventArgs e)
        {
            oIView.LoadDashboardClientes(oIGestCat.DBConsultaClientes());
        }

        void oView_eGetDashboardVuelos(object sender, EventArgs e)
        {
            oIView.LoadDashboardVuelos(oIGestCat.DBConsultaVuelos());
        }   
    }
}
