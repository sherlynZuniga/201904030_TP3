Public Class Form1
    Private Sub CalcularToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CalcularToolStripMenuItem.Click

        'validando que se hayan ingresado datos:
        If (TextBox1.Text = "") And (TextBox2.Text = "") And (TextBox3.Text = "") And (TextBox4.Text) Then
            MsgBox("FALTAN DATOS DE INGRESO")
            Exit Sub
        End If

        If (ComboBox1.SelectedIndex = -1) And (ComboBox2.SelectedIndex = -1) Then
            MsgBox("FALTA SELECCIONAR DATOS")
        End If

        'verificando que NIT no sea repetido 
        For fila As Integer = 0 To 5
            If (matriz(fila, 0) <> Nothing) Then
                If (matriz(fila, 0) = TextBox1.Text) Then
                    MsgBox("NIT ya registrado")
                End If
            End If
        Next

        'calculos:
        If Index < 6 Then
            'guardando NIT en columna 0
            matriz(index, 0) = TextBox1.Text
            'guardando nombre en columna 1 
            matriz(index, 1) = TextBox2.Text
            'guardando dias hospitalizado en columna 2
            matriz(index, 2) = TextBox3.Text
            'guardando honorarios en columna 3 
            matriz(index, 3) = TextBox4.Text
            'guardando tipo de habitación en columna 4 
            matriz(index, 4) = ComboBox1.SelectedItem
            'guardando tipo de pago en columna 5 
            matriz(index, 5) = ComboBox2.SelectedItem

            'CALCULO PAGO PARCIAL SEGÚN HABITACIÓN*DÍAS en columna 6
            Select Case ComboBox1.SelectedIndex
                Case 0
                    matriz(index, 6) = Val(TextBox3.Text) * 350
                Case 1
                    matriz(index, 6) = Val(TextBox3.Text) * 250
                Case 2
                    matriz(index, 6) = Val(TextBox3.Text) * 150
            End Select

            'CALCULO RECARGO/DESCUENTO en columna 7
            Select Case ComboBox2.SelectedIndex
                Case 0
                    matriz(index, 7) = Val(matriz(index, 6) * -10 / 100)
                Case 1
                    matriz(index, 7) = Val(matriz(index, 6) * -8 / 100)
                Case 2
                    matriz(index, 7) = Val(matriz(index, 6) * -10 / 100)
                Case 3
                    matriz(index, 7) = Val(matriz(index, 6) * 1.5 / 100)
                Case 4
                    matriz(index, 7) = Val(matriz(index, 6) * 0)
            End Select

            'CALCULO TOTAL A PAGAR = PARCIAL +/- RECARGO/DESCUENTO en columna 8
            matriz(index, 8) = Val(matriz(index, 6)) + Val(matriz(index, 7))

            'mensaje de información de datos que se agregarán


            index = index + 1
        Else
            MsgBox("LA MATRIZ ESTÁ LLENA")
        End If

    End Sub

    Private Sub MostrarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MostrarToolStripMenuItem.Click

        'BOTÓN: MOSTRAR
        'Mostrar datos en datagridview
        Dim fila As Integer = 0
        While fila < 6
            DataGridView1.Rows.Add(matriz(fila, 0), matriz(fila, 1), matriz(fila, 2), matriz(fila, 3), matriz(fila, 4), matriz(fila, 5), matriz(fila, 6), matriz(fila, 7), matriz(fila, 8))
            fila = fila + 1

        End While


    End Sub

    Private Sub LimpiarMatrizToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LimpiarMatrizToolStripMenuItem.Click

        'BOTÓN: LIMPIAR MATRIZ

        'limpiar matriz
        For fila As Integer = 0 To 5
            For columna As Integer = 0 To 8
                matriz(fila, columna) = Nothing
            Next
        Next


        'Limpiando datagridview
        DataGridView1.Rows.Clear()



    End Sub

    Private Sub ConsultarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ConsultarToolStripMenuItem.Click

        'BOTÓN: CONSULTA
        'consultar NIT
        If TextBox1.Text = "" Then
            MsgBox("NO SELECCIÓNÓ NIT")
            Exit Sub
        End If

        For fila As Integer = 0 To 5
            If (matriz(fila, 0) <> Nothing) Then
                If (matriz(fila, 0) = TextBox1.Text) Then
                    TextBox2.Text = matriz(fila, 1)
                    TextBox3.Text = matriz(fila, 2)
                    TextBox4.Text = matriz(fila, 3)
                    ComboBox1.Text = matriz(fila, 4)
                    ComboBox2.Text = matriz(fila, 5)
                    Exit Sub
                End If
            End If
        Next



    End Sub

    Private Sub EstadísticasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EstadísticasToolStripMenuItem.Click

        'BOTÓN:ESTADÍSTICAS
        'CALCULO DE ESTADISTICAS

        Dim fila As Integer = 0


        While fila < 6
            If (matriz(fila, 0) <> Nothing) Then

                'No. pacientes que utilizaron la habitación priv
                If matriz(fila, 4) = "privada" And (ComboBox1.SelectedIndex = 0) Then
                    est1 = est1 + 1
                End If

                'Total transferencias
                If matriz(fila, 5) = "transferencia" And (ComboBox2.SelectedIndex = 1) Then
                    est2 = est2 + Val(matriz(fila, 8))
                End If

                'dias utilizando NO privada
                If matriz(fila, 5) = "no privada" And (ComboBox1.SelectedIndex = 2) Then
                    est3 = est3 + Val(matriz(fila, 2))
                End If
            End If
            fila = fila + 1
        End While

        'mostrando valores estadisticas en textboxs
        TextBox5.Text = est1
        TextBox6.Text = est2
        TextBox7.Text = est3

    End Sub

    Private Sub LimpiarEstadísticasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LimpiarEstadísticasToolStripMenuItem.Click

        'BOTÓN:LIMPIAR ESTADISTICAS
        'Limpiar est
        est1 = 0
        est2 = 0
        est3 = 0

        'limpiando textboxs
        TextBox5.Clear()
        TextBox6.Clear()
        TextBox7.Clear()


    End Sub

    Private Sub CerrarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CerrarToolStripMenuItem.Click

        'BOTON:CERRAR
        Form2.Show()


    End Sub
End Class
