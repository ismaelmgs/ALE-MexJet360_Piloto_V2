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
    public class ConsultaContratos_Presenter : BasePresenter<iViewConsultaContrato>
    {
        private readonly DBConsultaContrato oIGestCat;

        public ConsultaContratos_Presenter(iViewConsultaContrato oView, DBConsultaContrato oGC)
            : base(oView)
        {
            oIGestCat = oGC;
            oIView.eSearchContratosCliente += SearchContratoCliente;
            oIView.eSearchKardex += SearchKardex_Presenter;
        }

        protected override void SearchObj_Presenter(object sender, EventArgs e)
        {
            oIView.dtClientes = oIGestCat.dtClientes;
            oIView.dtContratos = oIGestCat.DBSearchObj(oIView.oArrFiltros);
        }

        protected void SearchContratoCliente(object sender, EventArgs e)
        {
            oIView.dtContratosCliente = oIGestCat.DBGetContrato(oIView.iIdCliente);
        }
        protected void SearchKardex_Presenter(object sender, EventArgs e)
        {
            oIView.LoadKardex(oIGestCat.DBGetKardexContrato(oIView.iIdContrato));
        }

    }
}