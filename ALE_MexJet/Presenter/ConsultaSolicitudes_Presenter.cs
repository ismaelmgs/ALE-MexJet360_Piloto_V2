using ALE_MexJet.DomainModel;
using ALE_MexJet.Interfaces;
using ALE_MexJet.Objetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NucleoBase.Core;
using DevExpress.Web.Data;

namespace ALE_MexJet.Presenter
{
    public class ConsultaSolicitudesPresenter : BasePresenter <IViewConsultaSolicitudes>
    {
        private readonly DBConsultaSolicitudes oIGestCat;

        public ConsultaSolicitudesPresenter(IViewConsultaSolicitudes oView, DBConsultaSolicitudes oGC)
            : base(oView)
        {
            oIGestCat = oGC;
            oIView.eGetObjects += eGetObjects_Presenter;
            oIView.eObjConsultaSolicitudes += SearchObjConsultaSolicitudes_Presenter;
            oIView.eGetContratos += eGetContratos_Presenter;
        }

        protected void eGetObjects_Presenter(object sender, EventArgs e)
        {
            try
            {
               oIView.LoadEstatusSolicitud(oIGestCat.dtGetEstatusSolicitud);

               oIView.LoadCliente(new DBCliente().DBSearchObj("@CodigoCliente", string.Empty,
                                                               "@Nombre", string.Empty,
                                                               "@TipoCliente", string.Empty,
                                                               "@estatus", 1));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected override void SearchObj_Presenter(object sender, EventArgs e)
        {
            oIView.LoadConsultaSolicitudes(oIGestCat.dtGetConsultaUltimasSolicitudes);
        }

        protected void eGetContratos_Presenter (object sender, EventArgs e)
        {
            oIView.LoadContrato(new DBBitacora().dtObjContrato(oIView.oBitacoraContrato));
        }
        
        protected void SearchObjConsultaSolicitudes_Presenter(object sender, EventArgs e)
        {
            oIView.LoadConsultaSolicitudes(oIGestCat.dtObjConsultaSolicitudes(oIView.oConsultaSolicitudesConsultaSolicitudes));
        }
    }
}