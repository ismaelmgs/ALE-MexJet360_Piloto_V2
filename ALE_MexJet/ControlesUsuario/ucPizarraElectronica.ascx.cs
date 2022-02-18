using ALE_MexJet.DomainModel;
using ALE_MexJet.Objetos;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NucleoBase.Core;
using ALE_MexJet.Clases;

namespace ALE_MexJet.ControlesUsuario
{
    public partial class ucPizarraElectronica : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void gvFactoresDetalle_BeforePerformDataSelect(object sender, EventArgs e)
        {
            try
            {
                ASPxGridView grid = sender as ASPxGridView;

                int iIdPadre = grid.GetMasterRowKeyValue().S().I();

                ConsultaDetalle(iIdPadre);
                grid.DataSource = dtFactoresTramo;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ShowPizarra()
        {
            try
            {
                LlenaTitulos();
                if (IdRemision > 0)
                {
                    oSnap = new DBPizarra().DBGetObtieneSnapshotRemision(IdRemision);
                    if (oSnap.oDatosRem.lIdRemision == -1)
                    {
                        oSnap = (SnapshotRemision)Session["SnapshotRem"];
                    }
                }

                if (oSnap != null)
                {
                    LlenaModalSnapshotContrato();
                }

                pcPizarra.ShowOnPageLoad = true;
                pcPizarra.CloseAnimationType = DevExpress.Web.AnimationType.Fade;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LlenaTitulos()
        {
            try
            {
                DataTable dt = new DBPizarra().DBGetObtieneTitulosPizarra();
                foreach (DataRow item in dt.Rows)
                {
                    Control ctl = FindControlRecursive(pcPizarra, item["NombreLabel"].S());

                    if (ctl != null)
                    {
                        if (ctl is ASPxLabel)
                            ((ASPxLabel)ctl).Text = item["TextoLabel"].S();
                        else if (ctl is Label)
                            ((Label)ctl).Text = item["TextoLabel"].S();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Control FindControlRecursive(Control Root, string Id)
        {
            if (Root.ID == Id)
                return Root;

            foreach (Control Ctl in Root.Controls)
            {
                Control FoundCtl = FindControlRecursive(Ctl, Id);
                if (FoundCtl != null)
                {
                    return FoundCtl;
                }
            }

            return null;
        }

        public void LlenaModalSnapshotContrato()
        {
            try
            {
                // Generales
                lblSnapClaveCliente.Text = oSnap.oDatosRem.sClaveCliente;
                lblSnapClaveContrato.Text = oSnap.oDatosRem.sClaveContrato;

                lblSnapTipoContrato.Text = oSnap.oDatosRem.sTipoPaquete;
                lblSnapGrupoModelo.Text = oSnap.oDatosRem.sGrupoModeloDesc;

                lblSnapFolioRem.Text = oSnap.oDatosRem.lIdRemision.S();
                lblSnapTiempoCobrar.Text = oSnap.oDatosRem.sTotalTiempoCalzo;
                lblSnapMasMinutos.Text = oSnap.oDatosRem.iMasMinutos.S();
                lblSnapAplicaTramPact.Text = oSnap.oDatosRem.bTiemposPactados ? "Si" : "No";
                lblSnapHorasContratadasTotal.Text = oSnap.oDatosRem.iHorasContratadasTotal.S();
                lblSnapHorasContratadasAnio.Text = oSnap.oDatosRem.iHorasContratadasAnio.S();

                lblSnapSeCobraComb.Text = oSnap.oDatosRem.bSeCobraCombustible ? "Si" : "No";
                //lblFormaCobroCombustible.Text = FormaCobroCombustible(oSnap.oDatosRem.eCobroCombustible);
                lblSnapFactorTramosNales.Text = oSnap.oDatosRem.dFactorTramosNal.S();
                lblSnapFactorTramosInter.Text = oSnap.oDatosRem.dFactorTramosInt.S();
                lblSnapCostoDirVueloNal.Text = oSnap.oDatosRem.dVueloCostoDirNal.ToString("c");
                lblSnapCostoDirVueloInt.Text = oSnap.oDatosRem.dVueloCostoDirInt.ToString("c");

                lblSnapSeCobraTiempoEspera.Text = oSnap.oDatosRem.bSeCobreEspera ? "Si" : "No";
                lblSnapTarifaEspNal.Text = oSnap.oDatosRem.dTarifaNalEspera.ToString("c");
                lblSnapTarifaEspInt.Text = oSnap.oDatosRem.dTarifaIntEspera.ToString("c");
                lblSnapPorcentajeTarEspNal.Text = oSnap.oDatosRem.dPorTarifaNalEspera.S();
                lblSnapPorcentajeTarEspInt.Text = oSnap.oDatosRem.dPorTarifaIntEspera.S();
                lblSnapSeCobranPernoctas.Text = oSnap.oDatosRem.bSeCobraPernoctas ? "Si" : "No";

                lblSnapTarifaDllsPerNal.Text = oSnap.oDatosRem.dTarifaDolaresNal.ToString("c");
                lblSnapPorcentajeTarDllsPerNal.Text = oSnap.oDatosRem.dPorTarifaVueloNal.S();
                lblSnapTarifaDllsPerInt.Text = oSnap.oDatosRem.dTarifaDolaresInt.ToString("c");
                lblSnapPorcentajeTarDllsPerInt.Text = oSnap.oDatosRem.dPorTarifaVueloInt.S();

                lblSnapAplicaVloSimultaneo.Text = oSnap.oDatosRem.bAplicaFactorVueloSimultaneo ? "Si" : "No";
                lblSnapCuantosVuelosSim.Text = oSnap.oDatosRem.iCuantosVloSimultaneo.S();
                lblSnapFactorVuelosSim.Text = oSnap.oDatosRem.dFactorVloSimultaneo.S();
                lblSnapSeDescuentaEspNal.Text = oSnap.oDatosRem.bSeDescuentaEsperaNal ? "Si" : "No";
                lblSnapSeDescuentaEspInt.Text = oSnap.oDatosRem.bSeDescuentaEsperaInt ? "Si" : "No";
                lblSnapFactorEsperaHoraVueloNal.Text = oSnap.oDatosRem.dPorTarifaNalEspera.S(); //**//
                lblSnapFactorEsperaHoraVueloInt.Text = oSnap.oDatosRem.dPorTarifaIntEspera.S(); //**//

                lblSnapSeDescuentaPernoctaNal.Text = oSnap.oDatosRem.bSeDescuentanPerNal ? "Si" : "No";
                lblSnapSeDescuentaPernoctaInt.Text = oSnap.oDatosRem.bSeDescuentanPerInt ? "Si" : "No";
                lblSnapFactorPerHoraVueloNal.Text = oSnap.oDatosRem.dFactorEHrVueloNal.S();
                lblSnapFactorPerHoraVueloInt.Text = oSnap.oDatosRem.dFactorEHrVueloInt.S();
                    
                // Cobros Espera y Pernoctas
                lblSnapSeCobraTiempoEspera2.Text = oSnap.oDatosRem.bSeCobreEspera ? "Si" : "No";
                lblSnapHorasPernocta.Text = oSnap.oDatosRem.iHorasPernocta.S();
                lblSnapTiempoEsperaGeneral.Text = oSnap.oDatosRem.sTiempoEsperaGeneral;
                lblSnapTiempoVueloGeneral.Text = oSnap.oDatosRem.sTiempoVueloGeneral;
                lblSnapFactorHrVuelo.Text = oSnap.oDatosRem.dFactorHrVuelo.S();
                lblSnapTotalTiempoEsperaCobrar.Text = oSnap.oDatosRem.sTotalTiempoEsperaCobrar;


                gvFactoresRem.DataSource = oSnap.oFactoresTramos.ConvertListToDataTable();
                gvFactoresRem.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ConsultaDetalle(int iIdPadre)
        {
            //oSnap = (SnapshotRemision)Session["SnapshotRem"];
            DataRow[] rows = oSnap.oFactoresTramos.ConvertListToDataTable().Select("iNoTramo = " + iIdPadre.S());

            foreach(DataRow item in rows)
            {
                item["sFactorEspeciaRem"] = (double)item["dFactorEspeciaRem"] > 0 ? item["dFactorEspeciaRem"].ToString() : "--";
                item["sAplicaFactorTramoInternacional"] = (double)item["dAplicaFactorTramoInternacional"] > 0 ? item["dAplicaFactorTramoInternacional"].ToString() : "--";
                item["sAplicaFactorTramoNacional"] = (double)item["dAplicaFactorTramoNacional"] > 0 ? item["dAplicaFactorTramoNacional"].ToString() : "--";
                item["sAplicaGiraEspera"] = (double)item["dAplicaGiraEspera"] > 0 ? item["dAplicaGiraEspera"].ToString() : "--";
                item["sAplicoIntercambio"] = (double)item["dAplicoIntercambio"] > 0 ? item["dAplicoIntercambio"].ToString() : "--";
                item["sAplicaGiraHorario"] = (double)item["dAplicaGiraHorario"] > 0 ? item["dAplicaGiraHorario"].ToString() : "--";
                item["sAplicaFactorFechaPico"] = (double)item["dAplicaFactorFechaPico"] > 0 ? item["dAplicaFactorFechaPico"].ToString() : "--";
                item["sAplicaFactorVueloSimultaneo"] = (double)item["dAplicaFactorVueloSimultaneo"] > 0 ? item["dAplicaFactorVueloSimultaneo"].ToString() : "--";
            }

            dtFactoresTramo = new DataTable();
            dtFactoresTramo = rows.CopyToDataTable();
        }

        private long _IdRemision = 0;
        public long IdRemision { get => _IdRemision; set => _IdRemision = value; }

        //private SnapshotRemision _oSnap = new SnapshotRemision();
        //public SnapshotRemision oSnap { get => _oSnap; set => _oSnap = value; }

        public SnapshotRemision oSnap
        {
            get { return (SnapshotRemision)Session["SSSnap"]; }
            set { Session["SSSnap"] = value; }
        }

        public DataTable dtFactoresTramo
        {
            get { return (DataTable)ViewState["VSDTFactoresTramo"]; }
            set { ViewState["VSDTFactoresTramo"] = value; }
        }

        public SnapshotRemision oSnapShot
        {
            get { return (SnapshotRemision)ViewState["VSSnapshot"]; }
            set { ViewState["VSSnapshot"] = value; }
        }
    }
}