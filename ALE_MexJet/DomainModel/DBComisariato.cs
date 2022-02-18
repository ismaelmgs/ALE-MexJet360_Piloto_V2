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
    public class DBComisariato : DBBase
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
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaComisariato]", oArrFiltros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBSave(Comisariato oComisariato)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spI_MXJ_InsertaComisariato]", "@Descripcion", oComisariato.sDescripcion,
                                                                                        "@Status", oComisariato.iStatus,
                                                                                        "@IP", oComisariato.sIP,
                                                                                        "@Usuario", Utils.GetUser);
                new DBUtils().DBSaveBitacora(Enumeraciones.Pantallas.Comisariato.S().I(), Enumeraciones.TipoOperacion.Insertar.S().I(), "Se insertó el registro: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBUpdate(Comisariato oComisariato)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spu_MXJ_ActualizaComisariato]", "@ComisariatoId", oComisariato.iId,
                                                                                        "@Descripcion", oComisariato.sDescripcion,
                                                                                        "@Status", oComisariato.iStatus,
                                                                                        "@IP", null,
                                                                                        "@Usuario", Utils.GetUser);
                new DBUtils().DBSaveBitacora(Enumeraciones.Pantallas.Comisariato.S().I(), Enumeraciones.TipoOperacion.Actualizar.S().I(), "Se actualizó el registro: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBDelete(Comisariato oComisariato)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spD_MXJ_EliminaComisariato]", "@ComisariatoId", oComisariato.iId,
                                                                                      "@IP", null,
                                                                                      "@Usuario", Utils.GetUser);
                new DBUtils().DBSaveBitacora(Enumeraciones.Pantallas.Comisariato.S().I(), Enumeraciones.TipoOperacion.Eliminar.S().I(), "Se eliminó el registro: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBValida(Comisariato oComisariato)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spS_MXJ_ConsultaComisariatoExiste]", "@Descripcion", oComisariato.sDescripcion);

                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}