using ALE_MexJet.DomainModel;
using ALE_MexJet.Interfaces;
using ALE_MexJet.Objetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NucleoBase.Core;
using DevExpress.Web.Data;
using DevExpress.Web;
using System.Data;
using System.Collections.Specialized;
using ALE_MexJet.Clases;

namespace ALE_MexJet.Presenter
{
    public class RolAccion_Presenter : BasePresenter<IViewRolAccion>
    {
        private readonly DBRolAccion oIGestCat;

        public RolAccion_Presenter(IViewRolAccion oView, DBRolAccion oGC)
            : base(oView)
        {
            oIGestCat = oGC;
            oView.eSearchObjdll += eGetTipoRol_Presenter;
            oView.eUpdateObj += eUpdateCheck_Presenter;
        }
        protected override void SearchObj_Presenter(object sender, EventArgs e)
        {
            oIView.LoadObjects(oIGestCat.DBSearchObj(oIView.oArrFiltros));
        }
        //protected override void NewObj_Presenter(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        int id = oIGestCat.DBSave(oCatalogo);
        //        if (id > 0)
        //        {
        //            oIView.MostrarMensaje("Se guardó el registro numero " + id.ToString(), "REGISTRO GENERADO");
        //            oIView.ObtieneValores();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        oIView.MostrarMensaje("Se detectó el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
        //    }
        //}
        protected override void DeleteObj_Presenter(object sender, EventArgs e)
        {
            try
            {
                int id = oIGestCat.DBDelete(oRolAccion);
                if (id > 0)
                {
                    oIView.MostrarMensaje("Se desactivó el registro numero" + id.ToString(), "REGISTRO ELIMINADO");
                    oIView.ObtieneValores();
                }
            }
            catch (Exception ex)
            {
                oIView.MostrarMensaje("Se detectó el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
            }
        }
        protected void eGetTipoRol_Presenter(object sender, EventArgs e)
        {
            oIView.LoadCatalogoRol(oIGestCat.dtTipoRolAccion(oIView.oArrFiltrosdll));
        }
        protected void eUpdateCheck_Presenter(object sender, EventArgs e)
        {
            
            int id =  oIGestCat.DBSave(oRolAccion);
            if (id > 0)
            {
                oIView.ObtieneValores();
                oIView.MostrarMensaje("Se guardaron los permisos satisfactoriamente ", "PERMISOS ACTUALIZADOS");

            }
        }
        private RolAccion oRolAccion
        {
            get
            {
                List<ASPxDataUpdateValues> oLiUpValues = null;
                RolAccion oRol = new RolAccion();
                oRol.dtaRolAccion = oIView.CrearDataTable;
                int iColumnas = 0;
                int iCon = 0;
                switch (oIView.eCrud)
                {
                    case Enumeraciones.TipoOperacion.Insertar:
                        ASPxDataBatchUpdateEventArgs eU = (ASPxDataBatchUpdateEventArgs)oIView.oCrud;
                        oLiUpValues = eU.UpdateValues;
                        ASPxComboBox ddlRolAccion = (ASPxComboBox)oIView.oSelec;
                        foreach (ASPxDataUpdateValues oUpValues in oLiUpValues)
                        {
                            iColumnas = oUpValues.NewValues.Count-1;
                                for (iCon = 1; iCon <= iColumnas; iCon++)
                                {
                                    DataRow dRow = oRol.dtaRolAccion.NewRow();
                                    switch (iCon)
                                    {
                                        case 1: dRow["IdAccion"] = 1;
                                            dRow["Permitido"] = oUpValues.NewValues["Consultar"]; break;
                                        case 2: dRow["IdAccion"] = 2;
                                            dRow["Permitido"] = oUpValues.NewValues["Insertar"]; break;
                                        case 3: dRow["IdAccion"] = 3;
                                            dRow["Permitido"] = oUpValues.NewValues["Actualizar"]; break;
                                        case 4: dRow["IdAccion"] = 4;
                                            dRow["Permitido"] = oUpValues.NewValues["Eliminar"]; break;
                                        case 5: dRow["IdAccion"] = 5;
                                            dRow["Permitido"] = oUpValues.NewValues["Acceso"]; break;
                                    }

                                    dRow["IdRol"] = ddlRolAccion.Value;
                                    dRow["IdModulo"] = oUpValues.Keys["ModuloId"];
                                    dRow["Status"] = 1;
                                    dRow["UsuarioCreacion"] = Utils.GetUser;
                                    dRow["IP"] = "";
                                    oRol.dtaRolAccion.Rows.Add(dRow);

                                   
                                }
                        }


                            break;

                    case Enumeraciones.TipoOperacion.Eliminar:
                        ASPxDataDeletingEventArgs eD = (ASPxDataDeletingEventArgs)oIView.oCrud;
                        ASPxComboBox ddlRolAccionD = (ASPxComboBox)oIView.oSelec;
                        oRol.iIdRol = ddlRolAccionD.Value.S().I();
                        oRol.sIP = "";
                        break;
                }

                return oRol;
            }
        }
    }
}