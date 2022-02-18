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
    public class DistOrtPend_Presenter : BasePresenter<IVIewDistOrtPend>
    {
         private readonly DBDistOrtPend oIGestCat;

         public DistOrtPend_Presenter(IVIewDistOrtPend oView, DBDistOrtPend oGC)
            : base(oView)
        {
            oIGestCat = oGC;
            oIView.eSearchObj += LoadObjects_Presenter;
        }

         public void LoadObjects_Presenter(object sender, EventArgs e)
         {
             oIView.LoadGv(oIGestCat.dtObjDistOrtPend());
         }

    }
}