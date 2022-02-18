﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALE_MexJet.Interfaces
{
    public interface IBaseView
    {
        event EventHandler eNewObj;
        event EventHandler eObjSelected;
        event EventHandler eSaveObj;
        event EventHandler eDeleteObj;
        event EventHandler eSearchObj;
    }
}
