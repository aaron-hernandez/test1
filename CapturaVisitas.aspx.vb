Imports System.Data.OracleClient
Imports System.Data
Imports Db
Imports Funciones
Imports System.Web.Services
Imports System.Globalization
Imports Busquedas
Imports Class_InformacionAdicional
Imports System.Web.Script.Serialization

Partial Class CapturaVisitas
    Inherits System.Web.UI.Page
    <WebMethod(EnableSession:=True)> _
    Public Shared Function KeepActiveSession(ByVal Usuario As String) As String
        Dim DtsConectado As DataSet = Class_Sesion.LlenarElementos(Usuario, "", "", "", 3, "", "", "", "")
        If DtsConectado.Tables(0).Rows(0).Item("Cuantas") <> "0" Then
            Return "Hola"
        Else
            Return "Bye"
        End If
    End Function

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If ValidaLicencias(300, LblCat_Lo_Usuario.Text, "", "", 1, "") = 1 Then
                If Not IsPostBack Then
                    Llenar()
                    PERMISOS()
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
    Sub CreditoRetirado(ByVal V_Estatus As String)
        If (V_Estatus = "Retirada") Or (V_Estatus = "Liquidada") Then
            UpnlRetirada.Visible = True
            UpnlVisita.Visible = False
        Else
            UpnlVisita.Visible = True
            ValidaConfiguracion()
        End If
    End Sub
    Sub PERMISOS()
        Dim TmpUsr As USUARIO = CType(Session("Usuario"), USUARIO)
        If Val(TmpUsr.CAT_LO_MGESTION) <> 0 Then
            ImgAddGestion.Visible = True
        End If
    End Sub
    Protected Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        Try
            Dim USR As String = (CType(Session("Usuario"), USUARIO)).CAT_LO_USUARIO
        Catch ex As Exception
            Session.Clear()
            Session.Abandon()
            Response.Redirect("LogIn.aspx")
        End Try
        Try
            Dim USUARIO As USUARIO = CType(Session("Usuario"), USUARIO)
            LblCat_Lo_Nombre.Text = USUARIO.CAT_LO_NOMBRE
            LblCAT_PE_PERFIL.Text = USUARIO.CAT_PE_PERFIL
            LblCat_Lo_Usuario.Text = USUARIO.CAT_LO_USUARIO
            LblCat_Lo_Productos.Text = USUARIO.CAT_LO_PRODUCTOS
        Catch ex As Exception
            SendMail("Page_PreInit", ex, "", "", "")
        End Try

    End Sub

    Protected Sub Llenar()
        Dim TmpCrdt As Credito = CType(Session("Credito"), Credito)
        If TmpCrdt.PR_MC_CREDITO <> "" Then
            TxtHist_Pr_Dtepromesa_CalendarExtender.StartDate = DateTime.Now.ToShortDateString
            TxtHist_Vi_Dtevisita_CalendarExtender.EndDate = DateTime.Now.ToShortDateString
            GvDiasVisita.DataSource = Class_CapturaVisitas.LlenarElementosVisitas(CType(Session("Credito"), Credito).PR_MC_CREDITO, 0)
            GvDiasVisita.DataBind()
            DdlHist_Vi_Parentesco.DataTextField = "Parentesco"
            DdlHist_Vi_Parentesco.DataValueField = "Parentesco"
            DdlHist_Vi_Parentesco.DataSource = Class_CapturaVisitas.LlenarElementosVisitas(CType(Session("Credito"), Credito).PR_MC_CREDITO, 1)
            DdlHist_Vi_Parentesco.DataBind()
            DdlHist_Vi_Visitador.DataTextField = "Nombre"
            DdlHist_Vi_Visitador.DataValueField = "Usuario"
            DdlHist_Vi_Visitador.DataSource = Class_CapturaVisitas.LlenarElementosVisitas(CType(Session("Credito"), Credito).PR_MC_CREDITO, 2)
            DdlHist_Vi_Visitador.DataBind()
            DdlHist_Vi_Visitador.Items.Add("Seleccione")
            DdlHist_Vi_Visitador.SelectedValue = "Seleccione"
            CreditoRetirado(TmpCrdt.PR_MC_ESTATUS)
            LblPr_Mc_Credito.Visible = True
            lblPr_Mc_CreditoV.Text = TmpCrdt.PR_MC_CREDITO
            lblPr_Mc_CreditoV.Visible = True
            LblPr_Cd_Nombre.Visible = True
            LblPr_Cd_NombreV.Text = TmpCrdt.PR_CD_NOMBRE
            LblPr_Cd_NombreV.Visible = True
            LblPr_Mc_Expediente.Visible = True
            LblPr_Mc_ExpedienteV.Text = TmpCrdt.PR_MC_EXPEDIENTE
            LblPr_Mc_ExpedienteV.Visible = True
            TxtPR_CD_CALLEYNUM.Text = TmpCrdt.PR_CD_CALLE_NUM
            TxtPR_CD_CIUDAD.Text = TmpCrdt.PR_CD_DEL_MPIO
            TxtPR_CD_COLONIA.Text = TmpCrdt.PR_CD_COLONIA
            TxtPR_CD_CP.Text = TmpCrdt.PR_CD_CP
            TxtPR_CD_EMAIL.Text = TmpCrdt.PR_CD_EMAIL
            TxtPR_CD_ESTADO.Text = TmpCrdt.PR_CD_ESTADO
            TxtPR_CD_RFC.Text = " "
            'Lenar Datos VIsita
            Dim DtsVisitas As DataSet = Class_CapturaVisitas.LlenarElementosVisitas(CType(Session("Credito"), Credito).PR_MC_CREDITO, 4)
            Dim DtvVisitas As DataView = DtsVisitas.Tables(0).DefaultView
            If DtvVisitas.Count > 0 Then
                TxtHist_Vi_Entrecalle1.Text = DtsVisitas.Tables(0).Rows(0).Item("Hist_Vi_Entrecalle1")
                TxtHist_Vi_Entrecalle2.Text = DtsVisitas.Tables(0).Rows(0).Item("Hist_Vi_Entrecalle2")
                TxtHist_Vi_Referencia.Text = DtsVisitas.Tables(0).Rows(0).Item("Hist_Vi_Referencia")
                TxtHist_Vi_Colorf.Text = DtsVisitas.Tables(0).Rows(0).Item("Hist_Vi_Colorf")
                TxtHist_Vi_Colorp.Text = DtsVisitas.Tables(0).Rows(0).Item("Hist_Vi_Colorp")
                TxtHist_Vi_Colorf.BackColor = System.Drawing.ColorTranslator.FromHtml("#" & DtsVisitas.Tables(0).Rows(0).Item("Hist_Vi_Colorf"))
                TxtHist_Vi_Colorp.BackColor = System.Drawing.ColorTranslator.FromHtml("#" & DtsVisitas.Tables(0).Rows(0).Item("Hist_Vi_Colorp"))
                DdlHist_Vi_Tipodomicilio.SelectedValue = DtsVisitas.Tables(0).Rows(0).Item("Hist_Vi_Tipodomicilio")
                DdlHist_Vi_Caracteristicas.SelectedValue = DtsVisitas.Tables(0).Rows(0).Item("Hist_Vi_Caracteristicas")
                TxtHist_Vi_Niveles.Text = DtsVisitas.Tables(0).Rows(0).Item("Hist_Vi_Niveles")
                DdlHist_Vi_Nivelsocio.SelectedValue = DtsVisitas.Tables(0).Rows(0).Item("Hist_Vi_Nivelsocio")
            End If
        End If
    End Sub
    Protected Sub GvBusquedas_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GvBusquedas.SelectedIndexChanged
        Try
            Dim USR As String = (CType(Session("Usuario"), USUARIO)).CAT_LO_USUARIO
        Catch ex As Exception
            Session.Clear()
            Session.Abandon()
            Response.Redirect("ExpiroSesion.aspx")
        End Try
        Try
            Dim row As GridViewRow = GvBusquedas.SelectedRow
            Dim Tocada As String = Class_CapturaVisitas.VariosQ(row.Cells(1).Text, LblCat_Lo_Usuario.Text, 1)
            LlenarCredito.Busca(row.Cells(1).Text)
            Dim Tmpcrdt As Credito = (CType(Session("Credito"), Credito))
            Tmpcrdt.PR_MC_CUENTATRABAJADAFILA = 1
            Response.Redirect("CapturaVisitas.aspx", False)
        Catch ax As Exception
            SendMail("GvBusquedas_SelectedIndexChanged", ax, CType(Session("Credito"), Credito).PR_MC_CREDITO, "", CType(Session("Usuario"), USUARIO).CAT_LO_USUARIO)
        End Try
    End Sub
    Protected Sub SESION_Click(sender As Object, e As EventArgs) Handles SESION.Click

        ValidaLicencias(CType(Session("USUARIO"), USUARIO).Licencias, LblCat_Lo_Usuario.Text, "", DateTime.Now, 4, CType(Session("IdSession"), String))
        Session.Clear()
        Session.Abandon()
        Response.Redirect("Login.aspx")
    End Sub
    Private Sub SendMail(ByRef evento As String, ByVal ex As Exception, ByVal Cuenta As String, ByVal Captura As String, ByVal usr As String)
        EnviarCorreo("CapturaVisitas.aspx", evento, ex, Cuenta, Captura, usr)
    End Sub
    Protected Sub BtnBuscar_Click(sender As Object, e As EventArgs) Handles BtnBuscar.Click
        Dim DtsBusca As DataSet = Busquedas.Search("'%" & TxtBuscar.Text.ToUpper & "%'")
        Dim DtvBusca As DataView = DtsBusca.Tables("Busca").DefaultView
        If DtvBusca.Count = 0 Then
            LblMsj.Text = "No Existen Resultados Para Su Busqueda"
            MpuMensajes.Show()
        ElseIf DtvBusca.Count = 1 Then
            Dim Tmpcrdt As Credito = (CType(Session("Credito"), Credito))
            Dim Tocada As String = Class_CapturaVisitas.VariosQ(DtsBusca.Tables(0).Rows(0).Item("Credito"), LblCat_Lo_Usuario.Text, 1)
            LlenarCredito.Busca(DtsBusca.Tables(0).Rows(0).Item("Credito"))
            Tmpcrdt.PR_MC_CUENTATRABAJADAFILA = 1
            Response.Redirect("CapturaVisitas.aspx", False)
        Else
            GvBusquedas.DataSource = DtsBusca
            GvBusquedas.DataBind()
            LblTitulo.Text = "Se Han Encontrado " & DtvBusca.Count & " Resultados Seleccione Uno."
            MpuAcciones.Show()
        End If
    End Sub
    Sub ValidaConfiguracion()
        LblHist_Ge_NoPago.Visible = False
        DdlHist_Ge_NoPago.Visible = False
        Dim Aplicacion As Aplicacion = CType(Session("Aplicacion"), Aplicacion)
        Dim TmpCrdt As Credito = CType(Session("Credito"), Credito)
        Dim TmpUsr As USUARIO = CType(Session("USUARIO"), USUARIO)
        If Aplicacion.ACCION = 1 Then
            DdlHist_Ge_Accion.Visible = True
            LblHist_Ge_Accion.Visible = True
            Class_CapturaVisitas.LlenarElementosCodigos(DdlHist_Ge_Accion, "", TmpCrdt.PR_MC_PRODUCTO, TmpUsr.CAT_LO_PERFIL, "2,3", 1)
            DdlHist_Ge_Resultado.Visible = False
            LblHist_Ge_Resultado.Visible = False
        Else
            Class_CapturaVisitas.LlenarElementosCodigos(DdlHist_Ge_Resultado, "", TmpCrdt.PR_MC_PRODUCTO, TmpUsr.CAT_LO_PERFIL, "2,3", 2)
        End If
    End Sub
    Protected Sub DdlAccion_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlHist_Ge_Accion.SelectedIndexChanged
        DdlHist_Ge_NoPago.Visible = False
        LblHist_Ge_NoPago.Visible = False
        BtnGuardar.Visible = False
        If DdlHist_Ge_Accion.SelectedValue <> "Seleccione" Then
            Dim TmpCrdt As Credito = CType(Session("Credito"), Credito)
            Dim TmpUsr As USUARIO = CType(Session("USUARIO"), USUARIO)
            Class_CapturaVisitas.LlenarElementosCodigos(DdlHist_Ge_Resultado, DdlHist_Ge_Accion.SelectedValue, TmpCrdt.PR_MC_PRODUCTO, TmpUsr.CAT_LO_PERFIL, "2,3", 3)
            DdlHist_Ge_Resultado.Visible = True
            LblHist_Ge_Resultado.Visible = True
        Else
            DdlHist_Ge_Resultado.Visible = False
            LblHist_Ge_Resultado.Visible = False
        End If
    End Sub
    Protected Sub DdlResultado_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlHist_Ge_Resultado.SelectedIndexChanged
        Dim Aplicacion As Aplicacion = CType(Session("Aplicacion"), Aplicacion)
        Dim TmpCrdt As Credito = CType(Session("Credito"), Credito)
        If DdlHist_Ge_Resultado.SelectedValue <> "Seleccione" Then
            If Aplicacion.NOPAGO = 1 Then
                Dim DtsNopago As DataSet = Class_CapturaVisitas.LlenarElementosCodigos(DdlHist_Ge_NoPago, DdlHist_Ge_Accion.SelectedValue & "," & DdlHist_Ge_Resultado.SelectedValue.Split(",")(0), TmpCrdt.PR_MC_PRODUCTO, "", "2,3", 4)
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
                LblHist_Pr_Dtepromesa.Visible = True
                TxtHist_Pr_Dtepromesa.Visible = True
            Else
                LblHist_Pr_Montopp.Visible = False
                TxtHist_Pr_Montopp.Visible = False
                LblHist_Pr_Dtepromesa.Visible = False
                TxtHist_Pr_Dtepromesa.Visible = False
            End If
        Else
            If Aplicacion.NOPAGO = 1 Then
                DdlHist_Ge_NoPago.Visible = False
                LblHist_Ge_NoPago.Visible = False
            End If
            BtnGuardar.Visible = False
        End If
    End Sub
    Protected Sub DdlHist_vi_Parentesco_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlHist_Vi_Parentesco.SelectedIndexChanged
        If (DdlHist_Vi_Parentesco.SelectedValue <> "Cliente" And DdlHist_Vi_Parentesco.SelectedValue <> "Ninguno") Then
            LblHist_Vi_Nombrec.Visible = True
            TxtHist_Vi_Nombrec.Visible = True
        Else
            LblHist_Vi_Nombrec.Visible = False
            TxtHist_Vi_Nombrec.Visible = False
            TxtHist_Vi_Nombrec.Text = ""
        End If
    End Sub

    Protected Sub BtnGuardar_Click(sender As Object, e As EventArgs) Handles BtnGuardar.Click
        Dim TmpUsr As USUARIO = CType(Session("USUARIO"), USUARIO)
        Dim TmpCrdt As Credito = CType(Session("Credito"), Credito)
        Dim TmpAplcc As Aplicacion = CType(Session("Aplicacion"), Aplicacion)
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
                    Dim Resultado As String = Class_CapturaVisitas.GuardarVisita(TmpCrdt.PR_MC_CREDITO, TmpCrdt.PR_MC_PRODUCTO, DdlHist_Vi_Visitador.SelectedValue, TmpUsr.CAT_LO_USUARIO, TxtHist_Vi_Dtevisita.Text & " " & TxtHist_Vi_Dtevisita2.Text, DdlHist_Ge_Accion.SelectedValue, DdlHist_Ge_Resultado.SelectedValue, DdlHist_Ge_Resultado.SelectedItem.ToString, DdlHist_Ge_NoPago.SelectedValue, TxtHist_Vi_Comentario.Text, TxtHist_Vi_Nombrec.Text, DdlHist_Vi_Parentesco.SelectedValue, DdlHist_Vi_Tipodomicilio.SelectedValue, DdlHist_Vi_Nivelsocio.SelectedValue, TxtHist_Vi_Niveles.Text, DdlHist_Vi_Caracteristicas.SelectedValue, TxtHist_Vi_Colorf.Text, TxtHist_Vi_Colorp.Text, TxtHist_Vi_Hcontacto.Text & TxtHist_Vi_Hcontacto0.Text, GvDiasVisita, TxtHist_Vi_Referencia.Text, "Cliente", TmpCrdt.PR_MC_AGENCIA, TxtHist_Vi_Entrecalle1.Text, TxtHist_Vi_Entrecalle2.Text, TmpCrdt.PR_MC_CODIGOVISITA, TmpAplcc.ACCION, TmpAplcc.NOPAGO, 1, TxtHist_Pr_Montopp.Text, TxtHist_Pr_Dtepromesa.Text, "", "", TxtHIST_VI_FOLIO.Text)
                    LblMsj.Text = Resultado
                    If Resultado = "Visita Capturada" Then
                        MpuMensajes.Show()
                        Limpiar()
                    Else
                        MpuMensajes.Show()
                    End If
                Else
                    LblPromesa.Text = "Promesa Vigente Para " & Promesa.Split(",")(1) & " Por " & to_money(Promesa.Split(",")(0))
                    MpuPromesa.Show()
                End If
            End If
        Else
            Dim Resultado As String = Class_CapturaVisitas.GuardarVisita(TmpCrdt.PR_MC_CREDITO, TmpCrdt.PR_MC_PRODUCTO, DdlHist_Vi_Visitador.SelectedValue, TmpUsr.CAT_LO_USUARIO, TxtHist_Vi_Dtevisita.Text & " " & TxtHist_Vi_Dtevisita2.Text, DdlHist_Ge_Accion.SelectedValue, DdlHist_Ge_Resultado.SelectedValue, DdlHist_Ge_Resultado.SelectedItem.ToString, DdlHist_Ge_NoPago.SelectedValue, TxtHist_Vi_Comentario.Text, TxtHist_Vi_Nombrec.Text, DdlHist_Vi_Parentesco.SelectedValue, DdlHist_Vi_Tipodomicilio.SelectedValue, DdlHist_Vi_Nivelsocio.SelectedValue, TxtHist_Vi_Niveles.Text, DdlHist_Vi_Caracteristicas.SelectedValue, TxtHist_Vi_Colorf.Text, TxtHist_Vi_Colorp.Text, TxtHist_Vi_Hcontacto.Text & TxtHist_Vi_Hcontacto0.Text, GvDiasVisita, TxtHist_Vi_Referencia.Text, "Cliente", TmpCrdt.PR_MC_AGENCIA, TxtHist_Vi_Entrecalle1.Text, TxtHist_Vi_Entrecalle2.Text, TmpCrdt.PR_MC_CODIGOVISITA, TmpAplcc.ACCION, TmpAplcc.NOPAGO, 2, "", "", "", "", TxtHIST_VI_FOLIO.Text)
            LblMsj.Text = Resultado
            If Resultado = "Visita Capturada" Then
                MpuMensajes.Show()
                Limpiar()
            Else
                MpuMensajes.Show()
            End If
        End If
    End Sub
    Protected Sub BtnAceptarPromesa_Click(sender As Object, e As EventArgs) Handles BtnAceptarPromesa.Click
        Dim TmpCrdt As Credito = CType(Session("Credito"), Credito)
        Dim TmpUsr As USUARIO = CType(Session("USUARIO"), USUARIO)
        Dim TmpAplcc As Aplicacion = CType(Session("Aplicacion"), Aplicacion)
        If TmpUsr.CAT_LO_PGESTION.ToString.Substring(4, 1) = 1 Then
            If TxtHist_Pr_Motivo.Text.Length < 10 Then
                LblMsjPromesa.Text = "Capture Un Comentario Valido"
                MpuPromesa.Show()
            Else
                Dim Resultado As String = Class_CapturaVisitas.GuardarVisita(TmpCrdt.PR_MC_CREDITO, TmpCrdt.PR_MC_PRODUCTO, DdlHist_Vi_Visitador.SelectedValue, TmpUsr.CAT_LO_USUARIO, TxtHist_Vi_Dtevisita.Text & " " & TxtHist_Vi_Dtevisita2.Text, DdlHist_Ge_Accion.SelectedValue, DdlHist_Ge_Resultado.SelectedValue, DdlHist_Ge_Resultado.SelectedItem.ToString, DdlHist_Ge_NoPago.SelectedValue, TxtHist_Vi_Comentario.Text, TxtHist_Vi_Nombrec.Text, DdlHist_Vi_Parentesco.SelectedValue, DdlHist_Vi_Tipodomicilio.SelectedValue, DdlHist_Vi_Nivelsocio.SelectedValue, TxtHist_Vi_Niveles.Text, DdlHist_Vi_Caracteristicas.SelectedValue, TxtHist_Vi_Colorf.Text, TxtHist_Vi_Colorp.Text, TxtHist_Vi_Hcontacto.Text & TxtHist_Vi_Hcontacto0.Text, GvDiasVisita, TxtHist_Vi_Referencia.Text, "Cliente", TmpCrdt.PR_MC_AGENCIA, TxtHist_Vi_Entrecalle1.Text, TxtHist_Vi_Entrecalle2.Text, TmpCrdt.PR_MC_CODIGOVISITA, TmpAplcc.ACCION, TmpAplcc.NOPAGO, 3, TxtHist_Pr_Montopp.Text, TxtHist_Pr_Dtepromesa.Text, TxtHist_Pr_Motivo.Text, TxtHist_Pr_Supervisor.Text, TxtHIST_VI_FOLIO.Text)
                If Resultado = "Visita Capturada" Then
                    LblMsj.Text = Resultado
                    MpuMensajes.Show()
                    Limpiar()
                Else
                    LblMsj.Text = Resultado
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
                Dim Resultado As String = Class_CapturaVisitas.GuardarVisita(TmpCrdt.PR_MC_CREDITO, TmpCrdt.PR_MC_PRODUCTO, DdlHist_Vi_Visitador.SelectedValue, TmpUsr.CAT_LO_USUARIO, TxtHist_Vi_Dtevisita.Text & " " & TxtHist_Vi_Dtevisita2.Text, DdlHist_Ge_Accion.SelectedValue, DdlHist_Ge_Resultado.SelectedValue, DdlHist_Ge_Resultado.SelectedItem.ToString, DdlHist_Ge_NoPago.SelectedValue, TxtHist_Vi_Comentario.Text, TxtHist_Vi_Nombrec.Text, DdlHist_Vi_Parentesco.SelectedValue, DdlHist_Vi_Tipodomicilio.SelectedValue, DdlHist_Vi_Nivelsocio.SelectedValue, TxtHist_Vi_Niveles.Text, DdlHist_Vi_Caracteristicas.SelectedValue, TxtHist_Vi_Colorf.Text, TxtHist_Vi_Colorp.Text, TxtHist_Vi_Hcontacto.Text & TxtHist_Vi_Hcontacto0.Text, GvDiasVisita, TxtHist_Vi_Referencia.Text, "Cliente", TmpCrdt.PR_MC_AGENCIA, TxtHist_Vi_Entrecalle1.Text, TxtHist_Vi_Entrecalle2.Text, TmpCrdt.PR_MC_CODIGOVISITA, TmpAplcc.ACCION, TmpAplcc.NOPAGO, 3, TxtHist_Pr_Montopp.Text, TxtHist_Pr_Dtepromesa.Text, TxtHist_Pr_Motivo.Text, TxtHist_Pr_Supervisor.Text, TxtHIST_VI_FOLIO.Text)
                If Resultado = "Visita Capturada" Then
                    LblMsj.Text = Resultado
                    MpuMensajes.Show()
                    Limpiar()
                Else
                    LblMsj.Text = Resultado
                    MpuMensajes.Show()
                End If
            End If
        End If
    End Sub
    Sub Limpiar()
        ValidaConfiguracion()
        TxtHist_Vi_Dtevisita.Text = ""
        TxtHist_Vi_Dtevisita2.Text = ""
        TxtHist_Vi_Nombrec.Text = ""
        TxtHist_Vi_Comentario.Text = ""
        DdlHist_Vi_Parentesco.SelectedValue = "Cliente"
        LblHist_Vi_Nombrec.Visible = False
        TxtHist_Vi_Nombrec.Visible = False
        TxtHist_Vi_Nombrec.Text = ""
        DdlHist_Vi_Tipodomicilio.SelectedIndex = 0
        DdlHist_Vi_Nivelsocio.SelectedIndex = 0
        DdlHist_Vi_Caracteristicas.SelectedIndex = 0
        TxtHist_Vi_Colorf.Text = "FFFFFF"
        TxtHist_Vi_Colorp.Text = "FFFFFF"
        TxtHist_Vi_Colorf.BackColor = Drawing.Color.White
        TxtHist_Vi_Colorp.BackColor = Drawing.Color.White
        TxtHist_Vi_Hcontacto.Text = ""
        TxtHist_Vi_Hcontacto0.Text = ""
        TxtHist_Vi_Entrecalle1.Text = ""
        TxtHist_Vi_Entrecalle2.Text = ""
        DdlHist_Vi_Tipodomicilio.SelectedValue = " "
        TxtHist_Vi_Niveles.Text = " "
        DdlHist_Vi_Nivelsocio.SelectedValue = " "
        DdlHist_Vi_Caracteristicas.SelectedValue = " "
        GvDiasVisita.DataSource = Class_CapturaVisitas.LlenarElementosVisitas(CType(Session("Credito"), Credito).PR_MC_CREDITO, 0)
        GvDiasVisita.DataBind()
        TxtHist_Vi_Referencia.Text = ""
        TxtHist_Pr_Montopp.Text = ""
        TxtHist_Pr_Montopp.Visible = False
        LblHist_Pr_Montopp.Visible = False
        TxtHist_Pr_Dtepromesa.Text = ""
        TxtHist_Pr_Dtepromesa.Visible = False
        LblHist_Pr_Dtepromesa.Visible = False
        LblMsjPromesa.Text = ""
        TxtHist_Pr_Supervisor.Text = ""
        TxtHIST_VI_FOLIO.Text = ""
    End Sub

    Protected Sub ImgAddGestion_Click(sender As Object, e As ImageClickEventArgs) Handles ImgAddGestion.Click
        Session("Buscar") = 1
        Response.Redirect("MasterPage.aspx")
    End Sub

    Protected Sub DdlHist_Te_Parentesco_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlHist_Te_Parentesco.SelectedIndexChanged
        If DdlHist_Te_Parentesco.SelectedValue <> "Cliente" Then
            LblHist_Te_Nombre.Visible = True
            TxtHist_Te_Nombre.Visible = True
        Else
            LblHist_Te_Nombre.Visible = False
            TxtHist_Te_Nombre.Visible = False
            TxtHist_Te_Nombre.Text = ""
        End If
    End Sub
    Protected Sub DdlHist_Te_Tipo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlHist_Te_Tipo.SelectedIndexChanged
        If DdlHist_Te_Tipo.SelectedValue <> "Oficina" Then
            LblHist_Te_Extension.Visible = False
            TxtHist_Te_Extension.Visible = False
            TxtHist_Te_Extension.Text = ""
        Else
            LblHist_Te_Extension.Visible = True
            TxtHist_Te_Extension.Visible = True
        End If
    End Sub

    Protected Sub BtnAddTel_Click(sender As Object, e As EventArgs) Handles BtnAddTel.Click
        Dim TmpCredito As Credito = CType(Session("Credito"), Credito)
        Dim TmpUsuario As USUARIO = CType(Session("USUARIO"), USUARIO)
        LblMsj.Text = Class_InformacionAdicional.AgregarTelefono(0, TmpCredito.PR_MC_CREDITO, TmpCredito.PR_MC_PRODUCTO, TxtHist_Te_Lada.Text, TxtHist_Te_Numerotel.Text, DdlHist_Te_Tipo.SelectedValue, DdlHist_Te_Parentesco.SelectedValue, TxtHist_Te_Nombre.Text, TxtHist_Te_Extension.Text, TxtHist_Te_Horario0.Text, TxtHist_Te_Horario1.Text, TmpUsuario.CAT_LO_USUARIO, TmpCredito.PR_MC_AGENCIA, "Captura", BtnAddTel, GvDiasTel, TxtHist_Co_Proporciona.Text)
        If LblMsj.Text = "1" Then
            LblMsj.Text = "Teléfono Agregado"
            MpuMensajes.Show()
            Limpiar(0)
        ElseIf LblMsj.Text = "0" Then
            LblMsj.Text = "Teléfono Actualizado"
            MpuMensajes.Show()
            Limpiar(0)
        Else
            MpuMensajes.Show()
        End If
    End Sub

    Protected Sub BtnCancelAddTel_Click(sender As Object, e As EventArgs) Handles BtnCancelAddTel.Click
        Limpiar(0)
    End Sub
    Sub Limpiar(ByVal V_Bandera As Integer)
        If V_Bandera = 0 Then
            DdlHist_Te_Tipo.SelectedValue = "Casa"
            TxtHist_Te_Lada.Text = ""
            TxtHist_Te_Numerotel.Text = ""
            TxtHist_Te_Nombre.Text = ""
            TxtHist_Te_Nombre.Visible = False
            LblHist_Te_Nombre.Visible = False
            TxtHist_Te_Extension.Text = ""
            TxtHist_Te_Extension.Visible = False
            LblHist_Te_Extension.Visible = False
            DdlHist_Te_Parentesco.SelectedValue = "Cliente"
        End If
    End Sub
End Class
