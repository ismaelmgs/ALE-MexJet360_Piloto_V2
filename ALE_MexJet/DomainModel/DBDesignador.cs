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
    public class DBDesignador : DBBase
    {
        public DataTable dtObjsCat
        {
            get
            {
                try
                {
                    return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaDesignador]");
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
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaDesignador]", oArrFiltros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBSave(Designador oDesignador)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spI_MXJ_InsertaDesignador]", "@Clave", oDesignador.sClave,
                                                                                          "@Descripcion", oDesignador.sDescripcion,
                                                                                          "@Envergadura",oDesignador.dEnvergadura,
                                                                                          "@CuotaPorKilometro" ,oDesignador.dCuotaKilometro,
                                                                                          "@Status", oDesignador.iStatus,
                                                                                          "@IP", oDesignador.sIP,
                                                                                          "@Usuario", Utils.GetUser);
                new DBUtils().DBSaveBitacora(Convert.ToInt32(Enumeraciones.Pantallas.Designador), Convert.ToInt32(Enumeraciones.TipoOperacion.Insertar), "Se insertó el registro: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBUpdate(Designador oDesignador)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spu_MXJ_ActualizaDesignador]", "@Id", oDesignador.iId,
                                                                                            "@Clave", oDesignador.sClave,
                                                                                            "@Descripcion", oDesignador.sDescripcion,
                                                                                            "@Envergadura", oDesignador.dEnvergadura,
                                                                                            "@CuotaPorKilometro", oDesignador.dCuotaKilometro,
                                                                                            "@Status", oDesignador.iStatus,
                                                                                            "@IP", null,
                                                                                            "@Usuario", Utils.GetUser);
                new DBUtils().DBSaveBitacora(Convert.ToInt32(Enumeraciones.Pantallas.Designador), Convert.ToInt32(Enumeraciones.TipoOperacion.Actualizar), "Se actualizó el registro: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBDelete(Designador oDesignador)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spD_MXJ_EliminaDesignador]", "@Id", oDesignador.iId,
                                                                                    "@IP", null,
                                                                                    "@Usuario", Utils.GetUser);
                new DBUtils().DBSaveBitacora(Convert.ToInt32(Enumeraciones.Pantallas.Designador), Convert.ToInt32(Enumeraciones.TipoOperacion.Eliminar), "Se eliminó el registro: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBValida(Designador oDesignador)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spS_MXJ_ConsultaDesignadorExiste]", "@Clave", oDesignador.sClave,
                                                                                                 "@Descripcion", oDesignador.sDescripcion);

                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}