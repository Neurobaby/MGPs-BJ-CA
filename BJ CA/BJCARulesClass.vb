<Serializable()> Public Class BJCARulesClass
    Public BJBonuses As New BJCABonusRulesClass
    Public BonusRulesList As New BJCABonusRulesListClass
    Public ForcedTableRulesList As New BJCAForcedRulesListClass
    Public ForcedRulesList As New BJCAForcedRulesListClass
    Public ColorTable As New BJCAColorTableClass

    Public InfiniteDecks As Boolean
    Public Shoe As New BJCAShoeClass

    Public BJPays As Double
    Public StandOnSoft As Integer

    Public ENHC As Boolean
    Public BBO As Boolean
    Public OBBO As Boolean
    Public AOBBO As Boolean
    Public CheckAce As Boolean
    Public CheckTen As Boolean

    Public DoubleTable(21, 3, 1) As Boolean 'Total, 2CH-MultiH-2CS-MultiS, Pre/Post Split
    Public DAS As Boolean
    Public DSoftAllHard As Boolean
    Public DSoft19Hard As Boolean
    Public DSA As Boolean
    Public DDR As Boolean       'Double down rescue
    Public DDRPS As Boolean     'Double down rescue Post-Split
    Public DDRType As Integer
    Public RDA As Boolean       'Redoubling allowed
    Public RDAPS As Boolean     'Redoubling allowed Post-Split
    Public RDDepth As Integer   'Redouble Depth

    Public SPL As Integer
    Public HSA As Boolean
    Public SMA As Boolean

    Public SurrPays As Double
    Public SurrDBJPays As Double
    Public SAN As Boolean
    Public SAS As Boolean
    Public SSA As Boolean
    Public MacauSurrender2to10 As Boolean
    Public MacauSurrenderAce As Boolean
    Public SurrenderTable(10) As Integer

    Public BJSPlitAces As Boolean
    Public BJSplitTens As Boolean

    Public SplitAllowed(10) As Boolean
    Public CDP As Boolean
    Public CDPN As Boolean
    Public TDPlus As Boolean
    Public TCPlus As Boolean

    Public P21Autowin As Boolean
    Public PDTies(22) As Double

    Public DeckType As String
    Public DoubleType As String
    Public SurrType As String
    Public DAN As Boolean

    Public ForcednCD As Integer
    Public ForcedTablePreSplit As Boolean
    Public ForcedTablePostSplit As Boolean

    Public UCAllowed(10) As Boolean

    Public ComputeTD As Boolean
    Public ComputeTC As Boolean
    Public ComputeForced As Boolean
    Public PrintPSExceptions As Boolean
    Public UseDPDictionary As Boolean
    Public SaveDPDictionary As Boolean

    Public OutputPath As String
    Public ExcelFilePath As String
    Public ProbsPath As String

End Class
