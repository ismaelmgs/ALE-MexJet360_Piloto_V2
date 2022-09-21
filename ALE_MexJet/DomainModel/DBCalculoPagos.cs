﻿using ALE_MexJet.Objetos;
using NucleoBase.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ALE_MexJet.DomainModel
{
    public class DBCalculoPagos
    {
        public DataTable GetConceptos()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = new DBBase().oDB_SP.EjecutarDT("[VB].[spS_MXJ_ConsultaConceptos]");
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetPilotosVuelos(string sFechaDesde, string sFechaHasta, string sParametro)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = new DBBaseAleSuite().oDB_SP.EjecutarDT("[VB].[spS_MXJ_ConsultaCalculosPilotos]", "@FechaDesde", sFechaDesde.Dt(), "@FechaHasta", sFechaHasta.Dt(), "@Parametro", sParametro);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetVuelos(string sFechaDesde, string sFechaHasta, string sParametro)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = new DBBaseAleSuite().oDB_SP.EjecutarDT("[VB].[spS_MXJ_ConsultaCalculosVuelosPilotos]", "@IdBitacora", sParametro.I(), "@FechaDesde", sFechaDesde.Dt().ToString("yyyy-MM-dd"), "@FechaHasta", sFechaHasta.Dt().ToString("yyyy-MM-dd"));
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        //Metodos de Viaticos
        public DataSet ObtieneVuelosDelPeriodo(DateTime dtInicio, DateTime dtFin, string sClavePiloto)
        {
            try
            {
                DateTime dtFinalAux = new DateTime(dtFin.Year, dtFin.Month, dtFin.Day, 23, 59, 59);
                return new DBBaseAleSuite().oDB_SP.EjecutarDS("[VB].[spS_MXJ_ObtieneVuelosParaCalculoViaticos]", "@FechaInicio", dtInicio, "@FechaFin", dtFinalAux, "@ClavePil", sClavePiloto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ObtieneParametrosPernoctas()
        {
            try
            {
                return new DBBase().oDB_SP.EjecutarDT("[VB].[spS_MXJ_ObtieneValoresCalculoPernoctas]");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ActualizaVuelosNoCobrables(DataRow[] rowsNC)
        {
            try
            {
                string sQuery = string.Empty;
                string sCad = string.Empty;
                for (int i = 0; i < rowsNC.Length; i++)
                {
                    if (sCad == string.Empty)
                        sCad = rowsNC[i]["LegId"].S();
                    else
                    {
                        sCad = sCad + "," + rowsNC[i]["LegId"].S();
                    }
                }

                if (sCad != string.Empty)
                {
                    sQuery = "UPDATE [Principales].[tbp_VB_flightFollower] " +
                                    "SET Procesado = 1" +
                                    "WHERE Id IN (" + sCad + ") SELECT 1 ";

                    object oRes3 = new DBBase().oDB_SP.EjecutarValor_DeQuery(sQuery);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet ObtieneParametrosViaticos()
        {
            try
            {
                return new DBBase().oDB_SP.EjecutarDS("[VB].[spS_MXJ_ConsultaConfiguracion]");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet ObtieneConceptosAdicionales()
        {
            try
            {
                return new DBBase().oDB_SP.EjecutarDS("[VB].[spS_MXJ_ConsultaConceptosAdicionales]");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int SetInsertaPeriodo(PeriodoPiloto oPer)
        {
            try
            {
                object oRes;
                oRes = new DBBase().oDB_SP.EjecutarValor("[VB].[spI_MXJ_InsertaPeriodoPiloto]", "@CvePiloto", oPer.SCvePiloto,
                                                                                                "@FechaInicio", oPer.SFechaInicio.Dt(),
                                                                                                "@FechaFinal", oPer.SFechaFinal.Dt(),
                                                                                                "@Usuario", oPer.SUsuario);
                return oRes.S().I();
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        //public bool SetInsertaConceptosPiloto(ConceptosPiloto oCP, int iIdPeriodo)
        //{
        //    try
        //    {
        //        bool bRes = false;
        //        object oRes;
        //        oRes = new DBBase().oDB_SP.EjecutarValor("[VB].[spI_MXJ_InsertaConceptosPiloto]", "@IdPeriodo", iIdPeriodo,
        //                                                                                          "@IdConcepto", oCP.IIdConcepto,
        //                                                                                          "@DesConcepto", oCP.SDesConcepto,
        //                                                                                          "@Cantidad", oCP.ICantidad,
        //                                                                                          "@MontoConcepto", oCP.DMontoConcepto,
        //                                                                                          "@Total", oCP.DTotal,
        //                                                                                          "@Moneda", oCP.SMoneda);
        //        if (oRes.S().I() == 1)
        //            bRes = true;

        //        return bRes;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}

        public bool SetInsertaConceptosPilotoVitacora(List<ConceptosPiloto> oLstCP, int iIdPeriodo)
        {
            try
            {
                bool bRes = false;
                object oRes;

                for (int i = 0; i < oLstCP.Count; i++)
                {
                    oRes = new DBBase().oDB_SP.EjecutarValor("[VB].[spI_MXJ_InsertaConceptosPiloto]", "@IdPeriodo", iIdPeriodo,
                                                                                                      "@IdConcepto", oLstCP[i].IIdConcepto,
                                                                                                      "@DesConcepto", oLstCP[i].SDesConcepto,
                                                                                                      "@Cantidad", oLstCP[i].ICantidad,
                                                                                                      "@MontoConcepto", oLstCP[i].DMontoConcepto,
                                                                                                      "@Total", oLstCP[i].DTotal,
                                                                                                      "@Moneda", oLstCP[i].SMoneda);
                    if (oRes.S().I() == 1)
                        bRes = true;
                }

                return bRes;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool SetInsertaVuelosPiernasPiloto(List<VuelosPiernasPiloto> oLstPP, int iIdPeriodo)
        {
            try
            {
                bool bRes = false;
                object oRes;

                for (int i = 0; i < oLstPP.Count; i++)
                {
                    oRes = new DBBase().oDB_SP.EjecutarValor("[VB].[spI_MXJ_InsertaVuelosPiloto]", "@IdPeriodo", iIdPeriodo,
                                                                                                   "@Trip", oLstPP[i].LTrip,
                                                                                                   "@LegId", oLstPP[i].LLegId);
                    if (oRes.S().I() == 1)
                        bRes = true;
                }

                return bRes;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public int GetEstatusPeriodo(string sCvePiloto, DateTime dtInicio, DateTime dtFinal)
        {
            try
            {
                object oRes;
                oRes = new DBBase().oDB_SP.EjecutarValor("[VB].[spS_MXJ_ConsultaEstatusPeriodoxPiloto]", "@CvePiloto", sCvePiloto,
                                                                                                         "@FechaInicio", dtInicio.ToString("yyyy/MM/dd"),
                                                                                                         "@FechaFinal", dtFinal.ToString("yyyy/MM/dd"));
                return oRes.S().I();
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}