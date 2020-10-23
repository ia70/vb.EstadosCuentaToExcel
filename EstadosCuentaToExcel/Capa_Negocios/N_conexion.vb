Imports Capa_Datos
Imports Capa_Identidad
Imports Newtonsoft.Json
Imports System.IO

Public Class N_conexion
    Private conexionInfo As I_Conexion

    Public Sub New()
    End Sub

    Private Function getConexionString(ByVal obj As I_Conexion) As String
        Dim _conexion As String

        _conexion = "server=" & obj.Server & ";user id=" & obj.User_id & ";password=" & obj.Password & ";database=" & obj.Database

        Return _conexion
    End Function

    Public Sub setConexionInfo(ByVal info As I_Conexion)

    End Sub

    Public Function getConexionInfo() As I_Conexion

    End Function

    Public Function testConexion(ByVal obj As I_Conexion) As Boolean
        Dim _test As New D_conexion
        Return _test.testConexion(getConexionString(obj))
    End Function

    Public Sub leerDBinfo()
        Dim fichero As String = "Conexion.json"
        Dim texto As String = ""

        Try
            Dim sr As New StreamReader(fichero)
            texto = sr.ReadToEnd()
            sr.Close()
        Catch ex As Exception
            createDBinfo()
            Try
                Dim sr As New StreamReader(fichero)
                texto = sr.ReadToEnd()
                sr.Close()
            Catch ex2 As Exception
                createDBinfo()
            End Try
        End Try

        If texto.Length > 0 Then
            textToObj(texto)
        End If

    End Sub

    Private Sub textToObj(ByVal cadena As String)
        Try
            conexionInfo = JsonConvert.DeserializeObject(Of I_Conexion)(cadena)
        Catch ex As Exception
            conexionInfo = Nothing
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub createDBinfo()
        Dim fichero As String = "Conexion.json"
        Dim texto As String

        texto = "{" + vbCrLf + vbTab + "'server': '127.0.0.1'," + vbCrLf + vbTab + "'user_id': 'root'," + vbCrLf + vbTab + "'password': ''," + vbCrLf + vbTab + "'database': 'xrestadoscuentas'" + vbCrLf + "}"
        texto = texto.Replace("'", Chr(34))

        Try
            Dim sw As New StreamWriter(fichero)
            sw.WriteLine(texto)
            sw.Close()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

    End Sub

    Public Sub InicializarDB()
        Dim db = New D_conexion
        Try
            leerDBinfo()
            db.InicializarDB(getConexionString(conexionInfo))
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

    End Sub
End Class
