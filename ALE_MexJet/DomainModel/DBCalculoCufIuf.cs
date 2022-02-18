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
	public class DBCalculoCufIuf : DBBase
	{
		public DataTable dtObjCalculoCufIuf(params object[] oArrFiltros)
		{
			try
			{
				return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ObtieneCufIuf]", oArrFiltros);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public DataSet dtObjTotalesCufIuf(params object[] oArrFiltros)
		{
			try
			{
				return oDB_SP.EjecutarDS("[Principales].[spS_MXJ_ObtieneTotalesCufIuf]", oArrFiltros);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}