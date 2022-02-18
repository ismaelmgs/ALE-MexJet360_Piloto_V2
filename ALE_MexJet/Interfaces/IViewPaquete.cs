using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using ALE_MexJet.Objetos;

namespace ALE_MexJet.Interfaces
{
    public interface IViewPaquete : IBaseView
    {

        object oCrud { get; set; }

        Enumeraciones.TipoOperacion eCrud { set; get; }

        object[] oArrFiltros { get; }
        object[] oArrFiltrosCue { get; }
        int iIdTipoPaquete { get; }
        bool bDuplicado { get; set; }
        DataTable dtCuentas { get; set; }
        DataTable dtProyectoSAP { get; set; }
        void ObtieneValoresPaquete();
        void ObtieneValoresCuentas();

        

        void LoadObjects(DataTable dtObjCat);
        void LoadObjectsCuentas(DataTable dtObjCat);
        void MostrarMensaje(string sMensaje, string sCaption);
        void MostrarMensajeCuenta(string sMensaje, string sCaption);

        event EventHandler eNewObjCue;
        event EventHandler eObjSelectedCue;
        event EventHandler eSaveObjCue;
        event EventHandler eDeleteObjCue;
        event EventHandler eSearchObjCue;
        event EventHandler eGetCuentas;
        event EventHandler eGetProyectoSAP;

    }
}

