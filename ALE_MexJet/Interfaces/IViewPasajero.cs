using ALE_MexJet.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE_MexJet.Interfaces
{
    public interface IViewPasajero : IBaseView
    {
        Pasajero objPasajero { get; set; }
        int iIdPasajero { get; set; }
        void ConsultaPasajeros(DataTable dtResultado);
        void ConsultaPasajeroId(DataTable dtResultado);
        void ConsultaCliente(DataTable dtResultado);
        object[] oArrFiltros { get; }

        object[] oArrFiltrosCliente { get; }

        void MostrarMensaje(string sMensaje, string sCaption);

        event EventHandler eConsultaCliente;
        event EventHandler eConsultaPasajeroId;
        event EventHandler eGuardaPasajeroPerfil;
        event EventHandler eEliminaPasajeroPorId;
        event EventHandler eActualizaPasajeroPerfil;
        

    }
}
