using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ALE_MexJet.Objetos;


namespace ALE_MexJet.Interfaces
{
    public interface IViewConsultaTarifa : IBaseView
    {

        void ObtieneValores();

        void LoadTarifas(DataTable dtObjCat);
        void LoadClientes(DataTable dtObjCat);
        object[] oArrFiltroCliente { get;  }        
        event EventHandler eGetCliente;
        event EventHandler eGetDetalleGastoInterno;
        void LoadContrato(DataTable dtObjCat);
        object[] oArrFiltroContrato { get; }
        Object[] oArrFiltroTarifas { get; }
        event EventHandler eGetContrato;
    
     
             
    }
    
}