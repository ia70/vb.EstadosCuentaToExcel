Imports Capa_Datos
Imports Capa_Identidad

Public Class N_Configuracion
    Private tabla As String = "configuracion"

    Public Function Consultar() As I_configuracion
        Dim db As New D_db_operaciones(tabla)
        Dim iden As New I_configuracion
        Dim res As DataTable

        res = db.Consulta("1")

        Try
            With iden
                .Id = GetInt(res.Rows(0).Item(0))
                .Folder_in = GetStr(res.Rows(0).Item(1))
                .Folder_out = GetStr(res.Rows(0).Item(2))
                .Copia_origen = GetBool(res.Rows(0).Item(3))
            End With
        Catch ex As Exception
            X(ex)
        End Try

        Return iden
    End Function

    Public Function Editar(ByVal obj As I_configuracion) As Boolean
        Dim db As New D_db_operaciones(tabla)
        Eliminar()

        Return db.Insertar(obj)

    End Function

    Private Function Eliminar()
        Dim db As New D_db_operaciones(tabla)

        Return db.Eliminar(1)

    End Function

End Class
