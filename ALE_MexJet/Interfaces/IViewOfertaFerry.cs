using ALE_MexJet.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE_MexJet.Interfaces
{
    public interface IViewOfertaFerry : IBaseView
    {
        void LoadFerrys(DataTable dt);
        DataTable dtFerrysHijo { set;  get; }
        void MostrarMensaje(string sMensaje, string sCaption);
        void CreaCSV(string cadena);
        void enviaCorreoJetSmarter();

        //List<OfertaFerry> oLstFerrys { get; }
        List<object> oListFerrys { get; }

        List<OfertaFerry> oLstFerrysPendiente { get; }
        List<OfertaFerry> oLstFerrysHijo { get; }
        string sFiltroO { set; get; }
        string sFiltroD { set; get; }
        DataTable dtOrigen { set; get; }
        DataTable dtDestino { set; get; }
        DataTable dtMatriculas { set; get; }
        int iIdPadre { set; get; }
        int iIdPendiente { set; get; }
        string sIdsListaDifusion { set; get; }
        string sTipoListaDifusion { set; get; }
        DataTable dtListasDifusion { set; get; }


        event EventHandler eSearchEnviadas;
        event EventHandler eLoadOrigDestFiltro;
        event EventHandler eLoadOrigDestFiltroDest;
        event EventHandler eSavFerry;
        event EventHandler eSavFerryPendiente;
        event EventHandler eSearchFerryPendiente;
        event EventHandler eSearchFerryHijo;
        event EventHandler eSaveListaDifusionFerry;
        event EventHandler eSearchListaDifusionFerry;
        event EventHandler eDeleteOfertaFerryPendiente;
        event EventHandler eSaveObjFerryHijo;
        event EventHandler eSearchListaDifusion;
    }
}
