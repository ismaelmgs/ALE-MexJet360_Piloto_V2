using ALE_MexJet.Objetos;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE_MexJet.Interfaces
{
    public interface IViewAutorizacion : IBaseView
    {
        int iIdRemision { get; set; }
        int iIdAjuste { get; set; }
        void LoadRemision(DataSet ds);
        void MostrarMensaje(string sMensaje);
        AjusteRemision oAjuste { get; }
    }
}
