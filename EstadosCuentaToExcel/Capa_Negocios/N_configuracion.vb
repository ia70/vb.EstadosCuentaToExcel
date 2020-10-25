Imports Capa_Datos
Imports Capa_Identidad

Public Class N_configuracion
    Private tabla As String = "configuracion"

    Public Function getConfiguracion() As I_configuracion
        Dim db As New D_db_operaciones(tabla)
        Dim res As DataTable

        res = db.Consulta("1")

        Return Nothing
    End Function

    Public Function setConfiguracion(ByVal obj As I_configuracion) As Boolean
        Dim db As New D_db_operaciones(tabla)
        delconfiguracion()

        Return db.Insertar(obj)

    End Function

    Private Sub delconfiguracion()
        Dim db As New D_db_operaciones(tabla)
        db.Eliminar(1)

    End Sub

End Class
