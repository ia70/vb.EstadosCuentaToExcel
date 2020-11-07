Imports Microsoft.Office.Interop
Imports System.Windows.Forms
Imports Capa_Datos

Public Class N_ExportarExcel
#Region "Variables"
    Private Archivo As Excel.Application
    Private Libro As Excel.Workbook
    Private Hoja As Excel.Worksheet
    Private Rango As Excel.Range
    Private indice As Integer = 2
    Private Total As Integer
    Private db_Transaccion As DataTable

#End Region
#Region "Constructor"
    Public Sub New(ByVal Transacciones As DataTable)
        db_Transaccion = Transacciones
        ExportarExcel()
    End Sub
#End Region
#Region "Funciones"
    '----- EXPORTACION MAIN ----------------------------------------
    Private Sub ExportarExcel()
        ExcelHeader()
        ExcelBody()
        ExcelFormato()
        ExcelGuardar()
    End Sub

    '----- CABECERA ------------------------------------------------
    Private Sub ExcelHeader()
        Dim i As Integer = 1

        Try
            ' Creamos todo lo necesario para un excel
            Archivo = CreateObject("Excel.Application")
            Archivo.Visible = False 'Para que no se muestre mientras se crea
            Libro = Archivo.Workbooks.Add
            Hoja = Libro.ActiveSheet

            'NOMBRE DE COLUMNAS
            Rango = Hoja.Range("a1")
            Rango.EntireRow.Font.Bold = True
            Hoja.Cells(i, 1).Value = "No."
            Hoja.Cells(i, 2).Value = "Clave de rastreo"
            Hoja.Cells(i, 3).Value = "Nombre de Banco Origen"
            Hoja.Cells(i, 4).Value = "Nombre de la Cuenta Origen"
            Hoja.Cells(i, 5).Value = "RFC Origen"
            Hoja.Cells(i, 6).Value = "Número de Cuenta Origen"
            Hoja.Cells(i, 7).Value = "Nombre de Banco destino"
            Hoja.Cells(i, 8).Value = "Nombre de la Cuenta Destino"
            Hoja.Cells(i, 9).Value = "RFC Destino"
            Hoja.Cells(i, 10).Value = "Número de Cuenta Destino"
            Hoja.Cells(i, 11).Value = "No. Registros Transmitidos"
            Hoja.Cells(i, 12).Value = "Concepto de Pago"
            Hoja.Cells(i, 13).Value = "Beneficiario"
            Hoja.Cells(i, 14).Value = "Referencia"
            Hoja.Cells(i, 15).Value = "Folio de Internet"
            Hoja.Cells(i, 16).Value = "Importe"
            Hoja.Cells(i, 17).Value = "Moneda"
            Hoja.Cells(i, 18).Value = "Fecha de Aplicación "
            Hoja.Cells(i, 19).Value = "No. Empleado"
            Hoja.Cells(i, 20).Value = "Nombre"
            Hoja.Cells(i, 21).Value = "Tipo Cuenta"
            Hoja.Cells(i, 22).Value = "Número de Cuenta"
            Hoja.Cells(i, 23).Value = "Importe"
            Hoja.Cells(i, 24).Value = "Descripción"


            Hoja.Columns.NumberFormat = "@"
            Hoja.Columns("A").NumberFormat = "0"
            Hoja.Columns("P").NumberFormat = "#,###,###.00"
            Hoja.Columns("W").NumberFormat = "#,###,###.00"

            Total = db_Transaccion.Rows.Count + 1
        Catch ex As Exception

        End Try
    End Sub

    '----- FORMATO FINAL -----------------------------------------
    Private Sub ExcelFormato()
        Try
            'ANCHO DE COLUMNAS
            Hoja.Range("A1:X" & Total.ToString).Columns.AutoFit()

            Hoja.ListObjects.AddEx(Excel.XlListObjectSourceType.xlSrcRange, Hoja.Range("A1:X" & Total.ToString),, Excel.XlYesNoGuess.xlYes)
            Hoja.ListObjects("Tabla1").TableStyle = "TableStyleMedium7"

            Hoja.Range("A1:X" & Total.ToString).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft
        Catch ex As Exception

        End Try

    End Sub

    '----- CUERPO -------------------------------------------------
    Private Sub ExcelBody()
        Dim Registros As Integer
        Dim Tabla As DataTable
        Dim LineaY As Integer

        Try

            indice = 1
            LineaY = 2
            For Each Linea As DataRow In db_Transaccion.Rows
                ' Cargamos la información en el excel
                Hoja.Cells(LineaY, 1).Value = (indice)
                Hoja.Cells(LineaY, 2).Value = Linea(2)
                Hoja.Cells(LineaY, 3).Value = Linea(3)
                Hoja.Cells(LineaY, 4).Value = Linea(4)
                Hoja.Cells(LineaY, 5).Value = Linea(5)
                Hoja.Cells(LineaY, 6).Value = Linea(6)
                Hoja.Cells(LineaY, 7).Value = Linea(7)
                Hoja.Cells(LineaY, 8).Value = Linea(8)
                Hoja.Cells(LineaY, 9).Value = Linea(9)
                Hoja.Cells(LineaY, 10).Value = Linea(10)
                Hoja.Cells(LineaY, 11).Value = Linea(11)
                Hoja.Cells(LineaY, 12).Value = Linea(12)
                Hoja.Cells(LineaY, 13).Value = Linea(13)
                Hoja.Cells(LineaY, 14).Value = Linea(14)
                Hoja.Cells(LineaY, 15).Value = Linea(15)
                Hoja.Cells(LineaY, 16).Value = Linea(16)
                Hoja.Cells(LineaY, 17).Value = Linea(17)
                Hoja.Cells(LineaY, 18).Value = Linea(18)


                Registros = Val(Hoja.Cells(LineaY, 11).Value)

                If Registros > 0 Then
                    Total += Registros
                    Tabla = GetEmpleados(Hoja.Cells(LineaY, 2).Value)

                    For Each Lin As DataRow In Tabla.Rows
                        LineaY += 1

                        Hoja.Cells(LineaY, 2).Value = Linea(2)

                        Hoja.Cells(LineaY, 19).Value = Lin(0)
                        Hoja.Cells(LineaY, 20).Value = Lin(1)
                        Hoja.Cells(LineaY, 21).Value = Lin(2)
                        Hoja.Cells(LineaY, 22).Value = Lin(3)
                        Hoja.Cells(LineaY, 23).Value = Lin(4)
                        Hoja.Cells(LineaY, 24).Value = Lin(5)

                    Next
                End If
                LineaY += 1
                indice += 1
            Next
        Catch ex As Exception

        End Try
    End Sub

    'OBTENER EMPLEADOS
    Private Function GetEmpleados(ByVal ID As String) As DataTable
        Dim _Empleados As New N_Empleados
        Dim Tabla As DataTable

        Tabla = _Empleados.Consultar(ID)

        Return Tabla
    End Function

    'GUARDAR EXCEL
    Private Sub ExcelGuardar()
        Dim DB As New d_Transaccion
        Try
            ' Mostramos un dialog para que el usuario indique donde quiere guardar el excel
            Dim saveFileDialog1 As New SaveFileDialog()
            saveFileDialog1.Title = "Guardar documento Excel"
            saveFileDialog1.Filter = "Excel File|*.xlsx"
            saveFileDialog1.FileName = "Reporte " & Format(Today.Date, "yyyy-MM-dd")
            saveFileDialog1.ShowDialog()
            ' Guardamos el excel en la ruta que ha especificado el usuario
            Libro.SaveAs(saveFileDialog1.FileName)
            ' Cerramos el workbook
            Archivo.Workbooks.Close()
            ' Eliminamos el objeto excel
            Archivo.Quit()
            MsgBox("Reporte guardado!", vbInformation, "Bancos")

            'ELIMINACION DE TRANSACCIONES
            For Each Linea As DataRow In db_Transaccion.Rows
                DB.Eliminar(Linea.Item(0))
            Next

        Catch ex As Exception
            MsgBox("Error al exportar los datos a excel: " & ex.ToString, vbCritical, "Bancos")
        End Try

    End Sub
#End Region

End Class
