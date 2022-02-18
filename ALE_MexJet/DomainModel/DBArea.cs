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
    public class DBArea : DBBase
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
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaArea]", oArrFiltros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBSave(Area oArea)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spI_MXJ_InsertaArea]", "@Descripcion", oArea.sDescripcion,
                                                                                    "@AreaEmailCC", oArea.sAreaEmailCC,
                                                                                    "@AreaEmail", oArea.sAreaEmail,
                                                                                    "@Status", oArea.iStatus,
                                                                                    "@IP", oArea.sIP,
                                                                                    "@Usuario", Utils.GetUser);

                new DBUtils().DBSaveBitacora(Convert.ToInt32(Enumeraciones.Pantallas.Area), Convert.ToInt32(Enumeraciones.TipoOperacion.Insertar), "Se insertó el registro: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBUpdate(Area oArea)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spu_MXJ_ActualizaArea]", "@Id", oArea.iId,
                                                                                    "@AreaEmailCC", oArea.sAreaEmailCC,
                                                                                    "@AreaEmail", oArea.sAreaEmail,
                                                                                    "@Descripcion", oArea.sDescripcion,
                                                                                    "@Status", oArea.iStatus,
                                                                                    "@IP", null,
                                                                                    "@Usuario", Utils.GetUser);

                new DBUtils().DBSaveBitacora(Convert.ToInt32(Enumeraciones.Pantallas.Area), Convert.ToInt32(Enumeraciones.TipoOperacion.Actualizar), "Se actualizó el registro: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBDelete(Area oArea)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spD_MXJ_EliminaArea]", "@Id", oArea.iId,
                                                                                    "@IP", null,
                                                                                    "@Usuario", Utils.GetUser);

                new DBUtils().DBSaveBitacora(Convert.ToInt32(Enumeraciones.Pantallas.Area), Convert.ToInt32(Enumeraciones.TipoOperacion.Eliminar), "Se eliminó el registro: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBValida(Area oArea)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spS_MXJ_ConsultaAreaExiste]", "@Descripcion", oArea.sDescripcion);

                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
    }

}