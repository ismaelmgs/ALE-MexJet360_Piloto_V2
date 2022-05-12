using ALE_MexJet.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using ALE_MexJet.Clases;
using NucleoBase.Core;

namespace ALE_MexJet.DomainModel
{
    public class DBContrato : DBBase
    {
        public DataTable dtObjsCatCliente
        {
            get
            {
                try
                {
                    return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaCliente]", "@CodigoCliente", "%%",
                                                                                    "@Nombre", "%%",
                                                                                    "@TipoCliente", "%%",
                                                                                    "@estatus", -1);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable dtObjsCatVendedor
        {
            get
            {
                try
                {
                    return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaCatalogoVendedor]");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable dtObjsCatBase
        {
            get
            {
                try
                {
                    return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaAeropuertoBase]");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable dtObjsCatInflacion
        {
            get
            {
                try
                {
                    return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaCatalogoInflacion]");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable dtObjsCatServicioConCargo
        {
            get
            {
                try
                {
                    return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaCatalogoServicioConCargo]");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable dtObjsCatEjecutivo
        {
            get
            {
                try
                {
                    return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaCatalogoEjecutivo]");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable dtObjsCatPaquete
        {
            get
            {
                try
                {
                    return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaPaquete]", "@Descripcion","%%",
                                                                                       "@estatus",-1);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable dtGetMarca
        {
            get
            {
                try
                {
                    return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaCatalogoMarcas]");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable DBGetGrupoModelo
        {
            get
            {
                try
                {
                    return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaCatalogoGrupoModelo]");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable dtObjsCatGrupoModelo
        {
            get
            {
                try
                {
                    return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaCatalogoGrupoModelo]");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable DBSearchObj(params object[] oArrFiltros)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaRol]", oArrFiltros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable DBGetCombustibleInternacional (int iIdContrato)
        {
            try
            {
               
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaCombustibleInternacionalContrato]", "@IdContrato", iIdContrato);
            }
            catch(Exception Ex)
            {
                throw Ex;
            }
        }

        public DataTable DBGetTramoPactado(int iIdContrato)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaTramosEspecialesContrato]", "@IdContrato", iIdContrato);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public DataTable DBGetIntercambios  (int iIdContrato)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaIntercambioContrato]", "@IdContrato", iIdContrato);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public DataTable DBGetRangos(int iIdContrato)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaRangosCombustibleContrato]", "@IdContrato", iIdContrato);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public DataTable DBGetBases(int iIdContrato)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaBasesContrato]", "@IdContrato", iIdContrato);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet DBGetObtieneInformacionFacturacion
        {
            get 
            {
                try
                {
                    return oDB_SP.EjecutarDS("[Catalogos].[spS_MXJ_ObtieneCatsFacturacion]");
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public int DBSaveRangoCombustible(Contrato_RangoCombustible oRango)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Principales].[spI_MXJ_InsertaRangosCombustibleContrato]",   "@IdContrato", oRango.iIdContrato,
                                                                                                             "@IdGrupoModelo", oRango.iIdGrupoModelo,
                                                                                                             "@IdTipo", oRango.iIdTipo,
                                                                                                             "@Incremento", oRango.dIncremento,
                                                                                                             "@Desde", oRango.dDesde,
                                                                                                             "@Hasta", oRango.dHasta,
                                                                                                             "@Status", 1,
                                                                                                             "@UsuarioCreacion", Utils.GetUser,
                                                                                                             "@IP", null);

                new DBUtils().DBSaveBitacoraModulos(Convert.ToInt32(Enumeraciones.Pantallas.Contrato), Convert.ToInt32(Enumeraciones.TipoAccion.Insertar), "Se creó rango de combustible del contrato: " + oRango.iIdContrato.S());
                return oResult.I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DbSearchIdAeropuertoIata(string sIATA)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spS_MXJ_ConsultaIdAeropuertoIata]", "@IATA", sIATA);
                return oResult.S().I();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public DataTable dtObjsOrigen
        {
            get
            {
                try
                {
                    return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaOrigenAeropuertoContrato]");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable DBCargaDestino(int idOrigen)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaDestinoAeropuertoContrato]", "@IdOrigen", idOrigen);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable DBFiltraAeropuertoIATA(string letra)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaCatalogoAeropuertoIATA]", "@IATA", letra + "%");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable DBFiltraAeropuertoIATADestino(string letra, int iOrigen)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaCatalogoAeropuertoIATADestino]", "@IATA", letra + "%",
                                                                                                         "@IdOrigen", iOrigen);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBSaveGenerales(Contrato_Generales oContrato)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Principales].[spI_MXJ_InsertaContrato]","@IdCliente", oContrato.iIdCliente,
                                                                                        "@ClaveContrato",oContrato.sContrato,
                                                                                        "@FechaContrato",oContrato.dtFechaContrato,
                                                                                        "@IdVendedor",oContrato.iiDVendedor,
                                                                                        "@IdEjecutivo",oContrato.iIdEjecutivo,
                                                                                        "@FechaInicioVuelo",oContrato.dtFechaInicioVuelo,
                                                                                        "@IdPaquete",oContrato.iIdPAquete,
                                                                                        "@IdGrupoModelo",oContrato.iIdGrupoModelo,
                                                                                        "@AniosContratados",oContrato.iAñoContratados,
                                                                                        "@MesesGracia",oContrato.iMesesGracia,
                                                                                        "@HorasContratadasTotal",oContrato.iHorasContratadasTotal,
                                                                                        "@HorasContratadasAnio",oContrato.iHorasContratadasAño,
                                                                                        "@PorcentajeHorasNoUsadas",oContrato.dHorasNoUsadasAcumulables,
                                                                                        "@Matricula",oContrato.sMatricula,
                                                                                        "@TipoCambio",oContrato.iIdTipoCambio,
                                                                                        "@AnticipoInicial",oContrato.dAnticipioInicial,
                                                                                        "@FijoAnual",oContrato.dFijoAnual,
                                                                                        "@Renovacion",oContrato.dRenovacion,
                                                                                        "@Prenda",oContrato.dPrenda,
                                                                                        "@IncrementoCostoDirRen",oContrato.dIncrementoCostoDirectoRenovacion,
                                                                                        "@ReasignaRemisiones",oContrato.bReasigna,
                                                                                        "@Notas",oContrato.sNotas,
                                                                                        "@MetodoPagoFact", oContrato.sMetodoPagoFact,
                                                                                        "@FormaPagoFact", oContrato.sFormaPago,
                                                                                        "@UsoCFDIFact", oContrato.sUsoCFDI,
                                                                                        "@FormatoEdoCta", oContrato.sFormatoEdoCta,
                                                                                        "@Status", oContrato.iStatus,
                                                                                        "@IP", oContrato.sIP,
                                                                                        "@NotasIntercambio", oContrato.sNotasIntercambios,
                                                                                        "@NotasRangosCombutible", oContrato.sNotasRangoCombustible,
                                                                                        "@Usuario", Utils.GetUser);

                

                new DBUtils().DBSaveBitacoraModulos(Convert.ToInt32(Enumeraciones.Pantallas.Contrato), Convert.ToInt32(Enumeraciones.TipoAccion.Insertar), "Se creó el contrato: " + oResult.S());

                if(oResult != null)
                {
                    bool ban = DBSaveDigitalContract(oResult.S().I(), oContrato.bContratoD, oContrato.sNombreArchivo);
                }

                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DBSaveDigitalContract(int iIdContrato, byte[] bArchivo, string sNombreArchivo)
        {
            try
            {
                object oResult = oDB_SP.EjecutarValor("[Principales].[spI_MXJ_InsertaContratoDigital]", "@IdContrato", iIdContrato,
                                                                                                        "@Contrato", bArchivo,
                                                                                                        "@NombreArchivo", sNombreArchivo);

                return oResult != null ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBSaveGeneralesBase(Contrato_Generales oContrato)
        {
            try
            {
                object oResult;
                int iCantidadBases = 0;
                foreach( Contratos_Bases objBases in oContrato.lstBases)
                {
                    iCantidadBases += 1;
                    oResult = oDB_SP.EjecutarValor("[Principales].[spI_MXJ_InsertaBasesContrato]", "@IdContrato", oContrato.iId,
                                                                                                    "@IdAeropuerto", DbSearchIdAeropuertoIata(objBases.sAeropuerto),
                                                                                                    "@IdTipoBase", objBases.iPredeterminada,
                                                                                                    "@Status", oContrato.iStatus,
                                                                                                    "@IP", oContrato.sIP,
                                                                                                    "@Usuario", Utils.GetUser);

                    new DBUtils().DBSaveBitacoraModulos(Convert.ToInt32(Enumeraciones.Pantallas.Contrato), Convert.ToInt32(Enumeraciones.TipoAccion.Insertar), "Se creó base del contrato: " + oContrato.iId.S());
                }


                return iCantidadBases;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBSaveTarifa(Contrato_Tarifas objTarifa)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Principales].[spI_MXJ_InsertaTarifasContrato]", "@IdContrato", objTarifa.iIdContrato,
                                                                                               "@CostoDirectoNalV", objTarifa.dCostoDirNal,
                                                                                                "@CostoDirectoIntV", objTarifa.dCostoDirInt,
                                                                                                "@SeCobraCombustibleV", objTarifa.bCombustible,
                                                                                                "@CalculoPrecioCombuV", objTarifa.iTipoCalculo,
                                                                                                "@ConsumoGalonesHrV", objTarifa.dConsumoGalones,
                                                                                                "@FactorTramosNal", objTarifa.dFactorTramosNal,
                                                                                                "@FactorTramosInt", objTarifa.dFactorTramosInt,
                                                                                                "@AplicaFactorCombustible", objTarifa.bAplicaFactorCombustible == true ? 1 : 0,
                                                                                                "@CombustibleIntEspV", objTarifa.bPrecioInternacionalEspecial,
                                                                                                "@SeCobraTE", objTarifa.bCobraTiempoEspera,
                                                                                                "@TarifaNacionalTE", objTarifa.dTiempoEsperaFijaNal,
                                                                                                "@TarifaInteracionalTE", objTarifa.dTiempoEsperaFijaInt,
                                                                                                "@PorcentajeTarifaTE", objTarifa.dTiempoEsperaVarNal,
                                                                                                "@PorcentajeInternacionalTE", objTarifa.dTiempoEsperaVarInt,
                                                                                                "@SeCobraPernoctaP", objTarifa.bCobraPernoctas,
                                                                                                "@TarifaNacionalP", objTarifa.dPernoctasFijaNal,
                                                                                                "@TarifaInternacionalP", objTarifa.dPernoctasFijaInt,
                                                                                                "@PorcentajeTarifaP", objTarifa.dPernoctasVarNal,
                                                                                                "@PorcentajeInternacionalP", objTarifa.dPernoctasVarInt,
                                                                                                "@CostoDirectoNalInflacion", objTarifa.iCDNBaseInflacion,
                                                                                                "@CostoDirectoIntInflacion", objTarifa.iCDIBaseInflacion,
                                                                                                "@FijoAnualInflacion", objTarifa.iFABaseInflacion,
                                                                                                "@CostoDirectoNalPorcentaje", objTarifa.dCDNPorcentaje,
                                                                                                "@CostoDirectoIntPorcentaje", objTarifa.dCDIPorcentaje,
                                                                                                "@FijoAnualPorcentaje", objTarifa.dFAPorcentaje,
                                                                                                "@CostoDirectoNalMasPuntos", objTarifa.dCDNPuntos,
                                                                                                "@CostoDirectoIntMasPuntos", objTarifa.dCDIPuntos,
                                                                                                "@FijoAnualMasPuntos", objTarifa.dFAPuntos,
                                                                                                "@CostoDirectoNalTope", objTarifa.dCDNTopeMAximo,
                                                                                                "@CostoDirectoIntTope", objTarifa.dCDITopeMAximo,
                                                                                                "@FijoAnualTope", objTarifa.dFATopeMAximo,
                                                                                                "@Notas", objTarifa.sNotas,
                                                                                                "@AplicaIncremento", objTarifa.iAplicaIncremento,
                                                                                                "@Status", 1,
                                                                                                "@UsuarioCreacion", Utils.GetUser,
                                                                                                "@IP", string.Empty);

                new DBUtils().DBSaveBitacoraModulos(Convert.ToInt32(Enumeraciones.Pantallas.Contrato), Convert.ToInt32(Enumeraciones.TipoAccion.Insertar), "Se creó tarifa del contrato: " + objTarifa.iIdContrato.S());
                return oResult.I();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public int DBSaveCombustible(Contrato_CombustibleInternacional objCombustible )
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Principales].[spI_MXJ_InsertaCombustibleInternacionalContrato]", "@IdContrato", objCombustible.iIdContrato,
                                                                                                                  "@Fecha", objCombustible.dtFecha,
                                                                                                                  "@Importe",objCombustible.dImport,
                                                                                                                  "@Status", 1,
                                                                                                                  "@UsuarioCreacion", Utils.GetUser,
                                                                                                                  "@IP", string.Empty);

                new DBUtils().DBSaveBitacora(Convert.ToInt32(Enumeraciones.Pantallas.Contrato), Convert.ToInt32(Enumeraciones.TipoOperacion.Insertar), "Se creó Combustible Internacional del contrato: " + objCombustible.iIdContrato.S());
                return oResult.I();
            }
            catch(Exception Ex)
            {
                throw Ex;
            }

        }

        public int DBSaveGiras(Contrato_GirasFechasPico objGiras)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Principales].[spI_MXJ_InsertaGirasFechasPicoContrato]", "@IdContrato", objGiras.iIdContrato,
                                                                                                            "@GiraEspera", objGiras.bAplicaGiraEspera,
                                                                                                            "@TiempoVuelo", objGiras.iNumeroVeces,
                                                                                                            "@GiraHora", objGiras.bAplicaGiraHora,
                                                                                                            "@HoraInicio", objGiras.sHoraInicio,
                                                                                                            "@HoraFin", objGiras.sHoraFin,
                                                                                                            "@PorcentajeDescuentoHoras", objGiras.dPorcentajeDescuento,
                                                                                                            "@FechaPico", objGiras.bAplicaFactorFechaPico,
                                                                                                            "@FactorFechaPico", objGiras.dPorcentajeDescuento,
                                                                                                            "@Notas", objGiras.sNotas,
                                                                                                            "@Status", 1,
                                                                                                            "@UsuarioCreacion", Utils.GetUser,
                                                                                                            "@IP", string.Empty);

                new DBUtils().DBSaveBitacoraModulos(Convert.ToInt32(Enumeraciones.Pantallas.Contrato), Convert.ToInt32(Enumeraciones.TipoAccion.Insertar), "Se creó giras fechas pico del contrato: " + objGiras.iIdContrato.S());
                return oResult.I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBSaveTramoPActado(Contrato_TramoPactado objTramo)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Principales].[spI_MXJ_InsertaTramosEspecialesContrato]", "@IdContrato", objTramo.iIdContrato,
                                                                                                           "@IdAeropuertoO",objTramo.iIdOrigen,
                                                                                                           "@IdAeropuertoD", objTramo.iIdDestino,
                                                                                                           "@TiempoVuelo", objTramo.sTiempoVuelo,
                                                                                                            "@Status", 1,
                                                                                                            "@UsuarioCreacion", Utils.GetUser,
                                                                                                            "@IP", string.Empty);

                new DBUtils().DBSaveBitacora(Convert.ToInt32(Enumeraciones.Pantallas.Contrato), Convert.ToInt32(Enumeraciones.TipoOperacion.Insertar), "Se creó tramo especial del contrato: " + objTramo.iIdContrato.S());
                return oResult.I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBSaveCobros(Contrato_CobrosDescuentos objCobros)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Principales].[spI_MXJ_InsertaCobrosDescuentosContrato]",   "@IdContrato",objCobros.iIdContrato,
                                                                                                            "@IdFerryConCargo", objCobros.iFerrysConCargo,
                                                                                                            "@EsperalLibre", objCobros.bAplicaEsperaLibre == true ? 1:0,
                                                                                                            "@HorasPorVuelo", objCobros.dHorasVuelo,
                                                                                                            "@FactorHrVuelo", objCobros.dFactorHorasVuelo,
                                                                                                            "@SeDescuentaNalP", objCobros.bPernoctaNal == true ? 1 : 0,
                                                                                                            "@SeDescuentaIntP", objCobros.bPernoctaInt == true ? 1 : 0,
                                                                                                            "@FactorConvHrVueloNalP", objCobros.dPernoctaFactorConversionNal,
                                                                                                            "@FactorConvHrVueloIntP", objCobros.dPernoctaFactorConversionInt,
                                                                                                            "@NumPernoctaLibresAnioP", objCobros.dNumeroPernoctasLibreAnual,
                                                                                                            "@CobroP", objCobros.bPernoctasCobro == true ? 1 : 0,
                                                                                                            "@DescuentoP", objCobros.bPernoctasDescuento == true ? 1 : 0,
                                                                                                            "@SeDescuentaNalTE", objCobros.bTiempoEsperaNal == true ? 1 : 0,
                                                                                                            "@SeDescuentaIntTE", objCobros.bTiempoEsperaInt == true ? 1 : 0,
                                                                                                            "@FactorConvHrVueloNal", objCobros.dTiempoEsperaFactorConversionNal,
                                                                                                            "@FactorConvHrVueloInt", objCobros.dTiempoEsperaFactorConversionInt,
                                                                                                            "@TiempoFacturar", objCobros.iTiempoFatura,
                                                                                                            "@MasMinutos", objCobros.dMinutos,
                                                                                                            "@AplicaTramos",objCobros.bAplicaTramos,
                                                                                                            "@Notas",objCobros.sNotas,
                                                                                                            "@IdCobraFerryHelicoptero", objCobros.iCobroFerrysHelicoptero,
                                                                                                            "@MasMinutosHelicoptero", objCobros.iMasMinutosHelicoptero,
                                                                                                            "@Status", 1,
                                                                                                            "@UsuarioCreacion", Utils.GetUser,
                                                                                                            "@IP", string.Empty);

                new DBUtils().DBSaveBitacoraModulos(Convert.ToInt32(Enumeraciones.Pantallas.Contrato), Convert.ToInt32(Enumeraciones.TipoAccion.Insertar), "Se creó cobros descuentos  del contrato: " + objCobros.iIdContrato.S());
                return oResult.I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBSaveCaracteristicas(Contrato_CaracteristicasEspeciales objCaracterisitcas)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Principales].[spI_MXJ_InsertaCaracteristicasEspecialesContrato]",  "@IdContrato", objCaracterisitcas.iIdContrato,
                                                                                                                    "@PenalizacionAle", objCaracterisitcas.sPenalizacionAleIncuplimiento,
                                                                                                                    "@PenalizacionClienteDemora",objCaracterisitcas.sPenalizacionClienteRetraso,
                                                                                                                    "@Acuerdos",objCaracterisitcas.sAcuerdosEspeciales,
                                                                                                                    "@AntiguedadAeronave", objCaracterisitcas.sAntiguedadAeronave,
                                                                                                                    "@TiempoMinSolicitarVuelo", objCaracterisitcas.sTiempoMinimoSolicitud,
                                                                                                                    "@TiempoMinSolicitarVueloFeriado",objCaracterisitcas.sTiempoMinimoSolicitudFeriado,
                                                                                                                    "@TiempoMinSolicitarVueloCA", objCaracterisitcas.sTiempoMinimoSolicitudCA,
                                                                                                                    "@TiempoMinCancelarVuelo", objCaracterisitcas.sTiempoMinimoCancelarVuelo,
                                                                                                                    "@PenalizacionCancelacionExtemporaneo", objCaracterisitcas.bPenalizacion,
                                                                                                                    "@VueloSimultaneo", objCaracterisitcas.bVuelosSimultaneos,
                                                                                                                    "@CantidadVuelosSimultaneos", objCaracterisitcas.iVuelosSimultaneos,
                                                                                                                    "@FactorVueloSimultaneo", objCaracterisitcas.dFactorVloSim,

                                                                                                                    "@TiempoMinSolicitarVueloFueraBaseNal", objCaracterisitcas.dTiempoMinimoSolicitudFBN,
                                                                                                                    "@RangoAcomodoVueloDiaFeriado", objCaracterisitcas.dRangoAcomodoVuelosFeriado,
                                                                                                                    "@ReprogramarSalidaAntesSalidaProg", objCaracterisitcas.dReprogramarSalidaAntesProgramada,
                                                                                                                    "@CancelacionAnticipadaSalBase", objCaracterisitcas.dCancelacionAnticipadaSB,
                                                                                                                    "@CancelacionAnticipadaSalNoBase", objCaracterisitcas.dCancelacionAnticipadaFB,

                                                                                                                    "@Comentarios", objCaracterisitcas.sComentarios,
                                                                                                                    "@Notas", objCaracterisitcas.sNotas,
                                                                                                                    "@Status", 1,
                                                                                                                    "@UsuarioCreacion", Utils.GetUser,
                                                                                                                    "@IP", string.Empty);

                new DBUtils().DBSaveBitacoraModulos(Convert.ToInt32(Enumeraciones.Pantallas.Contrato), Convert.ToInt32(Enumeraciones.TipoAccion.Insertar), "Se creó caracteristica especiales del contrato: " + objCaracterisitcas.iIdContrato.S());
                return oResult.I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBSaveIntercambio(Contrato_Intercambios objIntercambio)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Principales].[spI_MXJ_InsertaIntercambioContrato]", "@IdContrato", objIntercambio.iIdContrato,
                                                                                                            "@IdGrupoModelo",objIntercambio.iIdGrupoModelo,
                                                                                                            "@AplicaFerry",objIntercambio.bAplicaFerry,
                                                                                                            "@Factor", objIntercambio.dFactor,
                                                                                                            "@TarifaNal",objIntercambio.dTarifaNalDlls,
                                                                                                            "@TarifaInt",objIntercambio.dTarifaIntDlls,
                                                                                                            "@Galones",objIntercambio.dGalones,
                                                                                                            "@CostoDirectoNal",objIntercambio.dCDN,
                                                                                                            "@CostoDirectoInt",objIntercambio.dCDI,

                                                                                                            "@Status", 1,
                                                                                                            "@UsuarioCreacion", Utils.GetUser,
                                                                                                            "@IP", string.Empty);

                new DBUtils().DBSaveBitacoraModulos(Convert.ToInt32(Enumeraciones.Pantallas.Contrato), Convert.ToInt32(Enumeraciones.TipoAccion.Insertar), "Se creó intercambio del contrato: " + objIntercambio.iIdContrato.S());
                return oResult.I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBSaveServicioConCargo(Contrato_CobrosDescuentos objDescuento)
        {
            try
            {
                int iCont = 0;
                foreach(int id in objDescuento.lstIdServiciosConCargo)
                {

                    object oResult;
                    oResult = oDB_SP.EjecutarValor("[Principales].[spI_MXJ_InsertaServiciosCargoContrato]", "@IdContrato", objDescuento.iIdContrato,
                                                                                                                "@IdServiciosCargo", id,
                                                                                                                "@Status", 1,
                                                                                                                "@UsuarioCreacion", Utils.GetUser,
                                                                                                                "@IP", string.Empty);

                    new DBUtils().DBSaveBitacoraModulos(Convert.ToInt32(Enumeraciones.Pantallas.Contrato), Convert.ToInt32(Enumeraciones.TipoAccion.Insertar), "Se creó servicio con cargo del contrato: " + objDescuento.iIdContrato.S());
                }
                return iCont;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public int DBUpdateGeneralesBase(Contrato_Generales oContrato)
        {
            try
            {
                object oResult;
                int iCantidadBases = 0;
                foreach (Contratos_Bases objBases in oContrato.lstBases)
                {
                    iCantidadBases += 1;
                    int iIdAeropuerto = DbSearchIdAeropuertoIata(objBases.sAeropuerto);
                    oResult = oDB_SP.EjecutarValor("[Principales].[spU_MXJ_ActualizaBasesContrato]", "@IdBase", objBases.iId,
                                                                                                    "@IdContrato", objBases.iIdContrato,
                                                                                                    "@IdAeropuerto", iIdAeropuerto,
                                                                                                    "@IdTipoBase", objBases.iPredeterminada,
                                                                                                    "@Status", oContrato.iStatus,
                                                                                                    "@IP", oContrato.sIP,
                                                                                                    "@UsuarioModificacion", Utils.GetUser);
                   new DBUtils().DBSaveBitacoraModulos(Convert.ToInt32(Enumeraciones.Pantallas.Contrato), Convert.ToInt32(Enumeraciones.TipoAccion.Actualizar), "Se modificó bases del contrato: " + objBases.iIdContrato.S());
                }

                return iCantidadBases;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBUpdateGeneral(Contrato_Generales oContrato)
        {
            try
            { 
                object oResult;
                int iCantidadBases = 0;
               
                    iCantidadBases += 1;
                    oResult = oDB_SP.EjecutarValor("[Principales].[spU_MXJ_ActualizaGeneralesContrato]", "@IdContrato", oContrato.iIdContrato,
                                                                                                        "@IdCliente", oContrato.iIdCliente,
                                                                                                        "@ClaveContrato",oContrato.sContrato,
                                                                                                        "@FechaContrato",oContrato.dtFechaContrato,
                                                                                                        "@IdVendedor",oContrato.iiDVendedor,
                                                                                                        "@IdEjecutivo",oContrato.iIdEjecutivo,
                                                                                                        "@FechaInicioVuelo",oContrato.dtFechaInicioVuelo,
                                                                                                        "@IdPaquete",oContrato.iIdPAquete,
                                                                                                        "@IdGrupoModelo",oContrato.iIdGrupoModelo,
                                                                                                        "@AniosContratados",oContrato.iAñoContratados,
                                                                                                        "@MesesGracia",oContrato.iMesesGracia,
                                                                                                        "@HorasContratadasTotal",oContrato.iHorasContratadasTotal,
                                                                                                        "@HorasContratadasAnio",oContrato.iHorasContratadasAño,
                                                                                                        "@PorcentajeHorasNoUsadas",oContrato.dHorasNoUsadasAcumulables,
                                                                                                        "@Matricula",oContrato.sMatricula,
                                                                                                        "@TipoCambio",oContrato.iIdTipoCambio,
                                                                                                        "@AnticipoInicial",oContrato.dAnticipioInicial,
                                                                                                        "@FijoAnual",oContrato.dFijoAnual,
                                                                                                        "@Renovacion",oContrato.dRenovacion,
                                                                                                        "@Prenda",oContrato.dPrenda,
                                                                                                        "@IncrementoCostoDirRen",oContrato.dIncrementoCostoDirectoRenovacion,
                                                                                                        "@ReasignaRemisiones",oContrato.bReasigna,
                                                                                                        "@Notas",oContrato.sNotas,
                                                                                                        "@MetodoPagoFact", oContrato.sMetodoPagoFact,
                                                                                                        "@FormaPagoFact", oContrato.sFormaPago,
                                                                                                        "@UsoCFDIFact", oContrato.sUsoCFDI,
                                                                                                        "@FormatoEdoCta", oContrato.sFormatoEdoCta,
                                                                                                        "@Status", oContrato.iStatus,
                                                                                                        "@IP", oContrato.sIP,
                                                                                                        "@NotasIntercambios", oContrato.sNotasIntercambios,
                                                                                                        "@NotasRangosCombustible", oContrato.sNotasRangoCombustible,
                                                                                                        "@Usuario", Utils.GetUser);

                if (oContrato.bContratoD.Length > 1)
                {
                    //AQUI VA EL CODIGO DE INSERCION
                    DBSaveDigitalContract(oContrato.iIdContrato, oContrato.bContratoD, oContrato.sNombreArchivo);
                }

                new DBUtils().DBSaveBitacoraModulos(Convert.ToInt32(Enumeraciones.Pantallas.Contrato), Convert.ToInt32(Enumeraciones.TipoAccion.Actualizar), "Se modificó el contrato: " + oContrato.iIdContrato.S());
                    
                return iCantidadBases;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public int DBUpdateEspeciales(Contrato_CaracteristicasEspeciales oCaracteristicas)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Principales].[spU_MXJ_ActualizaCaracteristicasEspecialesContrato]", "@IdContrato", oCaracteristicas.iIdContrato,
                                                                                                                    "@PenalizacionAle", oCaracteristicas.sPenalizacionAleIncuplimiento,
                                                                                                                    "@PenalizacionClienteDemora", oCaracteristicas.sPenalizacionClienteRetraso,
                                                                                                                    "@Acuerdos", oCaracteristicas.sAcuerdosEspeciales,
                                                                                                                    "@AntiguedadAeronave", oCaracteristicas.sAntiguedadAeronave,
                                                                                                                    "@TiempoMinSolicitarVuelo", oCaracteristicas.sTiempoMinimoSolicitud,
                                                                                                                    "@TiempoMinSolicitarVueloFeriado", oCaracteristicas.sTiempoMinimoSolicitudFeriado,
                                                                                                                    "@TiempoMinSolicitarVueloCA", oCaracteristicas.sTiempoMinimoSolicitudCA,
                                                                                                                    "@TiempoMinCancelarVuelo", oCaracteristicas.sTiempoMinimoCancelarVuelo,
                                                                                                                    "@PenalizacionCancelacionExtemporaneo", oCaracteristicas.bPenalizacion,
                                                                                                                    "@VueloSimultaneo", oCaracteristicas.bVuelosSimultaneos,
                                                                                                                    "@CantidadVuelosSimultaneos", oCaracteristicas.iVuelosSimultaneos,
                                                                                                                    "@FactorVueloSimultaneo", oCaracteristicas.dFactorVloSim,

                                                                                                                    "@TiempoMinSolicitarVueloFueraBaseNal", oCaracteristicas.dTiempoMinimoSolicitudFBN,
                                                                                                                    "@RangoAcomodoVueloDiaFeriado", oCaracteristicas.dRangoAcomodoVuelosFeriado,
                                                                                                                    "@ReprogramarSalidaAntesSalidaProg", oCaracteristicas.dReprogramarSalidaAntesProgramada,
                                                                                                                    "@CancelacionAnticipadaSalBase", oCaracteristicas.dCancelacionAnticipadaSB,
                                                                                                                    "@CancelacionAnticipadaSalNoBase", oCaracteristicas.dCancelacionAnticipadaFB,

                                                                                                                    "@Comentarios", oCaracteristicas.sComentarios,
                                                                                                                    "@Notas", oCaracteristicas.sNotas,
                                                                                                                    "@Status", 1,
                                                                                                                    "@UsuarioModificacion", Utils.GetUser,
                                                                                                                    "@IP", string.Empty);

                new DBUtils().DBSaveBitacoraModulos(Convert.ToInt32(Enumeraciones.Pantallas.Contrato), Convert.ToInt32(Enumeraciones.TipoAccion.Actualizar), "Se modificó caracteristicas especiales del contrato: " + oCaracteristicas.iIdContrato.S());
                return oResult.I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBUpdateCombustible(Contrato_CombustibleInternacional oCombustible)
            {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Principales].[spU_MXJ_ActualizaCombustibleInternacionalContrato]", "@IdCombustible",oCombustible.iId,
                                                                                                                    "@IdContrato", oCombustible.iIdContrato,
                                                                                                                     "@Fecha", oCombustible.dtFecha,
                                                                                                                     "@Importe", oCombustible.dImport,
                                                                                                                     "@Status", 1,
                                                                                                                     "@UsuarioModificacion", Utils.GetUser,
                                                                                                                     "@IP", null);

                new DBUtils().DBSaveBitacora(Convert.ToInt32(Enumeraciones.Pantallas.Contrato), Convert.ToInt32(Enumeraciones.TipoOperacion.Actualizar), "Se modificó combustible internacional del contrato: " + oCombustible.iIdContrato.S());
                return oResult.I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBUpdateGiras(Contrato_GirasFechasPico oGiras)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Principales].[spU_MXJ_ActualizaGirasFechasPicoContrato]", "@IdContrato", oGiras.iIdContrato,
                                                                                                            "@GiraEspera", oGiras.bAplicaGiraEspera,
                                                                                                            "@TiempoVuelo", oGiras.iNumeroVeces,
                                                                                                            "@GiraHora", oGiras.bAplicaGiraHora,
                                                                                                            "@HoraInicio", oGiras.sHoraInicio,
                                                                                                            "@HoraFin", oGiras.sHoraFin,
                                                                                                            "@PorcentajeDescuentoHoras", oGiras.dPorcentajeDescuento,
                                                                                                            "@FechaPico", oGiras.bAplicaFactorFechaPico,
                                                                                                            "@FactorFechaPico", oGiras.dFactorFechaPico,
                                                                                                            "@Notas", oGiras.sNotas,
                                                                                                            "@Status", 1,
                                                                                                            "@UsuarioModificacion", Utils.GetUser,
                                                                                                            "@IP", string.Empty);

                new DBUtils().DBSaveBitacoraModulos(Convert.ToInt32(Enumeraciones.Pantallas.Contrato), Convert.ToInt32(Enumeraciones.TipoAccion.Actualizar), "Se modificó giras fechas pico del contrato: " + oGiras.iIdContrato.S());
                return oResult.I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBUpdateIntercambioContratos(Contrato_Intercambios ointercambios)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Principales].[spU_MXJ_ActualizaIntercambioContrato]", "@IdIntercambio", ointercambios.iId, 
                                                                                                       "@IdContrato", ointercambios.iIdContrato,
                                                                                                       "@IdGrupoModelo", ointercambios.iIdGrupoModelo,
                                                                                                       "@AplicaFerry", ointercambios.bAplicaFerry,
                                                                                                       "@Factor", ointercambios.dFactor,
                                                                                                       "@TarifaNal", ointercambios.dTarifaNalDlls,
                                                                                                       "@TarifaInt", ointercambios.dTarifaIntDlls,
                                                                                                       "@Galones", ointercambios.dGalones,
                                                                                                       "@CostoDirectoNal", ointercambios.dCDN,
                                                                                                        "@CostoDirectoInt", ointercambios.dCDI,
                                                                                                        "@Status", 1,
                                                                                                        "@UsuarioModificacion", Utils.GetUser,
                                                                                                        "@IP", null);

                new DBUtils().DBSaveBitacoraModulos(Convert.ToInt32(Enumeraciones.Pantallas.Contrato), Convert.ToInt32(Enumeraciones.TipoAccion.Actualizar), "Se modificó intercambio del contrato: " + ointercambios.iIdContrato.S());
                return oResult.I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBUpdateRangoCombustible(Contrato_RangoCombustible oRango)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Principales].[spU_MXJ_ActualizaRangosCombustibleContrato]", "@IdRango", oRango.iId,
                                                                                                             "@IdContrato", oRango.iIdContrato,
                                                                                                             "@IdGrupoModelo", oRango.iIdGrupoModelo,
                                                                                                             "@IdTipo", oRango.iIdTipo,
                                                                                                             "@Incremento", oRango.dIncremento,
                                                                                                             "@Desde", oRango.dDesde,
                                                                                                             "@Hasta", oRango.dHasta,
                                                                                                             "@Status", 1,
                                                                                                             "@UsuarioModificacion", Utils.GetUser,
                                                                                                             "@IP", null);

                new DBUtils().DBSaveBitacoraModulos(Convert.ToInt32(Enumeraciones.Pantallas.Contrato), Convert.ToInt32(Enumeraciones.TipoAccion.Actualizar), "Se modifico rangos de combustible del contrato: " + oRango.iIdContrato.S());
                return oResult.I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBUpdateTarifasContrato (Contrato_Tarifas oTarifas)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Principales].[spU_MXJ_ActualizaTarifasContrato]", "@IdContrato", oTarifas.iIdContrato,
                                                                                                "@CostoDirectoNalV", oTarifas.dCostoDirNal,
                                                                                                "@CostoDirectoIntV", oTarifas.dCostoDirInt,
                                                                                                "@SeCobraCombustibleV", oTarifas.bCombustible,
                                                                                                "@CalculoPrecioCombuV", oTarifas.iTipoCalculo,
                                                                                                "@ConsumoGalonesHrV", oTarifas.dConsumoGalones,
                                                                                                "@FactorTramosNal", oTarifas.dFactorTramosNal,
                                                                                                "@FactorTramosInt", oTarifas.dFactorTramosInt,
                                                                                                "@AplicaFactorCombustible", oTarifas.bAplicaFactorCombustible ? 1 : 0,
                                                                                                "@CombustibleIntEspV", oTarifas.bPrecioInternacionalEspecial,
                                                                                                "@SeCobraTE", oTarifas.bCobraTiempoEspera,
                                                                                                "@TarifaNacionalTE", oTarifas.dTiempoEsperaFijaNal,
                                                                                                "@TarifaInteracionalTE", oTarifas.dTiempoEsperaFijaInt,
                                                                                                "@PorcentajeTarifaTE", oTarifas.dTiempoEsperaVarNal,
                                                                                                "@PorcentajeInternacionalTE", oTarifas.dTiempoEsperaVarInt,
                                                                                                "@SeCobraPernoctaP", oTarifas.bCobraPernoctas,
                                                                                                "@TarifaNacionalP", oTarifas.dPernoctasFijaNal,
                                                                                                "@TarifaInternacionalP", oTarifas.dPernoctasFijaInt,
                                                                                                "@PorcentajeTarifaP", oTarifas.dPernoctasVarNal,
                                                                                                "@PorcentajeInternacionalP", oTarifas.dPernoctasVarInt,
                                                                                                "@CostoDirectoNalInflacion", oTarifas.iCDNBaseInflacion,
                                                                                                "@CostoDirectoIntInflacion", oTarifas.iCDIBaseInflacion,
                                                                                                "@FijoAnualInflacion", oTarifas.iFABaseInflacion,
                                                                                                "@CostoDirectoNalPorcentaje", oTarifas.dCDNPorcentaje,
                                                                                                "@CostoDirectoIntPorcentaje", oTarifas.dCDIPorcentaje,
                                                                                                "@FijoAnualPorcentaje", oTarifas.dFAPorcentaje,
                                                                                                "@CostoDirectoNalMasPuntos", oTarifas.dCDNPuntos,
                                                                                                "@CostoDirectoIntMasPuntos", oTarifas.dCDIPuntos,
                                                                                                "@FijoAnualMasPuntos", oTarifas.dFAPuntos,
                                                                                                "@CostoDirectoNalTope", oTarifas.dCDNTopeMAximo,
                                                                                                "@CostoDirectoIntTope", oTarifas.dCDITopeMAximo,
                                                                                                "@FijoAnualTope", oTarifas.dFATopeMAximo,
                                                                                                "@Notas",oTarifas.sNotas,
                                                                                                "@AplicaIncremento", oTarifas.iAplicaIncremento,
                                                                                                "@Status", 1,
                                                                                                "@UsuarioModificacion", Utils.GetUser,
                                                                                                "@IP", string.Empty);

                new DBUtils().DBSaveBitacoraModulos(Convert.ToInt32(Enumeraciones.Pantallas.Contrato), Convert.ToInt32(Enumeraciones.TipoOperacion.Actualizar), "Se modificó tarifas del contrato: " + oTarifas.iIdContrato.S());
                return oResult.I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBUpdateTramosEspeciales (Contrato_TramoPactado oTramos)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Principales].[spU_MXJ_ActualizaTramosEspecialesContrato]", "@IdContrato", oTramos.iIdContrato,
                                                                                                            "@IdTramo",oTramos.iId,
                                                                                                           "@IdAeropuertoO", oTramos.iIdOrigen,
                                                                                                           "@IdAeropuertoD", oTramos.iIdDestino,
                                                                                                           "@TiempoVuelo", oTramos.sTiempoVuelo,
                                                                                                           "@Status", 1,
                                                                                                           "@UsuarioModificacion", Utils.GetUser,
                                                                                                           "@IP", string.Empty);

                new DBUtils().DBSaveBitacora(Convert.ToInt32(Enumeraciones.Pantallas.Contrato), Convert.ToInt32(Enumeraciones.TipoOperacion.Actualizar), "Se modificó tramo especial del contrato: " + oTramos.iIdContrato.S());
                return oResult.I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBUpdateCobros(Contrato_CobrosDescuentos objCobros)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Principales].[spU_MXJ_ActualizaCobrosDescuentosContrato]", "@IdContrato", objCobros.iIdContrato,
                                                                                                            "@IdFerryConCargo", objCobros.iFerrysConCargo,
                                                                                                            "@EsperalLibre", objCobros.bAplicaEsperaLibre == true ? 1 : 0,
                                                                                                            "@HorasPorVuelo", objCobros.dHorasVuelo,
                                                                                                            "@FactorHrVuelo", objCobros.dFactorHorasVuelo,
                                                                                                            "@SeDescuentaNalP", objCobros.bPernoctaNal == true ? 1 : 0,
                                                                                                            "@SeDescuentaIntP", objCobros.bPernoctaInt == true ? 1 : 0,
                                                                                                            "@FactorConvHrVueloNalP", objCobros.dPernoctaFactorConversionNal,
                                                                                                            "@FactorConvHrVueloIntP", objCobros.dPernoctaFactorConversionInt,
                                                                                                            "@NumPernoctaLibresAnioP", objCobros.dNumeroPernoctasLibreAnual,
                                                                                                            "@CobroP", objCobros.bPernoctasCobro == true ? 1 : 0,
                                                                                                            "@DescuentoP", objCobros.bPernoctasDescuento == true ? 1 : 0,
                                                                                                            "@SeDescuentaNalTE", objCobros.bTiempoEsperaNal == true ? 1 : 0,
                                                                                                            "@SeDescuentaIntTE", objCobros.bTiempoEsperaInt == true ? 1 : 0,
                                                                                                            "@FactorConvHrVueloNal", objCobros.dTiempoEsperaFactorConversionNal,
                                                                                                            "@FactorConvHrVueloInt", objCobros.dTiempoEsperaFactorConversionInt,
                                                                                                            "@TiempoFacturar", objCobros.iTiempoFatura,
                                                                                                            "@MasMinutos", objCobros.dMinutos,
                                                                                                            "@AplicaTramos", objCobros.bAplicaTramos,
                                                                                                            "@Notas", objCobros.sNotas,
                                                                                                            "@IdCobraFerryHelicoptero", objCobros.iCobroFerrysHelicoptero,
                                                                                                            "@MasMinutosHelicoptero", objCobros.iMasMinutosHelicoptero,
                                                                                                            "@Status", 1,
                                                                                                            "@UsuarioModificacion", Utils.GetUser,
                                                                                                            "@IP", string.Empty);

                new DBUtils().DBSaveBitacoraModulos(Convert.ToInt32(Enumeraciones.Pantallas.Contrato), Convert.ToInt32(Enumeraciones.TipoAccion.Actualizar), "Se modifico cobro descuentos del contrato: " + objCobros.iIdContrato.S());
                return oResult.I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBUpdateServicios(Contrato_CobrosDescuentos objDescuento)
        {
            try
            {
                 object oResult;
                 oResult = oDB_SP.EjecutarValor("[Principales].[spD_MXJ_EliminaServiciosCargoContrato]", "@IdContrato", objDescuento.iIdContrato);
                 int iCantidad = 0;
                foreach(int iIdServicio in objDescuento.lstIdServiciosConCargo)
                {
                    iCantidad += 1;

                    oResult = oDB_SP.EjecutarValor("[Principales].[spI_MXJ_InsertaServiciosCargoContrato]", "@IdContrato", objDescuento.iIdContrato,
                                                                                                                "@IdServiciosCargo", iIdServicio,
                                                                                                                "@Status",1,
                                                                                                                "@UsuarioCreacion", Utils.GetUser,
                                                                                                                "@IP",string.Empty);
                }
                new DBUtils().DBSaveBitacoraModulos(Convert.ToInt32(Enumeraciones.Pantallas.Contrato), Convert.ToInt32(Enumeraciones.TipoAccion.Insertar), "Se modifico la lista Cobro Servicio con Cargo del contrato: " + objDescuento.iIdContrato.S());
                return iCantidad;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public int DBDeleteBase(Contrato_Generales oContrato)
        {
            try
            {
                 object oResult;
                int iCantidadBases = 0;
                int iIdContrato = 0;
                foreach (Contratos_Bases objBases in oContrato.lstBases)
                {
                    oResult = oDB_SP.EjecutarValor("[Principales].[spD_MXJ_EliminaBasesContrato]", "@IdBase", objBases.iId,
                                                                                                  "@IdContrato", objBases.iIdContrato);
                    iCantidadBases = oResult.S().I();
                    iIdContrato = objBases.iIdContrato;
                }
                new DBUtils().DBSaveBitacoraModulos(Convert.ToInt32(Enumeraciones.Pantallas.Contrato), Convert.ToInt32(Enumeraciones.TipoAccion.Eliminar), "Se eliminó base del contrato: " + iIdContrato.S());
                return iCantidadBases;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBDeleteEspeciales(Contrato_CaracteristicasEspeciales oEspeciales)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Principales].[spD_MXJ_EliminaCaracteristicasEspecialesContrato]", "@IdContrato", oEspeciales.iIdContrato);

                new DBUtils().DBSaveBitacoraModulos(Convert.ToInt32(Enumeraciones.Pantallas.Contrato), Convert.ToInt32(Enumeraciones.TipoAccion.Eliminar), "Se eliminó caracteristicas especiales del contrato: " + oEspeciales.iIdContrato.S());
                return oResult.I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBDeleteCombusInternacional(Contrato_CombustibleInternacional oCombusInter)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Principales].[spD_MXJ_EliminaCombustibleInternacionalContrato]", "@IdContrato", oCombusInter.iIdContrato,
                                                                                                                  "@IdCombustible", oCombusInter.iId);

                new DBUtils().DBSaveBitacora(Convert.ToInt32(Enumeraciones.Pantallas.Contrato), Convert.ToInt32(Enumeraciones.TipoOperacion.Eliminar), "Se eliminó combustible internacional del contrato: " + oCombusInter.iIdContrato.S());
                return oResult.I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBDeleteIntercambio(Contrato_Intercambios oIntercambio)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Principales].[spD_MXJ_EliminaIntercambioContrato]", "@IdIntercambio", oIntercambio.iId,
                                                                                                       "@IdContrato", oIntercambio.iIdContrato);

                new DBUtils().DBSaveBitacoraModulos(Convert.ToInt32(Enumeraciones.Pantallas.Contrato), Convert.ToInt32(Enumeraciones.TipoAccion.Eliminar), "Se eliminó intercambio del contrato: " + oIntercambio.iIdContrato.S());
                return oResult.I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBDeleteRangoCombustible(Contrato_RangoCombustible oRango)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Principales].[spD_MXJ_EliminaRangosCombustibleContrato]", "@IdRango", oRango.iId,
                                                                                                             "@IdContrato", oRango.iIdContrato);

                new DBUtils().DBSaveBitacoraModulos(Convert.ToInt32(Enumeraciones.Pantallas.Contrato), Convert.ToInt32(Enumeraciones.TipoAccion.Eliminar), "Se eliminó rango de combustible del contrato: " + oRango.iIdContrato.S());
                return oResult.I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBDeleteTramoEspecial(Contrato_TramoPactado oTramo)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Principales].[spD_MXJ_EliminaTramosEspecialesContrato]", "@IdContrato", oTramo.iIdContrato,
                                                                                                           "@IdTramo", oTramo.iId);

                new DBUtils().DBSaveBitacora(Convert.ToInt32(Enumeraciones.Pantallas.Contrato), Convert.ToInt32(Enumeraciones.TipoOperacion.Eliminar), "Se eliminó tramo especial del contrato: " + oTramo.iIdContrato.S());
                return oResult.I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBValidaRangoCobustible(Contrato_RangoCombustible oRangoCombus)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Principales].[spS_MXJ_RangosCombustibleContratoDesdeHasta]", "@IdContrato", oRangoCombus.iIdContrato,
                                                                                                              "@IdGrupoModelo", oRangoCombus.iIdGrupoModelo,
                                                                                                              "@IdTipo", oRangoCombus.iIdTipo,
                                                                                                              "@Desde", oRangoCombus.dDesde,
                                                                                                              "@Hasta", oRangoCombus.dHasta);
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBValidaRangoCombustibleUpdate(Contrato_RangoCombustible oRangoCombustiValida, bool bDesde, bool bHasta)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Principales].[spS_MXJ_RangosCombustibleContratoDesdeHastaUpdate]", "@IdRango", oRangoCombustiValida.iId,
                                                                                                                      "@Desde", oRangoCombustiValida.dDesde,
                                                                                                                      "@Hasta", oRangoCombustiValida.dHasta,
                                                                                                                      "@validaDesde", bDesde,
                                                                                                                      "@validaHasta",bHasta);
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBValidaTramoEspecial(Contrato_TramoPactado oTramoEspecial)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Principales].[spS_MXJ_ConsultaTramosEspecialesContratoExiste]", "@IdContrato", oTramoEspecial.iIdContrato,
                                                                                                              "@IdAeropuertoO", oTramoEspecial.iIdOrigen,
                                                                                                           "@IdAeropuertoD", oTramoEspecial.iIdDestino);
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBValidaCombustiInterna(Contrato_CombustibleInternacional oCombustibleInternacional)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Principales].[spS_MXJ_ConsultaCombustibleInternacionalContratoExiste]", "@IdContrato", oCombustibleInternacional.iIdContrato,
                                                                                                                         "@Fecha", oCombustibleInternacional.dtFecha);
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBValidaIntercambio(Contrato_Intercambios oIntercambio)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Principales].[spS_MXJ_ConsultaIntercambioContratoExiste]", "@IdContrato", oIntercambio.iIdContrato,
                                                                                                            "@IdGrupoModelo", oIntercambio.iIdGrupoModelo,
                                                                                                            "@IdIntercambio",oIntercambio.iId);
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBValidaConsultaClave(Contrato_Generales oClaveContrato)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Principales].[spS_MXJ_ConsultaClaveContratoExiste]", "@ClaveContrato", oClaveContrato.sContrato);
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Contrato_Generales DBGetContratoGenerales(int iIdContrato)
        {
            try
            {
                DataTable dtResult;
                Contrato_Generales objGenerales = new Contrato_Generales();
                dtResult = oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaGeneralesContrato]", "@IdContrato", iIdContrato);
                foreach(DataRow dr in dtResult.Rows)
                {
                    objGenerales.iIdContrato = iIdContrato;
                    objGenerales.iIdCliente = dr.ItemArray[1].S().I();
                    objGenerales.sContrato = dr.ItemArray[2].S();
                    objGenerales.dtFechaContrato = dr.ItemArray[3].Dt();
                    objGenerales.iiDVendedor = dr.ItemArray[4].S().I();
                    objGenerales.iIdEjecutivo = dr.ItemArray[5].S().I();
                    objGenerales.dtFechaInicioVuelo = dr.ItemArray[6].Dt();
                    objGenerales.iIdPAquete = dr.ItemArray[7].S().I();
                    objGenerales.iIdGrupoModelo = dr.ItemArray[8].S().I();
                    objGenerales.iAñoContratados = dr.ItemArray[9].S().I();
                    objGenerales.iMesesGracia = dr.ItemArray[10].S().I();
                    objGenerales.iHorasContratadasTotal = dr.ItemArray[11].S().I();
                    objGenerales.iHorasContratadasAño = dr.ItemArray[12].S().I();
                    objGenerales.dHorasNoUsadasAcumulables = dr.ItemArray[13].S().D();
                    objGenerales.sMatricula = dr.ItemArray[14].S();
                    objGenerales.iIdTipoCambio = dr.ItemArray[15].S().I();
                    objGenerales.dAnticipioInicial = dr.ItemArray[16].S().D();
                    objGenerales.dFijoAnual = dr.ItemArray[17].S().D();
                    objGenerales.dRenovacion = dr.ItemArray[18].S().D();
                    objGenerales.dPrenda = dr.ItemArray[19].S().D();
                    objGenerales.dIncrementoCostoDirectoRenovacion = dr.ItemArray[20].S().D();
                    objGenerales.bReasigna = dr.ItemArray[21].S().I() > 0;
                    objGenerales.sNotas = dr.ItemArray[22].S();
                    objGenerales.sMetodoPagoFact = dr.S("MetodoPagoFact");
                    objGenerales.sFormaPago = dr.S("FormaPagoFact");
                    objGenerales.sUsoCFDI = dr.S("UsoCFDIFact");
                    objGenerales.sFormatoEdoCta = dr.S("FormatoEdoCta");
                    objGenerales.iStatus = dr["Status"].S().I();

                    objGenerales.sNotasIntercambios = dr["NotasIntercambios"].S();
                    objGenerales.sNotasRangoCombustible = dr["NotasRangosCombustible"].S();
                }

                if (objGenerales.iIdContrato > 0)
                {
                    DataTable dtContratosDigitales;
                    dtContratosDigitales = oDB_SP.EjecutarDT("[Principales].[SpS_MXJ_ConsultaContratoDigital]", "@IdContrato",iIdContrato);
                    foreach (DataRow row in dtContratosDigitales.Rows)
                    {
                        objGenerales.sNombreArchivo = row[2].S();
                        objGenerales.bContratoD = (byte[])row[1];
                    }
                }

                if (objGenerales.iIdContrato > 0)
                {
                    DataTable dtResultBases;
                    dtResultBases = oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaBasesContrato]", "@IdContrato", iIdContrato);
                    Contratos_Bases objContratoBase = new Contratos_Bases();
                    foreach (DataRow dr in dtResultBases.Rows)
                    {
                        objContratoBase = new Contratos_Bases();
                        objContratoBase.iIdContrato = iIdContrato;
                        objContratoBase.iId = dr.ItemArray[0].S().I();
                        objContratoBase.sAeropuerto = dr.ItemArray[1].S();
                        objContratoBase.iPredeterminada = dr.ItemArray[2].S().I();
                        objGenerales.lstBases.Add(objContratoBase);
                    }
                }

                return objGenerales;


            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public Contrato_Tarifas DBGetTarifa(int iIdContrato)
        {
            try 
            { 
                DataTable dtResult;
                Contrato_Tarifas objGenerales = new Contrato_Tarifas();
                dtResult = oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaTarifasContrato]", "@IdContrato", iIdContrato);
                foreach (DataRow dr in dtResult.Rows)
                {
                    objGenerales.iIdContrato = iIdContrato;
                    objGenerales.dCostoDirNal = dr.ItemArray[1].S().D();
                    objGenerales.dCostoDirInt = dr.ItemArray[2].S().D();
                    objGenerales.bCombustible = dr.ItemArray[3].S().I()>0;
                    objGenerales.iTipoCalculo = dr.ItemArray[4].S().I();
                    objGenerales.dConsumoGalones= dr.ItemArray[5].S().D();
                    objGenerales.bPrecioInternacionalEspecial= dr.ItemArray[6].S().I()>0;
                    
                    objGenerales.bCobraTiempoEspera = dr.ItemArray[7].S().I() > 0;
                    objGenerales.dTiempoEsperaFijaNal = dr.ItemArray[8].S().D();
                    objGenerales.dTiempoEsperaFijaInt = dr.ItemArray[9].S().D();
                    objGenerales.dTiempoEsperaVarNal  = dr.ItemArray[10].S().D();
                    objGenerales.dTiempoEsperaVarInt  = dr.ItemArray[11].S().D();

                    objGenerales.bCobraPernoctas = dr.ItemArray[12].S().I() > 0;
                    objGenerales.dPernoctasFijaNal = dr.ItemArray[13].S().D();
                    objGenerales.dPernoctasFijaInt = dr.ItemArray[14].S().D();
                    objGenerales.dPernoctasVarNal = dr.ItemArray[15].S().D();
                    objGenerales.dPernoctasVarInt = dr.ItemArray[16].S().D();

                    objGenerales.iCDNBaseInflacion = dr.ItemArray[17].S().I();
                    objGenerales.iCDIBaseInflacion = dr.ItemArray[18].S().I();
                    objGenerales.iFABaseInflacion= dr.ItemArray[19].S().I();

                    objGenerales.dCDNPorcentaje = dr.ItemArray[20].S().D();
                    objGenerales.dCDIPorcentaje = dr.ItemArray[21].S().D();
                    objGenerales.dFAPorcentaje = dr.ItemArray[22].S().D();

                    objGenerales.dCDNPuntos = dr.ItemArray[23].S().D();
                    objGenerales.dCDIPuntos = dr.ItemArray[24].S().D(); 
                    objGenerales.dFAPuntos = dr.ItemArray[25].S().D();

                    objGenerales.dCDNTopeMAximo = dr.ItemArray[26].S().D();
                    objGenerales.dCDITopeMAximo = dr.ItemArray[27].S().D();
                    objGenerales.dFATopeMAximo = dr.ItemArray[28].S().D();

                    objGenerales.sNotas = dr.ItemArray[29].S();
                    objGenerales.iAplicaIncremento = dr.ItemArray[30].S().I();

                    objGenerales.dFactorTramosNal = dr.S("FactorTramosNal").D();
                    objGenerales.dFactorTramosInt = dr.S("FactorTramosInt").D();

                    objGenerales.bAplicaFactorCombustible = dr.S("AplicaFactorCombustible") == "1" ? true : false;
                }
                return objGenerales;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public Contrato_CobrosDescuentos DBGetCobros(int iIdContrato)
        {
            try
            {
                DataTable dtResult;
                Contrato_CobrosDescuentos objGenerales = new Contrato_CobrosDescuentos();
                dtResult = oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaCobrosDescuentosContrato]", "@IdContrato", iIdContrato);
                foreach (DataRow dr in dtResult.Rows)
                {
                    objGenerales.iIdContrato = iIdContrato;
                    objGenerales.iFerrysConCargo = dr.ItemArray[1].S().I();
                    objGenerales.bAplicaEsperaLibre = dr.ItemArray[2].S().I() > 0;
                    objGenerales.dHorasVuelo = dr.ItemArray[3].S().D();
                    objGenerales.dFactorHorasVuelo = dr.ItemArray[4].S().D();

                    objGenerales.bPernoctaNal = dr.ItemArray[5].S().I() > 0;
                    objGenerales.bPernoctaInt = dr.ItemArray[6].S().I() > 0;
                    objGenerales.dPernoctaFactorConversionNal = dr.ItemArray[7].S().D();
                    objGenerales.dPernoctaFactorConversionInt = dr.ItemArray[8].S().D();
                    objGenerales.dNumeroPernoctasLibreAnual = dr.ItemArray[9].S().D();
                    objGenerales.bPernoctasCobro = dr.ItemArray[10].B();
                    objGenerales.bPernoctasDescuento = dr.ItemArray[11].B();

                    objGenerales.bTiempoEsperaNal = dr.ItemArray[12].S().I() > 0;
                    objGenerales.bTiempoEsperaInt = dr.ItemArray[13].S().I() > 0;
                    objGenerales.dTiempoEsperaFactorConversionNal = dr.ItemArray[14].S().D();
                    objGenerales.dTiempoEsperaFactorConversionInt = dr.ItemArray[15].S().D();
                    objGenerales.iTiempoFatura = dr.ItemArray[16].S().I();
                    objGenerales.dMinutos = dr.ItemArray[17].S().D();
                    objGenerales.bAplicaTramos = dr.ItemArray[18].B();
                    objGenerales.sNotas = dr.ItemArray[19].S();
                    objGenerales.iCobroFerrysHelicoptero = dr["IdCobraFerryHelicoptero"].S().I();
                    objGenerales.iMasMinutosHelicoptero = dr["MasMinutosHelicoptero"].S().I();
                }

                DataTable dtResultServicios;
                int iIDServicio = -1;
                dtResultServicios = oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaServiciosCargoContrato]", "@IdContrato", iIdContrato);
                foreach (DataRow dr in dtResultServicios.Rows)
                {
                    iIDServicio= 0;
                    iIDServicio = dr.ItemArray[1].S().I();
                    objGenerales.lstIdServiciosConCargo.Add(iIDServicio);
                }
                

                return objGenerales;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Contrato_GirasFechasPico DBGetGiras(int iIdContrato)
        {
            try
            {
                DataTable dtResult;
                Contrato_GirasFechasPico objGenerales = new Contrato_GirasFechasPico();
                dtResult = oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaGirasFechasPicoContrato]", "@IdContrato", iIdContrato);
                foreach (DataRow dr in dtResult.Rows)
                {
                    objGenerales.iIdContrato = iIdContrato;
                    objGenerales.bAplicaGiraEspera = dr.ItemArray[1].B();
                    objGenerales.iNumeroVeces = dr.ItemArray[2].S().I();
                    objGenerales.bAplicaGiraHora = dr.ItemArray[3].S().I() > 0;
                    objGenerales.sHoraInicio = dr.ItemArray[4].S();
                    objGenerales.sHoraFin = dr.ItemArray[5].S();
                    objGenerales.dPorcentajeDescuento = dr.ItemArray[6].S().D();
                    objGenerales.bAplicaFactorFechaPico = dr.ItemArray[7].S().I() > 0;
                    objGenerales.dFactorFechaPico = dr.ItemArray[8].S().D();
                    objGenerales.sNotas = dr.ItemArray[9].S();
                }
                return objGenerales;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBExisteCaracteristicas(int iIdContrato)
        {                                    
            try
            {
                DataTable oResult;
                oResult = oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ValidaExisteCaracteristicasEspecialesContrato]", "@IdContrato", iIdContrato);

                int Res = oResult.Rows[0][0].S().I();

                return Res.I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Contrato_CaracteristicasEspeciales DBGetCaracteristicas(int iIdContrato)
        {
            try
            {
                DataTable dtResult;
                Contrato_CaracteristicasEspeciales objGenerales = new Contrato_CaracteristicasEspeciales();
                dtResult = oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaCaracteristicasEspecialesContrato]", "@IdContrato", iIdContrato);
                foreach (DataRow dr in dtResult.Rows)
                {
                    objGenerales.iIdContrato = iIdContrato;
                    objGenerales.sPenalizacionAleIncuplimiento = dr.ItemArray[1].S();
                    objGenerales.sPenalizacionClienteRetraso = dr.ItemArray[2].S();
                    objGenerales.sAcuerdosEspeciales = dr.ItemArray[3].S();
                    objGenerales.sAntiguedadAeronave = dr.ItemArray[4].S();
                    objGenerales.sTiempoMinimoSolicitudFeriado = dr.ItemArray[5].S();
                    objGenerales.bPenalizacion = dr.ItemArray[6].S().I() > 0;
                    objGenerales.bVuelosSimultaneos = dr.ItemArray[7].B();
                    objGenerales.iVuelosSimultaneos = dr.ItemArray[8].S().I();
                    objGenerales.sComentarios = dr.ItemArray[9].S();
                    objGenerales.sTiempoMinimoCancelarVuelo = dr.ItemArray[10].S();
                    objGenerales.sTiempoMinimoSolicitud = dr.ItemArray[11].S();
                    objGenerales.sTiempoMinimoSolicitudCA = dr.ItemArray[12].S();

                    objGenerales.dTiempoMinimoSolicitudFBN = dr["TiempoMinSolicitarVueloFueraBaseNal"].S().D();
                    objGenerales.dRangoAcomodoVuelosFeriado = dr["RangoAcomodoVueloDiaFeriado"].S().D();
                    objGenerales.dReprogramarSalidaAntesProgramada = dr["ReprogramarSalidaAntesSalidaProg"].S().D();
                    objGenerales.dCancelacionAnticipadaSB = dr["CancelacionAnticipadaSalBase"].S().D();
                    objGenerales.dCancelacionAnticipadaFB = dr["CancelacionAnticipadaSalNoBase"].S().D();
                    objGenerales.sNotas = dr["Notas"].S();
                    objGenerales.dFactorVloSim = dr["FactorVueloSimultaneo"].S().D();
                }
                return objGenerales;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable DtEstatusContrato  
        {
            get
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaCatalogoEstatusContrato]");
            }
        }

        public int DBSavePreferenciaContrato(Contrato_Preferencias objPreferencias)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Principales].[spI_MXJ_InsertaPreferenciasContrato]", "@IdContrato", objPreferencias.iIdContrato,
                                                                                                                    "@Puntualidad", objPreferencias.iPuntualidad,
                                                                                                                    "@TipoProteccion", objPreferencias.iTipoProteccion,
                                                                                                                    "@FlexibilidadCambios", objPreferencias.iFlexibilidadCambios,
                                                                                                                    "@MomentoSolicitaVuelo", objPreferencias.iMomentoSolicitaVuelo,
                                                                                                                    "@TamanioFamilia", objPreferencias.iTamanioFamilia,
                                                                                                                    "@PreferenciaFBOAeropuerto", objPreferencias.iPreferenciaFBOAeropuerto,
                                                                                                                    "@RealizaReservaciones", objPreferencias.iRealizaReservaciones,
                                                                                                                    "@PreferenciaContacto", objPreferencias.iPreferenciaContacto,
                                                                                                                    "@FavoresClienteALE", objPreferencias.iFavoresClienteALE,
                                                                                                                    "@FavoresALECliente", objPreferencias.iFavoresALECliente,
                                                                                                                    "@AnticipaServicios", objPreferencias.iAnticipaServicios,
                                                                                                                    "@ComisariatoEspecial", objPreferencias.iComisariatoEspecial,
                                                                                                                    "@PreocupaCosto", objPreferencias.iPreocupaCosto,
                                                                                                                    "@TransporteTerrestreEspecial", objPreferencias.iTransporteTerrestreEspecial,
                                                                                                                    "@Mascotas", objPreferencias.iMascotas,
                                                                                                                    "@ServiciosOtroProveedor", objPreferencias.iServiciosOtroProveedor,
                                                                                                                    "@AeronavePropia", objPreferencias.iAeronavePropia,
                                                                                                                    "@IntercambioPagoServicios", objPreferencias.iIntercambioPagoServicios,
                                                                                                                    "@UsuarioCreacion", Utils.GetUser,
                                                                                                                    "@IP", string.Empty);

                new DBUtils().DBSaveBitacoraModulos(Convert.ToInt32(Enumeraciones.Pantallas.Contrato), Convert.ToInt32(Enumeraciones.TipoAccion.Insertar), "Se creó preferencias del contrato: " + objPreferencias.iIdContrato.S());
                return oResult.I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBUpdatePreferenciaContrato(Contrato_Preferencias objPreferencias)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Principales].[spU_MXJ_ActualizaPreferenciaContrato]", "@IdContrato", objPreferencias.iIdContrato,
                                                                                                                    "@Puntualidad", objPreferencias.iPuntualidad,
                                                                                                                    "@TipoProteccion", objPreferencias.iTipoProteccion,
                                                                                                                    "@FlexibilidadCambios", objPreferencias.iFlexibilidadCambios,
                                                                                                                    "@MomentoSolicitaVuelo", objPreferencias.iMomentoSolicitaVuelo,
                                                                                                                    "@TamanioFamilia", objPreferencias.iTamanioFamilia,
                                                                                                                    "@PreferenciaFBOAeropuerto", objPreferencias.iPreferenciaFBOAeropuerto,
                                                                                                                    "@RealizaReservaciones", objPreferencias.iRealizaReservaciones,
                                                                                                                    "@PreferenciaContacto", objPreferencias.iPreferenciaContacto,
                                                                                                                    "@FavoresClienteALE", objPreferencias.iFavoresClienteALE,
                                                                                                                    "@FavoresALECliente", objPreferencias.iFavoresALECliente,
                                                                                                                    "@AnticipaServicios", objPreferencias.iAnticipaServicios,
                                                                                                                    "@ComisariatoEspecial", objPreferencias.iComisariatoEspecial,
                                                                                                                    "@PreocupaCosto", objPreferencias.iPreocupaCosto,
                                                                                                                    "@TransporteTerrestreEspecial", objPreferencias.iTransporteTerrestreEspecial,
                                                                                                                    "@Mascotas", objPreferencias.iMascotas,
                                                                                                                    "@ServiciosOtroProveedor", objPreferencias.iServiciosOtroProveedor,
                                                                                                                    "@AeronavePropia", objPreferencias.iAeronavePropia,
                                                                                                                    "@IntercambioPagoServicios", objPreferencias.iIntercambioPagoServicios,
                                                                                                                    "@UsuarioModificacion", Utils.GetUser,
                                                                                                                    "@IP", string.Empty);

                new DBUtils().DBSaveBitacoraModulos(Convert.ToInt32(Enumeraciones.Pantallas.Contrato), Convert.ToInt32(Enumeraciones.TipoAccion.Actualizar), "Se modificó intercambio del contrato: " + objPreferencias.iIdContrato.S());
                return oResult.I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Contrato_Preferencias DBGetPreferenciaContrato(int iIdContrato)
        {
            try
            {
                try
                {
                    DataTable dtResultado;
                    Contrato_Preferencias objPreferencia = new Contrato_Preferencias();
                    dtResultado = oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaPreferenciasContrato]", "@IdContrato", iIdContrato);
                    foreach (DataRow dr in dtResultado.Rows)
                    {
                        objPreferencia.iIdContrato = iIdContrato;
                        objPreferencia.iPuntualidad = dr["Puntualidad"].S().I();
                        objPreferencia.iTipoProteccion = dr["TipoProteccion"].S().I();
                        objPreferencia.iFlexibilidadCambios = dr["FlexibilidadCambios"].S().I();
                        objPreferencia.iMomentoSolicitaVuelo = dr["MomentoSolicitaVuelo"].S().I();
                        objPreferencia.iTamanioFamilia = dr["TamanioFamilia"].S().I();
                        objPreferencia.iPreferenciaFBOAeropuerto = dr["PreferenciaFBOAeropuerto"].S().I();
                        objPreferencia.iRealizaReservaciones = dr["RealizaReservaciones"].S().I();
                        objPreferencia.iPreferenciaContacto = dr["PreferenciaContacto"].S().I();
                        objPreferencia.iFavoresClienteALE = dr["FavoresClienteALE"].S().I();
                        objPreferencia.iFavoresALECliente = dr["FavoresALECliente"].S().I();
                        objPreferencia.iAnticipaServicios = dr["AnticipaServicios"].S().I();
                        objPreferencia.iComisariatoEspecial = dr["ComisariatoEspecial"].S().I();
                        objPreferencia.iPreocupaCosto = dr["PreocupaCosto"].S().I();
                        objPreferencia.iTransporteTerrestreEspecial = dr["TransporteTerrestreEspecial"].S().I();
                        objPreferencia.iMascotas = dr["Mascotas"].S().I();
                        objPreferencia.iServiciosOtroProveedor = dr["ServiciosOtroProveedor"].S().I();
                        objPreferencia.iAeronavePropia = dr["AeronavePropia"].S().I();
                        objPreferencia.iIntercambioPagoServicios = dr["IntercambioPagoServicios"].S().I();
                    }
                    return objPreferencia;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }


    }
}