Imports Microsoft.VisualBasic

Public Class USUARIO
    Public Sub New(
   ByVal CAT_LO_ID As String,
   ByVal CAT_LO_USUARIO As String,
   ByVal CAT_LO_NOMBRE As String,
   ByVal CAT_LO_CONTRASENA As String,
   ByVal CAT_PE_PERFIL As String,
   ByVal CAT_LO_PERFIL As String,
   ByVal CAT_LO_SUPERVISOR As String,
   ByVal CAT_LO_DTEALTA As String,
   ByVal CAT_LO_MOTIVO As String,
   ByVal CAT_LO_MGESTION As String,
   ByVal CAT_LO_PGESTION As String,
   ByVal CAT_LO_MADMINISTRADOR As String,
   ByVal CAT_LO_MREPORTES As String,
   ByVal CAT_LO_HENTRADA As String,
   ByVal CAT_LO_HSALIDA As String,
   ByVal CAT_LO_AGENCIA As String,
   ByVal CAT_LO_PRODUCTOS As String,
   ByVal CAT_LO_PRODUCTO As String,
   ByVal CAT_LO_CADENAPRODUCTOS As String,
   ByVal CAT_LO_AGENCIASVER As String,
   ByVal CAT_LO_CADENAAGENCIAS As String,
   ByVal CAT_LO_ESTATUS As String,
   ByVal CAT_AG_ESTATUS As String,
   ByVal CUsuario As String,
   ByVal cat_Lo_Num_Agencia As String,
   ByVal Licencias As Integer,
   ByVal CAT_LO_HEREDAR As String,
   ByVal GESTIONES As String,
   ByVal CUANTASPP As String,
   ByVal MONTOPP As String,
   ByVal CUANTASPPC As String,
   ByVal MONTOPPC As String
   )
        Me._CAT_LO_ID = CAT_LO_ID
        Me._CAT_LO_USUARIO = CAT_LO_USUARIO
        Me._CAT_LO_NOMBRE = CAT_LO_NOMBRE
        Me._CAT_LO_CONTRASENA = CAT_LO_CONTRASENA
        Me._CAT_PE_PERFIL = CAT_PE_PERFIL
        Me._CAT_LO_PERFIL = CAT_LO_PERFIL
        Me._CAT_LO_SUPERVISOR = CAT_LO_SUPERVISOR
        Me._CAT_LO_DTEALTA = CAT_LO_DTEALTA
        Me._CAT_LO_MOTIVO = CAT_LO_MOTIVO
        Me._CAT_LO_MGESTION = CAT_LO_MGESTION
        Me._CAT_LO_PGESTION = CAT_LO_PGESTION
        Me._CAT_LO_MADMINISTRADOR = CAT_LO_MADMINISTRADOR
        Me._CAT_LO_MREPORTES = CAT_LO_MREPORTES
        Me._CAT_LO_HENTRADA = CAT_LO_HENTRADA
        Me._CAT_LO_HSALIDA = CAT_LO_HSALIDA
        Me._CAT_LO_AGENCIA = CAT_LO_AGENCIA
        Me._CAT_LO_PRODUCTOS = CAT_LO_PRODUCTOS
        Me._CAT_LO_PRODUCTO = CAT_LO_PRODUCTO
        Me._CAT_LO_CADENAPRODUCTOS = CAT_LO_CADENAPRODUCTOS
        Me._CAT_LO_AGENCIASVER = CAT_LO_AGENCIASVER
        Me._CAT_LO_CADENAAGENCIAS = CAT_LO_CADENAAGENCIAS
        Me._CAT_LO_ESTATUS = CAT_LO_ESTATUS
        Me._CAT_AG_ESTATUS = CAT_AG_ESTATUS
        Me._CUsuario = CUsuario
        Me._cat_Lo_Num_Agencia = cat_Lo_Num_Agencia
        Me._Licencias = Licencias
        Me._CAT_LO_HEREDAR = CAT_LO_HEREDAR
        Me._GESTIONES = GESTIONES
        Me._CUANTASPP = CUANTASPP
        Me._MONTOPP = MONTOPP
        Me._CUANTASPPC = CUANTASPPC
        Me._MONTOPPC = MONTOPPC


    End Sub


    Private _CAT_LO_ID As String
    Property CAT_LO_ID() As String
        Get
            Return _CAT_LO_ID
        End Get
        Set(ByVal value As String)
            _CAT_LO_ID = value
        End Set
    End Property

    Private _CAT_LO_USUARIO As String
    Property CAT_LO_USUARIO() As String
        Get
            Return _CAT_LO_USUARIO
        End Get
        Set(ByVal value As String)
            _CAT_LO_USUARIO = value
        End Set
    End Property

    Private _CAT_LO_NOMBRE As String
    Property CAT_LO_NOMBRE() As String
        Get
            Return _CAT_LO_NOMBRE
        End Get
        Set(ByVal value As String)
            _CAT_LO_NOMBRE = value
        End Set
    End Property

    Private _CAT_LO_CONTRASENA As String
    Property CAT_LO_CONTRASENA() As String
        Get
            Return _CAT_LO_CONTRASENA
        End Get
        Set(ByVal value As String)
            _CAT_LO_CONTRASENA = value
        End Set
    End Property

    Private _CAT_PE_PERFIL As String
    Property CAT_PE_PERFIL() As String
        Get
            Return _CAT_PE_PERFIL
        End Get
        Set(ByVal value As String)
            _CAT_PE_PERFIL = value
        End Set
    End Property
    Private _CAT_LO_PERFIL As String
    Property CAT_LO_PERFIL() As String
        Get
            Return _CAT_LO_PERFIL
        End Get
        Set(ByVal value As String)
            _CAT_LO_PERFIL = value
        End Set
    End Property
    Private _CAT_LO_SUPERVISOR As String
    Property CAT_LO_SUPERVISOR() As String
        Get
            Return _CAT_LO_SUPERVISOR
        End Get
        Set(ByVal value As String)
            _CAT_LO_SUPERVISOR = value
        End Set
    End Property

    Private _CAT_LO_DTEALTA As String
    Property CAT_LO_DTEALTA() As String
        Get
            Return _CAT_LO_DTEALTA
        End Get
        Set(ByVal value As String)
            _CAT_LO_DTEALTA = value
        End Set
    End Property

    Private _CAT_LO_MOTIVO As String
    Property CAT_LO_MOTIVO() As String
        Get
            Return _CAT_LO_MOTIVO
        End Get
        Set(ByVal value As String)
            _CAT_LO_MOTIVO = value
        End Set
    End Property

    Private _CAT_LO_MGESTION As String
    Property CAT_LO_MGESTION() As String
        Get
            Return _CAT_LO_MGESTION
        End Get
        Set(ByVal value As String)
            _CAT_LO_MGESTION = value
        End Set
    End Property

    Private _CAT_LO_PGESTION As String
    Property CAT_LO_PGESTION() As String
        Get
            Return _CAT_LO_PGESTION
        End Get
        Set(ByVal value As String)
            _CAT_LO_PGESTION = value
        End Set
    End Property

    Private _CAT_LO_MADMINISTRADOR As String
    Property CAT_LO_MADMINISTRADOR() As String
        Get
            Return _CAT_LO_MADMINISTRADOR
        End Get
        Set(ByVal value As String)
            _CAT_LO_MADMINISTRADOR = value
        End Set
    End Property
    Private _CAT_LO_MREPORTES As String
    Property CAT_LO_MREPORTES() As String
        Get
            Return _CAT_LO_MREPORTES
        End Get
        Set(ByVal value As String)
            _CAT_LO_MREPORTES = value
        End Set
    End Property


    Private _CAT_LO_HENTRADA As String
    Property CAT_LO_HENTRADA() As String
        Get
            Return _CAT_LO_HENTRADA
        End Get
        Set(ByVal value As String)
            _CAT_LO_HENTRADA = value
        End Set
    End Property

    Private _CAT_LO_HSALIDA As String
    Property CAT_LO_HSALIDA() As String
        Get
            Return _CAT_LO_HSALIDA
        End Get
        Set(ByVal value As String)
            _CAT_LO_HSALIDA = value
        End Set
    End Property

    Private _CAT_LO_AGENCIA As String
    Property CAT_LO_AGENCIA() As String
        Get
            Return _CAT_LO_AGENCIA
        End Get
        Set(ByVal value As String)
            _CAT_LO_AGENCIA = value
        End Set
    End Property

    Private _CAT_LO_PRODUCTOS As String
    Property CAT_LO_PRODUCTOS() As String
        Get
            Return _CAT_LO_PRODUCTOS
        End Get
        Set(ByVal value As String)
            _CAT_LO_PRODUCTOS = value
        End Set
    End Property

    Private _CAT_LO_PRODUCTO As String
    Property CAT_LO_PRODUCTO() As String
        Get
            Return _CAT_LO_PRODUCTO
        End Get
        Set(ByVal value As String)
            _CAT_LO_PRODUCTO = value
        End Set
    End Property
    Private _CAT_LO_CADENAPRODUCTOS As String
    Property CAT_LO_CADENAPRODUCTOS() As String
        Get
            Return _CAT_LO_CADENAPRODUCTOS
        End Get
        Set(ByVal value As String)
            _CAT_LO_CADENAPRODUCTOS = value
        End Set
    End Property
    Private _CAT_LO_AGENCIASVER As String
    Property CAT_LO_AGENCIASVER() As String
        Get
            Return _CAT_LO_AGENCIASVER
        End Get
        Set(ByVal value As String)
            _CAT_LO_AGENCIASVER = value
        End Set
    End Property
    Private _CAT_LO_CADENAAGENCIAS As String
    Property CAT_LO_CADENAAGENCIAS() As String
        Get
            Return _CAT_LO_CADENAAGENCIAS
        End Get
        Set(ByVal value As String)
            _CAT_LO_CADENAAGENCIAS = value
        End Set
    End Property
    Private _CAT_LO_ESTATUS As String
    Property CAT_LO_ESTATUS() As String
        Get
            Return _CAT_LO_ESTATUS
        End Get
        Set(ByVal value As String)
            _CAT_LO_ESTATUS = value
        End Set
    End Property
    Private _CAT_AG_ESTATUS As String
    Property CAT_AG_ESTATUS() As String
        Get
            Return _CAT_AG_ESTATUS
        End Get
        Set(ByVal value As String)
            _CAT_AG_ESTATUS = value
        End Set
    End Property
    Private _CUsuario As String
    Property CUsuario() As String
        Get
            Return _CUsuario
        End Get
        Set(ByVal value As String)
            _CUsuario = value
        End Set
    End Property
    Private _cat_Lo_Num_Agencia As String
    Property cat_Lo_Num_Agencia() As String
        Get
            Return _cat_Lo_Num_Agencia
        End Get
        Set(ByVal value As String)
            _cat_Lo_Num_Agencia = value
        End Set
    End Property

    Private _Licencias As String
    Property Licencias() As String
        Get
            Return _Licencias
        End Get
        Set(ByVal value As String)
            _Licencias = value
        End Set
    End Property
    Private _CAT_LO_HEREDAR As String
    Property CAT_LO_HEREDAR() As String
        Get
            Return _CAT_LO_HEREDAR
        End Get
        Set(ByVal value As String)
            _CAT_LO_HEREDAR = value
        End Set
    End Property
    Private _GESTIONES As String
    Property GESTIONES() As String
        Get
            Return _GESTIONES
        End Get
        Set(ByVal value As String)
            _GESTIONES = value
        End Set
    End Property
    Private _CUANTASPP As String
    Property CUANTASPP() As String
        Get
            Return _CUANTASPP
        End Get
        Set(ByVal value As String)
            _CUANTASPP = value
        End Set
    End Property
    Private _MONTOPP As String
    Property MONTOPP() As String
        Get
            Return _MONTOPP
        End Get
        Set(ByVal value As String)
            _MONTOPP = value
        End Set
    End Property
    Private _CUANTASPPC As String
    Property CUANTASPPC() As String
        Get
            Return _CUANTASPPC
        End Get
        Set(ByVal value As String)
            _CUANTASPPC = value
        End Set
    End Property
    Private _MONTOPPC As String
    Property MONTOPPC() As String
        Get
            Return _MONTOPPC
        End Get
        Set(ByVal value As String)
            _MONTOPPC = value
        End Set
    End Property
End Class