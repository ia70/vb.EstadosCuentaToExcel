Public Class I_configuracion
    Private _id As Integer
    Private _db_ip As String
    Private _db_user As String
    Private _db_password As String
    Private _db_name As String
    Private _folder_in As String
    Private _folder_out As String

#Region "Constructor"
    Public Sub New()

    End Sub

    Public Sub New(db_ip As String, db_user As String, db_password As String, db_name As String, folder_in As String, folder_out As String)
        _id = Id
        _db_ip = db_ip
        _db_user = db_user
        _db_password = db_password
        _db_name = db_name
        _folder_in = folder_in
        _folder_out = folder_out
    End Sub

    Public Sub New(id As Integer, db_ip As String, db_user As String, db_password As String, db_name As String, folder_in As String, folder_out As String)
        _id = id
        _db_ip = db_ip
        _db_user = db_user
        _db_password = db_password
        _db_name = db_name
        _folder_in = folder_in
        _folder_out = folder_out
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
    Public Property Db_ip As String
        Get
            Return _db_ip
        End Get
        Set(value As String)
            _db_ip = value
        End Set
    End Property

    Public Property Db_user As String
        Get
            Return _db_user
        End Get
        Set(value As String)
            _db_user = value
        End Set
    End Property

    Public Property Db_password As String
        Get
            Return _db_password
        End Get
        Set(value As String)
            _db_password = value
        End Set
    End Property

    Public Property Db_name As String
        Get
            Return _db_name
        End Get
        Set(value As String)
            _db_name = value
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
#End Region

End Class
