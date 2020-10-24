Imports Capa_Identidad
Imports Capa_Negocios

Public Class GUI_Configuracion
    Private Sub BtnIniciarProceso_Click(sender As Object, e As EventArgs) Handles btnIniciarProceso.Click
        Dim db As New N_conexion
    End Sub

    Private Sub BtnTest_Click(sender As Object, e As EventArgs) Handles btnTest.Click
        Dim db As New N_conexion
        Dim identidad As New I_Conexion

        With identidad
            .Server = txtIP.Text
            .User_id = txtUsuario.Text
            .Password = txtPassword.Text
            .Database = txtDataBase.Text
        End With

        If db.testConexion(identidad) Then
            msg("¡Conexión exitosa!")
        Else
            msg("¡Error de conexión", 3)
        End If

    End Sub

    Private Sub BtnBuscarIn_Click(sender As Object, e As EventArgs) Handles btnBuscarIn.Click
        If dlgFolder.ShowDialog() = DialogResult.OK Then
            txtFolderIn.Text = dlgFolder.SelectedPath
        End If
    End Sub

    Private Sub BtnBuscarOut_Click(sender As Object, e As EventArgs) Handles btnBuscarOut.Click
        If dlgFolder.ShowDialog() = DialogResult.OK Then
            txtFolderOut.Text = dlgFolder.SelectedPath
        End If
    End Sub
End Class