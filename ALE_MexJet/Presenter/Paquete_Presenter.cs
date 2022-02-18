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
using System.Data;

namespace ALE_MexJet.Presenter
{
    public class Paquete_Presenter : BasePresenter<IViewPaquete>
    {
        private readonly DBPaquete oIGestCat;
        private readonly DBPaqueteCuenta oIGestCatCue;

        public Paquete_Presenter(IViewPaquete oView, DBPaquete oGC, DBPaqueteCuenta oGCCue)
            : base(oView)
        {
            oIGestCat = oGC;

            oIGestCatCue = oGCCue;
            oView.eSearchObjCue += SearchObj_PresenterCue;
            oView.eSaveObjCue += SaveObj_PresenterCue;
            oView.eNewObjCue += NewObj_PresenterCue;
            oView.eObjSelectedCue += ObjSelectedCue_Presenter;
            oView.eDeleteObjCue += DeleteObj_PresenterCue;
            oIView.eGetCuentas += GetCuentas_Presenter;
            oIView.eGetProyectoSAP += eGetProyectoSAP_Presenter;
        }


        public void LoadObjects_Presenter()
        {
            oIView.LoadObjects(oIGestCat.DBSearchObj(oIView.oArrFiltros));
        }

        protected void GetCuentas_Presenter(object sender, EventArgs e)
        {
            oIView.dtCuentas = oIGestCatCue.dtCuenta;
        }
        protected override void SearchObj_Presenter(object sender, EventArgs e)
        {
            oIView.LoadObjects(oIGestCat.DBSearchObj(oIView.oArrFiltros));
        }
        protected void SearchObj_PresenterCue(object sender, EventArgs e)
        {
            oIView.LoadObjectsCuentas(oIGestCatCue.DBSearchObj(oIView.oArrFiltrosCue));
        }

        protected override void SaveObj_Presenter(object sender, EventArgs e)
        {
            try
            {
                int id = oIGestCat.DBUpdate(oCatalogo);
                if (id > 0)
                {
                    oIView.ObtieneValoresPaquete();
                    oIView.MostrarMensaje(Mensajes.LMensajes[Enumeraciones.Mensajes.Modificacion._I()].sMensaje,
                                            Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);

                }
            }
            catch (Exception ex)
            {
                oIView.MostrarMensaje("Se detecto el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
            }
        }
        protected void SaveObj_PresenterCue(object sender, EventArgs e)
        {
            try
            {
                int id = oIGestCatCue.DBUpdate(oCatalogoCuenta);
                if (id > 0)
                {
                    oIView.ObtieneValoresCuentas();
                    oIView.MostrarMensajeCuenta(Mensajes.LMensajes[Enumeraciones.Mensajes.Modificacion._I()].sMensaje,
                                            Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);

                }
            }
            catch (Exception ex)
            {
                oIView.MostrarMensajeCuenta("Se detecto el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
            }
        }
        protected override void NewObj_Presenter(object sender, EventArgs e)
        {
            try
            {
                int id = oIGestCat.DBSave(oCatalogo);
                if (id > 0)
                {
                    oIView.MostrarMensaje(Mensajes.LMensajes[Enumeraciones.Mensajes.Insercion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
                    oIView.ObtieneValoresPaquete();
                }
            }
            catch (Exception ex)
            {
                oIView.MostrarMensaje("Se detecto el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
            }
        }
        protected void NewObj_PresenterCue(object sender, EventArgs e)
        {
            try
            {
                int id = oIGestCatCue.DBSave(oCatalogoCuenta);
                if (id > 0)
                {
                    oIView.MostrarMensajeCuenta(Mensajes.LMensajes[Enumeraciones.Mensajes.Insercion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
                    oIView.ObtieneValoresCuentas();
                }
            }
            catch (Exception ex)
            {
                oIView.MostrarMensajeCuenta("Se detecto el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
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
                    oIView.ObtieneValoresPaquete();
                }
            }
            catch (Exception ex)
            {
                oIView.MostrarMensaje("Se detecto el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
            }
        }
        protected void DeleteObj_PresenterCue(object sender, EventArgs e)
        {
            try
            {
                int id = oIGestCatCue.DBDelete(oCatalogoCuenta);

                if (id > 0)
                {
                    oIView.MostrarMensajeCuenta(Mensajes.LMensajes[Enumeraciones.Mensajes.Eliminacion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
                    oIView.ObtieneValoresCuentas();
                }
            }
            catch (Exception ex)
            {
                oIView.MostrarMensaje("Se detecto el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
            }
        }
        protected override void ObjSelected_Presenter(object sender, EventArgs e)
        {
            int existe = 0;
            if (oIView.eCrud == Enumeraciones.TipoOperacion.Actualizar)
            {
                if (bValidaActualizacionCue)
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
        protected void ObjSelectedCue_Presenter(object sender, EventArgs e)
        {
            int existe = 0;
            if (oIView.eCrud == Enumeraciones.TipoOperacion.Actualizar)
            {
                if (bValidaActualizacionCue)
                {
                    oIView.eCrud = Enumeraciones.TipoOperacion.Validar;
                    existe = oIGestCatCue.DBValida(oCatalogoCuenta);
                }
            }
            else
            {
                oIView.eCrud = Enumeraciones.TipoOperacion.Validar;
                existe = oIGestCatCue.DBValida(oCatalogoCuenta);
            }
            oIView.bDuplicado = existe > 0;
        }
        protected void eGetProyectoSAP_Presenter(object sender, EventArgs e)
        {
            oIView.dtProyectoSAP = new DBSAP().DBGetProyectos;
        }
        private Paquete oCatalogo
        {
            get
            {
                Paquete oPaquete = new Paquete();
                switch (oIView.eCrud)
                {
                    case Enumeraciones.TipoOperacion.Insertar:
                        ASPxDataInsertingEventArgs eI = (ASPxDataInsertingEventArgs)oIView.oCrud;
                        oPaquete.sDescripcion = eI.NewValues["Descripcion"].S();
                        oPaquete.iMexJet = eI.NewValues["MexJet"].S().I();
                        oPaquete.iEzMexJet = eI.NewValues["EzMexJet"].S().I();
                        oPaquete.sProyectoSAP = eI.NewValues["ProyectoSAP"].S();

                        DataRow[] drResultsI = oIView.dtProyectoSAP.Select("PrcCode = '" + oPaquete.sProyectoSAP + "'");
                        oPaquete.sDescProyectoSAP = drResultsI[0]["PrcName"].S();

                        oPaquete.iStatus = 1;
                        break;
                    case Enumeraciones.TipoOperacion.Actualizar:
                        ASPxDataUpdatingEventArgs eU = (ASPxDataUpdatingEventArgs)oIView.oCrud;
                        oPaquete.iIdTipoPaquete = eU.Keys[0].S().I();
                        oPaquete.sDescripcion = eU.NewValues["Descripcion"].S();
                        oPaquete.iMexJet = eU.NewValues["MexJet"].S().I();
                        oPaquete.iEzMexJet = eU.NewValues["EzMexJet"].S().I();
                        oPaquete.sProyectoSAP = eU.NewValues["ProyectoSAP"].S();

                        drResultsI = oIView.dtProyectoSAP.Select("PrcCode = '" + oPaquete.sProyectoSAP + "'");
                        oPaquete.sDescProyectoSAP = drResultsI[0]["PrcName"].S();

                        oPaquete.iStatus = eU.NewValues["Status"].S().I();
                        break;
                    case Enumeraciones.TipoOperacion.Validar:
                        ASPxDataValidationEventArgs eV = (ASPxDataValidationEventArgs)oIView.oCrud;
                        oPaquete.sDescripcion = eV.NewValues["Descripcion"].S();
                        break;
                    case Enumeraciones.TipoOperacion.Eliminar:
                        ASPxDataDeletingEventArgs eD = (ASPxDataDeletingEventArgs)oIView.oCrud;
                        oPaquete.iIdTipoPaquete = eD.Values["IdTipoPaquete"].S().I();
                        break;
                }

                return oPaquete;
            }
        }
        private PaqueteCuenta oCatalogoCuenta
        {
            get
            {
                PaqueteCuenta oPaqueteCuenta = new PaqueteCuenta();
                switch (oIView.eCrud)
                {
                    case Enumeraciones.TipoOperacion.Insertar:
                        ASPxDataInsertingEventArgs eI = (ASPxDataInsertingEventArgs)oIView.oCrud;
                        int iIdTipoPaqueteI = (int)oIView.iIdTipoPaquete;
                        oPaqueteCuenta.iIdTipoPaquete = iIdTipoPaqueteI;
                        oPaqueteCuenta.sClaveCuenta = eI.NewValues["ClaveCuenta"].S();
                        DataRow[] drResultsI = oIView.dtCuentas.Select("acct = " + oPaqueteCuenta.sClaveCuenta);
                        oPaqueteCuenta.sDescripcion = drResultsI[0].ItemArray[2].S();
                        
                        oPaqueteCuenta.iStatus = 1;
                        break;
                    case Enumeraciones.TipoOperacion.Actualizar:
                        ASPxDataUpdatingEventArgs eU = (ASPxDataUpdatingEventArgs)oIView.oCrud;
                        oPaqueteCuenta.iIdTipoPaqueteCuenta = eU.Keys["IdTipoPaqueteCuenta"].S().I();  
                        //oPaqueteCuenta.sDescripcion = eU.NewValues["Descripcion"].S();
                        oPaqueteCuenta.sClaveCuenta = eU.NewValues["ClaveCuenta"].S();
                        DataRow[] drResults = oIView.dtCuentas.Select("acct = " + oPaqueteCuenta.sClaveCuenta);
                        oPaqueteCuenta.sDescripcion = drResults[0].ItemArray[2].S(); 
                        oPaqueteCuenta.iStatus = eU.NewValues["Status"].S().I();
                        break;
                    case Enumeraciones.TipoOperacion.Validar:
                        ASPxDataValidationEventArgs eV = (ASPxDataValidationEventArgs)oIView.oCrud;
                        int iIdTipoPaqueteV = (int)oIView.iIdTipoPaquete;
                        oPaqueteCuenta.iIdTipoPaquete = iIdTipoPaqueteV;
                        oPaqueteCuenta.sClaveCuenta = eV.NewValues["ClaveCuenta"].S();
                        DataRow[] drResultsV = oIView.dtCuentas.Select("acct = " + oPaqueteCuenta.sClaveCuenta);
                        oPaqueteCuenta.sDescripcion = drResultsV[0].ItemArray[2].S();
                        oPaqueteCuenta.iStatus = eV.NewValues["Status"].S().I();
                        break;
                    case Enumeraciones.TipoOperacion.Eliminar:
                        ASPxDataDeletingEventArgs eD = (ASPxDataDeletingEventArgs)oIView.oCrud;
                        oPaqueteCuenta.iIdTipoPaqueteCuenta = eD.Values["IdTipoPaqueteCuenta"].S().I();
                        break;
                }
                return oPaqueteCuenta;
            }
        }
        private bool bValidaActualizacionCue
        {
            get
            {
                bool bValida = true;
                ASPxDataValidationEventArgs eU = (ASPxDataValidationEventArgs)oIView.oCrud;
                bValida = eU.NewValues["Descripcion"].S().ToUpper() != eU.OldValues["Descripcion"].S().ToUpper();
                return bValida;
            }

        }
        private bool bValidaActualizacion
        {
            get
            {
                bool bValida = true;
                ASPxDataValidationEventArgs eU = (ASPxDataValidationEventArgs)oIView.oCrud;
                bValida = eU.NewValues["Descripcion"].S().ToUpper() != eU.OldValues["Descripcion"].S().ToUpper();
                return bValida;
            }

        }

    }
}