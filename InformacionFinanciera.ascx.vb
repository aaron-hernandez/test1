Imports System.Data
Imports Microsoft.VisualBasic
Imports System.Data.OracleClient
Imports Db
Imports System.Globalization
Imports System.IO
Imports Funciones

Partial Class InformacionFinanciera
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
                Response.Redirect("ExpiroSesion.aspx")
            End If
        Catch ex As Exception
            SendMail("Page_Load", ex, "", "", LblCat_Lo_Usuario.Text)
        End Try
    End Sub

    Sub Llenar()
        Dim TmpCredito As credito = CType(Session("Credito"), credito)
        If TmpCredito.PR_MC_CREDITO <> "" Then

            TxtPR_AV_DIR_AV1.Text = TmpCredito.PR_AV_DIR_AV1
            TxtPR_AV_DIR_AV2.Text = TmpCredito.PR_AV_DIR_AV2
            TxtPR_AV_DIR_AV3.Text = TmpCredito.PR_AV_DIR_AV3
            TxtPR_AV_DIR_FAM_CER.Text = TmpCredito.PR_AV_DIR_FAM_CER
            TxtPR_AV_EMAIL_FAM_CER.Text = TmpCredito.PR_AV_EMAIL_FAM_CER
            TxtPR_AV_MAIL_AV1.Text = TmpCredito.PR_AV_MAIL_AV1
            TxtPR_AV_MAIL_AV2.Text = TmpCredito.PR_AV_MAIL_AV2
            TxtPR_AV_MAIL_AV3.Text = TmpCredito.PR_AV_MAIL_AV3
            TxtPR_AV_NOMBRE_AV1.Text = TmpCredito.PR_AV_NOMBRE_AV1
            TxtPR_AV_NOMBRE_AV2.Text = TmpCredito.PR_AV_NOMBRE_AV2
            TxtPR_AV_NOMBRE_AV3.Text = TmpCredito.PR_AV_NOMBRE_AV3
            TxtPR_AV_NOMBRE_FAM_CER.Text = TmpCredito.PR_AV_NOMBRE_FAM_CER
            TxtPR_AV_PARENTESCO_AV1.Text = TmpCredito.PR_AV_PARENTESCO_AV1
            TxtPR_AV_PARENTESCO_AV2.Text = TmpCredito.PR_AV_PARENTESCO_AV2
            TxtPR_AV_PARENTESCO_AV3.Text = TmpCredito.PR_AV_PARENTESCO_AV3
            TxtPR_AV_PARENTESCO_FAM_PER.Text = TmpCredito.PR_AV_PARENTESCO_FAM_PER
            TxtPR_AV_TELS_CASA_AV1.Text = TmpCredito.PR_AV_TELS_CASA_AV1
            TxtPR_AV_TELS_CASA_AV2.Text = TmpCredito.PR_AV_TELS_CASA_AV2
            TxtPR_AV_TELS_CASA_AV3.Text = TmpCredito.PR_AV_TELS_CASA_AV3
            TxtPR_AV_TELS_CASA_FAM_CER.Text = TmpCredito.PR_AV_TELS_CASA_FAM_CER
            TxtPR_AV_TELS_CEL_AV1.Text = TmpCredito.PR_AV_TELS_CEL_AV1
            TxtPR_AV_TELS_CEL_AV2.Text = TmpCredito.PR_AV_TELS_CEL_AV2
            TxtPR_AV_TELS_CEL_AV3.Text = TmpCredito.PR_AV_TELS_CEL_AV3
            TxtPR_AV_TELS_CEL_FAM_CER.Text = TmpCredito.PR_AV_TELS_CEL_FAM_CER
            TxtPR_AV_TELS_OFICINA_AV1.Text = TmpCredito.PR_AV_TELS_OFICINA_AV1
            TxtPR_AV_TELS_OFICINA_AV2.Text = TmpCredito.PR_AV_TELS_OFICINA_AV2
            TxtPR_AV_TELS_OFICINA_AV3.Text = TmpCredito.PR_AV_TELS_OFICINA_AV3
            TxtPR_AV_TELS_OFI_FAM_CER.Text = TmpCredito.PR_AV_TELS_OFI_FAM_CER
            'TxtPR_AV_TIPO_AVAL.Text = TmpCredito.PR_AV_TIPO_AVAL
            'TxtPR_AV_TIPO_AV2.Text = TmpCredito.PR_AV_TIPO_AV2
            'TxtPR_AV_TIPO_AV3.Text = TmpCredito.PR_AV_TIPO_AV3
            'TxtPR_AV_TIPO_FAM_CER.Text = TmpCredito.PR_AV_TIPO_FAM_CER

        End If
    End Sub
    Private Sub SendMail(ByRef evento As String, ByVal ex As Exception, ByVal Cuenta As String, ByVal Captura As String, ByVal usr As String)
        EnviarCorreo("InformacionFinanciera.aspx", evento, ex, Cuenta, Captura, usr)
    End Sub


    Protected Sub BtnModificar_Click(sender As Object, e As EventArgs) Handles BtnModificar.Click
        Dim TmpCredito As credito = CType(Session("Credito"), credito)
        If TxtPR_AV_NOMBRE_AV1.Text <> TmpCredito.PR_AV_NOMBRE_AV1 Then
            ModificarDatos(1, "TxtPR_AV_NOMBRE_AV1", TxtPR_AV_NOMBRE_AV1.Text)
        End If

        If TxtPR_AV_TELS_CASA_AV1.Text <> TmpCredito.PR_AV_TELS_CASA_AV1 Then
            ModificarDatos(1, "TxtPR_AV_TELS_CASA_AV1", TxtPR_AV_TELS_CASA_AV1.Text)
        End If

        If TxtPR_AV_TELS_OFICINA_AV1.Text <> TmpCredito.PR_AV_TELS_OFICINA_AV1 Then
            ModificarDatos(1, "TxtPR_AV_TELS_OFICINA_AV1", TxtPR_AV_TELS_OFICINA_AV1.Text)
        End If

        If TxtPR_AV_TELS_CEL_AV1.Text <> TmpCredito.PR_AV_TELS_CEL_AV1 Then
            ModificarDatos(1, "TxtPR_AV_TELS_CEL_AV1", TxtPR_AV_TELS_CEL_AV1.Text)
        End If

        If TxtPR_AV_MAIL_AV1.Text <> TmpCredito.PR_AV_MAIL_AV1 Then
            ModificarDatos(1, "TxtPR_AV_MAIL_AV1", TxtPR_AV_MAIL_AV1.Text)
        End If

        If TxtPR_AV_PARENTESCO_AV1.Text <> TmpCredito.PR_AV_PARENTESCO_AV1 Then
            ModificarDatos(1, "TxtPR_AV_PARENTESCO_AV1", TxtPR_AV_PARENTESCO_AV1.Text)
        End If

        If TxtPR_AV_DIR_AV1.Text <> TmpCredito.PR_AV_DIR_AV1 Then
            ModificarDatos(1, "TxtPR_AV_DIR_AV1", TxtPR_AV_DIR_AV1.Text)
        End If



        If TxtPR_AV_NOMBRE_AV2.Text <> TmpCredito.PR_AV_NOMBRE_AV2 Then
            ModificarDatos(1, "TxtPR_AV_NOMBRE_AV2", TxtPR_AV_NOMBRE_AV2.Text)
        End If

        If TxtPR_AV_TELS_CASA_AV2.Text <> TmpCredito.PR_AV_TELS_CASA_AV2 Then
            ModificarDatos(1, "TxtPR_AV_TELS_CASA_AV2", TxtPR_AV_TELS_CASA_AV2.Text)
        End If

        If TxtPR_AV_TELS_OFICINA_AV2.Text <> TmpCredito.PR_AV_TELS_OFICINA_AV2 Then
            ModificarDatos(1, "TxtPR_AV_TELS_OFICINA_AV2", TxtPR_AV_TELS_OFICINA_AV2.Text)
        End If

        If TxtPR_AV_TELS_CEL_AV2.Text <> TmpCredito.PR_AV_TELS_CEL_AV2 Then
            ModificarDatos(1, "TxtPR_AV_TELS_CEL_AV2", TxtPR_AV_TELS_CEL_AV2.Text)
        End If

        If TxtPR_AV_MAIL_AV2.Text <> TmpCredito.PR_AV_MAIL_AV2 Then
            ModificarDatos(1, "TxtPR_AV_MAIL_AV2", TxtPR_AV_MAIL_AV2.Text)
        End If

        If TxtPR_AV_PARENTESCO_AV2.Text <> TmpCredito.PR_AV_PARENTESCO_AV2 Then
            ModificarDatos(1, "TxtPR_AV_PARENTESCO_AV2", TxtPR_AV_PARENTESCO_AV2.Text)
        End If

        If TxtPR_AV_DIR_AV2.Text <> TmpCredito.PR_AV_DIR_AV2 Then
            ModificarDatos(1, "TxtPR_AV_DIR_AV2", TxtPR_AV_DIR_AV2.Text)
        End If


        If TxtPR_AV_NOMBRE_AV3.Text <> TmpCredito.PR_AV_NOMBRE_AV3 Then
            ModificarDatos(1, "TxtPR_AV_NOMBRE_AV3", TxtPR_AV_NOMBRE_AV3.Text)
        End If

        If TxtPR_AV_TELS_CASA_AV3.Text <> TmpCredito.PR_AV_TELS_CASA_AV3 Then
            ModificarDatos(1, "TxtPR_AV_TELS_CASA_AV3", TxtPR_AV_TELS_CASA_AV3.Text)
        End If

        If TxtPR_AV_TELS_OFICINA_AV3.Text <> TmpCredito.PR_AV_TELS_OFICINA_AV3 Then
            ModificarDatos(1, "TxtPR_AV_TELS_OFICINA_AV3", TxtPR_AV_TELS_OFICINA_AV3.Text)
        End If

        If TxtPR_AV_TELS_CEL_AV3.Text <> TmpCredito.PR_AV_TELS_CEL_AV3 Then
            ModificarDatos(1, "TxtPR_AV_TELS_CEL_AV3", TxtPR_AV_TELS_CEL_AV3.Text)
        End If

        If TxtPR_AV_MAIL_AV3.Text <> TmpCredito.PR_AV_MAIL_AV3 Then
            ModificarDatos(1, "TxtPR_AV_MAIL_AV3", TxtPR_AV_MAIL_AV3.Text)
        End If

        If TxtPR_AV_PARENTESCO_AV3.Text <> TmpCredito.PR_AV_PARENTESCO_AV3 Then
            ModificarDatos(1, "TxtPR_AV_PARENTESCO_AV3", TxtPR_AV_PARENTESCO_AV3.Text)
        End If

        If TxtPR_AV_DIR_AV3.Text <> TmpCredito.PR_AV_DIR_AV3 Then
            ModificarDatos(1, "TxtPR_AV_DIR_AV3", TxtPR_AV_DIR_AV3.Text)
        End If

        If TxtPR_AV_NOMBRE_FAM_CER.Text <> TmpCredito.PR_AV_NOMBRE_FAM_CER Then
            ModificarDatos(1, "TxtPR_AV_NOMBRE_FAM_CER", TxtPR_AV_NOMBRE_FAM_CER.Text)
        End If

        If TxtPR_AV_TELS_CASA_FAM_CER.Text <> TmpCredito.PR_AV_TELS_CASA_FAM_CER Then
            ModificarDatos(1, "TxtPR_AV_TELS_CASA_FAM_CER", TxtPR_AV_TELS_CASA_FAM_CER.Text)
        End If

        If TxtPR_AV_TELS_OFI_FAM_CER.Text <> TmpCredito.PR_AV_TELS_OFI_FAM_CER Then
            ModificarDatos(1, "TxtPR_AV_TELS_OFI_FAM_CER", TxtPR_AV_TELS_OFI_FAM_CER.Text)
        End If

        If TxtPR_AV_TELS_CEL_FAM_CER.Text <> TmpCredito.PR_AV_TELS_CEL_FAM_CER Then
            ModificarDatos(1, "TxtPR_AV_TELS_CEL_FAM_CER", TxtPR_AV_TELS_CEL_FAM_CER.Text)
        End If

        If TxtPR_AV_TELS_CEL_FAM_CER.Text <> TmpCredito.PR_AV_TELS_CEL_FAM_CER Then
            ModificarDatos(1, "TxtPR_AV_TELS_CEL_FAM_CER", TxtPR_AV_TELS_CEL_FAM_CER.Text)
        End If

        If TxtPR_AV_PARENTESCO_FAM_PER.Text <> TmpCredito.PR_AV_PARENTESCO_FAM_PER Then
            ModificarDatos(1, "TxtPR_AV_PARENTESCO_FAM_PER", TxtPR_AV_PARENTESCO_FAM_PER.Text)
        End If

        If TxtPR_AV_DIR_FAM_CER.Text <> TmpCredito.PR_AV_DIR_FAM_CER Then
            ModificarDatos(1, "TxtPR_AV_DIR_FAM_CER", TxtPR_AV_DIR_FAM_CER.Text)
        End If

        LblMsj.Text = "Datos Actualizados."
        MpuMensajes.Show()
    End Sub

    Sub ModificarDatos(ByVal bandera As Integer, ByVal CAMPO As String, ByVal VALOR As String)
        If bandera = 0 Then
            TxtPR_AV_NOMBRE_AV1.ReadOnly = False
            TxtPR_AV_TELS_CASA_AV1.ReadOnly = False
            TxtPR_AV_TELS_OFICINA_AV1.ReadOnly = False
            TxtPR_AV_TELS_CEL_AV1.ReadOnly = False
            TxtPR_AV_MAIL_AV1.ReadOnly = False
            TxtPR_AV_PARENTESCO_AV1.ReadOnly = False
            TxtPR_AV_DIR_AV1.ReadOnly = False
            TxtPR_AV_NOMBRE_AV2.ReadOnly = False
            TxtPR_AV_TELS_CASA_AV2.ReadOnly = False
            TxtPR_AV_TELS_OFICINA_AV2.ReadOnly = False
            TxtPR_AV_TELS_CEL_AV2.ReadOnly = False
            TxtPR_AV_MAIL_AV2.ReadOnly = False
            TxtPR_AV_PARENTESCO_AV2.ReadOnly = False
            TxtPR_AV_DIR_AV2.ReadOnly = False
            TxtPR_AV_NOMBRE_AV3.ReadOnly = False
            TxtPR_AV_TELS_CASA_AV3.ReadOnly = False
            TxtPR_AV_TELS_OFICINA_AV3.ReadOnly = False
            TxtPR_AV_TELS_CEL_AV3.ReadOnly = False
            TxtPR_AV_MAIL_AV3.ReadOnly = False
            TxtPR_AV_PARENTESCO_AV3.ReadOnly = False
            TxtPR_AV_DIR_AV3.ReadOnly = False
            TxtPR_AV_NOMBRE_FAM_CER.ReadOnly = False
            TxtPR_AV_TELS_CASA_FAM_CER.ReadOnly = False
            TxtPR_AV_TELS_OFI_FAM_CER.ReadOnly = False
            TxtPR_AV_TELS_CEL_FAM_CER.ReadOnly = False
            TxtPR_AV_EMAIL_FAM_CER.ReadOnly = False
            TxtPR_AV_PARENTESCO_FAM_PER.ReadOnly = False
            TxtPR_AV_DIR_FAM_CER.ReadOnly = False



        ElseIf bandera = 1
            Dim oraCommand As New OracleCommand
            oraCommand.CommandText = "SP_MODIFICAR_DATOS"
            oraCommand.CommandType = CommandType.StoredProcedure
            oraCommand.Parameters.Add("V_VALOR", OracleType.VarChar).Value = VALOR
            oraCommand.Parameters.Add("V_CAMPO", OracleType.VarChar).Value = CAMPO
            oraCommand.Parameters.Add("V_CREDITO", OracleType.Number).Value = CType(Session("Credito"), credito).PR_MC_CREDITO
            oraCommand.Parameters.Add("cv_1", OracleType.Cursor).Direction = ParameterDirection.Output
            Dim DTS As DataSet = Consulta_Procedure(oraCommand, "MODIFICA")

        End If
    End Sub

End Class
