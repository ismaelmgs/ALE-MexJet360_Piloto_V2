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
    public class DBInflacion : DBBase
    {
        public DataTable dtObjsCat
        {
            get
            {
                try
                {
                    return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaPilotos]");
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
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaInflacion]", oArrFiltros);
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
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaInflacionDetalle]", oArrFiltros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBSave(Inflacion oInflacion)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spI_MXJ_InsertaInflacion]","@TipoInflacion", oInflacion.sTipoInflacion,
                                                                                       // "@Año",oInflacion.iAño,
                                                                                       // "@Porcentaje", oInflacion.dcPorcentaje,
                                                                                        "@Status", oInflacion.iStatus,
                                                                                        "@IP", oInflacion.sIP,
                                                                                        "@Usuario", Utils.GetUser);
                new DBUtils().DBSaveBitacora(Convert.ToInt32(Enumeraciones.Pantallas.Inflacion), Convert.ToInt32(Enumeraciones.TipoOperacion.Insertar), "Se insertó el registro: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBUpdate(Inflacion oInflacion)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spu_MXJ_ActualizaInflacion]", "@Id", oInflacion.iId,
                                                                                           "@TipoInflacion", oInflacion.sTipoInflacion,
                                                                                         //  "@Año", oInflacion.iAño,
                                                                                         //  "@Porcentaje", oInflacion.dcPorcentaje,
                                                                                           "@Status", oInflacion.iStatus,
                                                                                           "@IP", oInflacion.sIP,
                                                                                           "@Usuario", Utils.GetUser);
                new DBUtils().DBSaveBitacora(Convert.ToInt32(Enumeraciones.Pantallas.Inflacion), Convert.ToInt32(Enumeraciones.TipoOperacion.Actualizar), "Se actualizó el registro: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBDelete(Inflacion oInflacion)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spD_MXJ_EliminaInflacion]", "@Id", oInflacion.iId,
                                                                                      "@IP", null,
                                                                                      "@Usuario", Utils.GetUser);
                new DBUtils().DBSaveBitacora(Convert.ToInt32(Enumeraciones.Pantallas.Inflacion), Convert.ToInt32(Enumeraciones.TipoOperacion.Eliminar), "Se eliminó el registro: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBValida(Inflacion oInflacion)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[sps_MXJ_ConsultaInflacionExiste]", "@TipoInflacion", oInflacion.sTipoInflacion
                                                                                          // ,"@Año", oInflacion.iAño
                                                                                           );

                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public int DBSaveDetalle(Inflacion oInflacion)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spI_MXJ_InsertaInflacionDetalle]",// "@TipoInflacion", oInflacion.sTipoInflacion,
                                                                                        "@IdInflcacion",oInflacion.iId,
                                                                                        "@Año",oInflacion.iAño,
                                                                                        "@Porcentaje", oInflacion.dcPorcentaje,
                                                                                        "@Status", oInflacion.iStatus,
                                                                                        "@IP", oInflacion.sIP,
                                                                                        "@Usuario", Utils.GetUser);
                new DBUtils().DBSaveBitacora(Convert.ToInt32(Enumeraciones.Pantallas.Inflacion), Convert.ToInt32(Enumeraciones.TipoOperacion.Insertar), "Se insertó el registro: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBUpdateDetalle(Inflacion oInflacion)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spu_MXJ_ActualizaInflacionDetalle]", "@IdDetalle", oInflacion.iIdDetalle,
                                                                                         //  "@TipoInflacion", oInflacion.sTipoInflacion,
                                                                                            "@Año", oInflacion.iAño,
                                                                                            "@Porcentaje", oInflacion.dcPorcentaje,
                                                                                            "@Status", oInflacion.iStatus,
                                                                                            "@IP", oInflacion.sIP,
                                                                                            "@Usuario", Utils.GetUser);
                new DBUtils().DBSaveBitacora(Convert.ToInt32(Enumeraciones.Pantallas.Inflacion), Convert.ToInt32(Enumeraciones.TipoOperacion.Actualizar), "Se actualizó el registro: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBDeleteDetalle(Inflacion oInflacion)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spD_MXJ_EliminaInflacionDetalle]", "@IdDetalle", oInflacion.iIdDetalle,
                                                                                      "@IP", null,
                                                                                      "@Usuario", Utils.GetUser);
                new DBUtils().DBSaveBitacora(Convert.ToInt32(Enumeraciones.Pantallas.Inflacion), Convert.ToInt32(Enumeraciones.TipoOperacion.Eliminar), "Se eliminó el registro: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBValidaDetalle(Inflacion oInflacion)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[sps_MXJ_ConsultaInflacionDetalleExiste]"//, "@TipoInflacion", oInflacion.sTipoInflacion
                                                                                                     ,"@Año", oInflacion.iAño
                                                                                                     , "@idInflcacion",oInflacion.iId
                                                                                           );

                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}