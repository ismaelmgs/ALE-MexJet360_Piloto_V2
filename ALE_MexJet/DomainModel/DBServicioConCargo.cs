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
    public class DBServicioConCargo : DBBase
    {
        public DataTable dtObjsCat
        {
            get
            {
                try
                {
                    return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaServicioConCargo]");
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
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaServicioConCargo]", oArrFiltros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBSave(ServicioConCargo oServicioConCargo)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spI_MXJ_InsertaServicioConCargo]", "@Descripcion", oServicioConCargo.sDescripcion,
                                                                                                "@ItemSAP", oServicioConCargo.sCveArticulo,
                                                                                                "@ItemSAPDescripcion", oServicioConCargo.sArticuloDescripcion,
                                                                                                //"@cve" ,oServicioConCargo.sCveCuenta,
                                                                                                //"@CuentaDesc", oServicioConCargo.sDescripcionCuenta,
                                                                                                "@CveCodigoUnidad1", oServicioConCargo.sCveCodUnitUno,
                                                                                                "@CodUnidad1Desc", oServicioConCargo.sDescripcionCodUnitUno,
                                                                                                "@Importe",oServicioConCargo.dImporte,
                                                                                                "@PorPierna",oServicioConCargo.bPierna,
                                                                                                "@PorPasajero",oServicioConCargo.bPasajero,
                                                                                                "@Status", oServicioConCargo.iStatus,
                                                                                                "@IP", oServicioConCargo.sIP,
                                                                                                "@Usuario", Utils.GetUser);
                new DBUtils().DBSaveBitacora(Convert.ToInt32(Enumeraciones.Pantallas.ServiciosConCargo), Convert.ToInt32(Enumeraciones.TipoOperacion.Insertar), "Se insertó el registro: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBUpdate(ServicioConCargo oServicioConCargo)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spu_MXJ_ActualizaServicioConCargo]","@Id", oServicioConCargo.iId,
                                                                                                "@Descripcion", oServicioConCargo.sDescripcion,
                                                                                                "@ItemSAP", oServicioConCargo.sCveArticulo,
                                                                                                "@ItemSAPDescripcion", oServicioConCargo.sArticuloDescripcion,
                                                                                                //"@cve", oServicioConCargo.sCveCuenta,
                                                                                                //"@CuentaDesc", oServicioConCargo.sDescripcionCuenta,
                                                                                                "@CveCodigoUnidad1", oServicioConCargo.sCveCodUnitUno,
                                                                                                "@CodUnidad1Desc", oServicioConCargo.sDescripcionCodUnitUno,
                                                                                                "@Importe", oServicioConCargo.dImporte,
                                                                                                "@PorPierna", oServicioConCargo.bPierna,
                                                                                                "@PorPasajero", oServicioConCargo.bPasajero,
                                                                                                "@Status", oServicioConCargo.iStatus,
                                                                                                "@IP", oServicioConCargo.sIP,
                                                                                                "@Usuario", Utils.GetUser);
                new DBUtils().DBSaveBitacora(Convert.ToInt32(Enumeraciones.Pantallas.ServiciosConCargo), Convert.ToInt32(Enumeraciones.TipoOperacion.Actualizar), "Se actualizó el registro: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBValida(ServicioConCargo oServicioConCargo)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spS_MXJ_ConsultaServicioConCargoExiste]", "@Descripcion", oServicioConCargo.sDescripcion);
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBDelete(ServicioConCargo oServicioConCargo)
        {
            try
            {

                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spD_MXJ_EliminaServicioConCargo]", "@Id", oServicioConCargo.iId,
                                                                                    "@IP", null,
                                                                                    "@Usuario", Utils.GetUser);
                new DBUtils().DBSaveBitacora(Convert.ToInt32(Enumeraciones.Pantallas.ServiciosConCargo), Convert.ToInt32(Enumeraciones.TipoOperacion.Eliminar), "Se eliminó el registro: " + oResult.S());
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetCodigoUnidadUno
        {
            get
            {
                //ALE_MexJet.wsSyteline.Iws_SyteLineClient wssyte = new wsSyteline.Iws_SyteLineClient();
                //return wssyte.GetCodigoUnidad_Uno().Tables[0];
                return new DBSAP().DBGetServiciosCC;
            }
        }

        public DataTable dtCuenta
        {
            get
            {
                //ALE_MexJet.wsSyteline.Iws_SyteLineClient wssyte = new wsSyteline.Iws_SyteLineClient();
                //return wssyte.GetCuentasContablesServicios().Tables[0];
                return new DBSAP().DBGetCuentas;
            }
        }
    }
}