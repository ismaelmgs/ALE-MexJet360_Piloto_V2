<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucPizarraElectronica.ascx.cs" Inherits="ALE_MexJet.ControlesUsuario.ucPizarraElectronica" %>
<%@ Register Assembly="DevExpress.Web.Bootstrap.v18.1, Version=18.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<dx:ASPxPopupControl ID="pcPizarra" runat="server" ClientInstanceName="pcPizarra" Width="1200px" Height="350px"
    MaxWidth="1000px" MaxHeight="1000px" MinHeight="150px" MinWidth="150px" AllowDragging="true" AllowResize="false"
    ShowFooter="false" HeaderText="" CloseOnEscape="true" CloseAction="CloseButton"
    EnableViewState="false" PopupHorizontalAlign="LeftSides" PopupVerticalAlign="Below" EnableHierarchyRecreation="True">
    <ContentCollection>
        <dx:PopupControlContentControl runat="server">
            <asp:Panel ID="Panel3" runat="server">
                <div class="row">
                    <div class="col-md-12">
                        <fieldset class="Personal1">
                            <legend>
                                <span style="font-family: Helvetica, Arial,sans-serif; text-align: center;"></span>
                            </legend>
                            <div class="row">
                                <div class="col-sm-12">
                                    <table width="100%">
                                        <tr>
                                            <td style="text-align: left;">
                                                <dx:ASPxLabel runat="server" ID="lblPizClaveCliente" Text="Clave del Cliente:"></dx:ASPxLabel>
                                            </td>
                                            <td style="text-align: left;">
                                                <dx:ASPxLabel runat="server" ID="lblSnapClaveCliente"></dx:ASPxLabel>
                                            </td>
                                            <td style="text-align: left;">
                                                <dx:ASPxLabel runat="server" ID="lblPizClaveContrato" Text="Clave del Contrato:"></dx:ASPxLabel>
                                            </td>
                                            <td style="text-align: left;">
                                                <dx:ASPxLabel runat="server" ID="lblSnapClaveContrato"></dx:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left;">
                                                <dx:ASPxLabel runat="server" ID="lblPizTipoContrato" Text="Tipo de Contrato:"></dx:ASPxLabel>
                                            </td>
                                            <td style="text-align: left;">
                                                <dx:ASPxLabel runat="server" ID="lblSnapTipoContrato"></dx:ASPxLabel>
                                            </td>
                                            <td style="text-align: left;">
                                                <dx:ASPxLabel runat="server" ID="lblPizGrupoModelo" Text="Grupo Modelo:"></dx:ASPxLabel>
                                            </td>
                                            <td style="text-align: left;">
                                                <dx:ASPxLabel runat="server" ID="lblSnapGrupoModelo"></dx:ASPxLabel>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                </div>
                <div class="row">
                    <dx:ASPxPageControl ID="ASPxPageControl2" Theme="Office2010Black" runat="server" Width="100%" Height="100%"
                        TabAlign="Justify" ActiveTabIndex="0" EnableTabScrolling="true">
                        <TabStyle Paddings-PaddingLeft="50px" Paddings-PaddingRight="50px" />
                        <ContentStyle>
                            <Paddings PaddingLeft="40px" />
                        </ContentStyle>
                        <TabPages>
                            <dx:TabPage Text="Datos Generales" Enabled="true">
                                <ContentCollection>
                                    <dx:ContentControl>
                                        <dx:ASPxPanel Theme="Office2010Black" runat="server" Width="100%" BackColor="White">
                                            <PanelCollection>
                                                <dx:PanelContent runat="server">
                                                    <div class="row">
                                                        <div class="col-md-12 table">
                                                            <table class="table-bordered table-hover" style="width: 100%; border: 0px solid #efefef;">
                                                                <tr>
                                                                    <td style="text-align: left; background-color: #efefef; padding: 2px;">
                                                                        <span style="font-size: 18px;">&#x1f5b9;</span>
                                                                        <dx:ASPxLabel runat="server" ID="lblPizFolioRemision" Text="Folio Remision:"></dx:ASPxLabel>
                                                                    </td>
                                                                    <td style="text-align: center; padding: 2px;">
                                                                        <dx:ASPxLabel runat="server" ID="lblSnapFolioRem" Text="000" Style="font-weight: bold;"></dx:ASPxLabel>
                                                                    </td>
                                                                    <td colspan="2"></td>
                                                                    <td style="text-align: left; background-color: #efefef; padding: 2px;">
                                                                        <span style="font-size: 16px; padding: 2px;">&#x1f551;&#xfe0e;</span>
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel48" Text="Tiempo a Cobrar:"></dx:ASPxLabel>
                                                                    </td>
                                                                    <td style="text-align: center; padding: 2px;">
                                                                        <dx:ASPxLabel runat="server" ID="lblSnapTiempoCobrar" Text="Vuelo" Style="font-weight: bold;"></dx:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="text-align: left; background-color: #efefef; padding: 2px;">
                                                                        <span style="font-size: 18px;">&#x1f551;&#xfe0e;</span>
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel50" Text="Más Minutos:"></dx:ASPxLabel>
                                                                    </td>
                                                                    <td style="text-align: center; padding: 2px;">
                                                                        <dx:ASPxLabel runat="server" ID="lblSnapMasMinutos" Text="10" Style="font-weight: bold;"></dx:ASPxLabel>
                                                                    </td>
                                                                    <td colspan="2"></td>
                                                                    <td style="text-align: left; background-color: #efefef; padding: 2px;">
                                                                        <span style="font-size: 20px;">&#x2bd1;</span>
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel52" Text="¿Aplica tramos pactados?:"></dx:ASPxLabel>
                                                                    </td>
                                                                    <td style="text-align: center; padding: 2px;">
                                                                        <dx:ASPxLabel runat="server" ID="lblSnapAplicaTramPact" Text="No" Style="font-weight: bold;"></dx:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="text-align: left; background-color: #efefef; padding: 2px;">
                                                                        <span style="font-size: 18px;">&#x1f551;&#xfe0e;</span>
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel56" Text="Horas Contratadas Total:"></dx:ASPxLabel>
                                                                    </td>
                                                                    <td style="text-align: center; padding: 2px;">
                                                                        <dx:ASPxLabel runat="server" ID="lblSnapHorasContratadasTotal" Text="1500" Style="font-weight: bold;"></dx:ASPxLabel>
                                                                    </td>
                                                                    <td colspan="2"></td>

                                                                    <td style="text-align: left; background-color: #efefef; padding: 2px;">
                                                                        <span style="font-size: 18px;">&#x1f551;&#xfe0e;</span>
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel58" Text="Horas contratadas por año:"></dx:ASPxLabel>
                                                                    </td>
                                                                    <td style="text-align: center; padding: 2px;">
                                                                        <dx:ASPxLabel runat="server" ID="lblSnapHorasContratadasAnio" Text="300" Style="font-weight: bold;"></dx:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                                <tr>

                                                                    <td style="text-align: left; background-color: #efefef; padding: 2px;">
                                                                        <span style="font-size: 20px;">&#x2bd1;</span>
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel60" Text="¿Se cobra combustible?:"></dx:ASPxLabel>
                                                                    </td>
                                                                    <td style="text-align: center; padding: 2px;">
                                                                        <dx:ASPxLabel runat="server" ID="lblSnapSeCobraComb" Text="Si" Style="font-weight: bold;"></dx:ASPxLabel>
                                                                    </td>
                                                                    <td colspan="2"></td>

                                                                    <td style="text-align: left; background-color: #efefef; padding: 2px;">
                                                                        <span style="font-size: 18px;">&#x1f4b3;&#xfe0e;</span>
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel62" Text="Forma de Cobro Combustible:"></dx:ASPxLabel>
                                                                    </td>
                                                                    <td style="text-align: center; padding: 2px;">
                                                                        <dx:ASPxLabel runat="server" ID="lblFormaCobroCombustible" Text="Promedio" Style="font-weight: bold;"></dx:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                                <tr>

                                                                    <td style="text-align: left; background-color: #efefef; padding: 4px;">
                                                                        <span style="font-size: 18px;">&#x2b86;</span>
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel64" Text="Factor de Tramos Nacionales:"></dx:ASPxLabel>
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <dx:ASPxLabel runat="server" ID="lblSnapFactorTramosNales" Text="0" Style="font-weight: bold;"></dx:ASPxLabel>
                                                                    </td>
                                                                    <td colspan="2"></td>
                                                                    <td style="text-align: left; background-color: #efefef;">
                                                                        <span style="font-size: 18px;">&#x2b86;</span>
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel66" Text="Factor de tramos interacionales:"></dx:ASPxLabel>
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <dx:ASPxLabel runat="server" ID="lblSnapFactorTramosInter" Text="0" Style="font-weight: bold;"></dx:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="text-align: left; background-color: #efefef;">
                                                                        <span style="font-weight: bold; border: 1px solid #000000; border-radius: 50%; font-size: 10px; padding: 1px; padding-bottom: 2px;">&nbsp;$ </span>&nbsp;<dx:ASPxLabel runat="server" ID="ASPxLabel68" Text="Costo Directo de vuelo Nacional:"></dx:ASPxLabel>
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <dx:ASPxLabel runat="server" ID="lblSnapCostoDirVueloNal" Text="1234.00" Style="font-weight: bold;"></dx:ASPxLabel>
                                                                    </td>
                                                                    <td colspan="2"></td>
                                                                    <td style="text-align: left; background-color: #efefef;">
                                                                        <span style="font-weight: bold; border: 1px solid #000000; border-radius: 50%; font-size: 10px; padding: 1px; padding-bottom: 2px;">&nbsp;$ </span>&nbsp;<dx:ASPxLabel runat="server" ID="ASPxLabel118" Text="Costo Directo de vuelo internacional:"></dx:ASPxLabel>
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <dx:ASPxLabel runat="server" ID="lblSnapCostoDirVueloInt" Text="1234.00" Style="font-weight: bold;"></dx:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </dx:PanelContent>
                                            </PanelCollection>
                                        </dx:ASPxPanel>
                                    </dx:ContentControl>
                                </ContentCollection>
                            </dx:TabPage>
                            <dx:TabPage Text="Tarifas de Vuelo" Enabled="true">
                                <ContentCollection>
                                    <dx:ContentControl>
                                        <dx:ASPxPanel Theme="Office2010Black" runat="server" Width="100%" BackColor="White">
                                            <PanelCollection>
                                                <dx:PanelContent runat="server">
                                                    <div class="row">
                                                        <div class="col-md-12 table">
                                                            <table class="table-bordered table-hover" style="width: 100%; border: 0px solid #efefef;">
                                                                <tr>
                                                                    <td style="text-align: left; background-color: #efefef; padding: 4px;">
                                                                        <span style="font-size: 20px;">&#x2bd1;</span>
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel22" Text="¿Se cobra tiempo de espera?:"></dx:ASPxLabel>
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <dx:ASPxLabel runat="server" ID="lblSnapSeCobraTiempoEspera" Text="si" Style="font-weight: bold;"></dx:ASPxLabel>
                                                                    </td>
                                                                    <td colspan="2"></td>
                                                                    <td></td>
                                                                    <td></td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="text-align: left; background-color: #efefef;">
                                                                        <span style="font-weight: bold; border: 1px solid #000000; border-radius: 50%; font-size: 10px; padding: 1px; padding-bottom: 2px;">&nbsp;$ </span>&nbsp;<dx:ASPxLabel runat="server" ID="ASPxLabel24" Text="Tarifa de espera nacional:"></dx:ASPxLabel>
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <dx:ASPxLabel runat="server" ID="lblSnapTarifaEspNal" Text="385.33" Style="font-weight: bold;"></dx:ASPxLabel>
                                                                    </td>
                                                                    <td colspan="2"></td>
                                                                    <td style="text-align: left; background-color: #efefef;">
                                                                        <span style="font-weight: bold; border: 1px solid #000000; border-radius: 50%; font-size: 10px; padding: 1px; padding-bottom: 2px;">%</span>
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel28" Text="Porcentaje de tarifa de espera nacional:"></dx:ASPxLabel>
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <dx:ASPxLabel runat="server" ID="lblSnapPorcentajeTarEspNal" Text="0" Style="font-weight: bold;"></dx:ASPxLabel>
                                                                    </td>
                                                                    
                                                                </tr>
                                                                <tr>
                                                                    <td style="text-align: left; background-color: #efefef; padding: 4px;">
                                                                        <span style="font-weight: bold; border: 1px solid #000000; border-radius: 50%; font-size: 10px; padding: 1px; padding-bottom: 2px;">&nbsp;$ </span>&nbsp;<dx:ASPxLabel runat="server" ID="ASPxLabel26" Text="Tarifa de espera internacional:"></dx:ASPxLabel>
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <dx:ASPxLabel runat="server" ID="lblSnapTarifaEspInt" Text="385.33" Style="font-weight: bold;"></dx:ASPxLabel>
                                                                    </td>
                                                                    <td colspan="2"></td>
                                                                    <td style="text-align: left; background-color: #efefef; padding: 4px;">
                                                                        <span style="font-weight: bold; border: 1px solid #000000; border-radius: 50%; font-size: 10px; padding: 1px; padding-bottom: 2px;">%</span>
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel30" Text="Porcentaje de tarifa de espera internacional:"></dx:ASPxLabel>
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <dx:ASPxLabel runat="server" ID="lblSnapPorcentajeTarEspInt" Text="0" Style="font-weight: bold;"></dx:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="text-align: left; background-color: #efefef;">
                                                                        <span style="font-size: 20px;">&#x2bd1;</span>
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel36" Text="¿Se cobran pernoctas?:"></dx:ASPxLabel>
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <dx:ASPxLabel runat="server" ID="lblSnapSeCobranPernoctas" Text="Si" Style="font-weight: bold;"></dx:ASPxLabel>
                                                                    </td>
                                                                    <td colspan="2"></td>
                                                                    
                                                                </tr>
                                                                <tr>
                                                                    <td style="text-align: left; background-color: #efefef; padding: 4px;">
                                                                        <span style="font-weight: bold; border: 1px solid #000000; border-radius: 50%; font-size: 10px; padding: 1px; padding-bottom: 2px;">&nbsp;$ </span>&nbsp;<dx:ASPxLabel runat="server" ID="ASPxLabel38" Text="Tarifa en Dlls de pernocta nacional:"></dx:ASPxLabel>
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <dx:ASPxLabel runat="server" ID="lblSnapTarifaDllsPerNal" Text="12345.00" Style="font-weight: bold;"></dx:ASPxLabel>
                                                                    </td>
                                                                    <td colspan="2"></td>
                                                                    <td style="text-align: left; background-color: #efefef;">
                                                                        <span style="font-weight: bold; border: 1px solid #000000; border-radius: 50%; font-size: 10px; padding: 1px; padding-bottom: 2px;">%</span>
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel40" Text="Porcentaje de tarifa en Dlls de pernocta nacional:"></dx:ASPxLabel>
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <dx:ASPxLabel runat="server" ID="lblSnapPorcentajeTarDllsPerNal" Text="0" Style="font-weight: bold;"></dx:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="text-align: left; background-color: #efefef; padding: 4px;">
                                                                        <span style="font-weight: bold; border: 1px solid #000000; border-radius: 50%; font-size: 10px; padding: 1px; padding-bottom: 2px;">&nbsp;$ </span>&nbsp;<dx:ASPxLabel runat="server" ID="ASPxLabel42" Text="Tarifa end Dlls de pernocta internacional:"></dx:ASPxLabel>
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <dx:ASPxLabel runat="server" ID="lblSnapTarifaDllsPerInt" Text="12345.00" Style="font-weight: bold;"></dx:ASPxLabel>
                                                                    </td>
                                                                    <td colspan="2"></td>
                                                                    <td style="text-align: left; background-color: #efefef;">
                                                                        <span style="font-weight: bold; border: 1px solid #000000; border-radius: 50%; font-size: 10px; padding: 1px; padding-bottom: 2px;">%</span>
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel44" Text="Porcentaje de tarifa en Dlls de pernocta internacional:"></dx:ASPxLabel>
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <dx:ASPxLabel runat="server" ID="lblSnapPorcentajeTarDllsPerInt" Text="1234.00" Style="font-weight: bold;"></dx:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </dx:PanelContent>
                                            </PanelCollection>
                                        </dx:ASPxPanel>
                                    </dx:ContentControl>
                                </ContentCollection>
                            </dx:TabPage>
                            <dx:TabPage Text="Cobros y Descuentos" Enabled="true">
                                <ContentCollection>
                                    <dx:ContentControl>
                                        <dx:ASPxPanel Theme="Office2010Black" runat="server" Width="100%" BackColor="White">
                                            <PanelCollection>
                                                <dx:PanelContent runat="server">
                                                    <div class="row">
                                                        <div class="col-md-12 table">
                                                            <table class="table-bordered table-hover" style="width: 100%; border: 0px solid #efefef;">
                                                                <tr>
                                                                    <td style="text-align: left; background-color: #efefef; padding: 2px;">
                                                                        <span style="font-size: 20px;">&#x2bd1;</span>
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel32" Text="¿Aplica vuelo simultaneo?:"></dx:ASPxLabel>
                                                                    </td>
                                                                    <td style="text-align: center; padding: 2px;">
                                                                        <dx:ASPxLabel runat="server" ID="lblSnapAplicaVloSimultaneo" Text="no" Style="font-weight: bold;"></dx:ASPxLabel>
                                                                    </td>
                                                                    <td colspan="2"></td>
                                                                    <td style="text-align: left; background-color: #efefef; padding: 2px;">
                                                                        <span style="font-size: 22px;">&#x2058;</span>
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel34" Text="Cuantos vuelos simultaneos:"></dx:ASPxLabel>
                                                                    </td>
                                                                    <td style="text-align: center; padding: 2px;">
                                                                        <dx:ASPxLabel runat="server" ID="lblSnapCuantosVuelosSim" Text="0" Style="font-weight: bold;"></dx:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="text-align: left; background-color: #efefef; padding: 2px;">
                                                                        <span style="font-size: 20px;">&#x21c9;</span>
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel70" Text="Factor de vuelos simultaneos:"></dx:ASPxLabel>
                                                                    </td>
                                                                    <td style="text-align: center; padding: 2px;">
                                                                        <dx:ASPxLabel runat="server" ID="lblSnapFactorVuelosSim" Text="1" Style="font-weight: bold;"></dx:ASPxLabel>
                                                                    </td>
                                                                    <td colspan="2"></td>
                                                                    <td style="text-align: left; background-color: #efefef; padding: 2px;">
                                                                        <span style="font-size: 20px;">&#x2bd1;</span>
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel72" Text="¿Se descuenta espera nacional?:"></dx:ASPxLabel>
                                                                    </td>
                                                                    <td style="text-align: center; padding: 2px;">
                                                                        <dx:ASPxLabel runat="server" ID="lblSnapSeDescuentaEspNal" Text="no" Style="font-weight: bold;"></dx:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="text-align: left; background-color: #efefef; padding: 2px;">
                                                                        <span style="font-size: 20px;">&#x2bd1;</span>
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel74" Text="¿Se descuenta espera internacional?:"></dx:ASPxLabel>
                                                                    </td>
                                                                    <td style="text-align: center; padding: 2px;">
                                                                        <dx:ASPxLabel runat="server" ID="lblSnapSeDescuentaEspInt" Text="no" Style="font-weight: bold;"></dx:ASPxLabel>
                                                                    </td>

                                                                    <td colspan="2"></td>
                                                                    <td style="text-align: left; background-color: #efefef; padding: 2px;">
                                                                        <span style="font-size: 20px;">&#x1f6c9;</span>
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel86" Text="Factor de espera por hora de vuelo nacional:"></dx:ASPxLabel>
                                                                    </td>
                                                                    <td style="text-align: center; padding: 2px;">
                                                                        <dx:ASPxLabel runat="server" ID="lblSnapFactorEsperaHoraVueloNal" Text="0" Style="font-weight: bold;"></dx:ASPxLabel>
                                                                    </td>

                                                                </tr>
                                                                <tr>
                                                                    <td style="text-align: left; background-color: #efefef; padding: 2px;">
                                                                        <span style="font-size: 20px;">&#x1f6c9;</span>
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel76" Text="Factor de espera por hora de vuelo internacional:"></dx:ASPxLabel>
                                                                    </td>
                                                                    <td style="text-align: center; padding: 2px;">
                                                                        <dx:ASPxLabel runat="server" ID="lblSnapFactorEsperaHoraVueloInt" Text="0" Style="font-weight: bold;"></dx:ASPxLabel>
                                                                    </td>
                                                                    <td colspan="2"></td>
                                                                    <td style="text-align: left; background-color: #efefef; padding: 2px;">
                                                                        <span style="font-size: 20px;">&#x2bd1;</span>
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel78" Text="¿Se descuenta pernocta nacional?:"></dx:ASPxLabel>
                                                                    </td>
                                                                    <td style="text-align: center; padding: 2px;">
                                                                        <dx:ASPxLabel runat="server" ID="lblSnapSeDescuentaPernoctaNal" Text="no" Style="font-weight: bold;"></dx:ASPxLabel>
                                                                    </td>


                                                                </tr>
                                                                <tr>

                                                                    <td style="text-align: left; background-color: #efefef; padding: 2px;">
                                                                        <span style="font-size: 20px;">&#x2bd1;</span>
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel80" Text="¿Se descuenta pernocta internacional?:"></dx:ASPxLabel>
                                                                    </td>
                                                                    <td style="text-align: center; padding: 2px;">
                                                                        <dx:ASPxLabel runat="server" ID="lblSnapSeDescuentaPernoctaInt" Text="no" Style="font-weight: bold;"></dx:ASPxLabel>
                                                                    </td>
                                                                    <td colspan="2"></td>
                                                                    <td style="text-align: left; background-color: #efefef; padding: 2px;">
                                                                        <span style="font-size: 20px;">&#x1d1b8;</span>
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel82" Text="Factor de pernocta en hora de vuelo nacional:"></dx:ASPxLabel>
                                                                    </td>
                                                                    <td style="text-align: center; padding: 2px;">
                                                                        <dx:ASPxLabel runat="server" ID="lblSnapFactorPerHoraVueloNal" Text="0" Style="font-weight: bold;"></dx:ASPxLabel>
                                                                    </td>

                                                                </tr>
                                                                <tr>
                                                                    <td style="text-align: left; background-color: #efefef; padding: 2px;">
                                                                        <span style="font-size: 20px;">&#x1d1b8 </span>
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel84" Text="Factor de pernocta en hora de vuelo internacional:"></dx:ASPxLabel>
                                                                    </td>
                                                                    <td style="text-align: center; padding: 2px;">
                                                                        <dx:ASPxLabel runat="server" ID="lblSnapFactorPerHoraVueloInt" Text="0" Style="font-weight: bold;"></dx:ASPxLabel>
                                                                    </td>
                                                                    <td colspan="2"></td>
                                                                    <td colspan="2" style="background-color: #efefef; padding: 2px;"></td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </dx:PanelContent>
                                            </PanelCollection>
                                        </dx:ASPxPanel>
                                    </dx:ContentControl>
                                </ContentCollection>
                            </dx:TabPage>
                            <dx:TabPage Text="Factores Aplicados a la Remisión" Enabled="true">
                                <ContentCollection>
                                    <dx:ContentControl>
                                        <dx:ASPxPanel Theme="Office2010Black" runat="server" Width="100%" BackColor="White">
                                            <PanelCollection>
                                                <dx:PanelContent runat="server">
                                                    <div class="row">
                                                        <div class="col-md-12 table">
                                                            <dx:BootstrapGridView ID="gvFactoresRem" runat="server" KeyFieldName="iNoTramo">
                                                                <settingsadaptivity adaptivitymode="HideDataCells" allowonlyoneadaptivedetailexpanded="true"></settingsadaptivity>
                                                                <columns>
                                                                        <dx:BootstrapGridViewDataColumn FieldName="sMatricula" Caption="Matricula" VisibleIndex="0" HorizontalAlign="Center" Width="25" />
                                                                        <dx:BootstrapGridViewDataColumn FieldName="sOrigen" Caption="Origen" VisibleIndex="1" CssClasses-HeaderCell="" CssClasses-DataCell="" HorizontalAlign="Center" Settings-AllowCellMerge="False" />
                                                                        <dx:BootstrapGridViewDataColumn FieldName="sDestino" Caption="Destino" VisibleIndex="2" CssClasses-HeaderCell="border-1" CssClasses-DataCell="itemTd" HorizontalAlign="Center" />
                                                                        <dx:BootstrapGridViewDataColumn FieldName="sTiempoOriginal" Caption="Tiempo original" VisibleIndex="3" />
                                                                        <dx:BootstrapGridViewDataColumn FieldName="sTiempoFinal" Caption="Tiempo final" VisibleIndex="4" CssClasses-HeaderCell="" CssClasses-DataCell="" HorizontalAlign="Center" Settings-AllowCellMerge="False" />
                                                                        
                                                                    </columns>
                                                                <templates>
                                                                    <DetailRow>
                                                                        <dx:BootstrapGridView ID="gvFactoresDetalle" runat="server" KeyFieldName="iNoTramo"
                                                                            OnBeforePerformDataSelect="gvFactoresDetalle_BeforePerformDataSelect">
                                                                            <Columns>
                                                                                <dx:BootstrapGridViewDataColumn FieldName="sFactorEspeciaRem" Caption="Factor especial rem" VisibleIndex="1" CssClasses-DataCell="hideColumn" HorizontalAlign="Center" />
                                                                                <dx:BootstrapGridViewDataColumn FieldName="sAplicaFactorTramoNacional" Caption="Factor tramo nal." VisibleIndex="3" CssClasses-HeaderCell="" CssClasses-DataCell="" HorizontalAlign="Center" Settings-AllowCellMerge="True" />
                                                                                <dx:BootstrapGridViewDataColumn FieldName="sAplicaFactorTramoInternacional" Caption="Factor tramo int." VisibleIndex="2" SortIndex="0" CssClasses-HeaderCell="" CssClasses-DataCell="" HorizontalAlign="Center" Settings-AllowCellMerge="True" />
                                                                                <dx:BootstrapGridViewDataColumn FieldName="sAplicoIntercambio" Caption="Factor intercambio" VisibleIndex="4" CssClasses-HeaderCell="border-1" CssClasses-DataCell="itemTd" HorizontalAlign="Center" />
                                                                                <dx:BootstrapGridViewDataColumn FieldName="sAplicaGiraEspera" Caption="Factor gira espera" VisibleIndex="5" CssClasses-HeaderCell="border-1" CssClasses-DataCell="itemTd" HorizontalAlign="Center" />
                                                                                <dx:BootstrapGridViewDataColumn FieldName="sAplicaGiraHorario" Caption="Factor gira horario" VisibleIndex="6" CssClasses-HeaderCell="border-1" CssClasses-DataCell="itemTd" HorizontalAlign="Center" />
                                                                                <dx:BootstrapGridViewDataColumn FieldName="sAplicaFactorFechaPico" Caption="Factor fecha pico" VisibleIndex="7" CssClasses-HeaderCell="border-1" CssClasses-DataCell="itemTd" HorizontalAlign="Center" />
                                                                                <dx:BootstrapGridViewDataColumn FieldName="sAplicaFactorVueloSimultaneo" Caption="Factor vlo simultaneo" VisibleIndex="8" CssClasses-HeaderCell="border-1" CssClasses-DataCell="itemTd" HorizontalAlign="Center" />
                                                                            </Columns>
                                                                        </dx:BootstrapGridView>
                                                                    </DetailRow>
                                                                </templates>
                                                                <settingsdetail showdetailrow="true" />
                                                                <clientsideevents init="onSelectionGridViewAction" selectionchanged="onSelectionGridViewAction" endcallback="onSelectionGridViewAction" />
                                                                <%--<settingspager numericbuttoncount="4">
                                                                    <PageSizeItemSettings Visible="true" Items="10, 20, 50" />
                                                                </settingspager>--%>
                                                            </dx:BootstrapGridView>

                                                        </div>
                                                    </div>
                                                </dx:PanelContent>
                                            </PanelCollection>
                                        </dx:ASPxPanel>
                                    </dx:ContentControl>
                                </ContentCollection>
                            </dx:TabPage>
                            <dx:TabPage Text="Cobros Espera y Pernoctas" Enabled="true">
                                <ContentCollection>
                                    <dx:ContentControl>
                                        <dx:ASPxPanel Theme="Office2010Black" runat="server" Width="100%" BackColor="White">
                                            <PanelCollection>
                                                <dx:PanelContent runat="server">
                                                    <div class="row">
                                                        <div class="col-md-12 table">
                                                            <table class="table-bordered table-hover" style="width: 100%; border: 0px solid #efefef;">
                                                                <tr>
                                                                    <td style="text-align: left; background-color: #efefef;">
                                                                        <span style="font-size: 20px;">&#x1f6c9;</span>
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel39" Text="¿Se cobra tiempo de espera?:"></dx:ASPxLabel>
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <dx:ASPxLabel runat="server" ID="lblSnapSeCobraTiempoEspera2" Text="0" Style="font-weight: bold;"></dx:ASPxLabel>
                                                                    </td>
                                                                    <td colspan="2"></td>
                                                                    <td style="text-align: left; background-color: #efefef;">
                                                                        <span style="font-size: 20px;">&#x2bd1;</span>
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel43" Text="Horas de pernocta:"></dx:ASPxLabel>
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <dx:ASPxLabel runat="server" ID="lblSnapHorasPernocta" Text="no" Style="font-weight: bold;"></dx:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="text-align: left; background-color: #efefef;">
                                                                        <span style="font-size: 20px;">&#x2bd1;</span>
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel8" Text="Tiempo de espera Total:"></dx:ASPxLabel>
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <dx:ASPxLabel runat="server" ID="lblSnapTiempoEsperaGeneral" Text="no" Style="font-weight: bold;"></dx:ASPxLabel>
                                                                    </td>
                                                                    <td colspan="2"></td>
                                                                    <td style="text-align: left; background-color: #efefef;">
                                                                        <span style="font-size: 22px;">&#x2058;</span>
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel18" Text="Tiempo de vuelo total:"></dx:ASPxLabel>
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <dx:ASPxLabel runat="server" ID="lblSnapTiempoVueloGeneral" Text="0" Style="font-weight: bold;"></dx:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="text-align: left; background-color: #efefef;">
                                                                        <span style="font-size: 20px;">&#x21c9;</span>
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel23" Text="Factor de Hr de vuelo (Tiempo de espera):"></dx:ASPxLabel>
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <dx:ASPxLabel runat="server" ID="lblSnapFactorHrVuelo" Text="1" Style="font-weight: bold;"></dx:ASPxLabel>
                                                                    </td>
                                                                    <td colspan="2"></td>
                                                                    <td style="text-align: left; background-color: #efefef;">
                                                                        <span style="font-size: 20px;">&#x2bd1;</span>
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel27" Text="Total tiempo de espera cobrar:"></dx:ASPxLabel>
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <dx:ASPxLabel runat="server" ID="lblSnapTotalTiempoEsperaCobrar" Text="no" Style="font-weight: bold;"></dx:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="text-align: left; background-color: #efefef;">
                                                                       
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                       
                                                                    </td>
                                                                    <td colspan="2"></td>
                                                                    <td style="text-align: left; background-color: #efefef;">
                                                                        <span style="font-size: 20px;">&#x2bd1;</span>
                                                                        <dx:ASPxLabel runat="server" ID="ASPxLabel29" Text="Formula tiempo de espera:"></dx:ASPxLabel>
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        <dx:ASPxLabel runat="server" ID="lblSnapCalculoTiempoEsperaCobrar" Text="TE cobrar = TE total - (total tiempo de vuelo total * factor tiempo de vuelo)" Style="font-weight: bold;"></dx:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </dx:PanelContent>
                                            </PanelCollection>
                                        </dx:ASPxPanel>
                                    </dx:ContentControl>
                                </ContentCollection>
                            </dx:TabPage>
                        </TabPages>
                    </dx:ASPxPageControl>
                </div>
            </asp:Panel>
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>
