<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Hist_Pagos.ascx.vb" Inherits="Hist_Pagos" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Label ID="LblCat_Lo_Usuario" runat="server" Visible="false"></asp:Label>
<asp:HiddenField ID="HidenCredito" runat="server" />
<table class="Table">
    <tr class="Titulos">
        <td>Histórico de Pagos</td>
    </tr>
    <tr align="center">
        <td>
            <asp:Panel ID="PnlCapturaPagos" runat="server">
                <table class="Izquierda">
                    <tr>
                        <td>
                            <asp:Label ID="LblHist_Pa_Dtepago" runat="server" CssClass="LblDesc" Text="Fecha De Pago"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TxtHist_Pa_Dtepago" runat="server" MaxLength="10" CssClass="TxtDesc" Width="128px"></asp:TextBox>
                            <asp:MaskedEditExtender ID="DateBox_MaskedEditExtender" runat="server" Mask="99/99/9999" MaskType="Date" OnFocusCssClass="MaskedEditFocus" TargetControlID="TxtHist_Pa_Dtepago">
                            </asp:MaskedEditExtender>
                            <asp:CalendarExtender ID="TxtHist_Pa_Dtepago_CalendarExtender" runat="server" Enabled="True" TargetControlID="TxtHist_Pa_Dtepago" CssClass="Calendario">
                            </asp:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LblHist_Pa_Montopago" runat="server" CssClass="LblDesc" Text="Monto De pago"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TxtHist_Pa_Montopago" runat="server" MaxLength="15" CssClass="LblDesc"></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="TxtHist_Pa_Montopago_FilteredTextBoxExtender" runat="server" Enabled="True" FilterType="Custom, Numbers" TargetControlID="TxtHist_Pa_Montopago" ValidChars=".">
                            </asp:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LblHist_Pa_Lugarpago" runat="server" CssClass="LblDesc" Text="Lugar De Pago"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="DdlHist_Pa_Lugarpago" runat="server" CssClass="DdlDesc">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LblHist_Pa_Referencia" runat="server" CssClass="LblDesc" Text="Referencia"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TxtHist_Pa_Referencia" runat="server" MaxLength="25" CssClass="TxtDesc" Width="237px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LblHist_Pa_Confirmacion" runat="server" CssClass="LblDesc" Text="Tipo De Confirmación"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="DdlHist_Pa_Confirmacion" runat="server" CssClass="DdlDesc">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>
                            <asp:Button ID="BtnGuardar" runat="server" CssClass="Botones" Text="Guardar" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">&nbsp;</td>
                    </tr>
                </table>
            </asp:Panel>
        </td>
    </tr>
    <tr class="Titulos">
        <td>Pagos Pendientes</td>
    </tr>
    <tr>
        <td align="center">
            <div class="ScroolAddPagos">
                <div class="force-overflow">
                    <asp:GridView ID="GVHist_PagP" runat="server" AlternatingRowStyle-CssClass="alt" CssClass="mGrid" Font-Names="Tahoma" Font-Size="small" PagerStyle-CssClass="pgr" Style="font-size: small" AutoGenerateColumns="true">
                    </asp:GridView>
                </div>
            </div>
        </td>
    </tr>
    <tr class="Titulos">
        <td>Pagos Validados</td>
    </tr>
    <tr>
        <td align="center">
            <div class="ScroolAddPagos">
                <div class="force-overflow">
                    <asp:GridView ID="GVHist_PagV" runat="server" AlternatingRowStyle-CssClass="alt" CssClass="mGrid" Font-Names="Tahoma" Font-Size="small" PagerStyle-CssClass="pgr" Style="font-size: small" AutoGenerateColumns="true">
                    </asp:GridView>
                </div>
            </div>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="PagoT" runat="server" Text="Pago Total Validado: " CssClass="LblMsj"></asp:Label>
            &nbsp;<asp:TextBox ID="TextPag" runat="server" CssClass="TxtDesc" ReadOnly="True"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>&nbsp;</td>
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
