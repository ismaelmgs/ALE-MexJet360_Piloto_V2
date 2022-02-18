using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ALE_MexJet.Objetos;
using System.Data;
using ALE_MexJet.Clases;
using NucleoBase.Core;
using NucleoBase.BaseDeDatos;

namespace ALE_MexJet.DomainModel
{
    public class DBEstadoCuenta : DBBase
    {
        public DataTable dtObjCliente()
        {
            try
            {
                DocPendientes B = new DocPendientes();
                return oDB_SP.EjecutarDT("Catalogos.spS_MXJ_ConsultaClienteDDL", "@status", 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable dtObjContrato(params object[] oArrFiltros)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaContratosDDL]", oArrFiltros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable dtObjVuelosHead(params object[] oArrVuelosHead)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[SpS_MXJ_ConsultaEstadoCuentaVuelosCliente]", oArrVuelosHead);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable dtObjHeadEdoCnta(params object[] oArrVuelosHead)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[SpS_MXJ_ConsultaHeadEdoCnta]", oArrVuelosHead);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable dtObjVuelosDetail(params object[] oArrVuelosDetail)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[SpS_MXJ_ConsultaEstadoCuentaVuelosClienteFactor]", oArrVuelosDetail);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable dtObjVuelosEquiposDiferentes(params object[] oArrVuelosEqDif)
        {
            try
           {
                return oDB_SP.EjecutarDT("[Principales].[SpS_MXJ_ConsultaEstadoCuentaVuelosEquiposDiferentes]", oArrVuelosEqDif);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable dtObjVuelosDetail2(int iIdContrato, string dtIni, string dtFin)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[SpS_MXJ_ConsultaEstadoCuentaVuelosClienteFactor]", "@IdContrato", iIdContrato,
                                                                                                            "@FechaInicio", dtIni,
                                                                                                            "@FechaFin", dtFin);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ReporteEdoCuent DBGetObtieneSaldosIniciales(string sContrato, DateTime dtFechaInicio, DateTime dtFechaFin)
        {
            try
            {
                ReporteEdoCuent oRes = new ReporteEdoCuent();
                DataSet ds = oDB_SP.EjecutarDS("[Principales].[spS_MXJ_ConsultaSaldosInicialesyGI]",  "@ClaveCliente", sContrato, 
                                                                                                "@FechaInicio", dtFechaInicio);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    
                    // SALDOS
                    oRes.dAnticipoInicialSP = dr["AnticipoInicialSP"].S().D();
                    oRes.dQuintoAnioSP = dr["QuintoAnioSP"].S().D();
                    oRes.dFijoAnualSP = dr["FijoAnualSP"].S().D();
                    oRes.dIvaSP = dr["IvaSP"].S().D();
                    oRes.dGastosSP = dr["GastosSP"].S().D();
                    oRes.dServiciosConCargoSP = dr["ServicioConCargoSP"].S().D();
                    oRes.dVuelosSP = dr["VuelosSP"].S().D();

                    oRes.dAnticipoInicialSD = dr["AnticipoInicialSD"].S().D();
                    oRes.dQuintoAnioSD = dr["QuintoAnioSD"].S().D();
                    oRes.dFijoAnualSD = dr["FijoAnualSD"].S().D();
                    oRes.dIvaSD = dr["IvaSD"].S().D();
                    oRes.dGastosSD = dr["GastosSD"].S().D();
                    oRes.dServiciosConCargoSD = dr["ServicioConCargoSD"].S().D();
                    oRes.dVuelosSD = dr["VuelosSD"].S().D();

                    // PAGOS
                    oRes.dAnticipoInicialPP = dr["AnticipoInicialPP"].S().D();
                    oRes.dQuintoAnioPP = dr["QuintoAnioPP"].S().D();
                    oRes.dFijoAnualPP = dr["FijoAnualPP"].S().D();
                    oRes.dIvaPP = dr["IvaPP"].S().D();
                    oRes.dGastosPP = dr["GastosPP"].S().D();
                    oRes.dServiciosConCargoPP = dr["ServicioConCargoPP"].S().D();
                    oRes.dVuelosPP = dr["VuelosPP"].S().D();

                    oRes.dAnticipoInicialPD = dr["AnticipoInicialPD"].S().D();
                    oRes.dQuintoAnioPD = dr["QuintoAnioPD"].S().D();
                    oRes.dFijoAnualPD = dr["FijoAnualPD"].S().D();
                    oRes.dIvaPD = dr["IvaPD"].S().D();
                    oRes.dGastosPD = dr["GastosPD"].S().D();
                    oRes.dServiciosConCargoPD = dr["ServicioConCargoPD"].S().D();
                    oRes.dVuelosPD = dr["VuelosPD"].S().D();
                }

                if (ds.Tables[1].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[1].Rows[0];
                    // DOLARES
                    oRes.dAnticipoInicialGIAD = dr["AnticipoInicialD"].S().D();
                    oRes.dQuintoAnioGIAD = dr["QuintoAnioD"].S().D();
                    oRes.dFijoAnualGIAD = dr["FijoAnualD"].S().D();
                    oRes.dIvaGIAD = dr["IvaD"].S().D();
                    oRes.dGastosGIAD = dr["GastosD"].S().D();
                    oRes.dServiciosConCargoGIAD = dr["ServiciosCargoD"].S().D();
                    oRes.dVuelosGIAD = dr["VuelosD"].S().D();
                    // PESOS
                    oRes.dAnticipoInicialGIAP = dr["AnticipoInicialP"].S().D();
                    oRes.dQuintoAnioGIAP = dr["QuintoAnioP"].S().D();
                    oRes.dFijoAnualGIAP = dr["FijoAnualP"].S().D();
                    oRes.dIvaGIAP = dr["IvaP"].S().D();
                    oRes.dGastosGIAP = dr["GastosP"].S().D();
                    oRes.dServiciosConCargoGIAP = dr["ServiciosCargoP"].S().D();
                    oRes.dVuelosGIAP = dr["VuelosP"].S().D();
                }

                //DataTable dtGIP = oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaGastosInternosPeriodo]", "@ClaveCliente", sContrato,
                //                                                                                            "@FechaInicio", dtFechaInicio,
                //                                                                                            "@FechaFin", dtFechaFin);

                //if (dtGIP.Rows.Count > 0)
                //{
                //    DataRow dr = dtGIP.Rows[0];
                //    oRes.dAnticipoInicialGIPD = dr["AnticipoInicialD"].S().D();
                //    oRes.dQuintoAnioGIPD = dr["QuintoAnioD"].S().D();
                //    oRes.dFijoAnualGIPD = dr["FijoAnualD"].S().D();
                //    oRes.dIvaGIPD = dr["IvaD"].S().D();
                //    oRes.dGastosGIPD = dr["GastosD"].S().D();
                //    oRes.dServiciosConCargoGIPD = dr["ServiciosCargoD"].S().D();
                //    oRes.dVuelosGIPD = dr["VuelosD"].S().D();

                //    oRes.dAnticipoInicialGIPP = dr["AnticipoInicialP"].S().D();
                //    oRes.dQuintoAnioGIPP = dr["QuintoAnioP"].S().D();
                //    oRes.dFijoAnualGIPP = dr["FijoAnualP"].S().D();
                //    oRes.dIvaGIPP = dr["IvaP"].S().D();
                //    oRes.dGastosGIPP = dr["GastosP"].S().D();
                //    oRes.dServiciosConCargoGIPP = dr["ServiciosCargoP"].S().D();
                //    oRes.dVuelosGIPP = dr["VuelosP"].S().D();
                //}

                return oRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable DBGetObtieneGIdelPeriodo(int iIdContrato, DateTime dtFechaInicio, DateTime dtFechaFin)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaGastosInternosPeriodo]", "@ClaveCliente", iIdContrato,
                                                                                                    "@FechaInicio", dtFechaInicio,
                                                                                                    "@FechaFin", dtFechaFin);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}