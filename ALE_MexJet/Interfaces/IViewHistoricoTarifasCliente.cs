using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE_MexJet.Interfaces
{
    public interface IViewHistoricoTarifasCliente : IBaseView
    {
        void ObtieneDatosCliente();
        void ObtieneDatosContrato();
        void ObtieneDatosHistoricoTarifas();

        void LoadClientes(DataTable dtObjCat);
        void LoadContrato(DataTable dtObjCat);
        void LoadHistoricoTarifas(DataTable dtObjCat);

        object[] oArrFiltroClientes { get; }
        object[] oArrFiltroContrato { get; }
        object[] oArrFiltroHistoricoTarifas { get; }

        event EventHandler eGetClientes;
        event EventHandler eGetContrato;
        event EventHandler eGetHistoricoTarifas;        
    }
}
