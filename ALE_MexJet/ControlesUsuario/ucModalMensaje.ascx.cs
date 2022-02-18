using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ALE_MexJet.ControlesUsuario
{
    public partial class ucModalMensaje : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if(!IsPostBack)
            //    btnOk.OnClientClick = String.Format("fnClickOK('{0}','{1}')", btnOk.UniqueID, "");
        }

       
        public void ShowMessage(string Message, string Caption)
        {
            lbl.Text = Message;
            ppMensaje.ShowOnPageLoad = true;
            ppMensaje.CloseAnimationType = DevExpress.Web.AnimationType.Fade;
        }

        protected void btOK_Click(object sender, EventArgs e)
        {
            //OnOkButtonPressed(e);
        }

        public delegate void OkButtonPressedHandler(object sender, EventArgs args);
        public event OkButtonPressedHandler OkButtonPressed;

        protected virtual void OnOkButtonPressed(EventArgs e)
        {
            if (OkButtonPressed != null)
                OkButtonPressed(btOK, e);
        }
    }
}