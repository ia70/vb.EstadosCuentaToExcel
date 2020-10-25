Imports System.IO
Imports Capa_Datos
Imports Capa_Identidad
Imports Newtonsoft.Json

Public Class N_Formato
    Private tabla As String = "formato"

#Region "FUNCIONES PUBLICAS"

    Public Function ListaSimple() As DataTable
        Dim db As New D_db_operaciones(tabla)

        Return db.Lista

    End Function

    Public Function ListaCompleta() As List(Of I_formato)
        Dim lista As DataTable
        Dim formatos As New List(Of I_formato)

        lista = ListaSimple()

        For Each linea As DataRow In lista.Rows
            formatos.Add(Consultar(linea.Item(0)))
        Next

        Return formatos
    End Function

    Public Function Insertar(ByVal ruta As String) As Boolean
        Dim res As Boolean

        Dim iden_formato As I_formato
        Dim db_f As New D_db_operaciones(tabla)
        Dim db_fce As New N_Formato_campo_egreso
        Dim db_fci As New N_Formato_campo_ingreso
        Dim db_fc As New N_Formato_campos
        Dim db_fg As New N_Formato_global

        iden_formato = leerFichero(ruta)
        res = db_f.Insertar(iden_formato)

        For Each ifce As I_formato_campo_egreso In iden_formato.Formato_campo_egreso
            ifce.Id_formato = iden_formato.Id_formato
            db_fce.Insertar(ifce)
        Next

        For Each ifci As I_formato_campo_ingreso In iden_formato.Formato_campo_ingreso
            ifci.Id_formato = iden_formato.Id_formato
            db_fci.Insertar(ifci)
        Next

        For Each ifc As I_formato_campos In iden_formato.Formato_campos
            ifc.Id_formato = iden_formato.Id_formato
            db_fc.Insertar(ifc)
        Next

        iden_formato.Formato_global.Id_formato = iden_formato.Id_formato
        db_fg.Insertar(iden_formato.Formato_global)

        Return res
    End Function

    Public Function Consultar(ByVal id As String) As I_formato
        Dim db As New D_db_operaciones(tabla)
        Dim iden As New I_formato
        Dim res As DataTable

        Dim db_fce As New N_Formato_campo_egreso
        Dim db_fci As New N_Formato_campo_ingreso
        Dim db_fc As New N_Formato_campos
        Dim db_fg As New N_Formato_global

        res = db.Consulta("id_formato", id)

        With iden
            .Id_formato = res.Rows(0).Item(0)
            .Banco = res.Rows(0).Item(1)
            .Algoritmo = res.Rows(0).Item(2)
            .Cadena = res.Rows(0).Item(3)
            .Formato_campos = db_fc.Consultar(id)
            .Formato_campo_egreso = db_fce.Consultar(id)
            .Formato_campo_ingreso = db_fci.Consultar(id)
            .Formato_global = db_fg.Consultar(id)
        End With

        Return iden
    End Function

    Public Function Eliminar(ByVal id As String)
        Dim res As Boolean
        Dim db As New D_db_operaciones(tabla)
        Dim db_fce As New N_Formato_campo_egreso
        Dim db_fci As New N_Formato_campo_ingreso
        Dim db_fc As New N_Formato_campos
        Dim db_fg As New N_Formato_global

        res = db.Eliminar("id_formato", id)

        db_fce.Eliminar(id)
        db_fci.Eliminar(id)
        db_fc.Eliminar(id)
        db_fg.Eliminar(id)

        Return res
    End Function

#End Region

#Region "FUNCIONES PRIVADAS"

    Private Function leerFichero(ByVal fichero As String) As I_formato
        Dim texto As String = ""

        Try
            Dim sr As New StreamReader(fichero)
            texto = sr.ReadToEnd()
            sr.Close()
        Catch ex As Exception
        End Try

        If texto.Length > 0 Then
            Return textToObj(texto)
        Else
            Return Nothing
        End If

    End Function

    Private Function textToObj(ByVal cadena As String) As I_formato
        Try
            Return JsonConvert.DeserializeObject(Of I_formato)(cadena)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

#End Region
End Class
