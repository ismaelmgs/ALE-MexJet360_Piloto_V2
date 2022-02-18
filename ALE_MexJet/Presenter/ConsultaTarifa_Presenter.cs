using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ALE_MexJet.Interfaces;
using ALE_MexJet.DomainModel;
using ALE_MexJet.Objetos;
using DevExpress.Web.Data;
using NucleoBase.Core;
using System.Data;

namespace ALE_MexJet.Presenter
{
    public class ConsultaTarifa_Presenter : BasePresenter<IViewConsultaTarifa>
    {
        private readonly DBConsultaTarifa oIGestCat;

        public ConsultaTarifa_Presenter(IViewConsultaTarifa oView, DBConsultaTarifa oGC)
            : base(oView)
        {
            oIGestCat = oGC;
            oIView.eGetCliente += eGetCliente_Presenter;
            oIView.eGetDetalleGastoInterno += eGetConsultaTar_Presenter;
            oIView.eGetContrato += eGetContrato_Presenter;
                         
        }
        protected void eGetCliente_Presenter(object sender, EventArgs e)
        {
            oIView.LoadClientes(oIGestCat.DBSearchCliente(oIView.oArrFiltroCliente));
        }
        private void eGetContrato_Presenter(object sender, EventArgs e)
        {
            oIView.LoadContrato(oIGestCat.DBSearchContrato(oIView.oArrFiltroContrato));
        }

        private void eGetConsultaTar_Presenter(object sender, EventArgs e)
        {
            oIView.LoadTarifas(oIGestCat.DBCargaDetalle(oIView.oArrFiltroTarifas));
        }


    }
}