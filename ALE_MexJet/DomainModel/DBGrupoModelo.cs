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
    public class DBGrupoModelo : DBBase
    {

        public DataTable DBSearchObj(params object[] oArrFiltros)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaGrupoModelo]", oArrFiltros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBSave(GrupoModelo oGrupoModelo)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spI_MXJ_InsertaGrupoModelo]", "@Descripcion", oGrupoModelo.sDescripcion,
                                                                                           "@ConsumoGalones",oGrupoModelo.iGalones,
                                                                                           "@Tarifa", oGrupoModelo.dTarifa,
                                                                                           "@NoPasajeros", oGrupoModelo.iNoPasajeros,
                                                                                           "@Status", oGrupoModelo.iStatus,
                                                                                           "@IP", oGrupoModelo.sIP,
                                                                                           "@Usuario", Utils.GetUser);
                new DBUtils().DBSaveBitacora(Convert.ToInt32(Enumeraciones.Pantallas.GrupoModelo), Convert.ToInt32(Enumeraciones.TipoOperacion.Insertar), "Se insertó el registro: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBUpdate(GrupoModelo oGrupoModelo)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spU_MXJ_ActualizaGrupoModelo]", "@GrupoModeloId", oGrupoModelo.iId,
                                                                                             "@Descripcion", oGrupoModelo.sDescripcion,
                                                                                             "@ConsumoGalones", oGrupoModelo.iGalones,
                                                                                             "@Tarifa", oGrupoModelo.dTarifa,
                                                                                             "@NoPasajeros", oGrupoModelo.iNoPasajeros,
                                                                                             "@Status", oGrupoModelo.iStatus,
                                                                                             "@IP", null,
                                                                                             "@Usuario", Utils.GetUser);
                new DBUtils().DBSaveBitacora(Convert.ToInt32(Enumeraciones.Pantallas.GrupoModelo), Convert.ToInt32(Enumeraciones.TipoOperacion.Actualizar), "Se actualizó el registro: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBDelete(GrupoModelo oGrupoModelo)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spD_MXJ_EliminaGrupoModelo]", "@GrupoModeloId", oGrupoModelo.iId,
                                                                                           "@IP", null,
                                                                                           "@Usuario", Utils.GetUser);
                new DBUtils().DBSaveBitacora(Convert.ToInt32(Enumeraciones.Pantallas.GrupoModelo), Convert.ToInt32(Enumeraciones.TipoOperacion.Eliminar), "Se eliminó el registro: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBValida(GrupoModelo oGrupoModelo)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spS_MXJ_ConsultaGrupoModeloExiste]", "@Descripcion", oGrupoModelo.sDescripcion);

                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}