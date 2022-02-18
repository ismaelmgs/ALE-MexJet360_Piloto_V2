using ALE_MexJet.DomainModel;
using ALE_MexJet.Interfaces;
using ALE_MexJet.Presenter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NucleoBase.Core;
using System.Reflection;
using ALE_MexJet.Objetos;
using DevExpress.Web;
using ALE_MexJet.ControlesUsuario;
using ALE_MexJet.Clases;
using System.ComponentModel;
using DevExpress.Web.Data;
using DevExpress.XtraPrinting;
using System.Text;
using System.IO;
using DevExpress.Utils;
using System.Drawing;

namespace ALE_MexJet.Views.Rentas
{
    public partial class frmPresupuesto : System.Web.UI.Page, iViewPresupuesto
    {

        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                mpeMensaje.OkButtonPressed += mpeMensaje_OkButtonPressed;
                oPresenter = new Presupuesto_Presenter(this, new DBPresupuesto());
                if (!IsPostBack)
                {
                    ObtieneValores();
                    txtDgDiasVigencia.Text = Utils.GetParametrosClave("58");
                    dteDgFechaPresupuesto.Date = DateTime.Now;
                    dteDgFechaPresupuesto.MinDate = DateTime.Now;
                    dteDgFechaPresupuesto.ReadOnly = true;
                    dteDgFechaPresupuesto.DropDownButton.Enabled = false;

                    if (Request.QueryString.Count > 0)
                    {
                        string sContrato = Request.QueryString["Presupuesto"];
                        if (!string.IsNullOrEmpty(sContrato))
                        {
                            iIdPresupuesto = Encoding.UTF8.GetString(Convert.FromBase64String(Request.QueryString["Presupuesto"])).S().I();
                        }
                        sAccionRecibido = Encoding.UTF8.GetString(Convert.FromBase64String(Request.QueryString["Accion"]));

                        switch (sAccionRecibido)
                        {
                            case "Nuevo":
                                iIdPresupuesto = 0;
                                iIdSolicitudVuelo = 0;
                                break;
                            case "Editar":
                                CargaDatosPresupuesto();
                                break;
                            case "Consultar":
                                CargaDatosPresupuesto();
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        void mpeMensaje_OkButtonPressed(object sender, EventArgs args)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(System.Web.UI.Page), "OcultaError", "OcultaError();", true);
        }
        protected void gvTramos_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        protected void gvServiciosC_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ASPxTextBox txtImporte = (ASPxTextBox)e.Row.FindControl("txtImporte");
                if (txtImporte != null)
                {
                    txtImporte.Text = dtServiciosC.Rows[e.Row.RowIndex]["Importe"].S();
                }

                if (dtServiciosC.Rows[e.Row.RowIndex][1].S() == "SubTotal")
                {
                    ASPxButton btn = (ASPxButton)e.Row.FindControl("btneliminarsc");
                    if (btn != null)
                        btn.Visible = false;
                }
            }
        }
        protected void txtImporte_TextChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow row in gvServiciosC.Rows)
                {
                    if (row.Cells[0].Text != "SubTotal")
                    {
                        foreach (DataRow dr in dtServiciosC.Rows)
                        {
                            if (dr.S("ServicioConCargoDescripcion") == row.Cells[0].Text.S())
                            {
                                ASPxTextBox txt = (ASPxTextBox)row.FindControl("txtImporte");
                                if (txt != null)
                                {
                                    dr["Importe"] = txt.Text.S().D();
                                }
                            }
                        }
                    }
                }

                decimal dSuma = 0;

                dtServiciosC.Rows.RemoveAt(dtServiciosC.Rows.Count - 1);


                DataRow[] drSub = dtServiciosC.Select("ServicioConCargoDescripcion = 'SubTotal'");
                if (drSub.Length == 0)
                {
                    DataRow drS = dtServiciosC.NewRow();
                    dSuma = SumaImportesdeTabla(dtServiciosC, "Importe");
                    drS["IdServicioConCargo"] = 999999;
                    drS["ServicioConCargoDescripcion"] = "SubTotal";
                    drS["Importe"] = dSuma;
                    dtServiciosC.Rows.Add(drS);
                }
                else
                {
                    dSuma = SumaImportesdeTabla(dtServiciosC, "Importe");
                    drSub[0]["Importe"] = dSuma;
                }

                dSubTotSC = dSuma;


                decimal dIva = 0;
                dIva = Utils.GetParametrosClave("9").S().D();

                decimal dServiciosCM = 0;
                decimal dIvaServCM = 0;
                foreach (DataRow row in dtImportes.Rows)
                {
                    if (row["Concepto"].S() == "TOTAL SERVICIOS CON CARGO")
                    {
                        int iTipoMonedaPres = cboDgMonedaPresupueto.SelectedItem.Value.S().I();
                        decimal dDividendoSC = iTipoMonedaPres == 1 ? dTipoCambioDia : 1;
                        dServiciosCM = (dSubTotSC / dDividendoSC);
                        row["Importe"] = dServiciosCM;
                    }

                    if (row["Concepto"].S() == "IVA SERVICIOS CON CARGO")
                    {
                        int iTipoMonedaPres = cboDgMonedaPresupueto.SelectedItem.Value.S().I();
                        decimal dDividendoSC = iTipoMonedaPres == 1 ? dTipoCambioDia : 1;
                        dIvaServCM = (dSubTotSC / dDividendoSC) * dIva;

                        row["Importe"] = dIvaServCM;
                    }

                    if (row["Concepto"].S() == "TOTAL PRESUPUESTO")
                    {
                        //decimal dTotalPres = 0;

                        //int iTipoMonedaPres = cboDgMonedaPresupueto.SelectedItem.Value.S().I();
                        //decimal dDividendoSC = iTipoMonedaPres == 2 ? dTipoCambioDia : 1;

                        //decimal dServiciosVP = 0;
                        //decimal dIvaServiciosVP = 0;

                        //dServiciosVP = dSubTotSV * dDividendoSC;
                        //dIvaServiciosVP = (dSubTotSV * dIvaSV) * dDividendoSC;

                        //dTotalPres = dServiciosVP;
                        //dTotalPres += dIvaServiciosVP;
                        //dTotalPres += dServiciosCM;
                        //dTotalPres += dIvaServCM;

                        //row["Importe"] = dTotalPres;
                        row["Importe"] = SumaTotalPresupuesto(dtImportes);
                    }
                }

                gvConceptos.DataSource = dtImportes;
                gvConceptos.DataBind();

                PintaTotalesConceptos();

                gvServiciosC.DataSource = dtServiciosC;
                gvServiciosC.DataBind();
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "txtImporte_TextChanged", "Aviso");
            }
        }
        protected void gvServiciosC_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
        protected void cboGgClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (eGetDatosCliente != null)
                    eGetDatosCliente(sender, e);

                cboDgSolicitante.DataSource = dtContactos;
                cboDgSolicitante.ValueField = "IdContacto";
                cboDgSolicitante.TextField = "Nombre";
                cboDgSolicitante.DataBind();

                cboDgContrato.DataSource = dtContrato;
                cboDgContrato.ValueField = "IdContrato";
                cboDgContrato.TextField = "ClaveContrato";
                cboDgContrato.DataBind();

                DataRow[] row = dtClientes.Select("IdCliente = " + cboGgClientes.Value.S());
                lbldgCompañiaregisrada.Text = row[0]["Nombre"].S();
                txtDgCompaniaImpresion.Text = row[0]["Nombre"].S();
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "cboGgClientes_SelectedIndexChanged", "Aviso");
            }

        }
        protected void cboDgContrato_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                DataRow[] row = dtContrato.Select("IdContrato = " + cboDgContrato.Value.S());

                lblDgTipoPaquete.Text = row[0]["DescripcionPaquete"].S();
                lblDgGrupoModelo.Text = row[0]["GrupoModeloDesc"].S();

                DatosRemision oR = new DBRemision().DBGetObtieneDatosRemision(0, cboDgContrato.Value.S().I());
                eSeCobraFerrys = oR.eSeCobraFerry;

                oR.Dispose();
            }
            catch (Exception ex)
            {

            }
        }
        protected void imbDelete_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ImageButton txtCambio = (ImageButton)sender;
                GridViewRow rowSelect = (GridViewRow)((txtCambio).NamingContainer);

                dtTramos.Rows.RemoveAt(rowSelect.RowIndex);

                if (dtTramos.Rows.Count > 0)
                {
                    RecargaGridTramosPasoTres();
                    ReCalculaPresupuesto();
                }
                else
                {
                    gvTramosOpc1.DataSource = null;
                    gvTramosOpc1.DataBind();

                    gvServiciosC.DataSource = null;
                    gvServiciosC.DataBind();

                    gvConceptos.DataSource = null;
                    gvConceptos.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void btnAgregaPierna_Click(object sender, EventArgs e)
        {
            try
            {
                hdOpcPresupuesto.Value = "1";

                if (dtTramos == null)
                    CreaEstructuraTramos();

                ddlOrigenFV.Value = null;
                ddlDestinoFV.Value = null;
                spePax.Text = "0";
                txtFechaSalidaFV.Text = string.Empty;
                txtFechaLlegadaFV.Text = string.Empty;
                txtTiempoVueloFV.Text = string.Empty;


                ddlOrigenFV.IsValid = true;
                ddlDestinoFV.IsValid = true;
                txtFechaSalidaFV.IsValid = true;
                txtFechaLlegadaFV.IsValid = true;
                txtTiempoVueloFV.IsValid = true;


                ppTramos.ShowOnPageLoad = true;
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnAgregaPierna_Click", "Aviso");
            }
        }
        protected void txtFechaSalida_DateChanged(object sender, EventArgs e)
        {
            try
            {
                DateTime FechaSalida;
                DateTime FechaLlegada;
                ASPxDateEdit txt = (ASPxDateEdit)sender;
                GridViewRow Row = (GridViewRow)txt.NamingContainer;
                ASPxDateEdit txtLlegada = (ASPxDateEdit)Row.FindControl("txtFechaLlegada");

                if (txt != null)
                {
                    FechaSalida = txt.Value.Dt();
                    FechaLlegada = txtLlegada.Value.Dt();

                    if (FechaSalida > FechaLlegada)
                    {
                        AjustaTextBox(txt, System.Drawing.Color.Red, "La fecha de salida no puede ser mayor que la de llegada");
                        txt.Focus();
                    }
                    else
                    {
                        AjustaTextBox(txt, System.Drawing.Color.Black, "Fecha correcta.");

                        TimeSpan ts = FechaLlegada.AddSeconds(FechaLlegada.Second * -1) - FechaSalida.AddSeconds(FechaSalida.Second * -1);

                        dtTramos.Rows[Row.RowIndex]["TiempoVuelo"] = ts.S().Length == 8 ? ts.S().Substring(0,5) : ts.S();
                        dtTramos.Rows[Row.RowIndex]["FechaSalida"] = FechaSalida;

                        if (dtTramos.Rows[Row.RowIndex].S("Destino") == dtTramos.Rows[0].S("Origen"))
                        {
                            dtTramos.Rows[Row.RowIndex]["TiempoEspera"] = "00:00";
                        }

                        DefineFerryCobrar();
                        ActualizatiempoEspera();
                        RecargaGridTramosPasoTres();
                        CalculaPresupuesto();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void txtFechaLlegada_DateChanged(object sender, EventArgs e)
        {
            try
            {
                DateTime FechaSalida;
                DateTime FechaLlegada;
                ASPxDateEdit txt = (ASPxDateEdit)sender;
                GridViewRow Row = (GridViewRow)txt.NamingContainer;
                ASPxDateEdit txtSalida = (ASPxDateEdit)Row.FindControl("txtFechaSalida");

                if (txt != null)
                {
                    FechaSalida = txtSalida.Value.Dt();
                    FechaLlegada = txt.Value.Dt();

                    if (FechaSalida > FechaLlegada)
                    {
                        AjustaTextBox(txt, System.Drawing.Color.Red, "La fecha de salida no puede ser mayor que la de llegada");
                        txt.Focus();
                    }
                    else
                    {
                        AjustaTextBox(txt, System.Drawing.Color.Black, "Fecha correcta.");

                        TimeSpan ts = FechaLlegada.AddSeconds(FechaLlegada.Second * -1) - FechaSalida.AddSeconds(FechaSalida.Second * -1);

                        dtTramos.Rows[Row.RowIndex]["TiempoVuelo"] = ts.S().Length == 8 ? ts.S().Substring(0, 5) : ts.S();
                        dtTramos.Rows[Row.RowIndex]["FechaLlegada"] = FechaLlegada;

                        if (dtTramos.Rows[Row.RowIndex].S("Destino") == dtTramos.Rows[0].S("Origen"))
                        {
                            dtTramos.Rows[Row.RowIndex]["TiempoEspera"] = "00:00";
                        }

                        DefineFerryCobrar();
                        ActualizatiempoEspera();
                        RecargaGridTramosPasoTres();
                        CalculaPresupuesto();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void ddlOrigenFV_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            try
            {
                //Verificar si tiene pierna predecesora para obtener el ICAO y mandarlo como parametro.
                ASPxComboBox comboBox = (ASPxComboBox)source;
                sFiltroO = e.Filter;

                if (eLoadOrigDestFiltro != null)
                    eLoadOrigDestFiltro(source, e);

                CargaComboAeropuertosFV(comboBox, dtOrigen, sFiltroO);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "ddlOrigenFV_ItemsRequestedByFilterCondition", "Aviso");
            }
        }
        protected void ddlOrigenFV_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "ddlOrigenFV_ItemRequestedByValue", "Aviso");
            }
        }
        protected void ddlDestinoFV_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            try
            {
                //Verificar si tiene pierna predecesora para obtener el ICAO y mandarlo como parametro.
                ASPxComboBox comboBox = (ASPxComboBox)source;
                sFiltroD = e.Filter;

                if (eLoadOrigDestFiltroDest != null)
                    eLoadOrigDestFiltroDest(source, e);

                CargaComboAeropuertosFV(comboBox, dtDestino, sFiltroD);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "ddlDestinoFV_ItemsRequestedByFilterCondition", "Aviso");
            }
        }
        protected void ddlDestinoFV_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "ddlDestinoFV_ItemRequestedByValue", "Aviso");
            }
        }
        protected void txtFechaSalidaFV_DateChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dtTram = new DataTable();
                if (hdOpcPresupuesto.Value == "1")
                    dtTram = dtTramos.Copy();
                else
                    dtTram = dtTramos2.Copy();


                if (dtTram.Rows.Count > 0)
                {
                    string sIdOrigen = string.Empty;
                    string sIdDestinoA = string.Empty;

                    DateTime dtOrigen = new DateTime();
                    DateTime dtDestino = new DateTime();

                    sIdOrigen = ddlOrigenFV.Value.S();
                    sIdDestinoA = dtTram.Rows[dtTram.Rows.Count - 1]["IdDestino"].S();

                    dtOrigen = ((ASPxDateEdit)sender).Value.S().Dt();
                    dtDestino = dtTram.Rows[dtTram.Rows.Count - 1]["FechaLlegada"].S().Dt();

                    dtOrigen = dtOrigen.AddSeconds(dtOrigen.Second * -1);
                    dtDestino = dtDestino.AddSeconds(dtOrigen.Second * -1);


                    if (txtFechaSalidaFV.Text != string.Empty && dtOrigen < dtDestino)
                    {
                        txtFechaSalidaFV.IsValid = false;
                        txtFechaSalidaFV.ErrorText = "La fecha de salida debe ser mayor o igual a la fecha de llegada de la pierna anterior.";
                        txtFechaSalidaFV.ValidationSettings.ErrorTextPosition = ErrorTextPosition.Bottom;
                        txtFechaSalidaFV.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.Text;
                    }
                    else
                        txtFechaSalidaFV.IsValid = true;

                    if (ddlOrigenFV.IsValid && txtFechaSalidaFV.IsValid)
                    {
                        if (oDatosContrato == null)
                        {
                            oDatosContrato = new DBRemision().DBGetObtieneDatosRemision(0, iIdContrato);
                        }

                        if (oDatosContrato != null)
                        {
                            string sTiempo = Utils.ObtieneTiempoTramosPresupuestos(oDatosContrato, ddlOrigenFV.Value.S().I(), ddlDestinoFV.Value.S().I(), cboDgModeloSolicitado.Value.S().I());
                            txtTiempoVueloFV.Text = sTiempo.Substring(0, 5);

                            string[] sTiempos = sTiempo.Split(':');
                            DateTime dtLlegada = txtFechaSalidaFV.Text.S().Dt();

                            dtLlegada = dtLlegada.AddHours(sTiempos[0].S().I());
                            dtLlegada = dtLlegada.AddMinutes(sTiempos[1].S().I());

                            txtFechaLlegadaFV.Value = dtLlegada;
                        }
                    }
                }
                else
                {
                    if (oDatosContrato == null)
                    {
                        oDatosContrato = new DBRemision().DBGetObtieneDatosRemision(0, iIdContrato);
                    }

                    if (oDatosContrato != null)
                    {
                        string sTiempo = Utils.ObtieneTiempoTramosPresupuestos(oDatosContrato, ddlOrigenFV.Value.S().I(), ddlDestinoFV.Value.S().I(), cboDgModeloSolicitado.Value.S().I());
                        txtTiempoVueloFV.Text = sTiempo.Substring(0, 5);

                        string[] sTiempos = sTiempo.Split(':');
                        DateTime dtLlegada = txtFechaSalidaFV.Text.S().Dt();

                        dtLlegada = dtLlegada.AddHours(sTiempos[0].S().I());
                        dtLlegada = dtLlegada.AddMinutes(sTiempos[1].S().I());

                        txtFechaLlegadaFV.Value = dtLlegada;
                    }
                }

                if (hdOpcPresupuesto.Value == "2")
                    AbreSegundoPresupuesto();
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "txtFechaSalidaFV_DateChanged", "Aviso");
            }
        }
        protected void txtFechaSalidaFV_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                
                if (oDatosContrato == null)
                {
                    oDatosContrato = new DBRemision().DBGetObtieneDatosRemision(0, iIdContrato);
                }

                if (oDatosContrato != null)
                {
                    if (oDatosContrato.bTiemposPactados)
                    {
                        string sTiempo = Utils.ObtieneTiempoTramosPresupuestos(oDatosContrato, ddlOrigenFV.Value.S().I(), ddlDestinoFV.Value.S().I(), cboDgModeloSolicitado.Value.S().I());
                        txtTiempoVueloFV.Text = sTiempo.Substring(0, 5);

                        string[] sTiempos = sTiempo.Split(':');
                        DateTime dtLlegada = txtFechaSalidaFV.Text.S().Dt();

                        dtLlegada = dtLlegada.AddHours(sTiempos[0].S().I());
                        dtLlegada = dtLlegada.AddHours(sTiempos[1].S().I());

                        txtFechaLlegadaFV.Text = dtLlegada.S();
                    }
                }
                
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "txtFechaSalidaFV_ValueChanged", "Aviso");
            }
        }
        protected void txtFechaLlegadaFV_DateChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtFechaLlegadaFV.Text.S() != string.Empty)
                {
                    DateTime FechaSalida;
                    DateTime FechaLlegada;
                    ASPxDateEdit txt = (ASPxDateEdit)sender;

                    if (txtFechaSalidaFV.Value.S() != string.Empty)
                    {
                        if (txt != null)
                        {
                            FechaSalida = txtFechaSalidaFV.Value.Dt();
                            FechaLlegada = txt.Value.Dt();

                            if (FechaSalida > FechaLlegada)
                            {
                                AjustaTextBox(txt, System.Drawing.Color.Red, "La fecha de salida no puede ser mayor que la de llegada");
                                txtTiempoVueloFV.Focus();
                                txtFechaLlegadaFV.IsValid = false;
                                txtFechaLlegadaFV.ErrorText = "La fecha de salida no puede ser mayor que la de llegada";
                            }
                            else
                            {
                                AjustaTextBox(txt, System.Drawing.Color.Black, "Fecha correcta.");

                                FechaSalida = FechaSalida.AddSeconds(FechaSalida.Second * -1);
                                FechaLlegada = FechaLlegada.AddSeconds(FechaLlegada.Second * -1);

                                TimeSpan ts = FechaLlegada - FechaSalida;
                                if (!(FechaLlegada.Day.S().PadLeft(2, '0') == "01" && FechaLlegada.Month.S().PadLeft(2, '0') == "01" && FechaLlegada.Year.S().PadLeft(4, '0') == "0001") && !(FechaSalida.Day.S().PadLeft(2, '0') == "01" && FechaSalida.Month.S().PadLeft(2, '0') == "01" && FechaSalida.Year.S().PadLeft(4, '0') == "0001"))
                                    txtTiempoVueloFV.Text = ts.S();
                            }
                        }
                    }
                }

                if (hdOpcPresupuesto.Value == "2")
                    AbreSegundoPresupuesto();
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "txtFechaLlegadaFV_DateChanged", "Aviso");
            }
        }
        protected void btnAgregarFV_Click1(object sender, EventArgs e)
        {
            try
            {
                DateTime FechaSalida = txtFechaSalidaFV.Value.Dt();
                DateTime FechaLlegada = txtFechaLlegadaFV.Value.Dt();

                if (FechaSalida < FechaLlegada)
                {

                    if (hdOpcPresupuesto.Value == "1")
                    {
                        #region PRESUPUESTO 1
                        // Agrega la pierna escrita en el DataTable de piernas.
                        DataRow row = dtTramos.NewRow();

                        row["IdOrigen"] = ddlOrigenFV.Value.S().I();
                        row["IdDestino"] = ddlDestinoFV.Value.S().I();
                        row["Origen"] = ddlOrigenFV.Text;
                        row["Destino"] = ddlDestinoFV.Text;
                        row["CantPax"] = spePax.Text.S().I();
                        row["FechaSalida"] = txtFechaSalidaFV.Value.Dt();
                        row["FechaLlegada"] = txtFechaLlegadaFV.Value.Dt();
                        row["TiempoVuelo"] = txtTiempoVueloFV.Text.S();
                        row["TiempoEspera"] = "00:00:00";
                        row["TiempoCobrar"] = txtTiempoVueloFV.Text.S();
                        row["SeCobra"] = 1;


                        if (dtTramos.Rows.Count > 0)
                        {
                            // Calcula Tiempo de Espera entre la pierna nueva y la anterior.
                            TimeSpan ts;
                            ts = txtFechaSalidaFV.Value.Dt() - dtTramos.Rows[dtTramos.Rows.Count - 1]["FechaLlegada"].S().Dt();

                            double dHoras = ts.TotalHours.S().Replace("-", "").S().Db();
                            double dMinutos = ts.Minutes.S().Replace("-", "").S().Db();
                            double iHoras = Math.Truncate(dHoras);
                            double iMinutos = Math.Truncate(dMinutos);

                            dtTramos.Rows[dtTramos.Rows.Count - 1]["TiempoEspera"] = iHoras.S().PadLeft(2, '0') + ":" + iMinutos.S().PadLeft(2, '0') + ":00";


                            if (ddlOrigenFV.Text.S() == dtTramos.Rows[0].S("Origen") && dtTramos.Rows.Count >= 2)
                            {
                                dtTramos.Rows[dtTramos.Rows.Count - 1]["TiempoEspera"] = "00:00";
                                //row["TiempoEspera"] = "00:00:00";
                            }
                        }

                        dtTramos.Rows.Add(row);
                        eOpcionesPres = OpcionesPresupuesto.Pernoctas;


                        RecargaGridTramosPasoTres();
                        CalculaPresupuesto();
                        #endregion
                    }
                    else
                    {
                        #region PRESUPUESTO 2
                        DataRow row = dtTramos2.NewRow();

                        row["IdOrigen"] = ddlOrigenFV.Value.S().I();
                        row["IdDestino"] = ddlDestinoFV.Value.S().I();
                        row["Origen"] = ddlOrigenFV.Text;
                        row["Destino"] = ddlDestinoFV.Text;
                        row["CantPax"] = spePax.Text.S().I();
                        row["FechaSalida"] = txtFechaSalidaFV.Value.Dt();
                        row["FechaLlegada"] = txtFechaLlegadaFV.Value.Dt();
                        row["TiempoVuelo"] = txtTiempoVueloFV.Text.S();
                        row["TiempoEspera"] = "00:00:00";
                        row["TiempoCobrar"] = txtTiempoVueloFV.Text.S();
                        row["SeCobra"] = 1;


                        if (dtTramos2.Rows.Count > 0)
                        {
                            // Calcula Tiempo de Espera entre la pierna nueva y la anterior.
                            TimeSpan ts;
                            ts = txtFechaSalidaFV.Value.Dt() - dtTramos2.Rows[dtTramos.Rows.Count - 1]["FechaLlegada"].S().Dt();

                            double dHoras = ts.TotalHours.S().Replace("-", "").S().Db();
                            double dMinutos = ts.Minutes.S().Replace("-", "").S().Db();
                            double iHoras = Math.Truncate(dHoras);
                            double iMinutos = Math.Truncate(dMinutos);

                            dtTramos2.Rows[dtTramos2.Rows.Count - 1]["TiempoEspera"] = iHoras.S().PadLeft(2, '0') + ":" + iMinutos.S().PadLeft(2, '0') + ":00";


                            if (ddlOrigenFV.Text.S() == dtTramos2.Rows[0].S("Origen") && dtTramos2.Rows.Count >= 2)
                            {
                                dtTramos2.Rows[dtTramos2.Rows.Count - 1]["TiempoEspera"] = "00:00";
                                //row["TiempoEspera"] = "00:00:00";
                            }
                        }

                        dtTramos2.Rows.Add(row);
                        eOpcionesPres = OpcionesPresupuesto.FerrysVirtuales;

                        RecargaGridTramosPasoTres2();
                        CalculaPresupuesto2();

                        AbreSegundoPresupuesto();
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnAgregarFerry_Click", "Aviso");
            }
        }
        protected void gvTramosOpc1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    ASPxDateEdit txtFS = (ASPxDateEdit)e.Row.FindControl("txtFechaSalida");
                    if (txtFS != null)
                    {
                        txtFS.Value = dtTramos.Rows[e.Row.RowIndex]["FechaSalida"].S().Dt();

                        if (dtTramos.Rows[e.Row.RowIndex]["SeCobra"].S() == "0")
                        {
                            e.Row.BackColor = System.Drawing.Color.Beige;
                        }
                    }

                    ASPxDateEdit txtFL = (ASPxDateEdit)e.Row.FindControl("txtFechaLlegada");
                    if (txtFL != null)
                    {
                        txtFL.Value = dtTramos.Rows[e.Row.RowIndex]["FechaLlegada"].S().Dt();
                    }

                    ASPxTextBox txtPax = (ASPxTextBox)e.Row.FindControl("txtCantPax");
                    if (txtPax != null)
                    {
                        txtPax.Text = dtTramos.Rows[e.Row.RowIndex]["CantPax"].S();
                    }

                    ASPxTextBox txtTC = (ASPxTextBox)e.Row.FindControl("txtTiempoCobrar");
                    if (txtTC != null)
                    {
                        txtTC.Text = dtTramos.Rows[e.Row.RowIndex]["TiempoCobrar"].S();
                    }

                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvTramosOpc1_RowDataBound", "Aviso");
            }
        }
        protected void gvConceptos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (e.Row.Cells[0].Text.S() == "% DESC SERVICIOS VUELO" || e.Row.Cells[0].Text.S() == "IVA DE VUELO" || e.Row.Cells[0].Text.S() == "IVA SERVICIOS CON CARGO")
                    {
                        ASPxTextBox txtDescSV = (ASPxTextBox)e.Row.Cells[2].FindControl("txtDescUnidad");
                        if (txtDescSV == null)
                        {
                            txtDescSV = new ASPxTextBox();
                        }
                        ASPxLabel lbl = (ASPxLabel)e.Row.Cells[2].FindControl("lblDescUnidad");
                        if (lbl != null)
                        {
                            lbl.Visible = false;
                        }

                        txtDescSV.Text = dtImportes.Rows[e.Row.RowIndex]["Unidad"].S();
                    }
                    else
                    {
                        ASPxLabel lblTexto = (ASPxLabel)e.Row.Cells[2].FindControl("lblDescUnidad");
                        if (lblTexto == null)
                        {
                            lblTexto = new ASPxLabel();
                        }
                        ASPxTextBox txt = (ASPxTextBox)e.Row.Cells[2].FindControl("txtDescUnidad");
                        if (txt != null)
                        {
                            txt.Visible = false;
                        }

                        lblTexto.Text = dtImportes.Rows[e.Row.RowIndex]["Unidad"].S();
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvConceptos_RowDataBound", "Aviso");
            }
        }
        protected void gvConceptos_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

            }
        }
        protected void upaConceptos_Unload(object sender, EventArgs e)
        {
            MethodInfo methodInfo = typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Where(i => i.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel")).First();
            methodInfo.Invoke(ScriptManager.GetCurrent(Page),
                new object[] { sender as UpdatePanel });
        }
        protected void txtDescUnidad_TextChanged(object sender, EventArgs e)
        {
            ASPxTextBox txtCambio = (ASPxTextBox)sender;
            GridViewRow rowSelect = (GridViewRow)((txtCambio).NamingContainer);
            dtImportes.Rows[rowSelect.RowIndex]["Unidad"] = txtCambio.Text;

            decimal dDesc = 0;
            decimal dDescSV = 0;
            foreach (GridViewRow row in gvConceptos.Rows)
            {
                if (row.Cells[0].Text == "% DESC SERVICIOS VUELO")
                {
                    ASPxTextBox txt = (ASPxTextBox)row.FindControl("txtDescUnidad");
                    dDesc = txt.Text.S().D();
                    dtImportes.Rows[row.RowIndex]["Importe"] = dSubTotSV;
                }

                if (row.Cells[0].Text == "IVA DE VUELO")
                {
                    ASPxTextBox txt = (ASPxTextBox)row.FindControl("txtDescUnidad");
                    dIvaSV = txt.Text.S().D();
                    dtImportes.Rows[row.RowIndex]["Importe"] = dIvaSV;
                }

                if (row.Cells[0].Text == "IVA SERVICIOS CON CARGO")
                {
                    ASPxTextBox txt = (ASPxTextBox)row.FindControl("txtDescUnidad");
                    dIvaSC = txt.Text.S().D();
                    dtImportes.Rows[row.RowIndex]["Importe"] = dIvaSC;
                }
            }


            decimal dIvaVuelo = 0;
            decimal dSerCargo = 0;
            decimal dIvaSCargo = 0;
            decimal dTotalSV = 0;
            foreach (GridViewRow row in gvConceptos.Rows)
            {
                // Row SubTotal de Vuelo
                if (row.Cells[0].Text == "SUBTOTAL SERVICIOS DE VUELO")
                {
                    dSubTotSV = dtImportes.Rows[row.RowIndex]["Importe"].S().D();
                }

                // Row % Servicios Vuelo
                if (row.Cells[0].Text == "% DESC SERVICIOS VUELO")
                {
                    dDescSV = (dSubTotSV * (dDesc / 100)).S().D();
                    dtImportes.Rows[row.RowIndex]["Importe"] = dDescSV;

                    dSubTotSV = dSubTotSV - dDescSV;
                }

                // Row Total Serv Vuelo
                if (row.Cells[0].Text == "TOTAL SERVICIOS VUELO")
                {
                    dTotalSV = dSubTotSV;
                    dtImportes.Rows[row.RowIndex]["Importe"] = dTotalSV;
                }

                // Row Iva de Vuelo
                if (row.Cells[0].Text == "IVA DE VUELO")
                {
                    dIvaVuelo = (dSubTotSV * (dIvaSV / 100)).S().D();
                    dtImportes.Rows[row.RowIndex]["Importe"] = dIvaVuelo;
                }

                // Row Total Servicios Cargo
                if (row.Cells[0].Text == "TOTAL SERVICIOS CON CARGO")
                {
                    dSubTotSC = SumaTablaServiciosConCargo(dtServiciosC);

                    dSerCargo = cboDgMonedaPresupueto.Value.S() == "2" ? dSubTotSC : (dSubTotSC / dTipoCambioDia);
                    dtImportes.Rows[row.RowIndex]["Importe"] = dSerCargo;
                }

                // Row IVA Servicios Cargo
                if (row.Cells[0].Text == "IVA SERVICIOS CON CARGO")
                {
                    dIvaSCargo = dSerCargo * (dIvaSC / 100);
                    dtImportes.Rows[row.RowIndex]["Importe"] = dIvaSCargo;
                }

                // Row Total Presupuesto
                if (row.Cells[0].Text == "TOTAL PRESUPUESTO")
                {
                    dtImportes.Rows[row.RowIndex]["Importe"] = dTotalSV + dIvaVuelo + dSerCargo + dIvaSCargo;
                }
            }

            gvConceptos.DataSource = dtImportes;
            gvConceptos.DataBind();

            PintaTotalesConceptos();
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (oPresupuesto.dtTramos == null)
                {
                    MostrarMensaje("Es necesario ingresar tramos", "Aviso");
                    return;
                }
                if (iIdPresupuesto == 0)
                {
                    if (eNewObj != null)
                        eNewObj(sender, e);
                }
                else
                {
                    if (eSaveObj != null)
                        eSaveObj(sender, e);
                }

                MostrarMensaje(sMensaje, "Aviso");
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnGuardar_Click", "Aviso");
            }
        }
        protected void btnImprimir_Click(object sender, EventArgs e)
        {

            if (iIdPresupuesto != 0)
            {
                string sIdPresupuesto = Convert.ToBase64String(Encoding.UTF8.GetBytes(iIdPresupuesto.S()));
                string ruta = "~/Views/Consultas/frmReportePresupuesto.aspx?Presupuesto=" + sIdPresupuesto + "&Op=0";
                if (IsCallback)
                    ASPxWebControl.RedirectOnCallback(ruta);
                else
                    Response.Redirect(ruta);
            }
        }
        protected void btnCreaSol_Click(object sender, EventArgs e)
        {
            try
            {
                if (oPresupuesto.dtTramos == null)
                {
                    MostrarMensaje("Es necesario ingresar tramos", "Aviso");
                    return;
                }
                
                sMensaje = string.Empty;

                if (eSaveSolicitud != null)
                    eSaveSolicitud(sender, e);
                
                if (iIdPresupuesto == 0)
                {
                    if (eNewObj != null)
                        eNewObj(sender, e);
                }
                else
                {
                    if (iIdSolicitudVuelo > 0)
                    {
                        oPresupuesto.iIdSolicitudVuelo = iIdSolicitudVuelo;

                        if (eSaveObj != null)
                            eSaveObj(sender, e);
                    }
                }

                // Muesttra mensaje de confirmación
                if (sMensaje.Length > 0)
                {
                    sMensaje = string.Format("{0} {1} Se guardo la solicitud: {2}", sMensaje, System.Environment.NewLine, iIdSolicitudVuelo.S());
                }
                else
                {
                    sMensaje = string.Format("Se guardo la solicitud: {0}", iIdSolicitudVuelo);
                }



                if (sMensaje.Length > 0)
                {
                    MostrarMensaje(sMensaje, "Aviso");
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnGuardar_Click", "Aviso");
            }
        }
        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            if (txtDgCorreo.Text.S() == string.Empty)
            {
                txtDgCorreo.IsValid = false;
                txtDgCorreo.ErrorText = "El Campo es requerido";
                txtDgCorreo.ValidationSettings.ErrorTextPosition = ErrorTextPosition.Bottom;
                txtDgCorreo.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.Text;
                return;
            }
            string sNombreUsuario = string.Empty;
            if (txtDgNombreSolicitante.Text.S() != string.Empty)
            {
                sNombreUsuario = txtDgNombreSolicitante.Text.S();
            }

            if (iIdPresupuesto != 0)
            {
                string sIdPresupuesto = Convert.ToBase64String(Encoding.UTF8.GetBytes(iIdPresupuesto.S()));
                string ruta = "~/Views/Consultas/frmReportePresupuesto.aspx?Presupuesto=" + sIdPresupuesto+"&Op=1";
                if (IsCallback)
                    ASPxWebControl.RedirectOnCallback(ruta);
                else
                    Response.Redirect(ruta);
            }
         

        }
        protected void upaServicios_Unload(object sender, EventArgs e)
        {
            try
            {
                MethodInfo methodInfo = typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Where(i => i.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel")).First();
                methodInfo.Invoke(ScriptManager.GetCurrent(Page),
                    new object[] { sender as UpdatePanel });
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "Unnamed_Unload", "Aviso");
            }
        }
        protected void txtTarVueloNal_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ASPxTextBox txt = (ASPxTextBox)sender;
                if (txt != null && txt.Text != string.Empty)
                {
                    if (dtImportes != null)
                    {
                        if (dtImportes.Rows.Count > 0)
                        {
                            dtImportes = DBSetCambiaImportesPesosDolares(dtImportes);
                            gvConceptos.DataSource = dtImportes;
                            gvConceptos.DataBind();

                            PintaTotalesConceptos();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "txtTarVueloNal_TextChanged", "Aviso");
            }
        }
        protected void cboDgModeloSolicitado_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (dtTramos != null)
                {
                    if (dtTramos.Rows.Count > 0)
                        CalculaPresupuesto();
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "cboDgModeloSolicitado_ValueChanged", "Aviso");
            }
        }
        protected void cboDgMonedaPresupueto_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (dtImportes != null)
                {
                    if (dtImportes.Rows.Count > 0)
                    {
                        dtImportes = DBSetCambiaImportesMoneda(dtImportes);

                        gvConceptos.DataSource = dtImportes;
                        gvConceptos.DataBind();

                        PintaTotalesConceptos();
                    }

                    if (dtImportes2 != null)
                    {
                        if (dtImportes2.Rows.Count > 0)
                        {
                            dtImportes2 = DBSetCambiaImportesMoneda2(dtImportes2);

                            gvConceptos2.DataSource = dtImportes2;
                            gvConceptos2.DataBind();

                            PintaTotalesConceptos2();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "cboDgMonedaPresupueto_ValueChanged", "Aviso");
            }
        }
        protected void txtCantPax_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ASPxTextBox txt = (ASPxTextBox)sender;
                GridViewRow Row = (GridViewRow)txt.NamingContainer;
                ASPxTextBox txtCant = (ASPxTextBox)Row.FindControl("txtCantPax");

                if (txtCant != null)
                {
                    if (txtCant.Text.S() != string.Empty)
                    {
                        dtTramos.Rows[Row.RowIndex]["CantPax"] = txtCant.Text;
                        CalculaPresupuesto();
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "txtCantPax_TextChanged", "Aviso");
            }
        }
        protected void txtTiempoCobrar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ASPxTextBox txt = (ASPxTextBox)sender;
                GridViewRow Row = (GridViewRow)txt.NamingContainer;
                ASPxTextBox txtCant = (ASPxTextBox)Row.FindControl("txtTiempoCobrar");

                if (txtCant != null)
                {
                    if (txtCant.Text.S() != string.Empty)
                    {
                        dtTramos.Rows[Row.RowIndex]["TiempoCobrar"] = txtCant.Text;
                        CalculaPresupuesto();
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "txtTiempoCobrar_TextChanged", "Aviso");
            }
        }
        protected void ddlOrigenFV_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                int iIdOrigen = 0;
                int iIdDestino = 0;
                ASPxComboBox ddl = (ASPxComboBox)sender;

                if (dtTramos != null && dtTramos.Rows.Count > 0)
                {
                    if (ddlOrigenFV.Value.S() != string.Empty)
                    {
                        if (ddl != null)
                        {
                            iIdOrigen = ddlOrigenFV.Value.S().I();
                            iIdDestino = dtTramos.Rows[dtTramos.Rows.Count - 1]["IdDestino"].S().I();

                            if (iIdOrigen != iIdDestino)
                            {
                                ddlOrigenFV.IsValid = false;
                                ddlOrigenFV.ErrorText = "El aeropuerto de salida debe ser igual al destino del tramo anterior.";
                                ddlOrigenFV.ValidationSettings.ErrorTextPosition = ErrorTextPosition.Bottom;
                                ddlOrigenFV.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.Text;
                            }
                        }
                    }
                }

                if (ddl.IsValid)
                    ddlDestinoFV.Focus();

                if (hdOpcPresupuesto.Value == "2")
                    AbreSegundoPresupuesto();
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "ddlOrigenFV_ValueChanged", "Aviso");
            }
        }
        protected void cboDgSolicitante_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtDgNombreSolicitante.Text = cboDgSolicitante.SelectedItem.Text;
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "cboDgSolicitante_SelectedIndexChanged", "Aviso");
            }
        }
        protected void ddlConceptoAdi_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                switch (ddlConceptoAdi.SelectedItem.Value.S())
                {
                    case "1":
                    case "2":
                    case "3":
                    case "4":
                        txttiempoAdi.ToolTip = "Ejemplo: '-01:00' o '01:00'";
                        break;
                    case "5":
                    case "6":
                        txttiempoAdi.ToolTip = "Ejemplo: 1,2,3...";
                        break;
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "ddlConceptoAdi_SelectedIndexChanged", "Aviso");
            }
        }
        protected void chkTiempoAdi_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                lblTiempoAdi.Visible = chkTiempoAdi.Checked;
                txttiempoAdi.Visible = chkTiempoAdi.Checked;
                lblConceptoAdi.Visible = chkTiempoAdi.Checked;
                ddlConceptoAdi.Visible = chkTiempoAdi.Checked;
                btnAgregarTiempo.Visible = chkTiempoAdi.Checked;

                if (ddlConceptoAdi.Items.Count == 0)
                {
                    DataTable dtServ = new DBRemision().DBGetConsultaConceptosServiciosVuelo;
                    ddlConceptoAdi.DataSource = dtServ;
                    ddlConceptoAdi.TextField = "Descripcion";
                    ddlConceptoAdi.ValueField = "IdConcepto";
                    ddlConceptoAdi.DataBind();
                }

            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "chkTiempoAdi_CheckedChanged", "Aviso");
            }
        }
        protected void btnAgregarTiempo_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataRow dr in dtImportes.Rows)
                {
                    if (dr.S("IdConcepto") == ddlConceptoAdi.SelectedItem.Value.S())
                    {
                        string sCantidad1 = dr.S("Cantidad");
                        string sCantidad2 = txttiempoAdi.Text.S();

                        switch (dr.S("IdConcepto"))
                        {
                            case "1":
                            case "2":
                            case "3":
                            case "4":
                                dr["Cantidad"] = Utils.GetSumaTiempos(sCantidad1, sCantidad2);
                                break;
                            case "5":
                            case "6":
                                dr["Cantidad"] = (sCantidad1.S().I() + sCantidad2.S().I()).S();
                                break;
                        }
                    }
                }

                //// Recalcula Grid
                dtImportes = CalculaTiempoModificado(dtImportes, cboDgContrato.SelectedItem.Value.S().I());
                gvConceptos.DataSource = dtImportes;
                gvConceptos.DataBind();

                txttiempoAdi.Text = string.Empty;
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnAgregarTiempo_Click", "Aviso");
            }
        }


        // EVENTOS DEL SEGUNDO PRESUPUESTO
        protected void gvTramosOpc2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    ASPxDateEdit txtFS = (ASPxDateEdit)e.Row.FindControl("txtFechaSalida2");
                    if (txtFS != null)
                    {
                        txtFS.Value = dtTramos2.Rows[e.Row.RowIndex]["FechaSalida"].S().Dt();

                        if (dtTramos2.Rows[e.Row.RowIndex]["SeCobra"].S() == "0")
                        {
                            e.Row.BackColor = System.Drawing.Color.Beige;
                        }
                    }

                    ASPxDateEdit txtFL = (ASPxDateEdit)e.Row.FindControl("txtFechaLlegada2");
                    if (txtFL != null)
                    {
                        txtFL.Value = dtTramos2.Rows[e.Row.RowIndex]["FechaLlegada"].S().Dt();
                    }

                    ASPxTextBox txtPax = (ASPxTextBox)e.Row.FindControl("txtCantPax2");
                    if (txtPax != null)
                    {
                        txtPax.Text = dtTramos2.Rows[e.Row.RowIndex]["CantPax"].S();
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvTramosOpc2_RowDataBound", "Aviso");
            }
        }
        protected void btnAgregaPierna2_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtTramos2 != null && dtTramos2.Rows.Count > 0)
                {
                    hdOpcPresupuesto.Value = "2";

                    ddlOrigenFV.Value = null;
                    ddlDestinoFV.Value = null;
                    spePax.Text = "0";
                    txtFechaSalidaFV.Text = string.Empty;
                    txtFechaLlegadaFV.Text = string.Empty;
                    txtTiempoVueloFV.Text = string.Empty;

                    ddlOrigenFV.IsValid = true;
                    ddlDestinoFV.IsValid = true;
                    txtFechaSalidaFV.IsValid = true;
                    txtFechaLlegadaFV.IsValid = true;
                    txtTiempoVueloFV.IsValid = true;

                    ppTramos.ShowOnPageLoad = true;

                    AbreSegundoPresupuesto();
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnAgregaPierna_Click", "Aviso");
            }
        }
        protected void txtCantPax2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ASPxTextBox txt = (ASPxTextBox)sender;
                GridViewRow Row = (GridViewRow)txt.NamingContainer;
                ASPxTextBox txtCant = (ASPxTextBox)Row.FindControl("txtCantPax2");

                if (txtCant != null)
                {
                    if (txtCant.Text.S() != string.Empty)
                    {
                        dtTramos2.Rows[Row.RowIndex]["CantPax"] = txtCant.Text;
                        CalculaPresupuesto2();
                    }
                }

                AbreSegundoPresupuesto();
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "txtCantPax2_TextChanged", "Aviso");
            }
        }
        protected void imbDelete2_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ImageButton txtCambio = (ImageButton)sender;
                GridViewRow rowSelect = (GridViewRow)((txtCambio).NamingContainer);

                dtTramos2.Rows.RemoveAt(rowSelect.RowIndex);

                if (dtTramos2.Rows.Count > 0)
                {
                    RecargaGridTramosPasoTres2();
                    ReCalculaPresupuesto2();
                }
                else
                {
                    gvTramosOpc2.DataSource = null;
                    gvTramosOpc2.DataBind();

                    gvServiciosC2.DataSource = null;
                    gvServiciosC2.DataBind();

                    gvConceptos2.DataSource = null;
                    gvConceptos2.DataBind();
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "imbDelete2_Click", "Aviso");
            }
        }
        protected void gvServiciosC2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ASPxTextBox txtImporte = (ASPxTextBox)e.Row.FindControl("txtImporte2");
                if (txtImporte != null)
                {
                    txtImporte.Text = dtServiciosC2.Rows[e.Row.RowIndex]["Importe"].S();
                }

                if (dtServiciosC2.Rows[e.Row.RowIndex][1].S() == "SubTotal")
                {
                    ASPxButton btn = (ASPxButton)e.Row.FindControl("btnEliminarSC2");
                    if (btn != null)
                        btn.Visible = false;
                }
            }
        }
        protected void gvServiciosC2_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
        protected void txtImporte2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow row in gvServiciosC2.Rows)
                {
                    if (row.Cells[0].Text != "SubTotal")
                    {
                        foreach (DataRow dr in dtServiciosC2.Rows)
                        {
                            if (dr.S("ServicioConCargoDescripcion") == row.Cells[0].Text.S())
                            {
                                ASPxTextBox txt = (ASPxTextBox)row.FindControl("txtImporte2");
                                if (txt != null)
                                {
                                    dr["Importe"] = txt.Text.S().D();
                                }
                            }
                        }
                    }
                }

                decimal dSuma = 0;

                dtServiciosC2.Rows.RemoveAt(dtServiciosC2.Rows.Count - 1);


                DataRow[] drSub = dtServiciosC2.Select("ServicioConCargoDescripcion = 'SubTotal'");
                if (drSub.Length == 0)
                {
                    DataRow drS = dtServiciosC2.NewRow();
                    dSuma = SumaImportesdeTabla(dtServiciosC2, "Importe");
                    drS["IdServicioConCargo"] = 999999;
                    drS["ServicioConCargoDescripcion"] = "SubTotal";
                    drS["Importe"] = dSuma;
                    dtServiciosC2.Rows.Add(drS);
                }
                else
                {
                    dSuma = SumaImportesdeTabla(dtServiciosC2, "Importe");
                    drSub[0]["Importe"] = dSuma;
                }

                dSubTotSC2 = dSuma;


                decimal dIva = 0;
                dIva = Utils.GetParametrosClave("9").S().D();

                decimal dServiciosCM = 0;
                decimal dIvaServCM = 0;
                foreach (DataRow row in dtImportes2.Rows)
                {
                    if (row["Concepto"].S() == "TOTAL SERVICIOS CON CARGO")
                    {
                        int iTipoMonedaPres = cboDgMonedaPresupueto.SelectedItem.Value.S().I();
                        decimal dDividendoSC = iTipoMonedaPres == 1 ? dTipoCambioDia : 1;
                        dServiciosCM = (dSubTotSC2 / dDividendoSC);
                        row["Importe"] = dServiciosCM;
                    }

                    if (row["Concepto"].S() == "IVA SERVICIOS CON CARGO")
                    {
                        int iTipoMonedaPres = cboDgMonedaPresupueto.SelectedItem.Value.S().I();
                        decimal dDividendoSC = iTipoMonedaPres == 1 ? dTipoCambioDia : 1;
                        dIvaServCM = (dSubTotSC2 / dDividendoSC) * dIva;

                        row["Importe"] = dIvaServCM;
                    }

                    if (row["Concepto"].S() == "TOTAL PRESUPUESTO")
                    {
                        row["Importe"] = SumaTotalPresupuesto(dtImportes2);
                    }
                }

                gvConceptos2.DataSource = dtImportes2;
                gvConceptos2.DataBind();

                PintaTotalesConceptos2();

                gvServiciosC2.DataSource = dtServiciosC;
                gvServiciosC2.DataBind();

                AbreSegundoPresupuesto();
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "txtImporte2_TextChanged", "Aviso");
            }
        }
        protected void gvConceptos2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (e.Row.Cells[0].Text.S() == "% DESC SERVICIOS VUELO" || e.Row.Cells[0].Text.S() == "IVA DE VUELO" || e.Row.Cells[0].Text.S() == "IVA SERVICIOS CON CARGO")
                    {
                        ASPxTextBox txtDescSV = (ASPxTextBox)e.Row.Cells[2].FindControl("txtDescUnidad2");
                        if (txtDescSV == null)
                        {
                            txtDescSV = new ASPxTextBox();
                        }
                        ASPxLabel lbl = (ASPxLabel)e.Row.Cells[2].FindControl("lblDescUnidad2");
                        if (lbl != null)
                        {
                            lbl.Visible = false;
                        }

                        txtDescSV.Text = dtImportes2.Rows[e.Row.RowIndex]["Unidad"].S();
                    }
                    else
                    {
                        ASPxLabel lblTexto = (ASPxLabel)e.Row.Cells[2].FindControl("lblDescUnidad2");
                        if (lblTexto == null)
                        {
                            lblTexto = new ASPxLabel();
                        }
                        ASPxTextBox txt = (ASPxTextBox)e.Row.Cells[2].FindControl("txtDescUnidad2");
                        if (txt != null)
                        {
                            txt.Visible = false;
                        }

                        lblTexto.Text = dtImportes2.Rows[e.Row.RowIndex]["Unidad"].S();
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvConceptos2_RowDataBound", "Aviso");
            }
        }
        protected void gvConceptos2_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

            }
        }
        protected void txtDescUnidad2_TextChanged(object sender, EventArgs e)
        {
            ASPxTextBox txtCambio = (ASPxTextBox)sender;
            GridViewRow rowSelect = (GridViewRow)((txtCambio).NamingContainer);
            dtImportes2.Rows[rowSelect.RowIndex]["Unidad"] = txtCambio.Text;

            decimal dDesc = 0;
            decimal dDescSV = 0;
            foreach (GridViewRow row in gvConceptos2.Rows)
            {
                if (row.Cells[0].Text == "% DESC SERVICIOS VUELO")
                {
                    ASPxTextBox txt = (ASPxTextBox)row.FindControl("txtDescUnidad2");
                    dDesc = txt.Text.S().D();
                    dtImportes2.Rows[row.RowIndex]["Importe"] = dSubTotSV;
                }

                if (row.Cells[0].Text == "IVA DE VUELO")
                {
                    ASPxTextBox txt = (ASPxTextBox)row.FindControl("txtDescUnidad2");
                    dIvaSV = txt.Text.S().D();
                    dtImportes2.Rows[row.RowIndex]["Importe"] = dIvaSV;
                }

                if (row.Cells[0].Text == "IVA SERVICIOS CON CARGO")
                {
                    ASPxTextBox txt = (ASPxTextBox)row.FindControl("txtDescUnidad2");
                    dIvaSC = txt.Text.S().D();
                    dtImportes2.Rows[row.RowIndex]["Importe"] = dIvaSC;
                }
            }


            decimal dIvaVuelo = 0;
            decimal dSerCargo = 0;
            decimal dIvaSCargo = 0;
            decimal dTotalSV = 0;
            decimal dServCargoTemp = 0;
            foreach (GridViewRow row in gvConceptos2.Rows)
            {
                // Row SubTotal de Vuelo
                if (row.Cells[0].Text == "SUBTOTAL SERVICIOS DE VUELO")
                {
                    dSubTotSV2 = dtImportes2.Rows[row.RowIndex]["Importe"].S().D();
                    dServCargoTemp = dSubTotSV2;
                }

                // Row % Servicios Vuelo
                if (row.Cells[0].Text == "% DESC SERVICIOS VUELO")
                {
                    dDescSV = (dSubTotSV2 * (dDesc / 100)).S().D();
                    dtImportes2.Rows[row.RowIndex]["Importe"] = dDescSV;

                    dSubTotSV2 = dServCargoTemp - dDescSV;
                }

                // Row Total Serv Vuelo
                if (row.Cells[0].Text == "TOTAL SERVICIOS VUELO")
                {
                    dTotalSV = dSubTotSV2;
                    dtImportes2.Rows[row.RowIndex]["Importe"] = dTotalSV;
                }

                // Row Iva de Vuelo
                if (row.Cells[0].Text == "IVA DE VUELO")
                {
                    dIvaVuelo = (dSubTotSV2 * (dIvaSV / 100)).S().D();
                    dtImportes2.Rows[row.RowIndex]["Importe"] = dIvaVuelo;
                }

                // Row Total Servicios Cargo
                if (row.Cells[0].Text == "TOTAL SERVICIOS CON CARGO")
                {
                    dSubTotSC2 = SumaTablaServiciosConCargo(dtServiciosC2);

                    dSerCargo = cboDgMonedaPresupueto.Value.S() == "2" ? dSubTotSC2 : (dSubTotSC2 / dTipoCambioDia);
                    dtImportes2.Rows[row.RowIndex]["Importe"] = dSerCargo;
                }

                // Row IVA Servicios Cargo
                if (row.Cells[0].Text == "IVA SERVICIOS CON CARGO")
                {
                    dIvaSCargo = dSerCargo * (dIvaSC / 100);
                    dtImportes2.Rows[row.RowIndex]["Importe"] = dIvaSCargo;
                }

                // Row Total Presupuesto
                if (row.Cells[0].Text == "TOTAL PRESUPUESTO")
                {
                    dtImportes2.Rows[row.RowIndex]["Importe"] = dTotalSV + dIvaVuelo + dSerCargo + dIvaSCargo;
                }
            }

            gvConceptos2.DataSource = dtImportes2;
            gvConceptos2.DataBind();

            PintaTotalesConceptos2();
            
            AbreSegundoPresupuesto();
        }
        protected void txtFechaSalida2_DateChanged(object sender, EventArgs e)
        {
            try
            {
                DateTime FechaSalida;
                DateTime FechaLlegada;
                ASPxDateEdit txt = (ASPxDateEdit)sender;
                GridViewRow Row = (GridViewRow)txt.NamingContainer;
                ASPxDateEdit txtLlegada = (ASPxDateEdit)Row.FindControl("txtFechaLlegada2");

                if (txt != null)
                {
                    FechaSalida = txt.Value.Dt();
                    FechaLlegada = txtLlegada.Value.Dt();

                    if (FechaSalida > FechaLlegada)
                    {
                        AjustaTextBox(txt, System.Drawing.Color.Red, "La fecha de salida no puede ser mayor que la de llegada");
                        txt.Focus();
                    }
                    else
                    {
                        AjustaTextBox(txt, System.Drawing.Color.Black, "Fecha correcta.");

                        TimeSpan ts = FechaLlegada.AddSeconds(FechaLlegada.Second * -1) - FechaSalida.AddSeconds(FechaSalida.Second * -1);

                        dtTramos2.Rows[Row.RowIndex]["TiempoVuelo"] = ts.S().Length == 8 ? ts.S().Substring(0, 5) : ts.S();
                        dtTramos2.Rows[Row.RowIndex]["FechaSalida"] = FechaSalida;

                        if (dtTramos2.Rows[Row.RowIndex].S("Destino") == dtTramos2.Rows[0].S("Origen"))
                        {
                            dtTramos2.Rows[Row.RowIndex]["TiempoEspera"] = "00:00";
                        }

                        DefineFerryCobrar2();
                        ActualizatiempoEspera2();
                        RecargaGridTramosPasoTres2();
                        CalculaPresupuesto2();
                    }
                }

                AbreSegundoPresupuesto();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void txtFechaLlegada2_DateChanged(object sender, EventArgs e)
        {
            try
            {
                DateTime FechaSalida;
                DateTime FechaLlegada;
                ASPxDateEdit txt = (ASPxDateEdit)sender;
                GridViewRow Row = (GridViewRow)txt.NamingContainer;
                ASPxDateEdit txtSalida = (ASPxDateEdit)Row.FindControl("txtFechaSalida2");

                if (txt != null)
                {
                    FechaSalida = txtSalida.Value.Dt();
                    FechaLlegada = txt.Value.Dt();

                    if (FechaSalida > FechaLlegada)
                    {
                        AjustaTextBox(txt, System.Drawing.Color.Red, "La fecha de salida no puede ser mayor que la de llegada");
                        txt.Focus();
                    }
                    else
                    {
                        AjustaTextBox(txt, System.Drawing.Color.Black, "Fecha correcta.");

                        TimeSpan ts = FechaLlegada.AddSeconds(FechaLlegada.Second * -1) - FechaSalida.AddSeconds(FechaSalida.Second * -1);

                        dtTramos2.Rows[Row.RowIndex]["TiempoVuelo"] = ts.S().Length == 8 ? ts.S().Substring(0, 5) : ts.S();
                        dtTramos2.Rows[Row.RowIndex]["FechaLlegada"] = FechaLlegada;

                        if (dtTramos2.Rows[Row.RowIndex].S("Destino") == dtTramos2.Rows[0].S("Origen"))
                        {
                            dtTramos2.Rows[Row.RowIndex]["TiempoEspera"] = "00:00";
                        }

                        DefineFerryCobrar2();
                        ActualizatiempoEspera2();
                        RecargaGridTramosPasoTres2();
                        CalculaPresupuesto2();
                    }
                }

                AbreSegundoPresupuesto();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void chkTiempoAdi2_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                lblTiempoAdi2.Visible = chkTiempoAdi2.Checked;
                txttiempoAdi2.Visible = chkTiempoAdi2.Checked;
                lblConceptoAdi2.Visible = chkTiempoAdi2.Checked;
                ddlConceptoAdi2.Visible = chkTiempoAdi2.Checked;
                btnAgregarTiempo2.Visible = chkTiempoAdi2.Checked;

                if (ddlConceptoAdi2.Items.Count == 0)
                {
                    DataTable dtServ = new DBRemision().DBGetConsultaConceptosServiciosVuelo;
                    ddlConceptoAdi2.DataSource = dtServ;
                    ddlConceptoAdi2.TextField = "Descripcion";
                    ddlConceptoAdi2.ValueField = "IdConcepto";
                    ddlConceptoAdi2.DataBind();
                }
                AbreSegundoPresupuesto();
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "chkTiempoAdi_CheckedChanged", "Aviso");
            }
        }
        protected void btnAgregarTiempo2_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataRow dr in dtImportes2.Rows)
                {
                    if (dr.S("IdConcepto") == ddlConceptoAdi2.SelectedItem.Value.S())
                    {
                        string sCantidad1 = dr.S("Cantidad");
                        string sCantidad2 = txttiempoAdi2.Text.S();

                        switch (dr.S("IdConcepto"))
                        {
                            case "1":
                            case "2":
                            case "3":
                            case "4":
                                dr["Cantidad"] = Utils.GetSumaTiempos(sCantidad1, sCantidad2);
                                break;
                            case "5":
                            case "6":
                                dr["Cantidad"] = (sCantidad1.S().I() + sCantidad2.S().I()).S();
                                break;
                        }
                    }
                }

                //// Recalcula Grid
                dtImportes2 = CalculaTiempoModificado(dtImportes2, cboDgContrato.SelectedItem.Value.S().I());

                gvConceptos2.DataSource = dtImportes2;
                gvConceptos2.DataBind();

                txttiempoAdi2.Text = string.Empty;
                AbreSegundoPresupuesto();
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnAgregarTiempo2_Click", "Aviso");
            }
        }
        protected void btnSelOpc1_Click(object sender, EventArgs e)
        {
            eOpcionesPres = OpcionesPresupuesto.Pernoctas;
            imbPernoctas.ImageUrl = "~/img/iconos/palomita.png";
            imbPernoctas.ToolTip = "Cotización Recomendada";
            imbPernoctas.Width = 24;
            imbPernoctas.Height = 24;

            imbFerrys.ImageUrl = "~/img/iconos/tache.png";
            imbFerrys.ToolTip = "Cotización NO Recomendada";
            imbFerrys.Width = 24;
            imbFerrys.Height = 30;
        }
        protected void btnSelOpc2_Click(object sender, EventArgs e)
        {
            eOpcionesPres = OpcionesPresupuesto.FerrysVirtuales;
            imbFerrys.ImageUrl = "~/img/iconos/palomita.png";
            imbFerrys.ToolTip = "Cotización Recomendada";
            imbFerrys.Width = 24;
            imbFerrys.Height = 24;

            imbPernoctas.ImageUrl = "~/img/iconos/tache.png";
            imbPernoctas.ToolTip = "Cotización NO Recomendada";
            imbPernoctas.Width = 24;
            imbPernoctas.Height = 30;

            AbreSegundoPresupuesto();
        }
        protected void ddlConceptoAdi2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                switch (ddlConceptoAdi2.SelectedItem.Value.S())
                {
                    case "1":
                    case "2":
                    case "3":
                    case "4":
                        txttiempoAdi2.ToolTip = "Ejemplo: '-01:00' o '01:00'";
                        break;
                    case "5":
                    case "6":
                        txttiempoAdi2.ToolTip = "Ejemplo: 1,2,3...";
                        break;
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "ddlConceptoAdi2_SelectedIndexChanged", "Aviso");
            }
        }
        #endregion

        #region METODOS
        private DataTable CalculaTiempoModificado(DataTable dtImportes, int iIdContrato)
        {
            try
            {
                DatosRemision oRem = new DBRemision().DBGetObtieneDatosRemision(0, iIdContrato);

                decimal dSubTotal = 0;
                decimal dDesc = 0;
                decimal dTotSV = 0;
                decimal dIvaC = 0;
                decimal dSSC = 0;
                decimal dISC = 0;

                for (int i = 0; i < dtImportes.Rows.Count; i++)
                {
                    DataRow row = dtImportes.Rows[i];
                    float fTiempoT = 0;
                    decimal dImporte = 0;

                    #region SERVICIOS DE VUELO
                    switch (row.S("IdConcepto"))
                    {
                        case "1":
                            dImporte = Utils.ConvierteTiempoaDecimal(row.S("Cantidad")).S().D() * txtTarVueloNal.Text.S().D();
                            row["Importe"] = dImporte;
                            row["HrDescontar"] = row.S("Cantidad");
                            dSubTotal += dImporte;
                            break;

                        case "2":
                            dImporte = Utils.ConvierteTiempoaDecimal(row.S("Cantidad")).S().D() * txtTarVueloInt.Text.S().D();
                            row["Importe"] = dImporte;
                            row["HrDescontar"] = row.S("Cantidad");
                            dSubTotal += dImporte;
                            break;

                        case "3":
                            dImporte = Utils.ConvierteTiempoaDecimal(row.S("Cantidad")).S().D() * txtEsperaNal.Text.S().D();
                            row["Importe"] = dImporte;
                            if (oRem.bSeDescuentaEsperaNal)
                            {
                                fTiempoT = Utils.ConvierteTiempoaDecimal(row.S("Cantidad"));
                                decimal dHr = (fTiempoT.S().D() * (oRem.dFactorEHrVueloNal / 100));
                                row["HrDescontar"] = Utils.ConvierteDecimalATiempo(dHr);
                            }
                            dSubTotal += dImporte;
                            break;

                        case "4":
                            dImporte = Utils.ConvierteTiempoaDecimal(row.S("Cantidad")).S().D() * txtEsperaInt.Text.S().D();
                            row["Importe"] = dImporte;
                            if (oRem.bSeDescuentaEsperaInt)
                            {
                                fTiempoT = Utils.ConvierteTiempoaDecimal(row.S("Cantidad"));
                                decimal dHr = (fTiempoT.S().D() * (oRem.dFactorEHrVueloInt / 100));
                                row["HrDescontar"] = Utils.ConvierteDecimalATiempo(dHr);
                            }
                            dSubTotal += dImporte;
                            break;

                        case "5":
                            dImporte = row.S("Cantidad").S().D() * txtTarPernoctaNal.Text.S().D();
                            row["Importe"] = dImporte;
                            if (oRem.bSeDescuentanPerNal)
                            {
                                decimal dHr = (row.S("Cantidad").I() * oRem.dFactorConvHrVueloNal);
                                row["HrDescontar"] = Utils.ConvierteDecimalATiempo(dHr);
                            }
                            dSubTotal += dImporte;
                            break;
                        case "6":
                            dImporte = row.S("Cantidad").S().D() * txtTarPernoctaInt.Text.S().D();
                            row["Importe"] = dImporte;
                            if (oRem.bSeDescuentanPerInt)
                            {
                                decimal dHr = (row.S("Cantidad").I() * oRem.dFactorConvHrVueloInt);
                                row["HrDescontar"] = Utils.ConvierteDecimalATiempo(dHr);
                            }
                            dSubTotal += dImporte;
                            break;
                    }
                    #endregion

                    switch (row.S("Concepto"))
                    {
                            
                        case "SUBTOTAL SERVICIOS DE VUELO":
                            row["Importe"] = dSubTotal;
                            row["HrDescontar"] = Utils.ObtieneTotalTiempo(dtImportes, "HrDescontar", ref fTiempoT);
                            break;
                        case "% DESC SERVICIOS VUELO":
                            dDesc = dSubTotal * (row.S("Unidad").D() / 100);
                            row["Importe"] = dDesc;
                            break;
                        case "TOTAL SERVICIOS VUELO":
                            dTotSV = dSubTotal - dDesc;
                            row["Importe"] = dTotSV;
                            break;
                        case "IVA DE VUELO":
                            dIvaC = dTotSV * (row.S("Unidad").D() / 100);
                            row["Importe"] = dIvaC;
                            break;
                        case "TOTAL SERVICIOS CON CARGO":
                            dSSC = row["Importe"].S().D();
                            break;
                        case "IVA SERVICIOS CON CARGO":
                            dISC = row["Importe"].S().D();
                            break;
                        case "TOTAL PRESUPUESTO":
                            row["Importe"] = (dTotSV + dIvaC + dSSC + dISC);
                            break;
                    }
                }

                return dtImportes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void CreaEstructuraTramos()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("IdOrigen", typeof(int));
                dt.Columns.Add("IdDestino", typeof(int));
                dt.Columns.Add("Origen");
                dt.Columns.Add("Destino");
                dt.Columns.Add("CantPax", typeof(int));
                dt.Columns.Add("FechaSalida", typeof(DateTime));
                dt.Columns.Add("FechaLlegada", typeof(DateTime));
                dt.Columns.Add("TiempoVuelo");
                dt.Columns.Add("TiempoEspera");
                dt.Columns.Add("TiempoCobrar");
                dt.Columns.Add("SeCobra", typeof(int));

                dtTramos = dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ObtieneValores()
        {
            try
            {

                if (eSearchObj != null)
                    eSearchObj(null, EventArgs.Empty);

                cboGgClientes.DataSource = dtClientes;
                cboGgClientes.ValueField = "IdCliente";
                cboGgClientes.TextField = "CodigoCliente";
                cboGgClientes.DataBind();

                cboDgModeloSolicitado.DataSource = dtGrupoModelo;
                cboDgModeloSolicitado.ValueField = "GrupoModeloId";
                cboDgModeloSolicitado.TextField = "Descripcion";
                cboDgModeloSolicitado.DataBind();


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LoadObjects(DataTable dtObjCat)
        {
            throw new NotImplementedException();
        }
        public void MostrarMensaje(string sMensaje, string sCaption)
        {
            mpeMensaje.ShowMessage(sMensaje, sCaption);
        }
        private void CargaComboAeropuertosFV(ASPxComboBox ddl, DataTable dt, string sFiltro)
        {
            try
            {
                ddl.DataSource = dt;
                ddl.TextField = iTipoFiltro == 1 ? "AeropuertoIATA" : "AeropuertoICAO";
                ddl.ValueField = "idAeropuert";
                ddl.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void AjustaTextBox(ASPxDateEdit txt, System.Drawing.Color oColor, string sTooltip)
        {
            txt.ForeColor = oColor;
            txt.ToolTip = sTooltip;
        }
        private void RecargaGridTramosPasoTres()
        {
            try
            {
                gvTramosOpc1.DataSource = dtTramos;
                gvTramosOpc1.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private string ObtieneIdAeropuerto(int iTipoBusqueda, string sCadena)
        {
            DataTable dt = new DataTable();

            switch (iTipoBusqueda)
            {
                case 1:
                    dt = new DBAeropuerto().DBSearchObj("@IATA", sCadena,
                                                   "@ICAO", string.Empty,
                                                   "@Descripcion", string.Empty,
                                                   "@estatus", 1);
                    break;
                case 2:
                    dt = new DBAeropuerto().DBSearchObj("@IATA", string.Empty,
                                                    "@ICAO", sCadena,
                                                    "@Descripcion", string.Empty,
                                                    "@estatus", 1);
                    break;
            }

            return dt.Rows.Count > 0 ? dt.Rows[0]["idAeropuert"].S() : string.Empty;
        }
        private void CalculaPresupuesto()
        {
            try
            {
                DefineFerryCobrar();

                if (eGetCalculoPresupuesto != null)
                    eGetCalculoPresupuesto(null, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ReCalculaPresupuesto()
        {
            try
            {
                DefineFerryCobrar();

                if (eGetReCalculoPresupuesto != null)
                    eGetReCalculoPresupuesto(null, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LoadServiciosC(DataTable dtImp, DataTable dtServ, DataTable dtTramosC)
        {
            try
            {
                dtTramos = ArreglaTiemposPresupuesto(dtTramosC);
                gvTramosOpc1.DataSource = dtTramos;
                gvTramosOpc1.DataBind();

                dtServiciosC = dtServ;
                gvServiciosC.DataSource = dtServiciosC;
                gvServiciosC.DataBind();

                dtImp.Columns.Add("Unidad");
                dtImp.Rows[0]["Unidad"] = "Horas";
                dtImp.Rows[1]["Unidad"] = "Horas";
                dtImp.Rows[2]["Unidad"] = "Horas";
                dtImp.Rows[3]["Unidad"] = "Horas";
                dtImp.Rows[4]["Unidad"] = "Unidad";
                dtImp.Rows[5]["Unidad"] = "Unidad";

                if (dtImp.Rows.Count > 0)
                {
                    //1.- Dolares    2.- Pesos
                    int iTipoMonedaPres = cboDgMonedaPresupueto.SelectedItem.Value.S().I();
                    decimal dDividendoConcetos = iTipoMonedaPres == 1 ? 1 : dTipoCambioDia;

                    txtTarVueloNal.Text = dtImp.Rows[0].S("TarifaDlls").D() > 0 ? Math.Round((dtImp.Rows[0].S("TarifaDlls").D() * dDividendoConcetos),2).S() : "0.00";
                    txtTarVueloInt.Text = dtImp.Rows[1].S("TarifaDlls").D() > 0 ? Math.Round((dtImp.Rows[1].S("TarifaDlls").D() * dDividendoConcetos),2).S() : "0.00";
                    txtEsperaNal.Text = dtImp.Rows[2].S("TarifaDlls").D() > 0 ? Math.Round((dtImp.Rows[2].S("TarifaDlls").D() * dDividendoConcetos),2).S() : "0.00";
                    txtEsperaInt.Text = dtImp.Rows[3].S("TarifaDlls").D() > 0 ? Math.Round((dtImp.Rows[3].S("TarifaDlls").D() * dDividendoConcetos),2).S() : "0.00";
                    txtTarPernoctaNal.Text = dtImp.Rows[4].S("TarifaDlls").D() > 0 ? Math.Round((dtImp.Rows[4].S("TarifaDlls").D() * dDividendoConcetos),2).S() : "0.00";
                    txtTarPernoctaInt.Text = dtImp.Rows[5].S("TarifaDlls").D() > 0 ? Math.Round((dtImp.Rows[5].S("TarifaDlls").D() * dDividendoConcetos), 2).S() : "0.00";
                }

                dtImportes = GetArmaTotalesImporte(dtImp);
                dtImportes = DBSetCambiaImportesPesosDolares(dtImportes);

                gvConceptos.DataSource = dtImportes;
                gvConceptos.DataBind();

                PintaTotalesConceptos();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void RecargaLoadServiciosC(DataTable dtImp, DataTable dtServ, DataTable dtTramos)
        {
            try
            {
                gvTramosOpc1.DataSource = dtTramos;
                gvTramosOpc1.DataBind();

                dtServiciosC = dtServ;
                gvServiciosC.DataSource = dtServiciosC;
                gvServiciosC.DataBind();

                dtImp.Columns.Add("Unidad");
                dtImp.Rows[0]["Unidad"] = "Horas";
                dtImp.Rows[1]["Unidad"] = "Horas";
                dtImp.Rows[2]["Unidad"] = "Horas";
                dtImp.Rows[3]["Unidad"] = "Horas";
                dtImp.Rows[4]["Unidad"] = "Unidad";
                dtImp.Rows[5]["Unidad"] = "Unidad";

                dtImportes = GetArmaTotalesImporte(dtImp);

                dtImportes = DBSetCambiaImportesPesosDolares(dtImportes);

                gvConceptos.DataSource = dtImportes;
                gvConceptos.DataBind();

                PintaTotalesConceptos();

                if (dtImp.Rows.Count > 0)
                {
                    txtTarVueloNal.Text = dtImp.Rows[0].S("TarifaDlls");
                    txtTarVueloInt.Text = dtImp.Rows[1].S("TarifaDlls");
                    txtEsperaNal.Text = dtImp.Rows[2].S("TarifaDlls");
                    txtEsperaInt.Text = dtImp.Rows[3].S("TarifaDlls");
                    txtTarPernoctaNal.Text = dtImp.Rows[4].S("TarifaDlls");
                    txtTarPernoctaInt.Text = dtImp.Rows[5].S("TarifaDlls");
                }

                lblTipoCambio.Text = "El tipo de cambio actual es: " + dTipoCambioDia.S();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private DataTable GetArmaTotalesImporte(DataTable dtImp)
        {
            try
            {
                DataTable dtNuevo = new DataTable();
                dtNuevo = dtImp.Copy();

                float fHrDescontar = 0;

                //// Row Totales
                //DataRow rowTotales = dtNuevo.NewRow();
                //rowTotales["HrDescontar"] = Utils.ObtieneTotalTiempo(dtImp, "HrDescontar", ref fHrDescontar);
                //rowTotales["Importe"] = Utils.ObtieneTotalTabla(dtImp, "Importe");
                //dtNuevo.Rows.Add(rowTotales);


                dSubTotSV = Utils.ObtieneTotalTabla(dtImp, "Importe");

                // Row SubTotal Servicios Vuelo
                DataRow rowSubTot = dtNuevo.NewRow();
                rowSubTot["Concepto"] = "SUBTOTAL SERVICIOS DE VUELO";
                rowSubTot["HrDescontar"] = Utils.ObtieneTotalTiempo(dtImp, "HrDescontar", ref fHrDescontar);
                rowSubTot["Importe"] = dSubTotSV;
                dtNuevo.Rows.Add(rowSubTot);

                // Row Descuento Serv Vuelo
                DataRow rowDescSV = dtNuevo.NewRow();
                rowDescSV["Concepto"] = "% DESC SERVICIOS VUELO";
                rowDescSV["Unidad"] = "0";
                rowDescSV["Importe"] = 0;
                dtNuevo.Rows.Add(rowDescSV);

                // Row Total Serv Vuelo
                DataRow rowTDescSV = dtNuevo.NewRow();
                rowTDescSV["Concepto"] = "TOTAL SERVICIOS VUELO";
                rowTDescSV["Importe"] = dSubTotSV + (dSubTotSV * dIvaSV).S().D();
                dtNuevo.Rows.Add(rowTDescSV);

                // Row Iva de Vuelo
                DataRow rowIvaVuelo = dtNuevo.NewRow();
                rowIvaVuelo["Concepto"] = "IVA DE VUELO";
                rowIvaVuelo["Unidad"] = (dIvaSV * 100);
                rowIvaVuelo["Importe"] = (dSubTotSV * dIvaSV).S().D();
                dtNuevo.Rows.Add(rowIvaVuelo);

                // Row Total Servicios Cargo
                DataRow rowServCargo = dtNuevo.NewRow();
                rowServCargo["Concepto"] = "TOTAL SERVICIOS CON CARGO";
                rowServCargo["Importe"] = dSubTotSC;
                dtNuevo.Rows.Add(rowServCargo);

                // Row IVA Servicios Cargo
                DataRow rowIServCargo = dtNuevo.NewRow();
                rowIServCargo["Concepto"] = "IVA SERVICIOS CON CARGO";
                decimal dIva = 0;
                dIva = Utils.GetParametrosClave("9").S().D();
                string sIVA = (dIva * 100).S();
                rowIServCargo["Unidad"] = sIVA;
                rowIServCargo["Importe"] = dSubTotSC * dIva;
                dtNuevo.Rows.Add(rowIServCargo);

                // Row Total Presupuesto
                DataRow rowTPresupuesto = dtNuevo.NewRow();
                rowTPresupuesto["Concepto"] = "TOTAL PRESUPUESTO";
                rowTPresupuesto["Importe"] = SumaTotalPresupuesto(dtNuevo);
                dtNuevo.Rows.Add(rowTPresupuesto);

                return dtNuevo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
        private decimal SumaTablaServiciosConCargo(DataTable dt)
        {
            try
            {
                decimal dSuma = 0;

                foreach (DataRow row in dt.Rows)
                {
                    if (row["ServicioConCargoDescripcion"].S() != "SubTotal")
                        dSuma += row["Importe"].S().D();
                }

                return dSuma;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private DataTable DBSetCambiaImportesPesosDolares(DataTable dtImportes)
        {
            try
            {
                if (dtImportes.Columns["TarifaDlls"] == null)
                    dtImportes.Columns.Add("TarifaDlls");

                //1.- Dolares    2.- Pesos
                int iTipoMonedaPres = cboDgMonedaPresupueto.SelectedItem.Value.S().I();
                decimal dDividendoConcetos = iTipoMonedaPres == 1 ? 1 : 1; // dTipoCambioDia;
                decimal dDividendoSC = iTipoMonedaPres == 1 ? dTipoCambioDia : 1;

                decimal dTarifa = 0;
                decimal dTotalSV = 0;
                decimal dPorDescSV = 0;
                decimal dImpDescSV = 0;
                decimal dTotalServiciosV = 0;
                decimal dPorIvaV = 0;
                decimal dImpIvaV = 0;
                decimal dTotalSC = 0;
                decimal dImpIvaSC = 0;


                dSubTotSC = dtServiciosC.Rows[dtServiciosC.Rows.Count - 1]["Importe"].S().D();

                foreach (DataRow row in dtImportes.Rows)
                {
                    #region CONCEPTO 1
                    if (row.S("IdConcepto") == "1")
                    {
                        dTarifa = txtTarVueloNal.Text.S().D() * dDividendoConcetos;

                        row["TarifaDlls"] = dTarifa;
                        float dVNal = Utils.ConvierteTiempoaDecimal(row.S("Cantidad"));
                        dTotalSV += dVNal.S().D() * dTarifa;
                        row["Importe"] = dVNal.S().D() * dTarifa;
                    }
                    #endregion

                    #region CONCEPTO 2
                    if (row.S("IdConcepto") == "2")
                    {
                        dTarifa = txtTarVueloInt.Text.S().D() * dDividendoConcetos;

                        row["TarifaDlls"] = dTarifa;
                        float dVNal = Utils.ConvierteTiempoaDecimal(row.S("Cantidad"));
                        dTotalSV += dVNal.S().D() * dTarifa;
                        row["Importe"] = dVNal.S().D() * dTarifa;
                    }
                    #endregion

                    #region CONCEPTO 3
                    if (row.S("IdConcepto") == "3")
                    {
                        dTarifa = txtEsperaNal.Text.S().D() * dDividendoConcetos;

                        row["TarifaDlls"] = dTarifa;
                        float dVNal = Utils.ConvierteTiempoaDecimal(row.S("Cantidad"));
                        dTotalSV += dVNal.S().D() * dTarifa;
                        row["Importe"] = dVNal.S().D() * dTarifa;
                    }
                    #endregion

                    #region CONCEPTO 4
                    if (row.S("IdConcepto") == "4")
                    {
                        dTarifa = txtEsperaInt.Text.S().D() * dDividendoConcetos;

                        row["TarifaDlls"] = dTarifa;
                        float dVNal = Utils.ConvierteTiempoaDecimal(row.S("Cantidad"));
                        dTotalSV += dVNal.S().D() * dTarifa;
                        row["Importe"] = dVNal.S().D() * dTarifa;
                    }
                    #endregion

                    #region CONCEPTO 5
                    if (row.S("IdConcepto") == "5")
                    {
                        dTarifa = txtTarPernoctaNal.Text.S().D() * dDividendoConcetos;

                        row["TarifaDlls"] = dTarifa;
                        decimal dVPer = row.S("Cantidad").S().D();
                        dTotalSV += dVPer.S().D() * dTarifa;
                        row["Importe"] = dVPer * dTarifa;
                    }
                    #endregion

                    #region CONCEPTO 6
                    if (row.S("IdConcepto") == "6")
                    {
                        dTarifa = txtTarPernoctaInt.Text.S().D() * dDividendoConcetos;

                        row["TarifaDlls"] = dTarifa;
                        decimal dVPer = row.S("Cantidad").S().D();
                        dTotalSV += dVPer.S().D() * dTarifa;
                        row["Importe"] = dVPer * dTarifa;
                    }
                    #endregion

                    #region SERVICIOS DE VUELO

                    if (row.S("Concepto") == "" || row.S("Concepto") == "SUBTOTAL SERVICIOS DE VUELO")
                    {
                        row["Importe"] = dTotalSV;
                    }
                    if (row.S("Concepto") == "% DESC SERVICIOS VUELO")
                    {
                        dPorDescSV = row["Unidad"].S().D();
                        dImpDescSV = (dPorDescSV / 100) * dTotalSV;
                        row["Importe"] = dImpDescSV;
                    }

                    if (row.S("Concepto") == "TOTAL SERVICIOS VUELO")
                    {
                        dTotalServiciosV = dTotalSV - dImpDescSV;
                        row["Importe"] = dTotalServiciosV;
                    }

                    if (row.S("Concepto") == "IVA DE VUELO")
                    {
                        dPorIvaV = row["Unidad"].S().D();
                        dImpIvaV = (dPorIvaV / 100) * dTotalServiciosV;
                        row["Importe"] = dImpIvaV;
                    }
                    #endregion

                    #region SERVICIOS CON CARGO
                    if (row.S("Concepto") == "TOTAL SERVICIOS CON CARGO")
                    {
                        //dTarifa = dSubTotSC / dDividendoConcetos;
                        dTotalSC = (dSubTotSC / dDividendoSC);
                        row["Importe"] = dTotalSC;
                    }
                    if (row.S("Concepto") == "IVA SERVICIOS CON CARGO")
                    {
                        decimal dIvaSC = row.S("Unidad").D();
                        dImpIvaSC = (dIvaSC / 100) * dTotalSC;
                        row["Importe"] = dImpIvaSC;
                    }
                    #endregion

                    #region TOTAL PRESUPUESTO
                    if (row.S("Concepto") == "TOTAL PRESUPUESTO")
                    {
                        row["Importe"] = SumaTotalPresupuesto(dtImportes);
                    }

                    #endregion

                }

                return dtImportes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private DataTable DBSetCambiaImportesMoneda(DataTable dtImportes)
        {
            try
            {
                if (dtImportes.Columns["TarifaDlls"] == null)
                    dtImportes.Columns.Add("TarifaDlls");

                //1.- Dolares    2.- Pesos
                int iTipoMonedaPres = cboDgMonedaPresupueto.SelectedItem.Value.S().I();
                decimal dDividendoConcetos = iTipoMonedaPres == 1 ? dTipoCambioDia : 1;
                decimal dDividendoSC = iTipoMonedaPres == 1 ? dTipoCambioDia : 1;

                decimal dTarifa = 0;
                decimal dTotalSV = 0;
                decimal dPorDescSV = 0;
                decimal dImpDescSV = 0;
                decimal dTotalServiciosV = 0;
                decimal dPorIvaV = 0;
                decimal dImpIvaV = 0;
                decimal dTotalSC = 0;
                decimal dImpIvaSC = 0;


                dSubTotSC = dtServiciosC.Rows[dtServiciosC.Rows.Count - 1]["Importe"].S().D();

                foreach (DataRow row in dtImportes.Rows)
                {
                    #region CONCEPTO 1
                    if (row.S("IdConcepto") == "1")
                    {
                        dTarifa = iTipoMonedaPres == 1 ? txtTarVueloNal.Text.S().D() / dTipoCambioDia : txtTarVueloNal.Text.S().D() * dTipoCambioDia;
                        txtTarVueloNal.Text = Math.Round(dTarifa,2).S();

                        row["TarifaDlls"] = dTarifa;
                        float dVNal = Utils.ConvierteTiempoaDecimal(row.S("Cantidad"));
                        dTotalSV += dVNal.S().D() * dTarifa;
                        row["Importe"] = dVNal.S().D() * dTarifa;
                    }
                    #endregion

                    #region CONCEPTO 2
                    if (row.S("IdConcepto") == "2")
                    {
                        dTarifa = iTipoMonedaPres == 1 ? txtTarVueloInt.Text.S().D() / dTipoCambioDia : txtTarVueloInt.Text.S().D() * dTipoCambioDia;
                        txtTarVueloInt.Text = Math.Round(dTarifa,2).S();

                        row["TarifaDlls"] = dTarifa;
                        float dVNal = Utils.ConvierteTiempoaDecimal(row.S("Cantidad"));
                        dTotalSV += dVNal.S().D() * dTarifa;
                        row["Importe"] = dVNal.S().D() * dTarifa;
                    }
                    #endregion

                    #region CONCEPTO 3
                    if (row.S("IdConcepto") == "3")
                    {
                        dTarifa = iTipoMonedaPres == 1 ? txtEsperaNal.Text.S().D() / dTipoCambioDia : txtEsperaNal.Text.S().D() * dTipoCambioDia;
                        txtEsperaNal.Text = Math.Round(dTarifa,2).S();

                        row["TarifaDlls"] = dTarifa;
                        float dVNal = Utils.ConvierteTiempoaDecimal(row.S("Cantidad"));
                        dTotalSV += dVNal.S().D() * dTarifa;
                        row["Importe"] = dVNal.S().D() * dTarifa;
                    }
                    #endregion

                    #region CONCEPTO 4
                    if (row.S("IdConcepto") == "4")
                    {
                        dTarifa = iTipoMonedaPres == 1 ? txtEsperaInt.Text.S().D() / dTipoCambioDia : txtEsperaInt.Text.S().D() * dTipoCambioDia;
                        txtEsperaInt.Text = Math.Round(dTarifa,2).S();

                        row["TarifaDlls"] = dTarifa;
                        float dVNal = Utils.ConvierteTiempoaDecimal(row.S("Cantidad"));
                        dTotalSV += dVNal.S().D() * dTarifa;
                        row["Importe"] = dVNal.S().D() * dTarifa;
                    }
                    #endregion

                    #region CONCEPTO 5
                    if (row.S("IdConcepto") == "5")
                    {
                        dTarifa = iTipoMonedaPres == 1 ? txtTarPernoctaNal.Text.S().D() / dTipoCambioDia : txtTarPernoctaNal.Text.S().D() * dTipoCambioDia;
                        txtTarPernoctaNal.Text = Math.Round(dTarifa,2).S();

                        row["TarifaDlls"] = dTarifa;
                        decimal dVPer = row.S("Cantidad").S().D();
                        dTotalSV += dVPer.S().D() * dTarifa;
                        row["Importe"] = dVPer * dTarifa;
                    }
                    #endregion

                    #region CONCEPTO 6
                    if (row.S("IdConcepto") == "6")
                    {
                        dTarifa = iTipoMonedaPres == 1 ? txtTarPernoctaInt.Text.S().D() / dTipoCambioDia : txtTarPernoctaInt.Text.S().D() * dTipoCambioDia;
                        txtTarPernoctaInt.Text = Math.Round(dTarifa,2).S();

                        row["TarifaDlls"] = dTarifa;
                        decimal dVPer = row.S("Cantidad").S().D();
                        dTotalSV += dVPer.S().D() * dTarifa;
                        row["Importe"] = dVPer * dTarifa;
                    }
                    #endregion

                    #region SERVICIOS DE VUELO

                    if (row.S("Concepto") == "" || row.S("Concepto") == "SUBTOTAL SERVICIOS DE VUELO")
                    {
                        row["Importe"] = dTotalSV;
                    }
                    if (row.S("Concepto") == "% DESC SERVICIOS VUELO")
                    {
                        dPorDescSV = row["Unidad"].S().D();
                        dImpDescSV = (dPorDescSV / 100) * dTotalSV;
                        row["Importe"] = dImpDescSV;
                    }

                    if (row.S("Concepto") == "TOTAL SERVICIOS VUELO")
                    {
                        dTotalServiciosV = dTotalSV - dImpDescSV;
                        row["Importe"] = dTotalServiciosV;
                    }

                    if (row.S("Concepto") == "IVA DE VUELO")
                    {
                        dPorIvaV = row["Unidad"].S().D();
                        dImpIvaV = (dPorIvaV / 100) * dTotalServiciosV;
                        row["Importe"] = dImpIvaV;
                    }
                    #endregion

                    #region SERVICIOS CON CARGO
                    if (row.S("Concepto") == "TOTAL SERVICIOS CON CARGO")
                    {
                        //dTarifa = dSubTotSC / dDividendoConcetos;
                        dTotalSC = (dSubTotSC / dDividendoSC);
                        row["Importe"] = dTotalSC;
                    }
                    if (row.S("Concepto") == "IVA SERVICIOS CON CARGO")
                    {
                        decimal dIvaSC = row.S("Unidad").D();
                        dImpIvaSC = (dIvaSC / 100) * dTotalSC;
                        row["Importe"] = dImpIvaSC;
                    }
                    #endregion

                    #region TOTAL PRESUPUESTO
                    if (row.S("Concepto") == "TOTAL PRESUPUESTO")
                    {
                        row["Importe"] = SumaTotalPresupuesto(dtImportes);
                    }

                    #endregion

                }

                return dtImportes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargaDatosPresupuesto()
        {
            try
            {
                if (eObjSelected != null)
                    eObjSelected(null, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private DataTable ArreglaTiemposPresupuesto(DataTable dtTramos)
        {
            try
            {
                foreach (DataRow row in dtTramos.Rows)
                {
                    row["TiempoVuelo"] = row["TiempoVuelo"].S().Length > 5 ? row["TiempoVuelo"].S().Substring(0, 5) : row["TiempoVuelo"].S();
                    row["TiempoEspera"] = row["TiempoEspera"].S().Length > 5 ? row["TiempoEspera"].S().Substring(0, 5) : row["TiempoEspera"];
                    row["TiempoCobrar"] = row["TiempoCobrar"].S().Length > 5 ? row["TiempoCobrar"].S().Substring(0, 5) : row["TiempoCobrar"];
                }

                return dtTramos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private bool ValidaTramoConTramoAnterios(string sIdOrigen, DateTime dtOrigen, ref List<ALE_MexJet.Objetos.Genericos.ValidationRes> oVldResults)
        {
            try
            {
                bool ban = true;

                if (dtTramos.Rows.Count > 0)
                {
                    string sIdDestinoA = string.Empty;

                    DateTime dtDestino = new DateTime();

                    sIdDestinoA = dtTramos.Rows[dtTramos.Rows.Count - 1]["IdDestino"].S();
                    dtDestino = dtTramos.Rows[dtTramos.Rows.Count - 1]["IdOrigen"].S().Dt();

                    dtOrigen = dtOrigen.AddSeconds(dtOrigen.Second * -1);
                    dtDestino = dtDestino.AddSeconds(dtOrigen.Second * -1);

                    if (sIdOrigen != sIdDestinoA)
                    {
                        ALE_MexJet.Objetos.Genericos.ValidationRes oVal = new Genericos.ValidationRes();
                        oVal.sMensaje = "El origen de la pierna debe ser igual al destino de la pierna anterior.";
                        oVldResults.Add(oVal);
                        ban = false;
                    }

                    if (dtOrigen > dtDestino)
                    {
                        ALE_MexJet.Objetos.Genericos.ValidationRes oVal = new Genericos.ValidationRes();
                        oVal.sMensaje = "La fecha de salida debe ser mayor o igual a la fecha de llegada de la pierna anterior.";
                        oVldResults.Add(oVal);
                        ban = false;
                    }
                }

                return ban;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void DefineFerryCobrar()
        {
            try
            {
                foreach (DataRow row in dtTramos.Rows)
                {
                    row["SeCobra"] = 1;
                }

                if (eSeCobraFerrys == Enumeraciones.SeCobraFerrys.Reposicionamiento)
                {
                    if (dtTramos.Rows.Count > 1)
                    {
                        if (hdAplicaFerryInt.Value == "0")
                        {
                            if (dtTramos.Rows[0]["CantPax"].S() == "0" && dtTramos.Rows[dtTramos.Rows.Count - 1]["CantPax"].S() == "0")
                            {
                                float fInicio = Utils.ConvierteTiempoaDecimal(dtTramos.Rows[0]["TiempoCobrar"].S());
                                float fFin = Utils.ConvierteTiempoaDecimal(dtTramos.Rows[dtTramos.Rows.Count - 1]["TiempoCobrar"].S());

                                DateTime dtSalida;
                                DateTime dtLlegada;
                                TimeSpan ts;

                                if (fInicio < fFin)
                                {
                                    dtSalida = dtTramos.Rows[0]["FechaSalida"].S().Dt();
                                    dtLlegada = dtTramos.Rows[0]["FechaLlegada"].S().Dt();

                                    ts = dtLlegada - dtSalida;

                                    dtTramos.Rows[dtTramos.Rows.Count - 1]["SeCobra"] = 0;

                                    dtTramos.Rows[0]["SeCobra"] = 1;
                                    dtTramos.Rows[0]["TiempoCobrar"] = ts.S();
                                    gvTramosOpc1.Rows[0].Cells[8].Text = ts.S();
                                }
                                else
                                {
                                    dtSalida = dtTramos.Rows[dtTramos.Rows.Count - 1]["FechaSalida"].S().Dt();
                                    dtLlegada = dtTramos.Rows[dtTramos.Rows.Count - 1]["FechaLlegada"].S().Dt();

                                    ts = dtLlegada - dtSalida;

                                    dtTramos.Rows[0]["SeCobra"] = 0;

                                    dtTramos.Rows[dtTramos.Rows.Count - 1]["SeCobra"] = 1;
                                    dtTramos.Rows[dtTramos.Rows.Count - 1]["TiempoCobrar"] = ts.S();
                                    gvTramosOpc1.Rows[dtTramos.Rows.Count - 1].Cells[8].Text = ts.S();
                                }

                            }
                            else if ((dtTramos.Rows[0]["CantPax"].S() == "0" && dtTramos.Rows[dtTramos.Rows.Count - 1]["CantPax"].S().I() > 0) ||
                                (dtTramos.Rows[0]["CantPax"].S().I() > 0 && dtTramos.Rows[dtTramos.Rows.Count - 1]["CantPax"].S() == "0"))
                            {
                                if (dtTramos.Rows[0]["CantPax"].S() == "0")
                                {
                                    dtTramos.Rows[0]["SeCobra"] = 0;
                                }
                                else
                                {
                                    dtTramos.Rows[dtTramos.Rows.Count - 1]["SeCobra"] = 0;
                                }

                            }
                        }
                    }
                }

                if (eSeCobraFerrys == Enumeraciones.SeCobraFerrys.Ninguno)
                {
                    if (hdAplicaFerryInt.Value == "0")
                    {
                        foreach (DataRow row in dtTramos.Rows)
                        {
                            if (row.S("CantPax") == "0")
                            {
                                row["SeCobra"] = 0;
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void PintaTotalesConceptos()
        {
            try
            {
                System.Drawing.Color oColor = System.Drawing.Color.DarkGray;

                decimal dPres1 = 0;
                decimal dPres2 = 0;

                foreach (GridViewRow row in gvConceptos.Rows)
                {
                    if (row.Cells[0].Text == "SUBTOTAL SERVICIOS DE VUELO")
                    {
                        row.BackColor = System.Drawing.Color.Gainsboro;
                        row.Cells[0].Font.Bold = true;
                        row.Cells[3].Font.Bold = true;
                        row.Cells[4].Font.Bold = true;
                    }
                    if (row.Cells[0].Text == "TOTAL SERVICIOS VUELO")
                    {
                        row.BackColor = System.Drawing.Color.Gainsboro;
                        row.Cells[0].Font.Bold = true;
                        row.Cells[3].Font.Bold = true;
                    }
                    if (row.Cells[0].Text == "TOTAL PRESUPUESTO")
                    {
                        row.BackColor = System.Drawing.Color.Gainsboro;
                        row.Cells[0].Font.Bold = true;
                        row.Cells[3].Font.Bold = true;

                        dPres1 = row.Cells[3].Text.Replace("$", "").Replace(",", "").S().D();
                    }
                }


                foreach (GridViewRow row in gvConceptos2.Rows)
                {
                    if (row.Cells[0].Text == "TOTAL PRESUPUESTO")
                    {
                        dPres2 = row.Cells[3].Text.Replace("$", "").Replace(",", "").S().D();
                        break;
                    }
                }

                if (dPres2 == 0)
                {
                    eOpcionesPres = OpcionesPresupuesto.Pernoctas;

                    imbPernoctas.ImageUrl = "~/img/iconos/palomita.png";
                    imbPernoctas.ToolTip = "Cotización Recomendada";
                    imbPernoctas.Width = 24;
                    imbPernoctas.Height = 24;

                    imbFerrys.ImageUrl = "~/img/iconos/tache.png";
                    imbFerrys.ToolTip = "Cotización NO Recomendada";
                    imbFerrys.Width = 24;
                    imbFerrys.Height = 30;

                    divFerrys.Visible = false;
                }
                else
                {
                    divFerrys.Visible = true;

                    if (dPres1 <= dPres2)
                    {
                        eOpcionesPres = OpcionesPresupuesto.Pernoctas;
                        imbPernoctas.ImageUrl = "~/img/iconos/palomita.png";
                        imbPernoctas.ToolTip = "Cotización Recomendada";
                        imbPernoctas.Width = 24;
                        imbPernoctas.Height = 24;

                        imbFerrys.ImageUrl = "~/img/iconos/tache.png";
                        imbFerrys.ToolTip = "Cotización NO Recomendada";
                        imbFerrys.Width = 24;
                        imbFerrys.Height = 30;
                    }
                    else
                    {
                        eOpcionesPres = OpcionesPresupuesto.FerrysVirtuales;
                        imbFerrys.ImageUrl = "~/img/iconos/palomita.png";
                        imbFerrys.ToolTip = "Cotización Recomendada";
                        imbFerrys.Width = 24;
                        imbFerrys.Height = 24;

                        imbPernoctas.ImageUrl = "~/img/iconos/tache.png";
                        imbPernoctas.ToolTip = "Cotización NO Recomendada";
                        imbPernoctas.Width = 24;
                        imbPernoctas.Height = 30;
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void PintaTotalesConceptos2()
        {
            try
            {
                System.Drawing.Color oColor = System.Drawing.Color.DarkGray;

                foreach (GridViewRow row in gvConceptos2.Rows)
                {
                    if (row.Cells[0].Text == "SUBTOTAL SERVICIOS DE VUELO")
                    {
                        row.BackColor = System.Drawing.Color.Gainsboro;
                        row.Cells[0].Font.Bold = true;
                        row.Cells[3].Font.Bold = true;
                        row.Cells[4].Font.Bold = true;
                    }
                    if (row.Cells[0].Text == "TOTAL SERVICIOS VUELO")
                    {
                        row.BackColor = System.Drawing.Color.Gainsboro;
                        row.Cells[0].Font.Bold = true;
                        row.Cells[3].Font.Bold = true;
                    }
                    if (row.Cells[0].Text == "TOTAL PRESUPUESTO")
                    {
                        row.BackColor = System.Drawing.Color.Gainsboro;
                        row.Cells[0].Font.Bold = true;
                        row.Cells[3].Font.Bold = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ActualizatiempoEspera()
        {
            try
            {
                if (dtTramos.Rows.Count > 1)
                {
                    for (int i = 0; i < dtTramos.Rows.Count - 1; i++)
                    {
                        // Calcula Tiempo de Espera entre la pierna nueva y la anterior.
                        TimeSpan ts;
                        ts = dtTramos.Rows[i]["FechaLlegada"].S().Dt() - dtTramos.Rows[i + 1]["FechaSalida"].S().Dt();

                        double dHoras = ts.TotalHours.S().Replace("-", "").S().Db();
                        double dMinutos = ts.Minutes.S().Replace("-", "").S().Db();
                        double iHoras = Math.Truncate(dHoras);
                        double iMinutos = Math.Truncate(dMinutos);

                        dtTramos.Rows[i]["TiempoEspera"] = iHoras.S().PadLeft(2, '0') + ":" + iMinutos.S().PadLeft(2, '0') + ":00";

                        if (dtTramos.Rows[i].S("Destino") == dtTramos.Rows[0].S("Origen"))
                        {
                            dtTramos.Rows[i]["TiempoEspera"] = "00:00";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private decimal SumaTotalPresupuesto(DataTable dt)
        {
            try
            {
                decimal dSuma = 0;
                foreach (DataRow row in dt.Rows)
                {
                    if (row["Concepto"].S() == "TOTAL SERVICIOS VUELO")
                        dSuma += row["Importe"].S().D();
                    if (row["Concepto"].S() == "IVA DE VUELO")
                        dSuma += row["Importe"].S().D();
                    if (row["Concepto"].S() == "TOTAL SERVICIOS CON CARGO")
                        dSuma += row["Importe"].S().D();
                    if (row["Concepto"].S() == "IVA SERVICIOS CON CARGO")
                        dSuma += row["Importe"].S().D();
                }

                dt.Rows[dt.Rows.Count - 1]["Importe"] = dSuma;

                return dSuma;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ObtieneViabilidad()
        {
            if (lstObjTramos.Count() > 0)
            {
                DataTable Tramo = dtTramos;
                object[] oViabilidad;

                foreach (TramoSolicitud objTramo in lstObjTramos)
                {
                    oViabilidad = new object[] { 
                                                    new object[] 
                                                    { 
                                                        "@FechaVuelo", objTramo.dFechaVuelo.S().Dt().ToString("MM/dd/yyyy HH:mm"),
                                                        "@Origen", objTramo.iIdAeropuertoO,
                                                        "@Destino", objTramo.iIdAeropuertoD,
                                                        "@NumPax", objTramo.iNoPax,
                                                        "@IdSolicitud", iIdSolicitudVuelo
                                                    } 
                                                };


                    if (eViabilidad != null)
                        eViabilidad(oViabilidad, null);

                    if (!bViabilidad)
                        break;

                }

                if (eInsertaMonitorDespacho != null)
                    eInsertaMonitorDespacho(null, null);

                if (bViabilidad)
                {
                    if (eGuardaSeguimientoHistorico != null)
                        eGuardaSeguimientoHistorico(null, null);
                }

                bViabilidad = false;
            }

        }
        private void AsignaFactoresPresupuesto(Presupuesto oPre)
        {
            //chkIntercambio.Checked = oPre.bFactorIntercambio;
            //chkGiraEspera.Checked = oPre.bFactorGiraEspera;
            //chkGiraHorario.Checked = oPre.bFactorGiraHorario;
            //chkFechaPico.Checked = oPre.bFactorFechaPico;

            hdIntercambio.Value = oPre.dFactorIntercambio.S();
            hdFechaPico.Value = oPre.dFactorFechaPico.S();
            hdGiraEspera.Value = oPre.dGiraEspera.S();
            hdGiraHorario.Value = oPre.dGiraHorario.S();
            hdFactorTramoNal.Value = oPre.dFactorTramoNal.S();
            hdFactorTramoInt.Value = oPre.dFactorTramoInt.S();

            // NUEVA FORMA DE MOSTRAR FACTORES
            ListItem lI = chkList.Items.FindByValue("1");
            lI.Text = "Intercambio - " + oPre.dFactorIntercambio.S();
            lI.Selected = oPre.bFactorIntercambio;

            ListItem lGe = chkList.Items.FindByValue("2");
            lGe.Text = "Gira Espera - " + oPre.dGiraEspera.S();
            lGe.Selected = oPre.bFactorGiraEspera;

            ListItem lGh = chkList.Items.FindByValue("3");
            lGh.Text = "Gira Horario - " + oPre.dGiraHorario.S();
            lGh.Selected = oPre.bFactorGiraHorario;

            ListItem lFp = chkList.Items.FindByValue("4");
            lFp.Text = "Fecha Pico - " + oPre.dFactorFechaPico.S();
            lFp.Selected = oPre.bFactorFechaPico;

            ListItem lFTN = chkList.Items.FindByValue("5");
            lFTN.Text = "Tramo Nacional - " + oPre.dFactorTramoNal.S();
            lFTN.Selected = oPre.bFactorTramoNal;

            ListItem lFTI = chkList.Items.FindByValue("6");
            lFTI.Text = "Tramo Internacional - " + oPre.dFactorTramoInt.S();
            lFTI.Selected = oPre.bFactorTramoInt;
        }
        public void MuestraTextoDosPresupuestos(string sTexto)
        {
            lblMensajeDosPresupuestos.Text = sTexto;
        }

        
        /// METODOS DEL SEGUNDO PRESUPUESTO
        private DataTable GetArmaTotalesImporteDosPresupuestos(DataTable dtImp)
        {
            try
            {
                DataTable dtNuevo = new DataTable();
                dtNuevo = dtImp.Copy();

                float fHrDescontar = 0;



                dSubTotSV2 = Utils.ObtieneTotalTabla(dtImp, "Importe");

                // Row SubTotal Servicios Vuelo
                DataRow rowSubTot = dtNuevo.NewRow();
                rowSubTot["Concepto"] = "SUBTOTAL SERVICIOS DE VUELO";
                rowSubTot["HrDescontar"] = Utils.ObtieneTotalTiempo(dtImp, "HrDescontar", ref fHrDescontar);
                rowSubTot["Importe"] = dSubTotSV2;
                dtNuevo.Rows.Add(rowSubTot);

                // Row Descuento Serv Vuelo
                DataRow rowDescSV = dtNuevo.NewRow();
                rowDescSV["Concepto"] = "% DESC SERVICIOS VUELO";
                rowDescSV["Unidad"] = "0";
                rowDescSV["Importe"] = 0;
                dtNuevo.Rows.Add(rowDescSV);

                // Row Total Serv Vuelo
                DataRow rowTDescSV = dtNuevo.NewRow();
                rowTDescSV["Concepto"] = "TOTAL SERVICIOS VUELO";
                rowTDescSV["Importe"] = dSubTotSV2 + (dSubTotSV2 * dIvaSV).S().D();
                dtNuevo.Rows.Add(rowTDescSV);

                // Row Iva de Vuelo
                DataRow rowIvaVuelo = dtNuevo.NewRow();
                rowIvaVuelo["Concepto"] = "IVA DE VUELO";
                rowIvaVuelo["Unidad"] = (dIvaSV * 100);
                rowIvaVuelo["Importe"] = (dSubTotSV2 * dIvaSV).S().D();
                dtNuevo.Rows.Add(rowIvaVuelo);

                // Row Total Servicios Cargo
                DataRow rowServCargo = dtNuevo.NewRow();
                rowServCargo["Concepto"] = "TOTAL SERVICIOS CON CARGO";
                rowServCargo["Importe"] = dSubTotSC2;
                dtNuevo.Rows.Add(rowServCargo);

                // Row IVA Servicios Cargo
                DataRow rowIServCargo = dtNuevo.NewRow();
                rowIServCargo["Concepto"] = "IVA SERVICIOS CON CARGO";
                decimal dIva = 0;
                dIva = Utils.GetParametrosClave("9").S().D();
                string sIVA = (dIva * 100).S();
                rowIServCargo["Unidad"] = sIVA;
                rowIServCargo["Importe"] = dSubTotSC2 * dIva;
                dtNuevo.Rows.Add(rowIServCargo);

                // Row Total Presupuesto
                DataRow rowTPresupuesto = dtNuevo.NewRow();
                rowTPresupuesto["Concepto"] = "TOTAL PRESUPUESTO";
                rowTPresupuesto["Importe"] = SumaTotalPresupuesto(dtNuevo);
                dtNuevo.Rows.Add(rowTPresupuesto);

                return dtNuevo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LoadServiciosDosPresupuestos(DataTable dtImp, DataTable dtServ, DataTable dtTramosC)
        {
            try
            {
                dtTramos2 = ArreglaTiemposPresupuesto(dtTramosC);

                gvTramosOpc2.DataSource = dtTramos2;
                gvTramosOpc2.DataBind();

                dtServiciosC2 = dtServ;

                gvServiciosC2.DataSource = dtServiciosC2;
                gvServiciosC2.DataBind();

                dtImp.Columns.Add("Unidad");
                dtImp.Rows[0]["Unidad"] = "Horas";
                dtImp.Rows[1]["Unidad"] = "Horas";
                dtImp.Rows[2]["Unidad"] = "Horas";
                dtImp.Rows[3]["Unidad"] = "Horas";
                dtImp.Rows[4]["Unidad"] = "Unidad";
                dtImp.Rows[5]["Unidad"] = "Unidad";


                dtImportes2 = GetArmaTotalesImporteDosPresupuestos(dtImp);
                dtImportes2 = DBSetCambiaImportesPesosDolaresDosPres(dtImportes2);

                gvConceptos2.DataSource = dtImportes2;
                gvConceptos2.DataBind();

                PintaTotalesConceptos2();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private DataTable DBSetCambiaImportesPesosDolaresDosPres(DataTable dtImportes)
        {
            try
            {
                if (dtImportes.Columns["TarifaDlls"] == null)
                    dtImportes.Columns.Add("TarifaDlls");

                //1.- Dolares    2.- Pesos
                int iTipoMonedaPres = cboDgMonedaPresupueto.SelectedItem.Value.S().I();
                decimal dDividendoConcetos = iTipoMonedaPres == 1 ? 1 : 1; // dTipoCambioDia;
                decimal dDividendoSC = iTipoMonedaPres == 1 ? dTipoCambioDia : 1;

                decimal dTarifa = 0;
                decimal dTotalSV = 0;
                decimal dPorDescSV = 0;
                decimal dImpDescSV = 0;
                decimal dTotalServiciosV = 0;
                decimal dPorIvaV = 0;
                decimal dImpIvaV = 0;
                decimal dTotalSC = 0;
                decimal dImpIvaSC = 0;


                dSubTotSC2 = dtServiciosC2.Rows[dtServiciosC2.Rows.Count - 1]["Importe"].S().D();

                foreach (DataRow row in dtImportes2.Rows)
                {
                    #region CONCEPTO 1
                    if (row.S("IdConcepto") == "1")
                    {
                        dTarifa = txtTarVueloNal.Text.S().D() * dDividendoConcetos;

                        row["TarifaDlls"] = dTarifa;
                        float dVNal = Utils.ConvierteTiempoaDecimal(row.S("Cantidad"));
                        dTotalSV += dVNal.S().D() * dTarifa;
                        row["Importe"] = dVNal.S().D() * dTarifa;
                    }
                    #endregion

                    #region CONCEPTO 2
                    if (row.S("IdConcepto") == "2")
                    {
                        dTarifa = txtTarVueloInt.Text.S().D() * dDividendoConcetos;

                        row["TarifaDlls"] = dTarifa;
                        float dVNal = Utils.ConvierteTiempoaDecimal(row.S("Cantidad"));
                        dTotalSV += dVNal.S().D() * dTarifa;
                        row["Importe"] = dVNal.S().D() * dTarifa;
                    }
                    #endregion

                    #region CONCEPTO 3
                    if (row.S("IdConcepto") == "3")
                    {
                        dTarifa = txtEsperaNal.Text.S().D() * dDividendoConcetos;

                        row["TarifaDlls"] = dTarifa;
                        float dVNal = Utils.ConvierteTiempoaDecimal(row.S("Cantidad"));
                        dTotalSV += dVNal.S().D() * dTarifa;
                        row["Importe"] = dVNal.S().D() * dTarifa;
                    }
                    #endregion

                    #region CONCEPTO 4
                    if (row.S("IdConcepto") == "4")
                    {
                        dTarifa = txtEsperaInt.Text.S().D() * dDividendoConcetos;

                        row["TarifaDlls"] = dTarifa;
                        float dVNal = Utils.ConvierteTiempoaDecimal(row.S("Cantidad"));
                        dTotalSV += dVNal.S().D() * dTarifa;
                        row["Importe"] = dVNal.S().D() * dTarifa;
                    }
                    #endregion

                    #region CONCEPTO 5
                    if (row.S("IdConcepto") == "5")
                    {
                        dTarifa = txtTarPernoctaNal.Text.S().D() * dDividendoConcetos;

                        row["TarifaDlls"] = dTarifa;
                        decimal dVPer = row.S("Cantidad").S().D();
                        dTotalSV += dVPer.S().D() * dTarifa;
                        row["Importe"] = dVPer * dTarifa;
                    }
                    #endregion

                    #region CONCEPTO 6
                    if (row.S("IdConcepto") == "6")
                    {
                        dTarifa = txtTarPernoctaInt.Text.S().D() * dDividendoConcetos;

                        row["TarifaDlls"] = dTarifa;
                        decimal dVPer = row.S("Cantidad").S().D();
                        dTotalSV += dVPer.S().D() * dTarifa;
                        row["Importe"] = dVPer * dTarifa;
                    }
                    #endregion

                    #region SERVICIOS DE VUELO

                    if (row.S("Concepto") == "" || row.S("Concepto") == "SUBTOTAL SERVICIOS DE VUELO")
                    {
                        row["Importe"] = dTotalSV;
                    }
                    if (row.S("Concepto") == "% DESC SERVICIOS VUELO")
                    {
                        dPorDescSV = row["Unidad"].S().D();
                        dImpDescSV = (dPorDescSV / 100) * dTotalSV;
                        row["Importe"] = dImpDescSV;
                    }

                    if (row.S("Concepto") == "TOTAL SERVICIOS VUELO")
                    {
                        dTotalServiciosV = dTotalSV - dImpDescSV;
                        row["Importe"] = dTotalServiciosV;
                    }

                    if (row.S("Concepto") == "IVA DE VUELO")
                    {
                        dPorIvaV = row["Unidad"].S().D();
                        dImpIvaV = (dPorIvaV / 100) * dTotalServiciosV;
                        row["Importe"] = dImpIvaV;
                    }
                    #endregion

                    #region SERVICIOS CON CARGO
                    if (row.S("Concepto") == "TOTAL SERVICIOS CON CARGO")
                    {
                        //dTarifa = dSubTotSC / dDividendoConcetos;
                        dTotalSC = (dSubTotSC2 / dDividendoSC);
                        row["Importe"] = dTotalSC;
                    }
                    if (row.S("Concepto") == "IVA SERVICIOS CON CARGO")
                    {
                        decimal dIvaSC = row.S("Unidad").D();
                        dImpIvaSC = (dIvaSC / 100) * dTotalSC;
                        row["Importe"] = dImpIvaSC;
                    }
                    #endregion

                    #region TOTAL PRESUPUESTO
                    if (row.S("Concepto") == "TOTAL PRESUPUESTO")
                    {
                        row["Importe"] = SumaTotalPresupuesto(dtImportes);
                    }

                    #endregion

                }

                return dtImportes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void AbreSegundoPresupuesto()
        {
            string m = "AbreSegundoPresupuesto();";
            ScriptManager.RegisterClientScriptBlock(this, typeof(System.Web.UI.Page), "AbreSegundoPresupuesto", m, true);
        }
        private void DefineFerryCobrar2()
        {
            try
            {
                foreach (DataRow row in dtTramos2.Rows)
                {
                    row["SeCobra"] = 1;
                }

                if (eSeCobraFerrys == Enumeraciones.SeCobraFerrys.Reposicionamiento)
                {
                    if (dtTramos2.Rows.Count > 1)
                    {
                        if (hdAplicaFerryInt.Value == "0")
                        {
                            if (dtTramos.Rows[0]["CantPax"].S() == "0" && dtTramos2.Rows[dtTramos2.Rows.Count - 1]["CantPax"].S() == "0")
                            {
                                float fInicio = Utils.ConvierteTiempoaDecimal(dtTramos2.Rows[0]["TiempoCobrar"].S());
                                float fFin = Utils.ConvierteTiempoaDecimal(dtTramos2.Rows[dtTramos2.Rows.Count - 1]["TiempoCobrar"].S());

                                DateTime dtSalida;
                                DateTime dtLlegada;
                                TimeSpan ts;

                                if (fInicio < fFin)
                                {
                                    dtSalida = dtTramos2.Rows[0]["FechaSalida"].S().Dt();
                                    dtLlegada = dtTramos2.Rows[0]["FechaLlegada"].S().Dt();

                                    ts = dtLlegada - dtSalida;

                                    dtTramos2.Rows[dtTramos2.Rows.Count - 1]["SeCobra"] = 0;

                                    dtTramos2.Rows[0]["SeCobra"] = 1;
                                    dtTramos2.Rows[0]["TiempoCobrar"] = ts.S();
                                    gvTramosOpc2.Rows[0].Cells[8].Text = ts.S();
                                }
                                else
                                {
                                    dtSalida = dtTramos2.Rows[dtTramos2.Rows.Count - 1]["FechaSalida"].S().Dt();
                                    dtLlegada = dtTramos2.Rows[dtTramos2.Rows.Count - 1]["FechaLlegada"].S().Dt();

                                    ts = dtLlegada - dtSalida;

                                    dtTramos2.Rows[0]["SeCobra"] = 0;

                                    dtTramos2.Rows[dtTramos2.Rows.Count - 1]["SeCobra"] = 1;
                                    dtTramos2.Rows[dtTramos2.Rows.Count - 1]["TiempoCobrar"] = ts.S();
                                    gvTramosOpc2.Rows[dtTramos2.Rows.Count - 1].Cells[8].Text = ts.S();
                                }

                            }
                            else if ((dtTramos2.Rows[0]["CantPax"].S() == "0" && dtTramos2.Rows[dtTramos2.Rows.Count - 1]["CantPax"].S().I() > 0) ||
                                (dtTramos2.Rows[0]["CantPax"].S().I() > 0 && dtTramos2.Rows[dtTramos2.Rows.Count - 1]["CantPax"].S() == "0"))
                            {
                                if (dtTramos2.Rows[0]["CantPax"].S() == "0")
                                {
                                    dtTramos2.Rows[0]["SeCobra"] = 0;
                                }
                                else
                                {
                                    dtTramos2.Rows[dtTramos2.Rows.Count - 1]["SeCobra"] = 0;
                                }

                            }
                        }
                    }
                }

                if (eSeCobraFerrys == Enumeraciones.SeCobraFerrys.Ninguno)
                {
                    if (hdAplicaFerryInt.Value == "0")
                    {
                        foreach (DataRow row in dtTramos2.Rows)
                        {
                            if (row.S("CantPax") == "0")
                            {
                                row["SeCobra"] = 0;
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ActualizatiempoEspera2()
        {
            try
            {
                if (dtTramos2.Rows.Count > 1)
                {
                    for (int i = 0; i < dtTramos2.Rows.Count - 1; i++)
                    {
                        // Calcula Tiempo de Espera entre la pierna nueva y la anterior.
                        TimeSpan ts;
                        ts = dtTramos2.Rows[i]["FechaLlegada"].S().Dt() - dtTramos2.Rows[i + 1]["FechaSalida"].S().Dt();

                        double dHoras = ts.TotalHours.S().Replace("-", "").S().Db();
                        double dMinutos = ts.Minutes.S().Replace("-", "").S().Db();
                        double iHoras = Math.Truncate(dHoras);
                        double iMinutos = Math.Truncate(dMinutos);

                        dtTramos2.Rows[i]["TiempoEspera"] = iHoras.S().PadLeft(2, '0') + ":" + iMinutos.S().PadLeft(2, '0') + ":00";

                        if (dtTramos2.Rows[i].S("Destino") == dtTramos2.Rows[0].S("Origen"))
                        {
                            dtTramos2.Rows[i]["TiempoEspera"] = "00:00";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void RecargaGridTramosPasoTres2()
        {
            try
            {
                gvTramosOpc2.DataSource = dtTramos2;
                gvTramosOpc2.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CalculaPresupuesto2()
        {
            try
            {
                DefineFerryCobrar2();

                if (eGetCalculoPresupuesto2 != null)
                    eGetCalculoPresupuesto2(null, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ReCalculaPresupuesto2()
        {
            try
            {
                DefineFerryCobrar2();

                if (eGetReCalculoPresupuesto2 != null)
                    eGetReCalculoPresupuesto2(null, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private DataTable DBSetCambiaImportesMoneda2(DataTable dtImportes)
        {
            try
            {
                if (dtImportes.Columns["TarifaDlls"] == null)
                    dtImportes.Columns.Add("TarifaDlls");

                //1.- Dolares    2.- Pesos
                int iTipoMonedaPres = cboDgMonedaPresupueto.SelectedItem.Value.S().I();
                decimal dDividendoConcetos = iTipoMonedaPres == 1 ? dTipoCambioDia : 1;
                decimal dDividendoSC = iTipoMonedaPres == 1 ? dTipoCambioDia : 1;

                decimal dTarifa = 0;
                decimal dTotalSV = 0;
                decimal dPorDescSV = 0;
                decimal dImpDescSV = 0;
                decimal dTotalServiciosV = 0;
                decimal dPorIvaV = 0;
                decimal dImpIvaV = 0;
                decimal dTotalSC = 0;
                decimal dImpIvaSC = 0;


                dSubTotSC2 = dtServiciosC2.Rows[dtServiciosC2.Rows.Count - 1]["Importe"].S().D();

                foreach (DataRow row in dtImportes.Rows)
                {
                    #region CONCEPTO 1
                    if (row.S("IdConcepto") == "1")
                    {
                        dTarifa = txtTarVueloNal.Text.S().D();
                        
                        row["TarifaDlls"] = dTarifa;
                        float dVNal = Utils.ConvierteTiempoaDecimal(row.S("Cantidad"));
                        dTotalSV += dVNal.S().D() * dTarifa;
                        row["Importe"] = dVNal.S().D() * dTarifa;
                    }
                    #endregion

                    #region CONCEPTO 2
                    if (row.S("IdConcepto") == "2")
                    {
                        dTarifa = txtTarVueloInt.Text.S().D();
                        
                        row["TarifaDlls"] = dTarifa;
                        float dVNal = Utils.ConvierteTiempoaDecimal(row.S("Cantidad"));
                        dTotalSV += dVNal.S().D() * dTarifa;
                        row["Importe"] = dVNal.S().D() * dTarifa;
                    }
                    #endregion

                    #region CONCEPTO 3
                    if (row.S("IdConcepto") == "3")
                    {
                        dTarifa = txtEsperaNal.Text.S().D();
                        
                        row["TarifaDlls"] = dTarifa;
                        float dVNal = Utils.ConvierteTiempoaDecimal(row.S("Cantidad"));
                        dTotalSV += dVNal.S().D() * dTarifa;
                        row["Importe"] = dVNal.S().D() * dTarifa;
                    }
                    #endregion

                    #region CONCEPTO 4
                    if (row.S("IdConcepto") == "4")
                    {
                        dTarifa = txtEsperaInt.Text.S().D();
                        
                        row["TarifaDlls"] = dTarifa;
                        float dVNal = Utils.ConvierteTiempoaDecimal(row.S("Cantidad"));
                        dTotalSV += dVNal.S().D() * dTarifa;
                        row["Importe"] = dVNal.S().D() * dTarifa;
                    }
                    #endregion

                    #region CONCEPTO 5
                    if (row.S("IdConcepto") == "5")
                    {
                        dTarifa = txtTarPernoctaNal.Text.S().D();
                        
                        row["TarifaDlls"] = dTarifa;
                        decimal dVPer = row.S("Cantidad").S().D();
                        dTotalSV += dVPer.S().D() * dTarifa;
                        row["Importe"] = dVPer * dTarifa;
                    }
                    #endregion

                    #region CONCEPTO 6
                    if (row.S("IdConcepto") == "6")
                    {
                        dTarifa = txtTarPernoctaInt.Text.S().D();
                        
                        row["TarifaDlls"] = dTarifa;
                        decimal dVPer = row.S("Cantidad").S().D();
                        dTotalSV += dVPer.S().D() * dTarifa;
                        row["Importe"] = dVPer * dTarifa;
                    }
                    #endregion

                    #region SERVICIOS DE VUELO

                    if (row.S("Concepto") == "" || row.S("Concepto") == "SUBTOTAL SERVICIOS DE VUELO")
                    {
                        row["Importe"] = dTotalSV;
                    }
                    if (row.S("Concepto") == "% DESC SERVICIOS VUELO")
                    {
                        dPorDescSV = row["Unidad"].S().D();
                        dImpDescSV = (dPorDescSV / 100) * dTotalSV;
                        row["Importe"] = dImpDescSV;
                    }

                    if (row.S("Concepto") == "TOTAL SERVICIOS VUELO")
                    {
                        dTotalServiciosV = dTotalSV - dImpDescSV;
                        row["Importe"] = dTotalServiciosV;
                    }

                    if (row.S("Concepto") == "IVA DE VUELO")
                    {
                        dPorIvaV = row["Unidad"].S().D();
                        dImpIvaV = (dPorIvaV / 100) * dTotalServiciosV;
                        row["Importe"] = dImpIvaV;
                    }
                    #endregion

                    #region SERVICIOS CON CARGO
                    if (row.S("Concepto") == "TOTAL SERVICIOS CON CARGO")
                    {
                        //dTarifa = dSubTotSC / dDividendoConcetos;
                        dTotalSC = (dSubTotSC2 / dDividendoSC);
                        row["Importe"] = dTotalSC;
                    }
                    if (row.S("Concepto") == "IVA SERVICIOS CON CARGO")
                    {
                        decimal dIvaSC = row.S("Unidad").D();
                        dImpIvaSC = (dIvaSC / 100) * dTotalSC;
                        row["Importe"] = dImpIvaSC;
                    }
                    #endregion

                    #region TOTAL PRESUPUESTO
                    if (row.S("Concepto") == "TOTAL PRESUPUESTO")
                    {
                        row["Importe"] = SumaTotalPresupuesto(dtImportes);
                    }

                    #endregion

                }

                return dtImportes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region VARIABLES Y PROPIEDADES

        Presupuesto_Presenter oPresenter;
        private const string sPagina = "frmPresupuesto.aspx";
        private const string sClase = "frmPresupuesto.aspx.cs";

        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eGetDatosContrato;
        public event EventHandler eSearchObj;
        public event EventHandler eGetDatosCliente;
        public event EventHandler eLoadOrigDestFiltro;
        public event EventHandler eLoadOrigDestFiltroDest;
        public event EventHandler eGetCalculoPresupuesto;
        public event EventHandler eGetReCalculoPresupuesto;
        public event EventHandler eViabilidad;
        public event EventHandler eInsertaMonitorDespacho;
        public event EventHandler eGuardaSeguimientoHistorico;
        public event EventHandler eSaveSolicitud;
		public event EventHandler eSavetramos;
        // DOBLE PRESUPUESTO
        public event EventHandler eGetCalculoPresupuesto2;
        public event EventHandler eGetReCalculoPresupuesto2;

		public DataTable dtTramos
        {
            get { return (DataTable)ViewState["VSdtTramos"]; }
            set { ViewState["VSdtTramos"] = value; }
        }
        public DataTable dtTramos2
        {
            get { return (DataTable)ViewState["VSdtTramos2"]; }
            set { ViewState["VSdtTramos2"] = value; }
        }
        public DataTable dtClientes
        {
            get { return (DataTable)ViewState["dtClientes"]; }
            set { ViewState["dtClientes"] = value; }
        }
        public DataTable dtContactos
        {
            get { return (DataTable)ViewState["dtContactos"]; }
            set { ViewState["dtContactos"] = value; }
        }
        public int iIdCliente
        {
            get { return cboGgClientes.Value.I(); }
            set { ViewState["iIdCliente"] = value; }
        }
        public DataTable dtContrato
        {
            get { return (DataTable)ViewState["dtContrato"]; }
            set { ViewState["dtContrato"] = value; }
        }
        public DataTable dtSolicitante
        {
            get { return (DataTable)ViewState["dtSolicitante"]; }
            set { ViewState["dtSolicitante"] = value; }
        }
        public DataTable dtGrupoModelo
        {
            get { return (DataTable)ViewState["dtGrupoModelo"]; }
            set { ViewState["dtGrupoModelo"] = value; }
        }
        public int iIdContrato
        {
            get
            {
                return cboDgContrato.Value.S().I();
            }
        }
        public int iTipoFiltro
        {
            get
            {
                return rdlTarIAta.Checked ? 1 : 2;
            }

        }
        public string sAeropuertoO
        {
            get
            {
                return ddlOrigenFV.Text.S();
            }
        }
        public string sFiltroO
        {
            get { return (string)ViewState["VSFiltroO"]; }
            set { ViewState["VSFiltroO"] = value; }
        }
        public string sFiltroD
        {
            get { return (string)ViewState["VSFiltroD"]; }
            set { ViewState["VSFiltroD"] = value; }
        }
        public DateTime dtFechaFerruyV
        {
            get { return (DateTime)ViewState["VSFechaFV"]; }
            set { ViewState["VSFechaFV"] = value; }
        }
        public DataTable dtOrigen
        {
            get { return (DataTable)ViewState["VSOrigen"]; }
            set { ViewState["VSOrigen"] = value; }
        }
        public DataTable dtDestino
        {
            get { return (DataTable)ViewState["VSDestino"]; }
            set { ViewState["VSDestino"] = value; }
        }
        public string sRutaTramos
        {
            get
            {
                try
                {
                    string sInicio = string.Empty;
                    for (int i = 0; i < dtTramos.Rows.Count; i++)
                    {
                        if (i == 0)
                        {
                            sInicio = dtTramos.Rows[i]["Origen"].S() + "-" + dtTramos.Rows[i]["Destino"].S();
                        }
                        else
                        {
                            sInicio += "-" + dtTramos.Rows[i]["Destino"].S();
                        }
                    }

                    return sInicio;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public string sRutaTramos2
        {
            get
            {
                try
                {
                    string sInicio = string.Empty;
                    for (int i = 0; i < dtTramos2.Rows.Count; i++)
                    {
                        if (i == 0)
                        {
                            sInicio = dtTramos2.Rows[i]["Origen"].S() + "-" + dtTramos2.Rows[i]["Destino"].S();
                        }
                        else
                        {
                            sInicio += "-" + dtTramos2.Rows[i]["Destino"].S();
                        }
                    }

                    return sInicio;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public int iIdGrupoModelo
        {
            get
            {
                return cboDgModeloSolicitado.Value.S().I();
            }
        }
        public decimal dIvaSV
        {
            get { return (decimal)ViewState["VSIvaSV"]; }
            set { ViewState["VSIvaSV"] = value; }
        }
        public string sIdGrupoModeloPre
        {
            get
            {
                return cboDgModeloSolicitado.Text.S();
            }
        }
        public decimal dSubTotSV
        {
            get { return (decimal)ViewState["VSSubTSV"]; }
            set { ViewState["VSSubTSV"] = value; }
        }
        public decimal dSubTotSV2
        {
            get { return (decimal)ViewState["VSSubTSV2"]; }
            set { ViewState["VSSubTSV2"] = value; }
        }
        public string sDescSV
        {
            get { return (string)ViewState["VSDescSV"]; }
            set { ViewState["VSDescSV"] = value; }
        }
        public DataTable dtImportes
        {
            get { return (DataTable)ViewState["VSdtImp"]; }
            set { ViewState["VSdtImp"] = value; }
        }
        public DataTable dtImportes2
        {
            get { return (DataTable)ViewState["VSdtImp2"]; }
            set { ViewState["VSdtImp2"] = value; }
        }
        public DataTable dtServiciosC
        {
            get { return (DataTable)ViewState["VSServiciosC"]; }
            set { ViewState["VSServiciosC"] = value; }
        }
        public DataTable dtServiciosC2
        {
            get { return (DataTable)ViewState["VSServiciosC2"]; }
            set { ViewState["VSServiciosC2"] = value; }
        }
        public decimal dSubTotSC
        {
            get { return (decimal)ViewState["VSSubTSC"]; }
            set { ViewState["VSSubTSC"] = value; }
        }
        public decimal dSubTotSC2
        {
            get { return (decimal)ViewState["VSSubTSC2"]; }
            set { ViewState["VSSubTSC2"] = value; }
        }
        public decimal dIvaSC
        {
            get { return (decimal)ViewState["VSdIvaSC"]; }
            set { ViewState["VSdIvaSC"] = value; }
        }
        public Presupuesto oPresupuesto
        {
            get
            {
                Presupuesto oPre = new Presupuesto();
                oPre.iIdPresupuesto = iIdPresupuesto;
                oPre.dtFechaPresupuesto = dteDgFechaPresupuesto.Text.S().Dt();
                oPre.iDiasVigencia = txtDgDiasVigencia.Text.S().I();
                oPre.iIdContrato = cboDgContrato.SelectedItem.Value.S().I();
                oPre.sCompaniaImpresion = txtDgCompaniaImpresion.Text.S();
                oPre.iIdSolicitante = cboDgSolicitante.SelectedItem == null ? 0 : cboDgSolicitante.SelectedItem.Value.S().I();
                oPre.sNombreSolicitante = txtDgNombreSolicitante.Text.S();
                oPre.sTelefono = txtDgTelefono.Text.S();
                oPre.sEmail = txtDgCorreo.Text.S();
                oPre.iIdGrupoModeloSol = cboDgModeloSolicitado.SelectedItem.Value.S().I();
                oPre.iIdMonedaPresupuesto = cboDgMonedaPresupueto.SelectedItem.Value.S().I();
                oPre.dVueloNal = txtTarVueloNal.Text.S().D();
                oPre.dVueloInt = txtTarVueloInt.Text.S().D();
                oPre.dEsperaNal = txtEsperaNal.Text.S().D();
                oPre.dEsperaInt = txtEsperaInt.Text.S().D();
                oPre.dPernoctaNal = txtTarPernoctaNal.Text.S().D();
                oPre.dPernoctaInt = txtTarPernoctaInt.Text.S().D();
                oPre.iIdSiglasAeropuerto = rdlTarIAta.Checked ? 1 : 2;
                
                //oPre.bFactorIntercambio = chkIntercambio.Checked;
                //oPre.bFactorGiraEspera = chkGiraEspera.Checked;
                //oPre.bFactorGiraHorario = chkGiraHorario.Checked;
                //oPre.bFactorFechaPico = chkFechaPico.Checked;
                oPre.bFactorIntercambio = chkList.Items.FindByValue("1").Selected;
                oPre.bFactorGiraEspera = chkList.Items.FindByValue("2").Selected;
                oPre.bFactorGiraHorario = chkList.Items.FindByValue("3").Selected;
                oPre.bFactorFechaPico = chkList.Items.FindByValue("4").Selected;
                oPre.bFactorTramoNal = chkList.Items.FindByValue("5").Selected;
                oPre.bFactorTramoInt = chkList.Items.FindByValue("6").Selected;

                
                oPre.dFactorIntercambio = hdIntercambio.Value.S().D();
                oPre.dFactorFechaPico = hdFechaPico.Value.S().D();
                oPre.dGiraEspera = hdGiraEspera.Value.S().D();
                oPre.dGiraHorario = hdGiraHorario.Value.S().D();
                oPre.dFactorTramoNal = hdFactorTramoNal.Value.S().D();
                oPre.dFactorTramoInt = hdFactorTramoInt.Value.S().D();
                
                oPre.sObservaciones = txtObservaciones.Text.S();
                oPre.dTipoCambio = dTipoCambioDia;

                oPre.iIdSolicitudVuelo = iIdSolicitudVuelo == null ? 0 : iIdSolicitudVuelo;

                if (eOpcionesPres == OpcionesPresupuesto.Pernoctas)
                {
                    oPre.dtTramos = dtTramos;
                    oPre.dtServicios = dtServiciosC;
                    oPre.dtConceptos = dtImportes;
                }
                else
                {
                    oPre.dtTramos = dtTramos2;
                    oPre.dtServicios = dtServiciosC2;
                    oPre.dtConceptos = dtImportes2;
                }

                return oPre;
            }
            set
            {
                Presupuesto oPre = value;
                if (oPre != null)
                {
                    iIdPresupuesto = oPre.iIdPresupuesto;
                    txtDgDiasVigencia.Text = oPre.iDiasVigencia.S();
                    cboGgClientes.Value = oPre.iIdCliente.S();
                    cboGgClientes_SelectedIndexChanged(null, EventArgs.Empty);
                    cboDgContrato.Value = oPre.iIdContrato.S();
                    cboDgContrato_SelectedIndexChanged(null, EventArgs.Empty);
                    txtDgCompaniaImpresion.Text = oPre.sCompaniaImpresion.S();
                    cboDgSolicitante.Value = oPre.iIdSolicitante.S();
                    txtDgNombreSolicitante.Text = oPre.sNombreSolicitante.S();
                    txtDgTelefono.Text = oPre.sTelefono.S();
                    txtDgCorreo.Text = oPre.sEmail.S();
                    cboDgModeloSolicitado.Value = oPre.iIdGrupoModeloSol.S();
                    cboDgMonedaPresupueto.Value = oPre.iIdMonedaPresupuesto.S();
                    txtTarVueloNal.Text = oPre.dVueloNal.S();
                    txtTarVueloInt.Text = oPre.dVueloInt.S();
                    txtEsperaNal.Text = oPre.dEsperaNal.S();
                    txtEsperaInt.Text = oPre.dEsperaInt.S();
                    txtTarPernoctaNal.Text = oPre.dPernoctaNal.S();
                    txtTarPernoctaInt.Text = oPre.dPernoctaInt.S();
                    rdlTarIAta.Checked = oPre.iIdSiglasAeropuerto == 1;
                    rdlTarIcao.Checked = oPre.iIdSiglasAeropuerto == 2;
                    
                    //chkIntercambio.Checked = oPre.bFactorIntercambio;
                    //chkGiraEspera.Checked = oPre.bFactorGiraEspera;
                    //chkGiraHorario.Checked = oPre.bFactorGiraHorario;
                    //chkFechaPico.Checked = oPre.bFactorFechaPico;
                    
                    
                    txtObservaciones.Text = oPre.sObservaciones;

                    iIdSolicitudVuelo = oPre.iIdSolicitudVuelo;

                    dTipoCambioDia = oPre.dTipoCambio;
                    dSubTotSV = oPre.dSubTotalSV;
                    dSubTotSC = oPre.dSubTotalSC;

                    AsignaFactoresPresupuesto(oPre);
                    //chkIntercambio.Checked = oPre.bFactorIntercambio;
                    //chkGiraEspera.Checked = oPre.bFactorGiraEspera;
                    //chkGiraHorario.Checked = oPre.bFactorGiraHorario;
                    //chkFechaPico.Checked = oPre.bFactorFechaPico;

                    //hdIntercambio.Value = oPre.dFactorIntercambio.S();
                    //hdFechaPico.Value = oPre.dFactorFechaPico.S();
                    //hdGiraEspera.Value = oPre.dGiraEspera.S();
                    //hdGiraHorario.Value = oPre.dGiraHorario.S();
                    
                    dtTramos = oPre.dtTramos;
                    dtServiciosC = oPre.dtServicios;
                    dtImportes = oPre.dtConceptos;

                    gvTramosOpc1.DataSource = dtTramos;
                    gvTramosOpc1.DataBind();
                    
                    gvServiciosC.DataSource = dtServiciosC;
                    gvServiciosC.DataBind();
                    
                    gvConceptos.DataSource = dtImportes;
                    gvConceptos.DataBind();

                    PintaTotalesConceptos();
                }
            }
        }
        public DatosRemision oDatos
        {
            set
            {
                DatosRemision oRem = value;
                if (oRem != null)
                {
                    //chkIntercambio.Checked = oRem.bAplicoIntercambio;
                    //chkGiraEspera.Checked = oRem.bAplicaGiraEspera;
                    //chkGiraHorario.Checked = oRem.bAplicaGiraHorario;
                    //chkFechaPico.Checked = oRem.bAplicaFactorFechaPico;

                    hdIntercambio.Value = oRem.dFactorIntercambioF.S();
                    hdFechaPico.Value = oRem.dFactorFechaPicoF.S();
                    hdGiraEspera.Value = oRem.dFactorGiraEsperaF.S();
                    hdGiraHorario.Value = oRem.dFactorGiraHorarioF.S();
                    hdFactorTramoNal.Value = oRem.dFactorTramosNal.S();
                    hdFactorTramoInt.Value = oRem.dFactorTramosInt.S();


                    ListItem lI = chkList.Items.FindByValue("1");
                    lI.Text = "Intercambio - " + oRem.dFactorIntercambioF.S();
                    lI.Selected = oRem.bAplicoIntercambio;

                    ListItem lGe = chkList.Items.FindByValue("2");
                    lGe.Text = "Gira Espera - " + oRem.dFactorGiraEsperaF.S();
                    lGe.Selected = oRem.bAplicaGiraEspera;

                    ListItem lGh = chkList.Items.FindByValue("3");
                    lGh.Text = "Gira Horario - " + oRem.dFactorGiraHorarioF.S();
                    lGh.Selected = oRem.bAplicaGiraHorario;

                    ListItem lFp = chkList.Items.FindByValue("4");
                    lFp.Text = "Fecha Pico - " + oRem.dFactorFechaPicoF.S();
                    lFp.Selected = oRem.bAplicaFactorFechaPico;

                    ListItem lFTN = chkList.Items.FindByValue("5");
                    lFTN.Text = "Tramo Nacional - " + oRem.dFactorTramosNal.S();
                    lFTN.Selected = oRem.bAplicaFactorTramoNacional;

                    ListItem lFTI = chkList.Items.FindByValue("6");
                    lFTI.Text = "Tramo Internacional - " + oRem.dFactorTramosInt.S();
                    lFTI.Selected = oRem.bAplicaFactorTramoInternacional;


                    hdAplicaFerryInt.Value = oRem.bAplicaFerryIntercambio ? "1" : "0";
                }
            }
        }
        public DatosRemision oDatosContrato
        {
            get { return (DatosRemision)ViewState["VSDatosContrato"]; }
            set { ViewState["VSDatosContrato"] = value; }
        }
        public int iIdTipoAeropuerto
        {
            get
            {
                return rdlTarIcao.Checked ? 2 : 1;
            }
        }
        public decimal dTipoCambioDia
        {
            get { return Utils.GetTipoCambioDia; }
            set { ViewState["VSTipoCambioDia"] = value; }
        }
        public SolicitudVuelo oSolicitudVuelo
        {
            get
            {
                SolicitudVuelo oSol = new SolicitudVuelo();

                oSol.iIdSolicitud = iIdSolicitudVuelo != null ? iIdSolicitudVuelo : 0;
                oSol.iIdContrato = cboDgContrato.Value.S().I();
                oSol.iIdContacto = cboDgSolicitante.SelectedItem != null ? cboDgSolicitante.Value.S().I() : 0;
                oSol.iIdEquipo = cboDgModeloSolicitado.Value.S().I() ;
                oSol.sNotaSolVuelo = txtObservaciones.Text;
                oSol.iStatus = 1;
                oSol.iIdOrigen = 1;
                oSol.iIdMotivo = 3;
                oSol.sUsuarioCreacion = Utils.GetUser;
                oSol.sUsuarioMod = Utils.GetUser;
                oSol.sIP = Utils.GetIPAddress();
                oSol.sMatricula = "";

                return oSol;
            }
            set
            {
                SolicitudVuelo oSol = value;
                if (oSol != null)
                {
                    iIdSolicitudVuelo = oSol.iIdSolicitud;
                }
            }
        }
        public int iIdPresupuesto
        {
            get { return ViewState["VSIdPresupuesto"].S().I(); }
            set 
            { 
                ViewState["VSIdPresupuesto"] = value;
                lblFolio.Text = ViewState["VSIdPresupuesto"].S();
            }
        }
        public int iIdSolicitudVuelo
        {
            get { return ViewState["VSIdSolicitud"].S().I(); }
            set { ViewState["VSIdSolicitud"] = value; }
        }
        public string sAccionRecibido
        {
            get { return ViewState["VSAccion"].S(); }
            set { ViewState["VSAccion"] = value; }
        }
        public string sMensaje
        {
            get { return ViewState["sMEnsaje"].S(); }
            set { ViewState["sMEnsaje"] = value; }
        }
        public enum OpcionesPresupuesto : int
        {
            Pernoctas = 1,
            FerrysVirtuales = 2
        }
        public OpcionesPresupuesto eOpcionesPres
        {
            set { ViewState["VSOpcPresupuesto"] = value; }
            get { return (OpcionesPresupuesto)ViewState["VSOpcPresupuesto"]; }
        }
        public bool bViabilidad
        {
            get { return ViewState["bViabilidad"].S().B(); }
            set { ViewState["bViabilidad"] = value; }
        }
        public object[] oGuardaSeguimiento
        {
            get
            {
                return new object[] { 
                                        "@IdSolicitud", iIdSolicitudVuelo,
                                        "@idAutor", bViabilidad == true ? 3 : 0,
                                        "@Nota", bViabilidad == true ? "La solicitud " + iIdSolicitudVuelo + "es viable." : Session["hfEdicion"].S().Equals("1") ? "Se generó la solicitud No. " + iIdSolicitudVuelo : "Se modifico la solicitud No. " + iIdSolicitudVuelo,
                                        "@Status", 1,
                                        "@IP", "",
                                        "@Usuario", ((UserIdentity)Session["UserIdentity"]).sUsuario.S()
                                    };
            }
        }
        public List<TramoSolicitud> lstObjTramos
        {
            get { return (List<TramoSolicitud>)ViewState["lstObjTramos"]; }
            set { ViewState["lstObjTramos"] = value; }
        }
        public Enumeraciones.SeCobraFerrys eSeCobraFerrys
        {
            set { ViewState["VSSeCobraFerrys"] = value; }
            get { return (Enumeraciones.SeCobraFerrys)ViewState["VSSeCobraFerrys"]; }
        }

		#endregion

        

	}
}