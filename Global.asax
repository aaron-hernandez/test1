<%@ Application Language="VB" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.OracleClient" %>

<script RunAt="server">
    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        'Dim oraCommand As New OracleCommand
        'oraCommand.CommandText = "SP_VARIOS_QRS"
        'oraCommand.CommandType = CommandType.StoredProcedure
        'oraCommand.Parameters.Add("V_USUARIO", OracleType.VarChar).Value = ""
        'oraCommand.Parameters.Add("V_VALOR", OracleType.Number).Value = 0
        'oraCommand.Parameters.Add("V_BANDERA", OracleType.Number).Value = 4
        'oraCommand.Parameters.Add("CV_1", OracleType.Cursor).Direction = ParameterDirection.Output
        'Dim DtsVariable As DataSet = Conexiones.Consulta_Procedure(oraCommand, "Licencias")
        'Dim Licencias(DtsVariable.Tables(0).Rows(0).Item("Cuantas"), 2) As String
        'Application("UsersLoggedIn") =  Licencias
        'Dim StrinUsr As String = CType(Session("Usuario"), USUARIO).CAT_LO_USUARIO
    End Sub
    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        Dim userLoggedIn As String = Session("UserLoggedIn")

        Dim DtsDisponibles As DataSet = Class_Sesion.LlenarElementos(userLoggedIn, "Finem", "Gestion", "Inactividad Por 3 Minutos", 5, "", "", "Cierre De Sesion", CType(Session("IdSession"), String))
    End Sub

    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' Código que se ejecuta al producirse un error no controlado   
    End Sub
    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        Dim userLoggedIn As String = Session("UserLoggedIn")
        Dim DtsDisponibles As DataSet = Class_Sesion.LlenarElementos(userLoggedIn, "Finem", "Gestion", "Inactividad Por 3 Minutos", 5, "", "", "Cierre De Sesion", CType(Session("IdSession"), String))
    End Sub
</script>
