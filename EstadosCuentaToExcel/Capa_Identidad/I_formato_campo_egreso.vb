Public Class I_Formato_campo_egreso
    Private _id As Integer
    Private _cadena As String
    Private _id_formato As String

    Public Sub New()
    End Sub

    Public Sub New(id As Integer, cadena As String)
        Me.Id = id
        Me.Cadena = cadena
    End Sub

    Public Sub New(id As Integer, cadena As String, id_formato As String)
        Me.New(id, cadena)
        _id_formato = id_formato
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
