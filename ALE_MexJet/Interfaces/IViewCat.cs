using ALE_MexJet.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE_MexJet.Interfaces
{
    public interface IViewCat : IBaseView
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
        void LoadObjects(DataTable dtObjCat);
        void MostrarMensaje(string sMensaje, string sCaption);


    }
}
