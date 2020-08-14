Imports System.Data.OracleClient
Imports System.Data
Imports Db
Imports Funciones
Imports System.Web.Services
Imports System.Globalization
Imports Busquedas
Imports Class_Hist_Act
Imports System.Web.Script.Serialization
Partial Class Historicos_De_Actividades
    Inherits System.Web.UI.UserControl
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
                    If Not IsPostBack Then
                Llenar()
                GvHistActJudicial.Visible = False
            End If
        Catch ex As Exception
            SendMail("Page_Load", ex, "", "", "")
        End Try
    End Sub
    Sub Llenar()
        Dim TmpCredito As Credito = CType(Session("Credito"), Credito)
        If TmpCredito.PR_MC_CREDITO <> "" Then
            Try
                GvHistActMasivo.DataSource = Class_Hist_Act.LlenarElementosHistAct(TmpCredito.PR_MC_CREDITO, "Hist_Ge_Dteactividad", "DESC", 1)
                GvHistActMasivo.DataBind()

                'GvHistActJudicial.DataSource = Class_Hist_Act.LlenarElementosHistAct(TmpCredito.PR_MC_CREDITO, "Hist_Ge_Dteactividad", "DESC", 2)
                'GvHistActJudicial.DataBind()

                'GVHist_Atencion_C.DataSource = Class_Hist_Act.LlenarElementosHistAct(TmpCredito.PR_MC_CREDITO, "Hist_At_Dteactividad", "DESC", 3)
                'GVHist_Atencion_C.DataBind()

            Catch ex As Exception
                Dim EXCEPCION = ex
            End Try
        End If
    End Sub
  
    Protected Sub GvHistActMasivo_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GvHistActMasivo.PageIndexChanging
        Dim TmpCredito As credito = CType(Session("Credito"), credito)
        GvHistActMasivo.PageIndex = e.NewPageIndex
        GvHistActMasivo.DataBind()
        GvHistActMasivo.DataSource = Class_Hist_Act.LlenarElementosHistAct(TmpCredito.PR_MC_CREDITO, "Hist_Ge_Dteactividad", "DESC", 1)
        GvHistActMasivo.DataBind()
    End Sub
    Protected Sub GvHistActJudicial_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GvHistActJudicial.PageIndexChanging
        Dim TmpCredito As credito = CType(Session("Credito"), credito)
        GvHistActJudicial.PageIndex = e.NewPageIndex
        GvHistActJudicial.DataBind()
        GvHistActJudicial.DataSource = Class_Hist_Act.LlenarElementosHistAct(TmpCredito.PR_MC_CREDITO, "", "", 1)
        GvHistActJudicial.DataBind()
    End Sub

    Private Sub SendMail(ByRef evento As String, ByVal ex As Exception, ByVal Cuenta As String, ByVal Captura As String, ByVal usr As String)
        EnviarCorreo("Historicos_De_Actividades.aspx", evento, ex, Cuenta, Captura, usr)
    End Sub
End Class
