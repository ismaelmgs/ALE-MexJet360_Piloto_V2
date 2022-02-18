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
    public class DBPaqueteCuenta : DBBase
    {
        public DataTable DBSearchObj(params object[] oArrFiltros)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaPaqueteCuenta]", oArrFiltros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBSave(PaqueteCuenta oPaqueteCuenta)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spI_MXJ_InsertaPaqueteCuenta]", "@IdTipoPaquete", oPaqueteCuenta.iIdTipoPaquete,
                                                                                             "@Descripcion", oPaqueteCuenta.sDescripcion,
                                                                                             "@ClaveCuenta", oPaqueteCuenta.sClaveCuenta,
                                                                                             "@Status", oPaqueteCuenta.iStatus,
                                                                                             "@IP", oPaqueteCuenta.sIP,
                                                                                             "@Usuario", Utils.GetUser);

                new DBUtils().DBSaveBitacora(Enumeraciones.Pantallas.Cliente.S().I(), Enumeraciones.TipoOperacion.Insertar.S().I(), "Se inserto el registro: " + oResult.S());

                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBUpdate(PaqueteCuenta oPaqueteCuenta)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spU_MXJ_ActualizaPaqueteCuenta]", "@IdTipoPaqueteCuenta", oPaqueteCuenta.iIdTipoPaqueteCuenta,
                                                                                               "@Descripcion", oPaqueteCuenta.sDescripcion,
                                                                                               "@ClaveCuenta", oPaqueteCuenta.sClaveCuenta,
                                                                                               "@Status", oPaqueteCuenta.iStatus,
                                                                                               "@Usuario", Utils.GetUser,
                                                                                               "@IP", oPaqueteCuenta.sIP
                                                                                        );
                new DBUtils().DBSaveBitacora(Enumeraciones.Pantallas.Cliente.S().I(), Enumeraciones.TipoOperacion.Actualizar.S().I(), "Se actualizo el registro: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBDelete(PaqueteCuenta oPaqueteCuenta)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spD_MXJ_EliminaPaqueteCuenta]", "@IdTipoPaqueteCuenta", oPaqueteCuenta.iIdTipoPaqueteCuenta,
                                                                                             "@IP", oPaqueteCuenta.sIP,
                                                                                             "@Usuario", Utils.GetUser);
                new DBUtils().DBSaveBitacora(Enumeraciones.Pantallas.Cliente.S().I(), Enumeraciones.TipoOperacion.Eliminar.S().I(), "Se elimino el registro: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBValida(PaqueteCuenta oPaqueteCuenta)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spS_MXJ_ConsultaPaqueteCuentaExiste]", "@IdTipoPaquete", oPaqueteCuenta.iIdTipoPaquete,
                                                                                                    "@Descripcion", oPaqueteCuenta.sDescripcion,
                                                                                                    "@ClaveCuenta" , oPaqueteCuenta.sClaveCuenta,
                                                                                                    "@estatus", oPaqueteCuenta.iStatus);
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public  DataTable dtCuenta
        {
            get
            {
                ALE_MexJet.wsSyteline.Iws_SyteLineClient wssyte = new wsSyteline.Iws_SyteLineClient();
                return wssyte.GetCuentasContables().Tables[0];
            }
        }
            
    }
}