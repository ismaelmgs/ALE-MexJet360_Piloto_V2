using ALE_MexJet.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE_MexJet.Interfaces
{
    public interface IViewPermisosContrato : IBaseView
    {
        int iIdRol { get; }
        int iIdPestana { get; }
        DataTable dtRoles { set; get; }
        DataTable dtPestanas { set; get; }
        DataTable dtSecciones { set; get; }
        DataTable dtCampos { set; get; }
        PermisosContrato oPermisos { get; }

        void MostrarMensaje(string Mensaje, string Caption);
        void LoadObjects();
        //void HabilitaPermisosCheckBoxContrato(DataSet dt);

        //event EventHandler eGetPermisosCheckBoxContrato;
    }
}
