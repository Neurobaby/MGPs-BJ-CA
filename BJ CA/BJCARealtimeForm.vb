Imports BJ_CA.BJCAShared

Public Class BJCARealtimeForm
    Inherits System.Windows.Forms.Form

#Region " Declarations "
    Dim InsPays As Double

    Public FileNames As New BJCAFileSetClass
    Public OriginalRules As New BJCARulesClass
    Public Rules As New BJCARulesClass
    Public DealerProbsRT As BJCADealerProbsDictionary
    Public UseSPL1Estimate As Boolean
    Public SPL1Estimate As Double

    Public OriginalEV As Double
    Public CurrentEV As Double
    Public ClearingForm As Boolean

    Public OriginalShoe As New BJCAShoeClass
    Public CurrentShoe As New BJCAShoeClass
    Public CurrentHandShoe As New BJCAShoeClass
    Private OriginalSPL As Integer
    Private CurrentSPL As Integer
    Private CurrentPaircard As Integer
    Private CurrentPHandIndex As Integer

    Private CurrentUpcard As Integer
    Private CurrentDealerHand As New BJCAHandClass
    Private DealersHandString As String
    Private DealersHand As IndexedTextBox
    Friend WithEvents DealersHandHandler As System.Windows.Forms.TextBox

    Private NetPlayerHand As New BJCAHandClass
    Private CurrentPlayerHand As New BJCAHandClass
    Private PlayerHandsStrings(4) As String
    Private PlayerHands(4) As IndexedTextBox
    Private PlayerHandEVs(4) As BJCARealtimeHandClass
    Private PlayerHandEVBoxes(4) As IndexedTextBox
    Private PlayerHandTotalBoxes(4) As IndexedTextBox
    Private PlayerHandStratBoxes(4) As IndexedTextBox
    Friend WithEvents PlayerHandsArrayHandler As System.Windows.Forms.TextBox

    Private NetOtherPlayersHands As New BJCAHandClass
    Private CurrentOthersHand As New BJCAHandClass
    Private OtherPlayersHandsStrings(6) As String
    Private OtherPlayersHands(6) As IndexedTextBox
    Friend WithEvents OtherPlayersHandsArrayHandler As System.Windows.Forms.TextBox

    Private CurrentShoeBoxesArray(10) As IndexedTextBox
    Private CurrentShoeButtonsArray(10, 2) As IndexedButton
    Friend WithEvents CurrentShoeBoxesArrayHandler As System.Windows.Forms.TextBox
    Friend WithEvents CurrentShoeButtonsArrayHandler As System.Windows.Forms.Button

#End Region

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        InitializeForm()

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
    Friend WithEvents ShoeChangesLabelRTForm As System.Windows.Forms.Label
    Friend WithEvents HandShoeButtonRTForm As System.Windows.Forms.Button
    Friend WithEvents CTLabelRTForm As System.Windows.Forms.Label
    Friend WithEvents C2LabelRTForm As System.Windows.Forms.Label
    Friend WithEvents C3LabelRTForm As System.Windows.Forms.Label
    Friend WithEvents C9LabelRTForm As System.Windows.Forms.Label
    Friend WithEvents C4LabelRTForm As System.Windows.Forms.Label
    Friend WithEvents C5LabelRTForm As System.Windows.Forms.Label
    Friend WithEvents C6LabelRTForm As System.Windows.Forms.Label
    Friend WithEvents C7LabelRTForm As System.Windows.Forms.Label
    Friend WithEvents C8LabelRTForm As System.Windows.Forms.Label
    Friend WithEvents CALabelRTForm As System.Windows.Forms.Label
    Friend WithEvents ShoeLabelRTForm As System.Windows.Forms.Label
    Friend WithEvents SplitButtonRTForm As System.Windows.Forms.Button
    Friend WithEvents NewHandButtonRTForm As System.Windows.Forms.Button
    Friend WithEvents OPHandsLabelRTForm As System.Windows.Forms.Label
    Friend WithEvents StratLabelRTForm As System.Windows.Forms.Label
    Friend WithEvents SplitLabelRTForm As System.Windows.Forms.Label
    Friend WithEvents HitLabelRTForm As System.Windows.Forms.Label
    Friend WithEvents SurrLabelRTForm As System.Windows.Forms.Label
    Friend WithEvents DoubleLabelRTForm As System.Windows.Forms.Label
    Friend WithEvents StandLabelRTForm As System.Windows.Forms.Label
    Friend WithEvents PHandLabelRTForm As System.Windows.Forms.Label
    Friend WithEvents ResetShoeCheckRTForm As System.Windows.Forms.CheckBox
    Friend WithEvents OriginalShoeButtonRTForm As System.Windows.Forms.Button
    Friend WithEvents DHandLabelRTForm As System.Windows.Forms.Label
    Friend WithEvents InsuranceEVBoxRTForm As System.Windows.Forms.TextBox
    Friend WithEvents InsuranceLabelRTForm As System.Windows.Forms.Label
    Friend WithEvents InsuranceBoxRTForm As System.Windows.Forms.TextBox
    Friend WithEvents SplitBoxRTForm As System.Windows.Forms.TextBox
    Friend WithEvents NewEVLabelRTForm As System.Windows.Forms.Label
    Friend WithEvents NewEVBoxRTForm As System.Windows.Forms.TextBox
    Friend WithEvents StratBoxRTForm As System.Windows.Forms.TextBox
    Friend WithEvents StandBoxRTForm As System.Windows.Forms.TextBox
    Friend WithEvents DoubleBoxRTForm As System.Windows.Forms.TextBox
    Friend WithEvents HitBoxRTForm As System.Windows.Forms.TextBox
    Friend WithEvents SurrBoxRTForm As System.Windows.Forms.TextBox
    Friend WithEvents SplitsLabelRTForm As System.Windows.Forms.Label
    Friend WithEvents SuitedHandComboBoxRTForm As System.Windows.Forms.ComboBox
    Friend WithEvents TLabelRTForm As System.Windows.Forms.Label
    Friend WithEvents ALabelRTForm As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(BJCARealtimeForm))
        Me.ShoeChangesLabelRTForm = New System.Windows.Forms.Label
        Me.HandShoeButtonRTForm = New System.Windows.Forms.Button
        Me.CTLabelRTForm = New System.Windows.Forms.Label
        Me.C2LabelRTForm = New System.Windows.Forms.Label
        Me.C3LabelRTForm = New System.Windows.Forms.Label
        Me.C9LabelRTForm = New System.Windows.Forms.Label
        Me.C4LabelRTForm = New System.Windows.Forms.Label
        Me.C5LabelRTForm = New System.Windows.Forms.Label
        Me.C6LabelRTForm = New System.Windows.Forms.Label
        Me.C7LabelRTForm = New System.Windows.Forms.Label
        Me.C8LabelRTForm = New System.Windows.Forms.Label
        Me.CALabelRTForm = New System.Windows.Forms.Label
        Me.ShoeLabelRTForm = New System.Windows.Forms.Label
        Me.SplitButtonRTForm = New System.Windows.Forms.Button
        Me.NewHandButtonRTForm = New System.Windows.Forms.Button
        Me.OPHandsLabelRTForm = New System.Windows.Forms.Label
        Me.StratLabelRTForm = New System.Windows.Forms.Label
        Me.StandBoxRTForm = New System.Windows.Forms.TextBox
        Me.DoubleBoxRTForm = New System.Windows.Forms.TextBox
        Me.SurrBoxRTForm = New System.Windows.Forms.TextBox
        Me.SplitBoxRTForm = New System.Windows.Forms.TextBox
        Me.HitBoxRTForm = New System.Windows.Forms.TextBox
        Me.StratBoxRTForm = New System.Windows.Forms.TextBox
        Me.SplitLabelRTForm = New System.Windows.Forms.Label
        Me.HitLabelRTForm = New System.Windows.Forms.Label
        Me.SurrLabelRTForm = New System.Windows.Forms.Label
        Me.DoubleLabelRTForm = New System.Windows.Forms.Label
        Me.StandLabelRTForm = New System.Windows.Forms.Label
        Me.PHandLabelRTForm = New System.Windows.Forms.Label
        Me.ResetShoeCheckRTForm = New System.Windows.Forms.CheckBox
        Me.OriginalShoeButtonRTForm = New System.Windows.Forms.Button
        Me.DHandLabelRTForm = New System.Windows.Forms.Label
        Me.InsuranceEVBoxRTForm = New System.Windows.Forms.TextBox
        Me.InsuranceLabelRTForm = New System.Windows.Forms.Label
        Me.InsuranceBoxRTForm = New System.Windows.Forms.TextBox
        Me.NewEVLabelRTForm = New System.Windows.Forms.Label
        Me.NewEVBoxRTForm = New System.Windows.Forms.TextBox
        Me.SplitsLabelRTForm = New System.Windows.Forms.Label
        Me.SuitedHandComboBoxRTForm = New System.Windows.Forms.ComboBox
        Me.TLabelRTForm = New System.Windows.Forms.Label
        Me.ALabelRTForm = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'ShoeChangesLabelRTForm
        '
        Me.ShoeChangesLabelRTForm.Location = New System.Drawing.Point(384, 498)
        Me.ShoeChangesLabelRTForm.Name = "ShoeChangesLabelRTForm"
        Me.ShoeChangesLabelRTForm.Size = New System.Drawing.Size(211, 37)
        Me.ShoeChangesLabelRTForm.TabIndex = 157
        Me.ShoeChangesLabelRTForm.Text = "*Note: Press New Hand to save shoe changes made with buttons."
        '
        'HandShoeButtonRTForm
        '
        Me.HandShoeButtonRTForm.Location = New System.Drawing.Point(470, 222)
        Me.HandShoeButtonRTForm.Name = "HandShoeButtonRTForm"
        Me.HandShoeButtonRTForm.Size = New System.Drawing.Size(106, 27)
        Me.HandShoeButtonRTForm.TabIndex = 156
        Me.HandShoeButtonRTForm.Text = "Previous Shoe"
        '
        'CTLabelRTForm
        '
        Me.CTLabelRTForm.Location = New System.Drawing.Point(475, 450)
        Me.CTLabelRTForm.Name = "CTLabelRTForm"
        Me.CTLabelRTForm.Size = New System.Drawing.Size(15, 20)
        Me.CTLabelRTForm.TabIndex = 155
        Me.CTLabelRTForm.Text = "T"
        '
        'C2LabelRTForm
        '
        Me.C2LabelRTForm.Location = New System.Drawing.Point(360, 330)
        Me.C2LabelRTForm.Name = "C2LabelRTForm"
        Me.C2LabelRTForm.Size = New System.Drawing.Size(15, 20)
        Me.C2LabelRTForm.TabIndex = 154
        Me.C2LabelRTForm.Text = "2"
        '
        'C3LabelRTForm
        '
        Me.C3LabelRTForm.Location = New System.Drawing.Point(360, 370)
        Me.C3LabelRTForm.Name = "C3LabelRTForm"
        Me.C3LabelRTForm.Size = New System.Drawing.Size(15, 20)
        Me.C3LabelRTForm.TabIndex = 153
        Me.C3LabelRTForm.Text = "3"
        '
        'C9LabelRTForm
        '
        Me.C9LabelRTForm.Location = New System.Drawing.Point(475, 410)
        Me.C9LabelRTForm.Name = "C9LabelRTForm"
        Me.C9LabelRTForm.Size = New System.Drawing.Size(15, 20)
        Me.C9LabelRTForm.TabIndex = 152
        Me.C9LabelRTForm.Text = "9"
        '
        'C4LabelRTForm
        '
        Me.C4LabelRTForm.Location = New System.Drawing.Point(360, 410)
        Me.C4LabelRTForm.Name = "C4LabelRTForm"
        Me.C4LabelRTForm.Size = New System.Drawing.Size(15, 20)
        Me.C4LabelRTForm.TabIndex = 151
        Me.C4LabelRTForm.Text = "4"
        '
        'C5LabelRTForm
        '
        Me.C5LabelRTForm.Location = New System.Drawing.Point(360, 450)
        Me.C5LabelRTForm.Name = "C5LabelRTForm"
        Me.C5LabelRTForm.Size = New System.Drawing.Size(15, 20)
        Me.C5LabelRTForm.TabIndex = 150
        Me.C5LabelRTForm.Text = "5"
        '
        'C6LabelRTForm
        '
        Me.C6LabelRTForm.Location = New System.Drawing.Point(475, 290)
        Me.C6LabelRTForm.Name = "C6LabelRTForm"
        Me.C6LabelRTForm.Size = New System.Drawing.Size(15, 20)
        Me.C6LabelRTForm.TabIndex = 149
        Me.C6LabelRTForm.Text = "6"
        '
        'C7LabelRTForm
        '
        Me.C7LabelRTForm.Location = New System.Drawing.Point(475, 330)
        Me.C7LabelRTForm.Name = "C7LabelRTForm"
        Me.C7LabelRTForm.Size = New System.Drawing.Size(15, 20)
        Me.C7LabelRTForm.TabIndex = 148
        Me.C7LabelRTForm.Text = "7"
        '
        'C8LabelRTForm
        '
        Me.C8LabelRTForm.Location = New System.Drawing.Point(475, 370)
        Me.C8LabelRTForm.Name = "C8LabelRTForm"
        Me.C8LabelRTForm.Size = New System.Drawing.Size(15, 20)
        Me.C8LabelRTForm.TabIndex = 147
        Me.C8LabelRTForm.Text = "8"
        '
        'CALabelRTForm
        '
        Me.CALabelRTForm.Location = New System.Drawing.Point(360, 290)
        Me.CALabelRTForm.Name = "CALabelRTForm"
        Me.CALabelRTForm.Size = New System.Drawing.Size(15, 20)
        Me.CALabelRTForm.TabIndex = 146
        Me.CALabelRTForm.Text = "A"
        '
        'ShoeLabelRTForm
        '
        Me.ShoeLabelRTForm.Location = New System.Drawing.Point(432, 258)
        Me.ShoeLabelRTForm.Name = "ShoeLabelRTForm"
        Me.ShoeLabelRTForm.Size = New System.Drawing.Size(86, 19)
        Me.ShoeLabelRTForm.TabIndex = 145
        Me.ShoeLabelRTForm.Text = "Current Shoe"
        '
        'SplitButtonRTForm
        '
        Me.SplitButtonRTForm.Location = New System.Drawing.Point(0, 92)
        Me.SplitButtonRTForm.Name = "SplitButtonRTForm"
        Me.SplitButtonRTForm.Size = New System.Drawing.Size(112, 28)
        Me.SplitButtonRTForm.TabIndex = 144
        Me.SplitButtonRTForm.Text = "Split"
        Me.SplitButtonRTForm.Visible = False
        '
        'NewHandButtonRTForm
        '
        Me.NewHandButtonRTForm.Location = New System.Drawing.Point(136, 224)
        Me.NewHandButtonRTForm.Name = "NewHandButtonRTForm"
        Me.NewHandButtonRTForm.Size = New System.Drawing.Size(128, 27)
        Me.NewHandButtonRTForm.TabIndex = 143
        Me.NewHandButtonRTForm.Text = "New Hand"
        '
        'OPHandsLabelRTForm
        '
        Me.OPHandsLabelRTForm.Location = New System.Drawing.Point(298, 18)
        Me.OPHandsLabelRTForm.Name = "OPHandsLabelRTForm"
        Me.OPHandsLabelRTForm.Size = New System.Drawing.Size(134, 19)
        Me.OPHandsLabelRTForm.TabIndex = 142
        Me.OPHandsLabelRTForm.Text = "Other Players' Hands"
        '
        'StratLabelRTForm
        '
        Me.StratLabelRTForm.Location = New System.Drawing.Point(67, 268)
        Me.StratLabelRTForm.Name = "StratLabelRTForm"
        Me.StratLabelRTForm.Size = New System.Drawing.Size(77, 18)
        Me.StratLabelRTForm.TabIndex = 140
        Me.StratLabelRTForm.Text = "Strategy"
        '
        'StandBoxRTForm
        '
        Me.StandBoxRTForm.Location = New System.Drawing.Point(163, 295)
        Me.StandBoxRTForm.Name = "StandBoxRTForm"
        Me.StandBoxRTForm.ReadOnly = True
        Me.StandBoxRTForm.Size = New System.Drawing.Size(77, 22)
        Me.StandBoxRTForm.TabIndex = 139
        Me.StandBoxRTForm.TabStop = False
        Me.StandBoxRTForm.Text = ""
        Me.StandBoxRTForm.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'DoubleBoxRTForm
        '
        Me.DoubleBoxRTForm.Location = New System.Drawing.Point(163, 351)
        Me.DoubleBoxRTForm.Name = "DoubleBoxRTForm"
        Me.DoubleBoxRTForm.ReadOnly = True
        Me.DoubleBoxRTForm.Size = New System.Drawing.Size(77, 22)
        Me.DoubleBoxRTForm.TabIndex = 138
        Me.DoubleBoxRTForm.TabStop = False
        Me.DoubleBoxRTForm.Text = ""
        Me.DoubleBoxRTForm.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'SurrBoxRTForm
        '
        Me.SurrBoxRTForm.Location = New System.Drawing.Point(163, 378)
        Me.SurrBoxRTForm.Name = "SurrBoxRTForm"
        Me.SurrBoxRTForm.ReadOnly = True
        Me.SurrBoxRTForm.Size = New System.Drawing.Size(77, 22)
        Me.SurrBoxRTForm.TabIndex = 137
        Me.SurrBoxRTForm.TabStop = False
        Me.SurrBoxRTForm.Text = ""
        Me.SurrBoxRTForm.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'SplitBoxRTForm
        '
        Me.SplitBoxRTForm.Location = New System.Drawing.Point(163, 406)
        Me.SplitBoxRTForm.Name = "SplitBoxRTForm"
        Me.SplitBoxRTForm.ReadOnly = True
        Me.SplitBoxRTForm.Size = New System.Drawing.Size(77, 22)
        Me.SplitBoxRTForm.TabIndex = 136
        Me.SplitBoxRTForm.TabStop = False
        Me.SplitBoxRTForm.Text = ""
        Me.SplitBoxRTForm.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'HitBoxRTForm
        '
        Me.HitBoxRTForm.Location = New System.Drawing.Point(163, 323)
        Me.HitBoxRTForm.Name = "HitBoxRTForm"
        Me.HitBoxRTForm.ReadOnly = True
        Me.HitBoxRTForm.Size = New System.Drawing.Size(77, 22)
        Me.HitBoxRTForm.TabIndex = 135
        Me.HitBoxRTForm.TabStop = False
        Me.HitBoxRTForm.Text = ""
        Me.HitBoxRTForm.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'StratBoxRTForm
        '
        Me.StratBoxRTForm.Location = New System.Drawing.Point(163, 268)
        Me.StratBoxRTForm.Name = "StratBoxRTForm"
        Me.StratBoxRTForm.ReadOnly = True
        Me.StratBoxRTForm.Size = New System.Drawing.Size(77, 22)
        Me.StratBoxRTForm.TabIndex = 134
        Me.StratBoxRTForm.TabStop = False
        Me.StratBoxRTForm.Text = ""
        Me.StratBoxRTForm.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'SplitLabelRTForm
        '
        Me.SplitLabelRTForm.Location = New System.Drawing.Point(67, 406)
        Me.SplitLabelRTForm.Name = "SplitLabelRTForm"
        Me.SplitLabelRTForm.Size = New System.Drawing.Size(58, 19)
        Me.SplitLabelRTForm.TabIndex = 133
        Me.SplitLabelRTForm.Text = "Split EV"
        '
        'HitLabelRTForm
        '
        Me.HitLabelRTForm.Location = New System.Drawing.Point(67, 323)
        Me.HitLabelRTForm.Name = "HitLabelRTForm"
        Me.HitLabelRTForm.Size = New System.Drawing.Size(58, 19)
        Me.HitLabelRTForm.TabIndex = 132
        Me.HitLabelRTForm.Text = "Hit EV"
        '
        'SurrLabelRTForm
        '
        Me.SurrLabelRTForm.Location = New System.Drawing.Point(67, 378)
        Me.SurrLabelRTForm.Name = "SurrLabelRTForm"
        Me.SurrLabelRTForm.Size = New System.Drawing.Size(96, 19)
        Me.SurrLabelRTForm.TabIndex = 131
        Me.SurrLabelRTForm.Text = "Surrender EV"
        '
        'DoubleLabelRTForm
        '
        Me.DoubleLabelRTForm.Location = New System.Drawing.Point(67, 351)
        Me.DoubleLabelRTForm.Name = "DoubleLabelRTForm"
        Me.DoubleLabelRTForm.Size = New System.Drawing.Size(96, 18)
        Me.DoubleLabelRTForm.TabIndex = 130
        Me.DoubleLabelRTForm.Text = "Double EV"
        '
        'StandLabelRTForm
        '
        Me.StandLabelRTForm.Location = New System.Drawing.Point(67, 295)
        Me.StandLabelRTForm.Name = "StandLabelRTForm"
        Me.StandLabelRTForm.Size = New System.Drawing.Size(96, 19)
        Me.StandLabelRTForm.TabIndex = 129
        Me.StandLabelRTForm.Text = "Stand EV"
        '
        'PHandLabelRTForm
        '
        Me.PHandLabelRTForm.Location = New System.Drawing.Point(10, 74)
        Me.PHandLabelRTForm.Name = "PHandLabelRTForm"
        Me.PHandLabelRTForm.Size = New System.Drawing.Size(105, 18)
        Me.PHandLabelRTForm.TabIndex = 128
        Me.PHandLabelRTForm.Text = "Player's Hand(s)"
        '
        'ResetShoeCheckRTForm
        '
        Me.ResetShoeCheckRTForm.Location = New System.Drawing.Point(384, 194)
        Me.ResetShoeCheckRTForm.Name = "ResetShoeCheckRTForm"
        Me.ResetShoeCheckRTForm.Size = New System.Drawing.Size(173, 18)
        Me.ResetShoeCheckRTForm.TabIndex = 127
        Me.ResetShoeCheckRTForm.Text = "Reset shoe every hand"
        '
        'OriginalShoeButtonRTForm
        '
        Me.OriginalShoeButtonRTForm.Location = New System.Drawing.Point(355, 222)
        Me.OriginalShoeButtonRTForm.Name = "OriginalShoeButtonRTForm"
        Me.OriginalShoeButtonRTForm.Size = New System.Drawing.Size(106, 27)
        Me.OriginalShoeButtonRTForm.TabIndex = 124
        Me.OriginalShoeButtonRTForm.Text = "Original Shoe"
        '
        'DHandLabelRTForm
        '
        Me.DHandLabelRTForm.Location = New System.Drawing.Point(10, 18)
        Me.DHandLabelRTForm.Name = "DHandLabelRTForm"
        Me.DHandLabelRTForm.Size = New System.Drawing.Size(105, 19)
        Me.DHandLabelRTForm.TabIndex = 123
        Me.DHandLabelRTForm.Text = "Dealer's Hand"
        '
        'InsuranceEVBoxRTForm
        '
        Me.InsuranceEVBoxRTForm.Location = New System.Drawing.Point(163, 434)
        Me.InsuranceEVBoxRTForm.Name = "InsuranceEVBoxRTForm"
        Me.InsuranceEVBoxRTForm.ReadOnly = True
        Me.InsuranceEVBoxRTForm.Size = New System.Drawing.Size(77, 22)
        Me.InsuranceEVBoxRTForm.TabIndex = 159
        Me.InsuranceEVBoxRTForm.TabStop = False
        Me.InsuranceEVBoxRTForm.Text = ""
        Me.InsuranceEVBoxRTForm.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.InsuranceEVBoxRTForm.Visible = False
        '
        'InsuranceLabelRTForm
        '
        Me.InsuranceLabelRTForm.Location = New System.Drawing.Point(67, 434)
        Me.InsuranceLabelRTForm.Name = "InsuranceLabelRTForm"
        Me.InsuranceLabelRTForm.Size = New System.Drawing.Size(87, 18)
        Me.InsuranceLabelRTForm.TabIndex = 158
        Me.InsuranceLabelRTForm.Text = "Insurance EV"
        Me.InsuranceLabelRTForm.Visible = False
        '
        'InsuranceBoxRTForm
        '
        Me.InsuranceBoxRTForm.BackColor = System.Drawing.SystemColors.Control
        Me.InsuranceBoxRTForm.Location = New System.Drawing.Point(250, 268)
        Me.InsuranceBoxRTForm.Name = "InsuranceBoxRTForm"
        Me.InsuranceBoxRTForm.ReadOnly = True
        Me.InsuranceBoxRTForm.Size = New System.Drawing.Size(76, 22)
        Me.InsuranceBoxRTForm.TabIndex = 160
        Me.InsuranceBoxRTForm.TabStop = False
        Me.InsuranceBoxRTForm.Text = ""
        Me.InsuranceBoxRTForm.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.InsuranceBoxRTForm.Visible = False
        '
        'NewEVLabelRTForm
        '
        Me.NewEVLabelRTForm.Location = New System.Drawing.Point(67, 268)
        Me.NewEVLabelRTForm.Name = "NewEVLabelRTForm"
        Me.NewEVLabelRTForm.Size = New System.Drawing.Size(77, 18)
        Me.NewEVLabelRTForm.TabIndex = 162
        Me.NewEVLabelRTForm.Text = "EV of Hand"
        Me.NewEVLabelRTForm.Visible = False
        '
        'NewEVBoxRTForm
        '
        Me.NewEVBoxRTForm.Location = New System.Drawing.Point(163, 268)
        Me.NewEVBoxRTForm.Name = "NewEVBoxRTForm"
        Me.NewEVBoxRTForm.ReadOnly = True
        Me.NewEVBoxRTForm.Size = New System.Drawing.Size(77, 22)
        Me.NewEVBoxRTForm.TabIndex = 161
        Me.NewEVBoxRTForm.TabStop = False
        Me.NewEVBoxRTForm.Text = ""
        Me.NewEVBoxRTForm.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.NewEVBoxRTForm.Visible = False
        '
        'SplitsLabelRTForm
        '
        Me.SplitsLabelRTForm.Location = New System.Drawing.Point(67, 526)
        Me.SplitsLabelRTForm.Name = "SplitsLabelRTForm"
        Me.SplitsLabelRTForm.Size = New System.Drawing.Size(173, 37)
        Me.SplitsLabelRTForm.TabIndex = 163
        Me.SplitsLabelRTForm.Text = "*Note: Splits are calculated using a CDZ- strategy."
        '
        'SuitedHandComboBoxRTForm
        '
        Me.SuitedHandComboBoxRTForm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.SuitedHandComboBoxRTForm.Items.AddRange(New Object() {"Mixed Suits", "Spades", "Hearts", "Diamonds", "Clubs"})
        Me.SuitedHandComboBoxRTForm.Location = New System.Drawing.Point(0, 129)
        Me.SuitedHandComboBoxRTForm.Name = "SuitedHandComboBoxRTForm"
        Me.SuitedHandComboBoxRTForm.Size = New System.Drawing.Size(96, 24)
        Me.SuitedHandComboBoxRTForm.TabIndex = 164
        Me.SuitedHandComboBoxRTForm.Visible = False
        '
        'TLabelRTForm
        '
        Me.TLabelRTForm.Location = New System.Drawing.Point(163, 498)
        Me.TLabelRTForm.Name = "TLabelRTForm"
        Me.TLabelRTForm.Size = New System.Drawing.Size(87, 19)
        Me.TLabelRTForm.TabIndex = 172
        Me.TLabelRTForm.Text = "Ten = T, t, 0"
        '
        'ALabelRTForm
        '
        Me.ALabelRTForm.Location = New System.Drawing.Point(67, 498)
        Me.ALabelRTForm.Name = "ALabelRTForm"
        Me.ALabelRTForm.Size = New System.Drawing.Size(87, 19)
        Me.ALabelRTForm.TabIndex = 171
        Me.ALabelRTForm.Text = "Ace = A, a, 1"
        '
        'BJCARealtimeForm
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(614, 569)
        Me.Controls.Add(Me.TLabelRTForm)
        Me.Controls.Add(Me.ALabelRTForm)
        Me.Controls.Add(Me.SuitedHandComboBoxRTForm)
        Me.Controls.Add(Me.SplitsLabelRTForm)
        Me.Controls.Add(Me.NewEVLabelRTForm)
        Me.Controls.Add(Me.NewEVBoxRTForm)
        Me.Controls.Add(Me.InsuranceBoxRTForm)
        Me.Controls.Add(Me.InsuranceEVBoxRTForm)
        Me.Controls.Add(Me.InsuranceLabelRTForm)
        Me.Controls.Add(Me.ShoeChangesLabelRTForm)
        Me.Controls.Add(Me.HandShoeButtonRTForm)
        Me.Controls.Add(Me.CTLabelRTForm)
        Me.Controls.Add(Me.C2LabelRTForm)
        Me.Controls.Add(Me.C3LabelRTForm)
        Me.Controls.Add(Me.C9LabelRTForm)
        Me.Controls.Add(Me.C4LabelRTForm)
        Me.Controls.Add(Me.C5LabelRTForm)
        Me.Controls.Add(Me.C6LabelRTForm)
        Me.Controls.Add(Me.C7LabelRTForm)
        Me.Controls.Add(Me.C8LabelRTForm)
        Me.Controls.Add(Me.CALabelRTForm)
        Me.Controls.Add(Me.ShoeLabelRTForm)
        Me.Controls.Add(Me.SplitButtonRTForm)
        Me.Controls.Add(Me.NewHandButtonRTForm)
        Me.Controls.Add(Me.OPHandsLabelRTForm)
        Me.Controls.Add(Me.StratLabelRTForm)
        Me.Controls.Add(Me.StandBoxRTForm)
        Me.Controls.Add(Me.DoubleBoxRTForm)
        Me.Controls.Add(Me.SurrBoxRTForm)
        Me.Controls.Add(Me.SplitBoxRTForm)
        Me.Controls.Add(Me.HitBoxRTForm)
        Me.Controls.Add(Me.StratBoxRTForm)
        Me.Controls.Add(Me.SplitLabelRTForm)
        Me.Controls.Add(Me.HitLabelRTForm)
        Me.Controls.Add(Me.SurrLabelRTForm)
        Me.Controls.Add(Me.DoubleLabelRTForm)
        Me.Controls.Add(Me.StandLabelRTForm)
        Me.Controls.Add(Me.PHandLabelRTForm)
        Me.Controls.Add(Me.ResetShoeCheckRTForm)
        Me.Controls.Add(Me.OriginalShoeButtonRTForm)
        Me.Controls.Add(Me.DHandLabelRTForm)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "BJCARealtimeForm"
        Me.Text = "MGP's BJ CA Realtime Analysis"
        Me.ResumeLayout(False)

    End Sub

#End Region

#Region " Initialization "

    Private Sub InitializeForm()
        PopulateShoeTable()
        PopulateShoeButtonsTable()
        PopulateHands()

        InsPays = 2
    End Sub

    Public Sub LoadFormRealtime()
        Dim tempCA As New BJCA
        Dim card As Integer

        If UseSPL1Estimate And Rules.SPL < 2 Then UseSPL1Estimate = False

        OriginalSPL = Rules.SPL
        Rules.ComputeForced = False
        Rules.ComputeTC = False
        Rules.ComputeTD = False
        Rules.CDP = False
        Rules.CDPN = False
        For card = 1 To 10
            Rules.UCAllowed(card) = True
        Next
        If Rules.UseDPDictionary Then
            If Not tempCA.LoadDealerProbsFile(DealerProbsRT, Rules.ProbsPath, (Rules.StandOnSoft = 17), Rules.DeckType, "RT ") Then
                DealerProbsRT = Nothing
            End If
        Else
            DealerProbsRT = Nothing
        End If
        tempCA.BJCA(Rules, DealerProbsRT)
        If Rules.UseDPDictionary And DealerProbsRT Is Nothing Then
            DealerProbsRT = tempCA.DealerProbs
        End If
        OriginalEV = tempCA.Opt.GameEVs.NetGameEV
        CurrentEV = OriginalEV

        If UseSPL1Estimate Then
            Dim tempCA2 As New BJCA

            Rules.SPL = 1
            tempCA2.BJCA(Rules, DealerProbsRT)
            SPL1Estimate = OriginalEV - tempCA.Opt.GameEVs.NetGameEV
            Rules.SPL = OriginalSPL
        End If

        ResetOriginalShoeForm()
        LoadFormEVs()

        tempCA = Nothing
    End Sub

#End Region

#Region " General Methods "

    Private Sub NewHandButtonRTForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewHandButtonRTForm.Click
        Dim tempShoe As New BJCAShoeClass
        Dim tempCA As New BJCA
        Dim card As Integer

        CurrentSPL = 0
        CurrentPHandIndex = 0
        CurrentPaircard = 0

        If ResetShoeCheckRTForm.Checked Then
            ResetOriginalShoeForm()
        Else
            tempShoe.Reset(CurrentHandShoe)
            Rules.Shoe.Reset(CurrentHandShoe)
            For card = 1 To 10
                Rules.UCAllowed(card) = True
                Rules.SplitAllowed(card) = OriginalRules.SplitAllowed(card)
            Next card
            If UseSPL1Estimate Then
                Rules.SPL = 1
            Else
                Rules.SPL = OriginalSPL
            End If

            ClearHandEVs()
            ClearHandsForm()
            tempCA.BJCA(Rules, DealerProbsRT)

            If UseSPL1Estimate Then
                CurrentEV = tempCA.Opt.GameEVs.NetGameEV + SPL1Estimate
            Else
                CurrentEV = tempCA.Opt.GameEVs.NetGameEV
            End If

            CurrentShoe.Reset(tempShoe)
            ResetCurrentShoeForm()
        End If

        tempCA = Nothing
    End Sub

    Private Sub BJCARealtimeForm_Close(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Dim tempCA As New BJCA

        If Rules.SaveDPDictionary Then tempCA.SaveDealerProbsFile(DealerProbsRT, Rules.ProbsPath, (Rules.StandOnSoft = 17), Rules.DeckType, "RT ")
    End Sub

#End Region

#Region " Shoe Controls "

    Private Sub PopulateShoeTable()
        Dim card As Integer

        For card = 0 To 9 Step 1
            Dim box As New IndexedTextBox

            box.ImeMode = System.Windows.Forms.ImeMode.On
            box.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
            box.Size = New System.Drawing.Size(32, 20)
            box.Index = card + 1

            If card < 5 Then
                box.Location = New System.Drawing.Point(380, 290 + 40 * card)
            Else
                box.Location = New System.Drawing.Point(495, 290 + 40 * (card - 5))
            End If

            CurrentShoeBoxesArray(card) = box
            Me.Controls.Add(box)

            'Add Handler to the general handler
            AddHandler box.Validating, AddressOf CurrentShoeBoxesArrayHandler_Validating

        Next card
    End Sub

    Private Sub PopulateShoeButtonsTable()
        Dim card As Integer
        Dim cardbutton As Integer

        For card = 0 To 9 Step 1
            For cardbutton = 0 To 1
                Dim newbutton As New IndexedButton

                newbutton.ImeMode = System.Windows.Forms.ImeMode.On
                newbutton.Size = New System.Drawing.Size(24, 16)
                newbutton.Index = card + 1
                newbutton.Index2 = cardbutton

                If card < 5 Then
                    If cardbutton = 0 Then
                        newbutton.Text = "+"
                        newbutton.Location = New System.Drawing.Point(412, 290 + 40 * card)
                    Else
                        newbutton.Text = "-"
                        newbutton.Location = New System.Drawing.Point(412, 306 + 40 * card)
                    End If
                Else
                    If cardbutton = 0 Then
                        newbutton.Text = "+"
                        newbutton.Location = New System.Drawing.Point(527, 290 + 40 * (card - 5))
                    Else
                        newbutton.Text = "-"
                        newbutton.Location = New System.Drawing.Point(527, 306 + 40 * (card - 5))
                    End If
                End If

                CurrentShoeButtonsArray(card, cardbutton) = newbutton
                Me.Controls.Add(newbutton)

                'Add Handler to the general handler
                AddHandler newbutton.Click, AddressOf CurrentShoeButtonsArrayHandler_Click

            Next cardbutton
        Next card
    End Sub

    Private Sub ResetShoeCheckRTForm_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ResetShoeCheckRTForm.CheckedChanged
        If ResetShoeCheckRTForm.Checked Then
            OriginalShoeButtonRTForm.Visible = False
            HandShoeButtonRTForm.Visible = False
        Else
            OriginalShoeButtonRTForm.Visible = True
            HandShoeButtonRTForm.Visible = True
        End If
    End Sub

    Private Sub GetShoeForm(ByRef shoe As BJCAShoeClass)
        Dim card As Integer

        For card = 1 To 10
            shoe.Cards(card) = CInt(CurrentShoeBoxesArray(card - 1).Text)
        Next card
    End Sub

    Private Sub CheckShoeButtonsForm()
        Dim card As Integer

        For card = 1 To 10
            If CInt(CurrentShoeBoxesArray(card - 1).Text) = OriginalShoe.Cards(card) Then
                CurrentShoeButtonsArray(card - 1, 0).Visible = False
            Else
                CurrentShoeButtonsArray(card - 1, 0).Visible = True
            End If
            If CInt(CurrentShoeBoxesArray(card - 1).Text) = 0 Then
                CurrentShoeButtonsArray(card - 1, 1).Visible = False
            Else
                CurrentShoeButtonsArray(card - 1, 1).Visible = True
            End If
        Next card
    End Sub

    Private Sub LoadShoeForm(ByRef shoe As BJCAShoeClass)
        Dim card As Integer

        For card = 1 To 10
            CurrentShoeBoxesArray(card - 1).Text = shoe.Cards(card)
        Next card
        CheckShoeButtonsForm()
    End Sub

    Private Sub CurrentShoeBoxesArrayHandler_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CurrentShoeBoxesArrayHandler.Validating
        If Not CheckValidInteger(DirectCast(sender, IndexedTextBox).Text, 0, OriginalShoe.Cards(DirectCast(sender, IndexedTextBox).Index), True) Then
            DirectCast(sender, IndexedTextBox).Text = CurrentHandShoe.Cards(DirectCast(sender, IndexedTextBox).Index)
            e.Cancel = True
            Exit Sub
        End If
        GetShoeForm(CurrentHandShoe)
        CheckShoeButtonsForm()
        GetCurrentHandShoeForm(True, True, True)
        LoadHandEVs()
    End Sub

    Private Sub CurrentShoeButtonsArrayHandler_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CurrentShoeButtonsArrayHandler.Click
        Select Case DirectCast(sender, IndexedButton).Index2
            Case 0
                CurrentHandShoe.Cards(DirectCast(sender, IndexedButton).Index) += 1
            Case 1
                CurrentHandShoe.Cards(DirectCast(sender, IndexedButton).Index) -= 1
        End Select
        LoadShoeForm(CurrentHandShoe)
        LoadFormEVs()
    End Sub

    Private Sub OriginalShoeButtonRTForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OriginalShoeButtonRTForm.Click
        ResetOriginalShoeForm()
    End Sub

    Private Sub HandShoeButtonRTForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HandShoeButtonRTForm.Click
        ResetCurrentShoeForm()
    End Sub

    Private Sub ResetOriginalShoeForm()
        ClearHandsForm()
        Rules.Shoe.Reset(OriginalShoe)
        CurrentShoe.Reset(OriginalShoe)
        CurrentHandShoe.Reset(OriginalShoe)
        LoadShoeForm(OriginalShoe)
        GetCurrentHandShoeForm(True, True, True)
        CurrentEV = OriginalEV
        LoadFormEVs()
    End Sub

    Private Sub ResetCurrentShoeForm()
        Dim tempShoe As New BJCAShoeClass

        tempShoe.Reset(CurrentShoe)

        ClearHandsForm()
        Rules.Shoe.Reset(tempShoe)
        CurrentShoe.Reset(tempShoe)
        LoadShoeForm(tempShoe)
        CurrentHandShoe.Reset(tempShoe)
        GetCurrentHandShoeForm(True, True, True)
        LoadFormEVs()
    End Sub

#End Region

#Region " Hand Methods "

    Private Sub PopulateHands()
        Dim box As New IndexedTextBox

        box.ImeMode = System.Windows.Forms.ImeMode.On
        box.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        box.Size = New System.Drawing.Size(128, 20)
        box.Location = New System.Drawing.Point(136, 19)
        DealersHand = box
        Me.Controls.Add(box)
        AddHandler box.TextChanged, AddressOf DealersHandHandler_TextChanged

        PopulateOtherPlayersHandsTable()
        PopulatePlayersHandsTable()
    End Sub

    Private Sub PopulatePlayersHandsTable()
        Dim hand As Integer

        'First populate the Player's hand boxes
        For hand = 0 To 3
            Dim box As New IndexedTextBox

            box.ImeMode = System.Windows.Forms.ImeMode.On
            box.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
            box.Size = New System.Drawing.Size(128, 20)
            box.Index = hand
            box.Location = New System.Drawing.Point(136, 74 + 28 * hand)

            If hand > 0 Then
                box.Visible = False
            End If

            PlayerHands(hand) = box
            Me.Controls.Add(box)

            'Add Handler to the general handler
            AddHandler box.TextChanged, AddressOf PlayerHandsArrayHandler_TextChanged

        Next hand

        'Now the Player's Total boxes
        For hand = 0 To 3
            Dim box As New IndexedTextBox

            box.ImeMode = System.Windows.Forms.ImeMode.On
            box.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
            box.Size = New System.Drawing.Size(24, 20)
            box.ReadOnly = True
            box.Index = hand
            box.Location = New System.Drawing.Point(272, 74 + 28 * hand)

            If hand > 0 Then
                box.Visible = False
            End If

            PlayerHandTotalBoxes(hand) = box
            Me.Controls.Add(box)
        Next hand

        'And now the Player's Strat boxes
        For hand = 0 To 3
            Dim box As New IndexedTextBox

            box.ImeMode = System.Windows.Forms.ImeMode.On
            box.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
            box.Size = New System.Drawing.Size(24, 20)
            box.ReadOnly = True
            box.Index = hand
            box.Location = New System.Drawing.Point(304, 74 + 28 * hand)

            If hand > 0 Then
                box.Visible = False
            End If

            PlayerHandStratBoxes(hand) = box
            Me.Controls.Add(box)
        Next hand

        'And finally the Player's EV boxes
        For hand = 0 To 3
            Dim box As New IndexedTextBox

            box.ImeMode = System.Windows.Forms.ImeMode.On
            box.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
            box.Size = New System.Drawing.Size(40, 20)
            box.ReadOnly = True
            box.Index = hand
            box.Location = New System.Drawing.Point(336, 74 + 28 * hand)

            If hand > 0 Then
                box.Visible = False
            End If

            PlayerHandEVBoxes(hand) = box
            Me.Controls.Add(box)

            PlayerHandEVs(hand) = New BJCARealtimeHandClass
        Next hand
    End Sub

    Private Function SplitOK(ByRef rules As BJCARulesClass, ByVal hand As BJCAHandClass) As Boolean
        'Returns if splitting is allowed

        Dim card As Integer
        Dim paircard As Integer
        Dim ok As Boolean

        ok = False
        paircard = hand.Paircard()
        If paircard > 0 And (CurrentSPL = 0 Or paircard = CurrentPaircard) And CurrentSPL < OriginalSPL Then
            ok = True
            CurrentPaircard = paircard
        End If
        If paircard = 1 And CurrentSPL > 0 And Not rules.SMA Then ok = False

        Return ok
    End Function

    Private Function SuitedOK(ByRef rules As BJCARulesClass, ByVal hand As BJCAHandClass, ByVal suitedNotPossible As Boolean) As Boolean
        'Returns if suited hand bonus is possible

        Dim ok As Boolean

        ok = False
        If CurrentSPL = 0 And hand.NumCards >= 2 And Not suitedNotPossible Then
            ok = True
        End If

        Return ok
    End Function

    Private Sub PopulateOtherPlayersHandsTable()
        Dim player As Integer

        For player = 0 To 5
            Dim box As New IndexedTextBox

            box.ImeMode = System.Windows.Forms.ImeMode.On
            box.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
            box.Size = New System.Drawing.Size(128, 20)
            box.Index = player

            box.Location = New System.Drawing.Point(436, 19 + 28 * player)

            OtherPlayersHands(player) = box
            Me.Controls.Add(box)

            'Add Handler to the general handler
            AddHandler box.TextChanged, AddressOf OtherPlayersHandsArrayHandler_TextChanged

        Next player
    End Sub

    Private Sub GetNetOtherPlayersHandsForm()
        Dim hand As Integer
        Dim card As Integer
        Dim nCard As Integer

        NetOtherPlayersHands.Empty()

        For hand = 0 To 5
            For card = 1 To 10
                For nCard = 1 To OtherPlayersHands(hand).Hand.Cards(card)
                    NetOtherPlayersHands.Deal(card)
                Next nCard
            Next card
        Next hand

    End Sub

    Private Sub GetNetPlayerHandForm()
        Dim hand As Integer
        Dim card As Integer
        Dim nCard As Integer

        NetPlayerHand.Empty()

        For hand = 0 To 3
            For card = 1 To 10
                For nCard = 1 To PlayerHands(hand).Hand.Cards(card)
                    NetPlayerHand.Deal(card)
                Next nCard
            Next card
        Next hand

    End Sub

    Private Sub ClearHandsForm()
        Dim hand As Integer

        ClearingForm = True
        For hand = 3 To 0 Step -1
            PlayerHands(hand).Text = ""
            PlayerHands(hand).Hand.Empty()
            PlayerHandTotalBoxes(hand).Text = ""
            PlayerHandEVBoxes(hand).Text = ""
            PlayerHandEVs(hand).Hand.Empty()
            If hand > 0 Then
                PlayerHands(hand).Visible = False
                PlayerHandTotalBoxes(hand).Visible = False
                PlayerHandStratBoxes(hand).Visible = False
                PlayerHandEVBoxes(hand).Visible = False
            End If
        Next hand
        For hand = 0 To 5
            OtherPlayersHands(hand).Text = ""
            OtherPlayersHands(hand).Hand.Empty()
        Next
        DealersHand.Text = ""
        DealersHand.Hand.Empty()
        ClearingForm = False
    End Sub

    Private Sub GetCurrentHandShoeForm(ByVal includePlayer As Boolean, ByVal includeDealer As Boolean, ByVal includeOtherPlayers As Boolean)
        Dim hand As Integer

        CurrentHandShoe.Reset(CurrentShoe)

        If includeDealer Then
            CurrentHandShoe.Deal(DealersHand.Hand)
        End If

        If includeOtherPlayers Then
            For hand = 0 To 5
                CurrentHandShoe.Deal(OtherPlayersHands(hand).Hand)
            Next
        End If

        If includePlayer Then
            For hand = 0 To 3
                CurrentHandShoe.Deal(PlayerHands(hand).Hand)
            Next hand
        End If
    End Sub

    Private Sub OtherPlayersHandsArrayHandler_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OtherPlayersHandsArrayHandler.TextChanged
        If ValidCardString(DirectCast(sender, IndexedTextBox).Text) Then
            OtherPlayersHands(DirectCast(sender, IndexedTextBox).Index).Hand.Empty()
            OtherPlayersHands(DirectCast(sender, IndexedTextBox).Index).Hand = GetStringHand(DirectCast(sender, IndexedTextBox).Text)
            GetCurrentHandShoeForm(True, True, False)
            GetNetOtherPlayersHandsForm()
            If CurrentHandShoe.HandPossible(NetOtherPlayersHands) Then
                OtherPlayersHandsStrings(DirectCast(sender, IndexedTextBox).Index) = DirectCast(sender, IndexedTextBox).Text
            Else
                MsgBox("This Other Player's hand is not possible given the current shoe.", MsgBoxStyle.OKOnly)
                OtherPlayersHands(DirectCast(sender, IndexedTextBox).Index).Text = OtherPlayersHandsStrings(DirectCast(sender, IndexedTextBox).Index)
                If OtherPlayersHandsStrings(DirectCast(sender, IndexedTextBox).Index) <> "" Then
                    OtherPlayersHands(DirectCast(sender, IndexedTextBox).Index).Hand.Empty()
                    OtherPlayersHands(DirectCast(sender, IndexedTextBox).Index).Hand = GetStringHand(DirectCast(sender, IndexedTextBox).Text)
                Else
                    OtherPlayersHands(DirectCast(sender, IndexedTextBox).Index).Hand.Empty()
                End If
            End If
        Else
            MsgBox("This is not a valid card string.", MsgBoxStyle.OKOnly)
            OtherPlayersHands(DirectCast(sender, IndexedTextBox).Index).Text = OtherPlayersHandsStrings(DirectCast(sender, IndexedTextBox).Index)
            If OtherPlayersHandsStrings(DirectCast(sender, IndexedTextBox).Index) <> "" Then
                OtherPlayersHands(DirectCast(sender, IndexedTextBox).Index).Hand.Empty()
                OtherPlayersHands(DirectCast(sender, IndexedTextBox).Index).Hand = GetStringHand(DirectCast(sender, IndexedTextBox).Text)
            Else
                OtherPlayersHands(DirectCast(sender, IndexedTextBox).Index).Hand.Empty()
            End If
        End If

        GetCurrentHandShoeForm(True, True, True)
        LoadFormEVs()
    End Sub

    Private Sub DealersHandHandler_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DealersHandHandler.TextChanged
        Dim upcardtext As String

        If ValidCardString(DealersHand.Text) And DealersHand.Text <> "" Then
            DealersHand.Hand.Empty()
            DealersHand.Hand = GetStringHand(DealersHand.Text)
            GetCurrentHandShoeForm(True, False, True)
            If CurrentHandShoe.HandPossible(DealersHand.Hand) Then
                DealersHandString = DealersHand.Text
                upcardtext = DealersHand.Text.Chars(0)
                If upcardtext = "a" Or upcardtext = "A" Then
                    upcardtext = "1"
                ElseIf upcardtext = "t" Or upcardtext = "T" Or upcardtext = "0" Then
                    upcardtext = "10"
                End If
                CurrentUpcard = CInt(upcardtext)
            Else
                MsgBox("This Dealer's hand is not possible given the current shoe.", MsgBoxStyle.OKOnly)
                DealersHand.Text = DealersHandString
                If DealersHandString <> "" Then
                    DealersHand.Hand.Empty()
                    DealersHand.Hand = GetStringHand(DealersHand.Text)
                    upcardtext = DealersHand.Text.Chars(0)
                    If upcardtext = "a" Or upcardtext = "A" Then
                        upcardtext = "1"
                    ElseIf upcardtext = "t" Or upcardtext = "T" Or upcardtext = "0" Then
                        upcardtext = "10"
                    End If
                    CurrentUpcard = CInt(upcardtext)
                Else
                    DealersHand.Hand.Empty()
                    CurrentUpcard = 0
                End If
            End If
        ElseIf DealersHand.Text <> "" Then
            MsgBox("This is not a valid card string.", MsgBoxStyle.OKOnly)
            DealersHand.Text = DealersHandString
            If DealersHandString <> "" Then
                DealersHand.Hand.Empty()
                DealersHand.Hand = GetStringHand(DealersHand.Text)
                upcardtext = DealersHand.Text.Chars(0)
                If upcardtext = "a" Or upcardtext = "A" Then
                    upcardtext = "1"
                ElseIf upcardtext = "t" Or upcardtext = "T" Or upcardtext = "0" Then
                    upcardtext = "10"
                End If
                CurrentUpcard = CInt(upcardtext)
            Else
                DealersHand.Hand.Empty()
                CurrentUpcard = 0
            End If
        Else
            DealersHand.Hand.Empty()
            CurrentUpcard = 0
        End If
        GetCurrentHandShoeForm(True, True, True)
        LoadFormEVs()
    End Sub

    Public Sub CheckInsurance()
        Dim p10 As Double
        Dim insuranceEV As Double

        If DealersHand.Hand.NumCards = 1 And DealersHand.Hand.Cards(1) = 1 And CurrentSPL = 0 And PlayerHands(0).Hand.NumCards <= 2 Then
            InsuranceBoxRTForm.Visible = True
            InsuranceEVBoxRTForm.Visible = True
            InsuranceLabelRTForm.Visible = True
            p10 = CurrentHandShoe.Cards(10) / CurrentHandShoe.CardsLeft
            insuranceEV = (-(1 - p10) + InsPays * p10)
            FillNumberTextBox(InsuranceEVBoxRTForm, insuranceEV, 7, False)
            InsuranceBoxRTForm.BackColor = InsuranceEVBoxRTForm.BackColor
            If insuranceEV > 0 Then
                InsuranceBoxRTForm.Text = "Insure"
            ElseIf insuranceEV = 0 Then
                InsuranceBoxRTForm.Text = ""
            Else
                InsuranceBoxRTForm.Text = "Don't Insure"
            End If
        ElseIf DealersHand.Text = "" Then
            InsuranceBoxRTForm.Visible = False
            InsuranceEVBoxRTForm.Visible = False
            InsuranceLabelRTForm.Visible = False
        Else
            InsuranceBoxRTForm.Visible = False
            InsuranceEVBoxRTForm.Visible = False
            InsuranceLabelRTForm.Visible = False
        End If

    End Sub

    Private Sub PlayerHandsArrayHandler_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PlayerHandsArrayHandler.TextChanged

        CurrentPHandIndex = DirectCast(sender, IndexedTextBox).Index

        'First make sure the hand is valid and see if it's possible to split it
        If ValidCardString(DirectCast(sender, IndexedTextBox).Text) Then
            PlayerHands(CurrentPHandIndex).Hand.Empty()
            PlayerHands(CurrentPHandIndex).Hand = GetStringHand(DirectCast(sender, IndexedTextBox).Text)
            GetCurrentHandShoeForm(False, True, True)
            GetNetPlayerHandForm()
            If CurrentHandShoe.HandPossible(NetPlayerHand) Then
                If Not ClearingForm And CurrentPHandIndex > 0 And CurrentSPL > 0 Then
                    If PlayerHands(CurrentPHandIndex).Text = "" Then
                        MsgBox("The split cards must match.", MsgBoxStyle.OKOnly)
                        If PlayerHandsStrings(CurrentPHandIndex) <> "" Then
                            PlayerHands(CurrentPHandIndex).Text = PlayerHandsStrings(CurrentPHandIndex)
                            PlayerHands(CurrentPHandIndex).Hand.Empty()
                            PlayerHands(CurrentPHandIndex).Hand = GetStringHand(DirectCast(sender, IndexedTextBox).Text)
                        Else
                            PlayerHands(CurrentPHandIndex).Text = ""
                            PlayerHands(CurrentPHandIndex).Hand.Empty()
                        End If
                    ElseIf PlayerHands(CurrentPHandIndex).Text.Chars(0) <> PlayerHands(0).Text.Chars(0) Then
                        MsgBox("The split cards must match.", MsgBoxStyle.OKOnly)
                        If PlayerHandsStrings(CurrentPHandIndex) <> "" Then
                            PlayerHands(CurrentPHandIndex).Text = PlayerHandsStrings(CurrentPHandIndex)
                            PlayerHands(CurrentPHandIndex).Hand.Empty()
                            PlayerHands(CurrentPHandIndex).Hand = GetStringHand(DirectCast(sender, IndexedTextBox).Text)
                        Else
                            PlayerHands(CurrentPHandIndex).Text = ""
                            PlayerHands(CurrentPHandIndex).Hand.Empty()
                        End If
                    Else
                        'Check for the possibility of a split
                        If SplitOK(Rules, PlayerHands(CurrentPHandIndex).Hand) Then
                            SplitButtonRTForm.Visible = True
                            SplitButtonRTForm.Location = New System.Drawing.Point(144, 102 + 28 * CurrentSPL)
                        Else
                            SplitButtonRTForm.Visible = False
                        End If

                        PlayerHandsStrings(CurrentPHandIndex) = DirectCast(sender, IndexedTextBox).Text
                    End If
                Else
                    'Check for the possibility of a split
                    If SplitOK(Rules, PlayerHands(CurrentPHandIndex).Hand) Then
                        SplitButtonRTForm.Visible = True
                        SplitButtonRTForm.Location = New System.Drawing.Point(144, 102 + 28 * CurrentSPL)
                    Else
                        SplitButtonRTForm.Visible = False
                    End If

                    PlayerHandsStrings(CurrentPHandIndex) = DirectCast(sender, IndexedTextBox).Text
                End If
            Else
                MsgBox("This Player's hand is not possible given the current shoe.", MsgBoxStyle.OKOnly)
                If PlayerHandsStrings(CurrentPHandIndex) <> "" Then
                    PlayerHands(CurrentPHandIndex).Text = PlayerHandsStrings(CurrentPHandIndex)
                    PlayerHands(CurrentPHandIndex).Hand.Empty()
                    PlayerHands(CurrentPHandIndex).Hand = GetStringHand(DirectCast(sender, IndexedTextBox).Text)
                Else
                    PlayerHands(CurrentPHandIndex).Text = ""
                    PlayerHands(CurrentPHandIndex).Hand.Empty()
                End If
            End If
        Else
            MsgBox("This is not a valid card string.", MsgBoxStyle.OKOnly)
            If PlayerHandsStrings(CurrentPHandIndex) <> "" Then
                PlayerHands(CurrentPHandIndex).Text = PlayerHandsStrings(CurrentPHandIndex)
                PlayerHands(CurrentPHandIndex).Hand.Empty()
                PlayerHands(CurrentPHandIndex).Hand = GetStringHand(DirectCast(sender, IndexedTextBox).Text)
            Else
                PlayerHands(CurrentPHandIndex).Text = ""
                PlayerHands(CurrentPHandIndex).Hand.Empty()
            End If
        End If

        GetCurrentHandShoeForm(True, True, True)
        LoadFormEVs()

    End Sub

    Private Sub SplitButtonRTForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SplitButtonRTForm.Click
        Dim index As Integer

        index = CurrentPHandIndex
        PlayerHands(index).Text = GetCardString(CurrentPaircard)
        PlayerHands(index).Hand.Empty()
        PlayerHands(index).Hand.Deal(CurrentPaircard)

        PlayerHands(CurrentSPL + 1).Text = GetCardString(CurrentPaircard)
        PlayerHands(CurrentSPL + 1).Visible = True
        PlayerHandTotalBoxes(CurrentSPL + 1).Visible = True
        PlayerHandStratBoxes(CurrentSPL + 1).Visible = True
        PlayerHandEVBoxes(CurrentSPL + 1).Visible = True
        PlayerHands(CurrentSPL + 1).Hand.Empty()
        PlayerHands(CurrentSPL + 1).Hand.Deal(CurrentPaircard)

        GetCurrentHandShoeForm(True, True, True)
        CurrentSPL += 1
        SplitButtonRTForm.Visible = False
        LoadHandEVs()
    End Sub

#End Region

#Region " Strategy Methods "

    Private Sub ShowGameEV()
        NewEVLabelRTForm.Visible = True
        NewEVBoxRTForm.Visible = True
        FillNumberTextBox(NewEVBoxRTForm, CurrentEV, 7, False)

        StratLabelRTForm.Visible = False
        StratBoxRTForm.Visible = False
        StandLabelRTForm.Visible = False
        StandBoxRTForm.Visible = False
        HitLabelRTForm.Visible = False
        HitBoxRTForm.Visible = False
        DoubleLabelRTForm.Visible = False
        DoubleBoxRTForm.Visible = False
        SurrLabelRTForm.Visible = False
        SurrBoxRTForm.Visible = False
        SplitLabelRTForm.Visible = False
        SplitBoxRTForm.Visible = False
        InsuranceBoxRTForm.Visible = False
        InsuranceEVBoxRTForm.Visible = False
        InsuranceLabelRTForm.Visible = False
    End Sub

    Private Sub ShowHandEVs()
        NewEVLabelRTForm.Visible = False
        NewEVBoxRTForm.Visible = False
        StratLabelRTForm.Visible = True
        StratBoxRTForm.Visible = True
        StandLabelRTForm.Visible = True
        StandBoxRTForm.Visible = True
    End Sub

    Private Sub ClearHandEVs()
        PlayerHandTotalBoxes(0).Text = ""
        FillNumberTextBox(PlayerHandStratBoxes(0), 0, 3, False)
        FillNumberTextBox(PlayerHandEVBoxes(0), 0, 3, False)

        FillNumberTextBox(StratBoxRTForm, 0, 7, False)
        FillNumberTextBox(StandBoxRTForm, 0, 7, False)
        FillNumberTextBox(DoubleBoxRTForm, 0, 7, False)
        FillNumberTextBox(HitBoxRTForm, 0, 7, False)
        FillNumberTextBox(SurrBoxRTForm, 0, 7, False)
        FillNumberTextBox(SplitBoxRTForm, 0, 7, False)
    End Sub

    Private Sub LoadHandEVs()
        Dim handIndex As Integer
        Dim newEvs As BJCAStratHandEVsClass

        If DealersHand.Hand.NumCards = 1 Then
            For handIndex = 0 To 3
                If PlayerHands(handIndex).Hand.NumCards > 1 Then
                    If PlayerHands(handIndex).Hand.Total > 21 Then
                        PlayerHandTotalBoxes(handIndex).BackColor = System.Drawing.Color.Orange
                        PlayerHandTotalBoxes(handIndex).Text = PlayerHands(handIndex).Hand.Total
                        PlayerHandStratBoxes(handIndex).BackColor = System.Drawing.Color.Orange
                        PlayerHandStratBoxes(handIndex).Text = "B"
                        FillNumberTextBox(PlayerHandEVBoxes(handIndex), -1, 0, False)
                    Else
                        GetPlayerHandStrategy(handIndex)

                        'Check for the possibility of a suited hand box
                        If SuitedOK(Rules, PlayerHands(CurrentPHandIndex).Hand, PlayerHandEVs(CurrentPHandIndex).SuitedEVs Is Nothing) Then
                            SuitedHandComboBoxRTForm.Visible = True
                            SuitedHandComboBoxRTForm.SelectedIndex = 0
                            SuitedHandComboBoxRTForm.Location = New System.Drawing.Point(240, 64 + 24)
                        Else
                            SuitedHandComboBoxRTForm.SelectedIndex = 0
                            SuitedHandComboBoxRTForm.Visible = False
                        End If

                        If PlayerHands(handIndex).Hand.Soft Then
                            PlayerHandTotalBoxes(handIndex).BackColor = System.Drawing.SystemColors.ControlLight
                        Else
                            PlayerHandTotalBoxes(handIndex).BackColor = Nothing
                        End If
                        PlayerHandTotalBoxes(handIndex).Text = CStr(PlayerHands(handIndex).Hand.Total)

                        FillStratTextBox(PlayerHandStratBoxes(handIndex), PlayerHandEVs(handIndex).Hand.Strat(CurrentUpcard), False, Rules.ColorTable)
                        FillNumberTextBox(PlayerHandEVBoxes(handIndex), PlayerHandEVs(handIndex).Hand.StratEV(CurrentUpcard), 3, False)
                    End If
                Else
                    PlayerHandTotalBoxes(handIndex).BackColor = Nothing
                    PlayerHandTotalBoxes(handIndex).Text = ""

                    PlayerHandStratBoxes(handIndex).BackColor = Nothing
                    PlayerHandStratBoxes(handIndex).Text = ""

                    PlayerHandEVBoxes(handIndex).BackColor = Nothing
                    PlayerHandEVBoxes(handIndex).Text = ""
                End If
            Next handIndex

            newEvs = PlayerHandEVs(CurrentPHandIndex).Hand
            If PlayerHands(CurrentPHandIndex).Hand.Total > 21 Then
                ClearHandEVs()
                StratBoxRTForm.Text = "Busted"
                StratBoxRTForm.BackColor = System.Drawing.Color.Orange
                FillNumberTextBox(StandBoxRTForm, -1, 0, False)
            ElseIf PlayerHands(CurrentPHandIndex).Hand.NumCards < 2 Then
                ClearHandEVs()
            Else
                FillStratTextBox(StratBoxRTForm, newEvs.Strat(CurrentUpcard), True, Rules.ColorTable)
                FillNumberTextBox(StandBoxRTForm, newEvs.StandEV(CurrentUpcard), 7, False)

                If Not (CurrentPaircard = 1 And CurrentSPL > 0 And Not Rules.HSA) Then
                    FillNumberTextBox(HitBoxRTForm, newEvs.HitEV(CurrentUpcard), 7, False)
                    HitLabelRTForm.Visible = True
                    HitBoxRTForm.Visible = True
                Else
                    FillNumberTextBox(HitBoxRTForm, 0, 7, False)
                    HitLabelRTForm.Visible = False
                    HitBoxRTForm.Visible = False
                End If
                If newEvs.DAllowed(CurrentUpcard) Then
                    FillNumberTextBox(DoubleBoxRTForm, newEvs.DEV(CurrentUpcard), 7, False)
                    DoubleLabelRTForm.Visible = True
                    DoubleBoxRTForm.Visible = True
                Else
                    FillNumberTextBox(DoubleBoxRTForm, 0, 7, False)
                    DoubleLabelRTForm.Visible = False
                    DoubleBoxRTForm.Visible = False
                End If
                If newEvs.RAllowed(CurrentUpcard) > 0 Then
                    FillNumberTextBox(SurrBoxRTForm, newEvs.SurrEV(CurrentUpcard), 7, False)
                    SurrLabelRTForm.Visible = True
                    SurrBoxRTForm.Visible = True
                Else
                    FillNumberTextBox(SurrBoxRTForm, 0, 7, False)
                    SurrLabelRTForm.Visible = False
                    SurrBoxRTForm.Visible = False
                End If
                If newEvs.PAllowed(CurrentUpcard) Then
                    FillNumberTextBox(SplitBoxRTForm, newEvs.SplitEV(CurrentUpcard), 7, False)
                    SplitLabelRTForm.Visible = True
                    SplitBoxRTForm.Visible = True
                Else
                    FillNumberTextBox(SplitBoxRTForm, 0, 7, False)
                    SplitLabelRTForm.Visible = False
                    SplitBoxRTForm.Visible = False
                End If
            End If
        Else
            ClearHandEVs()
        End If
    End Sub

    Private Sub SuitedHandComboBoxRTForm_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SuitedHandComboBoxRTForm.SelectedIndexChanged
        LoadSuitedHandEVs()
    End Sub

    Private Sub LoadSuitedHandEVs()
        Dim handIndex As Integer
        Dim suit As Integer
        Dim newEvs As BJCAStratHandEVsClass

        'Since a suited hand is possible we know we're at Hand 1 and not split and we already have the EVs
        If SuitedHandComboBoxRTForm.SelectedIndex = 0 Then
            FillStratTextBox(PlayerHandStratBoxes(CurrentPHandIndex), PlayerHandEVs(CurrentPHandIndex).Hand.Strat(CurrentUpcard), False, Rules.ColorTable)
            FillNumberTextBox(PlayerHandEVBoxes(CurrentPHandIndex), PlayerHandEVs(CurrentPHandIndex).Hand.StratEV(CurrentUpcard), 3, False)

            newEvs = PlayerHandEVs(CurrentPHandIndex).Hand

            FillStratTextBox(StratBoxRTForm, newEvs.Strat(CurrentUpcard), True, Rules.ColorTable)
            FillNumberTextBox(StandBoxRTForm, newEvs.StandEV(CurrentUpcard), 7, False)

            If Not (CurrentPaircard = 1 And CurrentSPL > 0 And Not Rules.HSA) Then
                FillNumberTextBox(HitBoxRTForm, newEvs.HitEV(CurrentUpcard), 7, False)
                HitLabelRTForm.Visible = True
                HitBoxRTForm.Visible = True
            Else
                FillNumberTextBox(HitBoxRTForm, 0, 7, False)
                HitLabelRTForm.Visible = False
                HitBoxRTForm.Visible = False
            End If
            If newEvs.DAllowed(CurrentUpcard) Then
                FillNumberTextBox(DoubleBoxRTForm, newEvs.DEV(CurrentUpcard), 7, False)
                DoubleLabelRTForm.Visible = True
                DoubleBoxRTForm.Visible = True
            Else
                FillNumberTextBox(DoubleBoxRTForm, 0, 7, False)
                DoubleLabelRTForm.Visible = False
                DoubleBoxRTForm.Visible = False
            End If
            If newEvs.RAllowed(CurrentUpcard) > 0 Then
                FillNumberTextBox(SurrBoxRTForm, newEvs.SurrEV(CurrentUpcard), 7, False)
                SurrLabelRTForm.Visible = True
                SurrBoxRTForm.Visible = True
            Else
                FillNumberTextBox(SurrBoxRTForm, 0, 7, False)
                SurrLabelRTForm.Visible = False
                SurrBoxRTForm.Visible = False
            End If
            If newEvs.PAllowed(CurrentUpcard) Then
                FillNumberTextBox(SplitBoxRTForm, newEvs.SplitEV(CurrentUpcard), 7, False)
                SplitLabelRTForm.Visible = True
                SplitBoxRTForm.Visible = True
            Else
                FillNumberTextBox(SplitBoxRTForm, 0, 7, False)
                SplitLabelRTForm.Visible = False
                SplitBoxRTForm.Visible = False
            End If
        Else
            suit = SuitedHandComboBoxRTForm.SelectedIndex - 1
            If PlayerHandEVs(CurrentPHandIndex).Hand.DealerProbs(CurrentUpcard, suit) = 0 Then
                PlayerHandTotalBoxes(CurrentPHandIndex).BackColor = Nothing
                PlayerHandTotalBoxes(CurrentPHandIndex).Text = ""

                PlayerHandStratBoxes(CurrentPHandIndex).BackColor = Nothing
                PlayerHandStratBoxes(CurrentPHandIndex).Text = ""

                PlayerHandEVBoxes(CurrentPHandIndex).BackColor = Nothing
                PlayerHandEVBoxes(CurrentPHandIndex).Text = ""

                ClearHandEVs()
            Else
                FillStratTextBox(PlayerHandStratBoxes(CurrentPHandIndex), PlayerHandEVs(CurrentPHandIndex).SuitedEVs.SuitedStrat(CurrentUpcard, suit), False, Rules.ColorTable)
                FillNumberTextBox(PlayerHandEVBoxes(CurrentPHandIndex), PlayerHandEVs(CurrentPHandIndex).SuitedEVs.SuitedStratEV(CurrentUpcard, suit), 3, False)

                newEvs = PlayerHandEVs(CurrentPHandIndex).Hand

                FillStratTextBox(StratBoxRTForm, PlayerHandEVs(CurrentPHandIndex).SuitedEVs.SuitedStrat(CurrentUpcard, suit), True, Rules.ColorTable)
                FillNumberTextBox(StandBoxRTForm, PlayerHandEVs(CurrentPHandIndex).SuitedEVs.SuitedStandEV(CurrentUpcard, suit), 7, False)

                FillNumberTextBox(HitBoxRTForm, PlayerHandEVs(CurrentPHandIndex).SuitedEVs.SuitedHitEV(CurrentUpcard, suit), 7, False)
                HitLabelRTForm.Visible = True
                HitBoxRTForm.Visible = True

                If newEvs.DAllowed(CurrentUpcard) Then
                    FillNumberTextBox(DoubleBoxRTForm, newEvs.DEV(CurrentUpcard), 7, False)
                    DoubleLabelRTForm.Visible = True
                    DoubleBoxRTForm.Visible = True
                Else
                    FillNumberTextBox(DoubleBoxRTForm, 0, 7, False)
                    DoubleLabelRTForm.Visible = False
                    DoubleBoxRTForm.Visible = False
                End If
                If newEvs.RAllowed(CurrentUpcard) > 0 Then
                    FillNumberTextBox(SurrBoxRTForm, newEvs.SurrEV(CurrentUpcard), 7, False)
                    SurrLabelRTForm.Visible = True
                    SurrBoxRTForm.Visible = True
                Else
                    FillNumberTextBox(SurrBoxRTForm, 0, 7, False)
                    SurrLabelRTForm.Visible = False
                    SurrBoxRTForm.Visible = False
                End If
                If newEvs.PAllowed(CurrentUpcard) Then
                    FillNumberTextBox(SplitBoxRTForm, newEvs.SplitEV(CurrentUpcard), 7, False)
                    SplitLabelRTForm.Visible = True
                    SplitBoxRTForm.Visible = True
                Else
                    FillNumberTextBox(SplitBoxRTForm, 0, 7, False)
                    SplitLabelRTForm.Visible = False
                    SplitBoxRTForm.Visible = False
                End If
            End If
        End If

    End Sub

    Private Sub GetPlayerHandStrategy(ByVal handIndex As Integer)
        Dim tempCA As New BJCA
        Dim card As Integer

        Rules.Shoe.Reset(CurrentHandShoe)
        Rules.Shoe.Undeal(PlayerHands(handIndex).Hand)
        Rules.Shoe.Undeal(CurrentUpcard)
        For card = 1 To 10
            Rules.UCAllowed(card) = (card = CurrentUpcard)
            If card <> CurrentPaircard Then
                Rules.SplitAllowed(card) = False
            Else
                Rules.SplitAllowed(card) = OriginalRules.SplitAllowed(card)
            End If
        Next card
        Rules.SPL = OriginalSPL - CurrentSPL
        PlayerHandEVs(handIndex) = tempCA.BJCART(PlayerHands(handIndex).Hand, CurrentUpcard, CurrentSPL > 0, CurrentPaircard, Rules, DealerProbsRT)
    End Sub

    Private Sub LoadFormEVs()
        CheckInsurance()
        LoadShoeForm(CurrentHandShoe)
        If DealersHand.Hand.NumCards = 0 Then
            ShowGameEV()
        Else
            ShowHandEVs()
            LoadHandEVs()
        End If
    End Sub

#End Region





End Class
