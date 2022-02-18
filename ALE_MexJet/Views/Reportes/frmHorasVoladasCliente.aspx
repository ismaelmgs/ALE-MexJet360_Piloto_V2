<%@ Page Title="" Language="C#" MasterPageFile="~/ALE_Main.Master" AutoEventWireup="true" CodeBehind="frmHorasVoladasCliente.aspx.cs" Inherits="ALE_MexJet.Views.Reportes.frmHorasVoladasCliente" UICulture="es" Culture="es-MX" %>

<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Src="~/ControlesUsuario/ucModalMensaje.ascx" TagPrefix="uc1" TagName="ucModalMensaje" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content runat="server" ContentPlaceHolderID="head" ID="Content1">
	<style>
		.tblRpt {
			font-size:10.0pt; font-family:Calibri; background:white; width:100%; border: 1px solid;
		}
		.tblRpt td, .tblRpt th {
			padding-left:4px; padding-right:4px;
		}
		.tblRpt th {
			text-align:center; border-left:none; border-right:none; text-align: center;
		}
		.tblRpt th.head-1 {
			border-top: 1px solid; border-bottom:none;
		}
		.tblRpt th.head-2 {
			border-top:none; border-bottom:1px solid;
		}
		.tblRpt td.no-border, .tblRpt th.no-border {
			border:none;
		}
		.tblRpt td.border-top, .tblRpt th.border-top {
			border-top:1px solid; border-right:none; border-bottom:none; border-left:none;
		}
		.tblRpt td.border-bottom, .tblRpt th.border-bottom {
			border-top:none; border-right:none; border-bottom:1px solid; border-left:none;
		}
		.tblRpt td.align-right, .tblRpt th.align-right {
			text-align: right;
		}
	</style>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content2">
	<dx:ASPxPanel ID="ASPxPanel1" runat="server" Width="100%" BackColor="White" Style="border-radius: 21px;">
		<PanelCollection>
			<dx:PanelContent>
				<div class="row header1">
					<div class="col-md-12">
						<span class="FTitulo">&nbsp;&nbsp;Horas Voladas por Cliente</span>
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
													<dx:ASPxLabel ID="lblCliente" Text="Cliente:" AssociatedControlID="ddlClientes" Theme="Office2010Black" runat="server">
													</dx:ASPxLabel>&nbsp;&nbsp;
												</td>
												<td>
													<dx:ASPxComboBox ToolTip="Cliente" ID="ddlClientes" runat="server" Theme="Office2010Black" EnableTheming="True" NullText="Seleccione un Cliente" AutoPostBack="true" OnSelectedIndexChanged="ddlClientes_SelectedIndexChanged">
														<ValidationSettings ErrorDisplayMode="Text" ErrorTextPosition="Bottom">
															<RequiredField IsRequired="true" ErrorText="Seleccione un Cliente" />
														</ValidationSettings>
													</dx:ASPxComboBox>
												</td>
											</tr>
											<tr><td colspan="2">&nbsp;</td></tr>
											<tr>
												<td>
													<dx:ASPxLabel ID="lblFechaDesde" Text="Desde:" AssociatedControlID="dtFechaDesde" Theme="Office2010Black" runat="server">
													</dx:ASPxLabel>
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
													<dx:ASPxLabel ID="lblCosto" Text="Costo por Vuelo:" AssociatedControlID="chkCosto" Theme="Office2010Black" runat="server">
													</dx:ASPxLabel>&nbsp;&nbsp;
												</td>
												<td>
													<dx:ASPxCheckBox ID="chkCosto" Theme="Office2010Black" runat="server"></dx:ASPxCheckBox>
												</td>
											</tr>
										</table>
									</div>
									<div class="col-md-4">
										<table>
											<tr>
												<td>
													<dx:ASPxLabel Text="Contrato:" ID="lblContrato" Theme="Office2010Black"  runat="server"></dx:ASPxLabel>&nbsp;&nbsp;
												</td>
												<td>
													<asp:UpdatePanel runat="server" ID="UpdatePanel3" UpdateMode="Conditional" OnUnload="UpdatePanel_Unload">
														<ContentTemplate>
															<dx:ASPxComboBox ToolTip="Contrato" ID="ddlContrato" NullText="Seleccione un Contrato" runat="server" Theme="Office2010Black" EnableTheming="True">
																<ValidationSettings ErrorDisplayMode="Text" ErrorTextPosition="Bottom">
																	<RequiredField IsRequired="true" ErrorText="Seleccione un Contrato" />
																</ValidationSettings>
															</dx:ASPxComboBox>
														</ContentTemplate>
														<Triggers>
															<asp:AsyncPostBackTrigger ControlID="ddlClientes" EventName="SelectedIndexChanged" />
														</Triggers>
													</asp:UpdatePanel>
												</td>
											</tr>
											<tr><td colspan="2">&nbsp;</td></tr>
											<tr>
												<td>
													<dx:ASPxLabel ID="lblFechaHasta" Text="Hasta:" AssociatedControlID="dtFechaHasta" Theme="Office2010Black" runat="server">
													</dx:ASPxLabel>
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
										</table>
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
									<div class="col-md-1"></div>
								</div>
							</fieldset>
						</div>
					</div>
					<br />
					<div class="row">
						<div class="col-md-6">
							
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
									<asp:Panel ID="pnlHorasVoladas" runat="server" Width="100%">
										<div id="HorasVoladas" runat="server"></div>
										<%--
										<dx:ASPxGridView ID="gvHorasVoladas" ClientInstanceName="gvHorasVoladas" AutoGenerateColumns="False" runat="server"
											Font-Size="Small" EnableTheming="True" Theme="Office2010Black" Width="100%" KeyFieldName="IdRemision"
											SettingsText-SearchPanelEditorNullText="Ingrese la información a buscar" EnableCallBacks="true">
											<Styles Header-HorizontalAlign="Center" Header-VerticalAlign="Middle"></Styles>
											<ClientSideEvents 
												EndCallback="function (s, e) {
													if (s.cpShowPopup)
													{
														delete s.cpShowPopup;
														lbl.SetText(s.cpText);
														popup.Show();
													}
												}" />
											<Columns>
												<dx:GridViewDataTextColumn VisibleIndex="0" Visible="true" FieldName="Fecha" Caption="Fecha"></dx:GridViewDataTextColumn>
												<dx:GridViewDataTextColumn VisibleIndex="1" Visible="true" FieldName="Mes" Caption="Mes"></dx:GridViewDataTextColumn>
												<dx:GridViewDataTextColumn VisibleIndex="2" Visible="true" FieldName="Matricula" Caption="Matricula"></dx:GridViewDataTextColumn>
												<dx:GridViewDataTextColumn VisibleIndex="3" Visible="true" FieldName="IdRemision" Caption="Id Remision"></dx:GridViewDataTextColumn>
												<dx:GridViewDataTextColumn VisibleIndex="4" Visible="true" FieldName="Ruta" Caption="Ruta"></dx:GridViewDataTextColumn>
												<dx:GridViewDataTextColumn VisibleIndex="5" Visible="true" FieldName="VueloNacional" Caption="Vuelo Nacional"></dx:GridViewDataTextColumn>
												<dx:GridViewDataTextColumn VisibleIndex="6" Visible="true" FieldName="VueloInternacional" Caption="Vuelo Internacional"></dx:GridViewDataTextColumn>
												<dx:GridViewDataTextColumn VisibleIndex="7" Visible="true" FieldName="EsperaNacional" Caption="Espera Nacional"></dx:GridViewDataTextColumn>
												<dx:GridViewDataTextColumn VisibleIndex="8" Visible="true" FieldName="EsperaInternacional" Caption="Espera Internacional"></dx:GridViewDataTextColumn>
												<dx:GridViewDataTextColumn VisibleIndex="9" Visible="true" FieldName="PernoctaNacional" Caption="Pernocta Nacional"></dx:GridViewDataTextColumn>
												<dx:GridViewDataTextColumn VisibleIndex="10" Visible="true" FieldName="PernoctaInternacional" Caption="Pernocta Internacional"></dx:GridViewDataTextColumn>
												<dx:GridViewDataTextColumn VisibleIndex="11" Visible="true" FieldName="HorasVoladas" Caption="Horas Voladas"></dx:GridViewDataTextColumn>
												<%--<dx:GridViewCommandColumn ShowEditButton="true" ButtonType="Button" Caption="Acciones"
													ShowInCustomizationForm="True" VisibleIndex ="12">
													<CustomButtons>
														<dx:GridViewCommandColumnCustomButton ID="btnVer" Text="Ver">
															<Image Height="20px" ToolTip="Ver" Width="20px"></Image>
														</dx:GridViewCommandColumnCustomButton>
													</CustomButtons>
												</dx:GridViewCommandColumn>
											</Columns>
											<Settings ShowGroupPanel="true" />
											<SettingsSearchPanel Visible="true" />
											<SettingsPager Position="TopAndBottom">
												<PageSizeItemSettings Items="1, 10, 20, 50, 100" Visible="true"></PageSizeItemSettings>
											</SettingsPager>
											<%--<SettingsCommandButton>
												<EditButton Text="Editar">
													<Image Height="20px" ToolTip="Editar" Width="20px"></Image>
												</EditButton>
											</SettingsCommandButton>
										</dx:ASPxGridView>
										<dx:ASPxGridViewExporter ID="Exporter" runat="server" GridViewID="gvHorasVoladas"></dx:ASPxGridViewExporter>
										--%>
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
							
						</div>
						<div class="col-md-6" style="text-align: right;">
							<asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Conditional" OnUnload="UpdatePanel_Unload">
								<ContentTemplate>
									<dx:ASPxLabel Theme="Office2010Black" Text="Exportar a:" runat="server"></dx:ASPxLabel>
									&nbsp;&nbsp;
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
</asp:Content>