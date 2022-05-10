using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ALE_MexJet.DomainModel;
using ALE_MexJet.Interfaces;
using ALE_MexJet.Objetos;
using ALE_MexJet.Presenter;
using NucleoBase.Core;

namespace ALE_MexJet.Views.CreditoCobranza
{
    public partial class frmAutorizacion : System.Web.UI.Page, IViewAutorizacion
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            iIdRemision = 0;
            iIdAjuste = 0;
            iIdRemision = Request.QueryString["IdRemision"].S().I();
            iIdAjuste = Request.QueryString["IdAjuste"].S().I();
            oPresenter = new Autorizacion_Presenter(this, new DBRemision());

            if (!IsPostBack)
            {
                if (iIdRemision > 0 && iIdAjuste > 0)
                {
                    if (eSearchObj != null)
                        eSearchObj(sender, e);
                }
            }
        }

        public void LoadRemision(DataSet ds)
        {
            try
            {
                dsDatosRem = null;
                dsDatosRem = ds;

                if (dsDatosRem != null && dsDatosRem.Tables[0].Rows.Count > 0)
                {
                    rdCargoAbono.Text = dsDatosRem.Tables[0].Rows[0]["Tipo"].S();
                    rdMotivo.Text = dsDatosRem.Tables[0].Rows[0]["DesMotivo"].S();
                    rdHoras.Text = dsDatosRem.Tables[0].Rows[0]["Horas"].S();
                    rdComentarios.Text = dsDatosRem.Tables[0].Rows[0]["Comentarios"].S();
                    sAutorizador = dsDatosRem.Tables[1].Rows[0]["Autorizador"].S();

                    if(dsDatosRem.Tables[0].Rows[0]["Estatus"].S().I() == 1)
                    {
                        btnAutorizar.Enabled = true;
                        btnRechazar.Enabled = true;
                        LblMsg.Visible = false;
                    }
                    else if(dsDatosRem.Tables[0].Rows[0]["Estatus"].S().I() == 2)
                    {
                        btnAutorizar.Enabled = false;
                        btnRechazar.Enabled = false;
                        LblMsg.Text = "La solicitud de ajuste ha sido aprobada con anterioridad";
                        LblMsg.Visible = true;
                    }
                    else if (dsDatosRem.Tables[0].Rows[0]["Estatus"].S().I() == -1)
                    {
                        btnAutorizar.Enabled = false;
                        btnRechazar.Enabled = false;
                        LblMsg.Text = "La solicitud de ajuste ha sido rechazada con anterioridad";
                        LblMsg.Visible = true;
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region VARIABLES Y PROPIEDADES
        Autorizacion_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;

        public DataTable dtConceptosR
        {
            get { return (DataTable)ViewState["VSConceptos"]; }
            set { ViewState["VSConceptos"] = value; }
        }
        public DataSet dsDatosRem
        {
            get { return (DataSet)ViewState["VSDatosRem"]; }
            set { ViewState["VSDatosRem"] = value; }
        }
        public int iIdRemision
        {
            get { return (int)ViewState["IdRemision"]; }
            set { ViewState["IdRemision"] = value; }
        }
        public int iIdAjuste
        {
            get { return (int)ViewState["idAjuste"]; }
            set { ViewState["idAjuste"] = value; }
        }
        public int iRespuesta
        {
            get { return (int)ViewState["VSRespuesta"]; }
            set { ViewState["VSRespuesta"] = value; }
        }
        public string sAutorizador
        {
            get { return (string)ViewState["VSAutorizador"]; }
            set { ViewState["VSAutorizador"] = value; }
        }
        public AjusteRemision oAjuste
        {
            get
            {
                try
                {
                    AjusteRemision oAJ = new AjusteRemision();
                    oAJ.IIdRemision = iIdRemision;
                    oAJ.IEstatus = iRespuesta;
                    oAJ.SUsuarioAutorizador = sAutorizador;
                    oAJ.IIdAjuste = iIdAjuste;
                    return oAJ;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            #endregion
        }

        protected void btnAutorizar_Click(object sender, EventArgs e)
        {
            try
            {
                iRespuesta = 2;

                if (eSaveObj != null)
                    eSaveObj(sender, e);

                btnAutorizar.Enabled = false;
                btnRechazar.Enabled = false;
                string sMsg = "Se autorizó correctamente el ajuste";
                MostrarMensaje(sMsg);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnRechazar_Click(object sender, EventArgs e)
        {
            try
            {
                iRespuesta = -1;

                if (eSaveObj != null)
                    eSaveObj(sender, e);

                string sMsg = "Se ha rechazado el ajuste solicitado";
                MostrarMensaje(sMsg);

                btnAutorizar.Enabled = false;
                btnRechazar.Enabled = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MostrarMensaje(string sMensaje)
        {
            lbl.Text = sMensaje;
            msgAlert.ShowOnPageLoad = true;
        }

    }
}