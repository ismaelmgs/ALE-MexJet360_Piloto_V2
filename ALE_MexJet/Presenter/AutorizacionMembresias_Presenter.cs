using ALE_MexJet.DomainModel;
using ALE_MexJet.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ALE_MexJet.Presenter
{
    public class AutorizacionMembresias_Presenter : BasePresenter<IViewAutorizacionMembresias>
    {
        private readonly DBAutorizacionMembresias oIGesCat;

        public AutorizacionMembresias_Presenter(IViewAutorizacionMembresias oView, DBAutorizacionMembresias oCI) 
			: base(oView)
		{
            oIGesCat = oCI;

		}

        protected override void ObjSelected_Presenter(object sender, EventArgs e)
        {
            oIView.dtContratos = new DBAutorizacionMembresias().ConsultaContratosEzMexJet;
            oIView.dtTiposClientes = oIGesCat.ConsultaTiposSubscripcionesDisponiebles;
        }

        protected override void SearchObj_Presenter(object sender, EventArgs e)
        {
            oIView.LoadObjects(oIGesCat.ConsultaMiembrosPorStatus(oIView.oArrFiltros));
        }

        protected override void SaveObj_Presenter(object sender, EventArgs e)
        {
            if (oIGesCat.ActualizaMiembro(oIView.oMiembro))
            {
                oIView.LoadObjects(oIGesCat.ConsultaMiembrosPorStatus(oIView.oArrFiltros));
                oIView.MostrarMensaje("Se actualizó el registro correctamente", "Aviso");
            }
            else
                oIView.MostrarMensaje("Ocurrio un error al actualizar", "Aviso");
        }

        protected override void NewObj_Presenter(object sender, EventArgs e)
        {
            oIGesCat.ActualizaEstatusMiembro(oIView.iIdMiembro, oIView.iIdStatus);
        }
    }
}