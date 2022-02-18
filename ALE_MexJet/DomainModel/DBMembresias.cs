using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using NucleoBase.BaseDeDatos;
using ALE_MexJet.Objetos;
using NucleoBase.Core;
using ALE_MexJet.Clases;

namespace ALE_MexJet.DomainModel
{
    public class DBMembresias : DBBase
    {
        //public DataTable dtObjsCat
        //{
        //    get
        //    {
        //        try
        //        {
        //            return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaPilotos]");
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //    }
        //}
        public DataTable DBSearchObj(params object[] oArrFiltros)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaMembresias]", oArrFiltros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBSave(Membresia oMembresia)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spI_MXJ_InsertaMembresia]", "@Codigo", oMembresia.sCodigo,
                                                                                        "@Descripcion", oMembresia.sDescripcion,
                                                                                        "@UsuarioCreacion", Utils.GetUser,
                                                                                        "@IP", Utils.GetIPAddress());
                
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBUpdate(Membresia oMembresia)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spU_MXJ_ActualizaMembresia]",  "@IdMembresia", oMembresia.iId,
                                                                                            "@Codigo", oMembresia.sCodigo,
                                                                                            "@Descripcion", oMembresia.sDescripcion,
                                                                                            "@Status", oMembresia.iStatus,
                                                                                            "@UsuarioModificacion", Utils.GetUser,
                                                                                            "@IP", Utils.GetIPAddress());

                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBValida(Membresia oMembresia)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[tbc_MXJ_ConsultaMembresiasExiste]", "@Codigo", oMembresia.sCodigo);
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBDelete(Membresia oMembresia)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spD_MXJ_EliminaMembresias]", "@IdMembresia", oMembresia.iId,
                                                                                            "@UsuarioModificacion", Utils.GetUser,
                                                                                            "@IP", Utils.GetIPAddress());

                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}