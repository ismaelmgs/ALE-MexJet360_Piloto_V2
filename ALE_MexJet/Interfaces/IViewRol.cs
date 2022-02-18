using ALE_MexJet.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE_MexJet.Interfaces
{
    public interface IViewRol : IBaseView
    {
        // oGetSetObjSelection { get; set; }
        //TObjCat oCatalogo { get; set; }
        object oCrud { get; set; }

        bool bDuplicado { get; set; }
        Enumeraciones.TipoOperacion eCrud { set; get; }

        object[] oArrFiltros { get; }
        object[] oArrFiltrosDll { get; }
        //object[] oArrFiltrosRol { get; }
        //string sCaptionBtnGuardar { set; }
        //string sNumRegistros { get; set; }

        void ObtieneValores();
        void ObtieneValoresOrigen();
        void ObtieneValoresDestino();

        void LoadObjects(DataTable dtObjCat);
        void LoadObjectsO(DataTable dtObjCatO);
        void LoadObjectsD(DataTable dtObjCatD);
        void LoadModulo(DataTable dtModuloNoHeader);
        void MostrarMensaje(string sMensaje, string sCaption);

        event EventHandler eSearchCloneO;
        event EventHandler eSearchCloneD;
        event EventHandler eSearchModDef;
        event EventHandler eCloneObj;


    }
}
