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
    public class BitacoraPeticion_Presenter : BasePresenter<IViewBitacoraPeticion>
    {
        private readonly DBBitacoraPeticion oIGestCat;
        public BitacoraPeticion_Presenter(IViewBitacoraPeticion oView, DBBitacoraPeticion oGC)
           : base(oView)
        {
            oIGestCat = oGC;
            oIView.eObjProcesar += SearchObjProcesar_Presenter;
        }
        protected override void SearchObj_Presenter(object sender, EventArgs e)
        {
            oIView.LoadFl3xx(oIGestCat.getFlights(oIView.sFechaDesde, oIView.sFechaHasta, oIView.stripNumber));
        }

        protected void SearchObjProcesar_Presenter(object sender, EventArgs e)
        {
            oIGestCat.setFlights(oIView.flights);
            oIView.getPostflight(oIGestCat.postFlights(oIView.flights));
            oIGestCat.setPostFlight(oIView.lstPostFlights);
            oIGestCat.createBitacora(oIView.flights, oIView.lstPostFlights);
            oIView.vuelosProcesados(oIGestCat.validarBitacora());
        }
    }
}