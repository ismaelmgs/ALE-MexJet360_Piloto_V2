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
    public class Contrato_Presenter : BasePresenter<IViewContrato>
    {
        private readonly DBContrato oIGestCat;

        public Contrato_Presenter(IViewContrato oView, DBContrato oGC)
            : base(oView)
        {
            oIGestCat = oGC;

            oIView.eGetServicioConCargo += GetServicios;
            oIView.eGetAeropuertoOrigen += GetOrigen;
            oIView.eGetAeropuertoOrigenFiltrado += GetOrigenFiltrado;
            oIView.eGetAeropuertoDestino += GetDestino;
            oIView.eGetAeropuertoDestinoFiltrado += GetDestinoFiltrado;
            oIView.eSearchModelos += SearchModelo_Presenter;

            oIView.eSaveGenerales += SaveGenerales;
            oIView.eSaveTarifa += SaveTarifa_Presenter;
            oIView.eSaveTramo += SaveTramoPactado_Presenter;
            oIView.eSaveCombustibleInternacional += SaveCombustible_Presenter;
            oIView.eSaveCobros += SaveCobros_Presenter;
            oIView.eSaveIntercambio += SaveIntercambio_Presenter;
            oIView.eSaveGiras += SaveGirasFechasPico_Presenter;
            oIView.eSaveRangos += SaveRango_Presenter;
            oIView.eSaveCaracteristicas += SaveCaracteristicas_Presenter;
            oIView.eSaveBases += SaveGeneralesContrato;

            oIView.eUpdateGenerales += UpdateGenerales;
            oIView.eUpdateTramo += UpdateTramoPactado_Presenter;
            oIView.eUpdateCobros += UpdateCobros_Presenter;
            oIView.eUpdateCombustibleInternacional += UpdateCombustible_Presenter;
            oIView.eUpdateIntercambio += UpdateIntercambio_Presenter;
            oIView.eUpdateGiras += UpdateGirasFechasPico_Presenter;
            oIView.eUpdateRangos += UpdateRango_Presenter;
            oIView.eUpdateCaracteristicas += UpdateCaracteristicas_Presenter;
            oIView.eUpdateTarifas += UpdateTarifa_Presenter;
            oIView.eUpdateBases += UpdateBases_Presenter;

            oIView.eValidaTramo += ValidateTramoPactado_Presenter;
            oIView.eValidaContrato += ValidaContrato_Presenter;
            oIView.eValidaCombustible += ValidaCombustible_Presenter;
            oIView.eValidaIntercambio += ValidateIntercambio_Presenter;
            oIView.eValidaRangos += ValidateRango_Presenter;

            oIView.eDeleteTramo += DeleteTramoPactado_Presenter;
            oIView.eDeleteIntercambio += DeleteIntercambio_Presenter;
            oIView.eDeleteCombustibleInternacional += DeleteCombustible_Presenter;
            oIView.eDeleteRangos += DeleteRango_Presenter;

            oIView.eGetContratoEdicion += searchContrato_Presenter;

            oIView.eSavePreferencias += SavePreferencias_Presenter;
            oIView.eUpdatePreferencias += UpdatePreferencias_Presenter;
            oIView.eGetDatosFacturacion += eGetDatosFacturacion_Presenter;
            oIView.eGetPermisosContrato += eGetPermisosContrato_Presenter;
            
        }


        protected void searchContrato_Presenter(object sender, EventArgs e)
        {
            try
            {
                oIView.objContratosGenerales = oIGestCat.DBGetContratoGenerales(oIView.iIdContrato);
                oIView.objContratosTarifas = oIGestCat.DBGetTarifa(oIView.iIdContrato);
                oIView.dtServicioConCargo = oIGestCat.dtObjsCatServicioConCargo;
                oIView.objCobrosDesc = oIGestCat.DBGetCobros(oIView.iIdContrato);
                oIView.objGirasFechas = oIGestCat.DBGetGiras(oIView.iIdContrato);
                oIView.objCaracteristicasEspeciales = oIGestCat.DBGetCaracteristicas(oIView.iIdContrato);
                oIView.objPreferencias = oIGestCat.DBGetPreferenciaContrato(oIView.iIdContrato);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void SearchModelo_Presenter(object sender, EventArgs e)
        {
            try
            {
                oIView.dtModelo = oIGestCat.DBGetGrupoModelo;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

        }

        protected override void SearchObj_Presenter(object sender, EventArgs e)
        {
            oIView.dtCliente = oIGestCat.dtObjsCatCliente;
            oIView.dtEjecutivo = oIGestCat.dtObjsCatEjecutivo;
            oIView.dtGrupoModelo = oIGestCat.dtObjsCatGrupoModelo;
            oIView.dtPaquete = oIGestCat.dtObjsCatPaquete;
            oIView.dtVendedor = oIGestCat.dtObjsCatVendedor;
            oIView.dtBase = oIGestCat.dtObjsCatBase;
            oIView.dtInflacion = oIGestCat.dtObjsCatInflacion;
            oIView.dtTarifaCombustible = oIGestCat.DBGetCombustibleInternacional(oIView.iIdContrato);
            oIView.dtTramosPactados = oIGestCat.DBGetTramoPactado(oIView.iIdContrato);
            oIView.dtIntercambios = oIGestCat.DBGetIntercambios(oIView.iIdContrato);
            oIView.dtMarca = oIGestCat.dtGetMarca;
            oIView.dtRangos = oIGestCat.DBGetRangos(oIView.iIdContrato);
            oIView.dtEstatusContratos = oIGestCat.DtEstatusContrato;
        }

        protected void SaveGenerales(object sender, EventArgs e)
        {
            try
            {
                Contrato_Generales objContratos = oIView.objContratosGenerales;

                objContratos.iId = oIGestCat.DBSaveGenerales(objContratos);
                if (objContratos.iId > 0)
                {
                    oIGestCat.DBSaveGeneralesBase(objContratos);
                }

                oIView.iIdContrato = objContratos.iId;
                oIView.objContratosGenerales = oIGestCat.DBGetContratoGenerales(oIView.iIdContrato);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

        }

        protected void SaveGeneralesContrato(object sender, EventArgs e)
        {
            try
            {
                Contrato_Generales obj = new Contrato_Generales();
                obj.lstBases.Add(oCatalogoBase);
                obj.iId = oIView.iIdContrato;
                oIGestCat.DBSaveGeneralesBase(obj);

                oIView.dtBasesSeleccionadas = oIGestCat.DBGetBases(oIView.iIdContrato);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

        }

        protected void SaveTarifa_Presenter(object sender, EventArgs e)
        {
            try
            {
                int idTarifa = oIGestCat.DBSaveTarifa(oIView.objContratosTarifas);
                oIView.dtTarifaCombustible = oIGestCat.DBGetCombustibleInternacional(oIView.iIdContrato);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void ValidaContrato_Presenter(object sender, EventArgs e)
        {
            try
            {
                int idTarifa = oIGestCat.DBValidaConsultaClave(oIView.objContratosGenerales);
                oIView.bDuplicaClaveContrato = idTarifa > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void ValidaCombustible_Presenter(object sender, EventArgs e)
        {
            try
            {
                int existe = 0;
                if (oIView.eCrud == Enumeraciones.TipoOperacion.Actualizar)
                {
                    if (bValidaActualizacionCombustible)
                    {
                        oIView.eCrud = Enumeraciones.TipoOperacion.Validar;
                        existe = oIGestCat.DBValidaCombustiInterna(oCatalogoCombustible);
                    }
                }
                else
                {
                    oIView.eCrud = Enumeraciones.TipoOperacion.Validar;
                    existe = oIGestCat.DBValidaCombustiInterna(oCatalogoCombustible);
                }
                oIView.bDuplicaCombustibleInternacional = existe > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void SaveCombustible_Presenter(object sender, EventArgs e)
        {
            try
            {
                int idTarifa = oIGestCat.DBSaveCombustible(oCatalogoCombustible);
                oIView.dtTarifaCombustible = oIGestCat.DBGetCombustibleInternacional(oIView.iIdContrato);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void SaveTramoPactado_Presenter(object sender, EventArgs e)
        {
            try
            {
                oIGestCat.DBSaveTramoPActado(oCatalogoTramo);
                SearchObj_Presenter(sender, e);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        protected void SaveCobros_Presenter(object sender, EventArgs e)
        {
            try
            {
                Contrato_CobrosDescuentos objContratos = oIView.objCobrosDesc;
                oIGestCat.DBSaveCobros(objContratos);
                if (objContratos.lstIdServiciosConCargo.Count > 0)
                {
                    oIGestCat.DBSaveServicioConCargo(objContratos);
                }
            }
            catch (Exception EX)
            {
                throw EX;
            }

        }

        protected void SaveIntercambio_Presenter(object sender, EventArgs e)
        {
            try
            {

                oIGestCat.DBSaveIntercambio(oCatalogoIntercambio);
                oIView.dtIntercambios = oIGestCat.DBGetIntercambios(oIView.iIdContrato);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        protected void SaveGirasFechasPico_Presenter(object sender, EventArgs e)
        {
            try
            {

                oIGestCat.DBSaveGiras(oIView.objGirasFechas);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        protected void SaveRango_Presenter(object sender, EventArgs e)
        {
            try
            {
                oIGestCat.DBSaveRangoCombustible(oCatalogoRangos);
                oIView.dtRangos = oIGestCat.DBGetRangos(oIView.iIdContrato);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        protected void SaveCaracteristicas_Presenter(object sender, EventArgs e)
        {
            try
            {

                oIGestCat.DBSaveCaracteristicas(oIView.objCaracteristicasEspeciales);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        protected void UpdateGenerales(object sender, EventArgs e)
        {
            Contrato_Generales objContratos = oIView.objContratosGenerales;
            objContratos.iIdContrato = oIView.iIdContrato;
            oIGestCat.DBUpdateGeneral(objContratos);
            //if (objContratos.iId > 0)
            //{
            //    oIGestCat.DBSaveGeneralesBase(objContratos);
            //}
        }

        protected void UpdateTarifa_Presenter(object sender, EventArgs e)
        {
            try
            {
                int idTarifa = oIGestCat.DBUpdateTarifasContrato(oIView.objContratosTarifas);
                oIView.dtTarifaCombustible = oIGestCat.DBGetCombustibleInternacional(oIView.iIdContrato);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void UpdateCobros_Presenter(object sender, EventArgs e)
        {
            try
            {
                Contrato_CobrosDescuentos obj = oIView.objCobrosDesc;
                int idTarifa = oIGestCat.DBUpdateCobros(obj);
                int idx = oIGestCat.DBUpdateServicios(obj);
                oIView.dtTarifaCombustible = oIGestCat.DBGetCombustibleInternacional(oIView.iIdContrato);
                oIView.objCobrosDesc = oIGestCat.DBGetCobros(oIView.iIdContrato);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void UpdateCombustible_Presenter(object sender, EventArgs e)
        {
            try
            {
                int idTarifa = oIGestCat.DBUpdateCombustible(oCatalogoCombustible);
                oIView.dtTarifaCombustible = oIGestCat.DBGetCombustibleInternacional(oIView.iIdContrato);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void UpdateTramoPactado_Presenter(object sender, EventArgs e)
        {
            try
            {
                oIGestCat.DBUpdateTramosEspeciales(oCatalogoTramo);

                SearchObj_Presenter(sender, e);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        protected void UpdateIntercambio_Presenter(object sender, EventArgs e)
        {
            try
            {

                oIGestCat.DBUpdateIntercambioContratos(oCatalogoIntercambio);
                oIView.dtIntercambios = oIGestCat.DBGetIntercambios(oIView.iIdContrato);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        protected void UpdateGirasFechasPico_Presenter(object sender, EventArgs e)
        {
            try
            {

                oIGestCat.DBUpdateGiras(oIView.objGirasFechas);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        protected void UpdateRango_Presenter(object sender, EventArgs e)
        {
            try
            {

                oIGestCat.DBUpdateRangoCombustible(oCatalogoRangos);
                oIView.dtRangos = oIGestCat.DBGetRangos(oIView.iIdContrato);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        protected void UpdateCaracteristicas_Presenter(object sender, EventArgs e)
        {
            try
            {
                int iExiste = oIGestCat.DBExisteCaracteristicas(oIView.iIdContrato);
                if (iExiste > 0)
                    oIGestCat.DBUpdateEspeciales(oIView.objCaracteristicasEspeciales);
                else
                    oIGestCat.DBSaveCaracteristicas(oIView.objCaracteristicasEspeciales);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        protected void UpdateBases_Presenter(object sender, EventArgs e)
        {
            try
            {
                Contrato_Generales obj = new Contrato_Generales();
                obj.lstBases.Add(oCatalogoBase);

                oIGestCat.DBUpdateGeneralesBase(obj);
                oIView.dtBasesSeleccionadas = oIGestCat.DBGetBases(oIView.iIdContrato);

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        protected void ValidateCombustible_Presenter(object sender, EventArgs e)
        {
            try
            {
                int idTarifa = oIGestCat.DBSaveTarifa(oIView.objContratosTarifas);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void ValidateTramoPactado_Presenter(object sender, EventArgs e)
        {
            try
            {
                int existe = 0;
                if (oIView.eCrud == Enumeraciones.TipoOperacion.Actualizar)
                {
                    if (bValidaActualizacionTramo)
                    {
                        oIView.eCrud = Enumeraciones.TipoOperacion.Validar;
                        existe = oIGestCat.DBValidaTramoEspecial(oCatalogoTramo);
                    }
                }
                else
                {
                    oIView.eCrud = Enumeraciones.TipoOperacion.Validar;
                    existe = oIGestCat.DBValidaTramoEspecial(oCatalogoTramo);
                }
                oIView.bDuplicaTramo = existe > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ValidateIntercambio_Presenter(object sender, EventArgs e)
        {
            try
            {
                int iExiste = 0;
                oIView.eCrud = Enumeraciones.TipoOperacion.Validar;
                iExiste = oIGestCat.DBValidaIntercambio(oCatalogoIntercambio);
                oIView.bDuplicaIntercambio = iExiste > 0;

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        protected void ValidateRango_Presenter(object sender, EventArgs e)
        {
            try
            {
                int iExiste = -1;
                if (oIView.eCrud == Enumeraciones.TipoOperacion.Actualizar)
                {
                    oIView.eCrud = Enumeraciones.TipoOperacion.Validar;
                    bool bDesde = BValidaDesde;
                    bool bHasta = bValidaHasta;
                    iExiste = oIGestCat.DBValidaRangoCombustibleUpdate(oCatalogoRangos, bDesde, bHasta);
                }
                else
                {
                    oIView.eCrud = Enumeraciones.TipoOperacion.Validar;
                    iExiste = oIGestCat.DBValidaRangoCobustible(oCatalogoRangos);
                }
                oIView.bduplicaRango = iExiste > 0;

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        protected void ValidateContrato_Presenter(object sender, EventArgs e)
        {
            try
            {
                int idTarifa = oIGestCat.DBSaveTarifa(oIView.objContratosTarifas);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        protected void DeleteCombustible_Presenter(object sender, EventArgs e)
        {
            try
            {
                int idTarifa = oIGestCat.DBDeleteCombusInternacional(oCatalogoCombustible);
                oIView.dtTarifaCombustible = oIGestCat.DBGetCombustibleInternacional(oIView.iIdContrato);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void DeleteTramoPactado_Presenter(object sender, EventArgs e)
        {
            try
            {

                oIGestCat.DBDeleteTramoEspecial(oCatalogoTramo);
                oIView.dtTramosPactados = oIGestCat.DBGetTramoPactado(oIView.iIdContrato);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        protected void DeleteIntercambio_Presenter(object sender, EventArgs e)
        {
            try
            {

                oIGestCat.DBDeleteIntercambio(oCatalogoIntercambio);
                oIView.dtIntercambios = oIGestCat.DBGetIntercambios(oIView.iIdContrato);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        protected void DeleteRango_Presenter(object sender, EventArgs e)
        {
            try
            {
                oIGestCat.DBDeleteRangoCombustible(oCatalogoRangos);
                oIView.dtRangos = oIGestCat.DBGetRangos(oIView.iIdContrato);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        protected void SavePreferencias_Presenter(object sender, EventArgs e)
        {
            try
            {
                oIGestCat.DBSavePreferenciaContrato(oIView.objPreferencias);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void UpdatePreferencias_Presenter(object sender, EventArgs e)
        {
            try
            {
                oIGestCat.DBUpdatePreferenciaContrato(oIView.objPreferencias);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected override void DeleteObj_Presenter(object sender, EventArgs e)
        {
            try
            {
                Contrato_Generales obj = new Contrato_Generales();
                obj.lstBases.Add(oCatalogoBase);

                int id = oIGestCat.DBDeleteBase(obj);
                if (id > 0)
                {
                    oIView.MostrarMensaje(Mensajes.LMensajes[Enumeraciones.Mensajes.Eliminacion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
                    oIView.ObtieneValores();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void eGetDatosFacturacion_Presenter(object sender, EventArgs e)
        {
            oIView.dsCatFacturacion = oIGestCat.DBGetObtieneInformacionFacturacion;
            oIView.LlenaControlesFacturacion();
        }

        protected void GetServicios(object sender, EventArgs e)
        {
            try
            {
                oIView.dtServicioConCargo = oIGestCat.dtObjsCatServicioConCargo;
            }
            catch (Exception Ex)
            {
            }

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
            oIView.dtTarifaDestino = oIGestCat.DBCargaDestino(oIGestCat.DbSearchIdAeropuertoIata(oIView.sOrigen));
        }

        protected void GetDestinoFiltrado(object sender, EventArgs e)
        {
            oIView.dtTarifaDestino = oIGestCat.DBFiltraAeropuertoIATADestino(oIView.sFiltroAeropuerto, oIGestCat.DbSearchIdAeropuertoIata(oIView.sOrigen));
        }

        private Contrato_TramoPactado oCatalogoTramo
        {
            get
            {
                Contrato_TramoPactado oContrato = new Contrato_TramoPactado();
                switch (oIView.eCrud)
                {
                    case Enumeraciones.TipoOperacion.Insertar:
                        ASPxDataInsertingEventArgs eI = (ASPxDataInsertingEventArgs)oIView.oCrudTamoPactado;
                        oContrato.iIdContrato = oIView.iIdContrato;
                        oContrato.iIdOrigen = oIGestCat.DbSearchIdAeropuertoIata(eI.NewValues["Origen"].S());
                        oContrato.iIdDestino = oIGestCat.DbSearchIdAeropuertoIata(eI.NewValues["Destino"].S());
                        oContrato.sTiempoVuelo = eI.NewValues["TiempoVuelo"].S();
                        break;
                    case Enumeraciones.TipoOperacion.Actualizar:
                        ASPxDataUpdatingEventArgs eU = (ASPxDataUpdatingEventArgs)oIView.oCrudTamoPactado;
                        oContrato.iIdContrato = oIView.iIdContrato;
                        oContrato.iId = eU.Keys[0].S().I();
                        oContrato.iIdOrigen = oIGestCat.DbSearchIdAeropuertoIata(eU.NewValues["Origen"].S());
                        oContrato.iIdDestino = oIGestCat.DbSearchIdAeropuertoIata(eU.NewValues["Destino"].S());
                        oContrato.sTiempoVuelo = eU.NewValues["TiempoVuelo"].S();
                        break;
                    case Enumeraciones.TipoOperacion.Validar:
                        ASPxDataValidationEventArgs eV = (ASPxDataValidationEventArgs)oIView.oCrudTamoPactado;
                        oContrato.iIdContrato = oIView.iIdContrato;
                        oContrato.iIdOrigen = oIGestCat.DbSearchIdAeropuertoIata(eV.NewValues["Origen"].S());
                        oContrato.iIdDestino = oIGestCat.DbSearchIdAeropuertoIata(eV.NewValues["Destino"].S());
                        oContrato.sTiempoVuelo = eV.NewValues["TiempoVuelo"].S();

                        break;
                    case Enumeraciones.TipoOperacion.Eliminar:
                        ASPxDataDeletingEventArgs eD = (ASPxDataDeletingEventArgs)oIView.oCrudTamoPactado;
                        oContrato.iId = eD.Keys[0].S().I();
                        break;
                }
                oContrato.iIdContrato = oIView.iIdContrato;
                return oContrato;
            }
        }

        private Contrato_CombustibleInternacional oCatalogoCombustible
        {
            get
            {
                Contrato_CombustibleInternacional oContrato = new Contrato_CombustibleInternacional();
                switch (oIView.eCrud)
                {
                    case Enumeraciones.TipoOperacion.Insertar:
                        ASPxDataInsertingEventArgs eI = (ASPxDataInsertingEventArgs)oIView.oCrudCombustible;
                        oContrato.dtFecha = eI.NewValues["Fecha"].S().Dt();
                        oContrato.dImport = eI.NewValues["Importe"].S().D();
                        break;
                    case Enumeraciones.TipoOperacion.Actualizar:
                        ASPxDataUpdatingEventArgs eU = (ASPxDataUpdatingEventArgs)oIView.oCrudCombustible;
                        oContrato.iId = eU.Keys[0].S().I();
                        oContrato.dtFecha = eU.NewValues["Fecha"].S().Dt(); ;
                        oContrato.dImport = eU.NewValues["Importe"].S().D();
                        break;
                    case Enumeraciones.TipoOperacion.Validar:
                        ASPxDataValidationEventArgs eV = (ASPxDataValidationEventArgs)oIView.oCrudCombustible;
                        if (eV.Keys.Count > 0)
                            oContrato.iId = eV.Keys[0].S().I();
                        oContrato.dtFecha = eV.NewValues["Fecha"].S().Dt();
                        oContrato.dImport = eV.NewValues["Importe"].S().D();

                        break;
                    case Enumeraciones.TipoOperacion.Eliminar:
                        ASPxDataDeletingEventArgs eD = (ASPxDataDeletingEventArgs)oIView.oCrudCombustible;
                        oContrato.iId = eD.Keys[0].S().I();
                        break;
                }
                oContrato.iIdContrato = oIView.iIdContrato;
                return oContrato;
            }
        }

        private Contrato_Intercambios oCatalogoIntercambio
        {
            get
            {

                Contrato_Intercambios oContrato = new Contrato_Intercambios();
                switch (oIView.eCrud)
                {
                    case Enumeraciones.TipoOperacion.Insertar:
                        ASPxDataInsertingEventArgs eI = (ASPxDataInsertingEventArgs)oIView.oCrudIntercambio;
                        oContrato.iIdGrupoModelo = eI.NewValues["IdGrupoModelo"].S().I();
                        oContrato.dFactor = eI.NewValues["Factor"].S().D();
                        oContrato.bAplicaFerry = eI.NewValues["Ferry"].S().B();
                        oContrato.dTarifaNalDlls = eI.NewValues["TarifaNal"].S().D();
                        oContrato.dTarifaIntDlls = eI.NewValues["TarifaInt"].S().D();
                        oContrato.dGalones = eI.NewValues["Galones"].S().D();
                        oContrato.dCDN = eI.NewValues["CDN"].S().D();
                        oContrato.dCDI = eI.NewValues["CDI"].S().D();
                        break;
                    case Enumeraciones.TipoOperacion.Actualizar:
                        ASPxDataUpdatingEventArgs eU = (ASPxDataUpdatingEventArgs)oIView.oCrudIntercambio;
                        oContrato.iId = eU.Keys[0].S().I();
                        oContrato.iIdGrupoModelo = eU.NewValues["IdGrupoModelo"].S().I();
                        oContrato.dFactor = eU.NewValues["Factor"].S().D();
                        oContrato.bAplicaFerry = eU.NewValues["Ferry"].S().B();
                        oContrato.dTarifaNalDlls = eU.NewValues["TarifaNal"].S().D();
                        oContrato.dTarifaIntDlls = eU.NewValues["TarifaInt"].S().D();
                        oContrato.dGalones = eU.NewValues["Galones"].S().D();
                        oContrato.dCDN = eU.NewValues["CDN"].S().D();
                        oContrato.dCDI = eU.NewValues["CDI"].S().D();
                        break;
                    case Enumeraciones.TipoOperacion.Validar:
                        ASPxDataValidationEventArgs eV = (ASPxDataValidationEventArgs)oIView.oCrudIntercambio;
                        if (eV.Keys.Count > 0)
                            oContrato.iId = eV.Keys[0].S().I();
                        oContrato.iIdContrato = oIView.iIdContrato;
                        oContrato.iIdGrupoModelo = eV.NewValues["IdGrupoModelo"].S().I();

                        break;
                    case Enumeraciones.TipoOperacion.Eliminar:
                        ASPxDataDeletingEventArgs eD = (ASPxDataDeletingEventArgs)oIView.oCrudIntercambio;
                        oContrato.iId = eD.Keys[0].S().I();
                        break;
                }
                oContrato.iIdContrato = oIView.iIdContrato;
                return oContrato;
            }
        }

        private Contrato_RangoCombustible oCatalogoRangos
        {
            get
            {
                Contrato_RangoCombustible oContrato = new Contrato_RangoCombustible();
                switch (oIView.eCrud)
                {
                    case Enumeraciones.TipoOperacion.Insertar:
                        ASPxDataInsertingEventArgs eI = (ASPxDataInsertingEventArgs)oIView.oCrudRangos;
                        oContrato.iIdGrupoModelo = eI.NewValues["IdGrupoModelo"].S().I();
                        oContrato.iIdTipo = eI.NewValues["idTipo"].S().I();
                        oContrato.dDesde = eI.NewValues["Desde"].S().D();
                        oContrato.dHasta = eI.NewValues["Hasta"].S().D();
                        oContrato.dIncremento = eI.NewValues["Incremento"].S().D();

                        break;
                    case Enumeraciones.TipoOperacion.Actualizar:
                        ASPxDataUpdatingEventArgs eU = (ASPxDataUpdatingEventArgs)oIView.oCrudRangos;
                        oContrato.iId = eU.Keys[0].S().I();
                        oContrato.iIdGrupoModelo = eU.NewValues["IdGrupoModelo"].S().I();
                        oContrato.iIdTipo = eU.NewValues["idTipo"].S().I();
                        oContrato.dDesde = eU.NewValues["Desde"].S().D();
                        oContrato.dHasta = eU.NewValues["Hasta"].S().D();
                        oContrato.dIncremento = eU.NewValues["Incremento"].S().D();
                        break;
                    case Enumeraciones.TipoOperacion.Validar:
                        ASPxDataValidationEventArgs eV = (ASPxDataValidationEventArgs)oIView.oCrudRangos;
                        if (eV.Keys.Count > 0)
                            oContrato.iId = eV.Keys[0].S().I();
                        oContrato.iIdGrupoModelo = eV.NewValues["IdGrupoModelo"].S().I();
                        oContrato.iIdTipo = eV.NewValues["idTipo"].S().I();
                        oContrato.dDesde = eV.NewValues["Desde"].S().D();
                        oContrato.dHasta = eV.NewValues["Hasta"].S().D();
                        oContrato.dIncremento = eV.NewValues["Incremento"].S().D();

                        break;
                    case Enumeraciones.TipoOperacion.Eliminar:
                        ASPxDataDeletingEventArgs eD = (ASPxDataDeletingEventArgs)oIView.oCrudIntercambio;
                        oContrato.iId = eD.Keys[0].S().I();
                        break;
                }
                oContrato.iIdContrato = oIView.iIdContrato;
                return oContrato;
            }
        }

        private Contratos_Bases oCatalogoBase
        {
            get
            {
                Contratos_Bases oContrato = new Contratos_Bases();
                switch (oIView.eCrud)
                {
                    case Enumeraciones.TipoOperacion.Insertar:
                        ASPxDataInsertingEventArgs eI = (ASPxDataInsertingEventArgs)oIView.oCrudBases;
                        oContrato.sAeropuerto = eI.NewValues["Aeropuerto"].S();
                        oContrato.iPredeterminada = eI.NewValues["TipoBase"].S().I();
                        break;
                    case Enumeraciones.TipoOperacion.Actualizar:
                        ASPxDataUpdatingEventArgs eU = (ASPxDataUpdatingEventArgs)oIView.oCrudBases;
                        oContrato.iId = eU.Keys[0].S().I();
                        oContrato.sAeropuerto = eU.NewValues["Aeropuerto"].S();
                        oContrato.iPredeterminada = eU.NewValues["TipoBase"].S().I();
                        break;
                    case Enumeraciones.TipoOperacion.Eliminar:
                        ASPxDataDeletingEventArgs eD = (ASPxDataDeletingEventArgs)oIView.oCrudBases;
                        oContrato.iId = eD.Keys[0].S().I();
                        break;
                }
                oContrato.iIdContrato = oIView.iIdContrato;
                return oContrato;
            }
        }

        protected void eGetPermisosContrato_Presenter(object sender, EventArgs e)
        {
            DataTable dt = new DBPermisosContrato().DBGetObtienePermisosContratoPorRol(Utils.GetRolUser);
            oIView.HabilitaPermisosContrato(dt);
        }       


        private bool bValidaHasta
        {
            get
            {
                bool bValida = true;
                ASPxDataValidationEventArgs eU = (ASPxDataValidationEventArgs)oIView.oCrudRangos;

                bValida = (eU.NewValues["Hasta"].S().ToUpper() != eU.OldValues["Hasta"].S().ToUpper());

                return bValida;
            }
        }

        private bool BValidaDesde
        {
            get
            {
                bool bValida = true;
                ASPxDataValidationEventArgs eU = (ASPxDataValidationEventArgs)oIView.oCrudRangos;

                bValida = (eU.NewValues["Desde"].S().ToUpper() != eU.OldValues["Desde"].S().ToUpper());

                return bValida;
            }
        }
        private bool bValidaActualizacionCombustible
        {
            get
            {
                bool bValida = true;
                ASPxDataValidationEventArgs eU = (ASPxDataValidationEventArgs)oIView.oCrudCombustible;

                bValida = eU.NewValues["Fecha"].S().Dt() != eU.OldValues["Fecha"].S().Dt();

                return bValida;
            }
        }

        private bool bValidaActualizacionTramo
        {
            get
            {
                bool bValida = true;
                ASPxDataValidationEventArgs eU = (ASPxDataValidationEventArgs)oIView.oCrudTamoPactado;

                bValida = ((eU.NewValues["Origen"].S().ToUpper() != eU.OldValues["Origen"].S().ToUpper()) || (eU.NewValues["Destino"].S().ToUpper() != eU.OldValues["Destino"].S().ToUpper()));

                return bValida;
            }
        }
    }
}