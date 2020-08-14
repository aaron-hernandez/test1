Imports Microsoft.VisualBasic
Imports System.Data.OracleClient
Imports Db
Imports System.Globalization
Imports System.IO
Imports Funciones
Imports System.Web.SessionState.HttpSessionState
Imports System.Data
Public Class Class_CapturaVisitas
    Public Shared Function LlenarElementosVisitas(ByVal V_Credito As String, ByVal V_Bandera As String) As Object
        Dim oraCommand As New OracleCommand
        oraCommand.CommandText = "SP_HISTORICO_VISITAS"
        oraCommand.CommandType = CommandType.StoredProcedure
        oraCommand.Parameters.Add("V_Credito", OracleType.VarChar).Value = V_Credito
        oraCommand.Parameters.Add("V_Bandera", OracleType.VarChar).Value = V_Bandera
        oraCommand.Parameters.Add("cv_1", OracleType.Cursor).Direction = ParameterDirection.Output

        Dim DtsElemento As DataSet = Consulta_Procedure(oraCommand, "ELEMENTOS")
       
        Return DtsElemento
    End Function
    Public Shared Function LlenarElementosCodigos(ByVal Objeto As DropDownList, ByVal V_Valor As String, ByVal V_Producto As String, ByVal V_Perfil As String, ByVal V_Tipo As String, V_Bandera As String) As Object
        Dim oraCommand As New OracleCommand
        oraCommand.CommandText = "SP_CODIGOS_GESTION"
        oraCommand.CommandType = CommandType.StoredProcedure
        oraCommand.Parameters.Add("V_Valor", OracleType.VarChar).Value = V_Valor
        oraCommand.Parameters.Add("V_Producto", OracleType.VarChar).Value = V_Producto
        oraCommand.Parameters.Add("V_Perfil", OracleType.VarChar).Value = V_Perfil
        oraCommand.Parameters.Add("V_Tipo", OracleType.VarChar).Value = V_Tipo
        oraCommand.Parameters.Add("V_Bandera", OracleType.VarChar).Value = V_Bandera
        oraCommand.Parameters.Add("cv_1", OracleType.Cursor).Direction = ParameterDirection.Output
        Dim DtsTelefonos As DataSet = Consulta_Procedure(oraCommand, "ELEMENTOS")

        If Objeto.ID <> "DdlHist_Ge_NoPago" Then
            Objeto.DataTextField = "Descripcion"
            Objeto.DataValueField = "Codigo"
            Objeto.DataSource = DtsTelefonos
            Objeto.DataBind()
            Objeto.Items.Add("Seleccione")
            Objeto.SelectedValue = "Seleccione"
        End If
        Return DtsTelefonos
    End Function

    Public Shared Function VariosQ(ByVal V_Valor As String, ByVal V_Usuario As String, ByVal V_Bandera As String) As String
        Dim oraCommand As New OracleCommand
        oraCommand.CommandText = "SP_VARIOS_QRS"
        oraCommand.CommandType = CommandType.StoredProcedure
        oraCommand.Parameters.Add("V_USUARIO", OracleType.VarChar).Value = V_Usuario
        oraCommand.Parameters.Add("V_VALOR", OracleType.VarChar).Value = V_Valor
        oraCommand.Parameters.Add("V_BANDERA", OracleType.Number).Value = V_Bandera
        oraCommand.Parameters.Add("CV_1", OracleType.Cursor).Direction = ParameterDirection.Output
        Dim DtsVariable As DataSet = Consulta_Procedure(oraCommand, "Tocar")
        If V_Bandera > 5 Then
            Return DtsVariable.Tables(0).Rows(0).Item("Valor")
        Else
            Return ""
        End If
    End Function
    Public Shared Function GuardarVisita(ByVal V_Hist_Vi_Credito As String, ByVal V_Hist_Vi_Producto As String, ByVal V_Hist_Vi_Visitador As String, ByVal V_Hist_Vi_Capturista As String, ByVal V_Hist_Vi_Dtevisita As String, ByVal V_Codaccion As String, ByVal V_Hist_Vi_Codigo As String, ByVal V_Hist_Vi_Resultado As String, ByVal V_Codnopago As String, ByVal V_Hist_Vi_Comentario As String, ByVal V_Hist_Vi_Nombrec As String, ByVal V_Hist_Vi_Parentesco As String, ByVal V_Hist_Vi_Tipodomicilio As String, ByVal V_Hist_Vi_Nivelsocio As String, ByVal V_Hist_Vi_Niveles As String, ByVal V_Hist_Vi_Caracteristicas As String, ByVal V_Hist_Vi_Colorf As String, ByVal V_Hist_Vi_Colorp As String, ByVal V_Hist_Vi_Hcontacto As String, ByVal V_Hist_Vi_Dcontacto As Object, ByVal V_Hist_Vi_Referencia As String, ByVal V_Hist_Vi_Fuente As String, ByVal V_Hist_Vi_Agencia As String, ByVal V_Hist_Vi_Entrecalle1 As String, ByVal V_Hist_Vi_Entrecalle2 As String, ByVal V_Anterior As String, ByVal V_Aplicacion_Accion As String, ByVal V_Aplicacion_NoPago As String, ByVal Tipo As Integer, ByVal v_HIST_PR_MONTOPP As String, ByVal v_HIST_PR_DTEPROMESA As String, ByVal v_Hist_Pr_Motivo As String, ByVal v_Hist_Pr_Supervisor As String, ByVal V_Hist_Vi_Folio As String) As String

        If V_Aplicacion_Accion = 1 And V_Codaccion = "Seleccione" Then
            Return "Seleccione Una Acción"
        ElseIf V_Hist_Vi_Visitador = "Seleccione" Then
            Return "Seleccione Un Visitador"
        ElseIf V_Hist_Vi_Resultado = "Seleccione" Then
            Return "Seleccione Un Resultado"
        ElseIf (V_Aplicacion_NoPago = 1 And V_Codnopago = "Seleccione" And V_Hist_Vi_Codigo.Split(",")(1) = 1) Then
            Return "Seleccione Una Causa De No Pago"
        ElseIf V_Hist_Vi_Comentario.Length < 10 Then
            Return "Capture Un Comentario Valido"
        ElseIf V_Hist_Vi_Dtevisita.Length < 10 Then
            Return "Capture La Fecha De La Visita"
        ElseIf (V_Hist_Vi_Parentesco <> "Cliente" And V_Hist_Vi_Parentesco <> "Ninguno") And V_Hist_Vi_Nombrec = "" Then
            Return "Capture El Nombre Del " & V_Hist_Vi_Parentesco
        ElseIf VariosQ(V_Hist_Vi_Dtevisita.Split(" ")(0), "", 6) = "NO VALIDA" Then
            Return "La Fecha De Visita No Puede Ser Mayor A " & V_Hist_Vi_Dtevisita
        Else
            Dim Dias As String = ""
            For Each gvRow As GridViewRow In V_Hist_Vi_Dcontacto.Rows
                Dim chkSel As CheckBox = DirectCast(gvRow.FindControl("chkSelect"), CheckBox)
                Dias = Dias & Boleano(chkSel.Checked).ToString
            Next
            If Tipo = 1 Then
                Promesa(V_Hist_Vi_Credito, V_Hist_Vi_Producto, v_HIST_PR_MONTOPP, v_HIST_PR_DTEPROMESA, V_Hist_Vi_Visitador, "Parcial", 1, "Terreno", V_Hist_Vi_Agencia, V_Codaccion, V_Hist_Vi_Resultado, V_Hist_Vi_Codigo, V_Codnopago, V_Hist_Vi_Comentario, V_Hist_Vi_Dtevisita, "", 0, V_Anterior)

                Visitas(V_Hist_Vi_Credito, V_Hist_Vi_Producto, V_Hist_Vi_Visitador, V_Hist_Vi_Capturista, V_Hist_Vi_Dtevisita, V_Codaccion, V_Hist_Vi_Codigo, V_Hist_Vi_Resultado, V_Codnopago, V_Hist_Vi_Comentario, V_Hist_Vi_Nombrec, V_Hist_Vi_Parentesco, V_Hist_Vi_Tipodomicilio, V_Hist_Vi_Nivelsocio, V_Hist_Vi_Niveles, V_Hist_Vi_Caracteristicas, V_Hist_Vi_Colorf, V_Hist_Vi_Colorp, V_Hist_Vi_Hcontacto, Dias, V_Hist_Vi_Referencia, V_Hist_Vi_Agencia, V_Hist_Vi_Entrecalle1, V_Hist_Vi_Entrecalle2, V_Hist_Vi_Folio, V_Anterior)
                Return "Visita Capturada"
            ElseIf Tipo = 3 Then
                VariosQ(v_Hist_Pr_Motivo & "," & V_Hist_Vi_Credito, v_Hist_Pr_Supervisor, 9)

                Promesa(V_Hist_Vi_Credito, V_Hist_Vi_Producto, v_HIST_PR_MONTOPP, v_HIST_PR_DTEPROMESA, V_Hist_Vi_Visitador, "Parcial", 1, "Terreno", V_Hist_Vi_Agencia, V_Codaccion, V_Hist_Vi_Resultado, V_Hist_Vi_Codigo, V_Codnopago, V_Hist_Vi_Comentario, V_Hist_Vi_Dtevisita, "", 0, V_Anterior)

                Visitas(V_Hist_Vi_Credito, V_Hist_Vi_Producto, V_Hist_Vi_Visitador, V_Hist_Vi_Capturista, V_Hist_Vi_Dtevisita, V_Codaccion, V_Hist_Vi_Codigo, V_Hist_Vi_Resultado, V_Codnopago, V_Hist_Vi_Comentario, V_Hist_Vi_Nombrec, V_Hist_Vi_Parentesco, V_Hist_Vi_Tipodomicilio, V_Hist_Vi_Nivelsocio, V_Hist_Vi_Niveles, V_Hist_Vi_Caracteristicas, V_Hist_Vi_Colorf, V_Hist_Vi_Colorp, V_Hist_Vi_Hcontacto, Dias, V_Hist_Vi_Referencia, V_Hist_Vi_Agencia, V_Hist_Vi_Entrecalle1, V_Hist_Vi_Entrecalle2, V_Hist_Vi_Folio, V_Anterior)

                Return "Visita Capturada"
            Else
                Visitas(V_Hist_Vi_Credito, V_Hist_Vi_Producto, V_Hist_Vi_Visitador, V_Hist_Vi_Capturista, V_Hist_Vi_Dtevisita, V_Codaccion, V_Hist_Vi_Codigo, V_Hist_Vi_Resultado, V_Codnopago, V_Hist_Vi_Comentario, V_Hist_Vi_Nombrec, V_Hist_Vi_Parentesco, V_Hist_Vi_Tipodomicilio, V_Hist_Vi_Nivelsocio, V_Hist_Vi_Niveles, V_Hist_Vi_Caracteristicas, V_Hist_Vi_Colorf, V_Hist_Vi_Colorp, V_Hist_Vi_Hcontacto, Dias, V_Hist_Vi_Referencia, V_Hist_Vi_Agencia, V_Hist_Vi_Entrecalle1, V_Hist_Vi_Entrecalle2, V_Hist_Vi_Folio, V_Anterior)
                Return "Visita Capturada"
            End If
        End If
    End Function
    Public Shared Sub Promesa(ByVal v_HIST_PR_CREDITO As String, ByVal v_HIST_PR_PRODUCTO As String, ByVal v_HIST_PR_MONTOPP As String, ByVal v_HIST_PR_DTEPROMESA As String, ByVal v_HIST_PR_USUARIO As String, ByVal v_HIST_PR_TIPO As String, ByVal v_HIST_PR_CONSECUTIVO As String, ByVal v_HIST_PR_ORIGEN As String, ByVal v_HIST_PR_AGENCIA As String, ByVal V_CODACCION As String, ByVal v_HIST_GE_RESULTADO As String, ByVal v_HIST_GE_CODIGO As String, ByVal V_CODNOPAGO As String, ByVal v_HIST_GE_COMENTARIO As String, ByVal v_HIST_VI_DTEVISITA As String, ByVal v_HIST_GE_TELEFONO As String, ByVal v_HIST_GE_INOUTBOUND As String, ByVal V_Anterior As String)
        Dim oraCommand As New OracleCommand
        oraCommand.CommandText = "SP_ADD_HIST_PROMESAS"
        oraCommand.CommandType = CommandType.StoredProcedure
        oraCommand.Parameters.Add("v_HIST_PR_CREDITO", OracleType.VarChar).Value = v_HIST_PR_CREDITO
        oraCommand.Parameters.Add("v_HIST_PR_PRODUCTO", OracleType.VarChar).Value = v_HIST_PR_PRODUCTO
        oraCommand.Parameters.Add("v_HIST_PR_MONTOPP", OracleType.Number).Value = v_HIST_PR_MONTOPP
        oraCommand.Parameters.Add("v_HIST_PR_DTEPROMESA", OracleType.VarChar).Value = v_HIST_PR_DTEPROMESA
        oraCommand.Parameters.Add("v_HIST_PR_USUARIO", OracleType.VarChar).Value = v_HIST_PR_USUARIO
        oraCommand.Parameters.Add("v_HIST_PR_TIPO", OracleType.VarChar).Value = v_HIST_PR_TIPO
        oraCommand.Parameters.Add("v_HIST_PR_CONSECUTIVO", OracleType.Number).Value = v_HIST_PR_CONSECUTIVO
        oraCommand.Parameters.Add("v_HIST_PR_ORIGEN", OracleType.VarChar).Value = v_HIST_PR_ORIGEN
        oraCommand.Parameters.Add("v_HIST_PR_AGENCIA", OracleType.VarChar).Value = v_HIST_PR_AGENCIA
        oraCommand.Parameters.Add("V_CODACCION", OracleType.VarChar).Value = V_CODACCION
        oraCommand.Parameters.Add("v_HIST_GE_RESULTADO", OracleType.VarChar).Value = v_HIST_GE_RESULTADO
        oraCommand.Parameters.Add("v_HIST_GE_CODIGO", OracleType.VarChar).Value = v_HIST_GE_CODIGO.Split(",")(0)
        oraCommand.Parameters.Add("V_CODNOPAGO", OracleType.VarChar).Value = V_CODNOPAGO
        oraCommand.Parameters.Add("v_HIST_GE_COMENTARIO", OracleType.VarChar).Value = v_HIST_GE_COMENTARIO
        oraCommand.Parameters.Add("v_HIST_VI_DTEVISITA", OracleType.VarChar).Value = v_HIST_VI_DTEVISITA & ":00"
        oraCommand.Parameters.Add("v_HIST_GE_TELEFONO", OracleType.VarChar).Value = v_HIST_GE_TELEFONO
        oraCommand.Parameters.Add("v_HIST_GE_INOUTBOUND", OracleType.Number).Value = v_HIST_GE_INOUTBOUND
        oraCommand.Parameters.Add("V_Anterior", OracleType.VarChar).Value = V_Anterior
        oraCommand.Parameters.Add("cv_1", OracleType.Cursor).Direction = ParameterDirection.Output
        Dim DtsPromesa As DataSet = Consulta_Procedure(oraCommand, "Promesa")
    End Sub
    Public Shared Sub Visitas(ByVal V_Hist_Vi_Credito As String, ByVal V_Hist_Vi_Producto As String, ByVal V_Hist_Vi_Visitador As String, ByVal V_Hist_Vi_Capturista As String, ByVal V_Hist_Vi_Dtevisita As String, ByVal V_Codaccion As String, ByVal V_Hist_Vi_Codigo As String, ByVal V_Hist_Vi_Resultado As String, ByVal V_Codnopago As String, ByVal V_Hist_Vi_Comentario As String, ByVal V_Hist_Vi_Nombrec As String, ByVal V_Hist_Vi_Parentesco As String, ByVal V_Hist_Vi_Tipodomicilio As String, ByVal V_Hist_Vi_Nivelsocio As String, ByVal V_Hist_Vi_Niveles As String, ByVal V_Hist_Vi_Caracteristicas As String, ByVal V_Hist_Vi_Colorf As String, ByVal V_Hist_Vi_Colorp As String, ByVal V_Hist_Vi_Hcontacto As String, ByVal Dias As String, ByVal V_Hist_Vi_Referencia As String, ByVal V_Hist_Vi_Agencia As String, ByVal V_Hist_Vi_Entrecalle1 As String, ByVal V_Hist_Vi_Entrecalle2 As String, ByVal V_Hist_Vi_Folio As String, ByVal V_Anterior As String)
        Dim oraCommandF As New OracleCommand
        oraCommandF.CommandText = "SP_ADD_HIST_VISITAS"
        oraCommandF.CommandType = CommandType.StoredProcedure
        oraCommandF.Parameters.Add("V_Hist_Vi_Credito", OracleType.VarChar).Value = V_Hist_Vi_Credito
        oraCommandF.Parameters.Add("V_Hist_Vi_Producto", OracleType.VarChar).Value = V_Hist_Vi_Producto
        oraCommandF.Parameters.Add("V_Hist_Vi_Visitador", OracleType.VarChar).Value = V_Hist_Vi_Visitador
        oraCommandF.Parameters.Add("V_Hist_Vi_Capturista", OracleType.VarChar).Value = V_Hist_Vi_Capturista
        oraCommandF.Parameters.Add("V_Hist_Vi_Dtevisita", OracleType.VarChar).Value = V_Hist_Vi_Dtevisita
        oraCommandF.Parameters.Add("V_Codaccion", OracleType.VarChar).Value = V_Codaccion
        oraCommandF.Parameters.Add("V_Hist_Vi_Codigo", OracleType.VarChar).Value = V_Hist_Vi_Codigo.Split(",")(0)
        oraCommandF.Parameters.Add("V_Hist_Vi_Resultado", OracleType.VarChar).Value = V_Hist_Vi_Resultado
        oraCommandF.Parameters.Add("V_Codnopago", OracleType.VarChar).Value = V_Codnopago
        oraCommandF.Parameters.Add("V_Hist_Vi_Comentario", OracleType.VarChar).Value = V_Hist_Vi_Comentario
        oraCommandF.Parameters.Add("V_Hist_Vi_Nombrec", OracleType.VarChar).Value = V_Hist_Vi_Nombrec
        oraCommandF.Parameters.Add("V_Hist_Vi_Parentesco", OracleType.VarChar).Value = V_Hist_Vi_Parentesco
        oraCommandF.Parameters.Add("V_Hist_Vi_Tipodomicilio", OracleType.VarChar).Value = V_Hist_Vi_Tipodomicilio
        oraCommandF.Parameters.Add("V_Hist_Vi_Nivelsocio", OracleType.VarChar).Value = V_Hist_Vi_Nivelsocio
        oraCommandF.Parameters.Add("V_Hist_Vi_Niveles", OracleType.VarChar).Value = V_Hist_Vi_Niveles
        oraCommandF.Parameters.Add("V_Hist_Vi_Caracteristicas", OracleType.VarChar).Value = V_Hist_Vi_Caracteristicas
        oraCommandF.Parameters.Add("V_Hist_Vi_Colorf", OracleType.VarChar).Value = V_Hist_Vi_Colorf
        oraCommandF.Parameters.Add("V_Hist_Vi_Colorp", OracleType.VarChar).Value = V_Hist_Vi_Colorp
        oraCommandF.Parameters.Add("V_Hist_Vi_Hcontacto", OracleType.VarChar).Value = V_Hist_Vi_Hcontacto
        oraCommandF.Parameters.Add("V_Hist_Vi_Dcontacto", OracleType.VarChar).Value = Dias
        oraCommandF.Parameters.Add("V_Hist_Vi_Referencia", OracleType.VarChar).Value = V_Hist_Vi_Referencia
        oraCommandF.Parameters.Add("V_Hist_Vi_Fuente", OracleType.VarChar).Value = "Cliente"
        oraCommandF.Parameters.Add("V_Hist_Vi_Agencia", OracleType.VarChar).Value = V_Hist_Vi_Agencia
        oraCommandF.Parameters.Add("V_Hist_Vi_Entrecalle1", OracleType.VarChar).Value = V_Hist_Vi_Entrecalle1
        oraCommandF.Parameters.Add("V_Hist_Vi_Entrecalle2", OracleType.VarChar).Value = V_Hist_Vi_Entrecalle2
        oraCommandF.Parameters.Add("V_Hist_Vi_Folio", OracleType.VarChar).Value = V_Hist_Vi_Folio
        oraCommandF.Parameters.Add("V_Anterior", OracleType.VarChar).Value = V_Anterior
        oraCommandF.Parameters.Add("cv_1", OracleType.Cursor).Direction = ParameterDirection.Output
        Dim oDatasetF As DataSet = Consulta_Procedure(oraCommandF, "SP_ADD_HIST_VISITAS")
    End Sub
End Class
