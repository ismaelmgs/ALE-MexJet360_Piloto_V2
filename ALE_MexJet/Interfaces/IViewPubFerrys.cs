using ALE_MexJet.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE_MexJet.Interfaces
{
    public interface IViewPubFerrys : IBaseView
    {
        int iIdFerry { set; get; }
        string sFiltroO { set; get; }
        string sFiltroD { set; get; }
        DataTable dtOrigen { set; get; }
        DataTable dtDestino { set; get; }
        DataTable dtMatriculas { set; get; }
        OfertaFerry oFerrysPadre { get; }
        List<OfertaFerry> oLstFerrysHijo { get; }

        void ConfimaPublicacion(string sTitulo, string sMensaje);
        void MostrarMensaje(string sTitulo, string sMensaje);

        event EventHandler eLoadOrigDestFiltro;
        event EventHandler eLoadOrigDestFiltroDest;
        event EventHandler eSavFerryPendiente;
        event EventHandler SearchMatricula;
    }
}
