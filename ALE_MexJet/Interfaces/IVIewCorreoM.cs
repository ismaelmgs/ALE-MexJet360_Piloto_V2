using ALE_MexJet.Objetos;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE_MexJet.Interfaces
{
    public interface IVIewCorreoM : IBaseView
    {
        CorreoM oCorreo { get; }

        event EventHandler eSearchCorreo;

        void MostrarMensaje(string sMensaje, string sCaption);

        void LoadContactos(DataTable dtObjCat);
        void LoadCorreo(DataTable dtObjCat);
    }
}