Imports Capa_Identidad
Imports Capa_Datos

Public Class N_Fechas
    Private tabla As String = "fechas"

#Region "FUNCIONES PUBLICAS"

    ''' <summary>
    ''' Inserta un elemento nuevo
    ''' </summary>
    ''' <param name="obj">Objeto a insertar</param>
    ''' <returns>True - si se ha insertado</returns>
    Public Function Insertar(ByVal obj As I_Fechas) As Boolean
        Dim db As New D_db_operaciones(tabla)

        Return db.Insertar(obj)
    End Function

    ''' <summary>
    ''' Devuelve lista de elementos asociados al formato
    ''' </summary>
    ''' <param name="id">ID formato</param>
    ''' <returns>Lista de objetos</returns>
    Public Function Consultar(ByVal id As String) As List(Of I_Fechas)
        Dim db As New D_db_operaciones(tabla)
        Dim fechas As New List(Of I_Fechas)
        Dim iden As I_Fechas
        Dim res As DataTable

        Try
            res = db.Consulta("id_formato", id)

            For Each linea As DataRow In res.Rows
                iden = New I_Fechas
                With iden
                    .Id = GetInt(linea.Item(0))
                    .Id_formato = GetStr(linea.Item(1))
                    .Tipo = GetStr(linea.Item(2))
                    .Inicio = GetStr(linea(3))
                    .Fin = GetStr(linea(4))
                    .Dia_length = GetInt(linea(5))
                    .Mes_length = GetInt(linea(6))
                    .Anio_length = GetInt(linea(7))
                    .Separador = GetStr(linea(8))
                End With

                fechas.Add(iden)
            Next
        Catch ex As Exception
            X(ex)
        End Try

        Return fechas
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
