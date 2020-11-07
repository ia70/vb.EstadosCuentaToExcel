Imports System.IO
Imports System.Threading
Imports Capa_Negocios
Imports Capa_Identidad
Imports System.Text
Imports iTextSharp.text.pdf
Imports iTextSharp.text.pdf.parser

Module mAutomatizacion
#Region "VARIABLES"
#Region "GENERAL"

    Public PROCESO_CORE As Thread                           'Proceso principal que siempre estará ejecutandose

#End Region
#Region "BUSQUEDA DE ARCHIVOS"

    Public PROCESO_BUSQUEDA As Thread                       'Proceso que almacena la función para buscar archivos
    Dim DB_FICHEROS_PROCESADOS As N_Ficheros_procesados

#End Region
#End Region
    '----------------------------------------------------------------------------------------------------
#Region "PRINCIPAL"

    ''' <summary>
    ''' Termina los subprocesos globales abiertos
    ''' </summary>
    Public Sub PROCESOS_TERMINAR()
        On Error Resume Next
        PROCESO_CORE.Abort()
        On Error Resume Next
        PROCESO_CORE.Join()
        On Error Resume Next
        PROCESO_BUSQUEDA.Abort()
        End
    End Sub

    Public Sub IniciarBusqueda()

        Try
            PROCESO_BUSQUEDA = New Thread(AddressOf Buscararchivos) 'Proceso que buscará archivos
            PROCESO_BUSQUEDA.Start()
        Catch ex As Exception
            X(ex)
        End Try

    End Sub
#End Region


#Region "BUSCAR ARCHIVOS"

    Public Sub Buscararchivos()
        DB_FICHEROS_PROCESADOS = New N_Ficheros_procesados

        'Documentación
        'https://social.msdn.microsoft.com/Forums/es-ES/56f39dbe-a524-46be-89ef-0fffcaa9706e/problema-con-ruta-de-mas-de-260-caracteres?forum=vbes
        Try
            AnalizarCarpeta("\\?\" & G_Folder_In, "\\?\" & G_Folder_In) '\\?\  -> Prefijo para solucionar error de path con mas de 250 Caracteres
        Catch ex As Exception
            X(ex)
        End Try

        Try
            PROCESOS_TERMINAR()
        Catch ex As Exception
            'X(ex)
        End Try

    End Sub

    Private Sub AnalizarCarpeta(ByVal sDir As String, ByVal CarpetaRaiz As String)
        Dim d As String
        Dim f As String

        Try
            For Each f In Directory.GetFiles(sDir, "*.*")
                Try
                    If Not DB_FICHEROS_PROCESADOS.Existe(f) Then
                        ProcesarArchivo(f)
                        DB_FICHEROS_PROCESADOS.Insertar(New I_ficheros_procesados(f))
                    End If
                Catch ex As Exception
                    X(ex)
                End Try
            Next
            Try
                For Each d In Directory.GetDirectories(sDir)
                    AnalizarCarpeta(d, CarpetaRaiz)
                Next
            Catch ex As Exception
            End Try
        Catch ex As Exception
            X(ex)
        End Try
    End Sub

#End Region

#Region "PROCESAR ARCHIVOS"

    Public Function ProcesarArchivo(ByVal archivo As String) As Boolean
        Try
            If File.Exists(archivo) Then
                Dim Cadena As String = ""
                Dim PdfReader = New PdfReader(archivo)
                Dim page As Integer
                Dim indice As Integer = 0

                For page = 1 To PdfReader.NumberOfPages
                    Dim currentText = PdfTextExtractor.GetTextFromPage(PdfReader, page, New LocationTextExtractionStrategy())

                    currentText = Encoding.UTF8.GetString(Encoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(currentText)))
                    Cadena &= currentText
                Next
                PdfReader.Close()

                For Each formato As I_formato In G_Formatos
                    If Cadena.Contains(formato.Cadena) Then
                        ProcesarFormato(Cadena, G_Formatos(indice))
                        Exit For
                    End If
                    indice += 1
                Next



                Return True
            End If
        Catch ex As Exception
        End Try

        Return False

    End Function

#End Region
#Region "ALGORITMOS DE LOS FORMATOS"

    Public Sub ProcesarFormato(ByVal cadena As String, ByVal formato As I_formato)
        Dim algoritmo As N_Algoritmo_Banamex

        Try
            algoritmo = New N_Algoritmo_Banamex(cadena, formato, G_Folder_Out)
        Catch ex As Exception

        End Try

    End Sub


#End Region

End Module
