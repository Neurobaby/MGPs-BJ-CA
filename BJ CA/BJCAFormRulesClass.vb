Imports BJ_CA.BJCAShared

<Serializable()> Public Class BJCAFormRulesClass

#Region " Declarations "

    'Bonus Rules Declarations
    Public BonusRulesList As New BJCABonusRulesListClass
    Public DefaultBonusRulesList As New BJCABonusRulesListClass

    'Forced Rules Declarations
    Public ForcedStrat As New BJCAForcedRulesStratClass
    Public DefaultForcedRulesList As New BJCAForcedRulesListClass

    'Other Declarations
    Public General As New BJCAGeneralRulesClass
    Public DefaultGeneralRules As New BJCAGeneralRulesClass

    Public DoubleTable As New BJCADoubleTableClass
    Public ForcedShoe As New BJCAShoeClass
    Public ColorTable As New BJCAColorTableClass
    Public DefaultColorTable As New BJCAColorTableClass
    Public PDTies(22) As Integer                  '0=Player Wins  1=Push  2=Dealer Wins
    Public FileNames As New BJCAFileSetClass

#End Region

    Public Sub New()
        DefineDefaultGeneralRules()
        DefineDefaultBonusRules()
        DefineDefaultForcedStrat()
        DefineDefaultPDTies()
        DefineDefaultColorTable()

        General = CloneObject(DefaultGeneralRules)
        BonusRulesList = CloneObject(DefaultBonusRulesList)
        ForcedStrat.ForcedRulesList = CloneObject(DefaultForcedRulesList)
        ColorTable = CloneObject(DefaultColorTable)


    End Sub

#Region " Default Definitions "

    Public Sub DefineDefaultGeneralRules()
        Dim i As Integer

        With DefaultGeneralRules
            .FiniteDecks = True
            .InfiniteDecks = False
            .UseForcedShoe = False
            .NDecks = 1

            .BJPays = 1.5
            .S17 = True
            .S18 = False

            .OBO = True
            .ENHC = False
            .BBO = False
            .OBBO = False
            .AOBBO = False
            .UseDefault = True
            .CheckAce = True
            .CheckTen = True

            .DAS = True
            .DAN = False
            .DOA = True
            .D91011 = False
            .D1011 = False
            .UseDTable = False

            .NS = True
            .LS = False
            .ES = False
            .ES10 = False
            .UseSTable = False
            .SurrPays = -0.5

            .SPL0 = False
            .SPL1 = False
            .SPL2 = False
            .SPL3 = True
            .HSA = False
            .SMA = False

            .DSoftAllHard = False
            .DSoft19Hard = False
            .DSA = False
            .DDR = False
            .DDRType = 1
            .DDRPS = False
            .RDA = False
            .RDAPS = False
            .RDDepth = 1

            .SAN = False
            .SAS = False
            .SurrDBJPays = -0.5
            .SSA = False
            .MacauSurrender2to10 = False
            .MacauSurrenderAce = False
            .SurrToggleAll = 0
            For i = 0 To 9
                .SurrComboValueArray(i) = 0
            Next i

            .BJSPlitAces = False
            .BJSplitTens = False
            .SpToggleAll = True
            For i = 0 To 9
                .SpCheckValueArray(i) = True
            Next
            .CDZ = True
            .CDP = False
            .CDPN = False
            .TDPlus = False
            .TCPlus = False

            .P21Autowin = False
            .PDTiesToggleAll = 0
            For i = 0 To 5
                .PDTiesComboValueArray(i) = 0
                .PDTiesValueArray(i) = 0
            Next i

            .RefDecks = 6
            .SpanishDecks = False

            .BJBonuses.Empty()
            '.BJBonuses.SpecificTenFraction     Holds Spec10Bonus Specific 10 Fraction
            '.BJBonuses.PayoffBJ                Holds Spec10Bonus Payoff
            '.BJBonuses.PayoffGeneral           Holds Spec10Bonus Suited Payoff
            '.BJBonuses.PayoffUC                Holds Spec10Bonus Specific Suit Payoff
            '.BJBonuses.Upcard                  Holds Spec10Bonus Suit to Win
            .BJBonuses.SpecificTenFraction = 0.25
            .BJBonuses.PayoffGeneralBJ = 0
            .BJBonuses.PayoffGeneral = 0
            .BJBonuses.PayoffUCGeneral = 0
            .BJBonuses.Upcard = 0

            .UCToggleAll = True
            For i = 0 To 9
                .UCCheckValueArray(i) = True
            Next

            .ComputeTD = True
            .ComputeTC = True
            .ComputeForced = True
            .PrintPSExceptions = False
            .UseDPDictionary = True
            .SaveDPDictionary = True
            .RTSPL1Est = True
            .RTSmall = False
        End With

    End Sub

    Private Sub DefineDefaultBonusRules()
        DefaultBonusRulesList.NumRules = 9
        ReDim DefaultBonusRulesList.L(DefaultBonusRulesList.NumRules - 1)

        DefaultBonusRulesList.L(0) = New BJCABonusRulesClass
        DefaultBonusRulesList.L(1) = New BJCABonusRulesClass
        DefaultBonusRulesList.L(2) = New BJCABonusRulesClass
        DefaultBonusRulesList.L(3) = New BJCABonusRulesClass
        DefaultBonusRulesList.L(4) = New BJCABonusRulesClass
        DefaultBonusRulesList.L(5) = New BJCABonusRulesClass
        DefaultBonusRulesList.L(6) = New BJCABonusRulesClass
        DefaultBonusRulesList.L(7) = New BJCABonusRulesClass
        DefaultBonusRulesList.L(8) = New BJCABonusRulesClass

        With DefaultBonusRulesList.L(0)
            .Name = "5-card Charlie"
            .RuleOn = False
            .UseSpecificHand = False

            .Hand.NumCards = 5
            .Hand.Soft = False
            .HardSoftOnly = False
            .HardOnly = True
            .SoftOnly = False
            .OrMore = True
            .OrLess = False

            .ExactMatch = True
            .SpecificTen = False
            .SpecificTenFraction = 0.25

            .PayoffGeneral = 1
            .PayoffSuited = 0
            .PayoffSpecificSuit = 0
            .PayoffGeneralBJ = -1
            .PayoffSuitedBJ = -1
            .PayoffSpecificSuitBJ = -1
            .BJAUp = True
            .BJTUp = True

            .PayoffUCGeneral = 0
            .PayoffUCSuited = 0
            .PayoffUCSpecificSuit = 0
            .Upcard = 0

            .Suited = False
            .SpecificSuit = False
            .SuitToWin = 0

            .MustWin = False
            .HandContinues = False
            .PreSplit = True
            .PostSplit = False
            .Index = 0

        End With

        With DefaultBonusRulesList.L(1)
            .Name = "6-card Charlie"
            .RuleOn = False
            .UseSpecificHand = False

            .Hand.NumCards = 6
            .Hand.Soft = False
            .HardSoftOnly = False
            .HardOnly = True
            .SoftOnly = False
            .OrMore = True
            .OrLess = False

            .ExactMatch = True
            .SpecificTen = False
            .SpecificTenFraction = 0.25

            .PayoffGeneral = 1
            .PayoffSuited = 0
            .PayoffSpecificSuit = 0
            .PayoffGeneralBJ = -1
            .PayoffSuitedBJ = -1
            .PayoffSpecificSuitBJ = -1
            .BJAUp = True
            .BJTUp = True

            .PayoffUCGeneral = 0
            .PayoffUCSuited = 0
            .PayoffUCSpecificSuit = 0
            .Upcard = 0

            .Suited = False
            .SpecificSuit = False
            .SuitToWin = 0

            .MustWin = False
            .HandContinues = False
            .PreSplit = True
            .PostSplit = False
            .Index = 1

        End With

        With DefaultBonusRulesList.L(2)
            .Name = "6-7-8"
            .RuleOn = False
            .UseSpecificHand = True

            .HardSoftOnly = False
            .HardOnly = True
            .SoftOnly = False
            .OrMore = False
            .OrLess = False

            .ExactMatch = True
            .Hand.Cards(6) = 1
            .Hand.Cards(7) = 1
            .Hand.Cards(8) = 1
            .Hand.NumCards = 3
            .Hand.Total = 21
            .Hand.Soft = False
            .SpecificTen = False
            .SpecificTenFraction = 0.25

            .PayoffGeneral = 2
            .PayoffSuited = 0
            .PayoffSpecificSuit = 0
            .PayoffGeneralBJ = -1
            .PayoffSuitedBJ = -1
            .PayoffSpecificSuitBJ = -1
            .BJAUp = True
            .BJTUp = True

            .PayoffUCGeneral = 0
            .PayoffUCSuited = 0
            .PayoffUCSpecificSuit = 0
            .Upcard = 0

            .Suited = False
            .SpecificSuit = False
            .SuitToWin = 0

            .MustWin = False
            .HandContinues = False
            .PreSplit = True
            .PostSplit = False
            .Index = 2

        End With

        With DefaultBonusRulesList.L(3)
            .Name = "7-7-7"
            .RuleOn = False
            .UseSpecificHand = True

            .HardSoftOnly = False
            .HardOnly = True
            .SoftOnly = False
            .OrMore = False
            .OrLess = False

            .ExactMatch = True
            .Hand.Cards(7) = 3
            .Hand.NumCards = 3
            .Hand.Total = 21
            .Hand.Soft = False
            .SpecificTen = False
            .SpecificTenFraction = 0.25

            .PayoffGeneral = 2
            .PayoffSuited = 0
            .PayoffSpecificSuit = 0
            .PayoffGeneralBJ = -1
            .PayoffSuitedBJ = -1
            .PayoffSpecificSuitBJ = -1
            .BJAUp = True
            .BJTUp = True

            .PayoffUCGeneral = 0
            .PayoffUCSuited = 0
            .PayoffUCSpecificSuit = 0
            .Upcard = 0

            .Suited = False
            .SpecificSuit = False
            .SuitToWin = 0

            .MustWin = False
            .HandContinues = False
            .PreSplit = True
            .PostSplit = False
            .Index = 3

        End With

        With DefaultBonusRulesList.L(4)
            .Name = "5-Card 21"
            .RuleOn = False
            .UseSpecificHand = False

            .Hand.NumCards = 5
            .Hand.Total = 21
            .Hand.Soft = False
            .HardSoftOnly = False
            .HardOnly = True
            .SoftOnly = False
            .OrMore = True
            .OrLess = False

            .ExactMatch = True
            .SpecificTen = False
            .SpecificTenFraction = 0.25

            .PayoffGeneral = 2
            .PayoffSuited = 0
            .PayoffSpecificSuit = 0
            .PayoffGeneralBJ = -1
            .PayoffSuitedBJ = -1
            .PayoffSpecificSuitBJ = -1
            .BJAUp = True
            .BJTUp = True

            .PayoffUCGeneral = 0
            .PayoffUCSuited = 0
            .PayoffUCSpecificSuit = 0
            .Upcard = 0

            .Suited = False
            .SpecificSuit = False
            .SuitToWin = 0

            .MustWin = False
            .HandContinues = False
            .PreSplit = True
            .PostSplit = False
            .Index = 4

        End With

        With DefaultBonusRulesList.L(5)
            .Name = "5-Card 21 Must Win"
            .RuleOn = False
            .UseSpecificHand = False
            .Hand.NumCards = 5
            .Hand.Total = 21
            .Hand.Soft = False
            .HardSoftOnly = False
            .HardOnly = True
            .SoftOnly = False
            .OrMore = True
            .OrLess = False

            .ExactMatch = True
            .SpecificTen = False
            .SpecificTenFraction = 0.25

            .PayoffGeneral = 2
            .PayoffSuited = 0
            .PayoffSpecificSuit = 0
            .PayoffGeneralBJ = -1
            .PayoffSuitedBJ = -1
            .PayoffSpecificSuitBJ = -1
            .BJAUp = True
            .BJTUp = True

            .PayoffUCGeneral = 0
            .PayoffUCSuited = 0
            .PayoffUCSpecificSuit = 0
            .Upcard = 0

            .Suited = False
            .SpecificSuit = False
            .SuitToWin = 0

            .MustWin = True
            .HandContinues = False
            .PreSplit = True
            .PostSplit = False
            .Index = 5

        End With

        With DefaultBonusRulesList.L(6)
            .Name = "6-Card 21"
            .RuleOn = False
            .UseSpecificHand = False

            .Hand.NumCards = 6
            .Hand.Total = 21
            .Hand.Soft = False
            .HardSoftOnly = False
            .HardOnly = True
            .SoftOnly = False
            .OrMore = True
            .OrLess = False

            .ExactMatch = True
            .SpecificTen = False
            .SpecificTenFraction = 0.25

            .PayoffGeneral = 2
            .PayoffSuited = 0
            .PayoffSpecificSuit = 0
            .PayoffGeneralBJ = -1
            .PayoffSuitedBJ = -1
            .PayoffSpecificSuitBJ = -1
            .BJAUp = True
            .BJTUp = True

            .PayoffUCGeneral = 0
            .PayoffUCSuited = 0
            .PayoffUCSpecificSuit = 0
            .Upcard = 0

            .Suited = False
            .SpecificSuit = False
            .SuitToWin = 0

            .MustWin = False
            .HandContinues = False
            .PreSplit = True
            .PostSplit = False
            .Index = 6

        End With

        With DefaultBonusRulesList.L(7)
            .Name = "6-7-8 Suited"
            .RuleOn = False
            .UseSpecificHand = True

            .HardSoftOnly = False
            .HardOnly = True
            .SoftOnly = False
            .OrMore = False
            .OrLess = False

            .ExactMatch = True
            .Hand.Cards(6) = 1
            .Hand.Cards(7) = 1
            .Hand.Cards(8) = 1
            .Hand.NumCards = 3
            .Hand.Total = 21
            .Hand.Soft = False
            .SpecificTen = False
            .SpecificTenFraction = 0.25

            .PayoffGeneral = 2
            .PayoffSuited = 3
            .PayoffSpecificSuit = 0
            .PayoffGeneralBJ = -1
            .PayoffSuitedBJ = -1
            .PayoffSpecificSuitBJ = -1
            .BJAUp = True
            .BJTUp = True

            .PayoffUCGeneral = 0
            .PayoffUCSuited = 0
            .PayoffUCSpecificSuit = 0
            .Upcard = 0

            .Suited = True
            .SpecificSuit = False
            .SuitToWin = 0

            .MustWin = False
            .HandContinues = False
            .PreSplit = True
            .PostSplit = False
            .Index = 2

        End With

        With DefaultBonusRulesList.L(8)
            .Name = "7-7-7 Suited"
            .RuleOn = False
            .UseSpecificHand = True

            .HardSoftOnly = False
            .HardOnly = True
            .SoftOnly = False
            .OrMore = False
            .OrLess = False

            .ExactMatch = True
            .Hand.Cards(7) = 3
            .Hand.NumCards = 3
            .Hand.Total = 21
            .Hand.Soft = False
            .SpecificTen = False
            .SpecificTenFraction = 0.25

            .PayoffGeneral = 2
            .PayoffSuited = 3
            .PayoffSpecificSuit = 0
            .PayoffGeneralBJ = -1
            .PayoffSuitedBJ = -1
            .PayoffSpecificSuitBJ = -1
            .BJAUp = True
            .BJTUp = True

            .PayoffUCGeneral = 0
            .PayoffUCSuited = 0
            .PayoffUCSpecificSuit = 0
            .Upcard = 0

            .Suited = True
            .SpecificSuit = False
            .SuitToWin = 0

            .MustWin = False
            .HandContinues = False
            .PreSplit = True
            .PostSplit = False
            .Index = 3

        End With

    End Sub

    Private Sub DefineDefaultForcedStrat()
        ForcedStrat.ForcednCD = 2
        ForcedStrat.ForcedTablePreSplit = True
        ForcedStrat.ForcedTablePostSplit = True

        DefineDefaultForcedRules()
    End Sub

    Private Sub DefineDefaultForcedRules()
        DefaultForcedRulesList.NumRules = 1
        ReDim DefaultForcedRulesList.L(DefaultForcedRulesList.NumRules - 1)

        DefaultForcedRulesList.L(0) = New BJCAForcedRulesClass
        '        DefaultForcedRulesList.L(1) = New BJCAForcedRulesClass
        '        DefaultForcedRulesList.L(2) = New BJCAForcedRulesClass
        '        DefaultForcedRulesList.L(3) = New BJCAForcedRulesClass
        '        DefaultForcedRulesList.L(4) = New BJCAForcedRulesClass
        '        DefaultForcedRulesList.L(5) = New BJCAForcedRulesClass
        '        DefaultForcedRulesList.L(6) = New BJCAForcedRulesClass

        With DefaultForcedRulesList.L(0)
            .Name = "16 v 10 Multicard Stand"
            .RuleOn = False
            .UseSpecificHand = False

            .Hand.Total = 16
            .Hand.Soft = False
            .Hand.NumCards = 3

            .ExactMatch = False

            .OrMore = True
            .OrLess = False

            .PreSplit = True
            .PostSplit = True

            .Upcard = 10
            .Strat = 1
            .Index = 0
        End With
    End Sub

    Public Sub DefineDefaultPDTies()
        Dim total As Integer

        For total = 17 To 22
            PDTies(total) = 0
            PDTies(total) = 0
            PDTies(total) = 0
        Next total
    End Sub

    Public Sub DefineDefaultColorTable()
        DefaultColorTable.C(BJCAGlobalsClass.Strat.None) = System.Drawing.Color.White            'Blank strategy
        DefaultColorTable.C(BJCAGlobalsClass.Strat.S) = System.Drawing.Color.Red                 'Stand
        DefaultColorTable.C(BJCAGlobalsClass.Strat.D) = System.Drawing.Color.Yellow              'Double, Hit
        DefaultColorTable.C(BJCAGlobalsClass.Strat.DS) = System.Drawing.Color.Yellow             'Double, Stand
        DefaultColorTable.C(BJCAGlobalsClass.Strat.H) = System.Drawing.Color.Lime                'Hit
        DefaultColorTable.C(BJCAGlobalsClass.Strat.R) = System.Drawing.Color.HotPink             'Surrender, Hit
        DefaultColorTable.C(BJCAGlobalsClass.Strat.RS) = System.Drawing.Color.HotPink            'Surrender, Stand
        DefaultColorTable.C(BJCAGlobalsClass.Strat.P) = System.Drawing.Color.DeepSkyBlue          'Split
        DefaultColorTable.C(BJCAGlobalsClass.Strat.PS) = System.Drawing.Color.DeepSkyBlue         'Split, Stand
        DefaultColorTable.C(BJCAGlobalsClass.Strat.PD) = System.Drawing.Color.DeepSkyBlue         'Split, Double
        DefaultColorTable.C(BJCAGlobalsClass.Strat.PH) = System.Drawing.Color.DeepSkyBlue         'Split, Hit
        DefaultColorTable.C(BJCAGlobalsClass.Strat.PR) = System.Drawing.Color.DeepSkyBlue         'Split, Surrender

        DefaultColorTable.C(BJCAGlobalsClass.Strat.xS) = System.Drawing.Color.DarkSalmon         'Don't Stand
        DefaultColorTable.C(BJCAGlobalsClass.Strat.xD) = System.Drawing.Color.Khaki              'Don't Double, Hit
        DefaultColorTable.C(BJCAGlobalsClass.Strat.xH) = System.Drawing.Color.YellowGreen        'Don't Hit
        DefaultColorTable.C(BJCAGlobalsClass.Strat.xR) = System.Drawing.Color.PaleVioletRed      'Don't Surrender, Hit
        DefaultColorTable.C(BJCAGlobalsClass.Strat.xP) = System.Drawing.Color.MediumAquamarine          'Don't Split
    End Sub

#End Region

End Class
