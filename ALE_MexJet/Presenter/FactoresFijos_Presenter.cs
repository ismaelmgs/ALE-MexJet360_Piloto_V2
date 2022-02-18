using ALE_MexJet.Clases;
using ALE_MexJet.DomainModel;
using ALE_MexJet.Interfaces;
using ALE_MexJet.Objetos;
using DevExpress.Web.Data;
using NucleoBase.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ALE_MexJet.Presenter
{
    public class FactoresFijos_Presenter : BasePresenter<IViewFactoresFijos>
    {
        private readonly DBFactoresFijos oIGestCat;

        public FactoresFijos_Presenter(IViewFactoresFijos oView, DBFactoresFijos oGC)
            : base(oView)
        {
            oIGestCat = oGC;

        }

        protected override void SearchObj_Presenter(object sender, EventArgs e)
        {
            oIView.LoadObjects(oIGestCat.DBSearchObj(oIView.oArrFiltros));
        }

        protected override void NewObj_Presenter(object sender, EventArgs e)
        {
            try
            {
                FactorFijo oFactor = oCatalogo;
                int id = oIGestCat.DBSave(oFactor);
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

        protected override void SaveObj_Presenter(object sender, EventArgs e)
        {
            try
            {
                FactorFijo oFactor = oCatalogo;
                int id = oIGestCat.DBUpdate(oFactor);
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

        protected override void DeleteObj_Presenter(object sender, EventArgs e)
        {
            try
            {
                FactorFijo oFactor = oCatalogo;
                int id = oIGestCat.DBDelete(oFactor);
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


        private FactorFijo oCatalogo
        {
            get
            {
                FactorFijo oFactor = new FactorFijo();
                switch (oIView.eCrud)
                {
                    case Enumeraciones.TipoOperacion.Insertar:
                        ASPxDataInsertingEventArgs eI = (ASPxDataInsertingEventArgs)oIView.oCrud;
                        oFactor.iId = 0;
                        oFactor.sClave = eI.NewValues["Clave"].S();
                        oFactor.sDescripcion = eI.NewValues["Descripcion"].S();
                        oFactor.dValor = eI.NewValues["Valor"].S().D();
                        oFactor.iStatus = 1;

                        break;
                    case Enumeraciones.TipoOperacion.Actualizar:
                        ASPxDataUpdatingEventArgs eU = (ASPxDataUpdatingEventArgs)oIView.oCrud;
                        oFactor.iId = eU.Keys[0].S().I();
                        oFactor.sClave = eU.NewValues["Clave"].S();
                        oFactor.sDescripcion = eU.NewValues["Descripcion"].S();
                        oFactor.dValor = eU.NewValues["Valor"].S().D();
                        oFactor.iStatus = eU.NewValues["Sts"].S().I();

                        break;
                    case Enumeraciones.TipoOperacion.Validar:
                        ASPxDataValidationEventArgs eV = (ASPxDataValidationEventArgs)oIView.oCrud;
                        oFactor.sClave = eV.NewValues["Clave"].S();
                        oFactor.sDescripcion = eV.NewValues["Descripcion"].S();
                        oFactor.dValor = eV.NewValues["Valor"].S().D();
                        oFactor.iStatus = eV.NewValues["Sts"].S().I();

                        break;
                    case Enumeraciones.TipoOperacion.Eliminar:
                        ASPxDataDeletingEventArgs eD = (ASPxDataDeletingEventArgs)oIView.oCrud;
                        oFactor.iId = eD.Keys[0].S().I();
                        oFactor.sClave = eD.Values["Clave"].S();
                        oFactor.sDescripcion = eD.Values["Descripcion"].S();
                        oFactor.dValor = eD.Values["Valor"].S().D();
                        break;
                }

                return oFactor;
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