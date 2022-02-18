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
    public class DocPendientes_Presenter : BasePresenter<IViewDocPendientes>
    {
        private readonly DBDocPendientes oIGestCat;

        public DocPendientes_Presenter(IViewDocPendientes oView, DBDocPendientes oGC)
            : base(oView)
        {
            oIGestCat = oGC;
            oIView.eObjCliente += SearchObjCiente_Presenter;
            oIView.eObjConrato += SearchObjContrato_Presenter;
            oIView.eObjBitPend += SearchObjBitPend_Presenter;
            oIView.eObjRemPend += SearchObjRemPend_Presenter;
            oIView.eObjFacPend += SearchObjFacPend_Presenter;
        }

        protected void SearchObjCiente_Presenter(object sender, EventArgs e)
        {
            oIView.LoadCliente(oIGestCat.dtObjCliente());
        }
        protected void SearchObjContrato_Presenter(object sender, EventArgs e)
        {
            oIView.LoadContrato(oIGestCat.dtObjContrato(oIView.oDocContrato));
        }
        protected void SearchObjBitPend_Presenter(object sender, EventArgs e)
        {
            oIView.LoadBitPend(oIGestCat.dtObjBitPen(oIView.oBitPen));
        }
        protected void SearchObjRemPend_Presenter(object sender, EventArgs e)
        {
            oIView.LoadRemPend(oIGestCat.dtObjRemPen(oIView.oBitPen));
        }
        protected void SearchObjFacPend_Presenter(object sender, EventArgs e)
        {
            oIView.LoadFacPend(oIGestCat.dtObjFacPen(oIView.oBitPen));
        }
    }
}