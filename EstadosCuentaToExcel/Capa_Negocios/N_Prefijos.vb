Imports Capa_Datos
Imports Capa_Identidad
Public Class N_Prefijos
    Private ReadOnly tabla As String = "prefijos"

#Region "FUNCIONES PUBLICAS"

    ''' <summary>
    ''' Inserta un elemento nuevo
    ''' </summary>
    ''' <param name="obj">Objeto a insertar</param>
    ''' <returns>True - si se ha insertado</returns>
    Public Function Insertar(ByVal obj As I_Prefijos) As Boolean
        Dim db As New D_db_operaciones(tabla)
        Dim db_fechas As New N_Fechas

        Try
            If Not IsNothing(obj.Rfc) Then
                db.Insertar(obj.Rfc)
            End If

            If Not IsNothing(obj.Fecha_general) Then
                db.Insertar(obj.Fecha_general)
            End If

            If Not IsNothing(obj.Moneda) Then
                db.Insertar(obj.Moneda)
            End If

            If Not IsNothing(obj.No_cuenta) Then
                db.Insertar(obj.No_cuenta)
            End If

            If Not IsNothing(obj.No_cuenta_2) Then
                db.Insertar(obj.No_cuenta_2)
            End If

            If Not IsNothing(obj.Saldo_anterior) Then
                db.Insertar(obj.Saldo_anterior)
            End If

            If Not IsNothing(obj.Detalles_saldo) Then
                db.Insertar(obj.Detalles_saldo)
            End If

            If Not IsNothing(obj.Referencia) Then
                db.Insertar(obj.Referencia)
            End If

            If Not IsNothing(obj.Folio) Then
                db.Insertar(obj.Folio)
            End If

            For Each linea As I_Prefijo_simple In obj.Ignora_parcial
                db.Insertar(linea)
            Next

            For Each linea As I_Prefijo_simple In obj.Fin_documento
                db.Insertar(linea)
            Next

            For Each linea As I_Fechas In obj.Fechas_registro
                db_fechas.Insertar(linea)
            Next

            Return True
        Catch ex As Exception
            X(ex)
            Return False
        End Try

    End Function

    ''' <summary>
    ''' Devuelve lista de elementos asociados al formato
    ''' </summary>
    ''' <param name="id">ID formato</param>
    ''' <returns>Lista de objetos</returns>
    Public Function Consultar(ByVal id As String) As I_Prefijos
        Dim db As New D_db_operaciones(tabla)
        Dim db_fechas As New N_Fechas
        Dim iden As New I_Prefijos
        Dim res As DataTable

        Try
            res = db.Consulta("id_formato", id)

            'OBTIENE LOS CAMPOS prefijos_simples
            With iden
                .Id_formato = id
                For Each linea As DataRow In res.Rows
                    Select Case GetStr(linea.Item(2)).ToLower
                        Case "rfc"
                            .Rfc = New I_Prefijo_simple(GetInt(linea.Item(0)), GetStr(linea.Item(1)), GetStr(linea.Item(2)), GetStr(linea.Item(3)), GetStr(linea.Item(4)), GetStr(linea.Item(5)), GetStr(linea.Item(6)))
                        Case "fecha_general"
                            .Fecha_general = New I_Prefijo_simple(GetInt(linea.Item(0)), GetStr(linea.Item(1)), GetStr(linea.Item(2)), GetStr(linea.Item(3)), GetStr(linea.Item(4)), GetStr(linea.Item(5)), GetStr(linea.Item(6)))
                        Case "moneda"
                            .Moneda = New I_Prefijo_simple(GetInt(linea.Item(0)), GetStr(linea.Item(1)), GetStr(linea.Item(2)), GetStr(linea.Item(3)), GetStr(linea.Item(4)), GetStr(linea.Item(5)), GetStr(linea.Item(6)))
                        Case "no_cuenta"
                            .No_cuenta = New I_Prefijo_simple(GetInt(linea.Item(0)), GetStr(linea.Item(1)), GetStr(linea.Item(2)), GetStr(linea.Item(3)), GetStr(linea.Item(4)), GetStr(linea.Item(5)), GetStr(linea.Item(6)))
                        Case "no_cuenta_2"
                            .No_cuenta_2 = New I_Prefijo_simple(GetInt(linea.Item(0)), GetStr(linea.Item(1)), GetStr(linea.Item(2)), GetStr(linea.Item(3)), GetStr(linea.Item(4)), GetStr(linea.Item(5)), GetStr(linea.Item(6)))
                        Case "saldo_anterior"
                            .Saldo_anterior = New I_Prefijo_simple(GetInt(linea.Item(0)), GetStr(linea.Item(1)), GetStr(linea.Item(2)), GetStr(linea.Item(3)), GetStr(linea.Item(4)), GetStr(linea.Item(5)), GetStr(linea.Item(6)))
                        Case "detalles_saldo"
                            .Detalles_saldo = New I_Prefijo_simple(GetInt(linea.Item(0)), GetStr(linea.Item(1)), GetStr(linea.Item(2)), GetStr(linea.Item(3)), GetStr(linea.Item(4)), GetStr(linea.Item(5)), GetStr(linea.Item(6)))
                        Case "folio"
                            .Folio = New I_Prefijo_simple(GetInt(linea.Item(0)), GetStr(linea.Item(1)), GetStr(linea.Item(2)), GetStr(linea.Item(3)), GetStr(linea.Item(4)), GetStr(linea.Item(5)), GetStr(linea.Item(6)))
                        Case "referencia"
                            .Referencia = New I_Prefijo_simple(GetInt(linea.Item(0)), GetStr(linea.Item(1)), GetStr(linea.Item(2)), GetStr(linea.Item(3)), GetStr(linea.Item(4)), GetStr(linea.Item(5)), GetStr(linea.Item(6)))
                        Case "ignora_parcial"
                            .Ignora_parcial.Add(New I_Prefijo_simple(GetInt(linea.Item(0)), GetStr(linea.Item(1)), GetStr(linea.Item(2)), GetStr(linea.Item(3)), GetStr(linea.Item(4)), GetStr(linea.Item(5)), GetStr(linea.Item(6))))
                        Case "fin_documento"
                            .Fin_documento.Add(New I_Prefijo_simple(GetInt(linea.Item(0)), GetStr(linea.Item(1)), GetStr(linea.Item(2)), GetStr(linea.Item(3)), GetStr(linea.Item(4)), GetStr(linea.Item(5)), GetStr(linea.Item(6))))
                    End Select
                Next
            End With

            iden.Fechas_registro = db_fechas.Consultar(id)

        Catch ex As Exception
            X(ex)
        End Try

        Return iden
    End Function

    ''' <summary>
    ''' Elimina elemetos asociados a un formato
    ''' </summary>
    ''' <param name="id">ID formato</param>
    ''' <returns>True - si se ha eliminado</returns>
    Public Function Eliminar(ByVal id As String)
        Dim db As New D_db_operaciones(tabla)
        Dim db_fechas As New N_Fechas

        Try
            db_fechas.Eliminar(id)
        Catch ex As Exception

        End Try

        Return db.Eliminar("id_formato", id)

    End Function

#End Region

End Class
