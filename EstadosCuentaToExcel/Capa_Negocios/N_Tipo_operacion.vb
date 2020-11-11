Imports Capa_Datos
Imports Capa_Identidad

Public Class N_Tipo_operacion
    Private tabla As String = "tipo_operacion"

#Region "FUNCIONES PUBLICAS"

    ''' <summary>
    ''' Inserta un elemento nuevo
    ''' </summary>
    ''' <param name="obj">Objeto a insertar</param>
    ''' <returns>True - si se ha insertado</returns>
    Public Function Insertar(ByVal obj As I_Tipo_operacion) As Boolean
        Dim db As New D_db_operaciones(tabla)

        Return db.Insertar(obj)
    End Function

    ''' <summary>
    ''' Devuelve lista de elementos asociados al formato
    ''' </summary>
    ''' <param name="id">ID formato</param>
    ''' <returns>Lista de objetos</returns>
    Public Function Consultar(ByVal id As String) As List(Of I_Tipo_operacion)
        Dim db As New D_db_operaciones(tabla)
        Dim campos As New List(Of I_Tipo_operacion)
        Dim iden As I_Tipo_operacion
        Dim res As DataTable

        Try
            res = db.Consulta("id_formato", id)

            For Each linea As DataRow In res.Rows
                iden = New I_Tipo_operacion
                With iden
                    .Id = linea.Item(0)
                    .Id_formato = linea.Item(1)
                    .Cadena = linea.Item(2)
                    .Cadena_adicional_1 = linea.Item(3)
                    .Cadena_adicional_2 = linea.Item(4)
                    .Cadena_no_contener = linea.Item(5)
                    .Tipo = linea.Item(6)
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
