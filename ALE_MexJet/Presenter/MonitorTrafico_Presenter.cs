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
    public class MonitorTrafico_Presenter : BasePresenter<IViewMonitorTrafico>
    {
        private readonly DBMonitorTrafico oIGestCat;

        public MonitorTrafico_Presenter(IViewMonitorTrafico oView, DBMonitorTrafico oGC)
            : base(oView)
        {
            oIGestCat = oGC;
            oIView.eSearchObj += SearchObjCiente_Presenter;
            oIView.eSearchDetalle += oIView_eSearchDetalle;
            oIView.eGuardaMonitorTrafico += oIView_eGuardaMonitorTrafico; 
            oIView.eBuscaAropuertoBase += oIView_eBuscaAropuertoBase;
            oIView.eBuscaPax += oIView_eBuscaPax;
            oIView.eBuscaNotas += oIView_eBuscaNotas;

            oIView.eNewTrip += NewObjTrip_Presenter;
            oIView.eLoadObjTrips += LoadObjectsTrips_Presenter;
            oIView.eSaveTrip += UpdateTrip;
            oView.eEliminaTripSolicitud += EliminaTrip_Presenter;
            oView.eValidaTrip += eValidaTrip_Presenter;
        }

        void oIView_eBuscaNotas(object sender, EventArgs e)
        {
            oIView.CargaNotas(oIGestCat.DBConsultaNotas(oIView.oArrConsultaNotas));
        }

        void oIView_eBuscaPax(object sender, EventArgs e)
        {
            oIView.CargaPax(oIGestCat.DBConsultaPax(oIView.oArrConsultaPax));
        }

        void oIView_eBuscaAropuertoBase(object sender, EventArgs e)
        {
            oIView.CargaAeropuertoBase(oIGestCat.DBBuscaAeropuertosBase());
        }

        void oIView_eGuardaMonitorTrafico(object sender, EventArgs e)
        {
            try
            {                
                //int id = oIGestCat.DBSaveTrip(oCatalogoTrip);
                //new DBMonitorTrafico().DBInsertaMonitorTrafico(1);
                //int id = oIGestCat.DBInsertaMonitorTrafico(oIView.iIdSolicitud);
                int id = oIGestCat.DBActualizaMonitorTrafico(oIView.oArrMonitorTrafico);

                DBMonitorAtencionClientes DBGestCatAC = new DBMonitorAtencionClientes();
                DBGestCatAC.DBEditaAtnCliente(oIView.iIdSolicitud);


                if (id > 0)
                {
                    oIView.MostrarMensaje(Mensajes.LMensajes[Enumeraciones.Mensajes.Modificacion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
                    //oIView.GetIdSolicitud(id);
                    oIView.ObtieneTrips();
                }
            }
            catch (Exception ex)
            {
                oIView.MostrarMensaje("Se detectó el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
            }
        }

        void oIView_eSearchDetalle(object sender, EventArgs e)
        {
            oIView.LoadDetalle(oIGestCat.DBConsultaDetalle(oIView.oArrDetalle));
        }

        protected void SearchObjCiente_Presenter(object sender, EventArgs e)
        {
            oIView.LlenaGrid(oIGestCat.DBSearchObj(oIView.oArrConsultaMonitorTrafico));
        }

        public void LoadObjectsTrips_Presenter(object sender, EventArgs e)
        {
            oIView.LoadObjectsTrips(oIGestCat.DBSearchTrips(oIView.iIdSolicitud));
        }
        public void NewObjTrip_Presenter(object sender, EventArgs e)
        {
            try
            {
                int id = oIGestCat.DBSaveTrip(oCatalogoTrip);
                if (id > 0)
                {
                    oIView.MostrarMensaje(Mensajes.LMensajes[Enumeraciones.Mensajes.Insercion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
                    //oIView.GetIdSolicitud(id);
                    oIView.ObtieneTrips();
                }
            }
            catch (Exception ex)
            {
                oIView.MostrarMensaje("Se detectó el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
            }
        }
        public void EliminaTrip_Presenter(object sender, EventArgs e)
        {
            try
            {
                int id = oIGestCat.DBEliminaTripSolicitud(oCatalogoTrip);
                if (id > 0)
                {
                    oIView.MostrarMensaje(Mensajes.LMensajes[Enumeraciones.Mensajes.Eliminacion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
                    //oIView.GetIdSolicitud(id);
                    oIView.ObtieneTrips();
                }
            }
            catch (Exception ex)
            {
                oIView.MostrarMensaje("Se detectó el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
            }
        }
        public void eValidaTrip_Presenter(object sender, EventArgs e)
        {
            try
            {
                int Id = sender.S().I();
                oIView.Validacion(oIGestCat.DBValida(Id));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateTrip(object sender, EventArgs e)
        {
            try
            {
                int id = oIGestCat.DBUpdateTrip(oCatalogoTrip);
                if (id > 0)
                {
                    oIView.ObtieneTrips();
                    oIView.MostrarMensaje(Mensajes.LMensajes[Enumeraciones.Mensajes.Modificacion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
                }
            }
            catch (Exception ex)
            {
                oIView.MostrarMensaje("Se detectó el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
            }
        }

       

        private Modelo oCatalogoTrip
        {
            get
            {
                Modelo oModelo = new Modelo();
                switch (oIView.eCrud)
                {
                    case Enumeraciones.TipoOperacion.Insertar:
                        ASPxDataInsertingEventArgs eI = (ASPxDataInsertingEventArgs)oIView.oCrud;
                        oModelo.iId = eI.NewValues["IdSolicitud"].S().I();
                        oModelo.sDescripcion = eI.NewValues["Trip"].S();
                        oModelo.iStatus = 1;
                        oModelo.sUsuarioCreacion = eI.NewValues["UserIdentity"].S();
                        break;
                    case Enumeraciones.TipoOperacion.Actualizar:
                        ASPxDataUpdatingEventArgs eU = (ASPxDataUpdatingEventArgs)oIView.oCrud;

                        oModelo.iId = eU.Keys[0].S().I();
                        oModelo.sDescripcion = eU.NewValues["Trip"].S();
                        oModelo.sUsuarioCreacion = eU.NewValues["UserIdentity"].S();
                        break;
                    case Enumeraciones.TipoOperacion.Eliminar:
                        ASPxDataDeletingEventArgs eD = (ASPxDataDeletingEventArgs)oIView.oCrud;
                        oModelo.iId = eD.Keys[0].S().I();
                        break;
                }

                return oModelo;
            }
        }
    }
}