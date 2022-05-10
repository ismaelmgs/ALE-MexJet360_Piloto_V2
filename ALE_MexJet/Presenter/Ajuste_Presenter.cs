using ALE_MexJet.DomainModel;
using ALE_MexJet.Interfaces;
using ALE_MexJet.Objetos;
using DevExpress.Web.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using NucleoBase.Core;
using System.Web.UI.WebControls;
using ALE_MexJet.Clases;
using DevExpress.Web;

namespace ALE_MexJet.Presenter
{
    public class Ajuste_Presenter : BasePresenter<IViewAjuste>
    {
        private readonly DBAjuste oIGestCat;

        public Ajuste_Presenter(IViewAjuste oView, DBAjuste oGC)
            : base(oView)
        {
            oIGestCat = oGC;
            oIView.eValidateObj += eValidateObj_Presenter;
        }

        protected override void SearchObj_Presenter(object sender, EventArgs e)
        {
            oIView.LoadContratos(oIGestCat.DBGetContratos());
            oIView.LoadMotivos(oIGestCat.DBGetMotivosAjuste());
        }

        protected override void ObjSelected_Presenter(object sender, EventArgs e)
        {
            oIView.LoadRemisiones(oIGestCat.DBGetRemisionesContrato(oIView.iIdContrato));
        }

        protected override void NewObj_Presenter(object sender, EventArgs e)
        {
            oIView.iIdAjuste = oIGestCat.DBSetInsertaAjuste(oIView.oAjuste);
        }
        protected void eValidateObj_Presenter(object sender, EventArgs e)
        {
            oIView.setParameters(oIGestCat.getParameters(1)); //Datos de template para autorizador
            oIView.setParametersNot(oIGestCat.getParameters(2)); //Datos de template para ejecutivo y vendedor (Notificaciones)
            oIView.isValidUser(oIGestCat.DBGetDatosAutorizador(oIView.iIdContrato));
        }
    }
}