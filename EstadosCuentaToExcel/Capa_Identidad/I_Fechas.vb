Public Class I_Fechas

    Private _id As Integer
    Private _id_formato As String
    Private _tipo As String
    Private _inicio As String
    Private _fin As String
    Private _dia_length As Integer
    Private _mes_length As Integer
    Private _anio_length As Integer
    Private _separador As String

    Public Sub New()
    End Sub

    Public Sub New(id As Integer, id_formato As String, tipo As String, inicio As String, fin As String, dia_length As Integer, mes_length As Integer, anio_length As Integer, separador As String)
        Me.Id = id
        Me.Id_formato = id_formato
        Me.Tipo = tipo
        Me.Inicio = inicio
        Me.Fin = fin
        Me.Dia_length = dia_length
        Me.Mes_length = mes_length
        Me.Anio_length = anio_length
        Me.Separador = separador
    End Sub

    Public Property Id As Integer
        Get
            Return _id
        End Get
        Set(value As Integer)
            _id = value
        End Set
    End Property

    Public Property Id_formato As String
        Get
            Return _id_formato
        End Get
        Set(value As String)
            _id_formato = value
        End Set
    End Property

    Public Property Tipo As String
        Get
            Return _tipo
        End Get
        Set(value As String)
            _tipo = value
        End Set
    End Property

    Public Property Inicio As String
        Get
            Return _inicio
        End Get
        Set(value As String)
            _inicio = value
        End Set
    End Property

    Public Property Fin As String
        Get
            Return _fin
        End Get
        Set(value As String)
            _fin = value
        End Set
    End Property

    Public Property Dia_length As Integer
        Get
            Return _dia_length
        End Get
        Set(value As Integer)
            _dia_length = value
        End Set
    End Property

    Public Property Mes_length As Integer
        Get
            Return _mes_length
        End Get
        Set(value As Integer)
            _mes_length = value
        End Set
    End Property

    Public Property Anio_length As Integer
        Get
            Return _anio_length
        End Get
        Set(value As Integer)
            _anio_length = value
        End Set
    End Property

    Public Property Separador As String
        Get
            Return _separador
        End Get
        Set(value As String)
            _separador = value
        End Set
    End Property
End Class
