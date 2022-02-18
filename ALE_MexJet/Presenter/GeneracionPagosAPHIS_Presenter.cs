using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ALE_MexJet.Interfaces;
using ALE_MexJet.DomainModel;

namespace ALE_MexJet.Presenter
{
    public class GeneracionPagosAPHIS_Presenter:BasePresenter<IViewGeneracionPagosAPHIS>
    {
        private readonly DBGeneracionPagosAPHIS oIDBAPHIS;

        public GeneracionPagosAPHIS_Presenter(IViewGeneracionPagosAPHIS oView,DBGeneracionPagosAPHIS oDato):base(oView)
        {
            oIDBAPHIS = oDato;
            oIView.eGetPagosAPHIS += oIView_eGetPagosAPHIS;
        }
      
        void oIView_eGetPagosAPHIS(object sender, EventArgs e)
        {
            oIView.LoadPagosAPHIS(oIDBAPHIS.GeneracionCalculoPagoAPHIS(oIView.oArrFiltroPagosAPHIS));
        }

    }
}