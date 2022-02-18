using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using ALE_MexJet.Objetos;
using NucleoBase.Core;
using ALE_MexJet.Clases;

namespace ALE_MexJet.DomainModel
{
    public class DBGastoInterno : DBBase
    {
        public DataTable DBSearchCliente(params object[] oArrFiltros)
        {
            try
            {
                return oDB_SP.EjecutarDT("CATALOGOS.spS_MXJ_ClienteContratoGastosInternos", oArrFiltros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBSearchObjConcepto(params object[] oArrFiltros)
        {
            try 
            {                
                return  oDB_SP.EjecutarDT("CATALOGOS.spS_MXJ_ConceptoGastosInternos", oArrFiltros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBSearchContrato(params object[] oArrFiltros)
        {
            try
            {
                return oDB_SP.EjecutarDT("CATALOGOS.spS_MXJ_ClienteContrato", oArrFiltros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBSearchDetalleGastoInterno(params object[] oArrFiltros)
        {
            try
            {
                return oDB_SP.EjecutarDT("PRINCIPALES.spS_MXJ_DetalleGastosInternos", oArrFiltros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBSearchTipoMoneda(params object[] oArrFiltros)
        {
            try
            {
                return oDB_SP.EjecutarDT("CATALOGOS.spS_MXJ_ConsultaTipoMoneda", oArrFiltros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBSearchMatriculaAeronave(params object[] oArrFiltros)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaMatriculaAeronave]", oArrFiltros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable DBSearchPaqueteGrupomodelo(params object[] oArrFiltros)
        {
            try
            {
                return oDB_SP.EjecutarDT("[PRINCIPALES].[spS_MXJ_ConsultaPaqueteGrupoModelo]", oArrFiltros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable DBSearchTipoFactura()
        {
            try 
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaTipoFactura]", "@Descripcion", "%%",
                                                                                        "@estatus", 1);
            }
            catch (Exception ex)
            { 
                throw ex; 
            }
        }



        public int DBInsertaGastoInterno(GastoInterno oGastoInterno)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Catalogos].[spI_MXJ_InsertaGastosInternos]",   "@NombreCliente", oGastoInterno.sNombreCliente,
                                                                                                "@ClaveContrato", oGastoInterno.sClaveContrato,                                                                                                                                                                                                
                                                                                                "@Concepto", oGastoInterno.sDescripcionConcepto,
                                                                                                "@IdTipoMovimiento", oGastoInterno.iIdTipoMovimiento,
                                                                                                "@TipoMovimiento", oGastoInterno.sTipoMovimiento,
                                                                                                "@Importe", oGastoInterno.dImporte,
                                                                                                "@IVA", oGastoInterno.dIVA,
                                                                                                "@Total", oGastoInterno.dTotal,
                                                                                                "@idTipoMoneda", oGastoInterno.iIdTipoMoneda,
                                                                                                "@IdAeronaveMatricula",oGastoInterno.iIdAeronaveMatricula,
                                                                                                "@FechaGasto", oGastoInterno.dtFechaGasto,
                                                                                                "@Status", oGastoInterno.iStatus,
                                                                                                "@Usuario", Utils.GetUser,
                                                                                                "@IP", oGastoInterno.sIP                                                                                                
                                                                                                );

                //new DBUtils().DBSaveBitacora(Enumeraciones.Pantallas.Cliente.S().I(), Enumeraciones.TipoOperacion.Insertar.S().I(), "Se insertó el registro: " + oResult.S());

                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBValidaImporteGastoInterno(GastoInterno oGastoInterno)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("Catalogos.spI_MXJ_ValidaImporteGastosInternos", "@NombreCliente", oGastoInterno.sNombreCliente,
                                                                                                "@ClaveContrato", oGastoInterno.sClaveContrato,                                                                                               
                                                                                                "@Importe", oGastoInterno.dImporte                                                                                                
                                                                                                );

                //new DBUtils().DBSaveBitacora(Enumeraciones.Pantallas.Cliente.S().I(), Enumeraciones.TipoOperacion.Insertar.S().I(), "Se insertó el registro: " + oResult.S());

                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        } 

        public int DBEliminaGastoInterno(GastoInterno oGastoInterno)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("Catalogos.spD_MXJ_EliminaGastosInternos",
                                                                                                "@IDGastoInterno", oGastoInterno.iIdGastoInterno
                                                                                                );

                //new DBUtils().DBSaveBitacora(Enumeraciones.Pantallas.Cliente.S().I(), Enumeraciones.TipoOperacion.Insertar.S().I(), "Se insertó el registro: " + oResult.S());

                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBActualizaGastoInterno(GastoInterno oGastoInterno)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("Catalogos.spU_MXJ_ActualizaGastosInternos",
                                                                                                "@IDGastoInterno", oGastoInterno.iIdGastoInterno,
                                                                                                "@IdTipoMovimiento", oGastoInterno.iIdTipoMovimiento,
                                                                                                "@TipoMovimiento", oGastoInterno.sTipoMovimiento,
                                                                                                "@Concepto", oGastoInterno.sDescripcionConcepto,
                                                                                                "@Importe", oGastoInterno.dImporte,
                                                                                                "@IVA", oGastoInterno.dIVA,
                                                                                                "@Total", oGastoInterno.dTotal,
                                                                                                "@IDTipoMoneda", oGastoInterno.iIdTipoMoneda,
                                                                                                "@IdAeronaveMatricula",oGastoInterno.iIdAeronaveMatricula,
                                                                                                "@FechaGasto",oGastoInterno.dtFechaGasto,
                                                                                                "@Status", oGastoInterno.iStatus,
                                                                                                "@Usuario", Utils.GetUser,
                                                                                                "@IP", oGastoInterno.sIP 
                                                                                                );
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          
                //new DBUtils().DBSaveBitacora(Enumeraciones.Pantallas.Cliente.S().I(), Enumeraciones.TipoOperacion.Insertar.S().I(), "Se insertó el registro: " + oResult.S());

                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}