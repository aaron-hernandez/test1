Imports Microsoft.VisualBasic

Public Class SessionesUsr
    Public Sub New(ByVal UsuarioSesion As List(Of String),
                  ByVal Horario As String,
                  ByVal Ip As String)
        Me._UsuarioSesion = UsuarioSesion
        Me._Horario = Horario
        Me._Ip = Ip
    End Sub

    Private _UsuarioSesion As List(Of String)
    Public Property UsuarioSesion() As List(Of String)
        Get
            Return _UsuarioSesion
        End Get
        Set(value As List(Of String))
            _UsuarioSesion = value
        End Set
    End Property

    Private _Horario As String
    Public Property Horario() As String
        Get
            Return _Horario
        End Get
        Set(value As String)
            _Horario = value
        End Set
    End Property
    Private _Ip As String
    Public Property Ip() As String
        Get
            Return _Ip
        End Get
        Set(value As String)
            _Ip = value
        End Set
    End Property
End Class
