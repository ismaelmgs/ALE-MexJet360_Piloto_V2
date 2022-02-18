using ALE_MexJet.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE_MexJet.Interfaces
{
    public interface IViewRolAccion : IBaseView
    {
   

        object oCrud { get; set; }
        object oSelec { get; set; }

        Enumeraciones.TipoOperacion eCrud { set; get; }

        object[] oArrFiltros { get; }

        object[] oArrFiltrosdll { get; }

        DataTable CrearDataTable { get; }


        void ObtieneValores();
        void LoadCatalogoRol(DataTable dtTipoRolAccion);

        void LoadObjects(DataTable dtObjCat);
        void MostrarMensaje(string sMensaje, string sCaption);

        event EventHandler eSearchObjdll;
        event EventHandler eUpdateObj;

    }
}
