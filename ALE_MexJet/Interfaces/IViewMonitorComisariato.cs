using ALE_MexJet.Objetos;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE_MexJet.Interfaces
{
    public interface IViewMonitorComisariato:IBaseView
    {
        object oCrud { get; set; }
        void ObtieneValores();
        object[] oArrFiltros { get; }
        object[] oArrFil { get; }
        object[] oArrID { get; }
        void LoadObjects(DataTable dtObjMonitor);
        void LoadStatus(DataTable dtObjSatus);
        void LoadComisariatoDetalle(DataTable dtObjSatus);
        void MostrarMensaje(string sMensaje, string sCaption);

        Enumeraciones.TipoOperacion eCrud { set; get; }
        event EventHandler eLoadStaus;
        event EventHandler eActualizaStataus;
        event EventHandler eInsertaDetalle;
        event EventHandler eConsultaComisariatoDetalle;
    }
}
