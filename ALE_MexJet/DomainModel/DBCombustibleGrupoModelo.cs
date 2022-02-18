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
    public class DBCombustibleGrupoModelo : DBBase
    {
        public DataTable DBSearchObj(params object[] oArrFiltros)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaCombustible]", oArrFiltros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBSearchObjGrupoModelo(params object[] oArrFiltrosGruModelo)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaGrupoModelo]", oArrFiltrosGruModelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBSearchObjContrato(params object[] oArrFiltrosCon)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Catalogos].[SpS_MXJ_ConsultaTipoContrato]", oArrFiltrosCon);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBSave(CombustibleGrupoModelo oCombustibleGrupoModelo)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spI_MXJ_InsertaCombustible]", "@IdGrupoModelo", oCombustibleGrupoModelo.iIdGrupoModelo,
                                                                                    "@IdTipoGrupo", oCombustibleGrupoModelo.iIdTipoGrupo,
                                                                                    "@IdTipoContrato", oCombustibleGrupoModelo.iIdTipoContrato,
                                                                                    "@Status", oCombustibleGrupoModelo.iStatus,
                                                                                    "@IP", oCombustibleGrupoModelo.sIP,
                                                                                    "@Usuario",Utils.GetUser);

                new DBUtils().DBSaveBitacora(Enumeraciones.Pantallas.Cliente.S().I(), Enumeraciones.TipoOperacion.Insertar.S().I(), "Se inserto el registro: " + oResult.S());

                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBUpdate(CombustibleGrupoModelo oCombustibleGrupoModelo)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spU_MXJ_ActualizaCombustible]", "@IdCombustible", oCombustibleGrupoModelo.iIdCombustible,
                                                                                        "@IdGrupoModelo", oCombustibleGrupoModelo.iIdGrupoModelo,
                                                                                        "@IdTipoGrupo", oCombustibleGrupoModelo.iIdTipoGrupo,
                                                                                        "@IdTipoContrato", oCombustibleGrupoModelo.iIdTipoContrato,
                                                                                        "@Usuario", Utils.GetUser,
                                                                                        "@IP", oCombustibleGrupoModelo.sIP,
                                                                                        "@Status",oCombustibleGrupoModelo.iStatus
                                                                                        );
                new DBUtils().DBSaveBitacora(Enumeraciones.Pantallas.Cliente.S().I(), Enumeraciones.TipoOperacion.Actualizar.S().I(), "Se actualizo el registro: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBDelete(CombustibleGrupoModelo oCombustibleGrupoModelo)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spD_MXJ_EliminaCombustible]", "@IdCombustible", oCombustibleGrupoModelo.iIdCombustible,
                                                                                        "@IP", oCombustibleGrupoModelo.sIP,
                                                                                        "@Usuario", Utils.GetUser);
                new DBUtils().DBSaveBitacora(Enumeraciones.Pantallas.Cliente.S().I(), Enumeraciones.TipoOperacion.Eliminar.S().I(), "Se elimino el registro: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBValida(CombustibleGrupoModelo oCombustibleGrupoModelo)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spS_MXJ_ConsultaCombustibleExiste]", "@IdGrupoModelo", oCombustibleGrupoModelo.iIdGrupoModelo,
                                                                                                  "@IdTipoGrupo", oCombustibleGrupoModelo.iIdTipoGrupo,
                                                                                                  "@idTipoContrato", oCombustibleGrupoModelo.iIdTipoContrato,
                                                                                                  "@estatus", oCombustibleGrupoModelo.iStatus);

                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}