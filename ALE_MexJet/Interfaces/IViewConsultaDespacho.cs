using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE_MexJet.Interfaces
{
    public interface IViewConsultaDespacho: IBaseView
    {
        object[] oArrFiltros { get; }
        object[] oArrFiltroContrato { get; }
        object[] oArrFiltroClientes { get; }
        object[] oArrFilSolicitud { get; }
        object[] oArrInsertDictamen { get; }

        void MostrarMensaje(string sMensaje, string sCaption);
        void LoadConsultaDespacho(DataTable dtConsultaDespacho);
        void CargaConsultaDespacho();
        void ObtieneClientes();
        void ObtieneContrato();
        void LoadClientes(DataTable dtObjCat);
        void LoadContrato(DataTable dtObjCat);
        void LoadPiernas(DataTable dtObjCat);
        void LoadSol(DataTable dtObjCat);

        event EventHandler eGetClientes;
        event EventHandler eGetContrato;
        event EventHandler eBuscaSubGrid;
        event EventHandler eInsertaDictamen;
        event EventHandler eConsultaSol;
    }
}
