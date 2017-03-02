Public Class BJCAAboutForm
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
    Friend WithEvents CopyrightLabelATab As System.Windows.Forms.Label
    Friend WithEvents VersionLabelATab As System.Windows.Forms.Label
    Friend WithEvents TitleLabelATab As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(BJCAAboutForm))
        Me.CopyrightLabelATab = New System.Windows.Forms.Label
        Me.VersionLabelATab = New System.Windows.Forms.Label
        Me.TitleLabelATab = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'CopyrightLabelATab
        '
        Me.CopyrightLabelATab.Location = New System.Drawing.Point(88, 144)
        Me.CopyrightLabelATab.Name = "CopyrightLabelATab"
        Me.CopyrightLabelATab.Size = New System.Drawing.Size(136, 18)
        Me.CopyrightLabelATab.TabIndex = 5
        Me.CopyrightLabelATab.Text = "Copyright 2006-8"
        Me.CopyrightLabelATab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'VersionLabelATab
        '
        Me.VersionLabelATab.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.VersionLabelATab.Location = New System.Drawing.Point(88, 104)
        Me.VersionLabelATab.Name = "VersionLabelATab"
        Me.VersionLabelATab.Size = New System.Drawing.Size(136, 18)
        Me.VersionLabelATab.TabIndex = 4
        Me.VersionLabelATab.Text = "Version 2.0"
        Me.VersionLabelATab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TitleLabelATab
        '
        Me.TitleLabelATab.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TitleLabelATab.Location = New System.Drawing.Point(88, 72)
        Me.TitleLabelATab.Name = "TitleLabelATab"
        Me.TitleLabelATab.Size = New System.Drawing.Size(136, 28)
        Me.TitleLabelATab.TabIndex = 3
        Me.TitleLabelATab.Text = "MGP's BJ CA"
        Me.TitleLabelATab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'BJCAAboutForm
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.ClientSize = New System.Drawing.Size(314, 260)
        Me.Controls.Add(Me.CopyrightLabelATab)
        Me.Controls.Add(Me.VersionLabelATab)
        Me.Controls.Add(Me.TitleLabelATab)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "BJCAAboutForm"
        Me.Text = "MGP's Blackjack Combinatorial Analyzer"
        Me.ResumeLayout(False)

    End Sub

#End Region

End Class
