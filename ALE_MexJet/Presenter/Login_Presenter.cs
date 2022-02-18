using ALE_MexJet.DomainModel;
using ALE_MexJet.Interfaces;
using ALE_MexJet.Objetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NucleoBase.Core;
using DevExpress.Web.Data;
using DevExpress.Web;
using System.Data;
using System.Collections.Specialized;

namespace ALE_MexJet.Presenter
{
    public class Login_Presenter : BasePresenter<IViewLogin>
    {
        private readonly DBRolAccion oIGestCat;

        public Login_Presenter(IViewLogin oView, DBRolAccion oGC)
            : base(oView)
        {
            oIGestCat = oGC;
            oIView.eSearchToken += eSearchToken_Presenter;
        }
        protected override void SearchObj_Presenter(object sender, EventArgs e)
        {
            oIView.LoadObjects(oIGestCat.DBSearchObj(oIView.oArrFiltros));
        }

        protected override void SaveObj_Presenter(object sender, EventArgs e)
        {
            // Metodo de actulización
            DataTable dtRes = new DBLogin().DBSaveToken(oIView.iToken, oIView.sUsuario);
            oIView.iRestoken = dtRes.Rows[0]["IdToken"].S().I();
            oIView.sNumeroTel = dtRes.Rows[0]["Numero"].S();
            oIView.sClaveToken = dtRes.Rows[0]["ClaveToken"].S();
        }

        protected void eSearchToken_Presenter(object sender, EventArgs e)
        {
            // Metodo de busqueda
            oIView.TokenGuardado = new DBLogin().DBConsultaToken(oIView.sUsuario, oIView.sToken);
        }
    }
}