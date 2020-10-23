Public Class I_Conexion
    Private _server As String
    Private _user_id As String
    Private _password As String
    Private _database As String

    Public Sub New()
    End Sub

    Public Sub New(server As String, user_id As String, password As String, database As String)
        _server = server
        _user_id = user_id
        _password = password
        _database = database
    End Sub

    Public Property Server As String
        Get
            Return _server
        End Get
        Set(value As String)
            _server = value
        End Set
    End Property

    Public Property User_id As String
        Get
            Return _user_id
        End Get
        Set(value As String)
            _user_id = value
        End Set
    End Property

    Public Property Password As String
        Get
            Return _password
        End Get
        Set(value As String)
            _password = value
        End Set
    End Property

    Public Property Database As String
        Get
            Return _database
        End Get
        Set(value As String)
            _database = value
        End Set
    End Property
End Class
