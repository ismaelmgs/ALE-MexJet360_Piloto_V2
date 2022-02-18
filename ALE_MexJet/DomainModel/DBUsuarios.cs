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
    public class DBUsuarios : DBBase
    {
        public DataTable dtGetMenu(string sUsuario)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultarMenuporRol]", "@RolId",sUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable dtObjsCat
        {
            get
            {
                try
                {
                    return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaCatalogoAeronaves]");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable dtObjCatAro
        {
            get
            {
                try
                {
                    return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaCataloRol]");
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
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaUsuarios]", oArrFiltros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBSave(Usuarios oUsuario)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spI_MXJ_InsertaUsuario]", "@Descripcion", oUsuario.sDescripcion,
                                                                                        "@IdRol", oUsuario.iIdRol,
                                                                                        "@IdBase", oUsuario.iIdBase,
                                                                                        "@Status", oUsuario.iStatus,
                                                                                        "@NoTelefonico", oUsuario.sNoTelefonico,
                                                                                        "@IP", oUsuario.sIP,
                                                                                        "@Usuario", Utils.GetUser);
                new DBUtils().DBSaveBitacora(Convert.ToInt32(Enumeraciones.Pantallas.Usuario), Convert.ToInt32(Enumeraciones.TipoOperacion.Insertar), "Se insertó el registro: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBUpdate(Usuarios oUsuario)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spu_MXJ_ActualizaUsuario]", "@Id", oUsuario.iId,
                                                                                        "@Descripcion", oUsuario.sDescripcion,
                                                                                        "@IdRol", oUsuario.iIdRol,
                                                                                        "@IdBase", oUsuario.iIdBase,
                                                                                        "@Status", oUsuario.iStatus,
                                                                                        "@NoTelefonico", oUsuario.sNoTelefonico,
                                                                                        "@IP", null,
                                                                                        "@Usuario", Utils.GetUser);
                new DBUtils().DBSaveBitacora(Convert.ToInt32(Enumeraciones.Pantallas.Usuario), Convert.ToInt32(Enumeraciones.TipoOperacion.Actualizar), "Se actualizó el registro: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBValida(Usuarios oUsuarios)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spS_MXJ_ConsultaUsuariosExiste]", "@Descripcion", oUsuarios.sDescripcion);
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBDelete(Usuarios oUsuario)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spD_MXJ_EliminaUsuario]","@IdUsuario", oUsuario.iId,
                                                                                      "@IP", null,
                                                                                      "@Usuario", Utils.GetUser);
                new DBUtils().DBSaveBitacora(Convert.ToInt32(Enumeraciones.Pantallas.Usuario), Convert.ToInt32(Enumeraciones.TipoOperacion.Eliminar), "Se eliminó el registro: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable dtGetAeronave
        {
            get
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

        }

        public DataTable dtGetRol
        {
            get
            {
                try
                {
                    return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaCatalogoRol]");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool DBEXisteUsuarioActivo(string sUser)
        {
            try
            {
                object oRes = oDB_SP.EjecutarValor("[Seguridad].[spS_ConsultaUsuarioActivo]", "@User", sUser);
                return oRes.S() == "1" ? true : false;
            }
            catch (Exception ex) 
            {
                throw ex;
            }
        }

        public DataTable DBGetMessages
        {
            get
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaMensajesSistema]");
            }
        }

        public DataTable DBGetUsuario(string sUser) 
        {
            try
            {
                return oDB_SP.EjecutarDT("[Seguridad].[spS_MXJ_ConsultaUsuarioActivo]", "@Usuario", sUser);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}