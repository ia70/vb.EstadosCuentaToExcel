Public Class I_formato_campos
    Private _id As Integer
    Private _id_formato As String
    Private _indice As Integer
    Private _nombre As String

#Region "CONSTRUCTOR"
    Public Sub New()
    End Sub

    Public Sub New(indice As Integer, nombre As String)
        Me.Indice = indice
        Me.Nombre = nombre
    End Sub

    Public Sub New(id As Integer, id_formato As String, indice As Integer, nombre As String)
        _id = id
        _id_formato = id_formato
        _indice = indice
        _nombre = nombre
    End Sub
#End Region
#Region "PROPIEDADES"
    Public Property Indice As Integer
        Get
            Return _indice
        End Get
        Set(value As Integer)
            _indice = value
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
#End Region
End Class
