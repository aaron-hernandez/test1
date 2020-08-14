Imports Microsoft.VisualBasic
Imports System.Data.OracleClient
Imports Db
Imports System.Globalization
Imports System.IO
Imports Funciones
Imports System.Web.SessionState.HttpSessionState
Imports System.Data
Public Class Class_Sesion
    Public Shared Function LlenarElementos(ByVal V_USUARIO As String, ByVal V_CAMPANA As String, ByVal V_MODULO As String, ByVal V_MOTIVO As String, ByVal V_BANDERA As String, ByVal V_CONTRASENA As String, ByVal V_IP As String, ByVal V_EVENTO As String, ByVal V_HIST_LO_ID_LOGIN As String) As DataSet
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
        Dim DtsIngreso As DataSet = Consulta_Procedure(oraCommand, "ELEMENTOS")
        Return DtsIngreso
    End Function

End Class
