Imports BJ_CA.BJCAShared

<Serializable()> Public Class BJCABonusRulesClass
    Public Name As String
    Public RuleOn As Boolean
    Public UseSpecificHand As Boolean

    Public Hand As New BJCAHandClass
    Public HardSoftOnly As Boolean
    Public HardOnly As Boolean
    Public SoftOnly As Boolean
    Public ExactMatch As Boolean
    Public OrMore As Boolean
    Public OrLess As Boolean
    Public SpecificTen As Boolean
    Public SpecificTenFraction As Double

    Public PayoffGeneral As Double
    Public PayoffSuited As Double
    Public PayoffSpecificSuit As Double

    Public PayoffGeneralBJ As Double
    Public PayoffSuitedBJ As Double
    Public PayoffSpecificSuitBJ As Double
    Public BJAUp As Boolean
    Public BJTUp As Boolean

    Public PayoffUCGeneral As Double
    Public PayoffUCSuited As Double
    Public PayoffUCSpecificSuit As Double

    Public Upcard As Integer

    Public Suited As Boolean
    Public SpecificSuit As Boolean
    Public SuitToWin As Integer

    Public MustWin As Boolean
    Public HandContinues As Boolean
    Public PreSplit As Boolean
    Public PostSplit As Boolean

    Public Index As Integer

    Public Sub New()
        Name = ""
        RuleOn = False
        UseSpecificHand = False

        Hand.Empty()
        HardSoftOnly = False
        HardOnly = True
        SoftOnly = False
        OrMore = False
        OrLess = False
        ExactMatch = True
        SpecificTen = False
        SpecificTenFraction = 0.25

        PayoffGeneral = 1
        PayoffSuited = 0
        PayoffSpecificSuit = 0
        PayoffGeneralBJ = -1
        PayoffSuitedBJ = -1
        PayoffSpecificSuitBJ = -1
        BJAUp = True
        BJTUp = True

        PayoffUCGeneral = 0
        PayoffUCSuited = 0
        PayoffUCSpecificSuit = 0
        Upcard = 0

        Suited = False
        SpecificSuit = False
        SuitToWin = 0

        MustWin = False
        HandContinues = False
        PreSplit = True
        PostSplit = False

        Index = -1
    End Sub

    Public Sub Empty()
        Name = ""
        RuleOn = False
        UseSpecificHand = False

        Hand.Empty()
        HardSoftOnly = False
        HardOnly = False
        SoftOnly = False
        OrMore = False
        OrLess = False
        ExactMatch = True
        SpecificTen = False
        SpecificTenFraction = 0.25

        PayoffGeneral = 1
        PayoffSuited = 0
        PayoffSpecificSuit = 0
        PayoffGeneralBJ = -1
        PayoffSuitedBJ = -1
        PayoffSpecificSuitBJ = -1
        BJAUp = True
        BJTUp = True

        PayoffUCGeneral = 0
        PayoffUCSuited = 0
        PayoffUCSpecificSuit = 0
        Upcard = 0

        Suited = False
        SpecificSuit = False
        SuitToWin = 0

        MustWin = False
        HandContinues = False
        PreSplit = True
        PostSplit = False

        Index = -1
    End Sub

End Class

<Serializable()> Public Class BJCABonusRulesListClass
    Public NumRules As Integer
    Public L() As BJCABonusRulesClass

    Public Sub New()
        NumRules = 0
    End Sub

    Public Sub Empty()
        Dim rule As Integer

        For rule = 0 To NumRules - 1
            L(rule) = Nothing
        Next rule
        ReDim L(0)
        NumRules = 0
    End Sub

    Public Sub AddBonusRule(ByVal newRule As BJCABonusRulesClass)
        NumRules += 1
        ReDim Preserve L(NumRules - 1)
        L(NumRules - 1) = New BJCABonusRulesClass
        L(NumRules - 1) = CType(CloneObject(newRule), BJCABonusRulesClass)
        L(NumRules - 1).Index = NumRules - 1
    End Sub

    Public Sub DeleteBonusRule(ByVal index As Integer)
        Dim rule As Integer

        For rule = index + 1 To NumRules - 1
            L(rule).Index -= 1
            L(rule - 1) = L(rule)
        Next rule
        L(NumRules - 1) = Nothing
        NumRules -= 1
        ReDim Preserve L(NumRules - 1)
    End Sub

    Public Sub MoveBonusRuleUp(ByVal currentIndex As Integer)
        Dim tempRule As New BJCABonusRulesClass

        If currentIndex > 0 And NumRules > 1 Then
            'First move the index
            L(currentIndex).Index -= 1
            L(currentIndex - 1).Index += 1

            'Then move the actual rule
            tempRule = L(currentIndex)
            L(currentIndex) = L(currentIndex - 1)
            L(currentIndex - 1) = tempRule

        End If
    End Sub

    Public Sub MoveBonusRuleDown(ByVal currentIndex As Integer)
        Dim tempRule As New BJCABonusRulesClass

        If currentIndex < NumRules - 1 And NumRules > 1 Then
            'First move the index
            L(currentIndex).Index += 1
            L(currentIndex + 1).Index -= 1

            'Then move the actual rule
            tempRule = L(currentIndex)
            L(currentIndex) = L(currentIndex + 1)
            L(currentIndex + 1) = tempRule
        End If
    End Sub

End Class

<Serializable()> Public Class BJCASuitedBonusEVClass
    Public SuitedNetEV(10) As Double
    Public SuitedNetBJEV(10) As Double
    Public SuitedStratEV(10, 4) As Double
    Public SuitedStratBJEV(10, 4) As Double
    Public SuitedStrat(10, 4) As Integer

    Public SuitedStandEV(10, 4) As Double
    Public SuitedStandBJEV(10, 4) As Double
    Public SuitedHitEV(10, 4) As Double
    Public SuitedHitBJEV(10, 4) As Double

    Public SuitedBonusEV(10, 4) As Double

    Public Sub New()
        Dim card As Integer
        Dim suit As Integer

        For card = 0 To 10
            SuitedNetEV(card) = 0
            SuitedNetBJEV(card) = 0
            For suit = 0 To 4
                SuitedStrat(card, suit) = 0
                SuitedStratEV(card, suit) = 0

                SuitedStandEV(card, suit) = 0
                SuitedHitEV(card, suit) = 0

                SuitedBonusEV(card, suit) = 0

                If card = 1 Or card = 10 Then
                    SuitedStratBJEV(card, suit) = 0
                    SuitedStandBJEV(card, suit) = 0
                    SuitedHitBJEV(card, suit) = 0
                End If
            Next
        Next card
    End Sub


    Public Sub Empty()
        Dim card As Integer
        Dim suit As Integer

        For card = 0 To 10
            SuitedNetEV(card) = 0
            SuitedNetBJEV(card) = 0
            For suit = 0 To 4
                SuitedStrat(card, suit) = 0
                SuitedStratEV(card, suit) = 0

                SuitedStandEV(card, suit) = 0
                SuitedHitEV(card, suit) = 0

                SuitedBonusEV(card, suit) = 0

                If card = 1 Or card = 10 Then
                    SuitedStratBJEV(card, suit) = 0
                    SuitedStandBJEV(card, suit) = 0
                    SuitedHitBJEV(card, suit) = 0
                End If
            Next
        Next card
    End Sub

End Class