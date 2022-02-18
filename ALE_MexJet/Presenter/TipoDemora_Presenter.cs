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
    public class TipoDemora_Presenter: BasePresenter<IViewCat>
    {
        private readonly DBTipoDemora oIGestCat;

        public TipoDemora_Presenter(IViewCat oView, DBTipoDemora oGC)
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
                TipoDemora oTipoDemora = oCatalogo;
                int id = oIGestCat.DBUpdate(oTipoDemora);
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
                TipoDemora oTipoDemora = oCatalogo;
                int id = oIGestCat.DBSave(oTipoDemora);
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
                TipoDemora oTipoDemora = oCatalogo;
                int id = oIGestCat.DBDelete(oTipoDemora);
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


        private TipoDemora oCatalogo
        {
            get
            {
                TipoDemora oTipoDemora = new TipoDemora();
                switch (oIView.eCrud)
                {
                    case Enumeraciones.TipoOperacion.Insertar:
                        ASPxDataInsertingEventArgs eI = (ASPxDataInsertingEventArgs)oIView.oCrud;
                        oTipoDemora.iId = 0;
                        oTipoDemora.sDescripcion = eI.NewValues["TipoDemoraDescripcion"].S();
                        oTipoDemora.iStatus = 1;

                        break;
                    case Enumeraciones.TipoOperacion.Actualizar:
                        ASPxDataUpdatingEventArgs eU = (ASPxDataUpdatingEventArgs)oIView.oCrud;

                        oTipoDemora.iId = eU.Keys[0].S().I();
                        oTipoDemora.sDescripcion = eU.NewValues["TipoDemoraDescripcion"].S();
                        oTipoDemora.iStatus = eU.NewValues["Status"].S().I();

                        break;
                    case Enumeraciones.TipoOperacion.Validar:
                        ASPxDataValidationEventArgs eV = (ASPxDataValidationEventArgs)oIView.oCrud;

                        oTipoDemora.sDescripcion = eV.NewValues["TipoDemoraDescripcion"].S();
                        oTipoDemora.iStatus = eV.NewValues["Status"].S().I();
                        break;
                    case Enumeraciones.TipoOperacion.Eliminar:
                        ASPxDataDeletingEventArgs eD = (ASPxDataDeletingEventArgs)oIView.oCrud;
                        oTipoDemora.iId = eD.Keys[0].S().I();
                        oTipoDemora.sDescripcion = eD.Values["TipoDemoraDescripcion"].S();
                        break;
                }

                return oTipoDemora;
            }
        }
        private bool bValidaActualizacion
        {
            get
            {
                bool bValida = true;
                ASPxDataValidationEventArgs eU = (ASPxDataValidationEventArgs)oIView.oCrud;

                bValida = eU.NewValues["TipoDemoraDescripcion"].S().ToUpper() != eU.OldValues["TipoDemoraDescripcion"].S().ToUpper();

                return bValida;
            }

        }
    }
}