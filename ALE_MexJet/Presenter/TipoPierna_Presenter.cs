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
    public class TipoPierna_Presenter : BasePresenter<iViewTipoPierna>
    {
        private readonly DBTipoPierna oIGestCat;

        public TipoPierna_Presenter(iViewTipoPierna oView, DBTipoPierna oGC)
            : base(oView)
        {
            oIGestCat = oGC;
            oIView.eGetPaquetes += GetPaquete_Presenter;
        }

        public void LoadObjects_Presenter()
        {
            oIView.LoadObjects(oIGestCat.DBSearchObj(oIView.oArrFiltros));
        }

        public void GetPaquete_Presenter(object sender, EventArgs e)
        {
            try
            {
                oIView.dtPaquetesActivos = oIGestCat.dtObjCatPaquete;
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }
        protected override void SearchObj_Presenter(object sender, EventArgs e)
        {
            oIView.LoadObjects(oIGestCat.DBSearchObj(oIView.oArrFiltros));
        }

        protected override void SaveObj_Presenter(object sender, EventArgs e)
        {
            try
            {
                TipoPierna oTipoPierna = oCatalogo;
                int id = oIGestCat.DBUpdate(oTipoPierna);
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
                TipoPierna oTipoPierna = oCatalogo;
                int id = oIGestCat.DBSave(oTipoPierna);
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
                TipoPierna oTipoPierna = oCatalogo;
                int id = oIGestCat.DBDelete(oTipoPierna);
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


        private TipoPierna oCatalogo
        {
            get
            {
                TipoPierna oTipoPierna = new TipoPierna();
                switch (oIView.eCrud)
                {
                    case Enumeraciones.TipoOperacion.Insertar:
                        ASPxDataInsertingEventArgs eI = (ASPxDataInsertingEventArgs)oIView.oCrud;
                        oTipoPierna.iId = 0;
                        oTipoPierna.sDescripcion = eI.NewValues["TipoPiernaDescripcion"].S();
                        oTipoPierna.iIdPaquete = eI.NewValues["IdTipoPaquete"].S().I();
                        oTipoPierna.iStatus = 1;

                        break;
                    case Enumeraciones.TipoOperacion.Actualizar:
                        ASPxDataUpdatingEventArgs eU = (ASPxDataUpdatingEventArgs)oIView.oCrud;

                        oTipoPierna.iId = eU.Keys[0].S().I();
                        oTipoPierna.sDescripcion = eU.NewValues["TipoPiernaDescripcion"].S();
                        oTipoPierna.iIdPaquete = eU.NewValues["IdTipoPaquete"].S().I();
                        oTipoPierna.iStatus = eU.NewValues["Status"].S().I();

                        break;
                    case Enumeraciones.TipoOperacion.Validar:
                        ASPxDataValidationEventArgs eV = (ASPxDataValidationEventArgs)oIView.oCrud;

                        oTipoPierna.sDescripcion = eV.NewValues["TipoPiernaDescripcion"].S();
                        oTipoPierna.iIdPaquete = eV.NewValues["IdTipoPaquete"].S().I();
                        oTipoPierna.iStatus = eV.NewValues["Status"].S().I();
                        break;
                    case Enumeraciones.TipoOperacion.Eliminar:
                        ASPxDataDeletingEventArgs eD = (ASPxDataDeletingEventArgs)oIView.oCrud;
                        oTipoPierna.iId = eD.Keys[0].S().I();
                        oTipoPierna.sDescripcion = eD.Values["TipoPiernaDescripcion"].S();
                        break;
                }

                return oTipoPierna;
            }
        }
        private bool bValidaActualizacion
        {
            get
            {
                bool bValida = true;
                ASPxDataValidationEventArgs eU = (ASPxDataValidationEventArgs)oIView.oCrud;

                bValida = eU.NewValues["TipoPiernaDescripcion"].S().ToUpper() != eU.OldValues["TipoPiernaDescripcion"].S().ToUpper();

                return bValida;
            }


        }
    }
}