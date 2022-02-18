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
    public class DBFechaPico : DBBase
    {
        public DataTable DBSearchObj(params object[] oArrFiltros)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaFechaPico]", oArrFiltros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBSave(FechaPico oFechaPico)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spI_MXJ_InsertaFechaPico]", "@Fecha", oFechaPico.datFecha,
                                                                                         "@Status", oFechaPico.iStatus,
                                                                                         "@IP", oFechaPico.sIP,
                                                                                         "@Usuario", Utils.GetUser);

                new DBUtils().DBSaveBitacora(Enumeraciones.Pantallas.Cliente.S().I(), Enumeraciones.TipoOperacion.Insertar.S().I(), "Se inserto el registro: " + oResult.S());

                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBUpdate(FechaPico oFechaPico)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spU_MXJ_ActualizaFechaPico]", "@IdFechaPico", oFechaPico.iIdFechaPico,
                                                                                           "@Fecha", oFechaPico.datFecha,
                                                                                           "@Usuario", Utils.GetUser,
                                                                                           "@IP", oFechaPico.sIP
                                                                                        );
                new DBUtils().DBSaveBitacora(Enumeraciones.Pantallas.Cliente.S().I(), Enumeraciones.TipoOperacion.Actualizar.S().I(), "Se actualizo el registro: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBDelete(FechaPico oFechaPico)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spD_MXJ_EliminaFechaPico]", "@IdFechaPico", oFechaPico.iIdFechaPico,
                                                                                         "@IP", oFechaPico.sIP,
                                                                                         "@Usuario", Utils.GetUser);
                new DBUtils().DBSaveBitacora(Enumeraciones.Pantallas.Cliente.S().I(), Enumeraciones.TipoOperacion.Eliminar.S().I(), "Se elimino el registro: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBValida(FechaPico oFechaPico)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spS_MXJ_ConsultaFechaPicoExiste]", "@Fecha", oFechaPico.datFecha,
                                                                                                "@estatus", oFechaPico.iStatus);
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}