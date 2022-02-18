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
    public class DBModelo : DBBase
    {

       
        public DataTable dtObjsCatMarca
        {
            get
            {
                try
                {
                    return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaMarcas]", "@IdMarca", 0, "@Descripcion", "%%", "@estatus", 1);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public DataTable dtObjsCatGrupoModelo
        {
            get
            {
                try
                {
                    return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaGrupoModelo]", "@GrupoModeloId", 0, "@Descripcion", "%%", "@ConsumoGalones", 0, "@estatus", 1);

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public DataTable dtObjsCatEspacio
        {
            get
            {
                try
                {
                    return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaGrupoEspacio]");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public DataTable dtObjsCatDesignador
        {
            get
            {
                try
                {
                    return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaDesignador]", "@Clave", "%%", "@Descripcion", "%%", "@estatus", 1);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public DataTable dtObjsCatTipo
        {
            get
            {
                try
                {
                    return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaTipoModelo]");
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
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaModelo]", oArrFiltros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBSave(Modelo oModelo)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spI_MXJ_InsertaModelo]", "@IdMarca", oModelo.iMarca,
                                                                                      "@DescripcionModelo", oModelo.sDescripcion,
                                                                                      "@IdGrupoModelo", oModelo.iGrupoModelo,
                                                                                      "@IdTipo", oModelo.iTipo,
                                                                                      "@Velocidad", oModelo.dVelocidad,
                                                                                      "@IdGrupoTamaño", oModelo.iGrupoTamaño,
                                                                                      "@IdHorasAño", oModelo.iHorasAño,
                                                                                      "@PesoMaximo", oModelo.dPesoMaximo,
                                                                                      "@IdDesignador", oModelo.iDesignador,
                                                                                      "@Status", oModelo.iStatus,
                                                                                      "@IP", oModelo.sIP,
                                                                                      "@Usuario", Utils.GetUser);//piloto.sUsuarioCreacion,
                //"@fc_IP	", "101.1.15.2");//piloto.sIP);
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBUpdate(Modelo oModelo)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spu_MXJ_ActualizaModelo]", "@Id", oModelo.iId,
                                                                                        "@IdMarca", oModelo.iMarca,
                                                                                        "@DescripcionModelo", oModelo.sDescripcion,
                                                                                        "@IdGrupoModelo", oModelo.iGrupoModelo,
                                                                                        "@IdTipo", oModelo.iTipo,
                                                                                        "@Velocidad", oModelo.dVelocidad,
                                                                                        "@IdGrupoTamaño", oModelo.iGrupoTamaño,
                                                                                        "@IdHorasAño", oModelo.iHorasAño,
                                                                                        "@PesoMaximo", oModelo.dPesoMaximo,
                                                                                        "@IdDesignador", oModelo.iDesignador,
                                                                                        "@Status", oModelo.iStatus,
                                                                                        "@IP", oModelo.sIP,
                                                                                        "@Usuario", Utils.GetUser);
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBDelete(Modelo oModelo)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spD_MXJ_EliminaModelo]",   "@Id", oModelo.iId,
                                                                                        "@IP", null,
                                                                                        "@Usuario", Utils.GetUser);
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBValida(Modelo oModelo)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spS_MXJ_ConsultaModeloExiste]", "@Descripcion", oModelo.sDescripcion);
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}