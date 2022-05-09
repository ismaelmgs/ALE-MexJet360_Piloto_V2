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
using System.Globalization;
using System.ComponentModel;

namespace ALE_MexJet.Views.CreditoCobranza
{
    public partial class frmGRemision : System.Web.UI.Page, IViewGRemision
    {

        #region EVENTOS

        protected void Page_Load(object sender, EventArgs e)
        {
            oPresenter = new GRemision_Presenter(this, new DBRemision());

            if (!IsPostBack)
            {
                mpeMensaje.OkButtonPressed += mpeMensaje_OkButtonPressed;

                iIdRemision = 0;
                sFiltro = "|";
                oPresenter.LoadObjects_Presenter();

                if (Page.Request["Folio"] != null)
                {
                    iIdRemision = Page.Request["Folio"].S().L();
                    if (eGetTipoPaquete != null)
                        eGetTipoPaquete(sender, e);

                    CargaPasoUnoRemisiones();

                }
                else
                {
                    detFecha.Value = dtFechaHoy;
                    chkAplicaIntercambio.Checked = true;
                }
            }

            if (dsNotas != null)
            {
                gvCasos.DataSource = dsNotas.Tables[1];
                gvCasos.DataBind();
            }
        }
        protected void mpeMensaje_OkButtonPressed(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "OcultaError", "OcultaError();", true);

        }
        protected void ddlCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (eGetContracts != null)
                    eGetContracts(sender, e);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "ddlCliente_SelectedIndexChanged", "Aviso");
            }
        }
        protected void btnGuardarAR_Click(object sender, EventArgs e)
        {
            try
            {
                Page.Validate("VGRemi");
                if (Page.IsValid)
                {
                    if (eSaveObj != null)
                        eSaveObj(sender, e);
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnGuardarAR_Click", "Aviso");
            }
        }
        protected void UpdatePanel1_Unload(object sender, EventArgs e)
        {
            MethodInfo methodInfo = typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Where(i => i.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel")).First();
            methodInfo.Invoke(ScriptManager.GetCurrent(Page),
                new object[] { sender as UpdatePanel });
        }
        protected void gvTramos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DropDownList ddlTipoPierna = (DropDownList)e.Row.FindControl("ddlTipoPierna");
                    if (ddlTipoPierna != null)
                    {
                        ddlTipoPierna.DataSource = dtTiposPierna;
                        ddlTipoPierna.DataTextField = "TipoPiernaDescripcion";
                        ddlTipoPierna.DataValueField = "IdTipoPierna";
                        ddlTipoPierna.DataBind();

                        ddlTipoPierna.SelectedValue = dtTramos.Rows[e.Row.RowIndex]["IdTipoPierna"].S();
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvTramos_RowDataBound", "Aviso");
            }
        }
        protected void btnSiguienteTramos_Click(object sender, EventArgs e)
        {
            try
            {
                if (oLstBit.Count > 0)
                {
                    bool ban = false;
                    string sContrato = Utils.ObtieneParametroPorClave("84");
                    string[] scontratos = sContrato.Split(',');
                    for (int i = 0; i < scontratos.Length; i++)
                    {
                        if (scontratos[i] == IdContrato.S())
                        {
                            ban = true;
                            break;
                        }
                    }

                    if (!ban)
                    {
                        int iban = 0;
                        foreach (BitacoraRemision ob in oLstBit)
                        {
                            if (ob.iSeCobra == 1)
                            {
                                iban = 1;
                                break;
                            }
                        }

                        if (iban == 0)
                        {
                            MostrarMensaje("Se debe seleccionar al menos un tramo a cobrar, favor de verificar", string.Empty);
                        }
                        else
                        {
                            if (eSetTramosRem != null)
                                eSetTramosRem(sender, e);

                            tmRecarga.Enabled = true;
                        }
                    }
                    else
                    {
                        if (oLstBit.Count > 0)
                        {
                            int iIdSolicitud = oLstBit[0].iIdSolicitudVuelo;
                            if (iIdSolicitud != 0)
                            {
                                DataTable dt = new DBPresupuesto().DBGetConsultaPresupuestoSolicitud(iIdSolicitud);
                                if (dt.Rows.Count > 0)
                                {
                                    if (dt.Rows[0]["IdPresupuesto"].S().I() != 0)
                                    {
                                        //Existe una cotización
                                        iIdPresupuesto = dt.Rows[0]["IdPresupuesto"].S().I();

                                        lblConfirmacionSolEx.Text = "Existe una cotización con Folio: " + dt.Rows[0]["IdPresupuesto"].S() + " " +
                                            "¿Desea remisionar con base a esta cotización?";
                                        ppConfirmacionSolEx.ShowOnPageLoad = true;
                                    }
                                    else
                                    {
                                        //No existe cotización asociada, pero si puede generar 
                                        lblConfirmacion.Text = "¿Desea remisionar con base a una cotización existente?    ";
                                        ppConfirmacion.ShowOnPageLoad = true;
                                    }
                                }
                                else
                                {
                                    //No existe cotización asociada, pero si puede generar 
                                    lblConfirmacion.Text = "¿Desea remisionar con base a una cotización existente?    ";
                                    ppConfirmacion.ShowOnPageLoad = true;
                                }
                            }
                            else
                            {
                                //No existe cotización asociada, pero si puede generar 
                                lblConfirmacion.Text = "¿Desea remisionar con base a una cotización existente?    ";
                                ppConfirmacion.ShowOnPageLoad = true;
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnSiguienteTramos_Click", "Aviso");
            }
        }
        protected void btnRegresarTramos_Click(object sender, EventArgs e)
        {
            try
            {
                RedireccionWizard(0);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnRegresarTramos_Click", "Aviso");
            }
        }
        decimal _dImporte = 0;
        DataTable _dtTotalHr = new DataTable();
        float dTiempoT = 0;
        protected void gvConceptos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (_dtTotalHr.Columns.Count == 0)
                    {
                        _dtTotalHr.Columns.Add("Tiempo");
                    }

                    _dImporte += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Importe"));

                    DataRow row = _dtTotalHr.NewRow();
                    row["Tiempo"] = DataBinder.Eval(e.Row.DataItem, "HrDescontar").S();
                    _dtTotalHr.Rows.Add(row);

                }

                if (e.Row.RowType == DataControlRowType.Footer)
                {
                    e.Row.Cells[3].Text = _dImporte.ToString("c");
                    e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[3].Font.Bold = true;

                    e.Row.Cells[4].Text = Utils.ObtieneTotalTiempo(_dtTotalHr, "Tiempo", ref dTiempoT);
                    e.Row.Cells[4].Font.Bold = true;
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvConceptos_RowDataBound", "Aviso");
            }
        }
        decimal _dImporte2 = 0;
        DataTable _dtTotalHr2 = new DataTable();
        float dTiempoT2 = 0;
        protected void gvConceptos2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (_dtTotalHr2.Columns.Count == 0)
                    {
                        _dtTotalHr2.Columns.Add("Tiempo");
                    }

                    _dImporte2 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Importe"));

                    DataRow row = _dtTotalHr2.NewRow();
                    row["Tiempo"] = DataBinder.Eval(e.Row.DataItem, "HrDescontar").S();
                    _dtTotalHr2.Rows.Add(row);

                }

                if (e.Row.RowType == DataControlRowType.Footer)
                {
                    e.Row.Cells[3].Text = _dImporte2.ToString("c");
                    e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[3].Font.Bold = true;

                    e.Row.Cells[4].Text = Utils.ObtieneTotalTiempo(_dtTotalHr2, "Tiempo", ref dTiempoT2);
                    e.Row.Cells[4].Font.Bold = true;
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvConceptos_RowDataBound", "Aviso");
            }
        }
        protected void btnSiguienteSC_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtTramosRem.Columns["Usuario"] == null)
                    dtTramosRem.Columns.Add("Usuario");

                foreach (DataRow row in dtTramosRem.Rows)
                {
                    row["Usuario"] = ((UserIdentity)Session["UserIdentity"]).sUsuario;
                }

                if (eSaveImportesR != null)
                    eSaveImportesR(sender, e);

                tmRecarga.Enabled = true;
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnSiguienteSC_Click", "Aviso");
            }
        }
        protected void btnSiguienteOpc2_Click(object sender, EventArgs e)
        {
            try
            {
                dtTramosRemOpc2.Columns.Add("Usuario");
                foreach (DataRow row in dtTramosRemOpc2.Rows)
                {
                    row["Usuario"] = ((UserIdentity)Session["UserIdentity"]).sUsuario;
                }

                dtConceptosR = null;
                dtConceptosR = dtConceptosR2.Copy();

                if (eSaveImportesOpc2 != null)
                    eSaveImportesOpc2(sender, e);

                tmRecarga.Enabled = true;
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnSiguienteSC_Click", "Aviso");
            }
        }
        protected void btnRegresarSC_Click(object sender, EventArgs e)
        {
            try
            {
                RedireccionWizard(1);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnRegresarSC_Click", "Aviso");
            }
        }
        protected void gvServiciosC_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    ASPxTextBox txtImporte = (ASPxTextBox)e.Row.FindControl("txtImporte");
                    if (txtImporte != null)
                    {
                        txtImporte.Text = dtServiciosC.Rows[e.Row.RowIndex]["Importe"].S();
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvServiciosC_RowDataBound", "Aviso");
            }
        }
        protected void btnAgregarSC_Click(object sender, EventArgs e)
        {
            try
            {
                if (eGetServiciosC != null)
                    eGetServiciosC(sender, e);

                ddlServiciosCargo.DataSource = dtServCargo;
                ddlServiciosCargo.TextField = "ServicioConCargoDescripcion";
                ddlServiciosCargo.ValueField = "IdServicioConCargo";
                ddlServiciosCargo.DataBind();

                ddlServiciosCargo.Value = null;
                ddlServiciosCargo.IsValid = true;

                ppServicios.ShowOnPageLoad = true;
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnAgregarSC_Click", "Aviso");
            }
        }
        protected void btnGuardarSC_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlServiciosCargo.SelectedItem.Value != null)
                {
                    BorraTotalesTabla();

                    DataRow dr = dtServiciosC.NewRow();
                    dr["IdServicioConCargo"] = ddlServiciosCargo.SelectedItem.Value.S();
                    dr["ServicioConCargoDescripcion"] = ddlServiciosCargo.SelectedItem.Text.S(); ;
                    dr["Importe"] = 0;
                    dr["PorPasajero"] = false;
                    dr["PorPierna"] = false;

                    dtServiciosC.Rows.Add(dr);

                    CargaTotales();
                }
                else
                {
                    ppServicios.ShowOnPageLoad = true;
                    ddlServiciosCargo.IsValid = false;
                    ddlServiciosCargo.ValidationSettings.ErrorText = "El campo es requerido.";
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnGuardarSC_Click", "Aviso");
            }
        }
        protected void btnRecalcular_Click(object sender, EventArgs e)
        {
            try
            {
                decimal dIva = 0;
                dIva = Utils.GetParametrosClave("9").S().D();
                string sIVA = (dIva * 100).S();

                foreach (GridViewRow row in gvServiciosC.Rows)
                {
                    if (row.Cells[0].Text != "SubTotal" && row.Cells[0].Text != "Total" && row.Cells[0].Text != "IVA Nal. " + sIVA + "%")
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

                BorraTotalesTabla();
                CargaTotales();
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnRecalcular_Click", "Aviso");
            }
        }
        protected void gvServiciosC_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Delete")
                {
                    dtServiciosC.Rows.RemoveAt(e.CommandArgument.S().I());
                }

                BorraTotalesTabla();
                CargaTotales();
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvServiciosC_RowCommand", "Aviso");
            }
        }
        protected void btnFinalizar_Click(object sender, EventArgs e)
        {
            try
            {
                iConversion = 0; //Sin conversion
                if(iConversion == 0)
                    RemisionesKardex();

                if (eSetFinalizaR != null)
                    eSetFinalizaR(sender, e);

                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "redirecciona", "window.location.href = 'frmRemisiones.aspx'", true);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnFinalizar_Click", "Aviso");
            }
        }

        public void RemisionesKardex()
        {
            try
            {
                string sMatricula = string.Empty;
                string sFechaVuelo = string.Empty;
                string sSubtotal = string.Empty;
                int index = 0;
                KardexRemision oRm = new KardexRemision();
                OLstKardex = new List<KardexRemision>();

                if (Session["Matricula"] != null)
                    sMatricula = Session["Matricula"].S();

                //Remision

                if(dtServVuelo != null && dtServVuelo.Rows.Count > 0)
                {
                    DataRow[] rows = dtServVuelo.Select("Cantidad = 'SubTotal'");
                    index = rows[0].ItemArray.Length - 1;
                    sSubtotal = rows[0].ItemArray[index].S();
                }

                oRm.IIdRemision = iIdRemision;
                oRm.IIdContrato = IdContrato;
                oRm.SMatricula = sMatricula;
                oRm.SCargo = sSubtotal;
                oRm.SAbono = "0";
                oRm.IIdMotivo = 1; //Remisión
                oRm.SNotas = string.Empty;
                oRm.SUsuario = ((UserIdentity)Session["UserIdentity"]).sUsuario;
                OLstKardex.Add(oRm);

                //Remisión Servicios con cargo
                //Cuando quiere convertir dinero a horas y el cliente de la remisión sea JetCard
                if (iConversion == 1 && (iPaquete == 1 || iPaquete == 2))
                {
                    if (dtServiciosC != null)
                    {
                        DateTime dtFechaSalida = new DateTime();
                        KardexRemision oRem = new KardexRemision();

                        if (Session["FechaVuelo"] != null)
                            sFechaVuelo = Session["FechaVuelo"].S();

                        dtFechaSalida = sFechaVuelo.Dt();

                        oRem.IIdRemision = iIdRemision;
                        oRem.IIdContrato = IdContrato;
                        oRem.SMatricula = sMatricula;

                        if (dSubTotalRemSC != 0)
                            oRem.SCargo = Utils.ObtenerHorasServicioConCargo(dSubTotalRemSC.S(), IdContrato, dtFechaSalida);
                        else
                            oRem.SCargo = "0";

                        oRem.SAbono = "0";
                        oRem.IIdMotivo = 9; //Remisión SCC (Servicio con cargo)
                        oRem.SNotas = string.Empty;
                        oRem.SUsuario = ((UserIdentity)Session["UserIdentity"]).sUsuario;
                        OLstKardex.Add(oRem);
                    }
                }
                else if(iConversion == 0)
                {
                    if (dtServiciosC != null)
                    {
                        DateTime dtFechaSalida = new DateTime();
                        KardexRemision oRem = new KardexRemision();

                        if (Session["FechaVuelo"] != null)
                            sFechaVuelo = Session["FechaVuelo"].S();

                        dtFechaSalida = sFechaVuelo.Dt();

                        oRem.IIdRemision = iIdRemision;
                        oRem.IIdContrato = IdContrato;
                        oRem.SMatricula = sMatricula;

                        if (dSubTotalRemSC != 0)
                            oRem.SCargo = Utils.ObtenerHorasServicioConCargo(dSubTotalRemSC.S(), IdContrato, dtFechaSalida);
                        else
                            oRem.SCargo = "0";

                        oRem.SAbono = "0";
                        oRem.IIdMotivo = 9; //Remisión SCC (Servicio con cargo)
                        oRem.SNotas = string.Empty;
                        oRem.SUsuario = ((UserIdentity)Session["UserIdentity"]).sUsuario;
                        OLstKardex.Add(oRem);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
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
        protected void btnAgregarTiempo_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataRow dr in dtServVuelo.Rows)
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

                // Recalcula Grid
                CargaServiciosVuelo(Utils.ReCalculaServiciosRemision(dtServVuelo, iIdRemision, IdContrato));
                txttiempoAdi.Text = string.Empty;
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnAgregarTiempo_Click", "Aviso");
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
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "chkTiempoAdi_CheckedChanged", "Aviso");
            }
        }
        protected void gvTramosOpc1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    ImageButton imb = (ImageButton)e.Row.FindControl("imbDelete");
                    DropDownList ddl = (DropDownList)e.Row.FindControl("ddlTipoPierna");
                    if (ddl != null)
                    {
                        ddl.DataSource = dtTiposPierna;
                        ddl.DataTextField = "TipoPiernaDescripcion";
                        ddl.DataValueField = "IdTipoPierna";
                        ddl.DataBind();

                        if (dtTramosRem.Rows[e.Row.RowIndex]["IdTipoPierna"].S() != "0")
                            ddl.SelectedValue = dtTramosRem.Rows[e.Row.RowIndex]["IdTipoPierna"].S();

                        if (dtTramosRem.Rows[e.Row.RowIndex].S("RealVirtual") == "Real")
                        {
                            ddl.Enabled = false;
                            imb.Visible = false;
                        }

                        if (dtTramosRem.Rows[e.Row.RowIndex]["SeCobra"].S() == "0")
                        {
                            e.Row.BackColor = System.Drawing.Color.Beige;
                        }
                    }

                    ASPxDateEdit txtFS = (ASPxDateEdit)e.Row.FindControl("txtFechaSalida");
                    if (txtFS != null)
                    {
                        txtFS.Value = dtTramosRem.Rows[e.Row.RowIndex]["FechaSalida"].S().Dt();
                        if (dtTramosRem.Rows[e.Row.RowIndex].S("RealVirtual") == "Real")
                        {
                            txtFS.ReadOnly = true;
                        }
                    }

                    ASPxDateEdit txtFL = (ASPxDateEdit)e.Row.FindControl("txtFechaLlegada");
                    if (txtFL != null)
                    {
                        txtFL.Value = dtTramosRem.Rows[e.Row.RowIndex]["FechaLlegada"].S().Dt();
                        if (dtTramosRem.Rows[e.Row.RowIndex].S("RealVirtual") == "Real")
                        {
                            txtFL.ReadOnly = true;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvTramosOpc1_RowDataBound", "Aviso");
            }
        }
        protected void gvTramosOpc2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DropDownList ddl = (DropDownList)e.Row.FindControl("ddlTipoPierna");
                    if (ddl != null)
                    {
                        ddl.DataSource = dtTiposPierna;
                        ddl.DataTextField = "TipoPiernaDescripcion";
                        ddl.DataValueField = "IdTipoPierna";
                        ddl.DataBind();

                        if (dtTramosRemOpc2.Rows[e.Row.RowIndex]["IdTipoPierna"].S() != "0")
                            ddl.SelectedValue = dtTramosRemOpc2.Rows[e.Row.RowIndex]["IdTipoPierna"].S();

                        if (dtTramosRemOpc2.Rows[e.Row.RowIndex].S("RealVirtual") == "Real")
                        {
                            ddl.Enabled = false;
                        }
                    }

                    ASPxDateEdit txtFS = (ASPxDateEdit)e.Row.FindControl("txtFechaSalida");
                    if (txtFS != null)
                    {
                        txtFS.Value = dtTramosRemOpc2.Rows[e.Row.RowIndex]["FechaSalida"].S().Dt();
                        if (dtTramosRemOpc2.Rows[e.Row.RowIndex].S("RealVirtual") == "Real")
                        {
                            txtFS.ReadOnly = true;
                        }
                    }

                    ASPxDateEdit txtFL = (ASPxDateEdit)e.Row.FindControl("txtFechaLlegada");
                    if (txtFL != null)
                    {
                        txtFL.Value = dtTramosRemOpc2.Rows[e.Row.RowIndex]["FechaLlegada"].S().Dt();
                        if (dtTramosRemOpc2.Rows[e.Row.RowIndex].S("RealVirtual") == "Real")
                        {
                            txtFL.ReadOnly = true;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvTramosOpc1_RowDataBound", "Aviso");
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

                        TimeSpan ts = FechaLlegada - FechaSalida;

                        dtTramosRem.Rows[Row.RowIndex]["TotalTiempoCalzo"] = ts.S();
                        dtTramosRem.Rows[Row.RowIndex]["TotalTiempoVuelo"] = ts.S();
                        dtTramosRem.Rows[Row.RowIndex]["FechaSalida"] = FechaSalida;

                        Row.Cells[6].Text = ts.S();
                        Row.Cells[7].Text = ts.S();

                        DefineFerryCobrar();
                        RecargaGridTramosPasoTres(Row);

                        //double dbTemp = Utils.ConvierteTiempoaDecimal(ts.S()).S().Db();
                        //dbTemp = (dbFactorTotalRem * dbTemp).S().Db();
                        //string sTemp = Utils.ConvierteDoubleATiempo(dbTemp.S().Db());

                        //Row.Cells[8].Text = sTemp;
                        //dtTramosRem.Rows[Row.RowIndex]["TiempoCobrar"] = sTemp;
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "txtFechaSalida_DateChanged", "Aviso");
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

                        TimeSpan ts = FechaLlegada - FechaSalida;

                        dtTramosRem.Rows[Row.RowIndex]["TotalTiempoCalzo"] = ts.S();
                        dtTramosRem.Rows[Row.RowIndex]["TotalTiempoVuelo"] = ts.S();
                        dtTramosRem.Rows[Row.RowIndex]["FechaLlegada"] = FechaLlegada;

                        Row.Cells[6].Text = ts.S();
                        Row.Cells[7].Text = ts.S();

                        DefineFerryCobrar();
                        RecargaGridTramosPasoTres(Row);
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "txtFechaLlegada_DateChanged", "Aviso");
            }
        }
        protected void imbDelete_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ImageButton imb = (ImageButton)sender;
                GridViewRow Row = (GridViewRow)imb.NamingContainer;

                dtTramosRem.Rows.RemoveAt(Row.RowIndex);

                DefineFerryCobrar();
                RecargaGridTramosPasoTres();
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "imbDelete_Click", "Aviso");
            }
        }
        protected void btnAgregarFerry_Click(object sender, EventArgs e)
        {
            try
            {
                ddlTipoPiernaFV.Items.Clear();
                ddlTipoPiernaFV.DataSource = dtTiposPierna;
                ddlTipoPiernaFV.DataTextField = "TipoPiernaDescripcion";
                ddlTipoPiernaFV.DataValueField = "IdTipoPierna";
                ddlTipoPiernaFV.DataBind();

                ddlTipoPiernaFV.SelectedValue = sTipoPierna;

                //sFiltro = dtTramosRem.Rows[dtTramosRem.Rows.Count - 1]["OrigenICAO"].S() + "|";
                rblPosicionFV_ValueChanged(sender, e);

                ppVirtuales.ShowOnPageLoad = true;
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnAgregarFerry_Click", "Aviso");
            }
        }
        protected void btnAgregarFV_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ddlOrigenFV.IsValid)
                    ppVirtuales.ShowOnPageLoad = true;
                else if (!ddlDestinoFV.IsValid)
                    ppVirtuales.ShowOnPageLoad = true;
                else if (!txtFechaSalidaFV.IsValid)
                    ppVirtuales.ShowOnPageLoad = true;
                else if (!txtFechaLlegadaFV.IsValid)
                    ppVirtuales.ShowOnPageLoad = true;
                else if (!txtTiempoVueloFV.IsValid)
                    ppVirtuales.ShowOnPageLoad = true;

                // Agrega la pierna escrita en el DataTable de piernas.
                DataTable dtTemp = new DataTable();
                dtTemp = dtTramosRem.Clone();

                DataRow row;

                if (rblPosicionFV.Value.S() == "1")
                    row = dtTemp.NewRow();
                else
                    row = dtTramosRem.NewRow();

                row["Origen"] = new DBRemision().DBGetIATAporICAO(ddlOrigenFV.Text.S());
                row["Destino"] = new DBRemision().DBGetIATAporICAO(ddlDestinoFV.Text.S());
                row["OrigenICAO"] = ddlOrigenFV.Text.S();
                row["DestinoICAO"] = ddlDestinoFV.Text.S();
                row["FechaSalida"] = txtFechaSalidaFV.Value.Dt();
                row["FechaLlegada"] = txtFechaLlegadaFV.Value.Dt();
                row["CantPax"] = 0;
                row["TotalTiempoCalzo"] = "00:00:00";
                row["TotalTiempoVuelo"] = "00:00:00";
                row["TiempoCobrar"] = txtTiempoVueloFV.Text.S();
                row["TiempoEspera"] = "00:00:00";
                row["VueloClienteId"] = ddlCliente.SelectedItem.Text.S();
                row["VueloContratoId"] = ddlContrato.SelectedItem.Text.S();
                row["IdTipoPierna"] = ddlTipoPiernaFV.SelectedItem.Value.S();
                row["TipoPierna"] = ddlTipoPiernaFV.SelectedItem.Text.S();
                row["RealVirtual"] = "Virtual";
                row["SeCobra"] = 1;
                row["TiempoOriginal"] = txtTiempoVueloFV.Text.S();

                switch (rblPosicionFV.Value.S())
                {
                    case "1":

                        row["Matricula"] = dtTramosRem.Rows[dtTramosRem.Rows.Count - 1]["Matricula"].S();

                        dtTemp.Rows.Add(row);

                        foreach (DataRow dr in dtTramosRem.Rows)
                        {
                            dtTemp.ImportRow(dr);
                        }
                        dtTramosRem = null;
                        dtTramosRem = dtTemp.Copy();

                        break;
                    case "2":
                        row["Matricula"] = dtTramosRem.Rows[0]["Matricula"].S();

                        dtTramosRem.Rows.Add(row);
                        break;
                }

                DefineFerryCobrar();
                RecargaGridTramosPasoTres();
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnAgregarFerry_Click", "Aviso");
            }
        }
        protected void ddlOrigenFV_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            try
            {
                //Verificar si tiene pierna predecesora para obtener el ICAO y mandarlo como parametro.

                if (rblPosicionFV.Value.S() == "1") // Arriba
                {
                    ASPxComboBox comboBox = (ASPxComboBox)source;

                    sFiltro = e.Filter + "|";

                    CargaComboAeropuertosFV(comboBox, sFiltro);
                }

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
                if (rblPosicionFV.Value.S() == "2")
                {
                    ASPxComboBox comboBox = (ASPxComboBox)source;
                    string sFiltro = string.Empty;
                    string sOrigen = ddlOrigenFV.Value.S();

                    sFiltro = e.Filter + "|" + sOrigen;

                    CargaComboAeropuertosFV(comboBox, sFiltro);
                }
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
                DateTime FechaSalida;
                DateTime FechaLlegada;
                ASPxDateEdit txt = (ASPxDateEdit)sender;

                if (txtFechaLlegadaFV.Value.S() != string.Empty)
                {
                    if (txt != null)
                    {
                        FechaSalida = txt.Value.Dt();
                        FechaLlegada = txtFechaLlegadaFV.Value.Dt();

                        if (FechaSalida > FechaLlegada)
                        {
                            AjustaTextBox(txt, System.Drawing.Color.Red, "La fecha de salida no puede ser mayor que la de llegada");
                            txt.Focus();
                        }
                        else
                        {
                            AjustaTextBox(txt, System.Drawing.Color.Black, "Fecha correcta.");
                            TimeSpan ts = FechaLlegada - FechaSalida;
                            if (!(FechaLlegada.Day.S().PadLeft(2, '0') == "01" && FechaLlegada.Month.S().PadLeft(2, '0') == "01" && FechaLlegada.Year.S().PadLeft(4, '0') == "0001") && !(FechaSalida.Day.S().PadLeft(2, '0') == "01" && FechaSalida.Month.S().PadLeft(2, '0') == "01" && FechaSalida.Year.S().PadLeft(4, '0') == "0001"))
                                txtTiempoVueloFV.Text = ts.S();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "txtFechaSalidaFV_DateChanged", "Aviso");
            }
        }
        protected void txtFechaLlegadaFV_DateChanged(object sender, EventArgs e)
        {
            try
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
                            txt.Focus();
                        }
                        else
                        {
                            AjustaTextBox(txt, System.Drawing.Color.Black, "Fecha correcta.");
                            TimeSpan ts = FechaLlegada - FechaSalida;
                            if (!(FechaLlegada.Day.S().PadLeft(2, '0') == "01" && FechaLlegada.Month.S().PadLeft(2, '0') == "01" && FechaLlegada.Year.S().PadLeft(4, '0') == "0001") && !(FechaSalida.Day.S().PadLeft(2, '0') == "01" && FechaSalida.Month.S().PadLeft(2, '0') == "01" && FechaSalida.Year.S().PadLeft(4, '0') == "0001"))
                                txtTiempoVueloFV.Text = ts.S();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "txtFechaLlegadaFV_DateChanged", "Aviso");
            }
        }
        protected void rblPosicionFV_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                //Veirificar si tiene pierna predecesora para obtener el ICAO y mandarlo como parametro
                if (rblPosicionFV.Value.S() == "2") // Abajo
                {
                    sFiltro = dtTramosRem.Rows[dtTramosRem.Rows.Count - 1]["DestinoICAO"].S() + "|";
                    dtFechaFerruyV = dtTramosRem.Rows[dtTramosRem.Rows.Count - 1]["FechaLlegada"].S().Dt();
                    CargaComboAeropuertosFV2(ddlDestinoFV, sFiltro, ddlOrigenFV, 2);
                }
                else // Arriba
                {
                    sFiltro = dtTramosRem.Rows[0]["OrigenICAO"].S() + "|";
                    dtFechaFerruyV = dtTramosRem.Rows[0]["FechaSalida"].S().Dt();
                    CargaComboAeropuertosFV2(ddlOrigenFV, sFiltro, ddlDestinoFV, 1);
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "rblPosicionFV_ValueChanged", "Aviso");
            }
        }
        protected void btnPreferencias_Click(object sender, EventArgs e)
        {
            try
            {
                gvCasos.DataSource = null;
                gvCasos.DataBind();
                txtAlertasTrip.Text = string.Empty;
                txtNotasTrip.Text = string.Empty;

                if (eGetNotasTrip != null)
                    eGetNotasTrip(sender, e);

                ppNotas.ShowOnPageLoad = true;
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnPreferencias_Click", "Aviso");
            }
        }
        protected void btnCancelarAR_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmRemisiones.aspx");
        }
        protected void tmRecarga_Tick(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvTramos.Rows)
            {
                DropDownList ddl = (DropDownList)row.FindControl("ddlTipoPierna");
                if (ddl != null)
                {
                    if (dtTramos.Rows[row.RowIndex]["IdTipoPierna"].S() != "0")
                        ddl.SelectedValue = dtTramos.Rows[row.RowIndex]["IdTipoPierna"].S();
                }
            }


            foreach (GridViewRow row in gvTramosOpc1.Rows)
            {
                DropDownList ddl = (DropDownList)row.FindControl("ddlTipoPierna");
                if (ddl != null)
                {
                    if (dtTramosRem.Rows[row.RowIndex]["IdTipoPierna"].S() != "0")
                        ddl.SelectedValue = dtTramosRem.Rows[row.RowIndex]["IdTipoPierna"].S();
                }
            }

            if (gvTramosOpc2.Rows.Count > 0)
            {
                foreach (GridViewRow row in gvTramosOpc2.Rows)
                {
                    DropDownList ddl = (DropDownList)row.FindControl("ddlTipoPierna");
                    if (ddl != null)
                    {
                        if (dtTramosRemOpc2.Rows[row.RowIndex]["IdTipoPierna"].S() != "0")
                            ddl.SelectedValue = dtTramosRemOpc2.Rows[row.RowIndex]["IdTipoPierna"].S();
                    }
                }
            }


            tmRecarga.Enabled = false;
        }

        protected void tmRecarga2_Tick(object sender, EventArgs e)
        {
            tmRecarga2.Enabled = false;
        }
        protected void btnButton_Click(object sender, EventArgs e)
        {
            if (ddlContrato.Value != null && ddlContrato.Value != "0")

                if (eGetContractsDates != null)
                    eGetContractsDates(sender, e);
        }
        protected void btnConfirmarSolEx_Click(object sender, EventArgs e)
        {
            try
            {
                if (eSetTramosCotizacion != null)
                    eSetTramosCotizacion(sender, e);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnConfirmarSolEx_Click", "Aviso");
            }
        }
        protected void lblConfirmar_Click(object sender, EventArgs e)
        {

        }
        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                switch (rblRentas.SelectedValue.S())
                {
                    case "1":
                        iIdPresupuesto = txtFolioCotizacion.Text.S().I();
                        if (iIdPresupuesto != 0)
                        {
                            lblMensajeVal.Text = string.Empty;
                            if (eSetTramosCotizacion != null)
                                eSetTramosCotizacion(sender, e);
                        }
                        else
                        {
                            lblMensajeVal.Text = "Ingrese un Folio válido.";
                            ppConfirmacion.ShowOnPageLoad = true;
                        }

                        break;

                    case "0":
                        if (eSetTramosRem != null)
                            eSetTramosRem(sender, e);
                        break;
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "lblConfirmar_Click", "Aviso");
            }
        }
        protected void rblRentas_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFolioCotizacion.Visible = rblRentas.SelectedValue.S() == "1" ? true : false;
            lblFolioCotizacion.Visible = rblRentas.SelectedValue.S() == "1" ? true : false;
        }
        protected void btnPizarra_Click(object sender, EventArgs e)
        {
            //LlenaModalSnapshotContrato();
            if (iIdRemision > 0)
            {
                ucPizarraElectronica.IdRemision = iIdRemision;
                ucPizarraElectronica.ShowPizarra();
            }

        }
        //protected void gvFactoresDetalle_BeforePerformDataSelect(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        ASPxGridView grid = sender as ASPxGridView;

        //        int iIdPadre = grid.GetMasterRowKeyValue().S().I();

        //        ConsultaDetalle(iIdPadre);
        //        grid.DataSource = dtFactoresTramo;
                
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        #endregion

        #region METODOS

        public void LoadObjects(DataTable dtClientes)
        {
            try
            {
                ddlCliente.DataSource = dtClientes;
                ddlCliente.TextField = "CodigoCliente";
                ddlCliente.ValueField = "IdCliente";
                ddlCliente.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LoadPackRem(DataTable dt)
        {
            try
            {
                dtPackRem = null;
                dtPackRem = dt;
                iPaquete = 0;

                if (dtPackRem != null && dtPackRem.Rows.Count > 0)
                    iPaquete = dtPackRem.Rows[0]["IdTipoPaquete"].S().I();

                if (iPaquete == 1 || iPaquete == 2)
                    btnFinalizarSCC.Visible = true;
                else
                    btnFinalizarSCC.Visible = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LoadContracts(DataTable dtCon)
        {
            try
            {
                ddlContrato.Items.Clear();

                ddlContrato.DataSource = dtCon;
                //ddlContrato.DataValueField = "IdContrato";
                //ddlContrato.DataTextField = "ClaveContrato";
                ddlContrato.ValueField = "IdContrato";
                ddlContrato.TextField = "ClaveContrato";
                ddlContrato.DataBind();

                if (ddlContrato.Items.Count > 0)
                    ddlContrato.SelectedIndex = 0;
                else
                {
                    ddlContrato.SelectedItem = null;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void RedireccionWizard(int iIndex)
        {
            //iIndicePestana = iIndex;
            if (iIndex > 0)
            {
                for (int i = iIndex; i > 0; i--)
                {
                    pcRemision.TabPages[i].Enabled = true;
                }
            }

            pcRemision.ActiveTabIndex = iIndex;
        }
        public void MostrarMensaje(string sMensaje, string sCaption)
        {
            //mpeMensaje.ShowMessage(sMensaje, sCaption);
            lbl.Text = sMensaje;
            ppAlert.ShowOnPageLoad = true;
        }
        public void CargaHeaders(string sGrupoModelo)
        {
            try
            {
                lblRespClienteP.Text = ddlCliente.SelectedItem.Text.S();
                lblRespContratoP.Text = ddlContrato.SelectedItem.Text.S();
                lblRespTipoEquipoP.Text = sGrupoModelo;
                lblRespFactorEspecialP.Text = txtFactorEspecial.Text.S();
                lblRespAplicaIntercambioP.Text = chkAplicaIntercambio.Checked ? "Si" : "No";


                lblRespClienteTP.Text = ddlCliente.SelectedItem.Text.S();
                lblRespContratoTP.Text = ddlContrato.SelectedItem.Text.S();
                lblRespTipoETP.Text = sGrupoModelo;
                lblRespFactorTP.Text = txtFactorEspecial.Text.S();
                lblRespInterTP.Text = chkAplicaIntercambio.Checked ? "Si" : "No";

                lblRespClienteSC.Text = ddlCliente.SelectedItem.Text.S();
                lblRespContratoSC.Text = ddlContrato.SelectedItem.Text.S();
                lblRespTipoESC.Text = sGrupoModelo;
                lblRespFactorSC.Text = txtFactorEspecial.Text.S();
                lblRespInterSC.Text = chkAplicaIntercambio.Checked ? "Si" : "No";

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void CargaTramos(DataTable dt)
        {
            try
            {
                if (eGetTiposPierna != null)
                    eGetTiposPierna(null, EventArgs.Empty);

                dtTramos = dt;

                gvTramos.DataSource = dtTramos;
                gvTramos.DataBind();

                tmRecarga.Enabled = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LoadHeaders(DataSet ds, DataTable dtTramos, DatosRemision oRem)
        {
            try
            {
                if (ds.Tables.Count > 1)
                {
                    dtTramosRem = dtTramos;

                    foreach (DataRow row in dtTramos.Rows)
                    {
                        row["TotalTiempoCalzo"] = row.S("TotalTiempoCalzo").Substring(0, 5);
                        row["TotalTiempoVuelo"] = row.S("TotalTiempoVuelo").Substring(0, 5);
                    }

                    gvTramosOpc1.DataSource = dtTramos;
                    gvTramosOpc1.DataBind();

                    CargaTotalesEnHeaders(oRem);

                    dtConceptosR = ds.Tables[2];
                    gvConceptos.DataSource = ds.Tables[2];
                    gvConceptos.DataBind();

                    btnSiguienteSC.Enabled = true;

                    if (oRem.oErr.bExisteError)
                    {
                        MostrarMensaje(oRem.oErr.sMsjError, "Aviso");
                        btnSiguienteSC.Enabled = false;
                        btnSiguienteOpc2.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void CargaTotalesEnHeaders(DatosRemision oRem)
        {
            try
            {
                float dTiempoF = 0;
                string sTiempoVuelo = string.Empty;
                string sTiempoCalzo = string.Empty;
                string sTiempoCobrar = string.Empty;

                if (oRem.sTotalTiempoVuelo == string.Empty)
                    sTiempoVuelo = Utils.ObtieneTotalTiempo(dtTramosRem, "TotalTiempoVuelo", ref dTiempoF);
                else
                    sTiempoVuelo = oRem.sTotalTiempoVuelo;

                if (oRem.sTotalTiempoCalzo == string.Empty)
                    sTiempoCalzo = Utils.ObtieneTotalTiempo(dtTramosRem, "TotalTiempoCalzo", ref dTiempoF);
                else
                    sTiempoCalzo = oRem.sTotalTiempoCalzo;

                if (oRem.sTotalTiempoCobrar == string.Empty)
                    sTiempoCobrar = Utils.ObtieneTotalTiempo(dtTramosRem, "TiempoCobrar", ref dTiempoF);
                else
                    sTiempoCalzo = oRem.sTotalTiempoCobrar;


                lblRespTiempoVueloReal.Text = sTiempoVuelo;
                lblRespTiempoVueloRealF.Text = sTiempoVuelo;
                lblRespTotalTiempoCalzo.Text = sTiempoCalzo;
                lblRespTotalTiempoCalzoF.Text = sTiempoCalzo;
                lblRespTotalVueloCobrar.Text = sTiempoCobrar;
                lblRespTotalVueloCobrarF.Text = sTiempoCobrar;

                ListItem lFe = chkList.Items.FindByValue("1");
                lFe.Text = "Factor Especial - " + oRem.dFactorEspecialF.S();
                lFe.Selected = oRem.bAplicoFactorEspecial;

                ListItem lI = chkList.Items.FindByValue("2");
                lI.Text = "Intercambio - " + oRem.dFactorIntercambioF.S();
                lI.Selected = oRem.bAplicoIntercambio;

                ListItem lGe = chkList.Items.FindByValue("3");
                lGe.Text = "Gira Espera - " + oRem.dFactorGiraEsperaF.S();
                lGe.Selected = oRem.bAplicaGiraEspera;

                ListItem lGh = chkList.Items.FindByValue("4");
                lGh.Text = "Gira Horario - " + oRem.dFactorGiraHorarioF.S();
                lGh.Selected = oRem.bAplicaGiraHorario;

                ListItem lFp = chkList.Items.FindByValue("5");
                lFp.Text = "Fecha Pico - " + oRem.dFactorFechaPicoF.S();
                lFp.Selected = oRem.bAplicaFactorFechaPico;

                ListItem lVs = chkList.Items.FindByValue("6");
                lVs.Text = "Vuelo Simultaneo - " + (oRem.dFactorVloSimultaneo == 1 || oRem.dFactorVloSimultaneo == 0 ? "0" : oRem.dFactorVloSimultaneo.S());
                lVs.Selected = oRem.bAplicaFactorVueloSimultaneo;

                ListItem lFTN = chkList.Items.FindByValue("7");
                lFTN.Text = "Tramo Nacional - " + oRem.dFactorTramosNal.S();
                lFTN.Selected = oRem.bAplicaFactorTramoNacional;

                ListItem lFTI = chkList.Items.FindByValue("8");
                lFTI.Text = "Tramo Internacional - " + oRem.dFactorTramosInt.S();
                lFTI.Selected = oRem.bAplicaFactorTramoInternacional;



                // Pestaña 2
                ListItem lFeF = chkListF.Items.FindByValue("1");
                lFeF.Text = "Factor Especial - " + oRem.dFactorEspecialF.S();
                lFeF.Selected = oRem.bAplicoFactorEspecial;

                ListItem lIF = chkListF.Items.FindByValue("2");
                lIF.Text = "Intercambio - " + oRem.dFactorIntercambioF.S();
                lIF.Selected = oRem.bAplicoIntercambio;

                ListItem lGeF = chkListF.Items.FindByValue("3");
                lGeF.Text = "Gira Espera - " + oRem.dFactorGiraEsperaF.S();
                lGeF.Selected = oRem.bAplicaGiraEspera;

                ListItem lGhF = chkListF.Items.FindByValue("4");
                lGhF.Text = "Gira Horario - " + oRem.dFactorGiraHorarioF.S();
                lGhF.Selected = oRem.bAplicaGiraHorario;

                ListItem lFpF = chkListF.Items.FindByValue("5");
                lFpF.Text = "Fecha Pico - " + oRem.dFactorFechaPicoF.S();
                lFpF.Selected = oRem.bAplicaFactorFechaPico;

                ListItem lVsF = chkListF.Items.FindByValue("6");
                lVsF.Text = "Vuelo Simultaneo - " + (oRem.dFactorVloSimultaneo == 1 || oRem.dFactorVloSimultaneo == 0 ? "0" : oRem.dFactorVloSimultaneo.S());
                lVsF.Selected = oRem.bAplicaFactorVueloSimultaneo;

                ListItem lTNF = chkListF.Items.FindByValue("7");
                lTNF.Text = "Tramo Nacional - " + oRem.dFactorTramosNal.S();
                lTNF.Selected = oRem.bAplicaFactorTramoNacional;

                ListItem lTIF = chkListF.Items.FindByValue("8");
                lTIF.Text = "Tramo Internacional - " + oRem.dFactorTramosInt.S();
                lTIF.Selected = oRem.bAplicaFactorTramoInternacional;


                hdFactorIntercambio.Value = oRem.dFactorIntercambioF.S();
                hdFactorGiraEspera.Value = oRem.dFactorGiraEsperaF.S();
                hdFactorGiraHorario.Value = oRem.dFactorGiraHorarioF.S();
                hdFactorFechaPico.Value = oRem.dFactorFechaPicoF.S();
                hdFactorVueloSimul.Value = oRem.dFactorVloSimultaneo.S();
                hdFactorTramoNal.Value = oRem.dFactorTramosNal.S();
                hdFactorTramoInt.Value = oRem.dFactorTramosInt.S();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LoadServiciosV(DataTable dtSC, DataTable dtSV, DataTable dtSCC)
        {
            try
            {
                CargaServiciosVuelo(dtSC);

                DataTable dtConceptos = new DBRemision().DBGetConceptosRemision;

                ddlConceptoAdi.DataSource = dtConceptos;
                ddlConceptoAdi.TextField = "Descripcion";
                ddlConceptoAdi.ValueField = "IdConcepto";
                ddlConceptoAdi.DataBind();

                dtServiciosC = dtSCC;
                CargaTotales();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LoadRemisionesTerminadas(DataSet ds, DatosRemision oRem)
        {
            try
            {

                CalculaTotalesTable(ds.Tables[1], ds.Tables[0].Rows[0]["FactorIVA"].S().D(), ds.Tables[0].Rows[0]["PorDescuento"].S().D());
                CargaServiciosVuelo(ds.Tables[1]);

                dtServiciosC = ds.Tables[3];
                CargaTotalesRemisionTerminada(ds.Tables[0].Rows[0]["TipoCambio"].S().D());

                dtTramosRem = ds.Tables[4].Copy();
                gvTramosOpc1.DataSource = ds.Tables[4];
                gvTramosOpc1.DataBind();

                gvConceptos.DataSource = ds.Tables[5];
                gvConceptos.DataBind();

                BloqueaControlesSegunEstatus(2);

                CargaTotalesEnHeaders(oRem);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private static void CalculaTotalesTable(DataTable dtSC, decimal dFactIva, decimal dFactorDesc)
        {
            try
            {
                decimal SumaDlls = 0;
                decimal SumaIVA = 0;
                decimal Total = 0;
                decimal dFactorIVA = (dFactIva / 100);

                foreach (DataRow rowS in dtSC.Rows)
                {
                    SumaDlls += rowS["ImporteDlls"].S().D();
                }

                float dTiempoT = 0;

                DataRow drSub = dtSC.NewRow();
                //drSub["IdConcepto"] = "999";
                drSub["Cantidad"] = "SubTotal";
                drSub["ImporteDlls"] = SumaDlls;
                drSub["HrDescontar"] = Utils.ObtieneTotalTiempo(dtSC, "HrDescontar", ref dTiempoT);
                dtSC.Rows.Add(drSub);

                decimal dImpDesc = 0;
                dImpDesc = (SumaDlls * (dFactorDesc / 100));
                SumaDlls = SumaDlls - dImpDesc;

                DataRow drDesc = dtSC.NewRow();
                drDesc["Cantidad"] = "Descuento " + dFactorDesc + "%";
                drDesc["ImporteDlls"] = dImpDesc;
                dtSC.Rows.Add(drDesc);


                SumaIVA = SumaDlls * dFactorIVA;
                Total = SumaDlls * (1 + dFactorIVA);


                DataRow drIva = dtSC.NewRow();
                //drIva["IdConcepto"] = "998";
                drIva["Cantidad"] = "IVA " + (dFactorIVA * 100).S() + "%";
                drIva["ImporteDlls"] = SumaIVA;
                dtSC.Rows.Add(drIva);

                DataRow drTotal = dtSC.NewRow();
                //drTotal["IdConcepto"] = "997";
                drTotal["Cantidad"] = "Total";
                drTotal["ImporteDlls"] = Total;
                dtSC.Rows.Add(drTotal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void BloqueaControlesSegunEstatus(int iStatus)
        {
            switch (iStatus)
            {
                case 2:
                    ddlCliente.ReadOnly = true;
                    ddlContrato.ReadOnly = true;
                    btnCancelarAR.Enabled = false;
                    btnGuardarAR.Enabled = false;

                    gvTramos.Enabled = false;
                    btnSiguienteTramos.Enabled = false;
                    btnRegresarTramos.Enabled = false;

                    chkTiempoAdi.ReadOnly = true;
                    btnAgregarSC.Enabled = false;
                    btnFinalizar.Enabled = false;
                    btnRecalcular.Enabled = false;
                    btnSiguienteSC.Enabled = false;
                    btnAgregarFerry.Enabled = false;
                    btnRegresarSC.Enabled = false;
                    ASPxButton1.Enabled = false;
                    btnSiguienteOpc2.Enabled = false;
                    chkList.Enabled = false;
                    chkListF.Enabled = false;

                    pcRemision.TabPages[0].Enabled = false;
                    pcRemision.TabPages[2].Enabled = true;
                    pcRemision.TabPages[3].Enabled = true;

                    pcRemision.ActiveTabIndex = 3;

                    break;

            }
        }
        private void CargaServiciosVuelo(DataTable dtSC)
        {
            try
            {
                dtServVuelo = dtSC;
                gvSC.DataSource = dtSC;
                gvSC.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void LoadGridServicios()
        {
            try
            {
                gvServiciosC.DataSource = dtServiciosC;
                gvServiciosC.DataBind();


                foreach (GridViewRow r in gvServiciosC.Rows)
                {
                    if (r.Cells[0].Text == "SubTotal" || r.Cells[0].Text == "Total" || r.Cells[0].Text == "IVA Nal. " + sIVA + "%")
                    {
                        ASPxButton btn = (ASPxButton)r.FindControl("btnEliminarSC");
                        if (btn != null)
                            btn.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargaTotales()
        {
            try
            {
                decimal dSuma = 0;
                decimal dIva = 0;
                dIva = Utils.GetParametrosClave("9").S().D();

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

                DataRow[] drIva = dtServiciosC.Select("ServicioConCargoDescripcion = 'IVA Nal. " + sIVA + "%'");
                if (drSub.Length == 0)
                {
                    DataRow drS = dtServiciosC.NewRow();
                    dIva = (dSuma * dIva);
                    drS["IdServicioConCargo"] = 999998;
                    drS["ServicioConCargoDescripcion"] = "IVA Nal. " + sIVA + "%";
                    drS["Importe"] = dIva;
                    dtServiciosC.Rows.Add(drS);
                }
                else
                {
                    dIva = (dSuma * Utils.GetParametrosClave("9").S().D());
                    drIva[0]["Importe"] = dIva;
                }

                DataRow[] drTot = dtServiciosC.Select("ServicioConCargoDescripcion = 'Total'");
                if (drSub.Length == 0)
                {
                    DataRow drS = dtServiciosC.NewRow();
                    drS["IdServicioConCargo"] = 999997;
                    drS["ServicioConCargoDescripcion"] = "Total";
                    drS["Importe"] = dSuma + dIva;
                    dtServiciosC.Rows.Add(drS);
                }
                else
                {
                    drTot[0]["Importe"] = dSuma + dIva;
                }

                LoadGridServicios();


                decimal dTipoC = Utils.GetTipoCambioDia;

                decimal dSumaSV = 0;
                decimal dSumaSC = 0;

                foreach (GridViewRow gr in gvSC.Rows)
                {
                    if (gr.Cells[4].Text == "Total")
                        dSumaSV = gr.Cells[5].Text.S().Replace("$", "").Replace(",", "").S().D();
                }

                foreach (GridViewRow gr in gvServiciosC.Rows)
                {
                    if (gr.Cells[0].Text == "Total")
                    {
                        ASPxTextBox txt = (ASPxTextBox)gr.FindControl("txtImporte");
                        dSumaSC = txt.Text.S().D();
                    }
                }

                dSubTotalRemSC = 0;
                dSubTotalRemSC = dSuma;

                dTotalPesos = dSumaSC + (dSumaSV * dTipoC);
                dTotalDlls = dSumaSV + (dSumaSC / dTipoC);

                lblTotalPesos.Text = "Total Remisión en Pesos: " + dTotalPesos.ToString("c");
                lblTotalDlls.Text = "Total Remisión en Dólares: " + dTotalDlls.ToString("c");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargaTotalesRemisionTerminada(decimal dTipoCambio)
        {
            try
            {
                decimal dSuma = 0;
                decimal dIva = 0;
                dIva = Utils.GetParametrosClave("9").S().D();

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

                DataRow[] drIva = dtServiciosC.Select("ServicioConCargoDescripcion = 'IVA Nal. " + sIVA + "%'");
                if (drSub.Length == 0)
                {
                    DataRow drS = dtServiciosC.NewRow();
                    dIva = (dSuma * dIva);
                    drS["IdServicioConCargo"] = 999998;
                    drS["ServicioConCargoDescripcion"] = "IVA Nal. " + sIVA + "%";
                    drS["Importe"] = dIva;
                    dtServiciosC.Rows.Add(drS);
                }
                else
                {
                    dIva = (dSuma * Utils.GetParametrosClave("9").S().D());
                    drIva[0]["Importe"] = dIva;
                }

                DataRow[] drTot = dtServiciosC.Select("ServicioConCargoDescripcion = 'Total'");
                if (drSub.Length == 0)
                {
                    DataRow drS = dtServiciosC.NewRow();
                    drS["IdServicioConCargo"] = 999997;
                    drS["ServicioConCargoDescripcion"] = "Total";
                    drS["Importe"] = dSuma + dIva;
                    dtServiciosC.Rows.Add(drS);
                }
                else
                {
                    drTot[0]["Importe"] = dSuma + dIva;
                }

                LoadGridServicios();


                decimal dTipoC = dTipoCambio;

                decimal dSumaSV = 0;
                decimal dSumaSC = 0;

                foreach (GridViewRow gr in gvSC.Rows)
                {
                    if (gr.Cells[4].Text == "Total")
                        dSumaSV = gr.Cells[5].Text.S().Replace("$", "").Replace(",", "").S().D();
                }

                foreach (GridViewRow gr in gvServiciosC.Rows)
                {
                    if (gr.Cells[0].Text == "Total")
                    {
                        ASPxTextBox txt = (ASPxTextBox)gr.FindControl("txtImporte");
                        dSumaSC = txt.Text.S().D();
                    }
                }

                dTotalPesos = dSumaSC + (dSumaSV * dTipoC);
                dTotalDlls = dSumaSV + (dSumaSC / dTipoC);

                lblTotalPesos.Text = "Total Remisión en Pesos: " + dTotalPesos.ToString("c");
                lblTotalDlls.Text = "Total Remisión en Dólares: " + dTotalDlls.ToString("c");
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
        private void BorraTotalesTabla()
        {
            try
            {
                DataRow[] drSer = new DataRow[3];
                foreach (DataRow row in dtServiciosC.Rows)
                {
                    if (row["ServicioConCargoDescripcion"].S() == "Total")
                        drSer[0] = row;

                    else if (row["ServicioConCargoDescripcion"].S() == "SubTotal")
                        drSer[1] = row;

                    else if (row["ServicioConCargoDescripcion"].S() == "IVA Nal. " + sIVA + "%")
                        drSer[2] = row;
                }

                for (int i = 0; i < drSer.Length; i++)
                {
                    dtServiciosC.Rows.Remove(drSer[i]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargaPasoUnoRemisiones()
        {
            try
            {
                if (eGetPasoUno != null)
                    eGetPasoUno(null, EventArgs.Empty);
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
                DatosRemision odRem = new DBRemision().DBGetObtieneDatosRemision(iIdRemision, IdContrato);

                CargaTotalesEnHeaders(odRem);

                Utils.AplicaIntercambioTarifasSiExiste(odRem, dtTramosRem);

                FactoresTramos oFact = Utils.ObtieneFactoresDeUnaRemision(dtTramosRem, odRem, IdContrato);
                if (oFact != null)
                {
                    AplicaFactoresHeader(oFact);
                    dbFactorTotalRem = AsignaFactoresHeader(oFact).S().Db();
                }

                dtConceptosR = Utils.CalculaCostosRemision(odRem.iCobroTiempo, dtTramosRem, odRem);


                gvTramosOpc1.DataSource = dtTramosRem;
                gvTramosOpc1.DataBind();

                gvConceptos.DataSource = dtConceptosR;
                gvConceptos.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void RecargaGridTramosPasoTres(GridViewRow Row)
        {
            try
            {
                DatosRemision odRem = new DBRemision().DBGetObtieneDatosRemision(iIdRemision, IdContrato);

                CargaTotalesEnHeaders(odRem);
                Utils.AplicaIntercambioTarifasSiExiste(odRem, dtTramosRem);

                FactoresTramos oFact = Utils.ObtieneFactoresDeUnaRemision(dtTramosRem, odRem, IdContrato);
                if (oFact != null)
                {
                    AplicaFactoresHeader(oFact);
                    dbFactorTotalRem = AsignaFactoresHeader(oFact).S().Db();
                }

                string sT = dtTramosRem.Rows[Row.RowIndex]["TotalTiempoCalzo"].S();

                double dbTemp = Utils.ConvierteTiempoaDecimal(sT).S().Db();
                dbTemp = (dbFactorTotalRem * dbTemp).S().Db();
                string sTemp = Utils.ConvierteDoubleATiempo(dbTemp.S().Db());

                dtTramosRem.Rows[Row.RowIndex]["TiempoCobrar"] = sTemp;
                Row.Cells[8].Text = sTemp;


                dtConceptosR = Utils.CalculaCostosRemision(odRem.iCobroTiempo, dtTramosRem, odRem);

                gvTramosOpc1.DataSource = dtTramosRem;
                gvTramosOpc1.DataBind();

                gvConceptos.DataSource = dtConceptosR;
                gvConceptos.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargaComboAeropuertosFV(ASPxComboBox ddl, string sFiltro)
        {
            try
            {
                if (eLoadOrigDestFiltro != null)
                    eLoadOrigDestFiltro(sFiltro, EventArgs.Empty);

                ddl.DataSource = dtOrigenDestino;
                ddl.TextField = "AeropuertoICAO";
                ddl.ValueField = "idorigen";
                ddl.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargaComboAeropuertosFV2(ASPxComboBox ddlOrigen, string sFiltro, ASPxComboBox ddlDestino, int iPosition)
        {
            try
            {
                if (eLoadOrigDestFiltro != null)
                    eLoadOrigDestFiltro(sFiltro, EventArgs.Empty);

                ddlOrigenFV.DataSource = null;
                ddlOrigenFV.Text = string.Empty;
                ddlOrigenFV.Value = null;
                ddlOrigenFV.DataBind();

                ddlDestinoFV.DataSource = null;
                ddlDestinoFV.Text = string.Empty;
                ddlDestinoFV.Value = null;
                ddlDestinoFV.DataBind();

                switch (iPosition)
                {
                    case 1: // Arriba
                        ddlOrigen.DataSource = dtOrigenDestino2;
                        ddlOrigen.TextField = "AeropuertoICAO";
                        ddlOrigen.ValueField = "idorigen";
                        ddlOrigen.DataBind();

                        ddlDestino.DataSource = dtOrigenDestino;
                        ddlDestino.TextField = "AeropuertoICAO";
                        ddlDestino.ValueField = "idorigen";
                        ddlDestino.DataBind();

                        txtFechaSalidaFV.Value = null;
                        txtFechaSalidaFV.Text = string.Empty;

                        if (dtFechaFerruyV != null)
                            txtFechaLlegadaFV.Value = dtFechaFerruyV;
                        break;
                    case 2: // Abajo
                        ddlDestino.DataSource = dtOrigenDestino;
                        ddlDestino.TextField = "AeropuertoICAO";
                        ddlDestino.ValueField = "idorigen";
                        ddlDestino.DataBind();

                        ddlOrigen.DataSource = dtOrigenDestino2;
                        ddlOrigen.TextField = "AeropuertoICAO";
                        ddlOrigen.ValueField = "idorigen";
                        ddlOrigen.DataBind();

                        txtFechaLlegadaFV.Value = null;
                        txtFechaLlegadaFV.Text = string.Empty;

                        if (dtFechaFerruyV != null)
                            txtFechaSalidaFV.Value = dtFechaFerruyV;
                        break;
                }

                txtTiempoVueloFV.Value = null;
                txtTiempoVueloFV.Text = string.Empty;
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
                if (eSeCobraFerrys == Enumeraciones.SeCobraFerrys.Reposicionamiento)
                {
                    if (dtTramosRem.Rows.Count > 0)
                    {
                        if (dtTramosRem.Rows[0]["CantPax"].S() == "0" && dtTramosRem.Rows[dtTramosRem.Rows.Count - 1]["CantPax"].S() == "0")
                        {
                            float fInicio = Utils.ConvierteTiempoaDecimal(dtTramosRem.Rows[0]["TiempoCobrar"].S());
                            float fFin = Utils.ConvierteTiempoaDecimal(dtTramosRem.Rows[dtTramosRem.Rows.Count - 1]["TiempoCobrar"].S());

                            DateTime dtSalida;
                            DateTime dtLlegada;
                            TimeSpan ts;

                            if (fInicio < fFin)
                            {
                                dtSalida = dtTramosRem.Rows[0]["FechaSalida"].S().Dt();
                                dtLlegada = dtTramosRem.Rows[0]["FechaLlegada"].S().Dt();

                                ts = dtLlegada - dtSalida;

                                dtTramosRem.Rows[dtTramosRem.Rows.Count - 1]["SeCobra"] = 0;
                                //dtTramosRem.Rows[dtTramosRem.Rows.Count - 1]["TiempoCobrar"] = "00:00:00";

                                dtTramosRem.Rows[0]["SeCobra"] = 1;
                                dtTramosRem.Rows[0]["TiempoCobrar"] = ts.S().Substring(0, 5);
                                gvTramosOpc1.Rows[0].Cells[8].Text = ts.S().Substring(0, 5);
                            }
                            else
                            {
                                dtSalida = dtTramosRem.Rows[dtTramosRem.Rows.Count - 1]["FechaSalida"].S().Dt();
                                dtLlegada = dtTramosRem.Rows[dtTramosRem.Rows.Count - 1]["FechaLlegada"].S().Dt();

                                ts = dtLlegada - dtSalida;

                                dtTramosRem.Rows[0]["SeCobra"] = 0;
                                //dtTramosRem.Rows[0]["TiempoCobrar"] = "00:00:00";

                                dtTramosRem.Rows[dtTramosRem.Rows.Count - 1]["SeCobra"] = 1;
                                dtTramosRem.Rows[dtTramosRem.Rows.Count - 1]["TiempoCobrar"] = ts.S().Substring(0, 5);

                                if (dtTramosRem.Rows.Count > gvTramosOpc1.Rows.Count)
                                    gvTramosOpc1.Rows[0].Cells[8].Text = ts.S().Substring(0, 5);
                                else
                                    gvTramosOpc1.Rows[dtTramosRem.Rows.Count - 1].Cells[8].Text = ts.S();
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
        public void CargaGridDoblePresupuesto(DataTable dtTramos, DataTable dtConceptos2)
        {
            try
            {
                dtTramosRemOpc2 = dtTramos;
                gvTramosOpc2.DataSource = dtTramos;
                gvTramosOpc2.DataBind();

                dtConceptosR2 = dtConceptos2.Copy();
                gvConceptos2.DataSource = dtConceptos2;
                gvConceptos2.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LlenaModalCaracteristicasContrato(DatosRemision oRem)
        {
            try
            {
                lblppdClaveCliente.Text = ddlCliente.SelectedItem.Text.S();
                lblppdClaveContrato.Text = ddlContrato.SelectedItem.Text.S();
                lblppdTipoContrato.Text = oRem.sTipoPaquete;
                lblppdGrupoModelo.Text = oRem.sGrupoModeloDesc;

                // Generales
                lblppdFechaInicio.Text = oRem.sFechaInicio;
                lblppdHorasContratadas.Text = oRem.iHorasContratadasTotal.S();
                lblppdHorasAño.Text = oRem.iHorasContratadasAnio.S();
                gvBases.DataSource = oRem.dtBases;
                gvBases.DataBind();

                // Tarifas
                lblppdCDN.Text = oRem.dVueloCostoDirNal.ToString("c");
                lblppdCDI.Text = oRem.dVueloCostoDirInt.ToString("c");
                lblppdTEN.Text = oRem.dTarifaNalEspera.ToString("c");
                lblppdTEI.Text = oRem.dTarifaIntEspera.ToString("c");
                lblppdPernoctasN.Text = oRem.dTarifaDolaresNal.ToString("c");
                lblppdPernoctasI.Text = oRem.dTarifaDolaresInt.ToString("c");

                // Cobros
                lblppdCobroCombustible.Text = oRem.eCobroCombustible.S();
                lblppdCobroFerrys.Text = oRem.eSeCobraFerry.S();
                lblppdCobroTipoTiempo.Text = oRem.iCobroTiempo == 1 ? "Vuelo" : "Calzo";
                lblppdCobroGiras.Text = oRem.bAplicaGiraEspera ? "Espera" : "Horario";
                lblppdCobroTipoTiempoMinutos.Text = oRem.iMasMinutos.S();
                lblppdTiempoEsperaLibre.Text = oRem.bAplicaEsperaLibre ? "Si" : "No";

                gvIntercambios.DataSource = oRem.dtIntercambios;
                gvIntercambios.DataBind();

                pcMain.ShowOnPageLoad = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public void LlenaModalSnapshotContrato()
        //{
        //    try
        //    {
        //        if (Session["SnapshotRem"] != null)
        //        {
        //            SnapshotRemision oSnap = new SnapshotRemision();
        //            oSnap = (SnapshotRemision)Session["SnapshotRem"];
        //            // Generales
        //            lblSnapClaveCliente.Text = ddlCliente.SelectedItem.Text.S();
        //            lblSnapClaveContrato.Text = ddlContrato.SelectedItem.Text.S();
        //            lblSnapTipoContrato.Text = oSnap.oDatosRem.sTipoPaquete;
        //            lblSnapGrupoModelo.Text = oSnap.oDatosRem.sGrupoModeloDesc;

        //            lblSnapFolioRem.Text = oSnap.oDatosRem.lIdRemision.S();
        //            lblSnapTiempoCobrar.Text = oSnap.oDatosRem.sTotalTiempoCalzo;
        //            lblSnapMasMinutos.Text = oSnap.oDatosRem.iMasMinutos.S();
        //            lblSnapAplicaTramPact.Text = oSnap.oDatosRem.bTiemposPactados ? "SI" : "NO";
        //            lblSnapHorasContratadasTotal.Text = oSnap.oDatosRem.iHorasContratadasTotal.S();
        //            lblSnapHorasContratadasAnio.Text = oSnap.oDatosRem.iHorasContratadasAnio.S();

        //            lblSnapSeCobraComb.Text = oSnap.oDatosRem.bSeCobraCombustible ? "SI" : "NO";
        //            lblFormaCobroCombustible.Text = FormaCobroCombustible(oSnap.oDatosRem.eCobroCombustible);
        //            lblSnapFactorTramosNales.Text = oSnap.oDatosRem.dFactorTramosNal.S();
        //            lblSnapFactorTramosInter.Text = oSnap.oDatosRem.dFactorTramosInt.S();
        //            lblSnapCostoDirVueloNal.Text = oSnap.oDatosRem.dVueloCostoDirNal.ToString("c");
        //            lblSnapCostoDirVueloInt.Text = oSnap.oDatosRem.dVueloCostoDirInt.ToString("c");

        //            lblSnapSeCobraTiempoEspera.Text = oSnap.oDatosRem.bSeCobreEspera ? "SI" : "NO";
        //            lblSnapTarifaEspNal.Text = oSnap.oDatosRem.dTarifaNalEspera.ToString("c");
        //            lblSnapTarifaEspInt.Text = oSnap.oDatosRem.dTarifaIntEspera.ToString("c");
        //            lblSnapPorcentajeTarEspNal.Text = oSnap.oDatosRem.dPorTarifaNalEspera.S();
        //            lblSnapPorcentajeTarEspInt.Text = oSnap.oDatosRem.dPorTarifaIntEspera.S();
        //            lblSnapSeCobranPernoctas.Text = oSnap.oDatosRem.bSeCobraPernoctas ? "SI" : "NO";

        //            lblSnapTarifaDllsPerNal.Text = oSnap.oDatosRem.dTarifaDolaresNal.ToString("c");
        //            lblSnapPorcentajeTarDllsPerNal.Text = oSnap.oDatosRem.dPorTarifaVueloNal.S();
        //            lblSnapTarifaDllsPerInt.Text = oSnap.oDatosRem.dTarifaDolaresInt.ToString("c");
        //            lblSnapPorcentajeTarDllsPerInt.Text = oSnap.oDatosRem.dPorTarifaVueloInt.S();

        //            lblSnapAplicaVloSimultaneo.Text = oSnap.oDatosRem.bAplicaFactorVueloSimultaneo ? "SI" : "NO";
        //            lblSnapCuantosVuelosSim.Text = oSnap.oDatosRem.iCuantosVloSimultaneo.S();
        //            lblSnapFactorVuelosSim.Text = oSnap.oDatosRem.dFactorVloSimultaneo.S();
        //            lblSnapSeDescuentaEspNal.Text = oSnap.oDatosRem.bSeDescuentaEsperaNal ? "SI" : "NO";
        //            lblSnapSeDescuentaEspInt.Text = oSnap.oDatosRem.bSeDescuentaEsperaInt ? "SI" : "NO";
        //            lblSnapFactorEsperaHoraVueloNal.Text = oSnap.oDatosRem.dPorTarifaNalEspera.S(); //**//
        //            lblSnapFactorEsperaHoraVueloInt.Text = oSnap.oDatosRem.dPorTarifaIntEspera.S(); //**//

        //            lblSnapSeDescuentaPernoctaNal.Text = oSnap.oDatosRem.bSeDescuentanPerNal ? "SI" : "NO";
        //            lblSnapSeDescuentaPernoctaInt.Text = oSnap.oDatosRem.bSeDescuentanPerInt ? "SI" : "NO";
        //            lblSnapFactorPerHoraVueloNal.Text = oSnap.oDatosRem.dFactorEHrVueloNal.S();
        //            lblSnapFactorPerHoraVueloInt.Text = oSnap.oDatosRem.dFactorEHrVueloInt.S();

        //            //// Tarifas
        //            //lblppdCDN.Text = oRem.dVueloCostoDirNal.ToString("c");
        //            //lblppdCDI.Text = oRem.dVueloCostoDirInt.ToString("c");
        //            //lblppdTEN.Text = oRem.dTarifaNalEspera.ToString("c");
        //            //lblppdTEI.Text = oRem.dTarifaIntEspera.ToString("c");
        //            //lblppdPernoctasN.Text = oRem.dTarifaDolaresNal.ToString("c");
        //            //lblppdPernoctasI.Text = oRem.dTarifaDolaresInt.ToString("c");
        //            //// Cobros
        //            //lblppdCobroCombustible.Text = oRem.eCobroCombustible.S();
        //            //lblppdCobroFerrys.Text = oRem.eSeCobraFerry.S();
        //            //lblppdCobroTipoTiempo.Text = oRem.iCobroTiempo == 1 ? "Vuelo" : "Calzo";
        //            //lblppdCobroGiras.Text = oRem.bAplicaGiraEspera ? "Espera" : "Horario";
        //            //lblppdCobroTipoTiempoMinutos.Text = oRem.iMasMinutos.S();
        //            //lblppdTiempoEsperaLibre.Text = oRem.bAplicaEsperaLibre ? "Si" : "No";
        //            //gvIntercambios.DataSource = oRem.dtIntercambios;
        //            //gvIntercambios.DataBind();

        //            // Cobros Espera y Pernoctas
        //            lblSnapSeCobraTiempoEspera2.Text = oSnap.oDatosRem.bSeCobreEspera ? "SI" : "NO";
        //            lblSnapHorasPernocta.Text = oSnap.oDatosRem.iHorasPernocta.S();
        //            lblSnapTiempoEsperaGeneral.Text = oSnap.oDatosRem.sTiempoEsperaGeneral;
        //            lblSnapTiempoVueloGeneral.Text = oSnap.oDatosRem.sTiempoVueloGeneral;
        //            lblSnapFactorHrVuelo.Text = oSnap.oDatosRem.dFactorHrVuelo.S();
        //            lblSnapTotalTiempoEsperaCobrar.Text = oSnap.oDatosRem.sTotalTiempoEsperaCobrar;


        //            gvFactoresRem.DataSource = oSnap.oFactoresTramos.ConvertListToDataTable();
        //            gvFactoresRem.DataBind();


        //            pcPizarra.ShowOnPageLoad = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        public void LlenaNotasYCasos()
        {
            try
            {
                if (dsNotas.Tables.Count == 2)
                {
                    if (dsNotas.Tables[0].Rows.Count > 0)
                    {
                        txtNotasTrip.Text = dsNotas.Tables[0].Rows[0]["Notes"].S();
                        txtAlertasTrip.Text = dsNotas.Tables[0].Rows[0]["TSNotes"].S();
                    }

                    if (dsNotas.Tables[1].Rows.Count > 0)
                    {
                        gvCasos.DataSource = dsNotas.Tables[1];
                        gvCasos.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void AplicaFactoresHeader(FactoresTramos oFact)
        {
            try
            {
                ListItem lFe = chkList.Items.FindByValue("1");
                lFe.Text = "Factor Especial - " + (txtFactorEspecial.Text.S() == string.Empty ? "0" : txtFactorEspecial.Text.S());
                lFe.Selected = oFact.bAplicoFactorEspecial;


                ListItem lI = chkList.Items.FindByValue("2");
                lI.Text = "Intercambio - " + (oFact.dFactorIntercambio.S() == "1" ? "0" : oFact.dFactorIntercambio.S());
                lI.Selected = oFact.bAplicoIntercambio;

                ListItem lGe = chkList.Items.FindByValue("3");
                lGe.Text = "Gira Espera - " + (oFact.dFactorGiraEspera.S() == "1" ? "0" : oFact.dFactorGiraEspera.S());
                lGe.Selected = oFact.bAplicaGiraEspera;

                ListItem lGh = chkList.Items.FindByValue("4");
                lGh.Text = "Gira Horario - " + (oFact.dFactorGiraHorario.S() == "1" ? "0" : oFact.dFactorGiraHorario.S());
                lGh.Selected = oFact.bAplicaGiraHorario;

                ListItem lFp = chkList.Items.FindByValue("5");
                lFp.Text = "Fecha Pico - " + (oFact.dFactorFechaPico.S() == "1" ? "0" : oFact.dFactorFechaPico.S());
                lFp.Selected = oFact.bAplicaFactorFechaPico;

                ListItem lVs = chkList.Items.FindByValue("6");
                lVs.Text = "Vuelo Simultaneo - " + (oFact.dFactorVloSimultaneo.S() == "1" ? "0" : oFact.dFactorVloSimultaneo.S());
                lVs.Selected = oFact.bAplicaFactorVueloSimultaneo;


                // Pestaña 2
                ListItem lFeF = chkListF.Items.FindByValue("1");
                lFeF.Text = "Factor Especial - " + (txtFactorEspecial.Text.S() == string.Empty ? "0" : txtFactorEspecial.Text.S());
                lFeF.Selected = oFact.bAplicoFactorEspecial;

                ListItem lIF = chkListF.Items.FindByValue("2");
                lIF.Text = "Intercambio - " + (oFact.dFactorIntercambio.S() == "1" ? "0" : oFact.dFactorIntercambio.S());
                lIF.Selected = oFact.bAplicoIntercambio;

                ListItem lGeF = chkListF.Items.FindByValue("3");
                lGeF.Text = "Gira Espera - " + (oFact.dFactorGiraEspera.S() == "1" ? "0" : oFact.dFactorGiraEspera.S());
                lGeF.Selected = oFact.bAplicaGiraEspera;

                ListItem lGhF = chkListF.Items.FindByValue("4");
                lGhF.Text = "Gira Horario - " + (oFact.dFactorGiraHorario.S() == "1" ? "0" : oFact.dFactorGiraHorario.S());
                lGhF.Selected = oFact.bAplicaGiraHorario;

                ListItem lFpF = chkListF.Items.FindByValue("5");
                lFpF.Text = "Fecha Pico - " + (oFact.dFactorFechaPico.S() == "1" ? "0" : oFact.dFactorFechaPico.S());
                lFpF.Selected = oFact.bAplicaFactorFechaPico;

                ListItem lVsF = chkListF.Items.FindByValue("6");
                lVsF.Text = "Vuelo Simultaneo - " + (oFact.dFactorVloSimultaneo.S() == "1" ? "0" : oFact.dFactorVloSimultaneo.S());
                lVsF.Selected = oFact.bAplicaFactorVueloSimultaneo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private string AplicaFactoresATiempoFerryVirtual(string sTiempo)
        {
            try
            {
                string sRes = string.Empty;

                float fTemp = Utils.ConvierteTiempoaDecimal(sTiempo);
                decimal dTemp = fTemp.S().D();
                

                if (hdFactorIntercambio.Value.S().D() > 0)
                    dTemp = dTemp + ((hdFactorIntercambio.Value.S().D() - 1) * dTemp);

                if (hdFactorGiraHorario.Value.S().D() > 0)
                    dTemp = dTemp + ((hdFactorGiraHorario.Value.S().D() - 1) * dTemp);

                if (hdFactorGiraEspera.Value.S().D() > 0)
                    dTemp = dTemp + ((hdFactorGiraEspera.Value.S().D() - 1) * dTemp);

                if (hdFactorFechaPico.Value.S().D() > 0)
                    dTemp = dTemp + ((hdFactorFechaPico.Value.S().D() - 1) * dTemp);

                if (hdFactorVueloSimul.Value.S().D() > 0)
                    dTemp = dTemp + ((hdFactorVueloSimul.Value.S().D() - 1) * dTemp);


                sRes = Utils.ConvierteDecimalATiempo(dTemp);

                return sRes;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        private double AsignaFactoresHeader(FactoresTramos oFact)
        {
            try
            {
                double dbFactor = 1;

                dbFactor = dbFactor * (oFact.dFactorFactorEspecial == 0 ? 1 : oFact.dFactorFactorEspecial).S().Db();
                dbFactor = dbFactor * (oFact.dFactorIntercambio == 0 ? 1 : oFact.dFactorIntercambio).S().Db();
                dbFactor = dbFactor * (oFact.dFactorGiraEspera == 0 ? 1 : oFact.dFactorGiraEspera).S().Db();
                dbFactor = dbFactor * (oFact.dFactorGiraHorario == 0 ? 1 : oFact.dFactorGiraHorario).S().Db();
                dbFactor = dbFactor * (oFact.dFactorFechaPico == 0 ? 1 : oFact.dFactorFechaPico).S().Db();
                dbFactor = dbFactor * (oFact.dFactorVloSimultaneo == 0 ? 1 : oFact.dFactorVloSimultaneo).S().Db();
                

                return dbFactor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string FormaCobroCombustible(Enumeraciones.CobroCombustible oCobro)
        {
            try
            {
                string sFormaCobroComb = string.Empty;
                switch (oCobro)
                {
                    case Enumeraciones.CobroCombustible.HorasDescuento:
                        sFormaCobroComb = "Horas de descuento";
                        break;
                    case Enumeraciones.CobroCombustible.Ninguno:
                        sFormaCobroComb = "Ninguno";
                        break;
                    case Enumeraciones.CobroCombustible.Promedio:
                        sFormaCobroComb = "Promedio";
                        break;
                    case Enumeraciones.CobroCombustible.Rango:
                        sFormaCobroComb = "Rango";
                        break;
                    case Enumeraciones.CobroCombustible.Real:
                        sFormaCobroComb = "Real";
                        break;
                }

                return sFormaCobroComb;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //private void ConsultaDetalle(int iIdPadre)
        //{
        //    if (Session["SnapshotRem"] != null)
        //    {
        //        SnapshotRemision oSnap = new SnapshotRemision();
        //        oSnap = (SnapshotRemision)Session["SnapshotRem"];

        //        DataRow[] rows = oSnap.oFactoresTramos.ConvertListToDataTable().Select("iNoTramo = " + iIdPadre.S());
        //        dtFactoresTramo = new DataTable();
        //        dtFactoresTramo = rows.CopyToDataTable();
        //    }
        //}
        #endregion

        #region VARIABLES Y PROPIEDADES

        private const string sClase = "frmGRemision.aspx.cs";
        private const string sPagina = "frmGRemision.aspx";

        GRemision_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eGetContracts;
        public event EventHandler eGetTiposPierna;
        public event EventHandler eSetTramosRem;
        public event EventHandler eSaveImportesR;
        public event EventHandler eGetServiciosC;
        public event EventHandler eSetFinalizaR;
        public event EventHandler eGetPasoUno;
        public event EventHandler eLoadOrigDestFiltro;
        public event EventHandler eSaveImportesOpc2;
        public event EventHandler eGetNotasTrip;
        public event EventHandler eGetContractsDates;
        public event EventHandler eSetTramosCotizacion;
        public event EventHandler eGetTipoPaquete;

        public int iConversion
        {
            get { return (int)ViewState["VSConversion"]; }
            set { ViewState["VSConversion"] = 0; }
        }
        public int iPaquete
        {
            get { return (int)ViewState["VSPaquete"]; }
            set { ViewState["VSPaquete"] = value; }
        }
        public int IdCliente
        {
            get 
            {
                return ddlCliente.SelectedItem.Value.S().I();
            }
        }
        public int IdContrato
        {
            get 
            {
                return ddlContrato.SelectedItem.Value.S().I();
            }
        }
        public long iIdRemision
        {
            get { return (long)ViewState["IdRemision"]; }
            set { ViewState["IdRemision"] = value; }
        }
        public int iIdPresupuesto
        {
            get { return (int)ViewState["VSIdPresupuesto"]; }
            set { ViewState["VSIdPresupuesto"] = value; }
        }
        public int iNoContrato
        {
            get { return (int)ViewState["VSNoContrato"]; }
            set { ViewState["VSNoContrato"] = value; }
        }
        public decimal dTotalPesos
        {
            get { return (decimal)ViewState["VSTotalPesos"]; }
            set { ViewState["VSTotalPesos"] = value; }
        }
        public decimal dTotalDlls
        {
            get { return (decimal)ViewState["VSTotalDlls"]; }
            set { ViewState["VSTotalDlls"] = value; }
        }
        public Remision oRemision
        {
            get
            {
                Remision oRem = new Remision();
                DateTime dtFecha = new DateTime(detFecha.Value.S().Substring(6,4).S().I(),detFecha.Value.S().Substring(3,2).S().I(),detFecha.Value.S().Substring(0,2).S().I());

                oRem.dtFechaRemision = dtFecha;
                //oRem.iIdCliente = ddlCliente.SelectedValue.S().I();
                //oRem.iIdContrato = ddlContrato.SelectedValue.S().I();
                oRem.iIdCliente = ddlCliente.SelectedItem.Value.S().I();
                oRem.iIdContrato = ddlContrato.SelectedItem.Value.S().I();
                oRem.sCliente = ddlCliente.SelectedItem.Text.S();
                oRem.sContrato = ddlContrato.SelectedItem.Text.S();
                oRem.bAplicaIntercambio = chkAplicaIntercambio.Checked;
                oRem.dFactorEspecial = txtFactorEspecial.Text.S().D();
                oRem.sUsuarioCreacion = ((UserIdentity)Session["UserIdentity"]).sUsuario;
                
                return oRem;
            }
            set
            {
                Remision oTemp = value;
                if (oTemp != null)
                {
                    ddlCliente.Value = oTemp.iIdCliente.S();
                    detFecha.Value = oTemp.dtFechaRemision;
                    chkAplicaIntercambio.Checked = oTemp.bAplicaIntercambio;
                    txtFactorEspecial.Text = oTemp.dFactorEspecial.S();

                    ddlCliente_SelectedIndexChanged(null, EventArgs.Empty);
                    ddlContrato.Value = oTemp.iIdContrato.S();
                }
            }
        }
        public DataTable dtTiposPierna
        {
            get { return (DataTable)ViewState["TiposPierna"]; }
            set { ViewState["TiposPierna"] = value; }
        }
        public DataTable dtConceptosR
        {
            get { return (DataTable)ViewState["VSConceptos"]; }
            set { ViewState["VSConceptos"] = value; }
        }
        public DataTable dtConceptosR2
        {
            get { return (DataTable)ViewState["VSConceptos2"]; }
            set { ViewState["VSConceptos2"] = value; }
        }
        public DataTable dtTramos
        {
            get { return (DataTable)ViewState["VSTramos"]; }
            set { ViewState["VSTramos"] = value; }
        }
        public DataTable dtServiciosC
        {
            get { return (DataTable)ViewState["VSServiciosC"]; }
            set { ViewState["VSServiciosC"] = value; }
        }
        public DataTable dtServVuelo
        {
            get { return (DataTable)ViewState["VSServVuelo"]; }
            set { ViewState["VSServVuelo"] = value; }
        }
        public DataTable dtServCargo
        {
            get { return (DataTable)ViewState["VSServCargo"]; }
            set { ViewState["VSServCargo"] = value; }
        }
        public string sIVA
        {
            get 
            {
                decimal dIva = 0;
                dIva = Utils.GetParametrosClave("9").S().D();

                return (dIva * 100).S();
            }
        }
        public string sFiltro
        {
            get { return (string)ViewState["VSFiltro"]; }
            set { ViewState["VSFiltro"] = value; }
        }
        public DataTable dtTramosRem
        {
            get { return (DataTable)ViewState["TramosRemision"]; }
            set { ViewState["TramosRemision"] = value; }
        }
        public DataTable dtTramosRemOpc2
        {
            get { return (DataTable)ViewState["VSTramosOpc2"]; }
            set { ViewState["VSTramosOpc2"] = value; }
        }
        public ALE_MexJet.Objetos.Enumeraciones.TipoOperacion eCrud
        {
            get { return (ALE_MexJet.Objetos.Enumeraciones.TipoOperacion)ViewState["eCrud"]; }
            set { ViewState["eCrud"] = value; }
        }
        public List<BitacoraRemision> oLstBit
        {
            get
            {
                List<BitacoraRemision> oLst = new List<BitacoraRemision>();

                foreach (GridViewRow row in gvTramos.Rows)
                {
                    ASPxButton btnPre = (ASPxButton)row.FindControl("btnPreferencias");
                    CheckBox chk = (CheckBox)row.FindControl("chbSeleccione");
                    DropDownList ddl = (DropDownList)row.FindControl("ddlTipoPierna");
                    CheckBox chkSeCobra = (CheckBox)row.FindControl("chkSeCobra");

                    if (chk != null && chk.Checked && ddl != null && chkSeCobra != null)
                    {
                        BitacoraRemision oBit = new BitacoraRemision();
                        oBit.iIdRemision = iIdRemision;
                        oBit.lBitacora = btnPre.CommandArgument.S().L();
                        oBit.iIdSolicitudVuelo = row.Cells[2].Text.I();
                        oBit.iIdTipoPierna = ddl.SelectedValue.S().I();
                        oBit.iSeCobra = chkSeCobra.Checked == true ? 1 : 0;
                        oBit.iPax = row.Cells[9].Text.S().I();

                        oLst.Add(oBit);
                    }
                }

                return oLst;
            }
        }
        public List<ImportesRemision> oLstImp
        {
            get
            {
                List<ImportesRemision> oLst = new List<ImportesRemision>();

                foreach (GridViewRow row in gvConceptos.Rows)
                {
                    ImportesRemision oR = new ImportesRemision();
                    oR.iIdRemision = iIdRemision;
                    oR.iIdConcepto = dtConceptosR.Rows[row.RowIndex]["IdConcepto"].S().I();
                    oR.sCantidad = dtConceptosR.Rows[row.RowIndex]["Cantidad"].S();
                    oR.dTarifaDlls = dtConceptosR.Rows[row.RowIndex]["TarifaDlls"].S().D();
                    oR.dImporte = dtConceptosR.Rows[row.RowIndex]["Importe"].S().D();
                    oR.sHrDescontar = dtConceptosR.Rows[row.RowIndex]["HrDescontar"].S();
                    oR.sUsuario = ((UserIdentity)Session["UserIdentity"]).sUsuario;
                    oR.sIP = Utils.GetIPAddress();

                    oLst.Add(oR);
                }

                return oLst;
            }
        }
        public ServiciosVueloH oServiciosV
        {
            get
            {
                try
                {
                    ServiciosVueloH oSer = new ServiciosVueloH();

                    oSer.iIdRemision = iIdRemision;
                    foreach (GridViewRow gr in gvSC.Rows)
                    {
                        if (gr.Cells[4].Text == "Total")
                            oSer.dTotal = gr.Cells[5].Text.S().Replace("$", "").Replace(",", "").S().D();

                        if (gr.Cells[4].Text == "SubTotal")
                        {
                            oSer.dSubtotal = gr.Cells[5].Text.S().Replace("$", "").Replace(",", "").S().D();
                            oSer.sHrDescontar = gr.Cells[6].Text.S();
                        }

                        if (gr.Cells[4].Text.S().Contains("IVA"))
                            oSer.dIva = gr.Cells[5].Text.S().Replace("$", "").Replace(",", "").S().D();
                    }

                    oSer.dCombustibleNal = dtServVuelo.Rows[0]["CombustibleAumento"].S().D();
                    oSer.dCombustibleInt = dtServVuelo.Rows[1]["CombustibleAumento"].S().D();

                    oSer.iFactorIva = Math.Truncate(iFactorIVASV.Db() * 100).S().I();
                    oSer.dTipoCambio = Utils.GetTipoCambioDia;


                    oSer.sUsuario = ((UserIdentity)Session["UserIdentity"]).sUsuario;
                    oSer.sIP = Utils.GetIPAddress();

                    return oSer;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public List<ServiciosVueloD> oLstSV
        {
            get
            {
                try
                {
                    List<ServiciosVueloD> oLs = new List<ServiciosVueloD>();
                    string sUs = ((UserIdentity)Session["UserIdentity"]).sUsuario;
                    string sIp = Utils.GetIPAddress();
                    
                    foreach (DataRow dr in dtServVuelo.Rows)
                    {
                        if (dr.S("IdConcepto") != "999" && dr.S("IdConcepto") != "998" && dr.S("IdConcepto") != "997")
                        {
                            ServiciosVueloD oSer = new ServiciosVueloD();
                            oSer.iIdRemision = iIdRemision;
                            oSer.iIdConcepto = dr["IdConcepto"].S().I();
                            oSer.dCostoDirecto = dr["CostoDirecto"].S().D();
                            oSer.dCostoComb = dr["TarifaVuelo"].S().D();
                            oSer.dTarifaDlls = dr["TarifaDlls"].S().D();
                            oSer.sCantidad = dr["Cantidad"].S();
                            oSer.dImporteDlls = dr["ImporteDlls"].S().D();
                            oSer.sHrDescontar = dr["HrDescontar"].S();
                            oSer.sUsuario = sUs;
                            oSer.sIP = sIp;

                            oLs.Add(oSer);
                        }
                    }

                    return oLs;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public ServiciosCargoH oServiciosC
        {
            get
            {
                try
                {
                    ServiciosCargoH oSer = new ServiciosCargoH();
                    oSer.iIdRemision = iIdRemision;

                    foreach (GridViewRow gr in gvServiciosC.Rows)
                    {
                        if (gr.Cells[0].Text == "Total")
                        {
                            ASPxTextBox txt = (ASPxTextBox)gr.FindControl("txtImporte");
                            oSer.dTotal = txt.Text.S().D();
                        }

                        if (gr.Cells[0].Text == "SubTotal")
                        {
                            ASPxTextBox txt = (ASPxTextBox)gr.FindControl("txtImporte");
                            oSer.dSubtotal = txt.Text.S().D();
                        }

                        if (gr.Cells[0].Text == "IVA Nal. " + sIVA + "%")
                        {
                            ASPxTextBox txt = (ASPxTextBox)gr.FindControl("txtImporte");
                            oSer.dIva = txt.Text.S().D();
                        }
                    }

                    oSer.iFactorIVA = sIVA.S().D();
                    oSer.sUsuario = ((UserIdentity)Session["UserIdentity"]).sUsuario;
                    oSer.sIP = Utils.GetIPAddress();

                    return oSer;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public List<ServiciosCargoD> oLstD 
        {
            get
            {
                try
                {
                    List<ServiciosCargoD> oLst = new List<ServiciosCargoD>();
                    string sUs = ((UserIdentity)Session["UserIdentity"]).sUsuario;
                    string sIp = Utils.GetIPAddress();

                    foreach (DataRow row in dtServiciosC.Rows)
                    {
                        if (row.S("IdServicioConCargo") != "999999" && row.S("IdServicioConCargo") != "999998" && row.S("IdServicioConCargo") != "999997")
                        {
                            ServiciosCargoD oSer = new ServiciosCargoD();
                            oSer.iIdRemision = iIdRemision;
                            oSer.iIdServicioCargo = row["IdServicioConCargo"].S().I();
                            oSer.dImporte = row["Importe"].S().D();
                            oSer.sUsuario = sUs;
                            oSer.sIP = sIp;

                            oLst.Add(oSer);
                        }
                    }

                    return oLst;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public RemisionDatosGrals oRemGrals
        {
            get
            {
                try
                {
                    RemisionDatosGrals oRG = new RemisionDatosGrals();
                    oRG.iIdRemision = iIdRemision;
                    oRG.sTotalTiempoCobrar = lblRespTotalVueloCobrar.Text.S();
                    oRG.bAplicaFactorEspecual = chkList.Items.FindByValue("1").Selected;
                    oRG.bIntercambio = chkList.Items.FindByValue("2").Selected;
                    oRG.bGira = chkList.Items.FindByValue("3").Selected;
                    oRG.bGiraHorario = chkList.Items.FindByValue("4").Selected;
                    oRG.bAplicaHoraPico = chkList.Items.FindByValue("5").Selected;
                    oRG.bAplicaVueloSimultaneo = chkList.Items.FindByValue("6").Selected;
                    oRG.bAplicaFactorTramoNal = chkList.Items.FindByValue("7").Selected;
                    oRG.bAplicaFactorTramoInt = chkList.Items.FindByValue("8").Selected;

                    oRG.dFactorIntercambio = hdFactorIntercambio.Value.S().D();
                    oRG.dFactorGiraEspera = hdFactorGiraEspera.Value.S().D();
                    oRG.dFactorGiraHorario = hdFactorGiraHorario.Value.S().D();
                    oRG.dFactorFechaPico = hdFactorFechaPico.Value.S().D();
                    oRG.dFactorVueloSimultaneo = hdFactorVueloSimul.Value.S().D();
                    oRG.dFactorTramoNal = hdFactorTramoNal.Value.S().D();
                    oRG.dFactorTramoInt = hdFactorTramoInt.Value.S().D();

                    oRG.dTotalRemisionPesos = dTotalPesos;
                    oRG.dTotalRemisionDlls = dTotalDlls;
                    oRG.iStatus = 2;
                    oRG.sUsuario = ((UserIdentity)Session["UserIdentity"]).sUsuario;
                    oRG.sIP = Utils.GetIPAddress();
                    oRG.IIdContrato = IdContrato;

                    //if (Session["Matricula"] != null)
                    //    oRG.SMatricula = Session["Matricula"].S();
                    //else
                    //    oRG.SMatricula = string.Empty;

                    //oRG.SCargo = lblRespTotalVueloCobrar.Text.S();
                    //oRG.SAbono = "0";
                    //oRG.IIdMotivo = 1; //Remisión
                    //oRG.SNotas = string.Empty;
                    

                    return oRG;
                }
                catch(Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<KardexRemision> OLstKardex
        {
            get { return (List<KardexRemision>)ViewState["VSLstKardex"]; }
            set { ViewState["VSLstKardex"] = value; }
        }

        public DataTable dtOrigenDestino
        {
            get { return (DataTable)ViewState["VSOrigenDestino"]; }
            set { ViewState["VSOrigenDestino"] = value; }
        }
        public DataTable dtOrigenDestino2
        {
            get { return (DataTable)ViewState["VSOrigenDestino2"]; }
            set { ViewState["VSOrigenDestino2"] = value; }
        }
        public decimal iFactorIVASV
        {
            get { return (decimal)ViewState["VSiFactorIVA"]; }
            set { ViewState["VSiFactorIVA"] = value; }
        }
        public Enumeraciones.SeCobraFerrys eSeCobraFerrys
        {
            set { ViewState["VSSeCobraFerrys"] = value; }
            get { return (Enumeraciones.SeCobraFerrys)ViewState["VSSeCobraFerrys"]; }
        }
        public DateTime dtFechaHoy
        {
            get
            {
                return Utils.ObtieneFechaServidor;
            }
        }
        public DataSet dsNotas
        {
            get { return (DataSet)ViewState["VSdsNotas"]; }
            set { ViewState["VSdsNotas"] = value; }
        }
        public int iIndicePestana
        {
            get { return (int)ViewState["VSIndice"]; }
            set { ViewState["VSIndice"] = value; }
        }
        public string sTipoPierna
        {
            get { return (string)ViewState["VSsTipoPierna"]; }
            set { ViewState["VSsTipoPierna"] = value; }
        }
        public DateTime dtFechaFerruyV
        {
            get { return (DateTime)ViewState["VSFechaFV"]; }
            set { ViewState["VSFechaFV"] = value; }
        }
        public DatosRemision oDatosFactor
        {
            get { return (DatosRemision)ViewState["VSDatosFactor"]; }
            set { ViewState["VSDatosFactor"] = value; }
        }
        public double dbFactorTotalRem
        {
            get { return (double)ViewState["VSFactorTotalRem"]; }
            set { ViewState["VSFactorTotalRem"] = value; }
        }
        public decimal dSubTotalRemSC
        {
            get { return (decimal)ViewState["VSSubTotalRemSC"]; }
            set { ViewState["VSSubTotalRemSC"] = value; }
        }
        public DataTable dtPackRem
        {
            get { return (DataTable)ViewState["VSPackRem"]; }
            set { ViewState["VSPackRem"] = value; }
        }
        //public DataTable dtFactoresTramo
        //{
        //    get { return (DataTable)ViewState["VSDTFactoresTramo"]; }
        //    set { ViewState["VSDTFactoresTramo"] = value; }
        //}
        #endregion

        protected void btnFinalizarSCC_Click(object sender, EventArgs e)
        {
            try
            {
                //Inserta Servicio con cargo con Conversión
                iConversion = 1;
                if(iConversion == 1)
                    RemisionesKardex();

                if (eSetFinalizaR != null)
                    eSetFinalizaR(sender, e);

                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "redirecciona", "window.location.href = 'frmRemisiones.aspx'", true);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnFinalizarSCC_Click", "Aviso");
            }
        }
    }
}