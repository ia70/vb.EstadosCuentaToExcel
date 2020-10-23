Public Class I_ficheros_procesados
    Private _id As Integer
    Private _nombre As String

    Public Sub New()
    End Sub
    Public Sub New(id As Integer, nombre As String)
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
