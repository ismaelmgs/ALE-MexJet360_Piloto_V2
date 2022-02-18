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
    public class DBConsultaPrefactura : DBBase
    {
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

        public DataTable DBGetContratosCliente(int iIdClient)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaContratosDDL]", "@IdCliente", iIdClient);

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
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaPrefactura]", oArrFiltros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ValidaEstadoFacturaCancelacion(string sInvNum)
        {
            try
            {
                //ALE_MexJet.wsSyteline.Iws_SyteLineClient wssyte = new wsSyteline.Iws_SyteLineClient();
                //return wssyte.ValidaEstatusCancelacionFactura(sInvNum).I() > 0 ;

                string sCad = string.Empty;
                sCad = "SELECT COUNT(1) FROM [Principales].[tbp_DI_Facturas] WHERE ID = " + sInvNum + " AND Estatus = 3";
                object oRes = new DBIntegrator().oDB_SP.EjecutarValor_DeQuery(sCad);

                return oRes.S().I() > 0;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public void CancelaPrefactura(int IdPrefactura)
        {
            try
            {
                oDB_SP.EjecutarSP("[Principales].[spD_MXJ_CancelaPrefactura]", "@IdPrefactura", IdPrefactura,
                                                                            "@Usuario", Utils.GetUser,
                                                                            "@IP", Utils.GetIPAddress());
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

    }
}