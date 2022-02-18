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
    public class TramoPactado_Presenter : BasePresenter<IViewTramoPactado>
    {
        private readonly DBTramoPactado oIGestCat;

        public TramoPactado_Presenter(IViewTramoPactado oView, DBTramoPactado oGC)
            : base(oView)
        {
            oIGestCat = oGC;
            oIView.eGetAeropuerto += GetAeronave_presenter;
            oIView.eGetGrupoModelo += GetGrupoModelo_presenter;

            oIView.eGetAeropuertoOrigen += GetOrigen;
            oIView.eGetAeropuertoOrigenFiltrado += GetOrigenFiltrado;
            oIView.eGetAeropuertoDestino += GetDestino;
            oIView.eGetAeropuertoDestinoFiltrado += GetDestinoFiltrado;
        }


        protected void GetOrigen(object sender, EventArgs e)
        {
            oIView.dtTarifaOrigen = oIGestCat.dtObjsOrigen;
        }

        protected void GetOrigenFiltrado(object sender, EventArgs e)
        {
            oIView.dtTarifaOrigen = oIGestCat.DBFiltraAeropuertoIATA(oIView.sFiltroAeropuerto);
        }

        protected void GetDestino(object sender, EventArgs e)
        {
            oIView.dtTarifaDestino = oIGestCat.DBCargaDestino(oIView.iOrigen);
        }

        protected void GetDestinoFiltrado(object sender, EventArgs e)
        {
            oIView.dtTarifaDestino = oIGestCat.DBFiltraAeropuertoIATADestino(oIView.sFiltroAeropuerto,oIView.iOrigen);
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
                TramoPactado TramoPactado = oCatalogo;
                int id = oIGestCat.DBUpdate(TramoPactado);
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
                TramoPactado TramoPactado = oCatalogo;
                int id = oIGestCat.DBSave(TramoPactado);
                if (id > 0)
                {                    
                    oIView.ObtieneValores();                    
                    oIView.MostrarMensaje(Mensajes.LMensajes[Enumeraciones.Mensajes.Insercion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
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
                TramoPactado TramoPactado = oCatalogo;
                int id = oIGestCat.DBDelete(TramoPactado);
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

        protected void GetAeronave_presenter(object sender, EventArgs e)
        {
            oIView.LoadCatalogoAeropuertos(oIGestCat.dtObjsCatAeropuerto);
        }

        protected void GetGrupoModelo_presenter(object sender, EventArgs e)
        {
            oIView.LoadCatalogoGrupoModelo(oIGestCat.dtObjsCat);
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

        private TramoPactado oCatalogo
        {
            get
            {
                TramoPactado oTramoPactado = new TramoPactado();
                switch (oIView.eCrud)
                {
                    case Enumeraciones.TipoOperacion.Insertar:
                        ASPxDataInsertingEventArgs eI = (ASPxDataInsertingEventArgs)oIView.oCrud;
                        oTramoPactado.iId = 0;
                        oTramoPactado.iIdGrupoModelo = eI.NewValues["IdGrupoModelo"].S().I();
                        oTramoPactado.iIdOrigen = eI.NewValues["IdOrigen"].S().I();
                        oTramoPactado.iIdDestino = eI.NewValues["IdDestino"].S().I();
                        oTramoPactado.sTiempoVuelo = eI.NewValues["TiempoDeVuelo"].S();
                        oTramoPactado.iStatus = 1;
                        break;
                    case Enumeraciones.TipoOperacion.Actualizar:
                        ASPxDataUpdatingEventArgs eU = (ASPxDataUpdatingEventArgs)oIView.oCrud;

                        oTramoPactado.iId = eU.Keys[0].S().I();
                        oTramoPactado.iIdGrupoModelo = eU.NewValues["IdGrupoModelo"].S().I();
                        oTramoPactado.iIdOrigen = eU.NewValues["IdOrigen"].S().I();
                        oTramoPactado.iIdDestino = eU.NewValues["IdDestino"].S().I();
                        oTramoPactado.sTiempoVuelo = eU.NewValues["TiempoDeVuelo"].S();
                        oTramoPactado.iStatus = eU.NewValues["Status"].S().I();
                        break;
                    case Enumeraciones.TipoOperacion.Validar:
                        ASPxDataValidationEventArgs eV = (ASPxDataValidationEventArgs)oIView.oCrud;
                        oTramoPactado.iIdGrupoModelo = eV.NewValues["IdGrupoModelo"].S().I();
                        oTramoPactado.iIdOrigen = eV.NewValues["IdOrigen"].S().I();
                        oTramoPactado.iIdDestino = eV.NewValues["IdDestino"].S().I();
                        oTramoPactado.sTiempoVuelo = eV.NewValues["TiempoDeVuelo"].S();
                        oTramoPactado.iStatus = eV.NewValues["Status"].S().I();

                        break;
                    case Enumeraciones.TipoOperacion.Eliminar:
                        ASPxDataDeletingEventArgs eD = (ASPxDataDeletingEventArgs)oIView.oCrud;
                        oTramoPactado.iIdGrupoModelo = eD.Values["IdGrupoModelo"].S().I();
                        oTramoPactado.iIdOrigen = eD.Values["IdOrigen"].S().I();
                        oTramoPactado.iIdDestino = eD.Values["IdDestino"].S().I();
                        oTramoPactado.iId = eD.Keys[0].S().I();
                        break;
                }

                return oTramoPactado;
            }
        }

        private bool bValidaActualizacion
        {
            get
            {
                bool bValida = true;
                ASPxDataValidationEventArgs eV = (ASPxDataValidationEventArgs)oIView.oCrud;

                bValida = ((eV.NewValues["IdGrupoModelo"].S().I() != eV.OldValues["IdGrupoModelo"].S().I()) || (eV.NewValues["IdOrigen"].S().I() != eV.OldValues["IdOrigen"].S().I()) || (eV.NewValues["IdDestino"].S().I() != eV.OldValues["IdDestino"].S().I()));

                return bValida;
            }
        }

    }
}