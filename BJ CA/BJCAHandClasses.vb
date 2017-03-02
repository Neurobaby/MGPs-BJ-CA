<Serializable()> Public Class BJCAHandClass
    Public Cards(10) As Integer        'Number of each card in the hand
    Public NumCards As Integer         'Total number of cards in the hand
    Public Total As Integer            'Total value of the hand
    Public Soft As Boolean             'Is the hand soft?

    Public Sub Empty()
        Cards(0) = 0
        Cards(1) = 0
        Cards(2) = 0
        Cards(3) = 0
        Cards(4) = 0
        Cards(5) = 0
        Cards(6) = 0
        Cards(7) = 0
        Cards(8) = 0
        Cards(9) = 0
        Cards(10) = 0
        NumCards = 0
        Total = 0
        Soft = 0
    End Sub

    Public Sub Copy(ByVal fromHand As BJCAHandClass)
        Cards(0) = fromHand.Cards(0)
        Cards(1) = fromHand.Cards(1)
        Cards(2) = fromHand.Cards(2)
        Cards(3) = fromHand.Cards(3)
        Cards(4) = fromHand.Cards(4)
        Cards(5) = fromHand.Cards(5)
        Cards(6) = fromHand.Cards(6)
        Cards(7) = fromHand.Cards(7)
        Cards(8) = fromHand.Cards(8)
        Cards(9) = fromHand.Cards(9)
        Cards(10) = fromHand.Cards(10)
        NumCards = fromHand.NumCards
        Total = fromHand.Total
        Soft = fromHand.Soft
    End Sub

    Public Sub UpdateTotal()
        Total = 0
        Soft = False
        NumCards = 0
        Total += Cards(1)
        Total += 2 * Cards(2)
        Total += 3 * Cards(3)
        Total += 4 * Cards(4)
        Total += 5 * Cards(5)
        Total += 6 * Cards(6)
        Total += 7 * Cards(7)
        Total += 8 * Cards(8)
        Total += 9 * Cards(9)
        Total += 10 * Cards(10)
        NumCards += Cards(0)
        NumCards += Cards(1)
        NumCards += Cards(2)
        NumCards += Cards(3)
        NumCards += Cards(4)
        NumCards += Cards(5)
        NumCards += Cards(6)
        NumCards += Cards(7)
        NumCards += Cards(8)
        NumCards += Cards(9)
        NumCards += Cards(10)
        If Total < 12 And Cards(1) > 0 Then
            Soft = True
            Total += 10
        End If
    End Sub

    Public Function SameAs(ByVal fromHand As BJCAHandClass) As Boolean
        SameAs = (Cards(0) = fromHand.Cards(0) And _
        Cards(1) = fromHand.Cards(1) And _
        Cards(2) = fromHand.Cards(2) And _
        Cards(3) = fromHand.Cards(3) And _
        Cards(4) = fromHand.Cards(4) And _
        Cards(5) = fromHand.Cards(5) And _
        Cards(6) = fromHand.Cards(6) And _
        Cards(7) = fromHand.Cards(7) And _
        Cards(8) = fromHand.Cards(8) And _
        Cards(9) = fromHand.Cards(9) And _
        Cards(10) = fromHand.Cards(10) And _
        NumCards = fromHand.NumCards And _
        Total = fromHand.Total And _
        Soft = fromHand.Soft)
    End Function

    Public Function Includes(ByVal includedHand As BJCAHandClass) As Boolean
        Includes = (Cards(0) >= includedHand.Cards(0) And _
        Cards(1) >= includedHand.Cards(1) And _
        Cards(2) >= includedHand.Cards(2) And _
        Cards(3) >= includedHand.Cards(3) And _
        Cards(4) >= includedHand.Cards(4) And _
        Cards(5) >= includedHand.Cards(5) And _
        Cards(6) >= includedHand.Cards(6) And _
        Cards(7) >= includedHand.Cards(7) And _
        Cards(8) >= includedHand.Cards(8) And _
        Cards(9) >= includedHand.Cards(9) And _
        Cards(10) >= includedHand.Cards(10) And _
        NumCards >= includedHand.NumCards)
    End Function

    Public Sub Deal(ByVal card As Integer)
        NumCards += 1
        Cards(card) += 1
        Total += card
        If card = 1 And Total < 12 Then
            Total += 10
            Soft = True
        ElseIf Total > 21 And Soft Then
            Total -= 10
            Soft = False
        End If
    End Sub

    Public Sub Undeal(ByVal card As Integer)
        Cards(card) -= 1
        NumCards -= 1
        Total -= card

        If card = 1 And Cards(1) = 0 And Soft Then
            Total -= 10
            Soft = False
        ElseIf Total < 12 And Cards(1) > 0 And Not Soft Then
            Total += 10
            Soft = True
        End If
    End Sub

    Public Function IsBJ() As Boolean
        Return (NumCards = 2 And Cards(1) = 1 And Cards(10) = 1)
    End Function

    Public Function IsPair() As Boolean
        Return (NumCards = 2 And (Cards(1) = 2 Or Cards(2) = 2 _
            Or Cards(3) = 2 Or Cards(4) = 2 Or Cards(5) = 2 _
            Or Cards(6) = 2 Or Cards(7) = 2 Or Cards(8) = 2 _
            Or Cards(9) = 2 Or Cards(10) = 2))
    End Function

    Public Function Paircard() As Integer
        Dim card As Integer

        If NumCards <> 2 Then
            Return 0
        Else
            For card = 1 To 10
                If Cards(card) = 2 Then
                    Exit For
                End If
            Next
            If card = 11 Then
                Return 0
            Else
                Return card
            End If
        End If
    End Function

    Public Function BJProb(ByVal upcard As Integer, ByVal shoe As BJCAShoeClass, ByVal handIncluded As Boolean)
        If (upcard <> 1 And upcard <> 10) Or (Not handIncluded And Not shoe.HandPossible(Me)) Then
            Return 0
        Else
            If upcard = 1 Then
                If handIncluded Then
                    Return shoe.Cards(10) / shoe.CardsLeft
                Else
                    Return (shoe.Cards(10) - Cards(10)) / (shoe.CardsLeft - NumCards)
                End If
            Else
                If handIncluded Then
                    Return shoe.Cards(1) / shoe.CardsLeft
                Else
                    Return (shoe.Cards(1) - Cards(1)) / (shoe.CardsLeft - NumCards)
                End If
            End If
        End If
    End Function

End Class

<Serializable()> Public Class BJCAHandEVsClass
    Public Prob(10) As Double                 'Probability of Hand occurring given upcard
    Public SumSuited(10) As Double            'Net probability of the hand being suited
    Public ProbSuited(10, 4) As Double        'Probability of Hand being a given suit
    Public SuitedPossible(10, 4) As Boolean   'Possibility of Hand being suited

    Public StandEV(10) As Double              'Upcard   Stand EV's relative to each dealer upcard
    Public StandPushEV(10) As Double          'Upcard   EV of pushing; can be used for special rules and variance
    Public StandDone(10) As Boolean
    Public DealerProbs(10, 4) As Double       'Upcard

    Public DEV(10) As Double                  'Upcard   Double EV's relative to each dealer upcard
    Public DPushEV(10) As Double              'Upcard   EV of pushing; can be used for special rules and variance

    Public DStrat(10) As Integer
    Public DStratEV(10) As Double
    Public DStratPushEV(10) As Double         'Upcard   This is necessary because of soft doubles can be hard

    Public SurrEV(10) As Double               'Upcard   Surrender EV's relative to each dealer upcard

    Public BJProb(10) As Double               'Upcard   Stand EV's relative to each dealer upcard
    Public BJStandEV(10) As Double            'Upcard   Stand EV's relative to each dealer upcard

    Public BonusEV(10) As Double               'Upcard   Stand EV's relative to each dealer upcard

    Public Sub New()
        BJStandEV(1) = -1
        BJStandEV(10) = -1
    End Sub

    Public Sub Empty()
        Dim card As Integer
        Dim total As Integer

        For card = 0 To 10
            Prob(card) = 0
            StandEV(card) = 0
            StandPushEV(card) = 0
            StandDone(card) = 0
            DEV(card) = 0
            DPushEV(card) = 0
            DStrat(card) = 0
            DStratEV(card) = 0
            DStratPushEV(card) = 0
            SurrEV(card) = 0
            BJStandEV(card) = 0
            BonusEV(card) = 0
            For total = 0 To 4
                DealerProbs(card, total) = 0
            Next total
        Next card
        BJStandEV(1) = -1
        BJStandEV(10) = -1
    End Sub

    Public Sub Copy(ByVal originalHand As BJCAHandEVsClass)
        Dim card As Integer
        Dim total As Integer

        For card = 0 To 10
            Prob(card) = originalHand.Prob(card)
            StandEV(card) = originalHand.StandEV(card)
            StandPushEV(card) = originalHand.StandPushEV(card)
            StandDone(card) = originalHand.StandDone(card)
            DEV(card) = originalHand.DEV(card)
            DPushEV(card) = originalHand.DPushEV(card)
            DStrat(card) = originalHand.DStrat(card)
            DStratEV(card) = originalHand.DStratEV(card)
            DStratPushEV(card) = originalHand.DStratPushEV(card)
            SurrEV(card) = originalHand.SurrEV(card)
            BJStandEV(card) = originalHand.BJStandEV(card)
            BonusEV(card) = originalHand.BonusEV(card)
            For total = 0 To 4
                DealerProbs(card, total) = originalHand.DealerProbs(card, total)
            Next total
        Next card
    End Sub
End Class

<Serializable()> Public Class BJCAStratHandEVsClass
    Public Prob(10) As Double                 'Probability of Hand occurring given upcard

    Public Strat(10) As Integer               'Upcard     Stand EV's relative to each dealer upcard
    Public StratEV(10) As Double              'Upcard     Stand EV's relative to each dealer upcard
    Public StratPushEV(10) As Double          'Upcard     Stand EV's relative to each dealer upcard
    Public StratBustProb(10) As Double        'Upcard     Stand EV's relative to each dealer upcard

    Public StandEV(10) As Double              'Upcard     Stand EV's relative to each dealer upcard
    Public StandPushEV(10) As Double          'Upcard     EV of pushing; can be used for special rules and variance
    Public DealerProbs(10, 4) As Double       'Upcard

    Public DEV(10) As Double                  'Upcard     Double EV's relative to each dealer upcard
    Public DPushEV(10) As Double              'Upcard     EV of pushing; can be used for special rules and variance
    Public DBustProb(10) As Double            'Upcard     EV of pushing; can be used for special rules and variance

    Public SurrEV(10) As Double               'Upcard     Surrender EV's relative to each dealer upcard

    Public HitEV(10) As Double                'Upcard     Double EV's relative to each dealer upcard
    Public HitPushEV(10) As Double            'Upcard     EV of pushing; can be used for special rules and variance
    Public HitBustProb(10) As Double          'Upcard     EV of pushing; can be used for special rules and variance

    Public SplitEV(10) As Double              'Upcard     Surrender EV's relative to each dealer upcard

    Public DAllowed(10) As Boolean            'Double Allowed Pre-Split
    Public RAllowed(10) As Integer            'Surrender Type Allowed Pre-Split
    Public PAllowed(10) As Boolean            'Split Allowed

    Public Sub Empty()
        Dim card As Integer
        Dim total As Integer

        For card = 0 To 10
            Prob(card) = 0
            Strat(card) = 0
            StratEV(card) = 0
            StratPushEV(card) = 0
            StratBustProb(card) = 0
            StandEV(card) = 0
            StandPushEV(card) = 0
            DEV(card) = 0
            DPushEV(card) = 0
            DBustProb(card) = 0
            SurrEV(card) = 0
            HitEV(card) = 0
            HitPushEV(card) = 0
            HitBustProb(card) = 0
            SplitEV(card) = 0
            DAllowed(card) = False
            RAllowed(card) = 0
            PAllowed(card) = False
            For total = 0 To 4
                DealerProbs(card, total) = 0
            Next total
        Next card
    End Sub

    Public Sub Copy(ByVal originalHand As BJCAStratHandEVsClass)
        Dim card As Integer
        Dim total As Integer

        For card = 0 To 10
            Prob(card) = originalHand.Prob(card)
            StandEV(card) = originalHand.StandEV(card)
            StandPushEV(card) = originalHand.StandPushEV(card)
            DEV(card) = originalHand.DEV(card)
            DPushEV(card) = originalHand.DPushEV(card)
            SurrEV(card) = originalHand.SurrEV(card)
            HitEV(card) = originalHand.HitEV(card)
            HitPushEV(card) = originalHand.HitPushEV(card)
            SplitEV(card) = originalHand.SplitEV(card)
            For total = 0 To 4
                DealerProbs(card, total) = originalHand.DealerProbs(card, total)
            Next total
        Next card
    End Sub
End Class

<Serializable()> Public Class BJCAPlayerHandClass
    Public Hand As New BJCAHandClass            'The actual hand
    Public HandEVs As New BJCAHandEVsClass      'EVs for full deck

    Public HitHand(10) As Integer               'Hand in index that follows if (card) is hit
    Public NextHand As Integer                  'Next hand in list with same count

    'Strategies allowed by the game rules
    Public PAllowed(10) As Boolean              'Split Allowed
    Public DPreallowed(10) As Boolean           'Double Allowed Pre-Split
    Public RPreallowed(10) As Integer           'Surrender Type Allowed Pre-Split

    Public DPostallowed(10) As Boolean          'Double Allowed Post-Split
    Public RPostallowed(10) As Integer          'Surrender Type Allowed Post-Split

    Public PairIndex(10) As Integer             'Paircard - stores the reference index for the split data
    Public SplitEVs(6, 11) As BJCAHandEVsClass

    Public SuitedBonusEVs As BJCASuitedBonusEVClass   'Only initialize if suited bonus is possible
End Class

<Serializable()> Public Class BJCADealerHandClass
    Public Hand As New BJCAHandClass               'The actual hand
    Public NextHandTotal As Integer                'Next hand in list with same total
    Public NextHandTotalUC(10) As Integer          'Next hand in list with same total for a given upcard
    Public Multiplier(10) As Integer               'Number of different ways the Dealer hand can be reached for a given upcard
End Class

<Serializable()> Public Class BJCARealtimeHandClass
    Public Hand As New BJCAStratHandEVsClass       'The hand evs
    Public SuitedEVs As BJCASuitedBonusEVClass     'This is only initialized if needed
End Class

<Serializable()> Public Class BJCASplitHandEVsClass
    Public Prob(10) As Double                 'Probability of Hand occurring given upcard
    Public StandEV(10) As Double              'Upcard   Stand EV's relative to each dealer upcard
    Public DEV(10) As Double                  'Upcard   Double EV's relative to each dealer upcard
    Public SurrEV(10) As Double               'Upcard   Surrender EV's relative to each dealer upcard
    Public BJProb(10) As Double               'Upcard   Stand EV's relative to each dealer upcard
    Public BJEV(10) As Double                 'Upcard   Stand EV's relative to each dealer upcard
    Public BonusEV(10) As Double              'Upcard   Stand EV's relative to each dealer upcard

    'The following are used for OBBO Calcs
    Public CEV(10) As Double                  'Upcard   CEV
    Public BJCEV(10) As Double                'Upcard   p*EV
    Public PRes(10) As Double                 'Upcard   Probability of Resolving Hand with BJ
    Public BBOEV(10) As Double                'Upcard   BBO EV
    Public OBBOEV(10) As Double               'Upcard   OBBO EV

    Public Sub New()
        BJEV(1) = -1
        BJEV(10) = -1
    End Sub

    Public Sub Empty()
        Dim card As Integer

        For card = 0 To 10
            Prob(card) = 0
            StandEV(card) = 0
            DEV(card) = 0
            SurrEV(card) = 0
            BJEV(card) = 0
            BonusEV(card) = 0
        Next card
        BJEV(1) = -1
        BJEV(10) = -1
    End Sub

    Public Sub Copy(ByVal originalHand As BJCAHandEVsClass)
        Dim card As Integer

        For card = 0 To 10
            Prob(card) = originalHand.Prob(card)
            StandEV(card) = originalHand.StandEV(card)
            DEV(card) = originalHand.DEV(card)
            SurrEV(card) = originalHand.SurrEV(card)
            BJEV(card) = originalHand.BJStandEV(card)
            BonusEV(card) = originalHand.BonusEV(card)
        Next card
    End Sub
End Class
