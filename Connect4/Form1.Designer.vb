﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.QuitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PlayerVCPUToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.CPUVPlayerToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.PlayerVPlayerToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.tbDisplay = New System.Windows.Forms.TextBox()
        Me.tbNotes = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.PlayerVCPUToolStripMenuItem1, Me.CPUVPlayerToolStripMenuItem1, Me.PlayerVPlayerToolStripMenuItem1})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1381, 24)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.QuitToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'QuitToolStripMenuItem
        '
        Me.QuitToolStripMenuItem.Name = "QuitToolStripMenuItem"
        Me.QuitToolStripMenuItem.Size = New System.Drawing.Size(97, 22)
        Me.QuitToolStripMenuItem.Text = "Quit"
        '
        'PlayerVCPUToolStripMenuItem1
        '
        Me.PlayerVCPUToolStripMenuItem1.Name = "PlayerVCPUToolStripMenuItem1"
        Me.PlayerVCPUToolStripMenuItem1.Size = New System.Drawing.Size(111, 20)
        Me.PlayerVCPUToolStripMenuItem1.Text = "Player O  v CPU X"
        '
        'CPUVPlayerToolStripMenuItem1
        '
        Me.CPUVPlayerToolStripMenuItem1.Name = "CPUVPlayerToolStripMenuItem1"
        Me.CPUVPlayerToolStripMenuItem1.Size = New System.Drawing.Size(108, 20)
        Me.CPUVPlayerToolStripMenuItem1.Text = "CPU O v Player X"
        '
        'PlayerVPlayerToolStripMenuItem1
        '
        Me.PlayerVPlayerToolStripMenuItem1.Name = "PlayerVPlayerToolStripMenuItem1"
        Me.PlayerVPlayerToolStripMenuItem1.Size = New System.Drawing.Size(117, 20)
        Me.PlayerVPlayerToolStripMenuItem1.Text = "Player O v Player X"
        '
        'tbDisplay
        '
        Me.tbDisplay.Font = New System.Drawing.Font("Courier New", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbDisplay.Location = New System.Drawing.Point(13, 28)
        Me.tbDisplay.Multiline = True
        Me.tbDisplay.Name = "tbDisplay"
        Me.tbDisplay.ReadOnly = True
        Me.tbDisplay.Size = New System.Drawing.Size(365, 273)
        Me.tbDisplay.TabIndex = 1
        '
        'tbNotes
        '
        Me.tbNotes.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbNotes.Font = New System.Drawing.Font("Courier New", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbNotes.Location = New System.Drawing.Point(384, 28)
        Me.tbNotes.Multiline = True
        Me.tbNotes.Name = "tbNotes"
        Me.tbNotes.ReadOnly = True
        Me.tbNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.tbNotes.Size = New System.Drawing.Size(985, 555)
        Me.tbNotes.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 318)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(0, 13)
        Me.Label1.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(13, 348)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(0, 13)
        Me.Label2.TabIndex = 4
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1381, 595)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.tbNotes)
        Me.Controls.Add(Me.tbDisplay)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Form1"
        Me.Text = "Connect4"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents FileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents QuitToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents tbDisplay As TextBox
    Friend WithEvents PlayerVCPUToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents CPUVPlayerToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents PlayerVPlayerToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents tbNotes As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
End Class
