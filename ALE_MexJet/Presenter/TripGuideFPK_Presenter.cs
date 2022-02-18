using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ALE_MexJet.Objetos;
using ALE_MexJet.DomainModel;
using ALE_MexJet.Interfaces;

namespace ALE_MexJet.Presenter
{
    public class TripGuideFPK_Presenter : BasePresenter<IViewTripGuideFPK>
    {
        private readonly DBTripGuideFPK oFPK;

        public TripGuideFPK_Presenter(IViewTripGuideFPK oView, DBTripGuideFPK oGC)
            : base(oView)
        {
            oFPK = oGC;

            oIView.eSearchLegsByTrip += SearchLegsByTrip_Presenter;
            oIView.eBuscaDatosTrip += oIView_eBuscaDatosTrip;
            oIView.eBuscaDetallePierna += oIView_eBuscaDetallePierna;
            oIView.eBuscaPasajero += oIView_eBuscaPasajero;
            oIView.eBuscaCoordenadaOrigen += oIView_eBuscaCoordenadaOrigen;
            oIView.eBuscaCoordenadaDestino += oIView_eBuscaCoordenadaDestino;
        }

        protected void oIView_eBuscaDatosTrip(object sender, EventArgs e)
        {
            oIView.ObtieneDatosTrip(oFPK.DBGetObtieneDatosTrip(oIView.iTrip));
        }

        protected void SearchLegsByTrip_Presenter(object sender, EventArgs e)
        {
            oIView.CargaPiernas(oFPK.DBGetObtienePiernasPorTrip(oIView.iTrip));
        }

        protected void oIView_eBuscaDetallePierna(object sender, EventArgs e)
        {
            oIView.CargaDetallePierna(oFPK.DBGetObtieneDetallePierna(oIView.iPierna));
        }

        protected void oIView_eBuscaPasajero(object sender, EventArgs e)
        {
            oIView.ObtienePasajeros(oFPK.DBGetObtienePasajeros(oIView.iPierna));
        }

        protected void oIView_eBuscaCoordenadaOrigen(object sender, EventArgs e)
        {
            oIView.ObtieneCoordenadaOrigen(oFPK.DBObtieneCoordenadas(oIView.sICAOOrigen));
        }

        protected void oIView_eBuscaCoordenadaDestino(object sender, EventArgs e)
        {
            oIView.ObtieneCoordenadaDestino(oFPK.DBObtieneCoordenadas(oIView.sICAODestino));
        }

    }
}