using ALE_MexJet.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE_MexJet.Interfaces
{
    public interface IViewImagen : IBaseView
    {
        object[] oInsertaPasajero { get; }
        object[] oArrFiltros { get; }
        object[] oNotas { get; }
        object[] oArrFilUpd { get; }
        void LoadArea(DataTable dtObjCat);
        void LoadPaxTramo(DataTable dtObjCat);
        void LoadPaxTramo2(DataTable dtObjCat);
        void LoadObjects(DataTable dtObjCat);
        void LoadPersona(DataTable dtObjCat);
        void LoadCliente(DataTable dtObjCat);
        void LoadImagen(DataTable dtObjCat);
        void LoadImagenD(DataTable dtObjCat);
        void LoadPilot(DataTable dtObjCat);
        void CargaPasajeros(DataTable DT);
        void LoadCorreoDespacho(DataTable dtCorreoDespacho);

        Enumeraciones.TipoOperacion eCrud { set; get; }
        object oCrud { get; set; }
        void MostrarMensaje(string sMensaje, string sCaption);
        Imagen oBusqueda { get; }
        Imagen pBusqueda { get; }
        Imagen BusquedaC { get; }
        Imagen BusquedaI { get; }


        event EventHandler pSearchObj;
        event EventHandler IUpdObj;
        event EventHandler SearchObC;
        event EventHandler SearchObI;
        event EventHandler SearchObD;
        event EventHandler SaveUpdaI;
        event EventHandler SearchObjPilot;
        event EventHandler eEditaPaxTramo;
        event EventHandler eEliminaPaxTramo;
        event EventHandler eInsertaPasajero;
        event EventHandler eInsertaPaxTramo;
        event EventHandler eObtienePaxTramo;
        event EventHandler eLoadPaxTramo2;
        event EventHandler eBuscaPasajero;
        event EventHandler eInsertaInteriores;
        event EventHandler eConsultaArea;
        event EventHandler eEnviaNotificacionDespacho;
    }
}