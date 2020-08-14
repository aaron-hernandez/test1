Imports System.Data.OracleClient
Imports System.Data
Imports Db
Imports Funciones
Imports System.Web.Services
Imports System.Globalization
Imports Busquedas
Imports Class_InformacionAdicional
Imports System.Web.Script.Serialization

Partial Class Estilos_InformacionAdicional
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
        Try
            Dim USR As String = (CType(Session("Usuario"), USUARIO)).CAT_LO_USUARIO
        Catch ex As Exception
            Session.Clear()
            Session.Abandon()
            Response.Redirect("LogIn.aspx")
        End Try
        Try
            Dim USUARIO As USUARIO = CType(Session("Usuario"), USUARIO)
            LblCat_Lo_Usuario.Text = USUARIO.CAT_LO_USUARIO
            Dim TmpCredito As Credito = CType(Session("Credito"), Credito)
        Catch ex As Exception
            SendMail("Page_PreInit", ex, "", "", "")
        End Try
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If ValidaLicencias(300, LblCat_Lo_Usuario.Text, "", "", 1, "") = 1 Then
                If Val(CType(Session("Usuario"), USUARIO).CAT_LO_MGESTION) = 0 Then
                    Response.Redirect("LogIn.aspx")
                Else
                    If Not IsPostBack Then
                        If CType(Session("Credito"), credito).PR_MC_CREDITO <> "" Then
                            OcultarMostrarInformacionAdicional(0)
                            CreditoRetirado(CType(Session("credito"), credito).PR_MC_ESTATUS)
                        End If
                    End If
                End If
            Else
                Session.Clear()
                Session.Abandon()
                Response.Redirect("ExpiroSesion.aspx")
            End If
        Catch ex As Exception
            SendMail("Page_Load", ex, "", "", LblCat_Lo_Usuario.Text)
        End Try
    End Sub
    
    Sub OcultarMostrarInformacionAdicional(ByVal V_Bandera As Integer)
        PnlTelefonos.Visible = False
        PnlDirecciones.Visible = False
        PnlCorreos.Visible = False
        UpnlAddTelefonos.Visible = False
        UpnlAddDirecciones.Visible = False
        UpnlAddCorreos.Visible = False
        UpnlOpcionesAdd.Visible = False
        If V_Bandera = 0 Then
            UpnlOpcionesAdd.Visible = True
            PnlTelefonos.Visible = True
            PnlDirecciones.Visible = True
            PnlCorreos.Visible = True
            GvTelefonos.DataSource = Class_InformacionAdicional.LlenarElementosAgregar(CType(Session("Credito"), Credito).PR_MC_CREDITO, 1)
            GvTelefonos.DataBind()
            GvDirecciones.DataSource = Class_InformacionAdicional.LlenarElementosAgregar(CType(Session("Credito"), Credito).PR_MC_CREDITO, 2)
            GvDirecciones.DataBind()
            GvCorreos.DataSource = Class_InformacionAdicional.LlenarElementosAgregar(CType(Session("Credito"), Credito).PR_MC_CREDITO, 3)
            GvCorreos.DataBind()
            'Dim DtstIPO_700 As DataSet = Class_InformacionAdicional.LlenarElementosAgregar("700", 4)
            'DdlHist_Te_Tipo.DataTextField = "Descripcion"
            'DdlHist_Te_Tipo.DataValueField = "Descripcion"
            'DdlHist_Te_Tipo.DataSource = DtstIPO_700
            'DdlHist_Te_Tipo.DataBind()
            'DdlHist_Te_Tipo.Items.Add("Seleccione")
            'DdlHist_Te_Tipo.SelectedValue = "Seleccione"

            'Dim DtstIPO_800 As DataSet = Class_InformacionAdicional.LlenarElementosAgregar("800", 4)
            'DdlHist_Di_Parentesco.DataTextField = "Descripcion"
            'DdlHist_Di_Parentesco.DataValueField = "Descripcion"
            'DdlHist_Di_Parentesco.DataSource = DtstIPO_800
            'DdlHist_Di_Parentesco.DataBind()
            'DdlHist_Di_Parentesco.Items.Add("Seleccione")
            'DdlHist_Di_Parentesco.SelectedValue = "Seleccione"
        ElseIf V_Bandera = 1 Then
            UpnlAddTelefonos.Visible = True
        ElseIf V_Bandera = 2 Then
            UpnlAddDirecciones.Visible = True
        ElseIf V_Bandera = 3 Then
            UpnlAddCorreos.Visible = True
        End If
    End Sub

    Sub CreditoRetirado(ByVal V_Estatus As String)
        Dim TmpUsr As USUARIO = CType(Session("Usuario"), USUARIO)
        If (V_Estatus = "Retirada") Or (V_Estatus = "Liquidada") Then
            BtnAgregarTelefonos.Visible = False
            BtnAgregarDirecciones.Visible = False
            BtnAgregarCorreos.Visible = False
        Else
            If TmpUsr.CAT_LO_PGESTION.ToString.Substring(0, 1) = 0 Then ' Adicionales
                BtnAgregarTelefonos.Visible = False
                BtnAgregarDirecciones.Visible = False
                BtnAgregarCorreos.Visible = False
                GvTelefonos.Enabled = False
                GvDirecciones.Enabled = False
                GvCorreos.Enabled = False
            End If
        End If
    End Sub

    Protected Sub BtnAgregarTelefonos_Click(sender As Object, e As EventArgs) Handles BtnAgregarTelefonos.Click
        LblTelefono.Text = "Agregar Teléfono"
        LimpiarInformacionAdicional(1)
        OcultarMostrarInformacionAdicional(1)
        BtnAddTel.Text = "Agregar"
        GvDiasTel.DataSource = Class_InformacionAdicional.LlenarElementosAgregar(CType(Session("Credito"), Credito).PR_MC_CREDITO, 0)
        GvDiasTel.DataBind()
    End Sub

    Protected Sub BtnAgregarDirecciones_Click(sender As Object, e As EventArgs) Handles BtnAgregarDirecciones.Click
        LblDireccion.Text = "Agregar Dirección"
        LimpiarInformacionAdicional(2)
        OcultarMostrarInformacionAdicional(2)
        BtnAddDir.Text = "Agregar"
        GvDiasDir.DataSource = Class_InformacionAdicional.LlenarElementosAgregar(CType(Session("Credito"), Credito).PR_MC_CREDITO, 0)
        GvDiasDir.DataBind()
    End Sub

    Protected Sub BtnAgregarCorreos_Click(sender As Object, e As EventArgs) Handles BtnAgregarCorreos.Click
        LblCorreo.Text = "Agregar Correo"
        LimpiarInformacionAdicional(3)
        OcultarMostrarInformacionAdicional(3)
        BtnAddCorreo.Text = "Agregar"
    End Sub

    Protected Sub BtnAddTel_Click(sender As Object, e As EventArgs) Handles BtnAddTel.Click
        Dim TmpCredito As Credito = CType(Session("Credito"), Credito)
        Dim TmpUsuario As USUARIO = CType(Session("USUARIO"), USUARIO)
        LblMsj.Text = Class_InformacionAdicional.AgregarTelefono(LblHist_Te_Consecutivo.Text, TmpCredito.PR_MC_CREDITO, TmpCredito.PR_MC_PRODUCTO, TxtHist_Te_Lada.Text, TxtHist_Te_Numerotel.Text, DdlHist_Te_Tipo.SelectedValue, DdlHist_Te_Parentesco.SelectedValue, TxtHist_Te_Nombre.Text, TxtHist_Te_Extension.Text, TxtHist_Te_Horario0.Text, TxtHist_Te_Horario1.Text, TmpUsuario.CAT_LO_USUARIO, TmpCredito.PR_MC_AGENCIA, "Captura", BtnAddTel, GvDiasTel, TxtHist_Te_Proporciona.Text)
        If LblMsj.Text = "1" Then
            LblMsj.Text = "Teléfono Agregado"
            MpuMensajes.Show()
            OcultarMostrarInformacionAdicional(0)
        ElseIf LblMsj.Text = "0" Then
            LblMsj.Text = "Teléfono Actualizado"
            MpuMensajes.Show()
            OcultarMostrarInformacionAdicional(0)
        Else
            MpuMensajes.Show()
        End If
    End Sub
    Protected Sub BtnCancelAddTel_Click(sender As Object, e As EventArgs) Handles BtnCancelAddTel.Click
        OcultarMostrarInformacionAdicional(0)
    End Sub

    Protected Sub GvTelefonos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GvTelefonos.SelectedIndexChanged
        Dim row As GridViewRow = GvTelefonos.SelectedRow
        LblTelefono.Text = "Modificar Teléfono"
        LimpiarInformacionAdicional(1)
        OcultarMostrarInformacionAdicional(1)
        BtnAddTel.Text = "Modificar"
        GvDiasTel.DataSource = Class_InformacionAdicional.LlenarElementosAgregar(CType(Session("Credito"), Credito).PR_MC_CREDITO, 0)
        GvDiasTel.DataBind()
        Dim arrayDias() As String = Strings.Split(row.Cells(6).Text.Replace(" ", ""), ",")
        Dim Cuantos As Integer = arrayDias.Count()
        If arrayDias(0) <> "&nbsp;" Then
            For x As Integer = 0 To Cuantos - 1
                For Each gvRow As GridViewRow In GvDiasTel.Rows
                    Dim chkSel As CheckBox = DirectCast(gvRow.FindControl("chkSelect"), CheckBox)
                    If arrayDias(x) = gvRow.Cells(1).Text Then
                        chkSel.Checked = True
                        Exit For
                    End If
                Next
            Next
        End If
        LblHist_Te_Consecutivo.Text = row.Cells(1).Text
        TxtHist_Te_Lada.Text = row.Cells(2).Text
        TxtHist_Te_Numerotel.Text = row.Cells(3).Text

        DdlHist_Te_Tipo.SelectedValue = row.Cells(5).Text
        If row.Cells(5).Text = "Oficina" Then
            LblHist_Te_Extension.Visible = True
            TxtHist_Te_Extension.Visible = True
            TxtHist_Te_Extension.Text = row.Cells(4).Text
        End If
        If row.Cells(9).Text <> "Cliente" Then
            TxtHist_Te_Nombre.Visible = True
            LblHist_Te_Nombre.Visible = True
            TxtHist_Te_Nombre.Text = HttpUtility.HtmlDecode(row.Cells(10).Text)
        End If
        DdlHist_Te_Parentesco.SelectedValue = HttpUtility.HtmlDecode(row.Cells(9).Text)
        TxtHist_Te_Horario0.Text = row.Cells(7).Text
        TxtHist_Te_Horario1.Text = row.Cells(8).Text
    End Sub
    Protected Sub BtnAddDir_Click(sender As Object, e As EventArgs) Handles BtnAddDir.Click
        Dim TmpCredito As Credito = CType(Session("Credito"), Credito)
        Dim TmpUsuario As USUARIO = CType(Session("USUARIO"), USUARIO)
        LblMsj.Text = Class_InformacionAdicional.AgregarDireccion(LblHist_Di_Consecutivo.Text, TmpCredito.PR_MC_CREDITO, TmpCredito.PR_MC_PRODUCTO, TxtHist_Di_Calle.Text, DdlHist_Di_Colonia.SelectedValue, TxtHist_Di_Muni.Text, TxtHist_Di_Ciudad.Text, TxtHist_Di_Estado.Text, TxtHist_Di_Cp.Text, TxtHist_Di_Numext.Text, TxtHist_Di_Numint.Text, DdlHist_Di_Parentesco.SelectedValue, TxtHist_Di_Nombre.Text, TmpUsuario.CAT_LO_USUARIO, TmpCredito.PR_MC_AGENCIA, "Captura", TxtHist_Di_Horario0.Text, TxtHist_Di_Horario1.Text, Boleano(CbxHist_Di_Contacto.Checked), BtnAddDir, GvDiasDir, TxtHist_Di_Proporciona.Text)
        If LblMsj.Text = "1" Then
            LblMsj.Text = "Dirección Agregada"
            MpuMensajes.Show()
            OcultarMostrarInformacionAdicional(0)
        ElseIf LblMsj.Text = "0" Then
            LblMsj.Text = "Dirección Actualizada"
            MpuMensajes.Show()
            OcultarMostrarInformacionAdicional(0)
        Else
            MpuMensajes.Show()
        End If
    End Sub

    Protected Sub TxtHist_Di_Cp_TextChanged(sender As Object, e As EventArgs) Handles TxtHist_Di_Cp.TextChanged
        Dim DtsDatosCp As DataSet
        Dim oraCommand As New OracleCommand
        oraCommand.CommandText = "SP_COMPLETA_BUSQUEDA"
        oraCommand.CommandType = CommandType.StoredProcedure
        oraCommand.Parameters.Add("V_PATRON", OracleType.VarChar).Value = TxtHist_Di_Cp.Text 'TxtHist_Di_Cp.Text & "%"
        oraCommand.Parameters.Add("V_BANDERA", OracleType.VarChar).Value = 1
        oraCommand.Parameters.Add("cv_1", OracleType.Cursor).Direction = ParameterDirection.Output
        DtsDatosCp = Consulta_Procedure(oraCommand, "BUSQUEDA")

        Dim Source As DataView
        Source = DtsDatosCp.Tables("BUSQUEDA").DefaultView
        If Source.Count <> 0 Then
            DdlHist_Di_Colonia.DataTextField = "CAT_SE_COLONIA"
            DdlHist_Di_Colonia.DataValueField = "CAT_SE_COLONIA"
            DdlHist_Di_Colonia.DataSource = DtsDatosCp
            DdlHist_Di_Colonia.DataBind()
            TxtHist_Di_Ciudad.Text = DtsDatosCp.Tables(0).Rows(0)("CAT_SE_CIUDAD")
            TxtHist_Di_Estado.Text = DtsDatosCp.Tables(0).Rows(0)("CAT_SE_ESTADO")
            TxtHist_Di_Muni.Text = DtsDatosCp.Tables(0).Rows(0)("CAT_SE_MUNICIPIO")
        End If
    End Sub

    Protected Sub GvDirecciones_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GvDirecciones.SelectedIndexChanged
        Dim row As GridViewRow = GvDirecciones.SelectedRow
        LblTelefono.Text = "Modificar Dirección"
        LimpiarInformacionAdicional(2)
        OcultarMostrarInformacionAdicional(2)
        BtnAddDir.Text = "Modificar"
        GvDiasDir.DataSource = Class_InformacionAdicional.LlenarElementosAgregar(CType(Session("Credito"), Credito).PR_MC_CREDITO, 0)
        GvDiasDir.DataBind()
        Dim arrayDias() As String = Strings.Split(row.Cells(14).Text.Replace(" ", ""), ",")
        Dim Cuantos As Integer = arrayDias.Count()
        If arrayDias(0) <> "&nbsp;" Then
            For x As Integer = 0 To Cuantos - 1
                For Each gvRow As GridViewRow In GvDiasDir.Rows
                    Dim chkSel As CheckBox = DirectCast(gvRow.FindControl("chkSelect"), CheckBox)
                    If arrayDias(x) = gvRow.Cells(1).Text Then
                        chkSel.Checked = True
                        Exit For
                    End If
                Next
            Next
        End If
        LblHist_Di_Consecutivo.Text = HttpUtility.HtmlDecode(row.Cells(1).Text)
        TxtHist_Di_Cp.Enabled = False
        TxtHist_Di_Cp.Text = HttpUtility.HtmlDecode(row.Cells(2).Text)
        TxtHist_Di_Ciudad.Text = HttpUtility.HtmlDecode(row.Cells(3).Text)
        TxtHist_Di_Estado.Text = HttpUtility.HtmlDecode(row.Cells(4).Text)
        TxtHist_Di_Muni.Text = HttpUtility.HtmlDecode(row.Cells(5).Text)
        DdlHist_Di_Colonia.Items.Add(HttpUtility.HtmlDecode(row.Cells(6).Text))
        DdlHist_Di_Colonia.DataBind()
        TxtHist_Di_Calle.Text = HttpUtility.HtmlDecode(row.Cells(7).Text)
        TxtHist_Di_Numext.Text = HttpUtility.HtmlDecode(row.Cells(8).Text)
        TxtHist_Di_Numint.Text = HttpUtility.HtmlDecode(row.Cells(9).Text)
        DdlHist_Di_Parentesco.SelectedValue = HttpUtility.HtmlDecode(row.Cells(10).Text)
        TxtHist_Di_Horario0.Text = HttpUtility.HtmlDecode(row.Cells(11).Text)
        TxtHist_Di_Horario1.Text = HttpUtility.HtmlDecode(row.Cells(12).Text)
        If row.Cells(10).Text <> "Cliente" Then
            TxtHist_Di_Nombre.Visible = True
            LblHist_Di_Nombre.Visible = True
            TxtHist_Di_Nombre.Text = HttpUtility.HtmlDecode(row.Cells(13).Text)
        End If
        If row.Cells(15).Text = "Si" Then
            CbxHist_Di_Contacto.Checked = True
        End If

    End Sub

    Protected Sub DdlHist_Di_Parentesco_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlHist_Di_Parentesco.SelectedIndexChanged
        If DdlHist_Di_Parentesco.SelectedValue <> "Cliente" Then
            LblHist_Di_Nombre.Visible = True
            TxtHist_Di_Nombre.Visible = True
        Else
            LblHist_Di_Nombre.Visible = False
            TxtHist_Di_Nombre.Visible = False
            TxtHist_Di_Nombre.Text = ""
        End If
    End Sub

    'Correos

    Protected Sub DdlHist_Co_Parentesco_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlHist_Co_Parentesco.SelectedIndexChanged
        If DdlHist_Co_Parentesco.SelectedValue <> "Cliente" Then
            LblHist_Co_Nombre.Visible = True
            TxtHist_Co_Nombre.Visible = True
        Else
            LblHist_Co_Nombre.Visible = False
            TxtHist_Co_Nombre.Visible = False
            TxtHist_Co_Nombre.Text = ""
        End If
    End Sub

    Protected Sub BtnCancelAddCorreo_Click(sender As Object, e As EventArgs) Handles BtnCancelAddCorreo.Click
        OcultarMostrarInformacionAdicional(0)
    End Sub

    Protected Sub BtnAddCorreo_Click(sender As Object, e As EventArgs) Handles BtnAddCorreo.Click
        Dim TmpCredito As Credito = CType(Session("Credito"), Credito)
        Dim TmpUsuario As USUARIO = CType(Session("USUARIO"), USUARIO)
        LblMsj.Text = Class_InformacionAdicional.AgregarCorreo(LblHist_Co_Consecutivo.Text, TmpCredito.PR_MC_CREDITO, TmpCredito.PR_MC_PRODUCTO, DdlHist_Co_Parentesco.SelectedValue, TxtHist_Co_Nombre.Text, TxtHist_Co_Correo.Text, Boleano(CbxHist_Co_Contacto.Checked), TmpUsuario.CAT_LO_USUARIO, TmpCredito.PR_MC_AGENCIA, "Captura", DdlHist_Co_Tipo.SelectedValue, BtnAddCorreo, TxtHist_Co_Proporciona.Text)
        If LblMsj.Text = "1" Then

            LblMsj.Text = "Correo Agregado"
            MpuMensajes.Show()
            OcultarMostrarInformacionAdicional(0)
        ElseIf LblMsj.Text = "0" Then

            LblMsj.Text = "Correo Actualizado"
            MpuMensajes.Show()
            OcultarMostrarInformacionAdicional(0)
        Else
            MpuMensajes.Show()
        End If
        'Response.Redirect("MasterPage.aspx", False)
    End Sub

    Protected Sub GvCorreos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GvCorreos.SelectedIndexChanged
        Dim row As GridViewRow = GvCorreos.SelectedRow
        LblCorreo.Text = "Modificar Correo"
        LimpiarInformacionAdicional(3)
        OcultarMostrarInformacionAdicional(3)
        BtnAddCorreo.Text = "Modificar"

        LblHist_Co_Consecutivo.Text = row.Cells(1).Text
        DdlHist_Co_Tipo.SelectedValue = row.Cells(2).Text
        TxtHist_Co_Correo.Text = row.Cells(3).Text
        DdlHist_Co_Parentesco.SelectedValue = HttpUtility.HtmlDecode(row.Cells(4).Text)
        If row.Cells(4).Text <> "Cliente" Then
            TxtHist_Co_Nombre.Visible = True
            LblHist_Co_Nombre.Visible = True
            TxtHist_Co_Nombre.Text = HttpUtility.HtmlDecode(row.Cells(5).Text)
        End If
        If row.Cells(6).Text = "Si" Then
            CbxHist_Co_Contacto.Checked = True
        End If

    End Sub

    Sub LimpiarInformacionAdicional(ByVal V_Bandera As Integer)
        If V_Bandera = 1 Then
            DdlHist_Te_Tipo.SelectedValue = "Casa"
            TxtHist_Te_Lada.Text = ""
            TxtHist_Te_Numerotel.Text = ""
            TxtHist_Te_Nombre.Text = ""
            TxtHist_Te_Nombre.Visible = False
            LblHist_Te_Nombre.Visible = False
            TxtHist_Te_Extension.Text = ""
            TxtHist_Te_Extension.Visible = False
            LblHist_Te_Extension.Visible = False
            DdlHist_Te_Parentesco.SelectedValue = "Cliente"
        ElseIf V_Bandera = 2 Then
            LblHist_Di_Nombre.Visible = False
            TxtHist_Di_Nombre.Visible = False
            TxtHist_Di_Nombre.Text = ""
            DdlHist_Di_Colonia.Items.Clear()
            TxtHist_Di_Cp.Text = ""
            TxtHist_Di_Ciudad.Text = ""
            TxtHist_Di_Estado.Text = ""
            TxtHist_Di_Calle.Text = ""
            TxtHist_Di_Numext.Text = ""
            TxtHist_Di_Numint.Text = ""
            TxtHist_Di_Muni.Text = ""
            DdlHist_Di_Parentesco.SelectedValue = "Cliente"
            TxtHist_Di_Horario0.Text = ""
            TxtHist_Di_Horario1.Text = ""
            TxtHist_Di_Cp.Enabled = True
            CbxHist_Di_Contacto.Checked = False
        ElseIf V_Bandera = 3 Then
            LblHist_Co_Nombre.Visible = False
            TxtHist_Co_Nombre.Visible = False
            TxtHist_Co_Nombre.Text = ""
            DdlHist_Co_Tipo.SelectedValue = "Personal"
            TxtHist_Co_Correo.Text = ""
            DdlHist_Co_Parentesco.SelectedValue = "Cliente"
            CbxHist_Co_Contacto.Checked = False
        End If
    End Sub

    Protected Sub DdlHist_Te_Parentesco_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlHist_Te_Parentesco.SelectedIndexChanged
        If DdlHist_Te_Parentesco.SelectedValue <> "Cliente" Then
            LblHist_Te_Nombre.Visible = True
            TxtHist_Te_Nombre.Visible = True
        Else
            LblHist_Te_Nombre.Visible = False
            TxtHist_Te_Nombre.Visible = False
            TxtHist_Te_Nombre.Text = ""
        End If
    End Sub
    Protected Sub DdlHist_Te_Tipo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlHist_Te_Tipo.SelectedIndexChanged
        If DdlHist_Te_Tipo.SelectedValue <> "Oficina" Then
            LblHist_Te_Extension.Visible = False
            TxtHist_Te_Extension.Visible = False
            TxtHist_Te_Extension.Text = ""
        Else
            LblHist_Te_Extension.Visible = True
            TxtHist_Te_Extension.Visible = True
        End If
    End Sub
    Protected Sub BtnCancelAddDir_Click(sender As Object, e As EventArgs) Handles BtnCancelAddDir.Click
        OcultarMostrarInformacionAdicional(0)
    End Sub
    Private Sub SendMail(ByRef evento As String, ByVal ex As Exception, ByVal Cuenta As String, ByVal Captura As String, ByVal usr As String)
        EnviarCorreo("InformacionAdicional.aspx", evento, ex, Cuenta, Captura, usr)
    End Sub
End Class
