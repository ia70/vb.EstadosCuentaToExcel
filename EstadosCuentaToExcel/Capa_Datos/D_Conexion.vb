Imports MySql.Data.MySqlClient

Public Class D_Conexion
    Dim conexion As New MySqlConnection

    Public Function conectar() As MySqlConnection
        conexion.ConnectionString = "server=127.0.0.1;user id=root;password=;database=xrestadoscuentas"
        Return conexion
    End Function
End Class
