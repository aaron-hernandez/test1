<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Historicos_De_Actividades.ascx.vb" Inherits="Historicos_De_Actividades" %>
<link href="Estilos/ObjHtmlS.css" rel="stylesheet" />
<link href="Estilos/ObjHtmlNoS.css" rel="stylesheet" />
<link href="Estilos/ObjAjax.css" rel="stylesheet" />
<link href="Estilos/Modal.css" rel="stylesheet" />
<link href="Estilos/HTML.css" rel="stylesheet" />
<table class="Table">
    <!--
    <tr class="Titulos">
        <td>Histórico Judicial&nbsp;</td>
    </tr>
    <tr align="center">
        <td>
            <div class="ScroolHist_ActOtro">
                <div class="force-overflow">
             <asp:GridView ID="GvHistActJudicial" runat="server" CssClass="mGrid" Font-Names="Tahoma" Font-Size="Small" AllowPaging="True" PageSize="40" OnPageIndexChanging="GvHistActJudicial_PageIndexChanging">
                        <AlternatingRowStyle CssClass="alt" />
                        <PagerStyle CssClass="pgr" />
                    </asp:GridView>
                    </div>
                </div>
        </td>
    </tr>
    -->
    <tr class="Titulos">
        <td>Histórico De Actividades 2</td>
    </tr>
    <tr>
        <td align="center">
            <div class="ScroolHist_ActOtro">
                <div class="force-overflow">
                    <asp:GridView ID="GvHistActMasivo" runat="server" CssClass="mGrid" Font-Names="Tahoma" Font-Size="Small" AllowPaging="True" PageSize="40" OnPageIndexChanging="GvHistActMasivo_PageIndexChanging">
                        <AlternatingRowStyle CssClass="alt" />
                        <PagerStyle CssClass="pgr" />
                    </asp:GridView>
                </div>
            </div>
        </td>
    </tr>
    <!--  Comentando el historico de atencion a clientes, este sera cargado en la master page
    <tr class="Titulos">
        <td>Histórico Atencion A Clientes</td>
    </tr>
    <tr>
        <td align="center">
            <div class="ScroolHist_ActOtro">
                <div class="force-overflow">
                    <asp:GridView ID="GVHist_Atencion_C" runat="server" CssClass="mGrid" Font-Names="Tahoma" Font-Size="Small" AllowPaging="True" PageSize="40" OnPageIndexChanging="GvHistActMasivo_PageIndexChanging">
                        <AlternatingRowStyle CssClass="alt" />
                        <PagerStyle CssClass="pgr" />
                    </asp:GridView>
                </div>
            </div>
        </td>
    </tr>
     -->
    
</table>
