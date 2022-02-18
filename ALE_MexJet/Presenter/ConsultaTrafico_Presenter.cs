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
    public class ConsultaTrafico_Presenter : BasePresenter<IViewConsultaTrafico>
    {
        private readonly DBConsultaTrafico oIGestCat;

        public ConsultaTrafico_Presenter(IViewConsultaTrafico oView, DBConsultaTrafico oGC)
            : base(oView)
        {
            oIGestCat = oGC;
            oIView.eGetClientes += oIView_eGetClientes;
            oIView.eGetContrato += oIView_eGetContrato;
            oIView.eSearchObj += oIView_eSearchObj;
            oIView.eConsultaSol += oIView_eConsultaSol;
            oIView.eBuscaSubGrid += oIView_eBuscaSubGrid;
            oIView.eNewTrip += oIView_eNewTrip;
            oIView.eSaveTrip += oIView_eSaveTrip;
            oIView.eValidaTrip += oIView_eValidaTrip;
            oIView.eLoadObjTrips += oIView_eLoadObjTrips;
            oIView.eEliminaTripSolicitud += oIView_eEliminaTripSolicitud;
        }

        void oIView_eEliminaTripSolicitud(object sender, EventArgs e)
        {
            try
            {
                DBMonitorTrafico DBGestCat = new DBMonitorTrafico();
                int id = DBGestCat.DBEliminaTripSolicitud(oCatalogoTrip);
                if (id > 0)
                {
                    oIView.MostrarMensaje(Mensajes.LMensajes[Enumeraciones.Mensajes.Eliminacion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
                    //oIView.GetIdSolicitud(id);
                    oIView.ObtieneTrips();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void oIView_eLoadObjTrips(object sender, EventArgs e)
        {
            try
            {
                DBMonitorTrafico DBGestCat = new DBMonitorTrafico();
                oIView.LoadObjectsTrips(DBGestCat.DBSearchTrips(oIView.iIdSolicitud));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void oIView_eValidaTrip(object sender, EventArgs e)
        {
            
        }

        void oIView_eSaveTrip(object sender, EventArgs e)
        {
            try
            {
                DBMonitorTrafico DBGestCat = new DBMonitorTrafico();                
                int id = DBGestCat.DBUpdateTrip(oCatalogoTrip);

                DBMonitorAtencionClientes DBGestCatAC = new DBMonitorAtencionClientes();
                DBGestCatAC.DBEditaAtnCliente(oIView.iIdSolicitud);

                if (id > 0)
                {
                    oIView.ObtieneTrips();
                    oIView.MostrarMensaje(Mensajes.LMensajes[Enumeraciones.Mensajes.Modificacion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void oIView_eNewTrip(object sender, EventArgs e)
        {
            try
            {
                DBMonitorTrafico DBGestCat = new DBMonitorTrafico();
                int id = DBGestCat.DBSaveTrip(oCatalogoTrip);

                DBMonitorAtencionClientes DBGestCatAC = new DBMonitorAtencionClientes();
                DBGestCatAC.DBEditaAtnCliente(oIView.iIdSolicitud);

                if (id > 0)
                {
                    oIView.MostrarMensaje(Mensajes.LMensajes[Enumeraciones.Mensajes.Insercion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
                    //oIView.GetIdSolicitud(id);
                    oIView.ObtieneTrips();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void oIView_eSearchObj(object sender, EventArgs e)
        {
            oIView.LlenaGrid(oIGestCat.DBSearchObj(oIView.oArrFiltros));
        }

        void oIView_eGetClientes(object sender, EventArgs e)
        {
            oIView.LoadClientes(oIGestCat.DBSearchCliente(oIView.oArrFiltroClientes));
        }

        void oIView_eGetContrato(object sender, EventArgs e)
        {
            oIView.LoadContrato(oIGestCat.DBSearchContrato(oIView.oArrFiltroContrato));
        }

        void oIView_eConsultaSol(object sender, EventArgs e)
        {
            DBConsultaDespacho dbGestCat = new DBConsultaDespacho();
            oIView.LoadSol(dbGestCat.DBConsultaSolicitud(oIView.oArrFilSolicitud));
        }
        void oIView_eBuscaSubGrid(object sender, EventArgs e)
        {
            DBConsultaDespacho dbGestCat = new DBConsultaDespacho();
            oIView.LoadPiernas(dbGestCat.DBSearchPiernasSolicitud(oIView.oArrFilSolicitud));
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