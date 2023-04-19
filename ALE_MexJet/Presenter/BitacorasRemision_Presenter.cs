using ALE_MexJet.DomainModel;
using ALE_MexJet.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using NucleoBase.Core;
using ALE_MexJet.Objetos;
using ALE_MexJet.Clases;

namespace ALE_MexJet.Presenter
{
    public class BitacorasRemision_Presenter : BasePresenter<IViewBitacorasRemision>
    {
        private readonly DBBitacorasRemision oIGestCat;
        public BitacorasRemision_Presenter(IViewBitacorasRemision oView, DBBitacorasRemision oGC)
            : base(oView)
        {
            oIGestCat = oGC;
        }
        protected override void SearchObj_Presenter(object sender, EventArgs e)
        {
            oIView.LoadBitacoras(oIGestCat.GetBitacorasSinRemisiones());
        }
    }
}