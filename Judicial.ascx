<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Judicial.ascx.vb" Inherits="Judicial" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<link href="Estilos/HTML.css" rel="stylesheet" />
<link href="Estilos/ObjHtmlNoS.css" rel="stylesheet" />
<link href="Estilos/ObjHtmlS.css" rel="stylesheet" />
<link href="Estilos/ObjAjax.css" rel="stylesheet" />
<asp:Label ID="LblCat_Lo_Usuario" runat="server" Visible="false"></asp:Label>
<table class="Table">
    <tr class="Titulos">
        <td>Judicial</td>
    </tr>
    <tr class="Arriba" align="center">
        <td align="left">
            <table>
                <tr class="Arriba">
                    <td>
                        <table>
                            <tr class="Titulos">
                                <td colspan="4">Estatus</td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <asp:Image ID="ImgSemaforoJ" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="LblPR_JU_ABOGADO" runat="server" Text="Abogado Trabajó" CssClass="LblDesc"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtPR_JU_ABOGADO" runat="server" ReadOnly="True" CssClass="TxtDesNS"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="LblPR_JU_ETAPA" runat="server" Text="Última Etapa " CssClass="LblDesc"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtPR_JU_ETAPA" runat="server" ReadOnly="True" CssClass="TxtDesNS"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="LblPR_JU_FECHAETAPA" runat="server" Text="Fecha Actividad" CssClass="LblDesc"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtPR_JU_FECHAETAPA" runat="server" ReadOnly="True" CssClass="TxtDesNS"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="LblPR_JU_TIPOJUICIO" runat="server" Text="Tipo Juicio" CssClass="LblDesc"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtPR_JU_TIPOJUICIO" runat="server" ReadOnly="True"  CssClass="TxtDesNS"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="LblPR_JU_FECHAENJUDICIAL" runat="server" Text="Fecha De Asignación A Legal" CssClass="LblDesc"></asp:Label>
                                </td>
                                <td colspan="3">
                                    <asp:TextBox ID="TxtPR_JU_FECHAENJUDICIAL" runat="server" ReadOnly="True" CssClass="TxtDesNS"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="LblPR_JU_FECHAABOGADO" runat="server" Text="Fecha De Asignación Abogado" CssClass="LblDesc"></asp:Label>
                                </td>
                                <td colspan="3">
                                    <asp:TextBox ID="TxtPR_JU_FECHAABOGADO" runat="server" ReadOnly="True" CssClass="TxtDesNS"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <table class="Table">
                            <tr class="Titulos">
                                <td colspan="2">Gestión Judicial </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="LblHIST_JU_COMENTARIO" runat="server" CssClass="LblDesc" Text="Comentario"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtHIST_JU_COMENTARIO" runat="server" CssClass="fuenteTxt" Height="72px" MaxLength="500" TextMode="MultiLine" Width="389px"></asp:TextBox>
                                    <asp:FilteredTextBoxExtender ID="TxtHIST_JU_COMENTARIO_FilteredTextBoxExtender0" runat="server" Enabled="True" TargetControlID="TxtHIST_JU_COMENTARIO" ValidChars="aqzxswcdevfrbgtnhymjukiloñpZAQXSWCDEVFRBGTNHYMJUKILOPÑ1230456789.,$ %"></asp:FilteredTextBoxExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="LblHIST_JU_ESTADO" runat="server" CssClass="LblDesc" Text="Estado" Visible="False"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="DdlHIST_JU_ESTADO" runat="server" AutoPostBack="True">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="LblHIST_JU_TIPOJUICIO" runat="server" CssClass="LblDesc" Text="Juicio" Visible="False"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="DdlHIST_JU_TIPOJUICIO" runat="server" AutoPostBack="True">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="LblHIST_JU_ETAPA" runat="server" CssClass="LblDesc" Text="Etapa Judicial" Visible="False"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="DdlHIST_JU_ETAPA" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td >
                                    <asp:Label ID="LblHIST_JU_DTEETAPA" runat="server" CssClass="LblDesc" Text="Fecha" Visible="False"></asp:Label>
                                </td>
                                <td >
                                    <asp:TextBox ID="TxtHIST_JU_DTEETAPA" runat="server" CssClass="fuenteTxt" MaxLength="10" Visible="false"></asp:TextBox>
                                    <asp:CalendarExtender ID="TxtHIST_JU_DTEETAPA_CE" runat="server" Enabled="True" PopupButtonID="TxtHIST_JU_DTEETAPA" TargetControlID="TxtHIST_JU_DTEETAPA" CssClass="Calendario"></asp:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="ChkModificar" runat="server" AutoPostBack="true" Text="Modificar Juicio" CssClass="LblDesc" />
                                </td>
                                <td>
                                    <asp:DropDownList ID="DrlCambioTipoJuicio" runat="server" Visible="false">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="centrar" colspan="2">&nbsp;
                                    <asp:Button ID="BtnGuarJud" runat="server" CssClass="Botones" Text="Guardar" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr class="Titulos">
        <td>Datos Del Juicio</td>
    </tr>
    <tr>
        <td>
            <table>
                <tr>
                    <td>
                        <asp:Label ID="LblPR_JU_JUZGADO" runat="server" CssClass="LblDesc" Text="Juzgado"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TxtPR_JU_JUZGADO" runat="server" CssClass="fuenteTxt" MaxLength="25"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="LblPR_JU_LOCALIDAD" runat="server" CssClass="LblDesc" Text="Localidad"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TxtPR_JU_LOCALIDAD" runat="server" CssClass="fuenteTxt" MaxLength="25"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="LblPR_JU_EXPEDIENTE" runat="server" CssClass="LblDesc" Text="Folio / Número de Expediente"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TxtPR_JU_EXPEDIENTE" runat="server" CssClass="fuenteTxt" MaxLength="25"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="LblPR_JU_ABOGADO1" runat="server" CssClass="LblDesc" Text="Abogado Responsable"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="DdlPR_JU_ABOGADO" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>
                        <asp:Button ID="BtnGuarJuicio" runat="server" CssClass="Botones" Text="Guardar" />
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
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
