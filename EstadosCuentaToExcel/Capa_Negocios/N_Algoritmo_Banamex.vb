
Imports Capa_Identidad

Public Class N_Algoritmo_Banamex
    Inherits N_Algoritmo

    Public Sub New()
    End Sub

    Public Sub New(cadena As String, formato As I_formato)
        MyBase.New(cadena, formato)
    End Sub

    Protected Overrides Sub GetDatos()
        Dim cadena As String = _cadena
        Dim aux As String
        Dim indice As Integer

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

    Private Function ProcesarLinea(ByVal cadena As String) As List(Of String)




    End Function


End Class
