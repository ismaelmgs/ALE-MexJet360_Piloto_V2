using ALE_MexJet.DomainModel;
using ALE_MexJet.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace ALE_MexJet.Presenter
{
    public class Master_Presenter : BasePresenter<IViewMaster>
    {
        private readonly DBUsuarios oIGestCat;

        public Master_Presenter(IViewMaster oView, DBUsuarios oGC)
            : base(oView)
        {
            oIGestCat = oGC;
            oIView.eGetMenu += eGetMenu;
            
            
            
        }

        protected void eGetMenu(object sender, EventArgs e)
        {
            try
            {
                oIView.LoadObjects(oIGestCat.dtGetMenu(oIView.sRolId));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LoadObjects_Presenter()
        {
            try
            {
                oIView.LoadObjects(oIGestCat.dtGetMenu(oIView.sRolId));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public void LoadMenu_Presenter()
        {
          
        }


        


    }
}