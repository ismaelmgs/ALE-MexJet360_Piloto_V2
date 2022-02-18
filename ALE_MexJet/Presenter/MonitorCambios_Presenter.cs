using ALE_MexJet.DomainModel;
using ALE_MexJet.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ALE_MexJet.Presenter
{
    public class MonitorCambios_Presenter : BasePresenter<IViewMonitorCambios>
    {
        private readonly DBMonitorCambios oIGestCat;

        public MonitorCambios_Presenter(IViewMonitorCambios oView, DBMonitorCambios oGC)
            : base(oView)
        {
            oIGestCat = oGC;
            oIView.eSearchObj += oIView_eSearchObj;
            oIView.eSearchDetalle += oIView_eSearchDetalle;
            oIView.eActualizaStatus += oIView_eActualizaStatus;
        }

        void oIView_eActualizaStatus(object sender, EventArgs e)
        {
            int id = oIGestCat.DBUpdateStatus(oIView.oArrActualizaStatus);
            //if (id > 0)
            //{                
                oIView.loadConsultaMonitorNotificaciones();
            //}
        }
        
        void oIView_eSearchObj(object sender, EventArgs e)
        {
            oIView.CargaConsultaMonitorNotificaciones(oIGestCat.DBSearchObj(new object[]{}));
        }

        void oIView_eSearchDetalle(object sender, EventArgs e)
        {
            oIView.CargaConsultaCambiosDetalle(oIGestCat.DBSearchObjDetalle(oIView.oArrConsultaCambiosDetalle));
        }
       
    }
}