Imports System.Data
Imports System.Data.OracleClient
Imports System.Globalization
Imports Db
Imports Funciones
Partial Class Hist_Pagos
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
            Dim CREDITO As Credito = CType(Session("Credito"), Credito)
            HidenCredito.Value = CREDITO.PR_MC_CREDITO
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
                        If CType(Session("Credito"), credito).PR_MC_CREDITO <> "" Then
                            Llenar()
                            HidenCredito.Value = CType(Session("Credito"), credito).PR_MC_CREDITO
                        End If
                    End If
                End If
            Else
                Session.Clear()
                Session.Abandon()
                Response.Redirect("ExpiroSesion.aspx")
            End If
        Catch ex As Exception
            SendMail("Page_Load", ex, HidenCredito.Value, "", LblCat_Lo_Usuario.Text)
        End Try
    End Sub
    Sub Llenar()
        Try
            If CType(Session("Credito"), Credito).PR_MC_ESTATUS = "Retirada" Then
                PnlCapturaPagos.Visible = False
            ElseIf CType(Session("Credito"), Credito).PR_MC_CODIGO Like "DI*" And CType(Session("Credito"), Credito).PR_MC_CODIGO <> "DIIS" Then
                PnlCapturaPagos.Visible = False
            Else

                If (CType(Session("Usuario"), USUARIO)).CAT_LO_PGESTION.Substring(1, 1) = 0 Then
                    PnlCapturaPagos.Visible = False
                Else
                    PnlCapturaPagos.Visible = True
                    TxtHist_Pa_Dtepago_CalendarExtender.EndDate = DateTime.Now.ToShortDateString
                End If
            End If
            CARGAR()
        Catch ex As Exception
            SendMail("Llenar", ex, HidenCredito.Value, "", LblCat_Lo_Usuario.Text)
        End Try
    End Sub
    Private Sub CARGAR()
        Dim TmpCredito As Credito = CType(Session("Credito"), Credito)
        If TmpCredito.PR_MC_CREDITO <> "" Then
            GVHist_PagP.DataSource = Class_Hist_Pagos.LlenarElementosHist_Pagos(TmpCredito.PR_MC_CREDITO, 1)
            GVHist_PagP.DataBind()
            GVHist_PagV.DataSource = Class_Hist_Pagos.LlenarElementosHist_Pagos(TmpCredito.PR_MC_CREDITO, 0)
            GVHist_PagV.DataBind()

            Dim DtsPagos As DataSet = Class_Hist_Pagos.LlenarElementosHist_Pagos(TmpCredito.PR_MC_CREDITO, 2)
            TextPag.Text = to_money(DtsPagos.Tables(0).Rows(0).Item("Suma"))

            Dim DtsLugar As DataSet = Class_Hist_Pagos.LlenarElementosHist_Pagos(TmpCredito.PR_MC_CREDITO, 3)
            DdlHist_Pa_Lugarpago.DataTextField = "Descripcion"
            DdlHist_Pa_Lugarpago.DataValueField = "Descripcion"
            DdlHist_Pa_Lugarpago.DataSource = DtsLugar
            DdlHist_Pa_Lugarpago.DataBind()
            DdlHist_Pa_Lugarpago.Items.Add("Seleccione")
            DdlHist_Pa_Lugarpago.SelectedValue = "Seleccione"
            Dim DtsConfirmacion As DataSet = Class_Hist_Pagos.LlenarElementosHist_Pagos(TmpCredito.PR_MC_CREDITO, 4)
            DdlHist_Pa_Confirmacion.DataTextField = "Descripcion"
            DdlHist_Pa_Confirmacion.DataValueField = "Descripcion"
            DdlHist_Pa_Confirmacion.DataSource = DtsConfirmacion
            DdlHist_Pa_Confirmacion.DataBind()
            DdlHist_Pa_Confirmacion.Items.Add("Seleccione")
            DdlHist_Pa_Confirmacion.SelectedValue = "Seleccione"
        End If
    End Sub
    Private Sub Limpiar()
        TxtHist_Pa_Dtepago.Text = ""
        TxtHist_Pa_Montopago.Text = ""
        DdlHist_Pa_Lugarpago.SelectedValue = "Seleccione"
        TxtHist_Pa_Referencia.Text = ""
        DdlHist_Pa_Confirmacion.SelectedValue = "Seleccione"
    End Sub
    Private Sub SendMail(ByRef evento As String, ByVal ex As Exception, ByVal Cuenta As String, ByVal Captura As String, ByVal usr As String)
        EnviarCorreo("Hist_Pagos.ascx", evento, ex, Cuenta, Captura, usr)
    End Sub
    Protected Sub BtnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnGuardar.Click
        Dim TmpCredito As Credito = CType(Session("Credito"), Credito)
        Dim TmpUsuario As USUARIO = CType(Session("USUARIO"), USUARIO)
        LblMsj.Text = Class_Hist_Pagos.AgregarPago(TmpCredito.PR_MC_CREDITO, TmpCredito.PR_MC_PRODUCTO, TmpUsuario.CAT_LO_USUARIO, TxtHist_Pa_Referencia.Text, TxtHist_Pa_Montopago.Text, TxtHist_Pa_Dtepago.Text, DdlHist_Pa_Confirmacion.SelectedValue, DdlHist_Pa_Lugarpago.SelectedValue, TmpCredito.PR_MC_AGENCIA)
        If LblMsj.Text = "1" Then
            LblMsj.Text = "Pago Agregado"
            MpuMensajes.Show()
            CARGAR()
            Limpiar()
        Else
            MpuMensajes.Show()
        End If
    End Sub
End Class
