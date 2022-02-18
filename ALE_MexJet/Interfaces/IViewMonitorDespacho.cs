using ALE_MexJet.Objetos;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE_MexJet.Interfaces
{
    public interface IViewMonitorDespacho : IBaseView  
    {
        object[] oArrFiltros { get; }
        object[] oArrFilSolicitud { get; }
        object[] oArrFilUpd { get; }
        object[] oArrFilSeguimiento { get; }
        int iIdSolicitudApp { set; get; }

        event EventHandler eBuscaDDL;
        event EventHandler eBuscaSubGrid;
        event EventHandler eBuscaDictamen;
        event EventHandler eSaveSeguimiento;
        event EventHandler eBuscaPiernadictamen;
        void MostrarMensaje(string sMensaje, string sCaption);
        void LlenaPiernaDictamen(DataTable dtObjCat);
        void LlenaDDL(DataTable dtObjCat);
        void LlenaGrid(DataTable dtObjCat);
        void LlenaSubGrid(DataTable dtObjCat);
        void LlenaDictamen(DataTable dtObjCat);
    }
}