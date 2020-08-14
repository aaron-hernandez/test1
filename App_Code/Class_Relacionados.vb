Imports Microsoft.VisualBasic
Imports System.Data.OracleClient
Imports Db
Imports System.Globalization
Imports System.IO
Imports Funciones
Imports System.Web.SessionState.HttpSessionState
Imports System.Data
Public Class Class_Relacionados
    Public Shared Function LlenarElementosRelacionados(ByVal V_NumCliente As String, ByVal V_Credito As String, ByVal V_Producto As String, ByVal V_Bandera As String) As Object
        Dim oraCommand As New OracleCommand
        oraCommand.CommandText = "SP_CREDITOS_RELACIONADOS"
        oraCommand.CommandType = CommandType.StoredProcedure
        oraCommand.Parameters.Add("V_NumCliente", OracleType.VarChar).Value = V_NumCliente
        oraCommand.Parameters.Add("V_Credito", OracleType.VarChar).Value = V_Credito
        oraCommand.Parameters.Add("V_Producto", OracleType.VarChar).Value = V_Producto
        oraCommand.Parameters.Add("V_Bandera", OracleType.VarChar).Value = V_Bandera
        oraCommand.Parameters.Add("cv_1", OracleType.Cursor).Direction = ParameterDirection.Output
        Dim DtsTelefonos As DataSet = Consulta_Procedure(oraCommand, "ELEMENTOS")
        Return DtsTelefonos
    End Function
End Class
