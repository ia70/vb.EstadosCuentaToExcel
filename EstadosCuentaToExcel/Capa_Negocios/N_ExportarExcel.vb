Imports Microsoft.Office.Interop
Imports Capa_Identidad

Public Class N_ExportarExcel
#Region "Variables"
    Private Archivo As Excel.Application
    Private Libro As Excel.Workbook
    Private Hoja As Excel.Worksheet
    Private Rango As Excel.Range

    Private _info As I_Archivo
    Private _campos As List(Of I_Campos)
    Private _tabla As DataTable
    Private _ruta As String

#End Region
#Region "PROPIEDADES"
    Public Property Tabla As DataTable
        Get
            Return _tabla
        End Get
        Set(value As DataTable)
            _tabla = value
        End Set
    End Property

    Public Property Ruta As String
        Get
            Return _ruta
        End Get
        Set(value As String)
            _ruta = value
        End Set
    End Property

    Public Property Campos As List(Of I_Campos)
        Get
            Return _campos
        End Get
        Set(value As List(Of I_Campos))
            _campos = value
        End Set
    End Property

    Public Property Info As I_Archivo
        Get
            Return _info
        End Get
        Set(value As I_Archivo)
            _info = value
        End Set
    End Property

#End Region
#Region "Constructor"
    Public Sub New()
    End Sub
    Public Sub New(ByVal datos As DataTable, ByVal campos_ As List(Of I_Campos), ByVal ruta_ As String, ByVal info_ As I_Archivo)
        Tabla = datos
        Ruta = ruta_
        Campos = campos_
        Exportar(Tabla, Campos, _ruta, info_)
    End Sub
#End Region
#Region "Funciones"
    '----- EXPORTACION MAIN ----------------------------------------
    Public Function Exportar(ByVal datos As DataTable, ByVal campos_ As List(Of I_Campos), ByVal ruta_ As String, ByVal info_ As I_Archivo) As Boolean
        Tabla = datos
        Ruta = ruta_
        Campos = campos_
        Info = info_

        ExcelHeader()
        ExcelBody()
        ExcelFormato()
        ExcelGuardar()

        Return True
    End Function

    '----- CABECERA ------------------------------------------------
    Private Function ExcelHeader() As Boolean
        Dim i As Integer = 0
        Dim tipo As String

        Try
            ' Creamos todo lo necesario para un excel
            Archivo = CreateObject("Excel.Application")
            Archivo.Visible = False 'Para que no se muestre mientras se crea
            Libro = Archivo.Workbooks.Add
            Hoja = Libro.ActiveSheet

            'NOMBRE DE COLUMNAS
            Rango = Hoja.Range("a1")
            Rango.EntireRow.Font.Bold = True
            Hoja.Columns.NumberFormat = "@"

            For Each columna As DataColumn In Tabla.Columns
                i += 1
                Hoja.Cells(1, i).Value = columna.ColumnName
                tipo = Campos(i - 1).Tipo
                If tipo = "Decimal" Then
                    Hoja.Columns(i).NumberFormat = "###,###,###,##0.00"
                End If
            Next
            Return True
        Catch ex As Exception
            X(ex)
            Return False
        End Try
    End Function

    '----- CUERPO -------------------------------------------------
    Private Function ExcelBody() As Boolean
        Dim tipo As String
        Dim _X, Y As Integer

        Try

            Y = 2
            For Each Linea As DataRow In Tabla.Rows
                _X = 0
                ' Cargamos la información en el excel

                For Each campo As Object In Linea.ItemArray
                    tipo = Campos(_X).Tipo
                    _X += 1
                    If tipo = "Decimal" Then
                        Try
                            Hoja.Cells(Y, _X).Value = Convert.ToDecimal(campo)
                        Catch ex As Exception
                            Hoja.Cells(Y, _X).Value = ""
                        End Try
                    Else
                        Hoja.Cells(Y, _X).Value = Convert.ToString(campo)
                    End If
                Next
                Y += 1
            Next
            Return True
        Catch ex As Exception
            X(ex)
            Return False
        End Try
    End Function

    '----- FORMATO FINAL -----------------------------------------
    Private Function ExcelFormato() As Boolean
        Dim col, lin As Integer
        col = Tabla.Columns.Count
        lin = Tabla.Rows.Count + 1

        Try
            'ANCHO DE COLUMNAS
            Hoja.Range("A1:" & GetLetraAbc(col) & lin).Columns.AutoFit()
            Hoja.Range("B1:B" & lin).ColumnWidth = 105

            Hoja.ListObjects.AddEx(Excel.XlListObjectSourceType.xlSrcRange, Hoja.Range("A1:" & GetLetraAbc(col) & lin),, Excel.XlYesNoGuess.xlYes)
            Hoja.ListObjects("Tabla1").TableStyle = "TableStyleMedium7"

            Hoja.Range("A1:" & GetLetraAbc(col) & lin).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft
            Hoja.Range("A1:" & GetLetraAbc(col) & lin).VerticalAlignment = Excel.XlHAlign.xlHAlignCenter

            Return True
        Catch ex As Exception
            X(ex)
            Return False
        End Try

    End Function

    'GUARDAR EXCEL
    Private Function ExcelGuardar() As Boolean
        Dim nom As String
        Try
            nom = GetNombreFichero()
            ' Guardamos el excel en la ruta que ha especificado el usuario
            Libro.SaveAs(nom)
            ' Cerramos el workbook
            Archivo.Workbooks.Close()
            ' Eliminamos el objeto excel
            Archivo.Quit()
            Return True
        Catch ex As Exception
            X(ex)
            Return False
        End Try

    End Function

    Private Function GetNombreFichero() As String
        Dim nom As String
        Dim res As String

        Try
            nom = Info.Rfc + " - " + Format(Info.Fecha, "yyyy-MM")
            res = Ruta + "\" + nom
        Catch ex As Exception
            X(ex)
            res = ""
        End Try

        Return res
    End Function

    Private Function GetLetraAbc(ByVal num As Integer) As String
        Dim Letra As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
        Try
            Return Letra(num - 1)
        Catch ex As Exception
            X(ex)
            Return ""
        End Try

    End Function
#End Region

End Class
