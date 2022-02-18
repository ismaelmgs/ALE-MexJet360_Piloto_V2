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
using System.Data;

namespace ALE_MexJet.Presenter
{
    public class SolicitudVuelo_Presenter : BasePresenter<IViewSolicitudVuelo>
    {
        private readonly DBSolicitudesVuelo oIGestCat;

        public SolicitudVuelo_Presenter(IViewSolicitudVuelo oView, DBSolicitudesVuelo oGC)
            : base(oView)
        {
            oIGestCat = oGC;

            oIView.eLoadContactosCliente += LoadContactosCliente;
            oIView.eLoadGrupoModelo += LoadGrupoModelo_Presenter;
            oIView.eLoadEstatus += LoadGrupoEstatus_Presenter;
            oIView.eLoadOrigen += LoadGrupoOrigen_Presenter;
            oIView.eLoadMotivos += LoadMotivos_Presenter;
            oIView.eNewTrip += NewObjTrip_Presenter;
            oIView.eLoadObjTrips += LoadObjectsTrips_Presenter;
            oIView.eSaveTrip += UpdateTrip;
            oIView.eLoadOrigenVuelo += LoadOrigenVuelo_Presenter;
            oIView.eLoadDestinoVuelo += LoadDestinoVuelo_Presenter;
            oView.eNewTramo += NewObjTramo_Presenter;
            oView.eNewSeguimiento += NewObjSeguimiento_Presenter;
            oView.eLoadHistorico += LoadHistorico_Presenter;
            oView.eNewItinerario += NewObjItinerario_Presenter;
            oView.eLoadOrigenDestino += LoadOrigenDestino_Presenter;
            oView.eLoadOrigDestFiltro += LoadOrigDestFltro_Presenter;
            oView.eLoadTramoSol += LoadTramoSol_Presenter;
            oView.eEditaTramoSol += EditaTramoSol_Presenter;
            oView.eEliminaTramoSol += EliminaTramoSol_Presenter;
            oView.eInsertaPaxTramo += InsertaPaxTramo_Presenter;
            oView.eObtienePaxTramo += LoadPaxTramo_Presenter;
            oView.eEditaPaxTramo += EditaPaxTamo_Presenter;
            oView.eEliminaPaxTramo += EliminaPaxTramo_Presenter;
            oView.eInsertaComisariatoTramo += InsertaComisariatoTramo_Presenter;
            oView.eEditaComisariatoTramo += EditaComisariatoTamo_Presenter;
            oView.eEliminaComisariatoTramo += EliminaComisariatoTramo_Presenter;
            oView.eConsultaComisariatoTramo += LoadComisariatoTramo_Presenter;
            oView.eEliminaTripSolicitud += EliminaTrip_Presenter;
            oView.eConsultaProveedor += LoadProveedor_Presenter;
            oView.eEliminaHistorico += EliminaHistorico_Presenter;
            oView.eEditarSolVuelo += EditaSolVuelo_Presenter;
            oView.eConsultasolVueloByID += eLoadSolVueloByID_Presenter;
            oView.eLoadCorreoAlta += eLoadCorreoAlta_Presenter;
            oView.eValidaTrip += eValidaTrip_Presenter;
            oView.eLoadPaxTramo2 += LoadPaxTramo2_Presenter;
            oView.eValidaFechaHora += eValFechaHora_Presenter;
            oView.eAerTramo += eAeTRamo_Presenter;
            oView.eConsultaDetalle += ConsultaDetalle_Presenter;
            oView.eConsultaSolPDF += ConsultaSolPDF_Presenter;
            oView.eConsultaModCon += GrupoModeloCont_Presenter;
            oView.eConsultaItinerario += ItinerarioVuelo_Presenter;
            oView.eEliminaItinerario  += EliminaItinerariSolicitud_Presenter;
            oView.eGuardaSeguimientoHistorico += GuardaSeguimiento_Presenter;
            oView.eConsultaDetalleItinerario += ConsultaDetalleItinerario_Presenter;
            oView.eValidaVuelosimultaneo += ValidaSolVuelo_Presenter;
            oView.eInsertaMonitorDespacho += NewMonitorDespacho_Presenter;
            oView.eBuscaPasajero += LoadPasajeroFltro_Presenter;
            oView.eInsertaPasajero +=  NewPasajero_Presenter;
            oView.eViabilidad +=  LoadViabilidad_Presenter;
            oView.ePDFSeguimiento += PDFSeguimiento_Presenter;
            oView.eConsultaPDFSeguimiento += ConsultarPDFSeguimiento_Presenter;
            oIView.eConsultaTripGuides += ConsultaTripGuides_Presenter;
            oIView.eDeleteTripGuide += DeleteTripGuide_Presenter;
            oIView.eConsultaContactoSolicitud += ConsultaContactoSolicitud_Presenter;
            oIView.eConsultaVendedorSolicitud += ConsultaVendedorSolicitud_Presenter;
        }

        public void LoadObjects_Presenter()
        {
            oIView.LoadObjects(oIGestCat.DBSearchObj(oIView.oArrFiltros));
        }
        public void LoadObjectsTrips_Presenter(object sender, EventArgs e)
        {
            oIView.LoadObjectsTrips(oIGestCat.DBSearchTrips(oIView.iIdSolicitud));
        }
        public void LoadContactosCliente(object sender, EventArgs e)
        {
            oIView.LoadContactosCliente(oIGestCat.DBSearchContactos(oIView.iIdCliente));
        }
        public void LoadGrupoModelo_Presenter(object sender, EventArgs e)
        {
            oIView.LoadGrupoModelo(oIGestCat.DBSearchGrupoModelo());
        }
        public void LoadGrupoOrigen_Presenter(object sender, EventArgs e)
        {
            oIView.LoadOrigen(oIGestCat.DBSearchOrigen());
        }
        public void LoadGrupoEstatus_Presenter(object sender, EventArgs e)
        {
            oIView.LoadEstatus(oIGestCat.DBSearchEstatus());
        }
        public void LoadMotivos_Presenter(object sender, EventArgs e)
        {
            oIView.LoadMotivos(oIGestCat.DBSearchMotivos());
        }
        public void LoadOrigenVuelo_Presenter(object sender, EventArgs e)
        {
            //oIView.LoadOrigenVuelo(oIGestCat.DBSearchOrigenVuelo());
        }
        public void LoadDestinoVuelo_Presenter(object sender, EventArgs e)
        {
            oIView.LoadDestinoVuelo(oIGestCat.DBSearchDestinoVuelo());
        }
        public void LoadHistorico_Presenter(object sender, EventArgs e)
        {
            oIView.LoadHistorico(oIGestCat.DBSearchHistorico(oIView.iIdSolicitud));
        }
        public void LoadOrigenDestino_Presenter(object sender, EventArgs e)
        {
            oIView.LoadOrigenDestino(oIGestCat.DBObtieneDestinoOrigen());
        }
        public void LoadOrigDestFltro_Presenter(object sender, EventArgs e)
        {
            string filtro = sender.S();
            oIView.LoadOrigDestFiltro(oIGestCat.DBObtieneDestOrigFiltro(filtro));
        }
        public void LoadTramoSol_Presenter(object sender, EventArgs e)
        {
            int filtro = sender.I();
            oIView.LoadTramoSol(oIGestCat.DBObtieneTramoSol(filtro));
        }
        public void LoadPaxTramo_Presenter(object sender, EventArgs e)
        {
            int filtro = sender.I();
            oIView.LoadPaxTramo(oIGestCat.DBObtienePaxTramo(filtro));
        }
        public void LoadPaxTramo2_Presenter(object sender, EventArgs e)
        {
            int filtro = sender.I();
            oIView.LoadPaxTramo2(oIGestCat.DBObtienePaxTramo(filtro));
        }
        public void LoadComisariatoTramo_Presenter(object sender, EventArgs e)
        {
            int filtro = sender.I();
            oIView.LoadComisariatoTramo(oIGestCat.DBObtieneComisariatoTramo(filtro));
        }
        public void LoadProveedor_Presenter(object sender, EventArgs e)
        {
            oIView.LoadProveedor(oIGestCat.DBObtieneProveedor());
        }
        protected override void NewObj_Presenter(object sender, EventArgs e)
        {
            try
            {
                int id = oIGestCat.DBSave(oIView.oCatalogo);
                
                if (id > 0)
                {
                    
                    //oIView.MostrarMensaje(Mensajes.LMensajes[Enumeraciones.Mensajes.Insercion._I()].sMensaje,Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
                    oIView.GetIdSolicitud(id);
                    oIView.ObtieneTrips();
                }
            }
            catch (Exception ex)
            {
                oIView.MostrarMensaje("Se detectó el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
            }
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
        public void NewObjTramo_Presenter(object sender, EventArgs e)
        {
            try
            {
                
                DataTable dt = oIGestCat.DBSaveTramo(oCatalogoTramo);
                int id = dt.Rows[0][0].S().I();
                if (id > 0 && dt.Columns.Count > 1)
                {
                    if (Utils.GetParametrosClave("73").Equals("1"))
                    {
                        DataTable dtFechasPico = new DBFechaPico().DBSearchObj("@Fecha", DBNull.Value, "@estatus", 1);
                        DateTime FV =  oCatalogoTramo.dFechaVuelo.Dt();
                        bool FP = false;
                        foreach (DataRow rw in dtFechasPico.Rows)
                        {
                            if (FV == rw[1].Dt())
                            {
                                FP = true;
                                break;
                            }
                        }

                        if(FP)
                            oIView.MostrarMensaje(Mensajes.LMensajes[Enumeraciones.Mensajes.RangAcomDiaFer._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
                        else
                            oIView.MostrarMensaje(Mensajes.LMensajes[Enumeraciones.Mensajes.TiempMinVulBN._I()].sMensaje + " " + dt.Rows[0][1].S(), Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
                    }
                    else
                    oIView.MostrarMensaje(Mensajes.LMensajes[Enumeraciones.Mensajes.Insercion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
                }
                else
                {
                    if (Utils.GetParametrosClave("73").Equals("1"))
                    {
                        DataTable dtFechasPico = new DBFechaPico().DBSearchObj("@Fecha", DBNull.Value, "@estatus", 1);
                        DateTime FV = oCatalogoTramo.dFechaVuelo.Dt();
                        bool FP = false;
                        foreach (DataRow rw in dtFechasPico.Rows)
                        {
                            if (FV == rw[1].Dt())
                            {
                                FP = true;
                                break;
                            }
                        }

                        if (FP)
                            oIView.MostrarMensaje(Mensajes.LMensajes[Enumeraciones.Mensajes.RangAcomDiaFer._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
                        else
                            oIView.MostrarMensaje(Mensajes.LMensajes[Enumeraciones.Mensajes.Insercion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
                    }
                    else
                        oIView.MostrarMensaje(Mensajes.LMensajes[Enumeraciones.Mensajes.Insercion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
                }
            }
            catch (Exception ex)
            {
                oIView.MostrarMensaje("Se detectó el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
            }
        }
        public void EditaTramoSol_Presenter(object sender, EventArgs e)
        {
            try
            {
                DataTable id = oIGestCat.DBEditaTramoSol(oCatalogoTramo);
                if (id.Columns.Count == 1)
                    oIView.MostrarMensaje(Mensajes.LMensajes[Enumeraciones.Mensajes.Modificacion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
                else
                    oIView.MostrarMensaje(Mensajes.LMensajes[Enumeraciones.Mensajes.RepSalAntSal._I()].sMensaje + " " + id.Rows[0][1].S(), Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
            }
            catch (Exception ex)
            {
                oIView.MostrarMensaje("Se detectó el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
            }
        }
        public void EliminaTramoSol_Presenter(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = oIGestCat.DBEliminaTramoSol(oCatalogoTramo);

                if (dt.Columns.Count == 1)
                {
                    oIView.MostrarMensaje(Mensajes.LMensajes[Enumeraciones.Mensajes.Eliminacion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
                }
                else
                {
                    if (dt.Columns[1].ColumnName == "CancelacionAnticipadaSalNoBase")
                    {
                        oIView.MostrarMensaje(Mensajes.LMensajes[Enumeraciones.Mensajes.CanNoBase._I()].sMensaje + " " + dt.Rows[0][1].S(), Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
                    }
                    else 
                    {
                        oIView.MostrarMensaje(Mensajes.LMensajes[Enumeraciones.Mensajes.CanBase._I()].sMensaje + " " + dt.Rows[0][1].S(), Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
                    }
                }
            }
            catch (Exception ex)
            {
                oIView.MostrarMensaje("Se detectó el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
            }
        }
        public void NewObjSeguimiento_Presenter(object sender, EventArgs e)
        {
            try
            {
                int id = oIGestCat.DBSaveSeguimiento(oIView.oCatalogoSeguimiento);
                if (id > 0)
                {
                    oIView.iIdSeguimiento = id;
                    oIView.MostrarMensaje(Mensajes.LMensajes[Enumeraciones.Mensajes.Insercion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
                    
                }
            }
            catch (Exception ex)
            {
                oIView.MostrarMensaje("Se detectó el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
            }
        }
        public void NewObjItinerario_Presenter(object sender, EventArgs e)
        {
            try
            {
                int id = oIGestCat.DBSaveItinerario(oIView.oCatalogoItinerario);
                if (id > 0)
                {
                    oIView.MostrarMensaje(Mensajes.LMensajes[Enumeraciones.Mensajes.Insercion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
                    oIView.ObtieneHistorico();
                }
            }
            catch (Exception ex)
            {
                oIView.MostrarMensaje("Se detectó el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
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
        public void InsertaPaxTramo_Presenter(object sender, EventArgs e)
        {
            try
            {
                int id = oIGestCat.DBInsertaPaxTramo(oCatalogoPaxTramo);
                //if (id > 0)
                //{
                //    oIView.MostrarMensaje(Mensajes.LMensajes[Enumeraciones.Mensajes.Insercion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
                //}
            }
            catch (Exception ex)
            {
                oIView.MostrarMensaje("Se detectó el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
            }
        }
        public void EditaPaxTamo_Presenter(object sender, EventArgs e)
        {
            try
            {
                int id = oIGestCat.DBEditaPaxTramo(oCatalogoPaxTramo);
                //if (id > 0)
                //{
                //    oIView.MostrarMensaje(Mensajes.LMensajes[Enumeraciones.Mensajes.Modificacion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
                //}
            }
            catch (Exception ex)
            {
                oIView.MostrarMensaje("Se detectó el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
            }
        }
        public void EliminaPaxTramo_Presenter(object sender, EventArgs e)
        {
            try
            {
                int id = oIGestCat.DBEliminaPaxTramo(oCatalogoPaxTramo);
                if (id > 0)
                {
                    oIView.MostrarMensaje(Mensajes.LMensajes[Enumeraciones.Mensajes.Eliminacion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
                }
            }
            catch (Exception ex)
            {
                oIView.MostrarMensaje("Se detectó el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
            }
        }
        public void InsertaComisariatoTramo_Presenter(object sender, EventArgs e)
        {
            try
            {
                int id = oIGestCat.DBInsertaComisariatoTramo(oCatalogoComisariatoTramo);
                //if (id > 0)
                //{
                //    oIView.MostrarMensaje(Mensajes.LMensajes[Enumeraciones.Mensajes.Insercion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
                //}
            }
            catch (Exception ex)
            {
                oIView.MostrarMensaje("Se detectó el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
            }
        }
        public void EditaComisariatoTamo_Presenter(object sender, EventArgs e)
        {
            try
            {
                int id = oIGestCat.DBEditaComisariatoTramo(oCatalogoComisariatoTramo);
                //if (id > 0)
                //{
                //    oIView.MostrarMensaje(Mensajes.LMensajes[Enumeraciones.Mensajes.Modificacion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
                //}
            }
            catch (Exception ex)
            {
                oIView.MostrarMensaje("Se detectó el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
            }
        }
        public void EliminaComisariatoTramo_Presenter(object sender, EventArgs e)
        {
            try
            {
                int id = oIGestCat.DBEliminaComisariatoTramo(oCatalogoComisariatoTramo);
                if (id > 0)
                {
                    oIView.MostrarMensaje(Mensajes.LMensajes[Enumeraciones.Mensajes.Eliminacion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
                }
            }
            catch (Exception ex)
            {
                oIView.MostrarMensaje("Se detectó el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
            }
        }
        public void EliminaHistorico_Presenter(object sender, EventArgs e)
        {
            try
            {
                int id = oIGestCat.DBEliminaSeguimientosolicitud(oCatalogoSolicitud);
                if (id > 0)
                {
                    oIView.MostrarMensaje(Mensajes.LMensajes[Enumeraciones.Mensajes.Eliminacion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
                    oIView.ObtieneHistorico();
                }
            }
            catch (Exception ex)
            {
                oIView.MostrarMensaje("Se detectó el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
            }
        }
        public void EditaSolVuelo_Presenter(object sender, EventArgs e)
        {
            try
            {
                int id = oIGestCat.DBEditaSolVuelo(oIView.oCat);
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
        protected void eLoadSolicitud_Presenter(object sender, EventArgs e)
        {
            try
            {
                oIView.oCatalogo = oIGestCat.DBGetSolicitud(oIView.iIdSolicitud);


                //oIView.LoadSolicitud(oIGestCat.DBGetSolicitudCompleta());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void eLoadSolVueloByID_Presenter(object sender, EventArgs e)
        {
            try
            {
                int Id = sender.S().I();
                oIView.LoadSolVueloByID(oIGestCat.DBObtieneSolVueloByID(Id));
                LoadHistorico_Presenter(sender, e);
            }
            catch (Exception ex)
            {
                throw ex;
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
        protected void eLoadCorreoAlta_Presenter(object sender, EventArgs e)
        {
            try
            {
                int Id = sender.S().I();
                oIView.LoadCorreoAlta(oIGestCat.DBLoadCorreoAlta(Id));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eValFechaHora_Presenter(object sender, EventArgs e)
        {
            try
            {
                oIView.ValidaFechaHora(oIGestCat.DBValidaFechaHora(oCatalogoTramo));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eAeTRamo_Presenter(object sender, EventArgs e)
        {
            try
            {
                oIView.LoadAerTramo(oIGestCat.DBLoadAeTramo(sender.S()));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaDetalle_Presenter(object sender, EventArgs e)
        {
            try
            {
                oIView.LoadConsultaDetalle(oIGestCat.DBGetObtieneSeguimientoID(sender.S().I()));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ConsultaSolPDF_Presenter(object sender, EventArgs e)
        {
            try
            {
                oIView.LoadSolPDF(oIGestCat.DBGetSolPDFID(sender.S().I()));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GrupoModeloCont_Presenter(object sender, EventArgs e)
        {
            try
            {
                oIView.Modelo(oIGestCat.DBConsultaGrupoModeloCon(oIView.oFiltroContrato));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ItinerarioVuelo_Presenter(object sender, EventArgs e)
        {
            try
            {
                oIView.LoadItinerario(oIGestCat.DBConsultaItinerarioVuelo(oIView.oItinerario));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void EliminaItinerariSolicitud_Presenter(object sender, EventArgs e)
        {
            try
            {
                int id = oIGestCat.DBEliminaItinerarioSolicitud(oIView.oEliminaItinerario);
                if (id > 0)
                {
                    oIView.MostrarMensaje(Mensajes.LMensajes[Enumeraciones.Mensajes.Eliminacion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
                }
            }
            catch (Exception ex)
            {
                oIView.MostrarMensaje("Se detectó el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
            }
        }
        public void GuardaSeguimiento_Presenter(object sender, EventArgs e)
        {
            try
            {
                int id = oIGestCat.DBGuardaSeguimiento(oIView.oGuardaSeguimiento);
                if (id > 0)
                {
                    //oIView.MostrarMensaje(Mensajes.LMensajes[Enumeraciones.Mensajes.Insercion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
                    oIView.ObtieneHistorico();
                }
            }
            catch (Exception ex)
            {
                oIView.MostrarMensaje("Se detectó el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
            }
        }
        public void ConsultaDetalleItinerario_Presenter(object sender, EventArgs e)
        {
            try
            {
                oIView.ObtieneDetalle(oIGestCat.DBconsutaDetalleItinerario(oIView.oConsultaDetalleItinerario));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ValidaSolVuelo_Presenter(object sender, EventArgs e)
        {
            try
            {
                oIView.ValidaVueloSimultaneo(oIGestCat.DBValidaVueloSimultaneo(sender.S()));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void NewMonitorDespacho_Presenter(object sender, EventArgs e)
        {
            try
            {
                int id = oIGestCat.DBGurdaMonitorDespacho(oIView.oCatalogo);
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
        public void NewPasajero_Presenter(object sender, EventArgs e)
        {
            try
            {
                int id = oIGestCat.DBGurdaPasajero(oIView.oInsertaPasajero);
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
        public void LoadPasajeroFltro_Presenter(object sender, EventArgs e)
        {
            string filtro = sender.S();
            oIView.CargaPasajeros(oIGestCat.DBObtienePasajeroFiltro(filtro));
        }
        public void LoadViabilidad_Presenter(object sender, EventArgs e)
        {
            DataTable DT = oIGestCat.DBViabilidad(sender);

            if (DT != null && DT.Rows.Count > 0)
                oIView.bViabilidad = Convert.ToBoolean( DT.Rows[0][0]);

        }
        public void PDFSeguimiento_Presenter(object sender, EventArgs e)
        {
            int id = oIGestCat.DBGurdaPDFSeguimieno(oIView.oInsertaPDFSeguimiento);
            if (id > 0)
            {
                oIView.MostrarMensaje(Mensajes.LMensajes[Enumeraciones.Mensajes.Insercion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
            }
        }
        public void ConsultarPDFSeguimiento_Presenter(object sender, EventArgs e)
        {
            int x = sender.S().I();
            oIView.ConsultaPDFSeguimiento(oIGestCat.DBConsultarPDFSeguimiento(x));           
        }
        protected void ConsultaTripGuides_Presenter(object sender, EventArgs e)
        {
            oIView.CargaTripGuides(oIGestCat.DBGetTripGuidesPorSolicitud(oIView.iIdSolicitud));
        }
        protected void DeleteTripGuide_Presenter(object sender, EventArgs e)
        {
            // Elimina Trip Guide Por ID
            int iIdTripGuide = sender.S().I();
            oIGestCat.DBSetEliminaTripGuide(iIdTripGuide);
            // Recarga TripGuides
            oIView.CargaTripGuides(oIGestCat.DBGetTripGuidesPorSolicitud(oIView.iIdSolicitud));
        }

        protected void ConsultaContactoSolicitud_Presenter(object sender, EventArgs e)
        {
            oIView.ConsultaContactoSolicitud(oIGestCat.DBConsultaContactoSolicitud(oIView.iIdSolicitud));
        }

        protected void ConsultaVendedorSolicitud_Presenter(object sender, EventArgs e)
        {
            oIView.ConsultaVendedorSolicitud(oIGestCat.DBConsultaVendedorSolicitud(oIView.iIdSolicitud));
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
        private TramoSolicitud oCatalogoTramo
        {
            get
            {
                TramoSolicitud oTramo = new TramoSolicitud();
                switch (oIView.eCrud)
                {
                    case Enumeraciones.TipoOperacion.Insertar:
                        ASPxDataInsertingEventArgs eI = (ASPxDataInsertingEventArgs)oIView.oCrud;
                        oTramo.iIdSolicitud = eI.NewValues["IdSolicitud"].S().I();
                        oTramo.iIdAeropuertoO = eI.NewValues["idaeropuertoo"].S().I();
                        oTramo.iIdAeropuertoD = eI.NewValues["idaeropuertod"].S().I();
                        oTramo.dFechaVuelo = eI.NewValues["FechaVuelo"].S();
                       
                        if (oTramo.dFechaVuelo == "")
                            oTramo.dFechaVuelo = "01/01/1900";

                        oTramo.sHoraVuelo = eI.NewValues["HoraVuelo"].S().Dt().ToString("HH:mm");
                        oTramo.sTransportacion = eI.NewValues["Transportacion"].S();
                        oTramo.iStatus = 1;
                        oTramo.sUsuarioCreacion = eI.NewValues["UserIdentity"].S();
                        break;
                    case Enumeraciones.TipoOperacion.Actualizar:
                        ASPxDataUpdatingEventArgs eU = (ASPxDataUpdatingEventArgs)oIView.oCrud;

                        oTramo.iIdTramo = eU.Keys[0].S().I();
                        oTramo.iIdSolicitud = eU.NewValues["idSolicitud"].S().I();
                        oTramo.iIdAeropuertoO = eU.NewValues["idaeropuertoo"].S().I();
                        oTramo.iIdAeropuertoD = eU.NewValues["idaeropuertod"].S().I();
                        oTramo.dFechaVuelo = eU.NewValues["FechaVuelo"].S();
                        oTramo.sHoraVuelo = eU.NewValues["HoraVuelo"].S().Dt().ToString("HH:mm");
                        oTramo.sTransportacion = eU.NewValues["Transportacion"].S();
                        oTramo.iStatus = 1;
                        oTramo.sUsuarioCreacion = eU.NewValues["UserIdentity"].S();
                        break;
                    case Enumeraciones.TipoOperacion.Eliminar:
                        ASPxDataDeletingEventArgs eD = (ASPxDataDeletingEventArgs)oIView.oCrud;
                        oTramo.iIdTramo = eD.Keys[0].S().I();
                        break;

                    case Enumeraciones.TipoOperacion.Validar:
                        ASPxDataValidationEventArgs Val = (ASPxDataValidationEventArgs)oIView.oCrud;
                        oTramo.iIdSolicitud = Val.NewValues["IdSolicitud"].S().I();
                        oTramo.dFechaVuelo = Val.NewValues["FechaVuelo"].S();


                        if (oTramo.dFechaVuelo == "")
                            oTramo.dFechaVuelo = "01/01/1900";

                        oTramo.sHoraVuelo = Val.NewValues["HoraVuelo"].S().Dt().ToString("HH:mm");
                        oTramo.iIdTramo = Val.Keys.Count == 0 ? 0 : Val.Keys[0].S().I();
                        break;
                }

                return oTramo;
            }
        }
        private SolicitudVuelo oCatalogoSolicitud
        {
            get
            {
                SolicitudVuelo oHistorico = new SolicitudVuelo();
                switch (oIView.eCrud)
                {
                    case Enumeraciones.TipoOperacion.Insertar:
                        ASPxDataInsertingEventArgs eI = (ASPxDataInsertingEventArgs)oIView.oCrud;
                     

                        oHistorico.iIdSolicitud = eI.NewValues["IdSolicitud"].S().I();
                        oHistorico.iIdAutor = eI.NewValues["Autor"].S().I();
                        oHistorico.sNotas = eI.NewValues["Nota"].S();
                        oHistorico.iStatus = 1;
                        oHistorico.sUsuarioCreacion = eI.NewValues["UserIdentity"].S();
                        break;
                    //case Enumeraciones.TipoOperacion.Actualizar:
                    //    ASPxDataUpdatingEventArgs eU = (ASPxDataUpdatingEventArgs)oIView.oCrud;

                    //    oModelo.iId = eU.Keys[0].S().I();
                    //    oModelo.sDescripcion = eU.NewValues["Trip"].S();

                    //    break;
                    case Enumeraciones.TipoOperacion.Eliminar:
                        ASPxDataDeletingEventArgs eD = (ASPxDataDeletingEventArgs)oIView.oCrud;
                        oHistorico.iIdSeguimiento  = eD.Keys[0].S().I();
                        break;
                }

                return oHistorico;
            }
        }
        private SolicitudVuelo oCatalogoItinerario
        {
            get
            {
                SolicitudVuelo oHistorico = new SolicitudVuelo();
                switch (oIView.eCrud)
                {
                    case Enumeraciones.TipoOperacion.Insertar:
                        ASPxDataInsertingEventArgs eI = (ASPxDataInsertingEventArgs)oIView.oCrud;


                        oHistorico.iIdSolicitud = eI.NewValues["IdSolicitud"].S().I();
                        oHistorico.iIdAutor = eI.NewValues["Autor"].S().I();
                        oHistorico.sNotas = eI.NewValues["Nota"].S();
                        oHistorico.iStatus = 1;
                        oHistorico.sUsuarioCreacion = eI.NewValues["UserIdentity"].S();
                        break;
                    //case Enumeraciones.TipoOperacion.Actualizar:
                    //    ASPxDataUpdatingEventArgs eU = (ASPxDataUpdatingEventArgs)oIView.oCrud;

                    //    oModelo.iId = eU.Keys[0].S().I();
                    //    oModelo.sDescripcion = eU.NewValues["Trip"].S();

                    //    break;
                    case Enumeraciones.TipoOperacion.Eliminar:
                        ASPxDataDeletingEventArgs eD = (ASPxDataDeletingEventArgs)oIView.oCrud;
                        oHistorico.iIdSolicitud = eD.Keys[0].S().I();
                        break;
                }

                return oHistorico;
            }
        }
        private TramoSolicitud oCatalogoPaxTramo
        {
            get
            {
                TramoSolicitud oTramo = new TramoSolicitud();
                switch (oIView.eCrud)
                {
                    case Enumeraciones.TipoOperacion.Insertar:
                        ASPxDataInsertingEventArgs eI = (ASPxDataInsertingEventArgs)oIView.oCrud;
                        oTramo.iIdTramo = eI.NewValues["IdTramo"].S().I();
                        oTramo.sNombrePax = eI.NewValues["NombrePax"].S();
                        oTramo.sUsuarioCreacion = eI.NewValues["UserIdentity"].S();
                        break;
                    case Enumeraciones.TipoOperacion.Actualizar:
                        ASPxDataUpdatingEventArgs eU = (ASPxDataUpdatingEventArgs)oIView.oCrud;

                        oTramo.iNoPax = eU.Keys[0].S().I();
                        oTramo.sNombrePax = eU.NewValues["NombrePax"].S();
                        oTramo.sUsuarioCreacion = eU.NewValues["UserIdentity"].S();
                        break;
                    case Enumeraciones.TipoOperacion.Eliminar:
                        ASPxDataDeletingEventArgs eD = (ASPxDataDeletingEventArgs)oIView.oCrud;
                        oTramo.iNoPax = eD.Keys[0].S().I();
                        break;
                }

                return oTramo;
            }
        }
        private TramoSolicitud oCatalogoComisariatoTramo
        {
            get
            {
                TramoSolicitud oTramo = new TramoSolicitud();
                switch (oIView.eCrud)
                {
                    case Enumeraciones.TipoOperacion.Insertar:
                        ASPxDataInsertingEventArgs eI = (ASPxDataInsertingEventArgs)oIView.oCrud;
                        oTramo.iIdTramo = eI.NewValues["IdTramo"].S().I();
                        oTramo.iIdProveedor = eI.NewValues["Descripcion"].S().I();
                        oTramo.sComisariatoDesc = eI.NewValues["ComisariatoDesc"].S();
                        oTramo.dPrecioCotizado = eI.NewValues["PrecioCotizado"].S().D();
                        oTramo.sUsuarioCreacion = eI.NewValues["UserIdentity"].S();
                        break;
                    case Enumeraciones.TipoOperacion.Actualizar:
                        ASPxDataUpdatingEventArgs eU = (ASPxDataUpdatingEventArgs)oIView.oCrud;

                        oTramo.iIdComisariato = eU.Keys[0].S().I();
                        oTramo.iIdProveedor = eU.NewValues["Descripcion"].S().I() == 0 ? eU.Keys[1].S().I() : eU.NewValues["Descripcion"].S().I()   ;
                        oTramo.sComisariatoDesc = eU.NewValues["ComisariatoDesc"].S();
                        oTramo.dPrecioCotizado = eU.NewValues["PrecioCotizado"].S().D();
                        oTramo.sUsuarioCreacion = eU.NewValues["UserIdentity"].S();

                        break;
                    case Enumeraciones.TipoOperacion.Eliminar:
                        ASPxDataDeletingEventArgs eD = (ASPxDataDeletingEventArgs)oIView.oCrud;
                        oTramo.iIdComisariato = eD.Keys[0].S().I();
                        break;
                }

                return oTramo;
            }
        }
       
    }
}