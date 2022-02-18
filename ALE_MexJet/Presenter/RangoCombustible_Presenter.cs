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
    public class RangoCombustible_Presenter : BasePresenter<IViewRangoCombustible>
    {
        private readonly DBCombustibleGrupoModelo oIGestCat;
        private readonly DBRangoCombustible oIGestCatRan;

        public RangoCombustible_Presenter(IViewRangoCombustible oView, DBCombustibleGrupoModelo oGC, DBRangoCombustible oGCRan)
            : base(oView)
        {
            oIGestCat = oGC;
            oView.eSearchObjGru += eGetGrupoModelo_Presenter;
            oView.eSearchObjCon += eGetContrato_Presenter;

            oIGestCatRan = oGCRan;
            oView.eSearchObjRan += SearchObj_PresenterRan;
            oView.eSaveObjRan += SaveObj_PresenterRan;
            oView.eNewObjRan += NewObj_PresenterRan;
            oView.eObjSelectedRan += ObjSelectedRan_Presenter;
            oView.eDeleteObjRan += DeleteObj_PresenterRan;
            oIView.eValRango += ValidaRangos;
        }

        public void LoadObjects_Presenter()
        {
            oIView.LoadObjects(oIGestCat.DBSearchObj(oIView.oArrFiltros));
        }

        protected override void SearchObj_Presenter(object sender, EventArgs e)
        {
            oIView.LoadObjects(oIGestCat.DBSearchObj(oIView.oArrFiltros));
        }
        public void LoadObjects_PresenterRan()
        {
            oIView.LoadObjectsRangos(oIGestCatRan.DBSearchObj(oIView.oArrFiltros));
        }
        protected void SearchObj_PresenterRan(object sender, EventArgs e)
        {
            oIView.LoadObjectsRangos(oIGestCatRan.DBSearchObj(oIView.oArrFiltrosRan));
        }

        protected override void SaveObj_Presenter(object sender, EventArgs e)
        {
            try
            {
                int id = oIGestCat.DBUpdate(oCatalogo);
                if (id > 0)
                {
                    oIView.ObtieneValoresCombustible();
                    oIView.MostrarMensaje(Mensajes.LMensajes[Enumeraciones.Mensajes.Modificacion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
                }
            }
            catch (Exception ex)
            {
                oIView.MostrarMensaje("Se detectó el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
            }
        }
        protected void SaveObj_PresenterRan(object sender, EventArgs e)
        {
            try
            {
                int id = oIGestCatRan.DBUpdate(oCatalogoRan);
                if (id > 0)
                {
                    oIView.ObtieneValoresRango();
                    oIView.MostrarMensajeRan(Mensajes.LMensajes[Enumeraciones.Mensajes.Modificacion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
                }
            }
            catch (Exception ex)
            {
                oIView.MostrarMensaje("Se detecto el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
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
                    oIView.ObtieneValoresCombustible();
                }
            }
            catch (Exception ex)
            {
                oIView.MostrarMensaje("Se detectó el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
            }
        }
        protected void NewObj_PresenterRan(object sender, EventArgs e)
        {
            try
            {
                int id = oIGestCatRan.DBSave(oCatalogoRan);
                if (id > 0)
                {
                    oIView.MostrarMensajeRan(Mensajes.LMensajes[Enumeraciones.Mensajes.Insercion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
                    oIView.ObtieneValoresRango();
                }
            }
            catch (Exception ex)
            {
                oIView.MostrarMensaje("Se detecto el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
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
                    oIView.ObtieneValoresCombustible();
                }
            }
            catch (Exception ex)
            {
                oIView.MostrarMensaje("Se detectó el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
            }
        }
        protected void DeleteObj_PresenterRan(object sender, EventArgs e)
        {
            try
            {
                int id = oIGestCatRan.DBDelete(oCatalogoRan);

                if (id > 0)
                {
                    oIView.MostrarMensajeRan(Mensajes.LMensajes[Enumeraciones.Mensajes.Eliminacion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
                    oIView.ObtieneValoresRango();
                }
            }
            catch (Exception ex)
            {
                oIView.MostrarMensaje("Se detecto el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
            }
        }
        protected override void ObjSelected_Presenter(object sender, EventArgs e)
        {
            int existe = 0;
            if (oIView.eCrud == Enumeraciones.TipoOperacion.Actualizar)
            {
                oIView.eCrud = Enumeraciones.TipoOperacion.Validar;
                existe = oIGestCat.DBValida(oCatalogo);

            }
            else
            {
                oIView.eCrud = Enumeraciones.TipoOperacion.Validar;
                existe = oIGestCat.DBValida(oCatalogo);
            }
            oIView.bDuplicado = existe > 0;
        }
        protected void ObjSelectedRan_Presenter(object sender, EventArgs e)
        {
            int existe = 0;
            if (oIView.eCrud == Enumeraciones.TipoOperacion.Actualizar)
            {
                oIView.eCrud = Enumeraciones.TipoOperacion.Validar;
                existe = oIGestCatRan.DBValida(oCatalogoRan);

            }
            else
            {
                oIView.eCrud = Enumeraciones.TipoOperacion.Validar;
                existe = oIGestCatRan.DBValida(oCatalogoRan);
            }
            oIView.bDuplicado = existe > 0;
        }

        protected void eGetGrupoModelo_Presenter(object sender, EventArgs e)
        {
            oIView.LoadObjectsGrupoModelo(oIGestCat.DBSearchObjGrupoModelo(oIView.oArrFiltrosGruModelo));
        }
        protected void eGetContrato_Presenter(object sender, EventArgs e)
        {
            oIView.LoadObjectsContrato(oIGestCat.DBSearchObjContrato(oIView.oArrFiltrosCon));
        }

        protected void ValidaRangos(object sender, EventArgs e)
        {
            try
            {
                int IError = -1;
                if (oIView.eCrud == Enumeraciones.TipoOperacion.Actualizar)
                {

                    if (BValidaDesde || bValidaHasta)
                    {
                        oIView.eCrud = Enumeraciones.TipoOperacion.Validar;
                        IError = oIGestCatRan.DBValidaRangoUpdate(oCatalogoRan, BValidaDesde, bValidaHasta);
                    }
                }
                else
                {
                    oIView.eCrud = Enumeraciones.TipoOperacion.Validar;
                    IError = oIGestCatRan.DBValidaRango(oCatalogoRan, BValidaDesde, bValidaHasta);

                }
                oIView.bErrorRango = IError > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private CombustibleGrupoModelo oCatalogo
        {
            get
            {
                CombustibleGrupoModelo oCatalogoGrupo = new CombustibleGrupoModelo();
                switch (oIView.eCrud)
                {
                    case Enumeraciones.TipoOperacion.Insertar:
                        ASPxDataInsertingEventArgs eI = (ASPxDataInsertingEventArgs)oIView.oCrud;
                        oCatalogoGrupo.iIdGrupoModelo = eI.NewValues["IdGrupoModelo"].S().I();
                        oCatalogoGrupo.iIdTipoGrupo = eI.NewValues["IdTipoGrupo"].S().I();
                        oCatalogoGrupo.iIdTipoContrato = eI.NewValues["IdTipoContrato"].S().I();
                        oCatalogoGrupo.iStatus = 1;

                        break;
                    case Enumeraciones.TipoOperacion.Actualizar:
                        ASPxDataUpdatingEventArgs eU = (ASPxDataUpdatingEventArgs)oIView.oCrud;

                        oCatalogoGrupo.iIdCombustible = eU.Keys["IdCombustible"].S().I();
                        oCatalogoGrupo.iIdGrupoModelo = eU.NewValues["IdGrupoModelo"].S().I();
                        oCatalogoGrupo.iIdTipoGrupo = eU.NewValues["IdTipoGrupo"].S().I();
                        oCatalogoGrupo.iIdTipoContrato = eU.NewValues["IdTipoContrato"].S().I();
                        oCatalogoGrupo.iStatus = eU.NewValues["Status"].S().I();
                        break;
                    case Enumeraciones.TipoOperacion.Validar:
                        ASPxDataValidationEventArgs eV = (ASPxDataValidationEventArgs)oIView.oCrud;
                        oCatalogoGrupo.iIdGrupoModelo = eV.NewValues["IdGrupoModelo"].S().I();
                        oCatalogoGrupo.iIdTipoGrupo = eV.NewValues["IdTipoGrupo"].S().I();
                        oCatalogoGrupo.iIdTipoContrato = eV.NewValues["IdTipoContrato"].S().I();
                        oCatalogoGrupo.iStatus = eV.NewValues["Status"].S().I();
                        break;
                    case Enumeraciones.TipoOperacion.Eliminar:
                        ASPxDataDeletingEventArgs eD = (ASPxDataDeletingEventArgs)oIView.oCrud;
                        oCatalogoGrupo.iIdCombustible = eD.Keys["IdCombustible"].S().I();
                        break;
                }
                return oCatalogoGrupo;
            }
        }
        private RangoCombustible oCatalogoRan
        {
            get
            {
                RangoCombustible oRangoCombustible = new RangoCombustible();
                switch (oIView.eCrud)
                {
                    case Enumeraciones.TipoOperacion.Insertar:
                        ASPxDataInsertingEventArgs eI = (ASPxDataInsertingEventArgs)oIView.oCrud;
                        int iIdCombustibleI = (int)oIView.iIdCombustible;
                        oRangoCombustible.iIdCombustible = iIdCombustibleI;
                        oRangoCombustible.dDesde = eI.NewValues["Desde"].S().D();
                        oRangoCombustible.dHasta = eI.NewValues["Hasta"].S().D();
                        oRangoCombustible.dAumento = eI.NewValues["Aumento"].S().D();
                        oRangoCombustible.iStatus = 1;
                        break;
                    case Enumeraciones.TipoOperacion.Actualizar:
                        ASPxDataUpdatingEventArgs eU = (ASPxDataUpdatingEventArgs)oIView.oCrud;

                        oRangoCombustible.iIdRangoIden = eU.Keys["IdRangoIden"].S().I();
                        oRangoCombustible.dDesde = eU.NewValues["Desde"].S().D();
                        oRangoCombustible.dHasta = eU.NewValues["Hasta"].S().D();
                        oRangoCombustible.dAumento = eU.NewValues["Aumento"].S().D();
                        oRangoCombustible.iStatus = eU.NewValues["Status"].S().I();
                        break;
                    case Enumeraciones.TipoOperacion.Validar:
                        ASPxDataValidationEventArgs eV = (ASPxDataValidationEventArgs)oIView.oCrud;
                        int iIdCombustibleV = (int)oIView.iIdCombustible;
                        oRangoCombustible.iIdCombustible = iIdCombustibleV;

                        oRangoCombustible.dDesde = eV.NewValues["Desde"].S().D();
                        oRangoCombustible.iIdRangoIden = eV.Keys["IdRangoIden"].S().I();
                        oRangoCombustible.dHasta = eV.NewValues["Hasta"].S().D();
                        oRangoCombustible.dAumento = eV.NewValues["Aumento"].S().D();
                        oRangoCombustible.iStatus = eV.NewValues["Status"].S().I();
                        break;
                    case Enumeraciones.TipoOperacion.Eliminar:
                        ASPxDataDeletingEventArgs eD = (ASPxDataDeletingEventArgs)oIView.oCrud;
                        oRangoCombustible.iIdRangoIden = eD.Keys["IdRangoIden"].S().I();
                        break;
                }

                return oRangoCombustible;
            }
        }

        private bool bValidaHasta
        {
            get
            {
                bool bValida = true;
                ASPxDataValidationEventArgs eU = (ASPxDataValidationEventArgs)oIView.oCrud;

                bValida = (eU.NewValues["Hasta"].S().ToUpper() != eU.OldValues["Hasta"].S().ToUpper());

                return bValida;
            }
        }

        private bool BValidaDesde
        {
            get
            {
                bool bValida = true;
                ASPxDataValidationEventArgs eU = (ASPxDataValidationEventArgs)oIView.oCrud;

                bValida = (eU.NewValues["Desde"].S().ToUpper() != eU.OldValues["Desde"].S().ToUpper());

                return bValida;
            }
        }
    }
}