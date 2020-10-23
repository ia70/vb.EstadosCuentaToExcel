Imports Capa_Identidad
Imports MySql.Data.MySqlClient

Public Class D_conexion

    Public Sub InicializarDB(ByVal conexion As String)
        ConexionString = conexion
    End Sub

    Public Function testConexion(ByVal _conexion As String) As Boolean
        Try
            Dim Conexion = New MySqlConnection
            Conexion.ConnectionString = _conexion
            Conexion.Open()
            Conexion.Close()
            Return True
        Catch ex As MySqlException
            Return False
        End Try
    End Function

End Class
