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
    public class Piloto_Presenter : BasePresenter<IViewCat>
    {
        private readonly DBPiloto oIGestCat;

        public Piloto_Presenter(IViewCat oView, DBPiloto oGC)
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
                    oIView.ObtieneValores();
                    oIView.MostrarMensaje(Mensajes.LMensajes[Enumeraciones.Mensajes.Eliminacion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
                }
            }
            catch (Exception ex)
            {
                oIView.MostrarMensaje("Se detectó el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
            }
        }

        private Piloto oCatalogo
        {
            get
            {
                Piloto oPiloto = new Piloto();
                switch (oIView.eCrud)
                {
                    case Enumeraciones.TipoOperacion.Insertar:
                        ASPxDataInsertingEventArgs eI = (ASPxDataInsertingEventArgs)oIView.oCrud;
                        oPiloto.iIdPiloto = 0;
                        oPiloto.sCrewCode = eI.NewValues["CREWCODE"].S();
                        oPiloto.sPilotoNombre = eI.NewValues["PilotoNombre"].S();
                        oPiloto.sPilotoApPaterno = eI.NewValues["PilotoApPat"].S();
                        oPiloto.sPilotoApMaterno = eI.NewValues["PilotoApMat"].S();
                        oPiloto.iStatus = 1;

                        break;
                    case Enumeraciones.TipoOperacion.Actualizar:
                        ASPxDataUpdatingEventArgs eU = (ASPxDataUpdatingEventArgs)oIView.oCrud;

                        oPiloto.iIdPiloto = eU.Keys[0].S().I();
                        oPiloto.sCrewCode = eU.NewValues["CREWCODE"].S();
                        oPiloto.sPilotoNombre = eU.NewValues["PilotoNombre"].S();
                        oPiloto.sPilotoApPaterno = eU.NewValues["PilotoApPat"].S();
                        oPiloto.sPilotoApMaterno = eU.NewValues["PilotoApMat"].S();
                        oPiloto.iStatus = eU.NewValues["Status"].S().I();

                        break;
                    case Enumeraciones.TipoOperacion.Validar:
                        ASPxDataValidationEventArgs eV = (ASPxDataValidationEventArgs)oIView.oCrud;

                        oPiloto.sCrewCode = eV.NewValues["CREWCODE"].S();
                        oPiloto.sPilotoNombre = eV.NewValues["PilotoNombre"].S();
                        oPiloto.sPilotoApPaterno = eV.NewValues["PilotoApPat"].S();
                        oPiloto.sPilotoApMaterno = eV.NewValues["PilotoApMat"].S();
                        oPiloto.iStatus = eV.NewValues["Status"].S().I();
                        break;
                    case Enumeraciones.TipoOperacion.Eliminar:
                        ASPxDataDeletingEventArgs eD = (ASPxDataDeletingEventArgs)oIView.oCrud;
                        oPiloto.iIdPiloto = eD.Keys[0].S().I();
                        break;
                }

                return oPiloto;
            }
        }
        private bool bValidaActualizacion
        {
            get
            {
                bool bValida = true;
                ASPxDataValidationEventArgs eU = (ASPxDataValidationEventArgs)oIView.oCrud;
                bValida = ((eU.NewValues["CREWCODE"].S().I() != eU.OldValues["CREWCODE"].S().I()) || (eU.NewValues["PilotoNombre"].S().I() != eU.OldValues["PilotoNombre"].S().I()));
                return bValida;
            }

        }
    }
}