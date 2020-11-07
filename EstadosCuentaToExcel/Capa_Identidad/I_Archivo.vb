Public Class I_Archivo

    Private _rfc As String
    Private _saldo_inicial As Decimal
    Private _no_cuenta As String
    Private _fecha As Date
    Private _mes As String
    Private _anio As String
    Private _dia As String
    Private _tabla As DataTable
    Private _campos As List(Of I_formato_campos)

#Region "CONSTRUCTORES"

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="campos">Lista de campos</param>
    Public Sub New(ByVal campos As List(Of I_formato_campos))
        Tabla = New DataTable
        _campos = campos

        Try
            For Each campo As I_Formato_campos In campos
                'Tabla.Columns.Add(campo.Nombre, Type.GetType("System." & campo.Tipo))
                Tabla.Columns.Add(campo.Nombre, Type.GetType("System.String"))
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


    End Sub

#End Region
#Region "FUNCIONES"
    Public Function agregarFila(ByVal fila As List(Of String)) As Boolean
        Dim indice As Integer = 0
        Dim dr As DataRow
        dr = Tabla.NewRow

        Try
            For Each campo As String In fila
                dr.Item(indice) = campo
                indice += 1
            Next
            Tabla.Rows.Add(dr)
        Catch ex As Exception
            Return False
        End Try

        Return True
    End Function

#End Region
#Region "PROPIEDADES"

    Public Property Rfc As String
        Get
            Return _rfc
        End Get
        Set(value As String)
            _rfc = value
        End Set
    End Property

    Public Property Saldo_inicial As Decimal
        Get
            Return _saldo_inicial
        End Get
        Set(value As Decimal)
            _saldo_inicial = value
        End Set
    End Property

    Public Property Tabla As DataTable
        Get
            Return _tabla
        End Get
        Set(value As DataTable)
            _tabla = value
        End Set
    End Property

    Public Property No_cuenta As String
        Get
            Return _no_cuenta
        End Get
        Set(value As String)
            _no_cuenta = value
        End Set
    End Property

    Public Property Fecha As Date
        Get
            Return _fecha
        End Get
        Set(value As Date)
            _fecha = value
            Try
                Dia = Format(_fecha.Day, "00").ToString
                Mes = Format(_fecha.Month, "00").ToString
                Anio = Format(_fecha.Year, "0000").ToString
            Catch ex As Exception

            End Try
        End Set
    End Property

    Public Property Mes As String
        Get
            Return _mes
        End Get
        Set(value As String)
            _mes = value
        End Set
    End Property

    Public Property Anio As String
        Get
            Return _anio
        End Get
        Set(value As String)
            _anio = value
        End Set
    End Property

    Public Property Dia As String
        Get
            Return _dia
        End Get
        Set(value As String)
            _dia = value
        End Set
    End Property

    Public Property Fecha1 As Date
        Get
            Return _fecha
        End Get
        Set(value As Date)
            _fecha = value
        End Set
    End Property

#End Region


End Class
