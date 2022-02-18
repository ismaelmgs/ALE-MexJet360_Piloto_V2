using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using ALE_MexJet.Objetos;
using ALE_MexJet.Clases;
using NucleoBase.Core;

namespace ALE_MexJet.DomainModel
{
	public class DBConsultaPresupuestos : DBBase
	{
		public DataTable DBSearchObj(params object[] oArrFiltros)
		{
			try
			{
				return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaPresupuestos]", oArrFiltros);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public int DBDelete(Presupuesto oPresupuesto)
		{
			try
			{
				object oResult;
				oResult = oDB_SP.EjecutarValor("[Principales].[spD_MXJ_EliminaPresupuesto]",  "@id", oPresupuesto.iIdPresupuesto
																							, "@Usuario", Utils.GetUser
																							, "@IP", oPresupuesto.sIP);
				return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
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
				return oDB_SP.EjecutarDT("Catalogos.spS_MXJ_ConsultaClienteDDL", "@status", 1);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public DataTable dtObjContrato(params object[] oArrFiltros)
		{
			try
			{
				return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaContratosDDL]", oArrFiltros);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		internal DataSet dsObjPresupuesto(int iIdPresupuesto)
		{
			try
			{
				return oDB_SP.EjecutarDS("[Principales].[spS_MXJ_ReportePresupuesto]", "@id", iIdPresupuesto);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}