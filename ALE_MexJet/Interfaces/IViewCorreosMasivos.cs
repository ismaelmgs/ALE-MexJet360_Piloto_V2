using ALE_MexJet.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE_MexJet.Interfaces
{
	public interface IViewCorreosMasivos : IBaseView
	{
		object oCrud { get; set; }

		Enumeraciones.TipoOperacion eCrud { get; set; }

		object[] oArrFiltros { get; }

		void ObtieneValores();

		void LoadObjects(DataTable dtObjCat);

		void MuestraMensg(string sMensaje, string sCaption);

		void MostrarMensaje(string sMensaje, string sCaption);

		event EventHandler eCancelObj;
	}
}
