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
    public class Designador_Presenter: BasePresenter<IViewCat>
    {
        private readonly DBDesignador oIGestCat;

        public Designador_Presenter(IViewCat oView, DBDesignador oGC)
            : base(oView)
        {
            oIGestCat = oGC;
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
                Designador oDesignador = oCatalogo;
                int id = oIGestCat.DBUpdate(oDesignador);
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
                Designador oDesignador = oCatalogo;
                int id = oIGestCat.DBSave(oDesignador);
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
                Designador oDesignador = oCatalogo;
                int id = oIGestCat.DBDelete(oDesignador);
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

        protected override void ObjSelected_Presenter(object sender, EventArgs e)
        {
            int existe = 0;
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
            oIView.bDuplicado = existe > 0;
        }

        private Designador oCatalogo
        {
            get
            {
                Designador oDesignador = new Designador();
                switch (oIView.eCrud)
                {
                    case Enumeraciones.TipoOperacion.Insertar:
                        ASPxDataInsertingEventArgs eI = (ASPxDataInsertingEventArgs)oIView.oCrud;
                        oDesignador.iId = 0;
                        oDesignador.sDescripcion = eI.NewValues["Descripcion"].S();
                        oDesignador.sClave = eI.NewValues["Clave"].S();
                        oDesignador.dEnvergadura = eI.NewValues["Envergadura"].S().D();
                        oDesignador.dCuotaKilometro = eI.NewValues["CuotaPorKilometro"].S().D();
                        oDesignador.iStatus = 1;

                        break;
                    case Enumeraciones.TipoOperacion.Actualizar:
                        ASPxDataUpdatingEventArgs eU = (ASPxDataUpdatingEventArgs)oIView.oCrud;

                        oDesignador.iId = eU.Keys[0].S().I();
                        oDesignador.sDescripcion = eU.NewValues["Descripcion"].S();
                        oDesignador.sClave = eU.NewValues["Clave"].S();
                        oDesignador.dEnvergadura = eU.NewValues["Envergadura"].S().D();
                        oDesignador.dCuotaKilometro = eU.NewValues["CuotaPorKilometro"].S().D();
                        oDesignador.iStatus = eU.NewValues["Status"].S().I();

                        break;
                    case Enumeraciones.TipoOperacion.Validar:
                        ASPxDataValidationEventArgs eV = (ASPxDataValidationEventArgs)oIView.oCrud;

                        oDesignador.sDescripcion = eV.NewValues["Descripcion"].S();
                        oDesignador.sClave = eV.NewValues["Clave"].S();
                        oDesignador.dEnvergadura = eV.NewValues["Envergadura"].S().I();
                        oDesignador.dCuotaKilometro = eV.NewValues["CuotaPorKilometro"].I();
                        oDesignador.iStatus = eV.NewValues["Status"].S().I();

                        break;
                    case Enumeraciones.TipoOperacion.Eliminar:
                        ASPxDataDeletingEventArgs eD = (ASPxDataDeletingEventArgs)oIView.oCrud;
                        oDesignador.iId = eD.Keys[0].S().I();
                        oDesignador.sDescripcion = eD.Values["Descripcion"].S();
                        break;
                }

                return oDesignador;
            }
        }
        private bool bValidaActualizacion
        {
            get
            {
                bool bValida = true;
                ASPxDataValidationEventArgs eV = (ASPxDataValidationEventArgs)oIView.oCrud;

                bValida = (eV.NewValues["Descripcion"].S() != eV.OldValues["Descripcion"].S()) || (eV.NewValues["Clave"].S() != eV.OldValues["Clave"].S());

                return bValida;
            }

        }
    }
}