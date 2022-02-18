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
    public class NotaCredito_Presenter : BasePresenter<IViewNotaCredito>
    {
        private readonly DBNotaCredito oIGestCat;
        

        public NotaCredito_Presenter(IViewNotaCredito oView, DBNotaCredito oGC)
            : base(oView)
        {
            oIGestCat = oGC;
            oIView.eSearchConsulta += eSearchConsulta_Presenter;
            oIView.eDeleteObj += eDelete_Presenter;
        }

        protected override void SearchObj_Presenter(object sender, EventArgs e)
        {
            try 
            {
                oIView.LoadObjects(oIGestCat.DBSearchObj(oIView.oArrFiltros));
            }
            catch (Exception x) { throw x; }
        }

        protected override void NewObj_Presenter(object sender, EventArgs e)
        {
            try
            {
                if (oIView.oCatalogo.iTipoNotaCredito == "Seleccione")
                { oIView.MostrarMensaje("Debe seleccionar un Tipo de Crédito", "ATENCIÓN"); return; }
                if (oIView.oCatalogo.iTipoNotaCredito == "Tiempo Vuelo" && oIView.oCatalogo.iTiempo == string.Empty)
                { oIView.MostrarMensaje("Debe Ingresar el Tiempo de Vuelo", "ATENCIÓN"); return; }
                if (oIView.oCatalogo.iTipoNotaCredito == "Pernocta" && oIView.oCatalogo.iCantidad == 0)
                { oIView.MostrarMensaje("Debe Ingresar la Cantidad de Pernocta", "ATENCIÓN"); return; }
                if (oIView.oCatalogo.iIdFolioRemision == 0)
                { oIView.MostrarMensaje("No existe un folio de Remisión", "ATENCIÓN"); return; }


                int id = oIGestCat.DBSave(oIView.oCatalogo);
                if (id > 0)
                {
                    oIView.MostrarMensaje(Mensajes.LMensajes[Enumeraciones.Mensajes.Insercion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
                 
                    //oIView.MostrarMensaje("Se guardó Folio Nota Crédito: " + id, "REGISTRO GENERADO");
                }
            }
            catch (Exception ex)
            {
               throw ex;
            }
        }

        protected void eSearchConsulta_Presenter(object sender, EventArgs e)
        {
            try 
            { 
                 oIView.LoadObjects(oIGestCat.DBSearchObjP(oIView.oBusqueda));
            }
            catch (Exception x) { throw x; }
        }

        protected void eDelete_Presenter(object sender, EventArgs e)
        {
            try
            {
                int id = oIGestCat.DBDelete(oCatalogoNotaCredito);
                if (id > 0)
                {
                    oIView.LoadObjects(oIGestCat.DBSearchObjP(oIView.oBusqueda));
                    oIView.MostrarMensaje(Mensajes.LMensajes[Enumeraciones.Mensajes.Eliminacion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
                }
            }
            catch (Exception x) { throw x; }
        }

        private NotaCredito oCatalogoNotaCredito
        {
            get
            {
                NotaCredito oNota = new NotaCredito();
                switch (oIView.eCrud)
                {
                    case Enumeraciones.TipoOperacion.Eliminar:
                        ASPxDataDeletingEventArgs eD = (ASPxDataDeletingEventArgs)oIView.oCrud;
                        oNota.IdNotaCredito = eD.Keys[0].S().I();
                        break;
                }
                return oNota;
            }
        }
    }
}