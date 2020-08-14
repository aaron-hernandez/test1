Imports Microsoft.VisualBasic
Imports System.Data.OracleClient
Imports System.Data.SqlClient
Imports Db
Imports System.Data


Public Class EventosCalendario

    Public Shared Function getEvents(start As DateTime, [end] As DateTime) As List(Of CalendarEvent)
        Dim events As New List(Of CalendarEvent)()
        Dim oraCommand As New OracleCommand
        oraCommand.CommandText = "SP_AGENDA"
        oraCommand.CommandType = CommandType.StoredProcedure
        oraCommand.Parameters.Add("V_USUARIO", OracleType.VarChar).Value = "YO" 'CType(Session("Usuario"), USUARIO).CAT_LO_USUARIO
        oraCommand.Parameters.Add("V_INICIO", OracleType.DateTime).Value = start
        oraCommand.Parameters.Add("V_FIN", OracleType.DateTime).Value = [end]
        oraCommand.Parameters.Add("cv_1", OracleType.Cursor).Direction = ParameterDirection.Output
        Dim DtsAgenda As DataSet = Consulta_Procedure(oraCommand, "AGENDA")
        If DtsAgenda.Tables(0).Rows.Count() = 0 Then
            Dim rtn As Date = "01/01/2000"
           
            Dim cevent As New CalendarEvent()
            cevent.id = CInt(0)
            cevent.title = "INICIO MES"
            cevent.description = "INICIO MES"
            cevent.start = rtn
            cevent.[end] = rtn
            events.Add(cevent)
        Else
            For A As Integer = 0 To DtsAgenda.Tables(0).Rows.Count - 1
                Dim cevent As New CalendarEvent()
                cevent.id = CInt(DtsAgenda.Tables(0).Rows(A)("HIST_AG_ID"))
                cevent.title = DirectCast(DtsAgenda.Tables(0).Rows(A)("HIST_AG_TITULO"), String)
                cevent.description = DirectCast(DtsAgenda.Tables(0).Rows(A)("HIST_AG_COMENTARIO"), String)
                cevent.start = DirectCast(DtsAgenda.Tables(0).Rows(A)("HIST_AG_INICIO"), DateTime)
                cevent.[end] = DirectCast(DtsAgenda.Tables(0).Rows(A)("HIST_AG_FIN"), DateTime)
                events.Add(cevent)
            Next
        End If
        Return events
    End Function

    Public Shared Sub updateEvent(id As Integer, title As [String], description As [String])
        Dim oraCommand As New OracleCommand
        oraCommand.CommandText = "SP_ADD_HIST_AGENDA"
        oraCommand.CommandType = CommandType.StoredProcedure
        oraCommand.Parameters.Add("V_BANDERA", OracleType.Number).Value = 1
        oraCommand.Parameters.Add("V_ID", OracleType.Number).Value = id
        oraCommand.Parameters.Add("V_INICIO", OracleType.DateTime).Value = Now
        oraCommand.Parameters.Add("V_FIN", OracleType.DateTime).Value = Now
        oraCommand.Parameters.Add("V_USUARIO", OracleType.VarChar).Value = "YO" 'CType(Session("Usuario"), USUARIO).CAT_LO_USUARIO
        oraCommand.Parameters.Add("V_COMENTARIO", OracleType.VarChar).Value = description
        oraCommand.Parameters.Add("V_TITULO", OracleType.VarChar).Value = title
        oraCommand.Parameters.Add("V_CREDITO", OracleType.VarChar).Value = "501086065965" 'CType(Session("Credito"), Credito).PR_KT_CREDITO
        oraCommand.Parameters.Add("CV_1", OracleType.Cursor).Direction = ParameterDirection.Output
        Dim DtsAddAgenda As DataSet = Consulta_Procedure(oraCommand, "Agenda")
    End Sub

    Public Shared Sub updateEventTime(id As Integer, start As DateTime, [end] As DateTime)
        Dim oraCommand As New OracleCommand
        oraCommand.CommandText = "SP_ADD_HIST_AGENDA"
        oraCommand.CommandType = CommandType.StoredProcedure
        oraCommand.Parameters.Add("V_BANDERA", OracleType.Number).Value = 2
        oraCommand.Parameters.Add("V_ID", OracleType.Number).Value = id
        oraCommand.Parameters.Add("V_INICIO", OracleType.DateTime).Value = start
        oraCommand.Parameters.Add("V_FIN", OracleType.DateTime).Value = [end]
        oraCommand.Parameters.Add("V_USUARIO", OracleType.VarChar).Value = "YO" 'CType(Session("Usuario"), USUARIO).CAT_LO_USUARIO
        oraCommand.Parameters.Add("V_COMENTARIO", OracleType.VarChar).Value = ""
        oraCommand.Parameters.Add("V_TITULO", OracleType.VarChar).Value = ""
        oraCommand.Parameters.Add("V_CREDITO", OracleType.VarChar).Value = "501086065965" 'CType(Session("Credito"), Credito).PR_KT_CREDITO
        oraCommand.Parameters.Add("CV_1", OracleType.Cursor).Direction = ParameterDirection.Output
        Dim DtsAddAgenda As DataSet = Consulta_Procedure(oraCommand, "Agenda")
    End Sub

    Public Shared Sub deleteEvent(id As Integer)
        Dim oraCommand As New OracleCommand
        oraCommand.CommandText = "SP_ADD_HIST_AGENDA"
        oraCommand.CommandType = CommandType.StoredProcedure
        oraCommand.Parameters.Add("V_BANDERA", OracleType.Number).Value = 3
        oraCommand.Parameters.Add("V_ID", OracleType.Number).Value = id
        oraCommand.Parameters.Add("V_INICIO", OracleType.DateTime).Value = Now
        oraCommand.Parameters.Add("V_FIN", OracleType.DateTime).Value = Now
        oraCommand.Parameters.Add("V_USUARIO", OracleType.VarChar).Value = "YO" 'CType(Session("Usuario"), USUARIO).CAT_LO_USUARIO
        oraCommand.Parameters.Add("V_COMENTARIO", OracleType.VarChar).Value = ""
        oraCommand.Parameters.Add("V_TITULO", OracleType.VarChar).Value = ""
        oraCommand.Parameters.Add("V_CREDITO", OracleType.VarChar).Value = "501086065965" 'CType(Session("Credito"), Credito).PR_KT_CREDITO
        oraCommand.Parameters.Add("CV_1", OracleType.Cursor).Direction = ParameterDirection.Output
        Dim DtsAddAgenda As DataSet = Consulta_Procedure(oraCommand, "Agenda")
    End Sub

    Public Shared Function addEvent(cevent As CalendarEvent) As Integer
        Dim oraCommand As New OracleCommand
        oraCommand.CommandText = "SP_ADD_HIST_AGENDA"
        oraCommand.CommandType = CommandType.StoredProcedure
        oraCommand.Parameters.Add("V_BANDERA", OracleType.Number).Value = 0
        oraCommand.Parameters.Add("V_INICIO", OracleType.DateTime).Value = cevent.start
        oraCommand.Parameters.Add("V_FIN", OracleType.DateTime).Value = cevent.[end]
        oraCommand.Parameters.Add("V_USUARIO", OracleType.VarChar).Value = "YO" 'CType(Session("Usuario"), USUARIO).CAT_LO_USUARIO
        oraCommand.Parameters.Add("V_COMENTARIO", OracleType.VarChar).Value = cevent.description
        oraCommand.Parameters.Add("V_TITULO", OracleType.VarChar).Value = cevent.title
        oraCommand.Parameters.Add("V_CREDITO", OracleType.VarChar).Value = "501086065965" 'CType(Session("Credito"), Credito).PR_KT_CREDITO
        oraCommand.Parameters.Add("CV_1", OracleType.Cursor).Direction = ParameterDirection.Output
        Dim DtsAddAgenda As DataSet = Consulta_Procedure(oraCommand, "Agenda")
        Return Val(DtsAddAgenda.Tables("Agenda").Rows(0).Item("IDENTIFICADOR"))
    End Function

End Class
