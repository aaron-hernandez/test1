Imports Microsoft.VisualBasic
Imports System.Data.OracleClient
Imports Db
Imports System.Web.SessionState.HttpSessionState
Imports System.Data

Public Class LlenarAplicacion

    Public Shared Sub APLICACION()
        Dim oraCommand As New OracleCommand
        oraCommand.CommandText = "SP_CATALOGOS"
        oraCommand.CommandType = CommandType.StoredProcedure
        oraCommand.Parameters.Add("V_BANDERA", OracleType.Number).Value = 1
        oraCommand.Parameters.Add("V_Valor", OracleType.VarChar).Value = ""
        oraCommand.Parameters.Add("cv_1", OracleType.Cursor).Direction = ParameterDirection.Output
        Dim DtsLlenarAplicacion As DataSet = Consulta_Procedure(oraCommand, "APLICACION")
        Dim DtviewLlenarAplicacion As DataView = DtsLlenarAplicacion.Tables("APLICACION").DefaultView

        Dim TmpAplicacion As Aplicacion = HttpContext.Current.Session("Aplicacion")

        For a As Integer = 0 To DtviewLlenarAplicacion.Count - 1
            Select Case DtsLlenarAplicacion.Tables(0).Rows(a)("CAT_VA_DESCRIPCION")
                Case Is = "Letra"
                    TmpAplicacion.LETRA = DtsLlenarAplicacion.Tables(0).Rows(a)("CAT_VA_VALOR")
                Case Is = "Consecutivo"
                    TmpAplicacion.CONSECUTIVO = DtsLlenarAplicacion.Tables(0).Rows(a)("CAT_VA_VALOR")
                Case Is = "PROMESAS DE PAGO"
                    TmpAplicacion.PROMESAS_PAGO = DtsLlenarAplicacion.Tables(0).Rows(a)("CAT_VA_VALOR")
                Case Is = "Negociaciones"
                    TmpAplicacion.NEGOCIACIONES = DtsLlenarAplicacion.Tables(0).Rows(a)("CAT_VA_VALOR")
                Case Is = "Codigo Accion"
                    TmpAplicacion.ACCION = DtsLlenarAplicacion.Tables(0).Rows(a)("CAT_VA_VALOR")
                Case Is = "Codigo Resultado"
                    TmpAplicacion.RESULTADO = DtsLlenarAplicacion.Tables(0).Rows(a)("CAT_VA_VALOR")
                Case Is = "Codigo Causa No Pago"
                    TmpAplicacion.NOPAGO = DtsLlenarAplicacion.Tables(0).Rows(a)("CAT_VA_VALOR")
            End Select
        Next
    End Sub
End Class
