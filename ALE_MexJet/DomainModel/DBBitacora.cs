using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ALE_MexJet.Objetos;
using System.Data;
using ALE_MexJet.Clases;
using NucleoBase.Core;

namespace ALE_MexJet.DomainModel
{
    public class DBBitacora : DBBase
    {
        public DataTable dtObjCliente()
        {
            try
            {
                Bitacora B = new Bitacora();
                return oDB_SP.EjecutarDT("Catalogos.spS_MXJ_ConsultaClienteDDL", "@status", B.status);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable dtObjContrato(Bitacora B)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaContratosDDL]", "@IdCliente", B.IdCliente);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable dtObjBitacora(Bitacora B)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaBitacora]",
                                                             "@IdFolio",B.IdFolio
                                                            ,"@IdCliente",B.IdCliente
                                                            ,"@IdContrato",B.IdContrato
                                                            ,"@FechaIni",B.FechaIni
                                                            ,"@FechaFin",B.FechaFin
                                                                                 );
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable dtBitacoraDuplicada()
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaBitacorasDuplicadas]");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable dtBitacoraCobrada(Bitacora B)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaBitacorasCobradas]",
                                                                            "@FechaIni", B.FechaIni
                                                                            ,"@FechaFin", B.FechaFin);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable dtBitacoraPorCobrar(Bitacora B)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaBitacorasPorCobrar]",
                                                                            "@FechaIni", B.FechaIni
                                                                            ,"@FechaFin", B.FechaFin);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable dtNumeroBitacorasCobradas(Bitacora B)
        {
            try
            {
                DataTable oRes = oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaNumeroBitacorasCobradas]",
                                                                                        "@FechaIni", B.FechaIni
                                                                                        ,"@FechaFin", B.FechaFin);
                return oRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable dtNumeroBitacorasPorCobrar(Bitacora B)
        {
            try
            {
                DataTable oRes = oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaNumeroBitacorasPorCobrar]",
                                                                                            "@FechaIni", B.FechaIni
                                                                                            ,"@FechaFin", B.FechaFin);
                return oRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable dtNumeroTotalRegistros(Bitacora B)
        {
            try
            {
                DataTable oRes = oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaNumeroTotalRegistros]", 
                                                                                            "@FechaIni", B.FechaIni
                                                                                            ,"@FechaFin", B.FechaFin);
                return oRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable dtNumeroTotalDuplicadas()
        {
            try
            {
                DataTable oRes = oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaNumeroTotalDuplicadas]");
                return oRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBDelete(Bitacora oBitacora)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Principales].[spD_MXJ_EliminaBitacorasDuplicadas]", "@idBitacora", oBitacora.iIdBitacora);
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}