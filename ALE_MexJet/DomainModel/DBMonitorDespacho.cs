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
    public class DBMonitorDespacho : DBBase
    {
        public DataTable DBSearchDDL()
        {
            try
            {
                return oDB_SP.EjecutarDT("Catalogos.spS_MXJ_ConsultaAeropuertosBASEDespacho");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBSearchMonitorDespacho(params object[] oArrFiltros)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaMonitorDespacho]", oArrFiltros);
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
        public int DBUpdate(params object[] oArrFiltros)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spI_MXJ_ActualizaMonitorDespacho]", oArrFiltros);
                new DBUtils().DBSaveBitacora(Convert.ToInt32(Enumeraciones.Pantallas.MonitorDespacho), Convert.ToInt32(Enumeraciones.TipoOperacion.Actualizar), "Se actualizó el registro: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBConsultaMonitorDespacho(params object[] oArrFiltros)
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
        public int DBSaveSeguimiento(params object[] oArrFilSeguimiento)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Principales].[spI_MXJ_InsertaHistorico]", oArrFilSeguimiento);
                new DBUtils().DBSaveBitacora(Convert.ToInt32(Enumeraciones.Pantallas.MonitorDespacho), Convert.ToInt32(Enumeraciones.TipoOperacion.Insertar), "Se insertó el registro:: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }catch (Exception ex){
                throw ex;
            }
        }
       
    }
}