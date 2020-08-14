Imports Microsoft.VisualBasic
Imports System.Data.OracleClient
Imports Db
Imports System.Globalization
Imports System.IO
Imports Funciones
Imports System.Web.SessionState.HttpSessionState
Imports System.Data
Public Class Class_Informaciongeneral
    Public Shared Function LlenarElementosAgregar(ByVal V_CREDITO As String, ByVal V_Persona As String, ByVal V_usuario As String) As Object
        Dim oraCommand As New OracleCommand
        oraCommand.CommandText = "SP_AURORIZA_PERSONA"
        oraCommand.CommandType = CommandType.StoredProcedure
        oraCommand.Parameters.Add("V_CREDITO", OracleType.VarChar).Value = V_CREDITO
        oraCommand.Parameters.Add("V_Persona", OracleType.VarChar).Value = V_Persona
        oraCommand.Parameters.Add("V_usuario", OracleType.VarChar).Value = V_usuario
        oraCommand.Parameters.Add("cv_1", OracleType.Cursor).Direction = ParameterDirection.Output
        Dim DtsTelefonos As DataSet = Consulta_Procedure(oraCommand, "ELEMENTOS")
        Return DtsTelefonos
    End Function
End Class
