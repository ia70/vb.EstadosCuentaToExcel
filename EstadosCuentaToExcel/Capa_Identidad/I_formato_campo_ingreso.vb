Public Class I_formato_campo_ingreso
    Private _id As Integer
    Private _id_formato As String
    Private _cadena As String
    Public Sub New()
    End Sub

    Public Sub New(id As Integer, cadena As String)
        Me.Id = id
        Me.Cadena = cadena
    End Sub

    Public Sub New(id As Integer, id_formato As String, cadena As String)
        Me.New(id, id_formato)
        _cadena = cadena
    End Sub

    Public Property Id As Integer
        Get
            Return _id
        End Get
        Set(value As Integer)
            _id = value
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

    Public Property Id_formato As String
        Get
            Return _id_formato
        End Get
        Set(value As String)
            _id_formato = value
        End Set
    End Property
End Class
