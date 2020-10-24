Imports System.IO
Imports System.Text
Imports Capa_Identidad
Imports iTextSharp.text.pdf
Imports iTextSharp.text.pdf.parser
Imports Capa_Negocios

Imports Newtonsoft.Json

Public Class Test

    Private Sub BtnBorrar_Click(sender As Object, e As EventArgs) Handles btnBorrar.Click
        txtDetalles.Text = ""
    End Sub

    Public Sub Cargar()
        Try
            Dim Cadena As New StringBuilder
            If File.Exists(txtArchivo.Text) Then
                Dim PdfReader = New PdfReader(txtArchivo.Text)
                Dim page As Integer

                For page = 1 To PdfReader.NumberOfPages
                    Dim currentText = PdfTextExtractor.GetTextFromPage(PdfReader, page, New LocationTextExtractionStrategy())

                    currentText = Encoding.UTF8.GetString(Encoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(currentText)))
                    Cadena.Append(currentText)
                Next
                PdfReader.Close()
            End If
            txtDetalles.Text = Cadena.ToString
        Catch ex As Exception
            MsgBox(ex.ToString)

        End Try

    End Sub

    Private Sub BtnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        Try
            OpenFileDialog1.ShowDialog()
            txtArchivo.Text = OpenFileDialog1.FileName
            If txtArchivo.TextLength > 0 Then
                Cargar()
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub BtnDeserializar_Click(sender As Object, e As EventArgs) Handles btnDeserializar.Click
        Dim jsonTxt As String = "{
    'id_formato': 'F001',
    'cadena': 'Banamex Resuelve',
    'banco': 'CITIBANAMEX',
    'algoritmo': '1',
    'formato_campos': [
        {
            'indice': 1,
            'nombre': 'Fecha'
        },
        {
            'indice': 2,
            'nombre': 'Concepto'
        },
        {
            'indice': 3,
            'nombre': 'Retiros'
        },
        {
            'indice': 4,
            'nombre': 'Depósitos'
        },
        {
            'indice': 5,
            'nombre': 'Saldo'
        }
    ],
    'formato_campo_ingreso': ['PAGO RECIBIDO','CTA.ORDENANTE'],
    'formato_campo_egreso': ['PAGO INTERBANCARIO','PAGO A TERCEROS','PAGO DE SERVICIO'],
    'formato_global': {
        'rfc_ini': 'Registro Federal de Contribuyentes: ',
        'rfc_fin': '\r',
        'fecha_corte_ini': 'SALDO ANTERIOR SALDO AL ',
        'fecha_corte_fin': '\r',
        'saldo_inicial_ini': 'SALDO ANTERIOR  ',
        'saldo_inicial_fin': '\r',
        'algoritmo_nueva_linea': '1',
        'separador_linea': '\r',
        'ignora_parcial_ini': 'AAAA',
        'ignora_parcial_fin': 'BBBB',
        'ignora_total': 'CCCC'
    }
}"


        Dim formato As I_formato = JsonConvert.DeserializeObject(Of I_formato)(jsonTxt)
        'Dim formato As Object = JsonConvert.DeserializeObject(Of Object)(jsonTxt)

        Console.WriteLine(formato.Banco)
        Console.ReadLine()



    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'Dim _d As New I_configuracion("192.168.8.1", "root", "12345", "data", "C:", "D:")
        'Dim cadena As String = JsonConvert.SerializeObject(_d)

        '{"Id":0,"Db_ip":"192.168.8.1","Db_user":"root","Db_password":"12345","Db_name":"data","Folder_in":"C:","Folder_out":"D:"}

        'MsgBox(getCampos(cadena))

        'Dim cn As New N_conexion
        'cn.leerDBinfo()

    End Sub

End Class