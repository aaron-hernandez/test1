Imports System.Data.OracleClient
Imports System.Data
Imports System.IO
Imports System.Net
Imports Db
Imports Funciones

Partial Class Documentos
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
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ScriptManager.GetCurrent(Page).RegisterPostBackControl(GridArchivos)
        ScriptManager.GetCurrent(Page).RegisterPostBackControl(GridConvenios)
        BtnEliminar.Attributes.Add("onclick", "this.disabled=true;" + Page.ClientScript.GetPostBackEventReference(BtnEliminar, "").ToString())

        Try
            If ValidaLicencias(300, LblCat_Lo_Usuario.Text, "", "", 1, "") = 1 Then
                If Val(CType(Session("Usuario"), USUARIO).CAT_LO_MGESTION) = 0 Then
                    Response.Redirect("LogIn.aspx")
                Else
                    If Not IsPostBack Then
                        If Not IsPostBack Then
                            If CType(Session("credito"), credito).PR_MC_CREDITO <> "" Then
                                llenar_grid()
                                llenar_grid2()
                                If (CType(Session("Usuario"), USUARIO)).CAT_LO_PGESTION.Substring(6, 1) = 1 Then
                                    PnlCargar.Visible = True
                                Else
                                    PnlCargar.Visible = False
                                End If

                                If (CType(Session("Usuario"), USUARIO)).CAT_LO_PGESTION.Substring(7, 1) = 1 Then
                                    BtnEliminar.Visible = True
                                Else
                                    BtnEliminar.Visible = False
                                End If
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

    Protected Sub llenar_grid()
        Try
            Dim oraCommandT As New OracleCommand
            oraCommandT.CommandText = "SP_HISTORICO_ARCHIVO"
            oraCommandT.CommandType = CommandType.StoredProcedure
            oraCommandT.Parameters.Add("V_HIST_DO_CREDITO", OracleType.VarChar).Value = CType(Session("credito"), credito).PR_MC_CREDITO
            oraCommandT.Parameters.Add("V_HIST_DO_USUARIO", OracleType.VarChar).Value = ""
            oraCommandT.Parameters.Add("V_HIST_DO_NOMBRE_DOCUMENTO", OracleType.VarChar).Value = ""
            oraCommandT.Parameters.Add("V_HIST_DO_FECHA", OracleType.VarChar).Value = ""
            oraCommandT.Parameters.Add("V_HIST_DESC_DOCUMENTO", OracleType.VarChar).Value = ""
            oraCommandT.Parameters.Add("v_bandera", OracleType.Number).Value = 3
            oraCommandT.Parameters.Add("cv_1", OracleType.Cursor).Direction = ParameterDirection.Output
            Dim oDataset1 As DataSet = Consulta_Procedure(oraCommandT, "documento")

            Dim objDN As DataSet = Consulta_Procedure(oraCommandT, "Documentos")
            Dim cuantos As Integer = objDN.Tables("Documentos").Rows.Count

            If cuantos <= 0 Then
                lbmsj.Text = "No se Encontraron Documentos"

                GridArchivos.Visible = False
                BtnEliminar.Visible = False
            Else
                GridArchivos.DataSource = objDN
                GridArchivos.DataBind()
                BtnEliminar.Visible = True
                GridArchivos.Visible = True

            End If
        Catch ex As System.Threading.ThreadAbortException
        Catch ex As Exception
            lbmsj.Text = ex.Message
            SendMail("llenar_grid", ex, "", "", LblCat_Lo_Usuario.Text)
        End Try
    End Sub

    Protected Sub AsyncFileUpload1_UploadedComplete(ByVal sender As Object, ByVal e As AjaxControlToolkit.AsyncFileUploadEventArgs) Handles AsyncFileUpload1.UploadedComplete
        Dim ruta As String = StrRuta() & "\PDF"
        SubExisteRuta(ruta)
        Dim Tamano As Integer = e.FileSize
        Dim nom As String = e.FileName.Substring(e.FileName.LastIndexOf("\") + 1)
        Dim extension As String = nom.Substring(nom.LastIndexOf("."))
        If extension.ToUpper = ".PDF" And Len(txtDescripcion.Text) > 5 Then
            If Tamano < 4000000 Then
                Dim ARCHIVO As String = ruta & "\" & nom
                Me.AsyncFileUpload1.SaveAs(ARCHIVO)
                Dim fs As Stream = File.OpenRead(ARCHIVO)
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
                Session("Mensajes") = "Documento Cargado"
                Dim tempLob As OracleLob
                tempLob = cmd.Parameters(0).Value
                tempLob.BeginBatch(OracleLobOpenMode.ReadWrite)
                tempLob.Write(tempBuff, 0, tempBuff.Length)
                tempLob.EndBatch()
                cmd.Parameters.Clear()
                cmd.CommandText = "SP_ADD_ARCHIVO"
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add(New OracleParameter("V_HIST_DO_DOCUMENTO", OracleType.Blob)).Value = tempLob
                cmd.Parameters.Add("V_HIST_DO_CREDITO", OracleType.VarChar).Value = CType(Session("credito"), credito).PR_MC_CREDITO
                cmd.Parameters.Add("V_HIST_DO_USUARIO", OracleType.VarChar).Value = (CType(Session("Usuario"), USUARIO)).CAT_LO_USUARIO
                cmd.Parameters.Add("V_HIST_DO_NOMBRE_DOCUMENTO", OracleType.VarChar).Value = nom
                cmd.Parameters.Add("V_HIST_DESC_DOCUMENTO", OracleType.VarChar).Value = txtDescripcion.Text
                cmd.Parameters.Add("V_Bandera", OracleType.Number).Value = 1
                cmd.ExecuteNonQuery()
                tx.Commit()
                conn.Close()
            Else
                Session("Mensajes") = "El Archivo Supera El Tamaño Permitido De 4 Megas,Intente De Nuevo"
            End If
        Else
            Session("Mensajes") = "Solo Se Pueden Cargar Archivos En Formato PDF,Intente De Nuevo"
        End If

    End Sub

    Protected Sub chkSelect_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim chkTest As CheckBox = DirectCast(sender, CheckBox)
        Dim grdRow As GridViewRow = DirectCast(chkTest.NamingContainer, GridViewRow)
    End Sub

    Protected Sub chkSelectAll_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim chkAll As CheckBox = DirectCast(GridArchivos.HeaderRow.FindControl("chkSelectAll"), CheckBox)
        If chkAll.Checked = True Then
            For Each gvRow As GridViewRow In GridArchivos.Rows
                Dim chkSel As CheckBox = DirectCast(gvRow.FindControl("chkSelect"), CheckBox)
                chkSel.Checked = True
            Next
        Else
            For Each gvRow As GridViewRow In GridArchivos.Rows
                Dim chkSel As CheckBox = DirectCast(gvRow.FindControl("chkSelect"), CheckBox)
                chkSel.Checked = False
            Next
        End If
    End Sub

    Private Sub SendMail(ByRef evento As String, ByVal ex As Exception, ByVal Cuenta As String, ByVal Captura As String, ByVal usr As String)
        EnviarCorreo("Archivos.ascx", evento, ex, Cuenta, Captura, usr)
    End Sub

    Protected Sub GridArchivos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridArchivos.SelectedIndexChanged
        'Try


        Dim row As GridViewRow = GridArchivos.SelectedRow
        Dim oraCommandT As New OracleCommand
        oraCommandT.CommandText = "SP_HISTORICO_ARCHIVO"
        oraCommandT.CommandType = CommandType.StoredProcedure
        oraCommandT.Parameters.Add("V_HIST_DO_CREDITO", OracleType.VarChar).Value = CType(Session("credito"), credito).PR_MC_CREDITO
        oraCommandT.Parameters.Add("V_HIST_DO_USUARIO", OracleType.VarChar).Value = row.Cells(2).Text
        oraCommandT.Parameters.Add("V_HIST_DO_NOMBRE_DOCUMENTO", OracleType.VarChar).Value = HttpUtility.HtmlDecode(row.Cells(3).Text)
        oraCommandT.Parameters.Add("V_HIST_DO_FECHA", OracleType.VarChar).Value = row.Cells(4).Text
        oraCommandT.Parameters.Add("V_HIST_DESC_DOCUMENTO", OracleType.VarChar).Value = row.Cells(5).Text
        oraCommandT.Parameters.Add("v_bandera", OracleType.Number).Value = 1
        oraCommandT.Parameters.Add("cv_1", OracleType.Cursor).Direction = ParameterDirection.Output
        Dim oDataset1 As DataSet = Consulta_Procedure(oraCommandT, "documento")
        If oDataset1.Tables(0).Rows.Count > 0 Then
            Dim ruta As String = StrRuta() & "\PDF"
            SubExisteRuta(ruta)
            Dim tmp_archivo As String = ruta & "\" & oDataset1.Tables(0).Rows(0)("HIST_DO_NOMBRE_DOCUMENTO")
            If File.Exists(tmp_archivo) Then
                Kill(tmp_archivo)
            End If
            Dim PDF As Byte() = DirectCast(oDataset1.Tables(0).Rows(0)("HIST_DO_DOCUMENTO"), Byte())

            Dim oFileStream As FileStream
            oFileStream = New FileStream(tmp_archivo, FileMode.CreateNew, FileAccess.Write)
            oFileStream.Write(PDF, 0, PDF.Length)
            oFileStream.Close()

            If File.Exists(tmp_archivo) Then
                Dim ioflujo As FileInfo = New FileInfo(tmp_archivo)
                Response.Clear()
                Response.AddHeader("Content-Disposition", "attachment; filename=" + ioflujo.Name)
                Response.AddHeader("Content-Length", ioflujo.Length.ToString())
                Response.ContentType = "application/octet-stream"
                Response.WriteFile(tmp_archivo)
                Response.End()
            End If
        End If

    End Sub

    Protected Sub BtnEliminar_Click(sender As Object, e As EventArgs) Handles BtnEliminar.Click
        Try
            Dim cont As Integer = 0
            For Each gvRow As GridViewRow In GridArchivos.Rows
                Dim chkSel As CheckBox = DirectCast(gvRow.FindControl("chkSelect"), CheckBox)
                If chkSel.Checked = True Then
                    Dim oraCommandT As New OracleCommand
                    oraCommandT.CommandText = "SP_HISTORICO_ARCHIVO"
                    oraCommandT.CommandType = CommandType.StoredProcedure
                    oraCommandT.Parameters.Add("V_HIST_DO_CREDITO", OracleType.VarChar).Value = CType(Session("credito"), credito).PR_MC_CREDITO
                    oraCommandT.Parameters.Add("V_HIST_DO_USUARIO", OracleType.VarChar).Value = gvRow.Cells(2).Text
                    oraCommandT.Parameters.Add("V_HIST_DO_NOMBRE_DOCUMENTO", OracleType.VarChar).Value = gvRow.Cells(3).Text
                    oraCommandT.Parameters.Add("V_HIST_DO_FECHA", OracleType.VarChar).Value = gvRow.Cells(4).Text
                    oraCommandT.Parameters.Add("V_HIST_DESC_DOCUMENTO", OracleType.VarChar).Value = gvRow.Cells(5).Text
                    oraCommandT.Parameters.Add("v_bandera", OracleType.Number).Value = 2
                    oraCommandT.Parameters.Add("cv_1", OracleType.Cursor).Direction = ParameterDirection.Output
                    Dim oDataset1 As DataSet = Consulta_Procedure(oraCommandT, "documento")
                End If
            Next
            Session("Mensajes") = "Documento Eliminado"
            Response.Redirect("MasterPage.aspx")
        Catch ex As System.Threading.ThreadAbortException
        Catch ex As Exception
            lbmsj.Text = ex.Message
            SendMail("BtnEliminar_Click", ex, "", "", LblCat_Lo_Usuario.Text)
        End Try
    End Sub
    Protected Sub llenar_grid2()
        Try
            Dim oraCommandT As New OracleCommand
            oraCommandT.CommandText = "SP_HISTORICO_ARCHIVO"
            oraCommandT.CommandType = CommandType.StoredProcedure
            oraCommandT.Parameters.Add("V_HIST_DO_CREDITO", OracleType.VarChar).Value = CType(Session("credito"), credito).PR_MC_CREDITO
            oraCommandT.Parameters.Add("V_HIST_DO_USUARIO", OracleType.VarChar).Value = ""
            oraCommandT.Parameters.Add("V_HIST_DO_NOMBRE_DOCUMENTO", OracleType.VarChar).Value = ""
            oraCommandT.Parameters.Add("V_HIST_DO_FECHA", OracleType.VarChar).Value = ""
            oraCommandT.Parameters.Add("V_HIST_DESC_DOCUMENTO", OracleType.VarChar).Value = ""
            oraCommandT.Parameters.Add("v_bandera", OracleType.Number).Value = 4
            oraCommandT.Parameters.Add("cv_1", OracleType.Cursor).Direction = ParameterDirection.Output
            Dim objDN As DataSet = Consulta_Procedure(oraCommandT, "Convenios")

            Dim cuantos As Integer = objDN.Tables("Convenios").Rows.Count
            If cuantos <= 0 Then
                Lbmsj2.Text = "No se Encontraron Documentos"

                GridConvenios.Visible = False

            Else
                GridConvenios.DataSource = objDN
                GridConvenios.DataBind()
                GridConvenios.Visible = True
                Lbmsj2.Text = "La contraseña de los Convenios son los ultimos 6 digitos de la matricula del estudiante"
            End If
        Catch ex As System.Threading.ThreadAbortException
        Catch ex As Exception
            lbmsj.Text = ex.Message
            SendMail("llenar_grid2", ex, "", "", LblCat_Lo_Usuario.Text)
        End Try
    End Sub

    Protected Sub GridConvenios_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridConvenios.SelectedIndexChanged
        'Try


        Dim row As GridViewRow = GridConvenios.SelectedRow
        Dim oraCommandT As New OracleCommand
        oraCommandT.CommandText = "SP_HISTORICO_ARCHIVO"
        oraCommandT.CommandType = CommandType.StoredProcedure
        oraCommandT.Parameters.Add("V_HIST_DO_CREDITO", OracleType.VarChar).Value = CType(Session("credito"), credito).PR_MC_CREDITO
        oraCommandT.Parameters.Add("V_HIST_DO_USUARIO", OracleType.VarChar).Value = "" 'row.Cells(2).Text
        oraCommandT.Parameters.Add("V_HIST_DO_NOMBRE_DOCUMENTO", OracleType.VarChar).Value = HttpUtility.HtmlDecode(row.Cells(4).Text)
        oraCommandT.Parameters.Add("V_HIST_DO_FECHA", OracleType.VarChar).Value = row.Cells(2).Text
        oraCommandT.Parameters.Add("V_HIST_DESC_DOCUMENTO", OracleType.VarChar).Value = row.Cells(3).Text
        oraCommandT.Parameters.Add("v_bandera", OracleType.Number).Value = 5
        oraCommandT.Parameters.Add("cv_1", OracleType.Cursor).Direction = ParameterDirection.Output
        Dim oDataset1 As DataSet = Consulta_Procedure(oraCommandT, "documento")
        If oDataset1.Tables(0).Rows.Count > 0 Then
            Dim ruta As String = StrRuta() & "PDF"
            SubExisteRuta(ruta)
            Dim tmp_archivo As String = ruta & "\" & oDataset1.Tables(0).Rows(0)("HIST_CON_NOMPDF")
            If File.Exists(tmp_archivo) Then
                Kill(tmp_archivo)
            End If
            Dim PDF As Byte() = DirectCast(oDataset1.Tables(0).Rows(0)("HIST_CON_CONVENIO"), Byte())

            Dim oFileStream As FileStream
            oFileStream = New FileStream(tmp_archivo, FileMode.CreateNew, FileAccess.Write)
            oFileStream.Write(PDF, 0, PDF.Length)
            oFileStream.Close()

            If File.Exists(tmp_archivo) Then
                Dim ioflujo As FileInfo = New FileInfo(tmp_archivo)
                Response.Clear()
                Response.AddHeader("Content-Disposition", "attachment; filename=" + ioflujo.Name)
                Response.AddHeader("Content-Length", ioflujo.Length.ToString())
                Response.ContentType = "application/octet-stream"
                Response.WriteFile(tmp_archivo)
                Response.End()
            End If
        End If

    End Sub
End Class
