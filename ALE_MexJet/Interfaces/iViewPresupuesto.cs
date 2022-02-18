using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ALE_MexJet.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ALE_MexJet.Interfaces
{
    public interface iViewPresupuesto : IBaseView
    {
        
        DataTable dtClientes { get; set; }
        int iIdCliente { get; }
        DataTable dtContrato { get; set; }
        int iIdContrato { get; }
        DataTable dtSolicitante { get; set; }
        DataTable dtGrupoModelo { get; set; }
        DataTable dtContactos { get; set; }
        DataTable dtOrigen { set; get; }
        DataTable dtDestino { set; get; }
        DataTable dtTramos { set; get; }
        DataTable dtTramos2 { set; get; }
        int iTipoFiltro { get; }
        string sFiltroO { set; get; }
        string sFiltroD { set; get; }
        string sAeropuertoO { get; }
        string sRutaTramos { get; }
        string sRutaTramos2 { get; }
        int iIdGrupoModelo { get; }
        string sIdGrupoModeloPre { get; }
        decimal dIvaSV { set; get; }
        decimal dSubTotSC { get; set; }
        decimal dSubTotSC2 { get; set; }
        DatosRemision oDatos { set; }
        Presupuesto oPresupuesto { set; get; }
        int iIdTipoAeropuerto { get; }
        int iIdPresupuesto { set; get; }
        string sMensaje { set; get; }
        Enumeraciones.SeCobraFerrys eSeCobraFerrys { set; get; }
        SolicitudVuelo oSolicitudVuelo { get; set; }
        int iIdSolicitudVuelo { set; get; }


        void ObtieneValores();
        void LoadObjects(DataTable dtObjCat);
        void MostrarMensaje(string sMensaje, string sCaption);
        void LoadServiciosC(DataTable dtImp, DataTable dtServ, DataTable dtTramos);
        void LoadServiciosDosPresupuestos(DataTable dtImp, DataTable dtServ, DataTable dtTramos);
        object[] oGuardaSeguimiento { get; }
        void ObtieneViabilidad();
        void MuestraTextoDosPresupuestos(string sTexto);
        bool bViabilidad { get; set; }


        
        event EventHandler eViabilidad;
        event EventHandler eInsertaMonitorDespacho;
        event EventHandler eGuardaSeguimientoHistorico;
        event EventHandler eGetDatosCliente;
        event EventHandler eGetDatosContrato;
        event EventHandler eLoadOrigDestFiltro;
        event EventHandler eLoadOrigDestFiltroDest;
        event EventHandler eGetCalculoPresupuesto;
        event EventHandler eGetReCalculoPresupuesto;
        event EventHandler eSaveSolicitud;

        event EventHandler eGetCalculoPresupuesto2;
        event EventHandler eGetReCalculoPresupuesto2;

        List<TramoSolicitud> lstObjTramos { get; set; }
    }
}
