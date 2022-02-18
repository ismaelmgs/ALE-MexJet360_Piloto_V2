using ALE_MexJet.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE_MexJet.Interfaces
{
    public interface IViewIncrementoTarifas : IBaseView
    {
        object[] oArrFiltros { get; }

        void LoadIncrementoTarifas(DataTable dtCal);

        List<IncrementoTarifas> oTarifa { get; }

        event EventHandler eGetIncrementos;
        event EventHandler eGetAplicaIncrementos;
    }
}
