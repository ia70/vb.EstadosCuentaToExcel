Imports System.IO

Module mGeneral
#Region "VARIABLES GLOBALES"

    Public G_EmpresaNombre As String = "13 Ponientes analistas"
    Public G_MostrarErrores As Boolean = False
    Public G_ErrorLog As Boolean = True

#End Region

#Region "FUNCIONES"
    Public Function GetStr(ByVal valor As Object) As String
        Dim aux As String

        Try
            aux = valor
            Return valor
        Catch ex As Exception
            Return ""
        End Try

    End Function

    Public Function GetInt(ByVal valor As Object) As Integer
        Dim aux As Integer

        Try
            aux = valor
            Return valor
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Public Function GetBool(ByVal valor As Object) As Boolean
        Dim aux As Boolean

        Try
            aux = valor
            Return valor
        Catch ex As Exception
            Return False
        End Try
    End Function
#End Region

#Region "FUNCIONES GENERALES"

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
            Msg(ex.StackTrace.ToString, 3)
        End If
        Try
            If G_ErrorLog Then
                Dim fichero As String = "log_Negocios.txt"
                Dim escritor As StreamWriter

                escritor = File.AppendText(fichero)
                escritor.Write(vbCrLf + "///////////////////////////////////////////////////////////////////////////////////" + vbCrLf)
                escritor.Write(Now.ToString + vbCrLf)
                escritor.Write(ex.Message + vbCrLf)
                escritor.Write(ex.StackTrace + vbCrLf)
                escritor.Write("-------------------------------------------------------------------------------------" + vbCrLf + vbCrLf)
                escritor.Flush()
                escritor.Close()
            End If
        Catch ex2 As Exception
            Msg(ex2.Message, 3)
        End Try

    End Sub
#End Region

End Module
