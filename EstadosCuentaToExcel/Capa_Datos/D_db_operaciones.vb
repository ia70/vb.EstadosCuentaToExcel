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
        Return Lista("id")
    End Function

    ''' <summary>
    ''' Obtiene Lista completa de los registros
    ''' </summary>
    ''' <param name="campo">Nombre del campo</param>
    ''' <returns>DataTable</returns>
    Public Function Lista(ByVal campo As String) As DataTable
        Dim DB As New D_DB_Core
        Dim sql As String
        Dim res As DataTable

        Try
            sql = "SELECT * FROM " & Tabla & " ORDER BY " + campo + " ASC"
            res = DB.Query(sql)
        Catch ex As Exception
            X(ex)
            Return New DataTable
        End Try

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

        Try
            sql = "INSERT INTO " & Tabla & " (" + jsonGetCampos(Obj) + ") VALUES(" + jsonGetValores(Obj) + ")"
            res = DB.Update(sql)
        Catch ex As Exception
            X(ex)
            Return False
        End Try

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

        Try
            sql = "SELECT * FROM " & Tabla & " WHERE " + campo + "=" & id
            Res = DB.Query(sql)
        Catch ex As Exception
            X(ex)
            Return New DataTable
        End Try

        Return Res
    End Function

    ''' <summary>
    ''' Devuelve el ultimo id insertado en la tabla AUTO-INCREMENT
    ''' </summary>
    ''' <returns></returns>
    Public Function GetUltimoID() As Integer
        Dim DB As New D_DB_Core
        Dim sql As String
        Dim Res As DataTable
        Dim indice As Integer

        Try
            sql = "SELECT MAX(id) FROM " & Tabla
            Res = DB.Query(sql)

            indice = Res.Rows(0).Item(0)

            Return indice
        Catch ex As Exception
            X(ex)
            Return -1
        End Try

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

        Try
            sql = "UPDATE " & Tabla & " SET " + jsonGetString(Obj) + " WHERE id=" & jsonGetId(Obj)
            res = DB.Update(sql)
        Catch ex As Exception
            X(ex)
            Return False
        End Try

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

        Try
            sql = "DELETE FROM " & Tabla & " WHERE " + campo + "=" & id
            res = DB.Update(sql)
        Catch ex As Exception
            X(ex)
            Return False
        End Try

        Return res
    End Function

    ''' <summary>
    ''' Elimina todo el contenido de una tabla
    ''' </summary>
    ''' <returns>True - Si exitoso</returns>
    Public Function VaciarTabla() As Boolean
        Dim DB As New D_DB_Core
        Dim sql As String
        Dim res As Boolean

        Try
            sql = "TRUNCATE TABLE " & Tabla
            res = DB.Update(sql)
        Catch ex As Exception
            X(ex)
            Return False
        End Try

        Return res
    End Function

#End Region
End Class
