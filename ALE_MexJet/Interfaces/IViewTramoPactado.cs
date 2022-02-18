using ALE_MexJet.Objetos;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE_MexJet.Interfaces
{
    public interface IViewTramoPactado : IBaseView
    {
        // oGetSetObjSelection { get; set; }
        //TObjCat oCatalogo { get; set; }
        object oCrud { get; set; }
        bool bDuplicado { get; set; }
        Enumeraciones.TipoOperacion eCrud { set; get; }

        object[] oArrFiltros { get; }

        void ObtieneValores();
        void LoadObjects(DataTable dtObjCat);
        void MostrarMensaje(string sMensaje, string sCaption);
        void LoadCatalogoAeropuertos(DataTable dtObjCat);
        void LoadCatalogoGrupoModelo(DataTable dtObjCat);


        event EventHandler eGetGrupoModelo;
        event EventHandler eGetAeropuerto;


        DataTable dtTarifaOrigen { get; set; }
        DataTable dtTarifaDestino { get; set; }

        event EventHandler eGetAeropuertoOrigen;
        event EventHandler eGetAeropuertoDestino;
        event EventHandler eGetAeropuertoOrigenFiltrado;
        event EventHandler eGetAeropuertoDestinoFiltrado;

        string sFiltroAeropuerto { get; set; }
        int iOrigen { get; set; }

    }
}

