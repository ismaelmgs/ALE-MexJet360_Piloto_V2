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
        protected void Page_Load(object sender, EventArgs e)
        {
            oPresenter = new ConsultaBitacoras_Presenter(this, new DBConsultaBitacoras());

            if (!IsPostBack)
            {
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
                    string[] fieldValues = { "leg_id", "folio", "flight_off", "flight_on", "flight_diff", "calzo_in", "calzo_out", "calzo_diff", "fuel_in", "fuel_out", "fuel_diff", "estatus" };
                    object obj = gvBitacoras.GetRowValues(index, fieldValues);
                    object[] oB = (object[])obj;
                    //List<object> olst = oB;

                    if (oB.Length > 0)
                    {
                        txtLegId.Text = oB[0].S();
                        txtFolio.Text = oB[1].S();
                        txtFlightOff.Text = oB[2].S();
                        txtFlightOn.Text = oB[3].S();
                        txtFlightDiff.Text = oB[4].S();
                        txtCalzoIn.Text = oB[5].S();
                        txtCalzoOut.Text = oB[6].S();
                        txtCalzoDiff.Text = oB[7].S();
                        txtFuelIn.Text = oB[8].S();
                        txtFuelOut.Text = oB[9].S();
                        txtFuelDiff.Text = oB[10].S();
                        hdnIdBitacora.Value = iIdBitacora.S();
                        pnlBusqueda.Visible = false;
                        pnlActualizaBitacora.Visible = true;
                        pnlBitacoras.Visible = false;

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
                oBita.LLegId = txtLegId.Text.I();
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

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (eSaveObj != null)
                    eSaveObj(sender, e);

                if (bRes == true)
                {
                    pnlBusqueda.Visible = true;
                    pnlBitacoras.Visible = true;
                    pnlActualizaBitacora.Visible = false;

                    if (eSearchObj != null)
                        eSearchObj(sender, e);

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
                    pnlBusqueda.Visible = true;
                    pnlBitacoras.Visible = true;
                    pnlActualizaBitacora.Visible = false;

                    if (eSearchObj != null)
                        eSearchObj(sender, e);

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
                if (eSearchPhoto != null)
                    eSearchPhoto(sender, e);

                ppVerImagen.ShowOnPageLoad = true;
            }
            catch (Exception)
            {
                throw;
            }
            
        }
    }
}