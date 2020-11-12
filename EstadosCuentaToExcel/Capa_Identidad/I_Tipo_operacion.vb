Imports Capa_Identidad

Public Class I_Tipo_operacion
    Private _id As Integer
    Private _id_formato As String
    Private _tipo As Boolean

    Private _condiciones As List(Of I_Condicion)

#Region "CONSTRUCTOR"
    Public Sub New()
    End Sub

    Public Sub New(id As Integer, id_formato As String, tipo As Boolean, condiciones As List(Of I_Condicion))
        Me.Id = id
        Me.Id_formato = id_formato
        Me.Tipo = tipo
        Me.Condiciones = condiciones
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

    Public Property Tipo As Boolean
        Get
            Return _tipo
        End Get
        Set(value As Boolean)
            _tipo = value
        End Set
    End Property

    Public Property Condiciones As List(Of I_Condicion)
        Get
            Return _condiciones
        End Get
        Set(value As List(Of I_Condicion))
            _condiciones = value
        End Set
    End Property
#End Region
#Region "FUNCIONES"
    Public Sub SetIdCampos(ByVal id_ As Integer)

        Try
            For i As Integer = 0 To Condiciones.Count - 1
                Condiciones(i).Id_tipo_operacion = Id
                Condiciones(i).Id_formato = Id_formato
            Next
        Catch ex As Exception
            X(ex)
        End Try

    End Sub
#End Region

End Class
