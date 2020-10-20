Option Explicit On
Option Strict Off
Imports System.Math
Imports System.Drawing


Public Class graph

    Dim max As Double = 10
    Dim min As Double = max * -1
    Dim plotlength As Double = max - min

    'Dim userequation As String = "x+2"
    Dim graphMargin As Integer = 5
    Dim dataPoints(plotlength) As Point

    Dim pen As New Pen(Color.LightGray, 2)
    Dim axispen As New Pen(Color.Black, 4)
    'this sub plots each point individually as points
    Private Sub ploteq_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'fill the array with data:


        For i As Integer = min To max

            Dim x, y As Double
            x = i
            'function
            y = x ^ 5


            'scale and centre the point coordinates to fit the picture box
            Dim p As Point
            p.X = x * ((GraphBox.Width / plotlength)) + (GraphBox.Width / 2) 'half the box's width and height are added so that the plot starts at 0
            p.Y = (GraphBox.Height / 2 - ((y * GraphBox.Height / plotlength))) '10 is added due to a strange offset

            'add the data point to the array
            dataPoints(i + (plotlength / 2)) = p
        Next
    End Sub
    'this sub draws the graph and the grid
    Private Sub PictureBox1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles GraphBox.Paint



        Dim numberfont As New Font("Arial", GraphBox.Width / (plotlength * 3))
        Dim numberbrush As New SolidBrush(Color.Black)
        'horizontal lines are as long as the width of the box
        Dim linehor As Double = GraphBox.Width
        'and they're divided by plotlength so that it goes over 10 to -10

        'changing y coordinate
        Dim y As Double
        Dim x As Double

        '___________________________________________Y AXIS_______________________________________________
        'this block of code draws the vertical grid lines
        Dim vertlinespacing As Single = GraphBox.Width / plotlength
        Dim v As Integer = 0
        For y = min To max - 1
            Dim yaxisnumbers As Point

            e.Graphics.DrawLine(pen, v, 0, v, GraphBox.Height)
            v = v + vertlinespacing

            yaxisnumbers.X = x - 5 'numbers are drawn a bit to the right so I subtracted
            yaxisnumbers.Y = GraphBox.Height / 2
            e.Graphics.DrawString(y, numberfont, numberbrush, yaxisnumbers) 'Draws the numbers for the x axis

            x = x + vertlinespacing
            'draws the y axis
            If y = -1 Then
                e.Graphics.DrawLine(axispen, v, 0, v, GraphBox.Height)
            End If
        Next
        '___________________________________________HORITZONTAL LINES_______________________________________________
        Dim horlinespacing As Double = GraphBox.Height / plotlength
        Dim h As Integer = 0
        For x = min To max - 1

            e.Graphics.DrawLine(pen, 0, h, GraphBox.Width, h)
            'draws the numbers for the y axis
            Dim xaxisnumbers As Point
            xaxisnumbers.X = GraphBox.Width / 2
            xaxisnumbers.Y = h - 5 'numbers are drawn a bit down so I subtracted
            If x > 0 Then
                e.Graphics.DrawString(0 - x, numberfont, numberbrush, xaxisnumbers)
            Else
                e.Graphics.DrawString(0 - x, numberfont, numberbrush, xaxisnumbers)
            End If

            h = h + horlinespacing

            If x = -1 Then
                e.Graphics.DrawLine(axispen, 0, h, GraphBox.Width, h)

            End If

        Next
        '___________________________________________LINE DRAWING_______________________________________________
        e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
        e.Graphics.DrawLines(Pens.Red, dataPoints)



    End Sub
    Public Sub graph_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        GraphBox.Width = Me.Width * 0.923
        GraphBox.Height = Me.Height * 0.923

        GraphBox.Invalidate()
        Me.Invalidate()
        GraphBox.CreateGraphics()


    End Sub

    Public Class equation
        Function parse(ByRef equation As String)


            Dim s As String = equation 'our equation
            s = s.Replace(" ", "") 'remove spaces
            Dim iTemp As Double = 0 'double (in case decimal) for our calculations
            For i As Integer = 0 To s.Length - 1 'standard loop
                If IsNumeric(s(i)) Then 'if it is an ascii number
                    iTemp = iTemp & (Convert.ToInt32(s(i + 1)) - 48) 'offset by 48 and get real number
                Else

                    Select Case s(i)
                        Case "+" 's(i+1) looks 1 index ahead
                            iTemp = iTemp + (Convert.ToInt32(s(i + 1))) 'solution
                            If (s(i + 1)) = "-" Then
                                i = i + 1
                                iTemp = iTemp + (Convert.ToInt32(s(i + 1)) * -1)
                            End If

                        Case "-"
                            iTemp = iTemp - (Convert.ToInt32(s(i + 1))) 'solution
                            If (s(i + 1)) = "-" Then
                                i = i + 1
                                iTemp = iTemp - (Convert.ToInt32(s(i + 1)) * -1)
                            End If

                        Case "*"
                            iTemp = iTemp * (Convert.ToInt32(s(i + 1))) 'solution
                            If (s(i + 1)) = "-" Then
                                i = i + 1
                                iTemp = iTemp * (Convert.ToInt32(s(i + 1)) * -1)
                            End If

                        Case "/"
                            'you should check for zero since x/0 = undefined
                            iTemp = iTemp / (Convert.ToInt32(s(i + 1))) 'solution
                            If (s(i + 1)) = "-" Then
                                i = i + 1
                                iTemp = iTemp / (Convert.ToInt32(s(i + 1)) * -1)
                            End If

                        Case "^"
                            'you should check for zero since x/0 = undefined
                            iTemp = iTemp ^ (Convert.ToInt32(s(i + 1))) 'solution
                            If (s(i + 1)) = "-" Then
                                i = i + 1
                                iTemp = iTemp ^ (Convert.ToInt32(s(i + 1)) * -1)
                            End If
                    End Select
                    Exit For
                End If
            Next
            Return iTemp

        End Function
        Function substituteequation(ByRef equation As String, ByVal x As Double)
            For i As Integer = 0 To equation.Length - 1 'standard loop
                equation = equation.Replace("x", ("1*" & x)) 'each occurence of x is replaced with our value
            Next


            Return equation



        End Function
    End Class


End Class