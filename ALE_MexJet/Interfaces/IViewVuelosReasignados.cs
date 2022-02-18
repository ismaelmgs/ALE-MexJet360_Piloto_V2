using ALE_MexJet.Objetos;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE_MexJet.Interfaces
{
    public interface IViewVuelosReasignados : IBaseView  
    {
        
        object[] oArrFiltros { get; }

        void MostrarMensaje(string sMensaje, string sCaption);
        void LlenaGrid(DataTable dtObjCat);
    }
}