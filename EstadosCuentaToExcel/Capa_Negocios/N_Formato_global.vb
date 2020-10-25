Imports Capa_Datos
Imports Capa_Identidad
Public Class N_Formato_global
    Private tabla As String = "formato_global"

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

        With iden
            .Id = res.Rows(0).Item(0)
            .Id_formato = res.Rows(0).Item(1)
            .Rfc_ini = res.Rows(0).Item(2)
            .Rfc_fin = res.Rows(0).Item(3)
            .Saldo_inicial_ini = res.Rows(0).Item(4)
            .Saldo_inicial_fin = res.Rows(0).Item(5)
            .Mes_ini = res.Rows(0).Item(6)
            .Mes_fin = res.Rows(0).Item(7)
            .Anio_ini = res.Rows(0).Item(8)
            .Anio_fin = res.Rows(0).Item(9)
            .Cuenta_ini = res.Rows(0).Item(10)
            .Cuenta_fin = res.Rows(0).Item(11)
            .Algoritmo_nueva_linea = res.Rows(0).Item(12)
            .Separador_linea = res.Rows(0).Item(13)
            .Ignora_parcial_ini = res.Rows(0).Item(14)
            .Ignora_parcial_fin = res.Rows(0).Item(15)
            .Ignora_total = res.Rows(0).Item(16)
        End With

        Return iden
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
