using ALE_MexJet.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE_MexJet.Interfaces
{
    public interface IViewExtensionServicios : IBaseView
    {
        DataTable dtOrigenDestino { set; get; }
        ExtensionServicios oExtensionServ { set; get; }
        int iExtension { set; get; }

        void LoadDatosControles(DataTable dtPiloto, DataTable dtMat, DataSet dsTiempo);
        void MostrarMensaje(string sMensaje, string sCaption);
        void ImprimirReporte(ExtensionServiciosReporte oExtensionServicioReporte);
        void EnviarMailReporte(ExtensionServiciosReporte oExtensionServicioReporte);

        event EventHandler eLoadOrigDestFiltro;
        event EventHandler eLoadExtensionServicioImprimir;
        event EventHandler eLoadExtensionServicioEnviarMail;
        
    }
}
