using ALE_MexJet.Objetos;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE_MexJet.Interfaces
{
    public interface IViewDocPendientes : IBaseView
    {
        event EventHandler eObjCliente;
        event EventHandler eObjConrato;
        event EventHandler eObjBitPend;
        event EventHandler eObjRemPend;
        event EventHandler eObjFacPend;

        void MostrarMensaje(string sMensaje, string sCaption);
        void LoadCliente(DataTable dtObjCat);
        void LoadContrato(DataTable dtObjCat);
        void LoadBitPend(DataTable dtObjCat);
        void LoadRemPend(DataTable dtObjCat);
        void LoadFacPend(DataTable dtObjCat);

        DocPendientes oDocContrato { get; }

        DocPendientes oBitPen { get; }
    }
}