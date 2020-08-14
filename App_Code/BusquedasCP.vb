Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Data
Imports System.Data.OracleClient
Imports Db
Imports System.IO
Imports System.Collections.Generic

<System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class BusquedasCP
    Inherits System.Web.Services.WebService
    <WebMethod(True)> _
    Public Function BuscarCodigo(ByVal prefixText As String, ByVal count As Integer) As System.String()
        Dim patronDatos As New List(Of String)
        Dim oraCommand As New OracleCommand

        oraCommand.CommandText = "SP_BUSQUEDA_CP"
        oraCommand.CommandType = CommandType.StoredProcedure
        oraCommand.Parameters.Add("V_PATRON", OracleType.VarChar).Value = "%" & prefixText.ToUpper & "%"
        oraCommand.Parameters.Add("V_BANDERA", OracleType.Number).Value = 1
        oraCommand.Parameters.Add("cv_1", OracleType.Cursor).Direction = ParameterDirection.Output
        Dim registros As DataSet = Consulta_Procedure(oraCommand, "Busca")
        Dim Source As DataView = registros.Tables("Busca").DefaultView
        If Source.Count <> 0 Then
            For indice As Integer = 0 To Source.Count - 1
                patronDatos.Add(registros.Tables("Busca").Rows(indice)("CAT_SE_CODIGO"))
            Next
        End If
        Return patronDatos.ToArray()
    End Function
End Class