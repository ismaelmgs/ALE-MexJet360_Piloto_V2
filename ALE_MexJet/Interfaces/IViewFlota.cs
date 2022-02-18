using ALE_MexJet.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE_MexJet.Interfaces
{
    public interface IViewFlota:IBaseView
    {
        object oCrud { get; set; }
        bool bDuplicado { get; set; }
        Enumeraciones.TipoOperacion eCrud { set; get; }
        object[] oArrFiltros { get; }
        void ObtieneValores();
        void LoadObjects(DataTable dtObjCat);
        void MostrarMensaje(string sMensaje, string sCaption);

        void LoadCodigoUnidadDos_Union(DataTable dtCodigoUnitDosUnion);

        event EventHandler eGetCodigoUnidadDosUnion;
    }
}
