using ALE_MexJet.DomainModel;
using ALE_MexJet.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ALE_MexJet.Objetos;

namespace ALE_MexJet.Presenter
{
    public class Autorizacion_Presenter : BasePresenter<IViewAutorizacion>
    {
        private readonly DBRemision oIGestCat;

        public Autorizacion_Presenter(IViewAutorizacion oView, DBRemision oGC)
            : base(oView)
        {
            oIGestCat = oGC;
        }

        protected override void SearchObj_Presenter(object sender, EventArgs e)
        {
            oIView.LoadRemision(oIGestCat.DBGetAjusteRemision(oIView.iIdRemision, oIView.iIdAjuste));
        }

        protected override void SaveObj_Presenter(object sender, EventArgs e)
        {
            int iRes = oIGestCat.DBSetActualizaAutorizacionAjuste(oIView.oAjuste);

            if (iRes > 0)
                oIView.MostrarMensaje("Se autorizó ajuste de remisión", "Aviso");
            else
                oIView.MostrarMensaje("No se puede registrar el ajuste de la remisión, revisar por favor", "Aviso");
        }
    }
}