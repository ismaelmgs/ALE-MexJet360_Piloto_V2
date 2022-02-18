using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ALE_MexJet.DomainModel
{
    public class DBConsultaTrafico : DBBase
    {
        /// <summary>
        /// Consulta Clientes
        /// </summary>
        /// <param name="oArrFiltros"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Consulta Contratos
        /// </summary>
        /// <param name="oArrFiltros"></param>
        /// <returns></returns>
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

        public DataTable DBSearchObj(params object[] oArrFiltros)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaTrafico]", oArrFiltros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}