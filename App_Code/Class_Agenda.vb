Imports Microsoft.VisualBasic
Imports System.Data.OracleClient
Imports Db
Imports System.Globalization
Imports System.IO
Imports Funciones
Imports System.Web.SessionState.HttpSessionState
Imports System.Data
Public Class Class_Agenda
    Public Shared Function LlenarElementosAgenda(ByVal V_Valor As String, ByVal V_Usuario As String, ByVal V_Bandera As String) As DataSet
        Dim oraCommand As New OracleCommand
        oraCommand.CommandText = "Sp_Filasagenda"
        oraCommand.CommandType = CommandType.StoredProcedure
        oraCommand.Parameters.Add("V_Valor", OracleType.VarChar).Value = V_Valor
        oraCommand.Parameters.Add("V_Usuario", OracleType.VarChar).Value = V_Usuario
        oraCommand.Parameters.Add("V_Bandera", OracleType.Number).Value = V_Bandera
        oraCommand.Parameters.Add("cv_1", OracleType.Cursor).Direction = ParameterDirection.Output
        Dim DtsHist_Act As DataSet = Consulta_Procedure(oraCommand, "ELEMENTOS")
        Return DtsHist_Act
    End Function

End Class
