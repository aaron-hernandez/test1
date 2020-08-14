Imports System.Data
Imports Microsoft.VisualBasic
Imports System.Data.OracleClient
Imports Db
Imports System.Globalization
Imports System.IO
Imports System.Web.SessionState.HttpSessionState
Public Class Funciones

    Public Shared Sub EnviarCorreo(ByVal Pantalla As String, ByVal Evento As String, ByVal ex As Exception, ByVal Mgcta As String, ByVal Captura As String, ByVal usr As String)
        Dim oraCommand As New OracleCommand
        oraCommand.CommandText = "SP_ENVIAR_CORREO"
        oraCommand.CommandType = CommandType.StoredProcedure
        oraCommand.Parameters.Add("v_Subject", OracleType.VarChar).Value = "Cliente WEB"
        oraCommand.Parameters.Add("V_Pantalla", OracleType.VarChar).Value = Pantalla
        oraCommand.Parameters.Add("V_Evento", OracleType.VarChar).Value = Evento
        oraCommand.Parameters.Add("V_Error", OracleType.VarChar).Value = ex.ToString
        oraCommand.Parameters.Add("V_CREDITO", OracleType.VarChar).Value = Mgcta
        oraCommand.Parameters.Add("V_Captura", OracleType.VarChar).Value = Captura
        oraCommand.Parameters.Add("V_Usuario", OracleType.VarChar).Value = usr
        Ejecuta_Procedure(oraCommand)
    End Sub

    Public Shared Function Codificar(ByVal V_Valor As String) As String
        Return HttpUtility.HtmlDecode(V_Valor)
    End Function

    Public Shared Sub OffLine(ByVal Usuario As String)
        Dim DtsVariable As DataSet
        Dim oraCommand As New OracleCommand
        oraCommand.CommandText = "SP_INGRESO"
        oraCommand.CommandType = CommandType.StoredProcedure
        oraCommand.Parameters.Add("V_USUARIO", OracleType.VarChar).Value = Usuario
        oraCommand.Parameters.Add("V_CAMPANA", OracleType.VarChar).Value = "Planfia"
        oraCommand.Parameters.Add("V_MODULO", OracleType.VarChar).Value = "Gestion"
        oraCommand.Parameters.Add("V_MOTIVO", OracleType.VarChar).Value = "Expiro Sesión"
        oraCommand.Parameters.Add("V_BANDERA", OracleType.Number).Value = 4
        'DtsVariable = Consulta_Procedure(oraCommand, "Licencias")
    End Sub

    Public Shared Sub AUDITORIA(ByVal USUARIO As String, ByVal MODULO As String, ByVal PAGINA As String, ByVal CREDITO As String, ByVal EVENTO As String, ByVal VALOR As String, ByVal IPPUBLICA As String, ByVal IPPRIVADA As String, ByVal V_ID_Session As String)
        Dim DtsDisponibles As DataSet = Class_Sesion.LlenarElementos(USUARIO, "", "Gestion", "Cierre De Sesion Manual", 5, "", "", "Cierre De Sesion", V_ID_Session)
    End Sub

    Public Shared Function EmailValida(ByVal correo As String) As Boolean
        Dim atCnt As String
        Dim revCorreo As Boolean = 0
        If Len(correo) < 5 Then
            revCorreo = True
        ElseIf InStr(correo, "@") = 0 Then
            revCorreo = True
        ElseIf InStr(correo, ".") = 0 Then
            revCorreo = True
        ElseIf Len(correo) - InStrRev(correo, ".") > 4 Then
            revCorreo = True
        Else
            atCnt = 0
            For i = 1 To Len(correo)
                If Mid(correo, i, 1) = "@" Then
                    atCnt = atCnt + 1
                End If
            Next
            If atCnt > 1 Then
                revCorreo = True
            End If
            For i = 1 To Len(correo)
                If Not IsNumeric(Mid(correo, i, 1)) And _
          (LCase(Mid(correo, i, 1)) < "a" Or _
          LCase(Mid(correo, i, 1)) > "z") And _
          Mid(correo, i, 1) <> "_" And _
          Mid(correo, i, 1) <> "." And _
          Mid(correo, i, 1) <> "@" And _
          Mid(correo, i, 1) <> "-" Then
                    revCorreo = True
                End If
            Next
        End If
        Return revCorreo
    End Function

    Public Shared Function Boleano(ByVal valor As String) As Integer
        Dim bin As Integer
        If valor = True Then
            bin = 1
        Else
            bin = 0
        End If
        Return bin
    End Function

    Public Shared Function Boleano2(ByVal valor As Integer) As Boolean
        Dim bin As Integer
        If valor = 1 Then
            bin = True
        Else
            bin = False
        End If
        Return bin
    End Function

    Public Shared Function ValidaMonto(ByVal cadena As String) As Integer
        Dim CuantosCaracteres As Integer
        Dim MontoValido As Integer = 0
        CuantosCaracteres = 0
        For i = 1 To Len(cadena)
            If Mid(cadena, i, 1) = "." Then
                CuantosCaracteres = CuantosCaracteres + 1
            End If
        Next
        If CuantosCaracteres > 1 Then
            MontoValido = 1
        Else
            MontoValido = 0
        End If
        Return MontoValido
    End Function

    Public Shared Function ppvigente(ByVal cuenta As String) As Integer
        Dim oraCommand As New OracleCommand
        oraCommand.CommandText = "SP_VALIDAPP"
        oraCommand.CommandType = CommandType.StoredProcedure
        oraCommand.Parameters.Add("V_CREDITO", OracleType.VarChar).Value = cuenta
        oraCommand.Parameters.Add("cv_1", OracleType.Cursor).Direction = ParameterDirection.Output
        Dim oDataset As DataSet = Consulta_Procedure(oraCommand, "VALIDAPP")
        Dim VALIDA As Integer = oDataset.Tables(0).Rows(0)("EXISTE")
        Return VALIDA
    End Function

    Public Shared Function to_money(ByRef numero As String) As String
        Dim valor As Double = CDbl(Val(numero))
        to_money = "$ " & (valor.ToString("N", CultureInfo.InvariantCulture))
        Return to_money
    End Function

    Public Shared Function to_codigos(ByVal VALOR As String, ByVal campo As Integer) As String
        Dim extrae() As String = VALOR.Split("|")
        Return extrae(campo)
    End Function

    Public Shared Sub to_excel(ByRef pagina As Page, ByVal control As Control, ByVal file As String)
        Dim sb As New StringBuilder()
        Dim sw As New StringWriter(sb)
        Dim htw As New HtmlTextWriter(sw)
        Dim Page As New Page()
        Dim Form As New HtmlForm()
        control.EnableViewState = False
        Page.EnableEventValidation = False
        Page.DesignerInitialize()
        Page.Controls.Add(Form)
        Form.Controls.Add(control)
        Page.RenderControl(htw)
        pagina.Response.Clear()
        pagina.Response.Clear()
        pagina.Response.Buffer = True
        pagina.Response.ContentType = "text/plain"
        Dim fecha As String = Now.ToString("ddMMyyyy")
        pagina.Response.AddHeader("Content-Disposition", "attachment;filename=" & file + fecha & ".xls")
        pagina.Response.Charset = "UTF-8"
        pagina.Response.ContentEncoding = Encoding.Default
        pagina.Response.Write(sb.ToString())
        pagina.Response.End()
    End Sub

    Public Shared Sub LLENAR_DROP(ByVal bandera As String, ByVal ITEM As DropDownList, ByVal DataValueField As String, ByVal DataTextField As String)
        Dim oraCommand As New OracleCommand
        oraCommand.CommandText = "SP_CATALOGOS"
        oraCommand.CommandType = CommandType.StoredProcedure
        oraCommand.Parameters.Add("V_BANDERA", OracleType.Number).Value = bandera
        oraCommand.Parameters.Add("V_Valor", OracleType.VarChar).Value = ""
        oraCommand.Parameters.Add("cv_1", OracleType.Cursor).Direction = ParameterDirection.Output
        Dim objDSa As DataSet = Consulta_Procedure(oraCommand, "PROD")
        ITEM.Visible = True
        If objDSa.Tables(0).Rows.Count >= 1 Then
            ITEM.DataTextField = DataTextField
            ITEM.DataValueField = DataValueField
            ITEM.DataSource = objDSa.Tables(0)
            ITEM.DataBind()
            ITEM.Items.Add("Seleccione")
            ITEM.SelectedValue = "Seleccione"
        Else
            ITEM.Visible = False
        End If
    End Sub

    Public Shared Function GuardarGestion(ByVal v_HIST_GE_CREDITO As String, ByVal v_HIST_GE_PRODUCTO As String, ByVal v_HIST_GE_USUARIO As String, ByVal V_CODACCION As String, ByVal v_HIST_GE_RESULTADO As String, ByVal v_HIST_GE_CODIGO As String, ByVal V_CODNOPAGO As String, ByVal v_HIST_GE_COMENTARIO As String, ByVal v_HIST_GE_INOUTBOUND As Integer, ByVal v_HIST_GE_TELEFONO As String, ByVal v_HIST_GE_AGENCIA As String, ByVal V_DTECONTACTO As String, ByVal V_CONFIGURACION As String) As DataSet
        Dim oraCommand As New OracleCommand
        oraCommand.CommandText = "SP_ADD_HIST_GESTIONES"
        oraCommand.CommandType = CommandType.StoredProcedure
        oraCommand.Parameters.Add("v_HIST_GE_CREDITO", OracleType.VarChar).Value = v_HIST_GE_CREDITO
        oraCommand.Parameters.Add("v_HIST_GE_PRODUCTO", OracleType.VarChar).Value = "%" & v_HIST_GE_PRODUCTO & "%"
        oraCommand.Parameters.Add("v_HIST_GE_USUARIO", OracleType.VarChar).Value = v_HIST_GE_USUARIO
        oraCommand.Parameters.Add("V_CODACCION", OracleType.VarChar).Value = V_CODACCION
        oraCommand.Parameters.Add("v_HIST_GE_RESULTADO", OracleType.VarChar).Value = v_HIST_GE_RESULTADO
        oraCommand.Parameters.Add("v_HIST_GE_CODIGO", OracleType.VarChar).Value = v_HIST_GE_CODIGO
        oraCommand.Parameters.Add("V_CODNOPAGO", OracleType.VarChar).Value = V_CODNOPAGO
        oraCommand.Parameters.Add("v_HIST_GE_COMENTARIO", OracleType.VarChar).Value = v_HIST_GE_COMENTARIO
        oraCommand.Parameters.Add("v_HIST_GE_INOUTBOUND", OracleType.Number).Value = v_HIST_GE_INOUTBOUND
        oraCommand.Parameters.Add("v_HIST_GE_TELEFONO", OracleType.VarChar).Value = v_HIST_GE_TELEFONO
        oraCommand.Parameters.Add("v_HIST_GE_AGENCIA", OracleType.VarChar).Value = v_HIST_GE_AGENCIA
        oraCommand.Parameters.Add("V_DTECONTACTO", OracleType.VarChar).Value = V_DTECONTACTO
        oraCommand.Parameters.Add("V_CONFIGURACION", OracleType.VarChar).Value = V_CONFIGURACION
        oraCommand.Parameters.Add("cv_1", OracleType.Cursor).Direction = ParameterDirection.Output
        Dim DtsGestion As DataSet = Consulta_Procedure(oraCommand, "Gestion")
        Return DtsGestion
    End Function

    Public Shared Function GuardarPromesa(ByVal v_HIST_PR_CREDITO As String, ByVal v_HIST_PR_PRODUCTO As String, ByVal v_HIST_PR_MONTOPP As Double, ByVal v_HIST_PR_DTEPROMESA As String, ByVal v_HIST_PR_USUARIO As String, ByVal v_HIST_PR_TIPO As String, ByVal v_HIST_PR_CONSECUTIVO As Integer, ByVal v_HIST_PR_ORIGEN As String, ByVal v_HIST_PR_AGENCIA As String, ByVal V_CODACCION As String, ByVal v_HIST_GE_RESULTADO As String, ByVal v_HIST_GE_CODIGO As String, ByVal v_HIST_GE_COMENTARIO As String, ByVal v_HIST_VI_DTEVISITA As String, ByVal v_HIST_GE_TELEFONO As String, ByVal v_HIST_GE_INOUTBOUND As Integer, ByVal V_CONFIGURACION As String) As DataSet

        Dim oraCommand As New OracleCommand
        oraCommand.CommandText = "SP_ADD_HIST_PROMESAS"
        oraCommand.CommandType = CommandType.StoredProcedure
        oraCommand.Parameters.Add("v_HIST_PR_CREDITO", OracleType.VarChar).Value = v_HIST_PR_CREDITO
        oraCommand.Parameters.Add("v_HIST_PR_PRODUCTO", OracleType.VarChar).Value = "%" & v_HIST_PR_PRODUCTO & "%"
        oraCommand.Parameters.Add("v_HIST_PR_MONTOPP", OracleType.Number).Value = v_HIST_PR_MONTOPP
        oraCommand.Parameters.Add("v_HIST_PR_DTEPROMESA", OracleType.VarChar).Value = v_HIST_PR_DTEPROMESA
        oraCommand.Parameters.Add("v_HIST_PR_USUARIO", OracleType.VarChar).Value = v_HIST_PR_USUARIO
        oraCommand.Parameters.Add("v_HIST_PR_TIPO", OracleType.VarChar).Value = v_HIST_PR_TIPO
        oraCommand.Parameters.Add("v_HIST_PR_CONSECUTIVO", OracleType.Number).Value = v_HIST_PR_CONSECUTIVO
        oraCommand.Parameters.Add("v_HIST_PR_ORIGEN", OracleType.VarChar).Value = v_HIST_PR_ORIGEN
        oraCommand.Parameters.Add("v_HIST_PR_AGENCIA", OracleType.VarChar).Value = v_HIST_PR_AGENCIA
        oraCommand.Parameters.Add("V_CODACCION", OracleType.VarChar).Value = V_CODACCION
        oraCommand.Parameters.Add("v_HIST_GE_RESULTADO", OracleType.VarChar).Value = v_HIST_GE_RESULTADO
        oraCommand.Parameters.Add("v_HIST_GE_CODIGO", OracleType.VarChar).Value = v_HIST_GE_CODIGO
        oraCommand.Parameters.Add("v_HIST_GE_COMENTARIO", OracleType.VarChar).Value = v_HIST_GE_COMENTARIO
        oraCommand.Parameters.Add("v_HIST_VI_DTEVISITA", OracleType.VarChar).Value = v_HIST_VI_DTEVISITA
        oraCommand.Parameters.Add("v_HIST_GE_TELEFONO", OracleType.VarChar).Value = v_HIST_GE_TELEFONO
        oraCommand.Parameters.Add("v_HIST_GE_INOUTBOUND", OracleType.Number).Value = v_HIST_GE_INOUTBOUND
        oraCommand.Parameters.Add("V_CONFIGURACION", OracleType.VarChar).Value = V_CONFIGURACION
        oraCommand.Parameters.Add("cv_1", OracleType.Cursor).Direction = ParameterDirection.Output
        Dim DtsPromesa As DataSet = Consulta_Procedure(oraCommand, "Gestion")
        Return DtsPromesa
    End Function

    Public Shared Function ValidaLicencias(ByVal Cuantas As Integer, ByVal Usuario As String, ByVal Ip As String, ByVal Hora As String, ByVal V_bandera As Integer, ByVal V_ID_Session As String) As Integer
        Try
            Dim Cuantos As Integer = 0
            If V_bandera = 1 Then ' Esta Conectado?
                Dim DtsConectado As DataSet = Class_Sesion.LlenarElementos(Usuario, "", "", "", 3, "", "", "", "")
                If DtsConectado.Tables(0).Rows(0).Item("Cuantas") <> "0" Then
                    Return 1
                Else
                    Return 0
                End If
            ElseIf V_bandera = 2 Then ' Insertar Usuario Nuevo
                Dim DtsDisponibles As DataSet = Class_Sesion.LlenarElementos(Usuario, Cuantas, Ip, Hora, 6, "", "", "", "")
                Return 0
            ElseIf V_bandera = 3 Then 'Existen licencias Disponibles
                Dim DtsDisponibles As DataSet = Class_Sesion.LlenarElementos(Usuario, Cuantas, "", "", 4, "", "", "", "")
                If Val(DtsDisponibles.Tables(0).Rows(0).Item("Cuantas")) < Val(Cuantas) Then
                    Return 1
                Else
                    Return 0
                End If
            ElseIf V_bandera = 4 Then 'Desconectar
                Dim DtsDisponibles As DataSet = Class_Sesion.LlenarElementos(Usuario, "Finem", "Gestion", "Cierre De Sesion Manual", 5, "", "", "Cierre De Sesion", V_ID_Session)
                Return 0
            End If
        Catch ex As Exception
        End Try
    End Function
End Class
