Imports Microsoft.VisualBasic

Public Class QRegreso
   Public Shared Function QCampo(ByVal V_Cadena As String, ByVal V_Valor As Integer) As String
        Dim valor As String = V_Cadena.Replace("""", "").Split(":")(V_Valor).Split(",")(0)
        Return valor
    End Function
End Class

