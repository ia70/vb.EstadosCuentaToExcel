Public Class I_Condicion
    Private _id As Integer
    Private _id_formato As String
    Private _id_tipo_operacion As Integer
    Private _tipo As Integer
    Private _cadena As String

#Region "CONSTRUCTOR"
    Public Sub New()
    End Sub

    ''' <summary>
    '''  Constructor
    ''' </summary>
    ''' <param name="id_formato"></param>
    ''' <param name="id_tipo_operacion">ID tipo_operacion</param>
    ''' <param name="tipo">1 - CONTENER | 0 NO_CONTENER | 2 Opcional</param>
    ''' <param name="cadena">Cadena a buscar</param>
    Public Sub New(id_formato As String, id_tipo_operacion As Integer, tipo As Integer, cadena As String)
        _id = Id
        _id_formato = id_formato
        _id_tipo_operacion = id_tipo_operacion
        _tipo = tipo
        _cadena = cadena
    End Sub
#End Region
#Region "PROPIEDADES"
    ''' <summary>
    ''' Identificador único auto-incremento
    ''' </summary>
    ''' <returns></returns>
    Public Property Id As Integer
        Get
            Return _id
        End Get
        Set(value As Integer)
            _id = value
        End Set
    End Property

    ''' <summary>
    ''' Identificador ID de la tabla tipo_operacion
    ''' </summary>
    ''' <returns></returns>
    Public Property Id_tipo_operacion As Integer
        Get
            Return _id_tipo_operacion
        End Get
        Set(value As Integer)
            _id_tipo_operacion = value
        End Set
    End Property

    ''' <summary>
    ''' True - CONTENER | False NO_CONTENER Cadena
    ''' </summary>
    ''' <returns></returns>
    Public Property Tipo As Integer
        Get
            Return _tipo
        End Get
        Set(value As Integer)
            _tipo = value
        End Set
    End Property

    ''' <summary>
    ''' Cadena a buscar
    ''' </summary>
    ''' <returns></returns>
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
#End Region

End Class
