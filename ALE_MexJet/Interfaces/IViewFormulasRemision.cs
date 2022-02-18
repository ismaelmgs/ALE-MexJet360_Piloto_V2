using ALE_MexJet.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE_MexJet.Interfaces
{
    public interface IViewFormulasRemision : IBaseView
    {
        DataTable dtFactoresFijos { set; get; }
        DataTable dtFactoresVariables { set; get; }
        DataTable dtFormulasRemision { set; get; }

        void LoadObjects();
        void LoadGridView();
        void MostrarMensaje(string sMensaje, string sCaption);

        FormulaRem oFormula { get; }

        event EventHandler eGetFactores;
    }
}
