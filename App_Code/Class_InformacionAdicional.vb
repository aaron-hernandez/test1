Imports Microsoft.VisualBasic
Imports System.Data.OracleClient
Imports Db
Imports System.Globalization
Imports System.IO
Imports Funciones
Imports System.Web.SessionState.HttpSessionState
Imports System.Data
Public Class Class_InformacionAdicional
    Public Shared Function LlenarElementosAgregar(ByVal V_Credito As String, ByVal V_Bandera As String) As Object
        Dim oraCommand As New OracleCommand
        oraCommand.CommandText = "SP_INFORMACION_ADICIONAL"
        oraCommand.CommandType = CommandType.StoredProcedure
        oraCommand.Parameters.Add("V_Credito", OracleType.VarChar).Value = V_Credito
        oraCommand.Parameters.Add("V_Bandera", OracleType.VarChar).Value = V_Bandera
        oraCommand.Parameters.Add("cv_1", OracleType.Cursor).Direction = ParameterDirection.Output
        Dim DtsTelefonos As DataSet = Consulta_Procedure(oraCommand, "ELEMENTOS")
        Return DtsTelefonos
    End Function
    Public Shared Function AgregarTelefono(ByVal V_HIST_TE_CONSECUTIVO As String, ByVal V_Hist_Te_Credito As String, ByVal V_Hist_Te_Producto As String, ByVal V_Hist_Te_Lada As String, ByVal V_Hist_Te_Numerotel As String, ByVal V_Hist_Te_Tipo As String, ByVal V_Hist_Te_Parentesco As String, ByVal V_Hist_Te_Nombre As String, ByVal V_Hist_Te_Extension As String, ByVal V_Hist_Te_Horario0 As String, ByVal V_Hist_Te_Horario1 As String, ByVal V_Hist_Te_Usuario As String, ByVal V_Hist_Te_Agencia As String, ByVal V_Hist_Te_Fuente As String, ByVal Boton As Object, ByVal Gv As Object, ByVal V_Hist_Te_Proporciona As String) As String
        Dim V_Bandera As Integer = 2
        Dim Conse As Integer

        If Boton.Text = "Agregar" Then
            V_Bandera = 1
            Conse = 0
        Else
            Conse = V_HIST_TE_CONSECUTIVO
        End If
        Dim Dias As String = ""
        For Each gvRow As GridViewRow In Gv.Rows
            Dim chkSel As CheckBox = DirectCast(gvRow.FindControl("chkSelect"), CheckBox)
            Dias = Dias & Boleano(chkSel.Checked).ToString
        Next
        If V_Hist_Te_Parentesco <> "Cliente" And V_Hist_Te_Nombre = "" Then
            Return "Capture El Nombre De " & V_Hist_Te_Parentesco
        ElseIf V_Hist_Te_Tipo = "Oficina" And V_Hist_Te_Extension = "" Then
            Return "Capture El Número De Extensión "
        ElseIf V_Hist_Te_Lada = "" Then
            Return "Capture El Número De Lada"
        ElseIf Len(V_Hist_Te_Lada) + Len(V_Hist_Te_Numerotel) <> 10 Then
            Return "Número Incorrecto, Valide"
        ElseIf Val(V_Hist_Te_Horario0) > Val(V_Hist_Te_Horario1) Then
            Return "Valide Horario"
        Else
            Dim oraCommand As New OracleCommand
            oraCommand.CommandText = "SP_ADD_HIST_TELEFONOS"
            oraCommand.CommandType = CommandType.StoredProcedure
            oraCommand.Parameters.Add("V_Bandera", OracleType.Number).Value = V_Bandera
            oraCommand.Parameters.Add("V_HIST_TE_CONSECUTIVO", OracleType.Number).Value = Conse
            oraCommand.Parameters.Add("V_Hist_Te_Credito", OracleType.VarChar).Value = V_Hist_Te_Credito
            oraCommand.Parameters.Add("V_Hist_Te_Producto", OracleType.VarChar).Value = V_Hist_Te_Producto
            oraCommand.Parameters.Add("V_Hist_Te_Lada", OracleType.VarChar).Value = V_Hist_Te_Lada
            oraCommand.Parameters.Add("V_Hist_Te_Numerotel", OracleType.VarChar).Value = V_Hist_Te_Numerotel
            oraCommand.Parameters.Add("V_Hist_Te_Tipo", OracleType.VarChar).Value = V_Hist_Te_Tipo
            oraCommand.Parameters.Add("V_Hist_Te_Parentesco", OracleType.VarChar).Value = V_Hist_Te_Parentesco
            oraCommand.Parameters.Add("V_Hist_Te_Nombre", OracleType.VarChar).Value = V_Hist_Te_Nombre
            oraCommand.Parameters.Add("V_Hist_Te_Extension", OracleType.VarChar).Value = V_Hist_Te_Extension
            oraCommand.Parameters.Add("V_Hist_Te_Horario", OracleType.VarChar).Value = V_Hist_Te_Horario0 & V_Hist_Te_Horario1
            oraCommand.Parameters.Add("V_Hist_Te_Dias", OracleType.VarChar).Value = Dias
            oraCommand.Parameters.Add("V_Hist_Te_Usuario", OracleType.VarChar).Value = V_Hist_Te_Usuario
            oraCommand.Parameters.Add("V_Hist_Te_Agencia", OracleType.VarChar).Value = V_Hist_Te_Agencia
            oraCommand.Parameters.Add("V_Hist_Te_Fuente", OracleType.VarChar).Value = V_Hist_Te_Fuente
            oraCommand.Parameters.Add("V_Hist_Te_Proporciona", OracleType.VarChar).Value = V_Hist_Te_Proporciona
            oraCommand.Parameters.Add("cv_1", OracleType.Cursor).Direction = ParameterDirection.Output
            Dim DtsTelefonos As DataSet = Consulta_Procedure(oraCommand, "Telefonos")
            Return DtsTelefonos.Tables("Telefonos").Rows(0).Item("telefono")
        End If
    End Function
    Public Shared Function AgregarDireccion(ByVal V_Hist_Di_Consecutivo As String, ByVal V_Hist_Di_Credito As String, ByVal V_Hist_Di_Producto As String, ByVal V_Hist_Di_Calle As String, ByVal V_Hist_Di_Colonia As String, ByVal V_Hist_Di_Muni As String, ByVal V_Hist_Di_Ciudad As String, ByVal V_Hist_Di_Estado As String, ByVal V_Hist_Di_Cp As String, ByVal V_Hist_Di_Numext As String, ByVal V_Hist_Di_Numint As String, ByVal V_Hist_Di_Parentesco As String, ByVal V_Hist_Di_Nombre As String, ByVal V_Hist_Di_Usuario As String, ByVal V_Hist_Di_Agencia As String, ByVal V_Hist_Di_Fuente As String, ByVal V_Hist_Di_Horario0 As String, ByVal V_Hist_Di_Horario1 As String, ByVal V_Hist_Di_Contacto As String, ByVal Boton As Object, ByVal Gv As Object, ByVal V_HIST_DI_PROPORCIONA As String) As String
        Dim V_Bandera As Integer = 2
        Dim Conse As Integer

        If Boton.Text = "Agregar" Then
            V_Bandera = 1
            Conse = 0
        Else
            Conse = V_Hist_Di_Consecutivo
        End If
        Dim Dias As String = ""
        For Each gvRow As GridViewRow In Gv.Rows
            Dim chkSel As CheckBox = DirectCast(gvRow.FindControl("chkSelect"), CheckBox)
            Dias = Dias & Boleano(chkSel.Checked).ToString
        Next

        If V_Hist_Di_Parentesco <> "Cliente" And V_Hist_Di_Nombre = "" Then
            Return "Capture El Nombre De " & V_Hist_Di_Parentesco
        ElseIf V_Hist_Di_Colonia = "" Then
            Return "Selecciona Una Colonia"
        ElseIf V_Hist_Di_Numext = "" Then
            Return "Escribe Número Exterior"
        ElseIf V_Hist_Di_Calle = "" Then
            Return "Escribe Una Calle"
        ElseIf Val(V_Hist_Di_Horario0) > Val(V_Hist_Di_Horario1) Then
            Return "Valide Horario"
        Else
            Dim DtsDireccion As DataSet
            Dim oraCommandD As New OracleCommand
            oraCommandD.CommandText = "SP_ADD_HIST_DIRECCIONES"
            oraCommandD.CommandType = CommandType.StoredProcedure
            oraCommandD.Parameters.Add("V_BANDERA", OracleType.Number).Value = V_Bandera
            oraCommandD.Parameters.Add("V_Hist_Di_Consecutivo", OracleType.Number).Value = Conse
            oraCommandD.Parameters.Add("V_HIST_DI_CREDITO", OracleType.VarChar).Value = V_Hist_Di_Credito
            oraCommandD.Parameters.Add("V_Hist_Di_Producto", OracleType.VarChar).Value = V_Hist_Di_Producto
            oraCommandD.Parameters.Add("V_HIST_DI_CALLE", OracleType.VarChar).Value = V_Hist_Di_Calle
            oraCommandD.Parameters.Add("V_Hist_Di_Colonia", OracleType.VarChar).Value = V_Hist_Di_Colonia
            oraCommandD.Parameters.Add("V_Hist_Di_Muni", OracleType.VarChar).Value = V_Hist_Di_Muni
            oraCommandD.Parameters.Add("V_Hist_Di_Ciudad", OracleType.VarChar).Value = V_Hist_Di_Ciudad
            oraCommandD.Parameters.Add("V_HIST_DI_ESTADO", OracleType.VarChar).Value = V_Hist_Di_Estado
            oraCommandD.Parameters.Add("V_Hist_Di_Cp", OracleType.VarChar).Value = V_Hist_Di_Cp
            oraCommandD.Parameters.Add("V_HIST_DI_NUMEXT", OracleType.VarChar).Value = V_Hist_Di_Numext
            oraCommandD.Parameters.Add("V_Hist_Di_Numint", OracleType.VarChar).Value = V_Hist_Di_Numint
            oraCommandD.Parameters.Add("V_HIST_DI_PARENTESCO", OracleType.VarChar).Value = V_Hist_Di_Parentesco
            oraCommandD.Parameters.Add("V_Hist_Di_Nombre", OracleType.VarChar).Value = V_Hist_Di_Nombre
            oraCommandD.Parameters.Add("V_Hist_Di_Usuario", OracleType.VarChar).Value = V_Hist_Di_Usuario
            oraCommandD.Parameters.Add("V_Hist_Di_Agencia", OracleType.VarChar).Value = V_Hist_Di_Agencia
            oraCommandD.Parameters.Add("V_HIST_DI_FUENTE", OracleType.VarChar).Value = V_Hist_Di_Fuente
            oraCommandD.Parameters.Add("V_Hist_Di_Diascontacto", OracleType.VarChar).Value = Dias
            oraCommandD.Parameters.Add("V_Hist_Di_Horario", OracleType.VarChar).Value = V_Hist_Di_Horario0 & V_Hist_Di_Horario1
            oraCommandD.Parameters.Add("V_HIST_DI_CONTACTO", OracleType.VarChar).Value = V_Hist_Di_Contacto
            oraCommandD.Parameters.Add("V_HIST_DI_PROPORCIONA", OracleType.VarChar).Value = V_HIST_DI_PROPORCIONA
            oraCommandD.Parameters.Add("Cv_1", OracleType.Cursor).Direction = ParameterDirection.Output
            DtsDireccion = Consulta_Procedure(oraCommandD, "Direccion")
            Return DtsDireccion.Tables("Direccion").Rows(0).Item("Direccion")
        End If
    End Function

    Public Shared Function AgregarCorreo(ByVal V_Hist_Co_Consecutivo As String, ByVal V_Hist_Co_Credito As String, ByVal V_Hist_Co_Producto As String, ByVal V_Hist_Co_Parentesco As String, ByVal V_Hist_Co_Nombre As String, ByVal V_Hist_Co_Correo As String, ByVal V_Hist_Co_Contacto As String, ByVal V_Hist_Co_Usuario As String, ByVal V_Hist_Co_Agencia As String, ByVal V_Hist_Co_Fuente As String, ByVal V_Hist_Co_Tipo As String, ByVal Boton As Object, ByVal V_Hist_Co_Proporciona As String) As String

        Dim V_Bandera As Integer = 2
        Dim Conse As Integer

        If Boton.Text = "Agregar" Then
            V_Bandera = 1
            Conse = 0
        Else
            Conse = V_Hist_Co_Consecutivo
        End If

        If V_Hist_Co_Parentesco <> "Cliente" And V_Hist_Co_Nombre = "" Then
            Return "Capture El Nombre De " & V_Hist_Co_Parentesco
        ElseIf EmailValida(V_Hist_Co_Correo) = True Then
            Return "Correo No Valido"
        Else
            Dim DtsCorreos As DataSet
            Dim OraCommandCorreo As New OracleCommand
            OraCommandCorreo.CommandText = "SP_ADD_HIST_CORREOS"
            OraCommandCorreo.CommandType = CommandType.StoredProcedure
            OraCommandCorreo.Parameters.Add("V_Bandera", OracleType.Number).Value = V_Bandera
            OraCommandCorreo.Parameters.Add("V_Hist_Co_Consecutivo", OracleType.Number).Value = Conse
            OraCommandCorreo.Parameters.Add("V_Hist_Co_Credito", OracleType.VarChar).Value = V_Hist_Co_Credito
            OraCommandCorreo.Parameters.Add("V_Hist_Co_Producto", OracleType.VarChar).Value = V_Hist_Co_Producto
            OraCommandCorreo.Parameters.Add("V_Hist_Co_Parentesco", OracleType.VarChar).Value = V_Hist_Co_Parentesco
            OraCommandCorreo.Parameters.Add("V_Hist_Co_Nombre", OracleType.VarChar).Value = V_Hist_Co_Nombre
            OraCommandCorreo.Parameters.Add("V_Hist_Co_Correo", OracleType.VarChar).Value = V_Hist_Co_Correo
            OraCommandCorreo.Parameters.Add("V_Hist_Co_Contacto", OracleType.Number).Value = V_Hist_Co_Contacto
            OraCommandCorreo.Parameters.Add("V_Hist_CO_Usuario", OracleType.VarChar).Value = V_Hist_Co_Usuario
            OraCommandCorreo.Parameters.Add("V_Hist_Co_Agencia", OracleType.VarChar).Value = V_Hist_Co_Agencia
            OraCommandCorreo.Parameters.Add("V_Hist_Co_Fuente", OracleType.VarChar).Value = V_Hist_Co_Fuente
            OraCommandCorreo.Parameters.Add("V_HIST_CO_TIPO", OracleType.VarChar).Value = V_Hist_Co_Tipo
            OraCommandCorreo.Parameters.Add("V_Hist_Co_Proporciona", OracleType.VarChar).Value = V_Hist_Co_Proporciona
            OraCommandCorreo.Parameters.Add("CV_1", OracleType.Cursor).Direction = ParameterDirection.Output
            DtsCorreos = Consulta_Procedure(OraCommandCorreo, "Correo")
            Return DtsCorreos.Tables(0).Rows(0).Item("Correo")
        End If
    End Function
End Class
