Option Explicit On
Option Strict Off
Imports System.Math
Imports System.Drawing

Public Class graph
    Dim _max As Decimal = 100
    Dim _min As Decimal = _max * -1
        Dim _plotlength As Decimal = (_max - _min) * 10
        Dim _numberrange As Decimal = _max - _min
        Dim dataPoints(_plotlength) As Point
        Dim xyvalues(_plotlength) As Point

    Structure coordinate
        Public realposition As Point
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

    End Sub
    Private Sub plot(max As Integer)
            Header.ResetText()

       If Decimal.Parse(FromBox.Text) = 0 Then
            FromBox.Text = "10"
            Header.Text = "Invalid input. Please try again."
            Header.ForeColor = Color.Red
        End If

        '_max = (Decimal.Parse(FromBox.Text))
        '_min = _max * -1
        '_plotlength = (_max - _min) / 0.1
        '_numberrange = _max - _min
        'ReDim dataPoints(_plotlength)





        Dim eq As Object = New Parser(userequationbox.Text, "x")

        cleantable(TableOfValues)

        FillGridPoints(eq)

        If Decimal.Parse(maxlimitbox.Text) < Decimal.Parse(minlimitbox.Text) Then 'ensures that limits don't give a negative answer
            swap(maxlimitbox.Text, minlimitbox.Text)
        End If

        AreaLabel.Text = trapeziumrule(xyvalues, _plotlength / 2 + (Decimal.Parse(minlimitbox.Text) * 10), (_plotlength / 2 + (Decimal.Parse(maxlimitbox.Text) * 10)), 0.1)




    End Sub
    Private Sub cleantable(Table As DataGridView)
        Table.Rows.Clear()
        Table.Columns.Clear()
    End Sub

    Private Sub FillGridPoints(equation As Parser)
        For x As Decimal = _min To _max Step 0.1 'step 0.1 for more accurate shape, plotting 200 points

            Dim y As Decimal
            'function of x in use
            Try
                y = equation.Evaluate(x)
            Catch ex As OverflowException
                y = Math.Truncate(equation.Evaluate)
            End Try

            'scale and centre the point coordinates to fit the picture box
            Dim p As coordinate

            p.screenposition.X = 10 * x * ((GraphBox.Width / _plotlength)) + (GraphBox.Width / 2) 'half the box's width and height are added so that the plot starts at 0
            p.screenposition.Y = (GraphBox.Height / 2 - ((y * 10 * GraphBox.Height / _plotlength))) '10 is added due to a strange offset\

            Dim o As coordinate
            o.realposition.X = x
            o.realposition.Y = y

            Me.FillTableofValues(Nothing, Nothing)
            Dim row As String() = New String() {x, y}
            TableOfValues.Rows.Add(row)


            Dim arraylocation As Decimal = (x * 10 + (_plotlength * 0.5))
            'add the data point to the array
            'x is multiplied by 10 so that the many decimal coordinate values are assigned a place in the array
            xyvalues(arraylocation) = o.realposition
            dataPoints(arraylocation) = p.screenposition
        Next
    End Sub

    'this sub plots each point individually as points
    'this sub draws the graph and the grid
    Private Sub PictureBox1_Paint(ByVal sender As Object, ByVal e As PaintEventArgs) Handles GraphBox.Paint

        Dim linepen As New Pen(Color.LightGray, 2)
        Dim axispen As New Pen(Color.Black, 4)

        Dim numberfont As New Font("Arial", GraphBox.Width / (_numberrange * 3))
        Dim numberbrush As New SolidBrush(Color.Black)
        'horizontal lines are as long as the width of the box
        Dim linehor As Decimal = GraphBox.Width
        'and they're divided by plotlength so that it goes over 10 to -10

        'changing y coordinate
        Dim ycoord As Decimal
        Dim xcoord As Decimal

        '___________________________________________Y AXIS_______________________________________________
        'this block of code draws the vertical grid lines
        Dim vertlinespacing As Single = GraphBox.Width / _numberrange
        Dim v As Integer = 0
        For ycoord = _min To _max - 1
            Dim yaxisnumbers As Point

            e.Graphics.DrawLine(linepen, v, 0, v, GraphBox.Height)
            v = v + vertlinespacing

            yaxisnumbers.X = xcoord - 5 'numbers are drawn a bit to the right so I subtracted
            yaxisnumbers.Y = GraphBox.Height / 2
            e.Graphics.DrawString(ycoord, numberfont, numberbrush, yaxisnumbers) 'Draws the numbers for the x axis

            xcoord = xcoord + vertlinespacing
            'draws the y axis
            If ycoord = -1 Then
                e.Graphics.DrawLine(axispen, v, 0, v, GraphBox.Height)
            End If
        Next

        e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
        'step 10 so that every integer value has its own point

        Dim _pointsize As Single = 5
        Dim _pointoffset As Single = _pointsize / 2
        For x = 0 To dataPoints.Length Step 10
            'draws an ellipse for integer points on the graph 

            e.Graphics.FillEllipse(numberbrush, dataPoints(x).X - _pointoffset, dataPoints(x).Y - _pointoffset, _pointsize, _pointsize)
        Next
        e.Graphics.DrawLines(Pens.HotPink, dataPoints)

        GraphBox.Invalidate()
    End Sub

    Private Sub _drawhorizontallines(ByVal sender As Object, ByVal e As PaintEventArgs) Handles GraphBox.Paint
        Dim linepen As New Pen(Color.LightGray, 2)
        Dim axispen As New Pen(Color.Black, 4)
        Dim numberfont As New Font("Arial", GraphBox.Width / (_numberrange * 3))
        Dim numberbrush As New SolidBrush(Color.Black)

        Dim horlinespacing As Decimal = GraphBox.Height / _numberrange
        Dim h As Integer = 0
        For xcoord = _min To _max - 1

            e.Graphics.DrawLine(linepen, 0, h, GraphBox.Width, h)
            'draws the numbers for the y axis
            Dim xaxisnumbers As Point
            xaxisnumbers.X = GraphBox.Width / 2
            xaxisnumbers.Y = h - 5 'numbers are drawn a bit down so I subtracted
            If xcoord > 0 Then
                e.Graphics.DrawString(0 - xcoord, numberfont, numberbrush, xaxisnumbers)
            Else
                e.Graphics.DrawString(0 - xcoord, numberfont, numberbrush, xaxisnumbers)
            End If

            h = h + horlinespacing

            If xcoord = -1 Then
                e.Graphics.DrawLine(axispen, 0, h, GraphBox.Width, h)

            End If

        Next
    End Sub
    Private Sub graph_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        ' GraphBox.Width = Me.Width * (600 / 650)
        '  GraphBox.Height = Me.Height * (600 / 650) '0.923

        GraphBox.Invalidate()
        Me.Invalidate()
        GraphBox.CreateGraphics()


    End Sub

    Private Sub FillTableofValues(sender As Object, e As PaintEventArgs) Handles TableOfValues.Paint
        TableOfValues.ColumnCount = 2
        TableOfValues.Columns(0).Name = "x"
        TableOfValues.Columns(1).Name = "y"

    End Sub

    Private Function trapeziumrule(ByVal pointlist() As Point, ByVal y0 As Decimal, ByVal yn As Decimal, ByVal h As Decimal)
        Dim multipleloop As Decimal
        For i = y0 + 1 To yn - 1
            multipleloop = multipleloop + pointlist(i).Y
        Next

        Return 0.5 * h * ((pointlist(y0).Y + pointlist(yn).Y) + (2 * (multipleloop)))
    End Function
    Private Sub swap(ByRef a, ByRef b)
        Dim c
        c = a
        a = b
        b = c
    End Sub
    Private Function FiniteDifferenceApproximation(ByVal point1 As Decimal, ByVal point2 As Decimal, ByVal f As Parser)
        Dim h As Decimal = point2 - point1

        Return (f.Evaluate(point2) - f.Evaluate(point1)) / h
    End Function


    Private Sub GraphBox_MouseWheel(sender As Object, e As MouseEventArgs) Handles GraphBox.MouseWheel


        If e.Delta > 0 Then
            _max *= 2
            _min = _max * -1
            _plotlength = (_max - _min) * 10
            _numberrange = _max - _min
            ReDim dataPoints(_plotlength)
            ReDim xyvalues(_plotlength)
            plot(_max)
            GraphBox.Invalidate()

        ElseIf e.Delta < 0 And _max > 5 Then
            _max *= 1 / 2
            _min = _max * -1
            _plotlength = (_max - _min) * 10
            _numberrange = _max - _min
            ReDim dataPoints(_plotlength)
            ReDim xyvalues(_plotlength)
            plot(_max)
            GraphBox.Invalidate()
        End If


    End Sub

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

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

    End Sub
    'Asc(e.KeyChar) <> Asc("x") And Asc(e.KeyChar) <> Asc("X") And Asc(e.KeyChar) <> Asc("e") And Asc(e.KeyChar) <> Asc("+") And Asc(e.KeyChar) <> Asc("-") And Asc(e.KeyChar) <> Asc("^") And Asc(e.KeyChar) <> Asc("*") And Asc(e.KeyChar) <> Asc("/") And Asc(e.KeyChar) <> Asc(" ") And Asc(e.KeyChar) <> Asc("(") And Asc(e.KeyChar) <> Asc(")") Then
    Private Sub userequationbox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles userequationbox.KeyPress

        Dim acceptedcharacters As String = "1234567890xe^*+- ()./"
        If Not (acceptedcharacters.Contains(e.KeyChar)) And Asc(e.KeyChar) <> 8 Then
            e.Handled = True
        End If

    End Sub

    Private Sub maxlimitbox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles maxlimitbox.KeyPress
        Dim acceptedcharacters As String = "1234567890"
        If Not (acceptedcharacters.Contains(e.KeyChar)) And Asc(e.KeyChar) <> 8 Then
            e.Handled = True
        End If
    End Sub

    Private Sub minlimitbox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles minlimitbox.KeyPress
        Dim acceptedcharacters As String = "123456789"
        If Not (acceptedcharacters.Contains(e.KeyChar)) And Asc(e.KeyChar) <> 8 Then
            e.Handled = True
        End If
    End Sub

    Private Sub userequationbox_TextChanged(sender As Object, e As EventArgs) Handles userequationbox.TextChanged
        equationintegralbox.Text = userequationbox.Text
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        _max = CInt(FromBox.Text)
        plot(_max)
    End Sub
End Class

Public Class Parser

    Private _equation As String = String.Empty
    Private _rpnEquation As String = String.Empty
    Private _replaceVariable As Char = Nothing




    ''' <summary>
    ''' Initializes the RPN Class, passing in an equation with a variable.
    ''' </summary>
    ''' <param name="_equ">The equation to process.</param>
    ''' <param name="_var">The variable character.</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal _equ As String, ByVal _var As Char)

        'String builder
        Dim _strBld As New System.Text.StringBuilder()

        'AppendString
        Dim _dataList As New List(Of String)

        'backup
        Dim _back As String = String.Empty

        'Check each character
        For Each _ch As Char In _equ

            'Add it together
            If Char.IsNumber(_ch) Then
                _back &= _ch.ToString
            ElseIf _ch.ToString = "e" OrElse _ch.ToString = "E" Then
                _back &= 2.71828
            Else
                _dataList.Add(_back)
                _dataList.Add(_ch.ToString)
                _back = String.Empty
            End If


        Next

        'One last check
        If Not _back.Equals(String.Empty) Then _dataList.Add(_back) : _back = String.Empty

        'Now spit it back out properly
        For Each _str As String In _dataList
            If Not _str.Equals(String.Empty) Then _strBld.AppendFormat("{0} ", _str)
        Next


        'Assign back
        _equ = _strBld.ToString.TrimEnd

        'Do a bit of messing
        Dim _equList As New List(Of String)

        For Each ch As Char In _equ.ToCharArray
            _equList.Add(ch.ToString)
        Next


        'Loop
        For i As Integer = 0 To _equList.Count - 1
            If _equList(i).Equals(_var) Then

                'Parenthesis Check
                If _equList.Count >= i + 3 Then If _equList(i + 2).Equals("("c) Then _equList(i) = String.Format("{0} *", _equList(i))
                If Not (i - 2) <= -1 Then If _equList(i - 2).Equals(")"c) Then _equList(i) = String.Format("* {0}", _equList(i))

                'Number check
                If _equList.Count >= i + 3 Then If New TokenHelper().IsNumber(_equList(i + 2)) Then _equList(i) = String.Format("{0} *", _equList(i))
                If Not (i - 2) <= -1 Then If New TokenHelper().IsNumber(_equList(i - 2)) Then _equList(i) = String.Format("* {0}", _equList(i))

            End If
        Next

        _strBld = New System.Text.StringBuilder
        For Each _str As String In _equList
            _strBld.Append(_str)
        Next

        'Store our original equation
        _equation = _strBld.ToString

        'Now convert it to RPN
        _rpnEquation = New InfixTransformer().Transform(_equation)

        'Store the character replacer
        _replaceVariable = _var

    End Sub

    ''' <summary>
    ''' Initializes the RPN Class, passing in an equation with a variable.
    ''' </summary>
    ''' <param name="_equ">The equation to process.</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal _equ As String)

        'Store our original equation
        _equation = _equ

        'Now convert it to RPN
        _rpnEquation = New InfixTransformer().Transform(_equ)

    End Sub

    ''' <summary>
    ''' Evaluates the function at the given value.
    ''' </summary>
    ''' <param name="_int">The number to evaluate the function at.</param>
    ''' <returns>The value of this function</returns>
    ''' <remarks></remarks>
    ''' 
    Public Function Evaluate(ByVal _int As Double) As Double

        'Create our helper
        Dim _th As New TokenHelper

        'Number list
        Dim _numberList As New List(Of Double)

        'Split
        Dim _list As New List(Of String)(_rpnEquation.Split(" "c))

        'Start looping
        For Each ch As String In _list

            'check for a blank
            If Not ch.Equals(" "c) Then

                'check this
                If ch.Equals(_replaceVariable) Then ch = _int

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
                                _storedNumber = _first / _second
                            End If

                        Case "^"
                            _storedNumber = _first ^ _second
                        Case "*"
                            _storedNumber = _first * _second
                        Case "!"
                            '_storedNumber = Factoral(_first)
                    End Select

                    'Remove the two we just used
                    _numberList.RemoveRange(_numberList.Count - 2, 2)

                    'Put back into the stack
                    _numberList.Add(_storedNumber)

                End If

            End If


        Next

        'Give it back.
        Return _numberList(0)

    End Function


    ''' <summary>
    ''' Evaluates the function.
    ''' </summary>
    ''' <returns>The value of this function</returns>
    ''' <remarks></remarks>
    Public Function Evaluate() As Double
        Return Evaluate(0)
    End Function

End Class

Public Class InfixTransformer

        Private _tokenHelper As New TokenHelper
        Private _output As New Queue(Of String)
        Private _operators As New Stack(Of String)

        ''' <summary>
        ''' Converts an Infix Equation to a PostFix Equation.
        ''' </summary>
        ''' <param name="expression">The expression to use, minus any spaces.</param>
        ''' <returns>A PostFix Equation.</returns>
        ''' <remarks></remarks>
        Public Function Transform(ByVal expression As String) As String

            'Tokens
            Dim tokens As New List(Of String)(expression.Split(" "c))

            'Remove the spaces
            For i As Integer = tokens.Count - 1 To 0 Step -1
                If tokens(i).Equals(String.Empty) Then tokens.RemoveAt(i)
            Next

            'Loop
            For Each token As String In tokens

                If Not token.Equals(" "c) Then

                    If _tokenHelper.IsNumber(token) Then
                        'Number
                        _output.Enqueue(token)

                    ElseIf _tokenHelper.IsFunction(token) Then
                        'Function
                        _operators.Push(token)

                    ElseIf token.Equals(","c) Then
                        While _operators.Count > 0 AndAlso Not _operators.Peek.Equals("("c)


                            Dim topOperator As String = _operators.Pop
                            _output.Enqueue(topOperator)

                        End While
                    ElseIf _tokenHelper.IsOperator(token) Then

                        While _operators.Count > 0 AndAlso _tokenHelper.IsOperator(_operators.Peek)

                            If (_tokenHelper.IsLeftAssociative(token) AndAlso _operators.Count > 0 AndAlso _tokenHelper.GetPrecedenceFor(token) <= _tokenHelper.GetPrecedenceFor(_operators.Peek)) OrElse (_tokenHelper.IsRightAssociative(token) AndAlso _tokenHelper.GetPrecedenceFor(token) < _tokenHelper.GetPrecedenceFor(_operators.Peek)) Then

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

                    If _operators.Count > 0 AndAlso _tokenHelper.IsFunction(_operators.Peek) Then

                        _output.Enqueue(_operators.Pop)

                    End If

                Else

                    _output.Enqueue(" ")

                End If

            Next

            While _operators.Count > 0 AndAlso _tokenHelper.IsOperator(_operators.Peek)
                _output.Enqueue(_operators.Pop)
            End While

            Dim _result As String = String.Empty
            While _output.Count > 0
                _result &= _output.Dequeue & " "
            End While
            Return _result.TrimEnd

        End Function

    End Class

    Public Class TokenHelper

        Private _operators As String = "+-*/%=!^"
        Private _leftAssociativeOperators As String = "*/%+-"
        Private _rightAssociativeOperators As String = "!=^"

        Private ReadOnly Property _operatorsToPrecedenceMapping As Dictionary(Of String, Integer)

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

        ''' <summary>
        ''' Checks to see if a token is an operator.
        ''' </summary>
        ''' <param name="token">The token.</param>
        ''' <returns>Boolean</returns>
        ''' <remarks></remarks>
        Public Function IsOperator(ByVal token As String) As Boolean
            If String.IsNullOrEmpty(token) Then Return False
            Return _operators.Contains(token)
        End Function

        ''' <summary>
        ''' Returns the precedence for the passed token.
        ''' </summary>
        ''' <param name="token">The token to pass.</param>
        ''' <returns>Integer</returns>
        ''' <remarks></remarks>
        Public Function GetPrecedenceFor(ByVal token As String) As Integer
            For Each _key As String In _operatorsToPrecedenceMapping.Keys
                If _key.Contains(token) Then Return _operatorsToPrecedenceMapping(_key)
            Next
            Throw New InvalidOperationException("invalid operator")
        End Function

        ''' <summary>
        ''' Returns if the passed token is a number.
        ''' </summary>
        ''' <param name="token">The token.</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function IsNumber(ByVal token As String) As Boolean
            Dim _x As Double
            If Double.TryParse(token, _x) Then Return True Else Return False
            'Return Char.IsNumber(Convert.ToChar(token))
        End Function

        ''' <summary>
        ''' Returns if the token
        ''' </summary>
        ''' <param name="token"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function IsFunction(ByVal token As String) As Boolean
            Return Char.IsLetter(Convert.ToChar(token))
        End Function

        ''' <summary>
        ''' Returns if the token is left associative
        ''' </summary>
        ''' <param name="token">the token</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function IsLeftAssociative(ByVal token As String) As Boolean
            If Not String.IsNullOrEmpty(token) AndAlso token.Length.Equals(1) Then Return _leftAssociativeOperators.Contains(token) Else Throw New ArgumentException("incorrect token!")
        End Function

        ''' <summary>
        ''' Returns if the token is right associative.
        ''' </summary>
        ''' <param name="token">the token</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function IsRightAssociative(ByVal token As String) As Boolean
            If Not String.IsNullOrEmpty(token) AndAlso token.Length.Equals(1) Then Return _rightAssociativeOperators.Contains(token) Else Throw New ArgumentException("incorrect token!")
        End Function

    End Class
