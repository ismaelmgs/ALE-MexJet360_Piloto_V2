using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NucleoBase.Core;
using DevExpress.Web.Bootstrap;
using DevExpress.Web.Data;
using ALE_MexJet.Interfaces;
using ALE_MexJet.DomainModel;
using ALE_MexJet.Presenter;
using ALE_MexJet.Objetos;
using System.IO;

namespace ALE_MexJet.Views.Operaciones
{
    public partial class frmConsultaBitacoras : System.Web.UI.Page, IViewConsultaBitacoras
    {
        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {

            oPresenter = new ConsultaBitacoras_Presenter(this, new DBConsultaBitacoras());
            gvBitacoras.SettingsPager.Position = PagerPosition.TopAndBottom;
            gvBitacoras.SettingsPager.ShowDisabledButtons = true;
            gvBitacoras.SettingsPager.ShowNumericButtons = true;
            gvBitacoras.SettingsPager.ShowSeparators = true;
            gvBitacoras.SettingsPager.Summary.Visible = true;
            gvBitacoras.SettingsPager.PageSizeItemSettings.Visible = true;
            gvBitacoras.SettingsPager.PageSizeItemSettings.Position = PagerPageSizePosition.Right;
            gvBitacoras.SettingsText.SearchPanelEditorNullText = "Ingresa la información a buscar:";

            if (!IsPostBack)
            {
                bViewDetail = false;
                sParametro = ddlBusqueda.SelectedItem.Value.S();
                if (eSearchObj != null)
                    eSearchObj(sender, e);
            }
        }


        protected void gvBitacoras_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int pageIndex = gvBitacoras.PageIndex;
                gvBitacoras.PageIndex = pageIndex;
                gvBitacoras.DataSource = dtBitacoras;
                gvBitacoras.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gvBitacoras_RowCommand(object sender, DevExpress.Web.ASPxGridViewRowCommandEventArgs e)
        {
            try
            {
                if (e.CommandArgs.CommandName.S() == "Actualiza")
                {
                    int index = e.VisibleIndex.I();
                    int iIdBitacora = gvBitacoras.GetRowValues(index, "idBitacora").S().I();
                    string[] fieldValues = { "leg_id", "folio", "flight_off", "flight_on", "flight_diff", "calzo_in", "calzo_out", "calzo_diff", "fuel_in", "fuel_out", "fuel_diff", "estatus", "trip", "matricula", "Foto" };
                    object obj = gvBitacoras.GetRowValues(index, fieldValues);
                    object[] oB = (object[])obj;
                    //List<object> olst = oB;

                    if (oB.Length > 0)
                    {
                        hdnLegId.Value = oB[0].S();
                        txtFolio.Text = oB[1].S();

                        if (oB[2].S().Length >= 16)
                            txtFlightOff.Text = oB[2].S().Substring(0, 16);
                        else
                            txtFlightOff.Text = oB[2].S();

                        if (oB[3].S().Length >= 16)
                            txtFlightOn.Text = oB[3].S().Substring(0, 16);
                        else
                            txtFlightOn.Text = oB[3].S();

                        txtFlightDiff.Text = oB[4].S();

                        if (oB[5].S().Length >= 16)
                            txtCalzoIn.Text = oB[5].S().Substring(0, 16);
                        else
                            txtCalzoIn.Text = oB[5].S();

                        if (oB[6].S().Length >= 16)
                            txtCalzoOut.Text = oB[6].S().Substring(0, 16);
                        else
                            txtCalzoOut.Text = oB[6].S();

                        txtCalzoDiff.Text = oB[7].S();
                        txtFuelIn.Text = oB[8].S();
                        txtFuelOut.Text = oB[9].S();
                        txtFuelDiff.Text = oB[10].S();
                        txtTrip.Text = oB[12].S();
                        txtMatricula.Text = oB[13].S();
                        hdnIdBitacora.Value = iIdBitacora.S();
                        hdnFoto.Value = oB[14].S();
                        pnlBusqueda.Visible = false;
                        pnlActualizaBitacora.Visible = true;
                        pnlBitacoras.Visible = false;
                        bViewDetail = true;

                        if (oB[14].S() == "1")
                            btnVerImagen.Enabled = true;
                        else if (oB[14].S() == "0")
                            btnVerImagen.Enabled = false;

                        if (oB[11].I() == 1)
                            btnAutorizar.Enabled = true;
                        else if (oB[11].I() == 2)
                        {
                            txtFlightOff.Enabled = false;
                            txtFlightOn.Enabled = false;
                            txtFlightDiff.Enabled = false;
                            txtCalzoIn.Enabled = false;
                            txtCalzoOut.Enabled = false;
                            txtCalzoDiff.Enabled = false;
                            txtFuelIn.Enabled = false;
                            txtFuelOut.Enabled = false;
                            txtFuelDiff.Enabled = false;
                            btnActualizar.Enabled = false;
                            btnAutorizar.Enabled = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (eSaveObj != null)
                    eSaveObj(sender, e);

                if (bRes == true)
                {
                    pnlBusqueda.Visible = false;
                    pnlBitacoras.Visible = false;
                    pnlActualizaBitacora.Visible = true;

                    MostrarMensaje("Se actualizó la información correctamente", "¡Listo!");
                }
                else
                    MostrarMensaje("No se pudo actualizar, favor de verificar", "¡Atención!");
            }
            catch (Exception ex)
            {
                MostrarMensaje("Ocurrió un error :" + ex.Message, "¡Error!");
            }
        }

        protected void btnAutorizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (eUpdateSts != null)
                    eUpdateSts(sender, e);

                if (bRes == true)
                {
                    pnlBusqueda.Visible = false;
                    pnlBitacoras.Visible = false;
                    pnlActualizaBitacora.Visible = true;
                    MostrarMensaje("Se autorizó la información correctamente", "¡Listo!");
                }
                else
                    MostrarMensaje("No se pudo autorizar la información, favor de verificar", "¡Atención!");
            }
            catch (Exception ex)
            {
                MostrarMensaje("Ocurrió un error :" + ex.Message, "¡Error!");
            }
        }

        protected void btnVerImagen_Click(object sender, EventArgs e)
        {
            try
            {
                //if (eSearchPhoto != null)
                //    eSearchPhoto(sender, e);

                //ppVerImagen.ShowOnPageLoad = true;
                int iBitacora = hdnIdBitacora.Value.I();
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('../Operaciones/frmViewBitacora.aspx?Bitacora=" + iBitacora + "',this.target, 'width=700,height=500,top=120,left=400,toolbar=no,location=no,status=no,menubar=no');", true);
            }
            catch (Exception)
            {
                throw;
            }

        }
        #endregion

        #region MÉTODOS
        public void MostrarMensaje(string sMensaje, string sCaption)
        {
            //mpeMensaje.ShowMessage(sMensaje, sCaption);
            lbl.Text = sMensaje;
            ppAlert.ShowOnPageLoad = true;
        }
        public void LoadBitacoras(DataTable dt)
        {
            try
            {
                dtBitacoras = null;
                dtBitacoras = dt;

                if (dt != null && dt.Rows.Count > 0)
                {
                    gvBitacoras.DataSource = dt;
                    gvBitacoras.DataBind();
                    pnlBitacoras.Visible = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LoadFoto(DataTable dt)
        {
            try
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    byte[] imgData = (byte[])dt.Rows[0]["Foto"];
                    ppVerImagen.HeaderText = dt.Rows[0]["NombreArchivo"].S();
                    using (MemoryStream ms = new MemoryStream(imgData))
                    {
                        //System.Drawing.Image image = System.Drawing.Image.FromStream(ms);
                        imgFoto.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray(), 0, ms.ToArray().Length);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region PROPIEDADES Y VARIABLES
        ConsultaBitacoras_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eUpdateSts;
        public event EventHandler eSearchPhoto;
        public bool bRes
        {
            get { return (bool)ViewState["VSRes"]; }
            set { ViewState["VSRes"] = value; }
        }
        public bool bViewDetail
        {
            get { return (bool)ViewState["VSViewDetail"]; }
            set { ViewState["VSViewDetail"] = value; }
        }
        public string sParametro
        {
            get { return (string)ViewState["VSParametro"]; }
            set { ViewState["VSParametro"] = value; }
        }
        public DataTable dtBitacoras
        {
            get { return (DataTable)ViewState["VSdtBitacoras"]; }
            set { ViewState["VSdtBitacoras"] = value; }
        }
        public BitacoraVuelo oBi
        {
            get
            {
                BitacoraVuelo oBita = new BitacoraVuelo();
                oBita.IIdBitacora = hdnIdBitacora.Value.I();
                oBita.LLegId = hdnLegId.Value.I();
                oBita.SFolio = txtFolio.Text;
                oBita.SFlightOff = txtFlightOff.Text;
                oBita.SFlightOn = txtFlightOn.Text;
                oBita.SFlightDiff = txtFlightDiff.Text;
                oBita.SCalzoIn = txtCalzoIn.Text;
                oBita.SCalzoOut = txtCalzoOut.Text;
                oBita.SCalzoDiff = txtCalzoDiff.Text;
                oBita.SFuelIn = txtFuelIn.Text;
                oBita.SFuelOut = txtFuelOut.Text;
                oBita.SFuelDiff = txtFuelDiff.Text;
                oBita.SUser = ((UserIdentity)Session["UserIdentity"]).sUsuario;
                return oBita;
            }
        }

        #endregion

        protected void txtFuelOut_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double dFuelIn = txtFuelIn.Text.Db();
                double dFuelOut = txtFuelOut.Text.Db();
                double dRes = 0;

                if (dFuelOut > dFuelIn)
                {
                    MostrarMensaje("La cantidad de combustible no concuerdan para esta operación, favor de revisar.", "");
                }
                else 
                {
                    dRes = dFuelIn - dFuelOut;
                    txtFuelDiff.Text = dRes.S();
                    upaOperaciones.Update();
                } 
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        

        

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                sParametro = ddlBusqueda.SelectedItem.Value.S();

                if (eSearchObj != null)
                    eSearchObj(sender, e);
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error: " + ex.Message, "Aviso");
            }
        }

        protected void gvBitacoras_Load(object sender, EventArgs e)
        {
            if (bViewDetail == false)
            {
                sParametro = ddlBusqueda.SelectedItem.Value.S();
                if (eSearchObj != null)
                    eSearchObj(sender, e);
            }
        }

        protected void txtFlightOff_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string[] sArrFliOff = txtFlightOff.Text.Split(' ');
                string[] sArrFliOn = txtFlightOn.Text.Split(' ');
                if (sArrFliOff.Length > 1 && sArrFliOn.Length > 1)
                {
                    string sTimeOff = sArrFliOff[1].S();
                    string sTimeOn = sArrFliOn[1].S();
                    TimeSpan timeOff = TimeSpan.Parse(sTimeOff);
                    TimeSpan timeOn = TimeSpan.Parse(sTimeOn);

                    if (timeOff > timeOn)
                    {
                        divFlightOff.Attributes.CssStyle.Add("border", "1px solid #ff0000");
                        divFlightOff.Attributes.CssStyle.Add("border-radius", "4px");
                        btnActualizar.Enabled = false;
                        btnAutorizar.Enabled = false;
                        MostrarMensaje("Los tiempos no concuerdan para esta operación, favor de revisar tiempos de vuelo.", "");
                    }
                    else
                    {
                        TimeSpan timeRes = timeOn - timeOff;
                        txtFlightDiff.Text = timeRes.S();
                        divFlightOff.Attributes.CssStyle.Add("border", "none");
                        btnActualizar.Enabled = true;
                        btnAutorizar.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        protected void txtFlightOn_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string[] sArrFliOff = txtFlightOff.Text.Split(' ');
                string[] sArrFliOn = txtFlightOn.Text.Split(' ');
                if (sArrFliOff.Length > 1 && sArrFliOn.Length > 1)
                {
                    string sTimeOff = sArrFliOff[1].S();
                    string sTimeOn = sArrFliOn[1].S();
                    TimeSpan timeOff = TimeSpan.Parse(sTimeOff);
                    TimeSpan timeOn = TimeSpan.Parse(sTimeOn);

                    if (timeOn < timeOff)
                    {
                        divFlightOn.Attributes.CssStyle.Add("border", "1px solid #ff0000");
                        divFlightOn.Attributes.CssStyle.Add("border-radius", "4px");
                        btnActualizar.Enabled = false;
                        btnAutorizar.Enabled = false;
                        MostrarMensaje("Los tiempos no concuerdan para esta operación, favor de revisar tiempos de vuelo.", "");
                    }
                    else
                    {
                        TimeSpan timeRes = timeOn - timeOff;
                        txtFlightDiff.Text = timeRes.S();
                        divFlightOn.Attributes.CssStyle.Add("border", "none");
                        btnActualizar.Enabled = true;
                        btnAutorizar.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void txtCalzoOut_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string[] sArrClzOut = txtCalzoOut.Text.Split(' ');
                string[] sArrClzIn = txtCalzoIn.Text.Split(' ');
                if (sArrClzOut.Length > 1 && sArrClzIn.Length > 1)
                {
                    string sTimeOut = sArrClzOut[1].S();
                    string sTimeIn = sArrClzIn[1].S();
                    TimeSpan timeOut = TimeSpan.Parse(sTimeOut);
                    TimeSpan timeIn = TimeSpan.Parse(sTimeIn);

                    if (timeIn > timeOut)
                    {
                        divCalzoOut.Attributes.CssStyle.Add("border", "1px solid #ff0000");
                        divCalzoOut.Attributes.CssStyle.Add("border-radius", "4px");
                        btnActualizar.Enabled = false;
                        btnAutorizar.Enabled = false;
                        MostrarMensaje("Los tiempos no concuerdan para esta operación, favor de revisar tiempos Calzo.", "");
                    }
                    else
                    {
                        TimeSpan timeRes = timeOut - timeIn;
                        txtCalzoDiff.Text = timeRes.S();
                        divCalzoOut.Attributes.CssStyle.Add("border", "none");
                        btnActualizar.Enabled = true;
                        btnAutorizar.Enabled = true;
                    }

                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void txtCalzoIn_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string[] sArrClzOut = txtCalzoOut.Text.Split(' ');
                string[] sArrClzIn = txtCalzoIn.Text.Split(' ');
                if (sArrClzOut.Length > 1 && sArrClzIn.Length > 1)
                {
                    string sTimeOut = sArrClzOut[1].S();
                    string sTimeIn = sArrClzIn[1].S();
                    TimeSpan timeOut = TimeSpan.Parse(sTimeOut);
                    TimeSpan timeIn = TimeSpan.Parse(sTimeIn);

                    if (timeOut < timeIn)
                    {
                        divCalzoIn.Attributes.CssStyle.Add("border", "1px solid #ff0000");
                        divCalzoIn.Attributes.CssStyle.Add("border-radius", "4px");
                        btnActualizar.Enabled = false;
                        btnAutorizar.Enabled = false;
                        MostrarMensaje("Los tiempos no concuerdan para esta operación, favor de revisar tiempos Calzo.", "");
                    }
                    else
                    {
                        TimeSpan timeRes = timeOut - timeIn;
                        txtCalzoDiff.Text = timeRes.S();
                        divCalzoIn.Attributes.CssStyle.Add("border", "none");
                        btnActualizar.Enabled = true;
                        btnAutorizar.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}