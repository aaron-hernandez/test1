Imports System.Data
Imports Microsoft.VisualBasic
Imports System.Data.OracleClient
Imports Db
Imports System.Globalization
Imports System.IO
Imports Funciones
Partial Class Relacionados
    Inherits System.Web.UI.UserControl
    Protected Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
        Try
            Dim USR As String = (CType(Session("Usuario"), USUARIO)).CAT_LO_USUARIO
        Catch ex As Exception
            Session.Clear()
            Session.Abandon()
            Response.Redirect("LogIn.aspx")
        End Try
        Try
            Dim USUARIO As USUARIO = CType(Session("Usuario"), USUARIO)
            LblCat_Lo_Usuario.Text = USUARIO.CAT_LO_USUARIO
        Catch ex As Exception
            SendMail("Page_PreInit", ex, "", "", "")
        End Try
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If ValidaLicencias(300, LblCat_Lo_Usuario.Text, "", "", 1, "") = 1 Then
                If Val(CType(Session("Usuario"), USUARIO).CAT_LO_MGESTION) = 0 Then
                    Response.Redirect("LogIn.aspx")
                Else
                    If Not IsPostBack Then
                        Llenar()
                    End If
                End If
            Else
                Session.Clear()
                Session.Abandon()
                Response.Redirect("ExpiroSesion.aspx")
            End If
        Catch ex As Exception
            SendMail("Page_Load", ex, "", "", LblCat_Lo_Usuario.Text)
        End Try
    End Sub

    Sub Llenar()
        Dim TmpCredito As Credito = CType(Session("Credito"), Credito)
        Dim TmpUsr As USUARIO = CType(Session("USUARIO"), USUARIO)
        If TmpCredito.PR_MC_CREDITO <> "" Then
            'GvRelacionados.DataSource = Class_Relacionados.LlenarElementosRelacionados(TmpCredito.PR_CD_NUMERO_CLIENTE, TmpCredito.PR_MC_CREDITO, TmpUsr.CAT_LO_CADENAPRODUCTOS, 0) ''CREDITO RELACIONADO POR???
            GvRelacionados.DataBind()
            For Each GVrow As GridView In GvRelacionados.Rows

            Next
        End If
    End Sub
    Private Sub SendMail(ByRef evento As String, ByVal ex As Exception, ByVal Cuenta As String, ByVal Captura As String, ByVal usr As String)
        EnviarCorreo("Relacionados.aspx", evento, ex, Cuenta, Captura, usr)
    End Sub

    Protected Sub GvRelacionados_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GvRelacionados.SelectedIndexChanged
        Dim Tmpcrdt As Credito = (CType(Session("Credito"), Credito))
        Dim row As GridViewRow = GvRelacionados.SelectedRow
        Class_MasterPage.Tocar(row.Cells(1).Text, LblCat_Lo_Usuario.Text)
        LlenarCredito.Busca(row.Cells(1).Text)
        Session("Buscar") = 1
        Tmpcrdt.PR_MC_CUENTATRABAJADAFILA = 1
        Response.Redirect("MasterPage.aspx", False)
    End Sub
End Class
