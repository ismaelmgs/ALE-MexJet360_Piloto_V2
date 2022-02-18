using ALE_MexJet.Interfaces;
using ALE_MexJet.DomainModel;
using ALE_MexJet.Objetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevExpress.Web.Data;
using NucleoBase.Core;
using ALE_MexJet.Clases;

namespace ALE_MexJet.Presenter
{
	public class CorreosMasivos_Presenter : BasePresenter<IViewCorreosMasivos>
	{
		private readonly DBCorreosMasivos oICorreosMasivos;

		public CorreosMasivos_Presenter(IViewCorreosMasivos oView, DBCorreosMasivos oCM) 
			: base(oView)
		{
			oICorreosMasivos = oCM;
			oIView.eCancelObj += CancelObj_Presenter;
		}

		public void LoadObjects_Presenter()
		{
			oIView.LoadObjects(oICorreosMasivos.DBSearchObj(oIView.oArrFiltros));
		}

		protected override void SearchObj_Presenter(object sender, EventArgs e)
		{
			oIView.LoadObjects(oICorreosMasivos.DBSearchObj(oIView.oArrFiltros));
		}

		protected override void NewObj_Presenter(object sender, EventArgs e)
		{
			try
			{
				int id = oICorreosMasivos.DBSave(oCatalogo);
				if (id > 0)
				{
					oIView.ObtieneValores();
					oIView.MuestraMensg(Mensajes.LMensajes[Enumeraciones.Mensajes.Insercion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
				}
			}
			catch (Exception ex)
			{
				oIView.MostrarMensaje("Se detectó el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
			}
		}

		protected override void SaveObj_Presenter(object sender, EventArgs e)
		{
			try
			{
				int rowCount = oICorreosMasivos.DBUpdate(oCatalogo);
				if (rowCount > 0)
				{
					oIView.ObtieneValores();
					oIView.MuestraMensg(Mensajes.LMensajes[Enumeraciones.Mensajes.Modificacion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
				}
			}
			catch (Exception ex)
			{
				oIView.MostrarMensaje("Se detectó el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
			}
		}

		protected override void DeleteObj_Presenter(object sender, EventArgs e)
		{
			try
			{
				int id = oICorreosMasivos.DBDelete(oCatalogo);
				if (id > 0)
				{
					oIView.MuestraMensg(Mensajes.LMensajes[Enumeraciones.Mensajes.Eliminacion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
					oIView.ObtieneValores();
				}
			}
			catch (Exception ex)
			{
				oIView.MostrarMensaje("Se detectó el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
			}
		}

		public void CancelObj_Presenter(object sender, EventArgs e)
		{
			try
			{
				int id = oICorreosMasivos.DBCancel(oCatalogo);
				if (id > 0)
				{
					oIView.MuestraMensg(Mensajes.LMensajes[Enumeraciones.Mensajes.Eliminacion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
					oIView.ObtieneValores();
				}
			}
			catch (Exception ex)
			{
				//oIView.MostrarMensaje("Se detectó el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
				throw ex;
			}
		}

		private CorreoMasivo oCatalogo
		{
			get
			{
				CorreoMasivo oCorreoMasivo = new CorreoMasivo();
				switch (oIView.eCrud)
				{
					case Enumeraciones.TipoOperacion.Insertar:
						ASPxDataInsertingEventArgs eI = (ASPxDataInsertingEventArgs)oIView.oCrud;
						oCorreoMasivo.sMotivo = eI.NewValues["Motivo"].S();
						oCorreoMasivo.sAsunto = eI.NewValues["Asunto"].S();
						oCorreoMasivo.sDestinatarios = eI.NewValues["Destinatarios"].S();
						oCorreoMasivo.sCopiados = eI.NewValues["Copiados"].S();
						oCorreoMasivo.sContenido = eI.NewValues["Contenido"].S();
						oCorreoMasivo.iStatus = eI.NewValues["Status"].S().I();
						break;
					case Enumeraciones.TipoOperacion.Actualizar:
						ASPxDataUpdatingEventArgs eU = (ASPxDataUpdatingEventArgs)oIView.oCrud;
						oCorreoMasivo.iIdCorreoMasivo = eU.Keys[0].S().I();
						oCorreoMasivo.sMotivo = eU.NewValues["Motivo"].S();
						oCorreoMasivo.sAsunto = eU.NewValues["Asunto"].S();
						oCorreoMasivo.sDestinatarios = eU.NewValues["Destinatarios"].S();
						oCorreoMasivo.sCopiados = eU.NewValues["Copiados"].S();
						oCorreoMasivo.sContenido = eU.NewValues["Contenido"].S();
						oCorreoMasivo.iStatus = eU.NewValues["Status"].S().I();
						break;
					case Enumeraciones.TipoOperacion.Eliminar:
						int eD = (int)oIView.oCrud;
						oCorreoMasivo.iIdCorreoMasivo = eD;
						break;
				}

				return oCorreoMasivo;
			}
		}
	}
}