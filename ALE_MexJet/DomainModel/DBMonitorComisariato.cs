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
    public class DBMonitorComisariato:DBBase
    {
        public DataTable DBSearchMonitorComisariato(params object[] oArrFiltros)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MonitorComisariato]", oArrFiltros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBGetStausComisariato()
        {
            try
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaEstausComisariato]");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBUpdateStaus(MonitorComisariato oMonitor)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Principales].[spU_MXJ_ActualizaStatusComisariato]",
                                                                                    "@IdComisariato" , oMonitor.iIdComisariato,
                                                                                    "@Status", oMonitor.iStatus,
                                                                                    "@UsuarioModificacion", Utils.GetUser);

                new DBUtils().DBSaveBitacora(Enumeraciones.Pantallas.MonitorComisariato.S().I(), Enumeraciones.TipoOperacion.Actualizar.S().I(), "Se actualizó el registro: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBInsertaDetalle(params object[] oArrFil)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Principales].[spI_MXJ_InsertComDetalle]",oArrFil);

                new DBUtils().DBSaveBitacora(Enumeraciones.Pantallas.MonitorComisariato.S().I(), Enumeraciones.TipoOperacion.Actualizar.S().I(), "Se actualizó el registro: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBGetComisariatoDetalle(params object[] oArrFil)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaComisariatoDetalle]", oArrFil);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}