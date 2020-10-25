Public Class I_formato_global
    Private _id As Integer
    Private _id_formato As String
    Private _rfc_ini As String
    Private _rfc_fin As String
    Private _saldo_inicial_ini As String
    Private _saldo_inicial_fin As String
    Private _mes_ini As String
    Private _mes_fin As String
    Private _anio_ini As String
    Private _anio_fin As String
    Private _cuenta_ini As String
    Private _cuenta_fin As String
    Private _algoritmo_nueva_linea As String
    Private _separador_linea As String
    Private _ignora_parcial_ini As String
    Private _ignora_parcial_fin As String
    Private _ignora_total As String



#Region "CONSTRUCTORES"

    Public Sub New()
    End Sub

    Public Sub New(id As Integer, id_formato As String, rfc_ini As String, rfc_fin As String, saldo_inicial_ini As String, saldo_inicial_fin As String, mes_ini As String, mes_fin As String, anio_ini As String, anio_fin As String, cuenta_ini As String, cuenta_fin As String, algoritmo_nueva_linea As String, separador_linea As String, ignora_parcial_ini As String, ignora_parcial_fin As String, ignora_total As String)
        _id = id
        _id_formato = id_formato
        _rfc_ini = rfc_ini
        _rfc_fin = rfc_fin
        _saldo_inicial_ini = saldo_inicial_ini
        _saldo_inicial_fin = saldo_inicial_fin
        _mes_ini = mes_ini
        _mes_fin = mes_fin
        _anio_ini = anio_ini
        _anio_fin = anio_fin
        _cuenta_ini = cuenta_ini
        _cuenta_fin = cuenta_fin
        _algoritmo_nueva_linea = algoritmo_nueva_linea
        _separador_linea = separador_linea
        _ignora_parcial_ini = ignora_parcial_ini
        _ignora_parcial_fin = ignora_parcial_fin
        _ignora_total = ignora_total
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

    Public Property Mes_ini As String
        Get
            Return _mes_ini
        End Get
        Set(value As String)
            _mes_ini = value
        End Set
    End Property

    Public Property Mes_fin As String
        Get
            Return _mes_fin
        End Get
        Set(value As String)
            _mes_fin = value
        End Set
    End Property

    Public Property Anio_ini As String
        Get
            Return _anio_ini
        End Get
        Set(value As String)
            _anio_ini = value
        End Set
    End Property

    Public Property Anio_fin As String
        Get
            Return _anio_fin
        End Get
        Set(value As String)
            _anio_fin = value
        End Set
    End Property

    Public Property Cuenta_ini As String
        Get
            Return _cuenta_ini
        End Get
        Set(value As String)
            _cuenta_ini = value
        End Set
    End Property

    Public Property Cuenta_fin As String
        Get
            Return _cuenta_fin
        End Get
        Set(value As String)
            _cuenta_fin = value
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
