using ALE_MexJet.Objetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NucleoBase.Core;
using ALE_MexJet.DomainModel;
using ALE_MexJet.Presenter;
using ALE_MexJet.Interfaces;
using ALE_MexJet.Clases;
using System.Data;

namespace ALE_MexJet.Views.JetSmart
{
    public partial class frmEncuesta : System.Web.UI.Page, IBaseView
    {
        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            oPresenter = new Encuesta_Presenter(this, new DBEncuesta());

            hdIP.Value = Utils.GetIPAddress();
            hdUsuario.Value = Utils.GetUser; 
        }

        protected void tlPreguntas_NodeInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            //if (eNewObj != null)
            //    eNewObj(sender, e);
        }

        protected void tlPreguntas_NodeUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            DevExpress.Web.Data.ASPxDataUpdatingEventArgs eU = e;

        }

        protected void tlPreguntas_NodeDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            DevExpress.Web.Data.ASPxDataDeletingEventArgs eD = e;
        }
        #endregion

        #region METODOS
        #endregion

        #region VARIABLES Y PROPIEDADES
        Encuesta_Presenter oPresenter;

        private const string sPagina = "frmEncuesta.aspx";
        private const string sClase = "frmEncuesta.aspx.cs";

        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;


        public Encuesta oEncuesta
        {
            get { return (Encuesta)ViewState["VSEncuesta"]; }
            set { ViewState["VSEncuesta"] = value; }
        }


        #endregion

        protected void btnLista_Click(object sender, EventArgs e)
        {
            

        }
    }


    

}