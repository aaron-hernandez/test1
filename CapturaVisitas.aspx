<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CapturaVisitas.aspx.vb" Inherits="CapturaVisitas" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Mc :: Modulo Captura</title>
    <link href="Estilos/ObjHtmlS.css" rel="stylesheet" />
    <link href="Estilos/ObjHtmlNoS.css" rel="stylesheet" />
    <link href="Estilos/ObjAjax.css" rel="stylesheet" />
    <link href="Estilos/Modal.css" rel="stylesheet" />
    <link href="Estilos/HTML.css" rel="stylesheet" />
    <script src="JQ/jquery.min.js"></script>
    <script src="JQ/jquery.timer.js"></script>
    <script lang="javascript" type="text/javascript">
        var count = 180
        var timer = $.timer(
            function () {
                count--;
                if (count == -8) {
                    window.location.href = "ExpiroSesion.aspx";
                }
                else {

                    $('.count').html(count);
                    if (count <= 30) {
                        $find("MpuSession").show();
                    }
                }
            },
            1000,
            true
        );
        function Mover(event) {
            s = 180;
            count = 180;
            $.ajax({
                type: "POST",
                url: "MasterPage.aspx/KeepActiveSession",
                data: "{'Usuario':'" + document.getElementById('LblCat_Lo_Usuario').textContent + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                success: function (msg) {
                    if (msg.d == "Bye") {
                        window.location.href = "ExpiroSesion.aspx";
                    }
                }
            });

        };
    </script>
</head>
<body onclick="Mover(event);">
    <form id="FrmMasterPage" runat="server">
        <asp:ToolkitScriptManager ID="TKSMMaster" runat="server" CombineScripts="true" EnableScriptGlobalization="true">
        </asp:ToolkitScriptManager>
        <asp:UpdatePanel ID="UpnlGral" runat="server">
            <ContentTemplate>
                <div class="Pagina">
                    <table class="Table">
                        <tr>
                            <td style="width: 40%" class="Izquierda">
                                <asp:Image ID="ImgLogo_Cl" runat="server" ImageUrl="~/Imagenes/ImgLogo_Cl2.jpg" />
                            </td>
                            <td style="width: 20%" class="TituloP" rowspan="2">Módulo De Visitas
                            </td>
                            <td class="Derecha" style="width: 40%">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="Izquierda">
                                <marquee scrollamount="10" direction="left" loop="infinite">
                                <asp:Label ID="LblPrivacidad" runat="server" CssClass="LblSesion" Text="Le recordamos que sus datos están protegidos, para mayor información consulte el aviso de privacidad en www.finem.com"></asp:Label>
                                </marquee>
                            </td>
                            <td class="Derecha">
                                <div>
                                    <marquee width="30%" scrollamount="5" direction="left"
                                        loop="infinite">
                            <asp:Label ID="LblCat_Lo_Usuario1" runat="server" CssClass="LblSesion" Text="Usuario:"></asp:Label>
                            <asp:Label ID="LblCat_Lo_Usuario" runat="server" CssClass="LblSesion1"></asp:Label>
                            <asp:Label ID="LblCat_Lo_Nombre1" runat="server" CssClass="LblSesion" Text="Nombre:"></asp:Label>
                            <asp:Label ID="LblCat_Lo_Nombre" runat="server" CssClass="LblSesion1"></asp:Label>
                            <asp:Label ID="LblCAT_PE_PERFIL1" runat="server" CssClass="LblSesion" Text="Rol:"></asp:Label>
                            <asp:Label ID="LblCAT_PE_PERFIL" runat="server" CssClass="LblSesion1"></asp:Label>
                            <asp:Label ID="LblCat_Lo_Productos1" runat="server" CssClass="LblSesion" Text="Campaña:"></asp:Label>
                            <asp:Label ID="LblCat_Lo_Productos" runat="server" CssClass="LblSesion1"></asp:Label></marquee>
                                    <asp:LinkButton ID="SESION" runat="server" Visible="true" BackColor="White"
                                        CssClass="LblMsj">Cerrar Sesión</asp:LinkButton>
                                </div>
                            </td>
                        </tr>
                    </table>
                    <div class="Espacio">
                    </div>
                    <table class="Table">
                        <tr>
                            <td>
                                <asp:UpdatePanel ID="UpnlBuscar" runat="server">
                                    <ContentTemplate>
                                        <div class="searchdiv">
                                            <asp:Panel ID="PnlBuscar" runat="server" DefaultButton="BtnBuscar">
                                                <asp:TextBox ID="TxtBuscar" runat="server" CssClass="TxtxSearch" MaxLength="100"></asp:TextBox>
                                                <asp:Button ID="BtnBuscar" runat="server" CssClass="SearchButton" Text="Ir" />
                                                <asp:TextBoxWatermarkExtender ID="TxtBuscar_TextBoxWatermarkExtender" runat="server" Enabled="True" TargetControlID="TxtBuscar" WatermarkText="Buscar....">
                                                </asp:TextBoxWatermarkExtender>
                                                <asp:Label ID="lblcuantos" runat="server" CssClass="LblMsj"></asp:Label>
                                            </asp:Panel>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td class="Derecha">
                                <asp:ImageButton ID="ImgAddGestion" runat="server" ImageUrl="~/Imagenes/ImgAddGestion.png" ToolTip="Ir A Módulo De Gestión" Visible="false" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <table class="Table">
                                    <tr>
                                        <td>
                                            <asp:UpdatePanel ID="UpnlRetirada" runat="server" Visible="false">
                                                <ContentTemplate>
                                                    <div class="Retirada">
                                                        Crédito Retirado
                                                    </div>

                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <asp:AnimationExtender ID="AnimationExtender1" runat="server" TargetControlID="UpnlRetirada">
                                                <Animations>
        <OnLoad>
             <Sequence>
                 <FadeOut Duration=".45" Fps="20" MinimumOpacity=".2" MaximumOpacity=".8" />
             </Sequence>
         </OnLoad>
                                                    <OnHoverOver>
                <FadeIn Duration=".25" Fps="20" MinimumOpacity=".2" MaximumOpacity=".8" />
            </OnHoverOver>
            <OnHoverOut>
                <FadeOut Duration=".25" Fps="20" MinimumOpacity=".2" MaximumOpacity=".8" />
            </OnHoverOut>

                                                </Animations>
                                            </asp:AnimationExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="LblPr_Mc_Credito" runat="server" CssClass="LblDesc" Text="Crédito: " Visible="False"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblPr_Mc_CreditoV" runat="server" CssClass="LblDesc" Visible="False"></asp:Label></td>
                                                    <td>

                                                        <asp:Label ID="LblPr_Cd_Nombre" runat="server" CssClass="LblDesc" Text="Nombre: " Visible="False"></asp:Label>
                                                        <asp:Label ID="LblPr_Cd_NombreV" runat="server" CssClass="LblDesc" Visible="False"></asp:Label>

                                                    </td>
                                                    <td>
                                                        <asp:Label ID="LblPr_Mc_Producto" runat="server" CssClass="LblDesc" Text="Producto:" Visible="False"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="LblPr_Mc_ProductoV" runat="server" CssClass="LblDesc" Visible="False"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="LblPr_Mc_Expediente" runat="server" CssClass="LblDesc" Text="Folio:" Visible="False"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="LblPr_Mc_ExpedienteV" runat="server" CssClass="LblDesc" Visible="False"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr align="center">
                            <td colspan="2">
                                <asp:UpdatePanel runat="server" ID="UpnlVisita" Visible="false">
                                    <ContentTemplate>
                                        <table class="Table">
                                            <tr class="Titulos">
                                                <td>Captura De Visitas</td>
                                            </tr>
                                        </table>
                                        <table class="Izquierda">
                                            <tr>
                                                <td colspan="2" class="Izquierda">


                                                    <asp:CollapsiblePanelExtender ID="CollapsiblePanelExtender" runat="server" CollapseControlID="pnlcontrol_ges"
                                                        Collapsed="True" CollapsedImage="~/Imagenes/ImgMostrarG.png" CollapsedText="Más"
                                                        ExpandControlID="pnlcontrol_ges" ExpandedImage="~/Imagenes/ImgOcultarG.png" ExpandedText="Menos"
                                                        ImageControlID="abajo" SuppressPostBack="true" TargetControlID="Pnlgestion" TextLabelID="Label1">
                                                    </asp:CollapsiblePanelExtender>
                                                    <asp:Panel ID="pnlcontrol_ges" runat="server" CssClass="collapsePanelHeader">
                                                        <asp:Image ID="abajo" runat="server" ImageUrl="~/Imagenes/ImgMostrarG.png" />
                                                        <asp:Label ID="LblGestion" runat="server" CssClass="LblDesc"></asp:Label>
                                                    </asp:Panel>
                                                    <asp:Panel ID="Pnlgestion" runat="server" CssClass="collapsePanel">
                                                        <table>
                                                            <tr class="Titulos">
                                                                <td>Dirección Actual</td>
                                                                <td>Teléfonos Adicionales</td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="LblPR_CD_CALLEYNUM" runat="server" CssClass="LblDesc" Text="Calle Y Número "></asp:Label>
                                                                            </td>
                                                                            <td colspan="3">
                                                                                <asp:TextBox ID="TxtPR_CD_CALLEYNUM" runat="server" CssClass="TxtDesNS" MaxLength="80" ReadOnly="True" Width="370px"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="LblPR_CD_COLONIA" runat="server" CssClass="LblDesc" Text="Colonia"></asp:Label>
                                                                            </td>
                                                                            <td colspan="3">
                                                                                <asp:TextBox ID="TxtPR_CD_CIUDAD" runat="server" CssClass="TxtDesNS" MaxLength="40" ReadOnly="True" Width="370px"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="LblPR_CD_CIUDAD" runat="server" CssClass="LblDesc" Text="Ciudad"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="TxtPR_CD_COLONIA" runat="server" CssClass="TxtDesNS" MaxLength="80" ReadOnly="True" Width="140px"></asp:TextBox>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="LblPR_CD_CP" runat="server" CssClass="LblDesc" Text="Código Postal"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="TxtPR_CD_CP" runat="server" CssClass="TxtDesNS" MaxLength="22" ReadOnly="True"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="LblPR_CD_ESTADO" runat="server" CssClass="LblDesc" Text="Estado "></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="TxtPR_CD_ESTADO" runat="server" CssClass="TxtDesNS" MaxLength="4" ReadOnly="True" Width="140px"></asp:TextBox>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="LblPR_CD_RFC" runat="server" CssClass="LblDesc" Text="Rfc"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="TxtPR_CD_RFC" runat="server" CssClass="TxtDesNS" MaxLength="13" ReadOnly="True"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="LblPR_CD_EMAIL" runat="server" CssClass="LblDesc" Text="E-Mail"></asp:Label>
                                                                            </td>
                                                                            <td colspan="3">
                                                                                <asp:TextBox ID="TxtPR_CD_EMAIL" runat="server" CssClass="TxtDesNS" MaxLength="60" ReadOnly="True" Width="370px"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                <td>
                                                    <asp:Label ID="LblHist_Co_Proporciona" runat="server" CssClass="LblDesc" Text="Proporciona información" Visible="false"></asp:Label></td>
                                                <td colspan="3">
                                                    <asp:TextBox ID="TxtHist_Co_Proporciona" runat="server" MaxLength="50" Visible="false" Width="280"></asp:TextBox></td>
                                            </tr>
                                                                    </table>
                                                                </td>
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
                                                                            <td>&nbsp;</td>
                                                                            <td colspan="3">
                                                                                <asp:Label ID="LblHist_Te_Consecutivo" runat="server" CssClass="LblDesc" Visible="false"></asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Button ID="BtnCancelAddTel" runat="server" CssClass="Botones" Text="Cancelar" /></td>
                                                                            <td colspan="3" style="text-align: right">
                                                                                <asp:Button ID="BtnAddTel" runat="server" CssClass="Botones" Text="Agregar" /></td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr class="Titulos">
                                                <td colspan="2">Gestión</td>
                                            </tr>
                                            <tr class="Arriba">
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="LblHIST_VI_FOLIO" runat="server" CssClass="LblDesc" Text="Folio"></asp:Label>
                                                            </td>
                                                            <td colspan="3">
                                                             <asp:TextBox ID="TxtHIST_VI_FOLIO" runat="server" MaxLength="49"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="LblHist_Vi_Visitador" runat="server" CssClass="LblDesc" Text="Visitador"></asp:Label>
                                                            </td>
                                                            <td colspan="3">
                                                                <asp:DropDownList ID="DdlHist_Vi_Visitador" runat="server" CssClass="DdlDesc">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="LblHist_Ge_Accion" runat="server" CssClass="LblDesc" Text="Acción" Visible="False"></asp:Label>
                                                            </td>
                                                            <td colspan="3">
                                                                <asp:DropDownList ID="DdlHist_Ge_Accion" runat="server" AutoPostBack="true" CssClass="DdlDesc" Visible="False">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="LblHist_Ge_Resultado" runat="server" CssClass="LblDesc" Text="Resultado"></asp:Label>
                                                            </td>
                                                            <td colspan="3">
                                                                <asp:DropDownList ID="DdlHist_Ge_Resultado" runat="server" AutoPostBack="true" CssClass="DdlDesc">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="LblHist_Ge_NoPago" runat="server" CssClass="LblDesc" Text="No Pago" Visible="False"></asp:Label>
                                                            </td>
                                                            <td colspan="3">
                                                                <asp:DropDownList ID="DdlHist_Ge_NoPago" runat="server" CssClass="TxtDesNS" Visible="False">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="LblHist_Pr_Dtepromesa" runat="server" CssClass="LblDesc" Text="Fecha" Visible="false"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="TxtHist_Pr_Dtepromesa" runat="server" MaxLength="10" Visible="false"></asp:TextBox>
                                                                <asp:CalendarExtender ID="TxtHist_Pr_Dtepromesa_CalendarExtender" runat="server" Enabled="True" PopupButtonID="TxtHist_Pr_Dtepromesa" TargetControlID="TxtHist_Pr_Dtepromesa">
                                                                </asp:CalendarExtender>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="LblHist_Pr_Montopp" runat="server" CssClass="LblDesc" Text="Monto" Visible="false"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="TxtHist_Pr_Montopp" runat="server" Visible="false" MaxLength="10"></asp:TextBox>
                                                                <asp:FilteredTextBoxExtender ID="TxtHist_Pr_Montopp_FilteredTextBoxExtender" runat="server" Enabled="True" TargetControlID="TxtHist_Pr_Montopp" ValidChars="1234567890.">
                                                                </asp:FilteredTextBoxExtender>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="LblHist_Vi_Dtevisita" runat="server" CssClass="LblDesc" Text="Fecha Y hora De Visita"></asp:Label>
                                                            </td>
                                                            <td colspan="3">
                                                                <asp:TextBox ID="TxtHist_Vi_Dtevisita" runat="server"></asp:TextBox>
                                                                <asp:CalendarExtender ID="TxtHist_Vi_Dtevisita_CalendarExtender" runat="server" Enabled="True" TargetControlID="TxtHist_Vi_Dtevisita" PopupButtonID="TxtHist_Vi_Dtevisita">
                                                                </asp:CalendarExtender>
                                                                <asp:TextBox ID="TxtHist_Vi_Dtevisita2" runat="server" Width="50px"></asp:TextBox>
                                                                <asp:MaskedEditExtender ID="TxtHist_Vi_Dtevisita2MEE" runat="server" AcceptNegative="None"
                                                                    ErrorTooltipEnabled="True" InputDirection="RightToLeft" Mask="99:99" MaskType="Time"
                                                                    MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError"
                                                                    TargetControlID="TxtHist_Vi_Dtevisita2">
                                                                </asp:MaskedEditExtender>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="LblHist_Vi_Parentesco" runat="server" CssClass="LblDesc" Text="Parentesco"></asp:Label>
                                                            </td>
                                                            <td colspan="3">
                                                                <asp:DropDownList ID="DdlHist_Vi_Parentesco" runat="server" AutoPostBack="true" CssClass="DdlDesc">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="LblHist_Vi_Nombrec" runat="server" CssClass="LblDesc" Text="Nombre" Visible="false"></asp:Label>
                                                            </td>
                                                            <td colspan="3">
                                                                <asp:TextBox ID="TxtHist_Vi_Nombrec" runat="server" MaxLength="50" Visible="false" Width="250px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="LblHist_Vi_Comentario" runat="server" CssClass="LblDesc" Text="Comentario"></asp:Label>
                                                            </td>
                                                            <td colspan="3">
                                                                <asp:TextBox ID="TxtHist_Vi_Comentario" runat="server" MaxLength="500" Width="330px" Height="90px" TextMode="MultiLine"></asp:TextBox>
                                                                <asp:FilteredTextBoxExtender ID="Hist_Vi_ComentarioFTB" runat="server" Enabled="True" TargetControlID="TxtHist_Vi_Comentario" ValidChars="aqzxswcdevfrbgtnhymjukiloñpZAQXSWCDEVFRBGTNHYMJUKILOPÑ1230456789@ ">
                                                                </asp:FilteredTextBoxExtender>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td>
                                                    <table>
                                                        <tr class="Arriba">
                                                            <td>
                                                                <asp:Label ID="LblHist_Vi_Dcontacto" runat="server" CssClass="LblDesc" Text="Dias"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:GridView ID="GvDiasVisita" runat="server" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="True" CssClass="mGrid" Font-Names="Tahoma" Font-Size="small" PagerStyle-CssClass="pgr">
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
                                                        <tr class="Arriba">
                                                            <td>
                                                                <asp:Label ID="LblHist_Vi_Hcontacto" runat="server" CssClass="LblDesc" Text="Horario De Contacto"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="TxtHist_Vi_Hcontacto" runat="server" MaxLength="5" Width="35px"></asp:TextBox>
                                                                <asp:MaskedEditExtender ID="TxtHist_Vi_HcontactoMEE" runat="server" AcceptNegative="None"
                                                                    ErrorTooltipEnabled="True" InputDirection="RightToLeft" Mask="99:99" MaskType="Time"
                                                                    MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError"
                                                                    TargetControlID="TxtHist_Vi_Hcontacto">
                                                                </asp:MaskedEditExtender>
                                                                &nbsp;<asp:Label ID="LblHist_Vi_Hcontacto0" runat="server" CssClass="LblDesc" Text="a"></asp:Label>
                                                                &nbsp;<asp:TextBox ID="TxtHist_Vi_Hcontacto0" runat="server" MaxLength="5" Width="35px"></asp:TextBox>
                                                                <asp:MaskedEditExtender ID="TxtHist_Vi_Hcontacto0MEE" runat="server" AcceptNegative="None"
                                                                    ErrorTooltipEnabled="True" InputDirection="RightToLeft" Mask="99:99" MaskType="Time"
                                                                    MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError"
                                                                    TargetControlID="TxtHist_Vi_Hcontacto0">
                                                                </asp:MaskedEditExtender>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="Titulos">
                                                <td colspan="2">Datos Visita</td>
                                            </tr>
                                            <tr class="Arriba">
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="LblHist_Vi_Referencia" runat="server" CssClass="LblDesc" Text="Punto De Referencia"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="TxtHist_Vi_Referencia" runat="server" Height="70px" TextMode="MultiLine" Width="250px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="LblHist_Vi_Entrecalle1" runat="server" CssClass="LblDesc" Text="Entre Calle"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="TxtHist_Vi_Entrecalle1" runat="server" Width="250px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="LblHist_Vi_Entrecalle2" runat="server" CssClass="LblDesc" Text="Entre Calle"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="TxtHist_Vi_Entrecalle2" runat="server" Width="250px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="LblHist_Vi_Colorf" runat="server" CssClass="LblDesc" Text="Color De La Fachada"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="TxtHist_Vi_Colorf" runat="server" Enabled="false" Width="50px" Font-Size="0px" Height="17px" BackColor="White" Text="FFFFFF"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:ImageButton ID="ImgColorF" runat="server" ImageUrl="~/Imagenes/ImgColor.png" />
                                                                <asp:ColorPickerExtender ID="CPImgColorF" runat="server" Enabled="True" PopupButtonID="ImgColorF" SampleControlID="TxtHist_Vi_Colorf" TargetControlID="TxtHist_Vi_Colorf">
                                                                </asp:ColorPickerExtender>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="LblHist_Vi_Colorp" runat="server" CssClass="LblDesc" Text="Color De La Puerta"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="TxtHist_Vi_Colorp" runat="server" Enabled="false" Width="50px" Font-Size="0px" Height="17px" BackColor="White" Text="FFFFFF"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:ImageButton ID="ImgColorP" runat="server" ImageUrl="~/Imagenes/ImgColor.png" />
                                                                <asp:ColorPickerExtender ID="CpImgColorP" runat="server" Enabled="True" PopupButtonID="ImgColorP" SampleControlID="TxtHist_Vi_Colorp" TargetControlID="TxtHist_Vi_Colorp">
                                                                </asp:ColorPickerExtender>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="LblHist_Vi_Tipodomicilio" runat="server" CssClass="LblDesc" Text="Tipo"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="DdlHist_Vi_Tipodomicilio" runat="server" CssClass="DdlDesc">
                                                                    <asp:ListItem> </asp:ListItem>
                                                                    <asp:ListItem>Depto.</asp:ListItem>
                                                                    <asp:ListItem>Casa</asp:ListItem>
                                                                    <asp:ListItem>Otro</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>&nbsp;</td>
                                                            <td>
                                                                <asp:Label ID="LblHist_Vi_Caracteristicas" runat="server" CssClass="LblDesc" Text="Caracteristicas"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="DdlHist_Vi_Caracteristicas" runat="server" CssClass="DdlDesc">
                                                                    <asp:ListItem> </asp:ListItem>
                                                                    <asp:ListItem>Propia</asp:ListItem>
                                                                    <asp:ListItem>Rentada</asp:ListItem>
                                                                    <asp:ListItem>Otro</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="LblHist_Vi_Nivelsocio" runat="server" CssClass="LblDesc" Text="Nivel Socioeconómico"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="DdlHist_Vi_Nivelsocio" runat="server" CssClass="DdlDesc">
                                                                    <asp:ListItem> </asp:ListItem>
                                                                    <asp:ListItem>Alto</asp:ListItem>
                                                                    <asp:ListItem>Medio</asp:ListItem>
                                                                    <asp:ListItem>Bajo</asp:ListItem>
                                                                    <asp:ListItem>Otro</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>&nbsp;</td>
                                                            <td>
                                                                <asp:Label ID="LblHist_Vi_Niveles" runat="server" CssClass="LblDesc" Text="Niveles"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="TxtHist_Vi_Niveles" runat="server"  Width="50px" MaxLength="2"></asp:TextBox>
                                                                
                                                            </td>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr class="Centro">
                                                <td colspan="2">

                                                    <asp:Button ID="BtnGuardar" runat="server" Text="Guardar" CssClass="Botones" Visible="false" />

                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </div>
                <asp:Button ID="BtnModalMsj" runat="server" Style="visibility: hidden" />
                <asp:Panel ID="PnlModalMsj" runat="server" CssClass="ModalMsj"  Style="display: none">
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
                <asp:Button ID="BtnModalSession" runat="server" Style="visibility: hidden" />
                <asp:Panel ID="PnlModalSession" runat="server" CssClass="ModalMsj" Style="display: none">
                    <div class="heading">
                        Su Sesión Caducara en     <font id="TimeField" size="3"><span class="count">0</span></font>
                    </div>
                    <div class="CuerpoMsj">
                        <table class="Table">
                            <tr align="center">
                                <td>&nbsp;</td>
                            </tr>
                            <tr align="center">
                                <td>
                                    <asp:Label ID="LblSession" runat="server" CssClass="LblDesc" Text="¿Continuar En El Sistema?"></asp:Label>
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
                                    <asp:Button ID="BtnCancelarSession" runat="server" CssClass="button green close" Text="Aceptar" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>
                <asp:ModalPopupExtender ID="MpuSession" runat="server" PopupControlID="PnlModalSession" TargetControlID="BtnModalSession"
                    CancelControlID="BtnCancelarSession" BackgroundCssClass="modalBackground">
                </asp:ModalPopupExtender>
                <asp:Button ID="BtnModalAcciones" runat="server" Style="visibility: hidden" />
                <asp:Panel ID="PnlModalAcciones" runat="server" CssClass="ModalAcciones" Style="display: none">
                    <table class="Table">
                        <tr class="heading">
                            <td>
                                <asp:Label ID="LblTitulo" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr class="CuerpoAcciones">
                            <td>
                                <div class="ScroolBusquedas">
                                    <asp:GridView ID="GvBusquedas" runat="server" AlternatingRowStyle-CssClass="alt" CssClass="mGrid" Font-Names="Tahoma" Font-Size="small" PagerStyle-CssClass="pgr" Style="font-size: small" AutoGenerateColumns="true">
                                        <AlternatingRowStyle CssClass="alt" />
                                        <Columns>
                                            <asp:CommandField ButtonType="Image" HeaderText="" SelectImageUrl="~/Imagenes/ImgSeleccion.png" ShowSelectButton="True" />
                                        </Columns>
                                        <PagerStyle CssClass="pgr" />
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="BtnCancelarAcciones" runat="server" CssClass="button red close" Text="Cancelar" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:ModalPopupExtender ID="MpuAcciones" runat="server" PopupControlID="PnlModalAcciones" TargetControlID="BtnModalAcciones"
                    CancelControlID="BtnCancelarAcciones" BackgroundCssClass="modalBackground">
                </asp:ModalPopupExtender>
                <asp:Button ID="BtnModalPromesa" runat="server" Style="visibility: hidden" />
                <asp:Panel ID="PnlModalPromesa" runat="server" CssClass="ModalAcciones"  Style="display: none">
                    <table>
                        <tr class="heading">
                            <td>
                                <asp:Label ID="LblPromesa" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr class="CuerpoAcciones">
                            <td>
                                <table>
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
                                            <asp:TextBox ID="TxtHist_Pr_Motivo" runat="server" MaxLength="200" Height="67px" Width="311px" TextMode="MultiLine"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender ID="TxtHist_Pr_Motivo_FilteredTextBoxExtender" runat="server" Enabled="True" TargetControlID="TxtHist_Pr_Motivo" ValidChars="aqzxswcdevfrbgtnhymjukiloñpZAQXSWCDEVFRBGTNHYMJUKILOPÑ1230456789@ ">
                                            </asp:FilteredTextBoxExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Label ID="LblMsjPromesa" runat="server" CssClass="LblMsj"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="BtnAceptarPromesa" runat="server" CssClass="button green close" Text="Aceptar" />
                                        </td>
                                        <td>
                                            <asp:Button ID="BtnCancelarPromesa" runat="server" CssClass="button red close" Text="Cancelar" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:ModalPopupExtender ID="MpuPromesa" runat="server" PopupControlID="PnlModalPromesa" TargetControlID="BtnModalPromesa"
                    CancelControlID="BtnCancelarPromesa" BackgroundCssClass="modalBackground">
                </asp:ModalPopupExtender>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
