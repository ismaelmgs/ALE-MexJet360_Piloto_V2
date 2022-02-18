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
    public class ComisariatoProducto_Presenter : BasePresenter<IViewComisariatoProducto>
    {
        private readonly DBComisariatoProducto oIGestCat;

        public ComisariatoProducto_Presenter(IViewComisariatoProducto oView, DBComisariatoProducto oGC)
            : base(oView)
        {
            oIGestCat = oGC;
            oIView.eGetComisariato += GetComisariatos_Presenter;
            oIView.eGetProducto += GetProductos_Presenter;
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
                ComisariatoProducto oComisariatoProducto = oCatalogo;
                int id = oIGestCat.DBUpdate(oComisariatoProducto);
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

        protected void GetComisariatos_Presenter(object sender, EventArgs e)
        {
            oIView.LoadCatalogoComisariato(oIGestCat.dtObjsComisariato);
        }

        protected void GetProductos_Presenter(object sender, EventArgs e)
        {
            oIView.LoadCatalogoProducto(oIGestCat.dtObjsProducto);
        }

        private ComisariatoProducto oCatalogo
        {
            get
            {
                ComisariatoProducto oComisariatoProducto = new ComisariatoProducto();
                switch (oIView.eCrud)
                {
                    case Enumeraciones.TipoOperacion.Insertar:
                        ASPxDataInsertingEventArgs eI = (ASPxDataInsertingEventArgs)oIView.oCrud;
                        oComisariatoProducto.iId = 0;
                        oComisariatoProducto.iIdComisariato = eI.NewValues["IdComisariato"].S().I();
                        oComisariatoProducto.iIdProducto = eI.NewValues["IdProducto"].S().I();
                        oComisariatoProducto.iCantidad = eI.NewValues["Cantidad"].S().I();
                        oComisariatoProducto.iStatus = 1;

                        break;
                    case Enumeraciones.TipoOperacion.Actualizar:
                        ASPxDataUpdatingEventArgs eU = (ASPxDataUpdatingEventArgs)oIView.oCrud;
                        oComisariatoProducto.iId = eU.Keys[0].S().I();
                        oComisariatoProducto.iIdComisariato = eU.NewValues["IdComisariato"].S().I();
                        oComisariatoProducto.iIdProducto = eU.NewValues["IdProducto"].S().I();
                        oComisariatoProducto.iCantidad = eU.NewValues["Cantidad"].S().I();
                        oComisariatoProducto.iStatus = eU.NewValues["Status"].S().I();

                        break;
                    case Enumeraciones.TipoOperacion.Validar:
                        ASPxDataValidationEventArgs eV = (ASPxDataValidationEventArgs)oIView.oCrud;

                        oComisariatoProducto.iIdComisariato = eV.NewValues["IdComisariato"].S().I();
                        oComisariatoProducto.iIdProducto = eV.NewValues["IdProducto"].S().I();
                        oComisariatoProducto.iCantidad = eV.NewValues["Cantidad"].S().I();
                        oComisariatoProducto.iStatus = eV.NewValues["Status"].S().I();

                        break;
                    case Enumeraciones.TipoOperacion.Eliminar:
                        ASPxDataDeletingEventArgs eD = (ASPxDataDeletingEventArgs)oIView.oCrud;
                        oComisariatoProducto.iId = eD.Keys[0].S().I();
                        oComisariatoProducto.iIdComisariato = eD.Values["IdComisariato"].S().I();
                        oComisariatoProducto.iIdProducto = eD.Values["IdProducto"].S().I();
                        break;
                }

                return oComisariatoProducto;
            }
        }
        private bool bValidaActualizacion
        {
            get
            {
                bool bValida = true;
                ASPxDataValidationEventArgs eU = (ASPxDataValidationEventArgs)oIView.oCrud;
                bValida = ((eU.NewValues["IdComisariato"].S().I() != eU.OldValues["IdComisariato"].S().I()) || (eU.NewValues["IdProducto"].S().I() != eU.OldValues["IdProducto"].S().I()));
                return bValida;
            }


        }
    }
}