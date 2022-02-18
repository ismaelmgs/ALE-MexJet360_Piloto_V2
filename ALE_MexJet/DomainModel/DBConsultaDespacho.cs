using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

using ALE_MexJet.Objetos;
using ALE_MexJet.Clases;
using NucleoBase.Core;

namespace ALE_MexJet.DomainModel
{
    public class DBConsultaDespacho: DBBase
    {
        public DataTable DBBuscaDespacho(params object[] oArrFiltros)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[SpS_MXJ_ConsultaDespacho]", oArrFiltros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBSearchCliente(params object[] oArrFiltros)
        {
            try
            {
                return oDB_SP.EjecutarDT("CATALOGOS.spS_MXJ_ClienteContratoGastosInternos", oArrFiltros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBSearchContrato(params object[] oArrFiltros)
        {
            try
            {
                return oDB_SP.EjecutarDT("CATALOGOS.spS_MXJ_ClienteContrato", oArrFiltros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBSearchPiernasSolicitud(params object[] oArrFiltros)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaPiernasVuelo]", oArrFiltros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBInsertDictamen(params object[] oArrFiltros)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Principales].[spI_MXJ_EditaConsultaDespacho]", oArrFiltros);
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBConsultaSolicitud(params object[] oArrFiltros)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaMonitorDespacho]", oArrFiltros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}