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
    public class Cliente_Presenter : BasePresenter<IViewCliente>
    {
        private readonly DBCliente oIGestCat;

        public Cliente_Presenter(IViewCliente oView, DBCliente oGC)
            : base(oView)
        {
            oIGestCat = oGC;
            oView.eGetTipCliente += eGetTipoCliente_Presenter;
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
                Cliente oCliente = oCatalogo;
                int id = oIGestCat.DBUpdate(oCliente);
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
                Cliente oCliente = oCatalogo;
                int id = oIGestCat.DBSave(oCliente);
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
                Cliente oCliente = oCatalogo;
                int id = oIGestCat.DBDelete(oCliente);
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

        protected void eGetTipoCliente_Presenter(object sender, EventArgs e)
        {
            oIView.LoadCatalogoTipocliente(oIGestCat.dtTipoCliente);
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
        

        private Cliente oCatalogo
        {
            get
            {
                Cliente oCliente = new Cliente();
                switch (oIView.eCrud)
                {
                    case Enumeraciones.TipoOperacion.Insertar:
                        ASPxDataInsertingEventArgs eI = (ASPxDataInsertingEventArgs)oIView.oCrud;
                        oCliente.iId = 0;
                        oCliente.sNombre = eI.NewValues["Nombre"].S();
                        oCliente.sCodigoCliente = eI.NewValues["CodigoCliente"].S();
                        oCliente.iStatus = eI.NewValues["Status"].S().I();
                        oCliente.sObservaciones=eI.NewValues["Observaciones"].S() ;
                        oCliente.sNotas = eI.NewValues["Notas"].S();
                        oCliente.sOtros = eI.NewValues["Otros"].S();
                        oCliente.iTipoCliente = eI.NewValues["TipoCliente"].S().I();

                        break;
                    case Enumeraciones.TipoOperacion.Validar:
                        ASPxDataValidationEventArgs eV = (ASPxDataValidationEventArgs)oIView.oCrud;
                        oCliente.iId = 0;
                        oCliente.sNombre = eV.NewValues["Nombre"].S();
                        oCliente.sCodigoCliente = eV.NewValues["CodigoCliente"].S();
                        oCliente.iStatus = eV.NewValues["Status"].S().I();
                        oCliente.sObservaciones = eV.NewValues["Observaciones"].S();
                        oCliente.sNotas = eV.NewValues["Notas"].S();
                        oCliente.sOtros = eV.NewValues["Otros"].S();
                        oCliente.iTipoCliente = eV.NewValues["TipoCliente"].S().I();

                        break;
                    case Enumeraciones.TipoOperacion.Actualizar:
                        ASPxDataUpdatingEventArgs eU = (ASPxDataUpdatingEventArgs)oIView.oCrud;

                        oCliente.iId = eU.Keys[0].S().I();
                        oCliente.sNombre = eU.NewValues["Nombre"].S();
                        oCliente.sCodigoCliente = eU.NewValues["CodigoCliente"].S();
                        oCliente.iStatus = eU.NewValues["Status"].S().I();
                        oCliente.sObservaciones = eU.NewValues["Observaciones"].S();
                        oCliente.sNotas = eU.NewValues["Notas"].S();
                        oCliente.sOtros = eU.NewValues["Otros"].S();
                        oCliente.iTipoCliente = eU.NewValues["TipoCliente"].S().I();

                        break;
                    case Enumeraciones.TipoOperacion.Eliminar:
                        ASPxDataDeletingEventArgs eD = (ASPxDataDeletingEventArgs)oIView.oCrud;
                        oCliente.iId = eD.Keys[0].S().I();
                        oCliente.sCodigoCliente = eD.Values["CodigoCliente"].S();
                        break;
                }

                return oCliente;
            }
        }

        private bool bValidaActualizacion
        {
            get
            {
                bool bValida = true;
                ASPxDataValidationEventArgs eU = (ASPxDataValidationEventArgs)oIView.oCrud;

                bValida = eU.NewValues["CodigoCliente"].S().ToUpper() != eU.OldValues["CodigoCliente"].S().ToUpper();

                return bValida;
            }
        }

    }
}