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
    public class TipoCliente_Presenter: BasePresenter<IviewTipoCliente>
    {
        private readonly DBTipoCliente oIGestCat;

        public TipoCliente_Presenter(IviewTipoCliente oView, DBTipoCliente oGC)
            : base(oView)
        {
            oIGestCat = oGC;
            oView.eGetTipCliente += GetCodigoUnidad_Presenter;
            
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
                    oIView.MostrarMensaje(Mensajes.LMensajes[Enumeraciones.Mensajes.Eliminacion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
                    oIView.ObtieneValores();
                }
            }
            catch (Exception ex)
            {
                oIView.MostrarMensaje("Se Eliminó el registro.", "ERROR DE SISTEMA");
            }
        }
        protected void GetCodigoUnidad_Presenter(object sender, EventArgs e)
        {
            try
            {
                oIView.LoadCatalogoTipocliente(oIGestCat.dtObjsCat);
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


        private TipoCliente oCatalogo
        {
            get
            {
                TipoCliente oTipoCliente = new TipoCliente();
                switch (oIView.eCrud)
                {
                    case Enumeraciones.TipoOperacion.Insertar:
                        ASPxDataInsertingEventArgs eI = (ASPxDataInsertingEventArgs)oIView.oCrud;
                        oTipoCliente.iId = 0;
                        oTipoCliente.sTipoClienteDescripcion = eI.NewValues["TipoClienteDescripcion"].S();
                        oTipoCliente.sCodigoDeUnidad4 = eI.NewValues["CodigoDeUnidad4"].S();
                        oTipoCliente.sCodigoDeUnidad4Descripcion = oIGestCat.GetCodUnit4Descripcion(oTipoCliente.sCodigoDeUnidad4);
                        oTipoCliente.iHrsPernocta = eI.NewValues["HrsPernocta"].S().I();
                        oTipoCliente.iStatus = 1;

                        break;
                    case Enumeraciones.TipoOperacion.Actualizar:
                        ASPxDataUpdatingEventArgs eU = (ASPxDataUpdatingEventArgs)oIView.oCrud;

                        oTipoCliente.iId = eU.Keys[0].S().I();
                        oTipoCliente.sTipoClienteDescripcion = eU.NewValues["TipoClienteDescripcion"].S();
                        oTipoCliente.sCodigoDeUnidad4 = eU.NewValues["CodigoDeUnidad4"].S();
                        oTipoCliente.sCodigoDeUnidad4Descripcion = oIGestCat.GetCodUnit4Descripcion(oTipoCliente.sCodigoDeUnidad4);
                        oTipoCliente.iHrsPernocta = eU.NewValues["HrsPernocta"].S().I();
                        oTipoCliente.iStatus = eU.NewValues["Status"].S().I();

                        break;
                    case Enumeraciones.TipoOperacion.Eliminar:
                        ASPxDataDeletingEventArgs eD = (ASPxDataDeletingEventArgs)oIView.oCrud;
                        oTipoCliente.iId = eD.Keys[0].S().I();
                        break;
                    case Enumeraciones.TipoOperacion.Validar:
                        ASPxDataValidationEventArgs eV = (ASPxDataValidationEventArgs)oIView.oCrud;

                        oTipoCliente.sTipoClienteDescripcion = eV.NewValues["TipoClienteDescripcion"].S();
                        oTipoCliente.sCodigoDeUnidad4 = eV.NewValues["CodigoDeUnidad4"].S();
                        break;
                }

                return oTipoCliente;
            }
        }
        private bool bValidaActualizacion
        {
            get
            {
                bool bValida = false;
                ASPxDataValidationEventArgs eU = (ASPxDataValidationEventArgs)oIView.oCrud;

                if (eU.NewValues["TipoClienteDescripcion"].S().ToUpper() != eU.OldValues["TipoClienteDescripcion"].S().ToUpper() ||
                            eU.NewValues["CodigoDeUnidad4"].S().ToUpper() != eU.OldValues["CodigoDeUnidad4"].S().ToUpper())
                    bValida = true;

                return bValida;
            }
        }
    }
}