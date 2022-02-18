using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE_MexJet.Interfaces
{
    public interface IViewResumenContrato: IBaseView
    {

        void LoadClientes(DataTable dtClientes);
        void LoadContracts(DataTable dtContratos);
        void LoadReport(DataSet ds);
        void LoadTransferenciasPorPeriodo(DataTable dtTras);
        DataTable ObtieneResumenContrato();


        int iIdCliente { get; }
        int iIdContrato { get; }
        int iIdResumen { get; }
        DataSet dsResumenPres { set; get; }



        event EventHandler eSearchContracts;
        event EventHandler eSearchTraspasos;
        event EventHandler eSearchReport;
    }
}
