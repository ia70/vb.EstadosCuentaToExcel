Public Class I_Prefijo_simple

    Private _id As Integer
    Private _id_formato As String
    Private _tipo As String
    Private _inicio As String
    Private _fin As String
    Private _separador As String
    Private _size As Integer

    Public Sub New()
        Id = 0
        Id_formato = ""
        Tipo = ""
        Inicio = ""
        Fin = ""
        Separador = ""
        Size = 0
    End Sub

    Public Sub New(id As Integer, id_formato As String, tipo As String, inicio As String, fin As String, separador As String, size As Integer)
        _id = id
        _id_formato = id_formato
        _tipo = tipo
        _inicio = inicio
        _fin = fin
        _separador = separador
        _size = size
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

    Public Property Separador As String
        Get
            Return _separador
        End Get
        Set(value As String)
            _separador = value
        End Set
    End Property

    Public Property Size As Integer
        Get
            Return _size
        End Get
        Set(value As Integer)
            _size = value
        End Set
    End Property
End Class
