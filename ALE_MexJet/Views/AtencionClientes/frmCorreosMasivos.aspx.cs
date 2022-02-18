using ALE_MexJet.Clases;
using ALE_MexJet.ControlesUsuario;
using ALE_MexJet.DomainModel;
using ALE_MexJet.Interfaces;
using ALE_MexJet.Presenter;
using ALE_MexJet.Objetos;
using NucleoBase.Core;
using NucleoBase.Seguridad;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.XtraPrinting;
using DevExpress.Export;
using DevExpress.Web;
using System.IO;
using DevExpress.Web.Export;

namespace ALE_MexJet.Views.AtencionClientes
{
	public partial class frmCorreosMasivos : System.Web.UI.Page, IViewCorreosMasivos
	{
		#region Eventos
		protected void Page_Load(object sender, EventArgs e)
		{
			try
			{
				oPresenter = new CorreosMasivos_Presenter(this, new DBCorreosMasivos());
				
				if (!IsPostBack)
				{
					ObtieneValores();
				}
				LlenaGrid();
			}
			catch (Exception x)
			{
				throw x;
			}
		}

		protected void btnBuscar_Click(object sender, EventArgs e)
		{
			try
			{
				if (eSearchObj != null)
					eSearchObj(null, EventArgs.Empty);

				gvCorreosMasivos.DataSource = null;
				gvCorreosMasivos.DataSource = (DataTable)ViewState["gvCorreosMasivos"];
				gvCorreosMasivos.DataBind();
			}
			catch (Exception x)
			{
				throw x;
			}
		}

		protected void btnNuevo_Click(object sender, EventArgs e)
		{
			try
			{
                Response.Redirect("~/Views/AtencionClientes/frmCorreoM.aspx?Id=0", false);
			}
			catch (Exception x)
			{
				Utils.SaveErrorEnBitacora(mpeMensaje, x.Message, sPagina, sClase, "btnNuevo_Click", "Aviso");
			}
		}

		protected void btnExcel_Click(object sender, EventArgs e)
		{
			try
			{
				for (int i = 0; i < gvCorreosMasivos.Columns.Count; i++)
				{
					if(i != 4)
						gvCorreosMasivos.Columns[i].Visible = true;
				}
				gvCorreosMasivos.Columns[0].Visible = true;
				XlsExportOptionsEx exportOptions = new XlsExportOptionsEx() { ExportType = ExportType.WYSIWYG, };
				Exporter.WriteXlsToResponse("CorreosMasivos", exportOptions);
				
				gvCorreosMasivos.Columns[2].Visible = false;
				gvCorreosMasivos.Columns[3].Visible = false;
				gvCorreosMasivos.Columns[4].Visible = false;
			}
			catch (Exception x)
			{
				//Utils.SaveErrorEnBitacora(mpeMensaje, x.Message, sPagina, sClase, "btnExcel_Click", "Aviso");
				throw x;
			}
		}

		protected void gvCorreosMasivos_CustomButtonCallback(object sender, DevExpress.Web.ASPxGridViewCustomButtonCallbackEventArgs e)
		{
			try
			{
				if (e.ButtonID == "btnCancelar")
				{
					eCrud = Enumeraciones.TipoOperacion.Eliminar;
					
					DataRowView row = gvCorreosMasivos.GetRow(e.VisibleIndex) as DataRowView;
					oCrud = row.Row.ItemArray[0].S().I();

					if (eCancelObj != null)
						eCancelObj(sender, EventArgs.Empty);

					ObtieneValores();
					LlenaGrid();
				}
			}
			catch (Exception x)
			{
				throw x;
			}
		}

		protected void gvCorreosMasivos_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
		{
			try
			{
				DataRowView row = gvCorreosMasivos.GetRow(e.VisibleIndex) as DataRowView;
				if (row != null && row.Row.ItemArray.Length > 0)
				{
					if (row.Row.ItemArray[6].S() == "Cancelado")
					{
						if (e.ButtonID == "btnCancelar" || e.ButtonID == "btnEditar")
							e.Enabled = false;
					}
					if (row.Row.ItemArray[6].S() == "Enviado")
					{
						if (e.ButtonID == "btnCancelar")
							e.Enabled = false;
					}
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		protected void gvCorreosMasivos_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
		{
			try
			{
				DataRowView row = gvCorreosMasivos.GetRow(e.VisibleIndex) as DataRowView;
				if (row != null && row.Row.ItemArray.Length > 0)
				{
					if (row.Row.ItemArray[6].S() == "Cancelado")
					{
						if (e.ButtonType == ColumnCommandButtonType.Edit)
							e.Enabled = false;
					}
				}
				
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		protected void UpdatePanel_Unload(object sender, EventArgs e)
		{
			MethodInfo methodInfo = typeof(ScriptManager)
				.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
				.Where(i => i.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel"))
				.First();
			methodInfo.Invoke(ScriptManager.GetCurrent(Page),
			new object[] { sender as UpdatePanel });
		}
		#endregion Eventos

		#region Metodos
		public void ObtieneValores()
		{
			try
			{
				if (eSearchObj != null)
					eSearchObj(null, EventArgs.Empty);
			}
			catch (Exception x)
			{
				throw x;
			}
		}

		public void LlenaGrid()
		{
			try
			{
				DataTable dt = (DataTable)ViewState["gvCorreosMasivos"];
				if (dt != null && dt.Rows.Count > 0)
				{
					gvCorreosMasivos.DataSource = null;
					gvCorreosMasivos.DataSource = dt;
					gvCorreosMasivos.DataBind();
				}
			}
			catch (Exception x)
			{
				throw x;
			}
		}

		public void LoadObjects(DataTable dtObjCat)
		{
			try
			{
				ViewState["gvCorreosMasivos"] = dtObjCat;
			}
			catch (Exception x)
			{
				throw x;
			}
		}

		public void MostrarMensaje(string sMensaje, string sCaption)
		{
			mpeMensaje.ShowMessage(sMensaje, sCaption);
		}

		public void MuestraMensg(string sMensaje, string sCaption)
		{
			MostrarMensaje(sMensaje, sCaption);
		}
		#endregion Metodos

		#region Variables
		CorreosMasivos_Presenter oPresenter;
		
		private const string sClase = "frmCorreosMasivos.aspx.cs";
		private const string sPagina = "frmCorreosMasivos.aspx";

		public event EventHandler eNewObj;
		public event EventHandler eObjSelected;
		public event EventHandler eSaveObj;
		public event EventHandler eDeleteObj;
		public event EventHandler eSearchObj;
		public event EventHandler eCancelObj;
		public event EventHandler eObjCorreosMasivos;

		public object[] oArrFiltros
		{
			get
			{
				DateTime dFechaDesde = dtFechaDesde.Date == DateTime.MinValue ? DateTime.Parse("1/1/1753 12:00:00") : dtFechaDesde.Date;
				DateTime dFechaHasta = dtFechaHasta.Date == DateTime.MinValue ? DateTime.Parse("1/1/1753 12:00:00") : dtFechaHasta.Date;
				return new object[]
				{
					"@fechaDesde", dFechaDesde,
					"@fechaHasta", dFechaHasta,
					"@status", ddlStatus.SelectedItem.Value
				};
			}
		}

		public object oCrud
		{
			get { return ViewState["CrudCorreosMasivos"]; }
			set { ViewState["CrudCorreosMasivos"] = value; }
		}

		public Enumeraciones.TipoOperacion eCrud
		{
			get { return (Enumeraciones.TipoOperacion)ViewState["eCrud"]; }
			set { ViewState["eCrud"] = value; }
		}
		#endregion Variables

      
	}
}