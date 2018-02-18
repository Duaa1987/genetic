<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ProtienSynth
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
        Me.LblPopsize = New System.Windows.Forms.Label()
        Me.LblElit = New System.Windows.Forms.Label()
        Me.LblCross = New System.Windows.Forms.Label()
        Me.LblMut = New System.Windows.Forms.Label()
        Me.Lbltarget = New System.Windows.Forms.Label()
        Me.Lblprot = New System.Windows.Forms.Label()
        Me.LblMax = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.TBMax = New System.Windows.Forms.TextBox()
        Me.TBTarget = New System.Windows.Forms.TextBox()
        Me.TBProtLength = New System.Windows.Forms.TextBox()
        Me.TBMut = New System.Windows.Forms.TextBox()
        Me.TBCrossover = New System.Windows.Forms.TextBox()
        Me.TBElite = New System.Windows.Forms.TextBox()
        Me.TBPopSize = New System.Windows.Forms.TextBox()
        Me.BtnStart = New System.Windows.Forms.Button()
        Me.PBProtien = New System.Windows.Forms.PictureBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.TBProtArray = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TBProtStruct = New System.Windows.Forms.TextBox()
        Me.LblProtStruct = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        CType(Me.PBProtien, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'LblPopsize
        '
        Me.LblPopsize.AutoSize = True
        Me.LblPopsize.Location = New System.Drawing.Point(39, 27)
        Me.LblPopsize.Name = "LblPopsize"
        Me.LblPopsize.Size = New System.Drawing.Size(80, 13)
        Me.LblPopsize.TabIndex = 0
        Me.LblPopsize.Text = "Population Size"
        '
        'LblElit
        '
        Me.LblElit.AutoSize = True
        Me.LblElit.Location = New System.Drawing.Point(39, 52)
        Me.LblElit.Name = "LblElit"
        Me.LblElit.Size = New System.Drawing.Size(53, 13)
        Me.LblElit.TabIndex = 1
        Me.LblElit.Text = "Elite Rate"
        '
        'LblCross
        '
        Me.LblCross.AutoSize = True
        Me.LblCross.Location = New System.Drawing.Point(39, 77)
        Me.LblCross.Name = "LblCross"
        Me.LblCross.Size = New System.Drawing.Size(80, 13)
        Me.LblCross.TabIndex = 2
        Me.LblCross.Text = "Crossover Rate"
        '
        'LblMut
        '
        Me.LblMut.AutoSize = True
        Me.LblMut.Location = New System.Drawing.Point(39, 102)
        Me.LblMut.Name = "LblMut"
        Me.LblMut.Size = New System.Drawing.Size(74, 13)
        Me.LblMut.TabIndex = 3
        Me.LblMut.Text = "Mutation Rate"
        '
        'Lbltarget
        '
        Me.Lbltarget.AutoSize = True
        Me.Lbltarget.Location = New System.Drawing.Point(288, 52)
        Me.Lbltarget.Name = "Lbltarget"
        Me.Lbltarget.Size = New System.Drawing.Size(68, 13)
        Me.Lbltarget.TabIndex = 4
        Me.Lbltarget.Text = "Target Value"
        '
        'Lblprot
        '
        Me.Lblprot.AutoSize = True
        Me.Lblprot.Location = New System.Drawing.Point(288, 27)
        Me.Lblprot.Name = "Lblprot"
        Me.Lblprot.Size = New System.Drawing.Size(72, 13)
        Me.Lblprot.TabIndex = 5
        Me.Lblprot.Text = "Protien length"
        '
        'LblMax
        '
        Me.LblMax.AutoSize = True
        Me.LblMax.Location = New System.Drawing.Point(288, 77)
        Me.LblMax.Name = "LblMax"
        Me.LblMax.Size = New System.Drawing.Size(73, 13)
        Me.LblMax.TabIndex = 6
        Me.LblMax.Text = "Max Iterations"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.TBMax)
        Me.GroupBox1.Controls.Add(Me.TBTarget)
        Me.GroupBox1.Controls.Add(Me.TBProtLength)
        Me.GroupBox1.Controls.Add(Me.TBMut)
        Me.GroupBox1.Controls.Add(Me.TBCrossover)
        Me.GroupBox1.Controls.Add(Me.TBElite)
        Me.GroupBox1.Controls.Add(Me.TBPopSize)
        Me.GroupBox1.Controls.Add(Me.LblMax)
        Me.GroupBox1.Controls.Add(Me.Lblprot)
        Me.GroupBox1.Controls.Add(Me.Lbltarget)
        Me.GroupBox1.Controls.Add(Me.LblMut)
        Me.GroupBox1.Controls.Add(Me.LblCross)
        Me.GroupBox1.Controls.Add(Me.LblElit)
        Me.GroupBox1.Controls.Add(Me.LblPopsize)
        Me.GroupBox1.Location = New System.Drawing.Point(0, 13)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(541, 128)
        Me.GroupBox1.TabIndex = 7
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "GroupBox1"
        '
        'TBMax
        '
        Me.TBMax.Location = New System.Drawing.Point(387, 73)
        Me.TBMax.Name = "TBMax"
        Me.TBMax.Size = New System.Drawing.Size(100, 20)
        Me.TBMax.TabIndex = 13
        '
        'TBTarget
        '
        Me.TBTarget.Location = New System.Drawing.Point(387, 48)
        Me.TBTarget.Name = "TBTarget"
        Me.TBTarget.Size = New System.Drawing.Size(100, 20)
        Me.TBTarget.TabIndex = 12
        '
        'TBProtLength
        '
        Me.TBProtLength.Location = New System.Drawing.Point(387, 23)
        Me.TBProtLength.Name = "TBProtLength"
        Me.TBProtLength.ReadOnly = True
        Me.TBProtLength.Size = New System.Drawing.Size(100, 20)
        Me.TBProtLength.TabIndex = 11
        '
        'TBMut
        '
        Me.TBMut.Location = New System.Drawing.Point(138, 98)
        Me.TBMut.Name = "TBMut"
        Me.TBMut.Size = New System.Drawing.Size(100, 20)
        Me.TBMut.TabIndex = 10
        '
        'TBCrossover
        '
        Me.TBCrossover.Location = New System.Drawing.Point(138, 73)
        Me.TBCrossover.Name = "TBCrossover"
        Me.TBCrossover.Size = New System.Drawing.Size(100, 20)
        Me.TBCrossover.TabIndex = 9
        '
        'TBElite
        '
        Me.TBElite.Location = New System.Drawing.Point(138, 48)
        Me.TBElite.Name = "TBElite"
        Me.TBElite.Size = New System.Drawing.Size(100, 20)
        Me.TBElite.TabIndex = 8
        '
        'TBPopSize
        '
        Me.TBPopSize.Location = New System.Drawing.Point(138, 23)
        Me.TBPopSize.Name = "TBPopSize"
        Me.TBPopSize.Size = New System.Drawing.Size(100, 20)
        Me.TBPopSize.TabIndex = 7
        '
        'BtnStart
        '
        Me.BtnStart.Location = New System.Drawing.Point(27, 158)
        Me.BtnStart.Name = "BtnStart"
        Me.BtnStart.Size = New System.Drawing.Size(105, 23)
        Me.BtnStart.TabIndex = 8
        Me.BtnStart.Text = "Start"
        Me.BtnStart.UseVisualStyleBackColor = True
        '
        'PBProtien
        '
        Me.PBProtien.BackColor = System.Drawing.SystemColors.Window
        Me.PBProtien.Location = New System.Drawing.Point(27, 205)
        Me.PBProtien.Name = "PBProtien"
        Me.PBProtien.Size = New System.Drawing.Size(686, 434)
        Me.PBProtien.TabIndex = 11
        Me.PBProtien.TabStop = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.TBProtArray)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.TBProtStruct)
        Me.GroupBox2.Controls.Add(Me.LblProtStruct)
        Me.GroupBox2.Location = New System.Drawing.Point(547, 13)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(421, 128)
        Me.GroupBox2.TabIndex = 14
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "GroupBox2"
        '
        'TBProtArray
        '
        Me.TBProtArray.Location = New System.Drawing.Point(6, 101)
        Me.TBProtArray.MaxLength = 64
        Me.TBProtArray.Name = "TBProtArray"
        Me.TBProtArray.ReadOnly = True
        Me.TBProtArray.Size = New System.Drawing.Size(409, 20)
        Me.TBProtArray.TabIndex = 17
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(10, 80)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(67, 13)
        Me.Label1.TabIndex = 16
        Me.Label1.Text = "Protien Array"
        '
        'TBProtStruct
        '
        Me.TBProtStruct.Location = New System.Drawing.Point(6, 48)
        Me.TBProtStruct.MaxLength = 64
        Me.TBProtStruct.Name = "TBProtStruct"
        Me.TBProtStruct.Size = New System.Drawing.Size(409, 20)
        Me.TBProtStruct.TabIndex = 15
        '
        'LblProtStruct
        '
        Me.LblProtStruct.AutoSize = True
        Me.LblProtStruct.Location = New System.Drawing.Point(10, 27)
        Me.LblProtStruct.Name = "LblProtStruct"
        Me.LblProtStruct.Size = New System.Drawing.Size(86, 13)
        Me.LblProtStruct.TabIndex = 14
        Me.LblProtStruct.Text = "Protien Structure"
        '
        'ProtienSynth
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(984, 711)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.PBProtien)
        Me.Controls.Add(Me.BtnStart)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "ProtienSynth"
        Me.RightToLeftLayout = True
        Me.Text = "Protein Synthesizer"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.PBProtien, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents LblPopsize As Label
    Friend WithEvents LblElit As Label
    Friend WithEvents LblCross As Label
    Friend WithEvents LblMut As Label
    Friend WithEvents Lbltarget As Label
    Friend WithEvents Lblprot As Label
    Friend WithEvents LblMax As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents TBMax As TextBox
    Friend WithEvents TBTarget As TextBox
    Friend WithEvents TBProtLength As TextBox
    Friend WithEvents TBMut As TextBox
    Friend WithEvents TBCrossover As TextBox
    Friend WithEvents TBElite As TextBox
    Friend WithEvents TBPopSize As TextBox
    Friend WithEvents BtnStart As Button
    Friend WithEvents PBProtien As PictureBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents TBProtStruct As TextBox
    Friend WithEvents LblProtStruct As Label
    Friend WithEvents TBProtArray As TextBox
    Friend WithEvents Label1 As Label
End Class
