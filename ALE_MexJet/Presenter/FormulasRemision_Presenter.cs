using ALE_MexJet.Clases;
using ALE_MexJet.DomainModel;
using ALE_MexJet.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ALE_MexJet.Presenter
{
    public class FormulasRemision_Presenter : BasePresenter<IViewFormulasRemision>
    {
        private readonly DBFormulas oIGestCat;

        public FormulasRemision_Presenter(IViewFormulasRemision oView, DBFormulas oGC)
            : base(oView)
        {
            oIGestCat = oGC;
            oIView.eGetFactores += eGetFactores_Presenter;
        }

        protected override void SearchObj_Presenter(object sender, EventArgs e)
        {
            oIView.dtFormulasRemision = oIGestCat.DBSearchObj();
            oIView.LoadGridView();
        }

        protected void eGetFactores_Presenter(object sender, EventArgs e)
        {
            DataSet ds = new DBFormulas().DBGetFactores();
            oIView.dtFactoresFijos = ds.Tables[0];
            oIView.dtFactoresVariables = ds.Tables[1];
            oIView.LoadObjects();
        }

        //Insertar
        protected override void NewObj_Presenter(object sender, EventArgs e)
        {
            int iRes = oIGestCat.DBSaveObj(oIView.oFormula);
            if (iRes == 0)
            {
                oIView.MostrarMensaje("Ocurrió un error al insertar la formula", "Aviso");
            }
        }

        // Actualizar
        protected override void SaveObj_Presenter(object sender, EventArgs e)
        {
            int iRes = oIGestCat.DBUpdateObj(oIView.oFormula);
            if(iRes == 0)
            {
                oIView.MostrarMensaje("Ocurrió un error al actualizar la formula", "Aviso");
            }
        }

        //Eliminar
        protected override void DeleteObj_Presenter(object sender, EventArgs e)
        {
            int iRes = oIGestCat.DBDelete(oIView.oFormula);
            if (iRes == 0)
            {
                oIView.MostrarMensaje("Ocurrió un error al eliminar la formula", "Aviso");
            }
        }
    }
}