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
    public class Bitacoras_Presenter : BasePresenter<IViewBitacoras>
    {
        private readonly DBBitacoras oIGestCat;
        public Bitacoras_Presenter(IViewBitacoras oView, DBBitacoras oGC)
           : base(oView)
        {
            oIGestCat = oGC;
            oIView.eSearchTipo += SearchTipo_Presenter;
        }
        protected override void SearchObj_Presenter(object sender, EventArgs e)
        {
            oIView.LoadBitacoras(oIGestCat.DBGetBitacoras(oIView.sParametro));
        }
        protected override void NewObj_Presenter(object sender, EventArgs e)
        {
            oIView.iOk = oIGestCat.DBSetBitacoras(oIView.oBi);
        }
        protected void SearchTipo_Presenter(object sender, EventArgs e)
        {
            oIView.LoadTipo(oIGestCat.DBGetTipo());
        }


    }
}