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
    public class Inflacion_Presenter : BasePresenter<IViewInflacion>
    {
        private readonly DBInflacion oIGestCat;

        public Inflacion_Presenter(IViewInflacion oView, DBInflacion oGC)
            : base(oView)
        {
            oIGestCat = oGC;
            oIView.eGetSearchDetalle += oIView_eGetSearchDetalle;

            oView.eSaveObjDet += SaveObj_PresenterDet;
            oView.eNewObjDet += NewObj_PresenterDet;
            oView.eObjSelectedDet += ObjSelectedDet_Presenter;
            oView.eDeleteObjDet += DeleteObj_PresenterDet;
        }

        private void DeleteObj_PresenterDet(object sender, EventArgs e)
        {
            try
            {
                //Inflacion oInflacionDetalle = oCatalogoDet;
                int id = oIGestCat.DBDeleteDetalle(oCatalogoDet);
                if (id > 0)
                {
                    oIView.MostrarMensajeDet(Mensajes.LMensajes[Enumeraciones.Mensajes.Eliminacion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
                    oIView.ObtieneValoresDetalles();
                }
            }
            catch (Exception ex)
            {
                oIView.MostrarMensaje("Se detectó el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
            }
        }

        private void ObjSelectedDet_Presenter(object sender, EventArgs e)
        {
            int existe = 0;
            if (oIView.eCrud == Enumeraciones.TipoOperacion.Actualizar)
            {
                if (bValidaActualizacion)
                {
                    oIView.eCrud = Enumeraciones.TipoOperacion.Validar;
                    existe = oIGestCat.DBValidaDetalle(oCatalogoDet);
                }

            }
            else
            {
                oIView.eCrud = Enumeraciones.TipoOperacion.Validar;
                existe = oIGestCat.DBValidaDetalle(oCatalogoDet);
            }
            oIView.bDuplicado = existe > 0;
        }

        private void NewObj_PresenterDet(object sender, EventArgs e)
        {
            try
            {
                int id = oIGestCat.DBSaveDetalle(oCatalogoDet);
                if (id > 0)
                {
                    oIView.MostrarMensajeDet(Mensajes.LMensajes[Enumeraciones.Mensajes.Insercion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
                    oIView.ObtieneValoresDetalles();
                }
            }
            catch (Exception ex)
            {
                oIView.MostrarMensaje("Se detecto el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
            }
        }

        private void SaveObj_PresenterDet(object sender, EventArgs e)
        {
            try
            {
                int id = oIGestCat.DBUpdateDetalle(oCatalogoDet);
                if (id > 0)
                {
                    oIView.ObtieneValoresDetalles();
                    oIView.MostrarMensajeDet(Mensajes.LMensajes[Enumeraciones.Mensajes.Modificacion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
                }
            }
            catch (Exception ex)
            {
                oIView.MostrarMensaje("Se detecto el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
            }
        }

        void oIView_eGetSearchDetalle(object sender, EventArgs e)
        {
            oIView.LoadObjectsDetalle(oIGestCat.DBSearchObjDetalle(oIView.oArrFiltrosDetalle));
        }

        public void LoadObjects_Presenter()
        {
            oIView.LoadObjects(oIGestCat.DBSearchObj(oIView.oArrFiltros));
        }

        protected override void SearchObj_Presenter(object sender, EventArgs e)
        {
            oIView.LoadObjects(oIGestCat.DBSearchObj(oIView.oArrFiltros));
        }

        protected override void SaveObj_Presenter(object sender, EventArgs e)
        {
            try
            {
                Inflacion oInflacion = oCatalogo;
                int id = oIGestCat.DBUpdate(oInflacion);
                if (id > 0)
                {
                    oIView.ObtieneValores();
                    oIView.MostrarMensaje(Mensajes.LMensajes[Enumeraciones.Mensajes.Modificacion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje); 	
                }
            }
            catch (Exception ex)
            {
                oIView.MostrarMensaje("Se detectó el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
            }
        }

        protected override void NewObj_Presenter(object sender, EventArgs e)
        {
            try
            {
                Inflacion oInflacion = oCatalogo;
                int id = oIGestCat.DBSave(oInflacion);
                if (id > 0)
                {
                    oIView.MostrarMensaje(Mensajes.LMensajes[Enumeraciones.Mensajes.Insercion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
                    oIView.ObtieneValores();
                }
            }
            catch (Exception ex)
            {
                oIView.MostrarMensaje("Se detectó el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
            }
        }

        protected override void DeleteObj_Presenter(object sender, EventArgs e)
        {
            try
            {
                Inflacion oInflacion = oCatalogo;
                int id = oIGestCat.DBDelete(oInflacion);
                if (id > 0)
                {
                    oIView.MostrarMensaje(Mensajes.LMensajes[Enumeraciones.Mensajes.Eliminacion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
                    oIView.ObtieneValores();
                }
            }
            catch (Exception ex)
            {
                oIView.MostrarMensaje("Se detectó el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
            }
        }

        protected override void ObjSelected_Presenter(object sender, EventArgs e)
        {
            int existe = 0;
            if (oIView.eCrud == Enumeraciones.TipoOperacion.Actualizar)
            {
                if (bValidaActualizacion)
                {
                    oIView.eCrud = Enumeraciones.TipoOperacion.Validar;
                    existe = oIGestCat.DBValida(oCatalogo);
                }

            }
            else
            {
                oIView.eCrud = Enumeraciones.TipoOperacion.Validar;
                existe = oIGestCat.DBValida(oCatalogo);
            }
            oIView.bDuplicado = existe > 0;
        }

        private Inflacion oCatalogo
        {
            get
            {
                Inflacion oInflacion = new Inflacion();
                switch (oIView.eCrud)
                {
                    case Enumeraciones.TipoOperacion.Insertar:
                        ASPxDataInsertingEventArgs eI = (ASPxDataInsertingEventArgs)oIView.oCrud;
                        oInflacion.iId = 0;
                        oInflacion.sTipoInflacion = eI.NewValues["TipoInflacion"].S();
                        oInflacion.iAño = eI.NewValues["Año"].S().I();
                        oInflacion.dcPorcentaje= eI.NewValues["Porcentaje"].S().D();
                        oInflacion.iStatus = 1;

                        break;
                    case Enumeraciones.TipoOperacion.Actualizar:
                        ASPxDataUpdatingEventArgs eU = (ASPxDataUpdatingEventArgs)oIView.oCrud;

                        oInflacion.iId = eU.Keys[0].S().I();
                        oInflacion.sTipoInflacion = eU.NewValues["TipoInflacion"].S();
                        oInflacion.iAño = eU.NewValues["Año"].S().I();
                        oInflacion.dcPorcentaje = eU.NewValues["Porcentaje"].S().D();
                        oInflacion.iStatus = eU.NewValues["Status"].S().I();

                        break;
                    case Enumeraciones.TipoOperacion.Validar:
                        ASPxDataValidationEventArgs eV = (ASPxDataValidationEventArgs)oIView.oCrud;

                        oInflacion.sTipoInflacion = eV.NewValues["TipoInflacion"].S();
                        oInflacion.iAño = eV.NewValues["Año"].S().I();
                        oInflacion.dcPorcentaje = eV.NewValues["Porcentaje"].S().D();
                        oInflacion.iStatus = eV.NewValues["Status"].S().I();

                        break;
                    case Enumeraciones.TipoOperacion.Eliminar:
                        ASPxDataDeletingEventArgs eD = (ASPxDataDeletingEventArgs)oIView.oCrud;
                        oInflacion.iId = eD.Keys[0].S().I();
                        oInflacion.sTipoInflacion = eD.Values["TipoInflacion"].S();
                        break;
                }

                return oInflacion;
            }
        }
        private bool bValidaActualizacion
        {
            get
            {
                bool bValida = true;
                ASPxDataValidationEventArgs eU = (ASPxDataValidationEventArgs)oIView.oCrud;

                bValida = (eU.NewValues["TipoInflacion"].S().ToUpper() != eU.OldValues["TipoInflacion"].S().ToUpper()) || (eU.NewValues["Año"].S().ToUpper() != eU.OldValues["Año"].S().ToUpper());

                return bValida;
            }
        }

        private Inflacion oCatalogoDet
        {
            get
            {
                Inflacion oInflacionDet = new Inflacion();
                switch (oIView.eCrud)
                {
                    case Enumeraciones.TipoOperacion.Insertar:
                        ASPxDataInsertingEventArgs eI = (ASPxDataInsertingEventArgs)oIView.oCrud;
                        oInflacionDet.iIdDetalle = 0;
                        oInflacionDet.iId = oIView.iIdInflacion;
                        //oInflacionDet.sTipoInflacion = eI.NewValues["TipoInflacion"].S();
                        oInflacionDet.iAño = eI.NewValues["Año"].S().I();
                        oInflacionDet.dcPorcentaje = eI.NewValues["Porcentaje"].S().D();
                        oInflacionDet.iStatus = 1;

                        break;
                    case Enumeraciones.TipoOperacion.Actualizar:
                        ASPxDataUpdatingEventArgs eU = (ASPxDataUpdatingEventArgs)oIView.oCrud;

                        oInflacionDet.iIdDetalle = eU.Keys[0].S().I();
                        //oInflacionDet.sTipoInflacion = eU.NewValues["TipoInflacion"].S();
                        oInflacionDet.iAño = eU.NewValues["Año"].S().I();
                        oInflacionDet.dcPorcentaje = eU.NewValues["Porcentaje"].S().D();
                        oInflacionDet.iStatus = eU.NewValues["Status"].S().I();
                        break;
                    case Enumeraciones.TipoOperacion.Validar:
                        ASPxDataValidationEventArgs eV = (ASPxDataValidationEventArgs)oIView.oCrud;
                        //oInflacionDet.sTipoInflacion = eV.NewValues["TipoInflacion"].S();
                        oInflacionDet.iId = oIView.iIdInflacion;
                        oInflacionDet.iAño = eV.NewValues["Año"].S().I();
                        oInflacionDet.dcPorcentaje = eV.NewValues["Porcentaje"].S().D();
                        oInflacionDet.iStatus = eV.NewValues["Status"].S().I();
                        break;
                    case Enumeraciones.TipoOperacion.Eliminar:
                        ASPxDataDeletingEventArgs eD = (ASPxDataDeletingEventArgs)oIView.oCrud;
                        oInflacionDet.iIdDetalle = eD.Keys[0].S().I();
                        oInflacionDet.iAño = eD.Values["Año"].S().I();
                        break;
                }

                return oInflacionDet;
            }
        }

    }
}