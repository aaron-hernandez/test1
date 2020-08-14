<%@ Control Language="VB" AutoEventWireup="false" CodeFile="InformacionAdicional.ascx.vb" Inherits="Estilos_InformacionAdicional" %>
<link href="Estilos/HTML.css" rel="stylesheet" />
<link href="Estilos/Modal.css" rel="stylesheet" />
<link href="Estilos/ObjAjax.css" rel="stylesheet" />
<link href="Estilos/ObjHtmlS.css" rel="stylesheet" />
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:UpdatePanel runat="server" ID="UpnlAddAdicional">
    <ContentTemplate>
        <asp:Label ID="LblCat_Lo_Usuario" runat="server" Visible="false"></asp:Label>
        <table class="Table">
            <tr class="Espacio">
                <td></td>
            </tr>
            <tr class="Titulos">
                <td>Información Adicional</td>
            </tr>
            <tr class="Centro">
                <td>
                    <asp:UpdatePanel runat="server" ID="UpnlOpcionesAdd">
                        <ContentTemplate>
                            <table class="Table">
                                <tr>
                                    <td>
                                        <asp:Button ID="BtnAgregarTelefonos" runat="server" CssClass="Botones" Text="Agregar Teléfono" /></td>
                                    <td>
                                        <asp:Button ID="BtnAgregarDirecciones" runat="server" CssClass="Botones" Text="Agregar Dirección" /></td>
                                    <td>
                                        <asp:Button ID="BtnAgregarCorreos" runat="server" CssClass="Botones" Text="Agregar Correo" /></td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Panel ID="PnlTelefonos" runat="server">
                        <table class="Table">
                            <tr class="Titulos">
                                <td>Teléfonos </td>
                            </tr>
                            <tr align="center">
                                <td>
                                    <div class="ScroolAddTelefonos">
                                        <div class="force-overflow">
                                        <asp:GridView ID="GvTelefonos" runat="server" CssClass="mGrid" Font-Names="Tahoma" Font-Size="Small">
                                            <AlternatingRowStyle CssClass="alt" />
                                            <PagerStyle CssClass="pgr" />
                                            <Columns>
                                                <asp:CommandField SelectText="Modificar" ShowSelectButton="True" ButtonType="Image"
                                                    SelectImageUrl="~/Imagenes/ImgModificar.png" HeaderText="Mod" />
                                            </Columns>
                                        </asp:GridView>
                                            </div>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpnlAddTelefonos" runat="server" Visible="False">
                        <ContentTemplate>
                            <table class="Table">
                                <tr class="Titulos">
                                    <td>
                                        <asp:Label ID="LblTelefono" runat="server"></asp:Label></td>
                                </tr>
                                <tr align="center">
                                    <td>
                                        <table class="Izquierda">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="LblHist_Te_Tipo" runat="server" CssClass="LblDesc" Text="Tipo"></asp:Label></td>
                                                <td colspan="3">
                                                    <asp:DropDownList ID="DdlHist_Te_Tipo" runat="server" AutoPostBack="true" CssClass="DdlDesc">
                                                        <asp:ListItem>Casa</asp:ListItem>
                                                        <asp:ListItem>Celular</asp:ListItem>
                                                        <asp:ListItem>Oficina</asp:ListItem>
                                                        <asp:ListItem>Fax</asp:ListItem>
                                                        <asp:ListItem>Otro</asp:ListItem>
                                                    </asp:DropDownList></td>
                                                <td rowspan="7" class="Arriba">
                                                    <asp:GridView ID="GvDiasTel" runat="server" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="True" CssClass="mGrid" Font-Names="Tahoma" Font-Size="small" PagerStyle-CssClass="pgr">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Seleccionar">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkSelect" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="LblHist_Te_Lada" runat="server" CssClass="LblDesc" Text="Lada"></asp:Label></td>
                                                <td colspan="3">
                                                    <asp:TextBox ID="TxtHist_Te_Lada" runat="server" MaxLength="5" Width="40"></asp:TextBox>&#160;&#160;&#160;
                                                                                                <asp:Label ID="LblHist_Te_Numerotel" runat="server" CssClass="LblDesc" Text="Teléfono"></asp:Label>&#160;&#160;<asp:TextBox ID="TxtHist_Te_Numerotel" runat="server" MaxLength="10" Width="100"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="LblHist_Te_Horario" runat="server" CssClass="LblDesc" Text="Horario"></asp:Label></td>
                                                <td>
                                                    <asp:TextBox ID="TxtHist_Te_Horario0" runat="server" MaxLength="5" Width="40"></asp:TextBox><asp:MaskedEditExtender ID="Hist_Te_Horario0MEE" runat="server" AcceptNegative="None" ErrorTooltipEnabled="True" InputDirection="RightToLeft" Mask="99:99" MaskType="Time" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" TargetControlID="TxtHist_Te_Horario0"></asp:MaskedEditExtender>
                                                    &#160;<asp:Label ID="LblHist_Te_Horario0" runat="server" CssClass="LblDesc" Text="a"></asp:Label>&#160;<asp:TextBox ID="TxtHist_Te_Horario1" runat="server" MaxLength="5" Width="40"></asp:TextBox><asp:MaskedEditExtender ID="Hist_Te_Horario1MEE" runat="server" AcceptNegative="None" ErrorTooltipEnabled="True" InputDirection="RightToLeft" Mask="99:99" MaskType="Time" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" TargetControlID="TxtHist_Te_Horario1"></asp:MaskedEditExtender>
                                                </td>
                                                <td>
                                                    <asp:Label ID="LblHist_Te_Extension" runat="server" CssClass="LblDesc" Text="Extensión" Visible="false"></asp:Label></td>
                                                <td>
                                                    <asp:TextBox ID="TxtHist_Te_Extension" runat="server" MaxLength="10" Visible="false" Width="40"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="LblHist_Te_Parentesco" runat="server" CssClass="LblDesc" Text="Parentesco"></asp:Label></td>
                                                <td colspan="3">
                                                    <asp:DropDownList ID="DdlHist_Te_Parentesco" runat="server" AutoPostBack="true" CssClass="DdlDesc">
                                                        <asp:ListItem Selected="True">Cliente</asp:ListItem>
                                                        <asp:ListItem>Compañero De Trabajo</asp:ListItem>
                                                        <asp:ListItem>Vecino</asp:ListItem>
                                                        <asp:ListItem>Familiar</asp:ListItem>
                                                        <asp:ListItem>Conyuge</asp:ListItem>
                                                        <asp:ListItem>Amigo</asp:ListItem>
                                                        <asp:ListItem>Referencia</asp:ListItem>
                                                        <asp:ListItem>Aval</asp:ListItem>
                                                    </asp:DropDownList></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="LblHist_Te_Nombre" runat="server" CssClass="LblDesc" Text="Nombre" Visible="false"></asp:Label></td>
                                                <td colspan="3">
                                                    <asp:TextBox ID="TxtHist_Te_Nombre" runat="server" MaxLength="50" Visible="false" Width="280"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="LblHist_Te_Proporciona" runat="server" CssClass="LblDesc" Text="Proporciona información"></asp:Label></td>
                                                <td colspan="3">
                                                    <asp:TextBox ID="TxtHist_Te_Proporciona" runat="server" MaxLength="50"  Width="280"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;</td>
                                                <td colspan="3">
                                                    <asp:Label ID="LblHist_Te_Consecutivo" runat="server" CssClass="LblDesc" Visible="false"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="BtnCancelAddTel" runat="server" CssClass="Botones" Text="Cancelar" /></td>
                                                <td colspan="3" style="text-align: right">
                                                    <asp:Button ID="BtnAddTel" runat="server" CssClass="Botones" /></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Panel ID="PnlDirecciones" runat="server">
                        <table class="Table">
                            <tr class="Titulos">
                                <td>Direcciones </td>
                            </tr>
                            <tr align="center">
                                <td>
                                    <div class="ScroolAddDirecciones">
                                        <div class="force-overflow">
                                        <asp:GridView ID="GvDirecciones" runat="server" CssClass="mGrid" Font-Names="Tahoma" Font-Size="Small">
                                            <AlternatingRowStyle CssClass="alt" />
                                            <PagerStyle CssClass="pgr" />
                                            <Columns>
                                                <asp:CommandField SelectText="Modificar" ShowSelectButton="True" ButtonType="Image"
                                                    SelectImageUrl="~/Imagenes/ImgModificar.png" HeaderText="Mod" />
                                            </Columns>
                                        </asp:GridView>
                                            </div>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpnlAddDirecciones" runat="server" Visible="False">
                        <ContentTemplate>
                            <table class="Table">
                                <tr class="Titulos">
                                    <td>
                                        <asp:Label ID="LblDireccion" runat="server"></asp:Label></td>
                                </tr>
                                <tr align="center">
                                    <td>
                                        <table class="Izquierda">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="LblHist_Di_Cp" runat="server" CssClass="LblDesc" Text="Código Postal"></asp:Label></td>
                                                <td>
                                                    <asp:TextBox ID="TxtHist_Di_Cp" runat="server" AutoPostBack="True" MaxLength="5" Width="40px"></asp:TextBox></td>
                                                <td>
                                                    <asp:Label ID="LblHist_Di_Ciudad" runat="server" CssClass="LblDesc" Text="Ciudad"></asp:Label></td>
                                                <td>
                                                    <asp:TextBox ID="TxtHist_Di_Ciudad" runat="server" Enabled="False" MaxLength="40" Width="200px"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="LblHist_Di_Estado" runat="server" CssClass="LblDesc" Text="Estado"></asp:Label></td>
                                                <td>
                                                    <asp:TextBox ID="TxtHist_Di_Estado" runat="server" Enabled="False" MaxLength="20" Width="200px"></asp:TextBox></td>
                                                <td>
                                                    <asp:Label ID="LblHist_Di_Muni" runat="server" CssClass="LblDesc" Text="Delegación o Municipio"></asp:Label></td>
                                                <td>
                                                    <asp:TextBox ID="TxtHist_Di_Muni" runat="server" Enabled="False" MaxLength="50" Width="200px"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="LblHist_Di_Colonia" runat="server" CssClass="LblDesc" Text="Colonia"></asp:Label></td>
                                                <td>
                                                    <asp:DropDownList ID="DdlHist_Di_Colonia" runat="server" CssClass="DdlDesc"></asp:DropDownList></td>
                                                <td>
                                                    <asp:Label ID="LblHist_Di_Calle" runat="server" CssClass="LblDesc" Text="Calle"></asp:Label></td>
                                                <td>
                                                    <asp:TextBox ID="TxtHist_Di_Calle" runat="server" MaxLength="100" Width="200px"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="LblHist_Di_Numint" runat="server" CssClass="LblDesc" Text="Número Interior"></asp:Label></td>
                                                <td>
                                                    <asp:TextBox ID="TxtHist_Di_Numint" runat="server" MaxLength="10" Width="200px"></asp:TextBox></td>
                                                <td>
                                                    <asp:Label ID="LblHist_Di_Parentesco" runat="server" CssClass="LblDesc" Text="Parentesco"></asp:Label></td>
                                                <td>
                                                    <asp:DropDownList ID="DdlHist_Di_Parentesco" runat="server" AutoPostBack="true" CssClass="DdlDesc">
                                                        <asp:ListItem Selected="True">Cliente</asp:ListItem>
                                                        <asp:ListItem>Compañero De Trabajo</asp:ListItem>
                                                        <asp:ListItem>Vecino</asp:ListItem>
                                                        <asp:ListItem>Familiar</asp:ListItem>
                                                        <asp:ListItem>Conyuge</asp:ListItem>
                                                        <asp:ListItem>Amigo</asp:ListItem>
                                                        <asp:ListItem>Referencia</asp:ListItem>
                                                        <asp:ListItem>Aval</asp:ListItem>
                                                    </asp:DropDownList></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="LblHist_Di_Numext" runat="server" CssClass="LblDesc" Text="Número Exterior"></asp:Label></td>
                                                <td>
                                                    <asp:TextBox ID="TxtHist_Di_Numext" runat="server" MaxLength="10" Width="200px"></asp:TextBox></td>
                                                <td>
                                                    <asp:Label ID="LblHist_Di_Nombre" runat="server" CssClass="LblDesc" Text="Nombre" Visible="False"></asp:Label></td>
                                                <td>
                                                    <asp:TextBox ID="TxtHist_Di_Nombre" runat="server" MaxLength="20" Visible="False" Width="200px"></asp:TextBox></td>
                                            </tr>
                                            
                                            <tr class="Arriba">
                                                <td>
                                                    <asp:Label ID="LblHist_Di_Horario" runat="server" CssClass="LblDesc" Text="Horario"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TxtHist_Di_Horario0" runat="server" MaxLength="5" Width="40"></asp:TextBox>
                                                    <asp:MaskedEditExtender ID="TxtHist_Di_Horario0_MaskedEditExtender" runat="server" AcceptNegative="None" ErrorTooltipEnabled="True" InputDirection="RightToLeft" Mask="99:99" MaskType="Time" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" TargetControlID="TxtHist_Di_Horario0">
                                                    </asp:MaskedEditExtender>
                                                    <asp:Label ID="LblHist_Di_Horario2" runat="server" CssClass="LblDesc" Text="a"></asp:Label>
                                                    <asp:TextBox ID="TxtHist_Di_Horario1" runat="server" MaxLength="5" Width="40"></asp:TextBox>
                                                    <asp:MaskedEditExtender ID="TxtHist_Di_Horario1_MaskedEditExtender" runat="server" AcceptNegative="None" ErrorTooltipEnabled="True" InputDirection="RightToLeft" Mask="99:99" MaskType="Time" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" TargetControlID="TxtHist_Di_Horario1">
                                                    </asp:MaskedEditExtender>
                                                </td>
                                                <td rowspan="5">
                                                    <asp:CheckBox ID="CbxHist_Di_Contacto" runat="server" CssClass="CbxDesc" Text="Contacto" /></td>
                                                <td rowspan="5">
                                                    <asp:GridView ID="GvDiasDir" runat="server" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="True" CssClass="mGrid" Font-Names="Tahoma" Font-Size="small" PagerStyle-CssClass="pgr">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Seleccionar">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkSelect" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="LblHist_Di_Proporciona" runat="server" CssClass="LblDesc" Text="Proporciona información"></asp:Label></td>
                                                <td colspan="3">
                                                    <asp:TextBox ID="TxtHist_Di_Proporciona" runat="server" MaxLength="50" Width="280"></asp:TextBox></td>
                                            </tr>
                                            <tr class="Arriba">
                                                <td>
                                                    <asp:Button ID="BtnCancelAddDir" runat="server" CssClass="Botones" Text="Cancelar" /></td>
                                                <td>
                                                    <asp:Button ID="BtnAddDir" runat="server" CssClass="Botones" /></td>
                                            </tr>
                                            <tr class="Arriba">
                                                <td>&nbsp;</td>
                                                <td>
                                                    <asp:Label ID="LblHist_Di_Consecutivo" runat="server" CssClass="LblDesc" Visible="False"></asp:Label></td>
                                            </tr>
                                            <tr class="Arriba">
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                            </tr>
                                            <tr class="Arriba">
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Panel ID="PnlCorreos" runat="server">
                        <table class="Table">
                            <tr class="Titulos">
                                <td>Correos </td>
                            </tr>
                            <tr align="center">
                                <td>
                                    <div class="ScroolAddCorreo">
                                        <div class="force-overflow">
                                        <asp:GridView ID="GvCorreos" runat="server" CssClass="mGrid" Font-Names="Tahoma" Font-Size="Small">
                                            <AlternatingRowStyle CssClass="alt" />
                                            <PagerStyle CssClass="pgr" />
                                            <Columns>
                                                <asp:CommandField SelectText="Modificar" ShowSelectButton="True" ButtonType="Image"
                                                    SelectImageUrl="~/Imagenes/ImgModificar.png" HeaderText="Mod" />
                                            </Columns>
                                        </asp:GridView>
                                            </div>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpnlAddCorreos" runat="server" Visible="False">
                        <ContentTemplate>
                            <table class="Table">
                                <tr class="Titulos">
                                    <td>
                                        <asp:Label ID="LblCorreo" runat="server"></asp:Label></td>
                                </tr>
                                <tr align="center">
                                    <td>
                                        <table class="Izquierda">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="LblHist_Co_Tipo" runat="server" CssClass="LblDesc" Text="Tipo"></asp:Label></td>
                                                <td colspan="3">
                                                    <asp:DropDownList ID="DdlHist_Co_Tipo" runat="server" AutoPostBack="true" CssClass="DdlDesc">
                                                        <asp:ListItem Selected="True">Adicional</asp:ListItem>
                                                        <asp:ListItem>Trabajo</asp:ListItem>
                                                        <asp:ListItem>Personal</asp:ListItem>
                                                    </asp:DropDownList></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="LblHist_Co_Correo" runat="server" CssClass="LblDesc" Text="Correo"></asp:Label></td>
                                                <td colspan="3">
                                                    <asp:TextBox ID="TxtHist_Co_Correo" runat="server" MaxLength="100" Width="200px"></asp:TextBox>
                                                    <asp:FilteredTextBoxExtender ID="FiltroTxtHist_Co_Correo" runat="server" Enabled="True" TargetControlID="TxtHist_Co_Correo" ValidChars="aqzxswcdevfrbgtnhymjukiloñpZAQXSWCDEVFRBGTNHYMJUKILOPÑ1230456789@$./-_#">
                                                    </asp:FilteredTextBoxExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="LblHist_Co_Parentesco" runat="server" CssClass="LblDesc" Text="Parentesco"></asp:Label></td>
                                                <td>
                                                    <asp:DropDownList ID="DdlHist_Co_Parentesco" runat="server" AutoPostBack="true" CssClass="DdlDesc">
                                                        <asp:ListItem Selected="True">Cliente</asp:ListItem>
                                                        <asp:ListItem>Compañero De Trabajo</asp:ListItem>
                                                        <asp:ListItem>Vecino</asp:ListItem>
                                                        <asp:ListItem>Familiar</asp:ListItem>
                                                        <asp:ListItem>Conyuge</asp:ListItem>
                                                        <asp:ListItem>Amigo</asp:ListItem>
                                                        <asp:ListItem>Referencia</asp:ListItem>
                                                        <asp:ListItem>Aval</asp:ListItem>
                                                    </asp:DropDownList></td>
                                                <td>
                                                    <asp:Label ID="LblHist_Co_Nombre" runat="server" CssClass="LblDesc" Text="Nombre" Visible="False"></asp:Label></td>
                                                <td>
                                                    <asp:TextBox ID="TxtHist_Co_Nombre" runat="server" MaxLength="20" Visible="False" Width="200px"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="LblHist_Co_Proporciona" runat="server" CssClass="LblDesc" Text="Proporciona información"></asp:Label></td>
                                                <td colspan="3">
                                                    <asp:TextBox ID="TxtHist_Co_Proporciona" runat="server" MaxLength="50" Width="280"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:CheckBox ID="CbxHist_Co_Contacto" runat="server" CssClass="CbxDesc" Text="Contacto" /></td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="BtnCancelAddCorreo" runat="server" CssClass="Botones" Text="Cancelar" /></td>
                                                <td>
                                                    <asp:Button ID="BtnAddCorreo" runat="server" CssClass="Botones" /></td>
                                                <td>
                                                    <asp:Label ID="LblHist_Co_Consecutivo" runat="server" CssClass="LblDesc" Visible="False"></asp:Label></td>
                                                <td>&nbsp;</td>
                                            </tr>
                                            <tr>
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
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
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
