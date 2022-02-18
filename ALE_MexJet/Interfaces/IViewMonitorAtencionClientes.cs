using ALE_MexJet.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE_MexJet.Interfaces
{
    public interface IViewMonitorAtencionClientes : IBaseView  
    {
        object[] oArrConsultaMonitorAtencionClientes { get; }
        object[] oArrConsultaMonitorAtencionClientesDetalle { get; }
        object[] oArrConsultaNotas { get; }
        object[] oArrActualizaStatus { get; }
        object[] oArrEditaAtncclientes { get; }

        void CargaConsultaMonitorAtencionClientes(DataTable dtObjCat);
        void CargaConsultaMonitorAtencionClientesDetalle(DataTable dtObjCat);
        void loadConsultaMonitorAtencionClientes();
        void loadConsultaMonitorAtencionClientesDetalle();
        void CargaNotas(DataSet dsNotas);
        void CargaAeropuertoBase(DataTable dtAeropuertoBase);

        Enumeraciones.TipoOperacion eCrud { set; get; }
        object oCrud { get; set; }

        int iIdSolicitud { get; set; }

        event EventHandler eSearchDetalle;
        event EventHandler eBuscaNotas;
        event EventHandler eSearchAropuertoBase;
        event EventHandler eActualizaStatus;
        event EventHandler eEditaAtnCliente;
    }
}
