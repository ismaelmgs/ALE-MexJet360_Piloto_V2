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
    public class DBConsultaTarifa : DBBase
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

       

        public DataTable DBCargaDetalle(params object[] oArrFiltros)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaTarifasActuales]", oArrFiltros);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}