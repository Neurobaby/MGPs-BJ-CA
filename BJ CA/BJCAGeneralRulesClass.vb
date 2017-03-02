<Serializable()> Public Class BJCAGeneralRulesClass
    Public FiniteDecks As Boolean
    Public InfiniteDecks As Boolean
    Public UseForcedShoe As Boolean
    Public NDecks As Integer
    Public SpanishDecks As Boolean

    Public BJPays As Single
    Public S17 As Boolean
    Public S18 As Boolean

    Public OBO As Boolean
    Public ENHC As Boolean
    Public BBO As Boolean
    Public OBBO As Boolean
    Public AOBBO As Boolean
    Public UseDefault As Boolean
    Public CheckAce As Boolean
    Public CheckTen As Boolean

    Public DAS As Boolean
    Public DAN As Boolean
    Public DOA As Boolean
    Public D91011 As Boolean
    Public D1011 As Boolean
    Public UseDTable As Boolean
    Public DDR As Boolean      'Double down rescue
    Public DDRPS As Boolean    'Double down rescue Post-Split
    Public DDRType As Integer
    Public RDA As Boolean      'Redoubling allowed
    Public RDAPS As Boolean    'Redoubling allowed Post-Split
    Public RDDepth As Integer  'Redouble Depth

    Public NS As Boolean
    Public LS As Boolean
    Public ES As Boolean
    Public ES10 As Boolean
    Public UseSTable As Boolean
    Public SurrPays As Single

    Public SPL0 As Boolean
    Public SPL1 As Boolean
    Public SPL2 As Boolean
    Public SPL3 As Boolean
    Public HSA As Boolean
    Public SMA As Boolean

    Public DSoftAllHard As Boolean
    Public DSoft19Hard As Boolean
    Public DSA As Boolean

    Public SurrDBJPays As Single
    Public SAN As Boolean
    Public SAS As Boolean
    Public SSA As Boolean
    Public MacauSurrender2to10 As Boolean
    Public MacauSurrenderAce As Boolean
    Public SurrToggleAll As Integer
    Public SurrComboValueArray(9) As Integer

    Public BJSPlitAces As Boolean
    Public BJSplitTens As Boolean
    Public SpToggleAll As Boolean
    Public SpCheckValueArray(9) As Boolean
    Public CDZ As Boolean
    Public CDP As Boolean
    Public CDPN As Boolean
    Public TDPlus As Boolean
    Public TCPlus As Boolean

    Public P21Autowin As Boolean
    Public PDTiesToggleAll As Integer
    Public PDTiesValueArray(5) As Single
    Public PDTiesComboValueArray(5) As Integer

    Public RefDecks As Single

    Public BJBonuses As New BJCABonusRulesClass

    Public UCToggleAll As Boolean
    Public UCCheckValueArray(9) As Boolean

    Public ComputeTD As Boolean
    Public ComputeTC As Boolean
    Public ComputeForced As Boolean
    Public PrintPSExceptions As Boolean
    Public UseDPDictionary As Boolean
    Public SaveDPDictionary As Boolean
    Public RTSPL1Est As Boolean
    Public RTSmall As Boolean

End Class
