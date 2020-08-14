<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Documentos.ascx.vb" Inherits="Documentos" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<link href="Estilos/HTML.css" rel="stylesheet" />
<link href="Estilos/ObjHtmlS.css" rel="stylesheet" />
<link href="Estilos/ObjHtmlNoS.css" rel="stylesheet" />
<script type="text/javascript">
    function uploadComplete(sender) {
        //var Desc = document.getElementById("Menu_TabDocumetos_Documentos_txtDescripcion");
        var Desc = document.getElementById('<%= txtDescripcion.ClientID%>').value.length;
        //alert(Desc)

        if (Desc > 5) {
            $get("<%=lblMesg.ClientID%>").innerHTML = "El Documento se Cargo Correctamente.";
            var holder = document.getElementById('<%= AsyncFileUpload1.ClientID %>');
            var txts = holder.getElementsByTagName("input");
            for (var i = 0; i < txts.length; i++) {
                if (txts[i].type == "file") {
                    txts[i].form.reset();
                    txts[i].focus();
                }
            }
            window.location = "MasterPage.aspx";
        } else {
            $get("<%=lblMesg.ClientID%>").innerHTML = "Capture una Descripción Valida.";
            var holder = document.getElementById('<%= AsyncFileUpload1.ClientID %>');
            var txts = holder.getElementsByTagName("input");
            for (var i = 0; i < txts.length; i++) {
                if (txts[i].type == "file") {
                    txts[i].form.reset();
                    txts[i].focus();
                }
                // alert("Capture una Descripción Valida.")
            }
        }
    }
    function uploadError(sender) {
        $get("<%=lblMesg.ClientID%>").innerHTML = "Fallo la Carga.";

        var holder = document.getElementById('<%= AsyncFileUpload1.ClientID %>');
        var txts = holder.getElementsByTagName("input");
        for (var i = 0; i < txts.length; i++) {
            if (txts[i].type == "file") {
                txts[i].form.reset();
                txts[i].focus();
            }
        }
    }



</script>

<table style="width:100%">
    <tr>
        <td>
            <asp:Panel ID="PnlCargar" runat="server" Visible="false">
                <table>
                    <tr>
        <td class="Titulos" colspan="4">Carga De Documentos</td>
    </tr>
    <tr>
        <td>
            <asp:Image ID="imgLoader0" runat="server" ImageUrl="~/Imagenes/ImgOne.png"  ToolTip="Capture Una Descripción Para El Documento Que Va A cargar"/>
            <asp:Label ID="LblPaso1" runat="server" Text="Agregar Descripción: " CssClass="LblDesc"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtDescripcion" runat="server" Height="40px" MaxLength="500" TextMode="MultiLine" Width="300px"></asp:TextBox>
        </td>
        <td>
            <asp:Image ID="imgLoader" runat="server" ImageUrl="~/Imagenes/ImgCarga.gif"/>
        </td>
        <td> 
            <asp:Image ID="imgLoader1" runat="server" ImageUrl="~/Imagenes/ImgTwo.png"  ToolTip="Recuerde Capturar La Descripcion Antes De Iniciar La Selección Del Archivo, Una Vez Capturada La Descripción Seleccione El PDF A Cargar Y El Sistema Realizara La Carga En Automatico"/>
            <asp:Label ID="LblPaso2" runat="server" CssClass="LblDesc" Text="Cargar Documento: "></asp:Label>
            <asp:AsyncFileUpload ID="AsyncFileUpload1" runat="server" FailedValidation="False" OnClientUploadComplete="uploadComplete" OnClientUploadError="uploadError" Style="text-align: left" ThrobberID="imgLoader" Width="400px" />
        </td>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td>
            <asp:Label ID="lblMesg" runat="server" CssClass="LblDesc"></asp:Label>
        </td>
    </tr>
                </table>
            </asp:Panel>            
        </td>
    </tr>
    <!-- Dede aqui -->
    <tr>
        <td colspan="4" align="center" bgcolor="#03205C" style="font-family: 'Century Gothic'; font-size: medium;
                            color: #FFFFFF; font-weight: bold;" >Historico de Documentos</td>
    </tr>
    <tr>
        <td colspan="4" align="center">
            <asp:Label ID="lbmsj" runat="server" CssClass="LblDesc"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="4" align="center">
            <asp:GridView ID="GridArchivos" runat="server" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="True" CssClass="mGrid" Font-Names="Tahoma" Font-Size="small" PagerStyle-CssClass="pgr">
                <Columns>
                    <asp:TemplateField HeaderText="ELIMINAR">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="False" OnCheckedChanged="chkSelect_CheckedChanged" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField ButtonType="Image" HeaderText="PDF" SelectImageUrl="~/Imagenes/ImgDownloadPdf.jpg" ShowSelectButton="True" />
                </Columns>
            </asp:GridView>
        </td>
    </tr>
    <!--Hasta aqui -->
    <tr>
        <td colspan="4" align="center">
            <asp:Button ID="BtnEliminar" runat="server" Text="Eliminar" Visible="False" CssClass="Botones" />
        </td>
    </tr>
    <!-- Dede aqui -->
    <tr>
        <td colspan="4" align="center" bgcolor="#03205C" style="font-family: 'Century Gothic'; font-size: medium;
                            color: #FFFFFF; font-weight: bold;" >Historico de Convenios</td>
    </tr>
    <tr>
        <td colspan="4" align="center">
            <asp:Label ID="Lbmsj2" runat="server" CssClass="LblDesc"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="4" align="center">
            <asp:GridView ID="GridConvenios" runat="server" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="True" CssClass="mGrid" Font-Names="Tahoma" Font-Size="small" PagerStyle-CssClass="pgr">
                <Columns>
                    <asp:CommandField ButtonType="Image" HeaderText="PDF CONVENIO" SelectImageUrl="~/Imagenes/ImgDownloadPdf.jpg" ShowSelectButton="True" />
                </Columns>
            </asp:GridView>
        </td>
    </tr>
    <!--Hasta aqui -->
</table>
<asp:Label ID="LblCat_Lo_Usuario" runat="server" Visible="false"></asp:Label>

