Imports Capa_Identidad
Public Class N_Algoritmo

#Region "VARIABLES"
    Private _textopdf As String         'Representa el archivo PDF en formato de texto plano.
    Private _fichero As I_Archivo       'Identidad del finchero.
    Private _formato As I_Formato       'Identidad del formato.
    Private _no_campos As Integer       'Número de campos que tiene el formato.

    Private _no_cifras As Integer       'Número de campos de tipo decimal que tiene el formato.
    Private _no_cadenas As Integer      'Número de campos de tipo decimal que tiene el formato.
    Private _no_fechas As Integer       'Número de campos de tipo decimal que tiene el formato.
    Private _size_fecha As Integer      'Longitud de fecha de Operacion/Liquidacion del formato.
    Private _ruta_guardado As String    'Ruta donde se guardará el archivo.
    Private _ultimoSaldo As Decimal     'Ultimi saldo del linea del formato

#End Region
#Region "CONSTRUCTORES"

    'Public Sub New()
    'End Sub

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="textopdf_">PDF en texto plano</param>
    ''' <param name="formato_">Identidad del formato</param>
    ''' <param name="ruta_">Ruta donde se guardará el archivo.</param>
    Public Sub New(ByVal textopdf_ As String, ByVal formato_ As I_Formato)
        Try
            _textopdf = textopdf_
            _fichero = New I_Archivo(formato_.Campos)
            _formato = formato_
        Catch ex As Exception
            X(ex)
        End Try

        Try
            CargarVariables()
            GetSizeFecha()
        Catch ex As Exception
            X(ex)
        End Try

    End Sub

#End Region
#Region "PROPIEDADES"

    Public Property Textopdf As String
        Get
            Return _textopdf
        End Get
        Set(value As String)
            _textopdf = value
        End Set
    End Property

    Public Property Fichero As I_Archivo
        Get
            Return _fichero
        End Get
        Set(value As I_Archivo)
            _fichero = value
        End Set
    End Property

    Public Property Formato As I_Formato
        Get
            Return _formato
        End Get
        Set(value As I_Formato)
            _formato = value
        End Set
    End Property

    Public Property No_campos As Integer
        Get
            Return _no_campos
        End Get
        Set(value As Integer)
            _no_campos = value
        End Set
    End Property

    Public Property No_cifras As Integer
        Get
            Return _no_cifras
        End Get
        Set(value As Integer)
            _no_cifras = value
        End Set
    End Property

    Public Property No_cadenas As Integer
        Get
            Return _no_cadenas
        End Get
        Set(value As Integer)
            _no_cadenas = value
        End Set
    End Property

    Public Property No_fechas As Integer
        Get
            Return _no_fechas
        End Get
        Set(value As Integer)
            _no_fechas = value
        End Set
    End Property

    Public Property Size_fecha As Integer
        Get
            Return _size_fecha
        End Get
        Set(value As Integer)
            _size_fecha = value
        End Set
    End Property

    Public Property Ruta_guardado As String
        Get
            Return _ruta_guardado
        End Get
        Set(value As String)
            _ruta_guardado = value
        End Set
    End Property

#End Region
#Region "FUNCIONES"
#Region "FORMATEAR TEXTO"

    ''' <summary>
    ''' Se encarga de quitar todo lo que no es el cuerpo del estado de cuenta
    ''' </summary>
    ''' <param name="cadena">cadena a limpiar</param>
    ''' <returns></returns>
    Private Function LimpiarTexto(ByVal cadena As String) As String
        Dim indice As Integer


        Try
            'RECUPERAR DATOS GLOBALES
            With _fichero
                .Rfc = GetRFC()
                .Fecha = GetFecha()
                .Saldo_inicial = GetSaldoInicial()
                .No_cuenta = GetNoCuenta()
            End With

        Catch ex As Exception
            X(ex)
        End Try

        Try
            'QUITAR ENCABEZADO
            indice = cadena.IndexOf(Formato.Prefijos.Detalles_saldo.Inicio)
            If indice >= 0 Then
                cadena = cadena.Substring(indice + Formato.Prefijos.Detalles_saldo.Inicio.Length)
            End If


            'QUITAR PIE
            For Each linea As I_Prefijo_simple In Formato.Prefijos.Fin_documento
                indice = cadena.IndexOf(linea.Fin)
                If indice >= 0 Then
                    cadena = cadena.Substring(0, indice)
                    Exit For
                End If
            Next

            'QUITAR PARCIAL
            For Each linea As I_Prefijo_simple In Formato.Prefijos.Ignora_parcial
                cadena = LimpiaParcial(cadena, linea.Inicio, linea.Fin)
            Next

        Catch ex As Exception
            X(ex)
        End Try

        Return cadena
    End Function

    ''' <summary>
    ''' Se encarga de quitar texto adicional parcial en el cuerpo del formato
    ''' </summary>
    ''' <param name="cadena">cuerpo del formato</param>
    ''' <param name="ini">inicio a ignorar</param>
    ''' <param name="fin">fin a ignorar</param>
    ''' <returns></returns>
    Private Function LimpiaParcial(ByVal cadena As String, ByVal ini As String, ByVal fin As String) As String
        Dim aux = "", aux3 = "", aux2 As String
        Dim indice, indice2 As Integer

        Try
            If cadena.Length > 0 Then
                If ini.Length > 0 And fin.Length > 0 Then

                    'QUITAR PARCIAL
                    While cadena.IndexOf(ini) >= 0
                        indice = cadena.IndexOf(ini)

                        If indice >= 0 Then
                            aux = cadena.Substring(0, indice)
                        End If

                        indice2 = cadena.IndexOf(fin)
                        If indice2 >= 0 And indice2 > indice Then
                            aux3 += aux
                            cadena = cadena.Substring(indice2 + fin.Length)
                        ElseIf indice2 < indice And indice2 >= 0 Then
                            aux = cadena.Substring(0, indice2 + fin.Length)
                            aux3 += aux
                            cadena = cadena.Substring(indice2 + fin.Length)
                        Else
                            aux3 = aux
                            cadena = ""
                            Exit While
                        End If
                        aux = ""
                        aux2 = ""
                    End While
                    aux3 += cadena
                Else
                    Return cadena
                End If
            End If
        Catch ex As Exception
            X(ex)
        End Try

        Return aux3
    End Function

    ''' <summary>
    ''' Inserta saltos de linea en la descripción de cada linea del formato.
    ''' </summary>
    ''' <param name="cadena">Descripcion/concepto</param>
    ''' <param name="salto">Número de caracteres para cada salto</param>
    ''' <returns></returns>
    Private Function InsertarSaltoslinea(ByVal cadena As String, ByVal salto As Integer) As String
        Dim copy, aux As String
        Dim i As Integer
        Dim res As String
        copy = cadena

        res = ""
        If salto > 10 Then
            Try
                While cadena.Length > salto
                    i = salto
                    For i = salto To 10 Step -1
                        aux = cadena(i)
                        If aux = " " Then
                            res += cadena.Substring(0, i) + vbLf
                            cadena = cadena.Substring(i + 1)
                            Exit For
                        End If
                    Next

                End While
                res += cadena
            Catch ex As Exception
                X(ex)
                Return copy
            End Try
            Return res
        Else
            Return cadena
        End If
    End Function

#End Region
#Region "FUNCIONES DE LINEA DE FORMATO"

    ''' <summary>
    ''' Devuelve lista de String con cada uno de los campos de la linea
    ''' </summary>
    ''' <param name="cadena"></param>
    ''' <returns></returns>
    Private Function ProcesarLinea(ByVal cadena As String) As List(Of String)
        Dim cifras As New List(Of String)
        Dim fechas As New List(Of String)
        Dim cadenas As New List(Of String)
        Dim operacion As String
        Dim SaldoLocal As Decimal

        'VARIABLES --------------------
        Dim Fecha_Operacion As String = ""
        Dim Fecha_Liquidacion As String = ""
        Dim Folio As String = ""
        Dim Concepto As String = ""
        Dim Referencia As String = ""
        Dim Retiro As String = ""
        Dim Deposito As String = ""
        Dim Saldo_Operacion As String = ""
        Dim Saldo_Liquidacion As String = ""

        Dim respuesta As New List(Of String)

        Dim numero, copy, aux As String
        Dim i, indice As Integer

        copy = cadena
        '///  OBTENER CIFRAS   /////////////////////////////////////////////////////////////////////////////////
        Try
            Do
                numero = GetCantidad(cadena)
                If numero.Length > 0 Then
                    cifras.Add(numero)
                    indice = cadena.IndexOf(numero)
                    cadena = cadena.Remove(indice, numero.Length)
                    i += 1
                Else
                    indice = cadena.IndexOf(".")
                    If indice >= 0 Then
                        cadena = cadena.Substring(indice + 1)
                    Else
                        Exit Do
                    End If
                End If
            Loop While cadena.Length > 0
        Catch ex As Exception
            X(ex)
        End Try
        cadena = copy

        '///  QUITAR CIFRAS DE CADENA   //////////////////////////////////////////////////////////////////////////
        Try
            For Each linea As String In cifras
                indice = cadena.IndexOf(linea)
                If indice >= 0 Then
                    cadena = cadena.Remove(indice, linea.Length)
                End If
            Next
        Catch ex As Exception
            X(ex)
        End Try

        'QUITAR FECHA -------------------------------------------------
        Try
            For i = 0 To _no_fechas - 1
                aux = cadena.Substring(0, _size_fecha)
                fechas.Add(aux)
                cadena = cadena.Substring(aux.Length + 1)
            Next
        Catch ex As Exception
            X(ex)
        End Try

        'ELIMINA SALTOS DE LINEA Y ESPACIOS EN BLANCOS DUPLICADOS
        cadena = cadena.Replace(vbLf, " ")
        cadena = cadena.Replace("$", " ")
        While cadena.Contains("  ")
            cadena = cadena.Replace("  ", " ")
        End While
        'ELimina espacios al inicio de cadena
        cadena = RemoveEspaciosInicio(cadena)

        cadena = InsertarSaltoslinea(cadena, 100)

        'OBTIENE FOLIO
        If Not IsNothing(Formato.Prefijos.Folio) Then
            aux = cadena.Substring(0, Formato.Prefijos.Folio.Size + 1)
            If IsNumeric(aux) Then
                Folio = aux
                cadena = cadena.Remove(0, Formato.Prefijos.Folio.Size + 1)
            End If
        End If

        'OBTIENE CADENAS --------------------------------------------
        cadenas.Add(cadena)

        'ORDENAMIENTO DE CAMPOS OBTENIDOS +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        Try
            'Fechas
            If Not IsNothing(fechas) Then
                Fecha_Operacion = fechas(0)
                If fechas.Count = 2 Then
                    Fecha_Liquidacion = fechas(1)
                End If
            End If

            'Cadenas
            If Not IsNothing(cadenas) Then
                Concepto = cadenas(0)
                If cadenas.Count = 2 Then
                    Referencia = cadenas(1)
                End If
            End If

            'Saldo de Operacion / Liquidacion
            If Not IsNothing(cifras) Then
                If cifras.Count = 1 And Concepto.Contains("SALDO") Then
                    Saldo_Operacion = cifras(0)
                ElseIf cifras.Count = 2 Then
                    Saldo_Operacion = cifras(1)
                ElseIf cifras.Count = 3 Then
                    Saldo_Operacion = cifras(1)
                    Saldo_Liquidacion = cifras(2)
                End If
            End If

            'DEFINE TIPO DE TRANSACCION INGRESO/EGRESO
            operacion = GetOperacion(cadena)
            If operacion = "" Then
                Try
                    If _ultimoSaldo >= 0 Then
                        SaldoLocal = Convert.ToDecimal(Saldo_Operacion)
                        If SaldoLocal > _ultimoSaldo Then
                            operacion = "Deposito"
                        ElseIf SaldoLocal < _ultimoSaldo Then
                            operacion = "Retiro"
                        End If
                    End If
                Catch ex As Exception
                End Try
            End If

            'Cifras
            If Not IsNothing(cifras) Then
                If operacion = "Deposito" Then
                    Deposito = cifras(0)
                ElseIf operacion = "Retiro" Then
                    Retiro = cifras(0)
                End If
            End If

            'AQUI SE DEFINE SI SE PUDO DETERMINAR EL TIPO DE OPERACION (INGRESO | EGRESO)



            '/////////////////////////////////////////////////////////////////////////////////////////////////////////

            'MANEJAR LA INFORMACION
            For Each campo As I_Campos In _formato.Campos

                Select Case campo.Idcampo.ToUpper
                    Case "FECHAOPERACION"
                        respuesta.Add(Fecha_Operacion)
                    Case "FECHALIQUIDACION"
                        respuesta.Add(Fecha_Liquidacion)
                    Case "FOLIO"
                        respuesta.Add(Folio)
                    Case "CONCEPTO"
                        respuesta.Add(Concepto)
                    Case "REFERENCIA"
                        respuesta.Add(Referencia)
                    Case "RETIRO"
                        respuesta.Add(Retiro)
                    Case "DEPOSITO"
                        respuesta.Add(Deposito)
                    Case "SALDOOPERACION"
                        respuesta.Add(Saldo_Operacion)
                        Try
                            _ultimoSaldo = Convert.ToDecimal(Saldo_Operacion)
                        Catch ex As Exception
                            _ultimoSaldo = -1
                        End Try
                    Case "SALDOLIQUIDACION"
                        respuesta.Add(Saldo_Liquidacion)
                    Case Else
                        respuesta.Add("")
                End Select
            Next

            If operacion = "" And Not Concepto.Contains("SALDO") Then
                respuesta.Add("x")
            Else
                respuesta.Add("")
            End If
        Catch ex As Exception
            X(ex)
        End Try

        Return respuesta
    End Function

    ''' <summary>
    ''' Obtiene el indice dentro de cadena de una fecha valida
    ''' </summary>
    ''' <param name="cadena"></param>
    ''' <param name="size"></param>
    ''' <returns></returns>
    Private Function GetFechaIndice(ByVal cadena As String, ByVal size As Integer) As Integer
        Dim indice As Integer
        Dim aux, copy As String

        copy = cadena
        'QUITAR ESPACIOS EN BLANCO AL COMIENZO DE LINEA
        cadena = RemoveEspaciosInicio(cadena)

        If size = 2 Then
            size = 3
        End If

        Try
            aux = cadena.Substring(0, size)
            Do
                If VerificarFecha(aux) Then
                    indice = copy.IndexOf(aux)
                    Return indice
                End If

                indice = cadena.IndexOf(vbLf)
                If indice >= 0 Then
                    Try
                        cadena = cadena.Substring(indice + 1)
                        cadena = RemoveEspaciosInicio(cadena)
                        aux = cadena.Substring(0, size)
                    Catch ex As Exception
                        Return -1
                    End Try
                End If
            Loop While indice >= 0

            Return -1
        Catch ex As Exception
            Return -1
        End Try

    End Function

    ''' <summary>
    ''' Elimina los espacios en blanco al inicio de la cadena
    ''' </summary>
    ''' <param name="cadena"></param>
    ''' <returns></returns>
    Private Function RemoveEspaciosInicio(ByVal cadena As String) As String
        Dim copy As String

        copy = cadena
        Try
            If cadena.Length > 0 Then
                While cadena(0) = " "
                    cadena = cadena.Substring(1)
                    If cadena.Length > 0 Then
                        Exit While
                    End If
                End While
            End If
            Return cadena
        Catch ex As Exception
            Return copy
        End Try
    End Function

    ''' <summary>
    ''' Obtiene la primer cifra encontrada en texto de descripcion.
    ''' </summary>
    ''' <param name="cadena"></param>
    ''' <returns></returns>
    Private Function GetCantidad(ByVal cadena As String) As String
        Dim copy As String
        Dim indice, ini, fin, i, size As Integer
        Dim numero As String

        Try
            copy = cadena
            size = cadena.Length
            indice = cadena.IndexOf(".")

            Do
                If indice >= 0 Then
                    i = indice - 1
                    While (i >= 0 Or Not cadena(i) = " ") And (IsNumeric(cadena(i)) Or cadena(i) = ",")
                        i -= 1
                    End While
                    If i >= 0 And IsNumeric(cadena(i + 1)) And cadena(i) = " " Then
                        ini = i + 1
                        If IsNumeric(cadena(indice + 1)) And IsNumeric(cadena(indice + 2)) Then
                            If ((indice + 3) <= size - 1) Then
                                If cadena(indice + 3) = " " Or cadena(indice + 3) = vbLf Then
                                    fin = indice + 3
                                    Exit Do
                                Else
                                    Return ""
                                End If
                            Else
                                fin = indice + 3
                                Exit Do
                            End If
                        Else
                            Return ""
                        End If
                    Else
                        Return ""
                    End If
                Else
                    Return ""
                End If

                cadena = cadena.Substring(indice + 1)
                size = cadena.Length
                indice = cadena.IndexOf(".")
            Loop While indice >= 0

            cadena = cadena.Substring(ini, fin - ini)

            'VERIFICAR QUE NO SEA CIFRA FALSA -----------------
            If cadena.Length > 6 Then
                indice = cadena.Length - (6 + 1)
                If Not cadena(indice) = "," Then
                    Return ""
                End If
            End If

            numero = cadena

            Return numero
        Catch ex As Exception
            X(ex)
            Return ""
        End Try

    End Function

    ''' <summary>
    ''' Verifica si la cadena es una fecha valida segun el formato
    ''' </summary>
    ''' <param name="cadena"></param>
    ''' <returns></returns>
    Private Function VerificarFecha(ByVal cadena As String) As Boolean
        Dim aux, copy As String
        Dim veri As Boolean
        Dim size As Integer
        copy = cadena

        Try
            size = cadena.Length
        Catch ex As Exception
            X(ex)
            size = 0
        End Try

        Try
            With _formato.Prefijos.Fechas_registro(0)
                If .Dia_length > 0 Then
                    aux = cadena.Substring(0, .Dia_length)
                    If IsNumeric(aux) Then
                        cadena = cadena.Substring(.Dia_length)
                        aux = cadena.Substring(0, .Separador.Length)
                        If aux = .Separador Then
                            If .Mes_length > 0 Then
                                cadena = cadena.Substring(.Separador.Length)
                                aux = cadena.Substring(0, .Mes_length)

                                Select Case .Mes_length
                                    Case 2
                                        veri = IsNumeric(aux)
                                    Case 3
                                        veri = GetMesNum(aux).Length > 0
                                    Case >= 4
                                        veri = GetMesNum(aux).Length > 0
                                End Select

                                If veri Then
                                    If .Anio_length > 0 Then
                                        cadena = cadena.Substring(.Mes_length)
                                        aux = cadena.Substring(0, .Separador.Length)

                                        If aux = .Separador Then
                                            cadena = cadena.Substring(.Separador.Length)
                                            aux = cadena.Substring(0, .Anio_length)

                                            If IsNumeric(aux) Then
                                                cadena = cadena.Substring(.Anio_length)
                                                If cadena.Length = 0 Then
                                                    Return True
                                                End If
                                            End If
                                        End If
                                    Else
                                        cadena = cadena.Substring(.Mes_length)
                                        If cadena.Length = 0 Then
                                            Return True
                                        End If
                                    End If
                                End If
                            End If
                            Return True
                        End If
                    End If
                End If

            End With
        Catch ex As Exception
            X(ex)
        End Try

        Return False
    End Function

    ''' <summary>
    ''' Obtiene el tipo de operacion de la linea: INGRESO|EGRESO
    ''' </summary>
    ''' <param name="cadena"></param>
    ''' <returns></returns>
    Protected Overridable Function GetOperacion(ByVal cadena As String) As String
        Dim verdadero As Boolean

        Try
            For Each linea As I_Tipo_operacion In _formato.Tipo_operacion
                verdadero = True
                For Each condicion As I_Condicion In linea.Condiciones
                    If condicion.Tipo Then
                        If Not cadena.Contains(condicion.Cadena) Then
                            verdadero = False
                            Exit For
                        End If
                    Else
                        If cadena.Contains(condicion.Cadena) Then
                            verdadero = False
                            Exit For
                        End If
                    End If
                Next

                If verdadero Then
                    If linea.Tipo Then
                        Return "Deposito"
                    Else
                        Return "Retiro"
                    End If
                End If
            Next
        Catch ex As Exception
            X(ex)
        End Try

        Return ""
    End Function

#End Region
#Region "CORE"

    ''' <summary>
    ''' Procesa el texto del PDF para obtener todos los datos
    ''' </summary>
    Public Function ProcesarFichero() As Boolean
        Dim cadena As String
        Dim campos As List(Of String)
        Dim aux As String
        Dim indice As Integer
        Dim res As Boolean = True

        'Se quita toda la información que no pertenece al cuerpo del documento
        _textopdf = LimpiarTexto(_textopdf)
        cadena = _textopdf
        _ultimoSaldo = -1

        'Se comienza a procesar el cuerpo del documento
        aux = GetLinea(cadena)
        Do
            Try
                If aux.Length > 0 Then
                    campos = ProcesarLinea(aux)
                    If campos(campos.Count - 1) = "x" Then
                        res = False
                    End If

                    _fichero.agregarFila(campos)
                Else
                    Exit Do
                End If
                indice = cadena.IndexOf(aux)
                If (indice + 1 + aux.Length) > cadena.Length Then
                    aux = ""
                    Exit Do
                Else
                    cadena = cadena.Substring(indice + 1 + aux.Length)
                End If

                aux = GetLinea(cadena)
            Catch ex As Exception
                X(ex)
                aux = ""
            End Try
        Loop While aux.Length >= 0

        Return res

    End Function

    ''' <summary>
    ''' Genera y crea archivo de Excel el la ubicación especificada
    ''' </summary>
    ''' <returns></returns>
    Public Function GenerarExcel(ByVal ruta_ As String) As String
        Dim res As Boolean
        Ruta_guardado = ruta_

        Try
            Dim Exportar As New N_ExportarExcel()
            res = Exportar.Exportar(Fichero.Tabla, Formato, Ruta_guardado, Fichero)

            If res Then
                Return Exportar.GetNombreFichero
            Else
                Return ""
            End If

        Catch ex As Exception
            X(ex)
            Return ""
        End Try

    End Function

#End Region
#Region "PROCESAMIENTO GENERAL DE DATOS DEL FORMATO"

    ''' <summary>
    ''' Obtiene la primer linea valida dentro el texto del PDF
    ''' </summary>
    ''' <param name="cadena">Texto del PDF</param>
    ''' <returns></returns>
    Private Function GetLinea(cadena As String) As String
        Dim in1, in2 As Integer
        Dim copy As String
        Dim sizeFechaLocal As Integer

        'Calcular tamaño de fecha si fecha liquidacion activo
        sizeFechaLocal = _size_fecha
        If _formato.Prefijos.Fechas_registro.Count > 1 Then
            sizeFechaLocal += sizeFechaLocal + 1
        End If

        copy = cadena
        Try
            in1 = GetFechaIndice(cadena, _size_fecha)
            If in1 >= 0 Then
                cadena = cadena.Substring(in1 + sizeFechaLocal + 1)
                in2 = GetFechaIndice(cadena, _size_fecha)
                If in2 >= 0 Then
                    cadena = copy.Substring(in1, in2 + sizeFechaLocal)
                    Return cadena
                Else
                    copy = RemoveEspaciosInicio(copy)
                    Return copy
                End If
            Else
                Return ""
            End If
        Catch ex As Exception
            X(ex)
            Return ""
        End Try

    End Function

    ''' <summary>
    ''' Calcula número de campos de cada tipo segun el formato
    ''' </summary>
    Private Sub CargarVariables()
        Try
            _no_campos = _formato.Campos.Count

            For Each campo As I_Campos In _formato.Campos
                Select Case campo.Tipo.ToUpper
                    Case "DATETIME"
                        _no_fechas += 1
                    Case "STRING"
                        _no_cadenas += 1
                    Case "DECIMAL"
                        _no_cifras += 1
                End Select
            Next
        Catch ex As Exception
            X(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Obtiene la longitud de fecha para el formato activo
    ''' </summary>
    Private Sub GetSizeFecha()
        Dim size As Integer

        Try
            With _formato.Prefijos.Fechas_registro(0)

                size = .Dia_length + .Mes_length + .Anio_length
                If .Mes_length > 0 Then
                    size += .Separador.Length
                End If
                If .Anio_length > 0 Then
                    size += .Separador.Length
                End If
            End With
        Catch ex As Exception
            X(ex)
        End Try

        _size_fecha = size
    End Sub

#End Region
#Region "OBTENER CAMPOS GENERALES"

    ''' <summary>
    ''' Obtiene el RFC de una cadena
    ''' </summary>
    ''' <returns></returns>
    Private Function GetRFC() As String
        Dim cadena As String

        Try
            cadena = GetCampo(_formato.Prefijos.Rfc.Inicio, _formato.Prefijos.Rfc.Fin)
            cadena = cadena.Replace(" ", "")
        Catch ex As Exception
            X(ex)
            cadena = ""
        End Try

        Return cadena
    End Function

    ''' <summary>
    ''' Obtiene el Saldo inicial de un formato
    ''' </summary>
    ''' <returns></returns>
    Private Function GetSaldoInicial() As Decimal
        Dim saldo As String = "0"

        Try
            saldo = GetCampo(_formato.Prefijos.Saldo_anterior.Inicio, _formato.Prefijos.Saldo_anterior.Fin)
        Catch ex As Exception
            X(ex)
        End Try

        Return CDec(saldo)
    End Function

    ''' <summary>
    ''' Devuelve la fecha general del formato
    ''' OUT(Date)
    ''' </summary>
    ''' <returns></returns>
    Private Function GetFecha() As Date
        Dim vfecha, separador, aux, dia, anio, mes As String
        Dim fecha As Date

        separador = _formato.Prefijos.Fecha_general.Separador
        Try
            vfecha = GetCampo(_formato.Prefijos.Fecha_general.Inicio, _formato.Prefijos.Fecha_general.Fin)

            'Obtencion de parametros individuales --------------------------------------------------------------------
            'OBTENER DIA ---------------------
            aux = vfecha.Substring(0, 2)
            dia = aux

            'OBTENER MES ---------------------
            aux = GetCampo(vfecha, separador, separador)
            If aux.Length >= 3 Then
                mes = GetMesNum(aux)
            Else
                mes = aux
            End If

            'OBTENER ANIO --------------------
            aux = vfecha.Substring(vfecha.Length - 4)
            anio = aux

            fecha = CDate(anio & "/" & mes & "/" & dia)
        Catch ex As Exception
            X(ex)
            Return Nothing
        End Try

        Return fecha
    End Function

    ''' <summary>
    ''' Devuelve el número de cuenta del formato
    ''' </summary>
    ''' <returns></returns>
    Private Function GetNoCuenta() As String
        Dim noCuenta As String = ""

        Try
            noCuenta = GetCampo(_formato.Prefijos.No_cuenta.Inicio, _formato.Prefijos.No_cuenta.Fin)
        Catch ex As Exception
            X(ex)
        End Try

        Return noCuenta
    End Function

    ''' <summary>
    ''' Se encarga de extraer la información de un campo
    ''' </summary>
    ''' <param name="cad_ini">Prefijo de inicio</param>
    ''' <param name="cad_fin">Prefijo de final</param>
    ''' <returns></returns>
    Private Function GetCampo(ByVal cad_ini As String, cad_fin As String) As String
        Return GetCampo(_textopdf, cad_ini, cad_fin)
    End Function

    ''' <summary>
    ''' Se encarga de extraer la información de un campo
    ''' </summary>
    ''' <param name="vcadena">Cadena en la cual buscar</param>
    ''' <param name="cad_ini">Prefijo de inicio</param>
    ''' <param name="cad_fin">Prefijo de final</param>
    ''' <returns></returns>
    Private Function GetCampo(ByVal vcadena As String, ByVal cad_ini As String, cad_fin As String) As String
        Dim cadenaAux As String
        Dim ini, fin As Integer
        cadenaAux = vcadena

        Try
            ini = cadenaAux.IndexOf(cad_ini)
            If ini >= 0 Then
                cadenaAux = cadenaAux.Substring(ini + cad_ini.Length)
            End If

            fin = cadenaAux.IndexOf(cad_fin)
            If fin >= 0 Then
                cadenaAux = cadenaAux.Substring(0, fin)
            End If

            Return cadenaAux
        Catch ex As Exception
            X(ex)
            Return ""
        End Try

    End Function

#End Region
#Region "OPERACIONES CON FECHAS"

    ''' <summary>
    ''' Devuelve número del mes dado.
    ''' IN ("ENE"|"ENERO") OUT("01") EX("")
    ''' </summary>
    ''' <param name="cadena">Mes</param>
    ''' <returns></returns>
    Private Function GetMesNum(ByVal cadena As String) As String
        Dim mes As String
        cadena = cadena.ToUpper

        Try
            Select Case cadena
                Case "ENE", "ENERO"
                    mes = "01"
                Case "FEB", "FEBRERO"
                    mes = "02"
                Case "MAR", "MARZO"
                    mes = "03"
                Case "ABR", "ABRIL"
                    mes = "04"
                Case "MAY", "MAYO"
                    mes = "05"
                Case "JUN", "JUNIO"
                    mes = "06"
                Case "JUL", "JULIO"
                    mes = "07"
                Case "AGO", "AGOSTO"
                    mes = "08"
                Case "SEP", "SEPTIEMBRE"
                    mes = "09"
                Case "OCT", "OCTUBRE"
                    mes = "10"
                Case "NOV", "NOVIEMBRE"
                    mes = "11"
                Case "DIC", "DICIEMBRE"
                    mes = "12"
                Case Else
                    mes = ""
            End Select
        Catch ex As Exception
            X(ex)
            mes = ""
        End Try

        Return mes
    End Function

    ''' <summary>
    ''' Devuelve el nombre del mes dado.
    ''' IN("01") OUT("Enero") EX("")
    ''' </summary>
    ''' <param name="num">Número de mes STRING</param>
    ''' <returns></returns>
    Private Function GetMesNombre(ByVal num As String) As String
        Return GetMesNombre(Convert.ToInt32(num))
    End Function

    ''' <summary>
    ''' Devuelve el nombre del mes dado.
    ''' IN(01) OUT("Enero") EX("")
    ''' </summary>
    ''' <param name="num">Número del mes INT</param>
    ''' <returns></returns>
    Private Function GetMesNombre(ByVal num As Integer) As String
        Dim mes As String

        Select Case num
            Case 1
                mes = "Enero"
            Case 2
                mes = "Febrero"
            Case 3
                mes = "Marzo"
            Case 4
                mes = "Abril"
            Case 5
                mes = "Mayo"
            Case 6
                mes = "Junio"
            Case 7
                mes = "Julio"
            Case 8
                mes = "Agosto"
            Case 9
                mes = "Septiembre"
            Case 10
                mes = "Octubre"
            Case 11
                mes = "Noviembre"
            Case 12
                mes = "Diciembre"
            Case Else
                mes = ""
        End Select

        Return mes
    End Function

    ''' <summary>
    ''' Devuelve la abreviación del mes dado.
    ''' IN(1) OUT("ENE") EX("")
    ''' </summary>
    ''' <param name="num">Número del mes</param>
    ''' <returns></returns>
    Private Function GetMesAbreviacion(ByVal num As Integer) As String
        Dim mes As String

        Select Case num
            Case 0
                mes = "DIC"
            Case 1
                mes = "ENE"
            Case 2
                mes = "FEB"
            Case 3
                mes = "MAR"
            Case 4
                mes = "ABR"
            Case 5
                mes = "MAY"
            Case 6
                mes = "JUN"
            Case 7
                mes = "JUL"
            Case 8
                mes = "AGO"
            Case 9
                mes = "SEP"
            Case 10
                mes = "OCT"
            Case 11
                mes = "NOV"
            Case 12
                mes = "DIC"
            Case 13
                mes = "ENE"
            Case Else
                mes = ""
        End Select

        Return mes
    End Function

#End Region

#End Region
End Class
