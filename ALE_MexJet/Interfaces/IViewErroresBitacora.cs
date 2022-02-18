using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE_MexJet.Interfaces
{
    public interface IViewErroresBitacora: IBaseView
    {
        object[] oArrFiltros { get; }
        void MostrarMensaje(string sMensaje, string sCaption);
        void LoadObjects(DataTable dtObjCat);
        void ObtieneValores();
        void MostrarMensajeError(string sMensaje, string sCaption);
    }
}
