Imports Microsoft.VisualBasic
Imports Db
Imports QParametros
Imports System.IO
Imports System.Xml
Imports System.Data
Imports System.Net

Public Class Quiubas
     Public Shared Function ExecutarCurl(ByVal V_URL As String, ByVal V_Telefono As String, ByVal V_MSJ As String, ByVal V_Tipo As String, ByVal V_Bandera As String) As String
        Dim data As String = ""
        If V_Bandera = "1" Then
            data = "to_number=%2B52" & V_Telefono & "&message=" & V_MSJ & "&test_mode=false"
        End If
        Dim CUrl As WebRequest = WebRequest.Create(V_URL)
        CUrl.Method = V_Tipo
        CUrl.ContentLength = data.Length
        CUrl.ContentType = "application/json; charset=UTF-8"
        Dim enc As New UTF8Encoding()
        Dim DtsParametros As DataSet = Class_SMS.LlenarElementos("", "", "", "", "", "", "", "", "", "", 0)
        Dim v_username As String = DtsParametros.Tables(0).Rows(0).Item(0)
        Dim v_password As String = DtsParametros.Tables(0).Rows(1).Item(0)
        CUrl.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(enc.GetBytes(v_username & ":" & v_password)))

        If V_Bandera = "1" Then
            Using ds As Stream = CUrl.GetRequestStream()
                ds.Write(enc.GetBytes(data), 0, data.Length)
            End Using
        End If

        Dim wr As WebResponse = CUrl.GetResponse()
        Dim receiveStream As Stream = wr.GetResponseStream()
        Dim reader As New StreamReader(receiveStream, Encoding.UTF8)
        Return reader.ReadToEnd()
    End Function

End Class