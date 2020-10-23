Module Funciones
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
End Module
