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
    public class Area_Presenter: BasePresenter<IViewCat>
    {
        private readonly DBArea oIGestCat;

        public Area_Presenter(IViewCat oView, DBArea oGC)
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
                Area oArea = oCatalogo;
                int id = oIGestCat.DBUpdate(oArea);
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
                Area oArea = oCatalogo;
                int id = oIGestCat.DBSave(oArea);
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
                Area oArea = oCatalogo;
                int id = oIGestCat.DBDelete(oArea);
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

        private Area oCatalogo
        {
            get
            {
                Area oArea = new Area();
                switch (oIView.eCrud)
                {
                    case Enumeraciones.TipoOperacion.Insertar:
                        ASPxDataInsertingEventArgs eI = (ASPxDataInsertingEventArgs)oIView.oCrud;
                        oArea.iId = 0;
                        oArea.sDescripcion = eI.NewValues["AreaDescripcion"].S();
                        oArea.sAreaEmail = eI.NewValues["AreaEmail"].S();
                        oArea.sAreaEmailCC = eI.NewValues["AreaEmailCC"].S();
                        oArea.iStatus = 1;

                        break;
                    case Enumeraciones.TipoOperacion.Actualizar:
                        ASPxDataUpdatingEventArgs eU = (ASPxDataUpdatingEventArgs)oIView.oCrud;

                        oArea.iId = eU.Keys[0].S().I();
                        oArea.sDescripcion = eU.NewValues["AreaDescripcion"].S();
                        oArea.sAreaEmail = eU.NewValues["AreaEmail"].S();
                        oArea.sAreaEmailCC = eU.NewValues["AreaEmailCC"].S();
                        oArea.iStatus = eU.NewValues["Status"].S().I();

                        break;
                    case Enumeraciones.TipoOperacion.Validar:
                        ASPxDataValidationEventArgs eV = (ASPxDataValidationEventArgs)oIView.oCrud;
                        oArea.sDescripcion = eV.NewValues["AreaDescripcion"].S();
                        oArea.sAreaEmail = eV.NewValues["AreaEmail"].S();
                        oArea.sAreaEmailCC = eV.NewValues["AreaEmailCC"].S();
                        oArea.iStatus = eV.NewValues["Status"].S().I();

                        break;
                    case Enumeraciones.TipoOperacion.Eliminar:
                        ASPxDataDeletingEventArgs eD = (ASPxDataDeletingEventArgs)oIView.oCrud;
                        oArea.iId = eD.Keys[0].S().I();
                        oArea.sDescripcion = eD.Values["AreaDescripcion"].S();
                        break;
                }

                return oArea;
            }
        }
        private bool bValidaActualizacion
        {
            get
            {
                bool bValida = true;
                ASPxDataValidationEventArgs eU = (ASPxDataValidationEventArgs)oIView.oCrud;

                bValida = eU.NewValues["AreaDescripcion"].S().ToUpper() != eU.OldValues["AreaDescripcion"].S().ToUpper();

                return bValida;
            }


        }


    }
}