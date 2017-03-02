Imports BJ_CA.BJCAShared

Public Class BJCASmallRealTimeForm
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
    Private OtherPlayersHandsStrings(1) As String
    Private OtherPlayersHands(1) As IndexedTextBox
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
    Friend WithEvents SplitButtonRTForm As System.Windows.Forms.Button
    Friend WithEvents NewHandButtonRTForm As System.Windows.Forms.Button
    Friend WithEvents OPHandsLabelRTForm As System.Windows.Forms.Label
    Friend WithEvents PHandLabelRTForm As System.Windows.Forms.Label
    Friend WithEvents ResetShoeCheckRTForm As System.Windows.Forms.CheckBox
    Friend WithEvents OriginalShoeButtonRTForm As System.Windows.Forms.Button
    Friend WithEvents DHandLabelRTForm As System.Windows.Forms.Label
    Friend WithEvents InsuranceBoxRTForm As System.Windows.Forms.TextBox
    Friend WithEvents NewEVLabelRTForm As System.Windows.Forms.Label
    Friend WithEvents NewEVBoxRTForm As System.Windows.Forms.TextBox
    Friend WithEvents StratLabelRTForm As System.Windows.Forms.Label
    Friend WithEvents StratBoxRTForm As System.Windows.Forms.TextBox
    Friend WithEvents SuitedHandComboBoxRTForm As System.Windows.Forms.ComboBox
    Friend WithEvents ALabelRTForm As System.Windows.Forms.Label
    Friend WithEvents TLabelRTForm As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(BJCASmallRealTimeForm))
        Me.SplitButtonRTForm = New System.Windows.Forms.Button
        Me.NewHandButtonRTForm = New System.Windows.Forms.Button
        Me.OPHandsLabelRTForm = New System.Windows.Forms.Label
        Me.PHandLabelRTForm = New System.Windows.Forms.Label
        Me.ResetShoeCheckRTForm = New System.Windows.Forms.CheckBox
        Me.OriginalShoeButtonRTForm = New System.Windows.Forms.Button
        Me.DHandLabelRTForm = New System.Windows.Forms.Label
        Me.InsuranceBoxRTForm = New System.Windows.Forms.TextBox
        Me.NewEVLabelRTForm = New System.Windows.Forms.Label
        Me.NewEVBoxRTForm = New System.Windows.Forms.TextBox
        Me.StratLabelRTForm = New System.Windows.Forms.Label
        Me.StratBoxRTForm = New System.Windows.Forms.TextBox
        Me.SuitedHandComboBoxRTForm = New System.Windows.Forms.ComboBox
        Me.ALabelRTForm = New System.Windows.Forms.Label
        Me.TLabelRTForm = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'SplitButtonRTForm
        '
        Me.SplitButtonRTForm.Location = New System.Drawing.Point(0, 92)
        Me.SplitButtonRTForm.Name = "SplitButtonRTForm"
        Me.SplitButtonRTForm.Size = New System.Drawing.Size(112, 28)
        Me.SplitButtonRTForm.TabIndex = 151
        Me.SplitButtonRTForm.Text = "Split"
        Me.SplitButtonRTForm.Visible = False
        '
        'NewHandButtonRTForm
        '
        Me.NewHandButtonRTForm.Location = New System.Drawing.Point(144, 194)
        Me.NewHandButtonRTForm.Name = "NewHandButtonRTForm"
        Me.NewHandButtonRTForm.Size = New System.Drawing.Size(128, 28)
        Me.NewHandButtonRTForm.TabIndex = 150
        Me.NewHandButtonRTForm.Text = "New Hand"
        '
        'OPHandsLabelRTForm
        '
        Me.OPHandsLabelRTForm.Location = New System.Drawing.Point(10, 46)
        Me.OPHandsLabelRTForm.Name = "OPHandsLabelRTForm"
        Me.OPHandsLabelRTForm.Size = New System.Drawing.Size(105, 19)
        Me.OPHandsLabelRTForm.TabIndex = 149
        Me.OPHandsLabelRTForm.Text = "Others' Cards"
        '
        'PHandLabelRTForm
        '
        Me.PHandLabelRTForm.Location = New System.Drawing.Point(10, 74)
        Me.PHandLabelRTForm.Name = "PHandLabelRTForm"
        Me.PHandLabelRTForm.Size = New System.Drawing.Size(105, 18)
        Me.PHandLabelRTForm.TabIndex = 148
        Me.PHandLabelRTForm.Text = "Player's Hand(s)"
        '
        'ResetShoeCheckRTForm
        '
        Me.ResetShoeCheckRTForm.Location = New System.Drawing.Point(125, 258)
        Me.ResetShoeCheckRTForm.Name = "ResetShoeCheckRTForm"
        Me.ResetShoeCheckRTForm.Size = New System.Drawing.Size(173, 19)
        Me.ResetShoeCheckRTForm.TabIndex = 147
        Me.ResetShoeCheckRTForm.Text = "Reset shoe every hand"
        '
        'OriginalShoeButtonRTForm
        '
        Me.OriginalShoeButtonRTForm.Location = New System.Drawing.Point(24, 194)
        Me.OriginalShoeButtonRTForm.Name = "OriginalShoeButtonRTForm"
        Me.OriginalShoeButtonRTForm.Size = New System.Drawing.Size(112, 28)
        Me.OriginalShoeButtonRTForm.TabIndex = 146
        Me.OriginalShoeButtonRTForm.Text = "Original Shoe"
        '
        'DHandLabelRTForm
        '
        Me.DHandLabelRTForm.Location = New System.Drawing.Point(10, 18)
        Me.DHandLabelRTForm.Name = "DHandLabelRTForm"
        Me.DHandLabelRTForm.Size = New System.Drawing.Size(105, 19)
        Me.DHandLabelRTForm.TabIndex = 145
        Me.DHandLabelRTForm.Text = "Dealer's Hand"
        '
        'InsuranceBoxRTForm
        '
        Me.InsuranceBoxRTForm.BackColor = System.Drawing.SystemColors.Control
        Me.InsuranceBoxRTForm.Location = New System.Drawing.Point(269, 231)
        Me.InsuranceBoxRTForm.Name = "InsuranceBoxRTForm"
        Me.InsuranceBoxRTForm.ReadOnly = True
        Me.InsuranceBoxRTForm.Size = New System.Drawing.Size(77, 22)
        Me.InsuranceBoxRTForm.TabIndex = 161
        Me.InsuranceBoxRTForm.TabStop = False
        Me.InsuranceBoxRTForm.Text = ""
        Me.InsuranceBoxRTForm.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.InsuranceBoxRTForm.Visible = False
        '
        'NewEVLabelRTForm
        '
        Me.NewEVLabelRTForm.Location = New System.Drawing.Point(48, 231)
        Me.NewEVLabelRTForm.Name = "NewEVLabelRTForm"
        Me.NewEVLabelRTForm.Size = New System.Drawing.Size(77, 18)
        Me.NewEVLabelRTForm.TabIndex = 166
        Me.NewEVLabelRTForm.Text = "EV of Hand"
        Me.NewEVLabelRTForm.Visible = False
        '
        'NewEVBoxRTForm
        '
        Me.NewEVBoxRTForm.Location = New System.Drawing.Point(163, 231)
        Me.NewEVBoxRTForm.Name = "NewEVBoxRTForm"
        Me.NewEVBoxRTForm.ReadOnly = True
        Me.NewEVBoxRTForm.Size = New System.Drawing.Size(77, 22)
        Me.NewEVBoxRTForm.TabIndex = 165
        Me.NewEVBoxRTForm.TabStop = False
        Me.NewEVBoxRTForm.Text = ""
        Me.NewEVBoxRTForm.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.NewEVBoxRTForm.Visible = False
        '
        'StratLabelRTForm
        '
        Me.StratLabelRTForm.Location = New System.Drawing.Point(48, 231)
        Me.StratLabelRTForm.Name = "StratLabelRTForm"
        Me.StratLabelRTForm.Size = New System.Drawing.Size(67, 18)
        Me.StratLabelRTForm.TabIndex = 164
        Me.StratLabelRTForm.Text = "Strategy"
        Me.StratLabelRTForm.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'StratBoxRTForm
        '
        Me.StratBoxRTForm.Location = New System.Drawing.Point(163, 231)
        Me.StratBoxRTForm.Name = "StratBoxRTForm"
        Me.StratBoxRTForm.ReadOnly = True
        Me.StratBoxRTForm.Size = New System.Drawing.Size(77, 22)
        Me.StratBoxRTForm.TabIndex = 167
        Me.StratBoxRTForm.TabStop = False
        Me.StratBoxRTForm.Text = ""
        Me.StratBoxRTForm.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'SuitedHandComboBoxRTForm
        '
        Me.SuitedHandComboBoxRTForm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.SuitedHandComboBoxRTForm.Items.AddRange(New Object() {"Mixed Suits", "Spades", "Hearts", "Diamonds", "Clubs"})
        Me.SuitedHandComboBoxRTForm.Location = New System.Drawing.Point(0, 129)
        Me.SuitedHandComboBoxRTForm.Name = "SuitedHandComboBoxRTForm"
        Me.SuitedHandComboBoxRTForm.Size = New System.Drawing.Size(96, 24)
        Me.SuitedHandComboBoxRTForm.TabIndex = 168
        Me.SuitedHandComboBoxRTForm.Visible = False
        '
        'ALabelRTForm
        '
        Me.ALabelRTForm.Location = New System.Drawing.Point(326, 18)
        Me.ALabelRTForm.Name = "ALabelRTForm"
        Me.ALabelRTForm.Size = New System.Drawing.Size(87, 19)
        Me.ALabelRTForm.TabIndex = 169
        Me.ALabelRTForm.Text = "Ace = A, a, 1"
        '
        'TLabelRTForm
        '
        Me.TLabelRTForm.Location = New System.Drawing.Point(326, 37)
        Me.TLabelRTForm.Name = "TLabelRTForm"
        Me.TLabelRTForm.Size = New System.Drawing.Size(87, 18)
        Me.TLabelRTForm.TabIndex = 170
        Me.TLabelRTForm.Text = "Ten = T, t, 0"
        '
        'BJCASmallRealTimeForm
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.ClientSize = New System.Drawing.Size(422, 283)
        Me.Controls.Add(Me.TLabelRTForm)
        Me.Controls.Add(Me.ALabelRTForm)
        Me.Controls.Add(Me.SuitedHandComboBoxRTForm)
        Me.Controls.Add(Me.StratBoxRTForm)
        Me.Controls.Add(Me.NewEVLabelRTForm)
        Me.Controls.Add(Me.NewEVBoxRTForm)
        Me.Controls.Add(Me.StratLabelRTForm)
        Me.Controls.Add(Me.InsuranceBoxRTForm)
        Me.Controls.Add(Me.SplitButtonRTForm)
        Me.Controls.Add(Me.NewHandButtonRTForm)
        Me.Controls.Add(Me.OPHandsLabelRTForm)
        Me.Controls.Add(Me.PHandLabelRTForm)
        Me.Controls.Add(Me.ResetShoeCheckRTForm)
        Me.Controls.Add(Me.OriginalShoeButtonRTForm)
        Me.Controls.Add(Me.DHandLabelRTForm)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "BJCASmallRealTimeForm"
        Me.Text = "MGP's BJ CA Realtime Analysis"
        Me.ResumeLayout(False)

    End Sub

#End Region

#Region " Initialization "

    Private Sub InitializeForm()
        PopulateShoeTable()
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
            box.Visible = False

            If card < 5 Then
                box.Location = New System.Drawing.Point(316, 248 + 36 * card)
            Else
                box.Location = New System.Drawing.Point(412, 248 + 36 * (card - 5))
            End If

            CurrentShoeBoxesArray(card) = box
            Me.Controls.Add(box)

            'Add Handler to the general handler
            AddHandler box.Validating, AddressOf CurrentShoeBoxesArrayHandler_Validating

        Next card
    End Sub

    Private Sub ResetShoeCheckRTForm_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ResetShoeCheckRTForm.CheckedChanged
        If ResetShoeCheckRTForm.Checked Then
            OriginalShoeButtonRTForm.Visible = False
        Else
            OriginalShoeButtonRTForm.Visible = True
        End If
    End Sub

    Private Sub GetShoeForm(ByRef shoe As BJCAShoeClass)
        Dim card As Integer

        For card = 1 To 10
            shoe.Cards(card) = CInt(CurrentShoeBoxesArray(card - 1).Text)
        Next card
    End Sub

    Private Sub LoadShoeForm(ByRef shoe As BJCAShoeClass)
        Dim card As Integer

        For card = 1 To 10
            CurrentShoeBoxesArray(card - 1).Text = shoe.Cards(card)
        Next card
    End Sub

    Private Sub CurrentShoeBoxesArrayHandler_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles CurrentShoeBoxesArrayHandler.Validating
        If Not CheckValidInteger(DirectCast(sender, IndexedTextBox).Text, 0, OriginalShoe.Cards(DirectCast(sender, IndexedTextBox).Index), True) Then
            DirectCast(sender, IndexedTextBox).Text = CurrentHandShoe.Cards(DirectCast(sender, IndexedTextBox).Index)
            e.Cancel = True
            Exit Sub
        End If
        GetShoeForm(CurrentHandShoe)
        GetCurrentHandShoeForm(True, True, True)
        LoadHandEVs()
    End Sub

    Private Sub OriginalShoeButtonRTForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OriginalShoeButtonRTForm.Click
        ResetOriginalShoeForm()
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
        box.Location = New System.Drawing.Point(144, 16)
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
            box.Location = New System.Drawing.Point(144, 64 + 28 * hand)

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
            box.Location = New System.Drawing.Point(280, 64 + 28 * hand)

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
            box.Location = New System.Drawing.Point(312, 64 + 28 * hand)

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
            box.Location = New System.Drawing.Point(344, 64 + 28 * hand)

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

        For player = 0 To 0
            Dim box As New IndexedTextBox

            box.ImeMode = System.Windows.Forms.ImeMode.On
            box.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
            box.Size = New System.Drawing.Size(128, 20)
            box.Index = player

            box.Location = New System.Drawing.Point(144, 40 + 28 * player)

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

        For hand = 0 To 0
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
        For hand = 0 To 0
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
            For hand = 0 To 0
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
            p10 = CurrentHandShoe.Cards(10) / CurrentHandShoe.CardsLeft
            insuranceEV = (-(1 - p10) + InsPays * p10)
            FillNumberTextBox(InsuranceBoxRTForm, insuranceEV, 7, False)
            If insuranceEV > 0 Then
                InsuranceBoxRTForm.Text = "Insure"
            ElseIf insuranceEV = 0 Then
                InsuranceBoxRTForm.Text = ""
            Else
                InsuranceBoxRTForm.Text = "Don't Insure"
            End If
        ElseIf DealersHand.Text = "" Then
            InsuranceBoxRTForm.Visible = False
        Else
            InsuranceBoxRTForm.Visible = False
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
                            SplitButtonRTForm.Location = New System.Drawing.Point(152, 92 + 28 * CurrentSPL)
                        Else
                            SplitButtonRTForm.Visible = False
                        End If

                        PlayerHandsStrings(CurrentPHandIndex) = DirectCast(sender, IndexedTextBox).Text
                    End If
                Else
                    'Check for the possibility of a split
                    If SplitOK(Rules, PlayerHands(CurrentPHandIndex).Hand) Then
                        SplitButtonRTForm.Visible = True
                        SplitButtonRTForm.Location = New System.Drawing.Point(152, 92 + 28 * CurrentSPL)
                    Else
                        SplitButtonRTForm.Visible = False
                    End If

                    PlayerHandsStrings(CurrentPHandIndex) = DirectCast(sender, IndexedTextBox).Text
                End If
            ElseIf Not ClearingForm Then
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
        InsuranceBoxRTForm.Visible = False
    End Sub

    Private Sub ShowHandEVs()
        NewEVLabelRTForm.Visible = False
        NewEVBoxRTForm.Visible = False
        StratLabelRTForm.Visible = True
        StratBoxRTForm.Visible = True
    End Sub

    Private Sub ClearHandEVs()
        PlayerHandTotalBoxes(0).Text = ""
        FillNumberTextBox(PlayerHandStratBoxes(0), 0, 3, False)
        FillNumberTextBox(PlayerHandEVBoxes(0), 0, 3, False)

        FillNumberTextBox(StratBoxRTForm, 0, 7, False)
    End Sub

    Private Sub LoadHandEVs()
        Dim tempIndex As Integer
        Dim handIndex As Integer
        Dim newEvs As BJCAStratHandEVsClass

        If Not ClearingForm Then
            tempIndex = CurrentPHandIndex
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

                CurrentPHandIndex = tempIndex
                newEvs = PlayerHandEVs(CurrentPHandIndex).Hand
                If PlayerHands(CurrentPHandIndex).Hand.Total > 21 Then
                    StratBoxRTForm.Text = "Busted"
                    StratBoxRTForm.BackColor = System.Drawing.Color.Orange
                ElseIf PlayerHands(CurrentPHandIndex).Hand.NumCards < 2 Then
                    ClearHandEVs()
                Else
                    FillStratTextBox(StratBoxRTForm, newEvs.Strat(CurrentUpcard), True, Rules.ColorTable)
                End If
            Else
                ClearHandEVs()
            End If
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
                newEvs = PlayerHandEVs(CurrentPHandIndex).Hand

                FillStratTextBox(PlayerHandStratBoxes(CurrentPHandIndex), PlayerHandEVs(CurrentPHandIndex).SuitedEVs.SuitedStrat(CurrentUpcard, suit), False, Rules.ColorTable)
                FillNumberTextBox(PlayerHandEVBoxes(CurrentPHandIndex), PlayerHandEVs(CurrentPHandIndex).SuitedEVs.SuitedStratEV(CurrentUpcard, suit), 3, False)

                FillStratTextBox(StratBoxRTForm, PlayerHandEVs(CurrentPHandIndex).SuitedEVs.SuitedStrat(CurrentUpcard, suit), True, Rules.ColorTable)
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
