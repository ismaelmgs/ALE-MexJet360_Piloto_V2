<%@ Page Title="" Language="C#" MasterPageFile="~/ALE_Main.Master" AutoEventWireup="true" CodeBehind="frmConsultaPresupuestos.aspx.cs" Inherits="ALE_MexJet.Views.Consultas.frmConsultaPresupuestos" EnableEventValidation="false" UICulture="es" Culture="es-MX" %>

<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Src="~/ControlesUsuario/ucModalMensaje.ascx" TagPrefix="uc1" TagName="ucModalMensaje" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content runat="server" ContentPlaceHolderID="head" ID="Content1"></asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content2">
	<dx:aspxpanel id="ASPxPanel1" runat="server" width="100%" backcolor="White" style="border-radius: 21px;">
        <PanelCollection>
            <dx:PanelContent>
                <div class="row header1">
                    <div class="col-md-12">
                        <span class="FTitulo">&nbsp;&nbsp;Cotizaciones de vuelo</span>
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
													<dx:ASPxLabel ID="lblClientes" Text="Cliente:" AssociatedControlID="ddlClientes" Theme="Office2010Black" runat="server">
													</dx:ASPxLabel>
												</td>
												<td>
													<dx:ASPxComboBox ToolTip="Cliente" ID="ddlClientes" ClientInstanceName="ddlClientes" runat="server" Theme="Office2010Black" EnableTheming="True" 
														NullText="Seleccione un Cliente" AutoPostBack="true" OnSelectedIndexChanged="ddlClientes_SelectedIndexChanged">
													</dx:ASPxComboBox>
												</td>
											</tr>
											<tr>
												<td>
													<dx:ASPxLabel ID="lblIdPresupuesto" Text="Num. Presupuesto:" AssociatedControlID="txtIdPresupuesto" Theme="Office2010Black" runat="server">
													</dx:ASPxLabel>
												</td>
												<td>
													<dx:ASPxTextBox ID="txtIdPresupuesto" ClientInstanceName="txtIdPresupuesto" Theme="Office2010Black" EnableTheming="True" runat="server" 
														NullText="Número de presupuesto"></dx:ASPxTextBox>
												</td>
											</tr>
										</table>
									</div>
									<div class="col-md-4">
										<table>
											<tr>
												<td>
													<dx:ASPxLabel ID="lblFechaHasta" Text="Contrato:" AssociatedControlID="ddlContrato" Theme="Office2010Black" runat="server">
													</dx:ASPxLabel>
												</td>
												<td>
													<asp:UpdatePanel runat="server" ID="UpdatePanel3" UpdateMode="Conditional" OnUnload="UpdatePanel_Unload">
														<ContentTemplate>
															<dx:ASPxComboBox ToolTip="Contrato" ID="ddlContrato" ClientInstanceName="ddlContrato" NullText="Seleccione un Contrato" runat="server" Theme="Office2010Black" EnableTheming="True">
															</dx:ASPxComboBox>
														</ContentTemplate>
														<Triggers>
															<asp:AsyncPostBackTrigger ControlID="ddlClientes" />
														</Triggers>
													</asp:UpdatePanel>
												</td>
											</tr>
										</table>
									</div>
									<div class="col-md-1">
										<dx:ASPxButton ID="btnBuscar" Text="Buscar" OnClick="btnBuscar_Click" Theme="Office2010Black" runat="server">
										</dx:ASPxButton>
									</div>
									<div class="col-md-1"></div>
								</div>
							</fieldset>
						</div>
					</div>
					<br />
					<div class="row">
	                    <div class="col-md-6">
	                        <dx:ASPxButton ID="btnNuevo" Text="Nuevo" Theme="Office2010Black" OnClick="btnNuevo_Click" runat="server" AutoPostBack="true">
	                        </dx:ASPxButton>
	                    </div>
	                    <div class="col-md-6" style="text-align: right;">
	                    	<asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional" OnUnload="UpdatePanel_Unload">
								<ContentTemplate>
			                        <dx:ASPxLabel Theme="Office2010Black" Text="Exportar a:" runat="server"></dx:ASPxLabel>&nbsp;&nbsp;
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
	                        <asp:UpdatePanel ID="upGv" OnUnload="UpdatePanel_Unload" UpdateMode="Conditional" runat="server">
	                            <ContentTemplate>
	                                <asp:Panel ID="pnlPresupuestos" runat="server" Width="100%">
										<dx:ASPxGridView ID="gvPresupuestos" ClientInstanceName="gvPresupuestos"  AutoGenerateColumns="False" runat="server"
											Font-Size="Small" EnableTheming="True" Theme="Office2010Black" Width="100%" KeyFieldName="IdPresupuesto"
											SettingsText-SearchPanelEditorNullText="Ingrese la información a buscar" EnableCallBacks="true" 
											OnCustomButtonCallback="gvPresupuestos_CustomButtonCallback" OnRowDeleting="gvPresupuestos_RowDeleting" 
											OnStartRowEditing="gvPresupuestos_StartRowEditing">
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
												<dx:GridViewDataTextColumn VisibleIndex="0" Visible="true" FieldName="IdPresupuesto" Caption="No." Width="80px"></dx:GridViewDataTextColumn>
												<dx:GridViewDataTextColumn VisibleIndex="1" Visible="false" FieldName="IdCliente" Caption="IdCliente" Width="80px" ></dx:GridViewDataTextColumn>
												<dx:GridViewDataTextColumn VisibleIndex="2" Visible="true" FieldName="CodigoCliente" Caption="Cliente" Width="80px" ></dx:GridViewDataTextColumn>
												<dx:GridViewDataTextColumn VisibleIndex="3" Visible="false" FieldName="IdContrato" Caption="IdContrato" ></dx:GridViewDataTextColumn>
												<dx:GridViewDataTextColumn VisibleIndex="4" Visible="true" FieldName="ClaveContrato" Caption="Contrato" Width="80px" ></dx:GridViewDataTextColumn>
												<dx:GridViewDataTextColumn VisibleIndex="5" Visible="true" FieldName="CompaniaImpresion" Caption="Compañía" Width="200px" ></dx:GridViewDataTextColumn>
												<dx:GridViewDataTextColumn VisibleIndex="6" Visible="true" FieldName="NombreSolicitante" Caption="Nombre de solicitante" Width="200px" ></dx:GridViewDataTextColumn>
												<dx:GridViewDataTextColumn VisibleIndex="7" Visible="true" FieldName="FechaPresupuesto" Caption="Fecha Presupuesto" Width="110px" ></dx:GridViewDataTextColumn>
												<dx:GridViewDataTextColumn VisibleIndex="8" Visible="true" FieldName="FechaSalida" Caption="Fecha salida" Width="100px" ></dx:GridViewDataTextColumn>
												<dx:GridViewDataTextColumn VisibleIndex="9" Visible="true" FieldName="RutaVuelo" Caption="Ruta del vuelo" Width="200px" ></dx:GridViewDataTextColumn>
												<dx:GridViewDataTextColumn VisibleIndex="10" Visible="true" FieldName="NumSolicitud" Caption="Solicitud" Width="80px" ></dx:GridViewDataTextColumn>
												<dx:GridViewDataTextColumn VisibleIndex="11" Visible="true" FieldName="UsuarioModificacion" Caption="Ult. Mod." Width="95px" ></dx:GridViewDataTextColumn>
												<%--<dx:GridViewDataTextColumn VisibleIndex="12" Visible="true" FieldName="Status" Caption="Estatus" Width="100px" ></dx:GridViewDataTextColumn>--%>
												<dx:GridViewCommandColumn VisibleIndex="13" ShowEditButton="true" ButtonType="Button" Caption="Acciones" Width="110px" 
													ShowInCustomizationForm="True">
													<CustomButtons>
														<%--<dx:GridViewCommandColumnCustomButton ID="btnConsultar" Text="Consultar">
															<Image Height="20px" ToolTip="Consultar" Width="20px"></Image>
														</dx:GridViewCommandColumnCustomButton>
														<dx:GridViewCommandColumnCustomButton ID="btnReprte" Text="Reporte">
															<Image Height="20px" ToolTip="Reporte" Width="20px"></Image>
														</dx:GridViewCommandColumnCustomButton>--%>
													</CustomButtons>
												</dx:GridViewCommandColumn>
											</Columns>
											<Settings ShowGroupPanel="true" HorizontalScrollBarMode="Auto"/>
											<SettingsSearchPanel Visible="true" />
											<SettingsPager Position="TopAndBottom" PageSize="30">
												<PageSizeItemSettings Items="1, 10, 20, 30, 50, 100" Visible="true"></PageSizeItemSettings>
											</SettingsPager>
											<SettingsCommandButton>
												<EditButton Text="Editar">
													<Image Height="20px" ToolTip="Editar" Width="20px"></Image>
												</EditButton>
												<%--<DeleteButton Text="Eliminar">
													<Image Height="20px" ToolTip="Editar" Width="20px"></Image>
												</DeleteButton>--%>
											</SettingsCommandButton>
										</dx:ASPxGridView>
										<dx:ASPxGridViewExporter ID="Exporter" runat="server" GridViewID="gvPresupuestos"></dx:ASPxGridViewExporter>
									</asp:Panel>
								</ContentTemplate>
								<Triggers>
	                            	<asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click"  />
	                            </Triggers>
							</asp:UpdatePanel>
						</div>
					</div>
					<br />
					<div class="row">
	                    <div class="col-md-6">
	                        <dx:ASPxButton ID="btnNuevo2" Text="Nuevo" Theme="Office2010Black" OnClick="btnNuevo_Click" runat="server" AutoPostBack="true">
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
	</dx:aspxpanel>
</asp:Content>