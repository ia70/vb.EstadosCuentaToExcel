Public Class I_formato_global

    Private _rfc_ini As String
    Private _rfc_fin As String
    Private _fecha_corte_ini As String
    Private _fecha_corte_fin As String
    Private _saldo_inicial_ini As String
    Private _saldo_inicial_fin As String
    Private _algoritmo_nueva_linea As String
    Private _separador_linea As String
    Private _ignora_parcial_ini As String
    Private _ignora_parcial_fin As String
    Private _ignora_total As String


#Region "CONSTRUCTORES"
    Public Sub New(rfc_ini As String, rfc_fin As String, fecha_corte_ini As String, fecha_corte_fin As String, saldo_inicial_ini As String, saldo_inicial_fin As String, algoritmo_nueva_linea As String, separador_linea As String, ignora_parcial_ini As String, ignora_parcial_fin As String, ignora_total As String)
        _rfc_ini = rfc_ini
        _rfc_fin = rfc_fin
        _fecha_corte_ini = fecha_corte_ini
        _fecha_corte_fin = fecha_corte_fin
        _saldo_inicial_ini = saldo_inicial_ini
        _saldo_inicial_fin = saldo_inicial_fin
        _algoritmo_nueva_linea = algoritmo_nueva_linea
        _separador_linea = separador_linea
        _ignora_parcial_ini = ignora_parcial_ini
        _ignora_parcial_fin = ignora_parcial_fin
        _ignora_total = ignora_total
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

    Public Property Fecha_corte_ini As String
        Get
            Return _fecha_corte_ini
        End Get
        Set(value As String)
            _fecha_corte_ini = value
        End Set
    End Property

    Public Property Fecha_corte_fin As String
        Get
            Return _fecha_corte_fin
        End Get
        Set(value As String)
            _fecha_corte_fin = value
        End Set
    End Property

    Public Property Saldo_inicial_ini As String
        Get
            Return _saldo_inicial_ini
        End Get
        Set(value As String)
            _saldo_inicial_ini = value
        End Set
    End Property

    Public Property Saldo_inicial_fin As String
        Get
            Return _saldo_inicial_fin
        End Get
        Set(value As String)
            _saldo_inicial_fin = value
        End Set
    End Property

    Public Property Algoritmo_nueva_linea As String
        Get
            Return _algoritmo_nueva_linea
        End Get
        Set(value As String)
            _algoritmo_nueva_linea = value
        End Set
    End Property

    Public Property Separador_linea As String
        Get
            Return _separador_linea
        End Get
        Set(value As String)
            _separador_linea = value
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

    Public Property Ignora_total As String
        Get
            Return _ignora_total
        End Get
        Set(value As String)
            _ignora_total = value
        End Set
    End Property
#End Region


End Class
