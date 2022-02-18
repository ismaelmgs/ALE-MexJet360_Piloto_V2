using ALE_MexJet.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE_MexJet.Interfaces
{
    public interface IViewComisariatoProducto : IBaseView
    {

        // oGetSetObjSelection { get; set; }
        //TObjCat oCatalogo { get; set; }
        object oCrud { get; set; }

        Enumeraciones.TipoOperacion eCrud { set; get; }
        bool bDuplicado { get; set; }
        object[] oArrFiltros { get; }
        //string sCaptionBtnGuardar { set; }
        //string sNumRegistros { get; set; }

        void ObtieneValores();
        void LoadObjects(DataTable dtObjCat);
        void LoadCatalogoComisariato(DataTable dtObjCat);
        void LoadCatalogoProducto(DataTable dtObjCat);

        void MostrarMensaje(string sMensaje, string sCaption);

        event EventHandler eGetComisariato;
        event EventHandler eGetProducto;

    }
}