Public Class D_db_operaciones
#Region "Variables"
    Private Tabla As String = ""

    Public Sub New(ByVal tabla As String)
        Me.Tabla = tabla
    End Sub

#End Region
#Region "Funciones"
    ''' <summary>
    ''' Obtiene Lista completa de los registros
    ''' </summary>
    ''' <returns>DataTable</returns>
    Public Function Lista() As DataTable
        Dim DB As New D_DB_Core
        Dim sql As String
        Dim res As DataTable

        sql = "SELECT * FROM " & Tabla & " ORDER BY id ASC"
        res = DB.Query(sql)

        Return res
    End Function

    ''' <summary>
    ''' Insertar
    ''' </summary>
    ''' <param name="Obj">Objeto Identidad</param>
    ''' <returns>True - Si exitoso</returns>
    Public Function Insertar(ByVal Obj As Object) As Boolean
        Dim DB As New D_DB_Core
        Dim sql As String
        Dim res As Boolean

        sql = "INSERT INTO " & Tabla & " (" + jsonGetCampos(Obj) + ") VALUES(" + jsonGetValores(Obj) + ")"
        res = DB.Update(sql)

        Return res

    End Function

    ''' <summary>
    ''' Consultar un registro
    ''' </summary>
    ''' <param name="id">Registro a buscar</param>
    ''' <returns>True - Si exitoso</returns>
    Public Function Consulta(ByVal id As String) As DataTable

        Return Consulta("id", id)

    End Function

    ''' <summary>
    ''' Consultar un registro
    ''' </summary>
    ''' <param name="campo">Nombre del campo</param>
    ''' <param name="id">Registro a buscar</param>
    ''' <returns>True - Si exitoso</returns>
    Public Function Consulta(ByVal campo As String, ByVal id As String) As DataTable
        Dim DB As New D_DB_Core
        Dim sql As String
        Dim Res As DataTable

        sql = "SELECT * FROM " & Tabla & " WHERE " + campo + "=" & id
        Res = DB.Query(sql)

        Return Res

    End Function

    ''' <summary>
    ''' Editar un registro
    ''' </summary>
    ''' <param name="Obj">Objeto Identidad</param>
    ''' <returns>True - Si exitoso</returns>
    Public Function Editar(ByVal Obj As Object) As Boolean
        Dim DB As New D_DB_Core
        Dim sql As String
        Dim res As Boolean

        sql = "UPDATE " & Tabla & " SET " + jsonGetString(Obj) + " WHERE id=" & jsonGetId(Obj)
        res = DB.Update(sql)

        Return res

    End Function

    ''' <summary>
    ''' Eliminar registro
    ''' </summary>
    ''' <param name="id">Registro a eliminar</param>
    ''' <returns>True - Si exitoso</returns>
    Public Function Eliminar(ByVal id As String) As Boolean

        Return Eliminar("id", id)

    End Function

    ''' <summary>
    ''' Eliminar registro
    ''' </summary>
    ''' <param name="campo">Nombre del campo</param>
    ''' <param name="id">Registro a eliminar</param>
    ''' <returns>True - Si exitoso</returns>
    Public Function Eliminar(ByVal campo As String, ByVal id As String) As Boolean
        Dim DB As New D_DB_Core
        Dim sql As String
        Dim res As Boolean

        sql = "DELETE FROM " & Tabla & " WHERE " + campo + "=" & id
        res = DB.Update(sql)

        Return res

    End Function

#End Region
End Class
