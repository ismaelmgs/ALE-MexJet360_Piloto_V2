using ALE_MexJet.DomainModel;
using ALE_MexJet.Interfaces;
using ALE_MexJet.Objetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NucleoBase.Core;
using DevExpress.Web.Data;
using ALE_MexJet.Clases;

namespace ALE_MexJet.Presenter
{
    public class ServicioCargo_Presenter : BasePresenter<IViewConsultaServicioCargo>
    {
        private readonly DBServicioCargo oIGestCat;

        public ServicioCargo_Presenter(IViewConsultaServicioCargo oView, DBServicioCargo oGC)
            : base(oView)
        {
            oIGestCat = oGC;
            oIView.eSearchObj += CargaClientes_Presenter;
            oIView.eObjContrato += CargaContrato_Presenter;
            oIView.eObjServico += CargaServicio_Presenter;
        }

        public void CargaClientes_Presenter(object sender, EventArgs e)
        {
            oIView.CargaCliente(oIGestCat.dtObjCliente());
        }
        public void CargaContrato_Presenter(object sender, EventArgs e)
        {
            oIView.CargaContrato(oIGestCat.dtObjContrato(oIView.oArrContrato));
        }
        public void CargaServicio_Presenter(object sender, EventArgs e)
        {
            oIView.CargaGrid(oIGestCat.dtObjServicioCargo(oIView.oArrServicio));
        }

    }
}