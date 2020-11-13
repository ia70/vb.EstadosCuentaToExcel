Imports System.IO
Imports Capa_Datos
Imports Capa_Identidad
Imports Newtonsoft.Json

Public Class N_Formato
    Private ReadOnly tabla As String = "formato"

#Region "FUNCIONES PUBLICAS"

    Public Function ListaSimple() As DataTable
        Dim db As New D_db_operaciones(tabla)

        Return db.Lista("id_formato")

    End Function

    Public Function ListaCompleta() As List(Of I_formato)
        Dim lista As DataTable
        Dim formatos As New List(Of I_formato)

        lista = ListaSimple()

        Try
            For Each linea As DataRow In lista.Rows
                formatos.Add(Consultar(linea.Item(0)))
            Next
        Catch ex As Exception
            X(ex)
        End Try

        Return formatos
    End Function

    Public Function Insertar(ByVal ruta As String) As Boolean
        Dim iden_formato As I_Formato
        Dim iden_formato_simple As New I_Formato_simple
        Dim db_formato As New D_db_operaciones(tabla)
        Dim db_tipo_operacion As New N_Tipo_operacion
        Dim db_campos As New N_Campos
        Dim db_prefijos As New N_Prefijos

        Try
            iden_formato = LeerFichero(ruta)
            With iden_formato
                iden_formato_simple.Id_formato = .Id_formato
                iden_formato_simple.Banco = .Banco
                iden_formato_simple.Algoritmo = .Algoritmo
                iden_formato_simple.Cadena = .Cadena
                iden_formato_simple.Idcamposdescripcion = .Idcamposdescripcion
            End With
        Catch ex As Exception
            X(ex)
            iden_formato = New I_Formato
        End Try

        Try
            If Existe(iden_formato.Id_formato) Then
                Return False
            End If

            'INSERTAR FORMATO
            If Not db_formato.Insertar(iden_formato_simple) Then
                Throw New Exception("Error")
            End If

            'INSERTAR PREFIJOS
            iden_formato.Prefijos.Id_formato = iden_formato.Id_formato
            If Not db_prefijos.Insertar(iden_formato.Prefijos) Then
                Throw New Exception("Error")
            End If

            'INSERTAR TIPO_OPERACION
            For Each linea As I_Tipo_operacion In iden_formato.Tipo_operacion
                linea.Id_formato = iden_formato.Id_formato
                If Not db_tipo_operacion.Insertar(linea) Then
                    Throw New Exception("Error")
                End If
            Next

            'INSERTAR CAMPOS
            For Each linea As I_Campos In iden_formato.Campos
                linea.Id_formato = iden_formato.Id_formato
                If Not db_campos.Insertar(linea) Then
                    Throw New Exception("Error")
                End If
            Next

            Return True
        Catch ex As Exception
            X(ex)
            Eliminar(iden_formato.Id_formato)
            Return False
        End Try

    End Function

    Public Function Consultar(ByVal id As String) As I_formato
        Dim db As New D_db_operaciones(tabla)
        Dim iden As New I_formato
        Dim res As DataTable

        Dim db_tipo_operacion As New N_Tipo_operacion
        Dim db_fc As New N_Campos
        Dim db_prefijos As New N_Prefijos

        Try
            id = "'" + id + "'"
            res = db.Consulta("id_formato", id)

            With iden
                .Id_formato = GetStr(res.Rows(0).Item(0))
                .Banco = GetStr(res.Rows(0).Item(1))
                .Algoritmo = GetStr(res.Rows(0).Item(2))
                .Cadena = GetStr(res.Rows(0).Item(3))
                .Idcamposdescripcion = GetStr(res.Rows(0).Item(4))
                .Campos = db_fc.Consultar(id)
                .Tipo_operacion = db_tipo_operacion.Consultar(id)
                .Prefijos = db_prefijos.Consultar(id)
            End With
        Catch ex As Exception
            X(ex)
        End Try

        Return iden
    End Function

    Private Function Existe(ByVal id As String) As Boolean
        Dim db As New D_db_operaciones(tabla)
        Dim dtabla As DataTable

        Try
            id = "'" + id + "'"
            dtabla = db.Consulta("id_formato", id)

            If dtabla.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            X(ex)
            Return False
        End Try

    End Function

    Public Function Eliminar(ByVal id As String)
        Dim res As Boolean
        Dim db As New D_db_operaciones(tabla)
        Dim db_tipo_operacion As New N_Tipo_operacion
        Dim db_fc As New N_Campos
        Dim db_fg As New N_Prefijos

        id = "'" + id + "'"

        On Error Resume Next
        res = db.Eliminar("id_formato", id)
        On Error Resume Next
        db_tipo_operacion.Eliminar(id)
        On Error Resume Next
        db_fc.Eliminar(id)
        On Error Resume Next
        db_fg.Eliminar(id)

        Return res
    End Function

#End Region

#Region "FUNCIONES PRIVADAS"

    Private Function LeerFichero(ByVal fichero As String) As I_Formato
        Dim texto As String

        Try
            Dim sr As New StreamReader(fichero)
            texto = sr.ReadToEnd()
            sr.Close()

            If texto.Length > 0 Then
                Return TextToObj(texto)
            Else
                Return Nothing
            End If
        Catch ex As Exception
            X(ex)
            Return Nothing
        End Try

    End Function

    Private Function TextToObj(ByVal cadena As String) As I_formato
        Try
            Return JsonConvert.DeserializeObject(Of I_formato)(cadena)
        Catch ex As Exception
            X(ex)
            Return Nothing
        End Try
    End Function

#End Region
End Class
