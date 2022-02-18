using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using ALE_MexJet.Objetos;
using System.Threading.Tasks;
using System.Text;
using System.Data;

namespace ALE_MexJet.Interfaces
{
    public interface IViewContacto : IBaseView
    {
        object oCrud { get; set; }

        Enumeraciones.TipoOperacion eCrud { get; set; }

        object[] oArrFiltros { get; }
        void ObtieneValores();
        void LoadObjects(DataSet dtObjCat);
        void MostrarMensaje(string sMensaje, string sCaption);

        void MuestraMensg(string sMensaje, string sCaption);
        void LoadCatalogoTipocliente(DataTable dtObjCat);
        event EventHandler eGetTipoTitulo;
        event EventHandler eUpdateObservacion;
        int iIdCliente { get; }

        Cliente oCliente { get; }
    }
}