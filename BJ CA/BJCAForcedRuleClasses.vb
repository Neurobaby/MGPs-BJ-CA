Imports BJ_CA.BJCAShared

<Serializable()> Public Class BJCAForcedRulesClass
    Public Name As String
    Public RuleOn As Boolean
    Public UseSpecificHand As Boolean

    Public Hand As New BJCAHandClass
    Public ExactMatch As Boolean

    Public OrMore As Boolean
    Public OrLess As Boolean

    Public PreSplit As Boolean
    Public PostSplit As Boolean

    Public Upcard As Integer
    Public Strat As Integer

    Public Index As Integer

    Public Sub New()
        Name = ""
        RuleOn = False
        UseSpecificHand = False

        Hand.Empty()
        ExactMatch = True

        OrMore = False
        OrLess = False

        PreSplit = True
        PostSplit = True

        Upcard = 0
        Strat = 0

        Index = -1
    End Sub

    Public Sub Empty()
        Name = ""
        RuleOn = False
        UseSpecificHand = False

        Hand.Empty()
        ExactMatch = True

        OrMore = False
        OrLess = False

        PreSplit = True
        PostSplit = True

        Upcard = 0
        Strat = 0

        Index = -1
    End Sub

End Class

<Serializable()> Public Class BJCAForcedRulesListClass
    Public NumRules As Integer
    Public L() As BJCAForcedRulesClass

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

    Public Sub AddForcedRule(ByVal newRule As BJCAForcedRulesClass)
        NumRules += 1
        ReDim Preserve L(NumRules - 1)
        L(NumRules - 1) = New BJCAForcedRulesClass
        L(NumRules - 1) = CType(CloneObject(newRule), BJCAForcedRulesClass)
        L(NumRules - 1).Index = NumRules - 1
    End Sub

    Public Sub DeleteForcedRule(ByVal index As Integer)
        Dim rule As Integer

        For rule = index + 1 To NumRules - 1
            L(rule).Index -= 1
            L(rule - 1) = L(rule)
        Next rule
        L(NumRules - 1) = Nothing
        NumRules -= 1
        ReDim Preserve L(NumRules - 1)
    End Sub

    Public Sub MoveForcedRuleUp(ByVal currentIndex As Integer)
        Dim tempRule As New BJCAForcedRulesClass

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

    Public Sub MoveForcedRuleDown(ByVal currentIndex As Integer)
        Dim tempRule As New BJCAForcedRulesClass

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

<Serializable()> Public Class BJCAForcedRulesTableClass
    Public ForcednCD As Integer
    Public ForcedTablePreSplit As Boolean
    Public ForcedTablePostSplit As Boolean

    Public ForcedTableRulesList As New BJCAForcedRulesListClass
End Class

<Serializable()> Public Class BJCAForcedRulesStratClass
    Public ForcednCD As Integer
    Public ForcedTablePreSplit As Boolean
    Public ForcedTablePostSplit As Boolean

    Public ForcedRulesList As New BJCAForcedRulesListClass
    Public ForcedTableRulesList As New BJCAForcedRulesListClass
End Class

