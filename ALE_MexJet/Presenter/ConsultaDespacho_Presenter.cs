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
    public class ConsultaDespacho_Presenter: BasePresenter<IViewConsultaDespacho>
    {
        private readonly DBConsultaDespacho oIGestCat;

        public ConsultaDespacho_Presenter(IViewConsultaDespacho oView, DBConsultaDespacho oGC)
            : base(oView)
        {
            oIGestCat = oGC;
            oIView.eSearchObj += oIView_eSearchObj;
            oIView.eGetClientes += oIView_eGetClientes;
            oIView.eGetContrato += oIView_eGetContrato;
            oIView.eBuscaSubGrid += oIView_eBuscaSubGrid;
            oIView.eInsertaDictamen += oIView_eInsertaDictamen;
            oIView.eConsultaSol += oIView_eConsultaSol;
        }

        void oIView_eGetContrato(object sender, EventArgs e)
        {
            oIView.LoadContrato(oIGestCat.DBSearchContrato(oIView.oArrFiltroContrato));
        }
        void oIView_eGetClientes(object sender, EventArgs e)
        {
            oIView.LoadClientes(oIGestCat.DBSearchCliente(oIView.oArrFiltroClientes));
        }
        void oIView_eSearchObj(object sender, EventArgs e)
        {
            oIView.LoadConsultaDespacho(oIGestCat.DBBuscaDespacho(oIView.oArrFiltros));
        }
        void oIView_eBuscaSubGrid(object sender, EventArgs e)
        {
            oIView.LoadPiernas(oIGestCat.DBSearchPiernasSolicitud(oIView.oArrFilSolicitud));
        }
        void oIView_eInsertaDictamen(object sender, EventArgs e)
        {
            int Resultado = oIGestCat.DBInsertDictamen(oIView.oArrInsertDictamen);
            if (Resultado >  0)
            {
                oIView.MostrarMensaje(Mensajes.LMensajes[Enumeraciones.Mensajes.Insercion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
            }
        }
        void oIView_eConsultaSol(object sender, EventArgs e)
        {
            oIView.LoadSol(oIGestCat.DBConsultaSolicitud(oIView.oArrFilSolicitud));
        }
    }
}