
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

        cadena = LimpiarTexto(cadena)


    End Sub

    Protected Overrides Function GetLinea(cadena As String) As List(Of String)

    End Function

    Protected Overrides Function LimpiarTexto(ByVal cadena As String) As String
        Dim ig_ini, ig_fin, ig_total As String
        Dim aux As String
        Dim indice As Integer

        'CARGAR VARIABLES
        ig_ini = _formato.Formato_global.Ignora_parcial_ini
        ig_fin = _formato.Formato_global.Ignora_parcial_fin
        'ig_total = _formato.Formato_global.Ignora_total

        Try
            'RECUPERAR DATOS GLOBALES
            With _archivo
                .Rfc = GetRFC()
                .Fecha = GetFecha()
                .Saldo_inicial = GetSaldoInicial()
                .No_cuenta = GetNoCuenta()
            End With
        Catch ex As Exception
        End Try

        Try
            'INNORA TOTAL
            indice = cadena.IndexOf(ig_total)
            cadena = cadena.Substring(0, indice)

        Catch ex As Exception

        End Try

        Return cadena
    End Function

End Class
