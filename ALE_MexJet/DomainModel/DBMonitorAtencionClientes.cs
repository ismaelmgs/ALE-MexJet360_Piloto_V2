using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using NucleoBase.Core;
using ALE_MexJet.Objetos;
using ALE_MexJet.Clases;

namespace ALE_MexJet.DomainModel
{
    public class DBMonitorAtencionClientes : DBBase
    {
        /// <summary>
        /// Inserta un registro en el Monitor de Atencion a Clientes con el estatus de pendiente
        /// </summary>
        /// <param name="iIdSolicitud">Se refiere al ID de la solicitud de vuelo.</param>
        /// <returns></returns>
        public int DBSetInsertaMonitorAtencionClientes(int iIdSolicitud)
        {
            try
            {
                object oRes = oDB_SP.EjecutarValor("[Principales].[spI_MXJ_InsertaMonitorAtencionClientes]", "@IdSolicitud", iIdSolicitud,
                                                                                                            "@UsuarioCreacion", Utils.GetUser,
                                                                                                            "@IP", Utils.GetIPAddress());

                return oRes != null ? oRes.S().I() : 0;

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
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaMonitorAtencionClientes]", oArrFiltros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBSearchObjDetalle(params object[] oArrFiltros)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaMonitorAtencionClientesDetalle]", oArrFiltros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet DBConsultaNotas(params object[] oArrFiltros)
        {
            try
            {
                return oDB_SP.EjecutarDS("[Principales].[SpS_MXJ_ConsultaNotas]", oArrFiltros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBSearchAeropuertosBase()
        {
            try
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaAeropuertosBASE]");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBUpdateStatus(params object[] oArrFiltros)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Principales].[spU_MXJ_ActualizaEstatusAtencionClientes]", oArrFiltros);
                new DBUtils().DBSaveBitacora(Convert.ToInt32(Enumeraciones.Pantallas.MonitorAtencionClientes), Convert.ToInt32(Enumeraciones.TipoOperacion.Actualizar), "Se actualizó el registro: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBUpdateAtnCliente(params object[] oArrFiltros)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Principales].[spU_MXJ_ActualizaMonitorAtencionClientes]", oArrFiltros);
                new DBUtils().DBSaveBitacora(Convert.ToInt32(Enumeraciones.Pantallas.MonitorAtencionClientes), Convert.ToInt32(Enumeraciones.TipoOperacion.Actualizar), "Se actualizó el registro: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBEditaAtnCliente(int IdSolicitud)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Principales].[spU_MXJ_EditaMonitorAtencionClientes]","@IdSolicitud",IdSolicitud, 
                                                                                                        "@Usuario",Utils.GetUser.S(),
                                                                                                        "@IP","");
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}