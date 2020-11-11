Public Class I_Tipo_operacion
    Private _id As Integer
    Private _id_formato As String
    Private _cadena As String
    Private _cadena_adicional_1 As String
    Private _cadena_adicional_2 As String
    Private _cadena_no_contener As String
    Private _tipo As Boolean

    Public Sub New()
    End Sub

    Public Sub New(id As Integer, cadena As String)
        Me.Id = id
        Me.Cadena = cadena
    End Sub

    Public Sub New(id As Integer, id_formato As String, cadena As String, cadena_adicional_1 As String, cadena_adicional_2 As String, cadena_no_contener As String, tipo As Boolean)
        _id = id
        _id_formato = id_formato
        _cadena = cadena
        _cadena_adicional_1 = cadena_adicional_1
        _cadena_adicional_2 = cadena_adicional_2
        _cadena_no_contener = cadena_no_contener
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

    Public Property Cadena_adicional_1 As String
        Get
            Return _cadena_adicional_1
        End Get
        Set(value As String)
            _cadena_adicional_1 = value
        End Set
    End Property

    Public Property Cadena_adicional_2 As String
        Get
            Return _cadena_adicional_2
        End Get
        Set(value As String)
            _cadena_adicional_2 = value
        End Set
    End Property

    Public Property Cadena_no_contener As String
        Get
            Return _cadena_no_contener
        End Get
        Set(value As String)
            _cadena_no_contener = value
        End Set
    End Property

    Public Property Tipo As Boolean
        Get
            Return _tipo
        End Get
        Set(value As Boolean)
            _tipo = value
        End Set
    End Property
End Class
