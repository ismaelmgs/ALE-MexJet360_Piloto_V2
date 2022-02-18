using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using ALE_MexJet.Objetos;


namespace ALE_MexJet.Interfaces
{
    public interface IViewTripGuideFPK : IBaseView
    {
        int iTrip { set; get; }

        int iPierna { set; get; }

        string sICAOOrigen { set; get; }

        string sICAODestino { set; get; }

        void ObtieneDatosTrip(DataTable dtTrip);

        void CargaPiernas(DataTable dtPiernas);
        void CargaDetallePierna(DataTable dtDetallePierna);
        void ObtienePasajeros(DataTable dtPasajero);

        void ObtieneCoordenadaOrigen(DataTable dtCoordenada);

        void ObtieneCoordenadaDestino(DataTable dtCoordenada);

        event EventHandler eBuscaDatosTrip;
        event EventHandler eSearchLegsByTrip;
        event EventHandler eBuscaDetallePierna;
        event EventHandler eBuscaPasajero;
        event EventHandler eBuscaCoordenadaOrigen;
        event EventHandler eBuscaCoordenadaDestino;
    }
}