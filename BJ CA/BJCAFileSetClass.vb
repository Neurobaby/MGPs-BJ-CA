<Serializable()> Public Class BJCAFileSetClass
    Public DoubleTableFileName As String
    Public ForcedShoeFileName As String
    Public BonusRulesFileName As String
    Public ColorTableFileName As String
    Public ForcedTablesFileName As String
    Public ForcedRulesFileName As String
    Public GeneralRulesFileName As String
    Public FileSetFileName As String
    Public ResultsFileName As String
    Public RuleSetFileName As String
    Public DefaultPath As String
    Public OutputPath As String
    Public BaseName As String

    Public ReadOnly DoubleTableFileExt As String = ".dt"
    Public ReadOnly ForcedShoeFileExt As String = ".shu"
    Public ReadOnly BonusRulesFileExt As String = ".br"
    Public ReadOnly ColorTableFileExt As String = ".col"
    Public ReadOnly ForcedTablesFileExt As String = ".ft"
    Public ReadOnly ForcedRulesFileExt As String = ".fr"
    Public ReadOnly GeneralRulesFileExt As String = ".gen"
    Public ReadOnly FileSetFileExt As String = ".set"
    Public ReadOnly ResultsFileExt As String = ".res"
    Public ReadOnly RuleSetFileExt As String = ".rs"

    Public Sub New()
        DoubleTableFileName = "Default"
        ForcedShoeFileName = "Default"
        BonusRulesFileName = "Default"
        ColorTableFileName = "Default"
        ForcedTablesFileName = "Default"
        ForcedRulesFileName = "Default"
        GeneralRulesFileName = "Default"
        FileSetFileName = "Default"
        ResultsFileName = "Default"
        RuleSetFileName = "Default"
        DefaultPath = "."
        OutputPath = "."
        BaseName = "Default"
    End Sub

End Class
