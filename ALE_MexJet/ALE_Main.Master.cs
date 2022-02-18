using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ALE_MexJet.Interfaces;
using ALE_MexJet.Presenter;
using ALE_MexJet.DomainModel;
using System.Data;
using ALE_MexJet.Objetos;
using NucleoBase.Core;
using System.Reflection;

namespace ALE_MexJet
{
    public partial class ALE_Main : System.Web.UI.MasterPage, IViewMaster
    {
        
        #region EVENTOS

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["UserIdentity"] == null)
                    Response.Redirect("../frmLogin.aspx");

                oPreseneter = new Master_Presenter(this, new DBUsuarios());
                if (!IsPostBack)
                {
                    if (eGetMenu != null)
                        eGetMenu(null, EventArgs.Empty);

                    UserIdentity oUser = (UserIdentity)Session["UserIdentity"];
                    lblUsuario.Text = oUser.sName;
                    lblPerfil.Text = oUser.sRolDescripcion;
                }
            }
            catch(Exception ex)
            {
                alert("Error de conexión");
            }
        }

        protected void upaMaster_Unload(object sender, EventArgs e)
        {
            MethodInfo methodInfo = typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Where(i => i.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel")).First();
            methodInfo.Invoke(ScriptManager.GetCurrent(Page),
                new object[] { sender as UpdatePanel });
        }

        protected void lkbSalir_Click(object sender, EventArgs e)
        {
            Session["UserIdentity"] = null;
            Response.Redirect("~/Views/frmLogin.aspx");
        }

        #endregion

        #region METODOS
        
        public void LoadMenu(DataTable dtObjMenu)
        {
            DataTable dt = dtObjMenu;
        }
        public void LoadObjects(DataTable dtOBject)
        {
            DataTable dt = dtOBject;
            CargaMenu(dt);
        }
        private void CargaMenu(DataTable dtObject)
        {
            try
            {
                DataTable dt = dtObject;
                if (dt.Rows.Count > 0)
                {
                    Menu(MenuUsuarios.Items, 0, dt);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void Menu(MenuItemCollection items, int idpadre, DataTable dt)
        {
            int id;
            string NombreMenu;

            try
            {
                // filtramos por el id que toma este, el primero es "0" que son los padres
                DataRow[] hijos = dt.Select("IdPadre=" + idpadre.ToString());

                // validamos que encontremos resultados, si no volvemos    
                if (hijos.Length == 0)
                    return;
                // recorremos la informacion filtrada
                foreach (DataRow hijo in hijos)
                {
                    // asignamos las variables al menu
                    id = Convert.ToInt32(hijo[0]);
                    NombreMenu = Convert.ToString(hijo[1]);
                    // creamos el item
                    MenuItem nuevoItem = new MenuItem(NombreMenu, id.ToString());
                    nuevoItem.NavigateUrl = hijo["UrlModulo"].ToString().Trim();

                    if (hijo["UrlModulo"].ToString().Trim() == string.Empty)
                        nuevoItem.Selectable = false;
                    else
                        nuevoItem.Selectable = true;

                    items.Add(nuevoItem);
                    // llamamos a la funcion nuevamente para que revise si tiene mas informacion asociada
                    Menu(nuevoItem.ChildItems, id, dt);
                }
            }
            catch (Exception)
            {

            }
        }
        private void alert(string m)
        {
            m = "alert('" + m + "');";
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", m, true);
        }
        #endregion

        #region "Variables y Propiedades

        Master_Presenter oPreseneter;
        public event EventHandler eGetMenu;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;

        public string sRolId
        {
            get
            {
                UserIdentity oUser = (UserIdentity)Session["UserIdentity"];
                return oUser.iRol.S();
            }
        }

        #endregion

        //protected void upaMaster_Unload(object sender, EventArgs e)
        //{
        //    MethodInfo methodInfo = typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Where(i => i.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel")).First();
        //        methodInfo.Invoke(ScriptManager.GetCurrent(Page),
        //        new object[] { sender as UpdatePanel });
        //}

    }
}