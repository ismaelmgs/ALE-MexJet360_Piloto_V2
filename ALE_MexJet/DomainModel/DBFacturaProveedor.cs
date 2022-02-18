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
    public class DBFacturaProveedor : DBBase
    {
        public DataTable dtObjsFacProveedor(params object[] oArrFiltros)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultarFacturaProveddor]", oArrFiltros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable dtObjCliente()
        {
            try
            {
                return oDB_SP.EjecutarDT("Catalogos.spS_MXJ_ConsultaClienteDDL", "@status",1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable dtObjMatricula()
        {
            try
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaMatriculaProveDDL]");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable dtObjTipoMoneda()
        {
            try
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaTipoMoneda]", "@IdTipoMoneda", 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable dtObjBitacoraMatricula(params object[] oArrMatricula)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaBitacoraPorMatricula]", oArrMatricula);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBSave(FacturaProveedor oFacutura)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Principales].[spI_MXJ_InsertaFacturaProveedor]",
                                                                                    "@Provedor", oFacutura.Provedor,
                                                                                    "@Factura", oFacutura.Factura,
                                                                                    "@Subtotal", oFacutura.Subtotal,
                                                                                    "@IVA", oFacutura.IVA,
                                                                                    "@Total", oFacutura.Total,
                                                                                    "@TipoMoneda", oFacutura.TipoMoneda,
                                                                                    "@Status", oFacutura.Status,
                                                                                    "@UsuarioCreacion", oFacutura.UsuarioCreacion,
                                                                                    "@IP", oFacutura.IP,
                                                                                    "@FechaFactura", oFacutura.FechaFactura,
                                                                                    "@TipoCambio", oFacutura.TipoCambio
                                                                                    );

                //new DBUtils().DBSaveBitacora(Enumeraciones.Pantallas.Cliente.S().I(), Enumeraciones.TipoOperacion.Insertar.S().I(), "Se insertó el registro: " + oResult.S());

                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBSaveFacturaDetalle(params object[] OArrProvDet)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Principales].[spI_MXJ_InsertaFacturaProveedorDetalle]", OArrProvDet
                                                                                    );

                //new DBUtils().DBSaveBitacora(Enumeraciones.Pantallas.Cliente.S().I(), Enumeraciones.TipoOperacion.Insertar.S().I(), "Se insertó el registro: " + oResult.S());

                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable dtSearchFacProvDetalle(params object[] oArrFilDetalle)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultarFacturaProveddorDetalle]", oArrFilDetalle);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBUpdateFacturaDetalle(FacturaProveedor oFacutura)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Principales].[spU_MXJ_ActualizaFacturaProveedorDetalle]",
                                                                                    "@IdFacturaProveedorDetalle", oFacutura.IdFaturaProveedorDetalle,
                                                                                    "@Matricula", oFacutura.Matricula,
                                                                                    "@FolioReal", oFacutura.FolioReal,
                                                                                    "@UsuarioModificacion", oFacutura.UsuarioCreacion,
                                                                                    "@IP", oFacutura.IP
                                                                                    );

                //new DBUtils().DBSaveBitacora(Enumeraciones.Pantallas.Cliente.S().I(), Enumeraciones.TipoOperacion.Insertar.S().I(), "Se insertó el registro: " + oResult.S());

                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBEliminaFacturaDetalle(FacturaProveedor oFacutura)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Principales].[spD_MXJ_EliminaFacturaProveedorDetalle]",
                                                                                    "@IdFacturaProveedorDetalle", oFacutura.IdFaturaProveedorDetalle
                                                                                    );

                //new DBUtils().DBSaveBitacora(Enumeraciones.Pantallas.Cliente.S().I(), Enumeraciones.TipoOperacion.Insertar.S().I(), "Se insertó el registro: " + oResult.S());

                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBEliminaFactura(params object[] oArrDeleteFac)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Principales].[spD_MXJ_EliminaFacturaProveedor]", oArrDeleteFac);
                //new DBUtils().DBSaveBitacora(Enumeraciones.Pantallas.Cliente.S().I(), Enumeraciones.TipoOperacion.Insertar.S().I(), "Se insertó el registro: " + oResult.S());

                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable dtObjProvED(params object[] oArrProvED)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultarFacturaProveddorED]", oArrProvED);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable dtObjsPiernaRentadas()
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaPiernasRentadas]");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}