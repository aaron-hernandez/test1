<%@ Page Language="VB" AutoEventWireup="false" CodeFile="LogIn.aspx.vb" Inherits="LogIn" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>Inicio de sesión</title>
    <link href="Estilos/HTML.css" rel="stylesheet" />
    <link href="Estilos/ObjAjax.css" rel="stylesheet" />
    <link href="Estilos/ObjHtmlNoS.css" rel="stylesheet" />
    <link href="Estilos/ObjHtmlS.css" rel="stylesheet" />
    <link href="Imagenes/IcoLogo_Mc.ico" rel="Shortcut icon" />
    <script type="text/javascript">
        function disableBackButton() {
            window.history.forward();
        }
        setTimeout("disableBackButton()", 0);
    </script>
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
</head>
<body>
    <form id="FrmLogin" runat="server">
        <asp:ToolkitScriptManager ID="TKSMLogin" runat="server"></asp:ToolkitScriptManager>
        <div class="Pagina">
            <table class="Table">
                <tr>
                    <td class="Centro">
                        <asp:Image ID="ImgLogo_Cl" runat="server" ImageUrl="~/Imagenes/ImgLogo_Cl2.jpg" />
                    </td>
                    <td class="TituloP">Módulo De Gestión
                    </td>
                    <td style="width: 30%" class="Derecha">&nbsp;</td>
                </tr>
            </table>
            <div class="Espacio">
                &nbsp;
            </div>
            <br />
            <asp:Label ID="Lbl0" runat="server" CssClass="LblDesc" Text="" Visible="false"></asp:Label>
            <br />
            <br />
            <br />
            <br />
            <br />
            <asp:UpdatePanel ID="UpnlGral" runat="server">
                <ContentTemplate>
                    <asp:UpdatePanel ID="UpnlLogin" runat="server">
                        <ContentTemplate>
                            <table align="center">
                                <tr>
                                    <td>
                                        <asp:Label ID="LblCat_Lo_Usuario" runat="server" Text="Usuario" CssClass="LblDesc"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="UserName" runat="server" MaxLength="25" Width="127px" AutoCompleteType="Disabled" autocomplete="off"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="UserName" Display="Static" ErrorMessage="* Introduce Usuario" ForeColor="#03205C"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="LblCat_Lo_Contrasena" runat="server" Text="Contraseña" CssClass="LblDesc"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="UserPass" runat="server" MaxLength="25" TextMode="Password" Width="127px" AutoCompleteType="Disabled" autocomplete="off"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="UserPass" Display="Static" ErrorMessage="* Introduce Contraseña" ForeColor="#03205C"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>
                                        <asp:UpdateProgress ID="UpnlProgres" runat="server">
                                            <ProgressTemplate>
                                                <asp:Image ID="ImgProgres" runat="server" ImageUrl="~/Imagenes/ImgBarraProgreso.gif" />
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </td>
                                    <td>
                                        <asp:Button ID="LoginBtn" runat="server" CssClass="Botones" Text="Iniciar Sesión" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:UpdatePanel runat="server" ID="UpnlMsj">
                                            <ContentTemplate>
                                                <asp:Label ID="LblMsj" runat="server" CssClass="LblMsj"></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:UpdatePanel ID="UPnlCambioPass" runat="server" Visible="false">
                        <ContentTemplate>
                            <br />
                            <br />
                            <div class="CajaDialogo">
                                <table align="center">
                                    <tr class="Titulos">
                                        <td colspan="2">Usuario Expirado, Cambie su Contraseña</td>
                                    </tr>
                                    <tr>
                                        <td class="Centro" colspan="2">
                                            <asp:Label ID="LblUsuario" runat="server" CssClass="LblMsj" Text="Usuario"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="LblPass1" runat="server" CssClass="LblDesc" Text="Contraseña"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtPassword" runat="server" AutoPostBack="False" MaxLength="25" TextMode="Password"></asp:TextBox>
                                            <%--<asp:PasswordStrength ID="TxtPassword_PasswordStrength" runat="server" BarBorderCssClass="barIndicatorBorder"
                                                BarIndicatorCssClass="MuySimple;Simple;Bien;Fuerte;Excelente" CalculationWeightings="50;15;15;20" DisplayPosition="RightSide"
                                                Enabled="True" HelpStatusLabelID="Pass_l" MinimumNumericCharacters="1" MinimumSymbolCharacters="1" PreferredPasswordLength="8"
                                                PrefixText="Complejidad:" RequiresUpperAndLowerCaseCharacters="True" MinimumLowerCaseCharacters="1" MinimumUpperCaseCharacters="1" StrengthIndicatorType="BarIndicator"
                                                StrengthStyles="MuySimple; Simple; Bien; Fuerte; Excelente" TargetControlID="TxtPassword">
                                            </asp:PasswordStrength>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Label ID="Pass_l" runat="server" Style="color: #000000; font-size: small"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="LblPass2" runat="server" CssClass="LblDesc" Text="Repetir Contraseña"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtPassword2" runat="server" MaxLength="25" TextMode="Password"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Label ID="LblErrores" runat="server" CssClass="LblError"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="Centro">
                                        <td>
                                            <asp:Button ID="guardarcon" runat="server" CssClass="Botones" Text="Guardar" />
                                        </td>
                                        <td>
                                            <asp:Button ID="guardarsin" runat="server" CssClass="Botones" Text="Cancelar" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </ContentTemplate>
            </asp:UpdatePanel>
            <br />
            <br />
            <br />
            <br />
            <br />
            <img id="ImgLogo_Mc2" src="Imagenes/ImgLogo_Mc2.jpg" />
            <asp:Label ID="LblVer" runat="server" Text="Powered By MC V3" Style="color: #000000; font-size: xx-small" ToolTip="Actualización:06/05/2015"></asp:Label>
            <br />
        </div>
    </form>
</body>
</html>
