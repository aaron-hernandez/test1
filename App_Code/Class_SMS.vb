Imports Microsoft.VisualBasic
Imports System.Data.OracleClient
Imports Db
Imports System.Globalization
Imports System.IO
'Imports Funciones
Imports System.Web.SessionState.HttpSessionState
Imports System.Data
Public Class Class_SMS
    Public Shared Function LlenarElementos(ByVal V_Valor1 As String, ByVal V_Valor2 As String, ByVal V_Valor3 As String, ByVal V_Valor4 As String, ByVal V_Valor5 As String, ByVal V_Valor6 As String, ByVal V_Valor7 As String, ByVal V_Valor8 As String, ByVal V_Valor9 As String, ByVal V_Valor10 As String, ByVal V_Bandera As String) As Object
        Dim oraCommanSms As New OracleCommand
        oraCommanSms.CommandText = "SP_VARIOS_SMS"
        oraCommanSms.CommandType = CommandType.StoredProcedure
        oraCommanSms.Parameters.Add("V_Valor1", OracleType.VarChar).Value = V_Valor1
        oraCommanSms.Parameters.Add("V_Valor2", OracleType.VarChar).Value = V_Valor2
        oraCommanSms.Parameters.Add("V_Valor3", OracleType.VarChar).Value = V_Valor3
        oraCommanSms.Parameters.Add("V_Valor4", OracleType.VarChar).Value = V_Valor4
        oraCommanSms.Parameters.Add("V_Valor5", OracleType.VarChar).Value = V_Valor5
        oraCommanSms.Parameters.Add("V_Valor6", OracleType.VarChar).Value = V_Valor6
        oraCommanSms.Parameters.Add("V_Valor7", OracleType.VarChar).Value = V_Valor7
        oraCommanSms.Parameters.Add("V_Valor8", OracleType.VarChar).Value = V_Valor8
        oraCommanSms.Parameters.Add("V_Valor9", OracleType.VarChar).Value = V_Valor9
        oraCommanSms.Parameters.Add("V_Valor10", OracleType.VarChar).Value = V_Valor10
        oraCommanSms.Parameters.Add("V_Bandera", OracleType.VarChar).Value = V_Bandera
        oraCommanSms.Parameters.Add("cv_1", OracleType.Cursor).Direction = ParameterDirection.Output
        Dim DtsNegociaciones As DataSet = Consulta_Procedure(oraCommanSms, "ELEMENTOS")
        Return DtsNegociaciones
    End Function
End Class
