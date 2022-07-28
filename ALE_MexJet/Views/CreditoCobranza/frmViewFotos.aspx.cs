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

namespace ALE_MexJet.Views.CreditoCobranza
{
    public partial class frmViewFotos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int iIdRemision = Request.QueryString["IdRemision"].S().I();
            CargaLegs(iIdRemision);
        }
        public DataTable dtLegs
        {
            get { return (DataTable)ViewState["VSLegs"]; }
            set { ViewState["VSLegs"] = value; }
        }

        public void CargaLegs(int iIdRemision)
        {
            try
            {
                dtLegs = null;
                dtLegs = new DBCliente().DBGetLegsByRemision(iIdRemision);

                if (dtLegs != null && dtLegs.Rows.Count > 0)
                    LoadFoto(dtLegs);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void LoadFoto(DataTable dtLegs)
        {
            try
            {
                string sHtml = string.Empty;
                BitacoraVuelo oB = new BitacoraVuelo();
                DataTable dt = new DataTable();
                DataTable dtFotos = new DataTable();
                dtFotos.Columns.Add("Foto");
                dtFotos.Columns.Add("NombreArchivo");

                for (int a = 0; a < dtLegs.Rows.Count; a++)
                {
                    oB.LLegId = dtLegs.Rows[a][0].L();
                    dt = new DBConsultaBitacoras().DBGetFotoXLegID(oB);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        for (int b = 0; b < dt.Rows.Count; b++)
                        {
                            DataRow row = dtFotos.NewRow();
                            row["Foto"] = dt.Rows[b]["Foto"];
                            row["NombreArchivo"] = dt.Rows[b]["NombreArchivo"];
                            dtFotos.Rows.Add(row);
                        }
                    }
                }
                dtFotos.AcceptChanges();
                
                if (dtFotos != null && dtFotos.Rows.Count > 0)
                {
                    sHtml = "<div id='myCarousel' class='carousel slide' data-ride='carousel'>";

                    //Indicators
                    sHtml += "  <ol class='carousel-indicators'>";
                    for (int x = 0; x < dtFotos.Rows.Count; x++)
                    {
                        if (x == 0)
                            sHtml += "<li data-target='#myCarousel' data-slide-to='" + x + "' class='active'></li>";
                        else
                            sHtml += "<li data-target='#myCarousel' data-slide-to='" + x + "'></li>";
                    }
                    sHtml += "</ol>";


                    //Wrapper for slides
                    sHtml += "<div class='carousel-inner'>";

                    for (int i = 0; i < dtFotos.Rows.Count; i++)
                    {

                        byte[] imgData = (byte[])dt.Rows[i]["Foto"];
                        string[] sExt = dt.Rows[i]["NombreArchivo"].S().Split('.');
                        string sImagenUrl = string.Empty;

                        using (MemoryStream ms = new MemoryStream(imgData))
                        {
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
                            sHtml += "  <div class='item active'>";
                            sHtml += "      <img src='" + sImagenUrl + "' alt='' width='400px' height='400px' />";
                            sHtml += "      <div class='carousel-caption'>";
                            sHtml += "          <h4 style='color:#CCCCCC;'>" + dtFotos.Rows[i]["NombreArchivo"].S() + "</h4>";
                            //sHtml += "          <p>This is the first image slide</p>";
                            sHtml += "      </div>";
                            sHtml += "  </div>";
                        }
                        else
                        {
                            sHtml += "  <div class='item'>";
                            sHtml += "      <img src='" + sImagenUrl + "' alt='' width='400px' height='400px' />";
                            sHtml += "      <div class='carousel-caption'>";
                            sHtml += "          <h4 style='color:#CCCCCC;'>" + dtFotos.Rows[i]["NombreArchivo"].S() + "</h4>";
                            //sHtml += "          <p>This is the first image slide</p>";
                            sHtml += "      </div>";
                            sHtml += "  </div>";
                        }
                    }

                    sHtml += "</div>";

                    //Controls
                    sHtml += "<a class='left carousel-control' href='#myCarousel' data-slide='prev'>";
                    sHtml += "  <span class='glyphicon glyphicon-chevron-left'></span>";
                    sHtml += "  <span class='sr-only'>Previous</span>";
                    sHtml += "</a>";
                    sHtml += "<a class='right carousel-control' href='#myCarousel' data-slide='next'>";
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