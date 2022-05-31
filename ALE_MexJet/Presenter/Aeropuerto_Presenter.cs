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
    public class Aeropuerto_Presenter: BasePresenter<IViewCat>
    {
        private readonly DBAeropuerto oIGestCat;

        public Aeropuerto_Presenter(IViewCat oView, DBAeropuerto oGC)
            : base(oView)
        {
            oIGestCat = oGC;
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
                int id = oIGestCat.DBUpdate(oCatalogo);
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
                int id = oIGestCat.DBSave(oCatalogo);
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
                int id = oIGestCat.DBDelete(oCatalogo);
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

        private Aeropuerto oCatalogo
        {
            get
            {
                Aeropuerto oAeropuerto = new Aeropuerto();
                switch (oIView.eCrud)
                {
                   
                    case Enumeraciones.TipoOperacion.Actualizar:
                        ASPxDataUpdatingEventArgs eU = (ASPxDataUpdatingEventArgs)oIView.oCrud;
                        //ASPxDataUpdateValues eU = (ASPxDataUpdateValues)oIView.oCrud;

                        oAeropuerto.iId = eU.Keys[0].S().I();
                        oAeropuerto.TipoDestino = eU.NewValues["TipoDestino"].S();
                        oAeropuerto.dAeropuertoHelipuertoTarifa= eU.NewValues["AeropuertoHelipuertoTarifa"].S().D();
                        oAeropuerto.bBase = eU.NewValues["Base"].S().B();
                        oAeropuerto.bAeropuertoHelipuerto = eU.NewValues["AeropuertoHelipuerto"].S().B();
                        oAeropuerto.bCobraAterrizaje = eU.NewValues["SeCobraAterrizaje"].S().B();
                        oAeropuerto.dAterrizajeNal = eU.NewValues["AterrizajeNal"].S().D();
                        oAeropuerto.dAterrizajeInt = eU.NewValues["AterrizajeInt"].S().D();
                        oAeropuerto.iStatus = eU.NewValues["Status"].S().I();
                        oAeropuerto.iTipoAeropuerto = eU.NewValues["TipoAeropuerto"].S().I(); 
                        break;
                }

                return oAeropuerto;
            }
        }
    }
}