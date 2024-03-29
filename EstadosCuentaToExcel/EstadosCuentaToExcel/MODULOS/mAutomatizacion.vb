﻿Imports System.IO
Imports System.Threading
Imports Capa_Negocios
Imports Capa_Identidad
Imports System.Text
Imports iTextSharp.text.pdf
Imports iTextSharp.text.pdf.parser
Imports Path = System.IO.Path

Module mAutomatizacion
#Region "VARIABLES"
#Region "GENERAL"
    'Public Formatos As List(Of I_formato)
    Public G_DB_Inicializada As Boolean = False

    Public G_Folder_In As String = ""
    Public G_Folder_Out As String = ""
    Public G_CopiaOrigen As Boolean = False
    Public G_Proceso_Activo As Boolean = False
    Public G_Total_ficheros_analizados As Integer = 0
    Public G_Total_ficheros_convertidos As Integer = 0
    Public G_Total_ficheros_detalles As Integer = 0
    Public G_Total_fecheros_no_convertidos As Integer = 0
    Public G_NombreFicheroDetalles As String = "ARCHIVOS CON DETALLES.txt"
    Public G_NombreFicherosNoProcesados As String = "ARCHIVOS NO CONVERTIDOS.txt"

    'VARIBLE DE FORMATOS --------------------
    Public G_Formatos As List(Of I_Formato)
    Public WithEvents FSWC As FileSystemWatcher
    Public PROCESO_CORE As Thread                           'Proceso principal que siempre estará ejecutandose

#End Region
#Region "BUSQUEDA DE ARCHIVOS"

    Public PROCESO_BUSQUEDA As Thread                       'Proceso que almacena la función para buscar archivos
    Dim DB_FICHEROS_PROCESADOS As N_Ficheros_procesados

#End Region
#End Region
    '----------------------------------------------------------------------------------------------------
#Region "PRINCIPAL"

    Public Function CargarVariablesGlobales() As Boolean
        Dim db As New N_Configuracion
        Dim db_formato As New N_Formato
        Dim db_conexion As New N_conexion
        Dim obj_config As I_Configuracion


        Try
            If db_conexion.InicializarDB Then
                obj_config = db.Consultar
                G_Folder_In = obj_config.Folder_in
                G_Folder_Out = obj_config.Folder_out
                G_CopiaOrigen = obj_config.Copia_origen
                G_Formatos = db_formato.ListaCompleta
                Return True
            End If
            Return False
        Catch ex As Exception
            Return False
        End Try

    End Function
    Public Sub DetenerProcesos()
        PROCESOS_TERMINAR()
        G_Proceso_Activo = False
        FSWC.EnableRaisingEvents = False
        Try
            PROCESO_BUSQUEDA.Abort()
        Catch ex As Exception
            X(ex)
        End Try
    End Sub
    ''' <summary>
    ''' Termina los subprocesos globales abiertos
    ''' </summary>
    Public Sub PROCESOS_TERMINAR()
        On Error Resume Next
        PROCESO_CORE.Abort()
        On Error Resume Next
        PROCESO_CORE.Join()
        AnalisisInicial()
    End Sub

    ''' <summary>
    ''' AL TERMINAR EL ANALISIS EJECUTA LO SIGUIENTE
    ''' </summary>
    Private Sub AnalisisInicial()

        Msg("FICHEROS ANALIZADOS: " + Convert.ToString(G_Total_ficheros_analizados) + vbCrLf _
            + "FICHEROS CONVERTIDOS: " + Convert.ToString(G_Total_ficheros_convertidos) + vbCrLf _
            + "FICHEROS CON DETALLES: " + Convert.ToString(G_Total_ficheros_detalles) + vbCrLf _
            + "FICHEROS NO CONVERTIDOS: " + Convert.ToString(G_Total_fecheros_no_convertidos))


        G_Total_ficheros_analizados = 0
        G_Total_ficheros_convertidos = 0
        G_Total_ficheros_detalles = 0
        G_Total_fecheros_no_convertidos = 0

        Try
            If File.Exists(G_NombreFicheroDetalles) Then
                Shell(Environ("windir") & "\System32\notepad.exe " + G_NombreFicheroDetalles, AppWinStyle.NormalFocus)
            End If
            If File.Exists(G_NombreFicherosNoProcesados) Then
                Shell(Environ("windir") & "\System32\notepad.exe " + G_NombreFicherosNoProcesados, AppWinStyle.NormalFocus)
            End If
        Catch ex As Exception

        End Try

    End Sub

    Public Sub ActivarMonitor()
        G_Proceso_Activo = True
        IniciarMonitor()
        IniciarBusqueda()
    End Sub

    Public Sub IniciarBusqueda()
        'ELIMINAR ARCHIVO PREVIO DE DETALLES ARCHIVOS
        Try
            My.Computer.FileSystem.DeleteFile(G_NombreFicheroDetalles)
        Catch ex As Exception
        End Try
        Try
            My.Computer.FileSystem.DeleteFile(G_NombreFicherosNoProcesados)
        Catch ex As Exception
        End Try

        Try
            PROCESO_BUSQUEDA = New Thread(AddressOf Buscararchivos) 'Proceso que buscará archivos
            PROCESO_BUSQUEDA.Start()
        Catch ex As Exception
            X(ex)
        End Try

    End Sub
#End Region
#Region "BUSCAR ARCHIVOS"

    ''' <summary>
    ''' Recorre Todos los archivos de una carpeta
    ''' </summary>
    Private Sub Buscararchivos()
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
            X(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Funcion recursiva para recorrer archivos de carpeta
    ''' </summary>
    ''' <param name="sDir"></param>
    ''' <param name="CarpetaRaiz"></param>
    Private Sub AnalizarCarpeta(ByVal sDir As String, ByVal CarpetaRaiz As String)
        Dim d As String
        Dim f As String

        Try
            For Each f In Directory.GetFiles(sDir, "*.*")
                Try
                    If Not DB_FICHEROS_PROCESADOS.Existe(f) Then
                        If ProcesarArchivo(f) Then
                            DB_FICHEROS_PROCESADOS.Insertar(New I_Ficheros_procesados(f))
                        End If
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
    ''' <summary>
    ''' convierte el pdf en texto
    ''' </summary>
    ''' <param name="archivo">Patch</param>
    ''' <returns></returns>
    Public Function ProcesarArchivo(ByVal archivo As String) As Boolean
        Dim ext As String

        'VALIDA LA EXTENCION DEL ARCHIVO
        Try
            ext = Path.GetExtension(archivo)
            If Not ext.ToLower = ".pdf" And Not ext = "" Then
                Return False
            End If
        Catch ex As Exception
            X(ex)
        End Try

        G_Total_ficheros_analizados += 1

        Try
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

            For Each formato As I_Formato In G_Formatos
                If Cadena.Contains(formato.Cadena) Then
                    G_Total_ficheros_convertidos += 1
                    Return ProcesarFormato(Cadena, G_Formatos(indice), archivo)
                End If
                indice += 1
            Next

            'NO RPOCESADO -------------------------------
            SetNoProcesados(archivo)

        Catch ex As Exception
            X(ex)
        End Try

        Return False
    End Function

#End Region
#Region "ALGORITMOS DE LOS FORMATOS"

    Public Function ProcesarFormato(ByVal cadena As String, ByVal formato As I_Formato, ByVal ruta_archivo As String) As Boolean
        Dim algoritmo As N_Algoritmo
        Dim res As String = ""
        Dim res_error As Boolean
        Dim rutas As List(Of String)
        Dim columna As Integer

        rutas = GetRutas(ruta_archivo)

        Try
            algoritmo = New N_Algoritmo(cadena, formato)
            res_error = algoritmo.ProcesarFichero()

            'SI EL ARCHIVO SE GUARDA DEVUELVE EL (NOMBRE) DEL FICHERO CON EXTENCION
            If rutas.Count > 0 Then
                res = algoritmo.GenerarExcel(rutas(0))
                If res.Length > 0 Then
                    If rutas.Count = 2 Then
                        CopiarArchivo(rutas(0), rutas(1), res)
                    End If
                End If
            End If

            'VERIFICAR SI TUVO DETALLES O NO
            columna = algoritmo.Fichero.Tabla.Columns.Count - 1
            For Each fila As DataRow In algoritmo.Fichero.Tabla.Rows
                Try
                    If fila.Item(columna).ToString = "x" Then
                        SetConDetalles(ruta_archivo, Path.Combine(rutas(0), res))
                        Exit For
                    End If
                Catch ex As Exception
                End Try
            Next

            Return res_error
        Catch ex As Exception
            X(ex)
            Return False
        End Try

    End Function

    Private Sub SetNoProcesados(ByVal origen As String)
        Try
            Dim fichero As String = G_NombreFicherosNoProcesados
            Dim escritor As StreamWriter
            Dim indice As Integer

            Try
                indice = origen.IndexOf("C:")
                If indice >= 0 Then
                    origen = origen.Substring(indice)
                End If
            Catch ex As Exception
            End Try


            G_Total_fecheros_no_convertidos += 1

            escritor = File.AppendText(fichero)
            escritor.Write(vbCrLf + "--------------------------------------------------------------------------------------------" + vbCrLf)
            escritor.Write(origen + vbCrLf)
            escritor.Flush()
            escritor.Close()
        Catch ex2 As Exception
        End Try
    End Sub

    Private Sub SetConDetalles(ByVal origen As String, ByVal destino As String)
        Try
            Dim fichero As String = G_NombreFicheroDetalles
            Dim escritor As StreamWriter
            Dim indice As Integer

            Try
                indice = origen.IndexOf("C:")
                If indice >= 0 Then
                    origen = origen.Substring(indice)
                End If
            Catch ex As Exception

            End Try


            G_Total_ficheros_detalles += 1

            escritor = File.AppendText(fichero)
            escritor.Write(vbCrLf + "--------------------------------------------------------------------------------------------" + vbCrLf)
            escritor.Write(origen + vbCrLf)
            escritor.Write(destino + vbCrLf)
            escritor.Flush()
            escritor.Close()
        Catch ex2 As Exception
        End Try
    End Sub

    Private Sub CopiarArchivo(ByVal origen As String, ByVal destino As String, ByVal nombre As String)

        Try
            Thread.Sleep(1000)
            File.Copy(Path.Combine(origen, nombre), Path.Combine(destino, nombre), True)
        Catch ex As Exception
            X(ex)
        End Try

    End Sub

    Private Function GetRutas(ByVal ruta_archivo As String) As List(Of String)
        Dim res As New List(Of String)
        Dim folder As String

        Try
            If G_Folder_Out.Length > 0 Then
                res.Add(G_Folder_Out)
            End If
            If G_CopiaOrigen Then
                folder = Path.GetDirectoryName(ruta_archivo)
                res.Add(folder)
            End If

        Catch ex As Exception
            X(ex)
        End Try

        Return res
    End Function


#End Region
#Region "MONITOR DE ARCHIVOS"
    Public Sub IniciarMonitor()
        Try
            FSWC = New FileSystemWatcher(G_Folder_In) With {
                .IncludeSubdirectories = True,
                .EnableRaisingEvents = True
            }
        Catch ex As Exception
            X(ex)
        End Try

    End Sub

#End Region
#Region "FUNCIONES PRIVADAS"

    ''' <summary>
    ''' Evento que se dispara cuando se crea un nuevo archivo en la carpeta
    ''' monitoreada
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FSWC_Created(sender As Object, e As FileSystemEventArgs) Handles FSWC.Created
        Try
            If Not DB_FICHEROS_PROCESADOS.Existe(e.FullPath) Then
                If ProcesarArchivo(e.FullPath) Then
                    DB_FICHEROS_PROCESADOS.Insertar(New I_Ficheros_procesados(e.FullPath))
                End If
            End If
        Catch ex As Exception
            X(ex)
        End Try
    End Sub
#End Region
End Module
