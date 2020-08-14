<%@ Control Language="VB" AutoEventWireup="false" CodeFile="InformacionGeneral.ascx.vb" Inherits="InformacionGeneral" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<link href="Estilos/HTML.css" rel="stylesheet" />
<link href="Estilos/ObjHtmlNoS.css" rel="stylesheet" />
<link href="Estilos/ObjHtmlS.css" rel="stylesheet" />
<style type="text/css">
    .auto-style1 {
        width: 50%;
    }

    .auto-style2 {
        height: 20px;
    }

    .auto-style5 {
        width: 103px;
    }

    .auto-style6 {
        height: 20px;
        width: 103px;
    }

    .auto-style7 {
        height: 17px;
    }
</style>
<script>

</script>
<asp:Label ID="LblCat_Lo_Usuario" runat="server" Visible="false"></asp:Label>
<table align="center">
    <tr>
        <td>
            <table>
                <tr class="Titulos">
                    <td colspan="2">Información General</td>
                </tr>
                <tr>
                    <td>
                        <div id="pr">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="LblPR_CD_NOMBRE" runat="server" Text="Nombre" CssClass="LblDesc"></asp:Label></td>
                                    <td>
                                        <asp:TextBox ID="TxtPR_CD_NOMBRE" runat="server" MaxLength="100" ReadOnly="True" CssClass="TxtDesNS" Width="250px"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="LblPR_CD_CALLE_NUM" runat="server" Text="Calle Y Número" CssClass="LblDesc"></asp:Label></td>
                                    <td>
                                        <asp:TextBox ID="TxtPR_CD_CALLE_NUM" runat="server" MaxLength="100" ReadOnly="True" CssClass="TxtDesNS" Width="250px" BackColor="#D6D3CE"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="LblPR_CD_COLONIA" runat="server" Text="Colonia" CssClass="LblDesc"></asp:Label></td>
                                    <td>
                                        <asp:TextBox ID="TxtPR_CD_COLONIA" runat="server" MaxLength="100" ReadOnly="True" CssClass="TxtDesNS" Width="250px" BackColor="#D6D3CE"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="LblPR_CD_DEL_MPIO" runat="server" Text="Del/Muni" CssClass="LblDesc"></asp:Label></td>
                                    <td>
                                        <asp:TextBox ID="TxtPR_CD_DEL_MPIO" runat="server" MaxLength="100" ReadOnly="True" CssClass="TxtDesNS" Width="250px" BackColor="#D6D3CE"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="LblPR_CD_CP" runat="server" Text="C.P." CssClass="LblDesc"></asp:Label></td>
                                    <td>
                                        <asp:TextBox ID="TxtPR_CD_CP" runat="server" MaxLength="100" ReadOnly="True" CssClass="TxtDesNS" Width="158px" BackColor="#D6D3CE"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="LblPR_CD_ESTADO" runat="server" Text="Estado" CssClass="LblDesc"></asp:Label></td>
                                    <td>
                                        <asp:TextBox ID="TxtPR_CD_ESTADO" runat="server" MaxLength="100" ReadOnly="True" CssClass="TxtDesNS" Width="250px" BackColor="#D6D3CE"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="LblPR_CD_MAIL" runat="server" Text="Mail" CssClass="LblDesc"></asp:Label>

                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtPR_CD_EMAIL" runat="server" MaxLength="100" ReadOnly="True" CssClass="TxtDesNS" Width="250px" BackColor="#D6D3CE"></asp:TextBox>

                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="LblPR_CD_DTE_NACIMIENTO" runat="server" Text="Fecha De Nacimiento" CssClass="LblDesc"></asp:Label>

                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtPR_CD_DTE_NACIMIENTO" runat="server" MaxLength="100" ReadOnly="True" CssClass="TxtDesNS" Width="250px" BackColor="#D6D3CE"></asp:TextBox>

                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
            </table>
        </td>
        <td>
            <table>
                <tr>
                    <td colspan="2" class="Titulos">Información Responsable de Pago
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LblPR_AV_RESP_PAGO" runat="server" Text="Responsable De Pago" CssClass="LblDesc"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="TxtPR_AV_RESP_PAGO" runat="server" MaxLength="100" CssClass="TxtDesNS" Width="250px" BackColor="#D6D3CE" Height="21px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LblPR_AV_TELS_CASA_PR" runat="server" Text="Tel Casa" CssClass="LblDesc"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="TxtPR_AV_TELS_CASA_PR" runat="server" MaxLength="100" CssClass="TxtDesNS" Width="250px" BackColor="#D6D3CE" Height="21px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LblPR_AV_TELS_CEL_PR" runat="server" Text="Tel Cel" CssClass="LblDesc"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="TxtPR_AV_TELS_CEL_PR" runat="server" MaxLength="25" CssClass="TxtDesNS" Width="250px" Height="21px" BackColor="#D6D3CE"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LblPR_AV_PARENTESCO_RP" runat="server" Text="Parentesco" CssClass="LblDesc"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="TxtPR_AV_PARENTESCO_RP" runat="server" MaxLength="15"  CssClass="TxtDesNS" Width="250px" BackColor="#D6D3CE" Height="21px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LblPR_AV_MAIL_PR" runat="server" Text="Email" CssClass="LblDesc"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="TxtPR_AV_MAIL_PR" runat="server" MaxLength="80" CssClass="TxtDesNS" Width="250px" BackColor="#D6D3CE" Height="21px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LblPR_CA_ENCONTRATO" runat="server" Text="Figura En El Contrato" CssClass="LblDesc"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="TxtPR_CA_ENCONTRATO" runat="server" MaxLength="80" ReadOnly="True" CssClass="TxtDesNS" Width="250px" BackColor="#D6D3CE" Height="21px"></asp:TextBox></td>
                </tr>
            </table>
        </td>
    </tr>
    <tr class="Arriba">
        <td colspan="2">
            <table class="Table">
                <tr>
                    <td colspan="6" class="Titulos">Información Financiera</td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LblPR_CA_UNIVERSIDAD" runat="server" Text="Universidad" CssClass="LblDesc"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="TxtPR_CA_UNIVERSIDAD" runat="server" MaxLength="60" ReadOnly="True" CssClass="TxtDesNS" Width="150px"></asp:TextBox>
                    </td>
                    <td class="auto-style5">
                        <asp:Label ID="LblPR_CA_CAMPANA" runat="server" Text="Campaña" CssClass="LblDesc"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="TxtPR_CA_CAMPANA" runat="server" MaxLength="15" ReadOnly="True" CssClass="TxtDesNS" Width="150px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="LblPR_CF_SALDO_VEN" runat="server" Text="Saldo Vencido" CssClass="LblDesc"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TxtPR_CF_SALDO_VEN" runat="server" MaxLength="22" ReadOnly="True" CssClass="TxtDesNS" Width="150px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LblPR_CA_MATRICULA" runat="server" Text="Matrícula" CssClass="LblDesc"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="TxtPR_CA_MATRICULA" runat="server" MaxLength="25" ReadOnly="True" CssClass="TxtDesNS" Width="150px"></asp:TextBox></td>
                    <td class="auto-style5">
                        <asp:Label ID="LblPR_CD_CARTERA" runat="server" Text="Cartera" CssClass="LblDesc"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="TxtPR_CD_CARTERA" runat="server" MaxLength="60" ReadOnly="True" CssClass="TxtDesNS" Width="150px"></asp:TextBox></td>
                    <td>
                        <asp:Label ID="LblPR_CF_LATE_FEE" runat="server" Text="Late Fee" CssClass="LblDesc"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="TxtPR_CF_LATE_FEE" runat="server" MaxLength="22" ReadOnly="True" CssClass="TxtDesNS" Width="150px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LblPR_CA_SIS_ADM" runat="server" Text="Sistema de administración" CssClass="LblDesc"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TxtPR_CA_SIS_ADM" runat="server" MaxLength="8" ReadOnly="True" CssClass="TxtDesNS" Width="150px"></asp:TextBox></td>
                    <td class="auto-style5">
                        <asp:Label ID="LblPR_CF_PORCENTAJE" runat="server" Text="Porcentaje" CssClass="LblDesc"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="TxtPR_CF_PORCENTAJE" runat="server" MaxLength="8" ReadOnly="True" CssClass="TxtDesNS" Width="150px"></asp:TextBox></td>
                    <td>
                        <asp:Label ID="LblPR_CF_SVLF" runat="server" Text="Sv+Lf" CssClass="LblDesc"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="TxtPR_CF_SVLF" runat="server" MaxLength="22" ReadOnly="True" CssClass="TxtDesNS" Width="150px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LblPR_CA_SUCURSAL" runat="server" Text="Sucursal" CssClass="LblDesc"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TxtPR_CA_SUCURSAL" runat="server" MaxLength="22" ReadOnly="True" CssClass="TxtDesNS" Width="150px"></asp:TextBox>
                    </td>
                    <td class="auto-style5">
                        <asp:Label ID="LblPR_CF_BUCKET" runat="server" Text="Bucket" CssClass="LblDesc"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="TxtPR_CF_BUCKET" runat="server" MaxLength="8" ReadOnly="True" CssClass="TxtDesNS" Width="150px"></asp:TextBox></td>
                    <td>
                        <asp:Label ID="LblPR_CF_SALDO_TOT" runat="server" Text="Saldo Total" CssClass="LblDesc"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="TxtPR_CF_SALDO_TOT" runat="server" MaxLength="22" ReadOnly="True" CssClass="TxtDesNS" Width="150px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LblPR_CA_ESTATUS_AC" runat="server" Text="Estatus Académico" CssClass="LblDesc"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="TxtPR_CA_ESTATUS_AC" runat="server" MaxLength="20" ReadOnly="True" CssClass="TxtDesNS" Width="150px" BackColor="#D6D3CE"></asp:TextBox></td>
                    <td class="auto-style5">
                        <asp:Label ID="LblPR_CA_ESTATUS_GE" runat="server" Text="Estatus De Gestión" CssClass="LblDesc"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="TxtPR_CA_ESTATUS_GE" runat="server" MaxLength="25" ReadOnly="True" CssClass="TxtDesNS" Width="150px"></asp:TextBox></td>
                    <td>
                        <asp:Label ID="LblPR_CF_FONDOS_CON" runat="server" Text="Fondos De Contingencia" CssClass="LblDesc"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="TxtPR_CF_FONDOS_CON" runat="server" MaxLength="22" ReadOnly="True" CssClass="TxtDesNS" Width="150px"></asp:TextBox></td>
                </tr>
                <tr>
                    
                    <td class="auto-style5">
                        <asp:Label ID="LblPR_CF_DTE_ULTPAGO" runat="server" Text="Fec Ultimo Pago" CssClass="LblDesc"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="TxtPR_CF_DTE_ULTPAGO" runat="server" MaxLength="7" ReadOnly="True" CssClass="TxtDesNS" Width="150px"></asp:TextBox></td>
                    <td>
                        <asp:Label ID="LblPR_CF_STFONDOS" runat="server" Text="Saldo Total + Fondos" CssClass="LblDesc"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="TxtPR_CF_STFONDOS" runat="server" MaxLength="22" ReadOnly="True" CssClass="TxtDesNS" Width="150px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LblPR_CF_FIRMA_CONTRATO" runat="server" Text="Fec Firma Contrato" CssClass="LblDesc"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="TxtPR_CF_FIRMA_CONTRATO" runat="server" MaxLength="15" ReadOnly="True" CssClass="TxtDesNS" Width="150px"></asp:TextBox></td>
                    <td class="auto-style5">
                        <asp:Label ID="LblPR_CF_MTO_ULTPAGO" runat="server" Text="Monto De Último Pago" CssClass="LblDesc"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="TxtPR_CF_MTO_ULTPAGO" runat="server" MaxLength="22" ReadOnly="True" CssClass="TxtDesNS" Width="150px"></asp:TextBox></td>
                    <td>
                        <asp:Label ID="LblPR_CF_PAGO_MENSUAL" runat="server" Text="Pago Mensual" CssClass="LblDesc"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="TxtPR_CF_PAGO_MENSUAL" runat="server" MaxLength="22" ReadOnly="True" CssClass="TxtDesNS" Width="150px" BackColor="#D6D3CE"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LblPR_CF_BANCO" runat="server" Text="Banco" CssClass="LblDesc"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="TxtPR_CF_BANCO" runat="server" MaxLength="15" ReadOnly="True" CssClass="TxtDesNS" Width="150px"></asp:TextBox></td>
                    <td class="auto-style5">
                        <asp:Label ID="LblPR_CF_DTE_PROXPAGO" runat="server" Text="Fec De Próximo Pago" CssClass="LblDesc"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="TxtPR_CF_DTE_PROXPAGO" runat="server" MaxLength="7" ReadOnly="True" CssClass="TxtDesNS" Width="150px"></asp:TextBox></td>
                    <td>
                        <asp:Label ID="LblPR_CA_ESQUEMA_SAL" runat="server" Text="Esquema De Salida" CssClass="LblDesc"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="TxtPR_CA_ESQUEMA_SAL" runat="server" MaxLength="25" ReadOnly="True" CssClass="TxtDesNS" Width="150px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LblPR_CF_REF_BANCO" runat="server" Text="Referencia Bancaria" CssClass="LblDesc"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="TxtPR_CF_REF_BANCO" runat="server" MaxLength="15" ReadOnly="True" CssClass="TxtDesNS" Width="150px"></asp:TextBox></td>
                    <td class="auto-style5">
                        <asp:Label ID="LblPR_CA_DTE_ULTPP" runat="server" Text="Fec Última Pp" CssClass="LblDesc"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="TxtPR_CA_DTE_ULTPP" runat="server" MaxLength="7" ReadOnly="True" CssClass="TxtDesNS" Width="150px"></asp:TextBox></td>
                    <td>
                        <asp:Label ID="LblPR_CA_ESTATUS_CTA" runat="server" Text="Estatus De Cuenta" CssClass="LblDesc"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="TxtPR_CA_ESTATUS_CTA" runat="server" MaxLength="15" ReadOnly="True" CssClass="TxtDesNS" Width="150px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LblPR_CF_CONVENIO_CTA" runat="server" Text="Convenio/Cuenta" CssClass="LblDesc"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="TxtPR_CF_CONVENIO_CTA" runat="server" MaxLength="15" ReadOnly="True" CssClass="TxtDesNS" Width="150px"></asp:TextBox></td>
                    <td class="auto-style6">
                        <asp:Label ID="LblPR_CA_MTO_ULTPP" runat="server" Text="Monto Última Pp" CssClass="LblDesc"></asp:Label></td>
                    <td class="auto-style2">
                        <asp:TextBox ID="TxtPR_CA_MTO_ULTPP" runat="server" MaxLength="22" ReadOnly="True" CssClass="TxtDesNS" Width="150px"></asp:TextBox></td>
                    <td class="auto-style2">
                        <asp:Label ID="LblPR_MC_PRODUCTO" runat="server" Text="Producto" CssClass="LblDesc"></asp:Label>
                    </td>
                    <td class="auto-style2">
                        <asp:TextBox ID="TxtPR_MC_PRODUCTO" runat="server" MaxLength="15" ReadOnly="True" CssClass="TxtDesNS" Width="150px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LblPR_CA_US_ASIGNADO" runat="server" Text="Asignado A" CssClass="LblDesc"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="TxtPR_CA_US_ASIGNADO" runat="server" MaxLength="60" ReadOnly="True" CssClass="TxtDesNS" Width="150px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td colspan="6" style="text-align: center" class="auto-style7">
                        <asp:Button ID="BtnModificar" runat="server" Text="Modificar Datos" CssClass="Botones" Visible="false" />
                    </td>
                </tr>
            </table>

    </tr>
    <tr>
        <td colspan="2">
            <div>
                <asp:GridView ID="GvHistActResumido" runat="server" CssClass="mGrid" Font-Names="Tahoma" Font-Size="Small" Visible="false">
                </asp:GridView>
            </div>
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
