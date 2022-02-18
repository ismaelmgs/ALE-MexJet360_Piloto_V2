using ALE_MexJet.Objetos;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE_MexJet.Interfaces
{
    public interface IViewSolicitudVuelo : IBaseView
    {   
        object oCrud { get; set; }
        object oCrudTrip { get; set; }
        object[] oArrFiltros { get; }
        object[] oFiltroContrato { get; }
        object[] oItinerario { get; }
        object[] oEliminaItinerario { get; }
        object[] oGuardaSeguimiento { get; }
        object[] oConsultaDetalleItinerario{ get; }
        object[] oInsertaPasajero { get; }
        

        bool bViabilidad { get; set; }
        int iIdCliente { get; set; }
        int iIdContrato { get; set; }
        int iIdSolicitud{ get; set; }
        int iIdSeguimiento { get; set; }
        string sOrigen { get; set; }
        SolicitudVuelo oCatalogo { get; set; }
        SolicitudVuelo oCatalogoSeguimiento { get; }
        SolicitudVuelo oCatalogoItinerario { get; }
        SolicitudVuelo oCat { get; }
        SolicitudVuelo oInsertaPDFSeguimiento { get; }

        
        void ObtieneDetalle(DataTable Detalle);
        void Modelo(DataTable DT);
        void ObtieneValores();
        void ObtieneTrips();
        void ObtieneHistorico();
        void LoadObjects(DataTable dtObjCat);
        void LoadObjectsTrips(DataTable dtObjCat);
        void MostrarMensaje(string sMensaje, string sCaption);
        void MostrarMensajeSolicitud(string sMensaje, string sCaption);
        void LoadContactosCliente(DataTable dtObjCat);
        void LoadGrupoModelo(DataTable dtObjCat);
        void LoadOrigen(DataTable dtObjCat);
        void LoadEstatus(DataTable dtObjCat);
        void LoadMotivos(DataTable dtObjCat);
        void LoadOrigenVuelo(DataTable dtObjCat);
        void LoadOrigenVueloTodos(DataTable dtObjCat);
        void LoadDestinoVuelo(DataTable dtObjCat);
        void GetIdSolicitud(int iobjIdSolicitud);
        void LoadHistorico(DataTable dtObjCat);
        void LoadOrigenDestino(DataTable dtObjCat);
        void LoadOrigDestFiltro(DataTable dtObjCat);
        void LoadTramoSol(DataTable dtObjCat);
        void LoadPaxTramo(DataTable dtObjCat);
        void LoadComisariatoTramo(DataTable dtObjCat);
        void LoadProveedor(DataTable dtObjCat);
        void LoadSolicitud(DataSet ds);
        void LoadSolVueloByID(DataTable dtObjCat);
        void LoadCorreoAlta(DataSet dtObjCat);
        void Validacion(int i);
        void LoadPaxTramo2(DataTable dtObjCat);
        void ValidaFechaHora(int i);
        void LoadAerTramo(DataSet dtObjCat);
        void LoadConsultaDetalle(DataTable dtObjCat);
        void LoadSolPDF(DataTable dtObjCat);
        void LoadItinerario(DataTable dtObjCat);
        void ConsultaModContrato(DataTable dtObjCat);
        void ValidaVueloSimultaneo(int iResultado);
        void CargaPasajeros(DataTable DT);
        void ConsultaPDFSeguimiento(DataTable DT);
        void CargaTripGuides(DataTable dt);
        void ConsultaContactoSolicitud(DataTable dt);

        void ConsultaVendedorSolicitud(DataTable dtResultado);

        Enumeraciones.TipoOperacion eCrud { set; get; }
        event EventHandler eLoadContactosCliente;
        event EventHandler eLoadGrupoModelo;
        event EventHandler eLoadOrigen;
        event EventHandler eLoadEstatus;
        event EventHandler eLoadMotivos;
        event EventHandler eNewTrip;
        event EventHandler eSaveTrip;
        event EventHandler eLoadObjTrips;
        event EventHandler eLoadOrigenVuelo;
        event EventHandler eLoadOrigenVueloTodos;
        event EventHandler eLoadHistorico;
        event EventHandler eLoadDestinoVuelo;
        event EventHandler eNewTramo;
        event EventHandler eNewSeguimiento;
        event EventHandler eNewItinerario;
        event EventHandler eLoadOrigenDestino;
        event EventHandler eLoadOrigDestFiltro;
        event EventHandler eLoadTramoSol;
        event EventHandler eEditaTramoSol;
        event EventHandler eEliminaTramoSol;
        event EventHandler eInsertaPaxTramo;
        event EventHandler eObtienePaxTramo;
        event EventHandler eEditaPaxTramo;
        event EventHandler eEliminaPaxTramo;
        event EventHandler eConsultaComisariatoTramo;
        event EventHandler eInsertaComisariatoTramo;
        event EventHandler eEditaComisariatoTramo;
        event EventHandler eEliminaComisariatoTramo;
        event EventHandler eEliminaTripSolicitud;
        event EventHandler eConsultaProveedor;
        event EventHandler eEliminaHistorico;
        event EventHandler eEditarSolVuelo;
        event EventHandler eConsultasolVueloByID;
        event EventHandler eLoadCorreoAlta;
        event EventHandler eValidaTrip;
        event EventHandler eLoadPaxTramo2;
        event EventHandler eValidaFechaHora;
        event EventHandler eAerTramo;
        event EventHandler eConsultaDetalle;
        event EventHandler eConsultaSolPDF;
        event EventHandler eConsultaModCon;
        event EventHandler eConsultaItinerario;
        event EventHandler eEliminaItinerario;
        event EventHandler eGuardaSeguimientoHistorico;
        event EventHandler eConsultaDetalleItinerario;
        event EventHandler eValidaVuelosimultaneo;
        event EventHandler eInsertaMonitorDespacho;
        event EventHandler eBuscaPasajero;
        event EventHandler eInsertaPasajero;
        event EventHandler eViabilidad;
        event EventHandler ePDFSeguimiento;
        event EventHandler eConsultaPDFSeguimiento;
        event EventHandler eConsultaTripGuides;
        event EventHandler eDeleteTripGuide;
        event EventHandler eConsultaContactoSolicitud;
        event EventHandler eConsultaVendedorSolicitud;
    }
}
