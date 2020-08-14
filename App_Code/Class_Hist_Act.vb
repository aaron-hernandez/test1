Imports Microsoft.VisualBasic
Imports System.Data.OracleClient
Imports Db
Imports System.Globalization
Imports System.IO
Imports Funciones
Imports System.Web.SessionState.HttpSessionState
Imports System.Data
Public Class Class_Hist_Act
    Public Shared Function LlenarElementosHistAct(ByVal V_Credito As String, ByVal V_Campo As String, ByVal V_Tipo As String, ByVal V_Bandera As Integer) As Object
        Dim oraCommand As New OracleCommand
        oraCommand.CommandText = "SP_HISTORICO_ACTIVIDADES"
        oraCommand.CommandType = CommandType.StoredProcedure
        oraCommand.Parameters.Add("V_Credito", OracleType.VarChar).Value = V_Credito
        oraCommand.Parameters.Add("V_Campo", OracleType.VarChar).Value = V_Campo
        oraCommand.Parameters.Add("V_Tipo", OracleType.VarChar).Value = V_Tipo
        oraCommand.Parameters.Add("V_Bandera", OracleType.Number).Value = V_Bandera
        oraCommand.Parameters.Add("cv_1", OracleType.Cursor).Direction = ParameterDirection.Output
        Dim DtsHist_Act As DataSet = Consulta_Procedure(oraCommand, "ELEMENTOS")
        Return DtsHist_Act
    End Function


End Class
