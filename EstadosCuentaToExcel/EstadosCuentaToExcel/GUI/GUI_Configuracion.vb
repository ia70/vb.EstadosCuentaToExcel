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
        Dim db_config As New N_Configuracion
        Dim iden_config As I_configuracion
        Dim db_formato As New N_Formato

        'GET CONECCION INFO -----------------------------
        iden_conexion = db_conexion.getConexionInfo

        If Not IsNothing(iden_conexion) Then
            txtIP.Text = iden_conexion.Server
            txtUsuario.Text = iden_conexion.User_id
            txtPassword.Text = iden_conexion.Password
            txtDataBase.Text = iden_conexion.Database
        End If

        'GET CONFIGURACION INFO -------------------------
        iden_config = db_config.Consultar
        If Not IsNothing(iden_config) Then
            txtFolderIn.Text = iden_config.Folder_in
            txtFolderOut.Text = iden_config.Folder_out
        End If

        'GET FORMATOS -----------------------------------
        Try
            dgTabla.DataSource = db_formato.ListaSimple
        Catch ex As Exception
            X(ex)
        End Try

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
        Dim db As New N_Formato
        If dlgFile.ShowDialog() = DialogResult.OK Then
            Try
                If db.Insertar(dlgFile.FileName) Then
                    Inicializar()
                    msg("¡Formato guardado!")
                Else
                    msg("¡Error al guardar!", 3)
                End If
            Catch ex As Exception

            End Try
        End If
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

            If db.Editar(iden_config) Then
                msg("¡Configuración guardada!")
            Else
                msg("¡Error!", 3)
            End If
        Else
            msg("¡Error de conexión a la base de datos!", 3)
        End If

    End Sub

    Private Sub BtnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        Dim lineas As Integer = 0
        Dim indice As Integer
        Dim opcion As Integer
        Dim id As String
        Dim db As N_Formato

        Try
            lineas = dgTabla.Rows.Count
            If lineas > 0 Then
                indice = dgTabla.CurrentRow.Index
                id = dgTabla.Rows(indice).Cells(0).Value
                opcion = MsgBox("Esta seguro de eliminar el formato: '" + id + "'", vbYesNo + vbExclamation, G_EmpresaNombre)
                If opcion = vbYes Then
                    db = New N_Formato
                    If db.Eliminar(id) Then
                        dgTabla.Rows.RemoveAt(indice)
                        msg("¡Formato eliminado!")
                    Else
                        msg("¡Error al eliminar!", 3)
                    End If
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class