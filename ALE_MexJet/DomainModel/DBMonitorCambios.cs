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
    public class DBMonitorCambios : DBBaseFlight
    {
        public DataTable DBSearchObj(params object[] oArrFiltros)
        {
            try
            {
                return oDB_SP.EjecutarDT("[VisasPasaportes].spS_ConsultaMonitorNotifications2", oArrFiltros);
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
                return oDB_SP.EjecutarDT("[VisasPasaportes].spS_ConsultaCambio", oArrFiltros);
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
                oResult = oDB_SP.EjecutarValor("[VisasPasaportes].spU_ActualizaMonitorNotifications2", oArrFiltros);
                new DBUtils().DBSaveBitacora(Convert.ToInt32(Enumeraciones.Pantallas.Notifications), Convert.ToInt32(Enumeraciones.TipoOperacion.Actualizar), "Se actualizó el registro: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBCountNotificaciones()
        {
            try
            {
                return oDB_SP.EjecutarDT("[VisasPasaportes].spS_ConsultaCountMonitorNotifications", new object[] { }).Rows[0].Field<int>(0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}