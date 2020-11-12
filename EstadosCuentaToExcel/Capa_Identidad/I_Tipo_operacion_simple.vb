Public Class I_Tipo_operacion_simple
    Private _id As Integer
    Private _id_formato As String
    Private _tipo As Integer

    Public Sub New()
    End Sub

    Public Sub New(id As Integer, id_formato As String, tipo As Integer)
        _id = id
        _id_formato = id_formato
        _tipo = tipo
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

    Public Property Tipo As Integer
        Get
            Return _tipo
        End Get
        Set(value As Integer)
            _tipo = value
        End Set
    End Property
End Class
