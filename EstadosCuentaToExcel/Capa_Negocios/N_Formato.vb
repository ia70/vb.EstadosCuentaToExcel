Imports Capa_Datos
Imports Capa_Identidad
Public Class N_Formato
    Private tabla As String = "formato"

    Public Function Lista() As DataTable
        Dim db As New D_db_operaciones(tabla)

        Return db.Lista

    End Function

    Public Function Consultar(ByVal id As String) As I_formato
        Dim db As New D_db_operaciones(tabla)
        Dim iden_config As New I_formato
        Dim res As DataTable

        res = db.Consulta(id)

        With iden_config
            .Id = res.Rows(0).Item(0)
            .Folder_in = res.Rows(0).Item(1)
            .Folder_out = res.Rows(0).Item(2)
        End With

        Return iden_config
    End Function

    Public Function Editar(ByVal obj As I_formato) As Boolean
        Dim db As New D_db_operaciones(tabla)


        Return db.Insertar(obj)

    End Function

    Public Function Eliminar()
        Dim db As New D_db_operaciones(tabla)

        Return db.Eliminar(1)

    End Function
End Class
