using ALE_MexJet.Interfaces;
using DevExpress.Export;
using DevExpress.XtraPrinting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;
using ALE_MexJet.Presenter;
using ALE_MexJet.DomainModel;
using NucleoBase.Core;
using System.ComponentModel;
using DevExpress.Web.Data;
using ALE_MexJet.Objetos;
using ALE_MexJet.Clases;

namespace ALE_MexJet.Views.CreditoCobranza
{
    public partial class frmTraspasoHoras : System.Web.UI.Page,IViewIntercambioHoras
    {
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            DrPermisos = Utils.ObtenerPermisos(Enumeraciones.Pantallas.TraspasoHora);
            LoadActions(DrPermisos);
            oPresenter = new IntercambioHoras_Presenter(this, new DBIntercambioHoras());
            gvTraspasoHrs.SettingsPager.Position = PagerPosition.TopAndBottom;
            gvTraspasoHrs.SettingsPager.ShowDisabledButtons = true;
            gvTraspasoHrs.SettingsPager.ShowNumericButtons = true;
            gvTraspasoHrs.SettingsPager.ShowSeparators = true;
            gvTraspasoHrs.SettingsPager.Summary.Visible = true;
            gvTraspasoHrs.SettingsPager.PageSizeItemSettings.Visible = true;
            gvTraspasoHrs.SettingsPager.PageSizeItemSettings.Position = PagerPageSizePosition.Right;
            gvTraspasoHrs.SettingsText.SearchPanelEditorNullText = "Ingrese la información a buscar";

			ObtieneValores();

			ApplySettings();
            if (!IsPostBack)
            {
				
			}

			if (eGetCliente != null)
				eGetCliente(this, e);
		}

        protected void Unnamed_Unload(object sender, EventArgs e)
        {
            MethodInfo methodInfo = typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Where(i => i.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel")).First();
            methodInfo.Invoke(ScriptManager.GetCurrent(Page),
                new object[] { sender as UpdatePanel });
        }

        protected void CancelEditing(CancelEventArgs e)
        {
            e.Cancel = true;
            gvTraspasoHrs.CancelEdit();
        }

        protected void gvTraspasoHrs_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {
            /*
            int iPos = 0;
            for (iPos = 0; iPos < DrPermisos[0].ItemArray.Length; iPos++)
            {
                switch (iPos)
                {
                    case 5: if (DrPermisos[0].ItemArray[iPos].S().I() == Convert.ToInt32(Enumeraciones.Bloqueo.Permitido))
                        {
                            if (e.ButtonType == ColumnCommandButtonType.New)
                                e.Enabled = true;
                        }
                        else
                        {
                            if (e.ButtonType == ColumnCommandButtonType.New)
                                e.Enabled = false;
                        } break;
                    case 6: if (DrPermisos[0].ItemArray[iPos].S().I() == Convert.ToInt32(Enumeraciones.Bloqueo.Permitido))
                        {
                            if (e.ButtonType == ColumnCommandButtonType.Edit)
                                e.Enabled = true;
                        }
                        else
                        {
                            if (e.ButtonType == ColumnCommandButtonType.Edit)
                                e.Enabled = false;
                        } break;
                    case 7: if (DrPermisos[0].ItemArray[iPos].S().I() == Convert.ToInt32(Enumeraciones.Bloqueo.Permitido))
                        {
                            if (e.ButtonType == ColumnCommandButtonType.Delete)
                                e.Enabled = true;
                        }
                        else
                        {
                            if (e.ButtonType == ColumnCommandButtonType.Delete)
                                e.Enabled = false;
                        } break;
                }
            }*/
        }

        protected void gvTraspasoHrs_CustomUnboundColumnData(object sender, ASPxGridViewColumnDataEventArgs e)
        {
            if (e.Column.FieldName == "Numero")
            {
                e.Value = (e.ListSourceRowIndex + 1).S();
            }
        }

		protected void gvTraspasoHrs_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
		{
			try
			{
				if (e.CommandArgs.CommandName == "Modifica")
				{
					object x = e.CommandArgs.CommandArgument.S();

					if (eGetCliente != null)
						eGetCliente(this, e);
					DataRowView row = gvTraspasoHrs.GetRow(e.VisibleIndex) as DataRowView;

					
					ddlClienteOrigen.SelectedItem = ddlClienteOrigen.Items.FindByText(row.Row.ItemArray[1].S());
					ddlClienteDestino.SelectedItem = ddlClienteDestino.Items.FindByText(row.Row.ItemArray[3].S());

					sClaveCliente = ddlClienteOrigen.SelectedItem.Text;
					if (eGetContrato != null)
						eGetContrato(sender, e);
					ddlContratoOrigen.Items.Clear();
					ddlContratoOrigen.DataSource = (DataTable)ViewState["Contrato"];
					ddlContratoOrigen.ValueField = "IdContrato";
					ddlContratoOrigen.ValueType = typeof(Int32);
					ddlContratoOrigen.TextField = "ClaveContrato";
					ddlContratoOrigen.DataBindItems();


					sClaveCliente = ddlClienteDestino.SelectedItem.Text;
					if (eGetContrato != null)
						eGetContrato(sender, e);
					ddlContratoDestino.Items.Clear();
					ddlContratoDestino.DataSource = (DataTable)ViewState["Contrato"];
					ddlContratoDestino.ValueField = "IdContrato";
					ddlContratoDestino.ValueType = typeof(Int32);
					ddlContratoDestino.TextField = "ClaveContrato";
					ddlContratoDestino.DataBindItems();

					txtTotalHoras.Value = row.Row.ItemArray[5];
					txtObservaciones.Value = row.Row.ItemArray[6];

					ddlContratoOrigen.ClientEnabled = true;
					ddlContratoDestino.ClientEnabled = true;
					ddlContratoOrigen.SelectedItem = ddlContratoOrigen.Items.FindByText(row.Row.ItemArray[2].S());
					ddlContratoDestino.SelectedItem = ddlContratoDestino.Items.FindByText(row.Row.ItemArray[4].S());
					
					if (eGetHorasDisponiblesOrigen != null)
						eGetHorasDisponiblesOrigen(null, EventArgs.Empty);
						
					if (eGetHorasDisponiblesDestino != null)
						eGetHorasDisponiblesDestino(null, EventArgs.Empty);

					HorasDispniblesOrigen.Value = ViewState["HorasDisponiblesOrigen"];
					HorasDispniblesDestino.Value = ViewState["HorasDisponiblesDestino"];

					ViewState["IsEditingIntercambioHoras"] = true;
					ViewState["EditId"] = row.Row.ItemArray[0];

					ppAgregar.ShowOnPageLoad = true;
					ppAgregar.PopupHorizontalAlign = PopupHorizontalAlign.WindowCenter;
					ppAgregar.PopupVerticalAlign = PopupVerticalAlign.Below;
				}
				else if (e.CommandArgs.CommandName == "Elimina")
				{
					eCrud = Enumeraciones.TipoOperacion.Eliminar;
					oCrud = e;

					if (eDeleteObj != null)
						eDeleteObj(sender, e);

					ObtieneValores();
					
				}
			}
			catch (Exception ex)
			{
				//Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvTraspasoHrs_RowCommand", "Aviso");
				throw ex;
			}
		}
		
		protected void gvTraspasoHrs_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
		{
			
		}

		protected void gvTraspasoHrs_RowDeleting(object sender, ASPxDataDeletingEventArgs e)
		{
			try
			{
				eCrud = Enumeraciones.TipoOperacion.Eliminar;
				oCrud = e;
				CancelEditing(e);

				if (eDeleteObj != null)
					eDeleteObj(sender, e);
			}
			catch (Exception ex)
			{
				Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvTraspasoHrs_RowDeleting", "Aviso");
			}
		}

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            if (eGetCliente != null)
                eGetCliente(this, e);

			ViewState["IsEditingIntercambioHoras"] = false;
			ViewState["EditId"] = 0;
			HorasDispniblesOrigen.Value = "";
            HorasDispniblesDestino.Value = "";
            ddlContratoDestino.SelectedItem = null;
            ddlClienteDestino.SelectedItem = null;
            ddlContratoOrigen.SelectedItem = null;
            ddlClienteOrigen.SelectedItem = null;
            txtTotalHoras.Value = "0:00";
            txtObservaciones.Value = "";
            ppAgregar.ShowOnPageLoad = true;
            ppAgregar.PopupHorizontalAlign = PopupHorizontalAlign.WindowCenter;
            ppAgregar.PopupVerticalAlign = PopupVerticalAlign.Below;
        }

        protected void btnBusqueda_Click(object sender, EventArgs e)
        {
            ObtieneValores();
        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            Exporter.WriteXlsToResponse(new XlsExportOptionsEx() { ExportType = ExportType.DataAware });
        }

        protected void btnGuardarTraspaso_Click(object sender, EventArgs e)
        {
            try
            {
				if (ViewState["IsEditingIntercambioHoras"].B())
				{
					eCrud = Enumeraciones.TipoOperacion.Actualizar;
					oCrud = e;

					if (eSaveObj != null)
						eSaveObj(sender, e);
				}
				else
				{
					eCrud = Enumeraciones.TipoOperacion.Insertar;
					oCrud = e;

					if (eNewObj != null)
						eNewObj(sender, e);
				}

                ppAgregar.ShowOnPageLoad = false;
				ObtieneValores();

			}
            catch (Exception ex) { Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnguardar_Click", "Aviso"); }
        }
        
        protected void ddlClienteOrigen_SelectedIndexChanged(object sender, EventArgs e)
        {
			if (ddlClienteOrigen.SelectedItem == null)
				return;
			sClaveCliente = ddlClienteOrigen.SelectedItem.Text;

            if (eGetContrato != null)
                eGetContrato(null, EventArgs.Empty);

            ddlContratoOrigen.Items.Clear();
            ddlContratoOrigen.DataSource = (DataTable)ViewState["Contrato"];
            ddlContratoOrigen.ValueField = "IdContrato";
            ddlContratoOrigen.ValueType = typeof(Int32);
            ddlContratoOrigen.TextField = "ClaveContrato";
            ddlContratoOrigen.DataBindItems();
            ddlContratoOrigen.ClientEnabled = true;
            ddlContratoOrigen.SelectedItem = null;
            HorasDispniblesOrigen.Value = "";
        }

        protected void ddlClienteDestino_SelectedIndexChanged(object sender, EventArgs e)
        {
			if (ddlClienteDestino.SelectedItem == null)
				return;
			sClaveCliente = ddlClienteDestino.SelectedItem.Text;

            if (eGetContrato != null)
                eGetContrato(null, EventArgs.Empty);

            ddlContratoDestino.Items.Clear();
            ddlContratoDestino.DataSource = (DataTable)ViewState["Contrato"];
            ddlContratoDestino.ValueField = "IdContrato";
            ddlContratoDestino.ValueType = typeof(Int32);
            ddlContratoDestino.TextField = "ClaveContrato";
            ddlContratoDestino.DataBindItems();
            ddlContratoDestino.ClientEnabled = true;
            ddlContratoDestino.SelectedItem = null;
            HorasDispniblesDestino.Value = "";
        }

        protected void ddlContratoOrigen_SelectedIndexChanged(object sender, EventArgs e)
        {
			if (ddlContratoOrigen.SelectedItem == null)
				return;

			if (eGetHorasDisponiblesOrigen != null)
                eGetHorasDisponiblesOrigen(null, EventArgs.Empty);

            HorasDispniblesOrigen.Value = ViewState["HorasDisponiblesOrigen"];
        }

        protected void ddlContratoDestino_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlContratoDestino.Value == null)
                return;
            
            if (eGetHorasDisponiblesDestino != null)
                eGetHorasDisponiblesDestino(null, EventArgs.Empty);

            HorasDispniblesDestino.Value = ViewState["HorasDisponiblesDestino"];
        }
        #endregion Eventos

        #region Metodos
        public void MostrarMensaje(string sMensaje, string sCaption)
        {
            popup.HeaderText = sCaption;

            mpeMensaje.ShowMessage(sMensaje, sCaption);
        }

        private void ApplySettings()
        {
            ApplySettingsInContainer(ppAgregar);
        }

        private void ApplySettingsInContainer(Control container)
        {
            foreach (Control child in container.Controls)
            {
                ASPxEdit edit = child as ASPxEdit;
                if (edit != null)
                {
                    edit.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.Text;
                    edit.ValidationSettings.ErrorTextPosition = ErrorTextPosition.Bottom;
                }
                else
                    ApplySettingsInContainer(child);
            }
        }
                                     
        public void LoadClientes(DataTable dtObjCliente)
        {
            ViewState["ClienteOrigen"] = dtObjCliente;

            ddlClienteOrigen.Items.Clear();
            ddlClienteDestino.Items.Clear();
            foreach (DataRow row in dtObjCliente.Rows)
            {
                ddlClienteOrigen.Items.Add(row[2].S(), row[0].S());
                ddlClienteDestino.Items.Add(row[2].S(), row[0].S());
            }   
        }

        public void LoadContrato(DataTable dtObjCat)
        {
            ViewState["Contrato"] = dtObjCat;            
            ddlContratoDestino.Items.Clear();            
        }

        public void LoadHorasDisponiblesOrigen(string sHoras)
        {
            ViewState["HorasDisponiblesOrigen"] = sHoras;
            HorasDispniblesOrigen.Value = sHoras;
        }

        public void LoadHorasDisponiblesDestino(string sHoras)
        {
            ViewState["HorasDisponiblesDestino"] = sHoras;
            HorasDispniblesDestino.Value = sHoras;
        }
        
        public void LoadIntercambioHoras(DataTable dtObjCat)
        {
            gvTraspasoHrs.DataSource = null;
            gvTraspasoHrs.DataSource = dtObjCat;
            gvTraspasoHrs.DataBind();
            ViewState["oDatos"] = dtObjCat;
        }
        
        public void ObtieneValores()
        {
            if (eGetIntercambioHoras != null)
                eGetIntercambioHoras(null, EventArgs.Empty);  
        }

        public void LoadActions(DataRow[] DrActions)
        {
            int iPos = 0;
            if (DrActions.Length == 0)
            {
                /*
                btnBusqueda.Enabled = false;
                txtTextoBusqueda.Enabled = false;
                ddlTipoBusqueda.Enabled = false;
                btnNuevo.Enabled = false;
                btnExcel.Enabled = false;
                btnNuevo2.Enabled = false;
                btnExportar.Enabled = false;
                */
                btnBusqueda.Enabled = true;
                txtTextoBusqueda.Enabled = true;
                ddlTipoBusqueda.Enabled = true;
                btnNuevo.Enabled = true;
                btnExcel.Enabled = true;
                btnNuevo2.Enabled = true;
                btnExportar.Enabled = true;
            }
            else
            {
                for (iPos = 0; iPos < DrActions[0].ItemArray.Length; iPos++)
                {
                    switch (iPos)
                    {
                        case 4: if (DrActions[0].ItemArray[iPos].S().I() == Convert.ToInt32(Enumeraciones.Bloqueo.Permitido))
                            {
                                btnBusqueda.Enabled = true;
                                txtTextoBusqueda.Enabled = true;
                                ddlTipoBusqueda.Enabled = true;
                                btnExcel.Enabled = true;
                                btnExportar.Enabled = true;
                            }
                            else
                            {
                                /*
                                btnBusqueda.Enabled = false;
                                txtTextoBusqueda.Enabled = false;
                                ddlTipoBusqueda.Enabled = false;
                                btnExcel.Enabled = false;
                                btnExportar.Enabled = false;
                                */
                                btnBusqueda.Enabled = true;
                                txtTextoBusqueda.Enabled = true;
                                ddlTipoBusqueda.Enabled = true;
                                btnExcel.Enabled = true;
                                btnExportar.Enabled = true;

                            }
                            break;
                        case 5: if (DrActions[0].ItemArray[iPos].S().I() == Convert.ToInt32(Enumeraciones.Bloqueo.Permitido))
                            {
                                btnNuevo.Enabled = true;
                                btnNuevo2.Enabled = true;
                            }
                            else
                            {
                                /*
                                btnNuevo.Enabled = false;
                                btnNuevo2.Enabled = false;
                                */
                                btnNuevo.Enabled = true;
                                btnNuevo2.Enabled = true;
                            }
                            break;
                    }
                }
            }

        }
        #endregion

        #region Variables
        public object[] oArrFiltroContrato
        {
            get
            {
                return new object[] { "@NombreCliente", "",
                                        "@CodigoCliente",sClaveCliente
                };
            }
        }

        public object[] oArrFiltroCliente
        {
            get { return new object[] { "@idCliente", 0 }; }
        }

        public object[] oArrFiltroHorasDisponiblesOrigen
        {
            get
            {
                int iIdCliente = ddlClienteOrigen.Value.S().I();
                int iIdContrato = ddlContratoOrigen.Value.S().I();
                return new object[] { iIdContrato, iIdCliente };
            }
        }

        public object[] oArrFiltroHorasDisponiblesDestino
        {
            get
            {
                int iIdCliente = ddlClienteDestino.Value.S().I();
                int iIdContrato = ddlContratoDestino.Value.S().I();
                return new object[] { iIdContrato, iIdCliente };
            }
        }

        public object[] oArrFiltroIntercambioHoras
        {
            get
            {
                int iOpcion = ddlTipoBusqueda.SelectedValue.I();
                string sclave = txtTextoBusqueda.Text;
				DateTime sFechaDesde = dtFechaDesde.Date == DateTime.MinValue ? DateTime.Parse("1/1/1753 12:00:00") : dtFechaDesde.Date;
				DateTime sFechaHasta = dtFechaHasta.Date == DateTime.MinValue ? DateTime.Parse("1/1/1753 12:00:00") : dtFechaHasta.Date;
				sclave = txtTextoBusqueda.Text;


				return new object[] { "@OPCION", iOpcion, "@Clave", sclave, "@FechaDesde", sFechaDesde, "@FechaHasta", sFechaHasta };
            }
        }

        public Enumeraciones.TipoOperacion eCrud
        {
            get { return (Enumeraciones.TipoOperacion)ViewState["eCrud"]; }
            set { ViewState["eCrud"] = value; }
        }

        public DataRow[] DrPermisos
        {
            get { return (DataRow[])Session["DrPermisos"]; }
            set { Session["DrPermisos"] = value; }
        } 

        public object oCrud
        {
            get { return ViewState["Crud"]; }
            set { ViewState["Crud"] = value; }
        }

        public TraspasoHoras oTraspasoHoras
        {
            get
            {
                TraspasoHoras oTraspaso = new TraspasoHoras();

				oTraspaso.iIdIntercambioHoras = ViewState["EditId"].I();
                oTraspaso.iIdClienteOrigen = ddlClienteOrigen.SelectedItem.Value.I();
                oTraspaso.iIdContratoOrigen = ddlContratoOrigen.SelectedItem.Value.I();
                oTraspaso.iIdClienteDestino = ddlClienteDestino.SelectedItem.Value.I();
                oTraspaso.iIdContratoDestino = ddlContratoDestino.SelectedItem.Value.I();
                oTraspaso.sHorasIntercambiadas = txtTotalHoras.Text;
                oTraspaso.sObservaciones = txtObservaciones.Text;

                return oTraspaso;
            }
        }

        protected string sClaveCliente = "";
        private const string sClase = "frmTraspasoHoras.aspx.cs";
        private const string sPagina = "frmTraspasoHoras.aspx";

        IntercambioHoras_Presenter oPresenter;
        public event EventHandler eGetCliente;
        public event EventHandler eNewObj;
        public event EventHandler eGetIntercambioHoras;
        public event EventHandler eGetContrato;
        public event EventHandler eGetHorasDisponiblesOrigen;
        public event EventHandler eGetHorasDisponiblesDestino;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        UserIdentity oUsuario = new UserIdentity();
		#endregion
	}
}