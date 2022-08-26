using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using NucleoBase.Core;
using ALE_MexJet.DomainModel;
using ALE_MexJet.Interfaces;
using ALE_MexJet.Objetos;

namespace ALE_MexJet.Presenter
{
    public class RevenewM_Presenter : BasePresenter<IViewRevenewM>
    {
        private readonly DBRevenewM oIGestCat;
        public RevenewM_Presenter(IViewRevenewM oView, DBRevenewM oGC)
            : base(oView)
        {
            oIGestCat = oGC;
            oIView.eSaveParametro += SaveParametro_Presenter;
            oIView.eSaveParametroAd += SaveParametroAd_Presenter;
            oIView.eSearchCuenta += SearchCuenta_Presenter;
            oIView.eSearchCuentasGrales += SearchCuentasGrales_Presenter;
            oIView.eInsertaCuentas += InsertaCuentas_Presenter;
        }

        protected override void SearchObj_Presenter(object sender, EventArgs e)
        {
            oIView.LoadConfiguracion(oIGestCat.DBConsultaConfiguracionViaticos());
        }
        protected override void SaveObj_Presenter(object sender, EventArgs e)
        {
            oIView.sOk = oIGestCat.DBActualizaConfiguracionConceptos(oIView.oC);
        }
        protected void SaveParametro_Presenter(object sender, EventArgs e)
        {
            oIView.sOk = oIGestCat.DBActualizaParametro(oIView.oP);
        }
        protected void SaveParametroAd_Presenter(object sender, EventArgs e)
        {
            oIView.sOk = oIGestCat.DBSetParametroAdicional(oIView.oPA);
        }

        public void SearchCuenta_Presenter(object sender, EventArgs e)
        {
            oIView.LoadDatosCuenta(oIGestCat.DBConsultaDatosCuenta(oIView.sNumCuenta));
        }

        public void SearchCuentasGrales_Presenter(object sender, EventArgs e)
        {
            oIView.LoadDatosCuentasGrales(oIGestCat.DBConsultaDatosCuentasGenerales());
        }
        public void InsertaCuentas_Presenter(object sender, EventArgs e)
        {
            oIView.iBan = oIGestCat.DBSetInsertaCuentas(oIView.ListaCuentas);
        }
    }
}