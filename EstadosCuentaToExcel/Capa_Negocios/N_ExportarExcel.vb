Imports Microsoft.Office.Interop
Imports Capa_Identidad

Public Class N_ExportarExcel
#Region "Variables"
    Private Archivo As Excel.Application
    Private Libro As Excel.Workbook
    Private Hoja As Excel.Worksheet
    Private Rango As Excel.Range

    'Private indice As Integer = 2
    'Private Total As Integer
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

#End Region
#Region "Constructor"
    Public Sub New()
    End Sub
    Public Sub New(ByVal datos As DataTable, ByVal ruta_ As String)
        Tabla = datos
        Ruta = ruta_
        Exportar(Tabla, _ruta)
    End Sub
#End Region
#Region "Funciones"
    '----- EXPORTACION MAIN ----------------------------------------
    Public Function Exportar(ByVal datos As DataTable, ByVal ruta_ As String) As Boolean
        Tabla = datos
        Ruta = ruta_

        ExcelHeader()
        ExcelBody()
        ExcelFormato()
        ExcelGuardar()

        Return True
    End Function

    '----- CABECERA ------------------------------------------------
    Private Sub ExcelHeader()
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
                tipo = columna.DataType.ToString
                If tipo = "System.Decimal" Then
                    Hoja.Columns(i).NumberFormat = "#,###,###.00"
                End If
            Next

        Catch ex As Exception

        End Try
    End Sub

    '----- CUERPO -------------------------------------------------
    Private Sub ExcelBody()
        Dim X, Y As Integer

        Try

            Y = 2
            For Each Linea As DataRow In Tabla.Rows
                X = 1
                ' Cargamos la información en el excel

                For Each campo As String In Linea.ItemArray
                    Hoja.Cells(Y, X).Value = campo
                Next
                Y += 1
            Next
        Catch ex As Exception

        End Try
    End Sub

    '----- FORMATO FINAL -----------------------------------------
    Private Sub ExcelFormato()
        Try
            'ANCHO DE COLUMNAS
            Hoja.Range("A1:X" & Tabla.Rows.Count.ToString).Columns.AutoFit()

            Hoja.ListObjects.AddEx(Excel.XlListObjectSourceType.xlSrcRange, Hoja.Range("A1:X" & Tabla.Rows.Count.ToString),, Excel.XlYesNoGuess.xlYes)
            Hoja.ListObjects("Tabla1").TableStyle = "TableStyleMedium7"

            Hoja.Range("A1:X" & Tabla.Rows.Count.ToString).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft
        Catch ex As Exception

        End Try

    End Sub

    'GUARDAR EXCEL
    Private Sub ExcelGuardar()
        Try
            ' Guardamos el excel en la ruta que ha especificado el usuario
            Libro.SaveAs(Ruta)
            ' Cerramos el workbook
            Archivo.Workbooks.Close()
            ' Eliminamos el objeto excel
            Archivo.Quit()
        Catch ex As Exception
            MsgBox("Error al exportar los datos a excel: " & ex.ToString, vbCritical, "Bancos")
        End Try

    End Sub
#End Region

End Class
