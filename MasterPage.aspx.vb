Imports System.Data.OracleClient
Imports System.Data
Imports Db
Imports Funciones
Imports System.Web.Services
Imports System.Globalization
Imports Busquedas
Imports Class_InformacionAdicional
Imports System.Web.Script.Serialization
Imports QParametros
Imports QRegreso
Imports Quiubas
Imports System.Web.UI.WebControls
Imports System.Drawing

Partial Class MGestion_MasterPage
    Inherits System.Web.UI.Page
    <WebMethod(EnableSession:=True)>
    Public Shared Function KeepActiveSession(ByVal Usuario As String) As String
        Dim DtsConectado As DataSet = Class_Sesion.LlenarElementos(Usuario, "", "", "", 3, "", "", "", "")
        If DtsConectado.Tables(0).Rows(0).Item("Cuantas") <> "0" Then
            Return "Hola"
        Else
            Return "Bye"
        End If
    End Function
    Public ReadOnly Property UsuarioActual() As String
        Get
            Return HiddenUrs.Value.ToString
        End Get
    End Property
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Session("Mensajes") <> "SinMensaje" Then
                TCM.AccessKey = 0
                LblMsj.Text = Session("Mensajes")
                MpuMensajes.Show()
                Session("Mensajes") = "SinMensaje"
            End If
            If ValidaLicencias(300, LblCat_Lo_Usuario.Text, "", "", 1, "") = 1 Then 'valida si el ususario aun se encuentra logeado

                If Val(CType(Session("Usuario"), USUARIO).CAT_LO_MGESTION) = 0 Then
                    Response.Redirect("LogIn.aspx", False)
                Else
                    HiddenUrs.Value = (CType(Session("Usuario"), USUARIO)).CAT_LO_USUARIO
                    Dim TmpUsr As USUARIO = CType(Session("USUARIO"), USUARIO)
                    Dim TmpCrdt As credito = CType(Session("Credito"), credito)
                    LblGESTIONES.Text = TmpUsr.GESTIONES
                    LblCUANTASPP.Text = TmpUsr.CUANTASPP
                    If TmpUsr.MONTOPP.Substring(0, 1) = "$" Then
                        LblMONTOPP.Text = TmpUsr.MONTOPP
                    Else
                        LblMONTOPP.Text = to_money(TmpUsr.MONTOPP)
                    End If
                    If Not IsPostBack Then
                        Dim DtsAgenda As DataSet = Class_MasterPage.Agenda(LblCat_Lo_Usuario.Text, 5)
                        LblAgenda.Text = (DtsAgenda.Tables(0).Rows(0).Item("Valor"))
                        Llenar()
                        Try
                            Session("Saldosms") = QCampo(ExecutarCurl("https://rest.quiubas.com/1.0/balance", "", "", "GET", 0), 2)
                            If Session("Saldosms") Is Nothing Then
                                Session("Saldosms") = 0
                            End If
                        Catch ex As Exception
                            Session("Saldosms") = 0
                        End Try
                    End If
                End If
            Else
                Session.Clear()
                Session.Abandon()
                Response.Redirect("ExpiroSesion.aspx", False)
            End If
        Catch ex As System.Threading.ThreadAbortException
        Catch ex As Exception
            SendMail("Page_Load", ex, "", "", LblCat_Lo_Usuario.Text)
        End Try
    End Sub

    Sub CreditoRetirado(ByVal V_Estatus As String, ByVal Codigo As String)
        If (V_Estatus = "Retirada") Or (V_Estatus = "Liquidada") Then
            GvCalTelefonos.Enabled = False
            UpnlGestion.Visible = False
            UpnlRetirada.Visible = True
            LblRetiro.Text = "Crédito Retirado"
            ImgAddVisita.Visible = False
        ElseIf Codigo Like "DI*" And Codigo <> "DIIS" Then
            GvCalTelefonos.Enabled = False
            UpnlGestion.Visible = False
            UpnlRetirada.Visible = True
            LblRetiro.Text = "Crédito Dictaminado"
        Else
            ValidaConfiguracion()
        End If
    End Sub
    Sub ValidaConfiguracion()
        LblHist_Ge_NoPago.Visible = False
        DdlHist_Ge_NoPago.Visible = False
        Dim Aplicacion As Aplicacion = CType(Session("Aplicacion"), Aplicacion)
        Dim TmpUsr As USUARIO = CType(Session("USUARIO"), USUARIO)
        Dim TmpCrdt As credito = CType(Session("Credito"), credito)
        If Aplicacion.ACCION = 1 Then
            DdlHist_Ge_Accion.Visible = True
            LblHist_Ge_Accion.Visible = True
            Class_MasterPage.LlenarElementosMaster(DdlHist_Ge_Accion, "", TmpCrdt.PR_MC_PRODUCTO, TmpUsr.CAT_LO_PERFIL, "1,3", 1)
            DdlHist_Ge_Resultado.Visible = False
            LblHist_Ge_Resultado.Visible = False
        Else
            Class_MasterPage.LlenarElementosMaster(DdlHist_Ge_Resultado, "", TmpCrdt.PR_MC_PRODUCTO, TmpUsr.CAT_LO_PERFIL, "1,3", 2)
        End If
    End Sub
    Protected Sub DdlAccion_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlHist_Ge_Accion.SelectedIndexChanged
        DdlHist_Ge_NoPago.Visible = False
        LblHist_Ge_NoPago.Visible = False
        BtnGuardar.Visible = False
        If DdlHist_Ge_Accion.SelectedValue <> "Seleccione" Then
            Dim TmpCrdt As credito = CType(Session("Credito"), credito)
            Dim TmpUsr As USUARIO = CType(Session("USUARIO"), USUARIO)

            Class_MasterPage.LlenarElementosMaster(DdlHist_Ge_Resultado, DdlHist_Ge_Accion.SelectedValue, TmpCrdt.PR_MC_PRODUCTO, TmpUsr.CAT_LO_PERFIL, "1,3", 3)
            DdlHist_Ge_Resultado.Visible = True
            LblHist_Ge_Resultado.Visible = True
        Else
            DdlHist_Ge_Resultado.Visible = False
            LblHist_Ge_Resultado.Visible = False
        End If
    End Sub
    Protected Sub DdlResultado_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlHist_Ge_Resultado.SelectedIndexChanged
        LblHist_Pr_Montopp.Visible = False
        TxtHist_Pr_Montopp.Visible = False
        LblHist_Pr_Dtepromesa.Text = "Proximo Contacto"
        Dim Aplicacion As Aplicacion = CType(Session("Aplicacion"), Aplicacion)
        Dim TmpCrdt As credito = CType(Session("Credito"), credito)
        If DdlHist_Ge_Resultado.SelectedValue <> "Seleccione" Then
            If Aplicacion.NOPAGO = 1 Then
                Dim DtsNopago As DataSet = Class_MasterPage.LlenarElementosMaster(DdlHist_Ge_NoPago, DdlHist_Ge_Accion.SelectedValue & "," & DdlHist_Ge_Resultado.SelectedValue.Split(",")(0), TmpCrdt.PR_MC_PRODUCTO, "", "1,3", 4)
                DdlHist_Ge_NoPago.Items.Clear()
                If Not IsDBNull(DtsNopago.Tables("ELEMENTOS").Rows(0).Item("Descripcion")) Then
                    Dim ArrayGeneral() As String = Strings.Split(DtsNopago.Tables("ELEMENTOS").Rows(0).Item("Descripcion"), ",")
                    For i As Integer = 0 To ArrayGeneral.Count() - 1
                        DdlHist_Ge_NoPago.Items.Add(New ListItem(ArrayGeneral(i).ToString().Split("|")(0), ArrayGeneral(i).ToString().Split("|")(1), True))
                    Next
                    DdlHist_Ge_NoPago.DataBind()
                    DdlHist_Ge_NoPago.Visible = True
                    LblHist_Ge_NoPago.Visible = True
                Else
                    DdlHist_Ge_NoPago.Visible = False
                    LblHist_Ge_NoPago.Visible = False
                End If
            End If
            BtnGuardar.Visible = True
            If DdlHist_Ge_Resultado.SelectedValue.Split(",")(1) = 1 Then
                LblHist_Pr_Montopp.Visible = True
                TxtHist_Pr_Montopp.Visible = True
                LblHist_Pr_Dtepromesa.Text = "Fecha Promesa"
                DdlHora.Visible = False
                LblHora.Visible = False
            Else
                LblHist_Pr_Montopp.Visible = False
                TxtHist_Pr_Montopp.Visible = False
                LblHist_Pr_Dtepromesa.Text = "Proximo Contacto"
                DdlHora.Visible = True
                LblHora.Visible = True
                TxtHist_Pr_Dtepromesa_CalendarExtender.EndDate = Now.AddDays(30)
            End If
        Else
            If Aplicacion.NOPAGO = 1 Then
                DdlHist_Ge_NoPago.Visible = False
                LblHist_Ge_NoPago.Visible = False
            End If
            BtnGuardar.Visible = False
        End If
    End Sub
    Protected Sub BtnGuardar_Click(sender As Object, e As EventArgs) Handles BtnGuardar.Click
        Dim TmpUsr As USUARIO = CType(Session("USUARIO"), USUARIO)
        Dim TmpCrdt As credito = CType(Session("Credito"), credito)
        Dim TmpAplcc As Aplicacion = CType(Session("Aplicacion"), Aplicacion)
        If DdlHist_Ge_Resultado.SelectedValue = "Seleccione" Then
            LblMsj.Text = "Seleccione Un Resultado"
            MpuMensajes.Show()
        Else 'CUndo resultado es promesa de pago
            If DdlHist_Ge_Resultado.SelectedValue.Split(",")(1) = 1 Then
                If ValidaMonto(TxtHist_Pr_Montopp.Text) = 1 Then
                    LblMsj.Text = "Monto Incorrecto, Valide " & TxtHist_Pr_Montopp.Text
                    MpuMensajes.Show()
                ElseIf Val(TxtHist_Pr_Montopp.Text) <= 0 Then
                    LblMsj.Text = "El Monto No Puede Se Menor O Igual A 0"
                    MpuMensajes.Show()
                ElseIf TxtHist_Pr_Dtepromesa.Text = "" Then
                    LblMsj.Text = "Captura La Fecha De La Promesa"
                    MpuMensajes.Show()
                Else
                    Dim Promesa As String = Class_CapturaVisitas.VariosQ(TmpCrdt.PR_MC_CREDITO, TmpUsr.CAT_LO_USUARIO, 7)
                    If Promesa = "0,0" Then
                        Dim Resultado As Object = Class_MasterPage.GuardarGestion(TmpCrdt.PR_MC_CREDITO, TmpCrdt.PR_MC_PRODUCTO, TmpUsr.CAT_LO_USUARIO, DdlHist_Ge_Accion.SelectedValue, DdlHist_Ge_Resultado.SelectedItem.ToString, DdlHist_Ge_Resultado.SelectedValue, DdlHist_Ge_NoPago.SelectedValue, TxtHist_Ge_Comentario.Text, Boleano(CbxHist_Ge_Inoutbound.Checked), TxtHist_Ge_Telefono.Text, TmpCrdt.PR_MC_AGENCIA, TxtHist_Pr_Dtepromesa.Text & " " & DdlHora.SelectedValue & ":00:00", TxtHist_Pr_Montopp.Text, TmpAplcc.ACCION, TmpAplcc.NOPAGO, TmpCrdt.PR_MC_CODIGO, 1, TxtHist_Pr_Motivo.Text, TxtHist_Pr_Supervisor.Text, TmpCrdt.PR_CA_CAMPANA, TmpCrdt.PR_CF_BUCKET, lblNomFila.Text)

                        If TypeOf Resultado Is DataSet Then
                            Dim Valores As DataSet = Resultado
                            TxtPr_Mc_Dtegestion.Text = Valores.Tables(0).Rows(0).Item("Pr_Mc_Dtegestion")
                            TxtPr_Mc_Dteresultadorelev.Text = Valores.Tables(0).Rows(0).Item("Pr_Mc_Dteresultadorelev")

                            TxtPr_Mc_Resultadorelev.Text = Valores.Tables(0).Rows(0).Item("Pr_Mc_Resultadorelev")
                            TxtPr_Mc_Resultado.Text = Valores.Tables(0).Rows(0).Item("Pr_Mc_Resultado")
                            TxtPr_Mc_Usuario.Text = TmpUsr.CAT_LO_USUARIO
                            TmpCrdt.PR_MC_CODIGO = Valores.Tables(0).Rows(0).Item("Pr_Mc_Codigo")
                            If Valores.Tables(0).Rows(0).Item("pr_mc_creditocontactado") = "1" Then
                                ImgNoContacto.ImageUrl = "~/Imagenes/ImgContacto.png"
                                TxtPr_Mc_Dtecreditocontactado.Text = Valores.Tables(0).Rows(0).Item("pr_mc_dtecreditocontactado")
                            End If
                            TxtPr_Mc_Primeragestion.Text = Valores.Tables(0).Rows(0).Item("Pr_Mc_Primeragestion")
                            TxtPr_Mc_Dteprimeragestion.Text = Valores.Tables(0).Rows(0).Item("Pr_Mc_Dteprimeragestion")
                            ImgSemaforo.ImageUrl = "~/Imagenes/ImgSemaforoVerde.png"
                            LblVi_Dias_Semaforo_Gestion.Text = Valores.Tables(0).Rows(0).Item("Vi_Dias_Semaforo_Gestion")
                            TmpCrdt.PR_MC_CUENTATRABAJADAFILA = 1

                            TmpUsr.GESTIONES = Val(TmpUsr.GESTIONES) + 1
                            LblGESTIONES.Text = Val(TmpUsr.GESTIONES)
                            Dim WCANPP As Double = TmpUsr.MONTOPP
                            If DdlHist_Ge_Resultado.SelectedValue.Split(",")(1) = "1" Then
                                TmpUsr.CUANTASPP = Val(TmpUsr.CUANTASPP) + 1
                                LblCUANTASPP.Text = Val(TmpUsr.CUANTASPP)
                                'TmpUsr.MONTOPP = to_money(Val(WCANPP) + Val(TxtHist_Pr_Montopp.Text))
                                TmpUsr.MONTOPP = to_money(Val(WCANPP) + Val(TxtHist_Pr_Montopp.Text))
                                LblMONTOPP.Text = to_money(Val(TmpUsr.MONTOPP.Replace("$", "").Replace(",", "")))
                            End If
                            Limpiar()
                        Else
                            LblMsj.Text = Resultado.ToString
                            MpuMensajes.Show()
                        End If
                    Else
                        LblPromesa.Text = "Promesa Vigente Para " & Promesa.Split(",")(1) & " Por " & to_money(Promesa.Split(",")(0))
                        MpuPromesa.Show()
                    End If
                End If

                'ElseIf

            Else

                Dim Resultado As Object = Class_MasterPage.GuardarGestion(TmpCrdt.PR_MC_CREDITO, TmpCrdt.PR_MC_PRODUCTO, TmpUsr.CAT_LO_USUARIO, DdlHist_Ge_Accion.SelectedValue, DdlHist_Ge_Resultado.SelectedItem.ToString, DdlHist_Ge_Resultado.SelectedValue, DdlHist_Ge_NoPago.SelectedValue, TxtHist_Ge_Comentario.Text, Boleano(CbxHist_Ge_Inoutbound.Checked), TxtHist_Ge_Telefono.Text, TmpCrdt.PR_MC_AGENCIA, TxtHist_Pr_Dtepromesa.Text & " " & DdlHora.SelectedValue & ":00:00", TxtHist_Pr_Montopp.Text, TmpAplcc.ACCION, TmpAplcc.NOPAGO, TmpCrdt.PR_MC_CODIGO, 2, TxtHist_Pr_Motivo.Text, TxtHist_Pr_Supervisor.Text, TmpCrdt.PR_CA_CAMPANA, TmpCrdt.PR_CF_BUCKET, lblNomFila.Text)

                If TypeOf Resultado Is DataSet Then
                    Limpiar()
                    Dim Valores As DataSet = Resultado
                    TxtPr_Mc_Dtegestion.Text = Valores.Tables(0).Rows(0).Item("Pr_Mc_Dtegestion")
                    TxtPr_Mc_Dteresultadorelev.Text = Valores.Tables(0).Rows(0).Item("Pr_Mc_Dteresultadorelev")

                    TxtPr_Mc_Resultadorelev.Text = Valores.Tables(0).Rows(0).Item("Pr_Mc_Resultadorelev")
                    TxtPr_Mc_Resultado.Text = Valores.Tables(0).Rows(0).Item("Pr_Mc_Resultado")
                    TxtPr_Mc_Usuario.Text = TmpUsr.CAT_LO_USUARIO
                    TmpCrdt.PR_MC_CODIGO = Valores.Tables(0).Rows(0).Item("Pr_Mc_Codigo")
                    If Valores.Tables(0).Rows(0).Item("pr_mc_creditocontactado") = "1" Then
                        ImgNoContacto.ImageUrl = "~/Imagenes/ImgNContacto.png"
                        TxtPr_Mc_Dtecreditocontactado.Text = Valores.Tables(0).Rows(0).Item("pr_mc_dtecreditocontactado")
                    End If
                    TxtPr_Mc_Primeragestion.Text = Valores.Tables(0).Rows(0).Item("Pr_Mc_Primeragestion")
                    TxtPr_Mc_Dteprimeragestion.Text = Valores.Tables(0).Rows(0).Item("Pr_Mc_Dteprimeragestion")
                    ImgSemaforo.ImageUrl = "~/Imagenes/ImgSemaforoVerde.png"
                    LblVi_Dias_Semaforo_Gestion.Text = Valores.Tables(0).Rows(0).Item("Vi_Dias_Semaforo_Gestion")
                    TmpCrdt.PR_MC_CUENTATRABAJADAFILA = 1
                    TmpUsr.GESTIONES = Val(TmpUsr.GESTIONES) + 1
                    LblGESTIONES.Text = TmpUsr.GESTIONES

                Else
                    LblMsj.Text = Resultado.ToString
                    MpuMensajes.Show()
                End If
            End If
        End If
    End Sub

    Function VALIDAR_PERMISO(ByVal USUARIO As String, ByVal PASS As String) As Boolean
        Dim PERMISO As Boolean = False
        Try
            If CType(Session("USUARIO"), USUARIO).CAT_LO_PGESTION.Substring(5, 1) = 1 Then
                PERMISO = True
            Else
                Try
                    Dim OraCommandL As New OracleCommand
                    OraCommandL.CommandText = "SP_USUARIO"
                    OraCommandL.CommandType = CommandType.StoredProcedure
                    OraCommandL.Parameters.Add("V_USUARIO", OracleType.VarChar).Value = TXTusr.Text
                    OraCommandL.Parameters.Add("V_CONTRASENA", OracleType.VarChar).Value = TXTpwd.Text
                    OraCommandL.Parameters.Add("V_MODULO", OracleType.VarChar).Value = "WEB"
                    OraCommandL.Parameters.Add("V_PANTALLA", OracleType.VarChar).Value = "Autoriza Promesas"
                    OraCommandL.Parameters.Add("CV_1", OracleType.Cursor).Direction = ParameterDirection.Output
                    Dim objDS As DataSet = Consulta_Procedure(OraCommandL, "LogIn")
                    Dim Source As DataView
                    Source = objDS.Tables("LogIn").DefaultView
                    If Source.Count = 0 Then
                        MpuMensajes.Show()
                        LblMsj.Text = "El Usuario No Existe O No Es Correcto, Por Favor Verifique."
                    ElseIf objDS.Tables("LogIn").Rows(0)("cat_lo_estatus") = "Cancelado" Then
                        MpuMensajes.Show()
                        LblMsj.Text = "El Usuario Esta Cancelado, Por Favor Verifique."
                    ElseIf objDS.Tables("LogIn").Rows(0)("cat_lo_pgestion").ToString.Substring(5, 1) = 0 Then
                        MpuMensajes.Show()
                        LblMsj.Text = "Usuario Sin Permisos, El Usuario No Puede Aprobar Promesas"
                    Else
                        PERMISO = True
                    End If
                Catch ex As Exception
                    MpuMensajes.Show()
                    LblMsj.Text = "El Usuario No Existe O No Es Correcto, Por Favor Verifique."
                End Try
            End If
        Catch ex As Exception
        End Try
        Return PERMISO
    End Function

    Protected Sub BtnAceptarPromesa_Click(sender As Object, e As EventArgs) Handles BtnAceptarPromesa.Click
        Dim TmpCrdt As credito = CType(Session("Credito"), credito)
        Dim TmpUsr As USUARIO = CType(Session("USUARIO"), USUARIO)
        Dim TmpAplcc As Aplicacion = CType(Session("Aplicacion"), Aplicacion)
        Dim provisional As Integer = Val(TxtHist_Pr_Montopp.Text)
        If TmpUsr.CAT_LO_PGESTION.ToString.Substring(4, 1) = 1 Then
            If TxtHist_Pr_Motivo.Text.Length < 10 Then
                LblMsjPromesa.Text = "Capture Un Comentario Valido"
                MpuPromesa.Show()
            Else
                Dim Resultado As Object = Class_MasterPage.GuardarGestion(TmpCrdt.PR_MC_CREDITO, TmpCrdt.PR_MC_PRODUCTO, TmpUsr.CAT_LO_USUARIO, DdlHist_Ge_Accion.SelectedValue, DdlHist_Ge_Resultado.SelectedItem.ToString, DdlHist_Ge_Resultado.SelectedValue, DdlHist_Ge_NoPago.SelectedValue, TxtHist_Ge_Comentario.Text, Boleano(CbxHist_Ge_Inoutbound.Checked), TxtHist_Ge_Telefono.Text, TmpCrdt.PR_MC_AGENCIA, TxtHist_Pr_Dtepromesa.Text & " " & DdlHora.SelectedValue & ":00:00", TxtHist_Pr_Montopp.Text, TmpAplcc.ACCION, TmpAplcc.NOPAGO, TmpCrdt.PR_MC_CODIGO, 3, TxtHist_Pr_Motivo.Text, TxtHist_Pr_Supervisor.Text, TmpCrdt.PR_CA_CAMPANA, TmpCrdt.PR_CF_BUCKET, lblNomFila.Text)
                If TypeOf Resultado Is DataSet Then
                    Limpiar()
                    Dim Valores As DataSet = Resultado

                    TxtPr_Mc_Dtegestion.Text = Valores.Tables(0).Rows(0).Item("Pr_Mc_Dtegestion")
                    TxtPr_Mc_Dteresultadorelev.Text = Valores.Tables(0).Rows(0).Item("Pr_Mc_Dteresultadorelev")

                    TxtPr_Mc_Resultadorelev.Text = Valores.Tables(0).Rows(0).Item("Pr_Mc_Resultadorelev")
                    TxtPr_Mc_Resultado.Text = Valores.Tables(0).Rows(0).Item("Pr_Mc_Resultado")
                    TxtPr_Mc_Usuario.Text = TmpUsr.CAT_LO_USUARIO
                    TmpCrdt.PR_MC_CODIGO = Valores.Tables(0).Rows(0).Item("Pr_Mc_Codigo")
                    If Valores.Tables(0).Rows(0).Item("pr_mc_creditocontactado") = "1" Then
                        ImgNoContacto.ImageUrl = "~/Imagenes/ImgContacto.png"
                        TxtPr_Mc_Dtecreditocontactado.Text = Valores.Tables(0).Rows(0).Item("pr_mc_dtecreditocontactado")
                    End If
                    TxtPr_Mc_Primeragestion.Text = Valores.Tables(0).Rows(0).Item("Pr_Mc_Primeragestion")
                    TxtPr_Mc_Dteprimeragestion.Text = Valores.Tables(0).Rows(0).Item("Pr_Mc_Dteprimeragestion")
                    ImgSemaforo.ImageUrl = "~/Imagenes/ImgSemaforoVerde.png"
                    LblVi_Dias_Semaforo_Gestion.Text = Valores.Tables(0).Rows(0).Item("Vi_Dias_Semaforo_Gestion")
                    TmpCrdt.PR_MC_CUENTATRABAJADAFILA = 1

                    TmpUsr.GESTIONES = Val(TmpUsr.GESTIONES) + 1
                    LblGESTIONES.Text = Val(TmpUsr.GESTIONES)
                    If DdlHist_Ge_Resultado.SelectedValue.Split(",")(1) = "1" Then
                        TmpUsr.CUANTASPP = Val(TmpUsr.CUANTASPP) + 1
                        LblCUANTASPP.Text = Val(TmpUsr.CUANTASPP)
                        'TmpUsr.MONTOPP = Val(TmpUsr.MONTOPP) + Val(TxtHist_Pr_Montopp.Text)
                        TxtHist_Pr_Montopp.Text = provisional.ToString
                        TmpUsr.MONTOPP = Val(TmpUsr.MONTOPP.Replace("$", "").Replace(",", "")) + Val(TxtHist_Pr_Montopp.Text)
                        LblMONTOPP.Text = to_money(Val(TmpUsr.MONTOPP.Replace("$", "")))
                    End If
                Else
                    LblMsj.Text = Resultado.ToString
                    MpuMensajes.Show()
                End If
            End If
        Else
            If TxtHist_Pr_Supervisor.Text = "" Then
                LblMsjPromesa.Text = "Capture Un Usuario"
                MpuPromesa.Show()
            ElseIf TxtContrasena.Text = "" Then
                LblMsjPromesa.Text = "Capture Una Contraseña"
                MpuPromesa.Show()
            ElseIf TxtHist_Pr_Motivo.Text.Length < 10 Then
                LblMsjPromesa.Text = "Capture Un Comentario Valido"
                MpuPromesa.Show()
            ElseIf Class_Negociaciones.LlenarElementosNego(TxtHist_Pr_Supervisor.Text, TxtContrasena.Text, "", 5).Tables("ELEMENTOS").Rows(0).Item("Permiso") = 0 Then
                LblMsjPromesa.Text = "Usuario O Contraseña Incorrectos O Usuario Sin Facultades"
                MpuPromesa.Show()
            Else
                Dim Resultado As Object = Class_MasterPage.GuardarGestion(TmpCrdt.PR_MC_CREDITO, TmpCrdt.PR_MC_PRODUCTO, TmpUsr.CAT_LO_USUARIO, DdlHist_Ge_Accion.SelectedValue, DdlHist_Ge_Resultado.SelectedItem.ToString, DdlHist_Ge_Resultado.SelectedValue, DdlHist_Ge_NoPago.SelectedValue, TxtHist_Ge_Comentario.Text, Boleano(CbxHist_Ge_Inoutbound.Checked), TxtHist_Ge_Telefono.Text, TmpCrdt.PR_MC_AGENCIA, TxtHist_Pr_Dtepromesa.Text & " " & DdlHora.SelectedValue & ":00:00", TxtHist_Pr_Montopp.Text, TmpAplcc.ACCION, TmpAplcc.NOPAGO, TmpCrdt.PR_MC_CODIGO, 3, TxtHist_Pr_Motivo.Text, TxtHist_Pr_Supervisor.Text, TmpCrdt.PR_CA_CAMPANA, TmpCrdt.PR_CF_BUCKET, lblNomFila.Text)
                If TypeOf Resultado Is DataSet Then
                    Limpiar()
                    Dim Valores As DataSet = Resultado

                    TxtPr_Mc_Dtegestion.Text = Valores.Tables(0).Rows(0).Item("Pr_Mc_Dtegestion")
                    TxtPr_Mc_Dteresultadorelev.Text = Valores.Tables(0).Rows(0).Item("Pr_Mc_Dteresultadorelev")
                    TxtPr_Mc_Resultadorelev.Text = Valores.Tables(0).Rows(0).Item("Pr_Mc_Resultadorelev")
                    TxtPr_Mc_Resultado.Text = Valores.Tables(0).Rows(0).Item("Pr_Mc_Resultado")
                    TxtPr_Mc_Usuario.Text = TmpUsr.CAT_LO_USUARIO
                    TmpCrdt.PR_MC_CODIGO = Valores.Tables(0).Rows(0).Item("Pr_Mc_Codigo")
                    If Valores.Tables(0).Rows(0).Item("pr_mc_creditocontactado") = "1" Then
                        ImgNoContacto.ImageUrl = "~/Imagenes/ImgContacto.png"
                        TxtPr_Mc_Dtecreditocontactado.Text = Valores.Tables(0).Rows(0).Item("pr_mc_dtecreditocontactado")
                    End If
                    TxtPr_Mc_Primeragestion.Text = Valores.Tables(0).Rows(0).Item("Pr_Mc_Primeragestion")
                    TxtPr_Mc_Dteprimeragestion.Text = Valores.Tables(0).Rows(0).Item("Pr_Mc_Dteprimeragestion")
                    ImgSemaforo.ImageUrl = "~/Imagenes/ImgSemaforoVerde.png"
                    LblVi_Dias_Semaforo_Gestion.Text = Valores.Tables(0).Rows(0).Item("Vi_Dias_Semaforo_Gestion")
                    TmpCrdt.PR_MC_CUENTATRABAJADAFILA = 1

                Else
                    LblMsj.Text = Resultado.ToString
                    MpuMensajes.Show()
                End If
            End If
        End If
    End Sub
    Sub Limpiar()
        ValidaConfiguracion()
        TxtHist_Pr_Montopp.Text = ""
        TxtHist_Pr_Montopp.Visible = False
        LblHist_Pr_Montopp.Visible = False
        LblHist_Pr_Dtepromesa.Text = "Proximo Contacto"
        LblMsjPromesa.Text = ""
        TxtHist_Pr_Supervisor.Text = ""
        TxtHist_Ge_Telefono.Text = ""
        TxtHist_Ge_Comentario.Text = ""
        TxtHist_Pr_Dtepromesa.Text = ""
        LblUsr.Visible = False
        TXTusr.Visible = False
        TXTpwd.Visible = False
        LblPwd.Visible = False
        Dim TmpCrdt As credito = CType(Session("Credito"), credito)
        GvHistAct.DataSource = Class_Hist_Act.LlenarElementosHistAct(TmpCrdt.PR_MC_CREDITO, "Hist_Ge_Dteactividad", "DESC", 0)
        GvHistAct.DataBind()

        GVHist_Atencion_C.DataSource = Class_Hist_Act.LlenarElementosHistAct(TmpCrdt.PR_MC_CREDITO, "Hist_At_Dteactividad", "DESC", 3)
        GVHist_Atencion_C.DataBind()

    End Sub
    Protected Sub BtnMantenimiento_Click(sender As Object, e As EventArgs) Handles BtnMantenimiento.Click
        Response.Redirect("Agenda.aspx", False)
    End Sub

    Protected Sub BtnAgenda_Click(sender As Object, e As EventArgs) Handles BtnAgenda.Click
        LlenarAgenda()
    End Sub
    Protected Sub BtnAvisos_Click(sender As Object, e As EventArgs) Handles BtnAvisos.Click
        LlenarAvisos()
    End Sub
    Sub LlenarAgenda()
        GvBusquedas.DataSource = Class_MasterPage.Agenda(CType(Session("USUARIO"), USUARIO).CAT_LO_USUARIO, 7)
        GvBusquedas.DataBind()
        LblTitulo.Text = "Agenda"
        MpuAcciones.Show()
        BtnAceptarAcciones.Visible = False
    End Sub
    Sub LlenarAvisos()
        GvBusquedas.DataSource = Class_MasterPage.Agenda(CType(Session("USUARIO"), USUARIO).CAT_LO_USUARIO, 6)
        GvBusquedas.DataBind()
        LblTitulo.Text = "Avisos"
        MpuAcciones.Show()
        BtnAceptarAcciones.Visible = True
        BtnAceptarAcciones.Text = "Marcar Como Leidos"
        GvBusquedas.HeaderRow.Cells(0).Visible = False
        For Each gvRow As GridViewRow In GvBusquedas.Rows
            gvRow.Cells(0).Visible = False
        Next
    End Sub

    Protected Sub BtnAceptarAcciones_Click(sender As Object, e As EventArgs) Handles BtnAceptarAcciones.Click
        Class_MasterPage.MarcarLeidos(5, LblCat_Lo_Usuario.Text)
        Dim DtsAgenda As DataSet = Class_MasterPage.Agenda(LblCat_Lo_Usuario.Text, 5)
        LblAgenda.Text = (DtsAgenda.Tables(0).Rows(0).Item("Valor"))
    End Sub

    Sub PERMISOS()
        Dim TmpUsr As USUARIO = CType(Session("Usuario"), USUARIO)
        Dim TMpCredito As credito = CType(Session("credito"), credito)

        For count As Integer = 0 To Len((CType(Session("Usuario"), USUARIO)).CAT_LO_MGESTION) - 1
            If TmpUsr.CAT_LO_MGESTION.ToString.Substring(count, 1) = 0 Then
                TCM.Tabs(count).Enabled = False
            End If
        Next

        If TmpUsr.CAT_LO_PGESTION.ToString.Substring(2, 1) = 1 Then ' Visitas
            ImgAddVisita.Visible = True
        End If
        If TmpUsr.CAT_LO_PGESTION.ToString.Substring(3, 1) = 0 Then 'Calificacion Telefonica
            GvCalTelefonos.Enabled = False
        End If
        'If TMpCredito.PR_JU_LEGAL = 0 Then
        '    TCM.Tabs(8).Visible = False
        'End If
    End Sub
    Protected Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        Try
            Dim USR As String = (CType(Session("Usuario"), USUARIO)).CAT_LO_USUARIO
        Catch ex As Exception
            Session.Clear()
            Session.Abandon()
            Response.Redirect("LogIn.aspx", False)
        End Try
        Try
            Dim USUARIO As USUARIO = CType(Session("Usuario"), USUARIO)
            LblCat_Lo_Nombre.Text = USUARIO.CAT_LO_NOMBRE
            LblCAT_PE_PERFIL.Text = USUARIO.CAT_PE_PERFIL
            LblCat_Lo_Usuario.Text = USUARIO.CAT_LO_USUARIO
            LblCat_Lo_Productos.Text = USUARIO.CAT_LO_PRODUCTOS
        Catch ex As Exception
            SendMail("Page_PreInit", ex, "", "", LblCat_Lo_Usuario.Text)
        End Try

    End Sub
    Protected Sub GvHist_Act_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GvHistAct.PageIndexChanging
        GvHistAct.PageIndex = e.NewPageIndex
        GvHistAct.DataBind()
        Dim TmpCrdt As credito = CType(Session("Credito"), credito)
        GvHistAct.DataSource = Class_Hist_Act.LlenarElementosHistAct(TmpCrdt.PR_MC_CREDITO, "Hist_Ge_Dteactividad", "DESC", 0)
        GvHistAct.DataBind()
    End Sub
    Protected Sub GVHist_Atencion_C_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GVHist_Atencion_C.PageIndexChanging
        GVHist_Atencion_C.PageIndex = e.NewPageIndex
        GVHist_Atencion_C.DataBind()
        Dim TmpCrdt As credito = CType(Session("Credito"), credito)
        GVHist_Atencion_C.DataSource = Class_Hist_Act.LlenarElementosHistAct(TmpCrdt.PR_MC_CREDITO, "Hist_At_Dteactividad", "DESC", 3)
        GVHist_Atencion_C.DataBind()
    End Sub
    Protected Sub RblSortTipo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles RblSortTipo.SelectedIndexChanged
        Dim TmpCrdt As credito = CType(Session("Credito"), credito)
        GvHistAct.DataSource = Class_Hist_Act.LlenarElementosHistAct(TmpCrdt.PR_MC_CREDITO, DdlSort.SelectedValue, RblSortTipo.SelectedValue, 0)
        GvHistAct.DataBind()
        RblSortTipo.Items(0).Selected = False
        RblSortTipo.Items(1).Selected = False
    End Sub

    Public ReadOnly Property NombreFila() As String
        Get
            Return lblNomFila.Text
        End Get
    End Property

    Protected Sub Llenar() 'Valida la existencia de las filas de trabajo
        Dim TmpCrdt As credito = CType(Session("Credito"), credito)
        Dim TmpUsr As USUARIO = CType(Session("USUARIO"), USUARIO)
        Dim dtsFilas As DataSet = Class_Agenda.LlenarElementosAgenda("", TmpUsr.CAT_LO_USUARIO, 9) 'valida si hay creditos en fila de trabajo
        Dim Credito As String = dtsFilas.Tables(0).Rows(0).Item("Credito")

        If Credito <> "NA" And Session("BUSCAR") = 0 Then
            Class_MasterPage.Tocar(Credito, LblCat_Lo_Usuario.Text)
            LlenarCredito.Busca(Credito)
            ImgNext.Visible = True
            TmpCrdt.PR_MC_CUENTATRABAJADAFILA = 0
            lblNomFila.Text = dtsFilas.Tables(0).Rows(0).Item("NomFila")
            Session("NombreFila") = dtsFilas.Tables(0).Rows(0).Item("NomFila").ToString
            lblNomFila.Visible = True
            HiddenInicioFila.Value = dtsFilas.Tables(0).Rows(0).Item("InicioFila")

        ElseIf Credito <> "NA" And Session("BUSCAR") = 1 Then
            ImgNext.Visible = True
            Session("NombreFila") = ""
        Else
            ImgNext.Visible = False
            Session("NombreFila") = ""
        End If
        If TmpCrdt.PR_MC_CREDITO <> "" Then
            GvHistAct.DataSource = Class_Hist_Act.LlenarElementosHistAct(TmpCrdt.PR_MC_CREDITO, "Hist_Ge_Dteactividad", "DESC", 0)
            GvHistAct.DataBind()

            GVHist_Atencion_C.DataSource = Class_Hist_Act.LlenarElementosHistAct(TmpCrdt.PR_MC_CREDITO, "Hist_At_Dteactividad", "DESC", 3)
            GVHist_Atencion_C.DataBind()

            TxtHist_Pr_Dtepromesa_CalendarExtender.StartDate = DateTime.Now.ToShortDateString
            UpnlGestion.Visible = True
            TCM.Visible = True
            PERMISOS()
            CreditoRetirado(TmpCrdt.PR_MC_ESTATUS, TmpCrdt.PR_MC_CODIGO)
            GvVisitas.DataSource = Class_CapturaVisitas.LlenarElementosVisitas(CType(Session("Credito"), credito).PR_MC_CREDITO, 3)
            GvVisitas.DataBind()
            For Each gvRow As GridViewRow In GvVisitas.Rows
                gvRow.Cells(10).BackColor = System.Drawing.ColorTranslator.FromHtml("#" & gvRow.Cells(10).Text)
                gvRow.Cells(10).ForeColor = System.Drawing.ColorTranslator.FromHtml("#" & gvRow.Cells(10).Text)
                gvRow.Cells(11).BackColor = System.Drawing.ColorTranslator.FromHtml("#" & gvRow.Cells(11).Text)
                gvRow.Cells(11).ForeColor = System.Drawing.ColorTranslator.FromHtml("#" & gvRow.Cells(11).Text)
            Next

            LblPr_Mc_Credito.Visible = True
            lblPr_Mc_CreditoV.Text = TmpCrdt.PR_MC_CREDITO
            lblPr_Mc_CreditoV.Visible = True
            LblPr_Cd_Nombre.Visible = True
            LblPr_Cd_NombreV.Text = HttpUtility.HtmlDecode(TmpCrdt.PR_CD_NOMBRE)
            LblPr_Cd_NombreV.Visible = True
            LblPr_Mc_Expediente.Visible = True
            LblPr_Mc_ExpedienteV.Text = TmpCrdt.PR_MC_EXPEDIENTE
            LblPr_Mc_ExpedienteV.Visible = True
            LblPr_Mc_EstatusF.Visible = True
            LblPr_Mc_EstatusFV.Text = TmpCrdt.CAT_EC_ESTATUS
            LblPr_Mc_EstatusFV.Visible = True
            LblPr_Mc_EstatusF_Text.Visible = True
            LblPr_Mc_EstatusF_TextV.Text = TmpCrdt.PR_MC_ESTATUSF
            LblPr_Mc_EstatusF_TextV.Visible = True


            If TmpCrdt.PR_MC_ESTATUSF = 1 Then
                LblPr_Mc_EstatusF_TextV.BackColor = Color.Green
                LblPr_Mc_EstatusFV.BackColor = Color.Green
            Else
                LblPr_Mc_EstatusF_TextV.BackColor = Color.Red
                LblPr_Mc_EstatusFV.BackColor = Color.Red
            End If



            ImgSemaforo.ToolTip = TmpCrdt.VI_DIAS_SEMAFORO_GESTION
            If TmpCrdt.VI_SEMAFORO_GESTION = "ROJO" Then
                ImgSemaforo.ImageUrl = "Imagenes/ImgSemaforoRojo.png"
            ElseIf TmpCrdt.VI_SEMAFORO_GESTION = "AMARILLO" Then
                ImgSemaforo.ImageUrl = "Imagenes/ImgSemaforoAmarillo.png"
            ElseIf TmpCrdt.VI_SEMAFORO_GESTION = "VERDE" Then
                ImgSemaforo.ImageUrl = "Imagenes/ImgSemaforoVerde.png"
            Else
                ImgSemaforo.ImageUrl = "Imagenes/ImgSemaforoRojo.png"
            End If

            If TmpCrdt.PR_MC_CREDITOCONTACTADO = 0 Then
                ImgNoContacto.ImageUrl = "Imagenes/ImgNoContacto.png"
            Else
                ImgNoContacto.ImageUrl = "Imagenes/ImgContacto.png"
            End If
            TxtPr_Mc_Dtecreditocontactado.Text = TmpCrdt.PR_MC_DTECREDITOCONTACTADO
            TxtPr_Mc_Dteprimeragestion.Text = TmpCrdt.PR_MC_DTEPRIMERAGESTION
            TxtPr_Mc_Primeragestion.Text = TmpCrdt.PR_MC_PRIMERAGESTION
            TxtPr_Mc_Usuario.Text = TmpCrdt.PR_MC_USUARIO
            TxtPr_Mc_Uasignado.Text = TmpCrdt.PR_MC_UASIGNADO
            TxtPr_Mc_Resultado.Text = TmpCrdt.PR_MC_RESULTADO
            TxtPr_Mc_Dtegestion.Text = TmpCrdt.PR_MC_DTEGESTION
            TxtPr_Mc_Resultadorelev.Text = TmpCrdt.PR_MC_RESULTADORELEV
            TxtPr_Mc_Dteresultadorelev.Text = TmpCrdt.PR_MC_DTERESULTADORELEV
            LblVi_Dias_Semaforo_Gestion.Text = TmpCrdt.VI_DIAS_SEMAFORO_GESTION
            Session("BUSCAR") = 0
            Try
                Session("Saldosms") = QCampo(ExecutarCurl("https://rest.quiubas.com/1.0/balance", "", "", "GET", 0), 2)
                If Session("Saldosms") Is Nothing Then
                    Session("Saldosms") = 0
                End If
            Catch ex As Exception
                Session("Saldosms") = 0
            End Try
            LlenarTelefonos(TmpCrdt.PR_MC_CREDITO)
        End If
    End Sub
    Protected Sub GvBusquedas_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GvBusquedas.SelectedIndexChanged
        Try
            Dim USR As String = (CType(Session("Usuario"), USUARIO)).CAT_LO_USUARIO
        Catch ex As Exception
            Session.Clear()
            Session.Abandon()
            Response.Redirect("ExpiroSesion.aspx", False)
        End Try
        Try
            Dim row As GridViewRow = GvBusquedas.SelectedRow
            Class_MasterPage.Tocar(row.Cells(1).Text, LblCat_Lo_Usuario.Text)
            LlenarCredito.Busca(row.Cells(1).Text)
            Session("Buscar") = 1
            Dim Tmpcrdt As credito = (CType(Session("Credito"), credito))
            Tmpcrdt.PR_MC_CUENTATRABAJADAFILA = 1
            Response.Redirect("MasterPage.aspx", False)
        Catch ex As System.Threading.ThreadAbortException
        Catch ax As Exception
            SendMail("GvBusquedas_SelectedIndexChanged", ax, CType(Session("Credito"), credito).PR_MC_CREDITO, "", CType(Session("Usuario"), USUARIO).CAT_LO_USUARIO)
        End Try
    End Sub
    Protected Sub SESION_Click(sender As Object, e As EventArgs) Handles SESION.Click
        ValidaLicencias(CType(Session("USUARIO"), USUARIO).Licencias, LblCat_Lo_Usuario.Text, "", DateTime.Now, 4, CType(Session("IdSession"), String))
        Session.Clear()
        Session.Abandon()
        Response.Redirect("Login.aspx", False)
    End Sub

    Private Sub SendMail(ByRef evento As String, ByVal ex As Exception, ByVal Cuenta As String, ByVal Captura As String, ByVal usr As String)
        EnviarCorreo("MasterPage.aspx", evento, ex, Cuenta, Captura, usr)
    End Sub

    Protected Sub BtnBuscar_Click(sender As Object, e As EventArgs) Handles BtnBuscar.Click
        If TxtBuscar.Text.Length < 1 Then
            LblMsj.Text = "No existen resultados para su busqueda"
            MpuMensajes.Show()
        Else
            Dim DtsBusca As DataSet = Busquedas.Search("'%" & TxtBuscar.Text.ToUpper & "%'")
            Dim DtvBusca As DataView = DtsBusca.Tables("Busca").DefaultView
            If DtvBusca.Count = 0 Then
                LblMsj.Text = "No existen resultados para su busqueda"
                MpuMensajes.Show()
            ElseIf DtvBusca.Count = 1 Then
                Dim Tmpcrdt As credito = (CType(Session("Credito"), credito))
                Class_MasterPage.Tocar(DtsBusca.Tables(0).Rows(0).Item("Credito"), LblCat_Lo_Usuario.Text)
                LlenarCredito.Busca(DtsBusca.Tables(0).Rows(0).Item("Credito"))
                Session("Buscar") = 1
                Tmpcrdt.PR_MC_CUENTATRABAJADAFILA = 1
                Response.Redirect("MasterPage.aspx", False)
            Else
                GvBusquedas.DataSource = DtsBusca
                GvBusquedas.DataBind()
                LblTitulo.Text = "Se han encontrado " & DtvBusca.Count & " resultados seleccione uno."
                MpuAcciones.Show()
            End If
        End If
    End Sub

    Sub LlenarTelefonos(ByVal V_Valor As String)
        Dim DtsTelefonos As DataSet = Class_MasterPage.Telefonos(V_Valor, "", "", "", "", "", "", 0)
        Dim DtvTelefonos As DataView = DtsTelefonos.Tables("Telefonos").DefaultView
        If DtvTelefonos.Count > 0 Then
            GvCalTelefonos.DataSource = DtsTelefonos
            GvCalTelefonos.DataBind()
            Liketelefonos()
        End If
    End Sub
    Sub Liketelefonos()
        GvCalTelefonos.HeaderRow.Cells(1).Visible = False
        GvCalTelefonos.HeaderRow.Cells(2).Visible = False
        GvCalTelefonos.HeaderRow.Cells(3).Visible = False
        GvCalTelefonos.HeaderRow.Cells(4).Visible = False
        GvCalTelefonos.HeaderRow.Cells(5).Visible = False
        'GvCalTelefonos.HeaderRow.Cells(6).Visible = False
        'GvCalTelefonos.HeaderRow.Cells(7).Visible = False
        GvCalTelefonos.HeaderRow.Cells(8).Visible = False
        GvCalTelefonos.HeaderRow.Cells(9).Visible = False
        GvCalTelefonos.HeaderRow.Cells(10).Visible = False
        GvCalTelefonos.HeaderRow.Cells(11).Visible = False
        GvCalTelefonos.HeaderRow.Cells(12).Visible = False
        'GvCalTelefonos.HeaderRow.Cells(13).Visible = False
        GvCalTelefonos.HeaderRow.Cells(14).Visible = False
        GvCalTelefonos.HeaderRow.Cells(0).Text = "Calificacion Telefónica"
        For Each gvRow As GridViewRow In GvCalTelefonos.Rows
            gvRow.Cells(1).Visible = False
            gvRow.Cells(2).Visible = False
            gvRow.Cells(3).Visible = False
            gvRow.Cells(4).Visible = False
            gvRow.Cells(5).Visible = False
            'gvRow.Cells(6).Visible = False
            'gvRow.Cells(7).Visible = False
            gvRow.Cells(8).Visible = False
            gvRow.Cells(9).Visible = False
            gvRow.Cells(10).Visible = False
            gvRow.Cells(11).Visible = False
            gvRow.Cells(12).Visible = False
            'gvRow.Cells(13).Visible = False
            gvRow.Cells(14).Visible = False
            Dim ImgValido As ImageButton = DirectCast(gvRow.FindControl("ImgValido"), ImageButton)
            Dim ImgNoValido As ImageButton = DirectCast(gvRow.FindControl("ImgNoValido"), ImageButton)
            Dim ImgLike As WebControls.Image = DirectCast(gvRow.FindControl("ImgLike"), WebControls.Image)
            Dim ImgNoLike As WebControls.Image = DirectCast(gvRow.FindControl("ImgNoLike"), WebControls.Image)
            Dim ImageSMS = TryCast(gvRow.FindControl("ImgSMS"), ImageButton)

            ImgValido.ToolTip = gvRow.Cells(2).Text & ",Ultimo Contacto " & gvRow.Cells(3).Text
            ImgNoValido.ToolTip = gvRow.Cells(4).Text & ",Ultima Calificacion " & gvRow.Cells(5).Text
            If Val(gvRow.Cells(8).Text) = 0 Then
                ImgLike.Visible = False
                ImgNoLike.Visible = True
            End If
            If Session("Saldosms") = "0" Then
                ImageSMS.ImageUrl = "~/Imagenes/ImgSmsSaldo.png"
                ImageSMS.ToolTip = "NoValido"
                ImageSMS.Enabled = False
            Else
                If (CType(Session("Usuario"), USUARIO)).CAT_LO_PGESTION.Substring(5, 1) = "0" Then
                    ImageSMS.Enabled = False
                    ImageSMS.ImageUrl = "~/Imagenes/ImgSMSSP.png"
                Else
                    ImageSMS.Enabled = True
                    ImageSMS.ImageUrl = "~/Imagenes/ImgSms.png"
                End If
            End If
        Next
    End Sub

    Protected Sub ImgValido_Click(sender As Object, e As ImageClickEventArgs)
        Try
            Dim USR As String = (CType(Session("Usuario"), USUARIO)).CAT_LO_USUARIO
        Catch ex As Exception
            Session.Clear()
            Session.Abandon()
            Response.Redirect("ExpiroSesion.aspx", False)
        End Try
        'Try


        Dim selectedRow As GridViewRow = TryCast(DirectCast(sender, Control).Parent.NamingContainer, GridViewRow)
        Dim Quien As ImageButton = sender
        Dim calificacion As String
        If Quien.ID = "ImgValido" Then
            Class_MasterPage.Telefonos(lblPr_Mc_CreditoV.Text, selectedRow.Cells(9).Text, selectedRow.Cells(11).Text, selectedRow.Cells(14).Text, selectedRow.Cells(7).Text, "Valido", selectedRow.Cells(1).Text, 1)
            TxtHist_Ge_Telefono.Text = selectedRow.Cells(1).Text
        Else
            Class_MasterPage.Telefonos(lblPr_Mc_CreditoV.Text, selectedRow.Cells(10).Text, selectedRow.Cells(12).Text, selectedRow.Cells(14).Text, selectedRow.Cells(7).Text, "NoValido", selectedRow.Cells(1).Text, 1)
            calificacion = "NoValido"
        End If
        LlenarTelefonos(lblPr_Mc_CreditoV.Text)
        'Catch ex As Exception
        '    SendMail("Valido_Click", ex, CType(Session("Credito"), Credito).PR_MC_CREDITO, "", (CType(Session("Usuario"), USUARIO)).CAT_LO_USUARIO)
        'End Try
    End Sub
    Protected Sub ImgAddVisita_Click(sender As Object, e As ImageClickEventArgs) Handles ImgAddVisita.Click
        Response.Redirect("CapturaVisitas.aspx", False)
    End Sub
    Protected Sub Bnt_Sms(sender As Object, e As System.EventArgs)
        Try
            Dim USR As String = (CType(Session("Usuario"), USUARIO)).CAT_LO_USUARIO
        Catch ex As Exception
            Session.Clear()
            Session.Abandon()
            Response.Redirect("LogIn.aspx")
        End Try
        Try
            Dim selectedRow As GridViewRow = TryCast(DirectCast(sender, Control).Parent.NamingContainer, GridViewRow)
            Session("Saldosms") = QCampo(ExecutarCurl("https://rest.quiubas.com/1.0/balance", "", "", "GET", 0), 2)
            If Session("Saldosms") Is Nothing Then
                Session("Saldosms") = 0
            Else
                If Val(Session("Saldosms")) > 0 Then
                    If selectedRow.Cells(8).Text = "1" Then

                        Dim DtsSms As DataSet = Class_SMS.LlenarElementos("", "", "", "", "", "", "", "", "", "", 1)
                        DdlOpcionSms.DataTextField = "Plantilla"
                        DdlOpcionSms.DataValueField = "Plantilla"
                        DdlOpcionSms.DataSource = DtsSms
                        DdlOpcionSms.DataBind()
                        DdlOpcionSms.Items.Add("Seleccione")
                        DdlOpcionSms.SelectedValue = "Seleccione"

                        LblMsjSms.Text = "Enviar SMS Al Teléfono "
                        LblTelSMS.Text = selectedRow.Cells(1).Text
                        LblTelSMSTIPO.Text = selectedRow.Cells(6).Text
                        MpuESMS.Show()
                    Else
                        MpuMensajes.Show()
                        LblMsj.Text = "Telefono No Valido, Imposible Enviar SMS"
                    End If
                End If
            End If

        Catch ex As Exception
            SendMail("Bnt_Sms", ex, "", "", LblCat_Lo_Usuario.Text)
        End Try
    End Sub
    Protected Sub BtnEnviarSms_Click(sender As Object, e As EventArgs) Handles BtnEnviarSms.Click
        Try
            Dim TmpUsr As String = CType(Session("Usuario"), USUARIO).CAT_LO_USUARIO
            Dim TmpCredito = CType(Session("credito"), credito).PR_MC_CREDITO
            If DdlOpcionSms.SelectedValue = "Seleccione" Then
                LblMsjSms.Text = "Seleccione El Mensaje Del SMS"
                MpuESMS.Show()
            Else
                 Dim Cadena As String = ExecutarCurl("https://rest.quiubas.com/1.0/sms", LblTelSMS.Text, TxtSms.Text, "Post", 1)
                If QCampo(Cadena, 10).Trim = "sending" Then
                    Class_SMS.LlenarElementos(QCampo(Cadena, 3), LblTelSMS.Text, TxtSms.Text, QCampo(Cadena, 10), TmpUsr, "Gestion", TmpCredito, DdlOpcionSms.SelectedValue, LblTelSMSTIPO.Text, QCampo(Cadena, 7), 3)
                End If
                LblMsj.Text = "SMS Enviado"
                MpuMensajes.Show()
            End If
        Catch ex As Exception
            SendMail("BtnEnviarSms.Click", ex, "", "", LblCat_Lo_Usuario.Text)
        End Try
    End Sub
    Protected Sub DdlOpcionSms_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlOpcionSms.SelectedIndexChanged
        If DdlOpcionSms.SelectedValue = "Seleccione" Then
            TxtSms.Text = ""
        Else
            Dim DtsSms As DataSet = Class_SMS.LlenarElementos(DdlOpcionSms.SelectedValue, CType(Session("credito"), credito).PR_MC_CREDITO, "", "", "", "", "", "", "", "", 2)
            TxtSms.Text = DtsSms.Tables(0).Rows(0).Item("MENSAJE")
        End If
        MpuESMS.Show()
    End Sub
    Protected Sub ImgNext_Click(sender As Object, e As ImageClickEventArgs) Handles ImgNext.Click
        Dim TmpCrdt As credito = CType(Session("Credito"), credito)
        Dim TmpUsr As USUARIO = CType(Session("USUARIO"), USUARIO)
        If TmpCrdt.PR_MC_CUENTATRABAJADAFILA = 0 Then
            LblMsj.Text = HttpUtility.HtmlDecode("Debe Trabajar El Crédito Antes De Continuar")
            MpuMensajes.Show()
        Else
            Dim Credito As String = Class_Agenda.LlenarElementosAgenda("", TmpUsr.CAT_LO_USUARIO, 9).Tables(0).Rows(0).Item("Credito")
            If Credito <> "NA" And Session("BUSCAR") = 0 Then
                ImgNext.Visible = True
                Response.Redirect("MasterPage.aspx", False)
            Else
                LblMsj.Text = "No Hay Más Créditos Por Trabajar"
                MpuMensajes.Show()
                ImgNext.Visible = False
            End If
        End If
    End Sub

    Protected Sub buscarNuevo_Click(sender As Object, e As EventArgs) Handles buscarNuevo.Click
        Try
            'Dim DtsCredito As DataSet = Class_MasterPage.RegresaCredito(TextoBusca.Value, "", 10)
            'Dim DtvCredito As DataView = DtsCredito.Tables("Credito").DefaultView
            'If DtvCredito.Count > 0 Then
            LlenarCredito.Busca(TextoBusca.Value)
            Session("Buscar") = 1
            Dim Tmpcrdt As credito = (CType(Session("Credito"), credito))
            Tmpcrdt.PR_MC_CUENTATRABAJADAFILA = 1
            Class_MasterPage.Tocar(TextoBusca.Value, LblCat_Lo_Usuario.Text)
            Response.Redirect("MasterPage.aspx", False)
            'Else
            'LblMsj.Text = "Crédito Enviado Por Marcador No Valido"
            'MpuMensajes.Show()
            'End If
        Catch ex As System.Threading.ThreadAbortException
        Catch ex As Exception
            LblMsj.Text = "Crédito Enviado Por Marcador No Valido"
            MpuMensajes.Show()
        End Try
    End Sub


End Class
