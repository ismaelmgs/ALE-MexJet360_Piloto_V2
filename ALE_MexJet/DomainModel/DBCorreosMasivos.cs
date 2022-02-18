using ALE_MexJet.Clases;
using ALE_MexJet.Objetos;
using NucleoBase.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ALE_MexJet.DomainModel
{
	public class DBCorreosMasivos : DBBase
	{
		public int DBSave(CorreoMasivo oCorreoMasivo)
		{
			try
			{
				object oResult = new object();
				oResult = oDB_SP.EjecutarValor("[Principales].[spI_MXJ_InsertaCorreoMasivo]", "@motivo", oCorreoMasivo.sMotivo
																					, "@asunto", oCorreoMasivo.sAsunto
																					, "@destinatarios", oCorreoMasivo.sDestinatarios
																					, "@copiados", oCorreoMasivo.sCopiados
																					, "@contenido", oCorreoMasivo.sContenido
																					, "@status", oCorreoMasivo.iStatus
																					, "@usuario", Utils.GetUser
																					, "@ip", oCorreoMasivo.sIP);
				return String.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public int DBUpdate(CorreoMasivo oCorreoMasivo)
		{
			try
			{
				object oResult = new object();
				oResult = oDB_SP.EjecutarValor("[Principales].[spU_MXJ_ActualizaCorreoMasivo]", "@idCorreoMasivo", oCorreoMasivo.iIdCorreoMasivo
																					, "@motivo", oCorreoMasivo.sMotivo
																					, "@asunto", oCorreoMasivo.sAsunto
																					, "@destinatarios", oCorreoMasivo.sDestinatarios
																					, "@copiados", oCorreoMasivo.sCopiados
																					, "@contenido", oCorreoMasivo.sContenido
																					, "@status", oCorreoMasivo.iStatus
																					, "@usuarioModificacion", Utils.GetUser
																					, "@ip", oCorreoMasivo.sIP);
				return String.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public int DBDelete(CorreoMasivo oCorreoMasivo)
		{
			try
			{
				object oResult;
				oResult = oDB_SP.EjecutarValor("[Principales].[spD_MXJ_EliminaCorreoMasivo]", "@idCorreoMasivo", oCorreoMasivo.iIdCorreoMasivo
																					 , "@usuarioModificacion", Utils.GetUser
																					 , "@ip", oCorreoMasivo.sIP);
				return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public int DBCancel(CorreoMasivo oCorreoMasivo)
		{
			try
			{
				object oResult;
				oResult = oDB_SP.EjecutarValor("[Principales].[spD_MXJ_EliminaCorreoMasivo]", "@idCorreoMasivo", oCorreoMasivo.iIdCorreoMasivo
																					 , "@usuarioModificacion", Utils.GetUser
																					 , "@ip", oCorreoMasivo.sIP
																					 , "@cancela", true);
				return string.IsNullOrEmpty(oResult.S()) ? 0 : oResult.S().I();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public DataTable DBSearchObj (params object[] oArrFiltros)
		{
			try
			{
				return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaCorreosMasivos]", oArrFiltros);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		} 
	}
}