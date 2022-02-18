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
    public class DBPersonaDifusion : DBBase
    {
        public DataTable DBSearchObj(params object[] oArrFiltros)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaPersonasDifusion]", oArrFiltros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public PersonaDifusion DBObtienePersonaDifusion(int iIdPersonaDifusion)
        {
            try
            {

                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaPersonaDifusion]", "@ID", iIdPersonaDifusion).AsEnumerable().
                    Select(r => new PersonaDifusion()
                    {
                        iId = r.S("IdPersonaDifusion").I(),
                        sNombre = r.S("Nombre"),
                        sApellidoPaterno = r.S("ApellidoPaterno"),
                        sApellidoMaterno = r.S("ApellidoMaterno"),
                        sTelefonoMovil = r.S("TelefonoMovil"),
                        sCorreoElectronico = r.S("CorreoElectronico"),
                        iIdTitulo = r.S("IdTitulo").I(),
                        iIdTipoPersona = r.S("IdTipoPersona").I(),
                        iIdTipoContacto = r.S("IdTipoContacto").I(),
                        iCorreos = r.S("Correos").I(),
                        iSMS = r.S("SMS").I(),
                        iPublicidad = r.S("Publicidad").I(),
                        iLLamadas = r.S("LLamadas").I(),
                        iStatus = r.S("Status").I()
                    }
                    ).First();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBSave(PersonaDifusion oPersonaDifusion)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spI_MXJ_InsertaPersonaDifusion]", "@Nombre", oPersonaDifusion.sNombre,
                                                                                                "@ApellidoPaterno",oPersonaDifusion.sApellidoPaterno,
                                                                                                "@ApellidoMaterno",oPersonaDifusion.sApellidoMaterno,
                                                                                                "@TelefonoMovil",oPersonaDifusion.sTelefonoMovil,
                                                                                                "@CorreoElectronico",oPersonaDifusion.sCorreoElectronico,
                                                                                                "@IdTitulo",oPersonaDifusion.iIdTitulo,
                                                                                                "@IdTipoPersona",oPersonaDifusion.iIdTipoPersona,
                                                                                                "@IdTipoContacto",oPersonaDifusion.iIdTipoContacto,
                                                                                                "@Correos",oPersonaDifusion.iCorreos,
                                                                                                "@SMS",oPersonaDifusion.iSMS,
                                                                                                "@Publicidad",oPersonaDifusion.iPublicidad,
                                                                                                "@LLamadas", oPersonaDifusion.iLLamadas,
                                                                                                "@Status", oPersonaDifusion.iStatus,
                                                                                                "@IP", oPersonaDifusion.sIP,
                                                                                                "@Usuario", Utils.GetUser);
                new DBUtils().DBSaveBitacora(Convert.ToInt32(Enumeraciones.Pantallas.Titulo), Convert.ToInt32(Enumeraciones.TipoOperacion.Insertar), "Se insertó el registro: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBSavePersonaListaDifusion(params object[] oArrFiltrosPersonaListaDifusion)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spI_MXJ_InsertaPersonaListaDifusion]", oArrFiltrosPersonaListaDifusion);
                new DBUtils().DBSaveBitacora(Convert.ToInt32(Enumeraciones.Pantallas.Titulo), Convert.ToInt32(Enumeraciones.TipoOperacion.Insertar), "Se insertó el registro: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBUpdate(PersonaDifusion oPersonaDifusion)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spu_MXJ_ActualizaPersonaDifusion]", "@Id", oPersonaDifusion.iId,
                                                                                                 "@Nombre", oPersonaDifusion.sNombre,
                                                                                                 "@ApellidoPaterno", oPersonaDifusion.sApellidoPaterno,
                                                                                                 "@ApellidoMaterno", oPersonaDifusion.sApellidoMaterno,
                                                                                                 "@TelefonoMovil", oPersonaDifusion.sTelefonoMovil,
                                                                                                 "@CorreoElectronico", oPersonaDifusion.sCorreoElectronico,
                                                                                                 "@IdTitulo", oPersonaDifusion.iIdTitulo,
                                                                                                 "@IdTipoPersona", oPersonaDifusion.iIdTipoPersona,
                                                                                                 "@IdTipoContacto", oPersonaDifusion.iIdTipoContacto,
                                                                                                 "@Correos", oPersonaDifusion.iCorreos,
                                                                                                 "@SMS", oPersonaDifusion.iSMS,
                                                                                                 "@Publicidad", oPersonaDifusion.iPublicidad,
                                                                                                 "@LLamadas", oPersonaDifusion.iLLamadas,
                                                                                                 "@Status", oPersonaDifusion.iStatus,
                                                                                                 "@IP", null,
                                                                                                 "@Usuario", Utils.GetUser);
                new DBUtils().DBSaveBitacora(Convert.ToInt32(Enumeraciones.Pantallas.Titulo), Convert.ToInt32(Enumeraciones.TipoOperacion.Actualizar), "Se actualizó el registro: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBDelete(int iIdPersonaDifusion)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spD_MXJ_EliminaPersonaDifusion]", "@Id", iIdPersonaDifusion,
                                                                                      "@IP", null,
                                                                                      "@Usuario", Utils.GetUser);
                new DBUtils().DBSaveBitacora(Convert.ToInt32(Enumeraciones.Pantallas.Titulo), Convert.ToInt32(Enumeraciones.TipoOperacion.Eliminar), "Se eliminó el registro: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBDeletePersonaListaDifusion(params object[] oArrFiltrosPersonaListaDifusion)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spD_MXJ_EliminaPersonaListaDifusion]", oArrFiltrosPersonaListaDifusion);
                new DBUtils().DBSaveBitacora(Convert.ToInt32(Enumeraciones.Pantallas.Titulo), Convert.ToInt32(Enumeraciones.TipoOperacion.Eliminar), "Se eliminó el registro: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBObtieneTitulos()
        {
            try
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaTitulo]", "@Descripcion", "%%", "@estatus", 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBObtieneTipoPersona()
        {
            try
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaTipoPersona]", "@Descripcion", "%%", "@estatus", 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBObtieneTipoContacto()
        {
            try
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaTipoContacto]", "@Id", 0,"@Descripcion", "%%", "@estatus", 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBObtienePersonaListaDifusion(int iIdPersonaDifusion)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaPersonaListaDifusion]", "@IdPersonaDifusion", iIdPersonaDifusion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}