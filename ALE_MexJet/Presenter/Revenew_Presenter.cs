using ALE_MexJet.DomainModel;
using ALE_MexJet.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ALE_MexJet.Presenter
{
    public class Revenew_Presenter : BasePresenter<IViewRevenew>
    {
        private readonly DBRevenew oIGestCat;

        public Revenew_Presenter(IViewRevenew oView, DBRevenew oGC)
            : base(oView)
        {
            oIGestCat = oGC;

            oIView.eGetDescuento += eGetDescuento_Presenter;
            oIView.eUpdateDescuento += eUpdateDescuento_Presenter;
        }

        private void eUpdateDescuento_Presenter(object sender, EventArgs e)
        {
            oIGestCat.DBSetActualizaDescuentoTipoCliente(oIView.oDescReve);
        }

        private void eGetDescuento_Presenter(object sender, EventArgs e)
        {
            oIView.LoadDescuento(oIGestCat.DBGetObtieneDescuentoPorTipoCliente(oIView.iIdDescuento));
        }

        protected override void SearchObj_Presenter(object sender, EventArgs e)
        {
            oIView.LoadTiposCliente(oIGestCat.DBGetObtieneTiposClientes);
        }

        protected override void ObjSelected_Presenter(object sender, EventArgs e)
        {
            oIView.dtDescuentos = oIGestCat.DBGetObtieneDescuentosTiposCliente(oIView.iIdTipoCliente);
        }

        protected override void SaveObj_Presenter(object sender, EventArgs e)
        {
            if (oIGestCat.DBSetInsertaDescuento(oIView.oDescReve))
                oIView.MostrarMensaje("Se insertó el descuento correctamente", "Aviso");
            else
                oIView.MostrarMensaje("Ocurrio un error al insertar el descuento", "Aviso");
        }

        protected override void DeleteObj_Presenter(object sender, EventArgs e)
        {
            oIGestCat.DBSetEliminaDescuentoTipocliente(oIView.iIdDescuento);
            //SearchObj_Presenter(sender, e);
        }
    }
}