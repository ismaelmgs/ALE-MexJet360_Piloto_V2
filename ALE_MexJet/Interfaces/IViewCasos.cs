using ALE_MexJet.Objetos;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE_MexJet.Interfaces
{
    public interface IViewCasos : IBaseView
    {
        object oCrud { get; set; }
        Enumeraciones.TipoOperacion eCrud { set; get; }
        bool bDuplicado { get; set; }
        object[] oArrFiltros { get; }
        void ObtieneValores();
        void LoadObjects(DataTable dtObjCat);
        void MostrarMensaje(string sMensaje, string sCaption);

        void LoadAreas(DataTable dtObjCat);

    }
}