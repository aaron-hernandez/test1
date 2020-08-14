<link href="Estilos/HTML.css" rel="stylesheet" />
<link href="Estilos/ObjHtmlNoS.css" rel="stylesheet" />
<link href="Estilos/ObjHtmlS.css" rel="stylesheet" />

<%@ Control Language="VB" AutoEventWireup="false" CodeFile="EnvioCorreos.ascx.vb" Inherits="EnvioCorreos" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="HTMLEditor" %>

<asp:UpdatePanel runat="server" ID="UpnlCorreos">
    <ContentTemplate>

        <table class="Table">
            <tr class="Titulos">
                <td colspan="3">Envio de Correos
                </td>
            </tr>
            <tr class="Centro">
                <td>&nbsp;</td>
                <td>
                    <asp:Label ID="LblPlantilla" runat="server" Text="Plantilla" CssClass="LblDesc"></asp:Label>
                    &nbsp;&nbsp;&nbsp;<asp:DropDownList ID="DdlPlantilla" runat="server" CssClass="DdlDesc" AutoPostBack="true" ></asp:DropDownList>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr class="Centro">
                <td></td>
                <%--<td class="Arriba">
                    <asp:TextBox ID="TxtPlantilla" runat="server" Height="112px" Width="601px"  CssClass="LblDescNS" MaxLength="1000" TextMode="MultiLine" Enabled="False"></asp:TextBox>
                </td>--%>
                <td align="center">
                <asp:UpdatePanel ID="updatePanel1Y" EnableViewState ="false"  runat="server">
                    <ContentTemplate>
                        <HTMLEditor:Editor   runat="server" ID="TxtPlantilla" Height="300px"  AutoFocus="true" Width="100%" EditModes="Preview"/>
                        <%----%>
                        <br />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
                <td></td>
            </tr>
            <tr class="Centro">
                <td>&nbsp;</td>
                <td align="center">
                     <asp:CheckBox ID="CbxAllCorreos" runat="server" AutoPostBack="True" text ="Seleccionar Todos" CssClass ="CbxDesc "/>
                                    <asp:GridView ID="GVCorreosCredito" runat="server" AlternatingRowStyle-CssClass="alt" CssClass="mGrid" Font-Names="Tahoma" Font-Size="Smaller" PagerStyle-CssClass="pgr" Style="font-size: small">
                    <AlternatingRowStyle CssClass="alt" />
                    <Columns>
                        <asp:TemplateField HeaderText="Agregar" SortExpression="Modificar">
                            <ItemTemplate>
                                <asp:CheckBox ID="CbxAgregar" runat="server"  />
                            </ItemTemplate>
                        </asp:TemplateField>   
                    </Columns>
                </asp:GridView>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr class="Centro">
                <td>&nbsp;</td>
                <td>
                    <asp:UpdatePanel ID="UpGenerar" runat="server">
                        <ContentTemplate>
                            <asp:Button ID="BtnEnviar" runat="server" Text="Enviar" CssClass="Botones" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr class="Arriba" align="center" >
                <td colspan="2">
                    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpGenerar">
                        <ProgressTemplate>
                            <asp:Image ID="Image4" runat="server" ImageUrl="~/Imagenes/sendingMail.gif" Width="200px"/>
                            <%--<asp:Image ID="Image1" runat="server" ImageUrl="~/Imagenes/737.gif" Width="50px" />--%>
                            <%--<div class="cssload-clock"></div>--%>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                </td>
            </tr>
        </table>

        <asp:Button ID="BtnModalMsj" runat="server" Style="visibility: hidden" />
        <asp:Panel ID="PnlModalMsj" runat="server" CssClass="ModalMsj" Style="display: none">
            <div class="heading">
                Aviso
            </div>
            <div class="CuerpoMsj">
                <table class="Table">
                    <tr align="center">
                        <td>&nbsp;</td>
                    </tr>
                    <tr align="center">
                        <td>
                            <asp:Label ID="LblMsj" runat="server" CssClass="LblDesc"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Button ID="BtnCancelarMsj" runat="server" CssClass="button green close" Text="Aceptar" />
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
        <asp:ModalPopupExtender ID="MpuMensajes" runat="server" PopupControlID="PnlModalMsj" TargetControlID="BtnModalMsj"
            CancelControlID="BtnCancelarMsj" BackgroundCssClass="modalBackground">
        </asp:ModalPopupExtender>
    </ContentTemplate>
</asp:UpdatePanel>
