using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NucleoBase.Core;
using ALE_MexJet.DomainModel;
using ALE_MexJet.Objetos;

namespace ALE_MexJet.Views.Operaciones
{
    public partial class frmViewBitacora : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int iIdBitacora = Request.QueryString["Bitacora"].S().I();
            LoadFoto(iIdBitacora);
        }
        public void LoadFoto(int iIdBitacora)
        {
            try
            {
                BitacoraVuelo oB = new BitacoraVuelo();
                DataTable dt = new DataTable();
                oB.IIdBitacora = iIdBitacora;
                dt = new DBConsultaBitacoras().DBGetFotoXBitacora(oB);

                if (dt != null && dt.Rows.Count > 0)
                {
                    byte[] imgData = (byte[])dt.Rows[0]["Foto"];
                    string[] sExt = dt.Rows[0]["NombreArchivo"].S().Split('.');
                    using (MemoryStream ms = new MemoryStream(imgData))
                    {
                        if (sExt.Length > 1)
                        {
                            switch (sExt[1].S())
                            {
                                case "jpg":
                                    imgFoto.ImageUrl = "data:image/jpg;base64," + Convert.ToBase64String(ms.ToArray(), 0, ms.ToArray().Length);
                                    break;
                                case "jpeg":
                                    imgFoto.ImageUrl = "data:image/jpeg;base64," + Convert.ToBase64String(ms.ToArray(), 0, ms.ToArray().Length);
                                    break;
                                case "png":
                                    imgFoto.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray(), 0, ms.ToArray().Length);
                                    break;
                                case "gif":
                                    imgFoto.ImageUrl = "data:image/gif;base64," + Convert.ToBase64String(ms.ToArray(), 0, ms.ToArray().Length);
                                    break;
                                default:
                                    break;
                            }
                        }
                        //System.Drawing.Image image = System.Drawing.Image.FromStream(ms);
                        
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}