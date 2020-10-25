Imports Capa_Identidad
Imports Capa_Negocios

Public Class GUI_Configuracion

    Private Sub GUI_Configuracion_Load(sender As Object, e As EventArgs) Handles Me.Load
        Inicializar()
    End Sub

    Private Sub BtnIniciarProceso_Click(sender As Object, e As EventArgs) Handles btnIniciarProceso.Click
        Dim db As New N_conexion
    End Sub

    Private Sub Inicializar()
        Dim db_conexion As New N_conexion
        Dim iden_conexion As I_Conexion

        iden_conexion = db_conexion.getConexionInfo

        If Not IsNothing(iden_conexion) Then
            txtIP.Text = iden_conexion.Server
            txtUsuario.Text = iden_conexion.User_id
            txtPassword.Text = iden_conexion.Password
            txtDataBase.Text = iden_conexion.Database
        End If

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

    Private Sub BtnImportar_Click(sender As Object, e As EventArgs) Handles btnImportar.Click
        If dlgFile.ShowDialog() = DialogResult.OK Then
            txtFolderOut.Text = dlgFile.FileName
        End If
    End Sub

    Private Sub BtnPruebas_Click(sender As Object, e As EventArgs) Handles btnPruebas.Click
        Dim db As New N_configuracion
        Dim iden As I_configuracion
        iden = db.getConfiguracion()
    End Sub

    Private Sub BtnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        Dim db As New N_configuracion
        Dim ini As New N_conexion
        Dim iden_config As New I_configuracion
        Dim iden_conexion As New I_Conexion

        With iden_conexion
            .Server = txtIP.Text
            .User_id = txtUsuario.Text
            .Password = txtPassword.Text
            .Database = txtDataBase.Text
        End With

        ini.setConexionInfo(iden_conexion)

        If ini.InicializarDB() Then
            With iden_config
                .Id = 1
                .Folder_in = txtFolderIn.Text
                .Folder_out = txtFolderOut.Text
            End With

            If db.setConfiguracion(iden_config) Then
                msg("¡Configuración guardada!")
            Else
                msg("¡Error!", 3)
            End If
        Else
            msg("¡Error de conexión a la base de datos!", 3)
        End If

    End Sub


End Class