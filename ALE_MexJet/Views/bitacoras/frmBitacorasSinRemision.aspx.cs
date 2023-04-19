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

namespace ALE_MexJet.Views.bitacoras
{
    public partial class frmBitacorasSinRemision : System.Web.UI.Page, IViewBitacorasRemision
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            oPresenter = new BitacorasRemision_Presenter(this, new DBBitacorasRemision());
            gvBitacorasSinRemision.SettingsText.SearchPanelEditorNullText = "Ingresa la información a buscar:";
            gvBitacorasSinRemision.Settings.ShowTitlePanel = true;
            gvBitacorasSinRemision.SettingsText.Title = "Bitácoras sin remisionar";

            if (!IsPostBack)
            {
                if (eSearchObj != null)
                    eSearchObj(sender, e);
            }
        }
        protected void gvBitacorasSinRemision_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int pageIndex = gvBitacorasSinRemision.PageIndex;
                gvBitacorasSinRemision.PageIndex = pageIndex;
                gvBitacorasSinRemision.DataSource = dtBitacoras;
                gvBitacorasSinRemision.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void gvBitacorasSinRemision_BeforePerformDataSelect(object sender, EventArgs e)
        {
            try
            {
                if (dtBitacoras != null && dtBitacoras.Rows.Count > 0)
                {
                    (sender as ASPxGridView).DataSource = dtBitacoras;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void gvBitacorasSinRemision_Load(object sender, EventArgs e)
        {
            //if (dtAeroEspecial == null)
            //    return;
            //(sender as ASPxGridView).DataBind();
        }

        public void LoadBitacoras(DataTable dt)
        {
            try
            {
                dtBitacoras = null;
                dtBitacoras = dt;
                gvBitacorasSinRemision.DataSource = dt;
                gvBitacorasSinRemision.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        BitacorasRemision_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;

        public DataTable dtBitacoras
        {
            get { return (DataTable)ViewState["VSBitacoras"]; }
            set { ViewState["VSBitacoras"] = value; }
        }
    }
}