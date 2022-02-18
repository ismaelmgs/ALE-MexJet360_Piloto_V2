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

namespace ALE_MexJet
{
    public partial class ALEMaster : System.Web.UI.MasterPage, IViewMaster
    {
        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            oPresenter = new Master_Presenter(this, new DBUsuarios());

            if (!IsPostBack)
            {
                try
                {
                    //DataUserIndetity oUsuario = (DataUserIndetity)Session["nombreI"];
                    //Session["usuario"] = oUsuario.sUser;
                    //lblNombreUsuario.Text = (oUsuario.sNombre + " " + oUsuario.sApellidos).Replace("  ", " ");
                    oPresenter.LoadObjects_Presenter();
                }
                catch (Exception ex)
                {
                    alert(ex.Message);
                }
            }
        }
        #endregion

        #region METODOS

        public void LoadObjects(DataTable dt)
        {
            try
            {
                Menu(MenuUsuarios.Items, 0, dt);
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


        #region VARIABLES Y PROPIEDADES

        Master_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;

        #endregion
    }
}