using ALE_MexJet.Objetos;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE_MexJet.Interfaces
{
    public interface IViewDashboardAtnCliente : IBaseView
    {
        object[] oArrFiltros { get; }
        object[] oArrFilSol { get; }
        object[] oArrCasos { get; }

        void LoadObjects(DataSet dtObjCat);
        void LoadSol(DataTable dtSol);
        void LoadCaso(DataTable dtCaso);

        event EventHandler eSolicitud;
        event EventHandler eCasos;
    }
}