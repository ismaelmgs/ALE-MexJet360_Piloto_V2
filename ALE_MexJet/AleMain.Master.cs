using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ALE_MexJet.Interfaces;
using System.Data;
using NucleoBase.Core;

namespace ALE_MexJet
{
    public partial class AleMain : System.Web.UI.MasterPage
    {
        public string sRolId
        {
            get
            {
                return "1";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (eGetMenu != null)
                eGetMenu(sender, e);
             
        }

        public void LoadMenu(DataTable dtObjMenu)
        {

        }

        public event EventHandler eGetMenu;


        public event EventHandler eNewObj;

        public event EventHandler eObjSelected;

        public event EventHandler eSaveObj;

        public event EventHandler eDeleteObj;

        public event EventHandler eSearchObj;
    }
}