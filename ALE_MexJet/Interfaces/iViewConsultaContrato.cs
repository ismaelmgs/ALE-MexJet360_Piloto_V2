using ALE_MexJet.Objetos;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE_MexJet.Interfaces
{
    public interface iViewConsultaContrato : IBaseView
    {
        object[] oArrFiltros { get; }
        int iIdCliente { get; }
        int iIdContrato { get; }

        DataTable dtClientes { get; set; }
        DataTable dtContratosCliente { get; set; }
        DataTable dtContratos { get; set; }

        void LoadKardex(DataSet ds);

        event EventHandler eSearchContratosCliente;
        event EventHandler eSearchKardex;


    }
}
