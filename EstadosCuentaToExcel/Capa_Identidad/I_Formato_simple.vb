Public Class I_Formato_simple

    Private _id_formato As String
    Private _banco As String
    Private _algoritmo As String
    Private _cadena As String

    Public Sub New()
    End Sub

    Public Sub New(id_formato As String, banco As String, algoritmo As String, cadena As String)
        _id_formato = id_formato
        _banco = banco
        _algoritmo = algoritmo
        _cadena = cadena
    End Sub

    Public Property Id_formato As String
        Get
            Return _id_formato
        End Get
        Set(value As String)
            _id_formato = value
        End Set
    End Property

    Public Property Banco As String
        Get
            Return _banco
        End Get
        Set(value As String)
            _banco = value
        End Set
    End Property

    Public Property Algoritmo As String
        Get
            Return _algoritmo
        End Get
        Set(value As String)
            _algoritmo = value
        End Set
    End Property

    Public Property Cadena As String
        Get
            Return _cadena
        End Get
        Set(value As String)
            _cadena = value
        End Set
    End Property
End Class
