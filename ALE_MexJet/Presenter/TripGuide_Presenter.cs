using ALE_MexJet.DomainModel;
using ALE_MexJet.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using ALE_MexJet.Objetos;

namespace ALE_MexJet.Presenter
{
    public class TripGuide_Presenter : BasePresenter<IViewTripGuide>
    {
        private readonly DBTripGuide oIGestCat;
        public TripGuide_Presenter(IViewTripGuide oView, DBTripGuide oGC)
            : base(oView)
        {
            oIGestCat = oGC;

            oIView.eGuardaTripGuide += oIView_eGuardaTripGuide;
            oIView.eConsultaContactoSolicitud += ConsultaContactoSolicitud_Presenter;
            oIView.eConsultaVendedorSolicitud += ConsultaVendedorSolicitud_Presenter;
        }

        void oIView_eGuardaTripGuide(object sender, EventArgs e)
        {
            oIGestCat.DBSave(oIView.TripGuide);
        }

        protected void ConsultaContactoSolicitud_Presenter(object sender, EventArgs e)
        {
            oIView.ConsultaContactoSolicitud(oIGestCat.DBConsultaContactoSolicitud(oIView.iIdSolicitud));
        }

        protected void ConsultaVendedorSolicitud_Presenter(object sender, EventArgs e)
        {
            oIView.ConsultaVendedorSolicitud(oIGestCat.DBConsultaVendedorSolicitud(oIView.iIdSolicitud));
        }



    }
}