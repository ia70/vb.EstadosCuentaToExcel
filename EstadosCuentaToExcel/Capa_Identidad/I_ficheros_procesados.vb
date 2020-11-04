Public Class I_Ficheros_procesados
    Private _id As Integer
    Private _nombre As String

    Public Sub New()
    End Sub

    Public Sub New(ByVal nombre As String)
        _nombre = nombre
    End Sub

    Public Sub New(ByVal id As Integer, ByVal nombre As String)
        _id = id
        _nombre = nombre
    End Sub

    Public Property Id As Integer
        Get
            Return _id
        End Get
        Set(value As Integer)
            _id = value
        End Set
    End Property

    Public Property Nombre As String
        Get
            Return _nombre
        End Get
        Set(value As String)
            _nombre = value
        End Set
    End Property
End Class
