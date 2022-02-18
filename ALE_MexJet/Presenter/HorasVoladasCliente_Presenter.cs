using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ALE_MexJet.Clases;
using ALE_MexJet.ControlesUsuario;
using ALE_MexJet.DomainModel;
using ALE_MexJet.Interfaces;
using ALE_MexJet.Presenter;
using ALE_MexJet.Objetos;
using NucleoBase.Core;
using NucleoBase.Seguridad;
using DevExpress.XtraPrinting;
using DevExpress.Export;
using DevExpress.Web;
using DevExpress.Web.Export;

namespace ALE_MexJet.Presenter
{
	public class HorasVoladasCliente_Presenter : BasePresenter<IViewHorasVoladasCliente>
	{
		private readonly DBHorasVoladasCliente oIGestCat;

		public HorasVoladasCliente_Presenter(IViewHorasVoladasCliente oView, DBHorasVoladasCliente oGC) : base(oView)
		{
			oIGestCat = oGC;
			oIView.eObjCliente += SearchObjCiente_Presenter;
			oIView.eObjContrato += SearchObjContrato_Presenter;
			oIView.eSearchObj2 += SearchObj2_Presenter;
		}

		protected override void SearchObj_Presenter(object sender, EventArgs e)
		{
			oIView.CargaGrid(oIGestCat.dsObjHorasVoladas(oIView.oArrFiltros));
		}

		protected void SearchObj2_Presenter(object sender, EventArgs e)
		{
			oIView.CargaGrid(oIGestCat.dsObjHorasVoladasCosto(oIView.oArrFiltros));
		}

		protected void SearchObjCiente_Presenter(object sender, EventArgs e)
		{
			oIView.LoadCliente(oIGestCat.dtObjCliente());
		}

		protected void SearchObjContrato_Presenter(object sender, EventArgs e)
		{
			oIView.LoadContrato(oIGestCat.dtObjContrato(oIView.oArrFiltroContrato));
		}
	}
}