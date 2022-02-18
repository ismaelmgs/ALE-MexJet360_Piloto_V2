using ALE_MexJet.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE_MexJet.Interfaces
{
    public interface IViewNotaCredito : IBaseView
    {
        object[] oArrFiltros { get; }
        void LoadObjects(DataTable dtObjCat);

        Enumeraciones.TipoOperacion eCrud { set; get; }

        object oCrud { get; set; }

        void MostrarMensaje(string sMensaje, string sCaption);

        NotaCredito oCatalogo { get; }

        NotaCredito oBusqueda { get; }

        event EventHandler eSearchConsulta;
    }
}
