Imports Microsoft.VisualBasic
Imports System.Data.OracleClient
Imports Db
Imports System.Globalization
Imports System.IO
Imports Funciones
Imports System.Web.SessionState.HttpSessionState
Imports System.Data
Public Class Class_Negociaciones
    Public Shared Function LlenarElementosNego(ByVal V_Valor As String, ByVal V_Valor2 As String, ByVal V_Valor3 As String, ByVal V_Bandera As String) As Object
        Dim oraCommand As New OracleCommand
        oraCommand.CommandText = "SP_NEGOCIACIONES"
        oraCommand.CommandType = CommandType.StoredProcedure
        oraCommand.Parameters.Add("V_Valor", OracleType.VarChar).Value = V_Valor
        oraCommand.Parameters.Add("V_Valor2", OracleType.VarChar).Value = V_Valor2
        oraCommand.Parameters.Add("V_Valor3", OracleType.VarChar).Value = V_Valor3
        oraCommand.Parameters.Add("V_Bandera", OracleType.VarChar).Value = V_Bandera
        oraCommand.Parameters.Add("cv_1", OracleType.Cursor).Direction = ParameterDirection.Output
        Dim DtsNegociaciones As DataSet = Consulta_Procedure(oraCommand, "ELEMENTOS")
        Return DtsNegociaciones
    End Function
    Public Shared Function GuardarNego(ByVal V_Hist_Pr_Credito As String, ByVal V_Hist_Pr_Producto As String, ByVal v_HIST_PR_MONTOPP As String, ByVal v_HIST_PR_DTEPROMESA As String, ByVal v_HIST_PR_USUARIO As String, ByVal v_HIST_PR_TIPO As String, ByVal v_HIST_PR_CONSECUTIVO As String, ByVal v_HIST_PR_ORIGEN As String, ByVal v_HIST_PR_AGENCIA As String, ByVal V_CODACCION As String, ByVal v_HIST_GE_RESULTADO As String, ByVal v_HIST_GE_CODIGO As String, ByVal V_CODNOPAGO As String, ByVal v_HIST_GE_COMENTARIO As String, ByVal v_HIST_VI_DTEVISITA As String, ByVal V_Hist_Ge_Telefono As String, ByVal v_HIST_GE_INOUTBOUND As String, ByVal V_Anterior As String, ByVal V_ACTUALIZAR As String, ByVal V_FECHASESGUIMIENTO As String, ByVal V_Hist_Pr_Tipoacuerdo As String, ByVal V_Hist_Pr_Tipodecontacto As String, ByVal V_Hist_Pr_Periodicidad As String, ByVal V_HIST_PR_SDONEGOCIADO As String, ByVal V_HIST_PR_SDODESCUENTO As String, ByVal V_Hist_Pr_Excepcion As String, ByVal V_HIST_CAMPANA As String, ByVal V_HIST_BUCKET As String, ByVal V_FILA_T As String) As String
        Dim oraCommand As New OracleCommand
        oraCommand.CommandText = "SP_ADD_HIST_PROMESAS"
        oraCommand.CommandType = CommandType.StoredProcedure
        oraCommand.Parameters.Add("v_HIST_PR_CREDITO", OracleType.VarChar).Value = V_Hist_Pr_Credito
        oraCommand.Parameters.Add("v_HIST_PR_PRODUCTO", OracleType.VarChar).Value = V_Hist_Pr_Producto
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
        oraCommand.Parameters.Add("v_HIST_VI_DTEVISITA", OracleType.VarChar).Value = v_HIST_VI_DTEVISITA
        oraCommand.Parameters.Add("v_HIST_GE_TELEFONO", OracleType.VarChar).Value = V_Hist_Ge_Telefono
        oraCommand.Parameters.Add("v_HIST_GE_INOUTBOUND", OracleType.Number).Value = v_HIST_GE_INOUTBOUND
        oraCommand.Parameters.Add("V_Anterior", OracleType.VarChar).Value = V_Anterior
        oraCommand.Parameters.Add("V_ACTUALIZAR", OracleType.VarChar).Value = V_ACTUALIZAR
        oraCommand.Parameters.Add("V_FECHASESGUIMIENTO", OracleType.VarChar).Value = V_FECHASESGUIMIENTO
        oraCommand.Parameters.Add("V_Hist_Pr_Tipoacuerdo", OracleType.VarChar).Value = V_Hist_Pr_Tipoacuerdo
        oraCommand.Parameters.Add("V_Hist_Pr_Tipodecontacto", OracleType.VarChar).Value = V_Hist_Pr_Tipodecontacto
        oraCommand.Parameters.Add("V_Hist_Pr_Periodicidad", OracleType.VarChar).Value = V_Hist_Pr_Periodicidad
        oraCommand.Parameters.Add("V_HIST_PR_SDONEGOCIADO", OracleType.VarChar).Value = V_HIST_PR_SDONEGOCIADO
        oraCommand.Parameters.Add("V_HIST_PR_SDODESCUENTO", OracleType.VarChar).Value = V_HIST_PR_SDODESCUENTO
        oraCommand.Parameters.Add("V_Hist_Pr_Excepcion", OracleType.VarChar).Value = V_Hist_Pr_Excepcion

        oraCommand.Parameters.Add("V_CAMPANA", OracleType.VarChar).Value = V_HIST_CAMPANA
        oraCommand.Parameters.Add("V_BUCKET", OracleType.VarChar).Value = V_HIST_BUCKET
        oraCommand.Parameters.Add("V_FILA_T", OracleType.VarChar).Value = V_FILA_T

        oraCommand.Parameters.Add("cv_1", OracleType.Cursor).Direction = ParameterDirection.Output
        Dim DtsPromesa As DataSet = Consulta_Procedure(oraCommand, "Promesa")
        Return " "
    End Function

    Public Shared Function LlenarElementosNego_Fijos(ByVal V_Tabla As String, ByVal V_Tipo As String, ByVal V_Credito As String, ByVal V_Valor1 As String, ByVal V_Valor2 As String, ByVal V_Valor3 As String, ByVal V_Valor4 As String, ByVal V_Bandera As String) As Object
        Dim oraCommand As New OracleCommand
        oraCommand.CommandText = "SP_NEGOCIACIONES_FIJAS"
        oraCommand.CommandType = CommandType.StoredProcedure
        oraCommand.Parameters.Add("V_Tabla", OracleType.VarChar).Value = V_Tabla
        oraCommand.Parameters.Add("V_Tipo", OracleType.VarChar).Value = V_Tipo
        oraCommand.Parameters.Add("V_Credito", OracleType.VarChar).Value = V_Credito
        oraCommand.Parameters.Add("V_Valor1", OracleType.VarChar).Value = V_Valor1
        oraCommand.Parameters.Add("V_Valor2", OracleType.VarChar).Value = V_Valor2
        oraCommand.Parameters.Add("V_Valor3", OracleType.VarChar).Value = V_Valor3
        oraCommand.Parameters.Add("V_Valor4", OracleType.VarChar).Value = V_Valor4
        oraCommand.Parameters.Add("V_Bandera", OracleType.VarChar).Value = V_Bandera
        oraCommand.Parameters.Add("cv_1", OracleType.Cursor).Direction = ParameterDirection.Output
        Dim DtsNegociaciones As DataSet = Consulta_Procedure(oraCommand, "ELEMENTOS")
        Return DtsNegociaciones
    End Function

End Class
