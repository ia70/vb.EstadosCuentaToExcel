Public Class Inicio
    Private Sub Inicio_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try
            Shell("app.exe", AppWinStyle.Hide)
        Catch ex As Exception
            MsgBox("No se encontro el archivo 'app.exe'!", vbOK + vbCritical, "Incicio Estados de cuentas")
        End Try

        End
    End Sub

End Class
