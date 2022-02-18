using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using ALE_MexJet.Objetos;

namespace ALE_MexJet.Interfaces
{
    public interface IViewCombustibleMensualInternacional: IBaseView
    {

        object oCrud { get; set; }

        Enumeraciones.TipoOperacion eCrud { set; get; }

        object[] oArrFiltros { get; }
        object[] oArrFiltrosMes { get; }
        int iIdCombustibleMenInt { get; }
        bool bDuplicado { get; set; }

        void ObtieneValoresCombustible();
        void LoadObjectsMes(DataTable dtMes);

        

        void LoadObjects(DataTable dtObjCat);
        void MostrarMensaje(string sMensaje, string sCaption);

        event EventHandler eSearchObjMes;
    }
}

