Imports System.IO

Module mGeneral
    Public G_EmpresaNombre As String = "13 Ponientes analistas"
    Public G_MostrarErrores As Boolean = True
    Public G_ErrorLog As Boolean = False
    Public Sub Msg()
        Msg("<..>", 3)
    End Sub

    Public Sub Msg(ByVal cadena As String, Optional ByVal tipo As Integer = 1)
        Dim icono As Integer
        Select Case tipo
            Case 1
                icono = vbInformation
            Case 2
                icono = vbExclamation
            Case 3
                icono = vbCritical
            Case 4
                icono = vbQuestion
            Case Else
                icono = vbInformation
        End Select
        MsgBox(cadena, icono, G_EmpresaNombre)
    End Sub

    Public Sub X(ByVal ex As Exception)
        If (G_MostrarErrores) Then
            Msg(ex.Message, 3)
        End If
        If G_ErrorLog Then
            Dim fichero As String = "log.txt"
            Dim escritor As StreamWriter

            escritor = File.AppendText(fichero)
            escritor.Write(vbCrLf + "///////////////////////////////////////////////////////////////////////////////////" + vbCrLf)
            escritor.Write(Now.ToString + vbCrLf)
            escritor.Write(ex.Message + vbCrLf)
            escritor.Write("-------------------------------------------------------------------------------------" + vbCrLf + vbCrLf)
            escritor.Flush()
            escritor.Close()
        End If
    End Sub
End Module
