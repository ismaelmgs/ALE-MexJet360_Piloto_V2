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
    public class EstadoCuenta_Presenter : BasePresenter<IViewEstadoCuenta>
    {
        private readonly DBEstadoCuenta oIGestCat;
        public EstadoCuenta_Presenter(IViewEstadoCuenta oView, DBEstadoCuenta oGC)
            : base(oView)
        {
            oIGestCat = oGC;
            oIView.eObjCliente += SearchObjCiente_Presenter;
            oIView.eObjContrato += SearchObjContrato_Presenter;
            oIView.eObjVueloHhead += SearchObjVueloHead_Presenter;
            oIView.eObjVueloHDetail += SearchObjVueloDetail_Presenter;
            oIView.eObjVueloEqDif += SearchObjVueloEqDif_Presenter;
            oIView.eObjHeadEdoCnta += SearchObjHeadEdoCnta_Presenter;
        }
        protected void SearchObjCiente_Presenter(object sender, EventArgs e)
        {
            oIView.LoadCliente(oIGestCat.dtObjCliente());
        }
        protected void SearchObjContrato_Presenter(object sender, EventArgs e)
        {
            oIView.LoadContrato(oIGestCat.dtObjContrato(oIView.oArrFiltros));
        }
        protected void SearchObjVueloHead_Presenter(object sender, EventArgs e)
        {
            oIView.LoadVueloHead(oIGestCat.dtObjVuelosHead(oIView.oArrVuelosHead));
        }
        protected void SearchObjHeadEdoCnta_Presenter(object sender, EventArgs e)
        {
            oIView.LoadHeadEdoCnta(oIGestCat.dtObjHeadEdoCnta(oIView.oArrVuelosHead));
        }
        protected void SearchObjVueloDetail_Presenter(object sender, EventArgs e)
        {
            oIView.LoadVueloDetail(oIGestCat.dtObjVuelosDetail(oIView.oArrVuelosHead));
        }
        protected void SearchObjVueloEqDif_Presenter(object sender, EventArgs e)
        {
            oIView.LoadVueloEqDif(oIGestCat.dtObjVuelosEquiposDiferentes(oIView.oArrVuelosHead));
        }
    }
}