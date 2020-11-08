Imports System.IO
Imports Capa_Identidad
Imports Capa_Negocios

Module mGeneral
#Region "VARIABLES GLOBALES"
    'Public Formatos As List(Of I_formato)
    Public G_EmpresaNombre As String = "13 Ponientes analistas"
    Public G_MostrarErrores As Boolean = True
    Public G_ErrorLog As Boolean = False
    Public G_DB_Inicializada As Boolean = False

    Public G_Folder_In As String = ""
    Public G_Folder_Out As String = ""
    Public G_Proceso_Activo As Boolean = False

    'VARIBLE DE FORMATOS --------------------
    Public G_Formatos As List(Of I_formato)

#End Region

#Region "FUNCIONES GENERALES"
    Public Function iniciarProceso() As Boolean
        Dim db As New N_Configuracion
        Dim db_formato As New N_Formato
        Dim db_conexion As New N_conexion
        Dim obj As I_configuracion


        Try
            If db_conexion.InicializarDB Then
                obj = db.Consultar
                G_Folder_In = obj.Folder_in
                G_Folder_Out = obj.Folder_out
                G_Formatos = db_formato.ListaCompleta
                Return True
            End If
            Return False
        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Sub Msg()
        Msg("<..>", 3)
    End Sub

    Public Sub Msg(ByVal cadena As String, Optional ByVal tipo As Integer = 1)
        Dim icono As Integer
        Select Case tipo
            Case 1
                icono = vbInformation
            Case 2
                icono = vbExclamation
            Case 3
                icono = vbCritical
            Case 4
                icono = vbQuestion
            Case Else
                icono = vbInformation
        End Select
        MsgBox(cadena, icono, G_EmpresaNombre)
    End Sub

    Public Sub X(ByVal ex As Exception)
        If (G_MostrarErrores) Then
            Msg(ex.Message, 3)
        End If
        If G_ErrorLog Then
            Dim fichero As String = "log.txt"
            Dim escritor As StreamWriter

            escritor = File.AppendText(fichero)
            escritor.Write(vbCrLf + "///////////////////////////////////////////////////////////////////////////////////" + vbCrLf)
            escritor.Write(Now.ToString + vbCrLf)
            escritor.Write(ex.Message + vbCrLf)
            escritor.Write("-------------------------------------------------------------------------------------" + vbCrLf + vbCrLf)
            escritor.Flush()
            escritor.Close()
        End If
    End Sub
#End Region

End Module
