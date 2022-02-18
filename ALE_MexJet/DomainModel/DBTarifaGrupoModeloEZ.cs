using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using NucleoBase.Core;
using ALE_MexJet.Objetos;
using ALE_MexJet.Clases;

namespace ALE_MexJet.DomainModel
{
    public class DBTarifaGrupoModeloEz : DBBase
    {
        public DataTable DBSearchObj(params object[] oArrFiltros)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaTarifasEzMexJet]", oArrFiltros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBSave(TarifaGrupoModeloEz oTarifa)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spI_MXJ_InsertaTarifaEzMexJet]", "@IdGrupoModelo", oTarifa.iGrupoModelo,
                                                                                            "@TarifaNacional", oTarifa.dTarifaNal,
                                                                                            "@TarifaInternacional", oTarifa.dTarifaInt,
                                                                                            "@IP", Utils.GetIPAddress(),
                                                                                            "@UsuarioCreacion", Utils.GetUser);
                
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBUpdate(TarifaGrupoModeloEz oTarifa)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spU_MXJ_ActualizaTarifaGrupoModeloEz]", "@IdTarifaEz", oTarifa.iId,
                                                                                                     "@IdGrupoModelo", oTarifa.iGrupoModelo,
                                                                                                     "@TarifaNacional", oTarifa.dTarifaNal,
                                                                                                     "@TarifaInternacional", oTarifa.dTarifaInt,
                                                                                                     "@Status", oTarifa.iStatus,
                                                                                                     "@IP", Utils.GetIPAddress(),
                                                                                                     "@UsuarioMod", Utils.GetUser);

                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBDelete(TarifaGrupoModeloEz oTarifaModelo)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spD_MXJ_EliminaTarifaGrupoModeloEzMexJet]", "@IdTarifaEz", oTarifaModelo.iId,
                                                                                                        "@UsuarioMod", Utils.GetUser,
                                                                                                        "@IP", Utils.GetIPAddress());
                
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBValida(TarifaGrupoModeloEz oTarifaModelo)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spS_MXJ_ConsultaTarifaGrupoModeloEzMexJet]", "@IdGrupoModelo", oTarifaModelo.iGrupoModelo);

                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}