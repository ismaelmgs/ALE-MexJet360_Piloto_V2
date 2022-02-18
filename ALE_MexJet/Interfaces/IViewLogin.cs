using ALE_MexJet.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE_MexJet.Interfaces
{
    public interface IViewLogin : IBaseView
    {
        object[] oArrFiltros { get; }
        void ObtieneValores();
        void LoadObjects(DataTable dtObjCat);
        void alert(string m);

        int iToken { get; set; }
        String sUsuario { get;  }//; set; }
        int TokenGuardado { get; set; }
        string sToken { get; }
        long iRestoken { set; get; }
        string sNumeroTel { set; get; }
        string sClaveToken { set; get; }
        

        event EventHandler eSearchToken;
        //void MostrarMensaje(string sMensaje, string sCaption);

    }
}
