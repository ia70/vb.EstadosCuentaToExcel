Public Class I_formato_campos
    Private _indice As Integer
    Private _nombre As String

    Public Sub New(indice As Integer, nombre As String)
        Me.Indice = indice
        Me.Nombre = nombre
    End Sub

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
End Class
