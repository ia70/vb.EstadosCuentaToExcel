Imports Capa_Identidad
Imports Capa_Negocios

Public Class GUI_Configuracion

    Private Sub GUI_Configuracion_Load(sender As Object, e As EventArgs) Handles Me.Load

#Region "REGISTRO DE WINDOWS"
        Dim cadena As String

        Try
            cadena = My.Computer.Registry.CurrentUser.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Run", True).GetValue(Application.ProductName).ToString
            If Not cadena Is Nothing Then
                btnInicioWindows.Checked = True
            Else
                btnInicioWindows.Checked = False
            End If
        Catch ex As Exception
            'X(ex)
        End Try

#End Region
        Try
            If Not G_Proceso_Activo Then
                If G_Formatos.Count > 0 Then
                    G_Proceso_Activo = True
                    Inicializar()
                    Me.WindowState = FormWindowState.Minimized
                    ActivarMonitor()
                Else
                    Inicializar()
                End If
            Else
                Inicializar()
            End If
        Catch ex As Exception
            X(ex)
        End Try
    End Sub

    Private Sub BtnIniciarProceso_Click(sender As Object, e As EventArgs) Handles btnIniciarProceso.Click
        Try
            If G_Proceso_Activo Then
                DetenerProcesos()
                Msg("Proceso detenido!", 2)
            Else
                ActivarMonitor()
                Msg("Proceso iniciado!")
            End If

            Inicializar()
        Catch ex As Exception
            X(ex)
        End Try

    End Sub

    Private Sub Inicializar()
        Dim db_conexion As New N_conexion
        Dim iden_conexion As I_Conexion
        Dim db_config As New N_Configuracion
        Dim iden_config As I_configuracion
        Dim db_formato As New N_Formato

        'GET CONECCION INFO -----------------------------
        Try
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

            'BOTON INICIAR PROCESO --------------------------
            If G_Proceso_Activo Then
                btnIniciarProceso.BackColor = Color.OrangeRed
                btnIniciarProceso.Text = "Detener Proceso"
            Else
                btnIniciarProceso.BackColor = Color.DodgerBlue
                btnIniciarProceso.Text = "Iniciar Proceso"
            End If
        Catch ex As Exception
            X(ex)
        End Try

    End Sub

    Private Sub BtnTest_Click(sender As Object, e As EventArgs) Handles btnTest.Click
        Dim db As New N_conexion
        Dim identidad As New I_Conexion

        Try
            With identidad
                .Server = txtIP.Text
                .User_id = txtUsuario.Text
                .Password = txtPassword.Text
                .Database = txtDataBase.Text
            End With

            If db.testConexion(identidad) Then
                Msg("¡Conexión exitosa!")
            Else
                Msg("¡Error de conexión", 3)
            End If
        Catch ex As Exception
            X(ex)
        End Try

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

        Try
            If dlgFile.ShowDialog() = DialogResult.OK Then
                Try
                    If db.Insertar(dlgFile.FileName) Then
                        Inicializar()
                        Msg("¡Formato guardado!")
                    Else
                        Msg("¡Error al guardar!", 3)
                    End If
                Catch ex As Exception

                End Try
            End If
        Catch ex As Exception
            X(ex)
        End Try

    End Sub

    Private Sub BtnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        Dim db As New N_configuracion
        Dim ini As New N_conexion
        Dim iden_config As New I_configuracion
        Dim iden_conexion As New I_Conexion

        Try
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
                    Msg("¡Configuración guardada!")
                Else
                    Msg("¡Error!", 3)
                End If
            Else
                Msg("¡Error de conexión a la base de datos!", 3)
            End If
        Catch ex As Exception
            X(ex)
        End Try
    End Sub

    Private Sub BtnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        Dim lineas As Integer
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
            X(ex)
        End Try
    End Sub


    Private Sub Notificacion_MouseClick(sender As Object, e As MouseEventArgs) Handles Notificacion.MouseClick
        Try
            If Me.WindowState = FormWindowState.Minimized Then
                WindowState = FormWindowState.Normal
            Else
                WindowState = FormWindowState.Minimized
            End If
        Catch ex As Exception
            X(ex)
        End Try

    End Sub

#Region "FUNCIONES ADICIONALES"
    Private Sub EscribirRegistro()
        '------ CLAVE DEL PROGRAMA ------
        If My.Computer.Registry.CurrentUser.OpenSubKey("SOFTWARE\Xcoru", True) Is Nothing Then
            On Error Resume Next
            My.Computer.Registry.CurrentUser.CreateSubKey("SOFTWARE\Xcoru")
        End If
        If My.Computer.Registry.CurrentUser.OpenSubKey("SOFTWARE\Xcoru\EstadosDeCuentas", True) Is Nothing Then
            On Error Resume Next
            My.Computer.Registry.CurrentUser.CreateSubKey("SOFTWARE\Xcoru\EstadosDeCuentas")
        End If


        '------ INICO CON WINDOWS ----------
        If btnInicioWindows.Checked Then
            On Error Resume Next
            My.Computer.Registry.CurrentUser.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Run", True).SetValue(Application.ProductName, Application.StartupPath & "\play.exe")
            Msg("Inicio con windows activado!")
        Else
            On Error Resume Next
            My.Computer.Registry.CurrentUser.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Run", True).DeleteValue(Application.ProductName)
            Msg("Inicio con windows desactivado!", 2)
        End If
    End Sub

    Private Sub BtnInicioWindows_Click(sender As Object, e As EventArgs) Handles btnInicioWindows.Click
        EscribirRegistro()
    End Sub
#End Region
End Class