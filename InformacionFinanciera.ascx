<%@ Control Language="VB" AutoEventWireup="false" CodeFile="InformacionFinanciera.ascx.vb" Inherits="InformacionFinanciera" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<link href="Estilos/HTML.css" rel="stylesheet" />
<link href="Estilos/ObjHtmlNoS.css" rel="stylesheet" />
<link href="Estilos/ObjHtmlS.css" rel="stylesheet" />

<style type="text/css">
    .auto-style1 {
        width: 135px;
    }
    .auto-style2 {
        width: 150px;
    }
    .auto-style3 {
        width: 145px;
    }
    .auto-style4 {
        width: 175px;
    }
</style>

<asp:Label ID="LblCat_Lo_Usuario" runat="server" Visible="false"></asp:Label>
<table class="Table">
    <tr class="Titulos">
        <td>Información Avales
        </td>
    </tr>
    <tr>
        <td>
            <table>
                <tr>
                    <%--<td>
                        <asp:Label ID="LblPR_AV_TIPO_AVAL" runat="server" Text="Tipo Aval 1" CssClass="LblDesc"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="TxtPR_AV_TIPO_AVAL" runat="server" MaxLength="8" ReadOnly="True" CssClass="TxtDesNS" Width="100px"></asp:TextBox></td>--%>
                    <td>
                        <asp:Label ID="LblPR_AV_NOMBRE_AV1" runat="server" Text="Nombre Aval 1" CssClass="LblDesc"></asp:Label></td>
                    <td class="auto-style1">
                        <asp:TextBox ID="TxtPR_AV_NOMBRE_AV1" runat="server" MaxLength="80" ReadOnly="True" CssClass="TxtDesNS" Width="150px" BackColor="#D6D3CE"></asp:TextBox></td>
                    <td>
                        <asp:Label ID="LblPR_AV_NOMBRE_AV2" runat="server" Text="Nombre Aval 2" CssClass="LblDesc"></asp:Label></td>
                    <td class="auto-style2">
                        <asp:TextBox ID="TxtPR_AV_NOMBRE_AV2" runat="server" MaxLength="80" ReadOnly="True" CssClass="TxtDesNS" Width="150px" BackColor="#D6D3CE"></asp:TextBox></td>
                    <td>
                        <asp:Label ID="LblPR_AV_NOMBRE_AV3" runat="server" Text="Nombre Aval 3" CssClass="LblDesc"></asp:Label></td>
                    <td class="auto-style3">
                        <asp:TextBox ID="TxtPR_AV_NOMBRE_AV3" runat="server" MaxLength="80" ReadOnly="True" CssClass="TxtDesNS" Width="150px" BackColor="#D6D3CE"></asp:TextBox></td>
                    <td>
                        <asp:Label ID="LblPR_AV_NOMBRE_FAM_CER" runat="server" Text="Nombre Fam Cercano" CssClass="LblDesc"></asp:Label></td>
                    <td class="auto-style4">
                        <asp:TextBox ID="TxtPR_AV_NOMBRE_FAM_CER" runat="server" MaxLength="80" ReadOnly="True" CssClass="TxtDesNS" Width="150px" BackColor="#D6D3CE"></asp:TextBox></td>
                </tr>
                <tr>
                    <%--<td>
                        <asp:Label ID="LblPR_AV_TIPO_AV2" runat="server" Text="Tipo Aval 2" CssClass="LblDesc"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="TxtPR_AV_TIPO_AV2" runat="server" MaxLength="8" ReadOnly="True" CssClass="TxtDesNS" Width="100px"></asp:TextBox></td>--%>
                    <td>
                        <asp:Label ID="LblPR_AV_PARENTESCO_AV1" runat="server" Text="Parentesco Aval 1" CssClass="LblDesc"></asp:Label></td>
                    <td class="auto-style1">
                        <asp:TextBox ID="TxtPR_AV_PARENTESCO_AV1" runat="server" MaxLength="35" ReadOnly="True" CssClass="TxtDesNS" Width="150px" BackColor="#D6D3CE"></asp:TextBox></td>
                    <td>
                        <asp:Label ID="LblPR_AV_PARENTESCO_AV2" runat="server" Text="Parentesco Aval 2" CssClass="LblDesc"></asp:Label></td>
                    <td class="auto-style2">
                        <asp:TextBox ID="TxtPR_AV_PARENTESCO_AV2" runat="server" MaxLength="25" ReadOnly="True" CssClass="TxtDesNS" Width="150px" BackColor="#D6D3CE"></asp:TextBox></td>
                    <td>
                        <asp:Label ID="LblPR_AV_PARENTESCO_AV3" runat="server" Text="Parentesco Aval 3" CssClass="LblDesc"></asp:Label></td>
                    <td class="auto-style3">
                        <asp:TextBox ID="TxtPR_AV_PARENTESCO_AV3" runat="server" MaxLength="25" ReadOnly="True" CssClass="TxtDesNS" Width="150px" BackColor="#D6D3CE"></asp:TextBox></td>
                    <td>
                        <asp:Label ID="LblPR_AV_PARENTESCO_FAM_PER" runat="server" Text="Parentesco Fam Cercano" CssClass="LblDesc"></asp:Label></td>
                    <td class="auto-style4">
                        <asp:TextBox ID="TxtPR_AV_PARENTESCO_FAM_PER" runat="server" MaxLength="25" ReadOnly="True" CssClass="TxtDesNS" Width="150px" BackColor="#D6D3CE"></asp:TextBox></td>
                </tr>
                <tr>
                    <%--<td>
                        <asp:Label ID="LblPR_AV_TIPO_AV3" runat="server" Text="Tipo Aval 3" CssClass="LblDesc"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="TxtPR_AV_TIPO_AV3" runat="server" MaxLength="8" ReadOnly="True" CssClass="TxtDesNS" Width="100px"></asp:TextBox></td>--%>
                    <td>
                        <asp:Label ID="LblPR_AV_DIR_AV1" runat="server" Text="Dirección Aval 1" CssClass="LblDesc"></asp:Label></td>
                    <td class="auto-style1">
                        <asp:TextBox ID="TxtPR_AV_DIR_AV1" runat="server" MaxLength="120" ReadOnly="True" CssClass="TxtDesNS" Width="150px" BackColor="#D6D3CE"></asp:TextBox></td>
                    <td>
                        <asp:Label ID="LblPR_AV_DIR_AV2" runat="server" Text="Dirección Aval 2" CssClass="LblDesc"></asp:Label></td>
                    <td class="auto-style2">
                        <asp:TextBox ID="TxtPR_AV_DIR_AV2" runat="server" MaxLength="120" ReadOnly="True" CssClass="TxtDesNS" Width="150px" BackColor="#D6D3CE"></asp:TextBox></td>
                    <td>
                        <asp:Label ID="LblPR_AV_DIR_AV3" runat="server" Text="Dirección Aval 3" CssClass="LblDesc"></asp:Label></td>
                    <td class="auto-style3">
                        <asp:TextBox ID="TxtPR_AV_DIR_AV3" runat="server" MaxLength="120" ReadOnly="True" CssClass="TxtDesNS" Width="150px" BackColor="#D6D3CE"></asp:TextBox></td>
                    <td>
                        <asp:Label ID="LblPR_AV_DIR_FAM_CER" runat="server" Text="Dirección Fam Cercano" CssClass="LblDesc"></asp:Label></td>
                    <td class="auto-style4">
                        <asp:TextBox ID="TxtPR_AV_DIR_FAM_CER" runat="server" MaxLength="120" ReadOnly="True" CssClass="TxtDesNS" Width="150px" BackColor="#D6D3CE"></asp:TextBox></td>
                </tr>
                <tr>
                    <%--<td>
                        <asp:Label ID="LblPR_AV_TIPO_FAM_CER" runat="server" Text="Tipo Fam Cercano" CssClass="LblDesc"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="TxtPR_AV_TIPO_FAM_CER" runat="server" MaxLength="25" ReadOnly="True" CssClass="TxtDesNS" Width="100px"></asp:TextBox></td>--%>
                    <td>
                        <asp:Label ID="LblPR_AV_TELS_CASA_AV1" runat="server" Text="Tel Casa Aval 1" CssClass="LblDesc"></asp:Label></td>
                    <td class="auto-style1">
                        <asp:TextBox ID="TxtPR_AV_TELS_CASA_AV1" runat="server" MaxLength="25" ReadOnly="True" CssClass="TxtDesNS" Width="150px" BackColor="#D6D3CE"></asp:TextBox></td>
                    <td>
                        <asp:Label ID="LblPR_AV_TELS_CASA_AV2" runat="server" Text="Tel Casa Aval 2" CssClass="LblDesc"></asp:Label></td>
                    <td class="auto-style2">
                        <asp:TextBox ID="TxtPR_AV_TELS_CASA_AV2" runat="server" MaxLength="25" ReadOnly="True" CssClass="TxtDesNS" Width="150px" BackColor="#D6D3CE"></asp:TextBox></td>
                    <td>
                        <asp:Label ID="LblPR_AV_TELS_CASA_AV3" runat="server" Text="Tel Casa Aval 3" CssClass="LblDesc"></asp:Label></td>
                    <td class="auto-style3">
                        <asp:TextBox ID="TxtPR_AV_TELS_CASA_AV3" runat="server" MaxLength="25" ReadOnly="True" CssClass="TxtDesNS" Width="150px" BackColor="#D6D3CE"></asp:TextBox></td>
                    <td>
                        <asp:Label ID="LblPR_AV_TELS_CASA_FAM_CER" runat="server" Text="Tel Casa Fam Cercano" CssClass="LblDesc"></asp:Label></td>
                    <td class="auto-style4">
                        <asp:TextBox ID="TxtPR_AV_TELS_CASA_FAM_CER" runat="server" MaxLength="30" ReadOnly="True" CssClass="TxtDesNS" Width="150px" BackColor="#D6D3CE"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LblPR_AV_TELS_OFICINA_AV1" runat="server" Text="Tel Oficina Aval 1" CssClass="LblDesc"></asp:Label></td>
                    <td class="auto-style1">
                        <asp:TextBox ID="TxtPR_AV_TELS_OFICINA_AV1" runat="server" MaxLength="35" ReadOnly="True" CssClass="TxtDesNS" Width="150px" BackColor="#D6D3CE"></asp:TextBox></td>
                    <td>
                        <asp:Label ID="LblPR_AV_TELS_OFICINA_AV2" runat="server" Text="Tel Oficina Aval 2" CssClass="LblDesc"></asp:Label></td>
                    <td class="auto-style2">
                        <asp:TextBox ID="TxtPR_AV_TELS_OFICINA_AV2" runat="server" MaxLength="25" ReadOnly="True" CssClass="TxtDesNS" Width="150px" BackColor="#D6D3CE"></asp:TextBox></td>
                    <td>
                        <asp:Label ID="LblPR_AV_TELS_OFICINA_AV3" runat="server" Text="Tel Oficina Aval 3" CssClass="LblDesc"></asp:Label></td>
                    <td class="auto-style3">
                        <asp:TextBox ID="TxtPR_AV_TELS_OFICINA_AV3" runat="server" MaxLength="25" ReadOnly="True" CssClass="TxtDesNS" Width="150px" BackColor="#D6D3CE"></asp:TextBox></td>
                    <td>
                        <asp:Label ID="LblPR_AV_TELS_OFI_FAM_CER" runat="server" Text="Tel Oficina Fam Cercano" CssClass="LblDesc"></asp:Label></td>
                    <td class="auto-style4">
                        <asp:TextBox ID="TxtPR_AV_TELS_OFI_FAM_CER" runat="server" MaxLength="25" ReadOnly="True" CssClass="TxtDesNS" Width="150px" BackColor="#D6D3CE"></asp:TextBox></td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LblPR_AV_TELS_CEL_AV1" runat="server" Text="Tel Celular Aval 1" CssClass="LblDesc"></asp:Label></td>
                    <td class="auto-style1">
                        <asp:TextBox ID="TxtPR_AV_TELS_CEL_AV1" runat="server" MaxLength="25" ReadOnly="True" CssClass="TxtDesNS" Width="150px" BackColor="#D6D3CE"></asp:TextBox></td>
                    <td>
                        <asp:Label ID="LblPR_AV_TELS_CEL_AV2" runat="server" Text="Tel Celular Aval 2" CssClass="LblDesc"></asp:Label></td>
                    <td class="auto-style2">
                        <asp:TextBox ID="TxtPR_AV_TELS_CEL_AV2" runat="server" MaxLength="25" ReadOnly="True" CssClass="TxtDesNS" Width="150px" BackColor="#D6D3CE"></asp:TextBox></td>
                    <td>
                        <asp:Label ID="LblPR_AV_TELS_CEL_AV3" runat="server" Text="Tel Celular Aval 3" CssClass="LblDesc"></asp:Label></td>
                    <td class="auto-style3">
                        <asp:TextBox ID="TxtPR_AV_TELS_CEL_AV3" runat="server" MaxLength="25" ReadOnly="True" CssClass="TxtDesNS" Width="150px" BackColor="#D6D3CE"></asp:TextBox></td>
                    <td>
                        <asp:Label ID="LblPR_AV_TELS_CEL_FAM_CER" runat="server" Text="Tel Celular Fam Cercano" CssClass="LblDesc"></asp:Label></td>
                    <td class="auto-style4">
                        <asp:TextBox ID="TxtPR_AV_TELS_CEL_FAM_CER" runat="server" MaxLength="25" ReadOnly="True" CssClass="TxtDesNS" Width="150px" BackColor="#D6D3CE"></asp:TextBox></td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LblPR_AV_MAIL_AV1" runat="server" Text="Email Aval 1" CssClass="LblDesc"></asp:Label></td>
                    <td class="auto-style1">
                        <asp:TextBox ID="TxtPR_AV_MAIL_AV1" runat="server" MaxLength="80" ReadOnly="True" CssClass="TxtDesNS" Width="150px" BackColor="#D6D3CE"></asp:TextBox></td>
                    <td>
                        <asp:Label ID="LblPR_AV_MAIL_AV2" runat="server" Text="Email Aval 2" CssClass="LblDesc"></asp:Label></td>
                    <td class="auto-style2">
                        <asp:TextBox ID="TxtPR_AV_MAIL_AV2" runat="server" MaxLength="80" ReadOnly="True" CssClass="TxtDesNS" Width="150px" BackColor="#D6D3CE"></asp:TextBox></td>
                    <td>
                        <asp:Label ID="LblPR_AV_MAIL_AV3" runat="server" Text="Email Aval 3" CssClass="LblDesc"></asp:Label></td>
                    <td class="auto-style3">
                        <asp:TextBox ID="TxtPR_AV_MAIL_AV3" runat="server" MaxLength="80" ReadOnly="True" CssClass="TxtDesNS" Width="150px" BackColor="#D6D3CE"></asp:TextBox></td>
                    <td>
                        <asp:Label ID="LblPR_AV_EMAIL_FAM_CER" runat="server" Text="Email Fam Cercano" CssClass="LblDesc"></asp:Label></td>
                    <td class="auto-style4">
                        <asp:TextBox ID="TxtPR_AV_EMAIL_FAM_CER" runat="server" MaxLength="80" ReadOnly="True" CssClass="TxtDesNS" Width="150px" BackColor="#D6D3CE"></asp:TextBox></td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td class="auto-style1">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td class="auto-style2">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td class="auto-style3">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td class="auto-style4">
                        &nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="10" style="text-align: center">
                        <asp:Button ID="BtnModificar" runat="server" Text="Modificar Datos" CssClass="Botones" Visible="false" />
                    </td>
                </tr>
            </table>
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

