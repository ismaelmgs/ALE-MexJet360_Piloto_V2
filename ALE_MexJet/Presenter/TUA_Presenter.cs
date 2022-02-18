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
    public class TUA_Presenter : BasePresenter<IViewTUA>
    {
        private readonly DBTUA oIGestCat;

        public TUA_Presenter(IViewTUA oView, DBTUA oGC)
            : base(oView)
        {
            oIGestCat = oGC;
            oView.eGetAeropuerto += eGetAeropuerto_Presenter;
            oView.eSearchObjMes += eGetMes_Presenter;
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
        protected void eGetMes_Presenter(object sender, EventArgs e)
        {
            oIView.LoadObjectsMes(oIGestCat.DBSearchObjMes(oIView.oArrFiltrosMes));
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
                    oIView.MostrarMensaje(Mensajes.LMensajes[Enumeraciones.Mensajes.Eliminacion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
                    oIView.ObtieneValores();
                }
            }
            catch (Exception ex)
            {
                oIView.MostrarMensaje("Se detectó el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
            }
        }
        protected void eGetAeropuerto_Presenter(object sender, EventArgs e)
        {
            oIView.LoadObjectsAereo(oIGestCat.DBSearchObjAero(oIView.oArrFiltrosAereo));
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

        private TUA oCatalogo
        {
            get
            {
                TUA oTUA = new TUA();
                switch (oIView.eCrud)
                {
                    case Enumeraciones.TipoOperacion.Insertar:
                        ASPxDataInsertingEventArgs eI = (ASPxDataInsertingEventArgs)oIView.oCrud;
                        oTUA.iIdAeropuerto = eI.NewValues["IdAeropuerto"].S().I();
                        oTUA.iIdMes = eI.NewValues["IdMes"].S().I();
                        oTUA.iAnio = eI.NewValues["Anio"].S().I();
                        oTUA.dNacional = eI.NewValues["Nacional"].S().D();
                        oTUA.dInternacional = eI.NewValues["Internacional"].S().D();
                        oTUA.iStatus = 1;
                        break;
                    case Enumeraciones.TipoOperacion.Actualizar:
                        ASPxDataUpdatingEventArgs eU = (ASPxDataUpdatingEventArgs)oIView.oCrud;

                        oTUA.iIdTUA = eU.Keys["IdTUA"].S().I();
                        oTUA.iIdAeropuerto = eU.NewValues["IdAeropuerto"].S().I();
                        oTUA.iIdMes = eU.NewValues["IdMes"].S().I();
                        oTUA.iAnio = eU.NewValues["Anio"].S().I();
                        oTUA.dNacional = eU.NewValues["Nacional"].S().D();
                        oTUA.dInternacional = eU.NewValues["Internacional"].S().D();
                        oTUA.iStatus = 1;
                        break;
                    case Enumeraciones.TipoOperacion.Eliminar:
                        ASPxDataDeletingEventArgs eD = (ASPxDataDeletingEventArgs)oIView.oCrud;
                        oTUA.iIdTUA = eD.Keys["IdTUA"].S().I();
                        break;

                    case Enumeraciones.TipoOperacion.Validar:
                        ASPxDataValidationEventArgs eV = (ASPxDataValidationEventArgs)oIView.oCrud;

                        oTUA.iIdAeropuerto = eV.NewValues["IdAeropuerto"].S().I();
                        oTUA.iIdMes = eV.NewValues["IdMes"].S().I();
                        oTUA.iAnio = eV.NewValues["Anio"].S().I();
                        break;
                }

                return oTUA;
            }
        }
        protected void eGetaeropuertosFiltrados_Presenter(object sender, EventArgs e)
        {
            oIView.LoadCatalogoAeropuerto(oIGestCat.DBFiltraAeropuertos(oIView.sFiltroAeropuerto));
        }

        private bool bValidaActualizacion
        {
            get
            {
                bool bValida = true;
                ASPxDataValidationEventArgs eU = (ASPxDataValidationEventArgs)oIView.oCrud;

                bValida = (eU.NewValues["IdAeropuerto"].S().ToUpper() != eU.OldValues["IdAeropuerto"].S().ToUpper() ||
                            eU.NewValues["Mes"].S().ToUpper() != eU.OldValues["Mes"].S().ToUpper() ||
                            eU.NewValues["Anio"].S().ToUpper() != eU.OldValues["Anio"].S().ToUpper() //||
                    //eU.NewValues["Nacional"].S().ToUpper() != eU.OldValues["Nacional"].S().ToUpper() ||
                    //eU.NewValues["Internacional"].S().ToUpper() != eU.OldValues["Internacional"].S().ToUpper() ||
                    //eU.NewValues["Status"].S().ToUpper() != eU.OldValues["Status"].S().ToUpper()

                            );

                return bValida;
            }


        }
    }
}