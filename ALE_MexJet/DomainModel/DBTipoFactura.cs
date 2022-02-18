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
    public class DBTipoFactura : DBBase
    {
        public DataTable dtObjsCat
        {
            get
            {
                try
                {
                    return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaTipoFactura]");
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
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaTipoFactura]", oArrFiltros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBSave(TipoFactura oTipoFactura)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spI_MXJ_InsertaTipoFactura]",  "@TipoFactura", oTipoFactura.sTipoFactura,
                                                                                            "@Descripcion", oTipoFactura.sDescripcion,
                                                                                            "@Disponible", oTipoFactura.bDisponible,
                                                                                            "@RequierePrefactura", oTipoFactura.bRequiererePrefactura,
                                                                                            "@ApareceTabulador", oTipoFactura.bApareseTabulador,
                                                                                            "@ApareceEstadoCuenta", oTipoFactura.bApareseEstadoCuenta,
                                                                                            "@BloqueaCampos", oTipoFactura.bBloqueaCampos,
                                                                                            "@Status", oTipoFactura.iStatus,
                                                                                            "@IP", oTipoFactura.sIP,
                                                                                            "@Usuario", oTipoFactura.sUsuarioCreacion);

                new DBUtils().DBSaveBitacora(Convert.ToInt32(Enumeraciones.Pantallas.TipoFactura), Convert.ToInt32(Enumeraciones.TipoOperacion.Insertar), "Se insertó el registro: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBUpdate(TipoFactura oTipoFactura)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spu_MXJ_ActualizaTipoFactura]", "@Id", oTipoFactura.iId,
                                                                                             "@TipoFactura", oTipoFactura.sTipoFactura,
                                                                                            "@Descripcion", oTipoFactura.sDescripcion,
                                                                                            "@Disponible", oTipoFactura.bDisponible,
                                                                                            "@RequierePrefactura", oTipoFactura.bRequiererePrefactura,
                                                                                            "@ApareceTabulador", oTipoFactura.bApareseTabulador,
                                                                                            "@ApareceEstadoCuenta", oTipoFactura.bApareseEstadoCuenta,
                                                                                            "@BloqueaCampos", oTipoFactura.bBloqueaCampos,
                                                                                            "@Status", oTipoFactura.iStatus,
                                                                                            "@IP", oTipoFactura.sIP,
                                                                                            "@Usuario", oTipoFactura.sUsuarioCreacion);

                new DBUtils().DBSaveBitacora(Convert.ToInt32(Enumeraciones.Pantallas.TipoFactura), Convert.ToInt32(Enumeraciones.TipoOperacion.Actualizar), "Se actualizó el registro: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBDelete(TipoFactura oTipoFactura)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spD_MXJ_EliminaTipoFactura]", "@Id", oTipoFactura.iId,
                                                                                    "@IP", null,
                                                                                    "@Usuario", Utils.GetUser);
                new DBUtils().DBSaveBitacora(Convert.ToInt32(Enumeraciones.Pantallas.TipoFactura), Convert.ToInt32(Enumeraciones.TipoOperacion.Eliminar), "Se eliminó el registro: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBValida(TipoFactura oTipoFactura)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spS_MXJ_ConsultaTipoFacturaExiste]", "@TipoFactura", oTipoFactura.sTipoFactura,
                                                                                            "@Descripcion", oTipoFactura.sDescripcion);
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}