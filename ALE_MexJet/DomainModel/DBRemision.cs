using ALE_MexJet.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using NucleoBase.Core;
using NucleoBase.BaseDeDatos;
using ALE_MexJet.Clases;

namespace ALE_MexJet.DomainModel
{
    public class DBRemision: DBBase
    {
        public DataTable DBSearchObj(params object[] oArrFiltros)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaRemisiones]", oArrFiltros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable DBGetContracts(int iIdCliente)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_ConsultaContratosporCliente]", "@IdCliente", iIdCliente);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBSetInsertaRemision(Remision oRem)
        {
            try
            {
                object oRes = oDB_SP.EjecutarValor("[Principales].[spI_MXJ_InsertaRemision]", "@IdCliente", oRem.iIdCliente,
                                                                                                "@IdContrato", oRem.iIdContrato,
                                                                                                "@Fecha", oRem.dtFechaRemision,
                                                                                                "@AplicaIntercambio", oRem.bAplicaIntercambio,
                                                                                                "@FactorEspecial", oRem.dFactorEspecial,
                                                                                                "@UsuarioCreacion", oRem.sUsuarioCreacion,
                                                                                                "@IP", oRem.sIP);
                if (oRes == null)
                    return -1;
                else
                {
                    new DBUtils().DBSaveBitacora(Convert.ToInt32(Enumeraciones.Pantallas.Remisiones), Convert.ToInt32(Enumeraciones.TipoOperacion.Insertar), "Se creó la Remisión: " + oRes.S());
                    return oRes.S().I();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBSetActualizaRemision(Remision oRem)
        {
            try
            {
                object oRes = oDB_SP.EjecutarValor("[Principales].[spU_MXJ_ActualizaRemision]", "@IdRemision", oRem.iIdRemision,
                                                                                                "@IdCliente", oRem.iIdCliente,
                                                                                                "@IdContrato", oRem.iIdContrato,
                                                                                                "@Fecha", oRem.dtFechaRemision,
                                                                                                "@AplicaIntercambio", oRem.bAplicaIntercambio,
                                                                                                "@FactorEspecial", oRem.dFactorEspecial,
                                                                                                "@Usuario", oRem.sUsuarioCreacion,
                                                                                                "@IP", oRem.sIP);
                if (oRes == null)
                    return -1;
                else
                {
                    new DBUtils().DBSaveBitacora(Convert.ToInt32(Enumeraciones.Pantallas.Remisiones), Convert.ToInt32(Enumeraciones.TipoOperacion.Actualizar), "Se modificó la Remisión: " + oRem.iIdRemision.S());
                    return oRes.S().I();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable DBGetObtieneBitacorasPendientes(string sCliente, string sContrato)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaBitacorasPendientes]", "@VueloClienteId", sCliente, 
                                                                                                "@VueloContratoId", sContrato);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DBSetInsertaTramosRemision(List<BitacoraRemision> oLs)
        {
            try
            {
                long idRemision = 0;
                foreach(BitacoraRemision oBit in oLs)
                {
                    oDB_SP.EjecutarSP("[Principales].[spI_MXJ_ActualizaTramosRemision]", "@IdRemision", oBit.iIdRemision,
                                                                                        "@IdBitacora", oBit.lBitacora,
                                                                                        "@IdTipoPierna", oBit.iIdTipoPierna,
                                                                                        "@SeCobra", oBit.iSeCobra,
                                                                                        "@UsuarioCreacion", oBit.sUsuario,
                                                                                        "@IP", oBit.sIP);
                    idRemision = oBit.iIdRemision;
                }
                new DBUtils().DBSaveBitacora(Convert.ToInt32(Enumeraciones.Pantallas.Remisiones), Convert.ToInt32(Enumeraciones.TipoOperacion.Actualizar), "Se modifico tramo de la Remisión: " + idRemision.S());
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public DataSet DBGetObtieneTiemposRemision(long lIdRemision)
        {
            try
            {
                return oDB_SP.EjecutarDS("[Principales].[spS_MXJ_ConsultaTotalTiemposVuelo]", "@IdRemision", lIdRemision);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public DataTable DBGetObtieneTramosPactadosContrato(int iIdContrato)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaTramosPactadosCotrato]", "@IdContrato", iIdContrato);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DatosRemision DBGetObtieneDatosRemision(long lIdRemision, int iIdContrato)
        {
            try
            {
                DatosRemision odRem = new DatosRemision();
                odRem.lIdRemision = lIdRemision;
                odRem.iIdContrato = iIdContrato;

                DataSet ds = oDB_SP.EjecutarDS("[Principales].[spS_MXJ_ConsultaDatosRemision]", "@IdRemision", lIdRemision,
                                                                                                "@IdContrato", iIdContrato);

                if(ds.Tables.Count == 3)
                {
                    DataRow row = ds.Tables[0].Rows[0];

                    odRem.iCobroTiempo = row["CobroTiempo"].S().I();  // 1.- Tiempo Vuelo 2.- TiempoCalzo
                    odRem.eSeCobraFerry = (Enumeraciones.SeCobraFerrys) row["SeCobraFerry"].S().I();
                    odRem.dFactorTramosNal = row["FactorTramosNal"].S().D();
                    odRem.dFactorTramosInt = row["FactorTramosInt"].S().D();
                    odRem.bAplicaEsperaLibre = row["EsperaLibre"].S() == "1" ? true : false;
                    odRem.dHorasPorVuelo = row["HorasPorVuelo"].S().D();
                    odRem.dFactorHrVuelo = row["FactorHrVuelo"].S().D();
                    odRem.iMasMinutos = row["MasMinutos"].S().I();
                    odRem.sRuta = row["Ruta"].S();
                    odRem.bTiemposPactados = row["TramosPactados"].S().B();
                    odRem.iHorasPernocta = row["HorasPernocta"].S().I();
                    odRem.iFactosTiempoEsp = row["FactorTiempoEspera"].S().I();
                    odRem.iIdGrupoModelo = row["GrupoModelo"].S().I();
                    odRem.sGrupoModeloDesc = row.S("GrupoModeloDesc");
                    odRem.dTipoCambio = row["TipoCambio"].S().D();
                    odRem.sTipoPaquete = row.S("PaqueteDesc");
                    odRem.sFechaInicio = row.S("FechaInicioVuelo");
                    odRem.iHorasContratadasTotal = row.S("HorasContratadasTotal").I();
                    odRem.iHorasContratadasAnio = row.S("HorasContratadasAnio").I();

                    // =====---- COBROS ----=====//
                    // COMBUSTIBLE
                    odRem.bSeCobraCombustible = row["SeCobraCombustible"].S().B();
                    odRem.eCobroCombustible = (Enumeraciones.CobroCombustible)row["eCobroCombustible"].S().I();
                    // TIEMPOS DE VUELO
                    odRem.dVueloCostoDirNal = row["VueloCostoDirNal"].S().D();
                    odRem.dVueloCostoDirInt = row["VueloCostoDirInt"].S().D();
                    // TIEMPOS DE ESPERA
                    odRem.bSeCobreEspera = row["SeCobraEspera"].S().B();
                    odRem.dTarifaNalEspera = row["TarifaNalEspera"].S().D();
                    odRem.dTarifaIntEspera = row["TarifaIntEspera"].S().D();
                    odRem.dPorTarifaNalEspera = row["PorTarifaNalEspera"].S().D();
                    odRem.dPorTarifaIntEspera = row["PorTarifaIntEspera"].S().D();
                    // PERNOCTAS
                    odRem.bSeCobraPernoctas = row["SeCobraPernoctas"].S().B();
                    odRem.dTarifaDolaresNal = row["PerTarifaDlsNal"].S().D();
                    odRem.dPorTarifaVueloNal = row["PerPorTarifaDlsNal"].S().D();
                    odRem.dTarifaDolaresInt = row["PerTarifaDlsInt"].S().D();
                    odRem.dPorTarifaVueloInt = row["PerPorTarifaDlsInt"].S().D();
                    // HELICOPTEROS
                    odRem.iCobraFerryHelicoptero = row["IdCobraFerryHelicoptero"].S().I();
                    odRem.iMasMinutosHelicoptero = row["MasMinutosHelicoptero"].S().I();

                    //=====---- DESCUENTOS ----=====//
                    // TIEMPOS DE ESPERA
                    odRem.bSeDescuentaEsperaNal = row["SeDescuentaEsperaNal"].S().B();
                    odRem.bSeDescuentaEsperaInt = row["SeDescuentaEsperaInt"].S().B();
                    odRem.dFactorEHrVueloNal = row["FactorEspHrVueloNal"].S().D();
                    odRem.dFactorEHrVueloInt = row["FactorEspHrVueloInt"].S().D();
                    // PERNOCTAS
                    odRem.bSeDescuentanPerNal = row["SeDescuentaPerNal"].S().B();
                    odRem.bSeDescuentanPerInt = row["SeDescuentaPerInt"].S().B();
                    odRem.dFactorConvHrVueloNal = row["PerFactorConvHrVueloNal"].S().D();
                    odRem.dFactorConvHrVueloInt = row["PerFactorConvHrVueloInt"].S().D();
                    odRem.iPernoctasLibreAnio = row["PernoctasLibresAnio"].S().I();

                    // VUELOS SIMULTANEOS
                    odRem.bPermiteVloSimultaneo = row.S("AplicaVloSimultaneo").B();
                    odRem.iCuantosVloSimultaneo = row.S("CuantosVloSimultaneo").I();
                    odRem.dFactorVloSimultaneo = row.S("FactorVloSimultaneo").D();
                }

                odRem.dtBases = ds.Tables[1];
                odRem.dtIntercambios = ds.Tables[2];
                odRem.dtTramosPactadosEsp = DBGetObtieneTramosPactadosContrato(iIdContrato).Copy();
                odRem.dtTramosPactadosGen = new DBTramoPactado().DBSearchObj("@GrupoModelo", odRem.sGrupoModeloDesc, 
                                                                            "@estatus", 1);
                DataTable dtRem = oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaRemisionId]", "@IdRemision", lIdRemision);
                if (dtRem.Rows.Count > 0)
                {
                    DataRow r = dtRem.Rows[0];
                    odRem.bAplicoFactorEspecial = r.S("AplicaFactorEspecial").B();
                    odRem.bAplicoIntercambio = r.S("Intercamio").B();
                    odRem.bAplicaGiraEspera = r.S("Gira").B();
                    odRem.bAplicaGiraHorario = r.S("GiraHorario").B();
                    odRem.bAplicaFactorFechaPico = r.S("AplicaFechaPico").B();
                    odRem.bAplicaFactorVueloSimultaneo = r.S("AplicaVueloSimultaneo").B();
                    odRem.bAplicaFactorTramoNacional = r.S("AplicaFactorTramoNal").B();
                    odRem.bAplicaFactorTramoInternacional = r.S("AplicaFactorTramoInt").B();

                    odRem.dFactorEspecialF = r.S("FactorEspecial").D();
                    odRem.dFactorFechaPicoF = r.S("FactorFechaPico").D();
                    odRem.dFactorGiraEsperaF = r.S("FactorGiraEspera").D();
                    odRem.dFactorGiraHorarioF = r.S("FactorGiraHorario").D();
                    odRem.dFactorIntercambioF = r.S("FactorIntercambio").D();

                    if (r.S("FactorVueloSimultaneo").D() > 0)
                        odRem.dFactorVloSimultaneo = r.S("FactorVueloSimultaneo").D();
                    if (r.S("FactorTramoNal").D() > 0)
                        odRem.dFactorTramosNal = r.S("FactorTramoNal").D();
                    if (r.S("FactorTramoInt").D() > 0)
                        odRem.dFactorTramosInt = r.S("FactorTramoInt").D();
                }
                
                return odRem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable DBGetObtieneTipoDestinoAeropuertoPorICAO(string sICAO)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaTipoDestinoICAO]", "@ICAO", sICAO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable DBGetConceptosRemision
        {
            get 
            {
                try
                {
                    return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaConceptosRemision]");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void DBSetInsertaImportesRemision(List<ImportesRemision> oLst)
        {
            try
            {
                int idRemision = 0;
                foreach (ImportesRemision o in oLst)
                {
                    oDB_SP.EjecutarSP("[Principales].[spI_MXJ_InsertaImportesRemision]", "@IdRemision", o.iIdRemision,
                                                                                        "@IdConcepto", o.iIdConcepto,
                                                                                        "@Cantidad", o.sCantidad,
                                                                                        "@TarifaDlls", o.dTarifaDlls,
                                                                                        "@Importe", o.dImporte,
                                                                                        "@HrDescontar", o.sHrDescontar,
                                                                                        "@UsuarioCreacion", o.sUsuario,
                                                                                        "@IP", o.sIP);
                    idRemision = o.iIdRemision.I();
                }
                new DBUtils().DBSaveBitacora(Convert.ToInt32(Enumeraciones.Pantallas.Remisiones), Convert.ToInt32(Enumeraciones.TipoOperacion.Insertar), "Se creo importe de la Remisión: " + idRemision.S());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable DBGetConsultaConceptosServiciosVuelo
        {
            get
            {
                try
                {
                    return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaServiciosCargoRemision]");
                }
                catch (Exception ex)
                {
                    return new DataTable();
                }
            }
        }

        public DataTable DBGetConsultaTarifasVuelo(int iIdContrato, long lIdRemision, string sAeropuertoICAO)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ObtieneTarifaVuelo]", "@IdContrato", iIdContrato,
                                                                                        "@IdRemision", lIdRemision,
                                                                                        "@AeropuertoICAO", sAeropuertoICAO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable DBGetServiciosCargoContrato(int iIdContrato, long iIdRemision)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaServiciosCContrato]", "@IdContrato", iIdContrato,
                                                                                                "@IdRemision", iIdRemision);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public DataTable DBGetImportesTUA(long iIdRemision)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaDatosCobrosServicios]", "@IdRemision", iIdRemision);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DBSetInsertaHeaderServiciosVuelo(ServiciosVueloH oSer)
        {
            try
            {
                oDB_SP.EjecutarSP("[Principales].[spI_MXJ_InsertaHeaderServiciosVuelo]", "@IdRemision", oSer.iIdRemision,
                                                                                        "@SubTotal", oSer.dSubtotal,
                                                                                        "@IVA", oSer.dIva,
                                                                                        "@Total", oSer.dTotal,
                                                                                        "@HrsDescontar", oSer.sHrDescontar,
                                                                                        "@TipoCambio", oSer.dTipoCambio,
                                                                                        "@FactorIVA", oSer.iFactorIva,
                                                                                        "@CombustibleNal", oSer.dCombustibleNal,
                                                                                        "@CombustibleInt", oSer.dCombustibleInt,
                                                                                        "@Usuario", oSer.sUsuario,
                                                                                        "@IP", oSer.sIP);
                new DBUtils().DBSaveBitacora(Convert.ToInt32(Enumeraciones.Pantallas.Remisiones), Convert.ToInt32(Enumeraciones.TipoOperacion.Insertar), "Se creó Header Servicios Vuelo de la Remisión: " + oSer.iIdRemision.S());
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public void DBSetInsertaDetalleServiciosVuelo(List<ServiciosVueloD> oLst)
        {
            try
            {
                long idRemision = 0;
                foreach (ServiciosVueloD oSer in oLst)
                {
                    oDB_SP.EjecutarSP("[Principales].[spI_MXJ_InsertaDetalleServiciosVuelo]", "@IdRemision", oSer.iIdRemision,
                                                                                                "@IdConcepto", oSer.iIdConcepto,
                                                                                                "@CostoDirecto", oSer.dCostoDirecto,
                                                                                                "@CostoComb", oSer.dCostoComb,
                                                                                                "@TarifaDlls", oSer.dTarifaDlls,
                                                                                                "@Cantidad", oSer.sCantidad,
                                                                                                "@ImporteDlls", oSer.dImporteDlls,
                                                                                                "@HrDescontar", oSer.sHrDescontar,
                                                                                                "@Usuario", oSer.sUsuario,
                                                                                                "@IP", oSer.sIP);
                    idRemision = oSer.iIdRemision;
                }
                new DBUtils().DBSaveBitacora(Convert.ToInt32(Enumeraciones.Pantallas.Remisiones), Convert.ToInt32(Enumeraciones.TipoOperacion.Insertar), "Se creó Detalle Servicios Vuelo de la Remisión: " + idRemision.S());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DBSetInsertaHeaderServiciosCargo(ServiciosCargoH oSer) 
        {
            try
            {
                oDB_SP.EjecutarSP("[Principales].[spI_MXJ_InsertaHeaderServiciosCargo]", "@IdRemision", oSer.iIdRemision,
                                                                                            "@SubTotal", oSer.dSubtotal,
                                                                                            "@FactorIVA", oSer.iFactorIVA,
                                                                                            "@IVA", oSer.dIva,
                                                                                            "@Total", oSer.dTotal,
                                                                                            "@Usuario", oSer.sUsuario,
                                                                                            "@FechaCreacion", DateTime.Now,
                                                                                            "@IP", oSer.sIP);
                new DBUtils().DBSaveBitacora(Convert.ToInt32(Enumeraciones.Pantallas.Remisiones), Convert.ToInt32(Enumeraciones.TipoOperacion.Insertar), "Se creó Header Servicios con Cargo de la Remisión: " + oSer.iIdRemision.S());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DBSetInsertaDetalleServiciosCargo(List<ServiciosCargoD> oLst)
        {
            try
            {
                long idRemision = 0;
                foreach (ServiciosCargoD oSer in oLst)
                {
                    oDB_SP.EjecutarSP("[Principales].[spI_MXJ_InsertaDetalleServiciosCargo]", "@IdRemision", oSer.iIdRemision,
                                                                                                "@IdServicioCargo", oSer.iIdServicioCargo,
                                                                                                "@Importe", oSer.dImporte,
                                                                                                "@Usuario", oSer.sUsuario,
                                                                                                "@IP", oSer.sIP);
                    idRemision = oSer.iIdRemision;
                }
                new DBUtils().DBSaveBitacora(Convert.ToInt32(Enumeraciones.Pantallas.Remisiones), Convert.ToInt32(Enumeraciones.TipoOperacion.Insertar), "Se creó Detalle Servicios con Cargo de la Remisión: " + idRemision.S());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable DBSetCambiaStatusRemision(RemisionDatosGrals oRem)
        {
            try
            {
                DataTable dt = oDB_SP.EjecutarDT("[Principales].[spU_MXJ_ActualizaStatusRemision]", "@IdRemision", oRem.iIdRemision,
                                                                                                    "@TotalTiempoCobrar", oRem.sTotalTiempoCobrar,
                                                                                                    "@Intercamio", oRem.bIntercambio,
                                                                                                    "@Gira", oRem.bGira,
                                                                                                    "@GiraHorario", oRem.bGiraHorario,
                                                                                                    "@AplicaFechaPico", oRem.bAplicaHoraPico,
                                                                                                    "@AplicaVueloSimultaneo", oRem.bAplicaVueloSimultaneo,
                                                                                                    "@AplicaFactorEspecial", oRem.bAplicaFactorEspecual,
                                                                                                    "@AplicaTramoNal", oRem.bAplicaFactorTramoNal,
                                                                                                    "@AplicaTramoInt", oRem.bAplicaFactorTramoInt,

                                                                                                    "@FactorIntercambio", oRem.dFactorIntercambio,
                                                                                                    "@FactorGiraHorario", oRem.dFactorGiraHorario,
                                                                                                    "@FactorGiraEspera", oRem.dFactorGiraEspera,
                                                                                                    "@FactorFechaPico", oRem.dFactorFechaPico,
                                                                                                    "@FactorVueloSimultaneo", oRem.dFactorVueloSimultaneo,
                                                                                                    "@FactorTramoNal", oRem.dFactorTramoNal,
                                                                                                    "@FactorTramoInt", oRem.dFactorTramoInt,

                                                                                                    "@TotalRemisionPesos", oRem.dTotalRemisionPesos,
                                                                                                    "@TotalRemisionDlls", oRem.dTotalRemisionDlls,
                                                                                                    "@Status", oRem.iStatus,
                                                                                                    "@Usuario", oRem.sUsuario,
                                                                                                    "@IP", oRem.sIP);

                new DBUtils().DBSaveBitacora(Convert.ToInt32(Enumeraciones.Pantallas.Remisiones), Convert.ToInt32(Enumeraciones.TipoOperacion.Actualizar), "Se modificó el estatus de la Remisión: " + oRem.iIdRemision.S());

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Remision DBGetRemisionId(long iIdRemision)
        {
            try
            {
                Remision oRem = null;
                DataTable dtRes = oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaRemisionId]", "@IdRemision", iIdRemision);

                if (dtRes.Rows.Count > 0)
                {
                    oRem = dtRes.AsEnumerable().Select(r => new Remision()
                                    {
                                        iIdRemision = r["IdRemision"].S().I(),
                                        iIdCliente = r["IdCliente"].S().I(),
                                        iIdContrato = r["IdContrato"].S().I(),
                                        sContrato = r.S("ClaveContrato"),
                                        sCliente = r.S("CodigoCliente"),
                                        dtFechaRemision = r["Fecha"].S().Dt(),
                                        sGrupoModeloDesc = r.S("GrupoModeloDesc"),
                                        bAplicaIntercambio = r["AplicaIntercambio"].S().B(),
                                        dFactorEspecial = r["FactorEspecial"].S().D(),
                                        iStatus = r["Status"].S().I()
                                    }).First();
                }

                return oRem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string DBGetIATAporICAO(string sICAO)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaIATAporICAO]", "@ICAO", sICAO).Rows[0][0].S();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public string DBGetICAOporIATA(string sIATA)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaICAOporIATA]", "@IATA", sIATA).Rows[0][1].S();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet DBGetAeropuertosFerrysVirtuales(string sICAO, int iIdAeroNeg)
        {
            try
            {
                return oDB_SP.EjecutarDS("[Principales].[spS_MXJ_ConsultaAeropuertosFerryVirtual]", "@ICAO", sICAO,
                                                                                                    "@IdAeN", iIdAeroNeg);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public void DBSetInsertaTramosRemisionReales(DataTable dt, long iIdRemision)
        {
            try
            {
                oDB_SP.EjecutarSP("[Principales].[spD_MXJ_EliminaTramosExistentes]", "@IdRemision", iIdRemision);
                
                int iCont = 0;
                foreach(DataRow row in dt.Rows)
                {
                    iCont++;
                    oDB_SP.EjecutarDT("[Principales].[spI_MXJ_InsertaTramosRemision]", "@IdRemision", iIdRemision,
                                                                                        "@IdTramo", row["IdTramo"].S().I(),
                                                                                        "@NoTramo", iCont,
                                                                                        "@Matricula", row["Matricula"].S(),
                                                                                        "@OrigenIATA", row["Origen"].S(),
                                                                                        "@DestinoIATA", row["Destino"].S(),
                                                                                        "@OrigenICAO", row["OrigenICAO"].S(),
                                                                                        "@DestinoICAO", row["DestinoICAO"].S(),
                                                                                        "@FechaSalida", row["FechaSalida"].S().Dt(),
                                                                                        "@FechaLlegada", row["FechaLlegada"].S().Dt(),
                                                                                        "@Pax", row["CantPax"].S().I(),
                                                                                        "@TotalTiempoCalzo", row["TotalTiempoCalzo"].S(),
                                                                                        "@TotalTiempoVuelo", row["TotalTiempoVuelo"].S(),
                                                                                        "@TiempoCobrar", row["TiempoCobrar"].S(),
                                                                                        "@TiempoEspera", row["TiempoEspera"].S(),
                                                                                        "@TiempoOriginal", row["TiempoOriginal"].S(),
                                                                                        "@IdTipoPierna", row["IdTipoPierna"].S().I(),
                                                                                        "@RealVirtual", row["RealVirtual"].S(),
                                                                                        "@SeCobra", row["SeCobra"].S().I(),
                                                                                        "@Usuario", row["Usuario"].S());                   
                }

                new DBUtils().DBSaveBitacora(Convert.ToInt32(Enumeraciones.Pantallas.Remisiones), Convert.ToInt32(Enumeraciones.TipoOperacion.Insertar), "Se creó tramos remisión reales de la Remisión: " + iIdRemision.S());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable DBGetConsultaTramosRemisionExistentes(long iIdRemision)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaTramosRemisionExistentes]", "@IdRemision", iIdRemision);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable DBGetConsultaAeropuertoId(int iIdAeropuerto)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaAeropuertoID]", "@idAeropuert", iIdAeropuerto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet DBGetConsultaIntercambiosVueloRemision(long iIdRemision)
        {
            try
            {
                return oDB_SP.EjecutarDS("[Principales].[spI_MXJ_ConsultaDatosIntercambioRemision]", "@IdRemision", iIdRemision);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable DBGetObtieneFechasPicoPorAnio(int iAnio, int iMes)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ObtieneFechasPicoAnio]", "@Anio", iAnio, "@Mes", iMes);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable DBGetConsultaVuelosDelMes(int iContrato, int iAnio, int iMes, long lIdRemision)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ObtieneBitacorasContratoPeriodo]", "@VueloContratoId", iContrato,
                                                                                                    "@Mes", iMes,
                                                                                                    "@Anio", iAnio,
                                                                                                    "@Remision",lIdRemision );
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public DataTable DBGetConsultaBitacoraUnVuelo(int iIdContrato, int iFolioReal, string sMatricula, int iTrip)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaBitacoraUnVuelo]", "@VueloContratoId", iIdContrato,
                                                                                            "@FolioReal", iFolioReal,
                                                                                            "@Matricula", sMatricula,
                                                                                            "@Trip", iTrip);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet DBGetConsultaNotasYCasosBitacora(long lBitacora)
        {
            try
            {
                return oDB_SP.EjecutarDS("[Principales].[spS_MXJ_ConsultaNotasCasosContrato]", "@IdBitacora", lBitacora);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string DBGetTipoPiernaContrato(int iIdcontrato)
        {
            try
            {
                object ob = oDB_SP.EjecutarValor("[Principales].[spS_MXJ_ConsultaTipoPiernaContrato]", "@IdContrato", iIdcontrato);

                return ob != null ? ob.S() : "0";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable DBGetObtieneTramosPactadosGeneralesMatricula(string sMatricula)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaTramosPactadosMatricula]", "@Matricula", sMatricula);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable DBGetObtieneTramosPactadosGeneralesGrupoModelo(int iIdGrupoModelo)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaTramosPactadosGrupoModelo]", "@IdGrupoModelo", iIdGrupoModelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBCancelaRemision(Remision oRem)
        {
            try
            {
                object oRes = oDB_SP.EjecutarValor("[Principales].[spU_MXJ_CancelaRemisiones]", "@IdRemision", oRem.iIdRemision);
                if (oRes.S() == "SE CANCELO LA REMISION EXITOSAMENTE")
                {
                    new DBUtils().DBSaveBitacora(Convert.ToInt32(Enumeraciones.Pantallas.Remisiones), Convert.ToInt32(Enumeraciones.TipoOperacion.Actualizar), "Se canceló la Remisión: " + oRem.iIdRemision.S());
                    return 1;
                }
                else
                    return -1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable DBGetClientesCombo
        {
            get
            {
                try
                {
                    return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaClienteDDL]", "@status", 1);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataSet DBGetTotalesRemisionTerminada(long lIdRemision)
        {
            try
            {
                return oDB_SP.EjecutarDS("[Principales].[spS_MXJ_ConsultaTotalesRemision]", "@IdRemision", lIdRemision);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBGetObtienePernoctasLibres(int iIdContrato)
        {
            try
            {
                object oResult = oDB_SP.EjecutarValor("[Principales].[spS_MXJ_ConsultaPernoctasLibres]", "@IdContrato", iIdContrato);

                return oResult != null ? oResult.S().I() : -1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public long DBSetActualizaPernoctasDisponibles(int iIdContrato, int iNoPernoctas)
        {
            try
            {
                object oResult = oDB_SP.EjecutarValor("[Principales].[spU_MXJ_ActualizaPernoctasLibres]", "@IdContrato", iIdContrato,
                                                                                                            "@Pernoctas", iNoPernoctas);

                return oResult != null ? oResult.S().L() : -1;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public void DBSetActualizaFactoresRemision(DatosRemision oRem)
        {
            try
            {
                oDB_SP.EjecutarSP("[Principales].[spU_MXJ_ActualizaFactoresRemision]", "@IdRemision", oRem.lIdRemision,
                                                                                        "@GiraHorario", oRem.bAplicaGiraHorario,
                                                                                        "@Intercamio", oRem.bAplicoIntercambio,
                                                                                        "@Gira", oRem.bAplicaGiraEspera,
                                                                                        "@AplicaFechaPico", oRem.bAplicaFactorFechaPico,
                                                                                        "@VueloSimultaneo", oRem.bAplicaFactorVueloSimultaneo,
                                                                                        "@TramoNacional", oRem.bAplicaFactorTramoNacional,
                                                                                        "@TramoInternacional", oRem.bAplicaFactorTramoInternacional,
                                                                                        "@FactorGiraHorario", oRem.dFactorGiraHorarioF,
                                                                                        "@FactorIntercambio", oRem.dFactorIntercambioF,
                                                                                        "@FactorGiraEspera", oRem.dFactorGiraEsperaF,
                                                                                        "@FactorFechaPico", oRem.dFactorFechaPicoF,
                                                                                        "@FactorVueloSimultaneo", oRem.dFactorVloSimultaneo,
                                                                                        "@FactorTramoNal", oRem.dFactorTramosNal,
                                                                                        "@FactorTramoInt", oRem.dFactorTramosInt);

                new DBUtils().DBSaveBitacora(Convert.ToInt32(Enumeraciones.Pantallas.Remisiones), Convert.ToInt32(Enumeraciones.TipoOperacion.Actualizar), "Se modificaron los factores de la Remisión: " + oRem.lIdRemision.S());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DBSetCreaRemisionAPartirCotizacion(int iIdPresupueto, long lIdRemision)
        {
            try
            {
                oDB_SP.EjecutarSP("[Principales].[spI_MXJ_CreaRemisionCotizacion]", "@IdPresupuesto", iIdPresupueto,
                                                                                    "@IdRemision", lIdRemision,
                                                                                    "@Usuario", Utils.GetUser,
                                                                                    "@IP", Utils.GetIPAddress());

                new DBUtils().DBSaveBitacora(Convert.ToInt32(Enumeraciones.Pantallas.Remisiones), Convert.ToInt32(Enumeraciones.TipoOperacion.Actualizar), "Se creó cotización de la Remisión: " + lIdRemision.S());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string DBGetConsultaComisariatoRemision(long lIdRemision)
        {
            try
            {
                object oRes = oDB_SP.EjecutarValor("[Principales].[spS_MXJ_ConsultaComisariatoRemision]", "@IdRemision", lIdRemision);

                return oRes != null ? oRes.S() : "0.00";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable DBGetObtieneMatriculasHelicoptero
        {
            get
            {
                try
                {
                    return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaMatriculasHelicopteros]");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        /// <summary>
        /// Obtiene el importe de los aterrizajes de todo un vuelo
        /// </summary>
        /// <param name="iTipoDocumento">Representa el documento: 1.- Remisión 2.- Cotización</param>
        /// <param name="iIdDocumento">Folio del documento</param>
        /// <param name="dTipoCambio">Tipo de Cambio del día</param>
        /// <returns></returns>
        public DataTable DBGetObtieneAterrizajesPorDocumento(int iTipoDocumento, int iIdDocumento, decimal dTipoCambio)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ObtieneCostosAterrizajes]", "@TipoDoc", iTipoDocumento,
                                                                                            "@IdDoc", iIdDocumento,
                                                                                            "@TipoCambio", dTipoCambio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable DBGetBitacorasPorRemision(long lIdRemision)
        {
            try
            {
                return oDB_SP.EjecutarDT("Principales.spS_MXJ_ObtieneBitacorasPorRemision", "@IdRemision", lIdRemision);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DBSetInsertaSnapshotRemision(List<FactoresTramoSnapshot> oLfts, DatosRemision DT)
        {
            try
            {
                oDB_SP.EjecutarSP("[Principales].[spI_MXJ_InsertaSnapshotRemision]", "@IdRemision", DT.lIdRemision,
                                                                                     "@iCobroTiempo", DT.iCobroTiempo,
                                                                                     "@MasMinutos", DT.iMasMinutos,
                                                                                     "@AplicaTramosPactados", DT.bTiemposPactados == true ? 1 : 0,
                                                                                     "@IdGrupoModeloContratado", DT.iIdGrupoModelo,
                                                                                     "@HorasContratadasTotal", DT.iHorasContratadasTotal,
                                                                                     "@HorasContratadasAnio", DT.iHorasContratadasAnio,
                                                                                     "@SeCobraCombustible", DT.bSeCobraCombustible,
                                                                                     "@eCobroCombustible", DT.eCobroCombustible,
                                                                                     "@FactorTramosNal", DT.dFactorTramosNal,
                                                                                     "@FactorTramosInt", DT.dFactorTramosInt,
                                                                                     "@VueloCostoDirNal", DT.dVueloCostoDirNal,
                                                                                     "@VueloCostoDirInt", DT.dVueloCostoDirInt,
                                                                                     "@SeCobraEspera", DT.bSeCobreEspera == true ? 1 : 0,
                                                                                     "@TarifaNalEspera", DT.dTarifaNalEspera,
                                                                                     "@TarifaIntEspera", DT.dTarifaIntEspera,
                                                                                     "@PorTarifaNalEspera", DT.dPorTarifaNalEspera,
                                                                                     "@PorTarifaIntEspera", DT.dPorTarifaIntEspera,
                                                                                     "@SeCobraPernoctas", DT.bSeCobraPernoctas == true ? 1 : 0,
                                                                                     "@PerTarifaDlsNal", DT.dTarifaDolaresNal,
                                                                                     "@PerPorTarifaDlsNal", DT.dPorTarifaVueloNal,
                                                                                     "@PerTarifaDlsInt", DT.dTarifaDolaresInt,
                                                                                     "@PerPorTarifaDlsInt", DT.dPorTarifaVueloInt,
                                                                                     "@AplicaVloSimultaneo", DT.bPermiteVloSimultaneo == true ? 1 : 0,
                                                                                     "@CuantosVloSimultaneo", DT.iCuantosVloSimultaneo,
                                                                                     "@FactorVloSimultaneo", DT.dFactorVloSimultaneo,
                                                                                     "@SeDescuentaEsperaNal", DT.bSeDescuentaEsperaNal,
                                                                                     "@SeDescuentaEsperaInt", DT.bSeDescuentaEsperaInt,
                                                                                     "@FactorEspHrVueloNal", DT.dFactorConvHrVueloNal,
                                                                                     "@FactorEspHrVueloInt", DT.dFactorConvHrVueloInt,
                                                                                     "@SeDescuentaPerNal", DT.bSeDescuentanPerNal == true ? 1 : 0,
                                                                                     "@SeDescuentaPerInt", DT.bSeDescuentanPerInt == true ? 1 : 0,
                                                                                     "@PerFactorConvHrVueloNal", DT.dFactorConvHrVueloNal,
                                                                                     "@PerFactorConvHrVueloInt", DT.dFactorConvHrVueloInt,
                                                                                     "@UsuarioCreacion", Utils.GetUser);

                long idRemision = 0;
                foreach (FactoresTramoSnapshot oSer in oLfts)
                {
                    oDB_SP.EjecutarSP("[Principales].[spI_MXJ_InsertaFactoresTramosSnapshot]", "@IdRemision", DT.lIdRemision,
                                                                                                "@Matricula", oSer.sMatricula,
                                                                                                "@Origen", oSer.sOrigen,
                                                                                                "@Destino", oSer.sDestino,
                                                                                                "@TiempoOriginal", oSer.sTiempoOriginal,
                                                                                                "@TiempoFinal", oSer.sTiempoFinal,
                                                                                                "@FactorEspecialRem", oSer.dFactorEspeciaRem,
                                                                                                "@FactorTramoInt", oSer.dAplicaFactorTramoInternacional,
                                                                                                "@FactorTramoNal", oSer.dAplicaFactorTramoNacional,
                                                                                                "@FactorIntercambio", oSer.dAplicoIntercambio,
                                                                                                "@FactorGiraEspera", oSer.dAplicaGiraEspera,
                                                                                                "@FactorGiraHorario", oSer.dAplicaGiraHorario,
                                                                                                "@FactorFechaPico", oSer.dAplicaFactorFechaPico,
                                                                                                "@FactorVloSimultaneo", oSer.dAplicaFactorVueloSimultaneo,
                                                                                                "@UsuarioCreacion", Utils.GetUser);

                    idRemision = DT.lIdRemision;
                }
                new DBUtils().DBSaveBitacora(Convert.ToInt32(Enumeraciones.Pantallas.Remisiones), Convert.ToInt32(Enumeraciones.TipoOperacion.Insertar), "Se creó Detalle Servicios Vuelo de la Remisión: " + idRemision.S());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string DBSetInsertaRemisionKardex(List<KardexRemision> oLstKdx)
        {
            try
            {
                string sRes = string.Empty;
                object oRes = new object();

                foreach (KardexRemision oRem in oLstKdx)
                {
                    oRes = oDB_SP.EjecutarValor("[Principales].[spI_MXJ_KardexHoras]", "@IdContrato", oRem.IIdContrato,
                                                                                          "@Matricula", oRem.SMatricula,
                                                                                          "@IdRemision", oRem.IIdRemision,
                                                                                          "@Cargo", oRem.SCargo,
                                                                                          "@Abono", oRem.SAbono,
                                                                                          "@IdMotivo", oRem.IIdMotivo,
                                                                                          "@Notas", oRem.SNotas,
                                                                                          "@Usuario", oRem.SUsuario);
                    new DBUtils().DBSaveBitacora(Convert.ToInt32(Enumeraciones.Pantallas.Remisiones), Convert.ToInt32(Enumeraciones.TipoOperacion.Actualizar), "Se inserto registro en el kardex del contrato: " + oRem.IIdContrato.S() + ", y IdMotivo: " + oRem.IIdMotivo.S());
                }
                return oRes.I().S();
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
        public DataTable DBGetObtieneHorasContratadas(int iIdContrato)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_KardexHorasContratadas]", "@IdContrato", iIdContrato);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable DBGetMotivos()
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaMotivosRemisiones]");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBSetInsertaAjuste(AjusteRemision oA)
        {
            try
            {
                object oRes = new object();
                
                oRes = oDB_SP.EjecutarValor("[Principales].[spI_MXJ_InsertaAjusteRemision]", "@IdRemision", oA.IIdRemision,
                                                                                             "@IdMotivo", oA.IIdMotivo,
                                                                                             "@Horas", oA.SHoras,
                                                                                             "@Comentarios", oA.SComentarios,
                                                                                             "@Estatus", oA.IEstatus,
                                                                                             "@Usuario", oA.SUsuario);                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  

                return oRes.I();
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int DBSetActualizaAutorizacionAjuste(AjusteRemision oA)
        {
            try
            {
                object oRes = new object();

                oRes = oDB_SP.EjecutarValor("[Principales].[spU_MXJ_ActualizaAjusteRemision]", "@IdRemision", oA.IIdRemision,
                                                                                               "@Estatus", oA.IEstatus,
                                                                                               "@UsuarioAut", oA.SUsuarioAutorizador);

                return oRes.I();
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public DataSet DBGetAjusteRemision(int iIdRemision)
        {
            try
            {
                DataSet ds = new DataSet();

                ds = oDB_SP.EjecutarDS("[Principales].[spS_MXJ_ConsultaAjusteRemision]", "@IdRemision", iIdRemision);

                return ds;
            }
            catch (Exception ex)
            {
                return null;
            }
        }



    }
}