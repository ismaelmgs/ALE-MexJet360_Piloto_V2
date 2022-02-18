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
    public class DBRangoCombustible : DBBase
    {
        public DataTable dtObjsCat
        {
            get
            {
                try
                {
                    return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaRangoCombustible]");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public DataTable DBSearchObj(params object[] oArrFiltros)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaRangoCombustible]", oArrFiltros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBSave(RangoCombustible oRangoCombustible)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spI_MXJ_InsertaRangoCombustible]", "@IdCombustible", oRangoCombustible.iIdCombustible,
                                                                                  "@Desde", oRangoCombustible.dDesde,
                                                                                  "@Hasta", oRangoCombustible.dHasta,
                                                                                  "@Aumento", oRangoCombustible.dAumento,
                                                                                  "@Status", oRangoCombustible.iStatus,
                                                                                  "@IP", oRangoCombustible.sIP,
                                                                                  "@Usuario", Utils.GetUser);
               
                //"@fc_IP	", "101.1.15.2");//piloto.sIP);
                new DBUtils().DBSaveBitacora(Convert.ToInt32(Enumeraciones.Pantallas.RangoCombustible), Convert.ToInt32(Enumeraciones.TipoOperacion.Insertar), "Se insertó el registro: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBUpdate(RangoCombustible oRangoCombustible)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spU_MXJ_ActualizaRangoCombustible]", "@IdRangoIden", oRangoCombustible.iIdRangoIden,
                                                                                     "@Desde", oRangoCombustible.dDesde,
                                                                                     "@Hasta", oRangoCombustible.dHasta,
                                                                                     "@Aumento", oRangoCombustible.dAumento,
                                                                                     "@Status", oRangoCombustible.iStatus,
                                                                                     "@IP", oRangoCombustible.sIP,
                                                                                     "@Usuario", Utils.GetUser);
                new DBUtils().DBSaveBitacora(Convert.ToInt32(Enumeraciones.Pantallas.RangoCombustible), Convert.ToInt32(Enumeraciones.TipoOperacion.Actualizar), "Se actualizó el registro: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBDelete(RangoCombustible oRangoCombustible)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spD_MXJ_EliminaRangoCombustible]", "@IdRangoIden", oRangoCombustible.iIdRangoIden,
                                                                                    "@IP", oRangoCombustible.sIP,
                                                                                    "@Usuario", Utils.GetUser);
                new DBUtils().DBSaveBitacora(Convert.ToInt32(Enumeraciones.Pantallas.RangoCombustible), Convert.ToInt32(Enumeraciones.TipoOperacion.Eliminar), "Se eliminó el registro: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBValidaRango(RangoCombustible oRangoCombustible, bool bDesde, bool bHasta)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spS_MXJ_RangoCombustibleDesdeHasta]", "@IdCombustible", oRangoCombustible.iIdCombustible,
                                                                                                    "@Desde", oRangoCombustible.dDesde,
                                                                                                    "@Hasta", oRangoCombustible.dHasta,
                                                                                                    "@validaDesde", true,
                                                                                                    "@validaHasta", true);

                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBValidaRangoUpdate(RangoCombustible oRangoCombustible, bool bDesde, bool bHasta)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spS_MXJ_RangoCombustibleDesdeHastaUpdate]", "@IdRangoId", oRangoCombustible.iIdRangoIden,
                                                                                                        "@IdCombustible", oRangoCombustible.iIdCombustible,
                                                                                                        "@Desde", oRangoCombustible.dDesde,
                                                                                                        "@Hasta", oRangoCombustible.dHasta,
                                                                                                        "@validaDesde", bDesde,
                                                                                                        "@validaHasta", bHasta);

                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public int DBValida(RangoCombustible oRangoCombustible)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spS_MXJ_ConsultaRangoCombustibleExiste]", "@IdCombustible", oRangoCombustible.iIdCombustible,
                                                                                                       "@Desde", oRangoCombustible.dDesde,
                                                                                                       "@Hasta", oRangoCombustible.dHasta,
                                                                                                       "@Aumento", oRangoCombustible.dAumento,
                                                                                                       "@estatus", oRangoCombustible.iStatus);

                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}