using ALE_MexJet.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE_MexJet.Interfaces
{
	public interface IViewConsultaPresupuestos : IBaseView
	{
		object oCrud { get; set; }
		Enumeraciones.TipoOperacion eCrud { get; set; }
		void CargaGrid(DataTable DT);
		void ObtieneValores();
		void LoadClientes(DataTable dtObjCat);
		void LoadContratos(DataTable dtObjCat);
		void CargaDSPresupuesto(DataSet dsPresupuesto);
		void MostrarMensaje(string sMensaje, string sCaption);
		object[] oArrFiltros { get; }
		object[] oArrFiltroContratos { get; }
		int iIdPresupuesto { get; }

		event EventHandler eObjCliente;
		event EventHandler eObjContrato;
		event EventHandler eObjPresupuesto;
	}
}
