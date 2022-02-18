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
    public class DBCombustibleMensualInternacional : DBBase
    {
        public DataTable DBSearchObj(params object[] oArrFiltros)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaCombustibleMen]", oArrFiltros);
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
        public int DBSave(CombustibleMensualInternacional oCombustibleMensual)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spI_MXJ_InsertaCombustibleMen]", "@Anio", oCombustibleMensual.iAnio,
                                                                                    "@IdMes", oCombustibleMensual.iIdMes,
                                                                                    "@Importe", oCombustibleMensual.dImporte,
                                                                                    "@Status", oCombustibleMensual.iStatus,
                                                                                    "@IP", oCombustibleMensual.sIP,
                                                                                    "@Usuario", Utils.GetUser);

                new DBUtils().DBSaveBitacora(Enumeraciones.Pantallas.RangoCombustible.S().I(), Enumeraciones.TipoOperacion.Insertar.S().I(), "Se inserto el registro: " + oResult.S());

                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBUpdate(CombustibleMensualInternacional oCombustibleMensual)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spU_MXJ_ActualizaCombustibleMen]", "@IdCombustibleMenInt", oCombustibleMensual.iIdCombustibleMenInt,
                                                                                        "@Anio", oCombustibleMensual.iAnio,
                                                                                        "@IdMes", oCombustibleMensual.iIdMes,
                                                                                        "@Importe", oCombustibleMensual.dImporte,
                                                                                        "@Status", oCombustibleMensual.iStatus,
                                                                                        "@Usuario", Utils.GetUser,
                                                                                        "@IP", oCombustibleMensual.sIP
                                                                                        );
                new DBUtils().DBSaveBitacora(Enumeraciones.Pantallas.RangoCombustible.S().I(), Enumeraciones.TipoOperacion.Actualizar.S().I(), "Se actualizo el registro: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBDelete(CombustibleMensualInternacional oCombustibleMensual)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spD_MXJ_EliminaCombustibleMen]", "@IdCombustibleMenInt", oCombustibleMensual.iIdCombustibleMenInt,
                                                                                        "@IP", oCombustibleMensual.sIP,
                                                                                        "@Usuario", Utils.GetUser);
                new DBUtils().DBSaveBitacora(Enumeraciones.Pantallas.RangoCombustible.S().I(), Enumeraciones.TipoOperacion.Eliminar.S().I(), "Se elimino el registro: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBValida(CombustibleMensualInternacional oCombustibleMensual)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spS_MXJ_ConsultaCombustibleMenExiste]", "@Anio", oCombustibleMensual.iAnio,
                                                                                                        "@IdMes", oCombustibleMensual.iIdMes,
                                                                                                        "@estatus", oCombustibleMensual.iStatus);

                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}