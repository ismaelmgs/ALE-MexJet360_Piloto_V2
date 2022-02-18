using ALE_MexJet.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE_MexJet.Interfaces
{
    public interface IViewUsuario : IBaseView
    {

        // oGetSetObjSelection { get; set; }
        //TObjCat oCatalogo { get; set; }
        object oCrud { get; set; }

        Enumeraciones.TipoOperacion eCrud { set; get; }

        object[] oArrFiltros { get; }
        //string sCaptionBtnGuardar { set; }
        //string sNumRegistros { get; set; }
        bool bDuplicado { get; set; }
        void ObtieneValores();
        void LoadObjects(DataTable dtObjCat);
        void LoadCatalogoBase(DataTable dtObjCat);

        void LoadCatalogoRol(DataTable dtObjCat);

        void MostrarMensaje(string sMensaje, string sCaption);

        event EventHandler eGetAero;
        event EventHandler eGetRol;
    }
}