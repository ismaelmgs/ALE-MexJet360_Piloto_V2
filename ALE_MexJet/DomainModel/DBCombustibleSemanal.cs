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
    public class DBCombustibleSemanal : DBBase
    {
        public DataTable DBSearchObj(params object[] oArrFiltros)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaCombustibleSem]", oArrFiltros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBSearchObjMes(params object[] oArrFiltrosMes)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaMes]", oArrFiltrosMes);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBSearchObjAero(params object[] oArrFiltrosAero)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaAeropuertoBase]");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBSave(CombustibleSemanal oCombustibleSemanal)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spI_MXJ_InsertaCombustibleSem]", "@Anio", oCombustibleSemanal.iAnio,
                                                                                    "@IdMes", oCombustibleSemanal.iIdMes,
                                                                                    "@IdAeropuerto", oCombustibleSemanal.iIdAeropuerto,
                                                                                    "@Status", oCombustibleSemanal.iStatus,
                                                                                    "@IP", oCombustibleSemanal.sIP,
                                                                                    "@Usuario", Utils.GetUser);

                new DBUtils().DBSaveBitacora(Enumeraciones.Pantallas.RangoCombustible.S().I(), Enumeraciones.TipoOperacion.Insertar.S().I(), "Se inserto el registro: " + oResult.S());

                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBUpdate(CombustibleSemanal oCombustibleSemanal)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spU_MXJ_ActualizaCombustibleSem]", "@IdCombustibleSem", oCombustibleSemanal.iIdCombustibleSem,
                                                                                        "@Anio", oCombustibleSemanal.iAnio,
                                                                                        "@IdMes", oCombustibleSemanal.iIdMes,
                                                                                        "@IdAeropuerto", oCombustibleSemanal.iIdAeropuerto,
                                                                                        "@Estatus",oCombustibleSemanal.iStatus,
                                                                                        "@Usuario", Utils.GetUser,
                                                                                        "@IP", oCombustibleSemanal.sIP
                                                                                        );
                new DBUtils().DBSaveBitacora(Enumeraciones.Pantallas.RangoCombustible.S().I(), Enumeraciones.TipoOperacion.Actualizar.S().I(), "Se actualizo el registro: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBDelete(CombustibleSemanal oCombustibleSemanal)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spD_MXJ_EliminaCombustibleSem]", "@IdCombustibleSem", oCombustibleSemanal.iIdCombustibleSem,
                                                                                        "@IP", oCombustibleSemanal.sIP,
                                                                                        "@Usuario", Utils.GetUser);
                new DBUtils().DBSaveBitacora(Enumeraciones.Pantallas.RangoCombustible.S().I(), Enumeraciones.TipoOperacion.Eliminar.S().I(), "Se elimino el registro: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBValidaTipoCambio(CombustibleSemanal oCombustibleSemanal)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spS_MXJ_ExisteTipoCambio]", "@Anio", oCombustibleSemanal.iAnio,
                                                                                        "@MES", oCombustibleSemanal.iIdMes);


                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBValida(CombustibleSemanal oCombustibleSemanal)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spS_MXJ_ConsultaCombustibleSemExiste]", "@Anio", oCombustibleSemanal.iAnio,
                                                                                                     "@IdMes", oCombustibleSemanal.iIdMes,
                                                                                                     "@IdAeropuerto", oCombustibleSemanal.iIdAeropuerto,
                                                                                                     "@estatus", oCombustibleSemanal.iStatus);
                

                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}