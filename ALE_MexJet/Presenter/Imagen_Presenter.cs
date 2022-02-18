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
    public class Imagen_Presenter : BasePresenter<IViewImagen>
    {
        private readonly DBImagen oIGestCat;

        public Imagen_Presenter(IViewImagen oView, DBImagen oGC)
            : base(oView)
        {
            oIGestCat = oGC;
            oIView.pSearchObj += pSearchObj_Presenter;
            oIView.IUpdObj += IUpdObj_Presenter;
            oIView.SearchObC += SearchObjC_Presenter;
            oIView.SearchObI += SearchObjI_Presenter;
            oIView.SearchObD += SearchObjID_Presenter;
            oIView.SaveUpdaI += SaveUpdaI_Presenter;
            oIView.SearchObjPilot += SearchObjPilot_Presenter;
            oView.eEditaPaxTramo += EditaPaxTamo_Presenter;
            oView.eEliminaPaxTramo += EliminaPaxTramo_Presenter;
            oView.eInsertaPasajero += NewPasajero_Presenter;
            oView.eInsertaPaxTramo += InsertaPaxTramo_Presenter;
            oView.eObtienePaxTramo += LoadPaxTramo_Presenter;
            oView.eLoadPaxTramo2 += LoadPaxTramo2_Presenter;
            oView.eBuscaPasajero += LoadPasajeroFltro_Presenter;
            oView.eInsertaInteriores += NewNotaInteriores_Presenter;
            oView.eConsultaArea += SearchArea_Presenter;
            oView.eEnviaNotificacionDespacho += oView_eEnviaNotificacionDespacho;
        }

        void oView_eEnviaNotificacionDespacho(object sender, EventArgs e)
        {
            int id = oIGestCat.DBUpdateMonitorDespacho(oIView.oArrFilUpd);
            if (id > 0)
            {
                oIView.MostrarMensaje(Mensajes.LMensajes[Enumeraciones.Mensajes.Insercion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);

            }

        }
        public void LoadPasajeroFltro_Presenter(object sender, EventArgs e)
        {
            string filtro = sender.S();
            oIView.CargaPasajeros(oIGestCat.DBObtienePasajeroFiltro(filtro));
        }
        public void LoadPaxTramo2_Presenter(object sender, EventArgs e)
        {
            int filtro = sender.I();
            oIView.LoadPaxTramo2(oIGestCat.DBObtienePaxTramo(filtro));
        }
        public void LoadPaxTramo_Presenter(object sender, EventArgs e)
        {
            int filtro = sender.I();
            oIView.LoadPaxTramo(oIGestCat.DBObtienePaxTramo(filtro));
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
        protected override void SearchObj_Presenter(object sender, EventArgs e)
        {
            try
            {
                oIView.LoadObjects(oIGestCat.DBSearchObj(oIView.oBusqueda));
            }
            catch (Exception x) { throw x; }
        }
        protected  void pSearchObj_Presenter(object sender, EventArgs e)
        {
            try
            {
                oIView.LoadPersona(oIGestCat.DBSearchObjP(oIView.pBusqueda));
            }
            catch (Exception x) { throw x; }
        }
        protected void IUpdObj_Presenter(object sender, EventArgs e)
        {
            try
            {
                Actualiza();
            }
            catch (Exception x) { throw x; }
        }

        

        protected void Actualiza()
        {
            try
            {
                Imagen oImagen = new Imagen();

                int Resultado = 0;
                List<ASPxDataUpdateValues> oLiUpValues = null;
                switch (oIView.eCrud)
                {
                    case Enumeraciones.TipoOperacion.Insertar:
                        ASPxDataBatchUpdateEventArgs eU = (ASPxDataBatchUpdateEventArgs)oIView.oCrud;
                        oLiUpValues = eU.UpdateValues;
                        foreach (ASPxDataUpdateValues oUpValues in oLiUpValues)
                        {
                            Imagen IMG = new Imagen();

                            oImagen.IdPax = oUpValues.Keys["IdPax"].S().I();
                            oImagen.Arrivo = oUpValues.NewValues["Arrivo"].B();
                            oImagen.HoraLlegada = oUpValues.NewValues["HoraLlegada"].S();

                            Resultado = oIGestCat.DBUpdate(oImagen);
                        }
                        break;
                    case Enumeraciones.TipoOperacion.Actualizar:
                        break;
                }

                if (Resultado > 0)
                {
                    oIView.MostrarMensaje("Se actualizo la infración", "REGISTRO ACTUALIZADO");
                }
            }
            catch (Exception x) { throw x; }
            }
        protected void SearchObjC_Presenter(object sender, EventArgs e)
         {
             try
             {
                 oIView.LoadCliente(oIGestCat.DBSearchObjC(oIView.BusquedaC));
             }
             catch (Exception x) { throw x; }
         }
        protected void SearchObjI_Presenter(object sender, EventArgs e)
         {
             try
             {
                 Imagen I = new Imagen();
                 I.Opcion = sender.S();
                 oIView.LoadImagen(oIGestCat.DBSearchObjI(I));
             }
             catch (Exception x) { throw x; }
         }
        protected void SearchObjID_Presenter(object sender, EventArgs e)
         {
             try
             {
                 oIView.LoadImagenD(oIGestCat.DBSearchObjID(oIView.BusquedaI));
             }
             catch (Exception x) { throw x; }
         }
        protected void SaveUpdaI_Presenter(object sender, EventArgs e)
         {
             try
             {
                 InsertActualiImagen(sender);
             }
             catch (Exception x) { throw x; } 
         }
        protected void InsertActualiImagen(object Z)
         {
             try
             {
                 Imagen oImagen = new Imagen();

                 int Resultado = 0;
                 List<ASPxDataUpdateValues> oLiUpValues = null;
                 switch (oIView.eCrud)
                 {
                     case Enumeraciones.TipoOperacion.Insertar:
                         ASPxDataBatchUpdateEventArgs eU = (ASPxDataBatchUpdateEventArgs)oIView.oCrud;
                         oLiUpValues = eU.UpdateValues;
                         foreach (ASPxDataUpdateValues oUpValues in oLiUpValues)
                         {
                             oImagen.IdControl = oUpValues.Keys["IdControl"].S().I();
                             oImagen.IdImagen = oUpValues.Keys["IdImagenD"].S().I();
                             oImagen.AbordadoPre = Convert.ToBoolean(oUpValues.NewValues["AbordadoPre"]);
                             oImagen.AbordadoPos = Convert.ToBoolean(oUpValues.NewValues["AbordadoPos"]);
                             oImagen.ObservacionesPre = oUpValues.NewValues["ObservacionesPre"].S();
                             oImagen.ObservacionesPos = oUpValues.NewValues["ObservacionesPos"].S();
                             oImagen.IdTramo = oUpValues.Keys["IdTramo"].S();
                             oImagen.PostFlight = oUpValues.NewValues["PostFlight"].S();
                             oImagen.Opcion = Z.S();
                             Resultado = oIGestCat.DBSave(oImagen);
                         }
                         break;
                     case Enumeraciones.TipoOperacion.Actualizar:
                         break;
                 }

                 if (Resultado > 0)
                 {
                     oIView.MostrarMensaje("Se actualizo la infración", "REGISTRO ACTUALIZADO");
                 }
             }
             catch (Exception x) { throw x; }
         }
        protected void SearchObjPilot_Presenter(object sender, EventArgs e)
         {
             try
             {
                 Imagen I = new Imagen();
                 I.IdSolicitud = sender.S();

                 oIView.LoadPilot(oIGestCat.DBSearchObjPilot(I));
             }
             catch (Exception x) { throw x; }
         }
        public void NewNotaInteriores_Presenter(object sender, EventArgs e)
        {
            try
            {
                int id = oIGestCat.DBInsertaNotas(oIView.oNotas);
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
        protected void SearchArea_Presenter(object sender, EventArgs e)
        {
            try
            {
                oIView.LoadArea(oIGestCat.DBObtieneArea(sender.S()));
            }
            catch (Exception x) { throw x; }
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
    }
}
 