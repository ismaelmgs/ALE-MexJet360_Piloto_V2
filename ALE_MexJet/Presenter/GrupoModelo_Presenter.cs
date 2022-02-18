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
    public class GrupoModelo_Presenter : BasePresenter<IViewCat>
    {
        private readonly DBGrupoModelo oIGestCat;

        public GrupoModelo_Presenter(IViewCat oView, DBGrupoModelo oGC)
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
                GrupoModelo oGrupoModelo = oCatalogo;
                int id = oIGestCat.DBUpdate(oGrupoModelo);
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
                GrupoModelo oGrupoModelo = oCatalogo;
                int id = oIGestCat.DBSave(oGrupoModelo);
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
                GrupoModelo oGrupoModelo = oCatalogo;
                int id = oIGestCat.DBDelete(oGrupoModelo);
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

        private GrupoModelo oCatalogo
        {
            get
            {
                GrupoModelo oRol = new GrupoModelo();
                switch (oIView.eCrud)
                {
                    case Enumeraciones.TipoOperacion.Insertar:
                        ASPxDataInsertingEventArgs eI = (ASPxDataInsertingEventArgs)oIView.oCrud;
                        oRol.iId = 0;
                        oRol.sDescripcion = eI.NewValues["Descripcion"].S();
                        oRol.iGalones = eI.NewValues["ConsumoGalones"].S().I();
                        oRol.dTarifa = eI.NewValues["Tarifa"].S().D();
                        oRol.iNoPasajeros = eI.NewValues["NoPasajeros"].S().I();
                        oRol.iStatus = 1;

                        break;
                    case Enumeraciones.TipoOperacion.Actualizar:
                        ASPxDataUpdatingEventArgs eU = (ASPxDataUpdatingEventArgs)oIView.oCrud;

                        oRol.iId = eU.Keys[0].S().I();
                        oRol.sDescripcion = eU.NewValues["Descripcion"].S();
                        oRol.iGalones = eU.NewValues["ConsumoGalones"].S().I();
                        oRol.dTarifa = eU.NewValues["Tarifa"].S().D();
                        oRol.iNoPasajeros = eU.NewValues["NoPasajeros"].S().I();
                        oRol.iStatus = eU.NewValues["Status"].S().I();

                        break;
                    case Enumeraciones.TipoOperacion.Validar:
                        ASPxDataValidationEventArgs eV = (ASPxDataValidationEventArgs)oIView.oCrud;

                        oRol.sDescripcion = eV.NewValues["Descripcion"].S();
                        oRol.iGalones = eV.NewValues["ConsumoGalones"].S().I();
                        oRol.iStatus = eV.NewValues["Status"].S().I();

                        break;
                    case Enumeraciones.TipoOperacion.Eliminar:
                        ASPxDataDeletingEventArgs eD = (ASPxDataDeletingEventArgs)oIView.oCrud;
                        oRol.sDescripcion = eD.Values["Descripcion"].S();
                        oRol.iId = eD.Keys[0].S().I();
                        break;
                }

                return oRol;
            }
        }
        private bool bValidaActualizacion
        {
            get
            {
                bool bValida = true;
                ASPxDataValidationEventArgs eU = (ASPxDataValidationEventArgs)oIView.oCrud;

                bValida = eU.NewValues["Descripcion"].S().ToUpper() != eU.OldValues["Descripcion"].S().ToUpper();

                return bValida;
            }

        }
    }
}