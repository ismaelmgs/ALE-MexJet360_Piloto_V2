using ALE_MexJet.Clases;
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
using System.Reflection;

namespace ALE_MexJet.Views.CreditoCobranza
{
    public partial class frmFormulasRemision : System.Web.UI.Page, IViewFormulasRemision
    {
        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager Script = (ScriptManager) Master.FindControl("ScriptManager1");
            if (Script != null)
            {
                //Script.RegisterAsyncPostBackControl("");
            }

            try
            {
                oPresenter = new FormulasRemision_Presenter(this, new DBFormulas());

                if (!IsPostBack)
                {
                    iIdFormula = 0;

                    if (eSearchObj != null)
                        eSearchObj(sender, e);

                    if (eGetFactores != null)
                        eGetFactores(sender, e);

                    variablesUso = new List<useVariables>();

                    btnActualizar.Visible = false;
                    btnInsertar.Visible = false;

                    metodo = 0; // 0 = insertar, 1 = actualizar
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "Page_Load", "Aviso");
            }

        }

        protected void gvFormulas_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvFormulas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int iIndex = e.CommandArgument.S().I();
            iIdFormula = (int)gvFormulas.DataKeys[iIndex]["idFormula"];

            if (e.CommandName == "actualizar")
            {
                tbcodigo.Text = gvFormulas.Rows[iIndex].Cells[0].Text.S();
                tbFormula.Text = gvFormulas.Rows[iIndex].Cells[1].Text.S();
                tbdescripcion.Text = gvFormulas.Rows[iIndex].Cells[2].Text.S();

                metodo = 1;
                btnActualizar.Visible = true;

            }

            if (e.CommandName == "eliminar")
            {
                if (eDeleteObj != null)
                    eDeleteObj(sender, e);
            }

        }

        protected void btnInsertar_Click(object sender, EventArgs e)
        {
            try
            {
                if (eNewObj != null)
                    eNewObj(sender, e);

                tbFormula.Text = string.Empty;
                tbcodigo.Text = string.Empty;
                tbdescripcion.Text = string.Empty;

                btnInsertar.Visible = false;
                btnValidar.Visible = true;

                if (eSearchObj != null)
                    eSearchObj(sender, e);

                if (eGetFactores != null)
                    eGetFactores(sender, e);
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (eSaveObj != null)
                    eSaveObj(sender, e);

                tbFormula.Text = string.Empty;
                tbcodigo.Text = string.Empty;
                tbdescripcion.Text = string.Empty;

                btnActualizar.Visible = false;
                btnValidar.Visible = true;

                if (eSearchObj != null)
                    eSearchObj(sender, e);

                if (eGetFactores != null)
                    eGetFactores(sender, e);
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void ddlFactoresVariables_SelectedIndexChanged(object sender, EventArgs e)
        {
            var texto = ddlFactoresVariables.SelectedItem.Text;
            var valor = ddlFactoresVariables.SelectedItem.Value;

            tbFormula.Text += texto + " ";
            tbFormula.Focus();

            ddlFactoresVariables.SelectedIndex = 0;

            agregaVariable(texto,valor);
        }

        protected void ddlFactoresFijos_SelectedIndexChanged(object sender, EventArgs e)
        {
            var texto = ddlFactoresFijos.SelectedItem.Text;
            var valor = ddlFactoresFijos.SelectedItem.Value;

            tbFormula.Text += texto + " ";
            tbFormula.Focus();

            ddlFactoresFijos.SelectedIndex = 0;

            agregaVariable(texto, valor);
        }

        protected void btnValidar_Click(object sender, EventArgs e)
        {
            var arrayVar = tbFormula.Text.Split(' ');
            var formula = tbFormula.Text;

            if (metodo > 0)
            {
                foreach (DataRow item in dtFactoresFijos.Rows)
                {
                    formula = formula.Replace(item["Clave"].ToString(), item["Valor"].ToString());
                }

                foreach (DataRow item in dtFactoresVariables.Rows)
                {
                    formula = formula.Replace(item["Clave"].ToString(), item["Valor"].ToString());
                }
            }
            else
            {
                foreach (useVariables item in variablesUso)
                {
                    formula = formula.Replace(item.sDescripcion, item.sValor);
                }
            }
            
            var aprueba = EvalExpression(formula);


            if (aprueba)
            {
                if(metodo > 0)
                {
                    btnActualizar.Visible = true;
                }
                else
                {
                    btnInsertar.Visible = true;
                }

                btnValidar.Visible = false;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            tbcodigo.Text = "";
            tbdescripcion.Text = "";
            tbFormula.Text = "";

            btnValidar.Visible = true;
            btnInsertar.Visible = false;
            btnActualizar.Visible = false;

        }

        protected void Unnamed_Unload(object sender, EventArgs e)
        {
            try
            {
                MethodInfo methodInfo = typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Where(i => i.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel")).First();
                methodInfo.Invoke(ScriptManager.GetCurrent(Page),
                    new object[] { sender as UpdatePanel });
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "UpdatePanel1_Unload", "Aviso");
            }
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            iIdFormula = 0;
            tbFormula.Text = string.Empty;
            tbcodigo.Text = string.Empty;
            tbdescripcion.Text = string.Empty;
        }

        #endregion

        #region METODOS

        public void LoadObjects()
        {
            try
            {
                ddlFactoresFijos.Items.Clear();
                ddlFactoresVariables.Items.Clear();

                // campo por default para ambas listas
                ListItem it = new ListItem();
                it.Text = "Selecciona Factor";
                it.Value = "";
                ddlFactoresFijos.Items.Add(it);
                ddlFactoresVariables.Items.Add(it);


                foreach (DataRow item in dtFactoresFijos.Rows)
                {
                    ListItem li = new ListItem();
                    li.Text = item["Clave"].ToString();
                    li.Value = item["Valor"].ToString();
                    ddlFactoresFijos.Items.Add(li);
                }

                foreach (DataRow item in dtFactoresVariables.Rows)
                {
                    ListItem li = new ListItem();
                    li.Text = item["Clave"].ToString();
                    li.Value = item["Valor"].ToString();
                    ddlFactoresVariables.Items.Add(li);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LoadGridView()
        {
            gvFormulas.DataSource = dtFormulasRemision;
            gvFormulas.DataBind();
        }

        public void MostrarMensaje(string sMensaje, string sCaption)
        {
            mpeMensaje.ShowMessage(sMensaje, sCaption);
        }

        private bool EvalExpression(string expression)
        {

            try
            {
                //Creo un DataTable
                System.Data.DataTable table = new System.Data.DataTable();
                //Realizo el cálculo..
                object result = table.Compute(expression, string.Empty);
                //Lo devuelvo convertido a Double

                return true;
            }
            catch(Exception e)
            {
                return false;
            }

        }

        private void agregaVariable(string texto, string valor)
        {
            ddlFactoresVariables.SelectedIndex = 0;
            useVariables v = new useVariables();
            v.sValor = valor;
            v.sDescripcion = texto;


            variablesUso.Add(v);
        }

        #endregion

        #region VARIABLES Y PROPIEDADES

        private const string sClase = "frmGRemision.aspx.cs";
        private const string sPagina = "frmGRemision.aspx";

        FormulasRemision_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eGetFactores;

        public List<useVariables> variablesUso
        {
            get
            {
                return (List<useVariables>)ViewState["lvariablesUso"];
            }
            set { ViewState["lvariablesUso"] = value; }
        }

        public DataTable dtFactoresFijos
        {
            get { return (DataTable)ViewState["VSFactoresFijos"]; }
            set { ViewState["VSFactoresFijos"] = value; }
        }

        public DataTable dtFactoresVariables
        {
            get { return (DataTable)ViewState["VSFactoresVariables"]; }
            set { ViewState["VSFactoresVariables"] = value; }
        }

        public DataTable dtFormulasRemision
        {
            get { return (DataTable)ViewState["VSFactoresFijos"]; }
            set { ViewState["VSFactoresFijos"] = value; }
        }

        public int iIdFormula
        {
            get { return (int)ViewState["VSIdFormula"]; }
            set { ViewState["VSIdFormula"] = value; }
        }

        public int metodo
        {
            get { return (int)ViewState["VSmetodo"]; }
            set { ViewState["VSmetodo"] = value; }
        }

        public FormulaRem oFormula
        {
            get
            {
                FormulaRem oForm = new FormulaRem();
                oForm.iId = iIdFormula;
                oForm.sFormula = tbFormula.Text.S();
                oForm.sDescripcion = tbdescripcion.Text.S();
                oForm.CodigoF = tbcodigo.Text.S();

                return oForm;
            }
        }
        #endregion

        
    }
}