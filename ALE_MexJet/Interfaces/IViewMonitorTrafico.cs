using ALE_MexJet.Objetos;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE_MexJet.Interfaces
{
    public interface IViewMonitorTrafico : IBaseView  
    {
        event EventHandler eNewTrip;
        event EventHandler eSaveTrip;
        event EventHandler eLoadObjTrips;
        event EventHandler eEliminaTripSolicitud;
        event EventHandler eValidaTrip;
        event EventHandler eSearchDetalle;
        event EventHandler eGuardaMonitorTrafico;
        event EventHandler eBuscaAropuertoBase;
        event EventHandler eBuscaPax;
        event EventHandler eBuscaNotas;

        Enumeraciones.TipoOperacion eCrud { set; get; }
        object oCrud { get; set; }
        int iIdSolicitud { get; set; }

        object[] oArrDetalle { get; }

        object[] oArrMonitorTrafico { get; }
        object[] oArrConsultaMonitorTrafico { get; }
        object[] oArrConsultaPax { get; }
        object[] oArrConsultaNotas { get; }

        void LoadDetalle(DataTable dtDetalle);

        void ObtieneTrips();
        void LoadObjectsTrips(DataTable dtObjCat);
        void MostrarMensaje(string sMensaje, string sCaption);
        void LlenaGrid(DataTable dtObjCat);

        void CargaAeropuertoBase(DataTable dtAeropuertoBase);
        void CargaPax(DataTable dtPax);
        void CargaNotas(DataSet dsNotas);

        void Validacion(int i);
    }
}