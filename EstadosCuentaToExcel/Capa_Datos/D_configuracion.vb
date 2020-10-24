Public Class D_configuracion
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
        Dim DB As New DataBase
        Return DB.Query("SELECT * FROM " & Tabla & " ORDER BY id ASC")
    End Function

    ''' <summary>
    ''' Insertar
    ''' </summary>
    ''' <param name="Obj">Objeto Identidad</param>
    ''' <returns>True - Si exitoso</returns>
    Public Function Insertar(ByVal Obj As Object) As Boolean
        Dim DB As New DataBase

        Return DB.Update("INSERT INTO " & Tabla & " (" + jsonGetCampos(Obj) + ") VALUES(" + jsonGetValores(Obj) + ")")

    End Function

    ''' <summary>
    ''' Consultar un registro
    ''' </summary>
    ''' <param name="Registro">Clave Primaria</param>
    ''' <returns>True - Si exitoso</returns>
    Public Function Consulta(ByVal Registro As String) As DataTable
        Dim DB As New DataBase
        Return DB.Query("SELECT * FROM " & Tabla & " WHERE id=" & Registro & "")
    End Function

    ''' <summary>
    ''' Editar un registro
    ''' </summary>
    ''' <param name="Obj">Objeto Identidad</param>
    ''' <returns>True - Si exitoso</returns>
    Public Function Editar(ByVal Obj As Object) As Boolean
        Dim DB As New DataBase

        Return DB.Update("UPDATE " & Tabla & " SET " + jsonGetString(Obj) + " WHERE id='" & jsonGetId(Obj) & "'")

    End Function

    ''' <summary>
    ''' Eliminar registro
    ''' </summary>
    ''' <param name="ID">Clave Primaria</param>
    ''' <returns>True - Si exitoso</returns>
    Public Function Eliminar(ByVal ID As String) As Boolean
        Dim DB As New DataBase

        Return DB.Update("DELETE FROM " & Tabla & " WHERE id='" & ID & "'")

    End Function

#End Region
End Class
