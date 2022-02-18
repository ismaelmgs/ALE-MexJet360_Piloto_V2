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
    public class BitacoraPresenter : BasePresenter<IViewBitacora>
    {
        private readonly DBBitacora oIGestCat;

        public BitacoraPresenter(IViewBitacora oView, DBBitacora oGC)
            : base(oView)
        {
            oIGestCat = oGC;
            oIView.eObjCliente += SearchObjCiente_Presenter;
            oIView.eObjConrato += SearchObjContrato_Presenter;
            oIView.eObjBitacora += SearchObjBitacora_Presenter;
            oIView.eObjBitacoraDuplicada += oIView_eObjBitacoraDuplicada;
            oIView.eObjBitacoraCobrada += oIView_eObjBitacoraCobrada;
            oIView.eObjBitacoraPorCobrar += oIView_eObjBitacoraPorCobrar;
            oIView.eObjNumeroBitacorasCobradas += oIView_eObjNumeroBitacorasCobradas;
            oIView.eObjNumeroBitacorasPorCobrar += oIView_eObjNumeroBitacorasPorCobrar;
            oIView.eObjNumeroTotalRegistros += oIView_eObjNumeroTotalRegistros;
            oIView.eObjNumeroTotalDuplicadas += oIView_eObjNumeroTotalDuplicadas;
        }

        void oIView_eObjBitacoraDuplicada(object sender, EventArgs e)
        {
            oIView.LoadBitacoraDuplicada(oIGestCat.dtBitacoraDuplicada());
        }

        void oIView_eObjBitacoraCobrada(object sender, EventArgs e)
        {
            oIView.LoadBitacoraCobrada(oIGestCat.dtBitacoraCobrada(oIView.oBitacoraBitacoraCobrada));
        }

        void oIView_eObjBitacoraPorCobrar(object sender, EventArgs e)
        {
            oIView.LoadBitacoraPorCobrar(oIGestCat.dtBitacoraPorCobrar(oIView.oBitacoraBitacoraPorCobrar));
        }

        void oIView_eObjNumeroBitacorasCobradas(object sender, EventArgs e)
        {
            oIView.LoadNumeroBitacorasCobradas(oIGestCat.dtNumeroBitacorasCobradas(oIView.oBitacoraBitacoraNumeroCobrada));
        }

        void oIView_eObjNumeroBitacorasPorCobrar(object sender, EventArgs e)
        {
            oIView.LoadNumeroBitacorasPorCobrar(oIGestCat.dtNumeroBitacorasPorCobrar(oIView.oBitacoraBitacoraNumeroPorCobrar));
        }

        void oIView_eObjNumeroTotalRegistros(object sender, EventArgs e)
        {
            oIView.LoadNumeroTotalRegistros(oIGestCat.dtNumeroTotalRegistros(oIView.oNumeroTotalRegistros));
        }

        void oIView_eObjNumeroTotalDuplicadas(object sender, EventArgs e)
        {
            oIView.LoadNumeroTotalDuplicadas(oIGestCat.dtNumeroTotalDuplicadas());
        }

        protected  void SearchObjCiente_Presenter(object sender, EventArgs e)
        {
            oIView.LoadCliente(oIGestCat.dtObjCliente());
        }

        protected void SearchObjContrato_Presenter(object sender, EventArgs e)
        {
            oIView.LoadContrato(oIGestCat.dtObjContrato(oIView.oBitacoraContrato));
        }

        protected void SearchObjBitacora_Presenter(object sender, EventArgs e)
        {
            oIView.LoadBitacora(oIGestCat.dtObjBitacora(oIView.oBitacoraBitacora));
        }

        protected override void DeleteObj_Presenter(object sender, EventArgs e)
        {
            try
            {
                int id = oIGestCat.DBDelete(oBitacora);
                if (id > 0)
                {
                    oIView.MostrarMensaje(Mensajes.LMensajes[Enumeraciones.Mensajes.Eliminacion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
                    oIView.CargaBitacorasDuplicadas();
                }
            }
            catch (Exception ex)
            {
                oIView.MostrarMensaje("Se detectó el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
            }
        }

        private Bitacora oBitacora
        {
            get 
            {
                Bitacora oBitacora = new Bitacora();
                switch (oIView.eCrud)
                {
                    case Enumeraciones.TipoOperacion.Eliminar:
                        ASPxDataDeletingEventArgs eD = (ASPxDataDeletingEventArgs)oIView.oCrud;
                        oBitacora.iIdBitacora = eD.Keys[0].S().I();
                        break;
                }
                return oBitacora;
            }
        }
    }
}


