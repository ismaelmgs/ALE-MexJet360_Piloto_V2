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
    public class TipoFactura_Presenter: BasePresenter<IViewCat>
    {
        private readonly DBTipoFactura oIGestCat;

        public TipoFactura_Presenter(IViewCat oView, DBTipoFactura oGC)
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
                TipoFactura oTipoFactura = oCatalogo;
                int id = oIGestCat.DBUpdate(oTipoFactura);
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
                TipoFactura oTipoFactura = oCatalogo;
                int id = oIGestCat.DBSave(oTipoFactura);
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
                TipoFactura oTipoFactura = oCatalogo;
                int id = oIGestCat.DBDelete(oTipoFactura);
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

        private TipoFactura oCatalogo
        {
            get
            {
                TipoFactura oTipoFactura = new TipoFactura();
                switch (oIView.eCrud)
                {
                    case Enumeraciones.TipoOperacion.Insertar:
                        ASPxDataInsertingEventArgs eI = (ASPxDataInsertingEventArgs)oIView.oCrud;
                        oTipoFactura.iId = 0;
                        oTipoFactura.sTipoFactura = eI.NewValues["TipoFactura"].S();
                        oTipoFactura.sDescripcion = eI.NewValues["Descripcion"].S();
                        oTipoFactura.bBloqueaCampos = eI.NewValues["BloqueaCampos"].S().B();
                        oTipoFactura.bDisponible = eI.NewValues["Disponible"].S().B();
                        oTipoFactura.bRequiererePrefactura = eI.NewValues["RequierePrefactura"].S().B();
                        oTipoFactura.bApareseTabulador = eI.NewValues["ApareceTabulador"].S().B();
                        oTipoFactura.bApareseEstadoCuenta = eI.NewValues["ApareceEstadoCuenta"].S().B();
                        oTipoFactura.iStatus = 1;

                        break;
                    case Enumeraciones.TipoOperacion.Actualizar:
                        ASPxDataUpdatingEventArgs eU = (ASPxDataUpdatingEventArgs)oIView.oCrud;

                        oTipoFactura.iId = eU.Keys[0].S().I();
                        oTipoFactura.sTipoFactura = eU.NewValues["TipoFactura"].S();
                        oTipoFactura.sDescripcion = eU.NewValues["Descripcion"].S();
                        oTipoFactura.bBloqueaCampos = eU.NewValues["BloqueaCampos"].S().B();
                        oTipoFactura.bDisponible = eU.NewValues["Disponible"].S().B();
                        oTipoFactura.bRequiererePrefactura = eU.NewValues["RequierePrefactura"].S().B();
                        oTipoFactura.bApareseTabulador = eU.NewValues["ApareceTabulador"].S().B();
                        oTipoFactura.bApareseEstadoCuenta = eU.NewValues["ApareceEstadoCuenta"].S().B();
                        oTipoFactura.iStatus = eU.NewValues["Status"].S().I();

                        break;
                    case Enumeraciones.TipoOperacion.Validar:
                        ASPxDataValidationEventArgs eV = (ASPxDataValidationEventArgs)oIView.oCrud;
                        oTipoFactura.sTipoFactura = eV.NewValues["TipoFactura"].S();
                        oTipoFactura.sDescripcion = eV.NewValues["Descripcion"].S();
                        oTipoFactura.bBloqueaCampos = eV.NewValues["BloqueaCampos"].S().B();
                        oTipoFactura.bDisponible = eV.NewValues["Disponible"].S().B();
                        oTipoFactura.bRequiererePrefactura = eV.NewValues["RequierePrefactura"].S().B();
                        oTipoFactura.bApareseTabulador = eV.NewValues["ApareceTabulador"].S().B();
                        oTipoFactura.bApareseEstadoCuenta = eV.NewValues["ApareceEstadoCuenta"].S().B();
                        oTipoFactura.iStatus = eV.NewValues["Status"].S().I();

                        break;


                    case Enumeraciones.TipoOperacion.Eliminar:
                        ASPxDataDeletingEventArgs eD = (ASPxDataDeletingEventArgs)oIView.oCrud;
                        oTipoFactura.iId = eD.Keys[0].S().I();
                        oTipoFactura.sDescripcion = eD.Values["Descripcion"].S();
                        break;
                }

                return oTipoFactura;
            }
        }

        private bool bValidaActualizacion
        {
            get
            {
                bool bValida = true;
                ASPxDataValidationEventArgs eV = (ASPxDataValidationEventArgs)oIView.oCrud;

                bValida = ((eV.NewValues["TipoFactura"].S() != eV.NewValues["TipoFactura"].S()) || (eV.NewValues["Descripcion"].S() != eV.NewValues["Descripcion"].S()));

                return bValida;
            }


        }
    }
}