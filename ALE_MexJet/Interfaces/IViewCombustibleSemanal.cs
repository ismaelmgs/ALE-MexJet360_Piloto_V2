using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using ALE_MexJet.Objetos;

namespace ALE_MexJet.Interfaces
{
    public interface IViewCombustibleSemanal : IBaseView
    {

        object oCrud { get; set; }

        Enumeraciones.TipoOperacion eCrud { set; get; }

        object[] oArrFiltros { get; }
        object[] oArrFiltrosDet { get; }
        object[] oArrFiltrosMes { get; }
        object[] oArrFiltrosAero { get; }
        int iIdCombustibleSem { get; }
        bool bDuplicado { get; set; }
        bool bExisteTipoCambio { get; set; }

        void ObtieneValoresCombustible();
        void ObtieneValoresDetalles();
        void LoadObjectsMes(DataTable dtMes);
        void LoadObjectsAero(DataTable dtAero);

        

        void LoadObjects(DataTable dtObjCat);
        void LoadObjectsDetalle(DataTable dtObjCat);
        void MostrarMensaje(string sMensaje, string sCaption);
        void MostrarMensajeDet(string sMensaje, string sCaption);

        event EventHandler eSearchObjMes;
        event EventHandler eSearchObjAero;
        event EventHandler eNewObjDet;
        event EventHandler eObjSelectedDet;
        event EventHandler eValTipoCambio;
        event EventHandler eSaveObjDet;
        event EventHandler eDeleteObjDet;
        event EventHandler eSearchObjDet;
    }
}

