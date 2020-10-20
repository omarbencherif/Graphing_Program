Option Explicit On
Option Strict Off
Imports System.Math
Imports System.Drawing
Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary




Public Class graph
    Private _max As Decimal = 100
    Private _min As Decimal = _max * -1
    Private _plotlength As Decimal = (_max - _min) * 10
    Private _numberrange As Decimal = _max - _min
    Private dataPoints(_plotlength) As PointF
    Private xyvalues(_plotlength) As PointF
    Private solutionslist As List(Of Decimal) = New List(Of Decimal)
    Dim vertlinespacing As Decimal
    Dim horlinespacing As Decimal
    'Local variables for the class ‘graph’ are defined. The maximum value is the largest value (of x or y) that the graph box will plot a graph up to. From this, the minimum value, other variables can be calculated. The minimum is the lowest negative value the graph will go up to. The plotlength is the amount of points which will be calculated by the program in order to produce a more accurate shape for the function. The numberrange is the range of integer numbers which will be plotted by the graph. 
    'These variables are in use for the drawing procedures in the code. Vertlinespacing and horlinespacing are the spacing between the vertical and horizontal grid lines of the graph. This is made so that the spacing inbetween the lines are equal and represents each unit of the scale equally.
    Dim linepen As New Pen(Color.LightGray, 1)
    Dim graphpen As New Pen(Color.Blue, _max / 100)
    Dim axispen As New Pen(Color.Black, 4)
    Dim numberfont As Font
    Dim numberbrush As New SolidBrush(Color.Black)



    'A point and a coordinate structure are created so that the program  can keep both the x and y coordinates of a point associated by use of the structure. 
    Structure coordinate
        Public realposition As PointF
        Public screenposition As Point
    End Structure
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        _max = 10
        _min = _max * -1
        _plotlength = (_max - _min) / 0.1
        _numberrange = _max - _min
        plot(_max)
    End Sub
    Private Sub plot(max As Decimal)
        numberfont = New Font("Arial", GraphBox.Width / (_max * 5))
        vertlinespacing = GraphBox.Width / _numberrange
        horlinespacing = GraphBox.Height / _numberrange

        If Decimal.Parse(FromBox.Text) = 0 Then
            FromBox.Text = "10"
            Header.Text = "Invalid input. Please try again."
            Header.ForeColor = Color.Red
        End If

        cleantable(TableOfValues)
        'clears the table so that values can be added for a new function
        If Not IsParametric.Checked Then
            Dim singleequation As Object = New Parser(yequationbox.Text, "x")
            xequationbox.Enabled = False
            MakePoints(singleequation) 'the program will make points
            DifferenceQuotient.Text = Math.Round(FiniteDifferenceApproximation(xyvalues(Convert.ToDecimal(xforgradient.Text) + (_plotlength / 2)), xyvalues(Convert.ToDecimal(xforgradient.Text) + 1 + (_plotlength / 2)), singleequation), 2)
        Else 'the program decides whether to plot a parametric function or not

            Dim xeq As Object = New Parser(yequationbox.Text, "z")
            Dim yeq As Object = New Parser(xequationbox.Text, "z")
            xequationbox.Enabled = True
            MakePoints(yeq, xeq)

        End If


        'If Decimal.Parse(maxlimitbox.Text) < Decimal.Parse(minlimitbox.Text) Then 'ensures that limits don't give a negative answer
        '    swap(maxlimitbox.Text, minlimitbox.Text)
        'End If
        If maxlimitbox.Text <> 0 Then
            AreaLabel.Text = trapeziumrule(xyvalues, _plotlength / 2 + (Decimal.Parse(minlimitbox.Text) * 10), (_plotlength / 2 + (Decimal.Parse(maxlimitbox.Text) * 10)), 0.1)
        End If

        'The plot subroutine is used to plot the graph. It first calculates the line spacing by dividing the width of the graphbox by the number of points the program wants to plot. After this, it uses the CalculateCoordinates subroutine to create a table of values for the graph, calculating all of the graph’s points by using its function. It then draws all of the features of the graph so that  it produces the desired interface as previously shown. The algorithms used for drawing these are later discussed.
    End Sub

    'the serialisation of the variables required for the program.
    Private Sub SaveButton_Click(sender As Object, e As EventArgs) Handles SaveButton.Click
        Dim savefiledialog1 As New SaveFileDialog
        savefiledialog1.FileName = "data.dat"
        If savefiledialog1.ShowDialog() = DialogResult.OK Then
            Dim filename As String = savefiledialog1.FileName ' allows user to choose a filename
            Dim format As New BinaryFormatter
            Using mystream As New FileStream(filename, FileMode.Create)
                format.Serialize(mystream, yequationbox.Text)
                format.Serialize(mystream, xequationbox.Text)
                format.Serialize(mystream, IsParametric.Checked)
                format.Serialize(mystream, maxlimitbox.Text)
                format.Serialize(mystream, minlimitbox.Text)
                format.Serialize(mystream, FromBox.Text)
                format.Serialize(mystream, xforgradient.Text)
            End Using
        End If
    End Sub 'Saves and loads the equation plotted by the user so that the user can plot the graph if needed in the future.
    '
    'the deserialisation of the variables required for the program.
    Private Sub LoadButton_Click(sender As Object, e As EventArgs) Handles LoadButton.Click
        Dim openfiledialog1 As New OpenFileDialog
        openfiledialog1.FileName = "data.dat"
        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
            Dim filename As String = openfiledialog1.FileName ' user enters the filename
            Dim format As New BinaryFormatter
            Using mystream As New FileStream(filename, FileMode.Open)
                yequationbox.Text = format.Deserialize(mystream) 'deserializes the file user specified
                xequationbox.Text = format.Deserialize(mystream)
                IsParametric.Checked = format.Deserialize(mystream)
                maxlimitbox.Text = format.Deserialize(mystream)
                minlimitbox.Text = format.Deserialize(mystream)
                FromBox.Text = format.Deserialize(mystream)
                xforgradient.Text = format.Deserialize(mystream)

            End Using
        End If
        plot(_max)
    End Sub
    Private Sub cleantable(Table As DataGridView)
        Table.Rows.Clear()
        Table.Columns.Clear()
    End Sub

    Private Sub MakePoints(equation As Parser)

        For x As Decimal = _min To _max Step 0.1 'step 0.1 for more accurate shape, plotting 200 points
            Dim y As Decimal

            'function of x in use
            Try
                y = equation.Evaluate(x)
            Catch ex As OverflowException
                Header.ForeColor = Color.Red
                Header.Text = "Overflow Error - Invalid Input. Please try again or enter a different function."
                Exit For

            End Try

            'scale and centre the point coordinates to fit the picture box
            Dim p As coordinate
            Try
                p.screenposition.X = 10 * x * ((GraphBox.Width / _plotlength)) + (GraphBox.Width / 2) 'half the box's width and height are added so that the plot starts at 0
                p.screenposition.Y = (GraphBox.Height / 2 - ((y * 10 * GraphBox.Height / _plotlength))) '10 is added due to a strange offset\
            Catch ex As OverflowException
                Header.Text = "Invalid input - Overflow Error"
                Exit For

            End Try


            p.realposition.X = x
            p.realposition.Y = y

            Me.FillTableofValues(Nothing, Nothing)
            Dim row As String() = New String() {x, y}
            TableOfValues.Rows.Add(row)


            Dim arraylocation As Decimal = (x * 10 + (_plotlength * 0.5))
            'add the data point to the array
            'x is multiplied by 10 so that the many decimal coordinate values are assigned a place in the array
            Try
                dataPoints(arraylocation) = p.screenposition
                xyvalues(arraylocation) = p.realposition
            Catch ex As Exception
                ReDim dataPoints(_plotlength)
                ReDim xyvalues(_plotlength)
                dataPoints(arraylocation) = p.screenposition
                xyvalues(arraylocation) = p.realposition
            End Try


            If p.realposition.Y = 0 Then
                solutionslist.Add(p.realposition.X)
            ElseIf p.screenposition.Y = GraphBox.Height / 2 Then
                solutionslist.Add(Math.Round(screentogrid(p.screenposition.X), 2))
            End If
        Next
    End Sub
    Private Sub MakePoints(yequation As Parser, xequation As Parser)


        For t As Decimal = _min To _max Step 0.1 'step 0.1 for more accurate shape, plotting 200 points
            Dim y As Decimal
            Dim x As Decimal
            'function of x in use

            y = yequation.Evaluate(t)
            x = xequation.Evaluate(t)

            'scale and centre the point coordinates to fit the picture box
            Dim p As coordinate

            p.screenposition.X = 10 * x * ((GraphBox.Width / _plotlength)) + (GraphBox.Width / 2) 'half the box's width and height are added so that the plot starts at 0
            p.screenposition.Y = (GraphBox.Height / 2 - ((y * 10 * GraphBox.Height / _plotlength))) '10 is added due to a strange offset\

            p.realposition.X = x
            p.realposition.Y = y

            Me.FillTableofValues(Nothing, Nothing) 'the table of values is filled with these calulated points so that the user can see them
            Dim row As String() = New String() {x, y}
            TableOfValues.Rows.Add(row)


            Dim arraylocation As Decimal = (t * 10 + (_plotlength * 0.5))
            'add the data point to the array
            'x is multiplied by 10 so that the many decimal coordinate values are assigned a place in the array
            dataPoints(arraylocation) = p.screenposition
            xyvalues(arraylocation) = p.realposition

            If p.realposition.Y = 0 Then 'the solutionslist contains all of the solutions where the program determines that the x coordinate is 0 and the point is therefore a solution
                solutionslist.Add(p.realposition.X)
            ElseIf p.screenposition.Y = GraphBox.Height / 2 Then
                solutionslist.Add(Math.Round(screentogrid(p.screenposition.X), 2))
            End If
        Next

    End Sub

    'this sub plots each point individually as points
    'this sub draws the graph and the grid

    Private Sub Xdrawaxisnumbers(ByVal sender As Object, ByVal e As PaintEventArgs) Handles GraphBox.Paint
        Dim ycoord As Decimal
        Dim xcoord As Decimal

        For ycoord = _min To _max - 1
            Dim X_Axis_Number As Point
            X_Axis_Number.X = xcoord - 5 'numbers are drawn a bit to the right so I subtracted
            X_Axis_Number.Y = GraphBox.Height / 2
            e.Graphics.DrawString(ycoord, numberfont, numberbrush, X_Axis_Number) 'Draws the numbers for the x axis
            xcoord = xcoord + vertlinespacing
        Next
    End Sub
    Private Sub Ydrawaxisnumbers(ByVal sender As Object, ByVal e As PaintEventArgs) Handles GraphBox.Paint
        Dim h As Decimal = 0
        For xcoord = _min To _max - 1
            'draws the numbers for the y axis
            Dim Y_Axis_Number As Point
            Y_Axis_Number.X = GraphBox.Width / 2
            Y_Axis_Number.Y = h - 5 'numbers are drawn a bit down so I subtracted
            If xcoord > 0 Then
                e.Graphics.DrawString(0 - xcoord, numberfont, numberbrush, Y_Axis_Number)
            Else
                e.Graphics.DrawString(0 - xcoord, numberfont, numberbrush, Y_Axis_Number)
            End If

            h = h + horlinespacing
        Next

    End Sub
    Private Sub _drawverticallines(ByVal sender As Object, ByVal e As PaintEventArgs) Handles GraphBox.Paint
        Dim linepen As New Pen(Color.LightGray, 2)
        Dim axispen As New Pen(Color.Black, 4)
        'horizontal lines are as long as the width of the box
        'and they're divided by plotlength so that it goes over 10 to -10
        'changing y coordinate
        Dim ycoord As Decimal
        Dim xcoord As Decimal

        Dim v As Decimal = 0
        For ycoord = _min To _max - 1
            e.Graphics.DrawLine(linepen, v, 0, v, GraphBox.Height)
            v = v + vertlinespacing


            xcoord = xcoord + vertlinespacing
            'draws the y axis
            If ycoord = -1 Then
                e.Graphics.DrawLine(axispen, v, 0, v, GraphBox.Height)
            End If
        Next
    End Sub
    Private Sub _Drawfunction(ByVal sender As Object, ByVal e As PaintEventArgs) Handles GraphBox.Paint

        e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
        'step 10 so that every integer value has its own point
        Dim _pointsize As Single = 5
        Dim midpointoffset As Decimal = _plotlength / 2
        Dim _pointoffset As Single = _pointsize / 2
        Dim _pointbrush As New SolidBrush(Color.Black)
        For x = 0 To dataPoints.Length Step _plotlength / 20
            'draws an ellipse for integer points on the graph 
            e.Graphics.FillEllipse(_pointbrush, dataPoints(x).X - _pointoffset, dataPoints(x).Y - _pointoffset, _pointsize, _pointsize)
        Next
        For i = 0 To _plotlength - 1
            e.Graphics.DrawLine(graphpen, dataPoints(i), dataPoints(i + 1)) 'draws lines from point to point
        Next
        GraphBox.Invalidate()
    End Sub
    'Private Sub _Drawtangent(ByVal sender As Object, ByVal e As PaintEventArgs) Handles GraphBox.Paint

    '    Dim singleequation As Object = New Parser(yequationbox.Text, "x")
    '    Dim gradient As Decimal = FiniteDifferenceApproximation(xyvalues(Convert.ToDecimal(xforgradient.Text) + (_plotlength / 2)), xyvalues(Convert.ToDecimal(xforgradient.Text) + 1 + (_plotlength / 2)), singleequation)
    '    Dim point1 As Point
    '    Dim pointx As Point
    '    point1.X = xforgradient.Text
    '    pointx.Y = singleequation.evaluate(point1.X + 5)
    '    pointx.X = point1.X + 5
    '    point1.Y = singleequation.Evaluate(point1.X)
    '    pointx.Y = gradient * (pointx.X - point1.X) + point1.Y

    '    e.Graphics.DrawLine(Pens.Black, point1, pointx)
    'End Sub
    Private Function screentogrid(coordinate As Decimal)
        Return (_plotlength * (coordinate - 0.5 * GraphBox.Height) / (GraphBox.Height * 10))
    End Function
    Private Sub _drawhorizontallines(ByVal sender As Object, ByVal e As PaintEventArgs) Handles GraphBox.Paint
        Dim h As Decimal = 0
        For xcoord = _min To _max - 1

            e.Graphics.DrawLine(linepen, 0, h, GraphBox.Width, h)
            'draws the numbers for the y axis
            Dim axisnumber_Y As Point
            axisnumber_Y.X = GraphBox.Width / 2
            axisnumber_Y.Y = h - 5 'numbers are drawn a bit down so I subtracted
            If xcoord > 0 Then
                e.Graphics.DrawString(0 - xcoord, numberfont, numberbrush, axisnumber_Y)
            Else
                e.Graphics.DrawString(0 - xcoord, numberfont, numberbrush, axisnumber_Y)
            End If

            h += horlinespacing

            If xcoord = -1 Then
                e.Graphics.DrawLine(axispen, 0, h, GraphBox.Width, h)
            End If

        Next
    End Sub
    Private Sub shadeareaundergraph(ByVal sender As Object, ByVal e As PaintEventArgs) Handles GraphBox.Paint
        For p = Single.Parse(minlimitbox.Text) * 10 + _plotlength / 2 To Single.Parse(maxlimitbox.Text) * 10 + _plotlength / 2
            e.Graphics.DrawLine(Pens.Red, dataPoints(p).X, dataPoints(p).Y, dataPoints(p).X, Single.Parse(GraphBox.Width / 2))
        Next
    End Sub 'the area under the graph is shaded to give the user a visual aid of the defnite integral

    Private Sub FillTableofValues(sender As Object, e As PaintEventArgs) Handles TableOfValues.Paint
        TableOfValues.ColumnCount = 2
        TableOfValues.Columns(0).Name = "x"
        TableOfValues.Columns(1).Name = "y"

    End Sub

    Private Function trapeziumrule(ByVal pointlist() As PointF, ByVal y0 As Decimal, ByVal yn As Decimal, ByVal h As Decimal)
        Dim multipleloop As Decimal
        For i = y0 + 1 To yn - 1
            multipleloop = multipleloop + Sqrt((pointlist(i).Y) ^ 2)
        Next

        Return 0.5 * h * ((pointlist(y0).Y + pointlist(yn).Y) + (2 * (multipleloop)))
    End Function 'the trapezium rule used to calculate the definite integral 
    Private Sub swap(ByRef a, ByRef b)
        Dim c
        c = a
        a = b
        b = c
    End Sub
    Private Function FiniteDifferenceApproximation(ByVal point1 As PointF, ByVal point2 As PointF, ByVal f As Parser)
        Dim h As Decimal = point2.X - point1.X

        Return (f.Evaluate(point2.X) - f.Evaluate(point1.X)) / h
    End Function 'used to approximate the gradient between two points


    Private Sub Zoom_GraphBox_MouseWheel(sender As Object, e As MouseEventArgs) Handles GraphBox.MouseWheel

        If e.Delta < 0 Then
            _max += 5
            _min = _max * -1
            _plotlength = (_max - _min) * 10
            _numberrange = _max - _min
            ReDim dataPoints(_plotlength)
            ReDim xyvalues(_plotlength)
            plot(_max)
            GraphBox.Refresh()

        ElseIf e.Delta > 0 And _max > 5 Then
            _max -= 5
            _min = _max * -1
            _plotlength = (_max - _min) * 10
            _numberrange = _max - _min
            ReDim dataPoints(_plotlength)
            ReDim xyvalues(_plotlength)
            plot(_max)
            GraphBox.Refresh()
        End If


    End Sub
    'zooms the graph depending on how much the user scrolls the mousewheel

    Private Sub FromBox_TextChanged(sender As Object, e As EventArgs) Handles FromBox.TextChanged
        Try
            ToBox.Text = -1 * Decimal.Parse(FromBox.Text)
        Catch ex As Exception
            ToBox.Text = 0
        End Try

    End Sub

    Private Sub ToBox_TextChanged(sender As Object, e As EventArgs) Handles ToBox.TextChanged
        Try
            FromBox.Text = -1 * Decimal.Parse(ToBox.Text)
        Catch ex As Exception
            FromBox.Text = 0
        End Try

    End Sub

    Private Sub FromBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles FromBox.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If

    End Sub

    Private Sub ToBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ToBox.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If

    End Sub
    ' a series of subroutines dedicated to limiting user input so that illegal inputs are not entered
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

    End Sub
    'Asc(e.KeyChar) <> Asc("x") And Asc(e.KeyChar) <> Asc("X") And Asc(e.KeyChar) <> Asc("e") And Asc(e.KeyChar) <> Asc("+") And Asc(e.KeyChar) <> Asc("-") And Asc(e.KeyChar) <> Asc("^") And Asc(e.KeyChar) <> Asc("*") And Asc(e.KeyChar) <> Asc("/") And Asc(e.KeyChar) <> Asc(" ") And Asc(e.KeyChar) <> Asc("(") And Asc(e.KeyChar) <> Asc(")") Then
    Private Sub userequationbox_allowedchars(sender As Object, e As KeyPressEventArgs) Handles yequationbox.KeyPress
        Dim acceptedcharacters As String
        If IsParametric.Checked = False Then
            acceptedcharacters = "1234567890xe^*+- ()./sctz"

        Else
            acceptedcharacters = "1234567890te^*+- ()./sctz"
        End If

        If Not (acceptedcharacters.Contains(e.KeyChar)) And Asc(e.KeyChar) <> 8 Then
            e.Handled = True
        End If
    End Sub

    Private Sub maxlimitbox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles maxlimitbox.KeyPress
        Dim acceptedcharacters As String = "-1234567890"
        If Not (acceptedcharacters.Contains(e.KeyChar)) And Asc(e.KeyChar) <> 8 Then
            e.Handled = True

        End If
    End Sub

    Private Sub minlimitbox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles minlimitbox.KeyPress
        Dim acceptedcharacters As String = "-1234567890"
        If Not (acceptedcharacters.Contains(e.KeyChar)) And Asc(e.KeyChar) <> 8 Then
            e.Handled = True
        End If
    End Sub

    Private Sub userequationbox_TextChanged(sender As Object, e As EventArgs) Handles yequationbox.TextChanged
        equationintegralbox.Text = yequationbox.Text
    End Sub
    'changes the integral box's equation so that it is identical to the main equation
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        _max = (Decimal.Parse(FromBox.Text))
        _min = _max * -1
        _plotlength = (_max - _min) / 0.1
        _numberrange = _max - _min
        ReDim dataPoints(_plotlength)
        plot(_max)
    End Sub

    Private Sub _changecoordinatelabel(sender As Object, e As MouseEventArgs) Handles GraphBox.MouseMove
        _xhover.Text = "x = " & Math.Round(screentogrid(e.X), 1) 'converts the screen position of the coordinates to the correct scale and rounds it to 1dp.
        _yhover.Text = "y = " & -1 * Math.Round(screentogrid(e.Y), 1)
    End Sub
    'changes the coordinates label so that the user can accurately pick out coordinates
    Private Sub maxlimitbox_TextChanged(sender As Object, e As EventArgs) Handles maxlimitbox.TextChanged
        If maxlimitbox.Text = "" Or maxlimitbox.Text = "-" Then
            maxlimitbox.Text = 0
        End If
    End Sub

    Private Sub minlimitbox_TextChanged(sender As Object, e As EventArgs) Handles minlimitbox.TextChanged
        If minlimitbox.Text = "" Or minlimitbox.Text = "-" Then
            minlimitbox.Text = 0
        End If
    End Sub


    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles IsParametric.CheckedChanged
        If IsParametric.Checked = True Then
            xequationbox.Enabled = True
            yequationbox.Clear()
            yequationbox.Text = "z"
        Else
            xequationbox.Enabled = False
        End If
    End Sub

End Class

Public Class Parser
    Private _equation As String = String.Empty
    Private _rpnEquation As String = String.Empty
    Private _replaceVariable As Char = Nothing





    Public Sub New(ByVal _equ As String, ByVal _var As Char)

        'String builder is used because it is much more flexible than a normal string
        Dim _strBld As New System.Text.StringBuilder()
        'is used to store each individual component of the equation so that it can be parsed
        Dim _dataList As New List(Of String)
        'stores the number temporarily
        Dim _store As String = String.Empty
        'Check each character
        For Each _ch As Char In _equ

            If Char.IsNumber(_ch) Then
                _store &= _ch.ToString 'separates the numbers
            ElseIf _ch.ToString = "e" Then
                _store &= Exp(1)
            Else
                _dataList.Add(_store) 'adds the numbers to the datalist
                _dataList.Add(_ch.ToString) 'adds the other characters (i.e. operators)
                _store = String.Empty
            End If


        Next


        If Not _store.Equals(String.Empty) Then
            _dataList.Add(_store) 'if the store contains characters then add them to the datalist
            _store = String.Empty
        End If

        For Each _str As String In _dataList
            If Not _str.Equals(String.Empty) Then 'if the string is not empty then append str to make a bigger string
                _strBld.AppendFormat("{0} ", _str)
            End If
        Next

        _equ = _strBld.ToString.TrimEnd 'end trimmed to reduce size

        Dim _equList As New List(Of String)

        For Each ch As Char In _equ.ToCharArray
            _equList.Add(ch.ToString)
        Next


        'Loop
        For i As Integer = 0 To _equList.Count - 1
            If _equList(i).Equals(_var) Then

                'Checks the brackets and replaces them as they are not needed in rpn
                If _equList.Count >= i + 3 Then
                    If _equList(i + 2).Equals("("c) Then
                        _equList(i) = String.Format("{0} *", _equList(i))
                    End If
                End If
                If Not (i - 2) <= -1 Then
                    If _equList(i - 2).Equals(")"c) Then
                        _equList(i) = String.Format("* {0}", _equList(i))
                    End If
                End If

                'checks each number to ensure that it is correctly formatted in the case that it is next to an x
                If _equList.Count >= i + 3 Then
                    If New TokenTool().IsNumber(_equList(i + 2)) Then
                        _equList(i) = String.Format("{0} *", _equList(i))
                    End If
                End If
                If Not (i - 2) <= -1 Then
                    If New TokenTool().IsNumber(_equList(i - 2)) Then
                        _equList(i) = String.Format("* {0}", _equList(i))
                    End If
                End If


            End If
        Next

        _strBld = New System.Text.StringBuilder
        For Each _str As String In _equList
            _strBld.Append(_str)
        Next

        'Store the equation
        _equation = _strBld.ToString
        'Converts the equation to reverse polish notation
        _rpnEquation = New InfixRPNConverter().ShuntingYard(_equation)
        'Store the character replacer
        _replaceVariable = _var

    End Sub


    Public Sub New(ByVal _equ As String)

        'Store our original equation
        _equation = _equ

        'Now convert it to RPN
        _rpnEquation = New InfixRPNConverter().ShuntingYard(_equ)

    End Sub


    Public Function Evaluate(ByVal _num As Double) As Double

        'Create our helper
        Dim _th As New TokenTool

        'Number list
        Dim _numberList As New List(Of Double)

        'Split
        Dim _list As New List(Of String)(_rpnEquation.Split(" "c))
        Dim sine As Boolean = False
        Dim tang As Boolean = False
        Dim cosine As Boolean = False
        'Start looping
        For Each ch As String In _list

            'check for a blank
            If Not ch.Equals(" "c) Then

                If ch.Equals("s"c) Then
                    ch = ""
                    sine = True
                    Continue For
                ElseIf ch.Equals("t"c) Then
                    ch = ""
                    tang = True
                    Continue For
                ElseIf ch.Equals("c"c) Then
                    ch = ""
                    cosine = True
                    Continue For
                End If
                'check this
                If ch.Equals(_replaceVariable) Then ch = _num
                ''''DANGER ZONE
                If sine = True Then
                    ch = Sin(ch)
                    sine = False
                ElseIf cosine = True Then
                    ch = Cos(ch)
                    cosine = False
                ElseIf tang = True Then
                    ch = Tan(ch)
                    tang = False
                End If
                'Check
                If _th.IsNumber(ch) Then

                    'It's a number, add it.
                    _numberList.Add(Convert.ToDouble(ch))

                ElseIf _th.IsOperator(ch) Then


                    'It's an operator, dequeue the previous items
                    Dim _first As Double = Convert.ToDouble(_numberList(_numberList.Count - 2))
                    Dim _second As Double = Convert.ToDouble(_numberList(_numberList.Count - 1))

                    'Create our stored number
                    Dim _storedNumber As Double = 0

                    'Perform our operation
                    Select Case ch
                        Case "+"
                            _storedNumber = _first + _second
                        Case "-"
                            _storedNumber = _first - _second
                        Case "/"
                            If _second = 0 Then
                                _storedNumber = 0
                            Else
                                _storedNumber = _first / _second 'a way of getting around dividing by 0 error
                            End If

                        Case "^"
                            _storedNumber = _first ^ _second
                        Case "*"
                            _storedNumber = _first * _second

                    End Select

                    'Remove the two we just used
                    _numberList.RemoveRange(_numberList.Count - 2, 2)

                    'Put back into the stack
                    _numberList.Add(_storedNumber)

                End If

            End If


        Next
        For ch = 0 To _list.Count - 1
            If ch.Equals("s"c) Then
                Dim ind As Integer = _list.IndexOf(ch)
                ch = Sin(Convert.ToDouble(_list(ind + 1)))
                _list(ind + 1) = ""
            End If
        Next
        'Give it back.
        Return _numberList(0)



    End Function


    Public Function Evaluate() As Double
        Return Evaluate(0)
    End Function

End Class

Public Class InfixRPNConverter

    Private _tt As New TokenTool
    Private _output As New Queue(Of String)
    Private _operators As New Stack(Of String)


    Public Function ShuntingYard(ByVal expression As String) As String
        'uses the shunting yard algorithm as discussed in the pseudocde in order to convert the expression into RPN
        'Tokens
        Dim tokens As New List(Of String)(expression.Split(" "c))

        'Remove the spaces
        For i As Integer = tokens.Count - 1 To 0 Step -1
            If tokens(i).Equals(String.Empty) Then tokens.RemoveAt(i)
        Next

        'Loop
        For Each token As String In tokens

            If Not token.Equals(" "c) Then

                If _tt.IsNumber(token) Then

                    _output.Enqueue(token)

                ElseIf _tt.IsFunction(token) Then
                    _operators.Push(token)

                ElseIf token.Equals(","c) Then
                    While _operators.Count > 0 AndAlso Not _operators.Peek.Equals("("c)


                        Dim topOperator As String = _operators.Pop
                        _output.Enqueue(topOperator)

                    End While
                ElseIf _tt.IsOperator(token) Then

                    While _operators.Count > 0 AndAlso _tt.IsOperator(_operators.Peek)

                        If (_tt.IsLeftAssociative(token) AndAlso _operators.Count > 0 AndAlso _tt.GetPrecedenceFor(token) <= _tt.GetPrecedenceFor(_operators.Peek)) OrElse (_tt.IsRightAssociative(token) AndAlso _tt.GetPrecedenceFor(token) < _tt.GetPrecedenceFor(_operators.Peek)) Then

                            Dim operatorToReturn As String = _operators.Pop()
                            _output.Enqueue(operatorToReturn)

                        Else

                            Exit While

                        End If

                    End While

                    _operators.Push(token)

                End If

                If token.Equals("("c) Then

                    _operators.Push(token)

                End If

                If token.Equals(")"c) Then

                    While _operators.Count > 0 AndAlso Not _operators.Peek.Equals("("c)
                        _output.Enqueue(_operators.Pop)
                    End While

                    _operators.Pop()

                End If

                If _operators.Count > 0 AndAlso _tt.IsFunction(_operators.Peek) Then

                    _output.Enqueue(_operators.Pop)

                End If

            Else

                _output.Enqueue(" ")

            End If

        Next

        While _operators.Count > 0 AndAlso _tt.IsOperator(_operators.Peek)
            _output.Enqueue(_operators.Pop)
        End While

        Dim _result As String = String.Empty
        While _output.Count > 0
            _result &= _output.Dequeue & " "
        End While
        Return _result.TrimEnd

    End Function

End Class

Public Class TokenTool
    'splits the equation into token characters which have unique properties which are used in converting the equation into rpn
    Private _operators As String = "+-*/%=!^"
    Private _leftAssoc As String = "*/%+-"
    Private _rightAssoc As String = "!=^"
    'sets the property of right or left associativity
    Private ReadOnly Property precedence As Dictionary(Of String, Integer)

        Get

            'Build the dictionary
            Dim _dic As New Dictionary(Of String, Integer)
            _dic.Add("=", 1)
            _dic.Add("+-", 2)
            _dic.Add("*/%", 3)
            _dic.Add("!", 4)
            _dic.Add("^", 5)

            'Give back
            Return _dic

        End Get

    End Property


    Public Function IsOperator(ByVal tk As String) As Boolean
        If String.IsNullOrEmpty(tk) Then Return False
        Return _operators.Contains(tk)
    End Function

    Public Function GetPrecedenceFor(ByVal token As String) As Integer
        For Each _key As String In precedence.Keys
            If _key.Contains(token) Then Return precedence(_key)
        Next
    End Function

    Public Function IsNumber(ByVal token As String) As Boolean
        Dim _x As Double
        If Double.TryParse(token, _x) Then Return True Else Return False
        'Return Char.IsNumber(Convert.ToChar(token))
    End Function

    Public Function IsFunction(ByVal token As String) As Boolean
        Return Char.IsLetter(Convert.ToChar(token))
    End Function

    Public Function IsLeftAssociative(ByVal token As String) As Boolean
        If Not String.IsNullOrEmpty(token) AndAlso token.Length.Equals(1) Then Return _leftAssoc.Contains(token) Else Throw New ArgumentException("incorrect token!")
    End Function

    Public Function IsRightAssociative(ByVal token As String) As Boolean
        If Not String.IsNullOrEmpty(token) AndAlso token.Length.Equals(1) Then Return _rightAssoc.Contains(token) Else Throw New ArgumentException("incorrect token!")
    End Function

End Class
