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
    public class IncrementoTarifas_Presenter : BasePresenter<IViewIncrementoTarifas>
    {
        private readonly DBIncrementoTarifas oIGestCat;

        public IncrementoTarifas_Presenter(IViewIncrementoTarifas oView, DBIncrementoTarifas oGC)
            : base(oView)
        {
            oIGestCat = oGC;
            oIView.eGetIncrementos += eGetIncrementos_Presenter;
            oIView.eGetAplicaIncrementos += oIView_eGetAplicaIncrementos;
        }

        void oIView_eGetAplicaIncrementos(object sender, EventArgs e)
        {
            oIGestCat.DBSetInsertaHistIncrementoTarifas(oIView.oTarifa);
        }

        protected void eGetIncrementos_Presenter(object sender, EventArgs e)
        {
            int iTipo = oIView.oArrFiltros[1].S().I();
            int iMes = oIView.oArrFiltros[3].S().I();
            oIView.LoadIncrementoTarifas(oIGestCat.DBGetCalculoIncrementoTarifas(iTipo, iMes));
        }
    }
}