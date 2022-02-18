using ALE_MexJet.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE_MexJet.Interfaces
{
    public interface IViewAutorizacionMembresias : IBaseView
    {
        int iIdMiembro { get; set; }
        int iIdStatus { get; set; }
        object[] oArrFiltros { get; }
        MiembroEZ oMiembro { get; }
        DataTable dtContratos { set; get; }
        DataTable dtTiposClientes { get; set; }

        void LoadObjects(DataTable dtObjCat);
        void MostrarMensaje(string sMensaje, string sCaption);
    }
}
