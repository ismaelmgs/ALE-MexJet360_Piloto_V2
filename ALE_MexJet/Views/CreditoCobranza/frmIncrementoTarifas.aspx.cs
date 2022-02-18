using ALE_MexJet.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NucleoBase.Core;
using System.Data;
using ALE_MexJet.Interfaces;
using ALE_MexJet.DomainModel;
using ALE_MexJet.Presenter;
using ALE_MexJet.Objetos;
using DevExpress.Web;
using DevExpress.XtraPrinting;
using DevExpress.Export;

namespace ALE_MexJet.Views.CreditoCobranza
{
    public partial class frmIncrementoTarifas : System.Web.UI.Page, IViewIncrementoTarifas
    {
        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                oPresenter = new IncrementoTarifas_Presenter(this, new DBIncrementoTarifas());
                gvIncrementoTarifas.SettingsPager.Position = PagerPosition.TopAndBottom;
                gvIncrementoTarifas.SettingsPager.ShowDisabledButtons = true;
                gvIncrementoTarifas.SettingsPager.ShowNumericButtons = true;
                gvIncrementoTarifas.SettingsPager.ShowSeparators = true;
                gvIncrementoTarifas.SettingsPager.Summary.Visible = true;
                gvIncrementoTarifas.SettingsPager.PageSizeItemSettings.Visible = true;
                gvIncrementoTarifas.SettingsPager.PageSizeItemSettings.Position = PagerPageSizePosition.Right;
                gvIncrementoTarifas.SettingsText.SearchPanelEditorNullText = "Ingrese la información a buscar";


                if (!IsPostBack)
                {
                    LoadMeses();
                }
                else 
                {
                    if (rblIncrementoTarifa.Items[0].Selected == true)
                    {
                        if (eGetIncrementos != null)
                            eGetIncrementos(sender, e);
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "Page_Load", "Aviso");
            }
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
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "Unnamed_Unload", "Aviso");
            }
        }
        protected void rblIncrementoTarifa_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (rblIncrementoTarifa.Value.S() == "2")
                {
                    lblMesIncremento.Visible = true;
                    ddlMesIncremento.Visible = true;
                }
                else
                {
                    lblMesIncremento.Visible = false;
                    ddlMesIncremento.Visible = false;
                }
                gvIncrementoTarifas.DataSource = null;
                gvIncrementoTarifas.DataBind();
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "rblIncrementoTarifa_ValueChanged", "Aviso");
            }
        }
        protected void btnCalcular_Click(object sender, EventArgs e)
        {
            try
            {
                if (eGetIncrementos != null)
                    eGetIncrementos(sender, e);

                gvIncrementoTarifas.Selection.SelectAll();
            }
            catch(Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnCalcular_Click", "Aviso");
            }
        }

        protected void btnAplicarTarifas_Click(object sender, EventArgs e)
        {
            try
            {                
                popupMsgConfirmacion.ShowOnPageLoad = true;
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnAplicarTarifas_Click", "Aviso");
            }
        }

        protected void btnAceptarIncrementoTarifas_Click(object sender, EventArgs e)
        {
            try
            {                
                if (eGetAplicaIncrementos != null)
                    eGetAplicaIncrementos(sender, EventArgs.Empty);                

                if (eGetIncrementos != null)
                    eGetIncrementos(sender, e);                

                popupTarifaAplicada.ShowOnPageLoad = true;                
                
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnAceptarIncrementoTarifas_Click", "Aviso");
            }
        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                Exporter.WriteXlsToResponse(new XlsExportOptionsEx() { ExportType = ExportType.DataAware });
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnExportar_Click", "Aviso");
            }
        }

        #endregion


        #region METODOS
        private void LoadMeses()
        {
            try
            {
                DataTable dtMeses = new DataTable();
                dtMeses.Columns.Add("IdMes", typeof(int));
                dtMeses.Columns.Add("Mes");

                DataRow row1 = dtMeses.NewRow();
                row1["IdMes"] = 1;
                row1["Mes"] = "Enero";

                DataRow row2 = dtMeses.NewRow();
                row2["IdMes"] = 2;
                row2["Mes"] = "Febrero";

                DataRow row3 = dtMeses.NewRow();
                row3["IdMes"] = 3;
                row3["Mes"] = "Marzo";

                DataRow row4 = dtMeses.NewRow();
                row4["IdMes"] = 4;
                row4["Mes"] = "Abril";

                DataRow row5 = dtMeses.NewRow();
                row5["IdMes"] = 5;
                row5["Mes"] = "Mayo";

                DataRow row6 = dtMeses.NewRow();
                row6["IdMes"] = 6;
                row6["Mes"] = "Junio";

                DataRow row7 = dtMeses.NewRow();
                row7["IdMes"] = 7;
                row7["Mes"] = "Julio";

                DataRow row8 = dtMeses.NewRow();
                row8["IdMes"] = 8;
                row8["Mes"] = "Agosto";

                DataRow row9 = dtMeses.NewRow();
                row9["IdMes"] = 9;
                row9["Mes"] = "Septiembre";

                DataRow row10 = dtMeses.NewRow();
                row10["IdMes"] = 10;
                row10["Mes"] = "Octubre";

                DataRow row11 = dtMeses.NewRow();
                row11["IdMes"] = 11;
                row11["Mes"] = "Noviembre";

                DataRow row12 = dtMeses.NewRow();
                row12["IdMes"] = 12;
                row12["Mes"] = "Diciembre";

                dtMeses.Rows.Add(row1);
                dtMeses.Rows.Add(row2);
                dtMeses.Rows.Add(row3);
                dtMeses.Rows.Add(row4);
                dtMeses.Rows.Add(row5);
                dtMeses.Rows.Add(row6);
                dtMeses.Rows.Add(row7);
                dtMeses.Rows.Add(row8);
                dtMeses.Rows.Add(row9);
                dtMeses.Rows.Add(row10);
                dtMeses.Rows.Add(row11);
                dtMeses.Rows.Add(row12);

                ddlMesIncremento.DataSource = dtMeses;
                ddlMesIncremento.ValueField="IdMes";
                ddlMesIncremento.TextField="Mes";
                ddlMesIncremento.DataBind();

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public void LoadIncrementoTarifas(DataTable dtCal)
        {
            try
            {
                gvIncrementoTarifas.DataSource = null;
                gvIncrementoTarifas.DataSource = dtCal;
                gvIncrementoTarifas.DataBind();
                ViewState["dtIncrementoT"] = dtCal; 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region VARIABLES Y PROPIEDADES

        IncrementoTarifas_Presenter oPresenter;
        private const string sClase = "frmIncrementoTarifas.aspx.cs";
        private const string sPagina = "frmIncrementoTarifas.aspx";

        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eGetIncrementos;
        public event EventHandler eGetAplicaIncrementos;

        public object[] oArrFiltros
        {
            get
            {
                return new object[] { 
                                        "@Tipo", rblIncrementoTarifa.Value.S().I(),
                                        "@Mes", ddlMesIncremento.Value.S().I()
                                    };
            }
        }

        public List<IncrementoTarifas> oTarifa
        {
            get
            {
                List<object> fieldValues = gvIncrementoTarifas.GetSelectedFieldValues(new string[] { "Id", "IdContrato", "IdConcepto" });
                List<IncrementoTarifas> lstIncrementoTarifa = new List<IncrementoTarifas>();
                DataTable dt = (DataTable)ViewState["dtIncrementoT"];

                foreach (DataRow row in dt.Rows)
                {
                    foreach (object[] item in fieldValues)
                    {
                        if (row[0].S() == item[0].S())
                        {
                            lstIncrementoTarifa.Add(new IncrementoTarifas
                            {
                                iIdContrato = row[1].S().I(),
                                iIdConcepto = row[2].S().I(),
                                dImporteOri = row[6].S().D(),
                                sInflacionDesc = row[7].S(),
                                dPorcentaje = row[8].S().D(),
                                dMasPuntos = row[9].S().D(),
                                dTope = row[10].S().D(),
                                dInflacion = row[11].S().D(),
                                dImporteNuevo = row[12].S().D(),
                                iAnio = row[13].S().I()
                            });
                        }
                    }
                }
                return lstIncrementoTarifa;
            }
        }        

        #endregion        
               
    }
}











