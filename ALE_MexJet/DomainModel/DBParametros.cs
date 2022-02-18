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
    public class DBParametros : DBBase
    {
        public DataTable DBSearchObj(params object[] oArrFiltros)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaParametros]", oArrFiltros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBSave(Parametros oParametros)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spI_MXJ_InsertaParametros]", "@Clave", oParametros.sClave,
                                                                                    "@Descripcion", oParametros.sDescripcion,
                                                                                    "@Valor", oParametros.sValor,
                                                                                    "@estatus", oParametros.iStatus,
                                                                                    "@Usuario", Utils.GetUser,
                                                                                    "@IP", oParametros.sIP
                                                                                    );
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBUpdate(Parametros oParametros)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spu_MXJ_ActualizaParametros]", "@IdParametro", oParametros.iIdParametro,
                                                                                        "@Clave", oParametros.sClave,
                                                                                        "@Descripcion", oParametros.sDescripcion,
                                                                                        "@Valor", oParametros.sValor,
                                                                                        "@estatus", oParametros.iStatus,
                                                                                        "@Usuario", Utils.GetUser,
                                                                                        "@IP", oParametros.sIP
                                                                                        );//piloto.sUsuarioCreacion
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBDelete(Parametros oParametros)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spD_MXJ_EliminaParametro]", "@IdParametro", oParametros.iIdParametro,
                                                                                      "@IP", oParametros.sIP,
                                                                                      "@Usuario", Utils.GetUser);
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable dtTipoCliente
        {
            get
            {
                try
                {
                    return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaTipoCliente]");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public string DBGetObtieneValorParametroPorClave(string sClave)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaValorParametro]", "@Clave", sClave).Rows[0][0].S();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}