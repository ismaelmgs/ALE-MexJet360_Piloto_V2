using ALE_MexJet.DomainModel;
using ALE_MexJet.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ALE_MexJet.Objetos;
using NucleoBase.Core;
using ALE_MexJet.Clases;
using DevExpress.Web.Data;

namespace ALE_MexJet.Presenter
{
    public class TarifaGrupoModeloEz_Presenter : BasePresenter<IViewCat>
    {
        private readonly DBTarifaGrupoModeloEz oIGestCat;

        public TarifaGrupoModeloEz_Presenter(IViewCat oView, DBTarifaGrupoModeloEz oGC)
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
                TarifaGrupoModeloEz oTarifa = oCatalogo;
                int id = oIGestCat.DBUpdate(oTarifa);
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
                TarifaGrupoModeloEz oTarifa = oCatalogo;
                int id = oIGestCat.DBSave(oTarifa);
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
                TarifaGrupoModeloEz oTarifa = oCatalogo;
                int id = oIGestCat.DBDelete(oTarifa);
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

        private TarifaGrupoModeloEz oCatalogo
        {
            get
            {
                TarifaGrupoModeloEz oRol = new TarifaGrupoModeloEz();
                switch (oIView.eCrud)
                {
                    case Enumeraciones.TipoOperacion.Insertar:
                        ASPxDataInsertingEventArgs eI = (ASPxDataInsertingEventArgs)oIView.oCrud;
                        oRol.iId = 0;
                        oRol.iGrupoModelo = eI.NewValues["IdGrupoModelo"].S().I();
                        oRol.dTarifaNal = eI.NewValues["TarifaNacional"].S().I();
                        oRol.dTarifaInt = eI.NewValues["TarifaInternacional"].S().D();
                        oRol.iStatus = 1;

                        break;
                    case Enumeraciones.TipoOperacion.Actualizar:
                        ASPxDataUpdatingEventArgs eU = (ASPxDataUpdatingEventArgs)oIView.oCrud;
                        oRol.iId = eU.Keys[0].S().I();
                        oRol.iGrupoModelo = eU.NewValues["IdGrupoModelo"].S().I();
                        oRol.dTarifaNal = eU.NewValues["TarifaNacional"].S().D();
                        oRol.dTarifaInt = eU.NewValues["TarifaInternacional"].S().D();
                        oRol.iStatus = eU.NewValues["Status"].S().I();

                        break;
                    case Enumeraciones.TipoOperacion.Validar:
                        ASPxDataValidationEventArgs eV = (ASPxDataValidationEventArgs)oIView.oCrud;
                        oRol.iGrupoModelo = eV.NewValues["IdGrupoModelo"].S().I();

                        break;
                    case Enumeraciones.TipoOperacion.Eliminar:
                        ASPxDataDeletingEventArgs eD = (ASPxDataDeletingEventArgs)oIView.oCrud;
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

                bValida = eU.NewValues["IdGrupoModelo"].S().ToUpper() != eU.OldValues["IdGrupoModelo"].S().ToUpper();

                return bValida;
            }

        }
    }
}