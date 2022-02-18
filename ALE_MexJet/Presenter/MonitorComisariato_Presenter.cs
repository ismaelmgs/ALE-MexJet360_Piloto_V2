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
    public class MonitorComisariato_Presenter : BasePresenter<IViewMonitorComisariato>
    {
        private readonly DBMonitorComisariato oIMonitorComisariato;

        public MonitorComisariato_Presenter(IViewMonitorComisariato oView, DBMonitorComisariato oDBMonitorComisariato)
            : base(oView)
        {
            oIMonitorComisariato  = oDBMonitorComisariato;
            oIView.eSearchObj += oIView_eSearchObj;
            oIView.eLoadStaus += GetStatus_Presenter;
            oIView.eActualizaStataus += EditaStatus_Presenter;
            oIView.eInsertaDetalle += InsertDetalle_Presenter;
            oIView.eConsultaComisariatoDetalle += GetComisariatoDetalle_Presenter;
        }

        void GetComisariatoDetalle_Presenter(object sender, EventArgs e)
        {
            oIView.LoadComisariatoDetalle(oIMonitorComisariato.DBGetComisariatoDetalle(oIView.oArrID));
        }
        void oIView_eSearchObj(object sender, EventArgs e)
        {
            oIView.LoadObjects(oIMonitorComisariato.DBSearchMonitorComisariato(oIView.oArrFiltros));
        }
        void GetStatus_Presenter(object sender, EventArgs e)
        {
            oIView.LoadStatus(oIMonitorComisariato.DBGetStausComisariato());
        }
        public void EditaStatus_Presenter(object sender, EventArgs e)
        {
            try
            {
                int id = oIMonitorComisariato.DBUpdateStaus(oCatComisariatoTramo);
                if (id > 0)
                {
                    //oIView.MostrarMensaje(Mensajes.LMensajes[Enumeraciones.Mensajes.Modificacion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
                }
            }
            catch (Exception ex)
            {
                oIView.MostrarMensaje("Se detectó el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
            }
        }
        public void InsertDetalle_Presenter(object sender, EventArgs e)
        {
            try
            {
                int id = oIMonitorComisariato.DBInsertaDetalle(oIView.oArrFil);
                if (id > 0)
                {
                    oIView.MostrarMensaje(Mensajes.LMensajes[Enumeraciones.Mensajes.Modificacion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
                }
            }
            catch (Exception ex)
            {
                oIView.MostrarMensaje("Se detectó el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
            }
        }


        private MonitorComisariato oCatComisariatoTramo
        {
            get
            {
                MonitorComisariato oMonitor = new MonitorComisariato();
                switch (oIView.eCrud)
                {
                    //case Enumeraciones.TipoOperacion.Insertar:
                    //    ASPxDataInsertingEventArgs eI = (ASPxDataInsertingEventArgs)oIView.oCrud;
                    //    oTramo.iIdTramo = eI.NewValues["IdTramo"].S().I();
                    //    oTramo.iIdProveedor = eI.NewValues["Descripcion"].S().I();
                    //    oTramo.sComisariatoDesc = eI.NewValues["ComisariatoDesc"].S();
                    //    oTramo.dPrecioCotizado = eI.NewValues["PrecioCotizado"].S().D();

                    //    break;
                    case Enumeraciones.TipoOperacion.Actualizar:
                        ASPxDataUpdatingEventArgs eU = (ASPxDataUpdatingEventArgs)oIView.oCrud;

                        oMonitor.iIdComisariato = eU.Keys[0].S().I();
                        oMonitor.iStatus = eU.NewValues["Estaus"].S().I();

                        if (oMonitor.iStatus==0)
                            oMonitor.iIdComisariato = -1;
                        //oTramo.iIdComisariato = eU.Keys[0].S().I();
                        //oTramo.iIdProveedor = eU.NewValues["Descripcion"].S().I() == 0 ? eU.Keys[1].S().I() : eU.NewValues["Descripcion"].S().I();
                        //oTramo.sComisariatoDesc = eU.NewValues["ComisariatoDesc"].S();
                        //oTramo.dPrecioCotizado = eU.NewValues["PrecioCotizado"].S().D();

                        break;
                    //case Enumeraciones.TipoOperacion.Eliminar:
                    //    ASPxDataDeletingEventArgs eD = (ASPxDataDeletingEventArgs)oIView.oCrud;
                    //    oTramo.iIdComisariato = eD.Keys[0].S().I();
                    //    break;
                }
                return oMonitor;
            }
        }
    }
}