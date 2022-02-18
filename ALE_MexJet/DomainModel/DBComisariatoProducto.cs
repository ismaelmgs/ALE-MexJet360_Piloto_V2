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
    public class DBComisariatoProducto : DBBase
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
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaComisariatoProducto]", oArrFiltros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBSave(ComisariatoProducto oComisariatoProducto)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spI_MXJ_InsertaComisariatoProducto]", "@ComisariatoId", oComisariatoProducto.iIdComisariato,
                                                                                                   "@IdProducto", oComisariatoProducto.iIdProducto, 
                                                                                                   "@Cantidad",oComisariatoProducto.iCantidad,
                                                                                                   "@Status", oComisariatoProducto.iStatus,
                                                                                                   "@IP", oComisariatoProducto.sIP,
                                                                                                   "@Usuario", Utils.GetUser);
                new DBUtils().DBSaveBitacora(Enumeraciones.Pantallas.ComisariatoProducto.S().I(), Enumeraciones.TipoOperacion.Insertar.S().I(), "Se insertó el registro: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBUpdate(ComisariatoProducto oComisariatoProducto)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spu_MXJ_ActualizaComisariatoProducto]", "@Id", oComisariatoProducto.iId,
                                                                                                     "@ComisariatoId", oComisariatoProducto.iIdComisariato,
                                                                                                     "@IdProducto", oComisariatoProducto.iIdProducto,
                                                                                                     "@Cantidad", oComisariatoProducto.iCantidad,
                                                                                                     "@Status", oComisariatoProducto.iStatus,
                                                                                                     "@IP", oComisariatoProducto.sIP,
                                                                                                     "@Usuario", Utils.GetUser);
                new DBUtils().DBSaveBitacora(Enumeraciones.Pantallas.ComisariatoProducto.S().I(), Enumeraciones.TipoOperacion.Actualizar.S().I(), "Se actualizó el registro: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBDelete(ComisariatoProducto oComisariatoProducto)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spD_MXJ_EliminaComisariatoProducto]", "@Id", oComisariatoProducto.iId,
                                                                                                   "@IP", null,
                                                                                                   "@Usuario", Utils.GetUser);
                new DBUtils().DBSaveBitacora(Enumeraciones.Pantallas.ComisariatoProducto.S().I(), Enumeraciones.TipoOperacion.Eliminar.S().I(), "Se eliminó el registro: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable dtObjsComisariato
        {
            get
            {
                try
                {
                    return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaCatalogoComisariato]");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable dtObjsProducto
        {
            get
            {
                try
                {
                    return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaCatalogoProducto]");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

    }
}