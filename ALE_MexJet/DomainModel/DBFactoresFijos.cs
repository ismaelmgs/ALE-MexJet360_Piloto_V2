
using ALE_MexJet.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using NucleoBase.Core;
using ALE_MexJet.Clases;

namespace ALE_MexJet.DomainModel
{
    public class DBFactoresFijos : DBBase
    {
        public DataTable DBSearchObj(params object[] oArrFiltros)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaFactoresFijos]", oArrFiltros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBSave(FactorFijo oFactor)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spI_MXJ_InsertarFactoresFijos]", "@Clave", oFactor.sClave,
                                                                                        "@Descripcion", oFactor.sDescripcion,
                                                                                        "@Valor", oFactor.dValor,
                                                                                        "@Sts", oFactor.iStatus,
                                                                                        "@IP", Utils.GetIPAddress(),
                                                                                        "@Usuario", Utils.GetUser);
                new DBUtils().DBSaveBitacora(Convert.ToInt32(Enumeraciones.Pantallas.FactorFijo), Convert.ToInt32(Enumeraciones.TipoOperacion.Insertar), "Se insertó el registro: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBUpdate(FactorFijo oFactor)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spU_MXJ_ActualizarFactoresFijos]", "@IdFactor", oFactor.iId,
                                                                                        "@Clave", oFactor.sClave,
                                                                                        "@Descripcion", oFactor.sDescripcion,
                                                                                        "@Valor", oFactor.dValor,
                                                                                        "@Sts", oFactor.iStatus,
                                                                                        "@IP", Utils.GetIPAddress(),
                                                                                        "@Usuario", Utils.GetUser);

                new DBUtils().DBSaveBitacora(Convert.ToInt32(Enumeraciones.Pantallas.FactorFijo), Convert.ToInt32(Enumeraciones.TipoOperacion.Insertar), "Se insertó el registro: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBDelete(FactorFijo oFactor)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spD_MXJ_EliminarFactoresFijos]", "@IdFactor", oFactor.iId,
                                                                                            "@Usuario", Utils.GetUser,
                                                                                            "@IP", Utils.GetIPAddress());

                new DBUtils().DBSaveBitacora(Convert.ToInt32(Enumeraciones.Pantallas.FactorFijo), Convert.ToInt32(Enumeraciones.TipoOperacion.Eliminar), "Se eliminó el registro: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBValida(FactorFijo oFactor)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spS_MXJ_ConsultaFactorFijoExistente]", "@Clave", oFactor.sClave);
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}