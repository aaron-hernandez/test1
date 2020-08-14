Imports Microsoft.VisualBasic
Imports iTextSharp
Imports iTextSharp.text
Imports iTextSharp.text.pdf


Public Class EventoTitulos
    Inherits PdfPageEventHelper
    Protected total As PdfTemplate
    Protected helv As BaseFont
    Private settingFont As Boolean = False

    Public Overrides Sub OnOpenDocument(writer As PdfWriter, document As Document)
        ' Se crea el template
        total = writer.DirectContent.CreateTemplate(100, 100)
        total.BoundingBox = New Rectangle(-20, -20, 100, 100)
        helv = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.WINANSI, BaseFont.NOT_EMBEDDED)
    End Sub


    'Public Overrides Sub OnStartPage(writer As PdfWriter, document As Document)
    '    Dim sTitulo As String = "", sLinea As String = ""
    '    Dim textSize As Single = 6
    '    Dim textBase As Single = 600
    '    ' Este pone la informacion en la parte superior
    '    Dim cb As PdfContentByte = writer.DirectContent
    '    cb.SaveState()
    '    cb.BeginText()
    '    cb.SetFontAndSize(helv, 6)
    '    sTitulo = "TITULO"
    '    cb.SetTextMatrix(document.Left, textBase)
    '    cb.ShowText(sTitulo)
    '    cb.AddTemplate(total, document.Left + textSize, textBase)
    '    textBase = textBase - 5
    '    sLinea = "------------------------------------"
    '    cb.SetTextMatrix(document.Left, textBase - 5)
    '    cb.ShowText(sLinea)
    '    cb.EndText()
    '    'Este es necesario para cerrar el BeginText, pero solo se pone en el ultimo texto a anexar, sino marcara error
    '    cb.AddTemplate(total, document.Left + textSize, textBase)
    '    cb.RestoreState()
    'End Sub

    Public Overrides Sub OnEndPage(writer As PdfWriter, document As Document)
        Dim cb As PdfContentByte = writer.DirectContent
        cb.SaveState()
        cb.BeginText()
        cb.SetFontAndSize(helv, 8)
        Dim sPiePagina As String = ""
        Dim textSize As Single = 10
        Dim textBase As Single = 20
        ' Este lo pone la informacion en la parte inferior
        sPiePagina = "                                         CORPORATIVO DE COBRANZA ROSALES HERNANDEZ S.A DE C.V"
        cb.SetTextMatrix(document.Left, textBase)
        cb.ShowText(sPiePagina)
        cb.AddTemplate(total, document.Left + textSize, textBase)
        sPiePagina = "                            ¿NECESITAS AYUDA, TIENES ALGUNA DUDA O SUGERENCIA? TELEFONOS: 018000220143"
        cb.SetTextMatrix(document.Left, textBase - 10)
        cb.ShowText(sPiePagina)
        'cb.AddTemplate(total, document.Left + textSize, textBase)
        'sPiePagina = "                                                        GERENTE DE CARTERA: <<SUPERVISOR>> "
        'cb.SetTextMatrix(document.Left, textBase - 10)
        'cb.ShowText(sPiePagina)

        cb.EndText()
        cb.AddTemplate(total, document.Left + textSize, textBase)
        cb.RestoreState()
    End Sub



End Class