<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MasterPage.aspx.vb" Inherits="MGestion_MasterPage" UICulture="es" Culture="es-MX" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="InformacionGeneral.ascx"          TagName="InformacionGeneral"        TagPrefix="MPCU1"  %>
<%@ Register Src="InformacionFinanciera.ascx"       TagName="InformacionFinanciera"     TagPrefix="MPCU2"  %>
<%@ Register Src="Historicos_De_Actividades.ascx"   TagName="Historico_De_Actividades"  TagPrefix="MPCU3"  %>
<%@ Register Src="Relacionados.ascx"                TagName="Relacionados"              TagPrefix="MPCU4"  %>
<%@ Register Src="Hist_Pagos.ascx"                  TagName="Hist_Pagos"                TagPrefix="MPCU5"  %>
<%@ Register Src="Judicial.ascx"                    TagName="Judicial"                  TagPrefix="MPCU6"  %>
<%@ Register Src="InformacionAdicional.ascx"        TagName="Adicional"                 TagPrefix="MPCU7"  %>
<%@ Register Src="Negociaciones.ascx"               TagName="GNegociaciones"            TagPrefix="MPCU8"  %>
<%@ Register Src="Documentos.ascx"                  TagName="Documentos"                TagPrefix="MPCU10" %>
<%@ Register Src="InformacionReferencias.ascx"      TagName="Referencias"               TagPrefix="MPCU11" %>
<%@ Register Src="EnvioCorreos.ascx"                TagName="EnvioCorreos"              TagPrefix="MPCU12" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Mc :: Modulo Gestión</title>
    <link href="Imagenes/IcoLogo_Mc.ico" rel="Shortcut icon" />
    <meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1" />
    <link href="Estilos/ObjHtmlS.css" rel="stylesheet" />
    <link href="Estilos/ObjHtmlNoS.css" rel="stylesheet" />
    <link href="Estilos/ObjAjax.css" rel="stylesheet" />
    <link href="Estilos/Modal.css" rel="stylesheet" />
    <link href="Estilos/HTML.css" rel="stylesheet" />
    <link href="Estilos/MenuLateral.css" rel="stylesheet" />
    <script src="FlashVersion.js"></script>
    <script src="JQ/jquery.min.js"></script>
    <script src="JQ/jquery.timer.js"></script>
    <script lang="javascript" type="text/javascript">
        Especial();
        var count = 360
        var Clicks = 0
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
                        TimeField.innerHTML = "Su Sesión Caducara en " + count
                        if (count <= 0) {
                            TimeField.innerHTML = "Su Sesión Ha Caducado, Redireccionado "
                        }
                    }
                }
            },
            1000,
            true
        );
        function Mover(event) {
            if (Clicks == 0) {
                count = 360;
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
                Especial(document.getElementById('LblAgenda').textContent)
                Clicks = 0;
            }
            else {
                count = 360;
                Clicks++
            }
        };
        function Especial(V_Bandera) {
            var $list = $('#rp_list ul');
            var elems_cnt = $list.children().length;
            var $elem0 = $list.find('li:nth-child(' + (1) + ')');
            var $elem1 = $list.find('li:nth-child(' + (2) + ')');
            var $elem2 = $list.find('li:nth-child(' + (3) + ')');
            $elem0.hide();
            $elem1.hide();
            $elem2.show();
            if (V_Bandera == '11') {
                $elem0.show();
                $elem1.show();
            } else if (V_Bandera == '01') {
                $elem1.show();
            }
            else if (V_Bandera == '10') {
                $elem0.show();
            }
            var d = 200;
            $list.find('li:visible div').each(function () {
                $(this).stop().animate({
                    'marginLeft': '-50px'
                }, d += 100);
            });
            $list.find('li:visible').live('mouseenter', function () {
                $(this).find('div').stop().animate({
                    'marginLeft': '-220px'
                }, 200);
            }).live('mouseleave', function () {
                $(this).find('div').stop().animate({
                    'marginLeft': '-50px'
                }, 200);
            });
        }
    </script>
    <script type="text/javascript">
        function onCallRecieved(callData) {
            //alert(callData.cal_key);
            if (callData.cal_key != "") {
                document.getElementById('TextoBusca').value = callData.cal_key
                var boton = document.getElementById('buscarNuevo');
                boton.click();
            }
            var inicio = window.parent.frames[0].document.getElementById('TextoBusca');
        }
    </script>
    <script type="text/javascript">
        function disableBackButton() {
            window.history.forward();
        }
        setTimeout("disableBackButton()", 0);
    </script>
</head>
<body onclick="Mover(event);" onload="disableBackButton()">
    
    <form id="FrmMasterPage" runat="server">
        <asp:HiddenField ID="HiddenUrs" runat="server" />
        <asp:HiddenField ID="HiddenInicioFila" runat="server" />
        <asp:ToolkitScriptManager ID="TKSMMaster" runat="server" CombineScripts="true" EnableScriptGlobalization="true">
        </asp:ToolkitScriptManager>
        <asp:Label ID="Label1" runat="server" CssClass="LblDesc"></asp:Label>
        <asp:Panel runat="server" DefaultButton="">
            <asp:UpdatePanel ID="UpnlGral" runat="server">
                <ContentTemplate>
                    <asp:UpdatePanel runat="server" ID="UpnlAgenda" EnableViewState="false">
                        <ContentTemplate>
                            <div id="rp_list" class="rp_list">
                                <ul>

                                    <li>
                                        <div>
                                            <table style="width: 240px; height: 70px;">
                                                <tr>
                                                    <td rowspan="2">
                                                        <img src="Imagenes/ImgAgenda.png" width="43" height="42" /></td>
                                                    <td style="color: white; text-align: center">Tiene Llamadas Pendientes
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td style="color: white; text-align: center">
                                                        <asp:Button ID="BtnAgenda" runat="server" CssClass="Botones" Text="Ir" />
                                                    </td>
                                                </tr>

                                            </table>
                                        </div>
                                    </li>
                                    <li>
                                        <div>
                                            <table style="width: 100%; height: 70px;">
                                                <tr>
                                                    <td rowspan="2">
                                                        <asp:Image runat="server" ImageUrl="Imagenes/ImgAvisos.png" ID="Image1" /></td>
                                                    <td style="color: white; text-align: center">Avisos
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td style="color: white; text-align: center">
                                                        <asp:Button ID="BtnAvisos" runat="server" CssClass="Botones" Text="Ir" />
                                                    </td>
                                                </tr>

                                            </table>
                                        </div>
                                    </li>
                                    <li>
                                        <div>
                                            <table style="width: 100%; height: 70px;">
                                                <tr>
                                                    <td rowspan="2">
                                                        <asp:Image runat="server" ImageUrl="Imagenes/ImgMantenimiento.png" ID="ImgMantenimiento" /></td>
                                                    <td style="color: white; text-align: center">Mantenimiento
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td style="color: white; text-align: center">
                                                        <asp:Button ID="BtnMantenimiento" runat="server" CssClass="Botones" Text="Ir" />
                                                    </td>
                                                </tr>

                                            </table>
                                        </div>
                                    </li>
                                </ul>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="Pagina">
                        <table class="Table">
                            <tr>
                                <td style="width: 30%">
                                    <asp:Image ID="ImgLogo_Cl" runat="server" ImageUrl="~/Imagenes/ImgLogo_Cl2.jpg" /><asp:Label ID="LblAgenda" runat="server" Text="00" Font-Size="0px"></asp:Label>
                                </td>
                                <td class="TituloP" rowspan="2" style="width: 40%">Módulo De Gestión
                                </td>
                                <td style="width: 30%" class="Derecha"></td>
                            </tr>
                            <tr>
                                <td>
                                    <marquee scrollamount="10" direction="left" loop="infinite" style="width: 99%">
                                <asp:Label ID="LblPrivacidad" runat="server" CssClass="LblSesion" Text="Le recordamos que sus datos están protegidos, para mayor información consulte el aviso de privacidad en www.finem.com.mx"></asp:Label>
                                </marquee>
                                </td>
                                <td class="Derecha">
                                    <div>
                                        <marquee width="80%" scrollamount="5" direction="left"
                                            loop="infinite" style="width: 66%">
                            <asp:Label ID="LblCat_Lo_Usuario1"  runat="server" CssClass="LblSesion" Text="Usuario:"></asp:Label>
                            <asp:Label ID="LblCat_Lo_Usuario"   runat="server" CssClass="LblSesion1"></asp:Label>
                            <asp:Label ID="LblCat_Lo_Nombre1"   runat="server" CssClass="LblSesion" Text="Nombre:"></asp:Label>
                            <asp:Label ID="LblCat_Lo_Nombre"    runat="server" CssClass="LblSesion1"></asp:Label>
                            <asp:Label ID="LblCAT_PE_PERFIL1"   runat="server" CssClass="LblSesion" Text="Rol:"></asp:Label>
                            <asp:Label ID="LblCAT_PE_PERFIL"    runat="server" CssClass="LblSesion1"></asp:Label>
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
                                    <asp:UpdatePanel runat="server" ID="UpnlBuscar">
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
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblGESTIONESL" runat="server" CssClass="LblDesc" Text="# De Gestiones:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="LblGESTIONES" runat="server" CssClass="LblDesc"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="LblCUANTASPPL" runat="server" CssClass="LblDesc" Text="# De Promesas:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="LblCUANTASPP" runat="server" CssClass="LblDesc"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="LblMONTOPPL" runat="server" CssClass="LblDesc" Text="Monto:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="LblMONTOPP" runat="server" CssClass="LblDesc"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <asp:ImageButton ID="ImgNext" runat="server" ImageUrl="~/Imagenes/ImgNext.png" Visible="false" />
                                    <asp:Label ID ="lblNomFila" runat="server" ForeColor="DarkRed" Visible="false"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <table class="Table">
                                        <tr>
                                            <td>
                                                <asp:UpdatePanel ID="UpnlRetirada" runat="server" Visible="false">
                                                    <ContentTemplate>
                                                        <div class="Retirada">
                                                            <asp:Label ID="LblRetiro" runat="server">Crédito Retirado</asp:Label>
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
                                                <asp:UpdatePanel ID="UpnlGestion" runat="server" Visible="false">
                                                    <ContentTemplate>
                                                        <asp:CollapsiblePanelExtender ID="CSPEGestion" runat="server" CollapseControlID="pnlcontrol_ges"
                                                            Collapsed="True" CollapsedImage="~/Imagenes/ImgMostrarG.png" CollapsedText="Mostrar Gestión"
                                                            ExpandControlID="pnlcontrol_ges" ExpandedImage="~/Imagenes/ImgOcultarG.png" ExpandedText="Ocultar Gestión"
                                                            ImageControlID="abajo" SuppressPostBack="true" TargetControlID="Pnlgestion" TextLabelID="LblGestion">
                                                        </asp:CollapsiblePanelExtender>
                                                        <asp:Panel ID="pnlcontrol_ges" runat="server" CssClass="collapsePanelHeader">
                                                            <asp:Image ID="abajo" runat="server" ImageUrl="~/Imagenes/ImgMostrarG.png" />
                                                            <asp:Label ID="LblGestion" runat="server" CssClass="LblDesc"></asp:Label>
                                                        </asp:Panel>
                                                        <asp:Panel ID="Pnlgestion" runat="server" CssClass="collapsePanel">
                                                            <table class="Table">
                                                                <tr class="Titulos">
                                                                    <td>Captura De Gestión</td>
                                                                    <td>Calificación Teléfonica</td>
                                                                </tr>
                                                                <tr valign="top">
                                                                    <td style="width: 60%">
                                                                        <table class="Table">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="LblHist_Ge_Accion" runat="server" CssClass="LblDesc" Text="Acción" Visible="False"></asp:Label>
                                                                                </td>
                                                                                <td colspan="4">
                                                                                    <asp:DropDownList ID="DdlHist_Ge_Accion" runat="server" AutoPostBack="true" CssClass="DdlDesc" Visible="False">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                                <td class="Arriba" rowspan="8">
                                                                                    <asp:TextBox ID="TxtHist_Ge_Comentario" runat="server" Height="155px" MaxLength="500" TextMode="MultiLine" Width="311px" CssClass="TxtDesc"></asp:TextBox>
                                                                                    <asp:FilteredTextBoxExtender ID="Hist_Vi_ComentarioFTB" runat="server" Enabled="True" TargetControlID="TxtHist_Ge_Comentario" ValidChars="aqzxswcdevfrbgtnhymjukiloñpZAQXSWCDEVFRBGTNHYMJUKILOPÑ1230456789áÁéÉíÍóÓúÚ@$./-_#;:,+()[]{}*!¡¿?&%'°<>= ">
                                                                                    </asp:FilteredTextBoxExtender>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="LblHist_Ge_Resultado" runat="server" CssClass="LblDesc" Text="Resultado"></asp:Label>
                                                                                </td>
                                                                                <td colspan="4">
                                                                                    <asp:DropDownList ID="DdlHist_Ge_Resultado" runat="server" CssClass="DdlDesc" AutoPostBack="true">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="LblHist_Ge_NoPago" runat="server" CssClass="LblDesc" Text="No Pago" Visible="False"></asp:Label>
                                                                                </td>
                                                                                <td colspan="4">
                                                                                    <asp:DropDownList ID="DdlHist_Ge_NoPago" runat="server" CssClass="TxtDesNS" Visible="False">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="LblHist_Pr_Dtepromesa" runat="server" CssClass="LblDesc" Text="Proximo Contacto"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="TxtHist_Pr_Dtepromesa" runat="server" MaxLength="10"></asp:TextBox>
                                                                                    <asp:CalendarExtender ID="TxtHist_Pr_Dtepromesa_CalendarExtender" runat="server" Enabled="True" PopupButtonID="TxtHist_Pr_Dtepromesa" TargetControlID="TxtHist_Pr_Dtepromesa">
                                                                                    </asp:CalendarExtender>
                                                                                </td>
                                                                                <td colspan="2">
                                                                                    <asp:Label ID="LblHist_Pr_Montopp" runat="server" CssClass="LblDesc" Text="Monto" Visible="false"></asp:Label>
                                                                                    <asp:Label ID="LblHora" runat="server" CssClass="LblDesc" Text="Hora"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="TxtHist_Pr_Montopp" runat="server" Visible="false" MaxLength="10"></asp:TextBox>
                                                                                    <asp:FilteredTextBoxExtender ID="TxtHist_Pr_Montopp_FilteredTextBoxExtender" runat="server" Enabled="True" TargetControlID="TxtHist_Pr_Montopp" ValidChars="1234567890.">
                                                                                    </asp:FilteredTextBoxExtender>
                                                                                    <asp:DropDownList ID="DdlHora" runat="server" CssClass="DdlDesc">
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
                                                                                    <asp:Label ID="LblUsr" runat="server" CssClass="LblDesc" Text="Usuario" Visible="False"></asp:Label>
                                                                                </td>
                                                                                <td colspan="2">
                                                                                    <asp:TextBox ID="TXTusr" runat="server" MaxLength="10" Visible="False"></asp:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="LblPwd" runat="server" CssClass="LblDesc" Text="Contraseña" Visible="False"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="TXTpwd" runat="server" MaxLength="25" Visible="False" TextMode="Password"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="LblHist_Ge_Telefono" runat="server" CssClass="LblDesc" Text="Teléfono De Contacto"></asp:Label>
                                                                                </td>
                                                                                <td colspan="2">
                                                                                    <asp:TextBox ID="TxtHist_Ge_Telefono" runat="server" ReadOnly="true"></asp:TextBox>
                                                                                </td>
                                                                                <td colspan="2">&nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>&nbsp;</td>
                                                                                <td colspan="4">
                                                                                    <asp:CheckBox ID="CbxHist_Ge_Inoutbound" runat="server" CssClass="CbxDesc" Text="Llamada De Entrada" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>&nbsp;</td>
                                                                                <td colspan="4">
                                                                                    <asp:Button ID="BtnGuardar" runat="server" CssClass="Botones" Text="Guardar" Visible="false" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                    <td>
                                                                        <table>
                                                                            <tr class="Izquierda">
                                                                                <td>
                                                                                    <div class="ScroolCalTelefonica" id="ScrollCool">
                                                                                        <div class="force-overflow">
                                                                                            <asp:UpdatePanel ID="UpnlTelefonos" runat="server">
                                                                                                <ContentTemplate>
                                                                                                    <asp:GridView ID="GvCalTelefonos" runat="server" CssClass="mGridSeleccionable" Font-Names="Tahoma" Font-Size="Small" Style="font-size: x-small; text-align: right" Width="100%">
                                                                                                        <AlternatingRowStyle CssClass="alt" />
                                                                                                        <Columns>
                                                                                                            <asp:TemplateField>
                                                                                                                <ItemTemplate>
                                                                                                                    <table style="border-style: hidden; text-align: left">
                                                                                                                        <tr style="border-style: hidden;">
                                                                                                                            <td style="vertical-align: top; border-style: hidden">
                                                                                                                                <asp:ImageButton ID="ImgValido" runat="server" ImageUrl="~/Imagenes/ImgValido.png" OnClick="ImgValido_Click" />
                                                                                                                            </td>
                                                                                                                            <td style="vertical-align: top; border-style: hidden">&nbsp;</td>
                                                                                                                            <td style="vertical-align: top; border-style: hidden">
                                                                                                                                <asp:ImageButton ID="ImgNoValido" runat="server" ImageUrl="~/Imagenes/ImgNoValido.png" OnClick="ImgValido_Click" />
                                                                                                                            </td>
                                                                                                                            <td style="vertical-align: top; border-style: hidden">&nbsp;</td>
                                                                                                                            <td style="vertical-align: top; border-style: hidden">
                                                                                                                                <asp:Label ID="LblTelefono" runat="server" CssClass="LblDesc" Text='<%# Bind("Telefono") %>'></asp:Label>
                                                                                                                            </td>
                                                                                                                            <td style="vertical-align: top; border-style: hidden">&nbsp;</td>
                                                                                                                            <td style="vertical-align: top; border-style: hidden">
                                                                                                                                <asp:Image ID="ImgLike" runat="server" ImageUrl="~/Imagenes/ImgLike.png" />
                                                                                                                            </td>
                                                                                                                            <td style="vertical-align: top; border-style: hidden">&nbsp;</td>
                                                                                                                            <td style="vertical-align: top; border-style: hidden">
                                                                                                                                <asp:Image ID="ImgNoLike" runat="server" ImageUrl="~/Imagenes/ImgNoLike.png" Visible="false" />
                                                                                                                            </td>
                                                                                                                            <td>
                                                                                                                                <asp:ImageButton ID="ImgSMS" runat="server" OnClick="Bnt_Sms" />
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                    </table>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                        </Columns>
                                                                                                        <PagerStyle CssClass="pgr" />
                                                                                                    </asp:GridView>
                                                                                                </ContentTemplate>
                                                                                            </asp:UpdatePanel>
                                                                                        </div>
                                                                                    </div>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="LblPr_Mc_Credito" runat="server" CssClass="LblDescNS" Text="Crédito: " Visible="False"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblPr_Mc_CreditoV" runat="server" CssClass="LblDescNS" Visible="False"></asp:Label>&nbsp;&nbsp; </td>
                                            <td>

                                                <asp:Label ID="LblPr_Cd_Nombre" runat="server" CssClass="LblDescNS" Text="Nombre: " Visible="False"></asp:Label>
                                                <asp:Label ID="LblPr_Cd_NombreV" runat="server" CssClass="LblDescNS" Visible="False"></asp:Label>

                                                &nbsp;&nbsp;&nbsp;&nbsp;

                                            </td>

                                            <td>
                                                <asp:Label ID="LblPr_Mc_Expediente" runat="server" CssClass="LblDescNS" Text="Folio:" Visible="False"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="LblPr_Mc_ExpedienteV" runat="server" CssClass="LblDesc" Visible="False"></asp:Label>
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            </td>
                                            <td>
                                                <asp:Label ID="LblPr_Mc_EstatusF" runat="server" CssClass="LblDescNS" Text="Status:" Visible="False"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="LblPr_Mc_EstatusFV" runat="server" CssClass="LblDescNS" Visible="False"></asp:Label>
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            </td>
                                            <td>
                                                <asp:Label ID="LblPr_Mc_EstatusF_Text" runat="server" CssClass="LblDescNS" Text="Inactividad:" Visible="False"></asp:Label>
                                            </td>
                                            <td style="align-content:center">
                                                <asp:Label ID="LblPr_Mc_EstatusF_TextV" runat="server" CssClass="LblDescNS" Visible="False" Width="30" ></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <asp:TabContainer ID="TCM" runat="server" ActiveTabIndex="0" Width="100%" Visible="false">
                                        <asp:TabPanel runat="server" HeaderText="Información General" ID="TabPanel1">
                                            <HeaderTemplate>
                                                Información General
                                            </HeaderTemplate>
                                            <ContentTemplate>
                                                <table class="Table">
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <MPCU1:InformacionGeneral ID="InformacionGeneral" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr class="Arriba">
                                                        <td class="Arriba">
                                                            <table class="Table">
                                                                <tr class="Titulos">
                                                                    <td>Información Sistema</td>
                                                                    <td>Scoring Crédito</td>
                                                                </tr>
                                                                <tr class="Arriba">
                                                                    <td>

                                                                        <table class="Izquierda">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="LblPr_Mc_Usuario" runat="server" CssClass="LblDesc" Text="Gestor Trabajó"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="TxtPr_Mc_Usuario" runat="server" CssClass="TxtDesNS" ReadOnly="True" Width="200px"></asp:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="LblPr_Mc_Uasignado" runat="server" CssClass="LblDesc" Text="Gestor Asignado"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="TxtPr_Mc_Uasignado" runat="server" CssClass="TxtDesNS" ReadOnly="True"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="LblPr_Mc_Resultado" runat="server" CssClass="LblDesc" Text="Última Gestión"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="TxtPr_Mc_Resultado" runat="server" CssClass="TxtDesNS" ReadOnly="True" Width="200px"></asp:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="LblPr_Mc_Dtegestion" runat="server" CssClass="LblDesc" Text="Fecha Última Gestión"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="TxtPr_Mc_Dtegestion" runat="server" CssClass="TxtDesNS" ReadOnly="True"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="LblPr_Mc_Resultadorelev" runat="server" CssClass="LblDesc" Text="Mejor Gestión"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="TxtPr_Mc_Resultadorelev" runat="server" CssClass="TxtDesNS" ReadOnly="True" Width="200px"></asp:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="LblPr_Mc_Dteresultadorelev" runat="server" CssClass="LblDesc" Text="Fecha Última Gestión"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="TxtPr_Mc_Dteresultadorelev" runat="server" CssClass="TxtDesNS" ReadOnly="True"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </table>

                                                                    </td>
                                                                    <td>
                                                                        <table>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="LblVi_Semaforo_Gestion" runat="server" CssClass="LblDesc" Text="Semaforo Gestión"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Image ID="ImgSemaforo" runat="server" ImageUrl="~/Imagenes/ImgSemaforoRojo.png" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="3">
                                                                                    <asp:Label ID="LblVi_Dias_Semaforo_Gestion" runat="server" CssClass="LblDesc"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="LblPr_Mc_Creditocontactado" runat="server" CssClass="LblDesc" Text="Deudor Contactado"></asp:Label>
                                                                                </td>
                                                                                <td colspan="2">
                                                                                    <asp:Image ID="ImgNoContacto" runat="server" ImageUrl="~/Imagenes/ImgNoContacto.png" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="LblPr_Mc_Dtecreditocontactado" runat="server" CssClass="LblDesc" Text="Fecha De Contacto"></asp:Label>
                                                                                </td>
                                                                                <td colspan="2">
                                                                                    <asp:TextBox ID="TxtPr_Mc_Dtecreditocontactado" runat="server" CssClass="TxtDesNS" ReadOnly="True"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="LblPr_Mc_Primeragestion" runat="server" CssClass="LblDesc" Text="Primera Gestión"></asp:Label>
                                                                                </td>
                                                                                <td colspan="2">
                                                                                    <asp:TextBox ID="TxtPr_Mc_Primeragestion" runat="server" CssClass="TxtDesNS" ReadOnly="True"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="LblPr_Mc_Dteprimeragestion" runat="server" CssClass="LblDesc">Fecha Primera Gestión</asp:Label>
                                                                                </td>
                                                                                <td colspan="2">
                                                                                    <asp:TextBox ID="TxtPr_Mc_Dteprimeragestion" runat="server" CssClass="TxtDesNS" ReadOnly="True"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>&nbsp;</td>
                                                                                <td>&nbsp;</td>
                                                                                <td>&nbsp;</td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:TabPanel>
                                        <asp:TabPanel runat="server" HeaderText="Información Financiera" ID="InfoGral">
                                            <HeaderTemplate>
                                                Información Avales
                                            </HeaderTemplate>
                                            <ContentTemplate>
                                                <MPCU2:InformacionFinanciera ID="InformacionFinanciera" runat="server" >
                                                
                                                </MPCU2:InformacionFinanciera>
                                            </ContentTemplate>
                                        </asp:TabPanel>
                                        <asp:TabPanel runat="server" HeaderText="Información Adicional" ID="InfoAdicinal">
                                            <HeaderTemplate>
                                                Información Adicional
                                            </HeaderTemplate>
                                            <ContentTemplate>
                                                <MPCU7:Adicional ID="Adicional" runat="server" />
                                            </ContentTemplate>
                                        </asp:TabPanel>
                                        <asp:TabPanel runat="server" HeaderText="Histórico Actividades" ID="HisAct">
                                            <HeaderTemplate>
                                                Histórico Actividades
                                            </HeaderTemplate>
                                            <ContentTemplate>
                                                <table class="Table">
                                                    <tr class="Titulos">
                                                        <td>Histórico De Actividades</td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="LblSort" runat="server" CssClass="LblDesc" Text="Ordenar Por:"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="DdlSort" runat="server" CssClass="DdlDesc">
                                                                            <asp:ListItem Selected="True" Value="Hist_Ge_Dteactividad">Fecha</asp:ListItem>
                                                                            <asp:ListItem Value="Hist_Ge_Ponderacion">Ponderación</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="LblSortTipo" runat="server" CssClass="LblDesc" Text="Tipo:"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:RadioButtonList ID="RblSortTipo" runat="server" AutoPostBack="True" CssClass="RblDesc" RepeatDirection="Horizontal">
                                                                            <asp:ListItem Value="Asc">Ascendente</asp:ListItem>
                                                                            <asp:ListItem Value="Desc">Descendente</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr align="center">
                                                        <td>
                                                            <div class="ScroolHist_ActCap">
                                                                <div class="force-overflow">
                                                                    <asp:GridView ID="GvHistAct" runat="server" CssClass="mGridSeleccionable" Font-Names="Tahoma" Font-Size="Small" AllowPaging="True" PageSize="40" OnPageIndexChanging="GvHist_Act_PageIndexChanging">
                                                                        <AlternatingRowStyle CssClass="alt" />
                                                                        <PagerStyle CssClass="pgr" />
                                                                    </asp:GridView>
                                                                </div>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <!-- Aqui dibujo el historico de Clientes -->
                                                    <tr class="Titulos">
                                                        <td>Histórico Atencion A Clientes</td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                            <div class="ScroolHist_ActOtro">
                                                                <div class="force-overflow">
                                                                    <asp:GridView ID="GVHist_Atencion_C" runat="server" CssClass="mGridSeleccionable" Font-Names="Tahoma" Font-Size="Small" AllowPaging="True" PageSize="40" OnPageIndexChanging="GVHist_Atencion_C_PageIndexChanging">
                                                                        <AlternatingRowStyle CssClass="alt" />
                                                                        <PagerStyle CssClass="pgr" />
                                                                    </asp:GridView>
                                                                </div>
                                                            </div>
                                                        </td>
                                                    </tr>

                                                    <!-- Aqui acabo el historico clientes -->
                                                    <tr>
                                                        <td>
                                                            <MPCU3:Historico_De_Actividades ID="HIstoricos_De_Actividades" runat="server" />
                                                        </td>
                                                    </tr>
                                                </table>

                                            </ContentTemplate>
                                        </asp:TabPanel>
                                        <asp:TabPanel runat="server" HeaderText="Histórico Pagos" ID="TCHist_Pagos">
                                            <HeaderTemplate>
                                                Histórico Pagos
                                            </HeaderTemplate>
                                            <ContentTemplate>
                                                <MPCU5:Hist_Pagos ID="Hist_Pagos" runat="server"></MPCU5:Hist_Pagos>
                                            </ContentTemplate>
                                        </asp:TabPanel>
                                        <asp:TabPanel runat="server" HeaderText="Histórico Visitas" ID="TC_Hist_Visitas">
                                            <HeaderTemplate>
                                                Histórico Visitas
                                            </HeaderTemplate>
                                            <ContentTemplate>
                                                <table class="Table">
                                                    <tr class="Titulos">
                                                        <td>Histórico De Visitas</td>
                                                    </tr>
                                                    <tr class="Izquierda">
                                                        <td>
                                                            <asp:ImageButton ID="ImgAddVisita" runat="server" ImageUrl="~/Imagenes/ImgAddVisitas.png" ToolTip="Ir A Módulo Captura De Visitas" Visible="false" />

                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <div class="ScroolHistVisitas">
                                                                <div class="force-overflow">

                                                                    <asp:GridView ID="GvVisitas" runat="server" CssClass="mGrid" Font-Names="Tahoma" Font-Size="Small">
                                                                        <AlternatingRowStyle CssClass="alt" Width="100%" />
                                                                        <PagerStyle CssClass="pgr" />
                                                                    </asp:GridView>
                                                                </div>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:TabPanel>
                                        <asp:TabPanel runat="server" HeaderText="Negociaciones" ID="TNegociaciones">
                                            <HeaderTemplate>
                                                Negociaciones
                                            </HeaderTemplate>
                                            <ContentTemplate>
                                                <MPCU8:GNegociaciones ID="UNegociaciones" runat="server" />
                                            </ContentTemplate>
                                        </asp:TabPanel>
                                        <asp:TabPanel ID="TcRelacionados" runat="server" HeaderText="Créditos Relacionados">
                                            <HeaderTemplate>
                                                Créditos Relacionados
                                            </HeaderTemplate>
                                            <ContentTemplate>
                                                <table class="Table">
                                                    <tr>
                                                        <td>
                                                            <MPCU4:Relacionados ID="Relacionados" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:TabPanel>
                                        <asp:TabPanel ID="TcJudicial" runat="server" HeaderText="Judicial">
                                            <HeaderTemplate>
                                                Judicial
                                            </HeaderTemplate>
                                            <ContentTemplate>
                                                <table class="Table">
                                                    <tr>
                                                        <td>
                                                            <MPCU6:Judicial ID="Judicial" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:TabPanel>
                                        <asp:TabPanel runat="server" HeaderText="Histórico De Documentos" ID="TcDocumentos">
                                            <HeaderTemplate>
                                                Histórico De Documentos
                                            </HeaderTemplate>
                                            <ContentTemplate>
                                                <MPCU10:Documentos ID="Documentos" runat="server"></MPCU10:Documentos>
                                            </ContentTemplate>
                                        </asp:TabPanel>
                                        <asp:TabPanel runat="server" HeaderText="Información Referencias" ID="TCReferencias">
                                            <HeaderTemplate>
                                                Información Referencias
                                            </HeaderTemplate>
                                            <ContentTemplate>
                                                <MPCU11:Referencias ID="Referencias" runat="server"></MPCU11:Referencias>
                                            </ContentTemplate>
                                        </asp:TabPanel>
                                    
                                    '''''''''''''''''''''''''''''''''''
                                        <asp:TabPanel runat="server" HeaderText="Envío De Correos" ID="TCEnvioCorreos">
                                            <HeaderTemplate>
                                                Envío De Correos
                                            </HeaderTemplate>
                                            <ContentTemplate>
                                                <MPCU12:EnvioCorreos ID="EnvioCorreos" runat="server"></MPCU12:EnvioCorreos>
                                            </ContentTemplate>
                                        </asp:TabPanel>
                                    '''''''''''''''''''''''''''''''''''
                                    </asp:TabContainer>
                                </td>
                            </tr>
                        </table>
                    </div>

                    <asp:Button ID="BtnModalSMS" runat="server" Style="visibility: hidden" />
                    <asp:Panel ID="PnlESms" runat="server" CssClass="ModalMsj" Style="display: none">
                        <div class="heading">
                            <table>
                                <tr>
                                    <td>

                                        <asp:Label ID="LblMsjSms" runat="server"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="LblTelSMS" runat="server"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="LblTelSMSTIPO" runat="server" Visible="false"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="CuerpoMsj">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="LblOpcionSms" runat="server" Text="Mensaje Del SMS" CssClass="LblDesc"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DdlOpcionSms" runat="server" CssClass="DdlDesc" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:TextBox ID="TxtSms" runat="server" Height="74px" ReadOnly="True" TextMode="MultiLine" Width="283px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="BtnEnviarSms" runat="server" CssClass="Botones" Text="Enviar" />
                                    </td>
                                    <td>
                                        <asp:Button ID="BtnCancelarSms" runat="server" CssClass="Botones" Text="Cancelar" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="LblErrorSms" runat="server" CssClass="LblError"></asp:Label>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                        </div>
                    </asp:Panel>
                    <asp:ModalPopupExtender ID="MpuESMS" runat="server" PopupControlID="PnlESms" TargetControlID="BtnModalSMS"
                        CancelControlID="BtnCancelarSms" BackgroundCssClass="modalBackground">
                    </asp:ModalPopupExtender>


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
                    <asp:Button ID="BtnModalSession" runat="server" Style="visibility: hidden" />
                    <asp:Panel ID="PnlModalSession" runat="server" CssClass="ModalMsj" Style="display: none">
                        <div class="heading">
                            <font id="TimeField" size="3"><span class="count">0</span></font>
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
                                <td colspan="2">
                                    <asp:Label ID="LblTitulo" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr class="CuerpoAcciones">
                                <td colspan="2">
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
                                <td>
                                    <asp:Button ID="BtnAceptarAcciones" runat="server" CssClass="button green close" Visible="false" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:ModalPopupExtender ID="MpuAcciones" runat="server" PopupControlID="PnlModalAcciones" TargetControlID="BtnModalAcciones"
                        CancelControlID="BtnCancelarAcciones" BackgroundCssClass="modalBackground">
                    </asp:ModalPopupExtender>
                    <asp:Button ID="BtnModalPromesa" runat="server" Style="visibility: hidden" />
                    <asp:Panel ID="PnlModalPromesa" runat="server" CssClass="ModalAcciones" Style="display: none">
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
        </asp:Panel>
        <input id="TextoBusca" type="text" runat="server" style="display: none; border-style: none; border-color: inherit; border-width: medium; width: 0px; height: 2px; background-color: #b6b7bc; color: gray" />
        <asp:Button ID="buscarNuevo" runat="server" Text="" BackColor="Gray" BorderColor="Gray" BorderStyle="None"
            Width="0px" Style="height: 0px; display: none" />
        <script language="JavaScript" type="text/javascript">
            AC_FL_RunContent(
            'codebase', 'http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=10,0,0,0',
            'width', '0px',
            'height', '0px',
            'src', 'JavaScriptPackageRemote',
            'quality', 'best',
            'pluginspage', 'http://www.adobe.com/go/getflashplayer',
            'align', 'middle',
            'play', 'true',
            'loop', 'true',
            'scale', 'showall',
            'wmode', 'opaque',
            'devicefont', 'false',
            'id', 'JavaScriptPackageRemote',
            'bgcolor', '#ffffff',
            'name', 'JavaScriptPackageRemote',
            'menu', 'true',
            'allowFullScreen', 'false',
            'allowScriptAccess', 'sameDomain',
            'movie', 'JavaScriptPackageRemote',
            'salign', ''
            ); //end AC code
        </script>
        <noscript>
            <object classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=10,0,0,0"
                width="0px" height="0px" id="JavaScriptPackageRemote" align="middle">
                <param name="allowScriptAccess" value="always" />
                <param name="allowFullScreen" value="false" />
                <param name="movie" value="JavaScriptPackageRemote.swf" />
                <param name="quality" value="best" />
                <param name="wmode" value="opaque" />
                <param name="bgcolor" value="#ffffff" />
                <embed src="JavaScriptPackageRemote.swf" quality="best" wmode="opaque" bgcolor="#ffffff"
                    width="0px" height="0px" name="JavaScriptPackageRemote" align="middle" allowscriptaccess="always"
                    allowfullscreen="false" type="application/x-shockwave-flash" pluginspage="http://www.adobe.com/go/getflashplayer" />
            </object>
        </noscript>
    </form>
</body>
</html>
