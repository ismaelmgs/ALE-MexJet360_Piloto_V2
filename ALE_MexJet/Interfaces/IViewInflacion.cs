using ALE_MexJet.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE_MexJet.Interfaces
{
    public interface IViewInflacion : IBaseView
    {
        object oCrud { get; set; }

        bool bDuplicado { get; set; }
        Enumeraciones.TipoOperacion eCrud { set; get; }

        int iIdInflacion { get; }
        object[] oArrFiltros { get; }
        object[] oArrFiltrosDetalle { get; }  

        void ObtieneValores();
        void ObtieneValoresDetalles();

        void LoadObjects(DataTable dtObjCat);
        void LoadObjectsDetalle(DataTable dtObject);

        void MostrarMensaje(string sMensaje, string sCaption);
        void MostrarMensajeDet(string sMensaje, string sCaption);

        event EventHandler eGetSearchDetalle;
        event EventHandler eNewObjDet;
        event EventHandler eSaveObjDet;
        event EventHandler eObjSelectedDet;
        event EventHandler eDeleteObjDet;
    }
}
