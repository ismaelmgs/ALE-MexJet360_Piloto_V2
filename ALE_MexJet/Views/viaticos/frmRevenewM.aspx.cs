using ALE_MexJet.DomainModel;
using ALE_MexJet.Interfaces;
using ALE_MexJet.Objetos;
using ALE_MexJet.Presenter;
using DevExpress.Web;
using DevExpress.Web.Bootstrap;
using NucleoBase.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ALE_MexJet.Views.viaticos
{
    public partial class frmRevenewM : System.Web.UI.Page, IViewRevenewM
    {
        #region EVENTOS

        protected void Page_Init(object sender, EventArgs e)
        {
            bool fu = fluArchivo.HasFile;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            

            oPresenter = new RevenewM_Presenter(this, new DBRevenewM());
            gvConceptos.SettingsPager.Position = PagerPosition.TopAndBottom;
            gvConceptos.SettingsPager.ShowDisabledButtons = true;
            gvConceptos.SettingsPager.ShowNumericButtons = true;
            gvConceptos.SettingsPager.ShowSeparators = true;
            gvConceptos.SettingsPager.Summary.Visible = true;
            gvConceptos.SettingsPager.PageSizeItemSettings.Visible = true;
            gvConceptos.SettingsPager.PageSizeItemSettings.Position = PagerPageSizePosition.Right;
            gvConceptos.SettingsText.SearchPanelEditorNullText = "Ingresa la información a buscar:";

            gvParametros.SettingsPager.Position = PagerPosition.TopAndBottom;
            gvParametros.SettingsPager.ShowDisabledButtons = true;
            gvParametros.SettingsPager.ShowNumericButtons = true;
            gvParametros.SettingsPager.ShowSeparators = true;
            gvParametros.SettingsPager.Summary.Visible = true;
            gvParametros.SettingsPager.PageSizeItemSettings.Visible = true;
            gvParametros.SettingsPager.PageSizeItemSettings.Position = PagerPageSizePosition.Right;
            gvParametros.SettingsText.SearchPanelEditorNullText = "Ingresa la información a buscar:";

            //gvParametrosAdicionales.SettingsPager.Position = PagerPosition.TopAndBottom;
            //gvParametrosAdicionales.SettingsPager.ShowDisabledButtons = true;
            //gvParametrosAdicionales.SettingsPager.ShowNumericButtons = true;
            //gvParametrosAdicionales.SettingsPager.ShowSeparators = true;
            //gvParametrosAdicionales.SettingsPager.Summary.Visible = true;
            //gvParametrosAdicionales.SettingsPager.PageSizeItemSettings.Visible = true;
            //gvParametrosAdicionales.SettingsPager.PageSizeItemSettings.Position = PagerPageSizePosition.Right;
            //gvParametrosAdicionales.SettingsText.SearchPanelEditorNullText = "Ingresa la información a buscar:";

            if (!IsPostBack)
            {
                if (eSearchObj != null)
                    eSearchObj(sender, e);
            }
        }
        protected void btnActualizarConceptos_Click(object sender, EventArgs e)
        {
            try
            {
                dtConfigConceptos = null;
                DataTable dt = GetTable(gvConceptos);
                if (dt != null && dt.Rows.Count > 0)
                {
                    dtConfigConceptos = dt;
                    //if (eSaveObj != null)
                    //    eSaveObj(sender, e);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gvConceptos_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
        {
            try
            {
                if (e.CommandArgs.CommandName.S() == "Actualiza")
                {
                    LimpiarFormulario();
                    int index = e.VisibleIndex.I();
                    int iIdConcepto = gvConceptos.GetRowValues(index, "IdConcepto").S().I();

                    string[] fieldValues = { "Concepto", "HoraIni", "HoraFin", "MontoMXN", "MontoUSD", "IdConcepto" };
                    object obj = gvConceptos.GetRowValues(index, fieldValues);
                    object[] oB = (object[])obj;

                    string sIdConcepto = oB[5].S();
                    string sDesConcepto = oB[0].S();
                    string sHoraInicial = oB[1].S();
                    string sHoraFinal = oB[2].S();
                    string sMontoMXN = oB[3].S();
                    string sMontoUSD = oB[4].S();

                    txtHorarioInicial.Text = sHoraInicial;
                    txtHorarioFinal.Text = sHoraFinal;
                    txtMontoMXN.Text = sMontoMXN.Replace("$", "");
                    txtMontoUSD.Text = sMontoUSD.Replace("$", "");
                    hdnIdConcepto.Value = sIdConcepto;
                    readConcepto.Text = sDesConcepto;

                    //ClientScript.RegisterStartupScript(this.GetType(), "alert", "ShowPopup();", true);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "MyScript", "ShowPopup();", true);
                    this.lblTitulo.Text = "Actualización de Conceptos";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnGuardarConcepto_Click(object sender, EventArgs e)
        {
            try
            {
                if (eSaveObj != null)
                    eSaveObj(sender, e);

                if (sOk == "1")
                {
                    //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "MyScript", "ShowPopup();", true);
                    if (eSearchObj != null)
                        eSearchObj(sender, e);

                    upaConceptos.Update();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnGuardarParametro_Click(object sender, EventArgs e)
        {
            try
            {
                if (eSaveParametro != null)
                    eSaveParametro(sender, e);

                if (sOk == "1")
                {
                    if (eSearchObj != null)
                        eSearchObj(sender, e);

                    upaParametros.Update();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gvParametros_RowCommand1(object sender, ASPxGridViewRowCommandEventArgs e)
        {
            try
            {
                if (e.CommandArgs.CommandName.S() == "Actualiza")
                {
                    LimpiarFormulario();
                    int index = e.VisibleIndex.I();
                    int iIdParametro = gvParametros.GetRowValues(index, "IdParametro").S().I();
                    string[] fieldValues = { "Clave", "Descripcion", "Valor" };
                    object obj = gvParametros.GetRowValues(index, fieldValues);
                    object[] oB = (object[])obj;
                    if (oB.Length > 0)
                    {
                        string sClave = string.Empty;
                        string sDescripcion = string.Empty;
                        string sValor = string.Empty;

                        sClave = oB[0].S();
                        sDescripcion = oB[1].S();
                        sValor = oB[2].S();

                        txtValor.Text = sValor;
                        readDesParametro.Text = sDescripcion;
                        hdnIdParametro.Value = iIdParametro.S();

                        //ClientScript.RegisterStartupScript(this.GetType(), "alert", "ShowPopup();", true);
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "MyScript", "ShowPopupParam();", true);
                        this.TituloParam.Text = "Actualización de Parametro";
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gvParametrosAdicionales_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
        {
            try
            {
                if (e.CommandArgs.CommandName.S() == "Actualiza")
                {
                    LimpiarFormulario();
                    iOpcion = 2;
                    int index = e.VisibleIndex.I();
                    int iIdParametro = gvParametrosAdicionales.GetRowValues(index, "IdParametro").S().I();
                    string[] fieldValues = { "Clave", "Descripcion", "Valor" };
                    object obj = gvParametrosAdicionales.GetRowValues(index, fieldValues);
                    object[] oB = (object[])obj;
                    if (oB.Length > 0)
                    {
                        string sClave = string.Empty;
                        string sDescripcion = string.Empty;
                        string sValor = string.Empty;

                        sClave = oB[0].S();
                        sDescripcion = oB[1].S();
                        sValor = oB[2].S();

                        txtClave.Text = sClave;
                        txtValorParaAd.Text = sValor;
                        txtDescripcionParaAd.Text = sDescripcion;
                        hdnIdParaAd.Value = iIdParametro.S();
                        txtClave.Enabled = false;

                        //ClientScript.RegisterStartupScript(this.GetType(), "alert", "ShowPopup();", true);
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "MyScript", "ShowPopupParamAd();", true);
                        this.TituloParam.Text = "Actualización de Parametro";
                        btnGuardarParaAd.Text = "Actualizar";
                    }
                }
                else if (e.CommandArgs.CommandName.S() == "Eliminar")
                {
                    iOpcion = 3;
                    int index = e.VisibleIndex.I();
                    int iIdParametro = gvParametrosAdicionales.GetRowValues(index, "IdParametro").S().I();
                    hdnIdParaAd.Value = iIdParametro.S();

                    if (eSaveParametroAd != null)
                        eSaveParametroAd(sender, e);

                    if (sOk == "1")
                    {
                        if (eSearchObj != null)
                            eSearchObj(sender, e);

                        upaGeneral.Update();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnNuevoParametroAdicional_Click(object sender, EventArgs e)
        {
            try
            {
                iOpcion = 1;
                LimpiarFormulario();
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "MyScript", "ShowPopupParamAd();", true);
                this.lblTituloAdicionales.Text = "Agregar parametro adicional";
                btnGuardarParaAd.Text = "Guardar";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnGuardarParaAd_Click(object sender, EventArgs e)
        {
            try
            {
                if (eSaveParametroAd != null)
                    eSaveParametroAd(sender, e);

                if (!string.IsNullOrEmpty(sOk))
                {
                    if (eSearchObj != null)
                        eSearchObj(sender, e);

                    upaGeneral.Update();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region MÉTODOS

        public void LoadConfiguracion(DataSet ds)
        {
            try
            {
                dtConceptos = null;
                dtParametros = null;

                if (ds.Tables.Count > 0) 
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        dtConceptos = ds.Tables[0];
                        gvConceptos.DataSource = dtConceptos;
                        gvConceptos.DataBind();

                        //gvConceptos.FooterRow.Cells[2].Text = "TOTAL";
                        //gvConceptos.FooterRow.Cells[2].Font.Bold = true;
                    }

                    if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                    {
                        dtParametros = ds.Tables[1];
                        gvParametros.DataSource = dtParametros;
                        gvParametros.DataBind();
                    }

                    if (ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
                    {
                        //dtParametros = ds.Tables[1];
                        gvParametrosAdicionales.DataSource = ds.Tables[2];
                        gvParametrosAdicionales.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetTable(BootstrapGridView gv)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("IdConcepto");
                dt.Columns.Add("DesConcepto");
                dt.Columns.Add("HoraInicial");
                dt.Columns.Add("HoraFinal");
                dt.Columns.Add("MontoMXN");
                dt.Columns.Add("MontoUSD");
                dt.Columns.Add("Status");



                for (int i = 0; i < gv.VisibleRowCount; i++)
                {
                    string[] fieldValues = { "Concepto", "HoraIni", "HoraFin", "MontoMXN", "MontoUSD", "IdConcepto" };
                    object obj = gv.GetRowValues(i, fieldValues);
                    object[] oB = (object[])obj;


                    //GridViewDataColumn colItem = (GridViewDataColumn)gv.Columns[1];

                    //ASPxTextBox txtHoraI = gv.FindRowCellTemplateControl(0, gv.Columns[1] as GridViewDataColumn, "txtHoraIni") as ASPxTextBox;

                    //int vi = gv.VisibleStartIndex + i;
                    //ASPxTextBox txtBox1 = (ASPxTextBox)gv.FindRowCellTemplateControl(vi, (GridViewDataColumn)gv.Columns[1], "txtHoraIni");

                    //string AA = txtHoraI.Text.ToString();
                    //string BB = txtHoraInicial.Text;
                    //string CC = txtHoraInicial2.Text;


                    //TextBox txtMontoMXN = (TextBox)gv.FindDetailRowTemplateControl(i, "txtMontoMXN");
                    //TextBox txtMontoUSD = (TextBox)gv.FindDetailRowTemplateControl(i, "txtMontoUSD");

                    DataRow dr = dt.NewRow();
                    dr["IdConcepto"] = oB[5].S();
                    dr["DesConcepto"] = oB[0].S();
                    dr["HoraInicial"] = oB[1].S();
                    dr["HoraFinal"] = oB[2].S();
                    dr["MontoMXN"] = oB[3].S();
                    dr["MontoUSD"] = oB[4].S();
                    dr["Status"] = 1;
                    dt.Rows.Add(dr);
                }
                dt.AcceptChanges();
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public void LimpiarFormulario()
        {
            txtHorarioInicial.Text = string.Empty;
            txtHorarioFinal.Text = string.Empty;
            txtMontoMXN.Text = string.Empty;
            txtMontoUSD.Text = string.Empty;
            txtValor.Text = string.Empty;
            hdnIdConcepto.Value = string.Empty;
            hdnIdParametro.Value = string.Empty;
            readDesParametro.Text = string.Empty;
            readConcepto.Text = string.Empty;

            hdnIdParaAd.Value = string.Empty;
            txtClave.Text = string.Empty;
            txtDescripcionParaAd.Text = string.Empty;
            txtValorParaAd.Text = string.Empty;
            txtClave.Enabled = true;
            txtDescripcionParaAd.Enabled = true;
        }

        #endregion

        #region VARIABLES Y PROPIEDADES
        RevenewM_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;

        public event EventHandler eSaveParametro;
        public event EventHandler eSaveParametroAd;
        public event EventHandler eSearchCuenta;

        public DataTable dtConceptos
        {
            get { return (DataTable)ViewState["VSConceptos"]; }
            set { ViewState["VSConceptos"] = value; }
        }
        public DataTable dtParametros
        {
            get { return (DataTable)ViewState["VSParametros"]; }
            set { ViewState["VSParametros"] = value; }
        }
        public DataTable dtConfigConceptos
        {
            get { return (DataTable)ViewState["VSConfigConceptos"]; }
            set { ViewState["VSConfigConceptos"] = value; }
        }
        public string sOk
        {
            get { return (string)ViewState["VSOk"]; }
            set { ViewState["VSOk"] = value; }
        }
        public int iOpcion
        {
            get { return (int)ViewState["VSOpcion"]; }
            set { ViewState["VSOpcion"] = value; }
        }
        public Concepto oC
        {
            get
            {
                Concepto oCon = new Concepto();
                oCon.IIdConcepto = hdnIdConcepto.Value.I();
                oCon.SHorarioInicial = txtHorarioInicial.Text.S();
                oCon.SHorarioFinal = txtHorarioFinal.Text.S();
                oCon.DMontoMXN = txtMontoMXN.Text.D();
                oCon.DMontoUSD = txtMontoUSD.Text.D();
                return oCon;
            }
        }

        public ParametrosGrales oP
        {
            get
            {
                ParametrosGrales oPar = new ParametrosGrales();
                oPar.IIdParametro = hdnIdParametro.Value.I();
                oPar.SDescripcion = readDesParametro.Text.S();
                oPar.SValor = txtValor.Text.S();
                oPar.SUsuario = ((UserIdentity)Session["UserIdentity"]).sUsuario;
                return oPar;
            }
        }

        public ParametrosAdicionales oPA
        {
            get
            {
                ParametrosAdicionales oPA = new ParametrosAdicionales();
                oPA.IIdParametro = hdnIdParaAd.Value.I();
                oPA.SClave = txtClave.Text.S();
                oPA.SDescripcion = txtDescripcionParaAd.Text.S();
                oPA.SValor = txtValorParaAd.Text.S();
                oPA.IOpcion = iOpcion;
                oPA.SUsuario = ((UserIdentity)Session["UserIdentity"]).sUsuario;
                return oPA;
            }
        }

        public DataTable dtCambios
        {
            get { return (System.Data.DataTable)ViewState["VSCambios"]; }
            set { ViewState["VSCambios"] = value; }
        }
        public string sNumCuenta
        {
            set { ViewState["VSNumCuenta"] = value; }
            get { return (string)ViewState["VSNumCuenta"]; }
        }
        public DataTable dtInfoCuenta
        {
            get { return (System.Data.DataTable)ViewState["VSInfoCuenta"]; }
            set { ViewState["VSInfoCuenta"] = value; }
        }
        #endregion

        protected void gvCargaCuentas_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {

        }

        protected void btnCargarArchivo_Click(object sender, EventArgs e)
        {
            try
            {
                msgError.Visible = false;
                lblError.Text = string.Empty;
                CargarArchivo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void CargarArchivo()
        {
            try
            {
                if (fluArchivo.HasFile == true)
                {
                    string FileName = Path.GetFileName(fluArchivo.PostedFile.FileName);
                    string Extension = Path.GetExtension(fluArchivo.PostedFile.FileName);
                    string FolderPath = "~/Files/";
                    string FilePath = Server.MapPath(FolderPath + FileName);

                    if (Extension == ".xls" || Extension == ".XLS" || Extension == ".xlsx" || Extension == ".XLSX")
                    {
                        if (File.Exists(FilePath))
                            File.Delete(FilePath);

                        if (!File.Exists(FilePath))
                            fluArchivo.SaveAs(FilePath);

                        GetDataTable(FilePath, Extension);
                        File.Delete(FilePath);
                    }
                    else
                    {
                        msgError.Visible = true;
                        lblError.Text = "El tipo de archivo a procesar no es válido, se recomienda subir archivos con extensión \"xls\".";
                    }
                }
                else
                {
                    msgError.Visible = true;
                    lblError.Text = "No ha seleccionado archivo a procesar, favor de verificar.";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void GetDataTable(string FilePath, string Extension)
        {
            try
            {
                msgError.Visible = false;
                lblError.Text = string.Empty;
                string conStr = "";
                string SheetName = string.Empty;
                string sQuery = string.Empty;
                bool bValidExcel = false;
                DataTable dtDatos = new DataTable();

                switch (Extension)
                {
                    case ".xls": //Excel 97-03
                        conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FilePath + ";Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1\";";
                        break;
                    case ".xlsx": //Excel 07, 2013, etc
                        conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FilePath + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";";
                        break;
                    case ".XLS": //Excel 97-03
                        conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FilePath + ";Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1\";";
                        break;
                    case ".XLSX": //Excel 07, 2013, etc
                        conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FilePath + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";";
                        break;
                }

                if (!string.IsNullOrEmpty(conStr))
                {
                    conStr = String.Format(conStr, FilePath);
                    OleDbConnection connExcel = new OleDbConnection(conStr);
                    OleDbCommand cmdExcel = new OleDbCommand();
                    OleDbDataAdapter oda = new OleDbDataAdapter();
                    System.Data.DataTable dt = new System.Data.DataTable();
                    cmdExcel.Connection = connExcel;

                    //Obtiene el nombre de la primera hoja
                    connExcel.Open();
                    System.Data.DataTable dtExcelSchema;
                    dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                    connExcel.Close();

                    sQuery = "SELECT * FROM [" + SheetName + "]";
                    cmdExcel.CommandText = sQuery;
                    oda.SelectCommand = cmdExcel;
                    oda.Fill(dt);
                    dtDatos = FormatData(dt);
                    bValidExcel = ValidarArchivo(dtDatos, "1");

                    if (bValidExcel)
                    {
                        gvCargaCuentas.DataSource = dtDatos;
                        gvCargaCuentas.DataBind();
                        Label4.Visible = true;
                        lblNota.Visible = true;
                        ControlConsultaInfoCuenta();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected DataTable FormatData(DataTable dt)
        {
            try
            {
                dt.Columns[0].ColumnName = "Titular";
                dt.Columns[1].ColumnName = "Cuenta";
                dt.Columns[2].ColumnName = "Tarjeta";
                dt.Columns[3].ColumnName = "EstadoCorte";
                dt.Columns[4].ColumnName = "CuartaLinea";

                dt.Columns.Add("CvePiloto");

                foreach (DataRow rw in dt.Rows)
                {
                    if (string.IsNullOrEmpty(rw["Titular"].S()) || rw["Titular"].S() == "")
                        rw.Delete();
                }

                dt.AcceptChanges();
                return dt;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool ValidarArchivo(DataTable dsFileExcel, string sLayout)
        {
            try
            {
                int iValidColumn;
                bool bValidExcel = false;
                string[] arrColumn1 = { "Titular", "Cuenta", "Tarjeta", "EstadoCorte", "CuartaLinea" };

                switch (sLayout)
                {
                    // Layout Cuentas
                    case "1":
                        ViewState["VSLayout"] = arrColumn1;
                        for (int i = 0; i < arrColumn1.Length; i++)
                        {
                            iValidColumn = dsFileExcel.Columns.IndexOf(arrColumn1[i]);

                            if (iValidColumn == -1)
                            {
                                bValidExcel = false;
                                break;
                            }
                            else
                                bValidExcel = true;
                        }
                        break;
                    default:
                        break;
                }

                return bValidExcel;
            }
            catch (Exception)
            {

                throw;
            }
        }
        protected void ControlConsultaInfoCuenta()
        {
            try
            {
                dtCambios = null;

                for (int i = 0; i < gvCargaCuentas.VisibleRowCount; i++)
                {
                    int index = i;
                    sNumCuenta = gvCargaCuentas.GetRowValues(index, "Cuenta").S();
                    string[] fieldValues = { "Titular", "Cuenta", "Tarjeta", "EstadoCorte", "CuartaLinea", "CvePiloto" };
                    object obj = gvCargaCuentas.GetRowValues(index, fieldValues);
                    object[] oB = (object[])obj;

                    if (oB.Length > 0)
                    {
                        string sTitular = string.Empty;
                        string sNoTarjeta = string.Empty;
                        string sEstado = string.Empty;
                        string sCuartaLin = string.Empty;

                        sTitular = HttpUtility.HtmlDecode(oB[0].S()).TrimEnd().TrimStart();
                        sNoTarjeta = oB[2].S();
                        sEstado = oB[3].S();
                        sCuartaLin = HttpUtility.HtmlDecode(oB[4].S());

                        var gvColumn = gvCargaCuentas.Columns[5] as GridViewDataColumn;

                        TextBox txtCvePiloto = (TextBox)gvCargaCuentas.FindRowCellTemplateControl(index, gvColumn, "txtCvePiloto");

                        if (eSearchCuenta != null)
                            eSearchCuenta(null, null);

                        if (dtInfoCuenta != null && dtInfoCuenta.Rows.Count > 0)
                        {
                            //    if (sTitular != dtInfoCuenta.Rows[0]["Titular"].S())
                            //    {
                            //        gvCargaCuentas.Rows[row.RowIndex].Cells[0].BackColor = Color.FromName("#ffb400");
                            //        gvCargaCuentas.Rows[row.RowIndex].Cells[0].ForeColor = Color.FromName("#ffffff");
                            //        gvCargaCuentas.Rows[row.RowIndex].Cells[0].ToolTip = "Nueva información a cargar";
                            //    }

                            //    if (sNoTarjeta != dtInfoCuenta.Rows[0]["NoTarjeta"].S())
                            //    {
                            //        gvCargaCuentas.Rows[row.RowIndex].Cells[2].BackColor = Color.FromName("#ffb400");
                            //        gvCargaCuentas.Rows[row.RowIndex].Cells[2].ForeColor = Color.FromName("#ffffff");
                            //        gvCargaCuentas.Rows[row.RowIndex].Cells[2].ToolTip = "Nueva información a cargar";
                            //    }

                            //    if (sEstado != dtInfoCuenta.Rows[0]["EstadoCorte"].S())
                            //    {
                            //        gvCargaCuentas.Rows[row.RowIndex].Cells[3].BackColor = Color.FromName("#ffb400");
                            //        gvCargaCuentas.Rows[row.RowIndex].Cells[3].ForeColor = Color.FromName("#ffffff");
                            //        gvCargaCuentas.Rows[row.RowIndex].Cells[3].ToolTip = "Nueva información a cargar";
                            //    }

                            if (sTitular != dtInfoCuenta.Rows[0]["Titular"].S() ||
                                sNoTarjeta != dtInfoCuenta.Rows[0]["NoTarjeta"].S() ||
                                sEstado != dtInfoCuenta.Rows[0]["EstadoCorte"].S())
                            {
                                CambiosEncontrados(dtInfoCuenta.Rows[0]["Titular"].S(), dtInfoCuenta.Rows[0]["Cuenta"].S(), dtInfoCuenta.Rows[0]["NoTarjeta"].S(), dtInfoCuenta.Rows[0]["EstadoCorte"].S(), dtInfoCuenta.Rows[0]["CuartaLinea"].S(), dtInfoCuenta.Rows[0]["CvePiloto"].S());
                            }


                            if (dtInfoCuenta != null && dtInfoCuenta.Rows.Count > 0)
                            {
                                txtCvePiloto.Text = dtInfoCuenta.Rows[0][0].S();
                            }
                        }
                        else
                        {
                            //if (!string.IsNullOrEmpty(sTitular) && !string.IsNullOrEmpty(sNumCuenta) && !string.IsNullOrEmpty(sNoTarjeta) && !string.IsNullOrEmpty(sEstado) && !string.IsNullOrEmpty(sCuartaLin))
                            //{
                            //    gvCargaCuentas.Rows[row.RowIndex].BackColor = Color.FromName("#ffb400");
                            //    gvCargaCuentas.Rows[row.RowIndex].ForeColor = Color.FromName("#ffffff");
                            //    gvCargaCuentas.Rows[row.RowIndex].ToolTip = "Nueva información a cargar";
                            //}
                            CambiosEncontrados(sTitular, sNumCuenta, sNoTarjeta, sEstado, sCuartaLin, txtCvePiloto.Text);
                        }
                    }
                }

                if (dtCambios != null && dtCambios.Rows.Count > 0)
                {
                    gvCambios.DataSource = dtCambios;
                    gvCambios.DataBind();
                    lblRegModificar.Visible = true;
                    msgWarning.Visible = true;
                    lblWarning.Text = "Le informamos que los datos mostrados han cambiado referente al nuevo archivo a cargar.";
                    Label4.Text = "Información a cargar";
                }
                else
                {
                    gvCambios.DataSource = dtCambios;
                    gvCambios.DataBind();
                    lblRegModificar.Visible = false;
                    msgWarning.Visible = false;
                    lblWarning.Visible = false;
                    lblNota.Visible = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LoadDatosCuenta(DataTable dt)
        {
            try
            {
                dtInfoCuenta = null;
                dtInfoCuenta = dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void CambiosEncontrados(string sTitular, string sCuenta, string sNoTar, string sEdo, string sCuarta, string sCve)
        {
            try
            {
                if (dtCambios == null)
                {
                    dtCambios = new DataTable();
                    dtCambios.Columns.Add("Titular");
                    dtCambios.Columns.Add("Cuenta");
                    dtCambios.Columns.Add("Tarjeta");
                    dtCambios.Columns.Add("EstadoCorte");
                    dtCambios.Columns.Add("CuartaLinea");
                    dtCambios.Columns.Add("CvePiloto");

                    DataRow dRow = dtCambios.NewRow();
                    dRow["Titular"] = sTitular;
                    dRow["Cuenta"] = sCuenta;
                    dRow["Tarjeta"] = sNoTar;
                    dRow["EstadoCorte"] = sEdo;
                    dRow["CuartaLinea"] = ScrubHtml(sCuarta);
                    dRow["CvePiloto"] = sCve;
                    dtCambios.Rows.Add(dRow);
                }
                else
                {
                    DataRow dRow = dtCambios.NewRow();
                    dRow["Titular"] = sTitular;
                    dRow["Cuenta"] = sCuenta;
                    dRow["Tarjeta"] = sNoTar;
                    dRow["EstadoCorte"] = sEdo;
                    dRow["CuartaLinea"] = ScrubHtml(sCuarta);
                    dRow["CvePiloto"] = sCve;
                    dtCambios.Rows.Add(dRow);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static string ScrubHtml(string value)
        {
            var step1 = Regex.Replace(value, @"<[^>]+>|&nbsp;", "").Trim();
            var step2 = Regex.Replace(step1, @"\s{2,}", " ");
            return step2;
        }
    }
}