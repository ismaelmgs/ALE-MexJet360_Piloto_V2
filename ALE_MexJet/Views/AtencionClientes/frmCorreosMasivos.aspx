﻿<%@ Page Title="" Language="C#" MasterPageFile="~/ALE_Main.Master" AutoEventWireup="true" CodeBehind="frmCorreosMasivos.aspx.cs" Inherits="ALE_MexJet.Views.AtencionClientes.frmCorreosMasivos" EnableEventValidation="false" UICulture="es" Culture="es-MX" %>
<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Src="~/ControlesUsuario/ucModalMensaje.ascx" TagPrefix="uc1" TagName="ucModalMensaje" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<script type="text/javascript">
		function gvCorreosMasivos_OnCustomButtonClick(s, e) {
			if (e.buttonID == "btnVerPersonas") {
				s.GetRowValues(
					e.visibleIndex,
					"Destinatarios;" +
					"Copiados",
					OnGetRowValues);
				ppPersonas.Show();
                }
			else if (e.buttonID == "btnCancelar") {
			    if (confirm("¿Está seguro que desea cancelar el correo?")) {
			        e.processOnServer = true;
			            }
			        }
			else if (e.buttonID == "btnEditar")
			{
			    s.GetRowValues(e.visibleIndex, "IdCorreoMasivo", OnPrueba);
            }
		}
		function OnGetRowValues(values) {
			var strDestinatarios = values[0].replace(/ /g, "");
			var strCopiados = values[1].replace(/ /g, "");
			
			var destinatarios = strDestinatarios.split(",");
			var copiados = strCopiados.split(",");

			tkDestinatarios.SetTokenCollection(destinatarios);
			tkCopiados.SetTokenCollection(copiados);
		}
		function OnPrueba(val)
		{
		    console.log("copiados: %o", val);
		    window.location.assign("frmCorreoM.aspx?Id=" + val);
		}
	</script>
	<style type="text/css">
		.remove-button {
			display: none;
		}
		.token-text {
			margin: 0px 2px 0px 4px;
			padding: 3px 2px 3px 2px;
		}
		.dxeButtonEditSys td.dxictb > span {
			margin: 0px 3px 2px 0px;
		}
	</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<dx:ASPxPanel ID="ASPxPanel1" runat="server" Width="100%" BackColor="White" Style="border-radius: 21px;">
		<PanelCollection>
			<dx:PanelContent>
				<div class="row header1">
					<div class="col-md-12">
						<span class="FTitulo">&nbsp;&nbsp;Correos Masivos</span>
					</div>
				</div>
				<uc1:ucModalMensaje ID="mpeMensaje" runat="server" />
				<div class="well-g">
					<div class="row">
						<div class="col-md-12">
							<fieldset class="Personal">
								<legend>
									<span style="font-family: Helvetica, Arial,sans-serif; text-align: center;">Búsqueda General</span>
								</legend>
								<div class="col-sm-12">
									<div class="col-md-2"></div>
									<div class="col-md-4">
										<table>
											<tr>
												<td>
													<dx:ASPxLabel ID="lblFechaDesde" Text="Desde:" AssociatedControlID="dtFechaDesde" Theme="Office2010Black" runat="server">
													</dx:ASPxLabel>&nbsp;&nbsp;
												</td>
												<td>
													<dx:ASPxDateEdit ID="dtFechaDesde" NullText="Seleccione" Theme="Office2010Black" runat="server"
														ValidationSettings-ErrorDisplayMode="Text">
														<DropDownButton>
															<Image IconID="scheduling_calendar_16x16"></Image>
														</DropDownButton>
													</dx:ASPxDateEdit>
												</td>
											</tr>
											<tr><td colspan="2">&nbsp;</td></tr>
											<tr>
												<td>
													<dx:ASPxLabel ID="lblStatus" Text="Estatus:" AssociatedControlID="ddlStatus" Theme="Office2010Black" runat="server">
													</dx:ASPxLabel>&nbsp;&nbsp;
												</td>
												<td>
													<dx:ASPxComboBox ID="ddlStatus" runat="server" ValueType="System.Byte" Theme="Office2010Black">
														<Items>
															<dx:ListEditItem Value="0" Text="[Sin Filtro]" Selected="true" />
															<dx:ListEditItem Value="1" Text="En Proceso" />
															<dx:ListEditItem Value="2" Text="Enviado" />
															<dx:ListEditItem Value="3" Text="Cancelado" />
														</Items>
													</dx:ASPxComboBox>
												</td>
											</tr>
										</table>
									</div>
									<div class="col-md-4">
										<table>
											<tr>
												<td>
													<dx:ASPxLabel ID="lblFechaHasta" Text="Hasta:" AssociatedControlID="dtFechaHasta" Theme="Office2010Black" runat="server">
													</dx:ASPxLabel>&nbsp;&nbsp;
												</td>
												<td>
													<dx:ASPxDateEdit ID="dtFechaHasta" NullText="Seleccione" Theme="Office2010Black" runat="server"
														ValidationSettings-ErrorDisplayMode="Text">
														<DropDownButton>
															<Image IconID="scheduling_calendar_16x16"></Image>
														</DropDownButton>
													</dx:ASPxDateEdit>
												</td>
											</tr>
											<tr><td colspan="2">&nbsp;</td></tr>
											<tr>
												<td>
													
												</td>
												<td>
													
												</td>
											</tr>
										</table>
									</div>
									<div class="col-md-1">
									</div>
									<div class="col-md-1">
										<table>
											<tr><td>&nbsp;</td></tr>
											<tr><td>&nbsp;</td></tr>
											<tr>
												<td>
													<dx:ASPxButton ID="btnBuscar" Text="Buscar" OnClick="btnBuscar_Click" Theme="Office2010Black" runat="server">
													</dx:ASPxButton>
												</td>
											</tr>
										</table>

									</div>
								</div>
							</fieldset>
						</div>
					</div>
					<br />
					<div class="row">
						<div class="col-md-6">
							<dx:ASPxButton ID="btnNuevo" Text="Nuevo" Theme="Office2010Black" OnClick="btnNuevo_Click" runat="server">
							</dx:ASPxButton>
						</div>
						<div class="col-md-6" style="text-align: right;">
							<asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional" OnUnload="UpdatePanel_Unload">
								<ContentTemplate>
							<dx:ASPxLabel Theme="Office2010Black" Text="Exportar a:" runat="server"></dx:ASPxLabel>
							&nbsp;&nbsp;
							<dx:ASPxButton ID="btnExcel" Text="Excel" Theme="Office2010Black" OnClick="btnExcel_Click" runat="server">
							</dx:ASPxButton>
								</ContentTemplate>
								<Triggers>
                                    <asp:PostBackTrigger ControlID="btnExcel" />
                                </Triggers>
							</asp:UpdatePanel>
						</div>
					</div>
					<br />
					<div class="row">
						<div class="col-md-12">
							<asp:UpdatePanel ID="upGv" OnUnload="UpdatePanel_Unload" UpdateMode="Always" runat="server">
								<ContentTemplate>
									<asp:Panel ID="pnlCorreosMasivos" runat="server" Width="100%">
										<dx:ASPxGridView ID="gvCorreosMasivos" ClientInstanceName="gvCorreosMasivos"  AutoGenerateColumns="False" runat="server"
											Font-Size="Small" EnableTheming="True" Theme="Office2010Black" Width="100%" KeyFieldName="IdCorreosMasivos" 
											SettingsText-SearchPanelEditorNullText="Ingrese la información a buscar" EnableCallBacks="true" 
											OnCustomButtonCallback="gvCorreosMasivos_CustomButtonCallback" OnCustomButtonInitialize="gvCorreosMasivos_CustomButtonInitialize" OnCommandButtonInitialize="gvCorreosMasivos_CommandButtonInitialize">
											<Styles Header-HorizontalAlign="Center" Header-VerticalAlign="Middle"></Styles>
											<ClientSideEvents CustomButtonClick="gvCorreosMasivos_OnCustomButtonClick"  EndCallback="function (s, e) {
													if (s.cpShowPopup)
													{
														delete s.cpShowPopup;
														lbl.SetText(s.cpText);
														popup.Show();
													}
												}" />
											<Columns>
                                                <dx:GridViewDataTextColumn FieldName="IdCorreoMasivo" Caption="Motivo" Visible="false" VisibleIndex="0"></dx:GridViewDataTextColumn>
												<dx:GridViewDataTextColumn FieldName="Motivo" Caption="Motivo" VisibleIndex="1"></dx:GridViewDataTextColumn>
												<dx:GridViewDataTextColumn FieldName="Asunto" Caption="Asunto" VisibleIndex="2"></dx:GridViewDataTextColumn>
												<dx:GridViewDataTextColumn FieldName="Destinatarios" Caption="Destinatarios" VisibleIndex="3" Visible="false"></dx:GridViewDataTextColumn>
												<dx:GridViewDataTextColumn FieldName="Copiados" Caption="Copiados" VisibleIndex="4" Visible="false"></dx:GridViewDataTextColumn>
												<dx:GridViewDataTextColumn FieldName="Contenido" Caption="Contenido" VisibleIndex="5" Visible="false"></dx:GridViewDataTextColumn>
												<dx:GridViewDataTextColumn FieldName="Status" Caption="Estatus" VisibleIndex="6"></dx:GridViewDataTextColumn>
											
												<dx:GridViewCommandColumn ShowEditButton="false"  ButtonType="Button" Caption="Acciones" VisibleIndex="7" 
													ShowInCustomizationForm="True" >
													<CustomButtons>
														<dx:GridViewCommandColumnCustomButton ID="btnVerPersonas" Text="Ver Personas">
															<Image Height="20px" ToolTip="Ver Personas" Width="20px"></Image>
														</dx:GridViewCommandColumnCustomButton>
														<dx:GridViewCommandColumnCustomButton ID="btnCancelar" Text="Cancelar">
															<Image Height="20px" ToolTip="Cancelar" Width="20px"></Image>
														</dx:GridViewCommandColumnCustomButton>
                                                        <dx:GridViewCommandColumnCustomButton ID="btnEditar" Text="Editar">
															<Image Height="20px" ToolTip="Editar" Width="20px"></Image>
														</dx:GridViewCommandColumnCustomButton>
													</CustomButtons>
												</dx:GridViewCommandColumn>
											</Columns>
											<Settings ShowGroupPanel="true" />
											<SettingsSearchPanel Visible="true" />
											<SettingsPager Position="TopAndBottom">
												<PageSizeItemSettings Items="1, 10, 20, 50, 100" Visible="true"></PageSizeItemSettings>
											</SettingsPager>
											<SettingsCommandButton>
												<EditButton Text="Editar">
													<Image Height="20px" ToolTip="Editar" Width="20px"></Image>
												</EditButton>
												<CancelButton Text="Cancelar">
													<Image Height="20px" ToolTip="Cancelar" Width="20px"></Image>
												</CancelButton>
											</SettingsCommandButton>
										</dx:ASPxGridView>
										<dx:ASPxGridViewExporter ID="Exporter" runat="server" GridViewID="gvCorreosMasivos"></dx:ASPxGridViewExporter>
									</asp:Panel>
								</ContentTemplate>
								<Triggers>
									<asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
								</Triggers>
							</asp:UpdatePanel>
						</div>
					</div>
					<br />
					<div class="row">
						<div class="col-md-6">
							<dx:ASPxButton ID="btnNuevo2" Text="Nuevo" Theme="Office2010Black" OnClick="btnNuevo_Click" runat="server">
							</dx:ASPxButton>
						</div>
						<div class="col-md-6" style="text-align: right;">
							<asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Conditional" OnUnload="UpdatePanel_Unload">
								<ContentTemplate>
							<dx:ASPxLabel Theme="Office2010Black" Text="Exportar a:" runat="server"></dx:ASPxLabel>&nbsp;&nbsp;
							<dx:ASPxButton ID="btnExcel2" Text="Excel" Theme="Office2010Black" OnClick="btnExcel_Click" runat="server">
							</dx:ASPxButton>
								</ContentTemplate>
								<Triggers>
									<asp:PostBackTrigger ControlID="btnExcel2" />
								</Triggers>
							</asp:UpdatePanel>
						</div>
					</div>
				</div>
			</dx:PanelContent>
		</PanelCollection>
	</dx:ASPxPanel>

	<dx:ASPxPopupControl ClientInstanceName="ppPersonas" Width="500px" Theme="Office2010Black"
		ID="ppPersonas" runat="server"  Modal="True"   HeaderText="Personas" AllowDragging="true" 
		PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Below" >
		<ContentCollection>
			<dx:PopupControlContentControl runat="server">
				<asp:Panel ID="Panel1" runat="server">
						<table style="width:100%" class="popup-form">
							<tr>
								<th colspan="2">Enviado a:<br /></th>
							</tr>
							<tr class="popup-form-tr">
								<td>&nbsp;</td>
								<td>
									<dx:ASPxTokenBox ID="tkDestinatarios" ClientInstanceName="tkDestinatarios" runat="server" Width="100%" TextField="Email" Theme="Office2010Black" 
										ReadOnly="true">
										<TokenRemoveButtonStyle CssClass="remove-button"></TokenRemoveButtonStyle>
										<TokenStyle Border-BorderColor="#808080" Border-BorderStyle="Solid" Border-BorderWidth="1px"></TokenStyle>
										<TokenTextStyle CssClass="token-text"></TokenTextStyle>
									</dx:ASPxTokenBox>
								</td>
							</tr>
							<tr class="popup-form-tr">
								<td>&nbsp;</td>
								<td>&nbsp;</td>
							</tr>
							<tr>
								<th colspan="2">Con copia:<br /></th>
							</tr>
							<tr class="popup-form-tr">
								<td>&nbsp;</td>
								<td>
									<dx:ASPxTokenBox ID="tkCopiados" ClientInstanceName="tkCopiados" runat="server" Width="100%" TextField="Email" Theme="Office2010Black"
										ReadOnly="true">
										<TokenRemoveButtonStyle CssClass="remove-button"></TokenRemoveButtonStyle>
										<TokenStyle Border-BorderColor="#808080" Border-BorderStyle="Solid" Border-BorderWidth="1px" ></TokenStyle>
										<TokenTextStyle CssClass="token-text"></TokenTextStyle>
									</dx:ASPxTokenBox>
								</td>
							</tr>
							<tr class="popup-form-tr">
								<td>&nbsp;</td>
								<td>&nbsp;</td>
							</tr>
							<tr class="popup-form-tr">
								<td></td>
								<td>
									<dx:ASPxButton ID="ASPxButton1" Text="Salir" runat="server" Theme="Office2010Black" Enabled="true" AutoPostBack ="false">
									<ClientSideEvents Click="function(s, e) {ppPersonas.Hide();}" />
									</dx:ASPxButton>
								</td>
							</tr>
						</table>
				</asp:Panel>
			</dx:PopupControlContentControl>
		</ContentCollection>
		 <ClientSideEvents  />
	</dx:ASPxPopupControl>
</asp:Content>