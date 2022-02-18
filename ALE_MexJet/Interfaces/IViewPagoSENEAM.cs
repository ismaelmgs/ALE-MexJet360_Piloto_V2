using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE_MexJet.Interfaces
{
    public interface IViewPagoSENEAM:IBaseView
    {
        void ObtieneValores();
        object[] oArrFiltros { get; }        
        void LoadObjects(DataTable dtObjPagoSENEAM);        
        void MostrarMensaje(string sMensaje, string sCaption);
    }
}
