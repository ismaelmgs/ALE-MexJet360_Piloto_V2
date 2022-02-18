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
    public class BitacoraAudit_Presenter : BasePresenter<IViewBitacoraAudit>
    {
        private readonly DBBitacoraAudit oIGestCat;

        public BitacoraAudit_Presenter(IViewBitacoraAudit oView, DBBitacoraAudit oGC)
            : base(oView)
        {
            oIGestCat = oGC;
            oIView.eObjAccion += SearchObjAccion_Presenter;
            oIView.eObjModulo += SearchObjModulo_Presenter;
            oIView.eObjUsuario += SearchObjUsuario_Presenter;
        }

        protected  void SearchObjAccion_Presenter(object sender, EventArgs e)
        {
            oIView.LoadAccion(oIGestCat.dtObjAccion());
        }

        protected void SearchObjModulo_Presenter(object sender, EventArgs e)
        {
            oIView.LoadModulo(oIGestCat.dtObjModulos());
        }
        protected void SearchObjUsuario_Presenter(object sender, EventArgs e)
        {
            oIView.LoadUsuario(oIGestCat.dtObjUsuario());
        }

        protected override void SearchObj_Presenter(object sender, EventArgs e)
        {
            oIView.LoadBitacoraAudit(oIGestCat.dtObjBitacora(oIView.oArrFiltros));
        }
    }
}