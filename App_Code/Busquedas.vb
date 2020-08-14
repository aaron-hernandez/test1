Imports System.Data
Imports Microsoft.VisualBasic
Imports System.Data.OracleClient
Imports Db
Imports System.Globalization
Imports System.IO
Imports Funciones
Imports System.Web.SessionState.HttpSessionState
Public Class Busquedas
    Public Shared Function Search(ByVal V_Valor As String) As Object

        Dim oraCommand As New OracleCommand
        oraCommand.CommandText = "SP_BUSQUEDAS"
        oraCommand.CommandType = CommandType.StoredProcedure
        oraCommand.Parameters.Add("v_valorBuscar", OracleType.VarChar).Value = V_Valor
        oraCommand.Parameters.Add("V_PRODUCTO", OracleType.VarChar).Value = CType(HttpContext.Current.Session("Usuario"), USUARIO).CAT_LO_CADENAPRODUCTOS
        oraCommand.Parameters.Add("V_AGENCIA", OracleType.VarChar).Value = CType(HttpContext.Current.Session("Usuario"), USUARIO).CAT_LO_CADENAAGENCIAS
        oraCommand.Parameters.Add("cv_1", OracleType.Cursor).Direction = ParameterDirection.Output
        Dim DtsBusca As DataSet = Consulta_Procedure(oraCommand, "Busca")
        Return DtsBusca
    End Function
End Class
