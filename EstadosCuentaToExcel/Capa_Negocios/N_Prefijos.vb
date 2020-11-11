Imports Capa_Datos
Imports Capa_Identidad
Public Class N_Prefijos
    Private ReadOnly tabla As String = "prefijos"
    Private i As Integer

#Region "FUNCIONES PUBLICAS"

    ''' <summary>
    ''' Inserta un elemento nuevo
    ''' </summary>
    ''' <param name="obj">Objeto a insertar</param>
    ''' <returns>True - si se ha insertado</returns>
    Public Function Insertar(ByVal obj As I_Prefijos) As Boolean
        Dim db As New D_db_operaciones(tabla)

        Return db.Insertar(obj)
    End Function

    ''' <summary>
    ''' Devuelve lista de elementos asociados al formato
    ''' </summary>
    ''' <param name="id">ID formato</param>
    ''' <returns>Lista de objetos</returns>
    Public Function Consultar(ByVal id As String) As I_Prefijos
        Dim db As New D_db_operaciones(tabla)
        Dim iden As New I_Prefijos
        Dim res As DataTable

        Try
            res = db.Consulta("id_formato", id)
            i = 0

            With iden

                .Id = GetInt(res.Rows(0).Item(GetIn))
                .Id_formato = GetStr(res.Rows(0).Item(GetIn))
                .Rfc_ini = GetStr(res.Rows(0).Item(GetIn))
                .Rfc_fin = GetStr(res.Rows(0).Item(GetIn))
                .Fecha_general_ini = GetStr(res.Rows(0).Item(GetIn))
                .Fecha_general_fin = GetStr(res.Rows(0).Item(GetIn))
                .Fecha_general_separador = GetStr(res.Rows(0).Item(GetIn))
                .No_cuenta_ini = GetStr(res.Rows(0).Item(GetIn))
                .No_cuenta_fin = GetStr(res.Rows(0).Item(GetIn))
                .Saldo_anterior_ini = GetStr(res.Rows(0).Item(GetIn))
                .Saldo_anterior_fin = GetStr(res.Rows(0).Item(GetIn))
                .Detalles_saldo_ini = GetStr(res.Rows(0).Item(GetIn))
                .Detalles_saldo_fin = GetStr(res.Rows(0).Item(GetIn))

                .Fecha_operacion_activo = GetBool(res.Rows(0).Item(GetIn))
                .Fecha_operacion_ini = GetStr(res.Rows(0).Item(GetIn))
                .Fecha_operacion_fin = GetStr(res.Rows(0).Item(GetIn))
                .Fecha_operacion_dia_length = GetInt(res.Rows(0).Item(GetIn))
                .Fecha_operacion_mes_length = GetInt(res.Rows(0).Item(GetIn))
                .Fecha_operacion_anio_length = GetInt(res.Rows(0).Item(GetIn))
                .Fecha_operacion_separador_dia_mes = GetStr(res.Rows(0).Item(GetIn))

                .Fecha_liquidacion_activo = GetBool(res.Rows(0).Item(GetIn))
                .Fecha_liquidacion_ini = GetStr(res.Rows(0).Item(GetIn))
                .Fecha_liquidacion_fin = GetStr(res.Rows(0).Item(GetIn))
                .Fecha_liquidacion_dia_length = GetInt(res.Rows(0).Item(GetIn))
                .Fecha_liquidacion_mes_length = GetInt(res.Rows(0).Item(GetIn))
                .Fecha_liquidacion_anio_length = GetInt(res.Rows(0).Item(GetIn))
                .Fecha_liquidacion_separador_dia_mes = GetStr(res.Rows(0).Item(GetIn))

                .Ignora_parcial_ini = GetStr(res.Rows(0).Item(GetIn))
                .Ignora_parcial_fin = GetStr(res.Rows(0).Item(GetIn))
                .Ignora_parcial_adicional_1_ini = GetStr(res.Rows(0).Item(GetIn))
                .Ignora_parcial_adicional_1_fin = GetStr(res.Rows(0).Item(GetIn))
                .Ignora_parcial_adicional_2_ini = GetStr(res.Rows(0).Item(GetIn))
                .Ignora_parcial_adicional_2_fin = GetStr(res.Rows(0).Item(GetIn))

                .Folio_activo = GetBool(res.Rows(0).Item(GetIn))
                .Folio_ini = GetStr(res.Rows(0).Item(GetIn).ToString)
                .Folio_fin = GetStr(res.Rows(0).Item(GetIn).ToString)
                .Folio_length = GetInt(res.Rows(0).Item(GetIn))
                .Folio_tipo = GetStr(res.Rows(0).Item(GetIn).ToString)

                .Referencia_activo = GetBool(res.Rows(0).Item(GetIn))
                .Referencia_ini = GetStr(res.Rows(0).Item(GetIn).ToString)
                .Referencia_fin = GetStr(res.Rows(0).Item(GetIn).ToString)


            End With
        Catch ex As Exception
            X(ex)
        End Try

        Return iden
    End Function

    Private Function GetStr(ByVal valor As Object) As String
        Dim aux As String

        Try
            aux = valor
            Return valor
        Catch ex As Exception
            Return ""
        End Try

    End Function

    Private Function GetInt(ByVal valor As Object) As Integer
        Dim aux As Integer

        Try
            aux = valor
            Return valor
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Private Function GetBool(ByVal valor As Object) As Boolean
        Dim aux As Boolean

        Try
            aux = valor
            Return valor
        Catch ex As Exception
            Return False
        End Try
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
