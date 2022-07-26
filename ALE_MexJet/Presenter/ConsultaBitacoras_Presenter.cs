using ALE_MexJet.DomainModel;
using ALE_MexJet.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using NucleoBase.Core;

namespace ALE_MexJet.Presenter
{
    public class ConsultaBitacoras_Presenter : BasePresenter<IViewConsultaBitacoras>
    {
        private readonly DBConsultaBitacoras oIGestCat;
        public ConsultaBitacoras_Presenter(IViewConsultaBitacoras oView, DBConsultaBitacoras oGC)
            : base(oView)
        {
            oIGestCat = oGC;
            oIView.eUpdateSts += UpdateSts_Presenter;
            oIView.eSearchPhoto += SearchPhoto_Presenter;
        }
        protected override void SearchObj_Presenter(object sender, EventArgs e)
        {
            oIView.LoadBitacoras(oIGestCat.DBGetConsultaBitacoras(oIView.sParametro));
        }

        protected override void SaveObj_Presenter(object sender, EventArgs e)
        {
            oIView.bRes = oIGestCat.DBSetActualizaBitacora(oIView.oBi);
        }

        protected void UpdateSts_Presenter(object sender, EventArgs e)
        {
            oIView.bRes = oIGestCat.DBSetAutorizaBitacora(oIView.oBi);
        }

        protected void SearchPhoto_Presenter(object sender, EventArgs e)
        {
            oIView.LoadFoto(oIGestCat.DBGetFotoXBitacora(oIView.oBi));
        }

    }
}