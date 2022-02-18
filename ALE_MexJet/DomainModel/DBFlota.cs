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
    public class DBFlota : DBBase
    {
      
        public DataTable DBSearchObj(params object[] oArrFiltros)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaFlota]", oArrFiltros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBSave(Flota oFlota)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spI_MXJ_InsertaFlota]", "@Descripcion", oFlota.sDescripcion,
                                                                                        "@FlotaCU2",oFlota.iFlotaCU2,
                                                                                        "@DescripcionFlotaCU2",oFlota.sDescripcionFlotaCU2,
                                                                                        "@FlotaSENEAM",oFlota.iFlotaSENEAM,
                                                                                        "@FlotaAPHIS",oFlota.iFlotaAPHIS,
                                                                                        "@Status", oFlota.iStatus,
                                                                                        "@IP", oFlota.sIP,
                                                                                        "@Usuario", Utils.GetUser);

                new DBUtils().DBSaveBitacora(Convert.ToInt32(Enumeraciones.Pantallas.Flota), Convert.ToInt32(Enumeraciones.TipoOperacion.Insertar), "Se insertó el registro: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBUpdate(Flota oFlota)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spu_MXJ_ActualizaFlota]", "@IdFlota", oFlota.iId,
                                                                                        "@Descripcion", oFlota.sDescripcion,
                                                                                        "@FlotaCU2", oFlota.iFlotaCU2,
                                                                                        "@DescripcionFlotaCU2", oFlota.sDescripcionFlotaCU2,
                                                                                        "@FlotaSENEAM", oFlota.iFlotaSENEAM,
                                                                                        "@FlotaAPHIS", oFlota.iFlotaAPHIS,
                                                                                        "@Status", oFlota.iStatus,
                                                                                        "@IP", "",
                                                                                        "@Usuario", Utils.GetUser);

                new DBUtils().DBSaveBitacora(Convert.ToInt32(Enumeraciones.Pantallas.Flota), Convert.ToInt32(Enumeraciones.TipoOperacion.Actualizar), "Se actualizó el registro: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBDelete(Flota oFlota)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spD_MXJ_EliminaFlota]", "@IdFlota", oFlota.iId,
                                                                                      "@IP", "",
                                                                                      "@Usuario", Utils.GetUser);

                new DBUtils().DBSaveBitacora(Convert.ToInt32(Enumeraciones.Pantallas.Flota), Convert.ToInt32(Enumeraciones.TipoOperacion.Eliminar), "Se eliminó el registro: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBValida(Flota oFlota)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spS_MXJ_ConsultaFlotaExiste]", "@Descripcion", oFlota.sDescripcion);
                                                                                    
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable DBGetCodUnidad2()
        {
            //ALE_MexJet.wsSyteline.Iws_SyteLineClient wssyte = new wsSyteline.Iws_SyteLineClient();
            //DataSet dsCodigoUnidadDosUnion = wssyte.GetCodigoUnidad_Dos_Union();
            //return dsCodigoUnidadDosUnion.Tables[0];
            return new DBSAP().DBGetCodigoUnidad2_Union;
        }
        public string DBGetCodUnidad2(string clave)
        {
            //ALE_MexJet.wsSyteline.Iws_SyteLineClient wssyte = new wsSyteline.Iws_SyteLineClient();
            //return wssyte.GetDescripcion_CodigoUnidadDOS(clave);
            return new DBSAP().DBGetDescripcionCodigoUnidad2(clave);
        }
    }
}