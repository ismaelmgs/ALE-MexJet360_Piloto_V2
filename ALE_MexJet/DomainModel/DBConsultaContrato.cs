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
    public class DBConsultaContrato : DBBase
    {
        public DataTable DBSearchObj(params object[] oArrFiltros)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaContratos]", oArrFiltros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable dtClientes
        {
            get
            {
                try
                {
                    return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaClientesConContrato]");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable DBGetContrato(int iIdCliente)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[SpS_MXJ_ConsultaContratoCliente]", "@IdCliente", iIdCliente);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet DBGetKardexContrato(int iIdContrato)
        {
            try
            {
                //return oDB_SP.EjecutarDS("[Principales].[spS_MXJ_KardexHorasPorContrato]", "@IdContrato", iIdContrato);
                return oDB_SP.EjecutarDS("[Principales].[spS_MXJ_StatusVueloHorasPorContrato]", "@IdContrato", iIdContrato);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}