Imports Microsoft.VisualBasic
Imports System.IO
Imports System.Data.OracleClient
Imports System.Data
Imports Db
Imports Conexiones

Public Class Db

    Public Shared Function StrRuta() As String
        StrRuta = "E:\Cargas\FINEM\"
        Return StrRuta
    End Function

    Public Shared Sub SubExisteRuta(ByRef Ruta As String)
        If Not Directory.Exists(Ruta) Then
            Directory.CreateDirectory(Ruta)
        End If
    End Sub

    Public Shared Function Conectando() As String
        Dim conexion As String = "Data Source=" & DesEncriptarCadena(StrConexion(3)) & ";Persist Security Info=True;User ID=" & DesEncriptarCadena(StrConexion(1)) & ";Password=" & DesEncriptarCadena(StrConexion(2)) & ""
        Return conexion

    End Function

    Public Shared Function Consulta(ByVal Query As String, ByVal Nombre As String) As DataSet
        Dim oDataset As New DataSet
        Dim conexion As String = Conectando()
        Dim objConexion As New OracleConnection(conexion)
        Dim objCommand As New OracleCommand(Query, objConexion)
        Dim objAdapter As New OracleDataAdapter
        objAdapter.SelectCommand = objCommand
        objAdapter.Fill(oDataset, Nombre)
        Return oDataset
    End Function

    Public Shared Function Consulta_Procedure(ByVal command As OracleCommand, ByVal Nombre As String) As DataSet
        Dim oDataset As New DataSet
        Dim conexion As String = Conectando()
        Dim objConexion As New OracleConnection(conexion)
        command.Connection = objConexion
        Dim oDAtabla1 As New OracleDataAdapter(command)
        oDAtabla1.Fill(oDataset, Nombre)
        Return oDataset
    End Function

    Public Shared Sub Ejecuta_Procedure(ByVal command As OracleCommand)
        Dim conexion As String = Conectando()
        Dim objConexion As New OracleConnection(conexion)
        If objConexion.State = ConnectionState.Closed Then
            objConexion.Open()
        End If
        command.Connection = objConexion
        Dim drDatos As OracleDataReader
        drDatos = command.ExecuteReader
        drDatos.Close()
        objConexion.Close()
    End Sub

    Public Shared Sub Ejecuta(ByVal Query As String)
        Dim conexion As String = Conectando()
        Dim objConexion As New OracleConnection(conexion)
        If objConexion.State = ConnectionState.Closed Then
            objConexion.Open()
        End If
        Dim MyQuery As New OracleCommand(Query, objConexion)
        MyQuery.ExecuteNonQuery()
        objConexion.Close()
    End Sub

    Public Shared Function DesEncriptarCadena(ByVal cadena As String) As String
        Dim idx As Integer
        Dim result As String
        For idx = 0 To cadena.Length - 1
            result += DesEncriptarCaracter(cadena.Substring(idx, 1), cadena.Length, idx)
        Next
        Return result
    End Function

    Public Shared Function DesEncriptarCaracter(ByVal caracter As String, ByVal variable As Integer, ByVal a_indice As Integer) As String
        Dim patron_busqueda As String = "qpwoeirutyQPWOEIRUTYañsld1234567890kfjghAÑSLDKFJGHzmxncbvZMXNCBV."
        Dim Patron_encripta As String = "zmxncbvZMXNCBVañsldkfjghAÑ.SLDKFJGHqpwoeirutyQPWOEIRUTY0987654321"

        Dim indice As Integer
        If Patron_encripta.IndexOf(caracter) <> -1 Then
            If (Patron_encripta.IndexOf(caracter) - variable - a_indice) > 0 Then
                indice = (Patron_encripta.IndexOf(caracter) - variable - a_indice) Mod Patron_encripta.Length
            Else
                indice = (patron_busqueda.Length) + ((Patron_encripta.IndexOf(caracter) - variable - a_indice) Mod Patron_encripta.Length)
            End If
            indice = indice Mod Patron_encripta.Length
            Return patron_busqueda.Substring(indice, 1)
        Else
            Return caracter
        End If
    End Function

End Class
