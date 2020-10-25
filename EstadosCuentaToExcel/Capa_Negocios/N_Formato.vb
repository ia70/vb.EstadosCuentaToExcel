Imports System.IO
Imports Capa_Datos
Imports Capa_Identidad
Imports Newtonsoft.Json

Public Class N_Formato
    Private tabla As String = "formato"

#Region "FUNCIONES PUBLICAS"

    Public Function Lista() As DataTable
        Dim db As New D_db_operaciones(tabla)

        Return db.Lista

    End Function

    Public Function Insertar(ByVal ruta As String) As Boolean
        Dim formato As I_formato
        formato = leerFichero(ruta)


    End Function

    Public Function Consultar(ByVal id As String) As I_formato
        Dim db As New D_db_operaciones(tabla)
        Dim iden As New I_formato
        Dim res As DataTable

        res = db.Consulta(id)

        With iden
            '.Id = res.Rows(0).Item(0)
            '.Folder_in = res.Rows(0).Item(1)
            '.Folder_out = res.Rows(0).Item(2)
        End With

        Return iden
    End Function

    Public Function Editar(ByVal obj As I_formato) As Boolean
        Dim db As New D_db_operaciones(tabla)


        Return db.Insertar(obj)

    End Function

    Public Function Eliminar()
        Dim db As New D_db_operaciones(tabla)

        Return db.Eliminar(1)

    End Function

#End Region

#Region "FUNCIONES PRIVADAS"

    Private Function leerFichero(ByVal fichero As String) As I_formato
        Dim texto As String = ""

        Try
            Dim sr As New StreamReader(fichero)
            texto = sr.ReadToEnd()
            sr.Close()
        Catch ex As Exception
        End Try

        If texto.Length > 0 Then
            Return textToObj(texto)
        Else
            Return Nothing
        End If

    End Function

    Private Function textToObj(ByVal cadena As String) As I_formato
        Try
            Return JsonConvert.DeserializeObject(Of I_formato)(cadena)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

#End Region
End Class
