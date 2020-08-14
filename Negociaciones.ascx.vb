Imports System.Data.OracleClient
Imports System.Data
Imports System.Web.UI.WebControls
Imports iTextSharp
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports Db
Imports Funciones
Imports System.Web.Services
Imports System.Globalization
Imports Busquedas
Imports Class_Hist_Act
Imports System.Web.Script.Serialization
Imports System.IO
Imports System.Net.Mail
Imports System.Net
Imports System.Net.Security
Partial Class Negociaciones
    Inherits System.Web.UI.UserControl
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                If CType(Session("Credito"), credito).PR_MC_CREDITO <> "" Then
                    Llenar()
                End If
            End If
        Catch ex As Exception
            SendMail("Page_Load", ex, "", "", "")
        End Try
    End Sub

    Sub Llenar()
        Try
            Dim TmpCredito As credito = CType(Session("Credito"), credito)
            Dim TmpUsr As USUARIO = CType(Session("USUARIO"), USUARIO)
            If TmpCredito.PR_MC_ESTATUS <> "Retirada" Then
                PnlEstatus.Visible = True
                Dim Promesa As String = Class_CapturaVisitas.VariosQ(TmpCredito.PR_MC_CREDITO, TmpUsr.CAT_LO_USUARIO, 7)
                If Promesa = "0,0" Then


                Else
                    LblPromesa.Text = "Promesa Vigente Para " & Promesa.Split(",")(1) & " Por " & to_money(Promesa.Split(",")(0))
                    GvCalendarioVig.DataSource = Class_Negociaciones.LlenarElementosNego(TmpCredito.PR_MC_CREDITO, "", "", 6)
                    GvCalendarioVig.DataBind()
                    PnlNeGociacion.Visible = False
                    PnlNegoVigente.Visible = True
                End If

            End If
        Catch ex As Exception
            DdlCat_Ne_Nombre.Items.Clear()
            DdlCat_Ne_Nombre.Items.Add("No Aplica")
            DdlCat_Ne_Nombre.DataBind()
            PnlNeGociacion.Visible = True
            PnlNegoVigente.Visible = False
        End Try
    End Sub
    Protected Sub DdlResultado_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlHist_Ge_Resultado.SelectedIndexChanged
        Dim Aplicacion As Aplicacion = CType(Session("Aplicacion"), Aplicacion)
        Dim TmpCrdt As credito = CType(Session("Credito"), credito)
        Dim TmpUsr As USUARIO = CType(Session("USUARIO"), USUARIO)

        If DdlHist_Ge_Resultado.SelectedValue <> "Seleccione" Then
            If Aplicacion.NOPAGO = 1 Then
                Dim DtsNopago As DataSet = Class_MasterPage.LlenarElementosMaster(DdlHist_Ge_NoPago, "" & "," & DdlHist_Ge_Resultado.SelectedValue.Split(",")(0), TmpCrdt.PR_MC_PRODUCTO, TmpUsr.CAT_LO_PERFIL, "3", 4)
                'Dim DtvNopago As DataView = Class_MasterPage.LlenarElementosMaster(DdlHist_Ge_NoPago, "" & "," & DdlHist_Ge_Resultado.SelectedValue.Split(",")(0), TmpCrdt.PR_MC_PRODUCTO, TmpUsr.CAT_LO_PERFIL, "3", 4)

                DdlHist_Ge_NoPago.Items.Clear()

                Dim v_count As DataView = DtsNopago.Tables("ELEMENTOS").DefaultView

                'If Not IsDBNull(DtsNopago.Tables("ELEMENTOS").Rows(0).Item("Descripcion")) Then
                If v_count.Count > 0 Then
                    Dim ArrayGeneral() As String = Strings.Split(DtsNopago.Tables("ELEMENTOS").Rows(0).Item("Descripcion"), ",")
                    For i As Integer = 0 To ArrayGeneral.Count() - 1
                        DdlHist_Ge_NoPago.Items.Add(New System.Web.UI.WebControls.ListItem(ArrayGeneral(i).ToString().Split("|")(0), ArrayGeneral(i).ToString().Split("|")(1), True))
                    Next
                    DdlHist_Ge_NoPago.DataBind()
                    DdlHist_Ge_NoPago.Visible = True
                    LblHist_Ge_NoPago.Visible = True
                Else
                    DdlHist_Ge_NoPago.Visible = False
                    LblHist_Ge_NoPago.Visible = False
                End If
                'End If
            Else
                If Aplicacion.NOPAGO = 1 Then
                    DdlHist_Ge_NoPago.Visible = False
                    LblHist_Ge_NoPago.Visible = False
                End If
                BtnGuardar.Visible = False
            End If
        End If
    End Sub
    Private Sub SendMail(ByRef evento As String, ByVal ex As Exception, ByVal Cuenta As String, ByVal Captura As String, ByVal usr As String)
        EnviarCorreo("Negociaciones.aspx", evento, ex, Cuenta, Captura, usr)
    End Sub
    Protected Sub DdlCat_Ne_Nombre_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlCat_Ne_Nombre.SelectedIndexChanged
        Dim TmpCredito As credito = CType(Session("Credito"), credito)
        Dim TmpUsr As USUARIO = CType(Session("USUARIO"), USUARIO)

        If DdlCat_Ne_Nombre.SelectedValue <> "Seleccione" Then
            Dim Promesa As String = Class_CapturaVisitas.VariosQ(TmpCredito.PR_MC_CREDITO, TmpUsr.CAT_LO_USUARIO, 7)
            If Promesa = "0,0" Then

                PnlDetalle.Visible = True
                PnlConfiguracion.Visible = True
                Class_MasterPage.LlenarElementosMaster(DdlHist_Ge_Resultado, "PP", TmpCredito.PR_MC_PRODUCTO, TmpUsr.CAT_LO_PERFIL, "1,3", 6)
                DdlHist_Ge_Resultado.Visible = True
                LblHist_Ge_Resultado.Visible = True
                GvDiasTel.DataSource = Class_InformacionAdicional.LlenarElementosAgregar(CType(Session("Credito"), credito).PR_MC_CREDITO, 0)
                GvDiasTel.DataBind()
                PnlNeGociacion.Visible = True
                PnlNegoVigente.Visible = False

                TxtPR_CF_SALDO_TOT.Text = to_money(TmpCredito.PR_CF_SALDO_TOT)

                TxtPR_CF_BUCKET.Text = TmpCredito.PR_CF_BUCKET
                '--------------llenar drop de correos--------------------
                Dim dts As DataSet = Class_InformacionAdicional.LlenarElementosAgregar(CType(Session("Credito"), credito).PR_MC_CREDITO, 4)
                If dts.Tables(0).Rows.Count > 0 Then
                    DdlSeleccionaMail.DataTextField = "Correo"
                    DdlSeleccionaMail.DataValueField = "Correo"
                    DdlSeleccionaMail.DataSource = dts
                    DdlSeleccionaMail.DataBind()
                End If


                If DdlCat_Ne_Nombre.SelectedValue = "CAT_NEGO_QUITAS" Then



                    LblCat_Ne_Num_Pagos.Text = "Numero de Pagos"
                    TxtSaldoNegociado.Enabled = True
                    LblNumPagos.Visible = False
                    TxtNumPagos.Visible = False
                    LblDescuentoMax.Visible = True
                    LblExhibiciones.Visible = True
                    DdlExhibiciones.Visible = True
                    TxtDescuento.Visible = True
                    LblDescuento.Visible = True
                    If Val(TmpCredito.PR_CF_BUCKET.Replace("+", "").Replace("BK", "")) >= 3 And Val(TmpCredito.PR_CF_BUCKET.Replace("+", "").Replace("BK", "")) <= 6 Then
                        TxtCartera.Text = "Extrajudicial"
                        MostrarNegoQuitas()
                    ElseIf Val(TmpCredito.PR_CF_BUCKET.Replace("+", "").Replace("BK", "")) >= 7 Then
                        TxtCartera.Text = "Cartera Castigo"
                        MostrarNegoQuitas()
                    Else ' para bucket 0,1 y 2
                        LblMsj.Text = "No Aplica Para Bucket  0,1 y 2"
                        MpuMensajes.Show()
                    End If



                ElseIf DdlCat_Ne_Nombre.SelectedValue = "CAT_NEGO_PAGOS_FIJOS" Then
                    GvOpcionesNegociacion.DataSource = Nothing
                    GvOpcionesNegociacion.DataBind()

                    LblTip.Visible = False
                    LblDescuentoMax.Visible = False
                    LblDteLimiteNegociacion.Visible = True
                    LblDteOriginacionContrato.Visible = True
                    TxtDteLimiteNegociacion.Visible = True
                    TxtDteLimiteNegociacion.Text = TmpCredito.PR_CF_DTE_LIM_PAGO
                    TxtDteOriginacionContrato.Visible = True
                    TxtDteOriginacionContrato.Text = TmpCredito.PR_CF_FIRMA_CONTRATO
                    TxtCartera.Text = "Castigada"
                    TxtNumPagos.ReadOnly = True
                    LblCat_Ne_Num_Pagos.Text = " "
                    TxtSaldoNegociado.Text = TmpCredito.PR_CF_SALDO_TOT
                    TxtSaldoNegociado.Enabled = False
                    LblNumPagos.Visible = True
                    TxtNumPagos.Visible = True
                    LblExhibiciones.Visible = False
                    DdlExhibiciones.Visible = False
                    DdlExhibiciones.Items.Clear()
                    DdlExhibiciones.DataBind()
                    DdlExhibiciones.Visible = False
                    LblExhibiciones.Visible = False
                    TxtDescuento.Visible = False
                    LblDescuento.Visible = False
                End If



            Else
                LblPromesa.Text = "Promesa Vigente Para " & Promesa.Split(",")(1) & " Por " & to_money(Promesa.Split(",")(0))
                GvCalendarioVig.DataSource = Class_Negociaciones.LlenarElementosNego(TmpCredito.PR_MC_CREDITO, "", "", 6)
                GvCalendarioVig.DataBind()
                PnlNeGociacion.Visible = False
            End If
        Else
            PnlConfiguracion.Visible = False
            PnlDetalle.Visible = False
            Bloquear_Desbloquear(0)
        End If
    End Sub

    Sub MostrarNegoQuitas()
        Dim TmpCredito As credito = CType(Session("Credito"), credito)
        GvOpcionesNegociacion.DataSource = Class_Negociaciones.LlenarElementosNego_Fijos(DdlCat_Ne_Nombre.SelectedValue, TxtCartera.Text, TmpCredito.PR_MC_CREDITO, "", "", "", "", 0)
        GvOpcionesNegociacion.DataBind()

        LblTip.Visible = False
        LblDteLimiteNegociacion.Visible = False
        LblDteOriginacionContrato.Visible = False
        TxtDteLimiteNegociacion.Visible = False
        TxtDteOriginacionContrato.Visible = False

        DdlExhibiciones.Items.Clear()
        DdlExhibiciones.Items.Add("Seleccione")
        DdlExhibiciones.SelectedValue = "Seleccione"
        DdlExhibiciones.Items.Add("Quita en 1 exhibición")
        DdlExhibiciones.Items.Add("Quita + plazo(3 meses)")
        DdlExhibiciones.DataBind()

    End Sub
    Sub pagos(ByVal V_Num_Pagos As Integer)
        DdlCat_Ne_Num_Pagos.Items.Clear()
        DdlCat_Ne_Num_Pagos.Items.Add("Seleccione")
        DdlCat_Ne_Num_Pagos.SelectedValue = "Seleccione"
        For x As Integer = 1 To V_Num_Pagos
            DdlCat_Ne_Num_Pagos.Items.Add(New System.Web.UI.WebControls.ListItem(x))
        Next
        DdlCat_Ne_Num_Pagos.DataBind()
    End Sub

    Sub Montos_Minimos()
        'Dim TmpCredito As Credito = CType(Session("Credito"), Credito)
        ''LblMinSaldoNego.Text = Math.Round((TmpCredito.PR_CF_IMPT_DEUDA_ACT) - (TmpCredito.PR_CF_IMPT_DEUDA_ACT * (Val(LblCat_Ne_Desc_Max.Text) / 100)), 2)
        'If LblCat_Ne_Desc_Max.Text = "0%" Then
        '    '  LblMinSaldoInic.Text = TxtSaldoNegociado.Text
        'Else
        '    '   LblMinSaldoInic.Text = Math.Round(((TmpCredito.PR_CF_IMPT_DEUDA_ACT) - (TmpCredito.PR_CF_IMPT_DEUDA_ACT * (Val(LblCat_Ne_Desc_Max.Text) / 100))) * (Val(LblCat_Ne_Porciento.Text.Replace("%", "")) / 100), 2)
        'End If
    End Sub
    Protected Sub DdlCat_Ne_Num_Pagos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlCat_Ne_Num_Pagos.SelectedIndexChanged
        If DdlCat_Ne_Num_Pagos.SelectedValue <> "Seleccione" Then
            'CalcularSaldos(0)
            If DdlCat_Ne_Num_Pagos.SelectedValue = "1" Then
                TxtPagoInicial.Text = Math.Round(Val(TxtSaldoNegociado.Text), 2)
            Else
            End If
        Else
            'CalcularSaldos(1)
        End If

    End Sub
    Sub CalcularSaldos(ByVal Flag As String)
        BtnGuardar.Visible = False
        'TxtDiasPago.Visible = False
        'LblDiasPago.Visible = False
        'TxtMontoPagos.Visible = False
        'LblMontoPagos.Visible = False
        BtnVisualizar.Visible = False
        BtnGuardar.Visible = False
        TxtContrasenaAuto.Visible = False
        LblContrasenaAuto.Visible = False
        TxtHist_Pr_SupervisorAuto.Visible = False
        LblHist_Pr_SupervisorAuto.Visible = False
        BtnCancelarVis.Visible = False
        'LblMontoReal.Visible = False
        'TxtMontoReal.Visible = False
        Dim TmpCredito As credito = CType(Session("Credito"), credito)
        If DdlCat_Ne_Num_Pagos.SelectedValue = "Seleccione" Then
            Limpiar(2)
        Else



            If Val(TxtSaldoNegociado.Text) > Val(TmpCredito.PR_CF_SALDO_TOT) Then
                LblMsj.Text = "El Saldo Negociado No Puede Ser Mayor A $" & TmpCredito.PR_CF_SALDO_TOT
                MpuMensajes.Show()
                Limpiar(2)
            Else

                TxtDescuento.Text = Math.Round(((TmpCredito.PR_CF_SALDO_TOT - TxtSaldoNegociado.Text) * 100) / TmpCredito.PR_CF_SALDO_TOT, 2)

                Dim IntBukcet As Integer = Val(TmpCredito.PR_CF_BUCKET.Replace("+", "").Replace("BK", ""))
                Dim StrRangoSaldo As String = ""
                Dim IntDescuento As Double = 0




                If DdlExhibiciones.SelectedValue <> "Seleccione" Then


                    If Val(TxtDescuento.Text) > Val(LblDescuentoMax.Text) Then
                        LblMsj.Text = "El Descuento Supera a " & IntDescuento & "Con El Monto De Esa Negociacion"
                        MpuMensajes.Show()
                        Limpiar(2)
                    End If





                End If


            End If


            'If Val(TxtDescuento.Text) < Val(LblCat_Ne_Desc_Min.Text) Or Val(TxtDescuento.Text) > Val(LblCat_Ne_Desc_Max.Text) Then
            '    LblMsj.Text = "El Porcentaje De Descuento No Puede Ser Mayor A " & LblCat_Ne_Desc_Max.Text & " Ni Menor A" & LblCat_Ne_Desc_Min.Text
            '    MpuMensajes.Show()
            '    DdlCat_Ne_Num_Pagos.SelectedValue = "Seleccione"
            '    Limpiar(2)
            'Else
            '    If LblCat_Ne_Pagounico.Text <> "Si" Then
            '        TxtDiasPago.Visible = True
            '        LblDiasPago.Visible = True
            '        TxtMontoPagos.Visible = True
            '        LblMontoPagos.Visible = True
            '        BtnVisualizar.Visible = True
            '        LblMontoReal.Visible = True
            '        TxtMontoReal.Visible = True

            '        If Flag = 0 Then
            '            TxtSaldoNegociado.Text = Math.Round((TmpCredito.PR_CF_IMPT_DEUDA_ACT) - (TmpCredito.PR_CF_IMPT_DEUDA_ACT * (Val(TxtDescuento.Text) / 100)), 2)
            '        Else
            '            TxtDescuento.Text = Math.Round(((TmpCredito.PR_CF_IMPT_DEUDA_ACT - TxtSaldoNegociado.Text) * 100) / TmpCredito.PR_CF_IMPT_DEUDA_ACT, 2)
            '        End If

            '        If LblCat_Ne_Porciento2.Text = "0%" Then
            '            TxtPagoInicial.Text = TxtSaldoNegociado.Text
            '        Else
            '            TxtPagoInicial.Text = Math.Round(((TmpCredito.PR_CF_IMPT_DEUDA_ACT) - (TmpCredito.PR_CF_IMPT_DEUDA_ACT * (Val(TxtDescuento.Text) / 100))) * (Val(LblCat_Ne_Porciento.Text.Replace("%", "")) / 100), 2)
            '        End If

            '        TxtMontoPagos.Text = Math.Round((Val(TxtSaldoNegociado.Text) - Val(TxtPagoInicial.Text)) / DdlCat_Ne_Num_Pagos.SelectedValue, 2)

            '        TxtSaldoNegociadoF.Text = TmpCredito.PR_CF_IMPT_DEUDA_ACT - Val(TxtSaldoNegociado.Text.Replace("$", "").Replace(",", ""))
            '        TxtDescuentoF.Text = Math.Round(Val(TxtSaldoNegociadoF.Text.Replace("$", "").Replace(",", "") * 100) / Val(TmpCredito.PR_CF_IMPT_DEUDA_ACT), 2) & "%"
            '    Else

            '        If Flag = 0 Then
            '            TxtSaldoNegociado.Text = Math.Round((TmpCredito.PR_CF_IMPT_DEUDA_ACT) - (TmpCredito.PR_CF_IMPT_DEUDA_ACT * (Val(TxtDescuento.Text) / 100)), 2)
            '        Else
            '            TxtDescuento.Text = Math.Round(((TmpCredito.PR_CF_IMPT_DEUDA_ACT - TxtSaldoNegociado.Text) * 100) / TmpCredito.PR_CF_IMPT_DEUDA_ACT, 2)
            '        End If

            '        If LblCat_Ne_Porciento.Text = "0%" Then
            '            TxtPagoInicial.Text = TxtSaldoNegociado.Text
            '        Else
            '            TxtPagoInicial.Text = Math.Round(TmpCredito.PR_CF_IMPT_DEUDA_ACT * (Val(LblCat_Ne_Porciento.Text.Replace("%", "")) / 100), 2)
            '        End If

            '        TxtSaldoNegociadoF.Text = TmpCredito.PR_CF_IMPT_DEUDA_ACT - Val(TxtSaldoNegociado.Text.Replace("$", "").Replace(",", ""))
            '        TxtDescuentoF.Text = Math.Round(Val(TxtSaldoNegociadoF.Text.Replace("$", "").Replace(",", "") * 100) / Val(TmpCredito.PR_CF_IMPT_DEUDA_ACT), 2) & "%"
            '        BtnGuardar.Visible = True
            '        TxtContrasenaAuto.Visible = True
            '        LblContrasenaAuto.Visible = True
            '        TxtHist_Pr_SupervisorAuto.Visible = True
            '        LblHist_Pr_SupervisorAuto.Visible = True
            '    End If
            'End If
        End If
    End Sub
    Sub VisualizarSaldo()
        Dim TmpCredito As credito = CType(Session("Credito"), credito)
        'If LblCat_Ne_Pagounico.Text <> "Si" Then
        '    If LblCat_Ne_Porciento.Text = "0%" Then
        '        TxtPagoInicial.Text = TxtSaldoNegociado.Text
        '    Else
        '        TxtPagoInicial.Text = Math.Round(((TmpCredito.PR_CF_IMPT_DEUDA_ACT) - (TmpCredito.PR_CF_IMPT_DEUDA_ACT * (Val(TxtDescuento.Text) / 100))) * (Val(LblCat_Ne_Porciento.Text.Replace("%", "")) / 100), 2)
        '    End If
        '    TxtMontoPagos.Text = Math.Round((Val(TxtSaldoNegociado.Text) - Val(TxtPagoInicial.Text)) / DdlCat_Ne_Num_Pagos.SelectedValue, 2)

        '    TxtSaldoNegociadoF.Text = TmpCredito.PR_CF_IMPT_DEUDA_ACT - Val(TxtSaldoNegociado.Text.Replace("$", "").Replace(",", ""))
        '    TxtDescuentoF.Text = Math.Round(Val(TxtSaldoNegociadoF.Text.Replace("$", "").Replace(",", "") * 100) / Val(TmpCredito.PR_CF_IMPT_DEUDA_ACT), 2) & "%"
        'Else
        '    TxtSaldoNegociado.Text = Math.Round((TmpCredito.PR_CF_IMPT_DEUDA_ACT) - (TmpCredito.PR_CF_IMPT_DEUDA_ACT * (Val(TxtDescuento.Text) / 100)), 2)

        '    If LblCat_Ne_Porciento.Text = "0%" Then
        '        TxtPagoInicial.Text = TxtSaldoNegociado.Text
        '    Else
        '        TxtPagoInicial.Text = Math.Round(((TmpCredito.PR_CF_IMPT_DEUDA_ACT) - (TmpCredito.PR_CF_IMPT_DEUDA_ACT * (Val(TxtDescuento.Text) / 100))) * (Val(LblCat_Ne_Porciento.Text.Replace("%", "")) / 100), 2)
        '    End If
        '    TxtSaldoNegociadoF.Text = TmpCredito.PR_CF_IMPT_DEUDA_ACT - Val(TxtSaldoNegociado.Text.Replace("$", "").Replace(",", ""))
        '    TxtDescuentoF.Text = Math.Round(Val(TxtSaldoNegociadoF.Text.Replace("$", "").Replace(",", "") * 100) / Val(TmpCredito.PR_CF_IMPT_DEUDA_ACT), 2) & "%"
        '    BtnGuardar.Visible = True
        '    TxtContrasenaAuto.Visible = True
        '    LblContrasenaAuto.Visible = True
        '    TxtHist_Pr_SupervisorAuto.Visible = True
        '    LblHist_Pr_SupervisorAuto.Visible = True
        'End If
    End Sub
    Protected Sub BtnVisualizar_Click(sender As Object, e As EventArgs) Handles BtnVisualizar.Click
        Dim TmpCredito As credito = CType(Session("Credito"), credito)
        BtnGuardar.Visible = False
        TxtContrasenaAuto.Visible = False
        LblContrasenaAuto.Visible = False
        TxtHist_Pr_SupervisorAuto.Visible = False
        LblHist_Pr_SupervisorAuto.Visible = False
        If DdlCat_Ne_Nombre.SelectedValue = "CAT_NEGO_QUITAS" Then

            If TxtFechaPagoInicial.Text = "" Then
                LblMsj.Text = " Falta Fecha De Pago Inicial"
                MpuMensajes.Show()
            ElseIf TxtDescuento.Text = "" Then
                LblMsj.Text = "Falta Descuento"
                MpuMensajes.Show()
            ElseIf TxtSaldoNegociado.Text = "" Then
                LblMsj.Text = "Falta Saldo A Negociar"
                MpuMensajes.Show()
            ElseIf val(TxtPagoInicial.Text) > val(TxtSaldoNegociado.Text) Then
                LblMsj.Text = "El pago inical no puede ser mayor al saldo negociado"
                MpuMensajes.Show()
            Else
                Dim fecha_dia_pago As Date
                fecha_dia_pago = TxtFechaPagoInicial.Text
                If TxtPagoInicial.Text = "" Or TxtPagoInicial.Text = "0" Or TxtFechaPagoInicial.Text = "" Or DdlPeriodicidad.Text = "Seleccione" Or DdlCat_Ne_Num_Pagos.Text = "Seleccione" Then

                    LblMsj.Text = "Faltan Datos"
                    MpuMensajes.Show()
                ElseIf fecha_dia_pago < System.DateTime.Today Then
                    LblMsj.Text = "La Fecha de Pago No Puede Ser Menor que la Fecha Actual"
                    MpuMensajes.Show()
                    'ElseIf Math.Round(Val(TxtSaldoNegociado.Text) - (Val(TxtPagoInicial.Text) * Val(DdlCat_Ne_Num_Pagos.SelectedValue)), 2) <> Math.Round(Val(TxtSaldoNegociado.Text), 2) Then
                    '    LblMsj.Text = "La Suma De Los Pagos No Es Igual A El Total A Negociar"
                    '    MpuMensajes.Show()
                ElseIf DdlCat_Ne_Num_Pagos.SelectedValue = "1" And fecha_dia_pago > System.DateTime.Today.AddDays(45) Then
                    LblMsj.Text = "En Una Exhibicion, La Fecha del Primer Pago No Puede Ser Mayor A 45 Dias"
                    MpuMensajes.Show()
                ElseIf DdlCat_Ne_Num_Pagos.SelectedValue <> "1" And fecha_dia_pago > System.DateTime.Today.AddDays(7) Then
                    LblMsj.Text = "En Mayor A Una Exhibicion, La Fecha del Primer Pago No Puede Ser Mayor A 7 Dias"
                    MpuMensajes.Show()
                Else
                    LlenarCalendario()
                    'If (TxtFechaPagoInicial.Text.Substring(0, 2) = "15" Or TxtFechaPagoInicial.Text.Substring(0, 2) = "30") And (DdlPeriodicidad.SelectedValue = "Mensual") Then
                    '    LlenarCalendario()
                    'ElseIf (TxtFechaPagoInicial.Text.Substring(0, 2) = "15" Or TxtFechaPagoInicial.Text.Substring(0, 2) = "30") And (DdlPeriodicidad.SelectedValue = "Quincenal")
                    '    LlenarCalendario()
                    'ElseIf (DdlPeriodicidad.SelectedValue = "Semanal")
                    '    Dim DtosN As DataSet = Class_Negociaciones.LlenarElementosNego(TxtFechaPagoInicial.Text, "", "", 7)
                    '    If DtosN.Tables(0).Rows(0).Item("VIERNES") = "Viernes" Then
                    '        LlenarCalendario()
                    '    Else
                    '        LblMsj.Text = "El Dia De Pago Debe De Ser Viernes"
                    '        MpuMensajes.Show()
                    '    End If
                    'Else
                    '    LblMsj.Text = "El Dia De Pago Es Incorrecto Para La Periodicidad"
                    '    MpuMensajes.Show()
                    'End If
                End If
            End If

            'BtnGuardar.Visible = False
            'TxtContrasenaAuto.Visible = False
            'LblContrasenaAuto.Visible = False
            'TxtHist_Pr_SupervisorAuto.Visible = False
            'LblHist_Pr_SupervisorAuto.Visible = False
            'If TxtPagoInicial.Text = "" Or TxtPagoInicial.Text = "0" Or TxtFechaPagoInicial.Text = "" Then
            '    LblMsj.Text = "Faltan Datos"
            '    MpuMensajes.Show()
            'ElseIf Math.Round(Val(DdlCat_Ne_Num_Pagos.SelectedValue) * Val(TxtPagoInicial.Text), 2) <> Math.Round(Val(TxtSaldoNegociado.Text), 2) Then
            '    LblMsj.Text = "La Suma De Los Montos No Coinciden"
            '    MpuMensajes.Show()

            'Else
            '    TxtMontoReal.Text = Math.Round(Val(DdlCat_Ne_Num_Pagos.SelectedValue) * Val(TxtPagoInicial.Text), 2)
            '    If DdlCat_Ne_Nombre.SelectedValue = "CAT_NEGO_QUITAS" Then
            '        If (TxtFechaPagoInicial.Text.Substring(0, 2) = "15" Or TxtFechaPagoInicial.Text.Substring(0, 2) = "30") And (DdlPeriodicidad.SelectedValue = "Mensual") Then
            '            LlenarCalendario()
            '        ElseIf (TxtFechaPagoInicial.Text.Substring(0, 2) = "15") And (DdlPeriodicidad.SelectedValue = "Quincenal")
            '            LlenarCalendario()
            '        ElseIf (TxtFechaPagoInicial.Text.Substring(0, 2) = "Viernes") And (DdlPeriodicidad.SelectedValue = "Semanal")
            '            LlenarCalendario()
            '        Else
            '            LblMsj.Text = "El Dia De Pago Es Incorrectos Para La Periodicidad"
            '            MpuMensajes.Show()
            '        End If
            '    ElseIf DdlCat_Ne_Nombre.SelectedValue = "CAT_NEGO_PAGOS_FIJOS" Then


            '    End If
            'End If
        ElseIf DdlCat_Ne_Nombre.SelectedValue = "CAT_NEGO_PAGOS_FIJOS" Then
            If DdlContacto.SelectedValue = "Seleccione" Then
                LblMsj.Text = "Falta Persona de Contacto"
                MpuMensajes.Show()
            ElseIf DdlPeriodicidad.SelectedValue = "Seleccione" Then
                LblMsj.Text = "Seleccione Periodicidad"
                MpuMensajes.Show()
            ElseIf TxtPagoInicial.Text = "" Then
                LblMsj.Text = "Falta Pago Inicial"
                MpuMensajes.Show()
            ElseIf TxtFechaPagoInicial.Text = "" Or TxtFechaPagoInicial.Text = "__/__/____" Then
                LblMsj.Text = "Falta Fecha de Pago Inicial"
                MpuMensajes.Show()
            ElseIf val(TxtPagoInicial.Text) > val(TxtSaldoNegociado.Text) Then
                LblMsj.Text = "El pago inical no puede ser mayor al saldo negociado"
                MpuMensajes.Show()
            Else
                Dim fecha_dia_pago As Date
                fecha_dia_pago = TxtFechaPagoInicial.Text
                If fecha_dia_pago < System.DateTime.Today Then
                    LblMsj.Text = "La Fecha de Pago No Puede Ser Menor que la Fecha Actual"
                    MpuMensajes.Show()
                Else
                    LlenarCalendario()
                End If
            End If

        End If
    End Sub

    Sub LlenarCalendario()

        Dim DtePagosSigts As Date = TxtFechaPagoInicial.Text

        If DdlCat_Ne_Num_Pagos.SelectedValue = "1" Then
            Dim DttCalendario As DataTable = New DataTable()
            DttCalendario.Columns.Add("Fecha")
            DttCalendario.Columns.Add("Monto")
            Dim dtRow As DataRow
            dtRow = DttCalendario.NewRow()
            dtRow.Item(0) = TxtFechaPagoInicial.Text
            dtRow.Item(1) = TxtPagoInicial.Text
            DttCalendario.Rows.Add(dtRow)
            dtRow = DttCalendario.NewRow()
            GvCalendario.DataSource = DttCalendario
            GvCalendario.DataBind()
        Else

            Dim DttCalendario As DataTable = New DataTable()
            DttCalendario.Columns.Add("Fecha")
            DttCalendario.Columns.Add("Monto")
            Dim dtRow As DataRow
            dtRow = DttCalendario.NewRow()
            dtRow.Item(0) = TxtFechaPagoInicial.Text
            dtRow.Item(1) = TxtPagoInicial.Text
            DttCalendario.Rows.Add(dtRow)
            dtRow = DttCalendario.NewRow()
            Dim DPagosSubs As Double = Math.Round((Val(TxtSaldoNegociado.Text) - Val(TxtPagoInicial.Text)) / Val(DdlCat_Ne_Num_Pagos.SelectedValue - 1), 2)
            TxtMontoReal.Text = Math.Round(Val(TxtPagoInicial.Text) + (DPagosSubs * Val(DdlCat_Ne_Num_Pagos.SelectedValue - 1)))


            For x As Integer = 1 To Val(DdlCat_Ne_Num_Pagos.SelectedValue) - 1

                DtePagosSigts = DtePagosSigts.AddDays(1)
                Dim DtosN As DataSet = Class_Negociaciones.LlenarElementosNego(DtePagosSigts, "", "", 7)
                If DdlPeriodicidad.SelectedValue = "Semanal" Then
                    While DtosN.Tables(0).Rows(0).Item("VIERNES") <> "Viernes"
                        DtePagosSigts = DtePagosSigts.AddDays(1)
                        DtosN = Class_Negociaciones.LlenarElementosNego(DtePagosSigts, "", "", 7)
                    End While
                    'Dim DtsNegociacion As DataSet = Class_Negociaciones.LlenarElementosNego(DdlPeriodicidad.SelectedValue, DtePagosSigts, FN_SEMANAL(x), 1)
                    dtRow.Item(0) = DtePagosSigts.ToShortDateString 'DtsNegociacion.Tables("ELEMENTOS").Rows(0).Item("Fecha")

                ElseIf DdlPeriodicidad.SelectedValue = "Quincenal" Then
                    While (DtePagosSigts.Day() <> 15 And DtePagosSigts.Day() <> 30)
                        DtePagosSigts = DtePagosSigts.AddDays(+1)
                    End While
                    dtRow.Item(0) = DtePagosSigts.ToShortDateString
                Else
                    If TxtFechaPagoInicial.Text.Substring(0, 2) < 15 Then
                        While (DtePagosSigts.Day() <> 15)
                            DtePagosSigts = DtePagosSigts.AddDays(+1)
                        End While
                    ElseIf TxtFechaPagoInicial.Text.Substring(0, 2) < 30 And TxtFechaPagoInicial.Text.Substring(0, 2) > 15 Then
                        While (DtePagosSigts.Day() <> 30)
                            DtePagosSigts = DtePagosSigts.AddDays(+1)
                        End While
                    ElseIf TxtFechaPagoInicial.Text.Substring(0, 2) = 15 Then
                        DtePagosSigts = DtePagosSigts.AddDays(+1)
                        While (DtePagosSigts.Day() <> 15)
                            DtePagosSigts = DtePagosSigts.AddDays(+1)
                        End While
                    ElseIf TxtFechaPagoInicial.Text.Substring(0, 2) = 30 Then
                        DtePagosSigts = DtePagosSigts.AddDays(+1)
                        While (DtePagosSigts.Day() <> 30)
                            DtePagosSigts = DtePagosSigts.AddDays(+1)
                        End While
                    End If
                    dtRow.Item(0) = DtePagosSigts.ToShortDateString
                End If
                dtRow.Item(1) = DPagosSubs 'Math.Round((Val(TxtSaldoNegociado.Text) - Val(TxtPagoInicial.Text)) / Val(DdlCat_Ne_Num_Pagos.SelectedValue), 2)
                DttCalendario.Rows.Add(dtRow)
                dtRow = DttCalendario.NewRow()




            Next
            GvCalendario.DataSource = DttCalendario
            GvCalendario.DataBind()
        End If

        'For x As Integer = 2 To DdlCat_Ne_Num_Pagos.SelectedValue
        '    If x <> DdlCat_Ne_Num_Pagos.SelectedValue Then
        '        If DdlPeriodicidad.SelectedValue = "Semanal" Then
        '            Dim DtsNegociacion As DataSet = Class_Negociaciones.LlenarElementosNego(DdlPeriodicidad.SelectedValue, TxtFechaPagoInicial.Text, FN_SEMANAL(x), 1)
        '            dtRow.Item(0) = DtsNegociacion.Tables("ELEMENTOS").Rows(0).Item("Fecha")
        '        ElseIf DdlPeriodicidad.SelectedValue = "Quincenal" Then
        '            Dim DtsNegociacion As DataSet = Class_Negociaciones.LlenarElementosNego(DdlPeriodicidad.SelectedValue, TxtFechaPagoInicial.Text, FN_QUINCENAL(x), 1)
        '            dtRow.Item(0) = DtsNegociacion.Tables("ELEMENTOS").Rows(0).Item("Fecha")
        '        Else
        '            Dim DtsNegociacion As DataSet = Class_Negociaciones.LlenarElementosNego(DdlPeriodicidad.SelectedValue, TxtFechaPagoInicial.Text, x, 1)
        '            dtRow.Item(0) = DtsNegociacion.Tables("ELEMENTOS").Rows(0).Item("Fecha")
        '        End If
        '        dtRow.Item(1) = TxtPagoInicial.Text
        '        DttCalendario.Rows.Add(dtRow)
        '        dtRow = DttCalendario.NewRow()
        '    Else
        '        If DdlPeriodicidad.SelectedValue = "Semanal" Then

        '            Dim DtsNegociacion As DataSet = Class_Negociaciones.LlenarElementosNego(DdlPeriodicidad.SelectedValue, TxtFechaPagoInicial.Text, FN_SEMANAL(x), 1)
        '            dtRow.Item(0) = DtsNegociacion.Tables("ELEMENTOS").Rows(0).Item("Fecha")
        '            dtRow.Item(1) = TxtPagoInicial.Text
        '            DttCalendario.Rows.Add(dtRow)
        '            dtRow = DttCalendario.NewRow()
        '        ElseIf DdlPeriodicidad.SelectedValue = "Quincenal" Then
        '            Dim DtsNegociacion As DataSet = Class_Negociaciones.LlenarElementosNego(DdlPeriodicidad.SelectedValue, TxtFechaPagoInicial.Text, FN_QUINCENAL(x), 1)
        '            dtRow.Item(0) = DtsNegociacion.Tables("ELEMENTOS").Rows(0).Item("Fecha")
        '            dtRow.Item(1) = TxtPagoInicial.Text
        '            DttCalendario.Rows.Add(dtRow)
        '            dtRow = DttCalendario.NewRow()
        '        Else

        '            Dim DtsNegociacion As DataSet = Class_Negociaciones.LlenarElementosNego(DdlPeriodicidad.SelectedValue, TxtFechaPagoInicial.Text, x, 1)
        '            dtRow.Item(0) = DtsNegociacion.Tables("ELEMENTOS").Rows(0).Item("Fecha")
        '            dtRow.Item(1) = TxtPagoInicial.Text
        '            DttCalendario.Rows.Add(dtRow)
        '            dtRow = DttCalendario.NewRow()
        '        End If

        '    End If
        'Next
        'GvCalendario.DataSource = DttCalendario
        'GvCalendario.DataBind()
        GvCalendario.Visible = True

        BtnVisualizar.Visible = False
        BtnCancelarVis.Visible = True
        BtnGuardar.Visible = True
        BtnGuardar.Enabled = True
        Bloquear_Desbloquear(1)









        'If DdlCat_Ne_Num_Pagos.SelectedValue = "1" Then
        '    Dim DttCalendario As DataTable = New DataTable()
        '    DttCalendario.Columns.Add("Fecha")
        '    DttCalendario.Columns.Add("Monto")
        '    Dim dtRow As DataRow
        '    dtRow = DttCalendario.NewRow()
        '    dtRow.Item(0) = TxtFechaPagoInicial.Text
        '    dtRow.Item(1) = TxtPagoInicial.Text
        '    DttCalendario.Rows.Add(dtRow)
        '    dtRow = DttCalendario.NewRow()
        '    GvCalendario.DataSource = DttCalendario
        '    GvCalendario.DataBind()
        'Else
        '    Dim DttCalendario As DataTable = New DataTable()
        '    DttCalendario.Columns.Add("Fecha")
        '    DttCalendario.Columns.Add("Monto")
        '    Dim dtRow As DataRow
        '    dtRow = DttCalendario.NewRow()
        '    For x As Integer = 1 To DdlCat_Ne_Num_Pagos.SelectedValue
        '        If x <> DdlCat_Ne_Num_Pagos.SelectedValue Then
        '            'Dim DtsNegociacion As DataSet = Class_Negociaciones.LlenarElementosNego(TxtFechaPagoInicial.Text, TxtDiasPago.Text, x, 1)
        '            Dim DtsNegociacion As DataSet = Class_Negociaciones.LlenarElementosNego(TxtFechaPagoInicial.Text, TxtFechaPagoInicial.Text, x, 1)
        '            dtRow.Item(0) = DtsNegociacion.Tables("ELEMENTOS").Rows(0).Item("Fecha")
        '            dtRow.Item(1) = TxtPagoInicial.Text
        '            DttCalendario.Rows.Add(dtRow)
        '            dtRow = DttCalendario.NewRow()
        '        Else
        '            'Dim DtsNegociacion As DataSet = Class_Negociaciones.LlenarElementosNego(TxtFechaPagoInicial.Text, TxtDiasPago.Text, x, 1)
        '            Dim DtsNegociacion As DataSet = Class_Negociaciones.LlenarElementosNego(TxtFechaPagoInicial.Text, TxtFechaPagoInicial.Text, x, 1)
        '            dtRow.Item(0) = DtsNegociacion.Tables("ELEMENTOS").Rows(0).Item("Fecha")
        '            dtRow.Item(1) = TxtPagoInicial.Text
        '            DttCalendario.Rows.Add(dtRow)
        '            dtRow = DttCalendario.NewRow()
        '        End If
        '    Next
        '    GvCalendario.DataSource = DttCalendario
        '    GvCalendario.DataBind()
        'End If


    End Sub
    Sub Bloquear_Desbloquear(ByVal V_Bandera As Integer)
        If V_Bandera = 1 Then
            TxtDescuento.Enabled = False
            DdlCat_Ne_Num_Pagos.Enabled = False
            DdlPeriodicidad.Enabled = False
            DdlExhibiciones.Enabled = False
            TxtSaldoNegociado.Enabled = False
            TxtPagoInicial.Enabled = False
            TxtFechaPagoInicial.Enabled = False
            'TxtDiasPago.Enabled = False
            'TxtMontoPagos.Enabled = False

        Else
            TxtDescuento.Enabled = True
            DdlCat_Ne_Num_Pagos.Enabled = True
            DdlPeriodicidad.Enabled = True
            DdlExhibiciones.Enabled = True
            TxtSaldoNegociado.Enabled = True
            TxtPagoInicial.Enabled = True
            TxtFechaPagoInicial.Enabled = True
            'TxtDiasPago.Enabled = True
            'TxtMontoPagos.Enabled = True
        End If
    End Sub

    Protected Sub BtnCancelarVis_Click(sender As Object, e As EventArgs) Handles BtnCancelarVis.Click
        BtnVisualizar.Visible = True
        BtnCancelarVis.Visible = False
        BtnGuardar.Visible = False
        GvCalendario.DataSource = Nothing
        GvCalendario.DataBind()
        Bloquear_Desbloquear(0)
        TxtMontoReal.Text = ""
    End Sub

    Protected Sub BtnAddTel_Click(sender As Object, e As EventArgs) Handles BtnAddTel.Click
        Dim TmpCredito As credito = CType(Session("Credito"), credito)
        Dim TmpUsuario As USUARIO = CType(Session("USUARIO"), USUARIO)
        LblMsj.Text = Class_InformacionAdicional.AgregarTelefono(0, TmpCredito.PR_MC_CREDITO, TmpCredito.PR_MC_PRODUCTO, TxtHist_Te_Lada.Text, TxtHist_Te_Numerotel.Text, DdlHist_Te_Tipo.SelectedValue, DdlHist_Te_Parentesco.SelectedValue, TxtHist_Te_Nombre.Text, TxtHist_Te_Extension.Text, TxtHist_Te_Horario0.Text, TxtHist_Te_Horario1.Text, TmpUsuario.CAT_LO_USUARIO, TmpCredito.PR_MC_AGENCIA, "Captura", BtnAddTel, GvDiasTel, TxtHist_Te_Proporciona.Text)
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
        ElseIf V_Bandera = 1 Then
            LblHist_Co_Nombre.Visible = False
            TxtHist_Co_Nombre.Visible = False
            TxtHist_Co_Nombre.Text = ""
            DdlHist_Co_Tipo.SelectedValue = "Personal"
            TxtHist_Co_Correo.Text = ""
            DdlHist_Co_Parentesco.SelectedValue = "Cliente"
            CbxHist_Co_Contacto.Checked = False
        ElseIf V_Bandera = 2 Then
            TxtDescuento.Text = ""
            TxtSaldoNegociado.Text = ""
            'TxtMontoPagos.Text = ""
            'TxtDiasPago.Text = ""
            TxtPagoInicial.Text = ""
            TxtFechaPagoInicial.Text = ""
            'TxtDescuentoF.Text = ""
            'TxtSaldoNegociadoF.Text = ""
            DdlHist_Ge_NoPago.SelectedValue = "Seleccione"
            DdlHist_Ge_Resultado.SelectedValue = "Seleccione"
            TxtHist_Ge_Comentario.Text = ""
            'TxtMontoReal.Text = ""
        End If
    End Sub
    Protected Sub BtnCancelAddCorreo_Click(sender As Object, e As EventArgs) Handles BtnCancelAddCorreo.Click
        Limpiar(1)
    End Sub
    Protected Sub BtnAddCorreo_Click(sender As Object, e As EventArgs) Handles BtnAddCorreo.Click
        Dim TmpCredito As credito = CType(Session("Credito"), credito)
        Dim TmpUsuario As USUARIO = CType(Session("USUARIO"), USUARIO)
        LblMsj.Text = Class_InformacionAdicional.AgregarCorreo(0, TmpCredito.PR_MC_CREDITO, TmpCredito.PR_MC_PRODUCTO, DdlHist_Co_Parentesco.SelectedValue, TxtHist_Co_Nombre.Text, TxtHist_Co_Correo.Text, Boleano(CbxHist_Co_Contacto.Checked), TmpUsuario.CAT_LO_USUARIO, TmpCredito.PR_MC_AGENCIA, "Captura", DdlHist_Co_Tipo.SelectedValue, BtnAddCorreo, TxtHist_Co_Proporciona.Text)
        If LblMsj.Text = "1" Then
            Dim dts As DataSet = Class_InformacionAdicional.LlenarElementosAgregar(CType(Session("Credito"), credito).PR_MC_CREDITO, 4)
            If dts.Tables(0).Rows.Count > 0 Then
                DdlPeriodicidad.DataSource = dts
                DdlPeriodicidad.DataBind()
            End If
            LblMsj.Text = "Correo Agregado"
            MpuMensajes.Show()
            Limpiar(1)
        ElseIf LblMsj.Text = "0" Then
            LblMsj.Text = "Correo Actualizado"
            MpuMensajes.Show()
            Limpiar(1)
        Else
            MpuMensajes.Show()
        End If
    End Sub
    Protected Sub DdlHist_Co_Parentesco_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlHist_Co_Parentesco.SelectedIndexChanged
        If DdlHist_Co_Parentesco.SelectedValue <> "Cliente" Then
            LblHist_Co_Nombre.Visible = True
            TxtHist_Co_Nombre.Visible = True
        Else
            LblHist_Co_Nombre.Visible = False
            TxtHist_Co_Nombre.Visible = False
            TxtHist_Co_Nombre.Text = ""
        End If
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
    Protected Sub BtnGuardar_Click(sender As Object, e As EventArgs) Handles BtnGuardar.Click
        Try
            Dim TmpAplcc As Aplicacion = CType(Session("Aplicacion"), Aplicacion)
            If DdlHist_Ge_Resultado.SelectedValue = "Seleccione" Then
                LblMsj.Text = "Seleccione Un Resultado"
                MpuMensajes.Show()
            ElseIf TxtHist_Ge_Comentario.Text.Length < 5 Then
                LblMsj.Text = "Capture Un Comentario Valido"
                MpuMensajes.Show()
            ElseIf (TmpAplcc.NOPAGO = 1 And DdlHist_Ge_NoPago.SelectedValue = "Seleccione" And DdlHist_Ge_Resultado.SelectedValue.Split(",")(1) = 1) Then
                LblMsj.Text = "Seleccione Una Causa De No Pago"
                MpuMensajes.Show()
            ElseIf TxtFechaSeguimiento.Text = "" Or TxtFechaSeguimiento.Text = "__/__/____" Then
                LblMsj.Text = "Seleccione Una Fecha de Seguimiento"
                MpuMensajes.Show()
            Else
                Dim TmpUsr As USUARIO = CType(Session("USUARIO"), USUARIO)
                If TmpUsr.CAT_LO_PGESTION.ToString.Substring(4, 1) = 1 Then
                    BtnGuardar.Visible = False
                    ''BtnGuardar.Enabled = False
                    GuardarNegociacion()
                    TmpUsr.GESTIONES = Val(TmpUsr.GESTIONES) + 1
                    If DdlHist_Ge_Resultado.SelectedValue = "PP,0" Or DdlHist_Ge_Resultado.SelectedValue = "CD,0" Or DdlHist_Ge_Resultado.SelectedValue = "PM,0" Or DdlHist_Ge_Resultado.SelectedValue = "PO,0" Or DdlHist_Ge_Resultado.SelectedValue = "Q1,0" Or DdlHist_Ge_Resultado.SelectedValue = "Q2,0" Or DdlHist_Ge_Resultado.SelectedValue = "PV,0" Or DdlHist_Ge_Resultado.SelectedValue = "PL,0" Then
                        TmpUsr.CUANTASPP = Val(TmpUsr.CUANTASPP) + 1
                        TmpUsr.MONTOPP = Val(TmpUsr.MONTOPP.Replace("$", "").Replace(",", "")) + Val(TxtPagoInicial.Text)
                        TmpUsr.MONTOPP = to_money(TmpUsr.MONTOPP.ToString)
                    End If
                    Dim dts As DataSet = Class_InformacionAdicional.LlenarElementosAgregar(CType(Session("Credito"), credito).PR_MC_CREDITO, 4)
                    If dts.Tables(0).Rows.Count > 0 Then 'validamos que existan correos
                        'Generando Convenio y enviandolo al correo del contacto siempre y cuando este cuente con correo
                        GenerarConvenio(DdlCat_Ne_Nombre.SelectedItem.Text, CType(Session("Credito"), credito).PR_MC_CREDITO, CType(Session("Credito"), credito).PR_MC_PRODUCTO)
                    End If


                Else
                    TxtContrasenaAuto.Visible = True
                    TxtHist_Pr_SupervisorAuto.Visible = True
                    If TxtContrasenaAuto.Text = "" Then
                        LblMsj.Text = "Capture Una Contraseña"
                        MpuMensajes.Show()
                    ElseIf TxtHist_Pr_SupervisorAuto.Text = "" Then
                        LblMsj.Text = "Capture Un Usuario"
                        MpuMensajes.Show()
                    Else
                        Dim Autorizado As String = Class_CapturaVisitas.VariosQ(TxtContrasenaAuto.Text, TxtHist_Pr_SupervisorAuto.Text, 11)
                        If Autorizado = "INCORRECTO" Then
                            LblMsj.Text = "Usuario O Contraseña Incorrectos"
                            MpuMensajes.Show()
                        Else
                            If Autorizado = "1" Then
                                BtnGuardar.Visible = False
                                GuardarNegociacion()

                                TmpUsr.GESTIONES = Val(TmpUsr.GESTIONES) + 1
                                If DdlHist_Ge_Resultado.SelectedValue = "PP,0" Or DdlHist_Ge_Resultado.SelectedValue = "CD,0" Or DdlHist_Ge_Resultado.SelectedValue = "PM,0" Or DdlHist_Ge_Resultado.SelectedValue = "PO,0" Or DdlHist_Ge_Resultado.SelectedValue = "Q1,0" Or DdlHist_Ge_Resultado.SelectedValue = "Q2,0" Or DdlHist_Ge_Resultado.SelectedValue = "PV,0" Or DdlHist_Ge_Resultado.SelectedValue = "PL,0" Then
                                    TmpUsr.CUANTASPP = Val(TmpUsr.CUANTASPP) + 1
                                    TmpUsr.MONTOPP = Val(TmpUsr.MONTOPP.Replace("$", "").Replace(",", "")) + Val(TxtPagoInicial.Text)
                                    TmpUsr.MONTOPP = to_money(TmpUsr.MONTOPP.ToString)
                                End If
                                Dim dts As DataSet = Class_InformacionAdicional.LlenarElementosAgregar(CType(Session("Credito"), credito).PR_MC_CREDITO, 4)
                                If dts.Tables(0).Rows.Count > 0 Then 'validamos que existan correos
                                    'Generando Convenio y enviandolo al correo del contacto siempre y cuando este cuente con correo
                                    GenerarConvenio(DdlCat_Ne_Nombre.SelectedItem.Text, CType(Session("Credito"), credito).PR_MC_CREDITO, CType(Session("Credito"), credito).PR_MC_PRODUCTO)
                                End If


                            Else
                                LblMsj.Text = "El Usuario No Tiene Permiso Para Realizar Esta Acción"
                                MpuMensajes.Show()
                            End If
                        End If
                    End If
                End If
                Response.Redirect("MasterPage.aspx", False)
            End If

        Catch ex As Exception
            Dim TmpCredito As credito = CType(Session("Credito"), credito)
            Dim TmpUsr As USUARIO = CType(Session("USUARIO"), USUARIO)
            SendMail("Negociaciones.aspx", ex, TmpCredito.PR_MC_CREDITO, "", TmpUsr.CAT_LO_USUARIO)
            Response.Redirect("MasterPage.aspx", False)
        End Try

    End Sub

    Sub GuardarNegociacion()
        BtnGuardar.Visible = False
        Dim TmpCredito As credito = CType(Session("Credito"), credito)
        Dim TmpUsr As USUARIO = CType(Session("USUARIO"), USUARIO)
        Dim TmpAplcc As Aplicacion = CType(Session("Aplicacion"), Aplicacion)
        Dim Estatus As String = ""
        Dim NombreFila As String = CType(Session("NombreFila"), String)
        If DdlCat_Ne_Num_Pagos.SelectedValue <> "1" Then ' MAS DE UN PAGO

            Dim DblMtoPagos As String
            Dim StrFechaProm As String
            Dim StrMontoPP As String
            Dim Cuantos As Integer = 0
            Dim Actualizar As Integer = 0
            Dim Ultimo As Integer
            Ultimo = GvCalendario.Rows.Count - 1
            If DdlCat_Ne_Nombre.SelectedValue <> "CAT_NEGO_QUITAS" Then
                TxtDescuento.Text = Math.Round(((Val(TxtPagoInicial.Text) * 100) / Val(TmpCredito.PR_CF_SALDO_TOT.ToString)), 2)
                DblMtoPagos = Math.Round(((Val(TmpCredito.PR_CF_SALDO_TOT) - Val(TxtPagoInicial.Text)) / Val(DdlCat_Ne_Num_Pagos.SelectedValue)), 2)
            Else
                DblMtoPagos = GvCalendario.Rows(1).Cells(1).Text
            End If
            Dim filas As Integer = GvCalendario.Rows.Count
            For Each gvRow As GridViewRow In GvCalendario.Rows
                If gvRow.RowIndex = Ultimo Then
                    Actualizar = 1
                    'StrFechaProm = TxtFechaPagoInicial.Text
                    'StrMontoPP = TxtPagoInicial.Text
                    'Else
                End If
                StrFechaProm = gvRow.Cells(0).Text.Replace("$", "").Replace(",", "")
                StrMontoPP = gvRow.Cells(1).Text.Replace("$", "").Replace(",", "")

                If DdlCat_Ne_Nombre.SelectedValue = "CAT_NEGO_QUITAS" Then
                    Class_Negociaciones.GuardarNego(TmpCredito.PR_MC_CREDITO, TmpCredito.PR_MC_PRODUCTO, StrMontoPP, StrFechaProm, TmpUsr.CAT_LO_USUARIO, "Negociacion", Cuantos, "Telefonica", TmpCredito.PR_MC_AGENCIA, "PR", DdlHist_Ge_Resultado.SelectedItem.Text, DdlHist_Ge_Resultado.SelectedValue, DdlHist_Ge_NoPago.SelectedValue, "Se contacta a ALUMNO. Se realiza QUITA MAS PLAZO del " & TxtDescuento.Text & "% sobre saldo total $" & TmpCredito.PR_CF_SALDO_TOT & ".  Cubrirá saldo de $ " & TxtSaldoNegociado.Text & " en " & (Val(DdlCat_Ne_Num_Pagos.SelectedValue) + 1) & " pago(s). Pago inicial el  " & TxtFechaPagoInicial.Text & " Por $ " & TxtPagoInicial.Text & " y " & DdlCat_Ne_Num_Pagos.SelectedValue & " pago(s) cada " & DdlPeriodicidad.SelectedValue.ToString.Replace("Semanal", "Semana").Replace("Quincenal", "15 dias").Replace("Mensual", "Mes") & " por $ " & DblMtoPagos, "", TxtHist_Te_Lada.Text & TxtHist_Te_Numerotel.Text, 0, TmpCredito.PR_MC_CODIGO, Actualizar, TxtFechaSeguimiento.Text & " " & DdlHora.SelectedValue & ":00:00", DdlCat_Ne_Nombre.Text, "", DdlPeriodicidad.SelectedValue, TxtMontoReal.Text.Replace("$", "").Replace(",", ""), "", "", TmpCredito.PR_CA_CAMPANA, TmpCredito.PR_CF_BUCKET, NombreFila)
                Else
                    Class_Negociaciones.GuardarNego(TmpCredito.PR_MC_CREDITO, TmpCredito.PR_MC_PRODUCTO, StrMontoPP, StrFechaProm, TmpUsr.CAT_LO_USUARIO, "Negociacion", Cuantos, "Telefonica", TmpCredito.PR_MC_AGENCIA, "PR", DdlHist_Ge_Resultado.SelectedItem.Text, DdlHist_Ge_Resultado.SelectedValue, DdlHist_Ge_NoPago.SelectedValue, "Se contacta a ALUMNO. Se realiza PLAN DE PAGOS, Pago inicial de " & TxtDescuento.Text & "% sobre saldo total $ " & TmpCredito.PR_CF_SALDO_TOT & ". Cubrirá saldo de $ " & TxtSaldoNegociado.Text & " en " & (Val(DdlCat_Ne_Num_Pagos.SelectedValue) + 1) & " pago(s). Pago inicial el  " & TxtFechaPagoInicial.Text & " Por $ " & TxtPagoInicial.Text & " y " & DdlCat_Ne_Num_Pagos.SelectedValue & " pago(s) cada " & DdlPeriodicidad.SelectedValue.ToString.Replace("Semanal", "Semana").Replace("Quincenal", "15 dias").Replace("Mensual", "Mes") & " por $ " & DblMtoPagos, "", TxtHist_Te_Lada.Text & TxtHist_Te_Numerotel.Text, 0, TmpCredito.PR_MC_CODIGO, Actualizar, TxtFechaSeguimiento.Text & " " & DdlHora.SelectedValue & ":00:00", DdlCat_Ne_Nombre.Text, "", DdlPeriodicidad.SelectedValue, TxtMontoReal.Text.Replace("$", "").Replace(",", ""), "", "", TmpCredito.PR_CA_CAMPANA,
                      TmpCredito.PR_CF_BUCKET, NombreFila)
                End If
                '-------------------------------------------------------------------------------------------------------------------------------
                '"Negociacion A " & DdlCat_Ne_Num_Pagos.SelectedValue & " Pagos Por " & TxtMontoReal.Text & " " & TxtHist_Ge_Comentario.Text, "", TxtHist_Te_Lada.Text & TxtHist_Te_Numerotel.Text, 0, TmpCredito.PR_MC_CODIGO, Actualizar, TxtFechaSeguimiento.Text & " " & DdlHora.SelectedValue & ":00:00", DdlCat_Ne_Nombre.Text, "", DdlPeriodicidad.SelectedValue, TxtMontoReal.Text.Replace("$", "").Replace(",", ""), "", "")
                Cuantos = Cuantos + 1
            Next
            Session("Buscar") = 1
            Response.Redirect("MasterPage.aspx", False)

        Else '  UN PAGO
            Class_Negociaciones.GuardarNego(TmpCredito.PR_MC_CREDITO, TmpCredito.PR_MC_PRODUCTO, TxtPagoInicial.Text, TxtFechaPagoInicial.Text, TmpUsr.CAT_LO_USUARIO, "Negociacion", 0, "Telefonica", TmpCredito.PR_MC_AGENCIA, "PR", DdlHist_Ge_Resultado.SelectedItem.Text, DdlHist_Ge_Resultado.SelectedValue, DdlHist_Ge_NoPago.SelectedValue, "Negociacion Con Pago En Una Sola Exhibicion Por " & TxtPagoInicial.Text & " " & TxtHist_Ge_Comentario.Text, "", TxtHist_Te_Lada.Text & TxtHist_Te_Numerotel.Text, 0, TmpCredito.PR_MC_CODIGO, 1, TxtFechaSeguimiento.Text & " " & DdlHora.SelectedValue & ":00:00", DdlCat_Ne_Nombre.Text, "", DdlPeriodicidad.SelectedValue, TxtSaldoNegociado.Text, "", "", TmpCredito.PR_CA_CAMPANA, TmpCredito.PR_CF_BUCKET, NombreFila)
            Session("Buscar") = 1
            Response.Redirect("MasterPage.aspx", False)

        End If
    End Sub
    'Function ValidarNegociacion(ByVal V_Bandera As Integer) As String
    '    Dim TmpCredito As Credito = CType(Session("Credito"), Credito)
    '    If V_Bandera = 0 Then
    '        If DdlCat_Ne_Num_Pagos.SelectedValue = 1 Then
    '            If TxtPagoInicial.Text < Math.Round(TmpCredito.PR_CF_IMPT_DEUDA_ACT * (Val(LblCat_Ne_Porciento.Text.Replace("%", "")) / 100), 2) Then
    '                Return "El Monto del Pago No Puede Ser Menor A " & Math.Round(TmpCredito.PR_CF_IMPT_DEUDA_ACT * (Val(LblCat_Ne_Porciento.Text.Replace("%", "")) / 100), 2)
    '            ElseIf TxtFechaPagoInicial.Text = "" Then
    '                Return "seleccione La Fecha De Pago"
    '            Else
    '                Return "1"
    '            End If
    '        Else
    '            If TxtPagoInicial.Text < Math.Round(TmpCredito.PR_CF_IMPT_DEUDA_ACT * (Val(LblCat_Ne_Porciento.Text.Replace("%", "")) / 100), 2) Then
    '                Return "El Monto del Pago No Puede Ser Menor A " & Math.Round(TmpCredito.PR_CF_IMPT_DEUDA_ACT * (Val(LblCat_Ne_Porciento.Text.Replace("%", "")) / 100), 2)
    '            ElseIf TxtFechaPagoInicial.Text = "" Then
    '                Return "seleccione La Fecha De Pago"
    '            Else
    '                Return "1"
    '            End If
    '        End If
    '    Else
    '        If Val(TxtDescuento.Text) < Val(LblCat_Ne_Desc_Min.Text) Or Val(TxtDescuento.Text) > Val(LblCat_Ne_Desc_Max.Text) Then
    '            Return "El Porcentaje De Descuento No Puede Ser Mayor A " & LblCat_Ne_Desc_Max.Text & " Ni Menor A" & LblCat_Ne_Desc_Min.Text
    '        ElseIf Val(TxtPagoInicial.Text) < Val(LblMinSaldoInic.Text) Then
    '            Return "El Monto del Pago Inicial No Puede Ser Menor A " & Val(LblMinSaldoInic.Text)
    '        ElseIf TxtFechaPagoInicial.Text = "" Then
    '            Return "seleccione La Fecha Del Pago Inicial"
    '        ElseIf Class_Negociaciones.LlenarElementosNego(TxtFechaPagoInicial.Text, "", "", 3).Tables("ELEMENTOS").Rows(0).Item("Dias") > LblCat_Ne_Diasprimerpago.Text Then
    '            Return "La Fecha Del Primer Pago No Puede Exceder " & LblCat_Ne_Diasprimerpago.Text & " Días"
    '        ElseIf TxtDiasPago.Text = "" Then
    '            Return "seleccione La Fecha De Los Pagos Subsecuentes"
    '        ElseIf Class_Negociaciones.LlenarElementosNego(TxtFechaPagoInicial.Text, TxtDiasPago.Text, "", 4).Tables("ELEMENTOS").Rows(0).Item("Dias") > LblCat_Ne_Diaspagos.Text Then
    '            Return "La Fecha Del Primer Pago Y Los Pagos Subsecuentes No Puede Exceder " & LblCat_Ne_Diaspagos.Text & " Días"
    '        ElseIf Val(TxtSaldoNegociado.Text) < Val(LblMinSaldoNego.Text) Then
    '            Return "El Monto Negociado No Puede Ser Menor A" & Val(LblMinSaldoNego.Text)
    '        ElseIf Val(TxtPagoInicial.Text + (TxtMontoPagos.Text * DdlCat_Ne_Num_Pagos.SelectedValue)) < Val(TxtSaldoNegociado.Text) Then
    '            Return "La Suma De los Pagos No Puede Ser Menor A " & TxtSaldoNegociado.Text & " y es:" & Val(TxtPagoInicial.Text + (TxtMontoPagos.Text * DdlCat_Ne_Num_Pagos.SelectedValue))
    '        Else
    '            Return "1"
    '        End If
    '    End If
    'End Function


    Function ValidarNegociacion(ByVal V_Bandera As Integer) As String
        Dim TmpCredito As credito = CType(Session("Credito"), credito)
        'If V_Bandera = 0 Then
        '    If DdlCat_Ne_Num_Pagos.SelectedValue = 1 Then
        '        If TxtPagoInicial.Text < Math.Round(TmpCredito.PR_CF_IMPT_DEUDA_ACT * (Val(LblCat_Ne_Porciento.Text.Replace("%", "")) / 100), 2) Then
        '            Return "El Monto del Pago No Puede Ser Menor A " & Math.Round(TmpCredito.PR_CF_IMPT_DEUDA_ACT * (Val(LblCat_Ne_Porciento.Text.Replace("%", "")) / 100), 2)
        '        ElseIf TxtFechaPagoInicial.Text = "" Then
        '            Return "seleccione La Fecha De Pago"
        '        Else
        '            Return "1"
        '        End If
        '    Else
        '        'If TxtPagoInicial.Text < Math.Round(TmpCredito.PR_CF_IMPT_DEUDA_ACT * (Val(LblCat_Ne_Porciento.Text.Replace("%", "")) / 100), 2) Or TxtPagoInicial.Text > Math.Round(TmpCredito.PR_CF_IMPT_DEUDA_ACT * (Val(LblCat_Ne_Porciento2.Text.Replace("%", "")) / 100), 2) Then
        '        If TxtPagoInicial.Text < Math.Round(TmpCredito.PR_CF_IMPT_DEUDA_ACT * (Val(LblCat_Ne_Porciento.Text.Replace("%", "")) / 100), 2) Then
        '            Return "El Monto del Pago No Puede Ser Menor A " & Math.Round(TmpCredito.PR_CF_IMPT_DEUDA_ACT * (Val(LblCat_Ne_Porciento.Text.Replace("%", "")) / 100), 2)
        '            'Return "El Monto del Pago No Puede Ser Menor A " & Math.Round(TmpCredito.PR_CF_IMPT_DEUDA_ACT * (Val(LblCat_Ne_Porciento.Text.Replace("%", "")) / 100), 2) & " Ni Mayor A " & Math.Round(TmpCredito.PR_CF_IMPT_DEUDA_ACT * (Val(LblCat_Ne_Porciento2.Text.Replace("%", "")) / 100), 2)
        '        ElseIf TxtFechaPagoInicial.Text = "" Then
        '            Return "seleccione La Fecha De Pago"
        '        Else
        '            Return "1"
        '        End If
        '    End If
        'Else
        '    If Val(TxtDescuento.Text) < Val(LblCat_Ne_Desc_Min.Text) Or Val(TxtDescuento.Text) > Val(LblCat_Ne_Desc_Max.Text) Then
        '        Return "El Porcentaje De Descuento No Puede Ser Mayor A " & LblCat_Ne_Desc_Max.Text & " Ni Menor A" & LblCat_Ne_Desc_Min.Text
        '    ElseIf TxtPagoInicial.Text < Math.Round(((TmpCredito.PR_CF_IMPT_DEUDA_ACT) - (TmpCredito.PR_CF_IMPT_DEUDA_ACT * (Val(TxtDescuento.Text) / 100))) * (Val(LblCat_Ne_Porciento.Text.Replace("%", "")) / 100), 2) Or TxtPagoInicial.Text > Math.Round(((TmpCredito.PR_CF_IMPT_DEUDA_ACT) - (TmpCredito.PR_CF_IMPT_DEUDA_ACT * (Val(TxtDescuento.Text) / 100))) * (Val(LblCat_Ne_Porciento2.Text.Replace("%", "")) / 100), 2) Then
        '        Return "El Monto del Pago Inicial No Puede Ser Menor A " & Math.Round(((TmpCredito.PR_CF_IMPT_DEUDA_ACT) - (TmpCredito.PR_CF_IMPT_DEUDA_ACT * (Val(TxtDescuento.Text) / 100))) * (Val(LblCat_Ne_Porciento.Text.Replace("%", "")) / 100), 2) & " Ni Mayor A " & Math.Round(((TmpCredito.PR_CF_IMPT_DEUDA_ACT) - (TmpCredito.PR_CF_IMPT_DEUDA_ACT * (Val(TxtDescuento.Text) / 100))) * (Val(LblCat_Ne_Porciento2.Text.Replace("%", "")) / 100), 2)
        '    ElseIf TxtFechaPagoInicial.Text = "" Then
        '        Return "seleccione La Fecha Del Pago Inicial"
        '    ElseIf Class_Negociaciones.LlenarElementosNego(TxtFechaPagoInicial.Text, "", "", 3).Tables("ELEMENTOS").Rows(0).Item("Dias") > LblCat_Ne_Diasprimerpago.Text Then
        '        Return "La Fecha Del Primer Pago No Puede Exceder " & LblCat_Ne_Diasprimerpago.Text & " Días"
        '    ElseIf TxtDiasPago.Text = "" Then
        '        Return "seleccione La Fecha De Los Pagos Subsecuentes"
        '    ElseIf Class_Negociaciones.LlenarElementosNego(TxtFechaPagoInicial.Text, TxtDiasPago.Text, "", 4).Tables("ELEMENTOS").Rows(0).Item("Dias") > LblCat_Ne_Diaspagos.Text Then
        '        Return "La Fecha Del Primer Pago Y Los Pagos Subsecuentes No Puede Exceder " & LblCat_Ne_Diaspagos.Text & " Días"
        '    ElseIf TxtSaldoNegociado.Text > Math.Round((TmpCredito.PR_CF_IMPT_DEUDA_ACT) - (TmpCredito.PR_CF_IMPT_DEUDA_ACT * (Val(LblCat_Ne_Desc_Min.Text.Replace("%", "")) / 100)), 2) Or TxtSaldoNegociado.Text < Math.Round((TmpCredito.PR_CF_IMPT_DEUDA_ACT) - (TmpCredito.PR_CF_IMPT_DEUDA_ACT * (Val(LblCat_Ne_Desc_Max.Text.Replace("%", "")) / 100)), 2) Then
        '        Return "El Monto Negociado No Puede Ser Menor A " & Math.Round((TmpCredito.PR_CF_IMPT_DEUDA_ACT) - (TmpCredito.PR_CF_IMPT_DEUDA_ACT * (Val(LblCat_Ne_Desc_Max.Text.Replace("%", "")) / 100)), 2) & " Ni Mayor A " & Math.Round((TmpCredito.PR_CF_IMPT_DEUDA_ACT) - (TmpCredito.PR_CF_IMPT_DEUDA_ACT * (Val(LblCat_Ne_Desc_Min.Text.Replace("%", "")) / 100)), 2)
        '    ElseIf (TxtPagoInicial.Text + (TxtMontoPagos.Text * DdlCat_Ne_Num_Pagos.SelectedValue)) > Math.Round((TmpCredito.PR_CF_IMPT_DEUDA_ACT) - (TmpCredito.PR_CF_IMPT_DEUDA_ACT * (Val(LblCat_Ne_Desc_Min.Text.Replace("%", "")) / 100)), 2) Or (TxtPagoInicial.Text + (TxtMontoPagos.Text * DdlCat_Ne_Num_Pagos.SelectedValue)) < Math.Round((TmpCredito.PR_CF_IMPT_DEUDA_ACT) - (TmpCredito.PR_CF_IMPT_DEUDA_ACT * (Val(LblCat_Ne_Desc_Max.Text.Replace("%", "")) / 100)), 2) Then
        '        Return "La Suma De los Pagos No Puede Ser Menor A " & Math.Round((TmpCredito.PR_CF_IMPT_DEUDA_ACT) - (TmpCredito.PR_CF_IMPT_DEUDA_ACT * (Val(LblCat_Ne_Desc_Max.Text.Replace("%", "")) / 100)), 2) & " Ni Mayor A " & Math.Round((TmpCredito.PR_CF_IMPT_DEUDA_ACT) - (TmpCredito.PR_CF_IMPT_DEUDA_ACT * (Val(LblCat_Ne_Desc_Min.Text.Replace("%", "")) / 100)), 2)
        '    Else
        '        Return "1"
        '    End If
        'End If
    End Function


    Protected Sub BtnAceptarPromesa_Click(sender As Object, e As EventArgs) Handles BtnAceptarPromesa.Click
        Dim TmpCredito As credito = CType(Session("Credito"), credito)
        Dim TmpUsr As USUARIO = CType(Session("USUARIO"), USUARIO)
        If TmpUsr.CAT_LO_PGESTION.ToString.Substring(6, 1) = 1 Then
            If TxtHist_Pr_Motivo.Text.Length < 10 Then
                LblMsj.Text = "Capture Un Comentario Valido"
                MpuMensajes.Show()
            Else
                Class_MasterPage.CancelarPP(TxtHist_Pr_Motivo.Text & "," & TmpCredito.PR_MC_CREDITO, TmpUsr.CAT_LO_USUARIO, 9)
                PnlNeGociacion.Visible = True
                PnlNegoVigente.Visible = False
                Llenar()
            End If
        Else
            If TxtHist_Pr_Supervisor.Text = "" Then
                LblMsj.Text = "Capture Un Usuario"
                MpuMensajes.Show()
            ElseIf TxtContrasena.Text = "" Then
                LblMsj.Text = "Capture Una Contraseña"
                MpuMensajes.Show()
            ElseIf TxtHist_Pr_Motivo.Text.Length < 10 Then
                LblMsj.Text = "Capture Un Comentario Valido"
                MpuMensajes.Show()
            ElseIf Class_Negociaciones.LlenarElementosNego(TxtHist_Pr_Supervisor.Text, TxtContrasena.Text, "", 5).Tables("ELEMENTOS").Rows(0).Item("Permiso") = 0 Then
                LblMsj.Text = "Usuario O Contraseña Incorrectos O Usuario Sin Facultades"
                MpuMensajes.Show()
            Else
                Class_MasterPage.CancelarPP(TxtHist_Pr_Motivo.Text & "," & TmpCredito.PR_MC_CREDITO, TmpUsr.CAT_LO_USUARIO, 9)
                PnlNeGociacion.Visible = True
                PnlNegoVigente.Visible = False
                Llenar()
            End If
        End If
    End Sub

    Protected Sub TxtDescuento_TextChanged(sender As Object, e As EventArgs) Handles TxtDescuento.TextChanged
        Try
            TxtPagoInicial.Text = ""
            DdlCat_Ne_Num_Pagos.SelectedValue = "Seleccione"
        Catch ex As Exception
        End Try
        Dim TmpCredito As credito = CType(Session("Credito"), credito)
        Dim StrRangoSaldo As String = ""

        If DdlExhibiciones.SelectedValue = "Seleccione" Then
            LblMsj.Text = "Seleccione Una Opcion De Exhibicion"
            MpuMensajes.Show()
            Limpiar(2)
        Else

            If DdlCat_Ne_Nombre.SelectedValue = "CAT_NEGO_QUITAS" Then


                If Val(TxtDescuento.Text) > LblDescuentoMax.Text.Replace("%", "") Then
                    LblMsj.Text = "El Descuento No Puede Superar El " & LblDescuentoMax.Text
                    MpuMensajes.Show()
                    Limpiar(2)
                Else
                    TxtSaldoNegociado.Text = Math.Round(Val(TmpCredito.PR_CF_SALDO_TOT) * ((100 - Val(TxtDescuento.Text)) / (100)), 2)
                End If


            ElseIf DdlCat_Ne_Nombre.SelectedValue = "CAT_NEGO_PAGOS_FIJOS" Then

            End If
        End If
    End Sub

    Protected Sub TxtSaldoNegociado_TextChanged(sender As Object, e As EventArgs) Handles TxtSaldoNegociado.TextChanged

        Try
            TxtPagoInicial.Text = ""
            DdlCat_Ne_Num_Pagos.SelectedValue = "Seleccione"
        Catch ex As Exception
        End Try
        Dim TmpCredito As credito = CType(Session("Credito"), credito)
        Dim StrRangoSaldo As String = ""
        Dim IntSubDescuento As Double = 0
        Dim IntOperacion As Double = 0
        Dim IntUnoPorciento As Double = 0

        If DdlExhibiciones.SelectedValue = "Seleccione" Then
            LblMsj.Text = "Seleccione Una Opcion De Exhibicion"
            MpuMensajes.Show()
        Else
            If Val(TxtSaldoNegociado.Text) > Val(TmpCredito.PR_CF_SALDO_TOT) Then
                LblMsj.Text = "El Saldo Negociado No Puede Ser Mayor A $" & TmpCredito.PR_CF_SALDO_TOT
                MpuMensajes.Show()
                Limpiar(2)
            ElseIf Val(TxtSaldoNegociado.Text) < Val(LblMontoNegoMin.Text.Replace("$", "")) Then
                LblMsj.Text = "El Saldo Negociado No Puede Ser Menor A " & LblMontoNegoMin.Text
                MpuMensajes.Show()
                Limpiar(2)
            Else
                If DdlCat_Ne_Nombre.SelectedValue = "CAT_NEGO_QUITAS" Then
                    IntUnoPorciento = Val(TmpCredito.PR_CF_SALDO_TOT) / 100 ' =   1%
                    IntOperacion = Math.Round(((Val(TxtSaldoNegociado.Text)) / (IntUnoPorciento)), 2)
                    IntSubDescuento = 100 - IntOperacion
                    TxtDescuento.Text = Math.Round(IntSubDescuento, 2)
                    If Val(TxtDescuento.Text) > Val(LblDescuentoMax.Text.Replace("%", "")) Then
                        LblMsj.Text = "El Descuento Supera a " & LblDescuentoMax.Text & "Con El Monto De Esa Negociacion"
                        MpuMensajes.Show()
                        Limpiar(2)
                    End If

                ElseIf DdlCat_Ne_Nombre.SelectedValue = "CAT_NEGO_PAGOS_FIJOS" Then

                End If

            End If
        End If


    End Sub

    Private Sub DdlExhibiciones_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlExhibiciones.SelectedIndexChanged
        If DdlExhibiciones.SelectedValue <> "Seleccione" Then
            If DdlCat_Ne_Nombre.SelectedValue = "CAT_NEGO_QUITAS" Then

                TxtDescuento.Text = ""
                TxtSaldoNegociado.Text = ""
                Dim TmpCredito As credito = CType(Session("Credito"), credito)
                Dim StrRangoSaldo As String = ""
                Dim intDescuentoMax As Double = 0
                If Val(TmpCredito.PR_CF_SALDO_TOT) <= 50000 Then
                    StrRangoSaldo="$ 0 - $ 50000"
                                                       Else
                    StrRangoSaldo="> $ 50000"
                End If
                Dim DTSNegoF As DataSet = Class_Negociaciones.LlenarElementosNego_Fijos(DdlCat_Ne_Nombre.SelectedValue, TxtCartera.Text, "", TmpCredito.PR_CF_BUCKET, StrRangoSaldo, "", "", 1)



                If DdlExhibiciones.SelectedValue = "Quita en 1 exhibición" Then
                    intDescuentoMax = DTSNegoF.Tables(0).Rows(0).Item("CAT_NQ_QUITA_1EXHIBICION")
                    LblDescuentoMax.Text = intDescuentoMax & "%"
                    pagos(6)
                    LblMontoNegoMin.Text = "$" & Math.Round(Val(TmpCredito.PR_CF_SALDO_TOT) * ((100 - intDescuentoMax) / (100)), 2)
                    TxtDescuento.Text = intDescuentoMax
                    TxtSaldoNegociado.Text = Math.Round(Val(TmpCredito.PR_CF_SALDO_TOT) * ((100 - intDescuentoMax) / (100)), 2)
                ElseIf DdlExhibiciones.SelectedValue = "Quita + plazo(3 meses)" Then
                    intDescuentoMax = DTSNegoF.Tables(0).Rows(0).Item("CAT_NQ_QUITA_MAS_PLAZO")
                    LblDescuentoMax.Text = intDescuentoMax & "%"
                    pagos(12)
                    LblMontoNegoMin.Text = "$" & Math.Round(Val(TmpCredito.PR_CF_SALDO_TOT) * ((100 - intDescuentoMax) / (100)), 2)
                    TxtDescuento.Text = intDescuentoMax
                    TxtSaldoNegociado.Text = Math.Round(Val(TmpCredito.PR_CF_SALDO_TOT) * ((100 - intDescuentoMax) / (100)), 2)
                End If

            ElseIf DdlCat_Ne_Nombre.SelectedValue = "CAT_NEGO_PAGOS_FIJOS" Then


            End If
        Else
            TxtMontoReal.Text = ""
            LblDescuentoMax.Text = ""
            LblMontoNegoMin.Text = ""
            TxtDescuento.Text = ""
            TxtSaldoNegociado.Text = ""
            DdlCat_Ne_Num_Pagos.Items.Clear()
            DdlCat_Ne_Num_Pagos.DataBind()
        End If
    End Sub

    Shared Function FN_SEMANAL(ByVal Valor As Integer) As Integer
        Dim Retorno As Integer
        Valor = Valor - 1
        Valor = Valor * 7
        Retorno = Valor
        Return Retorno
    End Function
    Shared Function FN_QUINCENAL(ByVal Valor As Integer) As Integer
        Dim Retorno As Integer
        Valor = Valor - 1
        Valor = Valor * 15
        Retorno = Valor
        Return Retorno
    End Function

    Private Sub DdlPeriodicidad_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlPeriodicidad.SelectedIndexChanged
        If DdlPeriodicidad.SelectedValue <> "Seleccione" Then

            If DdlCat_Ne_Nombre.SelectedValue = "CAT_NEGO_PAGOS_FIJOS" Then
                'LblCat_Ne_Num_Pagos.Text

                Dim dtsp As DataSet = Class_Negociaciones.LlenarElementosNego_Fijos(DdlCat_Ne_Nombre.SelectedValue, "", "", DdlPeriodicidad.SelectedValue, "", "", "", 1)
                If dtsp.Tables(0).Rows.Count > 0 Then


                    DdlCat_Ne_Num_Pagos.DataTextField = "PERIODICIDAD"
                    DdlCat_Ne_Num_Pagos.DataValueField = "PERIODICIDAD"
                    DdlCat_Ne_Num_Pagos.DataSource = dtsp
                    'DdlCat_Ne_Num_Pagos.Items.Add("Seleccione")
                    'DdlCat_Ne_Num_Pagos.SelectedValue = "Seleccione"
                    DdlCat_Ne_Num_Pagos.DataBind()

                    Dim DteFechas As Date = System.DateTime.Today()
                    Dim DteFechaIni As Date = TxtDteLimiteNegociacion.Text
                    Dim Cont As Integer = 0
                    If DdlPeriodicidad.SelectedValue.ToString = "Mensual" Then
                        LblCat_Ne_Num_Pagos.Text = "Meses que otorga FINEM"
                        DteFechas = DteFechas.AddMonths(+1)
                        While DteFechas < DteFechaIni
                            Cont = Cont + 1
                            DteFechas = DteFechas.AddMonths(+1)
                        End While
                    ElseIf DdlPeriodicidad.SelectedValue.ToString = "Quincenal" Then
                        LblCat_Ne_Num_Pagos.Text = "Quincenas que otorga FINEM"
                        While DteFechas < DteFechaIni
                            DteFechas = DteFechas.AddDays(+1)
                            While (DteFechas.Day <> 15 And DteFechas.Day <> 30)
                                DteFechas = DteFechas.AddDays(+1)
                            End While
                            Cont = Cont + 1
                        End While
                    ElseIf DdlPeriodicidad.SelectedValue.ToString = "Semanal" Then
                        LblCat_Ne_Num_Pagos.Text = "Semanas que otorga FINEM"
                        While DteFechas < DteFechaIni
                            DteFechas = DteFechas.AddDays(+1)
                            While DteFechas.DayOfWeek.value__ <> 5
                                DteFechas = DteFechas.AddDays(+1)
                            End While
                            Cont = Cont + 1
                        End While
                    End If
                    If dtsp.Tables(0).Rows(9).Item("PERIODICIDAD").ToString <= Cont Then
                        Cont = dtsp.Tables(0).Rows(9).Item("PERIODICIDAD").ToString
                    ElseIf dtsp.Tables(0).Rows(9).Item("PERIODICIDAD").ToString > Cont And dtsp.Tables(0).Rows(8).Item("PERIODICIDAD").ToString >= Cont Then
                        Cont = dtsp.Tables(0).Rows(8).Item("PERIODICIDAD").ToString
                    ElseIf dtsp.Tables(0).Rows(8).Item("PERIODICIDAD").ToString > Cont And dtsp.Tables(0).Rows(7).Item("PERIODICIDAD").ToString >= Cont Then
                        Cont = dtsp.Tables(0).Rows(7).Item("PERIODICIDAD").ToString
                    ElseIf dtsp.Tables(0).Rows(7).Item("PERIODICIDAD").ToString > Cont And dtsp.Tables(0).Rows(6).Item("PERIODICIDAD").ToString >= Cont Then
                        Cont = dtsp.Tables(0).Rows(6).Item("PERIODICIDAD").ToString
                    ElseIf dtsp.Tables(0).Rows(6).Item("PERIODICIDAD").ToString > Cont And dtsp.Tables(0).Rows(5).Item("PERIODICIDAD").ToString >= Cont Then
                        Cont = dtsp.Tables(0).Rows(5).Item("PERIODICIDAD").ToString
                    ElseIf dtsp.Tables(0).Rows(5).Item("PERIODICIDAD").ToString > Cont And dtsp.Tables(0).Rows(4).Item("PERIODICIDAD").ToString >= Cont Then
                        Cont = dtsp.Tables(0).Rows(4).Item("PERIODICIDAD").ToString
                    ElseIf dtsp.Tables(0).Rows(4).Item("PERIODICIDAD").ToString > Cont And dtsp.Tables(0).Rows(3).Item("PERIODICIDAD").ToString >= Cont Then
                        Cont = dtsp.Tables(0).Rows(3).Item("PERIODICIDAD").ToString
                    ElseIf dtsp.Tables(0).Rows(3).Item("PERIODICIDAD").ToString > Cont And dtsp.Tables(0).Rows(2).Item("PERIODICIDAD").ToString >= Cont Then
                        Cont = dtsp.Tables(0).Rows(2).Item("PERIODICIDAD").ToString
                    ElseIf dtsp.Tables(0).Rows(2).Item("PERIODICIDAD").ToString > Cont And dtsp.Tables(0).Rows(1).Item("PERIODICIDAD").ToString >= Cont Then
                        Cont = dtsp.Tables(0).Rows(1).Item("PERIODICIDAD").ToString
                    ElseIf dtsp.Tables(0).Rows(1).Item("PERIODICIDAD").ToString > Cont And dtsp.Tables(0).Rows(0).Item("PERIODICIDAD").ToString >= Cont Then
                        Cont = dtsp.Tables(0).Rows(0).Item("PERIODICIDAD").ToString
                    End If

                        TxtNumPagos.Text = "Hasta " & Cont.ToString
                End If
            End If

        Else
            If DdlCat_Ne_Nombre.SelectedValue = "CAT_NEGO_PAGOS_FIJOS" Then
                DdlCat_Ne_Num_Pagos.Items.Clear()
                DdlCat_Ne_Num_Pagos.DataBind()
                TxtNumPagos.Text = ""
            End If
        End If



    End Sub
    Sub Enter(ByVal V_documento As Object)
        V_documento.Add(New Paragraph(Chr(13), FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.NORMAL)))
    End Sub


    Sub GenerarConvenio(ByVal TipoConvenio As String, ByVal Credito As String, ByVal Producto As String)
        Try
            Dim ruta As String = StrRuta() & "PDF"
            SubExisteRuta(ruta)

            Dim oraCommand As New OracleCommand
            oraCommand.CommandText = "SP_CONVENIOS"
            oraCommand.CommandType = CommandType.StoredProcedure
            oraCommand.Parameters.Add("V_Credito", OracleType.VarChar).Value = CType(Session("Credito"), credito).PR_MC_CREDITO
            oraCommand.Parameters.Add("V_HIST_CON_TIPO", OracleType.VarChar).Value = TipoConvenio
            oraCommand.Parameters.Add("V_BANDERA", OracleType.Number).Value = 1
            oraCommand.Parameters.Add("cv_1", OracleType.Cursor).Direction = ParameterDirection.Output
            Dim DtsPromesa As DataSet = Consulta_Procedure(oraCommand, "Gestion")
            Dim cuantos As Integer = DtsPromesa.Tables(0).Rows.Count

            If cuantos > 0 Then
                Dim Consecutivo As String = DtsPromesa.Tables(0).Rows(0).Item("NUMNFOLIO").ToString
                Dim NomPDF As String = "C" & Consecutivo & "-Carta_Convenio_" & TipoConvenio & "_" & CType(Session("Credito"), credito).PR_CD_NOMBRE & "_Cte-" & CType(Session("Credito"), credito).PR_MC_CREDITO & ".pdf"
                Dim Entrada = ruta + "\" + NomPDF

                If File.Exists(Entrada) Then
                    Kill(Entrada)
                End If

                Dim MatriculaEst As String = CType(Session("Credito"), credito).PR_CA_MATRICULA
                If MatriculaEst.Length < 6 Then
                    For Largo = MatriculaEst.Length To 6
                        Dim Matricula2 As String = "0" + MatriculaEst
                        MatriculaEst = Matricula2
                    Next
                ElseIf MatriculaEst.Length > 6 Then
                    MatriculaEst = MatriculaEst.Substring(MatriculaEst.Length - 6, 6)
                End If

                Dim FolioConvenio As String

                If TipoConvenio = "Quitas" Then

                    '------------------ Definiendo Folio ---------------------------
                    FolioConvenio = Consecutivo & "/" & CType(Session("Credito"), credito).PR_MC_CREDITO & Mid(TxtCartera.Text, 1, 3) & "/QU" & Math.Round(Val(TxtDescuento.Text), 0) & "/" &
                        Mid(DtsPromesa.Tables(0).Rows(0).Item("FechaCaptura").ToString, 1, 2) & Mid(DtsPromesa.Tables(0).Rows(0).Item("FechaCaptura").ToString, 4, 2)
                    FolioConvenio = UCase(FolioConvenio)

                    Dim Pie As New EventoTitulos


                    Dim doc As iTextSharp.text.Document = New iTextSharp.text.Document(iTextSharp.text.PageSize.LETTER, 50, 50, 70, 0)
                    '---------------------- Contraseña para acceso a PDF -------------------------------------------------
                    'Creando instacia para cifrar PDF
                    Dim writer As PdfWriter = PdfWriter.GetInstance(doc, New FileStream(Entrada, FileMode.Create))
                    Dim Upass As String = MatriculaEst
                    Dim OPass As String = MatriculaEst
                    Dim userpass() As Byte = Encoding.ASCII.GetBytes(Upass)
                    Dim ownerpass() As Byte = Encoding.ASCII.GetBytes(OPass)
                    writer.SetEncryption(userpass, ownerpass, 255, True)
                    '------------------------------------------------------------------------------------------------------
                    doc.Open() 'Abriendo Documento pra su edicion
                    '----------------------- Tamaños de fuente ------------------------------------------------------------
                    Dim FuenteNormal As iTextSharp.text.Font = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL)
                    Dim FuenteBold As iTextSharp.text.Font = FontFactory.GetFont("Arial", 9, iTextSharp.text.Font.BOLD)
                    Dim FuentePequeña As iTextSharp.text.Font = FontFactory.GetFont("Arial", 6, iTextSharp.text.Font.NORMAL)
                    Dim FuneteTNormal As iTextSharp.text.Font = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.BOLD)
                    Dim FuneteTBlanca As iTextSharp.text.Font = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.BOLD, Color.WHITE)
                    Dim FuenteFinem As iTextSharp.text.Font = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLUE)

                    '------------------------ Imagenes Encabezados -------------------------------------------------------

                    Dim imageFilePath As String = Server.MapPath(".") & "\Imagenes\RecuadrosFINEM.png" 'Cuadros
                    Dim jpg As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(imageFilePath)
                    jpg.ScaleToFit(100, 90)
                    jpg.Alignment = iTextSharp.text.Image.UNDERLYING
                    jpg.SetAbsolutePosition(10, 730)
                    doc.Add(jpg)

                    imageFilePath = Server.MapPath(".") & "\Imagenes\Imglogo_Cl.png" 'Logo
                    jpg = iTextSharp.text.Image.GetInstance(imageFilePath)
                    jpg.ScaleToFit(90, 70)
                    jpg.Alignment = iTextSharp.text.Image.UNDERLYING
                    jpg.SetAbsolutePosition(300, 720)
                    doc.Add(jpg)

                    'Dim asd As New iTextSharp.text.HeaderFooter(New PdfImage(jpg,"Im",)
                    '------------------------------------------ Carta -------------------------------------------------------
                    Dim P1 As New Paragraph()
                    P1.Alignment = Element.ALIGN_RIGHT
                    P1.Add(New Chunk("Ciudad de México, a, " & Format(Now, "Long Date") & "", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD)))
                    doc.Add(P1)

                    'Enter(doc)
                    Enter(doc)
                    ''--------------------- Tabla Saludos ------------------------------------ Anidar tabas ????
                    'Dim table0 As New PdfPTable(2)
                    'table0.TotalWidth = 600
                    'table0.LockedWidth = True
                    'Dim p As Phrase = New Phrase("TITULAR: ", FuenteNormal)
                    'Dim p2 As Phrase = New Phrase(DtsPromesa.Tables(0).Rows(0).Item("PR_CD_NOMBRE").ToString, FuenteBold)
                    'Dim Cel0_0 As New PdfPCell(p)
                    'Cel0_0.HorizontalAlignment = 1 '0=Left, 1=Centre, 2=Right
                    'Cel0_0.Border = 0
                    'table0.AddCell(Cel0_0)

                    Dim P2 As New Paragraph()
                    P2.Alignment = Element.ALIGN_LEFT
                    P2.Add(New Chunk("TITULAR: ", FuenteNormal))
                    P2.Add(New Chunk(DtsPromesa.Tables(0).Rows(0).Item("PR_CD_NOMBRE").ToString, FuenteBold))
                    doc.Add(P2)
                    Dim P2_1 As New Paragraph()
                    P2_1.Alignment = Element.ALIGN_LEFT
                    P2_1.Add(New Chunk("CUENTA: ", FuenteNormal))
                    P2_1.Add(New Chunk(DtsPromesa.Tables(0).Rows(0).Item("CREDITO").ToString & "              ", FuenteBold))
                    P2_1.Add(New Chunk("                                                                                                                     FOLIO: ", FuenteNormal))
                    P2_1.Add(New Chunk(FolioConvenio, FuenteBold))
                    doc.Add(P2_1)
                    Dim P2_1_1 As New Paragraph()
                    P2_1_1.Alignment = Element.ALIGN_LEFT
                    P2_1_1.Add(New Chunk("RESUMEN DE SALDO(1)", FuenteNormal))
                    doc.Add(P2_1_1)
                    Dim P2_2 As New Paragraph()
                    P2_2.Alignment = Element.ALIGN_RIGHT
                    P2_2.Add(New Chunk("SALDO TOTAL: ", FuenteBold))
                    P2_2.Add(New Chunk("$" & DtsPromesa.Tables(0).Rows(0).Item("SalTotFondos").ToString, FuenteBold))
                    doc.Add(P2_2)
                    Dim P2_2_1 As New Paragraph()
                    P2_2_1.Alignment = Element.ALIGN_LEFT
                    P2_2_1.Add(New Chunk("CONVENIO(2)", FuenteNormal))
                    doc.Add(P2_2_1)
                    Enter(doc)

                    Dim P3 As New Paragraph()
                    P3.Alignment = Element.ALIGN_JUSTIFIED
                    P3.Add(New Chunk("Yo ", FuenteNormal))
                    P3.Add(New Chunk(DtsPromesa.Tables(0).Rows(0).Item("PR_CD_NOMBRE").ToString, FuenteBold))
                    P3.Add(New Chunk(" reconozco el saldo de ", FuenteNormal))
                    P3.Add(New Chunk("$" & DtsPromesa.Tables(0).Rows(0).Item("SalTotFondos").ToString & " (" & DtsPromesa.Tables(0).Rows(0).Item("SalTotFondosLetra").ToString & ") ", FuenteBold))
                    P3.Add(New Chunk(" del credito educativo otorgado por Financiera Educativa de México, S.A. de C.V., SOFOM E.N.R., y acepto el ACUERDO CON QUITA PARA LIQUIDAR MI DEUDA CON UNA CONDONACIÓN DEL ", FuenteNormal))
                    P3.Add(New Chunk(TxtDescuento.Text.ToString & "% ", FuenteBold))
                    P3.Add(New Chunk("comprometiendome a pagar en ", FuenteNormal))
                    P3.Add(New Chunk(DdlCat_Ne_Num_Pagos.SelectedItem.ToString, FuenteBold))
                    P3.Add(New Chunk(" exhibición(es), la cantidad de ", FuenteNormal))
                    P3.Add(New Chunk("$" & DtsPromesa.Tables(0).Rows(0).Item("SaldoNego").ToString & " (" & DtsPromesa.Tables(0).Rows(0).Item("SaldoNegoLetra").ToString & ") ", FuenteBold))
                    P3.Add(New Chunk(" realizando el pago inicial el día ", FuenteNormal))
                    P3.Add(New Chunk(DtsPromesa.Tables(0).Rows(0).Item("FECHA_PROMESA_Largo").ToString, FuenteBold))
                    P3.Add(New Chunk(". El último pago se realizará el día ", FuenteNormal))
                    P3.Add(New Chunk(DtsPromesa.Tables(0).Rows(cuantos - 1).Item("FECHA_PROMESA_Largo").ToString, FuenteBold))
                    P3.Add(New Chunk(" quedando liquidada la cuenta con quita de saldo de ", FuenteNormal))
                    P3.Add(New Chunk("$" & DtsPromesa.Tables(0).Rows(0).Item("SaldoCondonado").ToString & " (", FuenteBold))
                    P3.Add(New Chunk(DtsPromesa.Tables(0).Rows(0).Item("SaldoCondLetra").ToString & ") ", FuenteBold))
                    P3.Add(New Chunk("y apruebo que de no cumplir puntualmente con el pago se dará por terminado dicho compromiso y se me requerirá por la vía legal el pago total sin ningún tipo de bonificación y con los intereses generados al día. ", FuenteNormal))

                    doc.Add(P3)
                    Enter(doc)




                    '---------------------- Tabla resumen de convenio -----------------------------------------------------------------------
                    Dim P3_1 As New Paragraph()
                    P3_1.Alignment = Element.ALIGN_LEFT
                    P3_1.Add(New Chunk("RESUMEN DE CONVENIO ", FuenteBold))
                    doc.Add(P3_1)
                    Enter(doc)


                    Dim T_Tit As Integer = 8
                    Dim T_Txt As Integer = 7
                    Dim table As New PdfPTable(2)
                    table.TotalWidth = 400
                    table.LockedWidth = True
                    Dim ColCel As Color = New Color(0, 0, 102)

                    Dim Celda0_0 As New PdfPCell(New Phrase("SALDO TOTAL", FuneteTBlanca))
                    Celda0_0.HorizontalAlignment = 0 '0=Left, 1=Centre, 2=Right
                    Celda0_0.BackgroundColor = ColCel
                    table.AddCell(Celda0_0)
                    Dim Celda0_1 As New PdfPCell(New Phrase("$" & DtsPromesa.Tables(0).Rows(0).Item("SalTotFondos").ToString, FuneteTBlanca))
                    Celda0_1.HorizontalAlignment = 2
                    Celda0_1.BackgroundColor = ColCel
                    table.AddCell(Celda0_1)
                    Dim Celda1_0 As New PdfPCell(New Phrase("CANTIDAD A PAGAR", FuneteTNormal))
                    Celda1_0.HorizontalAlignment = 0
                    table.AddCell(Celda1_0)
                    Dim Celda1_1 As New PdfPCell(New Phrase("$" & DtsPromesa.Tables(0).Rows(0).Item("SaldoNego").ToString, FuneteTNormal))
                    Celda1_1.HorizontalAlignment = 2
                    table.AddCell(Celda1_1)
                    Dim Celda2_0 As New PdfPCell(New Phrase("PORCENTAJE DE BONIFICACIÓN", FuneteTBlanca))
                    Celda2_0.HorizontalAlignment = 0
                    Celda2_0.BackgroundColor = ColCel
                    table.AddCell(Celda2_0)
                    Dim Celda2_1 As New PdfPCell(New Phrase(TxtDescuento.Text.ToString & "%", FuneteTBlanca))
                    Celda2_1.HorizontalAlignment = 2
                    Celda2_1.BackgroundColor = ColCel
                    table.AddCell(Celda2_1)
                    Dim Celda3_0 As New PdfPCell(New Phrase("CANTIDAD BONIFICADA", FuneteTNormal))
                    Celda3_0.HorizontalAlignment = 0
                    table.AddCell(Celda3_0)
                    Dim Celda3_1 As New PdfPCell(New Phrase("$" & DtsPromesa.Tables(0).Rows(0).Item("SaldoCondonado").ToString, FuneteTNormal))
                    Celda3_1.HorizontalAlignment = 2
                    table.AddCell(Celda3_1)
                    Dim Celda4_0 As New PdfPCell(New Phrase("EXHIBICIONES", FuneteTBlanca))
                    Celda4_0.HorizontalAlignment = 0
                    Celda4_0.BackgroundColor = ColCel
                    table.AddCell(Celda4_0)
                    Dim Celda4_1 As New PdfPCell(New Phrase(DtsPromesa.Tables(0).Rows(0).Item("Maximo").ToString, FuneteTBlanca))
                    Celda4_1.HorizontalAlignment = 2
                    Celda4_1.BackgroundColor = ColCel
                    table.AddCell(Celda4_1)
                    Dim Celda5_0 As New PdfPCell(New Phrase("FECHA DE PAGO INICIAL(3)", FuneteTNormal))
                    Celda5_0.HorizontalAlignment = 0
                    table.AddCell(Celda5_0)
                    Dim Celda5_1 As New PdfPCell(New Phrase(DtsPromesa.Tables(0).Rows(0).Item("FECHA_PROMESA_Largo").ToString, FuneteTNormal))
                    Celda5_1.HorizontalAlignment = 2
                    table.AddCell(Celda5_1)
                    Dim Celda6_0 As New PdfPCell(New Phrase("FECHA DE ÚLTIMO PAGO(3)", FuneteTBlanca))
                    Celda6_0.HorizontalAlignment = 0
                    Celda6_0.BackgroundColor = ColCel
                    table.AddCell(Celda6_0)
                    Dim Celda6_1 As New PdfPCell(New Phrase(DtsPromesa.Tables(0).Rows(cuantos - 1).Item("FECHA_PROMESA_Largo").ToString, FuneteTBlanca))
                    Celda6_1.HorizontalAlignment = 2
                    Celda6_1.BackgroundColor = ColCel
                    table.AddCell(Celda6_1)
                    Dim Celda7_0 As New PdfPCell(New Phrase("MONTO DE PAGO TOTAL", FuneteTNormal))
                    Celda7_0.HorizontalAlignment = 0
                    table.AddCell(Celda7_0)
                    Dim Celda7_1 As New PdfPCell(New Phrase("$" & DtsPromesa.Tables(0).Rows(0).Item("SaldoNego").ToString, FuneteTNormal))
                    Celda7_1.HorizontalAlignment = 2
                    table.AddCell(Celda7_1)
                    Dim Celda8_0 As New PdfPCell(New Phrase("TIPO DE CONVENIO", FuneteTBlanca))
                    Celda8_0.HorizontalAlignment = 0
                    Celda8_0.BackgroundColor = ColCel
                    table.AddCell(Celda8_0)
                    Dim Celda8_1 As New PdfPCell(New Phrase("Quita", FuneteTBlanca))
                    Celda8_1.HorizontalAlignment = 2
                    Celda8_1.BackgroundColor = ColCel
                    table.AddCell(Celda8_1)

                    doc.Add(table)
                    Enter(doc)
                    '------------------------------- Letras Chiquitas --------------------------------------------------------------------------
                    Dim interlineado As Double = 7
                    Dim P5 As New Paragraph()
                    P5.Alignment = Element.ALIGN_LEFT
                    P5.Add(New Chunk("INFORMACION PARA USTED", FuenteNormal))
                    doc.Add(P5)
                    Enter(doc)
                    Dim P5_1 As New Paragraph()
                    P5_1.Alignment = Element.ALIGN_JUSTIFIED
                    P5_1.Leading = interlineado
                    P5_1.Add(New Chunk("Este documento respalda una negociación para la liquidación de su cuenta, la cancelación de la misma está condicionada al pago en la fecha que en el mismo se detallan. ", FuentePequeña))
                    P5_1.Add(New Chunk("Cumpliendo con el pago en tiempo y forma se cancelara la cuenta para que usted pueda solicitar el envío de sus pagares cancelados, ", FuentePequeña))
                    P5_1.Add(New Chunk("carta finiquito y actualización el estatus de esta cuenta en las Sociedades de Información Crediticia como cuenta liquidada negoció el adeudo remanente con base en una quita, ", FuentePequeña))
                    P5_1.Add(New Chunk("condonación o descuento a solicitud del cliente y/o un convenio de finiquito.", FuentePequeña))
                    doc.Add(P5_1)
                    Dim P5_2 As New Paragraph()
                    P5_2.Alignment = Element.ALIGN_JUSTIFIED
                    P5_2.Leading = interlineado
                    P5_2.Add(New Chunk("Unidad Especializada de Atencion a Usuarios FINEM (UNE) Calzada de la Naranja No. 159, Fraccionamiento Industrial Alce Blanco,  Naucalpan, Estado de México, C.P. 53370, ", FuentePequeña))
                    P5_2.Add(New Chunk("une@finem.com.mx Teléfono (55) 3088 3854 ", FuentePequeña))
                    P5_2.Add(New Chunk("de Lunes a Viernes de 9:00 a 14:00 hrs.", FuentePequeña))
                    doc.Add(P5_2)
                    Dim P5_3 As New Paragraph()
                    P5_3.Alignment = Element.ALIGN_JUSTIFIED
                    P5_3.Leading = interlineado
                    P5_3.Add(New Chunk("(1) El pago o los pagos realizados disminuyen al saldo total y su segmentación expuesta.", FuentePequeña))
                    doc.Add(P5_3)
                    Dim P5_4 As New Paragraph()
                    P5_4.Alignment = Element.ALIGN_JUSTIFIED
                    P5_4.Leading = interlineado
                    P5_4.Add(New Chunk("(2) Los pagos se realizan únicamente en las sucursales bancarias y establecimientos autorizados.: BBVA Bancomer con el número de convenio y referencia que le fue proporcionado. Conserve su ticket de pago para futuras consultas, aclaraciones y reclamaciones, repórtelo de inmediato a nuestra Línea FINEM (55) 3088 3851 o al correo electrónico atencionaclientes@finem.com.mx", FuentePequeña))
                    doc.Add(P5_4)
                    Dim P5_5 As New Paragraph()
                    P5_5.Alignment = Element.ALIGN_JUSTIFIED
                    P5_5.Leading = interlineado
                    P5_5.Add(New Chunk("(3) Si la fecha de pago corresponde a un día inhábil bancario, el pago lo podrá realizar al siguiente día hábil bancario o en su defecto en los centros comerciales ya citados.", FuentePequeña))
                    doc.Add(P5_5)

                    Enter(doc)
                    '--------------------------- Tabla de Firmas -----------------------------------------------------------
                    Dim table2 As New PdfPTable(2)
                    table2.TotalWidth = 600
                    table2.LockedWidth = True

                    Dim Celd0_0 As New PdfPCell(New Phrase("CONFORME", FuenteNormal))
                    Celd0_0.HorizontalAlignment = 1 '0=Left, 1=Centre, 2=Right
                    Celd0_0.Border = 0
                    table2.AddCell(Celd0_0)
                    Dim Celd0_1 As New PdfPCell(New Phrase("Financiera Educativa de México, S.A. de C.V., SOFOM E.N.R", FuenteFinem))
                    Celd0_1.HorizontalAlignment = 1 '0=Left, 1=Centre, 2=Right
                    Celd0_1.Border = 0
                    table2.AddCell(Celd0_1)
                    Dim Celd1_1 As New PdfPCell(New Phrase("C. " & DtsPromesa.Tables(0).Rows(0).Item("PR_CD_NOMBRE").ToString, FuenteNormal))
                    Celd1_1.HorizontalAlignment = 1 '0=Left, 1=Centre, 2=Right
                    Celd1_1.Border = 0
                    table2.AddCell(Celd1_1)

                    Dim imageFilePath1 As String = Server.MapPath(".") & "\Imagenes\FirmaCartasConvenio.png" 'Firma
                    Dim jpg1 As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(imageFilePath1)
                    jpg1.ScaleToFit(160, 190)
                    jpg1.Alignment = iTextSharp.text.Image.UNDERLYING
                    Dim Celd1_0 As New PdfPCell(jpg1)
                    Celd1_0.HorizontalAlignment = 1 '0=Left, 1=Centre, 2=Right
                    Celd1_0.Border = 0
                    table2.AddCell(Celd1_0)


                    Dim Celd2_0 As New PdfPCell(New Phrase("Cuenta: " & DtsPromesa.Tables(0).Rows(0).Item("CREDITO").ToString, FuenteNormal))
                    Celd2_0.HorizontalAlignment = 1 '0=Left, 1=Centre, 2=Right
                    Celd2_0.Border = 0
                    table2.AddCell(Celd2_0)
                    Dim Celd2_1 As New PdfPCell(New Phrase("__________________________________", FuenteBold))
                    Celd2_1.HorizontalAlignment = 1 '0=Left, 1=Centre, 2=Right
                    Celd2_1.Border = 0
                    table2.AddCell(Celd2_1)
                    Dim Celd3_0 As New PdfPCell(New Phrase(" ", FuenteBold))
                    Celd3_0.HorizontalAlignment = 1 '0=Left, 1=Centre, 2=Right
                    Celd3_0.Border = 0
                    table2.AddCell(Celd3_0)
                    Dim Celd3_1 As New PdfPCell(New Phrase("GABRIEL ANTONIO JUAREZ P", FuenteNormal))
                    Celd3_1.HorizontalAlignment = 1 '0=Left, 1=Centre, 2=Right
                    Celd3_1.Border = 0
                    table2.AddCell(Celd3_1)
                    Dim Celd4_0 As New PdfPCell(New Phrase(" ", FuenteBold))
                    Celd4_0.HorizontalAlignment = 1 '0=Left, 1=Centre, 2=Right
                    Celd4_0.Border = 0
                    table2.AddCell(Celd4_0)
                    Dim Celd4_1 As New PdfPCell(New Phrase("GERENTE DE ATENCIÓN A CLIENTES Y COBRANZA", FuenteNormal))
                    Celd4_1.HorizontalAlignment = 1 '0=Left, 1=Centre, 2=Right
                    Celd4_1.Border = 0
                    table2.AddCell(Celd4_1)
                    doc.Add(table2)
                    Enter(doc)



                    Dim P12 As New Paragraph
                    P12.Alignment = Element.ALIGN_CENTER
                    P12.Add(New Chunk("Recuerde que puede consultar nuestro aviso de privacidad en ", FontFactory.GetFont("Arial Narrow", 6, iTextSharp.text.Font.NORMAL)))
                    Dim under2 As Chunk = New Chunk("www.finem.com.mx ", FontFactory.GetFont("Arial Narrow", 6, iTextSharp.text.Font.NORMAL))
                    under2.SetUnderline(0.5F, -1.5F)
                    P12.Add(under2)
                    doc.Add(P12)
                    Enter(doc)
                    Enter(doc)

                    doc.Close()

                ElseIf TipoConvenio = "Pagos Fijos" Then 'Caso 2
                    '------------------ Definiendo Folio ---------------------------
                    FolioConvenio = Consecutivo & "/" & CType(Session("Credito"), credito).PR_MC_CREDITO & Mid(TxtCartera.Text, 1, 3) & "/PL" & Math.Round(Val(TxtDescuento.Text), 0) & "/" &
                        Mid(DtsPromesa.Tables(0).Rows(0).Item("FechaCaptura").ToString, 1, 2) & Mid(DtsPromesa.Tables(0).Rows(0).Item("FechaCaptura").ToString, 4, 2)
                    FolioConvenio = UCase(FolioConvenio)

                    '--------------------- Obteniendo Avales ---------------------------
                    Dim oraCommand2 As New OracleCommand
                    oraCommand2.CommandText = "SP_CONVENIOS"
                    oraCommand2.CommandType = CommandType.StoredProcedure
                    oraCommand2.Parameters.Add("V_Credito", OracleType.VarChar).Value = CType(Session("Credito"), credito).PR_MC_CREDITO
                    oraCommand2.Parameters.Add("V_BANDERA", OracleType.Number).Value = 2
                    oraCommand2.Parameters.Add("cv_1", OracleType.Cursor).Direction = ParameterDirection.Output
                    Dim DtsPromesa2 As DataSet = Consulta_Procedure(oraCommand2, "Gestion")

                    Dim documentoPDF As New Document
                    Dim Pie As New EventoTitulos

                    Dim doc As iTextSharp.text.Document = New iTextSharp.text.Document(iTextSharp.text.PageSize.LETTER, 50, 50, 70, 0)
                    '---------------------- Contraseña para acceso a PDF -------------------------------------------------
                    'Creando instacia para cifrar PDF
                    Dim writer As PdfWriter = PdfWriter.GetInstance(doc, New FileStream(Entrada, FileMode.Create))
                    Dim Upass As String = MatriculaEst
                    Dim OPass As String = MatriculaEst
                    Dim userpass() As Byte = Encoding.ASCII.GetBytes(Upass)
                    Dim ownerpass() As Byte = Encoding.ASCII.GetBytes(OPass)
                    writer.SetEncryption(userpass, ownerpass, 255, True)
                    '------------------------------------------------------------------------------------------------------
                    doc.Open() 'Abriondo Documento pra su edicion
                    '----------------------- Tamaños de fuente ------------------------------------------------------------
                    Dim FuenteNormal As iTextSharp.text.Font = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL)
                    Dim FuenteBold As iTextSharp.text.Font = FontFactory.GetFont("Arial", 9, iTextSharp.text.Font.BOLD)
                    Dim FuentePequeña As iTextSharp.text.Font = FontFactory.GetFont("Arial", 6, iTextSharp.text.Font.NORMAL)
                    Dim FuenteFinem As iTextSharp.text.Font = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLUE)

                    '------------------------ Imagenes Encabezados -------------------------------------------------------
                    Dim imageFilePath As String = Server.MapPath(".") & "\Imagenes\RecuadrosFINEM.png" 'Cuadros
                    Dim jpg As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(imageFilePath)
                    jpg.ScaleToFit(100, 90)
                    jpg.Alignment = iTextSharp.text.Image.UNDERLYING
                    jpg.SetAbsolutePosition(10, 730)
                    doc.Add(jpg)

                    imageFilePath = Server.MapPath(".") & "\Imagenes\Imglogo_Cl.png" 'Logo
                    jpg = iTextSharp.text.Image.GetInstance(imageFilePath)
                    jpg.ScaleToFit(80, 60)
                    jpg.Alignment = iTextSharp.text.Image.UNDERLYING
                    jpg.SetAbsolutePosition(300, 720)
                    doc.Add(jpg)
                    '------------------------------------------ Carta -------------------------------------------------------

                    Dim P1 As New Paragraph()
                    P1.Alignment = Element.ALIGN_RIGHT
                    P1.Add(New Chunk("Ciudad de México, a, " & Format(Now, "Long Date") & "", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD)))
                    doc.Add(P1)

                    'Enter(doc)
                    Enter(doc)

                    Dim P2 As New Paragraph()
                    P2.Alignment = Element.ALIGN_LEFT
                    P2.Add(New Chunk("TITULAR: ", FuenteNormal))
                    P2.Add(New Chunk(DtsPromesa.Tables(0).Rows(0).Item("PR_CD_NOMBRE").ToString, FuenteBold))
                    doc.Add(P2)
                    Dim P2_1 As New Paragraph()
                    P2_1.Alignment = Element.ALIGN_LEFT
                    P2_1.Add(New Chunk("CUENTA: ", FuenteNormal))
                    P2_1.Add(New Chunk(DtsPromesa.Tables(0).Rows(0).Item("CREDITO").ToString, FuenteBold))
                    P2_1.Add(New Chunk("                                                                                                                     FOLIO: ", FuenteNormal))
                    P2_1.Add(New Chunk(FolioConvenio, FuenteBold))
                    doc.Add(P2_1)
                    Dim P2_1_1 As New Paragraph()
                    P2_1_1.Alignment = Element.ALIGN_LEFT
                    P2_1_1.Add(New Chunk("RESUMEN DE SALDO(1)", FuenteNormal))
                    doc.Add(P2_1_1)
                    Dim P2_2 As New Paragraph()
                    P2_2.Alignment = Element.ALIGN_RIGHT
                    P2_2.Add(New Chunk("SALDO TOTAL: ", FuenteBold))
                    P2_2.Add(New Chunk("$" & DtsPromesa.Tables(0).Rows(0).Item("SalTotFondos").ToString, FuenteBold))
                    doc.Add(P2_2)
                    Dim P2_2_1 As New Paragraph()
                    P2_2_1.Alignment = Element.ALIGN_LEFT
                    P2_2_1.Add(New Chunk("CONVENIO(2)", FuenteNormal))
                    doc.Add(P2_2_1)
                    Enter(doc)

                    Dim P3 As New Paragraph()
                    P3.Alignment = Element.ALIGN_JUSTIFIED
                    P3.Add(New Chunk("Yo ", FuenteNormal))
                    P3.Add(New Chunk(DtsPromesa.Tables(0).Rows(0).Item("PR_CD_NOMBRE").ToString, FuenteBold))
                    If DtsPromesa2.Tables(0).Rows(0).Item("aval2") <> "NA" And DtsPromesa2.Tables(0).Rows(0).Item("aval2") <> "N/A" Then
                        P3.Add(New Chunk(" y mis avales ", FuenteNormal))
                        P3.Add(New Chunk(DtsPromesa2.Tables(0).Rows(0).Item("aval1").ToString & " y " & DtsPromesa2.Tables(0).Rows(0).Item("aval2").ToString & " ", FuenteBold))
                    Else
                        P3.Add(New Chunk(" y mi aval ", FuenteNormal))
                        P3.Add(New Chunk(DtsPromesa2.Tables(0).Rows(0).Item("aval1").ToString & " ", FuenteBold))
                    End If
                    P3.Add(New Chunk("reconocemos el saldo de ", FuenteNormal))
                    P3.Add(New Chunk("$" & DtsPromesa.Tables(0).Rows(0).Item("SalTotFondos").ToString & " (" & DtsPromesa.Tables(0).Rows(0).Item("SalTotFondosLetra").ToString & ") ", FuenteBold))
                    P3.Add(New Chunk(" del credito educativo otorgado por Financiera Educativa de México, S.A. de C.V., SOFOM E.N.R., y ", FuenteNormal))
                    P3.Add(New Chunk("aceptamos el PLAN DE PAGOS anexo en este escrito, comprometiéndonos a cumplir puntualmente con los pagos y ", FuenteNormal))
                    P3.Add(New Chunk("aprobamos que de no cumplir se dará por terminado dicho compromiso y se nos requerirá ", FuenteNormal))
                    P3.Add(New Chunk("por la vía legal el pago total sin ningún tipo de bonificación o plan de pagos y con los intereses generados al día.", FuenteNormal))
                    doc.Add(P3)
                    Enter(doc)

                    '---------------------- Tabla resumen de convenio -----------------------------------------------------------------------
                    Dim P3_1 As New Paragraph()
                    P3_1.Alignment = Element.ALIGN_CENTER
                    P3_1.Add(New Chunk("RESUMEN DE CONVENIO ", FuenteBold))
                    doc.Add(P3_1)
                    Enter(doc)

                    Dim T_Tit As Integer = 8
                    Dim T_Txt As Integer = 7
                    Dim table As New PdfPTable(4)
                    table.TotalWidth = 400
                    table.LockedWidth = True
                    Dim ColCel As Color = New Color(0, 0, 102)

                    Dim Celda0 As New PdfPCell(New Phrase("Número de Pago", FontFactory.GetFont("Arial", T_Tit, iTextSharp.text.Font.BOLD, Color.WHITE)))
                    Dim Celda1 As New PdfPCell(New Phrase("Saldo", FontFactory.GetFont("Arial", T_Tit, iTextSharp.text.Font.BOLD, Color.WHITE)))
                    Dim Celda2 As New PdfPCell(New Phrase("Fecha de pago", FontFactory.GetFont("Arial", T_Tit, iTextSharp.text.Font.BOLD, Color.WHITE)))
                    Dim Celda3 As New PdfPCell(New Phrase("$Pago", FontFactory.GetFont("Arial", T_Tit, iTextSharp.text.Font.BOLD, Color.WHITE)))

                    Celda0.HorizontalAlignment = 1 '0=Left, 1=Centre, 2=Right
                    Celda1.HorizontalAlignment = 1
                    Celda2.HorizontalAlignment = 1
                    Celda3.HorizontalAlignment = 1

                    Celda0.BackgroundColor = ColCel
                    Celda1.BackgroundColor = ColCel
                    Celda2.BackgroundColor = ColCel
                    Celda3.BackgroundColor = ColCel

                    table.AddCell(Celda0)
                    table.AddCell(Celda1)
                    table.AddCell(Celda2)
                    table.AddCell(Celda3)

                    Dim totalFilas As Integer = DtsPromesa.Tables(0).Rows.Count
                    Dim Saldo As Double = 0
                    Dim SaldoText As String
                    For ROWS = 0 To totalFilas - 1 'Recorrido para las filas del dataset

                        Dim celda As New PdfPCell(New Phrase((DtsPromesa.Tables(0).Rows(ROWS).Item("NumPago")), FuenteNormal))
                        celda.HorizontalAlignment = 1
                        table.AddCell(celda)
                        If ROWS = 0 Then
                            Saldo = Math.Round((DtsPromesa.Tables(0).Rows(ROWS).Item("CONVENIO") - DtsPromesa.Tables(0).Rows(ROWS).Item("MONTO_PROMESA")), 2)
                            SaldoText = Format(Saldo, "$ #,##0.00")
                            Dim celda4 As New PdfPCell(New Phrase(SaldoText, FuenteNormal))
                            celda4.HorizontalAlignment = 1
                            table.AddCell(celda4)
                        Else
                            Saldo = Math.Round((Saldo - DtsPromesa.Tables(0).Rows(ROWS).Item("MONTO_PROMESA")), 2)
                            SaldoText = Format(Saldo, "$ #,##0.00")
                            Dim celda4 As New PdfPCell(New Phrase(SaldoText, FuenteNormal))
                            celda4.HorizontalAlignment = 1
                            table.AddCell(celda4)
                        End If
                        Dim celda5 As New PdfPCell(New Phrase(DtsPromesa.Tables(0).Rows(ROWS).Item("FECHA_PROMESA"), FuenteNormal))
                        celda5.HorizontalAlignment = 1
                        table.AddCell(celda5)
                        Dim celda6 As New PdfPCell(New Phrase("$ " & DtsPromesa.Tables(0).Rows(ROWS).Item("MONTO_PROMESA"), FuenteNormal))
                        celda6.HorizontalAlignment = 1
                        table.AddCell(celda6)

                    Next
                    doc.Add(table)
                    Enter(doc)
                    '------------------------------- Letras Chiquitas --------------------------------------------------------------------------
                    Dim interlineado As Double = 7
                    Dim P5 As New Paragraph()
                    P5.Alignment = Element.ALIGN_LEFT
                    P5.Add(New Chunk("INFORMACION PARA USTED", FuenteNormal))
                    doc.Add(P5)
                    Enter(doc)
                    Dim P5_1 As New Paragraph()
                    P5_1.Alignment = Element.ALIGN_JUSTIFIED
                    P5_1.Leading = interlineado
                    P5_1.Add(New Chunk("Este documento respalda una negociación para la liquidación de su cuenta, la cancelación de la misma está condicionada al pago en la fecha que en el mismo se detallan. ", FuentePequeña))
                    P5_1.Add(New Chunk("Cumpliendo con el pago en tiempo y forma se cancelara la cuenta para que usted pueda solicitar el envío de sus pagares cancelados, ", FuentePequeña))
                    P5_1.Add(New Chunk("carta finiquito y actualización el estatus de esta cuenta en las Sociedades de Información Crediticia como cuenta liquidada negoció el adeudo remanente con base en una quita, ", FuentePequeña))
                    P5_1.Add(New Chunk("condonación o descuento a solicitud del cliente y/o un convenio de finiquito.", FuentePequeña))
                    doc.Add(P5_1)
                    Dim P5_2 As New Paragraph()
                    P5_2.Alignment = Element.ALIGN_JUSTIFIED
                    P5_2.Leading = interlineado
                    P5_2.Add(New Chunk("Unidad Especializada de Atencion a Usuarios FINEM (UNE) Calzada de la Naranja No. 159, Fraccionamiento Industrial Alce Blanco,  Naucalpan, Estado de México, C.P. 53370, ", FuentePequeña))
                    P5_2.Add(New Chunk("une@finem.com.mx Teléfono (55) 3088 3854 ", FuentePequeña))
                    P5_2.Add(New Chunk("de Lunes a Viernes de 9:00 a 14:00 hrs.", FuentePequeña))
                    doc.Add(P5_2)
                    Dim P5_3 As New Paragraph()
                    P5_3.Alignment = Element.ALIGN_JUSTIFIED
                    P5_3.Leading = interlineado
                    P5_3.Add(New Chunk("(1) El pago o los pagos realizados disminuyen al saldo total y su segmentación expuesta.", FuentePequeña))
                    doc.Add(P5_3)
                    Dim P5_4 As New Paragraph()
                    P5_4.Alignment = Element.ALIGN_JUSTIFIED
                    P5_4.Leading = interlineado
                    P5_4.Add(New Chunk("(2) Los pagos se realizan únicamente en las sucursales bancarias y establecimientos autorizados.: BBVA Bancomer con el número de convenio y referencia que le fue proporcionado. Conserve su ticket de pago para futuras consultas, aclaraciones y reclamaciones, repórtelo de inmediato a nuestra Línea FINEM (55) 3088 3851 o al correo electrónico atencionaclientes@finem.com.mx", FuentePequeña))
                    doc.Add(P5_4)
                    Dim P5_5 As New Paragraph()
                    P5_5.Alignment = Element.ALIGN_JUSTIFIED
                    P5_5.Leading = interlineado
                    P5_5.Add(New Chunk("(3) Si la fecha de pago corresponde a un día inhábil bancario, el pago lo podrá realizar al siguiente día hábil bancario o en su defecto en los centros comerciales ya citados.", FuentePequeña))
                    doc.Add(P5_5)

                    Enter(doc)
                    '--------------------------- Tabla de Firmas -----------------------------------------------------------
                    Dim table2 As New PdfPTable(2)
                    table2.TotalWidth = 600
                    table2.LockedWidth = True

                    Dim Celd0_0 As New PdfPCell(New Phrase("CONFORME", FuenteNormal))
                    Celd0_0.HorizontalAlignment = 1 '0=Left, 1=Centre, 2=Right
                    Celd0_0.Border = 0
                    table2.AddCell(Celd0_0)
                    Dim Celd0_1 As New PdfPCell(New Phrase("Financiera Educativa de México, S.A. de C.V., SOFOM E.N.R", FuenteFinem))
                    Celd0_1.HorizontalAlignment = 1 '0=Left, 1=Centre, 2=Right
                    Celd0_1.Border = 0
                    table2.AddCell(Celd0_1)
                    Dim Celd1_1 As New PdfPCell(New Phrase("C. " & DtsPromesa.Tables(0).Rows(0).Item("PR_CD_NOMBRE").ToString, FuenteNormal))
                    Celd1_1.HorizontalAlignment = 1 '0=Left, 1=Centre, 2=Right
                    Celd1_1.Border = 0
                    table2.AddCell(Celd1_1)

                    Dim imageFilePath1 As String = Server.MapPath(".") & "\Imagenes\FirmaCartasConvenio.png" 'Firma
                    Dim jpg1 As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(imageFilePath1)
                    jpg1.ScaleToFit(160, 190)
                    jpg1.Alignment = iTextSharp.text.Image.UNDERLYING
                    Dim Celd1_0 As New PdfPCell(jpg1)
                    Celd1_0.HorizontalAlignment = 1 '0=Left, 1=Centre, 2=Right
                    Celd1_0.Border = 0
                    table2.AddCell(Celd1_0)


                    Dim Celd2_0 As New PdfPCell(New Phrase("Cuenta " & DtsPromesa.Tables(0).Rows(0).Item("CREDITO").ToString, FuenteNormal))
                    Celd2_0.HorizontalAlignment = 1 '0=Left, 1=Centre, 2=Right
                    Celd2_0.Border = 0
                    table2.AddCell(Celd2_0)
                    Dim Celd2_1 As New PdfPCell(New Phrase("__________________________________", FuenteBold))
                    Celd2_1.HorizontalAlignment = 1 '0=Left, 1=Centre, 2=Right
                    Celd2_1.Border = 0
                    table2.AddCell(Celd2_1)
                    Dim Celd3_0 As New PdfPCell(New Phrase(" ", FuenteBold))
                    Celd3_0.HorizontalAlignment = 1 '0=Left, 1=Centre, 2=Right
                    Celd3_0.Border = 0
                    table2.AddCell(Celd3_0)
                    Dim Celd3_1 As New PdfPCell(New Phrase("GABRIEL ANTONIO JUAREZ P", FuenteNormal))
                    Celd3_1.HorizontalAlignment = 1 '0=Left, 1=Centre, 2=Right
                    Celd3_1.Border = 0
                    table2.AddCell(Celd3_1)
                    Dim Celd4_0 As New PdfPCell(New Phrase(" ", FuenteBold))
                    Celd4_0.HorizontalAlignment = 1 '0=Left, 1=Centre, 2=Right
                    Celd4_0.Border = 0
                    table2.AddCell(Celd4_0)
                    Dim Celd4_1 As New PdfPCell(New Phrase("GERENTE DE ATENCIÓN A CLIENTES Y COBRANZA", FuenteNormal))
                    Celd4_1.HorizontalAlignment = 1 '0=Left, 1=Centre, 2=Right
                    Celd4_1.Border = 0
                    table2.AddCell(Celd4_1)
                    doc.Add(table2)
                    Enter(doc)



                    Dim P12 As New Paragraph
                    P12.Alignment = Element.ALIGN_CENTER
                    P12.Add(New Chunk("Recuerde que puede consultar nuestro aviso de privacidad en ", FontFactory.GetFont("Arial Narrow", 6, iTextSharp.text.Font.NORMAL)))
                    Dim under2 As Chunk = New Chunk("www.finem.com.mx ", FontFactory.GetFont("Arial Narrow", 6, iTextSharp.text.Font.NORMAL))
                    under2.SetUnderline(0.5F, -1.5F)
                    P12.Add(under2)
                    doc.Add(P12)
                    Enter(doc)
                    Enter(doc)

                    doc.Close()


                End If


                If File.Exists(Entrada) Then
                    'Dim ioflujo As FileInfo = New FileInfo(Entrada)
                    'Response.Clear()
                    'Response.AddHeader("Content-Disposition", "attachment; filename=" + ioflujo.Name)
                    'Response.AddHeader("Content-Length", ioflujo.Length.ToString())
                    'Response.ContentType = "application/pdf"
                    'Response.WriteFile(Entrada)
                    'Response.End()




                    Dim oraCommand1 As New OracleCommand
                    oraCommand1.CommandText = "SP_CONFIGURACION_MAIL"
                    oraCommand1.CommandType = CommandType.StoredProcedure
                    'oraCommand1.Parameters.Add("V_SUCURSAL", OracleType.VarChar).Value = "" '(CType(Session("Usuario"), USUARIO)).cat_Lo_Num_Agencia
                    oraCommand1.Parameters.Add("v_bandera", OracleType.Number).Value = 3
                    oraCommand1.Parameters.Add("cv_1", OracleType.Cursor).Direction = ParameterDirection.Output
                    Dim DtsMail As DataSet = Consulta_Procedure(oraCommand1, "Mail")
                    Dim USUARIO As String = DtsMail.Tables(0).Rows(0).Item("CAT_CONF_USER")
                    Dim PASSWORD As String = DtsMail.Tables(0).Rows(0).Item("CAT_CONF_PWD")


                    Dim fs As Stream = File.OpenRead(Entrada)
                    Dim tempBuff(fs.Length) As Byte
                    fs.Read(tempBuff, 0, fs.Length)
                    fs.Close()
                    Dim conn As New OracleConnection(Conectando())
                    conn.Open()
                    Dim tx As OracleTransaction
                    tx = conn.BeginTransaction()


                    Dim cmd As New OracleCommand()
                    cmd = conn.CreateCommand()
                    cmd.Transaction = tx
                    cmd.CommandText = "declare xx blob; begin dbms_lob.createtemporary(xx, false, 0); :tempblob := xx; end;"
                    cmd.Parameters.Add(New OracleParameter("tempblob", OracleType.Blob)).Direction = ParameterDirection.Output
                    cmd.ExecuteNonQuery()
                    Dim tempLob As OracleLob
                    tempLob = cmd.Parameters(0).Value
                    tempLob.BeginBatch(OracleLobOpenMode.ReadWrite)
                    tempLob.Write(tempBuff, 0, tempBuff.Length)
                    tempLob.EndBatch()
                    cmd.Parameters.Clear()
                    cmd.CommandText = "SP_ADD_ARCHIVO_CONVENIO"
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.Add("V_HIST_CON_AUTORIZO", OracleType.VarChar).Value = (CType(Session("Usuario"), USUARIO)).CAT_LO_SUPERVISOR 'TxtHist_Pr_SupervisorAuto.Text
                    cmd.Parameters.Add("V_HIST_CON_USUARIO", OracleType.VarChar).Value = (CType(Session("Usuario"), USUARIO)).CAT_LO_USUARIO
                    cmd.Parameters.Add("V_HIST_CON_FOLIO", OracleType.VarChar).Value = FolioConvenio
                    cmd.Parameters.Add("V_HIST_CON_ENVIO", OracleType.VarChar).Value = USUARIO
                    cmd.Parameters.Add("V_HIST_CON_RECIBE", OracleType.VarChar).Value = DdlSeleccionaMail.SelectedValue 'DrlCorreo.SelectedValue
                    cmd.Parameters.Add("V_HIST_CON_AGN", OracleType.VarChar).Value = CType(Session("Credito"), credito).PR_MC_AGENCIA  'PR_CA_AGN
                    'cmd.Parameters.Add("V_HIST_CON_CAMP", OracleType.VarChar).Value = CType(Session("Credito"), credito).'PR_CA_TIPO_TARJETA
                    cmd.Parameters.Add("V_HIST_CON_CREDITO", OracleType.VarChar).Value = CType(Session("Credito"), credito).PR_MC_CREDITO
                    cmd.Parameters.Add("V_HIST_CON_NOMBRE", OracleType.VarChar).Value = CType(Session("Credito"), credito).PR_CD_NOMBRE
                    cmd.Parameters.Add("V_HIST_CON_PRODUCTO", OracleType.VarChar).Value = CType(Session("Credito"), credito).PR_MC_PRODUCTO
                    cmd.Parameters.Add("V_HIST_CON_DIASMORA", OracleType.VarChar).Value = "0"
                    'cmd.Parameters.Add("V_HIST_CON_DIASCORTE", OracleType.VarChar).Value = CType(Session("Credito"), credito).PR_CA_DIACORTE
                    'cmd.Parameters.Add("V_HIST_CON_CARTERA", OracleType.VarChar).Value = CType(Session("Credito"), credito).PR_CA_TIPO_TARJETA
                    cmd.Parameters.Add("V_HIST_CON_MTOTOTAL", OracleType.VarChar).Value = TxtPR_CF_SALDO_TOT.Text.Replace("$", "").Replace(",", "")
                    cmd.Parameters.Add("V_HIST_CON_MTONEGO", OracleType.VarChar).Value = TxtSaldoNegociado.Text
                    cmd.Parameters.Add("V_HIST_CON_NUM", OracleType.VarChar).Value = DdlCat_Ne_Num_Pagos.SelectedValue
                    cmd.Parameters.Add("V_HIST_CON_PORC", OracleType.VarChar).Value = TxtDescuento.Text.Replace("%", "")
                    cmd.Parameters.Add(New OracleParameter("V_HIST_CON_CONVENIO", OracleType.Blob)).Value = tempLob
                    cmd.Parameters.Add("V_HIST_CON_TIPO", OracleType.VarChar).Value = TipoConvenio
                    cmd.Parameters.Add("v_HIST_CON_NOMPDF", OracleType.VarChar).Value = NomPDF
                    cmd.ExecuteNonQuery()
                    tx.Commit()

                    conn.Close()

                    Dim msg As New System.Net.Mail.MailMessage()
                    'msg.[To].Add(DdlSeleccionaMail.SelectedValue)
                    msg.[To].Add("ahernandez@mastercollection.com.mx")
                    'msg.[CC].Add("cdaza@mastercollection.com.mx")
                    'msg.[CC].Add("uriel.lino@finem.com.mx")
                    msg.From = New MailAddress(USUARIO, "Financiera Educativa de México", System.Text.Encoding.UTF8)
                    msg.Subject = NomPDF
                    msg.SubjectEncoding = System.Text.Encoding.UTF8

                    Dim plainView = AlternateView.CreateAlternateViewFromString("This is my plain text content, viewable by those clients that don't support html", Nothing, "text/plain")

                    Dim v_Html As String = "<table style=""width: 100%"">" &
                                           "<tr>" &
                                           "<td>Buen día Sr.(Sra.) <strong>" & CType(Session("Credito"), credito).PR_CD_NOMBRE & "</strong> le hacemos llegar la Carta convenio.</td>" &
                                           "</tr>" &
                                           "<tr>" &
                                           "<td>" &
                                           "<hr />" &
                                           "</td>" &
                                           "</tr>" &
                                           "</table>" &
                                           "<img src=cid:FinemAviso>"

                    Dim htmlView = AlternateView.CreateAlternateViewFromString(v_Html, Nothing, "text/html")
                    Dim ruta_img As String = Server.MapPath(".") & "\Imagenes\FinemAviso.jpg"

                    Dim img_correo As New LinkedResource(ruta_img)
                    img_correo.ContentId = "FinemAviso"

                    htmlView.LinkedResources.Add(img_correo)

                    'msg.AlternateViews.Add(plainView)
                    msg.AlternateViews.Add(htmlView)

                    'Dim AltVwTextoPlano As AlternateView = AlternateView.CreateAlternateViewFromString("Buen día Sr.(Sra.) " & CType(Session("Credito"), credito).PR_CD_NOMBRE & " le hacemos llegar la Carta convenio.")
                    'Dim AltVwImagenHTML As AlternateView = AlternateView.CreateAlternateViewFromString("<img src=cid:ImagenAviso>", Nothing, "text/html")

                    'Dim LnkRsAviso As New LinkedResource("C:\MasterCollection\Finem\Portales\CWMCGestionFinem\Imagenes\FinemAviso.jpg")
                    'LnkRsAviso.ContentId = "ImagenAviso"

                    'AltVwImagenHTML.LinkedResources.Add(LnkRsAviso)

                    'msg.AlternateViews.Add(AltVwTextoPlano)
                    'msg.AlternateViews.Add(AltVwImagenHTML)
                    'Dim Body As String = "<html><body><p>Buen día Sr.(Sra.) " & CType(Session("Credito"), credito).PR_CD_NOMBRE & " le hacemos llegar la Carta convenio.</p>" &
                    '"<IMG SRC=""cid:\Imagenes\FinemAviso.jpg"">"
                    '"<p>  •  EN CASO DE QUE ESTE DE ACUERDO CON EL CONVENIO FAVOR DE REENVIARLO FIRMADO.</p>" &
                    '"<p> Saludos.</p>" &
                    '"<p> PD. Al realizar su pago y enviar la ficha por este medio le pido que en el apartado Asunto sea dirigido a : Lic." & DrlSupv.Text & "</p>"
                    'Body = Body & "</body></html>"
                    'msg.Body = Body

                    msg.Attachments.Add(New Attachment(Entrada))
                    msg.BodyEncoding = System.Text.Encoding.UTF8
                    msg.IsBodyHtml = True
                    Dim client As New SmtpClient()
                    client.Credentials = New System.Net.NetworkCredential(USUARIO, PASSWORD)
                    client.Port = DtsMail.Tables(0).Rows(0).Item("CAT_CONF_PUERTO")
                    client.Host = DtsMail.Tables(0).Rows(0).Item("CAT_CONF_HOST")
                    If DtsMail.Tables(0).Rows(0).Item("CAT_CONF_SSL") = 0 Then
                        client.EnableSsl = False
                    Else
                        client.EnableSsl = True
                    End If

                    Try
                        client.Send(msg)
                    Catch ex As System.Net.Mail.SmtpException
                        Ejecuta("insert into hist_errores (HIST_ER_ERROR,HIST_ER_FECHA,HIST_ER_PANTALLA) values('" & ex.ToString & "',SYSDATE,'Envio Mail Convenios')")

                        LblMsj.Text = "Ocurrio un Error al mandar el Convenio Via E-Mail"
                        MpuMensajes.Show()

                    End Try

                End If
            End If
        Catch ex As Exception
            Ejecuta("insert into hist_errores (HIST_ER_ERROR,HIST_ER_FECHA,HIST_ER_PANTALLA) values('" & ex.ToString & "',SYSDATE,'Generar_Convenio')")
            LblMsj.Text = "Ocurrio un Error al mandar el Convenio Via E-Mail"
            MpuMensajes.Show()
        End Try
    End Sub

    Protected Sub BtnGuardar_Click1(sender As Object, e As EventArgs) Handles BtnGuardar.Click

    End Sub
    Protected Sub TxtContrasenaAuto_TextChanged(sender As Object, e As EventArgs) Handles TxtContrasenaAuto.TextChanged

    End Sub
End Class
