Imports System.Data
Imports Microsoft.VisualBasic
Imports System.Data.OracleClient
Imports Db
Imports System.Globalization
Imports System.IO
Imports Funciones
Partial Class InformacionGeneral
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

    Sub ModificarDatos(ByVal bandera As Integer, ByVal CAMPO As String, ByVal VALOR As String)
        If bandera = 0 Then
            TxtPR_CA_ESTATUS_AC.ReadOnly = False
            TxtPR_CD_CALLE_NUM.ReadOnly = False
            TxtPR_CD_COLONIA.ReadOnly = False
            TxtPR_CD_DEL_MPIO.ReadOnly = False
            TxtPR_CD_ESTADO.ReadOnly = False
            TxtPR_CD_CP.ReadOnly = False
            TxtPR_CD_EMAIL.ReadOnly = False
            TxtPR_CF_PAGO_MENSUAL.ReadOnly = False
            TxtPR_CA_ENCONTRATO.ReadOnly = False
        ElseIf bandera = 1
            Dim oraCommand As New OracleCommand
            oraCommand.CommandText = "SP_MODIFICAR_DATOS"
            oraCommand.CommandType = CommandType.StoredProcedure
            oraCommand.Parameters.Add("V_VALOR", OracleType.VarChar).Value = VALOR
            oraCommand.Parameters.Add("V_CAMPO", OracleType.VarChar).Value = CAMPO
            oraCommand.Parameters.Add("V_CREDITO", OracleType.VarChar).Value = CType(Session("Credito"), credito).PR_MC_CREDITO
            oraCommand.Parameters.Add("cv_1", OracleType.Cursor).Direction = ParameterDirection.Output
            Dim DTS As DataSet = Consulta_Procedure(oraCommand, "MODIFICA")

        End If



    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If ValidaLicencias(300, LblCat_Lo_Usuario.Text, "", "", 1, "") = 1 Then
                If Val(CType(Session("Usuario"), USUARIO).CAT_LO_MGESTION) = 0 Then
                    Response.Redirect("LogIn.aspx")
                Else
                    If Not IsPostBack Then
                        Llenar()
                        If CType(Session("Usuario"), USUARIO).CAT_LO_PGESTION.ToString.Substring(8, 1) = 1 Then
                            Dim TmpCredito As credito = CType(Session("Credito"), credito)
                            If TmpCredito.PR_MC_CREDITO <> "" Then
                                ModificarDatos(0, 0, "")
                                BtnModificar.Visible = True
                            End If
                        End If
                    End If
                End If
            Else
                Session.Clear()
                Session.Abandon()
                Response.Redirect("ExpiroSesion.aspx", False)
            End If
        Catch ex As System.Threading.ThreadAbortException
        Catch ex As Exception
            SendMail("Page_Load", ex, CType(Session("Credito"), credito).PR_MC_CREDITO, "", LblCat_Lo_Usuario.Text)
        End Try
    End Sub

    Sub Llenar()
        Dim TmpCredito As credito = CType(Session("Credito"), credito)
        If TmpCredito.PR_MC_CREDITO <> "" Then
            'PR_MC_GRAL
            TxtPR_MC_PRODUCTO.Text = TmpCredito.PR_MC_PRODUCTO2

            'PR_CLIE_DEMO

            TxtPR_CD_CALLE_NUM.Text = TmpCredito.PR_CD_CALLE_NUM
            TxtPR_CD_CARTERA.Text = TmpCredito.PR_CD_CARTERA
            TxtPR_CD_COLONIA.Text = TmpCredito.PR_CD_COLONIA
            TxtPR_CD_CP.Text = TmpCredito.PR_CD_CP
            TxtPR_CD_DEL_MPIO.Text = TmpCredito.PR_CD_DEL_MPIO
            TxtPR_CD_EMAIL.Text = TmpCredito.PR_CD_EMAIL
            TxtPR_CD_ESTADO.Text = TmpCredito.PR_CD_ESTADO
            TxtPR_CD_NOMBRE.Text = TmpCredito.PR_CD_NOMBRE
            TxtPR_CD_DTE_NACIMIENTO.Text = TmpCredito.PR_CD_DTE_NACIMIENTO

            'PR_CLIE_AGENCY
            TxtPR_CA_DTE_ULTPP.Text = TmpCredito.PR_CA_DTE_ULTPP
            TxtPR_CA_ENCONTRATO.Text = TmpCredito.PR_CA_ENCONTRATO
            TxtPR_CA_ESQUEMA_SAL.Text = TmpCredito.PR_CA_ESQUEMA_SAL
            TxtPR_CA_ESTATUS_AC.Text = TmpCredito.PR_CA_ESTATUS_AC
            TxtPR_CA_ESTATUS_GE.Text = TmpCredito.PR_CA_ESTATUS_GE
            TxtPR_CA_MATRICULA.Text = TmpCredito.PR_CA_MATRICULA
            TxtPR_CA_MTO_ULTPP.Text = to_money(TmpCredito.PR_CA_MTO_ULTPP)
            TxtPR_CA_UNIVERSIDAD.Text = TmpCredito.PR_CA_UNIVERSIDAD
            TxtPR_CA_US_ASIGNADO.Text = TmpCredito.PR_CA_US_ASIGNADO
            TxtPR_CA_ESTATUS_CTA.Text = TmpCredito.PR_CA_ESTATUS_CTA
            TxtPR_CA_CAMPANA.Text = TmpCredito.PR_CA_CAMPANA
            TxtPR_CA_SIS_ADM.Text = TmpCredito.PR_CA_SIS_ADM
            TxtPR_CA_SUCURSAL.Text = TmpCredito.PR_CA_SUCURSAL

            'PR_CLIE_FINAN
            TxtPR_CF_BANCO.Text = TmpCredito.PR_CF_BANCO
            TxtPR_CF_BUCKET.Text = TmpCredito.PR_CF_BUCKET
            TxtPR_CF_CONVENIO_CTA.Text = TmpCredito.PR_CF_CONVENIO_CTA
            TxtPR_CF_DTE_PROXPAGO.Text = TmpCredito.PR_CF_DTE_PROXPAGO
            TxtPR_CF_DTE_ULTPAGO.Text = TmpCredito.PR_CF_DTE_ULTPAGO
            TxtPR_CF_FONDOS_CON.Text = to_money(TmpCredito.PR_CF_FONDOS_CON)
            TxtPR_CF_LATE_FEE.Text = to_money(TmpCredito.PR_CF_LATE_FEE)
            TxtPR_CF_MTO_ULTPAGO.Text = to_money(TmpCredito.PR_CF_MTO_ULTPAGO)
            TxtPR_CF_PAGO_MENSUAL.Text = to_money(TmpCredito.PR_CF_PAGO_MENSUAL)
            TxtPR_CF_PORCENTAJE.Text = TmpCredito.PR_CF_PORCENTAJE
            TxtPR_CF_REF_BANCO.Text = TmpCredito.PR_CF_REF_BANCO
            TxtPR_CF_SALDO_TOT.Text = to_money(TmpCredito.PR_CF_SALDO_TOT)
            TxtPR_CF_SALDO_VEN.Text = to_money(TmpCredito.PR_CF_SALDO_VEN)
            TxtPR_CF_STFONDOS.Text = to_money(TmpCredito.PR_CF_STFONDOS)
            TxtPR_CF_SVLF.Text = to_money(TmpCredito.PR_CF_SVLF)
            TxtPR_CF_FIRMA_CONTRATO.Text = TmpCredito.PR_CF_FIRMA_CONTRATO

            'PR_AV_AVALES
            TxtPR_AV_RESP_PAGO.Text = TmpCredito.PR_AV_RESP_PAGO
            TxtPR_AV_TELS_CASA_PR.Text = TmpCredito.PR_AV_TELS_CASA_PR
            TxtPR_AV_TELS_CEL_PR.Text = TmpCredito.PR_AV_TELS_CEL_PR
            TxtPR_AV_PARENTESCO_RP.Text = TmpCredito.PR_AV_PARENTESCO_RP
            TxtPR_AV_MAIL_PR.Text = TmpCredito.PR_AV_MAIL_PR

            GvHistActResumido.DataSource = Class_Hist_Act.LlenarElementosHistAct(TmpCredito.PR_MC_CREDITO, "Hist_Ge_Dteactividad", "DESC", 4)
            GvHistActResumido.DataBind()
            GvHistActResumido.Visible = True

            Dim permiso As Integer = 0
            If permiso = 0 Then
                TxtPR_CA_ESTATUS_AC.ReadOnly = True

            Else
                TxtPR_CA_ESTATUS_AC.ReadOnly = False

            End If
        End If
    End Sub
    Private Sub SendMail(ByRef evento As String, ByVal ex As Exception, ByVal Cuenta As String, ByVal Captura As String, ByVal usr As String)
        EnviarCorreo("InformacionGeneral.aspx", evento, ex, Cuenta, Captura, usr)
    End Sub

    Protected Sub BtnModificar_Click(sender As Object, e As EventArgs) Handles BtnModificar.Click
        Dim TmpCredito As credito = CType(Session("Credito"), credito)

        If TxtPR_CA_ESTATUS_AC.Text <> TmpCredito.PR_CA_ESTATUS_AC Then
            ModificarDatos(1, "TxtPR_CA_ESTATUS_AC", TxtPR_CA_ESTATUS_AC.Text)
        End If

        If TxtPR_CD_CALLE_NUM.Text <> TmpCredito.PR_CD_CALLE_NUM Then
            ModificarDatos(1, "TxtPR_CD_CALLE_NUM", TxtPR_CD_CALLE_NUM.Text)
        End If

        If TxtPR_CD_COLONIA.Text <> TmpCredito.PR_CD_COLONIA Then
            ModificarDatos(1, "TxtPR_CD_COLONIA", TxtPR_CD_COLONIA.Text)
        End If

        If TxtPR_CD_DEL_MPIO.Text <> TmpCredito.PR_CD_DEL_MPIO Then
            ModificarDatos(1, "TxtPR_CD_DEL_MPIO", TxtPR_CD_DEL_MPIO.Text)
        End If

        If TxtPR_CD_ESTADO.Text <> TmpCredito.PR_CD_ESTADO Then
            ModificarDatos(1, "TxtPR_CD_ESTADO", TxtPR_CD_ESTADO.Text)
        End If

        If TxtPR_CD_CP.Text <> TmpCredito.PR_CD_CP Then
            ModificarDatos(1, "TxtPR_CD_CP", TxtPR_CD_CP.Text)
        End If

        If TxtPR_CD_EMAIL.Text <> TmpCredito.PR_CD_EMAIL Then
            ModificarDatos(1, "TxtPR_CD_EMAIL", TxtPR_CD_EMAIL.Text)
        End If

        If TxtPR_CF_PAGO_MENSUAL.Text <> to_money(TmpCredito.PR_CF_PAGO_MENSUAL) Then
            ModificarDatos(1, "TxtPR_CF_PAGO_MENSUAL", TxtPR_CF_PAGO_MENSUAL.Text)
        End If

        If TxtPR_CA_ENCONTRATO.Text <> TmpCredito.PR_CA_ENCONTRATO Then
            ModificarDatos(1, "TxtPR_CA_ENCONTRATO", TxtPR_CA_ENCONTRATO.Text)
        End If

        If TxtPR_AV_RESP_PAGO.Text <> TmpCredito.PR_AV_RESP_PAGO Then 'aqui comienzo
            ModificarDatos(1, "TxtPR_AV_RESP_PAGO", TxtPR_AV_RESP_PAGO.Text)
        End If

        If TxtPR_AV_TELS_CASA_PR.Text <> TmpCredito.PR_AV_TELS_CASA_PR Then 'aqui comienzo
            ModificarDatos(1, "TxtPR_AV_TELS_CASA_PR", TxtPR_AV_TELS_CASA_PR.Text)
        End If

        If TxtPR_AV_TELS_CEL_PR.Text <> TmpCredito.PR_AV_TELS_CEL_PR Then 'aqui comienzo
            ModificarDatos(1, "TxtPR_AV_TELS_CEL_PR", TxtPR_AV_TELS_CEL_PR.Text)
        End If

        If TxtPR_AV_PARENTESCO_RP.Text <> TmpCredito.PR_AV_PARENTESCO_RP Then 'aqui comienzo
            ModificarDatos(1, "TxtPR_AV_PARENTESCO_RP", TxtPR_AV_PARENTESCO_RP.Text)
        End If

        If TxtPR_AV_MAIL_PR.Text <> TmpCredito.PR_AV_MAIL_PR Then 'aqui comienzo
            ModificarDatos(1, "TxtPR_AV_MAIL_PR", TxtPR_AV_MAIL_PR.Text)
        End If




        LblMsj.Text = "Datos Actualizados"
        MpuMensajes.Show()


    End Sub
End Class
