using ALE_MexJet.Clases;
using ALE_MexJet.ControlesUsuario;
using ALE_MexJet.DomainModel;
using ALE_MexJet.Interfaces;
using ALE_MexJet.Presenter;
using ALE_MexJet.Objetos;
using NucleoBase.Core;
using NucleoBase.Seguridad;
using DevExpress.XtraPrinting;
using DevExpress.Export;
using DevExpress.Web;
using DevExpress.Web.Export;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.Data;
using System.ComponentModel;
using System.Text;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace ALE_MexJet.Views.Consultas
{

	public partial class frmConsultaPresupuestos : System.Web.UI.Page, IViewConsultaPresupuestos
	{
		#region Eventos
		protected void Page_Load(object sender, EventArgs e)
		{
			try
			{
				oPresenter = new ConsultaPresupuestos_Presenter(this, new DBConsultaPresupuestos());

				if (ddlContrato.Items.Count == 0)
				{
					ddlContrato.Items.Add("[Contrato]", 0);
					ddlContrato.SelectedIndex = 0;
				}
				if (ddlClientes.Items.Count == 0)
				{
					ddlClientes.Items.Add("[Cliente]", 0);
					ddlClientes.SelectedIndex = 0;
				}

				if (!IsPostBack)
				{
					ObtieneValores();

					if (eObjCliente != null)
						eObjCliente(null, EventArgs.Empty);
				}
				CargaDataSource();
			}
			catch (Exception ex)
			{
				throw ex;
				//Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "Page_Load", "Aviso");
			}
		}

		protected void ddlClientes_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				if (eObjContrato != null)
					eObjContrato(null, EventArgs.Empty);
			}
			catch (Exception ex)
			{
				throw ex;
				//Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "ddlClientes_SelectedIndexChanged", "Aviso");
			}
		}

		protected void btnNuevo_Click(object sender, EventArgs e)
		{
			try
			{
				MostrarFormulario("Nuevo");
			}
			catch (Exception ex)
			{
				Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnNuevo_Click", "Aviso");
			}
		}

		protected void btnExcel_Click(object sender, EventArgs e)
		{
			try
			{
				XlsExportOptionsEx opts = new XlsExportOptionsEx() { ExportType = ExportType.WYSIWYG };
				Exporter.WriteXlsToResponse("Presupuestos", opts);
			}
			catch (Exception ex)
			{
				Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnExcel_Click", "Aviso");
			}
		}

		protected void btnBuscar_Click(object sender, EventArgs e)
		{
			try
			{
				if (eSearchObj != null)
					eSearchObj(null, EventArgs.Empty);
				CargaDataSource();
			}
			catch (Exception ex)
			{
				throw ex;
				//Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnBuscar_Click", "Aviso");
			}
		}

		protected void gvPresupuestos_RowDeleting(object sender, ASPxDataDeletingEventArgs e)
		{
			try
			{
				eCrud = Enumeraciones.TipoOperacion.Eliminar;
				oCrud = e;
				CancelEditing(e);
				if (eDeleteObj != null)
					eDeleteObj(sender, e);
				CargaDataSource();
			}
			catch (Exception ex)
			{
				throw ex;
				//Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvPresupuestos_RowDeleting", "Aviso");
			}
		}
		
		protected void gvPresupuestos_StartRowEditing(object sender, ASPxStartRowEditingEventArgs e)
		{
			try
			{
				CancelEditing(e);
				object value = e.EditingKeyValue;
				MostrarFormulario("Editar", value.S());
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		protected void gvPresupuestos_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
		{
			try
			{
				object value = (sender as ASPxGridView).GetRowValues(e.VisibleIndex, "IdPresupuesto");
				if (e.ButtonID == "btnConsultar")
					MostrarFormulario("Consultar", value.S());

				if (e.ButtonID == "btnReprte")
				{
					MostrarReprte(value.I());
				}
			}
			catch (Exception ex)
			{
				throw ex;
				//Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvPresupuestos_CustomButtonCallback", "Aviso");
			}
		}

		protected void UpdatePanel_Unload(object sender, EventArgs e)
		{
			MethodInfo methodInfo = typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Where(i => i.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel")).First();
			methodInfo.Invoke(ScriptManager.GetCurrent(Page),
			new object[] { sender as UpdatePanel });
		}
		#endregion Eventos

		#region Metodos
		public void CargaGrid(DataTable dtObjPresupuestos)
		{
			try
			{
				ViewState["gvPresupuestos"] = dtObjPresupuestos;
			}
			catch (Exception ex)
			{
				throw ex;
				//Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "CargaGrid", "Aviso");
			}
		}

		public void CargaDataSource()
		{
			try
			{
				DataTable dt = (DataTable)ViewState["gvPresupuestos"];
				gvPresupuestos.DataSource = null;
				gvPresupuestos.DataBind();
				if (dt != null && dt.Rows.Count > 0)
				{
					gvPresupuestos.DataSource = dt;
					gvPresupuestos.DataBind();
				}
			}
			catch (Exception ex)
			{
				throw ex;
				//Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "CargaDataSource", "Aviso");
			}
		}

		public void CargaDSPresupuesto(DataSet dsPresupuesto)
		{
			ViewState["dsPresupuesto"] = dsPresupuesto;
		}

		public void ObtieneValores()
		{
			try
			{
				if (eSearchObj != null)
					eSearchObj(null, EventArgs.Empty);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public void MostrarFormulario(string Accion, string id = "0")
		{
			try
			{
				string sIdPresupuesto = Convert.ToBase64String(Encoding.UTF8.GetBytes(id));
				string sAccion = Convert.ToBase64String(Encoding.UTF8.GetBytes(Accion));
				string ruta = "~/Views/Rentas/frmPresupuesto.aspx?Presupuesto=" + sIdPresupuesto + "&Accion=" + sAccion;

				if (IsCallback)
					ASPxWebControl.RedirectOnCallback(ruta);
				else
					Response.Redirect(ruta);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public void MostrarReprte(int idPresupuesto)
		{
			try
			{
				string sIdPresupuesto = Convert.ToBase64String(Encoding.UTF8.GetBytes(idPresupuesto.S()));
				string ruta = "~/Views/Consultas/frmReportePresupuesto.aspx?Presupuesto=" + sIdPresupuesto;
				if (IsCallback)
					ASPxWebControl.RedirectOnCallback(ruta);
				else
					Response.Redirect(ruta);

				//iIdPresupuesto = idPresupuesto;
				//if (eObjPresupuesto != null)
				//	eObjPresupuesto(null, EventArgs.Empty);

				//ReportDocument rd = new ReportDocument();

				//string strPath = string.Empty;
				//strPath = Server.MapPath("CristalReport\\Presupuesto_CR.rpt");
				//rd.Load(strPath);

				//DataSet dsPresupuesto = (DataSet)ViewState["dsPresupuesto"];

				//if (dsPresupuesto.Tables[0].Rows.Count > 0)
				//{
				//	rd.Database.Tables["Presupuestos"].SetDataSource(dsPresupuesto.Tables[0]);
				//	rd.Database.Tables["ServiciosCargoPresupuestos"].SetDataSource(dsPresupuesto.Tables[1]);
				//	rd.Database.Tables["ServiciosVueloConceptosPresupuesto_D"].SetDataSource(dsPresupuesto.Tables[2]);
				//	rd.Database.Tables["ServiciosVueloConceptosPresupuesto_D1"].SetDataSource(dsPresupuesto.Tables[3]);
				//	rd.Database.Tables["ServiciosVueloConceptosPresupuesto_D2"].SetDataSource(dsPresupuesto.Tables[4]);
				//	rd.Database.Tables["ServiciosVueloConceptosPresupuesto_D3"].SetDataSource(dsPresupuesto.Tables[5]);
				//	rd.Database.Tables["ServiciosVueloConceptosPresupuesto_D4"].SetDataSource(dsPresupuesto.Tables[6]);
				//	rd.Database.Tables["ServiciosVueloConceptosPresupuesto_D5"].SetDataSource(dsPresupuesto.Tables[7]);
				//	rd.Database.Tables["ServiciosVueloConceptosPresupuesto_H"].SetDataSource(dsPresupuesto.Tables[8]);
				//	rd.Database.Tables["TramosPresupuesto"].SetDataSource(dsPresupuesto.Tables[9]);
				//	rd.Database.Tables["TotalHrsDescontar"].SetDataSource(dsPresupuesto.Tables[10]);

				//	//rd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "Presupuesto " + idPresupuesto);
				//	rd.ExportToDisk(ExportFormatType.PortableDocFormat, Server.MapPath("CristalReport\\Presupuesto.pdf"));
				//	//System.IO.Stream oStream = null;
				//	//byte[] byteArray = null;
				//	//oStream = rd.ExportToStream(ExportFormatType.PortableDocFormat);
				//	//byteArray = new byte[oStream.Length];
				//	//oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
				//	//Response.Clear();
				//	//Response.ClearContent();
				//	//Response.ClearHeaders();
				//	//Response.Buffer = false;
				//	////Response.ContentType = "application/pdf";
				//	////Response.AddHeader("content-disposition", "attachment;filename=Presupuesto.pdf");
				//	//Response.AppendHeader("Content-Type", "application/pdf");
				//	//Response.AppendHeader("Content-Transfer-Encoding", "binary");
				//	//Response.AppendHeader("Content-Disposition", "attachment; filename=Presupuesto.pdf");
				//	//Response.BinaryWrite(byteArray);
				//	//Response.Flush();
				//	//Response.Close();
				//	//rd.Close();
				//	//rd.Dispose();
				//	//upGv.Update();
				//}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		protected void CancelEditing(CancelEventArgs e)
		{
			try
			{
				e.Cancel = true;
				gvPresupuestos.CancelEdit();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public void LoadClientes(DataTable dtObjCat)
		{
			try
			{
				//dtObjCat.Rows[0].Delete();
				ViewState["oCliente"] = dtObjCat;
				ddlClientes.DataSource = dtObjCat;
				ddlClientes.ValueField = "IdCliente";
				ddlClientes.TextField = "CodigoCliente";
				ddlClientes.DataBind();
				ddlClientes.Items[0].Text = "[Cliente]";
				ddlClientes.SelectedIndex = 0;
			}
			catch (Exception ex)
			{
				throw ex;
				//Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "LoadClientes", "Aviso");
			}
		}

		public void LoadContratos(DataTable dtObjCat)
		{
			try
			{
				//dtObjCat.Rows[0].Delete();
				ViewState["LoadContrato"] = dtObjCat;
				ddlContrato.DataSource = dtObjCat;
				ddlContrato.TextField = "ClaveContrato";
				ddlContrato.ValueField = "IdContrato";
				ddlContrato.DataBind();
				ddlContrato.Items[0].Text = "[Contrato]";
				ddlContrato.SelectedIndex = 0;
			}
			catch (Exception ex)
			{
				throw ex;
				//Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "LoadContratos", "Aviso");
			}
		}

		public void MostrarMensaje(string sMensaje, string sCaption)
		{
			mpeMensaje.ShowMessage(sMensaje, sCaption);
		}
		#endregion Metodos

		#region Variables
		ConsultaPresupuestos_Presenter oPresenter;
		private const string sClase = "frmConsultaPresupuestos.aspx.cs";
		private const string sPagina = "frmConsultaPresupuestos.aspx";

		public event EventHandler eNewObj;
		public event EventHandler eObjSelected;
		public event EventHandler eSaveObj;
		public event EventHandler eDeleteObj;
		public event EventHandler eSearchObj;
		public event EventHandler eObjCliente;
		public event EventHandler eObjContrato;
		public event EventHandler eObjPresupuesto;

		public object[] oArrFiltros
		{
			get
			{
				//int idCliente = ddlClientes.SelectedItem == null ? 0 : ddlClientes.SelectedItem.Value.S().I();
				//int idContrato = ddlContrato.SelectedItem == null ? 0 : ddlContrato.SelectedItem.Value.S().I();
				return new object[]
				{
					"@IdCliente", ddlClientes.SelectedItem == null ? 0 : ddlClientes.SelectedItem.Value.S().I(),
					"@IdContrato", ddlContrato.SelectedItem == null ? 0 : ddlContrato.SelectedItem.Value.S().I(),
					"@NumPresupuesto", txtIdPresupuesto.Value.S()
				};
			}
		}

		public object[] oArrFiltroContratos
		{
			get
			{
				return new object[]
				{
					"@IdCliente", ddlClientes.SelectedItem == null ? 0 : ddlClientes.SelectedItem.Value.S().I()
				};
			}
		}

		public object oCrud
		{
			get { return ViewState["CrudPresupuesto"]; }
			set { ViewState["CrudPresupuesto"] = value; }
		}

		public int iIdPresupuesto
		{
			get { return ViewState["IdPresupuesto"].I(); }
			set { ViewState["IdPresupuesto"] = value; }
		}

		public Enumeraciones.TipoOperacion eCrud
		{
			get { return (Enumeraciones.TipoOperacion)ViewState["eCrud"]; }
			set { ViewState["eCrud"] = value; }
		}
		#endregion Variables
	}
}