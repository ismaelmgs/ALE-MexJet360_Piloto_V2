using ALE_MexJet.DomainModel;
using ALE_MexJet.Interfaces;
using ALE_MexJet.Presenter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NucleoBase.Core;
using ALE_MexJet.Objetos;
using DevExpress.Web;
using ALE_MexJet.Clases;
using System.IO;
using System.Drawing;
using System.Net.Mail;
using System.Net.Mime;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Text;
using DevExpress.XtraPrinting;
using DevExpress.Export;
using DevExpress.Web.Bootstrap;

namespace ALE_MexJet.Views.JetSmart
{
    public partial class frmOfertaFerry : System.Web.UI.Page, IViewOfertaFerry
    {

        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            mpeMensaje.OkButtonPressed += mpeMensaje_OkButtonPressed;
            oPresenter = new OfertaFerry_Presenter(this, new DBOfertaFerry());

            if (!IsPostBack)
            {
                //hfIdFerry["IdFerry_value"] = 0;
                //hfOrigenFV["OrigenFV_value"] = 0;
                //hfTipoListaDifusion["hfTipoListaDifusion_value"] = string.Empty;

                //sRgbG = Utils.GetParametrosClave("88");
                //sRgbM = Utils.GetParametrosClave("89");
                //sRgbP = Utils.GetParametrosClave("90");

                //if (eSearchObj != null)
                //    eSearchObj(sender, e);

                //iIdPadre = 0;

                //if (ddlEstatus.SelectedItem.Value.S() == "1")
                //{
                //    if (eSearchFerryPendiente != null)
                //        eSearchFerryPendiente(sender, e);
                //}
            }
        }
        private void mpeMensaje_OkButtonPressed(object sender, EventArgs args)
        {

        }
        protected void btOK_Click(object sender, EventArgs e)
        {
            Session["SCad"] = sCadena;
            //tmDescarga.Enabled = true;
        }
        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            try
            {
                if (eSaveObj != null)
                    eSaveObj(sender, e);

                iIdPadre = 0;

                if (eSearchFerryPendiente != null)
                    eSearchFerryPendiente(sender, e);

            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnEnviar_Click", "Aviso");
            }
        }
        protected void UpdatePanel1_Unload(object sender, EventArgs e)
        {
            MethodInfo methodInfo = typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Where(i => i.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel")).First();
            methodInfo.Invoke(ScriptManager.GetCurrent(Page),
                new object[] { sender as UpdatePanel });
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        //protected void gvFerrys_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        iIdPadre = gvFerrys.KeyFieldName[e.Row.RowIndex].ToString().I();

        //        int iHorasMinimas = Utils.ObtieneParametroPorClave("112").S().I();
        //        int iHorasMaximas = Utils.ObtieneParametroPorClave("113").S().I();

        //        if (dtFerrys.Rows[e.Row.RowIndex]["Diferencia"].S().I() <= iHorasMinimas)
        //        {
        //            string hex = sRgbP;
        //            Color _color = System.Drawing.ColorTranslator.FromHtml(hex);
        //            e.Row.BackColor = _color;
        //        }
        //        if (dtFerrys.Rows[e.Row.RowIndex]["Diferencia"].S().I() > iHorasMinimas && dtFerrys.Rows[e.Row.RowIndex]["Diferencia"].S().I() <= iHorasMaximas)
        //        {
        //            string hex = sRgbM;
        //            Color _color = System.Drawing.ColorTranslator.FromHtml(hex);
        //            e.Row.BackColor = _color;
        //        }
        //        if (dtFerrys.Rows[e.Row.RowIndex]["Diferencia"].S().I() > iHorasMaximas)
        //        {
        //            string hex = sRgbG;
        //            Color _color = System.Drawing.ColorTranslator.FromHtml(hex);
        //            e.Row.BackColor = _color;
        //        }


        //        GridView gvFerryHijo = (GridView)e.Row.FindControl("gvFerrysHijo");
        //        gvFerryHijo.ID = "gvFerrysHijo" + e.Row.RowIndex;

        //        if (gvFerryHijo != null)
        //        {
        //            if (eSearchFerryHijo != null)
        //            {
        //                eSearchFerryHijo(sender, e);

        //                gvFerryHijo.DataSource = dtFerrysHijo;
        //                gvFerryHijo.DataBind();

        //                NumeroGVFerrysHijo = NumeroGVFerrysHijo + 1;
        //            }
        //        }
        //    }
        //}
        //protected void gvFerrysDetalle_BeforePerformDataSelect(object sender, EventArgs e)
        //{
        //    ASPxGridView grid = sender as ASPxGridView;

        //    iIdPadre = grid.GetMasterRowKeyValue().S().I();

        //    if (eSearchFerryHijo != null)
        //    {
        //        eSearchFerryHijo(sender, e);
        //        grid.DataSource = dtFerrysHijo;
        //    }
        //}
        //protected void ddlEstatus_ValueChanged(object sender, EventArgs e)
        //{
        //    switch (ddlEstatus.Value.S())
        //    {
        //        case "1":

        //            iIdPadre = 0;

        //            if (eSearchFerryPendiente != null)
        //                eSearchFerryPendiente(sender, e);


        //            break;
        //        case "2":

        //            if (eSearchEnviadas != null)
        //                eSearchEnviadas(sender, e);
        //            break;
        //    }
        //}
        //protected void tmDescarga_Tick(object sender, EventArgs e)
        //{
        //    /*
        //    if (Session["SCad"].S() == string.Empty)
        //        tmDescarga.Enabled = false;
        //    else
        //    {
        //        Response.Clear();
        //        Response.Buffer = true;
        //        Response.AddHeader("content-disposition", "attachment;filename=JetSmart.csv");
        //        Response.Charset = "";
        //        Response.ContentType = "application/text";
        //        Response.Output.Write(Session["SCad"].S());
        //        Session["SCad"] = string.Empty;
        //        Response.Flush();
        //        Response.End();
        //    }
        //     */
        //}
        protected void btnNuevoFerry_Click(object sender, EventArgs e)
        {
            try
            {
                eTipoFerry = TipoFerry.EsPadre;
                iIdPadre = 0;

                LimpiaCamposModal();
                ppTramos.ShowOnPageLoad = true;
                ddlOrigenFV.Enabled = true;
                ddlOrigenFV.ValidationSettings.EnableCustomValidation = true;
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnNuevoFerry_Click", "Aviso");
            }
        }
        protected void btnAgregarFV_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidaModal())
                {
                    DateTime FechaSalida = txtFechaSalidaFV.Value.Dt();

                    sTime = txtTiempoVuelo.Text;

                    FechaLlegada = FechaSalida.AddHours(sTime.Substring(0, 2).S().I());
                    FechaLlegada = FechaLlegada.AddMinutes(sTime.Substring(3, 2).S().I());

                    if (FechaSalida < FechaLlegada)
                    {
                        if (eSavFerryPendiente != null)
                            eSavFerryPendiente(sender, e);

                        LimpiaCamposModal();

                        ppTramos.ShowOnPageLoad = false;
                    }
                    else
                        ppTramos.ShowOnPageLoad = true;
                }
            }
            catch (Exception ex)
            {

                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnAgregarFV_Click", "Aviso");
            }
        }
        protected void txtFechaSalidaFV_DateChanged(object sender, EventArgs e)
        {
            try
            {
                DateTime dtOrigen = new DateTime();
                DateTime dtDestino = new DateTime();

                dtOrigen = txtFechaSalidaFV.Value.S().Dt();
                dtDestino = txtFechaLlegadaFV.Value.S().Dt();

                dtOrigen = dtOrigen.AddSeconds(dtOrigen.Second * -1);
                dtDestino = dtDestino.AddSeconds(dtOrigen.Second * -1);
                
                DatosRemision oDatosContrato = new DatosRemision();

                ddlOrigenFV.Text = sFiltroO;
                ddlOrigenFV.Value = sFiltroO;

                ddlDestinoFV.Text = sFiltroD;
                ddlDestinoFV.Value = sFiltroD;

                if (oDatosContrato != null)
                {
                    string sTiempo = Utils.ObtieneTiempoTramosPresupuestos(oDatosContrato, ddlOrigenFV.Value.S().I(), ddlDestinoFV.Value.S().I(), iIdGrupoModelo);

                    txtTiempoVuelo.Text = sTiempo;

                    string[] sTiempos = sTiempo.Split(':');
                    DateTime dtLlegada = txtFechaSalidaFV.Text.S().Dt();

                    dtLlegada = dtLlegada.AddHours(sTiempos[0].S().I());
                    dtLlegada = dtLlegada.AddMinutes(sTiempos[1].S().I());

                    txtFechaLlegadaFV.Value = dtLlegada;
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "txtFechaSalidaFV_DateChanged", "Aviso");
            }
        }
        protected void txtFechaLlegada_DateChanged(object sender, EventArgs e)
        {
            try
            {
                DateTime FechaSalida;
                DateTime FechaLlegada;
                ASPxDateEdit txt = (ASPxDateEdit)sender;
                ASPxDateEdit txtSalida = txtFechaSalidaFV;

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
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "txtFechaLlegada_DateChanged", "Aviso");
            }
        }
        protected void ddlOrigenFV_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            try
            {
                //Verificar si tiene pierna predecesora para obtener el ICAO y mandarlo como parametro.
                //DevExpress.Web.Bootstrap.BootstrapComboBox comboBox = (DevExpress.Web.Bootstrap.BootstrapComboBox)source;
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
                ASPxComboBox comboBox = (ASPxComboBox)source;
                sFiltroO = e.Text;

                //if (eLoadOrigDestFiltro != null)
                //eLoadOrigDestFiltro(source, e);
                dtOrigen = new DBPresupuesto().GetAeropuertosOrigen(sFiltroO, 2);

                CargaComboAeropuertosFV(comboBox, dtOrigen, sFiltroO);
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
                //DevExpress.Web.Bootstrap.BootstrapComboBox comboBox = (DevExpress.Web.Bootstrap.BootstrapComboBox)source;
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
                ASPxComboBox comboBox = (ASPxComboBox)source;
                sFiltroD = e.Text;

                dtDestino = new DBPresupuesto().GetAeropuertosDestino(sFiltroD, string.Empty, 2);

                CargaComboAeropuertosFV(comboBox, dtDestino, sFiltroD);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "ddlDestinoFV_ItemRequestedByValue", "Aviso");
            }
        }
        protected void ddlMatricula_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                iIdGrupoModelo = dtMatriculas.Rows[ddlMatricula.SelectedIndex]["GrupoModeloId"].S().I();
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "ddlMatricula_SelectedIndexChanged", "Aviso");
            }
        }
        protected void btnExcel_Click(object sender, EventArgs e)
        {
            //gvFerrys.ExportXlsToResponse();
        }
        //protected void gvFerrys_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
        //{
        //    try
        //    {
        //        string sOpcion = ((DevExpress.Web.ASPxButton)e.CommandSource).CommandName.S();

        //        if (sOpcion == "Agregar")
        //        {
        //            //GridViewRow gvr = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
        //            //int RowIndex = gvr.RowIndex;
        //            eTipoFerry = TipoFerry.EsHijo;
        //            int RowIndex = e.VisibleIndex;

        //            string Argumentos = e.CommandArgs.CommandArgument.S();
        //            string[] Parametros = Argumentos.Split('/');

        //            iIdPadre = Parametros[1].I();


        //            object cellValues = gvFerrys.GetRowValues(RowIndex, new string[] { "Trip" //0
        //                                                                        ,"NoPierna" //1
        //                                                                        ,"Origen" //2
        //                                                                        ,"FechaSalida" //3
        //                                                                        ,"FechaSalidaA" //4
        //                                                                        ,"Destino" //5
        //                                                                        ,"FechaLlegadaB" //6
        //                                                                        ,"FechaLlegadaA" //7
        //                                                                        ,"Matricula" //8
        //                                                                        ,"TiempoVuelo" //9
        //                                                                        ,"GrupoModelo" //10
        //                                                                        ,"LegId" //11
        //                                                                        ,"IdFerry" //12
        //                                                        });

        //            txtNoTrip.Text = ((object[])cellValues)[0].S(); //gvFerrys.Rows[RowIndex].Cells[2].Text;
        //            txtNoTrip.ReadOnly = true;
        //            ddlOrigenFV.Text = Parametros[0];
        //            ddlMatricula.Text = ((object[])cellValues)[8].S(); //gvFerrys.Rows[RowIndex].Cells[9].Text;
        //            ddlMatricula.ReadOnly = true;
        //            txtFechaSalidaFV.Text = ((object[])cellValues)[3].S();

        //            ppTramos.ShowOnPageLoad = true;
        //        }
        //        else if (sOpcion == "EditarListaMail")
        //        {
        //            //chklbxLista.UnselectAll();

        //            //iIdPendiente = e.CommandArgument.I();

        //            //sTipoListaDifusion = "MAIL";

        //            //ppListaDifusion.ShowOnPageLoad = true;

        //            //chklbxLista.Visible = true;

        //            //ppListaDifusion.HeaderText = "Seleccione las listas de difusión para Maíl:";

        //            //hfTipoListaDifusion["hfTipoListaDifusion_value"] = "MAIL";

        //            //CargaListaDifusion(sTipoListaDifusion);

        //            //CargaListaDifusionFerry();

        //        }
        //        else if (sOpcion == "EditarListaSMS")
        //        {
        //            //chklbxLista.UnselectAll();

        //            //iIdPendiente = e.CommandArgument.I();

        //            //sTipoListaDifusion = "SMS";

        //            //ppListaDifusion.ShowOnPageLoad = true;

        //            //chklbxLista.Visible = true;

        //            //ppListaDifusion.HeaderText = "Seleccione las listas de difusión para SMS:";

        //            //hfTipoListaDifusion["hfTipoListaDifusion_value"] = "SMS";

        //            //CargaListaDifusion(sTipoListaDifusion);

        //            //CargaListaDifusionFerry();

        //        }
        //        else if (sOpcion == "EliminarFerry")
        //        {
        //            iIdPendiente = e.CommandArgs.CommandArgument.I();

        //            if (eDeleteOfertaFerryPendiente != null)
        //                eDeleteOfertaFerryPendiente(sender, e);

        //            iIdPadre = 0;
        //            iIdPendiente = 0;

        //            if (eSearchFerryPendiente != null)
        //                eSearchFerryPendiente(sender, e);

        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvFerrys_RowCommand", "Aviso");
        //    }
        //}
        //protected void gvFerrysHijo_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
        //{
        //    string sOpcion = ((DevExpress.Web.ASPxButton)e.CommandSource).CommandName.S();

        //    if (sOpcion == "EditarListaMail")
        //    {
        //        //chklbxLista.UnselectAll();

        //        //iIdPendiente = e.CommandArgument.I();

        //        //sTipoListaDifusion = "MAIL";

        //        //ppListaDifusion.ShowOnPageLoad = true;

        //        //chklbxLista.Visible = true;

        //        //ppListaDifusion.HeaderText = "Seleccione las listas de difusión para Maíl:";

        //        //hfTipoListaDifusion["hfTipoListaDifusion_value"] = "MAIL";

        //        //CargaListaDifusionFerry();
        //    }
        //    else if (sOpcion == "EditarListaSMS")
        //    {
        //        //chklbxLista.UnselectAll();

        //        //iIdPendiente = e.CommandArgument.I();

        //        //sTipoListaDifusion = "SMS";

        //        //ppListaDifusion.ShowOnPageLoad = true;

        //        //chklbxLista.Visible = true;

        //        //ppListaDifusion.HeaderText = "Seleccione las listas de difusión para SMS:";

        //        //hfTipoListaDifusion["hfTipoListaDifusion_value"] = "SMS";

        //        //CargaListaDifusionFerry();
        //    }
        //    else if (sOpcion == "EliminarFerry")
        //    {
        //        iIdPendiente = e.CommandArgs.CommandArgument.I();

        //        if (eDeleteOfertaFerryPendiente != null)
        //            eDeleteOfertaFerryPendiente(sender, e);

        //        iIdPadre = 0;
        //        iIdPendiente = 0;

        //        if (eSearchFerryPendiente != null)
        //            eSearchFerryPendiente(sender, e);

        //    }
        //}
        //protected void gvFerrysHijo_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    try
        //    {
        //        if (e.Row.RowType == DataControlRowType.DataRow)
        //        {

        //            //ASPxDropDownEdit dde = (ASPxDropDownEdit)e.Row.FindControl("ASPxDropDownEdit1Hijo");
        //            //ASPxDropDownEdit ddeSMS = (ASPxDropDownEdit)e.Row.FindControl("ddeSMSHijo");
        //            //ASPxCheckBox ChkJ = (ASPxCheckBox)e.Row.FindControl("chkSmartHijo");
        //            //ASPxCheckBox ChkA = (ASPxCheckBox)e.Row.FindControl("chkAppHijo");
        //            //ASPxCheckBox ChkEZ = (ASPxCheckBox)e.Row.FindControl("chkEZHijo");

        //            //if (ChkJ != null)
        //            //{
        //            //    //ChkJ.ClientInstanceName = "chkSmartHijo_" + NumeroGVFerrysHijo + "_" + e.Row.RowIndex;

        //            //    ChkJ.Checked = dtFerrysHijo.Rows[e.Row.RowIndex].S("JetSmart") == "True" ? true : false;

        //            //    ChkJ.Enabled = true;
        //            //}

        //            //if (ChkA != null)
        //            //{
        //            //    //ChkA.ClientInstanceName = "chkAppHijo_" + NumeroGVFerrysHijo + "_" + e.Row.RowIndex;

        //            //    ChkA.Checked = dtFerrysHijo.Rows[e.Row.RowIndex].S("App") == "True" ? true : false;

        //            //    ChkA.Enabled = true;

        //            //}

        //            //if (ChkEZ != null)
        //            //{
        //            //    //ChkEZ.ClientInstanceName = "ChkEZHijo_" + NumeroGVFerrysHijo + "_" + e.Row.RowIndex;

        //            //    ChkEZ.Enabled = true;
        //            //}

        //            if (dtFerrysHijo.Rows[e.Row.RowIndex]["Diferencia"].S().I() <= Utils.ObtieneParametroPorClave("112").I())
        //            {
        //                string hex = sRgbP;
        //                Color _color = System.Drawing.ColorTranslator.FromHtml(hex);
        //                e.Row.BackColor = _color;


        //                //if (ChkJ != null)
        //                //{
        //                //    ChkJ.Checked = false;
        //                //    ChkJ.Enabled = false;
        //                //}


        //                //if (ChkA != null)
        //                //{
        //                //    ChkA.Checked = false;
        //                //    ChkA.Enabled = false;
        //                //}


        //                //if (ChkEZ != null)
        //                //{
        //                //    ChkEZ.Checked = false;
        //                //    ChkEZ.Enabled = false;
        //                //}

        //                //if (dde != null)
        //                //{
        //                //    dde.Enabled = false;
        //                //}

        //                //if (ddeSMS != null)
        //                //{
        //                //    ddeSMS.Enabled = false;
        //                //}
        //            }


        //            //if (dde != null)
        //            //{
        //            //    dde.ClientInstanceName = "ddeMailHijo" + e.Row.RowIndex;

        //            //    ASPxListBox lb = (ASPxListBox)dde.FindControl("listBoxHijo");

        //            //    if (lb != null)
        //            //    {

        //            //        lb.ID = "listBoxHijo";
        //            //        lb.ClientInstanceName = "chklbxHijo" + e.Row.RowIndex;


        //            //        dde.ClientSideEvents.DropDown = String.Format("function(s,e) {{SynchronizeListBoxValues(s, e, {0});}}", lb.ClientInstanceName);

        //            //        lb.ClientSideEvents.SelectedIndexChanged = String.Format("function(s,e) {{OnListBoxSelectionChanged(s, e, {0});}}", dde.ClientInstanceName);

        //            //        lb.DataSource = dtListasDifusion;
        //            //        lb.DataBind();
        //            //    }

        //            //    ASPxButton btn = (ASPxButton)dde.FindControl("btnCerrarLDMHijo");

        //            //    if (btn != null)
        //            //    {
        //            //        btn.ClientSideEvents.Click = String.Format("function(s, e){{CerrarDropDownList({0});}}", dde.ClientInstanceName);
        //            //    }
        //            //}

        //            //if (ddeSMS != null)
        //            //{
        //            //    ddeSMS.ClientInstanceName = "ddeSMSHijo" + e.Row.RowIndex;

        //            //    ASPxListBox lbSMS = (ASPxListBox)ddeSMS.FindControl("listBoxSMSHijo");

        //            //    if (lbSMS != null)
        //            //    {

        //            //        lbSMS.ID = "listBoxSMSHijo";
        //            //        lbSMS.ClientInstanceName = "cklbxSMSHijo" + e.Row.RowIndex;

        //            //        ddeSMS.ClientSideEvents.DropDown = String.Format("function(s,e) {{SynchronizeListBoxValuesSMS(s, e, {0});}}", lbSMS.ClientInstanceName);

        //            //        lbSMS.ClientSideEvents.SelectedIndexChanged = String.Format("function(s,e) {{OnListBoxSelectionChangedSMS(s, e, {0});}}", ddeSMS.ClientInstanceName);

        //            //        lbSMS.DataSource = dtListasDifusion;
        //            //        lbSMS.DataBind();

        //            //    }

        //            //    ASPxButton btn = (ASPxButton)ddeSMS.FindControl("btnCerrarLDSHijo");

        //            //    if (btn != null)
        //            //    {
        //            //        btn.ClientSideEvents.Click = String.Format("function(s, e){{CerrarDropDownList({0});}}", ddeSMS.ClientInstanceName);
        //            //    }
        //            //}

        //            if (dtFerrysHijo.Rows[e.Row.RowIndex]["Diferencia"].S().I() > Utils.ObtieneParametroPorClave("112").I() && dtFerrysHijo.Rows[e.Row.RowIndex]["Diferencia"].S().I() <= Utils.ObtieneParametroPorClave("113").I())
        //            {
        //                string hex = sRgbM;
        //                Color _color = System.Drawing.ColorTranslator.FromHtml(hex);
        //                e.Row.BackColor = _color;
        //            }
        //            if (dtFerrysHijo.Rows[e.Row.RowIndex]["Diferencia"].S().I() > Utils.ObtieneParametroPorClave("113").I())
        //            {
        //                string hex = sRgbG;
        //                Color _color = System.Drawing.ColorTranslator.FromHtml(hex);
        //                e.Row.BackColor = _color;
        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvFerrysHijo_RowDataBound", "Aviso");
        //    }
        //}
        //protected void btnAgregarFerryHijo_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        LimpiaCamposModal();
        //        ddlOrigenFV.Text = hfOrigenFV["OrigenFV_value"].ToString();
        //        ddlOrigenFV.Enabled = false;
        //        ppTramos.ShowOnPageLoad = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnAgregarFerryHijo_Click", "Aviso");
        //    }
        //}
        protected void btnCancelarFV_Click(object sender, EventArgs e)
        {
            try
            {
                LimpiaCamposModal();
                ppTramos.ShowOnPageLoad = false;
            }
            catch (Exception ex)
            {

                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnCancelarFV_Click", "Aviso");
            }
        }
        //protected void btnAceptarLista_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string Cadena = string.Empty;

        //        foreach (ListEditItem item in chklbxLista.SelectedItems)
        //        {
        //            if (item.Selected == true)
        //            {
        //                Cadena = Cadena + item.Value + "~";
        //            }
        //        }

        //        Cadena = Cadena.Substring(0, Cadena.Length - 1);

        //        string TipoFila = hfTipoListaDifusion["hfTipoListaDifusion_value"].ToString();

        //        sIdsListaDifusion = Cadena;
        //        sTipoListaDifusion = TipoFila;

        //        if (eSaveListaDifusionFerry != null)
        //            eSaveListaDifusionFerry(null, null);

        //    }
        //    catch (Exception ex)
        //    {

        //        Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnAceptarLista_Click", "Aviso");
        //    }
        //}
        //protected void btnEliminarFerry_Click(object sender, EventArgs e)
        //{

        //}
        //protected void btnEliminarFerryHijo_Click(object sender, EventArgs e)
        //{

        //}
        protected void ddlOrigenFV_CustomFiltering(object sender, ListEditCustomFilteringEventArgs e)
        {
            try
            {
                DevExpress.Web.Bootstrap.BootstrapComboBox comboBox = (DevExpress.Web.Bootstrap.BootstrapComboBox)sender;
                sFiltroO = e.Filter;

                if (eLoadOrigDestFiltro != null)
                    eLoadOrigDestFiltro(sender, e);

                CargaComboAeropuertosFV(comboBox, dtOrigen, sFiltroO);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "ddlDestinoFV_ItemsRequestedByFilterCondition", "Aviso");
            }
        }
        protected void ddlDestinoFV_CustomFiltering(object sender, ListEditCustomFilteringEventArgs e)
        {
            try
            {
                //Verificar si tiene pierna predecesora para obtener el ICAO y mandarlo como parametro.
                DevExpress.Web.Bootstrap.BootstrapComboBox comboBox = (DevExpress.Web.Bootstrap.BootstrapComboBox)sender;
                sFiltroD = e.Filter;

                if (eLoadOrigDestFiltroDest != null)
                    eLoadOrigDestFiltroDest(sender, e);

                CargaComboAeropuertosFV(comboBox, dtDestino, sFiltroD);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "ddlDestinoFV_ItemsRequestedByFilterCondition", "Aviso");
            }
        }

        #endregion

        #region METODOS
        public void LoadFerrys(DataTable dt)
        {
            dtFerrys = dt;
            //gvFerrys.DataSource = dt;
            //gvFerrys.DataBind();

            ddlMatricula.DataSource = dtMatriculas;
            ddlMatricula.ValueField = "IdAeroave";
            ddlMatricula.TextField = "Matricula";
            ddlMatricula.DataBind();
        }
        public void MostrarMensaje(string sMensaje, string sCaption)
        {
            lbl.Text = sMensaje;
            ppAlert.ShowOnPageLoad = true;
        }
        public void CreaCSV(string cadena)
        {
            sCadena = cadena;
        }
        public void enviaCorreoJetSmarter()
        {
            try
            {
                //Exportamos el CVS ...
                StringBuilder tmpCSV = new StringBuilder();

                string strPath = string.Empty;
                strPath = Server.MapPath("~\\App_Data\\UploadTemp\\OfertaFerrys.csv");

                using (StreamWriter sw = new StreamWriter(@strPath, false, System.Text.Encoding.UTF8))
                {
                    sw.Write(sCadena);
                    sw.Close();
                }

                //Cargamos el archivo en memoria ...               
                byte[] MyData = (byte[])System.IO.File.ReadAllBytes(@strPath);
                MemoryStream strmArchivo = new MemoryStream(MyData);

                //Stream sAdjunto = strmArchivo;
                string Mensaje = string.Empty;
                string Vuelo = string.Empty;
                string From = Utils.ObtieneParametroPorClave("92");
                string CC = string.Empty;

                Vuelo = Vuelo + " </table> ";
                Mensaje = Mensaje + Vuelo;

                string scorreo = Utils.ObtieneParametroPorClave("4");
                string sPass = Utils.ObtieneParametroPorClave("5");
                string sservidor = Utils.ObtieneParametroPorClave("6");
                string spuerto = Utils.ObtieneParametroPorClave("7");

                Mensaje = "<table width='800' border='0' cellspacing='0' cellpadding='10' align='center' bgcolor='#f8f8f8' style='border-radius: 20px;'>" +
                              "<tr><td><strong> This is a new empty leg for sale message </strong></td></tr>" +
                              "<tr><td>&nbsp;</td></tr>" +
                          "</table>";

                Utils.EnvioCorreo(sservidor, spuerto.S().I(), "This is a new empty leg for sale message", From, Mensaje, strmArchivo, scorreo, sPass, CC, "Oferta Ferrys: ");

                // Validamos si existe el archivo y lo eliminamos, para no cargar con muchos archivos el servidor
                if (System.IO.File.Exists(strPath))
                    System.IO.File.Delete(strPath);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargaComboAeropuertosFV(ASPxComboBox ddl, DataTable dt, string sFiltro)
        {
            try
            {
                ddl.DataSource = dt;
                ddl.TextField = "AeropuertoICAO";
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
        private void LimpiaCamposModal()
        {
            txtNoTrip.Text = string.Empty;
            ddlOrigenFV.DataSource = null;
            ddlOrigenFV.DataBind();
            ddlOrigenFV.Text = string.Empty;

            ddlDestinoFV.DataSource = null;
            ddlDestinoFV.DataBind();
            ddlDestinoFV.Text = string.Empty;

            ddlMatricula.SelectedIndex = 0;
            ddlMatricula.Text = string.Empty;

            txtFechaSalidaFV.Text = string.Empty;
            txtFechaLlegadaFV.Text = string.Empty;

            txtTiempoVuelo.Text = string.Empty;

            txtNoTrip.ReadOnly = false;
            ddlMatricula.ReadOnly = false;

        }
        //private void CargaListaDifusion(string sTipoLista)
        //{
        //    try
        //    {
        //        try
        //        {
        //            if (eSearchListaDifusion != null)
        //                eSearchListaDifusion(sTipoLista, null);

        //            chklbxLista.DataSource = dtListasDifusion;
        //            chklbxLista.DataBind();

        //        }
        //        catch (Exception ex)
        //        {

        //            throw ex;
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}
        //private void AgregaFuncionJavascriptDropDowlistMail()
        //{
        //    try
        //    {
        //        //ASPxListBox lb = (ASPxListBox)ddeMail.FindControl("cklbxMail");

        //        //lb.ClientInstanceName = "cklbxMail";

        //        //ddeMail.ClientSideEvents.DropDown = String.Format("function(s,e) {{SynchronizeListBoxValues(s, e, {0});}}", lb.ClientInstanceName);

        //        //ASPxButton btn = (ASPxButton)ddeMail.FindControl("btnCerrarLDM");

        //        //if (btn != null)
        //        //{
        //        //    btn.ClientSideEvents.Click = String.Format("function(s, e){{CerrarDropDownList({0});}}", ddeMail.ClientInstanceName);
        //        //}

        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}
        //private void AgregaFuncionJavaScriptCheckListBoxMail()
        //{
        //    try
        //    {
        //        //ASPxListBox lb = (ASPxListBox)ddeMail.FindControl("cklbxMail");

        //        //lb.ClientInstanceName = "cklbxMail";

        //        chklbxLista.ClientSideEvents.SelectedIndexChanged = String.Format("function(s,e) {{OnListBoxSelectionChanged(s, e);}}");

        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}
        //private void AgregaFuncionJavaScriptDropDownlistSMS()
        //{
        //    try
        //    {
        //        //ASPxListBox lbSMS = (ASPxListBox)ddeSMS.FindControl("cklbxSMS");

        //        //ddeSMS.ClientSideEvents.DropDown = String.Format("function(s,e) {{SynchronizeListBoxValuesSMS(s, e, {0});}}", lbSMS.ClientInstanceName);

        //        //ASPxButton btn = (ASPxButton)ddeSMS.FindControl("btnCerrarLDS");

        //        //if (btn != null)
        //        //{
        //        //    btn.ClientSideEvents.Click = String.Format("function(s, e){{CerrarDropDownList({0});}}", ddeSMS.ClientInstanceName);
        //        //}
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}
        //private void AgregarFuncionJavaScriptCheckListBoxSMS()
        //{
        //    try
        //    {
        //        //ASPxListBox lbSMS = (ASPxListBox)ddeSMS.FindControl("cklbxSMS");

        //        //lbSMS.ClientSideEvents.SelectedIndexChanged = String.Format("function(s,e) {{OnListBoxSelectionChangedSMS(s, e, {0});}}", ddeSMS.ClientInstanceName);
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}
        //private void CargaListaDifusionFerry()
        //{
        //    try
        //    {
        //        if (eSearchListaDifusionFerry != null)
        //            eSearchListaDifusionFerry(null, null);

        //        if (!sIdsListaDifusion.Equals(string.Empty))
        //        {
        //            string[] sIds = sIdsListaDifusion.Split('~');

        //            int contador = 0;

        //            for (int i = 0; i < chklbxLista.Items.Count; i++)
        //            {

        //                if (contador < sIds.Length)
        //                {
        //                    if (chklbxLista.Items[i].Value.I() == sIds[contador].I())
        //                    {
        //                        chklbxLista.Items[i].Selected = true;
        //                        contador++;
        //                    }
        //                }
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}
        private bool ValidaModal()
        {
            bool ban = true;

            if (ddlOrigenFV.Text.S() == string.Empty)
            {
                ban = false;
                ddlOrigenFV.IsValid = false;
                ddlOrigenFV.ValidationSettings.ErrorTextPosition = ErrorTextPosition.Bottom;
                ddlOrigenFV.ErrorText = "El campo es requerido.";
            }
            else
                ddlOrigenFV.IsValid = true;

            if (ddlDestinoFV.Text.S() == string.Empty)
            {
                ban = false;
                ddlDestinoFV.IsValid = false;
                ddlDestinoFV.ValidationSettings.ErrorTextPosition = ErrorTextPosition.Bottom;
                ddlDestinoFV.ErrorText = "El campo es requerido.";
            }
            else
                ddlDestinoFV.IsValid = true;

            if (txtFechaSalidaFV.Value.S() == string.Empty)
            {
                ban = false;
                txtFechaSalidaFV.IsValid = false;
                txtFechaSalidaFV.ValidationSettings.ErrorTextPosition = ErrorTextPosition.Bottom;
                txtFechaSalidaFV.ErrorText = "El campo es requerido.";
            }
            else
                txtFechaSalidaFV.IsValid = true;

            if (txtTiempoVuelo.Text.S() == "00:00" || txtTiempoVuelo.Text.S() == string.Empty)
            {
                ban = false;
                txtTiempoVuelo.IsValid = false;
                txtTiempoVuelo.ValidationSettings.ErrorTextPosition = ErrorTextPosition.Bottom;
                txtTiempoVuelo.ErrorText = "El campo es requerido.";
            }
            else
                txtTiempoVuelo.IsValid = true;

            return ban;
        }
        #endregion

        #region VARIABLES Y PROPIEDAES

        OfertaFerry_Presenter oPresenter;
        private const string sPagina = "frmOfertaFerry.aspx";
        private const string sClase = "frmOfertaFerry.aspx.cs";

        private int NumeroGVFerrysHijo = 0;

        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eSearchEnviadas;
        public event EventHandler eLoadOrigDestFiltro;
        public event EventHandler eLoadOrigDestFiltroDest;
        public event EventHandler eSavFerry;
        public event EventHandler eSavFerryPendiente;
        public event EventHandler eSearchFerryPendiente;
        public event EventHandler eSearchFerryHijo;
        public event EventHandler eSearchListaDifusionFerry;
        public event EventHandler eSaveListaDifusionFerry;
        public event EventHandler eDeleteOfertaFerryPendiente;
        public event EventHandler eSaveObjFerryHijo;
        public event EventHandler eSearchListaDifusion;

        public List<object> oListFerrys
        {
            get
            {
                //DevExpress.Web.Data.GridViewSelection oGSel = gvFerrys.Selection;
                //List<object> oLst = gvFerrys.GetSelectedFieldValues("IdFerry", "Trip", "");

                //return oLst;

                return new List<object>();
            }
        }

        //public List<OfertaFerry> oLstFerrys
        //{
        //    get
        //    {
        //        List<OfertaFerry> oLs = new List<OfertaFerry>();

        //object cellValues = gvFerrys.GetRowValues(i, new string[] {"Trip" //0
        //                                                            ,"NoPierna" //1
        //                                                            ,"Origen" //2
        //                                                            ,"FechaSalidaB" //3
        //                                                            ,"FechaSalidaA" //4
        //                                                            ,"Destino" //5
        //                                                            ,"FechaLlegadaB" //6
        //                                                            ,"FechaLlegadaA" //7
        //                                                            ,"Matricula" //8
        //                                                            ,"TiempoVuelo" //9
        //                                                            ,"GrupoModelo" //10
        //                                                            ,"LegId" //11
        //                                                            ,"IdFerry" //12
        //                                                            ,"#"
        //});


        //DevExpress.Web.Data.GridViewSelection oGSel = gvFerrys.Selection;
        //List<object> oLst = gvFerrys.GetSelectedFieldValues("IdFerry", "Trip", "");

        //CheckBox chkbx = (CheckBox)gvFerrys. row.FindControl("chkSeleccionar");

        //if (chkbx != null)
        //{
        //    if (chkbx.Checked == true)
        //    {

        //        OfertaFerry oF = new OfertaFerry();

        //        oF.iIdFerry = 0;//dtFerrys.Rows[row.RowIndex].S("IdFerry").I();
        //        oF.iTrip = dtFerrys.Rows[row.RowIndex].S("Trip").I();
        //        oF.iNoPierna = dtFerrys.Rows[row.RowIndex].S("NoPierna").I();
        //        oF.sOrigen = dtFerrys.Rows[row.RowIndex].S("Origen");
        //        oF.dtFechaSalida = dtFerrys.Rows[row.RowIndex].S("FechaSalidaB").Dt();
        //        oF.sFechaSalida = dtFerrys.Rows[row.RowIndex].S("FechaSalidaA");
        //        oF.sDestino = dtFerrys.Rows[row.RowIndex].S("Destino");
        //        oF.dtFechaLlegada = dtFerrys.Rows[row.RowIndex].S("FechaLlegadaB").Dt();
        //        oF.sFechaLlegada = dtFerrys.Rows[row.RowIndex].S("FechaLlegadaA");
        //        oF.sMatricula = dtFerrys.Rows[row.RowIndex].S("Matricula");
        //        oF.sTiempoVuelo = dtFerrys.Rows[row.RowIndex].S("TiempoVuelo");
        //        oF.sGrupoModelo = dtFerrys.Rows[row.RowIndex].S("GrupoModelo");
        //        oF.iLegId = dtFerrys.Rows[row.RowIndex].S("LegId").I();
        //        oF.iIdPendiente = dtFerrys.Rows[row.RowIndex].S("IdFerry").I();
        //        oF.sReferencia = string.Empty;

        //        oF.iIdPadre = 0;

        //        CheckBox chkSmart = (CheckBox)row.Cells[11].FindControl("chkSmart");
        //        oF.bJetSmart = chkSmart != null ? chkSmart.Checked : false;
        //        oF.iStatusJet = chkSmart.Checked ? 3 : 1;

        //        CheckBox chkApp = (CheckBox)row.Cells[12].FindControl("chkApp");
        //        oF.bApp = chkApp != null ? chkApp.Checked : false;
        //        oF.iStatusApp = chkApp.Checked ? 2 : 1;

        //        CheckBox chkEZ = (CheckBox)row.FindControl("chkEZ");
        //        oF.bEZMexJet = chkEZ != null ? chkEZ.Checked : false;
        //        oF.iStatusEZ = chkEZ.Checked ? 2 : 1;

        //        //List<ListaEnvios> oLsM = new List<ListaEnvios>();
        //        //ASPxDropDownEdit ddeM = (ASPxDropDownEdit)row.FindControl("ASPxDropDownEdit1");
        //        //if (ddeM != null)
        //        //{
        //        //    ASPxListBox lb = (ASPxListBox)ddeM.FindControl("listBox");
        //        //    if (lb != null)
        //        //    {

        //        //        var query = from DevExpress.Web.ListEditItem item in lb.Items where item.Selected select item;
        //        //        foreach (DevExpress.Web.ListEditItem item in query)
        //        //        {
        //        //            ListaEnvios oLE = new ListaEnvios();
        //        //            oLE.iIdLista = item.Value.S().I();
        //        //            oLE.sNombreLista = item.Text.S();
        //        //            oLsM.Add(oLE);
        //        //        }
        //        //    }
        //        //}

        //        //oF.oLstMail = oLsM;

        //        //List<ListaEnvios> oLsSMS = new List<ListaEnvios>();
        //        //ASPxDropDownEdit ddeSM = (ASPxDropDownEdit)row.FindControl("ddeSMS");
        //        //if (ddeSM != null)
        //        //{
        //        //    ASPxListBox lbSMS = (ASPxListBox)ddeSM.FindControl("listBoxSMS");
        //        //    if (lbSMS != null)
        //        //    {

        //        //        var query = from DevExpress.Web.ListEditItem item in lbSMS.Items where item.Selected select item;
        //        //        foreach (DevExpress.Web.ListEditItem item in query)
        //        //        {
        //        //            ListaEnvios oLE = new ListaEnvios();
        //        //            oLE.iIdLista = item.Value.S().I();
        //        //            oLE.sNombreLista = item.Text.S();
        //        //            oLsSMS.Add(oLE);
        //        //        }
        //        //    }
        //        //}

        //        //oF.oLstSMS = oLsSMS;

        //        oLs.Add(oF);

        //        iIdPadre = dtFerrys.Rows[row.RowIndex].S("IdFerry").I();

        //        gvHijo = (GridView)row.FindControl("gvFerrysHijo");

        //        if (gvHijo != null)
        //        {
        //            foreach (GridViewRow rowHijo in gvHijo.Rows)
        //            {

        //                OfertaFerry oFHijo = new OfertaFerry();

        //                oFHijo.iIdFerry = 0;//("IdFerry").I();
        //                oFHijo.iTrip = rowHijo.Cells[1].Text.I(); //dtFerrysHijo.Rows[row.RowIndex].S("Trip").I();
        //                oF.iNoPierna = rowHijo.Cells[2].Text.I(); //dtFerrysHijo.Rows[row.RowIndex].S("NoPierna").I();
        //                oFHijo.sOrigen = rowHijo.Cells[3].Text.S(); //dtFerrysHijo.Rows[row.RowIndex].S("Origen");
        //                oFHijo.dtFechaSalida = rowHijo.Cells[4].Text.Dt(); //dtFerrysHijo.Rows[row.RowIndex].S("FechaSalidaB").Dt();
        //                oFHijo.sFechaSalida = rowHijo.Cells[4].Text.S(); //dtFerrysHijo.Rows[row.RowIndex].S("FechaSalidaA");
        //                oFHijo.sDestino = rowHijo.Cells[5].Text.S(); //dtFerrysHijo.Rows[row.RowIndex].S("Destino");
        //                oFHijo.dtFechaLlegada = rowHijo.Cells[6].Text.Dt(); //dtFerrysHijo.Rows[row.RowIndex].S("FechaLlegadaB").Dt();
        //                oFHijo.sFechaLlegada = rowHijo.Cells[6].Text.S(); //dtFerrysHijo.Rows[row.RowIndex].S("FechaLlegadaA");
        //                oFHijo.sMatricula = rowHijo.Cells[8].Text.S(); //dtFerrysHijo.Rows[row.RowIndex].S("Matricula");
        //                oFHijo.sTiempoVuelo = rowHijo.Cells[7].Text.S(); //dtFerrysHijo.Rows[row.RowIndex].S("TiempoVuelo");
        //                oFHijo.sGrupoModelo = rowHijo.Cells[9].Text.S(); //dtFerrysHijo.Rows[row.RowIndex].S("GrupoModelo");
        //                oFHijo.iLegId = 0; //dtFerrysHijo.Rows[row.RowIndex].S("LegId").I();
        //                oFHijo.iIdPendiente = rowHijo.Cells[0].Text.I();
        //                oFHijo.sReferencia = string.Empty;

        //                oFHijo.iIdPadre = iIdPadre;

        //                CheckBox chkSmartHijo = (CheckBox)rowHijo.Cells[10].FindControl("chkSmartHijo");
        //                oFHijo.bJetSmart = chkSmartHijo != null ? chkSmartHijo.Checked : false;
        //                oFHijo.iStatusJet = chkSmartHijo.Checked ? 3 : 1;

        //                CheckBox chkAppHijo = (CheckBox)rowHijo.Cells[11].FindControl("chkAppHijo");
        //                oFHijo.bApp = chkAppHijo != null ? chkAppHijo.Checked : false;
        //                oFHijo.iStatusApp = chkAppHijo.Checked ? 2 : 1;

        //                CheckBox chkEZHijo = (CheckBox)rowHijo.FindControl("chkEZHijo");
        //                oFHijo.bEZMexJet = chkEZHijo != null ? chkEZHijo.Checked : false;
        //                oFHijo.iStatusEZ = chkEZHijo.Checked ? 2 : 1;

        //                oLs.Add(oFHijo);
        //            }
        //        }

        //    }
        //}
        //}

        //        return oLs;
        //    }
        //}
        public List<OfertaFerry> oLstFerrysHijo
        {
            get
            {
                List<OfertaFerry> oLs = new List<OfertaFerry>();

                foreach (GridViewRow row in gvHijo.Rows)
                {

                    OfertaFerry oF = new OfertaFerry();

                    oF.iIdFerry = 0;//("IdFerry").I();
                    oF.iTrip = row.Cells[1].Text.I(); //dtFerrysHijo.Rows[row.RowIndex].S("Trip").I();
                    oF.iNoPierna = row.Cells[2].Text.I(); //dtFerrysHijo.Rows[row.RowIndex].S("NoPierna").I();
                    oF.sOrigen = row.Cells[3].Text.S(); //dtFerrysHijo.Rows[row.RowIndex].S("Origen");
                    oF.dtFechaSalida = row.Cells[4].Text.Dt(); //dtFerrysHijo.Rows[row.RowIndex].S("FechaSalidaB").Dt();
                    oF.sFechaSalida = row.Cells[4].Text.S(); //dtFerrysHijo.Rows[row.RowIndex].S("FechaSalidaA");
                    oF.sDestino = row.Cells[5].Text.S(); //dtFerrysHijo.Rows[row.RowIndex].S("Destino");
                    oF.dtFechaLlegada = row.Cells[6].Text.Dt(); //dtFerrysHijo.Rows[row.RowIndex].S("FechaLlegadaB").Dt();
                    oF.sFechaLlegada = row.Cells[6].Text.S(); //dtFerrysHijo.Rows[row.RowIndex].S("FechaLlegadaA");
                    oF.sMatricula = row.Cells[8].Text.S(); //dtFerrysHijo.Rows[row.RowIndex].S("Matricula");
                    oF.sTiempoVuelo = row.Cells[7].Text.S(); //dtFerrysHijo.Rows[row.RowIndex].S("TiempoVuelo");
                    oF.sGrupoModelo = row.Cells[9].Text.S(); //dtFerrysHijo.Rows[row.RowIndex].S("GrupoModelo");
                    oF.iLegId = 0; //dtFerrysHijo.Rows[row.RowIndex].S("LegId").I();
                    //oF.iIdPendiente =                                                                                                                                                                                                                                                                                                                                                                                                                           
                    oF.sReferencia = string.Empty;

                    oF.iIdPadre = iIdPadre;

                    CheckBox chkSmart = (CheckBox)row.Cells[10].FindControl("chkSmartHijo");
                    oF.bJetSmart = chkSmart != null ? chkSmart.Checked : false;
                    oF.iStatusJet = chkSmart.Checked ? 3 : 1;

                    CheckBox chkApp = (CheckBox)row.Cells[11].FindControl("chkAppHijo");
                    oF.bApp = chkApp != null ? chkApp.Checked : false;
                    oF.iStatusApp = chkApp.Checked ? 2 : 1;

                    CheckBox chkEZ = (CheckBox)row.FindControl("chkEZHijo");
                    oF.bEZMexJet = chkEZ != null ? chkEZ.Checked : false;
                    oF.iStatusEZ = chkEZ.Checked ? 2 : 1;

                    List<ListaEnvios> oLsM = new List<ListaEnvios>();
                    ASPxDropDownEdit ddeM = (ASPxDropDownEdit)row.FindControl("ASPxDropDownEdit1");
                    if (ddeM != null)
                    {
                        ASPxListBox lb = (ASPxListBox)ddeM.FindControl("listBox");
                        if (lb != null)
                        {

                            var query = from DevExpress.Web.ListEditItem item in lb.Items where item.Selected select item;
                            foreach (DevExpress.Web.ListEditItem item in query)
                            {
                                ListaEnvios oLE = new ListaEnvios();
                                oLE.iIdLista = item.Value.S().I();
                                oLE.sNombreLista = item.Text.S();
                                oLsM.Add(oLE);
                            }

                        }
                    }
                    oF.oLstMail = oLsM;


                    List<ListaEnvios> oLsSMS = new List<ListaEnvios>();
                    ASPxDropDownEdit ddeSM = (ASPxDropDownEdit)row.FindControl("ddeSMS");
                    if (ddeSM != null)
                    {
                        ASPxListBox lbSMS = (ASPxListBox)ddeSM.FindControl("listBoxSMS");
                        if (lbSMS != null)
                        {

                            var query = from DevExpress.Web.ListEditItem item in lbSMS.Items where item.Selected select item;
                            foreach (DevExpress.Web.ListEditItem item in query)
                            {
                                ListaEnvios oLE = new ListaEnvios();
                                oLE.iIdLista = item.Value.S().I();
                                oLE.sNombreLista = item.Text.S();
                                oLsSMS.Add(oLE);
                            }

                        }
                    }
                    oF.oLstSMS = oLsSMS;


                    oLs.Add(oF);
                }

                return oLs;
            }
        }
        public List<OfertaFerry> oLstFerrysPendiente
        {
            get
            {
                List<OfertaFerry> oLs = new List<OfertaFerry>();

                //foreach (DataRow row in dtFerrys.Rows)
                //{
                OfertaFerry oF = new OfertaFerry();

                DateTime FechaSalida = txtFechaSalidaFV.Value.Dt();
                sTime = txtTiempoVuelo.Text;
                FechaLlegada = FechaSalida.AddHours(sTime.Substring(0, 2).S().I());
                FechaLlegada = FechaLlegada.AddMinutes(sTime.Substring(3, 2).S().I());

                oF.iIdFerry = 0;
                oF.iTrip = txtNoTrip.Text.S().I();
                oF.iNoPierna = 1;
                oF.sOrigen = ddlOrigenFV.Text.S();
                oF.dtFechaSalida = txtFechaSalidaFV.Value.S().Dt();
                oF.dFechaSalidaB = txtFechaSalidaFV.Value.S().Dt();
                oF.sFechaSalidaA = txtFechaSalidaFV.Value.S().Dt().ToString("YYYY/MM/DD");
                oF.sDestino = ddlDestinoFV.Text.S();
                oF.dtFechaLlegada = FechaLlegada;
                oF.dFechaLlegadaB = FechaLlegada;
                oF.sFechaLlegadaA = FechaLlegada.ToString("YYYY/MM/DD");

                TimeSpan ts = new TimeSpan();
                ts = FechaLlegada - txtFechaSalidaFV.Value.S().Dt();

                TimeSpan ts2 = new TimeSpan();
                ts2 = txtFechaLlegadaFV.Value.S().Dt() - DateTime.Now;

                oF.iDiferencia = Math.Truncate(decimal.Parse(ts2.TotalHours.ToString())).I();
                oF.sMatricula = ddlMatricula.Text.S();
                oF.sTiempoVuelo = ts.ToString().Substring(0, 5);
                oF.sGrupoModelo = dtMatriculas.Rows[ddlMatricula.SelectedIndex]["DescGrupoModelo"].S();
                oF.iLegId = 0;
                oF.sReferencia = string.Empty;

                //iIdPadre = hfIdFerry["IdFerry_value"].ToString().I();



                //if (iIdPadre.Equals(0))
                //{
                //    oF.iIdPadre = 0;
                //}
                //else
                //{
                //    oF.iIdPadre = iIdPadre;

                //    iIdPadre = 0;
                //}


                if (eTipoFerry == TipoFerry.EsPadre)
                    oF.iIdPadre = 0;
                else if(eTipoFerry == TipoFerry.EsHijo)
                    oF.iIdPadre = iIdPadre;


                //ASPxCheckBox chkSmart = (ASPxCheckBox)row.Cells[8].FindControl("chkSmart");
                oF.bJetSmart = false;//chkSmart != null ? chkSmart.Checked : false;
                oF.iStatusJet = 0;// chkSmart.Checked ? 3 : 1;

                //ASPxCheckBox chkApp = (ASPxCheckBox)row.Cells[9].FindControl("chkApp");
                oF.bApp = false;// chkApp != null ? chkApp.Checked : false;
                oF.iStatusApp = 0;// chkApp.Checked ? 2 : 1;

                //ASPxCheckBox chkEZ = (ASPxCheckBox)row.FindControl("chkEZ");
                oF.bEZMexJet = false;// chkEZ != null ? chkEZ.Checked : false;
                oF.iStatusEZ = 0;// chkEZ.Checked ? 2 : 1;

                List<ListaEnvios> oLsM = new List<ListaEnvios>();

                //ASPxDropDownEdit ddeM = (ASPxDropDownEdit)row.FindControl("ASPxDropDownEdit1");
                //if (ddeM != null)
                //{
                //    ASPxListBox lb = (ASPxListBox)ddeM.FindControl("listBox");
                //    if (lb != null)
                //    {

                //        var query = from DevExpress.Web.ListEditItem item in lb.Items where item.Selected select item;
                //        foreach (DevExpress.Web.ListEditItem item in query)
                //        {
                //            ListaEnvios oLE = new ListaEnvios();
                //            oLE.iIdLista = item.Value.S().I();
                //            oLE.sNombreLista = item.Text.S();
                //            oLsM.Add(oLE);
                //        }

                //    }
                //}
                oF.oLstMail = oLsM;


                List<ListaEnvios> oLsSMS = new List<ListaEnvios>();
                //ASPxDropDownEdit ddeSM = (ASPxDropDownEdit)row.FindControl("ddeSMS");
                //if (ddeSM != null)
                //{
                //    ASPxListBox lbSMS = (ASPxListBox)ddeSM.FindControl("listBoxSMS");
                //    if (lbSMS != null)
                //    {

                //        var query = from DevExpress.Web.ListEditItem item in lbSMS.Items where item.Selected select item;
                //        foreach (DevExpress.Web.ListEditItem item in query)
                //        {
                //            ListaEnvios oLE = new ListaEnvios();
                //            oLE.iIdLista = item.Value.S().I();
                //            oLE.sNombreLista = item.Text.S();
                //            oLsSMS.Add(oLE);
                //        }

                //    }
                //}
                oF.oLstSMS = oLsSMS;


                oLs.Add(oF);
                //}

                return oLs;
            }
        }
        public DataTable dtFerrys
        {
            get { return (DataTable)ViewState["VSOfertaFerry"]; }
            set { ViewState["VSOfertaFerry"] = value; }
        }
        public string sTime = string.Empty;
        public DateTime FechaLlegada;
        public int iIdPadre
        {
            get { return ViewState["VSiIdPadre"].I(); }
            set { ViewState["VSiIdPadre"] = value; }
        }
        public int iIdPendiente
        {
            get { return ViewState["VSiIdPendiente"].I(); }
            set { ViewState["VSiIdPendiente"] = value; }
        }
        public string sIdsListaDifusion
        {
            get { return ViewState["VSsIdsListaDifusion"].S(); }
            set { ViewState["VSsIdsListaDifusion"] = value; }
        }
        public string sTipoListaDifusion
        {
            get { return ViewState["VSsTipoListaDifusion"].S(); }
            set { ViewState["VSsTipoListaDifusion"] = value; }
        }
        public string sCadena
        {
            get { return ViewState["VSCadena"].S(); }
            set { ViewState["VSCadena"] = value; }
        }
        public string sRgbG
        {
            get { return ViewState["VSRGBG"].S(); }
            set { ViewState["VSRGBG"] = value; }
        }
        public string sRgbM
        {
            get { return ViewState["VSRGBM"].S(); }
            set { ViewState["VSRGBM"] = value; }
        }
        public string sRgbP
        {
            get { return ViewState["VSRGBP"].S(); }
            set { ViewState["VSRGBP"] = value; }
        }
        public DataTable dtListasDifusion
        {
            get { return (DataTable)ViewState["VSdtListasDifusion"]; }
            set { ViewState["VSdtListasDifusion"] = value; }
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
        public DataTable dtMatriculas
        {
            get { return (DataTable)ViewState["VSMatriculas"]; }
            set { ViewState["VSMatriculas"] = value; }
        }
        public int iIdGrupoModelo
        {
            get { return ViewState["VSGrupoModelo"].S().I(); }
            set { ViewState["VSGrupoModelo"] = value; }
        }
        public DataTable dtFerrysHijo
        {
            get { return (DataTable)ViewState["VSFerrysHijo"]; }
            set { ViewState["VSFerrysHijo"] = value; }
        }
        public TipoFerry eTipoFerry
        {
            set { ViewState["VSETipoFerry"] = value; }
            get { return (TipoFerry)ViewState["VSETipoFerry"]; }
        }
        GridView gvHijo;

        public enum TipoFerry : int
        {
            EsPadre = 1,
            EsHijo = 2
        }

        #endregion

        protected void imbAddFerryHijo_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (ValidaModal())
                {
                    DateTime FechaSalida = txtFechaSalidaFV.Value.Dt();

                    sTime = txtTiempoVuelo.Text;

                    FechaLlegada = FechaSalida.AddHours(sTime.Substring(0, 2).S().I());
                    FechaLlegada = FechaLlegada.AddMinutes(sTime.Substring(3, 2).S().I());

                    if (FechaSalida < FechaLlegada)
                    {
                        if (eSavFerryPendiente != null)
                            eSavFerryPendiente(sender, e);

                        LimpiaCamposModal();

                        ppTramos.ShowOnPageLoad = false;
                    }
                }
                else
                    ppTramos.ShowOnPageLoad = true;
            }
            catch (Exception ex)
            {

                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnAgregarFV_Click", "Aviso");
            }
        }
    }
}