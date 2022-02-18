using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE_MexJet.DomainModel
{
    class DBHistoricoTarifasCliente : DBBase
    {
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

        public DataTable DBSearchHistoricoTarifasCliente(params object[] oArrFiltros)
        {
             try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_HistoricoTarifasCliente]", oArrFiltros);
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }
    }
}
