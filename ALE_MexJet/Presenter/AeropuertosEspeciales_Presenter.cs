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
    public class AeropuertosEspeciales_Presenter : BasePresenter<IViewAeropuertosEspeciales>
    {
        private readonly DBAeropuertosEspeciales oIGestCat;
        public AeropuertosEspeciales_Presenter(IViewAeropuertosEspeciales oView, DBAeropuertosEspeciales oGC)
            : base(oView)
        {
            oIGestCat = oGC;
            oIView.eSearchAeropuertos += SearchAeropuertos_Presenter;
            oIView.eSearchAeropuertosEspeciales += SearchAeropuertosEspeciales_Presenter;
        }
        protected void SearchAeropuertos_Presenter(object sender, EventArgs e)
        {
            oIView.LoadAeropuertos(oIGestCat.GetAeropuertos());
        }
        protected void SearchAeropuertosEspeciales_Presenter(object sender, EventArgs e)
        {
            oIView.LoadAeropuertosEspeciales(oIGestCat.GetAeropuertosEspeciales());
        }
        protected override void NewObj_Presenter(object sender, EventArgs e)
        {
            oIView.iOK = oIGestCat.SetInsertaAeropuertosEspeciales(oIView.sPOA);
        }
        protected override void DeleteObj_Presenter(object sender, EventArgs e)
        {
            bool bRes = oIGestCat.SetEliminarAeropuertoEspecial(oIView.iIdEspecial);
            if (bRes)
                oIView.iOK = 1;
            else
                oIView.iOK = 0;
        }
    }
}