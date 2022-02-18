using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE_MexJet.Interfaces
{
    public interface IViewDashboard : IBaseView
    {
        
        void ObtieneDashboardAvisos();
        void ObtieneDashboardClientes();
        void ObtieneDashboardVuelos();
        void LoadDashboardAvisos(DataTable dtAvisos);
        void LoadDashboardClientes(DataTable dtClientes);
        void LoadDashboardVuelos(DataTable dtVuelos);

        event EventHandler eGetDashboardAvisos;                
        event EventHandler eGetDashboardClientes;
        event EventHandler eGetDashboardVuelos;
    }
}
