using ALE_MexJet.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE_MexJet.Interfaces
{
    public interface IViewTableroFerry : IBaseView
    {
        object[] oArrFiltros { get; }
        TableroFerry oTFerry { get; }

        void LoadFerrys(DataTable dt);
        void MostrarMensaje(string sMensaje, string sCaption);
        
    }
}
