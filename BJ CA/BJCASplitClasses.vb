<Serializable()> Public Class BJCANPxClass
    Public Name As String
    Public NP As Integer             'Number of Paircards removed after the initial pair
    Public NN As Integer             'Number of Non-paircards removed after the initial pair
    Public Nx As Integer             'Number of cards removed after reaching the maximum number of hands in a split
    Public UCH As Integer            'Unconditional split hand that the current split hand is based on
    Public CH As Integer             'Conditional split hand that the current split hand is based on
    Public Used(3) As Boolean        'The hand is actually used during play and not just for calculations
End Class

<Serializable()> Public Class BJCASplitRoundsClass
    Public Name(3, 8) As String
    Public Hands(3, 8, 10, 1) As Integer   'SPL, Rounds, Hand, x/N   The number of times each hand is used in a round
    Public NP As Integer             'Number of Paircards removed after the initial pair
    Public NN As Integer             'Number of Non-paircards removed after the initial pair
    Public Nx As Integer             'Number of cards removed after reaching the maximum number of hands in a split
End Class

<Serializable()> Public Class BJCASplitRoundProbsClass
    Public RoundProbs(3, 8) As Double        'SPL, Rounds       The prob of each round occuring
    Public NetHandProbs(3, 10, 0) As Double  'SPL, Hands, x/N   The net prob of each hand type during the entire calc 
End Class

<Serializable()> Public Class BJCASplitProbsClass
    'Probabilties for N hands occuring are simply 1-p(x)
    Public PxCards(10, 10) As Double       'Shoe state, card     Probability of receiving an x

    'These are used in OBBO to store the Conditional EV's given that the dealer doesn't have BJ
    'These are the same as the usual EV's when not using OBBO or ENHC
    Public PBJ(10, 10) As Double           'Shoe state, card     Probability of getting blackjack - needed for paircard
    Public PCEVxCards(10, 10) As Double    'Shoe state, card     Conditional probability of receiving an x
    Public PBJxCards(10, 10) As Double     'Shoe state, card     Probability of receiving an x given dealer BJ

    'This is only used to compute split probs for the prob of BJ given a card drawn
    Public PPairBJCards(10, 10) As Double  'Shoe state, card     Conditional probability of receiving an x
End Class

<Serializable()> Public Class BJCASplitStateStrategyClass
    'EVs for each second card and net evs
    Public EVx(10) As Double               'Shoe state           Overall EV for an x
    Public EVN(10) As Double               'Shoe state           Overall EV for an N
    Public EVCards(10, 10) As Double       'Shoe state, card     EV of the individual 2-card hand
    Public SPL(3) As Double                'Split EVs
    Public SPL2(3) As Double               'Split EV's using removal hands
    Public SPL3(3) As Double               'Split EV's using removal hands

    'These are used in OBBO to store the Conditional EV's given that the dealer doesn't have BJ
    '    These are the same as the usual EV's when not using OBBO or ENHC
    Public CEVx(10) As Double             'Shoe state           Overall CEV for an x
    Public CEVN(10) As Double             'Shoe state           Overall CEV for an N
    Public CEVCards(10, 10) As Double     'Shoe state, card     Conditional EV of the individual 2-card hand

    'Thesed are only used in OBBO for determining the Bust Probabilities give a dealer BJ
    Public PResx(10) As Double               'Shoe state          Overall Probability of Busting for an x
    Public PResN(10) As Double               'Shoe state          Overall Probability of Busting for an N
    Public PResEVx(10) As Double             'Shoe state          Overall Probability of Busting for an x
    Public PResEVN(10) As Double             'Shoe state          Overall Probability of Busting for an N
    Public PResCards(10, 10) As Double       'Shoe State, card    Probability of Busting given dealer BJ
    Public PResEVCards(10, 10) As Double     'Shoe state, card     Net pEV of resolved hands

    'Thesed are only used in OBBO and hold the values when the player loses 0 to dealer BJ
    Public BBOx(10) As Double               'Shoe state           Overall EV for a BBO x
    Public BBON(10) As Double               'Shoe state           Overall EV for a BBO N
    Public BBOCards(10, 10) As Double       'Shoe State, card     EV of the individual BBO 2-card hand

    'Thesed are only used in OBBO and hold the values when the player loses 1 to dealer BJ
    Public OBBOx(10) As Double               'Shoe state          Overall EV for a OBBO x
    Public OBBON(10) As Double               'Shoe state          Overall EV for a OBBO N
    Public OBBOCards(10, 10) As Double       'Shoe State, card    EV of the individual OBBO 2-card hand

    Public Sub Empty()
        Dim i As Integer
        Dim hands As Integer
        Dim card As Integer

        For hands = 0 To 10
            EVx(hands) = 0
            EVN(hands) = 0
            CEVx(hands) = 0
            CEVN(hands) = 0
            PResx(hands) = 0
            PResN(hands) = 0
            PResEVx(hands) = 0
            PResEVN(hands) = 0
            BBOx(hands) = 0
            BBON(hands) = 0
            OBBOx(hands) = 0
            OBBON(hands) = 0
            For card = 0 To 10
                EVCards(hands, card) = 0
                CEVCards(hands, card) = 0
                PResCards(hands, card) = 0
                PResEVCards(hands, card) = 0
                BBOCards(hands, card) = 0
                OBBOCards(hands, card) = 0
            Next card
        Next hands

        For i = 0 To 3
            SPL(i) = 0
            SPL2(i) = 0
            SPL3(i) = 0
        Next i
    End Sub
End Class

