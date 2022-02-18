using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ALE_MexJet.Objetos;

namespace ALE_MexJet.Interfaces
{
    public interface IViewRemision : IBaseView
    {
        object[] oArrFiltros { get; }
        void LoadObjects(DataTable dtObjCat);       
        void MostrarMensaje(string sMensaje, string sCaption);
        Remision oRemision { get; }
        
    }
}
