using ALE_MexJet.Clases;
using ALE_MexJet.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using NucleoBase.Core;

namespace ALE_MexJet.DomainModel
{
    public class DBRevenew : DBBase
    {
        public DataTable DBGetObtieneTiposClientes
        {
            get
            {
                try
                {
                    return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaTiposClienteRevenew]");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable DBGetObtieneDescuentosTiposCliente(int iIdTipoCliente)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaDescuentosTipoCliente]", "@IdTipoCliente", iIdTipoCliente);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DBSetInsertaDescuento(Revenew oDescRev)
        {
            try
            {
                object oRes = oDB_SP.EjecutarValor("[Principales].[spI_MXJ_InsertaDescuentoPaquete]", "@IdTipoCliente", oDescRev.iIdTipoCliente,
                                                                                                    "@Descripcion", oDescRev.sDescripcion,
                                                                                                    "@Porcentaje", oDescRev.dDescuento,
                                                                                                    "@Acumulado", oDescRev.iAcumulado,
                                                                                                    "@UsuarioCreacion", Utils.GetUser,
                                                                                                    "@IP", Utils.GetIPAddress());

                return oRes.S().I() > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DBSetEliminaDescuentoTipocliente(int iIdDescuento)
        {
            try
            {
                oDB_SP.EjecutarSP("[Principales].[spD_MXJ_EliminaDescuentoTipoCliente]", "@IdDescuento", iIdDescuento);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable DBGetObtieneDescuentoPorTipoCliente(int iIdDescuento)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaDescuentoTiposClienteByID]", "@IdDescuento", iIdDescuento);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DBSetActualizaDescuentoTipoCliente(Revenew oDesc)
        {
            try
            {
                oDB_SP.EjecutarSP("[Principales].[spU_MXJ_ActualizaDescuentoTiposClienteByID]", "@IdDescuento", oDesc.iIdDescuento,
                                                                                                "@Descripcion", oDesc.sDescripcion,
                                                                                                "@Porcentaje", oDesc.dDescuento,
                                                                                                "@Acumulado", oDesc.iAcumulado,
                                                                                                "@UsuarioModificacion", Utils.GetUser,
                                                                                                "@IP", Utils.GetIPAddress());
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}