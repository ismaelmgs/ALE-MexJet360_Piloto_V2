using ALE_MexJet.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using NucleoBase.Core;

namespace ALE_MexJet.DomainModel
{
    public class DBIntercambioHoras:DBBase
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

        public DataTable DBSearchIntercambioHoras(params object[] oArrFiltros)
        {
            try
            {
                return oDB_SP.EjecutarDT("Principales.spS_MXJ_IntercambioHoras", oArrFiltros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Busca los contratos ligados al cliente, apartir del nombre del cliente
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

        public string DBSearchHorasDisponibles(params object[] oArrFiltros)
        {
            try
            {
                string sCad = string.Format("SELECT Principales.fn_MXJ_ObtenerHorasDisponibles_S({0},{1})", oArrFiltros);

                return oDB_SP.EjecutarValor_DeQuery(sCad).S();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DBInsertaTraspasoHoras(TraspasoHoras oTraspasoHoras)
        {
            try {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Principales].[spI_MXJ_InsertaTraspasoHoras]",  "@IdClienteOrigen", oTraspasoHoras.iIdClienteOrigen,
                                                                                                "@IdContratoOrigen", oTraspasoHoras.iIdContratoOrigen,
                                                                                                "@IdClienteDestino", oTraspasoHoras.iIdClienteDestino,
                                                                                                "@IdContratoDestino", oTraspasoHoras.iIdContratoDestino,
                                                                                                "@HorasIntercambiadas", oTraspasoHoras.sHorasIntercambiadas,
                                                                                                "@Observaciones", oTraspasoHoras.sObservaciones,
	                                                                                            "@Status",1,
	                                                                                            "@UsuarioCreacion", oTraspasoHoras.sUsuarioCreacion);
                return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

		public int DBUpdateTraspasoHoras(TraspasoHoras oTraspasoHoras)
		{
			try
			{
				object oResult;
				oResult = oDB_SP.EjecutarValor("[Principales].[spU_MXJ_ActualizaIntercambioHoras]", "@IdIntercambio", oTraspasoHoras.iIdIntercambioHoras,
																								"@IdClienteOrigen", oTraspasoHoras.iIdClienteOrigen,
																								"@IdContratoOrigen", oTraspasoHoras.iIdContratoOrigen,
																								"@IdClienteDestino", oTraspasoHoras.iIdClienteDestino,
																								"@IdContratoDestino", oTraspasoHoras.iIdContratoDestino,
																								"@HorasIntercambiadas", oTraspasoHoras.sHorasIntercambiadas,
																								"@Observaciones", oTraspasoHoras.sObservaciones,
																								"@Status", 1,
																								"@UsuarioModificacion", oTraspasoHoras.sUsuarioModificacion);
				return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
 
		public int DBDelete(TraspasoHoras oTraspasoHoras)
		{
			try
			{
				object oResult;
				oResult = oDB_SP.EjecutarValor("[Principales].[spD_MXJ_EliminaIntercambioHoras]", "@Id", oTraspasoHoras.iIdIntercambioHoras,
																								"@Usuario", oTraspasoHoras.sUsuarioModificacion,
																								"@IP", null);
				return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
    }
}