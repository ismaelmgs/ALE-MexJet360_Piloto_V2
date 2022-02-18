using ALE_MexJet.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ALE_MexJet.Interfaces;
using ALE_MexJet.DomainModel;
using ALE_MexJet.Presenter;
using System.Data;
using System.Text;
using DevExpress.Web;

namespace ALE_MexJet.Views.AtencionClientes
{
    public partial class frmDashboard : System.Web.UI.Page, IViewDashboard 
    {        
        protected void Page_Load(object sender, EventArgs e)
        {
            oPresenter = new Dashboard_Presenter(this,new DBDashboard());
            ObtieneDashboardAvisos();
            ObtieneDashboardClientes();
            ObtieneDashboardVuelos();
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

        private const string sClase = "frmDashboard.aspx.cs";
        private const string sPagina = "frmDashboard.aspx";

        public void ObtieneDashboardAvisos()
        {
            if (eGetDashboardAvisos != null)
                eGetDashboardAvisos(null, EventArgs.Empty);
        }

        public void ObtieneDashboardClientes()
        {
            if (eGetDashboardClientes != null)
                eGetDashboardClientes(null, EventArgs.Empty);
        }

        public void ObtieneDashboardVuelos()
        {
            if (eGetDashboardVuelos != null)
                eGetDashboardVuelos(null, EventArgs.Empty);
        }

        public void LoadDashboardAvisos(DataTable dtAvisos)
        {
            StringBuilder sbPanel = new StringBuilder();

            sbPanel.Append("    <div id='miDiv' >");
            sbPanel.Append("        <table style='text-align:left'>");

            sbPanel.Append("    <tr>");
            sbPanel.Append("        <td WIDTH='150px' HEIGHT='20'>   </td>");       
            sbPanel.Append("    </tr>");

            foreach (DataRow row in dtAvisos.Rows)
            {
                sbPanel.Append("        <tr>");
                sbPanel.AppendFormat("      <td WIDTH='25px' HEIGHT='20'>                                               </td>");
                sbPanel.AppendFormat("      <td WIDTH='25px' HEIGHT='20'>     *                                         </td>");
                sbPanel.AppendFormat("      <td WIDTH='300px' HEIGHT='20'>   {0}                                        </td>", row[0].ToString());
                sbPanel.AppendFormat("      <td WIDTH='25px' HEIGHT='20'>    <IMG src ='{0}' width='15px' height='15px'></td>", row[3].ToString());
                sbPanel.AppendFormat("      <td WIDTH='80px' HEIGHT='20'>    {0}                                        </td>", row[4].ToString());
                sbPanel.Append("        </tr>");
            }

            sbPanel.Append("        </table>");
            sbPanel.Append("    </div>");

            divAvisos.InnerHtml = sbPanel.ToString();
        }

        public void LoadDashboardClientes(DataTable dtClientes)
        {
            StringBuilder sbPanel = new StringBuilder();

            sbPanel.Append("    <div id='miDiv' >");
            sbPanel.Append("        <table style='text-align:left'>");

            sbPanel.Append("    <tr>");
            sbPanel.Append("        <td WIDTH='150px' HEIGHT='20'> *     No. De contratos Mexjet  </td>");                      
            sbPanel.Append("    </tr>");

            foreach (DataRow row in dtClientes.Rows)
            {
                sbPanel.Append("        <tr>");
                sbPanel.AppendFormat("      <td WIDTH='25px' HEIGHT='20'>       </td>");
                sbPanel.AppendFormat("      <td WIDTH='25px' HEIGHT='20'>   *   </td>");
                sbPanel.AppendFormat("      <td WIDTH='100px' HEIGHT='20'> {0}  </td>",row[1].ToString());
                sbPanel.AppendFormat("      <td WIDTH='50px' HEIGHT='20'>  {0}  </td>",row[0].ToString());                
                sbPanel.Append("        </tr>");
            }

            sbPanel.Append("        </table>");
            sbPanel.Append("    </div>");

            divClientes.InnerHtml = sbPanel.ToString();
        }


        public void LoadDashboardVuelos(DataTable dtVuelos)
        {
            StringBuilder sbPanel = new StringBuilder();

            sbPanel.Append("    <div id='miDiv' >");
            sbPanel.Append("        <table style='text-align:left'>");

            sbPanel.Append("    <tr>");
            sbPanel.Append("        <td WIDTH='150px' HEIGHT='20'>   </td>");
            sbPanel.Append("    </tr>");

            foreach (DataRow row in dtVuelos.Rows)
            {
                sbPanel.Append("        <tr>");
                sbPanel.AppendFormat("      <td WIDTH='25px' HEIGHT='20'>       </td>");
                sbPanel.AppendFormat("      <td WIDTH='25px' HEIGHT='20'>   *   </td>");
                sbPanel.AppendFormat("      <td WIDTH='300px' HEIGHT='20'> {0}  </td>", row[1].ToString());
                sbPanel.AppendFormat("      <td WIDTH='50px' HEIGHT='20'>  {0}  </td>", row[0].ToString());
                sbPanel.Append("        </tr>");
            }

            sbPanel.Append("        </table>");
            sbPanel.Append("    </div>");

            divVuelos.InnerHtml = sbPanel.ToString();
        }

        Dashboard_Presenter oPresenter;
        public event EventHandler eGetDashboardAvisos;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;                
        public event EventHandler eGetDashboardClientes;
        public event EventHandler eGetDashboardVuelos;                        
    }
}