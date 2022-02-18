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
    public class AeronaveRen_Presenter : BasePresenter<IViewAeronaveRen>
    {
        private readonly DBAeronaveRen oIGestCat;

        public AeronaveRen_Presenter(IViewAeronaveRen oView, DBAeronaveRen oGC)
            : base(oView)
        {
            oIGestCat = oGC;
            oIView.eSearchObj += ConsultaAeronaveRen_Presenter;
        }

        public void ConsultaAeronaveRen_Presenter(object sender, EventArgs e)
        {
            try
            {
                oIView.LoadAeronaveRen(oIGestCat.dtObjsAeroRen(oIView.oArrFiltros));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}