Imports Microsoft.VisualBasic
Imports System.Data.OracleClient
Imports Db
Imports System.Globalization
Imports System.IO
Imports System.Web.SessionState.HttpSessionState
Imports System.Data
Public Class Class_EtapasJudiciales
    Public Shared Function LlenarElementos(ByVal V_HIST_JU_CREDITO As String, ByVal V_HIST_JU_PRODUCTO As String, ByVal V_HIST_JU_ABOGADO As String, ByVal V_HIST_JU_ETAPA As String, ByVal v_PR_JU_IDETAPA As String, ByVal V_HIST_JU_COMENTARIO As String, ByVal V_HIST_JU_AGENCIA As String, ByVal V_HIST_JU_TIPOJUICIO As String, ByVal V_HIST_JU_ESTADO As String, ByVal V_HIST_JU_DTEETAPA As String, ByVal V_Bandera As String) As Object
        Dim oraCommand As New OracleCommand
        oraCommand.CommandText = "SP_ADD_GESTION_JUDICIAL"
        oraCommand.CommandType = CommandType.StoredProcedure
        oraCommand.Parameters.Add("V_HIST_JU_CREDITO", OracleType.VarChar).Value = V_HIST_JU_CREDITO
        oraCommand.Parameters.Add("V_HIST_JU_PRODUCTO", OracleType.VarChar).Value = V_HIST_JU_PRODUCTO
        oraCommand.Parameters.Add("V_HIST_JU_ABOGADO", OracleType.VarChar).Value = V_HIST_JU_ABOGADO
        oraCommand.Parameters.Add("V_HIST_JU_ETAPA", OracleType.VarChar).Value = V_HIST_JU_ETAPA
        oraCommand.Parameters.Add("v_PR_JU_IDETAPA", OracleType.VarChar).Value = v_PR_JU_IDETAPA
        oraCommand.Parameters.Add("V_HIST_JU_COMENTARIO", OracleType.VarChar).Value = V_HIST_JU_COMENTARIO
        oraCommand.Parameters.Add("V_HIST_JU_AGENCIA", OracleType.VarChar).Value = V_HIST_JU_AGENCIA
        oraCommand.Parameters.Add("V_HIST_JU_TIPOJUICIO", OracleType.VarChar).Value = V_HIST_JU_TIPOJUICIO
        oraCommand.Parameters.Add("V_HIST_JU_ESTADO", OracleType.VarChar).Value = V_HIST_JU_ESTADO
        oraCommand.Parameters.Add("V_HIST_JU_DTEETAPA", OracleType.VarChar).Value = V_HIST_JU_DTEETAPA
        oraCommand.Parameters.Add("V_Bandera", OracleType.VarChar).Value = V_Bandera
        oraCommand.Parameters.Add("cv_1", OracleType.Cursor).Direction = ParameterDirection.Output
        Dim DtsNegociaciones As DataSet = Consulta_Procedure(oraCommand, "ELEMENTOS")
        Return DtsNegociaciones
    End Function
    Public Shared Function DatosJuicio(ByVal V_PR_JU_JUZGADO As String, ByVal V_PR_JU_LOCALIDAD As String, ByVal V_PR_JU_EXPEDIENTE As String, ByVal V_PR_JU_ABOGADO As String, ByVal v_PR_JU_CREDITO As String, ByVal V_Bandera As String) As Object
        Dim oraCommand As New OracleCommand
        oraCommand.CommandText = "SP_ADD_DATOS_JUDICIAL"
        oraCommand.CommandType = CommandType.StoredProcedure
        oraCommand.Parameters.Add("V_PR_JU_JUZGADO", OracleType.VarChar).Value = V_PR_JU_JUZGADO
        oraCommand.Parameters.Add("V_PR_JU_LOCALIDAD", OracleType.VarChar).Value = V_PR_JU_LOCALIDAD
        oraCommand.Parameters.Add("V_PR_JU_EXPEDIENTE", OracleType.VarChar).Value = V_PR_JU_EXPEDIENTE
        oraCommand.Parameters.Add("V_PR_JU_ABOGADO", OracleType.VarChar).Value = V_PR_JU_ABOGADO
        oraCommand.Parameters.Add("v_PR_JU_CREDITO", OracleType.VarChar).Value = v_PR_JU_CREDITO
        oraCommand.Parameters.Add("V_Bandera", OracleType.VarChar).Value = V_Bandera
        oraCommand.Parameters.Add("cv_1", OracleType.Cursor).Direction = ParameterDirection.Output
        Dim DtsNegociaciones As DataSet = Consulta_Procedure(oraCommand, "ELEMENTOS")
        Return DtsNegociaciones
    End Function
End Class
