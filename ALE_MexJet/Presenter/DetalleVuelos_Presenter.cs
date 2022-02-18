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
    public class DetalleVuelos_Presenter : BasePresenter<IViewDetalleVuelos>
    {
        private readonly DBDetalleVuelos oIGestCat;

        public DetalleVuelos_Presenter(IViewDetalleVuelos oView, DBDetalleVuelos oGC)
            : base(oView)
        {
            oIGestCat = oGC;
            oIView.eObjCliente += SearchObjCiente_Presenter;
            oIView.eObjContrato += SearchObjContrato_Presenter;
            oIView.eSearchObj += SearchObjVueloCliente_Presenter;
            oIView.eObjMatricula += SearchObjVueloMatricula_Presenter;
        }
        protected void SearchObjCiente_Presenter(object sender, EventArgs e)
        {
            oIView.LoadCliente(oIGestCat.dtObjCliente());
        }
        protected void SearchObjContrato_Presenter(object sender, EventArgs e)
        {
            oIView.LoadContrato(oIGestCat.dtObjContrato(oIView.oArrFiltros));
        }
        protected void SearchObjVueloCliente_Presenter(object sender, EventArgs e)
        {
            oIView.LoadVueloCliente(oIGestCat.dtObjVueloCliente(oIView.oArrCliente));
        }
        protected void SearchObjVueloMatricula_Presenter(object sender, EventArgs e)
        {
            oIView.LoadVueloMatricula(oIGestCat.dtObjVueloMatricula(oIView.oArrMatricula));
        }
    }
}