Imports System.IO

Public Class BJCARenameForm
    Inherits System.Windows.Forms.Form

    Public RenameOK As Boolean
    Public RenamingFile As Boolean = False

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        RenameOK = False
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents RenameLabelRForm As System.Windows.Forms.Label
    Friend WithEvents CurrentNameBoxRForm As System.Windows.Forms.TextBox
    Friend WithEvents NewNameBoxRForm As System.Windows.Forms.TextBox
    Friend WithEvents OKButtonRForm As System.Windows.Forms.Button
    Friend WithEvents CancelButtonRForm As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(BJCARenameForm))
        Me.RenameLabelRForm = New System.Windows.Forms.Label
        Me.CurrentNameBoxRForm = New System.Windows.Forms.TextBox
        Me.NewNameBoxRForm = New System.Windows.Forms.TextBox
        Me.OKButtonRForm = New System.Windows.Forms.Button
        Me.CancelButtonRForm = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'RenameLabelRForm
        '
        Me.RenameLabelRForm.Location = New System.Drawing.Point(112, 32)
        Me.RenameLabelRForm.Name = "RenameLabelRForm"
        Me.RenameLabelRForm.Size = New System.Drawing.Size(192, 16)
        Me.RenameLabelRForm.TabIndex = 0
        '
        'CurrentNameBoxRForm
        '
        Me.CurrentNameBoxRForm.Enabled = False
        Me.CurrentNameBoxRForm.Location = New System.Drawing.Point(32, 56)
        Me.CurrentNameBoxRForm.Name = "CurrentNameBoxRForm"
        Me.CurrentNameBoxRForm.Size = New System.Drawing.Size(360, 20)
        Me.CurrentNameBoxRForm.TabIndex = 1
        Me.CurrentNameBoxRForm.Text = ""
        Me.CurrentNameBoxRForm.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'NewNameBoxRForm
        '
        Me.NewNameBoxRForm.Location = New System.Drawing.Point(32, 96)
        Me.NewNameBoxRForm.Name = "NewNameBoxRForm"
        Me.NewNameBoxRForm.Size = New System.Drawing.Size(360, 20)
        Me.NewNameBoxRForm.TabIndex = 0
        Me.NewNameBoxRForm.Text = ""
        Me.NewNameBoxRForm.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'OKButtonRForm
        '
        Me.OKButtonRForm.Location = New System.Drawing.Point(112, 136)
        Me.OKButtonRForm.Name = "OKButtonRForm"
        Me.OKButtonRForm.Size = New System.Drawing.Size(72, 32)
        Me.OKButtonRForm.TabIndex = 3
        Me.OKButtonRForm.Text = "OK"
        '
        'CancelButtonRForm
        '
        Me.CancelButtonRForm.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.CancelButtonRForm.Location = New System.Drawing.Point(240, 136)
        Me.CancelButtonRForm.Name = "CancelButtonRForm"
        Me.CancelButtonRForm.Size = New System.Drawing.Size(72, 32)
        Me.CancelButtonRForm.TabIndex = 4
        Me.CancelButtonRForm.Text = "Cancel"
        '
        'BJCARenameForm
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(424, 198)
        Me.Controls.Add(Me.CancelButtonRForm)
        Me.Controls.Add(Me.OKButtonRForm)
        Me.Controls.Add(Me.NewNameBoxRForm)
        Me.Controls.Add(Me.CurrentNameBoxRForm)
        Me.Controls.Add(Me.RenameLabelRForm)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "BJCARenameForm"
        Me.Text = "Rename"
        Me.ResumeLayout(False)

    End Sub

#End Region

#Region " Methods "

    Private Sub RenameForm_ShowDialog(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.VisibleChanged
        If RenamingFile Then
            RenameLabelRForm.Text = "Please enter a new File Set name:"
        Else
            RenameLabelRForm.Text = "Please enter a new name for rule:"
        End If
    End Sub

    Private Sub CurrentNameBoxRForm_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CurrentNameBoxRForm.TextChanged
        NewNameBoxRForm.Text = CurrentNameBoxRForm.Text
    End Sub

    Private Sub OKButtonRForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OKButtonRForm.Click
        If RenamingFile Then
            Try
                Dim fileStream As New FileStream(NewNameBoxRForm.Text, FileMode.Create)
                fileStream.Close()
                File.Delete(NewNameBoxRForm.Text)
                RenameOK = True
            Catch
                MsgBox(NewNameBoxRForm.Text + " is not a valid filename")
                RenameOK = False
            End Try
        Else
            RenameOK = True
        End If
        Close()
    End Sub

    Private Sub CancelButtonRForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CancelButtonRForm.Click
        RenameOK = False
        Close()
    End Sub

#End Region

End Class
