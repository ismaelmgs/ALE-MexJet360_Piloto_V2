using ALE_MexJet.Objetos;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE_MexJet.Interfaces
{
    public interface IViewBitacoraAudit : IBaseView
    {
        event EventHandler eObjModulo;
        event EventHandler eObjAccion;
        event EventHandler eObjUsuario;

        object[] oArrFiltros { get; }

        void MostrarMensaje(string sMensaje, string sCaption);

        void LoadModulo(DataTable dtObjCat);

        void LoadAccion(DataTable dtObjCat);

        void LoadUsuario(DataTable dtObjCat);

        void LoadBitacoraAudit(DataTable dtObjCat);
    }
}