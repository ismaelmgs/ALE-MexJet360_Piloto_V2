using ALE_MexJet.DomainModel;
using ALE_MexJet.Interfaces;
using ALE_MexJet.Objetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NucleoBase.Core;
using ALE_MexJet.Clases;
using System.Data;

namespace ALE_MexJet.Presenter
{
    public class OfertaFerry_Presenter : BasePresenter<IViewOfertaFerry>
    {
        private readonly DBOfertaFerry oIGestCat;

        public OfertaFerry_Presenter(IViewOfertaFerry oView, DBOfertaFerry oGC)
            : base(oView)
        {
            oIGestCat = oGC;

            oIView.eSearchEnviadas += oIView_eSearchEnviadas;
            oIView.eLoadOrigDestFiltro += eLoadOrigDestFiltro_Presenter;
            oIView.eLoadOrigDestFiltroDest += eLoadOrigDestFiltroDest_Presenter;
            oIView.eSavFerry += eSavFerry_Presenter;
            oIView.eSavFerryPendiente += SaveObjFerryPendiente_Presenter;
            oIView.eSearchFerryPendiente += SearchFerryPendiente_Presenter;
            oView.eSearchFerryHijo += SearchObjFerryHijo_Presenter;
            oIView.eSearchListaDifusionFerry += SearchObjListaDifusionFerry_Presenter;
            oIView.eSaveListaDifusionFerry += SaveListaDifusionFerry_Presenter;
            oIView.eDeleteOfertaFerryPendiente += DeleteOfertaFerryPendiente_Presenter;
            oIView.eSaveObjFerryHijo += SaveObjFerryHijo_Presenter;
            oIView.eSearchListaDifusion += SearchObjListaDifusion_Presenter;
        }

        protected void oIView_eSearchEnviadas(object sender, EventArgs e)
        {
            oIView.LoadFerrys(oIGestCat.DBGetFerrysEnviados);
        }
        protected override void SaveObj_Presenter(object sender, EventArgs e)
        {
            List<OfertaFerry> oLst = new List<OfertaFerry>();
            List<object> ols = oIView.oListFerrys;

            foreach (object[] oFerry in ols)
            {
                DataTable dt = oIGestCat.DBGetObtieneFerryPendientePadreHijo(oFerry[0].S().I());

                foreach (DataRow row in dt.Rows)
                {
                    OfertaFerry oF = new OfertaFerry();
                    oF.iIdFerry = row["IdPendiente"].S().I();
                    oF.iTrip = row["Trip"].S().I();
                    oF.iNoPierna = row["NoPierna"].S().I();
                    oF.sOrigen = row["Origen"].S();
                    oF.dtFechaSalida = row["FechaSalida"].S().Dt();
                    oF.sDestino = row["Destino"].S();
                    oF.dtFechaLlegada = row["FechaLlegada"].S().Dt();
                    oF.sMatricula = row["Matricula"].S();
                    oF.sTiempoVuelo = row["TiempoVuelo"].S();
                    oF.sGrupoModelo = row["GrupoModelo"].S();
                    oF.iLegId = row["LegId"].S().I();
                    oF.iIdPendiente = row["IdPendiente"].S().I();
                    oF.sReferencia = string.Empty;
                    oF.iIdPadre = row["IdPadre"].S().I();
                    oF.bJetSmart = false;
                    oF.iStatusJet = 0;
                    oF.bApp = false;
                    oF.iStatusApp = 0;
                    oF.bEZMexJet = true;
                    oF.iStatusEZ = 2;

                    oLst.Add(oF);
                }
            }


            //List<OfertaFerry> oLst = oIView.oLstFerrys;

            if (oIGestCat.DBSetInsertaOfertaFerry(oLst))
            {
                string cadena = ArmaCSV(oLst);

                oIView.LoadFerrys(oIGestCat.DBGetFerrysPeriodo);
                oIView.MostrarMensaje("Los Ferrys se registraron correctamente.", "Aviso");
                oIView.CreaCSV(cadena);

                if (bJetSmart)
                {
                    oIView.enviaCorreoJetSmarter(); // Enviamos el correo si Jetsmart es verdadero
                }
            }
        }
        protected override void SearchObj_Presenter(object sender, EventArgs e)
        {

            object[] oArrMat = new object[] { "@Serie", string.Empty,
                                            "@Matricula", string.Empty,
                                            "@estatus", 1
                                    };
            oIView.dtMatriculas = new DBAeronave().DBSearchObj(oArrMat);

            oIView.LoadFerrys(oIGestCat.DBGetFerrysPeriodo);
        }
        protected void eLoadOrigDestFiltro_Presenter(object sender, EventArgs e)
        {
            oIView.dtOrigen = new DBPresupuesto().GetAeropuertosOrigen(oIView.sFiltroO, 2);
        }
        protected void eLoadOrigDestFiltroDest_Presenter(object sender, EventArgs e)
        {
            oIView.dtDestino = new DBPresupuesto().GetAeropuertosDestino(oIView.sFiltroD, string.Empty, 2);
        }
        protected void eSavFerry_Presenter(object sender, EventArgs e)
        {
            if (oIGestCat.DBSetInsertaOfertaFerryManuales((FerryNuevos)sender))
            {
                SearchObj_Presenter(null, EventArgs.Empty);
                oIView.MostrarMensaje("Se insertó correctamente el Ferry", "Aviso");
            }
            else
                oIView.MostrarMensaje("Ocurrio un error al registrar el Ferry", "Aviso");
        }

        private bool bJetSmart;
        private string ArmaCSV(List<OfertaFerry> oLst)
        {
            string cadena = string.Empty;
            foreach (OfertaFerry oF in oLst)
            {
                if (oF.bJetSmart)
                {
                    cadena +=
                            oF.sOrigen + "," +
                            oF.sDestino + "," +
                            oF.sFechaSalida + ":00" + "," +
                            oF.sFechaLlegada + ":00" + "," +
                            Math.Round(Utils.ConvierteTiempoaDecimal(oF.sTiempoVuelo), 2).ToString().Replace(",", ".") + "," +
                            oF.sGrupoModelo + "," +
                            oF.sMatricula + "," +
                        //oF.iNoPierna + ","+
                            oF.iLegId + "," +
                            oF.sReferencia;
                    cadena += "\r\n";

                    bJetSmart = oF.bJetSmart;
                }

            }

            return cadena;
        }

        protected void SaveObjFerryPendiente_Presenter(object sender, EventArgs e)
        {
            List<OfertaFerry> oLst = oIView.oLstFerrysPendiente;

            if (oIGestCat.DBSetInsertaOfertaFerryPendiente(oLst))
            {
                oIView.LoadFerrys(oIGestCat.DBGetFerrysPeriodoPendiente(0));//oIView.iIdPadre));
                oIView.MostrarMensaje("Los Ferrys se registraron temporalmente.", "Aviso");
            }
        }
        protected void SearchFerryPendiente_Presenter(object sender, EventArgs e)
        {
            oIView.LoadFerrys(oIGestCat.DBGetFerrysPeriodoPendiente(oIView.iIdPadre));
        }
        protected void SearchObjFerryHijo_Presenter(object sender, EventArgs e)
        {
            oIView.dtFerrysHijo = oIGestCat.DBGetFerrysPeriodoPendiente(oIView.iIdPadre);
        }
        protected void SearchObjListaDifusionFerry_Presenter(object sender, EventArgs e)
        {
            DataTable dtListaDifusionFerry = oIGestCat.DBGetListaDifusionFerry(oIView.iIdPendiente, oIView.sTipoListaDifusion);

            if (dtListaDifusionFerry.Rows.Count > 0)
            {
                if (dtListaDifusionFerry.Rows[0]["ListaDifusion"] != null)
                {
                    oIView.sIdsListaDifusion = dtListaDifusionFerry.Rows[0]["ListaDifusion"].S();
                }
                else
                {
                    oIView.sIdsListaDifusion = string.Empty;
                }
            }
            else
            {
                oIView.sIdsListaDifusion = string.Empty;
            }
        }
        protected void SaveListaDifusionFerry_Presenter(object sender, EventArgs e)
        {
            oIGestCat.DBUpdateListaDifusionFerry(oIView.iIdPendiente, oIView.sIdsListaDifusion, oIView.sTipoListaDifusion);
            oIView.MostrarMensaje("Se actualizaron las listas de Difusión correctamente.", "Aviso");
        }
        protected void DeleteOfertaFerryPendiente_Presenter(object sender, EventArgs e)
        {
            oIGestCat.DBDeleteOfertaFerryPendiente(oIView.iIdPendiente);
            oIView.MostrarMensaje("Se Eliminó el Ferry.", "Aviso");
        }
        protected void SaveObjFerryHijo_Presenter(object sender, EventArgs e)
        {
            List<OfertaFerry> oLst = oIView.oLstFerrysHijo;

            if (oIGestCat.DBSetInsertaOfertaFerry(oLst))
            {
                string cadena = ArmaCSV(oLst);

                oIView.LoadFerrys(oIGestCat.DBGetFerrysPeriodo);
                oIView.MostrarMensaje("Los Ferrys se registraron correctamente.", "Aviso");
                oIView.CreaCSV(cadena);

                if (bJetSmart)
                {
                    oIView.enviaCorreoJetSmarter(); // Enviamos el correo si Jetsmart es verdadero
                }
            }
        }

        protected void SearchObjListaDifusion_Presenter(object sender, EventArgs e)
        {
            string sTipoLista = (string)sender;

            object[] oArr = new object[] { "@TipoLista", sTipoLista };

            DataTable dtListas = oIGestCat.DBSearchObjListaDifusion(oArr);
            DataTable dtFinal = new DataTable();
            dtFinal = dtListas.Clone();

            DataRow dr = dtFinal.NewRow();
            dr["IdListaDifusion"] = 0;
            dr["Descripcion"] = "(TODOS)";

            dtFinal.Rows.Add(dr);

            foreach (DataRow row in dtListas.Rows)
            {
                dtFinal.ImportRow(row);
            }

            oIView.dtListasDifusion = dtFinal;
        }
    }
}