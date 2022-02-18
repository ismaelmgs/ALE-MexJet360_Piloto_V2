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
using System.Data;

namespace ALE_MexJet.Presenter
{
    public class Presupuesto_Presenter : BasePresenter<iViewPresupuesto>
    {
        private readonly DBPresupuesto oIGestCat;
        public Presupuesto_Presenter(iViewPresupuesto oView, DBPresupuesto oGC)
            : base(oView)
        {
            oIGestCat = oGC;
            oIView.eGetDatosCliente += GetContrato_Presenter;
            oIView.eLoadOrigDestFiltro += eLoadOrigDestFiltro_Presenter;
            oIView.eLoadOrigDestFiltroDest += eLoadOrigDestFiltroDest_Presenter;
            oIView.eGetCalculoPresupuesto += eGetCalculoPresupuesto_Presenter;
            oIView.eGetReCalculoPresupuesto += eGetReCalculoPresupuesto_Presenter;
            oIView.eViabilidad += LoadViabilidad_Presenter;
            oIView.eSaveSolicitud += GuardaSolicitud_Presenter;
            oIView.eInsertaMonitorDespacho += NewMonitorDespacho_Presenter;
            oIView.eGuardaSeguimientoHistorico += GuardaSeguimiento_Presenter;

            oIView.eGetCalculoPresupuesto2 += GetCalculoPresupuesto2_Presenter;
            oIView.eGetReCalculoPresupuesto2 += GetReCalculoPresupuesto2_Presenter;
        }
        protected override void SearchObj_Presenter(object sender, EventArgs e)
        {
            try
            {
                oIView.dtClientes = oIGestCat.dtCliente;
                oIView.dtGrupoModelo = oIGestCat.dtGrupoModelos;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void GetContrato_Presenter(object sender, EventArgs e)
        {
            try
            {
                oIView.dtContrato = oIGestCat.GetContratos(oIView.iIdCliente);
                oIView.dtContactos = oIGestCat.GetContactos(oIView.iIdCliente);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        protected void eLoadOrigDestFiltro_Presenter(object sender, EventArgs e)
        {
            oIView.dtOrigen = oIGestCat.GetAeropuertosOrigen(oIView.sFiltroO, oIView.iTipoFiltro);
        }
        protected void eLoadOrigDestFiltroDest_Presenter(object sender, EventArgs e)
        {
            oIView.dtDestino = oIGestCat.GetAeropuertosDestino(oIView.sFiltroD, oIView.sAeropuertoO ,oIView.iTipoFiltro);
        }
        protected void GetDatosContrato_Presente(object sender, EventArgs e)
        {
            try
            {
                
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        protected void eGetCalculoPresupuesto_Presenter(object sender, EventArgs e)
        {
            DataTable dtServ = new DBRemision().DBGetConsultaConceptosServiciosVuelo;
            
            DatosRemision odRem = new DBRemision().DBGetObtieneDatosRemision(0, oIView.iIdContrato);
            oIView.eSeCobraFerrys = odRem.eSeCobraFerry;
            odRem.dtTramos = oIView.dtTramos;
            odRem.sRuta = oIView.sRutaTramos;
            odRem.iIdGrupoModeloPres = oIView.iIdGrupoModelo;
            odRem.sIdGrupoModeloPres = oIView.sIdGrupoModeloPre;
            odRem.bPermiteDoblePresupuesto = true;

            DataTable dtPres2 = new DataTable();
            DataTable dtCon = Utils.CalculaCostosRemisionPresupuestos(odRem, ref dtPres2, oIView.iIdTipoAeropuerto, oIView.oPresupuesto).Copy();

            dtServ = Utils.CalculoServiciosCargoPresupuestos(oIView.iIdContrato, odRem.dtTramos, odRem.iIdGrupoModeloPres);
            oIView.dIvaSV = Utils.CalculaIVAServiciosPresupuestos(odRem.dtTramos);


            if (dtPres2.Rows.Count > 0)
            {
                oIView.MuestraTextoDosPresupuestos("Existe un segundo presupuesto, favor de verificarlo.");

                //APLICO DOS PRESUPUESTOS
                DataTable dtServ2 = Utils.CalculoServiciosCargoPresupuestos(oIView.iIdContrato, dtPres2, odRem.iIdGrupoModeloPres);

                decimal dSuma2 = 0;
                DataRow[] drSub2 = dtServ2.Select("ServicioConCargoDescripcion = 'SubTotal'");
                if (drSub2.Length == 0)
                {
                    DataRow drS = dtServ2.NewRow();
                    dSuma2 = SumaImportesdeTabla(dtServ2, "Importe");
                    drS["IdServicioConCargo"] = 999999;
                    drS["ServicioConCargoDescripcion"] = "SubTotal";
                    drS["Importe"] = dSuma2;
                    dtServ2.Rows.Add(drS);
                }
                else
                {
                    dSuma2 = SumaImportesdeTabla(dtServ2, "Importe");
                    drSub2[0]["Importe"] = dSuma2;
                }
                oIView.dSubTotSC2 = dSuma2;

                dtPres2 = Utils.AplicaFactoresATiemposRemisionPresupuestos(dtPres2, odRem, oIView.iIdContrato);
                DataTable dtConceptos2 = Utils.CalculaCostosRemisionPresupuestos(odRem.iCobroTiempo, dtPres2, odRem);
                oIView.LoadServiciosDosPresupuestos(dtConceptos2, dtServ2, dtPres2);
            }

            decimal dSuma = 0;
            DataRow[] drSub = dtServ.Select("ServicioConCargoDescripcion = 'SubTotal'");
            if (drSub.Length == 0)
            {
                DataRow drS = dtServ.NewRow();
                dSuma = SumaImportesdeTabla(dtServ, "Importe");
                drS["IdServicioConCargo"] = 999999;
                drS["ServicioConCargoDescripcion"] = "SubTotal";
                drS["Importe"] = dSuma;
                dtServ.Rows.Add(drS);
            }
            else
            {
                dSuma = SumaImportesdeTabla(dtServ, "Importe");
                drSub[0]["Importe"] = dSuma;
            }


            oIView.dSubTotSC = dSuma;
            oIView.oDatos = odRem;
            oIView.LoadServiciosC(dtCon, dtServ, odRem.dtTramos);
        }
        protected void eGetReCalculoPresupuesto_Presenter(object sender, EventArgs e)
        {
            DataTable dtServ = new DBRemision().DBGetConsultaConceptosServiciosVuelo;

            DatosRemision odRem = new DBRemision().DBGetObtieneDatosRemision(0, oIView.iIdContrato);
            oIView.eSeCobraFerrys = odRem.eSeCobraFerry;
            odRem.dtTramos = oIView.dtTramos;
            odRem.sRuta = oIView.sRutaTramos;
            odRem.iIdGrupoModeloPres = oIView.iIdGrupoModelo;
            odRem.sIdGrupoModeloPres = oIView.sIdGrupoModeloPre;

            DataTable dtPres2;

            if (oIView.dtTramos2 != null)
                dtPres2 = oIView.dtTramos2;
            else
                dtPres2 = new DataTable();

            DataTable dtCon = Utils.CalculaCostosRemisionPresupuestos(odRem.iCobroTiempo, odRem.dtTramos, odRem);

            dtServ = Utils.CalculoServiciosCargoPresupuestos(oIView.iIdContrato, odRem.dtTramos, odRem.iIdGrupoModeloPres);
            oIView.dIvaSV = Utils.CalculaIVAServiciosPresupuestos(odRem.dtTramos);

            #region PRESUPUESTO 2
            if (dtPres2.Rows.Count > 0)
            {
                oIView.MuestraTextoDosPresupuestos("Existe un segundo presupuesto, favor de verificarlo.");

                //APLICO DOS PRESUPUESTOS
                DataTable dtConceptos2 = Utils.CalculaCostosRemisionPresupuestos(odRem.iCobroTiempo, dtPres2, odRem);
                DataTable dtServ2 = Utils.CalculoServiciosCargoPresupuestos(oIView.iIdContrato, dtPres2, odRem.iIdGrupoModeloPres);

                decimal dSuma2 = 0;
                DataRow[] drSub2 = dtServ2.Select("ServicioConCargoDescripcion = 'SubTotal'");
                if (drSub2.Length == 0)
                {
                    DataRow drS = dtServ2.NewRow();
                    dSuma2 = SumaImportesdeTabla(dtServ2, "Importe");
                    drS["IdServicioConCargo"] = 999999;
                    drS["ServicioConCargoDescripcion"] = "SubTotal";
                    drS["Importe"] = dSuma2;
                    dtServ2.Rows.Add(drS);
                }
                else
                {
                    dSuma2 = SumaImportesdeTabla(dtServ2, "Importe");
                    drSub2[0]["Importe"] = dSuma2;
                }
                oIView.dSubTotSC = dSuma2;

                oIView.LoadServiciosDosPresupuestos(dtConceptos2, dtServ2, dtPres2);
            }
            #endregion

            decimal dSuma = 0;
            DataRow[] drSub = dtServ.Select("ServicioConCargoDescripcion = 'SubTotal'");
            if (drSub.Length == 0)
            {
                DataRow drS = dtServ.NewRow();
                dSuma = SumaImportesdeTabla(dtServ, "Importe");
                drS["IdServicioConCargo"] = 999999;
                drS["ServicioConCargoDescripcion"] = "SubTotal";
                drS["Importe"] = dSuma;
                dtServ.Rows.Add(drS);
            }
            else
            {
                dSuma = SumaImportesdeTabla(dtServ, "Importe");
                drSub[0]["Importe"] = dSuma;
            }
            oIView.dSubTotSC = dSuma;

            
            oIView.oDatos = odRem;
            oIView.LoadServiciosC(dtCon, dtServ, odRem.dtTramos);
        }
        private decimal SumaImportesdeTabla(DataTable dt, string sColumna)
        {
            try
            {
                decimal dSuma = 0;

                foreach (DataRow row in dt.Rows)
                {
                    dSuma += row[sColumna].S().D();
                }

                return dSuma;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected override void NewObj_Presenter(object sender, EventArgs e)
        {
            try
            {
                Presupuesto oPres = oIView.oPresupuesto;
                oIGestCat.DBSetInsertaPresupuesto(oPres);
                if (oPres.iIdPresupuesto > 0)
                {
                    oIView.iIdPresupuesto = oPres.iIdPresupuesto;
                    oIView.sMensaje = string.Format("Se guardo la cotización con Folio: {0}.", oPres.iIdPresupuesto.S());
                    
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected override void ObjSelected_Presenter(object sender, EventArgs e)
        {
            int iPres = oIView.iIdPresupuesto;
            Presupuesto oPres = oIGestCat.DBGetConsultaPresupuestoId(iPres);
            oIView.oPresupuesto = oPres;
        }
        protected override void SaveObj_Presenter(object sender, EventArgs e)
        {
            Presupuesto oPres = oIView.oPresupuesto;
            oIGestCat.DBSetActualizaPresupuesto(oPres);
            oIView.sMensaje = string.Format("Se guardo la cotización con Folio: {0}.", oPres.iIdPresupuesto.S());
        }
        public void LoadViabilidad_Presenter(object sender, EventArgs e)
        {
            DataTable DT = oIGestCat.DBViabilidad(sender);

            if (DT != null && DT.Rows.Count > 0)
                oIView.bViabilidad = Convert.ToBoolean(DT.Rows[0][0]);

        }
        public void NewMonitorDespacho_Presenter(object sender, EventArgs e)
        {
            try
            {
                int id = oIGestCat.DBGurdaMonitorDespacho(oIView.oSolicitudVuelo);
                if (id > 0)
                {
                    //oIView.MostrarMensaje(Mensajes.LMensajes[Enumeraciones.Mensajes.Insercion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
                }
            }
            catch (Exception ex)
            {
                oIView.MostrarMensaje("Se detectó el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
            }
        }
        public void GuardaSeguimiento_Presenter(object sender, EventArgs e)
        {
            try
            {
                int id = oIGestCat.DBGuardaSeguimiento(oIView.oGuardaSeguimiento);
                if (id > 0)
                {
                    //oIView.MostrarMensaje(Mensajes.LMensajes[Enumeraciones.Mensajes.Insercion._I()].sMensaje, Mensajes.LMensajes[Enumeraciones.Mensajes.Aviso._I()].sMensaje);
                }
            }
            catch (Exception ex)
            {
                oIView.MostrarMensaje("Se detectó el siguiente error: " + ex.Message, "ERROR DE SISTEMA");
            }
        }
        public void GuardaSolicitud_Presenter(object sender, EventArgs e)
        {

            SolicitudVuelo oSol = oIView.oSolicitudVuelo;
            int iIdSolicitud = new DBSolicitudesVuelo().DBSave(oSol);

            if (iIdSolicitud > 0)
            {
                oSol.iIdSolicitud = iIdSolicitud;
                oIView.oSolicitudVuelo = oSol;

                TramoSolicitud objTramo = new TramoSolicitud();
                List<TramoSolicitud> lstObjTramos = new List<TramoSolicitud>();
                foreach (DataRow oRow in oIView.oPresupuesto.dtTramos.Rows)
                {
                    objTramo = new TramoSolicitud();
                    objTramo.iIdAeropuertoO = oRow["IdOrigen"].S().I();
                    objTramo.iIdAeropuertoD = oRow["IdDestino"].S().I();
                    objTramo.dFechaVuelo = oRow["FechaSalida"].S();
                    objTramo.sHoraVuelo = oRow["TiempoVuelo"].S();
                    objTramo.iNoPax = oRow["CantPax"].S().I();
                    objTramo.iIdSolicitud = iIdSolicitud;
                    lstObjTramos.Add(objTramo);
                }

                // Elimina Tramos Existentes
                EliminaTramosSolicitud(oSol);

                // Guarda Tramos
                foreach (TramoSolicitud oTramo in lstObjTramos)
                {
                    oIGestCat.DBSaveTramo(oTramo);
                }


                oIView.lstObjTramos = lstObjTramos;
                oIView.ObtieneViabilidad();


                //string sMensaje = string.Empty;
                //sMensaje = "Mensaje de Prueba";

                //if (sMensaje.Length > 0)
                //{
                //    sMensaje = string.Format("{0} {1} Se guardo la solicitud: {2}", sMensaje, System.Environment.NewLine, iIdSolicitud);
                //}
                //else
                //{
                //    sMensaje = string.Format("Se guardo la solicitud: {0}", iIdSolicitud);
                //}

                //oIView.MostrarMensaje(sMensaje, "Aviso");
            }
        }
        private void EliminaTramosSolicitud(SolicitudVuelo oSol)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = oIGestCat.DBGetTramosSolicitudes(oSol);
                foreach (DataRow dr in dt.Rows)
                {
                    oIGestCat.DBEliminaTramoSol(dr["IdTramo"].S().I());

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// DOBLE PRESUPUESTO
        protected void GetCalculoPresupuesto2_Presenter(object sender, EventArgs e)
        {
            DataTable dtServ = new DBRemision().DBGetConsultaConceptosServiciosVuelo;

            DatosRemision odRem = new DBRemision().DBGetObtieneDatosRemision(0, oIView.iIdContrato);
            oIView.eSeCobraFerrys = odRem.eSeCobraFerry;
            odRem.dtTramos = oIView.dtTramos2;
            odRem.sRuta = oIView.sRutaTramos2;
            odRem.iIdGrupoModeloPres = oIView.iIdGrupoModelo;
            odRem.sIdGrupoModeloPres = oIView.sIdGrupoModeloPre;
            odRem.bPermiteDoblePresupuesto = false;

            DataTable dtPres1 = new DataTable();
            DataTable dtCon = Utils.CalculaCostosRemisionPresupuestos(odRem, ref dtPres1, oIView.iIdTipoAeropuerto, oIView.oPresupuesto).Copy();

            dtServ = Utils.CalculoServiciosCargoPresupuestos(oIView.iIdContrato, odRem.dtTramos, odRem.iIdGrupoModeloPres);
            oIView.dIvaSV = Utils.CalculaIVAServiciosPresupuestos(odRem.dtTramos);

            decimal dSuma = 0;
            DataRow[] drSub = dtServ.Select("ServicioConCargoDescripcion = 'SubTotal'");
            if (drSub.Length == 0)
            {
                DataRow drS = dtServ.NewRow();
                dSuma = SumaImportesdeTabla(dtServ, "Importe");
                drS["IdServicioConCargo"] = 999999;
                drS["ServicioConCargoDescripcion"] = "SubTotal";
                drS["Importe"] = dSuma;
                dtServ.Rows.Add(drS);
            }
            else
            {
                dSuma = SumaImportesdeTabla(dtServ, "Importe");
                drSub[0]["Importe"] = dSuma;
            }

            oIView.dSubTotSC2 = dSuma;
            oIView.LoadServiciosDosPresupuestos(dtCon, dtServ, odRem.dtTramos);
        }
        protected void GetReCalculoPresupuesto2_Presenter(object sender, EventArgs e)
        {
            DataTable dtServ = new DBRemision().DBGetConsultaConceptosServiciosVuelo;

            DatosRemision odRem = new DBRemision().DBGetObtieneDatosRemision(0, oIView.iIdContrato);
            oIView.eSeCobraFerrys = odRem.eSeCobraFerry;
            odRem.dtTramos = oIView.dtTramos;
            odRem.sRuta = oIView.sRutaTramos;
            odRem.iIdGrupoModeloPres = oIView.iIdGrupoModelo;
            odRem.sIdGrupoModeloPres = oIView.sIdGrupoModeloPre;

            DataTable dtPres2 = oIView.dtTramos2;

            #region PRESUPUESTO 2
            if (dtPres2.Rows.Count > 0)
            {
                oIView.MuestraTextoDosPresupuestos("Existe un segundo presupuesto, favor de verificarlo.");

                //APLICO DOS PRESUPUESTOS
                DataTable dtConceptos2 = Utils.CalculaCostosRemisionPresupuestos(odRem.iCobroTiempo, dtPres2, odRem);
                DataTable dtServ2 = Utils.CalculoServiciosCargoPresupuestos(oIView.iIdContrato, dtPres2, odRem.iIdGrupoModeloPres);

                decimal dSuma2 = 0;
                DataRow[] drSub2 = dtServ2.Select("ServicioConCargoDescripcion = 'SubTotal'");
                if (drSub2.Length == 0)
                {
                    DataRow drS = dtServ2.NewRow();
                    dSuma2 = SumaImportesdeTabla(dtServ2, "Importe");
                    drS["IdServicioConCargo"] = 999999;
                    drS["ServicioConCargoDescripcion"] = "SubTotal";
                    drS["Importe"] = dSuma2;
                    dtServ2.Rows.Add(drS);
                }
                else
                {
                    dSuma2 = SumaImportesdeTabla(dtServ2, "Importe");
                    drSub2[0]["Importe"] = dSuma2;
                }
                oIView.dSubTotSC = dSuma2;

                oIView.LoadServiciosDosPresupuestos(dtConceptos2, dtServ2, dtPres2);
            }
            #endregion

        }
    }
}