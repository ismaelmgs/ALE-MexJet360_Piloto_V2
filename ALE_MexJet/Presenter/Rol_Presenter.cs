using ALE_MexJet.DomainModel;
using ALE_MexJet.Interfaces;
using ALE_MexJet.Objetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NucleoBase.Core;
using DevExpress.Web.Data;
using DevExpress.Web;
using ALE_MexJet.Clases;

namespace ALE_MexJet.Presenter
{
    public class Rol_Presenter : BasePresenter<IViewRol>
    {
        private readonly DBRol oIGestCat;

        public Rol_Presenter(IViewRol oView, DBRol oGC)
            : base(oView)
        {
            oIGestCat = oGC;
            oIView.eSearchCloneO += eGetRolO_Presenter;
            oIView.eSearchCloneD += eGetRolD_Presenter;
            oView.eSearchModDef += SearchObjModulo_Presenter;
            oIView.eCloneObj += CloneObj_Presenter;
        }

        public void LoadObjects_Presenter()
        {
            oIView.LoadObjects(oIGestCat.DBSearchObj(oIView.oArrFiltros));
        }

        protected override void SearchObj_Presenter(object sender, EventArgs e)
        {
            oIView.LoadObjects(oIGestCat.DBSearchObj(oIView.oArrFiltros));
        }
        protected void SearchObjModulo_Presenter(object sender, EventArgs e)
        {
            oIView.LoadModulo(oIGestCat.dtObjModulos());
        }
        protected override void SaveObj_Presenter(object sender, EventArgs e)
        {
            try
            {
                Rol rol = oCatalogo;
                int id = oIGestCat.DBUpdate(rol);
                if (id > 0)



                {
                    oIView.ObtieneValores();
                    oIView.MostrarMensaje(Mensajes.LMensajes[Enumeraciones.Mensajes.Modificacion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);

                }
            }
            catch (Exception ex)
            {
                oIView.MostrarMensaje("Se detectó el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
            }
        }

        protected override void NewObj_Presenter(object sender, EventArgs e)
        {
            try
            {

                Rol rol = oCatalogo;
                int id = oIGestCat.DBSave(rol);
                if (id > 0)
                {
                    oIView.MostrarMensaje(Mensajes.LMensajes[Enumeraciones.Mensajes.Insercion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
                    oIView.ObtieneValores();
                }

            }
            catch (Exception ex)
            {
                oIView.MostrarMensaje("Se detectó el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
            }
        }
        protected  void CloneObj_Presenter(object sender, EventArgs e)
        {
            try
            {

                Rol rol = oCatalogo;
                int id = oIGestCat.DBClone(rol);
                if (id > 0)
                {
                    oIView.MostrarMensaje(Mensajes.LMensajes[Enumeraciones.Mensajes.Clonacion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
                    oIView.ObtieneValores();
                }

            }
            catch (Exception ex)
            {
                oIView.MostrarMensaje("Se detectó el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
            }
        }

        protected override void DeleteObj_Presenter(object sender, EventArgs e)
        {
            try
            {
                Rol rol = oCatalogo;
                int id = oIGestCat.DBDelete(rol);
                if (id > 0)
                {
                    oIView.MostrarMensaje(Mensajes.LMensajes[Enumeraciones.Mensajes.Eliminacion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
                    oIView.ObtieneValores();
                }
            }
            catch (Exception ex)
            {
                oIView.MostrarMensaje("Se detectó el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
            }
        }

        protected override void ObjSelected_Presenter(object sender, EventArgs e) {
            int existe = 0;
            if (oIView.eCrud == Enumeraciones.TipoOperacion.Clonar)
            {
                existe = 0;
            }
            else
            {
                if (oIView.eCrud == Enumeraciones.TipoOperacion.Actualizar)
                {
                    if (bValidaActualizacion)
                    {
                        oIView.eCrud = Enumeraciones.TipoOperacion.Validar;
                        existe = oIGestCat.DBValida(oCatalogo);
                    }

                }
                else
                {
                    oIView.eCrud = Enumeraciones.TipoOperacion.Validar;
                    existe = oIGestCat.DBValida(oCatalogo);
                }
            }
            oIView.bDuplicado = existe > 0;
        }
        protected void eGetRolO_Presenter(object sender, EventArgs e)
        {
            oIView.LoadObjectsO(oIGestCat.DBSearchObj(oIView.oArrFiltrosDll));
        }
        protected void eGetRolD_Presenter(object sender, EventArgs e)
        {
            oIView.LoadObjectsD(oIGestCat.DBSearchObj(oIView.oArrFiltrosDll));
        }
       
        private Rol oCatalogo
        {
            get
            {
                Rol oRol = new Rol();
                switch (oIView.eCrud)
                {
                    case Enumeraciones.TipoOperacion.Insertar:
                        ASPxDataInsertingEventArgs eI = (ASPxDataInsertingEventArgs)oIView.oCrud;
                        oRol.iId = 0;
                        oRol.sDescripcion = eI.NewValues["RolDescripcion"].S();
                        oRol.iIdModuloDefault = eI.NewValues["ModuloId"].S().I();
                        oRol.iStatus = 1;
                        break;
                    case Enumeraciones.TipoOperacion.Actualizar:
                        ASPxDataUpdatingEventArgs eU = (ASPxDataUpdatingEventArgs)oIView.oCrud;

                        oRol.iId = eU.Keys[0].S().I();
                        oRol.sDescripcion = eU.NewValues["RolDescripcion"].S();
                        oRol.iIdModuloDefault = eU.NewValues["ModuloId"].S().I();
                        oRol.iStatus = eU.NewValues["Status"].S().I();
                        break;
                    case Enumeraciones.TipoOperacion.Validar:
                        ASPxDataValidationEventArgs eV = (ASPxDataValidationEventArgs)oIView.oCrud;
                        oRol.sDescripcion = eV.NewValues["RolDescripcion"].S();

                        oRol.iStatus = eV.NewValues["Status"].S().I();

                        break;
                    case Enumeraciones.TipoOperacion.Eliminar:
                        ASPxDataDeletingEventArgs eD = (ASPxDataDeletingEventArgs)oIView.oCrud;
                        oRol.sDescripcion = eD.Values["RolDescripcion"].S();
                        oRol.iId = eD.Keys[0].S().I();
                        break;
                    case Enumeraciones.TipoOperacion.Clonar:
                        ASPxDataInsertingEventArgs eC = (ASPxDataInsertingEventArgs)oIView.oCrud;
                        oRol.iIdRolO = eC.NewValues["RolIdO"].I();
                        oRol.iIdRolD = eC.NewValues["RolIdD"].I();
                        break;


                }

                return oRol;
            }
        }

        private bool bValidaActualizacion
        {
            get{
                bool bValida = true;
                ASPxDataValidationEventArgs eU = (ASPxDataValidationEventArgs)oIView.oCrud;

                bValida = eU.NewValues["RolDescripcion"].S().ToUpper() != eU.OldValues["RolDescripcion"].S().ToUpper();

                return bValida;
            }
        }
    }
}