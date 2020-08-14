Imports System.Data.OracleClient
Imports System.Data
Imports Db
Imports Funciones
Imports System.Web.Services
Imports System.Globalization
Imports Busquedas
Partial Class Agenda
    Inherits System.Web.UI.Page
    <WebMethod(EnableSession:=True)> _
    Public Shared Function KeepActiveSession(ByVal Usuario As String) As String
        Dim DtsConectado As DataSet = Class_Sesion.LlenarElementos(Usuario, "", "", "", 3, "", "", "", "")
        If DtsConectado.Tables(0).Rows(0).Item("Cuantas") <> "0" Then
            Return "Hola"
        Else
            Return "Bye"
        End If
    End Function
    Protected Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        Try
            Dim USR As String = (CType(Session("Usuario"), USUARIO)).CAT_LO_USUARIO
        Catch ex As Exception
            Session.Clear()
            Session.Abandon()
            Response.Redirect("LogIn.aspx")
        End Try
        Try
            Dim USUARIO As USUARIO = CType(Session("Usuario"), USUARIO)
            LblCat_Lo_Nombre.Text = USUARIO.CAT_LO_NOMBRE
            LblCAT_PE_PERFIL.Text = USUARIO.CAT_PE_PERFIL
            LblCat_Lo_Usuario.Text = USUARIO.CAT_LO_USUARIO
            LblCat_Lo_Productos.Text = USUARIO.CAT_LO_PRODUCTOS
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
                    Dim TmpCrdt As credito = CType(Session("Credito"), credito)
                    If Not IsPostBack Then
                        Llenar()
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
    Sub Llenar()
        Dim USUARIO As USUARIO = CType(Session("Usuario"), USUARIO)
        GvAsignacion.DataSource = Class_Agenda.LlenarElementosAgenda("", USUARIO.CAT_LO_USUARIO, 8)
        GvAsignacion.DataBind()
        LblCat_Lo_Nombre.Text = USUARIO.CAT_LO_NOMBRE
        LblCAT_PE_PERFIL.Text = USUARIO.CAT_PE_PERFIL
        LblCat_Lo_Usuario.Text = USUARIO.CAT_LO_USUARIO
        LblCat_Lo_Productos.Text = USUARIO.CAT_LO_PRODUCTOS
        GvRDia.DataSource = Class_Agenda.LlenarElementosAgenda("", USUARIO.CAT_LO_USUARIO, 10)
        GvRDia.DataBind()
        GvRmes.DataSource = Class_Agenda.LlenarElementosAgenda("", USUARIO.CAT_LO_USUARIO, 11)
        GvRmes.DataBind()

        Dim DtsPromesasD As DataSet = Class_Agenda.LlenarElementosAgenda("", USUARIO.CAT_LO_USUARIO, 12)

        LblPPPD.Text = DtsPromesasD.Tables(0).Rows(0).Item("Cuantas") & " Promesas De Pago"
        LblMtoPPPD.Text = DtsPromesasD.Tables(0).Rows(0).Item("Monto")

        Dim DtsPromesasM As DataSet = Class_Agenda.LlenarElementosAgenda("", USUARIO.CAT_LO_USUARIO, 13)
        LblPPPM.Text = DtsPromesasM.Tables(0).Rows(0).Item("Cuantas") & " Promesas De Pago"
        LblMtoPPPM.Text = DtsPromesasM.Tables(0).Rows(0).Item("Monto")

        LblTrabajadas.Text = 0
        LblTrabajadasM.Text = 0

        For Each gvRow As GridViewRow In GvRDia.Rows
            LblTrabajadas.Text = Val(LblTrabajadas.Text) + HttpUtility.HtmlDecode(Val(gvRow.Cells(0).Text))
        Next
        For Each gvRow As GridViewRow In GvRmes.Rows
            LblTrabajadasM.Text = Val(LblTrabajadasM.Text) + HttpUtility.HtmlDecode(Val(gvRow.Cells(0).Text))
        Next
    End Sub

    Protected Sub chkSelect_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim chkTest As CheckBox = DirectCast(sender, CheckBox)
        Dim grdRow As GridViewRow = DirectCast(chkTest.NamingContainer, GridViewRow)
    End Sub
    Protected Sub chkSelectAll_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim chkAll As CheckBox = DirectCast(GridFilas.HeaderRow.FindControl("chkSelectAll"), CheckBox)
        If chkAll.Checked = True Then
            For Each gvRow As GridViewRow In GridFilas.Rows
                Dim chkSel As CheckBox = DirectCast(gvRow.FindControl("chkSelect"), CheckBox)
                chkSel.Checked = True
            Next
        Else
            For Each gvRow As GridViewRow In GridFilas.Rows
                Dim chkSel As CheckBox = DirectCast(gvRow.FindControl("chkSelect"), CheckBox)
                chkSel.Checked = False
            Next
        End If
    End Sub

    Protected Sub DrlFila_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DrlFila.SelectedIndexChanged
        Try
            GridFilas.DataSource = Nothing
            GridFilas.DataBind()
            Dim Bandera As Integer

            If DrlFila.SelectedValue <> "Seleccione" Then
                Select Case DrlFila.SelectedValue
                    Case Is = "Código Resultado"
                        Bandera = 0
                    Case Is = "Código Resultado Ponderado"
                        Bandera = 1
                    Case Is = "Promesas De Pago"
                        Bandera = 2
                    Case Is = "Semaforo"
                        Bandera = 3
                    Case Is = "Contactar Hoy"
                        Bandera = 4
                End Select
                Dim DTSFila As DataSet = Class_Agenda.LlenarElementosAgenda("", CType(Session("USUARIO"), USUARIO).CAT_LO_USUARIO, Bandera)
                Dim DTVFila As DataView = DTSFila.Tables("ELEMENTOS").DefaultView
                If DTVFila.Count > 0 Then
                    GridFilas.DataSource = DTSFila
                    GridFilas.DataBind()
                    BtnCrear.Visible = True
                    LblMsjFilas.Visible = False
                Else
                    GridFilas.DataSource = Nothing
                    GridFilas.DataBind()
                    BtnCrear.Visible = False
                    LblMsjFilas.Visible = True
                    LblMsjFilas.Text = "No Se Encontraron Resultados"
                End If
            End If
        Catch ex As Exception
            'SendMail("DrlFila_SelectedIndexChanged", ex, "", "", HidenUrs.Value)
        End Try
    End Sub
    Protected Sub BtnCrear_Click(sender As Object, e As System.EventArgs) Handles BtnCrear.Click
       LblMsj.Visible = True
        LblMsj.Text = ""
        Dim BANDERA As Integer = 0
        Dim Cuantos As Integer = 0
        For Each gvRow As GridViewRow In GridFilas.Rows
            Dim chkSel As CheckBox = DirectCast(gvRow.FindControl("chkSelect"), CheckBox)
            If chkSel.Checked = True Then
                BANDERA = BANDERA + 1
                Cuantos = Cuantos + gvRow.Cells(2).Text
            End If
        Next

        If BANDERA > 0 Then
            If Cuantos > 0 Then
                Try
                    Dim Cadena As String = ""
                    Dim oraCommand As New OracleCommand
                    oraCommand.CommandText = "SP_CREAR_FILA"
                    oraCommand.CommandType = CommandType.StoredProcedure
                    oraCommand.Parameters.Add("V_USUARIO", OracleType.VarChar).Value = (CType(Session("Usuario"), USUARIO)).CAT_LO_USUARIO

                    Select Case DrlFila.SelectedValue
                        Case Is = "Código Resultado"
                            For Each gvRow As GridViewRow In GridFilas.Rows
                                Dim chkSel As CheckBox = DirectCast(gvRow.FindControl("chkSelect"), CheckBox)
                                If chkSel.Checked = True Then
                                    Cadena = Cadena + "'" + HttpUtility.HtmlDecode(gvRow.Cells(1).Text) + "',"
                                End If
                            Next
                            oraCommand.Parameters.Add("V_Cadena", OracleType.VarChar).Value = Cadena.Substring(0, Len(Cadena) - 1)
                            oraCommand.Parameters.Add("v_Bandera", OracleType.VarChar).Value = 1
                        Case Is = "Promesas De Pago"
                            For Each gvRow As GridViewRow In GridFilas.Rows
                                Dim chkSel As CheckBox = DirectCast(gvRow.FindControl("chkSelect"), CheckBox)
                                If chkSel.Checked = True Then
                                    If HttpUtility.HtmlDecode(gvRow.Cells(1).Text) = "Promesas Incumplidas" Then
                                        Cadena = Cadena + "SELECT HIST_PR_CREDITO FROM HIST_PROMESAS WHERE HIST_PR_USUARIO ='" & (CType(Session("Usuario"), USUARIO)).CAT_LO_USUARIO & "' AND HIST_PR_ESTATUS ='Incumplida' AND TRUNC(HIST_PR_DTEPROMESA) < TRUNC(SYSDATE) And Hist_Pr_Credito Not In (Select Pr_Mc_Credito From Pr_Mc_Gral Where Pr_Mc_Modal=0 And Pr_Mc_Secfila != 0) UNION "
                                    ElseIf HttpUtility.HtmlDecode(gvRow.Cells(1).Text) = "Promesas Pendientes" Then
                                        Cadena = Cadena + "SELECT HIST_PR_CREDITO FROM HIST_PROMESAS WHERE HIST_PR_USUARIO ='" & (CType(Session("Usuario"), USUARIO)).CAT_LO_USUARIO & "' AND HIST_PR_ESTATUS ='Pendiente' AND TRUNC(HIST_PR_DTEPROMESA) >= TRUNC(SYSDATE) And Hist_Pr_Credito Not In (Select Pr_Mc_Credito From Pr_Mc_Gral Where Pr_Mc_Modal=0 And Pr_Mc_Secfila != 0) UNION "
                                    ElseIf HttpUtility.HtmlDecode(gvRow.Cells(1).Text) = "Promesas Vencen Hoy" Then
                                        Cadena = Cadena + "SELECT HIST_PR_CREDITO FROM HIST_PROMESAS WHERE HIST_PR_USUARIO ='" & (CType(Session("Usuario"), USUARIO)).CAT_LO_USUARIO & "' AND HIST_PR_ESTATUS ='Pendiente' AND TRUNC(HIST_PR_DTEPROMESA)=TRUNC(SYSDATE) And Hist_Pr_Credito Not In (Select Pr_Mc_Credito From Pr_Mc_Gral Where Pr_Mc_Modal=0 And Pr_Mc_Secfila != 0) UNION "
                                    ElseIf HttpUtility.HtmlDecode(gvRow.Cells(1).Text) = "Promesas Vencen Mañana" Then
                                        Cadena = Cadena + "SELECT HIST_PR_CREDITO FROM HIST_PROMESAS WHERE HIST_PR_USUARIO ='" & (CType(Session("Usuario"), USUARIO)).CAT_LO_USUARIO & "' AND HIST_PR_ESTATUS ='Pendiente' AND TRUNC(HIST_PR_DTEPROMESA)=TRUNC(SYSDATE + 1) And Hist_Pr_Credito Not In (Select Pr_Mc_Credito From Pr_Mc_Gral Where Pr_Mc_Modal=0 And Pr_Mc_Secfila != 0) UNION "
                                    ElseIf HttpUtility.HtmlDecode(gvRow.Cells(1).Text) = "Promesas Vencieron Ayer" Then
                                        Cadena = Cadena + "SELECT HIST_PR_CREDITO FROM HIST_PROMESAS WHERE HIST_PR_USUARIO ='" & (CType(Session("Usuario"), USUARIO)).CAT_LO_USUARIO & "' AND HIST_PR_ESTATUS ='Pendiente' AND TRUNC(HIST_PR_DTEPROMESA)=TRUNC(SYSDATE - 1) And Hist_Pr_Credito Not In (Select Pr_Mc_Credito From Pr_Mc_Gral Where Pr_Mc_Modal=0 And Pr_Mc_Secfila != 0) UNION "
                                    Else
                                        Cadena = Cadena + ""
                                    End If
                                End If
                            Next
                            oraCommand.Parameters.Add("V_Cadena", OracleType.VarChar).Value = Cadena.Substring(0, Len(Cadena) - 7)
                            oraCommand.Parameters.Add("v_Bandera", OracleType.VarChar).Value = 2
                        Case Is = "Contactar Hoy"
                            oraCommand.Parameters.Add("V_Cadena", OracleType.VarChar).Value = ""
                            oraCommand.Parameters.Add("v_Bandera", OracleType.VarChar).Value = 3
                        Case Is = "Semaforo"
                            For Each gvRow As GridViewRow In GridFilas.Rows
                                Dim chkSel As CheckBox = DirectCast(gvRow.FindControl("chkSelect"), CheckBox)
                                If chkSel.Checked = True Then
                                    Cadena = Cadena + "'" + gvRow.Cells(1).Text + "',"
                                End If
                            Next
                            oraCommand.Parameters.Add("V_Cadena", OracleType.VarChar).Value = Cadena.Substring(0, Len(Cadena) - 1)
                            oraCommand.Parameters.Add("v_Bandera", OracleType.VarChar).Value = 4
                        Case Is = "Código Resultado Ponderado"
                            For Each gvRow As GridViewRow In GridFilas.Rows
                                Dim chkSel As CheckBox = DirectCast(gvRow.FindControl("chkSelect"), CheckBox)
                                If chkSel.Checked = True Then
                                    Cadena = Cadena + "'" + HttpUtility.HtmlDecode(gvRow.Cells(1).Text) + "',"
                                End If
                            Next
                            oraCommand.Parameters.Add("V_Cadena", OracleType.VarChar).Value = Cadena.Substring(0, Len(Cadena) - 1)
                            oraCommand.Parameters.Add("v_Bandera", OracleType.VarChar).Value = 5
                    End Select
                    Ejecuta_Procedure(oraCommand)
                    Response.Redirect("MasterPage.aspx", False)
                Catch ex As System.Threading.ThreadAbortException
                Catch ex As Exception
                    LblMsjFilas.Visible = True
                    LblMsjFilas.Text = ex.ToString
                    SendMail("BtnCrear_Click", ex, "", "", HidenUrs.Value)
                End Try
            Else
                LblMsj.Text = "No Existen Créditos En La(s) Opcion(es) Seleccionada(s)"
                MpuMensajes.Show()
            End If
        Else
            LblMsj.Text = "Seleccione Una Opción Del Listado Para Crear La Fila De Trabajo"
            MpuMensajes.Show()
        End If
    End Sub
    Private Sub SendMail(ByRef evento As String, ByVal ex As Exception, ByVal Cuenta As String, ByVal Captura As String, ByVal usr As String)
        EnviarCorreo("Filas.ascx", evento, ex, Cuenta, Captura, usr)
    End Sub

    Protected Sub BtnRegresar_Click(sender As Object, e As EventArgs) Handles BtnRegresar.Click
        Response.Redirect("MasterPage.aspx")
    End Sub

    Protected Sub GvAsignacion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GvAsignacion.SelectedIndexChanged
        Try
            Dim USR As String = (CType(Session("Usuario"), USUARIO)).CAT_LO_USUARIO
        Catch ex As Exception
            Session.Clear()
            Session.Abandon()
            Response.Redirect("ExpiroSesion.aspx")
        End Try
        Try
            Dim row As GridViewRow = GvAsignacion.SelectedRow
            Class_MasterPage.Tocar(row.Cells(2).Text, LblCat_Lo_Usuario.Text)
            LlenarCredito.Busca(row.Cells(2).Text)
            Session("Buscar") = 1
            Dim Tmpcrdt As Credito = (CType(Session("Credito"), Credito))
            Tmpcrdt.PR_MC_CUENTATRABAJADAFILA = 1
            Response.Redirect("MasterPage.aspx", False)
        Catch ex As System.Threading.ThreadAbortException
        Catch ax As Exception
            SendMail("GvAsignacion_SelectedIndexChanged", ax, CType(Session("Credito"), Credito).PR_MC_CREDITO, "", CType(Session("Usuario"), USUARIO).CAT_LO_USUARIO)
        End Try
    End Sub
    Protected Sub SESION_Click(sender As Object, e As EventArgs) Handles SESION.Click
        ValidaLicencias(CType(Session("USUARIO"), USUARIO).Licencias, LblCat_Lo_Usuario.Text, "", DateTime.Now, 4, "")
        Session.Clear()
        Session.Abandon()
        Response.Redirect("Login.aspx", False)
    End Sub
End Class
