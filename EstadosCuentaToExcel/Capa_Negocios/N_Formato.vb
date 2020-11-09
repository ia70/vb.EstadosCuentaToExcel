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
        Dim iden_formato As I_formato
        Dim f_simple As New I_Formato_simple
        Dim db_f As New D_db_operaciones(tabla)
        Dim db_fce As New N_Formato_campo_egreso
        Dim db_fci As New N_Formato_campo_ingreso
        Dim db_fc As New N_Formato_campos
        Dim db_fg As New N_Formato_global

        Try
            iden_formato = LeerFichero(ruta)
            With iden_formato
                f_simple.Id_formato = .Id_formato
                f_simple.Banco = .Banco
                f_simple.Algoritmo = .Algoritmo
                f_simple.Cadena = .Cadena
                f_simple.Idcamposdescripcion = .Idcamposdescripcion
            End With
        Catch ex As Exception
            X(ex)
            iden_formato = New I_formato
        End Try

        Try
            If Existe(iden_formato.Id_formato) Then
                Return False
            End If

            If Not db_f.Insertar(f_simple) Then
                Throw New Exception("Error")
            End If

            For Each ifce As I_Formato_campo_egreso In iden_formato.Formato_campo_egreso
                ifce.Id_formato = iden_formato.Id_formato
                If Not db_fce.Insertar(ifce) Then
                    Throw New Exception("Error")
                End If
            Next

            For Each ifci As I_Formato_campo_ingreso In iden_formato.Formato_campo_ingreso
                ifci.Id_formato = iden_formato.Id_formato
                If Not db_fci.Insertar(ifci) Then
                    Throw New Exception("Error")
                End If
            Next

            For Each ifc As I_Formato_campos In iden_formato.Formato_campos
                ifc.Id_formato = iden_formato.Id_formato
                If Not db_fc.Insertar(ifc) Then
                    Throw New Exception("Error")
                End If
            Next

            iden_formato.Formato_global.Id_formato = iden_formato.Id_formato
            If Not db_fg.Insertar(iden_formato.Formato_global) Then
                Throw New Exception("Error")
            End If

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

        Dim db_fce As New N_Formato_campo_egreso
        Dim db_fci As New N_Formato_campo_ingreso
        Dim db_fc As New N_Formato_campos
        Dim db_fg As New N_Formato_global

        Try
            id = "'" + id + "'"
            res = db.Consulta("id_formato", id)

            With iden
                .Id_formato = res.Rows(0).Item(0)
                .Banco = res.Rows(0).Item(1)
                .Algoritmo = res.Rows(0).Item(2)
                .Cadena = res.Rows(0).Item(3)
                .Idcamposdescripcion = res.Rows(0).Item(4)
                .Formato_campos = db_fc.Consultar(id)
                .Formato_campo_egreso = db_fce.Consultar(id)
                .Formato_campo_ingreso = db_fci.Consultar(id)
                .Formato_global = db_fg.Consultar(id)
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
        Dim db_fce As New N_Formato_campo_egreso
        Dim db_fci As New N_Formato_campo_ingreso
        Dim db_fc As New N_Formato_campos
        Dim db_fg As New N_Formato_global

        id = "'" + id + "'"

        On Error Resume Next
        res = db.Eliminar("id_formato", id)
        On Error Resume Next
        db_fce.Eliminar(id)
        On Error Resume Next
        db_fci.Eliminar(id)
        On Error Resume Next
        db_fc.Eliminar(id)
        On Error Resume Next
        db_fg.Eliminar(id)

        Return res
    End Function

#End Region

#Region "FUNCIONES PRIVADAS"

    Private Function LeerFichero(ByVal fichero As String) As I_formato
        Dim texto As String = ""

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
