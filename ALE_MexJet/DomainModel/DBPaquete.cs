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
    public class DBPaquete : DBBase
    {
        public DataTable DBSearchObj(params object[] oArrFiltros)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaPaquete]", oArrFiltros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBSave(Paquete oPaquete)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spI_MXJ_InsertaPaquete]", "@Descripcion", oPaquete.sDescripcion,
                                                                                       "@MexJet", oPaquete.iMexJet,
                                                                                       "@EzMexJet", oPaquete.iEzMexJet,
                                                                                       "@ProyectoSAP", oPaquete.sProyectoSAP,
                                                                                       "@DescProyectoSAP", oPaquete.sDescProyectoSAP,
                                                                                       "@Status", oPaquete.iStatus,
                                                                                       "@IP", oPaquete.sIP,
                                                                                       "@Usuario", Utils.GetUser);

                new DBUtils().DBSaveBitacora(Enumeraciones.Pantallas.Paquete.S().I(), Enumeraciones.TipoOperacion.Insertar.S().I(), "Se inserto el registro: " + oResult.S());

                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBUpdate(Paquete oPaquete)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spU_MXJ_ActualizaPaquete]", "@IdTipoPaquete", oPaquete.iIdTipoPaquete,
                                                                                         "@Descripcion", oPaquete.sDescripcion,
                                                                                         "@MexJet", oPaquete.iMexJet,
                                                                                         "@EzMexJet", oPaquete.iEzMexJet,
                                                                                         "@ProyectoSAP", oPaquete.sProyectoSAP,
                                                                                         "@DescProyectoSAP", oPaquete.sDescProyectoSAP,
                                                                                         "@Status", oPaquete.iStatus,
                                                                                         "@Usuario", Utils.GetUser,
                                                                                         "@IP", oPaquete.sIP
                                                                                        );
                new DBUtils().DBSaveBitacora(Enumeraciones.Pantallas.Paquete.S().I(), Enumeraciones.TipoOperacion.Actualizar.S().I(), "Se actualizo el registro: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBDelete(Paquete oPaquete)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spD_MXJ_EliminaPaquete]", "@IdTipoPaquete", oPaquete.iIdTipoPaquete,
                                                                                        "@IP", oPaquete.sIP,
                                                                                        "@Usuario", Utils.GetUser);
                new DBUtils().DBSaveBitacora(Enumeraciones.Pantallas.Paquete.S().I(), Enumeraciones.TipoOperacion.Eliminar.S().I(), "Se elimino el registro: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBValida(Paquete oPaquete)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spS_MXJ_ConsultaPaqueteExiste]", "@Descripcion", oPaquete.sDescripcion,
                                                                                              "@estatus", oPaquete.iStatus);

                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}