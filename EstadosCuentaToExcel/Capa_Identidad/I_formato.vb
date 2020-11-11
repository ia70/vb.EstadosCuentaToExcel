Imports Capa_Identidad

Public Class I_Formato

    Private _id_formato As String
    Private _banco As String
    Private _algoritmo As String
    Private _cadena As String
    Private _idcamposdescripcion As String
    Private _campos As List(Of I_Campos)
    Private _tipo_operacion As List(Of I_Tipo_operacion)
    Private _prefijos As I_Prefijos

#Region "CONSTRUCTOR"

    Public Sub New()
    End Sub

    Public Sub New(id_formato As String, banco As String, algoritmo As String, cadena As String, idcamposdescripcion As String, campos As List(Of I_Campos), tipo_operacion As List(Of I_Tipo_operacion), prefijos As I_Prefijos)
        _id_formato = id_formato
        _banco = banco
        _algoritmo = algoritmo
        _cadena = cadena
        _idcamposdescripcion = idcamposdescripcion
        _campos = campos
        _tipo_operacion = tipo_operacion
        _prefijos = prefijos
    End Sub

#End Region
#Region "PROPIEDADES"

    Public Property Id_formato As String
        Get
            Return _id_formato
        End Get
        Set(value As String)
            _id_formato = value
        End Set
    End Property

    Public Property Banco As String
        Get
            Return _banco
        End Get
        Set(value As String)
            _banco = value
        End Set
    End Property

    Public Property Algoritmo As String
        Get
            Return _algoritmo
        End Get
        Set(value As String)
            _algoritmo = value
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

    Public Property Idcamposdescripcion As String
        Get
            Return _idcamposdescripcion
        End Get
        Set(value As String)
            _idcamposdescripcion = value
        End Set
    End Property

    Public Property Campos As List(Of I_Campos)
        Get
            Return _campos
        End Get
        Set(value As List(Of I_Campos))
            _campos = value
        End Set
    End Property

    Public Property Tipo_operacion As List(Of I_Tipo_operacion)
        Get
            Return _tipo_operacion
        End Get
        Set(value As List(Of I_Tipo_operacion))
            _tipo_operacion = value
        End Set
    End Property

    Public Property Prefijos As I_Prefijos
        Get
            Return _prefijos
        End Get
        Set(value As I_Prefijos)
            _prefijos = value
        End Set
    End Property

#End Region

End Class
