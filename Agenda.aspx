<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Agenda.aspx.vb" Inherits="Agenda" %>

<!DOCTYPE html>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Mc :: Modulo Gestión</title>
    <link href="Estilos/ObjHtmlS.css" rel="stylesheet" />
    <link href="Estilos/ObjHtmlNoS.css" rel="stylesheet" />
    <link href="Estilos/ObjAjax.css" rel="stylesheet" />
    <link href="Estilos/Modal.css" rel="stylesheet" />
    <link href="Estilos/HTML.css" rel="stylesheet" />
    <script src="JQ/jquery.min.js"></script>
    <script src="JQ/jquery.timer.js"></script>
    <script lang="javascript" type="text/javascript">
        var count = 180
        var Verde = 'JQ/SessionActive.png';
        var Amarillo = 'JQ/SessionInActive2.png';
        var Rojo = 'JQ/SessionInActive.png';
        var timer = $.timer(
            function () {
                count--;
                if (count == 0) {
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
                url: "Agenda.aspx/KeepActiveSession",
                data: {},
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true
            });
        };
    </script>
</head>
<body onclick="Mover(event);">
    <form id="FrmAgenda" runat="server">
        <asp:ToolkitScriptManager ID="TKSMMaster" runat="server" CombineScripts="true" EnableScriptGlobalization="true">
            <Services>
                <asp:ServiceReference Path="~/MGestion/CompletarDatosCP.asmx" />
            </Services>
        </asp:ToolkitScriptManager>
        <asp:UpdatePanel ID="UpnlGral" runat="server">
            <ContentTemplate>
                <div class="Pagina">
                    <table class="Table">
                        <tr>
                            <td class="Izquierda" rowspan="2" style="width:30%">
                                <asp:Image ID="ImgLogo_Cl" runat="server" ImageUrl="~/Imagenes/ImgLogo_Cl.png" />
                            </td>
                            <td class="TituloP" rowspan="2" style="width:40%">Módulo De Gestión 
                            </td>
                            <td class="Derecha" style="width:30%">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="Derecha">
                                <div>
                                    <marquee width="30%" scrolldelay="100" scrollamount="5" direction="left"
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
                    <asp:UpdatePanel ID="UPnlFilas" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="PnlFilas" runat="server" DefaultButton="BtnCrear">
                                <table width="100%">
                                    <tr align="center" class="Titulos">
                                        <td colspan="2">Filas de Trabajo
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <br />
                                        </td>
                                    </tr>
                                    <tr align="center">
                                        <td class="LblDesc">Crear fila de Trabajo por: 
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DrlFila" runat="server" AutoPostBack="True" CssClass="DdlDesc">
                                                <asp:ListItem>Seleccione</asp:ListItem>
                                                <asp:ListItem>Código Resultado</asp:ListItem>
                                                <asp:ListItem>Código Resultado ponderado</asp:ListItem>
                                                <asp:ListItem>Semaforo</asp:ListItem>
                                                <asp:ListItem>Promesas De Pago</asp:ListItem>
                                                <asp:ListItem>Contactar Hoy</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr align="center">
                                        <td colspan="2">
                                            <div class="DivFilas">
                                                <div class="force-overflow">
                                                    <asp:GridView ID="GridFilas" runat="server" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="true" CssClass="mGrid" Font-Names="Tahoma" Font-Size="Small" PagerStyle-CssClass="pgr" Style="text-align: left; font-size: x-small;">
                                                        <AlternatingRowStyle CssClass="alt" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="CheckAll">
                                                                <HeaderTemplate>
                                                                    <asp:CheckBox ID="chkSelectAll" runat="server" AutoPostBack="true" OnCheckedChanged="chkSelectAll_CheckedChanged" />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="true" OnCheckedChanged="chkSelect_CheckedChanged" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr align="center">
                                        <td>
                                            <asp:Button ID="BtnCrear" runat="server" CLASS="Botones" Text="Crear Fila" Visible="False" />
                                        </td>
                                        <td>
                                            <asp:Button ID="BtnRegresar" runat="server" CLASS="Botones" Text="Regresar" />
                                        </td>
                                    </tr>
                                    <tr class="Centro">
                                        <td colspan="2">
                                            <asp:Label ID="LblMsjFilas" runat="server" CssClass="LblMsj"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="Titulos">
                                        <td  colspan="2">
                                            Avance
                                        </td>
                                    </tr>
                                    <tr class="Centro">
                                        <td ALIGN="CENTER" colspan="2">
                                            <table>
                                                <tr>
                                                    <td>
                                                   
                                                        <table border="1">
                                                            <tr>
                                                                <td colspan="5" class="Titulos">
                                                                    <asp:Label ID="LblAsignadas" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr class="Titulos">
                                                                <td colspan="2">Resultados en el día</td>
                                                                <td colspan="3">Resultados en el mes</td>
                                                            </tr>
                                                            <tr class="Titulos">
                                                                <td colspan="2">
                                                                    <asp:Label ID="LblPromesasMetaDia" runat="server"></asp:Label>
                                                                </td>
                                                                <td colspan="3">
                                                                    <asp:Label ID="LblPromesasMetaMes" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr class="Titulos">
                                                                <td>Cuentas Trabajadas</td>
                                                                <td>Resultados de Gestión</td>
                                                                <td>&nbsp;&nbsp;&nbsp; &nbsp;</td>
                                                                <td>Cuentas Trabajadas</td>
                                                                <td>Resultados de Gestión</td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center">
                                                                    <asp:Label ID="LblTrabajadas" runat="server" CssClass="LblDescNS"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:GridView ID="GvRDia" runat="server" CssClass="mGrid" Style="font-size: small">
                                                                        <AlternatingRowStyle CssClass="alt" />
                                                                        <PagerStyle CssClass="pgr" />
                                                                    </asp:GridView>
                                                                </td>
                                                                <td>&nbsp;</td>
                                                                <td  align="center">
                                                                    <asp:Label ID="LblTrabajadasM" runat="server" CssClass="LblDescNS"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:GridView ID="GvRmes" runat="server" CssClass="mGrid" Style="font-size: small">
                                                                        <AlternatingRowStyle CssClass="alt" />
                                                                        <PagerStyle CssClass="pgr" />
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="LblPPPD" runat="server" CssClass="LblDescNS"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="LblMtoPPPD" runat="server" CssClass="LblDescNS"></asp:Label>
                                                                </td>
                                                                <td>&nbsp;</td>
                                                                <td>
                                                                    <asp:Label ID="LblPPPM" runat="server" CssClass="LblDescNS"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="LblMtoPPPM" runat="server" CssClass="LblDescNS"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr class="Titulos">
                                        <td colspan="2">Mi Asignación</td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="center">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <div class="ScroolBusquedas">
                                                            <asp:GridView ID="GvAsignacion" runat="server" AlternatingRowStyle-CssClass="alt" CssClass="mGrid" Font-Names="Tahoma" Font-Size="small" PagerStyle-CssClass="pgr" Style="font-size: small" AutoGenerateColumns="true">
                                                                <AlternatingRowStyle CssClass="alt" />
                                                                <Columns>
                                                                    <asp:CommandField ButtonType="Image" HeaderText="" SelectImageUrl="~/Imagenes/ImgSeleccion.png" ShowSelectButton="True" />
                                                                </Columns>
                                                                <PagerStyle CssClass="pgr" />
                                                            </asp:GridView>
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
                    <asp:HiddenField ID="HidenUrs" runat="server" />

                </div>
                <asp:Button ID="BtnModalMsj" runat="server" Style="visibility: hidden" />
                <asp:Panel ID="PnlModalMsj" runat="server" CssClass="ModalMsj" Style="display: none">
                    <%-- Style="display: none">--%>
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
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>

