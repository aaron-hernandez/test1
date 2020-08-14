Imports System.Data
Imports Microsoft.VisualBasic
Imports System.Data.OracleClient
Imports Db
Imports System.Globalization
Imports System.IO
Imports Funciones
Partial Class Judicial
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
                        Dim TMpCredito As credito = CType(Session("credito"), credito)
                        If TMpCredito.PR_MC_CREDITO <> "" Then
                            ' If TMpCredito.PR_JU_LEGAL = 1 Then
                            Llenar()
                            'End If
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
        Dim TmpCredito As credito = (CType(Session("credito"), credito))

        If TmpCredito.VI_SEMAFORO_JUDICIAL = "ROJO" Then
            ImgSemaforoJ.ImageUrl = "Imagenes/ImgSemaforoRojo.png"
        ElseIf TmpCredito.VI_SEMAFORO_JUDICIAL = "AMARILLO" Then
            ImgSemaforoJ.ImageUrl = "Imagenes/ImgSemaforoAmarillo.png"
        ElseIf TmpCredito.VI_SEMAFORO_JUDICIAL = "VERDE" Then
            ImgSemaforoJ.ImageUrl = "Imagenes/ImgSemaforoVerde.png"
        Else
            ImgSemaforoJ.ImageUrl = "Imagenes/ImgSemaforoRojo.png"
        End If

        TxtPR_JU_ABOGADO.Text = TmpCredito.PR_JU_ABOGADO
        TxtPR_JU_ETAPA.Text = TmpCredito.PR_JU_ETAPA
        TxtPR_JU_FECHAETAPA.Text = TmpCredito.PR_JU_FECHAETAPA
        TxtPR_JU_TIPOJUICIO.Text = TmpCredito.PR_JU_TIPOJUICIO
        TxtPR_JU_FECHAABOGADO.Text = TmpCredito.PR_JU_FECHAABOGADO
        TxtPR_JU_FECHAENJUDICIAL.Text = TmpCredito.PR_JU_FECHAENJUDICIAL
        TxtPR_JU_JUZGADO.Text = TmpCredito.PR_JU_JUZGADO
        TxtPR_JU_LOCALIDAD.Text = TmpCredito.PR_JU_LOCALIDAD
        TxtPR_JU_EXPEDIENTE.Text = TmpCredito.PR_JU_EXPEDIENTE
        DdlPR_JU_ABOGADO.SelectedValue = TmpCredito.PR_JU_ABOGADO


        Try
            Dim objDSa As DataSet = Class_EtapasJudiciales.DatosJuicio("", "", "", "", "", 0)
            DdlPR_JU_ABOGADO.DataTextField = "NOMBRE"
            DdlPR_JU_ABOGADO.DataValueField = "LOGIN"
            DdlPR_JU_ABOGADO.DataSource = objDSa.Tables(0)
            DdlPR_JU_ABOGADO.DataBind()
            DdlPR_JU_ABOGADO.Items.Add("SELECCIONE")
            DdlPR_JU_ABOGADO.SelectedValue = "SELECCIONE"
        Catch ex As Exception
            DdlPR_JU_ABOGADO.Items.Add("SELECCIONE")
            DdlPR_JU_ABOGADO.SelectedValue = "SELECCIONE"
        End Try



        If TmpCredito.PR_JU_TIPOJUICIO = " " Then
            LblHIST_JU_ESTADO.Visible = True
            DdlHIST_JU_ESTADO.Visible = True
            LblHIST_JU_TIPOJUICIO.Visible = False
            DdlHIST_JU_TIPOJUICIO.Visible = False
            LblHIST_JU_ETAPA.Visible = False
            DdlHIST_JU_ETAPA.Visible = False


            Dim DtsTipo As DataSet = Class_EtapasJudiciales.LlenarElementos("", "", "", "", "", "", "", "", "", "", 0)
            DdlHIST_JU_ESTADO.DataTextField = "CAT_FJ_ESTADO"
            DdlHIST_JU_ESTADO.DataValueField = "CAT_FJ_ESTADO"
            DdlHIST_JU_ESTADO.DataSource = DtsTipo.Tables(0)
            DdlHIST_JU_ESTADO.DataBind()
            DdlHIST_JU_ESTADO.Items.Add("SELECCIONE")
            DdlHIST_JU_ESTADO.SelectedValue = "SELECCIONE"
            ChkModificar.Visible = False
        Else
            ChkModificar.Visible = True
            LblHIST_JU_ESTADO.Visible = False
            DdlHIST_JU_ESTADO.Visible = False
            LblHIST_JU_TIPOJUICIO.Visible = False
            DdlHIST_JU_TIPOJUICIO.Visible = False
            LblHIST_JU_ETAPA.Visible = True
            DdlHIST_JU_ETAPA.Visible = True
            Dim objDSas As DataSet = Class_EtapasJudiciales.LlenarElementos("", "", "", "", TmpCredito.PR_JU_IDETAPA, "", "", TmpCredito.PR_JU_TIPOJUICIO, TmpCredito.PR_JU_ESTADOJUICIO, "", 3)
            DdlHIST_JU_ETAPA.DataTextField = "DESCRIPCION"
            DdlHIST_JU_ETAPA.DataValueField = "CADENA"
            DdlHIST_JU_ETAPA.DataSource = objDSas.Tables(0)
            DdlHIST_JU_ETAPA.DataBind()
            DdlHIST_JU_ETAPA.Items.Add(New ListItem(TmpCredito.PR_JU_ETAPA, TmpCredito.PR_JU_IDETAPA))
            DdlHIST_JU_ETAPA.Items.Add("SELECCIONE")
            DdlHIST_JU_ETAPA.SelectedValue = "SELECCIONE"
            For Each myItem As ListItem In DdlHIST_JU_ETAPA.Items
                If myItem.Value = TmpCredito.PR_JU_IDETAPA Then
                    myItem.Attributes.Add("style", "background-color:orange")
                Else
                    myItem.Attributes.Add("style", "background-color:white")
                End If
            Next
        End If
    End Sub
    Private Sub SendMail(ByRef evento As String, ByVal ex As Exception, ByVal Cuenta As String, ByVal Captura As String, ByVal usr As String)
        EnviarCorreo("Judicial.ascx", evento, ex, Cuenta, Captura, usr)
    End Sub

    Protected Sub BtnGuarJud_Click(sender As Object, e As EventArgs) Handles BtnGuarJud.Click
        Dim Tmpcredito As credito = CType(Session("credito"), credito)
        If ChkModificar.Checked = True Then
            If TxtHIST_JU_COMENTARIO.Text = "" Then
                LblMsj.Text = "Agrege Un Comentario Por El Cual Se Solicita El Cambio"
                MpuMensajes.Show()
            ElseIf DrlCambioTipoJuicio.SelectedValue = "SELECCIONE" Then
                LblMsj.Text = "Seleccione El cambio Que Se Desea Realizar"
                MpuMensajes.Show()
            Else
                Dim oraCommand1 As New OracleCommand
                oraCommand1.CommandText = "SP_MODIFICAR_JUICIOS"
                oraCommand1.CommandType = CommandType.StoredProcedure
                oraCommand1.Parameters.Add("V_HCCTA", OracleType.VarChar).Value = Tmpcredito.PR_MC_CREDITO
                oraCommand1.Parameters.Add("V_HCUSUARIO", OracleType.VarChar).Value = (CType(Session("Usuario"), USUARIO)).CAT_LO_USUARIO
                oraCommand1.Parameters.Add("V_HCDTE", OracleType.VarChar).Value = ""
                oraCommand1.Parameters.Add("V_HCMOTIVO", OracleType.VarChar).Value = TxtHIST_JU_COMENTARIO.Text
                oraCommand1.Parameters.Add("V_HCJUICIO", OracleType.VarChar).Value = DrlCambioTipoJuicio.SelectedValue
                oraCommand1.Parameters.Add("V_HCUSUARIOAUTORIZA", OracleType.VarChar).Value = ""
                oraCommand1.Parameters.Add("V_BANDERA", OracleType.Number).Value = 1
                oraCommand1.Parameters.Add("cv_1", OracleType.Cursor).Direction = ParameterDirection.Output
                Dim dts As DataSet = Consulta_Procedure(oraCommand1, "SP_MODIFCAR_JUICIOS")
                Response.Redirect("MasterPage.aspx")
            End If
        Else
            If DdlHIST_JU_ETAPA.SelectedValue = "SELECCIONE" Then
                LblMsj.Text = "Seleccione Una Etapa Judicial"
                MpuMensajes.Show()
            ElseIf TxtHIST_JU_COMENTARIO.Text = "" Then
                LblMsj.Text = "Agrege Un Comentario"
                MpuMensajes.Show()
                'ElseIf TxtHIST_JU_DTEETAPA.Text = "" Then
                '    LblMsj.Text = "Seleccione Fecha De " & DdlHIST_JU_ETAPA.SelectedValue
                '    MpuMensajes.Show()
            Else
                Dim DtsTipo As DataSet = Class_EtapasJudiciales.LlenarElementos(Tmpcredito.PR_MC_CREDITO, Tmpcredito.PR_MC_PRODUCTO, (CType(Session("Usuario"), USUARIO)).CAT_LO_USUARIO, DdlHIST_JU_ETAPA.SelectedItem.Text, DdlHIST_JU_ETAPA.SelectedValue, TxtHIST_JU_COMENTARIO.Text, CType(Session("Usuario"), USUARIO).CAT_LO_AGENCIA, Tmpcredito.PR_JU_TIPOJUICIO, Tmpcredito.PR_JU_ESTADOJUICIO, TxtHIST_JU_DTEETAPA.Text, 10)

                Tmpcredito.VI_SEMAFORO_JUDICIAL = "VERDE"
                Tmpcredito.PR_JU_ETAPA = DdlHIST_JU_ETAPA.SelectedItem.Text
                Tmpcredito.PR_JU_IDETAPA = DdlHIST_JU_ETAPA.SelectedValue
                Tmpcredito.PR_JU_FECHAETAPA = Now.ToShortDateString.ToString
                Tmpcredito.PR_JU_ABOGADO = (CType(Session("Usuario"), USUARIO)).CAT_LO_USUARIO
                LIMPIAR2()
                Response.Redirect("MasterPage.aspx")
            End If
        End If
    End Sub
    Sub LIMPIAR2()
        TxtHIST_JU_COMENTARIO.Text = ""
        DdlHIST_JU_ETAPA.SelectedValue = "SELECCIONE"
    End Sub

    Protected Sub DdlHIST_JU_ESTADO_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlHIST_JU_ESTADO.SelectedIndexChanged
      
        If DdlHIST_JU_ESTADO.SelectedValue <> "SELECCIONE" Then
            LblHIST_JU_ETAPA.Visible = False
            DdlHIST_JU_ETAPA.Visible = False
            DdlHIST_JU_ETAPA.Items.Clear()
            DdlHIST_JU_ETAPA.DataBind()
            LblHIST_JU_TIPOJUICIO.Visible = True
            DdlHIST_JU_TIPOJUICIO.Visible = True
            Dim DtsTipo As DataSet = Class_EtapasJudiciales.LlenarElementos("", "", "", "", "", "", "", "", DdlHIST_JU_ESTADO.SelectedValue, "", 1)
            DdlHIST_JU_TIPOJUICIO.DataTextField = "CAT_FJ_TIPOJUICIO"
            DdlHIST_JU_TIPOJUICIO.DataValueField = "CAT_FJ_TIPOJUICIO"
            DdlHIST_JU_TIPOJUICIO.DataSource = DtsTipo.Tables(0)
            DdlHIST_JU_TIPOJUICIO.DataBind()
            DdlHIST_JU_TIPOJUICIO.Items.Add("SELECCIONE")
            DdlHIST_JU_TIPOJUICIO.SelectedValue = "SELECCIONE"
            TxtHIST_JU_DTEETAPA.Visible = False
            LblHIST_JU_DTEETAPA.Visible = False
        Else
            LblHIST_JU_ETAPA.Visible = False
            DdlHIST_JU_ETAPA.Visible = False
            LblHIST_JU_TIPOJUICIO.Visible = False
            DdlHIST_JU_TIPOJUICIO.Visible = False
            DdlHIST_JU_TIPOJUICIO.Items.Clear()
            DdlHIST_JU_TIPOJUICIO.DataBind()
            TxtHIST_JU_DTEETAPA.Visible = False
            LblHIST_JU_DTEETAPA.Visible = False
        End If
    End Sub

    Protected Sub DdlHIST_JU_TIPOJUICIO_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlHIST_JU_TIPOJUICIO.SelectedIndexChanged
        Dim TmpCredito As credito = CType(Session("credito"), credito)
        If DdlHIST_JU_TIPOJUICIO.SelectedValue <> "SELECCIONE" Then
            LblHIST_JU_ETAPA.Visible = True
            DdlHIST_JU_ETAPA.Visible = True

            Dim DtsTipo As DataSet = Class_EtapasJudiciales.LlenarElementos("", "", "", "", "", "", "", DdlHIST_JU_TIPOJUICIO.SelectedValue, DdlHIST_JU_ESTADO.SelectedValue, "", 2)

            DdlHIST_JU_ETAPA.DataTextField = "CAT_FJ_ETAPA"
            DdlHIST_JU_ETAPA.DataValueField = "CAT_FJ_INDICE"
            DdlHIST_JU_ETAPA.DataSource = DtsTipo.Tables(0)
            DdlHIST_JU_ETAPA.DataBind()
            DdlHIST_JU_ETAPA.Items.Add("SELECCIONE")
            DdlHIST_JU_ETAPA.SelectedValue = "SELECCIONE"

            TmpCredito.PR_JU_ESTADOJUICIO = DdlHIST_JU_ESTADO.SelectedValue
            TmpCredito.PR_JU_TIPOJUICIO = DdlHIST_JU_TIPOJUICIO.SelectedValue
        Else
            LblHIST_JU_ETAPA.Visible = False
            DdlHIST_JU_ETAPA.Visible = False
            DdlHIST_JU_ETAPA.Items.Clear()
            DdlHIST_JU_ETAPA.DataBind()
        End If
    End Sub
    Protected Sub BtnGuarJuicio_Click(sender As Object, e As EventArgs) Handles BtnGuarJuicio.Click
        Try
            Dim TmpCredito As credito = CType(Session("credito"), credito)
            Dim Abogado As String
            If DdlPR_JU_ABOGADO.SelectedValue = "SELECCIONE" Then
                Abogado = ""
            Else
                Abogado = DdlPR_JU_ABOGADO.SelectedValue
            End If
            Dim objDSa As DataSet = Class_EtapasJudiciales.DatosJuicio(TxtPR_JU_JUZGADO.Text, TxtPR_JU_LOCALIDAD.Text, TxtPR_JU_EXPEDIENTE.Text, DdlPR_JU_ABOGADO.SelectedValue, CType(Session("credito"), credito).PR_MC_CREDITO, 1)
            TmpCredito.PR_JU_JUZGADO = TxtPR_JU_JUZGADO.Text
            TmpCredito.PR_JU_LOCALIDAD = TxtPR_JU_LOCALIDAD.Text
            TmpCredito.PR_JU_EXPEDIENTE = TxtPR_JU_EXPEDIENTE.Text
            TmpCredito.PR_JU_ABOGADO = DdlPR_JU_ABOGADO.SelectedValue
            LblMsj.Text = "Los Datos De Juicio Se Han Guardado"
            MpuMensajes.Show()
        Catch ex As Exception
            SendMail("BtnGuarJuicio_Click", ex, "", "", LblCat_Lo_Usuario.Text)
        End Try
    End Sub
End Class
