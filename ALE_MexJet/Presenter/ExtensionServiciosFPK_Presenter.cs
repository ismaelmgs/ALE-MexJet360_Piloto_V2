using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ALE_MexJet.Objetos;
using ALE_MexJet.DomainModel;
using ALE_MexJet.Interfaces;

namespace ALE_MexJet.Presenter
{
    public class ExtensionServiciosFPK_Presenter : BasePresenter<IViewExtensionServiciosFPK>
    {
        private readonly DBExtensionServiciosFPK oFPK;

        public ExtensionServiciosFPK_Presenter(IViewExtensionServiciosFPK oView, DBExtensionServiciosFPK oGC)
            : base(oView)
        {
            oFPK = oGC;

            oIView.eObtieneLicenciaPiloto += ObtieneLicenciaPiloto_Presenter;

        }

        protected void ObtieneLicenciaPiloto_Presenter(object sender, EventArgs e)
        {
            string sCrewCode = sender.ToString();

            oIView.sLicenciaPiloto = oFPK.DBObtieneLicenciaPiloto(sCrewCode);
        }
    }
}