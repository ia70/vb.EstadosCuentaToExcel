Public Class I_Configuracion
    Private _id As Integer = 1
    Private _folder_in As String
    Private _folder_out As String
    Private _copia_origen As Boolean

#Region "Constructor"
    Public Sub New()

    End Sub

    Public Sub New(id As Integer, folder_in As String, folder_out As String, copia_origen As Boolean)
        _id = id
        _folder_in = folder_in
        _folder_out = folder_out
        _copia_origen = copia_origen
    End Sub

#End Region
#Region "Propiedades"
    Public Property Id As Integer
        Get
            Return _id
        End Get
        Set(value As Integer)
            _id = value
        End Set
    End Property

    Public Property Folder_in As String
        Get
            Return _folder_in
        End Get
        Set(value As String)
            _folder_in = value
        End Set
    End Property

    Public Property Folder_out As String
        Get
            Return _folder_out
        End Get
        Set(value As String)
            _folder_out = value
        End Set
    End Property

    Public Property Copia_origen As Boolean
        Get
            Return _copia_origen
        End Get
        Set(value As Boolean)
            _copia_origen = value
        End Set
    End Property
#End Region

End Class
