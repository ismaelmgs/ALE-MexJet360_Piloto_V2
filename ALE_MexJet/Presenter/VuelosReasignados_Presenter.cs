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
    public class VuelosReasignados_Presenter : BasePresenter<IViewVuelosReasignados>
    {
        private readonly DBVuelosReasignados oIGestCat;

        public VuelosReasignados_Presenter(IViewVuelosReasignados oView, DBVuelosReasignados oGC)
            : base(oView)
        {
            oIGestCat = oGC;
            oIView.eSearchObj += SearchObjCiente_Presenter;
        }

        protected void SearchObjCiente_Presenter(object sender, EventArgs e)
        {
            oIView.LlenaGrid(oIGestCat.DBSearchObj(oIView.oArrFiltros));
        }
    }
}