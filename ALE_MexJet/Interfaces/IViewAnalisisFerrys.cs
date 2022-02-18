using ALE_MexJet.Objetos;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE_MexJet.Interfaces
{
    public interface IViewAnalisisFerrys : IBaseView
    {
        object[] oArrFiltros { get; }
        object[] oArrFiltrosPierna { get; }
        object[] oArrFiltrosPiernaDetalle { get; }
        object[] oArrParametrosUpdate { get; }
        void CargaDDl(DataTable DT);
        void CargaGrid(DataTable DT);
        void CargaGridDetalle(DataTable DT);
        void MostrarMensaje(string sMensaje, string sCaption);

        event EventHandler eSerachPiernas;
        event EventHandler eSerachPiernaDetalle;
    }
}