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
    public class AnalisisFerrys_Presenter : BasePresenter<IViewAnalisisFerrys>
    {
        private readonly DBAnalisisFerrys oIGestCat;

        public AnalisisFerrys_Presenter(IViewAnalisisFerrys oView, DBAnalisisFerrys oGC)
            : base(oView)
        {
            oIGestCat = oGC;
            oView.eSearchObj += SearchObj_Presenter;
            oView.eSerachPiernas += SearchObjPiernas_Presenter;
            oView.eSerachPiernaDetalle += SearchObjPiernasDetalle_Presenter;
            oView.eSaveObj += EditaTipoPierna_Presenter;
        }

        protected void SearchObj_Presenter(object sender, EventArgs e)
        {
            oIView.CargaDDl(oIGestCat.DBSearchObj(oIView.oArrFiltros));
        }
        protected void SearchObjPiernas_Presenter(object sender, EventArgs e)
        {
            oIView.CargaGrid(oIGestCat.DBSearchPiernas(oIView.oArrFiltrosPierna));
        }
        protected void SearchObjPiernasDetalle_Presenter(object sender, EventArgs e)
        {
            oIView.CargaGridDetalle(oIGestCat.DBSearchPiernasDetalle(oIView.oArrFiltrosPiernaDetalle));
        }
        public void EditaTipoPierna_Presenter(object sender, EventArgs e)
        {
            try
            {
                int id = oIGestCat.DBUpdate(oIView.oArrParametrosUpdate);
                if (id > 0)
                {
                    oIView.MostrarMensaje(Mensajes.LMensajes[Enumeraciones.Mensajes.Modificacion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}