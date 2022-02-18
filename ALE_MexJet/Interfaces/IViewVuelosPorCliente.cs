using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE_MexJet.Interfaces
{
    public interface IViewVuelosPorCliente:IBaseView
    {
        void ObtieneDatosCliente();
        void ObtieneDatosContrato();
        void OtieneReporteVuelosPorCliente();

        void LoadClientes(DataTable dtObjCat);
        void LoadContrato(DataTable dtObjCat);
        void LoadReporteVuelosPorCliente(DataTable dtObjCat);

        object[] oArrFiltroClientes { get; }
        object[] oArrFiltroContrato { get; }
        object[] oArrFiltroReporteVuelosPorCliente { get; }

        event EventHandler eGetClientes;
        event EventHandler eGetContrato;
        event EventHandler eGetReporteVuelosPorCliente;
    }
}
