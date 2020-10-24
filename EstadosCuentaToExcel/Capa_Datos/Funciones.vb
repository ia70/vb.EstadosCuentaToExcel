Imports Newtonsoft.Json

Module Funciones
    Public Function jsonGetCampos(ByVal obj As Object) As String
        Try
            Dim cadena As String = JsonConvert.SerializeObject(obj)
            Return cadena
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Function jsonGetValores(ByVal obj As Object) As String
        Try
            Dim cadena As String = JsonConvert.SerializeObject(obj)
            Return cadena
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Function jsonGetCampos(ByVal cadena As String) As String
        Dim indice As Integer
        Dim subcadena, respuesta, campo, valor As String

        Try
            respuesta = ""
            cadena = cadena.Replace(Chr(34), "*")
            cadena = cadena.Replace("{", "")
            cadena = cadena.Replace("}", "")


            While cadena.Length > 0
                indice = cadena.IndexOf(",")
                If indice > 0 Then
                    subcadena = cadena.Substring(0, indice)
                    cadena = cadena.Substring(indice + 1)

                    indice = subcadena.IndexOf(":")
                    If indice >= 0 Then
                        valor = subcadena.Substring(indice + 1)
                        campo = subcadena.Substring(0, indice)

                        If respuesta.Length > 0 Then
                            respuesta += "," & campo
                        Else
                            respuesta += campo
                        End If

                    Else
                        cadena = ""
                    End If
                Else
                    indice = cadena.IndexOf(":")
                    If indice > 0 Then
                        cadena &= ","
                    Else
                        Exit While
                    End If
                End If
            End While
        Catch ex As Exception
            respuesta = ""
            MsgBox(ex.ToString)
        End Try

        Return respuesta.Replace("*", Chr(39))
    End Function

    Public Function jsonGetValores(ByVal cadena As String) As String
        Dim indice As Integer
        Dim subcadena, respuesta, campo, valor As String

        Try
            respuesta = ""
            cadena = cadena.Replace(Chr(34), "*")
            cadena = cadena.Replace("{", "")
            cadena = cadena.Replace("}", "")


            While cadena.Length > 0
                indice = cadena.IndexOf(",")
                If indice > 0 Then
                    subcadena = cadena.Substring(0, indice)
                    cadena = cadena.Substring(indice + 1)

                    indice = subcadena.IndexOf(":")
                    If indice >= 0 Then
                        valor = subcadena.Substring(indice + 1)
                        campo = subcadena.Substring(0, indice)

                        If respuesta.Length > 0 Then
                            respuesta += "," & valor
                        Else
                            respuesta += valor
                        End If

                    Else
                        cadena = ""
                    End If
                Else
                    indice = cadena.IndexOf(":")
                    If indice > 0 Then
                        cadena &= ","
                    Else
                        Exit While
                    End If
                End If
            End While
        Catch ex As Exception
            respuesta = ""
            MsgBox(ex.ToString)
        End Try

        Return respuesta.Replace("*", Chr(39))
    End Function

    Public Function jsonGetString(ByVal obj As Object) As String
        Dim cadena As String = JsonConvert.SerializeObject(obj)

        cadena = cadena.Replace(":", "=")
        cadena = cadena.Replace("{", "")
        cadena = cadena.Replace("}", "")

        Return cadena
    End Function

    Public Function jsonGetId(ByVal obj As Object) As String
        Dim cadena As String = JsonConvert.SerializeObject(obj)
        Dim indice As Integer
        Dim subcadena As String = ""

        Try
            indice = cadena.IndexOf(",")
            If indice >= 0 Then
                cadena = cadena.Substring(0, indice)
                indice = cadena.IndexOf(":")
                If indice >= 0 Then
                    subcadena = cadena.Substring(indice + 1)
                End If
            End If
        Catch ex As Exception
            subcadena = ""
        End Try

        Return subcadena
    End Function

End Module
