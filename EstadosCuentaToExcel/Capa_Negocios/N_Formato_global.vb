Imports Capa_Datos
Imports Capa_Identidad
Public Class N_Formato_global
    Private ReadOnly tabla As String = "formato_global"
    Private i As Integer

#Region "FUNCIONES PUBLICAS"

    ''' <summary>
    ''' Inserta un elemento nuevo
    ''' </summary>
    ''' <param name="obj">Objeto a insertar</param>
    ''' <returns>True - si se ha insertado</returns>
    Public Function Insertar(ByVal obj As I_formato_global) As Boolean
        Dim db As New D_db_operaciones(tabla)

        Return db.Insertar(obj)
    End Function

    ''' <summary>
    ''' Devuelve lista de elementos asociados al formato
    ''' </summary>
    ''' <param name="id">ID formato</param>
    ''' <returns>Lista de objetos</returns>
    Public Function Consultar(ByVal id As String) As I_formato_global
        Dim db As New D_db_operaciones(tabla)
        Dim iden As New I_formato_global
        Dim res As DataTable

        res = db.Consulta("id_formato", id)
        i = 0

        With iden

            .Id = res.Rows(0).Item(getIn)
            .Id_formato = res.Rows(0).Item(getIn)
            .Rfc_ini = res.Rows(0).Item(getIn)
            .Rfc_fin = res.Rows(0).Item(getIn)
            .Fecha_general_ini = res.Rows(0).Item(getIn)
            .Fecha_general_fin = res.Rows(0).Item(getIn)
            .Fecha_general_separador = res.Rows(0).Item(getIn)
            .No_cuenta_ini = res.Rows(0).Item(getIn)
            .No_cuenta_fin = res.Rows(0).Item(getIn)
            .Saldo_anterior_ini = res.Rows(0).Item(getIn)
            .Saldo_anterior_fin = res.Rows(0).Item(getIn)
            .Detalles_saldo_ini = res.Rows(0).Item(getIn)
            .Detalles_saldo_fin = res.Rows(0).Item(getIn)

            .Fecha_operacion_activo = res.Rows(0).Item(getIn)
            .Fecha_operacion_ini = res.Rows(0).Item(getIn)
            .Fecha_operacion_fin = res.Rows(0).Item(getIn)
            .Fecha_operacion_dia_length = res.Rows(0).Item(getIn)
            .Fecha_operacion_mes_length = res.Rows(0).Item(getIn)
            .Fecha_operacion_anio_length = res.Rows(0).Item(getIn)
            .Fecha_operacion_separador_dia_mes = res.Rows(0).Item(getIn)

            .Fecha_liquidacion_activo = res.Rows(0).Item(getIn)
            .Fecha_liquidacion_ini = res.Rows(0).Item(getIn)
            .Fecha_liquidacion_fin = res.Rows(0).Item(getIn)
            .Fecha_liquidacion_dia_length = res.Rows(0).Item(getIn)
            .Fecha_liquidacion_mes_length = res.Rows(0).Item(getIn)
            .Fecha_liquidacion_anio_length = res.Rows(0).Item(getIn)
            .Fecha_liquidacion_separador_dia_mes = res.Rows(0).Item(getIn)

            .Ignora_parcial_ini = res.Rows(0).Item(getIn)
            .Ignora_parcial_fin = res.Rows(0).Item(getIn)

        End With

        Return iden
    End Function

    Private Function GetIn() As Integer
        Dim aux As Integer
        aux = i
        i += 1
        Return aux
    End Function

    ''' <summary>
    ''' Elimina elemetos asociados a un formato
    ''' </summary>
    ''' <param name="id">ID formato</param>
    ''' <returns>True - si se ha eliminado</returns>
    Public Function Eliminar(ByVal id As String)
        Dim db As New D_db_operaciones(tabla)

        Return db.Eliminar("id_formato", id)

    End Function

#End Region

End Class
