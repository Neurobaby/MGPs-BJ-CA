<Serializable()> Public Class BJCAGlobalsClass

    Public ReadOnly MaxDecks As Integer = 500
    Public ReadOnly NumStrats As Integer = 17
    Public ReadOnly NumExceptionTypes As Integer = 14
    Public ReadOnly MaxHandNumCards() As Integer = {21, 10, 7, 5, 4, 3, 3, 2, 2, 2}
    Public StratFullText() As String
    Public StratLongText() As String
    Public StratShortText() As String
    Public ExText() As String

    Public Sub New()
        LoadStratNames()
        LoadExceptionNames()
    End Sub

    Public Enum ExType
        None = 0

        CDTDPre = 1
        CDTCPre = 2
        CDForcedPre = 3
        ForcedTDPre = 4
        ForcedTCPre = 5
        TCTDPre = 6

        CDTDPost = 7
        CDTCPost = 8
        CDForcedPost = 9
        CDCDPost = 10

        TDTD = 11
        TCTC = 12
        ForcedForced = 13
    End Enum

    Public Enum Strat
        None = 0        'Blank strategy
        S = 1           'Stand
        D = 2           'Double, Hit
        DS = 3          'Double, Stand
        H = 4           'Hit
        R = 5           'Surrender, Hit
        RS = 6          'Surrender, Stand
        P = 7           'Split
        PS = 8          'Split, Stand
        PD = 9          'Split, Double
        PH = 10         'Split, Hit
        PR = 11         'Split, Surrender

        xS = 12         'Don't Stand
        xD = 13         'Don't Double
        xH = 14         'Don't Hit
        xR = 15         'Don't Surrender
        xP = 16         'Don't Split
    End Enum

    Private Sub LoadStratNames()
        ReDim StratFullText(NumStrats - 1)
        ReDim StratLongText(NumStrats - 1)
        ReDim StratShortText(NumStrats - 1)

        StratFullText(0) = "None"
        StratFullText(1) = "Stand"
        StratFullText(2) = "Double"
        StratFullText(3) = "Double, Stand"
        StratFullText(4) = "Hit"
        StratFullText(5) = "Surrender"
        StratFullText(6) = "Surrender, Stand"
        StratFullText(7) = "Split"
        StratFullText(8) = "Split, Stand"
        StratFullText(9) = "Split, Double"
        StratFullText(10) = "Split, Hit"
        StratFullText(11) = "Split, Surrender"
        StratFullText(12) = "Don't Stand"
        StratFullText(13) = "Don't Double"
        StratFullText(14) = "Don't Hit"
        StratFullText(15) = "Don't Surrender"
        StratFullText(16) = "Don't Split"

        StratLongText(0) = "--"
        StratLongText(1) = "S"
        StratLongText(2) = "D"
        StratLongText(3) = "D, S"
        StratLongText(4) = "H"
        StratLongText(5) = "R"
        StratLongText(6) = "R, S"
        StratLongText(7) = "P"
        StratLongText(8) = "P, S"
        StratLongText(9) = "P, D"
        StratLongText(10) = "P, H"
        StratLongText(11) = "P, R"
        StratLongText(12) = "Don't S"
        StratLongText(13) = "Don't D"
        StratLongText(14) = "Don't H"
        StratLongText(15) = "Don't R"
        StratLongText(16) = "Don't P"

        StratShortText(0) = "--"
        StratShortText(1) = "S"
        StratShortText(2) = "D"
        StratShortText(3) = "DS"
        StratShortText(4) = "H"
        StratShortText(5) = "R"
        StratShortText(6) = "RS"
        StratShortText(7) = "P"
        StratShortText(8) = "PS"
        StratShortText(9) = "PD"
        StratShortText(10) = "PH"
        StratShortText(11) = "PR"
        StratShortText(12) = "xS"
        StratShortText(13) = "xD"
        StratShortText(14) = "xH"
        StratShortText(15) = "xR"
        StratShortText(16) = "xP"
    End Sub

    Public Sub LoadExceptionNames()
        ReDim ExText(NumExceptionTypes - 1)

        ExText(0) = "None"

        ExText(1) = "CD-TD"
        ExText(2) = "CD-TC"
        ExText(3) = "CD-Forced"
        ExText(4) = "Forced-TD"
        ExText(5) = "Forced-TC"
        ExText(6) = "TC-TD"

        ExText(7) = "CD-TD"
        ExText(8) = "CD-TC"
        ExText(9) = "CD-Forced"
        ExText(10) = "CD-CD"

        ExText(11) = "TD-TD"
        ExText(12) = "TC-TC"
        ExText(13) = "Forced-Forced"

    End Sub

    Public Enum Suits
        Spades = 0
        Hearts = 1
        Diamonds = 2
        Clubs = 3
    End Enum

    Public Enum Surr
        NS = 0
        LS = 1
        ES = 2
    End Enum

    'Shoe States for Splits - The original pair is assumed to have been removed
    Public Enum Hands
        Full = 0    'Full Deck
        P = 1       '1P Removed
        PP = 2      '2P's Removed
        N = 3       '1N Removed
        PN = 4      '1P, 1N Removed
        PPP = 5     '3P's Removed
        PPPP = 6    '4P's Removed
        PPN = 7     '2P's, 1N Removed
        PPPN = 8    '3P's, 1N Removed
        PNN = 9     '1P, 2N's Removed
        PPNN = 10   '2P's, 2N's Removed
    End Enum

End Class
