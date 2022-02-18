using ALE_MexJet.DomainModel;
using ALE_MexJet.Interfaces;
using ALE_MexJet.Objetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NucleoBase.Core;
using DevExpress.Web.Data;
using DevExpress.Web;
using ALE_MexJet.Clases;

namespace ALE_MexJet.Presenter
{
    public class MonitorCliente_Presenter : BasePresenter<IViewMonitorCliente>
    {
        private readonly DBMonitorCliente oIGetCat;

        public MonitorCliente_Presenter(IViewMonitorCliente oView, DBMonitorCliente oGC)
            : base(oView)
        {
            oIGetCat = oGC;
            oIView.eSearchDetalle += LoadDetalleContrato_Presenter;
            oIView.eSearchPiernas += LoadDetallePiernas_Presenter;
            oIView.eSearchDatosContrato += LoadDatosContrato_Presenter;
            oIView.eSearchCasos += LoadCasos_Presenter;
            oIView.eSearchMotivo += LoadMotivos_Presenter;
            oIView.eSearchCasosTramo += SearchCasosTramo_Presenter;
            oIView.eSearchObservaciones += SearchObservaciones_Presenter;
            oIView.eSearchContactosCliente += SearchContactosCliente_Presenter;
            oIView.eDeleteSolicitud += DeleteObj_Presenter;
            oIView.eSearchAreas += LoadAreas_Presenter;
            oIView.eSearchSolicitudEspecial += LoadSolicitudEspecial_Presenter;
            oIView.eConsultaCorreo += LoadCorreo_Presenter;
            oIView.eEliminaCaso += EliminaCaso_Presenter;
            oIView.eConsultaCasoEd += LoadDatosCaso_Presenter;
            oIView.eGuardaSeguimientoHistorico += GuardaSeguimiento_Presenter;
        }
        public void LoadObjects_Presenter()
        {
            oIView.LoadObjects(oIGetCat.DBSearchObj(oIView.oArrFiltros));
        }
        protected void LoadDetalleContrato_Presenter(object sender, EventArgs e)
        {
            oIView.LoadDetalleContrato(oIGetCat.DBSearchDetalle(oIView.iIdContrato));
        }
        protected void LoadDetallePiernas_Presenter(object sender, EventArgs e)
        {
            oIView.LoadDetallePiernasSolicitud(oIGetCat.DBSearchPiernas(oIView.iIdSolicitud));
        }
        protected void LoadDatosContrato_Presenter(object sender, EventArgs e)
        {
            oIView.LoadDatosContrato(oIGetCat.DBSearchDetalleContrato(oIView.iIdContrato, oIView.sCodigoCliente));
        }
        protected void LoadCasos_Presenter(object sender, EventArgs e)
        {
            oIView.LoadCasos(oIGetCat.DBSearchCasos());
        }   
        protected void LoadMotivos_Presenter(object sender, EventArgs e)
        {
            oIView.LoadMotivos(oIGetCat.DBSearchMotivos(oIView.iIdCaso));
        }
        public void LoadSolicitudEspecial_Presenter(object sender, EventArgs e)
        {
            oIView.LoadSolicitudEspecial(oIGetCat.DBSerachSolicitudEspecial(oIView.iIdMotivo));
        }
        protected override void SearchObj_Presenter(object sender, EventArgs e)
        {
            oIView.LoadObjects(oIGetCat.DBSearchObj(oIView.oArrFiltros));
        }
        public void SearchCasosTramo_Presenter(object sender, EventArgs e)
        {
            oIView.LoadCasosTramo(oIGetCat.DBSearchCasosTramo(oIView.iIdCliente));
        }
        public void SearchObservaciones_Presenter(object sender, EventArgs e)
        {
            oIView.LoadObservaciones(oIGetCat.DBSearchObservaciones(oIView.iIdCliente));
        }
        public void SearchContactosCliente_Presenter(object sender,EventArgs e)
        {
            oIView.LoadContactosCliente(oIGetCat.DBSearchContactosCliente(oIView.iIdCliente));
        }

        public void LoadAreas_Presenter(object sender, EventArgs e)
        {
            oIView.LoadAreas(oIGetCat.DBSearchAreas());
        }
        protected override void DeleteObj_Presenter(object sender, EventArgs e)
        {
            try
            {
                int id = oIGetCat.DBDeleteSolicitud(oIView.iIdSolicitud);
                if (id > 0)
                {
                    oIView.MostrarMensaje(Mensajes.LMensajes[Enumeraciones.Mensajes.Cancelacion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
                    oIView.ObtieneValores();
                }
            }
            catch (Exception ex)
            {
                oIView.MostrarMensaje("Se detectó el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
            }
        }
        protected override void SaveObj_Presenter(object sender, EventArgs e)
        {
            //try
            //{
            //    int id = oIGestCat.DBUpdate(oCatalogo);
            //    if (id > 0)
            //    {
            //        oIView.ObtieneValores();
            //        oIView.MostrarMensaje("Se modificó  el registro numero " + id.ToString(), "REGISTRO ACTUALIZADO");

            //    }
            //}
            //catch (Exception ex)
            //{
            //    oIView.MostrarMensaje("Se detectó el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
            //}
        }

        protected override void NewObj_Presenter(object sender, EventArgs e)
        {
            try
            {
                int id = oIGetCat.DBSaveCasos(oIView.oCatalogoCasos);
                if (id > 0)
                {
                    oIView.CargaCasoID(0);
                    oIView.MostrarMensaje("Se guardó el registro.", Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
                    oIView.ObtieneCasosTramo();
                }
            }
            catch (Exception ex)
            {
                oIView.MostrarMensaje("Se detectó el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
            }
        }

        public void LoadCorreo_Presenter(object sender, EventArgs e)
        {
            oIView.LoadCorreo(oIGetCat.DBObtieneCorreoCliente(sender.S().I()));
        }
        public void EliminaCaso_Presenter(object sender, EventArgs e)
        {
            int id = oIGetCat.DBDeleteCaso(oIView.oArrEliminaCaso);
            if (id > 0)
            {
                oIView.MostrarMensaje(Mensajes.LMensajes[Enumeraciones.Mensajes.Eliminacion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
            }
        }
        public void LoadDatosCaso_Presenter(object sender, EventArgs e)
        {
            oIView.CargaDatosCaso(oIGetCat.DBConsultaCasoEd(oIView.oArrConsultaCasoEd));
        }
        public void GuardaSeguimiento_Presenter(object sender, EventArgs e)
        {
            try
            {
                int id = oIGetCat.DBGuardaSeguimiento(oIView.oGuardaSeguimiento);
            }
            catch (Exception ex)
            {
                oIView.MostrarMensaje("Se detectó el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
            }
        }
    }
}