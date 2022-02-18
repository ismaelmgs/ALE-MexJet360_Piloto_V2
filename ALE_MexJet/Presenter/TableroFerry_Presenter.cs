using ALE_MexJet.DomainModel;
using ALE_MexJet.Interfaces;
using ALE_MexJet.Objetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ALE_MexJet.Presenter
{
    public class TableroFerry_Presenter : BasePresenter<IViewTableroFerry>
    {
        private readonly DBTableroFerry oIGestCat;

        public TableroFerry_Presenter(IViewTableroFerry oView, DBTableroFerry oGC)
            : base(oView)
        {
            oIGestCat = oGC;

        }
        protected override void SaveObj_Presenter(object sender, EventArgs e)
        {
            TableroFerry oF = oIView.oTFerry;
            if(oIGestCat.DBSetActualizaEstatusFerry(oF))
                oIView.MostrarMensaje("Los Ferrys se actualizaron correctamente.", "Aviso");
            
        }
        protected override void SearchObj_Presenter(object sender, EventArgs e)
        {
            oIView.LoadFerrys(oIGestCat.DBGetFerrysPeriodo(oIView.oArrFiltros));
        }

        
    }
}