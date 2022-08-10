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
                string sHtml = string.Empty;

                BitacoraVuelo oB = new BitacoraVuelo();
                DataTable dt = new DataTable();
                oB.IIdBitacora = iIdBitacora;
                dt = new DBConsultaBitacoras().DBGetFotoXBitacora(oB);

                if (dt != null && dt.Rows.Count > 0)
                {
                    sHtml = "<div id='myCarousel' class='carousel slide' data-ride='carousel'>";

                    //Indicators
                    sHtml += "  <ol class='carousel-indicators'>";
                    for (int x = 0; x < dt.Rows.Count; x++)
                    {
                        if(x == 0)
                            sHtml += "<li data-target='#myCarousel' data-slide-to='" + x + "' class='active'></li>";
                        //sHtml += "<li data-target='#myCarousel' data-slide-to='" + x + "'></li>";

                        else
                            sHtml += "<li data-target='#myCarousel' data-slide-to='" + x + "'></li>";
                    }
                    sHtml += "</ol>";


                    //Wrapper for slides
                    sHtml += "<div class='carousel-inner'>";

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        byte[] imgData = (byte[])dt.Rows[i]["Foto"];
                        string[] sExt = dt.Rows[i]["NombreArchivo"].S().Split('.');
                        string sImagenUrl = string.Empty;

                        using (MemoryStream ms = new MemoryStream(imgData))
                        {
                            ms.Position = 0;
                            if (sExt.Length > 1)
                            {
                                switch (sExt[1].S())
                                {
                                    case "jpg":
                                    case "JPG":
                                        sImagenUrl = "data:image/jpg;base64," + Convert.ToBase64String(ms.ToArray(), 0, ms.ToArray().Length);
                                        break;
                                    case "jpeg":
                                    case "JPEG":
                                        sImagenUrl = "data:image/jpeg;base64," + Convert.ToBase64String(ms.ToArray(), 0, ms.ToArray().Length);
                                        break;
                                    case "png":
                                    case "PNG":
                                        sImagenUrl = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray(), 0, ms.ToArray().Length);
                                        break;
                                    case "gif":
                                    case "GIF":
                                        sImagenUrl = "data:image/gif;base64," + Convert.ToBase64String(ms.ToArray(), 0, ms.ToArray().Length);
                                        break;
                                    case "jfif":
                                    case "JFIF":
                                        sImagenUrl = "data:image/jfif;base64," + Convert.ToBase64String(ms.ToArray(), 0, ms.ToArray().Length);
                                        break;
                                    case "tiff":
                                    case "TIFF":
                                        sImagenUrl = "data:image/tiff;base64," + Convert.ToBase64String(ms.ToArray(), 0, ms.ToArray().Length);
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }

                        if (i == 0)
                        {
                            //sHtml += "  <div class='item active'>";
                            sHtml += "  <div class='item active'>";
                            sHtml += "      <img src='" + sImagenUrl + "' alt='' style='height: 90% !important;width: 90 % !important;margin: 0 auto 0 auto; max-height:500px !important;' />";
                            sHtml += "      <div class='carousel-caption'>";
                            sHtml += "          <h4 style='color:#50c878; text-shadow: -2px 1px 3px black;'>" + dt.Rows[i]["NombreArchivo"].S() + "</h4>";
                            //sHtml += "          <p>This is the first image slide</p>";
                            sHtml += "      </div>";
                            sHtml += "  </div>";
                        }
                        else
                        {
                            sHtml += "  <div class='item'>";
                            sHtml += "      <img src='" + sImagenUrl + "' alt='' style='height: 90% !important; width: 90 % !important;margin: 0 auto 0 auto; max-height:500px !important;' />";
                            sHtml += "      <div class='carousel-caption'>";
                            sHtml += "          <h4 style='color:#50c878; text-shadow: -2px 1px 3px black;'>" + dt.Rows[i]["NombreArchivo"].S() + "</h4>";
                            //sHtml += "          <p>This is the first image slide</p>";
                            sHtml += "      </div>";
                            sHtml += "  </div>";
                        }
                    }

                    sHtml += "</div>";

                    //Controls
                    sHtml += "<a class='left carousel-control' href='#myCarousel' data-slide='prev' style='color:#50c878; text-shadow: -2px 1px 3px black;'>";
                    sHtml += "  <span class='glyphicon glyphicon-chevron-left'></span>";
                    sHtml += "  <span class='sr-only'>Previous</span>";
                    sHtml += "</a>";
                    sHtml += "<a class='right carousel-control' href='#myCarousel' data-slide='next' style='color:#50c878; text-shadow: -2px 1px 3px black;'>";
                    sHtml += "  <span class='glyphicon glyphicon-chevron-right'></span>";
                    sHtml += "  <span class='sr-only'>Next</span>";
                    sHtml += "</a>";

                    sHtml += "</div>";
                    sHtml += "</div>";                 
                }
                divPrint.InnerHtml = sHtml;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}