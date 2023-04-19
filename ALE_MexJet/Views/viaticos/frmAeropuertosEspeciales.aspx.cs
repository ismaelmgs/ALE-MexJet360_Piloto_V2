using ALE_MexJet.DomainModel;
using ALE_MexJet.Interfaces;
using ALE_MexJet.Objetos;
using ALE_MexJet.Presenter;
using DevExpress.Web;
using DevExpress.Web.Bootstrap;
using DevExpress.XtraPrinting.Export.Web;
using NucleoBase.Core;
using Org.BouncyCastle.Utilities.Encoders;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace ALE_MexJet.Views.viaticos
{
    public partial class frmAeropuertos : System.Web.UI.Page, IViewAeropuertosEspeciales
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            oPresenter = new AeropuertosEspeciales_Presenter(this, new DBAeropuertosEspeciales());
            //gvAeropuertos.SettingsPager.Position = PagerPosition.TopAndBottom;
            //gvAeropuertos.SettingsPager.ShowDisabledButtons = true;
            //gvAeropuertos.SettingsPager.ShowNumericButtons = true;
            //gvAeropuertos.SettingsPager.ShowSeparators = true;
            //gvAeropuertos.SettingsPager.Summary.Visible = true;
            //gvAeropuertos.SettingsPager.PageSize = 10;
            //gvAeropuertos.SettingsPager.PageSizeItemSettings.Visible = true;
            //gvAeropuertos.SettingsPager.PageSizeItemSettings.Position = PagerPageSizePosition.Right;
            gvAeropuertos.SettingsText.SearchPanelEditorNullText = "Ingresa la información a buscar:";
            //gvAeropuertos.Settings.ShowGroupPanel = true;
            gvAeropuertos.Settings.ShowTitlePanel = true;
            gvAeropuertos.SettingsText.Title = "Aeropuertos Especiales";

            if (!IsPostBack) 
            {
                if (eSearchAeropuertos != null)
                    eSearchAeropuertos(sender, e);

                if (eSearchAeropuertosEspeciales != null)
                    eSearchAeropuertosEspeciales(sender, e);
            }
        }

        protected void btnAgregarAeropuerto_Click(object sender, EventArgs e)
        {
            try
            {
                ddlAeropuerto.Value = "";
                ppEspeciales.ShowOnPageLoad = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        

        protected void btnGuardarEspecial_Click(object sender, EventArgs e)
        {
            try
            {
                Page.Validate("gpEspecial");
                if (Page.IsValid)
                {
                    sPOA = ddlAeropuerto.Value.S();

                    if (eNewObj != null)
                        eNewObj(sender, e);

                    if (iOK != 0)
                    {
                        if (eSearchAeropuertosEspeciales != null)
                            eSearchAeropuertosEspeciales(sender, e);

                        ppEspeciales.ShowOnPageLoad = false;
                        MostrarMensaje("Se ha registrado el aeropuerto especial", "Listo");
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error: " + ex.Message, "Error");
            }
        }

        protected void gvAeropuertos_RowCommand(object sender, DevExpress.Web.ASPxGridViewRowCommandEventArgs e)
        {
            try
            {
                if (e.CommandArgs.CommandName.S() == "Eliminar")
                {
                    int index = e.VisibleIndex.I();
                    int IdEspecial = gvAeropuertos.GetRowValues(index, "IdEspecial").S().I();
                    iIdEspecial = IdEspecial;

                    ppAlertConfirm.ShowOnPageLoad = true;
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error: " + ex.Message, "Error");
            }
        }
        protected void gvAeropuertos_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int pageIndex = gvAeropuertos.PageIndex;
                gvAeropuertos.PageIndex = pageIndex;
                gvAeropuertos.DataSource = dtAeroEspecial;
                gvAeropuertos.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        protected void gvAeropuertos_BeforePerformDataSelect(object sender, EventArgs e)
        {
            try
            {
                if (dtAeroEspecial != null && dtAeroEspecial.Rows.Count > 0)
                {
                    (sender as ASPxGridView).DataSource = dtAeroEspecial;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void gvAeropuertos_Load(object sender, EventArgs e)
        {
            //if (dtAeroEspecial == null)
            //    return;
            //(sender as ASPxGridView).DataBind();
        }
        protected void btnAccept_Click(object sender, EventArgs e)
        {
            try
            {
                if (iIdEspecial != 0)
                {
                    if (eDeleteObj != null)
                        eDeleteObj(sender, e);

                    if (iOK == 1)
                    {
                        if (eSearchAeropuertosEspeciales != null)
                            eSearchAeropuertosEspeciales(sender, e);

                        ppAlertConfirm.ShowOnPageLoad = false;

                        MostrarMensaje("Se ha eliminado el aeropuerto", "Listo");
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error: " + ex.Message, "Error");
            }
        }

        public void MostrarMensaje(string sMensaje, string sCaption)
        {
            lbl.Text = sMensaje;
            ppAlert.ShowOnPageLoad = true;
        }
        public void LoadAeropuertos(DataTable dt)
        {
            try
            {
                ddlAeropuerto.DataSource = dt;
                ddlAeropuerto.ValueField = "AeropuertoICAO";
                ddlAeropuerto.TextField = "DesAeropuerto";
                ddlAeropuerto.DataBind();
                ddlAeropuerto.Items.Insert(0, new BootstrapListEditItem("-- Seleccione --", ""));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LoadAeropuertosEspeciales(DataTable dt)
        {
            try
            {
                dtAeroEspecial = null;
                dtAeroEspecial = dt;
                gvAeropuertos.DataSource = dt;
                gvAeropuertos.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        AeropuertosEspeciales_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eSearchAeropuertos;
        public event EventHandler eSearchAeropuertosEspeciales;

        public int iOK
        {
            get { return (int)ViewState["VSiOK"]; }
            set { ViewState["VSiOK"] = value; }
        }
        public string sPOA
        {
            get { return (string)ViewState["VSPOA"]; }
            set { ViewState["VSPOA"] = value; }
        }
        public int iIdEspecial
        {
            get { return (int)ViewState["VSIdEspecial"]; }
            set { ViewState["VSIdEspecial"] = value; }
        }
        public DataTable dtAeroEspecial
        {
            get { return (DataTable)ViewState["VSdtAeroEspecial"]; }
            set { ViewState["VSdtAeroEspecial"] = value; }
        }
    }
}