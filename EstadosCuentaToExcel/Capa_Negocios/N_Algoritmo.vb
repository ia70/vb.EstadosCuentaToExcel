Imports Capa_Identidad
Public MustInherit Class N_Algoritmo
    Protected _cadena As String
    Protected _archivo As I_Archivo
    Protected _formato As I_Formato

#Region "CONSTRUCTOR/PROPIEDAD"
    Public Sub New()
    End Sub

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="cadena">PDF en texto plano</param>
    ''' <param name="formato">Identidad del formato</param>
    Public Sub New(ByVal cadena As String, ByVal formato As I_Formato)
        _cadena = cadena
        _archivo = New I_Archivo(formato.Formato_campos)
        _formato = formato

        ProcesarInfo()
    End Sub

    ''' <summary>
    ''' Identidad del archivo
    ''' </summary>
    ''' <returns></returns>
    Public Property Archivo As I_Archivo
        Get
            Return _archivo
        End Get
        Set(value As I_Archivo)
            _archivo = value
        End Set
    End Property

#End Region
#Region "FUNCIONES"
#Region "VARIAS"

    Protected Sub ProcesarInfo()
        Try
            _cadena = LimpiarTexto(_cadena)
        Catch ex As Exception

        End Try
        GetDatos()
    End Sub

    Protected MustOverride Function GetLinea(ByVal cadena As String) As String
    Protected MustOverride Sub GetDatos()

    ''' <summary>
    ''' Se encarga de quitar todo lo que no es el cuerpo del estado de cuenta
    ''' </summary>
    ''' <param name="cadena">cadena a limpiar</param>
    ''' <returns></returns>
    Protected Function LimpiarTexto(ByVal cadena As String) As String
        Dim ig_ini, ig_fin, ig_total_ini, ig_total_fin As String
        Dim aux, aux2 As String
        Dim indice As Integer

        'CARGAR VARIABLES
        ig_ini = _formato.Formato_global.Ignora_parcial_ini
        ig_fin = _formato.Formato_global.Ignora_parcial_fin
        ig_total_ini = _formato.Formato_global.Detalles_saldo_ini
        ig_total_fin = _formato.Formato_global.Detalles_saldo_fin

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
            'QUITAR ENCABEZADO
            indice = cadena.IndexOf(ig_total_ini)
            cadena = cadena.Substring(indice + ig_total_ini.Length)

            'QUITAR PIE
            indice = cadena.IndexOf(ig_total_fin)
            cadena = cadena.Substring(0, indice)

            'QUITAR PARCIAL
            While cadena.IndexOf(ig_ini) >= 0
                indice = cadena.IndexOf(ig_ini)
                'indice2 = cadena.IndexOf(ig_fin)

                aux = cadena.Substring(0, indice)
                indice = cadena.IndexOf(ig_fin)

                aux2 = cadena.Substring(indice + ig_fin.Length)
                cadena = aux + aux2
            End While

        Catch ex As Exception
        End Try

        Return cadena
    End Function

    ''' <summary>
    ''' Se encarga de extraer la información de un campo
    ''' </summary>
    ''' <param name="cad_ini">Prefijo de inicio</param>
    ''' <param name="cad_fin">Prefijo de final</param>
    ''' <returns></returns>
    Protected Function GetCampo(ByVal cad_ini As String, cad_fin As String) As String
        Return GetCampo(_cadena, cad_ini, cad_fin)
    End Function

    ''' <summary>
    ''' Se encarga de extraer la información de un campo
    ''' </summary>
    ''' <param name="vcadena">Cadena en la cual buscar</param>
    ''' <param name="cad_ini">Prefijo de inicio</param>
    ''' <param name="cad_fin">Prefijo de final</param>
    ''' <returns></returns>
    Protected Function GetCampo(ByVal vcadena As String, ByVal cad_ini As String, cad_fin As String) As String
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
            Return ""
        End Try

    End Function

#End Region
#Region "OBTENER CAMPOS"

    Protected Function GetRFC() As String
        Return GetCampo(_formato.Formato_global.Rfc_ini, _formato.Formato_global.Rfc_fin)
    End Function

    Protected Function GetSaldoInicial() As Decimal
        Dim saldo As String = "0"

        Try
            saldo = GetCampo(_formato.Formato_global.Saldo_anterior_ini, _formato.Formato_global.Saldo_anterior_fin)
        Catch ex As Exception

        End Try

        Return CDec(saldo)
    End Function

    ''' <summary>
    ''' Devuelve la fecha del pdf
    ''' </summary>
    ''' <returns></returns>
    Protected Function GetFecha() As Date
        Dim vfecha, separador, aux, dia, anio, mes As String
        Dim fecha As Date


        separador = _formato.Formato_global.Fecha_general_separador

        Try
            vfecha = GetCampo(_formato.Formato_global.Fecha_general_ini, _formato.Formato_global.Fecha_general_fin)

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
            Return Nothing
        End Try

        Return fecha
    End Function

    ''' <summary>
    ''' Devuelve el número de cuenta del PDF
    ''' </summary>
    ''' <returns></returns>
    Protected Function GetNoCuenta() As String
        Dim noCuenta As String = ""

        Try
            noCuenta = GetCampo(_formato.Formato_global.No_cuenta_ini, _formato.Formato_global.No_cuenta_fin)
        Catch ex As Exception

        End Try

        Return noCuenta
    End Function

#End Region
#Region "OPERACIONES CON FECHAS"

    ''' <summary>
    ''' Devuelme el numero correspondiente al mes dado
    ''' </summary>
    ''' <param name="cadena">Nombre del mes</param>
    ''' <returns></returns>
    Protected Function GetMesNum(ByVal cadena As String) As String
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
            mes = ""
        End Try

        Return mes
    End Function

    ''' <summary>
    ''' Devuelve el nombre del mes dado un número
    ''' </summary>
    ''' <param name="num">Número de mes</param>
    ''' <returns></returns>
    Protected Function GetMesNombre(ByVal num As String) As String
        Return GetMesNombre(Convert.ToInt32(num))
    End Function

    ''' <summary>
    ''' Devuelve el nombre del mes dado un número
    ''' </summary>
    ''' <param name="num">Número del mes</param>
    ''' <returns></returns>
    Protected Function GetMesNombre(ByVal num As Integer) As String
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
    ''' Obtener la abreviación del mes
    ''' </summary>
    ''' <param name="num">Número del mes</param>
    ''' <returns></returns>
    Protected Function GetMesAbreviacion(ByVal num As Integer) As String
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
            Case Else
                mes = "ENE"
        End Select

        Return mes
    End Function

#End Region
#End Region
End Class
