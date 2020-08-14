Imports System.Data.OracleClient
Imports System.Data
Imports Db
Imports Funciones

Partial Class LogIn
    Inherits System.Web.UI.Page
    Dim DtsVariable As DataSet
    Dim Campana As String = "LogIn"
    Dim Modulos As String = "Gestion"

    Sub Licencias(ByVal V_USUARIO As String, ByVal V_CAMPANA As String, ByVal V_MODULO As String, ByVal V_MOTIVO As String, ByVal V_BANDERA As String, ByVal V_CONTRASENA As String, ByVal V_IP As String, ByVal V_EVENTO As String, ByVal V_HIST_LO_ID_LOGIN As String)
        Dim oraCommand As New OracleCommand
        oraCommand.CommandText = "SP_INGRESO"
        oraCommand.CommandType = CommandType.StoredProcedure
        oraCommand.Parameters.Add("V_USUARIO", OracleType.VarChar).Value = V_USUARIO
        oraCommand.Parameters.Add("V_CAMPANA", OracleType.VarChar).Value = V_CAMPANA
        oraCommand.Parameters.Add("V_MODULO", OracleType.VarChar).Value = V_MODULO
        oraCommand.Parameters.Add("V_MOTIVO", OracleType.VarChar).Value = V_MOTIVO
        oraCommand.Parameters.Add("V_BANDERA", OracleType.Number).Value = V_BANDERA
        oraCommand.Parameters.Add("V_CONTRASENA", OracleType.VarChar).Value = V_CONTRASENA
        oraCommand.Parameters.Add("V_IP", OracleType.VarChar).Value = V_IP
        oraCommand.Parameters.Add("V_EVENTO", OracleType.VarChar).Value = V_EVENTO
        oraCommand.Parameters.Add("V_HIST_LO_ID_LOGIN", OracleType.VarChar).Value = V_HIST_LO_ID_LOGIN
        oraCommand.Parameters.Add("CV_1", OracleType.Cursor).Direction = ParameterDirection.Output
        DtsVariable = Consulta_Procedure(oraCommand, "Licencias")
        If V_BANDERA = 2 Then
            Session.Add("IdSession", DtsVariable.Tables(0).Rows(0)("ID").ToString)
        End If
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim DtsIntentos As DataSet = Class_Sesion.LlenarElementos("", "", "", "", 8, "", "", "", "")
            If DtsIntentos.Tables(0).Rows.Count <> 0 Then
                Lbl0.Text = DtsIntentos.Tables(0).Rows(0).Item(0)
            End If
        End If
        If Session("Intentos") >= Lbl0.Text + 1 Then
            LoginBtn.Enabled = False
            UserName.Enabled = False
            UserPass.Enabled = False
            LblMsj.Text = Lbl0.Text & " Intentos,Usuario Cancelado, Contacte A Su Administrador De Sistema."
            Licencias(UserName.Text, Campana, Modulos, ("Ingreso Denegado, Usuario y/o Contrase&ntildea Incorectos"), 1, UserPass.Text, Request.ServerVariables("REMOTE_HOST"), "Inicio De Sesion", "")
        End If
    End Sub

    Protected Sub Modulo()
        Dim Usuario As USUARIO = CType(Session("Usuario"), USUARIO)
        If Val(Usuario.CAT_LO_MGESTION) = 0 And Usuario.CAT_PE_PERFIL <> "Capturista" Then
            Limpiar()
            Session("Intentos") = Session("Intentos") + 1
            LblMsj.Text = "No Tiene Permisos Para Ingresar Al Sistema"
            If Session("Intentos") >= Lbl0.Text Then
                LoginBtn.Enabled = False
                UserName.Enabled = False
                UserPass.Enabled = False
                LblMsj.Text = Lbl0.Text & " Intentos,Usuario Cancelado, Contacte A Su Administrador De Sistema."
                Licencias(UserName.Text, Campana, Modulos, "Sin Permisos Para Ingresar Al Sistema", 1, UserPass.Text, Request.ServerVariables("REMOTE_HOST"), "Inicio De Sesion", "")
            End If
        Else
            If CType(Session("Usuario"), USUARIO).CAT_AG_ESTATUS <> "Baja" Then
                Licencias(CType(Session("Usuario"), USUARIO).CAT_LO_USUARIO, Campana, Modulos, "Acceso Autorizado", 2, CType(Session("Usuario"), USUARIO).CAT_LO_CONTRASENA, Request.ServerVariables("REMOTE_HOST"), "Inicio De Sesion", "")
                Session("Intentos") = 0
                Dim TmpAplicacion As New Aplicacion(0, 0, 0, 0, 0, 0, 0)
                Dim Credito As credito = New credito("", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "")
                Session("Mensajes") = "SinMensaje"
                Session("Credito") = Credito
                Session("Aplicacion") = TmpAplicacion
                LlenarAplicacion.APLICACION()
                If Usuario.CAT_PE_PERFIL = "Capturista" Then
                    Response.Redirect("CapturaVisitas.aspx", False)
                Else
                    Response.Redirect("MasterPage.aspx", False)
                End If
            Else
                Session("Intentos") = Lbl0.Text
                If Session("Intentos") >= Lbl0.Text Then
                    LoginBtn.Enabled = False
                    UserName.Enabled = False
                    UserPass.Enabled = False
                    LblMsj.Text = "Agencia Sin Permiso Para Ingresar Al Sistema, Contacte A Su Administrador De Sistema."
                    Licencias(CType(Session("Usuario"), USUARIO).CAT_LO_USUARIO, Campana, Modulos, "Agencia Con Estatus De Baja", 1, CType(Session("Usuario"), USUARIO).CAT_LO_CONTRASENA, Request.ServerVariables("REMOTE_HOST"), "Inicio De Sesion", "")
                End If
            End If
        End If
    End Sub

    Protected Sub LoginBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LoginBtn.Click
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1))
        Response.Cache.SetNoStore()

        If Len(UserPass.Text) > 7 Then
            Dim strUserID As String
            strUserID = LogOn(UserName.Text, UserPass.Text)



            If strUserID = 0 Then
                Limpiar()
                Session("Intentos") = Session("Intentos") + 1
                LblMsj.Text = ("Usuario O Contrase&ntildea Incorrectos, Por Favor Verifique. Intento #" & Session("Intentos"))
                If Session("Intentos") >= Lbl0.Text Then
                    LoginBtn.Enabled = False
                    UserName.Enabled = False
                    UserPass.Enabled = False
                    LblMsj.Text = Lbl0.Text & " Intentos,Usuario Cancelado, Contacte A Su Administrador De Sistema."
                    Licencias(UserName.Text, Campana, Modulos, ("Ingreso Denegado, Usuario y/o Contraseña Incorectos"), 1, UserPass.Text, Request.ServerVariables("REMOTE_HOST"), "Inicio De Sesion", "")
                End If
            ElseIf strUserID = 1 Then
                Limpiar()
                LblMsj.Text = ("El usuario ha sido cancelado. P&oacutengase en contacto con el Administrador de Sistema.")
                Session("Intentos") = Session("Intentos") + 1
                If Session("Intentos") >= Lbl0.Text Then
                    LoginBtn.Enabled = False
                    UserName.Enabled = False
                    UserPass.Enabled = False
                    LblMsj.Text = Lbl0.Text & " Intentos,Usuario Cancelado, Contacte A Su Administrador De Sistema."
                End If
            ElseIf strUserID = 2 Then
                UPnlCambioPass.Visible = True
                UpnlLogin.Visible = False
                LblUsuario.Text = UserName.Text
            ElseIf strUserID = 3 Then
                Limpiar()
                Session("Intentos") = Session("Intentos") + 1
                LblMsj.Text = "Usuario Fuera De Horario. Intento #" & Session("Intentos")
                If Session("Intentos") >= Lbl0.Text Then
                    LoginBtn.Enabled = False
                    UserName.Enabled = False
                    UserPass.Enabled = False
                    LblMsj.Text = Lbl0.Text & " Intentos,Usuario Cancelado, Contacte A Su Administrador De Sistema."
                    Licencias(UserName.Text, Campana, Modulos, "Ingreso Denegado, Fuera De Horario", 1, UserPass.Text, Request.ServerVariables("REMOTE_HOST"), "Inicio De Sesion", "")
                End If
            ElseIf strUserID = 4 Then
                Limpiar()
                Session("Intentos") = Session("Intentos") + 1
                LblMsj.Text = "Usuario Conectado. Intento #" & Session("Intentos")
                If Session("Intentos") >= Lbl0.Text Then
                    LoginBtn.Enabled = False
                    UserName.Enabled = False
                    UserPass.Enabled = False
                    LblMsj.Text = Lbl0.Text & " Intentos,Usuario Cancelado, Contacte A Su Administrador De Sistema."
                    Licencias(UserName.Text, Campana, Modulos, "Ingreso Denegado, Usuario Conectado", 2, UserPass.Text, Request.ServerVariables("REMOTE_HOST"), "Inicio De Sesion", "")
                End If
            ElseIf strUserID = 5 Then
                Limpiar()
                Session("Intentos") = Session("Intentos") + 1
                LblMsj.Text = ("Se Ha Excedido El N&uacutemero De Licencias Permitidas. Intento #" & Session("Intentos"))
                If Session("Intentos") >= Lbl0.Text Then
                    LoginBtn.Enabled = False
                    UserName.Enabled = False
                    UserPass.Enabled = False
                    LblMsj.Text = Lbl0.Text & (" Intentos,Usuario Cancelado, Contacte A Su Administrador De Sistema.")
                    Licencias(UserName.Text, Campana, Modulos, "Ingreso Denegado,Sin Licencias", 1, UserPass.Text, Request.ServerVariables("REMOTE_HOST"), "Inicio De Sesion", "")
                End If
            ElseIf strUserID = 6 Then
                Modulo()
            End If
        Else
            Limpiar()
            Session("Intentos") = Session("Intentos") + 1
            LblMsj.Text = ("Usuario O Contrase&ntildea Incorrectos, Por Favor Verifique. Intento #" & Session("Intentos"))
            If Session("Intentos") >= Lbl0.Text Then
                LoginBtn.Enabled = False
                UserName.Enabled = False
                UserPass.Enabled = False
                LblMsj.Text = Lbl0.Text & (" Intentos,Usuario Cancelado, Contacte A Su Administrador De Sistema.")
                Licencias(UserName.Text, Campana, Modulos, ("Ingreso Denegado, Usuario y/o Contraseña Incorectos"), 1, UserPass.Text, Request.ServerVariables("REMOTE_HOST"), "Inicio De Sesion", "")
            End If
        End If
    End Sub

    Function LogOn(ByVal strEmail As String, ByVal strPassword As String) As String
        Dim validaSession As String = 0
        Dim OraCommandL As New OracleCommand
        OraCommandL.CommandText = "SP_USUARIO"
        OraCommandL.CommandType = CommandType.StoredProcedure
        OraCommandL.Parameters.Add("V_USUARIO", OracleType.VarChar).Value = UserName.Text
        OraCommandL.Parameters.Add("V_CONTRASENA", OracleType.VarChar).Value = UserPass.Text
        OraCommandL.Parameters.Add("V_MODULO", OracleType.VarChar).Value = "WEB"
        OraCommandL.Parameters.Add("V_PANTALLA", OracleType.VarChar).Value = "LogOn"
        OraCommandL.Parameters.Add("CV_1", OracleType.Cursor).Direction = ParameterDirection.Output
        Try
            Dim DtsLogin As DataSet = Consulta_Procedure(OraCommandL, "LogOn")
            Dim DtvLogin As DataView
            DtvLogin = DtsLogin.Tables("LogOn").DefaultView
            If DtvLogin.Count = 0 Then
                Limpiar()
                LblMsj.Text = ("El Usuario No Existe O No Es Correcto, Por Favor Verifique.")
            Else

                Dim varUsuario As USUARIO = New USUARIO(DtsLogin.Tables(0).Rows(0)("CAT_LO_ID"), DtsLogin.Tables(0).Rows(0)("CAT_LO_USUARIO"), DtsLogin.Tables(0).Rows(0)("CAT_LO_NOMBRE"), DtsLogin.Tables(0).Rows(0)("CAT_LO_CONTRASENA"), DtsLogin.Tables(0).Rows(0)("CAT_PE_PERFIL"), DtsLogin.Tables(0).Rows(0)("CAT_LO_PERFIL"), DtsLogin.Tables(0).Rows(0)("CAT_LO_SUPERVISOR"), DtsLogin.Tables(0).Rows(0)("CAT_LO_DTEALTA"), DtsLogin.Tables(0).Rows(0)("CAT_LO_MOTIVO"), DtsLogin.Tables(0).Rows(0)("CAT_LO_MGESTION"), DtsLogin.Tables(0).Rows(0)("CAT_LO_PGESTION"), DtsLogin.Tables(0).Rows(0)("CAT_LO_MADMINISTRADOR"), DtsLogin.Tables(0).Rows(0)("CAT_LO_MREPORTES"), DtsLogin.Tables(0).Rows(0)("CAT_LO_HENTRADA"), DtsLogin.Tables(0).Rows(0)("CAT_LO_HSALIDA"), DtsLogin.Tables(0).Rows(0)("CAT_LO_AGENCIA"), DtsLogin.Tables(0).Rows(0)("CAT_LO_PRODUCTOS"), DtsLogin.Tables(0).Rows(0)("CAT_LO_PRODUCTO"), DtsLogin.Tables(0).Rows(0)("CAT_LO_CADENAPRODUCTOS"), DtsLogin.Tables(0).Rows(0)("CAT_LO_AGENCIASVER"), DtsLogin.Tables(0).Rows(0)("CAT_LO_CADENAAGENCIAS"), DtsLogin.Tables(0).Rows(0)("CAT_LO_ESTATUS"), DtsLogin.Tables(0).Rows(0)("CAT_AG_ESTATUS"), "", DtsLogin.Tables(0).Rows(0)("cat_Lo_Num_Agencia"), DtsLogin.Tables(0).Rows(0)("Licencias"), DtsLogin.Tables(0).Rows(0)("CAT_LO_HEREDAR"), DtsLogin.Tables(0).Rows(0)("GESTIONES"), DtsLogin.Tables(0).Rows(0)("CUANTASPP"), DtsLogin.Tables(0).Rows(0)("MONTOPP"), DtsLogin.Tables(0).Rows(0)("CUANTASPPC"), DtsLogin.Tables(0).Rows(0)("MONTOPPC"))

                Session("Usuario") = varUsuario

                Select Case CType(Session("Usuario"), USUARIO).CAT_LO_ESTATUS
                    Case Is = "Cancelado"
                        validaSession = 1
                    Case Is = "Expirado"
                        validaSession = 2
                    Case Is = "Activo"
                        Dim Hora As String = Now.Hour & ":" & Now.Minute
                        If (Val((CType(Session("Usuario"), USUARIO)).CAT_LO_HENTRADA) <= Val(Hora)) = False Or (Val(CType(Session("Usuario"), USUARIO).CAT_LO_HSALIDA) >= Val(Hora)) = False Then
                            validaSession = 3
                        Else
                            Dim Licencias As Integer = CType(Session("Usuario"), USUARIO).Licencias
                            If ValidaLicencias(Licencias, UserName.Text, "", DateTime.Now, 1, "") = 1 Then
                                validaSession = 4
                            Else
                                If ValidaLicencias(Licencias, UserName.Text, "", DateTime.Now, 3, "") = 1 Then
                                    Session("UserLoggedIn") = UserName.Text
                                    validaSession = 6
                                Else
                                    validaSession = 5
                                End If

                            End If
                        End If
                End Select
            End If
        Catch ex As Exception
            LblMsj.Text = "Error: " & ex.Message
            SendMail("LogOn", ex, "", UserName.Text & "|" & UserPass.Text, UserName.Text)
        End Try
        Return validaSession
    End Function

    Private Sub Limpiar()
        UserPass.Text = ""
        LblMsj.Text = ""
    End Sub

    Private Sub SendMail(ByRef evento As String, ByVal ex As Exception, ByVal Cuenta As String, ByVal Captura As String, ByVal usr As String)
        EnviarCorreo("LogIn.aspx", evento, ex, Cuenta, Captura, usr)
    End Sub

    Protected Sub BtnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles guardarcon.Click

        Dim USER As USUARIO = CType(Session("Usuario"), USUARIO)
        Try
            If TxtPassword.Text = TxtPassword2.Text Then
                If Len(TxtPassword.Text) >= 1 Then
                    Dim oraCommand As New OracleCommand
                    oraCommand.CommandText = "SP_VALIDA_CONTRASENA"
                    oraCommand.CommandType = CommandType.StoredProcedure
                    oraCommand.Parameters.Add("V_USUARIO", OracleType.VarChar).Value = USER.CAT_LO_USUARIO
                    oraCommand.Parameters.Add("V_CONTRASENA", OracleType.VarChar).Value = TxtPassword.Text
                    oraCommand.Parameters.Add("V_PORTAL", OracleType.VarChar).Value = "Gestion"
                    oraCommand.Parameters.Add("CV_1", OracleType.Cursor).Direction = ParameterDirection.Output
                    Dim DTS As DataSet = Consulta_Procedure(oraCommand, "VALIDA_CONTRASENA")

                    If DTS.Tables(0).Rows.Item(0)("ESTATUS") = "OK" Then
                        Licencias(USER.CAT_LO_USUARIO, Campana, Modulos, ("Cambio De Contrase&ntildea"), 2, TxtPassword.Text, Request.ServerVariables("REMOTE_HOST"), ("Cambio De Contrase&ntildea"), "")
                        LblErrores.Text = DTS.Tables(0).Rows.Item(0)("MENSAJE")
                        guardar_psw()
                        Modulo()
                    Else
                        LblErrores.Text = DTS.Tables(0).Rows.Item(0)("MENSAJE")
                    End If

                Else
                    LblErrores.Text = "Contrase&ntildea no Valida, Por Favor Verifique."
                End If
            Else
                LblErrores.Text = "La Contrase&ntildea No Coincide, Por Favor Verifique."
            End If

        Catch ex As Exception
            LblErrores.Text = "Error" & ex.Message
            SendMail2("Function Search", ex, "", "", USER.CAT_LO_CONTRASENA)
        End Try

        'Dim USER As USUARIO = CType(Session("Usuario"), USUARIO)
        'Try
        '    If TxtPassword.Text = TxtPassword2.Text Then
        '        If Len(TxtPassword.Text) >= 8 Then
        '            Dim oraCommand As New OracleCommand
        '            oraCommand.CommandText = "SP_VALIDA_CONTRASENA"
        '            oraCommand.CommandType = CommandType.StoredProcedure
        '            oraCommand.Parameters.Add("V_USUARIO", OracleType.VarChar).Value = USER.CAT_LO_USUARIO
        '            oraCommand.Parameters.Add("V_CONTRASENA", OracleType.VarChar).Value = TxtPassword.Text
        '            oraCommand.Parameters.Add("CV_1", OracleType.Cursor).Direction = ParameterDirection.Output
        '            Dim DTS As DataSet = Consulta_Procedure(oraCommand, "VALIDA_CONTRASENA")
        '            If Val(DTS.Tables(0).Rows.Item(0)("CUANTOS")) = 0 Then
        '                Dim Cumple As Integer = ValidaPass(TxtPassword.Text)
        '                If Cumple = 0 Then
        '                    Licencias(USER.CAT_LO_USUARIO, Campana, Modulos, ("Cambio De Contrase&ntildea"), 2, TxtPassword.Text, Request.ServerVariables("REMOTE_HOST"), ("Cambio De Contrase&ntildea"))
        '                    guardar_psw()
        '                    Modulo()
        '                ElseIf Cumple = 1 Then
        '                    LblErrores.Text = ("La Contrase&ntildea Debe Contener Al Menos Un Caracter Especial")
        '                ElseIf Cumple = 2 Then
        '                    LblErrores.Text = ("La Contrase&ntildea Debe Contener Al Menos Un N&uacutemero")
        '                ElseIf Cumple = 3 Then
        '                    LblErrores.Text = ("La Contrase&ntildea Debe Contener Al Menos Una Letra Minuscula")
        '                ElseIf Cumple = 4 Then
        '                    LblErrores.Text = ("La Contrase&ntildea Debe Contener Al Menos Una Letra Mayuscula")
        '                Else
        '                    LblErrores.Text = ("Error Contrase&ntildea")
        '                End If
        '            Else
        '                LblErrores.Text = "La Contrase&ntildea Ya Ha Sido Utilizada Anteriormente, Ingrese Otra."
        '            End If
        '        Else
        '            LblErrores.Text = "La Contrase&ntildea Debe de Contener M&aacutes De 8 Caract&eacuteres, Por Favor Verifique."
        '        End If
        '    Else
        '        LblErrores.Text = "La Contrase&ntildea No Coincide, Por Favor Verifique."
        '    End If
        'Catch ex As Exception
        '    LblErrores.Text = "Error" & ex.Message
        '    SendMail2("Function Search", ex, "", "", USER.CAT_LO_CONTRASENA)
        'End Try

    End Sub

    Private Sub guardar_psw()
        Dim USER As USUARIO = CType(Session("Usuario"), USUARIO)
        Try
            Dim oraCommand As New OracleCommand
            oraCommand.CommandText = "SP_GUARDAR_CONTRASENA"
            oraCommand.CommandType = CommandType.StoredProcedure
            oraCommand.Parameters.Add("V_LUSUARIO", OracleType.VarChar).Value = USER.CAT_LO_USUARIO
            oraCommand.Parameters.Add("v_CONTRASENA", OracleType.VarChar).Value = TxtPassword.Text
            Ejecuta_Procedure(oraCommand)
            USER.CAT_LO_CONTRASENA = TxtPassword.Text
        Catch ex As Exception
            SendMail("guardar_psw", ex, "", TxtPassword.Text, USER.CAT_LO_CONTRASENA)
        End Try
    End Sub

    Private Sub SendMail2(ByRef evento As String, ByVal ex As Exception, ByVal Cuenta As String, ByVal Captura As String, ByVal usr As String)
        EnviarCorreo("ChgPsw.aspx", evento, ex, Cuenta, Captura, usr)
    End Sub

    Protected Sub GuardarSin_Click(sender As Object, e As EventArgs) Handles guardarsin.Click
        UPnlCambioPass.Visible = False
        UpnlLogin.Visible = True
        LblMsj.Text = ""
    End Sub

    'Function ValidaPass(ByVal Pass As String) As Integer
    '    Dim ValidoCaracter As Integer
    '    Dim ValidoNumero As Integer
    '    Dim ValidoMinusculas As Integer
    '    Dim ValidoMayusculas As Integer

    '    For i = 1 To Len(Pass)
    '        If InStr(1, "|!·$%&/()=?*,+-@<>", Mid(Pass, i, 1)) > 0 Then
    '            ValidoCaracter = ValidoCaracter + 1
    '        End If
    '    Next

    '    For a = 1 To Len(Pass)
    '        If InStr(1, "qwertyuiopasdfghjklzxcvbnmñ", Mid(Pass, a, 1)) > 0 Then
    '            ValidoMinusculas = ValidoMinusculas + 1
    '        End If
    '    Next

    '    For A = 1 To Len(Pass)
    '        If InStr(1, "QWERTYUIOPASDFGHJKLÑZXCVBNM", Mid(Pass, A, 1)) > 0 Then
    '            ValidoMayusculas = ValidoMayusculas + 1
    '        End If
    '    Next

    '    For i = 1 To Len(Pass)
    '        If InStr(1, "1234567890", Mid(Pass, i, 1)) > 0 Then
    '            ValidoNumero = ValidoNumero + 1
    '        End If
    '    Next

    '    If ValidoNumero > 0 And ValidoCaracter > 0 And ValidoMinusculas > 0 And ValidoMayusculas > 0 Then
    '        Return 0
    '    ElseIf ValidoNumero > 0 And ValidoCaracter = 0 And ValidoMinusculas > 0 And ValidoMayusculas > 0 Then
    '        Return 1
    '    ElseIf ValidoNumero = 0 And ValidoCaracter > 0 And ValidoMinusculas > 0 And ValidoMayusculas > 0 Then
    '        Return 2
    '    ElseIf ValidoNumero > 0 And ValidoCaracter > 0 And ValidoMinusculas = 0 And ValidoMayusculas > 0 Then
    '        Return 3
    '    ElseIf ValidoNumero > 0 And ValidoCaracter > 0 And ValidoMinusculas > 0 And ValidoMayusculas = 0 Then
    '        Return 4
    '    Else
    '        Return 5
    '    End If
    'End Function
End Class
