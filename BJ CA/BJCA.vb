Imports BJ_CA.BJCAShared

<Serializable()> Public Class BJCA

#Region " Declarations "
    Public AOBBO As Boolean
    Public P21AutoWin As Boolean
    Public DDRType As Integer

    'Declarations needed for EOR's and Realtime Strategies
    Public OriginalRules As New BJCARulesClass

    'Strategy Declarations
    Public Opt As New BJCAStrategyClass
    Public TD As New BJCAStrategyClass
    Public TC As New BJCAStrategyClass
    Public Forced As New BJCAStrategyClass

    'General Declarations
    Public CardProb As CardProbDelegate = Nothing
    Public C As New BJCAGlobalsClass

    'Declarations for Probability Arrays
    Public BJHandNumerator(10, 30, 30) As Double       'Card, Cards Played, Cards in Hand
    Public BJHandDivisor(40, 40) As Double             'Cards played, Cards in hand

    'Player Hand Declarations
    Public NumPHands As Integer                        'Number of possible player hands
    Public PlayerHandTotal(21, 1) As Integer           'Count, Soft True/False
    Public PlayerHands(3084) As BJCAPlayerHandClass    '3072 = Max hands without busting (10 1 card, 2 0 hands)
    Public CurrentPHand As New BJCAHandClass           'Current player hand
    Public BJIndex As Integer                          'Index of player BJ
    Public DoubleNeeded(3084, 10) As Boolean
    Public SurrenderNeeded(3084, 10) As Boolean

    'Dealer Hand Declarations
    Public NumDHands As Integer                        'Number of possible dealer hands
    Public DealerHandTotal(21) As Integer              'List of dealer hands with a given count
    Public DealerHandTotalUC(10, 21) As Integer        'Upcard, count      Used during splits
    Public DealerHands(1740) As BJCADealerHandClass    '1740 = Max hands with H17, No check
    Public CurrentDHand As New BJCADealerHandClass     'Current dealer hand

    'Shoe Declarations
    Public CurrentShoe As New BJCAShoeClass
    Public OriginalShoe As New BJCAShoeClass
    Public InfiniteDeck As Boolean

    'Split Declarations
    Public NSplitHands As Integer                               'Hands used depending on SPL
    Public NSplitCalcs As Integer                               'Number of shoe states needed for splits
    Public SplitCalcHands(5) As Integer                         'Used to index the shoe states that need to be calculated
    Public NPxHands(11) As BJCANPxClass                         'Different possible NPx hands
    Public NRounds(3) As Integer                                'Rounds used depending on SPL
    Public SplitProbs(10, 10) As BJCASplitProbsClass            'Hand probs based on Paircard, upcard
    Public SplitRounds As New BJCASplitRoundsClass              'Different possible rounds
    Public SplitRoundProbs(10, 10) As BJCASplitRoundProbsClass  'Round probs based on Paircard, upcard

    'Bonus Declarations
    Public BonusRuleOn As Boolean
    Public NSuitedBonusHands As Integer             'Number of hands that a suited bonus directly applies
    Public NSuitedHands As Integer                  'Number of hands that can lead to a suited bonus
    Public SuitedBonusHands(3084) As Integer        'Array of indices for Suited bonus hands
    Public SuitedHandsList(3084) As Integer         'Array of indices for hands that can lead to a suited bonus

    'Exceptions Declarations
    Public ExceptionsList As New BJCAExceptionListClass
    Public NCardExceptionsList As New BJCANCardExceptionListClass

    'Rules Declarations
    Public MaxPlayerCards As Integer
    Public MaxDealerCards As Integer

    Public BJPays As Double
    Public StandOnSoft As Integer
    Public SurrPays As Double
    Public SurrDBJPays As Double

    Public ENHC As Boolean
    Public BBO As Boolean
    Public OBBO As Boolean
    Public CheckAce As Boolean
    Public CheckTen As Boolean

    Public SPL As Integer
    Public HSA As Boolean
    Public SMA As Boolean

    Public DAS As Boolean
    Public SAS As Boolean
    Public SAN As Boolean
    Public DSA As Boolean
    Public DDR As Boolean
    Public DDRPS As Boolean
    Public RDA As Boolean
    Public RDAPS As Boolean
    Public RDDepth As Integer

    Public SSA As Boolean
    Public SurrType As String
    Public MacauType As String
    Public BJSplitAces As Boolean
    Public BJSplitTens As Boolean

    Public CDP As Boolean
    Public CDPN As Boolean
    Public TDPlus As Boolean
    Public TCPlus As Boolean

    Public DSoftAllHard As Boolean
    Public DSoft19Hard As Boolean
    Public PDTies(22) As Double

    Public DeckType As String
    Public DoubleType As String
    Public DAN As Boolean

    Public ForcednCD As Integer
    Public ForcedTablePreSplit As Boolean
    Public ForcedTablePostSplit As Boolean

    Public UCAllowed(10) As Boolean
    Public SplitAllowed(10) As Boolean
    Public SplitIndex(10, 10) As Integer     'paircard, upcard  index of hand = 0 if not enough cards
    Public SplitTotalIndex(21, 1) As Integer 'Total, Soft+1     index of paircard if exists

    'Special Rules Declarations
    Public BJBonuses As New BJCABonusRulesClass
    Public BonusRulesList As New BJCABonusRulesListClass
    Public ForcedTableRulesList As New BJCAForcedRulesListClass
    Public ForcedRulesList As New BJCAForcedRulesListClass
    Public ColorTable As New BJCAColorTableClass

    Public PrintPSExceptions As Boolean
    Public UseDPDictionary As Boolean
    Public SaveDPDictionary As Boolean

    Public OutputPath As String
    Public ExcelFilePath As String
    Public ProbsPath As String

    Public DealerProbs As New BJCADealerProbsDictionary

    'Debugging Declarations
    Public RunningTime As Double
    Public RunningTime2 As Double
    Public RunningTime3 As Double
#End Region

#Region " BJCA Classes "

#End Region

    Public Sub BJCA(ByVal newRules As BJCARulesClass, Optional ByRef dpDict As BJCADealerProbsDictionary = Nothing)
        Dim tempBool As Boolean
        Dim i As Integer
        Dim j As Integer

        GetRules(newRules)
        Initialize(newRules)
        If UseDPDictionary Then
            If dpDict Is Nothing Then
                tempBool = LoadDealerProbsFile(DealerProbs, ProbsPath, (newRules.StandOnSoft = 17), OriginalRules.DeckType)
            Else
                DealerProbs = dpDict
            End If
        End If
        ApplyRulesStrat(newRules)
        ApplyForcedRules()

        ComputeHandProbsPreSplit()

        RunningTime = Environment.TickCount
        ComputeStand()
        RunningTime = Environment.TickCount - RunningTime
        ComputeBlackjack()
        ComputeSurrender()
        ComputeDouble()
        ApplyBonusRulesPreSuited()
        ComputeOptInitialStrat()
        ComputeOptHit()

        RunningTime2 = Environment.TickCount
        InitializeSplits()
        ComputeOptSplit()
        RunningTime2 = Environment.TickCount - RunningTime2

        RunningTime3 = Environment.TickCount
        ComputeStrategies()
        RunningTime3 = Environment.TickCount - RunningTime3

        EnumSuitedBonusHandList()
        EnumSuitedHandList()
        ApplyBJSuitedBonuses()
        ComputeSuitedHandNetEVs(Opt)

        ComputeExceptions()
        ComputeNCardExceptions()
        ComputeGameEVs()

        '        MsgBox("TD GameEV: " + Chr(9) + CStr(TD.GameEVs.NetGameEV) + Chr(13) + "2C GameEV: " + Chr(9) + CStr(TC.GameEVs.NetGameEV) + Chr(13) + "Forced GameEV: " + Chr(9) + CStr(Forced.GameEVs.NetGameEV) + Chr(13) + "Opt GameEV: " + Chr(9) + CStr(Opt.GameEVs.NetGameEV) + Chr(13) + Chr(13) + "Stand: " + Chr(9) + Chr(9) + CStr(RunningTime / 1000) + Chr(13) + "Split: " + Chr(9) + Chr(9) + CStr(RunningTime2 / 1000) + Chr(13) + "Strategy: " + Chr(9) + CStr(RunningTime3 / 1000), MsgBoxStyle.OKOnly)

        'If a Dealer Probs Dictionary is supplied, it is up to the sender to save it
        If (SaveDPDictionary And dpDict Is Nothing) Then tempBool = SaveDealerProbsFile(DealerProbs, ProbsPath, (StandOnSoft = 17), OriginalRules.DeckType)
    End Sub

#Region " General Methods "

    Public Delegate Function CardProbDelegate(ByVal card As Integer, ByVal upcard As Integer) As Double

    Public Function FiniteCardProb(ByVal card As Integer, ByVal upcard As Integer) As Double
        If upcard = 1 And CheckAce Then
            'Burn a Non-10 card away for dealer's second card since we know
            '  the dealer doesn't have BJ
            If card = 10 Then
                FiniteCardProb = CurrentShoe.Cards(10) / (CurrentShoe.CardsLeft - 1)
            Else
                FiniteCardProb = (CurrentShoe.Cards(card) - CurrentShoe.Cards(card) / (CurrentShoe.CardsLeft - CurrentShoe.Cards(10))) / (CurrentShoe.CardsLeft - 1)
            End If
        ElseIf upcard = 10 And CheckTen Then
            If card = 1 Then
                FiniteCardProb = CurrentShoe.Cards(1) / (CurrentShoe.CardsLeft - 1)
            Else
                FiniteCardProb = (CurrentShoe.Cards(card) - CurrentShoe.Cards(card) / (CurrentShoe.CardsLeft - CurrentShoe.Cards(1))) / (CurrentShoe.CardsLeft - 1)
            End If
        Else
            FiniteCardProb = CurrentShoe.Cards(card) / CurrentShoe.CardsLeft
        End If

        If FiniteCardProb < 0 Then
            FiniteCardProb = 0
        End If

    End Function

    Public Function InfiniteCardProb(ByVal card As Integer, ByVal upcard As Integer) As Double
        InfiniteCardProb = OriginalShoe.Cards(card) / OriginalShoe.CardsLeft
    End Function

    Private Function ConditionalValue(ByVal a As Double, ByVal AB As Double, ByVal pB As Double) As Double
        'EV(A|~B) = (EV(A)- EV(A|B)*p(B))/ (1-p(B))
        'p(A|~B)  = (p(A) - p(A|B)* p(B))/ (1-p(B))

        ConditionalValue = (a - AB * pB) / (1 - pB)
    End Function

    Private Sub DealPCard(ByVal card As Integer)
        CurrentPHand.Deal(card)
        CurrentShoe.Deal(card)
    End Sub

    Private Sub DealDCard(ByVal card As Integer)
        CurrentDHand.Hand.Deal(card)
        CurrentShoe.Deal(card)
    End Sub

    Private Sub UndealPCard(ByVal card As Integer)
        CurrentPHand.Undeal(card)
        CurrentShoe.Undeal(card)
    End Sub

    Private Sub UndealDCard(ByVal card As Integer)
        CurrentDHand.Hand.Undeal(card)
        CurrentShoe.Undeal(card)
    End Sub

    Public Function FindDealerHand(ByVal Total As Integer, ByVal currentHand As BJCAHandClass) As Integer
        Dim index As Integer
        Dim card As Integer
        Dim handFound As Boolean

        index = DealerHandTotal(Total)
        handFound = False
        Do While (Not handFound And index)
            'Checking numcards first saves a loop if the hand doesn't match
            If currentHand.NumCards <> DealerHands(index).Hand.NumCards Then
                index = DealerHands(index).NextHandTotal
            Else
                For card = 1 To 10
                    If currentHand.Cards(card) <> DealerHands(index).Hand.Cards(card) Then
                        index = DealerHands(index).NextHandTotal
                        handFound = False
                        Exit For
                    End If
                    handFound = True
                Next card
            End If
        Loop

        FindDealerHand = index
    End Function

    Public Function FindPlayerHand(ByVal currentHand As BJCAHandClass) As Integer
        Dim index As Integer
        Dim card As Integer
        Dim i As Integer

        'This routine uses a simple loop to find the index of the currenthand
        '   It is assumed that impossible hands or busted hands have been pre-screened
        index = 0
        For card = 1 To 10
            i = 0
            Do While i < currentHand.Cards(card)
                index = PlayerHands(index).HitHand(card)
                i = i + 1
            Loop
        Next card
        FindPlayerHand = index
    End Function

    Private Function GetHandProb(ByVal playedHand As BJCAHandClass, ByVal currentHand As BJCAHandClass, ByVal extracard As Integer, ByVal Multiplier As Double) As Double
        If extracard <> 0 Then
            If BJHandNumerator(extracard, playedHand.Cards(extracard), 1) > 0 Then
                Multiplier = Multiplier * BJHandDivisor(playedHand.NumCards, 1) / BJHandNumerator(extracard, playedHand.Cards(extracard), 1)
            Else
                Multiplier = 0
            End If
        End If
        GetHandProb = Multiplier * BJHandNumerator(1, playedHand.Cards(1), currentHand.Cards(1)) _
                                 * BJHandNumerator(2, playedHand.Cards(2), currentHand.Cards(2)) _
                                 * BJHandNumerator(3, playedHand.Cards(3), currentHand.Cards(3)) _
                                 * BJHandNumerator(4, playedHand.Cards(4), currentHand.Cards(4)) _
                                 * BJHandNumerator(5, playedHand.Cards(5), currentHand.Cards(5)) _
                                 * BJHandNumerator(6, playedHand.Cards(6), currentHand.Cards(6)) _
                                 * BJHandNumerator(7, playedHand.Cards(7), currentHand.Cards(7)) _
                                 * BJHandNumerator(8, playedHand.Cards(8), currentHand.Cards(8)) _
                                 * BJHandNumerator(9, playedHand.Cards(9), currentHand.Cards(9)) _
                                 * BJHandNumerator(10, playedHand.Cards(10), currentHand.Cards(10)) _
                                 / BJHandDivisor(playedHand.NumCards, currentHand.NumCards)
    End Function

    Private Function HandPossible(ByVal currentHand As BJCAHandClass) As Boolean
        HandPossible = BJHandNumerator(1, CurrentShoe.Hand.Cards(1), currentHand.Cards(1)) > 0 And _
                   BJHandNumerator(2, CurrentShoe.Hand.Cards(2), currentHand.Cards(2)) > 0 And _
                   BJHandNumerator(3, CurrentShoe.Hand.Cards(3), currentHand.Cards(3)) > 0 And _
                   BJHandNumerator(4, CurrentShoe.Hand.Cards(4), currentHand.Cards(4)) > 0 And _
                   BJHandNumerator(5, CurrentShoe.Hand.Cards(5), currentHand.Cards(5)) > 0 And _
                   BJHandNumerator(6, CurrentShoe.Hand.Cards(6), currentHand.Cards(6)) > 0 And _
                   BJHandNumerator(7, CurrentShoe.Hand.Cards(7), currentHand.Cards(7)) > 0 And _
                   BJHandNumerator(8, CurrentShoe.Hand.Cards(8), currentHand.Cards(8)) > 0 And _
                   BJHandNumerator(9, CurrentShoe.Hand.Cards(9), currentHand.Cards(9)) > 0 And _
                   BJHandNumerator(10, CurrentShoe.Hand.Cards(10), currentHand.Cards(10)) > 0
    End Function

    Private Function MultiCardPossible(ByVal card As Integer, ByVal nCards As Integer) As Boolean
        If CurrentShoe.Cards(card) < nCards Then
            MultiCardPossible = False
        Else
            CurrentShoe.Deal(card, nCards - 1)
            MultiCardPossible = (CardProb(card, 0) > 0)
            CurrentShoe.Undeal(card, nCards - 1)
        End If
    End Function

    Private Function GetPostSplitStrat(ByVal preSplitStrat As Integer, ByVal paircard As Integer, ByVal upcard As Integer, ByVal cstrat As BJCAStrategyClass, ByVal pHand As BJCAPlayerHandClass) As Integer
        Dim postSplitStrat As Integer

        'PD, PR should not be passed if the rules don't allow it
        If preSplitStrat = BJCAGlobalsClass.Strat.P Then
            preSplitStrat = cstrat.StratTD(pHand.Hand.Total, pHand.Hand.Soft + 1).Strat(upcard)
        End If
        postSplitStrat = preSplitStrat
        If paircard = 1 And Not (HSA Or DSA Or SSA) Then
            postSplitStrat = BJCAGlobalsClass.Strat.S
        Else
            Select Case preSplitStrat
                Case BJCAGlobalsClass.Strat.H, BJCAGlobalsClass.Strat.PH
                    If (paircard = 1 And Not HSA) Then
                        postSplitStrat = BJCAGlobalsClass.Strat.S
                    Else
                        postSplitStrat = BJCAGlobalsClass.Strat.H
                    End If
                Case BJCAGlobalsClass.Strat.D, BJCAGlobalsClass.Strat.PD
                    If paircard = 1 And (Not DSA Or Not pHand.DPostallowed(upcard)) Then
                        If Not HSA Then
                            postSplitStrat = BJCAGlobalsClass.Strat.S
                        Else
                            postSplitStrat = BJCAGlobalsClass.Strat.H
                        End If
                    ElseIf Not pHand.DPostallowed(upcard) Then
                        postSplitStrat = BJCAGlobalsClass.Strat.H
                    Else
                        postSplitStrat = BJCAGlobalsClass.Strat.D
                    End If
                Case BJCAGlobalsClass.Strat.DS
                    If (paircard = 1 And Not DSA) Or Not pHand.DPostallowed(upcard) Then
                        postSplitStrat = BJCAGlobalsClass.Strat.S
                    Else
                        postSplitStrat = BJCAGlobalsClass.Strat.D
                    End If
                Case BJCAGlobalsClass.Strat.R, BJCAGlobalsClass.Strat.PR
                    If paircard = 1 And (Not SSA Or pHand.RPostallowed(upcard) = 0) Then
                        If Not HSA Then
                            postSplitStrat = BJCAGlobalsClass.Strat.S
                        Else
                            postSplitStrat = BJCAGlobalsClass.Strat.H
                        End If
                    ElseIf pHand.RPostallowed(upcard) = 0 Then
                        postSplitStrat = BJCAGlobalsClass.Strat.H
                    Else
                        postSplitStrat = BJCAGlobalsClass.Strat.R
                    End If
                Case BJCAGlobalsClass.Strat.RS
                    If (paircard = 1 And Not SSA) Or pHand.RPostallowed(upcard) = 0 Then
                        postSplitStrat = BJCAGlobalsClass.Strat.S
                    Else
                        postSplitStrat = BJCAGlobalsClass.Strat.R
                    End If
                Case BJCAGlobalsClass.Strat.PS
                    postSplitStrat = BJCAGlobalsClass.Strat.S
            End Select
        End If

        Return postSplitStrat
    End Function

    Private Function GetHandEV(ByVal total As Integer, ByVal handEVs As BJCAHandEVsClass, ByVal upcard As Integer, ByVal bjadjprob As Double) As BJCAHandEVsClass
        Dim win As Double
        Dim lose As Double
        Dim push As Double
        Dim newEVs As New BJCAHandEVsClass

        Select Case total
            Case Is < 17
                lose = handEVs.DealerProbs(upcard, 17 - 17) + _
                    handEVs.DealerProbs(upcard, 18 - 17) + handEVs.DealerProbs(upcard, 19 - 17) + _
                    handEVs.DealerProbs(upcard, 20 - 17) + handEVs.DealerProbs(upcard, 21 - 17)
                push = 0
            Case 17
                lose = handEVs.DealerProbs(upcard, 18 - 17) + _
                    handEVs.DealerProbs(upcard, 19 - 17) + handEVs.DealerProbs(upcard, 20 - 17) + _
                    handEVs.DealerProbs(upcard, 21 - 17)
                push = handEVs.DealerProbs(upcard, 17 - 17)
            Case 18
                lose = handEVs.DealerProbs(upcard, 19 - 17) + _
                    handEVs.DealerProbs(upcard, 20 - 17) + handEVs.DealerProbs(upcard, 21 - 17)
                push = handEVs.DealerProbs(upcard, 18 - 17)
            Case 19
                lose = handEVs.DealerProbs(upcard, 20 - 17) + _
                    handEVs.DealerProbs(upcard, 21 - 17)
                push = handEVs.DealerProbs(upcard, 19 - 17)
            Case 20
                lose = handEVs.DealerProbs(upcard, 21 - 17)
                push = handEVs.DealerProbs(upcard, 20 - 17)
            Case 21
                'Player 21 Always wins
                If P21AutoWin Then
                    lose = 0
                    push = 0
                Else
                    lose = bjadjprob
                    push = handEVs.DealerProbs(upcard, 21 - 17) - bjadjprob
                End If
        End Select

        win = 1 - push - lose

        'Apply PDTies now
        newEVs.StandEV(upcard) = win - lose + PDTies(total) * push
        If PDTies(total) <> 0 Then
            newEVs.StandPushEV(upcard) = 0
        Else
            newEVs.StandPushEV(upcard) = push
        End If

        If ((upcard = 1 And Not CheckAce) Or (upcard = 10 And Not CheckTen)) And total = 21 And P21AutoWin Then
            newEVs.BJStandEV(upcard) = 1
        End If

        Return newEVs
    End Function

#End Region

#Region " Initialization Methods "

    Private Sub GetRules(ByVal newRules As BJCARulesClass)
        Dim total As Integer
        Dim card As Integer

        OriginalRules = CloneObject(newRules)

        CurrentShoe.Reset(newRules.Shoe)
        OriginalShoe.Reset(newRules.Shoe)

        BJPays = newRules.BJPays
        StandOnSoft = newRules.StandOnSoft
        SurrPays = newRules.SurrPays
        SurrDBJPays = newRules.SurrDBJPays

        ENHC = newRules.ENHC
        BBO = newRules.BBO
        OBBO = newRules.OBBO
        AOBBO = newRules.AOBBO
        CheckAce = newRules.CheckAce
        CheckTen = newRules.CheckTen

        SPL = newRules.SPL
        HSA = newRules.HSA
        SMA = newRules.SMA

        DAS = newRules.DAS
        SAS = newRules.SAS
        DSA = newRules.DSA
        SSA = newRules.SSA
        BJSplitAces = newRules.BJSPlitAces
        BJSplitTens = newRules.BJSplitTens

        CDP = newRules.CDP
        CDPN = newRules.CDPN
        TDPlus = newRules.TDPlus
        TCPlus = newRules.TCPlus

        DSoftAllHard = newRules.DSoftAllHard
        DSoft19Hard = newRules.DSoft19Hard
        DDR = newRules.DDR
        DDRPS = newRules.DDRPS
        DDRType = newRules.DDRType

        RDA = newRules.RDA
        RDAPS = newRules.RDAPS
        RDDepth = newRules.RDDepth

        P21AutoWin = newRules.P21Autowin
        For total = 17 To 22
            PDTies(total) = newRules.PDTies(total)
        Next total

        ForcednCD = newRules.ForcednCD
        ForcedTablePreSplit = newRules.ForcedTablePreSplit
        ForcedTablePostSplit = newRules.ForcedTablePostSplit
        ForcedTableRulesList = CloneObject(newRules.ForcedTableRulesList)
        ForcedRulesList = CloneObject(newRules.ForcedRulesList)

        SplitAllowed(0) = False
        For card = 1 To 10
            If OriginalShoe.Cards(card) > 0 Then
                UCAllowed(card) = newRules.UCAllowed(card)
                If OriginalShoe.Cards(card) > 1 And SPL > 0 Then
                    SplitAllowed(card) = newRules.SplitAllowed(card)
                End If
            Else
                UCAllowed(card) = False
                SplitAllowed(card) = False
            End If
        Next card

        BonusRulesList = CloneObject(newRules.BonusRulesList)
        ColorTable = CloneObject(newRules.ColorTable)
        BJBonuses = CloneObject(newRules.BJBonuses)

        BonusRuleOn = False
        If BonusRulesList.NumRules > 0 Then
            For card = 0 To BonusRulesList.NumRules - 1
                If BonusRulesList.L(card).RuleOn Then
                    BonusRuleOn = True
                    Exit For
                End If
            Next card
        End If

        Opt.ComputeStrat = True
        TD.ComputeStrat = newRules.ComputeTD
        TC.ComputeStrat = newRules.ComputeTC
        Forced.ComputeStrat = newRules.ComputeForced
        PrintPSExceptions = newRules.PrintPSExceptions

        DeckType = newRules.DeckType
        DoubleType = newRules.DoubleType
        SurrType = newRules.SurrType
        DAN = newRules.DAN

        SAN = newRules.SAN
        If newRules.MacauSurrenderAce And newRules.MacauSurrender2to10 Then
            MacauType = "True"
        ElseIf newRules.MacauSurrenderAce Then
            MacauType = "Ace Only"
        ElseIf newRules.MacauSurrender2to10 Then
            MacauType = "2 to 10 Only"
        Else
            MacauType = "False"
        End If

        OutputPath = newRules.OutputPath
        ExcelFilePath = newRules.ExcelFilePath
        ProbsPath = newRules.ProbsPath

        UseDPDictionary = newRules.UseDPDictionary
        SaveDPDictionary = newRules.SaveDPDictionary

    End Sub

    Private Sub Initialize(ByVal newRules As BJCARulesClass)
        Dim paircard As Integer
        Dim upcard As Integer
        Dim index As Integer

        MaxDealerCards = 22
        MaxPlayerCards = 22

        If newRules.InfiniteDecks Then
            CardProb = New CardProbDelegate(AddressOf InfiniteCardProb)
            InfiniteDeck = True
        Else
            CardProb = New CardProbDelegate(AddressOf FiniteCardProb)
            InfiniteDeck = False
        End If

        NumPHands = 1
        NumDHands = 0
        PlayerHands(0) = New BJCAPlayerHandClass

        EnumBJHandNumerator(newRules.InfiniteDecks)
        EnumBJHandDivisor(newRules.InfiniteDecks)
        EnumDealerHands()
        EnumPlayerHands(0, 1)
        LinkPlayerHands()
        EnumSplitHands()

        Opt.Name = "CD"
        TD.Name = "TD"
        TC.Name = "TC"
        Forced.Name = "Forced"

        Opt.NCardsCD = 22
        TD.NCardsCD = 0
        TC.NCardsCD = 2
        Forced.NCardsCD = ForcednCD

        For paircard = 1 To 10
            If SPL > 0 And SplitAllowed(paircard) And CardProb(paircard, 0) > 0 Then
                DealPCard(paircard)
                If CardProb(paircard, 0) > 0 Then
                    DealPCard(paircard)
                    index = FindPlayerHand(CurrentPHand)
                    SplitTotalIndex(CurrentPHand.Total, CurrentPHand.Soft + 1) = index
                    For upcard = 1 To 10
                        If UCAllowed(upcard) And CardProb(upcard, 0) > 0 Then
                            PlayerHands(index).PAllowed(upcard) = True
                            SplitIndex(paircard, upcard) = index
                            Opt.HandEVs(index).PAllowed(upcard) = True
                            If TD.ComputeStrat Then TD.HandEVs(index).PAllowed(upcard) = True
                            If TC.ComputeStrat Then TC.HandEVs(index).PAllowed(upcard) = True
                            If Forced.ComputeStrat Then Forced.HandEVs(index).PAllowed(upcard) = True
                        Else
                            SplitIndex(paircard, upcard) = 0
                        End If
                    Next upcard
                    UndealPCard(paircard)
                Else
                    For upcard = 1 To 10
                        SplitIndex(paircard, upcard) = 0
                    Next upcard
                End If
                UndealPCard(paircard)
            Else
                For upcard = 1 To 10
                    SplitIndex(paircard, upcard) = 0
                Next upcard
            End If
        Next paircard
    End Sub

    Public Function LoadDealerProbsFile(ByRef dpDict As BJCADealerProbsDictionary, ByVal dpPath As String, ByVal s17 As Boolean, ByVal dType As String, Optional ByVal rtstring As String = "") As Boolean
        Dim standstr As String
        Dim loaded As Boolean

        If s17 Then
            standstr = "S17"
        Else
            standstr = "H17"
        End If

        loaded = True
        Try
            dpDict = CType(LoadObjectFile(dpPath + "\DealerProbs " + rtstring + dType + " " + standstr + ".dat"), BJCADealerProbsDictionary)
        Catch
            loaded = False
        End Try

        Return loaded
    End Function

    Public Function SaveDealerProbsFile(ByRef dpDict As BJCADealerProbsDictionary, ByVal dpPath As String, ByVal s17 As Boolean, ByVal dType As String, Optional ByVal rtstring As String = "") As Boolean
        Dim standstr As String
        Dim saved As Boolean

        If s17 Then
            standstr = "S17"
        Else
            standstr = "H17"
        End If

        saved = True
        Try
            SaveObjectFile(dpPath + "\DealerProbs " + rtstring + dType + " " + standstr + ".dat", dpDict)
        Catch
            saved = False
        End Try

        Return saved
    End Function

    Private Sub ApplyRulesStrat(ByVal newRules As BJCARulesClass, Optional ByVal postSplit As Boolean = False)
        Dim index As Integer
        Dim upcard As Integer
        Dim card As Integer

        For index = 1 To NumPHands
            If PlayerHands(index).Hand.NumCards > 1 Then
                For upcard = 1 To 10
                    If UCAllowed(upcard) Then
                        'Apply Double rules from Double Table
                        'Total, 2CH-MultiH-2CS-MultiS, Pre/Post Split
                        Select Case PlayerHands(index).Hand.NumCards
                            Case 2
                                If PlayerHands(index).Hand.Soft Then
                                    PlayerHands(index).DPreallowed(upcard) = newRules.DoubleTable(PlayerHands(index).Hand.Total, 2, 0)
                                    PlayerHands(index).DPostallowed(upcard) = newRules.DoubleTable(PlayerHands(index).Hand.Total, 2, 1)
                                Else
                                    PlayerHands(index).DPreallowed(upcard) = newRules.DoubleTable(PlayerHands(index).Hand.Total, 0, 0)
                                    PlayerHands(index).DPostallowed(upcard) = newRules.DoubleTable(PlayerHands(index).Hand.Total, 0, 1)
                                End If
                            Case Is > 2
                                If PlayerHands(index).Hand.Soft Then
                                    PlayerHands(index).DPreallowed(upcard) = newRules.DoubleTable(PlayerHands(index).Hand.Total, 3, 0)
                                    PlayerHands(index).DPostallowed(upcard) = newRules.DoubleTable(PlayerHands(index).Hand.Total, 3, 1)
                                Else
                                    PlayerHands(index).DPreallowed(upcard) = newRules.DoubleTable(PlayerHands(index).Hand.Total, 1, 0)
                                    PlayerHands(index).DPostallowed(upcard) = newRules.DoubleTable(PlayerHands(index).Hand.Total, 1, 1)
                                End If
                        End Select

                        'Apply surrender rules from Surrender Table and surrender options
                        'Early surrender cannot be available after split if the dealer checks for BJ
                        Select Case PlayerHands(index).Hand.NumCards
                            Case 2
                                If (newRules.SurrenderTable(upcard) > 0) Then
                                    PlayerHands(index).RPreallowed(upcard) = newRules.SurrenderTable(upcard)
                                    If newRules.SAS Then
                                        If (upcard = 1 And CheckAce) Or (upcard = 10 And CheckTen) Then
                                            PlayerHands(index).RPostallowed(upcard) = BJCAGlobalsClass.Surr.LS
                                        Else
                                            PlayerHands(index).RPostallowed(upcard) = newRules.SurrenderTable(upcard)
                                        End If
                                    End If
                                End If
                            Case 5
                                If (upcard > 1 And newRules.MacauSurrender2to10) Or (upcard = 1 And newRules.MacauSurrenderAce) Then
                                    'Macau is Early Surrender if not checking, otherwise assume LS
                                    If (upcard = 1 And CheckAce) Or (upcard = 10 And CheckTen) Then
                                        PlayerHands(index).RPreallowed(upcard) = BJCAGlobalsClass.Surr.LS
                                        If newRules.SAS Then
                                            PlayerHands(index).RPostallowed(upcard) = BJCAGlobalsClass.Surr.LS
                                        End If
                                    Else
                                        PlayerHands(index).RPreallowed(upcard) = BJCAGlobalsClass.Surr.ES
                                        If newRules.SAS Then
                                            PlayerHands(index).RPostallowed(upcard) = BJCAGlobalsClass.Surr.ES
                                        End If
                                    End If
                                ElseIf newRules.SAN And (newRules.SurrenderTable(upcard) > 0) Then
                                    PlayerHands(index).RPreallowed(upcard) = newRules.SurrenderTable(upcard)
                                    If newRules.SAS Then
                                        If (upcard = 1 And CheckAce) Or (upcard = 10 And CheckTen) Then
                                            PlayerHands(index).RPostallowed(upcard) = BJCAGlobalsClass.Surr.LS
                                        Else
                                            PlayerHands(index).RPostallowed(upcard) = newRules.SurrenderTable(upcard)
                                        End If
                                    End If
                                End If
                            Case Else
                                If newRules.SAN And (newRules.SurrenderTable(upcard) > 0) Then
                                    If (upcard = 1 And CheckAce) Or (upcard = 10 And CheckTen) Then
                                        PlayerHands(index).RPreallowed(upcard) = BJCAGlobalsClass.Surr.LS
                                    Else
                                        PlayerHands(index).RPreallowed(upcard) = newRules.SurrenderTable(upcard)
                                    End If
                                    If newRules.SAS Then
                                        If (upcard = 1 And CheckAce) Or (upcard = 10 And CheckTen) Then
                                            PlayerHands(index).RPostallowed(upcard) = BJCAGlobalsClass.Surr.LS
                                        Else
                                            PlayerHands(index).RPostallowed(upcard) = newRules.SurrenderTable(upcard)
                                        End If
                                    End If
                                End If
                        End Select
                        If postSplit Then
                            PlayerHands(index).DPreallowed(upcard) = PlayerHands(index).DPostallowed(upcard)
                            PlayerHands(index).RPreallowed(upcard) = PlayerHands(index).RPostallowed(upcard)
                        End If

                        'Transfer the initial rules to the individual strategies
                        Opt.HandEVs(index).DPreallowed(upcard) = PlayerHands(index).DPreallowed(upcard)
                        Opt.HandEVs(index).DPostallowed(upcard) = PlayerHands(index).DPostallowed(upcard)
                        Opt.HandEVs(index).RPreallowed(upcard) = PlayerHands(index).RPreallowed(upcard)
                        Opt.HandEVs(index).RPostallowed(upcard) = PlayerHands(index).RPostallowed(upcard)

                        If TD.ComputeStrat Then
                            TD.HandEVs(index).DPreallowed(upcard) = PlayerHands(index).DPreallowed(upcard)
                            TD.HandEVs(index).DPostallowed(upcard) = PlayerHands(index).DPostallowed(upcard)
                            TD.HandEVs(index).RPreallowed(upcard) = PlayerHands(index).RPreallowed(upcard)
                            TD.HandEVs(index).RPostallowed(upcard) = PlayerHands(index).RPostallowed(upcard)
                        End If
                        If TC.ComputeStrat Then
                            TC.HandEVs(index).DPreallowed(upcard) = PlayerHands(index).DPreallowed(upcard)
                            TC.HandEVs(index).DPostallowed(upcard) = PlayerHands(index).DPostallowed(upcard)
                            TC.HandEVs(index).RPreallowed(upcard) = PlayerHands(index).RPreallowed(upcard)
                            TC.HandEVs(index).RPostallowed(upcard) = PlayerHands(index).RPostallowed(upcard)
                        End If
                        If Forced.ComputeStrat Then
                            Forced.HandEVs(index).DPreallowed(upcard) = PlayerHands(index).DPreallowed(upcard)
                            Forced.HandEVs(index).DPostallowed(upcard) = PlayerHands(index).DPostallowed(upcard)
                            Forced.HandEVs(index).RPreallowed(upcard) = PlayerHands(index).RPreallowed(upcard)
                            Forced.HandEVs(index).RPostallowed(upcard) = PlayerHands(index).RPostallowed(upcard)
                        End If

                        If PlayerHands(index).DPreallowed(upcard) Or (RDA And (PlayerHands(index).Hand.NumCards <= 2 + newRules.RDDepth)) Or (DDR And PlayerHands(index).Hand.NumCards <= 3) Then
                            DoubleNeeded(index, upcard) = True
                        End If

                        If PlayerHands(index).RPreallowed(upcard) > 0 Or DDR Then
                            SurrenderNeeded(index, upcard) = True
                        End If

                    End If
                Next upcard
            End If
        Next index
    End Sub

    Private Sub EnumBJHandNumerator(ByVal infinite As Boolean)
        Dim i As Integer
        Dim j As Integer
        Dim card As Integer
        Dim tempNumerator As Double
        Dim tempCardsLeft As Double

        For card = 1 To 10
            For i = 0 To 30
                tempCardsLeft = OriginalShoe.Cards(card) - i + 1
                If tempCardsLeft > 0 Then
                    tempNumerator = 1
                Else
                    tempNumerator = 0
                End If

                BJHandNumerator(card, i, 0) = tempNumerator
                For j = 1 To 30
                    If infinite Then
                        tempNumerator = tempNumerator * OriginalShoe.Cards(card)
                    Else
                        tempNumerator = tempNumerator * (tempCardsLeft - j)
                    End If
                    BJHandNumerator(card, i, j) = tempNumerator
                Next j
            Next i
        Next card
    End Sub

    Private Sub EnumBJHandDivisor(ByVal infinite As Boolean)
        Dim i As Integer
        Dim j As Integer
        Dim tempDivisor As Double
        Dim tempCardsLeft As Double

        For i = 0 To 35
            tempDivisor = 1
            tempCardsLeft = OriginalShoe.CardsLeft - i
            For j = 1 To 35
                If infinite Then
                    tempDivisor = tempDivisor * (OriginalShoe.CardsLeft)
                Else
                    tempDivisor = tempDivisor * (tempCardsLeft - j + 1)
                End If
                BJHandDivisor(i, j) = tempDivisor
            Next j
        Next i

        For i = 0 To 35
            For j = 0 To 35
                If BJHandDivisor(i, j) = 0 Then BJHandDivisor(i, j) = 1
            Next j
        Next i
    End Sub

    Private Sub EnumDealerHands()
        Dim upcard As Integer

        For upcard = 1 To 10
            If CardProb(upcard, 0) > 0 Then
                DealDCard(upcard)
                EnumDealerHandsAll(upcard)
                UndealDCard(upcard)
            End If
        Next upcard
    End Sub

    Private Sub EnumDealerHandsAll(ByVal upcard As Integer)
        'Adds any missing hands (e.g. 11 aces and a 6) and fixes the multipliers
        Dim card As Integer
        Dim index As Integer
        Dim temp As Integer

        For card = 1 To 10
            If CardProb(card, 0) > 0 Then 'Card present
                DealDCard(card)

                'If dealer's hand is already defined then find it and increase the multiplier for the upcard
                If CurrentDHand.Hand.Total >= StandOnSoft Or (Not CurrentDHand.Hand.Soft And CurrentDHand.Hand.Total >= 17) Then
                    If CurrentDHand.Hand.Total < 22 Then
                        index = FindDealerHand(CurrentDHand.Hand.Total, CurrentDHand.Hand)
                        If index Then 'Dealer's hand was found
                            DealerHands(index).Multiplier(upcard) = DealerHands(index).Multiplier(upcard) + 1

                            'Adds the current hand into the handcount chain if this is it's first incidence for the upcard
                            If DealerHands(index).Multiplier(upcard) = 1 Then
                                temp = DealerHandTotalUC(upcard, CurrentDHand.Hand.Total)
                                DealerHandTotalUC(upcard, CurrentDHand.Hand.Total) = index
                                DealerHands(index).NextHandTotalUC(upcard) = temp
                            End If
                        Else 'Dealer hand wasn't found - add it now.
                            NumDHands = NumDHands + 1
                            DealerHands(NumDHands) = New BJCADealerHandClass
                            DealerHands(NumDHands).Hand.Copy(CurrentDHand.Hand)

                            temp = DealerHandTotal(CurrentDHand.Hand.Total)
                            DealerHandTotal(CurrentDHand.Hand.Total) = NumDHands
                            DealerHands(NumDHands).NextHandTotal = temp

                            temp = DealerHandTotalUC(upcard, CurrentDHand.Hand.Total)
                            DealerHandTotalUC(upcard, CurrentDHand.Hand.Total) = NumDHands
                            DealerHands(NumDHands).NextHandTotalUC(upcard) = temp

                            DealerHands(NumDHands).Multiplier(upcard) = 1
                        End If
                    End If
                    'Do nothing if dealer busts
                ElseIf CurrentDHand.Hand.NumCards <= MaxDealerCards Then 'Keep dealing
                    EnumDealerHandsAll(upcard)
                End If
                UndealDCard(card)
            End If
        Next card
    End Sub

    Private Sub EnumPlayerHands(ByVal index As Integer, ByVal startCard As Integer)
        Dim card As Integer
        Dim i As Integer
        Dim temp As Integer

        For card = startCard To 10
            If CardProb(card, 0) > 0 Then
                DealPCard(card)
                If CurrentPHand.Total <= 21 And CurrentPHand.NumCards <= MaxPlayerCards Then
                    PlayerHands(index).HitHand(card) = NumPHands
                    PlayerHands(NumPHands) = New BJCAPlayerHandClass
                    PlayerHands(NumPHands).Hand.Copy(CurrentPHand)

                    If CurrentPHand.IsBJ Then
                        BJIndex = NumPHands
                    End If

                    Opt.HandEVs(NumPHands) = New BJCAHandStrategyClass
                    If TD.ComputeStrat Then TD.HandEVs(NumPHands) = New BJCAHandStrategyClass
                    If TC.ComputeStrat Then TC.HandEVs(NumPHands) = New BJCAHandStrategyClass
                    If Forced.ComputeStrat Then Forced.HandEVs(NumPHands) = New BJCAHandStrategyClass

                    For i = 1 To 10
                        PlayerHands(NumPHands).HitHand(i) = -1
                    Next i
                    If CurrentPHand.NumCards > 1 Then
                        temp = PlayerHandTotal(CurrentPHand.Total, CurrentPHand.Soft + 1)
                        PlayerHandTotal(CurrentPHand.Total, CurrentPHand.Soft + 1) = NumPHands
                        PlayerHands(NumPHands).NextHand = temp

                        temp = 1
                        For i = 1 To 10
                            If PlayerHands(NumPHands).Hand.Cards(i) > 0 Then
                                PlayerHands(NumPHands).PairIndex(i) = temp
                                temp = temp + 1
                            End If
                        Next i
                    End If
                    NumPHands = NumPHands + 1
                    EnumPlayerHands(NumPHands - 1, card)
                Else
                    PlayerHands(index).HitHand(card) = 0
                End If
                UndealPCard(card)
            End If
        Next card
    End Sub

    Private Sub LinkPlayerHands()
        Dim index As Integer
        Dim card As Integer

        'At the end of EnumPlayerHands, NumPHands is 1 too high
        NumPHands = NumPHands - 1

        'Hithand will be -1 if it needs to be linked , and 0 if the hithand was busted previously
        '   It will be linked if there are enough cards to hit and it's not busted, otherwise it will be set to 0
        For index = 1 To NumPHands
            CurrentPHand.Copy(PlayerHands(index).Hand)
            For card = 1 To 10
                If PlayerHands(index).HitHand(card) = -1 And CurrentPHand.Cards(card) < CurrentShoe.Cards(card) Then
                    DealPCard(card)
                    If CurrentPHand.Total <= 21 Then
                        PlayerHands(index).HitHand(card) = FindPlayerHand(CurrentPHand)
                    Else
                        PlayerHands(index).HitHand(card) = 0
                    End If
                    UndealPCard(card)
                ElseIf PlayerHands(index).HitHand(card) = -1 And CurrentPHand.Cards(card) >= CurrentShoe.Cards(card) Then
                    PlayerHands(index).HitHand(card) = 0
                End If
            Next card
        Next index
        CurrentPHand.Empty()
    End Sub

    Private Sub EnumSplitHands()
        'The original pair is assumed to have been removed
        '0   Full Deck
        '1   1P Removed
        '2   2P's Removed
        '3   1N Removed
        '4   1P, 1N Removed
        '5   3P's Removed
        '6   4P's Removed
        '7   2P's, 1N Removed
        '8   3P's, 1N Removed
        '9   1P, 2N's Removed
        '10  2P's, 2N's Removed

        'At present only 3 splits are supported
        Select Case SPL
            Case 1
                NSplitHands = 0
                NSplitCalcs = 1
            Case 2
                NSplitHands = 4
                NSplitCalcs = 3
            Case 3
                NSplitHands = 10
                NSplitCalcs = 5
        End Select

        'These are the only hands that need to be calculated directly, the other hands are conditioned upon these.
        SplitCalcHands(1) = 0
        SplitCalcHands(2) = 1
        SplitCalcHands(3) = 2
        SplitCalcHands(4) = 5
        SplitCalcHands(5) = 6

        'The hands are defined below
        NPxHands(0) = New BJCANPxClass
        NPxHands(0).Name = "-Full"
        NPxHands(0).NP = 0
        NPxHands(0).NN = 0
        NPxHands(0).Nx = 0
        NPxHands(0).UCH = -1
        NPxHands(0).CH = -1
        NPxHands(0).Used(0) = False
        NPxHands(0).Used(1) = True
        NPxHands(0).Used(2) = False
        NPxHands(0).Used(3) = False

        NPxHands(1) = New BJCANPxClass
        NPxHands(1).Name = "-P"
        NPxHands(1).NP = 1
        NPxHands(1).NN = 0
        NPxHands(1).Nx = 0
        NPxHands(1).UCH = 0
        NPxHands(1).CH = -1
        NPxHands(1).Used(0) = False
        NPxHands(1).Used(1) = False
        NPxHands(1).Used(2) = True
        NPxHands(1).Used(3) = False

        NPxHands(2) = New BJCANPxClass
        NPxHands(2).Name = "-PP"
        NPxHands(2).NP = 2
        NPxHands(2).NN = 0
        NPxHands(2).Nx = 0
        NPxHands(2).UCH = 1
        NPxHands(2).CH = -1
        NPxHands(2).Used(0) = False
        NPxHands(2).Used(1) = False
        NPxHands(2).Used(2) = False
        NPxHands(2).Used(3) = True

        NPxHands(3) = New BJCANPxClass
        NPxHands(3).Name = "-N"
        NPxHands(3).NP = 0
        NPxHands(3).NN = 1
        NPxHands(3).Nx = 0
        NPxHands(3).UCH = 0
        NPxHands(3).CH = 1
        NPxHands(3).Used(0) = False
        NPxHands(3).Used(1) = False
        NPxHands(3).Used(2) = True
        NPxHands(3).Used(3) = True

        NPxHands(4) = New BJCANPxClass
        NPxHands(4).Name = "-PN"
        NPxHands(4).NP = 1
        NPxHands(4).NN = 1
        NPxHands(4).Nx = 0
        NPxHands(4).UCH = 1
        NPxHands(4).CH = 2
        NPxHands(4).Used(0) = False
        NPxHands(4).Used(1) = False
        NPxHands(4).Used(2) = True
        NPxHands(4).Used(3) = False

        NPxHands(5) = New BJCANPxClass
        NPxHands(5).Name = "-PPP"
        NPxHands(5).NP = 3
        NPxHands(5).NN = 0
        NPxHands(5).Nx = 0
        NPxHands(5).UCH = 2
        NPxHands(5).CH = -1
        NPxHands(5).Used(0) = False
        NPxHands(5).Used(1) = False
        NPxHands(5).Used(2) = False
        NPxHands(5).Used(3) = False

        NPxHands(6) = New BJCANPxClass
        NPxHands(6).Name = "-PPPP"
        NPxHands(6).NP = 4
        NPxHands(6).NN = 0
        NPxHands(6).Nx = 0
        NPxHands(6).UCH = 5
        NPxHands(6).CH = -1
        NPxHands(6).Used(0) = False
        NPxHands(6).Used(1) = False
        NPxHands(6).Used(2) = False
        NPxHands(6).Used(3) = False

        NPxHands(7) = New BJCANPxClass
        NPxHands(7).Name = "-PPN"
        NPxHands(7).NP = 2
        NPxHands(7).NN = 1
        NPxHands(7).Nx = 0
        NPxHands(7).UCH = 2
        NPxHands(7).CH = 5
        NPxHands(7).Used(0) = False
        NPxHands(7).Used(1) = False
        NPxHands(7).Used(2) = False
        NPxHands(7).Used(3) = True

        NPxHands(8) = New BJCANPxClass
        NPxHands(8).Name = "-PPPN"
        NPxHands(8).NP = 3
        NPxHands(8).NN = 1
        NPxHands(8).Nx = 0
        NPxHands(8).UCH = 5
        NPxHands(8).CH = 6
        NPxHands(8).Used(0) = False
        NPxHands(8).Used(1) = False
        NPxHands(8).Used(2) = False
        NPxHands(8).Used(3) = False

        NPxHands(9) = New BJCANPxClass
        NPxHands(9).Name = "-PNN"
        NPxHands(9).NP = 1
        NPxHands(9).NN = 2
        NPxHands(9).Nx = 0
        NPxHands(9).UCH = 4
        NPxHands(9).CH = 7
        NPxHands(9).Used(0) = False
        NPxHands(9).Used(1) = False
        NPxHands(9).Used(2) = False
        NPxHands(9).Used(3) = True

        NPxHands(10) = New BJCANPxClass
        NPxHands(10).Name = "-PPNN"
        NPxHands(10).NP = 2
        NPxHands(10).NN = 2
        NPxHands(10).Nx = 0
        NPxHands(10).UCH = 7
        NPxHands(10).CH = 8
        NPxHands(10).Used(0) = False
        NPxHands(10).Used(1) = False
        NPxHands(10).Used(2) = False
        NPxHands(10).Used(3) = True
    End Sub

    Private Sub EnumSplitRounds()
        'Hands
        '0   Full Deck
        '1   1P Removed
        '2   2P's Removed
        '3   1N Removed
        '4   1P, 1N Removed
        '5   3P's Removed
        '6   4P's Removed
        '7   2P's, 1N Removed
        '8   3P's, 1N Removed
        '9  1P, 2N's Removed
        '10  2P's, 2N's Removed

        'Rounds
        'SPL1	    MGP	(spl2)				            Eric  (spl)
        'xx	        2*EV(x)					            2*EV(x) 
        '
        'SPL2	    MGP					                Eric 
        'NN	        EV(N) + EV(N-N)			            2*EV(N-N) 
        'Pxxx	    3*EV(x-P)				            3*EV(x-P) 
        'NPxx	    EV(N) + 2*EV(x-PN)		            EV(N-P) + 2*EV(x-PN) 
        '
        'SPL3	    MGP					                Eric 
        'NN	        EV(N) + EV(N-N)				        2*EV(N-N) 
        'PNNN	    EV(N-P) + EV(N-PN) + EV(N-PNN)		3*EV(N-PNN) 
        'NPNN	    EV(N) + EV(N-PN) + EV(N-PNN)		3*EV(N-PNN) 
        'PPxxxx	    4*EV(x-PP)				            4*EV(x-PP) 
        'PNPxxx	    EV(N-P) + 3*EV(x-PPN)			    EV(N-PP) + 3*EV(x-PPN) 
        'NPPxxx	    EV(N) + 3*EV(x-PPN)			        EV(N-PP) + 3*EV(x-PPN) 
        'PNNPxx	    EV(N-P) + EV(N-PN) + 2*EV(x-PPNN)	2*EV(N-PPN) + 2*EV(x-PPNN) 
        'NPNPxx	    EV(N) + EV(N-PN) + 2*EV(x-PPNN)		2*EV(N-PPN) + 2*EV(x-PPNN) 

        'These are the only hands that need to be calculated directly, the other hands are conditioned upon these.
        NRounds(0) = 0
        NRounds(1) = 1
        NRounds(2) = 3
        NRounds(3) = 8

        'SPL, Round, Hand, x/N

        'The rounds are defined below
        'SPL1
        'xx	        2*EV(x)					            2*EV(x) 
        SplitRounds.Name(1, 1) = "xx"
        SplitRounds.NN = 0
        SplitRounds.NP = 0
        SplitRounds.Nx = 2
        SplitRounds.Hands(1, 1, BJCAGlobalsClass.Hands.Full, 0) = 2

        'SPL2
        'NN	        EV(N) + EV(N-N)			            2*EV(N-N) 
        SplitRounds.Name(2, 1) = "NN"
        SplitRounds.NN = 2
        SplitRounds.NP = 0
        SplitRounds.Nx = 2
        SplitRounds.Hands(2, 1, BJCAGlobalsClass.Hands.N, 1) = 2
        'Pxxx	    3*EV(x-P)				            3*EV(x-P) 
        SplitRounds.Name(2, 2) = "Pxxx"
        SplitRounds.NN = 0
        SplitRounds.NP = 1
        SplitRounds.Nx = 3
        SplitRounds.Hands(2, 2, BJCAGlobalsClass.Hands.P, 0) = 3
        'NPxx	    EV(N) + 2*EV(x-PN)		            EV(N-P) + 2*EV(x-PN) 
        SplitRounds.Name(2, 3) = "NPxx"
        SplitRounds.NN = 1
        SplitRounds.NP = 1
        SplitRounds.Nx = 2
        SplitRounds.Hands(2, 3, BJCAGlobalsClass.Hands.P, 1) = 1
        SplitRounds.Hands(2, 3, BJCAGlobalsClass.Hands.PN, 0) = 2

        'SPL3
        'NN	        EV(N) + EV(N-N)			            2*EV(N-N) 
        SplitRounds.Name(3, 1) = "NN"
        SplitRounds.NN = 2
        SplitRounds.NP = 0
        SplitRounds.Nx = 0
        SplitRounds.Hands(3, 1, BJCAGlobalsClass.Hands.N, 1) = 2
        'PNNN	    EV(N-P) + EV(N-PN) + EV(N-PNN)		3*EV(N-PNN) 
        SplitRounds.Name(3, 2) = "PNNN"
        SplitRounds.NN = 3
        SplitRounds.NP = 1
        SplitRounds.Nx = 0
        SplitRounds.Hands(3, 2, BJCAGlobalsClass.Hands.PNN, 1) = 3
        'NPNN	    EV(N) + EV(N-PN) + EV(N-PNN)		3*EV(N-PNN) 
        SplitRounds.Name(3, 3) = "NPNN"
        SplitRounds.NN = 3
        SplitRounds.NP = 1
        SplitRounds.Nx = 0
        SplitRounds.Hands(3, 3, BJCAGlobalsClass.Hands.PNN, 1) = 3
        'PPxxxx	    4*EV(x-PP)				            4*EV(x-PP) 
        SplitRounds.Name(3, 4) = "PPxxxx"
        SplitRounds.NN = 0
        SplitRounds.NP = 2
        SplitRounds.Nx = 4
        SplitRounds.Hands(3, 4, BJCAGlobalsClass.Hands.PP, 0) = 4
        'PNPxxx	    EV(N-P) + 3*EV(x-PPN)			    EV(N-PP) + 3*EV(x-PPN) 
        SplitRounds.Name(3, 5) = "PNPxxx"
        SplitRounds.NN = 1
        SplitRounds.NP = 2
        SplitRounds.Nx = 3
        SplitRounds.Hands(3, 5, BJCAGlobalsClass.Hands.PP, 1) = 1
        SplitRounds.Hands(3, 5, BJCAGlobalsClass.Hands.PPN, 0) = 3
        'NPPxxx	    EV(N) + 3*EV(x-PPN)			        EV(N-PP) + 3*EV(x-PPN) 
        SplitRounds.Name(3, 6) = "PNPxxx"
        SplitRounds.NN = 1
        SplitRounds.NP = 2
        SplitRounds.Nx = 3
        SplitRounds.Hands(3, 6, BJCAGlobalsClass.Hands.PP, 1) = 1
        SplitRounds.Hands(3, 6, BJCAGlobalsClass.Hands.PPN, 0) = 3
        'PNNPxx	    EV(N-P) + EV(N-PN) + 2*EV(x-PPNN)	2*EV(N-PPN) + 2*EV(x-PPNN) 
        SplitRounds.Name(3, 7) = "PNNPxx"
        SplitRounds.NN = 2
        SplitRounds.NP = 2
        SplitRounds.Nx = 2
        SplitRounds.Hands(3, 7, BJCAGlobalsClass.Hands.PPN, 1) = 2
        SplitRounds.Hands(3, 7, BJCAGlobalsClass.Hands.PPNN, 0) = 2
        'NPNPxx	    EV(N) + EV(N-PN) + 2*EV(x-PPNN)		2*EV(N-PPN) + 2*EV(x-PPNN) 
        SplitRounds.Name(3, 8) = "NPNPxx"
        SplitRounds.NN = 2
        SplitRounds.NP = 2
        SplitRounds.Nx = 2
        SplitRounds.Hands(3, 8, BJCAGlobalsClass.Hands.PPN, 1) = 2
        SplitRounds.Hands(3, 8, BJCAGlobalsClass.Hands.PPNN, 0) = 2
    End Sub

    Public Sub ResetForcedRulesHands(ByRef cStrat As BJCAStrategyClass)
        Dim index As Integer
        Dim upcard As Integer

        For index = 1 To NumPHands
            'Reset the strategy - this is needed since the forced rules can be recalculated
            For upcard = 1 To 10
                If Not cStrat.HandEVs(index) Is Nothing Then
                    cStrat.HandEVs(index).EVs.Strat(upcard) = BJCAGlobalsClass.Strat.None
                    cStrat.HandEVs(index).EVs.StratEV(upcard) = 0
                    cStrat.HandEVs(index).EVs.StratPushEV(upcard) = 0

                    cStrat.HandEVs(index).SPreallowed(upcard) = True
                    cStrat.HandEVs(index).HPreallowed(upcard) = True
                    cStrat.HandEVs(index).PAllowed(upcard) = PlayerHands(index).PAllowed(upcard)
                    cStrat.HandEVs(index).DPreallowed(upcard) = PlayerHands(index).DPreallowed(upcard)
                    cStrat.HandEVs(index).RPreallowed(upcard) = PlayerHands(index).RPreallowed(upcard)
                    cStrat.HandEVs(index).PreForced(upcard) = False
                    cStrat.HandEVs(index).HandUsed(upcard) = 0
                    cStrat.HandEVs(index).Multiplier(upcard) = 0
                    cStrat.HandEVs(index).SplitEV(upcard) = 0

                    cStrat.HandEVs(index).SPostallowed(upcard) = True
                    cStrat.HandEVs(index).HPostallowed(upcard) = True
                    cStrat.HandEVs(index).DPostallowed(upcard) = PlayerHands(index).DPostallowed(upcard)
                    cStrat.HandEVs(index).RPostallowed(upcard) = PlayerHands(index).RPostallowed(upcard)
                    cStrat.HandEVs(index).PostForced(upcard) = False
                    cStrat.HandEVs(index).EVs.ForcedPostStrat(upcard) = BJCAGlobalsClass.Strat.None
                    '                cStrat.HandEVs(index).HandUsedPost(upcard) = 0
                End If
            Next upcard

        Next
    End Sub

    Public Sub ApplyForcedRules()
        Dim index As Integer
        Dim rule As Integer
        Dim upcard As Integer
        Dim total As Integer
        Dim apply As Boolean

        If Forced.ComputeStrat Then
            'Clear out the Forced TD Strategies which are needed for the NCard strategies
            For total = 5 To 21
                For upcard = 1 To 10
                    If Forced.StratTD(total, False + 1) Is Nothing Then
                        Forced.StratTD(total, False + 1) = New BJCATDStratClass
                    End If
                    If total > 12 And Forced.StratTD(total, True + 1) Is Nothing Then
                        Forced.StratTD(total, True + 1) = New BJCATDStratClass
                    End If
                Next upcard
            Next total

            'Reset the Forced Strategy before applying the rules
            ResetForcedRulesHands(Forced)

            'First apply all the Forced Table TD Rules
            For rule = 0 To ForcedTableRulesList.NumRules - 1
                If ForcedTableRulesList.L(rule).RuleOn Then
                    If Not (ForcedTableRulesList.L(rule).UseSpecificHand And ForcedTableRulesList.L(rule).ExactMatch) Then
                        Forced.StratTD(ForcedTableRulesList.L(rule).Hand.Total, ForcedTableRulesList.L(rule).Hand.Soft + 1).Strat(ForcedTableRulesList.L(rule).Upcard) = ForcedTableRulesList.L(rule).Strat
                        ForcedTableRulesList.L(rule).Index = 0

                        index = PlayerHandTotal(ForcedTableRulesList.L(rule).Hand.Total, ForcedTableRulesList.L(rule).Hand.Soft + 1)
                        Do While index
                            'Don't apply general rules to pairs
                            If PlayerHands(index).Hand.NumCards > 1 And Not PlayerHands(index).Hand.IsPair And (Forced.NCardsCD = 0 Or PlayerHands(index).Hand.NumCards > Forced.NCardsCD) Then
                                ApplyForcedRuleHand(Forced, index, ForcedTableRulesList.L(rule))
                            End If
                            index = PlayerHands(index).NextHand
                        Loop
                    End If
                End If
            Next rule

            'Then find the hand indices for the table's CD Hands
            For rule = 0 To ForcedTableRulesList.NumRules - 1
                If ForcedTableRulesList.L(rule).RuleOn Then
                    If ForcedTableRulesList.L(rule).UseSpecificHand And ForcedTableRulesList.L(rule).ExactMatch Then
                        index = FindPlayerHand(ForcedTableRulesList.L(rule).Hand)
                        ForcedTableRulesList.L(rule).Index = index
                        ApplyForcedRuleHand(Forced, index, ForcedTableRulesList.L(rule))
                    End If
                End If
            Next rule

            'Find the hand indices for the the non-table CD Hands
            For rule = 0 To ForcedRulesList.NumRules - 1
                If ForcedRulesList.L(rule).RuleOn Then
                    If ForcedRulesList.L(rule).UseSpecificHand And ForcedRulesList.L(rule).ExactMatch Then
                        index = FindPlayerHand(ForcedRulesList.L(rule).Hand)
                        ForcedRulesList.L(rule).Index = index
                        ApplyForcedRuleHand(Forced, index, ForcedRulesList.L(rule))
                    Else
                        ForcedRulesList.L(rule).Index = 0
                        For index = 1 To NumPHands
                            If PlayerHands(index).Hand.NumCards > 1 Then
                                apply = False

                                'The hand does not require an exact match so don't apply it to pairs
                                If Not PlayerHands(index).Hand.IsPair Then
                                    If ForcedRulesList.L(rule).UseSpecificHand And (Not ForcedRulesList.L(rule).ExactMatch And PlayerHands(index).Hand.Includes(ForcedRulesList.L(rule).Hand)) Then
                                        'This will take care of specific hand rules
                                        apply = True
                                    Else
                                        'Finally check for rules that don't require specific hands
                                        If (PlayerHands(index).Hand.Total = ForcedRulesList.L(rule).Hand.Total) Or (ForcedRulesList.L(rule).Hand.Total = 0) Then
                                            If ((Not ForcedRulesList.L(rule).Hand.Soft And Not PlayerHands(index).Hand.Soft) Or (ForcedRulesList.L(rule).Hand.Soft And PlayerHands(index).Hand.Soft)) Then
                                                If (ForcedRulesList.L(rule).Hand.NumCards = 0) Or (ForcedRulesList.L(rule).Hand.NumCards = PlayerHands(index).Hand.NumCards) Or (ForcedRulesList.L(rule).OrLess And PlayerHands(index).Hand.NumCards <= ForcedRulesList.L(rule).Hand.NumCards) Or (ForcedRulesList.L(rule).OrMore And PlayerHands(index).Hand.NumCards >= ForcedRulesList.L(rule).Hand.NumCards) Then
                                                    apply = True
                                                End If
                                            End If
                                        End If
                                    End If
                                End If

                                If apply Then
                                    ApplyForcedRuleHand(Forced, index, ForcedRulesList.L(rule))
                                End If
                            End If
                        Next index
                    End If
                End If
            Next rule

            'Now that all the rules have been applied, adjust them for the current rules
            For index = 1 To NumPHands
                If PlayerHands(index).Hand.NumCards > 1 Then
                    For upcard = 1 To 10
                        If Forced.HandEVs(index).PreForced(upcard) Or Forced.HandEVs(index).PostForced(upcard) Then
                            ApplyForcedRuleHandUpcard(Forced, index, upcard)
                        End If
                    Next upcard
                End If
            Next index
        End If
    End Sub

    Public Sub ApplyForcedRuleHand(ByRef cStrat As BJCAStrategyClass, ByVal index As Integer, ByVal rule As BJCAForcedRulesClass)
        Dim minUC As Integer
        Dim maxUC As Integer
        Dim upcard As Integer

        If rule.Upcard = 0 Then
            minUC = 1
            maxUC = 10
        Else
            minUC = rule.Upcard
            maxUC = rule.Upcard
        End If

        For upcard = minUC To maxUC
            If rule.PreSplit Then
                cStrat.HandEVs(index).PreForced(upcard) = True
                cStrat.HandEVs(index).EVs.Strat(upcard) = rule.Strat
            End If
            If rule.PostSplit Then
                cStrat.HandEVs(index).PostForced(upcard) = True
                cStrat.HandEVs(index).EVs.ForcedPostStrat(upcard) = rule.Strat
            End If
        Next upcard
    End Sub

    Private Sub ApplyForcedRuleHandUpcard(ByRef cStrat As BJCAStrategyClass, ByVal index As Integer, ByVal upcard As Integer)
        Dim card As Integer

        If cStrat.HandEVs(index).PreForced(upcard) Then
            Select Case cStrat.HandEVs(index).EVs.Strat(upcard)
                Case BJCAGlobalsClass.Strat.None
                    cStrat.HandEVs(index).SPreallowed(upcard) = True
                    cStrat.HandEVs(index).HPreallowed(upcard) = True
                    cStrat.HandEVs(index).PAllowed(upcard) = True
                    cStrat.HandEVs(index).DPreallowed(upcard) = PlayerHands(index).DPreallowed(upcard)
                    cStrat.HandEVs(index).RPreallowed(upcard) = PlayerHands(index).RPreallowed(upcard)
                    cStrat.HandEVs(index).PreForced(upcard) = False
                    cStrat.HandEVs(index).EVs.Strat(upcard) = BJCAGlobalsClass.Strat.None
                Case BJCAGlobalsClass.Strat.S, BJCAGlobalsClass.Strat.H
                Case BJCAGlobalsClass.Strat.D
                    If Not cStrat.HandEVs(index).DPreallowed(upcard) Then
                        cStrat.HandEVs(index).EVs.Strat(upcard) = BJCAGlobalsClass.Strat.H
                    End If
                Case BJCAGlobalsClass.Strat.DS
                    If Not cStrat.HandEVs(index).DPreallowed(upcard) Then
                        cStrat.HandEVs(index).EVs.Strat(upcard) = BJCAGlobalsClass.Strat.S
                    End If
                Case BJCAGlobalsClass.Strat.R
                    If cStrat.HandEVs(index).RPreallowed(upcard) = 0 Then
                        cStrat.HandEVs(index).EVs.Strat(upcard) = BJCAGlobalsClass.Strat.H
                    End If
                Case BJCAGlobalsClass.Strat.RS
                    If cStrat.HandEVs(index).RPreallowed(upcard) = 0 Then
                        cStrat.HandEVs(index).EVs.Strat(upcard) = BJCAGlobalsClass.Strat.S
                    End If
                Case BJCAGlobalsClass.Strat.P, BJCAGlobalsClass.Strat.PS, BJCAGlobalsClass.Strat.PD, BJCAGlobalsClass.Strat.PH, BJCAGlobalsClass.Strat.PR
                    For card = 1 To 10
                        If PlayerHands(index).Hand.Cards(card) = 2 Then
                            Exit For
                        End If
                    Next card
                    If SplitIndex(card, upcard) > 0 Then
                        Select Case cStrat.HandEVs(index).EVs.Strat(upcard)
                            Case BJCAGlobalsClass.Strat.P
                                cStrat.HandEVs(index).PostForced(upcard) = False
                                cStrat.HandEVs(index).EVs.ForcedPostStrat(upcard) = BJCAGlobalsClass.Strat.None
                            Case BJCAGlobalsClass.Strat.PS
                                cStrat.HandEVs(index).PostForced(upcard) = True
                                cStrat.HandEVs(index).EVs.ForcedPostStrat(upcard) = BJCAGlobalsClass.Strat.S
                            Case BJCAGlobalsClass.Strat.PH
                                cStrat.HandEVs(index).PostForced(upcard) = True
                                cStrat.HandEVs(index).EVs.ForcedPostStrat(upcard) = BJCAGlobalsClass.Strat.H
                            Case BJCAGlobalsClass.Strat.PD
                                If cStrat.HandEVs(index).DPostallowed(upcard) Then
                                    cStrat.HandEVs(index).PostForced(upcard) = True
                                    cStrat.HandEVs(index).EVs.ForcedPostStrat(upcard) = BJCAGlobalsClass.Strat.D
                                Else
                                    cStrat.HandEVs(index).PostForced(upcard) = False
                                    cStrat.HandEVs(index).EVs.ForcedPostStrat(upcard) = BJCAGlobalsClass.Strat.None
                                End If
                            Case BJCAGlobalsClass.Strat.PR
                                If cStrat.HandEVs(index).RPostallowed(upcard) > 0 Then
                                    cStrat.HandEVs(index).PostForced(upcard) = True
                                    cStrat.HandEVs(index).EVs.ForcedPostStrat(upcard) = BJCAGlobalsClass.Strat.R
                                Else
                                    cStrat.HandEVs(index).PostForced(upcard) = False
                                    cStrat.HandEVs(index).EVs.ForcedPostStrat(upcard) = BJCAGlobalsClass.Strat.None
                                End If
                        End Select
                    End If
                Case BJCAGlobalsClass.Strat.xS
                    cStrat.HandEVs(index).SPreallowed(upcard) = False
                    cStrat.HandEVs(index).PreForced(upcard) = False
                    cStrat.HandEVs(index).EVs.Strat(upcard) = BJCAGlobalsClass.Strat.None
                Case BJCAGlobalsClass.Strat.xH
                    cStrat.HandEVs(index).HPreallowed(upcard) = False
                    cStrat.HandEVs(index).PreForced(upcard) = False
                    cStrat.HandEVs(index).EVs.Strat(upcard) = BJCAGlobalsClass.Strat.None
                Case BJCAGlobalsClass.Strat.xD
                    cStrat.HandEVs(index).DPreallowed(upcard) = False
                    cStrat.HandEVs(index).PreForced(upcard) = False
                    cStrat.HandEVs(index).EVs.Strat(upcard) = BJCAGlobalsClass.Strat.None
                Case BJCAGlobalsClass.Strat.xR
                    cStrat.HandEVs(index).RPreallowed(upcard) = False
                    cStrat.HandEVs(index).PreForced(upcard) = False
                    cStrat.HandEVs(index).EVs.Strat(upcard) = BJCAGlobalsClass.Strat.None
                Case BJCAGlobalsClass.Strat.xP
                    cStrat.HandEVs(index).PAllowed(upcard) = False
                    cStrat.HandEVs(index).PreForced(upcard) = False
                    cStrat.HandEVs(index).EVs.Strat(upcard) = BJCAGlobalsClass.Strat.None
            End Select
        End If

        If cStrat.HandEVs(index).PostForced(upcard) Then
            Select Case cStrat.HandEVs(index).EVs.ForcedPostStrat(upcard)
                Case BJCAGlobalsClass.Strat.None
                    cStrat.HandEVs(index).SPostallowed(upcard) = True
                    cStrat.HandEVs(index).HPostallowed(upcard) = True
                    cStrat.HandEVs(index).DPostallowed(upcard) = PlayerHands(index).DPostallowed(upcard)
                    cStrat.HandEVs(index).RPostallowed(upcard) = PlayerHands(index).RPostallowed(upcard)
                    cStrat.HandEVs(index).PostForced(upcard) = False
                    cStrat.HandEVs(index).EVs.ForcedPostStrat(upcard) = BJCAGlobalsClass.Strat.None
                Case BJCAGlobalsClass.Strat.S, BJCAGlobalsClass.Strat.H
                Case BJCAGlobalsClass.Strat.D
                    If Not cStrat.HandEVs(index).DPostallowed(upcard) Then
                        cStrat.HandEVs(index).EVs.ForcedPostStrat(upcard) = BJCAGlobalsClass.Strat.H
                    End If
                Case BJCAGlobalsClass.Strat.DS
                    If Not cStrat.HandEVs(index).DPostallowed(upcard) Then
                        cStrat.HandEVs(index).EVs.ForcedPostStrat(upcard) = BJCAGlobalsClass.Strat.S
                    End If
                Case BJCAGlobalsClass.Strat.R
                    If cStrat.HandEVs(index).RPostallowed(upcard) = 0 Then
                        cStrat.HandEVs(index).EVs.ForcedPostStrat(upcard) = BJCAGlobalsClass.Strat.H
                    End If
                Case BJCAGlobalsClass.Strat.RS
                    If cStrat.HandEVs(index).RPostallowed(upcard) = 0 Then
                        cStrat.HandEVs(index).EVs.ForcedPostStrat(upcard) = BJCAGlobalsClass.Strat.S
                    End If
                Case BJCAGlobalsClass.Strat.P, BJCAGlobalsClass.Strat.PS, BJCAGlobalsClass.Strat.PD, BJCAGlobalsClass.Strat.PH, BJCAGlobalsClass.Strat.PR
                    For card = 1 To 10
                        If PlayerHands(index).Hand.Cards(card) = 2 Then
                            Exit For
                        End If
                    Next card
                    If SplitIndex(card, upcard) > 0 Then
                        Select Case cStrat.HandEVs(index).EVs.Strat(upcard)
                            Case BJCAGlobalsClass.Strat.P
                                cStrat.HandEVs(index).PostForced(upcard) = False
                                cStrat.HandEVs(index).EVs.ForcedPostStrat(upcard) = BJCAGlobalsClass.Strat.None
                            Case BJCAGlobalsClass.Strat.PS
                                cStrat.HandEVs(index).PostForced(upcard) = True
                                cStrat.HandEVs(index).EVs.ForcedPostStrat(upcard) = BJCAGlobalsClass.Strat.S
                            Case BJCAGlobalsClass.Strat.PH
                                cStrat.HandEVs(index).PostForced(upcard) = True
                                cStrat.HandEVs(index).EVs.ForcedPostStrat(upcard) = BJCAGlobalsClass.Strat.H
                            Case BJCAGlobalsClass.Strat.PD
                                If cStrat.HandEVs(index).DPostallowed(upcard) Then
                                    cStrat.HandEVs(index).PostForced(upcard) = True
                                    cStrat.HandEVs(index).EVs.ForcedPostStrat(upcard) = BJCAGlobalsClass.Strat.D
                                Else
                                    cStrat.HandEVs(index).PostForced(upcard) = False
                                    cStrat.HandEVs(index).EVs.ForcedPostStrat(upcard) = BJCAGlobalsClass.Strat.None
                                End If
                            Case BJCAGlobalsClass.Strat.PR
                                If cStrat.HandEVs(index).RPostallowed(upcard) > 0 Then
                                    cStrat.HandEVs(index).PostForced(upcard) = True
                                    cStrat.HandEVs(index).EVs.ForcedPostStrat(upcard) = BJCAGlobalsClass.Strat.R
                                Else
                                    cStrat.HandEVs(index).PostForced(upcard) = False
                                    cStrat.HandEVs(index).EVs.ForcedPostStrat(upcard) = BJCAGlobalsClass.Strat.None
                                End If
                        End Select
                    End If
                Case BJCAGlobalsClass.Strat.xS
                    cStrat.HandEVs(index).SPostallowed(upcard) = False
                    cStrat.HandEVs(index).PostForced(upcard) = False
                    cStrat.HandEVs(index).EVs.ForcedPostStrat(upcard) = BJCAGlobalsClass.Strat.None
                Case BJCAGlobalsClass.Strat.xH
                    cStrat.HandEVs(index).HPostallowed(upcard) = False
                    cStrat.HandEVs(index).PostForced(upcard) = False
                    cStrat.HandEVs(index).EVs.ForcedPostStrat(upcard) = BJCAGlobalsClass.Strat.None
                Case BJCAGlobalsClass.Strat.xD
                    cStrat.HandEVs(index).DPostallowed(upcard) = False
                    cStrat.HandEVs(index).PostForced(upcard) = False
                    cStrat.HandEVs(index).EVs.ForcedPostStrat(upcard) = BJCAGlobalsClass.Strat.None
                Case BJCAGlobalsClass.Strat.xR
                    cStrat.HandEVs(index).RPostallowed(upcard) = False
                    cStrat.HandEVs(index).PostForced(upcard) = False
                    cStrat.HandEVs(index).EVs.ForcedPostStrat(upcard) = BJCAGlobalsClass.Strat.None
                Case BJCAGlobalsClass.Strat.xP
                    cStrat.HandEVs(index).PAllowed(upcard) = False
                    cStrat.HandEVs(index).PreForced(upcard) = False
                    cStrat.HandEVs(index).EVs.Strat(upcard) = BJCAGlobalsClass.Strat.None
            End Select
        End If

        If cStrat.HandEVs(index).SPreallowed(upcard) = False And _
            cStrat.HandEVs(index).HPreallowed(upcard) = False And _
            cStrat.HandEVs(index).PAllowed(upcard) = False And _
            cStrat.HandEVs(index).DPreallowed(upcard) = False And _
            cStrat.HandEVs(index).RPreallowed(upcard) = False And _
            cStrat.HandEVs(index).PreForced(upcard) = True Then

            cStrat.HandEVs(index).SPreallowed(upcard) = True
            cStrat.HandEVs(index).HPreallowed(upcard) = True
            cStrat.HandEVs(index).PAllowed(upcard) = True
            cStrat.HandEVs(index).DPreallowed(upcard) = PlayerHands(index).DPreallowed(upcard)
            cStrat.HandEVs(index).RPreallowed(upcard) = PlayerHands(index).RPreallowed(upcard)
            cStrat.HandEVs(index).PreForced(upcard) = False
            cStrat.HandEVs(index).EVs.Strat(upcard) = BJCAGlobalsClass.Strat.None
        End If
        If cStrat.HandEVs(index).SPostallowed(upcard) = False And _
            cStrat.HandEVs(index).HPostallowed(upcard) = False And _
            cStrat.HandEVs(index).DPostallowed(upcard) = False And _
            cStrat.HandEVs(index).RPostallowed(upcard) = False And _
            cStrat.HandEVs(index).PostForced(upcard) = True Then

            cStrat.HandEVs(index).SPostallowed(upcard) = True
            cStrat.HandEVs(index).HPostallowed(upcard) = True
            cStrat.HandEVs(index).DPostallowed(upcard) = PlayerHands(index).DPostallowed(upcard)
            cStrat.HandEVs(index).RPostallowed(upcard) = PlayerHands(index).RPostallowed(upcard)
            cStrat.HandEVs(index).PostForced(upcard) = False
            cStrat.HandEVs(index).EVs.ForcedPostStrat(upcard) = BJCAGlobalsClass.Strat.None
        End If
    End Sub

#End Region

#Region " Main Computation Methods "

#Region " Probability Methods "

    Private Sub ComputeHandProbsPreSplit()
        Dim index As Integer
        Dim upcard As Integer
        Dim ucprob As Double
        Dim prob As New Double
        Dim hand As New BJCAHandClass

        Dim suit As Integer
        Dim card As Integer
        Dim i As Integer

        For index = 1 To NumPHands
            If PlayerHands(index).Hand.NumCards > 1 Then
                For upcard = 1 To 10
                    If UCAllowed(upcard) Then
                        'Compute the Hand Prob
                        CurrentShoe.Deal(upcard)
                        PlayerHands(index).HandEVs.Prob(upcard) = GetHandProb(CurrentShoe.Hand, PlayerHands(index).Hand, 0, 1)
                        CurrentShoe.Undeal(upcard)

                        'Compute the BJ probs
                        If upcard = 1 Then
                            PlayerHands(index).HandEVs.BJProb(upcard) = BJHandNumerator(10, PlayerHands(index).Hand.Cards(10), 1) / BJHandDivisor(PlayerHands(index).Hand.NumCards + 1, 1)
                        ElseIf upcard = 10 Then
                            PlayerHands(index).HandEVs.BJProb(upcard) = BJHandNumerator(1, PlayerHands(index).Hand.Cards(1), 1) / BJHandDivisor(PlayerHands(index).Hand.NumCards + 1, 1)
                        End If

                        'Compute the Suited Probs
                        ucprob = CardProb(upcard, 0)
                        For suit = 0 To 3
                            PlayerHands(index).HandEVs.SuitedPossible(upcard, suit) = False
                            PlayerHands(index).HandEVs.ProbSuited(upcard, suit) = 1
                            For card = 1 To 10
                                If PlayerHands(index).HandEVs.ProbSuited(upcard, suit) > 0 Then
                                    For i = 1 To PlayerHands(index).Hand.Cards(card)
                                        If CurrentShoe.Suits(card, suit) > 0 Then
                                            PlayerHands(index).HandEVs.ProbSuited(upcard, suit) *= CurrentShoe.Suits(card, suit) / CurrentShoe.CardsLeft
                                            If Not InfiniteDeck Then CurrentShoe.DealSuited(card, suit)
                                        Else
                                            PlayerHands(index).HandEVs.ProbSuited(upcard, suit) = 0
                                            Exit For
                                        End If
                                    Next i
                                End If
                            Next card
                            If PlayerHands(index).HandEVs.ProbSuited(upcard, suit) > 0 Then
                                'So far the suited prob does not take into account the upcard so we do that now
                                PlayerHands(index).HandEVs.ProbSuited(upcard, suit) *= CardProb(upcard, 0)
                                If PlayerHands(index).HandEVs.ProbSuited(upcard, suit) > 0 Then
                                    PlayerHands(index).HandEVs.ProbSuited(upcard, suit) /= ucprob
                                    PlayerHands(index).HandEVs.SuitedPossible(upcard, suit) = True
                                Else

                                End If
                            End If
                            PlayerHands(index).HandEVs.SumSuited(upcard) += PlayerHands(index).HandEVs.ProbSuited(upcard, suit)
                            CurrentShoe.Reset(OriginalShoe)
                        Next suit
                    End If
                Next upcard
            End If
        Next index
    End Sub

#End Region

#Region " Stand Methods "

    Public Function ComputeStandHand(ByVal hand As BJCAHandClass, ByVal minUC As Integer, ByVal maxUC As Integer, ByVal computeAll As Boolean) As BJCAHandEVsClass
        Dim upcard As Integer
        Dim prob As Double
        Dim dIndex As Integer
        Dim total As Integer
        Dim bjadjprob As Double
        Dim tempEVs As New BJCAHandEVsClass
        Dim newEVs As New BJCAHandEVsClass
        Dim dprobs As New BJCADealerProbsClass
        Dim key As String
        'The Hand does not need to be dealt or undealt, however the Upcard should NOT be dealt yet.

        'Only dealer hands that push or win need to be calculated - this is all hands for total<17
        key = DealerProbs.GetHashKey(StandOnSoft, OriginalShoe, hand)
        If DealerProbs.Contains(key) And UseDPDictionary Then
            For total = 17 To 21
                For upcard = minUC To maxUC
                    If UCAllowed(upcard) Then
                        newEVs.DealerProbs(upcard, total - 17) = DealerProbs.Item(key).p(upcard, total - 17)
                    End If
                Next upcard
            Next total
        ElseIf hand.Total > 16 And Not computeAll And Not SaveDPDictionary Then
            For total = hand.Total To 21
                dIndex = DealerHandTotal(total)
                Do While dIndex
                    prob = GetHandProb(hand, DealerHands(dIndex).Hand, 0, BJHandDivisor(hand.NumCards, 1))
                    If prob <> 0 Then
                        For upcard = minUC To maxUC
                            If UCAllowed(upcard) And DealerHands(dIndex).Multiplier(upcard) <> 0 And BJHandNumerator(upcard, hand.Cards(upcard), 1) <> 0 Then
                                newEVs.DealerProbs(upcard, DealerHands(dIndex).Hand.Total - 17) += prob * DealerHands(dIndex).Multiplier(upcard) / BJHandNumerator(upcard, hand.Cards(upcard), 1)
                            End If
                        Next upcard
                    End If
                    dIndex = DealerHands(dIndex).NextHandTotal
                Loop
            Next total
        ElseIf SaveDPDictionary Then
            'Calculate all upcards regardless of UCAllowed status to save to the dictionary
            For total = 17 To 21
                dIndex = DealerHandTotal(total)
                Do While dIndex
                    prob = GetHandProb(hand, DealerHands(dIndex).Hand, 0, BJHandDivisor(hand.NumCards, 1))
                    If prob <> 0 Then
                        For upcard = 1 To 10
                            If DealerHands(dIndex).Multiplier(upcard) <> 0 And BJHandNumerator(upcard, hand.Cards(upcard), 1) <> 0 Then
                                newEVs.DealerProbs(upcard, DealerHands(dIndex).Hand.Total - 17) += prob * DealerHands(dIndex).Multiplier(upcard) / BJHandNumerator(upcard, hand.Cards(upcard), 1)
                            End If
                        Next upcard
                    End If
                    dIndex = DealerHands(dIndex).NextHandTotal
                Loop
            Next total
        Else
            'All dealer probs need to be calculated since the player has a stiff hand (total < 17)
            For total = 17 To 21
                dIndex = DealerHandTotal(total)
                Do While dIndex
                    prob = GetHandProb(hand, DealerHands(dIndex).Hand, 0, BJHandDivisor(hand.NumCards, 1))
                    If prob <> 0 Then
                        For upcard = minUC To maxUC
                            If UCAllowed(upcard) And DealerHands(dIndex).Multiplier(upcard) <> 0 And BJHandNumerator(upcard, hand.Cards(upcard), 1) <> 0 Then
                                newEVs.DealerProbs(upcard, DealerHands(dIndex).Hand.Total - 17) += prob * DealerHands(dIndex).Multiplier(upcard) / BJHandNumerator(upcard, hand.Cards(upcard), 1)
                            End If
                        Next upcard
                    End If
                    dIndex = DealerHands(dIndex).NextHandTotal
                Loop
            Next total
        End If

        'Update the dictionary with the non-BJ adjusted dealerprobs
        If Not DealerProbs.Contains(key) And SaveDPDictionary Then
            For upcard = 1 To 10
                For total = 17 To 21
                    dprobs.p(upcard, total - 17) = newEVs.DealerProbs(upcard, total - 17)
                Next total
            Next upcard
            DealerProbs.Add(key, dprobs)
        End If

        For upcard = minUC To maxUC
            prob = newEVs.DealerProbs(upcard, 17 - 17) _
                + newEVs.DealerProbs(upcard, 18 - 17) _
                + newEVs.DealerProbs(upcard, 19 - 17) _
                + newEVs.DealerProbs(upcard, 20 - 17) _
                + newEVs.DealerProbs(upcard, 21 - 17)
            If UCAllowed(upcard) And (prob > 0 Or (Not computeAll And hand.Total <> 21)) Then
                'bjadjprob is the prob of dealer blackjack given the current playerhand and upcard
                If upcard = 1 Then
                    bjadjprob = BJHandNumerator(10, hand.Cards(10), 1) / BJHandDivisor(hand.NumCards + 1, 1)
                    If CheckAce Then
                        newEVs.DealerProbs(upcard, 17 - 17) = newEVs.DealerProbs(upcard, 17 - 17) / (1 - bjadjprob)
                        newEVs.DealerProbs(upcard, 18 - 17) = newEVs.DealerProbs(upcard, 18 - 17) / (1 - bjadjprob)
                        newEVs.DealerProbs(upcard, 19 - 17) = newEVs.DealerProbs(upcard, 19 - 17) / (1 - bjadjprob)
                        newEVs.DealerProbs(upcard, 20 - 17) = newEVs.DealerProbs(upcard, 20 - 17) / (1 - bjadjprob)
                        newEVs.DealerProbs(upcard, 21 - 17) = (newEVs.DealerProbs(upcard, 21 - 17) - bjadjprob) / (1 - bjadjprob)
                    End If
                ElseIf upcard = 10 Then
                    bjadjprob = BJHandNumerator(1, hand.Cards(1), 1) / BJHandDivisor(hand.NumCards + 1, 1)
                    If CheckTen Then
                        newEVs.DealerProbs(upcard, 17 - 17) = newEVs.DealerProbs(upcard, 17 - 17) / (1 - bjadjprob)
                        newEVs.DealerProbs(upcard, 18 - 17) = newEVs.DealerProbs(upcard, 18 - 17) / (1 - bjadjprob)
                        newEVs.DealerProbs(upcard, 19 - 17) = newEVs.DealerProbs(upcard, 19 - 17) / (1 - bjadjprob)
                        newEVs.DealerProbs(upcard, 20 - 17) = newEVs.DealerProbs(upcard, 20 - 17) / (1 - bjadjprob)
                        newEVs.DealerProbs(upcard, 21 - 17) = (newEVs.DealerProbs(upcard, 21 - 17) - bjadjprob) / (1 - bjadjprob)
                    End If
                Else
                    bjadjprob = 0
                End If


                'Need to count pushes as losses against blackjack if dealer is not checking
                '   bjadjprob will only be sent to gethandevev if the adjustment needs to be made
                If Not (hand.Total = 21 And ((upcard = 1 And Not CheckAce) Or (upcard = 10 And Not CheckTen))) Then
                    bjadjprob = 0
                End If

                tempEVs.Empty()
                tempEVs = GetHandEV(hand.Total, newEVs, upcard, bjadjprob)
                newEVs.StandEV(upcard) = tempEVs.StandEV(upcard)
                newEVs.StandPushEV(upcard) = tempEVs.StandPushEV(upcard)
                newEVs.BJStandEV(upcard) = tempEVs.BJStandEV(upcard)
            End If
        Next upcard

        Return newEVs

    End Function

    Private Sub ComputeStand()
        Dim index As Integer
        Dim upcard As Integer
        Dim newEVs As New BJCAHandEVsClass

        For index = 1 To NumPHands
            If PlayerHands(index).Hand.NumCards > 1 Then

                newEVs = ComputeStandHand(PlayerHands(index).Hand, 1, 10, True)

                For upcard = 1 To 10
                    If UCAllowed(upcard) Then
                        PlayerHands(index).HandEVs.StandEV(upcard) = newEVs.StandEV(upcard)
                        PlayerHands(index).HandEVs.StandPushEV(upcard) = newEVs.StandPushEV(upcard)
                        PlayerHands(index).HandEVs.BJStandEV(upcard) = newEVs.BJStandEV(upcard)
                        PlayerHands(index).HandEVs.DealerProbs(upcard, 0) = newEVs.DealerProbs(upcard, 0)
                        PlayerHands(index).HandEVs.DealerProbs(upcard, 1) = newEVs.DealerProbs(upcard, 1)
                        PlayerHands(index).HandEVs.DealerProbs(upcard, 2) = newEVs.DealerProbs(upcard, 2)
                        PlayerHands(index).HandEVs.DealerProbs(upcard, 3) = newEVs.DealerProbs(upcard, 3)
                        PlayerHands(index).HandEVs.DealerProbs(upcard, 4) = newEVs.DealerProbs(upcard, 4)
                    End If
                Next upcard
            End If
        Next index
    End Sub

#End Region

#Region " Blackjack Methods "

    Private Function ComputeBlackjackHand(ByVal hand As BJCAHandClass, ByVal upcard As Integer, ByVal postSplit As Boolean) As BJCAHandEVsClass
        Dim bjadjprob As Double
        Dim netev As Double
        Dim spec10Fraction As Double
        Dim newEVs As New BJCAHandEVsClass

        'This is only called post-split if post-split BJ's apply

        If UCAllowed(upcard) Then
            'The easiest way to adjust the BJ values to account for suited bonuses is to 
            '   adjust the payoffs.
            'The Player's BJ hand has been dealt but not the Dealer's upcard

            If BJBonuses.SpecificTen Then
                spec10Fraction = BJBonuses.SpecificTenFraction
            Else
                spec10Fraction = 0
            End If

            'Readjust BJ for upcard 1 if Not Checking Ace - i.e. adjbjpays * Prob Dealer doesn't have BJ
            If (upcard = 1 And Not CheckAce) Then
                bjadjprob = BJHandNumerator(10, hand.Cards(10), 1) / BJHandDivisor(hand.NumCards + 1, 1)
                newEVs.StandEV(upcard) = BJPays * (1 - bjadjprob) + PDTies(22) * bjadjprob
                'HandContinues holds the MustWin value for Specific Tens
                If BJBonuses.HandContinues Then
                    newEVs.StandEV(upcard) = newEVs.StandEV(upcard) * (1 - spec10Fraction) + (BJBonuses.PayoffGeneralBJ * (1 - bjadjprob) + PDTies(22) * bjadjprob) * spec10Fraction
                    If PDTies(22) = 0 Then
                        newEVs.StandPushEV(upcard) = bjadjprob
                    Else
                        newEVs.StandPushEV(upcard) = 0
                    End If
                Else
                    newEVs.StandEV(upcard) = newEVs.StandEV(upcard) * (1 - spec10Fraction) + BJBonuses.PayoffGeneralBJ * spec10Fraction
                    If PDTies(22) = 0 Then
                        newEVs.StandPushEV(upcard) = bjadjprob * (1 - spec10Fraction)
                    Else
                        newEVs.StandPushEV(upcard) = 0
                    End If
                End If
            ElseIf (upcard = 10 And Not CheckTen) Then
                'Adjust BJ for upcard 10 if Not Checking Ten
                bjadjprob = BJHandNumerator(1, hand.Cards(1), 1) / BJHandDivisor(hand.NumCards + 1, 1)
                newEVs.StandEV(upcard) = BJPays * (1 - bjadjprob) + PDTies(22) * bjadjprob
                'HandContinues holds the MustWin value for Specific Tens
                If BJBonuses.HandContinues Then
                    newEVs.StandEV(upcard) = newEVs.StandEV(upcard) * (1 - spec10Fraction) + (BJBonuses.PayoffGeneralBJ * (1 - bjadjprob) + PDTies(22) * bjadjprob) * spec10Fraction
                    If PDTies(22) = 0 Then
                        newEVs.StandPushEV(upcard) = bjadjprob
                    Else
                        newEVs.StandPushEV(upcard) = 0
                    End If
                Else
                    newEVs.StandEV(upcard) = newEVs.StandEV(upcard) * (1 - spec10Fraction) + BJBonuses.PayoffGeneralBJ * spec10Fraction
                    If PDTies(22) = 0 Then
                        newEVs.StandPushEV(upcard) = bjadjprob * (1 - spec10Fraction)
                    Else
                        newEVs.StandPushEV(upcard) = 0
                    End If
                End If
            ElseIf (upcard <> 1 And upcard <> 10) Or postSplit Then
                'Either the upcard is non A/10 or the dealer's checking and post-split
                newEVs.StandEV(upcard) = BJPays * (1 - spec10Fraction) + BJBonuses.PayoffGeneralBJ * spec10Fraction
                newEVs.StandPushEV(upcard) = 0
            Else
                'All that's left is BJ Possible and Dealer Checking and pre-split
                'First we need the unconditional values
                '                netev = bjpays * (1 - bjadjprob) + pdties(22) * bjadjprob
                'Next we condition the values to allow fair future comparison
                '                newEVs.StandEV(upcard) = (netev + bjadjprob) / (1 - bjadjprob)
                If upcard = 1 Then
                    bjadjprob = BJHandNumerator(10, hand.Cards(10), 1) / BJHandDivisor(hand.NumCards + 1, 1)
                Else
                    bjadjprob = BJHandNumerator(1, hand.Cards(1), 1) / BJHandDivisor(hand.NumCards + 1, 1)
                End If

                newEVs.StandEV(upcard) = BJPays * (1 - spec10Fraction) + BJBonuses.PayoffGeneralBJ * spec10Fraction
                'HandContinues holds the MustWin value for Specific Tens
                If BJBonuses.HandContinues Then
                    newEVs.BJStandEV(upcard) = PDTies(22)
                    If PDTies(22) = 0 Then
                        newEVs.StandPushEV(upcard) = bjadjprob
                    Else
                        newEVs.StandPushEV(upcard) = 0
                    End If
                Else
                    newEVs.BJStandEV(upcard) = PDTies(22) * (1 - spec10Fraction) + BJBonuses.PayoffGeneralBJ * spec10Fraction
                    newEVs.StandPushEV(upcard) = 0
                End If

            End If
        End If

        Return newEVs

    End Function

    Private Sub ComputeBlackjack()
        Dim index As Integer
        Dim upcard As Integer
        Dim newEVs As New BJCAHandEVsClass

        'Deal the BJ to the Player and find the hand
        If CardProb(1, 0) > 0 Then
            DealPCard(1)
            If CardProb(10, 0) > 0 Then
                DealPCard(10)
                index = FindPlayerHand(CurrentPHand)
                If index > 0 Then
                    'Adjust BJ for all upcards
                    For upcard = 1 To 10
                        If UCAllowed(upcard) And CardProb(upcard, 0) > 0 Then
                            newEVs = ComputeBlackjackHand(CurrentPHand, upcard, False)

                            PlayerHands(index).HandEVs.StandEV(upcard) = newEVs.StandEV(upcard)
                            PlayerHands(index).HandEVs.StandPushEV(upcard) = newEVs.StandPushEV(upcard)
                            PlayerHands(index).HandEVs.BJStandEV(upcard) = newEVs.BJStandEV(upcard)
                        End If
                    Next upcard
                End If
                UndealPCard(10)
            End If
            UndealPCard(1)
        End If
    End Sub

    Private Function ApplyBJSuitedBonusesPayoffHand(ByRef pHand As BJCAPlayerHandClass, ByVal upcard As Integer, ByVal bjprob As Double) As BJCASuitedBonusEVClass
        Dim currentPayoff As Double
        Dim BJEV As Double

        Dim spec10Fraction As Double
        Dim suit As Integer
        Dim pSuited As Double
        Dim pSpecificSuit As Double
        Dim p10Suited As Double
        Dim p10SpecificSuit As Double

        Dim newHandEVs As New BJCASuitedBonusEVClass

        currentPayoff = pHand.HandEVs.StandEV(upcard)
        BJEV = pHand.HandEVs.BJStandEV(upcard)

        If BJBonuses.SpecificTen Then
            spec10Fraction = BJBonuses.SpecificTenFraction
        Else
            spec10Fraction = 0
        End If

        For suit = 0 To 3
            If pHand.HandEVs.SuitedPossible(upcard, suit) Then
                'Determine whether the suit is the Specific Suit or not
                If BJBonuses.SpecificSuit Then
                    If BJBonuses.SuitToWin = suit Then
                        pSuited = 0
                        pSpecificSuit = 1
                    Else
                        pSuited = 1
                        pSpecificSuit = 0
                    End If
                Else
                    pSuited = 1
                    pSpecificSuit = 0
                End If

                'Determine whether the suit is the Specific Suit or not for the Specific Ten bonuses
                If spec10Fraction > 0 Then
                    'PreSplit and PostSplit hold the Suited and Specific Suit values for the Specific Ten Bonuses
                    If BJBonuses.PostSplit Then
                        'Upcard holds the specific suit for the specific 10 bonuses
                        If BJBonuses.Upcard = suit Then
                            p10Suited = 0
                            p10SpecificSuit = 1
                        Else
                            p10Suited = 1
                            p10SpecificSuit = 0
                        End If
                    Else
                        p10Suited = 1
                        p10SpecificSuit = 0
                    End If
                Else
                    p10Suited = 0
                    p10SpecificSuit = 0
                End If


                'Now adjust the evs
                If (upcard <> 1 And upcard <> 10) Then
                    'If UC<>(1 or 10) then BJ always wins
                    If pSuited > 0 Then
                        If BJBonuses.PayoffSuited = 0 Then
                            newHandEVs.SuitedStandEV(upcard, suit) = pSuited * currentPayoff
                        Else
                            newHandEVs.SuitedStandEV(upcard, suit) = pSuited * BJBonuses.PayoffSuited
                        End If
                    End If

                    If pSpecificSuit > 0 Then
                        If BJBonuses.PayoffSpecificSuit = 0 Then
                            newHandEVs.SuitedStandEV(upcard, suit) = pSpecificSuit * currentPayoff
                        Else
                            newHandEVs.SuitedStandEV(upcard, suit) = pSpecificSuit * BJBonuses.PayoffSpecificSuit
                        End If
                    End If


                ElseIf (upcard = 1 And Not CheckAce) Or (upcard = 10 And Not CheckTen) Then
                    'No checking for BJ but BJ possible
                    If Not BJBonuses.MustWin Then
                        'If it's not necessary to win - then it always wins since it's BJ and it beats dealer BJ
                        If pSuited > 0 Then
                            If BJBonuses.PayoffSuited = 0 Then
                                newHandEVs.SuitedStandEV(upcard, suit) = pSuited * currentPayoff
                            Else
                                newHandEVs.SuitedStandEV(upcard, suit) = pSuited * BJBonuses.PayoffSuited
                            End If
                        End If

                        If pSpecificSuit > 0 Then
                            If BJBonuses.PayoffSpecificSuit = 0 Then
                                newHandEVs.SuitedStandEV(upcard, suit) = pSpecificSuit * currentPayoff
                            Else
                                newHandEVs.SuitedStandEV(upcard, suit) = pSpecificSuit * BJBonuses.PayoffSpecificSuit
                            End If
                        End If
                    Else
                        'Since hand Must Win, then BJ PDTies are still PDTies and it's a win otherwise
                        If pSuited > 0 Then
                            If BJBonuses.PayoffSuited = 0 Then
                                newHandEVs.SuitedStandEV(upcard, suit) = pSuited * currentPayoff
                            Else
                                newHandEVs.SuitedStandEV(upcard, suit) = pSuited * ((1 - bjprob) * BJBonuses.PayoffSuited + bjprob * PDTies(22))
                            End If
                        End If

                        If pSpecificSuit > 0 Then
                            If BJBonuses.PayoffSpecificSuit = 0 Then
                                newHandEVs.SuitedStandEV(upcard, suit) = pSpecificSuit * currentPayoff
                            Else
                                newHandEVs.SuitedStandEV(upcard, suit) = pSpecificSuit * ((1 - bjprob) * BJBonuses.PayoffSpecificSuit + bjprob * PDTies(22))
                            End If
                        End If
                    End If


                Else
                    'BJ Possible and dealer checking
                    If Not BJBonuses.MustWin Then
                        'BJEV must be adjusted accordingly
                        If pSuited > 0 Then
                            If BJBonuses.PayoffSuited = 0 Then
                                newHandEVs.SuitedStandEV(upcard, suit) = pSuited * currentPayoff
                                newHandEVs.SuitedStandBJEV(upcard, suit) = pSuited * BJEV
                            Else
                                newHandEVs.SuitedStandEV(upcard, suit) = pSuited * BJBonuses.PayoffSuited
                                newHandEVs.SuitedStandBJEV(upcard, suit) = pSuited * BJBonuses.PayoffSuited
                            End If
                        End If

                        If pSpecificSuit > 0 Then
                            If BJBonuses.PayoffSpecificSuit = 0 Then
                                newHandEVs.SuitedStandEV(upcard, suit) = pSpecificSuit * currentPayoff
                                newHandEVs.SuitedStandBJEV(upcard, suit) = pSpecificSuit * BJEV
                            Else
                                newHandEVs.SuitedStandEV(upcard, suit) = pSpecificSuit * BJBonuses.PayoffSpecificSuit
                                newHandEVs.SuitedStandBJEV(upcard, suit) = pSpecificSuit * BJBonuses.PayoffSpecificSuit
                            End If
                        End If
                    Else
                        'Since hand Must Win, pushev is unaffected 
                        If pSuited > 0 Then
                            If BJBonuses.PayoffSuited = 0 Then
                                newHandEVs.SuitedStandEV(upcard, suit) = pSuited * currentPayoff
                                newHandEVs.SuitedStandBJEV(upcard, suit) = pSuited * BJEV
                            Else
                                newHandEVs.SuitedStandEV(upcard, suit) = pSuited * BJBonuses.PayoffSuited
                                newHandEVs.SuitedStandBJEV(upcard, suit) = pSuited * BJEV
                            End If
                        End If

                        If pSpecificSuit > 0 Then
                            If BJBonuses.PayoffSpecificSuit = 0 Then
                                newHandEVs.SuitedStandEV(upcard, suit) = pSpecificSuit * currentPayoff
                                newHandEVs.SuitedStandBJEV(upcard, suit) = pSpecificSuit * BJEV
                            Else
                                newHandEVs.SuitedStandEV(upcard, suit) = pSpecificSuit * BJBonuses.PayoffSpecificSuit
                                newHandEVs.SuitedStandBJEV(upcard, suit) = pSpecificSuit * BJEV
                            End If
                        End If
                    End If
                End If
            Else
                newHandEVs.SuitedStandEV(upcard, suit) = pHand.HandEVs.StandEV(upcard)
            End If  'Suited Possible
        Next suit

        Return newHandEVs

    End Function

    Private Sub ApplyBJSuitedBonuses()
        Dim index As Integer
        Dim upcard As Integer
        Dim suit As Integer
        Dim bjprob As Double
        Dim newEVs As BJCASuitedBonusEVClass

        index = BJIndex
        If index > 0 And BJBonuses.Suited Then
            NSuitedBonusHands += 1
            SuitedBonusHands(NSuitedBonusHands) = index

            NSuitedHands += 1
            SuitedHandsList(NSuitedHands) = index
            PlayerHands(index).SuitedBonusEVs = New BJCASuitedBonusEVClass

            For upcard = 1 To 10
                If UCAllowed(upcard) Then
                    bjprob = PlayerHands(index).HandEVs.BJProb(upcard)
                    newEVs = ApplyBJSuitedBonusesPayoffHand(PlayerHands(index), upcard, bjprob)
                    'First copy over the Stand and BJ Stand EV's
                    For suit = 0 To 3
                        PlayerHands(index).SuitedBonusEVs.SuitedStandEV(upcard, suit) = newEVs.SuitedStandEV(upcard, suit)
                        PlayerHands(index).SuitedBonusEVs.SuitedStandBJEV(upcard, suit) = newEVs.SuitedStandBJEV(upcard, suit)
                    Next suit

                    'Now we need to determine the strategy
                    For suit = 0 To 3
                        newEVs.Empty()
                        newEVs = ComputeSuitedStratHand(Opt, index, upcard, suit, True)
                        PlayerHands(index).SuitedBonusEVs.SuitedStratEV(upcard, suit) = newEVs.SuitedStratEV(upcard, suit)
                        PlayerHands(index).SuitedBonusEVs.SuitedStratBJEV(upcard, suit) = newEVs.SuitedStratBJEV(upcard, suit)
                        PlayerHands(index).SuitedBonusEVs.SuitedStrat(upcard, suit) = newEVs.SuitedStrat(upcard, suit)
                    Next suit
                End If
            Next upcard
        End If

    End Sub

#End Region

#Region " Double Methods "

    Private Function ComputeDoubleCardEV(ByVal index As Integer, ByVal upcard As Integer, ByVal depth As Integer) As BJCAHandEVsClass
        Dim strat As Integer
        Dim card As Integer
        Dim prob As Double
        Dim EV As Double
        Dim push As Double
        Dim newIndex As Integer
        Dim tempEVs As New BJCAHandEVsClass
        Dim newEVs As New BJCAHandEVsClass

        'Here we limit the amount of redoubling allowed
        strat = PlayerHands(index).HandEVs.DStrat(upcard)
        If strat = BJCAGlobalsClass.Strat.D And depth > RDDepth Then
            strat = BJCAGlobalsClass.Strat.S
        End If

        Select Case strat
            Case BJCAGlobalsClass.Strat.D
                EV = 0
                push = 0
                For card = 1 To 10
                    prob = CardProb(card, 0)
                    If prob > 0 Then
                        newIndex = PlayerHands(index).HitHand(card)
                        If newIndex > 0 Then
                            DealPCard(card)
                            tempEVs = ComputeDoubleCardEV(newIndex, upcard, depth + 1)
                            EV = tempEVs.DStratEV(upcard)
                            push = tempEVs.DStratPushEV(upcard)
                            newEVs.DEV(upcard) += 2 * tempEVs.DEV(upcard) * prob
                            UndealPCard(card)
                        Else
                            EV = -1
                            push = 0
                        End If
                    End If
                    newEVs.DStratEV(upcard) += 2 * EV * prob
                    newEVs.DStratPushEV(upcard) += 2 * push * prob
                Next card
            Case BJCAGlobalsClass.Strat.R
                If DDRType = C.Surr.ES Then
                    newEVs.DStratEV(upcard) = SurrPays
                Else
                    newEVs.DStratEV(upcard) = PlayerHands(index).HandEVs.SurrEV(upcard)
                End If
                newEVs.DStratPushEV(upcard) = 0
                If (BBO Or OBBO Or AOBBO) And DDRType = C.Surr.ES Then
                    'Let the calling function know the hand was resolved before the BJ check
                    newEVs.DEV(upcard) = 1
                End If
            Case Else   'Stand
                newEVs.DStratEV(upcard) = PlayerHands(index).HandEVs.StandEV(upcard)
                newEVs.DStratPushEV(upcard) = PlayerHands(index).HandEVs.StandPushEV(upcard)
                If (BBO Or OBBO Or AOBBO) And PlayerHands(index).Hand.Total = 21 And P21AutoWin Then
                    'Let the calling function know the hand was resolved before the BJ check
                    newEVs.DEV(upcard) = 1
                End If
        End Select

        Return newEVs
    End Function

    Private Function ComputeDoubleHand(ByVal index As Integer, ByVal upcard As Integer) As BJCAHandEVsClass
        Dim card As Integer
        Dim prob As Double
        Dim bustprob As Double
        Dim bjadjprob As Double
        Dim newIndex As Integer
        Dim tempEVs As New BJCAHandEVsClass
        Dim newEVs As New BJCAHandEVsClass

        'The Hand and Upcard should be dealt already

        'In addition to determining the actual Double EV, we need to determine the strategies
        '   for the hand when it is post double.  This includes figuring out the value of
        '   the hand when standing which is only different if double softs count as hard.

        bustprob = 0
        newEVs.DPushEV(upcard) = 0
        For card = 1 To 10
            prob = CardProb(card, upcard)
            If prob > 0 Then
                If PlayerHands(index).HitHand(card) And PlayerHands(index).Hand.Soft And (PlayerHands(PlayerHands(index).HitHand(card)).Hand.Cards(1) = 1) And (DSoftAllHard Or (DSoft19Hard And PlayerHands(index).Hand.Total = 19)) Then
                    'These hands can't bust so the next hand always exists
                    tempEVs.Empty()
                    tempEVs = GetHandEV(PlayerHands(index).Hand.Total - 10 + card, PlayerHands(PlayerHands(index).HitHand(card)).HandEVs, upcard, 0)
                    newEVs.DEV(upcard) += 2 * prob * tempEVs.StandEV(upcard)
                    newEVs.DPushEV(upcard) += 2 * prob * tempEVs.StandPushEV(upcard)
                    'Deal with Player 21 beats BJ and OBBO
                    If (BBO Or OBBO Or AOBBO) And PlayerHands(PlayerHands(index).HitHand(card)).Hand.Total - 10 + card = 21 And P21AutoWin Then
                        If upcard = 1 And Not CheckAce And CardProb(10, 0) > 0 Then
                            CurrentShoe.Deal(10)
                            bustprob += CardProb(card, 0)
                            CurrentShoe.Undeal(10)
                        ElseIf upcard = 10 And Not CheckTen And CardProb(1, 0) > 0 Then
                            CurrentShoe.Deal(1)
                            bustprob += CardProb(card, 0)
                            CurrentShoe.Undeal(1)
                        End If
                    End If
                ElseIf PlayerHands(index).HitHand(card) Then
                    If Not (DDR Or RDA) Then
                        'The only option is to stand
                        newEVs.DEV(upcard) += 2 * prob * PlayerHands(PlayerHands(index).HitHand(card)).HandEVs.StandEV(upcard)
                        newEVs.DPushEV(upcard) += 2 * prob * PlayerHands(PlayerHands(index).HitHand(card)).HandEVs.StandPushEV(upcard)
                        If (BBO Or OBBO Or AOBBO) And PlayerHands(PlayerHands(index).HitHand(card)).Hand.Total = 21 And P21AutoWin Then
                            If upcard = 1 And Not CheckAce And CardProb(10, 0) > 0 Then
                                CurrentShoe.Deal(10)
                                bustprob += CardProb(card, 0)
                                CurrentShoe.Undeal(10)
                            ElseIf upcard = 10 And Not CheckTen And CardProb(1, 0) > 0 Then
                                CurrentShoe.Deal(1)
                                bustprob += CardProb(card, 0)
                                CurrentShoe.Undeal(1)
                            End If
                        End If
                    Else
                        DealPCard(card)
                        'We need to figure out the EV limiting the number of doubles to maximum number of redoubles
                        tempEVs.Empty()
                        tempEVs = ComputeDoubleCardEV(PlayerHands(index).HitHand(card), upcard, 1)
                        newEVs.DEV(upcard) += 2 * prob * tempEVs.DStratEV(upcard)
                        newEVs.DPushEV(upcard) += 2 * prob * tempEVs.DStratPushEV(upcard)
                        UndealPCard(card)
                        'tempevs.DEV =1 if the hand was reolved before the BJ check
                        If (BBO Or OBBO Or AOBBO) And tempEVs.DEV(upcard) = 1 Then
                            If upcard = 1 And Not CheckAce And CardProb(10, 0) > 0 Then
                                CurrentShoe.Deal(10)
                                bustprob += CardProb(card, 0)
                                CurrentShoe.Undeal(10)
                            ElseIf upcard = 10 And Not CheckTen And CardProb(1, 0) > 0 Then
                                CurrentShoe.Deal(1)
                                bustprob += CardProb(card, 0)
                                CurrentShoe.Undeal(1)
                            End If
                        End If
                    End If
                Else
                    newEVs.DEV(upcard) -= 2 * prob
                    If BBO Or OBBO Or AOBBO Then
                        If upcard = 1 And Not CheckAce And CardProb(10, 0) > 0 Then
                            CurrentShoe.Deal(10)
                            bustprob += CardProb(card, 0)
                            CurrentShoe.Undeal(10)
                        ElseIf upcard = 10 And Not CheckTen And CardProb(1, 0) > 0 Then
                            CurrentShoe.Deal(1)
                            bustprob += CardProb(card, 0)
                            CurrentShoe.Undeal(1)
                        End If
                    End If
                End If
            End If
        Next card

        'EV(Double | OBBO) = (1 - prob(BJ)) * CEV + prob(BJ) * ((1-p(Bust|BJ)-p(21 win|BJ)-p(R|BJ))*(-1) - 2*p(Bust|BJ) + 2*p(21 win|BJ)+0.5*p(R|BJ))
        'EV(Double | OBBO) = (1 - prob(BJ)) * CEV - prob(BJ) * ((1-p(Bust|BJ)-p(21 win|BJ)-p(R|BJ)) + 2*p(Bust|BJ) - 2*p(21 win|BJ)-0.5*p(R|BJ))
        'EV(Double | OBBO) = (1 - prob(BJ)) * CEV - prob(BJ) * (1 + p(Bust|BJ) - 3*p(21 win|BJ) - 1.5*p(R|BJ))

        'EV(Double | ENHC) = (1 - prob(BJ)) * CEV + prob(BJ)  * ((1 - p(21 win|BJ)-p(R|BJ))*(-2) + p(21 win|BJ)*2+0.5*P(R|BJ))
        'EV(Double | ENHC) = (1 - prob(BJ)) * CEV + prob(BJ)  * (-2 + 2*p(21 win|BJ) + 2*p(R|BJ)+ p(21 win|BJ)*2 + 0.5*p(R|BJ))
        'EV(Double | ENHC) = (1 - prob(BJ)) * CEV + prob(BJ)  * (-2 + 4*p(21 win|BJ) + 2.5*p(R|BJ))
        'EV(Double | ENHC) = (1 - prob(BJ)) * CEV - prob(BJ)  * (2 - 4*p(21 win|BJ) - 2.5p(R|BJ))
        'EV(Double | ENHC) = (1 - prob(BJ)) * CEV - prob(BJ)  * (1 + p(Bust|BJ) - 3*p(21 win|BJ)  - 1.5*p(R|BJ) + 1 - p(Bust|BJ) - p(21 win|BJ) - p(R|BJ))

        'EV(Double | ENHC) = EV(Double | OBBO) - prob(BJ) *(1 - p(Bust|BJ) - p(21 win|BJ)-p(R|BJ))
        'Therefore:
        'EV(Double | OBBO) = EV(Double | ENHC) + prob(BJ)*(1 - p(Bust|BJ) - p(21 win|BJ)-p(R|BJ))
        If BBO Or OBBO Or AOBBO Then
            If upcard = 1 And Not CheckAce Then
                bjadjprob = CardProb(10, 0)
            ElseIf upcard = 10 And Not CheckTen Then
                bjadjprob = CardProb(1, 0)
            Else
                bjadjprob = 0
            End If
            newEVs.DEV(upcard) += bjadjprob * (1 - bustprob)
        End If

        'Now we have DEV and DStandEV so we need to figure out DStrat
        tempEVs.Empty()
        tempEVs = ComputeDoubleStratHand(newEVs, index, upcard)
        newEVs.DStrat(upcard) = tempEVs.DStrat(upcard)
        newEVs.DStratEV(upcard) = tempEVs.DStratEV(upcard)
        newEVs.DStratPushEV(upcard) = tempEVs.DStratPushEV(upcard)

        Return newEVs
    End Function

    Private Sub ComputeDoubleTotal(ByVal total As Integer, ByVal soft As Boolean)
        Dim index As Integer
        Dim upcard As Integer
        Dim card As Integer

        Dim prob As Double
        Dim bustprob As Double
        Dim bjadjprob As Double
        Dim newEVs As New BJCAHandEVsClass

        index = PlayerHandTotal(total, soft + 1)
        Do While index
            CurrentShoe.Deal(PlayerHands(index).Hand)
            For upcard = 1 To 10
                If DoubleNeeded(index, upcard) Then
                    CurrentShoe.Deal(upcard)
                    newEVs = ComputeDoubleHand(index, upcard)
                    PlayerHands(index).HandEVs.DEV(upcard) = newEVs.DEV(upcard)
                    PlayerHands(index).HandEVs.DPushEV(upcard) = newEVs.DPushEV(upcard)
                    PlayerHands(index).HandEVs.DStratEV(upcard) = newEVs.DStratEV(upcard)
                    PlayerHands(index).HandEVs.DStratPushEV(upcard) = newEVs.DStratPushEV(upcard)
                    PlayerHands(index).HandEVs.DStrat(upcard) = newEVs.DStrat(upcard)
                    CurrentShoe.Undeal(upcard)
                End If
            Next upcard
            CurrentShoe.Undeal(PlayerHands(index).Hand)
            index = PlayerHands(index).NextHand
        Loop
    End Sub

    Private Sub ComputeDouble()
        Dim Total As Integer

        'It is necessary to go by total similar to hitting because of redoubling.
        For Total = 21 To 11 Step -1
            ComputeDoubleTotal(Total, False)
        Next Total
        For Total = 21 To 12 Step -1
            ComputeDoubleTotal(Total, True)
        Next Total
        For Total = 10 To 4 Step -1
            ComputeDoubleTotal(Total, False)
        Next Total
    End Sub

    Private Function ComputeDoubleStratHand(ByVal currEVs As BJCAHandEVsClass, ByVal index As Integer, ByVal upcard As Integer) As BJCAHandEVsClass
        Dim newEVs As New BJCAHandEVsClass
        Dim surrEV As Double

        'Begin with Stand as the strategy
        newEVs.DStratEV(upcard) = PlayerHands(index).HandEVs.StandEV(upcard)
        newEVs.DStratPushEV(upcard) = PlayerHands(index).HandEVs.StandPushEV(upcard)
        newEVs.DStrat(upcard) = BJCAGlobalsClass.Strat.S

        'Now check for Double strategy
        If RDA Then
            If currEVs.DEV(upcard) > newEVs.DStratEV(upcard) Then
                newEVs.DStratEV(upcard) = currEVs.DEV(upcard)
                newEVs.DStratPushEV(upcard) = currEVs.DPushEV(upcard)
                newEVs.DStrat(upcard) = BJCAGlobalsClass.Strat.D
            End If
        End If

        'Now finally check for Surrender strategy
        If DDR Then
            If DDRType = C.Surr.ES Then
                surrEV = SurrPays
            Else
                surrEV = PlayerHands(index).HandEVs.SurrEV(upcard)
            End If
            If surrEV > newEVs.DStratEV(upcard) Then
                newEVs.DStratEV(upcard) = surrEV
                newEVs.DStratPushEV(upcard) = 0
                newEVs.DStrat(upcard) = BJCAGlobalsClass.Strat.R
            End If
        End If

        Return newEVs

    End Function

#End Region

#Region " Surrender Methods "

    Private Function ComputeSurrenderHand(ByVal hand As BJCAHandClass, ByVal surrAllowed As Integer, ByVal upcard As Integer) As BJCAHandEVsClass
        Dim bjadjprob As Double
        Dim newEVs As New BJCAHandEVsClass

        If UCAllowed(upcard) And surrAllowed <> 0 Then
            Select Case surrAllowed
                Case BJCAGlobalsClass.Surr.LS
                    'Need to compare to conditional EV and ENHC/OBBO EV's are unconditional
                    'bjadjprob is the prob of dealer blackjack given the current playerhand and upcard
                    If upcard = 1 And Not CheckAce Then
                        bjadjprob = BJHandNumerator(10, hand.Cards(10), 1) / BJHandDivisor(hand.NumCards + 1, 1)
                    ElseIf upcard = 10 And Not CheckTen Then
                        bjadjprob = BJHandNumerator(1, hand.Cards(1), 1) / BJHandDivisor(hand.NumCards + 1, 1)
                    Else
                        bjadjprob = 0
                    End If
                    newEVs.SurrEV(upcard) = (SurrPays) * (1 - bjadjprob) - bjadjprob
                Case BJCAGlobalsClass.Surr.ES
                    'Need to compare to unconditional EV and OBO EV's are conditional
                    If upcard = 1 And (CheckAce Or (SurrDBJPays <> SurrPays)) Then
                        bjadjprob = BJHandNumerator(10, hand.Cards(10), 1) / BJHandDivisor(hand.NumCards + 1, 1)
                    ElseIf upcard = 10 And (CheckTen Or (SurrDBJPays <> SurrPays)) Then
                        bjadjprob = BJHandNumerator(1, hand.Cards(1), 1) / BJHandDivisor(hand.NumCards + 1, 1)
                    Else
                        bjadjprob = 0
                    End If
                    If (upcard = 1 And Not CheckAce) Or (upcard = 10 And Not CheckTen) Then
                        newEVs.SurrEV(upcard) = (SurrPays * (1 - bjadjprob) + SurrDBJPays * bjadjprob)
                    Else
                        newEVs.SurrEV(upcard) = ((SurrPays * (1 - bjadjprob) + SurrDBJPays * bjadjprob) + bjadjprob) / (1 - bjadjprob)
                    End If
            End Select
        End If

        Return newEVs

    End Function

    Private Sub ComputeSurrender()
        Dim index As Integer
        Dim upcard As Integer
        Dim newEVs As New BJCAHandEVsClass

        For index = 1 To NumPHands
            If PlayerHands(index).Hand.NumCards > 1 Then
                For upcard = 1 To 10
                    If SurrenderNeeded(index, upcard) Then
                        If PlayerHands(index).RPreallowed(upcard) > 0 Then
                            newEVs = ComputeSurrenderHand(PlayerHands(index).Hand, PlayerHands(index).RPreallowed(upcard), upcard)
                        Else
                            newEVs = ComputeSurrenderHand(PlayerHands(index).Hand, C.Surr.LS, upcard)
                        End If
                        PlayerHands(index).HandEVs.SurrEV(upcard) = newEVs.SurrEV(upcard)
                    End If
                Next upcard
            End If
        Next index
    End Sub

#End Region

#Region " Bonus Methods "

    Private Function ApplyBonusRulesNonSuitedHand(ByVal pHand As BJCAPlayerHandClass, ByVal upcard As Integer, ByVal bjprob As Double, ByVal postSplit As Boolean, ByVal postDouble As Boolean) As BJCAHandEVsClass
        Dim apply As Boolean
        Dim winEV As Double
        Dim loseEV As Double
        Dim pushEV As Double
        Dim netEV As Double
        Dim tempBJPayoff As Double

        Dim rule As Integer
        Dim newHandEVs As New BJCAHandEVsClass

        Dim noRulesApply As Boolean

        noRulesApply = True
        'Make sure the rule isn't applied to player BJ pre-split
        If Not pHand.Hand.IsBJ Or postSplit Then
            'netEV = winEV - loseEV
            'winEV = netEV + loseEV
            'push = 1 - winEV - loseEV = 1 - (netEV + loseEV) - loseEV = 1 - netEV - 2*loseEV
            'loseEV = (1 - netEV - push) / 2
            loseEV = (1 - pHand.HandEVs.StandEV(upcard) - pHand.HandEVs.StandPushEV(upcard)) / 2
            winEV = pHand.HandEVs.StandEV(upcard) + loseEV
            pushEV = pHand.HandEVs.StandPushEV(upcard)

            For rule = 0 To BonusRulesList.NumRules - 1
                If BonusRulesList.L(rule).RuleOn Then
                    'First see if the hand qualifies to have the rule applied to it
                    apply = False

                    'If post-split bonuses are added then add the following below:      ((Not postSplit And BonusRulesList.L(rule).PreSplit) Or (postSplit And BonusRulesList.L(rule).PostSplit)) And 

                    If (BonusRulesList.L(rule).PreSplit And Not postSplit) Or (BonusRulesList.L(rule).PostSplit And postSplit) Then
                        If BonusRulesList.L(rule).UseSpecificHand Then
                            If (BonusRulesList.L(rule).ExactMatch And pHand.Hand.SameAs(BonusRulesList.L(rule).Hand)) Or (Not BonusRulesList.L(rule).ExactMatch And pHand.Hand.Includes(BonusRulesList.L(rule).Hand)) Then
                                apply = True
                            End If
                        Else
                            If pHand.Hand.Total = BonusRulesList.L(rule).Hand.Total Or BonusRulesList.L(rule).Hand.Total = 0 Then
                                If (Not BonusRulesList.L(rule).HardSoftOnly) Or (BonusRulesList.L(rule).HardSoftOnly And Not BonusRulesList.L(rule).Hand.Soft And Not pHand.Hand.Soft) Or (BonusRulesList.L(rule).HardSoftOnly And BonusRulesList.L(rule).Hand.Soft And pHand.Hand.Soft) Then
                                    If (BonusRulesList.L(rule).Hand.NumCards = 0) Or (BonusRulesList.L(rule).Hand.NumCards = pHand.Hand.NumCards) Or (BonusRulesList.L(rule).OrLess And pHand.Hand.NumCards <= BonusRulesList.L(rule).Hand.NumCards) Or (BonusRulesList.L(rule).OrMore And pHand.Hand.NumCards >= BonusRulesList.L(rule).Hand.NumCards) Then
                                        apply = True
                                    End If
                                End If
                            End If
                        End If
                    End If

                    If apply Then
                        noRulesApply = False
                        newHandEVs.Empty()

                        'Apply the upcard limitation on BJ Payoffs here
                        '   No need to check for other UC's since bjprob is 0 for them
                        If (upcard = 1 And BonusRulesList.L(rule).BJAUp) Or (upcard = 10 And BonusRulesList.L(rule).BJTUp) Then
                            tempBJPayoff = BonusRulesList.L(rule).PayoffGeneralBJ
                        ElseIf pHand.Hand.Total = 21 And P21AutoWin Then
                            tempBJPayoff = 1
                        Else
                            tempBJPayoff = -1
                        End If

                        'Now adjust the evs
                        'The upcard can only be non-zero and therefore only match if UCPayoff <> 0
                        If (upcard <> 1 And upcard <> 10) Or (upcard = 1 And CheckAce And (pHand.Hand.NumCards > 2 Or postSplit)) Or (upcard = 10 And CheckTen And (pHand.Hand.NumCards > 2 Or postSplit)) Then
                            'No BJ Possible if UC<>(1 or 10) or if Checking and >2 cards in hand
                            If Not BonusRulesList.L(rule).MustWin And Not BonusRulesList.L(rule).HandContinues Then
                                If BonusRulesList.L(rule).PayoffGeneral = 0 And Not (BonusRulesList.L(rule).Upcard = upcard And BonusRulesList.L(rule).PayoffUCGeneral > 0) Then
                                    newHandEVs.StandEV(upcard) = (winEV - loseEV)
                                    'Only need to include pushevs when not winning since otherwise adding 0
                                    newHandEVs.StandPushEV(upcard) = pushEV
                                ElseIf (BonusRulesList.L(rule).Upcard = upcard And BonusRulesList.L(rule).PayoffUCGeneral > 0) Then
                                    newHandEVs.StandEV(upcard) = BonusRulesList.L(rule).PayoffUCGeneral
                                Else
                                    newHandEVs.StandEV(upcard) = BonusRulesList.L(rule).PayoffGeneral
                                End If

                            ElseIf BonusRulesList.L(rule).HandContinues Then
                                If BonusRulesList.L(rule).PayoffGeneral = 0 And Not (BonusRulesList.L(rule).Upcard = upcard And BonusRulesList.L(rule).PayoffUCGeneral > 0) Then
                                    newHandEVs.StandEV(upcard) = (winEV - loseEV)
                                    'Only need to include pushevs when not winning since otherwise adding 0
                                    newHandEVs.StandPushEV(upcard) = pushEV
                                ElseIf (BonusRulesList.L(rule).Upcard = upcard And BonusRulesList.L(rule).PayoffUCGeneral > 0) Then
                                    newHandEVs.StandEV(upcard) = (winEV - loseEV)
                                    newHandEVs.BonusEV(upcard) = BonusRulesList.L(rule).PayoffUCGeneral
                                    newHandEVs.StandPushEV(upcard) = pushEV
                                Else
                                    newHandEVs.StandEV(upcard) = (winEV - loseEV)
                                    newHandEVs.BonusEV(upcard) = BonusRulesList.L(rule).PayoffGeneral
                                    newHandEVs.StandPushEV(upcard) = pushEV
                                End If

                            Else
                                'Since hand Must Win, pushev is unaffected
                                newHandEVs.StandPushEV(upcard) = pushEV
                                If BonusRulesList.L(rule).PayoffGeneral = 0 And Not (BonusRulesList.L(rule).Upcard = upcard And BonusRulesList.L(rule).PayoffUCGeneral > 0) Then
                                    newHandEVs.StandEV(upcard) = (winEV - loseEV)
                                ElseIf (BonusRulesList.L(rule).Upcard = upcard And BonusRulesList.L(rule).PayoffUCGeneral > 0) Then
                                    newHandEVs.StandEV(upcard) = (BonusRulesList.L(rule).PayoffUCGeneral * winEV - loseEV)
                                Else
                                    newHandEVs.StandEV(upcard) = (BonusRulesList.L(rule).PayoffGeneral * winEV - loseEV)
                                End If
                            End If


                        ElseIf (upcard = 1 And Not CheckAce) Or (upcard = 10 And Not CheckTen) Then
                            'No checking for BJ but BJ possible
                            'BJStandEV is assigned but ignored during the game EV calcs since it's incorporated in the regular ev
                            '   It is used however for post-split OBBO calcs
                            If Not BonusRulesList.L(rule).MustWin And Not BonusRulesList.L(rule).HandContinues Then
                                If BonusRulesList.L(rule).PayoffGeneral = 0 And Not (BonusRulesList.L(rule).Upcard = upcard And BonusRulesList.L(rule).PayoffUCGeneral > 0) Then
                                    newHandEVs.StandEV(upcard) = (winEV - loseEV + bjprob) + bjprob * tempBJPayoff
                                    'Only need to include pushevs when not winning since otherwise adding 0
                                    newHandEVs.StandPushEV(upcard) = pushEV
                                    newHandEVs.BJStandEV(upcard) = tempBJPayoff
                                ElseIf (BonusRulesList.L(rule).Upcard = upcard And BonusRulesList.L(rule).PayoffUCGeneral > 0) Then
                                    newHandEVs.StandEV(upcard) = ((1 - bjprob) * BonusRulesList.L(rule).PayoffUCGeneral + bjprob * tempBJPayoff)
                                    newHandEVs.BJStandEV(upcard) = tempBJPayoff
                                Else
                                    newHandEVs.StandEV(upcard) = ((1 - bjprob) * BonusRulesList.L(rule).PayoffGeneral + bjprob * tempBJPayoff)
                                    newHandEVs.BJStandEV(upcard) = tempBJPayoff
                                End If
                            ElseIf BonusRulesList.L(rule).HandContinues Then
                                If BonusRulesList.L(rule).PayoffGeneral = 0 And Not (BonusRulesList.L(rule).Upcard = upcard And BonusRulesList.L(rule).PayoffUCGeneral > 0) Then
                                    newHandEVs.StandEV(upcard) = (winEV - loseEV + bjprob) + bjprob * tempBJPayoff
                                    'Only need to include pushevs when not winning since otherwise adding 0
                                    newHandEVs.StandPushEV(upcard) = pushEV
                                    newHandEVs.BJStandEV(upcard) = tempBJPayoff
                                ElseIf (BonusRulesList.L(rule).Upcard = upcard And BonusRulesList.L(rule).PayoffUCGeneral > 0) Then
                                    newHandEVs.StandEV(upcard) = (winEV - loseEV + bjprob) + bjprob * tempBJPayoff
                                    newHandEVs.BonusEV(upcard) = BonusRulesList.L(rule).PayoffUCGeneral
                                    newHandEVs.StandPushEV(upcard) = pushEV
                                    newHandEVs.BJStandEV(upcard) = tempBJPayoff
                                Else
                                    newHandEVs.StandEV(upcard) = (winEV - loseEV + bjprob) + bjprob * tempBJPayoff
                                    newHandEVs.BonusEV(upcard) = BonusRulesList.L(rule).PayoffGeneral
                                    newHandEVs.StandPushEV(upcard) = pushEV
                                    newHandEVs.BJStandEV(upcard) = tempBJPayoff
                                End If
                            Else
                                'Since hand Must Win, pushev is unaffected and hand cannot beat BJ
                                '  BJPayoff will be -1 automatically from the form with must win checked
                                newHandEVs.StandPushEV(upcard) = pushEV
                                newHandEVs.BJStandEV(upcard) = tempBJPayoff
                                If BonusRulesList.L(rule).PayoffGeneral = 0 And Not (BonusRulesList.L(rule).Upcard = upcard And BonusRulesList.L(rule).PayoffUCGeneral > 0) Then
                                    newHandEVs.StandEV(upcard) = (winEV - loseEV)
                                ElseIf (BonusRulesList.L(rule).Upcard = upcard And BonusRulesList.L(rule).PayoffUCGeneral > 0) Then
                                    newHandEVs.StandEV(upcard) = (BonusRulesList.L(rule).PayoffUCGeneral * winEV - loseEV)
                                Else
                                    newHandEVs.StandEV(upcard) = (BonusRulesList.L(rule).PayoffGeneral * winEV - loseEV)
                                End If
                            End If


                        Else
                            'BJ Possible and dealer checking and player's hand has 2 cards presplit
                            'No BJ adjustments need to be made when the hand doesn't beat BJ
                            'I.e. if PayoffBJ is -1, then the hand is already appropriately conditioned
                            If Not BonusRulesList.L(rule).MustWin And Not BonusRulesList.L(rule).HandContinues Then
                                If BonusRulesList.L(rule).PayoffGeneral = 0 And Not (BonusRulesList.L(rule).Upcard = upcard And BonusRulesList.L(rule).PayoffUCGeneral > 0) Then
                                    newHandEVs.StandEV(upcard) = (winEV - loseEV)
                                    'Only need to include pushevs when not winning since otherwise adding 0
                                    newHandEVs.StandPushEV(upcard) = pushEV
                                    newHandEVs.BJStandEV(upcard) = tempBJPayoff
                                ElseIf (BonusRulesList.L(rule).Upcard = upcard And BonusRulesList.L(rule).PayoffUCGeneral > 0) Then
                                    'NetPayoff = pBJ*PayoffBJ + (1-pBJ)*Payoff
                                    'NetPayoff = pBJ*(-1) + (1-pBJ)*CEV
                                    'CEV = (NetPayoff + pBJ)/(1-pBJ)
                                    '                                   netEV = bjprob * tempBJPayoff + (1 - bjprob) * BonusRulesList.L(rule).PayoffUC
                                    '                                   newHandEVs.StandEV(upcard) =  (netEV + bjprob) / (1 - bjprob)
                                    newHandEVs.StandEV(upcard) = BonusRulesList.L(rule).PayoffUCGeneral
                                    newHandEVs.StandPushEV(upcard) = 0
                                    newHandEVs.BJStandEV(upcard) = tempBJPayoff
                                Else
                                    '                                    netEV = bjprob * tempBJPayoff + (1 - bjprob) * BonusRulesList.L(rule).PayoffGeneral
                                    '                                    newHandEVs.StandEV(upcard) =  (netEV + bjprob) / (1 - bjprob)
                                    newHandEVs.StandEV(upcard) = BonusRulesList.L(rule).PayoffGeneral
                                    newHandEVs.StandPushEV(upcard) = 0
                                    newHandEVs.BJStandEV(upcard) = tempBJPayoff
                                End If
                            ElseIf BonusRulesList.L(rule).HandContinues Then
                                If BonusRulesList.L(rule).PayoffGeneral = 0 And Not (BonusRulesList.L(rule).Upcard = upcard And BonusRulesList.L(rule).PayoffUCGeneral > 0) Then
                                    newHandEVs.StandEV(upcard) = (winEV - loseEV)
                                    'Only need to include pushevs when not winning since otherwise adding 0
                                    newHandEVs.BJStandEV(upcard) = tempBJPayoff
                                    newHandEVs.StandPushEV(upcard) = pushEV
                                ElseIf (BonusRulesList.L(rule).Upcard = upcard And BonusRulesList.L(rule).PayoffUCGeneral > 0) Then
                                    'NetPayoff = pBJ*PayoffBJ + (1-pBJ)*Payoff
                                    'NetPayoff = pBJ*(-1) + (1-pBJ)*CEV
                                    'CEV = (NetPayoff + pBJ)/(1-pBJ)
                                    '                                    netEV = bjprob * tempBJPayoff + (1 - bjprob) * ((winEV - loseEV) + BonusRulesList.L(rule).PayoffUC)
                                    '                                    newHandEVs.StandEV(upcard) =  (netEV + bjprob) / (1 - bjprob)
                                    newHandEVs.StandEV(upcard) = (winEV - loseEV)
                                    newHandEVs.BonusEV(upcard) = BonusRulesList.L(rule).PayoffUCGeneral
                                    newHandEVs.BJStandEV(upcard) = tempBJPayoff
                                    newHandEVs.StandPushEV(upcard) = pushEV
                                Else
                                    '                                    netEV = bjprob * tempBJPayoff + (1 - bjprob) * ((winEV - loseEV) + BonusRulesList.L(rule).PayoffGeneral)
                                    '                                    newHandEVs.StandEV(upcard) =  (netEV + bjprob) / (1 - bjprob)
                                    newHandEVs.StandEV(upcard) = (winEV - loseEV)
                                    newHandEVs.BonusEV(upcard) = BonusRulesList.L(rule).PayoffGeneral
                                    newHandEVs.BJStandEV(upcard) = tempBJPayoff
                                    newHandEVs.StandPushEV(upcard) = pushEV
                                End If
                            Else
                                'Since hand Must Win, pushev is unaffected and BJ hand cannot beat BJ
                                '  BJPayoff will be -1 automatically from the form with must win checked
                                newHandEVs.StandPushEV(upcard) = pushEV
                                newHandEVs.BJStandEV(upcard) = tempBJPayoff
                                If BonusRulesList.L(rule).PayoffGeneral = 0 And Not (BonusRulesList.L(rule).Upcard = upcard And BonusRulesList.L(rule).PayoffUCGeneral > 0) Then
                                    newHandEVs.StandEV(upcard) = (winEV - loseEV)
                                ElseIf (BonusRulesList.L(rule).Upcard = upcard And BonusRulesList.L(rule).PayoffUCGeneral > 0) Then
                                    newHandEVs.StandEV(upcard) = (BonusRulesList.L(rule).PayoffUCGeneral * winEV - loseEV)
                                Else
                                    newHandEVs.StandEV(upcard) = (BonusRulesList.L(rule).PayoffGeneral * winEV - loseEV)
                                End If
                            End If
                        End If
                    End If  'Apply
                End If  'Rule On
            Next rule

            If pHand.Hand.Total = 21 And P21AutoWin And newHandEVs.BJStandEV(upcard) = -1 Then
                newHandEVs.BJStandEV(upcard) = 1
            End If
        End If  'Not BJ

        If noRulesApply Then
            newHandEVs.StandEV(upcard) = pHand.HandEVs.StandEV(upcard)
            newHandEVs.StandPushEV(upcard) = pHand.HandEVs.StandPushEV(upcard)
        End If

        Return newHandEVs
    End Function

    Private Sub ApplyBonusRulesPreSuited()
        'Apply both the Dealer-Player Push rules as well as the bonus Rules.
        'Bonus Rules override Dealer-Player push rules 
        Dim index As Integer
        Dim upcard As Integer
        Dim bjprob As Double
        Dim newEVs As New BJCAHandEVsClass

        If BonusRuleOn Then
            CurrentShoe.Reset(OriginalShoe)
            For index = 1 To NumPHands
                'Adjustment for player blackjack is separate
                If PlayerHands(index).Hand.NumCards > 1 And Not PlayerHands(index).Hand.IsBJ Then
                    For upcard = 1 To 10
                        If UCAllowed(upcard) And CardProb(upcard, 0) > 0 Then
                            If upcard = 1 Then
                                bjprob = BJHandNumerator(10, PlayerHands(index).Hand.Cards(10), 1) / BJHandDivisor(PlayerHands(index).Hand.NumCards + 1, 1)
                            ElseIf upcard = 10 Then
                                bjprob = BJHandNumerator(1, PlayerHands(index).Hand.Cards(1), 1) / BJHandDivisor(PlayerHands(index).Hand.NumCards + 1, 1)
                            End If

                            'Rules will be applied in sequence and override the previous rules.
                            newEVs.Empty()
                            newEVs = ApplyBonusRulesNonSuitedHand(PlayerHands(index), upcard, bjprob, False, False)
                            PlayerHands(index).HandEVs.StandEV(upcard) = newEVs.StandEV(upcard)
                            PlayerHands(index).HandEVs.BJStandEV(upcard) = newEVs.BJStandEV(upcard)
                            PlayerHands(index).HandEVs.StandPushEV(upcard) = newEVs.StandPushEV(upcard)
                            PlayerHands(index).HandEVs.BonusEV(upcard) = newEVs.BonusEV(upcard)
                        End If
                    Next
                End If
            Next index
        End If
    End Sub

    Private Function ApplyBonusRuleSuitedHand(ByVal rule As Integer, ByRef pHand As BJCAPlayerHandClass, ByVal upcard As Integer, ByVal bjprob As Double) As BJCASuitedBonusEVClass
        Dim winEV As Double
        Dim loseEV As Double
        Dim pushEV As Double
        Dim netEV As Double
        Dim tempBJPayoff As Double

        Dim suit As Integer
        Dim card As Integer
        Dim i As Integer

        Dim pSuited As Double
        Dim pSpecificSuit As Double

        Dim newHandEVs As New BJCASuitedBonusEVClass

        'Make sure the rule isn't applied to player BJ
        If Not pHand.Hand.IsBJ Then
            'netEV = winEV - loseEV
            'winEV = netEV + loseEV
            'push = 1 - winEV - loseEV = 1 - (netEV + loseEV) - loseEV = 1 - netEV - 2*loseEV
            'loseEV = (1 - netEV - push) / 2
            loseEV = (1 - pHand.HandEVs.StandEV(upcard) - pHand.HandEVs.StandPushEV(upcard)) / 2
            winEV = pHand.HandEVs.StandEV(upcard) + loseEV
            pushEV = pHand.HandEVs.StandPushEV(upcard)

            If BonusRulesList.L(Rule).RuleOn And BonusRulesList.L(Rule).Suited Then
                'First see if the hand qualifies to have the rule applied to it

                For suit = 0 To 3
                    If pHand.HandEVs.SuitedPossible(upcard, suit) Then
                        If BonusRulesList.L(Rule).SpecificSuit Then
                            If BonusRulesList.L(Rule).SuitToWin = suit Then
                                pSuited = 0
                                pSpecificSuit = 1
                            Else
                                pSuited = 1
                                pSpecificSuit = 0
                            End If
                        Else
                            pSuited = 1
                            pSpecificSuit = 0
                        End If

                        'Apply the upcard limitation on BJ Payoffs here
                        '   No need to check for other UC's since bjprob is 0 for them
                        If (upcard = 1 And BonusRulesList.L(Rule).BJAUp) Or (upcard = 10 And BonusRulesList.L(Rule).BJTUp) Then
                            If pSuited > 0 Then
                                tempBJPayoff = BonusRulesList.L(Rule).PayoffSuitedBJ
                                newHandEVs.SuitedStandBJEV(upcard, suit) = 0
                            ElseIf pSpecificSuit > 0 Then
                                tempBJPayoff = BonusRulesList.L(Rule).PayoffSpecificSuitBJ
                                newHandEVs.SuitedStandBJEV(upcard, suit) = 0
                            Else
                                tempBJPayoff = BonusRulesList.L(Rule).PayoffSuitedBJ
                                newHandEVs.SuitedStandBJEV(upcard, suit) = tempBJPayoff
                            End If
                        Else
                            tempBJPayoff = -1
                        End If

                        'Now adjust the evs
                        'The upcard can only be non-zero and therefore only match if UCPayoff <> 0
                        If (upcard <> 1 And upcard <> 10) Or (upcard = 1 And CheckAce And pHand.Hand.NumCards > 2) Or (upcard = 10 And CheckTen And pHand.Hand.NumCards > 2) Then
                            'No BJ Possible if UC<>(1 or 10) or if Checking and >2 cards in hand
                            If Not BonusRulesList.L(Rule).MustWin And Not BonusRulesList.L(Rule).HandContinues Then
                                'Only need to include pushevs when not winning since otherwise adding 0
                                If pSuited > 0 Then
                                    If BonusRulesList.L(Rule).PayoffSuited = 0 And Not (BonusRulesList.L(Rule).Upcard = upcard And BonusRulesList.L(Rule).PayoffUCSuited > 0) Then
                                        newHandEVs.SuitedStandEV(upcard, suit) = pSuited * (winEV - loseEV)
                                        '                                newHandEVs.StandPushEV(upcard) = pSuited * pushEV
                                    ElseIf (BonusRulesList.L(Rule).Upcard = upcard And BonusRulesList.L(Rule).PayoffUCSuited > 0) Then
                                        newHandEVs.SuitedStandEV(upcard, suit) = pSuited * BonusRulesList.L(Rule).PayoffUCSuited
                                    Else
                                        newHandEVs.SuitedStandEV(upcard, suit) = pSuited * BonusRulesList.L(Rule).PayoffSuited
                                    End If
                                End If

                                If pSpecificSuit > 0 Then
                                    If BonusRulesList.L(Rule).PayoffSpecificSuit = 0 And Not (BonusRulesList.L(Rule).Upcard = upcard And BonusRulesList.L(Rule).PayoffUCSpecificSuit > 0) Then
                                        newHandEVs.SuitedStandEV(upcard, suit) = pSpecificSuit * (winEV - loseEV)
                                        '                                newHandEVs.StandPushEV(upcard) = pSpecificSuit * pushEV
                                    ElseIf (BonusRulesList.L(Rule).Upcard = upcard And BonusRulesList.L(Rule).PayoffUCSpecificSuit > 0) Then
                                        newHandEVs.SuitedStandEV(upcard, suit) = pSpecificSuit * BonusRulesList.L(Rule).PayoffUCSpecificSuit
                                    Else
                                        newHandEVs.SuitedStandEV(upcard, suit) = pSpecificSuit * BonusRulesList.L(Rule).PayoffSpecificSuit
                                    End If
                                End If
                            ElseIf BonusRulesList.L(Rule).HandContinues Then
                                'Only need to include pushevs when not winning since otherwise adding 0
                                If pSuited > 0 Then
                                    If BonusRulesList.L(Rule).PayoffSuited = 0 And Not (BonusRulesList.L(Rule).Upcard = upcard And BonusRulesList.L(Rule).PayoffUCSuited > 0) Then
                                        newHandEVs.SuitedStandEV(upcard, suit) = pSuited * (winEV - loseEV)
                                        '                                newHandEVs.StandPushEV(upcard) = pSuited * pushEV
                                    ElseIf (BonusRulesList.L(Rule).Upcard = upcard And BonusRulesList.L(Rule).PayoffUCSuited > 0) Then
                                        newHandEVs.SuitedStandEV(upcard, suit) = pSuited * (winEV - loseEV)
                                        newHandEVs.SuitedBonusEV(upcard, suit) = pSuited * BonusRulesList.L(Rule).PayoffUCSuited
                                        '                                newHandEVs.StandPushEV(upcard) = pSuited * pushEV
                                    Else
                                        newHandEVs.SuitedStandEV(upcard, suit) = pSuited * (winEV - loseEV)
                                        newHandEVs.SuitedBonusEV(upcard, suit) = pSuited * BonusRulesList.L(Rule).PayoffSuited
                                        '                                newHandEVs.StandPushEV(upcard) = pSuited * pushEV
                                    End If
                                End If

                                If pSpecificSuit > 0 Then
                                    If BonusRulesList.L(Rule).PayoffSpecificSuit = 0 And Not (BonusRulesList.L(Rule).Upcard = upcard And BonusRulesList.L(Rule).PayoffUCSpecificSuit > 0) Then
                                        newHandEVs.SuitedStandEV(upcard, suit) = pSpecificSuit * (winEV - loseEV)
                                        '                                newHandEVs.StandPushEV(upcard) = pSpecificSuit * pushEV
                                    ElseIf (BonusRulesList.L(Rule).Upcard = upcard And BonusRulesList.L(Rule).PayoffUCSpecificSuit > 0) Then
                                        newHandEVs.SuitedStandEV(upcard, suit) = pSpecificSuit * (winEV - loseEV)
                                        newHandEVs.SuitedBonusEV(upcard, suit) = pSpecificSuit * BonusRulesList.L(Rule).PayoffUCSpecificSuit
                                        '                                newHandEVs.StandPushEV(upcard) = pSpecificSuit * pushEV
                                    Else
                                        newHandEVs.SuitedStandEV(upcard, suit) = pSpecificSuit * (winEV - loseEV)
                                        newHandEVs.SuitedBonusEV(upcard, suit) = pSpecificSuit * BonusRulesList.L(Rule).PayoffSpecificSuit
                                        '                                newHandEVs.StandPushEV(upcard) = pSpecificSuit * pushEV
                                    End If
                                End If
                            Else
                                'Since hand Must Win, pushev is unaffected
                                '                            newHandEVs.StandPushEV(upcard) = pushEV
                                If pSuited > 0 Then
                                    If BonusRulesList.L(Rule).PayoffSuited = 0 And Not (BonusRulesList.L(Rule).Upcard = upcard And BonusRulesList.L(Rule).PayoffUCSuited > 0) Then
                                        newHandEVs.SuitedStandEV(upcard, suit) = pSuited * (winEV - loseEV)
                                    ElseIf (BonusRulesList.L(Rule).Upcard = upcard And BonusRulesList.L(Rule).PayoffUCSuited > 0) Then
                                        newHandEVs.SuitedStandEV(upcard, suit) = pSuited * (BonusRulesList.L(Rule).PayoffUCSuited * winEV - loseEV)
                                    Else
                                        newHandEVs.SuitedStandEV(upcard, suit) = pSuited * (BonusRulesList.L(Rule).PayoffSuited * winEV - loseEV)
                                    End If
                                End If

                                If pSpecificSuit > 0 Then
                                    If BonusRulesList.L(Rule).PayoffSpecificSuit = 0 And Not (BonusRulesList.L(Rule).Upcard = upcard And BonusRulesList.L(Rule).PayoffUCSpecificSuit > 0) Then
                                        newHandEVs.SuitedStandEV(upcard, suit) = pSpecificSuit * (winEV - loseEV)
                                    ElseIf (BonusRulesList.L(Rule).Upcard = upcard And BonusRulesList.L(Rule).PayoffUCSpecificSuit > 0) Then
                                        newHandEVs.SuitedStandEV(upcard, suit) = pSpecificSuit * (BonusRulesList.L(Rule).PayoffUCSpecificSuit * winEV - loseEV)
                                    Else
                                        newHandEVs.SuitedStandEV(upcard, suit) = pSpecificSuit * (BonusRulesList.L(Rule).PayoffSpecificSuit * winEV - loseEV)
                                    End If
                                End If
                            End If


                        ElseIf (upcard = 1 And Not CheckAce) Or (upcard = 10 And Not CheckTen) Then
                            'No checking for BJ but BJ possible
                            If Not BonusRulesList.L(Rule).MustWin And Not BonusRulesList.L(Rule).HandContinues Then
                                'Only need to include pushevs when not winning since otherwise adding 0
                                If pSuited > 0 Then
                                    If BonusRulesList.L(Rule).PayoffSuited = 0 And Not (BonusRulesList.L(Rule).Upcard = upcard And BonusRulesList.L(Rule).PayoffUCSuited > 0) Then
                                        newHandEVs.SuitedStandEV(upcard, suit) = pSuited * ((1 - bjprob) * ((winEV - loseEV) + bjprob) + bjprob * tempBJPayoff)
                                        newHandEVs.SuitedStandBJEV(upcard, suit) = pSuited * tempBJPayoff
                                        '                                newHandEVs.StandPushEV(upcard) = pSuited * pushEV
                                    ElseIf (BonusRulesList.L(Rule).Upcard = upcard And BonusRulesList.L(Rule).PayoffUCSuited > 0) Then
                                        newHandEVs.SuitedStandEV(upcard, suit) = pSuited * ((1 - bjprob) * BonusRulesList.L(Rule).PayoffUCSuited + bjprob * tempBJPayoff)
                                        newHandEVs.SuitedStandBJEV(upcard, suit) = pSuited * tempBJPayoff
                                    Else
                                        newHandEVs.SuitedStandEV(upcard, suit) = pSuited * ((1 - bjprob) * BonusRulesList.L(Rule).PayoffSuited + bjprob * tempBJPayoff)
                                        newHandEVs.SuitedStandBJEV(upcard, suit) = pSuited * tempBJPayoff
                                    End If
                                End If

                                If pSpecificSuit > 0 Then
                                    If BonusRulesList.L(Rule).PayoffSpecificSuit = 0 And Not (BonusRulesList.L(Rule).Upcard = upcard And BonusRulesList.L(Rule).PayoffUCSpecificSuit > 0) Then
                                        newHandEVs.SuitedStandEV(upcard, suit) = pSpecificSuit * ((1 - bjprob) * ((winEV - loseEV) + bjprob) + bjprob * tempBJPayoff)
                                        newHandEVs.SuitedStandBJEV(upcard, suit) = pSpecificSuit * tempBJPayoff
                                        '                                newHandEVs.StandPushEV(upcard) = pSpecificSuit * pushEV
                                    ElseIf (BonusRulesList.L(Rule).Upcard = upcard And BonusRulesList.L(Rule).PayoffUCSpecificSuit > 0) Then
                                        newHandEVs.SuitedStandEV(upcard, suit) = pSpecificSuit * ((1 - bjprob) * BonusRulesList.L(Rule).PayoffUCSpecificSuit + bjprob * tempBJPayoff)
                                        newHandEVs.SuitedStandBJEV(upcard, suit) = pSpecificSuit * tempBJPayoff
                                    Else
                                        newHandEVs.SuitedStandEV(upcard, suit) = pSpecificSuit * ((1 - bjprob) * BonusRulesList.L(Rule).PayoffSpecificSuit + bjprob * tempBJPayoff)
                                        newHandEVs.SuitedStandBJEV(upcard, suit) = pSpecificSuit * tempBJPayoff
                                    End If
                                End If
                            ElseIf BonusRulesList.L(Rule).HandContinues Then
                                'Only need to include pushevs when not winning since otherwise adding 0
                                If pSuited > 0 Then
                                    If BonusRulesList.L(Rule).PayoffSuited = 0 And (BonusRulesList.L(Rule).Upcard = upcard And BonusRulesList.L(Rule).PayoffUCSuited > 0) Then
                                        newHandEVs.SuitedStandEV(upcard, suit) = pSuited * (winEV - loseEV)
                                        '                                newHandEVs.StandPushEV(upcard) = pSuited * pushEV
                                    ElseIf (BonusRulesList.L(Rule).Upcard = upcard And BonusRulesList.L(Rule).PayoffUCSuited > 0) Then
                                        newHandEVs.SuitedStandEV(upcard, suit) = pSuited * (winEV - loseEV)
                                        newHandEVs.SuitedBonusEV(upcard, suit) = pSuited * BonusRulesList.L(Rule).PayoffUCSuited
                                        '                                newHandEVs.StandPushEV(upcard) = pSuited * pushEV
                                    Else
                                        newHandEVs.SuitedStandEV(upcard, suit) = pSuited * (winEV - loseEV)
                                        newHandEVs.SuitedBonusEV(upcard, suit) = pSuited * BonusRulesList.L(Rule).PayoffSuited
                                        '                                newHandEVs.StandPushEV(upcard) = pSuited * pushEV
                                    End If
                                End If

                                If pSpecificSuit > 0 Then
                                    If BonusRulesList.L(Rule).PayoffSpecificSuit = 0 And Not (BonusRulesList.L(Rule).Upcard = upcard And BonusRulesList.L(Rule).PayoffUCSpecificSuit > 0) Then
                                        newHandEVs.SuitedStandEV(upcard, suit) = pSpecificSuit * (winEV - loseEV)
                                        '                                newHandEVs.StandPushEV(upcard) = pSpecificSuit * pushEV
                                    ElseIf (BonusRulesList.L(Rule).Upcard = upcard And BonusRulesList.L(Rule).PayoffUCSpecificSuit > 0) Then
                                        newHandEVs.SuitedStandEV(upcard, suit) = pSpecificSuit * (winEV - loseEV)
                                        newHandEVs.SuitedBonusEV(upcard, suit) = pSpecificSuit * BonusRulesList.L(Rule).PayoffUCSpecificSuit
                                        '                                newHandEVs.StandPushEV(upcard) = pSpecificSuit * pushEV
                                    Else
                                        newHandEVs.SuitedStandEV(upcard, suit) = pSpecificSuit * (winEV - loseEV)
                                        newHandEVs.SuitedBonusEV(upcard, suit) = pSpecificSuit * BonusRulesList.L(Rule).PayoffSpecificSuit
                                        '                                newHandEVs.StandPushEV(upcard) = pSpecificSuit * pushEV
                                    End If
                                End If

                            Else
                                'Since hand Must Win, pushev is unaffected and hand cannot beat BJ
                                '                            newHandEVs.StandPushEV(upcard) = pushEV
                                If pSuited > 0 Then
                                    If BonusRulesList.L(Rule).PayoffSuited = 0 And Not (BonusRulesList.L(Rule).Upcard = upcard And BonusRulesList.L(Rule).PayoffUCSuited > 0) Then
                                        newHandEVs.SuitedStandEV(upcard, suit) = pSuited * (winEV - loseEV)
                                    ElseIf (BonusRulesList.L(Rule).Upcard = upcard And BonusRulesList.L(Rule).PayoffUCSuited > 0) Then
                                        newHandEVs.SuitedStandEV(upcard, suit) = pSuited * (BonusRulesList.L(Rule).PayoffUCSuited * winEV - loseEV)
                                    Else
                                        newHandEVs.SuitedStandEV(upcard, suit) = pSuited * (BonusRulesList.L(Rule).PayoffSuited * winEV - loseEV)
                                    End If
                                End If

                                If pSpecificSuit > 0 Then
                                    If BonusRulesList.L(Rule).PayoffSpecificSuit = 0 And Not (BonusRulesList.L(Rule).Upcard = upcard And BonusRulesList.L(Rule).PayoffUCSpecificSuit > 0) Then
                                        newHandEVs.SuitedStandEV(upcard, suit) = pSpecificSuit * (winEV - loseEV)
                                    ElseIf (BonusRulesList.L(Rule).Upcard = upcard And BonusRulesList.L(Rule).PayoffUCSpecificSuit > 0) Then
                                        newHandEVs.SuitedStandEV(upcard, suit) = pSpecificSuit * (BonusRulesList.L(Rule).PayoffUCSpecificSuit * winEV - loseEV)
                                    Else
                                        newHandEVs.SuitedStandEV(upcard, suit) = pSpecificSuit * (BonusRulesList.L(Rule).PayoffSpecificSuit * winEV - loseEV)
                                    End If
                                End If
                            End If


                        Else
                            'BJ Possible and dealer checking and player's hand has 2 cards
                            'No BJ adjustments need to be made when the hand doesn't beat BJ
                            'I.e. if PayoffBJ is -1, then the hand is already appropriately conditioned
                            If Not BonusRulesList.L(Rule).MustWin And Not BonusRulesList.L(Rule).HandContinues Then
                                'Only need to include pushevs when not winning since otherwise adding 0
                                If pSuited > 0 Then
                                    If BonusRulesList.L(Rule).PayoffSuited = 0 And Not (BonusRulesList.L(Rule).Upcard = upcard And BonusRulesList.L(Rule).PayoffUCSuited > 0) Then
                                        newHandEVs.SuitedStandEV(upcard, suit) = pSuited * (winEV - loseEV)
                                        newHandEVs.SuitedStandBJEV(upcard, suit) = pSuited * tempBJPayoff
                                        '                                newHandEVs.StandPushEV(upcard) = pSuited * pushEV
                                    ElseIf (BonusRulesList.L(Rule).Upcard = upcard And BonusRulesList.L(Rule).PayoffUCSuited > 0) Then
                                        '                                    netEV = bjprob * tempBJPayoff + (1 - bjprob) * BonusRulesList.L(rule).PayoffUC
                                        '                                    newHandEVs.suitedstandev(upcard,suit) = pSuited * (netEV + bjprob) / (1 - bjprob)
                                        newHandEVs.SuitedStandEV(upcard, suit) = pSuited * BonusRulesList.L(Rule).PayoffUCSuited
                                        newHandEVs.SuitedStandBJEV(upcard, suit) = pSuited * tempBJPayoff
                                    Else
                                        '                                    netEV = bjprob * tempBJPayoff + (1 - bjprob) * BonusRulesList.L(rule).PayoffSuited
                                        '                                    newHandEVs.suitedstandev(upcard,suit) = pSuited * (netEV + bjprob) / (1 - bjprob)
                                        newHandEVs.SuitedStandEV(upcard, suit) = pSuited * BonusRulesList.L(Rule).PayoffSuited
                                        newHandEVs.SuitedStandBJEV(upcard, suit) = pSuited * tempBJPayoff
                                    End If
                                End If

                                If pSpecificSuit > 0 Then
                                    If BonusRulesList.L(Rule).PayoffSpecificSuit = 0 And Not (BonusRulesList.L(Rule).Upcard = upcard And BonusRulesList.L(Rule).PayoffUCSpecificSuit > 0) Then
                                        newHandEVs.SuitedStandEV(upcard, suit) = pSpecificSuit * (winEV - loseEV)
                                        newHandEVs.SuitedStandBJEV(upcard, suit) = pSpecificSuit * tempBJPayoff
                                        '                                newHandEVs.StandPushEV(upcard) = pSpecificSuit * pushEV
                                    ElseIf (BonusRulesList.L(Rule).Upcard = upcard And BonusRulesList.L(Rule).PayoffUCSpecificSuit > 0) Then
                                        '                                    netEV = bjprob * tempBJPayoff + (1 - bjprob) * BonusRulesList.L(rule).PayoffUC
                                        '                                    newHandEVs.suitedstandev(upcard,suit) = pSpecificSuit * (netEV + bjprob) / (1 - bjprob)
                                        newHandEVs.SuitedStandEV(upcard, suit) = pSpecificSuit * BonusRulesList.L(Rule).PayoffUCSpecificSuit
                                        newHandEVs.SuitedStandBJEV(upcard, suit) = pSpecificSuit * tempBJPayoff
                                    Else
                                        '                                    netEV = bjprob * tempBJPayoff + (1 - bjprob) * BonusRulesList.L(rule).PayoffSpecificSuit
                                        '                                    newHandEVs.suitedstandev(upcard,suit) = pSpecificSuit * (netEV + bjprob) / (1 - bjprob)
                                        newHandEVs.SuitedStandEV(upcard, suit) = pSpecificSuit * BonusRulesList.L(Rule).PayoffSpecificSuit
                                        newHandEVs.SuitedStandBJEV(upcard, suit) = pSpecificSuit * tempBJPayoff
                                    End If
                                End If
                            ElseIf BonusRulesList.L(Rule).HandContinues Then
                                'Only need to include pushevs when not winning since otherwise adding 0
                                If pSuited > 0 Then
                                    If BonusRulesList.L(Rule).PayoffSuited = 0 And Not (BonusRulesList.L(Rule).Upcard = upcard And BonusRulesList.L(Rule).PayoffUCSuited > 0) Then
                                        newHandEVs.SuitedStandEV(upcard, suit) = pSuited * (winEV - loseEV)
                                        newHandEVs.SuitedStandBJEV(upcard, suit) = pSuited * (-1)
                                        '                                newHandEVs.StandPushEV(upcard) = pSuited * pushEV
                                    ElseIf (BonusRulesList.L(Rule).Upcard = upcard And BonusRulesList.L(Rule).PayoffUCSuited > 0) Then
                                        '                                    netEV = bjprob * tempBJPayoff + (1 - bjprob) * ((winEV - loseEV) + BonusRulesList.L(rule).PayoffUC)
                                        '                                    newHandEVs.suitedstandev(upcard,suit) = pSuited * (netEV + bjprob) / (1 - bjprob)
                                        newHandEVs.SuitedStandEV(upcard, suit) = pSuited * (winEV - loseEV)
                                        newHandEVs.SuitedBonusEV(upcard, suit) = pSuited * BonusRulesList.L(Rule).PayoffUCSuited
                                        newHandEVs.SuitedStandBJEV(upcard, suit) = pSuited * tempBJPayoff
                                        '                               newHandEVs.StandPushEV(upcard) = pSuited * pushEV
                                    Else
                                        '                                   netEV = bjprob * tempBJPayoff + (1 - bjprob) * ((winEV - loseEV) + BonusRulesList.L(rule).PayoffSuited)
                                        '                                   newHandEVs.suitedstandev(upcard,suit) = pSuited * (netEV + bjprob) / (1 - bjprob)
                                        newHandEVs.SuitedStandEV(upcard, suit) = pSuited * (winEV - loseEV)
                                        newHandEVs.SuitedBonusEV(upcard, suit) = pSuited * BonusRulesList.L(Rule).PayoffSuited
                                        newHandEVs.SuitedStandBJEV(upcard, suit) = pSuited * tempBJPayoff
                                        '                                newHandEVs.StandPushEV(upcard) = pSuited * pushEV
                                    End If
                                End If

                                If pSpecificSuit > 0 Then
                                    If BonusRulesList.L(Rule).PayoffSpecificSuit = 0 And Not (BonusRulesList.L(Rule).Upcard = upcard And BonusRulesList.L(Rule).PayoffUCSpecificSuit > 0) Then
                                        newHandEVs.SuitedStandEV(upcard, suit) = pSpecificSuit * (winEV - loseEV)
                                        newHandEVs.SuitedStandBJEV(upcard, suit) = pSpecificSuit * (-1)
                                        '                                newHandEVs.StandPushEV(upcard) = pSpecificSuit * pushEV
                                    ElseIf (BonusRulesList.L(Rule).Upcard = upcard And BonusRulesList.L(Rule).PayoffUCSpecificSuit > 0) Then
                                        '                                    netEV = bjprob * tempBJPayoff + (1 - bjprob) * ((winEV - loseEV) + BonusRulesList.L(rule).PayoffUC)
                                        '                                    newHandEVs.suitedstandev(upcard,suit) = pSpecificSuit * (netEV + bjprob) / (1 - bjprob)
                                        newHandEVs.SuitedStandEV(upcard, suit) = pSpecificSuit * (winEV - loseEV)
                                        newHandEVs.SuitedBonusEV(upcard, suit) = pSpecificSuit * BonusRulesList.L(Rule).PayoffUCSpecificSuit
                                        newHandEVs.SuitedStandBJEV(upcard, suit) = pSpecificSuit * tempBJPayoff
                                        '                                newHandEVs.StandPushEV(upcard) = pSpecificSuit * pushEV
                                    Else
                                        '                                   netEV = bjprob * tempBJPayoff + (1 - bjprob) * ((winEV - loseEV) + BonusRulesList.L(rule).PayoffSpecificSuit)
                                        '                                   newHandEVs.suitedstandev(upcard,suit) = pSpecificSuit * (netEV + bjprob) / (1 - bjprob)
                                        newHandEVs.SuitedStandEV(upcard, suit) = pSpecificSuit * (winEV - loseEV)
                                        newHandEVs.SuitedBonusEV(upcard, suit) = pSpecificSuit * BonusRulesList.L(Rule).PayoffSpecificSuit
                                        newHandEVs.SuitedStandBJEV(upcard, suit) = pSpecificSuit * tempBJPayoff
                                        '                                newHandEVs.StandPushEV(upcard) = pSpecificSuit * pushEV
                                    End If
                                End If
                            Else
                                'Since hand Must Win, pushev is unaffected and BJ hand cannot beat BJ
                                '                            newHandEVs.StandPushEV(upcard) = pushEV
                                If pSuited > 0 Then
                                    If BonusRulesList.L(Rule).PayoffSuited = 0 And Not (BonusRulesList.L(Rule).Upcard = upcard And BonusRulesList.L(Rule).PayoffUCSuited > 0) Then
                                        newHandEVs.SuitedStandEV(upcard, suit) = pSuited * (winEV - loseEV)
                                    ElseIf (BonusRulesList.L(Rule).Upcard = upcard And BonusRulesList.L(Rule).PayoffUCSuited > 0) Then
                                        newHandEVs.SuitedStandEV(upcard, suit) = pSuited * (BonusRulesList.L(Rule).PayoffUCSuited * winEV - loseEV)
                                    Else
                                        newHandEVs.SuitedStandEV(upcard, suit) = pSuited * (BonusRulesList.L(Rule).PayoffSuited * winEV - loseEV)
                                    End If
                                End If

                                If pSpecificSuit > 0 Then
                                    If BonusRulesList.L(Rule).PayoffSpecificSuit = 0 And Not (BonusRulesList.L(Rule).Upcard = upcard And BonusRulesList.L(Rule).PayoffUCSpecificSuit > 0) Then
                                        newHandEVs.SuitedStandEV(upcard, suit) = pSpecificSuit * (winEV - loseEV)
                                    ElseIf (BonusRulesList.L(Rule).Upcard = upcard And BonusRulesList.L(Rule).PayoffUCSpecificSuit > 0) Then
                                        newHandEVs.SuitedStandEV(upcard, suit) = pSpecificSuit * (BonusRulesList.L(Rule).PayoffUCSpecificSuit * winEV - loseEV)
                                    Else
                                        newHandEVs.SuitedStandEV(upcard, suit) = pSpecificSuit * (BonusRulesList.L(Rule).PayoffSpecificSuit * winEV - loseEV)
                                    End If
                                End If
                            End If
                        End If
                    Else
                        newHandEVs.SuitedStandEV(upcard, suit) = pHand.HandEVs.StandEV(upcard)
                    End If  'Suited Possible
                Next suit
            End If
        End If  'Not BJ

        Return newHandEVs
    End Function

    Private Sub EnumSuitedBonusHandList()
        Dim index As Integer
        Dim upcard As Integer
        Dim suit As Integer
        Dim rule As Integer
        Dim apply As Boolean
        Dim bjprob As Double
        Dim newEVs As BJCASuitedBonusEVClass

        If BonusRuleOn Then
            NSuitedHands = 0
            NSuitedBonusHands = 0
            For index = 1 To NumPHands
                If index <> BJIndex And PlayerHands(index).Hand.NumCards > 1 Then
                    For rule = 0 To BonusRulesList.NumRules - 1
                        If BonusRulesList.L(rule).RuleOn And BonusRulesList.L(rule).Suited Then
                            'First see if the hand qualifies to have the rule applied to it
                            apply = False

                            'If post-split bonuses are added then add the following below:      ((Not postSplit And BonusRulesList.L(rule).PreSplit) Or (postSplit And BonusRulesList.L(rule).PostSplit)) And 

                            If BonusRulesList.L(rule).UseSpecificHand Then
                                If (BonusRulesList.L(rule).ExactMatch And PlayerHands(index).Hand.SameAs(BonusRulesList.L(rule).Hand)) Or (Not BonusRulesList.L(rule).ExactMatch And PlayerHands(index).Hand.Includes(BonusRulesList.L(rule).Hand)) Then
                                    apply = True
                                End If
                            Else
                                If PlayerHands(index).Hand.Total = BonusRulesList.L(rule).Hand.Total Or BonusRulesList.L(rule).Hand.Total = 0 Then
                                    If (Not BonusRulesList.L(rule).HardSoftOnly) Or (BonusRulesList.L(rule).HardSoftOnly And Not BonusRulesList.L(rule).Hand.Soft And Not PlayerHands(index).Hand.Soft) Or (BonusRulesList.L(rule).HardSoftOnly And BonusRulesList.L(rule).Hand.Soft And PlayerHands(index).Hand.Soft) Then
                                        If (BonusRulesList.L(rule).Hand.NumCards = 0) Or (BonusRulesList.L(rule).Hand.NumCards = PlayerHands(index).Hand.NumCards) Or (BonusRulesList.L(rule).OrLess And PlayerHands(index).Hand.NumCards <= BonusRulesList.L(rule).Hand.NumCards) Or (BonusRulesList.L(rule).OrMore And PlayerHands(index).Hand.NumCards >= BonusRulesList.L(rule).Hand.NumCards) Then
                                            apply = True
                                        End If
                                    End If
                                End If
                            End If

                            If apply Then
                                NSuitedBonusHands += 1
                                SuitedBonusHands(NSuitedBonusHands) = index

                                NSuitedHands += 1
                                SuitedHandsList(NSuitedHands) = index
                                PlayerHands(index).SuitedBonusEVs = New BJCASuitedBonusEVClass

                                For upcard = 1 To 10
                                    If UCAllowed(upcard) Then
                                        bjprob = PlayerHands(index).HandEVs.BJProb(upcard)
                                        newEVs = ApplyBonusRuleSuitedHand(rule, PlayerHands(index), upcard, bjprob)
                                        'First copy over the Stand and BJ Stand EV's
                                        For suit = 0 To 3
                                            PlayerHands(index).SuitedBonusEVs.SuitedStandEV(upcard, suit) = newEVs.SuitedStandEV(upcard, suit)
                                            PlayerHands(index).SuitedBonusEVs.SuitedStandBJEV(upcard, suit) = newEVs.SuitedStandBJEV(upcard, suit)
                                            PlayerHands(index).SuitedBonusEVs.SuitedBonusEV(upcard, suit) = newEVs.SuitedBonusEV(upcard, suit)
                                        Next suit
                                        'Now we need to determine the initial strategy
                                        '  **** Hits are ignored for these hands ?
                                        '  Currently set to include hits
                                        For suit = 0 To 3
                                            newEVs.Empty()
                                            newEVs = ComputeSuitedStratHand(Opt, index, upcard, suit, True)
                                            PlayerHands(index).SuitedBonusEVs.SuitedStratEV(upcard, suit) = newEVs.SuitedStratEV(upcard, suit)
                                            PlayerHands(index).SuitedBonusEVs.SuitedStratBJEV(upcard, suit) = newEVs.SuitedStratBJEV(upcard, suit)
                                            PlayerHands(index).SuitedBonusEVs.SuitedStrat(upcard, suit) = newEVs.SuitedStrat(upcard, suit)
                                        Next suit
                                    End If
                                Next upcard
                                Exit For
                            End If
                        End If
                    Next rule
                End If
            Next index
        End If
    End Sub

    Private Sub ComputeSuitedBonusHitStratHand(ByRef cstrat As BJCAStrategyClass, ByVal index As Integer)
        Dim card As Integer
        Dim bonusHand As Integer
        Dim newIndex As Integer
        Dim upcard As Integer
        Dim prob As Double
        Dim probsuit As Double
        Dim suit As Integer
        Dim newEVs As New BJCASuitedBonusEVClass
        Dim tempHand As New BJCAHandClass

        'First find the hand and then figure out the value of hitting
        NSuitedHands += 1
        SuitedHandsList(NSuitedHands) = index
        PlayerHands(index).SuitedBonusEVs = New BJCASuitedBonusEVClass

        For upcard = 1 To 10
            If UCAllowed(upcard) And CardProb(upcard, 0) > 0 Then
                CurrentShoe.Deal(upcard)
                For suit = 0 To 3
                    If PlayerHands(index).HandEVs.SuitedPossible(upcard, suit) Then
                        CurrentShoe.DealSuited(PlayerHands(index).Hand, suit)
                        For card = 1 To 10
                            prob = CardProb(card, upcard)
                            If prob > 0 Then
                                'BJ EV can be ignored because it is only non-zero when
                                '  the hand has 2 cards and OBO and these hands are forced to
                                '  stand.
                                If PlayerHands(index).HitHand(card) > 0 Then
                                    If PlayerHands(PlayerHands(index).HitHand(card)).SuitedBonusEVs Is Nothing Then
                                        'Use the Strat play values for non-suited hits
                                        PlayerHands(index).SuitedBonusEVs.SuitedHitEV(upcard, suit) += prob * (cstrat.HandEVs(PlayerHands(index).HitHand(card)).EVs.StratEV(upcard) + PlayerHands(PlayerHands(index).HitHand(card)).HandEVs.BonusEV(upcard))
                                        If (upcard = 1 Or upcard = 10) Then
                                            PlayerHands(index).SuitedBonusEVs.SuitedHitBJEV(upcard, suit) += prob * PlayerHands(PlayerHands(index).HitHand(card)).HandEVs.BJStandEV(upcard)
                                        End If
                                    Else
                                        'Break up hitting into suited hit and non-suited hit
                                        probsuit = CurrentShoe.Suits(card, suit) / CurrentShoe.Cards(card)
                                        'SuitedStratEV includes the SuitedBonusEV so it doesn't need to be added here
                                        PlayerHands(index).SuitedBonusEVs.SuitedHitEV(upcard, suit) += prob * (1 - probsuit) * (cstrat.HandEVs(PlayerHands(index).HitHand(card)).EVs.StratEV(upcard) + PlayerHands(PlayerHands(index).HitHand(card)).HandEVs.BonusEV(upcard))
                                        PlayerHands(index).SuitedBonusEVs.SuitedHitEV(upcard, suit) += prob * probsuit * PlayerHands(PlayerHands(index).HitHand(card)).SuitedBonusEVs.SuitedStratEV(upcard, suit)
                                        If (upcard = 1 Or upcard = 10) Then
                                            PlayerHands(index).SuitedBonusEVs.SuitedHitBJEV(upcard, suit) += prob * (1 - probsuit) * PlayerHands(PlayerHands(index).HitHand(card)).HandEVs.BJStandEV(upcard)
                                            PlayerHands(index).SuitedBonusEVs.SuitedHitBJEV(upcard, suit) += prob * probsuit * PlayerHands(PlayerHands(index).HitHand(card)).SuitedBonusEVs.SuitedStratBJEV(upcard, suit)
                                        End If
                                    End If
                                Else
                                    PlayerHands(index).SuitedBonusEVs.SuitedHitEV(upcard, suit) -= prob
                                    If (upcard = 1 Or upcard = 10) Then
                                        PlayerHands(index).SuitedBonusEVs.SuitedHitBJEV(upcard, suit) -= prob
                                    End If
                                End If
                            End If
                        Next card
                        CurrentShoe.UndealSuited(PlayerHands(index).Hand, suit)
                    Else
                        PlayerHands(index).SuitedBonusEVs.SuitedHitEV(upcard, suit) = cstrat.HandEVs(index).EVs.HitEV(upcard)
                        If (upcard = 1 Or upcard = 10) Then
                            PlayerHands(index).SuitedBonusEVs.SuitedHitBJEV(upcard, suit) = PlayerHands(index).HandEVs.BJStandEV(upcard)
                        End If
                    End If

                    'Copy Strat stand EV since these are the same for the non-bonus hands
                    PlayerHands(index).SuitedBonusEVs.SuitedStandEV(upcard, suit) = PlayerHands(index).HandEVs.StandEV(upcard)
                    PlayerHands(index).SuitedBonusEVs.SuitedStandBJEV(upcard, suit) = PlayerHands(index).HandEVs.BJStandEV(upcard)

                    'Then figure out if the strategy is to hit or follow the original CD strat
                    newEVs = ComputeSuitedStratHand(cstrat, index, upcard, suit, True)
                    PlayerHands(index).SuitedBonusEVs.SuitedStratEV(upcard, suit) = newEVs.SuitedStratEV(upcard, suit)
                    PlayerHands(index).SuitedBonusEVs.SuitedStratBJEV(upcard, suit) = newEVs.SuitedStratBJEV(upcard, suit)
                    PlayerHands(index).SuitedBonusEVs.SuitedStrat(upcard, suit) = newEVs.SuitedStrat(upcard, suit)
                Next suit
                CurrentShoe.Undeal(upcard)
            End If
        Next upcard

        'Finally, recursively go backwards through all the possible hands
        If PlayerHands(index).Hand.NumCards > 2 Then
            tempHand.Copy(PlayerHands(index).Hand)
            For card = 1 To 10
                If tempHand.Cards(card) > 0 Then
                    tempHand.Undeal(card)
                    newIndex = FindPlayerHand(tempHand)

                    If PlayerHands(newIndex).SuitedBonusEVs Is Nothing Then
                        ComputeSuitedBonusHitStratHand(cstrat, newIndex)
                    End If
                    tempHand.Deal(card)
                End If
            Next card
        End If

    End Sub

    Private Sub EnumSuitedHandList()
        Dim index As Integer
        Dim upcard As Integer
        Dim card As Integer
        Dim bonusHand As Integer
        Dim apply As Boolean
        Dim tempHand As New BJCAHandClass

        For bonusHand = 1 To NSuitedBonusHands
            'Work backwards through the suited hands to figure out the EV's
            If PlayerHands(SuitedBonusHands(bonusHand)).Hand.NumCards > 2 Then
                tempHand.Copy(PlayerHands(SuitedBonusHands(bonusHand)).Hand)
                For card = 1 To 10
                    If tempHand.Cards(card) > 0 Then
                        tempHand.Undeal(card)
                        index = FindPlayerHand(tempHand)

                        If PlayerHands(index).SuitedBonusEVs Is Nothing Then
                            ComputeSuitedBonusHitStratHand(Opt, index)
                        End If
                        tempHand.Deal(card)
                    End If
                Next card
            End If
        Next bonusHand
    End Sub

    Private Function ComputeSuitedStratHand(ByRef cstrat As BJCAStrategyClass, ByVal index As Integer, ByVal upcard As Integer, ByVal suit As Integer, ByVal includeHit As Boolean) As BJCASuitedBonusEVClass
        Dim newEVs As New BJCASuitedBonusEVClass

        'Begin with the current Strategy Play
        '   This takes care of doubles, surrender and splits
        newEVs.SuitedStratEV(upcard, suit) = cstrat.HandEVs(index).EVs.StratEV(upcard) + PlayerHands(index).HandEVs.BonusEV(upcard)
        If (upcard = 1 Or upcard = 10) Then
            newEVs.SuitedStratBJEV(upcard, suit) = PlayerHands(index).HandEVs.BJStandEV(upcard)
        End If
        newEVs.SuitedStrat(upcard, suit) = cstrat.HandEVs(index).EVs.Strat(upcard)

        'Now check for stand strategy
        If (PlayerHands(index).SuitedBonusEVs.SuitedStandEV(upcard, suit) + PlayerHands(index).SuitedBonusEVs.SuitedBonusEV(upcard, suit)) > newEVs.SuitedStratEV(upcard, suit) Then
            newEVs.SuitedStratEV(upcard, suit) = PlayerHands(index).SuitedBonusEVs.SuitedStandEV(upcard, suit) + PlayerHands(index).SuitedBonusEVs.SuitedBonusEV(upcard, suit)
            If (upcard = 1 Or upcard = 10) Then
                newEVs.SuitedStratBJEV(upcard, suit) = PlayerHands(index).SuitedBonusEVs.SuitedStandBJEV(upcard, suit)
            End If
            newEVs.SuitedStrat(upcard, suit) = BJCAGlobalsClass.Strat.S
        ElseIf PlayerHands(index).Hand.NumCards = 2 And ((upcard = 1 And CheckAce) Or (upcard = 10 And CheckTen)) Then
            '   The only time BJStand EV can cause a separate strategy from the rest of the
            '      hand is with 2-card dealer checking hands.  When this occurs only the
            '      BJEV is effected.
            '   BJ EV's are otherwise kept for analysis displays only.
            If PlayerHands(index).SuitedBonusEVs.SuitedStandBJEV(upcard, suit) > newEVs.SuitedStratBJEV(upcard, suit) Then
                newEVs.SuitedStratBJEV(upcard, suit) = PlayerHands(index).SuitedBonusEVs.SuitedStandBJEV(upcard, suit)
            End If
        End If

        'Finally check hitting
        If includeHit And (PlayerHands(index).SuitedBonusEVs.SuitedHitEV(upcard, suit) + PlayerHands(index).SuitedBonusEVs.SuitedBonusEV(upcard, suit)) > newEVs.SuitedStratEV(upcard, suit) Then
            newEVs.SuitedStratEV(upcard, suit) = PlayerHands(index).SuitedBonusEVs.SuitedHitEV(upcard, suit)
            If (upcard = 1 Or upcard = 10) Then
                newEVs.SuitedStratBJEV(upcard, suit) = PlayerHands(index).SuitedBonusEVs.SuitedHitBJEV(upcard, suit) + PlayerHands(index).SuitedBonusEVs.SuitedBonusEV(upcard, suit)
            End If
            newEVs.SuitedStrat(upcard, suit) = BJCAGlobalsClass.Strat.H
        End If

        Return newEVs

    End Function

    Private Sub ComputeSuitedHandNetEVs(ByRef cstrat As BJCAStrategyClass)
        Dim suitedIndex As Integer
        Dim index As Integer
        Dim suit As Integer
        Dim upcard As Integer
        Dim newEVs As New BJCASuitedBonusEVClass

        If BonusRuleOn Then
            For upcard = 1 To 10
                If UCAllowed(upcard) Then
                    For suitedIndex = 1 To NSuitedHands
                        index = SuitedHandsList(suitedIndex)
                        If PlayerHands(index).HandEVs.Prob(upcard) > 0 Then
                            PlayerHands(index).SuitedBonusEVs.SuitedNetEV(upcard) = (cstrat.HandEVs(index).EVs.StratEV(upcard) + PlayerHands(index).HandEVs.BonusEV(upcard)) * (PlayerHands(index).HandEVs.Prob(upcard) - PlayerHands(index).HandEVs.SumSuited(upcard))
                            PlayerHands(index).SuitedBonusEVs.SuitedNetBJEV(upcard) = PlayerHands(index).HandEVs.BJStandEV(upcard) * (PlayerHands(index).HandEVs.Prob(upcard) - PlayerHands(index).HandEVs.SumSuited(upcard))

                            PlayerHands(index).SuitedBonusEVs.SuitedNetEV(upcard) += (PlayerHands(index).SuitedBonusEVs.SuitedStratEV(upcard, 0) + PlayerHands(index).SuitedBonusEVs.SuitedBonusEV(upcard, 0)) * PlayerHands(index).HandEVs.ProbSuited(upcard, 0)
                            PlayerHands(index).SuitedBonusEVs.SuitedNetBJEV(upcard) += PlayerHands(index).SuitedBonusEVs.SuitedStratBJEV(upcard, 0) * PlayerHands(index).HandEVs.ProbSuited(upcard, 0)

                            PlayerHands(index).SuitedBonusEVs.SuitedNetEV(upcard) += (PlayerHands(index).SuitedBonusEVs.SuitedStratEV(upcard, 1) + PlayerHands(index).SuitedBonusEVs.SuitedBonusEV(upcard, 1)) * PlayerHands(index).HandEVs.ProbSuited(upcard, 1)
                            PlayerHands(index).SuitedBonusEVs.SuitedNetBJEV(upcard) += PlayerHands(index).SuitedBonusEVs.SuitedStratBJEV(upcard, 1) * PlayerHands(index).HandEVs.ProbSuited(upcard, 1)

                            PlayerHands(index).SuitedBonusEVs.SuitedNetEV(upcard) += (PlayerHands(index).SuitedBonusEVs.SuitedStratEV(upcard, 2) + PlayerHands(index).SuitedBonusEVs.SuitedBonusEV(upcard, 2)) * PlayerHands(index).HandEVs.ProbSuited(upcard, 2)
                            PlayerHands(index).SuitedBonusEVs.SuitedNetBJEV(upcard) += PlayerHands(index).SuitedBonusEVs.SuitedStratBJEV(upcard, 2) * PlayerHands(index).HandEVs.ProbSuited(upcard, 2)

                            PlayerHands(index).SuitedBonusEVs.SuitedNetEV(upcard) += (PlayerHands(index).SuitedBonusEVs.SuitedStratEV(upcard, 3) + PlayerHands(index).SuitedBonusEVs.SuitedBonusEV(upcard, 3)) * PlayerHands(index).HandEVs.ProbSuited(upcard, 3)
                            PlayerHands(index).SuitedBonusEVs.SuitedNetBJEV(upcard) += PlayerHands(index).SuitedBonusEVs.SuitedStratBJEV(upcard, 3) * PlayerHands(index).HandEVs.ProbSuited(upcard, 3)

                            PlayerHands(index).SuitedBonusEVs.SuitedNetEV(upcard) /= PlayerHands(index).HandEVs.Prob(upcard)
                            PlayerHands(index).SuitedBonusEVs.SuitedNetBJEV(upcard) /= PlayerHands(index).HandEVs.Prob(upcard)
                        End If
                    Next suitedIndex
                End If
            Next upcard
        End If
    End Sub

#End Region

#Region " Generic Strat Methods "

    Private Sub ComputeOptInitialStrat()
        Dim index As Integer
        Dim upcard As Integer
        Dim newEVs As New BJCAHandEVsClass

        For index = 1 To NumPHands
            If PlayerHands(index).Hand.NumCards > 1 Then
                For upcard = 1 To 10
                    If UCAllowed(upcard) Then
                        'Begin with Stand as the strategy
                        Opt.HandEVs(index).EVs.StratEV(upcard) = PlayerHands(index).HandEVs.StandEV(upcard)
                        Opt.HandEVs(index).EVs.StratPushEV(upcard) = PlayerHands(index).HandEVs.StandPushEV(upcard)
                        Opt.HandEVs(index).EVs.Strat(upcard) = BJCAGlobalsClass.Strat.S

                        'Now check for Double strategy
                        If PlayerHands(index).DPreallowed(upcard) Then
                            If PlayerHands(index).HandEVs.DEV(upcard) > Opt.HandEVs(index).EVs.StratEV(upcard) Then
                                Opt.HandEVs(index).EVs.StratEV(upcard) = PlayerHands(index).HandEVs.DEV(upcard)
                                Opt.HandEVs(index).EVs.StratPushEV(upcard) = PlayerHands(index).HandEVs.DPushEV(upcard)
                                Opt.HandEVs(index).EVs.Strat(upcard) = BJCAGlobalsClass.Strat.D
                            End If
                        End If

                        'Now finally check for Surrender strategy
                        If PlayerHands(index).RPreallowed(upcard) > 0 Then
                            If PlayerHands(index).HandEVs.SurrEV(upcard) > Opt.HandEVs(index).EVs.StratEV(upcard) Then
                                Opt.HandEVs(index).EVs.StratEV(upcard) = PlayerHands(index).HandEVs.SurrEV(upcard)
                                Opt.HandEVs(index).EVs.StratPushEV(upcard) = 0
                                Opt.HandEVs(index).EVs.Strat(upcard) = BJCAGlobalsClass.Strat.R
                            End If
                        End If
                    End If
                Next upcard
            End If
        Next index

    End Sub

#End Region

#Region " Hit Methods "

    Private Sub ComputeOptHit()
        Dim Total As Integer

        For Total = 21 To 11 Step -1
            ComputeOptHitTotal(Total, False)
        Next Total
        For Total = 21 To 12 Step -1
            ComputeOptHitTotal(Total, True)
        Next Total
        For Total = 10 To 4 Step -1
            ComputeOptHitTotal(Total, False)
        Next Total
    End Sub

    Private Sub ComputeOptHitTotal(ByVal Total As Integer, ByVal Soft As Boolean)
        Dim index As Integer
        Dim upcard As Integer
        Dim card As Integer
        Dim prob As Double

        index = PlayerHandTotal(Total, Soft + 1)
        Do While index
            CurrentShoe.Deal(PlayerHands(index).Hand)
            For upcard = 1 To 10
                If UCAllowed(upcard) And CardProb(upcard, 0) > 0 Then
                    CurrentShoe.Deal(upcard)
                    For card = 1 To 10
                        prob = CardProb(card, upcard)
                        If prob > 0 Then
                            If PlayerHands(index).HitHand(card) > 0 Then
                                Opt.HandEVs(index).EVs.HitEV(upcard) = Opt.HandEVs(index).EVs.HitEV(upcard) + prob * (Opt.HandEVs(PlayerHands(index).HitHand(card)).EVs.StratEV(upcard) + PlayerHands(PlayerHands(index).HitHand(card)).HandEVs.BonusEV(upcard))
                                Opt.HandEVs(index).EVs.HitPushEV(upcard) = Opt.HandEVs(index).EVs.HitPushEV(upcard) + prob * Opt.HandEVs(PlayerHands(index).HitHand(card)).EVs.StratPushEV(upcard)
                            Else
                                Opt.HandEVs(index).EVs.HitEV(upcard) = Opt.HandEVs(index).EVs.HitEV(upcard) - prob
                            End If
                        End If
                    Next card
                    CurrentShoe.Undeal(upcard)

                    'Update Max strategy for Optimal Play
                    If Opt.HandEVs(index).EVs.HitEV(upcard) > Opt.HandEVs(index).EVs.StratEV(upcard) Then
                        Opt.HandEVs(index).EVs.StratEV(upcard) = Opt.HandEVs(index).EVs.HitEV(upcard)
                        Opt.HandEVs(index).EVs.StratPushEV(upcard) = Opt.HandEVs(index).EVs.HitPushEV(upcard)
                        Opt.HandEVs(index).EVs.Strat(upcard) = BJCAGlobalsClass.Strat.H
                    End If
                    If Opt.HandEVs(index).EVs.Strat(upcard) = BJCAGlobalsClass.Strat.D And PlayerHands(index).HandEVs.StandEV(upcard) > Opt.HandEVs(index).EVs.HitEV(upcard) Then Opt.HandEVs(index).EVs.Strat(upcard) = BJCAGlobalsClass.Strat.DS
                    If Opt.HandEVs(index).EVs.Strat(upcard) = BJCAGlobalsClass.Strat.R And PlayerHands(index).HandEVs.StandEV(upcard) > Opt.HandEVs(index).EVs.HitEV(upcard) Then Opt.HandEVs(index).EVs.Strat(upcard) = BJCAGlobalsClass.Strat.RS
                End If
            Next upcard
            CurrentShoe.Undeal(PlayerHands(index).Hand)
            index = PlayerHands(index).NextHand
        Loop
    End Sub

#End Region

#Region " Split Methods "

#Region " Split Initializing Methods "

    Private Sub LinkSplitStand()
        Dim index As Integer
        Dim upcard As Integer
        Dim paircard As Integer
        Dim nextIndex As Integer
        Dim hands As Integer
        Dim bjadjprob As Double
        Dim newEVs As New BJCAHandEVsClass
        Dim newHand As New BJCAPlayerHandClass

        'Copy all the Stand values from the hands to the proper split hands
        For index = 1 To NumPHands
            If PlayerHands(index).Hand.NumCards > 1 Then
                For paircard = 1 To 10
                    If SplitAllowed(paircard) And CardProb(paircard, 0) > 0 And PlayerHands(index).PairIndex(paircard) > 0 Then
                        DealPCard(paircard)
                        If CardProb(paircard, 0) > 0 Then
                            DealPCard(paircard)
                            nextIndex = PlayerHands(index).HitHand(paircard)
                            For hands = 1 To NSplitCalcs
                                If nextIndex <> 0 Then
                                    'Need to add the new objects
                                    If PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)) Is Nothing Then
                                        PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)) = New BJCAHandEVsClass
                                        Opt.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)) = New BJCAHandStrategyEVsClass
                                        If TD.ComputeStrat Then TD.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)) = New BJCAHandStrategyEVsClass
                                        If TC.ComputeStrat Then TC.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)) = New BJCAHandStrategyEVsClass
                                        If Forced.ComputeStrat Then Forced.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)) = New BJCAHandStrategyEVsClass
                                    End If
                                    For upcard = 1 To 10
                                        If Not PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)).StandDone(upcard) And UCAllowed(upcard) And CardProb(upcard, 0) > 0 Then
                                            newEVs.Empty()

                                            'Player 21's lose to dealer BJ when not checking.
                                            'Dealer checks have already been accounted for in the dealer probs
                                            If ((upcard = 1 And Not CheckAce) Or (upcard = 10 And Not CheckTen)) Then
                                                If upcard = 1 And Not CheckAce Then
                                                    bjadjprob = BJHandNumerator(10, PlayerHands(nextIndex).Hand.Cards(10), 1) / BJHandDivisor(PlayerHands(nextIndex).Hand.NumCards + 1, 1)
                                                ElseIf upcard = 10 And Not CheckTen Then
                                                    bjadjprob = BJHandNumerator(1, PlayerHands(nextIndex).Hand.Cards(1), 1) / BJHandDivisor(PlayerHands(nextIndex).Hand.NumCards + 1, 1)
                                                Else
                                                    bjadjprob = 0
                                                End If
                                            Else
                                                bjadjprob = 0
                                            End If
                                            newEVs = GetHandEV(PlayerHands(index).Hand.Total, PlayerHands(nextIndex).HandEVs, upcard, bjadjprob)
                                            newHand.HandEVs.Empty()
                                            newHand.HandEVs.StandEV(upcard) = newEVs.StandEV(upcard)
                                            newHand.HandEVs.StandPushEV(upcard) = newEVs.StandPushEV(upcard)
                                            newHand.HandEVs.BJStandEV(upcard) = newEVs.BJStandEV(upcard)
                                            If BonusRuleOn Then
                                                newHand.Hand.Copy(PlayerHands(index).Hand)
                                                newEVs.Empty()
                                                newEVs = ApplyBonusRulesNonSuitedHand(newHand, upcard, bjadjprob, True, False)
                                                newHand.HandEVs.StandEV(upcard) = newEVs.StandEV(upcard)
                                                newHand.HandEVs.StandPushEV(upcard) = newEVs.StandPushEV(upcard)
                                                newHand.HandEVs.BonusEV(upcard) = newEVs.BonusEV(upcard)
                                                newHand.HandEVs.BJStandEV(upcard) = newEVs.BJStandEV(upcard)
                                            End If
                                            PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)).StandEV(upcard) = newHand.HandEVs.StandEV(upcard)
                                            PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)).StandPushEV(upcard) = newHand.HandEVs.StandPushEV(upcard)
                                            PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)).BonusEV(upcard) = newHand.HandEVs.BonusEV(upcard)
                                            PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)).BJStandEV(upcard) = newHand.HandEVs.BJStandEV(upcard)
                                            PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)).StandDone(upcard) = True
                                        End If
                                    Next upcard
                                    nextIndex = PlayerHands(nextIndex).HitHand(paircard)
                                End If
                            Next hands
                            UndealPCard(paircard)
                        End If
                        UndealPCard(paircard)
                    End If
                Next paircard
            End If
        Next index
    End Sub

    Private Sub InitializeSplits()
        If SPL > 0 Then
            ComputeSplitHandProbs()
            LinkSplitStand()
            ComputeSplitBlackjack()
            InitializeStrategySplitStates(Opt)
            If TD.ComputeStrat Then InitializeStrategySplitStates(TD)
            If TC.ComputeStrat Then InitializeStrategySplitStates(TC)
            If Forced.ComputeStrat Then InitializeStrategySplitStates(Forced)
        End If
    End Sub

    Private Sub ComputeSplitRoundProbs()
        Dim split As Integer
        Dim round As Integer
        Dim paircard As Integer
        Dim upcard As Integer
        Dim hands As Integer

        If SPL > 0 Then
            For paircard = 1 To 10
                For upcard = 1 To 10
                    If SplitIndex(paircard, upcard) > 0 Then
                        'SPL1
                        'xx	        2*EV(x)					            2*EV(x) 
                        SplitRoundProbs(paircard, upcard).RoundProbs(1, 1) = 1

                        'SPL2
                        'NN	        EV(N) + EV(N-N)			            2*EV(N-N) 
                        SplitRoundProbs(paircard, upcard).RoundProbs(2, 1) = (1 - SplitProbs(paircard, upcard).PxCards(BJCAGlobalsClass.Hands.Full, paircard))
                        SplitRoundProbs(paircard, upcard).RoundProbs(2, 1) *= (1 - SplitProbs(paircard, upcard).PxCards(BJCAGlobalsClass.Hands.N, paircard))
                        'Pxxx	    3*EV(x-P)				            3*EV(x-P) 
                        SplitRoundProbs(paircard, upcard).RoundProbs(2, 2) = SplitProbs(paircard, upcard).PxCards(BJCAGlobalsClass.Hands.Full, paircard)
                        'NPxx	    EV(N) + 2*EV(x-PN)		            EV(N-P) + 2*EV(x-PN) 
                        SplitRoundProbs(paircard, upcard).RoundProbs(2, 3) = (1 - SplitProbs(paircard, upcard).PxCards(BJCAGlobalsClass.Hands.Full, paircard))
                        SplitRoundProbs(paircard, upcard).RoundProbs(2, 3) *= SplitProbs(paircard, upcard).PxCards(BJCAGlobalsClass.Hands.N, paircard)

                        'SPL3
                        'NN	        EV(N) + EV(N-N)			            2*EV(N-N) 
                        SplitRoundProbs(paircard, upcard).RoundProbs(3, 1) = (1 - SplitProbs(paircard, upcard).PxCards(BJCAGlobalsClass.Hands.Full, paircard))
                        SplitRoundProbs(paircard, upcard).RoundProbs(3, 1) *= (1 - SplitProbs(paircard, upcard).PxCards(BJCAGlobalsClass.Hands.N, paircard))
                        'PNNN	    EV(N-P) + EV(N-PN) + EV(N-PNN)		3*EV(N-PNN) 
                        SplitRoundProbs(paircard, upcard).RoundProbs(3, 2) = SplitProbs(paircard, upcard).PxCards(BJCAGlobalsClass.Hands.Full, paircard)
                        SplitRoundProbs(paircard, upcard).RoundProbs(3, 2) *= (1 - SplitProbs(paircard, upcard).PxCards(BJCAGlobalsClass.Hands.P, paircard))
                        SplitRoundProbs(paircard, upcard).RoundProbs(3, 2) *= (1 - SplitProbs(paircard, upcard).PxCards(BJCAGlobalsClass.Hands.PN, paircard))
                        SplitRoundProbs(paircard, upcard).RoundProbs(3, 2) *= (1 - SplitProbs(paircard, upcard).PxCards(BJCAGlobalsClass.Hands.PNN, paircard))
                        'NPNN	    EV(N) + EV(N-PN) + EV(N-PNN)		3*EV(N-PNN) 
                        SplitRoundProbs(paircard, upcard).RoundProbs(3, 3) = (1 - SplitProbs(paircard, upcard).PxCards(BJCAGlobalsClass.Hands.Full, paircard))
                        SplitRoundProbs(paircard, upcard).RoundProbs(3, 3) *= SplitProbs(paircard, upcard).PxCards(BJCAGlobalsClass.Hands.N, paircard)
                        SplitRoundProbs(paircard, upcard).RoundProbs(3, 3) *= (1 - SplitProbs(paircard, upcard).PxCards(BJCAGlobalsClass.Hands.PN, paircard))
                        SplitRoundProbs(paircard, upcard).RoundProbs(3, 3) *= (1 - SplitProbs(paircard, upcard).PxCards(BJCAGlobalsClass.Hands.PNN, paircard))
                        'PPxxxx	    4*EV(x-PP)				            4*EV(x-PP) 
                        SplitRoundProbs(paircard, upcard).RoundProbs(3, 4) = SplitProbs(paircard, upcard).PxCards(BJCAGlobalsClass.Hands.Full, paircard)
                        SplitRoundProbs(paircard, upcard).RoundProbs(3, 4) *= SplitProbs(paircard, upcard).PxCards(BJCAGlobalsClass.Hands.P, paircard)
                        'PNPxxx	    EV(N-P) + 3*EV(x-PPN)			    EV(N-PP) + 3*EV(x-PPN) 
                        SplitRoundProbs(paircard, upcard).RoundProbs(3, 5) = SplitProbs(paircard, upcard).PxCards(BJCAGlobalsClass.Hands.Full, paircard)
                        SplitRoundProbs(paircard, upcard).RoundProbs(3, 5) *= (1 - SplitProbs(paircard, upcard).PxCards(BJCAGlobalsClass.Hands.P, paircard))
                        SplitRoundProbs(paircard, upcard).RoundProbs(3, 5) *= SplitProbs(paircard, upcard).PxCards(BJCAGlobalsClass.Hands.PN, paircard)
                        'NPPxxx	    EV(N) + 3*EV(x-PPN)			        EV(N-PP) + 3*EV(x-PPN) 
                        SplitRoundProbs(paircard, upcard).RoundProbs(3, 6) = (1 - SplitProbs(paircard, upcard).PxCards(BJCAGlobalsClass.Hands.Full, paircard))
                        SplitRoundProbs(paircard, upcard).RoundProbs(3, 6) *= SplitProbs(paircard, upcard).PxCards(BJCAGlobalsClass.Hands.N, paircard)
                        SplitRoundProbs(paircard, upcard).RoundProbs(3, 6) *= SplitProbs(paircard, upcard).PxCards(BJCAGlobalsClass.Hands.PN, paircard)
                        'PNNPxx	    EV(N-P) + EV(N-PN) + 2*EV(x-PPNN)	2*EV(N-PPN) + 2*EV(x-PPNN) 
                        SplitRoundProbs(paircard, upcard).RoundProbs(3, 7) = SplitProbs(paircard, upcard).PxCards(BJCAGlobalsClass.Hands.Full, paircard)
                        SplitRoundProbs(paircard, upcard).RoundProbs(3, 7) *= (1 - SplitProbs(paircard, upcard).PxCards(BJCAGlobalsClass.Hands.P, paircard))
                        SplitRoundProbs(paircard, upcard).RoundProbs(3, 7) *= (1 - SplitProbs(paircard, upcard).PxCards(BJCAGlobalsClass.Hands.PN, paircard))
                        SplitRoundProbs(paircard, upcard).RoundProbs(3, 7) *= SplitProbs(paircard, upcard).PxCards(BJCAGlobalsClass.Hands.PNN, paircard)
                        'NPNPxx	    EV(N) + EV(N-PN) + 2*EV(x-PPNN)		2*EV(N-PPN) + 2*EV(x-PPNN) 
                        SplitRoundProbs(paircard, upcard).RoundProbs(3, 8) = (1 - SplitProbs(paircard, upcard).PxCards(BJCAGlobalsClass.Hands.Full, paircard))
                        SplitRoundProbs(paircard, upcard).RoundProbs(3, 8) *= SplitProbs(paircard, upcard).PxCards(BJCAGlobalsClass.Hands.N, paircard)
                        SplitRoundProbs(paircard, upcard).RoundProbs(3, 8) *= (1 - SplitProbs(paircard, upcard).PxCards(BJCAGlobalsClass.Hands.PN, paircard))
                        SplitRoundProbs(paircard, upcard).RoundProbs(3, 8) *= SplitProbs(paircard, upcard).PxCards(BJCAGlobalsClass.Hands.PNN, paircard)

                        'Now add all the occurances of each hand to get it's net prob
                        For split = 1 To SPL
                            For hands = 0 To NSplitHands
                                SplitRoundProbs(paircard, upcard).NetHandProbs(split, hands, 0) = 0
                                SplitRoundProbs(paircard, upcard).NetHandProbs(split, hands, 1) = 0
                                For round = 1 To NRounds(split)
                                    SplitRoundProbs(paircard, upcard).NetHandProbs(split, hands, 0) += SplitRoundProbs(paircard, upcard).RoundProbs(split, round) * SplitRounds.Hands(split, round, hands, 0)
                                    SplitRoundProbs(paircard, upcard).NetHandProbs(split, hands, 1) += SplitRoundProbs(paircard, upcard).RoundProbs(split, round) * SplitRounds.Hands(split, round, hands, 1)
                                Next round
                            Next hands
                        Next split
                    End If
                Next upcard
            Next paircard
        End If
    End Sub

    Private Sub InitializeStrategySplitStates(ByRef cStrat As BJCAStrategyClass)
        Dim paircard As Integer
        Dim upcard As Integer

        For paircard = 1 To 10
            If SplitAllowed(paircard) Then
                For upcard = 1 To 10
                    If UCAllowed(upcard) Then
                        cStrat.SplitStateEVs(paircard, upcard) = New BJCASplitStateStrategyClass
                    End If
                Next upcard
            End If
        Next paircard
    End Sub

    Private Sub LinkSplitOptStrat()
        Dim index As Integer
        Dim upcard As Integer
        Dim paircard As Integer
        Dim hands As Integer

        'Copy the regular strategies over to the split strategies for CDZ
        For index = 1 To NumPHands
            If PlayerHands(index).Hand.NumCards > 1 Then
                For paircard = 1 To 10
                    If SplitAllowed(paircard) And PlayerHands(index).PairIndex(paircard) > 0 Then
                        For hands = 1 To NSplitCalcs
                            If PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)) Is Nothing Then
                                PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)) = New BJCAHandEVsClass
                                Opt.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)) = New BJCAHandStrategyEVsClass
                                If TD.ComputeStrat Then TD.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)) = New BJCAHandStrategyEVsClass
                                If TC.ComputeStrat Then TC.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)) = New BJCAHandStrategyEVsClass
                                If Forced.ComputeStrat Then Forced.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)) = New BJCAHandStrategyEVsClass
                            End If
                            For upcard = 1 To 10
                                If UCAllowed(upcard) Then
                                    Opt.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)).Strat(upcard) = GetPostSplitStrat(Opt.HandEVs(index).EVs.Strat(upcard), paircard, upcard, Opt, PlayerHands(index))
                                End If
                            Next upcard
                        Next hands
                    End If
                Next paircard
            End If
        Next index
    End Sub

#End Region

#Region " Opt Split Methods "

    Private Sub ComputeOptSplit()
        Dim paircard As Integer
        Dim upcard As Integer
        Dim index As Integer
        Dim tempStrat As Integer
        Dim tempEV As Double

        If SPL > 0 Then
            If CDP Or CDPN Then
                ComputeSplitStand()
                ComputeSplitDouble()
                ComputeSplitSurrender()
                ComputeSplitHit()
            Else    'CDZ
                LinkSplitOptStrat()
            End If

            For paircard = 1 To 10
                If SplitAllowed(paircard) Then
                    For upcard = 1 To 10
                        If UCAllowed(upcard) Then
                            index = SplitIndex(paircard, upcard)
                            If index > 0 Then
                                ComputeSplit(Opt, paircard, upcard)

                                'Update Max strategy for Optimal Play
                                If Opt.HandEVs(index).SplitEV(upcard) > Opt.HandEVs(index).EVs.StratEV(upcard) Then
                                    Opt.HandEVs(index).EVs.StratEV(upcard) = Opt.HandEVs(index).SplitEV(upcard)

                                    'Strategy after split needs to be determined by the x Hands
                                    'Opt.HandEVs(index).EVs.Strat(upcard) = ComputePostsplitPairMaxEVStrat(Opt, paircard, upcard)

                                    If paircard = 1 Then
                                        'Need special check here for HSA, DSA, RSA
                                        tempStrat = BJCAGlobalsClass.Strat.S
                                        tempEV = PlayerHands(index).HandEVs.StandEV(upcard)
                                        If HSA And Opt.HandEVs(index).EVs.HitEV(upcard) > tempEV Then
                                            tempStrat = BJCAGlobalsClass.Strat.H
                                            tempEV = Opt.HandEVs(index).EVs.HitEV(upcard)
                                        End If
                                        If DSA And PlayerHands(index).HandEVs.DEV(upcard) > tempEV Then
                                            tempStrat = BJCAGlobalsClass.Strat.D
                                            tempEV = PlayerHands(index).HandEVs.DEV(upcard)
                                        End If
                                        If SSA And PlayerHands(index).HandEVs.SurrEV(upcard) > tempEV Then
                                            tempStrat = BJCAGlobalsClass.Strat.R
                                            tempEV = PlayerHands(index).HandEVs.SurrEV(upcard)
                                        End If
                                        If tempStrat = BJCAGlobalsClass.Strat.S And Not (HSA Or DSA Or SSA) Then
                                            tempStrat = BJCAGlobalsClass.Strat.None
                                        End If
                                        Select Case tempStrat
                                            Case BJCAGlobalsClass.Strat.None
                                                Opt.HandEVs(index).EVs.Strat(upcard) = BJCAGlobalsClass.Strat.P
                                            Case BJCAGlobalsClass.Strat.S
                                                Opt.HandEVs(index).EVs.Strat(upcard) = BJCAGlobalsClass.Strat.PS
                                            Case BJCAGlobalsClass.Strat.H
                                                Opt.HandEVs(index).EVs.Strat(upcard) = BJCAGlobalsClass.Strat.PH
                                            Case BJCAGlobalsClass.Strat.D
                                                Opt.HandEVs(index).EVs.Strat(upcard) = BJCAGlobalsClass.Strat.PD
                                            Case BJCAGlobalsClass.Strat.R
                                                Opt.HandEVs(index).EVs.Strat(upcard) = BJCAGlobalsClass.Strat.PR
                                        End Select
                                    Else
                                        Select Case Opt.HandEVs(index).EVs.Strat(upcard)
                                            Case BJCAGlobalsClass.Strat.S
                                                Opt.HandEVs(index).EVs.Strat(upcard) = BJCAGlobalsClass.Strat.PS
                                            Case BJCAGlobalsClass.Strat.D
                                                If DAS Then
                                                    Opt.HandEVs(index).EVs.Strat(upcard) = BJCAGlobalsClass.Strat.PD
                                                Else
                                                    Opt.HandEVs(index).EVs.Strat(upcard) = BJCAGlobalsClass.Strat.PH
                                                End If
                                            Case BJCAGlobalsClass.Strat.DS
                                                If DAS Then
                                                    Opt.HandEVs(index).EVs.Strat(upcard) = BJCAGlobalsClass.Strat.PD
                                                Else
                                                    Opt.HandEVs(index).EVs.Strat(upcard) = BJCAGlobalsClass.Strat.PS
                                                End If
                                            Case BJCAGlobalsClass.Strat.H
                                                Opt.HandEVs(index).EVs.Strat(upcard) = BJCAGlobalsClass.Strat.PH
                                            Case BJCAGlobalsClass.Strat.R
                                                If SAS Then
                                                    Opt.HandEVs(index).EVs.Strat(upcard) = BJCAGlobalsClass.Strat.PR
                                                Else
                                                    Opt.HandEVs(index).EVs.Strat(upcard) = BJCAGlobalsClass.Strat.PH
                                                End If
                                            Case BJCAGlobalsClass.Strat.RS
                                                If SAS Then
                                                    Opt.HandEVs(index).EVs.Strat(upcard) = BJCAGlobalsClass.Strat.PR
                                                Else
                                                    Opt.HandEVs(index).EVs.Strat(upcard) = BJCAGlobalsClass.Strat.PS
                                                End If
                                        End Select
                                    End If
                                End If
                            End If
                        End If
                    Next upcard
                End If
            Next paircard
        End If
    End Sub

    Private Sub ComputeSplitStand()
        Dim index As Integer
        Dim upcard As Integer
        Dim paircard As Integer
        Dim hands As Integer
        Dim temphand As New BJCAHandClass
        Dim pcprob(11) As Double    'Prob of paircard
        Dim newEVs As New BJCAHandEVsClass
        Dim newBonusEVs As New BJCAHandEVsClass
        Dim newHand As New BJCAPlayerHandClass
        Dim bjadjprob As Double

        For paircard = 1 To 10
            If SplitAllowed(paircard) Then
                For index = 1 To NumPHands
                    'Only non-linked hands that have at least one paircard in them need to be calculated for a given paircard
                    If ((PlayerHands(index).Hand.NumCards > 1 And PlayerHands(index).Hand.Cards(paircard) > 0) And Not (paircard = 1 And Not HSA And Not DSA And (PlayerHands(index).Hand.NumCards > 2))) Then
                        temphand.Copy(PlayerHands(index).Hand)
                        For hands = 1 To NSplitCalcs
                            If (PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)) Is Nothing) And ((PlayerHands(index).Hand.Cards(paircard) + NPxHands(SplitCalcHands(hands)).NP + 1 - OriginalShoe.Cards(paircard)) < 1) Then
                                PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)) = New BJCAHandEVsClass
                                If Opt.ComputeStrat Then Opt.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)) = New BJCAHandStrategyEVsClass
                                If TD.ComputeStrat Then TD.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)) = New BJCAHandStrategyEVsClass
                                If TC.ComputeStrat Then TC.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)) = New BJCAHandStrategyEVsClass
                                If Forced.ComputeStrat Then Forced.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)) = New BJCAHandStrategyEVsClass

                                temphand.Cards(paircard) += (NPxHands(SplitCalcHands(hands)).NP + 1)
                                temphand.NumCards += (NPxHands(SplitCalcHands(hands)).NP + 1)

                                newEVs = ComputeStandHand(temphand, 1, 10, False)
                                newHand.HandEVs.Empty()
                                newHand.Hand.Copy(PlayerHands(index).Hand)

                                For upcard = 1 To 10
                                    If UCAllowed(upcard) Then
                                        PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)).DealerProbs(upcard, 0) = newEVs.DealerProbs(upcard, 0)
                                        PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)).DealerProbs(upcard, 1) = newEVs.DealerProbs(upcard, 1)
                                        PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)).DealerProbs(upcard, 2) = newEVs.DealerProbs(upcard, 2)
                                        PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)).DealerProbs(upcard, 3) = newEVs.DealerProbs(upcard, 3)
                                        PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)).DealerProbs(upcard, 4) = newEVs.DealerProbs(upcard, 4)
                                        newHand.HandEVs.StandEV(upcard) = newEVs.StandEV(upcard)
                                        newHand.HandEVs.StandPushEV(upcard) = newEVs.StandPushEV(upcard)
                                        newHand.HandEVs.BJStandEV(upcard) = newEVs.BJStandEV(upcard)

                                        If BonusRuleOn Then
                                            If ((upcard = 1 And Not CheckAce) Or (upcard = 10 And Not CheckTen)) Then
                                                If upcard = 1 And Not CheckAce Then
                                                    bjadjprob = BJHandNumerator(10, temphand.Cards(10), 1) / BJHandDivisor(temphand.NumCards + 1, 1)
                                                ElseIf upcard = 10 And Not CheckTen Then
                                                    bjadjprob = BJHandNumerator(1, temphand.Cards(1), 1) / BJHandDivisor(temphand.NumCards + 1, 1)
                                                Else
                                                    bjadjprob = 0
                                                End If
                                            Else
                                                bjadjprob = 0
                                            End If
                                            newBonusEVs.Empty()
                                            newBonusEVs = ApplyBonusRulesNonSuitedHand(newHand, upcard, bjadjprob, True, False)
                                            newHand.HandEVs.StandEV(upcard) = newBonusEVs.StandEV(upcard)
                                            newHand.HandEVs.StandPushEV(upcard) = newBonusEVs.StandPushEV(upcard)
                                            newHand.HandEVs.BonusEV(upcard) = newBonusEVs.BonusEV(upcard)
                                        End If


                                        PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)).StandEV(upcard) = newHand.HandEVs.StandEV(upcard)
                                        PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)).StandPushEV(upcard) = newHand.HandEVs.StandPushEV(upcard)
                                        PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)).BonusEV(upcard) = newHand.HandEVs.BonusEV(upcard)
                                        PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)).StandDone(upcard) = True

                                        'Max strategy so far is Stand
                                        Opt.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)).Strat(upcard) = BJCAGlobalsClass.Strat.S
                                        Opt.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)).StratEV(upcard) = PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)).StandEV(upcard)
                                        Opt.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)).StratPushEV(upcard) = PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)).StandPushEV(upcard)
                                    End If
                                Next upcard

                                temphand.Cards(paircard) -= (NPxHands(SplitCalcHands(hands)).NP + 1)
                                temphand.NumCards -= (NPxHands(SplitCalcHands(hands)).NP + 1)
                            ElseIf Not ((PlayerHands(index).Hand.Cards(paircard) + NPxHands(SplitCalcHands(hands)).NP + 1 - OriginalShoe.Cards(paircard)) < 1) Then
                                'Hand is not possible
                                PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)) = New BJCAHandEVsClass
                                If Opt.ComputeStrat Then Opt.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)) = New BJCAHandStrategyEVsClass
                                If TD.ComputeStrat Then TD.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)) = New BJCAHandStrategyEVsClass
                                If TC.ComputeStrat Then TC.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)) = New BJCAHandStrategyEVsClass
                                If Forced.ComputeStrat Then Forced.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)) = New BJCAHandStrategyEVsClass
                                For upcard = 1 To 10
                                    PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)).StandDone(upcard) = True
                                Next upcard
                            Else
                                For upcard = 1 To 10
                                    'Max strategy so far is Stand
                                    PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)).StandDone(upcard) = True
                                    Opt.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)).Strat(upcard) = BJCAGlobalsClass.Strat.S
                                    Opt.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)).StratEV(upcard) = PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)).StandEV(upcard)
                                    Opt.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)).StratPushEV(upcard) = PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)).StandPushEV(upcard)
                                Next upcard
                            End If
                        Next hands

                        If CDPN Then
                            CurrentShoe.Deal(paircard)
                            'Now the remaining shoe states need to be filled in
                            For hands = 0 To NSplitHands
                                If NPxHands(hands).NN > 0 Then
                                    For upcard = 1 To 10
                                        If UCAllowed(upcard) And CurrentShoe.Cards(upcard) > 0 Then
                                            CurrentShoe.Deal(upcard)
                                            If PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), hands) Is Nothing Then
                                                'Will only fill in empty hands so BJ isn't overwritten
                                                PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), hands) = New BJCAHandEVsClass
                                                If Opt.ComputeStrat Then Opt.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), hands) = New BJCAHandStrategyEVsClass
                                                If TD.ComputeStrat Then TD.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), hands) = New BJCAHandStrategyEVsClass
                                                If TC.ComputeStrat Then TC.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), hands) = New BJCAHandStrategyEVsClass
                                                If Forced.ComputeStrat Then Forced.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), hands) = New BJCAHandStrategyEVsClass
                                            End If

                                            If ((NPxHands(hands).NP + PlayerHands(index).Hand.Cards(paircard)) > CurrentShoe.Cards(paircard)) Then
                                                PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), hands).StandEV(upcard) = 0
                                                Opt.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), hands).Strat(upcard) = 0
                                                Opt.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), hands).StratEV(upcard) = 0
                                                Opt.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), hands).StratPushEV(upcard) = 0
                                                PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), hands).StandDone(upcard) = True
                                            Else
                                                'First need to figure out post split paircard probs of UCH and CH hands
                                                If NPxHands(hands).NN = 1 Then
                                                    If upcard = 1 And CheckAce Then
                                                        If paircard = 10 Then
                                                            pcprob(hands) = (CurrentShoe.Cards(paircard) - PlayerHands(index).Hand.Cards(paircard) - NPxHands(hands).NP) / (CurrentShoe.CardsLeft - PlayerHands(index).Hand.NumCards - NPxHands(hands).NN - NPxHands(hands).NP)
                                                        Else
                                                            pcprob(hands) = ((CurrentShoe.Cards(paircard) - PlayerHands(index).Hand.Cards(paircard) - NPxHands(hands).NP) - (CurrentShoe.Cards(paircard) - PlayerHands(index).Hand.Cards(paircard) - NPxHands(hands).NP) / (CurrentShoe.CardsLeft - PlayerHands(index).Hand.NumCards - NPxHands(hands).NN - NPxHands(hands).NP + 1 - (CurrentShoe.Cards(10) - PlayerHands(index).Hand.Cards(10)))) / (CurrentShoe.CardsLeft - PlayerHands(index).Hand.NumCards - NPxHands(hands).NN - NPxHands(hands).NP)
                                                        End If
                                                    ElseIf upcard = 10 And CheckTen Then
                                                        If paircard = 1 Then
                                                            pcprob(hands) = (CurrentShoe.Cards(paircard) - PlayerHands(index).Hand.Cards(paircard) - NPxHands(hands).NP) / (CurrentShoe.CardsLeft - PlayerHands(index).Hand.NumCards - NPxHands(hands).NN - NPxHands(hands).NP)
                                                        Else
                                                            pcprob(hands) = ((CurrentShoe.Cards(paircard) - PlayerHands(index).Hand.Cards(paircard) - NPxHands(hands).NP) - (CurrentShoe.Cards(paircard) - PlayerHands(index).Hand.Cards(paircard) - NPxHands(hands).NP) / (CurrentShoe.CardsLeft - PlayerHands(index).Hand.NumCards - NPxHands(hands).NN - NPxHands(hands).NP + 1 - (CurrentShoe.Cards(1) - PlayerHands(index).Hand.Cards(1)))) / (CurrentShoe.CardsLeft - PlayerHands(index).Hand.NumCards - NPxHands(hands).NN - NPxHands(hands).NP)
                                                        End If
                                                    Else
                                                        pcprob(hands) = (CurrentShoe.Cards(paircard) - PlayerHands(index).Hand.Cards(paircard) - NPxHands(hands).NP) / (CurrentShoe.CardsLeft - PlayerHands(index).Hand.NumCards - NPxHands(hands).NN - NPxHands(hands).NP + 1)
                                                    End If
                                                Else 'NPxHands(hands).NN = 2
                                                    If (upcard = 1 And CheckAce) Or (upcard = 10 And CheckTen) Then
                                                        pcprob(hands) = ConditionalValue(pcprob(NPxHands(hands).UCH), pcprob(NPxHands(hands).CH), pcprob(NPxHands(hands).UCH))
                                                    Else
                                                        pcprob(hands) = (CurrentShoe.Cards(paircard) - PlayerHands(index).Hand.Cards(paircard) - NPxHands(hands).NP) / (CurrentShoe.CardsLeft - PlayerHands(index).Hand.NumCards - NPxHands(hands).NN - NPxHands(hands).NP + 1)
                                                    End If
                                                End If
                                                'Then use these paircard probs for the strategy
                                                PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), hands).StandEV(upcard) = ConditionalValue(PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), NPxHands(hands).UCH).StandEV(upcard), PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), NPxHands(hands).CH).StandEV(upcard), pcprob(hands))
                                                PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), hands).StandPushEV(upcard) = ConditionalValue(PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), NPxHands(hands).UCH).StandPushEV(upcard), PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), NPxHands(hands).CH).StandPushEV(upcard), pcprob(hands))
                                                PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), hands).BonusEV(upcard) = ConditionalValue(PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), NPxHands(hands).UCH).BonusEV(upcard), PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), NPxHands(hands).CH).BonusEV(upcard), pcprob(hands))
                                            End If
                                            CurrentShoe.Undeal(upcard)

                                            'Max strategy so far is Stand
                                            PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), hands).StandDone(upcard) = True
                                            Opt.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), hands).Strat(upcard) = BJCAGlobalsClass.Strat.S
                                            Opt.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), hands).StratEV(upcard) = PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), hands).StandEV(upcard)
                                            Opt.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), hands).StratPushEV(upcard) = PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), hands).StandPushEV(upcard)
                                        End If
                                    Next upcard
                                End If
                            Next hands
                            CurrentShoe.Undeal(paircard)
                        End If
                    End If
                Next index
            End If
        Next paircard
    End Sub

    Private Sub ComputeSplitBlackjack()
        Dim index As Integer
        Dim upcard As Integer
        Dim paircard As Integer
        Dim hands As Integer
        Dim temphand As New BJCAHandClass
        Dim pcprob(11) As Double    'Prob of paircard
        Dim newEVs As New BJCAHandEVsClass

        index = BJIndex

        If index > 0 Then
            For paircard = 1 To 10
                If (SplitAllowed(paircard) And ((paircard = 1 And BJSplitAces) Or (paircard = 10 And BJSplitTens))) Then
                    temphand.Copy(PlayerHands(index).Hand)
                    For hands = 1 To NSplitCalcs
                        If (PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)) Is Nothing) Then
                            PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)) = New BJCAHandEVsClass
                            Opt.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)) = New BJCAHandStrategyEVsClass
                            If TD.ComputeStrat Then TD.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)) = New BJCAHandStrategyEVsClass
                            If TC.ComputeStrat Then TC.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)) = New BJCAHandStrategyEVsClass
                            If Forced.ComputeStrat Then Forced.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)) = New BJCAHandStrategyEVsClass
                        End If

                        If ((PlayerHands(index).Hand.Cards(paircard) + NPxHands(SplitCalcHands(hands)).NP + 1 - OriginalShoe.Cards(paircard)) < 1) Then
                            temphand.Cards(paircard) += (NPxHands(SplitCalcHands(hands)).NP + 1)
                            temphand.NumCards += (NPxHands(SplitCalcHands(hands)).NP + 1)

                            For upcard = 1 To 10
                                If UCAllowed(upcard) Then
                                    newEVs = ComputeBlackjackHand(temphand, upcard, True)

                                    PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)).StandEV(upcard) = newEVs.StandEV(upcard)
                                    PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)).StandPushEV(upcard) = newEVs.StandPushEV(upcard)
                                    PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)).StandDone(upcard) = True

                                    'Max strategy so far is Stand
                                    Opt.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)).Strat(upcard) = BJCAGlobalsClass.Strat.S
                                    Opt.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)).StratEV(upcard) = PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)).StandEV(upcard)
                                    Opt.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)).StratPushEV(upcard) = PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)).StandPushEV(upcard)
                                End If
                            Next upcard

                            temphand.Cards(paircard) -= (NPxHands(SplitCalcHands(hands)).NP + 1)
                            temphand.NumCards -= (NPxHands(SplitCalcHands(hands)).NP + 1)
                        End If
                    Next hands

                    If CDPN Then
                        CurrentShoe.Deal(paircard)
                        'Now the remaining shoe states need to be filled in
                        For hands = 0 To NSplitHands
                            If NPxHands(hands).NN > 0 Then
                                For upcard = 1 To 10
                                    If UCAllowed(upcard) And CurrentShoe.Cards(upcard) > 0 Then
                                        CurrentShoe.Deal(upcard)
                                        If PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), hands) Is Nothing Then
                                            PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), hands) = New BJCAHandEVsClass
                                            Opt.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), hands) = New BJCAHandStrategyEVsClass
                                            If TD.ComputeStrat Then TD.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), hands) = New BJCAHandStrategyEVsClass
                                            If TC.ComputeStrat Then TC.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), hands) = New BJCAHandStrategyEVsClass
                                            If Forced.ComputeStrat Then Forced.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), hands) = New BJCAHandStrategyEVsClass
                                        End If

                                        If ((NPxHands(hands).NP + PlayerHands(index).Hand.Cards(paircard)) > CurrentShoe.Cards(paircard)) Then
                                            PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), hands).StandEV(upcard) = 0
                                            Opt.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), hands).Strat(upcard) = 0
                                            Opt.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), hands).StratEV(upcard) = 0
                                            Opt.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), hands).StratPushEV(upcard) = 0
                                        Else
                                            'First need to figure out post split paircard probs of UCH and CH hands
                                            If NPxHands(hands).NN = 1 Then
                                                If upcard = 1 And CheckAce Then
                                                    If paircard = 10 Then
                                                        pcprob(hands) = (CurrentShoe.Cards(paircard) - PlayerHands(index).Hand.Cards(paircard) - NPxHands(hands).NP) / (CurrentShoe.CardsLeft - PlayerHands(index).Hand.NumCards - NPxHands(hands).NN - NPxHands(hands).NP)
                                                    Else
                                                        pcprob(hands) = ((CurrentShoe.Cards(paircard) - PlayerHands(index).Hand.Cards(paircard) - NPxHands(hands).NP) - (CurrentShoe.Cards(paircard) - PlayerHands(index).Hand.Cards(paircard) - NPxHands(hands).NP) / (CurrentShoe.CardsLeft - PlayerHands(index).Hand.NumCards - NPxHands(hands).NN - NPxHands(hands).NP + 1 - (CurrentShoe.Cards(10) - PlayerHands(index).Hand.Cards(10)))) / (CurrentShoe.CardsLeft - PlayerHands(index).Hand.NumCards - NPxHands(hands).NN - NPxHands(hands).NP)
                                                    End If
                                                ElseIf upcard = 10 And CheckTen Then
                                                    If paircard = 1 Then
                                                        pcprob(hands) = (CurrentShoe.Cards(paircard) - PlayerHands(index).Hand.Cards(paircard) - NPxHands(hands).NP) / (CurrentShoe.CardsLeft - PlayerHands(index).Hand.NumCards - NPxHands(hands).NN - NPxHands(hands).NP)
                                                    Else
                                                        pcprob(hands) = ((CurrentShoe.Cards(paircard) - PlayerHands(index).Hand.Cards(paircard) - NPxHands(hands).NP) - (CurrentShoe.Cards(paircard) - PlayerHands(index).Hand.Cards(paircard) - NPxHands(hands).NP) / (CurrentShoe.CardsLeft - PlayerHands(index).Hand.NumCards - NPxHands(hands).NN - NPxHands(hands).NP + 1 - (CurrentShoe.Cards(1) - PlayerHands(index).Hand.Cards(1)))) / (CurrentShoe.CardsLeft - PlayerHands(index).Hand.NumCards - NPxHands(hands).NN - NPxHands(hands).NP)
                                                    End If
                                                Else
                                                    pcprob(hands) = (CurrentShoe.Cards(paircard) - PlayerHands(index).Hand.Cards(paircard) - NPxHands(hands).NP) / (CurrentShoe.CardsLeft - PlayerHands(index).Hand.NumCards - NPxHands(hands).NN - NPxHands(hands).NP + 1)
                                                End If
                                            Else 'NPxHands(hands).NN = 2
                                                If (upcard = 1 And CheckAce) Or (upcard = 10 And CheckTen) Then
                                                    pcprob(hands) = ConditionalValue(pcprob(NPxHands(hands).UCH), pcprob(NPxHands(hands).CH), pcprob(NPxHands(hands).UCH))
                                                Else
                                                    pcprob(hands) = (CurrentShoe.Cards(paircard) - PlayerHands(index).Hand.Cards(paircard) - NPxHands(hands).NP) / (CurrentShoe.CardsLeft - PlayerHands(index).Hand.NumCards - NPxHands(hands).NN - NPxHands(hands).NP + 1)
                                                End If
                                            End If
                                            'Then use these paircard probs for the strategy
                                            PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), hands).StandEV(upcard) = ConditionalValue(PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), NPxHands(hands).UCH).StandEV(upcard), PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), NPxHands(hands).CH).StandEV(upcard), pcprob(hands))
                                            PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), hands).StandPushEV(upcard) = ConditionalValue(PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), NPxHands(hands).UCH).StandPushEV(upcard), PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), NPxHands(hands).CH).StandPushEV(upcard), pcprob(hands))
                                        End If
                                        CurrentShoe.Undeal(upcard)

                                        'Max strategy so far is Stand
                                        PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), hands).StandDone(upcard) = True
                                        Opt.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), hands).Strat(upcard) = BJCAGlobalsClass.Strat.S
                                        Opt.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), hands).StratEV(upcard) = PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), hands).StandEV(upcard)
                                        Opt.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), hands).StratPushEV(upcard) = PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), hands).StandPushEV(upcard)
                                    End If
                                Next upcard
                            End If
                        Next hands
                        CurrentShoe.Undeal(paircard)
                    End If
                End If
            Next paircard
        End If
    End Sub

    Private Sub ComputeSplitDouble()
        Dim index As Integer
        Dim upcard As Integer
        Dim card As Integer
        Dim prob As Double
        Dim paircard As Integer
        Dim hands As Integer
        Dim tempHand As New BJCAHandClass
        Dim tempHand2 As New BJCAHandClass
        Dim newEVs As New BJCAHandEVsClass
        Dim pcprob(11) As Double

        If DAS Then
            For paircard = 1 To 10
                If SplitAllowed(paircard) And Not (paircard = 1 And Not DSA) Then
                    For index = 1 To NumPHands
                        If PlayerHands(index).Hand.Cards(paircard) > 0 Then
                            tempHand.Copy(PlayerHands(index).Hand)
                            For upcard = 1 To 10
                                If UCAllowed(upcard) And PlayerHands(index).DPostallowed(upcard) And CurrentShoe.Cards(upcard) > 0 Then
                                    CurrentShoe.Deal(upcard, 1)
                                    For hands = 0 To NSplitHands
                                        If (PlayerHands(index).Hand.Cards(paircard) + NPxHands(hands).NP + 1 <= CurrentShoe.Cards(paircard)) Then
                                            If NPxHands(hands).NN = 0 Then
                                                tempHand.Cards(paircard) += NPxHands(hands).NP + 1
                                                tempHand.NumCards += NPxHands(hands).NP + 1
                                                If HandPossible(tempHand) Then
                                                    CurrentShoe.Deal(tempHand)
                                                    For card = 1 To 10
                                                        If CurrentShoe.Cards(card) Then
                                                            prob = CardProb(card, upcard)
                                                            If prob > 0 Then
                                                                If PlayerHands(index).HitHand(card) And PlayerHands(index).Hand.Soft And (PlayerHands(PlayerHands(index).HitHand(card)).Hand.Cards(1) = 1) And (DSoftAllHard Or (DSoft19Hard And PlayerHands(index).Hand.Total = 19)) Then
                                                                    CurrentShoe.Undeal(upcard, 1)
                                                                    CurrentShoe.Deal(card)
                                                                    CurrentShoe.Hand.Total = PlayerHands(index).Hand.Total - 10 + card
                                                                    newEVs = ComputeStandHand(CurrentShoe.Hand, upcard, upcard, False)
                                                                    PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), hands).DEV(upcard) += 2 * prob * newEVs.StandEV(upcard)
                                                                    PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), hands).DPushEV(upcard) += 2 * prob * newEVs.StandPushEV(upcard)
                                                                    CurrentShoe.Undeal(card)
                                                                    CurrentShoe.Deal(upcard, 1)
                                                                ElseIf PlayerHands(index).HitHand(card) Then
                                                                    PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), hands).DEV(upcard) += 2 * prob * PlayerHands(PlayerHands(index).HitHand(card)).SplitEVs(PlayerHands(PlayerHands(index).HitHand(card)).PairIndex(paircard), hands).StandEV(upcard)
                                                                    PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), hands).DPushEV(upcard) += 2 * prob * PlayerHands(PlayerHands(index).HitHand(card)).SplitEVs(PlayerHands(PlayerHands(index).HitHand(card)).PairIndex(paircard), hands).StandPushEV(upcard)
                                                                Else
                                                                    PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), hands).DEV(upcard) -= 2 * prob
                                                                End If
                                                            End If
                                                        End If
                                                    Next card

                                                    CurrentShoe.Undeal(tempHand)

                                                    'Update Max strategy
                                                    If PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), hands).DEV(upcard) > Opt.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), hands).StratEV(upcard) Then
                                                        Opt.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), hands).Strat(upcard) = BJCAGlobalsClass.Strat.D
                                                        Opt.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), hands).StratEV(upcard) = PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), hands).DEV(upcard)
                                                        Opt.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), hands).StratPushEV(upcard) = PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), hands).DPushEV(upcard)
                                                    End If
                                                End If

                                                tempHand.Cards(paircard) -= (NPxHands(hands).NP + 1)
                                                tempHand.NumCards -= (NPxHands(hands).NP + 1)
                                            ElseIf CDPN Then
                                                CurrentShoe.Deal(paircard, 1)
                                                If ((NPxHands(hands).NP + PlayerHands(index).Hand.Cards(paircard)) > CurrentShoe.Cards(paircard)) Then
                                                    PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), hands).DEV(upcard) = 0
                                                    PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), hands).DPushEV(upcard) = 0
                                                    pcprob(hands) = 0
                                                Else
                                                    If NPxHands(hands).NN = 1 Then
                                                        If upcard = 1 And CheckAce Then
                                                            If paircard = 10 Then
                                                                pcprob(hands) = (CurrentShoe.Cards(paircard) - PlayerHands(index).Hand.Cards(paircard) - NPxHands(hands).NP) / (CurrentShoe.CardsLeft - PlayerHands(index).Hand.NumCards - NPxHands(hands).NN - NPxHands(hands).NP)
                                                            Else
                                                                pcprob(hands) = ((CurrentShoe.Cards(paircard) - PlayerHands(index).Hand.Cards(paircard) - NPxHands(hands).NP) - (CurrentShoe.Cards(paircard) - PlayerHands(index).Hand.Cards(paircard) - NPxHands(hands).NP) / (CurrentShoe.CardsLeft - PlayerHands(index).Hand.NumCards - NPxHands(hands).NN - NPxHands(hands).NP + 1 - (CurrentShoe.Cards(10) - PlayerHands(index).Hand.Cards(10)))) / (CurrentShoe.CardsLeft - PlayerHands(index).Hand.NumCards - NPxHands(hands).NN - NPxHands(hands).NP)
                                                            End If
                                                        ElseIf upcard = 10 And CheckTen Then
                                                            If paircard = 1 Then
                                                                pcprob(hands) = (CurrentShoe.Cards(paircard) - PlayerHands(index).Hand.Cards(paircard) - NPxHands(hands).NP) / (CurrentShoe.CardsLeft - PlayerHands(index).Hand.NumCards - NPxHands(hands).NN - NPxHands(hands).NP)
                                                            Else
                                                                pcprob(hands) = ((CurrentShoe.Cards(paircard) - PlayerHands(index).Hand.Cards(paircard) - NPxHands(hands).NP) - (CurrentShoe.Cards(paircard) - PlayerHands(index).Hand.Cards(paircard) - NPxHands(hands).NP) / (CurrentShoe.CardsLeft - PlayerHands(index).Hand.NumCards - NPxHands(hands).NN - NPxHands(hands).NP + 1 - (CurrentShoe.Cards(1) - PlayerHands(index).Hand.Cards(1)))) / (CurrentShoe.CardsLeft - PlayerHands(index).Hand.NumCards - NPxHands(hands).NN - NPxHands(hands).NP)
                                                            End If
                                                        Else
                                                            pcprob(hands) = (CurrentShoe.Cards(paircard) - PlayerHands(index).Hand.Cards(paircard) - NPxHands(hands).NP) / (CurrentShoe.CardsLeft - PlayerHands(index).Hand.NumCards - NPxHands(hands).NN - NPxHands(hands).NP + 1)
                                                        End If
                                                    Else 'NPxHands(hands).NN = 2
                                                        If (upcard = 1 And CheckAce) Or (upcard = 10 And CheckTen) Then
                                                            pcprob(hands) = ConditionalValue(pcprob(NPxHands(hands).UCH), pcprob(NPxHands(hands).CH), pcprob(NPxHands(hands).UCH))
                                                        Else
                                                            pcprob(hands) = (CurrentShoe.Cards(paircard) - PlayerHands(index).Hand.Cards(paircard) - NPxHands(hands).NP) / (CurrentShoe.CardsLeft - PlayerHands(index).Hand.NumCards - NPxHands(hands).NN - NPxHands(hands).NP + 1)
                                                        End If
                                                    End If
                                                    PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), hands).DEV(upcard) = ConditionalValue(PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), NPxHands(hands).UCH).DEV(upcard), PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), NPxHands(hands).CH).DEV(upcard), pcprob(hands))
                                                    PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), hands).DPushEV(upcard) = ConditionalValue(PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), NPxHands(hands).UCH).DPushEV(upcard), PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), NPxHands(hands).CH).DPushEV(upcard), pcprob(hands))
                                                End If
                                                CurrentShoe.Undeal(paircard, 1)

                                                'Update Max strategy
                                                If PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), hands).DEV(upcard) > Opt.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), hands).StratEV(upcard) Then
                                                    Opt.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), hands).Strat(upcard) = BJCAGlobalsClass.Strat.D
                                                    Opt.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), hands).StratEV(upcard) = PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), hands).DEV(upcard)
                                                    Opt.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), hands).StratPushEV(upcard) = PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), hands).DPushEV(upcard)
                                                End If
                                            End If
                                        End If
                                    Next hands
                                    CurrentShoe.Undeal(upcard, 1)
                                End If
                            Next upcard
                        End If
                    Next index
                End If
            Next paircard
        End If
    End Sub

    Private Sub ComputeSplitSurrender()
        Dim index As Integer
        Dim upcard As Integer
        Dim paircard As Integer
        Dim hands As Integer
        Dim temphand As New BJCAHandClass
        Dim pcprob(11) As Double
        Dim newevs As BJCAHandEVsClass

        If SAS Then
            For paircard = 1 To 10
                If SplitAllowed(paircard) And Not (paircard = 1 And Not SSA) Then
                    For index = 1 To NumPHands
                        If ((PlayerHands(index).Hand.NumCards > 1 And PlayerHands(index).Hand.Cards(paircard) > 0)) Then
                            temphand.Copy(PlayerHands(index).Hand)
                            For hands = 1 To NSplitCalcs
                                If ((PlayerHands(index).Hand.Cards(paircard) + NPxHands(SplitCalcHands(hands)).NP + 1 - OriginalShoe.Cards(paircard)) < 1) Then
                                    temphand.Cards(paircard) += (NPxHands(SplitCalcHands(hands)).NP + 1)
                                    temphand.NumCards += (NPxHands(SplitCalcHands(hands)).NP + 1)

                                    For upcard = 1 To 10
                                        If UCAllowed(upcard) And PlayerHands(index).RPostallowed(upcard) > 0 Then
                                            newevs = ComputeSurrenderHand(temphand, PlayerHands(index).RPostallowed(upcard), upcard)

                                            PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)).SurrEV(upcard) = newevs.SurrEV(upcard)

                                            'Update Max strategy
                                            If PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)).SurrEV(upcard) > Opt.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)).StratEV(upcard) Then
                                                Opt.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)).Strat(upcard) = BJCAGlobalsClass.Strat.R
                                                Opt.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)).StratEV(upcard) = PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)).SurrEV(upcard)
                                                Opt.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)).StratPushEV(upcard) = 0
                                            End If

                                            temphand.Cards(paircard) -= (NPxHands(SplitCalcHands(hands)).NP + 1)
                                            temphand.NumCards -= (NPxHands(SplitCalcHands(hands)).NP + 1)
                                        End If
                                    Next upcard
                                End If
                            Next hands

                            If CDPN Then
                                CurrentShoe.Deal(paircard)
                                For hands = 0 To NSplitHands
                                    If NPxHands(hands).NN > 0 Then
                                        For upcard = 1 To 10
                                            If UCAllowed(upcard) And CurrentShoe.Cards(upcard) > 0 Then
                                                CurrentShoe.Deal(upcard)
                                                If ((NPxHands(hands).NP + PlayerHands(index).Hand.Cards(paircard)) > CurrentShoe.Cards(paircard)) Then
                                                    PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), hands).SurrEV(upcard) = 0
                                                Else
                                                    'First need to figure out post split paircard probs of UCH and CH hands
                                                    If NPxHands(hands).NN = 1 Then
                                                        If upcard = 1 And CheckAce Then
                                                            If paircard = 10 Then
                                                                pcprob(hands) = (CurrentShoe.Cards(paircard) - PlayerHands(index).Hand.Cards(paircard) - NPxHands(hands).NP) / (CurrentShoe.CardsLeft - PlayerHands(index).Hand.NumCards - NPxHands(hands).NN - NPxHands(hands).NP)
                                                            Else
                                                                pcprob(hands) = ((CurrentShoe.Cards(paircard) - PlayerHands(index).Hand.Cards(paircard) - NPxHands(hands).NP) - (CurrentShoe.Cards(paircard) - PlayerHands(index).Hand.Cards(paircard) - NPxHands(hands).NP) / (CurrentShoe.CardsLeft - PlayerHands(index).Hand.NumCards - NPxHands(hands).NN - NPxHands(hands).NP + 1 - (CurrentShoe.Cards(10) - PlayerHands(index).Hand.Cards(10)))) / (CurrentShoe.CardsLeft - PlayerHands(index).Hand.NumCards - NPxHands(hands).NN - NPxHands(hands).NP)
                                                            End If
                                                        ElseIf upcard = 10 And CheckTen Then
                                                            If paircard = 1 Then
                                                                pcprob(hands) = (CurrentShoe.Cards(paircard) - PlayerHands(index).Hand.Cards(paircard) - NPxHands(hands).NP) / (CurrentShoe.CardsLeft - PlayerHands(index).Hand.NumCards - NPxHands(hands).NN - NPxHands(hands).NP)
                                                            Else
                                                                pcprob(hands) = ((CurrentShoe.Cards(paircard) - PlayerHands(index).Hand.Cards(paircard) - NPxHands(hands).NP) - (CurrentShoe.Cards(paircard) - PlayerHands(index).Hand.Cards(paircard) - NPxHands(hands).NP) / (CurrentShoe.CardsLeft - PlayerHands(index).Hand.NumCards - NPxHands(hands).NN - NPxHands(hands).NP + 1 - (CurrentShoe.Cards(1) - PlayerHands(index).Hand.Cards(1)))) / (CurrentShoe.CardsLeft - PlayerHands(index).Hand.NumCards - NPxHands(hands).NN - NPxHands(hands).NP)
                                                            End If
                                                        Else
                                                            pcprob(hands) = (CurrentShoe.Cards(paircard) - PlayerHands(index).Hand.Cards(paircard) - NPxHands(hands).NP) / (CurrentShoe.CardsLeft - PlayerHands(index).Hand.NumCards - NPxHands(hands).NN - NPxHands(hands).NP + 1)
                                                        End If
                                                    Else 'NPxHands(hands).NN = 2
                                                        If (upcard = 1 And CheckAce) Or (upcard = 10 And CheckTen) Then
                                                            pcprob(hands) = ConditionalValue(pcprob(NPxHands(hands).UCH), pcprob(NPxHands(hands).CH), pcprob(NPxHands(hands).UCH))
                                                        Else
                                                            pcprob(hands) = (CurrentShoe.Cards(paircard) - PlayerHands(index).Hand.Cards(paircard) - NPxHands(hands).NP) / (CurrentShoe.CardsLeft - PlayerHands(index).Hand.NumCards - NPxHands(hands).NN - NPxHands(hands).NP + 1)
                                                        End If
                                                    End If
                                                    'Then use these paircard probs for the strategy
                                                    PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), hands).SurrEV(upcard) = ConditionalValue(PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), NPxHands(hands).UCH).SurrEV(upcard), PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), NPxHands(hands).CH).SurrEV(upcard), pcprob(hands))
                                                End If
                                                CurrentShoe.Undeal(upcard)

                                                'Update Max strategy
                                                If PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), hands).SurrEV(upcard) > Opt.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), hands).StratEV(upcard) Then
                                                    Opt.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), hands).Strat(upcard) = BJCAGlobalsClass.Strat.R
                                                    Opt.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), hands).StratEV(upcard) = PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), hands).SurrEV(upcard)
                                                    Opt.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), hands).StratPushEV(upcard) = 0
                                                End If
                                            End If
                                        Next upcard
                                    End If
                                Next hands
                                CurrentShoe.Undeal(paircard)
                            End If
                        End If
                    Next index
                End If
            Next paircard
        End If
    End Sub

    Private Sub ComputeSplitHit()
        Dim Total As Integer
        Dim paircard As Integer

        For paircard = 1 To 10
            If SplitAllowed(paircard) And (CurrentShoe.Cards(paircard) > 1 And Not (paircard = 1 And Not HSA)) Then
                'Hit Values for total 21 already defined during EnumPlayerHands
                For Total = 21 To 11 Step -1
                    ComputeSplitHitTotal(Total, False, paircard)
                Next Total
                For Total = 21 To 12 Step -1
                    ComputeSplitHitTotal(Total, True, paircard)
                Next Total
                For Total = 10 To 4 Step -1
                    ComputeSplitHitTotal(Total, False, paircard)
                Next Total
            End If
        Next paircard

    End Sub

    Private Sub ComputeSplitHitTotal(ByVal Total As Integer, ByVal Soft As Boolean, ByVal paircard As Integer)
        Dim index As Integer
        Dim upcard As Integer
        Dim card As Integer
        Dim prob As Double
        Dim hands As Integer
        Dim cProb(11, 10) As Double   'Prob of card
        Dim pcprob(11) As Double      'Prob of paircard

        index = PlayerHandTotal(Total, Soft + 1)
        Do While index
            If (PlayerHands(index).Hand.Cards(paircard) > 0) Then
                CurrentShoe.Deal(PlayerHands(index).Hand)
                For upcard = 1 To 10
                    If UCAllowed(upcard) And (CurrentShoe.Cards(upcard) > 0) Then
                        CurrentShoe.Deal(upcard)
                        For hands = 0 To NSplitHands
                            If (NPxHands(hands).NP + 1) > CurrentShoe.Cards(paircard) Or (NPxHands(hands).NN > 0 And Not CDPN) Then
                                pcprob(hands) = 0
                                For card = 1 To 10
                                    cProb(hands, card) = 0
                                Next card
                            Else
                                If NPxHands(hands).NN = 0 Then
                                    CurrentShoe.Deal(paircard, NPxHands(hands).NP + 1)
                                    pcprob(hands) = CardProb(paircard, upcard)
                                    CurrentShoe.Undeal(paircard, NPxHands(hands).NP + 1)
                                Else
                                    pcprob(hands) = ConditionalValue(pcprob(NPxHands(hands).UCH), pcprob(NPxHands(hands).CH), pcprob(NPxHands(hands).UCH))
                                End If

                                For card = 1 To 10
                                    If CurrentShoe.Cards(card) Then
                                        If NPxHands(hands).NN = 0 Then
                                            CurrentShoe.Deal(paircard, NPxHands(hands).NP + 1)
                                            cProb(hands, card) = CardProb(card, upcard)
                                            CurrentShoe.Undeal(paircard, NPxHands(hands).NP + 1)
                                        Else
                                            cProb(hands, card) = ConditionalValue(cProb(NPxHands(hands).UCH, card), cProb(NPxHands(hands).CH, card), pcprob(NPxHands(hands).UCH))
                                        End If
                                        prob = cProb(hands, card)

                                        If (prob > 0) And PlayerHands(index).HitHand(card) Then
                                            Opt.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), hands).HitEV(upcard) += prob * (Opt.HandEVs(PlayerHands(index).HitHand(card)).SplitData(PlayerHands(PlayerHands(index).HitHand(card)).PairIndex(paircard), hands).StratEV(upcard) + PlayerHands(PlayerHands(index).HitHand(card)).SplitEVs(PlayerHands(PlayerHands(index).HitHand(card)).PairIndex(paircard), hands).BonusEV(upcard))
                                            Opt.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), hands).HitPushEV(upcard) += prob * Opt.HandEVs(PlayerHands(index).HitHand(card)).SplitData(PlayerHands(PlayerHands(index).HitHand(card)).PairIndex(paircard), hands).StratPushEV(upcard)
                                        Else
                                            Opt.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), hands).HitEV(upcard) -= prob
                                        End If
                                    End If
                                Next card

                                'Update Max strategy for Optimal Play
                                If Opt.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), hands).HitEV(upcard) > Opt.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), hands).StratEV(upcard) Then
                                    Opt.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), hands).Strat(upcard) = BJCAGlobalsClass.Strat.H
                                    Opt.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), hands).StratEV(upcard) = Opt.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), hands).HitEV(upcard)
                                    Opt.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), hands).StratPushEV(upcard) = Opt.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), hands).HitPushEV(upcard)
                                End If
                            End If
                        Next hands
                        CurrentShoe.Undeal(upcard)
                    End If
                Next upcard
                CurrentShoe.Undeal(PlayerHands(index).Hand)
            End If
            index = PlayerHands(index).NextHand
        Loop

    End Sub

#End Region

#Region " General Split Methods "

    Private Sub ComputeSplitHandProbs()
        Dim paircard As Integer
        Dim upcard As Integer
        Dim hands As Integer
        Dim card As Integer
        Dim i As Integer

        For paircard = 1 To 10
            If SplitAllowed(paircard) Then
                CurrentShoe.Deal(paircard)  'Remove the extra pair card from the shoe
                If CardProb(paircard, 0) > 0 Then
                    DealPCard(paircard)
                    For upcard = 1 To 10
                        If UCAllowed(upcard) And CardProb(upcard, 0) > 0 Then
                            CurrentShoe.Deal(upcard)
                            SplitProbs(paircard, upcard) = New BJCASplitProbsClass

                            For card = 1 To 10
                                If CardProb(card, 0) > 0 Then
                                    'Calculate the non-conditional probabilities needed for p(P)'s
                                    For hands = 1 To NSplitCalcs
                                        i = NPxHands(SplitCalcHands(hands)).NP
                                        If i > CurrentShoe.Cards(paircard) Then i = CurrentShoe.Cards(paircard)
                                        CurrentShoe.Deal(paircard, i)

                                        SplitProbs(paircard, upcard).PxCards(SplitCalcHands(hands), card) = CardProb(card, upcard)

                                        If (BBO Or OBBO Or AOBBO) And ((upcard = 1 And Not CheckAce) Or (upcard = 10 And Not CheckTen)) Then 'things get really ugly!!
                                            'Now get the probs conditioned on dealer BJ and of dealer BJ
                                            CheckAce = True
                                            CheckTen = True
                                            SplitProbs(paircard, upcard).PCEVxCards(SplitCalcHands(hands), card) = CardProb(card, upcard)
                                            CheckAce = False
                                            CheckTen = False
                                            SplitProbs(paircard, upcard).PBJ(SplitCalcHands(hands), card) = 0
                                            SplitProbs(paircard, upcard).PPairBJCards(SplitCalcHands(hands), card) = 0
                                            If upcard = 1 And Not CheckAce Then
                                                CurrentShoe.Deal(card)
                                                SplitProbs(paircard, upcard).PBJ(SplitCalcHands(hands), card) = CardProb(10, 0)
                                                SplitProbs(paircard, upcard).PPairBJCards(SplitCalcHands(hands), card) = CardProb(paircard, 0)
                                                CurrentShoe.Undeal(card)
                                                If CardProb(10, 0) > 0 Then
                                                    CurrentShoe.Deal(10)
                                                    SplitProbs(paircard, upcard).PBJxCards(SplitCalcHands(hands), card) = CardProb(card, 0)
                                                    CurrentShoe.Undeal(10)
                                                Else
                                                    SplitProbs(paircard, upcard).PBJxCards(SplitCalcHands(hands), card) = 0
                                                End If
                                            ElseIf upcard = 10 And Not CheckTen Then
                                                CurrentShoe.Deal(card)
                                                SplitProbs(paircard, upcard).PBJ(SplitCalcHands(hands), card) = CardProb(1, 0)
                                                SplitProbs(paircard, upcard).PPairBJCards(SplitCalcHands(hands), card) = CardProb(paircard, 0)
                                                CurrentShoe.Undeal(card)
                                                If CardProb(1, 0) > 0 Then
                                                    CurrentShoe.Deal(1)
                                                    SplitProbs(paircard, upcard).PBJxCards(SplitCalcHands(hands), card) = CardProb(card, 0)
                                                    CurrentShoe.Undeal(1)
                                                Else
                                                    SplitProbs(paircard, upcard).PBJxCards(SplitCalcHands(hands), card) = 0
                                                End If
                                            End If
                                        End If
                                        CurrentShoe.Undeal(paircard, i)
                                    Next hands

                                    'Now fill in the conditional state probs
                                    For hands = 0 To NSplitHands
                                        If NPxHands(hands).NN > 0 Then
                                            SplitProbs(paircard, upcard).PxCards(hands, card) = ConditionalValue(SplitProbs(paircard, upcard).PxCards(NPxHands(hands).UCH, card), SplitProbs(paircard, upcard).PxCards(NPxHands(hands).CH, card), SplitProbs(paircard, upcard).PxCards(NPxHands(hands).UCH, paircard))
                                            If (BBO Or OBBO Or AOBBO) And ((upcard = 1 And Not CheckAce) Or (upcard = 10 And Not CheckTen)) Then
                                                SplitProbs(paircard, upcard).PBJ(hands, card) = ConditionalValue(SplitProbs(paircard, upcard).PBJ(NPxHands(hands).UCH, card), SplitProbs(paircard, upcard).PBJ(NPxHands(hands).CH, card), SplitProbs(paircard, upcard).PPairBJCards(NPxHands(hands).UCH, paircard))
                                                SplitProbs(paircard, upcard).PCEVxCards(hands, card) = ConditionalValue(SplitProbs(paircard, upcard).PCEVxCards(NPxHands(hands).UCH, card), SplitProbs(paircard, upcard).PCEVxCards(NPxHands(hands).CH, card), SplitProbs(paircard, upcard).PCEVxCards(NPxHands(hands).UCH, paircard))
                                                SplitProbs(paircard, upcard).PBJxCards(hands, card) = ConditionalValue(SplitProbs(paircard, upcard).PBJxCards(NPxHands(hands).UCH, card), SplitProbs(paircard, upcard).PBJxCards(NPxHands(hands).CH, card), SplitProbs(paircard, upcard).PBJxCards(NPxHands(hands).UCH, paircard))
                                            End If
                                        End If
                                    Next hands

                                End If  'Currentshoe(card)
                            Next card
                            CurrentShoe.Undeal(upcard)
                        End If
                    Next upcard
                    UndealPCard(paircard)
                End If
                CurrentShoe.Undeal(paircard)
            End If
        Next paircard
    End Sub

    Private Sub ComputeSplitHandsEV(ByRef cStrat As BJCAStrategyClass, ByVal paircard As Integer, ByVal upcard As Integer)
        Dim hands As Integer
        Dim card As Integer
        Dim i As Integer
        Dim prob As Double
        Dim cProb As Double
        Dim Strat As Integer
        Dim index As Integer
        Dim newIndex As Integer
        Dim EV As Double
        Dim cEV As Double
        Dim netCEV As Double
        Dim bjadjprob As Double
        Dim pRes As Double
        Dim pResEV As Double
        Dim newEVs As New BJCAHandEVsClass
        Dim newHitEVs As New BJCASplitHandEVsClass
        Dim newHand As New BJCAPlayerHandClass

        'Playerhand at this point should already have the first paircard and the upcard dealt

        'Saves alternative hands calculation for ev's
        Dim ev2 As Double

        'Compute the EV's of the second cards in the hands to use to calculate the EV(x)'s and EV(N)'s
        'Play out hand to stand based on TD Strategy
        For card = 1 To 10
            If CardProb(card, 0) > 0 Then
                DealPCard(card)
                index = FindPlayerHand(CurrentPHand)

                For hands = 1 To NSplitCalcs
                    'Check to see if the hands need to be added
                    If PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)) Is Nothing Then
                        PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)) = New BJCAHandEVsClass
                        Opt.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)) = New BJCAHandStrategyEVsClass
                        If TD.ComputeStrat Then TD.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)) = New BJCAHandStrategyEVsClass
                        If TC.ComputeStrat Then TC.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)) = New BJCAHandStrategyEVsClass
                        If Forced.ComputeStrat Then Forced.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)) = New BJCAHandStrategyEVsClass
                    End If

                    If (NPxHands(SplitCalcHands(hands)).NP <> 0 And Not MultiCardPossible(paircard, NPxHands(SplitCalcHands(hands)).NP)) Then
                        EV = 0
                        PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)).StandEV(upcard) = EV
                        PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)).StandDone(upcard) = True
                    ElseIf cStrat.Name = "CD" And (CDPN Or CDP) Then
                        cStrat.SplitStateEVs(paircard, upcard).EVCards(SplitCalcHands(hands), card) = cStrat.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)).StratEV(upcard)
                    Else
                        CurrentShoe.Deal(paircard, NPxHands(SplitCalcHands(hands)).NP)

                        If cStrat.HandEVs(index).PostForced(upcard) Then
                            Strat = GetPostSplitStrat(cStrat.HandEVs(index).EVs.ForcedPostStrat(upcard), paircard, upcard, cStrat, PlayerHands(index))
                        ElseIf cStrat.NCardsCD > 1 Then
                            Strat = GetPostSplitStrat(cStrat.HandEVs(index).EVs.Strat(upcard), paircard, upcard, cStrat, PlayerHands(index))
                        Else
                            Strat = GetPostSplitStrat(cStrat.StratTD(PlayerHands(index).Hand.Total, PlayerHands(index).Hand.Soft + 1).Strat(upcard), paircard, upcard, cStrat, PlayerHands(index))
                        End If
                        If Not cStrat.HandEVs(index).PostForced(upcard) Then
                            cStrat.HandEVs(index).EVs.ForcedPostStrat(upcard) = Strat
                        End If
                        Select Case Strat
                            Case BJCAGlobalsClass.Strat.S
                                If PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)).StandDone(upcard) And Not P21AutoWin And Not BonusRuleOn Then
                                    EV = PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)).StandEV(upcard)
                                    EV += PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)).BonusEV(upcard)
                                    cStrat.SplitStateEVs(paircard, upcard).EVCards(SplitCalcHands(hands), card) = EV
                                Else
                                    CurrentShoe.Undeal(upcard)
                                    CurrentShoe.Hand.Total = CurrentPHand.Total
                                    newEVs = ComputeStandHand(CurrentShoe.Hand, upcard, upcard, False)
                                    newHand.HandEVs.Empty()
                                    newHand.HandEVs.StandEV(upcard) = newEVs.StandEV(upcard)
                                    newHand.HandEVs.BJStandEV(upcard) = newEVs.BJStandEV(upcard)
                                    If BonusRuleOn Then
                                        newHand.Hand.Copy(PlayerHands(index).Hand)
                                        If ((upcard = 1 And Not CheckAce) Or (upcard = 10 And Not CheckTen)) Then
                                            If upcard = 1 And Not CheckAce Then
                                                bjadjprob = BJHandNumerator(10, CurrentShoe.Hand.Cards(10), 1) / BJHandDivisor(CurrentShoe.Hand.NumCards + 1, 1)
                                            ElseIf upcard = 10 And Not CheckTen Then
                                                bjadjprob = BJHandNumerator(1, CurrentShoe.Hand.Cards(1), 1) / BJHandDivisor(CurrentShoe.Hand.NumCards + 1, 1)
                                            Else
                                                bjadjprob = 0
                                            End If
                                        Else
                                            bjadjprob = 0
                                        End If
                                        newEVs.Empty()
                                        newEVs = ApplyBonusRulesNonSuitedHand(newHand, upcard, bjadjprob, True, False)
                                        newHand.HandEVs.StandEV(upcard) = newEVs.StandEV(upcard)
                                        newHand.HandEVs.BonusEV(upcard) = newEVs.BonusEV(upcard)
                                        newHand.HandEVs.BJStandEV(upcard) = newEVs.BJStandEV(upcard)
                                    End If
                                    CurrentShoe.Deal(upcard)
                                    EV = newHand.HandEVs.StandEV(upcard) + newHand.HandEVs.BonusEV(upcard)
                                    PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)).StandEV(upcard) = newHand.HandEVs.StandEV(upcard)
                                    PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)).BonusEV(upcard) = newHand.HandEVs.BonusEV(upcard)
                                    PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)).BJStandEV(upcard) = newHand.HandEVs.BJStandEV(upcard)
                                    PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)).StandDone(upcard) = True
                                    cStrat.SplitStateEVs(paircard, upcard).EVCards(SplitCalcHands(hands), card) = EV
                                End If
                                If (BBO Or OBBO Or AOBBO) And ((upcard = 1 And Not CheckAce) Or (upcard = 10 And Not CheckTen)) Then
                                    'ENHC = (1-p(BJ))*(EV|no dBJ) + p(BJ)*((1-sum(p(Res))*(-1) + sum(p(Res)*EV(Res|dBJ))
                                    'Non-Doubled
                                    'Note that pRes = 1 or 0 and BBO = ENHC when resolved
                                    'BBO = (1-p(BJ))*(EV|no dBJ) + p(BJ)*((1-sum(p(Res))*(0) + sum(p(Res)*EV(Res|dBJ))
                                    'BBO = ENHC + p(BJ)*(1-sum(p(Res))*(1)
                                    'OBBO = (1-p(BJ))*(EV|no dBJ) + p(BJ)*((1-sum(p(Res))*(-1) + sum(p(Res)*EV(Res|dBJ))
                                    'OBBO = ENHC

                                    If PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)).BJStandEV(upcard) <> -1 Then
                                        'The probability of resolution will be adjusted below for the player card
                                        cStrat.SplitStateEVs(paircard, upcard).PResCards(SplitCalcHands(hands), card) = 1
                                        cStrat.SplitStateEVs(paircard, upcard).PResEVCards(SplitCalcHands(hands), card) = PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)).BJStandEV(upcard)
                                    Else
                                        cStrat.SplitStateEVs(paircard, upcard).PResCards(SplitCalcHands(hands), card) = 0
                                        cStrat.SplitStateEVs(paircard, upcard).PResEVCards(SplitCalcHands(hands), card) = 0
                                    End If
                                    If EV = 0 Then
                                        cStrat.SplitStateEVs(paircard, upcard).BBOCards(SplitCalcHands(hands), card) = 0
                                        cStrat.SplitStateEVs(paircard, upcard).OBBOCards(SplitCalcHands(hands), card) = 0
                                        cStrat.SplitStateEVs(paircard, upcard).CEVCards(SplitCalcHands(hands), card) = 0
                                    Else
                                        cStrat.SplitStateEVs(paircard, upcard).BBOCards(SplitCalcHands(hands), card) = cStrat.SplitStateEVs(paircard, upcard).EVCards(SplitCalcHands(hands), card) + SplitProbs(paircard, upcard).PBJ(SplitCalcHands(hands), card) * (1 - cStrat.SplitStateEVs(paircard, upcard).PResCards(SplitCalcHands(hands), card))
                                        cStrat.SplitStateEVs(paircard, upcard).OBBOCards(SplitCalcHands(hands), card) = cStrat.SplitStateEVs(paircard, upcard).EVCards(SplitCalcHands(hands), card)

                                        'CEV = (UCEV - pBJ * netEVBJ) / (1 - pBJ)
                                        cStrat.SplitStateEVs(paircard, upcard).CEVCards(SplitCalcHands(hands), card) = (cStrat.SplitStateEVs(paircard, upcard).EVCards(SplitCalcHands(hands), card) - ((1 - cStrat.SplitStateEVs(paircard, upcard).PResCards(SplitCalcHands(hands), card)) * (-1) + cStrat.SplitStateEVs(paircard, upcard).PResEVCards(SplitCalcHands(hands), card)) * SplitProbs(paircard, upcard).PBJ(SplitCalcHands(hands), card)) / (1 - SplitProbs(paircard, upcard).PBJ(SplitCalcHands(hands), card))
                                    End If
                                End If
                            Case BJCAGlobalsClass.Strat.D
                                EV = 0
                                cEV = 0
                                netCEV = 0
                                pRes = 0
                                pResEV = 0
                                For i = 1 To 10 'Deal double card
                                    If CardProb(i, 0) = 0 Then
                                        EV = 0
                                        cEV = 0
                                    Else
                                        prob = CardProb(i, upcard)
                                        If (BBO Or OBBO Or AOBBO) And ((upcard = 1 And Not CheckAce) Or (upcard = 10 And Not CheckTen)) Then
                                            CheckAce = True
                                            CheckTen = True
                                            cProb = CardProb(i, upcard)
                                            CheckAce = False
                                            CheckTen = False
                                        End If
                                        DealPCard(i)
                                        If (BBO Or OBBO Or AOBBO) And ((upcard = 1 And Not CheckAce) Or (upcard = 10 And Not CheckTen)) Then
                                            If upcard = 1 And Not CheckAce Then
                                                bjadjprob = CardProb(10, 0)
                                            ElseIf upcard = 10 And Not CheckTen Then
                                                bjadjprob = CardProb(1, 0)
                                            Else
                                                bjadjprob = 0
                                            End If
                                        End If
                                        newIndex = PlayerHands(index).HitHand(i)
                                        If newIndex > 0 And PlayerHands(newIndex).SplitEVs(PlayerHands(newIndex).PairIndex(paircard), SplitCalcHands(hands)) Is Nothing Then
                                            PlayerHands(newIndex).SplitEVs(PlayerHands(newIndex).PairIndex(paircard), SplitCalcHands(hands)) = New BJCAHandEVsClass
                                            Opt.HandEVs(newIndex).SplitData(PlayerHands(newIndex).PairIndex(paircard), SplitCalcHands(hands)) = New BJCAHandStrategyEVsClass
                                            If TD.ComputeStrat Then TD.HandEVs(newIndex).SplitData(PlayerHands(newIndex).PairIndex(paircard), SplitCalcHands(hands)) = New BJCAHandStrategyEVsClass
                                            If TC.ComputeStrat Then TC.HandEVs(newIndex).SplitData(PlayerHands(newIndex).PairIndex(paircard), SplitCalcHands(hands)) = New BJCAHandStrategyEVsClass
                                            If Forced.ComputeStrat Then Forced.HandEVs(newIndex).SplitData(PlayerHands(newIndex).PairIndex(paircard), SplitCalcHands(hands)) = New BJCAHandStrategyEVsClass
                                        End If
                                        If PlayerHands(index).Hand.Soft And (PlayerHands(newIndex).Hand.Cards(1) = 1) And (DSoftAllHard Or (DSoft19Hard And PlayerHands(index).Hand.Total = 19)) Then
                                            CurrentShoe.Undeal(upcard)
                                            CurrentShoe.Hand.Total = CurrentPHand.Total - 10
                                            newEVs = ComputeStandHand(CurrentShoe.Hand, upcard, upcard, False)
                                            EV = newEVs.StandEV(upcard) * prob
                                            cEV = cProb * (newEVs.StandEV(upcard) - newEVs.BJStandEV(upcard) * bjadjprob) / (1 - bjadjprob)
                                            CurrentShoe.Deal(upcard)
                                        ElseIf CurrentPHand.Total > 21 Then
                                            EV = -1
                                            If (BBO Or OBBO Or AOBBO) And ((upcard = 1 And Not CheckAce) Or (upcard = 10 And Not CheckTen)) Then
                                                cEV = -cProb
                                                'pRes for DAS conditioned on dealer actually having BJ
                                                If upcard = 1 And Not CheckAce And CardProb(10, 0) > 0 Then
                                                    UndealPCard(i)
                                                    CurrentShoe.Deal(10)
                                                    pRes += CardProb(i, 0)
                                                    pResEV -= CardProb(i, 0)
                                                    CurrentShoe.Undeal(10)
                                                    DealPCard(i)
                                                ElseIf upcard = 10 And Not CheckTen And CardProb(1, 0) > 0 Then
                                                    UndealPCard(i)
                                                    CurrentShoe.Deal(1)
                                                    pRes += CardProb(i, 0)
                                                    pResEV -= CardProb(i, 0)
                                                    CurrentShoe.Undeal(1)
                                                    DealPCard(i)
                                                End If
                                            End If
                                        ElseIf Not (RDAPS Or DDRPS) Then
                                            'No need to go through the hassle of figuring out the double strategy
                                            If PlayerHands(newIndex).SplitEVs(PlayerHands(newIndex).PairIndex(paircard), SplitCalcHands(hands)).StandDone(upcard) Then
                                                EV = PlayerHands(newIndex).SplitEVs(PlayerHands(newIndex).PairIndex(paircard), SplitCalcHands(hands)).StandEV(upcard) * prob
                                                cEV = cProb * (EV / prob - PlayerHands(newIndex).SplitEVs(PlayerHands(newIndex).PairIndex(paircard), SplitCalcHands(hands)).BJStandEV(upcard) * bjadjprob) / (1 - bjadjprob)
                                            Else
                                                CurrentShoe.Undeal(upcard)
                                                CurrentShoe.Hand.Total = CurrentPHand.Total
                                                newEVs = ComputeStandHand(CurrentShoe.Hand, upcard, upcard, False)
                                                EV = newEVs.StandEV(upcard) * prob
                                                cEV = cProb * (newEVs.StandEV(upcard) - newEVs.BJStandEV(upcard) * bjadjprob) / (1 - bjadjprob)
                                                CurrentShoe.Deal(upcard)
                                                PlayerHands(newIndex).SplitEVs(PlayerHands(newIndex).PairIndex(paircard), SplitCalcHands(hands)).StandEV(upcard) = newEVs.StandEV(upcard)
                                                PlayerHands(newIndex).SplitEVs(PlayerHands(newIndex).PairIndex(paircard), SplitCalcHands(hands)).BJStandEV(upcard) = newEVs.BJStandEV(upcard)
                                                PlayerHands(newIndex).SplitEVs(PlayerHands(newIndex).PairIndex(paircard), SplitCalcHands(hands)).StandDone(upcard) = True
                                            End If
                                            If (BBO Or OBBO Or AOBBO) And ((upcard = 1 And Not CheckAce) Or (upcard = 10 And Not CheckTen)) And CurrentPHand.Total = 21 And P21AutoWin Then
                                                'pRes for DAS conditioned on dealer actually having BJ
                                                If upcard = 1 And Not CheckAce And CardProb(10, 0) > 0 Then
                                                    UndealPCard(i)
                                                    CurrentShoe.Deal(10)
                                                    pRes += CardProb(i, 0)
                                                    pResEV += 2 * CardProb(i, 0)
                                                    CurrentShoe.Undeal(10)
                                                    DealPCard(i)
                                                ElseIf upcard = 10 And Not CheckTen And CardProb(1, 0) > 0 Then
                                                    UndealPCard(i)
                                                    CurrentShoe.Deal(1)
                                                    pRes += CardProb(i, 0)
                                                    pResEV += 2 * CardProb(i, 0)
                                                    CurrentShoe.Undeal(1)
                                                    DealPCard(i)
                                                End If
                                            End If
                                        Else
                                            'The hand is not busted and DDR and/or RDA is allowed after splitting
                                            newEVs = ComputeSplitDoubleCardEV(newIndex, upcard, 1)
                                            EV = newEVs.DStratEV(upcard) * prob
                                            If newEVs.BJStandEV(upcard) <> 1 Then
                                                cEV = cProb * (newEVs.DStratEV(upcard) - (-1) * bjadjprob) / (1 - bjadjprob)
                                            Else
                                                cEV = cProb * newEVs.DStratEV(upcard)
                                            End If
                                            If (BBO Or OBBO Or AOBBO) And ((upcard = 1 And Not CheckAce) Or (upcard = 10 And Not CheckTen)) Then
                                                'pRes for DAS conditioned on dealer actually having BJ
                                                If upcard = 1 And Not CheckAce And CardProb(10, 0) > 0 And newEVs.BJStandEV(upcard) = 1 Then
                                                    UndealPCard(i)
                                                    CurrentShoe.Deal(10)
                                                    pRes += CardProb(i, 0)
                                                    pResEV += 2 * CardProb(i, 0) * newEVs.DStratEV(upcard)
                                                    CurrentShoe.Undeal(10)
                                                    DealPCard(i)
                                                ElseIf upcard = 10 And Not CheckTen And CardProb(1, 0) > 0 Then
                                                    UndealPCard(i)
                                                    CurrentShoe.Deal(1)
                                                    pRes += CardProb(i, 0)
                                                    pResEV += 2 * CardProb(i, 0) * newEVs.DStratEV(upcard)
                                                    CurrentShoe.Undeal(1)
                                                    DealPCard(i)
                                                End If
                                            End If
                                        End If
                                        UndealPCard(i)
                                    End If
                                    cStrat.SplitStateEVs(paircard, upcard).EVCards(SplitCalcHands(hands), card) += EV
                                    netCEV += cEV
                                Next i
                                cStrat.SplitStateEVs(paircard, upcard).EVCards(SplitCalcHands(hands), card) *= 2
                                netCEV *= 2
                                PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)).DEV(upcard) = cStrat.SplitStateEVs(paircard, upcard).EVCards(SplitCalcHands(hands), card)
                                If (BBO Or OBBO Or AOBBO) And ((upcard = 1 And Not CheckAce) Or (upcard = 10 And Not CheckTen)) Then
                                    'ENHC = (1-p(BJ))*(EV|no dBJ) + p(BJ)*((1-sum(p(Res))*(-2) + sum(p(Res)*EV(Res|dBJ))
                                    'Doubled
                                    'Note that pRes = 1 or 0 and BBO = ENHC when resolved
                                    'BBO = (1-p(BJ))*(EV|no dBJ) + p(BJ)*((1-sum(p(Res))*(0) + sum(p(Res)*EV(Res|dBJ))
                                    'BBO = ENHC + p(BJ)*(1-sum(p(Res))*(2)
                                    'OBBO = (1-p(BJ))*(EV|no dBJ) + p(BJ)*((1-sum(p(Res))*(-1) + sum(p(Res)*EV(Res|dBJ))
                                    'OBBO = ENHC + p(BJ)*(1-sum(p(Res))

                                    cStrat.SplitStateEVs(paircard, upcard).PResCards(SplitCalcHands(hands), card) = pRes
                                    cStrat.SplitStateEVs(paircard, upcard).PResEVCards(SplitCalcHands(hands), card) = pResEV
                                    If EV = 0 Then
                                        cStrat.SplitStateEVs(paircard, upcard).BBOCards(SplitCalcHands(hands), card) = 0
                                        cStrat.SplitStateEVs(paircard, upcard).OBBOCards(SplitCalcHands(hands), card) = 0
                                    Else
                                        cStrat.SplitStateEVs(paircard, upcard).BBOCards(SplitCalcHands(hands), card) = cStrat.SplitStateEVs(paircard, upcard).EVCards(SplitCalcHands(hands), card) + 2 * SplitProbs(paircard, upcard).PBJ(SplitCalcHands(hands), card) * (1 - cStrat.SplitStateEVs(paircard, upcard).PResCards(SplitCalcHands(hands), card))
                                        cStrat.SplitStateEVs(paircard, upcard).OBBOCards(SplitCalcHands(hands), card) = cStrat.SplitStateEVs(paircard, upcard).EVCards(SplitCalcHands(hands), card) + SplitProbs(paircard, upcard).PBJ(SplitCalcHands(hands), card) * (1 - cStrat.SplitStateEVs(paircard, upcard).PResCards(SplitCalcHands(hands), card))
                                    End If

                                    cStrat.SplitStateEVs(paircard, upcard).CEVCards(SplitCalcHands(hands), card) = netCEV
                                End If
                            Case BJCAGlobalsClass.Strat.H
                                newHitEVs.Empty()
                                cStrat.SplitStateEVs(paircard, upcard).PResCards(SplitCalcHands(hands), card) = 0
                                cStrat.SplitStateEVs(paircard, upcard).PResEVCards(SplitCalcHands(hands), card) = 0
                                newHitEVs = ComputeSplitHitHandEV(cStrat, paircard, upcard, SplitCalcHands(hands), index, card, 1)
                                EV = newHitEVs.StandEV(upcard)
                                cStrat.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)).HitEV(upcard) = EV
                                cStrat.SplitStateEVs(paircard, upcard).EVCards(SplitCalcHands(hands), card) = EV
                                If (BBO Or OBBO Or AOBBO) And ((upcard = 1 And Not CheckAce) Or (upcard = 10 And Not CheckTen)) Then
                                    '   pRes is calculated automatically through splitplayerhit
                                    If EV = 0 Then
                                        cStrat.SplitStateEVs(paircard, upcard).BBOCards(SplitCalcHands(hands), card) = 0
                                        cStrat.SplitStateEVs(paircard, upcard).OBBOCards(SplitCalcHands(hands), card) = 0
                                        cStrat.SplitStateEVs(paircard, upcard).CEVCards(SplitCalcHands(hands), card) = 0
                                    Else
                                        cStrat.SplitStateEVs(paircard, upcard).BBOCards(SplitCalcHands(hands), card) = newHitEVs.BBOEV(upcard)
                                        cStrat.SplitStateEVs(paircard, upcard).OBBOCards(SplitCalcHands(hands), card) = newHitEVs.OBBOEV(upcard)

                                        cStrat.SplitStateEVs(paircard, upcard).CEVCards(SplitCalcHands(hands), card) = newHitEVs.CEV(upcard)
                                    End If
                                End If
                            Case BJCAGlobalsClass.Strat.R
                                newEVs = ComputeSurrenderHand(CurrentShoe.Hand, cStrat.HandEVs(index).RPostallowed(upcard), upcard)
                                EV = newEVs.SurrEV(upcard)
                                PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), SplitCalcHands(hands)).SurrEV(upcard) = EV
                                cStrat.SplitStateEVs(paircard, upcard).EVCards(SplitCalcHands(hands), card) = EV

                                If (BBO Or OBBO Or AOBBO) And ((upcard = 1 And Not CheckAce) Or (upcard = 10 And Not CheckTen)) Then
                                    'See Stand for details
                                    If cStrat.HandEVs(index).RPostallowed(upcard) = C.Surr.ES Then
                                        cStrat.SplitStateEVs(paircard, upcard).PResCards(SplitCalcHands(hands), card) = 1
                                        cStrat.SplitStateEVs(paircard, upcard).PResEVCards(SplitCalcHands(hands), card) = SurrDBJPays
                                    Else
                                        cStrat.SplitStateEVs(paircard, upcard).PResCards(SplitCalcHands(hands), card) = 0
                                        cStrat.SplitStateEVs(paircard, upcard).PResEVCards(SplitCalcHands(hands), card) = 0
                                    End If
                                    If EV = 0 Then
                                        cStrat.SplitStateEVs(paircard, upcard).BBOCards(SplitCalcHands(hands), card) = 0
                                        cStrat.SplitStateEVs(paircard, upcard).OBBOCards(SplitCalcHands(hands), card) = 0
                                        cStrat.SplitStateEVs(paircard, upcard).CEVCards(SplitCalcHands(hands), card) = 0
                                    Else
                                        cStrat.SplitStateEVs(paircard, upcard).BBOCards(SplitCalcHands(hands), card) = cStrat.SplitStateEVs(paircard, upcard).EVCards(SplitCalcHands(hands), card) + SplitProbs(paircard, upcard).PBJ(SplitCalcHands(hands), card) * (1 - cStrat.SplitStateEVs(paircard, upcard).PResCards(SplitCalcHands(hands), card))
                                        cStrat.SplitStateEVs(paircard, upcard).OBBOCards(SplitCalcHands(hands), card) = cStrat.SplitStateEVs(paircard, upcard).EVCards(SplitCalcHands(hands), card)

                                        'CEV = (UCEV - pBJ * netEVBJ) / (1 - pBJ)
                                        cStrat.SplitStateEVs(paircard, upcard).CEVCards(SplitCalcHands(hands), card) = (cStrat.SplitStateEVs(paircard, upcard).EVCards(SplitCalcHands(hands), card) - ((1 - cStrat.SplitStateEVs(paircard, upcard).PResCards(SplitCalcHands(hands), card)) * (-1) + cStrat.SplitStateEVs(paircard, upcard).PResEVCards(SplitCalcHands(hands), card)) * SplitProbs(paircard, upcard).PBJ(SplitCalcHands(hands), card)) / (1 - SplitProbs(paircard, upcard).PBJ(SplitCalcHands(hands), card))
                                    End If
                                End If
                        End Select

                        CurrentShoe.Undeal(paircard, NPxHands(SplitCalcHands(hands)).NP)
                    End If
                Next hands
                UndealPCard(card)
            End If  'Currentshoe(card)
        Next card

        'Compute EV(x)'s and EV(N)'s
        For hands = 0 To NSplitHands
            If NPxHands(hands).NN = 0 Then
                For card = 1 To 10
                    cStrat.SplitStateEVs(paircard, upcard).EVx(hands) += cStrat.SplitStateEVs(paircard, upcard).EVCards(hands, card) * SplitProbs(paircard, upcard).PxCards(hands, card)
                Next card
                cStrat.SplitStateEVs(paircard, upcard).EVN(hands) = ConditionalValue(cStrat.SplitStateEVs(paircard, upcard).EVx(hands), cStrat.SplitStateEVs(paircard, upcard).EVCards(hands, paircard), SplitProbs(paircard, upcard).PxCards(hands, paircard))
            ElseIf CDPN And cStrat.Name = "CD" Then
                If CurrentShoe.Cards(paircard) >= NPxHands(hands).NP Then
                    CurrentShoe.Deal(paircard, NPxHands(hands).NP)
                    For card = 1 To 10
                        If CurrentShoe.Cards(card) > 0 Then
                            DealPCard(card)
                            index = FindPlayerHand(CurrentPHand)
                            cStrat.SplitStateEVs(paircard, upcard).EVCards(hands, card) = cStrat.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), hands).StratEV(upcard)
                            SplitProbs(paircard, upcard).PxCards(hands, card) = ConditionalValue(SplitProbs(paircard, upcard).PxCards(NPxHands(hands).UCH, card), SplitProbs(paircard, upcard).PxCards(NPxHands(hands).CH, card), SplitProbs(paircard, upcard).PxCards(NPxHands(hands).UCH, paircard))
                            cStrat.SplitStateEVs(paircard, upcard).EVx(hands) = cStrat.SplitStateEVs(paircard, upcard).EVx(hands) + cStrat.SplitStateEVs(paircard, upcard).EVCards(hands, card) * SplitProbs(paircard, upcard).PxCards(hands, card)
                            UndealPCard(card)
                        End If
                    Next card
                    cStrat.SplitStateEVs(paircard, upcard).EVN(hands) = ConditionalValue(cStrat.SplitStateEVs(paircard, upcard).EVx(hands), cStrat.SplitStateEVs(paircard, upcard).EVCards(hands, paircard), SplitProbs(paircard, upcard).PxCards(hands, paircard))

                    CurrentShoe.Undeal(paircard, NPxHands(hands).NP)
                End If
            Else
                cStrat.SplitStateEVs(paircard, upcard).EVx(hands) = ConditionalValue(cStrat.SplitStateEVs(paircard, upcard).EVx(NPxHands(hands).UCH), cStrat.SplitStateEVs(paircard, upcard).EVx(NPxHands(hands).CH), SplitProbs(paircard, upcard).PxCards(NPxHands(hands).UCH, paircard))
                cStrat.SplitStateEVs(paircard, upcard).EVN(hands) = ConditionalValue(cStrat.SplitStateEVs(paircard, upcard).EVN(NPxHands(hands).UCH), cStrat.SplitStateEVs(paircard, upcard).EVN(NPxHands(hands).CH), SplitProbs(paircard, upcard).PxCards(hands, paircard))
            End If

            'Here we need not only the probability of busting, but also the conditional EV of each N and x
            If (BBO Or OBBO Or AOBBO) And ((upcard = 1 And Not CheckAce) Or (upcard = 10 And Not CheckTen)) Then
                'Calculate the overall p(Bust) for N and x
                If NPxHands(hands).NN = 0 Then
                    For card = 1 To 10
                        'Add up the probabilities of the hand resolving given a dealer BJ
                        cStrat.SplitStateEVs(paircard, upcard).PResx(hands) += cStrat.SplitStateEVs(paircard, upcard).PResCards(hands, card) * SplitProbs(paircard, upcard).PBJxCards(hands, card)
                        cStrat.SplitStateEVs(paircard, upcard).PResEVx(hands) += cStrat.SplitStateEVs(paircard, upcard).PResEVCards(hands, card) * SplitProbs(paircard, upcard).PBJxCards(hands, card)
                        'Add up the appropriate hand evs
                        cStrat.SplitStateEVs(paircard, upcard).BBOx(hands) += cStrat.SplitStateEVs(paircard, upcard).BBOCards(hands, card) * SplitProbs(paircard, upcard).PxCards(hands, card)
                        cStrat.SplitStateEVs(paircard, upcard).OBBOx(hands) += cStrat.SplitStateEVs(paircard, upcard).OBBOCards(hands, card) * SplitProbs(paircard, upcard).PxCards(hands, card)
                        'Add them with the correct probs to get the conditional EV
                        cStrat.SplitStateEVs(paircard, upcard).CEVx(hands) += cStrat.SplitStateEVs(paircard, upcard).CEVCards(hands, card) * SplitProbs(paircard, upcard).PCEVxCards(hands, card)
                    Next card
                    cStrat.SplitStateEVs(paircard, upcard).PResN(hands) = ConditionalValue(cStrat.SplitStateEVs(paircard, upcard).PResx(hands), cStrat.SplitStateEVs(paircard, upcard).PResCards(hands, paircard), SplitProbs(paircard, upcard).PBJxCards(hands, paircard))
                    cStrat.SplitStateEVs(paircard, upcard).PResEVN(hands) = ConditionalValue(cStrat.SplitStateEVs(paircard, upcard).PResEVx(hands), cStrat.SplitStateEVs(paircard, upcard).PResEVCards(hands, paircard), SplitProbs(paircard, upcard).PBJxCards(hands, paircard))
                    cStrat.SplitStateEVs(paircard, upcard).BBON(hands) = ConditionalValue(cStrat.SplitStateEVs(paircard, upcard).BBOx(hands), cStrat.SplitStateEVs(paircard, upcard).BBOCards(hands, paircard), SplitProbs(paircard, upcard).PxCards(hands, paircard))
                    cStrat.SplitStateEVs(paircard, upcard).OBBON(hands) = ConditionalValue(cStrat.SplitStateEVs(paircard, upcard).OBBOx(hands), cStrat.SplitStateEVs(paircard, upcard).OBBOCards(hands, paircard), SplitProbs(paircard, upcard).PxCards(hands, paircard))
                    'Then to get EVN, we also need the EVx of the paircard)
                    cStrat.SplitStateEVs(paircard, upcard).CEVN(hands) = ConditionalValue(cStrat.SplitStateEVs(paircard, upcard).CEVx(hands), cStrat.SplitStateEVs(paircard, upcard).CEVCards(hands, paircard), SplitProbs(paircard, upcard).PCEVxCards(hands, paircard))
                Else
                    'Fill in the other states
                    cStrat.SplitStateEVs(paircard, upcard).PResx(hands) = ConditionalValue(cStrat.SplitStateEVs(paircard, upcard).PResx(NPxHands(hands).UCH), cStrat.SplitStateEVs(paircard, upcard).PResx(NPxHands(hands).CH), SplitProbs(paircard, upcard).PBJxCards(NPxHands(hands).UCH, paircard))
                    cStrat.SplitStateEVs(paircard, upcard).PResN(hands) = ConditionalValue(cStrat.SplitStateEVs(paircard, upcard).PResN(NPxHands(hands).UCH), cStrat.SplitStateEVs(paircard, upcard).PResN(NPxHands(hands).CH), SplitProbs(paircard, upcard).PBJxCards(hands, paircard))
                    cStrat.SplitStateEVs(paircard, upcard).PResEVx(hands) = ConditionalValue(cStrat.SplitStateEVs(paircard, upcard).PResEVx(NPxHands(hands).UCH), cStrat.SplitStateEVs(paircard, upcard).PResEVx(NPxHands(hands).CH), SplitProbs(paircard, upcard).PBJxCards(NPxHands(hands).UCH, paircard))
                    cStrat.SplitStateEVs(paircard, upcard).PResEVN(hands) = ConditionalValue(cStrat.SplitStateEVs(paircard, upcard).PResEVN(NPxHands(hands).UCH), cStrat.SplitStateEVs(paircard, upcard).PResEVN(NPxHands(hands).CH), SplitProbs(paircard, upcard).PBJxCards(hands, paircard))
                    cStrat.SplitStateEVs(paircard, upcard).BBOx(hands) = ConditionalValue(cStrat.SplitStateEVs(paircard, upcard).BBOx(NPxHands(hands).UCH), cStrat.SplitStateEVs(paircard, upcard).BBOx(NPxHands(hands).CH), SplitProbs(paircard, upcard).PxCards(NPxHands(hands).UCH, paircard))
                    cStrat.SplitStateEVs(paircard, upcard).BBON(hands) = ConditionalValue(cStrat.SplitStateEVs(paircard, upcard).BBON(NPxHands(hands).UCH), cStrat.SplitStateEVs(paircard, upcard).BBON(NPxHands(hands).CH), SplitProbs(paircard, upcard).PxCards(hands, paircard))
                    cStrat.SplitStateEVs(paircard, upcard).OBBOx(hands) = ConditionalValue(cStrat.SplitStateEVs(paircard, upcard).OBBOx(NPxHands(hands).UCH), cStrat.SplitStateEVs(paircard, upcard).OBBOx(NPxHands(hands).CH), SplitProbs(paircard, upcard).PxCards(NPxHands(hands).UCH, paircard))
                    cStrat.SplitStateEVs(paircard, upcard).OBBON(hands) = ConditionalValue(cStrat.SplitStateEVs(paircard, upcard).OBBON(NPxHands(hands).UCH), cStrat.SplitStateEVs(paircard, upcard).OBBON(NPxHands(hands).CH), SplitProbs(paircard, upcard).PxCards(hands, paircard))
                    cStrat.SplitStateEVs(paircard, upcard).CEVx(hands) = ConditionalValue(cStrat.SplitStateEVs(paircard, upcard).CEVx(NPxHands(hands).UCH), cStrat.SplitStateEVs(paircard, upcard).CEVx(NPxHands(hands).CH), SplitProbs(paircard, upcard).PCEVxCards(NPxHands(hands).UCH, paircard))
                    cStrat.SplitStateEVs(paircard, upcard).CEVN(hands) = ConditionalValue(cStrat.SplitStateEVs(paircard, upcard).CEVN(NPxHands(hands).UCH), cStrat.SplitStateEVs(paircard, upcard).CEVN(NPxHands(hands).CH), SplitProbs(paircard, upcard).PCEVxCards(hands, paircard))
                End If
            End If
        Next hands

    End Sub

    Private Function ComputeSplitHitHandEV(ByRef cStrat As BJCAStrategyClass, ByVal paircard As Integer, ByVal upcard As Integer, ByVal hands As Integer, ByVal oldindex As Integer, ByVal basecard As Integer, ByVal NetpResProb As Double) As BJCASplitHandEVsClass
        Dim card As Integer
        Dim i As Integer
        Dim EV As Double
        Dim CEV As Double
        Dim netCEV As Double
        Dim BBOEV As Double
        Dim OBBOEV As Double
        Dim pRes As Double
        Dim DEV As Double
        Dim prob As Double
        Dim cProb As Double
        Dim dProb As Double
        Dim cDEV As Double
        Dim cDProb As Double
        Dim bjadjprob As Double
        Dim tempEV As Double
        Dim tempPush As Double
        Dim tempBJEV As Double
        Dim strat As Integer
        Dim index As Integer
        Dim newIndex As Integer
        Dim newNetpResProb As Double
        Dim dRes As Double
        Dim dResEV As Double
        Dim push As Double
        Dim dPush As Double
        Dim newEVs As New BJCAHandEVsClass
        Dim newHitEVs As New BJCASplitHandEVsClass
        Dim newHand As New BJCAPlayerHandClass
        Dim tempHand As New BJCAHandClass

        EV = 0
        CEV = 0
        push = 0
        BBOEV = 0
        OBBOEV = 0

        'Play out hand to stand based on the Strategy
        For card = 1 To 10
            If CardProb(card, 0) > 0 Then
                prob = CardProb(card, upcard)

                newNetpResProb = NetpResProb
                If (BBO Or OBBO Or AOBBO) And ((upcard = 1 And Not CheckAce) Or (upcard = 10 And Not CheckTen)) Then
                    CheckAce = True
                    CheckTen = True
                    cProb = CardProb(card, upcard)
                    CheckAce = False
                    CheckTen = False
                    If upcard = 1 And Not CheckAce Then
                        If CardProb(10, 0) > 0 Then
                            CurrentShoe.Deal(10, 1)
                            newNetpResProb = NetpResProb * CardProb(card, 0)
                            CurrentShoe.Undeal(10, 1)
                        Else
                            newNetpResProb = 0
                        End If
                    ElseIf upcard = 10 And Not CheckTen Then
                        If CardProb(1, 0) > 0 Then
                            CurrentShoe.Deal(1, 1)
                            newNetpResProb = NetpResProb * CardProb(card, 0)
                            CurrentShoe.Undeal(1, 1)
                        Else
                            newNetpResProb = 0
                        End If
                    End If
                End If

                If (CurrentPHand.Total + card) > 21 And Not CurrentPHand.Soft Then  'Busted
                    EV -= prob
                    CEV -= cProb
                    If (BBO Or OBBO Or AOBBO) And ((upcard = 1 And Not CheckAce) Or (upcard = 10 And Not CheckTen)) Then
                        cStrat.SplitStateEVs(paircard, upcard).PResCards(hands, basecard) += newNetpResProb
                        cStrat.SplitStateEVs(paircard, upcard).PResEVCards(hands, basecard) -= newNetpResProb
                        BBOEV -= prob
                        OBBOEV -= prob
                    End If
                Else    'Not busted
                    DealPCard(card)
                    index = PlayerHands(oldindex).HitHand(card)

                    If PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), hands) Is Nothing Then
                        PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), hands) = New BJCAHandEVsClass
                    End If
                    If cStrat.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), hands) Is Nothing Then
                        cStrat.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), hands) = New BJCAHandStrategyEVsClass
                    End If

                    If cStrat.HandEVs(index).PostForced(upcard) Then
                        strat = GetPostSplitStrat(cStrat.HandEVs(index).EVs.ForcedPostStrat(upcard), paircard, upcard, cStrat, PlayerHands(index))
                    ElseIf cStrat.NCardsCD >= PlayerHands(index).Hand.NumCards Then
                        strat = GetPostSplitStrat(cStrat.HandEVs(index).EVs.Strat(upcard), paircard, upcard, cStrat, PlayerHands(index))
                    Else
                        strat = GetPostSplitStrat(cStrat.StratTD(PlayerHands(index).Hand.Total, PlayerHands(index).Hand.Soft + 1).Strat(upcard), paircard, upcard, cStrat, PlayerHands(index))
                    End If
                    If Not cStrat.HandEVs(index).PostForced(upcard) Then
                        cStrat.HandEVs(index).EVs.ForcedPostStrat(upcard) = strat
                    End If
                    Select Case strat
                        Case BJCAGlobalsClass.Strat.S   'Stand
                            If PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), hands).StandDone(upcard) And Not P21AutoWin And Not BonusRuleOn Then
                                tempEV = PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), hands).StandEV(upcard) + PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), hands).BonusEV(upcard)
                                tempPush = PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), hands).StandPushEV(upcard)
                                tempBJEV = PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), hands).BJStandEV(upcard)
                            Else
                                CurrentShoe.Undeal(upcard, 1)
                                CurrentShoe.Hand.Total = CurrentPHand.Total
                                newEVs = ComputeStandHand(CurrentShoe.Hand, upcard, upcard, False)
                                newHand.HandEVs.Empty()
                                newHand.HandEVs.StandEV(upcard) = newEVs.StandEV(upcard)
                                newHand.HandEVs.StandPushEV(upcard) = newEVs.StandPushEV(upcard)
                                newHand.HandEVs.BJStandEV(upcard) = newEVs.BJStandEV(upcard)
                                If BonusRuleOn Then
                                    newHand.Hand.Copy(PlayerHands(index).Hand)
                                    If ((upcard = 1 And Not CheckAce) Or (upcard = 10 And Not CheckTen)) Then
                                        If upcard = 1 And Not CheckAce Then
                                            bjadjprob = BJHandNumerator(10, CurrentShoe.Hand.Cards(10), 1) / BJHandDivisor(CurrentShoe.Hand.NumCards + 1, 1)
                                        ElseIf upcard = 10 And Not CheckTen Then
                                            bjadjprob = BJHandNumerator(1, CurrentShoe.Hand.Cards(1), 1) / BJHandDivisor(CurrentShoe.Hand.NumCards + 1, 1)
                                        Else
                                            bjadjprob = 0
                                        End If
                                    Else
                                        bjadjprob = 0
                                    End If
                                    newEVs.Empty()
                                    newEVs = ApplyBonusRulesNonSuitedHand(newHand, upcard, bjadjprob, True, False)
                                    newHand.HandEVs.StandEV(upcard) = newEVs.StandEV(upcard)
                                    newHand.HandEVs.StandPushEV(upcard) = newEVs.StandPushEV(upcard)
                                    newHand.HandEVs.BonusEV(upcard) = newEVs.BonusEV(upcard)
                                    newHand.HandEVs.BJStandEV(upcard) = newEVs.BJStandEV(upcard)
                                End If
                                tempEV = newHand.HandEVs.StandEV(upcard) + newHand.HandEVs.BonusEV(upcard)
                                tempPush = newHand.HandEVs.StandPushEV(upcard)
                                tempBJEV = newEVs.BJStandEV(upcard)
                                CurrentShoe.Deal(upcard, 1)
                                PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), hands).StandEV(upcard) = newEVs.StandEV(upcard)
                                PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), hands).StandPushEV(upcard) = newEVs.StandPushEV(upcard)
                                PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), hands).BonusEV(upcard) = newEVs.BonusEV(upcard)
                                PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), hands).BJStandEV(upcard) = newEVs.BJStandEV(upcard)
                                PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), hands).StandDone(upcard) = True
                            End If

                            EV += prob * tempEV
                            push += prob * tempPush
                            If (BBO Or OBBO Or AOBBO) And ((upcard = 1 And Not CheckAce) Or (upcard = 10 And Not CheckTen)) Then
                                'ENHC = (1-p(BJ))*(EV|no dBJ) + p(BJ)*((1-sum(p(Res))*(-1) + sum(p(Res)*EV(Res|dBJ))
                                'Non-Doubled
                                'Note that pRes = 1 or 0 and BBO = ENHC when resolved
                                'BBO = (1-p(BJ))*(EV|no dBJ) + p(BJ)*((1-sum(p(Res))*(0) + sum(p(Res)*EV(Res|dBJ))
                                'BBO = ENHC + p(BJ)*(1-sum(p(Res))*(1)
                                'OBBO = (1-p(BJ))*(EV|no dBJ) + p(BJ)*((1-sum(p(Res))*(-1) + sum(p(Res)*EV(Res|dBJ))
                                'OBBO = ENHC
                                If upcard = 1 And Not CheckAce Then
                                    bjadjprob = CardProb(10, 0)
                                ElseIf upcard = 10 And Not CheckTen Then
                                    bjadjprob = CardProb(1, 0)
                                Else
                                    bjadjprob = 0
                                End If
                                If tempBJEV <> -1 Then
                                    'The probability of resolution will be adjusted below for the player card
                                    cStrat.SplitStateEVs(paircard, upcard).PResCards(hands, basecard) += newNetpResProb
                                    cStrat.SplitStateEVs(paircard, upcard).PResEVCards(hands, basecard) += newNetpResProb * tempBJEV
                                    pRes = 1
                                End If
                                If EV <> 0 Then
                                    BBOEV += prob * (tempEV + bjadjprob * (1 - pRes))
                                    OBBOEV += prob * tempEV

                                    'CEV = (UCEV - pBJ * netEVBJ) / (1 - pBJ)
                                    CEV += cProb * (tempEV - bjadjprob * tempBJEV) / (1 - bjadjprob)
                                End If
                            End If
                        Case BJCAGlobalsClass.Strat.D   'Double
                            DEV = 0
                            dPush = 0
                            dRes = 0
                            dresev = 0
                            cDEV = 0
                            netCEV = 0
                            For i = 1 To 10 'Deal double card
                                dProb = CardProb(i, upcard)
                                If dProb = 0 Then
                                    DEV = 0
                                    cDEV = 0
                                Else
                                    If (BBO Or OBBO Or AOBBO) And ((upcard = 1 And Not CheckAce) Or (upcard = 10 And Not CheckTen)) Then
                                        CheckAce = True
                                        CheckTen = True
                                        cdProb = CardProb(i, upcard)
                                        CheckAce = False
                                        CheckTen = False
                                    End If
                                    DealPCard(i)
                                    If (BBO Or OBBO Or AOBBO) And ((upcard = 1 And Not CheckAce) Or (upcard = 10 And Not CheckTen)) Then
                                        If upcard = 1 And Not CheckAce Then
                                            bjadjprob = CardProb(10, 0)
                                        ElseIf upcard = 10 And Not CheckTen Then
                                            bjadjprob = CardProb(1, 0)
                                        Else
                                            bjadjprob = 0
                                        End If
                                    End If
                                    newIndex = PlayerHands(index).HitHand(i)
                                    If newIndex > 0 And PlayerHands(newIndex).SplitEVs(PlayerHands(newIndex).PairIndex(paircard), hands) Is Nothing Then
                                        PlayerHands(newIndex).SplitEVs(PlayerHands(newIndex).PairIndex(paircard), hands) = New BJCAHandEVsClass
                                        cStrat.HandEVs(newIndex).SplitData(PlayerHands(newIndex).PairIndex(paircard), hands) = New BJCAHandStrategyEVsClass
                                    End If
                                    If PlayerHands(oldindex).Hand.Soft And (PlayerHands(newIndex).Hand.Cards(1) = 1) And (DSoftAllHard Or (DSoft19Hard And PlayerHands(oldindex).Hand.Total = 19)) Then
                                        CurrentShoe.Undeal(upcard, 1)
                                        CurrentShoe.Hand.Total = CurrentPHand.Total - 10
                                        newEVs = ComputeStandHand(CurrentShoe.Hand, upcard, upcard, False)

                                        tempEV = newEVs.StandEV(upcard)
                                        tempPush = newEVs.StandPushEV(upcard)
                                        cDEV = (newEVs.StandEV(upcard) - newEVs.BJStandEV(upcard) * bjadjprob) / (1 - bjadjprob)
                                        CurrentShoe.Deal(upcard, 1)
                                    ElseIf CurrentPHand.Total > 21 Then
                                        tempEV = -1
                                        cDEV = -1
                                        If (BBO Or OBBO Or AOBBO) And ((upcard = 1 And Not CheckAce) Or (upcard = 10 And Not CheckTen)) Then
                                            'Busts for DAS conditioned on dealer actually having BJ
                                            If upcard = 1 And Not CheckAce And CardProb(10, 0) > 0 Then
                                                UndealPCard(i)
                                                CurrentShoe.Deal(10, 1)
                                                dRes += CardProb(i, 0)
                                                dResEV -= 2 * CardProb(i, 0)
                                                CurrentShoe.Undeal(10, 1)
                                                DealPCard(i)
                                            ElseIf upcard = 10 And Not CheckTen And CardProb(1, 0) > 0 Then
                                                UndealPCard(i)
                                                CurrentShoe.Deal(1, 1)
                                                dRes += CardProb(i, 0)
                                                dResEV -= 2 * CardProb(i, 0)
                                                CurrentShoe.Undeal(1, 1)
                                                DealPCard(i)
                                            End If
                                        End If
                                    ElseIf Not (RDAPS Or DDRPS) Then
                                        If PlayerHands(newIndex).SplitEVs(PlayerHands(newIndex).PairIndex(paircard), hands).StandDone(upcard) Then
                                            tempEV = PlayerHands(newIndex).SplitEVs(PlayerHands(newIndex).PairIndex(paircard), hands).StandEV(upcard)
                                            tempPush = PlayerHands(newIndex).SplitEVs(PlayerHands(newIndex).PairIndex(paircard), hands).StandPushEV(upcard)
                                            tempBJEV = PlayerHands(newIndex).SplitEVs(PlayerHands(newIndex).PairIndex(paircard), hands).BJStandEV(upcard)

                                            cDEV = (tempEV - tempBJEV * bjadjprob) / (1 - bjadjprob)
                                        Else
                                            CurrentShoe.Undeal(upcard, 1)
                                            CurrentShoe.Hand.Total = CurrentPHand.Total
                                            newEVs = ComputeStandHand(CurrentShoe.Hand, upcard, upcard, False)
                                            tempEV = newEVs.StandEV(upcard)
                                            tempPush = newEVs.StandPushEV(upcard)
                                            tempBJEV = newEVs.BJStandEV(upcard)
                                            cDEV = (newEVs.StandEV(upcard) - newEVs.BJStandEV(upcard) * bjadjprob) / (1 - bjadjprob)
                                            CurrentShoe.Deal(upcard, 1)
                                            PlayerHands(newIndex).SplitEVs(PlayerHands(newIndex).PairIndex(paircard), hands).StandEV(upcard) = newEVs.StandEV(upcard)
                                            PlayerHands(newIndex).SplitEVs(PlayerHands(newIndex).PairIndex(paircard), hands).StandPushEV(upcard) = newEVs.StandPushEV(upcard)
                                            PlayerHands(newIndex).SplitEVs(PlayerHands(newIndex).PairIndex(paircard), hands).BJStandEV(upcard) = newEVs.BJStandEV(upcard)
                                            PlayerHands(newIndex).SplitEVs(PlayerHands(newIndex).PairIndex(paircard), hands).StandDone(upcard) = True
                                        End If
                                        If (BBO Or OBBO Or AOBBO) And ((upcard = 1 And Not CheckAce) Or (upcard = 10 And Not CheckTen)) And tempBJEV <> -1 Then
                                            'pRes for DAS conditioned on dealer actually having BJ
                                            If upcard = 1 And Not CheckAce And CardProb(10, 0) > 0 Then
                                                UndealPCard(i)
                                                CurrentShoe.Deal(10)
                                                dRes += CardProb(i, 0)
                                                dResEV += 2 * CardProb(i, 0) * tempBJEV
                                                CurrentShoe.Undeal(10)
                                                DealPCard(i)
                                            ElseIf upcard = 10 And Not CheckTen And CardProb(1, 0) > 0 Then
                                                UndealPCard(i)
                                                CurrentShoe.Deal(1)
                                                dRes += CardProb(i, 0)
                                                dResEV += 2 * CardProb(i, 0) * tempBJEV
                                                CurrentShoe.Undeal(1)
                                                DealPCard(i)
                                            End If
                                        End If
                                    Else
                                        'The hand is not busted and DDR and/or RDA is allowed after splitting
                                        newEVs = ComputeSplitDoubleCardEV(newIndex, upcard, 1)
                                        tempEV = newEVs.DStratEV(upcard)
                                        tempPush = newEVs.DStratPushEV(upcard)
                                        If newEVs.BJStandEV(upcard) <> 1 Then
                                            cDEV = (newEVs.DStratEV(upcard) - (-1) * bjadjprob) / (1 - bjadjprob)
                                        Else
                                            cDEV = newEVs.DStratEV(upcard)
                                        End If
                                        If (BBO Or OBBO Or AOBBO) And ((upcard = 1 And Not CheckAce) Or (upcard = 10 And Not CheckTen)) And newEVs.BJStandEV(upcard) = 1 Then
                                            'pRes for DAS conditioned on dealer actually having BJ
                                            If upcard = 1 And Not CheckAce And CardProb(10, 0) > 0 Then
                                                UndealPCard(i)
                                                CurrentShoe.Deal(10)
                                                dRes += CardProb(i, 0)
                                                dResEV += 2 * CardProb(i, 0) * newEVs.DStratEV(upcard)
                                                CurrentShoe.Undeal(10)
                                                DealPCard(i)
                                            ElseIf upcard = 10 And Not CheckTen And CardProb(1, 0) > 0 Then
                                                UndealPCard(i)
                                                CurrentShoe.Deal(1)
                                                dRes += CardProb(i, 0)
                                                dResEV += 2 * CardProb(i, 0) * newEVs.DStratEV(upcard)
                                                CurrentShoe.Undeal(1)
                                                DealPCard(i)
                                            End If
                                        End If
                                    End If

                                    DEV += dProb * tempEV
                                    dPush += dProb * tempPush
                                    netCEV += cDProb * cDEV
                                    UndealPCard(i)
                                End If
                            Next i
                            EV += prob * 2 * DEV
                            push += prob * 2 * dPush
                            If (BBO Or OBBO Or AOBBO) And ((upcard = 1 And Not CheckAce) Or (upcard = 10 And Not CheckTen)) Then
                                cStrat.SplitStateEVs(paircard, upcard).PResCards(hands, basecard) += newNetpResProb * dRes
                                cStrat.SplitStateEVs(paircard, upcard).PResEVCards(hands, basecard) += newNetpResProb * dRes * dResEV
                                'ENHC = (1-p(BJ))*(EV|no dBJ) + p(BJ)*((1-sum(p(Res))*(-2) + sum(p(Res)*EV(Res|dBJ))
                                'Doubled
                                'Note that pRes = 1 or 0 and BBO = ENHC when resolved
                                'BBO = (1-p(BJ))*(EV|no dBJ) + p(BJ)*((1-sum(p(Res))*(0) + sum(p(Res)*EV(Res|dBJ))
                                'BBO = ENHC + p(BJ)*(1-sum(p(Res))*(2)
                                'OBBO = (1-p(BJ))*(EV|no dBJ) + p(BJ)*((1-sum(p(Res))*(-1) + sum(p(Res)*EV(Res|dBJ))
                                'OBBO = ENHC + p(BJ)*(1-sum(p(Res))

                                If upcard = 1 And Not CheckAce Then
                                    bjadjprob = CardProb(10, 0)
                                ElseIf upcard = 10 And Not CheckTen Then
                                    bjadjprob = CardProb(1, 0)
                                Else
                                    bjadjprob = 0
                                End If
                                If EV <> 0 Then
                                    BBOEV += prob * (2 * DEV + 2 * bjadjprob * (1 - dRes))
                                    OBBOEV += prob * (2 * DEV + bjadjprob * (1 - dRes))
                                End If
                                CEV += cProb * 2 * netCEV
                            End If
                        Case BJCAGlobalsClass.Strat.H  'Hit
                            newHitEVs.Empty()
                            newHitEVs = ComputeSplitHitHandEV(cStrat, paircard, upcard, hands, index, basecard, newNetpResProb)
                            'The stand and push ev's are actually holding the hit and hitpush ev's
                            tempEV = newHitEVs.StandEV(upcard)
                            cStrat.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), hands).HitEV(upcard) = tempEV
                            cStrat.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), hands).HitPushEV(upcard) = tempPush
                            EV += prob * tempEV
                            push += prob * tempPush
                            If (BBO Or OBBO Or AOBBO) And ((upcard = 1 And Not CheckAce) Or (upcard = 10 And Not CheckTen)) Then
                                BBOEV += prob * newHitEVs.BBOEV(upcard)
                                OBBOEV += prob * newHitEVs.OBBOEV(upcard)

                                'CEV = (UCEV - pBJ * netEVBJ) / (1 - pBJ)
                                CEV += cProb * newHitEVs.CEV(upcard)
                            End If
                        Case BJCAGlobalsClass.Strat.R
                            newEVs = ComputeSurrenderHand(CurrentShoe.Hand, cStrat.HandEVs(index).RPostallowed(upcard), upcard)
                            EV += prob * newEVs.SurrEV(upcard)
                            If (BBO Or OBBO Or AOBBO) And ((upcard = 1 And Not CheckAce) Or (upcard = 10 And Not CheckTen)) Then
                                'ENHC = (1-p(BJ))*(EV|no dBJ) + p(BJ)*((1-sum(p(Res))*(-1) + sum(p(Res)*EV(Res|dBJ))
                                'Non-Doubled
                                'Note that pRes = 1 or 0 and BBO = ENHC when resolved
                                'BBO = (1-p(BJ))*(EV|no dBJ) + p(BJ)*((1-sum(p(Res))*(0) + sum(p(Res)*EV(Res|dBJ))
                                'BBO = ENHC + p(BJ)*(1-sum(p(Res))*(1)
                                'OBBO = (1-p(BJ))*(EV|no dBJ) + p(BJ)*((1-sum(p(Res))*(-1) + sum(p(Res)*EV(Res|dBJ))
                                'OBBO = ENHC
                                If upcard = 1 And Not CheckAce Then
                                    bjadjprob = CardProb(10, 0)
                                ElseIf upcard = 10 And Not CheckTen Then
                                    bjadjprob = CardProb(1, 0)
                                Else
                                    bjadjprob = 0
                                End If
                                If cStrat.HandEVs(index).RPostallowed(upcard) = C.Surr.ES Then
                                    cStrat.SplitStateEVs(paircard, upcard).PResCards(hands, basecard) += newNetpResProb
                                    cStrat.SplitStateEVs(paircard, upcard).PResEVCards(hands, basecard) += newNetpResProb * SurrDBJPays
                                    pRes = 1
                                Else
                                    pRes = 0
                                End If
                                If EV <> 0 Then
                                    BBOEV += prob * (newEVs.SurrEV(upcard) + bjadjprob * (1 - pRes))
                                    OBBOEV += prob * newEVs.SurrEV(upcard)

                                    'CEV = (UCEV - pBJ * netEVBJ) / (1 - pBJ)
                                    CEV += cProb * (newEVs.SurrEV(upcard) - bjadjprob * SurrDBJPays) / (1 - bjadjprob)
                                End If
                            End If
                    End Select
                    UndealPCard(card)
                End If  'Busted
            End If  'Currentshoe(card)
        Next card

        newHitEVs.StandEV(upcard) = EV

        If (BBO Or OBBO Or AOBBO) And ((upcard = 1 And Not CheckAce) Or (upcard = 10 And Not CheckTen)) Then
            'Hold BBO and OBBO values in Dealerprobs
            newHitEVs.BBOEV(upcard) = BBOEV
            newHitEVs.OBBOEV(upcard) = OBBOEV
            newHitEVs.CEV(upcard) = CEV
        End If

        Return newHitEVs

    End Function

    Private Sub ComputeSplitEVs(ByRef cStrat As BJCAStrategyClass, ByVal paircard As Integer, ByVal upcard As Integer)
        Dim EV As Double
        Dim EV2 As Double
        Dim EV3 As Double
        Dim EV4 As Double
        Dim bjadjprob As Double
        Dim prob As Double
        Dim p1 As Double
        Dim p2 As Double

        'Compute the SPL's now
        If (BBO Or OBBO Or AOBBO) Then
            If (upcard = 1 And Not CheckAce) Then
                bjadjprob = CardProb(10, 0)
            ElseIf (upcard = 10 And Not CheckTen) Then
                bjadjprob = CardProb(1, 0)
            Else
                bjadjprob = 0
            End If
        End If

        'The first EV's take into account the effects of removal for each N and P card.
        '   The second represent the actual shoe states when using a fixed strategy and not using OBBO/BBO.
        '   When using a fixed stratey and non-OBBO/BBO play, the EV's for the two final split ev's are identical
        '   BBO  = Sum(State UCEVs with loss bust) - (1 - Prob(All Bust))*pBJ
        '   OBBO = UCEV(Original Bet with Loss 1) + Sum(UCEV's other hands with Loss Bust)
        '   AustOBBO = Sum(State UCEVs with Loss 1)
        '   The shoe states that take into account each card removed are closer to the sims so they are used.

        'ENHC = (1-p(BJ))*(EV|no dBJ) + p(BJ)*((1-sum(p(Res))*(-1) + sum(p(Res)*EV(Res|dBJ))


        'BBO = (1-p(BJ))*(EV|no dBJ) + p(BJ)*((1-sum(p(Res))*(0) + sum(p(Res)*EV(Res|dBJ))
        'BBO = ENHC + p(BJ)*(1-sum(p(Res))*(1)
        'OBBO = (1-p(BJ))*(EV|no dBJ) + p(BJ)*((1-sum(p(Res))*(-1) + sum(p(Res)*EV(Res|dBJ))
        'OBBO = ENHC

        'Note that in all cases, when a player doubles and busts, the extra loss is autmoatically included
        '   above during the EVCards calculations as a shortcut.

        'Compute SPL1
        'xx
        '2*EV(x)
        If (BBO And ((upcard = 1 And Not CheckAce) Or (upcard = 10 And Not CheckTen))) Then
            EV = 2 * (cStrat.SplitStateEVs(paircard, upcard).CEVx(BJCAGlobalsClass.Hands.Full) * (1 - bjadjprob) + cStrat.SplitStateEVs(paircard, upcard).PResEVx(BJCAGlobalsClass.Hands.Full) * bjadjprob)
            EV -= (1 - cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.Full) * cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.Full)) * bjadjprob

            EV2 = 2 * (cStrat.SplitStateEVs(paircard, upcard).CEVx(BJCAGlobalsClass.Hands.Full) * (1 - bjadjprob) - cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.Full) * bjadjprob)
            EV2 -= (1 - cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.Full) * cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.Full)) * bjadjprob

            EV3 = 2 * cStrat.SplitStateEVs(paircard, upcard).BBOx(BJCAGlobalsClass.Hands.Full)
            EV3 -= (1 - cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.Full) * cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.Full)) * bjadjprob
        ElseIf (OBBO And ((upcard = 1 And Not CheckAce) Or (upcard = 10 And Not CheckTen))) Then
            EV = cStrat.SplitStateEVs(paircard, upcard).CEVx(BJCAGlobalsClass.Hands.Full) * (1 - bjadjprob) + ((1 - cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.Full)) * (-1) + cStrat.SplitStateEVs(paircard, upcard).PResEVx(BJCAGlobalsClass.Hands.Full)) * bjadjprob
            EV += cStrat.SplitStateEVs(paircard, upcard).CEVx(BJCAGlobalsClass.Hands.Full) * (1 - bjadjprob) + cStrat.SplitStateEVs(paircard, upcard).PResEVx(BJCAGlobalsClass.Hands.Full) * bjadjprob

            EV2 = cStrat.SplitStateEVs(paircard, upcard).OBBOx(BJCAGlobalsClass.Hands.Full)
            EV2 += cStrat.SplitStateEVs(paircard, upcard).CEVx(BJCAGlobalsClass.Hands.Full) * (1 - bjadjprob) - cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.Full) * bjadjprob

            EV3 = cStrat.SplitStateEVs(paircard, upcard).OBBOx(BJCAGlobalsClass.Hands.Full)
            EV3 += cStrat.SplitStateEVs(paircard, upcard).BBOx(BJCAGlobalsClass.Hands.Full)
        ElseIf (AOBBO And ((upcard = 1 And Not CheckAce) Or (upcard = 10 And Not CheckTen))) Then
            EV = 2 * (cStrat.SplitStateEVs(paircard, upcard).CEVx(BJCAGlobalsClass.Hands.Full) * (1 - bjadjprob) + ((1 - cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.Full)) * (-1) + cStrat.SplitStateEVs(paircard, upcard).PResEVx(BJCAGlobalsClass.Hands.Full)) * bjadjprob)

            EV2 = 2 * cStrat.SplitStateEVs(paircard, upcard).OBBOx(BJCAGlobalsClass.Hands.Full)
        Else
            EV = 2 * cStrat.SplitStateEVs(paircard, upcard).EVx(BJCAGlobalsClass.Hands.Full)
        End If
        cStrat.SplitStateEVs(paircard, upcard).SPL(1) = EV
        cStrat.SplitStateEVs(paircard, upcard).SPL2(1) = EV2
        cStrat.SplitStateEVs(paircard, upcard).SPL3(1) = EV3
        If SPL = 1 Then
            p1 = EV / 2
            p2 = EV / 2
        End If

        'Compute SPL2
        If SPL > 1 Then
            'NN
            '2*EV(N-N)
            'EV(N) + EV(N-N)
            If (BBO And ((upcard = 1 And Not CheckAce) Or (upcard = 10 And Not CheckTen))) Then
                EV = cStrat.SplitStateEVs(paircard, upcard).CEVN(BJCAGlobalsClass.Hands.Full) * (1 - bjadjprob) + cStrat.SplitStateEVs(paircard, upcard).PResEVN(BJCAGlobalsClass.Hands.Full) * bjadjprob
                EV += cStrat.SplitStateEVs(paircard, upcard).CEVN(BJCAGlobalsClass.Hands.N) * (1 - bjadjprob) + cStrat.SplitStateEVs(paircard, upcard).PResEVN(BJCAGlobalsClass.Hands.N) * bjadjprob
                EV -= (1 - cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.Full) * cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.N)) * bjadjprob

                EV2 = cStrat.SplitStateEVs(paircard, upcard).CEVN(BJCAGlobalsClass.Hands.Full) * (1 - bjadjprob) - cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.Full) * bjadjprob
                EV2 += cStrat.SplitStateEVs(paircard, upcard).CEVN(BJCAGlobalsClass.Hands.N) * (1 - bjadjprob) - cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.N) * bjadjprob
                EV2 -= (1 - cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.Full) * cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.N)) * bjadjprob

                EV3 = cStrat.SplitStateEVs(paircard, upcard).BBON(BJCAGlobalsClass.Hands.Full) + cStrat.SplitStateEVs(paircard, upcard).BBON(BJCAGlobalsClass.Hands.N)
                EV3 -= (1 - cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.Full) * cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.N)) * bjadjprob
            ElseIf (OBBO And ((upcard = 1 And Not CheckAce) Or (upcard = 10 And Not CheckTen))) Then
                EV = cStrat.SplitStateEVs(paircard, upcard).CEVN(BJCAGlobalsClass.Hands.Full) * (1 - bjadjprob) + ((1 - cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.Full)) * (-1) + cStrat.SplitStateEVs(paircard, upcard).PResEVN(BJCAGlobalsClass.Hands.Full)) * bjadjprob
                EV += cStrat.SplitStateEVs(paircard, upcard).CEVN(BJCAGlobalsClass.Hands.N) * (1 - bjadjprob) + cStrat.SplitStateEVs(paircard, upcard).PResEVN(BJCAGlobalsClass.Hands.N) * bjadjprob

                EV2 = cStrat.SplitStateEVs(paircard, upcard).OBBON(BJCAGlobalsClass.Hands.Full)
                EV2 += cStrat.SplitStateEVs(paircard, upcard).CEVN(BJCAGlobalsClass.Hands.N) * (1 - bjadjprob) - cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.N) * bjadjprob

                EV3 = cStrat.SplitStateEVs(paircard, upcard).OBBON(BJCAGlobalsClass.Hands.Full)
                EV3 += cStrat.SplitStateEVs(paircard, upcard).BBON(BJCAGlobalsClass.Hands.N)
            ElseIf (AOBBO And ((upcard = 1 And Not CheckAce) Or (upcard = 10 And Not CheckTen))) Then
                EV = cStrat.SplitStateEVs(paircard, upcard).CEVN(BJCAGlobalsClass.Hands.Full) * (1 - bjadjprob) + ((1 - cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.Full)) * (-1) + cStrat.SplitStateEVs(paircard, upcard).PResEVN(BJCAGlobalsClass.Hands.Full)) * bjadjprob
                EV += cStrat.SplitStateEVs(paircard, upcard).CEVN(BJCAGlobalsClass.Hands.N) * (1 - bjadjprob) + ((1 - cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.N)) * (-1) + cStrat.SplitStateEVs(paircard, upcard).PResEVN(BJCAGlobalsClass.Hands.N)) * bjadjprob

                EV2 = cStrat.SplitStateEVs(paircard, upcard).OBBON(BJCAGlobalsClass.Hands.Full)
                EV2 += cStrat.SplitStateEVs(paircard, upcard).OBBON(BJCAGlobalsClass.Hands.N)
            Else
                EV = 2 * cStrat.SplitStateEVs(paircard, upcard).EVN(BJCAGlobalsClass.Hands.N)
                '                ev2 = cStrat.SplitStateEVs(paircard, upcard).EVN(BJCAGlobalsClass.Hands.Full) + cStrat.SplitStateEVs(paircard, upcard).EVN(BJCAGlobalsClass.Hands.N)
            End If
            prob = (1 - SplitProbs(paircard, upcard).PxCards(BJCAGlobalsClass.Hands.Full, paircard)) * (1 - SplitProbs(paircard, upcard).PxCards(BJCAGlobalsClass.Hands.N, paircard))
            cStrat.SplitStateEVs(paircard, upcard).SPL(2) = prob * EV
            cStrat.SplitStateEVs(paircard, upcard).SPL2(2) = prob * EV2
            cStrat.SplitStateEVs(paircard, upcard).SPL3(2) = prob * EV3
            If SPL > 2 Then
                cStrat.SplitStateEVs(paircard, upcard).SPL(3) = prob * EV
                cStrat.SplitStateEVs(paircard, upcard).SPL2(3) = prob * EV2
                cStrat.SplitStateEVs(paircard, upcard).SPL3(3) = prob * EV3
            End If
            If SPL = 2 Or SPL = 3 Then
                p1 = prob * EV / 2
                p2 = prob * EV / 2
            End If

            'Pxxx
            '3*EV(x-P)
            If (BBO And ((upcard = 1 And Not CheckAce) Or (upcard = 10 And Not CheckTen))) Then
                EV = 3 * (cStrat.SplitStateEVs(paircard, upcard).CEVx(BJCAGlobalsClass.Hands.P) * (1 - bjadjprob) + cStrat.SplitStateEVs(paircard, upcard).PResEVx(BJCAGlobalsClass.Hands.P) * bjadjprob)
                EV -= (1 - cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.P) * cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.P) * cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.P)) * bjadjprob

                EV2 = 3 * (cStrat.SplitStateEVs(paircard, upcard).CEVx(BJCAGlobalsClass.Hands.P) * (1 - bjadjprob) - cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.P) * bjadjprob)
                EV2 -= (1 - cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.P) * cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.P) * cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.P)) * bjadjprob

                EV3 = 3 * cStrat.SplitStateEVs(paircard, upcard).BBOx(BJCAGlobalsClass.Hands.P)
                EV3 -= (1 - cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.P) * cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.P) * cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.P)) * bjadjprob
            ElseIf (OBBO And ((upcard = 1 And Not CheckAce) Or (upcard = 10 And Not CheckTen))) Then
                EV = cStrat.SplitStateEVs(paircard, upcard).CEVx(BJCAGlobalsClass.Hands.P) * (1 - bjadjprob) + ((1 - cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.P)) * (-1) + cStrat.SplitStateEVs(paircard, upcard).PResEVx(BJCAGlobalsClass.Hands.P)) * bjadjprob
                EV += 2 * (cStrat.SplitStateEVs(paircard, upcard).CEVx(BJCAGlobalsClass.Hands.P) * (1 - bjadjprob) + cStrat.SplitStateEVs(paircard, upcard).PResEVx(BJCAGlobalsClass.Hands.P) * bjadjprob)

                EV2 = cStrat.SplitStateEVs(paircard, upcard).OBBOx(BJCAGlobalsClass.Hands.P)
                EV2 += 2 * (cStrat.SplitStateEVs(paircard, upcard).CEVx(BJCAGlobalsClass.Hands.P) * (1 - bjadjprob) - cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.P) * bjadjprob)

                EV3 = cStrat.SplitStateEVs(paircard, upcard).OBBOx(BJCAGlobalsClass.Hands.P)
                EV3 += 2 * cStrat.SplitStateEVs(paircard, upcard).BBOx(BJCAGlobalsClass.Hands.P)
            ElseIf (AOBBO And ((upcard = 1 And Not CheckAce) Or (upcard = 10 And Not CheckTen))) Then
                EV = 3 * (cStrat.SplitStateEVs(paircard, upcard).CEVx(BJCAGlobalsClass.Hands.P) * (1 - bjadjprob) + ((1 - cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.P)) * (-1) + cStrat.SplitStateEVs(paircard, upcard).PResEVx(BJCAGlobalsClass.Hands.P)) * bjadjprob)

                EV2 = 3 * cStrat.SplitStateEVs(paircard, upcard).OBBOx(BJCAGlobalsClass.Hands.P)
            Else
                EV = 3 * cStrat.SplitStateEVs(paircard, upcard).EVx(BJCAGlobalsClass.Hands.P)
            End If
            prob = SplitProbs(paircard, upcard).PxCards(BJCAGlobalsClass.Hands.Full, paircard)
            cStrat.SplitStateEVs(paircard, upcard).SPL(2) += prob * EV
            cStrat.SplitStateEVs(paircard, upcard).SPL2(2) += prob * EV2
            cStrat.SplitStateEVs(paircard, upcard).SPL3(2) += prob * EV3
            If SPL = 2 Then
                p1 += prob * EV * 2 / 3
                p2 += prob * EV * 1 / 3
            End If

            'NPxx
            'EV(N-P) + 2*EV(x-PN)
            'EV(N) + 2*EV(x-PN)
            If (BBO And ((upcard = 1 And Not CheckAce) Or (upcard = 10 And Not CheckTen))) Then
                EV = cStrat.SplitStateEVs(paircard, upcard).CEVN(BJCAGlobalsClass.Hands.Full) * (1 - bjadjprob) + cStrat.SplitStateEVs(paircard, upcard).PResEVN(BJCAGlobalsClass.Hands.Full) * bjadjprob
                EV += 2 * (cStrat.SplitStateEVs(paircard, upcard).CEVx(BJCAGlobalsClass.Hands.PN) * (1 - bjadjprob) + cStrat.SplitStateEVs(paircard, upcard).PResEVx(BJCAGlobalsClass.Hands.PN) * bjadjprob)
                EV -= (1 - cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.Full) * cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.PN) * cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.PN)) * bjadjprob

                EV2 = cStrat.SplitStateEVs(paircard, upcard).CEVN(BJCAGlobalsClass.Hands.Full) * (1 - bjadjprob) - cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.Full) * bjadjprob
                EV2 += 2 * (cStrat.SplitStateEVs(paircard, upcard).CEVx(BJCAGlobalsClass.Hands.PN) * (1 - bjadjprob) - cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.PN) * bjadjprob)
                EV2 -= (1 - cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.Full) * cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.PN) * cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.PN)) * bjadjprob

                EV3 = cStrat.SplitStateEVs(paircard, upcard).BBON(BJCAGlobalsClass.Hands.Full) + 2 * cStrat.SplitStateEVs(paircard, upcard).BBOx(BJCAGlobalsClass.Hands.PN)
                EV3 -= (1 - cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.Full) * cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.PN) * cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.PN)) * bjadjprob
            ElseIf (OBBO And ((upcard = 1 And Not CheckAce) Or (upcard = 10 And Not CheckTen))) Then
                EV = cStrat.SplitStateEVs(paircard, upcard).CEVN(BJCAGlobalsClass.Hands.Full) * (1 - bjadjprob) + ((1 - cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.Full)) * (-1) + cStrat.SplitStateEVs(paircard, upcard).PResEVN(BJCAGlobalsClass.Hands.Full)) * bjadjprob
                EV += 2 * (cStrat.SplitStateEVs(paircard, upcard).CEVx(BJCAGlobalsClass.Hands.PN) * (1 - bjadjprob) + cStrat.SplitStateEVs(paircard, upcard).PResEVx(BJCAGlobalsClass.Hands.PN) * bjadjprob)

                EV2 = cStrat.SplitStateEVs(paircard, upcard).OBBON(BJCAGlobalsClass.Hands.Full)
                EV2 += 2 * (cStrat.SplitStateEVs(paircard, upcard).CEVx(BJCAGlobalsClass.Hands.PN) * (1 - bjadjprob) - cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.PN) * bjadjprob)

                EV3 = cStrat.SplitStateEVs(paircard, upcard).OBBON(BJCAGlobalsClass.Hands.Full)
                EV3 += 2 * cStrat.SplitStateEVs(paircard, upcard).BBOx(BJCAGlobalsClass.Hands.PN)
            ElseIf (AOBBO And ((upcard = 1 And Not CheckAce) Or (upcard = 10 And Not CheckTen))) Then
                EV = cStrat.SplitStateEVs(paircard, upcard).CEVN(BJCAGlobalsClass.Hands.Full) * (1 - bjadjprob) + ((1 - cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.Full)) * (-1) + cStrat.SplitStateEVs(paircard, upcard).PResEVN(BJCAGlobalsClass.Hands.Full)) * bjadjprob
                EV += 2 * (cStrat.SplitStateEVs(paircard, upcard).CEVx(BJCAGlobalsClass.Hands.PN) * (1 - bjadjprob) + ((1 - cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.PN)) * (-1) + cStrat.SplitStateEVs(paircard, upcard).PResEVx(BJCAGlobalsClass.Hands.PN)) * bjadjprob)

                EV2 = cStrat.SplitStateEVs(paircard, upcard).OBBON(BJCAGlobalsClass.Hands.Full)
                EV2 += 2 * cStrat.SplitStateEVs(paircard, upcard).OBBOx(BJCAGlobalsClass.Hands.PN)
            Else
                EV = cStrat.SplitStateEVs(paircard, upcard).EVN(BJCAGlobalsClass.Hands.P) + 2 * cStrat.SplitStateEVs(paircard, upcard).EVx(BJCAGlobalsClass.Hands.PN)
                '                ev2 = cStrat.SplitStateEVs(paircard, upcard).EVN(BJCAGlobalsClass.Hands.Full) + 2 * cStrat.SplitStateEVs(paircard, upcard).EVx(BJCAGlobalsClass.Hands.PN)
            End If
            prob = (1 - SplitProbs(paircard, upcard).PxCards(BJCAGlobalsClass.Hands.Full, paircard)) * SplitProbs(paircard, upcard).PxCards(BJCAGlobalsClass.Hands.N, paircard)
            cStrat.SplitStateEVs(paircard, upcard).SPL(2) += prob * EV
            cStrat.SplitStateEVs(paircard, upcard).SPL2(2) += prob * EV2
            cStrat.SplitStateEVs(paircard, upcard).SPL3(2) += prob * EV3
            If SPL = 2 Then
                p1 += prob * cStrat.SplitStateEVs(paircard, upcard).EVN(BJCAGlobalsClass.Hands.P)
                p2 += prob * 2 * cStrat.SplitStateEVs(paircard, upcard).EVx(BJCAGlobalsClass.Hands.PN)
            End If
        End If

        'Compute SPL3
        If SPL > 2 Then
            'NN Already taken care of above with SPL2
            'PNNN
            '3*EV(N-PNN)
            'EV(N-P) + EV(N-PN) + EV(N-PNN)
            If (BBO And ((upcard = 1 And Not CheckAce) Or (upcard = 10 And Not CheckTen))) Then
                EV = cStrat.SplitStateEVs(paircard, upcard).CEVN(BJCAGlobalsClass.Hands.P) * (1 - bjadjprob) + cStrat.SplitStateEVs(paircard, upcard).PResEVN(BJCAGlobalsClass.Hands.P) * bjadjprob
                EV += cStrat.SplitStateEVs(paircard, upcard).CEVN(BJCAGlobalsClass.Hands.PN) * (1 - bjadjprob) + cStrat.SplitStateEVs(paircard, upcard).PResEVN(BJCAGlobalsClass.Hands.PN) * bjadjprob
                EV += cStrat.SplitStateEVs(paircard, upcard).CEVN(BJCAGlobalsClass.Hands.PNN) * (1 - bjadjprob) + cStrat.SplitStateEVs(paircard, upcard).PResEVN(BJCAGlobalsClass.Hands.PNN) * bjadjprob
                EV -= (1 - cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.P) * cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.PN) * cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.PNN)) * bjadjprob

                EV2 = cStrat.SplitStateEVs(paircard, upcard).CEVN(BJCAGlobalsClass.Hands.P) * (1 - bjadjprob) - cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.P) * bjadjprob
                EV2 += cStrat.SplitStateEVs(paircard, upcard).CEVN(BJCAGlobalsClass.Hands.PN) * (1 - bjadjprob) - cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.PN) * bjadjprob
                EV2 += cStrat.SplitStateEVs(paircard, upcard).CEVN(BJCAGlobalsClass.Hands.PNN) * (1 - bjadjprob) - cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.PNN) * bjadjprob
                EV2 -= (1 - cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.P) * cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.PN) * cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.PNN)) * bjadjprob

                EV3 = cStrat.SplitStateEVs(paircard, upcard).BBON(BJCAGlobalsClass.Hands.P)
                EV3 += cStrat.SplitStateEVs(paircard, upcard).BBON(BJCAGlobalsClass.Hands.PN)
                EV3 += cStrat.SplitStateEVs(paircard, upcard).BBON(BJCAGlobalsClass.Hands.PNN)
                EV3 -= (1 - cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.P) * cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.PN) * cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.PNN)) * bjadjprob
            ElseIf (OBBO And ((upcard = 1 And Not CheckAce) Or (upcard = 10 And Not CheckTen))) Then
                EV = cStrat.SplitStateEVs(paircard, upcard).CEVN(BJCAGlobalsClass.Hands.P) * (1 - bjadjprob) + ((1 - cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.P)) * (-1) + cStrat.SplitStateEVs(paircard, upcard).PResEVN(BJCAGlobalsClass.Hands.P)) * bjadjprob
                EV += cStrat.SplitStateEVs(paircard, upcard).CEVN(BJCAGlobalsClass.Hands.PN) * (1 - bjadjprob) + cStrat.SplitStateEVs(paircard, upcard).PResEVN(BJCAGlobalsClass.Hands.PN) * bjadjprob
                EV += cStrat.SplitStateEVs(paircard, upcard).CEVN(BJCAGlobalsClass.Hands.PNN) * (1 - bjadjprob) + cStrat.SplitStateEVs(paircard, upcard).PResEVN(BJCAGlobalsClass.Hands.PNN) * bjadjprob

                EV2 = cStrat.SplitStateEVs(paircard, upcard).OBBON(BJCAGlobalsClass.Hands.P)
                EV2 += cStrat.SplitStateEVs(paircard, upcard).CEVN(BJCAGlobalsClass.Hands.PN) * (1 - bjadjprob) - cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.PN) * bjadjprob
                EV2 += cStrat.SplitStateEVs(paircard, upcard).CEVN(BJCAGlobalsClass.Hands.PNN) * (1 - bjadjprob) - cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.PNN) * bjadjprob

                EV3 = cStrat.SplitStateEVs(paircard, upcard).OBBON(BJCAGlobalsClass.Hands.P)
                EV3 += cStrat.SplitStateEVs(paircard, upcard).BBON(BJCAGlobalsClass.Hands.PN)
                EV3 += cStrat.SplitStateEVs(paircard, upcard).BBON(BJCAGlobalsClass.Hands.PNN)
            ElseIf (AOBBO And ((upcard = 1 And Not CheckAce) Or (upcard = 10 And Not CheckTen))) Then
                EV = cStrat.SplitStateEVs(paircard, upcard).CEVN(BJCAGlobalsClass.Hands.P) * (1 - bjadjprob) + ((1 - cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.P)) * (-1) + cStrat.SplitStateEVs(paircard, upcard).PResEVN(BJCAGlobalsClass.Hands.P)) * bjadjprob
                EV += cStrat.SplitStateEVs(paircard, upcard).CEVN(BJCAGlobalsClass.Hands.PN) * (1 - bjadjprob) + ((1 - cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.PN)) * (-1) + cStrat.SplitStateEVs(paircard, upcard).PResEVN(BJCAGlobalsClass.Hands.PN)) * bjadjprob
                EV += cStrat.SplitStateEVs(paircard, upcard).CEVN(BJCAGlobalsClass.Hands.PNN) * (1 - bjadjprob) + ((1 - cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.PNN)) * (-1) + cStrat.SplitStateEVs(paircard, upcard).PResEVN(BJCAGlobalsClass.Hands.PNN)) * bjadjprob

                EV2 = cStrat.SplitStateEVs(paircard, upcard).OBBON(BJCAGlobalsClass.Hands.P)
                EV2 += cStrat.SplitStateEVs(paircard, upcard).OBBON(BJCAGlobalsClass.Hands.PN)
                EV2 += cStrat.SplitStateEVs(paircard, upcard).OBBON(BJCAGlobalsClass.Hands.PNN)
            Else
                EV = 3 * cStrat.SplitStateEVs(paircard, upcard).EVN(BJCAGlobalsClass.Hands.PNN)
                '                ev2 = cStrat.SplitStateEVs(paircard, upcard).EVN(BJCAGlobalsClass.Hands.P) + cStrat.SplitStateEVs(paircard, upcard).EVN(BJCAGlobalsClass.Hands.PN) + cStrat.SplitStateEVs(paircard, upcard).EVN(BJCAGlobalsClass.Hands.PNN)
            End If
            prob = SplitProbs(paircard, upcard).PxCards(BJCAGlobalsClass.Hands.Full, paircard) * (1 - SplitProbs(paircard, upcard).PxCards(BJCAGlobalsClass.Hands.P, paircard)) * (1 - SplitProbs(paircard, upcard).PxCards(BJCAGlobalsClass.Hands.PN, paircard)) * (1 - SplitProbs(paircard, upcard).PxCards(BJCAGlobalsClass.Hands.PNN, paircard))
            cStrat.SplitStateEVs(paircard, upcard).SPL(3) += prob * EV
            cStrat.SplitStateEVs(paircard, upcard).SPL2(3) += prob * EV2
            cStrat.SplitStateEVs(paircard, upcard).SPL3(3) += prob * EV3
            '                If spl > 3 Then cStrat.SplitStateEVs(paircard, upcard).SPL(4) = cStrat.SplitStateEVs(paircard, upcard).SPL(4) + prob * ev
            If SPL = 3 Then
                p1 += prob * EV * 2 / 3
                p2 += prob * EV * 1 / 3
            End If

            'NPNN
            '3*EV(N-PNN)
            'EV(N) + EV(N-PN) + EV(N-PNN)
            If (BBO And ((upcard = 1 And Not CheckAce) Or (upcard = 10 And Not CheckTen))) Then
                EV = cStrat.SplitStateEVs(paircard, upcard).CEVN(BJCAGlobalsClass.Hands.Full) * (1 - bjadjprob) + cStrat.SplitStateEVs(paircard, upcard).PResEVN(BJCAGlobalsClass.Hands.Full) * bjadjprob
                EV += cStrat.SplitStateEVs(paircard, upcard).CEVN(BJCAGlobalsClass.Hands.PN) * (1 - bjadjprob) + cStrat.SplitStateEVs(paircard, upcard).PResEVN(BJCAGlobalsClass.Hands.PN) * bjadjprob
                EV += cStrat.SplitStateEVs(paircard, upcard).CEVN(BJCAGlobalsClass.Hands.PNN) * (1 - bjadjprob) + cStrat.SplitStateEVs(paircard, upcard).PResEVN(BJCAGlobalsClass.Hands.PNN) * bjadjprob
                EV -= (1 - cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.Full) * cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.PN) * cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.PNN)) * bjadjprob

                EV2 = cStrat.SplitStateEVs(paircard, upcard).CEVN(BJCAGlobalsClass.Hands.Full) * (1 - bjadjprob) - cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.Full) * bjadjprob
                EV2 += cStrat.SplitStateEVs(paircard, upcard).CEVN(BJCAGlobalsClass.Hands.PN) * (1 - bjadjprob) - cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.PN) * bjadjprob
                EV2 += cStrat.SplitStateEVs(paircard, upcard).CEVN(BJCAGlobalsClass.Hands.PNN) * (1 - bjadjprob) - cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.PNN) * bjadjprob
                EV2 -= (1 - cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.Full) * cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.PN) * cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.PNN)) * bjadjprob

                EV3 = cStrat.SplitStateEVs(paircard, upcard).BBON(BJCAGlobalsClass.Hands.Full)
                EV3 += cStrat.SplitStateEVs(paircard, upcard).BBON(BJCAGlobalsClass.Hands.PN)
                EV3 += cStrat.SplitStateEVs(paircard, upcard).BBON(BJCAGlobalsClass.Hands.PNN)
                EV3 -= (1 - cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.Full) * cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.PN) * cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.PNN)) * bjadjprob
            ElseIf (OBBO And ((upcard = 1 And Not CheckAce) Or (upcard = 10 And Not CheckTen))) Then
                EV = cStrat.SplitStateEVs(paircard, upcard).CEVN(BJCAGlobalsClass.Hands.Full) * (1 - bjadjprob) + ((1 - cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.Full)) * (-1) + cStrat.SplitStateEVs(paircard, upcard).PResEVN(BJCAGlobalsClass.Hands.Full)) * bjadjprob
                EV += cStrat.SplitStateEVs(paircard, upcard).CEVN(BJCAGlobalsClass.Hands.PN) * (1 - bjadjprob) + cStrat.SplitStateEVs(paircard, upcard).PResEVN(BJCAGlobalsClass.Hands.PN) * bjadjprob
                EV += cStrat.SplitStateEVs(paircard, upcard).CEVN(BJCAGlobalsClass.Hands.PNN) * (1 - bjadjprob) + cStrat.SplitStateEVs(paircard, upcard).PResEVN(BJCAGlobalsClass.Hands.PNN) * bjadjprob

                EV2 = cStrat.SplitStateEVs(paircard, upcard).OBBON(BJCAGlobalsClass.Hands.Full)
                EV2 += cStrat.SplitStateEVs(paircard, upcard).CEVN(BJCAGlobalsClass.Hands.PN) * (1 - bjadjprob) - cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.PN) * bjadjprob
                EV2 += cStrat.SplitStateEVs(paircard, upcard).CEVN(BJCAGlobalsClass.Hands.PNN) * (1 - bjadjprob) - cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.PNN) * bjadjprob

                EV3 = cStrat.SplitStateEVs(paircard, upcard).OBBON(BJCAGlobalsClass.Hands.Full)
                EV3 += cStrat.SplitStateEVs(paircard, upcard).BBON(BJCAGlobalsClass.Hands.PN)
                EV3 += cStrat.SplitStateEVs(paircard, upcard).BBON(BJCAGlobalsClass.Hands.PNN)
            ElseIf (AOBBO And ((upcard = 1 And Not CheckAce) Or (upcard = 10 And Not CheckTen))) Then
                EV = cStrat.SplitStateEVs(paircard, upcard).CEVN(BJCAGlobalsClass.Hands.Full) * (1 - bjadjprob) + ((1 - cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.Full)) * (-1) + cStrat.SplitStateEVs(paircard, upcard).PResEVN(BJCAGlobalsClass.Hands.Full)) * bjadjprob
                EV += cStrat.SplitStateEVs(paircard, upcard).CEVN(BJCAGlobalsClass.Hands.PN) * (1 - bjadjprob) + ((1 - cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.PN)) * (-1) + cStrat.SplitStateEVs(paircard, upcard).PResEVN(BJCAGlobalsClass.Hands.PN)) * bjadjprob
                EV += cStrat.SplitStateEVs(paircard, upcard).CEVN(BJCAGlobalsClass.Hands.PNN) * (1 - bjadjprob) + ((1 - cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.PNN)) * (-1) + cStrat.SplitStateEVs(paircard, upcard).PResEVN(BJCAGlobalsClass.Hands.PNN)) * bjadjprob

                EV2 = cStrat.SplitStateEVs(paircard, upcard).OBBON(BJCAGlobalsClass.Hands.Full)
                EV2 += cStrat.SplitStateEVs(paircard, upcard).OBBON(BJCAGlobalsClass.Hands.PN)
                EV2 += cStrat.SplitStateEVs(paircard, upcard).OBBON(BJCAGlobalsClass.Hands.PNN)
            Else
                EV = 3 * cStrat.SplitStateEVs(paircard, upcard).EVN(BJCAGlobalsClass.Hands.PNN)
                '                ev2 = cStrat.SplitStateEVs(paircard, upcard).EVN(BJCAGlobalsClass.Hands.Full) + cStrat.SplitStateEVs(paircard, upcard).EVN(BJCAGlobalsClass.Hands.PN) + cStrat.SplitStateEVs(paircard, upcard).EVN(BJCAGlobalsClass.Hands.PNN)
            End If
            prob = (1 - SplitProbs(paircard, upcard).PxCards(BJCAGlobalsClass.Hands.Full, paircard)) * SplitProbs(paircard, upcard).PxCards(BJCAGlobalsClass.Hands.N, paircard) * (1 - SplitProbs(paircard, upcard).PxCards(BJCAGlobalsClass.Hands.PN, paircard)) * (1 - SplitProbs(paircard, upcard).PxCards(BJCAGlobalsClass.Hands.PNN, paircard))
            cStrat.SplitStateEVs(paircard, upcard).SPL(3) += prob * EV
            cStrat.SplitStateEVs(paircard, upcard).SPL2(3) += prob * EV2
            cStrat.SplitStateEVs(paircard, upcard).SPL3(3) += prob * EV3
            If SPL = 3 Then
                p1 += prob * EV * 1 / 3
                p2 += prob * EV * 2 / 3
            End If

            'PPxxxx
            '4*EV(x-PP)
            If (BBO And ((upcard = 1 And Not CheckAce) Or (upcard = 10 And Not CheckTen))) Then
                EV = 4 * (cStrat.SplitStateEVs(paircard, upcard).CEVx(BJCAGlobalsClass.Hands.PP) * (1 - bjadjprob) + cStrat.SplitStateEVs(paircard, upcard).PResEVx(BJCAGlobalsClass.Hands.PP) * bjadjprob)
                EV -= (1 - cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.PP) * cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.PP) * cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.PP) * cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.PP)) * bjadjprob

                EV2 = 4 * (cStrat.SplitStateEVs(paircard, upcard).CEVx(BJCAGlobalsClass.Hands.PP) * (1 - bjadjprob) - cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.PP) * bjadjprob)
                EV2 -= (1 - cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.PP) * cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.PP) * cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.PP) * cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.PP)) * bjadjprob

                EV3 = 4 * cStrat.SplitStateEVs(paircard, upcard).BBOx(BJCAGlobalsClass.Hands.PP)
                EV3 -= (1 - cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.PP) * cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.PP) * cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.PP) * cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.PP)) * bjadjprob
            ElseIf (OBBO And ((upcard = 1 And Not CheckAce) Or (upcard = 10 And Not CheckTen))) Then
                EV = cStrat.SplitStateEVs(paircard, upcard).CEVx(BJCAGlobalsClass.Hands.PP) * (1 - bjadjprob) + ((1 - cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.PP)) * (-1) + cStrat.SplitStateEVs(paircard, upcard).PResEVx(BJCAGlobalsClass.Hands.PP)) * bjadjprob
                EV += 3 * (cStrat.SplitStateEVs(paircard, upcard).CEVx(BJCAGlobalsClass.Hands.PP) * (1 - bjadjprob) + cStrat.SplitStateEVs(paircard, upcard).PResEVx(BJCAGlobalsClass.Hands.PP) * bjadjprob)

                EV2 = cStrat.SplitStateEVs(paircard, upcard).OBBOx(BJCAGlobalsClass.Hands.PP)
                EV2 += 3 * (cStrat.SplitStateEVs(paircard, upcard).CEVx(BJCAGlobalsClass.Hands.PP) * (1 - bjadjprob) - cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.PP) * bjadjprob)

                EV3 = cStrat.SplitStateEVs(paircard, upcard).OBBOx(BJCAGlobalsClass.Hands.PP)
                EV3 = EV + 3 * cStrat.SplitStateEVs(paircard, upcard).BBOx(BJCAGlobalsClass.Hands.PP)
            ElseIf (AOBBO And ((upcard = 1 And Not CheckAce) Or (upcard = 10 And Not CheckTen))) Then
                EV = 4 * (cStrat.SplitStateEVs(paircard, upcard).CEVx(BJCAGlobalsClass.Hands.PP) * (1 - bjadjprob) + ((1 - cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.PP)) * (-1) + cStrat.SplitStateEVs(paircard, upcard).PResEVx(BJCAGlobalsClass.Hands.PP)) * bjadjprob)

                EV2 = 4 * cStrat.SplitStateEVs(paircard, upcard).OBBOx(BJCAGlobalsClass.Hands.PP)
            Else
                EV = 4 * cStrat.SplitStateEVs(paircard, upcard).EVx(BJCAGlobalsClass.Hands.PP)
            End If
            prob = SplitProbs(paircard, upcard).PxCards(BJCAGlobalsClass.Hands.Full, paircard) * SplitProbs(paircard, upcard).PxCards(BJCAGlobalsClass.Hands.P, paircard)
            cStrat.SplitStateEVs(paircard, upcard).SPL(3) += prob * EV
            cStrat.SplitStateEVs(paircard, upcard).SPL2(3) += prob * EV2
            cStrat.SplitStateEVs(paircard, upcard).SPL3(3) += prob * EV3
            If SPL = 3 Then
                p1 += prob * EV * 3 / 4
                p2 += prob * EV * 1 / 4
            End If

            'PNPxxx
            'EV(N-PP) + 3*EV(x-PPN)
            'EV(N-P) + 3*EV(x-PPN)
            If (BBO And ((upcard = 1 And Not CheckAce) Or (upcard = 10 And Not CheckTen))) Then
                EV = cStrat.SplitStateEVs(paircard, upcard).CEVN(BJCAGlobalsClass.Hands.P) * (1 - bjadjprob) + cStrat.SplitStateEVs(paircard, upcard).PResEVN(BJCAGlobalsClass.Hands.P) * bjadjprob
                EV += 3 * (cStrat.SplitStateEVs(paircard, upcard).CEVx(BJCAGlobalsClass.Hands.PPN) * (1 - bjadjprob) + cStrat.SplitStateEVs(paircard, upcard).PResEVx(BJCAGlobalsClass.Hands.PPN) * bjadjprob)
                EV -= (1 - cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.P) * cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.PPN) * cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.PPN) * cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.PPN)) * bjadjprob

                EV2 = cStrat.SplitStateEVs(paircard, upcard).CEVN(BJCAGlobalsClass.Hands.P) * (1 - bjadjprob) - cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.P) * bjadjprob
                EV2 += 3 * (cStrat.SplitStateEVs(paircard, upcard).CEVx(BJCAGlobalsClass.Hands.PPN) * (1 - bjadjprob) - cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.PPN) * bjadjprob)
                EV2 -= (1 - cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.P) * cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.PPN) * cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.PPN) * cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.PPN)) * bjadjprob

                EV3 = cStrat.SplitStateEVs(paircard, upcard).BBON(BJCAGlobalsClass.Hands.P) + 3 * cStrat.SplitStateEVs(paircard, upcard).BBOx(BJCAGlobalsClass.Hands.PPN)
                EV3 -= (1 - cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.P) * cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.PPN) * cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.PPN) * cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.PPN)) * bjadjprob
            ElseIf (OBBO And ((upcard = 1 And Not CheckAce) Or (upcard = 10 And Not CheckTen))) Then
                EV = cStrat.SplitStateEVs(paircard, upcard).CEVN(BJCAGlobalsClass.Hands.P) * (1 - bjadjprob) + ((1 - cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.P)) * (-1) + cStrat.SplitStateEVs(paircard, upcard).PResEVN(BJCAGlobalsClass.Hands.P)) * bjadjprob
                EV += 3 * (cStrat.SplitStateEVs(paircard, upcard).CEVx(BJCAGlobalsClass.Hands.PPN) * (1 - bjadjprob) + cStrat.SplitStateEVs(paircard, upcard).PResEVx(BJCAGlobalsClass.Hands.PPN) * bjadjprob)

                EV2 = cStrat.SplitStateEVs(paircard, upcard).OBBON(BJCAGlobalsClass.Hands.P)
                EV2 += 3 * (cStrat.SplitStateEVs(paircard, upcard).CEVx(BJCAGlobalsClass.Hands.PPN) * (1 - bjadjprob) - cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.PPN) * bjadjprob)

                EV3 = cStrat.SplitStateEVs(paircard, upcard).OBBON(BJCAGlobalsClass.Hands.P)
                EV3 += 3 * cStrat.SplitStateEVs(paircard, upcard).BBOx(BJCAGlobalsClass.Hands.PPN)
            ElseIf (AOBBO And ((upcard = 1 And Not CheckAce) Or (upcard = 10 And Not CheckTen))) Then
                EV = cStrat.SplitStateEVs(paircard, upcard).CEVN(BJCAGlobalsClass.Hands.P) * (1 - bjadjprob) + ((1 - cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.P)) * (-1) + cStrat.SplitStateEVs(paircard, upcard).PResEVN(BJCAGlobalsClass.Hands.P)) * bjadjprob
                EV += 3 * (cStrat.SplitStateEVs(paircard, upcard).CEVx(BJCAGlobalsClass.Hands.PPN) * (1 - bjadjprob) + ((1 - cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.PPN)) * (-1) + cStrat.SplitStateEVs(paircard, upcard).PResEVx(BJCAGlobalsClass.Hands.PPN)) * bjadjprob)

                EV2 = cStrat.SplitStateEVs(paircard, upcard).OBBON(BJCAGlobalsClass.Hands.P)
                EV2 += 3 * cStrat.SplitStateEVs(paircard, upcard).OBBOx(BJCAGlobalsClass.Hands.PPN)
            Else
                EV = cStrat.SplitStateEVs(paircard, upcard).EVN(BJCAGlobalsClass.Hands.PP) + 3 * cStrat.SplitStateEVs(paircard, upcard).EVx(BJCAGlobalsClass.Hands.PPN)
                '                ev2 = cStrat.SplitStateEVs(paircard, upcard).EVN(BJCAGlobalsClass.Hands.P) + 3 * cStrat.SplitStateEVs(paircard, upcard).EVx(BJCAGlobalsClass.Hands.PPN)
            End If
            prob = SplitProbs(paircard, upcard).PxCards(BJCAGlobalsClass.Hands.Full, paircard) * (1 - SplitProbs(paircard, upcard).PxCards(BJCAGlobalsClass.Hands.P, paircard)) * SplitProbs(paircard, upcard).PxCards(BJCAGlobalsClass.Hands.PN, paircard)
            cStrat.SplitStateEVs(paircard, upcard).SPL(3) += prob * EV
            cStrat.SplitStateEVs(paircard, upcard).SPL2(3) += prob * EV2
            cStrat.SplitStateEVs(paircard, upcard).SPL3(3) += prob * EV3
            If SPL = 3 Then
                p1 += prob * (cStrat.SplitStateEVs(paircard, upcard).EVN(BJCAGlobalsClass.Hands.PP) + cStrat.SplitStateEVs(paircard, upcard).EVx(BJCAGlobalsClass.Hands.PPN))
                p2 += prob * 2 * cStrat.SplitStateEVs(paircard, upcard).EVx(BJCAGlobalsClass.Hands.PPN)
            End If

            'NPPxxx
            'EV(N-PP) + 3*EV(x-PPN)
            'EV(N) + 3*EV(x-PPN)
            If (BBO And ((upcard = 1 And Not CheckAce) Or (upcard = 10 And Not CheckTen))) Then
                EV = cStrat.SplitStateEVs(paircard, upcard).CEVN(BJCAGlobalsClass.Hands.Full) * (1 - bjadjprob) + cStrat.SplitStateEVs(paircard, upcard).PResEVN(BJCAGlobalsClass.Hands.Full) * bjadjprob
                EV += 3 * (cStrat.SplitStateEVs(paircard, upcard).CEVx(BJCAGlobalsClass.Hands.PPN) * (1 - bjadjprob) + cStrat.SplitStateEVs(paircard, upcard).PResEVx(BJCAGlobalsClass.Hands.PPN) * bjadjprob)
                EV -= (1 - cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.Full) * cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.PPN) * cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.PPN) * cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.PPN)) * bjadjprob

                EV2 = cStrat.SplitStateEVs(paircard, upcard).CEVN(BJCAGlobalsClass.Hands.Full) * (1 - bjadjprob) - cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.Full) * bjadjprob
                EV2 += 3 * (cStrat.SplitStateEVs(paircard, upcard).CEVx(BJCAGlobalsClass.Hands.PPN) * (1 - bjadjprob) - cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.PPN) * bjadjprob)
                EV2 -= (1 - cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.Full) * cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.PPN) * cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.PPN) * cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.PPN)) * bjadjprob

                EV3 = cStrat.SplitStateEVs(paircard, upcard).BBON(BJCAGlobalsClass.Hands.Full) + 3 * cStrat.SplitStateEVs(paircard, upcard).BBOx(BJCAGlobalsClass.Hands.PPN)
                EV3 -= (1 - cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.Full) * cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.PPN) * cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.PPN) * cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.PPN)) * bjadjprob
            ElseIf (OBBO And ((upcard = 1 And Not CheckAce) Or (upcard = 10 And Not CheckTen))) Then
                EV = cStrat.SplitStateEVs(paircard, upcard).CEVN(BJCAGlobalsClass.Hands.Full) * (1 - bjadjprob) + ((1 - cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.Full)) * (-1) + cStrat.SplitStateEVs(paircard, upcard).PResEVN(BJCAGlobalsClass.Hands.Full)) * bjadjprob
                EV += 3 * (cStrat.SplitStateEVs(paircard, upcard).CEVx(BJCAGlobalsClass.Hands.PPN) * (1 - bjadjprob) + cStrat.SplitStateEVs(paircard, upcard).PResEVx(BJCAGlobalsClass.Hands.PPN) * bjadjprob)

                EV2 = cStrat.SplitStateEVs(paircard, upcard).OBBON(BJCAGlobalsClass.Hands.Full)
                EV2 += 3 * (cStrat.SplitStateEVs(paircard, upcard).CEVx(BJCAGlobalsClass.Hands.PPN) * (1 - bjadjprob) - cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.PPN) * bjadjprob)

                EV3 = cStrat.SplitStateEVs(paircard, upcard).OBBON(BJCAGlobalsClass.Hands.Full)
                EV3 += 3 * cStrat.SplitStateEVs(paircard, upcard).BBOx(BJCAGlobalsClass.Hands.PPN)
            ElseIf (AOBBO And ((upcard = 1 And Not CheckAce) Or (upcard = 10 And Not CheckTen))) Then
                EV = cStrat.SplitStateEVs(paircard, upcard).CEVN(BJCAGlobalsClass.Hands.Full) * (1 - bjadjprob) + ((1 - cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.Full)) * (-1) + cStrat.SplitStateEVs(paircard, upcard).PResEVN(BJCAGlobalsClass.Hands.Full)) * bjadjprob
                EV += 3 * (cStrat.SplitStateEVs(paircard, upcard).CEVx(BJCAGlobalsClass.Hands.PPN) * (1 - bjadjprob) + ((1 - cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.PPN)) * (-1) + cStrat.SplitStateEVs(paircard, upcard).PResEVx(BJCAGlobalsClass.Hands.PPN)) * bjadjprob)

                EV2 = cStrat.SplitStateEVs(paircard, upcard).OBBON(BJCAGlobalsClass.Hands.Full)
                EV2 += 3 * cStrat.SplitStateEVs(paircard, upcard).OBBOx(BJCAGlobalsClass.Hands.PPN)
            Else
                EV = cStrat.SplitStateEVs(paircard, upcard).EVN(BJCAGlobalsClass.Hands.PP) + 3 * cStrat.SplitStateEVs(paircard, upcard).EVx(BJCAGlobalsClass.Hands.PPN)
                '                ev2 = cStrat.SplitStateEVs(paircard, upcard).EVN(BJCAGlobalsClass.Hands.Full) + 3 * cStrat.SplitStateEVs(paircard, upcard).EVx(BJCAGlobalsClass.Hands.PPN)
            End If
            prob = (1 - SplitProbs(paircard, upcard).PxCards(BJCAGlobalsClass.Hands.Full, paircard)) * SplitProbs(paircard, upcard).PxCards(BJCAGlobalsClass.Hands.N, paircard) * SplitProbs(paircard, upcard).PxCards(BJCAGlobalsClass.Hands.PN, paircard)
            cStrat.SplitStateEVs(paircard, upcard).SPL(3) += prob * EV
            cStrat.SplitStateEVs(paircard, upcard).SPL2(3) += prob * EV2
            cStrat.SplitStateEVs(paircard, upcard).SPL3(3) += prob * EV3
            If SPL = 3 Then
                p1 += prob * cStrat.SplitStateEVs(paircard, upcard).EVN(BJCAGlobalsClass.Hands.PP)
                p2 += prob * 3 * cStrat.SplitStateEVs(paircard, upcard).EVx(BJCAGlobalsClass.Hands.PPN)
            End If

            'PNNPxx
            '2*EV(N-PPN) + 2*EV(x-PPNN)
            'EV(N-P) + EV(N-PN) + 2*EV(x-PPNN)
            If (BBO And ((upcard = 1 And Not CheckAce) Or (upcard = 10 And Not CheckTen))) Then
                EV = cStrat.SplitStateEVs(paircard, upcard).CEVN(BJCAGlobalsClass.Hands.P) * (1 - bjadjprob) + cStrat.SplitStateEVs(paircard, upcard).PResEVN(BJCAGlobalsClass.Hands.P) * bjadjprob
                EV += cStrat.SplitStateEVs(paircard, upcard).CEVN(BJCAGlobalsClass.Hands.PN) * (1 - bjadjprob) + cStrat.SplitStateEVs(paircard, upcard).PResEVN(BJCAGlobalsClass.Hands.PN) * bjadjprob
                EV += 2 * (cStrat.SplitStateEVs(paircard, upcard).CEVx(BJCAGlobalsClass.Hands.PPNN) * (1 - bjadjprob) + cStrat.SplitStateEVs(paircard, upcard).PResEVx(BJCAGlobalsClass.Hands.PPNN) * bjadjprob)
                EV -= (1 - cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.P) * cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.PN) * cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.PPNN) * cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.PPNN)) * bjadjprob

                EV2 = cStrat.SplitStateEVs(paircard, upcard).CEVN(BJCAGlobalsClass.Hands.P) * (1 - bjadjprob) - cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.P) * bjadjprob
                EV2 += cStrat.SplitStateEVs(paircard, upcard).CEVN(BJCAGlobalsClass.Hands.PN) * (1 - bjadjprob) - cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.PN) * bjadjprob
                EV2 += 2 * (cStrat.SplitStateEVs(paircard, upcard).CEVx(BJCAGlobalsClass.Hands.PPNN) * (1 - bjadjprob) - cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.PPNN) * bjadjprob)
                EV2 -= (1 - cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.P) * cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.PN) * cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.PPNN) * cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.PPNN)) * bjadjprob

                EV3 = cStrat.SplitStateEVs(paircard, upcard).BBON(BJCAGlobalsClass.Hands.P)
                EV3 += cStrat.SplitStateEVs(paircard, upcard).BBON(BJCAGlobalsClass.Hands.PN)
                EV3 += 2 * cStrat.SplitStateEVs(paircard, upcard).BBOx(BJCAGlobalsClass.Hands.PPNN)
                EV3 -= (1 - cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.P) * cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.PN) * cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.PPNN) * cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.PPNN)) * bjadjprob
            ElseIf (OBBO And ((upcard = 1 And Not CheckAce) Or (upcard = 10 And Not CheckTen))) Then
                EV = cStrat.SplitStateEVs(paircard, upcard).CEVN(BJCAGlobalsClass.Hands.P) * (1 - bjadjprob) + ((1 - cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.P)) * (-1) + cStrat.SplitStateEVs(paircard, upcard).PResEVN(BJCAGlobalsClass.Hands.P)) * bjadjprob
                EV += cStrat.SplitStateEVs(paircard, upcard).CEVN(BJCAGlobalsClass.Hands.PN) * (1 - bjadjprob) + cStrat.SplitStateEVs(paircard, upcard).PResEVN(BJCAGlobalsClass.Hands.PN) * bjadjprob
                EV += 2 * (cStrat.SplitStateEVs(paircard, upcard).CEVx(BJCAGlobalsClass.Hands.PPNN) * (1 - bjadjprob) + cStrat.SplitStateEVs(paircard, upcard).PResEVx(BJCAGlobalsClass.Hands.PPNN) * bjadjprob)

                EV2 = cStrat.SplitStateEVs(paircard, upcard).OBBON(BJCAGlobalsClass.Hands.P)
                EV2 += cStrat.SplitStateEVs(paircard, upcard).CEVN(BJCAGlobalsClass.Hands.PN) * (1 - bjadjprob) - cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.PN) * bjadjprob
                EV2 += 2 * (cStrat.SplitStateEVs(paircard, upcard).CEVx(BJCAGlobalsClass.Hands.PPNN) * (1 - bjadjprob) - cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.PPNN) * bjadjprob)

                EV3 = cStrat.SplitStateEVs(paircard, upcard).OBBON(BJCAGlobalsClass.Hands.P)
                EV3 += cStrat.SplitStateEVs(paircard, upcard).BBON(BJCAGlobalsClass.Hands.PN)
                EV3 += 2 * cStrat.SplitStateEVs(paircard, upcard).BBOx(BJCAGlobalsClass.Hands.PPNN)
            ElseIf (AOBBO And ((upcard = 1 And Not CheckAce) Or (upcard = 10 And Not CheckTen))) Then
                EV = cStrat.SplitStateEVs(paircard, upcard).CEVN(BJCAGlobalsClass.Hands.P) * (1 - bjadjprob) + ((1 - cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.P)) * (-1) + cStrat.SplitStateEVs(paircard, upcard).PResEVN(BJCAGlobalsClass.Hands.P)) * bjadjprob
                EV = cStrat.SplitStateEVs(paircard, upcard).CEVN(BJCAGlobalsClass.Hands.PN) * (1 - bjadjprob) + ((1 - cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.PN)) * (-1) + cStrat.SplitStateEVs(paircard, upcard).PResEVN(BJCAGlobalsClass.Hands.PN)) * bjadjprob
                EV += 2 * (cStrat.SplitStateEVs(paircard, upcard).CEVx(BJCAGlobalsClass.Hands.PPNN) * (1 - bjadjprob) + ((1 - cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.PPNN)) * (-1) + cStrat.SplitStateEVs(paircard, upcard).PResEVx(BJCAGlobalsClass.Hands.PPNN)) * bjadjprob)

                EV2 = cStrat.SplitStateEVs(paircard, upcard).OBBON(BJCAGlobalsClass.Hands.P)
                EV2 += cStrat.SplitStateEVs(paircard, upcard).OBBON(BJCAGlobalsClass.Hands.PN)
                EV2 += 2 * cStrat.SplitStateEVs(paircard, upcard).OBBOx(BJCAGlobalsClass.Hands.PPNN)
            Else
                EV = 2 * cStrat.SplitStateEVs(paircard, upcard).EVN(BJCAGlobalsClass.Hands.PPN) + 2 * cStrat.SplitStateEVs(paircard, upcard).EVx(BJCAGlobalsClass.Hands.PPNN)
                '                ev2 = cStrat.SplitStateEVs(paircard, upcard).EVN(BJCAGlobalsClass.Hands.P) + cStrat.SplitStateEVs(paircard, upcard).EVN(BJCAGlobalsClass.Hands.PN) + 2 * cStrat.SplitStateEVs(paircard, upcard).EVx(BJCAGlobalsClass.Hands.PPNN)
            End If
            prob = SplitProbs(paircard, upcard).PxCards(BJCAGlobalsClass.Hands.Full, paircard) * (1 - SplitProbs(paircard, upcard).PxCards(BJCAGlobalsClass.Hands.P, paircard)) * (1 - SplitProbs(paircard, upcard).PxCards(BJCAGlobalsClass.Hands.PN, paircard)) * SplitProbs(paircard, upcard).PxCards(BJCAGlobalsClass.Hands.PNN, paircard)
            cStrat.SplitStateEVs(paircard, upcard).SPL(3) += prob * EV
            cStrat.SplitStateEVs(paircard, upcard).SPL2(3) += prob * EV2
            cStrat.SplitStateEVs(paircard, upcard).SPL3(3) += prob * EV3
            If SPL = 3 Then
                p1 += prob * (cStrat.SplitStateEVs(paircard, upcard).EVN(BJCAGlobalsClass.Hands.PPN) + 2 * cStrat.SplitStateEVs(paircard, upcard).EVx(BJCAGlobalsClass.Hands.PPNN))
                p2 += prob * cStrat.SplitStateEVs(paircard, upcard).EVN(BJCAGlobalsClass.Hands.PPN)
            End If

            'NPNPxx
            '2*EV(N-PPN) + 2*EV(x-PPNN)
            'EV(N) + EV(N-PN) + 2*EV(x-PPNN)
            If (BBO And ((upcard = 1 And Not CheckAce) Or (upcard = 10 And Not CheckTen))) Then
                EV = cStrat.SplitStateEVs(paircard, upcard).CEVN(BJCAGlobalsClass.Hands.Full) * (1 - bjadjprob) + cStrat.SplitStateEVs(paircard, upcard).PResEVN(BJCAGlobalsClass.Hands.Full) * bjadjprob
                EV += cStrat.SplitStateEVs(paircard, upcard).CEVN(BJCAGlobalsClass.Hands.PN) * (1 - bjadjprob) + cStrat.SplitStateEVs(paircard, upcard).PResEVN(BJCAGlobalsClass.Hands.PN) * bjadjprob
                EV += 2 * (cStrat.SplitStateEVs(paircard, upcard).CEVx(BJCAGlobalsClass.Hands.PPNN) * (1 - bjadjprob) + cStrat.SplitStateEVs(paircard, upcard).PResEVx(BJCAGlobalsClass.Hands.PPNN) * bjadjprob)
                EV -= (1 - cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.P) * cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.PN) * cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.PPNN) * cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.PPNN)) * bjadjprob

                EV2 = cStrat.SplitStateEVs(paircard, upcard).CEVN(BJCAGlobalsClass.Hands.Full) * (1 - bjadjprob) - cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.Full) * bjadjprob
                EV2 += cStrat.SplitStateEVs(paircard, upcard).CEVN(BJCAGlobalsClass.Hands.PN) * (1 - bjadjprob) - cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.PN) * bjadjprob
                EV2 += 2 * (cStrat.SplitStateEVs(paircard, upcard).CEVx(BJCAGlobalsClass.Hands.PPNN) * (1 - bjadjprob) - cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.PPNN) * bjadjprob)
                EV2 -= (1 - cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.Full) * cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.PN) * cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.PPNN) * cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.PPNN)) * bjadjprob

                EV3 = cStrat.SplitStateEVs(paircard, upcard).BBON(BJCAGlobalsClass.Hands.Full)
                EV3 += cStrat.SplitStateEVs(paircard, upcard).BBON(BJCAGlobalsClass.Hands.PN)
                EV3 += 2 * cStrat.SplitStateEVs(paircard, upcard).BBOx(BJCAGlobalsClass.Hands.PPNN)
                EV3 -= (1 - cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.Full) * cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.PN) * cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.PPNN) * cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.PPNN)) * bjadjprob
            ElseIf (OBBO And ((upcard = 1 And Not CheckAce) Or (upcard = 10 And Not CheckTen))) Then
                EV = cStrat.SplitStateEVs(paircard, upcard).CEVN(BJCAGlobalsClass.Hands.Full) * (1 - bjadjprob) + ((1 - cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.Full)) * (-1) + cStrat.SplitStateEVs(paircard, upcard).PResEVN(BJCAGlobalsClass.Hands.Full)) * bjadjprob
                EV += cStrat.SplitStateEVs(paircard, upcard).CEVN(BJCAGlobalsClass.Hands.PN) * (1 - bjadjprob) + cStrat.SplitStateEVs(paircard, upcard).PResEVN(BJCAGlobalsClass.Hands.PN) * bjadjprob
                EV += 2 * (cStrat.SplitStateEVs(paircard, upcard).CEVx(BJCAGlobalsClass.Hands.PPNN) * (1 - bjadjprob) + cStrat.SplitStateEVs(paircard, upcard).PResEVx(BJCAGlobalsClass.Hands.PPNN) * bjadjprob)

                EV2 = cStrat.SplitStateEVs(paircard, upcard).OBBON(BJCAGlobalsClass.Hands.Full)
                EV2 += cStrat.SplitStateEVs(paircard, upcard).CEVN(BJCAGlobalsClass.Hands.PN) * (1 - bjadjprob) - cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.PN) * bjadjprob
                EV2 += 2 * (cStrat.SplitStateEVs(paircard, upcard).CEVx(BJCAGlobalsClass.Hands.PPNN) * (1 - bjadjprob) - cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.PPNN) * bjadjprob)

                EV3 = cStrat.SplitStateEVs(paircard, upcard).OBBON(BJCAGlobalsClass.Hands.Full)
                EV3 += cStrat.SplitStateEVs(paircard, upcard).BBON(BJCAGlobalsClass.Hands.PN)
                EV3 += 2 * cStrat.SplitStateEVs(paircard, upcard).BBOx(BJCAGlobalsClass.Hands.PPNN)
            ElseIf (AOBBO And ((upcard = 1 And Not CheckAce) Or (upcard = 10 And Not CheckTen))) Then
                EV = cStrat.SplitStateEVs(paircard, upcard).CEVN(BJCAGlobalsClass.Hands.Full) * (1 - bjadjprob) + ((1 - cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.Full)) * (-1) + cStrat.SplitStateEVs(paircard, upcard).PResEVN(BJCAGlobalsClass.Hands.Full)) * bjadjprob
                EV = cStrat.SplitStateEVs(paircard, upcard).CEVN(BJCAGlobalsClass.Hands.PN) * (1 - bjadjprob) + ((1 - cStrat.SplitStateEVs(paircard, upcard).PResN(BJCAGlobalsClass.Hands.PN)) * (-1) + cStrat.SplitStateEVs(paircard, upcard).PResEVN(BJCAGlobalsClass.Hands.PN)) * bjadjprob
                EV += 2 * (cStrat.SplitStateEVs(paircard, upcard).CEVx(BJCAGlobalsClass.Hands.PPNN) * (1 - bjadjprob) + ((1 - cStrat.SplitStateEVs(paircard, upcard).PResx(BJCAGlobalsClass.Hands.PPNN)) * (-1) + cStrat.SplitStateEVs(paircard, upcard).PResEVx(BJCAGlobalsClass.Hands.PPNN)) * bjadjprob)

                EV2 = cStrat.SplitStateEVs(paircard, upcard).OBBON(BJCAGlobalsClass.Hands.Full)
                EV2 += cStrat.SplitStateEVs(paircard, upcard).OBBON(BJCAGlobalsClass.Hands.PN)
                EV2 += 2 * cStrat.SplitStateEVs(paircard, upcard).OBBOx(BJCAGlobalsClass.Hands.PPNN)
            Else
                EV = 2 * cStrat.SplitStateEVs(paircard, upcard).EVN(BJCAGlobalsClass.Hands.PPN) + 2 * cStrat.SplitStateEVs(paircard, upcard).EVx(BJCAGlobalsClass.Hands.PPNN)
                '                ev2 = cStrat.SplitStateEVs(paircard, upcard).EVN(BJCAGlobalsClass.Hands.Full) + cStrat.SplitStateEVs(paircard, upcard).EVN(BJCAGlobalsClass.Hands.PN) + 2 * cStrat.SplitStateEVs(paircard, upcard).EVx(BJCAGlobalsClass.Hands.PPNN)
            End If
            prob = (1 - SplitProbs(paircard, upcard).PxCards(BJCAGlobalsClass.Hands.Full, paircard)) * SplitProbs(paircard, upcard).PxCards(BJCAGlobalsClass.Hands.N, paircard) * (1 - SplitProbs(paircard, upcard).PxCards(BJCAGlobalsClass.Hands.PN, paircard)) * SplitProbs(paircard, upcard).PxCards(BJCAGlobalsClass.Hands.PNN, paircard)
            cStrat.SplitStateEVs(paircard, upcard).SPL(3) += prob * EV
            cStrat.SplitStateEVs(paircard, upcard).SPL2(3) += prob * EV2
            cStrat.SplitStateEVs(paircard, upcard).SPL3(3) += prob * EV3
            If SPL = 3 Then
                p1 += prob * cStrat.SplitStateEVs(paircard, upcard).EVN(BJCAGlobalsClass.Hands.PPN)
                p2 += prob * (cStrat.SplitStateEVs(paircard, upcard).EVN(BJCAGlobalsClass.Hands.PPN) + 2 * cStrat.SplitStateEVs(paircard, upcard).EVx(BJCAGlobalsClass.Hands.PPNN))
            End If

        End If

        '       If (p2 - p1) > 0.00000001 Then
        '            MsgBox("Paircard: " + CStr(paircard) + "  Upcard: " + CStr(upcard))
        '       End If


    End Sub

    Private Sub ComputeSplit(ByRef cStrat As BJCAStrategyClass, ByVal paircard As Integer, ByVal upcard As Integer)
        Dim index As Integer
        Dim i As Integer

        index = SplitIndex(paircard, upcard)
        If index > 0 Then
            CurrentShoe.Deal(paircard)  'Remove the extra pair card from the shoe
            DealPCard(paircard)
            CurrentShoe.Deal(upcard)    'Remove the upcard from the shoe

            'Reset the SplitHand values
            cStrat.SplitStateEVs(paircard, upcard).Empty()
            ComputeSplitHandsEV(cStrat, paircard, upcard)
            ComputeSplitEVs(cStrat, paircard, upcard)

            'Copy the split EVs into the hand EVs from the split state EVs
            If paircard = 1 And Not SMA Then
                cStrat.HandEVs(index).SplitEV(upcard) = cStrat.SplitStateEVs(paircard, upcard).SPL(1)
            Else
                cStrat.HandEVs(index).SplitEV(upcard) = cStrat.SplitStateEVs(paircard, upcard).SPL(SPL)
            End If
            For i = 1 To SPL
                cStrat.HandEVs(index).SPLEV(upcard, i) = cStrat.SplitStateEVs(paircard, upcard).SPL(i)
            Next

            CurrentShoe.Undeal(upcard)
            UndealPCard(paircard)
            CurrentShoe.Undeal(paircard)
        End If
    End Sub

    Private Function ComputeSplitDoubleCardEV(ByVal index As Integer, ByVal upcard As Integer, ByVal depth As Integer) As BJCAHandEVsClass
        Dim strat As Integer
        Dim card As Integer
        Dim prob As Double
        Dim EV As Double
        Dim push As Double
        Dim newIndex As Integer
        Dim tempEVs As New BJCAHandEVsClass
        Dim newEVs As New BJCAHandEVsClass

        'This function is only needed for RDA and DDR post-split
        'The CurrentShoe hand has the hand needed to calculate the necessary EVs.

        'The RDA and DDR Strategy will be the same as the PreSplit strategy so we only need to calculate the relevant EV
        strat = PlayerHands(index).HandEVs.DStrat(upcard)
        If strat = BJCAGlobalsClass.Strat.D And (Not RDAPS Or depth > RDDepth) Then
            strat = BJCAGlobalsClass.Strat.S
        End If
        If strat = BJCAGlobalsClass.Strat.R And Not DDRPS Then
            strat = BJCAGlobalsClass.Strat.S
        End If

        Select Case strat
            Case BJCAGlobalsClass.Strat.D
                EV = 0
                push = 0
                For card = 1 To 10
                    prob = CardProb(card, 0)
                    If prob > 0 Then
                        newIndex = PlayerHands(index).HitHand(card)
                        If newIndex > 0 Then
                            DealPCard(card)
                            tempEVs = ComputeSplitDoubleCardEV(newIndex, upcard, depth + 1)
                            EV = tempEVs.DStratEV(upcard)
                            push = tempEVs.DStratPushEV(upcard)
                            UndealPCard(card)
                        Else
                            EV = -1
                            push = 0
                        End If
                    End If
                    newEVs.DStratEV(upcard) += 2 * EV * prob
                    newEVs.DStratPushEV(upcard) += 2 * push * prob
                Next card
            Case BJCAGlobalsClass.Strat.R
                tempEVs.Empty()
                tempEVs = ComputeSurrenderHand(CurrentShoe.Hand, DDRType, upcard)
                newEVs.DStratEV(upcard) = tempEVs.SurrEV(upcard)
                newEVs.DStratPushEV(upcard) = 0
                If (BBO Or OBBO Or AOBBO) And DDRType = C.Surr.ES And ((upcard = 1 And Not CheckAce) Or (upcard = 10 And Not CheckTen)) Then
                    'Let the calling function know the hand was resolved before the BJ check
                    newEVs.BJStandEV(upcard) = 1
                End If
            Case Else   'Stand
                CurrentShoe.Undeal(upcard)
                CurrentShoe.Hand.Total = CurrentPHand.Total
                tempEVs = ComputeStandHand(CurrentShoe.Hand, upcard, upcard, False)
                newEVs.DStratEV(upcard) = tempEVs.StandEV(upcard)
                newEVs.DStratPushEV(upcard) = tempEVs.StandPushEV(upcard)
                CurrentShoe.Deal(upcard)
                If (BBO Or OBBO Or AOBBO) And PlayerHands(index).Hand.Total = 21 And P21AutoWin And ((upcard = 1 And Not CheckAce) Or (upcard = 10 And Not CheckTen)) Then
                    'Let the calling function know the hand was resolved before the BJ check
                    newEVs.BJStandEV(upcard) = 1
                End If
        End Select

        Return newEVs
    End Function

#End Region

#End Region

#Region " Strategy Methods "

#Region " General Strategy Methods "

    Public Sub ComputeStrategy(ByRef cStrat As BJCAStrategyClass)
        Dim upcard As Integer

        If cStrat.ComputeStrat Then
            For upcard = 1 To 10
                If UCAllowed(upcard) And CardProb(upcard, 0) > 0 Then
                    ComputeStrategyMaxEVMinus(cStrat, upcard)
                End If
            Next upcard
        End If
    End Sub

    Private Sub ResetStrategyEVs(ByRef cStrat As BJCAStrategyClass)
        Dim index As Integer
        Dim upcard As Integer
        Dim i As Integer


    End Sub

    Private Sub ComputeStrategies()
        Dim upcard As Integer

        'First figure out which hands are used in CD play
        For upcard = 1 To 10
            If UCAllowed(upcard) And CardProb(upcard, 0) > 0 Then

                DealDCard(upcard)
                GetHandsUsed(Opt, upcard, 1, True, True)
                UndealDCard(upcard)

            End If
        Next upcard

        If TD.ComputeStrat Then ComputeStrategy(TD)
        If TC.ComputeStrat Then ComputeStrategy(TC)
        If Forced.ComputeStrat Then ComputeStrategy(Forced)
    End Sub

    Private Sub HandsUsed(ByRef cStrat As BJCAStrategyClass, ByVal upcard As Integer, ByVal index As Integer, ByVal stratIndex As Integer)
        'Will play out the player's hands based on the Total Dependent strategy and increase the HandUsed index by 1
        '  stratindex is used to avoid an endless loop if a hand was not played for 1 round and then played again.
        Dim card As Integer
        Dim newIndex As Integer
        Dim strat As Integer

        For card = 1 To 10
            newIndex = PlayerHands(index).HitHand(card)
            If (CardProb(card, 0) > 0 And newIndex <> 0) Then
                DealPCard(card)
                'Check to see if hand has been used before - if so then increases multiplier.
                '  Otherwise it resets multiplier and the stratindex.
                If cStrat.HandEVs(newIndex).HandUsed(upcard) <> -1 Then
                    If cStrat.HandEVs(newIndex).HandUsed(upcard) = stratIndex Then
                        cStrat.HandEVs(newIndex).Multiplier(upcard) += 1
                    Else
                        cStrat.HandEVs(newIndex).HandUsed(upcard) = stratIndex
                        cStrat.HandEVs(newIndex).Multiplier(upcard) = 1
                    End If
                End If
                strat = cStrat.HandEVs(newIndex).EVs.Strat(upcard)
                If strat = BJCAGlobalsClass.Strat.H Or strat = BJCAGlobalsClass.Strat.P Or strat = BJCAGlobalsClass.Strat.PH Or (strat = BJCAGlobalsClass.Strat.D And Not cStrat.HandEVs(newIndex).DPreallowed(upcard)) Or (strat = BJCAGlobalsClass.Strat.R And Not cStrat.HandEVs(newIndex).RPreallowed(upcard)) Then
                    Call HandsUsed(cStrat, upcard, newIndex, stratIndex)
                End If
                UndealPCard(card)
            End If
        Next card

    End Sub

    Private Sub GetHandsUsed(ByRef cStrat As BJCAStrategyClass, ByVal upcard As Integer, ByVal stratIndex As Integer, ByVal resetHandsUsed As Boolean, Optional ByVal includeAll As Boolean = False)
        Dim card As Integer
        Dim index As Integer

        If resetHandsUsed Then
            'Remove any non-strategy determining hands from consideration
            For index = 1 To NumPHands
                If PlayerHands(index).Hand.NumCards < 2 Then
                    cStrat.HandEVs(index).HandUsed(upcard) = -1
                ElseIf Not includeAll And (cStrat.HandEVs(index).PreForced(upcard) Or PlayerHands(index).Hand.NumCards <= cStrat.NCardsCD) Then
                    cStrat.HandEVs(index).HandUsed(upcard) = -1
                Else
                    cStrat.HandEVs(index).HandUsed(upcard) = 0
                    cStrat.HandEVs(index).Multiplier(upcard) = 0
                End If
            Next index

            'See if Split is the Strategy - these are done separately because there is only one kind of each pair possible
            For card = 1 To 10
                If SplitIndex(card, upcard) > 0 Then
                    index = SplitIndex(card, upcard)
                    If Not includeAll And (cStrat.HandEVs(index).EVs.Strat(upcard) = BJCAGlobalsClass.Strat.P Or cStrat.HandEVs(index).EVs.Strat(upcard) = BJCAGlobalsClass.Strat.PS Or cStrat.HandEVs(index).EVs.Strat(upcard) = BJCAGlobalsClass.Strat.PD Or cStrat.HandEVs(index).EVs.Strat(upcard) = BJCAGlobalsClass.Strat.PR Or cStrat.HandEVs(index).EVs.Strat(upcard) = BJCAGlobalsClass.Strat.PH) Then
                        cStrat.HandEVs(index).HandUsed(upcard) = -1
                    End If
                End If
            Next card
        End If

        For card = 1 To 10
            If CardProb(card, 0) > 0 Then
                DealPCard(card)
                HandsUsed(cStrat, upcard, PlayerHands(0).HitHand(card), stratIndex)
                UndealPCard(card)
            End If
        Next card

    End Sub

    Private Sub ComputeStratHit(ByRef cStrat As BJCAStrategyClass, ByVal upcard As Integer)
        'useTD is true for TD and false for TC
        Dim Total As Integer

        For Total = 21 To 11 Step -1
            Call ComputeStratHitTotal(cStrat, Total, False, upcard)
        Next Total
        For Total = 21 To 12 Step -1
            Call ComputeStratHitTotal(cStrat, Total, True, upcard)
        Next Total
        For Total = 10 To 4 Step -1
            Call ComputeStratHitTotal(cStrat, Total, False, upcard)
        Next Total

    End Sub

    Private Sub ComputeStratHitTotal(ByRef cStrat As BJCAStrategyClass, ByVal Total As Integer, ByVal Soft As Boolean, ByVal upcard As Integer)
        Dim index As Integer
        Dim card As Integer
        Dim prob As Double
        Dim Strat As Integer
        Dim newStrat As Integer

        index = PlayerHandTotal(Total, Soft + 1)
        Do While index
            cStrat.HandEVs(index).EVs.HitEV(upcard) = 0
            cStrat.HandEVs(index).EVs.HitPushEV(upcard) = 0
            If PlayerHands(index).Hand.NumCards > 1 And HandPossible(PlayerHands(index).Hand) Then
                CurrentShoe.Deal(PlayerHands(index).Hand)
                For card = 1 To 10
                    prob = CardProb(card, upcard)
                    If prob > 0 Then
                        If PlayerHands(index).HitHand(card) > 0 Then
                            cStrat.HandEVs(index).EVs.HitEV(upcard) = cStrat.HandEVs(index).EVs.HitEV(upcard) + prob * (cStrat.HandEVs(PlayerHands(index).HitHand(card)).EVs.StratEV(upcard) + PlayerHands(PlayerHands(index).HitHand(card)).HandEVs.BonusEV(upcard))
                            cStrat.HandEVs(index).EVs.HitPushEV(upcard) = cStrat.HandEVs(index).EVs.HitPushEV(upcard) + prob * cStrat.HandEVs(PlayerHands(index).HitHand(card)).EVs.StratPushEV(upcard)
                        Else
                            cStrat.HandEVs(index).EVs.HitEV(upcard) = cStrat.HandEVs(index).EVs.HitEV(upcard) - prob
                        End If
                    End If
                Next card


                'Update strat ev
                If cStrat.HandEVs(index).PreForced(upcard) Then
                    Strat = cStrat.HandEVs(index).EVs.Strat(upcard)
                ElseIf cStrat.NCardsCD <> 0 And PlayerHands(index).Hand.NumCards <= cStrat.NCardsCD Then
                    Strat = cStrat.HandEVs(index).EVs.Strat(upcard)
                Else
                    Strat = cStrat.StratTD(PlayerHands(index).Hand.Total, PlayerHands(index).Hand.Soft + 1).Strat(upcard)
                End If
                If (Strat = BJCAGlobalsClass.Strat.DS And Not cStrat.HandEVs(index).DPreallowed(upcard)) Or (Strat = BJCAGlobalsClass.Strat.RS And Not cStrat.HandEVs(index).RPreallowed(upcard) > 0) Then
                    newStrat = BJCAGlobalsClass.Strat.S
                ElseIf (Strat = BJCAGlobalsClass.Strat.D And Not cStrat.HandEVs(index).DPreallowed(upcard)) Or (Strat = BJCAGlobalsClass.Strat.R And Not cStrat.HandEVs(index).RPreallowed(upcard) > 0) Then
                    newStrat = BJCAGlobalsClass.Strat.H
                Else
                    newStrat = Strat
                End If

                Select Case newStrat
                    Case BJCAGlobalsClass.Strat.S
                        cStrat.HandEVs(index).EVs.StratEV(upcard) = PlayerHands(index).HandEVs.StandEV(upcard)
                        cStrat.HandEVs(index).EVs.StratPushEV(upcard) = PlayerHands(index).HandEVs.StandPushEV(upcard)
                    Case BJCAGlobalsClass.Strat.D, BJCAGlobalsClass.Strat.DS
                        cStrat.HandEVs(index).EVs.StratEV(upcard) = PlayerHands(index).HandEVs.DEV(upcard)
                        cStrat.HandEVs(index).EVs.StratPushEV(upcard) = PlayerHands(index).HandEVs.DPushEV(upcard)
                    Case BJCAGlobalsClass.Strat.H
                        cStrat.HandEVs(index).EVs.StratEV(upcard) = cStrat.HandEVs(index).EVs.HitEV(upcard)
                        cStrat.HandEVs(index).EVs.StratPushEV(upcard) = cStrat.HandEVs(index).EVs.HitPushEV(upcard)
                    Case BJCAGlobalsClass.Strat.R, BJCAGlobalsClass.Strat.RS
                        cStrat.HandEVs(index).EVs.StratEV(upcard) = PlayerHands(index).HandEVs.SurrEV(upcard)
                        cStrat.HandEVs(index).EVs.StratPushEV(upcard) = 0
                End Select

                CurrentShoe.Undeal(PlayerHands(index).Hand)
            End If
            index = PlayerHands(index).NextHand
        Loop
    End Sub

    Private Function GetStratMaxEVHand(ByVal index As Integer, ByVal upcard As Integer, ByRef toStrat As BJCAStrategyClass, ByRef fromStrat As BJCAStrategyClass, Optional ByVal pre As Boolean = True, Optional ByVal includeSplits As Boolean = False) As BJCAHandStrategyEVsClass
        Dim newEvs As New BJCAHandStrategyEVsClass
        Dim card As Integer

        If pre And Not toStrat.HandEVs(index).PreForced(upcard) Then
            If toStrat.HandEVs(index).SPreallowed(upcard) Then
                newEvs.StratEV(upcard) = PlayerHands(index).HandEVs.StandEV(upcard)
                newEvs.StratPushEV(upcard) = PlayerHands(index).HandEVs.StandPushEV(upcard)
                newEvs.Strat(upcard) = BJCAGlobalsClass.Strat.S
            End If
            If toStrat.HandEVs(index).DPreallowed(upcard) Then
                If PlayerHands(index).HandEVs.DEV(upcard) > newEvs.StratEV(upcard) Then
                    newEvs.StratEV(upcard) = PlayerHands(index).HandEVs.DEV(upcard)
                    newEvs.StratPushEV(upcard) = PlayerHands(index).HandEVs.DPushEV(upcard)
                    newEvs.Strat(upcard) = BJCAGlobalsClass.Strat.D
                End If
            End If
            If toStrat.HandEVs(index).RPreallowed(upcard) Then
                If PlayerHands(index).HandEVs.SurrEV(upcard) > newEvs.StratEV(upcard) Then
                    newEvs.StratEV(upcard) = PlayerHands(index).HandEVs.SurrEV(upcard)
                    newEvs.StratPushEV(upcard) = 0
                    newEvs.Strat(upcard) = BJCAGlobalsClass.Strat.R
                End If
            End If
            If toStrat.HandEVs(index).HPreallowed(upcard) Then
                If fromStrat.HandEVs(index).EVs.HitEV(upcard) > newEvs.StratEV(upcard) Then
                    newEvs.StratEV(upcard) = fromStrat.HandEVs(index).EVs.HitEV(upcard)
                    newEvs.StratPushEV(upcard) = fromStrat.HandEVs(index).EVs.HitPushEV(upcard)
                    newEvs.Strat(upcard) = BJCAGlobalsClass.Strat.H
                End If
            End If
            If includeSplits And toStrat.HandEVs(index).PAllowed(upcard) Then
                For card = 1 To 10
                    If PlayerHands(index).Hand.Cards(card) = 2 Then Exit For
                Next card
                'Make sure the split is allowed for the game not just the hand
                If SplitIndex(card, upcard) > 0 Then
                    If fromStrat.HandEVs(index).SplitEV(upcard) > newEvs.StratEV(upcard) Then
                        newEvs.StratEV(upcard) = fromStrat.HandEVs(index).SplitEV(upcard)
                        If toStrat.NCardsCD = 0 Then
                            newEvs.Strat(upcard) = BJCAGlobalsClass.Strat.P
                        Else
                            Select Case newEvs.Strat(upcard)
                                Case BJCAGlobalsClass.Strat.S
                                    If toStrat.HandEVs(index).SPostallowed(upcard) Then
                                        newEvs.Strat(upcard) = BJCAGlobalsClass.Strat.PS
                                    ElseIf toStrat.HandEVs(index).RPostallowed(upcard) And (PlayerHands(index).HandEVs.SurrEV(upcard) > PlayerHands(index).HandEVs.DEV(upcard) Or Not toStrat.HandEVs(index).DPostallowed(upcard)) And (PlayerHands(index).HandEVs.SurrEV(upcard) > fromStrat.HandEVs(index).EVs.HitEV(upcard) Or toStrat.HandEVs(index).HPostallowed(upcard)) Then
                                        newEvs.Strat(upcard) = BJCAGlobalsClass.Strat.PR
                                    ElseIf toStrat.HandEVs(index).DPostallowed(upcard) And PlayerHands(index).HandEVs.DEV(upcard) > fromStrat.HandEVs(index).EVs.HitEV(upcard) Then
                                        newEvs.Strat(upcard) = BJCAGlobalsClass.Strat.PD
                                    Else
                                        newEvs.Strat(upcard) = BJCAGlobalsClass.Strat.PH
                                    End If
                                Case BJCAGlobalsClass.Strat.D
                                    If toStrat.HandEVs(index).DPostallowed(upcard) Then
                                        newEvs.Strat(upcard) = BJCAGlobalsClass.Strat.PD
                                    ElseIf toStrat.HandEVs(index).RPostallowed(upcard) And (PlayerHands(index).HandEVs.SurrEV(upcard) > PlayerHands(index).HandEVs.StandEV(upcard) Or Not toStrat.HandEVs(index).SPostallowed(upcard)) And (PlayerHands(index).HandEVs.SurrEV(upcard) > fromStrat.HandEVs(index).EVs.HitEV(upcard) Or toStrat.HandEVs(index).HPostallowed(upcard)) Then
                                        newEvs.Strat(upcard) = BJCAGlobalsClass.Strat.PR
                                    ElseIf toStrat.HandEVs(index).SPostallowed(upcard) And PlayerHands(index).HandEVs.StandEV(upcard) > fromStrat.HandEVs(index).EVs.HitEV(upcard) Then
                                        newEvs.Strat(upcard) = BJCAGlobalsClass.Strat.PS
                                    Else
                                        newEvs.Strat(upcard) = BJCAGlobalsClass.Strat.PH
                                    End If
                                Case BJCAGlobalsClass.Strat.H
                                    If toStrat.HandEVs(index).HPostallowed(upcard) Then
                                        newEvs.Strat(upcard) = BJCAGlobalsClass.Strat.PH
                                    ElseIf toStrat.HandEVs(index).RPostallowed(upcard) And (PlayerHands(index).HandEVs.SurrEV(upcard) > PlayerHands(index).HandEVs.DEV(upcard) Or Not toStrat.HandEVs(index).DPostallowed(upcard)) And (PlayerHands(index).HandEVs.SurrEV(upcard) > PlayerHands(index).HandEVs.StandEV(upcard) Or toStrat.HandEVs(index).SPostallowed(upcard)) Then
                                        newEvs.Strat(upcard) = BJCAGlobalsClass.Strat.PR
                                    ElseIf toStrat.HandEVs(index).DPostallowed(upcard) And PlayerHands(index).HandEVs.DEV(upcard) > PlayerHands(index).HandEVs.StandEV(upcard) Then
                                        newEvs.Strat(upcard) = BJCAGlobalsClass.Strat.PD
                                    Else
                                        newEvs.Strat(upcard) = BJCAGlobalsClass.Strat.PS
                                    End If
                                Case BJCAGlobalsClass.Strat.R
                                    If toStrat.HandEVs(index).RPostallowed(upcard) Then
                                        newEvs.Strat(upcard) = BJCAGlobalsClass.Strat.PR
                                    ElseIf toStrat.HandEVs(index).DPostallowed(upcard) And (PlayerHands(index).HandEVs.DEV(upcard) > PlayerHands(index).HandEVs.StandEV(upcard) Or Not toStrat.HandEVs(index).SPostallowed(upcard)) And (PlayerHands(index).HandEVs.DEV(upcard) > fromStrat.HandEVs(index).EVs.HitEV(upcard) Or toStrat.HandEVs(index).HPostallowed(upcard)) Then
                                        newEvs.Strat(upcard) = BJCAGlobalsClass.Strat.PD
                                    ElseIf toStrat.HandEVs(index).SPostallowed(upcard) And PlayerHands(index).HandEVs.StandEV(upcard) > fromStrat.HandEVs(index).EVs.HitEV(upcard) Then
                                        newEvs.Strat(upcard) = BJCAGlobalsClass.Strat.PS
                                    Else
                                        newEvs.Strat(upcard) = BJCAGlobalsClass.Strat.PH
                                    End If
                            End Select
                        End If
                    End If
                End If
            End If
            Select Case newEvs.Strat(upcard)
                Case BJCAGlobalsClass.Strat.D
                    If toStrat.HandEVs(index).SPreallowed(upcard) And Not (DAN And DAS) Then
                        If Not toStrat.HandEVs(index).HPreallowed(upcard) Or PlayerHands(index).HandEVs.StandEV(upcard) > fromStrat.HandEVs(index).EVs.HitEV(upcard) Then
                            newEvs.Strat(upcard) = BJCAGlobalsClass.Strat.DS
                        End If
                    End If
                Case BJCAGlobalsClass.Strat.R
                    If toStrat.HandEVs(index).SPreallowed(upcard) And Not (SAN And SAS) Then
                        If Not toStrat.HandEVs(index).HPreallowed(upcard) Or PlayerHands(index).HandEVs.StandEV(upcard) > fromStrat.HandEVs(index).EVs.HitEV(upcard) Then
                            newEvs.Strat(upcard) = BJCAGlobalsClass.Strat.RS
                        End If
                    End If
            End Select
        End If

        Return newEvs

    End Function

    Private Sub CopyOptStratTotal(ByRef cStrat As BJCAStrategyClass, ByVal upcard As Integer, ByVal total As Integer, ByVal soft As Integer)
        'Soft is passed as the Boolean+1 value
        Dim index As Integer
        Dim newEvs As New BJCAHandStrategyEVsClass

        'Need to copy the OptHitEvs as well for initialization of the strategies.
        index = PlayerHandTotal(total, soft)
        Do While index
            cStrat.HandEVs(index).HandUsed(upcard) = Opt.HandEVs(index).HandUsed(upcard)
            cStrat.HandEVs(index).Multiplier(upcard) = Opt.HandEVs(index).Multiplier(upcard)
            If PlayerHands(index).Hand.NumCards > 1 Then
                cStrat.HandEVs(index).EVs.HitEV(upcard) = Opt.HandEVs(index).EVs.HitEV(upcard)
                If Not cStrat.HandEVs(index).PreForced(upcard) Then
                    newEvs = GetStratMaxEVHand(index, upcard, cStrat, Opt, True, True)
                    cStrat.HandEVs(index).EVs.Strat(upcard) = newEvs.Strat(upcard)
                End If
            End If
            index = PlayerHands(index).NextHand
        Loop

    End Sub

    Private Sub CopyOptStrat(ByRef cStrat As BJCAStrategyClass, ByVal upcard As Integer)
        Dim index As Integer
        Dim newEvs As New BJCAHandStrategyEVsClass

        'Need to copy the OptHitEvs as well for initialization of the strategies.
        For index = 1 To NumPHands
            cStrat.HandEVs(index).HandUsed(upcard) = Opt.HandEVs(index).HandUsed(upcard)
            If PlayerHands(index).Hand.NumCards > 1 Then
                cStrat.HandEVs(index).EVs.HitEV(upcard) = Opt.HandEVs(index).EVs.HitEV(upcard)
                If Not cStrat.HandEVs(index).PreForced(upcard) Then
                    newEvs = GetStratMaxEVHand(index, upcard, cStrat, Opt, True, True)
                    cStrat.HandEVs(index).EVs.Strat(upcard) = newEvs.Strat(upcard)
                End If
            End If
        Next index

    End Sub

#End Region

#Region " Max EV Minus Methods "

    Private Sub ComputeStrategyMaxEVTotalMinus(ByRef cStrat As BJCAStrategyClass, ByVal total As Integer, ByVal soft As Integer, ByVal upcard As Integer)
        'Soft is passed as the Boolean+1 value
        Dim index As Integer
        Dim prob As Double
        Dim maxev As Double
        Dim allForced As Boolean
        Dim rule As Integer
        Dim forcedStrat As Integer

        'The following procedure is based on the fact that the only values that change with
        '  strategy changes are hit/split
        '  The only thing these values can do is decrease compared to optimal play
        '  Only a single pass is performed

        If cStrat.StratTD(total, soft) Is Nothing Then
            cStrat.StratTD(total, soft) = New BJCATDStratClass
        End If

        DealDCard(upcard)

        'First empty the strategy
        cStrat.StratTD(total, soft).Strat(upcard) = 0
        cStrat.StratTD(total, soft).StratEV(upcard) = 0
        cStrat.StratTD(total, soft).StratStandEV(upcard) = 0
        cStrat.StratTD(total, soft).StratHitEV(upcard) = 0
        cStrat.StratTD(total, soft).StratDEV(upcard) = 0
        cStrat.StratTD(total, soft).StratSurrEV(upcard) = 0

        cStrat.StratTD(total, soft).NetProb(upcard) = 0
        cStrat.StratTD(total, soft).NetSProb(upcard) = 0
        cStrat.StratTD(total, soft).NetHProb(upcard) = 0
        cStrat.StratTD(total, soft).NetDProb(upcard) = 0
        cStrat.StratTD(total, soft).NetSurrProb(upcard) = 0

        cStrat.StratTD(total, soft).SStandEV(upcard) = 0
        cStrat.StratTD(total, soft).SHitEV(upcard) = 0
        cStrat.StratTD(total, soft).SDEV(upcard) = 0
        cStrat.StratTD(total, soft).SSurrEV(upcard) = 0

        'Then copy the OP strategy including HandsUsed where necessary into the current strategy
        CopyOptStratTotal(cStrat, upcard, total, soft)

        'Now begin the actual strategy determination

        'The very first thing we need to do is see if the Total/Soft has any available hands
        '   If not we need to fill in the strategy with any cd/split/forced hands available

        'Begin by removing hands that are CD dependent including split hands
        prob = 0
        index = PlayerHandTotal(total, soft)
        Do While index
            If PlayerHands(index).Hand.NumCards < 2 Or (cStrat.NCardsCD <> 0 And PlayerHands(index).Hand.NumCards <= cStrat.NCardsCD) Or cStrat.HandEVs(index).PreForced(upcard) Then
                cStrat.HandEVs(index).HandUsed(upcard) = -1
            End If
            'Don't include blackjack in the strategy determination
            If PlayerHands(index).Hand.IsBJ Then
                cStrat.HandEVs(index).HandUsed(upcard) = -1
            End If
            Select Case cStrat.HandEVs(index).EVs.Strat(upcard)
                Case BJCAGlobalsClass.Strat.P, BJCAGlobalsClass.Strat.PS, BJCAGlobalsClass.Strat.PD, BJCAGlobalsClass.Strat.PH, BJCAGlobalsClass.Strat.PR
                    cStrat.HandEVs(index).HandUsed(upcard) = -1
            End Select
            If cStrat.HandEVs(index).HandUsed(upcard) = 1 Then
                prob += 1
            End If
            index = PlayerHands(index).NextHand
        Loop

        If prob = 0 Then
            'All hands are either nonexistent, forced, split or CD
            'The strategy must be dealt with differently
            'First do a quick check of all the hands and see if they're all forced
            allForced = True
            index = PlayerHandTotal(total, soft)
            Do While index
                If PlayerHands(index).Hand.NumCards > 1 And (cStrat.NCardsCD = 0 Or PlayerHands(index).Hand.NumCards > cStrat.NCardsCD) And cStrat.HandEVs(index).HandUsed(upcard) = -1 And Not cStrat.HandEVs(index).PreForced(upcard) Then
                    allForced = False
                    Exit Do
                End If
                index = PlayerHands(index).NextHand
            Loop

            If allForced Then
                'All the hands are forced so fill in from the Forced Table if possible.  Otherwise leave it blank.
                For rule = 0 To ForcedTableRulesList.NumRules - 1
                    If ForcedTableRulesList.L(rule).Upcard = upcard And ForcedTableRulesList.L(rule).Hand.Total = total And (ForcedTableRulesList.L(rule).Hand.Soft + 1) = soft And ForcedTableRulesList.L(rule).Hand.NumCards = 0 Then
                        cStrat.StratTD(total, soft).Strat(upcard) = ForcedTableRulesList.L(rule).Strat
                    End If
                Next rule
            Else
                'Non-forced hands are available for strategy determination
                cStrat.StratTD(total, soft).NetProb(upcard) = 0
                cStrat.StratTD(total, soft).NetSProb(upcard) = 0
                cStrat.StratTD(total, soft).NetHProb(upcard) = 0
                cStrat.StratTD(total, soft).NetDProb(upcard) = 0
                cStrat.StratTD(total, soft).NetSurrProb(upcard) = 0

                cStrat.StratTD(total, soft).StratStandEV(upcard) = 0
                cStrat.StratTD(total, soft).StratHitEV(upcard) = 0
                cStrat.StratTD(total, soft).StratDEV(upcard) = 0
                cStrat.StratTD(total, soft).StratSurrEV(upcard) = 0

                'Now fill in all the evs
                index = PlayerHandTotal(total, soft)
                Do While index
                    If PlayerHands(index).Hand.NumCards > 1 And (cStrat.NCardsCD = 0 Or PlayerHands(index).Hand.NumCards > cStrat.NCardsCD) And cStrat.HandEVs(index).HandUsed(upcard) = -1 And Not cStrat.HandEVs(index).PreForced(upcard) Then
                        prob = PlayerHands(index).HandEVs.Prob(upcard) * cStrat.HandEVs(index).Multiplier(upcard)
                        cStrat.StratTD(PlayerHands(index).Hand.Total, PlayerHands(index).Hand.Soft + 1).NetProb(upcard) += prob
                        If cStrat.HandEVs(index).SPreallowed(upcard) Then
                            cStrat.StratTD(PlayerHands(index).Hand.Total, PlayerHands(index).Hand.Soft + 1).NetSProb(upcard) += prob
                            cStrat.StratTD(PlayerHands(index).Hand.Total, PlayerHands(index).Hand.Soft + 1).StratStandEV(upcard) += prob * PlayerHands(index).HandEVs.StandEV(upcard)
                        End If
                        If cStrat.HandEVs(index).HPreallowed(upcard) Then
                            cStrat.StratTD(PlayerHands(index).Hand.Total, PlayerHands(index).Hand.Soft + 1).NetHProb(upcard) += prob
                            cStrat.StratTD(PlayerHands(index).Hand.Total, PlayerHands(index).Hand.Soft + 1).StratHitEV(upcard) += prob * cStrat.HandEVs(index).EVs.HitEV(upcard)
                        End If
                        If cStrat.HandEVs(index).DPreallowed(upcard) Then
                            cStrat.StratTD(PlayerHands(index).Hand.Total, PlayerHands(index).Hand.Soft + 1).NetDProb(upcard) += prob
                            cStrat.StratTD(PlayerHands(index).Hand.Total, PlayerHands(index).Hand.Soft + 1).StratDEV(upcard) += prob * PlayerHands(index).HandEVs.DEV(upcard)
                        End If
                        If cStrat.HandEVs(index).RPreallowed(upcard) > 0 Then
                            cStrat.StratTD(PlayerHands(index).Hand.Total, PlayerHands(index).Hand.Soft + 1).NetSurrProb(upcard) += prob
                            cStrat.StratTD(PlayerHands(index).Hand.Total, PlayerHands(index).Hand.Soft + 1).StratSurrEV(upcard) += prob * PlayerHands(index).HandEVs.SurrEV(upcard)
                        End If
                    End If
                    index = PlayerHands(index).NextHand
                Loop

                'Now we need to normalize the ev's
                If cStrat.StratTD(total, soft).NetSProb(upcard) <> 0 Then
                    cStrat.StratTD(total, soft).StratStandEV(upcard) = cStrat.StratTD(total, soft).StratStandEV(upcard) / cStrat.StratTD(total, soft).NetSProb(upcard)
                End If
                If cStrat.StratTD(total, soft).NetHProb(upcard) <> 0 Then
                    cStrat.StratTD(total, soft).StratHitEV(upcard) = cStrat.StratTD(total, soft).StratHitEV(upcard) / cStrat.StratTD(total, soft).NetHProb(upcard)
                End If
                If cStrat.StratTD(total, soft).NetDProb(upcard) <> 0 Then
                    cStrat.StratTD(total, soft).StratDEV(upcard) = cStrat.StratTD(total, soft).StratDEV(upcard) / cStrat.StratTD(total, soft).NetDProb(upcard)
                End If
                If cStrat.StratTD(total, soft).NetSurrProb(upcard) <> 0 Then
                    cStrat.StratTD(total, soft).StratSurrEV(upcard) = cStrat.StratTD(total, soft).StratSurrEV(upcard) / cStrat.StratTD(total, soft).NetSurrProb(upcard)
                End If

                'Fill in the final TD values for each total
                If cStrat.StratTD(total, soft).NetProb(upcard) <> 0 Then
                    'Then see if any hands actually exist with the Total/Soft
                    If (cStrat.StratTD(total, soft).NetSProb(upcard) <> 0) Then
                        cStrat.StratTD(total, soft).Strat(upcard) = BJCAGlobalsClass.Strat.S
                        maxev = cStrat.StratTD(total, soft).StratStandEV(upcard)
                    End If
                    If maxev = 0 Or ((cStrat.StratTD(total, soft).NetHProb(upcard) <> 0) And (cStrat.StratTD(total, soft).StratHitEV(upcard) > maxev)) Then
                        cStrat.StratTD(total, soft).Strat(upcard) = BJCAGlobalsClass.Strat.H
                        maxev = cStrat.StratTD(total, soft).StratHitEV(upcard)
                    End If
                    If maxev = 0 Or ((cStrat.StratTD(total, soft).NetDProb(upcard) <> 0) And (cStrat.StratTD(total, soft).StratDEV(upcard) > maxev)) Then
                        If maxev = 0 Or cStrat.StratTD(total, soft).Strat(upcard) = BJCAGlobalsClass.Strat.H Or (DAN And DAS) Then
                            cStrat.StratTD(total, soft).Strat(upcard) = BJCAGlobalsClass.Strat.D
                        Else
                            cStrat.StratTD(total, soft).Strat(upcard) = BJCAGlobalsClass.Strat.DS
                        End If
                        maxev = cStrat.StratTD(total, soft).StratDEV(upcard)
                    End If
                    If maxev = 0 Or ((cStrat.StratTD(total, soft).NetSurrProb(upcard) <> 0) And (cStrat.StratTD(total, soft).StratSurrEV(upcard) >= maxev)) Then
                        If maxev = 0 Or cStrat.StratTD(total, soft).Strat(upcard) = BJCAGlobalsClass.Strat.H Or cStrat.StratTD(total, soft).Strat(upcard) = BJCAGlobalsClass.Strat.D Or (SAN And SAS) Then
                            cStrat.StratTD(total, soft).Strat(upcard) = BJCAGlobalsClass.Strat.R
                        Else
                            cStrat.StratTD(total, soft).Strat(upcard) = BJCAGlobalsClass.Strat.RS
                        End If
                        maxev = cStrat.StratTD(total, soft).StratSurrEV(upcard)
                    End If
                End If
            End If
        Else
            'Hands are availabe for proper strategy determination so continue

            cStrat.StratTD(total, soft).NetProb(upcard) = 0
            cStrat.StratTD(total, soft).NetSProb(upcard) = 0
            cStrat.StratTD(total, soft).NetHProb(upcard) = 0
            cStrat.StratTD(total, soft).NetDProb(upcard) = 0
            cStrat.StratTD(total, soft).NetSurrProb(upcard) = 0

            cStrat.StratTD(total, soft).StratStandEV(upcard) = 0
            cStrat.StratTD(total, soft).StratHitEV(upcard) = 0
            cStrat.StratTD(total, soft).StratDEV(upcard) = 0
            cStrat.StratTD(total, soft).StratSurrEV(upcard) = 0

            'Fill in the strategy using all used hands to start
            'First look at hands where both double and surrender are allowed
            index = PlayerHandTotal(total, soft)
            Do While index
                If cStrat.HandEVs(index).HandUsed(upcard) = 1 And cStrat.HandEVs(index).RPreallowed(upcard) > 0 And cStrat.HandEVs(index).DPreallowed(upcard) Then
                    prob = PlayerHands(index).HandEVs.Prob(upcard) * cStrat.HandEVs(index).Multiplier(upcard)

                    cStrat.StratTD(total, soft).NetProb(upcard) += prob
                    cStrat.StratTD(total, soft).SStandEV(upcard) += prob * PlayerHands(index).HandEVs.StandEV(upcard)
                    cStrat.StratTD(total, soft).SDEV(upcard) += prob * PlayerHands(index).HandEVs.DEV(upcard)
                    cStrat.StratTD(total, soft).SHitEV(upcard) += prob * Opt.HandEVs(index).EVs.HitEV(upcard)
                    cStrat.StratTD(total, soft).SSurrEV(upcard) += prob * PlayerHands(index).HandEVs.SurrEV(upcard)
                End If
                index = PlayerHands(index).NextHand
            Loop

            'If the probability of a strategy option is 0 then prevent it from being the strategy
            If cStrat.StratTD(total, soft).NetProb(upcard) <> 0 Then
                If cStrat.StratTD(total, soft).SStandEV(upcard) = 0 Then cStrat.StratTD(total, soft).SStandEV(upcard) = -2
                If cStrat.StratTD(total, soft).SDEV(upcard) = 0 Then cStrat.StratTD(total, soft).SDEV(upcard) = -2
                If cStrat.StratTD(total, soft).SHitEV(upcard) = 0 Then cStrat.StratTD(total, soft).SHitEV(upcard) = -2
                If cStrat.StratTD(total, soft).SSurrEV(upcard) = 0 Then cStrat.StratTD(total, soft).SSurrEV(upcard) = -2
            End If

            'Compare double/surrender to hit/stand
            If (cStrat.StratTD(total, soft).NetProb(upcard) <> 0) Then
                cStrat.StratTD(total, soft).Strat(upcard) = BJCAGlobalsClass.Strat.S
                maxev = cStrat.StratTD(total, soft).SStandEV(upcard)
                If cStrat.StratTD(total, soft).SHitEV(upcard) > maxev Then
                    cStrat.StratTD(total, soft).Strat(upcard) = BJCAGlobalsClass.Strat.H
                    maxev = cStrat.StratTD(total, soft).SHitEV(upcard)
                End If
                If cStrat.StratTD(total, soft).SDEV(upcard) > maxev Then
                    If cStrat.StratTD(total, soft).Strat(upcard) = BJCAGlobalsClass.Strat.H Then
                        cStrat.StratTD(total, soft).Strat(upcard) = BJCAGlobalsClass.Strat.D
                    Else
                        cStrat.StratTD(total, soft).Strat(upcard) = BJCAGlobalsClass.Strat.DS
                    End If
                    maxev = cStrat.StratTD(total, soft).SDEV(upcard)
                End If
                If cStrat.StratTD(total, soft).SSurrEV(upcard) >= maxev Then
                    If cStrat.StratTD(total, soft).Strat(upcard) = BJCAGlobalsClass.Strat.H Or cStrat.StratTD(total, soft).Strat(upcard) = BJCAGlobalsClass.Strat.D Then
                        cStrat.StratTD(total, soft).Strat(upcard) = BJCAGlobalsClass.Strat.R
                    Else
                        cStrat.StratTD(total, soft).Strat(upcard) = BJCAGlobalsClass.Strat.RS
                    End If
                    maxev = cStrat.StratTD(total, soft).SSurrEV(upcard)
                End If
                If cStrat.StratTD(total, soft).Strat(upcard) = BJCAGlobalsClass.Strat.DS And DAN And DAS Then
                    cStrat.StratTD(total, soft).Strat(upcard) = BJCAGlobalsClass.Strat.D
                ElseIf cStrat.StratTD(total, soft).Strat(upcard) = BJCAGlobalsClass.Strat.RS And SAN And SAS Then
                    cStrat.StratTD(total, soft).Strat(upcard) = BJCAGlobalsClass.Strat.R
                End If
            End If

            'Now check values where only double is allowed and the strategy hasn't been set to surrender/split
            cStrat.StratTD(total, soft).NetProb(upcard) = 0

            cStrat.StratTD(total, soft).SStandEV(upcard) = 0
            cStrat.StratTD(total, soft).SHitEV(upcard) = 0
            cStrat.StratTD(total, soft).SDEV(upcard) = 0
            cStrat.StratTD(total, soft).SSurrEV(upcard) = 0

            If cStrat.StratTD(total, soft).Strat(upcard) <> BJCAGlobalsClass.Strat.R And cStrat.StratTD(total, soft).Strat(upcard) <> BJCAGlobalsClass.Strat.RS Then
                index = PlayerHandTotal(total, soft)
                Do While index
                    If cStrat.HandEVs(index).HandUsed(upcard) = 1 And cStrat.HandEVs(index).DPreallowed(upcard) Then
                        prob = PlayerHands(index).HandEVs.Prob(upcard) * cStrat.HandEVs(index).Multiplier(upcard)
                        cStrat.StratTD(total, soft).NetProb(upcard) += prob
                        cStrat.StratTD(total, soft).SStandEV(upcard) += prob * PlayerHands(index).HandEVs.StandEV(upcard)
                        cStrat.StratTD(total, soft).SDEV(upcard) += prob * PlayerHands(index).HandEVs.DEV(upcard)
                        cStrat.StratTD(total, soft).SHitEV(upcard) += prob * cStrat.HandEVs(index).EVs.HitEV(upcard)
                    End If
                    index = PlayerHands(index).NextHand
                Loop
            End If

            'If the probability of a strategy option is 0 then prevent it from being the strategy
            If cStrat.StratTD(total, soft).NetProb(upcard) <> 0 Then
                If cStrat.StratTD(total, soft).SStandEV(upcard) = 0 Then cStrat.StratTD(total, soft).SStandEV(upcard) = -2
                If cStrat.StratTD(total, soft).SDEV(upcard) = 0 Then cStrat.StratTD(total, soft).SDEV(upcard) = -2
                If cStrat.StratTD(total, soft).SHitEV(upcard) = 0 Then cStrat.StratTD(total, soft).SHitEV(upcard) = -2
                If cStrat.StratTD(total, soft).SSurrEV(upcard) = 0 Then cStrat.StratTD(total, soft).SSurrEV(upcard) = -2
            End If

            'Compare double to hit/stand
            If (cStrat.StratTD(total, soft).NetProb(upcard) <> 0) Then
                cStrat.StratTD(total, soft).Strat(upcard) = BJCAGlobalsClass.Strat.S
                maxev = cStrat.StratTD(total, soft).SStandEV(upcard)
                If cStrat.StratTD(total, soft).SHitEV(upcard) > maxev Then
                    cStrat.StratTD(total, soft).Strat(upcard) = BJCAGlobalsClass.Strat.H
                    maxev = cStrat.StratTD(total, soft).SHitEV(upcard)
                End If
                If cStrat.StratTD(total, soft).SDEV(upcard) > maxev Then
                    If cStrat.StratTD(total, soft).Strat(upcard) = BJCAGlobalsClass.Strat.H Or (DAN And DAS) Then
                        cStrat.StratTD(total, soft).Strat(upcard) = BJCAGlobalsClass.Strat.D
                    Else
                        cStrat.StratTD(total, soft).Strat(upcard) = BJCAGlobalsClass.Strat.DS
                    End If
                    maxev = cStrat.StratTD(total, soft).SDEV(upcard)
                End If
            End If

            'Now check values where only surrender is allowed and the strategy hasn't been set to double/split
            cStrat.StratTD(total, soft).NetProb(upcard) = 0

            cStrat.StratTD(total, soft).SStandEV(upcard) = 0
            cStrat.StratTD(total, soft).SHitEV(upcard) = 0
            cStrat.StratTD(total, soft).SDEV(upcard) = 0
            cStrat.StratTD(total, soft).SSurrEV(upcard) = 0

            If cStrat.StratTD(total, soft).Strat(upcard) <> BJCAGlobalsClass.Strat.D And cStrat.StratTD(total, soft).Strat(upcard) <> BJCAGlobalsClass.Strat.DS Then
                index = PlayerHandTotal(total, soft)
                Do While index
                    If cStrat.HandEVs(index).HandUsed(upcard) = 1 And cStrat.HandEVs(index).RPreallowed(upcard) > 0 Then
                        prob = PlayerHands(index).HandEVs.Prob(upcard) * cStrat.HandEVs(index).Multiplier(upcard)
                        cStrat.StratTD(total, soft).NetProb(upcard) += prob
                        cStrat.StratTD(total, soft).SStandEV(upcard) += prob * PlayerHands(index).HandEVs.StandEV(upcard)
                        cStrat.StratTD(total, soft).SHitEV(upcard) += prob * cStrat.HandEVs(index).EVs.HitEV(upcard)
                        cStrat.StratTD(total, soft).SSurrEV(upcard) += prob * PlayerHands(index).HandEVs.SurrEV(upcard)
                    End If
                    index = PlayerHands(index).NextHand
                Loop
            End If

            'If the probability of a strategy option is 0 then prevent it from being the strategy
            If cStrat.StratTD(total, soft).NetProb(upcard) <> 0 Then
                If cStrat.StratTD(total, soft).SStandEV(upcard) = 0 Then cStrat.StratTD(total, soft).SStandEV(upcard) = -2
                If cStrat.StratTD(total, soft).SHitEV(upcard) = 0 Then cStrat.StratTD(total, soft).SHitEV(upcard) = -2
                If cStrat.StratTD(total, soft).SSurrEV(upcard) = 0 Then cStrat.StratTD(total, soft).SSurrEV(upcard) = -2
            End If

            'Compare surrender to hit/stand
            If (cStrat.StratTD(total, soft).NetProb(upcard) <> 0) Then
                cStrat.StratTD(total, soft).Strat(upcard) = BJCAGlobalsClass.Strat.S
                maxev = cStrat.StratTD(total, soft).SStandEV(upcard)
                If cStrat.StratTD(total, soft).SHitEV(upcard) > maxev Then
                    cStrat.StratTD(total, soft).Strat(upcard) = BJCAGlobalsClass.Strat.H
                    maxev = cStrat.StratTD(total, soft).SHitEV(upcard)
                End If
                If cStrat.StratTD(total, soft).SSurrEV(upcard) > maxev Then
                    If cStrat.StratTD(total, soft).Strat(upcard) = BJCAGlobalsClass.Strat.H Or (SAN And SAS) Then
                        cStrat.StratTD(total, soft).Strat(upcard) = BJCAGlobalsClass.Strat.R
                    Else
                        cStrat.StratTD(total, soft).Strat(upcard) = BJCAGlobalsClass.Strat.RS
                    End If
                    maxev = cStrat.StratTD(total, soft).SSurrEV(upcard)
                End If
            End If

            'Now check values where neither surrender nor double is the strategy and the strategy hasn't been set to double/split
            cStrat.StratTD(total, soft).NetProb(upcard) = 0

            cStrat.StratTD(total, soft).SStandEV(upcard) = 0
            cStrat.StratTD(total, soft).SHitEV(upcard) = 0

            If cStrat.StratTD(total, soft).Strat(upcard) <> BJCAGlobalsClass.Strat.D And cStrat.StratTD(total, soft).Strat(upcard) <> BJCAGlobalsClass.Strat.DS And cStrat.StratTD(total, soft).Strat(upcard) <> BJCAGlobalsClass.Strat.R And cStrat.StratTD(total, soft).Strat(upcard) <> BJCAGlobalsClass.Strat.RS Then
                index = PlayerHandTotal(total, soft)
                Do While index
                    If cStrat.HandEVs(index).HandUsed(upcard) = 1 Then
                        prob = PlayerHands(index).HandEVs.Prob(upcard) * cStrat.HandEVs(index).Multiplier(upcard)
                        cStrat.StratTD(total, soft).NetProb(upcard) += prob
                        cStrat.StratTD(total, soft).SStandEV(upcard) += prob * PlayerHands(index).HandEVs.StandEV(upcard)
                        cStrat.StratTD(total, soft).SHitEV(upcard) += prob * cStrat.HandEVs(index).EVs.HitEV(upcard)
                    End If
                    index = PlayerHands(index).NextHand
                Loop
            End If

            'If the probability of a strategy option is 0 then prevent it from being the strategy
            If cStrat.StratTD(total, soft).NetProb(upcard) <> 0 Then
                If cStrat.StratTD(total, soft).SStandEV(upcard) = 0 Then cStrat.StratTD(total, soft).SStandEV(upcard) = -2
                If cStrat.StratTD(total, soft).SHitEV(upcard) = 0 Then cStrat.StratTD(total, soft).SHitEV(upcard) = -2
            End If

            'Compare hit to stand
            If (cStrat.StratTD(total, soft).NetProb(upcard) <> 0) Then
                cStrat.StratTD(total, soft).Strat(upcard) = BJCAGlobalsClass.Strat.S
                maxev = cStrat.StratTD(total, soft).SStandEV(upcard)
                If cStrat.StratTD(total, soft).SHitEV(upcard) > maxev Then
                    cStrat.StratTD(total, soft).Strat(upcard) = BJCAGlobalsClass.Strat.H
                    maxev = cStrat.StratTD(total, soft).SHitEV(upcard)
                End If
            End If

        End If

        UndealDCard(upcard)

    End Sub

    Private Sub ComputeStrategyMaxEVMinus(ByRef cStrat As BJCAStrategyClass, ByVal upcard As Integer)
        Dim total As Integer
        Dim soft As Integer
        Dim prob As Double
        Dim card As Integer
        Dim index As Integer
        Dim card2 As Integer
        Dim pairMultiplier As Integer
        Dim newEvs As New BJCAHandStrategyEVsClass

        For total = 21 To 11 Step -1
            Call ComputeStrategyMaxEVTotalMinus(cStrat, total, False + 1, upcard)
        Next total
        For total = 21 To 12 Step -1
            Call ComputeStrategyMaxEVTotalMinus(cStrat, total, True + 1, upcard)
        Next total
        For total = 10 To 4 Step -1
            Call ComputeStrategyMaxEVTotalMinus(cStrat, total, False + 1, upcard)
        Next total

        'Now that all the preliminary strategies are determined we need to
        '   check to see if any strategies changed.
        'This must be with all the strategies available so is done now.

        DealDCard(upcard)

        'Calculate the new overall hitting values using the current strategy
        ComputeStratHit(cStrat, upcard)

        'Figure out which hands are actually used using the strategy when hitting
        'The next step plays out the strategy and increases the HandUsed index
        GetHandsUsed(cStrat, upcard, 1, True)

        'Cycle through the hands and remove all forced, double, surrendered and CD hands
        prob = 0
        For index = 1 To NumPHands
            If PlayerHands(index).Hand.NumCards > 1 Then
                If (cStrat.NCardsCD <> 0 And PlayerHands(index).Hand.NumCards <= cStrat.NCardsCD) Or cStrat.HandEVs(index).PreForced(upcard) Then
                    cStrat.HandEVs(index).HandUsed(upcard) = -1
                ElseIf cStrat.HandEVs(index).DPreallowed(upcard) And (cStrat.StratTD(PlayerHands(index).Hand.Total, PlayerHands(index).Hand.Soft + 1).Strat(upcard) = BJCAGlobalsClass.Strat.D Or cStrat.StratTD(PlayerHands(index).Hand.Total, PlayerHands(index).Hand.Soft + 1).Strat(upcard) = BJCAGlobalsClass.Strat.DS) Then
                    cStrat.HandEVs(index).HandUsed(upcard) = -1
                ElseIf cStrat.HandEVs(index).RPreallowed(upcard) > 0 And (cStrat.StratTD(PlayerHands(index).Hand.Total, PlayerHands(index).Hand.Soft + 1).Strat(upcard) = BJCAGlobalsClass.Strat.R Or cStrat.StratTD(PlayerHands(index).Hand.Total, PlayerHands(index).Hand.Soft + 1).Strat(upcard) = BJCAGlobalsClass.Strat.RS) Then
                    cStrat.HandEVs(index).HandUsed(upcard) = -1
                End If
                If cStrat.HandEVs(index).HandUsed(upcard) = 1 Then
                    prob += 1
                End If
            End If
        Next index

        'Only change the strategy if there are non-forced/split hands.
        If prob > 0 Then
            For total = 4 To 21
                For soft = 0 To 1
                    'Look at all hands where the strategy included hitting
                    If Not cStrat.StratTD(total, soft) Is Nothing Then
                        If cStrat.StratTD(total, soft).Strat(upcard) = BJCAGlobalsClass.Strat.H Or cStrat.StratTD(total, soft).Strat(upcard) = BJCAGlobalsClass.Strat.D Or cStrat.StratTD(total, soft).Strat(upcard) = BJCAGlobalsClass.Strat.R Then
                            cStrat.StratTD(total, soft).NetProb(upcard) = 0
                            cStrat.StratTD(total, soft).SStandEV(upcard) = 0
                            cStrat.StratTD(total, soft).SHitEV(upcard) = 0

                            index = PlayerHandTotal(total, soft)
                            Do While index
                                If cStrat.HandEVs(index).HandUsed(upcard) = 1 Then
                                    prob = PlayerHands(index).HandEVs.Prob(upcard) * cStrat.HandEVs(index).Multiplier(upcard)
                                    cStrat.StratTD(total, soft).NetProb(upcard) += prob
                                    cStrat.StratTD(total, soft).SStandEV(upcard) += prob * PlayerHands(index).HandEVs.StandEV(upcard)
                                    cStrat.StratTD(total, soft).SHitEV(upcard) += prob * cStrat.HandEVs(index).EVs.HitEV(upcard)
                                End If
                                index = PlayerHands(index).NextHand
                            Loop

                            'If the probability of a strategy option is 0 then prevent it from being the strategy
                            If cStrat.StratTD(total, soft).NetProb(upcard) <> 0 Then
                                If cStrat.StratTD(total, soft).SStandEV(upcard) = 0 Then cStrat.StratTD(total, soft).SStandEV(upcard) = -2
                                If cStrat.StratTD(total, soft).SHitEV(upcard) = 0 Then cStrat.StratTD(total, soft).SHitEV(upcard) = -2
                            End If

                            'Compare hit to stand and adjust the strategy as needed
                            If cStrat.StratTD(total, soft).SHitEV(upcard) < cStrat.StratTD(total, soft).SStandEV(upcard) Then
                                If cStrat.StratTD(total, soft).Strat(upcard) = BJCAGlobalsClass.Strat.H Then
                                    cStrat.StratTD(total, soft).Strat(upcard) = BJCAGlobalsClass.Strat.S
                                ElseIf cStrat.StratTD(total, soft).Strat(upcard) = BJCAGlobalsClass.Strat.D And Not (DAN And DAS) Then
                                    cStrat.StratTD(total, soft).Strat(upcard) = BJCAGlobalsClass.Strat.DS
                                ElseIf cStrat.StratTD(total, soft).Strat(upcard) = BJCAGlobalsClass.Strat.R And Not (SAN And SAS) Then
                                    cStrat.StratTD(total, soft).Strat(upcard) = BJCAGlobalsClass.Strat.RS
                                End If
                            End If
                        End If
                    End If
                Next soft
            Next total
        End If

        'Now calculate the final Total/Soft evs
        'Determine the whether a given hand is used
        GetHandsUsed(cStrat, upcard, 1, True, True)

        'Calculate the final overall hitting values using the current strategy
        ComputeStratHit(cStrat, upcard)

        'Fill in the final TD values for each total
        For total = 4 To 21
            For soft = 0 To 1
                If Not cStrat.StratTD(total, soft) Is Nothing Then
                    cStrat.StratTD(total, soft).NetProb(upcard) = 0
                    cStrat.StratTD(total, soft).NetSProb(upcard) = 0
                    cStrat.StratTD(total, soft).NetHProb(upcard) = 0
                    cStrat.StratTD(total, soft).NetDProb(upcard) = 0
                    cStrat.StratTD(total, soft).NetSurrProb(upcard) = 0

                    cStrat.StratTD(total, soft).StratStandEV(upcard) = 0
                    cStrat.StratTD(total, soft).StratHitEV(upcard) = 0
                    cStrat.StratTD(total, soft).StratDEV(upcard) = 0
                    cStrat.StratTD(total, soft).StratSurrEV(upcard) = 0
                End If
            Next soft
        Next total

        For index = 1 To NumPHands
            If cStrat.HandEVs(index).HandUsed(upcard) = 1 And Not PlayerHands(index).Hand.IsBJ And (cStrat.NCardsCD = 0 Or PlayerHands(index).Hand.NumCards > cStrat.NCardsCD) Then
                prob = PlayerHands(index).HandEVs.Prob(upcard) * cStrat.HandEVs(index).Multiplier(upcard)
                cStrat.StratTD(PlayerHands(index).Hand.Total, PlayerHands(index).Hand.Soft + 1).NetProb(upcard) += prob
                If cStrat.HandEVs(index).SPreallowed(upcard) Then
                    cStrat.StratTD(PlayerHands(index).Hand.Total, PlayerHands(index).Hand.Soft + 1).NetSProb(upcard) += prob
                    cStrat.StratTD(PlayerHands(index).Hand.Total, PlayerHands(index).Hand.Soft + 1).StratStandEV(upcard) += prob * PlayerHands(index).HandEVs.StandEV(upcard)
                End If
                If cStrat.HandEVs(index).HPreallowed(upcard) Then
                    cStrat.StratTD(PlayerHands(index).Hand.Total, PlayerHands(index).Hand.Soft + 1).NetHProb(upcard) += prob
                    cStrat.StratTD(PlayerHands(index).Hand.Total, PlayerHands(index).Hand.Soft + 1).StratHitEV(upcard) += prob * cStrat.HandEVs(index).EVs.HitEV(upcard)
                End If
                If cStrat.HandEVs(index).DPreallowed(upcard) Then
                    cStrat.StratTD(PlayerHands(index).Hand.Total, PlayerHands(index).Hand.Soft + 1).NetDProb(upcard) += prob
                    cStrat.StratTD(PlayerHands(index).Hand.Total, PlayerHands(index).Hand.Soft + 1).StratDEV(upcard) += prob * PlayerHands(index).HandEVs.DEV(upcard)
                End If
                If cStrat.HandEVs(index).RPreallowed(upcard) Then
                    cStrat.StratTD(PlayerHands(index).Hand.Total, PlayerHands(index).Hand.Soft + 1).NetSurrProb(upcard) += prob
                    cStrat.StratTD(PlayerHands(index).Hand.Total, PlayerHands(index).Hand.Soft + 1).StratSurrEV(upcard) += prob * PlayerHands(index).HandEVs.SurrEV(upcard)
                End If
            End If
        Next index

        'Now we need to normalize the ev's
        For total = 4 To 21
            For soft = 0 To 1
                If Not cStrat.StratTD(total, soft) Is Nothing Then
                    If cStrat.StratTD(total, soft).NetSProb(upcard) <> 0 Then
                        cStrat.StratTD(total, soft).StratStandEV(upcard) /= cStrat.StratTD(total, soft).NetSProb(upcard)
                        cStrat.StratTD(total, soft).NetSProb(upcard) /= cStrat.StratTD(total, soft).NetProb(upcard)
                    End If
                    If cStrat.StratTD(total, soft).NetHProb(upcard) <> 0 Then
                        cStrat.StratTD(total, soft).StratHitEV(upcard) /= cStrat.StratTD(total, soft).NetHProb(upcard)
                        cStrat.StratTD(total, soft).NetHProb(upcard) /= cStrat.StratTD(total, soft).NetProb(upcard)
                    End If
                    If cStrat.StratTD(total, soft).NetDProb(upcard) <> 0 Then
                        cStrat.StratTD(total, soft).StratDEV(upcard) /= cStrat.StratTD(total, soft).NetDProb(upcard)
                        cStrat.StratTD(total, soft).NetDProb(upcard) /= cStrat.StratTD(total, soft).NetProb(upcard)
                    End If
                    If cStrat.StratTD(total, soft).NetSurrProb(upcard) <> 0 Then
                        cStrat.StratTD(total, soft).StratSurrEV(upcard) /= cStrat.StratTD(total, soft).NetSurrProb(upcard)
                        cStrat.StratTD(total, soft).NetSurrProb(upcard) /= cStrat.StratTD(total, soft).NetProb(upcard)
                    End If
                End If
            Next soft
        Next total


        'Finally fill in the strategies for each hand except for splits which are already determined
        For index = 1 To NumPHands
            If PlayerHands(index).Hand.NumCards > 1 And Not cStrat.HandEVs(index).PreForced(upcard) Then
                If (cStrat.HandEVs(index).EVs.Strat(upcard) <> BJCAGlobalsClass.Strat.P And cStrat.HandEVs(index).EVs.Strat(upcard) <> BJCAGlobalsClass.Strat.PS And cStrat.HandEVs(index).EVs.Strat(upcard) <> BJCAGlobalsClass.Strat.PH And cStrat.HandEVs(index).EVs.Strat(upcard) <> BJCAGlobalsClass.Strat.PD And cStrat.HandEVs(index).EVs.Strat(upcard) <> BJCAGlobalsClass.Strat.PR) Then
                    If cStrat.NCardsCD <> 0 And PlayerHands(index).Hand.NumCards <= cStrat.NCardsCD Then
                        newEvs = GetStratMaxEVHand(index, upcard, cStrat, cStrat, True, True)
                        cStrat.HandEVs(index).EVs.Strat(upcard) = newEvs.Strat(upcard)
                        cStrat.HandEVs(index).EVs.StratEV(upcard) = newEvs.StratEV(upcard)
                        cStrat.HandEVs(index).EVs.StratPushEV(upcard) = newEvs.StratPushEV(upcard)
                    ElseIf PlayerHands(index).Hand.IsBJ Then
                        'Always use the optimal strategy for BJ
                        cStrat.HandEVs(index).EVs.Strat(upcard) = Opt.HandEVs(index).EVs.Strat(upcard)
                        cStrat.HandEVs(index).EVs.StratEV(upcard) = Opt.HandEVs(index).EVs.StratEV(upcard)
                        cStrat.HandEVs(index).EVs.StratPushEV(upcard) = newEvs.StratPushEV(upcard)
                    Else
                        'Hand is total dependent
                        cStrat.HandEVs(index).EVs.Strat(upcard) = cStrat.StratTD(PlayerHands(index).Hand.Total, PlayerHands(index).Hand.Soft + 1).Strat(upcard)
                        If cStrat.HandEVs(index).EVs.Strat(upcard) = BJCAGlobalsClass.Strat.D And Not cStrat.HandEVs(index).DPreallowed(upcard) Then
                            cStrat.HandEVs(index).EVs.Strat(upcard) = BJCAGlobalsClass.Strat.H
                        ElseIf cStrat.HandEVs(index).EVs.Strat(upcard) = BJCAGlobalsClass.Strat.DS And Not cStrat.HandEVs(index).DPreallowed(upcard) Then
                            cStrat.HandEVs(index).EVs.Strat(upcard) = BJCAGlobalsClass.Strat.S
                        ElseIf cStrat.HandEVs(index).EVs.Strat(upcard) = BJCAGlobalsClass.Strat.R And (cStrat.HandEVs(index).RPreallowed(upcard) = 0) Then
                            cStrat.HandEVs(index).EVs.Strat(upcard) = BJCAGlobalsClass.Strat.H
                        ElseIf cStrat.HandEVs(index).EVs.Strat(upcard) = BJCAGlobalsClass.Strat.RS And (cStrat.HandEVs(index).RPreallowed(upcard) = 0) Then
                            cStrat.HandEVs(index).EVs.Strat(upcard) = BJCAGlobalsClass.Strat.S
                        End If
                    End If
                    Select Case cStrat.HandEVs(index).EVs.Strat(upcard)
                        Case BJCAGlobalsClass.Strat.S
                            cStrat.HandEVs(index).EVs.StratEV(upcard) = PlayerHands(index).HandEVs.StandEV(upcard)
                        Case BJCAGlobalsClass.Strat.H
                            cStrat.HandEVs(index).EVs.StratEV(upcard) = cStrat.HandEVs(index).EVs.HitEV(upcard)
                        Case BJCAGlobalsClass.Strat.D, BJCAGlobalsClass.Strat.DS
                            cStrat.HandEVs(index).EVs.StratEV(upcard) = PlayerHands(index).HandEVs.DEV(upcard)
                        Case BJCAGlobalsClass.Strat.R, BJCAGlobalsClass.Strat.RS
                            cStrat.HandEVs(index).EVs.StratEV(upcard) = PlayerHands(index).HandEVs.SurrEV(upcard)
                        Case Else
                            cStrat.HandEVs(index).EVs.StratEV(upcard) = 0
                            '                        Case BJCAGlobalsClass.Strat.P, BJCAGlobalsClass.Strat.PS, BJCAGlobalsClass.Strat.PH, BJCAGlobalsClass.Strat.PD, BJCAGlobalsClass.Strat.PR
                            '                            cStrat.HandEVs(index).EVs.StratEV(upcard) = cStrat.HandEVs(index).SplitEV(upcard)
                    End Select
                End If
            End If
        Next index

        UndealDCard(upcard)

        'Now calculate splits 
        If SPL > 0 Then
            For card = 1 To 10
                If SplitIndex(card, upcard) > 0 Then
                    index = SplitIndex(card, upcard)
                    Call ComputeSplit(cStrat, card, upcard)

                    'Now double check the strategies vs splitting
                    If Not cStrat.HandEVs(index).PreForced(upcard) Then
                        'CD Hands
                        newEvs = GetStratMaxEVHand(index, upcard, cStrat, cStrat, True, True)
                        If (cStrat.NCardsCD <> 0 And PlayerHands(index).Hand.NumCards <= cStrat.NCardsCD) Then
                            cStrat.HandEVs(index).EVs.Strat(upcard) = newEvs.Strat(upcard)
                            cStrat.HandEVs(index).EVs.StratEV(upcard) = newEvs.StratEV(upcard)
                        Else
                            'Total Dependent Hands
                            Select Case newEvs.Strat(upcard)
                                Case BJCAGlobalsClass.Strat.P, BJCAGlobalsClass.Strat.PS, BJCAGlobalsClass.Strat.PH, BJCAGlobalsClass.Strat.PD, BJCAGlobalsClass.Strat.PR
                                    cStrat.HandEVs(index).EVs.Strat(upcard) = newEvs.Strat(upcard)
                                    cStrat.HandEVs(index).EVs.StratEV(upcard) = newEvs.StratEV(upcard)
                            End Select
                        End If
                    End If
                End If
            Next
        End If

        'Remove the second strategy for TD pairs
        For card = 1 To 10
            If SplitIndex(card, upcard) > 0 Then
                If Not cStrat.HandEVs(SplitIndex(card, upcard)).PreForced(upcard) Then
                    Select Case cStrat.HandEVs(SplitIndex(card, upcard)).EVs.Strat(upcard)
                        Case BJCAGlobalsClass.Strat.P, BJCAGlobalsClass.Strat.PS, BJCAGlobalsClass.Strat.PH, BJCAGlobalsClass.Strat.PD, BJCAGlobalsClass.Strat.PR
                            'Fix the Soft 12 Strat while we're at it
                            If card = 1 And (HSA Or DSA Or SSA) Then
                                If cStrat.StratTD(12, True + 1).Strat(upcard) = BJCAGlobalsClass.Strat.H And Not HSA Then
                                    cStrat.StratTD(12, True + 1).Strat(upcard) = BJCAGlobalsClass.Strat.None
                                ElseIf cStrat.StratTD(12, True + 1).Strat(upcard) = BJCAGlobalsClass.Strat.D And Not DSA Then
                                    cStrat.StratTD(12, True + 1).Strat(upcard) = BJCAGlobalsClass.Strat.None
                                ElseIf cStrat.StratTD(12, True + 1).Strat(upcard) = BJCAGlobalsClass.Strat.R And Not SSA Then
                                    cStrat.StratTD(12, True + 1).Strat(upcard) = BJCAGlobalsClass.Strat.None
                                End If
                            ElseIf card = 1 Then
                                cStrat.StratTD(12, True + 1).Strat(upcard) = BJCAGlobalsClass.Strat.None
                            End If
                            If cStrat.NCardsCD = 0 Then
                                cStrat.HandEVs(SplitIndex(card, upcard)).EVs.Strat(upcard) = BJCAGlobalsClass.Strat.P
                            End If
                    End Select
                End If
            End If
        Next


    End Sub

#End Region

#Region " Game EV Methods "

    Public Sub ComputeGameEVsStrat(ByRef cStrat As BJCAStrategyClass)
        Dim card As Integer
        Dim card2 As Integer
        Dim upcard As Integer
        Dim prob As Double
        Dim bjadjprob As Double
        Dim cev As Double
        Dim bjev As Double
        Dim index As Integer

        CurrentPHand.Empty()
        CurrentDHand.Hand.Empty()
        CurrentShoe.Reset(OriginalShoe)

        cStrat.GameEVs.NetGameEV = 0
        For card = 1 To 10
            cStrat.GameEVs.CardProbs(card) = CardProb(card, 0)
            cStrat.GameEVs.UpcardEVs(card) = 0
            cStrat.GameEVs.FirstCardEVs(card) = 0
        Next card

        'Cycle through all 2 card hands to figure out net game ev's
        For card = 1 To 10
            If CardProb(card, 0) > 0 Then
                prob = CardProb(card, 0)
                DealPCard(card)
                For card2 = card To 10
                    If CardProb(card2, 0) > 0 Then
                        prob = prob * CardProb(card2, 0)
                        DealPCard(card2)
                        If card <> card2 Then prob = prob * 2
                        For upcard = 1 To 10
                            If UCAllowed(upcard) And CardProb(upcard, 0) > 0 Then
                                prob = prob * CardProb(upcard, 0)
                                DealDCard(upcard)
                                'Non-dealer Checking BJ's already accounted for
                                If (upcard = 1 And CheckAce) Then
                                    bjadjprob = CardProb(10, 0)
                                ElseIf (upcard = 10 And CheckTen) Then
                                    bjadjprob = CardProb(1, 0)
                                Else
                                    bjadjprob = 0
                                End If
                                index = FindPlayerHand(CurrentPHand)

                                If PlayerHands(index).SuitedBonusEVs Is Nothing Then
                                    bjev = PlayerHands(index).HandEVs.BJStandEV(upcard)

                                    Select Case cStrat.HandEVs(index).EVs.Strat(upcard)
                                        Case BJCAGlobalsClass.Strat.S
                                            cev = PlayerHands(index).HandEVs.StandEV(upcard)
                                        Case BJCAGlobalsClass.Strat.D, BJCAGlobalsClass.Strat.DS
                                            cev = PlayerHands(index).HandEVs.DEV(upcard)
                                        Case BJCAGlobalsClass.Strat.R, BJCAGlobalsClass.Strat.RS
                                            cev = PlayerHands(index).HandEVs.SurrEV(upcard)
                                        Case BJCAGlobalsClass.Strat.H
                                            cev = cStrat.HandEVs(index).EVs.HitEV(upcard)
                                        Case BJCAGlobalsClass.Strat.P, BJCAGlobalsClass.Strat.PS, BJCAGlobalsClass.Strat.PD, BJCAGlobalsClass.Strat.PH, BJCAGlobalsClass.Strat.PR
                                            cev = cStrat.HandEVs(index).SplitEV(upcard)
                                        Case Else
                                            cev = 0
                                    End Select
                                    'Bonus EVs of 2 card hands need to be added here
                                    cev += PlayerHands(index).HandEVs.BonusEV(upcard)
                                Else
                                    'Bonus EVs are already included in the SuitedNetEV
                                    bjev = PlayerHands(index).SuitedBonusEVs.SuitedNetBJEV(upcard)
                                    cev = PlayerHands(index).SuitedBonusEVs.SuitedNetEV(upcard)
                                End If

                                cStrat.GameEVs.NetGameEV += prob * ((1 - bjadjprob) * cev + bjadjprob * bjev)
                                cStrat.GameEVs.UpcardEVs(upcard) += prob * ((1 - bjadjprob) * cev + bjadjprob * bjev)
                                If card = card2 Then
                                    cStrat.GameEVs.FirstCardEVs(card) += prob * ((1 - bjadjprob) * cev + bjadjprob * bjev)
                                Else
                                    cStrat.GameEVs.FirstCardEVs(card) += prob / 2 * ((1 - bjadjprob) * cev + bjadjprob * bjev)
                                    cStrat.GameEVs.FirstCardEVs(card2) += prob / 2 * ((1 - bjadjprob) * cev + bjadjprob * bjev)
                                End If

                                UndealDCard(upcard)
                                prob = prob / CardProb(upcard, 0)
                            End If
                        Next upcard
                        UndealPCard(card2)
                        If card <> card2 Then prob = prob / 2
                        prob = prob / CardProb(card2, 0)
                    End If
                Next card2
                UndealPCard(card)
            End If
        Next card

        For card = 1 To 10
            If cStrat.GameEVs.CardProbs(card) > 0 Then
                cStrat.GameEVs.UpcardEVs(card) /= cStrat.GameEVs.CardProbs(card)
                cStrat.GameEVs.FirstCardEVs(card) /= cStrat.GameEVs.CardProbs(card)
            End If
        Next card
    End Sub

    Private Sub ComputeGameEVs()
        ComputeGameEVsStrat(Opt)
        If TD.ComputeStrat Then ComputeGameEVsStrat(TD)
        If TC.ComputeStrat Then ComputeGameEVsStrat(TC)
        If Forced.ComputeStrat Then ComputeGameEVsStrat(Forced)

    End Sub

#End Region

#End Region

#Region " Exceptions Methods "

    Private Sub ComputeExceptions()
        Dim index As Integer
        Dim upcard As Integer
        Dim tdstrat As Integer
        Dim strat2 As Integer
        Dim Total As Integer
        Dim Soft As Integer
        Dim paircard As Integer
        Dim hands As Integer
        Dim newException As New BJCAExceptionClass

        'CD-TD Pre-split Exceptions
        If TD.ComputeStrat Then
            For upcard = 1 To 10
                If UCAllowed(upcard) Then
                    DealDCard(upcard)
                    For Total = 4 To 21
                        For Soft = 1 To 0 Step -1
                            index = PlayerHandTotal(Total, Soft)
                            Do While index
                                If PlayerHands(index).HandEVs.Prob(upcard) > 0 And PlayerHands(index).Hand.NumCards >= 2 Then
                                    tdstrat = 0
                                    If PlayerHands(index).Hand.NumCards = 2 And (TD.HandEVs(index).EVs.Strat(upcard) = BJCAGlobalsClass.Strat.P Or TD.HandEVs(index).EVs.Strat(upcard) = BJCAGlobalsClass.Strat.PS Or TD.HandEVs(index).EVs.Strat(upcard) = BJCAGlobalsClass.Strat.PH Or TD.HandEVs(index).EVs.Strat(upcard) = BJCAGlobalsClass.Strat.PD Or TD.HandEVs(index).EVs.Strat(upcard) = BJCAGlobalsClass.Strat.PR) Then
                                        If PlayerHands(index).Hand.Total = 12 And PlayerHands(index).Hand.Soft Then
                                            If HSA = False Then
                                                tdstrat = BJCAGlobalsClass.Strat.P
                                            End If
                                        Else
                                            Select Case TD.StratTD(Total, Soft).Strat(upcard)
                                                Case BJCAGlobalsClass.Strat.S
                                                    tdstrat = BJCAGlobalsClass.Strat.PS
                                                Case BJCAGlobalsClass.Strat.D, BJCAGlobalsClass.Strat.DS
                                                    tdstrat = BJCAGlobalsClass.Strat.PD
                                                Case BJCAGlobalsClass.Strat.H
                                                    tdstrat = BJCAGlobalsClass.Strat.PH
                                                Case BJCAGlobalsClass.Strat.R, BJCAGlobalsClass.Strat.RS
                                                    tdstrat = BJCAGlobalsClass.Strat.PR
                                                Case Else
                                                    tdstrat = TD.StratTD(Total, Soft).Strat(upcard)
                                            End Select
                                        End If
                                    Else
                                        tdstrat = TD.StratTD(Total, Soft).Strat(upcard)
                                        If Not TD.HandEVs(index).DPreallowed(upcard) Then
                                            Select Case tdstrat
                                                Case BJCAGlobalsClass.Strat.D
                                                    tdstrat = BJCAGlobalsClass.Strat.H
                                                Case BJCAGlobalsClass.Strat.DS
                                                    tdstrat = BJCAGlobalsClass.Strat.S
                                            End Select
                                        End If
                                        If Not TD.HandEVs(index).RPreallowed(upcard) Then
                                            Select Case tdstrat
                                                Case BJCAGlobalsClass.Strat.R
                                                    tdstrat = BJCAGlobalsClass.Strat.H
                                                Case BJCAGlobalsClass.Strat.RS
                                                    tdstrat = BJCAGlobalsClass.Strat.S
                                            End Select
                                        End If
                                    End If

                                    If Opt.HandEVs(index).EVs.Strat(upcard) <> tdstrat And Opt.HandEVs(index).EVs.Strat(upcard) <> 0 Then
                                        newException.Empty()
                                        newException.Index = index
                                        newException.Upcard = upcard
                                        newException.Paircard = 0
                                        newException.ExceptionType = BJCAGlobalsClass.ExType.CDTDPre
                                        newException.SplitHand = 0
                                        newException.ShoeState = "Pre-Split"
                                        newException.ExceptionName = BJCAShared.GetHandString(PlayerHands(index).Hand) + " v " + CStr(upcard) + " " + C.StratShortText(Opt.HandEVs(index).EVs.Strat(upcard)) + " Pre-Split"
                                        ExceptionsList.AddException(newException)
                                    End If
                                End If
                                index = PlayerHands(index).NextHand
                            Loop
                        Next Soft
                    Next Total
                    UndealDCard(upcard)
                End If
            Next upcard
        End If

        'CD-Forced Pre-Split Exceptions
        If Forced.ComputeStrat Then
            For upcard = 1 To 10
                If UCAllowed(upcard) Then
                    DealDCard(upcard)
                    For Total = 4 To 21
                        For Soft = 1 To 0 Step -1
                            index = PlayerHandTotal(Total, Soft)
                            Do While index
                                If PlayerHands(index).HandEVs.Prob(upcard) > 0 And PlayerHands(index).Hand.NumCards >= 2 Then
                                    If PlayerHands(index).Hand.NumCards > Forced.NCardsCD Then
                                        tdstrat = 0
                                        If PlayerHands(index).Hand.NumCards = 2 And (Forced.HandEVs(index).EVs.Strat(upcard) = BJCAGlobalsClass.Strat.P Or Forced.HandEVs(index).EVs.Strat(upcard) = BJCAGlobalsClass.Strat.PS Or Forced.HandEVs(index).EVs.Strat(upcard) = BJCAGlobalsClass.Strat.PH Or Forced.HandEVs(index).EVs.Strat(upcard) = BJCAGlobalsClass.Strat.PD Or Forced.HandEVs(index).EVs.Strat(upcard) = BJCAGlobalsClass.Strat.PR) Then
                                            If PlayerHands(index).Hand.Total = 12 And PlayerHands(index).Hand.Soft Then
                                                If HSA = False Then
                                                    tdstrat = BJCAGlobalsClass.Strat.P
                                                End If
                                            Else
                                                Select Case Forced.StratTD(Total, Soft).Strat(upcard)
                                                    Case BJCAGlobalsClass.Strat.S
                                                        tdstrat = BJCAGlobalsClass.Strat.PS
                                                    Case BJCAGlobalsClass.Strat.D, BJCAGlobalsClass.Strat.DS
                                                        tdstrat = BJCAGlobalsClass.Strat.PD
                                                    Case BJCAGlobalsClass.Strat.H
                                                        tdstrat = BJCAGlobalsClass.Strat.PH
                                                    Case BJCAGlobalsClass.Strat.R, BJCAGlobalsClass.Strat.RS
                                                        tdstrat = BJCAGlobalsClass.Strat.PR
                                                    Case Else
                                                        tdstrat = Forced.StratTD(Total, Soft).Strat(upcard)
                                                End Select
                                            End If
                                        Else
                                            tdstrat = Forced.StratTD(Total, Soft).Strat(upcard)
                                            If Not Forced.HandEVs(index).DPreallowed(upcard) Then
                                                Select Case tdstrat
                                                    Case BJCAGlobalsClass.Strat.D
                                                        tdstrat = BJCAGlobalsClass.Strat.H
                                                    Case BJCAGlobalsClass.Strat.DS
                                                        tdstrat = BJCAGlobalsClass.Strat.S
                                                End Select
                                            End If
                                            If Not Forced.HandEVs(index).RPreallowed(upcard) Then
                                                Select Case tdstrat
                                                    Case BJCAGlobalsClass.Strat.R
                                                        tdstrat = BJCAGlobalsClass.Strat.H
                                                    Case BJCAGlobalsClass.Strat.RS
                                                        tdstrat = BJCAGlobalsClass.Strat.S
                                                End Select
                                            End If
                                        End If

                                        If Opt.HandEVs(index).EVs.Strat(upcard) <> tdstrat And Opt.HandEVs(index).EVs.Strat(upcard) <> 0 Then
                                            newException.Empty()
                                            newException.Index = index
                                            newException.Upcard = upcard
                                            newException.Paircard = 0
                                            newException.ExceptionType = BJCAGlobalsClass.ExType.CDForcedPre
                                            newException.SplitHand = 0
                                            newException.ShoeState = "Pre-Split"
                                            newException.ExceptionName = BJCAShared.GetHandString(PlayerHands(index).Hand) + " v " + CStr(upcard) + " " + C.StratShortText(Opt.HandEVs(index).EVs.Strat(upcard)) + " Pre-Split"
                                            ExceptionsList.AddException(newException)
                                        End If
                                    Else
                                        If Opt.HandEVs(index).EVs.Strat(upcard) <> Forced.HandEVs(index).EVs.Strat(upcard) And Opt.HandEVs(index).EVs.Strat(upcard) <> 0 Then
                                            newException.Empty()
                                            newException.Index = index
                                            newException.Upcard = upcard
                                            newException.Paircard = 0
                                            newException.ExceptionType = BJCAGlobalsClass.ExType.CDForcedPre
                                            newException.SplitHand = 0
                                            newException.ShoeState = "Pre-Split"
                                            newException.ExceptionName = BJCAShared.GetHandString(PlayerHands(index).Hand) + " v " + CStr(upcard) + " " + C.StratShortText(Opt.HandEVs(index).EVs.Strat(upcard)) + " Pre-Split"
                                            ExceptionsList.AddException(newException)
                                        End If
                                    End If
                                End If
                                index = PlayerHands(index).NextHand
                            Loop
                        Next Soft
                    Next Total
                    UndealDCard(upcard)
                End If
            Next upcard
        End If

        'Now look for post-split exceptions
        If (CDP Or CDPN) Then
            'CDP-TD Exceptions
            If TD.ComputeStrat Then
                For upcard = 1 To 10
                    If UCAllowed(upcard) Then
                        DealDCard(upcard)
                        For Total = 4 To 21
                            For Soft = 1 To 0 Step -1
                                For paircard = 1 To 10
                                    If SplitIndex(paircard, upcard) > 0 Then
                                        For hands = 1 To NSplitHands
                                            If NPxHands(hands).Used(SPL) And (NPxHands(hands).NN = 0 Or CDPN) Then
                                                index = PlayerHandTotal(Total, Soft)
                                                Do While index
                                                    If PlayerHands(index).HandEVs.Prob(upcard) > 0 And PlayerHands(index).PairIndex(paircard) > 0 Then
                                                        If Not Opt.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), hands) Is Nothing Then
                                                            If Opt.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), hands).Strat(upcard) <> 0 And PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), hands).StandEV(upcard) <> 0 Then
                                                                tdstrat = 0
                                                                If PlayerHands(index).Hand.NumCards = 2 Then
                                                                    Select Case TD.StratTD(Total, Soft).Strat(upcard)
                                                                        Case BJCAGlobalsClass.Strat.PS
                                                                            tdstrat = BJCAGlobalsClass.Strat.S
                                                                        Case BJCAGlobalsClass.Strat.PD
                                                                            tdstrat = BJCAGlobalsClass.Strat.D
                                                                        Case BJCAGlobalsClass.Strat.PH
                                                                            tdstrat = BJCAGlobalsClass.Strat.H
                                                                        Case BJCAGlobalsClass.Strat.PR
                                                                            tdstrat = BJCAGlobalsClass.Strat.R
                                                                        Case Else
                                                                            tdstrat = TD.StratTD(Total, Soft).Strat(upcard)
                                                                    End Select
                                                                Else
                                                                    tdstrat = TD.StratTD(Total, Soft).Strat(upcard)
                                                                End If
                                                                If Not TD.HandEVs(index).DPostallowed(upcard) Then
                                                                    Select Case tdstrat
                                                                        Case BJCAGlobalsClass.Strat.D
                                                                            tdstrat = BJCAGlobalsClass.Strat.H
                                                                        Case BJCAGlobalsClass.Strat.DS
                                                                            tdstrat = BJCAGlobalsClass.Strat.S
                                                                    End Select
                                                                End If
                                                                If Not TD.HandEVs(index).RPostallowed(upcard) Then
                                                                    Select Case tdstrat
                                                                        Case BJCAGlobalsClass.Strat.R
                                                                            tdstrat = BJCAGlobalsClass.Strat.H
                                                                        Case BJCAGlobalsClass.Strat.RS
                                                                            tdstrat = BJCAGlobalsClass.Strat.S
                                                                    End Select
                                                                End If

                                                                Select Case Opt.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), hands).Strat(upcard)
                                                                    Case BJCAGlobalsClass.Strat.PS
                                                                        strat2 = BJCAGlobalsClass.Strat.S
                                                                    Case BJCAGlobalsClass.Strat.PD
                                                                        strat2 = BJCAGlobalsClass.Strat.D
                                                                    Case BJCAGlobalsClass.Strat.PH
                                                                        strat2 = BJCAGlobalsClass.Strat.H
                                                                    Case BJCAGlobalsClass.Strat.PR
                                                                        strat2 = BJCAGlobalsClass.Strat.R
                                                                    Case Else
                                                                        strat2 = Opt.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), hands).Strat(upcard)
                                                                End Select
                                                                If strat2 <> tdstrat Then
                                                                    newException.Empty()
                                                                    newException.Index = index
                                                                    newException.Upcard = upcard
                                                                    newException.Paircard = paircard
                                                                    newException.ExceptionType = BJCAGlobalsClass.ExType.CDTDPost
                                                                    newException.SplitHand = hands
                                                                    newException.ShoeState = CStr(paircard) + "  " + NPxHands(hands).Name
                                                                    newException.ExceptionName = BJCAShared.GetHandString(PlayerHands(index).Hand) + " v " + CStr(upcard) + " " + C.StratShortText(strat2) + " Post-Split " + CStr(paircard) + "  " + NPxHands(hands).Name
                                                                    ExceptionsList.AddException(newException)
                                                                End If
                                                            End If
                                                        End If
                                                    End If
                                                    index = PlayerHands(index).NextHand
                                                Loop
                                            End If
                                        Next hands
                                    End If
                                Next paircard
                            Next Soft
                        Next Total
                        UndealDCard(upcard)
                    End If
                Next upcard
            End If

            'CDP-Forced Exceptions
            If Forced.ComputeStrat Then
                For upcard = 1 To 10
                    If UCAllowed(upcard) Then
                        DealDCard(upcard)
                        For Total = 4 To 21
                            For Soft = 1 To 0 Step -1
                                For paircard = 1 To 10
                                    If SplitIndex(paircard, upcard) > 0 Then
                                        For hands = 1 To NSplitHands
                                            If NPxHands(hands).Used(SPL) And (NPxHands(hands).NN = 0 Or CDPN) Then
                                                index = PlayerHandTotal(Total, Soft)
                                                Do While index
                                                    If PlayerHands(index).HandEVs.Prob(upcard) > 0 And PlayerHands(index).PairIndex(paircard) > 0 Then
                                                        If Not Opt.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), hands) Is Nothing Then
                                                            If Opt.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), hands).Strat(upcard) <> 0 And PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), hands).StandEV(upcard) <> 0 Then
                                                                If PlayerHands(index).Hand.NumCards > Forced.NCardsCD Then
                                                                    tdstrat = 0
                                                                    If PlayerHands(index).Hand.NumCards = 2 Then
                                                                        Select Case Forced.StratTD(Total, Soft).Strat(upcard)
                                                                            Case BJCAGlobalsClass.Strat.PS
                                                                                tdstrat = BJCAGlobalsClass.Strat.S
                                                                            Case BJCAGlobalsClass.Strat.PD
                                                                                tdstrat = BJCAGlobalsClass.Strat.D
                                                                            Case BJCAGlobalsClass.Strat.PH
                                                                                tdstrat = BJCAGlobalsClass.Strat.H
                                                                            Case BJCAGlobalsClass.Strat.PR
                                                                                tdstrat = BJCAGlobalsClass.Strat.R
                                                                            Case Else
                                                                                tdstrat = Forced.StratTD(Total, Soft).Strat(upcard)
                                                                        End Select
                                                                    Else
                                                                        tdstrat = Forced.StratTD(Total, Soft).Strat(upcard)
                                                                    End If
                                                                    If Not Forced.HandEVs(index).DPostallowed(upcard) Then
                                                                        Select Case tdstrat
                                                                            Case BJCAGlobalsClass.Strat.D
                                                                                tdstrat = BJCAGlobalsClass.Strat.H
                                                                            Case BJCAGlobalsClass.Strat.DS
                                                                                tdstrat = BJCAGlobalsClass.Strat.S
                                                                        End Select
                                                                    End If
                                                                    If Not Forced.HandEVs(index).RPostallowed(upcard) Then
                                                                        Select Case tdstrat
                                                                            Case BJCAGlobalsClass.Strat.R
                                                                                tdstrat = BJCAGlobalsClass.Strat.H
                                                                            Case BJCAGlobalsClass.Strat.RS
                                                                                tdstrat = BJCAGlobalsClass.Strat.S
                                                                        End Select
                                                                    End If

                                                                    Select Case Opt.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), hands).Strat(upcard)
                                                                        Case BJCAGlobalsClass.Strat.PS
                                                                            strat2 = BJCAGlobalsClass.Strat.S
                                                                        Case BJCAGlobalsClass.Strat.PD
                                                                            strat2 = BJCAGlobalsClass.Strat.D
                                                                        Case BJCAGlobalsClass.Strat.PH
                                                                            strat2 = BJCAGlobalsClass.Strat.H
                                                                        Case BJCAGlobalsClass.Strat.PR
                                                                            strat2 = BJCAGlobalsClass.Strat.R
                                                                        Case Else
                                                                            strat2 = Opt.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), hands).Strat(upcard)
                                                                    End Select
                                                                    If strat2 <> tdstrat Then
                                                                        newException.Empty()
                                                                        newException.Index = index
                                                                        newException.Upcard = upcard
                                                                        newException.Paircard = paircard
                                                                        newException.ExceptionType = BJCAGlobalsClass.ExType.CDTDPost
                                                                        newException.SplitHand = hands
                                                                        newException.ShoeState = CStr(paircard) + "  " + NPxHands(hands).Name
                                                                        newException.ExceptionName = BJCAShared.GetHandString(PlayerHands(index).Hand) + " v " + CStr(upcard) + " " + C.StratShortText(strat2) + " Post-Split " + CStr(paircard) + "  " + NPxHands(hands).Name
                                                                        ExceptionsList.AddException(newException)
                                                                    End If
                                                                Else
                                                                    Select Case Forced.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), hands).Strat(upcard)
                                                                        Case BJCAGlobalsClass.Strat.PS
                                                                            tdstrat = BJCAGlobalsClass.Strat.S
                                                                        Case BJCAGlobalsClass.Strat.PD
                                                                            tdstrat = BJCAGlobalsClass.Strat.D
                                                                        Case BJCAGlobalsClass.Strat.PH
                                                                            tdstrat = BJCAGlobalsClass.Strat.H
                                                                        Case BJCAGlobalsClass.Strat.PR
                                                                            tdstrat = BJCAGlobalsClass.Strat.R
                                                                        Case Else
                                                                            tdstrat = Forced.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), hands).Strat(upcard)
                                                                    End Select
                                                                    Select Case Opt.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), hands).Strat(upcard)
                                                                        Case BJCAGlobalsClass.Strat.PS
                                                                            strat2 = BJCAGlobalsClass.Strat.S
                                                                        Case BJCAGlobalsClass.Strat.PD
                                                                            strat2 = BJCAGlobalsClass.Strat.D
                                                                        Case BJCAGlobalsClass.Strat.PH
                                                                            strat2 = BJCAGlobalsClass.Strat.H
                                                                        Case BJCAGlobalsClass.Strat.PR
                                                                            strat2 = BJCAGlobalsClass.Strat.R
                                                                        Case Else
                                                                            strat2 = Opt.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), hands).Strat(upcard)
                                                                    End Select
                                                                    If strat2 <> tdstrat Then
                                                                        newException.Empty()
                                                                        newException.Index = index
                                                                        newException.Upcard = upcard
                                                                        newException.Paircard = paircard
                                                                        newException.ExceptionType = BJCAGlobalsClass.ExType.CDForcedPost
                                                                        newException.SplitHand = hands
                                                                        newException.ShoeState = CStr(paircard) + "  " + NPxHands(hands).Name
                                                                        newException.ExceptionName = BJCAShared.GetHandString(PlayerHands(index).Hand) + " v " + CStr(upcard) + " " + C.StratShortText(strat2) + " Post-Split " + CStr(paircard) + "  " + NPxHands(hands).Name
                                                                        ExceptionsList.AddException(newException)
                                                                    End If
                                                                End If
                                                            End If
                                                        End If
                                                    End If
                                                    index = PlayerHands(index).NextHand
                                                Loop
                                            End If
                                        Next hands
                                    End If
                                Next paircard
                            Next Soft
                        Next Total
                        UndealDCard(upcard)
                    End If
                Next upcard
            End If

            'CDP-CD Exceptions
            For upcard = 1 To 10
                If UCAllowed(upcard) Then
                    DealDCard(upcard)
                    For Total = 4 To 21
                        For Soft = 1 To 0 Step -1
                            For paircard = 1 To 10
                                If SplitIndex(paircard, upcard) > 0 Then
                                    For hands = 1 To NSplitHands
                                        If NPxHands(hands).Used(SPL) And (NPxHands(hands).NN = 0 Or CDPN) Then
                                            index = PlayerHandTotal(Total, Soft)
                                            Do While index
                                                If PlayerHands(index).HandEVs.Prob(upcard) > 0 And PlayerHands(index).PairIndex(paircard) > 0 Then
                                                    If Not Opt.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), hands) Is Nothing Then
                                                        If Opt.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), hands).Strat(upcard) <> 0 And PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), hands).StandEV(upcard) <> 0 Then
                                                            If Opt.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), hands).Strat(upcard) <> Opt.HandEVs(index).EVs.Strat(upcard) Then
                                                                newException.Empty()
                                                                newException.Index = index
                                                                newException.Upcard = upcard
                                                                newException.Paircard = paircard
                                                                newException.ExceptionType = BJCAGlobalsClass.ExType.CDCDPost
                                                                newException.SplitHand = hands
                                                                newException.ShoeState = CStr(paircard) + "  " + NPxHands(hands).Name
                                                                newException.ExceptionName = BJCAShared.GetHandString(PlayerHands(index).Hand) + " v " + CStr(upcard) + " " + C.StratShortText(Opt.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), hands).Strat(upcard)) + " Post-Split " + CStr(paircard) + "  " + NPxHands(hands).Name
                                                                ExceptionsList.AddException(newException)
                                                            End If
                                                        End If
                                                    End If
                                                End If
                                                index = PlayerHands(index).NextHand
                                            Loop
                                        End If
                                    Next hands
                                End If
                            Next paircard
                        Next Soft
                    Next Total
                    UndealDCard(upcard)
                End If
            Next upcard
        End If

    End Sub

    Private Sub ComputeNCardExceptions()
        Dim total As Integer
        Dim soft As Integer
        Dim upcard As Integer
        Dim nCards As Integer
        Dim stratSame As Boolean
        Dim strat As New BJCAStrategyClass
        Dim newException As New BJCANCardExceptionClass

        If TD.ComputeStrat Then
            For total = 4 To 21
                For soft = 0 To 1
                    For upcard = 1 To 10
                        For nCards = 2 To 21
                            If Not TD.StratTD(total, soft) Is Nothing Then
                                If TD.StratTD(total, soft).NetProb(upcard) > 0 And TD.StratTD(total, soft).Strat(upcard) <> C.Strat.None Then
                                    Dim newEvs As New BJCATDStratClass

                                    newEvs = ComputeNCardStratEVs(TD, total, soft, upcard, nCards, False, False, True)

                                    Select Case TD.StratTD(total, soft).Strat(upcard)
                                        Case C.Strat.D
                                            If newevs.NetDProb(upcard) = 0 Then
                                                stratSame = (newEvs.Strat(upcard) = C.Strat.H)
                                            Else
                                                stratSame = (newEvs.Strat(upcard) = C.Strat.D)
                                            End If
                                        Case C.Strat.DS
                                            If newevs.NetDProb(upcard) = 0 Then
                                                stratSame = (newEvs.Strat(upcard) = C.Strat.S)
                                            Else
                                                stratSame = (newEvs.Strat(upcard) = C.Strat.D)
                                            End If
                                        Case C.Strat.R
                                            If newevs.NetSurrProb(upcard) = 0 Then
                                                stratSame = (newEvs.Strat(upcard) = C.Strat.H)
                                            Else
                                                stratSame = (newEvs.Strat(upcard) = C.Strat.R)
                                            End If
                                        Case C.Strat.RS
                                            If newevs.NetSurrProb(upcard) = 0 Then
                                                stratSame = (newEvs.Strat(upcard) = C.Strat.S)
                                            Else
                                                stratSame = (newEvs.Strat(upcard) = C.Strat.R)
                                            End If
                                        Case Else
                                            stratSame = (newEvs.Strat(upcard) = TD.StratTD(total, soft).Strat(upcard))
                                    End Select
                                    If newEvs.NetProb(upcard) <> 0 And Not stratSame Then
                                        newException.Total = total
                                        newException.Soft = (soft = 0)
                                        newException.Upcard = upcard
                                        newException.NCards = nCards

                                        newException.Strat = newEvs.Strat(upcard)
                                        newException.SEV = newEvs.StratStandEV(upcard)
                                        newException.HEV = newEvs.StratHitEV(upcard)
                                        newException.DEV = newEvs.StratDEV(upcard)
                                        newException.SurrEV = newEvs.StratSurrEV(upcard)
                                        newException.ExceptionType = BJCAGlobalsClass.ExType.TDTD
                                        newException.ExceptionName = CStr(nCards) + "-Card "
                                        If soft = 0 Then
                                            newException.ExceptionName += "S"
                                        Else
                                            newException.ExceptionName += "H"
                                        End If
                                        newException.ExceptionName += CStr(total) + " v " + CStr(upcard) + " " + C.StratShortText(newEvs.Strat(upcard))

                                        NCardExceptionsList.AddException(newException)
                                    End If

                                    newEvs = Nothing
                                End If
                            End If
                        Next nCards
                    Next upcard
                Next soft
            Next total
        End If

        If TC.ComputeStrat Then
            For total = 4 To 21
                For soft = 0 To 1
                    For upcard = 1 To 10
                        For nCards = 2 To 21
                            If Not TC.StratTD(total, soft) Is Nothing Then
                                If TC.StratTD(total, soft).NetProb(upcard) > 0 And TC.StratTD(total, soft).Strat(upcard) <> C.Strat.None Then
                                    Dim newEvs As New BJCATDStratClass

                                    newEvs = ComputeNCardStratEVs(TC, total, soft, upcard, nCards, False, False, True)
                                    Select Case TC.StratTD(total, soft).Strat(upcard)
                                        Case C.Strat.D
                                            If newevs.NetDProb(upcard) = 0 Then
                                                stratSame = (newEvs.Strat(upcard) = C.Strat.H)
                                            Else
                                                stratSame = (newEvs.Strat(upcard) = C.Strat.D)
                                            End If
                                        Case C.Strat.DS
                                            If newevs.NetDProb(upcard) = 0 Then
                                                stratSame = (newEvs.Strat(upcard) = C.Strat.S)
                                            Else
                                                stratSame = (newEvs.Strat(upcard) = C.Strat.D)
                                            End If
                                        Case C.Strat.R
                                            If newevs.NetSurrProb(upcard) = 0 Then
                                                stratSame = (newEvs.Strat(upcard) = C.Strat.H)
                                            Else
                                                stratSame = (newEvs.Strat(upcard) = C.Strat.R)
                                            End If
                                        Case C.Strat.RS
                                            If newevs.NetSurrProb(upcard) = 0 Then
                                                stratSame = (newEvs.Strat(upcard) = C.Strat.S)
                                            Else
                                                stratSame = (newEvs.Strat(upcard) = C.Strat.R)
                                            End If
                                        Case Else
                                            stratSame = (newEvs.Strat(upcard) = TC.StratTD(total, soft).Strat(upcard))
                                    End Select
                                    If newEvs.NetProb(upcard) <> 0 And Not stratSame Then
                                        newException.Total = total
                                        newException.Soft = (soft = 0)
                                        newException.Upcard = upcard
                                        newException.NCards = nCards

                                        newException.Strat = newEvs.Strat(upcard)
                                        newException.SEV = newEvs.StratStandEV(upcard)
                                        newException.HEV = newEvs.StratHitEV(upcard)
                                        newException.DEV = newEvs.StratDEV(upcard)
                                        newException.SurrEV = newEvs.StratSurrEV(upcard)
                                        newException.ExceptionType = BJCAGlobalsClass.ExType.TCTC
                                        newException.ExceptionName = CStr(nCards) + "-Card "
                                        If soft = 0 Then
                                            newException.ExceptionName += "S"
                                        Else
                                            newException.ExceptionName += "H"
                                        End If
                                        newException.ExceptionName += CStr(total) + " v " + CStr(upcard) + " " + C.StratShortText(newEvs.Strat(upcard))

                                        NCardExceptionsList.AddException(newException)
                                    End If

                                    newEvs = Nothing
                                End If
                            End If
                        Next nCards
                    Next upcard
                Next soft
            Next total
        End If

        If Forced.ComputeStrat Then
            For total = 4 To 21
                For soft = 0 To 1
                    For upcard = 1 To 10
                        For nCards = 2 To 21
                            If Not Forced.StratTD(total, soft) Is Nothing Then
                                If Forced.StratTD(total, soft).NetProb(upcard) > 0 And Forced.StratTD(total, soft).Strat(upcard) <> C.Strat.None Then
                                    Dim newEvs As New BJCATDStratClass

                                    newEvs = ComputeNCardStratEVs(Forced, total, soft, upcard, nCards, False, False, True)
                                    Select Case Forced.StratTD(total, soft).Strat(upcard)
                                        Case C.Strat.D
                                            If newevs.NetDProb(upcard) = 0 Then
                                                stratSame = (newEvs.Strat(upcard) = C.Strat.H)
                                            Else
                                                stratSame = (newEvs.Strat(upcard) = C.Strat.D)
                                            End If
                                        Case C.Strat.DS
                                            If newevs.NetDProb(upcard) = 0 Then
                                                stratSame = (newEvs.Strat(upcard) = C.Strat.S)
                                            Else
                                                stratSame = (newEvs.Strat(upcard) = C.Strat.D)
                                            End If
                                        Case C.Strat.R
                                            If newevs.NetSurrProb(upcard) = 0 Then
                                                stratSame = (newEvs.Strat(upcard) = C.Strat.H)
                                            Else
                                                stratSame = (newEvs.Strat(upcard) = C.Strat.R)
                                            End If
                                        Case C.Strat.RS
                                            If newevs.NetSurrProb(upcard) = 0 Then
                                                stratSame = (newEvs.Strat(upcard) = C.Strat.S)
                                            Else
                                                stratSame = (newEvs.Strat(upcard) = C.Strat.R)
                                            End If
                                        Case Else
                                            stratSame = (newEvs.Strat(upcard) = Forced.StratTD(total, soft).Strat(upcard))
                                    End Select
                                    If newEvs.NetProb(upcard) <> 0 And Not stratSame Then
                                        newException.Total = total
                                        newException.Soft = (soft = 0)
                                        newException.Upcard = upcard
                                        newException.NCards = nCards

                                        newException.Strat = newEvs.Strat(upcard)
                                        newException.SEV = newEvs.StratStandEV(upcard)
                                        newException.HEV = newEvs.StratHitEV(upcard)
                                        newException.DEV = newEvs.StratDEV(upcard)
                                        newException.SurrEV = newEvs.StratSurrEV(upcard)
                                        newException.ExceptionType = BJCAGlobalsClass.ExType.ForcedForced
                                        newException.ExceptionName = CStr(nCards) + "-Card "
                                        If soft = 0 Then
                                            newException.ExceptionName += "S"
                                        Else
                                            newException.ExceptionName += "H"
                                        End If
                                        newException.ExceptionName += CStr(total) + " v " + CStr(upcard) + " " + C.StratShortText(newEvs.Strat(upcard))

                                        NCardExceptionsList.AddException(newException)
                                    End If

                                    newEvs = Nothing
                                End If
                            End If
                        Next nCards
                    Next upcard
                Next soft
            Next total
        End If

    End Sub

#End Region

#Region " Output Methods "

    Public Sub PrintToExcel()
        Dim oexcel As New Excel.Application
        Dim oexcelbook As Excel.Workbook

        Try
            oexcel = GetObject(, "Exel.Application")
            Try
                oexcelbook = oexcel.Workbooks(ExcelFilePath)
            Catch
                oexcelbook = oexcel.Workbooks.Open(ExcelFilePath)
            End Try
        Catch
            oexcelbook = oexcel.Workbooks.Open(ExcelFilePath)
        End Try
        PrintStrategiesExcel(oexcelbook)
        PrintSplitEVsExcel(oexcelbook)
        PrintGameEVsExcel(oexcelbook)
        PrintStrategyEVsExcel(oexcelbook)
        PrintExceptionsExcel(oexcelbook)
        PrintRulesExcel(oexcelbook)

        oexcelbook.Worksheets("Strategy Table").Select()

        oexcel.Visible = True
        oexcelbook = Nothing
        oexcel = Nothing
    End Sub

    Private Sub PrintStrategiesExcel(ByVal workBook As Excel.Workbook)
        Dim worksheet As Excel.Worksheet
        Dim Total As Integer
        Dim upcard As Integer
        Dim card As Integer
        Dim card2 As Integer
        Dim index As Integer
        Dim column As Integer
        Dim row As Integer

        worksheet = workBook.Worksheets("Strategy Table")
        worksheet.Select()
        worksheet.Range("C4:L21").ClearContents()
        worksheet.Range("C4:L21").Interior.Color = RGB(255, 255, 255)
        worksheet.Range("C25:L34").ClearContents()
        worksheet.Range("C25:L34").Interior.Color = RGB(255, 255, 255)
        worksheet.Range("C38:L47").ClearContents()
        worksheet.Range("C38:L47").Interior.Color = RGB(255, 255, 255)
        worksheet.Range("C51:L86").ClearContents()
        worksheet.Range("C51:L86").Interior.Color = RGB(255, 255, 255)
        worksheet.Range("C90:L98").ClearContents()
        worksheet.Range("C90:L98").Interior.Color = RGB(255, 255, 255)
        worksheet.Range("C102:L111").ClearContents()
        worksheet.Range("C102:L111").Interior.Color = RGB(255, 255, 255)
        worksheet.Range("C115:L132").ClearContents()
        worksheet.Range("C115:L132").Interior.Color = RGB(255, 255, 255)
        worksheet.Range("C136:L145").ClearContents()
        worksheet.Range("C136:L145").Interior.Color = RGB(255, 255, 255)
        worksheet.Range("C149:L184").ClearContents()
        worksheet.Range("C149:L184").Interior.Color = RGB(255, 255, 255)
        worksheet.Range("C188:L196").ClearContents()
        worksheet.Range("C188:L196").Interior.Color = RGB(255, 255, 255)
        worksheet.Range("C200:L209").ClearContents()
        worksheet.Range("C200:L209").Interior.Color = RGB(255, 255, 255)

        worksheet.Range("C213:L248").ClearContents()
        worksheet.Range("C213:L248").Interior.Color = RGB(255, 255, 255)
        worksheet.Range("C252:L260").ClearContents()
        worksheet.Range("C252:L260").Interior.Color = RGB(255, 255, 255)
        worksheet.Range("C264:L273").ClearContents()
        worksheet.Range("C264:L273").Interior.Color = RGB(255, 255, 255)
        worksheet.Range("C277:L294").ClearContents()
        worksheet.Range("C277:L294").Interior.Color = RGB(255, 255, 255)
        worksheet.Range("C298:L307").ClearContents()
        worksheet.Range("C298:L307").Interior.Color = RGB(255, 255, 255)
        worksheet.Range("A1").Select()

        'Fill in color table in worksheet
        worksheet.Range("O4").Interior.Color = RGB(ColorTable.C(BJCAGlobalsClass.Strat.None).R, ColorTable.C(BJCAGlobalsClass.Strat.None).G, ColorTable.C(BJCAGlobalsClass.Strat.None).B)
        worksheet.Range("O5").Interior.Color = RGB(ColorTable.C(BJCAGlobalsClass.Strat.S).R, ColorTable.C(BJCAGlobalsClass.Strat.S).G, ColorTable.C(BJCAGlobalsClass.Strat.S).B)
        worksheet.Range("O6").Interior.Color = RGB(ColorTable.C(BJCAGlobalsClass.Strat.H).R, ColorTable.C(BJCAGlobalsClass.Strat.H).G, ColorTable.C(BJCAGlobalsClass.Strat.H).B)
        worksheet.Range("O7").Interior.Color = RGB(ColorTable.C(BJCAGlobalsClass.Strat.D).R, ColorTable.C(BJCAGlobalsClass.Strat.D).G, ColorTable.C(BJCAGlobalsClass.Strat.D).B)
        worksheet.Range("O8").Interior.Color = RGB(ColorTable.C(BJCAGlobalsClass.Strat.DS).R, ColorTable.C(BJCAGlobalsClass.Strat.DS).G, ColorTable.C(BJCAGlobalsClass.Strat.DS).B)
        worksheet.Range("O9").Interior.Color = RGB(ColorTable.C(BJCAGlobalsClass.Strat.R).R, ColorTable.C(BJCAGlobalsClass.Strat.R).G, ColorTable.C(BJCAGlobalsClass.Strat.R).B)
        worksheet.Range("O10").Interior.Color = RGB(ColorTable.C(BJCAGlobalsClass.Strat.RS).R, ColorTable.C(BJCAGlobalsClass.Strat.RS).G, ColorTable.C(BJCAGlobalsClass.Strat.RS).B)
        worksheet.Range("O11").Interior.Color = RGB(ColorTable.C(BJCAGlobalsClass.Strat.P).R, ColorTable.C(BJCAGlobalsClass.Strat.P).G, ColorTable.C(BJCAGlobalsClass.Strat.P).B)
        worksheet.Range("O12").Interior.Color = RGB(ColorTable.C(BJCAGlobalsClass.Strat.PS).R, ColorTable.C(BJCAGlobalsClass.Strat.PS).G, ColorTable.C(BJCAGlobalsClass.Strat.PS).B)
        worksheet.Range("O13").Interior.Color = RGB(ColorTable.C(BJCAGlobalsClass.Strat.PH).R, ColorTable.C(BJCAGlobalsClass.Strat.PH).G, ColorTable.C(BJCAGlobalsClass.Strat.PH).B)
        worksheet.Range("O14").Interior.Color = RGB(ColorTable.C(BJCAGlobalsClass.Strat.PD).R, ColorTable.C(BJCAGlobalsClass.Strat.PD).G, ColorTable.C(BJCAGlobalsClass.Strat.PD).B)
        worksheet.Range("O15").Interior.Color = RGB(ColorTable.C(BJCAGlobalsClass.Strat.PR).R, ColorTable.C(BJCAGlobalsClass.Strat.PR).G, ColorTable.C(BJCAGlobalsClass.Strat.PR).B)

        CurrentShoe.Reset(OriginalShoe)
        CurrentPHand.Empty()

        'Print Total Dependent Hard Strategies
        If TD.ComputeStrat Then
            For Total = 4 To 21
                For upcard = 1 To 10
                    If UCAllowed(upcard) Then
                        If upcard = 1 Then
                            column = 9
                        Else
                            column = upcard - 2
                        End If
                        worksheet.Range("C4").Offset(Total - 4, column).Formula = C.StratLongText(TD.StratTD(Total, False + 1).Strat(upcard))
                        worksheet.Range("C4").Offset(Total - 4, column).Interior.Color = RGB(ColorTable.C(TD.StratTD(Total, False + 1).Strat(upcard)).R, ColorTable.C(TD.StratTD(Total, False + 1).Strat(upcard)).G, ColorTable.C(TD.StratTD(Total, False + 1).Strat(upcard)).B)
                    End If
                Next upcard
            Next Total

            'Print Total Dependent Soft Hands
            For Total = 12 To 21
                For upcard = 1 To 10
                    If UCAllowed(upcard) Then
                        If upcard = 1 Then
                            column = 9
                        Else
                            column = upcard - 2
                        End If
                        worksheet.Range("C25").Offset(Total - 12, column).Formula = C.StratLongText(TD.StratTD(Total, True + 1).Strat(upcard))
                        worksheet.Range("C25").Offset(Total - 12, column).Interior.Color = RGB(ColorTable.C(TD.StratTD(Total, True + 1).Strat(upcard)).R, ColorTable.C(TD.StratTD(Total, True + 1).Strat(upcard)).G, ColorTable.C(TD.StratTD(Total, True + 1).Strat(upcard)).B)
                    End If
                Next upcard
            Next Total

            'Print Total Dependent Splits
            If SPL > 0 Then
                For card = 1 To 10
                    For upcard = 1 To 10
                        If CurrentShoe.Cards(card) >= 2 And UCAllowed(upcard) Then
                            DealPCard(card)
                            DealPCard(card)
                            index = FindPlayerHand(CurrentPHand)
                            UndealPCard(card)
                            UndealPCard(card)
                            If upcard = 1 Then
                                column = 9
                            Else
                                column = upcard - 2
                            End If
                            If (TD.HandEVs(index).EVs.Strat(upcard) = BJCAGlobalsClass.Strat.P Or TD.HandEVs(index).EVs.Strat(upcard) = BJCAGlobalsClass.Strat.PS Or TD.HandEVs(index).EVs.Strat(upcard) = BJCAGlobalsClass.Strat.PH Or TD.HandEVs(index).EVs.Strat(upcard) = BJCAGlobalsClass.Strat.PD Or TD.HandEVs(index).EVs.Strat(upcard) = BJCAGlobalsClass.Strat.PR) Then
                                worksheet.Range("C38").Offset(card - 1, column).Formula = C.StratLongText(TD.HandEVs(index).EVs.Strat(upcard))
                                worksheet.Range("C38").Offset(card - 1, column).Interior.Color = RGB(ColorTable.C(TD.HandEVs(index).EVs.Strat(upcard)).R, ColorTable.C(TD.HandEVs(index).EVs.Strat(upcard)).G, ColorTable.C(TD.HandEVs(index).EVs.Strat(upcard)).B)
                            Else
                                worksheet.Range("C38").Offset(card - 1, column).Formula = C.StratLongText(0)
                                worksheet.Range("C38").Offset(card - 1, column).Interior.Color = RGB(ColorTable.C(0).R, ColorTable.C(0).G, ColorTable.C(0).B)
                            End If
                        End If
                    Next upcard
                Next card
            End If
        End If

        'Print 2 Card Dependent Hard Strategies
        If TC.ComputeStrat Then
            row = 0
            For Total = 5 To 19
                If Total < 13 Then
                    card = 2
                Else
                    card = Total - 10
                End If
                card2 = Total - card
                Do
                    If CardProb(card, 0) > 0 Then
                        DealPCard(card)
                        If CardProb(card2, 0) > 0 Then
                            DealPCard(card2)
                            index = FindPlayerHand(CurrentPHand)
                            For upcard = 1 To 10
                                If UCAllowed(upcard) Then
                                    If upcard = 1 Then
                                        column = 9
                                    Else
                                        column = upcard - 2
                                    End If
                                    worksheet.Range("C51").Offset(row, column).Formula = C.StratLongText(TC.HandEVs(index).EVs.Strat(upcard))
                                    worksheet.Range("C51").Offset(row, column).Interior.Color = RGB(ColorTable.C(TC.HandEVs(index).EVs.Strat(upcard)).R, ColorTable.C(TC.HandEVs(index).EVs.Strat(upcard)).G, ColorTable.C(TC.HandEVs(index).EVs.Strat(upcard)).B)
                                End If
                            Next upcard
                            UndealPCard(card2)
                        End If
                        UndealPCard(card)
                    End If
                    row = row + 1
                    card = card + 1
                    card2 = card2 - 1
                Loop Until (card >= card2)
            Next Total

            'Print 2 Card Dependent Soft Hands
            If CardProb(1, 0) > 0 Then
                DealPCard(1)
                index = FindPlayerHand(CurrentPHand)
                For card = 2 To 10
                    If CardProb(card, 0) > 0 Then
                        DealPCard(card)
                        For upcard = 1 To 10
                            If UCAllowed(upcard) Then
                                If upcard = 1 Then
                                    column = 9
                                Else
                                    column = upcard - 2
                                End If
                                worksheet.Range("C90").Offset(card - 2, column).Formula = C.StratLongText(TC.HandEVs(PlayerHands(index).HitHand(card)).EVs.Strat(upcard))
                                worksheet.Range("C90").Offset(card - 2, column).Interior.Color = RGB(ColorTable.C(TC.HandEVs(PlayerHands(index).HitHand(card)).EVs.Strat(upcard)).R, ColorTable.C(TC.HandEVs(PlayerHands(index).HitHand(card)).EVs.Strat(upcard)).G, ColorTable.C(TC.HandEVs(PlayerHands(index).HitHand(card)).EVs.Strat(upcard)).B)
                            End If
                        Next upcard
                        UndealPCard(card)
                    End If
                Next card
                UndealPCard(1)
            End If

            'Print 2 Card Dependent Splits
            For card = 1 To 10
                For upcard = 1 To 10
                    If CurrentShoe.Cards(card) >= 2 And UCAllowed(upcard) Then
                        DealPCard(card)
                        DealPCard(card)
                        index = FindPlayerHand(CurrentPHand)
                        UndealPCard(card)
                        UndealPCard(card)
                        If upcard = 1 Then
                            column = 9
                        Else
                            column = upcard - 2
                        End If
                        worksheet.Range("C102").Offset(card - 1, column).Formula = C.StratLongText(TC.HandEVs(index).EVs.Strat(upcard))
                        worksheet.Range("C102").Offset(card - 1, column).Interior.Color = RGB(ColorTable.C(TC.HandEVs(index).EVs.Strat(upcard)).R, ColorTable.C(TC.HandEVs(index).EVs.Strat(upcard)).G, ColorTable.C(TC.HandEVs(index).EVs.Strat(upcard)).B)
                    End If
                Next upcard
            Next card

            'Print 2 Card Dependent Hard Strategies
            For Total = 4 To 21
                For upcard = 1 To 10
                    If UCAllowed(upcard) Then
                        If upcard = 1 Then
                            column = 9
                        Else
                            column = upcard - 2
                        End If
                        worksheet.Range("C115").Offset(Total - 4, column).Formula = C.StratLongText(TC.StratTD(Total, False + 1).Strat(upcard))
                        worksheet.Range("C115").Offset(Total - 4, column).Interior.Color = RGB(ColorTable.C(TC.StratTD(Total, False + 1).Strat(upcard)).R, ColorTable.C(TC.StratTD(Total, False + 1).Strat(upcard)).G, ColorTable.C(TC.StratTD(Total, False + 1).Strat(upcard)).B)
                    End If
                Next upcard
            Next Total

            'Print 2 Card Dependent Soft Hands
            For Total = 12 To 21
                For upcard = 1 To 10
                    If UCAllowed(upcard) Then
                        If upcard = 1 Then
                            column = 9
                        Else
                            column = upcard - 2
                        End If
                        worksheet.Range("C136").Offset(Total - 12, column).Formula = C.StratLongText(TC.StratTD(Total, True + 1).Strat(upcard))
                        worksheet.Range("C136").Offset(Total - 12, column).Interior.Color = RGB(ColorTable.C(TC.StratTD(Total, True + 1).Strat(upcard)).R, ColorTable.C(TC.StratTD(Total, True + 1).Strat(upcard)).G, ColorTable.C(TC.StratTD(Total, True + 1).Strat(upcard)).B)
                    End If
                Next upcard
            Next Total
        End If

        'Print Optimal Play Hard Strategies
        row = 0
        For Total = 5 To 19
            If Total < 13 Then
                card = 2
            Else
                card = Total - 10
            End If
            card2 = Total - card
            Do
                If CardProb(card, 0) > 0 Then
                    DealPCard(card)
                    If CardProb(card2, 0) > 0 Then
                        DealPCard(card2)
                        index = FindPlayerHand(CurrentPHand)
                        For upcard = 1 To 10
                            If UCAllowed(upcard) Then
                                If upcard = 1 Then
                                    column = 9
                                Else
                                    column = upcard - 2
                                End If
                                worksheet.Range("C149").Offset(row, column).Formula = C.StratLongText(Opt.HandEVs(index).EVs.Strat(upcard))
                                worksheet.Range("C149").Offset(row, column).Interior.Color = RGB(ColorTable.C(Opt.HandEVs(index).EVs.Strat(upcard)).R, ColorTable.C(Opt.HandEVs(index).EVs.Strat(upcard)).G, ColorTable.C(Opt.HandEVs(index).EVs.Strat(upcard)).B)
                            End If
                        Next upcard
                        UndealPCard(card2)
                    End If
                    UndealPCard(card)
                End If
                row = row + 1
                card = card + 1
                card2 = card2 - 1
            Loop Until (card >= card2)
        Next Total

        'Print Optimal Play Soft Hands
        If CardProb(1, 0) > 0 Then
            DealPCard(1)
            index = FindPlayerHand(CurrentPHand)
            For card = 2 To 10
                If CardProb(card, 0) > 0 Then
                    DealPCard(card)
                    For upcard = 1 To 10
                        If UCAllowed(upcard) Then
                            If upcard = 1 Then
                                column = 9
                            Else
                                column = upcard - 2
                            End If
                            worksheet.Range("C188").Offset(card - 2, column).Formula = C.StratLongText(Opt.HandEVs(PlayerHands(index).HitHand(card)).EVs.Strat(upcard))
                            worksheet.Range("C188").Offset(card - 2, column).Interior.Color = RGB(ColorTable.C(Opt.HandEVs(PlayerHands(index).HitHand(card)).EVs.Strat(upcard)).R, ColorTable.C(Opt.HandEVs(PlayerHands(index).HitHand(card)).EVs.Strat(upcard)).G, ColorTable.C(Opt.HandEVs(PlayerHands(index).HitHand(card)).EVs.Strat(upcard)).B)
                        End If
                    Next upcard
                    UndealPCard(card)
                End If
            Next card
            UndealPCard(1)
        End If

        'Print Optimal Play Splits
        For card = 1 To 10
            For upcard = 1 To 10
                If CurrentShoe.Cards(card) >= 2 And UCAllowed(upcard) Then
                    DealPCard(card)
                    DealPCard(card)
                    index = FindPlayerHand(CurrentPHand)
                    UndealPCard(card)
                    UndealPCard(card)
                    If upcard = 1 Then
                        column = 9
                    Else
                        column = upcard - 2
                    End If
                    worksheet.Range("C200").Offset(card - 1, column).Formula = C.StratLongText(Opt.HandEVs(index).EVs.Strat(upcard))
                    worksheet.Range("C200").Offset(card - 1, column).Interior.Color = RGB(ColorTable.C(Opt.HandEVs(index).EVs.Strat(upcard)).R, ColorTable.C(Opt.HandEVs(index).EVs.Strat(upcard)).G, ColorTable.C(Opt.HandEVs(index).EVs.Strat(upcard)).B)
                End If
            Next upcard
        Next card

        'Print Forced Dependent Hard Strategies
        If Forced.ComputeStrat Then
            row = 0
            For Total = 5 To 19
                If Total < 13 Then
                    card = 2
                Else
                    card = Total - 10
                End If
                card2 = Total - card
                Do
                    If CardProb(card, 0) > 0 Then
                        DealPCard(card)
                        If CardProb(card2, 0) > 0 Then
                            DealPCard(card2)
                            index = FindPlayerHand(CurrentPHand)
                            For upcard = 1 To 10
                                If UCAllowed(upcard) Then
                                    If upcard = 1 Then
                                        column = 9
                                    Else
                                        column = upcard - 2
                                    End If
                                    worksheet.Range("C213").Offset(row, column).Formula = C.StratLongText(Forced.HandEVs(index).EVs.Strat(upcard))
                                    worksheet.Range("C213").Offset(row, column).Interior.Color = RGB(ColorTable.C(Forced.HandEVs(index).EVs.Strat(upcard)).R, ColorTable.C(Forced.HandEVs(index).EVs.Strat(upcard)).G, ColorTable.C(Forced.HandEVs(index).EVs.Strat(upcard)).B)
                                End If
                            Next upcard
                            UndealPCard(card2)
                        End If
                        UndealPCard(card)
                    End If
                    row = row + 1
                    card = card + 1
                    card2 = card2 - 1
                Loop Until (card >= card2)
            Next Total

            'Print Forced Dependent Soft Hands
            If CardProb(1, 0) > 0 Then
                DealPCard(1)
                index = FindPlayerHand(CurrentPHand)
                For card = 2 To 10
                    If CardProb(card, 0) > 0 Then
                        DealPCard(card)
                        For upcard = 1 To 10
                            If UCAllowed(upcard) Then
                                If upcard = 1 Then
                                    column = 9
                                Else
                                    column = upcard - 2
                                End If
                                worksheet.Range("C252").Offset(card - 2, column).Formula = C.StratLongText(Forced.HandEVs(PlayerHands(index).HitHand(card)).EVs.Strat(upcard))
                                worksheet.Range("C252").Offset(card - 2, column).Interior.Color = RGB(ColorTable.C(Forced.HandEVs(PlayerHands(index).HitHand(card)).EVs.Strat(upcard)).R, ColorTable.C(Forced.HandEVs(PlayerHands(index).HitHand(card)).EVs.Strat(upcard)).G, ColorTable.C(Forced.HandEVs(PlayerHands(index).HitHand(card)).EVs.Strat(upcard)).B)
                            End If
                        Next upcard
                        UndealPCard(card)
                    End If
                Next card
                UndealPCard(1)
            End If

            'Print Forced Dependent Splits
            For card = 1 To 10
                For upcard = 1 To 10
                    If CurrentShoe.Cards(card) >= 2 And UCAllowed(upcard) Then
                        DealPCard(card)
                        DealPCard(card)
                        index = FindPlayerHand(CurrentPHand)
                        UndealPCard(card)
                        UndealPCard(card)
                        If upcard = 1 Then
                            column = 9
                        Else
                            column = upcard - 2
                        End If
                        worksheet.Range("C264").Offset(card - 1, column).Formula = C.StratLongText(Forced.HandEVs(index).EVs.Strat(upcard))
                        worksheet.Range("C264").Offset(card - 1, column).Interior.Color = RGB(ColorTable.C(Forced.HandEVs(index).EVs.Strat(upcard)).R, ColorTable.C(Forced.HandEVs(index).EVs.Strat(upcard)).G, ColorTable.C(Forced.HandEVs(index).EVs.Strat(upcard)).B)
                    End If
                Next upcard
            Next card

            'Print Forced Dependent Hard Strategies
            For Total = 4 To 21
                For upcard = 1 To 10
                    If UCAllowed(upcard) Then
                        If upcard = 1 Then
                            column = 9
                        Else
                            column = upcard - 2
                        End If
                        worksheet.Range("C277").Offset(Total - 4, column).Formula = C.StratLongText(Forced.StratTD(Total, False + 1).Strat(upcard))
                        worksheet.Range("C277").Offset(Total - 4, column).Interior.Color = RGB(ColorTable.C(Forced.StratTD(Total, False + 1).Strat(upcard)).R, ColorTable.C(Forced.StratTD(Total, False + 1).Strat(upcard)).G, ColorTable.C(Forced.StratTD(Total, False + 1).Strat(upcard)).B)
                    End If
                Next upcard
            Next Total

            'Print Forced Dependent Soft Hands
            For Total = 12 To 21
                For upcard = 1 To 10
                    If UCAllowed(upcard) Then
                        If upcard = 1 Then
                            column = 9
                        Else
                            column = upcard - 2
                        End If
                        worksheet.Range("C298").Offset(Total - 12, column).Formula = C.StratLongText(Forced.StratTD(Total, True + 1).Strat(upcard))
                        worksheet.Range("C298").Offset(Total - 12, column).Interior.Color = RGB(ColorTable.C(Forced.StratTD(Total, True + 1).Strat(upcard)).R, ColorTable.C(Forced.StratTD(Total, True + 1).Strat(upcard)).G, ColorTable.C(Forced.StratTD(Total, True + 1).Strat(upcard)).B)
                    End If
                Next upcard
            Next Total
        End If

        worksheet = Nothing
    End Sub

    Private Sub PrintStrategyEVsExcel(ByVal workBook As Excel.Workbook)
        Dim worksheet As Excel.Worksheet
        Dim Total As Integer
        Dim upcard As Integer
        Dim card As Integer
        Dim card2 As Integer
        Dim index As Integer
        Dim column As Integer
        Dim row As Integer
        Dim dprob As Double
        Dim prob As Double
        Dim p2 As Double

        worksheet = workBook.Worksheets("Strategy EVs")
        worksheet.Select()

        worksheet.Range("D3:E3").ClearContents()
        worksheet.Range("D8:M241").ClearContents()
        worksheet.Range("D246:M375").ClearContents()
        worksheet.Range("D380:M499").ClearContents()
        worksheet.Range("D504:M791").ClearContents()
        worksheet.Range("D796:M867").ClearContents()
        worksheet.Range("A1").Select()

        'Print Blackjack EVs

        dprob = CardProb(1, 0)
        prob = CardProb(1, 0)
        DealDCard(1)
        dprob = dprob * CardProb(10, 0)
        prob = prob * CardProb(10, 0)
        DealDCard(10)
        prob = prob * CardProb(1, 0)
        DealPCard(1)
        prob = prob * CardProb(10, 0)
        UndealDCard(1)
        UndealDCard(10)
        UndealPCard(1)
        worksheet.Range("D3").Formula = 2 * (dprob - prob)
        worksheet.Range("E3").Formula = 2 * prob
        worksheet.Range("F3").Formula = 2 * dprob

        'Print the remaining EVs
        For upcard = 1 To 10
            If UCAllowed(upcard) And CardProb(upcard, 0) > 0 Then
                If upcard = 1 Then
                    column = 9
                Else
                    column = upcard - 2
                End If
                dprob = CardProb(upcard, 0)
                DealDCard(upcard)

                'Print Total Dependent Hard Strategies
                worksheet.Range("D8").Select()
                For Total = 4 To 21
                    row = Total - 4
                    If TD.ComputeStrat Then
                        worksheet.Range("D8").Offset(row * 13, column).Formula = dprob * TD.StratTD(Total, False + 1).NetProb(upcard)
                        worksheet.Range("D8").Offset(row * 13 + 1, column).Formula = TD.StratTD(Total, False + 1).StratStandEV(upcard)
                        worksheet.Range("D8").Offset(row * 13 + 2, column).Formula = TD.StratTD(Total, False + 1).StratHitEV(upcard)
                        worksheet.Range("D8").Offset(row * 13 + 3, column).Formula = TD.StratTD(Total, False + 1).StratDEV(upcard)
                        worksheet.Range("D8").Offset(row * 13 + 4, column).Formula = TD.StratTD(Total, False + 1).StratSurrEV(upcard)
                    End If
                    If TC.ComputeStrat Then
                        worksheet.Range("D8").Offset(row * 13 + 5, column).Formula = TC.StratTD(Total, False + 1).StratStandEV(upcard)
                        worksheet.Range("D8").Offset(row * 13 + 6, column).Formula = TC.StratTD(Total, False + 1).StratHitEV(upcard)
                        worksheet.Range("D8").Offset(row * 13 + 7, column).Formula = TC.StratTD(Total, False + 1).StratDEV(upcard)
                        worksheet.Range("D8").Offset(row * 13 + 8, column).Formula = TC.StratTD(Total, False + 1).StratSurrEV(upcard)
                    End If
                    If Forced.ComputeStrat Then
                        worksheet.Range("D8").Offset(row * 13 + 9, column).Formula = Forced.StratTD(Total, False + 1).StratStandEV(upcard)
                        worksheet.Range("D8").Offset(row * 13 + 10, column).Formula = Forced.StratTD(Total, False + 1).StratHitEV(upcard)
                        worksheet.Range("D8").Offset(row * 13 + 11, column).Formula = Forced.StratTD(Total, False + 1).StratDEV(upcard)
                        worksheet.Range("D8").Offset(row * 13 + 12, column).Formula = Forced.StratTD(Total, False + 1).StratSurrEV(upcard)
                    End If
                Next Total

                'Print Total Dependent Soft Hands
                For Total = 12 To 21
                    row = Total - 12
                    If TD.ComputeStrat Then
                        worksheet.Range("D246").Offset(row * 13, column).Formula = dprob * TD.StratTD(Total, True + 1).NetProb(upcard)
                        worksheet.Range("D246").Offset(row * 13 + 1, column).Formula = TD.StratTD(Total, True + 1).StratStandEV(upcard)
                        worksheet.Range("D246").Offset(row * 13 + 2, column).Formula = TD.StratTD(Total, True + 1).StratHitEV(upcard)
                        worksheet.Range("D246").Offset(row * 13 + 3, column).Formula = TD.StratTD(Total, True + 1).StratDEV(upcard)
                        worksheet.Range("D246").Offset(row * 13 + 4, column).Formula = TD.StratTD(Total, True + 1).StratSurrEV(upcard)
                    End If
                    If TC.ComputeStrat Then
                        worksheet.Range("D246").Offset(row * 13 + 5, column).Formula = TC.StratTD(Total, True + 1).StratStandEV(upcard)
                        worksheet.Range("D246").Offset(row * 13 + 6, column).Formula = TC.StratTD(Total, True + 1).StratHitEV(upcard)
                        worksheet.Range("D246").Offset(row * 13 + 7, column).Formula = TC.StratTD(Total, True + 1).StratDEV(upcard)
                        worksheet.Range("D246").Offset(row * 13 + 8, column).Formula = TC.StratTD(Total, True + 1).StratSurrEV(upcard)
                    End If
                    If Forced.ComputeStrat Then
                        worksheet.Range("D246").Offset(row * 13 + 9, column).Formula = Forced.StratTD(Total, True + 1).StratStandEV(upcard)
                        worksheet.Range("D246").Offset(row * 13 + 10, column).Formula = Forced.StratTD(Total, True + 1).StratHitEV(upcard)
                        worksheet.Range("D246").Offset(row * 13 + 11, column).Formula = Forced.StratTD(Total, True + 1).StratDEV(upcard)
                        worksheet.Range("D246").Offset(row * 13 + 12, column).Formula = Forced.StratTD(Total, True + 1).StratSurrEV(upcard)
                    End If
                Next Total

                'Print Split EVs
                CurrentPHand.Empty()
                For card = 1 To 10
                    If CardProb(card, 0) > 0 Then
                        prob = dprob * CardProb(card, upcard)
                        DealPCard(card)
                        If CardProb(card, 0) > 0 Then
                            prob = prob * CardProb(card, upcard)
                            DealPCard(card)
                            index = FindPlayerHand(CurrentPHand)
                            row = card - 1
                            worksheet.Range("D380").Offset(row * 12, column).Formula = prob
                            If SPL > 0 Then
                                If SplitAllowed(card) Then
                                    If TD.ComputeStrat Then worksheet.Range("D380").Offset(row * 12 + 1, column).Formula = TD.HandEVs(index).SplitEV(upcard)
                                    If TC.ComputeStrat Then worksheet.Range("D380").Offset(row * 12 + 2, column).Formula = TC.HandEVs(index).SplitEV(upcard)
                                    worksheet.Range("D380").Offset(row * 12 + 3, column).Formula = Opt.HandEVs(index).SplitEV(upcard)
                                    If Forced.ComputeStrat Then worksheet.Range("D380").Offset(row * 12 + 4, column).Formula = Forced.HandEVs(index).SplitEV(upcard)
                                End If
                            End If
                            worksheet.Range("D380").Offset(row * 12 + 5, column).Formula = PlayerHands(index).HandEVs.StandEV(upcard)
                            worksheet.Range("D380").Offset(row * 12 + 6, column).Formula = PlayerHands(index).HandEVs.DEV(upcard)
                            worksheet.Range("D380").Offset(row * 12 + 7, column).Formula = PlayerHands(index).HandEVs.SurrEV(upcard)
                            If TD.ComputeStrat Then worksheet.Range("D380").Offset(row * 12 + 8, column).Formula = TD.HandEVs(index).EVs.HitEV(upcard)
                            If TC.ComputeStrat Then worksheet.Range("D380").Offset(row * 12 + 9, column).Formula = TC.HandEVs(index).EVs.HitEV(upcard)
                            worksheet.Range("D380").Offset(row * 12 + 10, column).Formula = Opt.HandEVs(index).EVs.HitEV(upcard)
                            If Forced.ComputeStrat Then worksheet.Range("D380").Offset(row * 12 + 11, column).Formula = Forced.HandEVs(index).EVs.HitEV(upcard)
                            UndealPCard(card)
                        End If
                        UndealPCard(card)
                    End If
                Next card

                'Print 2 card and Optimal Play Hard EVs
                CurrentPHand.Empty()
                row = 0
                For Total = 5 To 19
                    If Total < 13 Then
                        card = 2
                    Else
                        card = Total - 10
                    End If
                    card2 = Total - card
                    Do
                        If CardProb(card, 0) > 0 Then
                            prob = dprob * CardProb(card, upcard)
                            DealPCard(card)
                            If CardProb(card2, 0) > 0 Then
                                prob = prob * CardProb(card2, upcard)
                                UndealPCard(card)
                                p2 = dprob * CardProb(card2, upcard)
                                DealPCard(card2)
                                p2 = p2 * CardProb(card, upcard)
                                DealPCard(card)
                                index = FindPlayerHand(CurrentPHand)
                                worksheet.Range("D504").Offset(row * 8, column).Formula = prob + p2
                                worksheet.Range("D504").Offset(row * 8 + 1, column).Formula = PlayerHands(index).HandEVs.StandEV(upcard)
                                worksheet.Range("D504").Offset(row * 8 + 2, column).Formula = PlayerHands(index).HandEVs.DEV(upcard)
                                worksheet.Range("D504").Offset(row * 8 + 3, column).Formula = PlayerHands(index).HandEVs.SurrEV(upcard)
                                If TD.ComputeStrat Then worksheet.Range("D504").Offset(row * 8 + 4, column).Formula = TD.HandEVs(index).EVs.HitEV(upcard)
                                If TC.ComputeStrat Then worksheet.Range("D504").Offset(row * 8 + 5, column).Formula = TC.HandEVs(index).EVs.HitEV(upcard)
                                worksheet.Range("D504").Offset(row * 8 + 6, column).Formula = Opt.HandEVs(index).EVs.HitEV(upcard)
                                If Forced.ComputeStrat Then worksheet.Range("D504").Offset(row * 8 + 7, column).Formula = Forced.HandEVs(index).EVs.HitEV(upcard)
                                UndealPCard(card2)
                            End If
                            UndealPCard(card)
                        End If
                        row = row + 1
                        card = card + 1
                        card2 = card2 - 1
                    Loop Until (card >= card2)
                Next Total

                'Print 2 Card and Optimal Play Soft Hand EVs
                CurrentPHand.Empty()
                If CardProb(1, 0) > 0 Then
                    prob = dprob * CardProb(1, upcard)
                    DealPCard(1)
                    For card = 2 To 10
                        If CardProb(card, 0) > 0 Then
                            prob = prob * CardProb(card, upcard)
                            UndealPCard(1)
                            p2 = dprob * CardProb(card, upcard)
                            DealPCard(card)
                            p2 = p2 * CardProb(1, upcard)
                            DealPCard(1)
                            index = FindPlayerHand(CurrentPHand)
                            row = card - 2
                            worksheet.Range("D796").Offset(row * 8, column).Formula = prob + p2
                            worksheet.Range("D796").Offset(row * 8 + 1, column).Formula = PlayerHands(index).HandEVs.StandEV(upcard)
                            worksheet.Range("D796").Offset(row * 8 + 2, column).Formula = PlayerHands(index).HandEVs.DEV(upcard)
                            worksheet.Range("D796").Offset(row * 8 + 3, column).Formula = PlayerHands(index).HandEVs.SurrEV(upcard)
                            If TD.ComputeStrat Then worksheet.Range("D796").Offset(row * 8 + 4, column).Formula = TD.HandEVs(index).EVs.HitEV(upcard)
                            If TC.ComputeStrat Then worksheet.Range("D796").Offset(row * 8 + 5, column).Formula = TC.HandEVs(index).EVs.HitEV(upcard)
                            worksheet.Range("D796").Offset(row * 8 + 6, column).Formula = Opt.HandEVs(index).EVs.HitEV(upcard)
                            If Forced.ComputeStrat Then worksheet.Range("D796").Offset(row * 8 + 7, column).Formula = Forced.HandEVs(index).EVs.HitEV(upcard)
                            UndealPCard(card)
                            prob = prob / CardProb(card, upcard)
                        End If
                    Next card
                    UndealPCard(1)
                End If
                UndealDCard(upcard)
            End If
        Next upcard

        worksheet = Nothing
    End Sub

    Private Sub PrintSplitEVsExcel(ByVal workBook As Excel.Workbook)
        Dim worksheet As Excel.Worksheet
        Dim paircard As Integer
        Dim upcard As Integer
        Dim splits As Integer
        Dim column As Integer
        Dim row As Integer
        Dim index As Integer

        worksheet = workBook.Worksheets("Split EVs")
        worksheet.Select()

        worksheet.Range("C3:L32").ClearContents()
        worksheet.Range("C36:L65").ClearContents()
        worksheet.Range("C69:L98").ClearContents()
        worksheet.Range("C102:L131").ClearContents()
        worksheet.Range("A1").Select()

        'Print Split EVs
        If SPL > 0 Then
            CurrentPHand.Empty()
            For paircard = 1 To 10
                If SplitAllowed(paircard) And CardProb(paircard, 0) > 0 Then
                    DealPCard(paircard)
                    If CardProb(paircard, 0) > 0 Then
                        DealPCard(paircard)
                        index = FindPlayerHand(CurrentPHand)
                        row = (paircard - 1) * 3
                        For upcard = 1 To 10
                            If UCAllowed(upcard) Then
                                If upcard = 1 Then
                                    column = 9
                                Else
                                    column = upcard - 2
                                End If
                                For splits = 1 To SPL
                                    If TD.ComputeStrat Then worksheet.Range("C3").Offset(row + splits - 1, column).Formula = TD.HandEVs(index).SPLEV(upcard, splits)
                                    worksheet.Range("C3").Offset(row + 33 + splits - 1, column).Formula = Opt.HandEVs(index).SPLEV(upcard, splits)
                                    If TC.ComputeStrat Then worksheet.Range("C3").Offset(row + 66 + splits - 1, column).Formula = TC.HandEVs(index).SPLEV(upcard, splits)
                                    If Forced.ComputeStrat Then worksheet.Range("C3").Offset(row + 99 + splits - 1, column).Formula = Forced.HandEVs(index).SPLEV(upcard, splits)
                                Next splits
                            End If
                        Next upcard
                        UndealPCard(paircard)
                    End If
                    UndealPCard(paircard)
                End If
            Next paircard
        End If

        worksheet = Nothing
    End Sub

    Private Sub PrintGameEVsExcel(ByVal workBook As Excel.Workbook)
        Dim worksheet As Excel.Worksheet
        Dim column As Integer
        Dim card As Integer
        Dim upcard As Integer

        worksheet = workBook.Worksheets("Summary")
        worksheet.Select()

        worksheet.Range("C2:C5").ClearContents()
        worksheet.Range("C8:L12").ClearContents()
        worksheet.Range("C15:L19").ClearContents()
        worksheet.Range("A1").Select()

        If TD.ComputeStrat Then worksheet.Range("C2").Formula = TD.GameEVs.NetGameEV
        If TC.ComputeStrat Then worksheet.Range("C3").Formula = TC.GameEVs.NetGameEV
        worksheet.Range("C4").Formula = Opt.GameEVs.NetGameEV
        If Forced.ComputeStrat Then worksheet.Range("C5").Formula = Forced.GameEVs.NetGameEV

        For upcard = 1 To 10
            If UCAllowed(upcard) And CardProb(upcard, 0) > 0 Then
                If upcard = 1 Then
                    column = 9
                Else
                    column = upcard - 2
                End If
                worksheet.Range("C8").Offset(0, column).Formula = Opt.GameEVs.CardProbs(upcard)
                If TD.ComputeStrat Then worksheet.Range("C8").Offset(1, column).Formula = TD.GameEVs.UpcardEVs(upcard)
                If TC.ComputeStrat Then worksheet.Range("C8").Offset(2, column).Formula = TC.GameEVs.UpcardEVs(upcard)
                worksheet.Range("C8").Offset(3, column).Formula = Opt.GameEVs.UpcardEVs(upcard)
                If Forced.ComputeStrat Then worksheet.Range("C8").Offset(4, column).Formula = Forced.GameEVs.UpcardEVs(upcard)
            End If
        Next upcard

        For card = 1 To 10
            If CardProb(card, 0) > 0 Then
                If card = 1 Then
                    column = 9
                Else
                    column = card - 2
                End If
                worksheet.Range("C15").Offset(0, column).Formula = Opt.GameEVs.CardProbs(card)
                If TD.ComputeStrat Then worksheet.Range("C15").Offset(1, column).Formula = TD.GameEVs.FirstCardEVs(card)
                If TC.ComputeStrat Then worksheet.Range("C15").Offset(2, column).Formula = TC.GameEVs.FirstCardEVs(card)
                worksheet.Range("C15").Offset(3, column).Formula = Opt.GameEVs.FirstCardEVs(card)
                If Forced.ComputeStrat Then worksheet.Range("C15").Offset(4, column).Formula = Forced.GameEVs.FirstCardEVs(card)
            End If
        Next card

        worksheet = Nothing
    End Sub

    Private Sub PrintExceptionsExcel(ByVal workBook As Excel.Workbook)
        Dim worksheet As Excel.Worksheet
        Dim index As Integer
        Dim upcard As Integer
        Dim card As Integer
        Dim handStr As String
        Dim n As Integer
        Dim n2 As Integer
        Dim n3 As Integer
        Dim i As Integer
        Dim tdstrat As Integer
        Dim strat2 As Integer
        Dim Total As Integer
        Dim Soft As Integer
        Dim paircard As Integer
        Dim hands As Integer

        worksheet = workBook.Worksheets("Exceptions")
        worksheet.Select()
        worksheet.Range("A6:R6").End(Excel.XlDirection.xlDown).ClearContents()

        If TD.ComputeStrat Then
            n = 0
            n2 = 0
            n3 = 0

            For upcard = 1 To 10
                If UCAllowed(upcard) Then
                    DealDCard(upcard)
                    For Total = 4 To 21
                        For Soft = 1 To 0 Step -1
                            index = PlayerHandTotal(Total, Soft)
                            Do While index
                                If HandPossible(PlayerHands(index).Hand) And PlayerHands(index).Hand.NumCards >= 2 Then
                                    tdstrat = 0
                                    If PlayerHands(index).Hand.NumCards = 2 And (TD.HandEVs(index).EVs.Strat(upcard) = BJCAGlobalsClass.Strat.P Or TD.HandEVs(index).EVs.Strat(upcard) = BJCAGlobalsClass.Strat.PS Or TD.HandEVs(index).EVs.Strat(upcard) = BJCAGlobalsClass.Strat.PH Or TD.HandEVs(index).EVs.Strat(upcard) = BJCAGlobalsClass.Strat.PD Or TD.HandEVs(index).EVs.Strat(upcard) = BJCAGlobalsClass.Strat.PR) Then
                                        Select Case TD.StratTD(Total, Soft).Strat(upcard)
                                            Case BJCAGlobalsClass.Strat.S
                                                tdstrat = BJCAGlobalsClass.Strat.PS
                                            Case BJCAGlobalsClass.Strat.D, BJCAGlobalsClass.Strat.DS
                                                tdstrat = BJCAGlobalsClass.Strat.PD
                                            Case BJCAGlobalsClass.Strat.H
                                                tdstrat = BJCAGlobalsClass.Strat.PH
                                            Case BJCAGlobalsClass.Strat.R, BJCAGlobalsClass.Strat.RS
                                                tdstrat = BJCAGlobalsClass.Strat.PR
                                            Case Else
                                                tdstrat = TD.StratTD(Total, Soft).Strat(upcard)
                                        End Select
                                    Else
                                        tdstrat = TD.StratTD(Total, Soft).Strat(upcard)
                                        If Not TD.HandEVs(index).DPreallowed(upcard) Then
                                            Select Case tdstrat
                                                Case BJCAGlobalsClass.Strat.D
                                                    tdstrat = BJCAGlobalsClass.Strat.H
                                                Case BJCAGlobalsClass.Strat.DS
                                                    tdstrat = BJCAGlobalsClass.Strat.S
                                            End Select
                                        End If
                                        If Not TD.HandEVs(index).RPreallowed(upcard) Then
                                            Select Case tdstrat
                                                Case BJCAGlobalsClass.Strat.R
                                                    tdstrat = BJCAGlobalsClass.Strat.H
                                                Case BJCAGlobalsClass.Strat.RS
                                                    tdstrat = BJCAGlobalsClass.Strat.S
                                            End Select
                                        End If
                                    End If

                                    If Opt.HandEVs(index).EVs.Strat(upcard) <> tdstrat And Opt.HandEVs(index).EVs.Strat(upcard) <> 0 Then
                                        handStr = ""
                                        For card = 1 To 10
                                            For i = 1 To PlayerHands(index).Hand.Cards(card)
                                                If card = 1 Then
                                                    handStr = handStr & "A"
                                                ElseIf card = 10 Then
                                                    handStr = handStr & "T"
                                                Else
                                                    handStr = handStr & Format(card)
                                                End If
                                            Next i
                                        Next card

                                        worksheet.Range("A7").Offset(n, 0).Formula = handStr
                                        worksheet.Range("A7").Offset(n, 1).Formula = upcard
                                        worksheet.Range("A7").Offset(n, 2).Formula = Total
                                        worksheet.Range("A7").Offset(n, 3).Formula = Soft
                                        worksheet.Range("A7").Offset(n, 4).Formula = PlayerHands(index).Hand.NumCards
                                        worksheet.Range("A7").Offset(n, 5).Formula = "PreSplit"
                                        worksheet.Range("A7").Offset(n, 6).Formula = "CD"
                                        worksheet.Range("A7").Offset(n, 7).Formula = "TD"

                                        worksheet.Range("A7").Offset(n, 8).Formula = C.StratShortText(Opt.HandEVs(index).EVs.Strat(upcard))
                                        worksheet.Range("A7").Offset(n, 9).Formula = C.StratShortText(TD.StratTD(Total, Soft).Strat(upcard))
                                        worksheet.Range("A7").Offset(n, 10).Formula = PlayerHands(index).HandEVs.StandEV(upcard)
                                        worksheet.Range("A7").Offset(n, 11).Formula = PlayerHands(index).HandEVs.StandEV(upcard)
                                        worksheet.Range("A7").Offset(n, 12).Formula = PlayerHands(index).HandEVs.DEV(upcard)
                                        worksheet.Range("A7").Offset(n, 13).Formula = PlayerHands(index).HandEVs.DEV(upcard)
                                        worksheet.Range("A7").Offset(n, 14).Formula = Opt.HandEVs(index).EVs.HitEV(upcard)
                                        worksheet.Range("A7").Offset(n, 15).Formula = TD.HandEVs(index).EVs.HitEV(upcard)
                                        worksheet.Range("A7").Offset(n, 16).Formula = Opt.HandEVs(index).HandUsed(upcard) <> 0
                                        worksheet.Range("A7").Offset(n, 17).Formula = TD.HandEVs(index).HandUsed(upcard) <> 0

                                        n += 1
                                    End If
                                End If
                                index = PlayerHands(index).NextHand
                            Loop
                        Next Soft
                    Next Total
                    UndealDCard(upcard)
                End If
            Next upcard

            'Now look for post-split exceptions
            If PrintPSExceptions And (CDP Or CDPN) Then
                For upcard = 1 To 10
                    If UCAllowed(upcard) Then
                        DealDCard(upcard)
                        For Total = 4 To 21
                            For Soft = 1 To 0 Step -1
                                For paircard = 1 To 10
                                    If SplitIndex(paircard, upcard) > 0 Then
                                        For hands = 1 To NSplitHands
                                            If NPxHands(hands).Used(SPL) And (NPxHands(hands).NN = 0 Or CDPN) Then
                                                index = PlayerHandTotal(Total, Soft)
                                                Do While index
                                                    If PlayerHands(index).PairIndex(paircard) > 0 Then
                                                        If Not Opt.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), hands) Is Nothing Then
                                                            If Opt.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), hands).Strat(upcard) <> 0 Then
                                                                tdstrat = 0
                                                                If PlayerHands(index).Hand.NumCards = 2 Then
                                                                    Select Case TD.StratTD(Total, Soft).Strat(upcard)
                                                                        Case BJCAGlobalsClass.Strat.PS
                                                                            tdstrat = BJCAGlobalsClass.Strat.S
                                                                        Case BJCAGlobalsClass.Strat.PD
                                                                            tdstrat = BJCAGlobalsClass.Strat.D
                                                                        Case BJCAGlobalsClass.Strat.PH
                                                                            tdstrat = BJCAGlobalsClass.Strat.H
                                                                        Case BJCAGlobalsClass.Strat.PR
                                                                            tdstrat = BJCAGlobalsClass.Strat.R
                                                                        Case Else
                                                                            tdstrat = TD.StratTD(Total, Soft).Strat(upcard)
                                                                    End Select
                                                                Else
                                                                    tdstrat = TD.StratTD(Total, Soft).Strat(upcard)
                                                                End If
                                                                If Not TD.HandEVs(index).DPostallowed(upcard) Then
                                                                    Select Case tdstrat
                                                                        Case BJCAGlobalsClass.Strat.D
                                                                            tdstrat = BJCAGlobalsClass.Strat.H
                                                                        Case BJCAGlobalsClass.Strat.DS
                                                                            tdstrat = BJCAGlobalsClass.Strat.S
                                                                    End Select
                                                                End If
                                                                If Not TD.HandEVs(index).RPostallowed(upcard) Then
                                                                    Select Case tdstrat
                                                                        Case BJCAGlobalsClass.Strat.R
                                                                            tdstrat = BJCAGlobalsClass.Strat.H
                                                                        Case BJCAGlobalsClass.Strat.RS
                                                                            tdstrat = BJCAGlobalsClass.Strat.S
                                                                    End Select
                                                                End If

                                                                Select Case Opt.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), hands).Strat(upcard)
                                                                    Case BJCAGlobalsClass.Strat.PS
                                                                        strat2 = BJCAGlobalsClass.Strat.S
                                                                    Case BJCAGlobalsClass.Strat.PD
                                                                        strat2 = BJCAGlobalsClass.Strat.D
                                                                    Case BJCAGlobalsClass.Strat.PH
                                                                        strat2 = BJCAGlobalsClass.Strat.H
                                                                    Case BJCAGlobalsClass.Strat.PR
                                                                        strat2 = BJCAGlobalsClass.Strat.R
                                                                    Case Else
                                                                        strat2 = Opt.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), hands).Strat(upcard)
                                                                End Select
                                                                If strat2 <> tdstrat Then
                                                                    handStr = ""
                                                                    For card = 1 To 10
                                                                        For i = 1 To PlayerHands(index).Hand.Cards(card)
                                                                            If card = 1 Then
                                                                                handStr = handStr & "A"
                                                                            ElseIf card = 10 Then
                                                                                handStr = handStr & "T"
                                                                            Else
                                                                                handStr = handStr & Format(card)
                                                                            End If
                                                                        Next i
                                                                    Next card

                                                                    worksheet.Range("A7").Offset(n + n2, 0).Formula = handStr
                                                                    worksheet.Range("A7").Offset(n + n2, 1).Formula = upcard
                                                                    worksheet.Range("A7").Offset(n + n2, 2).Formula = Total
                                                                    worksheet.Range("A7").Offset(n + n2, 3).Formula = Soft
                                                                    worksheet.Range("A7").Offset(n + n2, 4).Formula = PlayerHands(index).Hand.NumCards
                                                                    worksheet.Range("A7").Offset(n + n2, 5).Formula = Format(paircard) & "  " & NPxHands(hands).Name
                                                                    worksheet.Range("A7").Offset(n + n2, 6).Formula = "Post CD"
                                                                    worksheet.Range("A7").Offset(n + n2, 7).Formula = "TD"

                                                                    worksheet.Range("A7").Offset(n + n2, 8).Formula = C.StratShortText(Opt.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), hands).Strat(upcard))
                                                                    worksheet.Range("A7").Offset(n + n2, 9).Formula = C.StratShortText(TD.StratTD(Total, Soft).Strat(upcard))
                                                                    worksheet.Range("A7").Offset(n + n2, 10).Formula = PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), hands).StandEV(upcard)
                                                                    worksheet.Range("A7").Offset(n + n2, 11).Formula = PlayerHands(index).HandEVs.StandEV(upcard)
                                                                    worksheet.Range("A7").Offset(n + n2, 12).Formula = PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), hands).DEV(upcard)
                                                                    worksheet.Range("A7").Offset(n + n2, 13).Formula = PlayerHands(index).HandEVs.DEV(upcard)
                                                                    worksheet.Range("A7").Offset(n + n2, 14).Formula = Opt.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), hands).HitEV(upcard)
                                                                    worksheet.Range("A7").Offset(n + n2, 15).Formula = TD.HandEVs(index).EVs.HitEV(upcard)
                                                                    worksheet.Range("A7").Offset(n + n2, 16).Formula = Opt.HandEVs(index).HandUsed(upcard) <> 0
                                                                    worksheet.Range("A7").Offset(n + n2, 17).Formula = TD.HandEVs(index).HandUsed(upcard) <> 0

                                                                    n2 += 1
                                                                End If
                                                            End If
                                                        End If
                                                    End If
                                                    index = PlayerHands(index).NextHand
                                                Loop
                                            End If
                                        Next hands
                                    End If
                                Next paircard
                            Next Soft
                        Next Total
                        UndealDCard(upcard)
                    End If
                Next upcard

                For upcard = 1 To 10
                    If UCAllowed(upcard) Then
                        DealDCard(upcard)
                        For Total = 4 To 21
                            For Soft = 1 To 0 Step -1
                                For paircard = 1 To 10
                                    If SplitIndex(paircard, upcard) > 0 Then
                                        For hands = 1 To NSplitHands
                                            If NPxHands(hands).Used(SPL) And (NPxHands(hands).NN = 0 Or CDPN) Then
                                                index = PlayerHandTotal(Total, Soft)
                                                Do While index
                                                    If PlayerHands(index).PairIndex(paircard) > 0 Then
                                                        If Not Opt.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), hands) Is Nothing Then
                                                            If Opt.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), hands).Strat(upcard) <> 0 Then
                                                                If Opt.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), hands).Strat(upcard) <> Opt.HandEVs(index).EVs.Strat(upcard) Then
                                                                    handStr = ""
                                                                    For card = 1 To 10
                                                                        For i = 1 To PlayerHands(index).Hand.Cards(card)
                                                                            If card = 1 Then
                                                                                handStr = handStr & "A"
                                                                            ElseIf card = 10 Then
                                                                                handStr = handStr & "T"
                                                                            Else
                                                                                handStr = handStr & Format(card)
                                                                            End If
                                                                        Next i
                                                                    Next card

                                                                    worksheet.Range("A7").Offset(n + n2 + n3, 0).Formula = handStr
                                                                    worksheet.Range("A7").Offset(n + n2 + n3, 1).Formula = upcard
                                                                    worksheet.Range("A7").Offset(n + n2 + n3, 2).Formula = PlayerHands(index).Hand.Total
                                                                    worksheet.Range("A7").Offset(n + n2 + n3, 3).Formula = PlayerHands(index).Hand.Soft
                                                                    worksheet.Range("A7").Offset(n + n2 + n3, 4).Formula = PlayerHands(index).Hand.NumCards
                                                                    worksheet.Range("A7").Offset(n + n2 + n3, 5).Formula = Format(paircard) & "  " & NPxHands(hands).Name
                                                                    worksheet.Range("A7").Offset(n + n2 + n3, 6).Formula = "Post CD"
                                                                    worksheet.Range("A7").Offset(n + n2 + n3, 7).Formula = "CD"

                                                                    worksheet.Range("A7").Offset(n + n2 + n3, 8).Formula = C.StratShortText(Opt.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), hands).Strat(upcard))
                                                                    worksheet.Range("A7").Offset(n + n2 + n3, 9).Formula = C.StratShortText(Opt.HandEVs(index).EVs.Strat(upcard))
                                                                    worksheet.Range("A7").Offset(n + n2 + n3, 10).Formula = PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), hands).StandEV(upcard)
                                                                    worksheet.Range("A7").Offset(n + n2 + n3, 11).Formula = PlayerHands(index).HandEVs.StandEV(upcard)
                                                                    worksheet.Range("A7").Offset(n + n2 + n3, 12).Formula = PlayerHands(index).SplitEVs(PlayerHands(index).PairIndex(paircard), hands).DEV(upcard)
                                                                    worksheet.Range("A7").Offset(n + n2 + n3, 13).Formula = PlayerHands(index).HandEVs.DEV(upcard)
                                                                    worksheet.Range("A7").Offset(n + n2 + n3, 14).Formula = Opt.HandEVs(index).SplitData(PlayerHands(index).PairIndex(paircard), hands).HitEV(upcard)
                                                                    worksheet.Range("A7").Offset(n + n2 + n3, 15).Formula = Opt.HandEVs(index).EVs.HitEV(upcard)
                                                                    worksheet.Range("A7").Offset(n + n2 + n3, 16).Formula = Opt.HandEVs(index).HandUsed(upcard) <> 0
                                                                    worksheet.Range("A7").Offset(n + n2 + n3, 17).Formula = TD.HandEVs(index).HandUsed(upcard) <> 0

                                                                    n3 += 1
                                                                End If
                                                            End If
                                                        End If
                                                    End If
                                                    index = PlayerHands(index).NextHand
                                                Loop
                                            End If
                                        Next hands
                                    End If
                                Next paircard
                            Next Soft
                        Next Total
                        UndealDCard(upcard)
                    End If
                Next upcard
            End If

            worksheet.Range("B1").Formula = n
            worksheet.Range("B2").Formula = n2
            worksheet.Range("B3").Formula = n3
            If (n = 0 And n2 = 0 And n3 = 0) Then worksheet.Range("A7").Formula = "No Exceptions Found"

            '            worksheet.Range("A6:R6").End(Excel.XlDirection.xlDown). _
            '               Sort(Key1:=worksheet.Range("R6"), Order1:=Excel.XlSortOrder.xlDescending, _
            '              Key2:=worksheet.Range("H6"), Type:=Excel.XlSortType.xlSortValues, _
            '             Order2:=Excel.XlSortOrder.xlDescending, _
            '            Header:=Excel.XlYesNoGuess.xlGuess, OrderCustom:=1, MatchCase:=False, _
            '           Orientation:=Excel.XlSortOrientation.xlSortRows)

            worksheet.Range("A1").Select()

        End If

        worksheet = Nothing
    End Sub

    Private Sub PrintRulesExcel(ByVal workBook As Excel.Workbook)
        Dim worksheet As Excel.Worksheet
        Dim card As Integer
        Dim row As Integer

        worksheet = workBook.Worksheets("Rules")
        worksheet.Select()

        worksheet.Range("B1:B29").ClearContents()
        worksheet.Range("E2:N6").ClearContents()
        worksheet.Range("E11:N11").ClearContents()
        worksheet.Range("E16:E21").ClearContents()
        worksheet.Range("B1").Select()

        worksheet.Range("B1").Formula = DeckType

        For card = 1 To 10
            worksheet.Range("E2").Offset(0, card - 1).Formula = OriginalShoe.Cards(card)
            worksheet.Range("E2").Offset(1, card - 1).Formula = OriginalShoe.Suits(card, 0)
            worksheet.Range("E2").Offset(2, card - 1).Formula = OriginalShoe.Suits(card, 1)
            worksheet.Range("E2").Offset(3, card - 1).Formula = OriginalShoe.Suits(card, 2)
            worksheet.Range("E2").Offset(4, card - 1).Formula = OriginalShoe.Suits(card, 3)
        Next card

        If StandOnSoft = 17 Then
            worksheet.Range("B2").Formula = "S17"
        Else
            worksheet.Range("B2").Formula = "H17"
        End If

        If ENHC Then
            worksheet.Range("B3").Formula = "ENHC"
        ElseIf BBO Then
            worksheet.Range("B3").Formula = "BBO"
        ElseIf OBBO Then
            worksheet.Range("B3").Formula = "OBBO"
        ElseIf AOBBO Then
            worksheet.Range("B3").Formula = "AOBBO"
        Else
            worksheet.Range("B3").Formula = "OBO"
        End If

        worksheet.Range("B4").Formula = DoubleType
        worksheet.Range("B14").Formula = DAN

        worksheet.Range("B5").Formula = DAS

        worksheet.Range("B6").Formula = SurrType
        worksheet.Range("B19").Formula = SAN

        worksheet.Range("B7").Formula = "SPL" + CStr(SPL)
        worksheet.Range("B8").Formula = BJPays

        worksheet.Range("B10").Formula = SMA
        worksheet.Range("B11").Formula = HSA
        worksheet.Range("B12").Formula = DSA
        worksheet.Range("B13").Formula = SSA

        If DSoftAllHard Then
            worksheet.Range("B16").Formula = "All"
        ElseIf DSoft19Hard Then
            worksheet.Range("B16").Formula = "19 Only"
        Else
            worksheet.Range("B16").Formula = "False"
        End If

        worksheet.Range("B18").Formula = SurrPays
        worksheet.Range("B20").Formula = SurrDBJPays
        worksheet.Range("B21").Formula = MacauType
        worksheet.Range("B22").Formula = SAS

        If CDP Then
            worksheet.Range("B24").Formula = "CDP"
        ElseIf CDPN Then
            worksheet.Range("B24").Formula = "CDPN"
        Else
            worksheet.Range("B24").Formula = "CDZ-"
        End If

        worksheet.Range("B25").Formula = BJSplitAces
        worksheet.Range("B26").Formula = BJSplitTens

        worksheet.Range("B28").Formula = CheckTen
        worksheet.Range("B29").Formula = CheckAce

        For card = 1 To 10
            worksheet.Range("E11").Offset(0, card - 1).Formula = SplitAllowed(card)
        Next card

        For card = 17 To 22
            worksheet.Range("E16").Offset(card - 17, 0).Formula = PDTies(card)
        Next card

        worksheet = Nothing

        worksheet = workBook.Worksheets("Special Rules")
        worksheet.Select()

        worksheet.Range("A2").End(Excel.XlDirection.xlDown).ClearContents()
        worksheet.Range("C2").End(Excel.XlDirection.xlDown).ClearContents()
        worksheet.Range("A1").Select()

        row = 0
        For card = 0 To BonusRulesList.NumRules - 1
            If BonusRulesList.L(card).RuleOn Then
                worksheet.Range("A2").Offset(row, 0).Formula = BonusRulesList.L(card).Name
                row += 1
            End If
        Next card
        If row = 0 Then
            worksheet.Range("A2").Formula = "None"
        End If

        row = 0
        For card = 0 To ForcedTableRulesList.NumRules - 1
            If ForcedTableRulesList.L(card).RuleOn Then
                worksheet.Range("C2").Offset(row, 0).Formula = ForcedTableRulesList.L(card).Name
                row += 1
            End If
        Next card
        For card = 0 To ForcedRulesList.NumRules - 1
            If ForcedRulesList.L(card).RuleOn Then
                worksheet.Range("C2").Offset(row, 0).Formula = ForcedRulesList.L(card).Name
                row += 1
            End If
        Next card
        If row = 0 Then
            worksheet.Range("C2").Formula = "None"
        End If

        worksheet = Nothing
    End Sub

#End Region

#Region " EOR Methods "

    Public Sub CopyEOREVs(ByRef newEVs As BJCAEORsClass, ByRef oldCA As BJCA)
        Dim upcard As Integer
        Dim total As Integer
        Dim soft As Integer
        Dim oldIndex As Integer
        Dim index As Integer

        newEVs.ForcedNetGameEV = Forced.GameEVs.NetGameEV
        newEVs.CDNetGameEV = Opt.GameEVs.NetGameEV
        newEVs.TDNetGameEV = TD.GameEVs.NetGameEV

        For total = 4 To 21
            For soft = 0 To 1
                If total > 12 Or soft = False + 1 Then
                    If Not Forced.StratTD(total, soft) Is Nothing Then
                        If newEVs.ForcedStratTD(total, soft) Is Nothing Then newEVs.ForcedStratTD(total, soft) = New BJCATDStratClass
                        For upcard = 1 To 10
                            If UCAllowed(upcard) Then
                                newEVs.ForcedStratTD(total, soft).Strat(upcard) = Forced.StratTD(total, soft).Strat(upcard)
                                newEVs.ForcedStratTD(total, soft).StratEV(upcard) = Forced.StratTD(total, soft).StratEV(upcard)
                                newEVs.ForcedStratTD(total, soft).StratStandEV(upcard) = Forced.StratTD(total, soft).StratStandEV(upcard)
                                newEVs.ForcedStratTD(total, soft).StratDEV(upcard) = Forced.StratTD(total, soft).StratDEV(upcard)
                                newEVs.ForcedStratTD(total, soft).StratSurrEV(upcard) = Forced.StratTD(total, soft).StratSurrEV(upcard)
                                newEVs.ForcedStratTD(total, soft).StratHitEV(upcard) = Forced.StratTD(total, soft).StratHitEV(upcard)
                            End If
                        Next upcard
                    End If

                    If Not TD.StratTD(total, soft) Is Nothing Then
                        If newEVs.TDStratTD(total, soft) Is Nothing Then newEVs.TDStratTD(total, soft) = New BJCATDStratClass
                        For upcard = 1 To 10
                            If UCAllowed(upcard) Then
                                newEVs.TDStratTD(total, soft).Strat(upcard) = TD.StratTD(total, soft).Strat(upcard)
                                newEVs.TDStratTD(total, soft).StratEV(upcard) = TD.StratTD(total, soft).StratEV(upcard)
                                newEVs.TDStratTD(total, soft).StratStandEV(upcard) = TD.StratTD(total, soft).StratStandEV(upcard)
                                newEVs.TDStratTD(total, soft).StratDEV(upcard) = TD.StratTD(total, soft).StratDEV(upcard)
                                newEVs.TDStratTD(total, soft).StratSurrEV(upcard) = TD.StratTD(total, soft).StratSurrEV(upcard)
                                newEVs.TDStratTD(total, soft).StratHitEV(upcard) = TD.StratTD(total, soft).StratHitEV(upcard)
                            End If
                        Next upcard
                    End If
                End If
            Next soft
        Next total

        For index = 1 To NumPHands
            If PlayerHands(index).Hand.NumCards > 1 Then
                oldIndex = oldCA.FindPlayerHand(PlayerHands(index).Hand)
                If newEVs.EVs(oldIndex) Is Nothing Then newEVs.EVs(oldIndex) = New BJCAEOREVsClass
                For upcard = 1 To 10
                    If UCAllowed(upcard) Then
                        newEVs.EVs(oldIndex).StandEV(upcard) = PlayerHands(index).HandEVs.StandEV(upcard)
                        newEVs.EVs(oldIndex).StandPushEV(upcard) = PlayerHands(index).HandEVs.StandPushEV(upcard)
                        newEVs.EVs(oldIndex).DEV(upcard) = PlayerHands(index).HandEVs.DEV(upcard)
                        newEVs.EVs(oldIndex).DPushEV(upcard) = PlayerHands(index).HandEVs.DPushEV(upcard)
                        newEVs.EVs(oldIndex).SurrEV(upcard) = PlayerHands(index).HandEVs.SurrEV(upcard)

                        newEVs.EVs(oldIndex).ForcedStrat(upcard) = Forced.HandEVs(index).EVs.Strat(upcard)
                        newEVs.EVs(oldIndex).ForcedHitEV(upcard) = Forced.HandEVs(index).EVs.HitEV(upcard)
                        newEVs.EVs(oldIndex).ForcedHitPushEV(upcard) = Forced.HandEVs(index).EVs.HitPushEV(upcard)
                        newEVs.EVs(oldIndex).ForcedSplitEV(upcard) = Forced.HandEVs(index).SplitEV(upcard)

                        newEVs.EVs(oldIndex).CDStrat(upcard) = Opt.HandEVs(index).EVs.Strat(upcard)
                        newEVs.EVs(oldIndex).CDHitEV(upcard) = Opt.HandEVs(index).EVs.HitEV(upcard)
                        newEVs.EVs(oldIndex).CDHitPushEV(upcard) = Opt.HandEVs(index).EVs.HitPushEV(upcard)
                        newEVs.EVs(oldIndex).CDSplitEV(upcard) = Opt.HandEVs(index).SplitEV(upcard)

                        newEVs.EVs(oldIndex).TDStrat(upcard) = TD.HandEVs(index).EVs.Strat(upcard)
                        newEVs.EVs(oldIndex).TDHitEV(upcard) = TD.HandEVs(index).EVs.HitEV(upcard)
                        newEVs.EVs(oldIndex).TDHitPushEV(upcard) = TD.HandEVs(index).EVs.HitPushEV(upcard)
                        newEVs.EVs(oldIndex).TDSplitEV(upcard) = TD.HandEVs(index).SplitEV(upcard)
                    End If
                Next upcard
            End If
        Next index

    End Sub

    Public Sub CopyEORForcedStrat(ByRef oldCA As BJCA)
        Dim upcard As Integer
        Dim total As Integer
        Dim soft As Integer
        Dim oldIndex As Integer
        Dim index As Integer

        ResetForcedRulesHands(Forced)
        For total = 4 To 21
            For soft = 0 To 1
                Forced.StratTD(total, soft) = Nothing
                If total > 12 Or soft = False + 1 Then
                    If Not oldCA.Forced.StratTD(total, soft) Is Nothing Then
                        Forced.StratTD(total, soft) = New BJCATDStratClass
                        For upcard = 1 To 10
                            If UCAllowed(upcard) Then
                                Forced.StratTD(total, soft).Strat(upcard) = oldCA.Forced.StratTD(total, soft).Strat(upcard)
                            End If
                        Next upcard
                    End If
                End If
            Next soft
        Next total

        For index = 1 To NumPHands
            If PlayerHands(index).Hand.NumCards > 1 Then
                oldIndex = oldCA.FindPlayerHand(PlayerHands(index).Hand)
                For upcard = 1 To 10
                    If UCAllowed(upcard) Then
                        Forced.HandEVs(index).PreForced(upcard) = True
                        Forced.HandEVs(index).PostForced(upcard) = True
                        Forced.HandEVs(index).EVs.Strat(upcard) = oldCA.Forced.HandEVs(oldIndex).EVs.Strat(upcard)
                        Forced.HandEVs(index).EVs.ForcedPostStrat(upcard) = oldCA.Forced.HandEVs(oldIndex).EVs.ForcedPostStrat(upcard)
                    End If
                Next upcard
            End If
        Next index

    End Sub

    Public Sub FixForcedStrat(ByRef cstrat As BJCAStrategyClass)
        Dim index As Integer
        Dim upcard As Integer

        For index = 0 To NumPHands
            If PlayerHands(index).Hand.NumCards > 1 Then
                For upcard = 1 To 10
                    If UCAllowed(upcard) Then
                        cstrat.HandEVs(index).PreForced(upcard) = True
                        cstrat.HandEVs(index).PostForced(upcard) = True
                    End If
                Next upcard
            End If
        Next index
    End Sub

    Public Sub ComputeEORForcedStrat(ByRef cstrat As BJCAStrategyClass)
        Dim total As Integer
        Dim soft As Integer
        Dim prob As Double
        Dim card As Integer
        Dim index As Integer
        Dim upcard As Integer

        For upcard = 1 To 10
            If UCAllowed(upcard) And CardProb(upcard, 0) > 0 Then
                If SPL > 0 Then
                    For card = 1 To 10
                        If SplitIndex(card, upcard) > 0 Then
                            index = SplitIndex(card, upcard)
                            Call ComputeSplit(cstrat, card, upcard)
                        End If
                    Next card
                End If

                DealDCard(upcard)

                GetHandsUsed(cstrat, upcard, 1, True)

                'Calculate the final overall hitting values using the current strategy
                ComputeStratHit(cstrat, upcard)

                For total = 4 To 21
                    For soft = 0 To 1
                        If Not cstrat.StratTD(total, soft) Is Nothing Then
                            'First empty the strategy
                            cstrat.StratTD(total, soft).StratEV(upcard) = 0
                            cstrat.StratTD(total, soft).StratStandEV(upcard) = 0
                            cstrat.StratTD(total, soft).StratHitEV(upcard) = 0
                            cstrat.StratTD(total, soft).StratDEV(upcard) = 0
                            cstrat.StratTD(total, soft).StratSurrEV(upcard) = 0

                            cstrat.StratTD(total, soft).NetProb(upcard) = 0
                            cstrat.StratTD(total, soft).NetSProb(upcard) = 0
                            cstrat.StratTD(total, soft).NetHProb(upcard) = 0
                            cstrat.StratTD(total, soft).NetDProb(upcard) = 0
                            cstrat.StratTD(total, soft).NetSurrProb(upcard) = 0

                            cstrat.StratTD(total, soft).SStandEV(upcard) = 0
                            cstrat.StratTD(total, soft).SHitEV(upcard) = 0
                            cstrat.StratTD(total, soft).SDEV(upcard) = 0
                            cstrat.StratTD(total, soft).SSurrEV(upcard) = 0

                            'Begin by removing hands that are CD dependent including split hands
                            prob = 0
                            index = PlayerHandTotal(total, soft)
                            Do While index
                                If PlayerHands(index).Hand.NumCards < 2 Or (cstrat.NCardsCD <> 0 And PlayerHands(index).Hand.NumCards <= cstrat.NCardsCD) Then
                                    cstrat.HandEVs(index).HandUsed(upcard) = -1
                                End If
                                'Don't include blackjack in the strategy determination
                                If PlayerHands(index).Hand.IsBJ Then
                                    cstrat.HandEVs(index).HandUsed(upcard) = -1
                                End If
                                Select Case cstrat.HandEVs(index).EVs.Strat(upcard)
                                    Case BJCAGlobalsClass.Strat.P, BJCAGlobalsClass.Strat.PS, BJCAGlobalsClass.Strat.PD, BJCAGlobalsClass.Strat.PH, BJCAGlobalsClass.Strat.PR
                                        cstrat.HandEVs(index).HandUsed(upcard) = -1
                                End Select
                                If cstrat.HandEVs(index).HandUsed(upcard) = 1 Then
                                    prob += 1
                                End If
                                index = PlayerHands(index).NextHand
                            Loop

                            'Now fill in all the evs
                            index = PlayerHandTotal(total, soft)
                            Do While index
                                If PlayerHands(index).Hand.NumCards > 1 And PlayerHands(index).Hand.NumCards > cstrat.NCardsCD And cstrat.HandEVs(index).HandUsed(upcard) > 0 Then
                                    prob = PlayerHands(index).HandEVs.Prob(upcard) * cstrat.HandEVs(index).Multiplier(upcard)
                                    cstrat.StratTD(PlayerHands(index).Hand.Total, PlayerHands(index).Hand.Soft + 1).NetProb(upcard) += prob
                                    If cstrat.HandEVs(index).SPreallowed(upcard) Then
                                        cstrat.StratTD(PlayerHands(index).Hand.Total, PlayerHands(index).Hand.Soft + 1).NetSProb(upcard) += prob
                                        cstrat.StratTD(PlayerHands(index).Hand.Total, PlayerHands(index).Hand.Soft + 1).StratStandEV(upcard) += prob * PlayerHands(index).HandEVs.StandEV(upcard)
                                    End If
                                    If cstrat.HandEVs(index).HPreallowed(upcard) Then
                                        cstrat.StratTD(PlayerHands(index).Hand.Total, PlayerHands(index).Hand.Soft + 1).NetHProb(upcard) += prob
                                        cstrat.StratTD(PlayerHands(index).Hand.Total, PlayerHands(index).Hand.Soft + 1).StratHitEV(upcard) += prob * cstrat.HandEVs(index).EVs.HitEV(upcard)
                                    End If
                                    If cstrat.HandEVs(index).DPreallowed(upcard) Then
                                        cstrat.StratTD(PlayerHands(index).Hand.Total, PlayerHands(index).Hand.Soft + 1).NetDProb(upcard) += prob
                                        cstrat.StratTD(PlayerHands(index).Hand.Total, PlayerHands(index).Hand.Soft + 1).StratDEV(upcard) += prob * PlayerHands(index).HandEVs.DEV(upcard)
                                    End If
                                    If cstrat.HandEVs(index).RPreallowed(upcard) > 0 Then
                                        cstrat.StratTD(PlayerHands(index).Hand.Total, PlayerHands(index).Hand.Soft + 1).NetSurrProb(upcard) += prob
                                        cstrat.StratTD(PlayerHands(index).Hand.Total, PlayerHands(index).Hand.Soft + 1).StratSurrEV(upcard) += prob * PlayerHands(index).HandEVs.SurrEV(upcard)
                                    End If
                                End If
                                index = PlayerHands(index).NextHand
                            Loop

                            'Now we need to normalize the ev's
                            If cstrat.StratTD(total, soft).NetSProb(upcard) <> 0 Then
                                cstrat.StratTD(total, soft).StratStandEV(upcard) = cstrat.StratTD(total, soft).StratStandEV(upcard) / cstrat.StratTD(total, soft).NetSProb(upcard)
                            End If
                            If cstrat.StratTD(total, soft).NetHProb(upcard) <> 0 Then
                                cstrat.StratTD(total, soft).StratHitEV(upcard) = cstrat.StratTD(total, soft).StratHitEV(upcard) / cstrat.StratTD(total, soft).NetHProb(upcard)
                            End If
                            If cstrat.StratTD(total, soft).NetDProb(upcard) <> 0 Then
                                cstrat.StratTD(total, soft).StratDEV(upcard) = cstrat.StratTD(total, soft).StratDEV(upcard) / cstrat.StratTD(total, soft).NetDProb(upcard)
                            End If
                            If cstrat.StratTD(total, soft).NetSurrProb(upcard) <> 0 Then
                                cstrat.StratTD(total, soft).StratSurrEV(upcard) = cstrat.StratTD(total, soft).StratSurrEV(upcard) / cstrat.StratTD(total, soft).NetSurrProb(upcard)
                            End If

                        End If
                    Next soft
                Next total
                UndealDCard(upcard)
            End If
        Next upcard


        ComputeGameEVsStrat(cstrat)
    End Sub

#End Region

#End Region

#Region " Real Time Methods "

    Public Function BJCART(ByVal hand As BJCAHandClass, ByVal upcard As Integer, ByVal postsplit As Boolean, ByVal paircard As Integer, ByVal newRules As BJCARulesClass, Optional ByRef dpDict As BJCADealerProbsDictionary = Nothing) As BJCARealtimeHandClass
        Dim card As Integer
        Dim index As Integer
        Dim suit As Integer
        Dim tempEVs As New BJCAStratHandEVsClass
        Dim newEVs As New BJCARealtimeHandClass

        GetRules(newRules)
        Initialize(newRules)

        If Not dpDict Is Nothing Then DealerProbs = dpDict
        ApplyRulesStrat(newRules, postsplit)

        ComputeHandProbsPreSplit()
        ComputeStand()
        ComputeBlackjack()
        ComputeSurrender()
        ComputeDouble()
        ApplyBonusRulesPreSuited()
        ComputeOptInitialStrat()
        ComputeOptHit()

        If SPL > 0 Then
            InitializeSplits()
            ComputeOptSplit()
        End If

        EnumSuitedBonusHandList()
        EnumSuitedHandList()
        ComputeSuitedHandNetEVs(Opt)

        index = FindPlayerHand(hand)

        newEVs.Hand.StandEV(upcard) = PlayerHands(index).HandEVs.StandEV(upcard)
        newEVs.Hand.DEV(upcard) = PlayerHands(index).HandEVs.DEV(upcard)
        newEVs.Hand.SurrEV(upcard) = PlayerHands(index).HandEVs.SurrEV(upcard)
        newEVs.Hand.StratEV(upcard) = Opt.HandEVs(index).EVs.StratEV(upcard)
        newEVs.Hand.HitEV(upcard) = Opt.HandEVs(index).EVs.HitEV(upcard)
        newEVs.Hand.SplitEV(upcard) = Opt.HandEVs(index).SplitEV(upcard)

        'DealerProbs will hold the suited probs
        For suit = 0 To 3
            newEVs.Hand.DealerProbs(upcard, suit) = PlayerHands(index).HandEVs.ProbSuited(upcard, suit)
        Next suit

        If Not PlayerHands(index).SuitedBonusEVs Is Nothing Then
            newEVs.SuitedEVs = PlayerHands(index).SuitedBonusEVs
        End If

        If RDA Or DDR Then
            newEVs.Hand.DAllowed(upcard) = DoubleNeeded(index, upcard)
            newEVs.Hand.RAllowed(upcard) = SurrenderNeeded(index, upcard)
        Else
            newEVs.Hand.DAllowed(upcard) = Opt.HandEVs(index).DPreallowed(upcard)
            newEVs.Hand.RAllowed(upcard) = Opt.HandEVs(index).RPreallowed(upcard)
        End If
        newEVs.Hand.PAllowed(upcard) = Opt.HandEVs(index).PAllowed(upcard)

        Select Case Opt.HandEVs(index).EVs.Strat(upcard)
            Case C.Strat.PS, C.Strat.PR, C.Strat.PH, C.Strat.PD
                newEVs.Hand.Strat(upcard) = C.Strat.P
            Case C.Strat.DS
                newEVs.Hand.Strat(upcard) = C.Strat.D
            Case C.Strat.RS
                newEVs.Hand.Strat(upcard) = C.Strat.R
            Case Else
                newEVs.Hand.Strat(upcard) = Opt.HandEVs(index).EVs.Strat(upcard)
        End Select

        If paircard = 1 And postsplit And (Not newEVs.Hand.StratEV(upcard) = C.Strat.P Or Not SMA) Then
            tempEVs = ComputePostSplitAcesStratRT(index, upcard)
            newEVs.Hand.DEV(upcard) = tempEVs.DEV(upcard)
            newEVs.Hand.SurrEV(upcard) = tempEVs.SurrEV(upcard)
            newEVs.Hand.Strat(upcard) = tempEVs.Strat(upcard)
            newEVs.Hand.StratEV(upcard) = tempEVs.StratEV(upcard)
            newEVs.Hand.HitEV(upcard) = tempEVs.HitEV(upcard)
            newEVs.Hand.DAllowed(upcard) = tempEVs.DAllowed(upcard)
            newEVs.Hand.RAllowed(upcard) = tempEVs.RAllowed(upcard)
            newEVs.Hand.SplitEV(upcard) = tempEVs.SplitEV(upcard)
            newEVs.Hand.PAllowed(upcard) = False
        End If

        Return newEVs

    End Function

    Private Function ComputePostSplitAcesStratRT(ByVal index As Integer, ByVal upcard As Integer) As BJCAStratHandEVsClass
        Dim newHandEVs As New BJCAStratHandEVsClass

        'Now get the strategy and strategy EV
        newHandEVs.Strat(upcard) = C.Strat.S
        newHandEVs.StratEV(upcard) = PlayerHands(index).HandEVs.StandEV(upcard)

        If HSA Then
            newHandEVs.HitEV(upcard) = Opt.HandEVs(index).EVs.HitEV(upcard)
            If Opt.HandEVs(index).EVs.HitEV(upcard) > newHandEVs.StratEV(upcard) Then
                newHandEVs.Strat(upcard) = C.Strat.H
                newHandEVs.StratEV(upcard) = Opt.HandEVs(index).EVs.HitEV(upcard)
            End If
        End If
        If DSA Then
            newHandEVs.DEV(upcard) = PlayerHands(index).HandEVs.DEV(upcard)
            If PlayerHands(index).HandEVs.DEV(upcard) > newHandEVs.StratEV(upcard) Then
                newHandEVs.Strat(upcard) = C.Strat.D
                newHandEVs.StratEV(upcard) = PlayerHands(index).HandEVs.DEV(upcard)
            End If
        Else
            newHandEVs.DAllowed(upcard) = False
        End If
        If SSA Then
            newHandEVs.SurrEV(upcard) = PlayerHands(index).HandEVs.SurrEV(upcard)
            If PlayerHands(index).HandEVs.SurrEV(upcard) > newHandEVs.StratEV(upcard) Then
                newHandEVs.Strat(upcard) = C.Strat.R
                newHandEVs.StratEV(upcard) = PlayerHands(index).HandEVs.SurrEV(upcard)
            End If
        Else
            newHandEVs.RAllowed(upcard) = 0
        End If

        Return newHandEVs
    End Function

#End Region

#Region " N Card Strategy Methods "

    Public Function ComputeNCardStratEVs(ByRef cStrat As BJCAStrategyClass, ByVal total As Integer, ByVal soft As Integer, ByVal upcard As Integer, ByVal nCards As Integer, ByVal orMore As Boolean, ByVal orLess As Boolean, ByVal handsUsed As Boolean) As BJCATDStratClass
        'Soft is passed as the Boolean+1 value
        Dim index As Integer
        Dim prob As Double
        Dim maxev As Double
        Dim handIncluded As Boolean

        Dim NetProb As Double
        Dim NetSProb As Double
        Dim NetHProb As Double
        Dim NetDProb As Double
        Dim NetSurrProb As Double

        Dim SStandEV As Double
        Dim SHitEV As Double
        Dim SDEV As Double
        Dim SSurrEV As Double
        Dim SSplitEV As Double
        Dim Strat As Integer

        Dim newEvs As New BJCATDStratClass

        'The following procedure is based on the fact that the only values that change with
        '  strategy changes are hit/split
        '  The only thing these values can do is decrease compared to optimal play
        '  Only a single pass is performed

        'First empty the strategy
        NetProb = 0
        NetSProb = 0
        NetHProb = 0
        NetDProb = 0
        NetSurrProb = 0

        SStandEV = 0
        SHitEV = 0
        SDEV = 0
        SSurrEV = 0
        SSplitEV = 0

        'Now begin by seeing if any hands are included in the requirements and get the 
        '    split ev if there is one
        prob = 0
        index = PlayerHandTotal(total, soft)
        Do While index
            If cStrat.HandEVs(index).SplitEV(upcard) <> 0 Then
                SSplitEV = cStrat.HandEVs(index).SplitEV(upcard)
            End If
            handIncluded = ((Not handsUsed Or cStrat.HandEVs(index).HandUsed(upcard) = 1) And (PlayerHands(index).Hand.NumCards = nCards))
            If handIncluded Then
                prob += 1
                Exit Do
            End If
            index = PlayerHands(index).NextHand
        Loop

        If prob > 0 Then
            'Fill in the strategy using all used hands to start
            'First look at hands where both double and surrender are allowed
            index = PlayerHandTotal(total, soft)
            Do While index
                handIncluded = ((Not handsUsed Or cStrat.HandEVs(index).HandUsed(upcard) = 1) And (PlayerHands(index).Hand.NumCards = nCards Or (orMore And PlayerHands(index).Hand.NumCards >= nCards) Or (orLess And PlayerHands(index).Hand.NumCards <= nCards And PlayerHands(index).Hand.NumCards > 1)))
                If handIncluded And cStrat.HandEVs(index).RPreallowed(upcard) And cStrat.HandEVs(index).DPreallowed(upcard) Then
                    If handsUsed Then
                        prob = PlayerHands(index).HandEVs.Prob(upcard) * cStrat.HandEVs(index).Multiplier(upcard)
                    Else
                        prob = PlayerHands(index).HandEVs.Prob(upcard)
                    End If
                    NetProb += prob
                    SStandEV += prob * PlayerHands(index).HandEVs.StandEV(upcard)
                    SDEV += prob * PlayerHands(index).HandEVs.DEV(upcard)
                    SHitEV += prob * Opt.HandEVs(index).EVs.HitEV(upcard)
                    SSurrEV += prob * PlayerHands(index).HandEVs.SurrEV(upcard)
                End If
                index = PlayerHands(index).NextHand
            Loop

            'If the probability of a strategy option is 0 then prevent it from being the strategy
            If NetProb <> 0 Then
                If SStandEV = 0 Then SStandEV = -2
                If SDEV = 0 Then SDEV = -2
                If SHitEV = 0 Then SHitEV = -2
                If SSurrEV = 0 Then SSurrEV = -2
            End If

            'Compare double/surrender to hit/stand
            If NetProb <> 0 Then
                Strat = BJCAGlobalsClass.Strat.S
                maxev = SStandEV
                If SHitEV > maxev Then
                    Strat = BJCAGlobalsClass.Strat.H
                    maxev = SHitEV
                End If
                If SDEV > maxev Then
                    Strat = BJCAGlobalsClass.Strat.D
                    maxev = SDEV
                End If
                If SSurrEV >= maxev Then
                    Strat = BJCAGlobalsClass.Strat.R
                End If
            End If

            'Now check values where only double is allowed and the strategy hasn't been set to surrender/split
            NetProb = 0

            SStandEV = 0
            SHitEV = 0
            SDEV = 0
            SSurrEV = 0

            If Strat <> BJCAGlobalsClass.Strat.R And Strat <> BJCAGlobalsClass.Strat.RS Then
                index = PlayerHandTotal(total, soft)
                Do While index
                    handIncluded = ((Not handsUsed Or cStrat.HandEVs(index).HandUsed(upcard) = 1) And (PlayerHands(index).Hand.NumCards = nCards Or (orMore And PlayerHands(index).Hand.NumCards >= nCards) Or (orLess And PlayerHands(index).Hand.NumCards <= nCards And PlayerHands(index).Hand.NumCards > 1)))
                    If handIncluded And cStrat.HandEVs(index).DPreallowed(upcard) Then
                        If handsUsed Then
                            prob = PlayerHands(index).HandEVs.Prob(upcard) * cStrat.HandEVs(index).Multiplier(upcard)
                        Else
                            prob = PlayerHands(index).HandEVs.Prob(upcard)
                        End If
                        NetProb += prob
                        SStandEV += prob * PlayerHands(index).HandEVs.StandEV(upcard)
                        SDEV += prob * PlayerHands(index).HandEVs.DEV(upcard)
                        SHitEV += prob * cStrat.HandEVs(index).EVs.HitEV(upcard)
                    End If
                    index = PlayerHands(index).NextHand
                Loop
            End If

            'If the probability of a strategy option is 0 then prevent it from being the strategy
            If NetProb <> 0 Then
                If SStandEV = 0 Then SStandEV = -2
                If SDEV = 0 Then SDEV = -2
                If SHitEV = 0 Then SHitEV = -2
                If SSurrEV = 0 Then SSurrEV = -2
            End If

            'Compare double to hit/stand
            If (NetProb <> 0) Then
                Strat = BJCAGlobalsClass.Strat.S
                maxev = SStandEV
                If SHitEV > maxev Then
                    Strat = BJCAGlobalsClass.Strat.H
                    maxev = SHitEV
                End If
                If SDEV > maxev Then
                    Strat = BJCAGlobalsClass.Strat.D
                End If
            End If

            'Now check values where only surrender is allowed and the strategy hasn't been set to double/split
            NetProb = 0

            SStandEV = 0
            SHitEV = 0
            SDEV = 0
            SSurrEV = 0

            If Strat <> BJCAGlobalsClass.Strat.D And Strat <> BJCAGlobalsClass.Strat.DS Then
                index = PlayerHandTotal(total, soft)
                Do While index
                    handIncluded = ((Not handsUsed Or cStrat.HandEVs(index).HandUsed(upcard) = 1) And (PlayerHands(index).Hand.NumCards = nCards Or (orMore And PlayerHands(index).Hand.NumCards >= nCards) Or (orLess And PlayerHands(index).Hand.NumCards <= nCards And PlayerHands(index).Hand.NumCards > 1)))
                    If handIncluded And cStrat.HandEVs(index).RPreallowed(upcard) > 0 Then
                        If handsUsed Then
                            prob = PlayerHands(index).HandEVs.Prob(upcard) * cStrat.HandEVs(index).Multiplier(upcard)
                        Else
                            prob = PlayerHands(index).HandEVs.Prob(upcard)
                        End If
                        NetProb += prob
                        SStandEV += prob * PlayerHands(index).HandEVs.StandEV(upcard)
                        SHitEV += prob * cStrat.HandEVs(index).EVs.HitEV(upcard)
                        SSurrEV += prob * PlayerHands(index).HandEVs.SurrEV(upcard)
                    End If
                    index = PlayerHands(index).NextHand
                Loop
            End If

            'If the probability of a strategy option is 0 then prevent it from being the strategy
            If NetProb <> 0 Then
                If SStandEV = 0 Then SStandEV = -2
                If SHitEV = 0 Then SHitEV = -2
                If SSurrEV = 0 Then SSurrEV = -2
            End If

            'Compare surrender to hit/stand
            If (NetProb <> 0) Then
                Strat = BJCAGlobalsClass.Strat.S
                maxev = SStandEV
                If SHitEV > maxev Then
                    Strat = BJCAGlobalsClass.Strat.H
                    maxev = SHitEV
                End If
                If SSurrEV > maxev Then
                    Strat = BJCAGlobalsClass.Strat.R
                End If
            End If

            'Now check values where neither surrender nor double is the strategy and the strategy hasn't been set to double/split
            NetProb = 0

            SStandEV = 0
            SHitEV = 0

            If Strat <> BJCAGlobalsClass.Strat.D And Strat <> BJCAGlobalsClass.Strat.R Then
                index = PlayerHandTotal(total, soft)
                Do While index
                    handIncluded = ((Not handsUsed Or cStrat.HandEVs(index).HandUsed(upcard) = 1) And (PlayerHands(index).Hand.NumCards = nCards Or (orMore And PlayerHands(index).Hand.NumCards >= nCards) Or (orLess And PlayerHands(index).Hand.NumCards <= nCards And PlayerHands(index).Hand.NumCards > 1)))
                    If handIncluded Then
                        If handsUsed Then
                            prob = PlayerHands(index).HandEVs.Prob(upcard) * cStrat.HandEVs(index).Multiplier(upcard)
                        Else
                            prob = PlayerHands(index).HandEVs.Prob(upcard)
                        End If
                        NetProb += prob
                        SStandEV += prob * PlayerHands(index).HandEVs.StandEV(upcard)
                        SHitEV += prob * cStrat.HandEVs(index).EVs.HitEV(upcard)
                    End If
                    index = PlayerHands(index).NextHand
                Loop
            End If

            'If the probability of a strategy option is 0 then prevent it from being the strategy
            If NetProb <> 0 Then
                If SStandEV = 0 Then SStandEV = -2
                If SHitEV = 0 Then SHitEV = -2
            End If

            'Get the final strategy
            handIncluded = False
            If ((total = 12 And soft = True + 1) Or (total = 4)) And SplitTotalIndex(total, soft) > 0 Then
                Select Case cStrat.HandEVs(SplitTotalIndex(total, soft)).EVs.Strat(upcard)
                    Case BJCAGlobalsClass.Strat.P, BJCAGlobalsClass.Strat.PS, BJCAGlobalsClass.Strat.PH, BJCAGlobalsClass.Strat.PD, BJCAGlobalsClass.Strat.PR
                        'Use handincluded to determine if splitting is the strategy
                        handIncluded = True
                End Select
            End If
            If handIncluded Then
                If Strat = BJCAGlobalsClass.Strat.D And cStrat.HandEVs(SplitTotalIndex(total, soft)).DPostallowed(upcard) Then
                    Strat = BJCAGlobalsClass.Strat.PD
                ElseIf Strat = BJCAGlobalsClass.Strat.R And cStrat.HandEVs(SplitTotalIndex(total, soft)).RPostallowed(upcard) > 0 Then
                    Strat = BJCAGlobalsClass.Strat.PR
                Else
                    If SHitEV > SStandEV And cStrat.HandEVs(SplitTotalIndex(total, soft)).HPostallowed(upcard) Then
                        Strat = BJCAGlobalsClass.Strat.PH
                    Else
                        Strat = BJCAGlobalsClass.Strat.PS
                    End If
                End If
            ElseIf Strat = BJCAGlobalsClass.Strat.D Then
                If SStandEV > SHitEV And Not DAN Then
                    Strat = BJCAGlobalsClass.Strat.DS
                End If
            ElseIf Strat = BJCAGlobalsClass.Strat.R Then
                If SStandEV > SHitEV And Not SAN Then
                    Strat = BJCAGlobalsClass.Strat.RS
                End If
            Else
                Strat = BJCAGlobalsClass.Strat.S
                maxev = SStandEV
                If SHitEV > maxev Then
                    Strat = BJCAGlobalsClass.Strat.H
                End If
            End If

            'Now get the final EV's
            NetProb = 0
            NetSProb = 0
            NetHProb = 0
            NetDProb = 0
            NetSurrProb = 0

            SStandEV = 0
            SHitEV = 0
            SDEV = 0
            SSurrEV = 0

            index = PlayerHandTotal(total, soft)
            Do While index
                handIncluded = ((Not handsUsed Or cStrat.HandEVs(index).HandUsed(upcard) = 1) And (PlayerHands(index).Hand.NumCards = nCards Or (orMore And PlayerHands(index).Hand.NumCards >= nCards) Or (orLess And PlayerHands(index).Hand.NumCards <= nCards And PlayerHands(index).Hand.NumCards > 1)))
                If handIncluded Then
                    If handsUsed Then
                        prob = PlayerHands(index).HandEVs.Prob(upcard) * cStrat.HandEVs(index).Multiplier(upcard)
                    Else
                        prob = PlayerHands(index).HandEVs.Prob(upcard)
                    End If
                    NetProb += prob
                    If cStrat.HandEVs(index).SPreallowed(upcard) Then
                        NetSProb += prob
                        SStandEV += prob * PlayerHands(index).HandEVs.StandEV(upcard)
                    End If
                    If cStrat.HandEVs(index).HPreallowed(upcard) Then
                        NetHProb += prob
                        SHitEV += prob * cStrat.HandEVs(index).EVs.HitEV(upcard)
                    End If
                    If cStrat.HandEVs(index).DPreallowed(upcard) Then
                        NetDProb += prob
                        SDEV += prob * PlayerHands(index).HandEVs.DEV(upcard)
                    End If
                    If cStrat.HandEVs(index).RPreallowed(upcard) > 0 Then
                        NetSurrProb += prob
                        SSurrEV += prob * PlayerHands(index).HandEVs.SurrEV(upcard)
                    End If
                End If
                index = PlayerHands(index).NextHand
            Loop

            'Now we need to normalize the ev's
            If NetSProb <> 0 Then
                SStandEV /= NetSProb
                NetSProb /= NetProb
            End If
            If NetHProb <> 0 Then
                SHitEV /= NetHProb
                NetHProb /= NetProb
            End If
            If NetDProb <> 0 Then
                SDEV /= NetDProb
                NetDProb /= NetProb
            End If
            If NetSurrProb <> 0 Then
                SSurrEV /= NetSurrProb
                NetSurrProb /= NetProb
            End If

            'StratEV will hold the SplitEV
            newEvs.Strat(upcard) = Strat
            newEvs.StratStandEV(upcard) = SStandEV
            newEvs.StratHitEV(upcard) = SHitEV
            newEvs.StratDEV(upcard) = SDEV
            newEvs.StratSurrEV(upcard) = SSurrEV
            newEvs.StratEV(upcard) = SSplitEV
            newEvs.NetProb(upcard) = NetProb
            newEvs.NetSProb(upcard) = NetSProb
            newEvs.NetHProb(upcard) = NetHProb
            newEvs.NetDProb(upcard) = NetDProb
            newEvs.NetSurrProb(upcard) = NetSurrProb
        End If

        Return newEvs

    End Function

    Public Sub ComputeNCardStrat()
        Dim index As Integer
        Dim upcard As Integer
        Dim total As Integer
        Dim soft As Integer
        Dim ncards As Integer
        Dim UCStr As String

        ResetForcedRulesHands(Forced)
        Forced.NCardOn = True
        Forced.NCardsCD = 2
        Forced.ComputeStrat = True
        ForcedRulesList.Empty()
        ForcedTableRulesList.Empty()

        'For each total/upcard do the following:
        '   1) First determine the 3 (or minimum) card strat based on CD play and add it to the forced table list
        '   2) See if/when the strategy changes.
        '   3) Confirm that it's still changed with an "or more" strategy"
        '   4) Add the change and recalculate the ev's

        For upcard = 1 To 10
            If UCAllowed(upcard) And CardProb(upcard, 0) > 0 Then
                For total = 21 To 11 Step -1
                    Forced.StratTD(total, False + 1).NCardDeviation(upcard) = 0
                    Dim twoCardStrat As New BJCATDStratClass
                    Dim forcedTableRule As New BJCAForcedRulesClass

                    For index = Forced.NCardsCD + 1 To 21
                        twoCardStrat = ComputeNCardStratEVs(Opt, total, False + 1, upcard, index, False, False, True)
                        If twoCardStrat.NetProb(upcard) > 0 Then
                            With forcedTableRule
                                .Upcard = upcard
                                Select Case upcard
                                    Case 0
                                        UCStr = "A"
                                    Case 9
                                        UCStr = "T"
                                    Case Else
                                        UCStr = CStr(upcard)
                                End Select
                                .Hand.Total = total
                                .Hand.Soft = False
                                .Strat = twoCardStrat.Strat(upcard)
                                .UseSpecificHand = False
                                .Name = "H" + CStr(.Hand.Total) + " vs " + UCStr + " " + C.StratLongText(.Strat)
                                .RuleOn = True
                            End With
                            ForcedTableRulesList.AddForcedRule(forcedTableRule)
                            Exit For
                        End If
                    Next index

                    If index < 21 Then
                        For ncards = index + 1 To 21
                            Dim nCardStrat As New BJCATDStratClass

                            ncardStrat = ComputeNCardStratEVs(Opt, total, False + 1, upcard, ncards, False, False, True)

                            If twoCardStrat.Strat(upcard) <> C.Strat.None And ncardStrat.Strat(upcard) <> C.Strat.None And ncardStrat.Strat(upcard) <> twoCardStrat.Strat(upcard) Then
                                Dim ncardOrMoreStrat As New BJCATDStratClass

                                ncardOrMoreStrat = ComputeNCardStratEVs(Opt, total, False + 1, upcard, ncards, True, False, True)
                                If ncardOrMoreStrat.Strat(upcard) = ncardStrat.Strat(upcard) Then
                                    Dim ForcedRule As New BJCAForcedRulesClass

                                    Forced.StratTD(total, False + 1).NCardStrat(upcard) = ncardOrMoreStrat.Strat(upcard)
                                    Forced.StratTD(total, False + 1).NCardDeviation(upcard) = ncards

                                    With ForcedRule
                                        .Name = CStr(ncards) + "-Card "
                                        .Name += "H"
                                        .Name += CStr(total) + " v " + CStr(upcard) + " " + C.StratShortText(ncardOrMoreStrat.Strat(upcard))
                                        .RuleOn = True
                                        .UseSpecificHand = False

                                        .Hand.Empty()
                                        .Hand.Total = total
                                        .Hand.NumCards = ncards
                                        .Hand.Soft = False
                                        .ExactMatch = False

                                        .OrMore = True
                                        .OrLess = False

                                        .PreSplit = True
                                        .PostSplit = True

                                        .Upcard = upcard
                                        .Strat = ncardOrMoreStrat.Strat(upcard)
                                    End With
                                    ForcedRulesList.AddForcedRule(ForcedRule)
                                    Exit For
                                End If
                            End If
                        Next ncards
                        'If the strategy never changed then see if it's different than the n-card CD or less strategy
                        If ncards > 21 Then
                            Dim ncardOrMoreStrat As New BJCATDStratClass

                            twoCardStrat = ComputeNCardStratEVs(Opt, total, False + 1, upcard, Forced.NCardsCD, False, True, True)
                            ncardOrMoreStrat = ComputeNCardStratEVs(Opt, total, False + 1, upcard, Forced.NCardsCD + 1, True, False, True)

                            If twoCardStrat.Strat(upcard) <> C.Strat.None And ncardOrMoreStrat.Strat(upcard) <> C.Strat.None And ncardOrMoreStrat.Strat(upcard) <> twoCardStrat.Strat(upcard) Then
                                Dim ForcedRule As New BJCAForcedRulesClass

                                Forced.StratTD(total, False + 1).NCardStrat(upcard) = ncardOrMoreStrat.Strat(upcard)
                                Forced.StratTD(total, False + 1).NCardDeviation(upcard) = Forced.NCardsCD + 1

                                'The below rules must be added to fool the CA into listing the Ncard strat that it would do normally anyways.
                                With forcedTableRule
                                    .Upcard = upcard
                                    Select Case upcard
                                        Case 0
                                            UCStr = "A"
                                        Case 9
                                            UCStr = "T"
                                        Case Else
                                            UCStr = CStr(upcard)
                                    End Select
                                    .Hand.Total = total
                                    .Hand.Soft = False
                                    .Strat = twoCardStrat.Strat(upcard)
                                    .UseSpecificHand = False
                                    .Name = "H" + CStr(.Hand.Total) + " vs " + UCStr + " " + C.StratLongText(.Strat)
                                    .RuleOn = True
                                End With
                                ForcedTableRulesList.AddForcedRule(forcedTableRule)

                                With ForcedRule
                                    .Name = CStr(Forced.NCardsCD + 1) + "-Card "
                                    .Name += "H"
                                    .Name += CStr(total) + " v " + CStr(upcard) + " " + C.StratShortText(ncardOrMoreStrat.Strat(upcard))
                                    .RuleOn = True
                                    .UseSpecificHand = False

                                    .Hand.Empty()
                                    .Hand.Total = total
                                    .Hand.NumCards = Forced.NCardsCD + 1
                                    .Hand.Soft = False
                                    .ExactMatch = False

                                    .OrMore = True
                                    .OrLess = False

                                    .PreSplit = True
                                    .PostSplit = True

                                    .Upcard = upcard
                                    .Strat = ncardOrMoreStrat.Strat(upcard)
                                End With
                                ForcedRulesList.AddForcedRule(ForcedRule)
                            End If
                        End If
                    End If
                Next total

                For total = 21 To 13 Step -1
                    Forced.StratTD(total, True + 1).NCardDeviation(upcard) = 0
                    Dim twoCardStrat As New BJCATDStratClass
                    Dim forcedTableRule As New BJCAForcedRulesClass

                    For index = Forced.NCardsCD + 1 To 21
                        twoCardStrat = ComputeNCardStratEVs(Opt, total, True + 1, upcard, index, False, False, True)
                        If twoCardStrat.NetProb(upcard) > 0 Then
                            With forcedTableRule
                                .Upcard = upcard
                                Select Case upcard
                                    Case 0
                                        UCStr = "A"
                                    Case 9
                                        UCStr = "T"
                                    Case Else
                                        UCStr = CStr(upcard)
                                End Select
                                .Hand.Total = total
                                .Hand.Soft = True
                                .Strat = twoCardStrat.Strat(upcard)
                                .UseSpecificHand = False
                                .Name = "S" + CStr(.Hand.Total) + " vs " + UCStr + " " + C.StratLongText(.Strat)
                                .RuleOn = True
                            End With
                            ForcedTableRulesList.AddForcedRule(forcedTableRule)
                            Exit For
                        End If
                    Next index

                    If index < 21 Then
                        For ncards = index + 1 To 21
                            Dim nCardStrat As New BJCATDStratClass

                            ncardStrat = ComputeNCardStratEVs(Opt, total, True + 1, upcard, ncards, False, False, True)

                            If twoCardStrat.Strat(upcard) <> C.Strat.None And ncardStrat.Strat(upcard) <> C.Strat.None And ncardStrat.Strat(upcard) <> twoCardStrat.Strat(upcard) Then
                                Dim ncardOrMoreStrat As New BJCATDStratClass

                                ncardOrMoreStrat = ComputeNCardStratEVs(Opt, total, True + 1, upcard, ncards, True, False, True)
                                If ncardOrMoreStrat.Strat(upcard) = ncardStrat.Strat(upcard) Then
                                    Dim ForcedRule As New BJCAForcedRulesClass

                                    Forced.StratTD(total, True + 1).NCardStrat(upcard) = ncardOrMoreStrat.Strat(upcard)
                                    Forced.StratTD(total, True + 1).NCardDeviation(upcard) = ncards

                                    With ForcedRule
                                        .Name = CStr(ncards) + "-Card "
                                        .Name += "S"
                                        .Name += CStr(total) + " v " + CStr(upcard) + " " + C.StratShortText(ncardOrMoreStrat.Strat(upcard))
                                        .RuleOn = True
                                        .UseSpecificHand = False

                                        .Hand.Empty()
                                        .Hand.Total = total
                                        .Hand.NumCards = ncards
                                        .Hand.Soft = True
                                        .ExactMatch = False

                                        .OrMore = True
                                        .OrLess = False

                                        .PreSplit = True
                                        .PostSplit = True

                                        .Upcard = upcard
                                        .Strat = ncardOrMoreStrat.Strat(upcard)
                                    End With
                                    ForcedRulesList.AddForcedRule(ForcedRule)
                                    Exit For
                                End If
                            End If
                        Next ncards
                        'If the strategy never changed then see if it's different than the n-card CD or less strategy
                        If ncards > 21 Then
                            Dim ncardOrMoreStrat As New BJCATDStratClass

                            twoCardStrat = ComputeNCardStratEVs(Opt, total, True + 1, upcard, Forced.NCardsCD, False, True, True)
                            ncardOrMoreStrat = ComputeNCardStratEVs(Opt, total, True + 1, upcard, Forced.NCardsCD + 1, True, False, True)

                            If twoCardStrat.Strat(upcard) <> C.Strat.None And ncardOrMoreStrat.Strat(upcard) <> C.Strat.None And ncardOrMoreStrat.Strat(upcard) <> twoCardStrat.Strat(upcard) Then
                                Dim ForcedRule As New BJCAForcedRulesClass

                                Forced.StratTD(total, True + 1).NCardStrat(upcard) = ncardOrMoreStrat.Strat(upcard)
                                Forced.StratTD(total, True + 1).NCardDeviation(upcard) = Forced.NCardsCD + 1

                                'The below rules must be added to fool the CA into listing the Ncard strat that it would do normally anyways.
                                With forcedTableRule
                                    .Upcard = upcard
                                    Select Case upcard
                                        Case 0
                                            UCStr = "A"
                                        Case 9
                                            UCStr = "T"
                                        Case Else
                                            UCStr = CStr(upcard)
                                    End Select
                                    .Hand.Total = total
                                    .Hand.Soft = True
                                    .Strat = twoCardStrat.Strat(upcard)
                                    .UseSpecificHand = False
                                    .Name = "S" + CStr(.Hand.Total) + " vs " + UCStr + " " + C.StratLongText(.Strat)
                                    .RuleOn = True
                                End With
                                ForcedTableRulesList.AddForcedRule(forcedTableRule)

                                With ForcedRule
                                    .Name = CStr(Forced.NCardsCD + 1) + "-Card "
                                    .Name += "S"
                                    .Name += CStr(total) + " v " + CStr(upcard) + " " + C.StratShortText(ncardOrMoreStrat.Strat(upcard))
                                    .RuleOn = True
                                    .UseSpecificHand = False

                                    .Hand.Empty()
                                    .Hand.Total = total
                                    .Hand.NumCards = Forced.NCardsCD + 1
                                    .Hand.Soft = True
                                    .ExactMatch = False

                                    .OrMore = True
                                    .OrLess = False

                                    .PreSplit = True
                                    .PostSplit = True

                                    .Upcard = upcard
                                    .Strat = ncardOrMoreStrat.Strat(upcard)
                                End With
                                ForcedRulesList.AddForcedRule(ForcedRule)
                            End If
                        End If
                    End If
                Next total

                For total = 10 To 5 Step -1
                    Forced.StratTD(total, False + 1).NCardDeviation(upcard) = 0
                    Dim twoCardStrat As New BJCATDStratClass
                    Dim forcedTableRule As New BJCAForcedRulesClass

                    For index = Forced.NCardsCD + 1 To 21
                        twoCardStrat = ComputeNCardStratEVs(Opt, total, False + 1, upcard, index, False, False, True)
                        If twoCardStrat.NetProb(upcard) > 0 Then
                            With forcedTableRule
                                .Upcard = upcard
                                Select Case upcard
                                    Case 0
                                        UCStr = "A"
                                    Case 9
                                        UCStr = "T"
                                    Case Else
                                        UCStr = CStr(upcard)
                                End Select
                                .Hand.Total = total
                                .Hand.Soft = False
                                .Strat = twoCardStrat.Strat(upcard)
                                .UseSpecificHand = False
                                .Name = "H" + CStr(.Hand.Total) + " vs " + UCStr + " " + C.StratLongText(.Strat)
                                .RuleOn = True
                            End With
                            ForcedTableRulesList.AddForcedRule(forcedTableRule)
                            Exit For
                        End If
                    Next index

                    If index < 21 Then
                        For ncards = index + 1 To 21
                            Dim nCardStrat As New BJCATDStratClass

                            ncardStrat = ComputeNCardStratEVs(Opt, total, False + 1, upcard, ncards, False, False, True)

                            If twoCardStrat.Strat(upcard) <> C.Strat.None And ncardStrat.Strat(upcard) <> C.Strat.None And ncardStrat.Strat(upcard) <> twoCardStrat.Strat(upcard) Then
                                Dim ncardOrMoreStrat As New BJCATDStratClass

                                ncardOrMoreStrat = ComputeNCardStratEVs(Opt, total, False + 1, upcard, ncards, True, False, True)
                                If ncardOrMoreStrat.Strat(upcard) = ncardStrat.Strat(upcard) Then
                                    Dim ForcedRule As New BJCAForcedRulesClass

                                    Forced.StratTD(total, False + 1).NCardStrat(upcard) = ncardOrMoreStrat.Strat(upcard)
                                    Forced.StratTD(total, False + 1).NCardDeviation(upcard) = ncards

                                    With ForcedRule
                                        .Name = CStr(ncards) + "-Card "
                                        .Name += "H"
                                        .Name += CStr(total) + " v " + CStr(upcard) + " " + C.StratShortText(ncardOrMoreStrat.Strat(upcard))
                                        .RuleOn = True
                                        .UseSpecificHand = False

                                        .Hand.Empty()
                                        .Hand.Total = total
                                        .Hand.NumCards = ncards
                                        .Hand.Soft = False
                                        .ExactMatch = False

                                        .OrMore = True
                                        .OrLess = False

                                        .PreSplit = True
                                        .PostSplit = True

                                        .Upcard = upcard
                                        .Strat = ncardOrMoreStrat.Strat(upcard)
                                    End With
                                    ForcedRulesList.AddForcedRule(ForcedRule)
                                    Exit For
                                End If
                            End If
                        Next ncards
                        'If the strategy never changed then see if it's different than the n-card CD or less strategy
                        If ncards > 21 Then
                            Dim ncardOrMoreStrat As New BJCATDStratClass

                            twoCardStrat = ComputeNCardStratEVs(Opt, total, False + 1, upcard, Forced.NCardsCD, False, True, True)
                            ncardOrMoreStrat = ComputeNCardStratEVs(Opt, total, False + 1, upcard, Forced.NCardsCD + 1, True, False, True)

                            If twoCardStrat.Strat(upcard) <> C.Strat.None And ncardOrMoreStrat.Strat(upcard) <> C.Strat.None And ncardOrMoreStrat.Strat(upcard) <> twoCardStrat.Strat(upcard) Then
                                Dim ForcedRule As New BJCAForcedRulesClass

                                Forced.StratTD(total, False + 1).NCardStrat(upcard) = ncardOrMoreStrat.Strat(upcard)
                                Forced.StratTD(total, False + 1).NCardDeviation(upcard) = Forced.NCardsCD + 1

                                'The below rules must be added to fool the CA into listing the Ncard strat that it would do normally anyways.
                                With forcedTableRule
                                    .Upcard = upcard
                                    Select Case upcard
                                        Case 0
                                            UCStr = "A"
                                        Case 9
                                            UCStr = "T"
                                        Case Else
                                            UCStr = CStr(upcard)
                                    End Select
                                    .Hand.Total = total
                                    .Hand.Soft = False
                                    .Strat = twoCardStrat.Strat(upcard)
                                    .UseSpecificHand = False
                                    .Name = "H" + CStr(.Hand.Total) + " vs " + UCStr + " " + C.StratLongText(.Strat)
                                    .RuleOn = True
                                End With
                                ForcedTableRulesList.AddForcedRule(forcedTableRule)

                                With ForcedRule
                                    .Name = CStr(Forced.NCardsCD + 1) + "-Card "
                                    .Name += "H"
                                    .Name += CStr(total) + " v " + CStr(upcard) + " " + C.StratShortText(ncardOrMoreStrat.Strat(upcard))
                                    .RuleOn = True
                                    .UseSpecificHand = False

                                    .Hand.Empty()
                                    .Hand.Total = total
                                    .Hand.NumCards = Forced.NCardsCD + 1
                                    .Hand.Soft = False
                                    .ExactMatch = False

                                    .OrMore = True
                                    .OrLess = False

                                    .PreSplit = True
                                    .PostSplit = True

                                    .Upcard = upcard
                                    .Strat = ncardOrMoreStrat.Strat(upcard)
                                End With
                                ForcedRulesList.AddForcedRule(ForcedRule)
                            End If
                        End If
                    End If
                Next total
            End If
        Next upcard

    End Sub

#End Region







#Region " Methods Under Construction"

#End Region

End Class
