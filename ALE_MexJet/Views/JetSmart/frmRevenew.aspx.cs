using ALE_MexJet.Presenter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NucleoBase.Core;
using ALE_MexJet.Clases;
using ALE_MexJet.DomainModel;
using ALE_MexJet.Interfaces;
using System.Reflection;
using ALE_MexJet.Objetos;
using DevExpress.Web;

namespace ALE_MexJet.Views.JetSmart
{
    public partial class frmRevenew : System.Web.UI.Page, IViewRevenew
    {
        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                oPresenter = new Revenew_Presenter(this, new DBRevenew());

                if (!IsPostBack)
                {
                    if (eSearchObj != null)
                        eSearchObj(sender, e);
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "Page_Load", "Aviso");
            }
        }
        protected void gvTiposCliente_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    iIdTipoCliente = dtTiposCliente.Rows[e.Row.RowIndex]["IdTipoCliente"].S().I();

                    if (eObjSelected != null)
                        eObjSelected(sender, e);

                    GridView gvDetalle = (GridView)e.Row.FindControl("gvDetalle");
                    if (gvDetalle != null)
                    {
                        gvDetalle.DataSource = dtDescuentos;
                        gvDetalle.DataBind();
                    }

                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvTiposCliente_RowDataBound", "Aviso");
            }
        }
        protected void gvDetalle_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chk = (CheckBox)e.Row.FindControl("chkDescAcumulado");
                    if (chk != null)
                    {
                        chk.Checked = dtDescuentos.Rows[e.Row.RowIndex]["Acumulado"].S().B();
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvDetalle_RowDataBound", "Aviso"); 
            }
        }
        protected void UpdatePanel1_Unload(object sender, EventArgs e)
        {
            MethodInfo methodInfo = typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Where(i => i.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel")).First();
            methodInfo.Invoke(ScriptManager.GetCurrent(Page),
                new object[] { sender as UpdatePanel });
        }
        protected void btnGuardarDesc_Click(object sender, EventArgs e)
        {
            try
            {
                if (RevisaDescuentoValido())
                {
                    if(iIdDescuento == 0)
                    {
                        if (eSaveObj != null)
                            eSaveObj(sender, e);
                    }
                    else
                    {
                        if (eUpdateDescuento != null)
                            eUpdateDescuento(sender, e);
                    }

                    if (eSearchObj != null)
                        eSearchObj(sender, e);
                }
                else
                {
                    lblMsg.Text = "El descuento no puede ser mayor al 100%, favor de verificar.";
                    ppDescuentos.ShowOnPageLoad = true;
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnGuardarDesc_Click", "Aviso");
            }
        }
        protected void btnAgregarDesc_Click(object sender, EventArgs e)
        {
            try
            {
                ASPxButton imb = (ASPxButton)sender;
                GridViewRow Row = (GridViewRow)imb.NamingContainer;

                iIdTipoCliente = dtTiposCliente.Rows[Row.RowIndex]["IdTipoCliente"].S().I();
                lblTipoClienteResp.Text = dtTiposCliente.Rows[Row.RowIndex]["Descripcion"].S();
                
                LimpiaCampos();

                iIdDescuento = 0;
                ppDescuentos.ShowOnPageLoad = true;
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnAgregarDesc_Click", "Aviso");
            }
        }
        protected void gvDetalle_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Delete")
                {
                    iIdDescuento = e.CommandArgument.S().I();
                    if (eDeleteObj != null)
                        eDeleteObj(sender, e);
                }

                if (e.CommandName == "Edit")
                {
                    iIdDescuento = e.CommandArgument.S().I();

                    if (eGetDescuento != null)
                        eGetDescuento(sender, e);

                    ppDescuentos.ShowOnPageLoad = true;
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnAgregarDesc_Click", "Aviso");
            }
        }
        #endregion

        #region METODOS
        public void LoadTiposCliente(DataTable dt)
        {
            try
            {
                dtTiposCliente = dt;
                gvTiposCliente.DataSource = dt;
                gvTiposCliente.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void MostrarMensaje(string sMensaje, string sCaption)
        {
            mpeMensaje.ShowMessage(sMensaje, sCaption);
        }
        private bool RevisaDescuentoValido()
        {
            try
            {
                bool ban = false;

                DataRow[] dr = dtTiposCliente.Select("IdTipoCliente = " + iIdTipoCliente);
                if (dr != null)
                {
                    decimal dPor = dr[0]["DescuentoTotal"].S().D();

                    // Si es edición sobreescribimos el porcentaje para restarle el valor actual
                    if (iIdDescuento != 0)
                    {
                        dPor = dPor - dDescuentoTemp;
                    }
                    
                    if (chkDescuento.Checked)
                    {
                        if ((dPor + txtPorDescuento.Text.S().D()) <= 100)
                            ban = true;
                    }
                    else
                    {
                        ban = true;
                    }
                }

                return ban;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void LimpiaCampos()
        {
            try
            {
                txtDescDescuento.Text = string.Empty;
                txtPorDescuento.Text = string.Empty;
                lblMsg.Text = string.Empty;
                chkDescuento.Checked = true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void LoadDescuento(DataTable dt)
        {
            try
            {
                txtDescDescuento.Text = dt.Rows[0].S("Descripcion");
                dDescuentoTemp = dt.Rows[0].S("Porcentaje").D();
                txtPorDescuento.Text = dt.Rows[0].S("Porcentaje");
                chkDescuento.Checked = dt.Rows[0].S("Acumulado") == "1" ? true : false;
                lblTipoClienteResp.Text = dt.Rows[0].S("Paquete");
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region VARIABLES Y PROPIEDADES
        private const string sClase = "frmRevenew.aspx.cs";
        private const string sPagina = "frmRevenew.aspx";

        Revenew_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eGetDescuento;
        public event EventHandler eUpdateDescuento;

        public Revenew oDescReve
        {
            set 
            {
                Revenew oTemp = value;
                if (oTemp != null)
                {
                    iIdTipoCliente = oTemp.iIdTipoCliente;
                    txtDescDescuento.Text = oTemp.sDescripcion.S();
                    txtPorDescuento.Text = oTemp.dDescuento.S();
                    chkDescuento.Checked = oTemp.iAcumulado == 1 ? true : false;
                }
            }
            get
            {
                Revenew oRev = new Revenew();
                oRev.iIdDescuento = iIdDescuento;
                oRev.iIdTipoCliente = iIdTipoCliente;
                oRev.sDescripcion = txtDescDescuento.Text.S();
                oRev.dDescuento = txtPorDescuento.Text.S().D();
                oRev.iAcumulado = chkDescuento.Checked ? 1 : 0;

                return oRev;
            }
        }
        public int iIdTipoCliente
        {
            get { return (int)ViewState["VSIdTipoCliente"]; }
            set { ViewState["VSIdTipoCliente"] = value; }
        }
        public int iIdDescuento
        {
            get { return (int)ViewState["VSIdDescuento"]; }
            set { ViewState["VSIdDescuento"] = value; }
        }
        public decimal dDescuentoTemp
        {
            get { return (decimal)ViewState["VSDescuentoTemp"]; }
            set { ViewState["VSDescuentoTemp"] = value; }
        }
        public DataTable dtTiposCliente
        {
            get { return (DataTable)ViewState["VSTiposCliente"]; }
            set { ViewState["VSTiposCliente"] = value; }
        }
        public DataTable dtDescuentos
        {
            get { return (DataTable)ViewState["VSDescuentos"]; }
            set { ViewState["VSDescuentos"] = value; }
        }

        #endregion

        protected void gvDetalle_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (eSearchObj != null)
                eSearchObj(sender, e);
        }

        protected void gvDetalle_RowEditing(object sender, GridViewEditEventArgs e)
        {
            if (eSearchObj != null)
                eSearchObj(sender, e);
        }
    }
}