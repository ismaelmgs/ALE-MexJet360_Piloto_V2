using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ALE_MexJet.Interfaces;
using ALE_MexJet.DomainModel;

namespace ALE_MexJet.Presenter
{
    class HistoricoTarifasCliente_Presenter : BasePresenter<IViewHistoricoTarifasCliente>
    {
        private readonly DBHistoricoTarifasCliente oIGestCat;

        public HistoricoTarifasCliente_Presenter(IViewHistoricoTarifasCliente oView, DBHistoricoTarifasCliente oBC): base (oView)
        {
            oIGestCat = oBC;
            oIView.eGetClientes += oIView_eGetClientes;
            oIView.eGetContrato += oIView_eGetContrato;
            oIView.eGetHistoricoTarifas += oIView_eGetHistoricoTarifas;
        }

        void oIView_eGetHistoricoTarifas(object sender, EventArgs e)
        {
            oIView.LoadHistoricoTarifas(oIGestCat.DBSearchHistoricoTarifasCliente(oIView.oArrFiltroHistoricoTarifas));
        }

        void oIView_eGetContrato(object sender, EventArgs e)
        {
            oIView.LoadContrato(oIGestCat.DBSearchContrato(oIView.oArrFiltroContrato));
        }

        void oIView_eGetClientes(object sender, EventArgs e)
        {
            oIView.LoadClientes(oIGestCat.DBSearchCliente(oIView.oArrFiltroClientes));
        }
         
    }
}
