<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class graph
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(graph))
        Me.GraphBox = New System.Windows.Forms.PictureBox()
        Me.yequationbox = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Header = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.FromBox = New System.Windows.Forms.TextBox()
        Me.TableOfValues = New System.Windows.Forms.DataGridView()
        Me.AreaLabel = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.equationintegralbox = New System.Windows.Forms.TextBox()
        Me.maxlimitbox = New System.Windows.Forms.TextBox()
        Me.minlimitbox = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.ToBox = New System.Windows.Forms.TextBox()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me._xhover = New System.Windows.Forms.Label()
        Me._yhover = New System.Windows.Forms.Label()
        Me.SaveButton = New System.Windows.Forms.Button()
        Me.LoadButton = New System.Windows.Forms.Button()
        Me.xequationbox = New System.Windows.Forms.TextBox()
        Me.IsParametric = New System.Windows.Forms.CheckBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.xforgradient = New System.Windows.Forms.TextBox()
        Me.Gradientat = New System.Windows.Forms.Label()
        Me.equals = New System.Windows.Forms.Label()
        Me.DifferenceQuotient = New System.Windows.Forms.Label()
        CType(Me.GraphBox, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TableOfValues, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GraphBox
        '
        Me.GraphBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.GraphBox.Location = New System.Drawing.Point(12, 3)
        Me.GraphBox.MaximumSize = New System.Drawing.Size(600, 600)
        Me.GraphBox.Name = "GraphBox"
        Me.GraphBox.Size = New System.Drawing.Size(600, 600)
        Me.GraphBox.TabIndex = 11
        Me.GraphBox.TabStop = False
        '
        'yequationbox
        '
        Me.yequationbox.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.yequationbox.Location = New System.Drawing.Point(665, 38)
        Me.yequationbox.MaxLength = 21
        Me.yequationbox.Name = "yequationbox"
        Me.yequationbox.Size = New System.Drawing.Size(225, 38)
        Me.yequationbox.TabIndex = 12
        Me.yequationbox.Text = "x"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(665, 559)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(262, 44)
        Me.Button1.TabIndex = 13
        Me.Button1.Text = "Plot!"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Header
        '
        Me.Header.AutoSize = True
        Me.Header.Location = New System.Drawing.Point(665, 9)
        Me.Header.Name = "Header"
        Me.Header.Size = New System.Drawing.Size(225, 13)
        Me.Header.TabIndex = 14
        Me.Header.Text = "Welcome to Modulus! Please enter a function."
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(618, 44)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(47, 29)
        Me.Label2.TabIndex = 15
        Me.Label2.Text = "y ="
        '
        'FromBox
        '
        Me.FromBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FromBox.Location = New System.Drawing.Point(660, 123)
        Me.FromBox.MaxLength = 21
        Me.FromBox.Name = "FromBox"
        Me.FromBox.Size = New System.Drawing.Size(44, 26)
        Me.FromBox.TabIndex = 16
        Me.FromBox.Text = "10"
        '
        'TableOfValues
        '
        Me.TableOfValues.AllowUserToAddRows = False
        Me.TableOfValues.AllowUserToDeleteRows = False
        Me.TableOfValues.AllowUserToResizeColumns = False
        Me.TableOfValues.AllowUserToResizeRows = False
        Me.TableOfValues.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.TableOfValues.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.TableOfValues.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.TableOfValues.Location = New System.Drawing.Point(630, 155)
        Me.TableOfValues.Name = "TableOfValues"
        Me.TableOfValues.RowHeadersVisible = False
        Me.TableOfValues.Size = New System.Drawing.Size(236, 195)
        Me.TableOfValues.TabIndex = 17
        '
        'AreaLabel
        '
        Me.AreaLabel.AutoSize = True
        Me.AreaLabel.Location = New System.Drawing.Point(652, 487)
        Me.AreaLabel.Name = "AreaLabel"
        Me.AreaLabel.Size = New System.Drawing.Size(35, 13)
        Me.AreaLabel.TabIndex = 18
        Me.AreaLabel.Text = "Area: "
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(625, 421)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(21, 29)
        Me.Label4.TabIndex = 19
        Me.Label4.Text = "∫"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(871, 421)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(40, 29)
        Me.Label5.TabIndex = 20
        Me.Label5.Text = "dx"
        '
        'equationintegralbox
        '
        Me.equationintegralbox.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.equationintegralbox.Location = New System.Drawing.Point(692, 421)
        Me.equationintegralbox.MaxLength = 21
        Me.equationintegralbox.Name = "equationintegralbox"
        Me.equationintegralbox.ReadOnly = True
        Me.equationintegralbox.Size = New System.Drawing.Size(170, 29)
        Me.equationintegralbox.TabIndex = 21
        '
        'maxlimitbox
        '
        Me.maxlimitbox.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.maxlimitbox.Location = New System.Drawing.Point(655, 412)
        Me.maxlimitbox.MaxLength = 21
        Me.maxlimitbox.Name = "maxlimitbox"
        Me.maxlimitbox.Size = New System.Drawing.Size(29, 17)
        Me.maxlimitbox.TabIndex = 22
        Me.maxlimitbox.Text = "0"
        '
        'minlimitbox
        '
        Me.minlimitbox.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.minlimitbox.Location = New System.Drawing.Point(655, 438)
        Me.minlimitbox.MaxLength = 21
        Me.minlimitbox.Name = "minlimitbox"
        Me.minlimitbox.Size = New System.Drawing.Size(29, 17)
        Me.minlimitbox.TabIndex = 23
        Me.minlimitbox.Text = "0"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(624, 131)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(30, 13)
        Me.Label1.TabIndex = 24
        Me.Label1.Text = "From"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(710, 131)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(20, 13)
        Me.Label6.TabIndex = 25
        Me.Label6.Text = "To"
        '
        'ToBox
        '
        Me.ToBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToBox.Location = New System.Drawing.Point(736, 123)
        Me.ToBox.MaxLength = 21
        Me.ToBox.Name = "ToBox"
        Me.ToBox.Size = New System.Drawing.Size(44, 26)
        Me.ToBox.TabIndex = 26
        Me.ToBox.Text = "-10"
        '
        'Timer1
        '
        '
        '_xhover
        '
        Me._xhover.AutoSize = True
        Me._xhover.BackColor = System.Drawing.SystemColors.Control
        Me._xhover.Location = New System.Drawing.Point(12, 611)
        Me._xhover.Name = "_xhover"
        Me._xhover.Size = New System.Drawing.Size(18, 13)
        Me._xhover.TabIndex = 27
        Me._xhover.Text = "x="
        '
        '_yhover
        '
        Me._yhover.AutoSize = True
        Me._yhover.BackColor = System.Drawing.SystemColors.Control
        Me._yhover.Location = New System.Drawing.Point(168, 611)
        Me._yhover.Name = "_yhover"
        Me._yhover.Size = New System.Drawing.Size(18, 13)
        Me._yhover.TabIndex = 28
        Me._yhover.Text = "y="
        '
        'SaveButton
        '
        Me.SaveButton.Location = New System.Drawing.Point(977, 559)
        Me.SaveButton.Name = "SaveButton"
        Me.SaveButton.Size = New System.Drawing.Size(75, 23)
        Me.SaveButton.TabIndex = 30
        Me.SaveButton.Text = "Save"
        Me.SaveButton.UseVisualStyleBackColor = True
        '
        'LoadButton
        '
        Me.LoadButton.Location = New System.Drawing.Point(977, 580)
        Me.LoadButton.Name = "LoadButton"
        Me.LoadButton.Size = New System.Drawing.Size(75, 23)
        Me.LoadButton.TabIndex = 31
        Me.LoadButton.Text = "Load"
        Me.LoadButton.UseVisualStyleBackColor = True
        '
        'xequationbox
        '
        Me.xequationbox.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.xequationbox.Location = New System.Drawing.Point(665, 79)
        Me.xequationbox.MaxLength = 21
        Me.xequationbox.Name = "xequationbox"
        Me.xequationbox.Size = New System.Drawing.Size(225, 38)
        Me.xequationbox.TabIndex = 32
        Me.xequationbox.Text = "x"
        '
        'IsParametric
        '
        Me.IsParametric.AutoSize = True
        Me.IsParametric.Location = New System.Drawing.Point(906, 38)
        Me.IsParametric.Name = "IsParametric"
        Me.IsParametric.Size = New System.Drawing.Size(76, 17)
        Me.IsParametric.TabIndex = 33
        Me.IsParametric.Text = "Parametric"
        Me.IsParametric.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(618, 85)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(47, 29)
        Me.Label3.TabIndex = 34
        Me.Label3.Text = "x ="
        '
        'xforgradient
        '
        Me.xforgradient.Location = New System.Drawing.Point(717, 375)
        Me.xforgradient.Name = "xforgradient"
        Me.xforgradient.Size = New System.Drawing.Size(23, 20)
        Me.xforgradient.TabIndex = 35
        Me.xforgradient.Text = "0"
        '
        'Gradientat
        '
        Me.Gradientat.AutoSize = True
        Me.Gradientat.Location = New System.Drawing.Point(652, 378)
        Me.Gradientat.Name = "Gradientat"
        Me.Gradientat.Size = New System.Drawing.Size(59, 13)
        Me.Gradientat.TabIndex = 36
        Me.Gradientat.Text = "Gradient at"
        '
        'equals
        '
        Me.equals.AutoSize = True
        Me.equals.Location = New System.Drawing.Point(746, 378)
        Me.equals.Name = "equals"
        Me.equals.Size = New System.Drawing.Size(13, 13)
        Me.equals.TabIndex = 37
        Me.equals.Text = "="
        '
        'DifferenceQuotient
        '
        Me.DifferenceQuotient.AutoSize = True
        Me.DifferenceQuotient.Location = New System.Drawing.Point(765, 378)
        Me.DifferenceQuotient.Name = "DifferenceQuotient"
        Me.DifferenceQuotient.Size = New System.Drawing.Size(13, 13)
        Me.DifferenceQuotient.TabIndex = 38
        Me.DifferenceQuotient.Text = "0"
        '
        'graph
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.ClientSize = New System.Drawing.Size(1109, 633)
        Me.Controls.Add(Me.DifferenceQuotient)
        Me.Controls.Add(Me.equals)
        Me.Controls.Add(Me.Gradientat)
        Me.Controls.Add(Me.xforgradient)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.IsParametric)
        Me.Controls.Add(Me.xequationbox)
        Me.Controls.Add(Me.LoadButton)
        Me.Controls.Add(Me.SaveButton)
        Me.Controls.Add(Me._yhover)
        Me.Controls.Add(Me._xhover)
        Me.Controls.Add(Me.ToBox)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.minlimitbox)
        Me.Controls.Add(Me.maxlimitbox)
        Me.Controls.Add(Me.equationintegralbox)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.AreaLabel)
        Me.Controls.Add(Me.TableOfValues)
        Me.Controls.Add(Me.FromBox)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Header)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.yequationbox)
        Me.Controls.Add(Me.GraphBox)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimumSize = New System.Drawing.Size(642, 626)
        Me.Name = "graph"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Graphing Program"
        CType(Me.GraphBox, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TableOfValues, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GraphBox As PictureBox
    Friend WithEvents yequationbox As TextBox
    Friend WithEvents Button1 As Button
    Friend WithEvents Header As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents FromBox As TextBox
    Friend WithEvents TableOfValues As DataGridView
    Friend WithEvents AreaLabel As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents equationintegralbox As TextBox
    Friend WithEvents maxlimitbox As TextBox
    Friend WithEvents minlimitbox As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents ToBox As TextBox
    Friend WithEvents Timer1 As Timer
    Friend WithEvents _xhover As Label
    Friend WithEvents _yhover As Label
    Friend WithEvents SaveButton As Button
    Friend WithEvents LoadButton As Button
    Friend WithEvents xequationbox As TextBox
    Friend WithEvents IsParametric As CheckBox
    Friend WithEvents Label3 As Label
    Friend WithEvents xforgradient As TextBox
    Friend WithEvents Gradientat As Label
    Friend WithEvents equals As Label
    Friend WithEvents DifferenceQuotient As Label
End Class
