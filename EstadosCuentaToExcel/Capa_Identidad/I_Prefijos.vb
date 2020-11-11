Public Class I_Prefijos

    Private _id As Integer
    Private _id_formato As String
    Private _rfc_ini As String
    Private _rfc_fin As String
    Private _fecha_general_ini As String
    Private _fecha_general_fin As String
    Private _fecha_general_separador As String
    Private _no_cuenta_ini As String
    Private _no_cuenta_fin As String
    Private _saldo_anterior_ini As String
    Private _saldo_anterior_fin As String
    Private _detalles_saldo_ini As String
    Private _detalles_saldo_fin As String

    Private _fecha_operacion_activo As Boolean
    Private _fecha_operacion_ini As String
    Private _fecha_operacion_fin As String
    Private _fecha_operacion_dia_length As Integer
    Private _fecha_operacion_mes_length As Integer
    Private _fecha_operacion_anio_length As Integer
    Private _fecha_operacion_separador_dia_mes As String

    Private _fecha_liquidacion_activo As Boolean
    Private _fecha_liquidacion_ini As String
    Private _fecha_liquidacion_fin As String
    Private _fecha_liquidacion_dia_length As Integer
    Private _fecha_liquidacion_mes_length As Integer
    Private _fecha_liquidacion_anio_length As Integer
    Private _fecha_liquidacion_separador_dia_mes As String

    Private _ignora_parcial_ini As String
    Private _ignora_parcial_fin As String
    Private _ignora_parcial_adicional_1_ini As String
    Private _ignora_parcial_adicional_1_fin As String
    Private _ignora_parcial_adicional_2_ini As String
    Private _ignora_parcial_adicional_2_fin As String

    Private _folio_activo As Boolean
    Private _folio_ini As String
    Private _folio_fin As String
    Private _folio_length As Integer
    Private _folio_tipo As String

    Private _referencia_activo As Boolean
    Private _referencia_ini As String
    Private _referencia_fin As String


#Region "CONSTRUCTORES"

    Public Sub New()
    End Sub

    Public Sub New(id As Integer, id_formato As String, rfc_ini As String, rfc_fin As String, fecha_general_ini As String, fecha_general_fin As String, fecha_general_separador As String, no_cuenta_ini As String, no_cuenta_fin As String, saldo_anterior_ini As String, saldo_anterior_fin As String, detalles_saldo_ini As String, detalles_saldo_fin As String, fecha_operacion_activo As Boolean, fecha_operacion_ini As String, fecha_operacion_fin As String, fecha_operacion_dia_length As Integer, fecha_operacion_mes_length As Integer, fecha_operacion_anio_length As Integer, fecha_operacion_separador_dia_mes As String, fecha_liquidacion_activo As Boolean, fecha_liquidacion_ini As String, fecha_liquidacion_fin As String, fecha_liquidacion_dia_length As Integer, fecha_liquidacion_mes_length As Integer, fecha_liquidacion_anio_length As Integer, fecha_liquidacion_separador_dia_mes As String, ignora_parcial_ini As String, ignora_parcial_fin As String, ignora_parcial_adicional_1_ini As String, ignora_parcial_adicional_1_fin As String, ignora_parcial_adicional_2_ini As String, ignora_parcial_adicional_2_fin As String, folio_activo As Boolean, folio_ini As String, folio_fin As String, folio_length As Integer, folio_tipo As String, referencia_activo As Boolean, referencia_ini As String, referencia_fin As String)
        _id = id
        _id_formato = id_formato
        _rfc_ini = rfc_ini
        _rfc_fin = rfc_fin
        _fecha_general_ini = fecha_general_ini
        _fecha_general_fin = fecha_general_fin
        _fecha_general_separador = fecha_general_separador
        _no_cuenta_ini = no_cuenta_ini
        _no_cuenta_fin = no_cuenta_fin
        _saldo_anterior_ini = saldo_anterior_ini
        _saldo_anterior_fin = saldo_anterior_fin
        _detalles_saldo_ini = detalles_saldo_ini
        _detalles_saldo_fin = detalles_saldo_fin
        _fecha_operacion_activo = fecha_operacion_activo
        _fecha_operacion_ini = fecha_operacion_ini
        _fecha_operacion_fin = fecha_operacion_fin
        _fecha_operacion_dia_length = fecha_operacion_dia_length
        _fecha_operacion_mes_length = fecha_operacion_mes_length
        _fecha_operacion_anio_length = fecha_operacion_anio_length
        _fecha_operacion_separador_dia_mes = fecha_operacion_separador_dia_mes
        _fecha_liquidacion_activo = fecha_liquidacion_activo
        _fecha_liquidacion_ini = fecha_liquidacion_ini
        _fecha_liquidacion_fin = fecha_liquidacion_fin
        _fecha_liquidacion_dia_length = fecha_liquidacion_dia_length
        _fecha_liquidacion_mes_length = fecha_liquidacion_mes_length
        _fecha_liquidacion_anio_length = fecha_liquidacion_anio_length
        _fecha_liquidacion_separador_dia_mes = fecha_liquidacion_separador_dia_mes
        _ignora_parcial_ini = ignora_parcial_ini
        _ignora_parcial_fin = ignora_parcial_fin
        _ignora_parcial_adicional_1_ini = ignora_parcial_adicional_1_ini
        _ignora_parcial_adicional_1_fin = ignora_parcial_adicional_1_fin
        _ignora_parcial_adicional_2_ini = ignora_parcial_adicional_2_ini
        _ignora_parcial_adicional_2_fin = ignora_parcial_adicional_2_fin
        _folio_activo = folio_activo
        _folio_ini = folio_ini
        _folio_fin = folio_fin
        _folio_length = folio_length
        _folio_tipo = folio_tipo
        _referencia_activo = referencia_activo
        _referencia_ini = referencia_ini
        _referencia_fin = referencia_fin
    End Sub

#End Region
#Region "PROPIEDADES"

    Public Property Rfc_ini As String
        Get
            Return _rfc_ini
        End Get
        Set(value As String)
            _rfc_ini = value
        End Set
    End Property

    Public Property Rfc_fin As String
        Get
            Return _rfc_fin
        End Get
        Set(value As String)
            _rfc_fin = value
        End Set
    End Property

    Public Property Fecha_general_ini As String
        Get
            Return _fecha_general_ini
        End Get
        Set(value As String)
            _fecha_general_ini = value
        End Set
    End Property

    Public Property Fecha_general_fin As String
        Get
            Return _fecha_general_fin
        End Get
        Set(value As String)
            _fecha_general_fin = value
        End Set
    End Property

    Public Property Fecha_general_separador As String
        Get
            Return _fecha_general_separador
        End Get
        Set(value As String)
            _fecha_general_separador = value
        End Set
    End Property

    Public Property No_cuenta_ini As String
        Get
            Return _no_cuenta_ini
        End Get
        Set(value As String)
            _no_cuenta_ini = value
        End Set
    End Property

    Public Property No_cuenta_fin As String
        Get
            Return _no_cuenta_fin
        End Get
        Set(value As String)
            _no_cuenta_fin = value
        End Set
    End Property

    Public Property Saldo_anterior_ini As String
        Get
            Return _saldo_anterior_ini
        End Get
        Set(value As String)
            _saldo_anterior_ini = value
        End Set
    End Property

    Public Property Saldo_anterior_fin As String
        Get
            Return _saldo_anterior_fin
        End Get
        Set(value As String)
            _saldo_anterior_fin = value
        End Set
    End Property

    Public Property Detalles_saldo_ini As String
        Get
            Return _detalles_saldo_ini
        End Get
        Set(value As String)
            _detalles_saldo_ini = value
        End Set
    End Property

    Public Property Detalles_saldo_fin As String
        Get
            Return _detalles_saldo_fin
        End Get
        Set(value As String)
            _detalles_saldo_fin = value
        End Set
    End Property

    Public Property Fecha_operacion_activo As Boolean
        Get
            Return _fecha_operacion_activo
        End Get
        Set(value As Boolean)
            _fecha_operacion_activo = value
        End Set
    End Property

    Public Property Fecha_operacion_ini As String
        Get
            Return _fecha_operacion_ini
        End Get
        Set(value As String)
            _fecha_operacion_ini = value
        End Set
    End Property

    Public Property Fecha_operacion_fin As String
        Get
            Return _fecha_operacion_fin
        End Get
        Set(value As String)
            _fecha_operacion_fin = value
        End Set
    End Property

    Public Property Fecha_operacion_dia_length As Integer
        Get
            Return _fecha_operacion_dia_length
        End Get
        Set(value As Integer)
            _fecha_operacion_dia_length = value
        End Set
    End Property

    Public Property Fecha_operacion_mes_length As Integer
        Get
            Return _fecha_operacion_mes_length
        End Get
        Set(value As Integer)
            _fecha_operacion_mes_length = value
        End Set
    End Property

    Public Property Fecha_operacion_anio_length As Integer
        Get
            Return _fecha_operacion_anio_length
        End Get
        Set(value As Integer)
            _fecha_operacion_anio_length = value
        End Set
    End Property

    Public Property Fecha_operacion_separador_dia_mes As String
        Get
            Return _fecha_operacion_separador_dia_mes
        End Get
        Set(value As String)
            _fecha_operacion_separador_dia_mes = value
        End Set
    End Property

    Public Property Fecha_liquidacion_activo As Boolean
        Get
            Return _fecha_liquidacion_activo
        End Get
        Set(value As Boolean)
            _fecha_liquidacion_activo = value
        End Set
    End Property

    Public Property Fecha_liquidacion_ini As String
        Get
            Return _fecha_liquidacion_ini
        End Get
        Set(value As String)
            _fecha_liquidacion_ini = value
        End Set
    End Property

    Public Property Fecha_liquidacion_fin As String
        Get
            Return _fecha_liquidacion_fin
        End Get
        Set(value As String)
            _fecha_liquidacion_fin = value
        End Set
    End Property

    Public Property Fecha_liquidacion_dia_length As Integer
        Get
            Return _fecha_liquidacion_dia_length
        End Get
        Set(value As Integer)
            _fecha_liquidacion_dia_length = value
        End Set
    End Property

    Public Property Fecha_liquidacion_mes_length As Integer
        Get
            Return _fecha_liquidacion_mes_length
        End Get
        Set(value As Integer)
            _fecha_liquidacion_mes_length = value
        End Set
    End Property

    Public Property Fecha_liquidacion_anio_length As Integer
        Get
            Return _fecha_liquidacion_anio_length
        End Get
        Set(value As Integer)
            _fecha_liquidacion_anio_length = value
        End Set
    End Property

    Public Property Fecha_liquidacion_separador_dia_mes As String
        Get
            Return _fecha_liquidacion_separador_dia_mes
        End Get
        Set(value As String)
            _fecha_liquidacion_separador_dia_mes = value
        End Set
    End Property

    Public Property Ignora_parcial_ini As String
        Get
            Return _ignora_parcial_ini
        End Get
        Set(value As String)
            _ignora_parcial_ini = value
        End Set
    End Property

    Public Property Ignora_parcial_fin As String
        Get
            Return _ignora_parcial_fin
        End Get
        Set(value As String)
            _ignora_parcial_fin = value
        End Set
    End Property

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

    Public Property Ignora_parcial_adicional_1_ini As String
        Get
            Return _ignora_parcial_adicional_1_ini
        End Get
        Set(value As String)
            _ignora_parcial_adicional_1_ini = value
        End Set
    End Property

    Public Property Ignora_parcial_adicional_1_fin As String
        Get
            Return _ignora_parcial_adicional_1_fin
        End Get
        Set(value As String)
            _ignora_parcial_adicional_1_fin = value
        End Set
    End Property

    Public Property Ignora_parcial_adicional_2_ini As String
        Get
            Return _ignora_parcial_adicional_2_ini
        End Get
        Set(value As String)
            _ignora_parcial_adicional_2_ini = value
        End Set
    End Property

    Public Property Ignora_parcial_adicional_2_fin As String
        Get
            Return _ignora_parcial_adicional_2_fin
        End Get
        Set(value As String)
            _ignora_parcial_adicional_2_fin = value
        End Set
    End Property

    Public Property Folio_activo As Boolean
        Get
            Return _folio_activo
        End Get
        Set(value As Boolean)
            _folio_activo = value
        End Set
    End Property

    Public Property Folio_ini As String
        Get
            Return _folio_ini
        End Get
        Set(value As String)
            _folio_ini = value
        End Set
    End Property

    Public Property Folio_fin As String
        Get
            Return _folio_fin
        End Get
        Set(value As String)
            _folio_fin = value
        End Set
    End Property

    Public Property Folio_length As Integer
        Get
            Return _folio_length
        End Get
        Set(value As Integer)
            _folio_length = value
        End Set
    End Property

    Public Property Folio_tipo As String
        Get
            Return _folio_tipo
        End Get
        Set(value As String)
            _folio_tipo = value
        End Set
    End Property

    Public Property Referencia_activo As Boolean
        Get
            Return _referencia_activo
        End Get
        Set(value As Boolean)
            _referencia_activo = value
        End Set
    End Property

    Public Property Referencia_ini As String
        Get
            Return _referencia_ini
        End Get
        Set(value As String)
            _referencia_ini = value
        End Set
    End Property

    Public Property Referencia_fin As String
        Get
            Return _referencia_fin
        End Get
        Set(value As String)
            _referencia_fin = value
        End Set
    End Property

#End Region

End Class
