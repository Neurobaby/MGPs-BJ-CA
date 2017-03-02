Public Class BJCATextForm
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
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
    Friend WithEvents RichTextBoxTextForm As System.Windows.Forms.RichTextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(BJCATextForm))
        Me.RichTextBoxTextForm = New System.Windows.Forms.RichTextBox
        Me.SuspendLayout()
        '
        'RichTextBoxTextForm
        '
        Me.RichTextBoxTextForm.Font = New System.Drawing.Font("Courier New", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RichTextBoxTextForm.Location = New System.Drawing.Point(8, 8)
        Me.RichTextBoxTextForm.Name = "RichTextBoxTextForm"
        Me.RichTextBoxTextForm.ReadOnly = True
        Me.RichTextBoxTextForm.Size = New System.Drawing.Size(840, 536)
        Me.RichTextBoxTextForm.TabIndex = 0
        Me.RichTextBoxTextForm.Text = ""
        '
        'BJCATextForm
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.ClientSize = New System.Drawing.Size(856, 552)
        Me.Controls.Add(Me.RichTextBoxTextForm)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "BJCATextForm"
        Me.Text = "MGP's Blackjack Combinatorial Analyzer"
        Me.ResumeLayout(False)

    End Sub

#End Region

End Class
