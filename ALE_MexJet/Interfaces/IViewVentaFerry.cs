using ALE_MexJet.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE_MexJet.Interfaces
{
    public interface IViewVentaFerry : IBaseView
    {
        string sFiltroO { set; get; }
        string sFiltroD { set; get; }
        DataTable dtOrigen { set; get; }
        DataTable dtDestino { set; get; }
        VentaFerry oVFerry { get; }
        OfertaFerry oVFerry2 { get; }
        DataTable dtFerrysHijo { set; get; }
        int iIdPadre { set; get; }
        DataTable dtMatriculas { set; get; }
        OfertaFerry oFerrysHijo { get; }
        InformacionFerry oInfoFerry { get; }

        void LoadFerrys(DataTable dt);
        void MostrarMensaje(string sMensaje, string sCaption);

        event EventHandler eSearchFerryHijo;
        event EventHandler eLoadOrigDestFiltro;
        event EventHandler eLoadOrigDestFiltroDest;
        event EventHandler eSearchMatricula;
        event EventHandler eSaveFerryHijo;
        event EventHandler eSaveInformacionFerry;
    }
}
