Imports Capa_Negocios

Public Class GUI_ini
    Private Sub GUI_ini_Load(sender As Object, e As EventArgs) Handles Me.Load
        'GUI_Configuracion.Show()
        Dim db As New N_conexion

        If db.InicializarDB Then
            GUI_Configuracion.Show()
        Else
            GUI_Configuracion.Show()
        End If

        'Test.Show()
        Close()
    End Sub

End Class