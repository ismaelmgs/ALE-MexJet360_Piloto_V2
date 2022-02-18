using ALE_MexJet.Objetos;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE_MexJet.Interfaces
{
    public interface IViewMonitorCliente : IBaseView
    {
        object oCrud { get; set; }
        Enumeraciones.TipoOperacion eCrud { set; get; }
        object[] oArrFiltros { get; }
        object[] oArrEliminaCaso { get; }
        object[] oArrConsultaCasoEd { get; }
        int iIdContrato { get; }
        string sCodigoCliente { get; set; }
        int iIdSolicitud { get; }
        int iIdCaso { get; set; }
        int iIdCliente { get; }
        int iIdMotivo { get; set; }
        object[] oGuardaSeguimiento { get; }

        void CargaDatosCaso(DataTable DT);
        void ObtieneValores();
        void ObtieneCasosTramo();

        void LoadObjects(DataTable dtObjCat);
        void LoadCasosTramo(DataTable dtObjCat);
        void LoadSolicitudEspecial(DataTable dtObjCat);
        void MostrarMensaje(string sMensaje, string sCaption);
        void CargaCasoID(int ID);
        Casos oCatalogoCasos { get; }

        void LoadDetalleContrato(DataTable dtObjCat);
        void LoadDetallePiernasSolicitud(DataTable dtObjCat);
        void LoadDatosContrato(DataTable dtObjCat);
        void LoadCasos(DataTable dtObjCat);
        void LoadMotivos(DataTable dtObjCat);
        void LoadAreas(DataTable dtObjCat);
        void LoadObservaciones(DataTable dtObjCat);
        void LoadCorreo(DataSet DT);
        void LoadContactosCliente(DataTable dtObjCat);
        event EventHandler eSearchDetalle;
        event EventHandler eSearchPiernas;
        event EventHandler eSearchDatosContrato;
        event EventHandler eSearchCasos;
        event EventHandler eSearchMotivo;
        event EventHandler eSearchAreas;
        event EventHandler eSearchCasosTramo;
        event EventHandler eSearchObservaciones;
        event EventHandler eSearchContactosCliente;
        event EventHandler eDeleteSolicitud;
        event EventHandler eSearchSolicitudEspecial;
        event EventHandler eConsultaCorreo;
        event EventHandler eEliminaCaso;
        event EventHandler eConsultaCasoEd;
        event EventHandler eGuardaSeguimientoHistorico;
    }
}

