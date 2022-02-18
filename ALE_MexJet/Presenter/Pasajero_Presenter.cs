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
    public class Pasajero_Presenter : BasePresenter<IViewPasajero>
    {
        private readonly DBPasajero oIGestCat;

        public Pasajero_Presenter(IViewPasajero oView, DBPasajero oGC)
            : base(oView)
        {
            oIGestCat = oGC;

            oView.eConsultaCliente += ConsultaCliente_Presenter;
            oView.eConsultaPasajeroId += ConsultaPasajeroId_Presenter;
            oView.eSearchObj += ConsultaPasajeros_Presenter;
            oView.eEliminaPasajeroPorId += EliminaPasajeroPorId_Presenter;
            oView.eGuardaPasajeroPerfil += GuardaPasajeroPerfil_Presenter;
            oView.eActualizaPasajeroPerfil += ActualizaPasajeroPerfil_Presenter;
        }

        public void ConsultaCliente_Presenter(object sender, EventArgs e)
        {
            try
            {
                oIView.ConsultaCliente(oIGestCat.DBConsultaCliente(oIView.oArrFiltrosCliente));
            }
            catch (Exception ex)
            {
                
                oIView.MostrarMensaje("Se detectó el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
            }
        }
        public void ConsultaPasajeroId_Presenter(object sender, EventArgs e)
        {
            try
            {
                oIView.ConsultaPasajeroId(oIGestCat.DBConsultaPasajeros(oIView.oArrFiltros));
            }
            catch (Exception ex)
            {
                
                oIView.MostrarMensaje("Se detectó el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
            }
        }
        public void ConsultaPasajeros_Presenter(object sender, EventArgs e)
        {
            try
            {
                oIView.ConsultaPasajeros(oIGestCat.DBConsultaPasajeros(oIView.oArrFiltros));

                oIView.iIdPasajero = -1;

                oIView.ConsultaPasajeros(oIGestCat.DBConsultaPasajeros(oIView.oArrFiltros));

            }
            catch (Exception ex)
            {

                oIView.MostrarMensaje("Se detectó el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
            }
        }
        public void GuardaPasajeroPerfil_Presenter(object sender, EventArgs e)
        {
            try
            {
                oIGestCat.DBInsertaPasajeroPerfil(oIView.objPasajero);

                oIView.iIdPasajero = -1;

                oIView.ConsultaPasajeros(oIGestCat.DBConsultaPasajeros(oIView.oArrFiltros));

            }
            catch (Exception ex)
            {
                
                oIView.MostrarMensaje("Se detectó el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
            }
        }
        public void ActualizaPasajeroPerfil_Presenter(object sender, EventArgs e)
        {
            try
            {
                
                oIGestCat.DBActualizaPasajeroPerfil(oIView.objPasajero);

                oIView.iIdPasajero = -1;

                oIView.ConsultaPasajeros(oIGestCat.DBConsultaPasajeros(oIView.oArrFiltros));

            }
            catch (Exception ex)
            {

                oIView.MostrarMensaje("Se detectó el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
            }
        }
        public void EliminaPasajeroPorId_Presenter(object sender, EventArgs e)
        {
            try
            {
                int iIdPasajero = sender.S().I();

                oIGestCat.DBEliminaPasajeroPorId(iIdPasajero);

                oIView.iIdPasajero = -1;

                oIView.ConsultaPasajeros(oIGestCat.DBConsultaPasajeros(oIView.oArrFiltros));

            }
            catch (Exception ex)
            {

                oIView.MostrarMensaje("Se detectó el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
            }
        }
    }
}