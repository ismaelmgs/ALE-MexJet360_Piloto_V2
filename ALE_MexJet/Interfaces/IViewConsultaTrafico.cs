using ALE_MexJet.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE_MexJet.Interfaces
{
    public interface IViewConsultaTrafico : IBaseView  
    {
        event EventHandler eNewTrip;
        event EventHandler eSaveTrip;                
        event EventHandler eValidaTrip;
        event EventHandler eLoadObjTrips;
        event EventHandler eSearchDetalle;
        event EventHandler eEliminaTripSolicitud;
        event EventHandler eGetClientes;
        event EventHandler eGetContrato;
        event EventHandler eConsultaSol;
        event EventHandler eBuscaSubGrid;       

        Enumeraciones.TipoOperacion eCrud { set; get; }
        object oCrud { get; set; }
        int iIdSolicitud { get; set; }
        object[] oArrFiltros { get; }
        object[] oArrFiltroContrato { get; }
        object[] oArrFiltroClientes { get; }
        object[] oArrFilSolicitud { get; }
        void ObtieneTrips();
        void LoadObjectsTrips(DataTable dtObjCat);
        void MostrarMensaje(string sMensaje, string sCaption);
        void LlenaGrid(DataTable dtObjCat);
        void ObtieneClientes();
        void ObtieneContrato();
        void LoadClientes(DataTable dtObjCat);
        void LoadContrato(DataTable dtObjCat);
        void CargaConsultaTrafico();
        void LoadSol(DataTable DT);
        void LoadPiernas(DataTable dtObjCat);
    }
}
