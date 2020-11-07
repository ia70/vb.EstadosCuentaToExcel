
Imports Capa_Identidad

Public Class N_Algoritmo_Banamex
    Inherits N_Algoritmo

    Private elementos, no_decimales, no_cadenas, no_fechas As Integer
    Private size_fecha As Integer

    Public Sub New()
    End Sub

    Public Sub New(cadena As String, formato As I_formato)
        MyBase.New(cadena, formato)
    End Sub

    Protected Overrides Sub GetDatos()
        Dim cadena As String = _cadena
        Dim aux As String
        Dim indice As Integer

        CargarVariables()
        aux = GetLinea(cadena)
        Do
            Try

                Archivo.agregarFila(ProcesarLinea(aux))
                indice = cadena.IndexOf(aux)
                cadena = cadena.Substring(indice + aux.Length)
                aux = GetLinea(cadena)
            Catch ex As Exception
                aux = ""
            End Try

        Loop While aux.Length >= 0

    End Sub

    Protected Overrides Function GetLinea(cadena As String) As String
        Dim size, in1, in2 As Integer
        Dim copy As String

        copy = cadena

        With _formato.Formato_global

            size = .Fecha_operacion_dia_length + .Fecha_operacion_mes_length + .Fecha_operacion_anio_length
            If .Fecha_operacion_mes_length > 0 Then
                size += .Fecha_operacion_separador_dia_mes.Length
            End If
            If .Fecha_operacion_anio_length > 0 Then
                size += .Fecha_operacion_separador_dia_mes.Length
            End If
        End With

        size_fecha = size

        in1 = GetFechaIndice(cadena, size)
        If in1 >= 0 Then
            cadena = cadena.Substring(in1 + size + 1)
            in2 = GetFechaIndice(cadena, size)
            If in2 >= 0 Then
                cadena = copy.Substring(in1, in2 + size)
                Return cadena
            Else
                Return ""
            End If
        Else
            Return ""
        End If

    End Function

    Private Function GetFechaIndice(ByVal cadena As String, ByVal size As Integer) As Integer
        Dim indice As Integer
        Dim aux, copy As String

        copy = cadena
        aux = cadena.Substring(0, size)
        Do
            If VerificarFecha(aux) Then
                indice = copy.IndexOf(aux)
                Return indice
            End If

            indice = cadena.IndexOf(vbLf)
            If indice >= 0 Then
                cadena = cadena.Substring(indice + 1)
                aux = cadena.Substring(0, size)
            End If
        Loop While indice >= 0

        Return -1
    End Function

    Private Function VerificarFecha(ByVal cadena As String) As Boolean
        Dim aux, copy As String
        Dim veri As Boolean
        copy = cadena


        With _formato.Formato_global

            If .Fecha_operacion_dia_length > 0 Then
                aux = cadena.Substring(0, .Fecha_operacion_dia_length)
                If IsNumeric(aux) Then
                    cadena = cadena.Substring(.Fecha_operacion_dia_length)
                    aux = cadena.Substring(0, .Fecha_operacion_separador_dia_mes.Length)
                    If aux = .Fecha_operacion_separador_dia_mes Then
                        If .Fecha_operacion_mes_length > 0 Then
                            cadena = cadena.Substring(.Fecha_operacion_separador_dia_mes.Length)
                            aux = cadena.Substring(0, .Fecha_operacion_mes_length)

                            Select Case .Fecha_operacion_mes_length
                                Case 2
                                    veri = IsNumeric(aux)
                                Case 3
                                    veri = GetMesNum(aux).Length > 0
                                Case >= 4
                                    veri = GetMesNum(aux).Length > 0
                            End Select

                            If veri Then
                                If .Fecha_liquidacion_anio_length > 0 Then
                                    cadena = cadena.Substring(.Fecha_operacion_mes_length)
                                    aux = cadena.Substring(0, .Fecha_operacion_separador_dia_mes.Length)

                                    If aux = .Fecha_operacion_separador_dia_mes Then
                                        cadena = cadena.Substring(.Fecha_operacion_separador_dia_mes.Length)
                                        aux = cadena.Substring(0, .Fecha_operacion_anio_length)

                                        If IsNumeric(aux) Then
                                            cadena = cadena.Substring(.Fecha_operacion_anio_length)
                                            If cadena.Length = 0 Then
                                                Return True
                                            Else
                                                Return False
                                            End If
                                        Else
                                                Return False
                                        End If

                                    Else
                                        Return False
                                    End If

                                Else
                                    cadena = cadena.Substring(.Fecha_operacion_mes_length)
                                    If cadena.Length = 0 Then
                                        Return True
                                    Else
                                        Return False
                                    End If
                                End If
                            Else
                                Return False
                            End If

                        Else
                            Return True
                        End If
                    Else
                        Return False
                    End If
                Else
                    Return False
                End If
            End If

        End With


    End Function

    Private Sub CargarVariables()
        Try
            elementos = _formato.Formato_campos.Count

            For Each campo As I_Formato_campos In _formato.Formato_campos
                Select Case campo.Tipo.ToUpper
                    Case "DATETIME"
                        no_fechas += 1
                    Case "STRING"
                        no_cadenas += 1
                    Case "DECIMAL"
                        no_decimales += 1
                End Select
            Next
        Catch ex As Exception

        End Try
    End Sub

    Private Function ProcesarLinea(ByVal cadena As String) As List(Of String)
        Dim numeros As New List(Of String)
        Dim fechas As New List(Of String)
        Dim operacion As String
        Dim descripcion As String
        Dim respuesta As New List(Of String)

        Dim numero, copy, aux As String
        Dim i, indice As Integer

        copy = cadena
        'OBTENER CIFRAS -----------------------------------------------
        Try
            Do
                numero = GetCantidad(cadena)
                If numero.Length > 0 Then
                    numeros.Add(numero)
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

        End Try
        cadena = copy

        'QUITAR CIFRAS DE CADENA --------------------------------------
        For Each linea As String In numeros
            indice = cadena.IndexOf(linea)
            If indice >= 0 Then
                cadena = cadena.Remove(indice, linea.Length)
            End If
        Next

        'QUITAR FECHA -------------------------------------------------
        For i = 0 To no_fechas - 1
            aux = cadena.Substring(0, size_fecha)
            fechas.Add(aux)
            cadena = cadena.Substring(aux.Length + 1)
        Next

        'ELIMINA SALTOS DE LINEA Y ESPACIOS EN BLANCOS DUPLICADOS
        cadena = cadena.Replace(vbLf, " ")
        cadena = cadena.Replace("   ", " ")
        cadena = cadena.Replace("  ", " ")
        descripcion = cadena

        'DEFINE TIPO DE TRANSACCION INGRESO/EGRESO
        operacion = GetOperacion(descripcion)

        'MANEJAR LA INFORMACION
        For Each campo As I_Formato_campos In _formato.Formato_campos

            Select Case campo.Tipo.ToUpper
                Case "DATETIME"
                    respuesta.Add(fechas(0))
                    fechas.RemoveAt(0)
                Case "STRING"
                    respuesta.Add(descripcion)
                Case "DECIMAL"
                    respuesta.Add(fechas(0))
                    fechas.RemoveAt(0)
            End Select





        Next

        Return respuesta
    End Function

    Private Function GetOperacion(ByVal cadena As String) As String

        Try
            For Each campo As I_Formato_campo_ingreso In _formato.Formato_campo_ingreso
                If cadena.Contains(campo.Cadena) Then
                    Return "Ingreso"
                End If
            Next
        Catch ex As Exception

        End Try

        Try
            For Each campo As I_Formato_campo_egreso In _formato.Formato_campo_egreso
                If cadena.Contains(campo.Cadena) Then
                    Return "Egreso"
                End If
            Next
        Catch ex As Exception

        End Try

        Return ""

    End Function

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
                    If i >= 0 And (IsNumeric(cadena(i + 1)) Or cadena(i + 1) = ",") Then
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

            numero = cadena

            Return numero
        Catch ex As Exception
            Return ""
        End Try

    End Function

End Class
