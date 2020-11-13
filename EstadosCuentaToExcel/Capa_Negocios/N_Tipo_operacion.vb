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
        Dim iden_tipo_operacion As New I_Tipo_operacion_simple
        Dim db_condicion As New N_Condicion
        Dim indice As Integer


        Try
            With iden_tipo_operacion
                If Not IsNothing(obj.Id) Then
                    .Id = obj.Id
                End If

                If Not IsNothing(obj.Id_formato) Then
                    .Id_formato = obj.Id_formato
                End If

                If Not IsNothing(obj.Tipo) Then
                    .Tipo = obj.Tipo
                End If
            End With

            'INSERTAR TIPO_OPERACION SIMPLE
            If db.Insertar(iden_tipo_operacion) Then

                'INSERTAR ID EN CONDICIONES
                indice = db.GetUltimoID
                If indice >= 0 Then
                    obj.SetIdCampos(indice)

                    'INSERTAR CONDICIONES EN BASE DE DATOS
                    If Not IsNothing(obj.Condiciones) Then
                        For Each linea As I_Condicion In obj.Condiciones
                            db_condicion.Insertar(linea)
                        Next
                    End If

                    Return True
                End If
            End If

        Catch ex As Exception
            X(ex)
        End Try

        Return False

    End Function

    ''' <summary>
    ''' Devuelve lista de elementos asociados al formato
    ''' </summary>
    ''' <param name="id">ID formato</param>
    ''' <returns>Lista de objetos</returns>
    Public Function Consultar(ByVal id As String) As List(Of I_Tipo_operacion)
        Dim db As New D_db_operaciones(tabla)
        Dim db_condicion As New N_Condicion
        Dim campos As New List(Of I_Tipo_operacion)
        Dim iden As I_Tipo_operacion
        Dim res As DataTable

        Try
            res = db.Consulta("id_formato", id)

            For Each linea As DataRow In res.Rows
                iden = New I_Tipo_operacion
                With iden
                    .Id = GetInt(linea.Item(0))
                    .Id_formato = GetStr(linea.Item(1))
                    .Tipo = GetBool(linea.Item(2))
                    .Condiciones = db_condicion.Consultar(.Id)
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
        Dim db_condicion As New N_Condicion

        Try
            db_condicion.Eliminar(id)
        Catch ex As Exception
            X(ex)
        End Try

        Return db.Eliminar("id_formato", id)

    End Function

#End Region

End Class
