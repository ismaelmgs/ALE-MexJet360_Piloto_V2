using ALE_MexJet.Clases;
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
    public class PubFerrys_Presenter : BasePresenter<IViewPubFerrys>
    {
        private readonly DBPubFerrys oIGestCat;

        public PubFerrys_Presenter(IViewPubFerrys oView, DBPubFerrys oGC)
            : base(oView)
        {
            oIGestCat = oGC;

            oIView.eLoadOrigDestFiltro += eLoadOrigDestFiltro_Presenter;
            oIView.eLoadOrigDestFiltroDest += eLoadOrigDestFiltroDest_Presenter;
            oIView.eSavFerryPendiente += SaveObjFerryPendiente_Presenter;
            oIView.SearchMatricula += SearchMatricula_Presenter;
        }

        protected void eLoadOrigDestFiltro_Presenter(object sender, EventArgs e)
        {
            oIView.dtOrigen = new DBPresupuesto().GetAeropuertosOrigen(oIView.sFiltroO, 2);
        }

        protected void eLoadOrigDestFiltroDest_Presenter(object sender, EventArgs e)
        {
            oIView.dtDestino = new DBPresupuesto().GetAeropuertosDestino(oIView.sFiltroD, string.Empty, 2);
        }

        protected override void NewObj_Presenter(object sender, EventArgs e)
        {
            OfertaFerry oF = oIView.oFerrysPadre;
            
            oIView.iIdFerry = oIGestCat.DBSetInsertaFerry(oF, oIView.oLstFerrysHijo);
            if (oIView.iIdFerry > 0)
            {
                oIView.ConfimaPublicacion("Aviso", "Ferry guardado con exito!, ¿Deseas publicarlo ahora?");
            }
            else
            {
                oIView.MostrarMensaje("Aviso", "Error al guardar el ferry");
            }
        }

        protected void SaveObjFerryPendiente_Presenter(object sender, EventArgs e)
        {
            //List<OfertaFerry> oLst = oIView.oLstFerrysPendiente;

            //if (oIGestCat.DBSetInsertaOfertaFerryPendiente(oLst))
            //{
            //    oIView.LoadFerrys(oIGestCat.DBGetFerrysPeriodoPendiente(0));//oIView.iIdPadre));
            //    oIView.MostrarMensaje("Los Ferrys se registraron temporalmente.", "Aviso");
            //}
        }

        protected void SearchMatricula_Presenter(object sender, EventArgs e)
        {
            oIView.dtMatriculas = oIGestCat.DBSearchMatriculasMexJetExceptoLearJet45();
        }

    }
}