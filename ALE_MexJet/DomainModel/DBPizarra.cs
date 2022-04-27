using ALE_MexJet.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using NucleoBase.Core;

namespace ALE_MexJet.DomainModel
{
    public class DBPizarra : DBBase
    {
        public DataTable DBGetObtieneTitulosPizarra()
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ObtieneTitulosPizarraElectronica]");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SnapshotRemision DBGetObtieneSnapshotRemision(long iIdRemision)
        {
            try
            {
                SnapshotRemision oSnap = new SnapshotRemision();
                DatosRemision oDatRem = new DatosRemision();

                DataSet ds = oDB_SP.EjecutarDS("[Principales].[spS_MXJ_ConsultaSnapshotRemision]", "@IdRemision", iIdRemision);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataRow row = ds.Tables[0].Rows[0];
                    oDatRem.lIdRemision = row["IdRemision"].S().L();
                    oDatRem.sTotalTiempoCobrar = row["ToTalTiempoCobrar"].S();
                    oDatRem.sClaveContrato = row["ClaveContrato"].S();
                    oDatRem.sClaveCliente = row["CodigoCliente"].S();
                    oDatRem.sTipoPaquete = row["TipoPaquete"].S();
                    oDatRem.iIdContrato = row["IdContrato"].S().I();
                    oDatRem.iCobroTiempo = row["iCobroTiempo"].S().I();  // 1.- Tiempo Vuelo 2.- TiempoCalzo
                    //oDatRem.eSeCobraFerry = (Enumeraciones.SeCobraFerrys)row["SeCobraFerry"].S().I();
                    oDatRem.dFactorTramosNal = row["FactorTramosNal"].S().D();
                    oDatRem.dFactorTramosInt = row["FactorTramosInt"].S().D();
                    //oDatRem.bAplicaEsperaLibre = row["EsperaLibre"].S() == "1" ? true : false;
                    //oDatRem.dHorasPorVuelo = row["HorasPorVuelo"].S().D();
                    //oDatRem.dFactorHrVuelo = row["FactorHrVuelo"].S().D();
                    oDatRem.iMasMinutos = row["MasMinutos"].S().I();
                    //oDatRem.sRuta = row["Ruta"].S();
                    //oDatRem.bTiemposPactados = row["TramosPactados"].S().B();
                    //oDatRem.iHorasPernocta = row["HorasPernocta"].S().I();
                    //oDatRem.iFactosTiempoEsp = row["FactorTiempoEspera"].S().I();
                    oDatRem.iIdGrupoModelo = row["IdGrupoModeloContratado"].S().I();
                    oDatRem.sGrupoModeloDesc = row.S("GrupoModeloContratadoDesc");
                    //oDatRem.dTipoCambio = row["TipoCambio"].S().D();
                    //oDatRem.sTipoPaquete = row.S("PaqueteDesc");
                    //oDatRem.sFechaInicio = row.S("FechaInicioVuelo");
                    oDatRem.iHorasContratadasTotal = row.S("HorasContratadasTotal").I();
                    oDatRem.iHorasContratadasAnio = row.S("HorasContratadasAnio").I();

                    // =====---- COBROS ----=====//
                    // COMBUSTIBLE
                    oDatRem.bSeCobraCombustible = row["SeCobraCombustible"].S().B();
                    oDatRem.eCobroCombustible = (Enumeraciones.CobroCombustible)row["eCobroCombustible"].S().I();
                    // TIEMPOS DE VUELO
                    oDatRem.dVueloCostoDirNal = row["VueloCostoDirNal"].S().D();
                    oDatRem.dVueloCostoDirInt = row["VueloCostoDirInt"].S().D();
                    // TIEMPOS DE ESPERA
                    oDatRem.bSeCobreEspera = row["SeCobraEspera"].S().B();
                    oDatRem.dTarifaNalEspera = row["TarifaNalEspera"].S().D();
                    oDatRem.dTarifaIntEspera = row["TarifaIntEspera"].S().D();
                    oDatRem.dPorTarifaNalEspera = row["PorTarifaNalEspera"].S().D();
                    oDatRem.dPorTarifaIntEspera = row["PorTarifaIntEspera"].S().D();
                    // PERNOCTAS
                    oDatRem.bSeCobraPernoctas = row["SeCobraPernoctas"].S().B();
                    oDatRem.dTarifaDolaresNal = row["PerTarifaDlsNal"].S().D();
                    oDatRem.dPorTarifaVueloNal = row["PerPorTarifaDlsNal"].S().D();
                    oDatRem.dTarifaDolaresInt = row["PerTarifaDlsInt"].S().D();
                    //oDatRem.dPorTarifaVueloInt = row["PerPorTarifaDlsInt"].S().D();

                    //=====---- DESCUENTOS ----=====//
                    // TIEMPOS DE ESPERA
                    oDatRem.bSeDescuentaEsperaNal = row["SeDescuentaEsperaNal"].S().B();
                    oDatRem.bSeDescuentaEsperaInt = row["SeDescuentaEsperaInt"].S().B();
                    oDatRem.dFactorEHrVueloNal = row["FactorEspHrVueloNal"].S().D();
                    oDatRem.dFactorEHrVueloInt = row["FactorEspHrVueloInt"].S().D();
                    // PERNOCTAS
                    oDatRem.bSeDescuentanPerNal = row["SeDescuentaPerNal"].S().B();
                    oDatRem.bSeDescuentanPerInt = row["SeDescuentaPerInt"].S().B();
                    oDatRem.dFactorConvHrVueloNal = row["PerFactorConvHrVueloNal"].S().D();
                    oDatRem.dFactorConvHrVueloInt = row["PerFactorConvHrVueloInt"].S().D();
                    //oDatRem.iPernoctasLibreAnio = row["PernoctasLibresAnio"].S().I();

                    // VUELOS SIMULTANEOS
                    oDatRem.bPermiteVloSimultaneo = row.S("AplicaVloSimultaneo").B();
                    oDatRem.iCuantosVloSimultaneo = row.S("CuantosVloSimultaneo").I();
                    oDatRem.dFactorVloSimultaneo = row.S("FactorVloSimultaneo").D();

                    if (ds.Tables[1] != null)
                    {
                        foreach (DataRow item in ds.Tables[1].Rows)
                        {
                            FactoresTramoSnapshot oFact = new FactoresTramoSnapshot();
                            oFact.iNoTramo = item["iNoTramo"].S().I();
                            oFact.iIdRemision = row["IdRemision"].S().L();
                            oFact.sMatricula = item.S("Matricula");
                            oFact.sOrigen = item.S("Origen");
                            oFact.sDestino = item.S("Destino");
                            oFact.sTiempoOriginal = item.S("TiempoOriginal");
                            oFact.sTiempoFinal = item.S("TiempoFinal");
                            oFact.dFactorEspeciaRem = item.S("FactorEspecialRem").Db();
                            oFact.dAplicaFactorTramoInternacional = item.S("FactorTramoInt").Db();
                            oFact.dAplicaFactorTramoNacional = item.S("FactorTramoNal").Db();
                            oFact.dAplicoIntercambio = item.S("FactorIntercambio").Db();
                            oFact.dAplicaGiraEspera = item.S("FactorGiraEspera").Db();
                            oFact.dAplicaGiraHorario = item.S("FactorGiraHorario").Db();
                            oFact.dAplicaFactorFechaPico = item.S("FactorFechaPico").Db();
                            oFact.dAplicaFactorVueloSimultaneo = item.S("FactorVloSimultaneo").Db();

                            oSnap.oFactoresTramos.Add(oFact);
                        }
                    }


                    oSnap.oDatosRem = oDatRem;
                }
                
                return oSnap;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}