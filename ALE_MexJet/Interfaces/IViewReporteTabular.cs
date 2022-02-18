using ALE_MexJet.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE_MexJet.Interfaces
{
    public interface IViewReporteTabular : IBaseView
    {
        DataTable dtMatricula { get; set; }
        DataTable dtCliente { get; set; }
        DataTable dtContrato { get; set; }
        DataTable dtGrupoModelo { get; set; }
        DataTable dtBase { get; set; }
        object[] oArrFiltros { get; }
        string sTiposFactura { set; get; }


        void LoadGrid(DataTable dt);


        event EventHandler eGeneraRep;
    }
}
