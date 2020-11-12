Imports Capa_Identidad

Public Class I_Prefijos

    Private _id_formato As String
    Private _rfc As I_Prefijo_simple
    Private _fecha_general As I_Prefijo_simple
    Private _no_cuenta As I_Prefijo_simple
    Private _saldo_anterior As I_Prefijo_simple
    Private _detalles_saldo As I_Prefijo_simple

    Private _folio As I_Prefijo_simple
    Private _referencia As I_Prefijo_simple

    Private _fechas_registro As List(Of I_Fechas)
    Private _ignora_parcial As List(Of I_Prefijo_simple)
    Private _fin_documento As List(Of I_Prefijo_simple)

#Region "CONSTRUCTIRES"

    Public Sub New()
    End Sub

    Public Sub New(id_formato As String, rfc As I_Prefijo_simple, fecha_general As I_Prefijo_simple, no_cuenta As I_Prefijo_simple, saldo_anterior As I_Prefijo_simple, detalles_saldo As I_Prefijo_simple, folio As I_Prefijo_simple, referencia As I_Prefijo_simple, fechas_registro As List(Of I_Fechas), ignora_parcial As List(Of I_Prefijo_simple), fin_documento As List(Of I_Prefijo_simple))
        _id_formato = id_formato
        _rfc = rfc
        _fecha_general = fecha_general
        _no_cuenta = no_cuenta
        _saldo_anterior = saldo_anterior
        _detalles_saldo = detalles_saldo
        _folio = folio
        _referencia = referencia
        _fechas_registro = fechas_registro
        _ignora_parcial = ignora_parcial
        _fin_documento = fin_documento
    End Sub




#End Region
#Region "PROPIEDADES"

    Public Property Rfc As I_Prefijo_simple
        Get
            Return _rfc
        End Get
        Set(value As I_Prefijo_simple)
            _rfc = value
        End Set
    End Property

    Public Property Fecha_general As I_Prefijo_simple
        Get
            Return _fecha_general
        End Get
        Set(value As I_Prefijo_simple)
            _fecha_general = value
        End Set
    End Property

    Public Property No_cuenta As I_Prefijo_simple
        Get
            Return _no_cuenta
        End Get
        Set(value As I_Prefijo_simple)
            _no_cuenta = value
        End Set
    End Property

    Public Property Saldo_anterior As I_Prefijo_simple
        Get
            Return _saldo_anterior
        End Get
        Set(value As I_Prefijo_simple)
            _saldo_anterior = value
        End Set
    End Property

    Public Property Detalles_saldo As I_Prefijo_simple
        Get
            Return _detalles_saldo
        End Get
        Set(value As I_Prefijo_simple)
            _detalles_saldo = value
        End Set
    End Property

    Public Property Fechas_registro As List(Of I_Fechas)
        Get
            Return _fechas_registro
        End Get
        Set(value As List(Of I_Fechas))
            _fechas_registro = value
        End Set
    End Property

    Public Property Ignora_parcial As List(Of I_Prefijo_simple)
        Get
            Return _ignora_parcial
        End Get
        Set(value As List(Of I_Prefijo_simple))
            _ignora_parcial = value
        End Set
    End Property

    Public Property Fin_documento As List(Of I_Prefijo_simple)
        Get
            Return _fin_documento
        End Get
        Set(value As List(Of I_Prefijo_simple))
            _fin_documento = value
        End Set
    End Property

    Public Property Id_formato As String
        Get
            Return _id_formato
        End Get
        Set(value As String)
            _id_formato = value
            setIdFormato()
        End Set
    End Property

    Public Property Folio As I_Prefijo_simple
        Get
            Return _folio
        End Get
        Set(value As I_Prefijo_simple)
            _folio = value
        End Set
    End Property

    Public Property Referencia As I_Prefijo_simple
        Get
            Return _referencia
        End Get
        Set(value As I_Prefijo_simple)
            _referencia = value
        End Set
    End Property

#End Region
#Region "FUNCIONES"

    Private Sub setIdFormato()
        Try

            Rfc.Id_formato = Id_formato
            Fecha_general.Id_formato = Id_formato
            No_cuenta.Id_formato = Id_formato
            Saldo_anterior.Id_formato = Id_formato
            Detalles_saldo.Id_formato = Id_formato
            Folio.Id_formato = Id_formato
            Referencia.Id_formato = Id_formato

            For Each linea As I_Fechas In Fechas_registro
                linea.Id_formato = Id_formato
            Next

            For Each linea As I_Prefijo_simple In Ignora_parcial
                linea.Id_formato = Id_formato
            Next

            For Each linea As I_Prefijo_simple In Fin_documento
                linea.Id_formato = Id_formato
            Next

        Catch ex As Exception
            X(ex)
        End Try
    End Sub

#End Region
End Class
