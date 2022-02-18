using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE_MexJet.Interfaces
{
	public interface IViewHorasVoladasCliente : IBaseView
	{
		void CargaGrid(DataSet DS);
		void LoadCliente(DataTable dtObjCat);
		void LoadContrato(DataTable dtObjCat);

		void MostrarMensaje(string sMensaje, string sCaption);

		object[] oArrFiltros { get; }
		object[] oArrFiltroClientes { get; }
		object[] oArrFiltroContrato { get; }

		event EventHandler eObjCliente;
		event EventHandler eObjContrato;
		event EventHandler eSearchObj2;
	}
}
