using ALE_MexJet.Clases;
using ALE_MexJet.DomainModel;
using ALE_MexJet.Interfaces;
using ALE_MexJet.Objetos;
using DevExpress.Web.Data;
using NucleoBase.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ALE_MexJet.Presenter
{
	public class ConsultaPresupuestos_Presenter : BasePresenter<IViewConsultaPresupuestos>
	{
		private readonly DBConsultaPresupuestos oIGestCat;

		public ConsultaPresupuestos_Presenter(IViewConsultaPresupuestos oView, DBConsultaPresupuestos oGC) : base(oView)
		{
			oIGestCat = oGC;
			oIView.eObjCliente += SearchObjCiente_Presenter;
			oIView.eObjContrato += SearchObjContrato_Presenter;
			oIView.eObjPresupuesto += SearchObjPresupuesto_Presenter;
		}

		protected override void SearchObj_Presenter(object sender, EventArgs e)
		{
			oIView.CargaGrid(oIGestCat.DBSearchObj(oIView.oArrFiltros));
		}

		protected override void DeleteObj_Presenter(object sender, EventArgs e)
		{
			try
			{
				int id = oIGestCat.DBDelete(oCatalogo);
				if (id > 0)
				{
					oIView.MostrarMensaje(Mensajes.LMensajes[Enumeraciones.Mensajes.Eliminacion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
					oIView.ObtieneValores();
				}
			}
			catch (Exception ex)
			{
				oIView.MostrarMensaje("Se detectó el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
			}
		}

		protected void SearchObjCiente_Presenter(object sender, EventArgs e)
		{
			oIView.LoadClientes(oIGestCat.dtObjCliente());
		}

		protected void SearchObjContrato_Presenter(object sender, EventArgs e)
		{
			oIView.LoadContratos(oIGestCat.dtObjContrato(oIView.oArrFiltroContratos));
		}

		protected void SearchObjPresupuesto_Presenter(object sender, EventArgs e)
		{
			oIView.CargaDSPresupuesto(oIGestCat.dsObjPresupuesto(oIView.iIdPresupuesto));
		}

		private Presupuesto oCatalogo
		{
			get
			{
				Presupuesto oPresupuesto = new Presupuesto();
				switch (oIView.eCrud)
				{
					case Enumeraciones.TipoOperacion.Insertar:
						ASPxDataInsertingEventArgs eI = (ASPxDataInsertingEventArgs)oIView.oCrud;
						break;
					case Enumeraciones.TipoOperacion.Actualizar:
						ASPxDataUpdatingEventArgs eU = (ASPxDataUpdatingEventArgs)oIView.oCrud;
						oPresupuesto.iIdPresupuesto = eU.Keys[0].S().I();
						break;
					case Enumeraciones.TipoOperacion.Eliminar:
						ASPxDataDeletingEventArgs eD = (ASPxDataDeletingEventArgs)oIView.oCrud;
						oPresupuesto.iIdPresupuesto = eD.Keys[0].S().I();
						break;
				}

				return oPresupuesto;
			}
		}
	}
}