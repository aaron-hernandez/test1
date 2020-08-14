<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Relacionados.ascx.vb" Inherits="Relacionados" %>

<link href="Estilos/HTML.css" rel="stylesheet" />
<link href="Estilos/ObjHtmlNoS.css" rel="stylesheet" />
<link href="Estilos/ObjHtmlS.css" rel="stylesheet" />
<asp:Label ID="LblCat_Lo_Usuario" runat="server" Visible="false"></asp:Label>
<table class="Table">
    <tr class="Titulos">
        <td>Créditos Relacionados
        </td>
    </tr>
    <tr align="center">
        <td>
            <asp:GridView ID="GvRelacionados" runat="server" AlternatingRowStyle-CssClass="alt" CssClass="mGrid" Font-Names="Tahoma" Font-Size="small" PagerStyle-CssClass="pgr" Style="font-size: small" AutoGenerateColumns="true">
                <AlternatingRowStyle CssClass="alt" />
                <Columns>
                    <asp:CommandField ButtonType="Image" HeaderText="" SelectImageUrl="~/Imagenes/ImgSeleccion.png" ShowSelectButton="True" />
                </Columns>
                <PagerStyle CssClass="pgr" />
            </asp:GridView>
        </td>
    </tr>
</table>
