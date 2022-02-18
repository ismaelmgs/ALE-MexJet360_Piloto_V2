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
    public class CombustibleSemanal_Presenter : BasePresenter<IViewCombustibleSemanal>
    {
        private readonly DBCombustibleSemanal oIGestCat;
        private readonly DBCombustibleSemanalDetalle oIGestCatDet;

        public CombustibleSemanal_Presenter(IViewCombustibleSemanal oView, DBCombustibleSemanal oGC, DBCombustibleSemanalDetalle oGCDet)
            : base(oView)
        {
            oIGestCat = oGC;
            oView.eSearchObjMes += eGetMes_Presenter;
            oView.eSearchObjAero += eGetAero_Presenter;

            oIGestCatDet = oGCDet;
            oView.eSearchObjDet += SearchObj_PresenterDet;
            oView.eSaveObjDet += SaveObj_PresenterDet;
            oView.eNewObjDet += NewObj_PresenterDet;
            oView.eObjSelectedDet += ObjSelectedDet_Presenter;
            oView.eDeleteObjDet += DeleteObj_PresenterDet;
            oIView.eValTipoCambio += ValidaTipoCambio;
        }

        public void LoadObjects_Presenter()
        {
            oIView.LoadObjects(oIGestCat.DBSearchObj(oIView.oArrFiltros));
        }
        protected override void SearchObj_Presenter(object sender, EventArgs e)
        {
            oIView.LoadObjects(oIGestCat.DBSearchObj(oIView.oArrFiltros));
        }
        public void LoadObjects_PresenterDet()
        {
            oIView.LoadObjectsDetalle(oIGestCatDet.DBSearchObj(oIView.oArrFiltros));
        }
        protected void SearchObj_PresenterDet(object sender, EventArgs e)
        {
            oIView.LoadObjectsDetalle(oIGestCatDet.DBSearchObj(oIView.oArrFiltrosDet));
        }
        protected override void SaveObj_Presenter(object sender, EventArgs e)
        {
            try
            {
                CombustibleSemanal objCat = oCatalogo;
                int id = oIGestCat.DBUpdate(objCat);
                if (id > 0)
                {
                    oIView.ObtieneValoresCombustible();
                    oIView.MostrarMensaje(Mensajes.LMensajes[Enumeraciones.Mensajes.Modificacion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
                }
            }
            catch (Exception ex)
            {
                oIView.MostrarMensaje("Se detecto el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
            }
        }
        protected void SaveObj_PresenterDet(object sender, EventArgs e)
        {
            try
            {
                int id = oIGestCatDet.DBUpdate(oCatalogoDet);
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
                oIView.MostrarMensaje("Se detecto el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
            }
        }
        protected void NewObj_PresenterDet(object sender, EventArgs e)
        {
            try
            {
                int id = oIGestCatDet.DBSave(oCatalogoDet);
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
                oIView.MostrarMensaje("Se detecto el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
            }
        }
        protected void DeleteObj_PresenterDet(object sender, EventArgs e)
        {
            try
            {
                int id = oIGestCatDet.DBDelete(oCatalogoDet);

                if (id > 0)
                {
                    oIView.MostrarMensajeDet(Mensajes.LMensajes[Enumeraciones.Mensajes.Eliminacion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
                    oIView.ObtieneValoresDetalles();
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
        protected void ObjSelectedDet_Presenter(object sender, EventArgs e)
        {
            int existe = 0;
            if (oIView.eCrud == Enumeraciones.TipoOperacion.Actualizar)
            {
                if (bValidaActualizacionDet)
                {
                    oIView.eCrud = Enumeraciones.TipoOperacion.Validar;
                    existe = oIGestCatDet.DBValida(oCatalogoDet);
                }
            }
            else
            {
                oIView.eCrud = Enumeraciones.TipoOperacion.Validar;
                existe = oIGestCatDet.DBValida(oCatalogoDet);
            }
            oIView.bDuplicado = existe > 0;
        }
        
        protected void ValidaTipoCambio(object sender, EventArgs e)
        {
            try
            {
                oIView.eCrud = Enumeraciones.TipoOperacion.Validar;
                int iExiste = oIGestCat.DBValidaTipoCambio(oCatalogo);
                oIView.bExisteTipoCambio = iExiste > 0;
            
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        protected void eGetMes_Presenter(object sender, EventArgs e)
        {
            oIView.LoadObjectsMes(oIGestCat.DBSearchObjMes(oIView.oArrFiltrosMes));
        }
        protected void eGetAero_Presenter(object sender, EventArgs e)
        {
            oIView.LoadObjectsAero(oIGestCat.DBSearchObjAero(oIView.oArrFiltrosAero));
        }
        private CombustibleSemanal oCatalogo
        {
            get
            {
                CombustibleSemanal oCatalogo = new CombustibleSemanal();
                switch (oIView.eCrud)
                {
                    case Enumeraciones.TipoOperacion.Insertar:
                        ASPxDataInsertingEventArgs eI = (ASPxDataInsertingEventArgs)oIView.oCrud;
                        oCatalogo.iAnio = eI.NewValues["Anio"].S().I();
                        oCatalogo.iIdMes = eI.NewValues["IdMes"].S().I();
                        oCatalogo.iIdAeropuerto = eI.NewValues["IdAeropuerto"].S().I();
                        oCatalogo.iStatus = 1;

                        break;
                    case Enumeraciones.TipoOperacion.Actualizar:
                        ASPxDataUpdatingEventArgs eU = (ASPxDataUpdatingEventArgs)oIView.oCrud;

                        oCatalogo.iIdCombustibleSem = eU.Keys["IdCombustibleSem"].S().I();
                        oCatalogo.iAnio = eU.NewValues["Anio"].S().I(); ;
                        oCatalogo.iIdMes = eU.NewValues["IdMes"].S().I();
                        oCatalogo.iIdAeropuerto = eU.NewValues["IdAeropuerto"].S().I();
                        oCatalogo.iStatus = eU.NewValues["Status"].S().I();
                        break;
                    case Enumeraciones.TipoOperacion.Validar:
                        ASPxDataValidationEventArgs eV = (ASPxDataValidationEventArgs)oIView.oCrud;
                        oCatalogo.iAnio = eV.NewValues["Anio"].S().I();
                        oCatalogo.iIdMes = eV.NewValues["IdMes"].S().I();
                        oCatalogo.iIdAeropuerto = eV.NewValues["IdAeropuerto"].S().I();
                        oCatalogo.iStatus = 1;
                        break;
                    case Enumeraciones.TipoOperacion.Eliminar:
                        ASPxDataDeletingEventArgs eD = (ASPxDataDeletingEventArgs)oIView.oCrud;
                        oCatalogo.iIdCombustibleSem = eD.Keys["IdCombustibleSem"].S().I();
                        break;
                }
                return oCatalogo;
            }
        }
        private CombustibleSemanalDetalle oCatalogoDet
        {
            get
            {
                CombustibleSemanalDetalle oCatalogoDet = new CombustibleSemanalDetalle();
                switch (oIView.eCrud)
                {
                    case Enumeraciones.TipoOperacion.Insertar:
                        ASPxDataInsertingEventArgs eI = (ASPxDataInsertingEventArgs)oIView.oCrud;
                        int iIdCombustibleSemI = (int)oIView.iIdCombustibleSem;
                        oCatalogoDet.iIdCombustibleSem = iIdCombustibleSemI;
                        oCatalogoDet.iSemana = eI.NewValues["Semana"].S().I();
                        oCatalogoDet.dCostoXLitro = eI.NewValues["CostoXLitro"].S().D();
                        //oCatalogoDet.dGalonMXN = eI.NewValues["GalonMXN"].S().D();
                        //oCatalogoDet.dGalonUSD = eI.NewValues["GalonUSD"].S().D();
                        //oCatalogoDet.dTipoCambio = eI.NewValues["TipoCambio"].S().D();
                        oCatalogoDet.iStatus = 1;

                        break;
                    case Enumeraciones.TipoOperacion.Actualizar:
                        ASPxDataUpdatingEventArgs eU = (ASPxDataUpdatingEventArgs)oIView.oCrud;

                        oCatalogoDet.iIdCombustibleSemDet = eU.Keys["IdCombustibleSemDet"].S().I();
                        oCatalogoDet.iSemana = eU.NewValues["Semana"].S().I();
                        oCatalogoDet.dCostoXLitro = eU.NewValues["CostoXLitro"].S().D();
                        //oCatalogoDet.dGalonMXN = eU.NewValues["GalonMXN"].S().D();
                        //oCatalogoDet.dGalonUSD = eU.NewValues["GalonUSD"].S().D();
                        //oCatalogoDet.dTipoCambio = eU.NewValues["TipoCambio"].S().D();
                        oCatalogoDet.iStatus = eU.NewValues["Status"].S().I();
                        break;
                    case Enumeraciones.TipoOperacion.Validar:
                        ASPxDataValidationEventArgs eV = (ASPxDataValidationEventArgs)oIView.oCrud;
                        int iIdCombustibleSemV = (int)oIView.iIdCombustibleSem;
                        oCatalogoDet.iIdCombustibleSem = iIdCombustibleSemV;
                        oCatalogoDet.iSemana = eV.NewValues["Semana"].S().I();
                        oCatalogoDet.iStatus = 1;
                        break;
                    case Enumeraciones.TipoOperacion.Eliminar:
                        ASPxDataDeletingEventArgs eD = (ASPxDataDeletingEventArgs)oIView.oCrud;
                        oCatalogoDet.iIdCombustibleSemDet = eD.Keys["IdCombustibleSemDet"].S().I();
                        break;
                }

                return oCatalogoDet;
            }
        }
        private bool bValidaActualizacionDet
        {
            get
            {
                bool bValida = true;
                ASPxDataValidationEventArgs eU = (ASPxDataValidationEventArgs)oIView.oCrud;
                bValida = (eU.NewValues["Semana"].S().I() != eU.OldValues["Semana"].S().I());
                return bValida;
            }
        }

        private bool bValidaActualizacion
        {
            get
            {
                bool bValida = true;
                ASPxDataValidationEventArgs eV = (ASPxDataValidationEventArgs)oIView.oCrud;
                bValida = ((eV.NewValues["Anio"].S().I() != eV.OldValues["Anio"].S().I()) || (eV.NewValues["IdMes"].S().I() !=  eV.OldValues["IdMes"].S().I()) || (eV.NewValues["IdAeropuerto"].S().I() != eV.OldValues["IdAeropuerto"].S().I()));
                return bValida;
            }
        }
    }
}