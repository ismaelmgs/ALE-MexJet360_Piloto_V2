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
    public interface IViewModelo : IBaseView
    {
        object oCrud { get; set; }
        Enumeraciones.TipoOperacion eCrud { get; set; }
        bool bDuplicado { get; set; }
        object[] oArrFiltros { get; }
        void ObtieneValores();
        void LoadObjects(DataTable dtObjCat);
        void MostrarMensaje(string sMensaje, string sCaption);
        void LoadCatalogoMarca(DataTable dtObjCat);
        void LoadCatalogoGrupoModelo(DataTable dtObjCat);
        void LoadCatalogoGrupoEspacio(DataTable dtObjCat);
        void LoadCatalogoDesignador(DataTable dtObjCat);
        void LoadCatalogoTipo(DataTable dtObjCat);



        event EventHandler eGetMarca;
        event EventHandler eGetGrupoModelo;
        event EventHandler eGetGrupoEspacio;
        event EventHandler eGetDesignador;
        event EventHandler eGetTipo;


    }
}