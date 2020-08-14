<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Negociaciones.ascx.vb" Inherits="Negociaciones" %>
<link href="Estilos/ObjHtmlS.css" rel="stylesheet" />
<link href="Estilos/HTML.css" rel="stylesheet" />
<link href="Estilos/ObjHtmlNoS.css" rel="stylesheet" />
<link href="Estilos/ObjAjax.css" rel="stylesheet" />
<link href="Estilos/Modal.css" rel="stylesheet" />
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Panel ID="PnlEstatus" runat="server" Visible="false">
<asp:Panel ID="PnlNeGociacion" runat="server" Visible="true">
    <table class="Table">
        <tr class="Titulos">
            <td>Negociaciones
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LblCat_Ne_Nombre" runat="server" Text="Tipo De Acuerdo" CssClass="LblDesc"></asp:Label>
                <asp:DropDownList ID="DdlCat_Ne_Nombre" runat="server" CssClass="DdlDesc" AutoPostBack="true">
                    <asp:ListItem Value="Seleccione" Selected="True"  ></asp:ListItem>
                    <asp:ListItem Value="CAT_NEGO_QUITAS">Quitas</asp:ListItem>
                    <asp:ListItem Value="CAT_NEGO_PAGOS_FIJOS">Pagos Fijos</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        
        <tr>
            <td>
                <asp:Panel runat="server" ID="PnlDetalle" Visible="false">
                    <table class="Table">
                        <tr class="Arriba">
                            <td style="width: 50%">
                                <table class="Table">
                                    <tr class="Titulos">
                                        <td colspan="4">Información Financiera</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="LblCartera" runat="server" CssClass="LblDesc" Text="Cartera"></asp:Label>
                                        </td>

                                        <td>
                                            <asp:TextBox ID="TxtCartera" runat="server" CssClass="TxtDesNS" MaxLength="22" ReadOnly="True"></asp:TextBox>
                                        </td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="LblPR_CF_BUCKET" runat="server" CssClass="LblDesc" Text="Bucket"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtPR_CF_BUCKET" runat="server" CssClass="TxtDesNS" MaxLength="22" ReadOnly="True" Width="90px"></asp:TextBox>
                                        </td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="LblPR_CF_SALDO_TOT" runat="server" CssClass="LblDesc" Text="Saldo Total"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtPR_CF_SALDO_TOT" runat="server" CssClass="TxtDesNS" ReadOnly="true" Width="90px"></asp:TextBox>
                                        </td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td colspan ="2">
                                            <asp:Label ID="LblDteOriginacionContrato" runat="server" CssClass="LblDesc" Text="Fecha de Originación de Contrato"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtDteOriginacionContrato" runat="server" CssClass="TxtDesNS" MaxLength="22" ReadOnly="True"></asp:TextBox>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td colspan ="2">
                                            <asp:Label ID="LblDteLimiteNegociacion" runat="server" CssClass="LblDesc" Text="Fecha de Límite de Negociación"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtDteLimiteNegociacion" runat="server" CssClass="TxtDesNS" MaxLength="22" ReadOnly="True"></asp:TextBox>
                                        </td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td><asp:Label ID="LblTip" runat="server" Text="Mintras no rebase la fecha limte de pago"></asp:Label>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <table class="Table">
                                    <tr class="Titulos">
                                        <td>Opciones De Negociación</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table class="Table ">
                                                <tr>
                                                    <td align="center">
                                                        <asp:GridView ID="GvOpcionesNegociacion" runat="server" CssClass="mGrid" Font-Names="Tahoma" Font-Size="small"  >
                                                            
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
            </asp:Panel>
        </td>
    </tr>

    <tr>
        <td>
            <asp:Panel runat="server" ID="PnlConfiguracion" Visible="false">
                <table class="Table">
                    <tr class="Arriba">
                        <td>
                            <table class="Table">
                                <tr>
                                    <td class="Titulos" colspan="4">Negociación</td>
                                </tr>
                                <tr><td>
            <asp:Label ID="LblExhibiciones" runat="server" Text="Exhibiciones" CssClass="LblDesc"   ></asp:Label>
            </td>
                                    <td>
                                        <asp:DropDownList ID="DdlExhibiciones" runat="server" AutoPostBack="True" CssClass="DdlDesc" >
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="LblContacto" runat="server" CssClass="LblDesc" Text="Persona de Contacto"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DdlContacto" runat="server"  CssClass="DdlDesc">
                                            <asp:ListItem Selected="True" Value="Seleccione">Seleccione</asp:ListItem>
                                            <asp:ListItem Value="Alumno">Alumno</asp:ListItem>
                                            <asp:ListItem Value="Padre">Padre</asp:ListItem>
                                            <asp:ListItem Value="Madre">Madre</asp:ListItem>
                                            <asp:ListItem Value="Aval">Aval</asp:ListItem>
                                            <asp:ListItem Value="Conyuge">Conyuge</asp:ListItem>
                                            <asp:ListItem Value="Otro">Otro</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>Descuento Maximo: </td>
                                    <td><asp:Label ID="LblDescuentoMax" runat="server" CssClass="LblDesc" >
                                        </asp:Label></td>
                                </tr>
                                <tr>
                                    <td>
                                         <asp:Label ID="LblSaldoNegociado" runat="server" CssClass="LblDesc" Text="Saldo Negociado"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtSaldoNegociado" runat="server" AutoPostBack="True" CssClass="TxtDesc" Text =""></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="FtbTxtSaldoNegociado" runat="server" Enabled="True" TargetControlID="TxtSaldoNegociado" ValidChars="1234567890.">
                                        </asp:FilteredTextBoxExtender>
                                    </td>
                                    <td colspan="2">El pago MontoNegociado no puede ser Menor a:
                                        <asp:Label ID="LblMontoNegoMin" runat="server" ForeColor="#006600"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="LblDescuento" runat="server" CssClass="LblDesc" Text="Descuento">
                                        </asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtDescuento" runat="server" AutoPostBack="True" CssClass="TxtDesc" Width="70px"></asp:TextBox>
                                    </td>
                                    <td colspan="2"><%--El saldo Negociado no puede ser menor a:--%> 
                                        <%--<asp:Label ID="LblMinSaldoNego" runat="server" ForeColor="#006600"></asp:Label>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblPeriodicidad" runat="server" CssClass="LblDesc" Text="Periodicidad">
                                        </asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DdlPeriodicidad" runat="server"  AutoPostBack ="true"  CssClass="DdlDesc">
                                            <asp:ListItem Selected="True" Value="Seleccione">Seleccione</asp:ListItem>
                                            <asp:ListItem Value="Semanal">Semanal</asp:ListItem>
                                            <asp:ListItem Value="Quincenal">Quincenal</asp:ListItem>
                                            <asp:ListItem Value="Mensual">Mensual</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    
                                    <td>
                                        <asp:Label ID="LblNumPagos" runat="server" CssClass="LblDesc" Text="Numero de Pagos" Visible ="false" >
                                        </asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID ="TxtNumPagos" runat="server" CssClass="LblDesc" Text ="1 - 260" Visible ="false" >
                                        </asp:TextBox>
                                    </td>
                                </tr>
                                 <tr>
                                    <td>
                                        <asp:Label ID="LblCat_Ne_Num_Pagos" runat="server" CssClass="LblDesc" Text="Número De Pagos">
                                        </asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DdlCat_Ne_Num_Pagos" runat="server" AutoPostBack="true" CssClass="DdlDesc">
                                        </asp:DropDownList>
                                    </td>
                                     <td colspan="2">
                                         &nbsp;</tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="LblPagoInicial" runat="server" CssClass="LblDesc" Text="Monto Pago Inicial"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtPagoInicial" runat="server" CssClass="TxtDesc"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="TxtPagoInicial_FilteredTextBoxExtender" runat="server" Enabled="True" TargetControlID="TxtPagoInicial" ValidChars="1234567890.">
                                        </asp:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td>

                                        <asp:Label ID="LblFechaPagoInicial" runat="server" CssClass="LblDesc" Text="Fecha Primer Pago"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtFechaPagoInicial" runat="server" CssClass="TxtDesc"></asp:TextBox>
                                        <asp:CalendarExtender ID="TxtFechaPagoInicial_CalendarExtender" runat="server" CssClass="Calendario" Enabled="True" PopupButtonID="TxtFechaPagoInicial" TargetControlID="TxtFechaPagoInicial">
                                        </asp:CalendarExtender>
                                        <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" AcceptNegative="None" ErrorTooltipEnabled="True" InputDirection="RightToLeft" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" TargetControlID="TxtFechaPagoInicial">
                                        </asp:MaskedEditExtender>
                                    </td>
                                    <td>

                                        <asp:Label ID="LblMontoReal" runat="server" CssClass="LblDesc" Text="Monto Real"></asp:Label>
                                    </td><td>
                                        <asp:TextBox ID="TxtMontoReal" runat="server" CssClass="TxtDesc" Enabled ="false" ></asp:TextBox>
                                         </td>
                                </tr>
                                
                             
                               
                                <%--<tr>
                                    <td>
                                        <asp:Label ID="LblDiasPago" runat="server" CssClass="LblDesc" Text="Dias De Pago"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtDiasPago" runat="server" CssClass="TxtDesc"></asp:TextBox>
                                        <asp:CalendarExtender ID="CETxtDiasPago" runat="server" Enabled="True" TargetControlID="TxtDiasPago" PopupButtonID="TxtDiasPago" CssClass="Calendario">
                                        </asp:CalendarExtender>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="LblMontoPagos" runat="server" CssClass="LblDesc" Text="Monto Mensualidades"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtMontoPagos" runat="server" Width="70px"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="FTBTxtMontoPagos" runat="server" Enabled="True" TargetControlID="TxtMontoPagos" ValidChars="1234567890.">
                                        </asp:FilteredTextBoxExtender>
                                    </td>
                                    <td>
                                        <asp:Label ID="LblMontoReal" runat="server" CssClass="LblDesc" Text="Saldo Real" Visible="False"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtMontoReal" runat="server" Enabled="False" Visible="False" Width="70px"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="TxtMontoReal_FilteredTextBoxExtender" runat="server" Enabled="True" TargetControlID="TxtMontoReal" ValidChars="1234567890.">
                                        </asp:FilteredTextBoxExtender>
                                    </td>
                                </tr>--%>
                             
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>
                                        <asp:Button ID="BtnVisualizar" runat="server" CssClass="Botones" Text="Visualizar Calendario" />
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>
                                        <asp:Button ID="BtnCancelarVis" runat="server" CssClass="Botones" Text="Cancelar" Visible="False" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table class="Table">
                                <tr class="Titulos">
                                    <td colspan="2">Captura De Gestión</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="LblHist_Ge_Accion" runat="server" CssClass="LblDesc" Text="Acción" Visible="False"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DdlHist_Ge_Accion" runat="server" AutoPostBack="true" CssClass="DdlDesc" Visible="False">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="LblHist_Ge_Resultado" runat="server" CssClass="LblDesc" Text="Resultado"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DdlHist_Ge_Resultado" runat="server" AutoPostBack="true" CssClass="DdlDesc">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="LblHist_Ge_NoPago" runat="server" CssClass="LblDesc" Text="No Pago" Visible="False"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DdlHist_Ge_NoPago" runat="server" CssClass="TxtDesNS" Visible="False">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="LblFechaSeguimiento" runat="server" CssClass="LblDesc" Text="Fecha De Seguimiento"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtFechaSeguimiento" runat="server" CssClass="TxtDesc"></asp:TextBox>
                                        <asp:CalendarExtender ID="TxtFechaSeguimiento_CalendarExtender" runat="server" Enabled="True" TargetControlID="TxtFechaSeguimiento" CssClass="Calendario">
                                        </asp:CalendarExtender>
                                        <asp:MaskedEditExtender ID="MaskedEditExtender12" runat="server" AcceptNegative="None" ErrorTooltipEnabled="True" InputDirection="RightToLeft" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" TargetControlID="TxtFechaSeguimiento">
                                         </asp:MaskedEditExtender>
                                        &nbsp;<asp:DropDownList ID="DdlHora" runat="server" CssClass="DdlDesc">
                                            <asp:ListItem Selected="True">07</asp:ListItem>
                                            <asp:ListItem>08</asp:ListItem>
                                            <asp:ListItem>09</asp:ListItem>
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>11</asp:ListItem>
                                            <asp:ListItem>12</asp:ListItem>
                                            <asp:ListItem>13</asp:ListItem>
                                            <asp:ListItem>14</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>16</asp:ListItem>
                                            <asp:ListItem>17</asp:ListItem>
                                            <asp:ListItem>18</asp:ListItem>
                                            <asp:ListItem>19</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>21</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="LblSeleccioneMail" runat="server" CssClass="LblDesc" Text="Selecciona Mail"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DdlSeleccionaMail" runat="server" CssClass="TxtDesNS" >
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:TextBox ID="TxtHist_Ge_Comentario" runat="server" Height="130px" MaxLength="500" TextMode="MultiLine" Width="454px" CssClass="TxtDesc"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="Hist_Vi_ComentarioFTB" runat="server" Enabled="True" TargetControlID="TxtHist_Ge_Comentario" ValidChars="aqzxswcdevfrbgtnhymjukiloñpZAQXSWCDEVFRBGTNHYMJUKILOPÑ1230456789@$/.-_# ">
                                        </asp:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="LblHist_Pr_SupervisorAuto" runat="server" CssClass="LblDesc" Text="Supervisor" Visible="False"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtHist_Pr_SupervisorAuto" runat="server" MaxLength="25" Visible="False"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="LblContrasenaAuto" runat="server" CssClass="LblDesc" Text="Contraseña" Visible="False"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtContrasenaAuto" runat="server" MaxLength="10" OnTextChanged="TxtContrasenaAuto_TextChanged" TextMode="Password" Visible="False" Width="128px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="Arriba" align="center">
                                    <td colspan="2">
                                        <asp:UpdatePanel ID="UpGenerar" runat="server">
                                            <ContentTemplate>
                                                <asp:Button ID="BtnGuardar" runat="server" CssClass="Botones" Text="Guardar Negociacion" Visible="false" OnClick="BtnGuardar_Click1" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr class="Arriba" align="center" >
                                    <td colspan="2">
                                        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpGenerar">
                                            <ProgressTemplate>
                                                <asp:Image ID="Image4" runat="server" ImageUrl="~/Imagenes/Img_Cargando2.gif" Width="200px"/>
                                                <%--<asp:Image ID="Image1" runat="server" ImageUrl="~/Imagenes/737.gif" Width="50px" />--%>
                                                <%--<div class="cssload-clock"></div>--%>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </td>
                                </tr>

                            </table>
                        </td>
                    </tr>
                    <tr class="Arriba">
                        <td>
                            <table class="Table">
                                <tr>
                                    <td>
                                        <asp:Label ID="LblCalendario" runat="server" CssClass="Titulos" Text="Calendario" Width="100%"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Izquierda">
                                        <div class="ScroolCalendarioPagos">
                                            <div class="force-overflow">
                                                <asp:GridView ID="GvCalendario" runat="server" CssClass="mGrid" Font-Names="Tahoma" Font-Size="Small" Width="200PX">
                                                    <AlternatingRowStyle CssClass="alt" />
                                                    <PagerStyle CssClass="pgr" />
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <asp:CollapsiblePanelExtender ID="CollapsiblePanelExtender" runat="server" CollapseControlID="pnlcontrol_Ad" Collapsed="True" CollapsedImage="~/Imagenes/ImgMostrarG.png" CollapsedText="Mostrar" ExpandControlID="pnlcontrol_Ad" ExpandedImage="~/Imagenes/ImgOcultarG.png" ExpandedText="Ocultar" ImageControlID="abajo" SuppressPostBack="true" TargetControlID="PnlAdicionales" TextLabelID="LblAdicionales">
                            </asp:CollapsiblePanelExtender>
                            <asp:Panel ID="pnlcontrol_Ad" runat="server" CssClass="collapsePanelHeader">
                                <asp:Image ID="abajo" runat="server" ImageUrl="~/Imagenes/ImgMostrarG.png" />
                                <asp:Label ID="LblAdicionales" runat="server" CssClass="LblDesc"></asp:Label>
                            </asp:Panel>
                            <asp:Panel ID="PnlAdicionales" runat="server" CssClass="collapsePanel">
                                <table>
                                    <tr>
                                        <td>
                                            <table class="Izquierda">
                                                <tr class="Titulos">
                                                    <td colspan="5">Agregar Teléfonos</td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="LblHist_Te_Tipo" runat="server" CssClass="LblDesc" Text="Tipo"></asp:Label>
                                                    </td>
                                                    <td colspan="3">
                                                        <asp:DropDownList ID="DdlHist_Te_Tipo" runat="server" AutoPostBack="true" CssClass="DdlDesc">
                                                            <asp:ListItem>Casa</asp:ListItem>
                                                            <asp:ListItem>Celular</asp:ListItem>
                                                            <asp:ListItem>Oficina</asp:ListItem>
                                                            <asp:ListItem>Fax</asp:ListItem>
                                                            <asp:ListItem>Otro</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="Arriba" rowspan="6">
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
                                                        <asp:Label ID="LblHist_Te_Lada" runat="server" CssClass="LblDesc" Text="Lada"></asp:Label>
                                                    </td>
                                                    <td colspan="3">
                                                        <asp:TextBox ID="TxtHist_Te_Lada" runat="server" MaxLength="5" Width="40" CssClass="TxtDesc"></asp:TextBox>
                                                        &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="LblHist_Te_Numerotel" runat="server" CssClass="LblDesc" Text="Teléfono"></asp:Label>
                                                        &nbsp;&nbsp;<asp:TextBox ID="TxtHist_Te_Numerotel" runat="server" MaxLength="10" Width="100" CssClass="TxtDesc"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="LblHist_Te_Horario" runat="server" CssClass="LblDesc" Text="Horario"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TxtHist_Te_Horario0" runat="server" MaxLength="5" Width="40" CssClass="TxtDesc"></asp:TextBox>
                                                        <asp:MaskedEditExtender ID="Hist_Te_Horario0MEE" runat="server" AcceptNegative="None" ErrorTooltipEnabled="True" InputDirection="RightToLeft" Mask="99:99" MaskType="Time" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" TargetControlID="TxtHist_Te_Horario0">
                                                        </asp:MaskedEditExtender>
                                                        &nbsp;<asp:Label ID="LblHist_Te_Horario0" runat="server" CssClass="LblDesc" Text="a"></asp:Label>
                                                        &nbsp;<asp:TextBox ID="TxtHist_Te_Horario1" runat="server" MaxLength="5" Width="40" CssClass="TxtDesc"></asp:TextBox>
                                                        <asp:MaskedEditExtender ID="Hist_Te_Horario1MEE" runat="server" AcceptNegative="None" ErrorTooltipEnabled="True" InputDirection="RightToLeft" Mask="99:99" MaskType="Time" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" TargetControlID="TxtHist_Te_Horario1">
                                                        </asp:MaskedEditExtender>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="LblHist_Te_Extension" runat="server" CssClass="LblDesc" Text="Extensión" Visible="false"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TxtHist_Te_Extension" runat="server" MaxLength="10" Visible="false" Width="40" CssClass="TxtDesc"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="LblHist_Te_Parentesco" runat="server" CssClass="LblDesc" Text="Parentesco"></asp:Label>
                                                    </td>
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
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="LblHist_Te_Nombre" runat="server" CssClass="LblDesc" Text="Nombre" Visible="false"></asp:Label>
                                                    </td>
                                                    <td colspan="3">
                                                        <asp:TextBox ID="TxtHist_Te_Nombre" runat="server" MaxLength="50" Visible="false" Width="242px" CssClass="TxtDesc"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                <td>
                                                    <asp:Label ID="LblHist_Te_Proporciona" runat="server" CssClass="LblDesc" Text="Proporciona telefono" Visible="false"></asp:Label></td>
                                                <td colspan="3">
                                                    <asp:TextBox ID="TxtHist_Te_Proporciona" runat="server" MaxLength="50" Visible="false" Width="280"></asp:TextBox></td>
                                            </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="BtnCancelAddTel" runat="server" CssClass="Botones" Text="Cancelar" />
                                                    </td>
                                                    <td colspan="3" style="text-align: right">
                                                        <asp:Button ID="BtnAddTel" runat="server" CssClass="Botones" Text="Agregar" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table class="Table">
                                                <tr class="Titulos">
                                                    <td colspan="3">Agregar Correo</td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="LblHist_Co_Tipo" runat="server" CssClass="LblDesc" Text="Tipo"></asp:Label>
                                                    </td>
                                                    <td colspan="2">
                                                        <asp:DropDownList ID="DdlHist_Co_Tipo" runat="server" AutoPostBack="true" CssClass="DdlDesc">
                                                            <asp:ListItem Selected="True">Adicional</asp:ListItem>
                                                            <asp:ListItem>Trabajo</asp:ListItem>
                                                            <asp:ListItem>Personal</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="LblHist_Co_Correo" runat="server" CssClass="LblDesc" Text="Correo"></asp:Label>
                                                    </td>
                                                    <td colspan="2">
                                                        <asp:TextBox ID="TxtHist_Co_Correo" runat="server" MaxLength="50" Width="200px" CssClass="TxtDesc"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="LblHist_Co_Parentesco" runat="server" CssClass="LblDesc" Text="Parentesco"></asp:Label>
                                                    </td>
                                                    <td colspan="2">
                                                        <asp:DropDownList ID="DdlHist_Co_Parentesco" runat="server" AutoPostBack="true" CssClass="DdlDesc">
                                                            <asp:ListItem Selected="True">Cliente</asp:ListItem>
                                                            <asp:ListItem>Compañero De Trabajo</asp:ListItem>
                                                            <asp:ListItem>Vecino</asp:ListItem>
                                                            <asp:ListItem>Familiar</asp:ListItem>
                                                            <asp:ListItem>Conyuge</asp:ListItem>
                                                            <asp:ListItem>Amigo</asp:ListItem>
                                                            <asp:ListItem>Referencia</asp:ListItem>
                                                            <asp:ListItem>Aval</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="LblHist_Co_Nombre" runat="server" CssClass="LblDesc" Text="Nombre" Visible="False"></asp:Label>
                                                    </td>
                                                    <td colspan="2">
                                                        <asp:TextBox ID="TxtHist_Co_Nombre" runat="server" MaxLength="20" Visible="False" Width="200px" CssClass="TxtDesc"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                <td>
                                                    <asp:Label ID="LblHist_Co_Proporciona" runat="server" CssClass="LblDesc" Text="Proporciona correo" Visible="false"></asp:Label></td>
                                                <td colspan="2">
                                                    <asp:TextBox ID="TxtHist_Co_Proporciona" runat="server" MaxLength="50" Visible="false" Width="280"></asp:TextBox></td>
                                            </tr>
                                                <tr>
                                                    <td>
                                                        <asp:CheckBox ID="CbxHist_Co_Contacto" runat="server" CssClass="CbxDesc" Text="Contacto" />
                                                    </td>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="BtnCancelAddCorreo" runat="server" CssClass="Botones" Text="Cancelar" />
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="BtnAddCorreo" runat="server" CssClass="Botones" Text="Agregar" />
                                                    </td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </td>
    </tr>
</table>
</asp:Panel>
</asp:Panel>
<asp:Panel Id="PnlNegoVigente" runat="server" Visible="false">
    <table>
        <tr class="Arriba">
            <td>
                <table>
                                    <tr class="Titulos">
                                        <td colspan="2">
                                            <asp:Label ID="LblPromesa" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="LblHist_Pr_Supervisor" runat="server" CssClass="LblDesc" Text="Supervisor"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtHist_Pr_Supervisor" runat="server" MaxLength="25"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="LblContrasena" runat="server" CssClass="LblDesc" Text="Contraseña"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtContrasena" runat="server" MaxLength="10" TextMode="Password"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="LblHist_Pr_Motivo" runat="server" CssClass="LblDesc" Text="Motivo"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtHist_Pr_Motivo" runat="server" MaxLength="200" Height="67px" Width="311px" TextMode="MultiLine" CssClass="TxtDesc"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender ID="TxtHist_Pr_Motivo_FilteredTextBoxExtender" runat="server" Enabled="True" TargetControlID="TxtHist_Pr_Motivo" ValidChars="aqzxswcdevfrbgtnhymjukiloñpZAQXSWCDEVFRBGTNHYMJUKILOPÑ1230456789@ ">
                                            </asp:FilteredTextBoxExtender>
                                        </td>
                                    </tr>
                                     
                                    <tr>
                                        <td>
                                            <asp:Button ID="BtnAceptarPromesa" runat="server" CssClass="Botones" Text="Aceptar" />
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                </table>
            </td>
            <td>
                <table>
                    <tr class="Titulos">
                        <td>Calendario De Promesas</td>
                    </tr>
                    <tr>
                        <td>
                <div class="ScroolCalendarioPagos">
                                            <div class="force-overflow">
                                                <asp:GridView ID="GvCalendarioVig" runat="server" CssClass="mGrid" Font-Names="Tahoma" Font-Size="Small" Width="200PX">
                                                    <AlternatingRowStyle CssClass="alt" />
                                                    <PagerStyle CssClass="pgr" />
                                                </asp:GridView>
                                            </div>
                                        </div>
                        </td>
                    </tr>
                </table>

            </td>
        </tr>
    </table>
</asp:Panel>


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
