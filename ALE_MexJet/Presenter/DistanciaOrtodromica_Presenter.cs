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
    public class DistanciaOrtodromica_Presenter: BasePresenter<IViewDistanciaOrtodromica>
    {
        private readonly DBDistanciaOrtodromica oIGestCat;

        public DistanciaOrtodromica_Presenter(IViewDistanciaOrtodromica oView, DBDistanciaOrtodromica oGC)
            : base(oView)
        {
            oIGestCat = oGC;
            oView.eGetAeropuerto += eGetaeropuertos_Presenter;
            oIView.eGetAeropuertoFiltro += eGetaeropuertosFiltrados_Presenter;
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
                DistanciaOrtodromica oDistanciaOrtodromica = oCatalogo;
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
                DistanciaOrtodromica oDistanciaOrtodromica = oCatalogo;
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
                DistanciaOrtodromica oDistanciaOrtodromica = oCatalogo;
                int id = oIGestCat.DBDelete(oDistanciaOrtodromica);
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

        protected void eGetaeropuertos_Presenter(object sender, EventArgs e)
        {
            oIView.LoadCatalogoAeropuerto(oIGestCat.dtGetAeropuerto);
        }

        protected void eGetaeropuertosFiltrados_Presenter(object sender, EventArgs e)
        {
            oIView.LoadCatalogoAeropuerto(oIGestCat.DBFiltraAeropuertos(oIView.sFiltroAeropuerto));
        }


        protected override void ObjSelected_Presenter(object sender, EventArgs e)
        {
            int existe = 0;
            if (oIView.eCrud == Enumeraciones.TipoOperacion.Actualizar)
            {
            }
            else 
            {
                oIView.eCrud = Enumeraciones.TipoOperacion.Validar;
                existe = oIGestCat.DBValida(oCatalogo);
            }
            oIView.bDuplicado = existe > 0;
        }
        private DistanciaOrtodromica oCatalogo
        {
            get
            {
                DistanciaOrtodromica oDistanciaOrtodromica = new DistanciaOrtodromica();
                switch (oIView.eCrud)
                {
                    case Enumeraciones.TipoOperacion.Insertar:
                        ASPxDataInsertingEventArgs eI = (ASPxDataInsertingEventArgs)oIView.oCrud;
                        oDistanciaOrtodromica.iId = 0;
                        oDistanciaOrtodromica.dcDistancia = eI.NewValues["DistanciaKms"].S().D();
                        oDistanciaOrtodromica.iIdOrigen = eI.NewValues["IdOrigen"].S().I();
                        oDistanciaOrtodromica.iIdDestino = eI.NewValues["IdDestino"].S().I();
                        oDistanciaOrtodromica.iStatus = 1;

                        break;
                    case Enumeraciones.TipoOperacion.Actualizar:
                        ASPxDataUpdatingEventArgs eU = (ASPxDataUpdatingEventArgs)oIView.oCrud;

                        oDistanciaOrtodromica.iId = eU.Keys[0].S().I();
                        oDistanciaOrtodromica.dcDistancia = eU.NewValues["DistanciaKms"].S().D();
                        oDistanciaOrtodromica.iIdOrigen = eU.NewValues["IdOrigen"].S().I();
                        oDistanciaOrtodromica.iIdDestino = eU.NewValues["IdDestino"].S().I();
                        oDistanciaOrtodromica.iStatus = eU.NewValues["Status"].S().I();

                        break;
                    case Enumeraciones.TipoOperacion.Validar:
                        ASPxDataValidationEventArgs eV = (ASPxDataValidationEventArgs)oIView.oCrud;

                        oDistanciaOrtodromica.dcDistancia = eV.NewValues["DistanciaKms"].S().D();
                        oDistanciaOrtodromica.iIdOrigen = eV.NewValues["IdOrigen"].S().I();
                        oDistanciaOrtodromica.iIdDestino = eV.NewValues["IdDestino"].S().I();
                        oDistanciaOrtodromica.iStatus = eV.NewValues["Status"].S().I();

                        break;
                    case Enumeraciones.TipoOperacion.Eliminar:
                        ASPxDataDeletingEventArgs eD = (ASPxDataDeletingEventArgs)oIView.oCrud;
                        oDistanciaOrtodromica.iId = eD.Keys[0].S().I();
                        break;
                }

                return oDistanciaOrtodromica;
            }
        }
        private bool bValidaActualizacion
        {
            get
            {
                bool bValida = true;
                ASPxDataValidationEventArgs eU = (ASPxDataValidationEventArgs)oIView.oCrud;
                bValida = ((eU.NewValues["DistanciaKms"].S().I() != eU.OldValues["DistanciaKms"].S().I()) || (eU.NewValues["IdOrigen"].S().I() != eU.OldValues["IdOrigen"].S().I()) || (eU.NewValues["IdDestino"].S().I() != eU.OldValues["IdDestino"].S().I()));
                return bValida;
            }

        }
    }
}