using ALE_MexJet.DomainModel;
using ALE_MexJet.Interfaces;
using ALE_MexJet.Objetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NucleoBase.Core;
using DevExpress.Web.Data;
using ALE_MexJet.Clases;

namespace ALE_MexJet.Presenter
{
    public class Usuario_Presenter : BasePresenter<IViewUsuario>
    {

        private readonly DBUsuarios oIGestCat;

        public Usuario_Presenter(IViewUsuario oView, DBUsuarios oGC)
            : base(oView)
        {
            oIGestCat = oGC;
            oIView.eGetAero += eGetAero_Presenter;
            oIView.eGetRol += eGetRol_Presenter;

        }

        public void LoadObjects_Presenter()
        {
            oIView.LoadObjects(oIGestCat.DBSearchObj(oIView.oArrFiltros));
        }

        protected override void SearchObj_Presenter(object sender, EventArgs e)
        {
            oIView.LoadObjects(oIGestCat.DBSearchObj(oIView.oArrFiltros));
        }

        protected override void SaveObj_Presenter(object sender, EventArgs e)
        {
            try
            {
                int id = oIGestCat.DBUpdate(oCatalogo);
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
                /*int existe = 0;
                oIView.eCrud = Enumeraciones.TipoOperacion.Validar;
                existe = oIGestCat.DBValida(oCatalogo);
                
                if (existe > 0)
                    oIView.MostrarMensaje("El usuario ya existe, utilizar otro.", "Aviso");
                else
                */
                oIView.eCrud = Enumeraciones.TipoOperacion.Insertar;
                int id = oIGestCat.DBSave(oCatalogo);
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

        protected override void DeleteObj_Presenter(object sender, EventArgs e)
        {
            try
            {
                int id = oIGestCat.DBDelete(oCatalogo);
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

        protected void eGetAero_Presenter(object sender, EventArgs e)
        {
            oIView.LoadCatalogoBase(oIGestCat.dtGetAeronave);
        }

        protected void eGetRol_Presenter(object sender, EventArgs e)
        {
            oIView.LoadCatalogoRol(oIGestCat.dtGetRol);
        }

        protected override void ObjSelected_Presenter(object sender, EventArgs e)
        {
            int existe = 0;
            if (bValidaActualizacion)
            {
                oIView.eCrud = Enumeraciones.TipoOperacion.Validar;
                existe = oIGestCat.DBValida(oCatalogo);
            }
            oIView.bDuplicado = existe > 0;
        }


        private Usuarios oCatalogo
        {
            get
            {
                Usuarios oUsuario = new Usuarios();
                switch (oIView.eCrud)
                {
                    case Enumeraciones.TipoOperacion.Insertar:
                        ASPxDataInsertingEventArgs eI = (ASPxDataInsertingEventArgs)oIView.oCrud;
                        oUsuario.iId = 0;
                        oUsuario.sDescripcion = eI.NewValues["Descripcion"].S();
                        oUsuario.iIdRol = eI.NewValues["Rol"].S().I();
                        oUsuario.iIdBase = eI.NewValues["IdBase"].S().I();
                        oUsuario.iStatus = 1;
                        oUsuario.sNoTelefonico = eI.NewValues["NoTelefonico"].S();

                        break;
                    case Enumeraciones.TipoOperacion.Actualizar:
                        ASPxDataUpdatingEventArgs eU = (ASPxDataUpdatingEventArgs)oIView.oCrud;

                        oUsuario.iId = eU.Keys[0].S().I();
                        oUsuario.sDescripcion = eU.NewValues["Descripcion"].S();
                        oUsuario.iIdRol = eU.NewValues["Rol"].S().I();
                        oUsuario.iIdBase = eU.NewValues["IdBase"].S().I();
                        oUsuario.iStatus = eU.NewValues["Status"].S().I();
                        oUsuario.sNoTelefonico = eU.NewValues["NoTelefonico"].S();

                        break;
                    case Enumeraciones.TipoOperacion.Validar:
                        ASPxDataValidationEventArgs eV = (ASPxDataValidationEventArgs)oIView.oCrud;

                        oUsuario.sDescripcion = eV.NewValues["Descripcion"].S();
                        oUsuario.iIdRol = eV.NewValues["Rol"].S().I();
                        oUsuario.iIdBase = eV.NewValues["IdBase"].S().I();
                        oUsuario.iStatus = eV.NewValues["Status"].S().I();
                        oUsuario.sNoTelefonico = eV.NewValues["NoTelefonico"].S();
                        break;
                    case Enumeraciones.TipoOperacion.Eliminar:
                        ASPxDataDeletingEventArgs eD = (ASPxDataDeletingEventArgs)oIView.oCrud;
                        oUsuario.iId = eD.Keys[0].S().I();
                        break;
                }

                return oUsuario;
            }
        }
        private bool bValidaActualizacion
        {
            get
            {
                bool bValida = true;
                ASPxDataValidationEventArgs eU = (ASPxDataValidationEventArgs)oIView.oCrud;
                if (eU.OldValues["Descripcion"].S() == string.Empty)
                {
                    bValida = true;
                }
                else
                {
                    bValida = false;
                    //bValida = eU.NewValues["Descripcion"].S().ToUpper() != eU.OldValues["Descripcion"].S().ToUpper();
                }
                return bValida;
            }

        }
    }
}