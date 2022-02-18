using ALE_MexJet.DomainModel;
using ALE_MexJet.Interfaces;
using ALE_MexJet.Objetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NucleoBase.Core;

namespace ALE_MexJet.Presenter
{
    public class Encuesta_Presenter : BasePresenter<IBaseView>
    {
        private readonly DBEncuesta oIGestCat;

        public Encuesta_Presenter(IBaseView oView, DBEncuesta oGC)
            :base(oView)
        {
            oIGestCat = oGC;
        }

        protected override void NewObj_Presenter(object sender, EventArgs e)
        {
            //Encuesta oEncuesta = new Encuesta();
            //DevExpress.Web.Data.ASPxDataInsertingEventArgs eI = (DevExpress.Web.Data.ASPxDataInsertingEventArgs) e;

            //oEncuesta.iIdPadre = eI.NewValues["IdPadre"].S().I();
            //oEncuesta.sDescripcionNodo = eI.NewValues["Descripcion"].S();

            //if(oEncuesta != null)
            //{
            //    oIGestCat.InsertaNodoEncuesta(oEncuesta);
            //}
        }
    }
}