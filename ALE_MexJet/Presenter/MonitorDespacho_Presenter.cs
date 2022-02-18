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
    public class MonitorDespacho_Presenter : BasePresenter<IViewMonitorDespacho>
    {
        private readonly DBMonitorDespacho oIGestCat;

        public MonitorDespacho_Presenter(IViewMonitorDespacho oView, DBMonitorDespacho oGC)
            : base(oView)
        {
            oIGestCat = oGC;
            oIView.eBuscaDDL += SearchObjDDL_Presenter;
            oIView.eSearchObj += SearchObjGrid_Presenter;
            oIView.eBuscaSubGrid += SearchObjSubGrid_Presenter;
            oIView.eSaveObj += Update_Presenter;
            oIView.eSaveSeguimiento += SaveSeguimiento_Presenter;
            oIView.eBuscaDictamen += SearchDictamen_Presenter;
            oIView.eBuscaPiernadictamen += SearchPiernaDic_Presenter;
        }
        protected void SearchObjDDL_Presenter(object sender, EventArgs e)
        {
            oIView.LlenaDDL(oIGestCat.DBSearchDDL());
        }
        protected void SearchObjGrid_Presenter(object sender, EventArgs e)
        {
            oIView.LlenaGrid(oIGestCat.DBSearchMonitorDespacho(oIView.oArrFiltros));
        }
        protected void SearchObjSubGrid_Presenter(object sender, EventArgs e)
        {
            oIView.LlenaSubGrid(oIGestCat.DBSearchPiernasSolicitud(oIView.oArrFilSolicitud));
        }
        protected void SaveSeguimiento_Presenter(object sender, EventArgs e)
        {
            try
            {
                int id = oIGestCat.DBSaveSeguimiento(oIView.oArrFilSeguimiento);
                if (id > 0)
                {
                    oIView.MostrarMensaje(Mensajes.LMensajes[Enumeraciones.Mensajes.Insercion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
                }
            }
            catch (Exception ex)
            {
                oIView.MostrarMensaje("Se detectó el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
            }
        }
        protected void Update_Presenter(object sender, EventArgs e)
        {
            try
            {
                int id = oIGestCat.DBUpdate(oIView.oArrFilUpd);
                int iIdSolicitudApp = oIView.iIdSolicitudApp;
                if (id > 0)
                {

                    if (Utils.IsSolicitudMobile(iIdSolicitudApp))
                    {
                        switch (oIView.oArrFilUpd[3].S())
                        {
                            case "1": // Si
                                Utils.NotificaAppMobile(iIdSolicitudApp, "Aprobado");
                                break;
                            case "2": // No
                                Utils.NotificaAppMobile(iIdSolicitudApp, "Cancelado");
                                break;
                            case "3": // Restringido
                                Utils.NotificaAppMobile(iIdSolicitudApp, "Pendiente");
                                break;
                        }
                    }

                    oIView.MostrarMensaje(Mensajes.LMensajes[Enumeraciones.Mensajes.Insercion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
                    
                }
            }
            catch (Exception ex)
            {
                oIView.MostrarMensaje("Se detectó el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
            }
        }
        protected void SearchDictamen_Presenter(object sender, EventArgs e)
        {
            oIView.LlenaDictamen(oIGestCat.DBConsultaMonitorDespacho(oIView.oArrFilSolicitud));
        }
        protected void SearchPiernaDic_Presenter(object sender, EventArgs e)
        {
            oIView.LlenaPiernaDictamen(oIGestCat.DBSearchPiernasSolicitud(oIView.oArrFilSolicitud));
        }
    }
}