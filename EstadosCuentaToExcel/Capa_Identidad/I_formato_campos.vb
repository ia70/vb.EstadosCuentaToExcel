Public Class I_formato_campos
    Private _id As Integer
    Private _id_formato As String
    Private _nombre As String
    Private _tipo As String

#Region "CONSTRUCTOR"
    Public Sub New()
    End Sub

    Public Sub New(id As Integer, id_formato As String, nombre As String, tipo As String)
        _id = id
        _id_formato = id_formato
        _nombre = nombre
        _tipo = tipo
    End Sub


#End Region
#Region "PROPIEDADES"
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

    Public Property Nombre As String
        Get
            Return _nombre
        End Get
        Set(value As String)
            _nombre = value
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

#End Region
End Class
