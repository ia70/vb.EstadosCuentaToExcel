﻿Imports Capa_Negocios

Public Class GUI_ini
    Private Sub GUI_ini_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim db As New N_conexion
        Try
            G_DB_Inicializada = CargarVariablesGlobales()
            GUI_Configuracion.Show()
            'Test.Show()
        Catch ex As Exception
            X(ex)
            End
        End Try

        Close()
    End Sub

End Class