<%@ Control Language="VB" AutoEventWireup="false" CodeFile="InformacionReferencias.ascx.vb" Inherits="InformacionReferencias" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<link href="Estilos/HTML.css" rel="stylesheet" />
<link href="Estilos/ObjHtmlNoS.css" rel="stylesheet" />
<link href="Estilos/ObjHtmlS.css" rel="stylesheet" />
<asp:Label ID="LblCat_Lo_Usuario" runat="server" Visible="false"></asp:Label>
<table class="Table">
    <tr class="Titulos">
        <td colspan="4">Información Referencias
        </td>
    </tr>
    <tr>
        <td>
            <table>
                <tr class="Titulos">
                    <td colspan="2">Referencia 1</td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LblPR_AV_NOMBRE_REF1" runat="server" Text="Nombre" CssClass="LblDesc"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="TxtPR_AV_NOMBRE_REF1" runat="server" MaxLength="80" ReadOnly="True" CssClass="TxtDesNS" Width="100px" BackColor="#D6D3CE"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LblPR_AV_TELS_CASA_REF1" runat="server" Text="Telefono Casa" CssClass="LblDesc"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="TxtPR_AV_TELS_CASA_REF1" runat="server" MaxLength="25" ReadOnly="True" CssClass="TxtDesNS" Width="100px" BackColor="#D6D3CE"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LblPR_AV_MAIL_REF1" runat="server" Text="Email" CssClass="LblDesc"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="TxtPR_AV_MAIL_REF1" runat="server" MaxLength="80" ReadOnly="True" CssClass="TxtDesNS" Width="100px" BackColor="#D6D3CE"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LblPR_AV_TELS_OFICINA_REF1" runat="server" Text="Tel Oficina" CssClass="LblDesc"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="TxtPR_AV_TELS_OFICINA_REF1" runat="server" MaxLength="25" ReadOnly="True" CssClass="TxtDesNS" Width="100px" BackColor="#D6D3CE"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LblPR_AV_TELS_CEL_REF1" runat="server" Text="Tel Celular" CssClass="LblDesc"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="TxtPR_AV_TELS_CEL_REF1" runat="server" MaxLength="25" ReadOnly="True" CssClass="TxtDesNS" Width="100px" BackColor="#D6D3CE"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LblPR_AV_PARENTESCO_REF1" runat="server" Text="Parentesco" CssClass="LblDesc"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="TxtPR_AV_PARENTESCO_REF1" runat="server" MaxLength="25" ReadOnly="True" CssClass="TxtDesNS" Width="100px" BackColor="#D6D3CE"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LblPR_AV_DIR_REF1" runat="server" Text="Dirección" CssClass="LblDesc"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="TxtPR_AV_DIR_REF1" runat="server" MaxLength="120" ReadOnly="True" CssClass="TxtDesNS" Width="100px" BackColor="#D6D3CE"></asp:TextBox></td>
                </tr>
            </table>            
        </td>
        <td>
            <table>
                <tr class="Titulos">
                    <td colspan="2">Referencia 2</td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LblPR_AV_NOMBRE_REF2" runat="server" Text="Nombre" CssClass="LblDesc"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="TxtPR_AV_NOMBRE_REF2" runat="server" MaxLength="80" ReadOnly="True" CssClass="TxtDesNS" Width="100px" BackColor="#D6D3CE"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LblPR_AV_TELS_CASA_REF2" runat="server" Text="Telefono Casa" CssClass="LblDesc"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="TxtPR_AV_TELS_CASA_REF2" runat="server" MaxLength="25" ReadOnly="True" CssClass="TxtDesNS" Width="100px" BackColor="#D6D3CE"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LblPR_AV_MAIL_REF2" runat="server" Text="Email" CssClass="LblDesc"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="TxtPR_AV_MAIL_REF2" runat="server" MaxLength="80" ReadOnly="True" CssClass="TxtDesNS" Width="100px" BackColor="#D6D3CE"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LblPR_AV_TELS_OFICINA_REF2" runat="server" Text="Tel Oficina" CssClass="LblDesc"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="TxtPR_AV_TELS_OFICINA_REF2" runat="server" MaxLength="25" ReadOnly="True" CssClass="TxtDesNS" Width="100px" BackColor="#D6D3CE"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LblPR_AV_TELS_CEL_REF2" runat="server" Text="Tel Celular" CssClass="LblDesc"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="TxtPR_AV_TELS_CEL_REF2" runat="server" MaxLength="25" ReadOnly="True" CssClass="TxtDesNS" Width="100px" BackColor="#D6D3CE"></asp:TextBox></td>
                </tr>
                
                
                <tr>
                    <td>
                        <asp:Label ID="LblPR_AV_PARENTESCO_REF2" runat="server" Text="Parentesco" CssClass="LblDesc"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="TxtPR_AV_PARENTESCO_REF2" runat="server" MaxLength="25" ReadOnly="True" CssClass="TxtDesNS" Width="100px" BackColor="#D6D3CE"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LblPR_AV_DIR_REF2" runat="server" Text="Dirección" CssClass="LblDesc"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="TxtPR_AV_DIR_REF2" runat="server" MaxLength="120" ReadOnly="True" CssClass="TxtDesNS" Width="100px" BackColor="#D6D3CE"></asp:TextBox></td>
                </tr>
            </table>
        </td>
        <td>
            <table>
                <tr class="Titulos">
                    <td colspan="2">Referencia 3</td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LblPR_AV_NOMBRE_REF3" runat="server" Text="Nombre" CssClass="LblDesc"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="TxtPR_AV_NOMBRE_REF3" runat="server" MaxLength="80" ReadOnly="True" CssClass="TxtDesNS" Width="100px" BackColor="#D6D3CE"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LblPR_AV_TELS_CASA_REF3" runat="server" Text="Telefono Casa" CssClass="LblDesc"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="TxtPR_AV_TELS_CASA_REF3" runat="server" MaxLength="25" ReadOnly="True" CssClass="TxtDesNS" Width="100px" BackColor="#D6D3CE"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LblPR_AV_MAIL_REF3" runat="server" Text="Email" CssClass="LblDesc"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="TxtPR_AV_MAIL_REF3" runat="server" MaxLength="80" ReadOnly="True" CssClass="TxtDesNS" Width="100px" BackColor="#D6D3CE"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LblPR_AV_TELS_OFICINA_REF3" runat="server" Text="Tel Oficina" CssClass="LblDesc"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="TxtPR_AV_TELS_OFICINA_REF3" runat="server" MaxLength="25" ReadOnly="True" CssClass="TxtDesNS" Width="100px" BackColor="#D6D3CE"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LblPR_AV_TELS_CEL_REF3" runat="server" Text="Tel Celular" CssClass="LblDesc"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="TxtPR_AV_TELS_CEL_REF3" runat="server" MaxLength="25" ReadOnly="True" CssClass="TxtDesNS" Width="100px" BackColor="#D6D3CE"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LblPR_AV_PARENTESCO_REF3" runat="server" Text="Parentesco" CssClass="LblDesc"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="TxtPR_AV_PARENTESCO_REF3" runat="server" MaxLength="25" ReadOnly="True" CssClass="TxtDesNS" Width="100px" BackColor="#D6D3CE"></asp:TextBox></td>
                </tr>
                
                <tr>
                    <td>
                        <asp:Label ID="LblPR_AV_DIR_REF3" runat="server" Text="Dirección" CssClass="LblDesc"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="TxtPR_AV_DIR_REF3" runat="server" MaxLength="120" ReadOnly="True" CssClass="TxtDesNS" Width="100px" BackColor="#D6D3CE"></asp:TextBox></td>
                </tr>
            </table>
        </td>
        <td>
            <table>
                <tr class="Titulos">
                    <td colspan="2">Referencia 4</td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LblPR_AV_NOMBRE_REF4" runat="server" Text="Nombre" CssClass="LblDesc"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="TxtPR_AV_NOMBRE_REF4" runat="server" MaxLength="80" ReadOnly="True" CssClass="TxtDesNS" Width="100px" BackColor="#D6D3CE"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LblPR_AV_TELS_CASA_REF4" runat="server" Text="Telefono Casa" CssClass="LblDesc"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="TxtPR_AV_TELS_CASA_REF4" runat="server" MaxLength="25" ReadOnly="True" CssClass="TxtDesNS" Width="100px" BackColor="#D6D3CE"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LblPR_AV_MAIL_REF4" runat="server" Text="Email" CssClass="LblDesc"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="TxtPR_AV_MAIL_REF4" runat="server" MaxLength="80" ReadOnly="True" CssClass="TxtDesNS" Width="100px" BackColor="#D6D3CE"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LblPR_AV_TELS_OFICINA_REF4" runat="server" Text="Tel Oficina" CssClass="LblDesc"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="TxtPR_AV_TELS_OFICINA_REF4" runat="server" MaxLength="25" ReadOnly="True" CssClass="TxtDesNS" Width="100px" BackColor="#D6D3CE"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LblPR_AV_TELS_CEL_REF4" runat="server" Text="Tel Celular" CssClass="LblDesc"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="TxtPR_AV_TELS_CEL_REF4" runat="server" MaxLength="25" ReadOnly="True" CssClass="TxtDesNS" Width="100px" BackColor="#D6D3CE"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LblPR_AV_PARENTESCO_REF4" runat="server" Text="Parentesco" CssClass="LblDesc"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="TxtPR_AV_PARENTESCO_REF4" runat="server" MaxLength="25" ReadOnly="True" CssClass="TxtDesNS" Width="100px" BackColor="#D6D3CE"></asp:TextBox></td>
                </tr>
                
                <tr>
                    <td>
                        <asp:Label ID="LblPR_AV_DIR_REF4" runat="server" Text="Dirección" CssClass="LblDesc"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="TxtPR_AV_DIR_REF4" runat="server" MaxLength="120" ReadOnly="True" CssClass="TxtDesNS" Width="100px" BackColor="#D6D3CE"></asp:TextBox></td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <table>
                <tr class="Titulos">
                    <td colspan="2">Referencia 5</td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LblPR_AV_NOMBRE_REF5" runat="server" Text="Nombre" CssClass="LblDesc"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="TxtPR_AV_NOMBRE_REF5" runat="server" MaxLength="80" ReadOnly="True" CssClass="TxtDesNS" Width="100px" BackColor="#D6D3CE"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LblPR_AV_TELS_CASA_REF5" runat="server" Text="Telefono Casa" CssClass="LblDesc"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="TxtPR_AV_TELS_CASA_REF5" runat="server" MaxLength="25" ReadOnly="True" CssClass="TxtDesNS" Width="100px" BackColor="#D6D3CE"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LblPR_AV_MAIL_REF5" runat="server" Text="Email" CssClass="LblDesc"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="TxtPR_AV_MAIL_REF5" runat="server" MaxLength="80" ReadOnly="True" CssClass="TxtDesNS" Width="100px" BackColor="#D6D3CE"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LblPR_AV_TELS_OFICINA_REF5" runat="server" Text="Tel Oficina" CssClass="LblDesc"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="TxtPR_AV_TELS_OFICINA_REF5" runat="server" MaxLength="25" ReadOnly="True" CssClass="TxtDesNS" Width="100px" BackColor="#D6D3CE"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LblPR_AV_TELS_CEL_REF5" runat="server" Text="Tel Celular" CssClass="LblDesc"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="TxtPR_AV_TELS_CEL_REF5" runat="server" MaxLength="25" ReadOnly="True" CssClass="TxtDesNS" Width="100px" BackColor="#D6D3CE"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LblPR_AV_PARENTESCO_REF5" runat="server" Text="Parentesco" CssClass="LblDesc"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="TxtPR_AV_PARENTESCO_REF5" runat="server" MaxLength="25" ReadOnly="True" CssClass="TxtDesNS" Width="100px" BackColor="#D6D3CE"></asp:TextBox></td>
                </tr>
                
                <tr>
                    <td>
                        <asp:Label ID="LblPR_AV_DIR_REF5" runat="server" Text="Dirección" CssClass="LblDesc"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="TxtPR_AV_DIR_REF5" runat="server" MaxLength="120" ReadOnly="True" CssClass="TxtDesNS" Width="100px" BackColor="#D6D3CE"></asp:TextBox></td>
                </tr>
            </table>
        </td>
        <td>
            <table>
                <tr class="Titulos">
                    <td colspan="2">Referencia 6</td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LblPR_AV_NOMBRE_REF6" runat="server" Text="Nombre" CssClass="LblDesc"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="TxtPR_AV_NOMBRE_REF6" runat="server" MaxLength="80" ReadOnly="True" CssClass="TxtDesNS" Width="100px" BackColor="#D6D3CE"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LblPR_AV_TELS_CASA_REF6" runat="server" Text="Telefono Casa" CssClass="LblDesc"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="TxtPR_AV_TELS_CASA_REF6" runat="server" MaxLength="25" ReadOnly="True" CssClass="TxtDesNS" Width="100px" BackColor="#D6D3CE"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LblPR_AV_MAIL_REF6" runat="server" Text="Email" CssClass="LblDesc"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="TxtPR_AV_MAIL_REF6" runat="server" MaxLength="80" ReadOnly="True" CssClass="TxtDesNS" Width="100px" BackColor="#D6D3CE"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LblPR_AV_TELS_OFICINA_REF6" runat="server" Text="Tel Oficina" CssClass="LblDesc"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="TxtPR_AV_TELS_OFICINA_REF6" runat="server" MaxLength="25" ReadOnly="True" CssClass="TxtDesNS" Width="100px" BackColor="#D6D3CE"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LblPR_AV_TELS_CEL_REF6" runat="server" Text="Tel Celular" CssClass="LblDesc"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="TxtPR_AV_TELS_CEL_REF6" runat="server" MaxLength="25" ReadOnly="True" CssClass="TxtDesNS" Width="100px" BackColor="#D6D3CE"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LblPR_AV_PARENTESCO_REF6" runat="server" Text="Parentesco" CssClass="LblDesc"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="TxtPR_AV_PARENTESCO_REF6" runat="server" MaxLength="25" ReadOnly="True" CssClass="TxtDesNS" Width="100px" BackColor="#D6D3CE"></asp:TextBox></td>
                </tr>
                
                <tr>
                    <td>
                        <asp:Label ID="LblPR_AV_DIR_REF6" runat="server" Text="Dirección" CssClass="LblDesc"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="TxtPR_AV_DIR_REF6" runat="server" MaxLength="120" ReadOnly="True" CssClass="TxtDesNS" Width="100px" BackColor="#D6D3CE"></asp:TextBox></td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="4" align="center">
            <asp:Button ID="BtnModificar" runat="server" Text="Modificar Datos" CssClass="Botones" Visible="false" />
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


