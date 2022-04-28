using ALE_MexJet.DomainModel;
using ALE_MexJet.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ALE_MexJet.Objetos;

namespace ALE_MexJet.Presenter
{
    public class Remision_Presenter : BasePresenter<IViewRemision>
    {
        private readonly DBRemision oIGestCat;

        public Remision_Presenter(IViewRemision oView, DBRemision oGC)
            : base(oView)
        {
            oIGestCat = oGC;
            oIView.eSearchMotivos += SearchMotivos_Presenter;
            oIView.eInsertAjuste += InsertAjuste_Presenter;
            oIView.eValidateObj += eValidateObj_Presenter;

            LoadObjects_Presenter();                       
        }

        void oView_eGetCancelaRemision(object sender, EventArgs e)
        {
            //oIView.MostrarMensaje("Desea Cancelar la remision","Aviso");
        }

        public void LoadObjects_Presenter()
        {
            oIView.LoadObjects(oIGestCat.DBSearchObj(oIView.oArrFiltros));
        }
        protected override void DeleteObj_Presenter(object sender, EventArgs e)
        {
            try
            {
                int id = oIGestCat.DBCancelaRemision(oIView.oRemision);
                if (id > 0)
                {
                    //oIView.MostrarMensaje(Mensajes.LMensajes[Enumeraciones.Mensajes.Eliminacion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);                    
                    //recargamos nuevamente los elentos del grid
                    oIView.LoadObjects(oIGestCat.DBSearchObj(oIView.oArrFiltros));
                    oIView.MostrarMensaje("Se cancelo correctamente la remisión", "Aviso");
                }
                else
                {
                    oIView.MostrarMensaje("No se puede cancelar la remisión, ya que se encuentra prefacturado", "Aviso");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void SearchMotivos_Presenter(object sender, EventArgs e)
        {
            oIView.LoadMotivos(oIGestCat.DBGetMotivos());
        }
        protected void InsertAjuste_Presenter(object sender, EventArgs e)
        {
            oIView.iIdAjuste = oIGestCat.DBSetInsertaAjuste(oIView.oAjuste);

            if(oIView.iIdAjuste > 0)
                oIView.MostrarMensaje("Se registro correctamente el ajuste de la remisión", "Aviso");
            else
                oIView.MostrarMensaje("No se puede registrar el ajuste de la remisión, revisar por favor", "Aviso");
        }

        protected void eValidateObj_Presenter(object sender, EventArgs e)
        {
            oIView.setParameters(oIGestCat.getParameters());
            oIView.isValidUser(oIGestCat.ValidarUsuario(oIView.sEmail));
        }
    }
}