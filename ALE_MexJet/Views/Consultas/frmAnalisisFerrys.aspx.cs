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
using System.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ALE_MexJet.Views.Consultas
{
    public partial class frmAnalisisFerrys : System.Web.UI.Page, IViewAnalisisFerrys
    {
        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                oPresenter = new AnalisisFerrys_Presenter(this, new DBAnalisisFerrys());
                if (!IsPostBack)
                {
                    if (eSearchObj != null)
                        eSearchObj(null, null);
                }

                CargaGrid();
            }
            catch (Exception ex) { Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "Page_Load", "Aviso"); }
        }
        protected void btnBusqueda_Click(object sender, System.EventArgs e)
        {
            try { 
            if (eSerachPiernas != null)
                eSerachPiernas(null, null);
            }
            catch (Exception ex) { Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnBusqueda_Click", "Aviso"); }
        }
        protected void UpdatePanel1_Unload(object sender, EventArgs e)
        {
            MethodInfo methodInfo = typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Where(i => i.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel")).First();
            methodInfo.Invoke(ScriptManager.GetCurrent(Page),
                new object[] { sender as UpdatePanel });
        }
        protected void gvDetall_BeforePerformDataSelect(object sender, System.EventArgs e)
        {
            try { 
            ASPxGridView grid = sender as ASPxGridView;

            IDPierna = grid.GetMasterRowKeyValue().S().I();

            if (eSerachPiernaDetalle != null)
                eSerachPiernaDetalle(null, null);

            grid.DataSource = (DataTable)ViewState["Detalle"];
            }
            catch (Exception ex) { Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvDetall_BeforePerformDataSelect", "Aviso"); }
        }
        protected void gvDetall_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            try { 
            e.Editor.ReadOnly = false;
            if (e.Column.FieldName == "TipoPiernaDescripcion")
            {
                ASPxComboBox comboBox = e.Editor as ASPxComboBox;
                comboBox.DataSource = (DataTable)ViewState["listBox"];
                comboBox.ValueField = "IdTipoPierna";
                comboBox.ValueType = typeof(Int32);
                comboBox.TextField = "TipoPiernaDescripcion";
                comboBox.DataBindItems();
            }
            if (e.Column.FieldName == "AeronaveSerie")
            { e.Editor.ReadOnly = true; }
            if (e.Column.FieldName == "AeronaveMatricula")
            { e.Editor.ReadOnly = true; }
            if (e.Column.FieldName == "Origen")
            { e.Editor.ReadOnly = true; }
            if (e.Column.FieldName == "Destino")
            { e.Editor.ReadOnly = true; }
            if (e.Column.FieldName == "CantPax")
            { e.Editor.ReadOnly = true; }
            if (e.Column.FieldName == "OrigenVuelo")
            { e.Editor.ReadOnly = true; }
            if (e.Column.FieldName == "OrigenCalzo")
            { e.Editor.ReadOnly = true; }
            if (e.Column.FieldName == "DestinoVuelo")
            { e.Editor.ReadOnly = true; }
            if (e.Column.FieldName == "TripNum")
            { e.Editor.ReadOnly = true; }
            if (e.Column.FieldName == "FolioReal")
            { e.Editor.ReadOnly = true; }
            }
            catch (Exception ex) { Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvDetall_CellEditorInitialize", "Aviso"); }
        }
        protected void gvDetall_StartRowEditing(object sender, ASPxStartRowEditingEventArgs e)
        {

        }
        protected void gvDetall_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
        {
            try { 
            IDBitacora = e.Keys[0].S().I();
            IDPierna = e.NewValues["TipoPiernaDescripcion"].S().I();
            if (eSaveObj != null)
                eSaveObj(null, null);

            if (eSerachPiernas != null)
                eSerachPiernas(null, null);

            e.Cancel = true;
            ASPxGridView grid = sender as ASPxGridView;
            grid.CancelEdit();
            }
            catch (Exception ex) { Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvDetall_RowUpdating", "Aviso"); }
        }
        #endregion
        
        #region METODOS
        protected void CancelEditing(CancelEventArgs e)
        {
            try
            {
                e.Cancel = true;
                //gvDetall.CancelEdit();
            }
            catch (Exception x) { throw x; }
        }
        protected void Valores()
        {
            try
            {
                ASPxListBox lstbx = (ASPxListBox)ddeTipoPierna.FindControl("listBox");
                foreach (var x in lstbx.SelectedValues)
                {
                    sValores += x.S() + ",";
                }
            }
            catch (Exception x) { throw x; }
        }
        public void CargaGrid(DataTable DT)
        {
            try
            {
                gvPiernas.DataSource = DT;
                gvPiernas.DataBind();

                ViewState["gvPiernas"] = DT;
            }
            catch (Exception x) { throw x; }
        }
        protected void CargaGrid()
        {
            try
            {
                DataTable DT = (DataTable)ViewState["gvPiernas"];
                if (DT != null && DT.Rows.Count > 0)
                {
                    gvPiernas.DataSource = DT;
                    gvPiernas.DataBind();
                }
            }
            catch (Exception x) { throw x; }
        }
        public void CargaDDl(DataTable DT)
        {
            try
            {
                ASPxListBox lstbx = (ASPxListBox)ddeTipoPierna.FindControl("listBox");

                lstbx.DataSource = DT;
                lstbx.TextField = "TipoPiernaDescripcion";
                lstbx.ValueField = "IdTipoPierna";
                lstbx.DataBind();
                ViewState["listBox"] = DT;
            }
            catch (Exception z) { throw z; }
        }
        public void MostrarMensaje(string sMensaje, string sCaption)
        {
            mpeMensaje.ShowMessage(sMensaje, sCaption);
        }
        public void CargaGridDetalle(DataTable DT)
        {
            try
            {
                ViewState["Detalle"] = DT;
            }
            catch (Exception x) { throw x; }
        }
        #endregion

        #region "Vars y Propiedades"
        private const string sClase = "frmAnalisisFerrys.aspx.cs";
        private const string sPagina = "frmAnalisisFerrys.aspx";
        public string sValores = string.Empty;
        

        AnalisisFerrys_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eSerachPiernas;
        public event EventHandler eSerachPiernaDetalle;

        public object[] oArrFiltros
        {
            get
            {
                return new object[] { "@Status", 1};
            }
        }
        public object[] oArrFiltrosPierna {
            
            get
            {

                Valores();
                return new object[] { "@IdTipoPierna", sValores+"0",
                                      "@Fecha", deFechaIni.Text == "" ? System.DateTime.Now.ToString("MM-dd-yyyy") : deFechaIni.Date.ToString("MM-dd-yyyy"),
                                    };
            }
        }
        public object[] oArrFiltrosPiernaDetalle {
                get {
                    return new object[] { "@IdTipoPierna", IDPierna,
                                          "@Fecha", deFechaIni.Text == "" ? System.DateTime.Now.ToString("MM-dd-yyyy") : deFechaIni.Date.ToString("MM-dd-yyyy"),
                                        };
                    }
        }
        public object[] oArrParametrosUpdate
        {
            get
            {
                return new object[] { "@IdBitacora",  IDBitacora,
                                      "@IdTipoPierna",IDPierna,
                                        };
            }
        }
        
        public int IDPierna { set { ViewState["IDPierna"] = value; }
            get { return ViewState["IDPierna"].S().I(); }
        }
        public int IDBitacora
        {
            set { ViewState["IDBitacora"] = value; }
            get { return ViewState["IDBitacora"].S().I(); }
        }
        #endregion

    }
}