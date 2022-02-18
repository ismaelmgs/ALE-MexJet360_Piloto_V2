using ALE_MexJet.Clases;
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
using DevExpress.Web.Bootstrap;
using DevExpress.Web.Data;

namespace ALE_MexJet.Views.Seguridad
{
    public partial class frmPermisosContrato : System.Web.UI.Page, IViewPermisosContrato
    {
        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                mpeMensaje.OkButtonPressed += mpeMensaje_OkButtonPressed;
                oPresenter = new PermisosContrato_Presenter(this, new DBPermisosContrato());

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

        private void mpeMensaje_OkButtonPressed(object sender, EventArgs args)
        {

        }

        protected void Unnamed_Unload(object sender, EventArgs e)
        {
            MethodInfo methodInfo = typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Where(i => i.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel")).First();
            methodInfo.Invoke(ScriptManager.GetCurrent(Page),
                new object[] { sender as UpdatePanel });
        }

        protected void gvSecciones_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    int iIdSeccion = gvSecciones.DataKeys[e.Row.RowIndex]["IdSeccion"].S().I();

                    

                    // Checar las pestañas seleccionadas segun el DataTable Nuevo
                    ASPxCheckBox chkPestana = (ASPxCheckBox)e.Row.FindControl("chkSeccion");
                    if (chkPestana != null)
                    {
                        foreach (DataRow item in dtSecciones.Rows)
                        {
                            string Seccion = item["IdSeccion"].S();

                            if (iIdSeccion.S() == Seccion.S())
                            {
                                if (item["AccesoSeccion"].I() == 1)
                                {
                                    chkPestana.Checked = true;
                                }
                                else
                                    chkPestana.Checked = false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvSecciones_RowDataBound", "Aviso");
            }
        }

        protected void gvCampos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                GridView gv = (GridView)sender;
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    int iIdCampo = gv.DataKeys[e.Row.RowIndex]["IdCampo"].S().I();
                    CheckBox chkCampo = (CheckBox)e.Row.FindControl("chkCampo");
                    if (chkCampo != null)
                    {
                        foreach (DataRow item in dtCampos.Rows)
                        {
                            string sIdCampo = item.S("IdCampo");
                            if (iIdCampo.S() == sIdCampo)
                            {
                                if (item["AccesoCampo"].I() == 1)
                                {
                                    chkCampo.Checked = true;
                                }
                                else
                                    chkCampo.Checked = false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvCampos_RowDataBound", "Aviso");
            }
        }

        protected void ddlPestana_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                btnGuardar.Visible = true;
                if (ddlPestana.SelectedItem != null)
                {
                    string sIdPestana = ddlPestana.SelectedItem.Value.S();

                    if (eObjSelected != null)
                        eObjSelected(sender, e);

                    gvSecciones.DataSource = dtSecciones;
                    gvSecciones.DataBind();
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "ddlPestana_SelectedIndexChanged", "Aviso");
            }
        }

        protected void ddlRol_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                btnGuardar.Visible = true;
                if (ddlPestana.SelectedItem != null)
                {
                    string sIdPestana = ddlPestana.SelectedItem.Value.S();

                    if (eObjSelected != null)
                        eObjSelected(sender, e);

                    gvSecciones.DataSource = dtSecciones;
                    gvSecciones.DataBind();
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "ddlRol_SelectedIndexChanged", "Aviso");
            }
        }

        protected void gvSecciones_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int idx = e.CommandArgument.S().I();

                if (e.CommandName == "Detalle")
                {
                    ASPxCheckBox chk = (ASPxCheckBox)gvSecciones.Rows[idx].FindControl("chkSeccion");
                    if (chk != null)
                    {
                        iAccesoSeccion = chk.Checked ? 1 : 0;
                    }

                    Label lbl = (Label)gvSecciones.Rows[idx].FindControl("lblIdSeccion");
                    if (lbl != null)
                    {
                        iIdSeccion = lbl.Text.S().I();
                    }

                    DataRow[] rows = dtCampos.Select("IdSeccion = " + iIdSeccion.S());
                    if (rows != null)
                    {
                        DataTable dt = rows.CopyToDataTable();
                        gvCampos.DataSource = dt;
                        gvCampos.DataBind();
                    }

                    ppCamposSeccion.ShowOnPageLoad = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                PermisosContrato oPermisosContrato = new PermisosContrato();
                oPermisosContrato.iRol = ddlRol.SelectedItem.Value.S().I();
                oPermisosContrato.iPestana = ddlPestana.SelectedItem.Value.S().I();

                SeccionesPestana oSeccion = new SeccionesPestana();
                oSeccion.campos = new List<CamposPestana>();
                oSeccion.iSeccion = iIdSeccion;
                oSeccion.iAcceso = iAccesoSeccion;

                foreach (GridViewRow rowD in gvCampos.Rows)
                {
                    CamposPestana oCampos = new CamposPestana();
                    Label lbl = (Label) gvCampos.Rows[rowD.RowIndex].FindControl("lblIdCampo");

                    oCampos.iCampo = lbl.Text.S().I();

                    CheckBox chkCampo = (CheckBox)rowD.FindControl("chkCampo");
                    if (chkCampo != null)
                    {
                        if (chkCampo.Checked == true)
                            oCampos.iAcceso = chkCampo.Checked ? 1 : 0;
                        else
                            oCampos.iAcceso = 0;
                    }

                    CheckBox chkCampoVis = (CheckBox)rowD.FindControl("chkVisible");
                    if (chkCampoVis != null)
                    {
                        bool ban = chkCampoVis.Checked;
                    }

                    oSeccion.campos.Add(oCampos);
                }

                if (oPermisosContrato.secciones == null)
                    oPermisosContrato.secciones = new List<SeccionesPestana>();

                oPermisosContrato.secciones.Add(oSeccion);
                oPermisos = oPermisosContrato;

                if (eSaveObj != null)
                    eSaveObj(sender, e);

                ppCamposSeccion.ShowOnPageLoad = false;
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnGuardar_Click", "Aviso");
            }
        }

        
        #endregion


        #region METODOS

        public void LoadObjects()
        {
            try
            {
                ddlRol.DataSource = dtRoles;
                ddlRol.TextField = "RolDescripcion";
                ddlRol.ValueField = "RolId";
                ddlRol.DataBind();

                ddlPestana.DataSource = dtPestanas;
                ddlPestana.TextField = "Descripcion";
                ddlPestana.ValueField = "IdPestana";
                ddlPestana.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MostrarMensaje(string Mensaje, string Caption)
        {
            mpeMensaje.ShowMessage(Mensaje, Caption);
        }

        #endregion


        #region VARIABLES Y PROPIEDADES
        private const string sClase = "frmPermisosContrato.aspx.cs";
        private const string sPagina = "frmPermisosContrato.aspx";

        PermisosContrato_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;

        public int iAccesoSeccion
        {
            get { return (int)ViewState["VSAccesoSeccion"]; }
            set { ViewState["VSAccesoSeccion"] = value; }
        }

        public int iIdSeccion
        {
            get { return (int)ViewState["VSIdSeccion"]; }
            set { ViewState["VSIdSeccion"] = value; }
        }
        
        public int iIdRol
        {
            get { return ddlRol.SelectedItem.Value.S().I(); }
        }

        public int iIdPestana
        {
            get { return ddlPestana.SelectedItem.Value.S().I(); }
        }

        public DataTable dtRoles
        {
            get { return (DataTable)ViewState["VSRol"]; }
            set { ViewState["VSRol"] = value; }
        }

        public DataTable dtPestanas
        {
            get { return (DataTable)ViewState["VSPestanas"]; }
            set { ViewState["VSPestanas"] = value; }
        }

        public DataTable dtSecciones
        {
            get { return (DataTable)ViewState["VSSecciones"]; }
            set { ViewState["VSSecciones"] = value; }
        }

        public DataTable dtCampos
        {
            get { return (DataTable)ViewState["VSCampos"]; }
            set { ViewState["VSCampos"] = value; }
        }
        
        public PermisosContrato oPermisos
        {
            get { return (PermisosContrato)ViewState["VSPermisosContrato"]; }
            set { ViewState["VSPermisosContrato"] = value; }
        }

        #endregion

        //protected void gvCampos_BeforePerformDataSelect(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        ASPxGridView grid = sender as ASPxGridView;

        //        int iIdSeccion = grid.GetMasterRowKeyValue().S().I();

        //        DataRow[] rows = dtCampos.Select("IdSeccion = " + iIdSeccion.S());
        //        if (rows != null)
        //        {
        //            DataTable dt = rows.CopyToDataTable();
        //            grid.DataSource = dt;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //protected void gvSecciones2_DataBound(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        int cont = 0;
        //        foreach (DataRow row in dtSecciones.Rows)
        //        {
        //            bool bAcceso = row["AccesoSeccion"].S() == "1" ? true : false;
        //            gvSecciones2.Selection.SetSelectionByKey(row["IdSeccion"].S().I(), bAcceso);
        //            cont++;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //protected void gvCampos_DataBound(object sender, EventArgs e)
        //{
        //    int cont = 0;
        //    BootstrapGridView gvCampos = (BootstrapGridView)sender;
        //    if (gvCampos != null)
        //    {
        //        foreach (DataRow row in dtCampos.Rows)
        //        {
        //            bool bAcceso = row["AccesoCampo"].S() == "1" ? true : false;

        //            if (gvCampos.GetRowValuesByKeyValue(row["IdCampo"].S().I()) != null)
        //            {
        //                gvCampos.Selection.SetSelectionByKey(row["IdCampo"].S().I(), bAcceso);
        //            }
        //            cont++;
        //        }
        //    }
        //}


        //protected void ASPxButton2_Click(object sender, EventArgs e)
        //{
        //    GridViewSelection gvs = gvSecciones2.Selection;

        //    foreach (DataRow row in dtSecciones.Rows)
        //    {
        //        bool ban = gvs.IsRowSelectedByKey(row["IdSeccion"].S().I());
        //        if (ban)
        //        {

        //        }
        //    }
        //}

        
    }
}