Imports System.Windows.Forms.DataVisualization.Charting

Public Class Form1


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Chart1.Series.Clear()

        Dim plot1 As New Series
        Dim plot2 As New Series
        Dim plot3 As New Series
        Dim R As Double
        Dim Y As Double
        Dim X As Double

        plot1.ChartType = SeriesChartType.Spline
        plot2.ChartType = SeriesChartType.Line
        plot3.ChartType = SeriesChartType.Line
        plot1.Color = Color.Blue
        plot2.Color = Color.Blue
        plot3.Color = Color.Blue

        Try
            R = Convert.ToDouble(TextBox1.Text)
        Catch
            MessageBox.Show("Ошибка ввода")
        End Try
        'полукруг
        For j As Integer = -R To R
            Y = j
            X = Math.Sqrt(R * R - Y * Y)
            plot1.Points.AddXY(-X, Y)

        Next

        'верхния наклонная
        For j As Integer = 0 To R
            Y = j
            X = R - Y
            plot2.Points.AddXY(X, Y)
        Next


        'нижния наклонная
        For j As Integer = 0 To R

            X = j
            Y = X - R

            plot2.Points.AddXY(X, Y)
        Next

        Chart1.Series.Add(plot1)
        Chart1.Series.Add(plot2)
        Chart1.Series.Add(plot3)


    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim plot4 As New Series
        Dim plot5 As New Series
        Dim T_vn As Integer
        Dim T_za As Integer
        Dim n As Integer
        Dim x, y, S_fig, S_r, er, R As Double
        Dim var As Integer


        plot4.ChartType = SeriesChartType.Point
        plot5.ChartType = SeriesChartType.Point

        plot4.Color = Color.Green
        plot5.Color = Color.Red

        plot4.Points.Clear()
        plot5.Points.Clear()
        Try
            R = Convert.ToDouble(TextBox1.Text)
            'запрашиваем количество точек
            n = Convert.ToInt32(TextBox2.Text)
            'очищаем счётчик точек
            T_vn = T_za = 0
            For i As Integer = 1 To n
                Randomize()
                x = Rnd() Mod CInt((2 * R + 1) - R)
                y = Rnd() Mod CInt((2 * R + 1) - R)

                var = CInt(Math.Floor((4 - 1 + 1) * Rnd())) + 1
                If (var = 1) Then
                    x *= R
                    y *= R
                ElseIf (var = 2) Then
                    x *= -R
                    y *= R
                ElseIf (var = 3) Then
                    x *= -R
                    y *= -R
                Else
                    x *= R
                    y *= -R
                End If

               
                '    top line         bot line           circle
                If ((x <= R - y) And (y >= x - R) And (x * x <= R * R - y * y)) Then
                    plot4.Points.AddXY(x, y)
                    T_vn += 1
                Else
                    plot5.Points.AddXY(x, y)
                    T_za += 1
                End If
            Next
            'количество точек внутри
            TextBox3.Text = Convert.ToString(T_vn)
            'количество вне
            TextBox4.Text = Convert.ToString(T_za)
            'определение площади фигуры по методу Монте Карло
            S_fig = 4 * R * R * T_vn / n
            'площадь реальная
            S_r = R * R + Math.PI * R * R / 2
            'погрешность
            er = Math.Abs(S_r - S_fig) / S_r * R
            TextBox5.Text = Convert.ToString(S_fig)
            TextBox6.Text = Convert.ToString(er)
            Chart1.Series.Add(plot4)
            Chart1.Series.Add(plot5)
        Catch ex As Exception
            MessageBox.Show("Введите количество точек", "Ошибка ввода данных", MessageBoxButtons.OK ,MessageBoxIcon.Exclamation)
        End Try



    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Close()
    End Sub
End Class
