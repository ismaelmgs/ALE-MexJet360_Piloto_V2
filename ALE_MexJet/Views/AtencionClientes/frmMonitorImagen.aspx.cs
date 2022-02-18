using ALE_MexJet.DomainModel;
using ALE_MexJet.Interfaces;
using ALE_MexJet.Objetos;
using ALE_MexJet.Presenter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NucleoBase.Core;

using DevExpress.Export;
using DevExpress.XtraPrinting;
using System.ComponentModel;
using ALE_MexJet.ControlesUsuario;
using DevExpress.Web.Data;
using DevExpress.Web;
using ALE_MexJet.Clases;
using DevExpress.Utils;
using System.Reflection;
using System.Collections.Specialized;

namespace ALE_MexJet.Views.AtencionClientes
{
    public partial class frmMonitorImagen : System.Web.UI.Page, IViewImagen
    {
        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
            DrPermisos = Utils.ObtenerPermisos(Enumeraciones.Pantallas.MonitorImagen);
            LoadActions(DrPermisos);
            oPresenter = new Imagen_Presenter(this, new DBImagen());

           // CargaImagen();
            CargaPersonas();
            ObtieneValores();
            CargaPilotos();
                if (!IsPostBack)
                {
                    if (gvImagen.VisibleRowCount < 1)
                    {
                        ObtieneValores();
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "Page_Load", "Aviso");
            }
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                ObtieneValores();
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnBuscar_Click", "Aviso");
            }
        }
        protected void btnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                Exporter.WriteXlsToResponse(new XlsExportOptionsEx() { ExportType = ExportType.DataAware });
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnExportar_Click", "Aviso");
            }
        }
        protected void gvImagen_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
        {
            try
            {
                if (e.CommandArgs.CommandName.S() == "Seleccione")
                {
                  
                    this.ViewState["ID"] = e.CommandArgs.CommandArgument.S();
                    if (pSearchObj != null)
                        pSearchObj(null, EventArgs.Empty);

                    if (SearchObjPilot != null)
                        SearchObjPilot(e.CommandArgs.CommandArgument, e);

                    tPersonas.Visible = true;
                    tPreferencias.Visible = false;

                   // gvPersonas.Columns["IdPax"].Visible = false;
                    //ScriptManager.RegisterClientScriptBlock(this, typeof(frmMonitorImagen), "funcion", "showpp('Personas');", true);

                    var indx = e.KeyValue.ToString().Split('|');
                    bNewPasajero.Enabled = indx[1].ToString() == "ico_rojo.png" ? false : true;

                    ppImg.HeaderText = "Personas";
                    ppImg.ShowOnPageLoad = true;
                }
                else if (e.CommandArgs.CommandName.S() == "Preferencias")
                {
                    tPersonas.Visible = false;
                    tPreferencias.Visible = true;

                    hfidSolicitud["IdSolicitud"] = e.CommandArgs.CommandArgument.S();

                    if (SearchObC != null)
                        SearchObC(null, EventArgs.Empty);

                    //ScriptManager.RegisterClientScriptBlock(this, typeof(frmMonitorImagen), "funcion", "showpp('Preferencias');", true);
                    ppImg.HeaderText = "Preferencias";
                    ppImg.ShowOnPageLoad = true;
                }
                else if (e.CommandArgs.CommandName.S() == "Check")
                {
                    var indx = e.KeyValue.ToString().Split('|');
                    hfidSolicitud["IdSolicitud"] = e.CommandArgs.CommandArgument.S();

                    object x = indx[1].ToString() == "ico_rojo.png" ? "2" : "1";
                    hfCheck["OpcionCheck"] = x;

                    if (SearchObI != null)
                        SearchObI(x, EventArgs.Empty);

                    //ScriptManager.RegisterClientScriptBlock(this, typeof(frmMonitorImagen), "funcion", "showppC('Control de Comisariato e Imagen');", true);
                    ppCheck.HeaderText = "Control de Comisariato e Imagen";
                    ppCheck.ShowOnPageLoad = true;
                }
                else if (e.CommandArgs.CommandName.S() == "Interiores")
                {
                    
                    this.ViewState["ID"] = e.CommandArgs.CommandArgument.S();
                    ppNotaInteriores.ShowOnPageLoad = true;
                }
                
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvImagen_RowCommand", "Aviso");
            }
        }
        protected void UpdatePanel1_Unload(object sender, EventArgs e)
        {
            try
            {
                MethodInfo methodInfo = typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Where(i => i.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel")).First();
                methodInfo.Invoke(ScriptManager.GetCurrent(Page),
                new object[] { sender as UpdatePanel });
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "UpdatePanel1_Unload", "Aviso");
            }
        }
        protected void UpdatePanel3_Unload(object sender, EventArgs e)
        {
            try
            {
                MethodInfo methodInfo = typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Where(i => i.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel")).First();
                methodInfo.Invoke(ScriptManager.GetCurrent(Page),
                new object[] { sender as UpdatePanel });
            }
            catch (Exception ex)
            {
               Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "UpdatePanel3_Unload", "Aviso");
            }
        }
        protected void Pan_Unload(object sender, EventArgs e)
        {
            try
            {
                MethodInfo methodInfo = typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Where(i => i.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel")).First();
                methodInfo.Invoke(ScriptManager.GetCurrent(Page),
                    new object[] { sender as UpdatePanel });
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "Pan_Unload", "Aviso");
            }
        }
        protected void gvPersonas_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            try
            {
                e.Editor.ReadOnly = false;
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvPersonas_CellEditorInitialize", "Aviso");
            }
        }
        protected void gvPersonas_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
        {
            try
            {
                CancelEditing(e);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvPersonas_RowUpdating", "Aviso");
            }
        }
        protected void gvPersonas_BatchUpdate(object sender, ASPxDataBatchUpdateEventArgs e)
        {
            try
            {
                eCrud = Enumeraciones.TipoOperacion.Insertar;
                oCrud = e;

                if (IUpdObj != null)
                    IUpdObj(sender, e);

                ppImg.ShowOnPageLoad = false;
               ScriptManager.RegisterClientScriptBlock(this, typeof(frmMonitorImagen), "funcion", "hidepp();", true);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvPersonas_BatchUpdate", "Aviso");
            }
        }
        protected void gvImgD_BeforePerformDataSelect(object sender, EventArgs e)
        {
            try
            {
                ASPxGridView grid = sender as ASPxGridView;

                hfImagen["IdImagen"] = grid.GetMasterRowKeyValue().S();
                if (SearchObD != null)
                    SearchObD(sender, e);
            
                grid.DataSource = (DataTable)ViewState["oImagenD"];


                if (hfCheck["OpcionCheck"].S() == "1")
                {
                    grid.Columns["ObservacionesPos"].Visible = false;
                    grid.Columns["AbordadoPos"].Visible = false;
                }
                else if (hfCheck["OpcionCheck"].S() == "2")
                {
                    grid.Columns["ObservacionesPre"].Visible = false;
                    grid.Columns["AbordadoPre"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvImgD_BeforePerformDataSelect", "Aviso");
            }
        }
        protected void gvImgD_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            try
            {
                e.Editor.ReadOnly = false;

                if (e.Column.FieldName == "AbordadoPos" && hfCheck["OpcionCheck"].S() == "1")
                { e.Editor.ReadOnly = true; e.Editor.ClientEnabled = false; }
                if (e.Column.FieldName == "ObservacionesPos" && hfCheck["OpcionCheck"].S() == "1")
                { e.Editor.ReadOnly = true; e.Editor.ClientEnabled = false; }
                if (e.Column.FieldName == "ObservacionesPre" && hfCheck["OpcionCheck"].S() == "2")
                { e.Editor.ReadOnly = true; e.Editor.ClientEnabled = false; }
                if (e.Column.FieldName == "AbordadoPre" && hfCheck["OpcionCheck"].S() == "2")
                { e.Editor.ReadOnly = true; e.Editor.ClientEnabled = false; }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvImgD_CellEditorInitialize", "Aviso");
            }
        }
        protected void gvImgD_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
        {
            try
            {
                CancelEditing(e);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvImgD_RowUpdating", "Aviso");
            }
        }
        protected void gvImgD_BatchUpdate(object sender, ASPxDataBatchUpdateEventArgs e)
        {
            try
            {
                eCrud = Enumeraciones.TipoOperacion.Insertar;
                oCrud = e;

                object x = hfCheck["OpcionCheck"].S();

                if (SaveUpdaI != null)
                SaveUpdaI(x, e);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvImgD_BatchUpdate", "Aviso");
            }
        }
        protected void ASPxButton1_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/Views/AtencionClientes/frmMonitorImagen.aspx", false);
                //gvImagen.CancelEdit();
                //gvImgP.DataSource = null;
                //gvImgP.DataBind();
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvImgD_CellEditorInitialize", "Aviso");
            }
        }
        protected void bNewPasajero_Click(object sender, EventArgs e)
        {
            try
            {
                gvPasajeros.SettingsText.PopupEditFormCaption = "Formulario de Creación";
                gvPasajeros.AddNewRow();
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "bNewPasajero_Click", "Aviso");
            }
        }
        protected void gvPasajeros_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            try
            {
                e.Editor.ReadOnly = false;

                ASPxGridView gridView = (ASPxGridView)sender;

                if (gvPasajeros.IsNewRowEditing)
                {
                    eCrud = Enumeraciones.TipoOperacion.Insertar;
                }
                else
                {
                    eCrud = Enumeraciones.TipoOperacion.Actualizar;
                }
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvPasajeros_CellEditorInitialize", "Aviso");
            }
        }
        protected void gvPasajeros_RowDeleting(object sender, ASPxDataDeletingEventArgs e)
        {
            try
            {
                eCrud = Enumeraciones.TipoOperacion.Eliminar;
                oCrud = e;

                if (eEliminaPaxTramo != null)
                    eEliminaPaxTramo(sender, e);

                CancelEditing(e);

                ObtinePaxTramo(this.ViewState["ID"].S().I());

                gvPasajeros.DataSource = null;
                gvPasajeros.DataSource = (DataTable)ViewState["LoadPaxTramo"];
                gvPasajeros.DataBind();
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvPasajeros_RowDeleting", "Aviso");
            }
        }
        protected void gvPasajeros_RowInserting(object sender, ASPxDataInsertingEventArgs e)
        {
            try
            {
                eCrud = Enumeraciones.TipoOperacion.Insertar;
                e.NewValues["IdTramo"] = this.ViewState["ID"].S().I();
                e.NewValues["UserIdentity"] = ((UserIdentity)Session["UserIdentity"]).sUsuario.S();
                oCrud = e;

                if (e.NewValues.Count > 0)
                { 
                    // enviamos la notificacion a Despacho
                    if (eEnviaNotificacionDespacho != null)
                        eEnviaNotificacionDespacho(null, null);
                }

                if (eInsertaPaxTramo != null)
                    eInsertaPaxTramo(sender, e);
                ObtinePaxTramo2(this.ViewState["ID"].S().I());
                CancelEditing(e);
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvPasajeros_RowInserting", "Aviso");
            }
        }
        public void LoadCorreoDespacho(DataTable dtCorreoDespacho)
        {
            DataTable dt = dtCorreoDespacho;
            string Mensaje = string.Empty;
            string Vuelo = string.Empty;
            string From = string.Empty;
            string CC = string.Empty;

            Vuelo = Vuelo + " </table> ";
            Mensaje = Mensaje + Vuelo;

            string scorreo = Utils.ObtieneParametroPorClave("4");
            string sPass = Utils.ObtieneParametroPorClave("5");
            string sservidor = Utils.ObtieneParametroPorClave("6");
            string spuerto = Utils.ObtieneParametroPorClave("7");

            Mensaje = "<table style='border : 1px'><tr><td colspan='2'>Nuevos Pasajeros Incluidos</td></tr> " +
                " <tr>" + 
                    "<td>  </td> <td> </td>" +
                " </tr>" +
                " <tr>" +
                    "<td>  </td> <td> </td>" +
                " </tr>" +
                " <tr>" +
                    "<td>  </td> <td> </td>" +
                " </tr>" +  
                "</table>";

            From = dt.Rows[0]["AreaEmail"].S();

            CC = dt.Rows[0]["AreaEmailCC"].S();

            Utils.EnvioCorreo(sservidor, spuerto.S().I(), "Nuevo Pax.", From, Mensaje, "", scorreo, sPass, CC, "Atención a Despacho");
        }
        public void LoadCorreoAlta(DataSet dtObjCat)
        {
            try
            {
                //CorreoCliente(dtObjCat);
                //CorreoAsignacion(dtObjCat);

                DataSet ds = dtObjCat;
                DataTable cero = ds.Tables[0];
                DataTable uno = ds.Tables[1];
                DataTable dos = ds.Tables[2];
                string Mensaje = string.Empty;
                string Vuelo = string.Empty;
                string From = string.Empty;
                string CC = string.Empty;

                if (Session["hfEdicion"].S().Equals("1"))
                {
                    Mensaje = "<table style='border : 1px'><tr><td colspan='2'>Solicitudes de Vuelo</td></tr> " +
                    //"<tr><td>Nueva Solicitud de vuelo N° </td> <td> " + iIdSolicitud.S() + "</td> </tr>" +
                    " <tr><td>Ejecutivo</td> " +
                    "    <td>" + ((UserIdentity)Session["UserIdentity"]).sName + "</td></tr> <tr> <td>Cliente</td> " +
                    "    <td>" + uno.Rows[0]["CodigoCliente"].S() + "/" + uno.Rows[0]["ClaveContrato"].S() + "</td> " +
                    "</tr> <tr><td>Contacto</td> <td> " + uno.Rows[0]["Contacto"].S() + "</td></tr> " +
                    " <tr><td>Modelo Aeronave</td> <td> " + uno.Rows[0]["Descripcion"].S() + "</td> </tr>" +
                    "</table>";

                    From = cero.Rows[0]["AreaEmail"].S();

                    CC = cero.Rows[0]["AreaEmailCC"].S();
                }
                else if (Session["hfEdicion"].S().Equals("2"))
                {
                    Mensaje = "<table style='border : 1px'><tr><td colspan='2'>Solicitudes de Vuelo</td></tr>" +
                        //"<tr><td>Modificación a Solicitud de vuelo N° </td> <td> " + iIdSolicitud.S() + "</td> </tr>" +
                        " <tr><td>Ejecutivo</td><td>" + ((UserIdentity)Session["UserIdentity"]).sName + "</td></tr> " +
                        " <tr> <td>Cliente</td> <td>" + uno.Rows[0]["CodigoCliente"].S() + "/" + uno.Rows[0]["ClaveContrato"].S() + "</td> " +
                    "</tr> <tr><td>Contacto</td> <td> " + uno.Rows[0]["Contacto"].S() + "</td> </tr>" +
                    "</tr> <tr><td>Modelo Aeronave</td> <td> " + uno.Rows[0]["Descripcion"].S() + "</td> </tr>" +
                    "</table>";
                    From = cero.Rows[0]["AreaEmail"].S();
                    CC = cero.Rows[0]["AreaEmailCC"].S();
                }

                Vuelo = "<br/><br/> <table><tr><td style='width: 45px'>Origen</td><td style='width: 45px'>Destino</td><td style='width: 45px'>Fecha</td><td style='width: 45px'>Hora</td><td style='width: 45px'>Pax</td></tr> ";
                for (int x = 0; x < dos.Rows.Count; x++)
                {
                    Vuelo = Vuelo + "<tr style=\"border: medium groove #808080;\">" +
                    "<td > " + dos.Rows[x]["AeropuertoO"].S() + "</td>" +
                    "<td > " + dos.Rows[x]["AeropuertoD"].S() + "</td>" +
                    "<td > " + dos.Rows[x]["Fechavuelo"].S() + "</td>" +
                    "<td > " + dos.Rows[x]["HoraVuelo"].S() + "</td>" +
                    "<td > " + dos.Rows[x]["NoPax"].S() + "</td>" +
                    "</tr>";
                }
                Vuelo = Vuelo + " </table> ";
                Mensaje = Mensaje + Vuelo;

                string scorreo = Utils.ObtieneParametroPorClave("4");
                string sPass = Utils.ObtieneParametroPorClave("5");
                string sservidor = Utils.ObtieneParametroPorClave("6");
                string spuerto = Utils.ObtieneParametroPorClave("7");

                Utils.EnvioCorreo(sservidor, spuerto.S().I(), "SOLICITUD NUEVA.", From, Mensaje, "", scorreo, sPass, CC, "Atención a Clientes");
            }
            catch (Exception x)
            {
                throw x;
            }
        }
        protected void gvPasajeros_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
        {
            try
            {
                eCrud = Enumeraciones.TipoOperacion.Actualizar;
                oCrud = e;
                e.NewValues["UserIdentity"] = ((UserIdentity)Session["UserIdentity"]).sUsuario.S();
                if (eEditaPaxTramo != null)
                    eEditaPaxTramo(sender, e);

                ObtinePaxTramo2(this.ViewState["ID"].S().I());
                CancelEditing(e);

                Session["hfEdicion"] = "2";
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvPasajeros_RowUpdating", "Aviso");
            }
        }
        protected void gvPasajeros_StartRowEditing(object sender, ASPxStartRowEditingEventArgs e)
        {
            try
            {
                gvPasajeros.SettingsText.PopupEditFormCaption = "Formulario de Edición";
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "gvPasajeros_StartRowEditing", "Aviso");
            }
        }
        protected void gvPasajeros_RowValidating(object sender, ASPxDataValidationEventArgs e)
        {

        }
        protected void gvPasajeros_CancelRowEditing(object sender, ASPxStartRowEditingEventArgs e)
        {

        }
        protected void gvPasajeros_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {

        }
        protected void btnNuevoP_Click(object sender, EventArgs e)
        {
            try
            {
                ppAltaPasajeros.ShowOnPageLoad = true;
            }
            catch (Exception x)
            { Utils.SaveErrorEnBitacora(mpeMensaje, x.Message, sPagina, sClase, "btnNuevoP_Click", "Aviso"); }
        }
        protected void btnguardaP_Click(object sender, EventArgs e)
        {
            try
            {
                if (eInsertaPasajero != null)
                    eInsertaPasajero(null, null);

                txtApellidoP.Text = "";
                txtNombreP.Text = "";
            }
            catch(Exception ex)
            { Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnguardaP_Click", "Aviso"); }
        }
        protected void Pasajeros_OnItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            try
            {
                ASPxComboBox comboBox = (ASPxComboBox)source;
                string sFiltroAeropuerto = e.Filter;
                
                if (sFiltroAeropuerto.Length < 1)
                {
                    if (eBuscaPasajero != null)
                        eBuscaPasajero(string.Empty, e);
                }
                else
                {
                    if (eBuscaPasajero != null)
                        eBuscaPasajero(sFiltroAeropuerto, e);
                }

                DataTable DD = (DataTable)ViewState["CargaPasajeros"];
                if (DD.Rows.Count > 0)
                {
                    comboBox.DataSource = (DataTable)ViewState["CargaPasajeros"];
                    comboBox.ValueField = "NombrePax";
                    comboBox.TextField = "NombrePax";
                    comboBox.DataBindItems();
                }
                else
                    comboBox.JSProperties["cpV2"] = "1";
            }
            catch (Exception ex)
            {
                Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "Pasajeros_OnItemsRequestedByFilterCondition", "Aviso");
            }
        }
        protected void Pasajeros_OnItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {

        }
        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                int iId = Utils.ObtieneParametroPorClave("62").S().I(); 

                if (eInsertaInteriores != null)
                    eInsertaInteriores(null, null);

                if (eConsultaArea != null)
                    eConsultaArea(iId, null);

                mNotaI.Text = "";
            }
            catch (Exception ex)
             { Utils.SaveErrorEnBitacora(mpeMensaje, ex.Message, sPagina, sClase, "btnAceptar_Click", "Aviso"); } 
        }
        #endregion

        #region "METODOS"
        public void CargaPasajeros(DataTable DT)
        {
            try
            {
                ViewState["CargaPasajeros"] = DT;
            }
            catch (Exception x) { throw x; }
        }
        public void LoadPaxTramo(DataTable dtObjCat)
        {
            try
            {
                ViewState["LoadPaxTramo"] = dtObjCat;
            }
            catch (Exception x)
            {
                throw x;
            }
        }
        public void LoadPaxTramo2(DataTable dtObjCat)
        {
            try
            {
                ViewState["oPersona"] = dtObjCat;
                gvPasajeros.DataSource = null;
                gvPasajeros.DataSource = dtObjCat;
                gvPasajeros.DataBind();
            }
            catch (Exception x)
            {
                throw x;
            }
        }
        protected void ObtinePaxTramo(int IdTramo)
        {
            try
            {
                if (eObtienePaxTramo != null)
                    eObtienePaxTramo(IdTramo, EventArgs.Empty);
            }
            catch (Exception x)
            {
                throw x;
            }
        }
        protected void ObtinePaxTramo2(int IdTramo)
        {
            try
            {
                if (eLoadPaxTramo2 != null)
                    eLoadPaxTramo2(IdTramo, EventArgs.Empty);
            }
            catch (Exception x)
            {
                throw x;
            }
        }
        public void ObtieneValores()
        {
            try
            {
                if (eSearchObj != null)
                    eSearchObj(null, EventArgs.Empty);

                //gvImagen.Columns["IdSolicitud"].Visible = false;
            }
            catch (Exception x) { throw x; }
        }
        public void LoadObjects(DataTable dtObject)
        {
            try
            {
                gvImagen.DataSource = null;
                ViewState["oDatos"] = null;

                gvImagen.DataSource = dtObject;
                ViewState["oDatos"] = dtObject;

                gvImagen.DataBind();
            }
            catch (Exception x) { throw x; }
        }
        public void LoadPersona(DataTable dtObject)
        {
            try
            {
                ViewState["oPersona"] = dtObject;
                gvPasajeros.DataSource = null;
                gvPasajeros.DataSource = dtObject;
                gvPasajeros.DataBind();
            }
            catch (Exception x) { throw x; }
        }
        public void LoadCliente(DataTable dtObject)
        {
            try
            {
                ViewState["oCliente"] = dtObject;

                if (dtObject != null && dtObject.Rows.Count > 0)
                {
                    mPreferencia.Text = dtObject.Rows[0]["Observaciones"].S();
                    mNota.Text = dtObject.Rows[0]["NOTAS"].S();
                    mComisariato.Text = dtObject.Rows[0]["COMISARIATO"].S();
                }
            }
            catch (Exception x) { throw x; }
        }
        public void LoadImagen(DataTable dtObject)
        {
            try
            {
                ViewState["oImagen"] = dtObject;
                gvImgP.DataSource = dtObject;
                gvImgP.DataBind();
            }
            catch (Exception x) { throw x; }
        }
        public void LoadImagenD(DataTable dtObjCat)
        {
            try
            {
                ViewState["oImagenD"] = dtObjCat;
                //gvImgD.DataSource = dtObjCat;
                //gvImgD.DataBind();
            }
            catch (Exception x) { throw x; }
        }
        protected void CargaImagen()
        {
            try
            {
                DataTable dt = (DataTable)ViewState["oImagen"];

                if (dt != null && dt.Rows.Count > 0)
                {
                    gvImgP.DataSource = dt;
                    gvImgP.DataBind();
                }
            }
            catch (Exception x) { throw x; }
        }

        protected static int iCantidadPaxActual = 0;
        protected static int iCantidadPaxDespues;
        protected static int iIdTramoSolicitud;

        protected void CargaPersonas()
        {
            try
            {
                ObtinePaxTramo2(this.ViewState["ID"].S().I());
                //DataTable dt = (DataTable)ViewState["oPersona"];

                //if (dt != null && dt.Rows.Count > 0)
                //{
                //    gvPasajeros.DataSource = null;
                //    gvPasajeros.DataSource = dt;
                //    gvPasajeros.DataBind();
                //    iIdTramoSolicitud = Convert.ToInt32(dt.Rows[0]["idTramo"].S());
                //    iCantidadPaxActual = dt.Rows.Count; //Obtenemos los pasajeros actuales
                //    //gvPasajeros.Columns["IdPax"].Visible = false;
                //}
            }
            catch (Exception x) { throw x; }
        }
        public void MostrarMensaje(string sMensaje, string sCaption)
        {
            try
            {
                popup.HeaderText = sCaption;
                lbl.Text = sMensaje;
                popup.ShowOnPageLoad = true;
               // mpeMensaje.ShowMessage(sMensaje, sCaption);
            }
            catch (Exception x) { throw x; }
        }
        protected void CancelEditing(CancelEventArgs e)
        {
            try
            {
                e.Cancel = true;
                gvImagen.CancelEdit();
                gvPasajeros.CancelEdit();
                ppImg.ShowOnPageLoad = false;
            }
            catch (Exception x) { throw x; }
        }
        void AddError(Dictionary<GridViewColumn, string> errors, GridViewColumn column, string errorText)
        {
            try
            {
                if (errors.ContainsKey(column)) return;
                errors[column] = errorText;
            }
            catch (Exception x) { throw x; }
        }
        public void LoadPilot(DataTable dtObjCat)
        {
            try
            {
                ViewState["oPilot"] = dtObjCat;
                gvTripulacion.DataSource = dtObjCat;
                gvTripulacion.DataBind();
            }
            catch (Exception x) { throw x; }
        }
        public void CargaPilotos()
        {
            try
            {
                DataTable dt = (DataTable)ViewState["oPilot"];

                if (dt != null && dt.Rows.Count > 0)
                {
                    gvTripulacion.DataSource = dt;
                    gvTripulacion.DataBind();
                }
            }
            catch (Exception x) { throw x; }
        }
        public void LoadActions(DataRow[] DrActions)
        {
            try
            {
                int iPos = 0;
                if (DrActions.Length == 0)
                {
                    btnBusqueda.Enabled = false;
                    txtTextoBusqueda.Enabled = false;
                    ddlTipoBusqueda.Enabled = false;
                    deFecha.Enabled = false;
                    btnExcel.Enabled = false;
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
                                    deFecha.Enabled = true;
                                }
                                else
                                {
                                    btnBusqueda.Enabled = false;
                                    txtTextoBusqueda.Enabled = false;
                                    ddlTipoBusqueda.Enabled = false;
                                    btnExcel.Enabled = false;
                                    deFecha.Enabled = false;
                                } break;
                        }
                    }
                }
            }
            catch (Exception x)
            {
                throw x;
            }

        }
        protected void CorreoAsignacion(DataTable Data)
        {
            try
            {
                string Mensaje = string.Empty;
                string Vuelo = string.Empty;
                string From = string.Empty;
                string CC = string.Empty;

                if (Data != null && Data.Rows.Count > 0)
                {
                    Mensaje = @"<table style='border : 1px' style='text-align:justify'>" +
                    "<tr><td colspan='2'>Incidencia Interiores</td></tr>" +
                    "<tr><td colspan='2'>" +
                    mNotaI.Text +
                    "</td> </tr>" +
                    "</table>";

                    From = Data.Rows[0]["AreaEmail"].S();
                    CC = Data.Rows[0]["AreaEmailCC"].S();

                    string scorreo = Utils.ObtieneParametroPorClave("4");
                    string sPass = Utils.ObtieneParametroPorClave("5");
                    string sservidor = Utils.ObtieneParametroPorClave("6");
                    string spuerto = Utils.ObtieneParametroPorClave("7");

                    Utils.EnvioCorreo(sservidor, spuerto.S().I(), "Interiores", From, Mensaje, "", scorreo, sPass, CC, "Monitor Imagen");
                }
            }
            catch (Exception x) { throw x; }
        }
        public void LoadArea(DataTable dtObjCat)
        {
            try
            {
                CorreoAsignacion(dtObjCat);
            }
            catch (Exception x) { throw x; }
        }
        #endregion

        #region "Vars y Propiedades"

        private const string sClase = "frmMonitorImagen.aspx.cs";
        private const string sPagina = "frmMonitorImagen.aspx";

        Imagen_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler pSearchObj;
        public event EventHandler IUpdObj;
        public event EventHandler SearchObC;
        public event EventHandler SearchObI;
        public event EventHandler SearchObD;
        public event EventHandler SaveUpdaI;
        public event EventHandler SearchObjPilot;

        public event EventHandler eEditaPaxTramo;
        public event EventHandler eEliminaPaxTramo;
        public event EventHandler eInsertaPasajero;
        public event EventHandler eInsertaPaxTramo;
        public event EventHandler eObtienePaxTramo;
        public event EventHandler eLoadPaxTramo2;
        public event EventHandler eBuscaPasajero;
        public event EventHandler eInsertaInteriores;
        public event EventHandler eConsultaArea;
        public event EventHandler eEnviaNotificacionDespacho;

        UserIdentity oUsuario = new UserIdentity();
        public DataRow[] DrPermisos
        {
            get { return (DataRow[])Session["DrPermisos"]; }
            set { Session["DrPermisos"] = value; }
        }
        public object oCrud
        {
            get { return ViewState["CrudPiloto"]; }
            set { ViewState["CrudPiloto"] = value; }
        }
        public bool bDuplicado
        {
            get
            {
                return (bool)ViewState["RegistroDuplicado"];
            }
            set
            {
                ViewState["RegistroDuplicado"] = value;
            }
        }
        public ALE_MexJet.Objetos.Enumeraciones.TipoOperacion eCrud
        {
            get { return (ALE_MexJet.Objetos.Enumeraciones.TipoOperacion)ViewState["eCrud"]; }
            set { ViewState["eCrud"] = value; }
        }
        public object[] oArrFiltros
        {
            get
            {
                string sDescripcion = string.Empty;

                switch (ddlTipoBusqueda.Value.S())
                {
                    case "1":
                        sDescripcion = txtTextoBusqueda.Text.S();
                        break;
                    case "2":
                        sDescripcion = string.Empty;
                        break;
                    case "3":
                        sDescripcion = string.Empty;
                        break;
                }

                return new object[]{
                                        "@Descripcion", "%" + sDescripcion + "%"
                                   };
            }
        }
        public object[] oInsertaPasajero
        {
            get
            {
                return new object[] { 
                                        "@last_name",  txtApellidoP.Text,
                                        "@first_name",txtNombreP.Text,
                                        "@UsuarioCreacion", "",
                                        "@IP", ""
                                    };
            }
        }
        public object[] oNotas
        {
            get
            {
                return new object[] { 
                                        "@IdTramo",  ViewState["ID"].S().I(),
                                        "@Notas",mNotaI.Text,
                                        "@UsuarioCreacion", "",
                                        "@IP", ""
                                    };
            }
        }
        public object[] oArrFilUpd
        {
            get
            {
                return new object[] { 
                                        "@IdSolicitud", 0,
                                        "@Dictamen",  0,
                                        "@Observaciones", "",
                                        "@OrigenSolicitud", "Mex Jet 360",
                                        "@Usuario", ((UserIdentity)Session["UserIdentity"]).sUsuario.S(),
                                        "@IP", "",
                                        "@TramoSolicitud",iIdTramoSolicitud
                                    };
            }
        }
        public Imagen oBusqueda
        {
            get
            {
                Imagen I = new Imagen();
                switch (ddlTipoBusqueda.Value.S())
                {
                    case "1":
                        I.ClaveContrato = txtTextoBusqueda.Text.S();
                        break;
                    case "2":
                        I.Matricula = txtTextoBusqueda.Text.S();
                        break;
                    case "3":
                        I.Cliente = txtTextoBusqueda.Text.S();
                        break;
                }

                if (deFecha.Text.S() != string.Empty)
                { I.Fecha = deFecha.Date.ToString("MM/dd/yyyy"); }

                I.UsuarioModificacion = ((UserIdentity)Session["UserIdentity"]).sUsuario.S();
                return I;
            }
        }
        public Imagen pBusqueda
        {
            get
            {
                Imagen I = new Imagen();
                I.IdTramo = this.ViewState["ID"].S();
                return I;
            }
        }
        public Imagen BusquedaC
        { get 
        {
            Imagen I = new Imagen();
            I.IdSolicitud = hfidSolicitud["IdSolicitud"].S();
            return I;
        }
        }
        public Imagen BusquedaI 
        {
            get
            {
                Imagen i = new Imagen();
                i.IdPadre = hfImagen["IdImagen"].S().I();
                hfImagen["IdImagen"] = null;
                i.IdTramo = hfidSolicitud["IdSolicitud"].S();
                return i;
            }
        }
        #endregion

    }
}