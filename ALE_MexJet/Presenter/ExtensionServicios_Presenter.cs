using ALE_MexJet.DomainModel;
using ALE_MexJet.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using NucleoBase.Core;
using ALE_MexJet.Objetos;
using ALE_MexJet.Clases;

namespace ALE_MexJet.Presenter
{
    public class ExtensionServicios_Presenter : BasePresenter<IViewExtensionServicios>
    {
        private readonly DBExtensionServicios oIGestCat;

        public ExtensionServicios_Presenter(IViewExtensionServicios oView, DBExtensionServicios oGC)
            : base(oView)
        {
            oIGestCat = oGC;

            oIView.eLoadOrigDestFiltro += eLoadOrigDestFiltro_Presenter;
            oIView.eLoadExtensionServicioImprimir += LoadExtensionServicioImprimir_Presenter;
            oIView.eLoadExtensionServicioEnviarMail += LoadExtensionServicioEnviarMail_Presenter;
        }

        protected override void SearchObj_Presenter(object sender, EventArgs e)
        {
            DataTable dtPilotos = oIGestCat.DBGetObtienPilotosActivos;
            object[] arrFilM = new object[]{
                                                "@Serie", "%" + string.Empty + "%",
                                                "@Matricula", "%" + string.Empty + "%",
                                                "@estatus", 1
                                            };

            DataTable dtMat = new DBAeronave().DBSearchObj(arrFilM);
            oIView.dtOrigenDestino = new DBSolicitudesVuelo().DBObtieneDestinoOrigen();
            oIView.LoadDatosControles(dtPilotos, dtMat, GetArmaTablasHoras);

        }

        protected override void ObjSelected_Presenter(object sender, EventArgs e)
        {
            oIView.oExtensionServ = oIGestCat.DBGetObtieneExtensionById(oIView.iExtension);
        }

        protected override void SaveObj_Presenter(object sender, EventArgs e)
        {
            int id = oIGestCat.DBSetActualizaExtensionServicios(oIView.oExtensionServ);
            if (id > 0)
            {
                oIView.MostrarMensaje(Mensajes.LMensajes[Enumeraciones.Mensajes.Modificacion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
            }
        }

        protected override void NewObj_Presenter(object sender, EventArgs e)
        {
            int i = oIGestCat.DBSetInsertaExtensionServicios(oIView.oExtensionServ);
            if (i > 0)
            {
                oIView.MostrarMensaje(Mensajes.LMensajes[Enumeraciones.Mensajes.Insercion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
            }
        }

        protected void eLoadOrigDestFiltro_Presenter(object sender, EventArgs e)
        {
            oIView.dtOrigenDestino = new DBSolicitudesVuelo().DBObtieneDestOrigFiltro(sender.S());
        }

        protected DataSet GetArmaTablasHoras
        {
            get
            {
                try
                {
                    DataTable dtHoras = new DataTable();
                    dtHoras.Columns.Add("IdHora", typeof(int));
                    dtHoras.Columns.Add("DescHora");

                    DataTable dtMinutos = new DataTable();
                    dtMinutos.Columns.Add("IdMinuto", typeof(int));
                    dtMinutos.Columns.Add("DescMinuto");

                    for (int i = 0; i < 60; i++)
                    {
                        if (i < 24)
                        {
                            DataRow rowH = dtHoras.NewRow();
                            rowH["IdHora"] = i;
                            rowH["DescHora"] = i.S().PadLeft(2, '0');
                            dtHoras.Rows.Add(rowH);
                        }

                        DataRow rowM = dtMinutos.NewRow();
                        rowM["IdMinuto"] = i;
                        rowM["DescMinuto"] = i.S().PadLeft(2, '0');
                        dtMinutos.Rows.Add(rowM);
                    }


                    DataSet ds = new DataSet();
                    ds.Tables.Add(dtHoras);
                    ds.Tables.Add(dtMinutos);

                    return ds;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        protected void LoadExtensionServicioImprimir_Presenter(object sender, EventArgs e)
        {
            oIView.ImprimirReporte(oIGestCat.DBGetObtieneReporteExtensionById(oIView.iExtension));
        }

        protected void LoadExtensionServicioEnviarMail_Presenter(object sender, EventArgs e)
        {
            oIView.EnviarMailReporte(oIGestCat.DBGetObtieneReporteExtensionById(oIView.iExtension));
        }

    }
}