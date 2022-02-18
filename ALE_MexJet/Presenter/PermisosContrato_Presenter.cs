using ALE_MexJet.Clases;
using ALE_MexJet.DomainModel;
using ALE_MexJet.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ALE_MexJet.Presenter
{
    public class PermisosContrato_Presenter : BasePresenter<IViewPermisosContrato>
    {
        private readonly DBPermisosContrato oIGestCat;

        public PermisosContrato_Presenter(IViewPermisosContrato oView, DBPermisosContrato oGC)
            : base(oView)
        {
            oIGestCat = oGC;
        }

        protected override void SearchObj_Presenter(object sender, EventArgs e)
        {
            oIView.dtRoles = oIGestCat.DBGetObtieneRolesActivos();
            oIView.dtPestanas = oIGestCat.DBGetObtenePestanasContrato();
            oIView.LoadObjects();
        }

        protected override void ObjSelected_Presenter(object sender, EventArgs e)
        {
            oIView.dtSecciones = oIGestCat.DBGetObteneSeccionesPestanasContrato(oIView.iIdRol, oIView.iIdPestana);
            oIView.dtCampos = oIGestCat.DBGetObteneCamposSeccionesPestanasContrato(oIView.iIdRol, oIView.iIdPestana);
        }

        protected override void SaveObj_Presenter(object sender, EventArgs e)
        {
            try
            {
                if (oIGestCat.DBSetInsertaPermisosRolContrato(oIView.oPermisos) > 0)
                {
                    //oIView.MostrarMensaje("Los permisos de asignaron de forma correcta.", "Aviso");
                }
                else
                    oIView.MostrarMensaje("Ocurrió un error al actualizar los permisos.", "Aviso");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //protected void eGetPermisosCheckBoxContrato_Presenter(object sender, EventArgs e)
        //{
        //    DataSet ds = new DBPermisosContrato().DBGetObtienePermisosContratoParaCheckBox(Utils.GetRolUser);
        //    oIView.HabilitaPermisosCheckBoxContrato(ds);
        //}
    }
}