Imports Capa_Datos
Imports Capa_Identidad
Public Class N_Formato_campos
    Private tabla As String = "formato_campos"

#Region "FUNCIONES PUBLICAS"

    ''' <summary>
    ''' Inserta un elemento nuevo
    ''' </summary>
    ''' <param name="obj">Objeto a insertar</param>
    ''' <returns>True - si se ha insertado</returns>
    Public Function Insertar(ByVal obj As I_formato_campos) As Boolean
        Dim db As New D_db_operaciones(tabla)

        Return db.Insertar(obj)
    End Function

    ''' <summary>
    ''' Devuelve lista de elementos asociados al formato
    ''' </summary>
    ''' <param name="id">ID formato</param>
    ''' <returns>Lista de objetos</returns>
    Public Function Consultar(ByVal id As String) As List(Of I_formato_campos)
        Dim db As New D_db_operaciones(tabla)
        Dim campos As New List(Of I_formato_campos)
        Dim iden As I_formato_campos
        Dim res As DataTable

        res = db.Consulta("id_formato", id)

        For Each linea As DataRow In res.Rows
            iden = New I_formato_campos
            With iden
                .Id = linea.Item(0)
                .Id_formato = linea.Item(1)
                .Indice = linea.Item(2)
                .Nombre = linea.Item(3)
            End With

            campos.Add(iden)
        Next

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
