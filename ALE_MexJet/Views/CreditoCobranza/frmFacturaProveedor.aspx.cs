using ALE_MexJet.DomainModel;
using ALE_MexJet.Interfaces;
using ALE_MexJet.Objetos;
using ALE_MexJet.Presenter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NucleoBase.Core;

using DevExpress.Export;
using DevExpress.XtraPrinting;
using System.ComponentModel;
using ALE_MexJet.ControlesUsuario;
using DevExpress.Web.Data;
using DevExpress.Web;
using ALE_MexJet.Clases;
using DevExpress.Utils;
using System.Reflection;

namespace ALE_MexJet.Views.CreditoCobranza
{
    public partial class frmFacturaProveedor : System.Web.UI.Page, IVIewFacturaProveedor
    {
        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                gvFacturaProveedor.SettingsText.SearchPanelEditorNullText = "Ingrese la información a buscar";
                gvBitacora.SettingsText.SearchPanelEditorNullText = "Ingrese la información a buscar";
                oPresenter = new FacturaProveedor_Presenter(this, new DBFacturaProveedor());

                if (!IsPostBack)
                {
                    if (eSearchObj != null)
                        eSearchObj(null, null);

                    if (eSearCliente != null)
                        eSearCliente(null, null);

                    if (eSearMatricula != null)
                        eSearMatricula(null, null);

                    if (eSearTipoMoneda != null)
                        eSearTipoMoneda(null, null);

                    PDetallefactura.Visible = false;

                    iIdFaturaProveedor = 0;
                }

                RecargaGVFacturaProveedor();
                RecargaGVBitacora();
            }
            catch (Exception x) { Utils.SaveErrorEnBitacora(mpeMensaje, x.Message, sPagina, sClase, "Page_Load", "Aviso"); }
        }
        protected void UpdatePanel1_Unload(object sender, EventArgs e)
        {
            try
            {
                MethodInfo methodInfo = typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Where(i => i.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel")).First();
                methodInfo.Invoke(ScriptManager.GetCurrent(Page),
                    new object[] { sender as UpdatePanel });
            }
            catch (Exception x) { Utils.SaveErrorEnBitacora(mpeMensaje, x.Message, sPagina, sClase, "UpdatePanel1_Unload", "Aviso"); }
        }
        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                LimpiaControles();

                btnGuardar.Visible = true;
                ppFactura.ShowOnPageLoad = true;
                PDetallefactura.Visible = true;

                if (eSearPiernaRent != null)
                    eSearPiernaRent(null, null);
            }
            catch (Exception x) { Utils.SaveErrorEnBitacora(mpeMensaje, x.Message, sPagina, sClase, "btnNuevo_Click", "Aviso"); }
        }
        protected void btnSalir_Click(object sender, EventArgs e)
        {
            try
            {
                gvBitacora.DataSource = null;
                gvBitacora.DataBind();

                txtPProveedor.Text = "";
                txtFactura.Text = "";
                txtSubtotal.Text = "";
                txtIVA.Text = "";
                txtTotal.Text = "";
                ddlMoneda.SelectedItem = null;

                iIdFaturaProveedor = 0;

                if (eSearchObj != null)
                    eSearchObj(null, null);

                ASPxEdit.ClearEditorsInContainer(ppFactura);
            }
            catch (Exception x) { Utils.SaveErrorEnBitacora(mpeMensaje, x.Message, sPagina, sClase, "btnSalir_Click", "Aviso"); }
        }
        protected void gvBitacora_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            try
            {
                ASPxGridView gridView = (ASPxGridView)sender;

                if (gridView.IsNewRowEditing)
                {
                    eCrud = Enumeraciones.TipoOperacion.Insertar;
                }
                else
                {
                    eCrud = Enumeraciones.TipoOperacion.Actualizar;
                }

                e.Editor.ReadOnly = false;

                if (e.Column.FieldName == "Matricula")
                {
                    ASPxComboBox comboBox = e.Editor as ASPxComboBox;
                    comboBox.DataSource = (DataTable)ViewState["ddlMatricula"];
                    comboBox.ValueField = "IdAeroave";
                    comboBox.ValueType = typeof(Int32);
                    comboBox.TextField = "Matricula";
                    comboBox.DataBindItems();
                }

                if (e.Column.FieldName == "FolioReal")
                {
                    oPresenter = new FacturaProveedor_Presenter(this, new DBFacturaProveedor());

                    if (eSearBitacora != null)
                        eSearBitacora(null, null);

                    ASPxComboBox comboBox = e.Editor as ASPxComboBox;
                    comboBox.DataSource = (DataTable)ViewState["ddlBitacora"];
                    comboBox.ValueField = "FolioReal";
                    comboBox.ValueType = typeof(Int32);
                    comboBox.TextField = "FolioReal";
                    comboBox.DataBindItems();
                }
            }
            catch (Exception x) { Utils.SaveErrorEnBitacora(mpeMensaje, x.Message, sPagina, sClase, "gvBitacora_CellEditorInitialize", "Aviso"); }
        }
        protected void gvBitacora_RowDeleting(object sender, ASPxDataDeletingEventArgs e)
        {
            try
            {
                eCrud = Enumeraciones.TipoOperacion.Eliminar;
                oCrud = e;

                if (eDeleteObj != null)
                    eDeleteObj(sender, e);

                CancelEditing(e);

                if (eSearchProvDetalle != null)
                    eSearchProvDetalle(null, null);
            }
            catch (Exception x) { Utils.SaveErrorEnBitacora(mpeMensaje, x.Message, sPagina, sClase, "gvBitacora_RowDeleting", "Aviso"); }
        }
        protected void gvBitacora_RowInserting(object sender, ASPxDataInsertingEventArgs e)
        {
            try
            {
                eCrud = Enumeraciones.TipoOperacion.Insertar;
                oCrud = e;

                e.NewValues["iIdFaturaProveedor"] = iIdFaturaProveedor;
                if (eSaveProveedorDetalle != null)
                    eSaveProveedorDetalle(sender, e);

                CancelEditing(e);

                if (eSearchProvDetalle != null)
                    eSearchProvDetalle(null, null);
            }
            catch (Exception x) { Utils.SaveErrorEnBitacora(mpeMensaje, x.Message, sPagina, sClase, "gvBitacora_RowInserting", "Aviso"); }
        }
        protected void gvBitacora_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
        {
            try
            {
                eCrud = Enumeraciones.TipoOperacion.Actualizar;
                oCrud = e;

                if (eSaveObj != null)
                    eSaveObj(sender, e);

                CancelEditing(e);

                if (eSearchProvDetalle != null)
                    eSearchProvDetalle(null, null);
            }
            catch (Exception x) { Utils.SaveErrorEnBitacora(mpeMensaje, x.Message, sPagina, sClase, "gvBitacora_RowUpdating", "Aviso"); }
        }
        protected void gvBitacora_StartRowEditing(object sender, ASPxStartRowEditingEventArgs e)
        {

        }
        protected void gvBitacora_RowValidating(object sender, ASPxDataValidationEventArgs e)
        {

        }
        protected void gvBitacora_CancelRowEditing(object sender, ASPxStartRowEditingEventArgs e)
        {
            
        }
        protected void btnNuevoDetale_Click(object sender, EventArgs e)
        {
            gvBitacora.AddNewRow();
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvBitacora.Selection.Count > 0)
                {                    
                    if (string.IsNullOrEmpty(lblTipoCambio.Text) || (lblTipoCambio.Text == "El tipo de cambio es requerido."))
                    {
                        lblTipoCambio.Text = "El tipo de cambio es requerido.";
                        lblTipoCambio.ForeColor = System.Drawing.Color.Red;
                        return;
                    }

                    if (eSaveProveedor != null)
                        eSaveProveedor(null, null);

                    List<object> fieldValues = gvBitacora.GetSelectedFieldValues(new string[] { "IdBitacora" });

                    if (fieldValues.Count > 0)
                    {
                        foreach (var idbitacora in fieldValues)
                        {
                            IdBitacora = idbitacora.S();

                            if (eSaveProveedorDetalle != null)
                                eSaveProveedorDetalle(sender, e);
                        }
                    }

                    LimpiaControles();

                }
                else
                {
                    MostrarMensaje("Se deben seleccionar piernas." , "Mensaje");
                }
                
                if (eSearchObj != null)
                    eSearchObj(null, null);
            }
            catch (Exception x) { Utils.SaveErrorEnBitacora(mpeMensaje, x.Message, sPagina, sClase, "btnGuardar_Click", "Aviso"); }
        }
        protected void gvFacturaProveedor_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
        {
            try
            {
                if (e.CommandArgs.CommandName == "Eliminar")
                {
                    iIdFaturaProveedor = e.CommandArgs.CommandArgument.S().I();
                    if (eEliminaProv != null)
                        eEliminaProv(null, null);

                    if (eSearchObj != null)
                        eSearchObj(null, null);

                    iIdFaturaProveedor = 0;
                }
                else if (e.CommandArgs.CommandName == "Detalle")
                {
                    btnGuardar.Visible = false;
                    iIdFaturaProveedor = e.CommandArgs.CommandArgument.S().I();

                    if (eSearchProvDetalle != null)
                        eSearchProvDetalle(null, null);

                    if (eSearProvED != null)
                        eSearProvED(null, null);

                    PDetallefactura.Visible = true;

                    ppFactura.ShowOnPageLoad = true;
                }
            }
            catch (Exception x) { Utils.SaveErrorEnBitacora(mpeMensaje, x.Message, sPagina, sClase, "gvFacturaProveedor_RowCommand", "Aviso"); }
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if (eSearchObj != null)
                    eSearchObj(null, null);
            }
            catch (Exception x) { Utils.SaveErrorEnBitacora(mpeMensaje, x.Message, sPagina, sClase, "btnBuscar_Click", "Aviso"); }
        }
        protected void btnTipoCambio_Click(object sender, EventArgs e)
        {
            try
            {
                string stipoCambio = string.Empty;
                using (wsSyteline.Iws_SyteLineClient ws = new wsSyteline.Iws_SyteLineClient())
                {
                    ws.Open();
                    stipoCambio = ws.RecuperaTipoCambioFecha(deFechaFactura.Text.ToString().Dt());
                    ws.Close();
                }

                lblTipoCambio.ForeColor = System.Drawing.Color.Black;
                lblTipoCambio.Text = stipoCambio.D().ToString("C");
            }
            catch (Exception x) { Utils.SaveErrorEnBitacora(mpeMensaje, x.Message, sPagina, sClase, "btnTipoCambio_Click", "Aviso"); }
        }

        #endregion 

        #region METODOS
        protected void RecargaGVFacturaProveedor()
        {
            try
            {
                DataTable DT = (DataTable)ViewState["gvFacturaProveedor"];
                if (DT != null)
                {
                    gvFacturaProveedor.DataSource = DT;
                    gvFacturaProveedor.DataBind();
                }
            }
            catch (Exception x) { throw x; }
        }
        public void LoadFactura(DataTable dtObjCat)
        {
            try
            {
                gvFacturaProveedor.DataSource = dtObjCat;
                gvFacturaProveedor.DataBind();
                ViewState["gvFacturaProveedor"] = dtObjCat;
            }
            catch (Exception x) { throw x; }
        }
        public void LoadCliente(DataTable dtObjCat)
        {
            try
            {
                ddlCliente.DataSource = dtObjCat;
                ddlCliente.TextField = "CodigoCliente";
                ddlCliente.ValueField = "IdCliente";
                ddlCliente.DataBind();
            }
            catch (Exception x) { throw x; }
        }
        public void LoadMatricla(DataTable dtObjCat)
        {
            try
            {
                ViewState["ddlMatricula"] = dtObjCat;
            }
            catch (Exception x) { throw x; }
        }
        public void LoadTipoMoneda(DataTable dtObjCat)
        {
            try
            {
                ddlMoneda.DataSource = dtObjCat;
                ddlMoneda.TextField = "Descripcion";
                ddlMoneda.ValueField = "IdTipoMoneda";
                ddlMoneda.DataBind();
            }
            catch (Exception x) { throw x; }
        }
        public void LoadBitacora(DataTable dtObjCat)
        {
            try
            {
                ViewState["ddlBitacora"] = dtObjCat;
            }
            catch (Exception x) { throw x; }
        }
        public void GuardoProveedor(int iObjCat)
        {
            try
            {
                iIdFaturaProveedor = iObjCat;
                PDetallefactura.Visible = true;
            }
            catch (Exception x) { throw x; }
        }
        public void MostrarMensaje(string sMensaje, string sCaption)
        {
            try
            {
                popup.HeaderText = sCaption;
                lbl.Text = sMensaje;
                popup.ShowOnPageLoad = true;
            }
            catch (Exception x) { throw x; }
        }
        protected void CancelEditing(CancelEventArgs e)
        {
            try
            {
                e.Cancel = true;
                gvBitacora.CancelEdit();
            }
            catch (Exception ex) { throw ex; }
        }
        public void LoadProvDetalle(DataTable dtObjCat)
        {
            try
            {
                gvBitacora.DataSource = dtObjCat;
                gvBitacora.DataBind();
                ViewState["gvBitacora"] = dtObjCat;
            }
            catch (Exception x) { throw x; }
        }
        protected void RecargaGVBitacora()
        {
            try
            {
                DataTable DT = (DataTable)ViewState["gvBitacora"];

                if (DT != null)
                {
                    gvBitacora.DataSource = DT;
                    gvBitacora.DataBind();
                }
            }
            catch (Exception x) { throw x; }
        }
        public void LoadControlesProvED(DataTable dtObjCat)
        {
            try
            {
                if (dtObjCat != null)
                {
                    txtPProveedor.Text = dtObjCat.Rows[0]["Provedor"].S();
                    txtFactura.Text = dtObjCat.Rows[0]["Factura"].S();
                    txtSubtotal.Text = dtObjCat.Rows[0]["Subtotal"].S();
                    txtIVA.Text = dtObjCat.Rows[0]["IVA"].S();
                    txtTotal.Text = dtObjCat.Rows[0]["Total"].S();
                    ddlMoneda.SelectedIndex = dtObjCat.Rows[0]["TipoMoneda"].S().I() == 1 ? 0 : 1;
                    lblTipoCambio.Text = dtObjCat.Rows[0]["TipoCambio"].S();
                    deFechaFactura.Text = dtObjCat.Rows[0]["FechaFactura"].S();
                }
            }
            catch (Exception x) { throw x; }
        }
        public void LoadPiernaRent(DataTable dtObjCat)
        {
            try
            {
                gvBitacora.DataSource = dtObjCat;
                gvBitacora.DataBind();
                ViewState["gvBitacora"] = dtObjCat;
            }
            catch (Exception x) { throw x; }
        }
        protected void LimpiaControles()
        {
            try
            {
                txtPProveedor.Text = null;
                txtFactura.Text = null;
                txtSubtotal.Text = null;
                txtIVA.Text = null;
                txtTotal.Text = null;
                ddlMoneda.SelectedItem = null;
                deFechaFactura.Text = null;
                lblTipoCambio.Text = null;
                gvBitacora.DataSource = null;
            }
            catch (Exception x) { throw x; }
        }
        #endregion

        #region Variables

        private const string sClase = "frmFacturaProveedor.aspx.cs";
        private const string sPagina = "frmFacturaProveedor.aspx";
        public ALE_MexJet.Objetos.Enumeraciones.TipoOperacion eCrud
        {
            get { return (ALE_MexJet.Objetos.Enumeraciones.TipoOperacion)ViewState["eCrud"]; }
            set { ViewState["eCrud"] = value; }
        }
        public object oCrud
        {
            get { return ViewState["CrudProveedor"]; }
            set { ViewState["CrudProveedor"] = value; }
        }
        public int iIdFaturaProveedor
        {
            get { return ViewState["iIdFaturaProveedor"].S().I(); }
            set { ViewState["iIdFaturaProveedor"] = value; }
        }
        public string IdBitacora
        {
            get { return ViewState["IdBitacora"].S(); }
            set { ViewState["IdBitacora"] = value; }
        }

        FacturaProveedor_Presenter oPresenter;

        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eSearCliente;
        public event EventHandler eSearMatricula;
        public event EventHandler eSearTipoMoneda;
        public event EventHandler eSearBitacora;
        public event EventHandler eSaveProveedor;
        public event EventHandler eSaveProveedorDetalle;
        public event EventHandler eSearchProvDetalle;
        public event EventHandler eEliminaProv;
        public event EventHandler eSearProvED;
        public event EventHandler eSearPiernaRent;

        public object[] oArrFiltros 
        {
            get
            {
                return new object[]
                { 
                    "@Proveedor", txtProveedor.Text.S() + "%",
                    "@Cliente", ddlCliente.SelectedItem != null && ddlCliente.SelectedItem.Text != "Clientes" ? ddlCliente.SelectedItem.Text : string.Empty,
                    "@Fecha", deFecha.Text == "" ? string.Empty : deFecha.Date.ToString("MM/dd/yyyy")
                };
            }
        }
        public object[] oArrMatricula 
        {
            get
            {
                return new object[]
                { 
                    "@Matricula", HiddenField1.Value
                };
            }
        }
        public object[] oArrDetalle 
        {
            get
            {
                return new object[]
                { 
                    "@IdFaturaProveedor", iIdFaturaProveedor
                };
            }
        }
        public object[] oArrDeleteFac
        {
            get
            {
                return new object[]
                { 
                    "@IdFacturaProveedor", iIdFaturaProveedor
                };
            }
        }
        public object[] oArrProvED
        {
            get
            {
                return new object[]
                { 
                    "@IdFacturaProveedor", iIdFaturaProveedor
                };
            }
        }
        public object[] OArrProvDet
        {
            get
            {
                return new object[]
                { 
                    
                    "@IdFaturaProveedor",  iIdFaturaProveedor,  
                    "@IdBitacora", IdBitacora,  
                    "@UsuarioCreacion",    "",  
                    "@IP",   ""  

                };
            }
        }
        
        public FacturaProveedor oProveedor
        {
            get
            {
                FacturaProveedor oProv = new FacturaProveedor();
                oProv.Provedor = txtPProveedor.Text;
                oProv.Factura = txtFactura.Text;
                oProv.Subtotal = txtSubtotal.Text;
                oProv.IVA = txtIVA.Text;
                oProv.Total = txtTotal.Text;
                oProv.TipoMoneda = ddlMoneda.SelectedItem.Value.S().I();
                oProv.Status = 1;
                oProv.UsuarioCreacion = ((UserIdentity)Session["UserIdentity"]).sUsuario.S();
                oProv.IP = "";
                oProv.IdFaturaProveedor = iIdFaturaProveedor;
                oProv.FechaFactura = deFechaFactura.Date;
                oProv.TipoCambio = lblTipoCambio.Text;
                return oProv;
            }
        }

        #endregion 

    }
}