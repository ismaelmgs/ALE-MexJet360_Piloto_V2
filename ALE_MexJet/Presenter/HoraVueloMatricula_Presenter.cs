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
    public class HoraVueloMatricula_Presenter : BasePresenter<IViewHoraVueloMatricula>
    {
        private readonly DBHoraVueloMatricula oIGestCat;
        public HoraVueloMatricula_Presenter(IViewHoraVueloMatricula oView, DBHoraVueloMatricula oGC)
            : base(oView)
        {
            oIGestCat = oGC;
            oIView.eObjCliente += SearchObjCiente_Presenter;
            oIView.eObjContrato += SearchObjContrato_Presenter;
            oIView.eObjConsultaCliente += SearchObjConsultaCliente_Presenter;
            oIView.eObjConsultaMatricula += SearchObjConsultaMatricula_Presenter;
            oIView.eObjFlota += SearchObjFlota_Presenter;
            oIView.eObjConsultaFlota += SearchObjConsultaFlota_Presenter;
        }
        protected void SearchObjCiente_Presenter(object sender, EventArgs e)
        {
            oIView.LoadCliente(oIGestCat.dtObjCliente());
        }
        protected void SearchObjContrato_Presenter(object sender, EventArgs e)
        {
            oIView.LoadContrato(oIGestCat.dtObjContrato(oIView.oArrFiltros));
        }
        protected void SearchObjConsultaCliente_Presenter(object sender, EventArgs e)
        {
            oIView.LoadConsultaCliente(oIGestCat.dtObjConsultaCliente(oIView.oArrClient));
        }
        protected void SearchObjConsultaMatricula_Presenter(object sender, EventArgs e)
        {
            oIView.LoadConsultaMatricula(oIGestCat.dtObjConsultaMatricula(oIView.oArrMatricula));
        }
        protected void SearchObjFlota_Presenter(object sender, EventArgs e)
        {
            oIView.LoadFlota(oIGestCat.dtObjFlota());
        }
        protected void SearchObjConsultaFlota_Presenter(object sender, EventArgs e)
        {
            oIView.LoadConsultaFlota(oIGestCat.dtObjConsultaFlota(oIView.oArrFlota));
        }
    }
}