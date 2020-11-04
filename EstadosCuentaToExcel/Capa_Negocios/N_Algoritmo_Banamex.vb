
Imports Capa_Identidad

Public Class N_Algoritmo_Banamex
    Inherits N_Algoritmo

    Public Sub New()
    End Sub

    Public Sub New(cadena As String, formato As I_formato)
        MyBase.New(cadena, formato)
    End Sub

    Protected Overrides Sub GetDatos()
        Dim cadena As String = _cadena
        Dim aux As String
        Dim indice As Integer

        aux = GetLinea(cadena)
        Do
            Try

                Archivo.agregarFila(ProcesarLinea(aux))
                indice = cadena.IndexOf(aux)
                cadena = cadena.Substring(indice + aux.Length)
                aux = GetLinea(cadena)
            Catch ex As Exception
                aux = ""
            End Try

        Loop While aux.Length >= 0

    End Sub

    Protected Overrides Function GetLinea(cadena As String) As String

    End Function

    Private Function ProcesarLinea(ByVal cadena As String) As List(Of String)

    End Function


End Class
