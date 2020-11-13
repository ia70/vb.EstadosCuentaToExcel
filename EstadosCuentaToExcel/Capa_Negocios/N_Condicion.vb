Imports Capa_Datos
Imports Capa_Identidad
Public Class N_Condicion
    Private tabla As String = "condicion"

#Region "FUNCIONES PUBLICAS"

    ''' <summary>
    ''' Inserta un elemento nuevo
    ''' </summary>
    ''' <param name="obj">Objeto a insertar</param>
    ''' <returns>True - si se ha insertado</returns>
    Public Function Insertar(ByVal obj As I_Condicion) As Boolean
        Dim db As New D_db_operaciones(tabla)

        Return db.Insertar(obj)
    End Function

    ''' <summary>
    ''' Devuelve lista de elementos asociados al formato
    ''' </summary>
    ''' <param name="id">ID formato</param>
    ''' <returns>Lista de objetos</returns>
    Public Function Consultar(ByVal id As String) As List(Of I_Condicion)
        Dim db As New D_db_operaciones(tabla)
        Dim campos As New List(Of I_Condicion)
        Dim iden As I_Condicion
        Dim res As DataTable

        Try
            res = db.Consulta("id_tipo_operacion", id)

            For Each linea As DataRow In res.Rows
                iden = New I_Condicion
                With iden
                    .Id = GetInt(linea.Item(0))
                    .Id_formato = GetStr(linea(1))
                    .Id_tipo_operacion = GetInt(linea.Item(2))
                    .Tipo = GetBool(linea.Item(3))
                    .Cadena = GetStr(linea(4))
                End With

                campos.Add(iden)
            Next
        Catch ex As Exception
            X(ex)
        End Try

        Return campos
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
