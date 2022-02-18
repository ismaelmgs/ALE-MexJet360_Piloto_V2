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
    public class Vendedor_Presenter : BasePresenter<IViewVendedor>
    {
        private readonly DBVendedor oIGestCat;

        public Vendedor_Presenter(IViewVendedor oView, DBVendedor oGC)
            : base(oView)
        {
            oIGestCat = oGC;
            oView.eSearchObjBase += SearchObj_PresenterBase;
            oView.eSearchObjUni4 += SearchObj_PresenterUnidad4;
            oView.eSearchObjUni1V += SearchObj_PresenterUnidad1V;
        }

        public void LoadObjects_Presenter()
        {
            oIView.LoadObjects(oIGestCat.DBSearchObj(oIView.oArrFiltros));
        }
        public void LoadObjects_PresenterBase()
        {
            oIView.LoadObjectsBase(oIGestCat.DBSearchObjBase());
        }
       
        protected override void SearchObj_Presenter(object sender, EventArgs e)
        {
            oIView.LoadObjects(oIGestCat.DBSearchObj(oIView.oArrFiltros));
        }
        protected void SearchObj_PresenterUnidad4(object sender, EventArgs e)
        {
            oIView.LoadObjectsUnidad4(oIGestCat.DBGetCodUnidad4());
        }
        protected void SearchObj_PresenterUnidad1V(object sender, EventArgs e)
        {
            oIView.LoadObjectsUnidad1V(oIGestCat.DBGetCodUnidad1V());
        }
        protected void SearchObj_PresenterBase(object sender, EventArgs e)
        {
            oIView.LoadObjectsBase(oIGestCat.DBSearchObjBase());
        }
        protected override void SaveObj_Presenter(object sender, EventArgs e)
        {
            try
            {
                Vendedor oVendedor = oCatalogo;
                int id = oIGestCat.DBUpdate(oVendedor);
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
                Vendedor oVendedor = oCatalogo;
                int id = oIGestCat.DBSave(oVendedor);
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
                Vendedor oVendedor = oCatalogo;
                int id = oIGestCat.DBDelete(oVendedor);
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

        private Vendedor oCatalogo
        {
            get
            {
                Vendedor oVendedor = new Vendedor();
                switch (oIView.eCrud)
                {
                    case Enumeraciones.TipoOperacion.Insertar:
                        ASPxDataInsertingEventArgs eI = (ASPxDataInsertingEventArgs)oIView.oCrud;
                        oVendedor.iIdVendedor = 0;
                        oVendedor.sNombre = eI.NewValues["Nombre"].S();
                        oVendedor.sZona = eI.NewValues["Zona"].S();
                        oVendedor.sIdUnidad4 = eI.NewValues["unit4"].S();
                        oVendedor.sUnidadNegocio = oIGestCat.DBGetDesUnidad4(oVendedor.sIdUnidad4);
                        oVendedor.sLogin = eI.NewValues["Login"].S();
                        oVendedor.sIdUnidad1 = eI.NewValues["unit1"].S();
                        oVendedor.sDescripcionUnidad = oIGestCat.DBGetDesUnidad1V(oVendedor.sIdUnidad1);
                        oVendedor.sCorreoElectronico = eI.NewValues["CorreoElectronico"].S();
                        oVendedor.iIdBase = eI.NewValues["IdBase"].S().I();
                        oVendedor.iStatus = 1;

                        break;
                    case Enumeraciones.TipoOperacion.Actualizar:
                        ASPxDataUpdatingEventArgs eU = (ASPxDataUpdatingEventArgs)oIView.oCrud;

                        oVendedor.iIdVendedor = eU.Keys["IdVendedor"].S().I();
                        oVendedor.sNombre = eU.NewValues["Nombre"].S();
                        oVendedor.sZona = eU.NewValues["Zona"].S();
                        oVendedor.sIdUnidad4 = eU.NewValues["unit4"].S();
                        oVendedor.sUnidadNegocio = oIGestCat.DBGetDesUnidad4(oVendedor.sIdUnidad4);
                        oVendedor.sLogin = eU.NewValues["Login"].S();
                        oVendedor.sIdUnidad1 = eU.NewValues["unit1"].S();
                        oVendedor.sDescripcionUnidad = oIGestCat.DBGetDesUnidad1V(oVendedor.sIdUnidad1);
                        oVendedor.sCorreoElectronico = eU.NewValues["CorreoElectronico"].S();
                        oVendedor.iIdBase = eU.NewValues["IdBase"].S().I();
                        oVendedor.iStatus = eU.NewValues["Status"].S().I();

                        break;
                    case Enumeraciones.TipoOperacion.Validar:
                        ASPxDataValidationEventArgs eV = (ASPxDataValidationEventArgs)oIView.oCrud;    
                        oVendedor.sLogin = eV.NewValues["Login"].S();
                        oVendedor.iStatus = -1;

                        break;
                    case Enumeraciones.TipoOperacion.Eliminar:
                        ASPxDataDeletingEventArgs eD = (ASPxDataDeletingEventArgs)oIView.oCrud;
                        oVendedor.sDescripcionUnidad = eD.Values["DescripcionUnidad"].S();
                        oVendedor.iIdVendedor = eD.Keys["IdVendedor"].S().I();
                        break;
                }

                return oVendedor;
            }
        }
        private bool bValidaActualizacion
        {
            get
            {
                bool bValida = true;
                ASPxDataValidationEventArgs eU = (ASPxDataValidationEventArgs)oIView.oCrud;

                bValida = eU.NewValues["Login"].S().ToUpper() != eU.OldValues["Login"].S().ToUpper();

                return bValida;
            }

        }
    }
}