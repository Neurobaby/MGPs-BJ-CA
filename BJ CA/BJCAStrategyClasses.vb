<Serializable()> Public Class BJCAStrategyClass
    Public Name As String
    Public NCardsCD As Integer
    Public ComputeStrat As Boolean
    Public NCardOn As Boolean

    Public HandEVs(3084) As BJCAHandStrategyClass    '3072 = Max hands without busting (10 1 card, 2 0 hands)
    Public SplitStateEVs(10, 10) As BJCASplitStateStrategyClass
    Public GameEVs As New BJCAGameEVsClass
    Public StratTD(21, 1) As BJCATDStratClass
End Class

<Serializable()> Public Class BJCAHandStrategyClass
    'Strategies allowed by the forced rules
    Public SPreallowed(10) As Boolean     'Stand Allowed Pre-Split
    Public DPreallowed(10) As Boolean     'Double Allowed Pre-Split
    Public HPreallowed(10) As Boolean     'Hit Allowed Pre-Split
    Public RPreallowed(10) As Integer     'Surrender Allowed Pre-Split
    Public PAllowed(10) As Boolean        'Split Allowed

    Public SPostallowed(10) As Boolean    'Stand Allowed Post-Split
    Public DPostallowed(10) As Boolean    'Double Allowed Post-Split
    Public HPostallowed(10) As Boolean    'Hit Allowed Post-Split
    Public RPostallowed(10) As Integer    'Surrender Allowed Post-Split

    Public EVs As New BJCAHandStrategyEVsClass
    Public PreForced(10) As Boolean
    Public PostForced(10) As Boolean
    Public HandUsed(10) As Integer        'Is the hand used for the given strategy
    Public Multiplier(10) As Integer      'How often is the hand encountered for the given strategy

    '    Public HandUsedPost(10) As Integer              'Is the hand used after splitting
    '    Public MultiplierPost(10, 10) As Integer        'Paircard, Upcard      Post-Split Multiplier

    Public SplitEV(10) As Double          'EV based on the number of splits allowed
    Public SPLEV(10, 3) As Double         'All SPL EV's up to the number of splits allowed
    Public SplitData(6, 10) As BJCAHandStrategyEVsClass     'Paircard, Split Shoe State      Holds split hand probs

    Public Sub New()
        Dim upcard As Integer
        Dim paircard As Integer

        For upcard = 1 To 10
            SPreallowed(upcard) = True
            DPreallowed(upcard) = True
            HPreallowed(upcard) = True

            SPostallowed(upcard) = True
            HPostallowed(upcard) = True

            '            For paircard = 1 To 10
            '                MultiplierPost(paircard, upcard) = 1
            '            Next paircard
        Next upcard
    End Sub
End Class

<Serializable()> Public Class BJCAHandStrategyEVsClass
    Public Strat(10) As Integer             'Strategy of hand for given strategy
    Public ForcedPostStrat(10) As Integer   'Forced Post-Split Strategy of hand
    Public StratEV(10) As Double            'EV of hand for given strategy
    Public StratPushEV(10) As Double        'EV of push for given strategy

    Public HitEV(10) As Double              'Hit EV for given strategy
    Public HitPushEV(10) As Double          'Hit Push EV for given strategy
End Class

<Serializable()> Public Class BJCATDStratClass
    Public Strat(10) As Integer
    Public NetProb(10) As Double
    '    Public Forced(10) As Boolean

    Public NetSProb(10) As Double
    Public NetHProb(10) As Double
    Public NetDProb(10) As Double
    Public NetSurrProb(10) As Double

    Public SStandEV(10) As Double
    Public SHitEV(10) As Double
    Public SDEV(10) As Double
    Public SSurrEV(10) As Double

    Public StratEV(10) As Double
    Public StratStandEV(10) As Double
    Public StratDEV(10) As Double
    Public StratSurrEV(10) As Double
    Public StratHitEV(10) As Double

    Public NCardDeviation(10) As Integer
    Public NCardStrat(10) As Integer
End Class

<Serializable()> Public Class BJCAGameEVsClass
    Public NetGameEV As Double
    Public CardProbs(10) As Double
    Public FirstCardEVs(10) As Double
    Public UpcardEVs(10) As Double
End Class