Public Class I_Ficheros_procesados
    Private _id As Integer
    Private _nombre As String

    Public Sub New()
    End Sub

    Public Sub New(ByVal nombre_ As String)
        _nombre = GetNombre(nombre_)
    End Sub

    Private Function GetNombre(ByVal cadena As String) As String
        Dim indice As Integer
        Dim nom As String

        Try
            indice = cadena.IndexOf(":\")
            cadena = cadena.Replace("\", "^")
            If indice >= 0 Then
                indice -= 1
            Else
                indice = 0
            End If

            nom = cadena.Substring(indice)

            Return nom
        Catch ex As Exception
            X(ex)
            Return cadena
        End Try

    End Function

    Public Sub New(ByVal id As Integer, ByVal nombre_ As String)
        _id = id
        _nombre = GetNombre(nombre_)
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
            _nombre = GetNombre(value)
        End Set
    End Property
End Class
