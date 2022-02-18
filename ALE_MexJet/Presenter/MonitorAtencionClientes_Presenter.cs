using ALE_MexJet.DomainModel;
using ALE_MexJet.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ALE_MexJet.Presenter
{
    public class MonitorAtencionClientes_Presenter : BasePresenter<IViewMonitorAtencionClientes>
    {
        private readonly DBMonitorAtencionClientes oIGestCat;

        public MonitorAtencionClientes_Presenter(IViewMonitorAtencionClientes oView, DBMonitorAtencionClientes oGC)
            : base(oView)
        {
            oIGestCat = oGC;
            oIView.eSearchObj += oIView_eSearchObj;
            oIView.eSearchDetalle += oIView_eSearchDetalle;
            oIView.eBuscaNotas += oIView_eBuscaNotas;
            oIView.eSearchAropuertoBase += oIView_eSearchAropuertoBase;
            oIView.eActualizaStatus += oIView_eActualizaStatus;
            oIView.eEditaAtnCliente += oIView_EditaAtnCliente;
        }

        void oIView_eActualizaStatus(object sender, EventArgs e)
        {
            int id = oIGestCat.DBUpdateStatus(oIView.oArrActualizaStatus);
            if (id > 0)
            {                
                oIView.loadConsultaMonitorAtencionClientes();
            }
        }
        void oIView_eSearchAropuertoBase(object sender, EventArgs e)
        {
            oIView.CargaAeropuertoBase(oIGestCat.DBSearchAeropuertosBase());
        }
        void oIView_eBuscaNotas(object sender, EventArgs e)
        {
            oIView.CargaNotas(oIGestCat.DBConsultaNotas(oIView.oArrConsultaNotas));
        }
        void oIView_eSearchDetalle(object sender, EventArgs e)
        {
            oIView.CargaConsultaMonitorAtencionClientesDetalle(oIGestCat.DBSearchObjDetalle(oIView.oArrConsultaMonitorAtencionClientesDetalle));
        }
        void oIView_eSearchObj(object sender, EventArgs e)
        {
            oIView.CargaConsultaMonitorAtencionClientes(oIGestCat.DBSearchObj(oIView.oArrConsultaMonitorAtencionClientes));
        }
        void oIView_EditaAtnCliente(object sender, EventArgs e)
        {
            int id = oIGestCat.DBUpdateAtnCliente(oIView.oArrEditaAtncclientes);
            if (id > 0)
            {
                oIView.loadConsultaMonitorAtencionClientes();        
            }
        }
    }
}