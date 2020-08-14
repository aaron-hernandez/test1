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
Public Class BusquedasUsr
    Inherits System.Web.Services.WebService
    <WebMethod(True)> _
    Public Function BuscarUsuario(ByVal prefixText As String, ByVal count As Integer) As System.String()
        Dim patronDatos As New List(Of String)
        Dim oraCommandBuscar As New OracleCommand
        oraCommandBuscar.CommandText = "SP_BUSQUEDA_USUARIO"
        oraCommandBuscar.CommandType = CommandType.StoredProcedure
        oraCommandBuscar.Parameters.Add("V_Patron", OracleType.VarChar).Value = "%" & prefixText.ToUpper & "%"
        oraCommandBuscar.Parameters.Add("V_Agencia", OracleType.VarChar).Value = CType(Session("Usuario"), USUARIO).cat_Lo_Num_Agencia
        oraCommandBuscar.Parameters.Add("V_Usuario", OracleType.VarChar).Value = CType(Session("Usuario"), USUARIO).CAT_LO_USUARIO
        oraCommandBuscar.Parameters.Add("V_Bandera", OracleType.VarChar).Value = 1
        oraCommandBuscar.Parameters.Add("cv_1", OracleType.Cursor).Direction = ParameterDirection.Output
        Dim registros As DataSet = Consulta_Procedure(oraCommandBuscar, "Busca")
        Dim Source As DataView = registros.Tables("Busca").DefaultView
        If Source.Count <> 0 Then
            For indice As Integer = 0 To Source.Count - 1
                patronDatos.Add(registros.Tables("Busca").Rows(indice)("USUARIO"))
            Next
        End If
        Return patronDatos.ToArray()
    End Function
End Class