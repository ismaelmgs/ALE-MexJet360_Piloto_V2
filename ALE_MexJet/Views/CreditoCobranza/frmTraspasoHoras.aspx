﻿<%@ Page Title="" Language="C#" MasterPageFile="~/ALE_Main.Master" AutoEventWireup="true" CodeBehind="frmTraspasoHoras.aspx.cs" Inherits="ALE_MexJet.Views.CreditoCobranza.frmTraspasoHoras" UICulture="es" Culture="es-MX" %>

<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Src="~/ControlesUsuario/ucModalMensaje.ascx" TagPrefix="uc1" TagName="ucModalMensaje" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function OnContratoDestinoValidation(s, e) {
            var idContratoDestino = e.value;
            var idContratoOrigen = ddlContratoOrigen.GetValue();
            if (!idContratoDestino)
                return;
            if (idContratoOrigen == idContratoDestino) {
                e.isValid = false;
                e.errorText = "El contrato destino tiene que ser distinto al de origen";
            }
        }
        function OnTotalHorasValidation(s, e) {
            var horas = e.value;
            if (!horas)
                return;
            if (horas == "0:00" || horas == "00:00" || horas == "000:00") {
                e.isValid = false;
                e.errorText = "la cantidad de horas tiene que ser mayor que 0";
            }
        }
        function OnClienteOrigenChanged(control) {
        	ddlContratoOrigen.SetEnabled(true);
			
        }
        function OnClienteDestinoChanged(control) {
            ddlContratoDestino.SetEnabled(true);
        }
    </script>
	<style>
		table.popup-form > thead > tr > td,
		table.popup-form > tbody > tr > td,
		table.popup-form > tfoot > tr > td,
		table.popup-form > tr > td{
			padding-top: 5px;
			padding-bottom: 5px;
			padding-left: 20px;
		}
		table.popup-form > thead > tr > th,
		table.popup-form > tbody > tr > th,
		table.popup-form > tfoot > tr > th,
		table.popup-form > tr > th {
			padding-top: 10px;
			padding-bottom: 10px;
		}
	</style>
	 <link href="../../css/disabled-styles.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<script type="text/javascript">         
		 function ShowLoginWindow(mensaje) {
			 lbl.SetText(mensaje);
			 ppAlert.Show();
		 }

		 function pageLoad(sender, args) {
			 txtTextoBusquedaHabilitar();
			 $('.combo').change(txtTextoBusquedaHabilitar);
		 };
		 function txtTextoBusquedaHabilitar() {
			 var filtro = $(".combo").find(':selected').val();
			 if (filtro == 0) {
				 $(".txtBusqueda").attr('disabled', '-1');
				 $(".txtBusqueda").val('');
			 }
			 if (filtro == 1 || filtro == 2 || filtro == 3 || filtro == 4)
				 $(".txtBusqueda").removeAttr('disabled');
		 }
		 function gvTraspasoHrs_OnCustomButtonClick(s, e) {
		 	if (e.buttonID == "btnModificar") {
		 		s.GetRowValues(e.visibleIndex, "IdIntercambioHoras;ClienteOrigen;ContratoOrigen;ClienteDestino;ContratoDestino;HorasIntercambiadas;Observaciones", OnGetRowValues);
		 		console.log("ddlClienteOrigen= %o", ddlClienteOrigen)
		 		//ddlClienteOrigen.SetValue()
		 		ppAgregar.SetHeaderText("Modificar");
		 		ppAgregar.Show();
		 		console.log("ppAgregar= %o", ppAgregar);
		 	}
		 }
		 function OnGetRowValues (value) {
		 	console.log(value);

		 	ddlClienteOrigen.SetSelectedIndex(ddlClienteOrigen.FindItemByText(value[1]).index);
		 	ddlClienteDestino.SetSelectedIndex(ddlClienteDestino.FindItemByText(value[3]).index);
		 	ddlContratoOrigen.SetEnabled(true);
		 	ddlContratoDestino.SetEnabled(true);
		 	ddlClienteOrigen.SelectedIndexChanged.FireEvent();
		 	ddlClienteDestino.SelectedIndexChanged.FireEvent();
		 	ddlContratoOrigen.SetSelectedIndex(ddlContratoOrigen.FindItemByText(value[2]).index);
		 	ddlContratoDestino.SetSelectedIndex(ddlContratoDestino.FindItemByText(value[4]).index);

		 	
		 }

	</script> 
	  <dx:ASPxPanel ID="ASPxPanel1" runat="server" Width="100%" BackColor="White" Style="border-radius: 21px;">
		<PanelCollection>
			<dx:PanelContent>               
		
				<div class="row header1">
					<div class="col-md-12">
						<span style="color: #ffffff; font-family: Helvetica, Arial,sans-serif; font-size: 25px;">&nbsp;&nbsp;Intercambio de horas</span>
					</div>
				</div>
				<uc1:ucModalMensaje ID="mpeMensaje" runat="server" />

				<div class="well-g">
					<div class="row">
						<div class="col-md-12">

							<fieldset class="Personal">
								<legend>
									   <span style="font-family: Helvetica, Arial,sans-serif; text-align: center;">Búsqueda general</span>
								</legend>
								<div class="col-sm-12">
									 <div class="col-lg-4">
										<asp:TextBox ID="txtTextoBusqueda" CssClass="txtBusqueda" placeholder ="Ingrese la información a buscar" runat="server" Width="170px"></asp:TextBox>
									</div>
									<div class="col-lg-4">
										<asp:DropDownList runat="server" CssClass="combo" ID="ddlTipoBusqueda">
											<asp:ListItem Text="[Sin Filtro]" Value="0" Selected="true"></asp:ListItem>
											<asp:ListItem Text="Clave cliente origen" Value="1"></asp:ListItem>
											<asp:ListItem Text="Clave cliente destino" Value="2"></asp:ListItem>
											<asp:ListItem Text="Clave contrato origen" Value="3"></asp:ListItem>
											<asp:ListItem Text="Clave contrato destino" Value="4"></asp:ListItem>                                            
										</asp:DropDownList>
									</div>
									<div class="col-lg-4">
										<dx:ASPxButton ID="btnBusqueda" Text="Buscar" runat="server" Theme="Office2010Black" OnClick="btnBusqueda_Click"></dx:ASPxButton>
										
									</div>
								</div>
								<br />
								<div class="col-sm-12">
									<div class="col-lg-4">
										<table>
											<tr>
												<td>
													<dx:ASPxLabel runat="server" ID="lblFechaDesde" Text="Desde:&nbsp;" Theme="Office2010Black"></dx:ASPxLabel>
												</td>
												<td>
													 <dx:ASPxDateEdit runat="server" ID="dtFechaDesde" NullText="Seleccione" Theme="Office2010Black" ValidationSettings-ErrorDisplayMode="Text">
														<DropDownButton>
															<Image IconID="scheduling_calendar_16x16"></Image>
														</DropDownButton>
														<ValidationSettings>
                                                
														</ValidationSettings>
													</dx:ASPxDateEdit>
												</td>
											</tr>
										</table>
									</div>
									<div class="col-lg-4">
										<table>
											<tr>
												<td>
													<dx:ASPxLabel runat="server" ID="lblFechaHasta" Text="Hasta:&nbsp;" Theme="Office2010Black"></dx:ASPxLabel>
												</td>
												<td>
													<dx:ASPxDateEdit runat="server" ID="dtFechaHasta" NullText="Seleccione" Theme="Office2010Black" ValidationSettings-ErrorDisplayMode="Text">
														<DropDownButton>
															<Image IconID="scheduling_calendar_16x16"></Image>
														</DropDownButton>
														<ValidationSettings>
                                                
														</ValidationSettings>
													</dx:ASPxDateEdit>
												</td>
											</tr>
										</table>
									</div>
									<div class="col-lg-4">
										
										
									</div>
								</div>
							</fieldset>
						</div>
					</div>
					<br />
					<div class="row">
						<div class="col-md-6">
							<dx:ASPxButton ID="btnNuevo" runat="server" Text="Nuevo" Theme="Office2010Black" OnClick="btnNuevo_Click" >   
								<ClientSideEvents />                             
							</dx:ASPxButton>
					   
						</div>
						<div class="col-md-6" style="text-align: right;">
							<dx:ASPxLabel runat="server" Theme="Office2010Black" Text="Exportar a:"></dx:ASPxLabel>
							&nbsp;<dx:ASPxButton ID="btnExcel" runat="server" Text="Excel" Theme="Office2010Black"  OnClick="btnExportar_Click" ></dx:ASPxButton>
						</div>
					</div>
					<br />
					<div class="row">
						<div class="col-md-12" style="margin-left: -15px; width: 103%;">
							<asp:UpdatePanel runat="server" ID="UpdatePanel" UpdateMode="Always" OnUnload="Unnamed_Unload"> 
								<ContentTemplate>
									<div class="col-sm-12">

										<dx:ASPxGridView ID="gvTraspasoHrs" runat="server" AutoGenerateColumns="False" Font-Size="Small"
											ClientInstanceName="gvTraspasoHrs" EnableTheming="True" KeyFieldName="IdIntercambioHoras" 
											Theme="Office2010Black" Width="100%" StylesPopup-EditForm-ModalBackground-Opacity="90" 
											OnCommandButtonInitialize="gvTraspasoHrs_CommandButtonInitialize" 
											OnCustomUnboundColumnData="gvTraspasoHrs_CustomUnboundColumnData" 
											OnRowDeleting="gvTraspasoHrs_RowDeleting" OnRowCommand="gvTraspasoHrs_RowCommand">
											<%-- OnRowCommand="gvTraspasoHrs_RowCommand"  --%>
											<ClientSideEvents CustomButtonClick="gvTraspasoHrs_OnCustomButtonClick" EndCallback="function (s, e) {
										if (s.cpShowPopup)
										{
											delete s.cpShowPopup;
											lbl.SetText(s.cpText);
											popup.Show();
										}
									}" />
											<Columns>
                                                <dx:GridViewDataTextColumn FieldName="Numero" VisibleIndex="0" UnboundType="String" >
                                                </dx:GridViewDataTextColumn>
												<dx:GridViewDataTextColumn FieldName="IdIntercambioHoras" ShowInCustomizationForm="True" Caption="Código Cliente" VisibleIndex="0" Visible="false">
													<PropertiesTextEdit MaxLength="5">                                      
													</PropertiesTextEdit>
													<EditFormSettings Visible="False" />
												</dx:GridViewDataTextColumn>
												
												<dx:GridViewDataTextColumn FieldName="ClienteOrigen" ShowInCustomizationForm="True" Caption="Cliente Origen" VisibleIndex="1">                                                  
													<EditFormSettings Visible="False" />
												</dx:GridViewDataTextColumn>

												<dx:GridViewDataComboBoxColumn FieldName="ContratoOrigen" ShowInCustomizationForm="True" Visible="true" VisibleIndex="2"  Caption="Contrato Origen">
													<PropertiesComboBox EnableSynchronization="False" IncrementalFilteringMode="StartsWith">                                                        
														<ClientSideEvents EndCallback="OnEndCallback"/>
													</PropertiesComboBox>
												<EditFormSettings Visible="true" Caption="Contrato Origen*"></EditFormSettings>                                                    
												</dx:GridViewDataComboBoxColumn>

												<dx:GridViewDataTextColumn FieldName="ClienteDestino" ShowInCustomizationForm="True" Caption="Cliente Destino" VisibleIndex="3">
													<EditFormSettings Visible="true" />
												</dx:GridViewDataTextColumn>

												<dx:GridViewDataTextColumn FieldName="ContratoDestino" ShowInCustomizationForm="True" Caption="Contrato Destino" VisibleIndex="4">
												   <EditFormSettings Visible="true" />
												</dx:GridViewDataTextColumn>

												<dx:GridViewDataTextColumn FieldName="HorasIntercambiadas" ShowInCustomizationForm="True" Caption="Total Horas" VisibleIndex="5">
													<PropertiesTextEdit MaxLength="200" MaskSettings-Mask="<0..999>:<00..59>" MaskSettings-IncludeLiterals="All">
                                                        <ClientSideEvents Validation="OnTotalHorasValidation"/>
														<ValidationSettings ErrorDisplayMode="Text" ErrorText="Error en la informaci&#243;n ingresada." ErrorTextPosition="Bottom" SetFocusOnError="True">
															<RequiredField IsRequired="True" ErrorText="El campo es requerido."></RequiredField>
														</ValidationSettings>
													</PropertiesTextEdit>
													<EditFormSettings Visible="true" />
												</dx:GridViewDataTextColumn>                                                

												<dx:GridViewDataTextColumn FieldName="FechaCreacion" ShowInCustomizationForm="True" Caption="Fecha Movimiento" VisibleIndex="6">
													<PropertiesTextEdit MaxLength="200">
														<ValidationSettings ErrorDisplayMode="Text" ErrorText="Error en la informaci&#243;n ingresada." ErrorTextPosition="Bottom" SetFocusOnError="True">
															<RequiredField IsRequired="True" ErrorText="El campo es requerido."></RequiredField>
															<RegularExpression ErrorText="El campo contiene informaci&#243;n inv&#225;lida."></RegularExpression>
														</ValidationSettings>
													</PropertiesTextEdit>
													<EditFormSettings Visible="False" />
												</dx:GridViewDataTextColumn>
												
												<dx:GridViewDataTextColumn FieldName="Observaciones" Visible="false" Caption="" VisibleIndex="7">
												   <EditFormSettings Visible="true" Caption=""  />                                                    
												</dx:GridViewDataTextColumn>

												
												<%--<dx:GridViewCommandColumn ButtonType="Button" Caption="Acciones" ShowDeleteButton="true" ShowEditButton="false" ShowNewButton="false" VisibleIndex="8">
													<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
													<CustomButtons>
                                                        <dx:GridViewCommandColumnCustomButton ID="btnModificar" Styles-Style-CssClass="FBotton" Text="Modificar" Image-ToolTip="Modificar" />
                                                    </CustomButtons>
												</dx:GridViewCommandColumn>--%>
												<dx:GridViewDataColumn Caption="Acciones" Visible="true" VisibleIndex="8" CellStyle-HorizontalAlign="Center">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <DataItemTemplate>
                                                        <dx:ASPxButton Text="Modificar" Theme="Office2010Black" ID="btnModificar" runat="server" CommandArgument='<%# Eval("IdIntercambioHoras") %>' CommandName="Modifica" AutoPostBack="true" ToolTip="Modificar registro" HorizontalAlign="Center">
                                                        </dx:ASPxButton>
                                                        <dx:ASPxButton Text="Eliminar" Theme="Office2010Black" ID="btneliminaI" runat="server" CommandArgument='<%# Eval("IdIntercambioHoras") %>' CommandName="Elimina" AutoPostBack="true" ToolTip="Eliminar" HorizontalAlign="Center">
                                                            <ClientSideEvents Click="function(s, e){  e.processOnServer = confirm('¿Está seguro que desea eliminar el registro?');}" />
                                                        </dx:ASPxButton>
														
                                                    </DataItemTemplate>
                                                    <EditFormSettings Visible="false" />
                                                </dx:GridViewDataColumn>
											</Columns>

											<SettingsBehavior ConfirmDelete="True" />
											<SettingsPager Position="TopAndBottom">
												<PageSizeItemSettings Items="1, 10, 20, 50, 100">
												</PageSizeItemSettings>
											</SettingsPager>
											<SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1"></SettingsEditing>
											<Settings ShowGroupPanel="True" />
											<SettingsText ConfirmDelete="¿Está seguro que desea eliminar el registro?" />
											<SettingsPopup>
												<EditForm HorizontalAlign="Center" VerticalAlign="Above" Modal="true" CloseOnEscape="True" Width="400px" />
											</SettingsPopup>
											<SettingsSearchPanel Visible="true" />   
											<SettingsCommandButton>
												<UpdateButton Text="Guardar"></UpdateButton>
												<CancelButton></CancelButton>
												<EditButton>
													<Image Height="20px" ToolTip="Modificar" Width="20px"></Image>
												</EditButton>
												<DeleteButton>
													<Image Height="20px" ToolTip="Eliminar" Width="20px"></Image>
												</DeleteButton>
											</SettingsCommandButton>
										</dx:ASPxGridView>

									  
										<dx:ASPxGridViewExporter ID="Exporter" runat="server" GridViewID="gvTraspasoHrs">
										</dx:ASPxGridViewExporter>
									</div>
								</ContentTemplate>
								<Triggers>
									<asp:AsyncPostBackTrigger ControlID="btnNuevo" EventName="Click" />
									<asp:AsyncPostBackTrigger ControlID="btnNuevo2" EventName="Click" />
									<asp:AsyncPostBackTrigger ControlID="btnBusqueda" EventName="Click" />
									<asp:PostBackTrigger ControlID="btnExcel" />
									<asp:PostBackTrigger ControlID="btnExportar" />
								</Triggers>
							</asp:UpdatePanel>

						</div>
					</div>
					<br />
					<div class="row">
						<div class="col-sm-6">
							<dx:ASPxButton ID="btnNuevo2" runat="server" Text="Nuevo" Theme="Office2010Black" OnClick="btnNuevo_Click"></dx:ASPxButton>
						</div>
						<div class="col-sm-6" style="text-align: right;">
							<dx:ASPxLabel runat="server" Theme="Office2010Black" Text="Exportar a:"></dx:ASPxLabel>
							&nbsp;<dx:ASPxButton ID="btnExportar" runat="server" Text="Excel" Theme="Office2010Black" OnClick="btnExportar_Click" ></dx:ASPxButton>
						</div>
					</div>
					<br />
				</div><br />
			</dx:PanelContent>
		</PanelCollection>
	</dx:ASPxPanel>


	 <dx:ASPxPopupControl ID="popup" runat="server" ClientInstanceName="popup" CloseAction="CloseButton" CloseOnEscape="true" Modal="True"
		PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Above" HeaderText="Aviso" AllowDragging="true" ShowCloseButton="true" Width="300">
		<ClientSideEvents PopUp="function(s, e) { tbLogin.Focus(); }" />
		<ContentCollection>
			<dx:PopupControlContentControl runat="server">
				<dx:ASPxPanel ID="ASPxPanel2" runat="server" DefaultButton="btOK">
					<PanelCollection>
						<dx:PanelContent runat="server">
							<table>
								<tr>
									<td>
										<dx:ASPxImage ID="ASPxImage2" runat="server" ShowLoadingImage="true" ImageUrl="~/img/iconos/Information2.ico"></dx:ASPxImage>
										<dx:ASPxTextBox ID="tbLogin" ReadOnly="true" Border-BorderStyle="None" Height="1px" runat="server" Width="1px" ClientInstanceName="tbLogin"></dx:ASPxTextBox>
									</td>
									<td>
										<dx:ASPxLabel ID="lbl" runat="server" ClientInstanceName="lbl" Text="ASPxLabel"></dx:ASPxLabel>
									</td>
								</tr>

								<tr>
									<td>
										<dx:ASPxButton ID="btOK" runat="server" Text="OK" Width="80px" AutoPostBack="false" Style="float: left; margin-right: 8px" TabIndex="0">
											<ClientSideEvents Click="function(s, e) {popup.Hide(); }" />
										</dx:ASPxButton>
									</td>
								</tr>
							</table>
							<div>
							</div>
							</div>
						</dx:PanelContent>
					</PanelCollection>
				</dx:ASPxPanel>
			</dx:PopupControlContentControl>
		</ContentCollection>
	</dx:ASPxPopupControl>


	 <dx:ASPxPopupControl ClientInstanceName="ppAgregar" Width="350px" Theme="Office2010Black"
		 ID="ppAgregar" runat="server"  Modal="True"   HeaderText="Nuevo elemento" AllowDragging="true" >
		<ContentCollection>
			<dx:PopupControlContentControl runat="server">
				<asp:Panel ID="Panel1" runat="server">
						<table style="width:100%" class="popup-form">
							<tr>
								<th colspan="2">Origen</th>
							</tr>
							<tr class="popup-form-tr">
								<td>Cliente Origen</td>
								<td>
									<dx:ASPxComboBox 
                                        ID="ddlClienteOrigen" ClientInstanceName="ddlClienteOrigen" runat="server" ClientEnabled="true" 
                                        Theme="Office2010Black" EnableTheming="True" NullText="Seleccione el Cliente"  
										DropDownStyle="DropDownList" IncrementalFilteringMode="StartsWith" 
                                        EnableSynchronization="False" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlClienteOrigen_SelectedIndexChanged">
                                        <ClientSideEvents SelectedIndexChanged="OnClienteOrigenChanged"  />
										<ValidationSettings>
											<RequiredField IsRequired="true" ErrorText="El Cliente de origen es obligatorio" />
										</ValidationSettings>
									</dx:ASPxComboBox>
								</td>                                                                                                                               
							</tr>                            
							<tr class="popup-form-tr">
								<td>Contrato Origen</td>
								<td>
									<dx:ASPxComboBox 
                                        ID="ddlContratoOrigen" ClientInstanceName="ddlContratoOrigen" runat="server" ClientEnabled="false"
                                        Theme="Office2010Black" EnableTheming="True" NullText="Seleccione el Contrato" 
									    DropDownStyle="DropDownList" IncrementalFilteringMode="StartsWith" 
                                        EnableSynchronization="False" AutoPostBack="true" 
                                        OnSelectedIndexChanged="ddlContratoOrigen_SelectedIndexChanged">
										<ValidationSettings>
											<RequiredField IsRequired="true" ErrorText="El Contrato de origen es obligatorio" />
										</ValidationSettings> 
									</dx:ASPxComboBox>
								</td>
							</tr>
							<tr class="popup-form-tr">
								<td>Horas Disponibles</td>
								<td>
                                    <dx:ASPxTextBox ID="HorasDispniblesOrigen" runat="server" Theme="Office2010Black" EnableTheming="true" Enabled="false" ></dx:ASPxTextBox>
								</td>
							</tr>
							<tr>
								<th colspan="2">Destino</th>
							</tr>
							<tr class="popup-form-tr">
								<td>Cliente Destino</td>
								<td>
									<dx:ASPxComboBox 
                                        ID="ddlClienteDestino" ClientInstanceName="ddlClienteDestino" runat="server" ClientEnabled="true"
                                        Theme="Office2010Black" EnableTheming="True" NullText="Seleccione el Cliente"  
									    DropDownStyle="DropDownList" IncrementalFilteringMode="StartsWith" 
                                        EnableSynchronization="False" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlClienteDestino_SelectedIndexChanged">   
                                        <ClientSideEvents SelectedIndexChanged="OnClienteDestinoChanged" />                                                
										<ValidationSettings>
											<RequiredField IsRequired="true" ErrorText="El Cliente destino es obligatorio" />
										</ValidationSettings>
									 </dx:ASPxComboBox>
								</td>
                            </tr>
                            <tr class="popup-form-tr">
								<td>Contrato Destino</td>
                                <td>
                                    <dx:ASPxComboBox 
                                        ID="ddlContratoDestino" ClientInstanceName="ddlContratoDestino" runat="server" ClientEnabled="false"
                                        Theme="Office2010Black" EnableTheming="True" NullText="Seleccione el Contrato" 
                                        DropDownStyle="DropDownList" IncrementalFilteringMode="StartsWith" 
                                        EnableSynchronization="False" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlContratoDestino_SelectedIndexChanged" >
                                    <ClientSideEvents Validation="OnContratoDestinoValidation"  />
                                    <ValidationSettings>
                                        <RequiredField IsRequired="true" ErrorText="El Contrato destino es obligatorio" />
                                    </ValidationSettings>                                                
                                    </dx:ASPxComboBox>
                                </td>
							</tr>
							<tr class="popup-form-tr">
								<td>Horas Disponibles</td>
								<td><dx:ASPxTextBox ID="HorasDispniblesDestino" runat="server" Theme="Office2010Black" EnableTheming="true" Enabled="false" ></dx:ASPxTextBox></td>
							</tr>
							<tr>
								<th colspan="2">Intercambio</th>
							</tr>
							<tr class="popup-form-tr">
								<td>Horas a Intercambiar </td>
								<td><dx:ASPxTextBox ID="txtTotalHoras" runat="server" Theme="Office2010Black" EnableTheming="true" MaskSettings-Mask="<0..999>:<00..59>" MaskSettings-IncludeLiterals="All" >
									<ClientSideEvents Validation="OnTotalHorasValidation" />
                                    <ValidationSettings>
										<RequiredField IsRequired="True" ErrorText="La cantidad de horas a intercambiar es necesaria" ></RequiredField>
									</ValidationSettings>
									</dx:ASPxTextBox>                               

								</td>
							</tr>
							<tr class="popup-form-tr">
								 <td>Observaciones</td>
								<td>
									<dx:ASPxMemo ID="txtObservaciones" runat="server" Theme="Office2010Black" EnableTheming="true" Height="71px">
										<ValidationSettings>
											<RegularExpression ValidationExpression="[a-zA-Z ñÑáéíóúÁÉÍÓÚ]*[0-9]*" ErrorText="El campo contiene información inválida."></RegularExpression>
										</ValidationSettings>
									</dx:ASPxMemo>
									<dx:ASPxHiddenField ID="IdIntercambioHoras" ClientInstanceName="IdIntercambioHoras" runat="server">
									</dx:ASPxHiddenField>
							</tr>
							<tr class="popup-form-tr">
								<td>&nbsp;</td>
								<td>&nbsp;</td>
							</tr>
							<tr class="popup-form-tr">
								<td><dx:ASPxButton ID="btnGuardarTraspaso" runat="server" Theme="Office2010Black" EnableTheming="true" Text="Guardar" OnClick="btnGuardarTraspaso_Click"></dx:ASPxButton></td>
								<td><dx:ASPxButton ID="btnCancelar" runat="server" Theme="Office2010Black" Enabled="true" Text="Cancelar" AutoPostBack ="false">
									<ClientSideEvents Click="function(s, e) {ppAgregar.Hide();}" />
									</dx:ASPxButton></td>
							</tr>
						</table>
				</asp:Panel>
			</dx:PopupControlContentControl>
		</ContentCollection>
		 <ClientSideEvents  />
	</dx:ASPxPopupControl>
	
</asp:Content>
