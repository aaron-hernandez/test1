Imports System.Data
Imports Microsoft.VisualBasic
Imports System.Data.OracleClient
Imports Db
Imports System.Globalization
Imports System.IO
Imports Funciones
Imports System.Web.SessionState.HttpSessionState
Public Class Class_MasterPage
    Public Shared Function Telefonos(ByVal V_Credito As String, ByVal V_Campo As String, ByVal V_CampoFecha As String, ByVal V_Tabla As String, ByVal V_Tipo As String, ByVal V_Calificacion As String, ByVal V_Telefono As String, ByVal V_Bandera As String) As Object
        Dim oraCommand As New OracleCommand
        oraCommand.CommandText = "Sp_Telefonos"
        oraCommand.CommandType = CommandType.StoredProcedure
        oraCommand.Parameters.Add("V_Credito", OracleType.VarChar).Value = V_Credito
        oraCommand.Parameters.Add("V_Tabla", OracleType.VarChar).Value = V_Tabla
        oraCommand.Parameters.Add("V_Campo", OracleType.VarChar).Value = V_Campo
        oraCommand.Parameters.Add("V_CampoFecha", OracleType.VarChar).Value = V_CampoFecha
        oraCommand.Parameters.Add("V_Tipo", OracleType.VarChar).Value = V_Tipo
        oraCommand.Parameters.Add("V_Calificacion", OracleType.VarChar).Value = V_Calificacion
        oraCommand.Parameters.Add("V_Telefono", OracleType.VarChar).Value = V_Telefono
        oraCommand.Parameters.Add("V_Bandera", OracleType.VarChar).Value = V_Bandera
        oraCommand.Parameters.Add("cv_1", OracleType.Cursor).Direction = ParameterDirection.Output
        Dim DtsTelefonos As DataSet = Consulta_Procedure(oraCommand, "Telefonos")
        Return DtsTelefonos
    End Function
    Public Shared Function Agenda(ByVal V_Usuario As String, ByVal V_Bandera As String) As Object
        Dim oraCommand As New OracleCommand
        oraCommand.CommandText = "Sp_Filasagenda"
        oraCommand.CommandType = CommandType.StoredProcedure
        oraCommand.Parameters.Add("V_Valor", OracleType.VarChar).Value = ""
        oraCommand.Parameters.Add("V_Usuario", OracleType.VarChar).Value = V_Usuario
        oraCommand.Parameters.Add("V_Bandera", OracleType.VarChar).Value = V_Bandera
        oraCommand.Parameters.Add("cv_1", OracleType.Cursor).Direction = ParameterDirection.Output
        Dim DtsAgenda As DataSet = Consulta_Procedure(oraCommand, "Agenda")
        Return DtsAgenda
    End Function
    Public Shared Sub Tocar(ByVal V_Valor As String, ByVal V_Usuario As String)
        Dim oraCommand As New OracleCommand
        oraCommand.CommandText = "SP_VARIOS_QRS"
        oraCommand.CommandType = CommandType.StoredProcedure
        oraCommand.Parameters.Add("V_USUARIO", OracleType.VarChar).Value = V_Usuario
        oraCommand.Parameters.Add("V_VALOR", OracleType.VarChar).Value = V_Valor
        oraCommand.Parameters.Add("V_BANDERA", OracleType.Number).Value = 1
        oraCommand.Parameters.Add("CV_1", OracleType.Cursor).Direction = ParameterDirection.Output
        Dim DtsVariable As DataSet = Consulta_Procedure(oraCommand, "Tocar")
    End Sub
    Public Shared Sub MarcarLeidos(ByVal V_Valor As String, ByVal V_Usuario As String)
        Dim oraCommand As New OracleCommand
        oraCommand.CommandText = "SP_VARIOS_QRS"
        oraCommand.CommandType = CommandType.StoredProcedure
        oraCommand.Parameters.Add("V_USUARIO", OracleType.VarChar).Value = V_Usuario
        oraCommand.Parameters.Add("V_VALOR", OracleType.VarChar).Value = ""
        oraCommand.Parameters.Add("V_BANDERA", OracleType.Number).Value = V_Valor
        oraCommand.Parameters.Add("CV_1", OracleType.Cursor).Direction = ParameterDirection.Output
        Dim DtsVariable As DataSet = Consulta_Procedure(oraCommand, "Tocar")
    End Sub
    Public Shared Function LlenarElementosMaster(ByVal Objeto As DropDownList, ByVal V_Valor As String, ByVal V_Producto As String, ByVal V_Perfil As String, ByVal V_Tipo As String, V_Bandera As String) As Object
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
    Public Shared Function GuardarGestion(ByVal V_Hist_Ge_Credito As String, ByVal V_Hist_Ge_Producto As String, ByVal V_Hist_Ge_Usuario As String, ByVal V_Codaccion As String, ByVal V_Hist_Ge_Resultado As String, ByVal V_Codresult As String, ByVal V_Codnopago As String, ByVal V_Hist_Ge_Comentario As String, ByVal V_Hist_Ge_Inoutbound As String, ByVal V_Hist_Ge_Telefono As String, ByVal V_Hist_Ge_Agencia As String, ByVal v_Hist_Pr_Dtepromesa As String, ByVal V_Hist_Pr_Montopp As String, ByVal V_Aplicacion_Accion As String, ByVal V_Aplicacion_NoPago As String, ByVal V_Anterior As String, ByVal Tipo As String, ByVal v_Hist_Pr_Motivo As String, ByVal v_Hist_Pr_Supervisor As String, ByVal v_Hist_PR_CA_CAMPANA As String, ByVal v_Hist_PR_CF_BUCKET As String, ByVal V_HIST_GE_FILAS_T As String) As Object

        If V_Aplicacion_Accion = 1 And V_Codaccion = "Seleccione" Then
            Return "Seleccione Una Acción"
        ElseIf V_Codresult = "Seleccione" Then
            Return "Seleccione Un Resultado"
        ElseIf (V_Aplicacion_NoPago = 1 And V_Codnopago = "Seleccione" And V_Codresult.Split(",")(1) = 1) Then
            Return "Seleccione Una Causa De No Pago"
        ElseIf V_Hist_Ge_Comentario.Length < 10 Then
            Return "Capture Un Comentario Valido"
        Else

            If Tipo = 1 Then
                Return AddPromesa(V_Hist_Ge_Credito, V_Hist_Ge_Producto, V_Hist_Pr_Montopp, v_Hist_Pr_Dtepromesa, V_Hist_Ge_Usuario, "Parcial", 1, "Telefonica", V_Hist_Ge_Agencia, V_Codaccion, V_Hist_Ge_Resultado, V_Codresult.Split(",")(0), V_Codnopago, V_Hist_Ge_Comentario, "", V_Hist_Ge_Telefono, 0, V_Anterior, 1, v_Hist_Pr_Dtepromesa, v_Hist_PR_CA_CAMPANA, v_Hist_PR_CF_BUCKET, V_HIST_GE_FILAS_T)
            ElseIf Tipo = 3 Then
                CancelarPP(v_Hist_Pr_Motivo & "," & V_Hist_Ge_Credito, v_Hist_Pr_Supervisor, 9)
                Return AddPromesa(V_Hist_Ge_Credito, V_Hist_Ge_Producto, V_Hist_Pr_Montopp, v_Hist_Pr_Dtepromesa, V_Hist_Ge_Usuario, "Parcial", 1, "Telefonica", V_Hist_Ge_Agencia, V_Codaccion, V_Hist_Ge_Resultado, V_Codresult.Split(",")(0), V_Codnopago, V_Hist_Ge_Comentario, "", V_Hist_Ge_Telefono, 0, V_Anterior, 1, v_Hist_Pr_Dtepromesa, v_Hist_PR_CA_CAMPANA, v_Hist_PR_CF_BUCKET, V_HIST_GE_FILAS_T)
            Else
                Return AddGestion(V_Hist_Ge_Credito, V_Hist_Ge_Producto, V_Hist_Ge_Usuario, V_Codaccion, V_Hist_Ge_Resultado, V_Codresult.Split(",")(0), V_Codnopago, V_Hist_Ge_Comentario, V_Hist_Ge_Inoutbound, V_Hist_Ge_Telefono, V_Hist_Ge_Agencia, v_Hist_Pr_Dtepromesa, V_Anterior, v_Hist_PR_CA_CAMPANA, v_Hist_PR_CF_BUCKET, V_HIST_GE_FILAS_T)
            End If
        End If
    End Function
    Public Shared Function AddPromesa(ByVal v_HIST_PR_CREDITO As String, ByVal v_HIST_PR_PRODUCTO As String, ByVal v_HIST_PR_MONTOPP As String, ByVal v_HIST_PR_DTEPROMESA As String, ByVal v_HIST_PR_USUARIO As String, ByVal v_HIST_PR_TIPO As String, ByVal v_HIST_PR_CONSECUTIVO As String, ByVal v_HIST_PR_ORIGEN As String, ByVal v_HIST_PR_AGENCIA As String, ByVal V_CODACCION As String, ByVal v_HIST_GE_RESULTADO As String, ByVal v_HIST_GE_CODIGO As String, ByVal V_CODNOPAGO As String, ByVal v_HIST_GE_COMENTARIO As String, ByVal v_HIST_VI_DTEVISITA As String, ByVal V_Hist_Ge_Telefono As String, ByVal v_HIST_GE_INOUTBOUND As String, ByVal V_ANTERIOR As String, ByVal V_ACTUALIZAR As String, ByVal V_FECHASESGUIMIENTO As String, ByVal V_CAMPANA As String, ByVal V_BUKER As String, ByVal V_FILA_T As String) As DataSet
        Dim oraCommand As New OracleCommand
        oraCommand.CommandText = "SP_ADD_HIST_PROMESAS"
        oraCommand.CommandType = CommandType.StoredProcedure
        oraCommand.Parameters.Add("V_Hist_Pr_Credito", OracleType.VarChar).Value = v_HIST_PR_CREDITO
        oraCommand.Parameters.Add("V_Hist_Pr_Producto", OracleType.VarChar).Value = v_HIST_PR_PRODUCTO
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
        oraCommand.Parameters.Add("V_HIST_GE_COMENTARIO", OracleType.VarChar).Value = v_HIST_GE_COMENTARIO
        oraCommand.Parameters.Add("v_HIST_VI_DTEVISITA", OracleType.VarChar).Value = v_HIST_VI_DTEVISITA
        oraCommand.Parameters.Add("V_Hist_Ge_Telefono", OracleType.VarChar).Value = V_Hist_Ge_Telefono
        oraCommand.Parameters.Add("v_HIST_GE_INOUTBOUND", OracleType.Number).Value = v_HIST_GE_INOUTBOUND
        oraCommand.Parameters.Add("V_ANTERIOR", OracleType.VarChar).Value = V_ANTERIOR
        oraCommand.Parameters.Add("V_Actualizar", OracleType.VarChar).Value = V_ACTUALIZAR
        oraCommand.Parameters.Add("V_Fechasesguimiento", OracleType.VarChar).Value = V_FECHASESGUIMIENTO
        oraCommand.Parameters.Add("V_Hist_Pr_Tipoacuerdo", OracleType.VarChar).Value = ""
        oraCommand.Parameters.Add("V_Hist_Pr_Tipodecontacto", OracleType.VarChar).Value = ""
        oraCommand.Parameters.Add("V_Hist_Pr_Periodicidad", OracleType.VarChar).Value = ""
        oraCommand.Parameters.Add("V_HIST_PR_SDONEGOCIADO", OracleType.VarChar).Value = ""
        oraCommand.Parameters.Add("V_HIST_PR_SDODESCUENTO", OracleType.VarChar).Value = ""
        oraCommand.Parameters.Add("V_HIST_PR_EXCEPCION", OracleType.VarChar).Value = ""
        oraCommand.Parameters.Add("V_CAMPANA", OracleType.VarChar).Value = V_CAMPANA
        oraCommand.Parameters.Add("V_BUCKET", OracleType.VarChar).Value = V_BUKER
        oraCommand.Parameters.Add("V_FILA_T", OracleType.VarChar).Value = V_FILA_T
        oraCommand.Parameters.Add("cv_1", OracleType.Cursor).Direction = ParameterDirection.Output
        Dim DtsPromesa As DataSet = Consulta_Procedure(oraCommand, "Promesa")
        Return DtsPromesa
    End Function
    Public Shared Function AddGestion(ByVal V_Hist_Ge_Credito As String, ByVal V_Hist_Ge_Producto As String, ByVal V_Hist_Ge_Usuario As String, ByVal V_Codaccion As String, ByVal V_Hist_Ge_Resultado As String, ByVal V_Codresult As String, ByVal V_Codnopago As String, ByVal V_Hist_Ge_Comentario As String, ByVal V_Hist_Ge_Inoutbound As String, ByVal V_Hist_Ge_Telefono As String, ByVal V_Hist_Ge_Agencia As String, ByVal v_Hist_Pr_Dtepromesa As String, ByVal V_Anterior As String, ByVal V_CAMPANA As String, ByVal V_BUKER As String, ByVal V_FILA_T As String) As DataSet
        Dim oraCommand As New OracleCommand
        oraCommand.CommandText = "SP_ADD_HIST_GESTIONES"
        oraCommand.CommandType = CommandType.StoredProcedure
        oraCommand.Parameters.Add("V_Hist_Ge_Credito", OracleType.VarChar).Value = V_Hist_Ge_Credito
        oraCommand.Parameters.Add("V_Hist_Ge_Producto", OracleType.VarChar).Value = V_Hist_Ge_Producto
        oraCommand.Parameters.Add("V_Hist_Ge_Usuario", OracleType.VarChar).Value = V_Hist_Ge_Usuario
        oraCommand.Parameters.Add("V_Codaccion", OracleType.VarChar).Value = V_Codaccion
        oraCommand.Parameters.Add("V_Hist_Ge_Resultado", OracleType.VarChar).Value = V_Hist_Ge_Resultado
        oraCommand.Parameters.Add("V_Codresult", OracleType.VarChar).Value = V_Codresult
        oraCommand.Parameters.Add("V_Codnopago", OracleType.VarChar).Value = V_Codnopago
        oraCommand.Parameters.Add("V_Hist_Ge_Comentario", OracleType.VarChar).Value = V_Hist_Ge_Comentario
        oraCommand.Parameters.Add("V_Hist_Ge_Inoutbound", OracleType.VarChar).Value = V_Hist_Ge_Inoutbound
        oraCommand.Parameters.Add("V_Hist_Ge_Telefono", OracleType.VarChar).Value = V_Hist_Ge_Telefono
        oraCommand.Parameters.Add("V_Hist_Ge_Agencia", OracleType.VarChar).Value = V_Hist_Ge_Agencia
        oraCommand.Parameters.Add("V_Dtecontacto", OracleType.VarChar).Value = v_Hist_Pr_Dtepromesa
        oraCommand.Parameters.Add("V_Anterior", OracleType.VarChar).Value = V_Anterior
        oraCommand.Parameters.Add("V_CAMPANA", OracleType.VarChar).Value = V_CAMPANA
        oraCommand.Parameters.Add("V_BUCKET", OracleType.VarChar).Value = V_BUKER
        oraCommand.Parameters.Add("V_FILA_T", OracleType.VarChar).Value = V_FILA_T

        oraCommand.Parameters.Add("cv_1", OracleType.Cursor).Direction = ParameterDirection.Output
        Dim DtsGestion As DataSet = Consulta_Procedure(oraCommand, "Gestion")
        Return DtsGestion
    End Function

    Public Shared Sub CancelarPP(ByVal V_Valor As String, ByVal V_Usuario As String, ByVal V_Bandera As String)
        Dim oraCommand As New OracleCommand
        oraCommand.CommandText = "SP_VARIOS_QRS"
        oraCommand.CommandType = CommandType.StoredProcedure
        oraCommand.Parameters.Add("V_USUARIO", OracleType.VarChar).Value = V_Usuario
        oraCommand.Parameters.Add("V_VALOR", OracleType.VarChar).Value = V_Valor
        oraCommand.Parameters.Add("V_BANDERA", OracleType.Number).Value = V_Bandera
        oraCommand.Parameters.Add("CV_1", OracleType.Cursor).Direction = ParameterDirection.Output
        Dim DtsVariable As DataSet = Consulta_Procedure(oraCommand, "Tocar")
    End Sub

    Public Shared Function RegresaCredito(ByVal V_Valor As String, ByVal V_Usuario As String, ByVal V_Bandera As String) As DataSet
        Dim oraCommand As New OracleCommand
        oraCommand.CommandText = "SP_VARIOS_QRS"
        oraCommand.CommandType = CommandType.StoredProcedure
        oraCommand.Parameters.Add("V_USUARIO", OracleType.VarChar).Value = V_Usuario
        oraCommand.Parameters.Add("V_VALOR", OracleType.VarChar).Value = V_Valor
        oraCommand.Parameters.Add("V_BANDERA", OracleType.Number).Value = V_Bandera
        oraCommand.Parameters.Add("CV_1", OracleType.Cursor).Direction = ParameterDirection.Output
        Dim DtsVariable As DataSet = Consulta_Procedure(oraCommand, "Credito")
        Return DtsVariable
    End Function
End Class

