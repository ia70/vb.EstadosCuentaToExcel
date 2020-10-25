Imports Capa_Identidad

Public Class I_formato

    Private _id_formato As String
    Private _banco As String
    Private _algoritmo As String
    Private _cadena As String
    Private _formato_campos As List(Of I_formato_campos)
    Private _formato_campo_ingreso As List(Of I_formato_campo_ingreso)
    Private _formato_campo_egreso As List(Of I_formato_campo_egreso)
    Private _formato_global As I_formato_global

#Region "CONSTRUCTOR"

    Public Sub New()
    End Sub

    Public Sub New(id_formato As String, banco As String, algoritmo As String, cadena As String, formato_campos As List(Of I_formato_campos), formato_campo_ingreso As List(Of I_formato_campo_ingreso), formato_campo_egreso As List(Of I_formato_campo_egreso), formato_global As I_formato_global)
        _id_formato = id_formato
        _banco = banco
        _algoritmo = algoritmo
        _cadena = cadena
        _formato_campos = formato_campos
        _formato_campo_ingreso = formato_campo_ingreso
        _formato_campo_egreso = formato_campo_egreso
        _formato_global = formato_global
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

    Public Property Formato_campos As List(Of I_formato_campos)
        Get
            Return _formato_campos
        End Get
        Set(value As List(Of I_formato_campos))
            _formato_campos = value
        End Set
    End Property

    Public Property Formato_campo_ingreso As List(Of I_formato_campo_ingreso)
        Get
            Return _formato_campo_ingreso
        End Get
        Set(value As List(Of I_formato_campo_ingreso))
            _formato_campo_ingreso = value
        End Set
    End Property

    Public Property Formato_campo_egreso As List(Of I_formato_campo_egreso)
        Get
            Return _formato_campo_egreso
        End Get
        Set(value As List(Of I_formato_campo_egreso))
            _formato_campo_egreso = value
        End Set
    End Property

    Public Property Formato_global As I_formato_global
        Get
            Return _formato_global
        End Get
        Set(value As I_formato_global)
            _formato_global = value
        End Set
    End Property

#End Region

End Class
