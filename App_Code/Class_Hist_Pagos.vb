Imports Microsoft.VisualBasic
Imports System.Data.OracleClient
Imports Db
Imports System.Globalization
Imports System.IO
Imports Funciones
Imports System.Web.SessionState.HttpSessionState
Imports System.Data
Public Class Class_Hist_Pagos
    Public Shared Function LlenarElementosHist_Pagos(ByVal V_Credito As String, ByVal V_Bandera As String) As Object
        Dim oraCommand As New OracleCommand
        oraCommand.CommandText = "SP_HISTORICO_PAGOS"
        oraCommand.CommandType = CommandType.StoredProcedure
        oraCommand.Parameters.Add("V_Credito", OracleType.VarChar).Value = V_Credito
        oraCommand.Parameters.Add("V_Bandera", OracleType.VarChar).Value = V_Bandera
        oraCommand.Parameters.Add("cv_1", OracleType.Cursor).Direction = ParameterDirection.Output
        Dim DtsTelefonos As DataSet = Consulta_Procedure(oraCommand, "ELEMENTOS")
        Return DtsTelefonos
    End Function
    Public Shared Function AgregarPago(ByVal V_Hist_Pa_Credito As String, ByVal V_Hist_Pa_Producto As String, ByVal V_Hist_Pa_Usuario As String, ByVal V_Hist_Pa_Referencia As String, ByVal V_Hist_Pa_Montopago As String, ByVal V_Hist_Pa_Dtepago As String, ByVal V_Hist_Pa_Confirmacion As String, ByVal V_Hist_Pa_Lugarpago As String, ByVal V_Hist_Pa_Agencia As String) As String
        If V_Hist_Pa_Dtepago = "" Then
            Return "Seleccione La fecha De Pago"
        ElseIf Funciones.ValidaMonto(V_Hist_Pa_Montopago) = 1 Then
            Return "Monto Incorrecto"
        ElseIf Val(V_Hist_Pa_Montopago) <= 0 Then
            Return "El Monto Del Pago Debe De Ser Mayor A 0"
        ElseIf V_Hist_Pa_Montopago = "" Then
            Return "Capture Un Monto"
        ElseIf V_Hist_Pa_Lugarpago = "Seleccione" Then
            Return "Seleccione Lugar De Pago"
        ElseIf V_Hist_Pa_Referencia = "" Then
            Return "Capture Una Referencia"
        ElseIf V_Hist_Pa_Confirmacion = "Seleccione" Then
            Return "Seleccione Tipo De Confirmación"
        Else
            Dim oraCommand As New OracleCommand
            oraCommand.CommandText = "SP_ADD_HIST_PAGOS"
            oraCommand.CommandType = CommandType.StoredProcedure
            oraCommand.Parameters.Add("V_Hist_Pa_Credito", OracleType.VarChar).Value = V_Hist_Pa_Credito
            oraCommand.Parameters.Add("V_Hist_Pa_Producto", OracleType.VarChar).Value = V_Hist_Pa_Producto
            oraCommand.Parameters.Add("V_Hist_Pa_Usuario", OracleType.VarChar).Value = V_Hist_Pa_Usuario
            oraCommand.Parameters.Add("V_Hist_Pa_Referencia", OracleType.VarChar).Value = V_Hist_Pa_Referencia
            oraCommand.Parameters.Add("V_Hist_Pa_Montopago", OracleType.Number).Value = V_Hist_Pa_Montopago
            oraCommand.Parameters.Add("V_Hist_Pa_Dtepago", OracleType.VarChar).Value = V_Hist_Pa_Dtepago
            oraCommand.Parameters.Add("V_Hist_Pa_Confirmacion", OracleType.VarChar).Value = V_Hist_Pa_Confirmacion
            oraCommand.Parameters.Add("V_Hist_Pa_Lugarpago", OracleType.VarChar).Value = V_Hist_Pa_Lugarpago
            oraCommand.Parameters.Add("V_Hist_Pa_Agencia", OracleType.VarChar).Value = V_Hist_Pa_Agencia
            oraCommand.Parameters.Add("cv_1", OracleType.Cursor).Direction = ParameterDirection.Output
            Dim DtsPagos As DataSet = Consulta_Procedure(oraCommand, "Pagos")
            Return DtsPagos.Tables("Pagos").Rows(0).Item("Pago")
        End If
    End Function
End Class
