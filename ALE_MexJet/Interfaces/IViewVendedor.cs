using ALE_MexJet.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE_MexJet.Interfaces
{
    public interface IViewVendedor : IBaseView
    {
        // oGetSetObjSelection { get; set; }
        //TObjCat oCatalogo { get; set; }
        object oCrud { get; set; }

        bool bDuplicado { get; set; }
        Enumeraciones.TipoOperacion eCrud { set; get; }

        object[] oArrFiltros { get; }
        //string sCaptionBtnGuardar { set; }
        //string sNumRegistros { get; set; }

        void ObtieneValores();
        void LoadObjectsBase(DataTable dtBase);
        void LoadObjectsUnidad4(DataTable dtBase);
        void LoadObjectsUnidad1V(DataTable dtBase);
        void LoadObjects(DataTable dtObjCat);
        void MostrarMensaje(string sMensaje, string sCaption);

        event EventHandler eSearchObjBase;
        event EventHandler eSearchObjUni4;
        event EventHandler eSearchObjUni1V;
    }
}
