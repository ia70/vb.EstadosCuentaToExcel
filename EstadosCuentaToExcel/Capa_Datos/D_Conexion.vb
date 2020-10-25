Imports MySql.Data.MySqlClient

Public Class D_Conexion

    Public Function InicializarDB(ByVal _conexion As String) As Boolean
        Try
            If testConexion(_conexion) Then
                ConexionString = _conexion
                Return True
            End If
            ConexionString = ""
        Catch ex As Exception
        End Try

        Return False

    End Function

    Public Function testConexion(ByVal _conexion As String) As Boolean
        Try

            Dim Conexion = New MySqlConnection(_conexion)
            Conexion.Open()
            Dim cmd As New MySqlCommand("SELECT * FROM configuracion", Conexion)
            Dim dt As New DataTable
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dt)
            Conexion.Close()
            Return True
        Catch ex As MySqlException
            Return False
        End Try
    End Function

End Class
