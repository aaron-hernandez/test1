Imports System.Data
Imports System.Data.OracleClient
Imports Conexiones
Imports Funciones
Imports Db
Imports System.Net.Mail
Imports System.Net
Imports System.Net.Security


Partial Class EnvioCorreos
    Inherits System.Web.UI.UserControl
    Shared Plantillaoriginal As String
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Val(CType(Session("Usuario"), USUARIO).CAT_LO_MGESTION) = 0 Then
            Response.Redirect("LogIn.aspx")
        Else

            If Not IsPostBack Then
                LlenarPlantillas()
                If CType(Session("Credito"), credito).PR_MC_CREDITO <> "" Then
                    TxtPlantilla.Enabled = False
                    Dim dts As DataSet = Class_InformacionAdicional.LlenarElementosAgregar(CType(Session("Credito"), credito).PR_MC_CREDITO, 4)
                    If dts.Tables(0).Rows.Count > 0 Then
                        GVCorreosCredito.DataSource = dts
                        GVCorreosCredito.DataBind()
                        BtnEnviar.Visible = True
                    Else
                        BtnEnviar.Visible = False
                    End If
                End If
            End If
        End If
    End Sub

    Protected Sub BtnEnviar_Click(sender As Object, e As EventArgs) Handles BtnEnviar.Click

        Dim ToCorreosC As String = ""
        Dim CorreosC As Integer = 0
        Dim ToCorreosCLength As Integer = 0

        For Each gvRow As GridViewRow In GVCorreosCredito.Rows
            Dim chkSelect As CheckBox = DirectCast(gvRow.FindControl("CbxAgregar"), CheckBox)
            If chkSelect.Checked = True Then
                CorreosC = 1

                ToCorreosC = ToCorreosC & gvRow.Cells(1).Text.ToString & ","
            End If
        Next
        If CorreosC <> 0 Then
            ToCorreosC = ToCorreosC.Substring(0, ToCorreosC.Length - 1)
        End If
        If DdlPlantilla.SelectedValue = "Seleccione" Then
            LblMsj.Text = "Seleccione Una Plantilla"
            MpuMensajes.Show()
        ElseIf TxtPlantilla.Content = "" Then
            LblMsj.Text = "No Tiene Contenido Esa Plantilla"
            MpuMensajes.Show()
        ElseIf CorreosC = 0 Then
            LblMsj.Text = "Seleccione Por Lo Menos Un Correo Del Credito"
            MpuMensajes.Show()
        Else
            Dim oraCommand1 As New OracleCommand
            oraCommand1.CommandText = "SP_CONFIGURACION_MAIL"
            oraCommand1.CommandType = CommandType.StoredProcedure
            oraCommand1.Parameters.Add("v_bandera", OracleType.Number).Value = 3
            oraCommand1.Parameters.Add("cv_1", OracleType.Cursor).Direction = ParameterDirection.Output
            Dim DtsMail As DataSet = Consulta_Procedure(oraCommand1, "Mail")
            Dim USUARIO As String = DtsMail.Tables(0).Rows(0).Item("CAT_CONF_USER")
            If USUARIO = "Sin Correo Predeterminado, Contacta A Tu Administrador De Sistema" Then
                LblMsj.Text = USUARIO
                MpuMensajes.Show()
            Else
                Dim PASSWORD As String = DtsMail.Tables(0).Rows(0).Item("CAT_CONF_PWD")
                Dim SSL As Integer = DtsMail.Tables(0).Rows(0).Item("CAT_CONF_SSL")

                Dim msg As New System.Net.Mail.MailMessage()
                msg.[To].Add(ToCorreosC)
                'msg.[To].Add("ahernandez@mastercollection.com.mx")
                msg.From = New MailAddress(USUARIO, DtsMail.Tables(0).Rows(0).Item("CAT_CONF_RESPONSABLE"), System.Text.Encoding.UTF8)
                msg.Subject = DdlPlantilla.Text
                msg.SubjectEncoding = System.Text.Encoding.UTF8

                Dim oraCommandVALOR As New OracleCommand
                oraCommandVALOR.CommandText = "SP_ADD_CAT_PLANTILLAS_CORREO" '---Obtiene el valor que se va a utilizar en la sustitucion de las etiquetas
                oraCommandVALOR.CommandType = CommandType.StoredProcedure
                oraCommandVALOR.Parameters.Add("V_Cat_Pc_Id", OracleType.Number).Value = 0
                oraCommandVALOR.Parameters.Add("V_Cat_Pc_Nombre", OracleType.VarChar).Value = "PR_CA_MATRICULA"
                oraCommandVALOR.Parameters.Add("V_CAT_PC_CONFIGURACION", OracleType.Clob).Value = DBNull.Value
                oraCommandVALOR.Parameters.Add("V_CAT_PC_PRODUCTO", OracleType.VarChar).Value = CType(Session("Credito"), credito).PR_MC_CREDITO
                oraCommandVALOR.Parameters.Add("V_Bandera", OracleType.Number).Value = 7
                oraCommandVALOR.Parameters.Add("CV_1", OracleType.Cursor).Direction = ParameterDirection.Output
                Dim DtsBuscaValo As DataSet = Consulta_Procedure(oraCommandVALOR, "BuscaValor")

                Dim StrEncabezado As String = "<table align=""right"">" &
                                        "<tr align="" right"">" &
                                            "<td style="" text-align Right();font-weight: bold"">" &
                                                "FINANCIERA EDUCATIVA DE MEXICO" &
                                            "</td>" &
                                        "</tr>" &
                                    "</table>" &
                                    "<table>" &
                                        "<tr align=""left"">" &
                                            "<td>" &
                                                "<img src=cid:LogoFinem>" &
                                            "</td>" &
                                        "</tr>" &
                                        "<tr>" &
                                            "<td>" &
                                                "SR.(A) : " & DtsBuscaValo.Tables(0).Rows(0)("NOMBRE") &
                                            "</td>" &
                                        "</tr>" &
                                        "<tr>" &
                                            "<td>" &
                                                "Calle:" & DtsBuscaValo.Tables(0).Rows(0)("CALLE") &
                                            "</td>" &
                                        "</tr>" &
                                        "<tr>" &
                                            "<td>" &
                                                "Colonia:" & DtsBuscaValo.Tables(0).Rows(0)("COLONIA") &
                                            "</td>" &
                                        "</tr>" &
                                        "<tr>" &
                                            "<td>" &
                                                "Ciudad:" & DtsBuscaValo.Tables(0).Rows(0)("ESTADO") &
                                            "</td>" &
                                        "</tr>" &
                                        "<tr>" &
                                            "<td>" &
                                                "Delegación o Municipio:" & DtsBuscaValo.Tables(0).Rows(0)("DELEGACION") &
                                            "</td>" &
                                        "</tr>" &
                                        "<tr>" &
                                            "<td>" &
                                            "Código Postal:" & DtsBuscaValo.Tables(0).Rows(0)("CP") &
                                            "</td>" &
                                        "</tr>" &
                                        "<tr>" &
                                            "<td>" &
                                            "Estado : " & DtsBuscaValo.Tables(0).Rows(0)("ESTADO") &
                                            "</td>" &
                                        "</tr>" &
                                "</table>" &
                                "<table align=""right"">" &
                                    "<tr>" &
                                        "<td style="" font-weight:bold;text-align: Right"" >" &
                                            "Numero de Cuenta:" & CType(Session("Credito"), credito).PR_MC_CREDITO &
                                        "</td>" &
                                    "</tr>" &
                                    "<tr><td>&nbsp;</td></tr>" &
                                    "<tr><td>&nbsp;</td></tr>" &
                                "</table> <p><br><br><br><br></p>"

                Dim StrFinalCorreo As String = "<p align = ""center"">Le recordamos que puede leer nuestro anuncio de privacidad en <a href=""http://www.finem.com.mx/""> www.finem.com.mx </a> </p>" &
                                                "<p align = ""center"" > <b>ATENTAMENTE</b> <br><br></p>" &
                                                "<p align = ""center"" ><font color=""blue"">Financiera Educativa de México, S.A. de C.V. SOFOM. E.N.R.</font><br><br><br></p>" &
                                                "<table align = ""center"">" &
                                                    "<tr align=""right"">" &
                                                            "<td><p align = ""right""><img src=cid:CuadrosFinem align = ""right""><br></p> </td>" &
                                                    "</tr> " &
                                                    "<tr align=""ceter"">" &
                                                            "<p align = ""center"" ><b><font color=""blue""> ATENCION A CLIENTES </font></b>(55)30 88 38 51 </p>" &
                                                            "<p align = ""center"" >| Actualización | Cambio de Domicilio | Aclaraciones sobre su saldo | </p>" &
                                                            "<p align = ""center"" > Lunes a Viernes de 9:00 a 18:00 hrs.</p>" &
                                                            "<p align = ""center"" > <a href=""mailto:atencionaclientes@finem.com.mx"">atencionaclientes@finem.com.mx</a> </p>" &
                                                            "<p align = ""center"" > <a href="" http://www.finem.com.mx/""> www.finem.com.mx </a><br> </p></tr></table>"


                Try

                    Dim htmlview = AlternateView.CreateAlternateViewFromString(StrEncabezado + Plantillaoriginal, Nothing, "text/html")


                    Dim StrRutaLogo As String = Server.MapPath(".") & "\Imagenes\ImgLogo_Cl.png"
                    Dim StrRutaCuadros As String = Server.MapPath(".") & "\Imagenes\RecuadrosFINEM.png"
                    Dim LRLogoMail As New LinkedResource(StrRutaLogo)
                    Dim LRCuadroMail As New LinkedResource(StrRutaCuadros)
                    LRLogoMail.ContentId = "LogoFinem"
                    LRCuadroMail.ContentId = "CuadrosFinem"
                    htmlview.LinkedResources.Add(LRLogoMail)
                    htmlview.LinkedResources.Add(LRCuadroMail)


                    msg.AlternateViews.Add(htmlview)

                    msg.BodyEncoding = System.Text.Encoding.UTF8
                    msg.IsBodyHtml = True
                    Dim client As New SmtpClient()
                    client.Credentials = New System.Net.NetworkCredential(USUARIO, PASSWORD)
                    client.Port = DtsMail.Tables(0).Rows(0).Item("CAT_CONF_PUERTO")
                    client.Host = DtsMail.Tables(0).Rows(0).Item("CAT_CONF_HOST")
                    If SSL = 1 Then
                        client.EnableSsl = True
                    Else
                        client.EnableSsl = False
                    End If
                    Try
                        client.Send(msg)
                        Dim TmpUsr As USUARIO = CType(Session("USUARIO"), USUARIO)
                        Dim TmpCrdt As credito = CType(Session("Credito"), credito)
                        Dim TmpAplcc As Aplicacion = CType(Session("Aplicacion"), Aplicacion)


                        Dim oraCommandVALOR2 As New OracleCommand
                        oraCommandVALOR2.CommandText = "SP_ADD_GESTIONES_MASIVAS"
                        oraCommandVALOR2.CommandType = CommandType.StoredProcedure
                        oraCommandVALOR2.Parameters.Add("V_HIST_GE_CREDITO", OracleType.VarChar).Value = CType(Session("Credito"), credito).PR_MC_CREDITO
                        oraCommandVALOR2.Parameters.Add("V_HIST_GE_USUARIO", OracleType.VarChar).Value = TmpUsr.CAT_LO_USUARIO
                        oraCommandVALOR2.Parameters.Add("V_HIST_GE_COMENTARIO", OracleType.VarChar).Value = DdlPlantilla.SelectedValue
                        oraCommandVALOR2.Parameters.Add("V_HIST_GE_EMAIL", OracleType.VarChar).Value = ToCorreosC
                        oraCommandVALOR2.Parameters.Add("V_HIST_GE_CAMPANA", OracleType.VarChar).Value = CType(Session("Credito"), credito).PR_CA_CAMPANA
                        oraCommandVALOR2.Parameters.Add("V_HIST_GE_BUCKET", OracleType.VarChar).Value = CType(Session("Credito"), credito).PR_CF_BUCKET
                        Dim DtsBuscaValor As DataSet = Consulta_Procedure(oraCommandVALOR2, "AgregadoAlHistMasivas")

                        DdlPlantilla.Text = "Seleccione"
                        TxtPlantilla.Content = ""
                        DdlPlantilla.SelectedValue = "Seleccione"
                        LblMsj.Text = "Correo Enviado"
                        MpuMensajes.Show()
                    Catch ex As System.Net.Mail.SmtpException
                        Ejecuta("insert into hist_errores (ERRORS) values('" & ex.ToString & "')")
                        LblMsj.Text = "No Se Pudo Enviar El Corro, Configuración Del Correo Inválida."
                        MpuMensajes.Show()
                    End Try
                Catch ex As Exception
                    Ejecuta("insert into hist_errores (ERRORS) values('" & ex.ToString & "')")
                End Try
            End If
        End If
    End Sub

    Sub LlenarPlantillas()
        Dim oraCommand As New OracleCommand
        oraCommand.CommandText = "SP_ADD_CAT_PLANTILLAS_CORREO"
        oraCommand.CommandType = CommandType.StoredProcedure
        oraCommand.Parameters.Add("V_Cat_Pc_Id", OracleType.Number).Value = 0
        oraCommand.Parameters.Add("V_Cat_Pc_Nombre", OracleType.VarChar).Value = ""
        oraCommand.Parameters.Add("V_CAT_PC_CONFIGURACION", OracleType.Clob).Value = DBNull.Value
        oraCommand.Parameters.Add("V_CAT_PC_PRODUCTO", OracleType.VarChar).Value = ""
        oraCommand.Parameters.Add("V_Bandera", OracleType.Number).Value = 4
        oraCommand.Parameters.Add("CV_1", OracleType.Cursor).Direction = ParameterDirection.Output
        Dim DtsCorreo As DataSet = Consulta_Procedure(oraCommand, "Correo")
        If DtsCorreo.Tables(0).Rows.Count > 0 Then
            DdlPlantilla.DataTextField = "Nombre"
            DdlPlantilla.DataValueField = "Nombre"
            DdlPlantilla.DataSource = DtsCorreo.Tables(0)
            DdlPlantilla.DataBind()
            DdlPlantilla.Items.Add("Seleccione")
            DdlPlantilla.SelectedValue = "Seleccione"
        End If
    End Sub


    Private Sub DdlPlantilla_TextChanged(sender As Object, e As EventArgs) Handles DdlPlantilla.TextChanged

        If DdlPlantilla.SelectedValue = "Seleccione" Then
            TxtPlantilla.Content = ""
        Else


            Dim oraCommandDes As New OracleCommand
            oraCommandDes.CommandText = "SP_ADD_CAT_PLANTILLAS_CORREO"
            oraCommandDes.CommandType = CommandType.StoredProcedure
            oraCommandDes.Parameters.Add("V_Cat_Pc_Id", OracleType.Number).Value = 0
            oraCommandDes.Parameters.Add("V_Cat_Pc_Nombre", OracleType.VarChar).Value = DdlPlantilla.SelectedValue
            oraCommandDes.Parameters.Add("V_CAT_PC_CONFIGURACION", OracleType.Clob).Value = DBNull.Value
            oraCommandDes.Parameters.Add("V_CAT_PC_PRODUCTO", OracleType.VarChar).Value = ""
            oraCommandDes.Parameters.Add("V_Bandera", OracleType.Number).Value = 6
            oraCommandDes.Parameters.Add("CV_1", OracleType.Cursor).Direction = ParameterDirection.Output
            Dim dtsdesc As DataSet = Consulta_Procedure(oraCommandDes, "mail")
            Dim StrRemplazar As String = dtsdesc.Tables(0).Rows(0)("Cat_Pc_Configuracion").ToString.Replace("&lt;", "<").Replace("&gt;", ">")


            Dim oraCommandEti As New OracleCommand
            oraCommandEti.CommandText = "SP_CATALOGOS"
            oraCommandEti.CommandType = CommandType.StoredProcedure
            oraCommandEti.Parameters.Add("V_BANDERA", OracleType.Number).Value = 27
            oraCommandEti.Parameters.Add("CV_1", OracleType.Cursor).Direction = ParameterDirection.Output
            Dim dtsReplace As DataSet = Consulta_Procedure(oraCommandEti, "remplazar")


            For c As Integer = 0 To dtsReplace.Tables(0).Rows.Count - 1
                Dim oraCommandVALOR As New OracleCommand
                oraCommandVALOR.CommandText = "SP_ADD_CAT_PLANTILLAS_CORREO"
                oraCommandVALOR.CommandType = CommandType.StoredProcedure
                oraCommandVALOR.Parameters.Add("V_Cat_Pc_Id", OracleType.Number).Value = 0
                oraCommandVALOR.Parameters.Add("V_Cat_Pc_Nombre", OracleType.VarChar).Value = dtsReplace.Tables(0).Rows(c)("CAT_EC_CAMPOREAL")
                oraCommandVALOR.Parameters.Add("V_CAT_PC_CONFIGURACION", OracleType.Clob).Value = DBNull.Value
                oraCommandVALOR.Parameters.Add("V_CAT_PC_PRODUCTO", OracleType.VarChar).Value = CType(Session("Credito"), credito).PR_MC_CREDITO
                oraCommandVALOR.Parameters.Add("V_Bandera", OracleType.Number).Value = 7
                oraCommandVALOR.Parameters.Add("CV_1", OracleType.Cursor).Direction = ParameterDirection.Output
                Dim DtsBuscaValor As DataSet = Consulta_Procedure(oraCommandVALOR, "BuscaValor")
                StrRemplazar = StrRemplazar.Replace(dtsReplace.Tables(0).Rows(c)("CAT_EC_DESCRIPCION"), DtsBuscaValor.Tables(0).Rows(0)("VALOR"))
            Next

            Plantillaoriginal = StrRemplazar
            TxtPlantilla.Content = StrRemplazar
        End If

    End Sub

    Protected Sub CbxAllCorreos_CheckedChanged(sender As Object, e As EventArgs) Handles CbxAllCorreos.CheckedChanged

        For Each gvRow As GridViewRow In GVCorreosCredito.Rows
            Dim chkSelect As CheckBox = DirectCast(gvRow.FindControl("CbxAgregar"), CheckBox)
            validar(chkSelect, CbxAllCorreos.Checked)
        Next

    End Sub
    Sub validar(ByRef Objeto As Object, ByVal Estado As Boolean)
        If Objeto.visible = True And Estado = True Then
            Objeto.checked = True
        ElseIf Objeto.visible = False Then
            Objeto.checked = False
        Else
            Objeto.checked = False
        End If
    End Sub
End Class
