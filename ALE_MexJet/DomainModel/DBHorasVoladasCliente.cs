using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ALE_MexJet.DomainModel
{
	public class DBHorasVoladasCliente : DBBase
	{
		public DataSet dsObjHorasVoladas(params object[] oArrFiltros)
		{
			try
			{
				return oDB_SP.EjecutarDS("[Principales].[spS_MXJ_ReporteHorasVoladasCliente]", oArrFiltros);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		internal DataSet dsObjHorasVoladasCosto(object[] oArrFiltros)
		{
			try
			{
				return oDB_SP.EjecutarDS("[Principales].[spS_MXJ_ReporteHorasVoladasClienteCosto]", oArrFiltros);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		internal DataTable dtObjCliente()
		{
			try
			{
				return oDB_SP.EjecutarDT("[Catalogos].[spS_MXJ_ConsultaClienteDDL]", "@status", 1);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		internal DataTable dtObjContrato(params object[] oArrFiltroContrato)
		{
			try
			{
				return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaContratosDDL]", oArrFiltroContrato);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}